using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.Sarlaft
{
    public partial class AnalisisClientes : System.Web.UI.UserControl
    {
        private cListas cListas = new cListas();
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "41";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                resetValues();
                loadGridAnalisisClientes();
                if (FileUpload1.HasFile)
                {
                    if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower() == ".xls")
                    {
                        saveFile();
                        loadInfoArchivo();
                        analisisClientes();
                        loadInfoAnalisisClientes();
                        File.Delete(Server.MapPath("~/Archivos/AnalisisCliente/") + nameFile);
                        Mensaje("Finalizo análisis.");
                    }
                    else
                        Mensaje("Unicamente archivos .xls");
                }
                else
                    Mensaje("No hay archivos para cargar.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al analizar la información. " + ex.Message);
            }
            finally
            {
                truncateTable();
            }
        }

        private void truncateTable()
        {
            cListas.truncateTables();
        }

        private void analisisClientes()
        {
            cListas.analisisClientes();
        }

        private string nombreListas(string nombre, string documento)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cListas.ConsultaListas(nombre, documento);
            string cadena = "";
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    cadena += dtInfo.Rows[rows]["NombreClaseLista"].ToString().Trim() + ". ";
                }
            }
            return cadena;
        }

        private void loadInfoAnalisisClientes()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cListas.loadInfoAnalisisClientes();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAnalisisClientes.Rows.Add(new Object[] {dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Identificacion"].ToString().Trim(),
                                                                    nombreListas(dtInfo.Rows[rows]["Nombre"].ToString().Trim(), dtInfo.Rows[rows]["Identificacion"].ToString().Trim())
                                                                   });
                }
                GridView1.DataSource = InfoGridAnalisisClientes;
                GridView1.DataBind();
                Button2.Visible = true;
            }
            else
            {
                Button2.Visible = false;
                Mensaje("No se encontraron registros asociados a las listas restrictivas.");
            }
        }

        private void loadGridAnalisisClientes()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Identificacion", typeof(string));
            grid.Columns.Add("NombreClaseLista", typeof(string));
            InfoGridAnalisisClientes = grid;
            GridView1.DataSource = InfoGridAnalisisClientes;
            GridView1.DataBind();
        }

        private void loadInfoArchivo()
        {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Archivos/AnalisisCliente/") + nameFile + ";Extended Properties=Excel 8.0;";
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
                cListas.loadClientes(dtInfo);
            }
            catch (Exception ex)
            {
                objConn.Close();
                throw new Exception(ex.Message);
            }
        }

        private void saveFile()
        {
            nameFile = FileUpload1.FileName.ToString().Trim();
            FileUpload1.SaveAs(Server.MapPath("~/Archivos/AnalisisCliente/") + nameFile);
        }

        private void resetValues()
        {
            Button2.Visible = false;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties

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

        private DataTable infoGridAnalisisClientes;
        private DataTable InfoGridAnalisisClientes
        {
            get
            {
                infoGridAnalisisClientes = (DataTable)ViewState["infoGridAnalisisClientes"];
                return infoGridAnalisisClientes;
            }
            set
            {
                infoGridAnalisisClientes = value;
                ViewState["infoGridAnalisisClientes"] = infoGridAnalisisClientes;
            }
        }

        #endregion

        protected void Button2_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridAnalisisClientes, Response, "Conteo Registros");
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
            dg.DataSource = dt;
            dg.DataBind();
            dg.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            resetValues();
            loadGridAnalisisClientes();
        }
    }
}