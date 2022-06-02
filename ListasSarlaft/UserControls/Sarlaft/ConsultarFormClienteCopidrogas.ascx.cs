using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using System.IO;

namespace ListasSarlaft.UserControls
{
    public partial class ConsultarFormClienteCopidrogas : System.Web.UI.UserControl
    {
        string IdFormulario = "6008";
        private cKnowClient cKnowClient = new cKnowClient();
        private cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private int idConocimientoCliente;
        private int idexRow;
        private DataTable infGrid;
        private DataTable infoGridArchivos;
        private int rowGridArchivos;
        private int pagIndexInfoGridFormCliente;

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
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                mtdInicializarValores();
                mtdInicializarValores2();
                mtdHabilitarCampos(false);
                DDLDptoDomPpal.DataBind();
                
                if ((Request.QueryString["ValueTipoPersona"] != null) &&
                    (Request.QueryString["ValueInusualidad"] != null) &&
                    (Request.QueryString["Estado"] != null))
                {
                    if ((!string.IsNullOrEmpty(Request.QueryString["ValueTipoPersona"])) &&
                        (!string.IsNullOrEmpty(Request.QueryString["ValueInusualidad"])) &&
                        (!string.IsNullOrEmpty(Request.QueryString["Estado"])))
                        mtdBuscarFormulario(Request.QueryString["ValueTipoPersona"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                }
            }
        }

        #region Buttons
        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            string strMensaje = string.Empty;

            try
            {
                #region Ano
                if (!string.IsNullOrEmpty(TextBox107.Text.Trim()))
                {
                    int number;
                    bool result = Int32.TryParse(TextBox107.Text.Trim(), out number);

                    if (!result)
                        strMensaje = "Campo Año no valido. ";
                }
                #endregion

                #region Fecha Desde
                if (!string.IsNullOrEmpty(TextBox108.Text.Trim()))
                {
                    try
                    {
                        System.DateTime.Parse(TextBox108.Text.Trim());
                    }
                    catch
                    {
                        strMensaje = strMensaje + "Fecha desde no valida. ";
                    }
                }
                #endregion

                #region Fecha Hasta
                if (!string.IsNullOrEmpty(TextBox109.Text.Trim()))
                {
                    try
                    {
                        System.DateTime.Parse(TextBox109.Text.Trim());
                    }
                    catch
                    {
                        strMensaje = strMensaje + "Fecha hasta no valida.";
                    }

                }
                #endregion

                #region Ingreso Info
                if (string.IsNullOrEmpty(strMensaje.Trim()))
                {
                    mtdInicializarValores();
                    mtdBuscarFormulario(
                        DropDownList39.SelectedValue.ToString().Trim(),
                        TextBox99.Text.Trim(),
                        TextBox103.Text.Trim(), TextBox105.Text.Trim(),
                        TextBox106.Text.Trim(), TextBox107.Text.Trim(),
                        TextBox108.Text.Trim().Replace(" ", ""),
                        TextBox109.Text.Trim().Replace(" ", ""),
                        TextBox110.Text.Trim(),
                        TextBox111.Text.Trim());
                }
                else
                    Mensaje(strMensaje);
                #endregion
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        /// <summary>
        /// Limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button5_Click(object sender, EventArgs e)
        {
            mtdResetValuesConsulta();
            mtdLoadGrid();
            mtdResetValues();
            mtdHabilitarCampos(false);
            //Persona Natural
            Label188.Visible = true;
            Label189.Visible = true;
            Label190.Visible = true;
            Label191.Visible = true;
            TextBox99.Visible = true;
            TextBox103.Visible = true;
            TextBox105.Visible = true;
            TextBox106.Visible = true;
        }

        /// <summary>
        /// Exportar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button7_Click(object sender, EventArgs e)
        {
            mtdExportarExcel(InfoGrid, Response, "Export Clientes");
        }

        /// <summary>
        /// Modificar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdActualizarFormulario();
                    mtdResetValues();
                    mtdResetValuesConsulta();
                    mtdHabilitarCampos(false);
                    mtdLoadGrid();
                    Mensaje("Información actualizada con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la información. " + ex.Message);
            }
        }

        /// <summary>
        /// Imprimir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            mtdImprimirFormulario(IdConocimientoCliente.ToString());
        }

        /// <summary>
        /// Continuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            mtdResetValues();
            mtdHabilitarCampos(false);
        }

        /// <summary>
        /// Cargar archivos PDFs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {
                            mtdCargarPdfFormCliente();
                            mtdLoadGridArchivos();
                            mtdLoadInfoArchivos(IdConocimientoCliente.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al adjuntar el documento. " + ex.Message);
            }
        }

        #endregion

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
            IdexRow = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridFormCliente) + Convert.ToInt16(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Imprimir":
                    IdConocimientoCliente = Convert.ToInt32(InfoGrid.Rows[IdexRow]["IdConocimientoCliente"]);
                    mtdImprimirFormulario(IdConocimientoCliente.ToString());
                    break;

                case "Modificar":
                    IdConocimientoCliente = Convert.ToInt32(InfoGrid.Rows[IdexRow]["IdConocimientoCliente"]);
                    mtdCargarInfo(IdConocimientoCliente);
                    mtdLoadGridArchivos();
                    mtdLoadInfoArchivos(IdConocimientoCliente.ToString());
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

        #region Metodos
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Inicializar Valores
        private void mtdInicializarValores2()
        {
            IdConocimientoCliente = 0;
            IdexRow = 0;
        }

        private void mtdInicializarValores()
        {
            PagIndexInfoGridFormCliente = 0;
        }
        #endregion

        private void mtdBuscarFormulario(string strTipoPersona, string PrimerApellido, string SegundoApellido,
            string Nombre, string NumeroDocumento, string Ano, string FechaDesde, string FechaHasta,
            string strRazonSocial, string NIT)
        {
            DataTable dtInfo = new DataTable();

            mtdLoadGrid();
            mtdResetValues();
            mtdHabilitarCampos(false);

            #region Tipo Persona
            if (strTipoPersona == "0")
            {
                dtInfo = cKnowClient.buscarClientFormPN(PrimerApellido, SegundoApellido, Nombre, NumeroDocumento, Ano, FechaDesde, FechaHasta);
                GridView1.Columns[8].Visible = false;
                GridView1.Columns[9].Visible = false;
            }
            else
            {
                dtInfo = cKnowClient.buscarClientFormPJ(PrimerApellido, SegundoApellido, Nombre, NumeroDocumento, Ano, FechaDesde, FechaHasta, strRazonSocial, NIT);
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[9].Visible = true;
            }
            #endregion

            #region Mostrar info Grid
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
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
                        dtInfo.Rows[rows]["RazonDenominacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NIT"].ToString()                                                    
                        });
                }

                GridView1.PageIndex = PagIndexInfoGridFormCliente;
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
                Button7.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Button7.Visible = false;
                Mensaje("No se encontraron registros asociados a los parametros de búsqueda.");
            }
            #endregion
        }

        #region Loads
        private void mtdLoadGrid()
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
        }

        private void mtdLoadGridArchivos()
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

        private void mtdLoadInfoArchivos(string strIdConocimientoCliente)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cKnowClient.loadInfoGridArchivos(strIdConocimientoCliente);

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
        #endregion

        #region Resets
        private void mtdResetValues()
        {
            #region Info Persona
            #region Tipo Vinculacion
            ddlTipoVinculacion.SelectedIndex = 0;
            #endregion

            tbxPerApellido.Text = string.Empty;
            tbxSdoApellido.Text = string.Empty;
            tbxNombres.Text = string.Empty;

            #region Tipo Documento
            ddlTipoDocumento.SelectedIndex = 0;
            #endregion

            tbxNumeroDoc.Text = string.Empty;
            tbxFechaExp.Text = string.Empty;
            tbxFechaNmto.Text = string.Empty;
            tbxLugarFechaExp.Text = string.Empty;
            tbxNacionalidad.Text = string.Empty;
            tbxLugarFechaNmto.Text = string.Empty;

            #region Sexo
            ddlSexo.SelectedIndex = 0;
            #endregion

            #region Estado Civil
            ddlEstadoCivil.SelectedIndex = 0;
            #endregion

            #region Estrato
            DDLtbxEstratoDomPpal.SelectedIndex = 0;
            #endregion



            tbxDirDomicilioPpal.Text = string.Empty;
            tbxDptoDomPpal.Text = string.Empty;
            tbxCiudadDomPpal.Text = string.Empty;
            tbxBarrioDomPpal.Text = string.Empty;
            tbxEstratoDomPpal.Text = string.Empty;
            tbxTelefonoInfoPer.Text = string.Empty;
            tbxTelCelInfoPer.Text = string.Empty;
            tbxEmailInfoPer.Text = string.Empty;

            #region Recursos Publicos
            ddlAdmRecPublicos.SelectedIndex = 0;
            #endregion

            #region Poder Publico
            ddlGrPoderPublico.SelectedIndex = 0;
            #endregion

            #region Reconocimiento Pub
            ddlReconPubGral.SelectedIndex = 0;
            #endregion

            tbxActEcoInfoPer.Text = string.Empty;
            tbxCIIUInfoPer.Text = string.Empty;
            tbxIngMenInfoPer.Text = string.Empty;
            tbxTotActivosInfoPer.Text = string.Empty;
            tbxEgrMenInfoPer.Text = string.Empty;
            tbxTotPasivosInfoPer.Text = string.Empty;
            tbxOtrosIngInfoPer.Text = string.Empty;
            tbxConceptoOtrosIngInfoPer.Text = string.Empty;

            #region Tipo Inmueble
            ddlTipoInmueble.SelectedIndex = 0;
            #endregion

            tbxOtroTipoInmueble.Text = string.Empty;

            #region Tipo Vivienda
            ddlTipoVivienda.SelectedIndex = 0;
            #endregion

            tbxOtroTipoVivienda.Text = string.Empty;
            tbxViviendaPropia.Text = string.Empty;

            #region Credito Hipotecario
            ddlCredHipotecario.SelectedIndex = 0;
            #endregion

            tbxCuotaMensual.Text = string.Empty;
            tbxEntidadFinanciera.Text = string.Empty;

            #endregion

            #region Info Juridica
            tbxRazonSocial.Text = string.Empty;
            tbxNIT.Text = string.Empty;

            #region Tipo Per Juridica
            ddlTipoPerJuridica.SelectedIndex = 0;
            #endregion

            tbxOtroPerJuridica.Text = string.Empty;

            #region Soc. Comercial
            ddlSociedadComercial.SelectedIndex = 0;
            #endregion

            tbxActivEconomInfoJur.Text = string.Empty;
            tbxCIIUInfoJur.Text = string.Empty;
            tbxCapitalInfoJur.Text = string.Empty;
            tbxFechaConstitucionInfoJur.Text = string.Empty;
            tbxDocConstitucionInfoJur.Text = string.Empty;
            tbxFechaRegInfoJur.Text = string.Empty;
            tbxMatriMerInfoJur.Text = string.Empty;
            tbxTelefonoInfoJur.Text = string.Empty;
            tbxEmailInfoJur.Text = string.Empty;
            tbxRegSuperSolInfoJur.Text = string.Empty;

            #region Representantes legales
            #region Rep Legal Ppal
            tbxRepLegalPpalInfoJur.Text = string.Empty;
            ddlTipoDocRepLegalPpalInfoJur.SelectedIndex = 0;
            tbxNroDocRepLegalPpalInfoJur.Text = string.Empty;
            #endregion

            #region Rep Legal 1
            tbxRepLegal1InfoJur.Text = string.Empty;
            ddlTipoDocRepLegal1InfoJur.SelectedIndex = 0;
            tbxNroDocRepLegal1InfoJur.Text = string.Empty;
            #endregion

            #region Rep Legal 2
            tbxRepLegal2InfoJur.Text = string.Empty;
            ddlTipoDocRepLegal2InfoJur.SelectedIndex = 0;
            tbxNroDocRepLegal2InfoJur.Text = string.Empty;
            #endregion

            #region Rep Legal 3
            tbxRepLegal3InfoJur.Text = string.Empty;
            ddlTipoDocRepLegal3InfoJur.SelectedIndex = 0;
            tbxNroDocRepLegal3InfoJur.Text = string.Empty;
            #endregion

            #region Rep Legal 4
            tbxRepLegal4InfoJur.Text = string.Empty;
            ddlTipoDocRepLegal4InfoJur.SelectedIndex = 0;
            tbxNroDocRepLegal4InfoJur.Text = string.Empty;
            #endregion

            tbxTelefonoRepLegalInfoJur.Text = string.Empty;
            tbxDepCiuMunRepLegalInfoJur.Text = string.Empty;

            #region Rep Legal suplente
            tbxRepLegalSuplInfoJur.Text = string.Empty;
            ddlTipoDocRepLegalSuplInfoJur.SelectedIndex = 0;
            tbxNroDocRepLegalSuplInfoJur.Text = string.Empty;
            #endregion

            tbxTelefonoRepLegalSuplInfoJur.Text = string.Empty;
            tbxDepCiuMunRepLegalSuplInfoJur.Text = string.Empty;
            #endregion

            #region Tipo empresa
            ddlTipoEmpresaInfoJur.SelectedIndex = 0;
            #endregion

            tbxActEconPpalInfoJur.Text = string.Empty;
            tbxCIIUEmpInfoJur.Text = string.Empty;

            #region Accionistas
            #region Accionista 1
            tbxRazonSocialAccionistas1.Text = string.Empty;
            ddlTipoDocRazonSocialAccionistas1.SelectedIndex = 0;
            tbxNroDocRazonSocialAccionistas1.Text = string.Empty;
            #endregion

            #region Accionista 2
            tbxRazonSocialAccionistas2.Text = string.Empty;
            ddlTipoDocRazonSocialAccionistas2.SelectedIndex = 0;
            tbxNroDocRazonSocialAccionistas2.Text = string.Empty;
            #endregion

            #region Accionista 3
            tbxRazonSocialAccionistas3.Text = string.Empty;
            ddlTipoDocRazonSocialAccionistas3.SelectedIndex = 0;
            tbxNroDocRazonSocialAccionistas3.Text = string.Empty;
            #endregion

            #region Accionista 4
            tbxRazonSocialAccionistas4.Text = string.Empty;
            ddlTipoDocRazonSocialAccionistas4.SelectedIndex = 0;
            tbxNroDocRazonSocialAccionistas4.Text = string.Empty;
            #endregion

            #region Accionista 5
            tbxRazonSocialAccionistas5.Text = string.Empty;
            ddlTipoDocRazonSocialAccionistas5.SelectedIndex = 0;
            tbxNroDocRazonSocialAccionistas5.Text = string.Empty;
            #endregion

            #region Info Financ.
            tbxIngMenAccionistas.Text = string.Empty;
            tbxTotActivosAccionistas.Text = string.Empty;
            tbxEgrMenAccionistas.Text = string.Empty;
            tbxTotPasivosAccionistas.Text = string.Empty;
            tbxOtrosIngMenAccionistas.Text = string.Empty;
            tbxConceptOtrosIngMenAccionistas.Text = string.Empty;
            #endregion
            #endregion

            #endregion

            #region Op. Moneda Ext
            ddlOpMonedaExt.SelectedIndex = 0;
            tbxCualOpMonedaExt.Text = string.Empty;

            #region Cuentas Moneda Ext.
            ddlCuentasMonedaExt.SelectedIndex = 0;
            #endregion

            tbxNroCuentaMonedaExt.Text = string.Empty;
            tbxBancoMonedaExt.Text = string.Empty;
            tbxCiudadMonedaExt.Text = string.Empty;
            tbxPaisMonedaExt.Text = string.Empty;
            tbxMonMonedaExt.Text = string.Empty;
            #endregion

            #region Drogueria

            #region Drog Ppal
            tbxNombreDrogueriaPpal.Text = string.Empty;
            tbxNITDrogueriaPpal.Text = string.Empty;
            tbxDirDrogueriaPpal.Text = string.Empty;
            tbxDptoDrogueriaPpal.Text = string.Empty;
            tbxCiudadDrogueriaPpal.Text = string.Empty;
            tbxBarrioDrogueriaPpal.Text = string.Empty;
            tbxTelefonoDrogueriaPpal.Text = string.Empty;
            #endregion

            #region Drog 1
            tbxNombreDrogueria2.Text = string.Empty;
            tbxDirDrogueria2.Text = string.Empty;
            tbxDptoDrogueria2.Text = string.Empty;
            tbxCiudadDrogueria2.Text = string.Empty;
            tbxBarrioDrogueria2.Text = string.Empty;
            tbxTelefonoDrogueria2.Text = string.Empty;
            #endregion

            #region Drog 2
            tbxNombreDrogueria3.Text = string.Empty;
            tbxDirDrogueria3.Text = string.Empty;
            tbxDptoDrogueria3.Text = string.Empty;
            tbxCiudadDrogueria3.Text = string.Empty;
            tbxBarrioDrogueria3.Text = string.Empty;
            tbxTelefonoDrogueria3.Text = string.Empty;
            #endregion

            #endregion

            #region Sometimiento
            tbxAportesXPagar.Text = string.Empty;
            tbxVlrAperturaCtaCopicredito.Text = string.Empty;
            tbxCompromisoAhorroMen.Text = string.Empty;
            tbxCuotaAdmision.Text = string.Empty;
            tbxCuotaAfilAsocoldro.Text = string.Empty;
            tbxTotalCompromisos.Text = string.Empty;
            tbxFechaEntregaForm.Text = string.Empty;
            #endregion

            #region Espacio Copidrogas
            #region Admision
            tbxActoNoComAdm.Text = string.Empty;
            tbxFechaActoComAdm.Text = string.Empty;
            tbxFavorableComAdm.Text = string.Empty;
            tbxAplazadoComAdm.Text = string.Empty;
            tbxDesfavorableComAdm.Text = string.Empty;
            tbxVisitaEfectuadaPorComAdm.Text = string.Empty;
            tbxObservacionesComAdm.Text = string.Empty;
            tbxNombreCoordinador1ComAdm.Text = string.Empty;
            tbxNombreSecretario1ComAdm.Text = string.Empty;
            #endregion

            #region Administracion
            tbxActoNoConAdm.Text = string.Empty;
            tbxFechaActoConAdm.Text = string.Empty;
            tbxFavorableConAdm.Text = string.Empty;
            tbxAplazadoConAdm.Text = string.Empty;
            tbxDesfavorableConAdm.Text = string.Empty;
            tbxVisitaEfectuadaPorConAdm.Text = string.Empty;
            tbxObservacionesConAdm.Text = string.Empty;
            tbxNombreCoordinador1ConAdm.Text = string.Empty;
            tbxNombreSecretario1ConAdm.Text = string.Empty;
            #endregion
            #endregion

            #region Entrevista
            tbxFechaVerEntrevista1.Text = string.Empty;
            tbxObsEntrevista1.Text = string.Empty;
            tbxVerificaEntrevista1.Text = string.Empty;
            tbxFechaVerEntrevista2.Text = string.Empty;
            tbxObsEntrevista2.Text = string.Empty;
            tbxVerificaEntrevista2.Text = string.Empty;
            #endregion

            FileUpload1.Dispose();
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            tbConsulta.Visible = true;
            tbFormulario1.Visible = false;
            Button7.Visible = false;
        }

        private void mtdResetValuesConsulta()
        {
            DropDownList39.SelectedIndex = 0;

            TextBox99.Text = string.Empty;
            TextBox103.Text = string.Empty;
            TextBox105.Text = string.Empty;
            TextBox106.Text = string.Empty;
            TextBox107.Text = string.Empty;
            TextBox108.Text = string.Empty;
            TextBox109.Text = string.Empty;
            TextBox110.Text = string.Empty;
            TextBox111.Text = string.Empty;

            TextBox110.Visible = false;
            TextBox111.Visible = false;
            Label195.Visible = false;
            Label196.Visible = false;
        }
        #endregion

        #region Habilitacion Campos
        private void mtdHabilitarCampos(bool booEstado)
        {
            #region Info Persona
            #region Tipo Vinculacion
            ddlTipoVinculacion.Enabled = booEstado;
            #endregion

            tbxPerApellido.Enabled = booEstado;
            tbxSdoApellido.Enabled = booEstado;
            tbxNombres.Enabled = booEstado;

            #region Tipo Documento
            ddlTipoDocumento.Enabled = booEstado;
            #endregion

            tbxNumeroDoc.Enabled = booEstado;
            tbxFechaExp.Enabled = booEstado;
            tbxFechaNmto.Enabled = booEstado;
            tbxLugarFechaExp.Enabled = booEstado;
            tbxNacionalidad.Enabled = booEstado;
            tbxLugarFechaNmto.Enabled = booEstado;

            #region Sexo
            ddlSexo.Enabled = booEstado;
            #endregion

            #region Estado Civil
            ddlEstadoCivil.Enabled = booEstado;
            #endregion

            #region Estrato
            DDLtbxEstratoDomPpal.Enabled = booEstado;
            #endregion

            tbxDirDomicilioPpal.Enabled = booEstado;
            tbxDptoDomPpal.Enabled = booEstado;
            tbxCiudadDomPpal.Enabled = booEstado;
            tbxBarrioDomPpal.Enabled = booEstado;
            tbxEstratoDomPpal.Enabled = booEstado;
            tbxTelefonoInfoPer.Enabled = booEstado;
            tbxTelCelInfoPer.Enabled = booEstado;
            tbxEmailInfoPer.Enabled = booEstado;

            #region Recursos Publicos
            ddlAdmRecPublicos.Enabled = booEstado;
            #endregion

            #region Poder Publico
            ddlGrPoderPublico.Enabled = booEstado;
            #endregion

            #region Reconocimiento Pub
            ddlReconPubGral.Enabled = booEstado;
            #endregion

            tbxActEcoInfoPer.Enabled = booEstado;
            tbxCIIUInfoPer.Enabled = booEstado;
            tbxIngMenInfoPer.Enabled = booEstado;
            tbxTotActivosInfoPer.Enabled = booEstado;
            tbxEgrMenInfoPer.Enabled = booEstado;
            tbxTotPasivosInfoPer.Enabled = booEstado;
            tbxOtrosIngInfoPer.Enabled = booEstado;
            tbxConceptoOtrosIngInfoPer.Enabled = booEstado;

            #region Tipo Inmueble
            ddlTipoInmueble.Enabled = booEstado;
            #endregion

            tbxOtroTipoInmueble.Enabled = booEstado;

            #region Tipo Vivienda
            ddlTipoVivienda.Enabled = booEstado;
            #endregion

            tbxOtroTipoVivienda.Enabled = booEstado;
            tbxViviendaPropia.Enabled = booEstado;

            #region Credito Hipotecario
            ddlCredHipotecario.Enabled = booEstado;
            #endregion

            tbxCuotaMensual.Enabled = booEstado;
            tbxEntidadFinanciera.Enabled = booEstado;

            #endregion

            #region Info Juridica
            tbxRazonSocial.Enabled = booEstado;
            tbxNIT.Enabled = booEstado;

            #region Tipo Per Juridica
            ddlTipoPerJuridica.Enabled = booEstado;
            #endregion

            tbxOtroPerJuridica.Enabled = booEstado;

            #region Soc. Comercial
            ddlSociedadComercial.Enabled = booEstado;
            #endregion

            tbxActivEconomInfoJur.Enabled = booEstado;
            tbxCIIUInfoJur.Enabled = booEstado;
            tbxCapitalInfoJur.Enabled = booEstado;
            tbxFechaConstitucionInfoJur.Enabled = booEstado;
            tbxDocConstitucionInfoJur.Enabled = booEstado;
            tbxFechaRegInfoJur.Enabled = booEstado;
            tbxMatriMerInfoJur.Enabled = booEstado;
            tbxTelefonoInfoJur.Enabled = booEstado;
            tbxEmailInfoJur.Enabled = booEstado;
            tbxRegSuperSolInfoJur.Enabled = booEstado;

            #region Representantes legales
            #region Rep Legal Ppal
            tbxRepLegalPpalInfoJur.Enabled = booEstado;
            ddlTipoDocRepLegalPpalInfoJur.Enabled = booEstado;
            tbxNroDocRepLegalPpalInfoJur.Enabled = booEstado;
            #endregion

            #region Rep Legal 1
            tbxRepLegal1InfoJur.Enabled = booEstado;
            ddlTipoDocRepLegal1InfoJur.Enabled = booEstado;
            tbxNroDocRepLegal1InfoJur.Enabled = booEstado;
            #endregion

            #region Rep Legal 2
            tbxRepLegal2InfoJur.Enabled = booEstado;
            ddlTipoDocRepLegal2InfoJur.Enabled = booEstado;
            tbxNroDocRepLegal2InfoJur.Enabled = booEstado;
            #endregion

            #region Rep Legal 3
            tbxRepLegal3InfoJur.Enabled = booEstado;
            ddlTipoDocRepLegal3InfoJur.Enabled = booEstado;
            tbxNroDocRepLegal3InfoJur.Enabled = booEstado;
            #endregion

            #region Rep Legal 4
            tbxRepLegal4InfoJur.Enabled = booEstado;
            ddlTipoDocRepLegal4InfoJur.Enabled = booEstado;
            tbxNroDocRepLegal4InfoJur.Enabled = booEstado;
            #endregion

            tbxTelefonoRepLegalInfoJur.Enabled = booEstado;
            tbxDepCiuMunRepLegalInfoJur.Enabled = booEstado;

            #region Rep Legal suplente
            tbxRepLegalSuplInfoJur.Enabled = booEstado;
            ddlTipoDocRepLegalSuplInfoJur.Enabled = booEstado;
            tbxNroDocRepLegalSuplInfoJur.Enabled = booEstado;
            #endregion

            tbxTelefonoRepLegalSuplInfoJur.Enabled = booEstado;
            tbxDepCiuMunRepLegalSuplInfoJur.Enabled = booEstado;
            #endregion

            #region Tipo empresa
            ddlTipoEmpresaInfoJur.Enabled = booEstado;
            #endregion

            tbxActEconPpalInfoJur.Enabled = booEstado;
            tbxCIIUEmpInfoJur.Enabled = booEstado;

            #region Accionistas
            #region Accionista 1
            tbxRazonSocialAccionistas1.Enabled = booEstado;
            ddlTipoDocRazonSocialAccionistas1.Enabled = booEstado;
            tbxNroDocRazonSocialAccionistas1.Enabled = booEstado;
            #endregion

            #region Accionista 2
            tbxRazonSocialAccionistas2.Enabled = booEstado;
            ddlTipoDocRazonSocialAccionistas2.Enabled = booEstado;
            tbxNroDocRazonSocialAccionistas2.Enabled = booEstado;
            #endregion

            #region Accionista 3
            tbxRazonSocialAccionistas3.Enabled = booEstado;
            ddlTipoDocRazonSocialAccionistas3.Enabled = booEstado;
            tbxNroDocRazonSocialAccionistas3.Enabled = booEstado;
            #endregion

            #region Accionista 4
            tbxRazonSocialAccionistas4.Enabled = booEstado;
            ddlTipoDocRazonSocialAccionistas4.Enabled = booEstado;
            tbxNroDocRazonSocialAccionistas4.Enabled = booEstado;
            #endregion

            #region Accionista 5
            tbxRazonSocialAccionistas5.Enabled = booEstado;
            ddlTipoDocRazonSocialAccionistas5.Enabled = booEstado;
            tbxNroDocRazonSocialAccionistas5.Enabled = booEstado;
            #endregion

            #region Info Financ.
            tbxIngMenAccionistas.Enabled = booEstado;
            tbxTotActivosAccionistas.Enabled = booEstado;
            tbxEgrMenAccionistas.Enabled = booEstado;
            tbxTotPasivosAccionistas.Enabled = booEstado;
            tbxOtrosIngMenAccionistas.Enabled = booEstado;
            tbxConceptOtrosIngMenAccionistas.Enabled = booEstado;
            #endregion
            #endregion

            #endregion

            #region Op. Moneda Ext
            ddlOpMonedaExt.Enabled = booEstado;
            tbxCualOpMonedaExt.Enabled = booEstado;

            #region Cuentas Moneda Ext.
            ddlCuentasMonedaExt.Enabled = booEstado;
            #endregion

            tbxNroCuentaMonedaExt.Enabled = booEstado;
            tbxBancoMonedaExt.Enabled = booEstado;
            tbxCiudadMonedaExt.Enabled = booEstado;
            tbxPaisMonedaExt.Enabled = booEstado;
            tbxMonMonedaExt.Enabled = booEstado;
            #endregion

            #region Drogueria

            #region Drog Ppal
            tbxNombreDrogueriaPpal.Enabled = booEstado;
            tbxNITDrogueriaPpal.Enabled = booEstado;
            tbxDirDrogueriaPpal.Enabled = booEstado;
            tbxDptoDrogueriaPpal.Enabled = booEstado;
            tbxCiudadDrogueriaPpal.Enabled = booEstado;
            tbxBarrioDrogueriaPpal.Enabled = booEstado;
            tbxTelefonoDrogueriaPpal.Enabled = booEstado;
            #endregion

            #region Drog 1
            tbxNombreDrogueria2.Enabled = booEstado;
            tbxDirDrogueria2.Enabled = booEstado;
            tbxDptoDrogueria2.Enabled = booEstado;
            tbxCiudadDrogueria2.Enabled = booEstado;
            tbxBarrioDrogueria2.Enabled = booEstado;
            tbxTelefonoDrogueria2.Enabled = booEstado;
            #endregion

            #region Drog 2
            tbxNombreDrogueria3.Enabled = booEstado;
            tbxDirDrogueria3.Enabled = booEstado;
            tbxDptoDrogueria3.Enabled = booEstado;
            tbxCiudadDrogueria3.Enabled = booEstado;
            tbxBarrioDrogueria3.Enabled = booEstado;
            tbxTelefonoDrogueria3.Enabled = booEstado;
            #endregion

            #endregion

            #region Sometimiento
            tbxAportesXPagar.Enabled = booEstado;
            tbxVlrAperturaCtaCopicredito.Enabled = booEstado;
            tbxCompromisoAhorroMen.Enabled = booEstado;
            tbxCuotaAdmision.Enabled = booEstado;
            tbxCuotaAfilAsocoldro.Enabled = booEstado;
            tbxTotalCompromisos.Enabled = booEstado;
            tbxFechaEntregaForm.Enabled = booEstado;
            #endregion

            #region Espacio Copidrogas
            #region Admision
            tbxActoNoComAdm.Enabled = booEstado;
            tbxFechaActoComAdm.Enabled = booEstado;
            tbxFavorableComAdm.Enabled = booEstado;
            tbxAplazadoComAdm.Enabled = booEstado;
            tbxDesfavorableComAdm.Enabled = booEstado;
            tbxVisitaEfectuadaPorComAdm.Enabled = booEstado;
            tbxObservacionesComAdm.Enabled = booEstado;
            tbxNombreCoordinador1ComAdm.Enabled = booEstado;
            tbxNombreSecretario1ComAdm.Enabled = booEstado;
            #endregion

            #region Administracion
            tbxActoNoConAdm.Enabled = booEstado;
            tbxFechaActoConAdm.Enabled = booEstado;
            tbxFavorableConAdm.Enabled = booEstado;
            tbxAplazadoConAdm.Enabled = booEstado;
            tbxDesfavorableConAdm.Enabled = booEstado;
            tbxVisitaEfectuadaPorConAdm.Enabled = booEstado;
            tbxObservacionesConAdm.Enabled = booEstado;
            tbxNombreCoordinador1ConAdm.Enabled = booEstado;
            tbxNombreSecretario1ConAdm.Enabled = booEstado;
            #endregion
            #endregion

            #region Entrevista
            tbxFechaVerEntrevista1.Enabled = booEstado;
            tbxObsEntrevista1.Enabled = booEstado;
            tbxVerificaEntrevista1.Enabled = booEstado;
            tbxFechaVerEntrevista2.Enabled = booEstado;
            tbxObsEntrevista2.Enabled = booEstado;
            tbxVerificaEntrevista2.Enabled = booEstado;
            #endregion

            Button1.Visible = booEstado;
            Button2.Visible = booEstado;
            Button3.Visible = booEstado;

            FileUpload1.Enabled = booEstado;
        }
        #endregion

        public static void mtdExportarExcel(DataTable dt, HttpResponse Response, string filename)
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

        private void mtdImprimirFormulario(string strIdConocimientoCliente)
        {
            string str;
            str = "window.open('ReporteKnowClienteCopidrogas.aspx?IdConocimientoCliente=" + strIdConocimientoCliente + "','Reporte','width=950px,height=900px,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        private void mtdCargarInfo(int intIdConocimientoCliente)
        {
            DataTable dtInfo = new DataTable();

            mtdHabilitarCampos(true);

            #region Info Persona

            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente);
            if (dtInfo.Rows.Count > 0)
            {
                #region Tipo Vinculacion

                for (int i = 0; i < ddlTipoVinculacion.Items.Count; i++)
                {
                    ddlTipoVinculacion.SelectedIndex = i;

                    if (ddlTipoVinculacion.SelectedItem.Text.Trim() == dtInfo.Rows[0]["OtraClaseVinculacion"].ToString().Trim())
                        break;
                }
                #endregion

                #region Tipo Cliente
                for (int i = 0; i < ddlTipoCliente.Items.Count; i++)
                {
                    ddlTipoCliente.SelectedIndex = i;

                    if (ddlTipoCliente.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TipoCliente"].ToString().Trim())
                        break;
                }

                #endregion
            }

            dtInfo = cKnowClient.InfoFormPN(intIdConocimientoCliente);
            if (dtInfo.Rows.Count > 0)
            {
                tbxPerApellido.Text = dtInfo.Rows[0]["PNPrimerApellido"].ToString().Trim();
                tbxSdoApellido.Text = dtInfo.Rows[0]["PNSegunApellido"].ToString().Trim();
                tbxNombres.Text = dtInfo.Rows[0]["PNNombres"].ToString().Trim();

                #region Tipo Documento
                for (int i = 0; i < ddlTipoDocumento.Items.Count; i++)
                {
                    ddlTipoDocumento.SelectedIndex = i;
                    if (ddlTipoDocumento.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNTipoDocumento"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNumeroDoc.Text = dtInfo.Rows[0]["PNNumeroDocumento"].ToString().Trim();
                tbxFechaExp.Text = dtInfo.Rows[0]["PNFechaExpedicion"].ToString().Trim();
                tbxFechaNmto.Text = dtInfo.Rows[0]["PNFechaNacimiento"].ToString().Trim();
                tbxLugarFechaExp.Text = dtInfo.Rows[0]["PNLugar"].ToString().Trim();
                tbxNacionalidad.Text = dtInfo.Rows[0]["PNNacionalidad"].ToString().Trim();
                tbxLugarFechaNmto.Text = dtInfo.Rows[0]["PNLugarNmto"].ToString().Trim();

                #region Sexo
                for (int i = 0; i < ddlSexo.Items.Count; i++)
                {
                    ddlSexo.SelectedIndex = i;
                    if (ddlSexo.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNSexo"].ToString().Trim())
                        break;
                }
                #endregion

                #region Estado Civil
                for (int i = 0; i < ddlEstadoCivil.Items.Count; i++)
                {
                    ddlEstadoCivil.SelectedIndex = i;
                    if (ddlEstadoCivil.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNEstadoCivil"].ToString().Trim())
                        break;
                }
                #endregion


                #region Estrato
                for (int i = 0; i < DDLtbxEstratoDomPpal.Items.Count; i++)
                {
                    DDLtbxEstratoDomPpal.SelectedIndex = i;
                    if (DDLtbxEstratoDomPpal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNEstrato"].ToString().Trim())
                        break;
                }
                #endregion

                tbxDirDomicilioPpal.Text = dtInfo.Rows[0]["PNDireccionResidencia"].ToString().Trim();
                //tbxDptoDomPpal.Text = dtInfo.Rows[0]["PNDpto"].ToString().Trim();
                //tbxCiudadDomPpal.Text = dtInfo.Rows[0]["PNCiudad2"].ToString().Trim();
                
                #region Departamento y Ciudad

                if (dtInfo.Rows[0]["PNDpto"].ToString().Trim() != string.Empty)
                {
                    for (int i = 0; i < DDLDptoDomPpal.Items.Count; i++)
                    {
                        DDLDptoDomPpal.SelectedIndex = i;
                        if (DDLDptoDomPpal.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PNDpto"].ToString().Trim())
                        {
                            DDLCiudadDomPpal.DataBind();
                            for (int ic = 0; ic < DDLCiudadDomPpal.Items.Count; ic++)
                            {
                                DDLCiudadDomPpal.SelectedIndex = ic;
                                if (DDLCiudadDomPpal.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["PNCiudad2"].ToString().Trim())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            DDLDptoDomPpal.SelectedIndex = 0;
                        }
                    }
                }
                #endregion

                tbxBarrioDomPpal.Text = dtInfo.Rows[0]["PNBarrio"].ToString().Trim();
                tbxEstratoDomPpal.Text = dtInfo.Rows[0]["PNEstrato"].ToString().Trim();
                tbxTelefonoInfoPer.Text = dtInfo.Rows[0]["PNTelefono2"].ToString().Trim();
                tbxTelCelInfoPer.Text = dtInfo.Rows[0]["PNCelular"].ToString().Trim();
                tbxEmailInfoPer.Text = dtInfo.Rows[0]["PNCorreoElectronico"].ToString().Trim();

                #region Recursos Publicos
                for (int i = 0; i < ddlAdmRecPublicos.Items.Count; i++)
                {
                    ddlAdmRecPublicos.SelectedIndex = i;
                    if (ddlAdmRecPublicos.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta1"].ToString().Trim())
                        break;
                }
                #endregion

                #region Poder Publico
                for (int i = 0; i < ddlGrPoderPublico.Items.Count; i++)
                {
                    ddlGrPoderPublico.SelectedIndex = i;
                    if (ddlGrPoderPublico.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta2"].ToString().Trim())
                        break;
                }
                #endregion

                #region Reconocimiento Pub
                for (int i = 0; i < ddlReconPubGral.Items.Count; i++)
                {
                    ddlReconPubGral.SelectedIndex = i;
                    if (ddlReconPubGral.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta3"].ToString().Trim())
                        break;
                }
                #endregion

                tbxActEcoInfoPer.Text = dtInfo.Rows[0]["PNActividadEconomica"].ToString().Trim();
                tbxCIIUInfoPer.Text = dtInfo.Rows[0]["PNCIIU"].ToString().Trim();
                tbxIngMenInfoPer.Text = dtInfo.Rows[0]["PNIngresosMensuales"].ToString().Trim();
                tbxTotActivosInfoPer.Text = dtInfo.Rows[0]["PNActivos"].ToString().Trim();
                tbxEgrMenInfoPer.Text = dtInfo.Rows[0]["PNEgresoMensuales"].ToString().Trim();
                tbxTotPasivosInfoPer.Text = dtInfo.Rows[0]["PNPasivos"].ToString().Trim();
                tbxOtrosIngInfoPer.Text = dtInfo.Rows[0]["PNOtrosIngresos"].ToString().Trim();
                tbxConceptoOtrosIngInfoPer.Text = dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString().Trim();

                #region Tipo Inmueble
                for (int i = 0; i < ddlTipoInmueble.Items.Count; i++)
                {
                    ddlTipoInmueble.SelectedIndex = i;
                    if (ddlTipoInmueble.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNTipoInmueble"].ToString().Trim())
                        break;
                }
                #endregion

                tbxOtroTipoInmueble.Text = dtInfo.Rows[0]["PNOtroTipoInmueble"].ToString().Trim();

                #region Tipo Vivienda
                for (int i = 0; i < ddlTipoVivienda.Items.Count; i++)
                {
                    ddlTipoVivienda.SelectedIndex = i;
                    if (ddlTipoVivienda.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNTipoVivienda"].ToString().Trim())
                        break;
                }
                #endregion

                tbxOtroTipoVivienda.Text = dtInfo.Rows[0]["PNOtroTipoVivienda"].ToString().Trim();
                tbxViviendaPropia.Text = dtInfo.Rows[0]["PNViviendaPropia"].ToString().Trim();

                #region Credito Hipotecario
                for (int i = 0; i < ddlCredHipotecario.Items.Count; i++)
                {
                    ddlCredHipotecario.SelectedIndex = i;
                    if (ddlCredHipotecario.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNCreditoHipo"].ToString().Trim())
                        break;
                }
                #endregion

                tbxCuotaMensual.Text = dtInfo.Rows[0]["PNVlrCuotaMen"].ToString().Trim();
                tbxEntidadFinanciera.Text = dtInfo.Rows[0]["PNEntidadFinanc"].ToString().Trim();
            }
            #endregion

            #region Info Juridica
            dtInfo = cKnowClient.InfoFormPJ(intIdConocimientoCliente);
            if (dtInfo.Rows.Count > 0)
            {
                tbxRazonSocial.Text = dtInfo.Rows[0]["PJRazonDenominacion"].ToString().Trim();
                tbxNIT.Text = dtInfo.Rows[0]["PJNIT"].ToString().Trim();

                #region Tipo Per Juridica
                for (int i = 0; i < ddlTipoPerJuridica.Items.Count; i++)
                {
                    ddlTipoPerJuridica.SelectedIndex = i;
                    if (ddlTipoPerJuridica.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoPersonaJur"].ToString().Trim())
                        break;
                }
                #endregion

                tbxOtroPerJuridica.Text = string.Empty;//dtInfo.Rows[0][""].ToString().Trim();

                #region Soc. Comercial
                for (int i = 0; i < ddlSociedadComercial.Items.Count; i++)
                {
                    ddlSociedadComercial.SelectedIndex = i;
                    if (ddlSociedadComercial.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJSociedadComercial"].ToString().Trim())
                        break;
                }
                #endregion

                tbxActivEconomInfoJur.Text = dtInfo.Rows[0]["PJActividadEconomica"].ToString().Trim();
                tbxCIIUInfoJur.Text = dtInfo.Rows[0]["PJCIIU"].ToString().Trim();
                tbxCapitalInfoJur.Text = dtInfo.Rows[0]["PJCapitalSocial"].ToString().Trim();
                tbxFechaConstitucionInfoJur.Text = dtInfo.Rows[0]["PJFechaConstitucion"].ToString().Trim();
                tbxDocConstitucionInfoJur.Text = dtInfo.Rows[0]["PJDocumentoConstitucion"].ToString().Trim();
                tbxFechaRegInfoJur.Text = dtInfo.Rows[0]["PJFechaRegistro"].ToString().Trim();
                tbxMatriMerInfoJur.Text = dtInfo.Rows[0]["PJMatriculaMercantil"].ToString().Trim();
                tbxTelefonoInfoJur.Text = dtInfo.Rows[0]["PJTelefono1"].ToString().Trim();
                tbxEmailInfoJur.Text = dtInfo.Rows[0]["PJCorreoPrincipal"].ToString().Trim();
                tbxRegSuperSolInfoJur.Text = dtInfo.Rows[0]["PJRegSuperSolidaria"].ToString().Trim();

                #region Representantes legales
                #region Rep Legal Ppal
                tbxRepLegalPpalInfoJur.Text = dtInfo.Rows[0]["PJNombresRepLegalPpal"].ToString().Trim();

                #region Tipo Doc Rep Legal Ppal
                for (int i = 0; i < ddlTipoDocRepLegalPpalInfoJur.Items.Count; i++)
                {
                    ddlTipoDocRepLegalPpalInfoJur.SelectedIndex = i;
                    if (ddlTipoDocRepLegalPpalInfoJur.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegalPpal"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRepLegalPpalInfoJur.Text = dtInfo.Rows[0]["PJDocumentoRepLegalPpal"].ToString().Trim();
                #endregion

                #region Rep Legal 1
                tbxRepLegal1InfoJur.Text = dtInfo.Rows[0]["PJNombresRepLegal1"].ToString().Trim();

                #region Tipo Doc Rep Legal 1
                for (int i = 0; i < ddlTipoDocRepLegal1InfoJur.Items.Count; i++)
                {
                    ddlTipoDocRepLegal1InfoJur.SelectedIndex = i;
                    if (ddlTipoDocRepLegal1InfoJur.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal1"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRepLegal1InfoJur.Text = dtInfo.Rows[0]["PJDocumentoRepLegal1"].ToString().Trim();
                #endregion

                #region Rep Legal 2
                tbxRepLegal2InfoJur.Text = dtInfo.Rows[0]["PJNombresRepLegal2"].ToString().Trim();

                #region Tipo Doc Rep Legal 2
                for (int i = 0; i < ddlTipoDocRepLegal2InfoJur.Items.Count; i++)
                {
                    ddlTipoDocRepLegal2InfoJur.SelectedIndex = i;
                    if (ddlTipoDocRepLegal2InfoJur.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal2"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRepLegal2InfoJur.Text = dtInfo.Rows[0]["PJDocumentoRepLegal2"].ToString().Trim();
                #endregion

                #region Rep Legal 3
                tbxRepLegal3InfoJur.Text = dtInfo.Rows[0]["PJNombresRepLegal3"].ToString().Trim();

                #region Tipo Doc Rep Legal 3
                for (int i = 0; i < ddlTipoDocRepLegal3InfoJur.Items.Count; i++)
                {
                    ddlTipoDocRepLegal3InfoJur.SelectedIndex = i;
                    if (ddlTipoDocRepLegal3InfoJur.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal3"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRepLegal3InfoJur.Text = dtInfo.Rows[0]["PJDocumentoRepLegal3"].ToString().Trim();
                #endregion

                #region Rep Legal 4
                tbxRepLegal4InfoJur.Text = dtInfo.Rows[0]["PJNombresRepLegal4"].ToString().Trim();

                #region Tipo Doc Rep Legal 4
                for (int i = 0; i < ddlTipoDocRepLegal4InfoJur.Items.Count; i++)
                {
                    ddlTipoDocRepLegal4InfoJur.SelectedIndex = i;
                    if (ddlTipoDocRepLegal4InfoJur.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegal4"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRepLegal4InfoJur.Text = dtInfo.Rows[0]["PJDocumentoRepLegal4"].ToString().Trim();
                #endregion

                tbxTelefonoRepLegalInfoJur.Text = dtInfo.Rows[0]["PJTelefonoRepLegal"].ToString().Trim();
                tbxDepCiuMunRepLegalInfoJur.Text = dtInfo.Rows[0]["PJDptoCiudadRepLegal"].ToString().Trim();

                #region Rep Legal suplente
                tbxRepLegalSuplInfoJur.Text = dtInfo.Rows[0]["PJNombresRepLegalSupl"].ToString().Trim();

                #region Tipo Doc Rep Legal suplente
                for (int i = 0; i < ddlTipoDocRepLegalSuplInfoJur.Items.Count; i++)
                {
                    ddlTipoDocRepLegalSuplInfoJur.SelectedIndex = i;
                    if (ddlTipoDocRepLegalSuplInfoJur.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocRepLegalSupl"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRepLegalSuplInfoJur.Text = dtInfo.Rows[0]["PJDocumentoRepLegalSupl"].ToString().Trim();
                #endregion

                tbxTelefonoRepLegalSuplInfoJur.Text = dtInfo.Rows[0]["PJTelefonoRepLegalSupl"].ToString().Trim();
                tbxDepCiuMunRepLegalSuplInfoJur.Text = dtInfo.Rows[0]["PJDptoCiudadRepLegalSupl"].ToString().Trim();
                #endregion

                #region Tipo empresa
                for (int i = 0; i < ddlTipoEmpresaInfoJur.Items.Count; i++)
                {
                    ddlTipoEmpresaInfoJur.SelectedIndex = i;
                    if (ddlTipoEmpresaInfoJur.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoEmpresa"].ToString().Trim())
                        break;
                }
                #endregion

                tbxActEconPpalInfoJur.Text = dtInfo.Rows[0]["PJDescActEcoPpal"].ToString().Trim();
                tbxCIIUEmpInfoJur.Text = dtInfo.Rows[0]["PJCodCIIU2"].ToString().Trim();

                #region Accionistas
                #region Accionista 1
                tbxRazonSocialAccionistas1.Text = dtInfo.Rows[0]["PJNombreAS1"].ToString().Trim();

                #region Tipo Doc.
                for (int i = 0; i < ddlTipoDocRazonSocialAccionistas1.Items.Count; i++)
                {
                    ddlTipoDocRazonSocialAccionistas1.SelectedIndex = i;
                    if (ddlTipoDocRazonSocialAccionistas1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS1"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRazonSocialAccionistas1.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS1"].ToString().Trim();
                #endregion

                #region Accionista 2
                tbxRazonSocialAccionistas2.Text = dtInfo.Rows[0]["PJNombreAS2"].ToString().Trim();

                #region Tipo Doc.
                for (int i = 0; i < ddlTipoDocRazonSocialAccionistas2.Items.Count; i++)
                {
                    ddlTipoDocRazonSocialAccionistas2.SelectedIndex = i;
                    if (ddlTipoDocRazonSocialAccionistas2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS2"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRazonSocialAccionistas2.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS2"].ToString().Trim();
                #endregion

                #region Accionista 3
                tbxRazonSocialAccionistas3.Text = dtInfo.Rows[0]["PJNombreAS3"].ToString().Trim();

                #region Tipo Doc.
                for (int i = 0; i < ddlTipoDocRazonSocialAccionistas3.Items.Count; i++)
                {
                    ddlTipoDocRazonSocialAccionistas3.SelectedIndex = i;
                    if (ddlTipoDocRazonSocialAccionistas3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS3"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRazonSocialAccionistas3.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS3"].ToString().Trim();
                #endregion

                #region Accionista 4
                tbxRazonSocialAccionistas4.Text = dtInfo.Rows[0]["PJNombreAS4"].ToString().Trim();

                #region Tipo Doc.
                for (int i = 0; i < ddlTipoDocRazonSocialAccionistas4.Items.Count; i++)
                {
                    ddlTipoDocRazonSocialAccionistas4.SelectedIndex = i;
                    if (ddlTipoDocRazonSocialAccionistas4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS4"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRazonSocialAccionistas4.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS4"].ToString().Trim();
                #endregion

                #region Accionista 5
                tbxRazonSocialAccionistas5.Text = dtInfo.Rows[0]["PJNombreAS5"].ToString().Trim();

                #region Tipo Doc.
                for (int i = 0; i < ddlTipoDocRazonSocialAccionistas5.Items.Count; i++)
                {
                    ddlTipoDocRazonSocialAccionistas5.SelectedIndex = i;
                    if (ddlTipoDocRazonSocialAccionistas5.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS5"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroDocRazonSocialAccionistas5.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS5"].ToString().Trim();
                #endregion

                #region Info Financ.
                tbxIngMenAccionistas.Text = dtInfo.Rows[0]["PJIngresosMensuales"].ToString().Trim();
                tbxTotActivosAccionistas.Text = dtInfo.Rows[0]["PJActivos"].ToString().Trim();
                tbxEgrMenAccionistas.Text = dtInfo.Rows[0]["PJEgresoMensuales"].ToString().Trim();
                tbxTotPasivosAccionistas.Text = dtInfo.Rows[0]["PJPasivos"].ToString().Trim();
                tbxOtrosIngMenAccionistas.Text = dtInfo.Rows[0]["PJOtrosIngresos"].ToString().Trim();
                tbxConceptOtrosIngMenAccionistas.Text = dtInfo.Rows[0]["PJConceptoOtrosIngresos"].ToString().Trim();
                #endregion
                #endregion
            }
            #endregion

            #region Op. Moneda Ext
            dtInfo = cKnowClient.InfoFormPF(intIdConocimientoCliente);
            if (dtInfo.Rows.Count > 0)
            {
                #region Op. Moneda Ext
                for (int i = 0; i < ddlOpMonedaExt.Items.Count; i++)
                {
                    ddlOpMonedaExt.SelectedIndex = i;
                    if (ddlOpMonedaExt.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim())
                        break;
                }
                #endregion

                tbxCualOpMonedaExt.Text = dtInfo.Rows[0]["OtroTipoTransaccion"].ToString().Trim();

                #region Cuentas Moneda Ext.
                for (int i = 0; i < ddlCuentasMonedaExt.Items.Count; i++)
                {
                    ddlCuentasMonedaExt.SelectedIndex = i;
                    if (ddlCuentasMonedaExt.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PFCtaMonedaExtra"].ToString().Trim())
                        break;
                }
                #endregion

                tbxNroCuentaMonedaExt.Text = dtInfo.Rows[0]["PFNroCtaMonedaExtra"].ToString().Trim();
                tbxBancoMonedaExt.Text = dtInfo.Rows[0]["PFBancoCtaMonedaExtra"].ToString().Trim();
                tbxCiudadMonedaExt.Text = dtInfo.Rows[0]["PFCiudadCtaMonedaExtra"].ToString().Trim();
                tbxPaisMonedaExt.Text = dtInfo.Rows[0]["PFPaisCtaMonedaExtra"].ToString().Trim();
                tbxMonMonedaExt.Text = dtInfo.Rows[0]["PFMonedaCtaMonedaExtra"].ToString().Trim();
            }
            #endregion

            #region Drogueria
            dtInfo = cKnowClient.InfoFormDrogueria(intIdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                #region Drog Ppal
                tbxNombreDrogueriaPpal.Text = dtInfo.Rows[0]["NombreDrogueriaPpal"].ToString().Trim();
                tbxNITDrogueriaPpal.Text = dtInfo.Rows[0]["NitDrogueriaPpal"].ToString().Trim();
                tbxDirDrogueriaPpal.Text = dtInfo.Rows[0]["DirDrogueriaPpal"].ToString().Trim();
                tbxDptoDrogueriaPpal.Text = dtInfo.Rows[0]["DptoDrogueriaPpal"].ToString().Trim();
                tbxCiudadDrogueriaPpal.Text = dtInfo.Rows[0]["CiudadDrogueriaPpal"].ToString().Trim();
                tbxBarrioDrogueriaPpal.Text = dtInfo.Rows[0]["BarrioDrogueriaPpal"].ToString().Trim();
                tbxTelefonoDrogueriaPpal.Text = dtInfo.Rows[0]["TelDrogueriaPpal"].ToString().Trim();
                #endregion

                #region Departamento y Ciudad Drog Ppal

                if (dtInfo.Rows[0]["DptoDrogueriaPpal"].ToString() != string.Empty)
                {
                    for (int i = 0; i < DDLDptoDrogueriaPpal.Items.Count; i++)
                    {
                        DDLDptoDrogueriaPpal.SelectedIndex = i;
                        if (DDLDptoDrogueriaPpal.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["DptoDrogueriaPpal"].ToString())
                        {
                            DDLCiudadDrogueriaPpal.DataBind();
                            for (int ic = 0; ic < DDLCiudadDrogueriaPpal.Items.Count; ic++)
                            {
                                DDLCiudadDrogueriaPpal.SelectedIndex = ic;
                                if (DDLCiudadDrogueriaPpal.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["CiudadDrogueriaPpal"].ToString())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            DDLDptoDrogueriaPpal.SelectedIndex = 0;
                        }
                    }
                }
                #endregion

                #region Drog 1
                tbxNombreDrogueria2.Text = dtInfo.Rows[0]["NombreDrogueria1"].ToString().Trim();
                tbxDirDrogueria2.Text = dtInfo.Rows[0]["DirDrogueria1"].ToString().Trim();
                tbxDptoDrogueria2.Text = dtInfo.Rows[0]["DptoDrogueria1"].ToString().Trim();
                tbxCiudadDrogueria2.Text = dtInfo.Rows[0]["CiudadDrogueria1"].ToString().Trim();
                tbxBarrioDrogueria2.Text = dtInfo.Rows[0]["BarrioDrogueria1"].ToString().Trim();
                tbxTelefonoDrogueria2.Text = dtInfo.Rows[0]["TelDrogueria1"].ToString().Trim();
                #endregion

                #region Departamento y Ciudad Drog Ppal 2

                if (dtInfo.Rows[0]["DptoDrogueria1"].ToString() != string.Empty)
                {
                    for (int i = 0; i < DDLDptoDrogueria2.Items.Count; i++)
                    {
                        DDLDptoDrogueria2.SelectedIndex = i;
                        if (DDLDptoDrogueria2.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["DptoDrogueria1"].ToString())
                        {
                            DDLCiudadDrogueria2.DataBind();
                            for (int ic = 0; ic < DDLCiudadDrogueria2.Items.Count; ic++)
                            {
                                DDLCiudadDrogueria2.SelectedIndex = ic;
                                if (DDLCiudadDrogueria2.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["CiudadDrogueria1"].ToString())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            DDLDptoDrogueria2.SelectedIndex = 0;
                        }
                    }
                }
                #endregion

                #region Drog 2
                tbxNombreDrogueria3.Text = dtInfo.Rows[0]["NombreDrogueria2"].ToString().Trim();
                tbxDirDrogueria3.Text = dtInfo.Rows[0]["DirDrogueria2"].ToString().Trim();
                tbxDptoDrogueria3.Text = dtInfo.Rows[0]["DptoDrogueria2"].ToString().Trim();
                tbxCiudadDrogueria3.Text = dtInfo.Rows[0]["CiudadDrogueria2"].ToString().Trim();
                tbxBarrioDrogueria3.Text = dtInfo.Rows[0]["BarrioDrogueria2"].ToString().Trim();
                tbxTelefonoDrogueria3.Text = dtInfo.Rows[0]["TelDrogueria2"].ToString().Trim();
                #endregion

                #region Departamento y Ciudad Drog Ppal 3

                if (dtInfo.Rows[0]["DptoDrogueria1"].ToString() != string.Empty)
                {
                    for (int i = 0; i < DDLDptoDrogueria3.Items.Count; i++)
                    {
                        DDLDptoDrogueria3.SelectedIndex = i;
                        if (DDLDptoDrogueria3.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["DptoDrogueria2"].ToString())
                        {
                            DDLCiudadDrogueria3.DataBind();
                            for (int ic = 0; ic < DDLCiudadDrogueria3.Items.Count; ic++)
                            {
                                DDLCiudadDrogueria3.SelectedIndex = ic;
                                if (DDLCiudadDrogueria3.SelectedItem.Text.ToString().Trim() == dtInfo.Rows[0]["CiudadDrogueria2"].ToString())
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            DDLDptoDrogueria3.SelectedIndex = 0;
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region Sometimiento
            dtInfo = cKnowClient.InfoFormSometimiento(intIdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                tbxAportesXPagar.Text = dtInfo.Rows[0]["AportesSoc"].ToString().Trim();
                tbxVlrAperturaCtaCopicredito.Text = dtInfo.Rows[0]["VlrAperturaCopicredito"].ToString().Trim();
                tbxCompromisoAhorroMen.Text = dtInfo.Rows[0]["CompromisoAhorroMen"].ToString().Trim();
                tbxCuotaAdmision.Text = dtInfo.Rows[0]["CuotaAdm"].ToString().Trim();
                tbxCuotaAfilAsocoldro.Text = dtInfo.Rows[0]["CuotaAfilAsocoldro"].ToString().Trim();
                tbxTotalCompromisos.Text = dtInfo.Rows[0]["Total"].ToString().Trim();
                tbxFechaEntregaForm.Text = dtInfo.Rows[0]["FechaEntregaForm"].ToString().Trim();
            }
            #endregion

            #region Espacio Copidrogas
            dtInfo = cKnowClient.InfoFormCopidrogas(intIdConocimientoCliente);
            if (dtInfo.Rows.Count > 0)
            {
                #region Admision
                tbxActoNoComAdm.Text = dtInfo.Rows[0]["ActaNoAdmision"].ToString().Trim();
                tbxFechaActoComAdm.Text = dtInfo.Rows[0]["FechaAdmision"].ToString().Trim();
                tbxFavorableComAdm.Text = dtInfo.Rows[0]["FavorableAdmision"].ToString().Trim();
                tbxAplazadoComAdm.Text = dtInfo.Rows[0]["AplazadoAdmision"].ToString().Trim();
                tbxDesfavorableComAdm.Text = dtInfo.Rows[0]["DesfavorableAdmision"].ToString().Trim();
                tbxVisitaEfectuadaPorComAdm.Text = dtInfo.Rows[0]["VisitaEfectuadaAdmision"].ToString().Trim();
                tbxObservacionesComAdm.Text = dtInfo.Rows[0]["ObservacionesAdmision"].ToString().Trim();
                tbxNombreCoordinador1ComAdm.Text = dtInfo.Rows[0]["NombreCoordinadorAdmision"].ToString().Trim();
                tbxNombreSecretario1ComAdm.Text = dtInfo.Rows[0]["NombreSecretarioAdmision"].ToString().Trim();
                #endregion

                #region Administracion
                tbxActoNoConAdm.Text = dtInfo.Rows[0]["ActaNoAdmon"].ToString().Trim();
                tbxFechaActoConAdm.Text = dtInfo.Rows[0]["FechaAdmon"].ToString().Trim();
                tbxFavorableConAdm.Text = dtInfo.Rows[0]["FavorableAdmon"].ToString().Trim();
                tbxAplazadoConAdm.Text = dtInfo.Rows[0]["AplazadoAdmon"].ToString().Trim();
                tbxDesfavorableConAdm.Text = dtInfo.Rows[0]["DesfavorableAdmon"].ToString().Trim();
                tbxVisitaEfectuadaPorConAdm.Text = dtInfo.Rows[0]["VisitaEfectuadaAdmon"].ToString().Trim();
                tbxObservacionesConAdm.Text = dtInfo.Rows[0]["ObservacionesAdmon"].ToString().Trim();
                tbxNombreCoordinador1ConAdm.Text = dtInfo.Rows[0]["NombreCoordinadorAdmon"].ToString().Trim();
                tbxNombreSecretario1ConAdm.Text = dtInfo.Rows[0]["NombreSecretarioAdmon"].ToString().Trim();
                #endregion
            }
            #endregion

            #region Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(intIdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                tbxFechaVerEntrevista1.Text = dtInfo.Rows[0]["FechaEntrevista"].ToString().Trim();
                tbxObsEntrevista1.Text = dtInfo.Rows[0]["Observaciones1"].ToString().Trim();
                tbxVerificaEntrevista1.Text = dtInfo.Rows[0]["NombreResponsable"].ToString().Trim();
                tbxFechaVerEntrevista2.Text = dtInfo.Rows[0]["FechaVerificacion"].ToString().Trim();
                tbxObsEntrevista2.Text = dtInfo.Rows[0]["Observaciones2"].ToString().Trim();
                tbxVerificaEntrevista2.Text = dtInfo.Rows[0]["NombreVerifica"].ToString().Trim();

                #region Valida Firma
                for (int i = 0; i < ddlValidaFirma.Items.Count; i++)
                {
                    ddlValidaFirma.SelectedIndex = i;
                    if (ddlValidaFirma.SelectedItem.Text.Trim() == dtInfo.Rows[0]["ValidaFirma"].ToString().Trim())
                        break;
                }
                #endregion

                #region Valida Huella
                for (int i = 0; i < ddlValidaHuella.Items.Count; i++)
                {
                    ddlValidaHuella.SelectedIndex = i;
                    if (ddlValidaHuella.SelectedItem.Text.Trim() == dtInfo.Rows[0]["ValidaHuella"].ToString().Trim())
                        break;
                }
                #endregion

                #region Valida Entrevista
                for (int i = 0; i < ddlValidaEntrevista.Items.Count; i++)
                {
                    ddlValidaEntrevista.SelectedIndex = i;
                    if (ddlValidaEntrevista.SelectedItem.Text.Trim() == dtInfo.Rows[0]["ValidaEntrevista"].ToString().Trim())
                        break;
                }
                #endregion
            }
            #endregion

            tbConsulta.Visible = false;
            tbFormulario1.Visible = true;
        }

        private void mtdActualizarFormulario()
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
                DDLCiudadDomPpal.SelectedItem.Text.Trim(), tbxTelefonoInfoPer.Text.Trim(), tbxTelCelInfoPer.Text.Trim(),
                ddlAdmRecPublicos.SelectedValue.ToString().Trim(), ddlGrPoderPublico.SelectedValue.ToString().Trim(), ddlReconPubGral.SelectedValue.ToString().Trim(),
                string.Empty, tbxIngMenInfoPer.Text.Trim(), tbxTotActivosInfoPer.Text.Trim(), tbxEgrMenInfoPer.Text.Trim(), tbxTotPasivosInfoPer.Text.Trim(),
                tbxOtrosIngInfoPer.Text.Trim(), tbxConceptoOtrosIngInfoPer.Text.Trim(), ddlSexo.SelectedValue.ToString().Trim(),
                tbxEmailInfoPer.Text.Trim(), tbxLugarFechaNmto.Text.Trim(), ddlEstadoCivil.SelectedValue.ToString().Trim(), DDLDptoDomPpal.SelectedItem.Text.Trim(),
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
        }

        #endregion

        protected void DropDownList39_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox110.Text = string.Empty;
            TextBox111.Text = string.Empty;

            if (DropDownList39.SelectedValue == "1")
            {
                //Persona Natural
                Label188.Visible = false;
                Label189.Visible = false;
                Label190.Visible = false;
                Label191.Visible = false;
                TextBox99.Visible = false;
                TextBox103.Visible = false;
                TextBox105.Visible = false;
                TextBox106.Visible = false;
                
                //Persona Juridica
                Label195.Visible = true;
                Label196.Visible = true;
                TextBox110.Visible = true;
                TextBox111.Visible = true;
            }
            else
            {
                //Persona Natural
                Label188.Visible = true;
                Label189.Visible = true;
                Label190.Visible = true;
                Label191.Visible = true;
                TextBox99.Visible = true;
                TextBox103.Visible = true;
                TextBox105.Visible = true;
                TextBox106.Visible = true;

                //Persona Juridica
                Label195.Visible = false;
                Label196.Visible = false;
                TextBox110.Visible = false;
                TextBox111.Visible = false;
            }
        }
    }
}