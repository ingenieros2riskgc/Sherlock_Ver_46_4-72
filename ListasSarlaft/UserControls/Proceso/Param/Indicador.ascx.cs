using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class Indicador : System.Web.UI.UserControl
    {
        string IdFormulario = "4012";
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;

        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;

        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

        private DataTable infoGrid4;
        private int rowGrid4;
        private int pagIndex4;

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

        private DataTable InfoGrid2
        {
            get
            {
                infoGrid2 = (DataTable)ViewState["infoGrid2"];
                return infoGrid2;
            }
            set
            {
                infoGrid2 = value;
                ViewState["infoGrid2"] = infoGrid2;
            }
        }

        private int RowGrid2
        {
            get
            {
                rowGrid2 = (int)ViewState["rowGrid2"];
                return rowGrid2;
            }
            set
            {
                rowGrid2 = value;
                ViewState["rowGrid2"] = rowGrid2;
            }
        }

        private int PagIndex2
        {
            get
            {
                pagIndex2 = (int)ViewState["pagIndex2"];
                return pagIndex2;
            }
            set
            {
                pagIndex2 = value;
                ViewState["pagIndex2"] = pagIndex2;
            }
        }

        private DataTable InfoGrid3
        {
            get
            {
                infoGrid3 = (DataTable)ViewState["infoGrid3"];
                return infoGrid3;
            }
            set
            {
                infoGrid3 = value;
                ViewState["infoGrid3"] = infoGrid3;
            }
        }

        private int RowGrid3
        {
            get
            {
                rowGrid3 = (int)ViewState["rowGrid3"];
                return rowGrid3;
            }
            set
            {
                rowGrid3 = value;
                ViewState["rowGrid3"] = rowGrid3;
            }
        }

        private int PagIndex3
        {
            get
            {
                pagIndex3 = (int)ViewState["pagIndex3"];
                return pagIndex3;
            }
            set
            {
                pagIndex3 = value;
                ViewState["pagIndex3"] = pagIndex3;
            }
        }

        private DataTable InfoGrid4
        {
            get
            {
                infoGrid4 = (DataTable)ViewState["infoGrid4"];
                return infoGrid4;
            }
            set
            {
                infoGrid4 = value;
                ViewState["infoGrid4"] = infoGrid4;
            }
        }

        private int RowGrid4
        {
            get
            {
                rowGrid4 = (int)ViewState["rowGrid4"];
                return rowGrid4;
            }
            set
            {
                rowGrid4 = value;
                ViewState["rowGrid4"] = rowGrid4;
            }
        }

        private int PagIndex4
        {
            get
            {
                pagIndex4 = (int)ViewState["pagIndex4"];
                return pagIndex4;
            }
            set
            {
                pagIndex4 = value;
                ViewState["pagIndex4"] = pagIndex4;
            }
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["IdIndicador"] == null)
                    {
                        Global.mtdLimpiarListaCalidad();

                        mtdHabilitarBotonesPpal(4);
                        mtdInicializarValores();

                        if (!mtdCargarIndicador(ref strErrMsg))
                            omb.ShowMessage("No hay indicadores para cargar", 2, "Atención");

                        if (mtdCargarObjetivo(ref strErrMsg))
                            omb.ShowMessage(strErrMsg, 3, "Atención");
                    }
                    else {
                        mtdInicializarValores();
                        LidIndicador.Text = Request.QueryString["IdIndicador"];
                        mtdShowIndicador();
                    }
                }
            }
        }

        #region Gridview
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
                    break;
                case "Activar":
                    mtdActivar();
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid2 = (Convert.ToInt16(GridView2.PageSize) * PagIndex2) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    tbxObjetivo.Text = InfoGrid2.Rows[RowGrid2][1].ToString().Trim();
                    lblIdObjetivo.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid3 = (Convert.ToInt16(GridView3.PageSize) * PagIndex3) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificarVar();
                    break;
                case "Activar":
                    mtdActivarVar();
                    break;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //RowGrid4 = new int();
            RowGrid4 = (Convert.ToInt16(GridView4.PageSize) * PagIndex4) + Convert.ToInt16(e.CommandArgument);
            string IDValor = InfoGrid4.Rows[RowGrid4][0].ToString();
            //string contenidoCelda = GridView4.SelectedValue.ToString();
            
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdSeleccionaVar();
                    break;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridIndicador(ref strErrMsg);
        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex3 = e.NewPageIndex;
            GridView3.PageIndex = PagIndex3;
            GridView3.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridVariable(ref strErrMsg);
        }

        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex4 = e.NewPageIndex;
            GridView4.PageIndex = PagIndex4;
            GridView4.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridVariableActivas(ref strErrMsg);
        }
        #endregion

        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (ddlProceso.SelectedValue == "0")
                rfvProceso.Enabled = false;
            else
                rfvProceso.Enabled = true;

            ddlSubproceso.Items.Clear();

            if (mtdLoadDDLSubproceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlSubproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProceso.SelectedValue == "0")
                rfvSubproceso.Enabled = false;
            else
                rfvSubproceso.Enabled = true;
        }
        #endregion

        #region Buttons

        #region Indicador
        /// <summary>
        /// Permite habilitar los campos para crear un nuevo Indicador.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                TabDetalles.ActiveTabIndex = 0;

                tbxDescripcion.Focus();

                mtdLimpiarCamposGral();
                mtdCargarDDLs();
                mtdCargarVariable(ref strErrMsg);

                tbxId.Enabled = false;
                tbxUsuario.Enabled = false;
                tbxFecha.Enabled = false;

                mtdHabilitarBotonesPpal(1);

                filaGrid.Visible = false;
                filaDetalle.Visible = true;

                Global.LstSenalVar = new List<object>();
                Global.BooIsOkFormula = false;
            }
        }

        /// <summary>
        /// Permite la insercion del Indicador.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarIndicador(ref strErrMsg))
                {
                    omb.ShowMessage("Indicador registrado exitosamente.", 3, "Atención");
                    filaGrid.Visible = true;
                    filaDetalle.Visible = false;
                    mtdHabilitarBotonesPpal(4);

                    mtdLimpiarTodos();
                    mtdCargarIndicador(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar el indicador.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        /// <summary>
        /// Permite Actualizar el Indicador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (mtdActualizarIndicador(ref strErrMsg))
                    {
                        omb.ShowMessage("Indicador modificado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;
                        mtdHabilitarBotonesPpal(4);

                        mtdLimpiarTodos();
                        mtdCargarIndicador(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el indicador.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        /// <summary>
        /// Permite modificar el estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModificarEstado_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            mpeMsgBox.Hide();

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (Session["EstadoPerfil"].ToString() == "I")
                    {
                        if (mtdActualizarEstado(ref strErrMsg))
                        {
                            omb.ShowMessage("Indicador (in)activado exitosamente.", 3, "Atención");
                            filaGrid.Visible = true;
                            filaDetalle.Visible = false;
                            mtdHabilitarBotonesPpal(4);

                            mtdLimpiarCamposGral();
                            mtdCargarIndicador(ref strErrMsg);
                        }
                        else
                            omb.ShowMessage(strErrMsg, 1, "Atención");
                    }
                    else
                    {
                        if (Session["EstadoPerfil"].ToString() == "V")
                        {
                            if (mtdActualizarEstadoVar(ref strErrMsg))
                            {
                                omb.ShowMessage("Variable (in)activada exitosamente.", 3, "Atención");

                                mtdCargarVariable(ref strErrMsg);
                                mtdLimpiarCamposVar();
                                TblDetallesVar.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al inactivar el indicador.<br/>Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            mtdHabilitarBotonesPpal(4);

            mtdLimpiarTodos();
            Global.mtdLimpiarListaCalidad();

            mtdHabilitarBotonesPpal(4);
            mtdInicializarValores();

            if (!mtdCargarIndicador(ref strErrMsg))
                omb.ShowMessage("No hay indicadores para cargar", 2, "Atención");

            if (mtdCargarObjetivo(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 3, "Atención");
        }
        #endregion

        #region General
        protected void btnSiguienteGral_Click(object sender, ImageClickEventArgs e)
        {
            TabDetalles.ActiveTabIndex = 1;
        }
        #endregion

        #region Variables
        protected void btnPrevioVar_Click(object sender, ImageClickEventArgs e)
        {
            TabDetalles.ActiveTabIndex = 0;
        }

        protected void btnSiguienteVar_Click(object sender, ImageClickEventArgs e)
        {
            TabDetalles.ActiveTabIndex = 2;
        }

        protected void btnNuevaVar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                TblDetallesVar.Visible = true;
                btnGuardarVar.Visible = true;
                btnActualizarVar.Visible = false;

                chbEstadoVar.Enabled = true;

                mtdLimpiarCamposVar();
            }
        }

        /// <summary>
        /// Permite Actualizar la variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarVar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (mtdInsertarVariable(ref strErrMsg))
                    {
                        omb.ShowMessage("Variable registrada exitosamente.", 3, "Atención");
                        mtdCargarVariable(ref strErrMsg);
                        mtdLimpiarCamposVar();
                        TblDetallesVar.Visible = false;

                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al crear la variable.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnActualizarVar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (mtdActualizarVariable(ref strErrMsg))
                    {
                        omb.ShowMessage("Variable modificada exitosamente.", 3, "Atención");
                        TblDetallesVar.Visible = false;

                        mtdLimpiarCamposVar();
                        mtdCargarVariable(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar la Variable.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }

        }

        protected void btnCancelarVar_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposVar();
            TblDetallesVar.Visible = false;
        }
        #endregion

        #region Formula
        protected void btnPrevioForm_Click(object sender, ImageClickEventArgs e)
        {
            TabDetalles.ActiveTabIndex = 1;
        }

        protected void btnSiguienteForm_Click(object sender, ImageClickEventArgs e)
        {
            TabDetalles.ActiveTabIndex = 3;
        }

        protected void btnSelectCampoTexto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxCampoTexto.Text.Trim()))
            {
                clsFormula objFormula = new clsFormula(0, 3, 0, tbxCampoTexto.Text.Trim(), Global.intPosicion, 1, string.Empty);
                mtdIngresarListaFormula(objFormula);

                mtdLlenarCampoFormula(tbxCampoTexto.Text.Trim());

                tbxCampoTexto.Text = string.Empty;
            }
        }

        protected void btnOperMas_Click(object sender, EventArgs e)
        {
            clsFormula objFormula = new clsFormula(0, 2, 0, "+", Global.intPosicion, 1, string.Empty);
            mtdIngresarListaFormula(objFormula);

            mtdLlenarCampoFormula("+");
        }

        protected void btnMenos_Click(object sender, EventArgs e)
        {
            clsFormula objFormula = new clsFormula(0, 2, 0, "-", Global.intPosicion, 1, string.Empty);
            mtdIngresarListaFormula(objFormula);

            mtdLlenarCampoFormula("-");
        }

        protected void btnOperPor_Click(object sender, EventArgs e)
        {
            clsFormula objFormula = new clsFormula(0, 2, 0, "X", Global.intPosicion, 1, string.Empty);
            mtdIngresarListaFormula(objFormula);

            mtdLlenarCampoFormula("X");
        }

        protected void btnOperDividir_Click(object sender, EventArgs e)
        {
            clsFormula objFormula = new clsFormula(0, 2, 0, "/", Global.intPosicion, 1, string.Empty);
            mtdIngresarListaFormula(objFormula);

            mtdLlenarCampoFormula("/");
        }

        protected void btnOperParentIzq_Click(object sender, EventArgs e)
        {
            clsFormula objFormula = new clsFormula(0, 2, 0, "(", Global.intPosicion, 1, string.Empty);
            mtdIngresarListaFormula(objFormula);

            mtdLlenarCampoFormula("(");
        }

        protected void btnOperParentDer_Click(object sender, EventArgs e)
        {
            clsFormula objFormula = new clsFormula(0, 2, 0, ")", Global.intPosicion, 1, string.Empty);
            mtdIngresarListaFormula(objFormula);

            mtdLlenarCampoFormula(")");
        }

        protected void btnOperPorcentaje_Click(object sender, EventArgs e)
        {
            clsFormula objFormula = new clsFormula(0, 2, 0, "%", Global.intPosicion, 1, string.Empty);
            mtdIngresarListaFormula(objFormula);

            mtdLlenarCampoFormula("%");
        }

        protected void btnOperBorrar_Click(object sender, EventArgs e)
        {
            Global.mtdLimpiarListaCalidad();

            mtdLimpiarCampoFormula();
        }

        #endregion

        #region Cumplimiento
        protected void btnPrevioCump_Click(object sender, ImageClickEventArgs e)
        {
            TabDetalles.ActiveTabIndex = 2;
        }

        protected void btnSiguienteCump_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxId.Text.Trim()))
                mtdHabilitarBotonesPpal(2);//Inserta
            else
                mtdHabilitarBotonesPpal(3);//Actualiza
        }
        #endregion

        #endregion

        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            PagIndex2 = 0;
            PagIndex3 = 0;
            PagIndex4 = 0;
        }

        #region Limpiar Campos
        private void mtdLimpiarTodos()
        {
            mtdLimpiarCamposGral();
            mtdLimpiarCamposVar();
            mtdLimpiarCampoFormula();
            mtdLimpiarCamposCumplimiento();
        }

        private void mtdLimpiarCamposVar()
        {
            tbxCodigoVar.Text = string.Empty;
            tbxDescripcionVar.Text = string.Empty;
            ddlFormato.SelectedValue = "0";

        }

        private void mtdLimpiarCampoFormula()
        {
            tbxCampoFormula.Text = string.Empty;
            tbxCampoTexto.Text = string.Empty;
        }

        private void mtdLimpiarCamposCumplimiento()
        {
            tbxRangoMinUp.Text = string.Empty;
            tbxRangoMaxUp.Text = string.Empty;
            tbxCumpUp.Text = string.Empty;
            tbxIdCumpUp.Text = string.Empty;

            tbxRangoMinOdd.Text = string.Empty;
            tbxRangoMaxOdd.Text = string.Empty;
            tbxCumpOdd.Text = string.Empty;
            tbxIdCumpOdd.Text = string.Empty;

            tbxRangoMinDown.Text = string.Empty;
            tbxRangoMaxDown.Text = string.Empty;
            tbxCumpDown.Text = string.Empty;
            tbxIdCumpDown.Text = string.Empty;
        }

        private void mtdLimpiarCamposGral()
        {
            tbxId.Text = string.Empty;

            tbxProcIndica.Text = string.Empty;
            ddlCadenaValor.SelectedValue = "0";
            ddlMacroproceso.SelectedValue = "0";
            ddlProceso.SelectedValue = "0";
            ddlSubproceso.SelectedValue = "0";
            TXnombreIndicador.Text = string.Empty;
            tbxDescripcion.Text = string.Empty;

            ddlPeriodo.SelectedValue = "0";

            tbxMeta.Text = string.Empty;

            ChBEstado.Checked = true;

            lblIdObjetivo.Text = string.Empty;
            tbxObjetivo.Text = string.Empty;

            tbxUsuario.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }
        #endregion

        private void mtdHabilitarBotonesPpal(int intTipoH)
        {
            switch (intTipoH)
            {
                case 1:
                    btnImgInsertar.Visible = false;
                    btnImgActualizar.Visible = false;
                    btnImgCancelar.Visible = true;
                    break;
                case 2: //Inserta
                    btnImgInsertar.Visible = true;
                    btnImgActualizar.Visible = false;
                    btnImgCancelar.Visible = true;
                    break;
                case 3:
                    btnImgInsertar.Visible = false;
                    btnImgActualizar.Visible = true;
                    btnImgCancelar.Visible = true;
                    break;
                case 4:
                    btnImgInsertar.Visible = false;
                    btnImgActualizar.Visible = false;
                    btnImgCancelar.Visible = false;
                    break;
            }
        }

        #region Cargas

        #region DDLs
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;

            mtdLoadDDLPeriodos(ref strErrMsg);
            mtdLoadDDLFormato(ref strErrMsg);
            mtdLoadDDLCadenaValor(ref strErrMsg);
            mtdLoadDDLMacroProceso(ref strErrMsg);
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                ddlCadenaValor.Items.Insert(0, new ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                ddlMacroproceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstMacroproceso != null)
                    {
                        int intCounter = 1;


                        foreach (clsMacroproceso objMacroproceso in lstMacroproceso)
                        {
                            ddlMacroproceso.Items.Insert(intCounter, new ListItem(objMacroproceso.strNombreMacroproceso, objMacroproceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los macroprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {
                objMProceso = new clsMacroproceso(Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()), string.Empty, string.Empty, string.Empty,
                    true, 0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty);
                lstProceso = cProceso.mtdConsultarProceso(true, objMProceso, ref strErrMsg);

                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstProceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsProceso objProceso in lstProceso)
                        {
                            ddlProceso.Items.Insert(intCounter, new ListItem(objProceso.strNombreProceso, objProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los subprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLSubproceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsProceso objProceso = new clsProceso();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();
            #endregion Vars

            try
            {
                objProceso = new clsProceso(Convert.ToInt32(ddlProceso.SelectedValue.ToString().Trim()),
                    0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty);
                lstSubproceso = cSubproceso.mtdConsultarSubProceso(true, objProceso, ref strErrMsg);

                ddlSubproceso.Items.Clear();
                ddlSubproceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstSubproceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsSubproceso objSubProceso in lstSubproceso)
                        {
                            ddlSubproceso.Items.Insert(intCounter, new ListItem(objSubProceso.strNombreSubproceso, objSubProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Subprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Carga el DDL de los Periodos
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLPeriodos(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsPeriodicidad> lstPeriodos = new List<clsPeriodicidad>();
            clsPeriodicidadBLL cPeriodicidad = new clsPeriodicidadBLL();
            #endregion Vars

            try
            {
                lstPeriodos = cPeriodicidad.mtdConsultarPeriodos(ref strErrMsg);

                ddlPeriodo.Items.Clear();
                ddlPeriodo.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstPeriodos != null)
                    {
                        int intCounter = 1;

                        foreach (clsPeriodicidad objPeriodo in lstPeriodos)
                        {
                            ddlPeriodo.Items.Insert(intCounter, new ListItem(objPeriodo.strNombre, objPeriodo.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de los Periodos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Carga el DDL de los Formatos
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLFormato(ref string strErrMsg)
        {
            ddlFormato.Items.Clear();
            ddlFormato.Items.Insert(0, new ListItem("", "0"));
            ddlFormato.Items.Insert(1, new ListItem("#", "#"));
            ddlFormato.Items.Insert(2, new ListItem("$", "$"));
            ddlFormato.Items.Insert(3, new ListItem("%", "%"));

            return true;
        }
        #endregion

        #region Gridview Indicador
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarIndicador(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridIndicador();
            mtdLoadInfoGridIndicador(ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridIndicador()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreIndicador", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("strProceso", typeof(string));
            grid.Columns.Add("intIdPeriodicidad", typeof(string));
            grid.Columns.Add("intMeta", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdObjetivoCalidad", typeof(string));
            grid.Columns.Add("strDescObjetivo", typeof(string));
            grid.Columns.Add("intIdProcesoIndicador", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridIndicador(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsIndicadorBLL cIndicador = new clsIndicadorBLL();

            lstIndicador = cIndicador.mtdConsultarIndicador(ref strErrMsg);

            if (lstIndicador != null)
            {
                mtdLoadInfoGridIndicador(lstIndicador);
                GridView1.DataSource = lstIndicador;
                GridView1.PageIndex = PagIndex;
                GridView1.DataBind();
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadInfoGridIndicador(List<clsIndicador> lstIndicador)
        {
            foreach (clsIndicador objIndicador in lstIndicador)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objIndicador.intId.ToString().Trim(),
                    objIndicador.strNombreIndicador.ToString().Trim(),
                    objIndicador.strDescripcion.ToString().Trim(),
                    objIndicador.strProceso.ToString().Trim(),
                    objIndicador.intIdPeriodicidad.ToString().Trim(),
                    objIndicador.intMeta.ToString().Trim(),
                    objIndicador.booEstado.ToString().Trim(),
                    objIndicador.intIdObjetivoCalidad.ToString().Trim(),
                    objIndicador.strDescObjetivo.ToString().Trim(),
                    objIndicador.intIdProcesoIndicador.ToString().Trim(),
                    objIndicador.intIdUsuario.ToString().Trim(),
                    objIndicador.strNombreUsuario.ToString().Trim(),
                    objIndicador.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview Objetivos
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarObjetivo(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridObjetivo();
            mtdLoadInfoGridObjetivo(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridObjetivo()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGrid2 = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del objetivo al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridObjetivo(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsObjetivoCalidad> lstObjetivo = new List<clsObjetivoCalidad>();
            clsObjetivoBLL cObjetivo = new clsObjetivoBLL();

            lstObjetivo = cObjetivo.mtdConsultarObjetivoCalidad(true, ref strErrMsg);

            if (lstObjetivo != null)
            {
                mtdLoadInfoGridObjetivo(lstObjetivo);
                GridView2.DataSource = lstObjetivo;
                GridView2.DataBind();
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Objetivos</param>
        private void mtdLoadInfoGridObjetivo(List<clsObjetivoCalidad> lstObjetivo)
        {
            foreach (clsObjetivoCalidad objObjetivo in lstObjetivo)
            {
                InfoGrid2.Rows.Add(new Object[] {
                    objObjetivo.intId.ToString().Trim(),
                    objObjetivo.strDescripcion.ToString().Trim(),
                    objObjetivo.booEstado.ToString().Trim(),
                    objObjetivo.intIdUsuario.ToString().Trim(),
                    objObjetivo.strNombreUsuario.ToString().Trim(),
                    objObjetivo.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region GridView Variables
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarVariable(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridVariable();
            mtdLoadInfoGridVariable(ref strErrMsg);
            mtdLoadInfoGridVariableActivas(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridVariable()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("strFormato", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            DataTable grid2 = new DataTable();

            grid2.Columns.Add("intId", typeof(string));
            grid2.Columns.Add("strDescripcion", typeof(string));
            grid2.Columns.Add("strFormato", typeof(string));
            grid2.Columns.Add("booEstado", typeof(string));
            grid2.Columns.Add("intIdUsuario", typeof(string));
            grid2.Columns.Add("strNombreUsuario", typeof(string));
            grid2.Columns.Add("dtFechaRegistro", typeof(string));

            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGrid3 = grid;

            GridView4.DataSource = grid2;
            GridView4.DataBind();
            InfoGrid4 = grid2;

        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos de la Variable al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridVariable(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsVariable> lstVariable = new List<clsVariable>();
            clsVariableBLL cVariable = new clsVariableBLL();

            lstVariable = cVariable.mtdConsultarVariable(ref strErrMsg);

            if (lstVariable != null)
            {
                mtdLoadInfoGridVariable(lstVariable);
                GridView3.DataSource = lstVariable;
                GridView3.PageIndex = PagIndex3;
                GridView3.DataBind();
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con las Variables</param>
        private void mtdLoadInfoGridVariable(List<clsVariable> lstVariable)
        {
            foreach (clsVariable objVariable in lstVariable)
            {
                InfoGrid3.Rows.Add(new Object[] {
                    objVariable.intId.ToString().Trim(),
                    objVariable.strDescripcion.ToString().Trim(),
                    objVariable.strFormato.ToString().Trim(),
                    objVariable.booEstado.ToString().Trim(),
                    objVariable.intIdUsuario.ToString().Trim(),
                    objVariable.strNombreUsuario.ToString().Trim(),
                    objVariable.dtFechaRegistro.ToString().Trim()
                    });
            }
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos de la Variable al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridVariableActivas(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsVariable> lstVariableActivas = new List<clsVariable>();
            clsVariableBLL cVariable = new clsVariableBLL();

            lstVariableActivas = cVariable.mtdConsultarVariable(true, ref strErrMsg);

            if (lstVariableActivas != null)
            {
                mtdLoadInfoGridVariableActivas(lstVariableActivas);
                GridView4.DataSource = lstVariableActivas;
                GridView4.PageIndex = PagIndex4;
                GridView4.DataBind();
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con las Variables</param>
        private void mtdLoadInfoGridVariableActivas(List<clsVariable> lstVariable)
        {
            foreach (clsVariable objVariable in lstVariable)
            {
                InfoGrid4.Rows.Add(new Object[] {
                    objVariable.intId.ToString().Trim(),
                    objVariable.strDescripcion.ToString().Trim(),
                    objVariable.strFormato.ToString().Trim(),
                    objVariable.booEstado.ToString().Trim(),
                    objVariable.intIdUsuario.ToString().Trim(),
                    objVariable.strNombreUsuario.ToString().Trim(),
                    objVariable.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #endregion

        #region Indicador

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            bool booResult = false;
            string strErrMsg = string.Empty;

            filaGrid.Visible = false;
            filaDetalle.Visible = true;

            TabDetalles.ActiveTabIndex = 0;

            mtdLimpiarCamposGral();

            mtdHabilitarBotonesPpal(1);

            mtdCargarDDLs();
            mtdCargarObjetivo(ref strErrMsg);
            mtdCargarVariable(ref strErrMsg);

            tbxId.Enabled = false;
            tbxFecha.Enabled = false;
            tbxUsuario.Enabled = false;

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            TXnombreIndicador.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();
            tbxDescripcion.Text = InfoGrid.Rows[RowGrid][2].ToString().Trim();
            ddlPeriodo.SelectedValue = InfoGrid.Rows[RowGrid][4].ToString().Trim();
            tbxMeta.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();

            #region Estado
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][6].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            #region Objetivo
            lblIdObjetivo.Text = InfoGrid.Rows[RowGrid][7].ToString().Trim();
            tbxObjetivo.Text = InfoGrid.Rows[RowGrid][8].ToString().Trim();
            #endregion

            #region Procesos
            //Se DEBE CARGAR INFORMACION DEBE PROCESOS DEBE ACUERDO A LA INFORMACION DEL GRID
            tbxProcIndica.Text = InfoGrid.Rows[RowGrid][9].ToString().Trim();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador(Convert.ToInt32(InfoGrid.Rows[RowGrid][9].ToString().Trim()), 0, 0, 0, string.Empty);
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcIndicadorMod(objProcInd, ref strErrMsg);

            switch (objProcesos[0].ToString())
            {
                case "M":
                    clsMacroproceso objMP = (clsMacroproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objMP.intIdCadenaDeValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objMP.intId.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = "0";
                    break;
                case "P":
                    clsProceso objP = (clsProceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objP.intIdCadenaValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objP.intIdMacroProceso.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = objP.intId.ToString();
                    mtdLoadDDLSubproceso(ref strErrMsg);
                    ddlSubproceso.SelectedValue = "0";
                    break;
                case "S":
                    clsSubproceso objSP = (clsSubproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objSP.intIdCadenaValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objSP.intIdMacroProceso.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = objSP.intIdProceso.ToString();
                    mtdLoadDDLSubproceso(ref strErrMsg);
                    ddlSubproceso.SelectedValue = objSP.intId.ToString();
                    break;
            }
            #endregion

            tbxUsuario.Text = InfoGrid.Rows[RowGrid][11].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][12].ToString().Trim();

            /* Consultar Formula (InfoGrid.Rows[RowGrid][0].ToString().Trim()) */
            clsIndicador objIndicador = new clsIndicador(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty,string.Empty, 0, 0, true, 0, 0, 0, string.Empty);

            #region Formula
            booResult = mtdLlenarCamposFormula(objIndicador, ref strErrMsg);
            #endregion

            #region Cumplimiento
            booResult = mtdLlenarCamposCumplimiento(objIndicador, ref strErrMsg);
            #endregion

            if (!booResult)
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        /// <summary>
        /// Habilita los campos segun el request de ID indicador de la page VerCaracterizacion.
        /// </summary>
        private void mtdShowIndicador()
        {
            bool booResult = false;
            string strErrMsg = string.Empty;
            RowGrid = 0;
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsIndicadorBLL cIndicador = new clsIndicadorBLL();
            int intIdIndicador = Convert.ToInt32(Request.QueryString["IdIndicador"].ToString());
            lstIndicador = cIndicador.mtdConsultarIndicadorById(ref strErrMsg, intIdIndicador);
            mtdLoadGridIndicador();
            if (lstIndicador != null)
            {
                mtdLoadInfoGridIndicador(lstIndicador);
            }
            filaGrid.Visible = false;
            filaDetalle.Visible = true;

            TabDetalles.ActiveTabIndex = 0;

            mtdLimpiarCamposGral();

            mtdHabilitarBotonesPpal(1);

            mtdCargarDDLs();
            mtdCargarObjetivo(ref strErrMsg);
            mtdCargarVariable(ref strErrMsg);

            tbxId.Enabled = false;
            tbxFecha.Enabled = false;
            tbxUsuario.Enabled = false;

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            TXnombreIndicador.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();
            tbxDescripcion.Text = InfoGrid.Rows[RowGrid][2].ToString().Trim();
            ddlPeriodo.SelectedValue = InfoGrid.Rows[RowGrid][4].ToString().Trim();
            tbxMeta.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();

            #region Estado
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][6].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            #region Objetivo
            lblIdObjetivo.Text = InfoGrid.Rows[RowGrid][7].ToString().Trim();
            tbxObjetivo.Text = InfoGrid.Rows[RowGrid][8].ToString().Trim();
            #endregion

            #region Procesos
            //Se DEBE CARGAR INFORMACION DEBE PROCESOS DEBE ACUERDO A LA INFORMACION DEL GRID
            tbxProcIndica.Text = InfoGrid.Rows[RowGrid][9].ToString().Trim();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador(Convert.ToInt32(InfoGrid.Rows[RowGrid][9].ToString().Trim()), 0, 0, 0, string.Empty);
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcIndicadorMod(objProcInd, ref strErrMsg);

            switch (objProcesos[0].ToString())
            {
                case "M":
                    clsMacroproceso objMP = (clsMacroproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objMP.intIdCadenaDeValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objMP.intId.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = "0";
                    break;
                case "P":
                    clsProceso objP = (clsProceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objP.intIdCadenaValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objP.intIdMacroProceso.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = objP.intId.ToString();
                    mtdLoadDDLSubproceso(ref strErrMsg);
                    ddlSubproceso.SelectedValue = "0";
                    break;
                case "S":
                    clsSubproceso objSP = (clsSubproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objSP.intIdCadenaValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objSP.intIdMacroProceso.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = objSP.intIdProceso.ToString();
                    mtdLoadDDLSubproceso(ref strErrMsg);
                    ddlSubproceso.SelectedValue = objSP.intId.ToString();
                    break;
            }
            #endregion

            tbxUsuario.Text = InfoGrid.Rows[RowGrid][11].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][12].ToString().Trim();

            /* Consultar Formula (InfoGrid.Rows[RowGrid][0].ToString().Trim()) */
            clsIndicador objIndicador = new clsIndicador(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty, string.Empty, 0, 0, true, 0, 0, 0, string.Empty);

            #region Formula
            booResult = mtdLlenarCamposFormula(objIndicador, ref strErrMsg);
            #endregion

            #region Cumplimiento
            booResult = mtdLlenarCamposCumplimiento(objIndicador, ref strErrMsg);
            #endregion

            if (!booResult)
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        /// <summary>
        /// Realiza el proceso previo a actualizar el estado
        /// </summary>
        private void mtdActivar()
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                string strEstado = string.Empty;
                bool booEstado = InfoGrid.Rows[RowGrid][6].ToString().Trim() == "True" ? true : false;
                Session["EstadoPerfil"] = "I";
                
                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} el indicador?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Realiza la actualizacion de estado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no </returns>
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;

            clsIndicador objIndicador = new clsIndicador(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty,string.Empty, 0, 0,
                !(InfoGrid.Rows[RowGrid][6].ToString().Trim() == "True" ? true : false),
                0, 0, 0, string.Empty);

            clsIndicadorBLL cIndicador = new clsIndicadorBLL();

            booResult = cIndicador.mtdActualizarEstado(objIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la insercion del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        private bool mtdInsertarIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            int intIdProcInd = 0, intIdCalificacion = 0, intIdIndicador = 0;
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            clsCumplimientoBLL cCumplimiento = new clsCumplimientoBLL();
            #endregion

            #region Proceso del indicador
            booResult = mtdValidarProceso(0, ref objProcInd, ref strErrMsg);

            if (booResult)
                booResult = cProcInd.mtdInsertarProcesoIndicador(objProcInd, ref intIdProcInd, ref strErrMsg);
            #endregion

            #region Indicador
            if (booResult)
            {
                clsIndicador objIndicador = new clsIndicador(0,
                    TXnombreIndicador.Text.Trim(),
                    tbxDescripcion.Text.Trim(),
                    Convert.ToInt32(ddlPeriodo.SelectedValue.Trim()),
                    Convert.ToDecimal(tbxMeta.Text.Trim()),
                    ChBEstado.Checked,
                    Convert.ToInt32(lblIdObjetivo.Text.Trim()),
                    intIdProcInd,
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                    string.Empty);

                clsIndicadorBLL cIndicador = new clsIndicadorBLL();

                booResult = cIndicador.mtdInsertarIndicador(objIndicador, ref intIdIndicador, ref strErrMsg);
            }
            #endregion

            #region Formula
            if (booResult)
            {
                clsFormulaBLL cFormula = new clsFormulaBLL();
                booResult = cFormula.mtdInsertarFormula(Global.LstCalidad, intIdIndicador, ref strErrMsg);
            }
            #endregion

            #region Cumplimiento
            if (booResult)
            {
                List<clsDetalleCalificacion> lstDetalleCump = new List<clsDetalleCalificacion>();

                #region Semaforo Verde
                clsDetalleCalificacion objDetalleCump = new clsDetalleCalificacion(0, intIdIndicador,
                    Convert.ToDecimal(tbxRangoMinUp.Text.Trim()), Convert.ToDecimal(tbxRangoMaxUp.Text.Trim()), 1, tbxCumpUp.Text.Trim(),
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                lstDetalleCump.Add(objDetalleCump);
                #endregion

                #region Semaforo Amarillo
                objDetalleCump = new clsDetalleCalificacion(0, intIdIndicador,
                    Convert.ToDecimal(tbxRangoMinOdd.Text.Trim()), Convert.ToDecimal(tbxRangoMaxOdd.Text.Trim()), 2, tbxCumpOdd.Text.Trim(),
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                lstDetalleCump.Add(objDetalleCump);
                #endregion

                #region Semaforo Rojo
                objDetalleCump = new clsDetalleCalificacion(0, intIdIndicador,
                    Convert.ToDecimal(tbxRangoMinDown.Text.Trim()), Convert.ToDecimal(tbxRangoMaxDown.Text.Trim()), 3, tbxCumpDown.Text.Trim(),
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                lstDetalleCump.Add(objDetalleCump);
                #endregion

                booResult = cCumplimiento.mtdInsertarCumplimiento(lstDetalleCump, ref intIdCalificacion, ref strErrMsg);
            }
            #endregion

            return booResult;
        }

        /// <summary>
        /// Permite la actualizacion del Indicador
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdActualizarIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsIndicador objIndicador = new clsIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            clsCumplimientoBLL cCumplimiento = new clsCumplimientoBLL();
            #endregion

            #region Proceso del indicador
            booResult = mtdValidarProceso(Convert.ToInt32(tbxProcIndica.Text.Trim()), ref objProcInd, ref strErrMsg);

            if (booResult)
                booResult = cProcInd.mtdActualizarProcesoIndicador(objProcInd, ref strErrMsg);
            #endregion

            #region Indicador
            if (booResult)
            {
                objIndicador = new clsIndicador(Convert.ToInt32(tbxId.Text.Trim()),
                    TXnombreIndicador.Text.Trim(),
                   tbxDescripcion.Text.Trim(),
                   Convert.ToInt32(ddlPeriodo.SelectedValue.Trim()),
                   Convert.ToDecimal(tbxMeta.Text.Trim()),
                   ChBEstado.Checked,
                   Convert.ToInt32(lblIdObjetivo.Text.Trim()),
                   Convert.ToInt32(tbxProcIndica.Text.Trim()),
                   0, string.Empty);

                clsIndicadorBLL cIndicador = new clsIndicadorBLL();

                booResult = cIndicador.mtdActualizarIndicador(objIndicador, ref strErrMsg);
            }
            #endregion

            #region Cumplimiento
            if (booResult)
            {
                List<clsDetalleCalificacion> lstDetalleCump = new List<clsDetalleCalificacion>();

                #region Semaforo Verde
                clsDetalleCalificacion objDetalleCump = new clsDetalleCalificacion(
                   Convert.ToInt32(tbxIdCumpUp.Text.Trim()),
                   objIndicador.intId,
                   Convert.ToDecimal(tbxRangoMinUp.Text.Trim()),
                   Convert.ToDecimal(tbxRangoMaxUp.Text.Trim()),
                   1,
                   tbxCumpUp.Text.Trim(),
                   0, string.Empty);
                lstDetalleCump.Add(objDetalleCump);
                #endregion

                #region Semaforo Amarillo
                objDetalleCump = new clsDetalleCalificacion(
                    Convert.ToInt32(tbxIdCumpOdd.Text.Trim()),
                    objIndicador.intId,
                    Convert.ToDecimal(tbxRangoMinOdd.Text.Trim()),
                    Convert.ToDecimal(tbxRangoMaxOdd.Text.Trim()),
                    2,
                    tbxCumpOdd.Text.Trim(),
                    0, string.Empty);
                lstDetalleCump.Add(objDetalleCump);
                #endregion

                #region Semaforo Rojo
                objDetalleCump = new clsDetalleCalificacion(
                                    Convert.ToInt32(tbxIdCumpDown.Text.Trim()),
                                    objIndicador.intId,
                                    Convert.ToDecimal(tbxRangoMinDown.Text.Trim()),
                                    Convert.ToDecimal(tbxRangoMaxDown.Text.Trim()),
                                    3,
                                    tbxCumpDown.Text.Trim(),
                                    0, string.Empty);
                lstDetalleCump.Add(objDetalleCump);
                #endregion

                booResult = cCumplimiento.mtdActualizarCumplimiento(lstDetalleCump, ref strErrMsg);
            }
            #endregion

            #region Formula
            if (booResult)
            {
                clsFormulaBLL cFormula = new clsFormulaBLL();
                booResult = cFormula.mtdActualizarFormula(Global.LstCalidad, Convert.ToInt32(tbxId.Text.Trim()),
                    ref strErrMsg);
            }
            #endregion
            return booResult;
        }

        private bool mtdValidarProceso(int intId, ref clsProcesoIndicador objProcInd, ref string strErrMsg)
        {
            bool booResult = false;

            if ((ddlSubproceso.SelectedValue != "0") && (!string.IsNullOrEmpty(ddlSubproceso.SelectedValue)))
            {
                objProcInd = new clsProcesoIndicador(intId, 3, Convert.ToInt32(ddlSubproceso.SelectedValue),
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                booResult = true;
            }
            else
            {
                if ((ddlProceso.SelectedValue != "0") && (!string.IsNullOrEmpty(ddlProceso.SelectedValue)))
                {
                    objProcInd = new clsProcesoIndicador(intId, 2, Convert.ToInt32(ddlProceso.SelectedValue),
                        Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                    booResult = true;
                }
                else
                    if (ddlMacroproceso.SelectedValue != "0")
                    {
                        objProcInd = new clsProcesoIndicador(intId, 1, Convert.ToInt32(ddlMacroproceso.SelectedValue),
                            Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                        booResult = true;
                    }
            }

            if (!booResult)
                strErrMsg = "Error al asociar el proceso al Indicador";

            return booResult;
        }

        private bool mtdLlenarCamposCumplimiento(clsIndicador objIndicadorIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsDetalleCalificacion> lstDetCalif = new List<clsDetalleCalificacion>();
            clsCumplimientoBLL cCumplimiento = new clsCumplimientoBLL();
            #endregion

            booResult = cCumplimiento.mtdConsultarCumplimiento(objIndicadorIn, ref lstDetCalif, ref strErrMsg);

            if (booResult)
            {

                foreach (clsDetalleCalificacion objDetCal in lstDetCalif)
                {
                    switch (objDetCal.intIdSemaforo)
                    {
                        case 1:
                            tbxRangoMinUp.Text = objDetCal.intValorMin.ToString();
                            tbxRangoMaxUp.Text = objDetCal.intValorMax.ToString();
                            tbxCumpUp.Text = objDetCal.strNombreCumplimiento.ToString();
                            tbxIdCumpUp.Text = objDetCal.intId.ToString();
                            break;
                        case 2:
                            tbxRangoMinOdd.Text = objDetCal.intValorMin.ToString();
                            tbxRangoMaxOdd.Text = objDetCal.intValorMax.ToString();
                            tbxCumpOdd.Text = objDetCal.strNombreCumplimiento.ToString();
                            tbxIdCumpOdd.Text = objDetCal.intId.ToString();
                            break;
                        case 3:
                            tbxRangoMinDown.Text = objDetCal.intValorMin.ToString();
                            tbxRangoMaxDown.Text = objDetCal.intValorMax.ToString();
                            tbxCumpDown.Text = objDetCal.strNombreCumplimiento.ToString();
                            tbxIdCumpDown.Text = objDetCal.intId.ToString();
                            break;
                    }
                }
            }

            return booResult;
        }

        private bool mtdLlenarCamposFormula(clsIndicador objIndicadorIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strFormula = string.Empty;
            clsFormulaBLL cForm = new clsFormulaBLL();
            List<object> lstFormula = new List<object>();
            #endregion

            Global.mtdLimpiarListaCalidad();

            booResult = cForm.mtdConsultarFormula(objIndicadorIn, ref lstFormula, ref strErrMsg);

            if (booResult)
            {
                Global.LstCalidad = lstFormula;

                foreach (object objT in lstFormula)
                {
                    clsFormula objFormula = new clsFormula();

                    objFormula = (clsFormula)objT;

                    switch (objFormula.intOperando)
                    {
                        case 1: //Variable
                            clsVariable objVarIn = new clsVariable(Convert.ToInt32(objFormula.strValor), string.Empty, string.Empty, true, 0, string.Empty),
                                objVarOut = new clsVariable();
                            clsVariableBLL cVar = new clsVariableBLL();
                            booResult = cVar.mtdConsultarVariable(objVarIn, ref objVarOut, ref strErrMsg);

                            if (booResult)
                                mtdLlenarCampoFormula(objVarOut.strDescripcion);

                            break;
                        case 2://Operacion
                        case 3:// Otro valor
                            mtdLlenarCampoFormula(objFormula.strValor);

                            break;
                    }
                }
            }
            return booResult;
        }
        #endregion

        #region Variable
        private bool mtdInsertarVariable(ref string strErrMsg)
        {
            bool booResult = false;
            clsVariable objVariable = new clsVariable(0, tbxDescripcionVar.Text.Trim(),
                ddlFormato.SelectedItem.ToString(), chbEstadoVar.Checked, Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);

            clsVariableBLL cVariable = new clsVariableBLL();
            booResult = cVariable.mtdInsertarVariable(objVariable, ref strErrMsg);

            return booResult;
        }

        private void mtdModificarVar()
        {
            string strErrMsg = string.Empty;

            TblDetallesVar.Visible = true;
            chbEstadoVar.Enabled = false;

            btnGuardarVar.Visible = false;
            btnActualizarVar.Visible = true;
            btnCancelarVar.Visible = true;

            tbxCodigoVar.Text = InfoGrid3.Rows[RowGrid3][0].ToString().Trim();
            tbxDescripcionVar.Text = InfoGrid3.Rows[RowGrid3][1].ToString().Trim();
            ddlFormato.SelectedValue = InfoGrid3.Rows[RowGrid3][2].ToString().Trim();

            #region CheckBox
            chbEstadoVar.Checked = InfoGrid3.Rows[RowGrid3][3].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox
        }

        private bool mtdActualizarVariable(ref string strErrMsg)
        {
            bool booResult = false;
            clsVariable objVariable = new clsVariable(Convert.ToInt32(tbxCodigoVar.Text.Trim()), tbxDescripcionVar.Text.Trim(),
                ddlFormato.SelectedItem.ToString(), chbEstadoVar.Checked, 0, string.Empty);

            clsVariableBLL cVariable = new clsVariableBLL();
            booResult = cVariable.mtdActualizarVariable(objVariable, ref strErrMsg);

            return booResult;
        }

        private void mtdActivarVar()
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                string strEstado = string.Empty;
                bool booEstado = InfoGrid3.Rows[RowGrid3][3].ToString().Trim() == "True" ? true : false;
                Session["EstadoPerfil"] = "V";

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} la variable?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Realiza la actualizacion de estado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no </returns>
        private bool mtdActualizarEstadoVar(ref string strErrMsg)
        {
            bool booResult = false;

            clsVariable objVariable = new clsVariable(
                Convert.ToInt32(InfoGrid3.Rows[RowGrid3][0].ToString().Trim()),
                string.Empty, string.Empty,
                !(InfoGrid3.Rows[RowGrid3][3].ToString().Trim() == "True" ? true : false),
                0, string.Empty);

            clsVariableBLL cVariable = new clsVariableBLL();

            booResult = cVariable.mtdActualizarEstado(objVariable, ref strErrMsg);

            return booResult;
        }
        #endregion

        #region Formula
        private void mtdSeleccionaVar()
        {
            int cantLstCalidad = Global.LstCalidad.Count;
            int iteracion = 0;
            clsFormula objFormula = new clsFormula();
            
            foreach (object objT in Global.LstCalidad)
            {
                objFormula = (clsFormula)objT;
                iteracion++;
                if (iteracion == cantLstCalidad) {
                    int varaiable = objFormula.intOperando;
                    if (varaiable == 1 || varaiable == 3)
                    {
                        omb.ShowMessage("Debe seleccionar primero un operador", 2, "Atención");
                    }
                    else
                    {
                        objFormula = new clsFormula(0, 1, 0, InfoGrid4.Rows[RowGrid4][0].ToString().Trim(), Global.intPosicion, 1, string.Empty);
                        mtdIngresarListaFormula(objFormula);

                        mtdLlenarCampoFormula(InfoGrid4.Rows[RowGrid4][1].ToString().Trim());
                        break;
                    }
                }
            }
            if (Global.LstCalidad.Count == 0)
            {
                objFormula = new clsFormula(0, 1, 0, InfoGrid4.Rows[RowGrid4][0].ToString().Trim(), Global.intPosicion, 1, string.Empty);
                mtdIngresarListaFormula(objFormula);

                mtdLlenarCampoFormula(InfoGrid4.Rows[RowGrid4][1].ToString().Trim());

            }
        }

        private void mtdLlenarCampoFormula(string strValor)
        {
            if (string.IsNullOrEmpty(tbxCampoFormula.Text.Trim()))
                tbxCampoFormula.Text = strValor;
            else
                tbxCampoFormula.Text = tbxCampoFormula.Text + " " + strValor;
        }

        private void mtdIngresarListaFormula(clsFormula objFormula)
        {
            Global.mtdIncrementaPosicion();
            Global.LstCalidad.Add(objFormula);
        }
        #endregion

        /*protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e);
            string strEstado = string.Empty;
            bool booEstado = InfoGrid.Rows[RowGrid][5].ToString().Trim() == "True" ? true : false;
            Session["EstadoPerfil"] = "I";

            if (booEstado)
                strEstado = "inactivar";
            else
                strEstado = "activar";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string estado = e.Row.Cells[5].Text;
                var imagen = e.Row.FindControl("")
            }
        }*/


        #region
        #endregion
        #endregion
    }
}