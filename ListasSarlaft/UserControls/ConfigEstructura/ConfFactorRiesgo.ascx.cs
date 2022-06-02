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

namespace ListasSarlaft.UserControls.ConfigEstructura
{
    public partial class ConfFactorRiesgo : System.Web.UI.UserControl
    {
        string IdFormulario = "10005";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();

        #region Properties
        private int indexRow;
        private int pagIndex;
        private DataTable infoGrid;

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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            if (!Page.IsPostBack)
            {
                mtdLoadGridViewFactor();
                mtdLoadCBLSenal();
            }
        }

        #region GridView
        protected void gvFactores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvFactores.PageIndex = PagIndex;
            gvFactores.DataSource = InfoGrid;
            gvFactores.DataBind();
        }

        protected void gvFactores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRow = /*(Convert.ToInt16(gvFactores.PageSize) * Convert.ToInt16(gvFactores.PageIndex)) + */Convert.ToInt16(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ModificarFactor":
                    TabContainerFactor.ActiveTabIndex = 0;
                    mtdModificarSenal();
                    break;
                case "EliminarFactor":
                    lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                    mpeMsgBoxOkNo.Show();
                    break;
            }
        }
        #endregion

        #region Loads
        #region Grid
        private void mtdLoadGridViewFactor()
        {
            mtdLoadGridFactor();
            mtdLoadInfoGridFactor();
        }

        private void mtdLoadGridFactor()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdFactorRiesgo", typeof(string));
            grid.Columns.Add("StrCodigoFactorRiesgo", typeof(string));
            grid.Columns.Add("StrDescFactorRiesgo", typeof(string));

            gvFactores.DataSource = grid;
            gvFactores.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridFactor()
        {
            string strErrMsg = string.Empty;
            clsFactorRiesgo cFactor = new clsFactorRiesgo();
            List<clsDTOFactorRiesgo> lstFactor = new List<clsDTOFactorRiesgo>();

            lstFactor = cFactor.mtdConsultarFactor(ref strErrMsg);

            if (lstFactor != null)
            {
                mtdLoadInfoGridFactor(lstFactor);
                gvFactores.DataSource = lstFactor;
                gvFactores.DataBind();
            }
        }

        private void mtdLoadInfoGridFactor(List<clsDTOFactorRiesgo> lstFactor)
        {
            foreach (clsDTOFactorRiesgo objFactor in lstFactor)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objFactor.StrIdFactorRiesgo.ToString().Trim(),
                    objFactor.StrCodigoFactorRiesgo.ToString().Trim(),
                    objFactor.StrDescFactorRiesgo.ToString().Trim()
                    });
            }
        }
        #endregion

        private void mtdLoadCBLSenal()
        {
            #region Var
            int intContador = 0;
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOSenal> lstSenal = new List<clsDTOSenal>();
            #endregion

            try
            {
                lstSenal = cSenal.mtdCargarInfoSenal(ref strErrMsg);
                chbSenalAsoc.Items.Clear();
                foreach (clsDTOSenal objSenal in lstSenal)
                {
                    chbSenalAsoc.Items.Insert(intContador, new ListItem(objSenal.StrCodigoSenal, objSenal.StrIdSenal));
                    intContador++;
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al cargar señales. " + ex.Message);
            }
        }

        #endregion

        #region Buttons
        #region Factor
        protected void btnAgregarFactor_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                TabContainerFactor.Visible = true;
                TabContainerFactor.ActiveTabIndex = 0;
                lblTituloGestion.Text = "Creación de Factor de Riesgo";
                Session["UltFactor"] = string.Empty;
                mtdResetCamposInsertar();
            }
        }

        protected void ibtnGuardarFactor_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdAgregarFactor(
                        Session["IdUsuario"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbxCodigoFactor.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(tbxDescFactor.Text.Trim()));

                    mtdLoadGridViewFactor();
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar el factor de riesgo. [" + ex.Message + "].");
            }
        }

        protected void ibtnGuardarUpdFactor_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdActualizarFactor(Session["UltFactor"].ToString().Trim(),
                        Session["IdUsuario"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbxCodigoFactor.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(tbxDescFactor.Text.Trim()));

                    mtdLoadGridViewFactor();
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al modificar el factor de riesgo. " + ex.Message);
            }
        }

        protected void ibtnCancelFactor_Click(object sender, EventArgs e)
        {
            Session["UltFactor"] = string.Empty;
            TabContainerFactor.ActiveTabIndex = 0;
            mtdHabilitarSenales(2);
            mtdResetValues();
        }
        #endregion

        protected void ibtnCancelRelacion_Click(object sender, EventArgs e)
        {
            TabContainerFactor.ActiveTabIndex = 0;
            mtdHabilitarSenales(2);
            mtdResetValues();
            Session["UltFactor"] = string.Empty;

        }

        protected void ibtnGuardarRelacion_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (mtdHaySenalEscogida(chbSenalAsoc))
            {
                mtdGuardarRelacion(Session["UltFactor"].ToString().Trim(), chbSenalAsoc, ref strErrMsg);
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    mtdResetValues();
                    Session["UltFactor"] = string.Empty;
                    mtdMensaje("La relación [Factor de Riesgo - Señales] fue modificada exitósamente.");
                }
            }
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            mtdEliminarFactor();
        }
        #endregion

        #region Methods
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Resets
        private void mtdResetCamposInsertar()
        {
            TblGestionFactor.Visible = true;
            ibtnGuardarFactor.Visible = true;
            ibtnGuardarUpdFactor.Visible = false;
            mtdResetCamposFactor();

            mtdResetCamposFormFactor();
        }

        private void mtdResetCamposFactor()
        {
            tbxCodigoFactor.Text = string.Empty;
            tbxDescFactor.Text = string.Empty;
        }

        private void mtdResetCamposFormFactor()
        {
            for (int i = 0; i < chbSenalAsoc.Items.Count; i++)
            {
                chbSenalAsoc.Items[i].Selected = false;
            }
        }

        private void mtdResetValues()
        {
            TblGestionFactor.Visible = false;
            TabContainerFactor.Visible = false;
            mtdResetCamposFactor();
            mtdResetCamposFormFactor();
        }

        private void mtdResetCamposActualizar()
        {
            TblGestionFactor.Visible = true;
            ibtnGuardarFactor.Visible = false;
            ibtnGuardarUpdFactor.Visible = true;
            mtdResetCamposFactor();
        }
        #endregion

        private void mtdAgregarFactor(string strIdUsuario, string strCodigoFactor, string strDescripcionFactor)
        {
            string strErrMsg = string.Empty;
            clsFactorRiesgo cFactor = new clsFactorRiesgo();
            clsDTOFactorRiesgo objFactor = new clsDTOFactorRiesgo(string.Empty, strCodigoFactor, strDescripcionFactor, strIdUsuario);
            int intFactor = 0;

            intFactor = cFactor.mtdAgregarFactor(objFactor, ref strErrMsg);

            Session["UltFactor"] = intFactor.ToString().Trim();

            if (string.IsNullOrEmpty(strErrMsg))
            {
                TblSenalAsoc.Visible = true;
                mtdHabilitarSenales(1);
                mtdMensaje("El Factor de Riesgo fue creado exitósamente.");
            }
            else
                mtdMensaje(strErrMsg);
        }

        private void mtdActualizarFactor(string strIdFactor, string strCodigoFactor, string strDescripcionFactor, string strIdUsuario)
        {
            string strErrMsg = string.Empty;
            clsFactorRiesgo cFactor = new clsFactorRiesgo();
            clsDTOFactorRiesgo objFactor = new clsDTOFactorRiesgo(strIdFactor, strCodigoFactor, strDescripcionFactor, strIdUsuario);

            cFactor.mtdActualizarFactor(objFactor, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                mtdMensaje("El Factor de Riesgo fue modificado exitósamente.");
            else
                mtdMensaje(strErrMsg);
        }

        private void mtdModificarSenal()
        {
            #region Vars
            string strFormula = string.Empty, strErrMsg = string.Empty;

            clsFactorRiesgo cFactor = new clsFactorRiesgo();
            clsDTOFactorRiesgo objFactor = null;
            List<clsDTOFactorSenal> lstRelacion = new List<clsDTOFactorSenal>();
            #endregion

            TabContainerFactor.Visible = true;
            lblTituloGestion.Text = "Modificación de Factor de Riesgo";
            mtdResetCamposActualizar();

            TblSenalAsoc.Visible = true;
            mtdHabilitarSenales(1);

            objFactor = new clsDTOFactorRiesgo(
                InfoGrid.Rows[IndexRow]["StrIdFactorRiesgo"].ToString().Trim(), string.Empty, string.Empty, string.Empty);

            lstRelacion = cFactor.mtdConsultarRelacion(objFactor, ref strErrMsg);

            #region Recorrido relaciones
            foreach (clsDTOFactorSenal objRelacion in lstRelacion)
            {
                for (int j = 0; j < chbSenalAsoc.Items.Count; j++)
                {
                    if (objRelacion.StrIdSenal == chbSenalAsoc.Items[j].Value.ToString().Trim())
                    {
                        chbSenalAsoc.Items[j].Selected = true;
                        break;
                    }
                }
            }
            #endregion

            Session["UltFactor"] = InfoGrid.Rows[IndexRow]["StrIdFactorRiesgo"].ToString().Trim();
            tbxCodigoFactor.Text = InfoGrid.Rows[IndexRow]["StrCodigoFactorRiesgo"].ToString().Trim();
            tbxDescFactor.Text = InfoGrid.Rows[IndexRow]["StrDescFactorRiesgo"].ToString().Trim();

            if (!string.IsNullOrEmpty(strErrMsg))
                mtdMensaje(strErrMsg);
        }

        private void mtdEliminarFactor()
        {
            string strErrMsg = string.Empty;
            clsFactorRiesgo cFactor = new clsFactorRiesgo();
            clsDTOFactorRiesgo objFactor = new clsDTOFactorRiesgo(
                InfoGrid.Rows[IndexRow]["StrIdFactorRiesgo"].ToString().Trim(), string.Empty, string.Empty, string.Empty);

            cFactor.mtdEliminarFactor(objFactor, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
            {
                mtdMensaje("El factor de riesgo y sus relaciones fueron eliminadas exitósamente.");
                mtdHabilitarSenales(2);
                mtdLoadGridViewFactor();
                mtdLoadCBLSenal();
            }
            else
                mtdMensaje(strErrMsg);
        }

        private void mtdGuardarRelacion(string strUltFactor,
            CheckBoxList chbSenales, ref string strErrMsg)
        {
            #region Vars
            clsFactorRiesgo cFactor = new clsFactorRiesgo();
            clsDTOFactorRiesgo objFactor = null;
            #endregion

            objFactor = new clsDTOFactorRiesgo(strUltFactor, string.Empty, string.Empty, string.Empty);
            cFactor.mtdEliminarRelacion(objFactor, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                for (int i = 0; i < chbSenales.Items.Count; i++)
                {
                    if (chbSenales.Items[i].Selected)
                    {
                        clsDTOFactorSenal objRelacion = new clsDTOFactorSenal(string.Empty, strUltFactor, chbSenales.Items[i].Value.ToString().Trim());
                        cFactor.mtdGuardarRelacion(objRelacion, ref strErrMsg);
                    }
                }
        }

        private bool mtdHaySenalEscogida(CheckBoxList chbSenales)
        {
            bool booExiste = false;

            for (int i = 0; i < chbSenales.Items.Count; i++)
            {
                if (chbSenales.Items[i].Selected)
                {
                    booExiste = true;
                    break;
                }
            }

            return booExiste;
        }

        private void mtdHabilitarSenales(int intFase)
        {
            switch (intFase)
            {
                case 1:
                    chbSenalAsoc.Enabled = true;
                    break;
                case 2:
                    chbSenalAsoc.Enabled = false;
                    break;

            }
        }

        #endregion
    }
}