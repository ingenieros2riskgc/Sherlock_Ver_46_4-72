using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls
{
    public partial class UpdateListas : System.Web.UI.UserControl
    {
        string IdFormulario = "46";
        private cListas cListas = new cListas();
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower() == ".txt")
                    {
                        loadDTFile();
                        saveFile();
                        truncateTableLista();
                        loadInfoArchivo();
                        deleteFile();
                        Mensaje("Se cargaron " + cListas.conteoRegistrosLista() + " registros.");
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
            File.Delete(Server.MapPath("~/Archivos/Listas/") + nameFile);
        }

        private void loadInfoArchivo()
        {
            int Estado = 1;
            int IdTipoLista = 1;
            int resultado = 0;
            int contador = 0;
            StreamReader sr = File.OpenText(Server.MapPath("~/Archivos/Listas/") + nameFile);
            try
            {
                String cadena;
                while ((cadena = sr.ReadLine()) != null)
                {
                    char[] delimiter = { '|' };
                    String[] campos = cadena.ToString().Trim().Split(delimiter);
                    if (campos[13].ToString().Trim() == String.Empty)
                    {
                        Estado = 1;
                    }
                    else
                    {
                        Estado = Convert.ToInt32(campos[13].ToString().Trim());
                    }
                    if (campos[4].ToString().Trim() == String.Empty)
                    {
                        IdTipoLista = 1;
                    }
                    else
                    {
                        IdTipoLista = Convert.ToInt32(campos[4].ToString().Trim());
                    }
                    InfoFile.Rows.Add(new Object[] { Convert.ToInt32(campos[0].ToString().Trim()),
                                                     campos[1].ToString().Trim(),
                                                     campos[2].ToString().Trim(),
                                                     remplazarCaracteres(campos[3].ToString().Trim()),
                                                     IdTipoLista,
                                                     remplazarCaracteres(campos[5].ToString().Trim()),
                                                     remplazarCaracteres(campos[6].ToString().Trim()),
                                                     remplazarCaracteres(campos[7].ToString().Trim()),
                                                     remplazarCaracteres(campos[8].ToString().Trim()),
                                                     remplazarCaracteres(campos[9].ToString().Trim()),
                                                     campos[10].ToString().Trim(),
                                                     campos[11].ToString().Trim(),
                                                     remplazarCaracteres(campos[12].ToString().Trim()),
                                                     Estado
                                                   });
                    Math.DivRem(contador, 10000, out resultado);
                    if (resultado == 0)
                    {
                        cListas.updateInfoLista(InfoFile);
                        loadDTFile();
                    }
                    contador++;
                }
                sr.Close();
                cListas.updateInfoLista(InfoFile);
            }
            catch (Exception ex)
            {
                sr.Close();
                throw new Exception("Error al leer el archivo. " + ex.Message);
            }
        }

        private String remplazarCaracteres(String cadena)
        {
            return cadena.Replace("\"", "").Replace("'", "").Replace(",", "").Replace(";", "").Replace(".", "").Replace("á", "A").Replace("é", "E").Replace("í", "I").Replace("ó", "O").Replace("ú", "U").ToUpper();
        }

        private void truncateTableLista()
        {
            cListas.truncateTableLista();
        }

        private void saveFile()
        {
            nameFile = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString().Trim());
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Archivos/Listas/") + nameFile);
        }

        private void loadDTFile()
        {
            DataTable dtFile = new DataTable();
            dtFile.Columns.Add("IdLista", typeof(int));
            dtFile.Columns.Add("TipoDocumento", typeof(string));
            dtFile.Columns.Add("DocumentoIdentidad", typeof(string));
            dtFile.Columns.Add("NombreCompleto", typeof(string));
            dtFile.Columns.Add("IdTipoLista", typeof(int));
            dtFile.Columns.Add("FuenteConsulta", typeof(string));
            dtFile.Columns.Add("TipoPersona", typeof(string));
            dtFile.Columns.Add("Alias", typeof(string));
            dtFile.Columns.Add("Delito", typeof(string));
            dtFile.Columns.Add("Zona", typeof(string));
            dtFile.Columns.Add("Link", typeof(string));
            dtFile.Columns.Add("Imagen", typeof(string));
            dtFile.Columns.Add("OtraInformacion", typeof(string));
            dtFile.Columns.Add("Estado", typeof(int));
            InfoFile = dtFile;
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

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        loadDirectory();               
        //    }
        //}

        //private void loadDirectory()
        //{
        //    Menu1.Items.Clear();
        //    DirectoryInfo dIListas = new DirectoryInfo(Server.MapPath("~/Archivos/Listas/"));
        //    FileInfo[] arrArchivos = dIListas.GetFiles();
        //    foreach (FileInfo file in arrArchivos)
        //    {
        //        MenuItem mnuItem = new MenuItem();
        //        mnuItem.Value = file.Name.ToString().Trim();
        //        mnuItem.Text = file.Name.ToString().Trim();
        //        Menu1.Items.Add(mnuItem);
        //    }
        //}

        //private void Mensaje(String Mensaje)
        //{
        //    this.hdd_error.Value = Mensaje;
        //}

        //protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        //{
        //    SelectedFile = e.Item.Value.ToString().Trim();
        //    Label9.Text = SelectedFile;
        //    Label4.Text = File.GetCreationTime(Server.MapPath("~/Archivos/Listas/") + SelectedFile).ToString().Trim();
        //    Label6.Text = File.GetLastWriteTime(Server.MapPath("~/Archivos/Listas/") + SelectedFile).ToString().Trim();
        //    Label8.Text = File.GetLastAccessTime(Server.MapPath("~/Archivos/Listas/") + SelectedFile).ToString().Trim();
        //    tbFiles.Visible = true;            
        //}

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        leerArchivo();
        //        Mensaje("Información cargada con éxito.");
        //    }
        //    catch(Exception ex)
        //    {
        //        Mensaje("Error al cargar la información. " + ex.Message);
        //    }            
        //}

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        truncateTableLista();
        //        Mensaje("Información cargada con éxito.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("Error al cargar la información. " + ex.Message);
        //    }            
        //}

        //private void truncateTableLista()
        //{
        //    cListas.truncateTableLista();
        //    leerArchivo();       
        //}

        //protected void Button3_Click(object sender, EventArgs e)
        //{
        //    loadDirectory();
        //    resetValues();
        //}

        //private void leerArchivo()
        //{
        //    String sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Archivos/Listas/") + SelectedFile + ";Extended Properties=Excel 12.0 Xml;";
        //    OleDbConnection objConn = new OleDbConnection(sConnectionString);
        //    try
        //    {
        //        objConn.Open();
        //        OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Hoja1$] ", objConn);
        //        OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
        //        objAdapter1.SelectCommand = objCmdSelect;
        //        DataTable dtInfo = new DataTable();
        //        objAdapter1.Fill(dtInfo);
        //        objConn.Close();
        //        armarDTInfo(dtInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        objConn.Close();
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void armarDTInfo(DataTable dtInfo)
        //{
        //    if (dtInfo.Rows.Count > 0)
        //    {
        //        int Estado = 1;
        //        int IdTipoLista = 1;
        //        int resultado = 0;
        //        loadDTFile();
        //        for (int contador = 0; contador < dtInfo.Rows.Count; contador++)
        //        {
        //            if(dtInfo.Rows[contador]["F1"].ToString().Trim() != String.Empty)
        //            {
        //                if (dtInfo.Rows[contador]["F14"].ToString().Trim() == String.Empty)
        //                {
        //                    Estado = 1;
        //                }
        //                else
        //                {
        //                    Estado = Convert.ToInt32(dtInfo.Rows[contador]["F14"]);
        //                }
        //                if (dtInfo.Rows[contador]["F5"].ToString().Trim() == String.Empty)
        //                {
        //                    IdTipoLista = 1;
        //                }
        //                else
        //                {
        //                    IdTipoLista = Convert.ToInt32(dtInfo.Rows[contador]["F5"]);
        //                }
        //                InfoFile.Rows.Add(new Object[] { Convert.ToInt32(dtInfo.Rows[contador]["F1"]),
        //                                             dtInfo.Rows[contador]["F2"].ToString().Trim(),
        //                                             dtInfo.Rows[contador]["F3"].ToString().Trim(),
        //                                             remplazarCaracteres(dtInfo.Rows[contador]["F4"].ToString().Trim()),
        //                                             IdTipoLista,
        //                                             remplazarCaracteres(dtInfo.Rows[contador]["F6"].ToString().Trim()),
        //                                             remplazarCaracteres(dtInfo.Rows[contador]["F7"].ToString().Trim()),
        //                                             remplazarCaracteres(dtInfo.Rows[contador]["F8"].ToString().Trim()),
        //                                             remplazarCaracteres(dtInfo.Rows[contador]["F9"].ToString().Trim()),
        //                                             remplazarCaracteres(dtInfo.Rows[contador]["F10"].ToString().Trim()),
        //                                             dtInfo.Rows[contador]["F11"].ToString().Trim(),                                                     
        //                                             dtInfo.Rows[contador]["F12"].ToString().Trim(),
        //                                             remplazarCaracteres(dtInfo.Rows[contador]["F13"].ToString().Trim()),
        //                                             Estado});

        //            }                    
        //            Math.DivRem(contador, 10000, out resultado);
        //            if (resultado == 0)
        //            {
        //                cListas.updateInfoLista(InfoFile);
        //                loadDTFile();
        //            }   
        //        }                   
        //        cListas.updateInfoLista(InfoFile);
        //        loadDirectory();
        //        resetValues();
        //    }
        //}

        //private void loadDTFile()
        //{
        //    DataTable dtFile = new DataTable();
        //    dtFile.Columns.Add("IdLista", typeof(int));
        //    dtFile.Columns.Add("TipoDocumento", typeof(string));
        //    dtFile.Columns.Add("DocumentoIdentidad", typeof(string));
        //    dtFile.Columns.Add("NombreCompleto", typeof(string));
        //    dtFile.Columns.Add("IdTipoLista", typeof(int));
        //    dtFile.Columns.Add("FuenteConsulta", typeof(string));
        //    dtFile.Columns.Add("TipoPersona", typeof(string));
        //    dtFile.Columns.Add("Alias", typeof(string));
        //    dtFile.Columns.Add("Delito", typeof(string));
        //    dtFile.Columns.Add("Zona", typeof(string));
        //    dtFile.Columns.Add("Link", typeof(string));
        //    dtFile.Columns.Add("Imagen", typeof(string));
        //    dtFile.Columns.Add("OtraInformacion", typeof(string));
        //    dtFile.Columns.Add("Estado", typeof(int));
        //    InfoFile = dtFile;
        //}

        //private String remplazarCaracteres(String cadena)
        //{
        //    return cadena.Replace("\"", "").Replace("'", "").Replace(",", "").Replace(";", "").Replace(".","").Replace("á", "A").Replace("é", "E").Replace("í", "I").Replace("ó", "O").Replace("ú", "U").ToUpper();
        //}

        //private void resetValues()
        //{
        //    Label4.Text = "";
        //    Label6.Text = "";
        //    Label8.Text = "";
        //    tbFiles.Visible = false;
        //}

        //#region Propierties
        //private String selectedFile;
        //private String SelectedFile
        //{
        //    get
        //    {
        //        selectedFile = (String)ViewState["selectedFile"];
        //        return selectedFile;
        //    }
        //    set
        //    {
        //        selectedFile = value;
        //        ViewState["selectedFile"] = selectedFile;
        //    }
        //}

        //private DataTable infoFile;
        //private DataTable InfoFile
        //{
        //    get
        //    {
        //        infoFile = (DataTable)Session["infoFile"];
        //        return infoFile;
        //    }
        //    set
        //    {
        //        infoFile = value;
        //        Session["infoFile"] = infoFile;
        //    }
        //}
        //#endregion

    }
}