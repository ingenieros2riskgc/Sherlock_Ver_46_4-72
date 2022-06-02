using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class ParRieProbabilidad : System.Web.UI.UserControl
    {
		string IdFormulario = "5001";
        cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnInsertarNuevo);
            scriptManager.RegisterPostBackControl(this.ImbViewJPG);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            if (!Page.IsPostBack)
                loadInfo();
        }

        private void loadInfo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParametrizacionRiesgos.loadInfoProbabilidad();
            TextBox1.Text = dtInfo.Rows[0]["NombreProbabilidad"].ToString().Trim();
            TextBox2.Text = dtInfo.Rows[1]["NombreProbabilidad"].ToString().Trim();
            TextBox3.Text = dtInfo.Rows[2]["NombreProbabilidad"].ToString().Trim();
            TextBox4.Text = dtInfo.Rows[3]["NombreProbabilidad"].ToString().Trim();
            TextBox5.Text = dtInfo.Rows[4]["NombreProbabilidad"].ToString().Trim();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    bool booResult = false;
                    clsDTOParaProbabilidad Probabilidad = new clsDTOParaProbabilidad();
                    clsBLLParaProbabilidad ProbabilidadBLL = new clsBLLParaProbabilidad();
                    /*cParametrizacionRiesgos.modificarRegistroProbabilidad(TextBox1.Text.Trim(), TextBox2.Text.Trim(), 
                        TextBox3.Text.Trim(), TextBox4.Text.Trim(), TextBox5.Text.Trim());*/
                    Probabilidad.strNombreProbabilidad1 = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
                    Probabilidad.strNombreProbabilidad2 = Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim());
                    Probabilidad.strNombreProbabilidad3 = Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim());
                    Probabilidad.strNombreProbabilidad4 = Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim());
                    Probabilidad.strNombreProbabilidad5 = Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim());
                    string strErrMsg = string.Empty;
                    booResult = ProbabilidadBLL.mtdActualizarParaProbabilidad(Probabilidad, ref strErrMsg);
                    if (booResult == true)
                    {
                        loadInfo();
                        Mensaje("Información actualizada con éxito");
                    }
                    else
                    {
                        Mensaje(strErrMsg);
                    }
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la información. " + ex.Message); 
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            bool booResult = new bool();
            string extension = System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim();
            if (fuArchivoPerfil.HasFile)
            {
                if (extension == ".jpeg" || extension == ".gif" || extension == ".png" || extension == ".jpg")
            {
                
                booResult = mtdInsertarImagen(ref strErrMsg);
                if (booResult == false)
                {
                    Mensaje("Error: en la inserción de la imagen");
                }
                else
                {
                    Mensaje("Imagen cargada satisfactoriamente");
                    loadInfo();
                }
            }
                else
                {
                    Mensaje("Error: formato invalido, por favor cargar en formato png,jpeg,gif o jpg");
                }
            }
            else
            {
                Mensaje("Error: No se ha seleccionado un archivo para su cargue");
            }
            
        }
        private bool mtdInsertarImagen(ref string strErrMsg)
        {
            bool booResult = false;
            string pathFile = string.Empty;
            pathFile = "FrecuenciaImg" + fuArchivoPerfil.FileName;
            Byte[] archivo = fuArchivoPerfil.FileBytes;
            int length = Convert.ToInt32(fuArchivoPerfil.FileContent.Length);
            string extension = System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim();
            string modulo = "Frecuencia";
            saveFile(ref booResult, pathFile, length, archivo, modulo, extension);
            return booResult;
        }
        private void saveFile(ref bool booResult,string NombreArchivo, int Length, byte[] archivo, string modulo, string extension)
        {
            /*string path = ConfigurationManager.AppSettings.Get("DirectorioDocumentos").ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //string fullPath = Server.MapPath("/");
            fuArchivoPerfil.PostedFile.SaveAs(path + NombreArchivo);*/
            string strErrMsg = string.Empty;
            clsBLLParaProbabilidad cProbabilidad = new clsBLLParaProbabilidad();

            if (!cProbabilidad.mtdInsertarArchivo(ref  booResult,NombreArchivo, Length, archivo, modulo, ref strErrMsg, extension))
                Mensaje("Error: en la inserción de la imagen");
        }

        protected void ImbViewJPG_Click(object sender, ImageClickEventArgs e)
        {
            string str;
            str = "window.open('ViewImg/ViewImg.aspx?op=2','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }
    }
}