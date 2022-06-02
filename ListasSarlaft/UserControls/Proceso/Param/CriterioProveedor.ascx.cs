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
    public partial class CriterioProveedor : System.Web.UI.UserControl
    {
        string IdFormulario = "4017";
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;
        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;
        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

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

        private DataTable InfoGrid3
        {
            get
            {
                infoGrid3 = (DataTable)ViewState["infoGrid3"];
                return infoGrid3;
            }
            set
            {
                infoGrid3 = value;
                ViewState["infoGrid3"] = infoGrid3;
            }
        }

        private int RowGrid3
        {
            get
            {
                rowGrid3 = (int)ViewState["rowGrid3"];
                return rowGrid3;
            }
            set
            {
                rowGrid3 = value;
                ViewState["rowGrid3"] = rowGrid3;
            }
        }

        private int PagIndex3
        {
            get
            {
                pagIndex3 = (int)ViewState["pagIndex3"];
                return pagIndex3;
            }
            set
            {
                pagIndex3 = value;
                ViewState["pagIndex3"] = pagIndex3;
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
                    mtdInicializarValores();
                    if (!mtdCargarAspectos(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                }
            }
        }

        #region Buttons
        #region Aspectos
        protected void btnNuevoAspecto_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                txtNombreAspecto.Focus();

                mtdLimpiarCamposAspecto();
                mtdHabilitarCamposProveedor(2);

                tbxIdAspecto.Enabled = false;
                tbxUsuarioAspecto.Enabled = false;
                tbxFechaAspecto.Enabled = false;
            }
        }

        protected void btnInsertarAspectos_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarAspecto(ref strErrMsg))
                {
                    omb.ShowMessage("Aspecto de la evaluación de proveedores registrado exitosamente.", 3, "Atención");
                    mtdLimpiarCamposAspecto();
                    mtdHabilitarCamposProveedor(1);
                    mtdCargarAspectos(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar el Aspecto de la evaluación de proveedores. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnActualizarAspectos_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    if (mtdActualizarAspecto(ref strErrMsg))
                    {
                        omb.ShowMessage("Aspecto de la evaluación de proveedores modificado exitosamente.", 3, "Atención");
                        mtdLimpiarCamposAspecto();
                        mtdHabilitarCamposProveedor(1);
                        mtdCargarAspectos(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el Aspecto de la evaluación de proveedores.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarAspectos_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposAspecto();
            mtdHabilitarCamposProveedor(1);
        }
        #endregion

        #region Criterios
        protected void btnNuevoCriterio_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                tbxDescCriterio.Focus();

                mtdLimpiarCamposCriterios();
                mtdHabilitarCamposProveedor(5);

                tbxCodigoCriterio.Enabled = false;
                tbxNombreAspectoCriterio.Enabled = false;
                tbxUsuarioCriterio.Enabled = false;
                tbxFechaCriterio.Enabled = false;

                tbxNombreAspectoCriterio.Text = txtNombreAspecto.Text;
            }
        }

        protected void btnInsertarCri_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarCriterio(ref strErrMsg))
                {
                    omb.ShowMessage("Criterio registrado exitosamente.", 3, "Atención");
                    mtdLimpiarCamposCriterios();
                    mtdCriterios(tbxIdAspecto.Text.Trim(), ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al registrar el Criterio. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnActualizarCri_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    if (mtdActualizarCriterio(ref strErrMsg))
                    {
                        omb.ShowMessage("Criterio modificado exitosamente.", 3, "Atención");
                        mtdLimpiarCamposCriterios();
                        mtdCriterios(tbxIdAspecto.Text.Trim(), ref strErrMsg);
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

        protected void btnCancelarCri_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposCriterios();
            mtdHabilitarCamposProveedor(4);
        }

        protected void btnRegresarAspectos_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposAspecto();
            mtdLimpiarCamposCriterios();
            mtdHabilitarCamposProveedor(1);
        }
        #endregion

        #region Parametros
        protected void btnNuevoParametro_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                tbxDescCriterio.Focus();

                mtdLimpiarCamposParametro();
                mtdHabilitarCamposProveedor(8);

                tbxIdParametro.Enabled = false;
                tbxDescCriterioParam.Enabled = false;
                tbxUsuarioParametro.Enabled = false;
                tbxFechaParametro.Enabled = false;

                tbxDescCriterioParam.Text = tbxDescCriterio.Text;
            }
        }

        protected void btnInsertarParametro_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarParametro(ref strErrMsg))
                {
                    omb.ShowMessage("Parámetro registrado exitosamente.", 3, "Atención");
                    mtdLimpiarCamposParametro();
                    mtdParametros(tbxCodigoCriterio.Text.Trim(), ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al registrar el Parámetro. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnActualizarParametro_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    if (mtdActualizarParametro(ref strErrMsg))
                    {
                        omb.ShowMessage("Parámetro modificado exitosamente.", 3, "Atención");
                        mtdLimpiarCamposParametro();
                        //mtdCriterios(tbxCodigoCriterio.Text.Trim(), ref strErrMsg);
                        mtdParametros(tbxCodigoCriterio.Text.Trim(), ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el Parámetro. <br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarParametro_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposParametro();
            mtdHabilitarCamposProveedor(7);
        }

        protected void btnRegresarCriterios_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposCriterios();
            mtdLimpiarCamposParametro();
            mtdHabilitarCamposProveedor(4);
        }
        #endregion

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
                case "Criterios":
                    if (!mtdCriterios(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
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
                    mtdModificarCriterio();
                    break;

                case "Parametros":
                    if (!mtdParametros(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid3 = (Convert.ToInt16(GridView3.PageSize) * PagIndex3) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificarParametro();
                    break;

            }
        }
        #endregion

        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
            PagIndex3 = 0;
        }

        private void mtdHabilitarCamposProveedor(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1://Grid Aspecto
                    #region
                    filaGridAspectos.Visible = true;
                    trTituloAspectos.Visible = false;
                    filaDetalleAspectos.Visible = false;

                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresarCriterios.Visible = false;

                    trTituloParametros.Visible = false;
                    FilaGridParametros.Visible = false;
                    FilaDetParametros.Visible = false;
                    BotonRegresarParametros.Visible = false;
                    #endregion
                    break;
                case 2: //Nuevo Aspecto
                    #region
                    filaGridAspectos.Visible = false;
                    trTituloAspectos.Visible = true;
                    filaDetalleAspectos.Visible = true;

                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresarCriterios.Visible = false;

                    trTituloParametros.Visible = false;
                    FilaGridParametros.Visible = false;
                    FilaDetParametros.Visible = false;
                    BotonRegresarParametros.Visible = false;

                    btnInsertarAspectos.Visible = true;
                    btnActualizarAspectos.Visible = false;
                    btnCancelarAspectos.Visible = true;
                    #endregion
                    break;
                case 3: //Modificar Aspecto
                    #region
                    filaGridAspectos.Visible = false;
                    trTituloAspectos.Visible = true;
                    filaDetalleAspectos.Visible = true;

                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresarCriterios.Visible = false;

                    trTituloParametros.Visible = false;
                    FilaGridParametros.Visible = false;
                    FilaDetParametros.Visible = false;
                    BotonRegresarParametros.Visible = false;

                    btnInsertarAspectos.Visible = false;
                    btnActualizarAspectos.Visible = true;
                    btnCancelarAspectos.Visible = true;
                    #endregion
                    break;
                case 4: //Grid Criterio
                    #region
                    filaGridAspectos.Visible = false;
                    trTituloAspectos.Visible = false;
                    filaDetalleAspectos.Visible = false;

                    trTituloCriterios.Visible = true;
                    FilaGridCriterios.Visible = true;
                    DetalleCriterios.Visible = false;
                    BotonRegresarCriterios.Visible = true;

                    trTituloParametros.Visible = false;
                    FilaGridParametros.Visible = false;
                    FilaDetParametros.Visible = false;
                    BotonRegresarParametros.Visible = false;

                    btnInsertarAspectos.Visible = false;
                    btnActualizarAspectos.Visible = false;
                    btnCancelarAspectos.Visible = false;
                    #endregion
                    break;
                case 5: //Nuevo Criterio
                    #region
                    filaGridAspectos.Visible = false;
                    trTituloAspectos.Visible = false;
                    filaDetalleAspectos.Visible = false;

                    trTituloCriterios.Visible = true;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = true;
                    BotonRegresarCriterios.Visible = true;

                    trTituloParametros.Visible = false;
                    FilaGridParametros.Visible = false;
                    FilaDetParametros.Visible = false;
                    BotonRegresarParametros.Visible = false;

                    btnInsertarCri.Visible = true;
                    btnActualizarCri.Visible = false;
                    btnCancelarCri.Visible = true;
                    #endregion
                    break;
                case 6://Modificar Criterio
                    #region
                    filaGridAspectos.Visible = false;
                    trTituloAspectos.Visible = false;
                    filaDetalleAspectos.Visible = false;

                    trTituloCriterios.Visible = true;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = true;
                    BotonRegresarCriterios.Visible = true;

                    trTituloParametros.Visible = false;
                    FilaGridParametros.Visible = false;
                    FilaDetParametros.Visible = false;
                    BotonRegresarParametros.Visible = false;

                    btnInsertarCri.Visible = false;
                    btnActualizarCri.Visible = true;
                    btnCancelarCri.Visible = true;
                    #endregion
                    break;
                case 7://Grid Param
                    #region
                    filaGridAspectos.Visible = false;
                    trTituloAspectos.Visible = false;
                    filaDetalleAspectos.Visible = false;

                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresarCriterios.Visible = false;

                    trTituloParametros.Visible = true;
                    FilaGridParametros.Visible = true;
                    FilaDetParametros.Visible = false;
                    BotonRegresarParametros.Visible = true;

                    btnInsertarCri.Visible = false;
                    btnActualizarCri.Visible = false;
                    btnCancelarCri.Visible = false;
                    #endregion
                    break;
                case 8: //Nuevo Param
                    #region
                    filaGridAspectos.Visible = false;
                    trTituloAspectos.Visible = false;
                    filaDetalleAspectos.Visible = false;

                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresarCriterios.Visible = false;

                    trTituloParametros.Visible = true;
                    FilaGridParametros.Visible = false;
                    FilaDetParametros.Visible = true;
                    BotonRegresarParametros.Visible = true;

                    btnInsertarParametro.Visible = true;
                    btnActualizarParametro.Visible = false;
                    btnCancelarParametro.Visible = true;
                    #endregion
                    break;
                case 9://Modificar Param
                    #region
                    filaGridAspectos.Visible = false;
                    trTituloAspectos.Visible = false;
                    filaDetalleAspectos.Visible = false;

                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresarCriterios.Visible = false;

                    trTituloParametros.Visible = true;
                    FilaGridParametros.Visible = false;
                    FilaDetParametros.Visible = true;
                    BotonRegresarParametros.Visible = true;

                    btnInsertarParametro.Visible = false;
                    btnActualizarParametro.Visible = true;
                    btnCancelarParametro.Visible = true;
                    #endregion
                    break;
            }
        }

        #region Limpiar Campos
        private void mtdLimpiarCamposAspecto()
        {
            tbxIdAspecto.Text = string.Empty;
            txtNombreAspecto.Text = string.Empty;
            tbxValorAspecto.Text = string.Empty;
            tbxUsuarioAspecto.Text = string.Empty;
            tbxFechaAspecto.Text = string.Empty;
        }

        private void mtdLimpiarCamposCriterios()
        {
            tbxCodigoCriterio.Text = string.Empty;
            tbxDescCriterio.Text = string.Empty;
            tbxNombreAspectoCriterio.Text = string.Empty;
            tbxUsuarioCriterio.Text = string.Empty;
            tbxFechaCriterio.Text = string.Empty;
        }

        private void mtdLimpiarCamposParametro()
        {
            tbxIdParametro.Text = string.Empty;
            tbxDescParametro.Text = string.Empty;
            tbxDescCriterioParam.Text = string.Empty;
            tbxUsuarioParametro.Text = string.Empty;
            tbxFechaParametro.Text = string.Empty;
        }
        #endregion

        #region Cargas
        #region Gridview Aspectos
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarAspectos(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridAspectos();
            booResult = mtdLoadInfoGridAspectos(ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridAspectos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strAspecto", typeof(string));
            grid.Columns.Add("intValor", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid1 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la Competencia al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridAspectos(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsAspectoProveedor> lstAspecto = new List<clsAspectoProveedor>();
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdConsultarAspecto(ref lstAspecto, ref strErrMsg);

            if (booResult)
                if (lstAspecto != null)
                {
                    mtdLoadInfoGridAspectos(lstAspecto);
                    GridView1.DataSource = lstAspecto;
                    GridView1.DataBind();
                }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con las Aspectos</param>
        private void mtdLoadInfoGridAspectos(List<clsAspectoProveedor> lstAspecto)
        {
            foreach (clsAspectoProveedor objAspecto in lstAspecto)
            {
                InfoGrid1.Rows.Add(new Object[] {
                    objAspecto.intId.ToString().Trim(),
                    objAspecto.strAspecto.ToString().Trim(),
                    objAspecto.decValor.ToString().Trim(),
                    objAspecto.intIdUsuario.ToString().Trim(),
                    objAspecto.strNombreUsuario.ToString().Trim(),
                    objAspecto.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview Criterios
        private bool mtdCargarCriterios(clsAspectoProveedor objAspecto, ref string strErrMsg)
        {
            bool booResult = false;
            List<clsCriterioProveedor> lstCriterio = new List<clsCriterioProveedor>();
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdConsultarCriterio(objAspecto, ref lstCriterio, ref strErrMsg);

            if (booResult)
            {
                mtdLoadGridCriterios();
                mtdLoadInfoGridCriterio(lstCriterio, ref strErrMsg);
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridCriterios()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("intIdAspecto", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGrid2 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la Competencia al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridCriterio(List<clsCriterioProveedor> lstCriterio, ref string strErrMsg)
        {
            if (lstCriterio != null)
            {
                mtdLoadInfoGridCriterio(lstCriterio);
                GridView2.DataSource = lstCriterio;
                GridView2.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los criterios</param>
        private void mtdLoadInfoGridCriterio(List<clsCriterioProveedor> lstCriterio)
        {
            foreach (clsCriterioProveedor objCriterio in lstCriterio)
            {
                InfoGrid2.Rows.Add(new Object[] {
                    objCriterio.intId.ToString().Trim(),
                    objCriterio.strDescripcion.ToString().Trim(),
                    objCriterio.intIdAspecto.ToString().Trim(),
                    objCriterio.intIdUsuario.ToString().Trim(),
                    objCriterio.strNombreUsuario.ToString().Trim(),
                    objCriterio.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview Parametros
        private bool mtdCargarParametros(clsCriterioProveedor objCriterio, ref string strErrMsg)
        {
            bool booResult = false;
            List<clsParametros> lstParametro = new List<clsParametros>();
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdConsultarParametros(objCriterio, ref lstParametro, ref strErrMsg);

            if (booResult)
            {
                mtdLoadGridParametros();
                mtdLoadInfoGridParametros(lstParametro, ref strErrMsg);
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridParametros()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcionParametro", typeof(string));
            grid.Columns.Add("intIdCriterioProveedor", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGrid3 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridParametros(List<clsParametros> lstParametro, ref string strErrMsg)
        {
            if (lstParametro != null)
            {
                mtdLoadInfoGridParametros(lstParametro);
                GridView3.DataSource = lstParametro;
                GridView3.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los Parametros</param>
        private void mtdLoadInfoGridParametros(List<clsParametros> lstParametro)
        {
            foreach (clsParametros objParam in lstParametro)
            {
                InfoGrid3.Rows.Add(new Object[] {
                    objParam.intId.ToString().Trim(),
                    objParam.strDescripcionParametro.ToString().Trim(),
                    objParam.intIdCriterioProveedor.ToString().Trim(),
                    objParam.intIdUsuario.ToString().Trim(),
                    objParam.strNombreUsuario.ToString().Trim(),
                    objParam.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion

        #region Aspectos
        private bool mtdInsertarAspecto(ref string strErrMsg)
        {
            bool booResult = false;
            decimal decPonderado = 0;
            clsAspectoProveedor objAspecto = new clsAspectoProveedor(0,
                txtNombreAspecto.Text.Trim(),
                Convert.ToInt32(tbxValorAspecto.Text.Trim()),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdSumatoriaPonderado(ref decPonderado, ref strErrMsg);
            if (booResult)
            {
                decimal decPonderadoTemp = decPonderado + Convert.ToDecimal(tbxValorAspecto.Text.Trim());

                if (decPonderadoTemp <= 100)
                    booResult = cProveedor.mtdInsertarAspecto(objAspecto, ref strErrMsg);
                else
                {
                    booResult = false;
                    strErrMsg = string.Format("La suma de los ponderados de los aspectos debe ser menor o igual que 100 [Suma Ponderado = {0}]",
                        decPonderadoTemp);
                }
            }

            return booResult;
        }

        private bool mtdModificar()
        {
            bool booResult = true;

            mtdHabilitarCamposProveedor(3);

            tbxIdAspecto.Enabled = false;
            tbxUsuarioAspecto.Enabled = false;
            tbxFechaAspecto.Enabled = false;

            txtNombreAspecto.Focus();

            // Carga los datos en la respectiva caja de texto
            tbxIdAspecto.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            txtNombreAspecto.Text = InfoGrid1.Rows[RowGrid1][1].ToString().Trim();
            tbxValorAspecto.Text = InfoGrid1.Rows[RowGrid1][2].ToString().Trim();
            tbxUsuarioAspecto.Text = InfoGrid1.Rows[RowGrid1][4].ToString().Trim();
            tbxFechaAspecto.Text = InfoGrid1.Rows[RowGrid1][5].ToString().Trim();

            return booResult;
        }

        private bool mtdActualizarAspecto(ref string strErrMsg)
        {
            bool booResult = false;
            decimal decPonderado = 0;
            clsAspectoProveedor objAspecto = new clsAspectoProveedor();
            objAspecto.intId =    Convert.ToInt32(tbxIdAspecto.Text.Trim());
            objAspecto.strAspecto =   txtNombreAspecto.Text.Trim();
            //string valorAspescto = clsUtilidades.mtdQuitarComasAPuntos("" + tbxValorAspecto.Text.Trim());
            decimal valor = Convert.ToDecimal(tbxValorAspecto.Text.Trim());
            objAspecto.decValor = valor;
            objAspecto.intIdUsuario =   0;
            objAspecto.dtFechaRegistro = string.Empty;
            clsAspectoProveedor objAspectoComparar = new clsAspectoProveedor(); 
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdConsultarAspecto(objAspecto, ref objAspectoComparar, ref strErrMsg);

            if (booResult)
            {
                booResult = cProveedor.mtdSumatoriaPonderado(ref decPonderado, ref strErrMsg);
                if (booResult)
                {
                    decimal decPonderadoTemp = decPonderado + Convert.ToDecimal(tbxValorAspecto.Text.Trim()) - objAspectoComparar.decValor;
                    if (decPonderadoTemp <= 100)
                        booResult = cProveedor.mtdActualizarAspecto(objAspecto, ref strErrMsg);
                    else
                    {
                        booResult = false;
                        strErrMsg = string.Format("La suma de los ponderados de los Aspectos debe ser menor o igual que 100 [Suma Ponderado = {0}]",
                            decPonderadoTemp);
                    }
                }
            }

            return booResult;
        }
        #endregion

        #region Criterios
        private bool mtdCriterios(ref string strErrMsg)
        {
            bool booResult = false;
            clsAspectoProveedor objAspecto = new clsAspectoProveedor(
                Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                string.Empty, 0, 0, string.Empty);

            tbxIdAspecto.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            txtNombreAspecto.Text = InfoGrid1.Rows[RowGrid1][1].ToString().Trim();

            mtdHabilitarCamposProveedor(4);

            booResult = mtdCargarCriterios(objAspecto, ref strErrMsg);

            return booResult;
        }

        private bool mtdCriterios(string strIdAspecto, ref string strErrMsg)
        {
            bool booResult = false;
            clsAspectoProveedor objAspecto = new clsAspectoProveedor(
                Convert.ToInt32(strIdAspecto),
                string.Empty, 0, 0, string.Empty);

            mtdHabilitarCamposProveedor(4);

            booResult = mtdCargarCriterios(objAspecto, ref strErrMsg);

            return booResult;
        }

        private bool mtdInsertarCriterio(ref string strErrMsg)
        {
            bool booResult = false;
            clsCriterioProveedor objCriterio = new clsCriterioProveedor(0,
                Convert.ToInt32(tbxIdAspecto.Text.Trim()),
                tbxDescCriterio.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdInsertarCriterio(objCriterio, ref strErrMsg);

            return booResult;
        }

        private bool mtdModificarCriterio()
        {
            bool booResult = true;

            mtdHabilitarCamposProveedor(6);

            tbxCodigoCriterio.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
            tbxDescCriterio.Text = InfoGrid2.Rows[RowGrid2][1].ToString().Trim();
            tbxNombreAspectoCriterio.Text = txtNombreAspecto.Text;
            tbxUsuarioCriterio.Text = InfoGrid2.Rows[RowGrid2][4].ToString().Trim();
            tbxFechaCriterio.Text = InfoGrid2.Rows[RowGrid2][5].ToString().Trim();

            tbxNombreAspectoCriterio.Enabled = false;
            tbxUsuarioCriterio.Enabled = false;
            tbxFechaCriterio.Enabled = false;

            return booResult;
        }

        private bool mtdActualizarCriterio(ref string strErrMsg)
        {
            bool booResult = false;
            clsCriterioProveedor objCriterio = new clsCriterioProveedor(
                Convert.ToInt32(tbxCodigoCriterio.Text.Trim()),
                Convert.ToInt32(tbxIdAspecto.Text.Trim()),
                tbxDescCriterio.Text.Trim(),
                0, string.Empty);
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdActualizarCriterio(objCriterio, ref strErrMsg);

            return booResult;
        }
        #endregion

        #region Parametros
        private bool mtdParametros(ref string strErrMsg)
        {
            bool booResult = false;

            clsCriterioProveedor objCriterio = new clsCriterioProveedor(
                Convert.ToInt32(InfoGrid2.Rows[RowGrid2][0].ToString().Trim()),
                0, string.Empty, 0, string.Empty);

            tbxCodigoCriterio.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
            tbxDescCriterio.Text = InfoGrid2.Rows[RowGrid2][1].ToString().Trim();

            mtdHabilitarCamposProveedor(7);

            booResult = mtdCargarParametros(objCriterio, ref strErrMsg);

            return booResult;
        }

        private bool mtdParametros(string strIdCriterio, ref string strErrMsg)
        {
            bool booResult = false;

            clsCriterioProveedor objCriterio = new clsCriterioProveedor(
                Convert.ToInt32(strIdCriterio),
                0, string.Empty, 0, string.Empty);

            mtdHabilitarCamposProveedor(7);

            booResult = mtdCargarParametros(objCriterio, ref strErrMsg);

            return booResult;
        }

        private bool mtdInsertarParametro(ref string strErrMsg)
        {
            bool booResult = false;
            clsParametros objParametro = new clsParametros(0,
                tbxDescParametro.Text.Trim(),
                Convert.ToInt32(tbxCodigoCriterio.Text.Trim()),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdInsertarParametro(objParametro, ref strErrMsg);

            return booResult;
        }

        private bool mtdModificarParametro()
        {
            bool booResult = true;

            mtdHabilitarCamposProveedor(9);

            tbxIdParametro.Text = InfoGrid3.Rows[RowGrid3][0].ToString().Trim();
            tbxDescParametro.Text = InfoGrid3.Rows[RowGrid3][1].ToString().Trim();
            tbxDescCriterioParam.Text = tbxDescCriterio.Text.Trim();
            tbxUsuarioParametro.Text = InfoGrid3.Rows[RowGrid3][4].ToString().Trim();
            tbxFechaParametro.Text = InfoGrid3.Rows[RowGrid3][5].ToString().Trim();

            tbxDescCriterioParam.Enabled = false;
            tbxUsuarioParametro.Enabled = false;
            tbxFechaParametro.Enabled = false;

            return booResult;
        }

        private bool mtdActualizarParametro(ref string strErrMsg)
        {
            bool booResult = false;
            clsParametros objParametro = new clsParametros(
                Convert.ToInt32(tbxIdParametro.Text.Trim()),
                tbxDescParametro.Text.Trim(),
                Convert.ToInt32(tbxCodigoCriterio.Text.Trim()),
                0, string.Empty);
            clsCriterioProveedorBLL cProveedor = new clsCriterioProveedorBLL();

            booResult = cProveedor.mtdActualizarParametro(objParametro, ref strErrMsg);

            return booResult;
        }
        #endregion

        #endregion
    }
}