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
using System.Diagnostics;
using System.IO;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class ValorVariableIndicador : System.Web.UI.UserControl
    {
        string IdFormulario = "4018";
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;
        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;

        private DataTable InfoGrid1
        {
            get
            {
                infoGrid1 = (DataTable)ViewState["infoGrid1"];
                return infoGrid1;
            }
            set
            {
                infoGrid1 = value;
                ViewState["infoGrid1"] = infoGrid1;
            }
        }

        private int RowGrid1
        {
            get
            {
                rowGrid1 = (int)ViewState["rowGrid1"];
                return rowGrid1;
            }
            set
            {
                rowGrid1 = value;
                ViewState["rowGrid1"] = rowGrid1;
            }
        }

        private int PagIndex1
        {
            get
            {
                pagIndex1 = (int)ViewState["pagIndex1"];
                return pagIndex1;
            }
            set
            {
                pagIndex1 = value;
                ViewState["pagIndex1"] = pagIndex1;
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
                    // Controla cuando se edita una variable
                    Session["Editar"] = false;
                    mtdInicializarValores();
                    mtdHabilitarCampos(1);
                    if (!mtdCargarDDLs(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
        }

        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();

            if (!mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();

            if (!mtdLoadDDLProceso(ref strErrMsg))
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

            if (!mtdLoadDDLSubproceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlSubproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlProceso.SelectedValue == "0")
            //    rfvSubproceso.Enabled = false;
            //else
            //    rfvSubproceso.Enabled = true;
        }
        #endregion

        #region Gridview
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string path = @"c:\MyTest.txt";
            //StreamWriter sw = File.CreateText(path);
            /*if (!File.Exists(path))
            {
                // Create a file to write to.
                using ()
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                }
            }*/
            /*try
            {*/
            string strErrMsg = string.Empty;
            RowGrid1 = (Convert.ToInt16(GridView1.PageSize) * PagIndex1) + Convert.ToInt16(e.CommandArgument);
            //int year = Convert.ToInt32(Session["intPeridoAnual"].ToString());
            switch (e.CommandName)
            {
                case "Variables":
                    //System.Diagnostics.Debug.WriteLine("Antes de entrar mtdMostrarVariables");
                    //Debug.WriteLine("Antes de entrar mtdMostrarVariables");
                    //sw.WriteLine("Antes de entrar mtdMostrarVariables");
                    tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
                    Session["IdPeriodicidad"] = InfoGrid1.Rows[RowGrid1][3].ToString().Trim();
                    mpeMsgBox.Show();
                    //if (!mtdMostrarVariables(ref strErrMsg))
                    //    omb.ShowMessage(strErrMsg, 1, "Atención");
                    //else
                    //{
                    //    mtdHabilitarCampos(3);
                    //    tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
                    //    tbxIdPeriodicidad.Text = InfoGrid1.Rows[RowGrid1][3].ToString().Trim();
                    //}
                    break;
            }
            /*}
            catch (Exception ex)
            {
                sw.WriteLine("Registro de trasa", ex.ToString() + ex.Message + ex.StackTrace);
                //File.AppendText("ElFuckingLog.log", ex.ToString() + ex.Message + ex.StackTrace);
                //Debug.WriteLine(ex.ToString() + ex.Message + ex.StackTrace);
            }*/
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;
            RowGrid2 = (Convert.ToInt16(GridView3.PageSize) * PagIndex2) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Asignar":
                    if (!mtdAsignarVariables(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    break;
                case "Editar":
                    EditarVariable();
                    break;
            }
        }
        #endregion

        #region Buttons
        protected void btnBuscarIndicador_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            //System.Diagnostics.Debug.WriteLine("SomeText");
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                mtdHabilitarCampos(2);
                if (mtdCargarIndicador(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
        }

        protected void btnRegresarFiltros_Click(object sender, ImageClickEventArgs e)
        {
            mtdHabilitarCampos(1);
        }

        protected void btnGuardarVar_Click(object sender, ImageClickEventArgs e)
        {

            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarDetalle(ref strErrMsg))
                {
                    if ((bool)Session["Editar"])
                        omb.ShowMessage("Valor actualizado exitosamente.", 3, "Atención");
                    else
                        omb.ShowMessage("Valor ingresado exitosamente.", 3, "Atención");

                    mtdHabilitarCampos(3);

                    clsIndicador objIndicador = new clsIndicador(
                        Convert.ToInt32(tbxIdIndicador.Text.Trim()),
                        string.Empty, string.Empty, 0, 0, true, 0, 0, 0, string.Empty);

                    //mtdCargarVariable(objIndicador, ref strErrMsg);
                    int periodoAnual = Convert.ToInt32(Session["intPeridoAnual"].ToString()); 
                    
                    if (!mtdMostrarVariablesByYear(ref strErrMsg, periodoAnual))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    else
                    {
                        mtdHabilitarCampos(3);
                        tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
                        tbxIdPeriodicidad.Text = InfoGrid1.Rows[RowGrid1][3].ToString().Trim();
                        GridView3.Visible = true;
                    }

                    btnInsertarNuevo.Visible = true;
                    mtdLimpiarCamposVariable(2);

                    Session["Editar"] = false;
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al registrar el Valor de la variable.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnActualizarVar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {


                    if (mtdActualizarDetalle(ref strErrMsg))
                    {
                        omb.ShowMessage("Indicador modificado exitosamente.", 3, "Atención");

                        //mtdHabilitarCampos(3);
                        //clsIndicador objIndicador = new clsIndicador(
                        //    Convert.ToInt32(tbxIdIndicador.Text.Trim()),
                        //    string.Empty, 0, 0, true, 0, 0, 0, string.Empty);
                        //mtdCargarVariable(objIndicador, ref strErrMsg);

                        //mtdLimpiarCamposVariable(2);
                        

                        if (!mtdMostrarVariablesByYear(ref strErrMsg, Convert.ToInt32(Session["intPeridoAnual"])))
                            omb.ShowMessage(strErrMsg, 1, "Atención");
                        else
                        {
                            mtdHabilitarCampos(3);
                            tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
                            tbxIdPeriodicidad.Text = InfoGrid1.Rows[RowGrid1][3].ToString().Trim();
                        }
                        btnInsertarNuevo.Visible = true;
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el .<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarVar_Click(object sender, ImageClickEventArgs e)
        {
            //mtdHabilitarCampos(3);
            string strErrMsg = string.Empty;
            
            if (!mtdMostrarVariablesByYear(ref strErrMsg, Convert.ToInt32(Session["intPeridoAnual"].ToString())))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            else
            {
                mtdHabilitarCampos(3);
                tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
                tbxIdPeriodicidad.Text = InfoGrid1.Rows[RowGrid1][3].ToString().Trim();
                GridView3.Visible = true;
                btnInsertarNuevo.Visible = true;
            }
        }

        protected void btnRegresarIndicador_Click(object sender, ImageClickEventArgs e)
        {
            mtdHabilitarCampos(2);
        }
        #endregion

        #region Metodos

        private void EditarVariable()
        {
            try
            {
                string strErrMsg = string.Empty;
                List<clsVariable> lstVariable = new List<clsVariable>();
                clsVariableBLL cVariable = new clsVariableBLL();

                lstVariable = cVariable.mtdConsultarVariable(ref strErrMsg);

                ddlVariable.DataSource = lstVariable;
                ddlVariable.DataTextField = "strDescripcion";
                ddlVariable.DataValueField = "intId";
                ddlVariable.DataBind();
                mtdLimpiarCamposVariable(1);
                
                tbxId.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
                ddlVariable.SelectedValue = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
                Session["IdDetalleVariable"] = InfoGrid2.Rows[RowGrid2][6].ToString().Trim();
                tbxIdIndicador.Text = InfoGrid2.Rows[RowGrid2][11].ToString().Trim();
                tbxDescVariable.Text = InfoGrid2.Rows[RowGrid2][1].ToString().Trim();
                tbxVlrVariable.Text = InfoGrid2.Rows[RowGrid2][7].ToString().Trim().Split(',').FirstOrDefault();
                txtperiodoanual.Text = InfoGrid2.Rows[RowGrid2][10].ToString().Trim();
                mtdLoadDDLPeriodo(Convert.ToInt32(tbxIdPeriodicidad.Text.Trim()), ref strErrMsg);
                ddlDetPeriodo.SelectedValue = InfoGrid2.Rows[RowGrid2][8].ToString().Trim();
                mtdHabilitarCampos(5);
                Session["Editar"] = true;
                mtdCargarPeriodoAnual();
                btnActualizarVar.Visible = true;
                btnGuardarVar.Visible = false;
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al editar {ex.Message}", 1, "Atención");
            }

        }

        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
        }

        private void mtdLimpiarCamposVariable(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1: // todo
                    tbxId.Text = string.Empty;
                    //tbxIdIndicador.Text = string.Empty;
                    //tbxIdPeriodicidad.Text = string.Empty;
                    tbxIdDetalle.Text = string.Empty;
                    tbxDescVariable.Text = string.Empty;
                    tbxVlrVariable.Text = string.Empty;
                    ddlDetPeriodo.SelectedValue = "0";
                    ddlPeriodoAnual.Items.Clear();
                    break;
                case 2:
                    tbxId.Text = string.Empty;
                    //tbxIdIndicador.Text = string.Empty;
                    tbxIdDetalle.Text = string.Empty;
                    tbxDescVariable.Text = string.Empty;
                    tbxVlrVariable.Text = string.Empty;
                    ddlDetPeriodo.SelectedValue = "0";
                    ddlPeriodoAnual.Items.Clear();
                    txtperiodoanual.Text = string.Empty;
                    break;
            }
        }

        private void mtdHabilitarCampos(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1: // Ver Filtros
                    #region
                    FilaProcesos.Visible = true;
                    FilaBotonBuscar.Visible = true;

                    FilaTituloIndicador.Visible = false;
                    FilaGridIndicador.Visible = false;
                    BotonRegresarFiltros.Visible = false;

                    FilaTituloVariables.Visible = false;
                    FilaGridVariables.Visible = false;
                    FilaTituloAsignar.Visible = false;
                    FilaDetalleAsignar.Visible = false;
                    BotonRegresarIndicador.Visible = false;
                    #endregion
                    break;
                case 2: // Ver Grid Indicadores
                    #region
                    FilaProcesos.Visible = false;
                    FilaBotonBuscar.Visible = false;

                    FilaTituloIndicador.Visible = true;
                    FilaGridIndicador.Visible = true;
                    BotonRegresarFiltros.Visible = true;

                    FilaTituloVariables.Visible = false;
                    FilaGridVariables.Visible = false;
                    FilaTituloAsignar.Visible = false;
                    FilaDetalleAsignar.Visible = false;
                    BotonRegresarIndicador.Visible = false;

                    #endregion
                    break;
                case 3: // Ver grid Variables
                    #region
                    FilaProcesos.Visible = false;
                    FilaBotonBuscar.Visible = false;

                    FilaTituloIndicador.Visible = false;
                    FilaGridIndicador.Visible = false;
                    BotonRegresarFiltros.Visible = false;

                    FilaTituloVariables.Visible = true;
                    FilaGridVariables.Visible = true;
                    FilaTituloAsignar.Visible = false;
                    FilaDetalleAsignar.Visible = false;
                    BotonRegresarIndicador.Visible = true;

                    #endregion
                    break;
                case 4: // Nuevo Detalle
                    #region
                    FilaProcesos.Visible = false;
                    FilaBotonBuscar.Visible = false;

                    FilaTituloIndicador.Visible = false;
                    FilaGridIndicador.Visible = false;
                    BotonRegresarFiltros.Visible = false;

                    FilaTituloVariables.Visible = false;
                    FilaGridVariables.Visible = false;
                    FilaTituloAsignar.Visible = true;
                    FilaDetalleAsignar.Visible = true;

                    btnGuardarVar.Visible = true;
                    //btnActualizarVar.Visible = false;
                    btnCancelarVar.Visible = true;

                    BotonRegresarIndicador.Visible = true;

                    #endregion
                    break;
                case 5: // Modificar Detalle
                    #region
                    FilaProcesos.Visible = false;
                    FilaBotonBuscar.Visible = false;

                    FilaTituloIndicador.Visible = false;
                    FilaGridIndicador.Visible = false;
                    BotonRegresarFiltros.Visible = false;

                    FilaTituloVariables.Visible = false;
                    FilaGridVariables.Visible = false;
                    FilaTituloAsignar.Visible = true;
                    FilaDetalleAsignar.Visible = true;

                    btnGuardarVar.Visible = true;
                    //btnActualizarVar.Visible = true;
                    btnCancelarVar.Visible = true;

                    BotonRegresarIndicador.Visible = true;

                    #endregion
                    break;
            }
        }

        #region Cargas
        #region DDLs
        private bool mtdCargarDDLs(ref string strErrMsg)
        {
            bool booResult = false;

            booResult = mtdLoadDDLCadenaValor(ref strErrMsg);
            //if (booResult)
            //    booResult = mtdLoadDDLMacroProceso(ref strErrMsg);

            return booResult;
        }

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
                        booResult = true;
                    }
                    else
                        booResult = false;
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

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
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

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
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

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
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Subprocesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

        private bool mtdLoadDDLPeriodo(int intIdPeriodo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsPeriodicidad objPeriodicidad = new clsPeriodicidad(intIdPeriodo, string.Empty, 0, string.Empty);
            List<clsDetallePeriodo> lstPeriodo = new List<clsDetallePeriodo>();
            clsPeriodicidadBLL cPeriodo = new clsPeriodicidadBLL();
            #endregion Vars

            try
            {
                booResult = cPeriodo.mtdConsultarDetallePeriodo(objPeriodicidad, ref lstPeriodo, ref strErrMsg);
                ddlDetPeriodo.Items.Clear();
                ddlDetPeriodo.Items.Insert(0, new ListItem("", "0"));

                if (booResult)
                    if (lstPeriodo != null)
                    {
                        int intCounter = 1;

                        foreach (clsDetallePeriodo objPeriodo in lstPeriodo)
                        {
                            ddlDetPeriodo.Items.Insert(intCounter, new ListItem(objPeriodo.strNombre, objPeriodo.intId.ToString()));
                            intCounter++;
                        }
                        booResult = true;
                    }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta del periodo. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
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

            if (!string.IsNullOrEmpty(strErrMsg))
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
            grid.Columns.Add("intIdPeriodicidad", typeof(string));
            grid.Columns.Add("strNombrePeriodicidad", typeof(string));
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
            InfoGrid1 = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridIndicador(ref string strErrMsg)
        {
            bool booResult = false;
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsIndicadorBLL cIndicador = new clsIndicadorBLL();

            #region Proceso del indicador
            booResult = mtdValidarProceso(0, ref objProcInd, ref strErrMsg);
            #endregion

            if (booResult)
            {
                object objProceso = new object();
                mtdGenerarProceso(objProcInd, ref objProceso);

                lstIndicador = cIndicador.mtdConsultarIndicador(objProcInd.intIdTipoProceso, objProceso, ref strErrMsg);//, objProcInd.intPeridoAnual

                if (lstIndicador != null)
                {
                    mtdLoadInfoGridIndicador(lstIndicador);
                    GridView1.DataSource = lstIndicador;
                    GridView1.PageIndex = PagIndex1;
                    GridView1.DataBind();
                    booResult = true;
                }
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
                InfoGrid1.Rows.Add(new Object[] {
                    objIndicador.intId.ToString().Trim(),
                    objIndicador.strNombreIndicador.ToString().Trim(),
                    objIndicador.strDescripcion.ToString().Trim(),
                    objIndicador.intIdPeriodicidad.ToString().Trim(),
                    objIndicador.strNombrePeriodicidad.ToString().Trim(),
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

        #region  GridView3 Variables

        private bool mtdCargarVariable(clsIndicador objIndicador, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridVariable();
            booResult = mtdLoadInfoGridVariableActivas(objIndicador, ref strErrMsg);

            return booResult;
        }
        private bool mtdCargarVariableByYear(clsIndicador objIndicador, ref string strErrMsg, int year)
        {
            bool booResult = false;

            mtdLoadGridVariable();
            booResult = mtdLoadInfoGridVariableActivasByYear(objIndicador, ref strErrMsg, year);

            return booResult;
        }

        private void mtdLoadGridVariable()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("strFormato", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));

            grid.Columns.Add("intIdDetalleVariable", typeof(string));
            grid.Columns.Add("decValor", typeof(string));
            grid.Columns.Add("intIdDetallePeriodo", typeof(string));
            grid.Columns.Add("strPeriodoAnual", typeof(string));
            grid.Columns.Add("strNombreDetPeriodo", typeof(string));
            grid.Columns.Add("intIdIndicador", typeof(string));

            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGrid2 = grid;
        }

        private bool mtdLoadInfoGridVariableActivas(clsIndicador objIndicador, ref string strErrMsg)
        {
            bool booResult = false;
            List<clsVariable> lstVariable = new List<clsVariable>();
            clsVariableBLL cVariable = new clsVariableBLL();

            booResult = cVariable.mtdConsultarVariable(true, objIndicador, ref lstVariable, ref strErrMsg);

            if (booResult)
                if (lstVariable != null)
                {
                    mtdLoadInfoGridVariableActivas(lstVariable);
                    //gvVariablesClean.DataSource = lstVariable;
                    //gvVariablesClean.PageIndex = PagIndex2;
                    //gvVariablesClean.DataBind();
                    ddlVariable.DataSource = lstVariable;
                    ddlVariable.DataTextField = "strDescripcion";
                    ddlVariable.DataValueField = "intId";
                    ddlVariable.DataBind();
                    booResult = true;
                }

            return booResult;
        }
        private bool mtdLoadInfoGridVariableActivasByYear(clsIndicador objIndicador, ref string strErrMsg, int year)
        {
            bool booResult = false;
            List<clsVariable> lstVariable = new List<clsVariable>();
            clsVariableBLL cVariable = new clsVariableBLL();

            booResult = cVariable.mtdConsultarVariableByYear(true, objIndicador, ref lstVariable, ref strErrMsg, year);

            if (booResult)
                if (lstVariable != null)
                {
                    mtdLoadInfoGridVariableActivas(lstVariable);
                    GridView3.DataSource = lstVariable;
                    GridView3.PageIndex = PagIndex2;
                    GridView3.DataBind();
                    booResult = true;
                }

            return booResult;
        }

        private void mtdLoadInfoGridVariableActivas(List<clsVariable> lstVariable)
        {
            foreach (clsVariable objVariable in lstVariable)
            {
                InfoGrid2.Rows.Add(new Object[] {
                    objVariable.intId.ToString().Trim(),
                    objVariable.strDescripcion.ToString().Trim(),
                    objVariable.strFormato.ToString().Trim(),
                    objVariable.booEstado.ToString().Trim(),
                    objVariable.intIdUsuario.ToString().Trim(),
                    objVariable.strNombreUsuario.ToString().Trim(),
                    objVariable.intIdDetalleVariable.ToString().Trim(),
                    objVariable.decValor.ToString().Trim(),
                    objVariable.intIdDetallePeriodo.ToString().Trim(),
                    objVariable.strNombreDetPeriodo.ToString().Trim(),
                    objVariable.strPeriodoAnual.ToString().Trim(),
                    objVariable.intIdIndicador.ToString().Trim(),
                    objVariable.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion

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
            //objProcInd.intPeridoAnual = Convert.ToInt32(txtPeridoFiltro.Text);
            //Session["intPeridoAnual"] = Convert.ToInt32(txtPeridoFiltro.Text);
            if (!booResult)
                strErrMsg = "Error al asociar el proceso al Indicador";

            return booResult;
        }

        protected void ddlPeriodoAnual_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void mtdCargarPeriodoAnual()
        {
            DataTable dtInfo = new DataTable();

            dtInfo = cCuenta.loadPeriodoAnual();

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ddlPeriodoAnual.Items.Insert(i, new ListItem(dtInfo.Rows[i]["FechaRegistro"].ToString().Trim()));
            }
        }



        private bool mtdMostrarVariables(ref string strErrMsg)
        {
            bool booResult = false;
            clsIndicador objIndicador = new clsIndicador(
                Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                string.Empty, string.Empty, Convert.ToInt32(Session["IdPeriodicidad"].ToString())
                , 0, true, 0, 0, 0, string.Empty);

            booResult = mtdCargarVariable(objIndicador, ref strErrMsg);

            return booResult;
        }

        private bool mtdMostrarVariablesByYear(ref string strErrMsg, int year)
        {
            bool booResult = false;
            clsIndicador objIndicador = new clsIndicador(
                Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                string.Empty, string.Empty, 0, 0, true, 0, 0, 0, string.Empty);

            booResult = mtdCargarVariableByYear(objIndicador, ref strErrMsg, year);

            return booResult;
        }

        private bool mtdAsignarVariables(ref string strErrMsg)
        {
            bool booResult = true;

            //if (!mtdValidarExisteDetalle(InfoGrid2.Rows[RowGrid2][6].ToString().Trim()))
            //{
            mtdHabilitarCampos(4);
            mtdLimpiarCamposVariable(1);
            mtdCargarPeriodoAnual();

            /*tbxId.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
            tbxIdIndicador.Text = InfoGrid2.Rows[RowGrid2][11].ToString().Trim();
            tbxDescVariable.Text = InfoGrid2.Rows[RowGrid2][1].ToString().Trim();*/
            tbxId.Text = ddlVariable.SelectedValue;

            booResult = mtdLoadDDLPeriodo(Convert.ToInt32(tbxIdPeriodicidad.Text.Trim()), ref strErrMsg);
            //}
            //else
            //{
            //    mtdHabilitarCampos(5);
            //    mtdLimpiarCamposVariable(1);

            //    tbxId.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
            //    tbxIdIndicador.Text = InfoGrid2.Rows[RowGrid2][10].ToString().Trim();
            //    tbxIdDetalle.Text = InfoGrid2.Rows[RowGrid2][6].ToString().Trim();
            //    tbxDescVariable.Text = InfoGrid2.Rows[RowGrid2][2].ToString().Trim();
            //    tbxVlrVariable.Text = InfoGrid2.Rows[RowGrid2][7].ToString().Trim();
            //    tbxFechaDesde.Text = InfoGrid2.Rows[RowGrid2][8].ToString().Trim();
            //    tbxFechaHasta.Text = InfoGrid2.Rows[RowGrid2][9].ToString().Trim();
            //}

            return booResult;
        }

        private bool mtdValidarExisteDetalle(string strIdDetalle)
        {
            bool booResult = false;

            if (Convert.ToInt32(strIdDetalle) == 0)
                booResult = false;
            else
                booResult = true;

            return booResult;
        }

        private bool mtdInsertarDetalle(ref string strErrMsg)
        {
            bool booResult = false;
            clsDetalleVariable objDetalle = new clsDetalleVariable(0,
                Convert.ToInt32(ddlVariable.SelectedValue),
                Convert.ToInt32(tbxVlrVariable.Text.Trim()),//tbxVlrVariable.Text.Trim()
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                //objDetalle.intIdIndicador.Text.Trim(),
                Convert.ToInt32(tbxIdIndicador.Text),
                Convert.ToInt32(ddlDetPeriodo.SelectedValue),
                //Convert.ToString(ddlPeriodoAnual.SelectedValue),
                txtperiodoanual.Text,
                string.Empty,
                tbxIdIndicador.Text, (bool)Session["Editar"]);
            clsVariableBLL cVariable = new clsVariableBLL();

            booResult = cVariable.mtdInsertarDetalle(objDetalle, ref strErrMsg);

            return booResult;
        }
        private bool mtdActualizarDetalle(ref string strErrMsg)
        {
            bool booResult = false;
            clsDetalleVariable objDetalle = new clsDetalleVariable(Convert.ToInt32(tbxId.Text),
                Convert.ToInt32(ddlVariable.SelectedValue),
                Convert.ToInt32(tbxVlrVariable.Text.Trim()),//tbxVlrVariable.Text.Trim()
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                //objDetalle.intIdIndicador.Text.Trim(),
                Convert.ToInt32(tbxIdIndicador.Text),
                Convert.ToInt32(ddlDetPeriodo.SelectedValue),
                //Convert.ToString(ddlPeriodoAnual.SelectedValue),
                txtperiodoanual.Text,
                string.Empty,
                Session["IdDetalleVariable"].ToString(), (bool)Session["Editar"]);
            clsVariableBLL cVariable = new clsVariableBLL();

            booResult = cVariable.mtdActualizarDetalle(objDetalle, ref strErrMsg);

            return booResult;
        }
        private void mtdGenerarProceso(clsProcesoIndicador objProcInd, ref object objProceso)
        {
            switch (objProcInd.intIdTipoProceso)
            {
                case 1:
                    clsMacroproceso objMp = new clsMacroproceso(objProcInd.intIdProceso,
                        string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty, string.Empty);
                    objProceso = (object)objMp;
                    break;
                case 2:
                    clsProceso objP = new clsProceso(objProcInd.intIdProceso,
                        0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty);
                    objProceso = (object)objP;
                    break;
                case 3:
                    clsSubproceso objSp = new clsSubproceso(objProcInd.intIdProceso,
                        string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty);
                    objProceso = (object)objSp;
                    break;
            }

        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex1 = e.NewPageIndex;
            GridView1.PageIndex = PagIndex1;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridIndicador(ref strErrMsg);
        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex2 = e.NewPageIndex;
            GridView3.PageIndex = PagIndex2;
            GridView3.DataBind();
            string strErrMsg = "";
            bool booResult = false;
            clsIndicador objIndicador = new clsIndicador(
                Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                string.Empty, string.Empty, 0, 0, true, 0, 0, 0, string.Empty);

            booResult = mtdCargarVariable(objIndicador, ref strErrMsg);
        }

        protected void IBclear_Click(object sender, ImageClickEventArgs e)
        {
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.ClearSelection();
            ddlProceso.ClearSelection();
            ddlSubproceso.ClearSelection();
            txtPeridoFiltro.Text = string.Empty;
            Session["Editar"] = false;
        }

        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            GridView3.Visible = false;
            gvVariablesClean.Visible = true;
            btnInsertarNuevo.Visible = false;
            btnGuardarVar.Visible = true;
            btnActualizarVar.Visible = false;
            string strErrMsg = string.Empty;
            int year = Convert.ToInt32(Session["intPeridoAnual"].ToString());
            //System.Diagnostics.Debug.WriteLine("Antes de entrar mtdMostrarVariables");
            //Debug.WriteLine("Antes de entrar mtdMostrarVariables");
            //sw.WriteLine("Antes de entrar mtdMostrarVariables");
            tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            List<clsVariable> lstVariable = new List<clsVariable>();
            clsVariableBLL cVariable = new clsVariableBLL();

            lstVariable = cVariable.mtdConsultarVariable(ref strErrMsg);

            ddlVariable.DataSource = lstVariable;
            ddlVariable.DataTextField = "strDescripcion";
            ddlVariable.DataValueField = "intId";
            ddlVariable.DataBind();
            //if (!mtdMostrarVariables(ref strErrMsg))
            //    omb.ShowMessage(strErrMsg, 1, "Atención");
            //else
            //{
            //    mtdHabilitarCampos(3);
            //    tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            //    tbxIdPeriodicidad.Text = InfoGrid1.Rows[RowGrid1][3].ToString().Trim();
            //}

            if (!mtdAsignarVariables(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");

        }

        protected void gvVariablesClean_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;
            RowGrid2 = (Convert.ToInt16(gvVariablesClean.PageSize) * PagIndex2) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Asignar":
                    if (!mtdAsignarVariables(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    break;
                case "Editar":
                    EditarVariable();
                    break;
            }
        }

        protected void gvVariablesClean_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnImgok_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            Session["intPeridoAnual"] = txtPeridoFiltro.Text;
            mpeMsgBox.Hide();
            if (!mtdMostrarVariablesByYear(ref strErrMsg, Convert.ToInt32(txtPeridoFiltro.Text)))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            else
            {
                mtdHabilitarCampos(3);
                tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
                tbxIdPeriodicidad.Text = InfoGrid1.Rows[RowGrid1][3].ToString().Trim();
            }
        }
    }
}