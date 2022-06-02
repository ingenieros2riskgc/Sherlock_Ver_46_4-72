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

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando.Indicadores.ViewDetalleIndicador
{
    public partial class ViewDetalleIndicador : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                pagIndex = 0;
                string severidad = string.Empty;
                string cadenavalor = string.Empty;
                string macroproceso = string.Empty;
                string proceso = string.Empty;
                string subproceso = string.Empty;
                string jerarquias = string.Empty;
                try
                {
                    
                    if (Request["severidad"] != null)
                        severidad = Request["severidad"].ToString();
                    if (Request["cadenavalor"] != null)
                        cadenavalor = Request["cadenavalor"].ToString();
                    if (Request["macroproceso"] != null)
                        macroproceso = Request["macroproceso"].ToString();
                    if (Request["proceso"] != null)
                        proceso = Request["proceso"].ToString();
                    if (Request["subproceso"] != null)
                        subproceso = Request["subproceso"].ToString();
                    if (Request["jerarquias"] != null)
                        jerarquias = Request["jerarquias"].ToString();
                    Session["severidad"] = severidad;
                    Session["cadenavalor"] = cadenavalor;
                    Session["macroproceso"] = macroproceso;
                    Session["proceso"] = proceso;
                    Session["subproceso"] = subproceso;
                    Session["jerarquias"] = jerarquias;
                    lblEfectividad.Text = severidad;
                }
                catch (Exception ex)
                {
                    omb.ShowMessage("Error en la captura de datos: " + ex, 2, "Atención");
                }
                string strErrMsg = string.Empty;
                    bool booResult = false;
                    booResult = mtdLoadDataIndicador(ref strErrMsg, cadenavalor, macroproceso, proceso, subproceso, jerarquias);
                    if (booResult == false)
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                
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
        public bool mtdLoadDataIndicador(ref string strErrMsg, string cadenavalor, string macroproceso, string proceso, string subproceso, string jerarquias)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorBajo = 0;
            int valorModerado = 0;
            int valorAlto = 0;
            int valorExtremo = 0;
            List<clsDTOCuadroMandoIndicadores> lstReporte = new List<clsDTOCuadroMandoIndicadores>();
            clsBLLCuadroMandoIndicadores cCuadroControles = new clsBLLCuadroMandoIndicadores();
            clsDTOCuadroMandoIndicadorFiltros objFiltros = new clsDTOCuadroMandoIndicadorFiltros();
            /**********************Filtros de Consulta****************************/
            objFiltros.intIdCadenaValor = Convert.ToInt32(cadenavalor);
            objFiltros.intIdMacroproceso = Convert.ToInt32(macroproceso);
            objFiltros.intIdProceso = Convert.ToInt32(proceso);
            objFiltros.intIdSubproceso = Convert.ToInt32(subproceso);
            /**********************Filtros  de Consulta****************************/
            try
            {
                if (cadenavalor == "0" && jerarquias == "")
                    booResult = cCuadroControles.LoadInfoReporteIndicadoresRiesgos(ref strErrMsg, ref lstReporte);
                else
                {
                    if (cadenavalor != "0")
                    {
                        booResult = cCuadroControles.LoadInfoReporteIndicadoresProceso(ref strErrMsg, ref lstReporte, objFiltros);
                    }
                    else
                    {
                        booResult = cCuadroControles.LoadInfoReporteIndicadorResponsable(ref strErrMsg, ref lstReporte, jerarquias);
                    }
                }
                if (booResult == true)
                {
                    if (cadenavalor == "0" && jerarquias == "")
                    {
                        mtdLoadGridIndicadores();
                        mtdLoadGridIndicadores(lstReporte);
                        GVindicadores.PageIndex = pagIndex;
                        GVindicadores.DataSource = InfoGrid;
                        GVindicadores.DataBind();
                        GVindicadores.Visible = true;
                        GVindicadoresResponsables.Visible = false;
                    }else
                    {
                        if (cadenavalor != "0")
                        {
                            mtdLoadGridIndicadores();
                            mtdLoadGridIndicadores(lstReporte);
                            GVindicadores.PageIndex = pagIndex;
                            GVindicadores.DataSource = InfoGrid;
                            GVindicadores.DataBind();
                            GVindicadores.Visible = true;
                            GVindicadoresResponsables.Visible = false;
                        }
                        else
                        {
                            mtdLoadGridIndicadoresResponsable();
                            mtdLoadGridIndicadoresResponsable(lstReporte);
                            GVindicadoresResponsables.PageIndex = pagIndex;
                            GVindicadoresResponsables.DataSource = InfoGrid;
                            GVindicadoresResponsables.DataBind();
                            GVindicadores.Visible = false;
                            GVindicadoresResponsables.Visible = true;
                        }
                    }
                        

                }
                
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la consulta: " + ex, 2, "Atención");
            }
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdRiesgoIndicador", typeof(string));
            grid.Columns.Add("strNombreIndicador", typeof(string));
            grid.Columns.Add("strObjetivoIndicador", typeof(string));
            grid.Columns.Add("intIdRiesgoAsociado", typeof(string));
            grid.Columns.Add("strColor", typeof(string));


            GVindicadores.DataSource = grid;
            GVindicadores.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridIndicadores(List<clsDTOCuadroMandoIndicadores> lstReporte)
        {
            string strErrMsg = String.Empty;
            int cant = 0;
            foreach (clsDTOCuadroMandoIndicadores objCuadro in lstReporte)
            {
                if (Session["severidad"] != null && Session["severidad"].ToString() != "")
                {
                    if (objCuadro.strNombreRiesgo == Session["severidad"].ToString())
                    {
                        cant++;
                        InfoGrid.Rows.Add(new Object[] {
                        //objCuadro.intIdRiesgo.ToString().Trim(),
                        objCuadro.intIdRiesgoIndicador.ToString().Trim(),
                        objCuadro.strNombreIndicador.ToString().Trim(),
                        objCuadro.strObjetivoIndicador.ToString().Trim(),
                        objCuadro.intIdRiesgoAsociado.ToString().Trim(),
                        objCuadro.strColor.ToString().Trim()
                    });
                    }
                }

            }
            lblCantRiesgo.Text = cant.ToString();
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridIndicadoresResponsable()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdRiesgoIndicador", typeof(string));
            grid.Columns.Add("strNombreIndicador", typeof(string));
            grid.Columns.Add("strObjetivoIndicador", typeof(string));
            grid.Columns.Add("strResponsable", typeof(string));
            grid.Columns.Add("intIdRiesgoAsociado", typeof(string));
            grid.Columns.Add("strColor", typeof(string));


            GVindicadoresResponsables.DataSource = grid;
            GVindicadoresResponsables.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridIndicadoresResponsable(List<clsDTOCuadroMandoIndicadores> lstReporte)
        {
            string strErrMsg = String.Empty;
            int cant = 0;
            foreach (clsDTOCuadroMandoIndicadores objCuadro in lstReporte)
            {
                if (Session["severidad"] != null && Session["severidad"].ToString() != "")
                {
                    if (objCuadro.strNombreRiesgo == Session["severidad"].ToString())
                    {
                        cant++;
                        InfoGrid.Rows.Add(new Object[] {
                        //objCuadro.intIdRiesgo.ToString().Trim(),
                        objCuadro.intIdRiesgoIndicador.ToString().Trim(),
                        objCuadro.strNombreIndicador.ToString().Trim(),
                        objCuadro.strObjetivoIndicador.ToString().Trim(),
                        objCuadro.strResponsable.ToString().Trim(),
                        objCuadro.intIdRiesgoAsociado.ToString().Trim(),
                        objCuadro.strColor.ToString().Trim()
                    });
                    }
                }

            }
            lblCantRiesgo.Text = cant.ToString();
        }
        protected void GVindicadores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string severidad = string.Empty;
            string cadenavalor = string.Empty;
            string macroproceso = string.Empty;
            string proceso = string.Empty;
            string subproceso = string.Empty;
            string jerarquias = string.Empty;
            string strCondicion = string.Empty;
            try
            {


                    severidad = Session["severidad"].ToString();
                    cadenavalor = Session["cadenavalor"].ToString();
                    macroproceso = Session["macroproceso"].ToString();
                    proceso = Session["proceso"].ToString();
                    subproceso = Session["subproceso"].ToString();
                    jerarquias = Session["jerarquias"].ToString();
                
                //lblEfectividad.Text = severidad;
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error en la captura de datos: " + ex, 2, "Atención");
            }
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if(cadenavalor != "" && cadenavalor != "0")
                    {
                        strCondicion = string.Format("and PCV.IdCadenaValor = {0}", cadenavalor);
                    }
                    if(macroproceso != "" && macroproceso != "0")
                    {
                        if (strCondicion != "")
                            strCondicion = strCondicion + string.Format(" and PMP.IdMacroProceso = {0}", macroproceso);
                        else
                            strCondicion = string.Format(" and PMP.IdMacroProceso = {0}", macroproceso);
                    }
                    if (proceso != "" && proceso != "0")
                    {
                        if (strCondicion != "")
                            strCondicion = strCondicion + string.Format(" and PP.IdProceso = {0}", proceso);
                        else
                            strCondicion = string.Format(" and PP.IdProceso = {0}", proceso);
                    }
                    if (subproceso != "" && subproceso != "0")
                    {
                        if (strCondicion != "")
                            strCondicion = strCondicion + string.Format(" and PSP.IdSubproceso = {0}", subproceso);
                        else
                            strCondicion = string.Format(" and PSP.IdSubproceso = {0}", subproceso);
                    }
                    //if (cadenavalor != "" && cadenavalor != "0")
                    //{
                    //    if(macroproceso == "0")
                    //    {
                    //        if (strCondicion != "")
                    //            strCondicion = strCondicion + string.Format(" and ISNULL(PMP.IdMacroProceso,0) = 0");
                    //        else
                    //            strCondicion = string.Format(" and ISNULL(PMP.IdMacroProceso,0) = 0");
                    //    }
                    //    if(proceso == "0")
                    //    {
                    //        if (strCondicion != "")
                    //            strCondicion = strCondicion + string.Format(" and ISNULL(PP.IdProceso,0) = 0");
                    //        else
                    //            strCondicion = string.Format(" and ISNULL(PP.IdProceso,0) = 0");
                    //    }
                    //    if(subproceso == "0")
                    //    {
                    //        if (strCondicion != "")
                    //            strCondicion = strCondicion + string.Format(" and ISNULL(PSP.IdSubproceso,0) = 0");
                    //        else
                    //            strCondicion = string.Format(" and ISNULL(PSP.IdSubproceso,0) = 0");
                    //    }
                    //}
                    //    if (jerarquias != "" && jerarquias != "0")
                    //{
                    //    if (strCondicion != "")
                    //        strCondicion = strCondicion + string.Format(" and RR.IdResponsableRiesgo = {0}", jerarquias);
                    //    else
                    //        strCondicion = string.Format(" and RR.IdResponsableRiesgo = {0}", jerarquias);
                    //}
                    string IdRiesgo = GVindicadores.DataKeys[e.Row.RowIndex].Value.ToString();
                    //int IdRiesgo = Convert.ToInt32(Session["IdEvento"].ToString());
                    //int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
                    GridView gvRiesgos = e.Row.FindControl("gvRiesgos") as GridView;
                    gvRiesgos.DataSource = GetData(string.Format("SELECT [Codigo],RR.[Nombre],RR.[Descripcion],[IdProbabilidad],[IdImpacto] " +
                        ",PCV.NombreCadenaValor as CadenaValor, PMP.Nombre as Macroproceso, PP.Nombre as Proceso, PSP.Nombre as Subproceso " +
                        "FROM [Riesgos].[Riesgo] as RR LEFT OUTER JOIN " +
                        "Procesos.CadenaValor AS PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT OUTER JOIN " +
                        "Procesos.Macroproceso AS PMP ON PMP.IdMacroProceso = RR.IdMacroproceso LEFT OUTER JOIN " +
                        "Procesos.Proceso AS PP ON PP.IdProceso = RR.IdProceso LEFT OUTER JOIN " +
                        "Procesos.Subproceso AS PSP ON PSP.IdSubproceso = RR.IdSubProceso " +
                        "WHERE IdRiesgo = {0} {1}", IdRiesgo, strCondicion));

                    gvRiesgos.DataBind();
                    //gvEventos.DataBind();
                }
            }catch(Exception ex)
            {
                omb.ShowMessage("Error conexion string 'SarlaftConnectionString': " + ex, 2, "Atención");
            }
            
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

        protected void GVindicadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            string strErrMsg = "";
            string cadenavalor = Session["cadenavalor"].ToString();
            string macroproceso = Session["macroproceso"].ToString();
            string proceso = Session["proceso"].ToString();
            string subproceso = Session["subproceso"].ToString();
            string jerarquias = Session["jerarquias"].ToString();
            mtdLoadDataIndicador(ref strErrMsg,cadenavalor,macroproceso,proceso,subproceso,jerarquias);
        }

        protected void GVindicadores_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < GVindicadores.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVindicadores.Rows[rowIndex];
                string strColor = string.Empty;
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 4)
                    {
                        strColor = ((Label)row.FindControl("strColor")).Text;
                        ((Label)row.FindControl("strColor")).Visible = false;
                        ImageButton ImbRango = ((ImageButton)row.FindControl("ImbRango"));
                        if (strColor == "Amarillo")
                            ImbRango.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        if (strColor == "Naranja")
                            ImbRango.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        if (strColor == "Rojo")
                            ImbRango.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        if (strColor == "")
                            ImbRango.Visible = false;
                    }
                }
            }
        }

        protected void GVindicadoresResponsables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            string strErrMsg = "";
            string cadenavalor = Session["cadenavalor"].ToString();
            string macroproceso = Session["macroproceso"].ToString();
            string proceso = Session["proceso"].ToString();
            string subproceso = Session["subproceso"].ToString();
            string jerarquias = Session["jerarquias"].ToString();
            mtdLoadDataIndicador(ref strErrMsg, cadenavalor, macroproceso, proceso, subproceso, jerarquias);
        }

        protected void GVindicadoresResponsables_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string IdRiesgo = GVindicadoresResponsables.DataKeys[e.Row.RowIndex].Value.ToString();
                    //int IdRiesgo = Convert.ToInt32(Session["IdEvento"].ToString());
                    //int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
                    GridView gvRiesgos = e.Row.FindControl("gvRiesgos") as GridView;
                    gvRiesgos.DataSource = GetData(string.Format("SELECT [Codigo],RR.[Nombre],RR.[Descripcion],[IdProbabilidad],[IdImpacto] " +
                        ",PCV.NombreCadenaValor as CadenaValor, PMP.Nombre as Macroproceso, PP.Nombre as Proceso, PSP.Nombre as Subproceso " +
                        "FROM [Riesgos].[Riesgo] as RR LEFT OUTER JOIN " +
                        "Procesos.CadenaValor AS PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT OUTER JOIN " +
                        "Procesos.Macroproceso AS PMP ON PMP.IdMacroProceso = RR.IdMacroproceso LEFT OUTER JOIN " +
                        "Procesos.Proceso AS PP ON PP.IdProceso = RR.IdProceso LEFT OUTER JOIN " +
                        "Procesos.Subproceso AS PSP ON PSP.IdSubproceso = RR.IdSubProceso " +
                        "WHERE IdRiesgo = {0}", IdRiesgo));

                    gvRiesgos.DataBind();
                    //gvEventos.DataBind();
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error conexion string 'SarlaftConnectionString': " + ex, 2, "Atención");
            }
        }

        protected void GVindicadoresResponsables_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < GVindicadoresResponsables.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVindicadoresResponsables.Rows[rowIndex];
                string strColor = string.Empty;
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 5)
                    {
                        strColor = ((Label)row.FindControl("strColor")).Text;
                        ((Label)row.FindControl("strColor")).Visible = false;
                        ImageButton ImbRango = ((ImageButton)row.FindControl("ImbRango"));
                        if (strColor == "Amarillo")
                            ImbRango.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        if (strColor == "Naranja")
                            ImbRango.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        if (strColor == "Rojo")
                            ImbRango.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        if (strColor == "")
                            ImbRango.Visible = false;
                    }
                }
            }
        }
    }
}