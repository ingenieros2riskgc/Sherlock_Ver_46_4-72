using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class SeguimientoIndicadores : System.Web.UI.UserControl
    {
        string IdFormulario = "4020";
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
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.GridView1);
            scriptManager.RegisterPostBackControl(this.btnImgCancelar);
            scriptManager.RegisterPostBackControl(this.btnImgok);
            string strErrMsg = string.Empty;

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    mtdHabilitarCampos(1);
                    if (!mtdCargarDDLs(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
        }

        #region Gridview
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            theDiv.Visible = false;
            string strErrMsg = string.Empty;
            RowGrid1 = (Convert.ToInt16(GridView1.PageSize) * PagIndex1) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seguimiento":
                    mpeMsgBox.Show();
                    //}
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;
            RowGrid2 = (Convert.ToInt16(GridView2.PageSize) * PagIndex2) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    if (!mtdModificarAnalisis(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    break;
            }
        }
        #endregion

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

            //if (ddlProceso.SelectedValue == "0")
            //    rfvProceso.Enabled = false;
            //else
            //    rfvProceso.Enabled = true;

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

        protected void ddlDetPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            clsDetalleVariable objDetVar = new clsDetalleVariable(0, 0, 0, 0,
                Convert.ToInt32(tbxIdIndicador.Text.Trim()), Convert.ToInt32(ddlDetPeriodo.SelectedValue), Session["intPeridoAnual"].ToString(),
                string.Empty, string.Empty, false);

            // verificar cuantas variables hay para el indicador en las formulas y si tienen calificacion en el periodo
            if (!mtdValidarCalifVar(objDetVar, ref strErrMsg))
            {
                tbxEstaEvaluado.Text = "0";
                omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            else
                tbxEstaEvaluado.Text = "1";
        }
        #endregion

        #region Buttons
        protected void btnBuscarIndicador_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                mtdHabilitarCampos(2);
                if (mtdCargarIndicador(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
        }

        protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            theDiv.Visible = false;
            string strErrMsg = string.Empty;

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                ddlDetPeriodo.Focus();

                mtdLoadDDLPeriodo(Convert.ToInt32(tbxIdPeriodicidad.Text.Trim()), ref strErrMsg);
                mtdLimpiarCampos(1);
                mtdHabilitarCampos(4);
            }
        }

        protected void btnRegresarFiltros_Click(object sender, ImageClickEventArgs e)
        {
            mtdHabilitarCampos(1);
        }

        protected void btnInsertarAnalisis_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (tbxEstaEvaluado.Text == "1")
                {
                    if (mtdInsertarAnalisis(ref strErrMsg))
                    {
                        //omb.ShowMessage("Seguimiento registrado exitosamente.", 3, "Atención");
                        theDiv.Visible = true;
                        Lmensaje.Text = "Seguimiento registrado exitosamente";
                        mtdHabilitarCampos(3);
                        /*clsIndicador objIndicador = new clsIndicador(
                            Convert.ToInt32(tbxIdIndicador.Text.Trim()),
                            string.Empty, 0, 0, true, 0, 0, 0, string.Empty);*/
                        clsIndicador objIndicador = new clsIndicador(
                        Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                        string.Empty, string.Empty,
                        Convert.ToInt32(InfoGrid1.Rows[RowGrid1][2].ToString().Trim()),
                        0, true, 0, 0, 0, string.Empty);

                        mtdCargarAnalisis(objIndicador, ref strErrMsg);
                        mtdGeneracionDataInformativa(objIndicador, ref strErrMsg);
                        mtdLimpiarCampos(1);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
                else
                    omb.ShowMessage("No se puede realizar el registro. Por favor revisar la información registrada para efectuar el seguimiento.", 1, "Atención");
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al registrar el seguimiento del indicador.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnActualizarAnalisis_Click(object sender, ImageClickEventArgs e)
        {
            theDiv.Visible = false;
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    if (tbxEstaEvaluado.Text == "1")
                    {
                        if (mtdActualizarAnalisis(ref strErrMsg))
                        {
                            //omb.ShowMessage("Seguimiento actualizado exitosamente.", 3, "Atención");
                            theDiv.Visible = true;
                            Lmensaje.Text = "Seguimiento actualizado exitosamente";
                            /*clsIndicador objIndicador = new clsIndicador(
                                Convert.ToInt32(tbxIdIndicador.Text.Trim()),
                                string.Empty, 0, 0, true, 0, 0, 0, string.Empty);*/
                            clsIndicador objIndicador = new clsIndicador(
                            Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                            string.Empty, string.Empty,
                            Convert.ToInt32(InfoGrid1.Rows[RowGrid1][2].ToString().Trim()),
                            0, true, 0, 0, 0, string.Empty);
                            mtdCargarAnalisis(objIndicador, ref strErrMsg);
                            mtdGeneracionDataInformativa(objIndicador, ref strErrMsg);
                            mtdHabilitarCampos(3);
                            mtdLimpiarCampos(1);
                        }
                        else
                            omb.ShowMessage(strErrMsg, 1, "Atención");
                    }
                    else
                        omb.ShowMessage("No se puede realizar la actualización. Por favor revisar la información registrada para efectuar el seguimiento.", 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el seguimiento del indicador.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarAnalisis_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            mtdHabilitarCampos(3);
            mtdMostrarAnalisis(ref strErrMsg);
        }

        protected void btnRegresarIndicador_Click(object sender, ImageClickEventArgs e)
        {
            mtdHabilitarCampos(2);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo que inicia valores del datagrid
        /// </summary>
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
        }

        /// <summary>
        /// Metodo que habilita los campos de acuerdo al evento a ejecutar
        /// </summary>
        /// <param name="intOpcion">Opcion de los campos a habilitar</param>
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

                    FilaTituloAnalisis.Visible = false;
                    FilaGridAnalisis.Visible = false;
                    FilaAnalisis.Visible = false;
                    FilaInformativa.Visible = false;
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

                    FilaTituloAnalisis.Visible = false;
                    FilaGridAnalisis.Visible = false;
                    FilaAnalisis.Visible = false;
                    FilaInformativa.Visible = false;

                    BotonRegresarIndicador.Visible = false;

                    #endregion
                    break;
                case 3: // Ver grid Analisis
                    #region
                    FilaProcesos.Visible = false;
                    FilaBotonBuscar.Visible = false;

                    FilaTituloIndicador.Visible = false;
                    FilaGridIndicador.Visible = false;
                    BotonRegresarFiltros.Visible = false;

                    FilaTituloAnalisis.Visible = true;
                    FilaGridAnalisis.Visible = true;
                    FilaAnalisis.Visible = false;
                    FilaInformativa.Visible = true;

                    BotonRegresarIndicador.Visible = true;

                    #endregion
                    break;
                case 4: // Nuevo Analisis
                    #region
                    FilaProcesos.Visible = false;
                    FilaBotonBuscar.Visible = false;

                    FilaTituloIndicador.Visible = false;
                    FilaGridIndicador.Visible = false;
                    BotonRegresarFiltros.Visible = false;

                    FilaTituloAnalisis.Visible = true;
                    FilaGridAnalisis.Visible = false;
                    FilaAnalisis.Visible = true;
                    FilaInformativa.Visible = false;

                    btnInsertarAnalisis.Visible = true;
                    btnActualizarAnalisis.Visible = false;
                    btnCancelarAnalisis.Visible = true;

                    BotonRegresarIndicador.Visible = true;

                    #endregion
                    break;
                case 5: // Modificar Detalle Analisis
                    #region
                    FilaProcesos.Visible = false;
                    FilaBotonBuscar.Visible = false;

                    FilaTituloIndicador.Visible = false;
                    FilaGridIndicador.Visible = false;
                    BotonRegresarFiltros.Visible = false;

                    FilaTituloAnalisis.Visible = true;
                    FilaGridAnalisis.Visible = false;
                    FilaAnalisis.Visible = true;
                    FilaInformativa.Visible = false;

                    btnInsertarAnalisis.Visible = false;
                    btnActualizarAnalisis.Visible = true;
                    btnCancelarAnalisis.Visible = true;

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
                        //rfvProceso.Enabled = true;
                    }
                }
                else
                {
                    booResult = false;
                    //rfvProceso.Enabled = false;
                }
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
                       // rfvProceso.Enabled = true;
                    }
                }
                else
                {
                    booResult = false;
                   // rfvProceso.Enabled = false;
                }
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
            #region Vars
            bool booResult = false;
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsIndicadorBLL cIndicador = new clsIndicadorBLL();
            #endregion

            #region Proceso del indicador
            booResult = mtdValidarProceso(0, ref objProcInd, ref strErrMsg);
            #endregion

            if (booResult)
            {
                object objProceso = new object();
                mtdGenerarProceso(objProcInd, ref objProceso);

                lstIndicador = cIndicador.mtdConsultarIndicador(objProcInd.intIdTipoProceso, objProceso, ref strErrMsg);

                if (lstIndicador != null)
                {
                    mtdLoadInfoGridIndicador(lstIndicador);
                    GridView1.DataSource = lstIndicador;
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

        #region Gridview Analisis
        private bool mtdCargarAnalisis(clsIndicador objIndicador, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridAnalisis();
            booResult = mtdLoadInfoGridAnalisis(objIndicador, ref strErrMsg, Convert.ToInt32(Session["intPeridoAnual"].ToString()));

            return booResult;
        }

        private void mtdLoadGridAnalisis()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("intIdSeguimientoIndicador", typeof(string));
            grid.Columns.Add("intIdIndicador", typeof(string));
            grid.Columns.Add("strDescripcionAnalisis", typeof(string));
            grid.Columns.Add("strDescripcionAccionCorrectiva", typeof(string));
            grid.Columns.Add("intIdDetPeriodo", typeof(string));
            grid.Columns.Add("dtFechaDescripcionAnalisis", typeof(string));
            grid.Columns.Add("dtFechaAccionCorrectiva", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGrid2 = grid;
        }

        private bool mtdLoadInfoGridAnalisis(clsIndicador objIndicador, ref string strErrMsg, int periodoAnual)
        {
            bool booResult = false;
            List<clsDetalleSeguimientoIndicador> lstDetSegIndicador = new List<clsDetalleSeguimientoIndicador>();
            clsSeguimientoIndicadorBLL cSeguimiento = new clsSeguimientoIndicadorBLL();

            booResult = cSeguimiento.mtdConsultarSeguimiento(objIndicador, ref lstDetSegIndicador, ref strErrMsg, periodoAnual);

            if (booResult)
            {
                if (lstDetSegIndicador != null)
                {
                    mtdLoadInfoGridAnalisis(lstDetSegIndicador);
                    GridView2.DataSource = lstDetSegIndicador;
                    GridView2.DataBind();
                    booResult = true;
                }
                else
                {
                    strErrMsg = "No hay datos para cargar";
                    booResult = false;
                }
            }
            else
            {
                strErrMsg = "No hay datos para cargar";
                booResult = false;
            }
            return booResult;
        }

        private void mtdLoadInfoGridAnalisis(List<clsDetalleSeguimientoIndicador> lstDetSegIndicador)
        {
            foreach (clsDetalleSeguimientoIndicador objSegIndicador in lstDetSegIndicador)
            {
                InfoGrid2.Rows.Add(new Object[] {
                    objSegIndicador.intId.ToString().Trim(),
                    objSegIndicador.intIdSeguimientoIndicador.ToString().Trim(),
                    objSegIndicador.intIdIndicador.ToString().Trim(),
                    objSegIndicador.strDescripcionAnalisis.ToString().Trim(),
                    objSegIndicador.strDescripcionAccionCorrectiva.ToString().Trim(),
                    objSegIndicador.intIdDetPeriodo.ToString().Trim(),
                    objSegIndicador.dtFechaDescripcionAnalisis.ToString().Trim(),
                     objSegIndicador.dtFechaAccionCorrectiva.ToString().Trim(),
                    objSegIndicador.intIdUsuario.ToString().Trim(),
                    objSegIndicador.strNombreUsuario.ToString().Trim(),
                    objSegIndicador.dtFechaRegistro.ToString().Trim()
                   });
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// Metodo que valida la informacion del proceso de acuerdo a filtros seleccionados
        /// </summary>
        /// <param name="intId">Identificador del proceso del indicador</param>
        /// <param name="objProcInd">Objeto con la informacion del proceso indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
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

        /// <summary>
        /// Metodo que permite la generacion del proceso con la informacion de los filtros seleccionados
        /// </summary>
        /// <param name="objProcInd">Objeto con la informacion del proceso del indicador</param>
        /// <param name="objProceso">Objeto con la informacion detallada del proceso</param>
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
                        0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty);
                    objProceso = (object)objP;
                    break;
                case 3:
                    clsSubproceso objSp = new clsSubproceso(objProcInd.intIdProceso,
                        string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty);
                    objProceso = (object)objSp;
                    break;
            }

        }

        /// <summary>
        /// Metodo que muestra la informacion del analisis de seguimiento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        private bool mtdMostrarAnalisis(ref string strErrMsg)
        {
            bool booResult = false;
            clsIndicador objIndicador = new clsIndicador(
                Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                string.Empty, string.Empty,
                Convert.ToInt32(InfoGrid1.Rows[RowGrid1][2].ToString().Trim()),
                0, true, 0, 0, 0, string.Empty);

            booResult = mtdCargarAnalisis(objIndicador, ref strErrMsg);
            /*if(booResult == true)
            {*/
            mtdGeneracionDataInformativa(objIndicador, ref strErrMsg);
            //}

            return booResult;
        }

        /// <summary>
        /// Metodo que permite la carga de la informacion para ser mostrada
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        private bool mtdModificarAnalisis(ref string strErrMsg)
        {
            bool booResult = true;

            mtdHabilitarCampos(5);
            mtdLimpiarCampos(1);

            tbxIdDetSeg.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
            tbxDescAnalisis.Text = InfoGrid2.Rows[RowGrid2][3].ToString().Trim();
            tbxAccCorrectiva.Text = InfoGrid2.Rows[RowGrid2][4].ToString().Trim();

            booResult = mtdLoadDDLPeriodo(Convert.ToInt32(tbxIdPeriodicidad.Text.Trim()), ref strErrMsg);

            ddlDetPeriodo.SelectedValue = InfoGrid2.Rows[RowGrid2][5].ToString().Trim();
            tbxEstaEvaluado.Text = "1";

            return booResult;
        }

        /// <summary>
        /// Metodo que limpia los campos del formulario
        /// </summary>
        /// <param name="intOpcion">Opcion con la informacion de los campos a limpiar</param>
        private void mtdLimpiarCampos(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1: // todo
                    tbxIdDetSeg.Text = string.Empty;
                    tbxDescAnalisis.Text = string.Empty;
                    tbxAccCorrectiva.Text = string.Empty;
                    ddlDetPeriodo.SelectedValue = "0";
                    break;
                case 2:
                    tbxIdDetSeg.Text = string.Empty;
                    //tbxIdIndicador.Text = string.Empty;
                    //tbxIdPeriodicidad.Text = string.Empty;
                    tbxDescAnalisis.Text = string.Empty;
                    tbxAccCorrectiva.Text = string.Empty;
                    ddlDetPeriodo.SelectedValue = "0";

                    break;
            }
        }

        /// <summary>
        /// Metodo que inserta la nformacion  con los campos del analisis
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        private bool mtdInsertarAnalisis(ref string strErrMsg)
        {
            bool booResult = false;
            clsDetalleSeguimientoIndicador objDetSegIndicador = new clsDetalleSeguimientoIndicador(0,
                0,
                Convert.ToInt32(tbxIdIndicador.Text.Trim()),
                tbxDescAnalisis.Text.Trim(),
                tbxAccCorrectiva.Text.Trim(),
                Convert.ToInt32(ddlDetPeriodo.SelectedValue.ToString().Trim()),
                string.Empty, string.Empty,
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty, string.Empty,
                Convert.ToInt32(Session["intPeridoAnual"].ToString()));
            clsSeguimientoIndicadorBLL cSegIndicador = new clsSeguimientoIndicadorBLL();

            booResult = cSegIndicador.mtdInsertarSeguimiento(objDetSegIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo que actualiza la informacion del analisis
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        private bool mtdActualizarAnalisis(ref string strErrMsg)
        {
            bool booResult = false;
            clsDetalleSeguimientoIndicador objDetSegIndicador = new clsDetalleSeguimientoIndicador(
                Convert.ToInt32(tbxIdDetSeg.Text.Trim()),
                0,
                tbxDescAnalisis.Text.Trim(),
                tbxAccCorrectiva.Text.Trim(),
                Convert.ToInt32(ddlDetPeriodo.SelectedValue.ToString().Trim()),
                string.Empty, string.Empty, 0, string.Empty);

            clsSeguimientoIndicadorBLL cSegIndicador = new clsSeguimientoIndicadorBLL();

            booResult = cSegIndicador.mtdActualizarSeguimiento(objDetSegIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo que sirve para consultar y validar la informacion de las calificaciones de las variables
        /// </summary>
        /// <param name="objDetVar">Objeto con la informacion del detalle de la variable a consultar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        private bool mtdValidarCalifVar(clsDetalleVariable objDetVar, ref string strErrMsg)
        {
            bool booResult = false;
            clsVariableBLL cVariable = new clsVariableBLL();

            booResult = cVariable.mtdValidarValoresVars(objDetVar, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para la generacion de la data informativa y llenado de campos
        /// </summary>
        /// <param name="objIndicador">Objeto con la informacion del indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdGeneracionDataInformativa(clsIndicador objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            object objProceso = new object();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            clsSeguimientoIndicadorBLL cSegIndicador = new clsSeguimientoIndicadorBLL();
            #endregion

            #region Generacion Proceso del indicador
            #region Valido el proceso de acuerdo a los filtros
            booResult = mtdValidarProceso(0, ref objProcInd, ref strErrMsg);
            #endregion

            #region Generar Proceso de acuerdo a la validacion
            //if (booResult)
            //{
            //    mtdGenerarProceso(objProcInd, ref objProceso);
            //}
            #endregion
            #endregion

            #region Consulta de Periodos, variables, cabeceras y cuadro de mando
            DataTable dtCuadro = new DataTable();
            DataTable dtCabCuadro = new DataTable();
            string strFormula = string.Empty;
            cSegIndicador.mtdConsultarCuadroMando(objProcInd, objIndicador, ref dtCuadro, ref dtCabCuadro, ref strFormula, ref strErrMsg, Convert.ToInt32(Session["intPeridoAnual"]));
            #endregion
            if (dtCabCuadro.Rows.Count > 0)
            {
                #region Llenado de Region Informativa
                #region Cab. Cuadro Mando
                DataRow dr = dtCabCuadro.Rows[0];
                tbxCabEstrategia.Text = dr["Estrategia"].ToString();
                tbxCabMeta.Text = dr["Meta"].ToString();
                //tbxCabFrecuencia.Text = dr["NombrePeriodo"].ToString();
                tbxCabFrecuencia.Text = dr["Periodo"].ToString();
                tbxCabObjetivo.Text = dr["ObjCalidad"].ToString();
                tbxCabProceso.Text = dr["Proceso"].ToString();
                tbxCabResponsable.Text = dr["Responsable"].ToString();
                tbxCabFormula.Text = strFormula;
                #endregion

                #region Grid con la data informativa
                GridView3.DataSource = dtCuadro;
                GridView3.DataBind();
                #endregion

                #region Grafico Cuadro Mando
                string[] x = new string[dtCuadro.Rows.Count];
                decimal[] y = new decimal[dtCuadro.Rows.Count];
                int intNroCols = dtCuadro.Columns.Count;
                int intNroRegs = dtCuadro.Rows.Count;

                #region Recorrido asignacion de datos al grafico
                for (int i = 0; i < intNroRegs; i++)
                {
                    x[i] = dtCuadro.Rows[i][0].ToString();
                    if(dtCuadro.Rows[i][intNroCols - 3].ToString() != string.Empty)
                        y[i] = Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]);
                }
                #endregion

                #region Ajustes de Configuracion
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.Legends[0].Enabled = true;
                Chart1.ChartAreas["ChartArea1"].Axes[0].Interval = 1;
                #endregion

                #region Colores
                switch (intNroRegs)
                {
                    case 12:
                        Chart1.Series[0].Points[0].Color = System.Drawing.Color.Blue;
                        Chart1.Series[0].Points[1].Color = System.Drawing.Color.MediumBlue;
                        Chart1.Series[0].Points[2].Color = System.Drawing.Color.LightBlue;
                        Chart1.Series[0].Points[3].Color = System.Drawing.Color.SkyBlue;
                        Chart1.Series[0].Points[4].Color = System.Drawing.Color.MediumSeaGreen;
                        Chart1.Series[0].Points[5].Color = System.Drawing.Color.PaleGreen;
                        Chart1.Series[0].Points[6].Color = System.Drawing.Color.Green;
                        Chart1.Series[0].Points[7].Color = System.Drawing.Color.LawnGreen;
                        Chart1.Series[0].Points[8].Color = System.Drawing.Color.LightGreen;
                        Chart1.Series[0].Points[9].Color = System.Drawing.Color.SeaGreen;
                        Chart1.Series[0].Points[10].Color = System.Drawing.Color.MediumPurple;
                        Chart1.Series[0].Points[11].Color = System.Drawing.Color.Purple;
                        break;
                    case 6:
                        Chart1.Series[0].Points[0].Color = System.Drawing.Color.Blue;
                        Chart1.Series[0].Points[1].Color = System.Drawing.Color.MediumBlue;
                        Chart1.Series[0].Points[2].Color = System.Drawing.Color.LightBlue;
                        Chart1.Series[0].Points[3].Color = System.Drawing.Color.SkyBlue;
                        Chart1.Series[0].Points[4].Color = System.Drawing.Color.MediumSeaGreen;
                        Chart1.Series[0].Points[5].Color = System.Drawing.Color.PaleGreen;
                        break;
                    case 4:
                        Chart1.Series[0].Points[0].Color = System.Drawing.Color.MediumSeaGreen;
                        Chart1.Series[0].Points[1].Color = System.Drawing.Color.PaleGreen;
                        Chart1.Series[0].Points[2].Color = System.Drawing.Color.LawnGreen;
                        Chart1.Series[0].Points[3].Color = System.Drawing.Color.Green;
                        break;
                    case 2:
                        Chart1.Series[0].Points[0].Color = System.Drawing.Color.Blue;
                        Chart1.Series[0].Points[1].Color = System.Drawing.Color.MediumBlue;
                        break;
                    case 1:
                        Chart1.Series[0].Points[0].Color = System.Drawing.Color.Blue;
                        break;
                }
                #endregion

                #endregion
                #endregion
            }
            else
            {
                omb.ShowMessage("No hay datos suficientes para realizar el seguimiento, Por favor validar los valores de las variables", 2, "Atención");
            }
        }

        #endregion

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.ClearSelection();
            ddlProceso.ClearSelection();
            ddlSubproceso.ClearSelection();
        }

        protected void btnImgok_Click(object sender, EventArgs e)
        {
            Session["intPeridoAnual"] = txtPeridoFiltro.Text;
            mpeMsgBox.Hide();
            string strErrMsg = string.Empty;
            Boolean booResult = mtdMostrarAnalisis(ref strErrMsg);
            /*if (booResult == true)
                omb.ShowMessage(strErrMsg, 1, "Atención");
            else
            {*/
            mtdHabilitarCampos(3);
            tbxIdIndicador.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            tbxIdPeriodicidad.Text = InfoGrid1.Rows[RowGrid1][2].ToString().Trim();
        }
    }
}