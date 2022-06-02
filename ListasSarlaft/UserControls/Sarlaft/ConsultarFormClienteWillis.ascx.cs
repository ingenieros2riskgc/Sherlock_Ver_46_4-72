using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using System.IO;
using Microsoft.Security.Application;
using System.Configuration;

namespace ListasSarlaft.UserControls
{
    public partial class ConsultarFormClienteWillis : System.Web.UI.UserControl
    {
        string IdFormulario = "6008";
        string IdFormularioImprimir = "6100";
        string IdFormularioConsultar = "6101";
        string IdFormularioConsultarEstados = "6102";
        private cKnowClient cKnowClient = new cKnowClient();
        private cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        cCuenta cCuenta = new cCuenta();
        public string URL_FCC = System.Configuration.ConfigurationManager.AppSettings["URL_FCC"].ToString().Trim();
        public string URL_FCC_Print = System.Configuration.ConfigurationManager.AppSettings["URL_FCC"].ToString().Trim();
        
        #region Properties
        private int idConocimientoCliente;
        private int IdConocimientoCliente
        {
            get
            {
                idConocimientoCliente = (int)ViewState["idConocimientoCliente"];
                return idConocimientoCliente;
            }
            set
            {
                idConocimientoCliente = value;
                ViewState["idConocimientoCliente"] = idConocimientoCliente;
            }
        }

        private int idexRow;
        private int IdexRow
        {
            get
            {
                idexRow = (int)ViewState["idexRow"];
                return idexRow;
            }
            set
            {
                idexRow = value;
                ViewState["idexRow"] = idexRow;
            }
        }

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)Session["infGrid"];
                return infGrid;
            }
            set
            {
                infGrid = value;
                Session["infGrid"] = infGrid;
            }
        }

        private DataTable infGridFCC;
        private DataTable InfoGridFCC
        {
            get
            {
                infGridFCC = (DataTable)Session["infGridFCC"];
                return infGridFCC;
            }
            set
            {
                infGrid = value;
                Session["infGridFCC"] = infGridFCC;
            }
        }

        private DataTable infoGridArchivos;
        private DataTable InfoGridArchivos
        {
            get
            {
                infoGridArchivos = (DataTable)ViewState["infoGridArchivos"];
                return infoGridArchivos;
            }
            set
            {
                infoGridArchivos = value;
                ViewState["infoGridArchivos"] = infoGridArchivos;
            }
        }

        private int rowGridArchivos;
        private int RowGridArchivos
        {
            get
            {
                rowGridArchivos = (int)ViewState["rowGridArchivos"];
                return rowGridArchivos;
            }
            set
            {
                rowGridArchivos = value;
                ViewState["rowGridArchivos"] = rowGridArchivos;
            }
        }

        private int pagIndexInfoGridFormCliente;
        private int PagIndexInfoGridFormCliente
        {
            get
            {
                pagIndexInfoGridFormCliente = (int)ViewState["pagIndexInfoGridFormCliente"];
                return pagIndexInfoGridFormCliente;
            }
            set
            {
                pagIndexInfoGridFormCliente = value;
                ViewState["pagIndexInfoGridFormCliente"] = pagIndexInfoGridFormCliente;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            else
            {
                if (cCuenta.permisosConsulta(IdFormulario) == "False")
                    Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

                if (!Page.IsPostBack)
                {
                    tbFormulario.Visible = false;
                    inicializarValores();
                    initValues();
                    disableCampos();

                    if ((Request.QueryString["ValueTipoPersona"] != null) && (Request.QueryString["ValueInusualidad"] != null) && (Request.QueryString["Estado"] != null))
                        if ((Request.QueryString["ValueTipoPersona"] != string.Empty) &&
                            (Request.QueryString["ValueInusualidad"] != string.Empty) &&
                            (Request.QueryString["Estado"] != string.Empty))
                            buscarClientForm(Request.QueryString["ValueTipoPersona"].ToString().Trim(), "", "", "", "", "", "");
                }
            }
        }

        #region Buttons
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.",2);
                else
                {
                    updateClienteForm();

                    resetValues();
                    resetValuesConsulta();
                    disableCampos();
                    loadGrid();
                    Mensaje("Información actualizada con éxito.",1);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la información. " + ex.Message,3);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            imprimirFormulario();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            resetValues();
            disableCampos();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            try
            {
                if (mensaje.Trim() == string.Empty)
                {
                    inicializarValores();
                    buscarClientForm(DropDownList39.SelectedValue.ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(TextBox99.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox103.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox105.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(TextBox106.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox110.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox111.Text.Trim()));

                    DDLEstados.SelectedIndex = 0;
                    FechaDesde.Text = string.Empty;
                    FechaHasta.Text = string.Empty;
                }
                    
                else
                    Mensaje(mensaje,1);
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message,3);
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            resetValuesConsulta();
            loadGrid();
            resetValues();
            disableCampos();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Convertexcel(InfoGrid, Response, "Export Clientes");
        }

        public static void Convertexcel(DataTable dt, HttpResponse Response, string filename)
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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.",2);
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {
                            mtdCargarPdfFormCliente();
                            loadGridArchivos();
                            loadInfoArchivos();
                        }
                        else
                            Mensaje("Archivo sin guardar. Solo archivos en formato .pdf",2);
                    }
                    else
                        Mensaje("No hay archivos para cargar.",2);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al adjuntar el documento. " + ex.Message,3);
            }
        }
        #endregion

        #region GridViews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridFormCliente) + Convert.ToInt16(e.CommandArgument);
            IdConocimientoCliente = Convert.ToInt32(InfoGrid.Rows[IdexRow]["Id"]);
            string TipoPesona = InfoGrid.Rows[IdexRow]["IdTipoPersona"].ToString().Trim();
            switch (e.CommandName)
            {
                case "Imprimir":
                    if (cCuenta.permisosActualizar(IdFormularioImprimir) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.",2);
                    else
                    {
                        cargarInfoFCC_Validar(IdConocimientoCliente, TipoPesona);
                        cargarInfoFCC_Imprimir(IdConocimientoCliente, TipoPesona);
                    }
                    break;
                case "Consultar":
                    if (cCuenta.permisosActualizar(IdFormularioConsultarEstados) == "False")
                    {
                        if (InfoGrid.Rows[IdexRow]["IdEstado"].ToString().Trim() == "3")
                            cargarInfoFCC_Consultar(IdConocimientoCliente, TipoPesona);
                        else
                            Mensaje("El formulario seleccionado aun no ha sido aprobado. <br/> Intente más tarde. ",2);
                    }
                    else
                        cargarInfoFCC_Consultar(IdConocimientoCliente, TipoPesona);
                    break;
                case "Modificar":
                    if (cCuenta.permisosActualizar(IdFormularioConsultar) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.",2);
                    else
                    {
                        cargarInfoFCC_Validar(IdConocimientoCliente, TipoPesona);
                    }
                    break;
            }
        
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivos = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdfFormCliente();
                    break;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridFormCliente = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridFormCliente;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }
        #endregion

        protected void DropDownList39_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList39.SelectedValue.ToString() == "1")
            {
                TextBox110.Visible = true;
                TextBox111.Visible = true;
                Label195.Visible = true;
                Label196.Visible = true;
            }
            else
            {
                TextBox110.Visible = false;
                TextBox111.Visible = false;
                Label195.Visible = false;
                Label196.Visible = false;
            }
            DDLEstados.SelectedIndex = 0;
            FechaDesde.Text = string.Empty;
            FechaHasta.Text = string.Empty;
            
        }

        protected void ddlTipoFormulario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA NATURAL")
                mtdHabilitarTipoFormulario(1, true);

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA JURÍDICA")
                mtdHabilitarTipoFormulario(2, true);

            if (ddlTipoFormulario.SelectedValue.ToString() == "---")
                mtdHabilitarTipoFormulario(0, false);
        }

        private void inicializarValores()
        {
            PagIndexInfoGridFormCliente = 0;
        }

        private void initValues()
        {
            IdConocimientoCliente = 0;
            IdexRow = 0;
        }

        private void resetValues()
        {
            #region Informacion Cliente
            tbxFechaFormulario.Text = "";
            ddlClaseVinculacion.SelectedIndex = 0;
            tbxOtraClaseVinculacion.Text = "";
            ddlTomadorAsegurado.SelectedIndex = 0;
            tbxOtraTomadorAsegurado.Text = "";
            ddlTomadorBeneficiario.SelectedIndex = 0;
            tbxOtraTomadorBeneficiario.Text = "";
            ddlAseguradoBeneficiario.SelectedIndex = 0;
            tbxOtraAseguradoBeneficiario.Text = "";
            ddlTipoCliente.SelectedIndex = 0;
            tbxCiudad.Text = "";
            ddlSucursal.SelectedIndex = 0;
            ddlTipoSolicitud.SelectedIndex = 0;
            #endregion

            #region Informacion PN
            tbxPNPrimerApellido.Text = "";
            tbxPNSegunApellido.Text = "";
            tbxPNNombres.Text = "";
            ddlPNTipoDocumento.SelectedIndex = 0;
            tbxPNNumeroDocumento.Text = "";
            tbxPNFechaExpedicion.Text = "";
            tbxPNLugar.Text = "";
            tbxPNFechaNacimiento.Text = "";
            tbxPNNacionalidad.Text = "";
            tbxPNOcupacionOficio.Text = "";
            tbxPNCIIU.Text = "";
            DDLcodCIIU.ClearSelection();
            tbxPNEmpresaTrabajo.Text = "";
            tbxPNCargo.Text = "";
            tbxPNDireccion.Text = "";
            tbxPNTelefono1.Text = "";
            tbxPNDireccionResidencia.Text = "";
            tbxPNTelefono2.Text = "";
            tbxPNCelular.Text = "";
            tbxPNEspecificacionPreguntas.Text = "";
            tbxPNIngresosMensuales.Text = "";
            tbxPNActivos.Text = "";
            tbxPNEgresoMensuales.Text = "";
            tbxPNPasivos.Text = "";
            tbxPNOtrosIngresos.Text = "";
            tbxPNConceptoOtrosIngresos.Text = "";
            tbxPNCorreoElectronico.Text = "";
            tbxPNLugarNmto.Text = "";
            tbxPNOtraActEconomica.Text = "";
            tbxPNPatrimonio.Text = "";
            tbxPNDireccionEmpresa.Text = "";
            tbxPNTelefonoEmpresa.Text = "";
            tbxPNEspecificacionPreguntas2.Text = "";
            tbxPNServicio.Text = "";

            TrVinculoPPExpuestaPN.Visible = false;
            TXprimerApellidoPNPPE.Text = "";
            TXsegundApellidoPNPPE.Text = "";
            TXnombrePNPPE.Text = "";
            TXocupacionPNPPE.Text = "";
            TXcargoPNPPE.Text = "";
            #endregion

            #region Informacion PJ
            tbxPJRazonDenominacion.Text = "";
            tbxPJNIT.Text = "";
            tbxPJPrimerApellido.Text = "";
            tbxPJSegundoApellido.Text = "";
            tbxPJNombres.Text = "";
            tbxPJNumeroDocumento.Text = "";
            tbxPJLugarExpedicion.Text = "";
            tbxPJFechaExpedicion.Text = "";
            tbxPJDireccionOficina.Text = "";
            tbxPJTelefono1.Text = "";
            tbxPJDireccionSucursal.Text = "";
            tbxPJTelefono2.Text = "";
            tbxPJCIIU.Text = "";
            tbxPJIngresosMensuales.Text = "";
            tbxPJActivos.Text = "";
            tbxPJEgresoMensuales.Text = "";
            tbxPJPasivos.Text = "";
            tbxPJOtrosIngresos.Text = "";
            tbxPJConceptoOtrosIngresos.Text = "";
            tbxPJCorreoPrincipal.Text = "";
            tbxPJFechaConstitucion.Text = "";
            tbxPJNombresRepLegalPpal.Text = "";
            tbxPJDocumentoRepLegalPpal.Text = "";
            tbxPJNombresRepLegal1.Text = "";
            tbxPJDocumentoRepLegal1.Text = "";
            tbxPJNombresRepLegal2.Text = "";
            tbxPJDocumentoRepLegal2.Text = "";
            tbxPJNombresRepLegal3.Text = "";
            tbxPJDocumentoRepLegal3.Text = "";
            tbxPJNombresRepLegal4.Text = "";
            tbxPJDocumentoRepLegal4.Text = "";
            tbxPJNacionalidad1.Text = "";
            tbxPJDV.Text = "";
            tbxPJEspecificacionPreguntas.Text = "";
            tbxPJNIT1.Text = "";
            tbxPJPais1.Text = "";
            tbxPJNIT2.Text = "";
            tbxPJPais2.Text = "";
            tbxPJNIT3.Text = "";
            tbxPJPais3.Text = "";
            tbxPJNIT4.Text = "";
            tbxPJPais4.Text = "";
            tbxPJDireccionFiscal1.Text = "";
            tbxPJDireccionFiscal2.Text = "";
            tbxPJParticipacion1.Text = "";
            tbxPJParticipacion2.Text = "";
            tbxPJParticipacion3.Text = "";
            tbxPJParticipacion4.Text = "";
            tbxPJParticipacion5.Text = "";
            tbxPJSocMatriz.Text = "";
            tbxPJSocMatrizIdenTrib.Text = "";
            tbxPJSocMatrizJurisdiccion.Text = "";
            tbxPJSocMatrizDireccion.Text = "";
            tbxPJSocMatrizCiudad.Text = "";
            tbxPJSocMatrizTelefono.Text = "";
            tbxPJLugarNmto.Text = "";

            TrVinculoPPExpuestaPJ.Visible = false;
            ddlTipoDocumentoPJempresa.ClearSelection();
            TXprimerApellidoPPE.Text = "";
            TXsegundoApellidoPPE.Text = "";
            TXnombresPPE.Text = "";
            TXocipacionPPE.Text = "";
            TXcargoPPE.Text = "";
            #endregion

            #region Informacion Financiera
            ddlTransacMonedaExtra.SelectedIndex = 0;
            ddlTipoTransaccion.SelectedIndex = 0;
            tbxOtroTipoTransaccion.Text = "";
            tbxPFTipoProducto1.Text = "";
            tbxPFNumeroProducto1.Text = "";
            tbxPFEntidad1.Text = "";
            tbxPFMonto1.Text = "";
            tbxPFCiudad1.Text = "";
            tbxPFPais1.Text = "";
            tbxPFMoneda1.Text = "";
            tbxPFTipoProducto2.Text = "";
            tbxPFNumeroProducto2.Text = "";
            tbxPFEntidad2.Text = "";
            tbxPFMonto2.Text = "";
            tbxPFCiudad2.Text = "";
            tbxPFPais2.Text = "";
            tbxPFMoneda2.Text = "";
            ddlPFCtaMonedaExtra.SelectedIndex = 0;
            ddlPFProdExterior.SelectedIndex = 0;
            tbxPFTipoProducto3.Text = "";
            tbxPFNumeroProducto3.Text = "";
            tbxPFEntidad3.Text = "";
            tbxPFMonto3.Text = "";
            tbxPFCiudad3.Text = "";
            tbxPFPais3.Text = "";
            tbxPFMoneda3.Text = "";
            tbxPJSocMatriz.Text = "";
            tbxPJSocMatrizIdenTrib.Text = "";
            tbxPJSocMatrizJurisdiccion.Text = "";
            tbxPJSocMatrizDireccion.Text = "";
            tbxPJSocMatrizCiudad.Text = "";
            tbxPJSocMatrizTelefono.Text = "";
            #endregion

            #region Seguros
            tbxSeguroAno1.Text = "";
            tbxSeguroRamo1.Text = "";
            tbxSeguroCompania1.Text = "";
            tbxSeguroValor1.Text = "";
            ddlSeguroTipo1.SelectedIndex = 0;
            tbxSeguroAno2.Text = "";
            tbxSeguroRamo2.Text = "";
            tbxSeguroCompania2.Text = "";
            tbxSeguroValor2.Text = "";
            ddlSeguroTipo2.SelectedIndex = 0;
            tbxOrigenFondos.Text = "";
            ddlReclamaciones.SelectedIndex = 0;
            #endregion

            #region Entrevista
            tbxLugarEntrevista.Text = "";
            tbxFechaEntrevista.Text = "";
            tbxHoraEntrevista.Text = "";
            ddlResultado.SelectedIndex = 0;
            tbxObservaciones1.Text = "";
            tbxNombreResponsable.Text = "";
            tbxFechaVerificacion.Text = "";
            tbxHoraVerificacion.Text = "";
            tbxNombreVerifica.Text = "";
            tbxObservaciones2.Text = "";
            tbxNombreIntermediario.Text = "";
            #endregion

            mtdHabilitaVinculacion(false);
            mtdHabilitaVinculos(0, false);
            mtdHabilitaVinculos(1, false);
            mtdHabilitaVinculos(2, false);
            mtdHabilitaMoneda(false);
            mtdHabilitaReclamos(false);
            mtdHabilitaPregunta4(false);
            mtdHabilitaPregunta5(false);

            FileUpload1.Dispose();

            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            //Button7.Visible = false;
            tbConsulta.Visible = true;
            tbFormulario.Visible = false;
        }

        private void resetValuesConsulta()
        {
            DropDownList39.SelectedIndex = 0;

            #region PN
            ddlPNDpto.ClearSelection();
            ddlPNDpto2.ClearSelection();
            ddlPNCiudad1.ClearSelection();
            ddlPNCiudad2.ClearSelection();
            ddlPNDptoEmpresa.ClearSelection();
            ddlPNCiudadEmpresa.ClearSelection();
            ddlPNCIIUDescripcion.ClearSelection();

            ddlPNPregunta1.SelectedIndex = 0;
            ddlPNPregunta2.SelectedIndex = 0;
            ddlPNPregunta3.SelectedIndex = 0;
            ddlPNPregunta4.SelectedIndex = 0;
            ddlPNPregunta5.SelectedIndex = 0;
            ddlPNActividadEconomica.SelectedIndex = 0;
            ddlPNSector1.SelectedIndex = 0;
            ddlPNSector2.SelectedIndex = 0;
            #endregion

            #region PJ
            ddlPJCiudad1.ClearSelection();
            ddlPJCiudad2.ClearSelection();
            ddlPJDpto.ClearSelection();
            ddlPJDpto2.ClearSelection();
            ddlPJCodCIIU2.ClearSelection();

            ddlPJTipoDocumento.SelectedIndex = 0;
            ddlPJTipoEmpresa.SelectedIndex = 0;
            ddlPJActividadEconomica.SelectedIndex = 0;
            ddlPJTipoDocRepLegalPpal.SelectedIndex = 0;
            ddlPJTipoDocRepLegal1.SelectedIndex = 0;
            ddlPJTipoDocRepLegal2.SelectedIndex = 0;
            ddlPJTipoDocRepLegal3.SelectedIndex = 0;
            ddlPJTipoDocRepLegal4.SelectedIndex = 0;
            ddlPJPregunta1.SelectedIndex = 0;
            ddlPJPregunta2.SelectedIndex = 0;
            ddlPJPregunta3.SelectedIndex = 0;
            ddlPJPregunta4.SelectedIndex = 0;
            ddlPJPregunta5.SelectedIndex = 0;
            ddlPJPregunta6.SelectedIndex = 0;
            ddlPJPregunta7.SelectedIndex = 0;
            ddlPJPregunta8.SelectedIndex = 0;
            ddlPJPregunta9.SelectedIndex = 0;
            ddlPJPregunta10.SelectedIndex = 0;
            ddlPJPreguntaRep1Legal1.SelectedIndex = 0;
            ddlPJPreguntaRep1Legal2.SelectedIndex = 0;
            ddlPJPreguntaRep1Legal3.SelectedIndex = 0;
            ddlPJPreguntaRep1Legal4.SelectedIndex = 0;
            ddlPJPreguntaRep2Legal1.SelectedIndex = 0;
            ddlPJPreguntaRep2Legal2.SelectedIndex = 0;
            ddlPJPreguntaRep2Legal3.SelectedIndex = 0;
            ddlPJPreguntaRep2Legal4.SelectedIndex = 0;
            ddlPJPreguntaRep3Legal1.SelectedIndex = 0;
            ddlPJPreguntaRep3Legal2.SelectedIndex = 0;
            ddlPJPreguntaRep3Legal3.SelectedIndex = 0;
            ddlPJPreguntaRep3Legal4.SelectedIndex = 0;
            ddlPJPreguntaRep4Legal1.SelectedIndex = 0;
            ddlPJPreguntaRep4Legal2.SelectedIndex = 0;
            ddlPJPreguntaRep4Legal3.SelectedIndex = 0;
            ddlPJPreguntaRep4Legal4.SelectedIndex = 0;
            ddlPJPreguntaRepPpalLegal1.SelectedIndex = 0;
            ddlPJPreguntaRepPpalLegal2.SelectedIndex = 0;
            ddlPJPreguntaRepPpalLegal3.SelectedIndex = 0;
            ddlPJPreguntaRepPpalLegal4.SelectedIndex = 0;
            ddlPJCotizaBolsa.SelectedIndex = 0;
            ddlPJEstatal.SelectedIndex = 0;
            ddlPJSinAnimoLucro.SelectedIndex = 0;
            ddlPJSubsidiaria.SelectedIndex = 0;
            #endregion

            #region Entrevista
            ddlReclamaciones.SelectedIndex = 0;
            #endregion

            #region Campos Grilla
            TextBox99.Text = "";
            TextBox99.Text = "";
            TextBox103.Text = "";
            TextBox105.Text = "";
            TextBox106.Text = "";
            TextBox110.Text = "";
            TextBox111.Text = "";
            TextBox110.Visible = false;
            TextBox111.Visible = false;
            Label195.Visible = false;
            Label196.Visible = false;
            #endregion

            DDLEstados.SelectedIndex = 0;
            FechaDesde.Text = string.Empty;
            FechaHasta.Text = string.Empty;
        }

        private void CamposFormulario(bool booValor)
        {
            #region Cliente
            tbxFechaFormulario.Enabled = booValor;
            ddlClaseVinculacion.Enabled = booValor;
            tbxOtraClaseVinculacion.Enabled = booValor;
            ddlTomadorAsegurado.Enabled = booValor;
            tbxOtraTomadorAsegurado.Enabled = booValor;
            ddlTomadorBeneficiario.Enabled = booValor;
            tbxOtraTomadorBeneficiario.Enabled = booValor;
            ddlAseguradoBeneficiario.Enabled = booValor;
            tbxOtraAseguradoBeneficiario.Enabled = booValor;
            ddlTipoCliente.Enabled = booValor;
            tbxCiudad.Enabled = booValor;
            ddlSucursal.Enabled = booValor;
            ddlTipoSolicitud.Enabled = booValor;
            #endregion

            #region Informacion PN
            //tbxPNCIIU.Enabled = booValor; //Siempre debe estar inhabilitado
            tbxPNPrimerApellido.Enabled = booValor;
            tbxPNSegunApellido.Enabled = booValor;
            tbxPNNombres.Enabled = booValor;
            ddlPNTipoDocumento.Enabled = booValor;
            tbxPNNumeroDocumento.Enabled = booValor;
            tbxPNFechaExpedicion.Enabled = booValor;
            tbxPNLugar.Enabled = booValor;
            tbxPNFechaNacimiento.Enabled = booValor;
            tbxPNNacionalidad.Enabled = booValor;
            tbxPNOcupacionOficio.Enabled = booValor;
            ddlPNActividadEconomica.Enabled = booValor;
            ddlPNCIIUDescripcion.Enabled = booValor;
            tbxPNEmpresaTrabajo.Enabled = booValor;
            tbxPNCargo.Enabled = booValor;
            tbxPNDireccion.Enabled = booValor;
            tbxPNTelefono1.Enabled = booValor;
            tbxPNDireccionResidencia.Enabled = booValor;
            ddlPNCiudad2.Enabled = booValor;
            tbxPNTelefono2.Enabled = booValor;
            tbxPNCelular.Enabled = booValor;
            ddlPNPregunta1.Enabled = booValor;
            ddlPNPregunta2.Enabled = booValor;
            ddlPNPregunta3.Enabled = booValor;
            tbxPNEspecificacionPreguntas.Enabled = booValor;
            tbxPNIngresosMensuales.Enabled = booValor;
            tbxPNActivos.Enabled = booValor;
            tbxPNEgresoMensuales.Enabled = booValor;
            tbxPNPasivos.Enabled = booValor;
            tbxPNOtrosIngresos.Enabled = booValor;
            tbxPNConceptoOtrosIngresos.Enabled = booValor;
            tbxPNCorreoElectronico.Enabled = booValor;
            tbxPNLugarNmto.Enabled = booValor;
            ddlPNDpto.Enabled = booValor;
            tbxPNOtraActEconomica.Enabled = booValor;
            tbxPNPatrimonio.Enabled = booValor;
            tbxPNDireccionEmpresa.Enabled = booValor;
            ddlPNCiudadEmpresa.Enabled = booValor;
            tbxPNTelefonoEmpresa.Enabled = booValor;
            ddlPNPregunta4.Enabled = booValor;
            ddlPNPregunta5.Enabled = booValor;
            tbxPNEspecificacionPreguntas2.Enabled = booValor;
            ddlPNSector1.Enabled = booValor;
            ddlPNSector2.Enabled = booValor;
            ddlPNDptoEmpresa.Enabled = booValor;
            ddlPNDpto2.Enabled = booValor;
            tbxPNServicio.Enabled = booValor;
            ddlPNCiudad1.Enabled = booValor;
            ddlTipoActividadPN.Enabled = booValor;
            TXvalorOtroTipoActividadPN.Enabled = booValor;
            TXprimerApellidoPNPPE.Enabled = booValor;
            TXsegundApellidoPNPPE.Enabled = booValor;
            TXnombrePNPPE.Enabled = booValor;
            TXocupacionPNPPE.Enabled = booValor;
            TXcargoPNPPE.Enabled = booValor;
            #endregion

            #region Informacion PJ
            tbxPJRazonDenominacion.Enabled = booValor;
            tbxPJNIT.Enabled = booValor;
            tbxPJPrimerApellido.Enabled = booValor;
            tbxPJSegundoApellido.Enabled = booValor;
            DDLPJcodCiiu.Enabled = booValor;
            tbxPJNombres.Enabled = booValor;
            tbxPJNumeroDocumento.Enabled = booValor;
            tbxPJLugarExpedicion.Enabled = booValor;
            tbxPJFechaExpedicion.Enabled = booValor;
            tbxPJDireccionOficina.Enabled = booValor;
            tbxPJTelefono1.Enabled = booValor;
            tbxPJDireccionSucursal.Enabled = booValor;
            tbxPJTelefono2.Enabled = booValor;
            tbxPJIngresosMensuales.Enabled = booValor;
            tbxPJActivos.Enabled = booValor;
            tbxPJEgresoMensuales.Enabled = booValor;
            tbxPJPasivos.Enabled = booValor;
            tbxPJOtrosIngresos.Enabled = booValor;
            tbxPJConceptoOtrosIngresos.Enabled = booValor;
            tbxPJCorreoPrincipal.Enabled = booValor;
            tbxPJFechaConstitucion.Enabled = booValor;
            tbxPJNombresRepLegalPpal.Enabled = booValor;
            tbxPJDocumentoRepLegalPpal.Enabled = booValor;
            tbxPJNombresRepLegal1.Enabled = booValor;
            tbxPJDocumentoRepLegal1.Enabled = booValor;
            tbxPJNombresRepLegal2.Enabled = booValor;
            tbxPJDocumentoRepLegal2.Enabled = booValor;
            tbxPJNombresRepLegal3.Enabled = booValor;
            tbxPJDocumentoRepLegal3.Enabled = booValor;
            tbxPJNombresRepLegal4.Enabled = booValor;
            tbxPJDocumentoRepLegal4.Enabled = booValor;
            tbxPJNacionalidad1.Enabled = booValor;
            tbxPJDV.Enabled = booValor;
            tbxPJEspecificacionPreguntas.Enabled = booValor;
            tbxPJNIT1.Enabled = booValor;
            tbxPJPais1.Enabled = booValor;
            tbxPJNIT2.Enabled = booValor;
            tbxPJPais2.Enabled = booValor;
            tbxPJNIT3.Enabled = booValor;
            tbxPJPais3.Enabled = booValor;
            tbxPJNIT4.Enabled = booValor;
            tbxPJPais4.Enabled = booValor;
            tbxPJDireccionFiscal1.Enabled = booValor;
            tbxPJDireccionFiscal2.Enabled = booValor;
            tbxPJParticipacion1.Enabled = booValor;
            tbxPJParticipacion2.Enabled = booValor;
            tbxPJParticipacion3.Enabled = booValor;
            tbxPJParticipacion4.Enabled = booValor;
            tbxPJParticipacion5.Enabled = booValor;
            ddlPJCotizaBolsa.Enabled = booValor;
            ddlPJEstatal.Enabled = booValor;
            ddlPJSinAnimoLucro.Enabled = booValor;
            ddlPJSubsidiaria.Enabled = booValor;
            tbxPJSocMatriz.Enabled = booValor;
            tbxPJSocMatrizIdenTrib.Enabled = booValor;
            tbxPJSocMatrizJurisdiccion.Enabled = booValor;
            tbxPJSocMatrizDireccion.Enabled = booValor;
            tbxPJSocMatrizCiudad.Enabled = booValor;
            tbxPJSocMatrizTelefono.Enabled = booValor;
            tbxPJLugarNmto.Enabled = booValor;
            ddlPJTipoDocRepLegalPpal.Enabled = booValor;
            ddlPJTipoDocRepLegal1.Enabled = booValor;
            ddlPJTipoDocRepLegal2.Enabled = booValor;
            ddlPJTipoDocRepLegal3.Enabled = booValor;
            ddlPJTipoDocRepLegal4.Enabled = booValor;

            TrVinculoPPExpuestaPJ.Visible = false;
            ddlTipoDocumentoPJempresa.Enabled = booValor;
            TXprimerApellidoPPE.Enabled = booValor;
            TXsegundoApellidoPPE.Enabled = booValor;
            TXnombresPPE.Enabled = booValor;
            TXocipacionPPE.Enabled = booValor;
            TXcargoPPE.Enabled = booValor;
            #endregion

            #region Informacion Financiera
            ddlTransacMonedaExtra.Enabled = booValor;
            ddlTipoTransaccion.Enabled = booValor;
            tbxOtroTipoTransaccion.Enabled = booValor;
            tbxPFTipoProducto1.Enabled = booValor;
            tbxPFNumeroProducto1.Enabled = booValor;
            tbxPFEntidad1.Enabled = booValor;
            tbxPFMonto1.Enabled = booValor;
            tbxPFCiudad1.Enabled = booValor;
            tbxPFPais1.Enabled = booValor;
            tbxPFMoneda1.Enabled = booValor;
            tbxPFTipoProducto2.Enabled = booValor;
            tbxPFNumeroProducto2.Enabled = booValor;
            tbxPFEntidad2.Enabled = booValor;
            tbxPFMonto2.Enabled = booValor;
            tbxPFCiudad2.Enabled = booValor;
            tbxPFPais2.Enabled = booValor;
            tbxPFMoneda2.Enabled = booValor;
            ddlPFCtaMonedaExtra.Enabled = booValor;
            ddlPFProdExterior.Enabled = booValor;
            tbxPFTipoProducto3.Enabled = booValor;
            tbxPFNumeroProducto3.Enabled = booValor;
            tbxPFEntidad3.Enabled = booValor;
            tbxPFMonto3.Enabled = booValor;
            tbxPFCiudad3.Enabled = booValor;
            tbxPFPais3.Enabled = booValor;
            tbxPFMoneda3.Enabled = booValor;
            #endregion

            #region Seguros
            tbxSeguroAno1.Enabled = booValor;
            tbxSeguroRamo1.Enabled = booValor;
            tbxSeguroCompania1.Enabled = booValor;
            tbxSeguroValor1.Enabled = booValor;
            ddlSeguroTipo1.Enabled = booValor;
            tbxSeguroAno2.Enabled = booValor;
            tbxSeguroRamo2.Enabled = booValor;
            tbxSeguroCompania2.Enabled = booValor;
            tbxSeguroValor2.Enabled = booValor;
            ddlSeguroTipo2.Enabled = booValor;
            tbxOrigenFondos.Enabled = booValor;
            ddlReclamaciones.Enabled = booValor;
            #endregion

            #region Entrevista
            tbxLugarEntrevista.Enabled = booValor;
            tbxFechaEntrevista.Enabled = booValor;
            tbxHoraEntrevista.Enabled = booValor;
            ddlResultado.Enabled = booValor;
            tbxObservaciones1.Enabled = booValor;
            tbxNombreResponsable.Enabled = booValor;
            tbxFechaVerificacion.Enabled = booValor;
            tbxHoraVerificacion.Enabled = booValor;
            tbxNombreVerifica.Enabled = booValor;
            tbxObservaciones2.Enabled = booValor;
            tbxNombreIntermediario.Enabled = booValor;

            #endregion
            FileUpload1.Dispose();
        }

        private void disableCampos()
        {
            CamposFormulario(false);
            FileUpload1.Enabled = false;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;

        }

        private void enableCampos()
        {
            CamposFormulario(true);
            FileUpload1.Enabled = true;
            Button1.Visible = true;
            Button2.Visible = true;
            Button3.Visible = true;
        }

        private void Mensaje(string Mensaje, int intTipoMensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
            if (intTipoMensaje == 1)//ok
                imgInfo.ImageUrl = "~/Imagenes/Icons/icontexto-webdev-ok.png";
            else if (intTipoMensaje == 2)//Alerta
                imgInfo.ImageUrl = "~/Imagenes/Icons/icontexto-webdev-alert.png";
            else//Error
                imgInfo.ImageUrl = "~/Imagenes/Icons/icontexto-webdev-cancel.png";

        }

        private void BuscarFCC()
        {
            try
            {

                DataTable dtInfo = new DataTable();

                loadGrid();

                dtInfo = cKnowClient.buscarClienteFCC(DDLTipoPersona.SelectedValue.ToString(), TxBxPApellido.Text.Trim(), TxBxSApellido.Text.Trim(),
                    TxBxNombre.Text.Trim(), TxBxNumeiden.Text.Trim(), TxBxRazonSocial.Text.Trim(), TxBxNit.Text.Trim(), TxbFechaIni.Text.Trim(),
                    TxbFechaFin.Text.Trim(), DDLEstado.SelectedValue.ToString());

                if (dtInfo.Rows.Count > 0)
                {
                    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                    {
                        InfoGrid.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["Id"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdTipoPersona"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoPersona"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Documento"].ToString().Trim(),
                        dtInfo.Rows[rows]["nombreCliente"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdEstado"].ToString().Trim(),
                        dtInfo.Rows[rows]["estadoFormulario"].ToString().Trim(),
                        dtInfo.Rows[rows]["fechaActualizacion"].ToString().Trim(),
                        });
                    }
                    TrGvResultados.Visible = true;
                    TrGvExportar.Visible = true;
                    
                    GridView1.PageIndex = PagIndexInfoGridFormCliente;
                    GridView1.DataSource = InfoGrid;
                    GridView1.DataBind();

                }
                else
                {
                    TrGvResultados.Visible = false;
                    TrGvExportar.Visible = false;
                    Mensaje("No se encontraron registros asociados a los parámetros de búsqueda.",1);
                }
            }
            catch(Exception ex)
            {
                Mensaje("Error al cargar la información." + ex.Message,3);
            }
        }

           

        private void buscarClientForm(string NaOJu, string PrimerApellido, string SegundoApellido, string Nombre,
            string NumeroDocumento, string RazonSocial, string NIT)
        {
            DataTable dtInfo = new DataTable();

            loadGrid();
            resetValues();
            disableCampos();


            if (NaOJu == "0")
            {
                dtInfo = cKnowClient.buscarClientFormPN(PrimerApellido, SegundoApellido, Nombre, NumeroDocumento);
                GridView1.Columns[11].Visible = false;
                GridView1.Columns[12].Visible = false;
            }
            else
            {
                dtInfo = cKnowClient.buscarClientFormPJ(PrimerApellido, SegundoApellido, Nombre, NumeroDocumento, RazonSocial, NIT);

                GridView1.Columns[11].Visible = true;
                GridView1.Columns[12].Visible = true;
            }

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    DataTable dt = new DataTable();
                    dt = cKnowClient.InfoAprobadoFCC(Convert.ToInt32(dtInfo.Rows[rows]["IdConocimientoCliente"].ToString().Trim()));
                    if(dt.Rows.Count > 0)
                    { 
                    InfoGrid.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdConocimientoCliente"].ToString().Trim(),
                        dtInfo.Rows[rows]["PrimerApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["SegundoApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NumeroDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ano"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dt.Rows[0]["Estado"].ToString().Trim(),
                        dt.Rows[0]["Usuario"].ToString().Trim(),
                        dt.Rows[0]["FechaAprobado"].ToString().Trim(),
                        dtInfo.Rows[rows]["RazonDenominacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NIT"].ToString().Trim(),
                        
                        });
                    }
                    else
                    {
                        InfoGrid.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdConocimientoCliente"].ToString().Trim(),
                        dtInfo.Rows[rows]["PrimerApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["SegundoApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NumeroDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ano"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        dtInfo.Rows[rows]["RazonDenominacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NIT"].ToString().Trim(),
                        
                        });
                    }
                }

                GridView1.PageIndex = PagIndexInfoGridFormCliente;
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
                //Button7.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                //Button7.Visible = false;
                Mensaje("No se encontraron registros asociados a los parámetros de búsqueda.",1);
            }
        }

        private void cargarInfoFCC_Validar(Int32 IdRegistro,string TipoPersona)
        {
            try
            {
                trIframe.Visible = true;
                if (TipoPersona == "1")
                    URL_FCC += "personanatural/ValidatePN?IdFormulario=" + IdRegistro + "&IdEstado=1&nasesor=" + Session["NombreUsuario"].ToString() + " / Asistente Sarlaft";//&UserWP=" + ConfigurationManager.AppSettings["UsuarioWillis"].ToString();
                else
                    URL_FCC += "PersonaJuridica/ValidatePJ?IdFormulario=" + IdRegistro + "&IdEstado=1&nasesor=" + Session["NombreUsuario"].ToString() + " / Asistente Sarlaft";//&UserWP=" + ConfigurationManager.AppSettings["UsuarioWillis"].ToString();

                //iframeFCC_Validar.Attributes.Add("src", URL_FCC);
                Iframe_FCC.Attributes.Add("src", URL_FCC);
                BtnCerrarIFrame.Visible = true;
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar información FCC  -->> " + ex.Message,3);
            }

        }

        private void cargarInfoFCC_Consultar(Int32 IdRegistro, string TipoPersona)
        {
            try
            {
                trIframe.Visible = true;
                if (TipoPersona == "1")
                    URL_FCC += "PersonaNatural/Index?idFormulario=" + IdRegistro;
                else
                    URL_FCC += "PersonaJuridica/Index?IdFormulario=" + IdRegistro;

                //Iframe_FCC_Validar.Attributes.Add("src", URL_FCC);
                Iframe_FCC.Attributes.Add("src", URL_FCC);
                BtnCerrarIFrame.Visible = true;
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar información FCC  -->> " + ex.Message,3);
            }

        }

        private void cargarInfoFCC_Editar(string IdRegistro)
        {
            URL_FCC += "personanatural/Index?IdFormulario=" + IdRegistro + "&IdEstado=1";
            Iframe_FCC.Attributes.Add("src", URL_FCC);
        }

        private void cargarInfoFCC_Imprimir(Int32 IdRegistro, string TipoPersona)
        {
            try
            {
                    if (TipoPersona == "1")
                        URL_FCC_Print += "Home/PrintPdf?IdForm=" + IdRegistro + "&IdTypePerson=1";
                    else
                        URL_FCC_Print += "Home/PrintPdf?IdForm=" + IdRegistro + "&IdTypePerson=2";

                    Iframe_FCC.Attributes.Add("src", URL_FCC_Print);

            }
            catch (Exception ex)
            {
                Mensaje("Error al imprimir PDF -->> " + ex.Message,3);
            }
        }


        private void cargarInfo()
        {
            DataTable dtInfo = new DataTable();

            enableCampos();

            #region Encabezado
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente, false);
            tbxFechaFormulario.Text = dtInfo.Rows[0]["FechaFormulario"].ToString().Trim();
            tbxOtraClaseVinculacion.Text = dtInfo.Rows[0]["OtraClaseVinculacion"].ToString().Trim();
            tbxOtraTomadorAsegurado.Text = dtInfo.Rows[0]["OtraTomadorAsegurado"].ToString().Trim();
            tbxOtraTomadorBeneficiario.Text = dtInfo.Rows[0]["OtraTomadorBeneficiario"].ToString().Trim();
            tbxOtraAseguradoBeneficiario.Text = dtInfo.Rows[0]["OtraAseguradoBeneficiario"].ToString().Trim();
            tbxCiudad.Text = dtInfo.Rows[0]["Ciudad"].ToString().Trim();
            for (int i = 0; i < ddlClaseVinculacion.Items.Count; i++) { ddlClaseVinculacion.SelectedIndex = i; if (ddlClaseVinculacion.SelectedItem.Text.Trim() == dtInfo.Rows[0]["ClaseVinculacion"].ToString().Trim()) break; else ddlClaseVinculacion.SelectedIndex = 0; }
            for (int i = 0; i < ddlTomadorAsegurado.Items.Count; i++) { ddlTomadorAsegurado.SelectedIndex = i; if (ddlTomadorAsegurado.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TomadorAsegurado"].ToString().Trim()) break; else ddlTomadorAsegurado.SelectedIndex = 0; }
            for (int i = 0; i < ddlTomadorBeneficiario.Items.Count; i++) { ddlTomadorBeneficiario.SelectedIndex = i; if (ddlTomadorBeneficiario.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TomadorBeneficiario"].ToString().Trim()) break; else ddlTomadorBeneficiario.SelectedIndex = 0; }
            for (int i = 0; i < ddlAseguradoBeneficiario.Items.Count; i++) { ddlAseguradoBeneficiario.SelectedIndex = i; if (ddlAseguradoBeneficiario.SelectedItem.Text.Trim() == dtInfo.Rows[0]["AseguradoBeneficiario"].ToString().Trim()) break; else ddlAseguradoBeneficiario.SelectedIndex = 0; }
            for (int i = 0; i < ddlTipoCliente.Items.Count; i++) { ddlTipoCliente.SelectedIndex = i; if (ddlTipoCliente.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TipoCliente"].ToString().Trim()) break; else ddlTipoCliente.SelectedIndex = 0; }
            for (int i = 0; i < ddlSucursal.Items.Count; i++) { ddlSucursal.SelectedIndex = i; if (ddlSucursal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Sucursal"].ToString().Trim()) break; else ddlSucursal.SelectedIndex = 0; }
            for (int i = 0; i < ddlTipoSolicitud.Items.Count; i++) { ddlTipoSolicitud.SelectedIndex = i; if (ddlTipoSolicitud.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TipoSolicitud"].ToString().Trim()) break; else ddlTipoSolicitud.SelectedIndex = 0; }

            TXusuarioEntrevista.Text = dtInfo.Rows[0]["Usuario"].ToString().Trim();
            TXfechaRegistroEntrevista.Text = dtInfo.Rows[0]["FechaRegistro"].ToString().Trim();

            TXusuarioVerificacion.Text = dtInfo.Rows[0]["Usuario"].ToString().Trim();
            TXfechaRegistroVerificacion.Text = dtInfo.Rows[0]["FechaRegistro"].ToString().Trim();

            mtdHabilitaVinculacion(dtInfo.Rows[0]["ClaseVinculacion"].ToString().Trim().Equals("OTRO"));
            mtdHabilitaVinculos(0, dtInfo.Rows[0]["TomadorAsegurado"].ToString().Trim().Equals("OTRA"));
            mtdHabilitaVinculos(1, dtInfo.Rows[0]["TomadorBeneficiario"].ToString().Trim().Equals("OTRA"));
            mtdHabilitaVinculos(2, dtInfo.Rows[0]["AseguradoBeneficiario"].ToString().Trim().Equals("OTRA"));

            #endregion

            if (DropDownList39.SelectedValue.ToString() == "0")
            {
                mtdHabilitarTipoFormulario(1, true);

                for (int i = 0; i < ddlTipoFormulario.Items.Count; i++)
                {
                    ddlTipoFormulario.SelectedIndex = i;
                    if (ddlTipoFormulario.SelectedItem.Text.Trim() == "PERSONA NATURAL")
                        break;
                }

                #region Persona Natural
                dtInfo = cKnowClient.InfoFormPN(IdConocimientoCliente);

                tbxPNPrimerApellido.Text = dtInfo.Rows[0]["PNPrimerApellido"].ToString().Trim();
                tbxPNSegunApellido.Text = dtInfo.Rows[0]["PNSegunApellido"].ToString().Trim();
                tbxPNNombres.Text = dtInfo.Rows[0]["PNNombres"].ToString().Trim();
                tbxPNNumeroDocumento.Text = dtInfo.Rows[0]["PNNumeroDocumento"].ToString().Trim();
                tbxPNFechaExpedicion.Text = dtInfo.Rows[0]["PNFechaExpedicion"].ToString().Trim();
                tbxPNLugar.Text = dtInfo.Rows[0]["PNLugar"].ToString().Trim();
                tbxPNFechaNacimiento.Text = dtInfo.Rows[0]["PNFechaNacimiento"].ToString().Trim();
                tbxPNNacionalidad.Text = dtInfo.Rows[0]["PNNacionalidad"].ToString().Trim();
                tbxPNOcupacionOficio.Text = dtInfo.Rows[0]["PNOcupacionOficio"].ToString().Trim();
                tbxPNCIIU.Text = dtInfo.Rows[0]["PNCIIU"].ToString().Trim();
                tbxPNEmpresaTrabajo.Text = dtInfo.Rows[0]["PNEmpresaTrabajo"].ToString().Trim();
                tbxPNCargo.Text = dtInfo.Rows[0]["PNCargo"].ToString().Trim();
                tbxPNDireccion.Text = dtInfo.Rows[0]["PNDireccion"].ToString().Trim();
                tbxPNTelefono1.Text = dtInfo.Rows[0]["PNTelefono1"].ToString().Trim();
                tbxPNDireccionResidencia.Text = dtInfo.Rows[0]["PNDireccionResidencia"].ToString().Trim();
                tbxPNTelefono2.Text = dtInfo.Rows[0]["PNTelefono2"].ToString().Trim();
                tbxPNCelular.Text = dtInfo.Rows[0]["PNCelular"].ToString().Trim();
                tbxPNEspecificacionPreguntas.Text = dtInfo.Rows[0]["PNEspecificacionPreguntas"].ToString().Trim();
                tbxPNIngresosMensuales.Text = dtInfo.Rows[0]["PNIngresosMensuales"].ToString().Trim();
                tbxPNActivos.Text = dtInfo.Rows[0]["PNActivos"].ToString().Trim();
                tbxPNEgresoMensuales.Text = dtInfo.Rows[0]["PNEgresoMensuales"].ToString().Trim();
                tbxPNPasivos.Text = dtInfo.Rows[0]["PNPasivos"].ToString().Trim();
                tbxPNOtrosIngresos.Text = dtInfo.Rows[0]["PNOtrosIngresos"].ToString().Trim();
                tbxPNConceptoOtrosIngresos.Text = dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString().Trim();
                tbxPNCorreoElectronico.Text = dtInfo.Rows[0]["PNCorreoElectronico"].ToString().Trim();
                tbxPNOtraActEconomica.Text = dtInfo.Rows[0]["PNOtraActEconomica"].ToString().Trim();
                tbxPNPatrimonio.Text = dtInfo.Rows[0]["PNPatrimonio"].ToString().Trim();
                tbxPNDireccionEmpresa.Text = dtInfo.Rows[0]["PNDireccionEmpresa"].ToString().Trim();
                tbxPNTelefonoEmpresa.Text = dtInfo.Rows[0]["PNTelefonoEmpresa"].ToString().Trim();
                tbxPNEspecificacionPreguntas2.Text = dtInfo.Rows[0]["PNEspecificacionPreguntas2"].ToString().Trim();
                tbxPNLugarNmto.Text = dtInfo.Rows[0]["PNLugarNmto"].ToString().Trim();
                tbxPNServicio.Text = dtInfo.Rows[0]["PNServicio"].ToString().Trim();
                TXvalorOtroTipoActividadPN.Text = dtInfo.Rows[0]["PNTipoActividadOtra"].ToString().Trim();
                TXprimerApellidoPNPPE.Text = dtInfo.Rows[0]["PNpePrimerApellido"].ToString().Trim();
                TXsegundApellidoPNPPE.Text = dtInfo.Rows[0]["PNpeSegundoApellido"].ToString().Trim();
                TXnombrePNPPE.Text = dtInfo.Rows[0]["PNpeNombres"].ToString().Trim();
                TXocupacionPNPPE.Text = dtInfo.Rows[0]["PNpeOcupacion"].ToString().Trim();
                TXcargoPNPPE.Text = dtInfo.Rows[0]["PNpeCargo"].ToString().Trim();

                if (dtInfo.Rows[0]["PNPregunta4"].ToString().Trim() == "SI")
                    TrVinculoPPExpuestaPN.Visible = true;

                #region Carga Datos DB
                ddlPNDpto.DataBind();
                ddlPNDpto2.DataBind();
                ddlPNDptoEmpresa.DataBind();
                ddlPNCIIUDescripcion.DataBind();
                DDLcodCIIU.DataBind();
                ddlTipoActividadPN.DataBind();
                
                #endregion
                #region Departamento y Ciudad 1
                if (dtInfo.Rows[0]["PNDpto"].ToString().Trim() != string.Empty)
                {
                    for (int i = 0; i < ddlPNDpto.Items.Count; i++)
                    {
                        ddlPNDpto.SelectedIndex = i;
                        if (ddlPNDpto.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PNDpto"].ToString().Trim())
                        {
                            ddlPNCiudad1.DataBind();
                            for (int ic = 0; ic < ddlPNCiudad1.Items.Count; ic++)
                            {
                                ddlPNCiudad1.SelectedIndex = ic;
                                if (ddlPNCiudad1.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PNCiudad1"].ToString().Trim())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            ddlPNDpto.SelectedIndex = 0;
                        }
                    }
                }
                #endregion
                #region Departamento y Ciudad 2
                if (dtInfo.Rows[0]["PNDpto2"].ToString().Trim() != string.Empty)
                {
                    for (int i = 0; i < ddlPNDpto2.Items.Count; i++)
                    {
                        ddlPNDpto2.SelectedIndex = i;
                        if (ddlPNDpto2.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PNDpto2"].ToString().Trim())
                        {
                            ddlPNCiudad2.DataBind();
                            for (int ic = 0; ic < ddlPNCiudad2.Items.Count; ic++)
                            {
                                ddlPNCiudad2.SelectedIndex = ic;
                                if (ddlPNCiudad2.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PNCiudad2"].ToString().Trim())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            ddlPNDpto2.SelectedIndex = 0;
                        }
                    }
                }
                #endregion
                #region Departamento y Ciudad Empresa
                if (dtInfo.Rows[0]["PNDptoEmpresa"].ToString().Trim() != string.Empty)
                {
                    for (int i = 0; i < ddlPNDptoEmpresa.Items.Count; i++)
                    {
                        ddlPNDptoEmpresa.SelectedIndex = i;
                        if (ddlPNDptoEmpresa.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PNDptoEmpresa"].ToString().Trim())
                        {
                            ddlPNCiudadEmpresa.DataBind();
                            for (int ic = 0; ic < ddlPNCiudadEmpresa.Items.Count; ic++)
                            {
                                ddlPNCiudadEmpresa.SelectedIndex = ic;
                                if (ddlPNCiudadEmpresa.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PNCiudadEmpresa"].ToString().Trim())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            ddlPNDptoEmpresa.SelectedIndex = 0;
                        }
                    }
                }
                #endregion
                for (int i = 0; i < ddlPNCIIUDescripcion.Items.Count; i++) {
                    ddlPNCIIUDescripcion.SelectedIndex = i;
                    //DDLcodCIIU.SelectedIndex = i;
                    if (ddlPNCIIUDescripcion.SelectedValue.ToString().Trim() == dtInfo.Rows[0]["PNCIIU"].ToString().Trim())
                        break;
                    else
                        ddlPNCIIUDescripcion.SelectedIndex = 0;
                }
                for (int i = 0; i < ddlTipoActividadPN.Items.Count; i++)
                {
                    ddlTipoActividadPN.SelectedIndex = i;
                    
                    if (ddlTipoActividadPN.SelectedItem.ToString() == dtInfo.Rows[0]["PNTipoActividad"].ToString().Trim())
                        break;
                    else
                        ddlTipoActividadPN.SelectedIndex = 0;
                }
                //ddlTipoActividadPN.SelectedValue = dtInfo.Rows[0]["PNTipoActividad"].ToString().Trim();
                if (dtInfo.Rows[0]["PNTipoActividad"].ToString().Trim() == "Otro, cual?")
                {
                    tdOtroTipoActividadPN.Visible = true;
                    tdValorOtroTipoActividadPN.Visible = true;
                }
                else
                {
                    tdOtroTipoActividadPN.Visible = false;
                    tdValorOtroTipoActividadPN.Visible = false;
                }
                DDLcodCIIU.SelectedValue = ddlPNCIIUDescripcion.SelectedValue.ToString();
                for (int i = 0; i < ddlPNTipoDocumento.Items.Count; i++) { ddlPNTipoDocumento.SelectedIndex = i; if (ddlPNTipoDocumento.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNTipoDocumento"].ToString().Trim()) break; else ddlPNTipoDocumento.SelectedIndex = 0; }
                for (int i = 0; i < ddlPNActividadEconomica.Items.Count; i++) { ddlPNActividadEconomica.SelectedIndex = i; if (ddlPNActividadEconomica.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNActividadEconomica"].ToString().Trim()) break; else ddlPNActividadEconomica.SelectedIndex = 0; }
                for (int i = 0; i < ddlPNPregunta1.Items.Count; i++) { ddlPNPregunta1.SelectedIndex = i; if (ddlPNPregunta1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta1"].ToString().Trim()) break; else ddlPNPregunta1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPNPregunta2.Items.Count; i++) { ddlPNPregunta2.SelectedIndex = i; if (ddlPNPregunta2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta2"].ToString().Trim()) break; else ddlPNPregunta2.SelectedIndex = 0; }
                for (int i = 0; i < ddlPNPregunta3.Items.Count; i++) { ddlPNPregunta3.SelectedIndex = i; if (ddlPNPregunta3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta3"].ToString().Trim()) break; else ddlPNPregunta3.SelectedIndex = 0; }
                for (int i = 0; i < ddlPNPregunta4.Items.Count; i++) { ddlPNPregunta4.SelectedIndex = i; if (ddlPNPregunta4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta4"].ToString().Trim()) break; else ddlPNPregunta4.SelectedIndex = 0; }
                for (int i = 0; i < ddlPNPregunta5.Items.Count; i++) { ddlPNPregunta5.SelectedIndex = i; if (ddlPNPregunta5.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta5"].ToString().Trim()) break; else ddlPNPregunta5.SelectedIndex = 0; }
                for (int i = 0; i < ddlPNSector1.Items.Count; i++) { ddlPNSector1.SelectedIndex = i; if (ddlPNSector1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNSector1"].ToString().Trim()) break; else ddlPNSector1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPNSector2.Items.Count; i++) { ddlPNSector2.SelectedIndex = i; if (ddlPNSector2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNSector2"].ToString().Trim()) break; else ddlPNSector2.SelectedIndex = 0; }

                mtdHabilitaPregunta4(dtInfo.Rows[0]["PNEspecificacionPreguntas"].ToString().Trim().Length>0);
                mtdHabilitaPregunta5(dtInfo.Rows[0]["PNEspecificacionPreguntas2"].ToString().Trim().Length > 0);
                #endregion
            }
            else
            {
                mtdHabilitarTipoFormulario(2, true);

                for (int i = 0; i < ddlTipoFormulario.Items.Count; i++)
                {
                    ddlTipoFormulario.SelectedIndex = i;
                    if (ddlTipoFormulario.SelectedItem.Text.Trim() == "PERSONA JURÍDICA")
                        break;
                }

                #region Persona Juridica
                dtInfo = cKnowClient.InfoFormPJ(IdConocimientoCliente);
                tbxPJRazonDenominacion.Text = dtInfo.Rows[0]["PJRazonDenominacion"].ToString().Trim();
                tbxPJNIT.Text = dtInfo.Rows[0]["PJNIT"].ToString().Trim();
                tbxPJPrimerApellido.Text = dtInfo.Rows[0]["PJPrimerApellido"].ToString().Trim();
                tbxPJSegundoApellido.Text = dtInfo.Rows[0]["PJSegundoApellido"].ToString().Trim();
                tbxPJNombres.Text = dtInfo.Rows[0]["PJNombres"].ToString().Trim();
                tbxPJNumeroDocumento.Text = dtInfo.Rows[0]["PJNumeroDocumento"].ToString().Trim();
                tbxPJLugarExpedicion.Text = dtInfo.Rows[0]["PJLugarExpedicion"].ToString().Trim();
                tbxPJFechaExpedicion.Text = dtInfo.Rows[0]["PJFechaExpedicion"].ToString().Trim();
                tbxPJDireccionOficina.Text = dtInfo.Rows[0]["PJDireccionOficina"].ToString().Trim();
                tbxPJTelefono1.Text = dtInfo.Rows[0]["PJTelefono1"].ToString().Trim();
                tbxPJDireccionSucursal.Text = dtInfo.Rows[0]["PJDireccionSucursal"].ToString().Trim();
                tbxPJTelefono2.Text = dtInfo.Rows[0]["PJTelefono2"].ToString().Trim();
                tbxPJCIIU.Text = dtInfo.Rows[0]["PJCIIU"].ToString().Trim();
                tbxPJIngresosMensuales.Text = dtInfo.Rows[0]["PJIngresosMensuales"].ToString().Trim();
                tbxPJActivos.Text = dtInfo.Rows[0]["PJActivos"].ToString().Trim();
                tbxPJEgresoMensuales.Text = dtInfo.Rows[0]["PJEgresoMensuales"].ToString().Trim();
                tbxPJPasivos.Text = dtInfo.Rows[0]["PJPasivos"].ToString().Trim();
                tbxPJOtrosIngresos.Text = dtInfo.Rows[0]["PJOtrosIngresos"].ToString().Trim();
                tbxPJConceptoOtrosIngresos.Text = dtInfo.Rows[0]["PJConceptoOtrosIngresos"].ToString().Trim();
                tbxPJCorreoPrincipal.Text = dtInfo.Rows[0]["PJCorreoPrincipal"].ToString().Trim();
                tbxPJFechaConstitucion.Text = dtInfo.Rows[0]["PJFechaConstitucion"].ToString().Trim();
                tbxPJNombresRepLegalPpal.Text = dtInfo.Rows[0]["PJNombresRepLegalPpal"].ToString().Trim();
                tbxPJDocumentoRepLegalPpal.Text = dtInfo.Rows[0]["PJDocumentoRepLegalPpal"].ToString().Trim();
                tbxPJNombresRepLegal1.Text = dtInfo.Rows[0]["PJNombresRepLegal1"].ToString().Trim();
                tbxPJDocumentoRepLegal1.Text = dtInfo.Rows[0]["PJDocumentoRepLegal1"].ToString().Trim();
                tbxPJNombresRepLegal2.Text = dtInfo.Rows[0]["PJNombresRepLegal2"].ToString().Trim();
                tbxPJDocumentoRepLegal2.Text = dtInfo.Rows[0]["PJDocumentoRepLegal2"].ToString().Trim();
                tbxPJNombresRepLegal3.Text = dtInfo.Rows[0]["PJNombresRepLegal3"].ToString().Trim();
                tbxPJDocumentoRepLegal3.Text = dtInfo.Rows[0]["PJDocumentoRepLegal3"].ToString().Trim();
                tbxPJNombresRepLegal4.Text = dtInfo.Rows[0]["PJNombresRepLegal4"].ToString().Trim();
                tbxPJDocumentoRepLegal4.Text = dtInfo.Rows[0]["PJDocumentoRepLegal4"].ToString().Trim();
                tbxPJNacionalidad1.Text = dtInfo.Rows[0]["PJNacionalidad1"].ToString().Trim();
                tbxPJDV.Text = dtInfo.Rows[0]["PJDV"].ToString().Trim();
                tbxPJEspecificacionPreguntas.Text = dtInfo.Rows[0]["PJEspecificacionPreguntas"].ToString().Trim();
                tbxPJNIT1.Text = dtInfo.Rows[0]["PJNIT1"].ToString().Trim();
                tbxPJPais1.Text = dtInfo.Rows[0]["PJPais1"].ToString().Trim();
                tbxPJNIT2.Text = dtInfo.Rows[0]["PJNIT2"].ToString().Trim();
                tbxPJPais2.Text = dtInfo.Rows[0]["PJPais2"].ToString().Trim();
                tbxPJNIT3.Text = dtInfo.Rows[0]["PJNIT3"].ToString().Trim();
                tbxPJPais3.Text = dtInfo.Rows[0]["PJPais3"].ToString().Trim();
                tbxPJNIT4.Text = dtInfo.Rows[0]["PJNIT4"].ToString().Trim();
                tbxPJPais4.Text = dtInfo.Rows[0]["PJPais4"].ToString().Trim();
                tbxPJDireccionFiscal1.Text = dtInfo.Rows[0]["PJDireccionFiscal1"].ToString().Trim();
                tbxPJDireccionFiscal2.Text = dtInfo.Rows[0]["PJDireccionFiscal2"].ToString().Trim();
                tbxPJParticipacion1.Text = dtInfo.Rows[0]["PJParticipacion1"].ToString().Trim();
                tbxPJParticipacion2.Text = dtInfo.Rows[0]["PJParticipacion2"].ToString().Trim();
                tbxPJParticipacion3.Text = dtInfo.Rows[0]["PJParticipacion3"].ToString().Trim();
                tbxPJParticipacion4.Text = dtInfo.Rows[0]["PJParticipacion4"].ToString().Trim();
                tbxPJParticipacion5.Text = dtInfo.Rows[0]["PJParticipacion5"].ToString().Trim();
                tbxPJSocMatriz.Text = dtInfo.Rows[0]["PJSocMatriz"].ToString().Trim();
                tbxPJSocMatrizIdenTrib.Text = dtInfo.Rows[0]["PJSocMatrizIdenTrib"].ToString().Trim();
                tbxPJSocMatrizJurisdiccion.Text = dtInfo.Rows[0]["PJSocMatrizJurisdiccion"].ToString().Trim();
                tbxPJSocMatrizDireccion.Text = dtInfo.Rows[0]["PJSocMatrizDireccion"].ToString().Trim();
                tbxPJSocMatrizCiudad.Text = dtInfo.Rows[0]["PJSocMatrizCiudad"].ToString().Trim();
                tbxPJSocMatrizTelefono.Text = dtInfo.Rows[0]["PJSocMatrizTelefono"].ToString().Trim();
                tbxPJLugarNmto.Text = dtInfo.Rows[0]["PJLugarNmto"].ToString().Trim();

                ddlTipoDocumentoPJempresa.SelectedValue = dtInfo.Rows[0]["PJTipoDocumentoEmpresa"].ToString().Trim();
                TXprimerApellidoPPE.Text = dtInfo.Rows[0]["PJpePrimerApellido"].ToString().Trim();
                TXsegundoApellidoPPE.Text = dtInfo.Rows[0]["PJpeSegundoApellido"].ToString().Trim();
                TXnombresPPE.Text = dtInfo.Rows[0]["PJpeNombres"].ToString().Trim();
                TXocipacionPPE.Text = dtInfo.Rows[0]["PJpeOcupacion"].ToString().Trim();
                TXcargoPPE.Text = dtInfo.Rows[0]["PJpeCargo"].ToString().Trim();
                /*if (dtInfo.Rows[0]["PJPregunta4"].ToString().Trim() == "SI")
                    TrVinculoPPExpuestaPJ.Visible = true;*/

                #region Carga DB
                ddlPJDpto.DataBind();
                ddlPJDpto2.DataBind();
                ddlPJCodCIIU2.DataBind();
                DDLPJcodCiiu.DataBind();
                #endregion

                #region Departamento y Ciudad 1
                if (dtInfo.Rows[0]["PJDpto"].ToString().Trim() != string.Empty)
                {
                    for (int i = 0; i < ddlPJDpto.Items.Count; i++)
                    {
                        ddlPJDpto.SelectedIndex = i;
                        if (ddlPJDpto.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PJDpto"].ToString().Trim())
                        {
                            ddlPJCiudad1.DataBind();
                            for (int ic = 0; ic < ddlPNCiudad1.Items.Count; ic++)
                            {
                                ddlPJCiudad1.SelectedIndex = ic;
                                if (ddlPJCiudad1.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PJCiudad1"].ToString().Trim())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            ddlPJDpto.SelectedIndex = 0;
                        }
                    }
                }
                #endregion
                #region Departamento y Ciudad 2
                if (dtInfo.Rows[0]["PJDpto2"].ToString().Trim() != string.Empty)
                {
                    for (int i = 0; i < ddlPJDpto2.Items.Count; i++)
                    {
                        ddlPJDpto2.SelectedIndex = i;
                        if (ddlPJDpto2.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PJDpto2"].ToString().Trim())
                        {
                            ddlPJCiudad2.DataBind();
                            for (int ic = 0; ic < ddlPNCiudad1.Items.Count; ic++)
                            {
                                ddlPJCiudad2.SelectedIndex = ic;
                                if (ddlPJCiudad2.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PJCiudad2"].ToString().Trim())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            ddlPJDpto2.SelectedIndex = 0;
                        }
                    }
                }
                #endregion                
                for (int i = 0; i < ddlPJCodCIIU2.Items.Count; i++) { ddlPJCodCIIU2.SelectedIndex = i; if (ddlPJCodCIIU2.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PJCodCIIU2"].ToString().Trim()) break; else ddlPJCodCIIU2.SelectedIndex = 0; }
                DDLPJcodCiiu.SelectedValue = ddlPJCodCIIU2.SelectedValue.ToString();
                for (int i = 0; i < ddlPJTipoDocumento.Items.Count; i++) { ddlPJTipoDocumento.SelectedIndex = i; if (ddlPJTipoDocumento.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocumento"].ToString().Trim()) break; else ddlPJTipoDocumento.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJTipoEmpresa.Items.Count; i++) { ddlPJTipoEmpresa.SelectedIndex = i; if (ddlPJTipoEmpresa.SelectedValue == dtInfo.Rows[0]["PJTipoEmpresa"].ToString().Trim()) break; else ddlPJTipoEmpresa.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJActividadEconomica.Items.Count; i++) { ddlPJActividadEconomica.SelectedIndex = i; if (ddlPJActividadEconomica.SelectedValue.ToString().Trim() == dtInfo.Rows[0]["PJActividadEconomica"].ToString().Trim()) break; else ddlPJActividadEconomica.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJTipoDocRepLegalPpal.Items.Count; i++) { ddlPJTipoDocRepLegalPpal.SelectedIndex = i; if (ddlPJTipoDocRepLegalPpal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegalPpal"].ToString().Trim()) break; else ddlPJTipoDocRepLegalPpal.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJTipoDocRepLegal1.Items.Count; i++) { ddlPJTipoDocRepLegal1.SelectedIndex = i; if (ddlPJTipoDocRepLegal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal1"].ToString().Trim()) break; else ddlPJTipoDocRepLegal1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJTipoDocRepLegal2.Items.Count; i++) { ddlPJTipoDocRepLegal2.SelectedIndex = i; if (ddlPJTipoDocRepLegal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal2"].ToString().Trim()) break; else ddlPJTipoDocRepLegal2.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJTipoDocRepLegal3.Items.Count; i++) { ddlPJTipoDocRepLegal3.SelectedIndex = i; if (ddlPJTipoDocRepLegal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal3"].ToString().Trim()) break; else ddlPJTipoDocRepLegal3.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJTipoDocRepLegal4.Items.Count; i++) { ddlPJTipoDocRepLegal4.SelectedIndex = i; if (ddlPJTipoDocRepLegal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal4"].ToString().Trim()) break; else ddlPJTipoDocRepLegal4.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta1.Items.Count; i++) { ddlPJPregunta1.SelectedIndex = i; if (ddlPJPregunta1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta1"].ToString().Trim()) break; else ddlPJPregunta1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta2.Items.Count; i++) { ddlPJPregunta2.SelectedIndex = i; if (ddlPJPregunta2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta2"].ToString().Trim()) break; else ddlPJPregunta2.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta3.Items.Count; i++) { ddlPJPregunta3.SelectedIndex = i; if (ddlPJPregunta3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta3"].ToString().Trim()) break; else ddlPJPregunta3.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta4.Items.Count; i++) { ddlPJPregunta4.SelectedIndex = i; if (ddlPJPregunta4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta4"].ToString().Trim()) break; else ddlPJPregunta4.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta5.Items.Count; i++) { ddlPJPregunta5.SelectedIndex = i; if (ddlPJPregunta5.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta5"].ToString().Trim()) break; else ddlPJPregunta5.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta6.Items.Count; i++) { ddlPJPregunta6.SelectedIndex = i; if (ddlPJPregunta6.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta6"].ToString().Trim()) break; else ddlPJPregunta6.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta7.Items.Count; i++) { ddlPJPregunta7.SelectedIndex = i; if (ddlPJPregunta7.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta7"].ToString().Trim()) break; else ddlPJPregunta7.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta8.Items.Count; i++) { ddlPJPregunta8.SelectedIndex = i; if (ddlPJPregunta8.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta8"].ToString().Trim()) break; else ddlPJPregunta8.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta9.Items.Count; i++) { ddlPJPregunta9.SelectedIndex = i; if (ddlPJPregunta9.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta9"].ToString().Trim()) break; else ddlPJPregunta9.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPregunta10.Items.Count; i++) { ddlPJPregunta10.SelectedIndex = i; if (ddlPJPregunta10.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta10"].ToString().Trim()) break; else ddlPJPregunta10.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep1Legal1.Items.Count; i++) { ddlPJPreguntaRep1Legal1.SelectedIndex = i; if (ddlPJPreguntaRep1Legal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep1Legal1"].ToString().Trim()) break; else ddlPJPreguntaRep1Legal1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep1Legal2.Items.Count; i++) { ddlPJPreguntaRep1Legal2.SelectedIndex = i; if (ddlPJPreguntaRep1Legal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep1Legal2"].ToString().Trim()) break; else ddlPJPreguntaRep1Legal2.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep1Legal3.Items.Count; i++) { ddlPJPreguntaRep1Legal3.SelectedIndex = i; if (ddlPJPreguntaRep1Legal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep1Legal3"].ToString().Trim()) break; else ddlPJPreguntaRep1Legal3.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep1Legal4.Items.Count; i++) { ddlPJPreguntaRep1Legal4.SelectedIndex = i; if (ddlPJPreguntaRep1Legal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep1Legal4"].ToString().Trim()) break; else ddlPJPreguntaRep1Legal4.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep2Legal1.Items.Count; i++) { ddlPJPreguntaRep2Legal1.SelectedIndex = i; if (ddlPJPreguntaRep2Legal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep2Legal1"].ToString().Trim()) break; else ddlPJPreguntaRep2Legal1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep2Legal2.Items.Count; i++) { ddlPJPreguntaRep2Legal2.SelectedIndex = i; if (ddlPJPreguntaRep2Legal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep2Legal2"].ToString().Trim()) break; else ddlPJPreguntaRep2Legal2.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep2Legal3.Items.Count; i++) { ddlPJPreguntaRep2Legal3.SelectedIndex = i; if (ddlPJPreguntaRep2Legal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep2Legal3"].ToString().Trim()) break; else ddlPJPreguntaRep2Legal3.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep2Legal4.Items.Count; i++) { ddlPJPreguntaRep2Legal4.SelectedIndex = i; if (ddlPJPreguntaRep2Legal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep2Legal4"].ToString().Trim()) break; else ddlPJPreguntaRep2Legal4.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep3Legal1.Items.Count; i++) { ddlPJPreguntaRep3Legal1.SelectedIndex = i; if (ddlPJPreguntaRep3Legal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep3Legal1"].ToString().Trim()) break; else ddlPJPreguntaRep3Legal1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep3Legal2.Items.Count; i++) { ddlPJPreguntaRep3Legal2.SelectedIndex = i; if (ddlPJPreguntaRep3Legal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep3Legal2"].ToString().Trim()) break; else ddlPJPreguntaRep3Legal2.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep3Legal3.Items.Count; i++) { ddlPJPreguntaRep3Legal3.SelectedIndex = i; if (ddlPJPreguntaRep3Legal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep3Legal3"].ToString().Trim()) break; else ddlPJPreguntaRep3Legal3.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep3Legal4.Items.Count; i++) { ddlPJPreguntaRep3Legal4.SelectedIndex = i; if (ddlPJPreguntaRep3Legal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep3Legal4"].ToString().Trim()) break; else ddlPJPreguntaRep3Legal4.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep4Legal1.Items.Count; i++) { ddlPJPreguntaRep4Legal1.SelectedIndex = i; if (ddlPJPreguntaRep4Legal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep4Legal1"].ToString().Trim()) break; else ddlPJPreguntaRep4Legal1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep4Legal2.Items.Count; i++) { ddlPJPreguntaRep4Legal2.SelectedIndex = i; if (ddlPJPreguntaRep4Legal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep4Legal2"].ToString().Trim()) break; else ddlPJPreguntaRep4Legal2.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep4Legal3.Items.Count; i++) { ddlPJPreguntaRep4Legal3.SelectedIndex = i; if (ddlPJPreguntaRep4Legal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep4Legal3"].ToString().Trim()) break; else ddlPJPreguntaRep4Legal3.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRep4Legal4.Items.Count; i++) { ddlPJPreguntaRep4Legal4.SelectedIndex = i; if (ddlPJPreguntaRep4Legal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep4Legal4"].ToString().Trim()) break; else ddlPJPreguntaRep4Legal4.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRepPpalLegal1.Items.Count; i++) { ddlPJPreguntaRepPpalLegal1.SelectedIndex = i; if (ddlPJPreguntaRepPpalLegal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRepPpalLegal1"].ToString().Trim()) break; else ddlPJPreguntaRepPpalLegal1.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRepPpalLegal2.Items.Count; i++) { ddlPJPreguntaRepPpalLegal2.SelectedIndex = i; if (ddlPJPreguntaRepPpalLegal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRepPpalLegal2"].ToString().Trim()) break; else ddlPJPreguntaRepPpalLegal2.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRepPpalLegal3.Items.Count; i++) { ddlPJPreguntaRepPpalLegal3.SelectedIndex = i; if (ddlPJPreguntaRepPpalLegal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRepPpalLegal3"].ToString().Trim()) break; else ddlPJPreguntaRepPpalLegal3.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJPreguntaRepPpalLegal4.Items.Count; i++) { ddlPJPreguntaRepPpalLegal4.SelectedIndex = i; if (ddlPJPreguntaRepPpalLegal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRepPpalLegal4"].ToString().Trim()) break; else ddlPJPreguntaRepPpalLegal4.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJCotizaBolsa.Items.Count; i++) { ddlPJCotizaBolsa.SelectedIndex = i; if (ddlPJCotizaBolsa.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJCotizaBolsa"].ToString().Trim()) break; else ddlPJCotizaBolsa.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJEstatal.Items.Count; i++) { ddlPJEstatal.SelectedIndex = i; if (ddlPJEstatal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJEstatal"].ToString().Trim()) break; else ddlPJEstatal.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJSinAnimoLucro.Items.Count; i++) { ddlPJSinAnimoLucro.SelectedIndex = i; if (ddlPJSinAnimoLucro.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJSinAnimoLucro"].ToString().Trim()) break; else ddlPJSinAnimoLucro.SelectedIndex = 0; }
                for (int i = 0; i < ddlPJSubsidiaria.Items.Count; i++) { ddlPJSubsidiaria.SelectedIndex = i; if (ddlPJSubsidiaria.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJSubsidiaria"].ToString().Trim()) break; else ddlPJSubsidiaria.SelectedIndex = 0; }
                #endregion
            }

            #region Financiera
            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                tbxOtroTipoTransaccion.Text = dtInfo.Rows[0]["OtroTipoTransaccion"].ToString().Trim();
                tbxPFTipoProducto1.Text = dtInfo.Rows[0]["PFTipoProducto1"].ToString().Trim();
                tbxPFNumeroProducto1.Text = dtInfo.Rows[0]["PFNumeroProducto1"].ToString().Trim();
                tbxPFEntidad1.Text = dtInfo.Rows[0]["PFEntidad1"].ToString().Trim();
                tbxPFMonto1.Text = dtInfo.Rows[0]["PFMonto1"].ToString().Trim();
                tbxPFCiudad1.Text = dtInfo.Rows[0]["PFCiudad1"].ToString().Trim();
                tbxPFPais1.Text = dtInfo.Rows[0]["PFPais1"].ToString().Trim();
                tbxPFMoneda1.Text = dtInfo.Rows[0]["PFMoneda1"].ToString().Trim();
                tbxPFTipoProducto2.Text = dtInfo.Rows[0]["PFTipoProducto2"].ToString().Trim();
                tbxPFNumeroProducto2.Text = dtInfo.Rows[0]["PFNumeroProducto2"].ToString().Trim();
                tbxPFEntidad2.Text = dtInfo.Rows[0]["PFEntidad2"].ToString().Trim();
                tbxPFMonto2.Text = dtInfo.Rows[0]["PFMonto2"].ToString().Trim();
                tbxPFCiudad2.Text = dtInfo.Rows[0]["PFCiudad2"].ToString().Trim();
                tbxPFPais2.Text = dtInfo.Rows[0]["PFPais2"].ToString().Trim();
                tbxPFMoneda2.Text = dtInfo.Rows[0]["PFMoneda2"].ToString().Trim();
                tbxPFTipoProducto3.Text = dtInfo.Rows[0]["PFTipoProducto3"].ToString().Trim();
                tbxPFNumeroProducto3.Text = dtInfo.Rows[0]["PFNumeroProducto3"].ToString().Trim();
                tbxPFEntidad3.Text = dtInfo.Rows[0]["PFEntidad3"].ToString().Trim();
                tbxPFMonto3.Text = dtInfo.Rows[0]["PFMonto3"].ToString().Trim();
                tbxPFCiudad3.Text = dtInfo.Rows[0]["PFCiudad3"].ToString().Trim();
                tbxPFPais3.Text = dtInfo.Rows[0]["PFPais3"].ToString().Trim();
                tbxPFMoneda3.Text = dtInfo.Rows[0]["PFMoneda3"].ToString().Trim();
                for (int i = 0; i < ddlTransacMonedaExtra.Items.Count; i++) { ddlTransacMonedaExtra.SelectedIndex = i; if (ddlTransacMonedaExtra.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim()) break; else ddlTransacMonedaExtra.SelectedIndex = 0; }
                for (int i = 0; i < ddlTipoTransaccion.Items.Count; i++) { ddlTipoTransaccion.SelectedIndex = i; if (ddlTipoTransaccion.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TipoTransaccion"].ToString().Trim()) break; else ddlTipoTransaccion.SelectedIndex = 0; }
                for (int i = 0; i < ddlPFCtaMonedaExtra.Items.Count; i++) { ddlPFCtaMonedaExtra.SelectedIndex = i; if (ddlPFCtaMonedaExtra.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PFCtaMonedaExtra"].ToString().Trim()) break; else ddlPFCtaMonedaExtra.SelectedIndex = 0; }
                for (int i = 0; i < ddlPFProdExterior.Items.Count; i++) { ddlPFProdExterior.SelectedIndex = i; if (ddlPFProdExterior.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PFProdExterior"].ToString().Trim()) break; else ddlPFProdExterior.SelectedIndex = 0; }

                mtdHabilitaMoneda(dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim().Equals("SI"));
            }
            #endregion

            #region Seguros
            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                tbxSeguroAno1.Text = dtInfo.Rows[0]["SeguroAno1"].ToString().Trim();
                tbxSeguroRamo1.Text = dtInfo.Rows[0]["SeguroRamo1"].ToString().Trim();
                tbxSeguroCompania1.Text = dtInfo.Rows[0]["SeguroCompania1"].ToString().Trim();
                tbxSeguroValor1.Text = dtInfo.Rows[0]["SeguroValor1"].ToString().Trim();
                tbxSeguroAno2.Text = dtInfo.Rows[0]["SeguroAno2"].ToString().Trim();
                tbxSeguroRamo2.Text = dtInfo.Rows[0]["SeguroRamo2"].ToString().Trim();
                tbxSeguroCompania2.Text = dtInfo.Rows[0]["SeguroCompania2"].ToString().Trim();
                tbxSeguroValor2.Text = dtInfo.Rows[0]["SeguroValor2"].ToString().Trim();
                tbxOrigenFondos.Text = dtInfo.Rows[0]["OrigenFondos"].ToString().Trim();
                for (int i = 0; i < ddlSeguroTipo1.Items.Count; i++) { ddlSeguroTipo1.SelectedIndex = i; if (ddlSeguroTipo1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["SeguroTipo1"].ToString().Trim()) break; else ddlSeguroTipo1.SelectedIndex = 0; }
                for (int i = 0; i < ddlSeguroTipo2.Items.Count; i++) { ddlSeguroTipo2.SelectedIndex = i; if (ddlSeguroTipo2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["SeguroTipo2"].ToString().Trim()) break; else ddlSeguroTipo2.SelectedIndex = 0; }
                for (int i = 0; i < ddlReclamaciones.Items.Count; i++) { ddlReclamaciones.SelectedIndex = i; if (ddlReclamaciones.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Reclamaciones"].ToString().Trim()) break; else ddlReclamaciones.SelectedIndex = 0; }

                mtdHabilitaReclamos(dtInfo.Rows[0]["Reclamaciones"].ToString().Trim().Equals("SI"));
            }
            #endregion

            #region Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                tbxLugarEntrevista.Text = dtInfo.Rows[0]["LugarEntrevista"].ToString().Trim();
                tbxFechaEntrevista.Text = dtInfo.Rows[0]["FechaEntrevista"].ToString().Trim();
                tbxHoraEntrevista.Text = dtInfo.Rows[0]["HoraEntrevista"].ToString().Trim();
                tbxObservaciones1.Text = dtInfo.Rows[0]["Observaciones1"].ToString().Trim();
                tbxNombreResponsable.Text = dtInfo.Rows[0]["NombreResponsable"].ToString().Trim();
                tbxFechaVerificacion.Text = dtInfo.Rows[0]["FechaVerificacion"].ToString().Trim();
                tbxHoraVerificacion.Text = dtInfo.Rows[0]["HoraVerificacion"].ToString().Trim();
                tbxNombreVerifica.Text = dtInfo.Rows[0]["NombreVerifica"].ToString().Trim();
                tbxObservaciones2.Text = dtInfo.Rows[0]["Observaciones2"].ToString().Trim();
                tbxNombreIntermediario.Text = dtInfo.Rows[0]["NombreIntermediario"].ToString().Trim();
                for (int i = 0; i < ddlResultado.Items.Count; i++) { ddlResultado.SelectedIndex = i; if (ddlResultado.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Resultado"].ToString().Trim()) break; else ddlResultado.SelectedIndex = 0; }
            }
            #endregion

            tbConsulta.Visible = false;
            tbFormulario.Visible = true;
            tbDocumentos.Visible = true;
            if(ddlPNPregunta4.SelectedValue.ToString() == "SI")
            TrVinculoPPExpuestaPN.Visible = true;
        }

        private void loadGridArchivos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivos = grid;
            GridView2.DataSource = InfoGridArchivos;
            GridView2.DataBind();
        }

        private void loadInfoArchivos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cKnowClient.loadInfoGridArchivos(IdConocimientoCliente.ToString());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                        });
                }
                GridView2.DataSource = InfoGridArchivos;
                GridView2.DataBind();
            }
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            //grid.Columns.Add("IdConocimientoCliente", typeof(string));
            //grid.Columns.Add("PrimerApellido", typeof(string));
            //grid.Columns.Add("SegundoApellido", typeof(string));
            //grid.Columns.Add("Nombres", typeof(string));
            //grid.Columns.Add("TipoDocumento", typeof(string));
            //grid.Columns.Add("NumeroDocumento", typeof(string));
            //grid.Columns.Add("Ano", typeof(string));
            //grid.Columns.Add("FechaRegistro", typeof(string));
            //grid.Columns.Add("Estado", typeof(string));
            //grid.Columns.Add("Usuario", typeof(string));
            //grid.Columns.Add("FechaAprobado", typeof(string));
            //grid.Columns.Add("RazonDenominacion", typeof(string));
            //grid.Columns.Add("NIT", typeof(string));

            grid.Columns.Add("Id", typeof(string));
            grid.Columns.Add("IdTipoPersona", typeof(string));
            grid.Columns.Add("TipoPersona", typeof(string));
            grid.Columns.Add("TipoDocumento", typeof(string));
            grid.Columns.Add("Documento", typeof(string));
            grid.Columns.Add("nombreCliente", typeof(string));
            grid.Columns.Add("IdEstado", typeof(string));
            grid.Columns.Add("estadoFormulario", typeof(string));
            grid.Columns.Add("fechaActualizacion", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void loadGridFCC()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Id", typeof(string));
            grid.Columns.Add("IdTipoPersona", typeof(string));
            grid.Columns.Add("TipoPersona", typeof(string));
            grid.Columns.Add("TipoDocumento", typeof(string));
            grid.Columns.Add("Documento", typeof(string));
            grid.Columns.Add("nombreCliente", typeof(string));
            grid.Columns.Add("IdEstado", typeof(string));
            grid.Columns.Add("estadoFormulario", typeof(string));
            grid.Columns.Add("fechaActualizacion", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGridFCC = grid;
        }



        private void imprimirFormulario()
        {
            string strPage = string.Empty, strIdForm = string.Empty;

            if (DropDownList39.SelectedValue.ToString() == "0")
                strIdForm = "0";
            else
                strIdForm = "1";

            strPage = string.Format("window.open('ReporteKnowClienteWillis.aspx?IdConocimientoCliente={0}" +
                "&IdTipoForm={1}','Reporte','width=950px,height=900px,scrollbars=yes,resizable=yes')", IdConocimientoCliente, strIdForm);

            Response.Write("<script languaje=javascript>" + strPage + "</script>");
        }

        private void updateClienteForm()
        {
            IdConocimientoCliente = cKnowClient.agregarConocimientoCliente(
                string.Format("{0:yyyy MM dd}", DateTime.Now).Replace(" ", ""), String.Format("{0:yyyy}", DateTime.Now),
                ddlSucursal.Text.Trim(), ddlTipoFormulario.SelectedValue.ToString());

            cKnowClient.InfoFormCliente(IdConocimientoCliente,
                Sanitizer.GetSafeHtmlFragment(tbxFechaFormulario.Text.Trim()), ddlClaseVinculacion.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtraClaseVinculacion.Text.Trim()), ddlTomadorAsegurado.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtraTomadorAsegurado.Text.Trim()),
                ddlTomadorBeneficiario.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtraTomadorBeneficiario.Text.Trim()), ddlAseguradoBeneficiario.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtraAseguradoBeneficiario.Text.Trim()), ddlTipoCliente.SelectedValue.ToString().Trim(),
                tbxCiudad.Text.Trim(), ddlSucursal.SelectedValue.ToString().Trim(), ddlTipoSolicitud.SelectedValue.ToString().Trim());

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA NATURAL")
                cKnowClient.InfoFormPN(
                    IdConocimientoCliente, Sanitizer.GetSafeHtmlFragment(tbxPNPrimerApellido.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNSegunApellido.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNNombres.Text.Trim()), ddlPNTipoDocumento.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPNNumeroDocumento.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(tbxPNFechaExpedicion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNLugar.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNFechaNacimiento.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNNacionalidad.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNOcupacionOficio.Text.Trim()), string.Empty, ddlPNActividadEconomica.Text.Trim(),
                    ddlPNCIIUDescripcion.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPNEmpresaTrabajo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNCargo.Text.Trim()), ddlPNCiudad1.SelectedItem.Text.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPNTelefono1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNDireccionResidencia.Text.Trim()), ddlPNCiudad2.SelectedItem.Text.ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(tbxPNTelefono2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNCelular.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNIngresosMensuales.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNActivos.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNEgresoMensuales.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNPasivos.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNOtrosIngresos.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNConceptoOtrosIngresos.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(tbxPNCorreoElectronico.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNLugarNmto.Text.Trim()), ddlPNDpto.SelectedItem.Text.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPNOtraActEconomica.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNPatrimonio.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNDireccionEmpresa.Text.Trim()),
                    ddlPNCiudadEmpresa.SelectedItem.Text.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPNTelefonoEmpresa.Text.Trim()), string.Empty,
                    ddlPNPregunta1.SelectedValue.ToString().Trim(), ddlPNPregunta2.SelectedValue.ToString().Trim(), ddlPNPregunta3.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPNEspecificacionPreguntas.Text.Trim()), ddlPNPregunta4.SelectedValue.ToString().Trim(),
                    ddlPNPregunta5.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPNEspecificacionPreguntas2.Text.Trim()), ddlPNSector1.SelectedValue.ToString().Trim(), ddlPNSector2.SelectedValue.ToString().Trim(), ddlPNDptoEmpresa.SelectedItem.Text.ToString().Trim(),
                    ddlPNDpto2.SelectedItem.Text.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPNDireccion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPNServicio.Text.Trim()), ddlPNCIIUDescripcion.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(ddlTipoActividadPN.SelectedValue.ToString().Trim()),
                    Sanitizer.GetSafeHtmlFragment(TXvalorOtroTipoActividadPN.Text), Sanitizer.GetSafeHtmlFragment(TXprimerApellidoPNPPE.Text), Sanitizer.GetSafeHtmlFragment(TXsegundApellidoPNPPE.Text), Sanitizer.GetSafeHtmlFragment(TXnombrePNPPE.Text), Sanitizer.GetSafeHtmlFragment(TXocupacionPNPPE.Text), Sanitizer.GetSafeHtmlFragment(TXcargoPNPPE.Text));

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA JURÍDICA")
                cKnowClient.InfoFormPJ(IdConocimientoCliente,
                    Sanitizer.GetSafeHtmlFragment(tbxPJRazonDenominacion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNIT.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJPrimerApellido.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJSegundoApellido.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNombres.Text.Trim()), ddlPJTipoDocumento.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJNumeroDocumento.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJLugarExpedicion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJFechaExpedicion.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(tbxPJDireccionOficina.Text.Trim()), ddlPJCiudad1.SelectedItem.Text.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJTelefono1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJDireccionSucursal.Text.Trim()), ddlPJCiudad2.SelectedItem.Text.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJTelefono2.Text.Trim()), ddlPJTipoEmpresa.SelectedValue.ToString().Trim(), ddlPJActividadEconomica.SelectedValue.ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(tbxPJCIIU.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJIngresosMensuales.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJActivos.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJEgresoMensuales.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJPasivos.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJOtrosIngresos.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJConceptoOtrosIngresos.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJCorreoPrincipal.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJFechaConstitucion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNombresRepLegalPpal.Text.Trim()),
                    ddlPJTipoDocRepLegalPpal.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJDocumentoRepLegalPpal.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNombresRepLegal1.Text.Trim()), ddlPJTipoDocRepLegal1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJDocumentoRepLegal1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNombresRepLegal2.Text.Trim()), ddlPJTipoDocRepLegal2.SelectedValue.ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(tbxPJDocumentoRepLegal2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNombresRepLegal3.Text.Trim()), ddlPJTipoDocRepLegal3.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJDocumentoRepLegal3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNombresRepLegal4.Text.Trim()), ddlPJTipoDocRepLegal4.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJDocumentoRepLegal4.Text.Trim()), ddlPJCodCIIU2.SelectedItem.Text.ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(tbxPJNacionalidad1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJDV.Text.Trim()), ddlPJDpto.SelectedItem.Text.ToString().Trim(), ddlPJDpto2.SelectedItem.Text.ToString().Trim(),
                    ddlPJPregunta1.SelectedValue.ToString().Trim(), ddlPJPregunta2.SelectedValue.ToString().Trim(), ddlPJPregunta3.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJEspecificacionPreguntas.Text.Trim()), ddlPJPregunta4.SelectedValue.ToString().Trim(),
                    ddlPJPregunta5.SelectedValue.ToString().Trim(), ddlPJPregunta6.SelectedValue.ToString().Trim(), ddlPJPregunta7.SelectedValue.ToString().Trim(), ddlPJPregunta8.SelectedValue.ToString().Trim(), ddlPJPregunta9.SelectedValue.ToString().Trim(), ddlPJPregunta10.SelectedValue.ToString().Trim(),
                    ddlPJPreguntaRep1Legal1.SelectedValue.ToString().Trim(), ddlPJPreguntaRep1Legal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRep1Legal3.SelectedValue.ToString().Trim(),
                    ddlPJPreguntaRep1Legal4.SelectedValue.ToString().Trim(), ddlPJPreguntaRep2Legal1.SelectedValue.ToString().Trim(), ddlPJPreguntaRep2Legal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRep2Legal3.SelectedValue.ToString().Trim(), ddlPJPreguntaRep2Legal4.SelectedValue.ToString().Trim(), ddlPJPreguntaRep3Legal1.SelectedValue.ToString().Trim(),
                    ddlPJPreguntaRep3Legal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRep3Legal3.SelectedValue.ToString().Trim(), ddlPJPreguntaRep3Legal4.SelectedValue.ToString().Trim(), ddlPJPreguntaRep4Legal1.SelectedValue.ToString().Trim(), ddlPJPreguntaRep4Legal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRep4Legal3.SelectedValue.ToString().Trim(),
                    ddlPJPreguntaRep4Legal4.SelectedValue.ToString().Trim(), ddlPJPreguntaRepPpalLegal1.SelectedValue.ToString().Trim(), ddlPJPreguntaRepPpalLegal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRepPpalLegal3.SelectedValue.ToString().Trim(), ddlPJPreguntaRepPpalLegal4.SelectedValue.ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(tbxPJNIT1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJPais1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNIT2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJPais2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNIT3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJPais3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJNIT4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJPais4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJDireccionFiscal1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJDireccionFiscal2.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(tbxPJParticipacion1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJParticipacion2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJParticipacion3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJParticipacion4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJParticipacion5.Text.Trim()),
                    ddlPJCotizaBolsa.SelectedValue.ToString().Trim(), ddlPJEstatal.SelectedValue.ToString().Trim(), ddlPJSinAnimoLucro.SelectedValue.ToString().Trim(), ddlPJSubsidiaria.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxPJSocMatriz.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJSocMatrizIdenTrib.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJSocMatrizJurisdiccion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJSocMatrizDireccion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJSocMatrizCiudad.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPJSocMatrizTelefono.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(tbxPJLugarNmto.Text.Trim()), Sanitizer.GetSafeHtmlFragment(ddlTipoDocumentoPJempresa.SelectedValue.ToString()), Sanitizer.GetSafeHtmlFragment(TXprimerApellidoPPE.Text), Sanitizer.GetSafeHtmlFragment(TXsegundoApellidoPPE.Text), Sanitizer.GetSafeHtmlFragment(TXnombresPPE.Text), Sanitizer.GetSafeHtmlFragment(TXocipacionPPE.Text), Sanitizer.GetSafeHtmlFragment(TXcargoPPE.Text)
                    );
            cKnowClient.InfoFormPF(
                IdConocimientoCliente, ddlTransacMonedaExtra.SelectedValue.ToString().Trim(), ddlTipoTransaccion.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtroTipoTransaccion.Text.Trim()), ddlPFProdExterior.SelectedValue.ToString().Trim(),
                Sanitizer.GetSafeHtmlFragment(tbxPFTipoProducto1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFNumeroProducto1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFEntidad1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMonto1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFCiudad1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFPais1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMoneda1.Text.Trim()),
                Sanitizer.GetSafeHtmlFragment(tbxPFTipoProducto2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFNumeroProducto2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFEntidad2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMonto2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFCiudad2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFPais2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMoneda2.Text.Trim()),
                Sanitizer.GetSafeHtmlFragment(tbxPFTipoProducto3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFNumeroProducto3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFEntidad3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMonto3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFCiudad3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFPais3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMoneda3.Text.Trim()),
                ddlPFCtaMonedaExtra.SelectedValue.ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            cKnowClient.InfoFormSeguros(IdConocimientoCliente,
                Sanitizer.GetSafeHtmlFragment(tbxSeguroAno1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroRamo1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroCompania1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroValor1.Text.Trim()), ddlSeguroTipo1.SelectedValue.ToString().Trim(),
                Sanitizer.GetSafeHtmlFragment(tbxSeguroAno2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroRamo2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroCompania2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroValor2.Text.Trim()), ddlSeguroTipo2.SelectedValue.ToString().Trim(),
                Sanitizer.GetSafeHtmlFragment(tbxOrigenFondos.Text.Trim()), ddlReclamaciones.SelectedValue.ToString().Trim());

            cKnowClient.InfoFormEntrevista(IdConocimientoCliente,
               Sanitizer.GetSafeHtmlFragment(tbxLugarEntrevista.Text), Sanitizer.GetSafeHtmlFragment(tbxFechaEntrevista.Text), Sanitizer.GetSafeHtmlFragment(tbxHoraEntrevista.Text), ddlResultado.Text, Sanitizer.GetSafeHtmlFragment(tbxObservaciones1.Text),
                Sanitizer.GetSafeHtmlFragment(tbxNombreResponsable.Text), Sanitizer.GetSafeHtmlFragment(tbxFechaVerificacion.Text), Sanitizer.GetSafeHtmlFragment(tbxHoraVerificacion.Text), Sanitizer.GetSafeHtmlFragment(tbxNombreVerifica.Text), Sanitizer.GetSafeHtmlFragment(tbxObservaciones2.Text),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Sanitizer.GetSafeHtmlFragment(tbxNombreIntermediario.Text)
            );

        }

        #region PDFs

        private void mtdCargarPdfFormCliente()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cKnowClient.loadCodigoInfoFormArchivo();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    IdConocimientoCliente.ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}",
                    IdConocimientoCliente.ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cKnowClient.mtdAgregarArchivoPdf(IdConocimientoCliente.ToString().Trim(),
                strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfFormCliente()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivos.Rows[RowGridArchivos]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cKnowClient.mtdDescargarArchivoPdf(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }

        #endregion PDFs

        private void mtdHabilitarTipoFormulario(int intTipoFormulario, bool booVisible)
        {
            switch (intTipoFormulario)
            {
                case 0:
                    infoTituloPN.Visible = booVisible;
                    infoPN.Visible = booVisible;
                    infoTituloPJ.Visible = booVisible;
                    infoPJ.Visible = booVisible;
                    infoDeclaracionFondosPJ.Visible = booVisible;
                    lblTipoCliente.Visible = booVisible;
                    ddlTipoCliente.Visible = booVisible;
                    ddlTipoCliente.ValidationGroup = "";

                    #region encabezado
                    lblTituloFormulario.Text = "";
                    lblSubTituloFormulario.Text = "";
                    #endregion
                    #region InfoClaseVinculacionNota
                    lblCVN.Text = "";
                    #endregion
                    #region InfoNota2
                    lblN2.Text = "";
                    #endregion
                    #region infoDeclaracionFondos
                    lblDF_1.Text = "";
                    lblDF_2.Text = "";
                    lblDF_3.Text = "";
                    lblDF_4.Text = "";
                    lblDF_5.Text = "";
                    lblDF_6.Text = "";
                    #endregion
                    #region infoPF1
                    lblPF_1.Text = "";
                    lblPF_2.Text = "";
                    lblPF_3.Text = "";
                    lblPF_4.Text = "";
                    #endregion
                    #region infoAutorizacion
                    lblAuto_1.Text = "";
                    lblAuto_2.Text = "";
                    lblAuto_3.Text = "";
                    lblAuto_4.Text = "";
                    lblAuto_5.Text = "";
                    #endregion
                    #region infoClausula
                    lblClau_1.Text = "";
                    lblClau_2.Text = "";
                    lblClau_3.Text = "";
                    lblClau_4.Text = "";
                    lblClau_5.Text = "";
                    lblClau_6.Text = "";
                    lblClau_7.Text = "";
                    lblClau_8.Text = "";
                    lblClau_9.Text = "";
                    lblClau_10.Text = "";
                    lblClau_11.Text = "";
                    lblClau_12.Text = "";
                    lblClau_13.Text = "";
                    lblClau_14.Text = "";
                    lblClau_15.Text = "";
                    lblClau_16.Text = "";
                    lblClau_17.Text = "";
                    lblClau_18.Text = "";
                    lblClau_19.Text = "";
                    lblClau_20.Text = "";
                    lblClau_21.Text = "";
                    lblClau_22.Text = "";
                    lblClau_23.Text = "";
                    lblClau_24.Text = "";
                    lblClau_25.Text = "";
                    lblClau_26.Text = "";
                    lblClau_27.Text = "";
                    #endregion

                    break;
                case 1: //Natural
                    infoTituloPN.Visible = booVisible;
                    infoPN.Visible = booVisible;
                    infoTituloPJ.Visible = !booVisible;
                    infoPJ.Visible = !booVisible;
                    infoDeclaracionFondosPJ.Visible = !booVisible;
                    lblTipoCliente.Visible = !booVisible;
                    ddlTipoCliente.Visible = !booVisible;
                    ddlTipoCliente.ValidationGroup = "";
                    rfvTipoCliente.Enabled = !booVisible;

                    #region Encabezado
                    lblTituloFormulario.Text = "";
                    lblSubTituloFormulario.Text = "";
                    #endregion
                    #region InfoClaseVinculacionNota
                    lblCVN.Text = "EN EL EVENTO EN EL QUE EL POTENCIAL CLIENTE NO CUENTE CON LA INFORMAICIÓN SOLICITADA EN ESTE FORMULARIO, DEBERÁ CONSIGNAR DICHA CIRCUNSTACIA EN EL ESPACIO CORRESPONDIENTE";
                    #endregion
                    #region InfoNota2
                    lblN2.Text = "INDIQUE LOS VINCULOS EXISTENTES ENTRE EL TOMADOR, ASEGURADO, AFIANZADO Y BENEFICIARIO: (INDIVIDUALIZACION DEL PRODUCTO)";
                    #endregion
                    #region infoDeclaracionFondos
                    lblDF_1.Text = "Declaro expresamente que";
                    lblDF_2.Text = "1.La actividad, profesión u oficio es lícita y la ejerzo dentro del marco legal y los recursos que poseo no provienen de actividades ilícitas delas contempladasen el Código Penal Colombiano.";
                    lblDF_3.Text = "2.La información que he suministrado en la solicitud y en este documento es veraz y verificable y me comprometo a actualizarlaanualmente.";
                    lblDF_4.Text = "3.Los recursos que se deriven del desarrollo de este contrato no se destinaran a la financiación del terrorismo, grupos terroristas oactividades terroristas.";
                    lblDF_5.Text = "4.Los recursos que poseo provienen de la(s) siguiente(s) fuente(s) (Detalle ocupación, oficio, actividad o negocios)";

                    #endregion
                    #region infoPF1
                    lblPF_1.Text = "";
                    lblPF_2.Text = "";
                    lblPF_3.Text = "";
                    lblPF_4.Text = "";
                    #endregion
                    #region infoAutorizacion
                    lblAuto_1.Text = "DOCUMENTOS MINIMOS REQUERIDOS:";
                    lblAuto_2.Text = "Se debe adjuntar la siguiente documentación:";
                    lblAuto_3.Text = "1.Constancia de Ingresos (Honorarios laborales, Certificados de ingresos y Retenciones o el documento que corresponda) 2. Inventariogeneral de los bienes objeto del seguro salvo cuando se trate de pólizas flotantes o automáticas 3. Fotocopia del documento de identificación ampliada al 150% 4. Declaración de Renta del último período gravable disponible. (Si declara). 5 en caso de ser Apoderado, adjuntar Poder. La entidad aseguradora podrá requerir información adicional que considere relevante y necesaria para controlar el riesgo de LA/FT.";
                    lblAuto_4.Text = "";
                    lblAuto_5.Text = "";

                    #endregion
                    #region infoClausula
                    lblClau_1.Text = "CONSIDERACIONES:";
                    lblClau_2.Text = "1.Que los datos personales solicitados en el presente formulario de conocimiento del cliente son recogidos atendiendo las disposiciones einstrucciones de la Superintendencia Financiera de Colombia y los estándares internacionales para prevenir y controlar el lavado de activos y la financiación del terrorismo";
                    lblClau_3.Text = "2.Que conforme con lo dispuesto por el literal b) del artículo 2 de la ley 1581 de 2012, las disposiciones que buscan la protección de datospersonales y que se encuentran contenidos en dicha disposición, no son aplicables a las bases de datos y archivos que tengan por finalidad la prevención, detección, monitoreo y control del lavado de activos y el financiamiento del terrorismo, por lo que en principio su utilización no requeriría de una autorización de su titular, la cual proviene de la ley.";
                    lblClau_4.Text = "3.Que los datos personales adicionales para el estudio técnico del riesgo asegurable y reasegurable se tratarán observado las leyes 1581 de2012 y 1266 de 2008 según el caso.";
                    lblClau_5.Text = "4.Que los datos también serán tratados para fines comerciales, razón por la cual procedo a emitir la siguiente:";
                    lblClau_6.Text = "";
                    lblClau_7.Text = "y/o cualquier sociedad controlada, directa o indirectamente, por la misma sociedad(es) matriz de la(s) sociedad(es) referenciadas y con la(s) que se suscriba(n) contrato(s) de seguros.";
                    lblClau_8.Text = "Así mismo entiéndase como INTERMEDIARIO de SEGUROS La(s) sociedad(es) WILLIS COLOMBIA CORREDORES DE SEGUROS S.A, y/o cualquier sociedad controlada, directa o indirectamente, por la misma sociedad matriz de la(s) sociedad(es) antes mencionada(s)";
                    lblClau_9.Text = "Dirección Avenida Calle 26 No. 59-41 Piso 6; Bogotá, Teléfono +57 (1) 606 7575";
                    lblClau_10.Text = "I. Que para efectos de acceder a la prestación de servicios por parte de LA ASEGURADORA Y/O EL INTERMIDIARIO DE SEGUROS, suministro mis datos para todos los fines precontractuales y contractuales que comprende la actividad aseguradora.";
                    lblClau_11.Text = "II. Que LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS me han informado, de manera expresa:";
                    lblClau_12.Text = "1.FINALIDAD DEL TRATAMIENTO DE DATOS PERSONALES: Nuestros datos serán tratados por LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS, para las siguientes finalidades: i) El trámite de nuestra solicitud de vinculación como consumidor financiero, deudor, contraparte contractual y/o proveedor ii) El proceso de negociación de contratos con LA ASEGURADORA, incluyendo la determinación de primas y la selección de riesgos. iii)La ejecución y el cumplimiento de los contratos que celebre. iv) El control y la prevención del fraude. v) La liquidación y pago de siniestros. vi) Todo lo que involucre la gestión integral del seguro contratado. vii) Controlar el cumplimiento de requisitos para acceder al Sistema General de Seguridad Social Integral viii) La elaboración de estudios de técnicos-actuariales, estadísticas, encuestas, análisis de tendencia del mercado y, en general, estudios de técnica aseguradora. ix) Envío de información relativa a la educación financiera, encuestas de satisfacción de clientes y ofertas comerciales de seguros, así como de otros servicios inherentes a la actividad aseguradora. X) Realización de encuestas sobre satisfacción en los servicios prestados por LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS, xi) Intercambio o remisión de información en virtud de tratados y acuerdos internacionales e intergubernamentales suscritos por Colombia, xii) la prevención y control del lavado de activos y la financiación del terrorismos y la xiii) consulta, almacenamiento, administración , transferencia, procesamiento y reporte de información a las Centrales de información o bases de datos debidamente constituidas referentes al comportamiento crediticio, financiero y comercial.";
                    lblClau_13.Text = "2 el tratamiento podrá ser realizado directamente por las citadas sociedades o por los encargados del tratamiento que ellas consideren necesarios.";
                    lblClau_14.Text = "3 USUARIOS DE LA INFORMACION: Que los datos podrán ser compartidos, transmitidos, entregados, transferidos o divulgados para las finalidades mencionadas a i) las personas jurídicas que tienen la calidad de filiales, subsidiarias o vinculadas, o de matriz de LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS Y REASEGUROS. ii)Los operadores necesarios para el cumplimiento de derechos y obligaciones derivados de los contratos celebrados con LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS, tales como: ajustadores, call centers, investigadores, compañías de asistencia, abogados externos, entre otros iii) LOS INTERMEDIARIOS DE SEGUROS que intervengan en el proceso de celebración, ejecución y terminación del contrato de seguro iv) las personas con las cuales LA ASEGURADORA y/o EL INTERMEDIARIO DE SEGUROS adelante gestiones para efectos de celebrar contratos de Coaseguro o Reaseguro v) FASECOLDA, INVERFAS S.A y el INIF, Personas jurídicas que administran bases de datos para efectos de prevención y control de fraudes, la selección de riesgos, y control de requisitos para acceder al Sistema General de Seguridad Social Integral, como la elaboración de estudios actuariales.";
                    lblClau_15.Text = "4 TRANSFERENCIA INTERNACIONAL DE INFORMACION A TERCEROS PAISES: Que en ciertas situaciones es necesario realizar transferencias internacionales de mis datos para cumplir las finalidades del tratamiento.";
                    lblClau_16.Text = "5.DATOS SENSIBLES: Que son facultativas las respuestas a las preguntas que me han hecho o me harán sobre datos personales sensibles, de conformidad con la definición legal vigente. En consecuencia, no he sido obligado a responderlas, por lo que autorizo expresamente para que se lleve a cabo el tratamiento de mis datos sensibles, en especial, los relativos a la salud y a los datos biométricos. En todo caso, para efectos del presente formulario de conocimiento se debe tener en consideración que el capítulo Xl del título l de la Circular Básica Jurídica de la Superintendencia Financiera de Colombia exige las mismas.";
                    lblClau_17.Text = "6.DATOS PERSONALES DE NIÑAS, NIÑOS Y ADOLESCENTES: Que son facultativas las respuestas de las preguntas sobre datos de niñas, niños y adolescentes. En consecuencia, no he sido obligado a responderlas.";
                    lblClau_18.Text = "7.DERECHOS DEL TITULAR DE LA INFORMACIÓN: Que como titular de la información, me asisten los derechos previstos en las leyes 1266 de 2008 y 1581 de 2012. En especial, me asiste el derecho a conocer, actualizar ,rectificar, revocar y suspender las informaciones que se hayan recogido sobre mi.";
                    lblClau_19.Text = "8.RESPONSABLES Y ENCARGADOS DEL TRATAMIENTO DE LA INFORMACION: Que los responsables del tratamiento de la información son LAS ASEGURADORAS Y/O INTERMEDIARIOS, cuyos datos de contacto se incluyeron en el encabezado de esta autorización. En todo caso, los encargados del Tratamiento de los datos que se compartan,transfieran, transmitan, entreguen o divulguen, en desarrollo de lo previsto en el literal v) del numeral 3 anterior, serán.";
                    lblClau_20.Text = "a) FASECOLDA cuya dirección es carrera 7 No 26 – 20 piso 11 y 12, email: fasecolda@fasecolda.com Tel 3443080 de la Ciudad de Bogotá D.C";
                    lblClau_21.Text = "b) INVERFAS S.A cuya dirección es Carrera 7 No 26-20 piso 11, email: inverfas@fasecolda.com Tel. 3443080 de la Ciudad de Bogotá D.C";
                    lblClau_22.Text = "c) INIF- Instituto Nacional de Investigación y Prevención del Fraude al Seguro cuya dirección es Carrera 13 No 37-43, piso 8, email: directoroperatico@inif.com.co Tel. 2320105 de la ciudad de Bogotá D.C";
                    lblClau_23.Text = "iii.AUTORIZACION: De manera expresa, AUTORIZO el Tratamiento de los datos personales incluidos los sensibles y autorizo de ser necesario, la transferencia nacional e internacional de los mismos, por las personas, para las finalidades y en los términos que nos fueron informados en este documento.";
                    lblClau_24.Text = "iv) Autorizo al INTERMEDIARIO Y/O ASEGURADORA, sus sucursales, subsidiarias y/o filiales a divulgar mi información personal y financiera al US Internal Revenue Service (IRS), así como cualquier otraautoridad tributaria local o extranjera en virtud de cualquier requerimiento derivado del cumplimiento de la Ley de Cumplimiento Fiscal de Cuentas del Extranjero - FATCA o cualquier legislación similar local o internacional. Esta autorización aplicará al momento que sea aportada la información o aquél en el que el INTERMEDIARIO Y/O ASEGURADORA considere que dicha legislación le resulta aplicable. Así mismo, autorizo al INTERMEDIARIO Y/O ASEGURADORA a divulgar mi información en caso que el INTERMEDIARIO/ASEGURADORA celebre un acuerdo con otra jurisdicción que requiera o permita divulgar mi información a dicho país. La información divulgada por el INTERMEDIARIO Y/O ASEGURADORA podrá ser requerida por la ley en Colombia. En caso que divulgar dicha información no sea requerida por la ley, podré negarme a autorizar al INTERMEDIARIO Y/O ASEGURADORA a divulgar cualquier información relativa a mi persona. Dicha negativa podrá generar la revocación de los contratos de seguro o terminación de los productos adquiridos, salvo los contratos de renta vitalicia y de vida individual que por su naturaleza jurídica no son revocables en la jurisdicción colombiana.";
                    lblClau_25.Text = "";
                    lblClau_26.Text = "";
                    lblClau_27.Text = "";
                    #endregion
                    break;
                case 2://Juridico
                    infoTituloPN.Visible = !booVisible;
                    infoPN.Visible = !booVisible;
                    infoTituloPJ.Visible = booVisible;
                    infoPJ.Visible = booVisible;
                    infoDeclaracionFondosPJ.Visible = booVisible;
                    lblTipoCliente.Visible = booVisible;
                    ddlTipoCliente.Visible = booVisible;
                    ddlTipoCliente.ValidationGroup = "vdgWillis";
                    rfvTipoCliente.Enabled = booVisible;

                    #region Encabezado
                    lblTituloFormulario.Text = "";
                    lblSubTituloFormulario.Text = "";
                    #endregion
                    #region InfoClaseVinculacionNota
                    lblCVN.Text = "EN EL EVENTO EN EL QUE EL POTENCIAL CLIENTE NO CUENTE CON LA INFORMAICIÓN SOLICITADA EN ESTE FORMULARIO, DEBERÁ CONSIGNAR DICHA CIRCUNSTACIA EN EL ESPACIO CORRESPONDIENTE";
                    #endregion
                    #region InfoNota2
                    lblN2.Text = "INDIQUE LOS VINCULOS EXISTENTES ENTRE EL TOMADOR, ASEGURADOR, AFIANZADO Y BENEFICIARIO: (INDIVIDUALIZACIÓN DEL PRODUCTO)";
                    #endregion
                    #region infoDeclaracionFondos
                    lblDF_1.Text = "Declaro expresamente que";
                    lblDF_2.Text = "1.La actividad, profesión u oficio de la compañía es lícita y se ejerce dentro del marco legal y los recursos de la misma no provienen deactividades ilícitas de las contempladas en el Código Penal Colombiano.";
                    lblDF_3.Text = "2.La información suministrada en la solicitud y en este documento es veraz y verificable y la sociedad se compromete a actualizarla anualmente.";
                    lblDF_4.Text = "3.Los recursos que se deriven del desarrollo de este contrato no se destinaran a la financiación del terrorismo, grupos terroristas oactividades terroristas.";
                    lblDF_5.Text = "4.Los recursos que posee la compañía provienen de la(s) actividades descritas anteriormente.";
                    #endregion
                    #region infoPF1
                    lblPF_1.Text = "DOCUMENTOS MINIMOS REQUERIDOS:";
                    lblPF_2.Text = "Se debe adjuntar la siguiente documentación:";
                    lblPF_3.Text = "1 Estados financieros comparados a último corte Contable, 2 Fotocopia del documento de identificación ampliada al 150% del Representante Legal, 3 Declaración de Renta del último periodo gravable disponible, 4 Copia del RUT, 5 Cámara de Comercio no Mayor a 30 días, La entidad aseguradora podrá requerir información adicional que considere relevante y necesaria para controlar el riesgos de LA/FT.";
                    lblPF_4.Text = "";
                    #endregion
                    #region infoAutorizacion
                    lblAuto_1.Text = "";
                    lblAuto_2.Text = "";
                    lblAuto_3.Text = "";
                    lblAuto_4.Text = "";
                    lblAuto_5.Text = "";

                    #endregion
                    #region infoClausula
                    lblClau_1.Text = "";
                    lblClau_2.Text = "";
                    lblClau_3.Text = "";
                    lblClau_4.Text = "";
                    lblClau_5.Text = "";
                    lblClau_6.Text = "";
                    lblClau_7.Text = "y/o cualquier sociedad controlada, directa o indirectamente, por la misma sociedad(es) matriz de la(s) sociedad(es) referenciadas y con la(s) que se suscriba(n) contrato(s) de seguros.";
                    lblClau_8.Text = "Así mismo entiéndase como INTERMEDIARIO de SEGUROS La(s) sociedad(es) WILLIS COLOMBIA CORREDORES DE SEGUROS S.A y/o cualquier sociedad controlada, directa o indirectamente, por la misma sociedad matriz de la(s) sociedad(es) antes mencionada(s).";
                    lblClau_9.Text = "Dirección: Avenida Calle 26 No. 59-41 Piso 6 , Teléfono: +57 (1) 606 7575.";
                    lblClau_10.Text = "Declaro expresamente:";
                    lblClau_11.Text = "I. Que para efectos de acceder a la prestación de servicios por parte de LA ASEGURADORA Y/O EL INTERMIDIARIO DE SEGUROS, suministramos nuestros datos para todos los fines precontractuales y contractuales que comprende la actividad aseguradora.";
                    lblClau_12.Text = "II. Que LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS me han informado, de manera expresa:";
                    lblClau_13.Text = "1. FINALIDAD DEL TRATAMIENTO DE DATOS PERSONALES: Nuestros datos serán tratados por LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS, para las siguientes finalidades: i) El trámite de nuestra solicitud de vinculación como consumidor financiero, deudor, contraparte contractual y/o proveedor ii) El proceso de negociación de contratos con LA ASEGURADORA, incluyendo la determinación de primas y la selección de riesgos. iii) La ejecución y el cumplimiento de los contratos que celebre. iv) El control y la prevención del fraude. v) La liquidación y pago de siniestros. vi) Todo lo que involucre la gestión integral del seguro contratado. vii) Controlar el cumplimiento de requisitos para acceder al Sistema General de Seguridad Social Integral viii) La elaboración de estudios técnicos-actuariales, estadísticas, encuestas, análisis de tendencia del mercado y, en general, estudios de técnica aseguradora. ix) Envío de información relativa a la educación financiera, encuestas de satisfacción de clientes y ofertas comerciales de seguros, así como de otros servicios inherentes a la actividad aseguradora. X) Realización de encuestas sobre satisfacción en los servicios prestados por LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS, xi) Intercambio o remisión de información en virtud de tratados y acuerdos internacionales e intergubernamentales suscritos por Colombia, xii)la prevención y control del lavado de activos y la financiación del terrorismos y la xiii) consulta, almacenamiento, administración , transferencia, procesamiento y reporte de información a lasCentrales de Información o bases de datos debidamente constituidas referentes al comportamiento crediticio, financiero y comercial.";
                    lblClau_14.Text = "2. El tratamiento podrá ser realizado directamente por las citadas sociedades o por los encargados del tratamiento que ellas consideren necesarios.";
                    lblClau_15.Text = "3. USUARIOS DE LA INFORMACIÓN: Que los datos podrán ser compartidos, transmitidos, entregados, transferidos o divulgados para las finalidades mencionadas a: i) las personas jurídicas que tienen lacalidad de filiales, subsidiarias o vinculadas, o de matriz de LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS Y REASEGUROS. ii)Los operadores necesarios para el cumplimiento de derechos y obligaciones derivados de los contratos celebrados con LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS, tales como: ajustadores, call centers, investigadores, compañías de asistencia, abogados externos, entre otros iii) LOS INTERMEDIARIOS DE SEGUROS que intervengan en el proceso de celebración, ejecución y terminación del contrato de seguro iv) las personas con las cuales LA ASEGURADORA Y/O EL INTERMEDIARIO DE SEGUROS adelante gestiones para efectos de celebrar contratos de Coaseguro o Reaseguro v) FASECOLDA, INVERFAS S.A y el INIF, Personas jurídicas que administran bases de datos para efectos de prevención y control de fraudes, la selección de riesgos, y control de requisitos para acceder al Sistema General de Seguridad Social Integral, como la elaboración de estudios actuariales.";
                    lblClau_16.Text = "4. TRANSFERENCIA INTERNACIONAL DE INFORMACIÓN A TERCEROS PAISES: Que en ciertas situaciones es necesario realizar transferencias internacionales de mis datos para cumplir las finalidades del tratamiento.";
                    lblClau_17.Text = "5.DATOS PERSONALES DE NIÑAS, NIÑOS Y ADOLESCENTES: Que son facultativas las respuestas a las preguntas sobre datos de niñas, niños y adolescentes. En consecuencia, no hemos sido obligados a responderlas.";
                    lblClau_18.Text = "6. DERECHOS DEL TITULAR DE LA INFORMACIÓN: Que como titular de la información, nos asisten los derechos previstos en la Ley 1266 de 2008. En especial, el derecho a conocer, actualizar y rectificar las informaciones que se hayan recogido sobre nosotros.";
                    lblClau_19.Text = "7. RESPONSABLES Y ENCARGADOS DEL TRATAMIENTO DE LA INFORMACION: Que los responsables del tratamiento de la información son LAS ASEGURADORAS Y/O INTERMEDIARIOS, cuyos datos de contacto se incluyeron en el encabezado de esta autorización. En todo caso, los encargados del Tratamiento de los datos que se compartan,transfieran, transmitan, entreguen o divulguen, en desarrollo de lo previsto en el literal v) del numeral 3 anterior, serán.";
                    lblClau_20.Text = "a) FASECOLDA cuya dirección es carrera 7 No 26 – 20 piso 11 y 12, email: fasecolda@fasecolda.com Tel 3443080 de la Ciudad de Bogotá D.C";
                    lblClau_21.Text = "b) INVERFAS S.A. cuya dirección es Carrera 7 No 26-20 piso 11, email: inverfas@fasecolda.com Tel. 3443080 de la Ciudad de Bogotá D.C";
                    lblClau_22.Text = "c) INIF- Instituto Nacional de Investigación y Prevención del Fraude al Seguro cuya dirección es Carrera 13 No 37-43, piso 8, email: directoroperatico@inif.com.co Tel. 2320105 de la ciudad de BogotáD.C";
                    lblClau_23.Text = "iii.AUTORIZACIÓN: De manera expresa, AUTORIZAMOS el Tratamiento de los datos y, de ser necesario, la transferencia nacional e internacional de los mismos, por las personas, para las finalidades y en los términos que nos fueron informados en este documento.";
                    lblClau_24.Text = "iv) CERTIFICACION: Manifestamos que la información suministrada por nosotros para las finalidades señaladas en este documento, puede contener datos personales de empleados, proveedores, colaboradores o clientes de la compañía, por lo cual certificamos de manera expresa que la misma, ha sido: i) obtenida de acuerdo con lo previsto en la legislación aplicable, particularmente en la Ley 1581 de 2012 y (ii) que , existen las autorizaciones necesarias de acuerdo con lo previsto en la legislación aplicable, para el tratamiento y circulación de esta Base de Datos por parte de LA(S) ASEGURADA(S) Y/O INTERMEDIARIOS señalados en este documento..";
                    lblClau_25.Text = "v) Autorizamos al INTERMEDIARIO Y/O ASEGURADORA, sus sucursales, subsidiarias y/o filiales a divulgar nuestra información personal y financiera al US Internal Revenue Service (IRS), así comocualquier otra autoridad tributaria local o extranjera en virtud de cualquier requerimiento derivado del cumplimiento de la Ley de Cumplimiento Fiscal de Cuentas del Extranjero - FATCA o cualquier legislación similar";
                    lblClau_26.Text = "local o internacional. Esta autorización aplicará al momento que sea aportada la información o aquél en el que el INTERMEDIARIO Y/O ASEGURADORA considere que dicha legislación le resulta aplicable. Así mismo, autorizo al INTERMEDIARIO Y/O ASEGURADORA a divulgar nuestra información en caso que el INTERMEDIARIO/ASEGURADORA celebre un acuerdo con otra jurisdicción que requiera o permita divulgar mi información a dicho país. La información divulgada por el INTERMEDIARIO Y/O ASEGURADORA podrá ser requerida por la ley en Colombia. En caso que divulgar dicha información no sea requerida por la ley, podré negarme a autorizar al INTERMEDIARIO Y/O ASEGURADORA a divulgar cualquier información relativa a mi persona. Dicha negativa podrá generar la revocación de los contratos de seguro o terminación de los productos adquiridos, salvo los contratos de renta vitalicia y de vida individual que por su naturaleza jurídica no son revocables en la jurisdicción colombiana.";
                    lblClau_27.Text = "";
                    #endregion

                    break;
            }

            Encabezado.Visible = booVisible;
            InfoClaseVinculacionNota.Visible = booVisible;
            InfoClaseVinculacion.Visible = booVisible;
            infoNota2.Visible = booVisible;
            infoVinculos.Visible = booVisible;
            infoTituloDeclaracionFondos.Visible = booVisible;
            infoDeclaracionFondos.Visible = booVisible;
            infoTituloPF.Visible = booVisible;
            infoPF1.Visible = booVisible;
            infoTituloSeguros.Visible = booVisible;
            infoSeguros1.Visible = booVisible;
            infoSeguros2.Visible = booVisible;
            infoAutorizacion.Visible = booVisible;
            infoTituloClausula.Visible = booVisible;
            infoClausula.Visible = booVisible;
            InfoFirmaHuella1.Visible = booVisible;
            InfoFirmaHuella2.Visible = booVisible;
            infoEntrevista.Visible = booVisible;

            //mtdHabilitaVinculos(0, false);
            //mtdHabilitaVinculos(1, false);
            //mtdHabilitaVinculos(2, false);
            //mtdHabilitaVinculacion(false);
            //mtdHabilitaMoneda(false);
        }

        private void mtdHabilitaVinculos(int intTipoDatos, bool booVisible)
        {
            switch (intTipoDatos)
            {
                case 0:
                    lblOtraTomadorAsegurado.Visible = booVisible;
                    tbxOtraTomadorAsegurado.Visible = booVisible;
                    if (!booVisible)
                        tbxOtraTomadorAsegurado.Text = "";
                    break;
                case 1:
                    lblOtraTomadorBeneficiario.Visible = booVisible;
                    tbxOtraTomadorBeneficiario.Visible = booVisible;
                    if (!booVisible)
                        tbxOtraTomadorBeneficiario.Text = "";
                    break;
                case 2:
                    lblOtraAseguradoBeneficiario.Visible = booVisible;
                    tbxOtraAseguradoBeneficiario.Visible = booVisible;
                    if (!booVisible)
                        tbxOtraAseguradoBeneficiario.Text = "";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTomadorAsegurado_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTomadorAsegurado.SelectedValue.ToString().Trim())
            {
                case "OTRA":
                    mtdHabilitaVinculos(0, true);
                    break;
                default:
                    mtdHabilitaVinculos(0, false);
                    break;
            }
        }

        protected void ddlTomadorBeneficiario_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTomadorBeneficiario.SelectedValue.ToString().Trim())
            {
                case "OTRA":
                    mtdHabilitaVinculos(1, true);
                    break;
                default:
                    mtdHabilitaVinculos(1, false);
                    break;
            }
        }

        protected void ddlAseguradoBeneficiario_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlAseguradoBeneficiario.SelectedValue.ToString().Trim())
            {
                case "OTRA":
                    mtdHabilitaVinculos(2, true);
                    break;
                default:
                    mtdHabilitaVinculos(2, false);
                    break;
            }
        }

        private void mtdHabilitaReclamos(bool booVisible)
        {
            infoSeguros2.Visible = booVisible;
            rfvSeguroAno1.Enabled = booVisible;
            rfvSeguroRamo1.Enabled = booVisible;
            rfvSeguroCompania1.Enabled = booVisible;
            rfvSeguroValor1.Enabled = booVisible;
            rfvSeguroTipo1.Enabled = booVisible;
        }

        protected void ddlReclamaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlReclamaciones.SelectedValue.ToString().Trim())
            {
                case "SI":
                    mtdHabilitaReclamos(true);
                    break;
                default:
                    mtdHabilitaReclamos(false);
                    break;
            }
        }

        void mtdHabilitaVinculacion(bool booVisible)
        {
            lblOtraVinculacion.Visible = booVisible;
            tbxOtraClaseVinculacion.Visible = booVisible;
            if (!booVisible)
                tbxOtraClaseVinculacion.Text = "";
        }
        protected void dllClaseVinculacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlClaseVinculacion.SelectedValue.ToString().Trim())
            {
                case "OTRO":
                    mtdHabilitaVinculacion(true);
                    break;
                default:
                    mtdHabilitaVinculacion(false);
                    break;
            }
        }

        void mtdHabilitaMoneda(bool booVisible)
        {
            lblTipoTransaccion.Visible = booVisible;
            ddlTipoTransaccion.Visible = booVisible;
            rfvTipoTransaccion.Enabled = booVisible;
        }
        protected void ddlTransacMonedaExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTransacMonedaExtra.SelectedValue.ToString().Trim())
            {
                case "SI":
                    mtdHabilitaMoneda(true);
                    break;
                default:
                    mtdHabilitaMoneda(false);
                    break;

            }
        }

        protected void ddlPNCIIUDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxPNCIIU.Text = ddlPNCIIUDescripcion.SelectedValue.ToString().Trim();
            DDLcodCIIU.SelectedValue = ddlPNCIIUDescripcion.SelectedValue.ToString().Trim();
        }

        protected void ddlPJCodCIIU2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxPJCIIU.Text = ddlPJCodCIIU2.SelectedValue.ToString().Trim();
            DDLPJcodCiiu.SelectedValue = ddlPJCodCIIU2.SelectedValue.ToString().Trim();
        }

        protected void ddlClaseVinculacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlClaseVinculacion.SelectedValue.ToString().Trim())
            {
                case "OTRO":
                    mtdHabilitaVinculacion(true);
                    break;
                default:
                    mtdHabilitaVinculacion(false);
                    break;
            }
        }
        protected void tbxPJDocumentoRepLegalPpal_TextChanged(object sender, EventArgs e)
        {
        }

        protected void tbxPNIngresosMensuales_TextChanged(object sender, EventArgs e)
        {
            tbxPNIngresosMensuales.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPNIngresosMensuales.Text)).ToString("N2");
        }

        protected void tbxPNEgresoMensuales_TextChanged(object sender, EventArgs e)
        {
            tbxPNEgresoMensuales.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPNEgresoMensuales.Text)).ToString("N2");
        }

        protected void tbxPNActivos_TextChanged(object sender, EventArgs e)
        {
            tbxPNActivos.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPNActivos.Text)).ToString("N2");
        }

        protected void tbxPNPasivos_TextChanged(object sender, EventArgs e)
        {
            tbxPNPasivos.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPNPasivos.Text)).ToString("N2");
        }

        protected void tbxPNPatrimonio_TextChanged(object sender, EventArgs e)
        {
            tbxPNPatrimonio.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPNPatrimonio.Text)).ToString("N2");
        }
        protected void tbxPNOtrosIngresos_TextChanged(object sender, EventArgs e)
        {
            tbxPNOtrosIngresos.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPNOtrosIngresos.Text)).ToString("N2");
        }
        protected void tbxPJIngresosMensuales_TextChanged(object sender, EventArgs e)
        {
            tbxPJIngresosMensuales.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPJIngresosMensuales.Text)).ToString("N2");
        }

        protected void tbxPJEgresoMensuales_TextChanged(object sender, EventArgs e)
        {
            tbxPJEgresoMensuales.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPJEgresoMensuales.Text)).ToString("N2");
        }

        protected void tbxPJActivos_TextChanged(object sender, EventArgs e)
        {
            tbxPJActivos.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPJActivos.Text)).ToString("N2");
        }

        protected void tbxPJOtrosIngresos_TextChanged(object sender, EventArgs e)
        {
            tbxPJOtrosIngresos.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPJOtrosIngresos.Text)).ToString("N2");
        }

        protected void tbxPJPasivos_TextChanged(object sender, EventArgs e)
        {
            tbxPJPasivos.Text = Convert.ToDecimal(Sanitizer.GetSafeHtmlFragment(tbxPJPasivos.Text)).ToString("N2");
        }

        protected void ddlPNPregunta4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlPNPregunta4.SelectedValue.ToString().Trim())
            {
                case "SI":
                    mtdHabilitaPregunta4(true);
                    TrVinculoPPExpuestaPN.Visible = true;
                    break;
                default:
                    mtdHabilitaPregunta4(false);
                    TrVinculoPPExpuestaPN.Visible = false;
                    TXprimerApellidoPNPPE.Text = "";
                    TXsegundApellidoPNPPE.Text = "";
                    TXnombrePNPPE.Text = "";
                    TXocupacionPNPPE.Text = "";
                    TXcargoPNPPE.Text = "";
                    break;
            }
        }

        void mtdHabilitaPregunta4(bool booVisible)
        {
            /*lblPNEspecificacionPreguntas.Visible = booVisible;
            tbxPNEspecificacionPreguntas.Visible = booVisible;
            rfvPNEspecificacionPreguntas.Enabled = booVisible;
            if (!booVisible)
                tbxPNEspecificacionPreguntas.Text = "";*/
            
        }

        protected void ddlPNPregunta5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlPNPregunta5.SelectedValue.ToString().Trim())
            {
                case "SI":
                    mtdHabilitaPregunta5(true);
                    break;
                default:
                    mtdHabilitaPregunta5(false);
                    break;
            }
        }
        void mtdHabilitaPregunta5(bool booVisible)
        {
            lblPNEspecificacionPreguntas2.Visible = booVisible;
            tbxPNEspecificacionPreguntas2.Visible = booVisible;
            rfvPNEspecificacionPreguntas2.Enabled = booVisible;
            if (!booVisible)
                tbxPNEspecificacionPreguntas2.Text = "";
        }

        protected void DDLcodCIIU_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string cod = DDLcodCIIU.SelectedValue.ToString();
            ddlPNCIIUDescripcion.SelectedValue = cod;
            tbxPNCIIU.Text = cod;
        
    }

        protected void DDLPJcodCiiu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cod = DDLPJcodCiiu.SelectedValue.ToString().Trim();
            ddlPJCodCIIU2.SelectedValue = cod;
            tbxPJCIIU.Text = cod;
        }

        protected void ddlTipoActividadPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoActividadPN.SelectedValue == "15")
            {
                tdOtroTipoActividadPN.Visible = true;
                tdValorOtroTipoActividadPN.Visible = true;
            } else
            {
                tdOtroTipoActividadPN.Visible = false;
                tdValorOtroTipoActividadPN.Visible = false;
                TXvalorOtroTipoActividadPN.Text = "";
            }
        }

        protected void ddlPJPregunta4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPJPregunta4.SelectedValue != "SI")
            {
                rfvEspefificacionP4.Enabled = false;
                tbxPJEspecificacionPreguntas.Visible = false;
                lblPJEspecificacionPreguntas.Visible = false;
                tbxPJEspecificacionPreguntas.Text = "";
            }
            else
            {
                rfvEspefificacionP4.Enabled = true;
                tbxPJEspecificacionPreguntas.Visible = true;
                lblPJEspecificacionPreguntas.Visible = true;
            }
        }

        protected void DDLTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLTipoPersona.SelectedValue == "1")
            {
                TblFiltroNatural.Visible = true;
                TblFiltroJuridica.Visible = false;
            }
            else
            {
                TblFiltroNatural.Visible = false;
                TblFiltroJuridica.Visible = true;
            }
        }

        protected void Btn_Aceptar_Click(object sender, EventArgs e)
        {
            BuscarFCC();
            trIframe.Visible = false;
        }

        protected void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            DDLTipoPersona.SelectedIndex = 0;
            DDLEstado.SelectedIndex = 0;
            TxBxPApellido.Text = string.Empty;
            TxBxSApellido.Text = string.Empty;
            TxBxNombre.Text = string.Empty;
            TxBxNumeiden.Text = string.Empty;
            TxBxRazonSocial.Text = string.Empty;
            TxBxNit.Text = string.Empty;
            TxbFechaIni.Text = string.Empty;
            TxbFechaFin.Text = string.Empty;

            TblFiltroNatural.Visible = false;
            TblFiltroJuridica.Visible = false;

            TrGvResultados.Visible = false;
            TrGvExportar.Visible = false;
            trIframe.Visible = false;

            BtnCerrarIFrame.Visible = false;
        }

        protected void BtnCerrarIFrame_Click(object sender, EventArgs e)
        {
            trIframe.Visible = false;
            BtnCerrarIFrame.Visible = false;
        }

        protected void ImgBtnExportar_Click(object sender, ImageClickEventArgs e)
        {
            Convertexcel(InfoGrid, Response, "Export Clientes");
        }

        protected void btn_Exportar_Click(object sender, EventArgs e)
        {
            exportExcel(Response, "Interface_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {
            string strErrMsg = string.Empty;
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("NUMERO DE FOURNISSEUR");
            gridEncabezado.Columns.Add("GROUPE DE COMPTES FOURNISSEURS");
            gridEncabezado.Columns.Add("TITRE DE CIVILITE");
            gridEncabezado.Columns.Add("NOM 1");
            gridEncabezado.Columns.Add("NOM 2");
            gridEncabezado.Columns.Add("NOM 3");
            gridEncabezado.Columns.Add("NOM 4");
            gridEncabezado.Columns.Add("Critere de recherche 1");
            gridEncabezado.Columns.Add("Critere de recherche 2");
            gridEncabezado.Columns.Add("RUE");
            gridEncabezado.Columns.Add("Numero de rue");
            gridEncabezado.Columns.Add("Code postal");
            gridEncabezado.Columns.Add("VILLE");
            gridEncabezado.Columns.Add("Pays");
            gridEncabezado.Columns.Add("REGION");
            gridEncabezado.Columns.Add("Fuseau horaire");
            gridEncabezado.Columns.Add("Sigle Batiment");
            gridEncabezado.Columns.Add("Salle");
            gridEncabezado.Columns.Add("Etage");
            gridEncabezado.Columns.Add("Chez");
            gridEncabezado.Columns.Add("Rue 2");
            gridEncabezado.Columns.Add("Rue 3");
            gridEncabezado.Columns.Add("Complement de numero");
            gridEncabezado.Columns.Add("Rue 4");
            gridEncabezado.Columns.Add("Rue 5");
            gridEncabezado.Columns.Add("Arrondissement");
            gridEncabezado.Columns.Add("Autre domicile");
            gridEncabezado.Columns.Add("Groupement de la structure regionale");
            gridEncabezado.Columns.Add("Non-distribution");
            gridEncabezado.Columns.Add("Boîte postale");
            gridEncabezado.Columns.Add("BP sans N°");
            gridEncabezado.Columns.Add("Type du service de distribution");
            gridEncabezado.Columns.Add("Numero du service de distribution");
            gridEncabezado.Columns.Add("Station postale");
            gridEncabezado.Columns.Add("Code postal2");
            gridEncabezado.Columns.Add("Autre ville");
            gridEncabezado.Columns.Add("Autre pays");
            gridEncabezado.Columns.Add("Autre region");
            gridEncabezado.Columns.Add("Code postal de l'entreprise");
            gridEncabezado.Columns.Add("Non-distribution2");
            gridEncabezado.Columns.Add("Langue");
            gridEncabezado.Columns.Add("TELEPHONE");
            gridEncabezado.Columns.Add("N° de poste");
            gridEncabezado.Columns.Add("PORTABLE");
            gridEncabezado.Columns.Add("Telecopie");
            gridEncabezado.Columns.Add("N° de poste2");
            gridEncabezado.Columns.Add("E-mail");
            gridEncabezado.Columns.Add("Mode de communication standard");
            gridEncabezado.Columns.Add("Remarques");
            gridEncabezado.Columns.Add("Client");
            gridEncabezado.Columns.Add("Autorisation");
            gridEncabezado.Columns.Add("Societe S/L partenaire");
            gridEncabezado.Columns.Add("Cle du groupe");
            gridEncabezado.Columns.Add("N° SIRET");
            gridEncabezado.Columns.Add("TYPE N° ID TAXE");
            gridEncabezado.Columns.Add("Taxe egalis.");
            gridEncabezado.Columns.Add("Personne phys");
            gridEncabezado.Columns.Add("TVA sur C.A.");
            gridEncabezado.Columns.Add("N° SIREN");
            gridEncabezado.Columns.Add("TYPE DE TAXE");
            gridEncabezado.Columns.Add("ADRESSE FISCALE");
            gridEncabezado.Columns.Add("NOM REPRES.");
            gridEncabezado.Columns.Add("TYPE D'ACTIVITE");
            gridEncabezado.Columns.Add("Type industrie");
            gridEncabezado.Columns.Add("N° site int. 1");
            gridEncabezado.Columns.Add("N° du site 2");
            gridEncabezado.Columns.Add("Chiffre contr.");
            gridEncabezado.Columns.Add("Branche");
            gridEncabezado.Columns.Add("NE(E) LE");
            gridEncabezado.Columns.Add("LIEU NAISS.");
            gridEncabezado.Columns.Add("SEXE");
            gridEncabezado.Columns.Add("PROFESSION");
            gridEncabezado.Columns.Add("PAYS");
            gridEncabezado.Columns.Add("CLE BANCAIRE");
            gridEncabezado.Columns.Add("COMPTE BANCAIRE");
            gridEncabezado.Columns.Add("TITULAIRE COMPTE");
            gridEncabezado.Columns.Add("Cle RIB");
            gridEncabezado.Columns.Add("Categorie bancaire partenaire");
            gridEncabezado.Columns.Add("Donnees reference");
            gridEncabezado.Columns.Add("Destinataire paiement divergent");
            gridEncabezado.Columns.Add("Code avis sup. mgn.");
            gridEncabezado.Columns.Add("Cle d'instruction");
            gridEncabezado.Columns.Add("Numero PBC/BVR");
            gridEncabezado.Columns.Add("Donnees individuelles");
            gridEncabezado.Columns.Add("Compte collectif");
            gridEncabezado.Columns.Add("Cle de tri");
            gridEncabezado.Columns.Add("Centrale");
            gridEncabezado.Columns.Add("Autorisation2");
            gridEncabezado.Columns.Add("CIIU code");
            gridEncabezado.Columns.Add("Anc. N° de cpte");
            gridEncabezado.Columns.Add("Cond. paiemt");
            gridEncabezado.Columns.Add("Classe toler.");
            gridEncabezado.Columns.Add("Cond.pmt avoirs");
            gridEncabezado.Columns.Add("Ctrl.fac.double");
            gridEncabezado.Columns.Add("Modes paiement");
            gridEncabezado.Columns.Add("Blocage paiement");
            gridEncabezado.Columns.Add("Dest.paiem.div.");
            gridEncabezado.Columns.Add("Banque societe");
            gridEncabezado.Columns.Add("Pmt individuel");
            gridEncabezado.Columns.Add("Cle regroup.");
            gridEncabezado.Columns.Add("Avis par EDI");
            gridEncabezado.Columns.Add("Classe toler.2");
            gridEncabezado.Columns.Add("PaiemtPre.");
            gridEncabezado.Columns.Add("Extrait compte");
            gridEncabezado.Columns.Add("Gest.cptable");
            gridEncabezado.Columns.Add("Cpte ch.fourn.");
            gridEncabezado.Columns.Add("Resp.chez four.");
            gridEncabezado.Columns.Add("Telephone gest.");
            gridEncabezado.Columns.Add("Telecopie gest.");
            gridEncabezado.Columns.Add("Internet compt.");
            gridEncabezado.Columns.Add("Note de compte");
            gridEncabezado.Columns.Add("Pays de declaration d'honoraires");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires2");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires2");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires2");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires2");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires2");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires3");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires3");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires3");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires3");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires3");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires4");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires4");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires4");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires4");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires4");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires5");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires5");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires5");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires5");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires5");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires6");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires6");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires6");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires6");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires6");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires7");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires7");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires7");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires7");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires7");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires8");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires8");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires8");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires8");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires8");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires9");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires9");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires9");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires9");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires9");
            gridEncabezado.Columns.Add("Type Declaration d'honoraires10");
            gridEncabezado.Columns.Add("Code Declaration d'honoraires10");
            gridEncabezado.Columns.Add("Assujetti a la declaration d'honoraires10");
            gridEncabezado.Columns.Add("Classe declaration d'honoraires10");
            gridEncabezado.Columns.Add("Identification declaration d'honoraires10");

            DataRow rowEncabezado;
            
            DataTable dtInfo = new DataTable();
            cRiesgo cRiesgo = new cRiesgo();
            string fechaInicial = string.Empty;
            if (TxbFechaIni.Text != string.Empty)
                fechaInicial = TxbFechaIni.Text;
            string fechaFinal = string.Empty;
            if (TxbFechaFin.Text != string.Empty)
                fechaFinal = TxbFechaFin.Text;
            dtInfo = cRiesgo.loadInfoInterfaceFCC(fechaInicial, fechaFinal);
            foreach (DataRow dr in dtInfo.Rows)
            {
                rowEncabezado = gridEncabezado.NewRow();
                rowEncabezado["NUMERO DE FOURNISSEUR"] = dr["numeroProveedor"].ToString().Trim();
                rowEncabezado["GROUPE DE COMPTES FOURNISSEURS"] = dr["cod"].ToString().Trim();
                rowEncabezado["TITRE DE CIVILITE"] = HttpUtility.HtmlEncode(dr["titre"].ToString().Trim());
                if(dr["RazonSocial"] != null && dr["RazonSocial"].ToString() != string.Empty)
                {
                    rowEncabezado["NOM 1"] = HttpUtility.HtmlEncode(dr["RazonSocial"].ToString().Trim());
                    rowEncabezado["NOM 2"] = "";
                    rowEncabezado["NOM 3"] = "";
                    rowEncabezado["NOM 4"] = "";
                }
                else 
                {
                    //dr["NombrePerNatural"].ToString().Trim()
                    string[] nombres = dr["NombrePerNatural"].ToString().Trim().Split(' ');
                    rowEncabezado["NOM 1"] = HttpUtility.HtmlEncode(nombres[0].ToString());
                    if(nombres.Length > 1)
                    {
                        if (nombres[1] != null)
                            rowEncabezado["NOM 2"] = HttpUtility.HtmlEncode(nombres[1].ToString());
                        else
                            rowEncabezado["NOM 2"] = "";
                    }else
                        rowEncabezado["NOM 2"] = "";

                    rowEncabezado["NOM 3"] = HttpUtility.HtmlEncode(dr["PrimerApellido"].ToString().Trim());
                    rowEncabezado["NOM 4"] = HttpUtility.HtmlEncode(dr["SegundoApellido"].ToString().Trim());
                }
                rowEncabezado["Critere de recherche 1"] = dr["critere"].ToString().Trim();
                rowEncabezado["Critere de recherche 2"] = "";
                rowEncabezado["RUE"] = dr["DireccionOfcPrincipal"].ToString().Trim();
                rowEncabezado["Numero de rue"] = "";
                rowEncabezado["Code postal"] = dr["coddepart"].ToString().Trim();
                rowEncabezado["VILLE"] = "";
                rowEncabezado["Pays"] = dr["Pays"].ToString().Trim();
                rowEncabezado["REGION"] = "";
                rowEncabezado["Fuseau horaire"] = "";
                rowEncabezado["Sigle Batiment"] = "";
                rowEncabezado["Salle"] = "";
                rowEncabezado["Etage"] = "";
                rowEncabezado["Chez"] = "";
                rowEncabezado["Rue 2"] = "";
                rowEncabezado["Rue 3"] = "";
                rowEncabezado["Complement de numero"] = "";
                rowEncabezado["Rue 4"] = "";
                rowEncabezado["Rue 5"] = "";
                rowEncabezado["Arrondissement"] = "";
                rowEncabezado["Autre domicile"] = "";
                rowEncabezado["Groupement de la structure regionale"] = "";
                rowEncabezado["Non-distribution"] = "";
                rowEncabezado["Boîte postale"] = "";
                rowEncabezado["BP sans N°"] = "";
                rowEncabezado["Type du service de distribution"] = "";
                rowEncabezado["Numero du service de distribution"] = "";
                rowEncabezado["Station postale"] = "";
                rowEncabezado["Code postal2"] = "";
                rowEncabezado["Autre ville"] = "";
                rowEncabezado["Autre pays"] = "";
                rowEncabezado["Autre region"] = "";
                rowEncabezado["Code postal de l'entreprise"] = "";
                rowEncabezado["Non-distribution2"] = "";
                rowEncabezado["Langue"] = dr["Langue"].ToString().Trim();
                rowEncabezado["TELEPHONE"] = dr["NumCelular"].ToString().Trim();
                rowEncabezado["N° de poste"] = "";
                rowEncabezado["PORTABLE"] = dr["Langue"].ToString().Trim();
                rowEncabezado["Telecopie"] = "";
                rowEncabezado["N° de poste2"] = "";
                rowEncabezado["E-mail"] = dr["CorreroContacto"].ToString().Trim();
                rowEncabezado["Mode de communication standard"] = dr["mode"].ToString().Trim();
                rowEncabezado["Remarques"] = "";
                rowEncabezado["Client"] = "";
                rowEncabezado["Autorisation"] = "";
                rowEncabezado["Societe S/L partenaire"] = "";
                rowEncabezado["Cle du groupe"] = "";
                rowEncabezado["N° SIRET"] = dr["siret"].ToString().Trim();
                rowEncabezado["TYPE N° ID TAXE"] = "";
                rowEncabezado["Taxe egalis."] = "";
                rowEncabezado["Personne phys"] = dr["Personnephys"].ToString().Trim();
                rowEncabezado["TVA sur C.A."] = "";
                rowEncabezado["N° SIREN"] = "";
                rowEncabezado["TYPE DE TAXE"] = dr["txe"].ToString().Trim();
                rowEncabezado["ADRESSE FISCALE"] = "";
                rowEncabezado["NOM REPRES."] = "";
                rowEncabezado["TYPE D'ACTIVITE"] = dr["activite"].ToString().Trim();
                rowEncabezado["Type industrie"] = "";
                rowEncabezado["N° site int. 1"] = "";
                rowEncabezado["N° du site 2"] = "";
                rowEncabezado["Chiffre contr."] = "";
                rowEncabezado["Branche"] = "";
                rowEncabezado["NE(E) LE"] = dr["fechaNacimiento"].ToString().Trim();
                rowEncabezado["LIEU NAISS."] = dr["LugarNacimiento"].ToString().Trim();
                rowEncabezado["SEXE"] = dr["sexe"].ToString().Trim();
                rowEncabezado["PROFESSION"] = dr["PROFESSION"].ToString().Trim();
                rowEncabezado["PAYS"] = dr["Expr1"].ToString().Trim();
                rowEncabezado["CLE BANCAIRE"] = dr["IdBanco"].ToString().Trim();
                rowEncabezado["COMPTE BANCAIRE"] = dr["NumeroCuenta"].ToString().Trim();
                rowEncabezado["TITULAIRE COMPTE"] = dr["ContantoPago"].ToString().Trim();
                rowEncabezado["Cle RIB"] = dr["TipoCuenta"].ToString().Trim();
                rowEncabezado["Categorie bancaire partenaire"] = "";
                rowEncabezado["Donnees reference"] = "";
                rowEncabezado["Destinataire paiement divergent"] = "";
                rowEncabezado["Code avis sup. mgn."] = "";
                rowEncabezado["Cle d'instruction"] = "";
                rowEncabezado["Numero PBC/BVR"] = "";
                rowEncabezado["Donnees individuelles"] = "";
                rowEncabezado["Compte collectif"] = dr["collectif"].ToString().Trim();
                rowEncabezado["Cle de tri"] = "";
                rowEncabezado["Centrale"] = "";
                rowEncabezado["Autorisation2"] = "";
                rowEncabezado["CIIU code"] = dr["IdCodCIIUTrabajo"].ToString().Trim();
                rowEncabezado["Anc. N° de cpte"] = "";
                rowEncabezado["Cond. paiemt"] = dr["paiemt"].ToString().Trim();
                rowEncabezado["Classe toler."] = "";
                rowEncabezado["Cond.pmt avoirs"] = "";
                rowEncabezado["Ctrl.fac.double"] = "";
                rowEncabezado["Modes paiement"] = dr["paiement"].ToString().Trim();
                rowEncabezado["Blocage paiement"] = "";
                rowEncabezado["Dest.paiem.div."] = "";
                rowEncabezado["Banque societe"] = "";
                rowEncabezado["Pmt individuel"] = "";
                rowEncabezado["Cle regroup."] = "";
                rowEncabezado["Avis par EDI"] = "";
                rowEncabezado["Classe toler.2"] = "";
                rowEncabezado["PaiemtPre."] = "";
                rowEncabezado["Extrait compte"] = "";
                rowEncabezado["Gest.cptable"] = "";
                rowEncabezado["Cpte ch.fourn."] = "";
                rowEncabezado["Resp.chez four."] = "";
                rowEncabezado["Telephone gest."] = "";
                rowEncabezado["Telecopie gest."] = "";
                rowEncabezado["Internet compt."] = "";
                rowEncabezado["Note de compte"] = "";
                rowEncabezado["Pays de declaration d'honoraires"] = "";
                rowEncabezado["Type Declaration d'honoraires"] = "";
                rowEncabezado["Code Declaration d'honoraires"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires"] = "";
                rowEncabezado["Classe declaration d'honoraires"] = "";
                rowEncabezado["Identification declaration d'honoraires"] = "";

                rowEncabezado["Type Declaration d'honoraires2"] = "";
                rowEncabezado["Code Declaration d'honoraires2"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires2"] = "";
                rowEncabezado["Classe declaration d'honoraires2"] = "";
                rowEncabezado["Identification declaration d'honoraires2"] = "";
                rowEncabezado["Type Declaration d'honoraires3"] = "";
                rowEncabezado["Code Declaration d'honoraires3"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires3"] = "";
                rowEncabezado["Classe declaration d'honoraires3"] = "";
                rowEncabezado["Identification declaration d'honoraires3"] = "";
                rowEncabezado["Type Declaration d'honoraires4"] = "";
                rowEncabezado["Code Declaration d'honoraires4"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires4"] = "";
                rowEncabezado["Classe declaration d'honoraires4"] = "";
                rowEncabezado["Identification declaration d'honoraires4"] = "";
                rowEncabezado["Type Declaration d'honoraires5"] = "";
                rowEncabezado["Code Declaration d'honoraires5"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires5"] = "";
                rowEncabezado["Classe declaration d'honoraires5"] = "";
                rowEncabezado["Identification declaration d'honoraires5"] = "";
                rowEncabezado["Type Declaration d'honoraires6"] = "";
                rowEncabezado["Code Declaration d'honoraires6"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires6"] = "";
                rowEncabezado["Classe declaration d'honoraires6"] = "";
                rowEncabezado["Identification declaration d'honoraires6"] = "";
                rowEncabezado["Type Declaration d'honoraires7"] = "";
                rowEncabezado["Code Declaration d'honoraires7"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires7"] = "";
                rowEncabezado["Classe declaration d'honoraires7"] = "";
                rowEncabezado["Identification declaration d'honoraires7"] = "";
                rowEncabezado["Type Declaration d'honoraires8"] = "";
                rowEncabezado["Code Declaration d'honoraires8"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires8"] = "";
                rowEncabezado["Classe declaration d'honoraires8"] = "";
                rowEncabezado["Identification declaration d'honoraires8"] = "";
                rowEncabezado["Type Declaration d'honoraires9"] = "";
                rowEncabezado["Code Declaration d'honoraires9"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires9"] = "";
                rowEncabezado["Classe declaration d'honoraires9"] = "";
                rowEncabezado["Identification declaration d'honoraires9"] = "";
                rowEncabezado["Type Declaration d'honoraires10"] = "";
                rowEncabezado["Code Declaration d'honoraires10"] = "";
                rowEncabezado["Assujetti a la declaration d'honoraires10"] = "";
                rowEncabezado["Classe declaration d'honoraires10"] = "";
                rowEncabezado["Identification declaration d'honoraires10"] = "";


                gridEncabezado.Rows.Add(rowEncabezado);
            }
            

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename="+ filename + ".xls;");
            string tab = "";
            foreach (DataColumn dc in gridEncabezado.Columns)
            {
                Response.Write(tab + HttpUtility.HtmlEncode( dc.ColumnName));
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in gridEncabezado.Rows)
            {
                tab = "";
                for (i = 0; i < gridEncabezado.Columns.Count; i++)
                {
                    Response.Write(tab + HttpUtility.HtmlEncode(dr[i].ToString()));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        protected void btn_Reporte_Click(object sender, EventArgs e)
        {
            exportReportExcel(Response, "Reporte_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportReportExcel(HttpResponse Response, string filename)
        {
            string strErrMsg = string.Empty;
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("Razon Social");
            gridEncabezado.Columns.Add("Documento");
            gridEncabezado.Columns.Add("Fecha Diligenciamiento");
            gridEncabezado.Columns.Add("Fecha Verificacion");
            gridEncabezado.Columns.Add("Telefono Domicilio Colombia");
            gridEncabezado.Columns.Add("Correo Electronico Domicilio Colombia");
            gridEncabezado.Columns.Add("Telefono Domicilio Exterior");
            gridEncabezado.Columns.Add("Correo Electronico Domicilio Exterior");

            DataRow rowEncabezado;

            DataTable dtInfo = new DataTable();
            cRiesgo cRiesgo = new cRiesgo();
            string fechaInicial = string.Empty;
            if (TxbFechaIni.Text != string.Empty)
                fechaInicial = TxbFechaIni.Text;
            string fechaFinal = string.Empty;
            if (TxbFechaFin.Text != string.Empty)
                fechaFinal = TxbFechaFin.Text;
            dtInfo = cRiesgo.loadInfoReporte();
            foreach (DataRow dr in dtInfo.Rows)
            {
                rowEncabezado = gridEncabezado.NewRow();
                rowEncabezado["Razon Social"] = dr["RazonSocial"].ToString().Trim();
                rowEncabezado["Documento"] = dr["Documento"].ToString().Trim();
                rowEncabezado["Fecha Diligenciamiento"] = dr["FechaDiligenciamiento"].ToString().Trim();
                rowEncabezado["Fecha Verificacion"] = dr["FechaVerificacion"].ToString().Trim();
                rowEncabezado["Telefono Domicilio Colombia"] = dr["dcTel"].ToString().Trim();
                rowEncabezado["Correo Electronico Domicilio Colombia"] = dr["dcMail"].ToString().Trim();
                rowEncabezado["Telefono Domicilio Colombia"] = dr["deTel"].ToString().Trim();
                rowEncabezado["Correo Electronico Domicilio Colombia"] = dr["deMail"].ToString().Trim();
                gridEncabezado.Rows.Add(rowEncabezado);
            }


            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".xls;");
            string tab = "";
            foreach (DataColumn dc in gridEncabezado.Columns)
            {
                Response.Write(tab + HttpUtility.HtmlEncode(dc.ColumnName));
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in gridEncabezado.Rows)
            {
                tab = "";
                for (i = 0; i < gridEncabezado.Columns.Count; i++)
                {
                    Response.Write(tab + HttpUtility.HtmlEncode(dr[i].ToString()));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
    }
}