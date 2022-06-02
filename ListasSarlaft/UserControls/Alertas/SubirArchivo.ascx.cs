using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data.SqlClient;

namespace ListasSarlaft.UserControls.Alertas
{
    public partial class SubirArchivo : System.Web.UI.UserControl
    {
        private cListas cListas = new cListas();
        private cAlertas cAlertas = new cAlertas();

        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "6003";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
            }
        }

        /*
          Boton con la funcionalidad para la actualizacion de datos
         */
        protected void Button1_Click(object sender, EventArgs e)
        {
            int CantRegistros = 0;

            try
            {
                if (FileUpload1.HasFile)
                {
                    if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower() == ".txt")
                    {
                        saveFile();
                        truncateTable("Proceso.ArchivoGiros");
                        CantRegistros = loadInfoArchivoGirosCM(FileUpload1.FileName);               //Se llama el metodo para el cargue del SP que contiene el Bulk Insert
                        deleteFile();
                        Mensaje("Archivo " + nameFile + " cargado correctamente, Cantidad Registros: " + CantRegistros);
                    }
                    else
                        Mensaje("Unicamente archivos .txt");
                }
                else
                    Mensaje("No hay archivos para cargar.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la lista. " + ex.Message);
            }
        }

        private void deleteFile()
        {
            File.Delete(Server.MapPath("~/Archivos/Alertas/") + nameFile);
        }

        /*
         * Autor: Avis Fernando Torres
         * Fecha: 08/11/2013
         * Descripcion: Metodo que ejecuta un procedimiento almacenado que realiza un 
         * bulk insert para el cargue de datos mediante un txt, al momento de 
         * realizar la actualizacion por medio del Boton Actualizar
         */
        private int loadInfoArchivoGirosCM(string strRuta)
        {
            System.Configuration.Configuration rootwebconfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            int intRegistros = 0;
            try
            {
                if (rootwebconfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootwebconfig.ConnectionStrings.ConnectionStrings["SQLConnectionString"];
                    strRuta = Server.MapPath("~/Archivos/Alertas/") + strRuta;
                    using (SqlConnection connection = new SqlConnection(connString.ToString()))
                    {
                        connection.Open();
                        SqlCommand objComdProd = new SqlCommand("dbo.CargaGirosMasivos", connection);
                        objComdProd.CommandType = CommandType.StoredProcedure;
                        //Parametros del Procedimiento almacenado
                        objComdProd.Parameters.AddWithValue("@Ruta", strRuta);
                        objComdProd.CommandTimeout = 3600;
                        //Se ejecuta el Procedimiento almacenado
                        objComdProd.ExecuteNonQuery();

                        //Cantidad de Registros Procesados
                        SqlCommand objCmdRegistros = new SqlCommand("SELECT COUNT(NumeIdenRemitente) FROM [Proceso].[ArchivoGiros]", connection);
                        objCmdRegistros.CommandType = CommandType.Text;

                        intRegistros = int.Parse(objCmdRegistros.ExecuteScalar().ToString());

                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return intRegistros;
        }


        private void loadInfoArchivoGiros()
        {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Archivos/Alertas/") + nameFile + ";Extended Properties=Excel 8.0;";
            OleDbConnection objConn = new OleDbConnection(sConnectionString);
            try
            {
                objConn.Open();
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Hoja1$]", objConn);
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                objAdapter1.SelectCommand = objCmdSelect;
                DataTable dtInfo = new DataTable();
                objAdapter1.Fill(dtInfo);
                objConn.Close();
                cAlertas.loadGiros(dtInfo);
            }
            catch (Exception ex)
            {
                objConn.Close();
                throw new Exception(ex.Message);
            }
        }

        private void loadInfoArchivoPremios()
        {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Archivos/Alertas/") + nameFile + ";Extended Properties=Excel 8.0;";
            OleDbConnection objConn = new OleDbConnection(sConnectionString);
            try
            {
                objConn.Open();
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Hoja1$]", objConn);
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                objAdapter1.SelectCommand = objCmdSelect;
                DataTable dtInfo = new DataTable();
                objAdapter1.Fill(dtInfo);
                objConn.Close();
                cAlertas.loadPremios(dtInfo);
            }
            catch (Exception ex)
            {
                objConn.Close();
                throw new Exception(ex.Message);
            }
        }

        private void loadInfoOficinas()
        {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Archivos/Alertas/") + nameFile + ";Extended Properties=Excel 8.0;";
            OleDbConnection objConn = new OleDbConnection(sConnectionString);
            try
            {
                objConn.Open();
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Hoja1$]", objConn);
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                objAdapter1.SelectCommand = objCmdSelect;
                DataTable dtInfo = new DataTable();
                objAdapter1.Fill(dtInfo);
                objConn.Close();
                cAlertas.loadOficinas(dtInfo);
            }
            catch (Exception ex)
            {
                objConn.Close();
                throw new Exception(ex.Message);
            }
        }

        private void loadInfoOficinasRangos()
        {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Archivos/Alertas/") + nameFile + ";Extended Properties=Excel 8.0;";
            OleDbConnection objConn = new OleDbConnection(sConnectionString);
            try
            {
                objConn.Open();
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Hoja1$]", objConn);
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                objAdapter1.SelectCommand = objCmdSelect;
                DataTable dtInfo = new DataTable();
                objAdapter1.Fill(dtInfo);
                objConn.Close();
                cAlertas.loadOficinasRangos(dtInfo);
            }
            catch (Exception ex)
            {
                objConn.Close();
                throw new Exception(ex.Message);
            }
        }

        private void truncateTable(String table)
        {
            cAlertas.truncateTable(table);
        }

        private void saveFile()
        {
            nameFile = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString().Trim());
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Archivos/Alertas/") + nameFile);
        }

        private void saveFileGanadores()
        {
            nameFile = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName.ToString().Trim());
            FileUpload2.PostedFile.SaveAs(Server.MapPath("~/Archivos/Alertas/") + nameFile);
        }

        private void saveFileOficinas()
        {
            nameFile = System.IO.Path.GetFileName(FileUpload3.PostedFile.FileName.ToString().Trim());
            FileUpload3.PostedFile.SaveAs(Server.MapPath("~/Archivos/Alertas/") + nameFile);
        }

        private void saveFileOficinasRangos()
        {
            nameFile = System.IO.Path.GetFileName(FileUpload4.PostedFile.FileName.ToString().Trim());
            FileUpload4.PostedFile.SaveAs(Server.MapPath("~/Archivos/Alertas/") + nameFile);
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties
        private DataTable infoFile;
        private DataTable InfoFile
        {
            get
            {
                infoFile = (DataTable)Session["infoFile"];
                return infoFile;
            }
            set
            {
                infoFile = value;
                Session["infoFile"] = infoFile;
            }
        }

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

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload2.HasFile)
                {
                    if (System.IO.Path.GetExtension(FileUpload2.FileName).ToLower() == ".xls")
                    {
                        saveFileGanadores();
                        truncateTable("Proceso.ArchivoPremios");
                        loadInfoArchivoPremios();
                        deleteFile();
                        Mensaje("Archivo " + nameFile + " cargado correctamente");
                    }
                    else
                    {
                        Mensaje("Unicamente archivos .xls");
                    }
                }
                else
                    Mensaje("No hay archivos para cargar.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la lista. " + ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload3.HasFile)
                {
                    if (System.IO.Path.GetExtension(FileUpload3.FileName).ToLower() == ".xls")
                    {
                        saveFileOficinas();
                        truncateTable("Proceso.ArchivoOficinas");
                        loadInfoOficinas();
                        deleteFile();
                        Mensaje("Archivo " + nameFile + " cargado correctamente");
                    }
                    else
                    {
                        Mensaje("Unicamente archivos .xls");
                    }
                }
                else
                    Mensaje("No hay archivos para cargar.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la lista. " + ex.Message);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload4.HasFile)
                {
                    if (System.IO.Path.GetExtension(FileUpload4.FileName).ToLower() == ".xls")
                    {
                        saveFileOficinasRangos();
                        truncateTable("Proceso.ArchivoOficinasRangos");
                        loadInfoOficinasRangos();
                        deleteFile();
                        Mensaje("Archivo " + nameFile + " cargado correctamente");
                    }
                    else
                    {
                        Mensaje("Unicamente archivos .xls");
                    }
                }
                else
                    Mensaje("No hay archivos para cargar.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la lista. " + ex.Message);
            }
        }

    }
}