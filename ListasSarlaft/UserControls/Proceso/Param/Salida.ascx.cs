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
    public partial class Salida : System.Web.UI.UserControl
    {
        string IdFormulario = "4006";
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
                    if (mtdCargarSalidas(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                }
            }
        }

        #region Gridviews
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
        /// Permite habilitar los campos para crear un nueva salida.
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

                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaDetalle.Visible = true;
                filaGrid.Visible = false;
            }
        }

        /// <summary>
        /// Permite la insercion de la salida.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarSalida(ref strErrMsg))
                {
                    omb.ShowMessage("Salida registrada exitosamente.", 3, "Atención");
                    filaDetalle.Visible = false;
                    filaGrid.Visible = true;

                    mtdResetCampos();
                    mtdCargarSalidas(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar la salida. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
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
                    if (mtdActualizarSalida(ref strErrMsg))
                    {
                        omb.ShowMessage("Salida modificada exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetCampos();
                        mtdCargarSalidas(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar la salida." + "<br/>" + "Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
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
                    if (mtdActualizarEstado(ref strErrMsg))
                    {
                        omb.ShowMessage("Salida (in)activada exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetCampos();
                        mtdCargarSalidas(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al (in)activar la salida. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
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
            tbxCliente.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }

        #region Cargas
        #region Gridview
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarSalidas(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridSalida();
            mtdLoadInfoGridSalida(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridSalida()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("strCliente", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la cadena de valor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridSalida(ref string strErrMsg)
        {
            List<clsSalida> lstSalida = new List<clsSalida>();
            clsSalidaBLL cSalida = new clsSalidaBLL();

            lstSalida = cSalida.mtdConsultarSalida(ref strErrMsg);

            if (lstSalida != null)
            {
                mtdLoadInfoGridSalida(lstSalida);
                GridView1.DataSource = lstSalida;
                GridView1.PageIndex = PagIndex;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con las Entradas</param>
        private void mtdLoadInfoGridSalida(List<clsSalida> lstSalida)
        {
            foreach (clsSalida objSalida in lstSalida)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objSalida.intId.ToString().Trim(),
                    objSalida.strDescripcion.ToString().Trim(),
                    objSalida.booEstado.ToString().Trim(),
                    objSalida.strCliente.ToString().Trim(),
                    objSalida.intIdUsuario.ToString().Trim(),
                    objSalida.strNombreUsuario.ToString().Trim(),
                    objSalida.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Realiza la insercion de las salidas.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>SI la operacion fue exitosa o no.</returns>
        private bool mtdInsertarSalida(ref string strErrMsg)
        {
            bool booResult = false;
            clsSalida objSalida = new clsSalida(0,
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                tbxCliente.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsSalidaBLL cSalida = new clsSalidaBLL();

            booResult = cSalida.mtdInsertarSalida(objSalida, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            filaGrid.Visible = false;
            filaDetalle.Visible = true;
            btnImgInsertar.Visible = false;
            btnImgActualizar.Visible = true;

            tbxId.Enabled = false;
            tbxFecha.Enabled = false;
            ChBEstado.Enabled = false;
            tbxUsuarioCreacion.Enabled = false;

            tbxDescripcion.Focus();

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            tbxDescripcion.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            tbxCliente.Text = InfoGrid.Rows[RowGrid][3].ToString().Trim();
            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][6].ToString().Trim();
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarSalida(ref string strErrMsg)
        {
            bool booResult = false;
            clsSalida objSalida = new clsSalida(
                Convert.ToInt32(tbxId.Text.Trim()),
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                tbxCliente.Text.Trim(),
                0,
                string.Empty);
            clsSalidaBLL cSalida = new clsSalidaBLL();

            booResult = cSalida.mtdActualizarSalida(objSalida, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite mostar el mensaje de activar o inactivar la salida.
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

                lblMsgBox.Text = string.Format("Desea {0} la salida?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;
            clsSalida objSalida = new clsSalida(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty,
                !(InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false),
                string.Empty,
                0,
                string.Empty, string.Empty);
            clsSalidaBLL cSalida = new clsSalidaBLL();

            booResult = cSalida.mtdActualizarEstado(objSalida, ref strErrMsg);

            return booResult;
        }

        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridSalida(ref strErrMsg);
        }
    }
}