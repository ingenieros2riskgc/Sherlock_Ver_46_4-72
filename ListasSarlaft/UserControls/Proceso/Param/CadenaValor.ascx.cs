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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class CadenaValor : System.Web.UI.UserControl
    {
        string IdFormulario = "4001";
        cCuenta cCuenta = new cCuenta();
        clsCadenaValorBLL cCadenaValor = new clsCadenaValorBLL();

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
            /*get
            {
                txtId.Enabled = false;
                //txtNombre.Focus();
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;

                // Carga los datos en la respectiva caja de texto
                rowGrid = (int)ViewState["rowGrid"];
                
                txtId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();//GridView1.SelectedRow.Cells[0].Text.Trim();
                txtNombre.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim(); //GridView1.SelectedRow.Cells[1].Text.Trim();
                tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim(); //GridView1.SelectedDataKey[1].ToString().Trim();
                txtFecha.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim(); //GridView1.SelectedRow.Cells[3].Text.Trim();


                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }*/
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

        //private int pagIndex;
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
                    if (mtdCargarCadenaValor(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                }
            }
            
        }

        #region Gridviews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = /*(Convert.ToInt16(GridView1.PageSize) * PagIndex) +*/ Convert.ToInt16(e.CommandArgument);
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
        /// Permite habilitar los campos para crear un nuevo cadena de valor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNuevaCadenaValor_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtNombre.Focus();

                txtId.Text = string.Empty;
                txtNombre.Text = string.Empty;
                ChBEstado.Checked = true;
                txtFecha.Text = string.Empty;
                tbxUsuarioCreacion.Text = string.Empty;

                ChBEstado.Enabled = true;
                txtFecha.Enabled = false;
                txtId.Enabled = false;

                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaDetalle.Visible = true;
                filaGrid.Visible = false;
            }
        }

        /// <summary>
        /// Permite la insercion de la cadena de valor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarCadenaValor(ref strErrMsg))
                {
                    omb.ShowMessage("Cadena de valor registrada exitosamente.", 3, "Atención");
                    filaDetalle.Visible = false;
                    filaGrid.Visible = true;

                    mtdResetValores();
                    mtdCargarCadenaValor(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar la cadena de valor. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
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
                    if (mtdActualizarCadenaValor(ref strErrMsg))
                    {
                        omb.ShowMessage("Cadena de valor modificada exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetValores();
                        mtdCargarCadenaValor(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar la cadena de valor. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
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
                        omb.ShowMessage("Cadena de valor (in)activada exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetValores();
                        mtdCargarCadenaValor(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al (in)activar la cadena de valor. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }
        #endregion

        #region Metodos

        private void mtdResetValores()
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            ChBEstado.Checked = true;
            txtFecha.Text = string.Empty;
        }

        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        #region Cargas
        #region Gridview
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarCadenaValor(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridCadena();
            mtdLoadInfoGridCadena(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridCadena()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreCadenaValor", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
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
        private void mtdLoadInfoGridCadena(ref string strErrMsg)
        {
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            cCadenaValor = new clsCadenaValorBLL();

            lstCadenaValor = cCadenaValor.mtdConsultarCadenaValor(ref strErrMsg);

            if (lstCadenaValor != null)
            {
                mtdLoadInfoGridCadena(lstCadenaValor);
                GridView1.PageIndex = PagIndex;
                GridView1.DataSource = lstCadenaValor;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstCadenaValor">Lista con las Cadenas de valor</param>
        private void mtdLoadInfoGridCadena(List<clsCadenaValor> lstCadenaValor)
        {
            foreach (clsCadenaValor objCadena in lstCadenaValor)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objCadena.intId.ToString().Trim(),
                    objCadena.strNombreCadenaValor.ToString().Trim(),
                    objCadena.booEstado.ToString().Trim(),
                    objCadena.intIdUsuario.ToString().Trim(),
                    objCadena.strNombreUsuario.ToString().Trim(),
                    objCadena.dtFechaRegistro.ToString().Trim()
                    });
            }
            
        }
        #endregion
        #endregion

        /// <summary>
        /// Realiza la insercion de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>SI la operacion fue exitosa o no.</returns>
        private bool mtdInsertarCadenaValor(ref string strErrMsg)
        {
            bool booResult = false;
            clsCadenaValor objCadenaValor = new clsCadenaValor(0, txtNombre.Text.Trim(), ChBEstado.Checked,
                 Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty, string.Empty);
            cCadenaValor = new clsCadenaValorBLL();

            booResult = cCadenaValor.mtdInsertarCadenaValor(objCadenaValor, ref strErrMsg);

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

            txtId.Enabled = false;
            ChBEstado.Enabled = false;

            txtNombre.Focus();

            // Carga los datos en la respectiva caja de texto
            txtId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            txtNombre.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim();

            txtFecha.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarCadenaValor(ref string strErrMsg)
        {
            bool booResult = false;
            clsCadenaValor objCadenaValor = new clsCadenaValor(Convert.ToInt32(txtId.Text.Trim()), txtNombre.Text.Trim(),
                ChBEstado.Checked, Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty, string.Empty);
            cCadenaValor = new clsCadenaValorBLL();

            booResult = cCadenaValor.mtdActualizarCadenaValor(objCadenaValor, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite mostar el mensaje de activar o inactivar el subproceso.
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

                lblMsgBox.Text = string.Format("Desea {0} la cadena de valor?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Realiza la actualizacion de estado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no </returns>
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;

            clsCadenaValor objCadenaValor = new clsCadenaValor(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty,
                !(InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false),
                0, string.Empty, string.Empty);
            cCadenaValor = new clsCadenaValorBLL();


            booResult = cCadenaValor.mtdActualizarEstado(objCadenaValor, ref strErrMsg);

            return booResult;
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridCadena(ref strErrMsg);
        }

        
    }
}