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

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando.Riesgos
{
    public partial class Riesgos : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        string IdFormulario = "5032";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBprocess);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.GVriesgosSaro);
            scriptManager.RegisterPostBackControl(this.GVriesgoGlobal);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();

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
            if (loadDDLPlanes(ref strErrMsg))
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
                    0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty,string.Empty,string.Empty);
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
                ddlRiesgoGlobal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "---"));
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
        private bool loadDDLPlanes(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.loadDDLPlanes();
                DDLPlanEstrategico.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "---"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLPlanEstrategico.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdPlan"].ToString()));
                }
            }
            catch (Exception ex)
            {
                strErrMsg = "Error al cargar los planes riesgo. " + ex.Message;
                booResult = true;
            }
            return booResult;
        }
        private bool loadDDLObjetivos(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                //Camilo 12/02/2014
                //dtInfo = cRiesgo.loadDDLObjetivos();
                dtInfo = cRiesgo.loadDDLObjetivos(DDLPlanEstrategico.SelectedValue.ToString());
                DDLObjetivoEstrategico.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "---"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLObjetivoEstrategico.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreObjetivos"].ToString().Trim(), dtInfo.Rows[i]["IdObjetivos"].ToString()));
                }
            }
            catch (Exception ex)
            {
                strErrMsg = "Error al cargar los objetivos estrategicos riesgo. " + ex.Message;
                booResult = true;
            }
            return booResult;
        }
        private void loadDDLClasificacionGeneral(String IdClasificacionRiesgo, int Tipo)
        {
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.loadDDLClasificacionGeneral(IdClasificacionRiesgo);
                switch (Tipo)
                {
                    case 2:
                        ddlClasificacionGeneral.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "---"));
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            ddlClasificacionGeneral.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreClasificacionGeneralRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionGeneralRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar clasificación general. " + ex.Message,2,"Atención");
            }
        }
        private void mtdLoadFactorRiesgo(int Tipo)
        {
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.mtdCargarDDLFactorRiesgo(Tipo);

                switch (Tipo)
                {
                    case 1://FactorRiesgo
                        break;

                    case 2://FactorRiesgoLAFT
                        ddlFactorRiesgo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "---"));
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            ddlFactorRiesgo.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreFactorRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar el Factor de Riesgo. " + ex.Message, 2 ,"Atención");
            }
        }
        private void loadDDLObjetivos()
        {
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                //Camilo 12/02/2014
                //dtInfo = cRiesgo.loadDDLObjetivos();
                dtInfo = cRiesgo.loadDDLObjetivos(DDLPlanEstrategico.SelectedValue.ToString());
                DDLObjetivoEstrategico.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "---"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLObjetivoEstrategico.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreObjetivos"].ToString().Trim(), dtInfo.Rows[i]["IdObjetivos"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar objetivos. " + ex.Message,2,"Atención");
            }
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

        protected void ddlRiesgoGlobal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRiesgoGlobal.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLClasificacionGeneral(ddlRiesgoGlobal.SelectedValue.ToString().Trim(), 2);
            }
            if (ddlRiesgoGlobal.SelectedValue.ToString().Trim() == "2")
            {
                mtdLoadFactorRiesgo(2);
            }
        }
        protected void DDLPlanEstrategico_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLObjetivoEstrategico.Items.Clear();
            DDLObjetivoEstrategico.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "0"));
            if (DDLPlanEstrategico.SelectedValue.ToString().Trim() != "---")
                loadDDLObjetivos();
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
                if(ddlTipoReporte.SelectedValue == "1")
                {
                    
                        booResult = mtdLoadReporteRiesgos(ref strErrMsg);
                        if (booResult == true)
                        {
                        //omb.ShowMessage("Riesgo: " + ddlRiesgoGlobal.SelectedValue, 2, "aa");
                            if (ddlRiesgoGlobal.SelectedValue == "---" )
                            {
                                dvTablaRiesgos.Visible = true;
                                dvGraficosGeneral.Visible = true;
                                dvGraficosRiesgoInherente.Visible = false;
                                dvGraficoRiesgosParticular.Visible = false;
                                GVriesgoGlobal.Visible = true;
                                GVriesgosSaro.Visible = false;
                            dvGraficosRiesgoInherente.Visible = false;
                        }
                            /*if (ddlRiesgoInherente.SelectedValue != "---")
                            {
                                dvGraficosGeneral.Visible = false;
                                dvTablaRiesgos.Visible = true;
                                dvGraficosRiesgoInherente.Visible = true;
                                dvGraficoRiesgosParticular.Visible = false;
                                GVriesgoGlobal.Visible = false;
                                GVriesgosSaro.Visible = false;
                                dvGraficosRiesgoInherente.Visible = true;
                        }*/
                            if (ddlRiesgoGlobal.SelectedValue != "---" )
                            {
                                dvTablaRiesgos.Visible = true;
                                dvGraficosGeneral.Visible = false;
                                dvGraficoRiesgosParticular.Visible = true;
                                GVriesgoGlobal.Visible = false;
                                GVriesgosSaro.Visible = true;
                            dvGraficosRiesgoInherente.Visible = false;
                        }
                            Dbutton.Visible = true;
                        }
                        else
                        {
                            omb.ShowMessage(strErrMsg, 2, "Atención");
                        }
                    
                }
                if (ddlTipoReporte.SelectedValue == "2")
                {
                    booResult = LoadInfoReporteRiesgosSinControl(ref strErrMsg);
                    if (booResult == true)
                    {
                        dvTablaRiesgos.Visible = false;
                        dvGraficosGeneral.Visible = false;
                        dvGraficoRiesgosParticular.Visible = true;
                        GVriesgoGlobal.Visible = false;
                        GVriesgosSaro.Visible = false;
                        Dbutton.Visible = true;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }

                }
                if (ddlTipoReporte.SelectedValue == "3")
                {
                    booResult = mtdLoadReporteCausasSinControl(ref strErrMsg);
                    if(booResult == true)
                    {
                        dvTablaRiesgos.Visible = false;
                        dvGraficosGeneral.Visible = false;
                        dvGraficoRiesgosParticular.Visible = true;
                        GVriesgoGlobal.Visible = false;
                        GVriesgosSaro.Visible = false;
                        Dbutton.Visible = true;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }

                }
                if(ddlTipoReporte.SelectedValue == "4")
                {
                    booResult = LoadInfoReporteRiesgosPlanes(ref strErrMsg);
                    if (booResult == true)
                    {
                        dvTablaRiesgos.Visible = false;
                        dvGraficosGeneral.Visible = false;
                        dvGraficoRiesgosParticular.Visible = true;
                        GVriesgoGlobal.Visible = false;
                        GVriesgosSaro.Visible = false;
                        Dbutton.Visible = true;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }
                }
            }
            else
            {
                omb.ShowMessage("Se debe seleccionar un Tipo de Reporte", 2, "Atención");
            }
        }
        public void mdtGraficos()
        {
            bool booResult = false;

            //DataTable dt = GetData();
            //LoadChartData(dt);
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue != "---")
            {
                if (ddlTipoReporte.SelectedValue == "1")
                {

                    booResult = mtdLoadReporteRiesgos(ref strErrMsg);
                    if (booResult == true)
                    {
                        //omb.ShowMessage("Grafico general: "+ ddlRiesgoGlobal.SelectedValue, 2, "Atencion");
                        if (ddlRiesgoGlobal.SelectedValue == "---")
                        {
                            //omb.ShowMessage("Grafico general", 2, "Atencion");
                            dvTablaRiesgos.Visible = true;
                            dvGraficosGeneral.Visible = false;
                            dvGraficosRiesgoInherente.Visible = false;
                            dvGraficoRiesgosParticular.Visible = true;
                            GVriesgoGlobal.Visible = false;
                            GVriesgosSaro.Visible = true;
                        }
                        /*if (ddlRiesgoInherente.SelectedValue != "---")
                        {
                            dvGraficosGeneral.Visible = false;
                            dvTablaRiesgos.Visible = true;
                            dvGraficosRiesgoInherente.Visible = false;
                            dvGraficoRiesgosParticular.Visible = false;

                        }*/
                        if (ddlRiesgoGlobal.SelectedValue != "---" )
                        {
                            dvTablaRiesgos.Visible = true;
                            dvGraficosGeneral.Visible = true;
                            dvGraficoRiesgosParticular.Visible = false;
                            GVriesgoGlobal.Visible = true;
                            GVriesgosSaro.Visible = false;
                        }
                        Dbutton.Visible = true;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }

                }
                if (ddlTipoReporte.SelectedValue == "2")
                {
                    booResult = LoadInfoReporteRiesgosSinControl(ref strErrMsg);
                    if (booResult == true)
                    {
                        dvTablaRiesgos.Visible = false;
                        dvGraficosGeneral.Visible = false;
                        dvGraficoRiesgosParticular.Visible = true;
                        GVriesgoGlobal.Visible = false;
                        GVriesgosSaro.Visible = false;
                        Dbutton.Visible = true;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }

                }
                if (ddlTipoReporte.SelectedValue == "3")
                {
                    booResult = mtdLoadReporteCausasSinControl(ref strErrMsg);
                    if (booResult == true)
                    {
                        dvTablaRiesgos.Visible = false;
                        dvGraficosGeneral.Visible = false;
                        dvGraficoRiesgosParticular.Visible = true;
                        GVriesgoGlobal.Visible = false;
                        GVriesgosSaro.Visible = false;
                        Dbutton.Visible = true;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }

                }
                if (ddlTipoReporte.SelectedValue == "4")
                {
                    booResult = LoadInfoReporteRiesgosPlanes(ref strErrMsg);
                    if (booResult == true)
                    {
                        dvTablaRiesgos.Visible = false;
                        dvGraficosGeneral.Visible = false;
                        dvGraficoRiesgosParticular.Visible = true;
                        GVriesgoGlobal.Visible = false;
                        GVriesgosSaro.Visible = false;
                        Dbutton.Visible = true;
                    }
                    else
                    {
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                    }
                }
            }
            else
            {
                omb.ShowMessage("Se debe seleccionar un Tipo de Reporte", 2, "Atención");
            }
        }
        protected void cbComparativo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbComparativo.Checked)
            {
                trHistoricoInicial.Visible = true;
                trHistoricoFinal.Visible = true;
            }
            else
            {
                trHistoricoInicial.Visible = false;
                trHistoricoFinal.Visible = false;

                txbFechaInicial.Text = string.Empty;
                txbFechaFinal.Text = string.Empty;
            }
        }
        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private bool mtdLoadReporteRiesgos(ref string strErrMsg)
        {
            
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            
            List<clsDTOCuadroMandoConsolidado> lstReporte = new List<clsDTOCuadroMandoConsolidado>();
            clsBLLCuadroMandoRiesgos cCuadroRiesgos = new clsBLLCuadroMandoRiesgos();
            clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            try
            {
                if (ddlRiesgoGlobal.SelectedValue != "---")
                {
                    objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
                    objFiltros.strClasificacionGeneral = ddlRiesgoGlobal.SelectedItem.Text;
                }
                else
                    objFiltros.intRiesgoGlobal = 0;
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
                if(cbComparativo.Checked)
                    objFiltros.strAreaRiesgo = ddlAreas.SelectedItem.Text;
                else
                    objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
                if (ddlClasificacionGeneral.SelectedValue != "" && ddlClasificacionGeneral.SelectedValue != "---")
                    objFiltros.intIdClasificacionGeneral = Convert.ToInt32(ddlClasificacionGeneral.SelectedValue);
                else
                    objFiltros.intIdClasificacionGeneral = 0;
                if (ddlFactorRiesgo.SelectedValue != "---" && ddlFactorRiesgo.SelectedValue != "")
                    objFiltros.intIdFactor = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
                else
                    objFiltros.intIdFactor = 0;
                if (DDLPlanEstrategico.SelectedValue != "---" && DDLPlanEstrategico.SelectedValue != "")
                {
                    if (DDLObjetivoEstrategico.SelectedValue != "---" && DDLObjetivoEstrategico.SelectedValue != "")
                    {
                        objFiltros.intIdObjetivo = Convert.ToInt32(DDLObjetivoEstrategico.SelectedValue);
                    }
                    else
                    {
                        objFiltros.intIdObjetivo = 0;
                    }
                }
                if (cbComparativo.Checked)
                {
                    objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                    objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
                }
            }catch(Exception ex)
            {
                //strErrMsg = "Error en los filtros: " + ex;
                omb.ShowMessage("Error: "+ex, 2, "Atención");
            }
            //omb.ShowMessage("Riesgo: " + ddlRiesgoGlobal.SelectedValue, 2, "Atención");
            /**********************Filtros  de Consulta****************************/
            if(ddlRiesgoGlobal.SelectedValue == "---")
            {
                Session["Grafico"] = "General";
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                try
                {
                    dtInfo = cRiesgo.loadDDLClasificacion();
                }catch(Exception ex)
                {
                    strErrMsg = "Error en la consulta de calificación riesgo";
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                }
                try
                {
                    System.Data.DataTable initialDataSource = new System.Data.DataTable();
                    initialDataSource.Columns.Add("Riesgo", Type.GetType("System.String"));
                    initialDataSource.Columns.Add("Bajo", Type.GetType("System.String"));
                    initialDataSource.Columns.Add("Moderado", Type.GetType("System.String"));
                    initialDataSource.Columns.Add("Alto", Type.GetType("System.String"));
                    initialDataSource.Columns.Add("Extremo", Type.GetType("System.String"));
                    //omb.ShowMessage("Cant loadDDLClasificacion: " + dtInfo.Rows.Count.ToString(), 3, "2");
                    for (int i = 0; i < dtInfo.Rows.Count; i++)
                    {
                        lstReporte = new List<clsDTOCuadroMandoConsolidado>();
                        objFiltros.intRiesgoGlobal = Convert.ToInt32(dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString());
                        //omb.ShowMessage("Riesgo: "+ dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString(), 3, "Exito");
                        booResult = cCuadroRiesgos.mtdConsultarReporteRiesgos(ref strErrMsg, ref lstReporte, objFiltros);
                        if (lstReporte != null)
                        {
                            int valorBajo = 0;
                            int valorModerado = 0;
                            int valorAlto = 0;
                            int valorExtremo = 0;
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
                            DataRow dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString();
                            dr1["Bajo"] = valorBajo;
                            dr1["Moderado"] = valorModerado;
                            dr1["Alto"] = valorAlto;
                            dr1["Extremo"] = valorExtremo;
                            initialDataSource.Rows.Add(dr1);
                            
                        }
                        //omb.ShowMessage(strErrMgs, 1, "Consulta");
                    }
                    booResult = true;
                    LoadChartGeneral(initialDataSource);
                }catch(Exception ex)
                {
                    
                    omb.ShowMessage("Error en la generación de reporte: " + ex, 2, "Atención");
                }
                
            }
            else
            {
                //omb.ShowMessage("En el Else", 3, "3");
                Session["Grafico"] = "Particular";
                string strQuery = string.Empty;
                booResult = cCuadroRiesgos.mtdConsultarReporteRiesgosSaro(ref strErrMsg, ref lstReporte, objFiltros, ref strQuery);
                //omb.ShowMessage(strQuery, 3, "6");
                if(lstReporte != null)
                {
                    int valorBajo = 0;
                    int valorModerado = 0;
                    int valorAlto = 0;
                    int valorExtremo = 0;
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

                    mtdViewChartSaro(dtCuadroMando);
                    //omb.ShowMessage("Entro mtdConsultarReporteRiesgosSaro Si", 3, "Exito");
                    booResult = true;
                }
            }
            
            return booResult;
        }
        private void LoadChartGeneral(System.Data.DataTable initialDataSource)
        {

            GVriesgoGlobal.DataSource = initialDataSource;
            GVriesgoGlobal.DataBind();
            int Identity = 1;
            //System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();
            //omb.ShowMessage("Rows: "+ initialDataSource.Rows.Count, 3, "4");
            foreach (DataRow dr in initialDataSource.Rows)
            {
                
                    System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();
                series.Name = dr[0].ToString();
                series.XValueMember = "Riesgo Inherente";
                series.YValueMembers = "Cantidad";
                series.LabelBackColor = System.Drawing.Color.Green;
                int iteracion = 0;
                for (int i = 1; i < initialDataSource.Columns.Count; i++)
                {
                    
                    //series.Legend = dr[0].ToString();
                    int y = Convert.ToInt32(dr[i].ToString());
                    //series.Points.AddXY(dr[0].ToString(), y);
                    //series.Points.AddY(y);
                    if (i == 1)
                    {
                        series.Points.AddXY("Bajo", y);
                        
                    }
                    if (i == 2)
                    {
                        series.Points.AddXY("Moderado", y);
                        
                    }
                    if (i == 3)
                    {
                        series.Points.AddXY("Alto", y);
                        
                    }
                    if (i == 4)
                    {
                        series.Points.AddXY("Extremo", y);
                        
                    }
                }
                    //Chart4.Legends[0].Enabled = true;
                    ChartGeneral.Series.Add(series);
                }
            //Chart4.Legends[0].Enabled = true;
            
        }
        public void mtdViewChartSaro(System.Data.DataTable dtInfo)
        {
            GVriesgosSaro.DataSource = dtInfo;
            GVriesgosSaro.DataBind();
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtInfo.Rows[i][1]);
                Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
            }
            ChartSaro.Series[0].Points.DataBindXY(x, y);
            ChartSaro.Series[0].ChartType = SeriesChartType.Pie;
            ChartSaro.ChartAreas["ChartSaroArea"].Area3DStyle.Enable3D = true;
            ChartSaro.Legends[0].Enabled = true;
            ChartSaro.Titles.Add("NewTitle");
            if(ddlTipoReporte.SelectedValue == "2")
                ChartSaro.Titles[1].Text = "Riesgos sin Controles Valor Total: " + Total;
            if(ddlTipoReporte.SelectedValue == "1")
            {
                ChartSaro.Titles[1].Text = ddlRiesgoGlobal.SelectedItem.Text + " Total: " + Total;
            }
                
            if (ddlTipoReporte.SelectedValue == "3")
                ChartSaro.Titles[1].Text = "Riesgos Causas sin un control asociado Valor Total: " + Total;
            if(ddlTipoReporte.SelectedValue == "4")
                ChartSaro.Titles[1].Text = "Riesgos residuales Altos y/o Extremo sin planes de acción Valor Total: " + Total;
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartSaro.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "Bajo": point.Color = System.Drawing.Color.Green; break;
                        case "Moderado": point.Color = System.Drawing.Color.Yellow; break;
                        case "Alto": point.Color = System.Drawing.Color.Orange; break;
                        case "Extremo": point.Color = System.Drawing.Color.Red; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                }
            }
        }
        public void mtdViewChartSarlaft(System.Data.DataTable dtInfo)
        {
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtInfo.Rows[i][1]);
                Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
            }
            ChartSarlaft.Series[0].Points.DataBindXY(x, y);
            ChartSarlaft.Series[0].ChartType = SeriesChartType.Pie;
            ChartSarlaft.ChartAreas["ChartSarlaftArea"].Area3DStyle.Enable3D = true;
            ChartSarlaft.Legends[0].Enabled = true;
            ChartSarlaft.Titles.Add("NewTitle");
            ChartSarlaft.Titles[1].Text = "Sarlaft Valor Total: " + Total;
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartSarlaft.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "Bajo": point.Color = System.Drawing.Color.Green; break;
                        case "Moderado": point.Color = System.Drawing.Color.Yellow; break;
                        case "Alto": point.Color = System.Drawing.Color.Orange; break;
                        case "Extremo": point.Color = System.Drawing.Color.Red; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                }
            }
        }
        public void mtdViewChartRiesgoInherente(System.Data.DataTable dtInfo)
        {
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtInfo.Rows[i][1]);
                Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
                //ChartRiesgoInherente.Series[0].Points.AddY(Convert.ToInt32(dtInfo.Rows[i][1]));
                //ChartRiesgoInherente.Series[0].Points.AddXY(Convert.ToInt32(dtInfo.Rows[i][0]), Convert.ToInt32(dtInfo.Rows[i][1]));
            }
            ChartRiesgoInherente.Series[0].Points.DataBindXY(x, y);
            ChartRiesgoInherente.Series[0].ChartType = SeriesChartType.Bar;
            ChartRiesgoInherente.ChartAreas["ChartRiesgoInherenteArea"].Area3DStyle.Enable3D = true;
            ChartRiesgoInherente.Legends[0].Enabled = true;
            ChartRiesgoInherente.Titles.Add("NewTitle");
            ChartRiesgoInherente.Titles[1].Text = "Riesgo Inherente: "+ddlRiesgoInherente.SelectedItem.Text;
            ChartRiesgoInherente.ChartAreas["ChartRiesgoInherenteArea"].AxisX.Title = "Propabilidad";
            ChartRiesgoInherente.ChartAreas["ChartRiesgoInherenteArea"].AxisY.Title = "Valor";
            
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartRiesgoInherente.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "1": point.Color = System.Drawing.Color.Blue; break;
                        case "2": point.Color = System.Drawing.Color.Green; break;
                        case "3": point.Color = System.Drawing.Color.Yellow; break;
                        case "4": point.Color = System.Drawing.Color.Orange; break;
                        case "5": point.Color = System.Drawing.Color.Red; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                }
            }
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue == "1")
            {
                mtdLoadReporteRiesgos(ref strErrMsg);
            }
            if (ddlTipoReporte.SelectedValue == "2")
                LoadInfoReporteRiesgosSinControl(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "3")
                mtdLoadReporteCausasSinControl(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "4")
                LoadInfoReporteRiesgosPlanes(ref strErrMsg);
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
            pdfDocument.AddTitle("Cuadro de Mando Riesgos");
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
            if (ddlRiesgoGlobal.SelectedValue != "---")
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
            if (ddlCadenaValor.SelectedValue != "---" && ddlCadenaValor.SelectedValue != "")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Cadena Valor:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlCadenaValor.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
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

            if (ddlAreas.SelectedValue != "0" && ddlAreas.SelectedValue != "")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Área:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlAreas.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Área:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Todos"));
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
            pdfCellEncabezado = new PdfPCell(new Phrase("Clasificación general:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            if(ddlClasificacionGeneral.SelectedValue != "" && ddlClasificacionGeneral.SelectedValue != "0")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlClasificacionGeneral.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(""));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            
            pdfCellEncabezado = new PdfPCell(new Phrase("Factor de Riesgo LA/FT:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            if(ddlFactorRiesgo.SelectedValue != "" && ddlFactorRiesgo.SelectedValue != "0")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlFactorRiesgo.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(""));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            
            pdfCellEncabezado = new PdfPCell(new Phrase("Plan Estratégico:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            if(DDLPlanEstrategico.SelectedValue != "" && DDLPlanEstrategico.SelectedValue != "0")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(DDLPlanEstrategico.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(""));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            
            pdfCellEncabezado = new PdfPCell(new Phrase("Objetivo Estratégico:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            if(DDLObjetivoEstrategico.SelectedValue != "" && DDLObjetivoEstrategico.SelectedValue != "0")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(DDLObjetivoEstrategico.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(""));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            
            #endregion
            MemoryStream streamRiesgosSaro = new MemoryStream();
            MemoryStream streamRiesgosSarlaft = new MemoryStream();
            if (ddlTipoReporte.SelectedValue == "1")
            {
                if (Session["Grafico"].ToString() == "General")
                    ChartGeneral.SaveImage(streamRiesgosSaro, ChartImageFormat.Png);
                else
                    ChartSaro.SaveImage(streamRiesgosSaro, ChartImageFormat.Png);

                //ChartSarlaft.SaveImage(streamRiesgosSarlaft, ChartImageFormat.Png);
            }
            if (ddlTipoReporte.SelectedValue == "2")
                ChartSaro.SaveImage(streamRiesgosSaro, ChartImageFormat.Png);
            if (ddlTipoReporte.SelectedValue == "3")
                ChartSaro.SaveImage(streamRiesgosSaro, ChartImageFormat.Png);
            if (ddlTipoReporte.SelectedValue == "4")
                ChartSaro.SaveImage(streamRiesgosSaro, ChartImageFormat.Png);
            PdfPTable pdftblImageGraficoRiesgosSaro = new PdfPTable(1);
            PdfPTable pdftblImageGraficoRiesgosSarlaft = new PdfPTable(1);
            //PdfPTable pdftblImageGrafico2 = new PdfPTable(1);
            //PdfPTable pdftblImageGrafico3 = new PdfPTable(1);
            if (ddlTipoReporte.SelectedValue == "1")
            {
                iTextSharp.text.Image imagenGraficoSaro = iTextSharp.text.Image.GetInstance(streamRiesgosSaro.GetBuffer());
                //iTextSharp.text.Image imagenGraficoSarlaft = iTextSharp.text.Image.GetInstance(streamRiesgosSarlaft.GetBuffer());

                PdfPCell pdfcellImageGraficoRiesgosSaro = new PdfPCell(imagenGraficoSaro, true);
                //PdfPCell pdfcellImageGraficoRiesgosSarlaft = new PdfPCell(imagenGraficoSarlaft, true);
                pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoRiesgosSaro);
                //pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoRiesgosSarlaft);
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                iTextSharp.text.Image imagenGraficoSaro = iTextSharp.text.Image.GetInstance(streamRiesgosSaro.GetBuffer());

                PdfPCell pdfcellImageGraficoRiesgosSaro = new PdfPCell(imagenGraficoSaro, true);

                pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoRiesgosSaro);
            }
            if (ddlTipoReporte.SelectedValue == "3")
            {
                iTextSharp.text.Image imagenGraficoSaro = iTextSharp.text.Image.GetInstance(streamRiesgosSaro.GetBuffer());

                PdfPCell pdfcellImageGraficoRiesgosSaro = new PdfPCell(imagenGraficoSaro, true);
                
                pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoRiesgosSaro);
            }
            if (ddlTipoReporte.SelectedValue == "4")
            {
                iTextSharp.text.Image imagenGraficoSaro = iTextSharp.text.Image.GetInstance(streamRiesgosSaro.GetBuffer());

                PdfPCell pdfcellImageGraficoRiesgosSaro = new PdfPCell(imagenGraficoSaro, true);

                pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoRiesgosSaro);
            }
            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();
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
            if (cbComparativo.Checked)
            {
                pdfDocument.Add(pdfTableComparativo);
                pdfDocument.Add(Chunk.NEWLINE);
            }
            if (ddlTipoReporte.SelectedValue == "1")
            {
                
                pdfDocument.Add(pdftblImageGraficoRiesgosSaro);
                pdfDocument.Add(Chunk.NEWLINE);
                /*pdfDocument.Add(pdftblImageGraficoRiesgosSarlaft);
                pdfDocument.Add(Chunk.NEWLINE);*/
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {

                pdfDocument.Add(pdftblImageGraficoRiesgosSaro);
                pdfDocument.Add(Chunk.NEWLINE);
            }
            if (ddlTipoReporte.SelectedValue == "3")
            {

                pdfDocument.Add(pdftblImageGraficoRiesgosSaro);
                pdfDocument.Add(Chunk.NEWLINE);
                /*pdfDocument.Add(pdftblImageGraficoRiesgosSarlaft);
                pdfDocument.Add(Chunk.NEWLINE);*/
            }
            if (ddlTipoReporte.SelectedValue == "4")
            {

                pdfDocument.Add(pdftblImageGraficoRiesgosSaro);
                pdfDocument.Add(Chunk.NEWLINE);
            }
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=CuadroMandoRiesgos.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "Reporte" + ddlTipoReporte.SelectedItem.Text + "_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue == "1")
                mtdLoadReporteRiesgos(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "2")
                LoadInfoReporteRiesgosSinControl(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "3")
                mtdLoadReporteCausasSinControl(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "4")
                LoadInfoReporteRiesgosPlanes(ref strErrMsg);
            string tmpChartNameRiesgosSaro = "Grafico" + ddlTipoReporte.SelectedValue + "Saro.jpg";
            string tmpChartNameRiesgosSarlaft = "Grafico" + ddlTipoReporte.SelectedItem.Text + "Sarlaft.jpg";
            string imgPathSaro = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartNameRiesgosSaro;
            string imgPathSarlaft = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartNameRiesgosSarlaft;
            if (ddlTipoReporte.SelectedValue == "1")
            {
                if(Session["Grafico"].ToString() == "General")
                {
                    ChartGeneral.SaveImage(imgPathSaro);
                    ChartSarlaft.SaveImage(imgPathSarlaft);
                }
                else
                {
                    ChartSaro.SaveImage(imgPathSaro);
                    ChartSarlaft.SaveImage(imgPathSarlaft);
                }
                
            }
            if (ddlTipoReporte.SelectedValue == "2")
                ChartSaro.SaveImage(imgPathSaro);
            if (ddlTipoReporte.SelectedValue == "3")
                ChartSaro.SaveImage(imgPathSaro);
            if (ddlTipoReporte.SelectedValue == "4")
                ChartSaro.SaveImage(imgPathSaro);
            string imgPathSaroLocal = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartNameRiesgosSaro);
            string imgPathSarlaftLocal = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartNameRiesgosSarlaft);
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("Tipo reporte:");
            gridEncabezado.Columns.Add("Riesgo Global:");
            gridEncabezado.Columns.Add("Cadena Valor:");
            gridEncabezado.Columns.Add("Macroproceso:");
            gridEncabezado.Columns.Add("Proceso:");
            gridEncabezado.Columns.Add("Subproceso:");
            gridEncabezado.Columns.Add("Área:");
            gridEncabezado.Columns.Add("Clasificación general:");
            gridEncabezado.Columns.Add("Factor de Riesgo LA/FT:");
            gridEncabezado.Columns.Add("Plan Estratégico:");
            gridEncabezado.Columns.Add("Objetivo Estratégico:");
            if (cbComparativo.Checked)
            {
                gridEncabezado.Columns.Add("Fecha Inicial:");
                gridEncabezado.Columns.Add("Fecha Final:");
            }

            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Tipo reporte:"] = ddlTipoReporte.SelectedItem.Text;
            rowEncabezado["Riesgo Global:"] = ddlRiesgoGlobal.SelectedItem.Text;
            if (ddlCadenaValor.SelectedValue != "0" && ddlCadenaValor.SelectedValue != "")
            {
                rowEncabezado["Cadena Valor:"] = ddlCadenaValor.SelectedItem.Text;
            }
            else
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
            if (ddlAreas.SelectedValue != "0" && ddlAreas.SelectedValue != "")
            {
                rowEncabezado["Área:"] = ddlAreas.SelectedItem.Text;
            }
            else
            {
                rowEncabezado["Área:"] = "Todos";
            }
            if (cbComparativo.Checked)
            {
                rowEncabezado["Fecha Inicial:"] = txbFechaInicial.Text;
                rowEncabezado["Fecha Final:"] = txbFechaFinal.Text;
            }
            if (ddlClasificacionGeneral.SelectedValue != "" && ddlClasificacionGeneral.SelectedValue != "0")
                rowEncabezado["Clasificación general:"] = ddlClasificacionGeneral.SelectedItem.Text;
            else
                rowEncabezado["Clasificación general:"] = "";
            if (ddlFactorRiesgo.SelectedValue != "" && ddlFactorRiesgo.SelectedValue != "0")
                rowEncabezado["Factor de Riesgo LA/FT:"] = ddlFactorRiesgo.SelectedItem.Text;
            else
                rowEncabezado["Factor de Riesgo LA/FT:"] = "";
            if (DDLPlanEstrategico.SelectedValue != "" && DDLPlanEstrategico.SelectedValue != "0")
                rowEncabezado["Plan Estratégico:"] = DDLPlanEstrategico.SelectedItem.Text;
            else
                rowEncabezado["Plan Estratégico:"] = "";
            if (DDLObjetivoEstrategico.SelectedValue != "" && DDLObjetivoEstrategico.SelectedValue != "0")
                rowEncabezado["Objetivo Estratégico:"] = DDLObjetivoEstrategico.SelectedItem.Text;
            else
                rowEncabezado["Objetivo Estratégico:"] = "";
            gridEncabezado.Rows.Add(rowEncabezado);
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=CuadroMandoRiesgos.xls;");
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
            
            if (ddlTipoReporte.SelectedValue == "1")
            {

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathSaroLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
                /*StringWriter stringWrite2 = new StringWriter();
                HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
                string headerTableSarlaft = @"<Table><tr><td><img src='" + imgPathSarlaftLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSarlaft);
                Response.Write(stringWrite2.ToString());*/
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathSaroLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
            }
            if (ddlTipoReporte.SelectedValue == "3")
            {

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathSaroLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
                /*StringWriter stringWrite2 = new StringWriter();
                HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
                string headerTableSarlaft = @"<Table><tr><td><img src='" + imgPathSarlaftLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSarlaft);
                Response.Write(stringWrite2.ToString());*/
            }
            if (ddlTipoReporte.SelectedValue == "4")
            {

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathSaroLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
            }
            Response.End();

        }
        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            dvGraficosGeneral.Visible = false;
            dvGraficoRiesgosParticular.Visible = false;
            dvGraficosRiesgoInherente.Visible = false;
            dvGraficoRiesgosSarlaft.Visible = false;
            dvTablaRiesgos.Visible = false;
            Dbutton.Visible = false;
            ddlTipoReporte.SelectedIndex = 0;
            ddlRiesgoGlobal.Items.Clear();
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            ddlAreas.Items.Clear();
            ddlClasificacionGeneral.Items.Clear();
            ddlFactorRiesgo.Items.Clear();
            DDLPlanEstrategico.Items.Clear();
            DDLObjetivoEstrategico.Items.Clear();
            cbComparativo.Checked = false;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            txbPerfilInicial.Text = string.Empty;
            txbPerfilFinal.Text = string.Empty;
            trHistoricoInicial.Visible = false;
            trHistoricoFinal.Visible = false;
            mtdStard();
        }

        protected void GVriesgosSaro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGridRiesgos = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVriesgosSaro.Rows[RowGridRiesgos];
            string RiesgoInherente = row.Cells[0].Text;
            int IdRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            string clasificacionRiesgo = string.Empty;
            if (ddlRiesgoGlobal.SelectedValue != "---")
                clasificacionRiesgo = ddlRiesgoGlobal.SelectedItem.Text;
            int IdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            int IdMacroproceso = 0;
            int IdProceso = 0;
            int IdSubproceso = 0;
            if (ddlMacroproceso.Items.Count > 0)
                IdMacroproceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            if (ddlProceso.Items.Count > 0)
                IdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            if (ddlSubproceso.Items.Count > 0)
                IdSubproceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            int Area = Convert.ToInt32(ddlAreas.SelectedValue);
            int IdClasificacionRiesgo = 0;
            if (ddlClasificacionGeneral.SelectedValue != "---")
                IdClasificacionRiesgo = Convert.ToInt32(ddlClasificacionGeneral.SelectedValue);
            int IdFactor = 0;
            if (ddlFactorRiesgo.SelectedValue != "---" && ddlFactorRiesgo.SelectedValue != "")
                IdFactor = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            int IdObjetivo = 0;
            if (DDLPlanEstrategico.SelectedValue != "---")
            {
                if (DDLObjetivoEstrategico.SelectedValue != "---")
                {
                    IdObjetivo = Convert.ToInt32(DDLObjetivoEstrategico.SelectedValue);
                }
            }
            DateTime FechaHistoricoInicial = new DateTime();
            DateTime FechaHistoricoFinal = new DateTime();
            if (cbComparativo.Checked)
            {
                FechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                FechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            string str;
            string TipoReporte = ddlTipoReporte.SelectedValue;
            str = "window.open('ViewDetalleRiesgos/DetalleRiesgos.aspx?riesgo="+RiesgoInherente+ "&IdRiesgoGlobal="+ IdRiesgoGlobal + "&IdCadenaValor="+ IdCadenaValor + "&IdMacroproceso="+ IdMacroproceso + "&IdProceso="+ IdProceso + "&IdSubproceso="+IdSubproceso+ "&Area="+ Area + "&IdClasificacionRiesgo="+ IdClasificacionRiesgo + "&IdFactor="+ IdFactor + "&IdObjetivo="+ IdObjetivo + "&FechaHistoricoInicial="+
                FechaHistoricoInicial + "&FechaHistoricoFinal="+ FechaHistoricoFinal + "&clasificacionRiesgo="+clasificacionRiesgo+ "&riesgoGlobal=" + clasificacionRiesgo + "&tipoReporte="+TipoReporte+"','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
            string strErrMsg = string.Empty;
            mtdLoadReporteRiesgos(ref strErrMsg);
        }

        protected void GVriesgoGlobal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGridRiesgos = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVriesgoGlobal.Rows[RowGridRiesgos];
            string RiesgoGlobal = row.Cells[0].Text;
            int IdRiesgoGlobal = 0;
            string clasificacionRiesgo = string.Empty;
            if (ddlRiesgoGlobal.SelectedValue != "---")
            {
                IdRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
                clasificacionRiesgo = ddlRiesgoGlobal.SelectedItem.Text;
            } else
                IdRiesgoGlobal = 0;

                            int IdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            int IdMacroproceso = 0;
            int IdProceso = 0;
            int IdSubproceso = 0;
            if (ddlMacroproceso.Items.Count > 0)
                IdMacroproceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            if (ddlProceso.Items.Count > 0)
                IdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            if (ddlSubproceso.Items.Count > 0)
                IdSubproceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            string Area = ddlAreas.SelectedItem.Text;
            int IdArea = Convert.ToInt32(ddlAreas.SelectedValue);
            int IdClasificacionRiesgo = 0;
            if (ddlClasificacionGeneral.SelectedValue != "---" && ddlClasificacionGeneral.SelectedValue != "")
                IdClasificacionRiesgo = Convert.ToInt32(ddlClasificacionGeneral.SelectedValue);
            int IdFactor = 0;
            if (ddlFactorRiesgo.SelectedValue != "---" && ddlFactorRiesgo.SelectedValue != "")
                IdFactor = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            int IdObjetivo = 0;
            if (DDLPlanEstrategico.SelectedValue != "---" && DDLPlanEstrategico.SelectedValue != "")
            {
                if (DDLObjetivoEstrategico.SelectedValue != "---" && DDLObjetivoEstrategico.SelectedValue != "")
                {
                    IdObjetivo = Convert.ToInt32(DDLObjetivoEstrategico.SelectedValue);
                }
            }
            DateTime FechaHistoricoInicial = new DateTime();
            DateTime FechaHistoricoFinal = new DateTime();
            if (cbComparativo.Checked)
            {
                FechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                FechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            string str;
            str = "window.open('ViewDetalleRiesgos/DetalleRiesgos.aspx?riesgoGlobal=" + RiesgoGlobal + "&IdRiesgoGlobal=" + IdRiesgoGlobal + "&IdCadenaValor=" + IdCadenaValor + "&IdMacroproceso=" + IdMacroproceso + "&IdProceso=" + IdProceso + "&IdSubproceso=" + IdSubproceso + "&Area=" + Area + "&IdClasificacionRiesgo=" + IdClasificacionRiesgo + "&IdFactor=" + IdFactor + "&IdObjetivo=" + IdObjetivo + "&FechaHistoricoInicial=" +
                FechaHistoricoInicial + "&FechaHistoricoFinal=" + FechaHistoricoFinal + "&clasificacionRiesgo=" + clasificacionRiesgo + "&IdArea="+ IdArea + "','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
            string strErrMsg = string.Empty;
            bool booResult = false;
            if (ddlTipoReporte.SelectedValue == "1")
            {

                booResult = mtdLoadReporteRiesgos(ref strErrMsg);
                if (booResult == true)
                {
                    //omb.ShowMessage("Riesgo: " + ddlRiesgoGlobal.SelectedValue, 2, "aa");
                    if (ddlRiesgoGlobal.SelectedValue == "---")
                    {
                        dvTablaRiesgos.Visible = true;
                        dvGraficosGeneral.Visible = true;
                        dvGraficosRiesgoInherente.Visible = false;
                        dvGraficoRiesgosParticular.Visible = false;
                        GVriesgoGlobal.Visible = true;
                        GVriesgosSaro.Visible = false;
                        dvGraficosRiesgoInherente.Visible = false;
                    }
                    /*if (ddlRiesgoInherente.SelectedValue != "---")
                    {
                        dvGraficosGeneral.Visible = false;
                        dvTablaRiesgos.Visible = true;
                        dvGraficosRiesgoInherente.Visible = true;
                        dvGraficoRiesgosParticular.Visible = false;
                        GVriesgoGlobal.Visible = false;
                        GVriesgosSaro.Visible = false;
                        dvGraficosRiesgoInherente.Visible = true;
                }*/
                    if (ddlRiesgoGlobal.SelectedValue != "---")
                    {
                        dvTablaRiesgos.Visible = true;
                        dvGraficosGeneral.Visible = false;
                        dvGraficoRiesgosParticular.Visible = true;
                        GVriesgoGlobal.Visible = false;
                        GVriesgosSaro.Visible = true;
                        dvGraficosRiesgoInherente.Visible = false;
                    }
                    Dbutton.Visible = true;
                }
                else
                {
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                }

            }
            /*string str;
            str = "window.open('ViewDetalleRiesgos/DetalleRiesgos.aspx?riesgoGlobal=" + RiesgoGlobal + "','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
            mdtGraficos();*/
        }

        private bool mtdLoadReporteCausasSinControl(ref string strErrMgs)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;
            List<clsDTOCuadroMandoRiesgosDetalle> lstReporte = new List<clsDTOCuadroMandoRiesgosDetalle>();
            clsBLLCuadroMandoRiesgosDetalle cCuadroRiesgos = new clsBLLCuadroMandoRiesgosDetalle();
            //clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            if (ddlRiesgoGlobal.SelectedValue != "---" && ddlRiesgoGlobal.SelectedValue != "")
                objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            else
                objFiltros.intRiesgoGlobal = 0;
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
            //objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (cbComparativo.Checked)
                objFiltros.strAreaRiesgo = ddlAreas.SelectedItem.Text;
            else
                objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (ddlClasificacionGeneral.SelectedValue != "---" && ddlClasificacionGeneral.SelectedValue != "")
                objFiltros.intIdClasificacionGeneral = Convert.ToInt32(ddlClasificacionGeneral.SelectedValue);
            else
                objFiltros.intIdClasificacionGeneral = 0;
            if (ddlFactorRiesgo.SelectedValue != "---" && ddlFactorRiesgo.SelectedValue != "")
                objFiltros.intIdFactor = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            else
                objFiltros.intIdFactor = 0;
            if (DDLPlanEstrategico.SelectedValue != "---" && DDLPlanEstrategico.SelectedValue != "")
            {
                if (DDLObjetivoEstrategico.SelectedValue != "---" && DDLObjetivoEstrategico.SelectedValue != "")
                {
                    objFiltros.intIdObjetivo = Convert.ToInt32(DDLObjetivoEstrategico.SelectedValue);
                }
                else
                {
                    objFiltros.intIdObjetivo = 0;
                }
            }
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            if (ddlRiesgoGlobal.SelectedValue != "---")
                objFiltros.strClasificacionGeneral = ddlRiesgoGlobal.SelectedItem.Text;
            else
                objFiltros.strClasificacionGeneral = "";
            string TipoReporte = ddlTipoReporte.SelectedValue;
            /**********************Filtros  de Consulta****************************/
            booResult = cCuadroRiesgos.mtdConsultarReporteCausasSinControl(ref strErrMgs, ref lstReporte, objFiltros, TipoReporte);
                if (lstReporte != null)
                {
                /*foreach (clsDTOCuadroMandoRiesgosDetalle objCuadro in lstReporte)
                {
                    if (objCuadro.strRiesgoInherente == "Bajo")
                    {
                        valorBajo = valorBajo + objCuadro.intCausasSinControl;
                    }
                    if (objCuadro.strRiesgoInherente == "Moderado")
                    {
                        valorModerado = valorModerado + objCuadro.intCausasSinControl;
                    }
                    if (objCuadro.strRiesgoInherente == "Alto")
                    {
                        valorAlto = valorAlto + objCuadro.intCausasSinControl;
                    }
                    if (objCuadro.strRiesgoInherente == "Extremo")
                    {
                        valorExtremo = valorExtremo + objCuadro.intCausasSinControl;
                    }
                }*/
                foreach (clsDTOCuadroMandoRiesgosDetalle objCuadro in lstReporte)
                {
                    if (objCuadro.strRiesgoInherente == "Bajo")
                    {
                        valorBajo = valorBajo + 1;
                    }
                    if (objCuadro.strRiesgoInherente == "Moderado")
                    {
                        valorModerado = valorModerado + 1;
                    }
                    if (objCuadro.strRiesgoInherente == "Alto")
                    {
                        valorAlto = valorAlto + 1;
                    }
                    if (objCuadro.strRiesgoInherente == "Extremo")
                    {
                        valorExtremo = valorExtremo + 1;
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

                    mtdViewChartSaro(dtCuadroMando);
                }

            return booResult;
        }
        private bool LoadInfoReporteRiesgosSinControl(ref string strErrMgs)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;
            List<clsDTOCuadroMandoRiesgosDetalle> lstReporte = new List<clsDTOCuadroMandoRiesgosDetalle>();
            clsBLLCuadroMandoRiesgosDetalle cCuadroRiesgos = new clsBLLCuadroMandoRiesgosDetalle();
            //clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            if (ddlRiesgoGlobal.SelectedValue != "" && ddlRiesgoGlobal.SelectedValue != "---")
                objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            else
                objFiltros.intRiesgoGlobal = 0;
            if (ddlCadenaValor.SelectedValue != "" && ddlCadenaValor.SelectedValue != "---")
                objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            else
                objFiltros.intIdCadenaValor = 0;
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
            //objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (cbComparativo.Checked)
                objFiltros.strAreaRiesgo = ddlAreas.SelectedItem.Text;
            else
                objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (ddlClasificacionGeneral.SelectedValue != "---" && ddlClasificacionGeneral.SelectedValue != "")
                objFiltros.intIdClasificacionGeneral = Convert.ToInt32(ddlClasificacionGeneral.SelectedValue);
            else
                objFiltros.intIdClasificacionGeneral = 0;
            if (ddlFactorRiesgo.SelectedValue != "---" && ddlFactorRiesgo.SelectedValue != "")
                objFiltros.intIdFactor = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            else
                objFiltros.intIdFactor = 0;
            if (DDLPlanEstrategico.SelectedValue != "---" && DDLPlanEstrategico.SelectedValue != "")
            {
                if (DDLObjetivoEstrategico.SelectedValue != "---" && DDLObjetivoEstrategico.SelectedValue != "")
                {
                    objFiltros.intIdObjetivo = Convert.ToInt32(DDLObjetivoEstrategico.SelectedValue);
                }
                else
                {
                    objFiltros.intIdObjetivo = 0;
                }
            }
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            /**********************Filtros  de Consulta****************************/
            try
            {
                booResult = cCuadroRiesgos.LoadInfoReporteRiesgosSinControl(ref strErrMgs, ref lstReporte, objFiltros);
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la consulta: " + ex, 2, "Atención");
            }
            
            if (lstReporte != null)
            {
                foreach (clsDTOCuadroMandoRiesgosDetalle objCuadro in lstReporte)
                {
                    if (objCuadro.strRiesgoInherente == "Bajo")
                    {
                        valorBajo = valorBajo + 1;
                    }
                    if (objCuadro.strRiesgoInherente == "Moderado")
                    {
                        valorModerado = valorModerado + 1;
                    }
                    if (objCuadro.strRiesgoInherente == "Alto")
                    {
                        valorAlto = valorAlto + 1;
                    }
                    if (objCuadro.strRiesgoInherente == "Extremo")
                    {
                        valorExtremo = valorExtremo + 1;
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

                mtdViewChartSaro(dtCuadroMando);
            }

            return booResult;
        }
        private bool LoadInfoReporteRiesgosPlanes(ref string strErrMgs)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            //int valorBajo = 0;
            //int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;
            List<clsDTOCuadroMandoRiesgosDetalle> lstReporte = new List<clsDTOCuadroMandoRiesgosDetalle>();
            clsBLLCuadroMandoRiesgosDetalle cCuadroRiesgos = new clsBLLCuadroMandoRiesgosDetalle();
            //clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            if (ddlRiesgoGlobal.SelectedValue != "---" && ddlRiesgoGlobal.SelectedValue != "")
                objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            else
                objFiltros.intRiesgoGlobal = 0;
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
            //objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (cbComparativo.Checked)
                objFiltros.strAreaRiesgo = ddlAreas.SelectedItem.Text;
            else
                objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (ddlClasificacionGeneral.SelectedValue != "---" && ddlClasificacionGeneral.SelectedValue != "")
                objFiltros.intIdClasificacionGeneral = Convert.ToInt32(ddlClasificacionGeneral.SelectedValue);
            else
                objFiltros.intIdClasificacionGeneral = 0;
            if (ddlFactorRiesgo.SelectedValue != "---" && ddlFactorRiesgo.SelectedValue != "")
                objFiltros.intIdFactor = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            else
                objFiltros.intIdFactor = 0;
            if (DDLPlanEstrategico.SelectedValue != "---" && DDLPlanEstrategico.SelectedValue != "")
            {
                if (DDLObjetivoEstrategico.SelectedValue != "---" && DDLObjetivoEstrategico.SelectedValue != "")
                {
                    objFiltros.intIdObjetivo = Convert.ToInt32(DDLObjetivoEstrategico.SelectedValue);
                }
                else
                {
                    objFiltros.intIdObjetivo = 0;
                }
            }
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }
            /**********************Filtros  de Consulta****************************/
            booResult = cCuadroRiesgos.LoadInfoReporteRiesgosPlanes(ref strErrMgs, ref lstReporte, objFiltros);
            if (lstReporte != null)
            {
                foreach (clsDTOCuadroMandoRiesgosDetalle objCuadro in lstReporte)
                {
                    /*if (objCuadro.strRiesgoInherente == "Bajo")
                    {
                        valorBajo = valorBajo + 1;
                    }
                    if (objCuadro.strRiesgoInherente == "Moderado")
                    {
                        valorModerado = valorModerado + 1;
                    }*/
                    if (objCuadro.strRiesgoInherente == "Alto")
                    {
                        valorAlto = valorAlto + 1;
                    }
                    if (objCuadro.strRiesgoInherente == "Extremo")
                    {
                        valorExtremo = valorExtremo + 1;
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
                /*dr["Riesgo Inherente"] = "Bajo";
                dr["Valor"] = valorBajo;
                dtCuadroMando.Rows.Add(dr);
                dr = dtCuadroMando.NewRow();
                dr["Riesgo Inherente"] = "Moderado";
                dr["Valor"] = valorModerado;
                dtCuadroMando.Rows.Add(dr);*/
                //dr = dtCuadroMando.NewRow();
                dr["Riesgo Inherente"] = "Alto";
                dr["Valor"] = valorAlto;
                dtCuadroMando.Rows.Add(dr);
                dr = dtCuadroMando.NewRow();
                dr["Riesgo Inherente"] = "Extremo";
                dr["Valor"] = valorExtremo;
                dtCuadroMando.Rows.Add(dr);

                mtdViewChartSaro(dtCuadroMando);
            }

            return booResult;
        }

        protected void ImbClean_Click(object sender, ImageClickEventArgs e)
        {
            dvGraficosGeneral.Visible = false;
            dvGraficoRiesgosParticular.Visible = false;
            dvGraficosRiesgoInherente.Visible = false;
            dvGraficoRiesgosSarlaft.Visible = false;
            dvTablaRiesgos.Visible = false;
            Dbutton.Visible = false;
            ddlTipoReporte.SelectedIndex = 0;
            ddlRiesgoGlobal.Items.Clear();
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            ddlAreas.Items.Clear();
            ddlClasificacionGeneral.Items.Clear();
            ddlFactorRiesgo.Items.Clear();
            DDLPlanEstrategico.Items.Clear();
            DDLObjetivoEstrategico.Items.Clear();
            cbComparativo.Checked = false;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            txbPerfilInicial.Text = string.Empty;
            txbPerfilFinal.Text = string.Empty;
            trHistoricoInicial.Visible = false;
            trHistoricoFinal.Visible = false;
            mtdStard();
        }
    }
}