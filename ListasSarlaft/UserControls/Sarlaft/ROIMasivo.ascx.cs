using ClosedXML.Excel;
using Excel;
using ListasSarlaft.Classes;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ListasSarlaft.UserControls.Sarlaft
{
    public partial class ROIMasivo : System.Web.UI.UserControl
    {
        private cError cError = new cError();
        private int pagIndex;
        private int PagIndex
        {
            get
            {
                pagIndex = (int)ViewState["pagIndex"];
                return pagIndex;
            }
            set
            {
                pagIndex = value;
                ViewState["pagIndex"] = pagIndex;
            }
        }
        #region Properties
        private DataTable infoGridComentarioPlanEvaluacion;
        private DataTable InfoGridComentarioPlanEvaluacion
        {
            get
            {
                infoGridComentarioPlanEvaluacion = (DataTable)ViewState["infoGridComentarioPlanEvaluacion"];
                return infoGridComentarioPlanEvaluacion;
            }
            set
            {
                infoGridComentarioPlanEvaluacion = value;
                ViewState["infoGridComentarioPlanEvaluacion"] = infoGridComentarioPlanEvaluacion;
            }
        }

        private int rowGridComentarioPlanEvaluacion;
        private int RowGridComentarioPlanEvaluacion
        {
            get
            {
                rowGridComentarioPlanEvaluacion = (int)ViewState["rowGridComentarioPlanEvaluacion"];
                return rowGridComentarioPlanEvaluacion;
            }
            set
            {
                rowGridComentarioPlanEvaluacion = value;
                ViewState["rowGridComentarioPlanEvaluacion"] = rowGridComentarioPlanEvaluacion;
            }
        }

        private int rowGridArchivoPlanEvaluacion;
        private int RowGridArchivoPlanEvaluacion
        {
            get
            {
                rowGridArchivoPlanEvaluacion = (int)ViewState["rowGridArchivoPlanEvaluacion"];
                return rowGridArchivoPlanEvaluacion;
            }
            set
            {
                rowGridArchivoPlanEvaluacion = value;
                ViewState["rowGridArchivoPlanEvaluacion"] = rowGridArchivoPlanEvaluacion;
            }
        }

        private DataTable infoGridArchivoPlanEvaluacion;
        private DataTable InfoGridArchivoPlanEvaluacion
        {
            get
            {
                infoGridArchivoPlanEvaluacion = (DataTable)ViewState["infoGridArchivoPlanEvaluacion"];
                return infoGridArchivoPlanEvaluacion;
            }
            set
            {
                infoGridArchivoPlanEvaluacion = value;
                ViewState["infoGridArchivoPlanEvaluacion"] = infoGridArchivoPlanEvaluacion;
            }
        }

        private int rowGridPlanEvaluacion;
        private int RowGridPlanEvaluacion
        {
            get
            {
                rowGridPlanEvaluacion = (int)ViewState["rowGridPlanEvaluacion"];
                return rowGridPlanEvaluacion;
            }
            set
            {
                rowGridPlanEvaluacion = value;
                ViewState["rowGridPlanEvaluacion"] = rowGridPlanEvaluacion;
            }
        }

        private DataTable infoGridPlanEvaluacion;
        private DataTable InfoGridPlanEvaluacion
        {
            get
            {
                infoGridPlanEvaluacion = (DataTable)ViewState["infoGridPlanEvaluacion"];
                return infoGridPlanEvaluacion;
            }
            set
            {
                infoGridPlanEvaluacion = value;
                ViewState["infoGridPlanEvaluacion"] = infoGridPlanEvaluacion;
            }
        }

        private DataTable infoGridComentarioControl;
        private DataTable InfoGridComentarioControl
        {
            get
            {
                infoGridComentarioControl = (DataTable)ViewState["infoGridComentarioControl"];
                return infoGridComentarioControl;
            }
            set
            {
                infoGridComentarioControl = value;
                ViewState["infoGridComentarioControl"] = infoGridComentarioControl;
            }
        }

        private DataTable infoPorcentajeCalificarControl;
        private DataTable InfoPorcentajeCalificarControl
        {
            get
            {
                infoPorcentajeCalificarControl = (DataTable)ViewState["infoPorcentajeCalificarControl"];
                return infoPorcentajeCalificarControl;
            }
            set
            {
                infoPorcentajeCalificarControl = value;
                ViewState["infoPorcentajeCalificarControl"] = infoPorcentajeCalificarControl;
            }
        }

        private DataTable infoCalificacionControl;
        private DataTable InfoCalificacionControl
        {
            get
            {
                infoCalificacionControl = (DataTable)ViewState["infoCalificacionControl"];
                return infoCalificacionControl;
            }
            set
            {
                infoCalificacionControl = value;
                ViewState["infoCalificacionControl"] = infoCalificacionControl;
            }
        }

        private DataTable infoGridControles;
        private DataTable InfoGridControles
        {
            get
            {
                infoGridControles = (DataTable)ViewState["infoGridControles"];
                return infoGridControles;
            }
            set
            {
                infoGridControles = value;
                ViewState["infoGridControles"] = infoGridControles;
            }
        }

        private int rowGridControles;
        private int RowGridControles
        {
            get
            {
                rowGridControles = (int)ViewState["rowGridControles"];
                return rowGridControles;
            }
            set
            {
                rowGridControles = value;
                ViewState["rowGridControles"] = rowGridControles;
            }
        }

        private String idCalificacionControl;
        private String IdCalificacionControl
        {
            get
            {
                idCalificacionControl = (String)ViewState["idCalificacionControl"];
                return idCalificacionControl;
            }
            set
            {
                idCalificacionControl = value;
                ViewState["idCalificacionControl"] = idCalificacionControl;
            }
        }

        private DataTable infoGridArchivoControl;
        private DataTable InfoGridArchivoControl
        {
            get
            {
                infoGridArchivoControl = (DataTable)ViewState["infoGridArchivoControl"];
                return infoGridArchivoControl;
            }
            set
            {
                infoGridArchivoControl = value;
                ViewState["infoGridArchivoControl"] = infoGridArchivoControl;
            }
        }

        private int rowGridArchivoControl;
        private int RowGridArchivoControl
        {
            get
            {
                rowGridArchivoControl = (int)ViewState["rowGridArchivoControl"];
                return rowGridArchivoControl;
            }
            set
            {
                rowGridArchivoControl = value;
                ViewState["rowGridArchivoControl"] = rowGridArchivoControl;
            }
        }

        private int rowGridComentarioControl;
        private int RowGridComentarioControl
        {
            get
            {
                rowGridComentarioControl = (int)ViewState["rowGridComentarioControl"];
                return rowGridComentarioControl;
            }
            set
            {
                rowGridComentarioControl = value;
                ViewState["rowGridComentarioControl"] = rowGridComentarioControl;
            }
        }

        private DataTable infoIntervalos;
        private DataTable InfoIntervalos
        {
            get
            {
                infoIntervalos = (DataTable)ViewState["infoIntervalos"];
                return infoIntervalos;
            }
            set
            {
                infoIntervalos = value;
                ViewState["infoIntervalos"] = infoIntervalos;
            }
        }

        private int pagIndexInfoGridControles;
        private int PagIndexInfoGridControles
        {
            get
            {
                pagIndexInfoGridControles = (int)ViewState["pagIndexInfoGridControles"];
                return pagIndexInfoGridControles;
            }
            set
            {
                pagIndexInfoGridControles = value;
                ViewState["pagIndexInfoGridControles"] = pagIndexInfoGridControles;
            }
        }

        private DataTable infoGridROIS;
        private DataTable InfoGridROIS
        {
            get
            {
                infoGridROIS = (DataTable)ViewState["infoGridROIS"];
                return infoGridROIS;
            }
            set
            {
                infoGridROIS = value;
                ViewState["infoGridROIS"] = infoGridROIS;
            }
        }

        private int pagIndexInfoGridRegistroOperacion;
        private int PagIndexInfoGridRegistroOperacion
        {
            get
            {
                pagIndexInfoGridRegistroOperacion = (int)ViewState["pagIndexInfoGridRegistroOperacion"];
                return pagIndexInfoGridRegistroOperacion;
            }
            set
            {
                pagIndexInfoGridRegistroOperacion = value;
                ViewState["pagIndexInfoGridRegistroOperacion"] = pagIndexInfoGridRegistroOperacion;
            }
        }

        #endregion
        string IdFormulario = "6006";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportDataROIs);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportPlantillaROIs);
            scriptManager.RegisterPostBackControl(this.Bload);
            TrButtonsExportROIs.Visible = true;
            TrLoadFile.Visible = true;
        }
        protected void ImButtonExcelExportDataROIs_Click(object sender, ImageClickEventArgs e)
        {
            exportExcelROIs(Response, "DatosParametricosROIs_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcelROIs(HttpResponse Response, string filename)
        {

            cCargaMasivaROI carga = new cCargaMasivaROI();
            DataTable DataTipoId = carga.DataTipoId();
            DataTipoId.TableName = "Tipo Id 1-3";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(DataTipoId);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(ds);
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
        public DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();
            if (dtg.HeaderRow != null)
            {
                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.RowHeaderColumn[i].ToString());
                }
            }

            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        protected void Bload_Click(object sender, EventArgs e)
        {
            try
            {
                string NombreArchivo = string.Empty;
                if (FUloadExcel.HasFile)
                {
                    if (System.IO.Path.GetExtension(FUloadExcel.FileName).ToLower() == ".xls" || System.IO.Path.GetExtension(FUloadExcel.FileName).ToLower() == ".xlsx")
                    {
                        cCargaMasivaROI CargaMasiva = new cCargaMasivaROI();
                        DataTable dt = CargaMasiva.NumeroActual();
                        string idROIs = dt.Rows[0][0].ToString();
                        NombreArchivo = FUloadExcel.FileName;
                        LoadFilePLantillaROIs();
                        LoadGridROIs();
                        loadInfoROIs(idROIs);
                        omb.ShowMessage("Archivo procesado con éxito", 3, "Atención");
                    }
                    else
                    {
                        Mensaje("Por favor usar archivos .xls o .xlsx");
                    }
                }
                else
                {
                    Mensaje("El control no contiene ningún archivo.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al analizar la información. " + ex.Message);
            }
        }
        private void LoadGridROIs()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("TipoId", typeof(string));
            grid.Columns.Add("Id", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Mensaje", typeof(string));
            grid.Columns.Add("FechaDeteccion", typeof(string));
            infoGridROIS = grid;
            GVROIs.DataSource = infoGridROIS;
            GVROIs.DataBind();
        }
        private void loadInfoROIs(string idROIs)
        {
            cCargaMasivaROI CargaMasiva = new cCargaMasivaROI();
            DataTable dtInfo = new DataTable();
            dtInfo = CargaMasiva.loadInfoROI(idROIs);

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    infoGridROIS.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdTipoIden"].ToString().Trim(),
                        dtInfo.Rows[rows]["Identificacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["Indicador"].ToString().Trim(),
                        dtInfo.Rows[rows]["MensajeCorreo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaDeteccion"].ToString().Trim(),
                    });
                }

                GVROIs.PageIndex = pagIndex;
                GVROIs.DataBind();
                GVROIs.DataSource = dtInfo;
                TgridROIs.Visible = true;
            }
        }
        private void LoadFilePLantillaROIs()
        {
            cCargaMasivaROI CargaMasiva = new cCargaMasivaROI();
            IExcelDataReader excelReader = null;
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(FUloadExcel.PostedFile.InputStream);
                excelReader.IsFirstRowAsColumnNames = true;
            }
            catch (Exception ex)
            {
                Mensaje("Error al analizar el archivo Eventos. " + ex.Message);
            }

            if (excelReader != null)
            {
                ds = excelReader.AsDataSet();
                DataTable IdTipo = CargaMasiva.IdRm();
                string Tipo = IdTipo.Rows[0][0].ToString();
                DataTable datos = ds.Tables[0];
                for (int i = 0; i < datos.Rows.Count; i++)
                {
                    if (datos.Rows[i][0].ToString().Trim() != "")
                    {
                        string TipoId = datos.Rows[i][0].ToString().Trim();
                        string Id = datos.Rows[i][1].ToString().Trim();
                        string NombreApellido = datos.Rows[i][2].ToString().Trim();
                        string Descripcion = datos.Rows[i][3].ToString().Trim();
                        string Mensaje = datos.Rows[i][4].ToString().Trim();
                        DateTime FechaDeteccion = DateTime.FromOADate(Convert.ToDouble(datos.Rows[i][5]));
                        string Fecha = FechaDeteccion.ToString("yyyy-MM-dd");
                        try
                        {
                            DataTable IdRM = CargaMasiva.IdRm();
                            string RM = IdRM.Rows[0][0].ToString();
                            CargaMasiva.registarROI(Tipo, TipoId, Id, NombreApellido, Descripcion, Mensaje, Fecha, RM);
                        }
                        catch (Exception ex)
                        {
                            Mensaje = ("Error al registrar el control de la linea: " + i + "." + ex.Message);
                        }
                    }
                }
                excelReader.Close();
            }
        }
        private void armarInformacionROIs(DataTable dt)
        {
            int intCantidadROIs = dt.Rows.Count;
            DataTable dtInfoROIsIns = new DataTable();
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string valor = dt.Rows[i][0].ToString().Trim();
            }
        }
        private void Mensaje(String Mensaje)
        {
            lblMsgBox1.Text = Mensaje;
            mpeMsgBox1.Show();
        }
        protected void ImButtonExcelExportPlantillaROIs_Click(object sender, ImageClickEventArgs e)
        {
            cCargaMasivaROI carga = new cCargaMasivaROI();
            byte[] excel = carga.mtdDescargarPlantillaROIs();
            if (excel != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=RiesgosPlantillaCargaMasiva.xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(excel);
                Response.End();
            }
        }
        protected void GVROIs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndexInfoGridRegistroOperacion = (e.NewPageIndex);
            GVROIs.PageIndex = pagIndexInfoGridRegistroOperacion;
            GVROIs.DataSource = infoGridROIS;
            GVROIs.DataBind();
        }
        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            TrButtonsExportROIs.Visible = true;
            TrLoadFile.Visible = true;
            TgridROIs.Visible = false;
        }

        protected void GVROIs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}