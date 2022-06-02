using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.IO;
using System.Data;
using System.Web.SessionState;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls
{
    public partial class FormClienteWillis : System.Web.UI.UserControl
    {
        string IdFormulario = "6007";
        private cKnowClient cKnowClient = new cKnowClient();
        cCuenta cCuenta = new cCuenta();

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
            else
            {
                if (cCuenta.permisosConsulta(IdFormulario) == "False")
                    Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

                if (!Page.IsPostBack)
                    initValues();
            }
        }

        protected void ddlTipoFormulario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFormulario.SelectedValue.ToString().Equals("PERSONA NATURAL"))
                mtdHabilitarTipoFormulario(1, true);

            if (ddlTipoFormulario.SelectedValue.ToString().Equals("PERSONA JURÍDICA"))
                mtdHabilitarTipoFormulario(2, true);

            if (ddlTipoFormulario.SelectedValue.ToString().Equals("---"))
                mtdHabilitarTipoFormulario(0, false);
        }

        #region Buttons
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {
                            mtdCargarPdf();
                            loadGridArchivos();
                            loadInfoArchivos();
                            Mensaje("Archivo cargado exitósamente.");
                        }
                        else
                            Mensaje("El archivo a cargar debe ser en formato PDF.");
                    }
                    else
                        Mensaje("No hay archivos para cargar.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al adjuntar el documento. " + ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoFormulario.SelectedValue.ToString() != "---")
                {
                    if (cCuenta.permisosAgregar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        registrarFormulario();
                        Mensaje("Cliente registrado con éxito.");
                    }
                }
                else
                    Mensaje("Por favor indique el tipo de formulario.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar la información. " + ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string strPage = string.Empty, strIdForm = string.Empty;

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA NATURAL")
                strIdForm = "0";

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA JURÍDICA")
                strIdForm = "1";

            strPage = string.Format("window.open('ReporteKnowClienteWillis.aspx?IdConocimientoCliente={0}" +
                "&IdTipoForm={1}','Reporte','width=950px,height=900px,scrollbars=yes,resizable=yes')", IdConocimientoCliente, strIdForm);

            Response.Write("<script languaje=javascript>" + strPage + "</script>");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            rfvTipoTransaccion.ValidationGroup = "";
            initValues();
            resetValues();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            resetValues();
            enableCampos();
            initValues();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Button5.Visible = false;
            Button6.Visible = true;
            tbFormulario.Visible = false;
            tbDocumentos.Visible = true;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Button5.Visible = true;
            Button6.Visible = false;
            tbFormulario.Visible = true;
            tbDocumentos.Visible = false;
        }
        #endregion

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivos = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdf();
                    break;
            }
        }

        private void initValues()
        {
            IdConocimientoCliente = 0;
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
            ddlPJCodCIIU2.ClearSelection();
            ddlPNDpto.ClearSelection();
            ddlPNDpto2.ClearSelection();
            ddlPNCiudad1.ClearSelection();
            ddlPNCiudad2.ClearSelection();
            ddlPNDptoEmpresa.ClearSelection();
            ddlPNCiudadEmpresa.ClearSelection();

            ddlPNCIIUDescripcion.ClearSelection();
            DDLcodCIIU.ClearSelection();
            ddlPNPregunta1.SelectedIndex = 0;
            ddlPNPregunta2.SelectedIndex = 0;
            ddlPNPregunta3.SelectedIndex = 0;
            ddlPNPregunta4.SelectedIndex = 0;
            ddlPNPregunta5.SelectedIndex = 0;
            ddlPNActividadEconomica.SelectedIndex = 0;
            ddlPNSector1.SelectedIndex = 0;
            ddlPNSector2.SelectedIndex = 0;

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
            ddlPNActividadEconomica.SelectedIndex = 0;
            tbxPNCIIU.Text = "";
            tbxPNEmpresaTrabajo.Text = "";
            tbxPNCargo.Text = "";
            tbxPNDireccion.Text = "";
            tbxPNTelefono1.Text = "";
            tbxPNDireccionResidencia.Text = "";
            tbxPNTelefono2.Text = "";
            tbxPNCelular.Text = "";
            ddlPNPregunta1.SelectedIndex = 0;
            ddlPNPregunta2.SelectedIndex = 0;
            ddlPNPregunta3.SelectedIndex = 0;
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
            ddlPNPregunta4.SelectedIndex = 0;
            ddlPNPregunta5.SelectedIndex = 0;
            tbxPNEspecificacionPreguntas2.Text = "";
            ddlPNSector1.SelectedIndex = 0;
            ddlPNSector2.SelectedIndex = 0;
            tbxPNServicio.Text = "";

            TrVinculoPPExpuestaPN.Visible = false;
            TXprimerApellidoPNPPE.Text = "";
            TXsegundApellidoPNPPE.Text = "";
            TXnombrePNPPE.Text = "";
            TXocupacionPNPPE.Text = "";
            TXcargoPNPPE.Text = "";
            #endregion

            #region informacion PJ
            ddlPJCiudad1.ClearSelection();
            ddlPJCiudad2.ClearSelection();
            ddlPJCodCIIU2.ClearSelection();
            ddlPJDpto.ClearSelection();
            ddlPJDpto2.ClearSelection();

            tbxPJRazonDenominacion.Text = "";
            tbxPJNIT.Text = "";
            tbxPJPrimerApellido.Text = "";
            tbxPJSegundoApellido.Text = "";
            tbxPJNombres.Text = "";
            ddlPJTipoDocumento.SelectedIndex = 0;
            tbxPJNumeroDocumento.Text = "";
            tbxPJLugarExpedicion.Text = "";
            tbxPJFechaExpedicion.Text = "";
            tbxPJDireccionOficina.Text = "";
            tbxPJTelefono1.Text = "";
            tbxPJDireccionSucursal.Text = "";
            tbxPJTelefono2.Text = "";
            ddlPJTipoEmpresa.SelectedIndex = 0;
            ddlPJActividadEconomica.SelectedIndex = 0;
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
            ddlPJTipoDocRepLegalPpal.SelectedIndex = 0;
            tbxPJDocumentoRepLegalPpal.Text = "";
            tbxPJNombresRepLegal1.Text = "";
            ddlPJTipoDocRepLegal1.SelectedIndex = 0;
            tbxPJDocumentoRepLegal1.Text = "";
            tbxPJNombresRepLegal2.Text = "";
            ddlPJTipoDocRepLegal2.SelectedIndex = 0;
            tbxPJDocumentoRepLegal2.Text = "";
            tbxPJNombresRepLegal3.Text = "";
            ddlPJTipoDocRepLegal3.SelectedIndex = 0;
            tbxPJDocumentoRepLegal3.Text = "";
            tbxPJNombresRepLegal4.Text = "";
            ddlPJTipoDocRepLegal4.SelectedIndex = 0;
            tbxPJDocumentoRepLegal4.Text = "";
            tbxPJNacionalidad1.Text = "";
            tbxPJDV.Text = "";
            ddlPJPregunta1.SelectedIndex = 0;
            ddlPJPregunta2.SelectedIndex = 0;
            ddlPJPregunta3.SelectedIndex = 0;
            tbxPJEspecificacionPreguntas.Text = "";
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
            ddlPJCotizaBolsa.SelectedIndex = 0;
            ddlPJEstatal.SelectedIndex = 0;
            ddlPJSinAnimoLucro.SelectedIndex = 0;
            ddlPJSubsidiaria.SelectedIndex = 0;
            tbxPJSocMatriz.Text = "";
            tbxPJSocMatrizIdenTrib.Text = "";
            tbxPJSocMatrizJurisdiccion.Text = "";
            tbxPJSocMatrizDireccion.Text = "";
            tbxPJSocMatrizCiudad.Text = "";
            tbxPJSocMatrizTelefono.Text = "";
            tbxPJLugarNmto.Text = "";

            TrVinculoPPExpuestaPJ.Visible = false;
            TXprimerApellidoPPE.Text = "";
            TXsegundoApellidoPPE.Text = "";
            TXnombresPPE.Text = "";
            TXocipacionPPE.Text = "";
            TXcargoPPE.Text = "";

            tdOtroTipoActividadPN.Visible = false;
            tdValorOtroTipoActividadPN.Visible = false;
            ddlTipoActividadPN.ClearSelection();
            TXvalorOtroTipoActividadPN.Text = "";
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

            #region Habilita el formulario de Nuevo            
            Button1.Visible = true;
            Button2.Visible = false;
            Button3.Visible = true;
            Button4.Visible = false;
            Button5.Visible = false;
            Button6.Visible = false;
            tbFormulario.Visible = true;
            tbDocumentos.Visible = false;
            #endregion
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
            DDLcodCIIU.Enabled = booValor;
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
            tbxPJNombres.Enabled = booValor;
            ddlPJTipoDocumento.Enabled = booValor;
            DDLPJcodCiiu.Enabled = booValor;
            tbxPJNumeroDocumento.Enabled = booValor;
            tbxPJLugarExpedicion.Enabled = booValor;
            tbxPJFechaExpedicion.Enabled = booValor;
            tbxPJDireccionOficina.Enabled = booValor;
            ddlPJCiudad1.Enabled = booValor;
            tbxPJTelefono1.Enabled = booValor;
            tbxPJDireccionSucursal.Enabled = booValor;
            ddlPJCiudad2.Enabled = booValor;
            tbxPJTelefono2.Enabled = booValor;
            ddlPJTipoEmpresa.Enabled = booValor;
            ddlPJActividadEconomica.Enabled = booValor;
            tbxPJCIIU.Enabled = booValor;
            tbxPJIngresosMensuales.Enabled = booValor;
            tbxPJActivos.Enabled = booValor;
            tbxPJEgresoMensuales.Enabled = booValor;
            tbxPJPasivos.Enabled = booValor;
            tbxPJOtrosIngresos.Enabled = booValor;
            tbxPJConceptoOtrosIngresos.Enabled = booValor;
            tbxPJCorreoPrincipal.Enabled = booValor;
            tbxPJFechaConstitucion.Enabled = booValor;
            tbxPJNombresRepLegalPpal.Enabled = booValor;
            ddlPJTipoDocRepLegalPpal.Enabled = booValor;
            tbxPJDocumentoRepLegalPpal.Enabled = booValor;
            tbxPJNombresRepLegal1.Enabled = booValor;
            ddlPJTipoDocRepLegal1.Enabled = booValor;
            tbxPJDocumentoRepLegal1.Enabled = booValor;
            tbxPJNombresRepLegal2.Enabled = booValor;
            ddlPJTipoDocRepLegal2.Enabled = booValor;
            tbxPJDocumentoRepLegal2.Enabled = booValor;
            tbxPJNombresRepLegal3.Enabled = booValor;
            ddlPJTipoDocRepLegal3.Enabled = booValor;
            tbxPJDocumentoRepLegal3.Enabled = booValor;
            tbxPJNombresRepLegal4.Enabled = booValor;
            ddlPJTipoDocRepLegal4.Enabled = booValor;
            tbxPJDocumentoRepLegal4.Enabled = booValor;
            ddlPJCodCIIU2.Enabled = booValor;
            tbxPJNacionalidad1.Enabled = booValor;
            tbxPJDV.Enabled = booValor;
            ddlPJDpto.Enabled = booValor;
            ddlPJDpto2.Enabled = booValor;
            ddlPJPregunta1.Enabled = booValor;
            ddlPJPregunta2.Enabled = booValor;
            ddlPJPregunta3.Enabled = booValor;
            tbxPJEspecificacionPreguntas.Enabled = booValor;
            ddlPJPregunta4.Enabled = booValor;
            ddlPJPregunta5.Enabled = booValor;
            ddlPJPregunta6.Enabled = booValor;
            ddlPJPregunta7.Enabled = booValor;
            ddlPJPregunta8.Enabled = booValor;
            ddlPJPregunta9.Enabled = booValor;
            ddlPJPregunta10.Enabled = booValor;
            ddlPJPreguntaRep1Legal1.Enabled = booValor;
            ddlPJPreguntaRep1Legal2.Enabled = booValor;
            ddlPJPreguntaRep1Legal3.Enabled = booValor;
            ddlPJPreguntaRep1Legal4.Enabled = booValor;
            ddlPJPreguntaRep2Legal1.Enabled = booValor;
            ddlPJPreguntaRep2Legal2.Enabled = booValor;
            ddlPJPreguntaRep2Legal3.Enabled = booValor;
            ddlPJPreguntaRep2Legal4.Enabled = booValor;
            ddlPJPreguntaRep3Legal1.Enabled = booValor;
            ddlPJPreguntaRep3Legal2.Enabled = booValor;
            ddlPJPreguntaRep3Legal3.Enabled = booValor;
            ddlPJPreguntaRep3Legal4.Enabled = booValor;
            ddlPJPreguntaRep4Legal1.Enabled = booValor;
            ddlPJPreguntaRep4Legal2.Enabled = booValor;
            ddlPJPreguntaRep4Legal3.Enabled = booValor;
            ddlPJPreguntaRep4Legal4.Enabled = booValor;
            ddlPJPreguntaRepPpalLegal1.Enabled = booValor;
            ddlPJPreguntaRepPpalLegal2.Enabled = booValor;
            ddlPJPreguntaRepPpalLegal3.Enabled = booValor;
            ddlPJPreguntaRepPpalLegal4.Enabled = booValor;
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
        }

        private void enableCampos()
        {
            CamposFormulario(true);
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
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
                GridView1.DataSource = InfoGridArchivos;
                GridView1.DataBind();
            }
        }

        private void loadGridArchivos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivos = grid;
            GridView1.DataSource = InfoGridArchivos;
            GridView1.DataBind();
        }

        #region PDF
        private void mtdCargarPdf()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cKnowClient.loadCodigoInfoFormArchivo();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() +
                    "-" + IdConocimientoCliente.ToString() +
                    "-" + FileUpload1.FileName.ToString().Trim();
            else
                strNombreArchivo = "1-" + IdConocimientoCliente.ToString() +
                    "-" + FileUpload1.FileName.ToString().Trim();
            #endregion Nombre Archivo

            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);

            cKnowClient.mtdAgregarArchivoPdf(IdConocimientoCliente.ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdf()
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
        #endregion

        private void registrarFormulario()
        {
            IdConocimientoCliente = cKnowClient.agregarConocimientoCliente(
                string.Format("{0:yyyy MM dd}", DateTime.Now).Replace(" ", ""), String.Format("{0:yyyy}", DateTime.Now),
                ddlSucursal.Text.Trim(), ddlTipoFormulario.SelectedValue.ToString());

            cKnowClient.InfoFormCliente(/*IdConocimientoCliente,
                tbxFechaFormulario.Text.Trim(), ddlClaseVinculacion.SelectedValue.ToString().Trim(), tbxOtraClaseVinculacion.Text.Trim(), ddlTomadorAsegurado.SelectedValue.ToString().Trim(), tbxOtraTomadorAsegurado.Text.Trim(),
                ddlTomadorBeneficiario.SelectedValue.ToString().Trim(), tbxOtraTomadorBeneficiario.Text.Trim(), ddlAseguradoBeneficiario.SelectedValue.ToString().Trim(), tbxOtraAseguradoBeneficiario.Text.Trim(), ddlTipoCliente.SelectedValue.ToString().Trim(),
                tbxCiudad.Text.Trim(), ddlSucursal.SelectedValue.ToString().Trim(), ddlTipoSolicitud.SelectedValue.ToString().Trim()*/
                IdConocimientoCliente,
                Sanitizer.GetSafeHtmlFragment(tbxFechaFormulario.Text.Trim()), ddlClaseVinculacion.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtraClaseVinculacion.Text.Trim()), ddlTomadorAsegurado.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtraTomadorAsegurado.Text.Trim()),
                ddlTomadorBeneficiario.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtraTomadorBeneficiario.Text.Trim()), ddlAseguradoBeneficiario.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtraAseguradoBeneficiario.Text.Trim()), ddlTipoCliente.SelectedValue.ToString().Trim(),
                tbxCiudad.Text.Trim(), ddlSucursal.SelectedValue.ToString().Trim(), ddlTipoSolicitud.SelectedValue.ToString().Trim());

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA NATURAL")
                cKnowClient.InfoFormPN(
                    /*IdConocimientoCliente, tbxPNPrimerApellido.Text.Trim(), tbxPNSegunApellido.Text.Trim(), tbxPNNombres.Text.Trim(), ddlPNTipoDocumento.SelectedValue.ToString().Trim(), tbxPNNumeroDocumento.Text.Trim(),
                    tbxPNFechaExpedicion.Text.Trim(), tbxPNLugar.Text.Trim(), tbxPNFechaNacimiento.Text.Trim(), tbxPNNacionalidad.Text.Trim(), tbxPNOcupacionOficio.Text.Trim(), string.Empty, ddlPNActividadEconomica.Text.Trim(),
                    ddlPNCIIUDescripcion.SelectedValue.ToString().Trim(), tbxPNEmpresaTrabajo.Text.Trim(), tbxPNCargo.Text.Trim(), ddlPNCiudad1.SelectedItem.Text.ToString().Trim(), tbxPNTelefono1.Text.Trim(), tbxPNDireccionResidencia.Text.Trim(), ddlPNCiudad2.SelectedItem.Text.ToString().Trim(),
                    tbxPNTelefono2.Text.Trim(), tbxPNCelular.Text.Trim(), tbxPNIngresosMensuales.Text.Trim(), tbxPNActivos.Text.Trim(), tbxPNEgresoMensuales.Text.Trim(), tbxPNPasivos.Text.Trim(), tbxPNOtrosIngresos.Text.Trim(), tbxPNConceptoOtrosIngresos.Text.Trim(),
                    tbxPNCorreoElectronico.Text.Trim(), tbxPNLugarNmto.Text.Trim(), ddlPNDpto.SelectedItem.Text.ToString().Trim(), tbxPNOtraActEconomica.Text.Trim(), tbxPNPatrimonio.Text.Trim(), tbxPNDireccionEmpresa.Text.Trim(),
                    ddlPNCiudadEmpresa.SelectedItem.Text.ToString().Trim(), tbxPNTelefonoEmpresa.Text.Trim(), string.Empty,
                    ddlPNPregunta1.SelectedValue.ToString().Trim(), ddlPNPregunta2.SelectedValue.ToString().Trim(), ddlPNPregunta3.SelectedValue.ToString().Trim(), tbxPNEspecificacionPreguntas.Text.Trim(), ddlPNPregunta4.SelectedValue.ToString().Trim(),
                    ddlPNPregunta5.SelectedValue.ToString().Trim(), tbxPNEspecificacionPreguntas2.Text.Trim(), ddlPNSector1.SelectedValue.ToString().Trim(), ddlPNSector2.SelectedValue.ToString().Trim(), ddlPNDptoEmpresa.SelectedItem.Text.ToString().Trim(),
                    ddlPNDpto2.SelectedItem.Text.ToString().Trim(), tbxPNDireccion.Text.Trim(), tbxPNServicio.Text.Trim(), ddlPNCIIUDescripcion.SelectedItem.Text.ToString().Trim()*/
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
                cKnowClient.InfoFormPJ(/*IdConocimientoCliente,
                    tbxPJRazonDenominacion.Text.Trim(), tbxPJNIT.Text.Trim(), tbxPJPrimerApellido.Text.Trim(), tbxPJSegundoApellido.Text.Trim(), tbxPJNombres.Text.Trim(), ddlPJTipoDocumento.SelectedValue.ToString().Trim(), tbxPJNumeroDocumento.Text.Trim(), tbxPJLugarExpedicion.Text.Trim(), tbxPJFechaExpedicion.Text.Trim(),
                    tbxPJDireccionOficina.Text.Trim(), ddlPJCiudad1.SelectedItem.Text.ToString().Trim(), tbxPJTelefono1.Text.Trim(), tbxPJDireccionSucursal.Text.Trim(), ddlPJCiudad2.SelectedItem.Text.ToString().Trim(), tbxPJTelefono2.Text.Trim(), ddlPJTipoEmpresa.SelectedValue.ToString().Trim(), ddlPJActividadEconomica.SelectedValue.ToString().Trim(),
                    tbxPJCIIU.Text.Trim(), tbxPJIngresosMensuales.Text.Trim(), tbxPJActivos.Text.Trim(), tbxPJEgresoMensuales.Text.Trim(), tbxPJPasivos.Text.Trim(), tbxPJOtrosIngresos.Text.Trim(), tbxPJConceptoOtrosIngresos.Text.Trim(), tbxPJCorreoPrincipal.Text.Trim(), tbxPJFechaConstitucion.Text.Trim(), tbxPJNombresRepLegalPpal.Text.Trim(),
                    ddlPJTipoDocRepLegalPpal.SelectedValue.ToString().Trim(), tbxPJDocumentoRepLegalPpal.Text.Trim(), tbxPJNombresRepLegal1.Text.Trim(), ddlPJTipoDocRepLegal1.SelectedValue.ToString().Trim(), tbxPJDocumentoRepLegal1.Text.Trim(), tbxPJNombresRepLegal2.Text.Trim(), ddlPJTipoDocRepLegal2.SelectedValue.ToString().Trim(),
                    tbxPJDocumentoRepLegal2.Text.Trim(), tbxPJNombresRepLegal3.Text.Trim(), ddlPJTipoDocRepLegal3.SelectedValue.ToString().Trim(), tbxPJDocumentoRepLegal3.Text.Trim(), tbxPJNombresRepLegal4.Text.Trim(), ddlPJTipoDocRepLegal4.SelectedValue.ToString().Trim(), tbxPJDocumentoRepLegal4.Text.Trim(), ddlPJCodCIIU2.SelectedItem.Text.ToString().Trim(),
                    tbxPJNacionalidad1.Text.Trim(), tbxPJDV.Text.Trim(), ddlPJDpto.SelectedItem.Text.ToString().Trim(), ddlPJDpto2.SelectedItem.Text.ToString().Trim(),
                    ddlPJPregunta1.SelectedValue.ToString().Trim(), ddlPJPregunta2.SelectedValue.ToString().Trim(), ddlPJPregunta3.SelectedValue.ToString().Trim(), tbxPJEspecificacionPreguntas.Text.Trim(), ddlPJPregunta4.SelectedValue.ToString().Trim(),
                    ddlPJPregunta5.SelectedValue.ToString().Trim(), ddlPJPregunta6.SelectedValue.ToString().Trim(), ddlPJPregunta7.SelectedValue.ToString().Trim(), ddlPJPregunta8.SelectedValue.ToString().Trim(), ddlPJPregunta9.SelectedValue.ToString().Trim(), ddlPJPregunta10.SelectedValue.ToString().Trim(),
                    ddlPJPreguntaRep1Legal1.SelectedValue.ToString().Trim(), ddlPJPreguntaRep1Legal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRep1Legal3.SelectedValue.ToString().Trim(),
                    ddlPJPreguntaRep1Legal4.SelectedValue.ToString().Trim(), ddlPJPreguntaRep2Legal1.SelectedValue.ToString().Trim(), ddlPJPreguntaRep2Legal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRep2Legal3.SelectedValue.ToString().Trim(), ddlPJPreguntaRep2Legal4.SelectedValue.ToString().Trim(), ddlPJPreguntaRep3Legal1.SelectedValue.ToString().Trim(),
                    ddlPJPreguntaRep3Legal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRep3Legal3.SelectedValue.ToString().Trim(), ddlPJPreguntaRep3Legal4.SelectedValue.ToString().Trim(), ddlPJPreguntaRep4Legal1.SelectedValue.ToString().Trim(), ddlPJPreguntaRep4Legal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRep4Legal3.SelectedValue.ToString().Trim(),
                    ddlPJPreguntaRep4Legal4.SelectedValue.ToString().Trim(), ddlPJPreguntaRepPpalLegal1.SelectedValue.ToString().Trim(), ddlPJPreguntaRepPpalLegal2.SelectedValue.ToString().Trim(), ddlPJPreguntaRepPpalLegal3.SelectedValue.ToString().Trim(), ddlPJPreguntaRepPpalLegal4.SelectedValue.ToString().Trim(),
                    tbxPJNIT1.Text.Trim(), tbxPJPais1.Text.Trim(), tbxPJNIT2.Text.Trim(), tbxPJPais2.Text.Trim(), tbxPJNIT3.Text.Trim(), tbxPJPais3.Text.Trim(), tbxPJNIT4.Text.Trim(), tbxPJPais4.Text.Trim(), tbxPJDireccionFiscal1.Text.Trim(), tbxPJDireccionFiscal2.Text.Trim(),
                    tbxPJParticipacion1.Text.Trim(), tbxPJParticipacion2.Text.Trim(), tbxPJParticipacion3.Text.Trim(), tbxPJParticipacion4.Text.Trim(), tbxPJParticipacion5.Text.Trim(),
                    ddlPJCotizaBolsa.SelectedValue.ToString().Trim(), ddlPJEstatal.SelectedValue.ToString().Trim(), ddlPJSinAnimoLucro.SelectedValue.ToString().Trim(), ddlPJSubsidiaria.SelectedValue.ToString().Trim(), tbxPJSocMatriz.Text.Trim(), tbxPJSocMatrizIdenTrib.Text.Trim(), tbxPJSocMatrizJurisdiccion.Text.Trim(), tbxPJSocMatrizDireccion.Text.Trim(), tbxPJSocMatrizCiudad.Text.Trim(), tbxPJSocMatrizTelefono.Text.Trim(),
                    tbxPJLugarNmto.Text.Trim()*/
                    IdConocimientoCliente,
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
                /*IdConocimientoCliente, ddlTransacMonedaExtra.SelectedValue.ToString().Trim(), ddlTipoTransaccion.SelectedValue.ToString().Trim(), tbxOtroTipoTransaccion.Text.Trim(), ddlPFProdExterior.SelectedValue.ToString().Trim(),
                tbxPFTipoProducto1.Text.Trim(), tbxPFNumeroProducto1.Text.Trim(), tbxPFEntidad1.Text.Trim(), tbxPFMonto1.Text.Trim(), tbxPFCiudad1.Text.Trim(), tbxPFPais1.Text.Trim(), tbxPFMoneda1.Text.Trim(),
                tbxPFTipoProducto2.Text.Trim(), tbxPFNumeroProducto2.Text.Trim(), tbxPFEntidad2.Text.Trim(), tbxPFMonto2.Text.Trim(), tbxPFCiudad2.Text.Trim(), tbxPFPais2.Text.Trim(), tbxPFMoneda2.Text.Trim(),
                tbxPFTipoProducto3.Text.Trim(), tbxPFNumeroProducto3.Text.Trim(), tbxPFEntidad3.Text.Trim(), tbxPFMonto3.Text.Trim(), tbxPFCiudad3.Text.Trim(), tbxPFPais3.Text.Trim(), tbxPFMoneda3.Text.Trim(),
                ddlPFCtaMonedaExtra.SelectedValue.ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty*/
                IdConocimientoCliente, ddlTransacMonedaExtra.SelectedValue.ToString().Trim(), ddlTipoTransaccion.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxOtroTipoTransaccion.Text.Trim()), ddlPFProdExterior.SelectedValue.ToString().Trim(),
                Sanitizer.GetSafeHtmlFragment(tbxPFTipoProducto1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFNumeroProducto1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFEntidad1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMonto1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFCiudad1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFPais1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMoneda1.Text.Trim()),
                Sanitizer.GetSafeHtmlFragment(tbxPFTipoProducto2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFNumeroProducto2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFEntidad2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMonto2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFCiudad2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFPais2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMoneda2.Text.Trim()),
                Sanitizer.GetSafeHtmlFragment(tbxPFTipoProducto3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFNumeroProducto3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFEntidad3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMonto3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFCiudad3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFPais3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxPFMoneda3.Text.Trim()),
                ddlPFCtaMonedaExtra.SelectedValue.ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty
                );

            cKnowClient.InfoFormSeguros(/*IdConocimientoCliente,
                tbxSeguroAno1.Text.Trim(), tbxSeguroRamo1.Text.Trim(), tbxSeguroCompania1.Text.Trim(), tbxSeguroValor1.Text.Trim(), ddlSeguroTipo1.SelectedValue.ToString().Trim(),
                tbxSeguroAno2.Text.Trim(), tbxSeguroRamo2.Text.Trim(), tbxSeguroCompania2.Text.Trim(), tbxSeguroValor2.Text.Trim(), ddlSeguroTipo2.SelectedValue.ToString().Trim(), 
                tbxOrigenFondos.Text.Trim(), ddlReclamaciones.SelectedValue.ToString().Trim()*/
                IdConocimientoCliente,
                Sanitizer.GetSafeHtmlFragment(tbxSeguroAno1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroRamo1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroCompania1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroValor1.Text.Trim()), ddlSeguroTipo1.SelectedValue.ToString().Trim(),
                Sanitizer.GetSafeHtmlFragment(tbxSeguroAno2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroRamo2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroCompania2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxSeguroValor2.Text.Trim()), ddlSeguroTipo2.SelectedValue.ToString().Trim(),
                Sanitizer.GetSafeHtmlFragment(tbxOrigenFondos.Text.Trim()), ddlReclamaciones.SelectedValue.ToString().Trim()
                );

            cKnowClient.InfoFormEntrevista(/*IdConocimientoCliente,
                tbxLugarEntrevista.Text.Trim(), tbxFechaEntrevista.Text.Trim(), tbxHoraEntrevista.Text.Trim(), ddlResultado.SelectedValue.ToString().Trim(), tbxObservaciones1.Text.Trim(),
                tbxNombreResponsable.Text.Trim(), tbxFechaVerificacion.Text.Trim(), tbxHoraVerificacion.Text.Trim(), tbxNombreVerifica.Text.Trim(), tbxObservaciones2.Text.Trim(),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, tbxNombreIntermediario.Text*/
                IdConocimientoCliente,
               Sanitizer.GetSafeHtmlFragment(tbxLugarEntrevista.Text), Sanitizer.GetSafeHtmlFragment(tbxFechaEntrevista.Text), Sanitizer.GetSafeHtmlFragment(tbxHoraEntrevista.Text), ddlResultado.Text, Sanitizer.GetSafeHtmlFragment(tbxObservaciones1.Text),
                Sanitizer.GetSafeHtmlFragment(tbxNombreResponsable.Text), Sanitizer.GetSafeHtmlFragment(tbxFechaVerificacion.Text), Sanitizer.GetSafeHtmlFragment(tbxHoraVerificacion.Text), Sanitizer.GetSafeHtmlFragment(tbxNombreVerifica.Text), Sanitizer.GetSafeHtmlFragment(tbxObservaciones2.Text),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Sanitizer.GetSafeHtmlFragment(tbxNombreIntermediario.Text)
            );

            Button5.Visible = true;
            Button6.Visible = false;
            Button1.Visible = false;
            Button2.Visible = true;
            Button3.Visible = false;
            Button4.Visible = true;
            tbFormulario.Visible = true;
            tbDocumentos.Visible = false;
            disableCampos();
            loadGridArchivos();
        }

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
                    lblTituloFormulario.Text = "FORMULARIO DE CONOCIMIENTO DEL CLIENTE";
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
                    lblTituloFormulario.Text = "FORMULARIO DE CONOCIMIENTO DEL CLIENTE PERSONA NATURAL - SECTOR ASEGURADOR";
                    lblSubTituloFormulario.Text = "DIPOSICIONES CONTENIDAS EN LA CIRCULAR BASICA JURIDICA – SUPERINTENDENCIA FINANCIERA DE COLOMBIA";
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
                    lblTituloFormulario.Text = "FORMULARIO DE CONOCIMIENTO DEL CLIENTE PERSONA JURIDICA - SECTOR ASEGURADOR";
                    lblSubTituloFormulario.Text = "DIPOSICIONES CONTENIDAS EN LA CIRCULAR BASICA JURIDICA – SUPERINTENDENCIA FINANCIERA DE COLOMBIA";
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

            infoAutorizacion.Visible = booVisible;
            infoTituloClausula.Visible = booVisible;
            infoClausula.Visible = booVisible;
            InfoFirmaHuella1.Visible = booVisible;
            InfoFirmaHuella2.Visible = booVisible;
            infoEntrevista.Visible = booVisible;

            mtdHabilitaVinculos(0, false);
            mtdHabilitaVinculos(1, false);
            mtdHabilitaVinculos(2, false);
            mtdHabilitaVinculacion(false);
            mtdHabilitaMoneda(false);
        }

        #region Vinculos
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
        #endregion

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
        protected void ddlClaseVinculacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlClaseVinculacion.SelectedValue.ToString().Trim())
            {
                case "OTRO":
                    mtdHabilitaVinculacion(true);
                    tbxOtraClaseVinculacion.Focus();
                    break;
                default:
                    mtdHabilitaVinculacion(false);
                    tbxOtraClaseVinculacion.Focus();
                    break;
            }
        }

        void mtdHabilitaMoneda(bool booVisible)
        {
            lblTipoTransaccion.Visible = booVisible;
            ddlTipoTransaccion.Visible = booVisible;
            rfvTipoTransaccion.Enabled= booVisible;
            //if (booVisible)
            //    rfvTipoTransaccion.ValidationGroup = "vdgWillis";
            //else
            //    rfvTipoTransaccion.ValidationGroup = "";
        }

        protected void ddlTransacMonedaExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(ddlTransacMonedaExtra.SelectedValue.ToString().Trim())
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
            if(tbxPNOtrosIngresos.Text != "")
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
                    break;
            }
        }

        void mtdHabilitaPregunta4(bool booVisible)
        {
            lblPNEspecificacionPreguntas.Visible = booVisible;
            tbxPNEspecificacionPreguntas.Visible = booVisible;
            rfvPNEspecificacionPreguntas.Enabled = booVisible;
            rfvPrimerApellidoPNPPE.Enabled = booVisible;
            rfvSegundoApellidoPNPPE.Enabled = booVisible;
            rfvNombrePNPPE.Enabled = booVisible;
            rfvOcupacionPNPPE.Enabled = booVisible;
            rfvCargoPNPPE.Enabled = booVisible;
            rfvPNEspecificacionPreguntas.Enabled = booVisible;
            if (!booVisible)
                tbxPNEspecificacionPreguntas.Text = "";
            
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

        /*protected void ddlPJPregunta4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlPJPregunta4.SelectedValue == "SI")
            TrVinculoPPExpuestaPJ.Visible = true;
        }*/

        protected void ddlTipoActividadPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTipoActividadPN.SelectedValue == "15")
            {
                tdOtroTipoActividadPN.Visible = true;
                tdValorOtroTipoActividadPN.Visible = true;
            }
            else
            {
                tdOtroTipoActividadPN.Visible = false;
                tdValorOtroTipoActividadPN.Visible = false;
            }
        }

        protected void ddlPJPregunta4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlPJPregunta4.SelectedValue != "SI")
            {
                rfvEspefificacionP4.Enabled = false;
                tbxPJEspecificacionPreguntas.Visible = false;
                lblPJEspecificacionPreguntas.Visible = false;
            }
            else
            {
                rfvEspefificacionP4.Enabled = true;
                tbxPJEspecificacionPreguntas.Visible = true;
                lblPJEspecificacionPreguntas.Visible = true;
            }
        }
    }
}
