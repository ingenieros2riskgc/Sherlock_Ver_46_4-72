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
    public partial class ParConDesviaciones : System.Web.UI.UserControl
    {
        cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "5001";
        clsDTOParaDesviaciones ParaDesviaciones = new clsDTOParaDesviaciones();
        clsBLLParaDesviaciones DesviacionesBLL = new clsBLLParaDesviaciones();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadInfo();
            }
        }

        private void loadInfo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParametrizacionRiesgos.loadInfoDesviaciones();
            TextBox1.Text = dtInfo.Rows[0]["DesviacionProbabilidad"].ToString().Trim();
            TextBox2.Text = dtInfo.Rows[0]["DesviacionImpacto"].ToString().Trim();
            TextBox3.Text = dtInfo.Rows[1]["DesviacionProbabilidad"].ToString().Trim();
            TextBox4.Text = dtInfo.Rows[1]["DesviacionImpacto"].ToString().Trim();
            TextBox5.Text = dtInfo.Rows[2]["DesviacionProbabilidad"].ToString().Trim();
            TextBox6.Text = dtInfo.Rows[2]["DesviacionImpacto"].ToString().Trim();
            TextBox7.Text = dtInfo.Rows[3]["DesviacionProbabilidad"].ToString().Trim();
            TextBox8.Text = dtInfo.Rows[3]["DesviacionImpacto"].ToString().Trim();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (Page.IsValid)
                    {
                        //cParametrizacionRiesgos.modificarRegistroDesviaciones(TextBox1.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim(), TextBox4.Text.Trim(), TextBox5.Text.Trim(), TextBox6.Text.Trim(), TextBox7.Text.Trim(), TextBox8.Text.Trim());
                        bool booResult = false;
                        ParaDesviaciones.intDesviacionProbabilidad1 = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()));
                        ParaDesviaciones.intDesviacionProbabilidad2 = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()));
                        ParaDesviaciones.intDesviacionProbabilidad3 = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()));
                        ParaDesviaciones.intDesviacionProbabilidad4 = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()));

                        ParaDesviaciones.intDesviacionImpacto1 = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()));
                        ParaDesviaciones.intDesviacionImpacto2 = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()));
                        ParaDesviaciones.intDesviacionImpacto3 = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()));
                        ParaDesviaciones.intDesviacionImpacto4 = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox8.Text.Trim()));

                        string strErrMsg = string.Empty;

                        booResult = DesviacionesBLL.mtdActualizarParaDesviaciones(ParaDesviaciones, ref strErrMsg);
                        loadInfo();
                        Mensaje("Información actualizada con éxito");
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
    }
}