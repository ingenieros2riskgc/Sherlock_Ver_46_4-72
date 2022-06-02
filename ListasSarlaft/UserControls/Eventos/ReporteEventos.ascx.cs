using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Eventos
{
    public partial class ReporteEventos : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "5015";
        cEvento cEvento = new cEvento();

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

        private DataTable infoGridReporteSinRepEventos;
        private DataTable InfoGridReporteSinRepEventos
        {
            get
            {
                infoGridReporteSinRepEventos = (DataTable)ViewState["infoGridReporteRiesgosPlanesAccion"];
                return infoGridReporteSinRepEventos;
            }
            set
            {
                infoGridReporteSinRepEventos = value;
                ViewState["infoGridReporteRiesgosPlanesAccion"] = infoGridReporteSinRepEventos;
            }
        }

        private int pagIndexInfoGridReporteSinRepEventos;
        private int PagIndexInfoGridReporteSinRepEventos
        {
            get
            {
                pagIndexInfoGridReporteSinRepEventos = (int)ViewState["pagIndexInfoGridReporteRiesgosPlanesAccion"];
                return pagIndexInfoGridReporteSinRepEventos;
            }
            set
            {
                pagIndexInfoGridReporteSinRepEventos = value;
                ViewState["pagIndexInfoGridReporteRiesgosPlanesAccion"] = pagIndexInfoGridReporteSinRepEventos;
            }
        }
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
                inicializarValores();
            }
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

        #region Eventos Vs. Planes Accion
        private void loadGridReporteRiesgosEventos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("Código", typeof(string));
            grid.Columns.Add("Empresa", typeof(string));
            grid.Columns.Add("Area", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NombreUsuarioRegistro", typeof(string));
            grid.Columns.Add("Fecha Registro", typeof(string));
            grid.Columns.Add("Región", typeof(string));
            grid.Columns.Add("Pais", typeof(string));
            grid.Columns.Add("Departamento", typeof(string));
            grid.Columns.Add("Ciudad", typeof(string));
            grid.Columns.Add("Oficina/Sucursal", typeof(string));
            grid.Columns.Add("Detalle Ubicación", typeof(string));
            grid.Columns.Add("Descripción Evento", typeof(string));
            grid.Columns.Add("Servicio/Producto", typeof(string));
            grid.Columns.Add("SubServicio/SubProducto", typeof(string));
            grid.Columns.Add("Fecha Inicio", typeof(string));
            grid.Columns.Add("Hora Inicio", typeof(string));
            grid.Columns.Add("Fecha Finalización", typeof(string));
            grid.Columns.Add("Hora Finalización", typeof(string));
            grid.Columns.Add("Fecha Descubrimiento", typeof(string));
            grid.Columns.Add("Hora Descubrimiento", typeof(string));
            grid.Columns.Add("Canal", typeof(string));
            grid.Columns.Add("Generador del Evento", typeof(string));
            grid.Columns.Add("Responsable Evento", typeof(string));
            grid.Columns.Add("Posible Cuantía de Pérdida", typeof(string));
            grid.Columns.Add("Cadena Valor", typeof(string));
            grid.Columns.Add("MacroProceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("SubProceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("Responsable Solución", typeof(string));
            grid.Columns.Add("Clase de Riesgo", typeof(string));
            grid.Columns.Add("SubClase de Riesgo", typeof(string));
            grid.Columns.Add("Tipo de Pérdida", typeof(string));
            grid.Columns.Add("Línea Operativa", typeof(string));
            grid.Columns.Add("SubLínea Operativa", typeof(string));
            grid.Columns.Add("Más Líneas Operativas", typeof(string));
            grid.Columns.Add("Afecta Continuidad", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            grid.Columns.Add("Observaciones", typeof(string));
            grid.Columns.Add("Responsable Contabilidad", typeof(string));
            grid.Columns.Add("Cuenta PUC", typeof(string));
            grid.Columns.Add("Cuenta de Orden", typeof(string));
            grid.Columns.Add("Tasa de Cambio", typeof(string));
            grid.Columns.Add("Valor en Pesos", typeof(string));
            grid.Columns.Add("Valor Recuperado Total", typeof(string));
            grid.Columns.Add("Tasa de Cambio 2", typeof(string));
            grid.Columns.Add("Valor en Pesos 2", typeof(string));
            grid.Columns.Add("Recuperación", typeof(string));
            grid.Columns.Add("Fuente de la Recuperación", typeof(string));
            grid.Columns.Add("Código Plan Acción", typeof(string));
            grid.Columns.Add("Plan Acción", typeof(string));
            grid.Columns.Add("Responsable Plan Acción", typeof(string));
            grid.Columns.Add("Tipo Recurso Plan Accion", typeof(string));
            grid.Columns.Add("Valor Recurso Plan Acción", typeof(string));
            grid.Columns.Add("Estado Plan Acción", typeof(string));
            grid.Columns.Add("Fecha Compromiso Plan Acción", typeof(string));

            InfoGridReporteRiesgosEventos = grid;
            GridView3.DataSource = InfoGridReporteRiesgosEventos;
            GridView3.DataBind();
        }

        private void loadInfoReporteRiesgosEventos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cEvento.ReporteEventos(DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(),
                "3", "---", Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()));

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteRiesgosEventos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["Código"].ToString().Trim(),
                        dtInfo.Rows[rows]["Empresa"].ToString().Trim(),
                        dtInfo.Rows[rows]["Area"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuarioRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Registro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Región"].ToString().Trim(),
                        dtInfo.Rows[rows]["Pais"].ToString().Trim(),
                        dtInfo.Rows[rows]["Departamento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ciudad"].ToString().Trim(),
                        dtInfo.Rows[rows]["Oficina/Sucursal"].ToString().Trim(),
                        dtInfo.Rows[rows]["Detalle Ubicación"].ToString().Trim(),
                        dtInfo.Rows[rows]["Descripción Evento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Servicio/Producto"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubServicio/SubProducto"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Inicio"].ToString().Trim(),
                        dtInfo.Rows[rows]["Hora Inicio"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Finalización"].ToString().Trim(),
                        dtInfo.Rows[rows]["Hora Finalización"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Descubrimiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Hora Descubrimiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Canal"].ToString().Trim(),
                        dtInfo.Rows[rows]["Generador del Evento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Responsable Evento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Posible Cuantía de Pérdida"].ToString().Trim(),
                        dtInfo.Rows[rows]["Cadena Valor"].ToString().Trim(),
                        dtInfo.Rows[rows]["MacroProceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubProceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["Responsable Solución"].ToString().Trim(),
                        dtInfo.Rows[rows]["Clase de Riesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubClase de Riesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Tipo de Pérdida"].ToString().Trim(),
                        dtInfo.Rows[rows]["Línea Operativa"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubLínea Operativa"].ToString().Trim(),
                        dtInfo.Rows[rows]["Más Líneas Operativas"].ToString().Trim(),
                        dtInfo.Rows[rows]["Afecta Continuidad"].ToString().Trim(),
                        dtInfo.Rows[rows]["Estado"].ToString().Trim(),
                        dtInfo.Rows[rows]["Observaciones"].ToString().Trim(),
                        dtInfo.Rows[rows]["Responsable Contabilidad"].ToString().Trim(),
                        dtInfo.Rows[rows]["Cuenta PUC"].ToString().Trim(),
                        dtInfo.Rows[rows]["Cuenta de Orden"].ToString().Trim(),
                        dtInfo.Rows[rows]["Tasa de Cambio"].ToString().Trim(),
                        dtInfo.Rows[rows]["Valor en Pesos"].ToString().Trim(),
                        dtInfo.Rows[rows]["Valor Recuperado Total"].ToString().Trim(),
                        dtInfo.Rows[rows]["Tasa de Cambio 2"].ToString().Trim(),
                        dtInfo.Rows[rows]["Valor en Pesos 2"].ToString().Trim(),
                        dtInfo.Rows[rows]["Recuperación"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fuente de la Recuperación"].ToString().Trim(),
                        dtInfo.Rows[rows]["Código Plan Acción"].ToString().Trim(),
                        dtInfo.Rows[rows]["Plan Acción"].ToString().Trim(),
                        dtInfo.Rows[rows]["Responsable Plan Acción"].ToString().Trim(),
                        dtInfo.Rows[rows]["Tipo Recurso Plan Accion"].ToString().Trim(),
                        dtInfo.Rows[rows]["Valor Recurso Plan Acción"].ToString().Trim(),
                        dtInfo.Rows[rows]["Estado Plan Acción"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Compromiso Plan Acción"].ToString().Trim()
                        });
                }

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
        #endregion Eventos Vs. Planes Accion

        #region No hubo evento
        private void loadGridReporteRiesgos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("CodigoEvento", typeof(string));
            grid.Columns.Add("Empresa", typeof(string));
            grid.Columns.Add("FechaNoHuboEvento", typeof(string));
            grid.Columns.Add("NombreResponsable", typeof(string));
            grid.Columns.Add("Cargo", typeof(string));
            grid.Columns.Add("Area", typeof(string));

            InfoGridReporteRiesgos = grid;
            GridView1.DataSource = InfoGridReporteRiesgos;
            GridView1.DataBind();
        }

        private void loadInfoReporteRiesgos()
        {
            DataTable dtInfo = new DataTable();

            dtInfo = cEvento.ReporteEventos(DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(),
                "1", "---", Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()));

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {

                    InfoGridReporteRiesgos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Empresa"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaNoHuboEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreResponsable"].ToString().Trim(),                                                                  
                        dtInfo.Rows[rows]["Cargo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Area"].ToString().Trim()
                        });
                }

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
        #endregion No hubo evento

        #region Eventos
        private void loadGridReporteRiesgosControles()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("Código", typeof(string));
            grid.Columns.Add("Empresa", typeof(string));
            grid.Columns.Add("Area", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NombreUsuarioRegistro", typeof(string));
            grid.Columns.Add("Fecha Registro", typeof(string));
            grid.Columns.Add("Región", typeof(string));
            grid.Columns.Add("Pais", typeof(string));
            grid.Columns.Add("Departamento", typeof(string));
            grid.Columns.Add("Ciudad", typeof(string));
            grid.Columns.Add("Oficina/Sucursal", typeof(string));
            grid.Columns.Add("Detalle Ubicación", typeof(string));
            grid.Columns.Add("Descripción Evento", typeof(string));
            grid.Columns.Add("Servicio/Producto", typeof(string));
            grid.Columns.Add("SubServicio/SubProducto", typeof(string));
            grid.Columns.Add("Fecha Inicio", typeof(string));
            grid.Columns.Add("Hora Inicio", typeof(string));
            grid.Columns.Add("Fecha Finalización", typeof(string));
            grid.Columns.Add("Hora Finalización", typeof(string));
            grid.Columns.Add("Fecha Descubrimiento", typeof(string));
            grid.Columns.Add("Hora Descubrimiento", typeof(string));
            grid.Columns.Add("Canal", typeof(string));
            grid.Columns.Add("Generador del Evento", typeof(string));
            grid.Columns.Add("Responsable Evento", typeof(string));
            grid.Columns.Add("Posible Cuantía de Pérdida ", typeof(string));
            grid.Columns.Add("Cadena Valor", typeof(string));
            grid.Columns.Add("MacroProceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("SubProceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("Responsable Solución", typeof(string));
            grid.Columns.Add("Clase de Riesgo", typeof(string));
            grid.Columns.Add("SubClase de Riesgo", typeof(string));
            grid.Columns.Add("Tipo de Pérdida", typeof(string));
            grid.Columns.Add("Línea Operativa ", typeof(string));
            grid.Columns.Add("SubLínea Operativa", typeof(string));
            grid.Columns.Add("Más Líneas Operativas", typeof(string));
            grid.Columns.Add("Afecta Continuidad", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            grid.Columns.Add("Observaciones", typeof(string));
            grid.Columns.Add("Responsable Contabilidad", typeof(string));
            grid.Columns.Add("Cuenta PUC", typeof(string));
            grid.Columns.Add("Cuenta de Orden", typeof(string));
            grid.Columns.Add("Tasa de Cambio", typeof(string));
            grid.Columns.Add("Valor en Pesos", typeof(string));
            grid.Columns.Add("Valor Recuperado Total", typeof(string));
            grid.Columns.Add("Tasa de Cambio 2", typeof(string));
            grid.Columns.Add("Valor en Pesos 2", typeof(string));
            grid.Columns.Add("Recuperación ", typeof(string));
            grid.Columns.Add("Fuente de la Recuperación", typeof(string));
            grid.Columns.Add("Fecha Contabilización", typeof(string));

            InfoGridReporteRiesgosControles = grid;
            GridView2.DataSource = InfoGridReporteRiesgosControles;
            GridView2.DataBind();
        }

        private void loadInfoReporteRiesgosControles()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cEvento.ReporteEventos(DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(),
                "2", "---", Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()));

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteRiesgosControles.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["Código"].ToString().Trim(),
                        dtInfo.Rows[rows]["Empresa"].ToString().Trim(),
                        dtInfo.Rows[rows]["Area"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuarioRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Registro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Región"].ToString().Trim(),
                        dtInfo.Rows[rows]["Pais"].ToString().Trim(),
                        dtInfo.Rows[rows]["Departamento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ciudad"].ToString().Trim(),
                        dtInfo.Rows[rows]["Oficina/Sucursal"].ToString().Trim(),
                        dtInfo.Rows[rows]["Detalle Ubicación"].ToString().Trim(),
                        dtInfo.Rows[rows]["Descripción Evento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Servicio/Producto"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubServicio/SubProducto"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Inicio"].ToString().Trim(),
                        dtInfo.Rows[rows]["Hora Inicio"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Finalización"].ToString().Trim(),
                        dtInfo.Rows[rows]["Hora Finalización"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Descubrimiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Hora Descubrimiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Canal"].ToString().Trim(),
                        dtInfo.Rows[rows]["Generador del Evento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Responsable Evento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Posible Cuantía de Pérdida"].ToString().Trim(),
                        dtInfo.Rows[rows]["Cadena Valor"].ToString().Trim(),
                        dtInfo.Rows[rows]["MacroProceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubProceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["Responsable Solución"].ToString().Trim(),
                        dtInfo.Rows[rows]["Clase de Riesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubClase de Riesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Tipo de Pérdida"].ToString().Trim(),
                        dtInfo.Rows[rows]["Línea Operativa"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubLínea Operativa"].ToString().Trim(),
                        dtInfo.Rows[rows]["Más Líneas Operativas"].ToString().Trim(),
                        dtInfo.Rows[rows]["Afecta Continuidad"].ToString().Trim(),
                        dtInfo.Rows[rows]["Estado"].ToString().Trim(),
                        dtInfo.Rows[rows]["Observaciones"].ToString().Trim(),
                        dtInfo.Rows[rows]["Responsable Contabilidad"].ToString().Trim(),
                        dtInfo.Rows[rows]["Cuenta PUC"].ToString().Trim(),
                        dtInfo.Rows[rows]["Cuenta de Orden"].ToString().Trim(),
                        dtInfo.Rows[rows]["Tasa de Cambio"].ToString().Trim(),
                        dtInfo.Rows[rows]["Valor en Pesos"].ToString().Trim(),
                        dtInfo.Rows[rows]["Valor Recuperado Total"].ToString().Trim(),
                        dtInfo.Rows[rows]["Tasa de Cambio 2"].ToString().Trim(),
                        dtInfo.Rows[rows]["Valor en Pesos 2"].ToString().Trim(),
                        dtInfo.Rows[rows]["Recuperación"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fuente de la Recuperación"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha Contabilización"].ToString().Trim()
                        });
                }

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
        #endregion Eventos

        #region Sin Reporte
        private void mtdLoadGridReporte_SinReporte()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Area", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFinal", typeof(string));

            InfoGridReporteSinRepEventos = grid;
            GridView4.DataSource = InfoGridReporteSinRepEventos;
            GridView4.DataBind();
        }

        private void mtdLoadInfoReporte_SinReporte()
        {
            DataTable dtInfo = new DataTable();
            string FechaInicio = string.Empty, FechaFinal = string.Empty;// "Todo";

            dtInfo = cEvento.ReporteEventos(DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(),
                "4", "---", Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()));

            if (dtInfo.Rows.Count > 0)
            {

                if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim())))
                    FechaInicio = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
                else
                    FechaInicio = "Todo";

                if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim())))
                    FechaFinal = Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim());
                else
                    FechaFinal = "Todo";

                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteSinRepEventos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["Area"].ToString().Trim(),
                        FechaInicio,
                        FechaFinal
                        });
                }
                GridView4.PageIndex = PagIndexInfoGridReporteSinRepEventos;
                GridView4.DataSource = InfoGridReporteSinRepEventos;
                GridView4.DataBind();
            }
            else
            {
                mtdLoadGridReporte_SinReporte();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Sin Reporte

        #endregion Loads

        #region DDLs
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
        #endregion DDLs

        #region Buttons
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                inicializarValores();
                switch (DropDownList1.SelectedValue.ToString().Trim())
                {
                    case "1":
                        #region No hubo Eventos
                        loadGridReporteRiesgos();
                        loadInfoReporteRiesgos();
                        resetValuesConsulta();
                        ReporteRiesgos.Visible = true;
                        #endregion
                        break;
                    case "2":
                        #region Eventos
                        loadGridReporteRiesgosControles();
                        loadInfoReporteRiesgosControles();
                        resetValuesConsulta();
                        ReporteRiesgosControles.Visible = true;
                        #endregion
                        break;
                    case "3":
                        #region Eventos VS Plan Accion
                        loadGridReporteRiesgosEventos();
                        loadInfoReporteRiesgosEventos();
                        resetValuesConsulta();
                        ReporteRiesgosEventos.Visible = true;
                        #endregion
                        break;
                    case "4":
                        #region Sin Reporte
                        mtdLoadGridReporte_SinReporte();
                        mtdLoadInfoReporte_SinReporte();
                        resetValuesConsulta();
                        ReporteRiesgosPlanesAccion.Visible = true;
                        #endregion
                        break;
                    case "5":
                        #region Consolidado
                        verreporteconsolidado();
                        resetValuesConsulta();
                        #endregion
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
            mtdLoadGridReporte_SinReporte();
            inicializarValores();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteRiesgos, Response, "Reporte No Hubo Eventos " + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteRiesgosControles, Response, "Reporte Eventos");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                exportExcel(InfoGridReporteRiesgosEventos, Response, "Reporte Eventos vrs Planes de Accion");
            }
            catch (Exception ex)
            {
                Mensaje("Error al exportar Reporte Eventos vrs Planes de Acción." + ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteSinRepEventos, Response, "Reporte Areas sin Reporte de Eventos");
        }
        #endregion Buttons

        #region GridViews
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
            PagIndexInfoGridReporteSinRepEventos = e.NewPageIndex;
            GridView4.PageIndex = PagIndexInfoGridReporteSinRepEventos;
            GridView4.DataSource = InfoGridReporteSinRepEventos;
            GridView4.DataBind();
        }
        #endregion GridViews

        private void inicializarValores()
        {
            PagIndexInfoGridReporteRiesgos = 0;
            PagIndexInfoGridReporteRiesgosControles = 0;
            PagIndexInfoGridReporteRiesgosEventos = 0;
            PagIndexInfoGridReporteSinRepEventos = 0;
        }

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

        private void verreporteconsolidado()
        {
            //Response.Redirect("~/Formularios/Eventos/ReporteConsolidado.aspx?Denegar=1");
            string str;
            str = "window.open('ReporteConsolidado.aspx?','Reporte','width=800,height=600,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {
            Response.Clear();
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
            Response.End();
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
            ReporteRiesgos.Visible = false;
            ReporteRiesgosControles.Visible = false;
            ReporteRiesgosEventos.Visible = false;
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

    }
}