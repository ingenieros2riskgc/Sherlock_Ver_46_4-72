using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using clsLogica;
using clsDTO;

namespace ListasSarlaft.UserControls.ConfigEstructura
{
    public partial class ConfPerfilVariable : System.Web.UI.UserControl
    {
        string IdFormulario = "10006";
        clsCuenta cCuenta = new clsCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(Convert.ToInt32(Session["IdUsuario"].ToString()), Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1",false);

            if (!Page.IsPostBack)
                mtdResetValues();
        }

        #region Loads
        #region Variables
        private void mtdLoadCBLVariable()
        {
            #region Variables
            int intContador = 0;
            string strErrMsg = string.Empty;
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOVariable> lstVariable = new List<clsDTOVariable>();
            #endregion

            try
            {
                lstVariable = cParamArchivo.mtdCargarInfoVariables(objVariable, ref strErrMsg);

                if (lstVariable != null)
                {
                    chbVarAsoc.Items.Clear();
                    foreach (clsDTOVariable objVar in lstVariable)
                    {
                        chbVarAsoc.Items.Insert(intContador, new ListItem(objVar.StrNombreVariable, objVar.StrIdVariable));
                        intContador++;
                    }
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al cargar variables. " + ex.Message);
            }
        }
        #endregion

        #region Perfil
        private void mtdLoadDDLPerfil()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsPerfil cPerfil = new clsPerfil();
            List<clsDTOPerfil> lstPerfiles = new List<clsDTOPerfil>();
            #endregion Vars

            lstPerfiles = cPerfil.mtdCargarInfoPerfiles(ref strErrMsg);

            if (lstPerfiles != null)
            {
                int intCounter = 1;
                ddlPerfiles.Items.Clear();
                ddlPerfiles.Items.Insert(0, new ListItem("", "0"));

                foreach (clsDTOPerfil objPerfil in lstPerfiles)
                {
                    ddlPerfiles.Items.Insert(intCounter, new ListItem(objPerfil.StrNombrePerfil, objPerfil.StrIdPerfil));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }
        #endregion
        #endregion

        #region DDL
        protected void ddlPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsParamArchivo cParam = new clsParamArchivo();
            clsDTOPerfil objPerfil = new clsDTOPerfil(string.Empty,ddlPerfiles.SelectedValue.ToString().Trim(),
                string.Empty, string.Empty, string.Empty);
            List<clsDTOPerfilVariable> lstPerfilVar = new List<clsDTOPerfilVariable>();
            #endregion

            lstPerfilVar = cParam.mtdConsultarPerfilVariable(objPerfil, ref strErrMsg);

            #region
            mtdLoadCBLVariable();
            foreach (clsDTOPerfilVariable objPerfilVar in lstPerfilVar)
            {
                for (int j = 0; j < chbVarAsoc.Items.Count; j++)
                {
                    if (objPerfilVar.StrIdVariable == chbVarAsoc.Items[j].Value.ToString().Trim())
                    {
                        chbVarAsoc.Items[j].Selected = true;
                        break;
                    }
                }
            }
            #endregion
        }
        #endregion

        #region Buttons
        protected void ibtnCancelPerfilVar_Click(object sender, EventArgs e)
        {
            mtdResetValues();
        }

        protected void ibtnGuardarPerfilVar_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (mtdHayVarEscogida(chbVarAsoc))
            {
                mtdGuardarPerfilVar(ddlPerfiles.SelectedValue.ToString().Trim(), chbVarAsoc, ref strErrMsg);
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    mtdResetValues();
                    Session["UltFactor"] = string.Empty;
                    mtdMensaje("La relación [Factor de Riesgo - Señales] fue modificada exitósamente.");
                }
            }
        }
        #endregion

        #region Methods
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdResetValues()
        {
            mtdLoadDDLPerfil();
            mtdLoadCBLVariable();
        }

        private bool mtdHayVarEscogida(CheckBoxList chbVars)
        {
            bool booExiste = false;

            for (int i = 0; i < chbVars.Items.Count; i++)
            {
                if (chbVars.Items[i].Selected)
                {
                    booExiste = true;
                    break;
                }
            }

            return booExiste;
        }

        private void mtdGuardarPerfilVar(string strIdPerfil, CheckBoxList chbVariables, ref string strErrMsg)
        {
            #region Vars
            clsParamArchivo cParam = new clsParamArchivo();
            clsDTOPerfil objPerfil = null;
            #endregion

            objPerfil = new clsDTOPerfil(string.Empty, strIdPerfil, string.Empty, string.Empty, string.Empty);
            cParam.mtdEliminarPerfilVariable(objPerfil, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                for (int i = 0; i < chbVariables.Items.Count; i++)
                {
                    if (chbVariables.Items[i].Selected)
                    {
                        clsDTOPerfilVariable objPerfilVar = new clsDTOPerfilVariable(string.Empty, strIdPerfil, chbVariables.Items[i].Value.ToString().Trim());
                        cParam.mtdGuardarPerfilVariable(objPerfilVar, ref strErrMsg);
                    }
                }
        }

        #endregion
    }
}