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
    public partial class Entrada : System.Web.UI.UserControl
    {
        string IdFormulario = "4005";
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
                    if (mtdCargarEntradas(ref strErrMsg))
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
        /// Permite habilitar los campos para crear un nueva entrada.
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
        /// Permite la insercion de la entrada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarEntrada(ref strErrMsg))
                {
                    omb.ShowMessage("Entrada  registrada exitosamente.", 3, "Atención");
                    filaDetalle.Visible = false;
                    filaGrid.Visible = true;

                    mtdResetCampos();
                    mtdCargarEntradas(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar la entrada. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
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
                    if (mtdActualizarEntrada(ref strErrMsg))
                    {
                        omb.ShowMessage("Entrada modificada exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetCampos();
                        mtdCargarEntradas(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar la entrada. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
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
                        omb.ShowMessage("Entrada (in)activada exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetCampos();
                        mtdCargarEntradas(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al (in)activar la entrada. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
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
            tbxProveedor.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }

        #region Cargas
        #region Gridview
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarEntradas(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridEntrada();
            mtdLoadInfoGridEntrada(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridEntrada()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("strProveedor", typeof(string));
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
        private void mtdLoadInfoGridEntrada(ref string strErrMsg)
        {
            List<clsEntrada> lstEntrada = new List<clsEntrada>();
            clsEntradaBLL cEntrada = new clsEntradaBLL();

            lstEntrada = cEntrada.mtdConsultarEntrada(ref strErrMsg);

            if (lstEntrada != null)
            {
                mtdLoadInfoGridEntrada(lstEntrada);
                GridView1.DataSource = lstEntrada;
                GridView1.PageIndex = PagIndex;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstEntrada">Lista con las Entradas</param>
        private void mtdLoadInfoGridEntrada(List<clsEntrada> lstEntrada)
        {
            foreach (clsEntrada objEntrada in lstEntrada)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objEntrada.intId.ToString().Trim(),
                    objEntrada.strDescripcion.ToString().Trim(),
                    objEntrada.booEstado.ToString().Trim(),
                    objEntrada.strProveedor.ToString().Trim(),
                    objEntrada.intIdUsuario.ToString().Trim(),
                    objEntrada.strNombreUsuario.ToString().Trim(),
                    objEntrada.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Realiza la insercion de las entradas.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>SI la operacion fue exitosa o no.</returns>
        private bool mtdInsertarEntrada(ref string strErrMsg)
        {
            bool booResult = false;
            clsEntrada objEntrada = new clsEntrada(0,
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                tbxProveedor.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsEntradaBLL cEntrada = new clsEntradaBLL();

            booResult = cEntrada.mtdInsertarEntrada(objEntrada, ref strErrMsg);

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

            tbxProveedor.Text = InfoGrid.Rows[RowGrid][3].ToString().Trim();

            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][6].ToString().Trim();
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarEntrada(ref string strErrMsg)
        {
            bool booResult = false;
            clsEntrada objEntrada = new clsEntrada(
                Convert.ToInt32(tbxId.Text.Trim()), 
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                tbxProveedor.Text.Trim(),
                0,
                string.Empty);
            clsEntradaBLL cEntrada = new clsEntradaBLL();

            booResult = cEntrada.mtdActualizarEntrada(objEntrada, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite mostar el mensaje de activar o inactivar la entrada.
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

                lblMsgBox.Text = string.Format("Desea {0} la entrada?", strEstado);
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
            clsEntrada objEntrada = new clsEntrada(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty,
                !(InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false),
                string.Empty,
                0,
                string.Empty, string.Empty);
            clsEntradaBLL cEntrada = new clsEntradaBLL();

            booResult = cEntrada.mtdActualizarEstado(objEntrada, ref strErrMsg);

            return booResult;
        }

        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridEntrada(ref strErrMsg);
        }
    }
}