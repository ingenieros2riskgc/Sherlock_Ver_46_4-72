using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
//using Microsoft.Office.Interop.Excel;
using System.Configuration;
using ClosedXML.Excel;
using Microsoft.Security.Application;
using System.Web.Services;

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando.Consolidado
{
    public partial class Consolidado : System.Web.UI.UserControl
    {
        public string ChartLabels = null;
        public string ChartData1 = null;
        public string ChartData2 = null;
        cRiesgo cRiesgo = new cRiesgo();
        string IdFormulario = "5031";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBprocess);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    //mtdInicializarValores();

                    Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                    Chart1.Series["Series1"]["DrawingStyle"] = "Emboss";
                    Chart1.ChartAreas["ChartAreaBar"].Area3DStyle.Enable3D = true;
                    Chart1.Series["Series1"].IsValueShownAsLabel = true;


                }
            }
        }
        private void mtdStard()
        {
            mtdCargarDDLs();
        }
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;
            if (mtdLoadDDLCadenaValor(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (mtdLoadDDLArea(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (mtdLoadDDLClasificacion(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            //mtdLoadDDLMacroProceso(ref strErrMsg);
        }
        #region LoadDDL
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLCadenaValor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsCadenaValorBLL cCadenaValor = new clsCadenaValorBLL();
            #endregion Vars

            try
            {
                lstCadenaValor = cCadenaValor.mtdConsultarCadenaValor(true, ref strErrMsg);
                ddlCadenaValor.Items.Clear();
                ddlCadenaValor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLMacroProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCadenaValor objCadenaValor = new clsCadenaValor();
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                objCadenaValor = new clsCadenaValor(Convert.ToInt32(ddlCadenaValor.SelectedValue.ToString().Trim()), string.Empty, true, 0, string.Empty, string.Empty);
                lstMacroproceso = cMacroproceso.mtdConsultarMacroproceso(true, objCadenaValor, ref strErrMsg);

                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstMacroproceso != null)
                    {
                        int intCounter = 1;


                        foreach (clsMacroproceso objMacroproceso in lstMacroproceso)
                        {
                            ddlMacroproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objMacroproceso.strNombreMacroproceso, objMacroproceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los macroprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {
                objMProceso = new clsMacroproceso(Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()), string.Empty, string.Empty, string.Empty,
                    true, 0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty);
                lstProceso = cProceso.mtdConsultarProceso(true, objMProceso, ref strErrMsg);

                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstProceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsProceso objProceso in lstProceso)
                        {
                            ddlProceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objProceso.strNombreProceso, objProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los subprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLSubproceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsProceso objProceso = new clsProceso();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();
            #endregion Vars

            try
            {
                objProceso = new clsProceso(Convert.ToInt32(ddlProceso.SelectedValue.ToString().Trim()),
                    0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty);
                lstSubproceso = cSubproceso.mtdConsultarSubProceso(true, objProceso, ref strErrMsg);

                ddlSubproceso.Items.Clear();
                ddlSubproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstSubproceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsSubproceso objSubProceso in lstSubproceso)
                        {
                            ddlSubproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objSubProceso.strNombreSubproceso, objSubProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Subprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLArea(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsDTOAreas> lstArea = new List<clsDTOAreas>();
            clsBLLAreas cArea = new clsBLLAreas();
            #endregion Vars

            try
            {
                lstArea = cArea.mtdConsultarAreas(ref strErrMsg);
                ddlAreas.Items.Clear();
                ddlAreas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstArea != null)
                    {
                        int intCounter = 1;

                        foreach (clsDTOAreas objArea in lstArea)
                        {
                            ddlAreas.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objArea.strNombreArea, objArea.intIdArea.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de areas. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }
        private bool mtdLoadDDLClasificacion(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                ddlRiesgoGlobal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlRiesgoGlobal.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                strErrMsg = "Error al cargar clasificación riesgo. " + ex.Message;
                booResult = true;
            }
            return booResult;
        }
        #endregion LoadDDL
        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            /*if (ddlProceso.SelectedValue == "0")
                rfvProceso.Enabled = false;
            else
                rfvProceso.Enabled = true;
                */
            ddlSubproceso.Items.Clear();

            if (mtdLoadDDLSubproceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }


        #endregion

        protected void IBprocess_Click(object sender, ImageClickEventArgs e)
        {
            bool booResult = false;

            //DataTable dt = GetData();
            //LoadChartData(dt);
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue != "---")
            {
                if (ddlTipoReporte.SelectedValue == "2")
                {
                    booResult = mtdLoadReportXYZ(ref strErrMsg);
                    if (booResult == true)
                    {
                        dvChartsReporteXYZ.Visible = true;
                        ImButtonExcelExport.Visible = true;
                        txbTextoExcel.Visible = true;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }
                }
                if (ddlTipoReporte.SelectedValue == "3")
                {
                    if (!cbComparativo.Checked)
                    {
                        booResult = mtdLoadReportNLZ(ref strErrMsg);
                        if (booResult == true)
                        {
                            dvChartsReporteNLK.Visible = true;
                            ImButtonExcelExport.Visible = true;
                            txbTextoExcel.Visible = true;
                        }
                        else
                        {
                            omb.ShowMessage(strErrMsg, 2, "Atención");
                        }
                    }
                    else {
                        booResult = mtdLoadReportNLZ2(ref strErrMsg);
                        if (booResult == true)
                        {
                            dvChartsReporteNLK.Visible = true;
                            ImButtonExcelExport.Visible = true;
                            txbTextoExcel.Visible = true;
                        }
                        else
                        {
                            omb.ShowMessage(strErrMsg, 2, "Atención");
                        }

                    }
                }
                if(ddlTipoReporte.SelectedValue == "1")
                {
                    booResult = mtdLoadReportEvoPerfilRiesgo(ref strErrMsg);
                    if (booResult == true)
                    {
                        dvEvoPerfil.Visible = true;
                        ImButtonExcelExport.Visible = false;
                        txbTextoExcel.Visible = false;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }
                }
                Dbutton.Visible = true;

            }
            else
            {
                omb.ShowMessage("Se debe seleccionar un Tipo de Reporte", 2, "Atención");
                }
        }
        private void LoadChartData(System.Data.DataTable initialDataSource)
        {
            
            for (int i = 1; i < initialDataSource.Columns.Count; i++)
            {
                System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();
                foreach (DataRow dr in initialDataSource.Rows)
                {
                    //series.Legend = dr[0].ToString();
                    int y = (int)dr[i];
                    series.Points.AddXY(dr["Data"].ToString(), y);
                    
                    if (i == 1)
                    {
                        series.Color = System.Drawing.Color.Green;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                        series.BackSecondaryColor = System.Drawing.Color.DarkGreen;
                    }
                    if (i == 2)
                    {
                        series.Color = System.Drawing.Color.Yellow;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                    }
                    if (i == 3)
                    {
                        series.Color = System.Drawing.Color.Orange;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                        series.BackSecondaryColor = System.Drawing.Color.DarkOrange;
                    }
                    if (i == 4)
                    {
                        series.Color = System.Drawing.Color.Red;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                        series.BackSecondaryColor = System.Drawing.Color.DarkRed;
                    }
                    /*Chart1.Series["Series1"].Points[Iteracion].Color = System.Drawing.Color.Green;
                    Chart1.Series["Series1"].Points[Iteracion].BackGradientStyle = GradientStyle.TopBottom;
                    Chart1.Series["Series1"].Points[Iteracion].BackSecondaryColor = System.Drawing.Color.DarkGreen;
                    Chart1.Series["Series1"].Points[Iteracion].Label = dr["Valor"].ToString() + "%";
                    Chart1.Series["Series1"].Points[Iteracion].LabelToolTip = dr["Valor"].ToString() + "%";*/
                }
                /**/
                //Chart3.Legends[0].Enabled = true;
                Chart3.Series.Add(series);
                // Create a new legend called "Legend2".
                //Chart3.Legends.Add(new System.Web.UI.DataVisualization.Charting.Legend("Criticidad"));

                // Set Docking of the Legend chart to the Default Chart Area.
                //Chart3.Legends["Legend2"].DockedToChartArea = "Default";

                // Assign the legend to Series1.
                //Chart3.Series["Series1"].Legend = "Criticidad";
                Chart3.Series["Series1"].IsVisibleInLegend = true;
            }
            
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in Chart3.Series)
            {
                int iteracion = 0;
                foreach (DataPoint point in charts.Points)
                {
                    if(iteracion == 0)
                        point.Label = string.Format("{0}%", Math.Round((point.YValues[0]/Convert.ToInt32(Session["Total Inherente"].ToString())) * 100, 2));
                    else
                        point.Label = string.Format("{0}%", Math.Round((point.YValues[0] / Convert.ToInt32(Session["Total Residual"].ToString())) * 100, 2));
                    iteracion++;
                }
                
            }

        }

        private void LoadChartData2(System.Data.DataTable initialDataSource)
        {

            for (int i = 1; i < initialDataSource.Columns.Count; i++)
            {
                System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();
                foreach (DataRow dr in initialDataSource.Rows)
                {
                    //series.Legend = dr[0].ToString();
                    int y = (int)dr[i];
                    series.Points.AddXY(dr["Data"].ToString(), y);

                    if (i == 1)
                    {
                        series.Color = System.Drawing.Color.Green;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                        series.BackSecondaryColor = System.Drawing.Color.DarkGreen;
                    }
                    if (i == 2)
                    {
                        series.Color = System.Drawing.Color.Yellow;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                    }
                    if (i == 3)
                    {
                        series.Color = System.Drawing.Color.Orange;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                        series.BackSecondaryColor = System.Drawing.Color.DarkOrange;
                    }
                    if (i == 4)
                    {
                        series.Color = System.Drawing.Color.Red;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                        series.BackSecondaryColor = System.Drawing.Color.DarkRed;
                    }
                    /*Chart1.Series["Series1"].Points[Iteracion].Color = System.Drawing.Color.Green;
                    Chart1.Series["Series1"].Points[Iteracion].BackGradientStyle = GradientStyle.TopBottom;
                    Chart1.Series["Series1"].Points[Iteracion].BackSecondaryColor = System.Drawing.Color.DarkGreen;
                    Chart1.Series["Series1"].Points[Iteracion].Label = dr["Valor"].ToString() + "%";
                    Chart1.Series["Series1"].Points[Iteracion].LabelToolTip = dr["Valor"].ToString() + "%";*/
                }
                /**/
                //Chart3.Legends[0].Enabled = true;
                Chart3.Series.Add(series);
                // Create a new legend called "Legend2".
                //Chart3.Legends.Add(new System.Web.UI.DataVisualization.Charting.Legend("Criticidad"));

                // Set Docking of the Legend chart to the Default Chart Area.
                //Chart3.Legends["Legend2"].DockedToChartArea = "Default";

                // Assign the legend to Series1.
                //Chart3.Series["Series1"].Legend = "Criticidad";
                Chart3.Series["Series1"].IsVisibleInLegend = true;
            }

            foreach (System.Web.UI.DataVisualization.Charting.Series charts in Chart3.Series)
            {
                int iteracion = 0;
                foreach (DataPoint point in charts.Points)
                {
                    if ((iteracion == 0) || (iteracion == 2))
                    {
                        point.Label = string.Format("{0}%", Math.Round((point.YValues[0] / Convert.ToInt32(Session["Total Inherente"].ToString())) * 100, 2));
                    }
                    else
                    {
                        point.Label = string.Format("{0}%", Math.Round((point.YValues[0] / Convert.ToInt32(Session["Total Residual"].ToString())) * 100, 2));
                    }
                        //point.Label = string.Format("{0}%", Math.Round((point.YValues[0] / Convert.ToInt32(Session["Total Residual"].ToString())) * 100, 2));
                    iteracion++;
                }

            }

        }
        private void LoadChartDataPerfiles()
        {
            System.Data.DataTable initialDataSource = new System.Data.DataTable();
            initialDataSource.Columns.Add("Data", Type.GetType("System.String"));
            initialDataSource.Columns.Add("Value1", Type.GetType("System.String"));
            DataRow dr1 = initialDataSource.NewRow();
            dr1["Data"] = "Perfil Riesgo Inherente";
            dr1["Value1"] = 100;
            initialDataSource.Rows.Add(dr1);
            DataRow d2 = initialDataSource.NewRow();
            d2["Data"] = "Perfil Riesgo Residual";
            d2["Value1"] = 100;
            initialDataSource.Rows.Add(d2);
            for (int i = 1; i < initialDataSource.Columns.Count; i++)
            {
                System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();
                foreach (DataRow dr in initialDataSource.Rows)
                {
                    //series.Legend = dr[0].ToString();
                    int y = Convert.ToInt32(dr[i].ToString());
                        series.Points.AddXY(dr[0].ToString(), y);
                        series.Color = System.Drawing.Color.Blue;
                        series.BackGradientStyle = GradientStyle.TopBottom;
                        series.BackSecondaryColor = System.Drawing.Color.DarkBlue;
                    
                }
                //Chart4.Legends[0].Enabled = true;
                Chart4.Series.Add(series);
            }
        }
        protected void cbComparativo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbComparativo.Checked)
            {
                trHistoricoInicial.Visible = false;
                trHistoricoInicial1.Visible = true;
                trHistoricoInicial2.Visible = true;
                trHistoricoFinal.Visible = false;
            }
            else
            {
                trHistoricoInicial.Visible = false;
                trHistoricoFinal.Visible = false;
                trHistoricoInicial1.Visible = false;
                trHistoricoInicial2.Visible = false;

                txbFechaInicial.Text = string.Empty;
                txbFechaFinal.Text = string.Empty;
            }
        }
        public bool mtdLoadReportXYZ(ref string strErrMsg)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;
            List<clsDTOCuadroMandoConsolidado> lstReporte = new List<clsDTOCuadroMandoConsolidado>();
            clsBLLCuadroMandoConsolidado cCuadroConsolidado = new clsBLLCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            try
            {
                objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
                objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
                if (ddlMacroproceso.Items.Count > 0)
                    objFiltros.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
                else
                    objFiltros.intIdMacroProceso = 0;
                if (ddlProceso.Items.Count > 0)
                    objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                else
                    objFiltros.intIdProceso = 0;
                if (ddlSubproceso.Items.Count > 0)
                    objFiltros.intIdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
                else
                    objFiltros.intIdSubProceso = 0;
                objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
                if (cbComparativo.Checked)
                {
                    objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                    objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
                }
            }catch(Exception ex)
            {
                omb.ShowMessage("Error captura filtros: " + ex, 2, "Atención");
            }

            /**********************Filtros  de Consulta****************************/
            /*try
            {
                clsDALCuadroMandoConsolidado cCuadro = new clsDALCuadroMandoConsolidado();
                System.Data.DataTable dtInfo = cCuadro.LoadInfoReporteXYZ(ref strErrMgs, objFiltros);
                GridView1.DataSource = dtInfo;
                GridView1.DataBind();
                omb.ShowMessage(strErrMgs, 2, "Atención");
            }
            catch(Exception ex)
            {
                strErrMgs = "Error: " +ex.ToString();
            }*/
            try
            {
                booResult = cCuadroConsolidado.mtdConsultarReporteXYZ(ref strErrMsg, ref lstReporte, objFiltros);
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error en la consulta del reporte: " + ex, 2, "Atención");
            }
            if (booResult == true)
            {
                foreach (clsDTOCuadroMandoConsolidado objCuadro in lstReporte)
                {
                    if (objCuadro.strNombreRiesgo == "Bajo")
                    {
                        valorBajo = valorBajo + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Moderado")
                    {
                        valorModerado = valorModerado + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Alto")
                    {
                        valorAlto = valorAlto + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Extremo")
                    {
                        valorExtremo = valorExtremo + objCuadro.intNumeroRegistros;
                    }
                }
                System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                DataColumn dcColumn;

                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Riesgo Inherente";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Valor";
                dtCuadroMando.Columns.Add(dcColumn);

                DataRow dr;
                dr = dtCuadroMando.NewRow();
                dr["Riesgo Inherente"] = "Bajo";
                dr["Valor"] = valorBajo;
                dtCuadroMando.Rows.Add(dr);
                dr = dtCuadroMando.NewRow();
                dr["Riesgo Inherente"] = "Moderado";
                dr["Valor"] = valorModerado;
                dtCuadroMando.Rows.Add(dr);
                dr = dtCuadroMando.NewRow();
                dr["Riesgo Inherente"] = "Alto";
                dr["Valor"] = valorAlto;
                dtCuadroMando.Rows.Add(dr);
                dr = dtCuadroMando.NewRow();
                dr["Riesgo Inherente"] = "Extremo";
                dr["Valor"] = valorExtremo;
                dtCuadroMando.Rows.Add(dr);

                mtdViewCharts(dtCuadroMando);
            }
            return booResult;
        }
        public void mtdViewCharts(System.Data.DataTable dtInfo)
        {

            try
            {
                /*ChartArea1.BackGradientStyle = GradientStyle.HorizontalCenter;
                            ChartArea1.BackColor = System.Drawing.Color.SlateGray;
                            ChartArea1.BackSecondaryColor = System.Drawing.Color.White;*/
                Chart1.DataSource = dtInfo;
                Chart1.Series["Series1"].XValueMember = "Riesgo Inherente";
                Chart1.Series["Series1"].YValueMembers = "Valor";
                Chart1.DataBind();

                /*Chart1.Series["Series1"].Color = System.Drawing.Color.Blue;
                Chart1.Series["Series1"].BackGradientStyle = GradientStyle.TopBottom;
                Chart1.Series["Series1"].BackSecondaryColor = System.Drawing.Color.DarkBlue;*/
                int Iteracion = 0;
                foreach (DataRow dr in dtInfo.Rows)
                {
                    if (Iteracion == 0)
                    {
                        Chart1.Series["Series1"].Points[Iteracion].Color = System.Drawing.Color.Green;
                        Chart1.Series["Series1"].Points[Iteracion].BackGradientStyle = GradientStyle.TopBottom;
                        Chart1.Series["Series1"].Points[Iteracion].BackSecondaryColor = System.Drawing.Color.DarkGreen;
                        Chart1.Series["Series1"].Points[Iteracion].Label = dr["Valor"].ToString() + "%";
                        Chart1.Series["Series1"].Points[Iteracion].LabelToolTip = dr["Valor"].ToString() + "%";
                    }
                    if (Iteracion == 1)
                    {
                        Chart1.Series["Series1"].Points[Iteracion].Color = System.Drawing.Color.Yellow;
                        Chart1.Series["Series1"].Points[Iteracion].BackGradientStyle = GradientStyle.TopBottom;
                        Chart1.Series["Series1"].Points[Iteracion].Label = dr["Valor"].ToString() + "%";
                        Chart1.Series["Series1"].Points[Iteracion].LabelToolTip = dr["Valor"].ToString() + "%";
                    }
                    if (Iteracion == 2)
                    {
                        Chart1.Series["Series1"].Points[Iteracion].Color = System.Drawing.Color.Orange;
                        Chart1.Series["Series1"].Points[Iteracion].BackGradientStyle = GradientStyle.TopBottom;
                        Chart1.Series["Series1"].Points[Iteracion].BackSecondaryColor = System.Drawing.Color.DarkOrange;
                        Chart1.Series["Series1"].Points[Iteracion].Label = dr["Valor"].ToString() + "%";
                        Chart1.Series["Series1"].Points[Iteracion].LabelToolTip = dr["Valor"].ToString() + "%";
                    }
                    if (Iteracion == 3)
                    {
                        Chart1.Series["Series1"].Points[Iteracion].Color = System.Drawing.Color.Red;
                        Chart1.Series["Series1"].Points[Iteracion].BackGradientStyle = GradientStyle.TopBottom;
                        Chart1.Series["Series1"].Points[Iteracion].BackSecondaryColor = System.Drawing.Color.DarkRed;
                        Chart1.Series["Series1"].Points[Iteracion].Label = dr["Valor"].ToString() + "%";
                        Chart1.Series["Series1"].Points[Iteracion].LabelToolTip = dr["Valor"].ToString() + "%";
                    }
                    Iteracion++;
                }
                Chart1.Series["Series1"].SmartLabelStyle.Enabled = true;
                // Set series chart type
                Chart1.Series["Series1"].ChartType = SeriesChartType.Bar;

                // Set series point width
                Chart1.Series["Series1"]["PointWidth"] = "0.6";

                // Show chart with right-angled axes
                Chart1.ChartAreas["ChartAreaBar"].Area3DStyle.IsRightAngleAxes = true;

                // Show columns as clustered
                Chart1.ChartAreas["ChartAreaBar"].Area3DStyle.IsClustered = false;

                // Show X axis end labels
                Chart1.ChartAreas["ChartAreaBar"].AxisX.LabelStyle.IsEndLabelVisible = true;

                // Set rotational angles
                Chart1.ChartAreas["ChartAreaBar"].Area3DStyle.Inclination = 30;
                Chart1.ChartAreas["ChartAreaBar"].Area3DStyle.Inclination = 30;
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la generación del grafico: " + ex, 2, "Atención");
            }
            

        }
        public bool mtdLoadReportNLZ(ref string strErrMsg)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dtPerfiles = new System.Data.DataTable();
            dt.Columns.Add("Data", Type.GetType("System.String"));
            dt.Columns.Add("Value1", Type.GetType("System.Int32"));
            dt.Columns.Add("Value2", Type.GetType("System.Int32"));
            dt.Columns.Add("Value3", Type.GetType("System.Int32"));
            dt.Columns.Add("Value4", Type.GetType("System.Int32"));
            dtPerfiles.Columns.Add("Data", Type.GetType("System.String"));
            dtPerfiles.Columns.Add("Value1", Type.GetType("System.String"));
            System.Data.DataTable dtCriticidad = new System.Data.DataTable();
            dtCriticidad.Columns.Add("Titulo", Type.GetType("System.String"));
            dtCriticidad.Columns.Add("Serie1", Type.GetType("System.String"));
            dtCriticidad.Columns.Add("Serie2", Type.GetType("System.String"));
            dtCriticidad.Columns.Add("Serie3", Type.GetType("System.String"));
            dtCriticidad.Columns.Add("Serie4", Type.GetType("System.String"));
            DataRow RowCriticidad = dtCriticidad.NewRow();
            RowCriticidad["Titulo"] = "Criticidad de Riesgos";
            RowCriticidad["Serie1"] = "Bajo";
            RowCriticidad["Serie2"] = "Moderado";
            RowCriticidad["Serie3"] = "Alto";
            RowCriticidad["Serie4"] = "Extremo";
            dtCriticidad.Rows.Add(RowCriticidad);
            GVcriticidad.DataSource = dtCriticidad;
            GVcriticidad.DataBind();
            GVcriticidad.Visible = true;
            List<clsDTOCuadroMandoConsolidado> lstReporte = new List<clsDTOCuadroMandoConsolidado>();
            List<clsDTOCuadroMandoConsolidado> lstReporteNLK = new List<clsDTOCuadroMandoConsolidado>();
            List<clsDTOCuadroMandoConsolidado> lstReporteNLKPerilRI = new List<clsDTOCuadroMandoConsolidado>();
            List<clsDTOCuadroMandoConsolidado> lstReporteNLKPerilRR = new List<clsDTOCuadroMandoConsolidado>();
            clsBLLCuadroMandoConsolidado cCuadroConsolidado = new clsBLLCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            if (ddlMacroproceso.Items.Count > 0)
                objFiltros.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            else
                objFiltros.intIdMacroProceso = 0;
            if (ddlProceso.Items.Count > 0)
                objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            else
                objFiltros.intIdProceso = 0;
            if (ddlSubproceso.Items.Count > 0)
                objFiltros.intIdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            else
                objFiltros.intIdSubProceso = 0;
            objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            /**********************Filtros  de Consulta****************************/
            

            booResult = cCuadroConsolidado.mtdConsultarReporteNLK(ref strErrMsg, ref lstReporteNLK, objFiltros);
            if (booResult == true)
            {
                foreach (clsDTOCuadroMandoConsolidado objCuadro in lstReporteNLK)
                {
                    if (objCuadro.strNombreRiesgo == "Bajo")
                    {
                        valorBajo = valorBajo + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Moderado")
                    {
                        valorModerado = valorModerado + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Alto")
                    {
                        valorAlto = valorAlto + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Extremo")
                    {
                        valorExtremo = valorExtremo + objCuadro.intNumeroRegistros;
                    }
                }
                int total = valorBajo + valorModerado + valorAlto + valorExtremo;
                Session["Total Inherente"] = total;
                DataRow dr2 = dt.NewRow();
                dr2["Data"] = "Riesgo Inherente";
                dr2["Value1"] = valorBajo;
                dr2["Value2"] = valorModerado;
                dr2["Value3"] = valorAlto;
                dr2["Value4"] = valorExtremo;

                dt.Rows.Add(dr2);

            }
            valorBajo = 0;
            valorModerado = 0;
            valorAlto = 0;
            valorExtremo = 0;
            booResult = cCuadroConsolidado.mtdConsultarReporteXYZ(ref strErrMsg, ref lstReporte, objFiltros);
            System.Data.DataTable dtCuadroMandoNLK = new System.Data.DataTable();
            System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
            if (booResult == true)
            {
                foreach (clsDTOCuadroMandoConsolidado objCuadro in lstReporte)
                {
                    if (objCuadro.strNombreRiesgo == "Bajo")
                    {
                        valorBajo = valorBajo + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Moderado")
                    {
                        valorModerado = valorModerado + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Alto")
                    {
                        valorAlto = valorAlto + objCuadro.intNumeroRegistros;
                    }
                    if (objCuadro.strNombreRiesgo == "Extremo")
                    {
                        valorExtremo = valorExtremo + objCuadro.intNumeroRegistros;
                    }
                }
                int total = valorBajo + valorModerado + valorAlto + valorExtremo;
                Session["Total Residual"] = total;
                DataRow dr1 = dt.NewRow();
                dr1["Data"] = "Riesgo Residual";
                dr1["Value1"] = valorBajo;
                dr1["Value2"] = valorModerado;
                dr1["Value3"] = valorAlto;
                dr1["Value4"] = valorExtremo;
                dt.Rows.Add(dr1);

            }
            
            //desde aqui
            //booResult = cCuadroConsolidado.mtdConsultarReporteNLKPerfilRI(ref strErrMsg, ref lstReporteNLKPerilRI);
            //if(booResult == true)
            //{
            //    DataRow dr1 = dtPerfiles.NewRow();
            //    foreach (clsDTOCuadroMandoConsolidado objCuadro in lstReporteNLKPerilRI)
            //    {
                    
            //        dr1["Data"] = "Perfil Riesgo Inherente";
            //        dr1["Value1"] = objCuadro.intNumeroRegistros;
            //    }
            //    dtPerfiles.Rows.Add(dr1);
            //}
            //booResult = cCuadroConsolidado.mtdConsultarReporteNLKPerfilRR(ref strErrMsg, ref lstReporteNLKPerilRR);
            //if (booResult == true)
            //{
            //    DataRow dr2 = dtPerfiles.NewRow();
            //    foreach (clsDTOCuadroMandoConsolidado objCuadro in lstReporteNLKPerilRR)
            //    {
                    
            //        dr2["Data"] = "Perfil Riesgo Residual";
            //        dr2["Value1"] = objCuadro.intNumeroRegistros;
            //    }
            //    dtPerfiles.Rows.Add(dr2);
            //}
            //aqui
           // mtdViewChartsReporteNLK(ds);
            LoadChartData(dt);
            // LoadChartDataPerfiles();
            Chart4.Visible = false;
            return booResult;
        }

        public bool mtdLoadReportNLZ2(ref string strErrMsg)
        {
            bool booResult = false;
            System.Data.DataTable dt = new System.Data.DataTable(); ;
            System.Data.DataTable dtPerfiles = new System.Data.DataTable();
            dt.Columns.Add("Data", Type.GetType("System.String"));
            dt.Columns.Add("Value1", Type.GetType("System.Int32"));
            dt.Columns.Add("Value2", Type.GetType("System.Int32"));
            dt.Columns.Add("Value3", Type.GetType("System.Int32"));
            dt.Columns.Add("Value4", Type.GetType("System.Int32"));
            dtPerfiles.Columns.Add("Data", Type.GetType("System.String"));
            dtPerfiles.Columns.Add("Value1", Type.GetType("System.String"));
            System.Data.DataTable dtCriticidad = new System.Data.DataTable();
            dtCriticidad.Columns.Add("Titulo", Type.GetType("System.String"));
            dtCriticidad.Columns.Add("Serie1", Type.GetType("System.String"));
            dtCriticidad.Columns.Add("Serie2", Type.GetType("System.String"));
            dtCriticidad.Columns.Add("Serie3", Type.GetType("System.String"));
            dtCriticidad.Columns.Add("Serie4", Type.GetType("System.String"));
            DataRow RowCriticidad = dtCriticidad.NewRow();
            RowCriticidad["Titulo"] = "Criticidad de Riesgos";
            RowCriticidad["Serie1"] = "Bajo";
            RowCriticidad["Serie2"] = "Moderado";
            RowCriticidad["Serie3"] = "Alto";
            RowCriticidad["Serie4"] = "Extremo";
            dtCriticidad.Rows.Add(RowCriticidad);
            GVcriticidad.DataSource = dtCriticidad;
            GVcriticidad.DataBind();
            GVcriticidad.Visible = true;
            for (int i=1;i<3;i++)
            {
               
                //string strErrMgs = string.Empty;
                //string NombreRiesgoInherente = string.Empty;
                int valorBajo = 0;
                int valorModerado = 0;
                int valorAlto = 0;
                int valorExtremo = 0;
                
                
                List<clsDTOCuadroMandoConsolidado> lstReporte = new List<clsDTOCuadroMandoConsolidado>();
                List<clsDTOCuadroMandoConsolidado> lstReporteNLK = new List<clsDTOCuadroMandoConsolidado>();
                List<clsDTOCuadroMandoConsolidado> lstReporteNLKPerilRI = new List<clsDTOCuadroMandoConsolidado>();
                List<clsDTOCuadroMandoConsolidado> lstReporteNLKPerilRR = new List<clsDTOCuadroMandoConsolidado>();
                clsBLLCuadroMandoConsolidado cCuadroConsolidado = new clsBLLCuadroMandoConsolidado();
                clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
                clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
                /**********************Filtros de Consulta****************************/
                objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
                objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
                if (ddlMacroproceso.Items.Count > 0)
                    objFiltros.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
                else
                    objFiltros.intIdMacroProceso = 0;
                if (ddlProceso.Items.Count > 0)
                    objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                else
                    objFiltros.intIdProceso = 0;
                if (ddlSubproceso.Items.Count > 0)
                    objFiltros.intIdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
                else
                    objFiltros.intIdSubProceso = 0;
                objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);

                if (i == 1)
                {
                    string auxFechaHistoricoInicial = YearIni.SelectedValue.ToString() + "-" + MesIni.SelectedValue.ToString() + "-01";

                    string Auxdias = Convert.ToString(DateTime.DaysInMonth(Convert.ToInt32(YearIni.SelectedValue), Convert.ToInt32(MesIni.SelectedValue)));
                    string auxFechaHistoricoFinal = YearIni.SelectedValue.ToString() + "-" + MesIni.SelectedValue.ToString() + "-" + Auxdias;
                    objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(auxFechaHistoricoInicial);
                    objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(auxFechaHistoricoFinal);

                }
                else
                {
                    string auxFechaHistoricoInicial = yearFin.SelectedValue.ToString() + "-" + MesFin.SelectedValue.ToString() + "-01";

                    string Auxdias = Convert.ToString(DateTime.DaysInMonth(Convert.ToInt32(yearFin.SelectedValue), Convert.ToInt32(MesFin.SelectedValue)));
                    string auxFechaHistoricoFinal = yearFin.SelectedValue.ToString() + "-" + MesFin.SelectedValue.ToString() + "-" + Auxdias;
                    objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(auxFechaHistoricoInicial);
                    objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(auxFechaHistoricoFinal);

                }

                booResult = cCuadroConsolidado.mtdConsultarReporteNLK(ref strErrMsg, ref lstReporteNLK, objFiltros);
                if (booResult == true)
                {
                    foreach (clsDTOCuadroMandoConsolidado objCuadro in lstReporteNLK)
                    {
                        if (objCuadro.strNombreRiesgo == "Bajo")
                        {
                            valorBajo = valorBajo + objCuadro.intNumeroRegistros;
                        }
                        if (objCuadro.strNombreRiesgo == "Moderado")
                        {
                            valorModerado = valorModerado + objCuadro.intNumeroRegistros;
                        }
                        if (objCuadro.strNombreRiesgo == "Alto")
                        {
                            valorAlto = valorAlto + objCuadro.intNumeroRegistros;
                        }
                        if (objCuadro.strNombreRiesgo == "Extremo")
                        {
                            valorExtremo = valorExtremo + objCuadro.intNumeroRegistros;
                        }
                    }
                    int total = valorBajo + valorModerado + valorAlto + valorExtremo;
                    Session["Total Inherente"] = total;
                    DataRow dr2 = dt.NewRow();
                    if (i == 1)
                    {
                        dr2["Data"] = "Riesgo Inherente" + " año " + YearIni.SelectedValue.ToString() + " mes " + MesIni.SelectedValue.ToString();
                    }
                    else
                    {
                        dr2["Data"] = "Riesgo Inherente" + " año " + yearFin.SelectedValue.ToString() + " mes " + MesFin.SelectedValue.ToString();
                    }
                   
                    dr2["Value1"] = valorBajo;
                    dr2["Value2"] = valorModerado;
                    dr2["Value3"] = valorAlto;
                    dr2["Value4"] = valorExtremo;

                    dt.Rows.Add(dr2);
                    valorBajo = 0;
                    valorModerado = 0;
                    valorAlto = 0;
                    valorExtremo = 0;

                }
                valorBajo = 0;
                valorModerado = 0;
                valorAlto = 0;
                valorExtremo = 0;
                booResult = cCuadroConsolidado.mtdConsultarReporteXYZ(ref strErrMsg, ref lstReporte, objFiltros);
                System.Data.DataTable dtCuadroMandoNLK = new System.Data.DataTable();
                System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                if (booResult == true)
                {
                    foreach (clsDTOCuadroMandoConsolidado objCuadro in lstReporte)
                    {
                        if (objCuadro.strNombreRiesgo == "Bajo")
                        {
                            valorBajo = valorBajo + objCuadro.intNumeroRegistros;
                        }
                        if (objCuadro.strNombreRiesgo == "Moderado")
                        {
                            valorModerado = valorModerado + objCuadro.intNumeroRegistros;
                        }
                        if (objCuadro.strNombreRiesgo == "Alto")
                        {
                            valorAlto = valorAlto + objCuadro.intNumeroRegistros;
                        }
                        if (objCuadro.strNombreRiesgo == "Extremo")
                        {
                            valorExtremo = valorExtremo + objCuadro.intNumeroRegistros;
                        }
                    }
                    
                    int total = valorBajo + valorModerado + valorAlto + valorExtremo;
                    Session["Total Residual"] = total;
                    DataRow dr1 = dt.NewRow();
                    if (i == 1)
                    {
                        dr1["Data"] = "Riesgo Residual" + " año " + YearIni.SelectedValue.ToString() + " mes " + MesIni.SelectedValue.ToString();
                    }
                    else
                    {
                        dr1["Data"] = "Riesgo Residual" + " año " + yearFin.SelectedValue.ToString() + " mes " + MesFin.SelectedValue.ToString();
                    }
                    //dr1["Data"] = "Riesgo Residual";
                    dr1["Value1"] = valorBajo;
                    dr1["Value2"] = valorModerado;
                    dr1["Value3"] = valorAlto;
                    dr1["Value4"] = valorExtremo;
                    dt.Rows.Add(dr1);
                    valorBajo = 0;
                    valorModerado = 0;
                    valorAlto = 0;
                    valorExtremo = 0;
                }
            }

            LoadChartData2(dt);
            // LoadChartDataPerfiles();
            Chart4.Visible = false;
            return booResult;
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue == "2")
                mtdLoadReportXYZ(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "3")
                mtdLoadReportNLZ(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "1")
                mtdGrafico();
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Cuadro de Mando Consolidado");
            //....header
            // we Add a Header that will show up on PAGE 1
            // Creamos la imagen y le ajustamos el tamaño
            string pathImg = Server.MapPath("~") + "Imagenes/Logos/Risk.png";
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(pathImg);
            pathImg = Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
            iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(pathImg);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            PdfPTable pdftblImage = new PdfPTable(2);
            PdfPCell pdfcellImage = new PdfPCell(imagen, true);
            pdfcellImage.FixedHeight = 40f;
            /*pdfcellImage.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcellImage.VerticalAlignment = Element.ALIGN_LEFT;*/
            pdfcellImage.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdfcellImage.Border = iTextSharp.text.Rectangle.NO_BORDER;
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = iTextSharp.text.Rectangle.NO_BORDER;
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();

            phHeader.Add(pdftblImage);
            #region Tabla de Datos Principales
            iTextSharp.text.Font font1 = new iTextSharp.text.Font();
            font1.Color = Color.WHITE;
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Tipo Reporte:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlTipoReporte.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            if (ddlRiesgoGlobal.SelectedValue != "0" && ddlRiesgoGlobal.SelectedValue != "")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Riesgo Global:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlRiesgoGlobal.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Riesgo Global:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Todos"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if (ddlCadenaValor.SelectedValue != "0" && ddlCadenaValor.SelectedValue != "")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Cadena Valor:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlCadenaValor.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Cadena Valor:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Todos"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            
            if (ddlMacroproceso.SelectedItem != null)
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Macroproceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlMacroproceso.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Macroproceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Todos"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if (ddlProceso.SelectedItem != null)
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Proceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlProceso.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Proceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Todos"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if (ddlSubproceso.SelectedItem != null)
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Subproceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlSubproceso.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Subproceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Todos"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if(ddlAreas.SelectedValue != "0" && ddlAreas.SelectedValue != "")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Área:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlAreas.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Área:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Todos"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if(ddlTipoReporte.SelectedValue == "1")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Inicial de la Evoluación del perfil:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(txbPerfilInicial.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Final de la Evoluación del perfil:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(txbPerfilFinal.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            pdfCellEncabezado = new PdfPCell(new Phrase("", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(""));
            pdfTableData.AddCell(pdfCellEncabezado);

            PdfPTable pdfTableComparativo = new PdfPTable(2);
            if (cbComparativo.Checked)
            {

                pdfCellEncabezado = new PdfPCell(new Phrase("Comparativo:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Esta seleccionado como tal"));
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Inicio:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(txbFechaInicial.Text));
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Final:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(txbFechaFinal.Text));
                pdfTableComparativo.AddCell(pdfCellEncabezado);
            }
            PdfPTable pdfTableFecha = new PdfPTable(2);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Generación del reporte:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableFecha.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(DateTime.Now.ToString()));
            pdfTableFecha.AddCell(pdfCellEncabezado);
            #endregion
            MemoryStream stream = new MemoryStream();
            MemoryStream stream2 = new MemoryStream();
            if (ddlTipoReporte.SelectedValue == "2")
                Chart1.SaveImage(stream, ChartImageFormat.Png);
            if(ddlTipoReporte.SelectedValue == "3")
            {
                Chart3.SaveImage(stream, ChartImageFormat.Png);
                Chart4.SaveImage(stream2, ChartImageFormat.Png);
            }
            MemoryStream stream3 = new MemoryStream();
            if (ddlTipoReporte.SelectedValue == "1")
                Chart2.SaveImage(stream3, ChartImageFormat.Png);
            PdfPTable pdftblImageGrafico = new PdfPTable(1);
            PdfPTable pdftblImageGrafico2 = new PdfPTable(1);
            PdfPTable pdftblImageGrafico3 = new PdfPTable(1);
            if (ddlTipoReporte.SelectedValue == "2" || ddlTipoReporte.SelectedValue == "3")
            {
                iTextSharp.text.Image imagenGrafico = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                
                PdfPCell pdfcellImageGrafico = new PdfPCell(imagenGrafico, true);
                pdftblImageGrafico.AddCell(pdfcellImageGrafico);
            }
            if (ddlTipoReporte.SelectedValue == "3")
            {
                iTextSharp.text.Image imagenGrafico2 = iTextSharp.text.Image.GetInstance(stream2.GetBuffer());
               
                PdfPCell pdfcellImageGrafico2 = new PdfPCell(imagenGrafico2, true);
                pdftblImageGrafico2.AddCell(pdfcellImageGrafico2);
            }
            if (ddlTipoReporte.SelectedValue == "1")
            {
                iTextSharp.text.Image imagenGrafico3 = iTextSharp.text.Image.GetInstance(stream3.GetBuffer());
                
                PdfPCell pdfcellImageGrafico3 = new PdfPCell(imagenGrafico3, true);
                pdftblImageGrafico3.AddCell(pdfcellImageGrafico3);
            }
            /*PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            using (MemoryStream stream = new MemoryStream())
            {
                Chart1.SaveImage(stream, ChartImageFormat.Png);
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                chartImage.ScalePercent(75f);
                pdfDoc.Add(chartImage);
                pdfDoc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Chart.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }*/
            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase(ddlTipoReporte.SelectedItem.Text));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableFecha);
            pdfDocument.Add(Chunk.NEWLINE);
            if (cbComparativo.Checked)
            {
                pdfDocument.Add(pdfTableComparativo);
                pdfDocument.Add(Chunk.NEWLINE);
            }
            if (ddlTipoReporte.SelectedValue == "2" || ddlTipoReporte.SelectedValue == "3")
            {
                pdfDocument.Add(pdftblImageGrafico);
            }
            if (ddlTipoReporte.SelectedValue == "3")
            {
                pdfDocument.Add(pdftblImageGrafico2);
                pdfDocument.Add(Chunk.NEWLINE);
            }
            if (ddlTipoReporte.SelectedValue == "1")
            {
                pdfDocument.Add(pdftblImageGrafico3);
                pdfDocument.Add(Chunk.NEWLINE);
            }
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=CuadroMandoConsolidado.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "Reporte"+ddlTipoReporte.SelectedItem.Text+"_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }

        protected void exportExcel(HttpResponse Response, string filename)
        {
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue == "2")
                mtdLoadReportXYZ(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "3")
                mtdLoadReportNLZ(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "1")
                mtdGrafico();
            string tmpChartName = "Grafico" + ddlTipoReporte.SelectedValue + ".jpg";
            string tmpChartName2 = "Grafico" + ddlTipoReporte.SelectedValue + "2.jpg";
            string tmpChartName3 = "Grafico" + ddlTipoReporte.SelectedValue + "3.jpg";
            string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;
            string imgPath3 = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName2;
            string imgPath6 = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName3;
            
            if (ddlTipoReporte.SelectedValue == "2")
                Chart1.SaveImage(imgPath);
            if(ddlTipoReporte.SelectedValue == "3")
            {
                Chart3.SaveImage(imgPath);
                Chart4.SaveImage(imgPath3);
            }
            if (ddlTipoReporte.SelectedValue == "1")
                Chart2.SaveImage(imgPath6);
            MemoryStream stream3 = new MemoryStream();
            if (ddlTipoReporte.SelectedValue == "1")
                Chart2.SaveImage(stream3, ChartImageFormat.Png);
            string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);
            string imgPath4 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName2);
            string imgPath5 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName3);
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("Tipo reporte:");
            gridEncabezado.Columns.Add("Riesgo Global:");
            gridEncabezado.Columns.Add("Cadena Valor:");
            gridEncabezado.Columns.Add("Macroproceso:");
            gridEncabezado.Columns.Add("Proceso:");
            gridEncabezado.Columns.Add("Subproceso:");
            gridEncabezado.Columns.Add("Área:");
            if(ddlTipoReporte.SelectedValue == "1")
            {
                gridEncabezado.Columns.Add("Fecha Inicial de la Evoluación del perfil:");
                gridEncabezado.Columns.Add("Fecha Final de la Evoluación del perfil:");
            }
            if (cbComparativo.Checked)
            {
                gridEncabezado.Columns.Add("Fecha Inicial:");
                gridEncabezado.Columns.Add("Fecha Final:");
            }

            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Tipo reporte:"] = ddlTipoReporte.SelectedItem.Text;
            rowEncabezado["Riesgo Global:"] = ddlRiesgoGlobal.SelectedItem.Text;
            if(ddlCadenaValor.SelectedValue != "0" && ddlCadenaValor.SelectedValue != "")
            {
                rowEncabezado["Cadena Valor:"] = ddlCadenaValor.SelectedItem.Text;
            }else
            {
                rowEncabezado["Cadena Valor:"] = "Todos";
            }
            if (ddlMacroproceso.SelectedItem != null)
            {
                rowEncabezado["Macroproceso:"] = ddlCadenaValor.SelectedItem.Text;
            }
            else
            {
                rowEncabezado["Macroproceso:"] = "Todos";
            }
            if (ddlProceso.SelectedItem != null)
                rowEncabezado["Proceso:"] = ddlProceso.SelectedItem.Text;
            else
                rowEncabezado["Proceso:"] = "Todos";
            if (ddlSubproceso.SelectedItem != null)
                rowEncabezado["Subproceso:"] = ddlSubproceso.SelectedItem.Text;
            else
                rowEncabezado["Subproceso:"] = "Todos";
            if(ddlAreas.SelectedValue != "0" && ddlAreas.SelectedValue != "")
            {
                rowEncabezado["Área:"] = ddlAreas.SelectedItem.Text;
            }else
            {
                rowEncabezado["Área:"] = "Todos";
            }
            if (ddlTipoReporte.SelectedValue == "1")
            {
                rowEncabezado["Fecha Inicial de la Evoluación del perfil:"] = txbPerfilInicial.Text;
                rowEncabezado["Fecha Final de la Evoluación del perfil:"] = txbPerfilFinal.Text;
            }
                if (cbComparativo.Checked)
            {
                rowEncabezado["Fecha Inicial:"] = txbFechaInicial.Text;
                rowEncabezado["Fecha Final:"] = txbFechaFinal.Text;
            }
            gridEncabezado.Rows.Add(rowEncabezado);
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=CuadroMandoConsolidado.xls;");
            string tab = "";
            foreach (DataColumn dc in gridEncabezado.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in gridEncabezado.Rows)
            {
                tab = "";
                for (i = 0; i < gridEncabezado.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            if(ddlTipoReporte.SelectedValue == "2")
            {
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTable = @"<Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table>";
                Response.Write(headerTable);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
            }
            
            if (ddlTipoReporte.SelectedValue == "3")
            {
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTable = @"<Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table>";
                Response.Write(headerTable);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
                StringWriter stringWrite2 = new StringWriter();
                HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
                string headerTable2 = @"<Table><tr><td><img src='" + imgPath4 + @"' \></td></tr></Table>";
                Response.Write(headerTable2);
                Response.Write(stringWrite2.ToString());
            }
            if(ddlTipoReporte.SelectedValue == "1")
            {
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();

                img.ImageUrl = imgPath6;
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        //Create a Table.
                        System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();

                        //Add Image control to the Table Cell.
                        TableRow row = new TableRow();
                        row.Cells.Add(new TableCell());
                        row.Cells[0].Controls.Add(img);
                        table.Rows.Add(row);

                        //Render the Table as HTML.
                        table.RenderControl(hw);

                        //Export the Table to Excel.
                        //Response.Clear();
                        Response.Buffer = true;
                        //Response.AddHeader("content-disposition", "attachment;filename=Images.xls");
                        Response.Charset = "";
                        //Response.ContentType = "application/vnd.ms-excel";

                        //Write the HTML string to Response.
                        Response.Write(sw.ToString());
                        Response.Flush();
                        //Response.End();
                    }
                }
                /*string headerTable = @"<Table><tr><td><img src='" + imgPath6 + @"' \></td></tr></Table>";
                Response.Write(headerTable);
                Response.Write(stringWrite.ToString());*/
                /*Response.Write("\n");
                Response.Write("\n");*/
            }
            Response.End();
            
        }
        public bool mtdLoadReportEvoPerfilRiesgo(ref string strErrMsg)
        {
            string[] strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            System.Data.DataTable dtMes = new System.Data.DataTable();
            dtMes.Columns.Add("Serie");
            dtMes.Columns.Add("Mes");
            DataRow row;
            
            String promedioProbabilidad = "";
            String promedioImpacto = "";
            string severidad = string.Empty;
            string color = string.Empty;
            LinkButton coordenada = new LinkButton();
            Panel scoordenadaPanel = new Panel();
            bool booResult = false;
            //string strErrMsg = string.Empty;
            List<clsDTOCuadroMandoConsolidado> lstReporte = new List<clsDTOCuadroMandoConsolidado>();
            clsBLLCuadroMandoConsolidado cCuadroConsolidado = new clsBLLCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            if(txbPerfilInicial.Text != "")
                objFiltros.dtFechaEvoPerfilInicial = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbPerfilInicial.Text));
            if(txbPerfilFinal.Text != "")
                objFiltros.dtFechaEvoPerfilFinal = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbPerfilFinal.Text));
            /**********************Filtros de Consulta****************************/
            objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            if (ddlMacroproceso.Items.Count > 0)
                objFiltros.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            else
                objFiltros.intIdMacroProceso = 0;
            if (ddlProceso.Items.Count > 0)
                objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            else
                objFiltros.intIdProceso = 0;
            if (ddlSubproceso.Items.Count > 0)
                objFiltros.intIdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            else
                objFiltros.intIdSubProceso = 0;
            objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            booResult = cCuadroConsolidado.LoadInfoEvoPerfilRiesgo(ref strErrMsg, ref lstReporte, objFiltros);
            if(booResult == true)
            {
                int Iteracion = objFiltros.dtFechaEvoPerfilInicial.Month -1;
                int points = 0;
                int iteracion=0;
                Regex rex = new Regex("^[0-9]*$");
                foreach (clsDTOCuadroMandoConsolidado objReporte in lstReporte)
                {
                    promedioProbabilidad = Math.Round((Convert.ToDouble(objReporte.intSumatoriaProbabilidad)) / (Convert.ToDouble(objReporte.intNumeroRegistros))).ToString().Trim();
                    if ((promedioProbabilidad == "NaN")||(promedioProbabilidad == "NeuN"))
                        promedioProbabilidad = "0";
                    promedioImpacto = Math.Round((Convert.ToDouble(objReporte.intSumatoriaImpacto)) / (Convert.ToDouble(objReporte.intNumeroRegistros))).ToString().Trim();
                    if ((promedioImpacto == "NaN")||(promedioImpacto == "NeuN"))
                        promedioImpacto = "0";
                    coordenada = (LinkButton)FindControl("LBt" + cRiesgo.IdProbabilidad(promedioProbabilidad) + cRiesgo.IdImpacto(promedioImpacto));//
                    row = dtMes.NewRow();
                    if(coordenada != null)
                    {
                        if (coordenada.Text == "")
                        {
                            iteracion = 1;
                            coordenada.Text = strMeses[Iteracion].ToString();

                            coordenada.CssClass = "buttonClass";
                        }
                        else
                        {
                            if (rex.IsMatch(coordenada.Text) == true)
                            {
                                int NumCoordenada = Convert.ToInt32(coordenada.Text);
                                NumCoordenada++;
                                row["Serie"] = Iteracion;
                                row["Mes"] = strMeses[Iteracion].ToString();
                                coordenada.Text = NumCoordenada.ToString();
                            }
                            else
                            {
                                row["Serie"] = Iteracion - 1;
                                row["Mes"] = coordenada.Text;
                                dtMes.Rows.Add(row);
                                row = dtMes.NewRow();
                                row["Serie"] = Iteracion;
                                row["Mes"] = strMeses[Iteracion].ToString();
                                iteracion++;
                                coordenada.Text = iteracion.ToString();

                            }

                            dtMes.Rows.Add(row);
                        }
                    }
                    

                    /*if (coordenada != null)
                    {
                        //coordenada.CssClass = "buttonClassNumber";

                        //int mesPosicion = objFiltros.dtFechaEvoPerfilInicial.Month - 1;
                        coordenada.Text += "-"+strMeses[Iteracion].ToString();
                        coordenada.CssClass = "buttonClass";
                    }*
                    
                    /*for (int pointIndex = 0; pointIndex < lstReporte.Count; pointIndex++)
                    {*/
                    cCuadroConsolidado.mtdGetSeveridad(ref strErrMsg, ref severidad, ref color, promedioProbabilidad, promedioImpacto);
                    //Chart2.Series["Series1"].Points.AddY(promedioProbabilidad);
                    Chart2.Series["Series1"].Points.AddXY(promedioProbabilidad, promedioImpacto);

                    //Chart1.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                    Chart2.Series["Series1"].LegendText = "Evolución Perfil de Riesgo";
                    Chart2.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
                    // Set marker size
                    Chart2.Series["Series1"].MarkerSize = 20;
                    if (color == "")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Green;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].BackSecondaryColor = System.Drawing.Color.Blue;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    if (color == "Green")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Green;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].BackSecondaryColor = System.Drawing.Color.DarkGreen;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    if (color == "Yellow")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Yellow;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    if (color == "Orange")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Orange;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].BackSecondaryColor = System.Drawing.Color.DarkOrange;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    if (color == "Red")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Red;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].BackSecondaryColor = System.Drawing.Color.DarkRed;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    //Chart2.Series["Series2"].Points.AddY(promedioImpIBprocess_Click
                    //acto);
                    //}

                    points++;
                    Iteracion++;
                }
                Chart2.ChartAreas["ChartArea1"].AxisX.Title = "Propabilidad";
                Chart2.ChartAreas["ChartArea1"].AxisY.Title = "Impacto";
                // Set series chart type
                Chart2.Series["Series1"].ChartType = SeriesChartType.Spline;


                // Set point labels
                Chart2.Series["Series1"].IsValueShownAsLabel = true;

                // Enable X axis margin
                Chart2.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

                // Enable 3D, and show data point marker lines
                Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart2.Series["Series1"]["ShowMarkerLines"] = "True";
                
                dvLineChart.Visible = true;
                if(dtMes.Rows.Count > 0)
                {
                    GVmeses.DataSource = dtMes;
                    GVmeses.DataBind();
                }
            }
            return booResult;
        }
        public void mtdTableMes(string coorPro, string coorImp)
        {
            string[] strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            System.Data.DataTable dtMes = new System.Data.DataTable();
            dtMes.Columns.Add("Serie");
            dtMes.Columns.Add("Mes");
            DataRow row;
            string strErrMsg = string.Empty;
            String promedioProbabilidad = "";
            String promedioImpacto = "";
            string severidad = string.Empty;
            string color = string.Empty;
            LinkButton coordenada = new LinkButton();
            Panel scoordenadaPanel = new Panel();
            bool booResult = false;
            //string strErrMsg = string.Empty;
            List<clsDTOCuadroMandoConsolidado> lstReporte = new List<clsDTOCuadroMandoConsolidado>();
            clsBLLCuadroMandoConsolidado cCuadroConsolidado = new clsBLLCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            if (txbPerfilInicial.Text != "")
                objFiltros.dtFechaEvoPerfilInicial = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbPerfilInicial.Text));
            if (txbPerfilFinal.Text != "")
                objFiltros.dtFechaEvoPerfilFinal = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbPerfilFinal.Text));
            /**********************Filtros de Consulta****************************/
            objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            if (ddlMacroproceso.Items.Count > 0)
                objFiltros.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            else
                objFiltros.intIdMacroProceso = 0;
            if (ddlProceso.Items.Count > 0)
                objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            else
                objFiltros.intIdProceso = 0;
            if (ddlSubproceso.Items.Count > 0)
                objFiltros.intIdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            else
                objFiltros.intIdSubProceso = 0;
            objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            booResult = cCuadroConsolidado.LoadInfoEvoPerfilRiesgo(ref strErrMsg, ref lstReporte, objFiltros);
            if (booResult == true)
            {
                int Iteracion = objFiltros.dtFechaEvoPerfilInicial.Month - 1;
                int points = 0;
                int iteracion = 0;
                Regex rex = new Regex("^[0-9]*$");
                foreach (clsDTOCuadroMandoConsolidado objReporte in lstReporte)
                {
                    promedioProbabilidad = Math.Round((Convert.ToDouble(objReporte.intSumatoriaProbabilidad)) / (Convert.ToDouble(objReporte.intNumeroRegistros))).ToString().Trim();
                    if (promedioProbabilidad == "NaN")
                        promedioProbabilidad = "0";
                    promedioImpacto = Math.Round((Convert.ToDouble(objReporte.intSumatoriaImpacto)) / (Convert.ToDouble(objReporte.intNumeroRegistros))).ToString().Trim();
                    if (promedioImpacto == "NaN")
                        promedioImpacto = "0";
                    //coordenada = (LinkButton)FindControl("LBt" + cRiesgo.IdProbabilidad(promedioProbabilidad) + cRiesgo.IdImpacto(promedioImpacto));
                    if(coorPro == promedioProbabilidad && coorImp == promedioImpacto)
                    {
                        row = dtMes.NewRow();
                        row["Serie"] = iteracion;
                        row["Mes"] = strMeses[iteracion].ToString();
                        dtMes.Rows.Add(row);
                        
                    }
                    iteracion++;
                    if (dtMes.Rows.Count > 0)
                    {
                        GVmeses.DataSource = dtMes;
                        GVmeses.DataBind();
                    }
                }
            }
        }
        public void mtdGrafico()
        {
            string[] strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            System.Data.DataTable dtMes = new System.Data.DataTable();
            dtMes.Columns.Add("Serie");
            dtMes.Columns.Add("Mes");
            DataRow row;

            String promedioProbabilidad = "";
            String promedioImpacto = "";
            string severidad = string.Empty;
            string color = string.Empty;
            LinkButton coordenada = new LinkButton();
            Panel scoordenadaPanel = new Panel();
            bool booResult = false;
            string strErrMsg = string.Empty;
            List<clsDTOCuadroMandoConsolidado> lstReporte = new List<clsDTOCuadroMandoConsolidado>();
            clsBLLCuadroMandoConsolidado cCuadroConsolidado = new clsBLLCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            objFiltros.dtFechaEvoPerfilInicial = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbPerfilInicial.Text));
            objFiltros.dtFechaEvoPerfilFinal = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbPerfilFinal.Text));
            /**********************Filtros de Consulta****************************/
            objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            if (ddlMacroproceso.Items.Count > 0)
                objFiltros.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            else
                objFiltros.intIdMacroProceso = 0;
            if (ddlProceso.Items.Count > 0)
                objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            else
                objFiltros.intIdProceso = 0;
            if (ddlSubproceso.Items.Count > 0)
                objFiltros.intIdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            else
                objFiltros.intIdSubProceso = 0;
            objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            booResult = cCuadroConsolidado.LoadInfoEvoPerfilRiesgo(ref strErrMsg, ref lstReporte, objFiltros);
            if (booResult == true)
            {
                int Iteracion = objFiltros.dtFechaEvoPerfilInicial.Month - 1;
                int points = 0;
                int iteracion = 0;
                Regex rex = new Regex("^[0-9]*$");
                foreach (clsDTOCuadroMandoConsolidado objReporte in lstReporte)
                {
                    promedioProbabilidad = Math.Round((Convert.ToDouble(objReporte.intSumatoriaProbabilidad)) / (Convert.ToDouble(objReporte.intNumeroRegistros))).ToString().Trim();
                    if (promedioProbabilidad == "NaN")
                        promedioProbabilidad = "0";
                    promedioImpacto = Math.Round((Convert.ToDouble(objReporte.intSumatoriaImpacto)) / (Convert.ToDouble(objReporte.intNumeroRegistros))).ToString().Trim();
                    if (promedioImpacto == "NaN")
                        promedioImpacto = "0";
                    coordenada = (LinkButton)FindControl("LBt" + cRiesgo.IdProbabilidad(promedioProbabilidad) + cRiesgo.IdImpacto(promedioImpacto));
                    /*row = dtMes.NewRow();

                    if (coordenada.Text == "")
                    {
                        iteracion = 1;
                        coordenada.Text = strMeses[Iteracion].ToString();

                        coordenada.CssClass = "buttonClass";
                    }
                    else
                    {
                        if (rex.IsMatch(coordenada.Text) == true)
                        {
                            int NumCoordenada = Convert.ToInt32(coordenada.Text);
                            NumCoordenada++;
                            row["Serie"] = Iteracion;
                            row["Mes"] = strMeses[Iteracion].ToString();
                            coordenada.Text = NumCoordenada.ToString();
                        }
                        else
                        {
                            row["Serie"] = Iteracion - 1;
                            row["Mes"] = coordenada.Text;
                            dtMes.Rows.Add(row);
                            row = dtMes.NewRow();
                            row["Serie"] = Iteracion;
                            row["Mes"] = strMeses[Iteracion].ToString();
                            iteracion++;
                            coordenada.Text = iteracion.ToString();

                        }

                        dtMes.Rows.Add(row);
                    }
                    */
                    /*if (coordenada != null)
                    {
                        //coordenada.CssClass = "buttonClassNumber";

                        //int mesPosicion = objFiltros.dtFechaEvoPerfilInicial.Month - 1;
                        coordenada.Text += "-"+strMeses[Iteracion].ToString();
                        coordenada.CssClass = "buttonClass";
                    }*
                    
                    /*for (int pointIndex = 0; pointIndex < lstReporte.Count; pointIndex++)
                    {*/
                    cCuadroConsolidado.mtdGetSeveridad(ref strErrMsg, ref severidad, ref color, promedioProbabilidad, promedioImpacto);
                    //Chart2.Series["Series1"].Points.AddY(promedioProbabilidad);
                    Chart2.Series["Series1"].Points.AddXY(promedioImpacto, promedioProbabilidad);

                    //Chart1.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                    Chart2.Series["Series1"].LegendText = "Evolución Perfil de Riesgo";
                    //Chart2.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
                    // Set marker size
                    //Chart2.Series["Series1"].MarkerSize = 27;
                    if (color == "Green")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Green;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].BackSecondaryColor = System.Drawing.Color.DarkGreen;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    if (color == "Yellow")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Yellow;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    if (color == "Orange")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Orange;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].BackSecondaryColor = System.Drawing.Color.DarkOrange;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    if (color == "Red")
                    {
                        Chart2.Series["Series1"].Points[points].Color = System.Drawing.Color.Red;
                        Chart2.Series["Series1"].Points[points].BackGradientStyle = GradientStyle.TopBottom;
                        Chart2.Series["Series1"].Points[points].BackSecondaryColor = System.Drawing.Color.DarkRed;
                        Chart2.Series["Series1"].Points[points].Label = strMeses[Iteracion].ToString();
                        Chart2.Series["Series1"].Points[points].ToolTip = strMeses[Iteracion].ToString();
                    }
                    //Chart2.Series["Series2"].Points.AddY(promedioImpacto);
                    //}

                    points++;
                    Iteracion++;
                }
                Chart2.ChartAreas["ChartArea1"].AxisX.Title = "Impacto";
                Chart2.ChartAreas["ChartArea1"].AxisY.Title = "Propabilidad";
                // Set series chart type
                Chart2.Series["Series1"].ChartType = SeriesChartType.Line;


                // Set point labels
                Chart2.Series["Series1"].IsValueShownAsLabel = true;

                // Enable X axis margin
                Chart2.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

                // Enable 3D, and show data point marker lines
                Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
                Chart2.Series["Series1"]["ShowMarkerLines"] = "True";

                
                dvLineChart.Visible = true;
            }
        }
        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtdRest();
            if(ddlTipoReporte.SelectedValue == "1")
            {
                trPerfilHistoricoInicial.Visible = true;
                trPerfilHistoricoFinal.Visible = true;
                trComparativo.Visible = false;
            }
            else
            {
                trPerfilHistoricoInicial.Visible = false;
                trPerfilHistoricoFinal.Visible = false;
                trComparativo.Visible = true;
            }
        }

        protected void coordenadaRiesgo(object sender, CommandEventArgs e)
        {
            #region Variables
            char[] delimiter = { ',' };
            String[] coordenada = e.CommandArgument.ToString().Trim().Split(delimiter);
            mtdTableMes(coordenada[0].ToString(), coordenada[1].ToString());
            #endregion Variables
            trDivMeses.Visible = true;
            mtdGrafico();
        }
        public void mtdRest()
        {
            ddlRiesgoGlobal.Items.Clear();
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            ddlAreas.Items.Clear();
            cbComparativo.Checked = false;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            txbPerfilInicial.Text = string.Empty;
            txbPerfilFinal.Text = string.Empty;
            dvLineChart.Visible = false;
            trHistoricoInicial.Visible = false;
            trHistoricoInicial1.Visible = false;
            trHistoricoInicial2.Visible = false;
            trHistoricoFinal.Visible = false;
            trPerfilHistoricoInicial.Visible = false;
            trPerfilHistoricoFinal.Visible = false;
            mtdCargarDDLs();
        }
        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            dvChartsReporteXYZ.Visible = false;
            dvChartsReporteNLK.Visible = false;
            dvEvoPerfil.Visible = false;
            Dbutton.Visible = false;
            ddlTipoReporte.SelectedIndex = 0;
            ddlRiesgoGlobal.Items.Clear();
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            ddlAreas.Items.Clear();
            cbComparativo.Checked = false;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            txbPerfilInicial.Text = string.Empty;
            txbPerfilFinal.Text = string.Empty;
            dvLineChart.Visible = false;
            trHistoricoInicial.Visible = false;
            trHistoricoInicial1.Visible = false;
            trHistoricoInicial2.Visible = false;
            trHistoricoFinal.Visible = false;
            trPerfilHistoricoInicial.Visible = false;
            trPerfilHistoricoFinal.Visible = false;
            mtdStard();
        }

        protected void ImbCleanFiltros_Click(object sender, ImageClickEventArgs e)
        {
            GVcriticidad.DataSource = null;
            GVcriticidad.DataBind();
            GVmeses.DataSource = null;
            GVmeses.DataBind();
            dvChartsReporteXYZ.Visible = false;
            dvChartsReporteNLK.Visible = false;
            dvEvoPerfil.Visible = false;
            Dbutton.Visible = false;
            ddlTipoReporte.SelectedIndex = 0;
            ddlRiesgoGlobal.Items.Clear();
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            ddlAreas.Items.Clear();
            cbComparativo.Checked = false;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            txbPerfilInicial.Text = string.Empty;
            txbPerfilFinal.Text = string.Empty;
            dvLineChart.Visible = false;
            trHistoricoInicial.Visible = false;
            trHistoricoInicial1.Visible = false;
            trHistoricoInicial2.Visible = false;
            trHistoricoFinal.Visible = false;
            trPerfilHistoricoInicial.Visible = false;
            trPerfilHistoricoFinal.Visible = false;
            mtdStard();
            resetValuesMatriz();
        }
        private void resetValuesMatriz()
        {
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    LinkButton coordenada = (LinkButton)FindControl("LBt" + i.ToString() + j.ToString());
                    coordenada.Text = "";
                }
            }
            
        }

        protected void Chart1_Load(object sender, EventArgs e)
        {

        }
    }
}