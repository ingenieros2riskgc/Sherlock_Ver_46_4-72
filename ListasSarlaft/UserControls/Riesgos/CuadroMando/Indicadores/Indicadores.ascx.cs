using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
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
using ListasSarlaft.Classes.Utilerias;

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando.Indicadores
{
    public partial class Indicadores : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        string IdFormulario = "5035";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBprocess);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.GVindicadorRiesgo);
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
        public void mtdStard()
        {
            string strErrMsg = string.Empty;
            mtdCargarDDLs();
            if (loadListJerarquia(ref strErrMsg) != false)
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }

        #region Metodos de Carga
        private bool loadListJerarquia(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                string selectCommand = "SELECT PJO.IdHijo, PJO.IdPadre, PJO.NombreHijo, PDJ.NombreResponsable, PDJ.CorreoResponsable " +
                "FROM Parametrizacion.JerarquiaOrganizacional PJO LEFT JOIN Parametrizacion.DetalleJerarquiaOrg PDJ ON PJO.idHijo = PDJ.idHijo";
                string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
                SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
                System.Data.DataTable dtblDiscuss = new System.Data.DataTable();
                dad.Fill(dtblDiscuss);
                cbJerarquia.DataSource = dtblDiscuss;

                ddlJerarquiaO2.Items.Clear();
                ddlJerarquiaO2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "-1"));

                if (dtblDiscuss.Rows.Count > 0)
                {

                    int intCounter = 1;

                    foreach (DataRow dt in dtblDiscuss.Rows)
                    {
                        ddlJerarquiaO2.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(dt["NombreHijo"].ToString().Trim(), dt["IdHijo"].ToString().Trim()));
                        intCounter++;
                    }


                }

                cbJerarquia.DataValueField = "IdHijo";
                cbJerarquia.DataTextField = "NombreHijo";
                cbJerarquia.DataBind();
                /*for (int i = 0; i < dtblDiscuss.Rows.Count; i++)
                {
                    cbJerarquia.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtblDiscuss.Rows[i]["NombreHijo"].ToString().Trim(), dtblDiscuss.Rows[i]["IdHijo"].ToString()));
                }*/
            }
            catch (Exception ex)
            {
                strErrMsg = "Error al cargar los planes riesgo. " + ex.Message;
                booResult = true;
            }
            return booResult;
        }
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;
            if (mtdLoadDDLCadenaValor(ref strErrMsg))
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
        #endregion LoadDDL
        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            trJerarquia.Visible = false;
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
        #endregion Metodos de Carga

        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTipoReporte.SelectedValue == "1")
            {
                trEfectividad.Visible = true;
               // trJerarquia.Visible = false;
                trJerarquia2.Visible = false;
                CadenaValor.Visible = false;
                Macroproceso.Visible = false;
                Proceso.Visible = false;
                Subproceso.Visible = false;
            }
            if(ddlTipoReporte.SelectedValue == "2")
            {
                trEfectividad.Visible = false;
                //trJerarquia.Visible = true;
                trJerarquia2.Visible = true;
                CadenaValor.Visible = true;
                Macroproceso.Visible = true;
                Proceso.Visible = true;
                Subproceso.Visible = true;
                if(ddlCadenaValor.Items.Count == 0)
                    mtdCargarDDLs();
                else
                {
                    string strErrMsg = string.Empty;
                    loadListJerarquia(ref strErrMsg);
                }
                    
            }
        }
        #region LoadReportes
        private bool LoadInfoReporteIndicadorRiesgo(ref string strErrMgs)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;

            List<clsDTOCuadroMandoIndicadores> lstReporte = new List<clsDTOCuadroMandoIndicadores>();
            clsBLLCuadroMandoIndicadores cCuadroRiesgos = new clsBLLCuadroMandoIndicadores();
            //clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            //clsDTOCuadroMandoEventosFiltro objFiltros = new clsDTOCuadroMandoEventosFiltro();
            /**********************Filtros de Consulta****************************/
            /*objFiltros.dtFechaInicial = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbFechaInicial.Text));
            objFiltros.dtFechaFinal = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbFechaFinal.Text));*/
            /**********************Filtros  de Consulta****************************/
            booResult = cCuadroRiesgos.LoadInfoReporteIndicadoresRiesgos(ref strErrMgs, ref lstReporte);
            //string ListaEficacia = string.Empty;

            if (lstReporte != null)
            {
                
                System.Data.DataTable initialDataSource = new System.Data.DataTable();
                initialDataSource.Columns.Add("Riesgo", Type.GetType("System.String"));
                initialDataSource.Columns.Add("Cantidad", Type.GetType("System.String"));
               /* initialDataSource.Columns.Add("Moderado", Type.GetType("System.String"));
                initialDataSource.Columns.Add("Alto", Type.GetType("System.String"));
                initialDataSource.Columns.Add("Extremo", Type.GetType("System.String"));*/
                /*dcColumn = new DataColumn();
                dcColumn.ColumnName = "Participacion";
                dtCuadroMando.Columns.Add(dcColumn);*/
                double CantEventos = 0;
                //bool Flag = cCuadroRiesgos.GetAllEventos(ref strErrMgs, ref CantEventos);
                //Session["CantEventos"] = CantEventos;
                foreach (clsDTOCuadroMandoIndicadores objCuadro in lstReporte)
                {
                    for (int i = 0; i < cblEfectividades.Items.Count; i++)
                    {
                        if (cblEfectividades.Items[i].Selected)
                        {
                            if (objCuadro.strNombreRiesgo == cblEfectividades.Items[i].Value.ToString().Trim() && cblEfectividades.Items[i].Value.ToString().Trim() == "Bajo")
                            {
                                valorBajo++;
                            }
                            if (objCuadro.strNombreRiesgo == cblEfectividades.Items[i].Value.ToString().Trim() && cblEfectividades.Items[i].Value.ToString().Trim() == "Moderado")
                            {
                                valorModerado++;
                            }
                            if (objCuadro.strNombreRiesgo == cblEfectividades.Items[i].Value.ToString().Trim() && cblEfectividades.Items[i].Value.ToString().Trim() == "Alto")
                            {
                                valorAlto++;
                            }
                            if (objCuadro.strNombreRiesgo == cblEfectividades.Items[i].Value.ToString().Trim() && cblEfectividades.Items[i].Value.ToString().Trim() == "Extremo")
                            {
                                valorExtremo++;
                            }
                        }
                    }
                    /*DataRow dr;
                    dr = dtCuadroMando.NewRow();
                    dr["Estado"] = objCuadro.strDescripcionEstado;
                    dr["Cantidad"] = objCuadro.intNumEventos;
                    double Participacion = Convert.ToDouble(objCuadro.intNumEventos / CantEventos) * 100;
                    double d = Math.Round(Participacion, 2);
                    dr["Participacion"] = d;
                    dtCuadroMando.Rows.Add(dr);*/
                }
                for (int i = 0; i < cblEfectividades.Items.Count; i++)
                {
                    if (cblEfectividades.Items[i].Selected)
                    {
                        DataRow dr1 = initialDataSource.NewRow();
                        if (cblEfectividades.Items[i].Value.ToString().Trim() == "Bajo")
                        {
                            dr1["Riesgo"] = "Bajo";
                            dr1["Cantidad"] = valorBajo;
                            initialDataSource.Rows.Add(dr1);
                        }
                        if(cblEfectividades.Items[i].Value.ToString().Trim() == "Moderado")
                        {
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Moderado";
                            dr1["Cantidad"] = valorModerado;
                            initialDataSource.Rows.Add(dr1);
                        }
                        if (cblEfectividades.Items[i].Value.ToString().Trim() == "Alto")
                        {
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Alto";
                            dr1["Cantidad"] = valorAlto;
                            initialDataSource.Rows.Add(dr1);
                        }
                        if (cblEfectividades.Items[i].Value.ToString().Trim() == "Extremo")
                        {
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Extremo";
                            dr1["Cantidad"] = valorExtremo;
                            initialDataSource.Rows.Add(dr1);
                        }
                    }
                }
                mtdViewChartIndicadorRiesgo(initialDataSource);
            }

            return booResult;
        }
        private bool LoadInfoReporteIndicadorProcesos(ref string strErrMgs)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;

            List<clsDTOCuadroMandoIndicadores> lstReporte = new List<clsDTOCuadroMandoIndicadores>();
            clsBLLCuadroMandoIndicadores cCuadroRiesgos = new clsBLLCuadroMandoIndicadores();
            clsDTOCuadroMandoIndicadorFiltros objFiltros = new clsDTOCuadroMandoIndicadorFiltros();
            //clsDTOCuadroMandoEventosFiltro objFiltros = new clsDTOCuadroMandoEventosFiltro();
            /**********************Filtros de Consulta****************************/
            if (ddlCadenaValor.SelectedValue != "---" && ddlCadenaValor.Items.Count > 0)
                objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            else
                objFiltros.intIdCadenaValor = 0;
            if (ddlMacroproceso.SelectedValue != "---" && ddlMacroproceso.Items.Count > 0)
                objFiltros.intIdMacroproceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            else
                objFiltros.intIdMacroproceso = 0;
            if (ddlProceso.SelectedValue != "---" && ddlProceso.Items.Count > 0)
                objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            else
                objFiltros.intIdProceso = 0;
            if (ddlSubproceso.SelectedValue != "---" && ddlSubproceso.Items.Count > 0)
                objFiltros.intIdSubproceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            else
                objFiltros.intIdSubproceso = 0;
            //for (int i = 0; i < cbJerarquia.Items.Count; i++)
            //{
            //    if (cbJerarquia.Items[i].Selected)
            //    {
            //        objFiltros.strJerarquia = objFiltros.strJerarquia + "," + cbJerarquia.Items[i].Value;
            //    }
            //}
            objFiltros.strJerarquia = objFiltros.strJerarquia + "," + ddlJerarquiaO2.SelectedValue.ToString().Trim();

           /**********************Filtros  de Consulta****************************/
           booResult = cCuadroRiesgos.LoadInfoReporteIndicadoresProceso(ref strErrMgs, ref lstReporte, objFiltros);
            //string ListaEficacia = string.Empty;

            if (lstReporte != null)
            {

                System.Data.DataTable initialDataSource = new System.Data.DataTable();
                initialDataSource.Columns.Add("Riesgo", Type.GetType("System.String"));
                initialDataSource.Columns.Add("Cantidad", Type.GetType("System.String"));
                /* initialDataSource.Columns.Add("Moderado", Type.GetType("System.String"));
                 initialDataSource.Columns.Add("Alto", Type.GetType("System.String"));
                 initialDataSource.Columns.Add("Extremo", Type.GetType("System.String"));*/
                /*dcColumn = new DataColumn();
                dcColumn.ColumnName = "Participacion";
                dtCuadroMando.Columns.Add(dcColumn);*/
                double CantEventos = 0;
                //bool Flag = cCuadroRiesgos.GetAllEventos(ref strErrMgs, ref CantEventos);
                //Session["CantEventos"] = CantEventos;
                foreach (clsDTOCuadroMandoIndicadores objCuadro in lstReporte)
                {
                    
                            if (objCuadro.strNombreRiesgo ==  "Bajo")
                            {
                                valorBajo++;
                            }
                            if (objCuadro.strNombreRiesgo ==  "Moderado")
                            {
                                valorModerado++;
                            }
                            if (objCuadro.strNombreRiesgo == "Alto")
                            {
                                valorAlto++;
                            }
                            if (objCuadro.strNombreRiesgo ==  "Extremo")
                            {
                                valorExtremo++;
                            }
                       
                    /*DataRow dr;
                    dr = dtCuadroMando.NewRow();
                    dr["Estado"] = objCuadro.strDescripcionEstado;
                    dr["Cantidad"] = objCuadro.intNumEventos;
                    double Participacion = Convert.ToDouble(objCuadro.intNumEventos / CantEventos) * 100;
                    double d = Math.Round(Participacion, 2);
                    dr["Participacion"] = d;
                    dtCuadroMando.Rows.Add(dr);*/
                }
                
                        DataRow dr1 = initialDataSource.NewRow();
                       
                            dr1["Riesgo"] = "Bajo";
                            dr1["Cantidad"] = valorBajo;
                            initialDataSource.Rows.Add(dr1);
                       
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Moderado";
                            dr1["Cantidad"] = valorModerado;
                            initialDataSource.Rows.Add(dr1);
                        
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Alto";
                            dr1["Cantidad"] = valorAlto;
                            initialDataSource.Rows.Add(dr1);
                        
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Extremo";
                            dr1["Cantidad"] = valorExtremo;
                            initialDataSource.Rows.Add(dr1);
                        
                  
                mtdViewChartIndicadorRiesgo(initialDataSource);
            }

            return booResult;
        }
        private bool LoadInfoReporteIndicadorResponsable(ref string strErrMgs)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;

            List<clsDTOCuadroMandoIndicadores> lstReporte = new List<clsDTOCuadroMandoIndicadores>();
            clsBLLCuadroMandoIndicadores cCuadroRiesgos = new clsBLLCuadroMandoIndicadores();
            //clsDTOCuadroMandoIndicadorFiltros objFiltros = new clsDTOCuadroMandoIndicadorFiltros();
            //clsDTOCuadroMandoEventosFiltro objFiltros = new clsDTOCuadroMandoEventosFiltro();
            /**********************Filtros de Consulta****************************/
            string IdJerarquia = string.Empty;
            for (int i = 0; i < cbJerarquia.Items.Count; i++)
            {
                if (cbJerarquia.Items[i].Selected)
                {
                    IdJerarquia += cbJerarquia.Items[i].Value+",";
                }
            }
            string IdJerarquia2 = ddlJerarquiaO2.SelectedValue.ToString().Trim() + ",";
            /**********************Filtros  de Consulta****************************/
            booResult = cCuadroRiesgos.LoadInfoReporteIndicadorResponsable(ref strErrMgs, ref lstReporte, IdJerarquia2);
            //string ListaEficacia = string.Empty;

            if (lstReporte != null)
            {

                System.Data.DataTable initialDataSource = new System.Data.DataTable();
                initialDataSource.Columns.Add("Riesgo", Type.GetType("System.String"));
                initialDataSource.Columns.Add("Cantidad", Type.GetType("System.String"));
                /* initialDataSource.Columns.Add("Moderado", Type.GetType("System.String"));
                 initialDataSource.Columns.Add("Alto", Type.GetType("System.String"));
                 initialDataSource.Columns.Add("Extremo", Type.GetType("System.String"));*/
                /*dcColumn = new DataColumn();
                dcColumn.ColumnName = "Participacion";
                dtCuadroMando.Columns.Add(dcColumn);*/
                double CantEventos = 0;
                //bool Flag = cCuadroRiesgos.GetAllEventos(ref strErrMgs, ref CantEventos);
                //Session["CantEventos"] = CantEventos;
                foreach (clsDTOCuadroMandoIndicadores objCuadro in lstReporte)
                {

                    if (objCuadro.strNombreRiesgo == "Bajo")
                    {
                        valorBajo++;
                    }
                    if (objCuadro.strNombreRiesgo == "Moderado")
                    {
                        valorModerado++;
                    }
                    if (objCuadro.strNombreRiesgo == "Alto")
                    {
                        valorAlto++;
                    }
                    if (objCuadro.strNombreRiesgo == "Extremo")
                    {
                        valorExtremo++;
                    }

                    /*DataRow dr;
                    dr = dtCuadroMando.NewRow();
                    dr["Estado"] = objCuadro.strDescripcionEstado;
                    dr["Cantidad"] = objCuadro.intNumEventos;
                    double Participacion = Convert.ToDouble(objCuadro.intNumEventos / CantEventos) * 100;
                    double d = Math.Round(Participacion, 2);
                    dr["Participacion"] = d;
                    dtCuadroMando.Rows.Add(dr);*/
                }
                
                        DataRow dr1 = initialDataSource.NewRow();
                        
                            dr1["Riesgo"] = "Bajo";
                            dr1["Cantidad"] = valorBajo;
                            initialDataSource.Rows.Add(dr1);
                        
                        
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Moderado";
                            dr1["Cantidad"] = valorModerado;
                            initialDataSource.Rows.Add(dr1);
                       
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Alto";
                            dr1["Cantidad"] = valorAlto;
                            initialDataSource.Rows.Add(dr1);
                        
                            dr1 = initialDataSource.NewRow();
                            dr1["Riesgo"] = "Extremo";
                            dr1["Cantidad"] = valorExtremo;
                            initialDataSource.Rows.Add(dr1);
                        
                    
                mtdViewChartIndicadorRiesgo(initialDataSource);
            }

            return booResult;
        }
        #endregion LoadReportes
        #region Generacion Reportes
        public void mtdViewChartIndicadorRiesgo(System.Data.DataTable dtInfo)
        {
            GVindicadorRiesgo.DataSource = dtInfo;
            GVindicadorRiesgo.DataBind();
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtInfo.Rows[i][1]);
                Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
            }
            ChartIndicadorRiesgo.Series[0].Points.DataBindXY(x, y);
            ChartIndicadorRiesgo.Series[0].Name = "Indicadores por Riesgo Asociado";
            ChartIndicadorRiesgo.Series[0].XValueMember = "Riesgo";
            ChartIndicadorRiesgo.Series[0].YValueMembers = "Cantidad";
            ChartIndicadorRiesgo.Series[0].ChartType = SeriesChartType.Column;
            ChartIndicadorRiesgo.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            ChartIndicadorRiesgo.Legends[0].Enabled = true;
            //ChartEficacia.Titles.Add("NewTitle");
            ChartIndicadorRiesgo.Titles[0].Text = "Indicadores Totales: " + Total;
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartIndicadorRiesgo.Series)
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
        #endregion Generacion Reportes

        protected void IBprocess_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMgs = string.Empty;
            if (ddlTipoReporte.SelectedValue == "1")
            {
                if (cblEfectividades.SelectedValue != "")
                {
                    if (!LoadInfoReporteIndicadorRiesgo(ref strErrMgs))
                    {
                        omb.ShowMessage(strErrMgs, 2, "Atención");
                    }
                    else
                    {
                        dvTablaIndicadorRiesgo.Visible = true;
                        dvGraficoIndicadorRiesgo.Visible = true;
                        Dbutton.Visible = true;
                    }
                }
                else
                {
                    omb.ShowMessage("Se debe seleccionar un filtro", 2, "Atención");
                }

            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                //if(ddlCadenaValor.SelectedValue != "0")
                //{
                //    if (!LoadInfoReporteIndicadorProcesos(ref strErrMgs))
                //        omb.ShowMessage(strErrMgs, 2, "Atención");
                //    else
                //    {
                //        dvTablaIndicadorRiesgo.Visible = true;
                //        dvGraficoIndicadorRiesgo.Visible = true;
                //        Dbutton.Visible = true;
                //    }
                //}
                //    else
                //{
                //    if(ddlJerarquiaO2.SelectedValue != "-1")
                //    {

                //        if (!LoadInfoReporteIndicadorResponsable(ref strErrMgs))
                //            omb.ShowMessage(strErrMgs, 2, "Atención");
                //        else
                //        {
                //            dvTablaIndicadorRiesgo.Visible = true;
                //            dvGraficoIndicadorRiesgo.Visible = true;
                //            Dbutton.Visible = true;
                //        }
                //    }
                //    else
                //    {
                //        omb.ShowMessage("Se debe seleccionar un filtro", 2, "Atención");
                //    }
                if (!LoadInfoReporteIndicadorProcesos(ref strErrMgs))
                {
                    omb.ShowMessage(strErrMgs, 2, "Atención");
                }
                else
                {
                    dvTablaIndicadorRiesgo.Visible = true;
                    dvGraficoIndicadorRiesgo.Visible = true;
                    Dbutton.Visible = true;
                }
            }      


        }
        

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty ;
            if (ddlTipoReporte.SelectedValue == "1")
                LoadInfoReporteIndicadorRiesgo(ref strErrMsg);
            if(ddlTipoReporte.SelectedValue == "2")
            {
                if (ddlCadenaValor.SelectedValue != "0")
                {
                    LoadInfoReporteIndicadorProcesos(ref strErrMsg);
                }
                else
                {
                    LoadInfoReporteIndicadorResponsable(ref strErrMsg);
                }
            }
            // Creamos el tipo de Font que vamos utilizar
            Tools tools = new Tools();
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
            pdfDocument.AddTitle("Cuadro de Mando Indicador");
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
            PdfPTable pdfTableData = new PdfPTable(2);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Tipo Reporte:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlTipoReporte.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            if (ddlTipoReporte.SelectedValue == "1")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Severidad:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                string Efectividad = string.Empty;
                for (int i = 0; i < cblEfectividades.Items.Count; i++)
                {
                    if (cblEfectividades.Items[i].Selected)
                    {
                        Efectividad += cblEfectividades.Items[i].Text + ",";
                    }
                }
                pdfCellEncabezado = new PdfPCell(new Phrase(Efectividad));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                if (ddlCadenaValor.SelectedValue != "0" && ddlCadenaValor.SelectedValue != "")
                {
                    pdfCellEncabezado = new PdfPCell(new Phrase("Cadena Valor:", font1));
                    pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                    pdfTableData.AddCell(pdfCellEncabezado);
                    pdfCellEncabezado = new PdfPCell(new Phrase(ddlCadenaValor.SelectedItem.Text));
                    pdfTableData.AddCell(pdfCellEncabezado);
                    pdfCellEncabezado = new PdfPCell(new Phrase("Macroproceso:", font1));
                    pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                    pdfTableData.AddCell(pdfCellEncabezado);
                    if(ddlMacroproceso.Items.Count > 0)
                        pdfCellEncabezado = new PdfPCell(new Phrase(ddlMacroproceso.SelectedItem.Text));
                    else
                        pdfCellEncabezado = new PdfPCell(new Phrase("---"));
                    pdfTableData.AddCell(pdfCellEncabezado);
                    pdfCellEncabezado = new PdfPCell(new Phrase("Proceso:", font1));
                    pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                    pdfTableData.AddCell(pdfCellEncabezado);
                    if (ddlProceso.Items.Count > 0)
                        pdfCellEncabezado = new PdfPCell(new Phrase(ddlProceso.SelectedItem.Text));
                    else
                        pdfCellEncabezado = new PdfPCell(new Phrase("---"));
                    pdfTableData.AddCell(pdfCellEncabezado);
                    pdfCellEncabezado = new PdfPCell(new Phrase("Subproceso:", font1));
                    pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                    pdfTableData.AddCell(pdfCellEncabezado);
                    if (ddlSubproceso.Items.Count > 0)
                        pdfCellEncabezado = new PdfPCell(new Phrase(ddlSubproceso.SelectedItem.Text));
                    else
                        pdfCellEncabezado = new PdfPCell(new Phrase("---"));
                    pdfTableData.AddCell(pdfCellEncabezado);
                }
                else
                {
                    pdfCellEncabezado = new PdfPCell(new Phrase("Jerarquias Organizacionales:", font1));
                    pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                    pdfTableData.AddCell(pdfCellEncabezado);
                    string Jerarquia = string.Empty;
                    for (int i = 0; i < cbJerarquia.Items.Count; i++)
                    {
                        if (cbJerarquia.Items[i].Selected)
                        {
                            Jerarquia += cbJerarquia.Items[i].Text + ",";
                        }
                    }
                    pdfCellEncabezado = new PdfPCell(new Phrase(Jerarquia));
                    pdfTableData.AddCell(pdfCellEncabezado);
                }
                    
                
                
            }


            #endregion
            MemoryStream streamControles = new MemoryStream();
            //MemoryStream streamRiesgosSarlaft = new MemoryStream();
            
            ChartIndicadorRiesgo.SaveImage(streamControles, ChartImageFormat.Png);
            
           

            PdfPTable pdftblImageGraficoControles = new PdfPTable(1);


            iTextSharp.text.Image imagenGraficoControles = iTextSharp.text.Image.GetInstance(streamControles.GetBuffer());
            //iTextSharp.text.Image imagenGraficoSarlaft = iTextSharp.text.Image.GetInstance(streamRiesgosSarlaft.GetBuffer());

            PdfPCell pdfcellImageGraficoControles = new PdfPCell(imagenGraficoControles, true);
            //PdfPCell pdfcellImageGraficoRiesgosSarlaft = new PdfPCell(imagenGraficoSarlaft, true);
            pdftblImageGraficoControles.AddCell(pdfcellImageGraficoControles);
            //pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoControles);


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




            pdfDocument.Add(pdftblImageGraficoControles);
            pdfDocument.Add(Chunk.NEWLINE);




            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=CuadroMandoIndicador.pdf");
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
                LoadInfoReporteIndicadorRiesgo(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "2")
            {
                if (ddlCadenaValor.SelectedValue != "0")
                {
                    LoadInfoReporteIndicadorProcesos(ref strErrMsg);
                }
                else
                {
                    LoadInfoReporteIndicadorResponsable(ref strErrMsg);
                }
            }
            string tmpChartNameControl = "Grafico" + ddlTipoReporte.SelectedValue + ".jpg";
            string imgPathControl = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartNameControl;
            ChartIndicadorRiesgo.SaveImage(imgPathControl);
            string imgPathCotrolLocal = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartNameControl);
            //string imgPathSarlaftLocal = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartNameRiesgosSarlaft);
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("Tipo reporte:");
            if (ddlTipoReporte.SelectedValue == "1")
            {
                gridEncabezado.Columns.Add("Severidad:");
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                    if (ddlCadenaValor.SelectedValue != "0" && ddlCadenaValor.SelectedValue != "")
                    {
                        gridEncabezado.Columns.Add("Cadena Valor:");
                        gridEncabezado.Columns.Add("Macroproceso:");
                        gridEncabezado.Columns.Add("Proceso:");
                        gridEncabezado.Columns.Add("Subproceso:");
                    }
                    else
                    {
                        gridEncabezado.Columns.Add("Jeraraquias Organizacionales:");
                    }
                
            }


            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Tipo reporte:"] = ddlTipoReporte.SelectedItem.Text;

            if (ddlTipoReporte.SelectedValue == "1")
            {
                string Efectividad = string.Empty;
                for (int ident = 0; ident < cblEfectividades.Items.Count; ident++)
                {
                    if (cblEfectividades.Items[ident].Selected)
                    {
                        Efectividad += cblEfectividades.Items[ident].Text + ",";
                    }
                }
                rowEncabezado["Severidad:"] = Efectividad;
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                if (ddlCadenaValor.SelectedValue != "0" && ddlCadenaValor.SelectedValue != "")
                {
                    rowEncabezado["Cadena Valor:"] = ddlCadenaValor.SelectedItem.Text;
                    if(ddlMacroproceso.Items.Count > 0)
                        rowEncabezado["Macroproceso:"] = ddlMacroproceso.SelectedItem.Text;
                    else
                        rowEncabezado["Macroproceso:"] = "--";
                    if (ddlProceso.Items.Count > 0)
                        rowEncabezado["Proceso:"] = ddlProceso.SelectedItem.Text;
                    else
                        rowEncabezado["Proceso:"] = "---";
                    if (ddlSubproceso.Items.Count > 0)
                        rowEncabezado["Subproceso:"] = ddlSubproceso.SelectedItem.Text;
                    else
                        rowEncabezado["Subproceso:"] = "---";
                }
                else
                {
                    string Jerarquia = string.Empty;
                    for (int ident = 0; ident < cbJerarquia.Items.Count; ident++)
                    {
                        if (cbJerarquia.Items[ident].Selected)
                        {
                            Jerarquia += cbJerarquia.Items[ident].Text + ",";
                        }
                    }
                    rowEncabezado["Jeraraquias Organizacionales:"] = Jerarquia;
                }
                    
            }
            gridEncabezado.Rows.Add(rowEncabezado);
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=CuadroMandoIndicadores.xls;");
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
            


            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            string headerTableSaro = @"<Table><tr><td><img src='" + imgPathCotrolLocal + @"' \></td></tr></Table>";
            Response.Write(headerTableSaro);
            Response.Write(stringWrite.ToString());
            Response.Write("\n");
            Response.Write("\n");



            Response.End();

        }
        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            dvTablaIndicadorRiesgo.Visible = false;
            cblEfectividades.ClearSelection();
            dvGraficoIndicadorRiesgo.Visible = false;
            trEfectividad.Visible = false;
            CadenaValor.Visible = false;
            Macroproceso.Visible = false;
            Proceso.Visible = false;
            Subproceso.Visible = false;
            ddlTipoReporte.SelectedIndex = 0;
            Dbutton.Visible = false;
            
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();

            trJerarquia.Visible = false;
            cbJerarquia.Items.Clear();
            mtdStard();
        }

        protected void GVindicadorRiesgo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str;
            int RowGridRiesgos = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVindicadorRiesgo.Rows[RowGridRiesgos];
            string severidad = row.Cells[0].Text;
            string cadenavalor = string.Empty;
            string macroproceso = string.Empty;
            string proceso = string.Empty;
            string subproceso = string.Empty;
            string jerarquias = string.Empty;
            if (ddlCadenaValor.SelectedValue != "0")
            {
                cadenavalor = ddlCadenaValor.SelectedValue;
            }else
            {
                cadenavalor = "0";
            }
            if (ddlMacroproceso.Items.Count > 0)
            {
                if (ddlMacroproceso.SelectedValue != "0")
                    macroproceso = ddlMacroproceso.SelectedValue;
                else
                    macroproceso = "0";
            }
            else
                macroproceso = "0";
            if (ddlProceso.Items.Count > 0)
            {
                if (ddlProceso.SelectedValue != "0")
                    proceso = ddlProceso.SelectedValue;
                else
                    proceso = "0";
            }
            else
                proceso = "0";
            if (ddlSubproceso.Items.Count > 0)
            {
                if (ddlSubproceso.SelectedValue != "0")
                    subproceso = ddlSubproceso.SelectedValue;
                else
                    subproceso = "0";
            }
            else
                subproceso = "0";
            for (int i = 0; i < cbJerarquia.Items.Count; i++)
            {
                if (cbJerarquia.Items[i].Selected)
                {
                    jerarquias += cbJerarquia.Items[i].Value + ",";
                }
            }
            str = "window.open('ViewDetalleIndicador/ViewDetalleIndicador.aspx?severidad=" + severidad + "&cadenavalor="+cadenavalor+"&macroproceso="+macroproceso+"&proceso="+proceso+"&subproceso="+subproceso+"&jerarquias="+jerarquias+"','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue == "1")
                LoadInfoReporteIndicadorRiesgo(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "2")
            {
                if (ddlCadenaValor.SelectedValue != "0")
                {
                    LoadInfoReporteIndicadorProcesos(ref strErrMsg);
                }
                else
                {
                    LoadInfoReporteIndicadorResponsable(ref strErrMsg);
                }
            }

        }

        protected void cbJerarquia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CadenaValor.Visible = false;
            Macroproceso.Visible = false;
            Proceso.Visible = false;
            Subproceso.Visible = false;
        }

        protected void ImbClean_Click(object sender, ImageClickEventArgs e)
        {
            dvTablaIndicadorRiesgo.Visible = false;
            cblEfectividades.ClearSelection();
            dvGraficoIndicadorRiesgo.Visible = false;
            trEfectividad.Visible = false;
            CadenaValor.Visible = false;
            Macroproceso.Visible = false;
            Proceso.Visible = false;
            Subproceso.Visible = false;
            ddlTipoReporte.SelectedIndex = 0;
            Dbutton.Visible = false;

            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();

            trJerarquia.Visible = false;
            cbJerarquia.Items.Clear();
            mtdStard();
        }

        protected void GVindicadorRiesgo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
    }
}