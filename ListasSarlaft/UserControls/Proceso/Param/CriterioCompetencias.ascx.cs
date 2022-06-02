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
    public partial class CriterioCompetencias : System.Web.UI.UserControl
    {
        string IdFormulario = "4014";
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
                    mtdInicializarValores();
                    if (mtdCargarCompetencias(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                }
            }
        }

        #region Buttons
        #region Competencia
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                tbxNombre.Focus();

                mtdLimpiarCamposCompetencia();
                mtdHabilitarCamposCompetencia(1);

                tbxId.Enabled = false;
                tbxUsuarioCreacion.Enabled = false;
                tbxFecha.Enabled = false;
            }
        }

        protected void btnInsertarComp_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarCompetencia(ref strErrMsg))
                {
                    omb.ShowMessage("Competencia registrada exitosamente.", 3, "Atención");
                    mtdLimpiarCamposCompetencia();
                    mtdHabilitarCamposCompetencia(2);
                    mtdCargarCompetencias(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar la actividad. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnActualizarComp_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    if (mtdActualizarCompetencia(ref strErrMsg))
                    {
                        omb.ShowMessage("Competencia modificada exitosamente.", 3, "Atención");
                        mtdLimpiarCamposCompetencia();
                        mtdHabilitarCamposCompetencia(2);
                        mtdCargarCompetencias(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar la Competencia.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarComp_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposCompetencia();
            mtdHabilitarCamposCompetencia(2);
        }
        #endregion

        #region Criterios
        protected void btnInsertarNuevoCri_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                tbxDescripcionCri.Focus();

                mtdLimpiarCamposCriterios();
                mtdHabilitarCamposCompetencia(5);

                tbxCodigoCriterio.Enabled = false;
                tbxCompetenciaCri.Enabled = false;
                tbxUsuarioCreacionCri.Enabled = false;
                tbxFechaCri.Enabled = false;

                tbxCompetenciaCri.Text = tbxNombre.Text;
                tbxIdCompetenciaCri.Text = tbxId.Text;
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
                    mtdCriterios(tbxId.Text.Trim(), ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
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
                        mtdCriterios(tbxId.Text.Trim(), ref strErrMsg);
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
            mtdHabilitarCamposCompetencia(4);
        }
        #endregion

        protected void btnRegresarCompetencias_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposCompetencia();
            mtdLimpiarCamposCriterios();
            mtdHabilitarCamposCompetencia(2);
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
            }
        }
        #endregion

        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
        }

        #region Limpiar Campos
        private void mtdLimpiarCamposCompetencia()
        {
            tbxId.Text = string.Empty;
            tbxNombre.Text = string.Empty;
            tbxPonderacion.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }

        private void mtdLimpiarCamposCriterios()
        {
            tbxCodigoCriterio.Text = string.Empty;
            tbxDescripcionCri.Text = string.Empty;
            tbxIdCompetenciaCri.Text = string.Empty;
            tbxCompetenciaCri.Text = string.Empty;
            tbxUsuarioCreacionCri.Text = string.Empty;
            tbxFechaCri.Text = string.Empty;
        }
        #endregion

        private void mtdHabilitarCamposCompetencia(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1://Editar Competencia (insertar)
                    #region
                    filaGridCompetencias.Visible = false;
                    trTituloCompetencias.Visible = true;
                    filaDetalleCompetencias.Visible = true;
                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresar.Visible = false;
                    btnInsertarComp.Visible = true;
                    btnActualizarComp.Visible = false;
                    btnCancelarComp.Visible = true;
                    #endregion
                    break;
                case 2: //Ver Grid Competencia
                    #region
                    filaGridCompetencias.Visible = true;
                    trTituloCompetencias.Visible = false;
                    filaDetalleCompetencias.Visible = false;
                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresar.Visible = false;
                    btnInsertarComp.Visible = false;
                    btnActualizarComp.Visible = false;
                    btnCancelarComp.Visible = false;
                    #endregion
                    break;
                case 3://Editar Competencia (Actualizar)
                    #region
                    filaGridCompetencias.Visible = false;
                    trTituloCompetencias.Visible = true;
                    filaDetalleCompetencias.Visible = true;
                    trTituloCriterios.Visible = false;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = false;
                    BotonRegresar.Visible = false;
                    btnInsertarComp.Visible = false;
                    btnActualizarComp.Visible = true;
                    btnCancelarComp.Visible = true;
                    #endregion
                    break;
                case 4: //Ver Grid Criterios
                    #region
                    filaGridCompetencias.Visible = false;
                    trTituloCompetencias.Visible = false;
                    filaDetalleCompetencias.Visible = false;
                    trTituloCriterios.Visible = true;
                    FilaGridCriterios.Visible = true;
                    DetalleCriterios.Visible = false;
                    BotonRegresar.Visible = true;
                    btnInsertarComp.Visible = false;
                    btnActualizarComp.Visible = false;
                    btnCancelarComp.Visible = false;
                    #endregion
                    break;
                case 5://Editar Criterios (insertar)
                    #region
                    filaGridCompetencias.Visible = false;
                    trTituloCompetencias.Visible = false;
                    filaDetalleCompetencias.Visible = false;
                    trTituloCriterios.Visible = true;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = true;
                    BotonRegresar.Visible = true;
                    btnInsertarCri.Visible = true;
                    btnActualizarCri.Visible = false;
                    btnCancelarCri.Visible = true;
                    #endregion
                    break;
                case 6://Editar Criterios (Actualizar)
                    #region
                    filaGridCompetencias.Visible = false;
                    trTituloCompetencias.Visible = false;
                    filaDetalleCompetencias.Visible = false;
                    trTituloCriterios.Visible = true;
                    FilaGridCriterios.Visible = false;
                    DetalleCriterios.Visible = true;
                    BotonRegresar.Visible = true;
                    btnInsertarCri.Visible = false;
                    btnActualizarCri.Visible = true;
                    btnCancelarCri.Visible = true;
                    #endregion
                    break;
            }
        }

        #region Cargas
        #region Gridview Competencias
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarCompetencias(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridCompetencias();
            mtdLoadInfoGridCompetencias(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridCompetencias()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombre", typeof(string));
            grid.Columns.Add("intPonderacion", typeof(string));
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
        private void mtdLoadInfoGridCompetencias(ref string strErrMsg)
        {
            List<clsCompetencia> lstCompetencia = new List<clsCompetencia>();
            clsCompetenciaBLL cCompetencia = new clsCompetenciaBLL();

            lstCompetencia = cCompetencia.mtdConsultarCompetencia(ref strErrMsg);

            if (lstCompetencia != null)
            {
                mtdLoadInfoGridCompetencia(lstCompetencia);
                GridView1.DataSource = lstCompetencia;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con las Actividades</param>
        private void mtdLoadInfoGridCompetencia(List<clsCompetencia> lstCompetencia)
        {
            foreach (clsCompetencia objCompetencia in lstCompetencia)
            {
                InfoGrid1.Rows.Add(new Object[] {
                    objCompetencia.intId.ToString().Trim(),
                    objCompetencia.strNombre.ToString().Trim(),
                    objCompetencia.intPonderacion.ToString().Trim(),
                    objCompetencia.intIdUsuario.ToString().Trim(),
                    objCompetencia.strNombreUsuario.ToString().Trim(),
                    objCompetencia.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview Criterios
        private bool mtdCargarCriterios(clsCompetencia objCompetencia, ref string strErrMsg)
        {
            bool booResult = false;
            List<clsCriterioCompetencia> lstCriterio = new List<clsCriterioCompetencia>();
            clsCompetenciaBLL cCompetencia = new clsCompetenciaBLL();

            booResult = cCompetencia.mtdConsultarCriterio(objCompetencia, ref lstCriterio, ref strErrMsg);

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
            grid.Columns.Add("intIdCompetencia", typeof(string));
            grid.Columns.Add("strNombreCompetencia", typeof(string));
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
        private void mtdLoadInfoGridCriterio(List<clsCriterioCompetencia> lstCriterio, ref string strErrMsg)
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
        private void mtdLoadInfoGridCriterio(List<clsCriterioCompetencia> lstCriterio)
        {
            foreach (clsCriterioCompetencia objCriterio in lstCriterio)
            {
                InfoGrid2.Rows.Add(new Object[] {
                    objCriterio.intId.ToString().Trim(),
                    objCriterio.strDescripcion.ToString().Trim(),
                    objCriterio.intIdCompetencia.ToString().Trim(),
                    objCriterio.strNombreCompetencia.ToString().Trim(),
                    objCriterio.intIdUsuario.ToString().Trim(),
                    objCriterio.strNombreUsuario.ToString().Trim(),
                    objCriterio.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion

        #region Competencia
        private bool mtdInsertarCompetencia(ref string strErrMsg)
        {
            bool booResult = false;
            int intPonderado = 0;
            clsCompetencia objCompetencia = new clsCompetencia(0,
                Convert.ToInt32(tbxPonderacion.Text.Trim()),
                tbxNombre.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsCompetenciaBLL cCompetencia = new clsCompetenciaBLL();

            booResult = cCompetencia.mtdSumatoriaPonderado(ref intPonderado, ref strErrMsg);
            if (booResult)
            {
                int intPonderadoTemp = intPonderado + Convert.ToInt32(tbxPonderacion.Text.Trim());

                if (intPonderadoTemp <= 100)
                    booResult = cCompetencia.mtdInsertarCompetencia(objCompetencia, ref strErrMsg);
                else
                {
                    booResult = false;
                    strErrMsg = string.Format("La suma de los ponderados de las competencias debe ser menor o igual que 100 [Suma Ponderado = {0}]",
                        intPonderadoTemp);
                }
            }

            return booResult;
        }

        private bool mtdModificar()
        {
            bool booResult = true;

            mtdHabilitarCamposCompetencia(3);

            tbxId.Enabled = false;
            tbxUsuarioCreacion.Enabled = false;
            tbxFecha.Enabled = false;

            tbxNombre.Focus();

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            tbxNombre.Text = InfoGrid1.Rows[RowGrid1][1].ToString().Trim();
            tbxPonderacion.Text = InfoGrid1.Rows[RowGrid1][2].ToString().Trim();
            tbxUsuarioCreacion.Text = InfoGrid1.Rows[RowGrid1][4].ToString().Trim();
            tbxFecha.Text = InfoGrid1.Rows[RowGrid1][5].ToString().Trim();

            return booResult;
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarCompetencia(ref string strErrMsg)
        {
            bool booResult = false;
            int intPonderado = 0;
            clsCompetencia objCompetencia = new clsCompetencia(
                 Convert.ToInt32(tbxId.Text.Trim()),
                 Convert.ToInt32(tbxPonderacion.Text.Trim()),
                 tbxNombre.Text.Trim(),
                 0, string.Empty), objCompetenciaComparar = new clsCompetencia();
            clsCompetenciaBLL cCompetencia = new clsCompetenciaBLL();

            booResult = cCompetencia.mtdConsultarCompetencia(objCompetencia, ref  objCompetenciaComparar, ref strErrMsg);

            if (booResult)
            {
                booResult = cCompetencia.mtdSumatoriaPonderado(ref intPonderado, ref strErrMsg);
                if (booResult)
                {
                    int intPonderadoTemp = intPonderado + Convert.ToInt32(tbxPonderacion.Text.Trim()) - objCompetenciaComparar.intPonderacion;
                    if (intPonderadoTemp <= 100)
                        booResult = cCompetencia.mtdActualizarCompetencia(objCompetencia, ref strErrMsg);
                    else
                    {
                        booResult = false;
                        strErrMsg = string.Format("La suma de los ponderados de las competencias debe ser menor o igual que 100 [Suma Ponderado = {0}]",
                            intPonderadoTemp);
                    }
                }
            }
            return booResult;
        }
        #endregion

        #region Criterio
        private bool mtdCriterios(ref string strErrMsg)
        {
            bool booResult = false;
            tbxId.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            tbxNombre.Text = InfoGrid1.Rows[RowGrid1][1].ToString().Trim();
            clsCompetencia objCompetencia = new clsCompetencia(
                Convert.ToInt32(InfoGrid1.Rows[RowGrid1][0].ToString().Trim()),
                0, string.Empty, 0, string.Empty);

            mtdHabilitarCamposCompetencia(4);

            booResult = mtdCargarCriterios(objCompetencia, ref strErrMsg);

            return booResult;
        }

        private bool mtdCriterios(string strIdCompetencia, ref string strErrMsg)
        {
            bool booResult = false;
            clsCompetencia objCompetencia = new clsCompetencia(
                Convert.ToInt32(strIdCompetencia),
                0, string.Empty, 0, string.Empty);

            mtdHabilitarCamposCompetencia(4);

            booResult = mtdCargarCriterios(objCompetencia, ref strErrMsg);

            return booResult;
        }

        private bool mtdInsertarCriterio(ref string strErrMsg)
        {
            bool booResult = false;
            clsCriterioCompetencia objCriterio = new clsCriterioCompetencia(0,
                Convert.ToInt32(tbxIdCompetenciaCri.Text.Trim()),
                tbxDescripcionCri.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsCompetenciaBLL cCompetencia = new clsCompetenciaBLL();

            booResult = cCompetencia.mtdInsertarCriterio(objCriterio, ref strErrMsg);

            return booResult;
        }

        private bool mtdModificarCriterio()
        {
            bool booResult = true;

            mtdHabilitarCamposCompetencia(6);

            tbxCodigoCriterio.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
            tbxDescripcionCri.Text = InfoGrid2.Rows[RowGrid2][1].ToString().Trim();
            tbxIdCompetenciaCri.Text = InfoGrid2.Rows[RowGrid2][2].ToString().Trim();
            tbxCompetenciaCri.Text = InfoGrid2.Rows[RowGrid2][3].ToString().Trim();
            tbxUsuarioCreacionCri.Text = InfoGrid2.Rows[RowGrid2][5].ToString().Trim();
            tbxFechaCri.Text = InfoGrid2.Rows[RowGrid2][6].ToString().Trim();

            tbxCodigoCriterio.Enabled = false;
            tbxCompetenciaCri.Enabled = false;
            tbxUsuarioCreacionCri.Enabled = false;
            tbxFechaCri.Enabled = false;

            return booResult;
        }

        private bool mtdActualizarCriterio(ref string strErrMsg)
        {
            bool booResult = false;
            clsCriterioCompetencia objCriterio = new clsCriterioCompetencia(
                Convert.ToInt32(tbxCodigoCriterio.Text.Trim()),
                Convert.ToInt32(tbxIdCompetenciaCri.Text.Trim()),
                tbxDescripcionCri.Text.Trim(),
                0, string.Empty);
            clsCompetenciaBLL cCompetencia = new clsCompetenciaBLL();

            booResult = cCompetencia.mtdActualizarCriterio(objCriterio, ref strErrMsg);

            return booResult;
        }
        #endregion

        #endregion
    }
}