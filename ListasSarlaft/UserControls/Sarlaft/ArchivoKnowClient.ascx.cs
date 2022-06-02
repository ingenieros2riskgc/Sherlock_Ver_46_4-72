using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls
{
    public partial class ArchivoKnowClient : System.Web.UI.UserControl
    {
        private cKnowClient cKnowClient = new cKnowClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                inicializarValores();
            }
        }

        private void inicializarValores()
        {
            NameFile = string.Empty;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                    saveFile();
                else
                    Mensaje("No hay archivos para cargar.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el archivo. " + ex.Message);
            }
        }

        private void saveFile()
        {
            String extension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            String[] extensiones = { ".txt" };
            Boolean estado = false;

            for (int i = 0; i <= extensiones.Length - 1; i++)
            {
                if (extension == extensiones[i])
                {
                    estado = true;
                    break;
                }
            }
            if (estado)
            {
                NameFile = "KnowClient" + String.Format("{0:yyyy MMMM dd-hh mm ss-ffff tt}", DateTime.Now) + extension;
                FileUpload1.SaveAs(Server.MapPath("~/Archivos/KnowClient/") + NameFile);
                loadInfo();
            }
            else
                Mensaje("Unicamente archivos de texto (.txt)");
        }

        private void loadInfo()
        {
            string filePath = Server.MapPath("~/Archivos/KnowClient/") + NameFile;
            StreamReader sr = new StreamReader(filePath);
            string line;
            string[] stringSeparators = new string[] { "|" };

            try
            {
                if (sr.Peek() >= 0)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        int IdConocimientoCliente;
                        string[] result = line.Split(stringSeparators, StringSplitOptions.None);
                        IdConocimientoCliente = cKnowClient.agregarConocimientoCliente(String.Format("{0:yyyy MM dd}", DateTime.Now).Replace(" ", ""), String.Format("{0:yyyy}", DateTime.Now));
                        cKnowClient.InfoFormCliente(IdConocimientoCliente, "", "---", "", "---", "", "---", "", "---", "");
                        cKnowClient.InfoFormPN(IdConocimientoCliente, result[0].ToString().Trim(), result[1].ToString().Trim(), result[2].ToString().Trim(), "---", result[3].ToString().Trim(), "", "", "", "", "---", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "---", "---", "---", "", "", "", "", "", "", "", "", "");
                        cKnowClient.InfoFormPJ(IdConocimientoCliente, result[4].ToString().Trim(), result[5].ToString().Trim(), "", "", "", "---", "", "", "", "", "", "", "", "", "", "", "", "---", "---", "", "", "", "---", "", "", "---", "", "", "---", "", "", "---", "", "", "---", "", "", "", "", "", "", "", "", "", "");
                        cKnowClient.InfoFormPF(IdConocimientoCliente, "---", "---", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                        cKnowClient.InfoFormSeguros(IdConocimientoCliente, "", "", "", "", "---", "", "", "", "", "---", "");
                        cKnowClient.InfoFormEntrevista(IdConocimientoCliente, "", "", "", "---", "", "", "", "", "", "");
                        cKnowClient.InfoFormDocsInu(IdConocimientoCliente, "NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO", "", "NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO", "NO");
                    }
                    Mensaje("Información cargada con exito.");
                }
                else
                {
                    Mensaje("El archivo esta vacio.");
                }
                sr.Close();
                sr.Dispose();
                File.Delete(Server.MapPath("~/Archivos/KnowClient/") + NameFile);
            }
            catch (Exception ex)
            {
                sr.Close();
                sr.Dispose();
                File.Delete(Server.MapPath("~/Archivos/KnowClient/") + NameFile);
                throw new Exception(ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Properties

        private String nameFile;
        private String NameFile
        {
            get
            {
                nameFile = (String)ViewState["nameFile"];
                return nameFile;
            }
            set
            {
                nameFile = value;
                ViewState["nameFile"] = nameFile;
            }
        }

        #endregion
    }
}