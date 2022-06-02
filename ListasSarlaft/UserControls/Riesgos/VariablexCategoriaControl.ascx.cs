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

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class VariablexCategoriaControl : System.Web.UI.UserControl
    {
        string IdFormulario = "5029";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.GVvariablesCalificacionControl);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
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
        private DataTable infoGridVCC;
        private int rowGridVCC;
        private int pagIndexVCC;
        private DataTable infoGridCategoria;
        private int rowGridCategoria;
        private int pagIndexCategoria;
        private DataTable InfoGridVCC
        {
            get
            {
                infoGridVCC = (DataTable)ViewState["infoGridVCC"];
                return infoGridVCC;
            }
            set
            {
                infoGridVCC = value;
                ViewState["infoGridVCC"] = infoGridVCC;
            }
        }

        private int RowGridVCC
        {
            get
            {
                rowGridVCC = (int)ViewState["rowGridVCC"];
                return rowGridVCC;
            }
            set
            {
                rowGridVCC = value;
                ViewState["rowGridVCC"] = rowGridVCC;
            }
        }

        private int PagIndexVCC
        {
            get
            {
                pagIndexVCC = (int)ViewState["pagIndexVCC"];
                return pagIndexVCC;
            }
            set
            {
                pagIndexVCC = value;
                ViewState["pagIndexVCC"] = pagIndexVCC;
            }
        }
        private DataTable InfoGridCategoria
        {
            get
            {
                infoGridCategoria = (DataTable)ViewState["infoGridCategoria"];
                return infoGridCategoria;
            }
            set
            {
                infoGridCategoria = value;
                ViewState["infoGridCategoria"] = infoGridCategoria;
            }
        }

        private int RowGridCategoria
        {
            get
            {
                rowGridCategoria = (int)ViewState["rowGridCategoria"];
                return rowGridCategoria;
            }
            set
            {
                rowGridCategoria = value;
                ViewState["rowGridCategoria"] = rowGridCategoria;
            }
        }

        private int PagIndexCategoria
        {
            get
            {
                pagIndexCategoria = (int)ViewState["pagIndexCategoria"];
                return pagIndexCategoria;
            }
            set
            {
                pagIndexCategoria = value;
                ViewState["pagIndexCategoria"] = pagIndexCategoria;
            }
        }
        #endregion
        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndexVCC = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            if (!mtdLoadVariablesValificacionControl(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");

        }
        protected void mtdResetFields()
        {
            BodyFormVCC.Visible = false;
            BodyGridVCC.Visible = true;
            dvGridCategoriasxVariable.Visible = false;
            /*txtId.Text = string.Empty;
            txtDescripcionVariable.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;*/
        }
        protected void mtdResetFieldsCategorias()
        {
            BodyFormVCC.Visible = false;
            dvGridCategoriasxVariable.Visible = true;
            txtId.Text = string.Empty;
            ddlCategorias.Items.Clear();
            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;
        }
        private bool mtdLoadVariablesValificacionControl(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
            List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
            #endregion Vars
            lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);

            if (lstVariables != null)
            {
                mtdLoadVariablesValificacionControl();
                mtdLoadVariablesValificacionControl(lstVariables);
                GVvariablesCalificacionControl.DataSource = lstVariables;
                GVvariablesCalificacionControl.PageIndex = pagIndexVCC;
                GVvariablesCalificacionControl.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay variables registradas";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadVariablesValificacionControl()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdCalificacionControl", typeof(string));
            grid.Columns.Add("strDescripcionVariable", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));
            grid.Columns.Add("booActivo", typeof(string));

            GVvariablesCalificacionControl.DataSource = grid;
            GVvariablesCalificacionControl.DataBind();
            InfoGridVCC = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstVariables">Lista con los Indicadores</param>
        private void mtdLoadVariablesValificacionControl(List<clsDTOVariableCalificacionControl> lstVariables)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsDTOVariableCalificacionControl objVariable in lstVariables)
            {

                InfoGridVCC.Rows.Add(new Object[] {
                    objVariable.intIdCalificacionControl.ToString().Trim(),
                    objVariable.strDescripcionVariable.ToString().Trim(),
                    objVariable.intIdUsuario.ToString().Trim(),
                    objVariable.dtFechaRegistro.ToString().Trim(),
                    objVariable.strUsuario.ToString().Trim(),
                    objVariable.booActivo.ToString().Trim()
                    });
            }
        }
        private bool mtdLoadCategorias(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsDTOCategoriasVariableControl> lstCategorias = new List<clsDTOCategoriasVariableControl>();
            clsBLLParametrizacionAsignacionCategoriaVariable cCategorias = new clsBLLParametrizacionAsignacionCategoriaVariable();
            #endregion Vars

            try
            {
                lstCategorias = cCategorias.mtdConsultarCategoriaxVariablesControl(ref lstCategorias, ref strErrMsg);
                ddlCategorias.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCategorias != null)
                    {
                        int intCounter = 1;

                        foreach (clsDTOCategoriasVariableControl objCategorias in lstCategorias)
                        {
                            ddlCategorias.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objCategorias.strDescripcionCategoria, objCategorias.intIdCategoriaVariableControl.ToString()));
                            intCounter++;
                        }
                        booResult = true;
                    }
                    else
                    {
                        booResult = false;
                        strErrMsg = "No hay categorias para asignar";
                    }
                        
                }
                else
                {
                    booResult = false;
                    strErrMsg = "No hay categorias para asignar";
                }
                    
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la carga de Categorias. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }
        #endregion Metodos

        protected void GVvariablesCalificacionControl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridVCC = Convert.ToInt16(e.CommandArgument);
            
            switch (e.CommandName)
            {
                case "Seleccionar":
                    GridViewRow row = GVvariablesCalificacionControl.Rows[RowGridVCC];
                    var colsNoVisible = GVvariablesCalificacionControl.DataKeys[RowGridVCC].Values;
                    Session["IdVariable"] = colsNoVisible[2].ToString();
                    mtdStardCategorias();
                    break;
            }
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (!mtdInsertarCategoriaxVariablesControl(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");

        }
        public bool mtdInsertarCategoriaxVariablesControl(ref string strErrMsg)
        {
            bool booResult = false;
            clsDTOParametrizacionAsignarCategoriaVariable objVariable = new clsDTOParametrizacionAsignarCategoriaVariable();
            clsBLLParametrizacionAsignacionCategoriaVariable cVariable = new clsBLLParametrizacionAsignacionCategoriaVariable();
            objVariable.intIdVariable = Convert.ToInt32(Session["IdVariable"].ToString());
            objVariable.intIdCategoria = Convert.ToInt32(ddlCategorias.SelectedValue);
            objVariable.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objVariable.dtFechaRegistro = DateTime.Now;
            booResult = cVariable.mtdInsertarVariableCalificacion(objVariable, ref strErrMsg);
            if (booResult == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFieldsCategorias();
                mtdStardCategorias();
            }
            return booResult;
        }
        public void mtdStardCategorias()
        {
            string strErrMsg = string.Empty;
            if (!mtdConsultarCategoriasAsignadas(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            dvGridCategoriasxVariable.Visible = true;
            BodyGridVCC.Visible = false;
        }
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            dvGridCategoriasxVariable.Visible = true;
            string strErrMsg = string.Empty;
            if (!mtdLoadCategorias(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
            BodyFormVCC.Visible = true;
            dvGridCategoriasxVariable.Visible = false;
        }
        private bool mtdConsultarCategoriasAsignadas(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            //clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
            List<clsDTOParametrizacionAsignarCategoriaVariable> lstVariables = new List<clsDTOParametrizacionAsignarCategoriaVariable>();
            clsBLLParametrizacionAsignacionCategoriaVariable cVariable = new clsBLLParametrizacionAsignacionCategoriaVariable();
            #endregion Vars
            lstVariables = cVariable.mtdConsultarCategoriasAsignadas(ref lstVariables, ref strErrMsg, Convert.ToInt32(Session["IdVariable"].ToString()));

            if (lstVariables != null)
            {
                mtdLoadCategoriasAsignadas();
                mtdLoadCategoriasAsignadas(lstVariables);
                GVcategoriasxVariable.DataSource = lstVariables;
                GVcategoriasxVariable.PageIndex = pagIndexCategoria;
                GVcategoriasxVariable.DataBind();
                booResult = true;
                BodyGridVCC.Visible = true;
            }
            else
            {
                strErrMsg = "No hay categorias asociadas a la variable seleccionada";
                GVcategoriasxVariable.DataSource = null;
                GVcategoriasxVariable.DataBind();
                BodyGridVCC.Visible = false;
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadCategoriasAsignadas()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdVariableCategoria", typeof(string));
            grid.Columns.Add("intIdVariable", typeof(string));
            grid.Columns.Add("strVariable", typeof(string));
            grid.Columns.Add("intIdCategoria", typeof(string));
            grid.Columns.Add("strCategoria", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));

            GVcategoriasxVariable.DataSource = grid;
            GVcategoriasxVariable.DataBind();
            InfoGridCategoria = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstVariables">Lista con los Indicadores</param>
        private void mtdLoadCategoriasAsignadas(List<clsDTOParametrizacionAsignarCategoriaVariable> lstVariables)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsDTOParametrizacionAsignarCategoriaVariable objVariable in lstVariables)
            {

                InfoGridCategoria.Rows.Add(new Object[] {
                    objVariable.intIdVariableCategoria.ToString().Trim(),
                    objVariable.intIdVariable.ToString().Trim(),
                    objVariable.strVariable.ToString().Trim(),
                    objVariable.intIdCategoria.ToString().Trim(),
                    objVariable.strCategoria.ToString().Trim(),
                    objVariable.dtFechaRegistro.ToString().Trim(),
                    objVariable.strUsuario.ToString().Trim(),
                    objVariable.intIdUsuario.ToString().Trim()
                    });
            }
        }

        protected void ImbCancelCategoria_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
            mtdStard();
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFieldsCategorias();
            string strErrMsg = string.Empty;
            if (!mtdConsultarCategoriasAsignadas(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            dvGridCategoriasxVariable.Visible = true;
            BodyGridVCC.Visible = false;
        }

        protected void GVcategoriasxVariable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridCategoria = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdUpdateCategoria();
                    dvGridCategoriasxVariable.Visible = false;
                    BodyFormVCC.Visible = true;
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    break;
                case "Desasociar":
                    try
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la asociacion de la variable?";
                        mpeMsgBoxOkNo.Show();
                    }
                    catch (Exception ex)
                    {
                        omb.ShowMessage("Error " + ex.Message, 2, "Atención");
                    }
                    break;
            }
        }
        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            string strMessage = string.Empty;
            GridViewRow row = GVcategoriasxVariable.Rows[RowGridCategoria];
            int IdER = Convert.ToInt32(row.Cells[0].Text);
            clsDALParametrizacionAsignarCategoriaVariable cAsignar = new clsDALParametrizacionAsignarCategoriaVariable();
            bool booResult = false;
            try
            {
                booResult = cAsignar.mtdDesasociarVariable(IdER, ref strMessage);

                if (string.IsNullOrEmpty(strMessage))
                {
                    
                    omb.ShowMessage("Desasociación de la Variable correctamente", 3, "Atención");
                    mtdStardCategorias();
                }
                else
                    omb.ShowMessage(strMessage, 2, "Atención");
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error " + ex.Message, 2, "Atención");
            }

        }
        public void mtdUpdateCategoria()
        {
            GridViewRow row = GVcategoriasxVariable.Rows[RowGridCategoria];
            var colsNoVisible = GVcategoriasxVariable.DataKeys[RowGridCategoria].Values;
            string strErrMsg = string.Empty;
            mtdLoadCategorias(ref strErrMsg);
            Session["IdCategoriaVariable"] = row.Cells[0].Text;
            txtId.Text = row.Cells[0].Text;
            //ddlCategorias.SelectedValue = colsNoVisible[3].ToString();
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (!mtdActualizarCategoriaxVariablesControl(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }
        public bool mtdActualizarCategoriaxVariablesControl(ref string strErrMsg)
        {
            bool booResult = false;
            clsDTOParametrizacionAsignarCategoriaVariable objVariable = new clsDTOParametrizacionAsignarCategoriaVariable();
            clsBLLParametrizacionAsignacionCategoriaVariable cVariable = new clsBLLParametrizacionAsignacionCategoriaVariable();
            objVariable.intIdVariableCategoria = Convert.ToInt32(Session["IdCategoriaVariable"].ToString());
            objVariable.intIdCategoria = Convert.ToInt32(ddlCategorias.SelectedValue);
            booResult = cVariable.mtdActualizarVariableCalificacion(objVariable, ref strErrMsg);
            if (booResult == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFieldsCategorias();
                mtdStardCategorias();
            }
            return booResult;
        }

        protected void GVvariablesCalificacionControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexVCC = e.NewPageIndex;
            GVvariablesCalificacionControl.PageIndex = PagIndexVCC;
            GVvariablesCalificacionControl.DataBind();
            string strErrMsg = "";
            mtdLoadVariablesValificacionControl(ref strErrMsg);
        }
    }
}