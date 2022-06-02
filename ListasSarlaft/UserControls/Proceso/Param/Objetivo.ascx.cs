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

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class Objetivo : System.Web.UI.UserControl
    {
        string IdFormulario = "4010";
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;

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

                    if (mtdCargarObjetivo(ref strErrMsg))
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
        #endregion

        #region Buttons

        /// <summary>
        /// Permite habilitar los campos para crear un nuevo Objetivo.
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
                tbxUsuarioCreacion.Enabled = false;
                tbxFecha.Enabled = false;
                ChBEstado.Enabled = true;

                ChBEstado.Checked = true;

                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;
            }
        }

        /// <summary>
        /// Permite la insercion del Objetivo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarObjetivo(ref strErrMsg))
                {
                    omb.ShowMessage("Objetivo registrado exitosamente.", 3, "Atención");
                    filaGrid.Visible = true;
                    filaDetalle.Visible = false;

                    mtdResetCampos();
                    mtdCargarObjetivo(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar el objetivo.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        /// <summary>
        /// Permite Actualizar el Objetivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    if (mtdActualizarObjetivo(ref strErrMsg))
                    {
                        omb.ShowMessage("Objetivo modificado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;

                        mtdResetCampos();
                        mtdCargarObjetivo(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar el objetivo. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        /// <summary>
        /// Permite modificar el estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                    if (mtdActualizarEstado(ref strErrMsg))
                    {
                        omb.ShowMessage("Objetivo (in)activado exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetCampos();
                        mtdCargarObjetivo(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al inactivar el objetivo. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            mtdResetCampos();
        }
        #endregion

        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdResetCampos()
        {
            tbxId.Text = string.Empty;
            tbxDescripcion.Text = string.Empty;
            ChBEstado.Checked = true;
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }

        #region  Cargar Gridview
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarObjetivo(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridObjetivo();
            mtdLoadInfoGridObjetivo(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridObjetivo()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del objetivo al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridObjetivo(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsObjetivoCalidad> lstObjetivo = new List<clsObjetivoCalidad>();
            clsObjetivoBLL cObjetivo = new clsObjetivoBLL();

            lstObjetivo = cObjetivo.mtdConsultarObjetivoCalidad(ref strErrMsg);

            if (lstObjetivo != null)
            {
                mtdLoadInfoGridObjetivo(lstObjetivo);
                GridView1.DataSource = lstObjetivo;
                GridView1.PageIndex = PagIndex;
                GridView1.DataBind();
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Objetivos</param>
        private void mtdLoadInfoGridObjetivo(List<clsObjetivoCalidad> lstObjetivo)
        {
            foreach (clsObjetivoCalidad objObjetivo in lstObjetivo)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objObjetivo.intId.ToString().Trim(),
                    objObjetivo.strDescripcion.ToString().Trim(),
                    objObjetivo.booEstado.ToString().Trim(),
                    objObjetivo.intIdUsuario.ToString().Trim(),
                    objObjetivo.strNombreUsuario.ToString().Trim(),
                    objObjetivo.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            string strErrMsg = string.Empty;

            filaGrid.Visible = false;
            filaDetalle.Visible = true;
            btnImgInsertar.Visible = false;
            btnImgActualizar.Visible = true;

            tbxId.Enabled = false;
            ChBEstado.Enabled = false;
            tbxFecha.Enabled = false;
            tbxUsuarioCreacion.Enabled = false;

            tbxDescripcion.Focus();

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            tbxDescripcion.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();
        }

        /// <summary>
        /// Realiza el proceso previo a actualizar el estado
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

                lblMsgBox.Text = string.Format("Desea {0} el Objetivo?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Realiza la insercion del Objetivo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        private bool mtdInsertarObjetivo(ref string strErrMsg)
        {
            bool booResult = false;
            clsObjetivoCalidad objObjetivo = new clsObjetivoCalidad(0,
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty, string.Empty);

            clsObjetivoBLL cObjetivo = new clsObjetivoBLL();

            booResult = cObjetivo.mtdInsertarObjetivoCalidad(objObjetivo, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite la actualizacion de la Objetivo
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdActualizarObjetivo(ref string strErrMsg)
        {
            bool booResult = false;
            clsObjetivoCalidad objObjetivo = new clsObjetivoCalidad(Convert.ToInt32(tbxId.Text.Trim()),
                tbxDescripcion.Text.Trim(), ChBEstado.Checked, 0, string.Empty);
            clsObjetivoBLL cObjetivo = new clsObjetivoBLL();

            booResult = cObjetivo.mtdActualizarObjetivoCalidad(objObjetivo, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de estado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no </returns>
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;

            clsObjetivoCalidad objObjetivo = new clsObjetivoCalidad(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty,
                !(InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false),
                0, string.Empty);

            clsObjetivoBLL cObjetivo = new clsObjetivoBLL();

            booResult = cObjetivo.mtdActualizarEstado(objObjetivo, ref strErrMsg);

            return booResult;
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridObjetivo(ref strErrMsg);
        }
    }
}