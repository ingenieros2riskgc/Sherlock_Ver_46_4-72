using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel;
using ICSharpCode;
using System.Data;
using ListasSarlaft.Classes;
using ClosedXML.Excel;
using System.IO;
using System.Text;
using System.Data.OleDb;
using System.Globalization;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class CargaMasiva : System.Web.UI.UserControl
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
        #endregion

        cControl cControl = new cControl();
        
        cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        string IdFormulario = "5021";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportDataRiesgo);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportPlantillaRiesgo);
            if (!Page.IsPostBack)
            {
                Session["IdControl"] = cControl.SeleccionarUltimoControl();
            }
            scriptManager.RegisterPostBackControl(this.Bload);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
        }

        protected void DDLopciones_SelectedIndexChanged(object sender, EventArgs e)
        {

            TrButtonsExportRiesgos.Visible = true;
            TrLoadFile.Visible = true;
        }

        protected void ImButtonExcelExportDataRiesgo_Click(object sender, ImageClickEventArgs e)
        {
            if (DDLopciones.SelectedValue == "1")
                exportExcelRiesgo(Response, "DatosParametricosRiesgos_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
            if (DDLopciones.SelectedValue == "2")
                exportExcelControles(Response, "DatosParametricosControles_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
            if (DDLopciones.SelectedValue == "3")
                exportExcelEventos(Response, "DatosParametricosEventos_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
            if (DDLopciones.SelectedValue == "4")
                exportExcelRiesgosvsControles(Response, "DatosParametricosRiesgosvsControles_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        private void armarIntervalos()
        {
            DataTable dtInfo = new DataTable(), dtIntervalos = new DataTable();
            double maximo = 0, minimo = 0, intervalo = 0, delta = 0;

            try
            {
                dtInfo = cControl.valorMaxMinIntervalo();

                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        minimo = Convert.ToDouble(dtInfo.Rows[0]["Minimo"].ToString().Trim());
                        intervalo = (Convert.ToDouble(dtInfo.Rows[0]["Maximo"].ToString().Trim())) - (Convert.ToDouble(dtInfo.Rows[0]["Minimo"].ToString().Trim()));
                        delta = intervalo / 4;

                        dtIntervalos.Columns.Add("limiteInferior", typeof(string));
                        dtIntervalos.Columns.Add("limiteSuperior", typeof(string));
                        dtIntervalos.Columns.Add("IdCalificacionControl", typeof(string));
                        for (int rows = 4; rows > 0; rows--)
                        {
                            maximo = minimo + delta;
                            dtIntervalos.Rows.Add(new Object[] { minimo.ToString(), maximo.ToString(), rows.ToString() });
                            minimo = maximo;
                        }
                        dtIntervalos.Rows[0]["limiteInferior"] = "0";
                        dtIntervalos.Rows[3]["limiteSuperior"] = "100";
                        InfoIntervalos = dtIntervalos;
                    }
                    else
                        Mensaje("No hay información en las tablas maestras de parametrización. ");
                }
                else
                    Mensaje("No hay información en las tablas maestras de parametrización. ");
            }
            catch (Exception ex)
            {
                Mensaje("Error al armar intervalos. " + ex.Message);
            }
        }
        private void loadInfoCalificacionControl()
        {
            InfoCalificacionControl = cControl.loadInfoCalificacionControl();
        }
        private void loadInfoPorcentajeCalificarControl()
        {
            InfoPorcentajeCalificarControl = cControl.loadInfoPorcentajeCalificarControl();
        }
        private string calcularCalificacionControl(string ClaseControl, string TipoControl, string experiencia, string document, string respons)
        {
            double calificacionControl = 0;
            string calificacion = string.Empty;
            double claseControl = 0;
            if (ClaseControl != "0")
                claseControl = cControl.valorClaseControl(ClaseControl);

            double tipoControl = 0;
            if (TipoControl != "0")
                tipoControl = cControl.valorTipoControl(TipoControl);

            double responsableExperiencia = 0;
            if (experiencia != "0")
                responsableExperiencia = cControl.valorResponsableExperiencia(experiencia);

            double documentacion = 0;
            if (document != "0")
                documentacion = cControl.valorDocumentacion(document);

            double responsabilidad = 0;
            if (respons != "0")
                responsabilidad = cControl.valorResponsabilidad(respons);

            loadInfoPorcentajeCalificarControl();
            armarIntervalos();
            loadInfoCalificacionControl();

            calificacionControl = claseControl * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[0]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl += tipoControl * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[1]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl += responsableExperiencia * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[2]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl += documentacion * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[3]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl += responsabilidad * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[4]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl = (calificacionControl / 100);
            for (int i = 0; i < InfoIntervalos.Rows.Count; i++)
            {
                if (calificacionControl > Convert.ToDouble(InfoIntervalos.Rows[i]["limiteInferior"].ToString().Trim()) && calificacionControl <= Convert.ToDouble(InfoIntervalos.Rows[i]["limiteSuperior"].ToString().Trim()))
                {
                    IdCalificacionControl = InfoIntervalos.Rows[i]["IdCalificacionControl"].ToString().Trim();
                    for (int j = 0; j < InfoCalificacionControl.Rows.Count; j++)
                    {
                        if (IdCalificacionControl == InfoCalificacionControl.Rows[j]["IdCalificacionControl"].ToString().Trim())
                        {
                            calificacion = IdCalificacionControl;
                            //Panel1.BackColor = System.Drawing.Color.FromName(InfoCalificacionControl.Rows[j]["Color"].ToString().Trim());
                            break;
                        }
                    }
                    break;
                }
            }
            return calificacion;
        }
        protected void exportExcelRiesgo(HttpResponse Response, string filename)
        {

            cCargaMasivaRCE carga = new cCargaMasivaRCE();
            DataTable DataUbicacion = carga.DataUbicacionRiesgo();
            DataUbicacion.TableName = "Ubicacion 1-14";
            DataTable DataProcesos = carga.DataProcesosRiesgo();
            DataProcesos.TableName = "Procesos 2-14";
            DataTable DataClasificacion = carga.DataClasificacionRiesgo();
            DataClasificacion.TableName = "Clasificacion Riesgos 3-14";
            DataTable DataTipoEvento = carga.DataTipoEvento();
            DataTipoEvento.TableName = "Tipo Evento 4-14";
            DataTable DataRiesgoOperativo = carga.DataRiesgoOperativo();
            DataRiesgoOperativo.TableName = "Riesgo Operativo 5-14";
            DataTable DataRiesgoAsociado = carga.DataRiesgoAsociadoOperativo();
            DataRiesgoAsociado.TableName = "Riesgo Asociado 6-14";
            DataTable DataRiesgoLA = carga.DataRiesgoAsociadoLA();
            DataRiesgoLA.TableName = "Riesgo LA 7-14";
            DataTable DataRiesgoLAFT = carga.DataRiesgoAsociadoLAFT();
            DataRiesgoLAFT.TableName = "Riesgo LAFT 8-14";
            DataTable DataCausas = carga.DataCausas();
            DataCausas.TableName = "Causas 9-14";
            DataTable DataConsecuencias = carga.DataConsecuencia();
            DataConsecuencias.TableName = "Consecuencias 10-14";
            DataTable DataFrecuencia = carga.DataFrecuencia();
            DataFrecuencia.TableName = "Frecuencia-Cualitativa 11-14";
            DataTable DataTratamiento = carga.DataTratamiento();
            DataTratamiento.TableName = "Tratamiento 12-14";
            DataTable DataResponsable = carga.DataResponsable();
            DataResponsable.TableName = "Responsable 13-14";
            DataTable DataImpacto = carga.DataImpacto();
            DataImpacto.TableName = "Impacto 14-14";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(DataUbicacion);
            ds.Tables.Add(DataProcesos);
            ds.Tables.Add(DataClasificacion);
            ds.Tables.Add(DataTipoEvento);
            ds.Tables.Add(DataRiesgoOperativo);
            ds.Tables.Add(DataRiesgoAsociado);
            ds.Tables.Add(DataRiesgoLA);
            ds.Tables.Add(DataRiesgoLAFT);
            ds.Tables.Add(DataCausas);
            ds.Tables.Add(DataConsecuencias);
            ds.Tables.Add(DataFrecuencia);
            ds.Tables.Add(DataTratamiento);
            ds.Tables.Add(DataResponsable);
            ds.Tables.Add(DataImpacto);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(ds);
            // Prepare the response
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
        protected void exportExcelEventos(HttpResponse Response, string filename)
        {

            cCargaMasivaRCE carga = new cCargaMasivaRCE();
            DataTable DataEmpresa = carga.DataEmpresa();
            DataEmpresa.TableName = "Empresa 1-6";
            DataTable DataUbicacion = carga.DataUbicacionRiesgo();
            DataUbicacion.TableName = "Ubicacion 2-6";
            DataTable DataServicio = carga.DataServicio();
            DataServicio.TableName = "Servicios 3-6";
            DataTable DataCanal = carga.DataCanal();
            DataCanal.TableName = "Canal 4-6";
            DataTable DataGenerador = carga.DataGenerador();
            DataGenerador.TableName = "Generador 5-6";
            DataTable DataResponsable = carga.DataResponsable();
            DataResponsable.TableName = "Responsable 6-6";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(DataEmpresa);
            ds.Tables.Add(DataUbicacion);
            ds.Tables.Add(DataServicio);
            ds.Tables.Add(DataCanal);
            ds.Tables.Add(DataGenerador);
            ds.Tables.Add(DataResponsable);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(ds);
            // Prepare the response
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
        protected void exportExcelControles(HttpResponse Response, string filename)
        {

            cCargaMasivaRCE carga = new cCargaMasivaRCE();
            DataTable DataResponsable = carga.DataResponsable();
            DataResponsable.TableName = "Responsable 1-6";
            DataTable DataPeriodicidad = carga.DataPeriodicidad();
            DataPeriodicidad.TableName = "Periodicidad 2-6";
            DataTable DataTest = carga.DataTest();
            DataTest.TableName = "Test 3-6";
            DataTable DataReduce = carga.DataMitigaControl();
            DataReduce.TableName = "Reduce 4-6";
            //DataTable DataClaseControl = carga.DataClaseControl();
            //DataClaseControl.TableName = "Clase Control 5-11";
            //DataTable DataTipoControl = carga.DataTipoControl();
            //DataTipoControl.TableName = "Tipo Control 6-11";
            //DataTable DataExperiencia = carga.DataExperiencia();
            //DataExperiencia.TableName = "Responsable Experiencia 7-11";
            //DataTable DataDocumentacion = carga.DataDocumentacion();
            //DataDocumentacion.TableName = "Documentacion 8-11";
            //DataTable DataResponsabilidad = carga.DataResponsabilidad();
            //DataResponsabilidad.TableName = "Resposabilidad 9-11";
            DataTable DataGrupoTrabajo = carga.DataGruposTrabajo();
            DataGrupoTrabajo.TableName = "Grupos de Trabajo 5-6";
            //DataTable DataVariablesCalificacion = carga.DataVariablesCalificacionesControles();
            //DataVariablesCalificacion.TableName = "Variables Calificación 6-7";
            DataTable DataParametrosVariable = carga.SeleccionarParametrosVariable();
            DataParametrosVariable.TableName = "Parametros Variable 6-6";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(DataResponsable);
            ds.Tables.Add(DataPeriodicidad);
            ds.Tables.Add(DataTest);
            ds.Tables.Add(DataReduce);
            //ds.Tables.Add(DataClaseControl);
            //ds.Tables.Add(DataTipoControl);
            //ds.Tables.Add(DataExperiencia);
            //ds.Tables.Add(DataDocumentacion);
            //ds.Tables.Add(DataResponsabilidad);
            ds.Tables.Add(DataGrupoTrabajo);
            //ds.Tables.Add(DataVariablesCalificacion);
            ds.Tables.Add(DataParametrosVariable);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(ds);
            // Prepare the response
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
        protected void exportExcelRiesgosvsControles(HttpResponse Response, string filename)
        {
            cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
            cCargaMasivaRCE carga = new cCargaMasivaRCE();
            DataTable DataRiesgos = carga.DataRiesgo();
            DataRiesgos.TableName = "Riesgos 1-3";
            DataTable DataControles = carga.DataControles();
            DataControles.TableName = "Controles 2-3";
            //DataTable DataCausasRiegos = carga.DataCausasRiesgos();
            DataTable DataCausasRiegos = cParametrizacionRiesgos.loadInfoCausas();
            /*ListCausas = ListCausas.Replace("|", ",");*/
            DataCausasRiegos.TableName = "Causas del Riesgo 3-3";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(DataRiesgos);
            ds.Tables.Add(DataControles);
            ds.Tables.Add(DataCausasRiegos);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(ds);
            // Prepare the response
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
                        NombreArchivo = FUloadExcel.FileName;
                        if (DDLopciones.SelectedValue == "1")
                        {
                            LoadFilePLantillaRiesgos();
                            LoadGridRiesgos();
                        }
                        if (DDLopciones.SelectedValue == "2")
                        {
                            LoadPlantilla(DDLopciones.SelectedValue);
                            LoadGridControl();
                            omb.ShowMessage("Archivo procesado con éxito", 3, "Atención");
                        }
                        if (DDLopciones.SelectedValue == "3")
                        {
                            LoadFilePlantillaEventos();
                            LoadGridEventos();
                        }
                        if (DDLopciones.SelectedValue == "4")
                        {
                            LoadPlantilla(DDLopciones.SelectedValue);
                            LoadGridRiesgovsControl();
                            omb.ShowMessage("Archivo procesado con éxito", 3, "Atención");
                        }
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
        private void LoadGridRiesgos()
        {
            string IdRiesgo = Session["IdRiesgo"].ToString();
            cCargaMasivaRCE Carga = new cCargaMasivaRCE();
            DataTable dtRiesgo = Carga.GridRiesgos(IdRiesgo);
            GVriesgos.DataSource = dtRiesgo;
            GVriesgos.PageIndex = pagIndex;
            GVriesgos.DataBind();
            TgridRiesgos.Visible = true;
        }
        private void LoadGridControl()
        {
            string IdControl = Session["IdControl"].ToString();
            cCargaMasivaRCE Carga = new cCargaMasivaRCE();
            DataTable dtControl = Carga.GridControles(IdControl);
            GVcontrol.DataSource = dtControl;
            GVcontrol.PageIndex = pagIndex;
            GVcontrol.DataBind();
            TgridRiesgos.Visible = true;
        }
        private void LoadGridEventos()
        {
            string IdEvento = Session["IdEvento"].ToString();
            cCargaMasivaRCE Carga = new cCargaMasivaRCE();
            DataTable dtEventos = Carga.GridEventos(IdEvento);
            GVeventos.DataSource = dtEventos;
            GVeventos.PageIndex = pagIndex;
            GVeventos.DataBind();
            TgridRiesgos.Visible = true;
        }
        private void LoadGridRiesgovsControl()
        {
            string IdControlesRiesgo = Session["IdControlesRiesgo"].ToString();
            cCargaMasivaRCE Carga = new cCargaMasivaRCE();
            DataTable dtControlesRiesgo = Carga.GridRiesgosvsControl(IdControlesRiesgo);
            GVriesgovscontrol.DataSource = dtControlesRiesgo;
            GVriesgovscontrol.PageIndex = pagIndex;
            GVriesgovscontrol.DataBind();
            TgridRiesgos.Visible = true;
        }
        private void LoadFilePlantillaControl()
        {
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            string FechaRegistro = DateTime.Now.ToString();
            //Best Way To read file direct from stream
            cCargaMasivaRCE CargaMasiva = new cCargaMasivaRCE();
            IExcelDataReader excelReader = null;
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {

                //file.InputStream is the file stream stored in memeory by any ways like by upload file control or from database
                int excelFlag = 1; //this flag us used for execl file format .xls or .xlsx
                if (excelFlag == 1)
                {
                    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(FUloadExcel.PostedFile.InputStream);
                    excelReader.IsFirstRowAsColumnNames = true;
                }
                else if (excelFlag == 2)
                {
                    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(FUloadExcel.PostedFile.InputStream);
                    //excelReader.IsFirstRowAsColumnNames = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al analizar el archivo Eventos. " + ex.Message);
            }
            if (excelReader != null)
            {
                //...
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                ds = excelReader.AsDataSet();
                //...
                ////4. DataSet - Create column names from first row
                //excelReader.IsFirstRowAsColumnNames = true;
                //DataSet result = excelReader.AsDataSet();

                ////5. Data Reader methods
                //while (excelReader.Read())
                //{
                //    //excelReader.GetInt32(0);
                //}
                DataTable datos = ds.Tables[0];
                DataTable dt = CargaMasiva.LastCodControl();
                Session["IdControl"] = dt.Rows[0][0].ToString();
                for (int i = 1; i < datos.Rows.Count; i++)
                {
                    if (datos.Rows[i][0].ToString().Trim() != "")
                    {
                        string nombre = datos.Rows[i][0].ToString().Trim();
                        string descripcion = datos.Rows[i][1].ToString().Trim();
                        string objetivo = datos.Rows[i][2].ToString().Trim();
                        string responsable = datos.Rows[i][3].ToString().Trim();
                        string periodicidad = datos.Rows[i][4].ToString().Trim();
                        string test = datos.Rows[i][5].ToString().Trim();
                        string Reduce = datos.Rows[i][6].ToString().Trim();
                        /*string ClaseControl = datos.Rows[i][7].ToString().Trim();
                        string TipoControl = datos.Rows[i][8].ToString().Trim();
                        string Experiencia = datos.Rows[i][9].ToString().Trim();
                        string Documentacion = datos.Rows[i][10].ToString().Trim();
                        string Responsabilidad = datos.Rows[i][11].ToString().Trim();*/
                        //string calificacion = calcularCalificacionControl(ClaseControl,TipoControl,Experiencia,Documentacion,Responsabilidad);
                        string NombreVariable = datos.Rows[i][7].ToString().Trim();
                        string IdCategoria = datos.Rows[i][8].ToString().Trim();
                        string NombreCategoria = datos.Rows[i][9].ToString().Trim();
                        string ResponsableEjecucion = datos.Rows[i][10].ToString().Trim();
                        try
                        {
                            CargaMasiva.registrarControl(nombre, descripcion, objetivo, responsable, periodicidad, test, "0", NombreVariable, IdCategoria, NombreCategoria, Reduce, IdUsuario, ResponsableEjecucion);
                        }
                        catch (Exception ex)
                        {
                            Mensaje("Error al registrar el control de la linea: " + i + "." + ex.Message);
                        }
                    }
                }
                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();
            }
        }
        private void LoadFilePlantillaEventos()
        {
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            string FechaRegistro = DateTime.Now.ToShortDateString();
            //Best Way To read file direct from stream
            cCargaMasivaRCE CargaMasiva = new cCargaMasivaRCE();
            IExcelDataReader excelReader = null;
            System.Data.DataSet ds = new System.Data.DataSet();
            string nameFile = System.IO.Path.GetFileName(FUloadExcel.PostedFile.FileName.ToString().Trim());
            FUloadExcel.PostedFile.SaveAs(Server.MapPath("~/Archivos/") + nameFile);
            try
            {

                //file.InputStream is the file stream stored in memeory by any ways like by upload file control or from database
                //int excelFlag = 1; //this flag us used for execl file format .xls or .xlsx
                //if (excelFlag == 1)
                //{
                //    //1. Reading from a binary Excel file ('97-2003 format; *.xls)

                //    excelReader = ExcelReaderFactory.CreateBinaryReader(FUloadExcel.PostedFile.InputStream);
                //    excelReader.IsFirstRowAsColumnNames = true;
                //}
                //else if (excelFlag == 2)
                //{
                //    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                //    excelReader = ExcelReaderFactory.CreateOpenXmlReader(FUloadExcel.PostedFile.InputStream);
                //    //excelReader.IsFirstRowAsColumnNames = true;
                //}
                //var stream = File.Open(FUloadExcel.PostedFile.FileName, FileMode.Open, FileAccess.Read);
                //excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                string sql = "SELECT * FROM [Plantilla final$]";
                string excelConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Archivos/") + nameFile + ";Extended Properties=Excel 12.0;";
                
                using (OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, excelConnection))
                {
                    adaptor.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al analizar el archivo Eventos. " + ex.Message);
            }
            if (/*excelReader != null*/ ds != null)
            {
                //...
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                //ds = excelReader.AsDataSet();
                //...
                ////4. DataSet - Create column names from first row
                //excelReader.IsFirstRowAsColumnNames = true;
                //DataSet result = excelReader.AsDataSet();

                ////5. Data Reader methods
                //while (excelReader.Read())
                //{
                //    excelReader.GetInt32(0);
                //}
                DataTable datos = ds.Tables[0];
                DataTable dt = CargaMasiva.LastCodEvento();
                Session["IdEvento"] = dt.Rows[0][0].ToString();
                for (int i = 1; i < datos.Rows.Count; i++)
                {
                    if (datos.Rows[i][0].ToString().Trim() != "")
                    {
                        string idEmpresa = datos.Rows[i][0].ToString().Trim();
                        string Region = datos.Rows[i][1].ToString().Trim();
                        string Pais = datos.Rows[i][2].ToString().Trim();
                        string Departamento = datos.Rows[i][3].ToString().Trim();
                        string Ciudad = datos.Rows[i][4].ToString().Trim();
                        string Oficina = datos.Rows[i][5].ToString().Trim();
                        string Detalle_Ubicacion = datos.Rows[i][6].ToString().Trim();
                        string Descripcion_Evento = datos.Rows[i][7].ToString().Trim();
                        string IdServicio = datos.Rows[i][8].ToString().Trim();
                        string IdSubServicio = datos.Rows[i][9].ToString().Trim();
                        string FechaInicio = DateTime.ParseExact(datos.Rows[i][10].ToString().Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture).ToShortDateString();//Convert.ToDateTime( datos.Rows[i][10].ToString().Trim()).ToString();
                        string HoraInicio = Convert.ToDateTime(datos.Rows[i][11].ToString().Trim()).ToString("HH:mm");
                        string FechaFinalizacion = DateTime.ParseExact(datos.Rows[i][12].ToString().Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture).ToShortDateString();//Convert.ToDateTime(datos.Rows[i][12].ToString().Trim()).ToString();
                        string HoraFinalizacion = Convert.ToDateTime(datos.Rows[i][13].ToString().Trim()).ToString("HH:mm");
                        string FechaDescubrimiento = DateTime.ParseExact(datos.Rows[i][14].ToString().Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture).ToShortDateString();//Convert.ToDateTime(datos.Rows[i][14].ToString().Trim()).ToString();
                        string HoraDescubrimiento = Convert.ToDateTime(datos.Rows[i][15].ToString().Trim()).ToString("HH:mm");
                        string Canal = datos.Rows[i][16].ToString().Trim();
                        string Generador = datos.Rows[i][17].ToString().Trim();
                        string IdCargoResponsable = datos.Rows[i][18].ToString().Trim();
                        string CargoResponsable = datos.Rows[i][19].ToString().Trim();
                        string CuantiaPerdida = datos.Rows[i][20].ToString().Trim();
                        

                        try
                        {
                            CargaMasiva.registrarEvento(idEmpresa, Region, Pais, Departamento, Ciudad, Oficina, Detalle_Ubicacion, Descripcion_Evento, IdServicio, IdSubServicio,
                                FechaInicio, HoraInicio, FechaFinalizacion, HoraFinalizacion, FechaDescubrimiento, HoraDescubrimiento, Canal, Generador, IdCargoResponsable, CuantiaPerdida,
                                FechaRegistro, CargoResponsable, IdUsuario);
                        }
                        catch (Exception ex)
                        {
                            Mensaje("Error al registrar el evento de la linea: " + i + "." + ex.Message);
                        }
                    }
                }
                //6. Free resources (IExcelDataReader is IDisposable)
                //excelReader.Close();
            }
        }
        private void LoadFilePlantillaRiesgosControles()
        {
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            DateTime FechaRegistro = DateTime.Now;
            //Best Way To read file direct from stream
            cCargaMasivaRCE CargaMasiva = new cCargaMasivaRCE();
            IExcelDataReader excelReader = null;
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {

                //file.InputStream is the file stream stored in memeory by any ways like by upload file control or from database
                int excelFlag = 1; //this flag us used for execl file format .xls or .xlsx
                if (excelFlag == 1)
                {
                    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(FUloadExcel.PostedFile.InputStream);
                    excelReader.IsFirstRowAsColumnNames = true;
                }
                else if (excelFlag == 2)
                {
                    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(FUloadExcel.PostedFile.InputStream);
                    //excelReader.IsFirstRowAsColumnNames = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al analizar el archivo Eventos. " + ex.Message);
            }
            if (excelReader != null)
            {
                ds = excelReader.AsDataSet();
                DataTable datos = ds.Tables[0];
                DataTable dt = CargaMasiva.LastCodRiesgovsControl();
                Session["IdControlesRiesgo"] = dt.Rows[0][0].ToString();
                CRiesgoControl riesgoControl = new CRiesgoControl();
                for (int i = 0; i < datos.Rows.Count; i++)
                {
                    if (datos.Rows[i][0].ToString().Trim() != "")
                    {
                        riesgoControl.IdRiesgo = ValidarNumero(datos.Rows[i][0].ToString().Trim());
                        riesgoControl.IdControl = ValidarNumero(datos.Rows[i][1].ToString().Trim());
                        riesgoControl.IdCausa = datos.Rows[i][2].ToString().Trim();
                        riesgoControl.IdUsuario = IdUsuario;

                        try
                        {
                            //CargaMasiva.registrarRiesgoControl(riesgoControl);
                        }
                        catch (Exception ex)
                        {
                            Mensaje("Error al registrar el Riesgo vs el Control de la linea: " + i + "." + ex.Message);
                        }
                    }
                }
                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();
            }
        }
        private void LoadFilePLantillaRiesgos()
        {

            int IdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            string FechaRegistro = DateTime.Now.ToString();
            /*IExcelDataReader ExcelReader = ExcelReaderFactory.CreateBinaryReader(FUloadExcel.PostedFile.InputStream);
            ExcelReader.IsFirstRowAsColumnNames = true;
            System.Data.DataSet DSResult = ExcelReader.AsDataSet();
            ExcelReader.Close();
            armarInformacionRiesgo(DSResult.Tables[0]);*/
            ///Ajustes camilo 23-07-2015
            ///
            //Best Way To read file direct from stream
            cCargaMasivaRCE CargaMasiva = new cCargaMasivaRCE();
            IExcelDataReader excelReader = null;
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {

                //file.InputStream is the file stream stored in memeory by any ways like by upload file control or from database
                int excelFlag = 1; //this flag us used for execl file format .xls or .xlsx
                if (excelFlag == 1)
                {
                    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(FUloadExcel.PostedFile.InputStream);
                    excelReader.IsFirstRowAsColumnNames = true;
                }
                else if (excelFlag == 2)
                {
                    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(FUloadExcel.PostedFile.InputStream);
                    //excelReader.IsFirstRowAsColumnNames = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al analizar el archivo Riesgos. " + ex.Message);
            }
            if (excelReader != null)
            {
                //...
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                ds = excelReader.AsDataSet();
                //...
                ////4. DataSet - Create column names from first row
                //excelReader.IsFirstRowAsColumnNames = true;
                //DataSet result = excelReader.AsDataSet();

                ////5. Data Reader methods
                //while (excelReader.Read())
                //{
                //    //excelReader.GetInt32(0);
                //}
                DataTable datos = ds.Tables[0];
                DataTable dt = CargaMasiva.lastCod();
                Session["IdRiesgo"] = dt.Rows[0][0].ToString();
                for (int i = 1; i < datos.Rows.Count; i++)
                {
                    if (datos.Rows[i][0].ToString().Trim() != "")
                    {
                        string Region = datos.Rows[i][0].ToString().Trim();
                        string Pais = datos.Rows[i][1].ToString().Trim();
                        string Departamento = datos.Rows[i][2].ToString().Trim();
                        string Ciudad = datos.Rows[i][3].ToString().Trim();
                        string Oficina = datos.Rows[i][4].ToString().Trim();
                        string Cadena_de_valor = datos.Rows[i][5].ToString().Trim();
                        string Macroproceso = datos.Rows[i][6].ToString().Trim();
                        string Proceso = datos.Rows[i][7].ToString().Trim();
                        string Subproceso = datos.Rows[i][8].ToString().Trim();
                        string Actividad = datos.Rows[i][9].ToString().Trim();
                        string Riesgos_globales = datos.Rows[i][10].ToString().Trim();
                        string Clasificación_general = datos.Rows[i][11].ToString().Trim();
                        string Clasificación_particular = datos.Rows[i][12].ToString().Trim();
                        string Factor_de_riesgo_operativo = datos.Rows[i][13].ToString().Trim();
                        string Sub_factor_riesgo_operativo = datos.Rows[i][14].ToString().Trim();
                        string Tipo_de_evento = datos.Rows[i][15].ToString().Trim();
                        string Riesgo_asociado = datos.Rows[i][16].ToString().Trim();
                        string ListaRiesgoAsociadoLA = datos.Rows[i][17].ToString().Trim();
                        string ListaFactorRiesgoLAFT = datos.Rows[i][18].ToString().Trim();
                        string Nombre = datos.Rows[i][19].ToString().Trim();
                        string Descripción_del_riesgo = datos.Rows[i][20].ToString().Trim();
                        string Cargo_Responsable = datos.Rows[i][21].ToString().Trim();
                        string ListaCausas = datos.Rows[i][22].ToString().Trim();
                        string ListaConsecuencias = datos.Rows[i][23].ToString().Trim();
                        string Frecuencia_Cualitativa = datos.Rows[i][24].ToString().Trim();
                        string Se_esperaba_la_ocurrencia_de_un_evento_entre_un = datos.Rows[i][25].ToString().Trim();
                        string Porcentaje_y_un = datos.Rows[i][26].ToString().Trim();
                        string Impacto_cualitativo = datos.Rows[i][27].ToString().Trim();
                        string Pérdida_económica_entre = datos.Rows[i][28].ToString().Trim();
                        string y = datos.Rows[i][29].ToString().Trim();
                        string tratamiento = datos.Rows[i][30].ToString().Trim();
                        try
                        {
                            CargaMasiva.registrarRiesgo(Region, Pais, Departamento, Ciudad, Oficina, Cadena_de_valor, Macroproceso, Proceso, Subproceso, Actividad, Riesgos_globales,
                                Clasificación_general, Clasificación_particular, Factor_de_riesgo_operativo, Sub_factor_riesgo_operativo, Tipo_de_evento, Riesgo_asociado, ListaRiesgoAsociadoLA,
                                ListaFactorRiesgoLAFT, Nombre, Descripción_del_riesgo, ListaCausas, ListaConsecuencias, Cargo_Responsable, Frecuencia_Cualitativa, Se_esperaba_la_ocurrencia_de_un_evento_entre_un,
                                Porcentaje_y_un, Impacto_cualitativo, Pérdida_económica_entre, y, tratamiento, IdUsuario, FechaRegistro);
                        }
                        catch (Exception ex)
                        {
                            Mensaje("Error al registrar el riesgo de la linea: " + i + "." + ex.Message);
                        }
                    }
                }
                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();
            }


        }
        private void armarInformacionRiesgo(DataTable dt)
        {
            int intCantidadRegistros = dt.Rows.Count;
            DataTable dtInfoRiesgosIns = new DataTable();
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

        protected void ImButtonExcelExportPlantillaRiesgo_Click(object sender, ImageClickEventArgs e)
        {
            cCargaMasivaRCE carga = new cCargaMasivaRCE();
            if (DDLopciones.SelectedValue == "1")
            {
                byte[] excel = carga.mtdDescargarPlantillaRiesgos();
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
            if (DDLopciones.SelectedValue == "2")
            {
                DataTable dt = carga.DescargarPlantillaCargaControles();
                string[] columnNames = dt.Columns.Cast<DataColumn>()
                         .Select(x => x.ColumnName)
                         .ToArray();
                XLWorkbook workbook = new XLWorkbook();
                IXLWorksheet worksheet = workbook.Worksheets.Add("Plantilla");
                // Se crean los encabezados dinámicamente
                int cell = 0;
                foreach (var column in columnNames)
                {
                    worksheet.Cell(1, cell + 1).SetValue(column);
                    cell++;
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Range(1, 1, 1, cell).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                // Prepara la respuesta
                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", "attachment;filename=\"Plantilla cargue controles.xlsx\"");

                // Vacíe el libro de trabajo a Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                httpResponse.End();
            }
            if (DDLopciones.SelectedValue == "3")
            {
                byte[] excel = carga.mtdDescargarPlantillaEventos();
                if (excel != null)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "Application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=EventosPlantillaCargaMasiva.xls");
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(excel);
                    Response.End();
                }
            }
            if (DDLopciones.SelectedValue == "4")
            {
                //byte[] excel = carga.mtdDescargarPlantillaRisgovsControles();
                //if (excel != null)
                //{
                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.ContentType = "Application/pdf";
                //    Response.AddHeader("Content-Disposition", "attachment; filename=RiesgosVsControlesPlantillaCargaMasiva.xls");
                //    Response.Charset = "";
                //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //    Response.BinaryWrite(excel);
                //    Response.End();
                //}
                XLWorkbook workbook = new XLWorkbook();
                IXLWorksheet worksheet = workbook.Worksheets.Add("Plantilla");
                worksheet.Cell(1, 1).SetValue("IdRiesgo");
                worksheet.Cell(1, 2).SetValue("IdControl");
                worksheet.Cell(1, 3).SetValue("IdCausa");
                worksheet.Columns().AdjustToContents();
                worksheet.Range(1, 1, 1, 3).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                // Prepara la respuesta
                HttpResponse httpResponse = Response;
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=RiesgosVsControlesPlantillaCargaMasiva.xlsx");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);


                // Vacíe el libro de trabajo a Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
            }
        }

        private void LoadPlantilla(string tipoCarga)
        {
            try
            {
                string pathFile = FUloadExcel.FileName;
                Byte[] archivo = FUloadExcel.FileBytes;
                int length = Convert.ToInt32(FUloadExcel.FileContent.Length);
                string extension = Path.GetExtension(FUloadExcel.FileName).ToLower().ToString().Trim();
                int IdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
                //saveFile(FUloadExcel.FileName, length, archivo, extension, pathFile);

                // Se crean los objetos
                cControlEntity controlEntity = new cControlEntity();
                CRiesgoControl riesgoControl = new CRiesgoControl();
                cCargaMasivaRCE carga = new cCargaMasivaRCE();
                cRiesgo riesgo = new cRiesgo();

                // Leer archivo excel
                using (var excelWorkbook = new XLWorkbook(FUloadExcel.FileContent))
                {
                    var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();

                    // Toma el ultimo Id de la tabla para mostrar la grilla final
                    using (DataTable dt = carga.LastCodRiesgovsControl())
                    {
                        Session["IdControlesRiesgo"] = dt.Rows[0][0].ToString();
                    }

                    // Recorre el archivo y valida que se encuentre correcto de acuerdo al tipo de carga
                    foreach (var dataRow in nonEmptyDataRows)
                    {
                        if (dataRow.RowNumber() > 1)
                        {
                            if (tipoCarga == "2")
                            {
                                // Se valida que los datos en la plantilla sean los correctos
                                if (!int.TryParse(dataRow.Cell(4).Value.ToString(), out int value) || !int.TryParse(dataRow.Cell(5).Value.ToString(), out value) ||
                                    !int.TryParse(dataRow.Cell(6).Value.ToString(), out value) || !int.TryParse(dataRow.Cell(5).Value.ToString(), out value)
                                    )
                                    throw new Exception($"Se han encontrado valores no válidos en la línea {dataRow.RowNumber()} por favor verifique.");

                            }
                            else if (tipoCarga == "4")
                            {
                                // Llena la entidad para validar que los valores sean correctos
                                riesgoControl.IdRiesgo = ValidarNumero(dataRow.Cell(1).Value.ToString());
                                riesgoControl.IdControl = ValidarNumero(dataRow.Cell(2).Value.ToString());
                                riesgoControl.IdCausa = dataRow.Cell(3).Value.ToString();

                                if (riesgoControl.IdRiesgo is null || riesgoControl.IdControl is null)
                                    throw new Exception($"Se han encontrado valores no válidos en la línea {dataRow.RowNumber()} por favor verifique.");

                                if(cControl.ValidarExistenciaControl(Convert.ToInt32(riesgoControl.IdControl)) == 0)
                                    throw new Exception($"El control {riesgoControl.IdControl} de línea {dataRow.RowNumber()} no existe, por favor verifique.");

                                using (DataTable dt = cControl.SeleccionarRiesgo(riesgoControl.IdRiesgo))
                                {
                                    // Validación de las causas asosciadas al riesgo
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        string[] lstCausasRiesgo = dt.Rows[0][1].ToString().Split('|');
                                        lstCausasRiesgo = lstCausasRiesgo.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                        string[] lstCausasArchivo = riesgoControl.IdCausa.Split('|');
                                        foreach (var causa in lstCausasArchivo)
                                        {
                                            var results = Array.FindAll(lstCausasRiesgo, s => s.Equals(causa));
                                            if (results.Length < 1)
                                                throw new Exception($"Se han encontrado causas no válidas en la línea {dataRow.RowNumber()} por favor verifique.");
                                        }
                                    }
                                    else
                                        throw new Exception($"El Riesgo de la línea {dataRow.RowNumber()} no existe o no tiene causas asociadas.");

                                }
                            }
                        }
                    }

                    // Recorre el archivo y realiza la inserción
                    foreach (var dataRow in nonEmptyDataRows)
                    {
                        if (dataRow.RowNumber() > 1)
                        {
                            if (tipoCarga == "2")
                            {
                                controlEntity.NombreControl = dataRow.Cell(1).Value.ToString();
                                controlEntity.DescripcionControl = dataRow.Cell(2).Value.ToString();
                                controlEntity.ObjetivoControl = dataRow.Cell(3).Value.ToString();
                                controlEntity.Responsable = Convert.ToInt32(dataRow.Cell(4).Value);
                                controlEntity.IdPeriodicidad = Convert.ToInt32(dataRow.Cell(5).Value);
                                controlEntity.IdTest = Convert.ToInt32(dataRow.Cell(6).Value);
                                controlEntity.IdMitiga = Convert.ToInt32(dataRow.Cell(7).Value);
                                controlEntity.ResponsableEjecucion = dataRow.Cell(8).Value.ToString();
                                controlEntity.IdClaseControl = ValidarNumero(dataRow.Cell(9).Value.ToString());
                                controlEntity.IdTipoControl = ValidarNumero(dataRow.Cell(10).Value.ToString());
                                controlEntity.IdResponsableExperiencia = ValidarNumero(dataRow.Cell(11).Value.ToString());
                                controlEntity.IdDocumentacion = ValidarNumero(dataRow.Cell(12).Value.ToString());
                                controlEntity.IdResponsabilidad = ValidarNumero(dataRow.Cell(13).Value.ToString());
                                controlEntity.Variable6 = ValidarNumero(dataRow.Cell(14).Value.ToString());
                                controlEntity.Variable7 = ValidarNumero(dataRow.Cell(15).Value.ToString());
                                controlEntity.Variable8 = ValidarNumero(dataRow.Cell(16).Value.ToString());
                                controlEntity.Variable9 = ValidarNumero(dataRow.Cell(17).Value.ToString());
                                controlEntity.Variable10 = ValidarNumero(dataRow.Cell(18).Value.ToString());
                                controlEntity.Variable11 = ValidarNumero(dataRow.Cell(19).Value.ToString());
                                controlEntity.Variable12 = ValidarNumero(dataRow.Cell(20).Value.ToString());
                                controlEntity.Variable13 = ValidarNumero(dataRow.Cell(21).Value.ToString());
                                controlEntity.Variable14 = ValidarNumero(dataRow.Cell(22).Value.ToString());
                                controlEntity.IdCalificacionControl = carga.CalcularEficacia(controlEntity, Session["IdControl"].ToString());

                                // Se inserta el control en la tabla
                                cControl.InsertControl(controlEntity);
                            }
                            else if (tipoCarga == "4")
                            {
                                DataTable dt = carga.LastCodRiesgovsControl();
                                riesgoControl.IdRiesgo = ValidarNumero(dataRow.Cell(1).Value.ToString());
                                riesgoControl.IdControl = ValidarNumero(dataRow.Cell(2).Value.ToString());
                                riesgoControl.IdCausa = dataRow.Cell(3).Value.ToString();
                                riesgoControl.IdUsuario = IdUsuario;

                                // inserta la ascoación Riesgo- Control en la tabla
                                riesgo.registrarRiesgoControl(riesgoControl);

                                // Inserta las causas asociadas al riesgo
                                riesgo.registrarCausaRiesgoControl(riesgoControl);

                                // Recalcula calficación riesgo inherente
                                DataTable dtInfoRiesgo = cControl.CalcularRiesgoResidual(dataRow.Cell(1).Value.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? ValidarNumero(string value)
        {
            int a = 0;
            if (int.TryParse(value, out a))
                return a;
            else
                return null;
        }

        private void saveFile(string NombreArchivo, int Length, byte[] archivo, string extension, string pathFile)
        {
            /*string path = ConfigurationManager.AppSettings.Get("DirectorioDocumentos").ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //string fullPath = Server.MapPath("/");
            fuArchivoPerfil.PostedFile.SaveAs(path + NombreArchivo);*/
            string strErrMsg = string.Empty;
            string FechaRegistro = DateTime.Now.ToString();
            cCargaMasivaRCE carga = new cCargaMasivaRCE();

            if (!carga.Guardar(NombreArchivo, Length, archivo, ref strErrMsg, FechaRegistro, pathFile, archivo))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void GVriesgos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVriesgos.PageIndex = PagIndex;
            GVriesgos.DataBind();
            LoadGridRiesgos();
        }

        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            DDLopciones.ClearSelection();
            TrButtonsExportRiesgos.Visible = false;
            TrLoadFile.Visible = false;
            TgridRiesgos.Visible = false;
        }

        protected void GVeventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVeventos.PageIndex = PagIndex;
            GVeventos.DataBind();
            LoadGridEventos();
        }

        protected void GVcontrol_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVcontrol.PageIndex = PagIndex;
            GVcontrol.DataBind();
            LoadGridControl();
        }

        protected void GVriesgovscontrol_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVriesgovscontrol.PageIndex = PagIndex;
            GVriesgovscontrol.DataBind();
            LoadGridRiesgovsControl();
        }
    }
}