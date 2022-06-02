using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using Microsoft.Security.Application;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using System.Configuration;
using ClosedXML.Excel;
using System.Net.Mail;

namespace ListasSarlaft.UserControls.Riesgos.Gestion
{
    public partial class GestionRiesgoIndicador : System.Web.UI.UserControl
    {
        string IdFormulario = "5028";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdInicializarValores();
                }
            }
        }
        #region Properties
        private DataTable infoGridRiesgoIndicador;
        private int rowGridRiesgoIndicador;
        private int pagIndexRiesgoIndicador;

        private DataTable InfoGridRiesgoIndicador
        {
            get
            {
                infoGridRiesgoIndicador = (DataTable)ViewState["infoGridRiesgoIndicador"];
                return infoGridRiesgoIndicador;
            }
            set
            {
                infoGridRiesgoIndicador = value;
                ViewState["infoGridRiesgoIndicador"] = infoGridRiesgoIndicador;
            }
        }

        private int RowGridRiesgoIndicador
        {
            get
            {
                rowGridRiesgoIndicador = (int)ViewState["rowGridRiesgoIndicador"];
                return rowGridRiesgoIndicador;
            }
            set
            {
                rowGridRiesgoIndicador = value;
                ViewState["rowGridRiesgoIndicador"] = rowGridRiesgoIndicador;
            }
        }

        private int PagIndexRiesgoIndicador
        {
            get
            {
                pagIndexRiesgoIndicador = (int)ViewState["pagIndexRiesgoIndicador"];
                return pagIndexRiesgoIndicador;
            }
            set
            {
                pagIndexRiesgoIndicador = value;
                ViewState["pagIndexRiesgoIndicador"] = pagIndexRiesgoIndicador;
            }
        }
        private DataTable infoGridVarRiesgoIndicador;
        private int rowGridVarRiesgoIndicador;
        private int pagIndexVarRiesgoIndicador;

        private DataTable InfoGridVarRiesgoIndicador
        {
            get
            {
                infoGridVarRiesgoIndicador = (DataTable)ViewState["infoGridVarRiesgoIndicador"];
                return infoGridVarRiesgoIndicador;
            }
            set
            {
                infoGridVarRiesgoIndicador = value;
                ViewState["infoGridVarRiesgoIndicador"] = infoGridVarRiesgoIndicador;
            }
        }
        private int RowGridVarRiesgoIndicador
        {
            get
            {
                rowGridVarRiesgoIndicador = (int)ViewState["rowGridVarRiesgoIndicador"];
                return rowGridVarRiesgoIndicador;
            }
            set
            {
                rowGridVarRiesgoIndicador = value;
                ViewState["rowGridVarRiesgoIndicador"] = rowGridVarRiesgoIndicador;
            }
        }
        private int PagIndexVarRiesgoIndicador
        {
            get
            {
                pagIndexVarRiesgoIndicador = (int)ViewState["pagIndexVarRiesgoIndicador"];
                return pagIndexVarRiesgoIndicador;
            }
            set
            {
                pagIndexVarRiesgoIndicador = value;
                ViewState["pagIndexVarRiesgoIndicador"] = pagIndexVarRiesgoIndicador;
            }
        }
        private DataTable infoGridValorVariable;
        private int rowGridValorVariable;
        private int pagIndexValorVariable;

        private DataTable InfoGridValorVariable
        {
            get
            {
                infoGridValorVariable = (DataTable)ViewState["infoGridValorVariable"];
                return infoGridValorVariable;
            }
            set
            {
                infoGridValorVariable = value;
                ViewState["infoGridValorVariable"] = infoGridValorVariable;
            }
        }

        private int RowGridValorVariable
        {
            get
            {
                rowGridValorVariable = (int)ViewState["rowGridValorVariable"];
                return rowGridValorVariable;
            }
            set
            {
                rowGridValorVariable = value;
                ViewState["rowGridValorVariable"] = rowGridValorVariable;
            }
        }

        private int PagIndexValorVariable
        {
            get
            {
                pagIndexValorVariable = (int)ViewState["pagIndexValorVariable"];
                return pagIndexValorVariable;
            }
            set
            {
                pagIndexValorVariable = value;
                ViewState["pagIndexValorVariable"] = pagIndexValorVariable;
            }
        }
        #endregion Properties
        private void mtdInicializarValores()
        {
            PagIndexRiesgoIndicador = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            /*mtdLoadEvaluacionProveedor(ref strErrMsg);*/
            if (!mtdLoadRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        #region Metodos Load Riesgos Indicadores
        private bool mtdLoadRiesgosIndicadores(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOriesgosIndicadores objRiesgoInd = new clsDTOriesgosIndicadores();
            List<clsDTOriesgosIndicadores> lstRiesgoInd = new List<clsDTOriesgosIndicadores>();
            clsBLLriesgosIndicadores cRiesgoInd = new clsBLLriesgosIndicadores();
            #endregion Vars
            lstRiesgoInd = cRiesgoInd.mtdConsultarRiesgosIndicadoresGestion(booResult, ref strErrMsg, Convert.ToInt32(Session["IdUsuario"].ToString()));

            if (lstRiesgoInd != null)
            {
                mtdLoadRiesgosIndicadores();
                mtdLoadRiesgosIndicadores(lstRiesgoInd);
                GVriesgosIndicadores.DataSource = lstRiesgoInd;
                GVriesgosIndicadores.PageIndex = pagIndexRiesgoIndicador;
                GVriesgosIndicadores.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay datos para mostrar";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadRiesgosIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdRiesgoIndicador", typeof(string));
            grid.Columns.Add("strNombreIndicador", typeof(string));
            grid.Columns.Add("strObjetivoIndicador", typeof(string));
            grid.Columns.Add("intIProcesoIndicador", typeof(string));
            grid.Columns.Add("intIdProceso", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("intIdResponsableMedicion", typeof(string));
            grid.Columns.Add("strResponsableMedicion", typeof(string));
            grid.Columns.Add("intIdFrecuenciaMedicion", typeof(string));
            grid.Columns.Add("strFrecuenciaMedicion", typeof(string));
            grid.Columns.Add("intIdRiesgoAsociado", typeof(string));
            grid.Columns.Add("strCodRiesgo", typeof(string));
            grid.Columns.Add("strNombreRiesgo", typeof(string));
            grid.Columns.Add("intIdFormula", typeof(string));
            grid.Columns.Add("strNominador", typeof(string));
            grid.Columns.Add("strDenominador", typeof(string));
            grid.Columns.Add("intIdMeta", typeof(string));
            grid.Columns.Add("intIdEsquemaSeguimiento", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaCreacion", typeof(string));
            grid.Columns.Add("booActivo", typeof(string));

            GVriesgosIndicadores.DataSource = grid;
            GVriesgosIndicadores.DataBind();
            InfoGridRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstRiesgoInd">Lista con los Indicadores de riesgos</param>
        private void mtdLoadRiesgosIndicadores(List<clsDTOriesgosIndicadores> lstRiesgoInd)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOriesgosIndicadores objRiesgoIndicador in lstRiesgoInd)
            {

                InfoGridRiesgoIndicador.Rows.Add(new Object[] {
                    objRiesgoIndicador.intIdRiesgoIndicador.ToString().Trim(),
                    objRiesgoIndicador.strNombreIndicador.ToString().Trim(),
                    objRiesgoIndicador.strObjetivoIndicador.ToString().Trim(),
                    objRiesgoIndicador.intIProcesoIndicador.ToString().Trim(),
                    objRiesgoIndicador.intIdProceso.ToString().Trim(),
                    objRiesgoIndicador.strNombreProceso.ToString().Trim(),
                    objRiesgoIndicador.intIdResponsableMedicion.ToString().Trim(),
                    objRiesgoIndicador.strResponsableMedicion.ToString().Trim(),
                    objRiesgoIndicador.intIdFrecuenciaMedicion.ToString().Trim(),
                    objRiesgoIndicador.strFrecuenciaMedicion.ToString().Trim(),
                    objRiesgoIndicador.intIdRiesgoAsociado.ToString().Trim(),
                    objRiesgoIndicador.strCodRiesgo.ToString().Trim(),
                    objRiesgoIndicador.strNombreRiesgo.ToString().Trim(),
                    objRiesgoIndicador.intIdFormula.ToString().Trim(),
                    objRiesgoIndicador.strNominador.ToString().Trim(),
                    objRiesgoIndicador.strDenominador.ToString().Trim(),
                    objRiesgoIndicador.intIdMeta.ToString().Trim(),
                    objRiesgoIndicador.intIdEsquemaSeguimiento.ToString().Trim(),
                    objRiesgoIndicador.strUsuario.ToString().Trim(),
                    objRiesgoIndicador.dtFechaCreacion.ToString().Trim(),
                    objRiesgoIndicador.booActivo.ToString().Trim()
                    });
            }
        }
        #endregion Metodos Load Riesgos Indicadores

        protected void GVriesgosIndicadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "gestionar":
                    mtdShowGestionVariable(RowGrid);
                    break;
            }
            
        }
        private void mtdShowGestionVariable(int RowGrid)
        {
            string strErrMsg = string.Empty;
            GridViewRow row = GVriesgosIndicadores.Rows[RowGrid];
            var colsNoVisible = GVriesgosIndicadores.DataKeys[RowGrid].Values;
            int IdRiesgoIndicador = Convert.ToInt32(row.Cells[0].Text);
            Session["IdRiesgoIndicador"] = Convert.ToInt32(IdRiesgoIndicador);
            Session["IdFrecuenciaMedicion"] = Convert.ToInt32(colsNoVisible[6].ToString());
            
            if (!mtdLoadVariablesRiesgosIndicadores(ref strErrMsg, IdRiesgoIndicador))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        #region Load Variables
        private bool mtdLoadVariablesRiesgosIndicadores(ref string strErrMsg, int IdRiesgoIndicador)
        {
            #region Vars
            bool booResult = false;
            clsDTOvariableRiesgoIndicador objVarRiesgoInd = new clsDTOvariableRiesgoIndicador();
            List<clsDTOvariableRiesgoIndicador> lstVarRiesgoInd = new List<clsDTOvariableRiesgoIndicador>();
            clsBLLvariableRiesgoIndicador cVarRiesgoInd = new clsBLLvariableRiesgoIndicador();
            #endregion Vars
            lstVarRiesgoInd = cVarRiesgoInd.mtdConsultarVariableRiesgoIndicador(booResult, ref strErrMsg, IdRiesgoIndicador);

            if (lstVarRiesgoInd != null)
            {
                mtdLoadVariablesRiesgosIndicadores();
                mtdLoadVariablesRiesgosIndicadores(lstVarRiesgoInd);
                GridViewVariables.DataSource = lstVarRiesgoInd;
                GridViewVariables.PageIndex = pagIndexVarRiesgoIndicador;
                GridViewVariables.DataBind();
                booResult = true;
                dvVariables.Visible = true;
                BodyGridGRI.Visible = false;
                int IdFrecuencia = Convert.ToInt32(Session["IdFrecuenciaMedicion"].ToString());
                loadDDLDetalleFrecuencias(IdFrecuencia);
            }
            else
            {
                strErrMsg = "No hay registros de Variables";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadVariablesRiesgosIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdVariableRiesgoIndicador", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("dblValorVariable", typeof(string));
            grid.Columns.Add("intIdFormato", typeof(string));
            grid.Columns.Add("strFormato", typeof(string));

            GridViewVariables.DataSource = grid;
            GridViewVariables.DataBind();
            InfoGridVarRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstVarRiesgoInd">Lista con las Variables de Indicadores de riesgos</param>
        private void mtdLoadVariablesRiesgosIndicadores(List<clsDTOvariableRiesgoIndicador> lstVarRiesgoInd)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOvariableRiesgoIndicador objVarRiesgoIndicador in lstVarRiesgoInd)
            {

                InfoGridVarRiesgoIndicador.Rows.Add(new Object[] {
                    objVarRiesgoIndicador.intIdVariableRiesgoIndicador.ToString().Trim(),
                    objVarRiesgoIndicador.strDescripcion.ToString().Trim(),
                    objVarRiesgoIndicador.dblValorVariable.ToString().Trim(),
                    objVarRiesgoIndicador.intIdFormato.ToString().Trim(),
                    objVarRiesgoIndicador.strFormato.ToString().Trim()
                    });
            }
        }
        #endregion Load Variables

        protected void GridViewVariables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "valor":
                    mtdGestionVariable(RowGrid);
                    break;
            }
        }
        private void mtdGestionVariable(int RowGrid)
        {
            string strErrMsg = string.Empty;
            GridViewRow row = GridViewVariables.Rows[RowGrid];
            var colsNoVisible = GridViewVariables.DataKeys[RowGrid].Values;
            int IdVariable = Convert.ToInt32(colsNoVisible[1].ToString());
            Session["IdVariable"] = IdVariable;
            int IdFrecuencia = Convert.ToInt32(Session["IdFrecuenciaMedicion"].ToString());
            loadDDLDetalleFrecuencias(IdFrecuencia);
            if (IdFrecuencia == 2)
                TextBox12.Visible = true;
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
                ddlDetalleFrecuencias.Visible = true;
            if (IdFrecuencia == 9)
                txtFrecuenciaAno.Visible = true;
            dvVariableGestion.Visible = true;
            dvVariables.Visible = false;
            if(!mtdLoadValorVariables(ref strErrMsg, IdVariable))
            {
                omb.ShowMessage("No hay variables gestionadas",2,"Atención");
            }
        }
        private void loadDDLDetalleFrecuencias(int IdFrecuencia)
        {
            if (ddlDetalleFrecuencias.Items.Count > 0)
                ddlDetalleFrecuencias.Items.Clear();
            try
            {
                string strErrMsg = string.Empty;
                DataTable dtInfo = new DataTable();
                clsBLLFrecuenciaMedicion cFrecuencia = new clsBLLFrecuenciaMedicion();
                dtInfo = cFrecuencia.mtdConsultarDetalleFrecuencia(ref strErrMsg, IdFrecuencia);

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlDetalleFrecuencias.Items.Insert(i, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleFecruencia"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Erro al cargar El detalle de la frecuencia. " + ex.Message, 2, "Atención");
            }
        }

        protected void BtnGuardarVariable_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertarValorVariable(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFildValorVariable();
            }
        }
        private bool mtdInsertarValorVariable(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOvalorVariable objValorVariable = new clsDTOvalorVariable();
            clsBLLvariableRiesgoIndicador cRieInd = new clsBLLvariableRiesgoIndicador();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objValorVariable.intIdRiesgoIndicador = Convert.ToInt32(Session["IdRiesgoIndicador"].ToString());
            objValorVariable.intIdFrecuencia = Convert.ToInt32(Session["IdFrecuenciaMedicion"].ToString());
            objValorVariable.intIdVariable = Convert.ToInt32(Session["IdVariable"].ToString());
            int IdFrecuencia = Convert.ToInt32(Session["IdFrecuenciaMedicion"].ToString());
            if (IdFrecuencia == 2)
            {
                objValorVariable.intIdDetalleFrecuencia = 0;
                objValorVariable.strValorFrecuencia = Sanitizer.GetSafeHtmlFragment(TextBox12.Text);
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
            {
                objValorVariable.intIdDetalleFrecuencia = Convert.ToInt32(ddlDetalleFrecuencias.SelectedValue);
                if (IdFrecuencia == 3 || IdFrecuencia == 4)
                {
                    objValorVariable.strMes = ddlMesMetas.SelectedValue;
                }
                else
                {
                    objValorVariable.strMes = ddlDetalleFrecuencias.SelectedItem.Text;
                }
            }

            if (IdFrecuencia == 9)
            {
                objValorVariable.intIdDetalleFrecuencia = 0;
                objValorVariable.strValorFrecuencia = Sanitizer.GetSafeHtmlFragment(txtFrecuenciaAno.Text);
            }
            objValorVariable.dblValorVariable = Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtValorVariable.Text));
            objValorVariable.strAño = Sanitizer.GetSafeHtmlFragment(txbAñoMeta.Text);
            //objValorVariable.strMes = ddlMesMetas.SelectedValue;
            booResult = cRieInd.mtdInsertarValorVariable(objValorVariable, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Valor de la Variable registrada exitosamente";
                
            }
            else
            {
                strErrMsg = "Error al registrar el valor de la variable";
            }
            return booResult;
        }

        
        #region Load ValorVariablesGrid
        private bool mtdLoadValorVariables(ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            clsDTOvalorVariable objValor = new clsDTOvalorVariable();
            List<clsDTOvalorVariable> lstValorVariable = new List<clsDTOvalorVariable>();
            clsBLLvariableRiesgoIndicador cRiesgoInd = new clsBLLvariableRiesgoIndicador();
            #endregion Vars
            lstValorVariable = cRiesgoInd.mtdConsultarValorVariable(booResult, ref strErrMsg, Convert.ToInt32(Session["IdRiesgoIndicador"].ToString()),  IdVariable);

            if (lstValorVariable != null)
            {
                mtdLoadValorVariables();
                mtdLoadValorVariables(lstValorVariable);
                GVvalorVariable.DataSource = lstValorVariable;
                GVvalorVariable.PageIndex = pagIndexValorVariable;
                GVvalorVariable.DataBind();
                trGridVariable.Visible = true;
                int IdFrecuencia = Convert.ToInt32(Session["IdFrecuenciaMedicion"].ToString());
                loadDDLDetalleFrecuencias(IdFrecuencia);
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay datos para mostrar";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadValorVariables()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdValorVariable", typeof(string));
            grid.Columns.Add("intIdVariable", typeof(string));
            grid.Columns.Add("intIdFrecuencia", typeof(string));
            grid.Columns.Add("strFrecuenciaMedicion", typeof(string));
            grid.Columns.Add("strValorFrecuencia", typeof(string));
            grid.Columns.Add("dblValorVariable", typeof(string));
            grid.Columns.Add("intIdRiesgoIndicador", typeof(string));
            grid.Columns.Add("intIdDetalleFrecuencia", typeof(string));
            grid.Columns.Add("strDescripcionDetalle", typeof(string));
            grid.Columns.Add("strAño", typeof(string));
            grid.Columns.Add("strMes", typeof(string));

            GVvalorVariable.DataSource = grid;
            GVvalorVariable.DataBind();
            InfoGridValorVariable = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstRiesgoInd">Lista con los Indicadores de riesgos</param>
        private void mtdLoadValorVariables(List<clsDTOvalorVariable> lstValorVariable)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOvalorVariable objValor in lstValorVariable)
            {

                InfoGridValorVariable.Rows.Add(new Object[] {
                    objValor.intIdValorVariable.ToString().Trim(),
                    objValor.intIdVariable.ToString().Trim(),
                    objValor.intIdFrecuencia.ToString().Trim(),
                    objValor.strFrecuenciaMedicion.ToString().Trim(),
                    objValor.strValorFrecuencia.ToString().Trim(),
                    objValor.dblValorVariable.ToString().Trim(),
                    objValor.intIdRiesgoIndicador.ToString().Trim(),
                    objValor.intIdDetalleFrecuencia.ToString().Trim(),
                    objValor.strDescripcionDetalle.ToString().Trim(),
                    objValor.strAño.ToString().Trim(),
                    objValor.strMes.ToString().Trim()
                    });
            }
        }
        #endregion Load ValorVariablesGrid

        protected void GVvalorVariable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "modificar":
                    mtdShowUpdate(RowGrid);
                    break;
            }
        }
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            Tr9.Visible = true;
            Tr30.Visible = true;
            TrValorVariable.Visible = true;
            trButtonAdd.Visible = true;
            trGridVariable.Visible = false;
            trButtonNewV.Visible = false;
            

            BtnGuardarVariable.Visible = true;
            BtnActualiarVariable.Visible = false;
            int IdFrecuencia = Convert.ToInt32(Session["IdFrecuenciaMedicion"].ToString());
            //lblFrecuenciaMedicion.Text = IdFrecuencia.ToString();
            if (IdFrecuencia == 2)
            {
                TextBox12.Visible = true;
                RequiredFieldValidatorDia.Enabled = true;
                ddlDetalleFrecuencias.Visible = false;
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
            {
                CompareValidatorAno.Enabled = true;
                ddlDetalleFrecuencias.Visible = true;
                TextBox12.Visible = false;
            }
            if (IdFrecuencia == 9)
            {                
                rfvFrecuenciaAno.Enabled = true;
                TrfechasIns.Visible = false;
                ddlDetalleFrecuencias.Visible = false;
                TextBox12.Visible = false;
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4)
            {
                TrfechasIns.Visible = true;
            }
            else
            {
                if (IdFrecuencia != 9 && IdFrecuencia != 2)
                {
                    TrfechasIns.Visible = true;
                    TdAñoMeta.Visible = true;
                    TdValorAñoMeta.Visible = true;
                    TdmesMeta.Visible = false;
                    TdvalorMesMeta.Visible = false;
                }
            }
        }
        private void mtdShowUpdate(int RowGrid)
        {
            GridViewRow row = GVvalorVariable.Rows[RowGrid];
            var colsNoVisible = GVvalorVariable.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;
            int IdVariable = Convert.ToInt32(colsNoVisible[1].ToString());
            Session["IdVariable"] = IdVariable;
            int IdFrecuencia = Convert.ToInt32(colsNoVisible[2].ToString());
            Session["IdFrecuencia"] = IdFrecuencia;
            
            if (IdFrecuencia == 2)
            {
                TextBox12.Text = ((Label)row.FindControl("strValorFrecuencia")).Text;
                TextBox12.Visible = true;
                RequiredFieldValidatorDia.Enabled = true;
                ddlDetalleFrecuencias.Visible = false;
                rfvFrecuenciaAno.Enabled = false;
                txtFrecuenciaAno.Visible = false;
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
            {
                ddlDetalleFrecuencias.SelectedValue = colsNoVisible[4].ToString();
                TextBox12.Visible = false;
                CompareValidatorAno.Enabled = true;
                rfvFrecuenciaAno.Enabled = false;
                txtFrecuenciaAno.Visible = false;
            }
            if (IdFrecuencia == 9)
            {
                txtFrecuenciaAno.Text = ((Label)row.FindControl("strValorFrecuencia")).Text;
                rfvFrecuenciaAno.Enabled = true;
                txtFrecuenciaAno.Visible = true;
                ddlDetalleFrecuencias.Visible = false;
                TrfechasIns.Visible = false;
                TextBox12.Visible = false;
            }
            txtValorVariable.Text = row.Cells[4].Text;
            lblIdVariable.Text = colsNoVisible[0].ToString();
            txbAñoMeta.Text = ((Label)row.FindControl("strAño")).Text;
            //if (((Label)row.FindControl("strMes")).Text != string.Empty)
            //    ddlMesMetas.Items.Text = ((Label)row.FindControl("strMes")).Text;
            if (IdFrecuencia == 3 || IdFrecuencia == 4)
            {
                TrfechasIns.Visible = true;
            }else
            {
                if(IdFrecuencia != 9 && IdFrecuencia != 2)
                {
                    TrfechasIns.Visible = true;
                    TdAñoMeta.Visible = true;
                    TdValorAñoMeta.Visible = true;
                    TdmesMeta.Visible = false;
                    TdvalorMesMeta.Visible = false;
                }
                
            }
            Tr9.Visible = true;
            Tr30.Visible = true;
            TrValorVariable.Visible = true;
            trButtonAdd.Visible = true;
            trGridVariable.Visible = false;
            trButtonNewV.Visible = false;

            BtnGuardarVariable.Visible = false;
            BtnActualiarVariable.Visible = true;
        }

        protected void BtnActualiarVariable_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if(!mtdActualizarVariable(ref strErrMsg))
            {
                omb.ShowMessage(strErrMsg,1,"Atención");
                mtdResetFildValorVariable();
            }else
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFildValorVariable();
            }
        }
        private bool mtdActualizarVariable(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOvalorVariable objValorVariable = new clsDTOvalorVariable();
            clsBLLvariableRiesgoIndicador cRieInd = new clsBLLvariableRiesgoIndicador();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objValorVariable.intIdValorVariable = Convert.ToInt32(lblIdVariable.Text);
            objValorVariable.intIdFrecuencia = Convert.ToInt32(Session["IdFrecuencia"].ToString());
            objValorVariable.intIdVariable = Convert.ToInt32(Session["IdVariable"].ToString());
            int IdFrecuencia = Convert.ToInt32(Session["IdFrecuencia"].ToString());
            if (IdFrecuencia == 2)
            {
                objValorVariable.intIdDetalleFrecuencia = 0;
                objValorVariable.strValorFrecuencia = Sanitizer.GetSafeHtmlFragment(TextBox12.Text);
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
            {
                objValorVariable.intIdDetalleFrecuencia = Convert.ToInt32(ddlDetalleFrecuencias.SelectedValue);
                if(IdFrecuencia == 3 || IdFrecuencia == 4)
                {
                    objValorVariable.strMes = ddlMesMetas.SelectedValue;
                }else
                {
                    objValorVariable.strMes = ddlDetalleFrecuencias.SelectedItem.Text;
                }
            }

            if (IdFrecuencia == 9)
            {
                objValorVariable.intIdDetalleFrecuencia = 0;
                objValorVariable.strValorFrecuencia = Sanitizer.GetSafeHtmlFragment(txtFrecuenciaAno.Text);
            }
            objValorVariable.dblValorVariable = Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtValorVariable.Text));
            objValorVariable.strAño = Sanitizer.GetSafeHtmlFragment(txbAñoMeta.Text);
            
            booResult = cRieInd.mtdActualizarValorVariable(objValorVariable, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Valor de la Variable registrada exitosamente";

            }
            else
            {
                strErrMsg = "Error al registrar el valor de la variable";
            }
            return booResult;
        }
        private void mtdResetFildValorVariable()
        {
            Tr9.Visible = false;
            Tr30.Visible = false;
            TrValorVariable.Visible = false;
            trButtonAdd.Visible = false;
            trGridVariable.Visible = false;
            trButtonNewV.Visible = true;

            TextBox12.Text = string.Empty;
            ddlDetalleFrecuencias.Items.Clear();
            txtFrecuenciaAno.Text = string.Empty;
            txtValorVariable.Text = string.Empty;
            TrfechasIns.Visible = false;
            txbAñoMeta.Text = string.Empty;
            ddlMesMetas.SelectedIndex = 0;
            string strErrMsg = string.Empty;
            if (!mtdLoadValorVariables(ref strErrMsg, Convert.ToInt32(Session["IdVariable"].ToString())))
            {
                omb.ShowMessage("No hay variables gestionadas", 2, "Atención");
            }
        }

        protected void BtnCancelaVariable_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFildValorVariable();
            TextBox12.Visible = false;
            txtFrecuenciaAno.Visible = false;
        }

        protected void btnCancelVariable_Click(object sender, ImageClickEventArgs e)
        {
            dvVariableGestion.Visible = false;
            dvVariables.Visible = true;
            trGridVariable.Visible = false;
            string strErrMsg = string.Empty;
            int IdRiesgoIndicador = Convert.ToInt32(Session["IdRiesgoIndicador"].ToString());
            if (!mtdLoadVariablesRiesgosIndicadores(ref strErrMsg, IdRiesgoIndicador))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void btnCancelIndicador_Click(object sender, ImageClickEventArgs e)
        {
            dvVariables.Visible = false;
            BodyGridGRI.Visible = true;
            HeadGRI.Visible = true;
            mtdStard();
        }

        protected void GVriesgosIndicadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndexRiesgoIndicador = e.NewPageIndex;
            /*GVevaluacionDesempeño.PageIndex = PagIndex1;
            GVevaluacionDesempeño.DataBind();*/
            string strErrMsg = "";
            mtdLoadRiesgosIndicadores(ref strErrMsg);
        }
    }
  }