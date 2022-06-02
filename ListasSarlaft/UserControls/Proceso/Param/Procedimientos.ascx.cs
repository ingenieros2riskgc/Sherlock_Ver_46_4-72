using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class Procedimientos : System.Web.UI.UserControl
    {
        string IdFormulario = "4008";
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;

        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;

        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }

        private int PagIndex
        {
            get
            {
                pagIndex = (int)ViewState["pagIndex"];
                return pagIndex;
            }
            set
            {
                pagIndex = value;
                ViewState["pagIndex"] = pagIndex;
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

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);
            scrtManager.RegisterPostBackControl(btnAgregarPDF);
            scrtManager.RegisterPostBackControl(GridView3);

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    if (mtdCargarProcedimientos(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");

                    if (mtdCargarActividades(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                }
            }
        }

        #region Gridview
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
                    break;
                case "Activar":
                    mtdActivar();
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid2 = (Convert.ToInt16(GridView2.PageSize) * PagIndex2) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    tbxActividad.Text = InfoGrid2.Rows[RowGrid2][1].ToString().Trim();
                    lblIdActividad.Text = InfoGrid2.Rows[RowGrid2][0].ToString().Trim();
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid3 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarArchivo(ref strErrMsg);
                    if (!string.IsNullOrEmpty(strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");

                    break;
                case "Activar":
                    mtdActivarArchivo();
                    break;
            }
        }
        #endregion

        #region Buttons
        /// <summary>
        /// Permite habilitar los campos para crear un nuevo Procedimiento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                tbxDescripcion.Focus();

                mtdResetCampos();

                tbxId.Enabled = false;
                tbxActividad.Enabled = false;
                tbxUsuarioCreacion.Enabled = false;
                tbxFecha.Enabled = false;

                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;
                filaAdjuntos.Visible = false;
            }
        }

        /// <summary>
        /// Permite la insercion de la Procedimiento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarProcedimiento(ref strErrMsg))
                {
                    omb.ShowMessage("Procedimiento registrado exitosamente.", 3, "Atención");
                    filaGrid.Visible = true;
                    filaDetalle.Visible = false;
                    filaAdjuntos.Visible = false;

                    mtdResetCampos();
                    mtdCargarProcedimientos(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar el procedimiento.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (mtdActualizarProcedimiento(ref strErrMsg))
                    {
                        omb.ShowMessage("Procedimiento modificado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;
                        filaAdjuntos.Visible = false;

                        mtdResetCampos();
                        mtdCargarProcedimientos(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el procedimiento. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnModificarEstado_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            mpeMsgBox.Hide();

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (mtdActualizarEstadoArchivo(ref strErrMsg))
                    {
                        omb.ShowMessage("Archivo (in)activado exitosamente.", 3, "Atención");
                        mtdCargarPdf(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error durante la actualización de la información." + "<br/>" + "Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            filaAdjuntos.Visible = false;
        }

        protected void btnAgregarPDF_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (FileUpload2.HasFile)
                    {
                        if (System.IO.Path.GetExtension(FileUpload2.FileName).ToLower().ToString().Trim() == ".pdf")
                        {
                            if (mtdGuardarArchivo(ref strErrMsg))
                            {
                                omb.ShowMessage("Archivo cargado exitósamente.", 3, "Atención");

                                if (mtdCargarPdf(ref strErrMsg))
                                    omb.ShowMessage(strErrMsg, 1, "Atención");
                            }
                            else
                                omb.ShowMessage(strErrMsg, 1, "Atención");
                        }
                        else
                            omb.ShowMessage("El archivo a cargar debe ser en formato PDF.", 2, "Atención");
                    }
                    else
                        omb.ShowMessage("No hay archivos para cargar.", 2, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al agregar el archivo. " + ex.Message, 1, "Atención");
            }
        }
        #endregion

        #region Metodos

        private void mtdInicializarValores()
        {
            PagIndex = 0;
            PagIndex2 = 0;
            PagIndex3 = 0;
        }

        private void mtdResetCampos()
        {
            tbxId.Text = string.Empty;
            tbxDescripcion.Text = string.Empty;
            ChBEstado.Checked = true;
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
            tbxActividad.Text = string.Empty;
            lblIdActividad.Text = string.Empty;
        }

        #region Cargas
        #region Gridview Procedimiento
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarProcedimientos(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridProcedimiento();
            mtdLoadInfoGridProcedimiento(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridProcedimiento()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdActividad", typeof(string));
            grid.Columns.Add("strDescActividad", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del Procedimiento al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridProcedimiento(ref string strErrMsg)
        {
            List<clsProcedimiento> lstProcedimientos = new List<clsProcedimiento>();
            clsProcedimientoBLL cProcedimiento = new clsProcedimientoBLL();

            lstProcedimientos = cProcedimiento.mtdConsultarProcedimiento(ref strErrMsg);

            if (lstProcedimientos != null)
            {
                mtdLoadInfoGridProcedimiento(lstProcedimientos);
                GridView1.DataSource = lstProcedimientos;
                GridView1.PageIndex = PagIndex;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstProcedimiento">Lista con los Procedimientos</param>
        private void mtdLoadInfoGridProcedimiento(List<clsProcedimiento> lstProcedimiento)
        {
            foreach (clsProcedimiento objProcedimiento in lstProcedimiento)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objProcedimiento.intId.ToString().Trim(),
                    objProcedimiento.strDescripcion.ToString().Trim(),
                    objProcedimiento.booEstado.ToString().Trim(),
                    objProcedimiento.intIdActividad.ToString().Trim(),
                    objProcedimiento.strDescActividad.ToString().Trim(),
                    objProcedimiento.intIdUsuario.ToString().Trim(),
                    objProcedimiento.strNombreUsuario.ToString().Trim(),
                    objProcedimiento.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview Actividades
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarActividades(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridActividades();
            mtdLoadInfoGridActividades(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridActividades()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intCargoResponsable", typeof(string));
            grid.Columns.Add("strNombreCargoResponsable", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGrid2 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la actividad al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridActividades(ref string strErrMsg)
        {
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsActividadBLL cActividad = new clsActividadBLL();

            lstActividad = cActividad.mtdConsultarActividadActivas(ref strErrMsg);

            if (lstActividad != null)
            {
                mtdLoadInfoGridActividad(lstActividad);
                GridView2.DataSource = lstActividad;
                GridView2.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con las Actividades</param>
        private void mtdLoadInfoGridActividad(List<clsActividad> lstActividad)
        {
            foreach (clsActividad objActividad in lstActividad)
            {
                InfoGrid2.Rows.Add(new Object[] {
                    objActividad.intId.ToString().Trim(),
                    objActividad.strDescripcion.ToString().Trim(),
                    objActividad.booEstado.ToString().Trim(),
                    objActividad.intCargoResponsable.ToString().Trim(),
                    objActividad.strNombreCargoResponsable.ToString().Trim(),
                    objActividad.intIdUsuario.ToString().Trim(),
                    objActividad.strNombreUsuario.ToString().Trim(),
                    objActividad.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview PDF
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        private bool mtdCargarPdf(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridArchivos();
            mtdLoadInfoGridArchivos(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridArchivos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("intIdControl", typeof(string));
            grid.Columns.Add("intIdTipoControl", typeof(string));
            grid.Columns.Add("strNombreArchivo", typeof(string));
            grid.Columns.Add("bArchivoBinario", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGrid3 = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del archivo al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridArchivos(ref string strErrMsg)
        {
            List<clsArchivosCalidad> lstArchivos = new List<clsArchivosCalidad>();
            clsArchivosCalidadBLL cArchivos = new clsArchivosCalidadBLL();
            clsArchivosCalidad objArchivo = new clsArchivosCalidad(0,
                Convert.ToInt32(tbxId.Text.Trim()), 1, string.Empty, null, true, 0, string.Empty);

            lstArchivos = cArchivos.mtdConsultarArchivos(objArchivo, ref strErrMsg);

            if (lstArchivos != null)
            {
                mtdLoadInfoGridArchivos(lstArchivos);
                GridView3.DataSource = lstArchivos;
                GridView3.PageIndex = PagIndex2;
                GridView3.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los archivos</param>
        private void mtdLoadInfoGridArchivos(List<clsArchivosCalidad> lstArchivos)
        {
            foreach (clsArchivosCalidad objArchivo in lstArchivos)
            {
                InfoGrid3.Rows.Add(new Object[] {
                    objArchivo.intId.ToString().Trim(),
                    objArchivo.intIdControl.ToString().Trim(),
                    objArchivo.intIdTipoControl.ToString().Trim(),
                    objArchivo.strNombreArchivo.ToString().Trim(),
                    objArchivo.bArchivoBinario.ToString().Trim(),
                     objArchivo.booEstado.ToString().Trim(),
                    objArchivo.intIdUsuario.ToString().Trim(),
                    objArchivo.strNombreUsuario.ToString().Trim(),
                    objArchivo.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            string strErrMsg = string.Empty;

            filaGrid.Visible = false;
            filaDetalle.Visible = true;
            filaAdjuntos.Visible = true;

            btnImgInsertar.Visible = false;
            btnImgActualizar.Visible = true;

            tbxId.Enabled = false;
            tbxActividad.Enabled = false;
            tbxFecha.Enabled = false;
            //ChBEstado.Enabled = false;
            tbxUsuarioCreacion.Enabled = false;

            tbxDescripcion.Focus();

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            tbxDescripcion.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            tbxActividad.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim();
            lblIdActividad.Text = InfoGrid.Rows[RowGrid][3].ToString().Trim();

            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][6].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][7].ToString().Trim();

            mtdCargarPdf(ref strErrMsg);

        }

        /// <summary>
        /// Permite mostar el mensaje de activar o inactivar.
        /// </summary>
        private void mtdActivar()
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                string strEstado = string.Empty;
                bool booEstado = InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false;

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} el procedimiento?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Permite mostar el mensaje de activar o inactivar.
        /// </summary>
        private void mtdActivarArchivo()
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                string strEstado = string.Empty;
                bool booEstado = InfoGrid3.Rows[RowGrid3][5].ToString().Trim() == "True" ? true : false;

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} el archivo?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Realiza la insercion de los Procedimientos.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>SI la operacion fue exitosa o no.</returns>
        private bool mtdInsertarProcedimiento(ref string strErrMsg)
        {
            bool booResult = false;
            clsProcedimiento objProcedimiento = new clsProcedimiento(0,
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                Convert.ToInt32(lblIdActividad.Text.Trim()),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsProcedimientoBLL cProcedimiento = new clsProcedimientoBLL();

            booResult = cProcedimiento.mtdInsertarProcedimiento(objProcedimiento, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarProcedimiento(ref string strErrMsg)
        {
            bool booResult = false;
            clsProcedimiento objProcedimiento = new clsProcedimiento(
                Convert.ToInt32(tbxId.Text.Trim()),
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                Convert.ToInt32(lblIdActividad.Text.Trim()),
                0, string.Empty);
            clsProcedimientoBLL cProcedimiento = new clsProcedimientoBLL();

            booResult = cProcedimiento.mtdActualizarProcedimiento(objProcedimiento, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;
            clsProcedimiento objProcedimiento = new clsProcedimiento(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty,
                !(InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false),
                0, 0, string.Empty);
            clsProcedimientoBLL cProcedimiento = new clsProcedimientoBLL();

            booResult = cProcedimiento.mtdActualizarEstado(objProcedimiento, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarEstadoArchivo(ref string strErrMsg)
        {
            bool booResult = false;

            clsArchivosCalidad objArchivoIN = new clsArchivosCalidad(Convert.ToInt32(InfoGrid3.Rows[RowGrid3][0].ToString().Trim()),
                0, 0, string.Empty, null, !(InfoGrid3.Rows[RowGrid3][5].ToString().Trim() == "True" ? true : false), 0, string.Empty);
            clsArchivosCalidadBLL cArchivos = new clsArchivosCalidadBLL();

            booResult = cArchivos.mtdActualizarEstado(objArchivoIN, ref strErrMsg);

            return booResult;
        }

        #region PDF
        private void mtdDescargarArchivo(ref string strErrMsg)
        {
            #region Vars
            clsArchivosCalidadBLL cArchivos = new clsArchivosCalidadBLL();
            clsArchivosCalidad objArchivoIN = new clsArchivosCalidad(Convert.ToInt32(InfoGrid3.Rows[RowGrid3][0].ToString().Trim()),
                0, 0, string.Empty, null, true, 0, string.Empty), objArchivoOUT = new clsArchivosCalidad();
            bool booEstado = InfoGrid3.Rows[RowGrid3][5].ToString().Trim() == "True" ? true : false;
            #endregion Vars

            if (booEstado)
            {
                objArchivoOUT = cArchivos.mtdDescargarArchivo(objArchivoIN, ref strErrMsg);

                if (objArchivoOUT.bArchivoBinario != null)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "Application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + objArchivoOUT.strNombreArchivo);
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(objArchivoOUT.bArchivoBinario);
                    Response.End();
                }
            }
            else
                strErrMsg = "No se puede descargar el archivo porque esta inactivo";
        }

        private bool mtdGuardarArchivo(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsArchivosCalidadBLL cArchivo = new clsArchivosCalidadBLL();
            int intConsecutivo = cArchivo.mtdConsecutivoArchivo(ref strErrMsg), intTipoControl = 1;
            string strNombreArchivo = string.Empty;
            clsArchivosCalidad objArchivo = new clsArchivosCalidad();
            #endregion

            if (intConsecutivo > 0)
            {
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}", intConsecutivo, intTipoControl, tbxId.Text.Trim(), FileUpload2.FileName.ToString().Trim());

                #region Vars
                Stream fs = FileUpload2.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
                #endregion

                objArchivo = new clsArchivosCalidad(0, Convert.ToInt32(tbxId.Text.Trim()), intTipoControl, strNombreArchivo, bPdfData, true,
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);

                booResult = cArchivo.mtdInsertarArchivo(objArchivo, ref strErrMsg);
            }
            else
                booResult = false;

            return booResult;
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridProcedimiento(ref strErrMsg);
        }
        #endregion

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex2 = e.NewPageIndex;
            GridView3.PageIndex = PagIndex;
            GridView3.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridArchivos(ref strErrMsg);
        }
    }
}