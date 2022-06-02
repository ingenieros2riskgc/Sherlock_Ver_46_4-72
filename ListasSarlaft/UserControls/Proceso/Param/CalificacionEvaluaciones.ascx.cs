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
    public partial class CalificacionEvaluaciones : System.Web.UI.UserControl
    {
        string IdFormulario = "4015";
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;

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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnNuevo);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    if (!mtdCargarCalificacion(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                }
            }
        }

        #region Buttons
        protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                if (mtdLoadDDLEvaluacion(ref strErrMsg))
                {
                    tbxDescripcion.Focus();

                    mtdLimpiarCamposCalificacion();
                    mtdHabilitarCamposCalificacion(2);

                    tbxId.Enabled = false;
                    tbxUsuarioCreacion.Enabled = false;
                    tbxFecha.Enabled = false;
                }
            }
        }

        protected void btnInsertarCal_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarCalificacion(ref strErrMsg))
                {
                    omb.ShowMessage("Calificación registrada exitosamente.", 3, "Atención");
                    mtdLimpiarCamposCalificacion();
                    mtdHabilitarCamposCalificacion(1);
                    mtdCargarCalificacion(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar la Calificación. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnActualizarCal_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                else
                {
                    if (mtdActualizarCalificacion(ref strErrMsg))
                    {
                        omb.ShowMessage("Calificación modificada exitosamente.", 3, "Atención");
                        mtdHabilitarCamposCalificacion(1);
                        mtdLimpiarCamposCalificacion();
                        mtdCargarCalificacion(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar la actividad.<br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarCal_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCamposCalificacion();
            mtdHabilitarCamposCalificacion(1);
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
                    mtdModificar(ref strErrMsg);
                    break;
            }
        }
        #endregion     

        #region Metodos

        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
        }

        private void mtdLimpiarCamposCalificacion()
        {
            tbxId.Text = string.Empty;
            tbxIdConfCalifica.Text = string.Empty;
            ddlEvaluacion.SelectedValue = "0";
            tbxDescripcion.Text = string.Empty;
            tbxValorMinimo.Text = string.Empty;
            tbxValorMaximo.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }

        private void mtdHabilitarCamposCalificacion(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1: //Inicio
                    filaGridCompetencias.Visible = true;
                    filaTituloCalificacion.Visible = false;
                    filaDetalleCalificacion.Visible = false;

                    btnInsertarCal.Visible = false;
                    btnActualizarCal.Visible = false;
                    btnCancelarCal.Visible = false;
                    break;
                case 2://Insertar
                    filaGridCompetencias.Visible = false;
                    filaTituloCalificacion.Visible = true;
                    filaDetalleCalificacion.Visible = true;

                    btnInsertarCal.Visible = true;
                    btnActualizarCal.Visible = false;
                    btnCancelarCal.Visible = true;
                    break;
                case 3: //Actualizar
                    filaGridCompetencias.Visible = false;
                    filaTituloCalificacion.Visible = true;
                    filaDetalleCalificacion.Visible = true;

                    btnInsertarCal.Visible = false;
                    btnActualizarCal.Visible = true;
                    btnCancelarCal.Visible = true;
                    break;
            }
        }

        #region Cargas
        #region Gridview
        private bool mtdCargarCalificacion(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridCalificacion();

            if (mtdLoadInfoGridCalificacion(ref strErrMsg))
                booResult = true;

            return booResult;
        }

        private void mtdLoadGridCalificacion()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("intIdConfiguracionEvaluacion", typeof(string));
            grid.Columns.Add("intIdEvaluacion", typeof(string));
            grid.Columns.Add("strNombreEvaluacion", typeof(string));
            grid.Columns.Add("intValorMinimo", typeof(string));
            grid.Columns.Add("intValorMaximo", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
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
        private bool mtdLoadInfoGridCalificacion(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsCalificacionEvaluacion> lstCalificacion = new List<clsCalificacionEvaluacion>();
            clsCalificacionBLL cCalificacion = new clsCalificacionBLL();

            booResult = cCalificacion.mtdConsultarCalificacion(ref lstCalificacion, ref strErrMsg);

            if (booResult)
                if (lstCalificacion != null)
                {
                    mtdLoadInfoGridCalificacion(lstCalificacion);
                    GridView1.DataSource = lstCalificacion;
                    GridView1.DataBind();
                }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstCompetencia">Lista con las Calificaciones</param>
        private void mtdLoadInfoGridCalificacion(List<clsCalificacionEvaluacion> lstCalificacion)
        {
            foreach (clsCalificacionEvaluacion objCalificacion in lstCalificacion)
            {
                InfoGrid1.Rows.Add(new Object[] {
                    objCalificacion.intId.ToString().Trim(),
                    objCalificacion.intIdConfiguracionEvaluacion.ToString().Trim(),
                    objCalificacion.intIdEvaluacion.ToString().Trim(),
                    objCalificacion.strNombreEvaluacion.ToString().Trim(),
                    objCalificacion.intValorMinimo.ToString().Trim(),
                    objCalificacion.intValorMaximo.ToString().Trim(),
                    objCalificacion.strDescripcion.ToString().Trim(),
                    objCalificacion.intIdUsuario.ToString().Trim(),
                    objCalificacion.strNombreUsuario.ToString().Trim(),
                    objCalificacion.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region DDLs
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLEvaluacion(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsEvaluacion> lstEvaluacion = new List<clsEvaluacion>();
            clsCalificacionBLL cCalificacion = new clsCalificacionBLL();
            #endregion Vars

            try
            {
                booResult = cCalificacion.mtdConsultarEvaluacion(ref lstEvaluacion, ref strErrMsg);
                ddlEvaluacion.Items.Clear();
                ddlEvaluacion.Items.Insert(0, new ListItem("", "0"));

                if (booResult)
                {
                    if (string.IsNullOrEmpty(strErrMsg))
                    {
                        if (lstEvaluacion != null)
                        {
                            int intCounter = 1;

                            foreach (clsEvaluacion objEvaluacion in lstEvaluacion)
                            {
                                ddlEvaluacion.Items.Insert(intCounter, new ListItem(objEvaluacion.strNombre, objEvaluacion.intId.ToString()));
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
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las evaluaciones. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }
        #endregion
        #endregion

        private bool mtdInsertarCalificacion(ref string strErrMsg)
        {
            bool booResult = false;
            clsCalificacionEvaluacion objCalificacion = new clsCalificacionEvaluacion(0,
                0,
                Convert.ToInt32(ddlEvaluacion.SelectedValue.ToString()),
                Convert.ToDecimal(tbxValorMinimo.Text.Trim()),
                Convert.ToDecimal(tbxValorMaximo.Text.Trim()),
                tbxDescripcion.Text.Trim(),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsCalificacionBLL cCalificacion = new clsCalificacionBLL();

            booResult = cCalificacion.mtdInsertarCalificacion(objCalificacion, ref strErrMsg);

            return booResult;
        }

        private bool mtdModificar(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLimpiarCamposCalificacion();
            mtdHabilitarCamposCalificacion(3);
            mtdLoadDDLEvaluacion(ref strErrMsg);

            tbxId.Text = InfoGrid1.Rows[RowGrid1][0].ToString().Trim();
            tbxIdConfCalifica.Text = InfoGrid1.Rows[RowGrid1][1].ToString().Trim();
            ddlEvaluacion.SelectedValue = InfoGrid1.Rows[RowGrid1][2].ToString().Trim();
            tbxDescripcion.Text = InfoGrid1.Rows[RowGrid1][6].ToString().Trim();
            tbxValorMinimo.Text = InfoGrid1.Rows[RowGrid1][4].ToString().Trim();
            tbxValorMaximo.Text = InfoGrid1.Rows[RowGrid1][5].ToString().Trim();
            tbxUsuarioCreacion.Text = InfoGrid1.Rows[RowGrid1][8].ToString().Trim();
            tbxFecha.Text = InfoGrid1.Rows[RowGrid1][9].ToString().Trim();

            return booResult;
        }

        private bool mtdActualizarCalificacion(ref string strErrMsg)
        {
            bool booResult = false;
            clsCalificacionEvaluacion objCalificacion = new clsCalificacionEvaluacion(
                Convert.ToInt32(tbxId.Text.Trim()),
                Convert.ToInt32(tbxIdConfCalifica.Text.Trim()),
                Convert.ToInt32(ddlEvaluacion.SelectedValue.ToString()),
                Convert.ToDecimal(tbxValorMinimo.Text.Trim()),
                Convert.ToDecimal(tbxValorMaximo.Text.Trim()),
                tbxDescripcion.Text.Trim(),
                0, string.Empty);
            clsCalificacionBLL cCalificacion = new clsCalificacionBLL();

            booResult = cCalificacion.mtdActualizarCalificacion(objCalificacion, ref strErrMsg);
            return booResult;
        }
        #endregion
    }
}