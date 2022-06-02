using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using System.IO;
using Microsoft.Security.Application;
using System.Configuration;

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando.Controles.ViewDetalleControl
{
    public partial class ViewDetalleControl : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Efectividad = string.Empty;
            int IdJerarquia = 0;
            string Jerarquia = string.Empty;
            //string riesgoGlobal = string.Empty;
            if (Request["Efectividad"] != null)
                Efectividad = Request["Efectividad"].ToString();
            if (Request["IdJerarquia"] != null)
                IdJerarquia = Convert.ToInt32(Request["IdJerarquia"].ToString());
            if (Request["Jerarquia"] != null)
                Jerarquia = Request["Jerarquia"].ToString();
            Session["Efectividad"] = Efectividad;
            Session["IdJerarquia"] = IdJerarquia;
            Session["Jerarquia"] = Jerarquia;
            lblEfectividad.Text = Efectividad;
            string strErrMsg = string.Empty;
            bool booResult = false;
            booResult = mtdLoadDataControl(ref strErrMsg);
            if (booResult == false)
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }

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
        #endregion
        public bool mtdLoadDataControl(ref string strErrMsg)
        {
            bool booResult = false;
            string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;
            List<clsDTOCuadroMandoControles> lstReporte = new List<clsDTOCuadroMandoControles>();
            clsBLLCuadroMandoControles cCuadroControles = new clsBLLCuadroMandoControles();
            clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            /*if (Session["riesgoGlobal"] != null)
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                int idRiesgoGlobal = 0;
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    if (dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim() == Session["riesgoGlobal"].ToString())
                        idRiesgoGlobal = Convert.ToInt32(dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString());
                }
                objFiltros.intRiesgoGlobal = idRiesgoGlobal;
            }
            else
            {
                objFiltros.intRiesgoGlobal = 0;
            }
            if (Request["IdRiesgoGlobal"] != null)
            {
                objFiltros.intRiesgoGlobal = Convert.ToInt32(Request["IdRiesgoGlobal"].ToString());
            }
            else
            {
                objFiltros.intRiesgoGlobal = 0;
            }
            if (Request["IdCadenaValor"] != null)
                objFiltros.intIdCadenaValor = Convert.ToInt32(Request["IdCadenaValor"].ToString());
            else
                objFiltros.intIdCadenaValor = 0;
            if (Request["IdMacroproceso"] != null)
                objFiltros.intIdMacroProceso = Convert.ToInt32(Request["IdMacroproceso"].ToString());
            else
                objFiltros.intIdMacroProceso = 0;
            if (Request["IdProceso"] != null)
                objFiltros.intIdProceso = Convert.ToInt32(Request["IdProceso"].ToString());
            else
                objFiltros.intIdProceso = 0;
            if (Request["IdSubproceso"] != null)
                objFiltros.intIdSubProceso = Convert.ToInt32(Request["IdSubproceso"].ToString());
            else
                objFiltros.intIdSubProceso = 0;
            if (Request["Area"] != null)
                objFiltros.intArea = Convert.ToInt32(Request["Area"].ToString());
            else
                objFiltros.intArea = 0;
            if (Request["IdClasificacionRiesgo"] != null)
                objFiltros.intIdClasificacionGeneral = Convert.ToInt32(Request["IdClasificacionRiesgo"].ToString());
            else
                objFiltros.intIdClasificacionGeneral = 0;
            if (Request["IdFactor"] != null)
                objFiltros.intIdFactor = Convert.ToInt32(Request["IdFactor"].ToString());
            else
                objFiltros.intIdFactor = 0;
            if (Request["IdObjetivo"] != null)
                objFiltros.intIdObjetivo = Convert.ToInt32(Request["IdObjetivo"].ToString());
            else
                objFiltros.intIdObjetivo = 0;
            if (Request["FechaHistoricoInicial"] != null)
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(Request["FechaHistoricoInicial"].ToString());
            if (Request["FechaHistoricoFinal"] != null)
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(Request["FechaHistoricoFinal"].ToString());*/
            /**********************Filtros  de Consulta****************************/
            int IdJerarquia = 0;
            if (Session["IdJerarquia"] != null && Session["IdJerarquia"].ToString() != "0")
                IdJerarquia = Convert.ToInt32(Session["IdJerarquia"].ToString());
            if (Request["Jerarquia"] != null && Session["Jerarquia"].ToString() != "0")
                IdJerarquia = Convert.ToInt32(Request["Jerarquia"].ToString());
            int valorTotalRiesgo = 0;
            booResult = cCuadroControles.LoadInfoReporteControles(ref strErrMgs, ref lstReporte, IdJerarquia, ref valorTotalRiesgo);
            if (booResult == true)
            {
                /*System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                DataColumn dcColumn;

                dcColumn = new DataColumn();
                dcColumn.ColumnName = "CodigoRiesgo";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "NombreRiesgo";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "IdProbabilidadResidual";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "IdImpactoResidual";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "RiesgoInherente";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "CantidadControles";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "CantidadEventos";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "CausasSinControl";
                dtCuadroMando.Columns.Add(dcColumn);
                foreach (clsDTOCuadroMandoRiesgosDetalle objCuadro in lstReporte)
                {
                    if (objCuadro.strRiesgoInherente == riesgo)
                    {
                        DataRow dr;
                        dr = dtCuadroMando.NewRow();
                        dr["CodigoRiesgo"] = objCuadro.strCodigoRiesgo;
                        dr["NombreRiesgo"] = objCuadro.strNombreRiesgo;
                        dr["IdProbabilidadResidual"] = objCuadro.intIdProbabilidadResidual;
                        dr["IdImpactoResidual"] = objCuadro.intIdImpactoResidual;
                        dr["RiesgoInherente"] = objCuadro.strRiesgoInherente;
                        dr["CantidadControles"] = objCuadro.intCantControl;
                        dr["CantidadEventos"] = objCuadro.intCantEvento;
                        dr["CausasSinControl"] = objCuadro.intCausasSinControl;
                        dtCuadroMando.Rows.Add(dr);
                    }

                }*/
                if (Request["Jerarquia"] != null && Request["Jerarquia"].ToString() != "")
                {
                    mtdLoadGridControlesJeraraquia();
                    mtdLoadGridControlesJeraraquia(lstReporte);
                    gvControlesJeraraquia.PageIndex = pagIndex;
                    gvControlesJeraraquia.DataSource = InfoGrid;
                    gvControlesJeraraquia.DataBind();
                }
                else
                {
                    mtdLoadGridControles();
                    mtdLoadGridControles(lstReporte);
                    GVcontroles.PageIndex = pagIndex;
                    GVcontroles.DataSource = InfoGrid;
                    GVcontroles.DataBind();
                }
                //lblCantRiesgo.Text = valorTotalRiesgo.ToString();
            }
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridControles()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdControl", typeof(string));
            grid.Columns.Add("strCodigoControl", typeof(string));
            grid.Columns.Add("strNombreControl", typeof(string));
            grid.Columns.Add("strEfectividad", typeof(string));
            grid.Columns.Add("intCantRiesgo", typeof(string));

            GVcontroles.DataSource = grid;
            GVcontroles.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridControles(List<clsDTOCuadroMandoControles> lstReporte)
        {
            string strErrMsg = String.Empty;
            int cant = 0;
            foreach (clsDTOCuadroMandoControles objCuadro in lstReporte)
            {
                if (Session["Efectividad"] != null && Session["Efectividad"].ToString() != "")
                {
                    if (objCuadro.strEfectividad == Session["Efectividad"].ToString())
                    {
                        cant = cant + Convert.ToInt32(objCuadro.intCantRiesgo.ToString());
                        InfoGrid.Rows.Add(new Object[] {
                        //objCuadro.intIdRiesgo.ToString().Trim(),
                        objCuadro.intIdControl.ToString().Trim(),
                    objCuadro.strCodigoControl.ToString().Trim(),
                    objCuadro.strNombreControl.ToString().Trim(),
                    objCuadro.strEfectividad.ToString().Trim(),
                    objCuadro.intCantRiesgo.ToString().Trim()
                    });
                    }
                }
                /*else
                {
                    cant++;
                    InfoGrid.Rows.Add(new Object[] {
                        objCuadro.intIdRiesgo.ToString().Trim(),
                    objCuadro.strCodigoRiesgo.ToString().Trim(),
                    objCuadro.strNombreRiesgo.ToString().Trim(),
                    objCuadro.intIdProbabilidadResidual.ToString().Trim(),
                    objCuadro.intIdImpactoResidual.ToString().Trim(),
                    objCuadro.strRiesgoInherente.ToString().Trim(),
                    objCuadro.intCantControl.ToString().Trim(),
                    objCuadro.intCantEvento.ToString().Trim(),
                    objCuadro.intCausasSinControl.ToString().Trim()
                    });
                }*/

            }
            lblCantRiesgo.Text = cant.ToString();
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridControlesJeraraquia()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdControl", typeof(string));
            grid.Columns.Add("strCodigoControl", typeof(string));
            grid.Columns.Add("strNombreControl", typeof(string));
            grid.Columns.Add("strEfectividad", typeof(string));
            grid.Columns.Add("intCantRiesgo", typeof(string));
            grid.Columns.Add("strResponsable", typeof(string));

            gvControlesJeraraquia.DataSource = grid;
            gvControlesJeraraquia.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridControlesJeraraquia(List<clsDTOCuadroMandoControles> lstReporte)
        {
            string strErrMsg = String.Empty;
            int cant = 0;
            foreach (clsDTOCuadroMandoControles objCuadro in lstReporte)
            {
                if (Session["Efectividad"] != null && Session["Efectividad"].ToString() != "")
                {
                    if (objCuadro.strEfectividad == Session["Efectividad"].ToString())
                    {
                        cant = cant + Convert.ToInt32(objCuadro.intCantRiesgo.ToString());
                        InfoGrid.Rows.Add(new Object[] {
                        //objCuadro.intIdRiesgo.ToString().Trim(),
                        objCuadro.intIdControl.ToString().Trim(),
                    objCuadro.strCodigoControl.ToString().Trim(),
                    objCuadro.strNombreControl.ToString().Trim(),
                    objCuadro.strEfectividad.ToString().Trim(),
                    objCuadro.intCantRiesgo.ToString().Trim()
                    });
                    }
                }
                /*else
                {
                    cant++;
                    InfoGrid.Rows.Add(new Object[] {
                        objCuadro.intIdRiesgo.ToString().Trim(),
                    objCuadro.strCodigoRiesgo.ToString().Trim(),
                    objCuadro.strNombreRiesgo.ToString().Trim(),
                    objCuadro.intIdProbabilidadResidual.ToString().Trim(),
                    objCuadro.intIdImpactoResidual.ToString().Trim(),
                    objCuadro.strRiesgoInherente.ToString().Trim(),
                    objCuadro.intCantControl.ToString().Trim(),
                    objCuadro.intCantEvento.ToString().Trim(),
                    objCuadro.intCausasSinControl.ToString().Trim()
                    });
                }*/
                if(Session["Jerarquia"] != null && Session["Jerarquia"].ToString() != "")
                {
                    cant = cant + Convert.ToInt32(objCuadro.intCantRiesgo.ToString());
                    InfoGrid.Rows.Add(new Object[] {
                        //objCuadro.intIdRiesgo.ToString().Trim(),
                        objCuadro.intIdControl.ToString().Trim(),
                    objCuadro.strCodigoControl.ToString().Trim(),
                    objCuadro.strNombreControl.ToString().Trim(),
                    objCuadro.strEfectividad.ToString().Trim(),
                    objCuadro.intCantRiesgo.ToString().Trim(),
                    objCuadro.strResponsable.ToString().Trim()
                    });
                }
            }
            lblCantRiesgo.Text = cant.ToString();
        }
        private static DataTable GetData(string query)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (System.Data.DataSet ds = new System.Data.DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        protected void GVcontroles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            string strErrMsg = "";
            mtdLoadDataControl(ref strErrMsg);
        }

        protected void GVcontroles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*if(Session["Jerarquia"] == null || Session["Jerarquia"].ToString() == "")
            {*/
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string IdControl = GVcontroles.DataKeys[e.Row.RowIndex].Value.ToString();
                    //int IdRiesgo = Convert.ToInt32(Session["IdEvento"].ToString());
                    //int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
                    GridView gvRiesgos = e.Row.FindControl("gvRiesgos") as GridView;
                    gvRiesgos.DataSource = GetData(string.Format("SELECT [IdControlesRiesgo], RCR.IdControl, RR.[IdRiesgo]" +
                      ", RR.Codigo, RR.Nombre, PCR.NombreClasificacionRiesgo " +
                    "FROM[Riesgos].[ControlesRiesgo] as RCR " +
                    "INNER JOIN Riesgos.Riesgo as RR on RR.IdRiesgo = RCR.IdRiesgo " +
                    "INNER JOIN Parametrizacion.ClasificacionRiesgo as PCR on PCR.IdClasificacionRiesgo = RR.IdClasificacionRiesgo " +
                    "where RCR.IdControl = {0}", IdControl));

                    gvRiesgos.DataBind();
                    //gvEventos.DataBind();
                }
           // }
            
        }

        protected void gvControlesJeraraquia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string IdControl = gvControlesJeraraquia.DataKeys[e.Row.RowIndex].Value.ToString();
                //int IdRiesgo = Convert.ToInt32(Session["IdEvento"].ToString());
                //int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
                GridView gvRiesgos = e.Row.FindControl("gvRiesgos") as GridView;
                gvRiesgos.DataSource = GetData(string.Format("SELECT [IdControlesRiesgo], RCR.IdControl, RR.[IdRiesgo]" +
                  ", RR.Codigo, RR.Nombre, PCR.NombreClasificacionRiesgo, RR.IdProbabilidad, rr.IdImpacto " +
                  ",NombreRiesgoInherente " +
                "FROM[Riesgos].[ControlesRiesgo] as RCR " +
                "INNER JOIN Riesgos.Riesgo as RR on RR.IdRiesgo = RCR.IdRiesgo " +
                "INNER JOIN Parametrizacion.ClasificacionRiesgo as PCR on PCR.IdClasificacionRiesgo = RR.IdClasificacionRiesgo " +
                "INNER JOIN Parametrizacion.Probabilidad ON RR.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad "+
                "INNER JOIN Parametrizacion.RiesgoInherente ON RR.IdProbabilidadResidual = Parametrizacion.RiesgoInherente.IdProbabilidad "+
                "INNER JOIN Parametrizacion.Impacto ON RR.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto "+
                "AND RR.IdImpactoResidual = Parametrizacion.RiesgoInherente.IdImpacto "+
                "where RCR.IdControl = {0}", IdControl));

                gvRiesgos.DataBind();
                //gvEventos.DataBind();
            }
        }
    }
}