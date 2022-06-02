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

namespace ListasSarlaft.UserControls.Sarlaft
{
    public partial class FormularioConocimientoCliente : System.Web.UI.UserControl
    {
        string IdFormulario = "6010";
        private cKnowClient cKnowClient = new cKnowClient();
        private cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(GridView2);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            
        }
        #region Properties
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;
        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;

        private DataTable InfoGrid1
        {
            get
            {
                infoGrid1 = (DataTable)ViewState["infoGrid1"];
                return infoGrid1;
            }
            set
            {
                infoGrid1 = value;
                ViewState["infoGrid1"] = infoGrid1;
            }
        }

        private int RowGrid1
        {
            get
            {
                rowGrid1 = (int)ViewState["rowGrid1"];
                return rowGrid1;
            }
            set
            {
                rowGrid1 = value;
                ViewState["rowGrid1"] = rowGrid1;
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
        #endregion
        protected void btnSearchCarac_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    mtdLoadFormulario();                    
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
        }
        #region Loads
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridFormulario()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("IdRegistro", typeof(string));
            grid.Columns.Add("IdConocimientoCliente", typeof(string));
            grid.Columns.Add("NumeroDocumento", typeof(string));
            grid.Columns.Add("PrimerApellido", typeof(string));
            grid.Columns.Add("SegundoApellido", typeof(string));
            grid.Columns.Add("Nombres", typeof(string));
            grid.Columns.Add("estado", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));

            GVformularioCliente.DataSource = grid;
            GVformularioCliente.DataBind();
            InfoGrid1 = grid;
        }
        private void mtdLoadFormulario()
        {
            DataTable dtInfo = new DataTable();

            try
            {
                string NumeroDocumento = Sanitizer.GetSafeHtmlFragment(TXidentificacion.Text);
                int Tipo = Convert.ToInt32(DDLtipoPersona.SelectedIndex.ToString());
                if (Tipo == 2)
                {
                    dtInfo = cKnowClient.loadInfoGridCliente(NumeroDocumento);
                    mtdLoadGridFormulario();
                    if (dtInfo.Rows.Count > 0)
                    {
                        for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                        {
                            InfoGrid1.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdConocimientoCliente"].ToString().Trim(),
                        dtInfo.Rows[rows]["NumeroDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["PrimerApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["SegundoApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                        dtInfo.Rows[rows]["estado"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim()
                        });
                        }
                        GVformularioCliente.DataSource = InfoGrid1;
                        GVformularioCliente.DataBind();
                        GVformularioCliente.Visible = true;
                    }
                }
                else
                {
                    dtInfo = cKnowClient.loadInfoGridJudicial(NumeroDocumento);
                    mtdLoadGridFormulario();
                    if (dtInfo.Rows.Count > 0)
                    {
                        for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                        {
                            InfoGrid1.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdConocimientoCliente"].ToString().Trim(),
                        dtInfo.Rows[rows]["NumeroDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["PrimerApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["SegundoApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                        dtInfo.Rows[rows]["estado"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim()
                        });
                        }
                        GVformularioCliente.DataSource = InfoGrid1;
                        GVformularioCliente.DataBind();
                        GVformularioCliente.Visible = true;
                    }
                }
                //ddlEmpresa.DataBind();
                TBgridCliente.Visible = true;
            }
            catch (Exception ex)
            {
                //Mensaje("Error al cargar Empresas. " + ex.Message);
                omb.ShowMessage("Error al consultar formuario conocimiento cliente: "+ ex, 2, "Atención");
            }
        }
        #endregion

        protected void GVformularioCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid1 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowData(RowGrid1);
                    break;
            }
        }
        private void mtdShowData(int RowGrid1)
        {
            GridViewRow row = GVformularioCliente.Rows[RowGrid1];
            var colsNoVisible = GVformularioCliente.DataKeys[RowGrid1].Values;
            Session["IdRegistro"] = row.Cells[0].Text;
            Session["IdConocimientoCliente"] = colsNoVisible[0].ToString();

            IdConocimientoCliente =Convert.ToInt32(colsNoVisible[0].ToString());
            cargarInfo();
            Boolean booVisible = true;
            loadGridArchivos();
            loadInfoArchivos();
            TBFormularioNatural.Visible = booVisible;
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
        }
        #region LoadFormulario
        private void cargarInfo()
        {
            DataTable dtInfo = new DataTable();
            #region Encabezado
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente, false);
            tbxFechaFormulario.Text = dtInfo.Rows[0]["FechaFormulario"].ToString().Trim();
            tbxFechaFormulario.Enabled = false;
            tbxOtraClaseVinculacion.Text = dtInfo.Rows[0]["OtraClaseVinculacion"].ToString().Trim();
            tbxOtraClaseVinculacion.Enabled = false;
            tbxOtraTomadorAsegurado.Text = dtInfo.Rows[0]["OtraTomadorAsegurado"].ToString().Trim();
            tbxOtraTomadorAsegurado.Enabled = false;
            tbxOtraTomadorBeneficiario.Text = dtInfo.Rows[0]["OtraTomadorBeneficiario"].ToString().Trim();
            tbxOtraTomadorBeneficiario.Enabled = false;
            tbxOtraAseguradoBeneficiario.Text = dtInfo.Rows[0]["OtraAseguradoBeneficiario"].ToString().Trim();
            tbxOtraAseguradoBeneficiario.Enabled = false;
            tbxCiudad.Text = dtInfo.Rows[0]["Ciudad"].ToString().Trim();
            tbxCiudad.Enabled = false;
            for (int i = 0; i < ddlClaseVinculacion.Items.Count; i++) { ddlClaseVinculacion.SelectedIndex = i; if (ddlClaseVinculacion.SelectedItem.Text.Trim() == dtInfo.Rows[0]["ClaseVinculacion"].ToString().Trim()) break; }
            for (int i = 0; i < ddlTomadorAsegurado.Items.Count; i++) { ddlTomadorAsegurado.SelectedIndex = i; if (ddlTomadorAsegurado.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TomadorAsegurado"].ToString().Trim()) break; }
            for (int i = 0; i < ddlTomadorBeneficiario.Items.Count; i++) { ddlTomadorBeneficiario.SelectedIndex = i; if (ddlTomadorBeneficiario.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TomadorBeneficiario"].ToString().Trim()) break; }
            for (int i = 0; i < ddlAseguradoBeneficiario.Items.Count; i++) { ddlAseguradoBeneficiario.SelectedIndex = i; if (ddlAseguradoBeneficiario.SelectedItem.Text.Trim() == dtInfo.Rows[0]["AseguradoBeneficiario"].ToString().Trim()) break; }
            for (int i = 0; i < ddlSucursal.Items.Count; i++) { ddlSucursal.SelectedIndex = i; if (ddlSucursal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Sucursal"].ToString().Trim()) break; }
            for (int i = 0; i < ddlTipoSolicitud.Items.Count; i++) { ddlTipoSolicitud.SelectedIndex = i; if (ddlTipoSolicitud.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TipoSolicitud"].ToString().Trim()) break; }

            ddlClaseVinculacion.Enabled = false;
            if (ddlClaseVinculacion.SelectedValue == "OTRO" || ddlClaseVinculacion.SelectedValue == "OTRA")
            {
                tbxOtraClaseVinculacion.Visible = true;
                lblOtraVinculacion.Visible = true;
            }
            ddlTomadorAsegurado.Enabled = false;
            if (ddlTomadorAsegurado.SelectedValue == "OTRO" || ddlTomadorAsegurado.SelectedValue == "OTRA")
            {
                tbxOtraTomadorAsegurado.Visible = true;
                lblOtraTomadorAsegurado.Visible = true;
            }
            ddlTomadorBeneficiario.Enabled = false;
            if (ddlTomadorBeneficiario.SelectedValue == "OTRO" || ddlTomadorBeneficiario.SelectedValue == "OTRA")
            {
                tbxOtraTomadorBeneficiario.Visible = true;
                lblOtraTomadorBeneficiario.Visible = true;
            }
            ddlAseguradoBeneficiario.Enabled = false;
            if (ddlAseguradoBeneficiario.SelectedValue == "OTRO" || ddlAseguradoBeneficiario.SelectedValue == "OTRA")
            {
                tbxOtraAseguradoBeneficiario.Visible = true;
                lblOtraAseguradoBeneficiario.Visible = true;
            }
            ddlSucursal.Enabled = false;
            ddlTipoSolicitud.Enabled = false;
            #endregion
            if (DDLtipoPersona.SelectedValue.ToString() == "2")
            {
            //enableCampos();

            #region Persona Natural
            dtInfo = cKnowClient.InfoFormPN(IdConocimientoCliente);

                tbxPNPrimerApellido.Text = dtInfo.Rows[0]["PNPrimerApellido"].ToString().Trim();
                tbxPNPrimerApellido.Enabled = false;
                tbxPNSegunApellido.Text = dtInfo.Rows[0]["PNSegunApellido"].ToString().Trim();
                tbxPNSegunApellido.Enabled = false;
                tbxPNNombres.Text = dtInfo.Rows[0]["PNNombres"].ToString().Trim();
                tbxPNNombres.Enabled = false;
                tbxPNNumeroDocumento.Text = dtInfo.Rows[0]["PNNumeroDocumento"].ToString().Trim();
                tbxPNNumeroDocumento.Enabled = false;
                tbxPNFechaExpedicion.Text = dtInfo.Rows[0]["PNFechaExpedicion"].ToString().Trim();
                tbxPNFechaExpedicion.Enabled = false;
                tbxPNLugar.Text = dtInfo.Rows[0]["PNLugar"].ToString().Trim();
                tbxPNLugar.Enabled = false;
                tbxPNFechaNacimiento.Text = dtInfo.Rows[0]["PNFechaNacimiento"].ToString().Trim();
                tbxPNFechaNacimiento.Enabled = false;
                tbxPNNacionalidad.Text = dtInfo.Rows[0]["PNNacionalidad"].ToString().Trim();
                tbxPNNacionalidad.Enabled = false;
                tbxPNOcupacionOficio.Text = dtInfo.Rows[0]["PNOcupacionOficio"].ToString().Trim();
                tbxPNOcupacionOficio.Enabled = false;
                tbxPNCIIU.Text = dtInfo.Rows[0]["PNCIIU"].ToString().Trim();
                tbxPNCIIU.Enabled = false;
                tbxPNEmpresaTrabajo.Text = dtInfo.Rows[0]["PNEmpresaTrabajo"].ToString().Trim();
                tbxPNEmpresaTrabajo.Enabled = false;
                tbxPNCargo.Text = dtInfo.Rows[0]["PNCargo"].ToString().Trim();
                tbxPNCargo.Enabled = false;
                
                tbxPNDireccion.Text = dtInfo.Rows[0]["PNDireccion"].ToString().Trim();
                tbxPNDireccion.Enabled = false;
                tbxPNTelefono1.Text = dtInfo.Rows[0]["PNTelefono1"].ToString().Trim();
                tbxPNTelefono1.Enabled = false;
                tbxPNDireccionResidencia.Text = dtInfo.Rows[0]["PNDireccionResidencia"].ToString().Trim();
                tbxPNDireccionResidencia.Enabled = false;
                tbxPNTelefono2.Text = dtInfo.Rows[0]["PNTelefono2"].ToString().Trim();
                tbxPNTelefono2.Enabled = false;
                tbxPNCelular.Text = dtInfo.Rows[0]["PNCelular"].ToString().Trim();
                tbxPNCelular.Enabled = false;
                tbxPNEspecificacionPreguntas.Text = dtInfo.Rows[0]["PNEspecificacionPreguntas"].ToString().Trim();
                tbxPNEspecificacionPreguntas.Enabled = false;
                tbxPNIngresosMensuales.Text = dtInfo.Rows[0]["PNIngresosMensuales"].ToString().Trim();
                tbxPNIngresosMensuales.Enabled = false;
                tbxPNActivos.Text = dtInfo.Rows[0]["PNActivos"].ToString().Trim();
                tbxPNActivos.Enabled = false;
                tbxPNEgresoMensuales.Text = dtInfo.Rows[0]["PNEgresoMensuales"].ToString().Trim();
                tbxPNEgresoMensuales.Enabled = false;
                tbxPNPasivos.Text = dtInfo.Rows[0]["PNPasivos"].ToString().Trim();
                tbxPNPasivos.Enabled = false;
                tbxPNOtrosIngresos.Text = dtInfo.Rows[0]["PNOtrosIngresos"].ToString().Trim();
                tbxPNOtrosIngresos.Enabled = false;
                tbxPNConceptoOtrosIngresos.Text = dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString().Trim();
                tbxPNConceptoOtrosIngresos.Enabled = false;
                tbxPNCorreoElectronico.Text = dtInfo.Rows[0]["PNCorreoElectronico"].ToString().Trim();
                tbxPNCorreoElectronico.Enabled = false;
                tbxPNOtraActEconomica.Text = dtInfo.Rows[0]["PNOtraActEconomica"].ToString().Trim();
                tbxPNOtraActEconomica.Enabled = false;
                tbxPNPatrimonio.Text = dtInfo.Rows[0]["PNPatrimonio"].ToString().Trim();
                tbxPNPatrimonio.Enabled = false;
                tbxPNDireccionEmpresa.Text = dtInfo.Rows[0]["PNDireccionEmpresa"].ToString().Trim();
                tbxPNDireccionEmpresa.Enabled = false;
                tbxPNTelefonoEmpresa.Text = dtInfo.Rows[0]["PNTelefonoEmpresa"].ToString().Trim();
                tbxPNTelefonoEmpresa.Enabled = false;
                tbxPNEspecificacionPreguntas2.Text = dtInfo.Rows[0]["PNEspecificacionPreguntas2"].ToString().Trim();
                tbxPNEspecificacionPreguntas2.Enabled = false;
                tbxPNLugarNmto.Text = dtInfo.Rows[0]["PNLugarNmto"].ToString().Trim();
                tbxPNLugarNmto.Enabled = false;
                tbxPNServicio.Text = dtInfo.Rows[0]["PNServicio"].ToString().Trim();
                tbxPNServicio.Enabled = false;
                TXvalorOtroTipoActividadPN.Text = dtInfo.Rows[0]["PNTipoActividadOtra"].ToString().Trim();
                TXvalorOtroTipoActividadPN.Enabled = false;
                TXprimerApellidoPNPPE.Text = dtInfo.Rows[0]["PNpePrimerApellido"].ToString().Trim();
                TXprimerApellidoPNPPE.Enabled = false;
                TXsegundApellidoPNPPE.Text = dtInfo.Rows[0]["PNpeSegundoApellido"].ToString().Trim();
                TXsegundApellidoPNPPE.Enabled = false;
                TXnombrePNPPE.Text = dtInfo.Rows[0]["PNpeNombres"].ToString().Trim();
                TXnombrePNPPE.Enabled = false;
                TXocupacionPNPPE.Text = dtInfo.Rows[0]["PNpeOcupacion"].ToString().Trim();
                TXocupacionPNPPE.Enabled = false;
                TXcargoPNPPE.Text = dtInfo.Rows[0]["PNpeCargo"].ToString().Trim();
                TXcargoPNPPE.Enabled = false;
                for (int i = 0; i < ddlPNTipoDocumento.Items.Count; i++) { ddlPNTipoDocumento.SelectedIndex = i; if (ddlPNTipoDocumento.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNTipoDocumento"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNActividadEconomica.Items.Count; i++) { ddlPNActividadEconomica.SelectedIndex = i; if (ddlPNActividadEconomica.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNActividadEconomica"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNCIIUDescripcion.Items.Count; i++) { ddlPNCIIUDescripcion.SelectedIndex = i; if (ddlPNCIIUDescripcion.SelectedValue.ToString().Trim() == dtInfo.Rows[0]["PNCIIU"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNPregunta1.Items.Count; i++) { ddlPNPregunta1.SelectedIndex = i; if (ddlPNPregunta1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNPregunta2.Items.Count; i++) { ddlPNPregunta2.SelectedIndex = i; if (ddlPNPregunta2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNPregunta3.Items.Count; i++) { ddlPNPregunta3.SelectedIndex = i; if (ddlPNPregunta3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta3"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNPregunta4.Items.Count; i++) { ddlPNPregunta4.SelectedIndex = i; if (ddlPNPregunta4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta4"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNPregunta5.Items.Count; i++) { ddlPNPregunta5.SelectedIndex = i; if (ddlPNPregunta5.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta5"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNSector1.Items.Count; i++) { ddlPNSector1.SelectedIndex = i; if (ddlPNSector1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNSector1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPNSector2.Items.Count; i++) { ddlPNSector2.SelectedIndex = i; if (ddlPNSector2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNSector2"].ToString().Trim()) break; }
                ddlPNTipoDocumento.Enabled = false;
                ddlPNActividadEconomica.Enabled = false;
                ddlPNCIIUDescripcion.Enabled = false;
                ddlPNPregunta1.Enabled = false;
                ddlPNPregunta2.Enabled = false;
                ddlPNPregunta3.Enabled = false;
                ddlPNPregunta4.Enabled = false;
                ddlPNPregunta5.Enabled = false;
                ddlPNSector1.Enabled = false;
                ddlPNSector2.Enabled = false;
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
                ddlPNDpto.Enabled = false;
                ddlPNCiudad1.Enabled = false;

                #endregion
                #region Departamento y Ciudad 2
                if (dtInfo.Rows[0]["PNDpto"].ToString().Trim() != string.Empty)
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
                ddlPNDpto2.Enabled = false;
                ddlPNCiudad2.Enabled = false;
                #endregion
                #region Departamento y Ciudad Empresa
                if (dtInfo.Rows[0]["PNDpto"].ToString().Trim() != string.Empty)
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
                ddlPNDptoEmpresa.Enabled = false;
                ddlPNCiudadEmpresa.Enabled = false;
                #endregion
                for (int i = 0; i < ddlPNCIIUDescripcion.Items.Count; i++)
                {
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

                    if (ddlTipoActividadPN.SelectedValue.ToString().Trim() == dtInfo.Rows[0]["PNTipoActividad"].ToString().Trim())
                        break;
                    else
                        ddlTipoActividadPN.SelectedIndex = 0;
                }
                DDLcodCIIU.SelectedValue = ddlPNCIIUDescripcion.SelectedValue.ToString();
                DDLcodCIIU.Enabled = false;
                ddlTipoActividadPN.Enabled = false;
                infoTituloPN.Visible = true;
                infoPN.Visible = true;
                if (dtInfo.Rows[0]["PNPregunta4"].ToString().Trim() == "SI")
                    TrVinculoPPExpuestaPN.Visible = true;
                #endregion

            }
            else
            {
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
                tbxPJSocMatriz.Text = dtInfo.Rows[0]["PJSocMatriz"].ToString().Trim();
                tbxPJSocMatrizIdenTrib.Text = dtInfo.Rows[0]["PJSocMatrizIdenTrib"].ToString().Trim();
                tbxPJSocMatrizJurisdiccion.Text = dtInfo.Rows[0]["PJSocMatrizJurisdiccion"].ToString().Trim();
                tbxPJSocMatrizDireccion.Text = dtInfo.Rows[0]["PJSocMatrizDireccion"].ToString().Trim();
                tbxPJSocMatrizCiudad.Text = dtInfo.Rows[0]["PJSocMatrizCiudad"].ToString().Trim();
                tbxPJSocMatrizTelefono.Text = dtInfo.Rows[0]["PJSocMatrizTelefono"].ToString().Trim();

                ddlTipoDocumentoPJempresa.SelectedValue = dtInfo.Rows[0]["PJTipoDocumentoEmpresa"].ToString().Trim();
                ddlTipoDocumentoPJempresa.Enabled = false;
                TXprimerApellidoPPE.Text = dtInfo.Rows[0]["PJpePrimerApellido"].ToString().Trim();
                TXprimerApellidoPPE.Enabled = false;
                TXsegundoApellidoPPE.Text = dtInfo.Rows[0]["PJpeSegundoApellido"].ToString().Trim();
                TXsegundoApellidoPPE.Enabled = false;
                TXnombresPPE.Text = dtInfo.Rows[0]["PJpeNombres"].ToString().Trim();
                TXnombresPPE.Enabled = false;
                TXocipacionPPE.Text = dtInfo.Rows[0]["PJpeOcupacion"].ToString().Trim();
                TXocipacionPPE.Enabled = false;
                TXcargoPPE.Text = dtInfo.Rows[0]["PJpeCargo"].ToString().Trim();
                TXcargoPPE.Enabled = false;
                if (dtInfo.Rows[0]["PJPregunta4"].ToString().Trim() == "SI")
                    TrVinculoPPExpuestaPJ.Visible = true;

                #region Carga DB
                ddlPJDpto.DataBind();
                ddlPJDpto2.DataBind();
                ddlPJCodCIIU2.DataBind();
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

                for (int i = 0; i < ddlPJTipoDocumento.Items.Count; i++) { ddlPJTipoDocumento.SelectedIndex = i; if (ddlPJTipoDocumento.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocumento"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJTipoEmpresa.Items.Count; i++) { ddlPJTipoEmpresa.SelectedIndex = i; if (ddlPJTipoEmpresa.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoEmpresa"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJActividadEconomica.Items.Count; i++) { ddlPJActividadEconomica.SelectedIndex = i; if (ddlPJActividadEconomica.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJActividadEconomica"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJTipoDocRepLegalPpal.Items.Count; i++) { ddlPJTipoDocRepLegalPpal.SelectedIndex = i; if (ddlPJTipoDocRepLegalPpal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegalPpal"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJTipoDocRepLegal1.Items.Count; i++) { ddlPJTipoDocRepLegal1.SelectedIndex = i; if (ddlPJTipoDocRepLegal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJTipoDocRepLegal2.Items.Count; i++) { ddlPJTipoDocRepLegal2.SelectedIndex = i; if (ddlPJTipoDocRepLegal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJTipoDocRepLegal3.Items.Count; i++) { ddlPJTipoDocRepLegal3.SelectedIndex = i; if (ddlPJTipoDocRepLegal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal3"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJTipoDocRepLegal4.Items.Count; i++) { ddlPJTipoDocRepLegal4.SelectedIndex = i; if (ddlPJTipoDocRepLegal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal4"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJCodCIIU2.Items.Count; i++) { ddlPJCodCIIU2.SelectedIndex = i; if (ddlPJCodCIIU2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJCodCIIU2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta1.Items.Count; i++) { ddlPJPregunta1.SelectedIndex = i; if (ddlPJPregunta1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta2.Items.Count; i++) { ddlPJPregunta2.SelectedIndex = i; if (ddlPJPregunta2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta3.Items.Count; i++) { ddlPJPregunta3.SelectedIndex = i; if (ddlPJPregunta3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta3"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta4.Items.Count; i++) { ddlPJPregunta4.SelectedIndex = i; if (ddlPJPregunta4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta4"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta5.Items.Count; i++) { ddlPJPregunta5.SelectedIndex = i; if (ddlPJPregunta5.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta5"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta6.Items.Count; i++) { ddlPJPregunta6.SelectedIndex = i; if (ddlPJPregunta6.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta6"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta7.Items.Count; i++) { ddlPJPregunta7.SelectedIndex = i; if (ddlPJPregunta7.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta7"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta8.Items.Count; i++) { ddlPJPregunta8.SelectedIndex = i; if (ddlPJPregunta8.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta8"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta9.Items.Count; i++) { ddlPJPregunta9.SelectedIndex = i; if (ddlPJPregunta9.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta9"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPregunta10.Items.Count; i++) { ddlPJPregunta10.SelectedIndex = i; if (ddlPJPregunta10.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPregunta10"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep1Legal1.Items.Count; i++) { ddlPJPreguntaRep1Legal1.SelectedIndex = i; if (ddlPJPreguntaRep1Legal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep1Legal1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep1Legal2.Items.Count; i++) { ddlPJPreguntaRep1Legal2.SelectedIndex = i; if (ddlPJPreguntaRep1Legal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep1Legal2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep1Legal3.Items.Count; i++) { ddlPJPreguntaRep1Legal3.SelectedIndex = i; if (ddlPJPreguntaRep1Legal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep1Legal3"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep1Legal4.Items.Count; i++) { ddlPJPreguntaRep1Legal4.SelectedIndex = i; if (ddlPJPreguntaRep1Legal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep1Legal4"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep2Legal1.Items.Count; i++) { ddlPJPreguntaRep2Legal1.SelectedIndex = i; if (ddlPJPreguntaRep2Legal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep2Legal1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep2Legal2.Items.Count; i++) { ddlPJPreguntaRep2Legal2.SelectedIndex = i; if (ddlPJPreguntaRep2Legal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep2Legal2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep2Legal3.Items.Count; i++) { ddlPJPreguntaRep2Legal3.SelectedIndex = i; if (ddlPJPreguntaRep2Legal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep2Legal3"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep2Legal4.Items.Count; i++) { ddlPJPreguntaRep2Legal4.SelectedIndex = i; if (ddlPJPreguntaRep2Legal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep2Legal4"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep3Legal1.Items.Count; i++) { ddlPJPreguntaRep3Legal1.SelectedIndex = i; if (ddlPJPreguntaRep3Legal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep3Legal1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep3Legal2.Items.Count; i++) { ddlPJPreguntaRep3Legal2.SelectedIndex = i; if (ddlPJPreguntaRep3Legal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep3Legal2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep3Legal3.Items.Count; i++) { ddlPJPreguntaRep3Legal3.SelectedIndex = i; if (ddlPJPreguntaRep3Legal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep3Legal3"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep3Legal4.Items.Count; i++) { ddlPJPreguntaRep3Legal4.SelectedIndex = i; if (ddlPJPreguntaRep3Legal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep3Legal4"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep4Legal1.Items.Count; i++) { ddlPJPreguntaRep4Legal1.SelectedIndex = i; if (ddlPJPreguntaRep4Legal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep4Legal1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep4Legal2.Items.Count; i++) { ddlPJPreguntaRep4Legal2.SelectedIndex = i; if (ddlPJPreguntaRep4Legal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep4Legal2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep4Legal3.Items.Count; i++) { ddlPJPreguntaRep4Legal3.SelectedIndex = i; if (ddlPJPreguntaRep4Legal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep4Legal3"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRep4Legal4.Items.Count; i++) { ddlPJPreguntaRep4Legal4.SelectedIndex = i; if (ddlPJPreguntaRep4Legal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRep4Legal4"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRepPpalLegal1.Items.Count; i++) { ddlPJPreguntaRepPpalLegal1.SelectedIndex = i; if (ddlPJPreguntaRepPpalLegal1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRepPpalLegal1"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRepPpalLegal2.Items.Count; i++) { ddlPJPreguntaRepPpalLegal2.SelectedIndex = i; if (ddlPJPreguntaRepPpalLegal2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRepPpalLegal2"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRepPpalLegal3.Items.Count; i++) { ddlPJPreguntaRepPpalLegal3.SelectedIndex = i; if (ddlPJPreguntaRepPpalLegal3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRepPpalLegal3"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJPreguntaRepPpalLegal4.Items.Count; i++) { ddlPJPreguntaRepPpalLegal4.SelectedIndex = i; if (ddlPJPreguntaRepPpalLegal4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJPreguntaRepPpalLegal4"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJCotizaBolsa.Items.Count; i++) { ddlPJCotizaBolsa.SelectedIndex = i; if (ddlPJCotizaBolsa.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJCotizaBolsa"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJEstatal.Items.Count; i++) { ddlPJEstatal.SelectedIndex = i; if (ddlPJEstatal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJEstatal"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJSinAnimoLucro.Items.Count; i++) { ddlPJSinAnimoLucro.SelectedIndex = i; if (ddlPJSinAnimoLucro.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJSinAnimoLucro"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPJSubsidiaria.Items.Count; i++) { ddlPJSubsidiaria.SelectedIndex = i; if (ddlPJSubsidiaria.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJSubsidiaria"].ToString().Trim()) break; }

                infoTituloPJ.Visible = true;
                infoPJ.Visible = true;

                if (dtInfo.Rows[0]["PJPregunta4"].ToString().Trim() == "SI")
                    TrVinculoPPExpuestaPJ.Visible = true;
                #endregion
            }

            #region Financiera
            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                tbxOtroTipoTransaccion.Text = dtInfo.Rows[0]["OtroTipoTransaccion"].ToString().Trim();
                tbxOtroTipoTransaccion.Enabled = false;
                tbxPFTipoProducto1.Text = dtInfo.Rows[0]["PFTipoProducto1"].ToString().Trim();
                tbxPFTipoProducto1.Enabled = false;
                tbxPFNumeroProducto1.Text = dtInfo.Rows[0]["PFNumeroProducto1"].ToString().Trim();
                tbxPFNumeroProducto1.Enabled = false;
                tbxPFEntidad1.Text = dtInfo.Rows[0]["PFEntidad1"].ToString().Trim();
                tbxPFEntidad1.Enabled = false;
                tbxPFMonto1.Text = dtInfo.Rows[0]["PFMonto1"].ToString().Trim();
                tbxPFMonto1.Enabled = false;
                tbxPFCiudad1.Text = dtInfo.Rows[0]["PFCiudad1"].ToString().Trim();
                tbxPFCiudad1.Enabled = false;
                tbxPFPais1.Text = dtInfo.Rows[0]["PFPais1"].ToString().Trim();
                tbxPFPais1.Enabled = false;
                tbxPFMoneda1.Text = dtInfo.Rows[0]["PFMoneda1"].ToString().Trim();
                tbxPFMoneda1.Enabled = false;
                tbxPFTipoProducto2.Text = dtInfo.Rows[0]["PFTipoProducto2"].ToString().Trim();
                tbxPFTipoProducto2.Enabled = false;
                tbxPFNumeroProducto2.Text = dtInfo.Rows[0]["PFNumeroProducto2"].ToString().Trim();
                tbxPFNumeroProducto2.Enabled = false;
                tbxPFEntidad2.Text = dtInfo.Rows[0]["PFEntidad2"].ToString().Trim();
                tbxPFEntidad2.Enabled = false;
                tbxPFMonto2.Text = dtInfo.Rows[0]["PFMonto2"].ToString().Trim();
                tbxPFMonto2.Enabled = false;
                tbxPFCiudad2.Text = dtInfo.Rows[0]["PFCiudad2"].ToString().Trim();
                tbxPFCiudad2.Enabled = false;
                tbxPFPais2.Text = dtInfo.Rows[0]["PFPais2"].ToString().Trim();
                tbxPFPais2.Enabled = false;
                tbxPFMoneda2.Text = dtInfo.Rows[0]["PFMoneda2"].ToString().Trim();
                tbxPFMoneda2.Enabled = false;
                tbxPFTipoProducto3.Text = dtInfo.Rows[0]["PFTipoProducto3"].ToString().Trim();
                tbxPFTipoProducto3.Enabled = false;
                tbxPFNumeroProducto3.Text = dtInfo.Rows[0]["PFNumeroProducto3"].ToString().Trim();
                tbxPFNumeroProducto3.Enabled = false;
                tbxPFEntidad3.Text = dtInfo.Rows[0]["PFEntidad3"].ToString().Trim();
                tbxPFEntidad3.Enabled = false;
                tbxPFMonto3.Text = dtInfo.Rows[0]["PFMonto3"].ToString().Trim();
                tbxPFMonto3.Enabled = false;
                tbxPFCiudad3.Text = dtInfo.Rows[0]["PFCiudad3"].ToString().Trim();
                tbxPFCiudad3.Enabled = false;
                tbxPFPais3.Text = dtInfo.Rows[0]["PFPais3"].ToString().Trim();
                tbxPFPais3.Enabled = false;
                tbxPFMoneda3.Text = dtInfo.Rows[0]["PFMoneda3"].ToString().Trim();
                tbxPFMoneda3.Enabled = false;
                
                    
                for (int i = 0; i < ddlTransacMonedaExtra.Items.Count; i++) { ddlTransacMonedaExtra.SelectedIndex = i; if (ddlTransacMonedaExtra.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim()) break; }
                for (int i = 0; i < ddlTipoTransaccion.Items.Count; i++) { ddlTipoTransaccion.SelectedIndex = i; if (ddlTipoTransaccion.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TipoTransaccion"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPFCtaMonedaExtra.Items.Count; i++) { ddlPFCtaMonedaExtra.SelectedIndex = i; if (ddlPFCtaMonedaExtra.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PFCtaMonedaExtra"].ToString().Trim()) break; }
                for (int i = 0; i < ddlPFProdExterior.Items.Count; i++) { ddlPFProdExterior.SelectedIndex = i; if (ddlPFProdExterior.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PFProdExterior"].ToString().Trim()) break; }
                ddlTransacMonedaExtra.Enabled = false;
                ddlTipoTransaccion.Enabled = false;
                ddlPFCtaMonedaExtra.Enabled = false;
                ddlPFProdExterior.Enabled = false;

                
            }
            #endregion

            #region Seguros
            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                tbxSeguroAno1.Text = dtInfo.Rows[0]["SeguroAno1"].ToString().Trim();
                tbxSeguroAno1.Enabled = false;
                tbxSeguroRamo1.Text = dtInfo.Rows[0]["SeguroRamo1"].ToString().Trim();
                tbxSeguroRamo1.Enabled = false;
                tbxSeguroCompania1.Text = dtInfo.Rows[0]["SeguroCompania1"].ToString().Trim();
                tbxSeguroCompania1.Enabled = false;
                tbxSeguroValor1.Text = dtInfo.Rows[0]["SeguroValor1"].ToString().Trim();
                tbxSeguroValor1.Enabled = false;
                tbxSeguroAno2.Text = dtInfo.Rows[0]["SeguroAno2"].ToString().Trim();
                tbxSeguroAno2.Enabled = false;
                tbxSeguroRamo2.Text = dtInfo.Rows[0]["SeguroRamo2"].ToString().Trim();
                tbxSeguroRamo2.Enabled = false;
                tbxSeguroCompania2.Text = dtInfo.Rows[0]["SeguroCompania2"].ToString().Trim();
                tbxSeguroCompania2.Enabled = false;
                tbxSeguroValor2.Text = dtInfo.Rows[0]["SeguroValor2"].ToString().Trim();
                tbxSeguroValor2.Enabled = false;
                tbxOrigenFondos.Text = dtInfo.Rows[0]["OrigenFondos"].ToString().Trim();
                tbxOrigenFondos.Enabled = false;

                for (int i = 0; i < ddlReclamaciones.Items.Count; i++) { ddlReclamaciones.SelectedIndex = i; if (ddlReclamaciones.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Reclamaciones"].ToString().Trim()) break; }
                ddlReclamaciones.Enabled = false;
                for (int i = 0; i < ddlSeguroTipo1.Items.Count; i++) { ddlSeguroTipo1.SelectedIndex = i; if (ddlSeguroTipo1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["SeguroTipo1"].ToString().Trim()) break; }
                ddlSeguroTipo1.Enabled = false;
                for (int i = 0; i < ddlSeguroTipo2.Items.Count; i++) { ddlSeguroTipo2.SelectedIndex = i; if (ddlSeguroTipo2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["SeguroTipo2"].ToString().Trim()) break; }
                ddlSeguroTipo2.Enabled = false;
            }
            #endregion

            #region Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                tbxLugarEntrevista.Text = dtInfo.Rows[0]["LugarEntrevista"].ToString().Trim();
                tbxLugarEntrevista.Enabled = false;
                tbxFechaEntrevista.Text = dtInfo.Rows[0]["FechaEntrevista"].ToString().Trim();
                tbxFechaEntrevista.Enabled = false;
                tbxHoraEntrevista.Text = dtInfo.Rows[0]["HoraEntrevista"].ToString().Trim();
                tbxHoraEntrevista.Enabled = false;
                ddlResultado.SelectedValue = dtInfo.Rows[0]["Resultado"].ToString().Trim();
                ddlResultado.Enabled = false;
                tbxObservaciones1.Text = dtInfo.Rows[0]["Observaciones1"].ToString().Trim();
                tbxObservaciones1.Enabled = false;
                tbxNombreResponsable.Text = dtInfo.Rows[0]["NombreResponsable"].ToString().Trim();
                tbxNombreResponsable.Enabled = false;
                tbxFechaVerificacion.Text = dtInfo.Rows[0]["FechaVerificacion"].ToString().Trim();
                tbxFechaVerificacion.Enabled = false;
                tbxHoraVerificacion.Text = dtInfo.Rows[0]["HoraVerificacion"].ToString().Trim();
                tbxHoraVerificacion.Enabled = false;
                tbxNombreVerifica.Text = dtInfo.Rows[0]["NombreVerifica"].ToString().Trim();
                tbxNombreVerifica.Enabled = false;
                tbxObservaciones2.Text = dtInfo.Rows[0]["Observaciones2"].ToString().Trim();
                tbxObservaciones2.Enabled = false;
                tbxNombreIntermediario.Text = dtInfo.Rows[0]["NombreIntermediario"].ToString().Trim();
                tbxNombreIntermediario.Enabled = false;
            }
            #endregion

            /*tbConsulta.Visible = false;
            tbFormulario.Visible = true;*/
            tbDocumentos.Visible = true;
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

        /*private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdConocimientoCliente", typeof(string));
            grid.Columns.Add("PrimerApellido", typeof(string));
            grid.Columns.Add("SegundoApellido", typeof(string));
            grid.Columns.Add("Nombres", typeof(string));
            grid.Columns.Add("TipoDocumento", typeof(string));
            grid.Columns.Add("NumeroDocumento", typeof(string));
            grid.Columns.Add("Ano", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("RazonDenominacion", typeof(string));
            grid.Columns.Add("NIT", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }*/
        #endregion
        #region DLLEventos
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
        protected void ddlReclamaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlReclamaciones.SelectedValue.ToString().Trim())
            {
                case "NO":
                    mtdHabilitaReclamos(false);
                    break;
                default:
                    mtdHabilitaReclamos(true);
                    break;
            }
        }
        protected void ddlPNCIIUDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxPNCIIU.Text = ddlPNCIIUDescripcion.SelectedValue.ToString().Trim();
        }
        #endregion
        #region Metodos
        void mtdHabilitaVinculacion(bool booVisible)
        {
            lblOtraVinculacion.Visible = booVisible;
            tbxOtraClaseVinculacion.Visible = booVisible;
            if (!booVisible)
                tbxOtraClaseVinculacion.Text = "";
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
        void mtdHabilitaMoneda(bool booVisible)
        {
            lblTipoTransaccion.Visible = booVisible;
            ddlTipoTransaccion.Visible = booVisible;
            //if (!booVisible)
            //    DropDownList4.Text = "";
        }
        private void mtdHabilitaReclamos(bool booVisible)
        {
            infoSeguros2.Visible = booVisible;
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
        private void resetValues()
        {
            #region Informacion Persona Natural
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
            #endregion
            

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
            tbxCiudad.Text = "";
            ddlSucursal.SelectedIndex = 0;
            ddlTipoSolicitud.SelectedIndex = 0;

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
            
        }
        #endregion
        #region GridView Eventos
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
        #endregion

        protected void Baprobar_Click(object sender, EventArgs e)
        {
           
        }
        public void OnConfirm(object sender, EventArgs e)
        {
            string IdConocimientoCliente = Session["IdConocimientoCliente"].ToString().Trim();
            string confirmValue = Request.Form["confirm_value"];
            string confirmValue2 = Request.Form["confirm_value2"];
            if (confirmValue == "Si")
            {
                cKnowClient.mtdAprobarFormulario(IdConocimientoCliente);
            }
            if (confirmValue2 == "Si")
            {
                cKnowClient.mtdRechazarFormulario(IdConocimientoCliente);
            }
            resetValues();
            TBgridCliente.Visible = false;
            TBFormularioNatural.Visible = false;
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
                    break;
                default:
                    mtdHabilitaPregunta4(false);
                    break;
            }
        }

        void mtdHabilitaPregunta4(bool booVisible)
        {
            lblPNEspecificacionPreguntas.Visible = booVisible;
            tbxPNEspecificacionPreguntas.Visible = booVisible;
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

        protected void ddlPJCodCIIU2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxPJCIIU.Text = ddlPJCodCIIU2.SelectedValue.ToString().Trim();
        }

        
    }
}