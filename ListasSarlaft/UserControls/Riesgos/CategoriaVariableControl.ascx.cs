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
    public partial class CategoriaVariableControl : System.Web.UI.UserControl
    {
        string IdFormulario = "5025";
        cCuenta cCuenta = new cCuenta();
        clsDALVariableCalificacionControl cVariableCalificacionControl = new clsDALVariableCalificacionControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.GVcategoriasVariablesControl);
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
        private DataTable infoGridCVC;
        private int rowGridCVC;
        private int pagIndexCVC;

        private DataTable InfoGridCVC
        {
            get
            {
                infoGridCVC = (DataTable)ViewState["infoGridCVC"];
                return infoGridCVC;
            }
            set
            {
                infoGridCVC = value;
                ViewState["infoGridCVC"] = infoGridCVC;
            }
        }

        private int RowGridCVC
        {
            get
            {
                rowGridCVC = (int)ViewState["rowGridCVC"];
                return rowGridCVC;
            }
            set
            {
                rowGridCVC = value;
                ViewState["rowGridCVC"] = rowGridCVC;
            }
        }

        private int PagIndexCVC
        {
            get
            {
                pagIndexCVC = (int)ViewState["pagIndexCVC"];
                return pagIndexCVC;
            }
            set
            {
                pagIndexCVC = value;
                ViewState["pagIndexCVC"] = pagIndexCVC;
            }
        }

        #endregion
        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndexCVC = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            if (!mtdLoadCategoriaVariablesControl(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");

        }
        protected void mtdResetFields()
        {
            BodyFormCVC.Visible = false;
            BodyGridCVC.Visible = true;
            txtId.Text = string.Empty;
            txtDescripcionCategoria.Text = string.Empty;
            txtPesoCategoria.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;
            ddlVariable.ClearSelection();
        }
        private bool mtdInsertarCategoriaVariableControl(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            bool validaLimite = false;
            clsDTOCategoriasVariableControl objCategoria = new clsDTOCategoriasVariableControl();
            clsBLLCategoriaVariableControl cCategoria = new clsBLLCategoriaVariableControl();

            #endregion
            objCategoria.intIdCategoriaVariableControl = 0;
            objCategoria.strDescripcionCategoria = Sanitizer.GetSafeHtmlFragment(txtDescripcionCategoria.Text);
            objCategoria.intPesoCategoria = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtPesoCategoria.Text));
            objCategoria.dtFechaRegistro = DateTime.Now;
            objCategoria.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objCategoria.IdVariable = Convert.ToInt32(ddlVariable.SelectedValue);

            int valorPeso = cCategoria.mtdCantidadTotalPeso(ref strErrMsg);

            /*valorPeso = valorPeso + objCategoria.intPesoCategoria;
            if (valorPeso <= 100)
            {*/
            booResult = cCategoria.mtdInsertarCategoriaVariableControl(objCategoria, ref strErrMsg);
            #region Vars
            bool booResultVariable = false;
            //clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
            List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
            clsBLLCalculoEfectividadControl cCalculo = new clsBLLCalculoEfectividadControl();
            List<clsDTOCalificacionControl> lstCalificacion = new List<clsDTOCalificacionControl>();
            int ValorPorcentaje = 0;
            int PesoMaxCategoria = 0;
            int PesoMinCategoria = 0;
            double valorMaximo = 0;
            double valorMinimo = 0;
            double sumValorMax = 0;
            double sumValorMin = 0;
            double valorDiferencia = 0;
            double LimiteInf = 0;
            #endregion Vars
            lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);
            lstCalificacion = cCalculo.mtdConsultarCuadroCalificacion(ref lstCalificacion, ref strErrMsg);
                if (lstVariables != null)
                {
                    foreach(clsDTOVariableCalificacionControl objVariable in lstVariables)
                    {
                        ValorPorcentaje = cCalculo.mtdConsultarValorProcentaje(ref strErrMsg, objVariable.strDescripcionVariable);
                        PesoMaxCategoria = cCalculo.mtdConsultarMaxPeso(ref strErrMsg, objVariable.intIdCalificacionControl);
                        PesoMinCategoria = cCalculo.mtdConsultarMinPeso(ref strErrMsg, objVariable.intIdCalificacionControl);
                        valorMaximo = (ValorPorcentaje * PesoMaxCategoria) / 100;
                        valorMinimo = (ValorPorcentaje * PesoMinCategoria) / 100;
                    sumValorMax = sumValorMax + valorMaximo;
                    sumValorMin = sumValorMin + valorMinimo;
                    }
                valorDiferencia = (sumValorMax - sumValorMin)/4;
                if(lstCalificacion != null)
            {
                    foreach(clsDTOCalificacionControl objCalificacion in lstCalificacion)
                    {
                         
                        if (objCalificacion.strNombreEscala == "Excelente")
                        {
                            objCalificacion.intLimiteSuperior = sumValorMax;
                            LimiteInf = sumValorMax - valorDiferencia;
                            objCalificacion.intLimiteInferior = LimiteInf;
                        }else
                        {
                            objCalificacion.intLimiteSuperior = LimiteInf;
                            LimiteInf = LimiteInf - valorDiferencia;
                            objCalificacion.intLimiteInferior = LimiteInf;
                        }
                        //cCalculo.mtdUpdateCuadorCalificacion(objCalificacion, ref strErrMsg);
                    }
            }
                }
                if (booResult == true)
                {
                    strErrMsg = "Categoría de la Variable de Calificación del Control registrada exitosamente";
                }
                else
                {
                    strErrMsg = "Error al registrar la Categoría de la Variable de Calificación del Control";
                }
            /*}else
            {
                strErrMsg = "Error: La sumatoria de los pesos de las Categorias es mayor de 100";
            }*/
            return booResult;
        }
        private bool mtdLoadCategoriaVariablesControl(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOCategoriasVariableControl objCatergoria = new clsDTOCategoriasVariableControl();
            List<clsDTOCategoriasVariableControl> lstCategoria = new List<clsDTOCategoriasVariableControl>();
            clsBLLCategoriaVariableControl cCategoria = new clsBLLCategoriaVariableControl();
            #endregion Vars
            lstCategoria = cCategoria.mtdConsultarCategoriaVariablesControl(ref lstCategoria, ref strErrMsg);
            if (lstCategoria != null)
            {
                mtdLoadCategoriaVariablesControl();
                mtdLoadCategoriaVariablesControl(lstCategoria);
                GVcategoriasVariablesControl.DataSource = lstCategoria;
                GVcategoriasVariablesControl.PageIndex = pagIndexCVC;
                GVcategoriasVariablesControl.DataBind();
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
        private void mtdLoadCategoriaVariablesControl()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdCategoriaVariableControl", typeof(string));
            grid.Columns.Add("strDescripcionCategoria", typeof(string)); 
            grid.Columns.Add("intPesoCategoria", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));
            grid.Columns.Add("booActivo", typeof(string));
            grid.Columns.Add("IdVariable", typeof(string));

            GVcategoriasVariablesControl.DataSource = grid;
            GVcategoriasVariablesControl.DataBind();
            InfoGridCVC = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstCategoria">Lista con las Categorias</param>
        private void mtdLoadCategoriaVariablesControl(List<clsDTOCategoriasVariableControl> lstCategoria)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsDTOCategoriasVariableControl objCategoria in lstCategoria)
            {

                InfoGridCVC.Rows.Add(new Object[] {
                    objCategoria.intIdCategoriaVariableControl.ToString().Trim(),
                    objCategoria.strDescripcionCategoria.ToString().Trim(),
                    objCategoria.intPesoCategoria.ToString().Trim(),
                    objCategoria.intIdUsuario.ToString().Trim(),
                    objCategoria.dtFechaRegistro.ToString().Trim(),
                    objCategoria.strUsuario.ToString().Trim(),
                    objCategoria.booActivo.ToString().Trim(),
                    objCategoria.IdVariable.ToString().Trim()
                    });
            }
        }
        protected void mtdShowUpdate(int Rowgrid)
        {
            GridViewRow row = GVcategoriasVariablesControl.Rows[Rowgrid];
            var colsNoVisible = GVcategoriasVariablesControl.DataKeys[Rowgrid].Values;
            txtId.Text = row.Cells[0].Text;
            txtDescripcionCategoria.Text = ((Label)row.FindControl("strDescripcionCategoria")).Text;
            txtPesoCategoria.Text = row.Cells[2].Text;
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();

            DataTable dt = cVariableCalificacionControl.ObtenerVariables(0);
            if (dt != null && dt.Rows.Count > 0)
            {
                //Llenar el DropDownList de categoria
                ddlVariable.DataSource = dt;
                ddlVariable.DataTextField = "DescripcionVariable";
                ddlVariable.DataValueField = "IdVariableCalificacionControl";
                ddlVariable.DataBind();
                ddlVariable.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                ddlVariable.SelectedValue = ((Label)row.FindControl("lblIdVariable")).Text;
            }

        }
        private void mtdActivar(int Rowgrid)
        {

            string strEstado = string.Empty;
            int booEstado = Convert.ToInt32(InfoGridCVC.Rows[RowGridCVC][6].ToString().Trim());
            GridViewRow row = GVcategoriasVariablesControl.Rows[Rowgrid];
            Session["IdCategoriaVariable"] = row.Cells[0].Text;
            if (booEstado == 1)
            {
                Session["activo"] = 0;
                strEstado = "inactivar";
            }
            else
            {
                Session["activo"] = 1;
                strEstado = "activar";
            }
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
            else
            {
                lblMsgBox.Text = string.Format("Desea {0} la categoría de calificación control?", strEstado);
                mpeMsgBox.Show();
            }
        }
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;

            clsDTOCategoriasVariableControl objCategoria = new clsDTOCategoriasVariableControl();
            objCategoria.booActivo = Convert.ToInt32(Session["activo"].ToString());
            objCategoria.intIdCategoriaVariableControl = Convert.ToInt32(Session["IdCategoriaVariable"].ToString());
            clsBLLCategoriaVariableControl cCategoria = new clsBLLCategoriaVariableControl();

            booResult = cCategoria.mtdActivarCategariaVariableControl(objCategoria, ref strErrMsg);
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
                strErrMsg = "la categoría de calificación del control se " + strEstado + " exitosamente";
            }

                return booResult;
        }
        private bool mtdUpdateCategoriaVariableControl(ref string strErrMsg)
        {
            bool booResult = false;
            clsDTOCategoriasVariableControl objCategoria = new clsDTOCategoriasVariableControl();
            objCategoria.intIdCategoriaVariableControl = Convert.ToInt32(txtId.Text);
            objCategoria.strDescripcionCategoria = Sanitizer.GetSafeHtmlFragment(txtDescripcionCategoria.Text);
            objCategoria.intPesoCategoria = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtPesoCategoria.Text));
            objCategoria.IdVariable = Convert.ToInt32(ddlVariable.SelectedValue);
            clsBLLCategoriaVariableControl cCategoria = new clsBLLCategoriaVariableControl();
            int valorPeso = cCategoria.mtdCantidadTotalPeso(ref strErrMsg);
            valorPeso = valorPeso + objCategoria.intPesoCategoria;
            /*if (valorPeso <= 100)
            {*/
                booResult = cCategoria.mtdUpdateCategoriaVariableControl(objCategoria, ref strErrMsg);
            #region Vars
            bool booResultVariable = false;
            //clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
            List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
            clsBLLCalculoEfectividadControl cCalculo = new clsBLLCalculoEfectividadControl();
            List<clsDTOCalificacionControl> lstCalificacion = new List<clsDTOCalificacionControl>();
            int ValorPorcentaje = 0;
            int PesoMaxCategoria = 0;
            int PesoMinCategoria = 0;
            double valorMaximo = 0;
            double valorMinimo = 0;
            double sumValorMax = 0;
            double sumValorMin = 0;
            double valorDiferencia = 0;
            double LimiteInf = 0;
            #endregion Vars
            lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);
            lstCalificacion = cCalculo.mtdConsultarCuadroCalificacion(ref lstCalificacion, ref strErrMsg);
            if (lstVariables != null)
            {
                foreach (clsDTOVariableCalificacionControl objVariable in lstVariables)
                {
                    ValorPorcentaje = cCalculo.mtdConsultarValorProcentaje(ref strErrMsg, objVariable.strDescripcionVariable);
                    PesoMaxCategoria = cCalculo.mtdConsultarMaxPeso(ref strErrMsg, objVariable.intIdCalificacionControl);
                    PesoMinCategoria = cCalculo.mtdConsultarMinPeso(ref strErrMsg, objVariable.intIdCalificacionControl);
                    valorMaximo = (ValorPorcentaje * PesoMaxCategoria) / 100;
                    valorMinimo = (ValorPorcentaje * PesoMinCategoria) / 100;
                    sumValorMax = sumValorMax + valorMaximo;
                    sumValorMin = sumValorMin + valorMinimo;
                }
                valorDiferencia = (sumValorMax - sumValorMin) / 4;
                if (lstCalificacion != null)
                {
                    foreach (clsDTOCalificacionControl objCalificacion in lstCalificacion)
                    {

                        if (objCalificacion.strNombreEscala == "Excelente")
                        {
                            objCalificacion.intLimiteSuperior = sumValorMax;
                            LimiteInf = sumValorMax - valorDiferencia;
                            objCalificacion.intLimiteInferior = LimiteInf;
                        }
                        else
                        {
                            objCalificacion.intLimiteSuperior = LimiteInf;
                            LimiteInf = LimiteInf - valorDiferencia;
                            objCalificacion.intLimiteInferior = LimiteInf;
                        }
                        //cCalculo.mtdUpdateCuadorCalificacion(objCalificacion, ref strErrMsg);
                    }
                }
            }
                if (booResult == true)
                    strErrMsg = "Categoria de la Variable de Calificación del Control actualizada  exitosamente";
                else
                    strErrMsg = "Error al actualizar Categoria de la Variable de Calificación del Control";
            /*}else
            {
                strErrMsg = "Error: La sumatoria de los pesos de las Categorias es mayor de 100";
            }*/
            return booResult;
        }
        #endregion Metodos
        #region Buttons
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            BodyGridCVC.Visible = false;
            BodyFormCVC.Visible = true;
            DataTable dt = cVariableCalificacionControl.ObtenerVariables(0);
            if (dt != null && dt.Rows.Count > 0)
            {
                //Llenar el DropDownList de categoria
                ddlVariable.DataSource = dt;
                ddlVariable.DataTextField = "DescripcionVariable";
                ddlVariable.DataValueField = "IdVariableCalificacionControl";
                ddlVariable.DataBind();
                ddlVariable.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            }
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertarCategoriaVariableControl(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
            }
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdUpdateCategoriaVariableControl(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
            else
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
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
                omb.ShowMessage("Error al inactivar la Categoria de la Variable de la Calificación del Control.<br/>Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }
        #endregion Buttons
        protected void GVcategoriasVariablesControl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridCVC = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGridCVC);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    BodyFormCVC.Visible = true;
                    BodyGridCVC.Visible = false;
                    break;
                case "Activar":
                    mtdActivar(RowGridCVC);
                    break;
            }
        }

        protected void GVcategoriasVariablesControl_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < GVcategoriasVariablesControl.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVcategoriasVariablesControl.Rows[rowIndex];
                int booActivo = 0;
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 8)
                    {
                        booActivo = Convert.ToInt32(((Label)row.FindControl("booActivo")).Text);
                        ImageButton ImgBnt = ((ImageButton)row.FindControl("ImgBtnInact"));
                        if (booActivo == 0)
                            ImgBnt.ImageUrl = "~/Imagenes/Icons/switch-off-icon.png";
                    }
                }
            }
        }

        protected void GVcategoriasVariablesControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexCVC = e.NewPageIndex;
            GVcategoriasVariablesControl.PageIndex = PagIndexCVC;
            GVcategoriasVariablesControl.DataBind();
            string strErrMsg = "";
            mtdLoadCategoriaVariablesControl(ref strErrMsg);
        }
    }
}