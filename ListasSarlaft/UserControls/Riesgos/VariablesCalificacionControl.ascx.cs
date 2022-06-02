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
    public partial class VariablesCalificacionControl : System.Web.UI.UserControl
    {
        string IdFormulario = "5024";
        cCuenta cCuenta = new cCuenta();
        clsDALVariableCalificacionControl cVariableCalificacionControl = new clsDALVariableCalificacionControl();

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

            txtId.Text = string.Empty;
            txtDescripcionVariable.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtPesoVariable.Text = string.Empty;
        }
        private bool mtdInsertarVariableCalificacionControl(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            bool validaLimite = false;
            clsDTOVariableCalificacionControl objVariableCalificacion = new clsDTOVariableCalificacionControl();
            clsBLLVariableCalificacionControl cVariableCalificacion = new clsBLLVariableCalificacionControl();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objVariableCalificacion.intIdCalificacionControl = 0;
            objVariableCalificacion.strDescripcionVariable = Sanitizer.GetSafeHtmlFragment(txtDescripcionVariable.Text);
            objVariableCalificacion.dtFechaRegistro = DateTime.Now;
            objVariableCalificacion.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objVariableCalificacion.FlPesoVariable = Convert.ToDouble(txtPesoVariable.Text);

            // Validacion de los pesos
            double totalPesos = 0;
            double valorActual = 0;
            // Se instancia la GridViewRow que se esta tratando de actualizar
            IEnumerable<GridViewRow> rows = GVvariablesCalificacionControl.Rows.Cast<GridViewRow>();
            foreach (GridViewRow Row in GVvariablesCalificacionControl.Rows)
            {
                valorActual = Row.Cells[5].Equals(DBNull.Value) ? 0 : Convert.ToDouble(Row.Cells[5].Text);
                totalPesos = totalPesos + valorActual;
            }
            int valorIngresado  = Convert.ToInt32(txtPesoVariable.Text);
            // Se añade el valor nuevo al total
            totalPesos = totalPesos + valorIngresado;
            if (totalPesos > 100)
            {
                strErrMsg = "La sumatoria de los pesos es mayor que 100, modifique algún peso y vuelva a intentarlo";
                return false;
            }

            int valortotalregistros = cVariableCalificacion.mtdValorMaximo(ref strErrMsg);
            if (valortotalregistros < 15)
            {
                
                    booResult = cVariableCalificacion.mtdInsertarVariableCalificacion(objVariableCalificacion, ref strErrMsg);
                    if (booResult == true)
                    {
                        strErrMsg = "Variable de Calificación del Control registrada exitosamente";
                    }
                    else
                    {
                        strErrMsg = "Error al registrar la Variable de Calificación del Control";
                    }
            }else
                strErrMsg = "Error: ya hay 15 variables registradas";
            return booResult;
        }
        private bool mtdLoadVariablesValificacionControl(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
            List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
            #endregion Vars
            lstVariables = cVariable.mtdConsultarVariablesCalificacionControl(ref lstVariables, ref strErrMsg);

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
            grid.Columns.Add("FlPesoVariable", typeof(string));
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
                    objVariable.booActivo.ToString().Trim(),
                    objVariable.FlPesoVariable.ToString().Trim()
                    });
            }
        }
        protected void mtdShowUpdate(int Rowgrid)
        {
            GridViewRow row = GVvariablesCalificacionControl.Rows[Rowgrid];
            var colsNoVisible = GVvariablesCalificacionControl.DataKeys[Rowgrid].Values;
            DataTable dt = cVariableCalificacionControl.ObtenerVariables(Convert.ToInt32(row.Cells[0].Text));
            if (dt != null && dt.Rows.Count> 0)
            {
                foreach (DataRow Row in dt.Rows)
                {
                    txtId.Text = Row["IdVariableCalificacionControl"].ToString();
                    txtDescripcionVariable.Text = Row["DescripcionVariable"].ToString();
                    txtFecha.Text = Row["FechaCreacion"].ToString();
                    txtPesoVariable.Text = Row["PesoVariable"].ToString();
                    tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
                }
            }
            
            //txtId.Text = row.Cells[0].Text;
            //txtDescripcionVariable.Text = ((Label)row.FindControl("strDescripcionVariable")).Text;
            //tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            //txtFecha.Text = colsNoVisible[1].ToString();
            
        }
        private bool mtdUpdateVariableCalificacionControl(ref string strErrMsg)
        {

            // Validacion de los pesos
            double totalPesos = 0;
            double valorActual = 0;
            int indexRow = Convert.ToInt32(txtId.Text);
            string valorPesoAnterior = string.Empty;
            // Se instancia la GridViewRow que se esta tratando de actualizar
            IEnumerable<GridViewRow> rows = GVvariablesCalificacionControl.Rows.Cast<GridViewRow>()
    .Where(row => row.Cells[0].Text == indexRow.ToString());
            foreach (GridViewRow row in rows)
            {
                valorPesoAnterior = row.Cells[5].Text;
                row.Cells[5].Text = txtPesoVariable.Text;
            }
            foreach (GridViewRow Row in GVvariablesCalificacionControl.Rows)
            {
                valorActual = Row.Cells[5].Equals(DBNull.Value) ? 0 : Convert.ToDouble(Row.Cells[5].Text);
                totalPesos = totalPesos + valorActual;
            }
            if (totalPesos > 100)
            {
                strErrMsg = "La sumatoria de los pesos es mayor que 100, modifique algún peso y vuelva a intentarlo";
                //Si la sumatoria es mayor que 100 se coloca nuevamente el valor inicial
                foreach (GridViewRow row in rows)
                {
                    row.Cells[5].Text = valorPesoAnterior;
                }
                return false;
            }

            bool booResult = false;
            clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
            objVariable.intIdCalificacionControl = Convert.ToInt32(txtId.Text);
            objVariable.strDescripcionVariable = Sanitizer.GetSafeHtmlFragment(txtDescripcionVariable.Text);
            objVariable.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objVariable.FlPesoVariable = Convert.ToDouble(txtPesoVariable.Text);

            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl(); 

            booResult = cVariable.mtdUpdateVariableCalificacionControl(objVariable, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Variable de Calificación del Control actualizada  exitosamente";
            else
                strErrMsg = "Error al actualizar la Variable de Calificación del Control";
            return booResult;
        }
        private void mtdActivar(int Rowgrid)
        {
            clsDALVariableCalificacionControl dalVariable = new clsDALVariableCalificacionControl();
                string strEstado = string.Empty;
                 //Convert.ToInt32(InfoGridVCC.Rows[RowGridVCC][5].ToString().Trim());
            GridViewRow row = GVvariablesCalificacionControl.Rows[Rowgrid];
            Session["IdCalificacionControl"] = row.Cells[0].Text;
            int booEstado = Convert.ToInt32(((Label)row.FindControl("booActivo")).Text);
            string nombreVariable = ((Label)row.FindControl("strDescripcionVariable")).Text;
            string strErrMsg = string.Empty;
            int cantControl = dalVariable.mtdCantVariablexControl(ref strErrMsg, nombreVariable);
            
                if (booEstado == 1)
                {
                if (cantControl == 0)
                {
                    Session["activo"] = 0;
                    strEstado = "inactivar";

                    lblMsgBox.Text = string.Format("Desea {0} la variable de calificación control?", strEstado);
                    mpeMsgBox.Show();
                }
                else
                {
                    omb.ShowMessage("La variable esta asignada a " + cantControl + " controles, no se puede inhabilitar", 2, "Atención");
                }
            }
                else
                {
                    clsBLLVariableCalificacionControl cVariableCalificacion = new clsBLLVariableCalificacionControl();
                    int valortotalregistros = cVariableCalificacion.mtdValorMaximo(ref strErrMsg);
                    if (valortotalregistros < 15)
                    {
                        Session["activo"] = 1;
                        strEstado = "activar";

                    lblMsgBox.Text = string.Format("Desea {0} la variable de calificación control?", strEstado);
                    mpeMsgBox.Show();
                }
                    else
                        strErrMsg = "Error: ya hay 15 variables registradas";
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                }

                
            
            
        }
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;

            clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
            objVariable.booActivo = Convert.ToInt32(Session["activo"].ToString());
            objVariable.intIdCalificacionControl = Convert.ToInt32(Session["IdCalificacionControl"].ToString());
            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();

            booResult = cVariable.mtdActivarVariableCalificacionControl(objVariable, ref strErrMsg);
            string strEstado = string.Empty;
            if (booResult == true)
            {
                if (Session["activo"].ToString() == "1")
                {
                    strEstado = "activó";
                }
                else
                {
                    strEstado = "inactivó";
                }
                strErrMsg = "la variable de calificación del control se " + strEstado + " exitosamente";
            }
                

            return booResult;
        }
        #endregion Metodos
        #region Buttons Events
        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertarVariableCalificacionControl(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdUpdateVariableCalificacionControl(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }else
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
        }

        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            BodyGridVCC.Visible = false;
            BodyFormVCC.Visible = true;
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }
        protected void btnModificarEstado_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            mpeMsgBox.Hide();

            try
            {
                if (mtdActualizarEstado(ref strErrMsg) == true)
                {
                    omb.ShowMessage(strErrMsg, 3, "Atención");
                    mtdResetFields();
                    mtdStard();
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al inactivar la Variable de la Calificación del Control.<br/>Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }
        #endregion Buttons Events

        protected void GVvariablesCalificacionControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexVCC = e.NewPageIndex;
            GVvariablesCalificacionControl.PageIndex = PagIndexVCC;
            GVvariablesCalificacionControl.DataBind();
            string strErrMsg = "";
            mtdLoadVariablesValificacionControl(ref strErrMsg);
        }

        protected void GVvariablesCalificacionControl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridVCC = Convert.ToInt16(e.CommandArgument);
            //RowGridVCC = (Convert.ToInt16(GVvariablesCalificacionControl.PageSize) * PagIndexVCC) + Convert.ToInt16(e.CommandArgument);
            int poscition = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGridVCC);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    BodyFormVCC.Visible = true;
                    BodyGridVCC.Visible = false;
                    break;
                case "Activar":
                    mtdActivar(poscition);
                    break;
            }
        }

        protected void GVvariablesCalificacionControl_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < GVvariablesCalificacionControl.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVvariablesCalificacionControl.Rows[rowIndex];
                int booActivo = 0;
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if(cellIndex == 7)
                    {
                        booActivo = Convert.ToInt32(((Label)row.FindControl("booActivo")).Text);
                        ImageButton ImgBnt = ((ImageButton)row.FindControl("ImgBtnInact"));
                        if (booActivo == 0)
                            ImgBnt.ImageUrl = "~/Imagenes/Icons/switch-off-icon.png";
                    }
                }
            }
         }
    }
}