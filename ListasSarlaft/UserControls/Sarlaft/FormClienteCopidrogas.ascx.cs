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

namespace ListasSarlaft.UserControls
{
    public partial class FormClienteCopidrogas : System.Web.UI.UserControl
    {
        string IdFormulario = "6007";
        private cKnowClient cKnowClient = new cKnowClient();
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private int idConocimientoCliente;
        private DataTable infoGridArchivos;
        private int rowGridArchivos;

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
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
                initValues();

        }

        private void initValues()
        {
            IdConocimientoCliente = 0;
        }

        #region DDLs
        protected void ddlTipoInmueble_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoInmueble.SelectedValue.ToString() == "Otro")
            {
                lblOtroTipoInmueble.Visible = true;
                tbxOtroTipoInmueble.Visible = true;
                tbxOtroTipoInmueble.Text = string.Empty;
            }
            else
            {
                lblOtroTipoInmueble.Visible = false;
                tbxOtroTipoInmueble.Visible = false;
                tbxOtroTipoInmueble.Text = string.Empty;
            }
        }

        protected void ddlTipoVivienda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoVivienda.SelectedValue.ToString() == "Otro")
            {
                lblOtroTipoVivienda.Visible = true;
                tbxOtroTipoVivienda.Visible = true;
                tbxOtroTipoVivienda.Text = string.Empty;
            }
            else
            {
                lblOtroTipoVivienda.Visible = false;
                tbxOtroTipoVivienda.Visible = false;
                tbxOtroTipoVivienda.Text = string.Empty;
            }
        }

        protected void ddlTipoPerJuridica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPerJuridica.SelectedValue.ToString() == "Otro")
            {
                lblOtroPerJuridica.Visible = true;
                tbxOtroPerJuridica.Visible = true;
                tbxOtroPerJuridica.Text = string.Empty;
            }
            else
            {
                lblOtroPerJuridica.Visible = false;
                tbxOtroPerJuridica.Visible = false;
                tbxOtroPerJuridica.Text = string.Empty;
            }
        }

        protected void ddlOpMonedaExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOpMonedaExt.SelectedValue.ToString() == "SI")
            {
                lblCualOpMonedaExt.Visible = true;
                tbxCualOpMonedaExt.Visible = true;
                tbxCualOpMonedaExt.Text = string.Empty;

            }
            else
            {
                tbxCualOpMonedaExt.Visible = false;
                tbxCualOpMonedaExt.Visible = false;
                tbxCualOpMonedaExt.Text = string.Empty;
            }
        }
        #endregion

        #region GridView

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivos = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    descargarArchivo();
                    break;
            }
        }
        #endregion

        #region Loads
        private void mtdLoadInfoArchivos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cKnowClient.loadInfoGridArchivos(IdConocimientoCliente.ToString());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                           });
                }
                GridView1.DataSource = InfoGridArchivos;
                GridView1.DataBind();
            }
        }

        private void mtdLoadGridArchivos()
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

        #endregion

        //protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DropDownList2.SelectedValue.ToString() == "Otra")
        //    {
        //        Label9.Visible = true;
        //        TextBox3.Visible = true;
        //        TextBox3.Text = "";
        //    }
        //    else
        //    {
        //        Label9.Visible = false;
        //        TextBox3.Visible = false;
        //        TextBox3.Text = "";
        //    }
        //}

        //protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DropDownList3.SelectedValue.ToString() == "Otra")
        //    {
        //        Label11.Visible = true;
        //        TextBox4.Visible = true;
        //        TextBox4.Text = "";
        //    }
        //    else
        //    {
        //        Label11.Visible = false;
        //        TextBox4.Visible = false;
        //        TextBox4.Text = "";
        //    }
        //}

        //protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DropDownList4.SelectedValue.ToString() == "Otra")
        //    {
        //        Label13.Visible = true;
        //        TextBox5.Visible = true;
        //        TextBox5.Text = "";
        //    }
        //    else
        //    {
        //        Label13.Visible = false;
        //        TextBox5.Visible = false;
        //        TextBox5.Text = "";
        //    }
        //}

        //protected void DropDownList12_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DropDownList12.SelectedValue.ToString() == "OTRA")
        //    {
        //        Label69.Visible = true;
        //        TextBox52.Visible = true;
        //        TextBox52.Text = "";
        //    }
        //    else
        //    {
        //        Label69.Visible = false;
        //        TextBox52.Visible = false;
        //        TextBox52.Text = "";
        //    }
        //}

        //protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DropDownList19.SelectedValue.ToString() == "OTRA")
        //    {
        //        Label96.Visible = true;
        //        TextBox70.Visible = true;
        //        TextBox70.Text = "";
        //    }
        //    else
        //    {
        //        Label96.Visible = false;
        //        TextBox70.Visible = false;
        //        TextBox70.Text = "";
        //    }

        //}

        private void resetValues()
        {

            tbxActEcoInfoPer.Text = string.Empty;
            tbxActEconPpalInfoJur.Text = string.Empty;
            tbxActivEconomInfoJur.Text = string.Empty;
            tbxActoNoComAdm.Text = string.Empty;
            tbxActoNoConAdm.Text = string.Empty;
            tbxAplazadoComAdm.Text = string.Empty;
            tbxAplazadoConAdm.Text = string.Empty;
            tbxAportesXPagar.Text = string.Empty;
            tbxBancoMonedaExt.Text = string.Empty;
            tbxBarrioDomPpal.Text = string.Empty;
            tbxBarrioDrogueria2.Text = string.Empty;
            tbxBarrioDrogueria3.Text = string.Empty;
            tbxBarrioDrogueriaPpal.Text = string.Empty;
            tbxCapitalInfoJur.Text = string.Empty;
            tbxCIIUEmpInfoJur.Text = string.Empty;
            tbxCIIUInfoJur.Text = string.Empty;
            tbxCIIUInfoPer.Text = string.Empty;
            tbxCiudadDomPpal.Text = string.Empty;
            tbxCiudadDrogueria2.Text = string.Empty;
            tbxCiudadDrogueria3.Text = string.Empty;
            tbxCiudadDrogueriaPpal.Text = string.Empty;
            tbxCiudadMonedaExt.Text = string.Empty;
            tbxCompromisoAhorroMen.Text = string.Empty;
            tbxConceptoOtrosIngInfoPer.Text = string.Empty;
            tbxConceptOtrosIngMenAccionistas.Text = string.Empty;
            tbxCualOpMonedaExt.Text = string.Empty;
            tbxCuotaAdmision.Text = string.Empty;
            tbxCuotaAfilAsocoldro.Text = string.Empty;
            tbxCuotaMensual.Text = string.Empty;
            tbxDepCiuMunRepLegalInfoJur.Text = string.Empty;
            tbxDepCiuMunRepLegalSuplInfoJur.Text = string.Empty;
            tbxDesfavorableComAdm.Text = string.Empty;
            tbxDesfavorableConAdm.Text = string.Empty;
            tbxDirDomicilioPpal.Text = string.Empty;
            tbxDirDrogueria2.Text = string.Empty;
            tbxDirDrogueria3.Text = string.Empty;
            tbxDirDrogueriaPpal.Text = string.Empty;
            tbxDocConstitucionInfoJur.Text = string.Empty;
            tbxDptoDomPpal.Text = string.Empty;
            tbxDptoDrogueria2.Text = string.Empty;
            tbxDptoDrogueria3.Text = string.Empty;
            tbxDptoDrogueriaPpal.Text = string.Empty;
            tbxEgrMenAccionistas.Text = string.Empty;
            tbxEgrMenInfoPer.Text = string.Empty;
            tbxEmailInfoJur.Text = string.Empty;
            tbxEntidadFinanciera.Text = string.Empty;
            //tbxEstratoDomPpal.Text = string.Empty;
            tbxFavorableComAdm.Text = string.Empty;
            tbxFavorableConAdm.Text = string.Empty;
            tbxFechaActoComAdm.Text = string.Empty;
            tbxFechaActoConAdm.Text = string.Empty;
            tbxFechaConstitucionInfoJur.Text = string.Empty;
            tbxFechaEntregaForm.Text = string.Empty;
            tbxFechaExp.Text = string.Empty;
            tbxFechaNmto.Text = string.Empty;
            tbxFechaRegInfoJur.Text = string.Empty;
            tbxFechaVerEntrevista1.Text = string.Empty;
            tbxFechaVerEntrevista2.Text = string.Empty;
            tbxIngMenAccionistas.Text = string.Empty;
            tbxIngMenInfoPer.Text = string.Empty;
            tbxLugarFechaExp.Text = string.Empty;
            tbxLugarFechaNmto.Text = string.Empty;
            tbxMatriMerInfoJur.Text = string.Empty;
            tbxMonMonedaExt.Text = string.Empty;
            tbxNacionalidad.Text = string.Empty;
            tbxNIT.Text = string.Empty;
            tbxNITDrogueriaPpal.Text = string.Empty;
            tbxNombreCoordinador1ComAdm.Text = string.Empty;
            tbxNombreCoordinador1ConAdm.Text = string.Empty;
            tbxNombreDrogueria2.Text = string.Empty;
            tbxNombreDrogueria3.Text = string.Empty;
            tbxNombreDrogueriaPpal.Text = string.Empty;
            tbxNombres.Text = string.Empty;
            tbxNombreSecretario1ComAdm.Text = string.Empty;
            tbxNombreSecretario1ConAdm.Text = string.Empty;
            tbxNroCuentaMonedaExt.Text = string.Empty;
            tbxNroDocRazonSocialAccionistas1.Text = string.Empty;
            tbxNroDocRazonSocialAccionistas2.Text = string.Empty;
            tbxNroDocRazonSocialAccionistas3.Text = string.Empty;
            tbxNroDocRazonSocialAccionistas4.Text = string.Empty;
            tbxNroDocRazonSocialAccionistas5.Text = string.Empty;
            tbxNroDocRepLegal1InfoJur.Text = string.Empty;
            tbxNroDocRepLegal2InfoJur.Text = string.Empty;
            tbxNroDocRepLegal3InfoJur.Text = string.Empty;
            tbxNroDocRepLegal4InfoJur.Text = string.Empty;
            tbxNroDocRepLegalPpalInfoJur.Text = string.Empty;
            tbxNroDocRepLegalSuplInfoJur.Text = string.Empty;
            tbxNumeroDoc.Text = string.Empty;
            tbxObsEntrevista1.Text = string.Empty;
            tbxObsEntrevista2.Text = string.Empty;
            tbxObservacionesComAdm.Text = string.Empty;
            tbxObservacionesConAdm.Text = string.Empty;
            tbxOtroPerJuridica.Text = string.Empty;
            tbxOtrosIngInfoPer.Text = string.Empty;
            tbxOtrosIngMenAccionistas.Text = string.Empty;
            tbxOtroTipoInmueble.Text = string.Empty;
            tbxOtroTipoVivienda.Text = string.Empty;
            tbxPaisMonedaExt.Text = string.Empty;
            tbxPerApellido.Text = string.Empty;
            tbxRazonSocial.Text = string.Empty;
            tbxRazonSocialAccionistas1.Text = string.Empty;
            tbxRazonSocialAccionistas2.Text = string.Empty;
            tbxRazonSocialAccionistas3.Text = string.Empty;
            tbxRazonSocialAccionistas4.Text = string.Empty;
            tbxRazonSocialAccionistas5.Text = string.Empty;
            tbxRegSuperSolInfoJur.Text = string.Empty;
            tbxRepLegal1InfoJur.Text = string.Empty;
            tbxRepLegal2InfoJur.Text = string.Empty;
            tbxRepLegal3InfoJur.Text = string.Empty;
            tbxRepLegal4InfoJur.Text = string.Empty;
            tbxRepLegalPpalInfoJur.Text = string.Empty;
            tbxRepLegalSuplInfoJur.Text = string.Empty;
            tbxSdoApellido.Text = string.Empty;
            tbxTelCelInfoPer.Text = string.Empty;
            tbxTelefonoDrogueria2.Text = string.Empty;
            tbxTelefonoDrogueria3.Text = string.Empty;
            tbxTelefonoDrogueriaPpal.Text = string.Empty;
            tbxTelefonoInfoJur.Text = string.Empty;
            tbxTelefonoInfoPer.Text = string.Empty;
            tbxTelefonoRepLegalInfoJur.Text = string.Empty;
            tbxTelefonoRepLegalSuplInfoJur.Text = string.Empty;
            tbxTotActivosAccionistas.Text = string.Empty;
            tbxTotActivosInfoPer.Text = string.Empty;
            tbxTotalCompromisos.Text = string.Empty;
            tbxTotPasivosAccionistas.Text = string.Empty;
            tbxTotPasivosInfoPer.Text = string.Empty;
            tbxVerificaEntrevista1.Text = string.Empty;
            tbxVerificaEntrevista2.Text = string.Empty;
            tbxVisitaEfectuadaPorComAdm.Text = string.Empty;
            tbxVisitaEfectuadaPorConAdm.Text = string.Empty;
            tbxViviendaPropia.Text = string.Empty;
            tbxVlrAperturaCtaCopicredito.Text = string.Empty;

            ddlAdmRecPublicos.SelectedIndex = 0;
            ddlCredHipotecario.SelectedIndex = 0;
            ddlCuentasMonedaExt.SelectedIndex = 0;
            ddlEstadoCivil.SelectedIndex = 0;
            DDLtbxEstratoDomPpal.SelectedIndex = 0;
            ddlGrPoderPublico.SelectedIndex = 0;
            ddlOpMonedaExt.SelectedIndex = 0;
            ddlReconPubGral.SelectedIndex = 0;
            ddlSexo.SelectedIndex = 0;
            ddlSociedadComercial.SelectedIndex = 0;
            ddlTipoDocRazonSocialAccionistas1.SelectedIndex = 0;
            ddlTipoDocRazonSocialAccionistas2.SelectedIndex = 0;
            ddlTipoDocRazonSocialAccionistas3.SelectedIndex = 0;
            ddlTipoDocRazonSocialAccionistas4.SelectedIndex = 0;
            ddlTipoDocRazonSocialAccionistas5.SelectedIndex = 0;
            ddlTipoDocRepLegal1InfoJur.SelectedIndex = 0;
            ddlTipoDocRepLegal2InfoJur.SelectedIndex = 0;
            ddlTipoDocRepLegal3InfoJur.SelectedIndex = 0;
            ddlTipoDocRepLegal4InfoJur.SelectedIndex = 0;
            ddlTipoDocRepLegalPpalInfoJur.SelectedIndex = 0;
            ddlTipoDocRepLegalSuplInfoJur.SelectedIndex = 0;
            ddlTipoDocumento.SelectedIndex = 0;
            ddlTipoEmpresaInfoJur.SelectedIndex = 0;
            ddlTipoInmueble.SelectedIndex = 0;
            ddlTipoPerJuridica.SelectedIndex = 0;
            ddlTipoVinculacion.SelectedIndex = 0;
            ddlTipoVivienda.SelectedIndex = 0;                
            
            FileUpload1.Dispose();

            btnGuardar.Visible = true;
            btnImprimir.Visible = false;
            btnLimpiar.Visible = true;
            btnContinuar.Visible = false;
            btnSiguiente.Visible = false;
            btnAnterior.Visible = false;
                       
        }

        private void mtdDisableCampos()
        {
        }

        private void mtdEnableCampos()
        {
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdRegistrarFormulario();
                    Mensaje("Cliente registrado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar la información. " + ex.Message);
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string str;
            str = "window.open('ReporteKnowCliente.aspx?IdConocimientoCliente=" + IdConocimientoCliente + "','Reporte','width=950px,height=900px,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            initValues();
            resetValues();
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            resetValues();
            //enableCampos();
            initValues();
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnSiguiente.Visible = false;
            btnAnterior.Visible = true;
            //tbFormulario.Visible = false;
            //tbDocumentos.Visible = true;
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            btnSiguiente.Visible = true;
            btnAnterior.Visible = false;
            //tbFormulario.Visible = true;
            //tbDocumentos.Visible = false;
        }

        //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        if (cCuenta.permisosAgregar(IdFormulario) == "False")
        //        {
        //            Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
        //        }
        //        else
        //        {
        //            cKnowClient.UpdateInfoFormDocsInu(IdConocimientoCliente, DropDownList23.SelectedValue.ToString().Trim(), DropDownList24.SelectedValue.ToString().Trim(), DropDownList25.SelectedValue.ToString().Trim(), DropDownList26.SelectedValue.ToString().Trim(), DropDownList27.SelectedValue.ToString().Trim(), DropDownList28.SelectedValue.ToString().Trim(), DropDownList29.SelectedValue.ToString().Trim(), DropDownList30.SelectedValue.ToString().Trim(), DropDownList39.SelectedValue.ToString().Trim(), DropDownList31.SelectedValue.ToString().Trim(), DropDownList32.SelectedValue.ToString().Trim(), DropDownList33.SelectedValue.ToString().Trim(), DropDownList34.SelectedValue.ToString().Trim(), DropDownList35.SelectedValue.ToString().Trim(), DropDownList36.SelectedValue.ToString().Trim(), DropDownList37.SelectedValue.ToString().Trim(), DropDownList38.SelectedValue.ToString().Trim(), DropDownList40.SelectedValue.ToString().Trim(), DropDownList41.SelectedValue.ToString().Trim(), DropDownList42.SelectedValue.ToString().Trim(), DropDownList43.SelectedValue.ToString().Trim(), DropDownList44.SelectedValue.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("Error al registrar la información. " + ex.Message);
        //    }
        //}

        #region Cargar Archivos
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {
                            loadFile();
                            mtdLoadGridArchivos();
                            mtdLoadInfoArchivos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al adjuntar el documento. " + ex.Message);
            }
        }

        private void loadFile()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;

            dtInfo = cKnowClient.loadCodigoInfoFormArchivo();

            if (dtInfo.Rows.Count > 0)
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + IdConocimientoCliente.ToString() + "-" + FileUpload1.FileName.ToString().Trim();
            else
                nameFile = "1-" + IdConocimientoCliente.ToString() + "-" + FileUpload1.FileName.ToString().Trim();

            FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFs/") + nameFile);
            cKnowClient.agregarArchivo(IdConocimientoCliente.ToString().Trim(), nameFile);
        }

        private void descargarArchivo()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFs/" + InfoGridArchivos.Rows[RowGridArchivos]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }
        #endregion

        #region

        private void Mensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #endregion

        private void mtdRegistrarFormulario()
        {
            IdConocimientoCliente = cKnowClient.agregarConocimientoCliente(
                string.Format("{0:yyyy MM dd}", DateTime.Now).Replace(" ", ""), string.Format("{0:yyyy}", DateTime.Now));

            cKnowClient.InfoFormCliente(IdConocimientoCliente, string.Empty, string.Empty, ddlTipoVinculacion.SelectedValue.ToString().Trim(),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ddlTipoCliente.SelectedValue.ToString().Trim());

            //cKnowClient.InfoFormPN(IdConocimientoCliente, tbxPerApellido.Text.Trim(), tbxSdoApellido.Text.Trim(), tbxNombres.Text.Trim(),
            //    ddlTipoDocumento.SelectedValue.ToString().Trim(), tbxNumeroDoc.Text.Trim(), tbxFechaExp.Text.Trim(), tbxLugarFechaExp.Text.Trim(),
            //    tbxFechaNmto.Text.Trim(), tbxNacionalidad.Text.Trim(), string.Empty, tbxActEcoInfoPer.Text.Trim(), string.Empty, tbxCIIUInfoPer.Text.Trim(),
            //    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, tbxDirDomicilioPpal.Text.Trim(),
            //    tbxCiudadDomPpal.Text.Trim(), tbxTelefonoInfoPer.Text.Trim(), tbxTelCelInfoPer.Text.Trim(),
            //    ddlAdmRecPublicos.SelectedValue.ToString().Trim(), ddlGrPoderPublico.SelectedValue.ToString().Trim(), ddlReconPubGral.SelectedValue.ToString().Trim(),
            //    string.Empty, tbxIngMenInfoPer.Text.Trim(), tbxTotActivosInfoPer.Text.Trim(), tbxEgrMenInfoPer.Text.Trim(), tbxTotPasivosInfoPer.Text.Trim(),
            //    tbxOtrosIngInfoPer.Text.Trim(), tbxConceptoOtrosIngInfoPer.Text.Trim(), ddlSexo.SelectedValue.ToString().Trim(),
            //    tbxEmailInfoPer.Text.Trim(), tbxLugarFechaNmto.Text.Trim(), ddlEstadoCivil.SelectedValue.ToString().Trim(), tbxDptoDomPpal.Text.Trim(),
            //    tbxBarrioDomPpal.Text.Trim(), tbxEstratoDomPpal.Text.Trim(), ddlTipoInmueble.SelectedValue.ToString().Trim(), ddlTipoVivienda.SelectedValue.ToString().Trim(),
            //    tbxViviendaPropia.Text.Trim(), ddlCredHipotecario.SelectedValue.ToString().Trim(), tbxCuotaMensual.Text.Trim(), tbxEntidadFinanciera.Text.Trim(),
            //    tbxOtroTipoInmueble.Text.Trim(), tbxOtroTipoVivienda.Text.Trim());
            cKnowClient.InfoFormPN(IdConocimientoCliente, tbxPerApellido.Text.Trim(), tbxSdoApellido.Text.Trim(), tbxNombres.Text.Trim(),
                ddlTipoDocumento.SelectedValue.ToString().Trim(), tbxNumeroDoc.Text.Trim(), tbxFechaExp.Text.Trim(), tbxLugarFechaExp.Text.Trim(),
                tbxFechaNmto.Text.Trim(), tbxNacionalidad.Text.Trim(), string.Empty, tbxActEcoInfoPer.Text.Trim(), string.Empty, tbxCIIUInfoPer.Text.Trim(),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, tbxDirDomicilioPpal.Text.Trim(),
                DDLCiudadDomPpal.SelectedItem.Text.ToString().Trim(), tbxTelefonoInfoPer.Text.Trim(), tbxTelCelInfoPer.Text.Trim(),
                ddlAdmRecPublicos.SelectedValue.ToString().Trim(), ddlGrPoderPublico.SelectedValue.ToString().Trim(), ddlReconPubGral.SelectedValue.ToString().Trim(),
                string.Empty, tbxIngMenInfoPer.Text.Trim(), tbxTotActivosInfoPer.Text.Trim(), tbxEgrMenInfoPer.Text.Trim(), tbxTotPasivosInfoPer.Text.Trim(),
                tbxOtrosIngInfoPer.Text.Trim(), tbxConceptoOtrosIngInfoPer.Text.Trim(), ddlSexo.SelectedValue.ToString().Trim(),
                tbxEmailInfoPer.Text.Trim(), tbxLugarFechaNmto.Text.Trim(), ddlEstadoCivil.SelectedValue.ToString().Trim(), DDLDptoDomPpal.SelectedItem.Text.ToString().Trim(),
                tbxBarrioDomPpal.Text.Trim(), DDLtbxEstratoDomPpal.SelectedItem.Text.Trim(), ddlTipoInmueble.SelectedValue.ToString().Trim(), ddlTipoVivienda.SelectedValue.ToString().Trim(),
                tbxViviendaPropia.Text.Trim(), ddlCredHipotecario.SelectedValue.ToString().Trim(), tbxCuotaMensual.Text.Trim(), tbxEntidadFinanciera.Text.Trim(),
                tbxOtroTipoInmueble.Text.Trim(), tbxOtroTipoVivienda.Text.Trim());

            cKnowClient.InfoFormPJ(IdConocimientoCliente, tbxRazonSocial.Text.Trim(), tbxNIT.Text.Trim(),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                tbxTelefonoInfoJur.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                ddlTipoEmpresaInfoJur.SelectedValue.ToString().Trim(), tbxActivEconomInfoJur.Text.Trim(), string.Empty, tbxCIIUInfoJur.Text.Trim(),
                tbxRazonSocialAccionistas1.Text.Trim(), ddlTipoDocRazonSocialAccionistas1.SelectedValue.ToString().Trim(), tbxNroDocRazonSocialAccionistas1.Text.Trim(),
                tbxRazonSocialAccionistas2.Text.Trim(), ddlTipoDocRazonSocialAccionistas2.SelectedValue.ToString().Trim(), tbxNroDocRazonSocialAccionistas2.Text.Trim(),
                tbxRazonSocialAccionistas3.Text.Trim(), ddlTipoDocRazonSocialAccionistas3.SelectedValue.ToString().Trim(), tbxNroDocRazonSocialAccionistas3.Text.Trim(),
                tbxRazonSocialAccionistas4.Text.Trim(), ddlTipoDocRazonSocialAccionistas4.SelectedValue.ToString().Trim(), tbxNroDocRazonSocialAccionistas4.Text.Trim(),
                tbxRazonSocialAccionistas5.Text.Trim(), ddlTipoDocRazonSocialAccionistas5.SelectedValue.ToString().Trim(), tbxNroDocRazonSocialAccionistas5.Text.Trim(),
                tbxIngMenAccionistas.Text.Trim(), tbxTotActivosAccionistas.Text.Trim(), tbxEgrMenAccionistas.Text.Trim(),
                tbxTotPasivosAccionistas.Text.Trim(), tbxOtrosIngMenAccionistas.Text.Trim(), tbxConceptOtrosIngMenAccionistas.Text.Trim(),
                string.Empty, tbxEmailInfoJur.Text.Trim(), string.Empty, ddlTipoPerJuridica.SelectedValue.ToString().Trim(),
                ddlSociedadComercial.SelectedValue.ToString().Trim(), tbxCapitalInfoJur.Text.Trim(), tbxFechaConstitucionInfoJur.Text.Trim(),
                tbxDocConstitucionInfoJur.Text.Trim(), tbxFechaRegInfoJur.Text.Trim(), tbxMatriMerInfoJur.Text.Trim(), tbxRegSuperSolInfoJur.Text.Trim(),
                tbxRepLegalPpalInfoJur.Text.Trim(), ddlTipoDocRepLegalPpalInfoJur.Text.Trim(), tbxNroDocRepLegalPpalInfoJur.Text.Trim(),
                tbxRepLegal1InfoJur.Text.Trim(), ddlTipoDocRepLegal1InfoJur.Text.Trim(), tbxNroDocRepLegal1InfoJur.Text.Trim(),
                tbxRepLegal2InfoJur.Text.Trim(), ddlTipoDocRepLegal2InfoJur.Text.Trim(), tbxNroDocRepLegal2InfoJur.Text.Trim(),
                tbxRepLegal3InfoJur.Text.Trim(), ddlTipoDocRepLegal3InfoJur.Text.Trim(), tbxNroDocRepLegal3InfoJur.Text.Trim(),
                tbxRepLegal4InfoJur.Text.Trim(), ddlTipoDocRepLegal4InfoJur.Text.Trim(), tbxNroDocRepLegal4InfoJur.Text.Trim(),
                tbxTelefonoRepLegalInfoJur.Text.Trim(), tbxDepCiuMunRepLegalInfoJur.Text.Trim(), tbxRepLegalSuplInfoJur.Text.Trim(),
                ddlTipoDocRepLegalSuplInfoJur.SelectedValue.ToString().Trim(), tbxNroDocRepLegalSuplInfoJur.Text.Trim(), tbxTelefonoRepLegalSuplInfoJur.Text.Trim(),
                tbxDepCiuMunRepLegalSuplInfoJur.Text.Trim(), tbxActEconPpalInfoJur.Text.Trim(), tbxCIIUEmpInfoJur.Text.Trim());

            cKnowClient.InfoFormPF(IdConocimientoCliente, ddlOpMonedaExt.SelectedValue.ToString().Trim(), string.Empty, tbxCualOpMonedaExt.Text.Trim(),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                ddlCuentasMonedaExt.SelectedValue.ToString().Trim(), tbxNroCuentaMonedaExt.Text.Trim(), tbxBancoMonedaExt.Text.Trim(),
                tbxCiudadMonedaExt.Text.Trim(), tbxPaisMonedaExt.Text.Trim(), tbxMonMonedaExt.Text.Trim());

            //cKnowClient.InfoFormDroguerias(IdConocimientoCliente,
            //    tbxNombreDrogueriaPpal.Text.Trim(), tbxNITDrogueriaPpal.Text.Trim(), tbxDirDrogueriaPpal.Text.Trim(), tbxDptoDrogueriaPpal.Text.Trim(),
            //    tbxCiudadDrogueriaPpal.Text.Trim(), tbxBarrioDrogueriaPpal.Text.Trim(), tbxTelefonoDrogueriaPpal.Text.Trim(),
            //    tbxNombreDrogueria2.Text.Trim(), tbxDirDrogueria2.Text.Trim(), tbxDptoDrogueria2.Text.Trim(),
            //    tbxCiudadDrogueria2.Text.Trim(), tbxBarrioDrogueria2.Text.Trim(), tbxTelefonoDrogueria2.Text.Trim(),
            //    tbxNombreDrogueria3.Text.Trim(), tbxDirDrogueria3.Text.Trim(), tbxDptoDrogueria3.Text.Trim(),
            //    tbxCiudadDrogueria3.Text.Trim(), tbxBarrioDrogueria3.Text.Trim(), tbxTelefonoDrogueria3.Text.Trim());

            cKnowClient.InfoFormDroguerias(IdConocimientoCliente,
                tbxNombreDrogueriaPpal.Text.Trim(), tbxNITDrogueriaPpal.Text.Trim(), tbxDirDrogueriaPpal.Text.Trim(), DDLDptoDrogueriaPpal.SelectedItem.Text.Trim(),
                DDLCiudadDrogueriaPpal.SelectedItem.Text.Trim(), tbxBarrioDrogueriaPpal.Text.Trim(), tbxTelefonoDrogueriaPpal.Text.Trim(),
                tbxNombreDrogueria2.Text.Trim(), tbxDirDrogueria2.Text.Trim(), DDLDptoDrogueria2.SelectedItem.Text.Trim(),
                DDLCiudadDrogueria2.SelectedItem.Text.Trim(), tbxBarrioDrogueria2.Text.Trim(), tbxTelefonoDrogueria2.Text.Trim(),
                tbxNombreDrogueria3.Text.Trim(), tbxDirDrogueria3.Text.Trim(), DDLDptoDrogueria3.SelectedItem.Text.Trim(),
                DDLCiudadDrogueria3.SelectedItem.Text.Trim(), tbxBarrioDrogueria3.Text.Trim(), tbxTelefonoDrogueria3.Text.Trim());

            cKnowClient.InfoFormSometimiento(IdConocimientoCliente, tbxAportesXPagar.Text.Trim(),
                tbxVlrAperturaCtaCopicredito.Text.Trim(), tbxCompromisoAhorroMen.Text.Trim(), tbxCuotaAdmision.Text.Trim(),
                tbxCuotaAfilAsocoldro.Text.Trim(), tbxTotalCompromisos.Text.Trim(), tbxFechaEntregaForm.Text.Trim());

            cKnowClient.InfoFormCopidrogas(IdConocimientoCliente, tbxActoNoComAdm.Text.Trim(), tbxFechaActoComAdm.Text.Trim(), tbxFavorableComAdm.Text.Trim(),
                tbxAplazadoComAdm.Text.Trim(), tbxDesfavorableComAdm.Text.Trim(), tbxVisitaEfectuadaPorComAdm.Text.Trim(), tbxObservacionesComAdm.Text.Trim(),
                tbxNombreCoordinador1ComAdm.Text.Trim(), tbxNombreSecretario1ComAdm.Text.Trim(),
                tbxActoNoConAdm.Text.Trim(), tbxFechaActoConAdm.Text.Trim(), tbxFavorableConAdm.Text.Trim(), tbxAplazadoConAdm.Text.Trim(),
                tbxDesfavorableConAdm.Text.Trim(), tbxVisitaEfectuadaPorConAdm.Text.Trim(), tbxObservacionesConAdm.Text.Trim(),
                tbxNombreCoordinador1ConAdm.Text.Trim(), tbxNombreSecretario1ConAdm.Text.Trim());

            cKnowClient.InfoFormEntrevista(IdConocimientoCliente,
                string.Empty, tbxFechaVerEntrevista1.Text.Trim(), string.Empty, string.Empty, tbxObsEntrevista1.Text.Trim(),
                tbxVerificaEntrevista1.Text.Trim(), tbxFechaVerEntrevista2.Text.Trim(), string.Empty, tbxVerificaEntrevista2.Text.Trim(), tbxObsEntrevista2.Text.Trim(), 
                ddlValidaFirma.SelectedValue.ToString().Trim(), ddlValidaHuella.SelectedValue.ToString().Trim(), ddlValidaEntrevista.SelectedValue.ToString());

            btnGuardar.Visible = false;
            btnImprimir.Visible = true;
            btnLimpiar.Visible = false;
            btnContinuar.Visible = true;
            btnSiguiente.Visible = true;
            btnAnterior.Visible = false;

            tbFormulario.Visible = true;
            //tbDocumentos.Visible = false;
            //disableCampos();
            mtdLoadGridArchivos();
        }

        
    }
}