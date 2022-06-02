using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using ListasSarlaft.Classes.Utilerias;
using ClosedXML.Excel;
using ListasSarlaft.Classes.DTO.Calidad;
using System.Text;
using ListasSarlaft.Classes.DTO;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class VerCaracterizacion : System.Web.UI.UserControl
    {
        string IdFormulario = "4021";
        cCuenta cCuenta = new cCuenta();
        clsVerCaracterizacionBLL caracterizacionBLL = new clsVerCaracterizacionBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            string strErrMsg = string.Empty;
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["IdProceso"] != null)
                {
                    if (cCuenta.permisosConsulta(IdFormulario) == "False")
                        Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
                    else
                    {
                        LidProcesoT.Text = Request.QueryString["IdProceso"].ToString();
                        mtdInicializarValores();
                        BodyFormGEC.Visible = false;
                        string strErrMsgidProceso = "";
                        mtdLoadGridHeaderCaracterizacion();
                        mtdLoadGridBodyCaracterizacion();
                        mtdLoadGridCaracterizacionIndicador();
                        mtdLoadGridCaracterizacionRiesgos();
                        if (!mtdLoadInfoGridCaracterizacion(ref strErrMsgidProceso))
                            omb.ShowMessage(strErrMsgidProceso, 2, "Atención");

                        GridVC.Visible = true;
                        if (!mtdLoadInfoGridDetalle(ref strErrMsgidProceso))
                            omb.ShowMessage(strErrMsgidProceso, 2, "Atención");
                        GridVCdetalle.Visible = true;
                        if (!mtdLoadInfoGridIndicador(ref strErrMsgidProceso))
                            omb.ShowMessage(strErrMsgidProceso, 2, "Atención");
                        DindicadorRiesgo.Visible = true;
                        if(!mtdLoadInfoGridRiesgos(ref strErrMsgidProceso))
                            omb.ShowMessage(strErrMsgidProceso, 2, "Atención");
                        Driesgos.Visible = true;
                    }
                }
                else
                {
                    if (cCuenta.permisosConsulta(IdFormulario) == "False")
                        Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
                    else
                    {
                        if (!Page.IsPostBack)
                        {
                            mtdInicializarValores();
                            mtdHabilitarCampos(1);
                            if (!mtdCargarDDLs(ref strErrMsg))
                                omb.ShowMessage(strErrMsg, 1, "Atención");
                        }
                    }
                }
            }
        }
        
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridDetalle;
        private int rowGridDetalle;
        private int pagIndexDetalle;
        private DataTable infoGridIndicador;
        private int rowGridIndicador;
        private int pagIndexIndicador;
        private DataTable infoGridRiesgos;
        private int rowGridRiesgos;
        private int pagIndexRiesgos;

        private DataTable infoGridEntradas;
        private int rowGridEntradas;
        private int pagIndexEntradas;

        private DataTable infoGridActividades;
        private int rowGridActividades;
        private int pagIndexActividades;

        private DataTable infoGridSalidas;
        private int rowGridSalidas;
        private int pagIndexSalidas;

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
        private DataTable InfoGridDetalle
        {
            get
            {
                infoGridDetalle = (DataTable)ViewState["infoGridDetalle"];
                return infoGridDetalle;
            }
            set
            {
                infoGridDetalle = value;
                ViewState["infoGridDetalle"] = infoGridDetalle;
            }
        }

        private int RowGridDetalle
        {
            get
            {
                rowGridDetalle = (int)ViewState["rowGridDetalle"];
                return rowGridDetalle;
            }
            set
            {
                rowGridDetalle = value;
                ViewState["rowGridDetalle"] = rowGridDetalle;
            }
        }

        private int PagIndexDetalle
        {
            get
            {
                pagIndexDetalle = (int)ViewState["pagIndexDetalle"];
                return pagIndexDetalle;
            }
            set
            {
                pagIndexDetalle = value;
                ViewState["pagIndexDetalle"] = pagIndexDetalle;
            }
        }
        private DataTable InfoGridIndicador
        {
            get
            {
                infoGridIndicador = (DataTable)ViewState["infoGridIndicador"];
                return infoGridIndicador;
            }
            set
            {
                infoGridIndicador = value;
                ViewState["infoGridIndicador"] = infoGridIndicador;
            }
        }

        private int RowGridIndicador
        {
            get
            {
                rowGridIndicador = (int)ViewState["rowGridIndicador"];
                return rowGridIndicador;
            }
            set
            {
                rowGridIndicador = value;
                ViewState["rowGridIndicador"] = rowGridIndicador;
            }
        }

        private int PagIndexIndicador
        {
            get
            {
                pagIndexIndicador = (int)ViewState["pagIndexIndicador"];
                return pagIndexIndicador;
            }
            set
            {
                pagIndexIndicador = value;
                ViewState["pagIndexIndicador"] = pagIndexIndicador;
            }
        }

        private DataTable InfoGridEntradas
        {
            get
            {
                infoGridEntradas = (DataTable)ViewState["infoGridEntradas"];
                return infoGridEntradas;
            }
            set
            {
                infoGridEntradas = value;
                ViewState["infoGridEntradas"] = infoGridEntradas;
            }
        }

        private int RowGridEntradas
        {
            get
            {
                rowGridEntradas = (int)ViewState["rowGridEntradas"];
                return rowGridEntradas;
            }
            set
            {
                rowGridEntradas = value;
                ViewState["rowGridEntradas"] = rowGridEntradas;
            }
        }

        private int PagIndexEntradas
        {
            get
            {
                pagIndexEntradas = (int)ViewState["pagIndexEntradas"];
                return pagIndexEntradas;
            }
            set
            {
                pagIndexEntradas = value;
                ViewState["pagIndexEntradas"] = pagIndexEntradas;
            }
        }
        private DataTable InfoGridActividades
        {
            get
            {
                infoGridActividades = (DataTable)ViewState["infoGridActividades"];
                return infoGridActividades;
            }
            set
            {
                infoGridActividades = value;
                ViewState["infoGridActividades"] = infoGridActividades;
            }
        }

        private int RowGridActividades
        {
            get
            {
                rowGridActividades = (int)ViewState["rowGridActividades"];
                return rowGridActividades;
            }
            set
            {
                rowGridActividades = value;
                ViewState["rowGridActividades"] = rowGridActividades;
            }
        }

        private int PagIndexActividades
        {
            get
            {
                pagIndexActividades = (int)ViewState["pagIndexActividades"];
                return pagIndexActividades;
            }
            set
            {
                pagIndexActividades = value;
                ViewState["pagIndexActividades"] = pagIndexActividades;
            }
        }
        private DataTable InfoGridSalidas
        {
            get
            {
                infoGridSalidas = (DataTable)ViewState["infoGridSalidas"];
                return infoGridSalidas;
            }
            set
            {
                infoGridSalidas = value;
                ViewState["infoGridSalidas"] = infoGridSalidas;
            }
        }

        private int RowGridSalidas
        {
            get
            {
                rowGridSalidas = (int)ViewState["rowGridSalidas"];
                return rowGridSalidas;
            }
            set
            {
                rowGridSalidas = value;
                ViewState["rowGridSalidas"] = rowGridSalidas;
            }
        }

        private int PagIndexSalidas
        {
            get
            {
                pagIndexSalidas = (int)ViewState["pagIndexSalidas"];
                return pagIndexSalidas;
            }
            set
            {
                pagIndexSalidas = value;
                ViewState["pagIndexSalidas"] = pagIndexSalidas;
            }
        }

        private DataTable InfoGridRiesgos
        {
            get
            {
                infoGridRiesgos = (DataTable)ViewState["infoGridRiesgos"];
                return infoGridRiesgos;
            }
            set
            {
                infoGridRiesgos = value;
                ViewState["infoGridRiesgos"] = infoGridRiesgos;
            }
        }

        private int RowGridRiesgos
        {
            get
            {
                rowGridRiesgos = (int)ViewState["rowGridRiesgos"];
                return rowGridRiesgos;
            }
            set
            {
                rowGridRiesgos = value;
                ViewState["rowGridRiesgos"] = rowGridRiesgos;
            }
        }

        private int PagIndexRiesgos
        {
            get
            {
                pagIndexRiesgos = (int)ViewState["pagIndexRiesgos"];
                return pagIndexRiesgos;
            }
            set
            {
                pagIndexRiesgos = value;
                ViewState["pagIndexRiesgos"] = pagIndexRiesgos;
            }
        }

        #endregion

        #region DLLeventos
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (!mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchCarac.Visible = true;
            btnImgCancelar.Visible = true;
        }

        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            PagIndexDetalle = 0;
            PagIndexIndicador = 0;
            
        }
        private void mtdHabilitarCampos(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1:
                    break;
            }
        }
        #endregion
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridHeaderCaracterizacion()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strnombreProceso", typeof(string));
            grid.Columns.Add("strObjetivo", typeof(string));


            GVheader.DataSource = grid;
            GVheader.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridCaracterizacion(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsVerCaracterizacion> lstVerCaracterizacion = new List<clsVerCaracterizacion>();
            clsVerCaracterizacionBLL cVerCaracterizacion = new clsVerCaracterizacionBLL();
            if (Request.QueryString["IdProceso"] != null)
            {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(LidProcesoT.Text),
                    string.Empty, string.Empty);
                clsVerCaracterizacion objVerCaracterizacionOut = new clsVerCaracterizacion();
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacion(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
            }
            else {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()),
                                    string.Empty, string.Empty);
                clsVerCaracterizacion objVerCaracterizacionOut = new clsVerCaracterizacion();
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacion(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
            }


            if (lstVerCaracterizacion != null)
            {
                mtdLoadInfoGridHeaderCaracterizacion(lstVerCaracterizacion);
                GVheader.DataSource = lstVerCaracterizacion;
                GVheader.PageIndex = pagIndex;
                GVheader.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "La información no esta completa";
                booResult = false;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadInfoGridHeaderCaracterizacion(List<clsVerCaracterizacion> lstVerCaracterizacion)
        {
            foreach (clsVerCaracterizacion objVerCaracterizacion in lstVerCaracterizacion)
            {
                string cadena = objVerCaracterizacion.strnombreProceso.ToString().Trim();
                string resultCadena = remplazarCaracteres(cadena);
                InfoGrid.Rows.Add(new Object[] {
                    objVerCaracterizacion.intId.ToString().Trim(),
                    objVerCaracterizacion.strnombreProceso.ToString().Trim(),
                    objVerCaracterizacion.strObjetivo.ToString().Trim()
                    });
            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridBodyCaracterizacion()
        {
            DataTable gridDetalle = new DataTable();

            gridDetalle.Columns.Add("strDescripcionEntrada", typeof(string));
            gridDetalle.Columns.Add("strProveedor", typeof(string));
            gridDetalle.Columns.Add("strDescripcionActividad", typeof(string));
            gridDetalle.Columns.Add("strCargoResponsable", typeof(string));
            gridDetalle.Columns.Add("strDescripcionSalida", typeof(string));
            gridDetalle.Columns.Add("strCliente", typeof(string));
            gridDetalle.Columns.Add("strDescripcionProcedimiento", typeof(string));

            /*GVVCdetalle.DataSource = gridDetalle;
            GVVCdetalle.DataBind();*/
            InfoGridDetalle = gridDetalle;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridBodyCaracterizacionEntradas()
        {
            DataTable gridDetalle = new DataTable();

            gridDetalle.Columns.Add("strDescripcionEntrada", typeof(string));
            gridDetalle.Columns.Add("strProveedor", typeof(string));
            /*gridDetalle.Columns.Add("strDescripcionActividad", typeof(string));
            gridDetalle.Columns.Add("strCargoResponsable", typeof(string));
            gridDetalle.Columns.Add("strDescripcionSalida", typeof(string));
            gridDetalle.Columns.Add("strCliente", typeof(string));
            gridDetalle.Columns.Add("strDescripcionProcedimiento", typeof(string));*/

            GVVCentradas.DataSource = gridDetalle;
            GVVCentradas.DataBind();
            InfoGridEntradas = gridDetalle;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridBodyCaracterizacionActividades()
        {
            DataTable gridDetalle = new DataTable();

            /*gridDetalle.Columns.Add("strDescripcionEntrada", typeof(string));
            gridDetalle.Columns.Add("strProveedor", typeof(string));*/
            gridDetalle.Columns.Add("strDescripcionActividad", typeof(string));
            gridDetalle.Columns.Add("strCargoResponsable", typeof(string));
            /*gridDetalle.Columns.Add("strDescripcionSalida", typeof(string));
            gridDetalle.Columns.Add("strCliente", typeof(string));*/
            gridDetalle.Columns.Add("strDescripcionProcedimiento", typeof(string));
            gridDetalle.Columns.Add("strDescripcionPHVA", typeof(string));

            GVVCactividades.DataSource = gridDetalle;
            GVVCactividades.DataBind();
            InfoGridActividades = gridDetalle;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridBodyCaracterizacionSalidas()
        {
            DataTable gridDetalle = new DataTable();

            /*gridDetalle.Columns.Add("strDescripcionEntrada", typeof(string));
            gridDetalle.Columns.Add("strProveedor", typeof(string));
            gridDetalle.Columns.Add("strDescripcionActividad", typeof(string));
            gridDetalle.Columns.Add("strCargoResponsable", typeof(string));*/
            gridDetalle.Columns.Add("strDescripcionSalida", typeof(string));
            gridDetalle.Columns.Add("strCliente", typeof(string));
            //gridDetalle.Columns.Add("strDescripcionProcedimiento", typeof(string));

            GVVCsalidas.DataSource = gridDetalle;
            GVVCsalidas.DataBind();
            InfoGridSalidas = gridDetalle;
        }
        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridDetalle(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsVerCaracterizacionEntradas> lstVerCaracterizacion = new List<clsVerCaracterizacionEntradas>();
            List<clsVerCaracterizacionActividades> lstVerCaracterizacionActividades = new List<clsVerCaracterizacionActividades>();
            List<clsVerCaracterizacionSalidas> lstVerCaracterizacionSalidas= new List<clsVerCaracterizacionSalidas>();
            clsVerCaracterizacionBLL cVerCaracterizacion = new clsVerCaracterizacionBLL();
            if (Request.QueryString["IdProceso"] != null)
            {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(LidProcesoT.Text),
                    string.Empty, string.Empty);
                //clsVerCaracterizacionDetalle objVerCaracterizacionOut = new clsVerCaracterizacionDetalle();
                clsVerCaracterizacionEntradas objVerCaracterizacionOut = new clsVerCaracterizacionEntradas();
                clsVerCaracterizacionActividades objVerCaracterizacionActOut = new clsVerCaracterizacionActividades();
                clsVerCaracterizacionSalidas objVerCaracterizacionSalOut = new clsVerCaracterizacionSalidas();
                //lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacionDetalle(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacionEntradas(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
                lstVerCaracterizacionActividades = cVerCaracterizacion.mtdConsultarVerCaracterizacionActividades(objVerCaracterizacionIn, ref objVerCaracterizacionActOut, ref strErrMsg);
                lstVerCaracterizacionSalidas = cVerCaracterizacion.mtdConsultarVerCaracterizacionSalidas(objVerCaracterizacionIn, ref objVerCaracterizacionSalOut, ref strErrMsg);
                Session["IdProceso"] = Convert.ToInt32(LidProcesoT.Text);
            }
            else {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()),
                                    string.Empty, string.Empty);
                //clsVerCaracterizacionDetalle objVerCaracterizacionOut = new clsVerCaracterizacionDetalle();
                clsVerCaracterizacionEntradas objVerCaracterizacionOut = new clsVerCaracterizacionEntradas();
                clsVerCaracterizacionActividades objVerCaracterizacionActOut = new clsVerCaracterizacionActividades();
                clsVerCaracterizacionSalidas objVerCaracterizacionSalOut = new clsVerCaracterizacionSalidas();
                //lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacionDetalle(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacionEntradas(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
                lstVerCaracterizacionActividades = cVerCaracterizacion.mtdConsultarVerCaracterizacionActividades(objVerCaracterizacionIn, ref objVerCaracterizacionActOut, ref strErrMsg);
                lstVerCaracterizacionSalidas = cVerCaracterizacion.mtdConsultarVerCaracterizacionSalidas(objVerCaracterizacionIn, ref objVerCaracterizacionSalOut, ref strErrMsg);
                Session["IdProceso"] = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim());
            }

            if (lstVerCaracterizacion != null)
            {
                //mtdLoadInfoGridBodyCaracterizacion(lstVerCaracterizacion);
                mtdLoadInfoGridBodyCaracterizacionEntradas(lstVerCaracterizacion);
                mtdLoadInfoGridBodyCaracterizacionActividades(lstVerCaracterizacionActividades);
                /*GVVCdetalle.DataSource = lstVerCaracterizacion;
                GVVCdetalle.PageIndex = PagIndexDetalle;
                GVVCdetalle.DataBind();*/
                GVVCentradas.DataSource = lstVerCaracterizacion;
                GVVCentradas.PageIndex = pagIndexEntradas;
                GVVCentradas.DataBind();

                GVVCactividades.DataSource = lstVerCaracterizacionActividades;
                GVVCactividades.PageIndex = pagIndexActividades;
                GVVCactividades.DataBind();

                GVVCsalidas.DataSource = lstVerCaracterizacionSalidas;
                GVVCsalidas.PageIndex = pagIndexSalidas;
                GVVCsalidas.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay información Caracterización detallada para cargar";
                booResult = false;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadInfoGridBodyCaracterizacion(List<clsVerCaracterizacionDetalle> lstVerCaracterizacion)
        {
            foreach (clsVerCaracterizacionDetalle objVerCaracterizacion in lstVerCaracterizacion)
            {
                string DescripcionActividad = objVerCaracterizacion.strDescripcionActividad.ToString().Trim();
                string resultDescripcionActividad = remplazarCaracteres(DescripcionActividad);
                string strDescripcionEntrada = objVerCaracterizacion.strDescripcionEntrada.ToString().Trim();
                string resultstrDescripcionEntrada = remplazarCaracteres(strDescripcionEntrada);
                string strCargoResponsable = objVerCaracterizacion.strCargoResponsable.ToString().Trim();
                string resultstrCargoResponsable = remplazarCaracteres(strCargoResponsable);
                string strDescripcionSalida = objVerCaracterizacion.strDescripcionSalida.ToString().Trim();
                string resultstrDescripcionSalida = remplazarCaracteres(strDescripcionSalida);
                string resultDescripcionProcedimiento = remplazarCaracteres(objVerCaracterizacion.strDescripcionProcedimiento.ToString().Trim());
                    InfoGridDetalle.Rows.Add(new Object[] {
                        resultstrDescripcionEntrada,
                        objVerCaracterizacion.strProveedor.ToString().Trim(),
                        resultDescripcionActividad,
                        resultstrCargoResponsable,
                        resultstrDescripcionSalida,
                        objVerCaracterizacion.strCliente.ToString().Trim(),
                        resultDescripcionProcedimiento
                        });
                
                
            }
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con las Entradas de la Caracterizacion</param>
        private void mtdLoadInfoGridBodyCaracterizacionEntradas(List<clsVerCaracterizacionEntradas> lstVerCaracterizacion)
        {
            foreach (clsVerCaracterizacionEntradas objVerCaracterizacion in lstVerCaracterizacion)
            {
                string strDescripcionEntrada = objVerCaracterizacion.strDescripcionEntrada.ToString().Trim();
                string resultstrDescripcionEntrada = remplazarCaracteres(strDescripcionEntrada);
                
                InfoGridDetalle.Rows.Add(new Object[] {
                        resultstrDescripcionEntrada,
                        objVerCaracterizacion.strProveedor.ToString().Trim()
                        });


            }
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con las Actividades de la Caracterizacion</param>
        private void mtdLoadInfoGridBodyCaracterizacionActividades(List<clsVerCaracterizacionActividades> lstVerCaracterizacion)
        {
            foreach (clsVerCaracterizacionActividades objVerCaracterizacion in lstVerCaracterizacion)
            {
                string DescripcionActividad = objVerCaracterizacion.strDescripcionActividad.ToString().Trim();
                string resultDescripcionActividad = remplazarCaracteres(DescripcionActividad);
                string strCargoResponsable = objVerCaracterizacion.strCargoResponsable.ToString().Trim();
                string resultDescripcionProcedimiento = remplazarCaracteres(objVerCaracterizacion.strDescripcionProcedimiento.ToString().Trim());
                string strPHVA = remplazarCaracteres(objVerCaracterizacion.strDescripcionPHVA.ToString().Trim());
                InfoGridDetalle.Rows.Add(new Object[] {
                        resultDescripcionActividad,
                        strCargoResponsable,
                        resultDescripcionProcedimiento,
                        strPHVA
                        });


            }
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con las Actividades de la Caracterizacion</param>
        private void mtdLoadInfoGridBodyCaracterizacionSalidas(List<clsVerCaracterizacionSalidas> lstVerCaracterizacion)
        {
            foreach (clsVerCaracterizacionSalidas objVerCaracterizacion in lstVerCaracterizacion)
            {
                string strDescripcionSalida = objVerCaracterizacion.strDescripcionSalida.ToString().Trim();
                string resultstrDescripcionSalida = remplazarCaracteres(strDescripcionSalida);

                InfoGridDetalle.Rows.Add(new Object[] {
                        resultstrDescripcionSalida,
                        objVerCaracterizacion.strCliente.ToString().Trim()
                        });


            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid del INDICADOR
        /// </summary>
        private void mtdLoadGridCaracterizacionIndicador()
        {
            DataTable gridIndicador = new DataTable();

            gridIndicador.Columns.Add("strNombreIndicador", typeof(string));
            //gridIndicador.Columns.Add("strDescripcionIndicador", typeof(string));
            //gridIndicador.Columns.Add("strCodigo", typeof(string));
            //gridIndicador.Columns.Add("strNombreRiesgo", typeof(string));
            //gridIndicador.Columns.Add("strDescripcionRiesgo", typeof(string));
            gridIndicador.Columns.Add("strCodigoControl", typeof(string));
            gridIndicador.Columns.Add("strNombreControl", typeof(string));

            GVindicadorRiesgo.DataSource = gridIndicador;
            GVindicadorRiesgo.DataBind();
            DataTable gridRiesgo = new DataTable();

            gridRiesgo.Columns.Add("strNombreIndicador", typeof(string));
            //gridRiesgo.Columns.Add("strDescripcionIndicador", typeof(string));
            //gridRiesgo.Columns.Add("strNombreRiesgo", typeof(string));
           // gridRiesgo.Columns.Add("strDescripcionRiesgo", typeof(string));
            /*gridRiesgo.Columns.Add("strCodigoControl", typeof(string));
            gridRiesgo.Columns.Add("strNombreControl", typeof(string));*/

            GVindicadorRiesgoPrint.DataSource = gridRiesgo;
            GVindicadorRiesgoPrint.DataBind();
            InfoGridIndicador = gridIndicador;
        }
        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridIndicador(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsVerCaracterizacionIndicadorRiesgo> lstVerCaracterizacion = new List<clsVerCaracterizacionIndicadorRiesgo>();
            clsVerCaracterizacionBLL cVerCaracterizacion = new clsVerCaracterizacionBLL();
            if (Request.QueryString["IdProceso"] != null)
            {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(LidProcesoT.Text),
                    string.Empty, string.Empty);
                clsVerCaracterizacionIndicadorRiesgo objVerCaracterizacionOut = new clsVerCaracterizacionIndicadorRiesgo();
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacionIndicadorle(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
            }
            else {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()),
                    string.Empty, string.Empty);
                clsVerCaracterizacionIndicadorRiesgo objVerCaracterizacionOut = new clsVerCaracterizacionIndicadorRiesgo();
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacionIndicadorle(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
            }
            if (lstVerCaracterizacion != null)
            {
                mtdLoadInfoGridCaracterizacionIndicador(lstVerCaracterizacion);
                GVindicadorRiesgo.DataSource = lstVerCaracterizacion;
                GVindicadorRiesgo.PageIndex = PagIndexIndicador;
                GVindicadorRiesgo.DataBind();
                GVindicadorRiesgoPrint.DataSource = lstVerCaracterizacion;
                GVindicadorRiesgoPrint.DataBind();
                booResult = true;

            }
            else
            {
                strErrMsg = "No hay información de Indicadores para cargar";
                booResult = false;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadInfoGridCaracterizacionIndicador(List<clsVerCaracterizacionIndicadorRiesgo> lstVerCaracterizacion)
        {
            foreach (clsVerCaracterizacionIndicadorRiesgo objVerCaracterizacion in lstVerCaracterizacion)
            {
                string resultstrNombreIndicador = remplazarCaracteres(objVerCaracterizacion.strNombreIndicador.ToString().Trim());
                //string resultstrDescripcionIndicador = remplazarCaracteres(objVerCaracterizacion.strDescripcionIndicador.ToString().Trim());
                //string resultstrNombreRiesgo = remplazarCaracteres(objVerCaracterizacion.strNombreRiesgo.ToString().Trim());
                //string resultstrDescripcionRiesgo = remplazarCaracteres(objVerCaracterizacion.strDescripcionRiesgo.ToString().Trim());
                //string resultstrNombreControl = remplazarCaracteres(objVerCaracterizacion.strNombreControl.ToString().Trim());
                InfoGridIndicador.Rows.Add(new Object[] {
                    resultstrNombreIndicador,
                    //resultstrDescripcionIndicador,
                   // remplazarCaracteres(objVerCaracterizacion.strCodigoRiesgo.ToString().Trim()),
                    //resultstrNombreRiesgo
                    //resultstrDescripcionRiesgo,
                    //remplazarCaracteres(objVerCaracterizacion.strCodigoControl.ToString().Trim()),
                    //resultstrNombreControl
                    });
            }
        }

        // <SUMMARY>
        /////////////////CARGA DE CAMPOS DEL GRID DE RIESGOS
        // <SUMMARY>
        private void mtdLoadGridCaracterizacionRiesgos()
        {
            DataTable gridRiesgos = new DataTable();

            ////gridRiesgos.Columns.Add("strNombreIndicador", typeof(string));
            //gridIndicador.Columns.Add("strDescripcionIndicador", typeof(string));
            gridRiesgos.Columns.Add("strCodigo", typeof(string));
            gridRiesgos.Columns.Add("strNombreRiesgo", typeof(string));
            //gridIndicador.Columns.Add("strDescripcionRiesgo", typeof(string));
            gridRiesgos.Columns.Add("strCodigoControl", typeof(string));
            gridRiesgos.Columns.Add("strNombreControl", typeof(string));

            GVRiesgos.DataSource = gridRiesgos;
            GVRiesgos.DataBind();
            DataTable gridRiesgo = new DataTable();

            ////gridRiesgo.Columns.Add("strNombreIndicador", typeof(string));
            //gridRiesgo.Columns.Add("strDescripcionIndicador", typeof(string));
            gridRiesgo.Columns.Add("strNombreRiesgo", typeof(string));
            // gridRiesgo.Columns.Add("strDescripcionRiesgo", typeof(string));
            /*gridRiesgo.Columns.Add("strCodigoControl", typeof(string));
            gridRiesgo.Columns.Add("strNombreControl", typeof(string));*/

            GVRiesgosPrint.DataSource = gridRiesgo;
            GVRiesgosPrint.DataBind();
            InfoGridRiesgos = gridRiesgos;
        }
        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridRiesgos(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsVerCaracterizacionRiesgos> lstVerCaracterizacion = new List<clsVerCaracterizacionRiesgos>();
            clsVerCaracterizacionBLL cVerCaracterizacion = new clsVerCaracterizacionBLL();
            if (Request.QueryString["IdProceso"] != null)
            {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(LidProcesoT.Text),
                    string.Empty, string.Empty);
                clsVerCaracterizacionRiesgos objVerCaracterizacionOut = new clsVerCaracterizacionRiesgos();
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacionRiesgosle(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
            }
            else
            {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()),
                    string.Empty, string.Empty);
                clsVerCaracterizacionRiesgos objVerCaracterizacionOut = new clsVerCaracterizacionRiesgos();
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacionRiesgosle(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
            }
            if (lstVerCaracterizacion != null)
            {
                mtdLoadInfoGridCaracterizacionRiesgos(lstVerCaracterizacion);
                GVRiesgos.DataSource = lstVerCaracterizacion;
                GVRiesgos.PageIndex = PagIndexIndicador;
                GVRiesgos.DataBind();
                GVRiesgosPrint.DataSource = lstVerCaracterizacion;
                GVRiesgosPrint.DataBind();
                booResult = true;

            }
            else
            {
                strErrMsg = "No hay información de Riesgos para cargar";
                booResult = false;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadInfoGridCaracterizacionRiesgos(List<clsVerCaracterizacionRiesgos> lstVerCaracterizacion)
        {
            foreach (clsVerCaracterizacionRiesgos objVerCaracterizacion in lstVerCaracterizacion)
            {
                ////string resultstrNombreIndicador = remplazarCaracteres(objVerCaracterizacion.strNombreIndicador.ToString().Trim());
                //string resultstrDescripcionIndicador = remplazarCaracteres(objVerCaracterizacion.strDescripcionIndicador.ToString().Trim());
                string resultstrNombreRiesgo = remplazarCaracteres(objVerCaracterizacion.strNombreRiesgo.ToString().Trim());
                //string resultstrDescripcionRiesgo = remplazarCaracteres(objVerCaracterizacion.strDescripcionRiesgo.ToString().Trim());
                //string resultstrNombreControl = remplazarCaracteres(objVerCaracterizacion.strNombreControl.ToString().Trim());
                InfoGridRiesgos.Rows.Add(new Object[] {
                    ///resultstrNombreIndicador,
                    //resultstrDescripcionIndicador,
                    remplazarCaracteres(objVerCaracterizacion.strCodigoRiesgo.ToString().Trim()),
                    resultstrNombreRiesgo
                    //resultstrDescripcionRiesgo,
                    //remplazarCaracteres(objVerCaracterizacion.strCodigoControl.ToString().Trim()),
                    //resultstrNombreControl
                    });
            }
        }
        //
        /// <summary>
        /// ///////////////FIN GRID DE RIESGOS
        /// </summary>
        //
        private void CargaGridCamposCaracterizacion()
        {
            try
            {
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion();
                objVerCaracterizacionIn.intIdProceso = LidProcesoT.Text.Equals("") ? Convert.ToInt32(ddlMacroproceso.SelectedValue) : Convert.ToInt32(LidProcesoT.Text);
                List<clsVerCaracterizacion> lstVerCaracterizacion = caracterizacionBLL.VerCamposCaracterizacion(objVerCaracterizacionIn);
                if (lstVerCaracterizacion != null)
                {
                    gvCamposCaracterizacion.DataSource = lstVerCaracterizacion;
                    gvCamposCaracterizacion.DataBind();
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar campos caracterización. {ex.Message}", 1, "Error");
            }
        }

        public class GridDecorator
        {
            public static void MergeRowsDetalle(GridView gridView)
            {
                for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = gridView.Rows[rowIndex];
                    GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        //string text = ((Label)row.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;

                        //string previousText = ((Label)previousRow.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;
                        if (cellIndex == 0)
                        {
                            string text = ((Label)row.FindControl("strDescripcionEntrada")).Text;
                            string previousText = ((Label)previousRow.FindControl("strDescripcionEntrada")).Text;
                            /*string text = row.Cells[cellIndex].Text;
                            string previousText = previousRow.Cells[cellIndex].Text;*/
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 1)
                        {
                            string text = ((Label)row.FindControl("strProveedor")).Text;
                            string previousText = ((Label)previousRow.FindControl("strProveedor")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 2)
                        {
                            string text = ((Label)row.FindControl("strDescripcionActividad")).Text;
                            string previousText = ((Label)previousRow.FindControl("strDescripcionActividad")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 3)
                        {
                            string text = ((Label)row.FindControl("strCargoResponsable")).Text;
                            string previousText = ((Label)previousRow.FindControl("strCargoResponsable")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 4)
                        {
                            string text = ((Label)row.FindControl("strDescripcionSalida")).Text;
                            string previousText = ((Label)previousRow.FindControl("strDescripcionSalida")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 5)
                        {
                            string text = ((Label)row.FindControl("strCliente")).Text;
                            string previousText = ((Label)previousRow.FindControl("strCliente")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 6)
                        {
                            string text = ((Label)row.FindControl("strDescripcionProcedimiento")).Text;
                            string previousText = ((Label)previousRow.FindControl("strDescripcionProcedimiento")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }

                    }
                }
            }
            public static void MergeRowsIndicador(GridView gridView)
            {
                for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                    {
                    GridViewRow row = gridView.Rows[rowIndex];
                    GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        //string text = ((Label)row.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;

                        //string previousText = ((Label)previousRow.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;
                        if (cellIndex == 0)
                        {
                            string text = ((Label)row.FindControl("NombreIndicador")).Text;
                            string previousText = ((Label)previousRow.FindControl("NombreIndicador")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        /*if (cellIndex == 1)
                        {
                            string text = ((Label)row.FindControl("DescripcionIndicador")).Text;
                            string previousText = ((Label)previousRow.FindControl("DescripcionIndicador")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }*/
                        if (cellIndex == 1)
                        {
                            string text = ((Label)row.FindControl("IdIndicador")).Text;
                            string previousText = ((Label)previousRow.FindControl("IdIndicador")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 2)
                        {
                            string text = ((Label)row.FindControl("IdIndicador")).Text;
                            string previousText = ((Label)previousRow.FindControl("IdIndicador")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 3)
                        {
                            string text = ((Label)row.FindControl("NombreRiesgo")).Text;
                            string previousText = ((Label)previousRow.FindControl("NombreRiesgo")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        /*if (cellIndex == 5)
                        {
                            string text = ((Label)row.FindControl("DescripcionRiesgo")).Text;
                            string previousText = ((Label)previousRow.FindControl("DescripcionRiesgo")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }

                        if (cellIndex == 6)
                        {
                            string text = ((Label)row.FindControl("IdRiesgo")).Text;
                            string previousText = ((Label)previousRow.FindControl("IdRiesgo")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }*/
                        if (cellIndex == 4)
                        {
                            string text = ((Label)row.FindControl("CodigoRiesgo")).Text;
                            string previousText = ((Label)previousRow.FindControl("CodigoRiesgo")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 5)
                        {
                            string text = ((Label)row.FindControl("CodigoRiesgo")).Text;
                            string previousText = ((Label)previousRow.FindControl("CodigoRiesgo")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 6)
                        {
                            string text = ((Label)row.FindControl("CodigoControl")).Text;
                            string previousText = ((Label)previousRow.FindControl("CodigoControl")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 7)
                        {
                            string text = ((Label)row.FindControl("NombreControl")).Text;
                            string previousText = ((Label)previousRow.FindControl("NombreControl")).Text;
                            
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                            /*if (text == "" && previousText != "")
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }*/
                        }
                        if (cellIndex == 8)
                        {
                            string text = ((Label)row.FindControl("NombreControl")).Text;
                            string previousText = ((Label)previousRow.FindControl("NombreControl")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                            /*if (text == "" && previousText != "")
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }*/
                        }
                    }
                }
            }

            public static void MergeRowsRiesgos(GridView gridView)
            {
                for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = gridView.Rows[rowIndex];
                    GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        if (cellIndex == 0)
                        {
                            string text = ((Label)row.FindControl("NombreRiesgo")).Text;
                            string previousText = ((Label)previousRow.FindControl("NombreRiesgo")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        //if (cellIndex == 5)
                        //{
                        //    string text = ((Label)row.FindControl("DescripcionRiesgo")).Text;
                        //    string previousText = ((Label)previousRow.FindControl("DescripcionRiesgo")).Text;
                        //    if (text == previousText)
                        //    {
                        //        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        //        previousRow.Cells[cellIndex].Visible = false;
                        //    }
                        //}

                        //if (cellIndex == 1)
                        //{
                        //    string text = ((Label)row.FindControl("IdRiesgo")).Text;
                        //    string previousText = ((Label)previousRow.FindControl("IdRiesgo")).Text;
                        //    if (text == previousText)
                        //    {
                        //        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        //        previousRow.Cells[cellIndex].Visible = false;
                        //    }
                        //}
                        if (cellIndex == 1)
                        {
                            string text = ((Label)row.FindControl("CodigoRiesgo")).Text;
                            string previousText = ((Label)previousRow.FindControl("CodigoRiesgo")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 2)
                        {
                            string text = ((Label)row.FindControl("CodigoRiesgo")).Text;
                            string previousText = ((Label)previousRow.FindControl("CodigoRiesgo")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 3)
                        {
                            string text = ((Label)row.FindControl("CodigoControl")).Text;
                            string previousText = ((Label)previousRow.FindControl("CodigoControl")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                        }
                        if (cellIndex == 4)
                        {
                            string text = ((Label)row.FindControl("NombreControl")).Text;
                            string previousText = ((Label)previousRow.FindControl("NombreControl")).Text;

                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                            /*if (text == "" && previousText != "")
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }*/
                        }
                        if (cellIndex == 5)
                        {
                            string text = ((Label)row.FindControl("NombreControl")).Text;
                            string previousText = ((Label)previousRow.FindControl("NombreControl")).Text;
                            if (text == previousText)
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }
                            /*if (text == "" && previousText != "")
                            {
                                row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                                previousRow.Cells[cellIndex].Visible = false;
                            }*/
                        }
                    }
                }
            }
        }
        #endregion
        
        #region Cargas
        private bool mtdCargarDDLs(ref string strErrMsg)
        {
            bool booResult = false;

            booResult = mtdLoadDDLCadenaValor(ref strErrMsg);
            //if (booResult)
            //    booResult = mtdLoadDDLMacroProceso(ref strErrMsg);

            return booResult;
        }

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
                        booResult = true;
                    }
                    else
                        booResult = false;
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

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
                        booResult = true;
                    }
                }
                else
                {
                    strErrMsg = "Error al cargar la información";
                    booResult = false;
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }
        public void mtdShowIndicador(int RowGrid)
        {
            string strErrMsg = string.Empty;
            //RowGrid = (Convert.ToInt16(GVindicadorRiesgo.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVindicadorRiesgo.Rows[RowGrid];
            string text = ((Label)row.FindControl("IdIndicador")).Text;
            if(text != "")
                Response.Redirect("~\\Formularios\\Proceso\\Admin\\PrcAdmIndicador.aspx?IdIndicador=" + text);
            else
                omb.ShowMessage("No existe un indicador asociado", 2, "Atención");
        }
        public void mtdShowRiesgo(int RowGrid)
        {
            string strErrMsg = string.Empty;
            //RowGrid = (Convert.ToInt16(GVindicadorRiesgo.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVRiesgos.Rows[RowGrid];
            string text = ((Label)row.FindControl("CodigoRiesgo")).Text;
            if(text != "")
                Response.Redirect("~\\Formularios\\Riesgos\\Admin\\AdminRiesgos.aspx?CodRiesgo=" + text);
            else
                omb.ShowMessage("El proceso no tiene un riesgo asociado", 2, "Atención");
        }
        public void mtdShowControl(int RowGrid)
        {
            string strErrMsg = string.Empty;
            //RowGrid = (Convert.ToInt16(GVindicadorRiesgo.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVindicadorRiesgo.Rows[RowGrid];
            string text = ((Label)row.FindControl("CodigoControl")).Text;
            if (text != "")
                Response.Redirect("~\\Formularios\\Riesgos\\Admin\\AdminControles.aspx?CodControl=" + text);
            else
                omb.ShowMessage("El proceso no tiene un riesgo asociado", 2, "Atención");
        }
        #endregion

        #region Button Eventos
        protected void btnSearchCarac_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = "";
            /*DataSets.DataSet dsReporte = new DataSets.DataSet();
            clsVerCaracterizacionBLL cVerCaracterizacion = new clsVerCaracterizacionBLL();
            clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()),
                                    string.Empty, string.Empty);
            clsVerCaracterizacion objVerCaracterizacionOut = new clsVerCaracterizacion();
            dsReporte = cVerCaracterizacion.mtdConsultarVerCaracterizacionRPT(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);
            //bool booResult = false;
            if (string.IsNullOrEmpty(strErrMsg))
            {
                ReportDataSource rdsSource = new ReportDataSource("DataSet2", dsReporte.Tables["DataSet2"]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rdsSource);
            }
            else
            {
                ReportViewer1.Visible = false;
                omb.ShowMessage(strErrMsg, 1, "Atención");
            }*/

            //ReportViewer1.LocalReport.Refresh();

            CargarGvDocumentos();
            CargaGridCamposCaracterizacion();
            mtdLoadGridHeaderCaracterizacion();
            mtdLoadGridBodyCaracterizacion();
            mtdLoadGridCaracterizacionIndicador();
            mtdLoadGridCaracterizacionRiesgos();
            //booResult = mtdLoadInfoGridIndicador(ref strErrMsg);
            if (!mtdLoadInfoGridCaracterizacion(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            
            GridVC.Visible = true;
            if(!mtdLoadInfoGridDetalle(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            GridVCdetalle.Visible = true;
            if (!mtdLoadInfoGridIndicador(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            DindicadorRiesgo.Visible = true;
            if(!mtdLoadInfoGridRiesgos(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            Driesgos.Visible = true;
            Dbutton.Visible = true;
            
        }
        #endregion

        #region GridEventos
        /*protected void GVVCdetalle_PreRender(object sender, EventArgs e)
        {

            GridDecorator.MergeRowsDetalle(GVVCdetalle);
        }*/
        protected void GVindicadorRiesgo_PreRender(object sender, EventArgs e)
        {
            GridDecorator.MergeRowsIndicador(GVindicadorRiesgo);
            //GridDecorator.MergeRowsDetalle(GVindicadorRiesgo);
        }
        #endregion

        protected void GVindicadorRiesgo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = /*(Convert.ToInt16(GVindicadorRiesgo.PageSize) * PagIndex)*/ + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SeleccionarIndicador":
                    mtdShowIndicador(RowGrid);
                    break;
                case "SeleccionarRiesgo":
                    mtdShowRiesgo(RowGrid);
                    break;
                case "SeleccionarControl":
                    mtdShowControl(RowGrid);
                    break;
            } 
        }

        protected void GVRiesgos_PreRender(object sender, EventArgs e)
        {
            GridDecorator.MergeRowsRiesgos(GVRiesgos);
        }

        protected void GVRiesgos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = /*(Convert.ToInt16(GVindicadorRiesgo.PageSize) * PagIndex)*/ +Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SeleccionarRiesgo":
                    mtdShowRiesgo(RowGrid);
                    break;
                case "SeleccionarControl":
                    mtdShowControl(RowGrid);
                    break;
            }

        }


        private String remplazarCaracteres(String cadena)
        {
            return cadena.Replace("'", "").Replace(",", "").Replace(".", "").Replace(";", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(":", "").Replace("" + (char)34, "").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("Ñ", "N").Replace("ñ", "n").Replace("" + (char)13, "").Replace("" + (char)10, "").Replace("´", "").Replace("|", String.Empty).ToUpper();
        }

        private List<DocumentosCaracterizacion> ConsultarDocumentos(int idMacroProceso) => caracterizacionBLL.ConsultarDocumentos(idMacroProceso);

        private void CargarGvDocumentos()
        {
            try
            {
                gvDocumentos.DataSource = ConsultarDocumentos(Convert.ToInt32(ddlMacroproceso.SelectedValue));
                gvDocumentos.DataBind();
                divDocumentos.Visible = true;
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los documentos. {ex.Message}", 2, "Atención");
            }
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {

            // Creamos el tipo de Font que vamos utilizar
            Font titleFont = new Font(Font.HELVETICA, 10, Font.NORMAL, Color.WHITE);
            Font textFont = new Font(Font.HELVETICA, 8, Font.NORMAL, Color.BLACK);
            List<PdfPRow> pRows = new List<PdfPRow>();

            #region HeaderCaracterizacion

            PdfPTable pdfpTable = new PdfPTable(GVheader.HeaderRow.Cells.Count);
            foreach (TableCell headerCell in GVheader.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, titleFont));
                pdfCell.BackgroundColor = new Color(GVheader.HeaderStyle.BackColor);
                pdfpTable.AddCell(pdfCell);
            }
            foreach (GridViewRow Row in GVheader.Rows)
            {
                string codigo = ((Label)Row.FindControl("lCodigoAplicativo")).Text;
                string intId = ((Label)Row.FindControl("intId")).Text;
                string strnombreProceso = ((Label)Row.FindControl("strnombreProceso")).Text;
                string strObjetivo = ((Label)Row.FindControl("strObjetivo")).Text;
                string responsable = ((Label)Row.FindControl("strNombreResponsable")).Text;

                List<PdfPCell> rowTable = new List<PdfPCell>
                {
                    new PdfPCell(new Phrase(Context.Server.HtmlDecode(codigo), textFont)),
                    new PdfPCell(new Phrase(Context.Server.HtmlDecode(intId), textFont)),
                    new PdfPCell(new Phrase(Context.Server.HtmlDecode(strnombreProceso), textFont)),
                    new PdfPCell(new Phrase(Context.Server.HtmlDecode(strObjetivo), textFont)),
                    new PdfPCell(new Phrase(Context.Server.HtmlDecode(responsable), textFont))
                };
                pRows.Add(new PdfPRow(rowTable.ToArray()));
            }
            pdfpTable.Rows.AddRange(pRows);

            #endregion

            #region DetalleCaracterizacion

            //PdfPTable pdfpTableDetalle = new PdfPTable(GVVCdetalle.HeaderRow.Cells.Count);
            PdfPTable pdfpTableEntradas = new PdfPTable(GVVCentradas.HeaderRow.Cells.Count);
            pRows.Clear();

            /*foreach (TableCell headerCell in GVVCdetalle.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text.Replace("&#243;", "ó"), titleFont));
                pdfCell.BackgroundColor = new Color(GVVCdetalle.HeaderStyle.BackColor);
                pdfpTableDetalle.AddCell(pdfCell);
            }
            foreach (GridViewRow Row in GVVCdetalle.Rows)
            {
                string strDescripcionEntrada = ((Label)Row.FindControl("strDescripcionEntrada")).Text;
                string strProveedor = ((Label)Row.FindControl("strProveedor")).Text;
                string strDescripcionActividad = ((Label)Row.FindControl("strDescripcionActividad")).Text;
                string strCargoResponsable = ((Label)Row.FindControl("strCargoResponsable")).Text;
                string strDescripcionSalida = ((Label)Row.FindControl("strDescripcionSalida")).Text;
                string strCliente = ((Label)Row.FindControl("strCliente")).Text;
                string strDescripcionProcedimiento = ((Label)Row.FindControl("strDescripcionProcedimiento")).Text;

                List<PdfPCell> rowTable = new List<PdfPCell>();
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionEntrada), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strProveedor), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionActividad), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCargoResponsable), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionSalida), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCliente), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionProcedimiento), textFont)));

                pRows.Add(new PdfPRow(rowTable.ToArray()));
            }
            pdfpTableDetalle.Rows.AddRange(pRows);*/
            foreach (TableCell headerCell in GVVCentradas.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text.Replace("&#243;", "ó"), titleFont));
                pdfCell.BackgroundColor = new Color(GVVCentradas.HeaderStyle.BackColor);
                pdfpTableEntradas.AddCell(pdfCell);
            }
            foreach (GridViewRow Row in GVVCentradas.Rows)
            {
                string strDescripcionEntrada = ((Label)Row.FindControl("strDescripcionEntrada")).Text;
                string strProveedor = ((Label)Row.FindControl("strProveedor")).Text;
                /*string strDescripcionActividad = ((Label)Row.FindControl("strDescripcionActividad")).Text;
                string strCargoResponsable = ((Label)Row.FindControl("strCargoResponsable")).Text;
                string strDescripcionSalida = ((Label)Row.FindControl("strDescripcionSalida")).Text;
                string strCliente = ((Label)Row.FindControl("strCliente")).Text;
                string strDescripcionProcedimiento = ((Label)Row.FindControl("strDescripcionProcedimiento")).Text;*/

                List<PdfPCell> rowTable = new List<PdfPCell>();
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionEntrada), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strProveedor), textFont)));
                /*rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionActividad), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCargoResponsable), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionSalida), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCliente), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionProcedimiento), textFont)));*/

                pRows.Add(new PdfPRow(rowTable.ToArray()));
            }
            pdfpTableEntradas.Rows.AddRange(pRows);
            pRows.Clear();
            PdfPTable pdfpTableActividades = new PdfPTable(GVVCactividades.HeaderRow.Cells.Count);
            foreach (TableCell headerCell in GVVCactividades.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text.Replace("&#243;", "ó"), titleFont));
                pdfCell.BackgroundColor = new Color(GVVCentradas.HeaderStyle.BackColor);
                pdfpTableActividades.AddCell(pdfCell);
            }
            foreach (GridViewRow Row in GVVCactividades.Rows)
            {
                /*string strDescripcionEntrada = ((Label)Row.FindControl("strDescripcionEntrada")).Text;
                string strProveedor = ((Label)Row.FindControl("strProveedor")).Text;*/
                string strDescripcionActividad = ((Label)Row.FindControl("strDescripcionActividad")).Text;
                string strDescripcionPHVA = ((Label)Row.FindControl("strDescripcionPHVA")).Text;
                string strCargoResponsable = ((Label)Row.FindControl("strCargoResponsable")).Text;
                string strDescripcionProcedimiento = ((Label)Row.FindControl("strDescripcionProcedimiento")).Text;
                
                /*string strCliente = ((Label)Row.FindControl("strCliente")).Text;
                 * string strDescripcionSalida = ((Label)Row.FindControl("strDescripcionSalida")).Text;
                */

                List<PdfPCell> rowTable = new List<PdfPCell>();
                /*rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionEntrada), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strProveedor), textFont)));*/
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionActividad), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionPHVA), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCargoResponsable), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionProcedimiento), textFont)));
                
                /*rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCliente), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionProcedimiento), textFont)));*/

                pRows.Add(new PdfPRow(rowTable.ToArray()));
            }
            pdfpTableActividades.Rows.AddRange(pRows);
            pRows.Clear();
            PdfPTable pdfpTableSalidas = new PdfPTable(GVVCsalidas.HeaderRow.Cells.Count);
            foreach (TableCell headerCell in GVVCsalidas.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text.Replace("&#243;", "ó"), titleFont));
                pdfCell.BackgroundColor = new Color(GVVCsalidas.HeaderStyle.BackColor);
                pdfpTableSalidas.AddCell(pdfCell);
            }
            foreach (GridViewRow Row in GVVCsalidas.Rows)
            {
                /*string strDescripcionEntrada = ((Label)Row.FindControl("strDescripcionEntrada")).Text;
                string strProveedor = ((Label)Row.FindControl("strProveedor")).Text;*/
                /*string strDescripcionActividad = ((Label)Row.FindControl("strDescripcionActividad")).Text;
                string strCargoResponsable = ((Label)Row.FindControl("strCargoResponsable")).Text;
                string strDescripcionProcedimiento = ((Label)Row.FindControl("strDescripcionProcedimiento")).Text;*/

                string strCliente = ((Label)Row.FindControl("strCliente")).Text;
                string strDescripcionSalida = ((Label)Row.FindControl("strDescripcionSalida")).Text;
                

                List<PdfPCell> rowTable = new List<PdfPCell>();
                /*rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionEntrada), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strProveedor), textFont)));*/
                /*rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionActividad), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCargoResponsable), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionProcedimiento), textFont)));*/
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionSalida), textFont)));
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCliente), textFont)));
                /*rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strDescripcionProcedimiento), textFont)));*/

                pRows.Add(new PdfPRow(rowTable.ToArray()));
            }
            pdfpTableSalidas.Rows.AddRange(pRows);
            #endregion

            #region Documentos

            // Crea la informacion de documentos en el pdf
            PdfPTable tableDocumentos = new PdfPTable(gvDocumentos.HeaderRow.Cells.Count);

            pRows.Clear();

            foreach (TableCell headerCell in gvDocumentos.HeaderRow.Cells)
             {
                Font font = new Font
                {
                    Color = new Color(gvDocumentos.HeaderStyle.ForeColor)
                };
                PdfPCell pdfCell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(headerCell.Text), titleFont));
                pdfCell.BackgroundColor = new Color(gvDocumentos.HeaderStyle.BackColor);
                tableDocumentos.AddCell(pdfCell);
            }
            foreach (GridViewRow Row in gvDocumentos.Rows)
            {
                List<PdfPCell> rowdocumentos = new List<PdfPCell>();
                for (int i = 0; i < gvDocumentos.HeaderRow.Cells.Count; i++)
                {
                    rowdocumentos.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(Row.Cells[i].Text), textFont)));
                }
                pRows.Add(new PdfPRow(rowdocumentos.ToArray()));
            }
            tableDocumentos.Rows.AddRange(pRows);

            #endregion

            #region RiesgoControlCaracterizacion

            #region ImprimirGrilla 

            pRows.Clear();

            Tools tools = new Tools();
            PdfPTable pdfpTableRiesgoControl = tools.createPdftable(GVindicadorRiesgoPrint);

            foreach (GridViewRow GridViewRow in GVindicadorRiesgoPrint.Rows)
            {
                string NombreIndicador = ((Label)GridViewRow.FindControl("strNombreIndicador")).Text;
                //string strNombreRiesgo = ((Label)GridViewRow.FindControl("strNombreRiesgo")).Text;
                //string strCodigoControl = ((Label)GridViewRow.FindControl("strCodigoControl")).Text;
                //string strNombreControl = ((Label)GridViewRow.FindControl("strNombreControl")).Text;
                List<PdfPCell> rowTable = new List<PdfPCell>
                {
                    new PdfPCell(new Phrase(Context.Server.HtmlDecode(NombreIndicador), textFont)),
                    //new PdfPCell(new Phrase(Context.Server.HtmlDecode(strNombreRiesgo), textFont)),
                    //new PdfPCell(new Phrase(Context.Server.HtmlDecode(strCodigoControl), textFont)),
                    //new PdfPCell(new Phrase(Context.Server.HtmlDecode(strNombreControl), textFont))
                };
                pRows.Add(new PdfPRow(rowTable.ToArray()));
            }
            pdfpTableRiesgoControl.Rows.AddRange(pRows);
            #endregion ImprimirGrilla

            PdfPTable pdfpTableRiesgosControl = tools.createPdftable(GVRiesgosPrint);

            foreach (GridViewRow GridViewRow1 in GVRiesgosPrint.Rows)
            { 
                string strNombreRiesgo = ((Label)GridViewRow1.FindControl("strNombreRiesgo")).Text;
                List<PdfPCell> rowTable = new List<PdfPCell>
                {
                    new PdfPCell(new Phrase(Context.Server.HtmlDecode(strNombreRiesgo), textFont)),
                };
                pRows.Add(new PdfPRow(rowTable.ToArray()));
            }
            pdfpTableRiesgosControl.Rows.AddRange(pRows);
            #endregion ImprimirGrilla

            Document pdfDocument = new Document(PageSize.LETTER, 1, 1, 10, 10);
            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de Caracterizacion");
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
            pdfcellImage.Border = Rectangle.NO_BORDER;
            pdfcellImage.Border = Rectangle.NO_BORDER;
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();
            phHeader.Add(pdftblImage);
            //phHeader.Add(chnCompany);
            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Caracterizacion"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfpTable);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableEntradas); 
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableActividades); 
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableSalidas);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(tableDocumentos);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableRiesgoControl);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableRiesgosControl);
            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteCaracterizacion.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        public static void exportExcelCSV(DataTable dt, HttpResponse Response, string filename, bool booHeader)
        {
            #region Generar TXT

            if (dt.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.csv;", filename));

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (booHeader)
                {
                    string[] columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
                    sb.AppendLine(string.Join("|", columnNames));
                }

                foreach (DataRow row in dt.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                    ToArray();
                    sb.AppendLine(string.Join("|", fields));
                }

                // the most easy way as you have type it
                Response.Write(sb.ToString());


                Response.Flush();
                Response.End();
            }

            #endregion
        }
        public DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();
            if (dtg.HeaderRow != null)
            {
                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
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
        protected void exportExcel(HttpResponse Response, string filename)
        {

            #region TablaEncabezado
            DataTable grid = new DataTable();
            grid.Columns.Add("Código");
            grid.Columns.Add("Nombre Proceso");
            grid.Columns.Add("Objetivo");
            

            DataRow row;
            foreach (GridViewRow GridViewRow in GVheader.Rows)
            {
                row = grid.NewRow();
                string intId = ((Label)GridViewRow.FindControl("intId")).Text;
                string strnombreProceso = ((Label)GridViewRow.FindControl("strnombreProceso")).Text;
                string strObjetivo = ((Label)GridViewRow.FindControl("strObjetivo")).Text;
                row["Código"] = intId;
                row["Nombre Proceso"] = strnombreProceso;
                row["Objetivo"] = strObjetivo;
                

                grid.Rows.Add(row);
            }
            #endregion TablaEncabezado
            #region TablaDetalle
            DataTable gridEntradas = new DataTable();
            gridEntradas.Columns.Add("Descripción Entrada");
            gridEntradas.Columns.Add("Proveedor");

            DataRow rowEntradas;
            foreach (GridViewRow GridViewRow in GVVCentradas.Rows)
            {
                rowEntradas = gridEntradas.NewRow();
                string strDescripcionEntrada = ((Label)GridViewRow.FindControl("strDescripcionEntrada")).Text;
                string strProveedor = ((Label)GridViewRow.FindControl("strProveedor")).Text;
                rowEntradas["Descripción Entrada"] = strDescripcionEntrada;
                rowEntradas["Proveedor"] = strProveedor;


                gridEntradas.Rows.Add(rowEntradas);
            }
            DataTable gridActividades = new DataTable();
            gridActividades.Columns.Add("Descripción Actividad");
            gridActividades.Columns.Add("Descripción PHVA");
            gridActividades.Columns.Add("Cargo Responsable");
            gridActividades.Columns.Add("Descripción Procedimiento");

            DataRow rowActividades;
            foreach (GridViewRow GridViewRow in GVVCactividades.Rows)
            {
                rowActividades = gridActividades.NewRow();
                string strDescripcionActividad = ((Label)GridViewRow.FindControl("strDescripcionActividad")).Text;
                string strDescripcionPHVA = ((Label)GridViewRow.FindControl("strDescripcionPHVA")).Text;
                string strCargoResponsable = ((Label)GridViewRow.FindControl("strCargoResponsable")).Text;
                string strDescripcionProcedimiento = ((Label)GridViewRow.FindControl("strDescripcionProcedimiento")).Text;
                
                rowActividades["Descripción Actividad"] = strDescripcionActividad;
                rowActividades["Descripción PHVA"] = strDescripcionPHVA;
                rowActividades["Cargo Responsable"] = strCargoResponsable;
                rowActividades["Descripción Procedimiento"] = strDescripcionProcedimiento;
              


                gridActividades.Rows.Add(rowActividades);
            }
            DataTable gridSalidas = new DataTable();
           
            gridSalidas.Columns.Add("Descripción Salida");
            gridSalidas.Columns.Add("Cliente");
            

            DataRow rowSalida;
            foreach (GridViewRow GridViewRow in GVVCsalidas.Rows)
            {
                rowSalida = gridSalidas.NewRow();
        
                string strDescripcionSalida = ((Label)GridViewRow.FindControl("strDescripcionSalida")).Text;
                string strCliente = ((Label)GridViewRow.FindControl("strCliente")).Text;
                
                rowSalida["Descripción Salida"] = strDescripcionSalida;
                rowSalida["Cliente"] = strCliente;



                gridSalidas.Rows.Add(rowSalida);
            }
            #endregion TableDetalle
            #region TablaIndicador
            DataTable gridIndicador = new DataTable();
            gridIndicador.Columns.Add("Nombre Indicador");

            DataRow rowIndicador;
            foreach (GridViewRow GridViewRow in GVindicadorRiesgoPrint.Rows)
            {
                rowIndicador = gridIndicador.NewRow();
                string strNombreIndicador = ((Label)GridViewRow.FindControl("strNombreIndicador")).Text;
                
                rowIndicador["Nombre Indicador"] = strNombreIndicador;

                gridIndicador.Rows.Add(rowIndicador);
            }
            #endregion TablaIndicador

            DataTable gridRiesgos = new DataTable();
            gridRiesgos.Columns.Add("Nombre Riesgo");

            DataRow rowRiesgos;
            foreach (GridViewRow GridViewRow in GVRiesgosPrint.Rows)
            {
                rowRiesgos = gridRiesgos.NewRow();
                string strNombreRiesgo = ((Label)GridViewRow.FindControl("strNombreRiesgo")).Text;

                rowRiesgos["Nombre Riesgo"] = strNombreRiesgo;



                gridRiesgos.Rows.Add(rowRiesgos);
            }


            DataTable gridDocumentos = new DataTable();
            foreach (TableCell headerCell in gvDocumentos.HeaderRow.Cells)
            {
                gridDocumentos.Columns.Add(Context.Server.HtmlDecode(headerCell.Text));
            }
            DataRow rowDetalle;
            foreach (TableRow rowGrid in gvDocumentos.Rows)
            {
                rowDetalle = gridDocumentos.NewRow();
                rowDetalle[0] = Context.Server.HtmlDecode(rowGrid.Cells[0].Text);
                rowDetalle[1] = Context.Server.HtmlDecode(rowGrid.Cells[1].Text);
                rowDetalle[2] = Context.Server.HtmlDecode(rowGrid.Cells[2].Text);
                rowDetalle[3] = Context.Server.HtmlDecode(rowGrid.Cells[3].Text);
                rowDetalle[4] = Context.Server.HtmlDecode(rowGrid.Cells[4].Text);
                rowDetalle[5] = Context.Server.HtmlDecode(rowGrid.Cells[5].Text);
                gridDocumentos.Rows.Add(rowDetalle);
            }

            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            //workbook.Worksheets.Add(gridEncabezado, "Indicador");
            workbook.Worksheets.Add(grid, "Caracterización Encabezado");
            workbook.Worksheets.Add(gridEntradas, "Caracterización Entradas");
            workbook.Worksheets.Add(gridActividades, "Caracterización Actividades");
            workbook.Worksheets.Add(gridSalidas, "Caracterización Salida");
            workbook.Worksheets.Add(gridIndicador, "Caracterización Indicadores");
            workbook.Worksheets.Add(gridRiesgos, "Caracterizaciín Riesgos");
            workbook.Worksheets.Add(gridDocumentos, "Caracterización Documentos");
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

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteCaracterizacions_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            ddlCadenaValor.SelectedIndex = 0;
            ddlMacroproceso.SelectedIndex = 0;
            Dbutton.Visible = false;
            GridVC.Visible = false;
            GridVCdetalle.Visible = false;
            divDocumentos.Visible = false;
            divCamposCaracterizacion.Visible = false;
            DindicadorRiesgo.Visible = false;
            Driesgos.Visible = false;
        }
    }
}