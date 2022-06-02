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

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando.Riesgos.ViewDetalleRiesgos
{
    public partial class DetalleRiesgos : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        string IdFormulario = "5032";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                try
                {
                    if(!this.IsPostBack)
                    {
                        string riesgo = string.Empty;
                        string riesgoGlobal = string.Empty;
                        if (Request["riesgo"] != null)
                            riesgo = Request["riesgo"].ToString();
                        if (Request["riesgoGlobal"] != null)
                            riesgoGlobal = Request["riesgoGlobal"].ToString();
                        lblRiesgo.Text = riesgo;
                        Session["riesgo"] = riesgo;
                        Session["riesgoGlobal"] = riesgoGlobal;
                        string strErrMsg = string.Empty;
                        bool booResult = false;
                        booResult = mtdLoadDataRiesgo(ref strErrMsg, riesgo);
                        if (booResult == false)
                            omb.ShowMessage(strErrMsg, 2, "Atención");
                    }
                    
                }catch(Exception ex)
                {
                    omb.ShowMessage("Error en la captura de la información: " + ex, 2, "Atención");
                }
                
            }
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
        public bool mtdLoadDataRiesgo(ref string strErrMsg, string riesgo)
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
            clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            clsDALCuadroMandoRiesgosDetalle dalDetalle = new clsDALCuadroMandoRiesgosDetalle();
            /**********************Filtros de Consulta****************************/
            
            if (Request["IdRiesgoGlobal"] != null && Request["IdRiesgoGlobal"].ToString() != "0")
            {
                objFiltros.intRiesgoGlobal = Convert.ToInt32(Request["IdRiesgoGlobal"].ToString());
            } else
            {
                objFiltros.intRiesgoGlobal = 0;
            }
            if (Session["riesgoGlobal"] != null)
            {
                /*System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                int idRiesgoGlobal = 0;
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    if (dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim() == Session["riesgoGlobal"].ToString())
                        idRiesgoGlobal = Convert.ToInt32(dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString());
                }
                objFiltros.intRiesgoGlobal = idRiesgoGlobal;*/
                objFiltros.strClasificacionGeneral = Session["riesgoGlobal"].ToString();
                objFiltros.intRiesgoGlobal = dalDetalle.mtdGetIdRiesgoGlobal(ref strErrMsg, Session["riesgoGlobal"].ToString());
            }
            else
            {
                objFiltros.strClasificacionGeneral = Request["clasificacionRiesgo"].ToString();
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
                objFiltros.strAreaRiesgo = Request["Area"].ToString();
            else
                objFiltros.strAreaRiesgo = "0";
            if (Request["IdArea"] != null)
                objFiltros.intArea = Convert.ToInt32(Request["IdArea"].ToString());
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
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(Request["FechaHistoricoFinal"].ToString());
            string TipoReporte = string.Empty;
            if (Request["TipoReporte"] != null)
                TipoReporte = Request["TipoReporte"].ToString();
            /**********************Filtros  de Consulta****************************/
            booResult = cCuadroRiesgos.mtdConsultarReporteRiesgos(ref strErrMsg, ref lstReporte, objFiltros, TipoReporte);
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
                mtdLoadGridRiesgos();
                mtdLoadGridRiesgos(lstReporte);
                GVriesgos.PageIndex = pagIndex;
                GVriesgos.DataSource = InfoGrid;
                GVriesgos.DataBind();
            }else
            {
                strErrMsg = "No hay datos para generar el reporte";
            }
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridRiesgos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("IdProbabilidadResidual", typeof(string));
            grid.Columns.Add("IdImpactoResidual", typeof(string));
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CantidadControles", typeof(string));
            grid.Columns.Add("CantidadEventos", typeof(string));
            grid.Columns.Add("CausasSinControl", typeof(string));
            
            GVriesgos.DataSource = grid;
            GVriesgos.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridRiesgos(List<clsDTOCuadroMandoRiesgosDetalle> lstReporte)
        {
            string strErrMsg = String.Empty;
            int cant = 0;
            foreach (clsDTOCuadroMandoRiesgosDetalle objCuadro in lstReporte)
            {
                if(Session["riesgo"] != null && Session["riesgo"].ToString() != "")
                {
                    if (objCuadro.strRiesgoInherente == Session["riesgo"].ToString())
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
                    }
                }else
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
                }
                
            }
            lblCantRiesgo.Text = cant.ToString();
        }
        protected void GVriesgos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            string strErrMsg = "";
            mtdLoadDataRiesgo(ref strErrMsg, Session["riesgo"].ToString());

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

        protected void GVriesgos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string IdRiesgo = GVriesgos.DataKeys[e.Row.RowIndex].Value.ToString();
                //int IdRiesgo = Convert.ToInt32(Session["IdEvento"].ToString());
                //int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
                GridView gvOrders = e.Row.FindControl("gvControles") as GridView;
                gvOrders.DataSource = GetData(string.Format("SELECT Riesgos.ControlesRiesgo.IdControlesRiesgo,Riesgos.Control.IdControl,Riesgos.Control.CodigoControl,Riesgos.Control.NombreControl, Riesgos.Control.DescripcionControl,Parametrizacion.Test.NombreTest,((((Parametrizacion.ClaseControl.ValorClaseControl) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE(IdPorcentajeCalificarControl = 1))) + ((Parametrizacion.TipoControl.ValorTipoControl) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 2))) +((SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 3))) +((SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 4))) +((SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 5)))) / 100) AS CalificacionControl,Parametrizacion.CalificacionControl.DesviacionProbabilidad,Parametrizacion.CalificacionControl.DesviacionImpacto,Riesgos.Control.IdMitiga, Riesgos.Control.IdCalificacionControl, Parametrizacion.CalificacionControl.Color, Parametrizacion.CalificacionControl.NombreEscala, Riesgos.Control.Responsable, Parametrizacion.JerarquiaOrganizacional.NombreHijo FROM Riesgos.Control Left JOIN Parametrizacion.ClaseControl ON Riesgos.Control.IdClaseControl = Parametrizacion.ClaseControl.IdClaseControl INNER JOIN Parametrizacion.TipoControl ON Riesgos.Control.IdTipoControl = Parametrizacion.TipoControl.IdTipoControl INNER JOIN Riesgos.ControlesRiesgo ON Riesgos.Control.IdControl = Riesgos.ControlesRiesgo.IdControl INNER JOIN Parametrizacion.Test ON Riesgos.Control.IdTest = Parametrizacion.Test.IdTest INNER JOIN Parametrizacion.CalificacionControl ON Riesgos.Control.IdCalificacionControl = Parametrizacion.CalificacionControl.IdCalificacionControl INNER JOIN Parametrizacion.JerarquiaOrganizacional on Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Control.Responsable WHERE(Riesgos.ControlesRiesgo.IdRiesgo = {0})", IdRiesgo));
                //gvOrders.DataSource = GetData(string.Format("SELECT Riesgos.ControlesRiesgo.IdControlesRiesgo, Riesgos.Control.NombreControl FROM Riesgos.Control INNER JOIN Parametrizacion.ClaseControl ON Riesgos.Control.IdClaseControl = Parametrizacion.ClaseControl.IdClaseControl INNER JOIN Parametrizacion.TipoControl ON Riesgos.Control.IdTipoControl = Parametrizacion.TipoControl.IdTipoControl INNER JOIN Parametrizacion.ResponsableExperiencia ON Riesgos.Control.IdResponsableExperiencia = Parametrizacion.ResponsableExperiencia.IdResponsableExperiencia INNER JOIN Parametrizacion.Documentacion ON Riesgos.Control.IdDocumentacion = Parametrizacion.Documentacion.IdDocumentacion INNER JOIN Parametrizacion.Responsabilidad ON Riesgos.Control.IdResponsabilidad = Parametrizacion.Responsabilidad.IdResponsabilidad INNER JOIN Riesgos.ControlesRiesgo ON Riesgos.Control.IdControl = Riesgos.ControlesRiesgo.IdControl INNER JOIN Parametrizacion.Test ON Riesgos.Control.IdTest = Parametrizacion.Test.IdTest INNER JOIN Parametrizacion.CalificacionControl ON Riesgos.Control.IdCalificacionControl = Parametrizacion.CalificacionControl.IdCalificacionControl WHERE(Riesgos.ControlesRiesgo.IdRiesgo = {0})", IdRiesgo));
                GridView gvEventos = e.Row.FindControl("gvEventos") as GridView;
                gvEventos.DataSource = GetData(string.Format("SELECT [CodigoEvento],[DescripcionEvento],[IdTipoPerdidaEvento],[NombreTipoPerdidaEvento],[IdCadenaValor],[CadenaValor],[IdMacroproceso],[Macroproceso],[IdProceso],[Proceso],[IdSubProceso],[Subproceso],[IdRiesgo] FROM[Riesgos].[vwCuadroMandoRiesgoEventosRiesgos] where IdRiesgo = {0}", IdRiesgo)); 
                gvOrders.DataBind();
                gvEventos.DataBind();
            }
        }
    }
}