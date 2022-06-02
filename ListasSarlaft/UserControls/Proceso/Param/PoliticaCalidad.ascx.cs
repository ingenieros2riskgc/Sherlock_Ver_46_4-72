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
    public partial class PoliticaCalidad : System.Web.UI.UserControl
    {
        string IdFormulario = "4009";
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

                    if (mtdCargarPolitica(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                }
            }
        }

        #region Buttons

        /// <summary>
        /// Permite habilitar los campos para crear una nueva Politica.
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

                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;
            }
        }

        /// <summary>
        /// Permite la insercion de la politica.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarPolitica(ref strErrMsg))
                {
                    omb.ShowMessage("Política de calidad registrada exitosamente.", 3, "Atención");
                    filaGrid.Visible = true;
                    filaDetalle.Visible = false;

                    mtdResetCampos();
                    mtdCargarPolitica(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar la política de calidad.<br/>Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        /// <summary>
        /// Permite Actualizar la politica
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
                    if (mtdActualizarPolitica(ref strErrMsg))
                    {
                        omb.ShowMessage("Política de calidad modificada exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;

                        mtdResetCampos();
                        mtdCargarPolitica(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar la política de calidad. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
        }
        #endregion

        #region Gridview
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
                    break;
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
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }

        #region Cargas
        #region Gridview
        /// <summary>
        /// Permite la carga de la informacion a los controles
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdCargarPolitica(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridPolitica();

            if (mtdLoadInfoGridPolitica(ref strErrMsg))
                btnInsertarNuevo.Visible = false;
            else
                btnInsertarNuevo.Visible = true;

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridPolitica()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos de la Politica al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridPolitica(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsPoliticaCalidad> lstPolitica = new List<clsPoliticaCalidad>();
            clsPoliticaBLL cPolitica = new clsPoliticaBLL();

            lstPolitica = cPolitica.mtdConsultarPoliticasCalidad(ref strErrMsg);

            if (lstPolitica != null)
            {
                mtdLoadInfoGridPolitica(lstPolitica);
                GridView1.DataSource = lstPolitica;
                GridView1.DataBind();
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstProcedimiento">Lista con las Politicas</param>
        private void mtdLoadInfoGridPolitica(List<clsPoliticaCalidad> lstPolitica)
        {
            foreach (clsPoliticaCalidad objPolitica in lstPolitica)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objPolitica.intId.ToString().Trim(),
                    objPolitica.strDescripcion.ToString().Trim(),
                    objPolitica.intIdUsuario.ToString().Trim(),
                    objPolitica.strNombreUsuario.ToString().Trim(),
                    objPolitica.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Habilita los botones de acuerdo a la operacion a realizar
        /// </summary>
        /// <param name="booEstado"></param>
        private void mtdHabilitarBotones(bool booEstado)
        {
            btnImgInsertar.Enabled = booEstado;
            btnImgInsertar.Visible = booEstado;
            btnImgActualizar.Enabled = !booEstado;
            btnImgActualizar.Visible = !booEstado;

        }

        /// <summary>
        /// Permite la insercion de la politica
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdInsertarPolitica(ref string strErrMsg)
        {
            bool booResult = false;
            clsPoliticaCalidad objPolitica = new clsPoliticaCalidad(0,
                tbxDescripcion.Text.Trim(), Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
            clsPoliticaBLL cPolitica = new clsPoliticaBLL();

            booResult = cPolitica.mtdInsertarPoliticaCalidad(objPolitica, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite la actualizacion de la politica
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdActualizarPolitica(ref string strErrMsg)
        {
            bool booResult = false;
            clsPoliticaCalidad objPolitica = new clsPoliticaCalidad(Convert.ToInt32(tbxId.Text.Trim()),
                tbxDescripcion.Text.Trim(), 0, string.Empty);
            clsPoliticaBLL cPolitica = new clsPoliticaBLL();

            booResult = cPolitica.mtdActualizarPoliticaCalidad(objPolitica, ref strErrMsg);

            return booResult;
        }

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
            tbxFecha.Enabled = false;
            tbxUsuarioCreacion.Enabled = false;

            tbxDescripcion.Focus();

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            tbxDescripcion.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();
            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][3].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim();
        }
        #endregion
    }
}