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
    public partial class CriteriosDesempeno : System.Web.UI.UserControl
    {
        string IdFormulario = "4016";
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
            scriptManager.RegisterPostBackControl(this.btnInsertarDetFactor);
            string strErrMsg = string.Empty;

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    if (!mtdCargarFactores(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                }
            }
        }

        #region Buttons
        #region Factor
        protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                tbxNombre.Focus();

                mtdLimpiarCamposFactor();
                mtdHabilitarCamposFactor(2);

                tbxId.Enabled = false;
                tbxUsuarioCreacion.Enabled = false;
                tbxFecha.Enabled = false;
            }
        }

        protected void btnInsertarFact_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (tbxNombre.Text != "")
            {
                try
                {
                    if (mtdInsertarFactor(ref strErrMsg))
                    {
                        omb.ShowMessage("Factor de desempeño registrado exitosamente.", 3, "Atención");
                        mtdLimpiarCamposFactor();
                        mtdHabilitarCamposFactor(1);
                        mtdCargarFactores(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error al registrar el Factor. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
                }
            }
            else
            {
                omb.ShowMessage("El nombre del factor no puede ser vacio", 1, "Atención");
            }
        }

        protected void btnActualizarFact_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    if (mtdActualizarFactor(ref strErrMsg))
                    {
                        omb.ShowMessage("Factor de desempeño modificada exitosamente.", 3, "Atención");
                        mtdLimpiarCamposFactor();
                        mtdHabilitarCamposFactor(1);
                        mtdCargarFactores(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el Factor.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarFact_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposFactor();
            mtdHabilitarCamposFactor(1);
        }
        #endregion

        #region Detalle Factor
        protected void btnNuevoDetFactor_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                tbxDescripcionDetFactor.Focus();
                                
                if (!mtdLoadDDLCalificacion(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 1, "Atención");
                
                mtdLimpiarCamposDetFactor();
                mtdHabilitarCamposFactor(5);

                tbxCodigoDetFactor.Enabled = false;
                tbxIdFactorDetFactor.Enabled = false;
                tbxNombreFactorDetFactor.Enabled = false;
                tbxUsuarioCreacionDetFactor.Enabled = false;
                tbxFechaDetFactor.Enabled = false;

                tbxIdFactorDetFactor.Text = tbxId.Text;
                tbxNombreFactorDetFactor.Text = tbxNombre.Text;
            }
        }

        protected void btnInsertarDetFactor_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            //if (tbxDescripcionDetFactor.Text != "" || tbxValorMinimo.Text != "" & tbxValorMaximo.Text != "")
            //{
                try
                {
                    if (mtdInsertarDetFactor(ref strErrMsg))
                    {
                        omb.ShowMessage("Criterio del factor de desempeño registrado exitosamente.", 3, "Atención");
                        mtdLimpiarCamposDetFactor();
                        mtdDetallesFactor(tbxId.Text.Trim(), ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error al registrar el Criterio. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
                }
            /*}else
            {
                omb.ShowMessage("No se permiten dejar valores vacios", 1, "Atención");
            }*/
        }

        protected void btnActualizarDetFactor_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    if (mtdActualizarDetFactor(ref strErrMsg))
                    {
                        omb.ShowMessage("Criterio del factor de desempeño modificado exitosamente.", 3, "Atención");
                        mtdLimpiarCamposDetFactor();
                        mtdDetallesFactor(tbxId.Text.Trim(), ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el Criterio. <br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarDetFactor_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposDetFactor();
            mtdHabilitarCamposFactor(4);
        }
        #endregion

        protected void btnRegresarFactor_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposFactor();
            mtdLimpiarCamposDetFactor();
            mtdHabilitarCamposFactor(1);
        }
        #endregion

        #region Gridviews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;
            RowGrid1 = (Convert.ToInt16(GridView1.PageSize) * PagIndex1) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
                    break;
                case "Detalles":
                    if (!mtdDetallesFactor(ref strErrMsg))
                        omb.ShowMessage("No hay datos para cargar", 2, "Atención");
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
                    mtdModificarDetFactor();
                    break;
            }
        }
        #endregion

        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
        }

        private void mtdHabilitarCamposFactor(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1://Ver Grid Factor
                    #region
                    filaGridDesempeno.Visible = true;
                    trTituloDesempeno.Visible = false;
                    filaDetalleDesempeno.Visible = false;
                    trTituloDetallesFact.Visible = false;
                    FilaGridDetallesFact.Visible = false;
                    FilaDetallesFact.Visible = false;

                    BotonRegresar.Visible = false;

                    btnInsertarFact.Visible = false;
                    btnActualizarFact.Visible = false;
                    btnCancelarFact.Visible = false;
                    #endregion
                    break;
                case 2: //Editar Factor (insertar)
                    #region
                    filaGridDesempeno.Visible = false;
                    trTituloDesempeno.Visible = true;
                    filaDetalleDesempeno.Visible = true;
                    trTituloDetallesFact.Visible = false;
                    FilaGridDetallesFact.Visible = false;
                    FilaDetallesFact.Visible = false;

                    BotonRegresar.Visible = false;

                    btnInsertarFact.Visible = true;
                    btnActualizarFact.Visible = false;
                    btnCancelarFact.Visible = true;
                    #endregion
                    break;
                case 3://Editar Factor (Actualizar)
                    #region
                    filaGridDesempeno.Visible = false;
                    trTituloDesempeno.Visible = true;
                    filaDetalleDesempeno.Visible = true;
                    trTituloDetallesFact.Visible = false;
                    FilaGridDetallesFact.Visible = false;
                    FilaDetallesFact.Visible = false;

                    BotonRegresar.Visible = false;

                    btnInsertarFact.Visible = false;
                    btnActualizarFact.Visible = true;
                    btnCancelarFact.Visible = true;
                    #endregion
                    break;
                case 4: //Ver Grid Detalle Factor
                    #region
                    filaGridDesempeno.Visible = false;
                    trTituloDesempeno.Visible = false;
                    filaDetalleDesempeno.Visible = false;
                    trTituloDetallesFact.Visible = true;
                    FilaGridDetallesFact.Visible = true;
                    FilaDetallesFact.Visible = false;

                    BotonRegresar.Visible = true;

                    btnInsertarFact.Visible = false;
                    btnActualizarFact.Visible = false;
                    btnCancelarFact.Visible = false;
                    #endregion
                    break;
                case 5://Editar Detalle Factor (insertar)
                    #region
                    filaGridDesempeno.Visible = false;
                    trTituloDesempeno.Visible = false;
                    filaDetalleDesempeno.Visible = false;
                    trTituloDetallesFact.Visible = true;
                    FilaGridDetallesFact.Visible = false;
                    FilaDetallesFact.Visible = true;

                    BotonRegresar.Visible = true;

                    btnInsertarDetFactor.Visible = true;
                    btnActualizarDetFactor.Visible = false;
                    btnCancelarDetFactor.Visible = true;
                    #endregion
                    break;
                case 6://Editar Criterios (Actualizar)
                    #region
                    filaGridDesempeno.Visible = false;
                    trTituloDesempeno.Visible = false;
                    filaDetalleDesempeno.Visible = false;
                    trTituloDetallesFact.Visible = true;
                    FilaGridDetallesFact.Visible = false;
                    FilaDetallesFact.Visible = true;

                    BotonRegresar.Visible = true;

                    btnInsertarDetFactor.Visible = false;
                    btnActualizarDetFactor.Visible = true;
                    btnCancelarDetFactor.Visible = true;
                    #endregion
                    break;
            }
        }

        #region Limpiar Campos
        private void mtdLimpiarCamposFactor()
        {
            tbxId.Text = string.Empty;
            tbxNombre.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }

        private void mtdLimpiarCamposDetFactor()
        {
            tbxCodigoDetFactor.Text = string.Empty;
            tbxDescripcionDetFactor.Text = string.Empty;
            tbxIdFactorDetFactor.Text = string.Empty;
            tbxNombreFactorDetFactor.Text = string.Empty;
            ddlCalificacion.SelectedValue = "0";
            tbxUsuarioCreacionDetFactor.Text = string.Empty;
            tbxFechaDetFactor.Text = string.Empty;
            tbxValorMinimo.Text = String.Empty;
            tbxValorMaximo.Text = String.Empty;
        }
        #endregion

        #region Cargas
        #region Gridview Factores
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarFactores(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridFactores();
            booResult = mtdLoadInfoGridFactores(ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridFactores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strFactoresEvaluacion", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid1 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos del factor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridFactores(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsFactoresDesempeno> lstFactor = new List<clsFactoresDesempeno>();
            clsFactoresDesempenoBLL cFactor = new clsFactoresDesempenoBLL();

            booResult = cFactor.mtdConsultarFactor(ref lstFactor, ref strErrMsg);

            if (booResult)
                if (lstFactor != null)
                {
                    mtdLoadInfoGridFactores(lstFactor);
                    GridView1.DataSource = lstFactor;
                    GridView1.DataBind();
                }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los Factores</param>
        private void mtdLoadInfoGridFactores(List<clsFactoresDesempeno> lstFactor)
        {
            foreach (clsFactoresDesempeno objFactor in lstFactor)
            {
                InfoGrid1.Rows.Add(new Object[] {
                    objFactor.intId.ToString().Trim(),
                    objFactor.strFactoresEvaluacion.ToString().Trim(),
                    objFactor.intIdUsuario.ToString().Trim(),
                    objFactor.strNombreUsuario.ToString().Trim(),
                    objFactor.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview Detalles Factores
        private bool mtdCargarDetallesFactor(clsFactoresDesempeno objFactor, ref string strErrMsg)
        {
            bool booResult = false;
            List<clsDetalleFactorDesempeno> lstDetFactor = new List<clsDetalleFactorDesempeno>();
            clsFactoresDesempenoBLL cFactor = new clsFactoresDesempenoBLL();

            booResult = cFactor.mtdConsultarDetFactor(objFactor, ref lstDetFactor, ref strErrMsg);

            if (booResult)
            {
                mtdLoadGridDetFactor();
                mtdLoadInfoGridDetFactor(lstDetFactor, ref strErrMsg);
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridDetFactor()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("intIdFactoresDesempeno", typeof(string));
            grid.Columns.Add("strNombreFactor", typeof(string));
            grid.Columns.Add("intIdCalificacion", typeof(string));
            grid.Columns.Add("strNombreCalificacion", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("decCriterioMinimo", typeof(string));
            grid.Columns.Add("decCriterioMaximo", typeof(string));

            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGrid2 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de los Detalles Factor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridDetFactor(List<clsDetalleFactorDesempeno> lstDetFactor, ref string strErrMsg)
        {
            if (lstDetFactor != null)
            {
                mtdLoadInfoGridDetFactor(lstDetFactor);
                GridView2.DataSource = lstDetFactor;
                GridView2.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los criterios</param>
        private void mtdLoadInfoGridDetFactor(List<clsDetalleFactorDesempeno> lstDetFactor)
        {
            foreach (clsDetalleFactorDesempeno objDetFactor in lstDetFactor)
            {
                InfoGrid2.Rows.Add(new Object[] {
                    objDetFactor.intId.ToString().Trim(),
                    objDetFactor.strDescripcion.ToString().Trim(),
                    objDetFactor.intIdFactoresDesempeno.ToString().Trim(),
                    objDetFactor.strNombreFactor.ToString().Trim(),
                    objDetFactor.intIdCalificacion.ToString().Trim(),
                    objDetFactor.strNombreCalificacion.ToString().Trim(),
                    objDetFactor.intIdUsuario.ToString().Trim(),
                    objDetFactor.strNombreUsuario.ToString().Trim(),
                    objDetFactor.dtFechaRegistro.ToString().Trim(),
                    objDetFactor.decCriterioMinimo,
                    objDetFactor.decCriterioMaximo
                    });
            }
        }
        #endregion

        private bool mtdLoadDDLCalificacion(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsCalificacionEvaluacion> lstCalificacion = new List<clsCalificacionEvaluacion>();
            clsCalificacionBLL cCalificacion = new clsCalificacionBLL();
            clsEvaluacion objEvaluacion = new clsEvaluacion(1, string.Empty, 0, string.Empty);
            #endregion Vars

            try
            {
                booResult = cCalificacion.mtdConsultarCalificacion(objEvaluacion, ref lstCalificacion, ref strErrMsg);
                ddlCalificacion.Items.Clear();
                ddlCalificacion.Items.Insert(0, new ListItem("", "0"));

                if (booResult)
                {
                    if (lstCalificacion != null)
                    {
                        int intCounter = 1;

                        foreach (clsCalificacionEvaluacion objCalificacion in lstCalificacion)
                        {
                                ddlCalificacion.Items.Insert(intCounter, new ListItem(objCalificacion.strDescripcion, objCalificacion.intId.ToString()));
                                intCounter++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las calificaciones de la evaluación. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }
        #endregion

        private bool mtdInsertarFactor(ref string strErrMsg)
        {
            bool booResult = false;
            clsFactoresDesempeno objFactor = new clsFactoresDesempeno(0,
                tbxNombre.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsFactoresDesempenoBLL cFactor = new clsFactoresDesempenoBLL();

            booResult = cFactor.mtdInsertarFactor(objFactor, ref strErrMsg);

            return booResult;
        }

        private bool mtdModificar()
        {
            bool booResult = true;

            mtdHabilitarCamposFactor(3);

            tbxId.Enabled = false;
            tbxUsuarioCreacion.Enabled = false;
            tbxFecha.Enabled = false;

            tbxNombre.Focus();

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            tbxNombre.Text = InfoGrid1.Rows[RowGrid1][1].ToString().Trim();
            tbxUsuarioCreacion.Text = InfoGrid1.Rows[RowGrid1][3].ToString().Trim();
            tbxFecha.Text = InfoGrid1.Rows[RowGrid1][4].ToString().Trim();

            return booResult;
        }

        private bool mtdActualizarFactor(ref string strErrMsg)
        {
            bool booResult = false;
            clsFactoresDesempeno objFactor = new clsFactoresDesempeno(Convert.ToInt32(tbxId.Text.Trim()),
                tbxNombre.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsFactoresDesempenoBLL cFactor = new clsFactoresDesempenoBLL();

            booResult = cFactor.mtdActualizarFactor(objFactor, ref strErrMsg);

            return booResult;
        }

        private bool mtdDetallesFactor(ref string strErrMsg)
        {
            bool booResult = false;

            tbxId.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            tbxNombre.Text = InfoGrid1.Rows[RowGrid1][1].ToString().Trim();
            clsFactoresDesempeno objFactor = new clsFactoresDesempeno(
                Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                string.Empty, 0, string.Empty);

            mtdHabilitarCamposFactor(4);

            booResult = mtdCargarDetallesFactor(objFactor, ref strErrMsg);

            return booResult;
        }

        private bool mtdDetallesFactor(string strIdFactor, ref string strErrMsg)
        {
            bool booResult = false;
            clsFactoresDesempeno objFactor = new clsFactoresDesempeno(
                Convert.ToInt32(strIdFactor),
                string.Empty, 0, string.Empty);

            mtdHabilitarCamposFactor(4);

            booResult = mtdCargarDetallesFactor(objFactor, ref strErrMsg);

            return booResult;
        }

        private bool mtdInsertarDetFactor(ref string strErrMsg)
        {
            bool booResult = false;

            clsDetalleFactorDesempeno objDetFactor = new clsDetalleFactorDesempeno(0,
                Convert.ToInt32(tbxIdFactorDetFactor.Text.Trim()),
                Convert.ToInt32(1),
                tbxDescripcionDetFactor.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty,
                Convert.ToDecimal(tbxValorMinimo.Text),
                Convert.ToDecimal(tbxValorMaximo.Text)
                );
            clsFactoresDesempenoBLL cFactor = new clsFactoresDesempenoBLL();

            booResult = cFactor.mtdInsertarDetFactor(objDetFactor, ref strErrMsg);

            return booResult;
        }

        private bool mtdModificarDetFactor()
        {
            bool booResult = true;
            string strErrMsg = string.Empty;

            /*if (!mtdLoadDDLCalificacion(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");*/

            mtdHabilitarCamposFactor(6);
            
            tbxCodigoDetFactor.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
            tbxDescripcionDetFactor.Text = InfoGrid2.Rows[RowGrid2][1].ToString().Trim();

            tbxIdFactorDetFactor.Text = InfoGrid2.Rows[RowGrid2][2].ToString().Trim();
            tbxNombreFactorDetFactor.Text = InfoGrid2.Rows[RowGrid2][3].ToString().Trim();

            //ddlCalificacion.SelectedValue = InfoGrid2.Rows[RowGrid2][4].ToString().Trim();
            Session["IdConfig"] = InfoGrid2.Rows[RowGrid2][4].ToString().Trim();

            tbxUsuarioCreacionDetFactor.Text = InfoGrid2.Rows[RowGrid2][7].ToString().Trim();
            tbxFechaDetFactor.Text = InfoGrid2.Rows[RowGrid2][8].ToString().Trim();

            tbxValorMinimo.Text = InfoGrid2.Rows[RowGrid2][9].ToString().Trim();
            tbxValorMaximo.Text = InfoGrid2.Rows[RowGrid2][10].ToString().Trim();

            tbxCodigoDetFactor.Enabled = false;
            tbxNombreFactorDetFactor.Enabled = false;
            tbxUsuarioCreacionDetFactor.Enabled = false;
            tbxFechaDetFactor.Enabled = false;

            return booResult;
        }

        private bool mtdActualizarDetFactor(ref string strErrMsg)
        {
            bool booResult = false;
            clsDetalleFactorDesempeno objDetFactor = new clsDetalleFactorDesempeno();
            objDetFactor.intId = Convert.ToInt32(tbxCodigoDetFactor.Text.Trim());
            objDetFactor.intIdCalificacion = Convert.ToInt32(Session["IdConfig"].ToString().Trim());
            objDetFactor.strDescripcion = tbxDescripcionDetFactor.Text;
            objDetFactor.strNombreFactor = tbxNombreFactorDetFactor.Text;
            objDetFactor.decCriterioMinimo = Convert.ToDecimal(tbxValorMinimo.Text);
            objDetFactor.decCriterioMaximo = Convert.ToDecimal(tbxValorMaximo.Text);
            clsFactoresDesempenoBLL cFactor = new clsFactoresDesempenoBLL();

            booResult = cFactor.mtdActualizarDetFactor(objDetFactor, ref strErrMsg);

            return booResult;
        }

        #endregion

        
    }
}