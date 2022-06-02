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

namespace ListasSarlaft.UserControls
{
    public partial class ArchivoExcel : System.Web.UI.UserControl
    {
        private cListas cListas = new cListas();

        protected void Page_Load(object sender, EventArgs e)
        {
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
            int Estado = 1, IdTipoLista = 1, resultado = 0, contador = 0;
            StreamReader sr = File.OpenText(Server.MapPath("~/Archivos/Listas/") + nameFile);
            string cadena;

            try
            {
                while ((cadena = sr.ReadLine()) != null)
                {
                    char[] delimiter = { '|' };
                    string[] campos = cadena.ToString().Trim().Split(delimiter);

                    if (campos[13].ToString().Trim() == String.Empty)
                        Estado = 1;
                    else
                        Estado = Convert.ToInt32(campos[13].ToString().Trim());

                    if (campos[4].ToString().Trim() == String.Empty)
                        IdTipoLista = 1;
                    else
                        IdTipoLista = Convert.ToInt32(campos[4].ToString().Trim());

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
                cListas.updateInfoLista(InfoFile);
            }
            catch (Exception ex)
            {
                sr.Close();
                throw new Exception(ex.Message);
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

        #region Properties
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

    }
}