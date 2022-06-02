using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
//using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class Reporte : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        cCuenta cCuenta = new cCuenta();
        cControl cControl = new cControl();
        String IdFormulario = "5017";

        #region Properties
        private DataTable infoGridReporteRiesgos;
        private DataTable InfoGridReporteRiesgos
        {
            get
            {
                infoGridReporteRiesgos = (DataTable)ViewState["infoGridReporteRiesgos"];
                return infoGridReporteRiesgos;
            }
            set
            {
                infoGridReporteRiesgos = value;
                ViewState["infoGridReporteRiesgos"] = infoGridReporteRiesgos;
            }
        }

        private int pagIndexInfoGridReporteRiesgos;
        private int PagIndexInfoGridReporteRiesgos
        {
            get
            {
                pagIndexInfoGridReporteRiesgos = (int)ViewState["pagIndexInfoGridReporteRiesgos"];
                return pagIndexInfoGridReporteRiesgos;
            }
            set
            {
                pagIndexInfoGridReporteRiesgos = value;
                ViewState["pagIndexInfoGridReporteRiesgos"] = pagIndexInfoGridReporteRiesgos;
            }
        }

        private DataTable infoGridReporteRiesgosControles;
        private DataTable InfoGridReporteRiesgosControles
        {
            get
            {
                infoGridReporteRiesgosControles = (DataTable)ViewState["infoGridReporteRiesgosControles"];
                return infoGridReporteRiesgosControles;
            }
            set
            {
                infoGridReporteRiesgosControles = value;
                ViewState["infoGridReporteRiesgosControles"] = infoGridReporteRiesgosControles;
            }
        }

        private int pagIndexInfoGridReporteRiesgosControles;
        private int PagIndexInfoGridReporteRiesgosControles
        {
            get
            {
                pagIndexInfoGridReporteRiesgosControles = (int)ViewState["pagIndexInfoGridReporteRiesgosControles"];
                return pagIndexInfoGridReporteRiesgosControles;
            }
            set
            {
                pagIndexInfoGridReporteRiesgosControles = value;
                ViewState["pagIndexInfoGridReporteRiesgosControles"] = pagIndexInfoGridReporteRiesgosControles;
            }
        }

        private DataTable infoGridReporteRiesgosEventos;
        private DataTable InfoGridReporteRiesgosEventos
        {
            get
            {
                infoGridReporteRiesgosEventos = (DataTable)ViewState["infoGridReporteRiesgosEventos"];
                return infoGridReporteRiesgosEventos;
            }
            set
            {
                infoGridReporteRiesgosEventos = value;
                ViewState["infoGridReporteRiesgosEventos"] = infoGridReporteRiesgosEventos;
            }
        }

        private int pagIndexInfoGridReporteRiesgosEventos;
        private int PagIndexInfoGridReporteRiesgosEventos
        {
            get
            {
                pagIndexInfoGridReporteRiesgosEventos = (int)ViewState["pagIndexInfoGridReporteRiesgosEventos"];
                return pagIndexInfoGridReporteRiesgosEventos;
            }
            set
            {
                pagIndexInfoGridReporteRiesgosEventos = value;
                ViewState["pagIndexInfoGridReporteRiesgosEventos"] = pagIndexInfoGridReporteRiesgosEventos;
            }
        }

        private DataTable infoGridReporteRiesgosPlanesAccion;
        private DataTable InfoGridReporteRiesgosPlanesAccion
        {
            get
            {
                infoGridReporteRiesgosPlanesAccion = (DataTable)ViewState["infoGridReporteRiesgosPlanesAccion"];
                return infoGridReporteRiesgosPlanesAccion;
            }
            set
            {
                infoGridReporteRiesgosPlanesAccion = value;
                ViewState["infoGridReporteRiesgosPlanesAccion"] = infoGridReporteRiesgosPlanesAccion;
            }
        }

        private int pagIndexInfoGridReporteRiesgosPlanesAccion;
        private int PagIndexInfoGridReporteRiesgosPlanesAccion
        {
            get
            {
                pagIndexInfoGridReporteRiesgosPlanesAccion = (int)ViewState["pagIndexInfoGridReporteRiesgosPlanesAccion"];
                return pagIndexInfoGridReporteRiesgosPlanesAccion;
            }
            set
            {
                pagIndexInfoGridReporteRiesgosPlanesAccion = value;
                ViewState["pagIndexInfoGridReporteRiesgosPlanesAccion"] = pagIndexInfoGridReporteRiesgosPlanesAccion;
            }
        }


        private int pagIndexInfoGridReporteControles;
        private int PagIndexInfoGridReporteControles
        {
            get
            {
                pagIndexInfoGridReporteControles = (int)ViewState["pagIndexInfoGridReporteControles"];
                return pagIndexInfoGridReporteControles;
            }
            set
            {
                pagIndexInfoGridReporteControles = value;
                ViewState["pagIndexInfoGridReporteControles"] = pagIndexInfoGridReporteControles;
            }
        }

        private DataTable infoGridReporteControles;
        private DataTable InfoGridReporteControles
        {
            get
            {
                infoGridReporteControles = (DataTable)ViewState["infoGridReporteControles"];
                return infoGridReporteControles;
            }
            set
            {
                infoGridReporteControles = value;
                ViewState["infoGridReporteControles"] = infoGridReporteControles;
            }
        }

        #region Reportes de Modificacion de Controles y Riesgos
        private DataTable dtInfoGridReporteModControl;
        private DataTable DtInfoGridReporteModControl
        {
            get
            {
                dtInfoGridReporteModControl = (DataTable)ViewState["dtInfoGridReporteModControl"];
                return dtInfoGridReporteModControl;
            }
            set
            {
                dtInfoGridReporteModControl = value;
                ViewState["dtInfoGridReporteModControl"] = dtInfoGridReporteModControl;
            }
        }

        private DataTable dtInfoGridReporteModRiesgo;
        private DataTable DtInfoGridReporteModRiesgo
        {
            get
            {
                dtInfoGridReporteModRiesgo = (DataTable)ViewState["dtInfoGridReporteModRiesgo"];
                return dtInfoGridReporteModRiesgo;
            }
            set
            {
                dtInfoGridReporteModRiesgo = value;
                ViewState["dtInfoGridReporteModRiesgo"] = dtInfoGridReporteModRiesgo;
            }
        }

        private int pagIndexInfoGridReporteModControl;
        private int PagIndexInfoGridReporteModControl
        {
            get
            {
                pagIndexInfoGridReporteModControl = (int)ViewState["pagIndexInfoGridReporteModControl"];
                return pagIndexInfoGridReporteModControl;
            }
            set
            {
                pagIndexInfoGridReporteModControl = value;
                ViewState["pagIndexInfoGridReporteModControl"] = pagIndexInfoGridReporteModControl;
            }
        }

        private int pagIndexInfoGridReporteModRiesgo;
        private int PagIndexInfoGridReporteModRiesgo
        {
            get
            {
                pagIndexInfoGridReporteModRiesgo = (int)ViewState["pagIndexInfoGridReporteModRiesgo"];
                return pagIndexInfoGridReporteModRiesgo;
            }
            set
            {
                pagIndexInfoGridReporteModRiesgo = value;
                ViewState["pagIndexInfoGridReporteModRiesgo"] = pagIndexInfoGridReporteModRiesgo;
            }
        }
        #endregion Reportes de Modificacion de Controles y Riesgos
        #region Reporte Causas sin Control
        private DataTable infoGridReporteCausaSinControl;
        private DataTable InfoGridReporteCausaSinControl
        {
            get
            {
                infoGridReporteCausaSinControl = (DataTable)ViewState["infoGridReporteCausaSinControl"];
                return infoGridReporteCausaSinControl;
            }
            set
            {
                infoGridReporteCausaSinControl = value;
                ViewState["infoGridReporteCausaSinControl"] = infoGridReporteCausaSinControl;
            }
        }
        private int rowGridReporteCausaSinControl;
        private int RowGridReporteCausaSinControl
        {
            get
            {
                rowGridReporteCausaSinControl = (int)ViewState["rowGridReporteCausaSinControl"];
                return rowGridReporteCausaSinControl;
            }
            set
            {
                rowGridReporteCausaSinControl = value;
                ViewState["rowGridReporteCausaSinControl"] = rowGridReporteCausaSinControl;
            }
        }
        private int pagIndexReporteCausaSinControl;
        private int PagIndexReporteCausaSinControl
        {
            get
            {
                pagIndexReporteCausaSinControl = (int)ViewState["pagIndexReporteCausaSinControl"];
                return pagIndexReporteCausaSinControl;
            }
            set
            {
                pagIndexReporteCausaSinControl = value;
                ViewState["pagIndexReporteCausaSinControl"] = pagIndexReporteCausaSinControl;
            }
        }
        #endregion Reporte Causas sin Control
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadDDLCadenaValor();
                loadDDLClasificacion();
                mtdLoadAreas();
                inicializarValores();
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGridReporteRiesgos = 0;
            PagIndexInfoGridReporteRiesgosControles = 0;
            PagIndexInfoGridReporteRiesgosEventos = 0;
            PagIndexInfoGridReporteRiesgosPlanesAccion = 0;
            PagIndexInfoGridReporteModControl = 0;
            PagIndexInfoGridReporteModRiesgo = 0;
            PagIndexInfoGridReporteControles = 0;
            PagIndexReporteCausaSinControl = 0;
        }

        #region Loads
        private void loadDDLCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCadenaValor();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList52.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar cadena valor. " + ex.Message);
            }
        }
        private void mtdLoadAreas()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLAreas();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLareas.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreArea"].ToString().Trim(), dtInfo.Rows[i]["IdArea"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar areas. " + ex.Message);
            }
        }
        private void loadDDLClasificacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList56.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación riesgo. " + ex.Message);
            }
        }

        private void loadDDLMacroproceso(String IdCadenaValor, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLMacroproceso(IdCadenaValor);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList53.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar macroproceso. " + ex.Message);
            }
        }

        private void loadDDLProceso(String IdMacroproceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLProceso(IdMacroproceso);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList54.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar proceso. " + ex.Message);
            }
        }

        private void loadDDLClasificacionGeneral(String IdClasificacionRiesgo, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacionGeneral(IdClasificacionRiesgo);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList57.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionGeneralRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionGeneralRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación general. " + ex.Message);
            }
        }
        #endregion Loads

        #region DDL
        protected void DropDownList52_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList53.Items.Clear();
            DropDownList53.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList52.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(DropDownList52.SelectedValue.ToString().Trim(), 2);
            }
        }

        protected void DropDownList53_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList53.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList53.SelectedValue.ToString().Trim(), 2);
            }
        }

        protected void DropDownList56_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList57.Items.Clear();
            DropDownList57.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList56.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLClasificacionGeneral(DropDownList56.SelectedValue.ToString().Trim(), 2);
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "5" || DropDownList1.SelectedValue == "6")
            {
                DropDownList2.Enabled = false;
                DropDownList3.Enabled = false;
                DropDownList4.Enabled = false;
                DropDownList52.Enabled = false;
                DropDownList53.Enabled = false;
                DropDownList54.Enabled = false;
                DropDownList56.Enabled = false;
                DropDownList57.Enabled = false;
                DDLareas.Enabled = false;
                TxtFechaIni.Enabled = true;
                TxtFechaFin.Enabled = true;
            }
            else
            {
                if (DropDownList1.SelectedValue == "7")
                {
                    DropDownList2.Enabled = false;
                    DropDownList3.Enabled = false;
                    DropDownList4.Enabled = false;
                    DropDownList52.Enabled = false;
                    DropDownList53.Enabled = false;
                    DropDownList54.Enabled = false;
                    DropDownList56.Enabled = false;
                    DropDownList57.Enabled = false;
                    DDLareas.Enabled = false;
                    TxtFechaIni.Enabled = false;
                    TxtFechaFin.Enabled = false;
                }
                else
                {
                    DropDownList2.Enabled = true;
                    DropDownList3.Enabled = true;
                    DropDownList4.Enabled = true;
                    DropDownList52.Enabled = true;
                    DropDownList53.Enabled = true;
                    DropDownList54.Enabled = true;
                    DropDownList56.Enabled = true;
                    DropDownList57.Enabled = true;
                    DDLareas.Enabled = true;
                    TxtFechaIni.Enabled = false;
                    TxtFechaFin.Enabled = false;
                }
            }
        }
        #endregion DDL

        #region Buttons
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                inicializarValores();
                switch (DropDownList1.SelectedValue.ToString().Trim())
                {
                    case "1":
                        loadGridReporteRiesgos();
                        loadInfoReporteRiesgos();
                        resetValuesConsulta();
                        ReporteRiesgos.Visible = true;
                        break;
                    case "2":
                        loadGridReporteRiesgosControles();
                        loadInfoReporteRiesgosControles();
                        resetValuesConsulta();
                        ReporteRiesgosControles.Visible = true;
                        break;
                    case "3":
                        loadGridReporteRiesgosEventos();
                        loadInfoReporteRiesgosEventos();
                        resetValuesConsulta();
                        ReporteRiesgosEventos.Visible = true;
                        break;
                    case "4":
                        loadGridReporteRiesgosPlanesAccion();
                        loadInfoReporteRiesgosPlanesAccion();
                        resetValuesConsulta();
                        ReporteRiesgosPlanesAccion.Visible = true;
                        break;
                    case "5":
                        mtdCargarGridReporteModControl();
                        mtdCargarInfoReporteModControl();
                        resetValuesConsulta();
                        ReporteModControles.Visible = true;
                        break;
                    case "6":
                        mtdCargarGridReporteModRiesgo();
                        mtdCargarInfoReporteModRiesgo();
                        resetValuesConsulta();
                        ReporteModRiesgos.Visible = true;
                        break;
                    case "7":
                        mtdCargarGridReporteControles();
                        mtdCargarInfoReporteControles();
                        resetValuesConsulta();
                        ReporteControles.Visible = true;
                        break;
                    case "8":
                        mtdLoadGridReporteCausassinControles();
                        mtdLoadInfoGridReporteCausassinControles();
                        resetValuesConsulta();
                        ReporteCausasSinControl.Visible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la busqueda. " + ex.Message);
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesConsulta();
            loadGridReporteRiesgos();
            loadGridReporteRiesgosControles();
            loadGridReporteRiesgosEventos();
            loadGridReporteRiesgosPlanesAccion();
            inicializarValores();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteRiesgos, Response, "Reporte Riesgos");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteRiesgosControles, Response, "Reporte Riesgos vs controles");
            //InfoGridReporteControles.ExportToExcel(ExcelFilePath);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                exportExcel(InfoGridReporteRiesgosEventos, Response, "Reporte Riesgos vs eventos");
            }
            catch (Exception ex)
            {
                Mensaje("Error al exportar Reporte Riesgos vs eventos." + ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteRiesgosPlanesAccion, Response, "Reporte Riesgos vs planes de acción");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteControles, Response, "Reporte Controles");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            exportExcel(DtInfoGridReporteModControl, Response, "Reporte Modificacion de Controles");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            exportExcel(DtInfoGridReporteModRiesgo, Response, "Reporte Modificacion de Riesgo");
        }
        #endregion Buttons

        #region GridView
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteRiesgos = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridReporteRiesgos;
            GridView1.DataSource = InfoGridReporteRiesgos;
            GridView1.DataBind();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteRiesgosControles = e.NewPageIndex;
            GridView2.PageIndex = PagIndexInfoGridReporteRiesgosControles;
            GridView2.DataSource = InfoGridReporteRiesgosControles;
            GridView2.DataBind();
        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteRiesgosEventos = e.NewPageIndex;
            GridView3.PageIndex = PagIndexInfoGridReporteRiesgosEventos;
            GridView3.DataSource = InfoGridReporteRiesgosEventos;
            GridView3.DataBind();
        }

        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteRiesgosPlanesAccion = e.NewPageIndex;
            GridView4.PageIndex = PagIndexInfoGridReporteRiesgosPlanesAccion;
            GridView4.DataSource = InfoGridReporteRiesgosPlanesAccion;
            GridView4.DataBind();
        }

        protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteControles = e.NewPageIndex;
            GridView7.PageIndex = PagIndexInfoGridReporteControles;
            GridView7.DataSource = InfoGridReporteControles;
            GridView7.DataBind();
        }

        protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteModControl = e.NewPageIndex;
            GridView5.PageIndex = PagIndexInfoGridReporteModControl;
            GridView5.DataSource = DtInfoGridReporteModControl;
            GridView5.DataBind();
            /*mtdCargarGridReporteModControl();
            mtdCargarInfoReporteModControl();*/

        }

        protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteModRiesgo = e.NewPageIndex;
            GridView6.PageIndex = PagIndexInfoGridReporteModRiesgo;
            GridView6.DataSource = DtInfoGridReporteModRiesgo;
            GridView6.DataBind();
            /*mtdCargarGridReporteModRiesgo();
            mtdCargarInfoReporteModRiesgo();*/

        }
        #endregion GridView

        private String causas(String Causas)
        {
            DataTable dtInfoCausas = new DataTable();
            dtInfoCausas = cRiesgo.causas(Causas);
            Causas = "";
            for (int ca = 0; ca < dtInfoCausas.Rows.Count; ca++)
            {
                Causas += dtInfoCausas.Rows[ca]["NombreCausas"].ToString().Trim() + ". ";
            }
            return Causas;
        }

        private String consecuencias(String Consecuencias)
        {
            DataTable dtInfoConsecuencias = new DataTable();
            dtInfoConsecuencias = cRiesgo.consecuencias(Consecuencias);
            Consecuencias = "";
            for (int con = 0; con < dtInfoConsecuencias.Rows.Count; con++)
            {
                Consecuencias += dtInfoConsecuencias.Rows[con]["NombreConsecuencia"].ToString().Trim() + ". ";
            }
            return Consecuencias;
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {
            /*Response.Clear();
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
            Response.End();*/
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(dt);
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

        private void resetValuesConsulta()
        {
            DropDownList1.SelectedIndex = 0;
            DropDownList52.SelectedIndex = 0;
            DropDownList53.Items.Clear();
            DropDownList53.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            DropDownList56.SelectedIndex = 0;
            DropDownList57.Items.Clear();
            DropDownList57.Items.Insert(0, new ListItem("---", "---"));
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;
            DDLareas.ClearSelection();
            ReporteRiesgos.Visible = false;
            ReporteRiesgosControles.Visible = false;
            ReporteRiesgosEventos.Visible = false;
            ReporteRiesgosPlanesAccion.Visible = false;
            ReporteModControles.Visible = false;
            ReporteModRiesgos.Visible = false;
            ReporteCausasSinControl.Visible = false;
            TxtFechaIni.Text = string.Empty;
            TxtFechaFin.Text = string.Empty;
            ReporteControles.Visible = false;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Reporte Riesgos
        private void loadGridReporteRiesgos()
        {
            DataTable grid = new DataTable();

            #region Columnas GRID
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("DescripcionRiesgo", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("TipoEvento", typeof(string));
            grid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("FrecuenciaInherente", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
            grid.Columns.Add("ImpactoInherente", typeof(string));
            grid.Columns.Add("CodigoImpactoInherente", typeof(string));
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CodigoRiesgoInherente", typeof(string));
            grid.Columns.Add("FrecuenciaResidual", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
            grid.Columns.Add("ImpactoResidual", typeof(string));
            grid.Columns.Add("CodigoImpactoResidual", typeof(string));
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoRiesgoResidual", typeof(string));
            grid.Columns.Add("NombreArea", typeof(string));
            #endregion Columnas GRID

            //Fin Ajuetes 01
            InfoGridReporteRiesgos = grid;
            GridView1.DataSource = InfoGridReporteRiesgos;
            GridView1.DataBind();
        }

        private void loadInfoReporteRiesgos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.ReporteRiesgos(DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(), "1", "---", DDLareas.SelectedValue.ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar informacion
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteRiesgos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ubicacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubFactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoAsociadoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Causas"].ToString().Trim(),//Causas,
                        dtInfo.Rows[rows]["Consecuencias"].ToString().Trim(),//Consecuencias,
                        dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                        dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreArea"].ToString().Trim()
                        });
                }
                #endregion Recorrido para llenar informacion

                GridView1.PageIndex = PagIndexInfoGridReporteRiesgos;
                GridView1.DataSource = InfoGridReporteRiesgos;
                GridView1.DataBind();
            }
            else
            {
                loadGridReporteRiesgos();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Reporte Riesgos

        #region Reporte Riesgos-Controles
        private void loadGridReporteRiesgosControles()
        {
            #region GRID
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("DescripcionRiesgo", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("TipoEvento", typeof(string));
            grid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("FrecuenciaInherente", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
            grid.Columns.Add("ImpactoInherente", typeof(string));
            grid.Columns.Add("CodigoImpactoInherente", typeof(string));
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CodigoRiesgoInherente", typeof(string));
            grid.Columns.Add("FrecuenciaResidual", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
            grid.Columns.Add("ImpactoResidual", typeof(string));
            grid.Columns.Add("CodigoImpactoResidual", typeof(string));
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoRiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));
            grid.Columns.Add("DescripcionControl", typeof(string));
            grid.Columns.Add("ResponsableControlEjecucion", typeof(string));
            grid.Columns.Add("ResponsableControlCalificacion", typeof(string));
            grid.Columns.Add("FechaRegistroControl", typeof(string));
            grid.Columns.Add("NombrePeriodicidad", typeof(string));
            grid.Columns.Add("NombreTest", typeof(string));
            grid.Columns.Add("Variable1", typeof(string));
            grid.Columns.Add("Variable2", typeof(string));
            grid.Columns.Add("Variable3", typeof(string));
            grid.Columns.Add("Variable4", typeof(string));
            grid.Columns.Add("Variable5", typeof(string));
            grid.Columns.Add("Variable6", typeof(string));
            grid.Columns.Add("Variable7", typeof(string));
            grid.Columns.Add("Variable8", typeof(string));
            grid.Columns.Add("Variable9", typeof(string));
            grid.Columns.Add("Variable10", typeof(string));
            grid.Columns.Add("Variable11", typeof(string));
            grid.Columns.Add("Variable12", typeof(string));
            grid.Columns.Add("Variable13", typeof(string));
            grid.Columns.Add("Variable14", typeof(string));
            grid.Columns.Add("Variable15", typeof(string));
            grid.Columns.Add("NombreEscala", typeof(string));
            grid.Columns.Add("NombreMitiga", typeof(string));
            grid.Columns.Add("DesviacionImpacto", typeof(string));
            grid.Columns.Add("DesviacionFrecuencia", typeof(string));
            grid.Columns.Add("NombreArea", typeof(string));
            #endregion GRID

            InfoGridReporteRiesgosControles = grid;
            GridView2.DataSource = InfoGridReporteRiesgosControles;
            GridView2.DataBind();
        }

        private void loadInfoReporteRiesgosControles()
        {

            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.ReporteRiesgos(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                    DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                    DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), "2", "---",
                    DDLareas.SelectedValue.ToString().Trim());

                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido para llenar informacion
                    string NombreResponsableControlEjecucion = string.Empty, CodigoControl = string.Empty;
                    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                    {
                        if (dtInfo != null)
                        {
                            if (dtInfo.Rows[rows]["CodigoControl"].ToString().Trim() == "")
                                CodigoControl = "Sin Control Asociado";
                            else
                                CodigoControl = dtInfo.Rows[rows]["CodigoControl"].ToString().Trim();
                            InfoGridReporteRiesgosControles.Rows.Add(new Object[] {
                            dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["DescripcionRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["Ubicacion"].ToString().Trim(),
                            dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["FactorRiesgoOperativo"].ToString().Trim(),
                            dtInfo.Rows[rows]["SubFactorRiesgoOperativo"].ToString().Trim(),
                            dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                            dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                            dtInfo.Rows[rows]["TipoEvento"].ToString().Trim(),
                            dtInfo.Rows[rows]["RiesgoAsociadoOperativo"].ToString().Trim(),
                            dtInfo.Rows[rows]["Causas"].ToString().Trim(),//Causas,
                            dtInfo.Rows[rows]["Consecuencias"].ToString().Trim(),//Consecuencias,
                            dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                            dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                            dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                            dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                            dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                            dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                            dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),
                            CodigoControl,
                            dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                            dtInfo.Rows[rows]["DescripcionControl"].ToString().Trim(),
                            NombreResponsableControlEjecucion = mtdBuscarNombresRespEjecucion(dtInfo.Rows[rows]["ResponsableControlEjecucion"].ToString().Trim()),
                            dtInfo.Rows[rows]["ResponsableControlCalificacion"].ToString().Trim(),
                            dtInfo.Rows[rows]["FechaRegistroControl"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombrePeriodicidad"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable1"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable2"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable3"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable4"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable5"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable6"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable7"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable8"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable9"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable10"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable11"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable12"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable13"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable14"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable15"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreEscala"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreMitiga"].ToString().Trim(),
                            dtInfo.Rows[rows]["DesviacionImpacto"].ToString().Trim(),
                            dtInfo.Rows[rows]["DesviacionFrecuencia"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreArea"].ToString().Trim()
                            });
                        }

                    }
                    #endregion Recorrido para llenar informacion

                    GridView2.PageIndex = PagIndexInfoGridReporteRiesgosControles;
                    GridView2.DataSource = InfoGridReporteRiesgosControles;
                    GridView2.DataBind();
                }
                else
                {
                    loadGridReporteRiesgosControles();
                    Mensaje("No existen registros asociados a los parámetros de consulta.");
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message);
            }

        }
        #endregion Reporte Riesgos-Controles

        #region Reporte Riesgos-Eventos
        private void loadGridReporteRiesgosEventos()
        {
            #region Grid
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("DescripcionRiesgo", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("TipoEvento", typeof(string));
            grid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("FrecuenciaInherente", typeof(string));//
            grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));//
            grid.Columns.Add("ImpactoInherente", typeof(string));//
            grid.Columns.Add("CodigoImpactoInherente", typeof(string));//
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CodigoRiesgoInherente", typeof(string));//
            grid.Columns.Add("FrecuenciaResidual", typeof(string));//
            grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));//
            grid.Columns.Add("ImpactoResidual", typeof(string));//
            grid.Columns.Add("CodigoImpactoResidual", typeof(string));//
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoRiesgoResidual", typeof(string));//

            grid.Columns.Add("CodigoEvento", typeof(string));
            grid.Columns.Add("DescripcionEvento", typeof(string));
            grid.Columns.Add("ResponsableEvento", typeof(string));
            grid.Columns.Add("FechaRegistroEvento", typeof(string));
            grid.Columns.Add("ProcesoInvolucrado", typeof(string));
            grid.Columns.Add("AplicativoInvolucrado", typeof(string));
            grid.Columns.Add("ServicioProductoAfectado", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFinalizacion", typeof(string));
            grid.Columns.Add("FechaDescubrimiento", typeof(string));
            grid.Columns.Add("Catálogo afectadas", typeof(string));
            grid.Columns.Add("Cuantía total recuperada", typeof(string));
            grid.Columns.Add("ValorRecuperadoSeguro", typeof(string));
            grid.Columns.Add("Observaciones", typeof(string));
            grid.Columns.Add("NombreDepartamento", typeof(string));
            grid.Columns.Add("NombreCiudad", typeof(string));
            grid.Columns.Add("NombreOficinaSucursal", typeof(string));
            grid.Columns.Add("NombreClaseEvento", typeof(string));
            grid.Columns.Add("NombreTipoPerdidaEvento", typeof(string));
            grid.Columns.Add("NombreArea", typeof(string));
            #endregion Grid

            InfoGridReporteRiesgosEventos = grid;
            GridView3.DataSource = InfoGridReporteRiesgosEventos;
            GridView3.DataBind();
        }

        private void loadInfoReporteRiesgosEventos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.ReporteRiesgos(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), "3", "---",
                DDLareas.SelectedValue.ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar informacion
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteRiesgosEventos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ubicacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubFactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoAsociadoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Causas"].ToString().Trim(),
                        dtInfo.Rows[rows]["Consecuencias"].ToString().Trim(),
                        dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                        dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["ProcesoInvolucrado"].ToString().Trim(),
                        dtInfo.Rows[rows]["AplicativoInvolucrado"].ToString().Trim(),
                        dtInfo.Rows[rows]["ServicioProductoAfectado"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaFinalizacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaDescubrimiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["CuentaPUC"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecuperadoTotal"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecuperadoSeguro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Observaciones"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreDepartamento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreCiudad"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreOficinaSucursal"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreClaseEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoPerdidaEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreArea"].ToString().Trim()
                    });
                }
                #endregion Recorrido para llenar informacion

                GridView3.PageIndex = PagIndexInfoGridReporteRiesgosEventos;
                GridView3.DataSource = InfoGridReporteRiesgosEventos;
                GridView3.DataBind();
            }
            else
            {
                loadGridReporteRiesgosEventos();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Reporte Riesgos-Eventos

        #region Reporte Riesgos-Planes Accion
        private void loadGridReporteRiesgosPlanesAccion()
        {
            #region Grid
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("DescripcionRiesgo", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("TipoEvento", typeof(string));
            grid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("FrecuenciaInherente", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
            grid.Columns.Add("ImpactoInherente", typeof(string));
            grid.Columns.Add("CodigoImpactoInherente", typeof(string));
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CodigoRiesgoInherente", typeof(string));
            grid.Columns.Add("FrecuenciaResidual", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
            grid.Columns.Add("ImpactoResidual", typeof(string));
            grid.Columns.Add("CodigoImpactoResidual", typeof(string));
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoRiesgoResidual", typeof(string));
            grid.Columns.Add("DescripcionAccion", typeof(string));
            grid.Columns.Add("NombreTipoRecursoPlanAccion", typeof(string));
            grid.Columns.Add("ValorRecurso", typeof(string));
            grid.Columns.Add("NombreEstadoPlanAccion", typeof(string));
            grid.Columns.Add("FechaCompromiso", typeof(string));
            grid.Columns.Add("ResponsablePlanAccion", typeof(string));
            grid.Columns.Add("NombreArea", typeof(string));
            #endregion Grid

            InfoGridReporteRiesgosPlanesAccion = grid;
            GridView4.DataSource = InfoGridReporteRiesgosPlanesAccion;
            GridView4.DataBind();
        }

        private void loadInfoReporteRiesgosPlanesAccion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.ReporteRiesgos(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), "4", "---",
                DDLareas.SelectedValue.ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido de informacion
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteRiesgosPlanesAccion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ubicacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubFactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoAsociadoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Causas"].ToString().Trim(),
                        dtInfo.Rows[rows]["Consecuencias"].ToString().Trim(),
                        dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                        dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoRecursoPlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecurso"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreEstadoPlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaCompromiso"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsablePlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreArea"].ToString().Trim()
                        });
                }
                #endregion

                GridView4.PageIndex = PagIndexInfoGridReporteRiesgosPlanesAccion;
                GridView4.DataSource = InfoGridReporteRiesgosPlanesAccion;
                GridView4.DataBind();
            }
            else
            {
                loadGridReporteRiesgosPlanesAccion();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Reporte Riesgos-Planes Accion

        #region Modificacion Control
        /// <summary>
        /// Metodo para cargar el grid del reporte de Cambios a los controles
        /// </summary>
        private void mtdCargarGridReporteModControl()
        {
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("FechaModificacion", typeof(string));
            dtGrid.Columns.Add("CodigoControl", typeof(string));
            dtGrid.Columns.Add("NombreControl", typeof(string));
            dtGrid.Columns.Add("DescripcionControl", typeof(string));
            dtGrid.Columns.Add("ResponsableControl", typeof(string));
            dtGrid.Columns.Add("FechaRegistroControl", typeof(string));
            dtGrid.Columns.Add("NombrePeriodicidad", typeof(string));
            dtGrid.Columns.Add("NombreTest", typeof(string));
            /*dtGrid.Columns.Add("NombreClaseControl", typeof(string));
            dtGrid.Columns.Add("NombreTipoControl", typeof(string));
            dtGrid.Columns.Add("NombreResponsableExperiencia", typeof(string));
            dtGrid.Columns.Add("NombreDocumentacion", typeof(string));
            dtGrid.Columns.Add("NombreResponsabilidad", typeof(string));*/
            dtGrid.Columns.Add("NombreVariable", typeof(string));
            dtGrid.Columns.Add("NombreCategoria", typeof(string));
            dtGrid.Columns.Add("NombreEscala", typeof(string));
            dtGrid.Columns.Add("NombreMitiga", typeof(string));
            dtGrid.Columns.Add("NombreUsuarioCambio", typeof(string));
            dtGrid.Columns.Add("JustificacionCambio", typeof(string));
            //dtGrid.Columns.Add("UsuarioCambio", typeof(string));

            DtInfoGridReporteModControl = dtGrid;
            GridView5.DataSource = DtInfoGridReporteModControl;
            GridView5.DataBind();
        }

        private void mtdCargarInfoReporteModControl()
        {
            DataTable dtInfo = new DataTable();

            dtInfo = cRiesgo.mtdReporteCambiosControlRiesgo("1", Sanitizer.GetSafeHtmlFragment(TxtFechaIni.Text), Sanitizer.GetSafeHtmlFragment(TxtFechaFin.Text), DDLareas.SelectedValue.ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar el Grid
                foreach (DataRow dr in dtInfo.Rows)
                {
                    DtInfoGridReporteModControl.Rows.Add(new Object[] {
                        dr["FechaModificacion"].ToString().Trim(),
                        dr["CodigoControl"].ToString().Trim(),
                        dr["NombreControl"].ToString().Trim(),
                        dr["DescripcionControl"].ToString().Trim(),
                        dr["ResponsableControl"].ToString().Trim(),
                        dr["FechaRegistroControl"].ToString().Trim(),
                        dr["NombrePeriodicidad"].ToString().Trim(),
                        dr["NombreTest"].ToString().Trim(),
                        /*dr["NombreClaseControl"].ToString().Trim(),
                        dr["NombreTipoControl"].ToString().Trim(),
                        dr["NombreResponsableExperiencia"].ToString().Trim(),
                        dr["NombreDocumentacion"].ToString().Trim(),
                        dr["NombreResponsabilidad"].ToString().Trim(),*/
                        dr["NombreVariable"].ToString().Trim(),
                        dr["NombreCategoria"].ToString().Trim(),
                        dr["NombreEscala"].ToString().Trim(),
                        dr["NombreMitiga"].ToString().Trim(),
                        dr["NombreUsuarioCambio"].ToString().Trim(),
                        dr["JustificacionCambio"].ToString().Trim()});
                }
                #endregion Recorrido para llenar el Grid

                GridView5.PageIndex = PagIndexInfoGridReporteModControl;
                GridView5.DataSource = DtInfoGridReporteModControl;
                GridView5.DataBind();
            }
            else
            {
                mtdCargarGridReporteModControl();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Modificacion Control

        #region Modificacion Riesgo
        private void mtdCargarGridReporteModRiesgo()
        {
            DataTable dtGrid = new DataTable();

            #region Columnas GRID
            dtGrid.Columns.Add("FechaModificacion", typeof(string));
            dtGrid.Columns.Add("CodigoRiesgo", typeof(string));
            dtGrid.Columns.Add("NombreRiesgo", typeof(string));
            dtGrid.Columns.Add("DescripcionRiesgo", typeof(string));
            dtGrid.Columns.Add("ResponsableRiesgo", typeof(string));
            dtGrid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            dtGrid.Columns.Add("Ubicacion", typeof(string));
            dtGrid.Columns.Add("ClasificacionRiesgo", typeof(string));
            dtGrid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            dtGrid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            dtGrid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            dtGrid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            dtGrid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            dtGrid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            dtGrid.Columns.Add("TipoEvento", typeof(string));
            dtGrid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            dtGrid.Columns.Add("Causas", typeof(string));
            dtGrid.Columns.Add("Consecuencias", typeof(string));
            dtGrid.Columns.Add("CadenaValor", typeof(string));
            dtGrid.Columns.Add("Macroproceso", typeof(string));
            dtGrid.Columns.Add("Proceso", typeof(string));
            dtGrid.Columns.Add("Subproceso", typeof(string));
            dtGrid.Columns.Add("Actividad", typeof(string));
            dtGrid.Columns.Add("ListaTratamiento", typeof(string));
            dtGrid.Columns.Add("FrecuenciaInherente", typeof(string));
            dtGrid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
            dtGrid.Columns.Add("ImpactoInherente", typeof(string));
            dtGrid.Columns.Add("CodigoImpactoInherente", typeof(string));
            dtGrid.Columns.Add("RiesgoInherente", typeof(string));
            dtGrid.Columns.Add("CodigoRiesgoInherente", typeof(string));
            dtGrid.Columns.Add("FrecuenciaResidual", typeof(string));
            dtGrid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
            dtGrid.Columns.Add("ImpactoResidual", typeof(string));
            dtGrid.Columns.Add("CodigoImpactoResidual", typeof(string));
            dtGrid.Columns.Add("RiesgoResidual", typeof(string));
            dtGrid.Columns.Add("CodigoRiesgoResidual", typeof(string));
            dtGrid.Columns.Add("NombreUsuarioCambio", typeof(string));
            dtGrid.Columns.Add("JustificacionCambio", typeof(string));
            dtGrid.Columns.Add("NombreArea", typeof(string));
            #endregion Columnas GRID

            DtInfoGridReporteModRiesgo = dtGrid;
            GridView6.DataSource = DtInfoGridReporteModRiesgo;
            GridView6.DataBind();
        }

        private void mtdCargarInfoReporteModRiesgo()
        {
            DataTable dtInfo = new DataTable();

            dtInfo = cRiesgo.mtdReporteCambiosControlRiesgo("2", Sanitizer.GetSafeHtmlFragment(TxtFechaIni.Text), Sanitizer.GetSafeHtmlFragment(TxtFechaFin.Text), DDLareas.SelectedValue.ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar el Grid
                foreach (DataRow dr in dtInfo.Rows)
                {
                    #region Info GRID
                    DtInfoGridReporteModRiesgo.Rows.Add(new Object[] {
                        dr["FechaModificacion"].ToString().Trim(),
                        dr["CodigoRiesgo"].ToString().Trim(),
                        dr["NombreRiesgo"].ToString().Trim(),
                        dr["DescripcionRiesgo"].ToString().Trim(),
                        dr["ResponsableRiesgo"].ToString().Trim(),
                        dr["FechaRegistroRiesgo"].ToString().Trim(),
                        dr["Ubicacion"].ToString().Trim(),
                        dr["ClasificacionRiesgo"].ToString().Trim(),
                        dr["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dr["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dr["FactorRiesgoOperativo"].ToString().Trim(),
                        dr["SubFactorRiesgoOperativo"].ToString().Trim(),
                        dr["ListaRiesgoAsociadoLA"].ToString().Trim(),
                        dr["ListaFactorRiesgoLAFT"].ToString().Trim(),
                        dr["TipoEvento"].ToString().Trim(),
                        dr["RiesgoAsociadoOperativo"].ToString().Trim(),
                        dr["Causas"].ToString().Trim(),
                        dr["Consecuencias"].ToString().Trim(),
                        dr["CadenaValor"].ToString().Trim(),
                        dr["Macroproceso"].ToString().Trim(),
                        dr["Proceso"].ToString().Trim(),
                        dr["Subproceso"].ToString().Trim(),
                        dr["Actividad"].ToString().Trim(),
                        dr["ListaTratamiento"].ToString().Trim(),
                        dr["FrecuenciaInherente"].ToString().Trim(),
                        dr["CodigoFrecuenciaInherente"].ToString().Trim(),
                        dr["ImpactoInherente"].ToString().Trim(),
                        dr["CodigoImpactoInherente"].ToString().Trim(),
                        dr["RiesgoInherente"].ToString().Trim(),
                        dr["CodigoRiesgoInherente"].ToString().Trim(),
                        dr["FrecuenciaResidual"].ToString().Trim(),
                        dr["CodigoFrecuenciaResidual"].ToString().Trim(),
                        dr["ImpactoResidual"].ToString().Trim(),
                        dr["CodigoImpactoResidual"].ToString().Trim(),
                        dr["RiesgoResidual"].ToString().Trim(),
                        dr["CodigoRiesgoResidual"].ToString().Trim(),
                        dr["NombreUsuarioCambio"].ToString().Trim(),
                        dr["JustificacionCambio"].ToString().Trim(),
                        dr["NombreArea"].ToString().Trim()
                       });
                    #endregion Info GRID
                }
                #endregion Recorrido para llenar el Grid

                GridView6.PageIndex = PagIndexInfoGridReporteModRiesgo;
                GridView6.DataSource = DtInfoGridReporteModRiesgo;
                GridView6.DataBind();
            }
            else
            {
                mtdCargarGridReporteModRiesgo();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Modificacion Riesgo

        #region Reporte Controles
        private void mtdCargarGridReporteControles()
        {
            DataTable dtGrid = new DataTable();

            dtGrid.Columns.Add("CodigoControl", typeof(string));
            dtGrid.Columns.Add("NombreControl", typeof(string));
            dtGrid.Columns.Add("DescripcionControl", typeof(string));
            dtGrid.Columns.Add("ObjetivoControl", typeof(string));
            dtGrid.Columns.Add("ResponsableEjecucion", typeof(string));
            dtGrid.Columns.Add("ResponsableCalificacion", typeof(string));
            dtGrid.Columns.Add("FechaRegistroControl", typeof(string));
            dtGrid.Columns.Add("NombrePeriodicidad", typeof(string));
            dtGrid.Columns.Add("NombreTest", typeof(string));
            dtGrid.Columns.Add("Variable1", typeof(string));
            dtGrid.Columns.Add("Variable2", typeof(string));
            dtGrid.Columns.Add("Variable3", typeof(string));
            dtGrid.Columns.Add("Variable4", typeof(string));
            dtGrid.Columns.Add("Variable5", typeof(string));
            dtGrid.Columns.Add("Variable6", typeof(string));
            dtGrid.Columns.Add("Variable7", typeof(string));
            dtGrid.Columns.Add("Variable8", typeof(string));
            dtGrid.Columns.Add("Variable9", typeof(string));
            dtGrid.Columns.Add("Variable10", typeof(string));
            dtGrid.Columns.Add("Variable11", typeof(string));
            dtGrid.Columns.Add("Variable12", typeof(string));
            dtGrid.Columns.Add("Variable13", typeof(string));
            dtGrid.Columns.Add("Variable14", typeof(string));
            dtGrid.Columns.Add("Variable15", typeof(string));


            //dtGrid.Columns.Add("NombreVariable", typeof(string));
            //dtGrid.Columns.Add("NombreCategoria", typeof(string));

            dtGrid.Columns.Add("NombreEscala", typeof(string));
            dtGrid.Columns.Add("NombreMitiga", typeof(string));
            dtGrid.Columns.Add("DesviacionImpacto", typeof(string));
            dtGrid.Columns.Add("DesviacionFrecuencia", typeof(string));

            InfoGridReporteControles = dtGrid;
            GridView7.DataSource = InfoGridReporteControles;
            GridView7.DataBind();
        }
        private void mtdLoadGridReporteCausassinControles()
        {
            DataTable dtGrid = new DataTable();

            dtGrid.Columns.Add("NombreCausas", typeof(string));
            dtGrid.Columns.Add("CodigoRiesgo", typeof(string));
            dtGrid.Columns.Add("NombreRiesgo", typeof(string));
            dtGrid.Columns.Add("Descripcion", typeof(string));
            dtGrid.Columns.Add("RiesgoInherente", typeof(string));
            dtGrid.Columns.Add("RiesgoResidual", typeof(string));
            dtGrid.Columns.Add("CodigoEvento", typeof(string));
            dtGrid.Columns.Add("DescripcionEvento", typeof(string));
            dtGrid.Columns.Add("NombreArea", typeof(string));

            InfoGridReporteCausaSinControl = dtGrid;
            GVcausasControl.DataSource = InfoGridReporteCausaSinControl;
            GVcausasControl.DataBind();
        }
        private void mtdCargarInfoReporteControles()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                string ResponsableEjecucion = string.Empty;
                dtInfo = cRiesgo.mtdReporteControles(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                    DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                    DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim());
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido para llenar el Grid
                    foreach (DataRow dr in dtInfo.Rows)
                    {

                        InfoGridReporteControles.Rows.Add(new Object[] {
                        dr["CodigoControl"].ToString().Trim(),
                        dr["NombreControl"].ToString().Trim(),
                        dr["DescripcionControl"].ToString().Trim(),
                        dr["ObjetivoControl"].ToString().Trim(),
                        ResponsableEjecucion = mtdBuscarNombresRespEjecucion(dr["ResponsableEjecucion"].ToString().Trim()),
                        dr["ResponsableCalificacion"].ToString().Trim(),
                        dr["FechaRegistroControl"].ToString().Trim(),
                        dr["NombrePeriodicidad"].ToString().Trim(),
                        dr["NombreTest"].ToString().Trim(),
                        dr["Variable1"].ToString().Trim(),
                        dr["Variable2"].ToString().Trim(),
                        dr["Variable3"].ToString().Trim(),
                        dr["Variable4"].ToString().Trim(),
                        dr["Variable5"].ToString().Trim(),
                        dr["Variable6"].ToString().Trim(),
                        dr["Variable7"].ToString().Trim(),
                        dr["Variable8"].ToString().Trim(),
                        dr["Variable9"].ToString().Trim(),
                        dr["Variable10"].ToString().Trim(),
                        dr["Variable11"].ToString().Trim(),
                        dr["Variable12"].ToString().Trim(),
                        dr["Variable13"].ToString().Trim(),
                        dr["Variable14"].ToString().Trim(),
                        dr["Variable15"].ToString().Trim(),
                        //dr["NombreVariable"].ToString().Trim(),
                        //dr["NombreCategoria"].ToString().Trim(),
                        dr["NombreEscala"].ToString().Trim(),
                        dr["NombreMitiga"].ToString().Trim(),
                        dr["DesviacionImpacto"].ToString().Trim(),
                        dr["DesviacionFrecuencia"].ToString().Trim()
                    });
                    }
                    #endregion Recorrido para llenar el Grid

                    GridView7.PageIndex = PagIndexInfoGridReporteControles;
                    GridView7.DataSource = InfoGridReporteControles;
                    GridView7.DataBind();
                }
                else
                {
                    mtdCargarGridReporteControles();
                    Mensaje("No existen registros asociados a los parámetros de consulta.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void mtdLoadInfoGridReporteCausassinControles()
        {
            DataTable dtInfo = new DataTable();
            string ResponsableEjecucion = string.Empty;
            string strErrMsg = string.Empty;
            /*dtInfo = cRiesgo.ReporteRiesgosCausasSinControl(DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(), "---", DDLareas.SelectedValue.ToString().Trim());*/
            clsBLLReporteCausasSinControl cCausassincontrol = new clsBLLReporteCausasSinControl();
            dtInfo = cCausassincontrol.mtdConsultarCausasSinControl(ref strErrMsg, DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(), "---", DDLareas.SelectedValue.ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar el Grid
                foreach (DataRow dr in dtInfo.Rows)
                {
                    InfoGridReporteCausaSinControl.Rows.Add(new Object[] {
                        dr["NombreCausas"].ToString().Trim(),
                        dr["CodigoRiesgo"].ToString().Trim(),
                        dr["NombreRiesgo"].ToString().Trim(),
                        dr["Descripcion"].ToString().Trim(),
                        dr["RiesgoInherente"].ToString().Trim(),
                        dr["RiesgoResidual"].ToString().Trim(),
                        dr["CodigoEvento"].ToString().Trim(),
                        dr["DescripcionEvento"].ToString().Trim(),
                        dr["NombreArea"].ToString().Trim()
                    });

                }
                #endregion Recorrido para llenar el Grid

                GVcausasControl.PageIndex = PagIndexReporteCausaSinControl;
                GVcausasControl.DataSource = InfoGridReporteCausaSinControl;
                GVcausasControl.DataBind();
            }
            else
            {
                mtdCargarGridReporteControles();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }

        }
        #endregion Reporte Controles

        private string mtdBuscarNombresRespEjecucion(string IdResponsableEjecucion)
        {

            try
            {
                string NombresResponsablesEjecucion = string.Empty;
                string[] srtSeparator = new string[] { "|" };
                string[] arrNombres = IdResponsableEjecucion.Split(srtSeparator, StringSplitOptions.None);
                string IdNombre = string.Empty;
                int i = arrNombres.Length;

                if (IdResponsableEjecucion != string.Empty)
                {
                    int a = -1;
                    for (int j = 0; j < i; j++)
                    {
                        //Heber Jessid Correal 05/04/2017 Se valida que tenga 3 o mas caracteres para poder remover 3 caracteres
                        if (arrNombres[j].Length >= 3)
                        {
                            //Heber Jessid Correal 05/04/2017 Se controla que el valor enviado al metodo sea número.
                            if (int.TryParse(arrNombres[j].Remove(0, 3), out a))
                            {
                                if (arrNombres[j].Contains("JO"))
                                    NombresResponsablesEjecucion += cControl.NombreJerarquia(arrNombres[j].Remove(0, 3));
                                else if (arrNombres[j].Contains("GT"))
                                    NombresResponsablesEjecucion += cControl.NombreGrupoTrabajo(arrNombres[j].Remove(0, 3));
                            }
                        }
                    }
                }
                return NombresResponsablesEjecucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteCausaSinControl, Response, "Reporte Causas sin control");
        }

        protected void GVcausasControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexReporteCausaSinControl = e.NewPageIndex;
            GVcausasControl.PageIndex = PagIndexReporteCausaSinControl;
            GVcausasControl.DataSource = InfoGridReporteCausaSinControl;
            GVcausasControl.DataBind();
        }
    }
}