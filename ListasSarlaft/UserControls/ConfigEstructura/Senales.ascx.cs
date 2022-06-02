using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ListasSarlaft.UserControls.ConfigEstructura
{
    public partial class Senales : System.Web.UI.UserControl
    {
        string IdFormulario = "10004";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();
        private static int LastInsertIdCE;

        #region Properties
        private int indexRow;
        private int indexRowVar;
        private int indexRowOp;
        private int pagIndex;
        private int pagVarIndex;
        private int pagOpIndex;
        private DataTable infoGrid;
        private DataTable infoVarGrid;
        private DataTable infoOpGrid;
        private int rowGrid;

        private int IndexRow
        {
            get
            {
                indexRow = (int)ViewState["indexRow"];
                return indexRow;
            }
            set
            {
                indexRow = value;
                ViewState["indexRow"] = indexRow;
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

        private int IndexRowVar
        {
            get
            {
                indexRowVar = (int)ViewState["indexRowVar"];
                return indexRowVar;
            }
            set
            {
                indexRowVar = value;
                ViewState["indexRowVar"] = indexRowVar;
            }
        }

        private int IndexRowOp
        {
            get
            {
                indexRowOp = (int)ViewState["indexRowOp"];
                return indexRowOp;
            }
            set
            {
                indexRowOp = value;
                ViewState["indexRowOp"] = indexRowOp;
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

        private int PagVarIndex
        {
            get
            {
                pagVarIndex = (int)ViewState["pagVarIndex"];
                return pagVarIndex;
            }
            set
            {
                pagVarIndex = value;
                ViewState["pagVarIndex"] = pagVarIndex;
            }
        }

        private int PagOpIndex
        {
            get
            {
                pagOpIndex = (int)ViewState["pagOpIndex"];
                return pagOpIndex;
            }
            set
            {
                pagOpIndex = value;
                ViewState["pagOpIndex"] = pagOpIndex;
            }
        }

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

        private DataTable InfoVarGrid
        {
            get
            {
                infoVarGrid = (DataTable)ViewState["infoVarGrid"];
                return infoVarGrid;
            }
            set
            {
                infoVarGrid = value;
                ViewState["infoVarGrid"] = infoVarGrid;
            }
        }

        private DataTable InfoOpGrid
        {
            get
            {
                infoOpGrid = (DataTable)ViewState["infoOpGrid"];
                return infoOpGrid;
            }
            set
            {
                infoOpGrid = value;
                ViewState["infoOpGrid"] = infoOpGrid;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                Global.BooIsOkFormula = false;
                mtdInicializarValores();
                mtdLoadGridViewSenales();
                mtdLoadGridViewVars();
                mtdLoadGridViewOperadores();
                mtdLoadDDLOpGlobal();
                // controlan cuando se comparan dos variables
                Session["ComparaVariable"] = false;
                Session["SeleccionaVariable"] = false;
            }
        }

        #region Gridviews
        #region Senales
        protected void gvSenales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvSenales.PageIndex = PagIndex;
            gvSenales.DataSource = InfoGrid;
            gvSenales.DataBind();
        }

        protected void gvSenales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRow = (Convert.ToInt16(gvSenales.PageSize) * Convert.ToInt16(gvSenales.PageIndex)) + Convert.ToInt16(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ModificarSenal":
                    Global.BooIsNew = false;
                    TabContainerSenal.ActiveTabIndex = 0;
                    mtdModificarSenal();
                    break;
                case "EliminarSenal":
                    //lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                    //mpeMsgBoxOkNo.Show();
                    mtdEliminarSenal();
                    //mtdMensaje("Señal eliminada correctamente");
                    break;
            }
        }
        #endregion Senales

        #region Variables
        protected void gvVariable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagVarIndex = e.NewPageIndex;
            gvVariable.PageIndex = PagVarIndex;
            gvVariable.DataSource = InfoVarGrid;
            gvVariable.DataBind();
        }

        protected void gvVariable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRowVar = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecVariable":
                    mtdSeleccionVariable();
                    break;
            }
        }
        #endregion

        #region Operador
        protected void gvOperador_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagOpIndex = e.NewPageIndex;
            gvOperador.PageIndex = PagOpIndex;
            gvOperador.DataSource = InfoOpGrid;
            gvOperador.DataBind();
        }

        protected void gvOperador_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRowOp = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecOperador":
                    if (InfoOpGrid.Rows[IndexRowOp]["StrNombreOperador"].ToString().Trim().ToUpper() == "ENTRE")
                        mtdHabilitarControlRango(true);
                    else
                        mtdHabilitarControlRango(false);
                    mtdSeleccionOperador();
                    break;
            }
        }
        #endregion
        #endregion

        #region Buttons
        #region Senal
        protected void btnAgregarSenal_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                Global.BooIsNew = true;
                TabContainerSenal.Visible = true;
                TabContainerSenal.ActiveTabIndex = 0;
                lblTituloGestion.Text = "Creación de Señal de Alerta";
                Session["UltSeñalAlerta"] = string.Empty;
                mtdResetCamposInsertar();
            }
        }

        protected void ibtnGuardarSenal_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdAgregarSenal(Session["IdUsuario"].ToString().Trim(), Session["UltSeñalAlerta"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbxCodigoSenal.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxDescripcionSenal.Text.Trim()), chbAutomatico.Checked);

                    mtdLoadGridViewSenales();
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar la señal de alerta. [" + ex.Message + "].");
            }
        }

        protected void ibtnGuardarUpdSenal_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdActualizarSenal(Session["IdUsuario"].ToString().Trim(), Session["UltSeñalAlerta"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbxCodigoSenal.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxDescripcionSenal.Text.Trim()), chbAutomatico.Checked);

                    mtdLoadGridViewSenales();
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al modificar la señal de alerta. " + ex.Message);
            }
        }

        protected void ibtnCancelSenal_Click(object sender, EventArgs e)
        {
            Session["UltSeñalAlerta"] = string.Empty;
            TabContainerSenal.ActiveTabIndex = 0;
            mtdResetValues();
        }
        #endregion

        protected void ibtnGuardarFormula_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosActualizar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False" ||
                cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
            {
                mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                return;
            }

            if (Global.BooIsOkFormula)
            {
                mtdGuardarFormula(Global.LstSenalVar, Global.BooIsNew, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    Global.BooIsOkFormula = false;
                    mtdResetValues();

                    Session["UltSeñalAlerta"] = string.Empty;

                    if (Global.BooIsNew)
                        mtdMensaje("La fórmula fue creada exitósamente.");
                    else
                        mtdMensaje("La fórmula fue modificada exitósamente.");
                }
                else
                    mtdMensaje(strErrMsg);
            }
            else
                mtdMensaje("La fórmula debe terminar en una variable u otro valor diferente a un operador.");
        }

        protected void ibtnCancelFormula_Click(object sender, EventArgs e)
        {
            Global.BooIsOkFormula = false;
            Global.LstSenalVar = new List<object>();

            TabContainerSenal.ActiveTabIndex = 0;
            mtdResetValues();

            Session["UltSeñalAlerta"] = string.Empty;
        }

        protected void ibtnLimpiarFormula_Click(object sender, EventArgs e)
        {
            mtdResetCamposFormula();

            Global.mtdLimpiarLista();

            mtdHabilitarControlesFormula(4);
        }

        protected void ibtnSelecOtroValor_Click(object sender, EventArgs e)
        {

            int intUltimaSeñal = 0;

            if (!string.IsNullOrEmpty(tbxOtroValor.Text.Trim()) || ((bool)Session["ComparaVariable"] && (bool)Session["SeleccionaVariable"]))
            {
                intUltimaSeñal = Convert.ToInt32(Session["UltSeñalAlerta"].ToString());

                if (intUltimaSeñal > 0)
                {
                    clsDTOSenalVariable objSenalVar = new clsDTOSenalVariable(intUltimaSeñal.ToString().Trim(),
                        string.Empty, "3", tbxOtroValor.Text.Trim(), string.Empty, Global.BooIsGlobal);

                    Global.LstSenalVar.Add(objSenalVar);

                    tbxFormula.Text = tbxFormula.Text + " " + tbxOtroValor.Text;

                    mtdHabilitarControlesFormula(4);

                    Global.BooIsOkFormula = true;
                    mtdHabilitarControlRango(false);
                    tbxOtroValor.Text = string.Empty;
                }
                Session["ComparaVariable"] = false;
                Session["SeleccionaVariable"] = false;
            }
            else
                mtdMensaje("Este campo debe tener información.");
        }

        protected void ibtnRango_Click(object sender, EventArgs e)
        {
            bool booIsErr = false;
            int intUltimaSeñal = 0;

            #region Validacion Campos Rango Vacio
            if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(tbxDesde.Text.Trim())))
            {
                mtdMensaje("Error. El campo [Desde] no debe estar vacio.");
                booIsErr = true;
            }

            if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(tbxHasta.Text.Trim())))
            {
                mtdMensaje("Error. El campo [Hasta] no debe estar vacio.");
                booIsErr = true;
            }
            #endregion

            #region Validacion Campos Rango numericos
            if (!booIsErr)
            {
                if (!clsLogica.clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbxDesde.Text.Trim())))
                {
                    mtdMensaje("Error. El campo [Desde] debe ser un número.");
                    booIsErr = true;
                }

                if (!clsLogica.clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbxHasta.Text.Trim())))
                {
                    mtdMensaje("Error. El campo [Hasta] debe ser un número.");
                    booIsErr = true;
                }
            }
            #endregion

            if (!booIsErr)
            {
                intUltimaSeñal = Convert.ToInt32(Session["UltSeñalAlerta"].ToString());

                if (intUltimaSeñal > 0)
                {
                    clsDTOSenalVariable objSenalVar = new clsDTOSenalVariable(intUltimaSeñal.ToString().Trim(),
                       string.Empty, "4", Sanitizer.GetSafeHtmlFragment(tbxDesde.Text.Trim()), string.Empty, Global.BooIsGlobal);

                    Global.LstSenalVar.Add(objSenalVar);

                    objSenalVar = new clsDTOSenalVariable(intUltimaSeñal.ToString().Trim(),
                       string.Empty, "4", Sanitizer.GetSafeHtmlFragment(tbxHasta.Text.Trim()), string.Empty, Global.BooIsGlobal);

                    Global.LstSenalVar.Add(objSenalVar);

                    tbxFormula.Text = Sanitizer.GetSafeHtmlFragment(tbxFormula.Text) + " " + Sanitizer.GetSafeHtmlFragment(tbxDesde.Text) + " y " + Sanitizer.GetSafeHtmlFragment(tbxHasta.Text);
                    Global.BooIsOkFormula = true;
                    mtdHabilitarControlesFormula(4);

                    mtdHabilitarControlRango(false);
                }
            }
        }

        protected void ibtnSelecOpGlobal_Click(object sender, EventArgs e)
        {
            mtdSeleccionOPGlobal();
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            mtdEliminarSenal();
        }
        #endregion

        #region Loads
        #region Senales
        private void mtdLoadGridViewSenales()
        {
            mtdLoadGridSenales();
            mtdLoadInfoGridSenales();
        }

        private void mtdLoadGridSenales()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdUsuario", typeof(string));
            grid.Columns.Add("StrIdSenal", typeof(string));
            grid.Columns.Add("StrCodigoSenal", typeof(string));
            grid.Columns.Add("StrDescripcionSenal", typeof(string));
            grid.Columns.Add("BooEsAutomatico", typeof(string));

            gvSenales.DataSource = grid;
            gvSenales.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridSenales()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOSenal> lstSenal = new List<clsDTOSenal>();

            lstSenal = cSenal.mtdCargarInfoSenal(ref strErrMsg);

            if (lstSenal != null)
            {
                mtdLoadInfoGridSenales(lstSenal);
                gvSenales.DataSource = lstSenal;
                gvSenales.DataBind();
            }
        }

        private void mtdLoadInfoGridSenales(List<clsDTOSenal> lstSenal)
        {
            foreach (clsDTOSenal objSenal in lstSenal)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objSenal.StrIdUsuario.ToString().Trim(),
                    objSenal.StrIdSenal.ToString().Trim(),
                    objSenal.StrCodigoSenal.ToString().Trim(),
                    objSenal.StrDescripcionSenal.ToString().Trim(),
                    objSenal.BooEsAutomatico
                    });
            }
        }
        #endregion

        #region Variables
        private void mtdLoadGridViewVars()
        {
            //mtdLoadGridVars();
            //mtdLoadInfoGridVars();

            mtdLoadGridCampos();
            mtdLoadInfoGridCampos();
        }

        #region Viejo Info Sobre Tabla Variables de BD
        private void mtdLoadGridVars()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdVariable", typeof(string));
            grid.Columns.Add("StrNombreVariable", typeof(string));

            gvVariable.DataSource = grid;
            gvVariable.DataBind();
            InfoVarGrid = grid;
        }

        private void mtdLoadInfoGridVars()
        {
            string strErrMsg = string.Empty;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOVariable> lstTipoParam = new List<clsDTOVariable>();
            clsDTOVariable objVariableIn = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);

            lstTipoParam = cParamArchivo.mtdCargarInfoVariables(objVariableIn, ref strErrMsg);

            if (lstTipoParam != null)
            {
                mtdLoadInfoGridVars(lstTipoParam);
                gvVariable.DataSource = lstTipoParam;
                gvVariable.DataBind();
            }
        }

        private void mtdLoadInfoGridVars(List<clsDTOVariable> lstVariable)
        {
            foreach (clsDTOVariable objVariable in lstVariable)
            {
                InfoVarGrid.Rows.Add(new Object[] {
                    objVariable.StrIdVariable.ToString().Trim(),
                    objVariable.StrNombreVariable.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Info sobre Tabla Estructura de Campos de BD
        private void mtdLoadGridCampos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdEstructCampo", typeof(string));
            grid.Columns.Add("StrNombreCampo", typeof(string));

            gvVariable.DataSource = grid;
            gvVariable.DataBind();
            InfoVarGrid = grid;
        }

        private void mtdLoadInfoGridCampos()
        {
            string strErrMsg = string.Empty;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOEstructuraCampo> lstCampos = new List<clsDTOEstructuraCampo>();

            lstCampos = cParamArchivo.mtdCargarInfoEstructura(ref strErrMsg);

            if (lstCampos != null)
            {
                mtdLoadInfoGridCampos(lstCampos);
                gvVariable.DataSource = lstCampos;
                gvVariable.DataBind();
            }
        }

        private void mtdLoadInfoGridCampos(List<clsDTOEstructuraCampo> lstCampos)
        {
            foreach (clsDTOEstructuraCampo objCampo in lstCampos)
            {
                InfoVarGrid.Rows.Add(new Object[] {
                    objCampo.StrIdEstructCampo.ToString().Trim(),
                    objCampo.StrNombreCampo.ToString().Trim()
                    });
            }
        }

        #endregion
        #endregion

        #region Operadores
        private void mtdLoadGridViewOperadores()
        {
            mtdLoadGridOps();
            mtdLoadInfoGridOps();
        }

        private void mtdLoadGridOps()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdOperador", typeof(string));
            grid.Columns.Add("StrNombreOperador", typeof(string));
            grid.Columns.Add("StrIdentificadorOperador", typeof(string));

            gvOperador.DataSource = grid;
            gvOperador.DataBind();
            InfoOpGrid = grid;
        }

        private void mtdLoadInfoGridOps()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOOperador> lstOps = new List<clsDTOOperador>();

            lstOps = cSenal.mtdCargarInfoOps(ref strErrMsg);

            if (lstOps != null)
            {
                mtdLoadInfoGridOps(lstOps);
                gvOperador.DataSource = lstOps;
                gvOperador.DataBind();
            }
        }

        private void mtdLoadInfoGridOps(List<clsDTOOperador> lstOps)
        {
            foreach (clsDTOOperador objOp in lstOps)
            {
                InfoOpGrid.Rows.Add(new Object[] {
                    objOp.StrIdOperador.ToString().Trim(),
                    objOp.StrNombreOperador.ToString().Trim(),
                    objOp.StrIdentificadorOperador.ToString().Trim()
                    });
            }
        }

        private void mtdLoadDDLOpGlobal()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOOperadorGlobal> lstOpGlobal = new List<clsDTOOperadorGlobal>();
            #endregion Vars

            lstOpGlobal = cSenal.mtdCargarInfoOpGlobal(ref strErrMsg);

            if (lstOpGlobal != null)
            {
                int intCounter = 1;
                ddlOpGlobal.Items.Clear();
                ddlOpGlobal.Items.Insert(0, new ListItem("Ninguno", "0"));

                foreach (clsDTOOperadorGlobal objOpGlobal in lstOpGlobal)
                {
                    ddlOpGlobal.Items.Insert(intCounter, new ListItem(objOpGlobal.StrNombreOperador, objOpGlobal.StrIdOperador));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }
        #endregion

        #endregion

        #region Methods
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdHabilitarControlRango(bool booActivar)
        {
            lblDesde.Visible = booActivar;
            lblHasta.Visible = booActivar;
            tbxHasta.Visible = booActivar;
            tbxDesde.Visible = booActivar;
            ibtnRango.Visible = booActivar;

            tbxOtroValor.Visible = !booActivar;
            ibtnSelecOtroValor.Visible = !booActivar;
        }

        private void mtdHabilitarControlesFormula(int intFase)
        {
            switch (intFase)
            {
                case 1:
                    gvVariable.Enabled = true;
                    gvOperador.Enabled = false;
                    ibtnSelecOtroValor.Enabled = false;
                    ibtnSelecOpGlobal.Enabled = false;
                    break;
                case 2:
                    Global.BooIsOkFormula = false;
                    gvVariable.Enabled = false;
                    gvOperador.Enabled = true;
                    ibtnSelecOtroValor.Enabled = false;
                    break;
                case 3:
                    Global.BooIsOkFormula = false;
                    gvVariable.Enabled = true;
                    gvOperador.Enabled = false;
                    ibtnSelecOtroValor.Enabled = true;
                    tbxOtroValor.Enabled = true;
                    break;
                case 4:
                    ibtnSelecOpGlobal.Enabled = true;
                    gvVariable.Enabled = false;
                    gvOperador.Enabled = false;
                    ibtnSelecOtroValor.Enabled = false;
                    break;
                case 5:
                    Global.BooIsOkFormula = true;
                    ibtnSelecOpGlobal.Enabled = false;
                    gvVariable.Enabled = false;
                    gvOperador.Enabled = false;
                    ibtnSelecOtroValor.Enabled = true;
                    tbxOtroValor.Enabled = false;
                    tbxOtroValor.Text = string.Empty;
                    break;
            }
        }

        #region Resets
        private void mtdResetCamposInsertar()
        {
            TblGestionSenal.Visible = true;
            ibtnGuardarSenal.Visible = true;
            ibtnGuardarUpdSenal.Visible = false;
            mtdResetCamposSenal();

            mtdResetCamposFormula();
        }

        private void mtdResetCamposActualizar()
        {
            TblGestionSenal.Visible = true;
            ibtnGuardarSenal.Visible = false;
            ibtnGuardarUpdSenal.Visible = true;
            mtdResetCamposSenal();
        }

        private void mtdResetCamposSenal()
        {
            tbxCodigoSenal.Text = string.Empty;
            tbxDescripcionSenal.Text = string.Empty;
            chbAutomatico.Checked = false;
        }

        private void mtdResetCamposFormula()
        {
            tbxFormula.Text = string.Empty;
            tbxOtroValor.Text = string.Empty;
            tbxOtroValor.Enabled = false;
            tbxHasta.Text = string.Empty;
            tbxDesde.Text = string.Empty;
        }

        private void mtdResetValues()
        {
            TblGestionSenal.Visible = false;
            TabContainerSenal.Visible = false;
            mtdResetCamposSenal();
            mtdResetCamposFormula();
            mtdHabilitarControlRango(false);
        }
        #endregion

        #region Senal
        private void mtdAgregarSenal(string strIdUsuario, string strIdSenal, string strCodigoSenal, string strDescripcion, bool booEsAutomatico)
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            clsDTOSenal objSenal = new clsDTOSenal(strIdUsuario, strIdSenal, strCodigoSenal, strDescripcion, booEsAutomatico);
            int intUltimaSeñal = 0;

            intUltimaSeñal = cSenal.mtdAgregarSenal(objSenal, ref strErrMsg);

            Session["UltSeñalAlerta"] = intUltimaSeñal.ToString();
            LidSeñal.Text = intUltimaSeñal.ToString();
            if (string.IsNullOrEmpty(strErrMsg))
            {
                TblSeleccionarVariables.Visible = true;

                #region Habilitar Controles
                //mtdHabilitarControlesFormula(1);
                mtdHabilitarControlesFormula(4);
                #endregion
                string StrCodigoSenalng = tbxCodigoSenal.Text.Trim();
                string StrDescripcionSenal = tbxDescripcionSenal.Text.Trim();
                bool BooEsAutomatico = false;
                if (chbAutomatico.Checked)
                    BooEsAutomatico = true;
                mtdGenerarNotificacion(StrCodigoSenalng, StrDescripcionSenal, BooEsAutomatico);
                mtdMensaje("La señal de alerta fue creada exitósamente.");
            }
            else
                mtdMensaje(strErrMsg);
        }

        private void mtdActualizarSenal(string strIdUsuario, string strIdSenal, string strCodigoSenal, string strDescripcion, bool booEsAutomatico)
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            clsDTOSenal objSenal = new clsDTOSenal(strIdUsuario, strIdSenal, strCodigoSenal, strDescripcion, booEsAutomatico);

            cSenal.mtdActualizarSenal(objSenal, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
            {
                string StrCodigoSenalng = tbxCodigoSenal.Text.Trim();
                string StrDescripcionSenal = tbxDescripcionSenal.Text.Trim();
                bool BooEsAutomatico = false;
                if (chbAutomatico.Checked)
                    BooEsAutomatico = true;
                mtdGenerarNotificacion(StrCodigoSenalng, StrDescripcionSenal, BooEsAutomatico);
                mtdMensaje("La señal de alerta fue actualizada exitósamente.");
            }
            else
                mtdMensaje(strErrMsg);
        }

        private int mtdObtenerIdSeñal(List<object> LstFormula)
        {
            int intResult = 0;
            clsDTOSenalVariable objFormTemp = new clsDTOSenalVariable();

            object objFormula = LstFormula[0];
            objFormTemp = (clsDTOSenalVariable)objFormula;

            intResult = Convert.ToInt32(objFormTemp.mtdGetIdSenal());

            return intResult;
        }

        private void mtdModificarSenal()
        {
            #region Vars
            string strFormula = string.Empty, strErrMsg = string.Empty;
            bool booIsOk = false;
            List<object> LstFormula = new List<object>();
            clsSenal cSenal = new clsSenal();
            #endregion

            TabContainerSenal.Visible = true;
            lblTituloGestion.Text = "Modificación de Señal de Alerta";
            mtdResetCamposActualizar();

            TblSeleccionarVariables.Visible = true;

            #region Habilitar Controles
            //mtdHabilitarControlesFormula(1);
            mtdHabilitarControlesFormula(4);

            mtdHabilitarControlRango(false);
            #endregion

            clsDTOSenal objSenal = new clsDTOSenal(
                string.Empty,
                InfoGrid.Rows[IndexRow]["StrIdSenal"].ToString().Trim(),
                string.Empty,
                string.Empty,
                false);

            ///info
            strFormula = cSenal.mtdCargarFormulas(objSenal, ref LstFormula, ref booIsOk, ref strErrMsg);
            Global.LstSenalVar = LstFormula;
            Global.BooIsOkFormula = booIsOk;
            Session["UltSeñalAlerta"] = InfoGrid.Rows[IndexRow]["StrIdSenal"].ToString().Trim();
            tbxCodigoSenal.Text = InfoGrid.Rows[IndexRow]["StrCodigoSenal"].ToString().Trim();
            tbxDescripcionSenal.Text = InfoGrid.Rows[IndexRow]["StrDescripcionSenal"].ToString().Trim();
            chbAutomatico.Checked = InfoGrid.Rows[IndexRow][3].ToString().Trim() == "True" ? true : false;
            tbxFormula.Text = strFormula;

            if (!string.IsNullOrEmpty(strErrMsg))
                mtdMensaje(strErrMsg);
        }

        private void mtdEliminarSenal()
        {
            if (cCuenta.permisosBorrar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
            {
                mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                return;
            }

            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            clsDTOSenal objSenal = new clsDTOSenal(
                string.Empty,
                InfoGrid.Rows[indexRow]["StrIdSenal"].ToString().Trim(),
                InfoGrid.Rows[indexRow]["StrCodigoSenal"].ToString().Trim(),
                InfoGrid.Rows[indexRow]["StrDescripcionSenal"].ToString().Trim(),
                false);

            string StrCodigoSenalng = InfoGrid.Rows[indexRow]["StrCodigoSenal"].ToString().Trim();
            string StrDescripcionSenal = InfoGrid.Rows[indexRow]["StrDescripcionSenal"].ToString().Trim();
            bool BooEsAutomatico = false;
            if (chbAutomatico.Checked)
                BooEsAutomatico = true;
            mtdGenerarNotificacion(StrCodigoSenalng, StrDescripcionSenal, BooEsAutomatico);

            cSenal.mtdEliminarSenal(objSenal, ref strErrMsg);


            if (string.IsNullOrEmpty(strErrMsg))
            {
                mtdLoadGridViewSenales();

                mtdMensaje("La señal de alerta y fórmula fueron eliminadas exitósamente.");
            }
            else
                mtdMensaje(strErrMsg);
        }
        #endregion

        #region Formula
        private void mtdSeleccionOPGlobal()
        {
            int intUltimaSeñal = 0;

            if (ddlOpGlobal.SelectedIndex != 0)
            {
                intUltimaSeñal = Convert.ToInt32(Session["UltSeñalAlerta"].ToString());
                Global.BooIsGlobal = true;

                if (intUltimaSeñal > 0)
                {
                    if (Global.LstSenalVar.Count > 0)
                        Global.LstSenalVar = mtdCambiarAListaGlobal(Global.LstSenalVar);

                    clsDTOSenalVariable objSenalVar = new clsDTOSenalVariable(intUltimaSeñal.ToString().Trim(),
                        string.Empty, "5", ddlOpGlobal.SelectedIndex.ToString().Trim(), string.Empty, true);

                    Global.LstSenalVar.Add(objSenalVar);

                    #region Escribe en campo Formula
                    if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(tbxFormula.Text.Trim())))
                    {
                        if (ddlOpGlobal.SelectedIndex == 1)
                            tbxFormula.Text = "SUM ";
                        else
                            tbxFormula.Text = "CONTAR ";
                    }
                    else
                    {
                        if (ddlOpGlobal.SelectedIndex == 1)
                            tbxFormula.Text = Sanitizer.GetSafeHtmlFragment(tbxFormula.Text) + " y SUM ";
                        else
                            tbxFormula.Text = Sanitizer.GetSafeHtmlFragment(tbxFormula.Text) + " y CONTAR ";
                    }
                    #endregion
                }
            }

            mtdHabilitarControlesFormula(1);
        }

        private void mtdSeleccionVariable()
        {
            int intUltimaSeñal = Convert.ToInt32(Session["UltSeñalAlerta"].ToString());

            if (intUltimaSeñal > 0)
            {
                #region Viejo
                //clsDTOSenalVariable objSenalVar = new clsDTOSenalVariable(intUltimaSeñal.ToString().Trim(),
                //    string.Empty, "1", InfoVarGrid.Rows[IndexRowVar]["StrIdVariable"].ToString().Trim(), string.Empty, Global.BooIsGlobal);

                //Global.LstSenalVar.Add(objSenalVar);

                //if (string.IsNullOrEmpty(tbxFormula.Text.Trim()))
                //    tbxFormula.Text = InfoVarGrid.Rows[indexRowVar]["StrNombreVariable"].ToString().Trim();
                //else
                //{
                //    if (ddlOpGlobal.SelectedIndex != 0)
                //        tbxFormula.Text = tbxFormula.Text + " (" + InfoVarGrid.Rows[indexRowVar]["StrNombreVariable"].ToString().Trim() + " )";
                //    else
                //        tbxFormula.Text = tbxFormula.Text + " y " + InfoVarGrid.Rows[indexRowVar]["StrNombreVariable"].ToString().Trim();
                //}
                #endregion

                clsDTOSenalVariable objSenalVar = new clsDTOSenalVariable(intUltimaSeñal.ToString().Trim(),
                    string.Empty, "1", InfoVarGrid.Rows[IndexRowVar]["StrIdEstructCampo"].ToString().Trim(), string.Empty, Global.BooIsGlobal);

                Global.LstSenalVar.Add(objSenalVar);

                if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(tbxFormula.Text.Trim())))
                {
                    tbxFormula.Text = InfoVarGrid.Rows[indexRowVar]["StrNombreCampo"].ToString().Trim();
                    mtdHabilitarControlesFormula(2);
                }
                else
                {
                    if (ddlOpGlobal.SelectedIndex > 0)
                    {
                        tbxFormula.Text = Sanitizer.GetSafeHtmlFragment(tbxFormula.Text) + " (" + InfoVarGrid.Rows[indexRowVar]["StrNombreCampo"].ToString().Trim() + " )";
                        mtdHabilitarControlesFormula(2);
                    }
                    else if (ddlOpGlobal.SelectedIndex == 0 && !(bool)Session["ComparaVariable"])
                    {
                        mtdHabilitarControlesFormula(2);
                        tbxFormula.Text = Sanitizer.GetSafeHtmlFragment(tbxFormula.Text) + " y " + InfoVarGrid.Rows[indexRowVar]["StrNombreCampo"].ToString().Trim();
                    }
                    else if ((bool)Session["ComparaVariable"])
                    {
                        tbxFormula.Text = $"{Sanitizer.GetSafeHtmlFragment(tbxFormula.Text)} {InfoVarGrid.Rows[indexRowVar]["StrNombreCampo"].ToString().Trim()}";
                        mtdHabilitarControlesFormula(5);
                        Session["SeleccionaVariable"] = true;
                    }
                }
            }
        }

        private void mtdSeleccionOperador()
        {

            Session["ComparaVariable"] = true;

            int intUltimaSeñal = Convert.ToInt32(Session["UltSeñalAlerta"].ToString());

            if (intUltimaSeñal > 0)
            {
                clsDTOSenalVariable objSenalVar = new clsDTOSenalVariable(intUltimaSeñal.ToString().Trim(),
                    string.Empty, "2", InfoOpGrid.Rows[IndexRowOp]["StrIdOperador"].ToString().Trim(), string.Empty, Global.BooIsGlobal);

                Global.LstSenalVar.Add(objSenalVar);

                tbxFormula.Text = Sanitizer.GetSafeHtmlFragment(tbxFormula.Text) + " " + InfoOpGrid.Rows[IndexRowOp]["StrIdentificadorOperador"].ToString().Trim();

                mtdHabilitarControlesFormula(3);
            }
        }

        private void mtdGuardarFormula(List<object> LstFormula, bool booNuevo, ref string strErrMsg)
        {
            #region Vars
            int intSenal = 0;
            clsSenal cSenal = new clsSenal();
            clsDTOSenalVariable objFormula = null;
            #endregion

            if (!booNuevo)
            {
                intSenal = mtdObtenerIdSeñal(LstFormula);
                objFormula = new clsDTOSenalVariable(intSenal.ToString().Trim(),
                    string.Empty, string.Empty, string.Empty, string.Empty, false);

                cSenal.mtdEliminarFormula(objFormula, ref strErrMsg);
            }

            if (string.IsNullOrEmpty(strErrMsg))
            {
                cSenal.mtdGuardarFormula(LstFormula, ref strErrMsg);
                Session["ComparaVariable"] = false;
                Session["SeleccionaVariable"] = false;
            }
        }

        private List<object> mtdCambiarAListaGlobal(List<object> LstGlobal)
        {
            List<object> LstNuevaFormula = new List<object>();

            foreach (object objFormula in LstGlobal)
            {
                clsDTOSenalVariable objFormTemp = new clsDTOSenalVariable();
                objFormTemp = (clsDTOSenalVariable)objFormula;

                objFormTemp.BooEsGlobal = true;

                LstNuevaFormula.Add(objFormTemp);
            }

            return LstNuevaFormula;
        }


        #endregion

        #endregion













        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdGenerarNotificacion(String StrCodigoSenalng, String StrDescripcionSenal, bool BooEsAutomatico)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "<strong>NOTIFICACIÓN DE MODIFICACIÓN DE SEÑALES DE ALERTA" + "</strong><br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el cambio de información de señales de alerta.<br>";
                TextoAdicional = TextoAdicional + " Código de la señal : " + StrCodigoSenalng + "<br>";
                TextoAdicional = TextoAdicional + " Descripción : " + StrDescripcionSenal + "<br>";
                TextoAdicional = TextoAdicional + " Fecha de la modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario de Registro : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Registro : " + Session["nombreUsuario"].ToString() + "<br>";

                boolEnviarNotificacion(StrCodigoSenalng, Convert.ToInt16(Session["IdJerarquia"]), StrDescripcionSenal, TextoAdicional);
            }
            catch (Exception ex)
            {
                //strErrMsg = string.Format("Mensaje de error. [{0}]", ex.Message);
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }

        //String StrIdTipoParametro
        private Boolean boolEnviarNotificacion(String StrCodigoSenalng, int idNodoJerarquia,
            String StrDescripcionSenal, string textoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = string.Empty, Copia = string.Empty, Asunto = string.Empty, Otros = string.Empty, Cuerpo = string.Empty, NroDiasRecordatorio = string.Empty;
            string selectCommand = string.Empty, AJefeInmediato = string.Empty, AJefeMediato = string.Empty, RequiereFechaCierre = string.Empty;
            string idJefeInmediato = string.Empty, idJefeMediato = string.Empty;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Variables

            try
            {
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(StrCodigoSenalng.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = 104";

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Destinatario
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = " + idNodoJerarquia;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = row["CorreoResponsable"].ToString().Trim();
                        idJefeInmediato = row["idPadre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeInmediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeInmediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                            idJefeMediato = row["idPadre"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region correo del Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeMediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeMediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                //SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                //SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                //SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                //SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                //SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                //SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                //SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                //SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                //SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                //SqlDataSource200.Insert();
                #endregion
            }
            catch (Exception ex)
            {
                // Handle the Exception.
                Mensaje("Error en el envío de la notificación. " + ex.Message);
                err = true;
            }

            if (!err)
            {
                #region Restro
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (RequiereFechaCierre == "SI" && StrCodigoSenalng != "")
                {
                    ////Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    //SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    //SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    //SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    //SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = StrNombreParametro;
                    //SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    //SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    //SqlDataSource201.Insert();
                }
                #endregion

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");
                    message.From = fromAddress;//here you can set address

                    #region Destinatario
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region Copia
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region Otros
                    if (Otros.Trim() != "")
                        foreach (string substr in Otros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    message.Subject = Asunto;//subject of email
                    message.IsBodyHtml = true;//To determine email body is html or not
                    message.Body = Cuerpo;

                    smtpClient.Send(message);
                    #endregion
                }
                catch (Exception ex)
                {
                    Mensaje("Error en el envío de la notificación. " + ex.Message);
                    err = true;
                }

                if (!err)
                {
                    ////Actualiza el Estado del Correo Enviado
                    //SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    //SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    //SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    //SqlDataSource200.Update();
                }
            }

            return (err);
        }


    }
}