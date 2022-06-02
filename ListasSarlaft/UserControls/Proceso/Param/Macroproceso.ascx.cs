using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Security.Application;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class Macroproceso : System.Web.UI.UserControl
    {
        string IdFormulario = "4002";
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;

        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;

        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;

        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

        private DataTable infoGrid5;
        private int rowGrid5;
        private int pagIndex5;

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


        private DataTable InfoGrid5
        {
            get
            {
                infoGrid5 = (DataTable)ViewState["infoGrid5"];
                return infoGrid5;
            }
            set
            {
                infoGrid5 = value;
                ViewState["infoGrid5"] = infoGrid5;
            }
        }

        private int RowGrid5
        {
            get
            {
                rowGrid5 = (int)ViewState["rowGrid5"];
                return rowGrid5;
            }
            set
            {
                rowGrid5 = value;
                ViewState["rowGrid5"] = rowGrid5;
            }
        }

        private int PagIndex5
        {
            get
            {
                pagIndex5 = (int)ViewState["pagIndex5"];
                return pagIndex5;
            }
            set
            {
                pagIndex5 = value;
                ViewState["pagIndex5"] = pagIndex5;
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
                    if (mtdCargarMacroproceso(ref strErrMsg))
                    {
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                        strErrMsg = string.Empty;
                    }

                    if (mtdLoadDDLCadenaValor(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");

                    //PopulateTreeView();

                    TreeNodeCollection nodes = this.TreeView4.Nodes;

                    if (nodes.Count <= 0)
                        PopulateTreeView(2);

                }
            }
        }

        #region Gridviews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
                    case "Ver":
                        mtdConsultarMP();
                        break;
                }
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error" + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string strErrMsg = string.Empty;
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();

            mtdCargarMacroproceso(ref strErrMsg);
        }
        #endregion

        #region Buttons
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            filaConsulta.Visible = false;

            mtdResetValores();
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                mtdResetValores();

                ddlCadenaValor.Focus();

                txtId.Enabled = false;
                btnImgInsertar.Visible = true;
                ChBEstado.Enabled = true;

                btnImgActualizar.Visible = false;
                btnCancelarConsultar.Visible = false;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;
                filaConsulta.Visible = false;
                imgDependencia4.Visible = true;
            }
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarMacroproceso(ref strErrMsg))
                {
                    omb.ShowMessage("Macroproceso registrado exitosamente.", 3, "Atención");

                    filaGrid.Visible = true;
                    filaDetalle.Visible = false;
                    filaConsulta.Visible = false;

                    mtdResetValores();
                    mtdCargarMacroproceso(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar el macroproceso. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
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
                    if (mtdActualizarMacroproceso(ref strErrMsg))
                    {
                        omb.ShowMessage("Macroproceso modificado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;
                        filaConsulta.Visible = false;

                        mtdResetValores();
                        mtdCargarMacroproceso(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al modificar el macroproceso. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
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

                    if (mtdActualizarEstado(ref strErrMsg))
                    {
                        omb.ShowMessage("Macroproceso (in)activado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;
                        filaConsulta.Visible = false;

                        mtdResetValores();
                        mtdCargarMacroproceso(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al (in)activar el macroproceso." + "<br/>" + "Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarConsultar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            filaConsulta.Visible = false;
            btnCancelarConsultar.Visible = false;
            btnCancelarConsultar.Enabled = false;

            mtdResetCampos();
        }

        #endregion

        #region Metodos

        #region Limpiar Campos
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            PagIndex1 = 0;
            PagIndex2 = 0;
            PagIndex3 = 0;
            PagIndex5 = 0;
        }

        private void mtdResetValores()
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            TxtDescripcion.Text = string.Empty;
            txtObjetivo.Text = string.Empty;
            lblIdDependencia4.Text = string.Empty;
            tbxResponsable.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;

            ChBEstado.Checked = true;
            ddlCadenaValor.SelectedValue = "0";

        }

        private void mtdResetCampos()
        {
            tbxCadenaValor.Text = string.Empty;
            tbxMacroproceso.Text = string.Empty;
            tbxDescripcion.Text = string.Empty;
            tbxObjetivo.Text = string.Empty;
            tbxCargo.Text = string.Empty;
            tbxResponsable.Text = string.Empty;
        }

        #endregion

        #region Cargas
        #region Treeview
        private void PopulateTreeView(int numTV)
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData, numTV);
            TreeView4.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            //string selectCommand = "SELECT PJO.IdHijo, PJO.IdPadre, PJO.NombreHijo, PDJ.NombreResponsable, PDJ.CorreoResponsable " +
            //    "FROM Parametrizacion.JerarquiaOrganizacional PJO LEFT JOIN Parametrizacion.DetalleJerarquiaOrg PDJ ON PJO.idHijo = PDJ.idHijo";
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional]";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData, int numTV)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";

            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                if (numTV == 2)
                    TreeView4.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                //newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString();
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private string DetalleNodo(int tipoSelect, string idHijo)
        {
            string Detalle = "", selectCommand = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            if (tipoSelect == 1)
                selectCommand = "SELECT NombreResponsable,CorreoResponsable FROM [Parametrizacion].[DetalleJerarquiaOrg] WHERE idHijo = " + idHijo;
            else
                selectCommand = "SELECT NombreResponsable,CorreoResponsable, NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional] LEFT OUTER JOIN [Parametrizacion].[DetalleJerarquiaOrg] ON [DetalleJerarquiaOrg].idHijo = [JerarquiaOrganizacional].idHijo WHERE [JerarquiaOrganizacional].idHijo = " + idHijo;

            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                Detalle = "Responsable: " + row["NombreResponsable"].ToString() + "\r";
                Detalle = Detalle + "Correo: " + row["CorreoResponsable"].ToString().Trim();

                if (tipoSelect == 2)
                    Detalle = Detalle + "\r Nodo Jerarquía Org.: " + row["NombreHijo"].ToString().Trim();
            }

            if (Detalle == "")
                Detalle = "Responsable: \rCorreo:";

            return (Detalle);
        }

        protected void TreeView4_SelectedNodeChanged(object sender, EventArgs e)
        {
            tbxResponsable.Text = TreeView4.SelectedNode.Text;
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview

        #region DDLs
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLCadenaValor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsCadenaValorBLL cCadenaValor = new clsCadenaValorBLL();
            #endregion Vars

            try
            {
                lstCadenaValor = cCadenaValor.mtdConsultarCadenaValor(true, ref strErrMsg);
                ddlCadenaValor.Items.Clear();
                ddlCadenaValor.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }
        #endregion

        #region Gridview 1
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarMacroproceso(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridMacroproceso();
            mtdLoadInfoGridMacroproceso(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridMacroproceso()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreMacroproceso", typeof(string));
            grid.Columns.Add("intIdCadenaDeValor", typeof(string));
            grid.Columns.Add("strNombreCadenaValor", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("strObjetivo", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("intCargoResponsable", typeof(string));
            grid.Columns.Add("strNombreResponsable", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del Macroproceso al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridMacroproceso(ref string strErrMsg)
        {
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroProceso = new clsMacroProcesoBLL();

            lstMacroproceso = cMacroProceso.mtdConsultarMacroproceso(ref strErrMsg);

            if (lstMacroproceso != null)
            {

                mtdLoadInfoGridMacroproceso(lstMacroproceso);
                GridView1.DataSource = lstMacroproceso;
                GridView1.PageIndex = PagIndex;
                //InfoGrid = cMacroProceso.mtdDataMacroproceso(lstMacroproceso);
                //GridView1.DataSource = cMacroProceso.mtdDataMacroproceso(lstMacroproceso);
                GridView1.DataBind();

            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Macroprocesos</param>
        private void mtdLoadInfoGridMacroproceso(List<clsMacroproceso> lstMacroproceso)
        {
            foreach (clsMacroproceso objMacroproceso in lstMacroproceso)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objMacroproceso.intId.ToString().Trim(),
                    objMacroproceso.strNombreMacroproceso.ToString().Trim(),
                    objMacroproceso.intIdCadenaDeValor.ToString().Trim(),
                    objMacroproceso.strNombreCadenaValor.ToString().Trim(),
                    objMacroproceso.strDescripcion.ToString().Trim(),
                    objMacroproceso.strObjetivo.ToString().Trim(),
                    objMacroproceso.booEstado.ToString().Trim(),
                    objMacroproceso.intIdUsuario.ToString().Trim(),
                    objMacroproceso.strNombreUsuario.ToString().Trim(),
                    objMacroproceso.intCargoResponsable.ToString().Trim(),
                    objMacroproceso.strNombreResponsable.ToString().Trim(),
                    objMacroproceso.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview Entrada
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarEntradas(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridEntrada();
            mtdLoadInfoGridEntrada(objCaracter, ref strErrMsg);

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

            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGrid1 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la cadena de valor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridEntrada(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            List<clsEntrada> lstEntrada = new List<clsEntrada>();
            clsEntradaBLL cEntrada = new clsEntradaBLL();

            lstEntrada = cEntrada.mtdConsultarEntrada(objCaracter, ref strErrMsg);

            if (lstEntrada != null)
            {
                GridView2.DataSource = lstEntrada;
                GridView2.PageIndex = PagIndex2;
                GridView2.DataBind();
            }
        }

        #endregion

        #region Gridview Actividades
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarActividades(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridActividades();
            mtdLoadInfoGridActividades(objCaracter, ref strErrMsg);

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

            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGrid2 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la actividad al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridActividades(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsActividadBLL cActividad = new clsActividadBLL();

            lstActividad = cActividad.mtdConsultarActividad(objCaracter, ref strErrMsg);

            if (lstActividad != null)
            {
                GridView3.DataSource = lstActividad;
                GridView3.DataBind();
            }
        }

        #endregion

        #region Gridview Salidas
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarSalidas(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridSalida();
            mtdLoadInfoGridSalida(objCaracter, ref strErrMsg);

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

            GridView4.DataSource = grid;
            GridView4.DataBind();
            InfoGrid3 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la cadena de valor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridSalida(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            List<clsSalida> lstSalida = new List<clsSalida>();
            clsSalidaBLL cSalida = new clsSalidaBLL();

            lstSalida = cSalida.mtdConsultarSalida(objCaracter, ref strErrMsg);

            if (lstSalida != null)
            {
                GridView4.DataSource = lstSalida;
                GridView4.DataBind();
            }
        }

        #endregion

        #region Gridview Riesgos
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarRiesgos(clsMacroproceso objMPIn, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridRiesgos();
            mtdLoadInfoGridRiesgos(objMPIn, ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridRiesgos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdRiesgo", typeof(string));
            grid.Columns.Add("strCodigo", typeof(string));
            grid.Columns.Add("strNombreRiesgo", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("intIdRegion", typeof(string));
            grid.Columns.Add("intIdPais", typeof(string));
            grid.Columns.Add("intIdDepartamento", typeof(string));
            grid.Columns.Add("intIdCiudad", typeof(string));
            grid.Columns.Add("intIdOficinaSucursal", typeof(string));
            grid.Columns.Add("intIdCadenaValor", typeof(string));
            grid.Columns.Add("intIdMacroproceso", typeof(string));
            grid.Columns.Add("intIdProceso", typeof(string));
            grid.Columns.Add("intIdSubProceso", typeof(string));
            grid.Columns.Add("intIdActividad", typeof(string));
            grid.Columns.Add("intIdClasificacionRiesgo", typeof(string));
            grid.Columns.Add("intIdClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("intIdClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("intIdFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("intIdTipoRiesgoOperativo", typeof(string));
            grid.Columns.Add("intIdTipoEventoOperativo", typeof(string));
            grid.Columns.Add("intIdRiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("intIdResponsableRiesgo", typeof(string));
            grid.Columns.Add("intIdProbabilidad", typeof(string));
            grid.Columns.Add("intIdProbabilidadResidual", typeof(string));
            grid.Columns.Add("intIdImpacto", typeof(string));
            grid.Columns.Add("intIdImpactoResidual", typeof(string));
            grid.Columns.Add("strListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("strListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("strListaCausas", typeof(string));
            grid.Columns.Add("strListaConsecuencias", typeof(string));
            grid.Columns.Add("strOcurrenciaEventoHasta", typeof(string));
            grid.Columns.Add("strOcurrenciaEventoDesde", typeof(string));
            grid.Columns.Add("strPerdidaEconomicaDesde", typeof(string));
            grid.Columns.Add("strPerdidaEconomicaHasta", typeof(string));
            grid.Columns.Add("strListaTratamiento", typeof(string));
            grid.Columns.Add("booAnulado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView6.DataSource = grid;
            GridView6.DataBind();
            InfoGrid5 = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del Riesgo al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridRiesgos(clsMacroproceso objMPIn, ref string strErrMsg)
        {
            List<clsRiesgos> lstRiesgo = new List<clsRiesgos>();
            clsRiesgoBLL cRiesgo = new clsRiesgoBLL();

            lstRiesgo = cRiesgo.mtdConsultarRiesgosMP(objMPIn, ref strErrMsg);

            if (lstRiesgo != null)
            {
                GridView6.DataSource = lstRiesgo;
                GridView6.DataBind();
            }
        }

        #endregion

        #region Gridview Indicadores
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarIndicador(clsMacroproceso objMPIn, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridIndicador();
            mtdLoadInfoGridIndicador(objMPIn, ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridIndicador()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("intIdPeriodicidad", typeof(string));
            grid.Columns.Add("strNombrePeriodicidad", typeof(string));
            grid.Columns.Add("intMeta", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdCalificacion", typeof(string));
            grid.Columns.Add("intIdObjetivoCalidad", typeof(string));
            grid.Columns.Add("strDescObjetivo", typeof(string));
            grid.Columns.Add("intIdProcesoIndicador", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView5.DataSource = grid;
            GridView5.DataBind();
            InfoGrid5 = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del Indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridIndicador(clsMacroproceso objMPIn, ref string strErrMsg)
        {
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsIndicadorBLL cIndicador = new clsIndicadorBLL();

            lstIndicador = cIndicador.mtdConsultarIndicador(1, objMPIn, ref strErrMsg);

            if (lstIndicador != null)
            {
                GridView5.DataSource = lstIndicador;
                GridView5.DataBind();
            }
        }
        #endregion

        #endregion

        #region CRUD
        /// <summary>
        /// Realiza la insercion del macroproceso
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        private bool mtdInsertarMacroproceso(ref string strErrMsg)
        {
            bool booResult = false;
            clsMacroproceso objMacroproceso = new clsMacroproceso(0,
                txtNombre.Text.Trim(),
                TxtDescripcion.Text.Trim(),
                txtObjetivo.Text.Trim(),
                ChBEstado.Checked,
                Convert.ToInt32(lblIdDependencia4.Text.Trim()),
                Convert.ToInt32(ddlCadenaValor.SelectedValue.ToString().Trim()),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty, string.Empty);

            clsMacroProcesoBLL cMacroProceso = new clsMacroProcesoBLL();

            booResult = cMacroProceso.mtdInsertarMacroproceso(objMacroproceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            ddlCadenaValor.Focus();

            txtId.Enabled = false;
            ChBEstado.Enabled = false;

            btnImgInsertar.Visible = false;
            btnImgActualizar.Visible = true;

            btnCancelarConsultar.Visible = false;
            btnCancelarConsultar.Enabled = false;

            filaGrid.Visible = false;
            filaDetalle.Visible = true;
            filaConsulta.Visible = false;

            // Carga los datos en la respectiva caja de texto
            txtId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            txtNombre.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();
            ddlCadenaValor.SelectedValue = InfoGrid.Rows[RowGrid][2].ToString().Trim();
            TxtDescripcion.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim();
            txtObjetivo.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][6].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][8].ToString().Trim();
            lblIdDependencia4.Text = InfoGrid.Rows[RowGrid][9].ToString().Trim();
            tbxResponsable.Text = InfoGrid.Rows[RowGrid][10].ToString().Trim();
            tbxResponsable.Enabled = false;
            //imgDependencia4.Visible = false;
            txtFecha.Text = InfoGrid.Rows[RowGrid][11].ToString().Trim();
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarMacroproceso(ref string strErrMsg)
        {
            bool booResult = false;
            clsMacroproceso objMacroproceso = new clsMacroproceso(
                Convert.ToInt32(txtId.Text.Trim()),
                txtNombre.Text.Trim(),
                TxtDescripcion.Text.Trim(),
                txtObjetivo.Text.Trim(),
                ChBEstado.Checked,
                Convert.ToInt32(lblIdDependencia4.Text.Trim()),
                Convert.ToInt32(ddlCadenaValor.SelectedValue.ToString().Trim()),
                0, string.Empty,
                string.Empty);

            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();

            booResult = cMacroproceso.mtdActualizarMacroproceso(objMacroproceso, ref strErrMsg);

            return booResult;
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
                bool booEstado = InfoGrid.Rows[RowGrid][6].ToString().Trim() == "True" ? true : false;

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} el Macroproceso?", strEstado);
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

            clsMacroproceso objMacroproceso = new clsMacroproceso(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty, string.Empty, string.Empty,
                !(InfoGrid.Rows[RowGrid][6].ToString().Trim() == "True" ? true : false),
                0, 0, 0, string.Empty, string.Empty);

            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();

            booResult = cMacroproceso.mtdActualizarEstado(objMacroproceso, ref strErrMsg);

            return booResult;
        }

        private void mtdConsultarMP()
        {
            string strErrMsg = string.Empty;

            filaGrid.Visible = false;
            filaDetalle.Visible = false;
            filaConsulta.Visible = true;

            btnCancelarConsultar.Visible = true;
            btnCancelarConsultar.Enabled = true;

            if (mtdCargarProceso("MP", InfoGrid.Rows[RowGrid][0].ToString().Trim(), ref strErrMsg))
                omb.ShowMessage(strErrMsg, 3, "Atención");
        }

        private bool mtdCargarProceso(string strProceso, string strIdProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCaracterizacion objCaracter = new clsCaracterizacion(0,
                mtdObtenerTipoProceso(strProceso), Convert.ToInt32(strIdProceso), 0, string.Empty);

            clsMacroproceso objMPIn = new clsMacroproceso(Convert.ToInt32(strIdProceso),
                string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty, string.Empty),
                objMPOut;
            clsMacroProcesoBLL cMacroP = new clsMacroProcesoBLL();
            #endregion

            objMPOut = cMacroP.mtdConsultarMacroproceso(objMPIn, ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;
            else
            {
                mtdLlenarCamposMP(objMPOut);

                if (mtdCargarEntradas(objCaracter, ref strErrMsg))
                    booResult = true;

                if (!booResult)
                    if (mtdCargarActividades(objCaracter, ref strErrMsg))
                        booResult = true;

                if (!booResult)
                    if (mtdCargarSalidas(objCaracter, ref strErrMsg))
                        booResult = true;

                if (!booResult)
                    if (mtdCargarIndicador(objMPIn, ref strErrMsg))
                        booResult=true;

                if (!booResult)
                    if (mtdCargarRiesgos(objMPIn, ref strErrMsg))
                        booResult = true;
            }

            return booResult;
        }

        private int mtdObtenerTipoProceso(string strProceso)
        {
            int intResult = 0;

            switch (strProceso)
            {
                case "MP":
                    intResult = 1;
                    break;
                case "P":
                    intResult = 2;
                    break;
                case "SP":
                    intResult = 3;
                    break;
            }

            return intResult;
        }

        private void mtdLlenarCamposMP(clsMacroproceso objMPIn)
        {
            tbxCadenaValor.Text = objMPIn.strNombreCadenaValor;
            tbxMacroproceso.Text = objMPIn.strNombreMacroproceso;
            tbxDescripcion.Text = objMPIn.strDescripcion;
            tbxObjetivo.Text = objMPIn.strObjetivo;
            tbxCargo.Text = objMPIn.strNombreResponsable;

            mtdHabilitarCampos(true);
        }

        void mtdHabilitarCampos(bool booEstado)
        {
            Proceso.Visible = !booEstado;
            SubProceso.Visible = !booEstado;
            lblProceso.Visible = !booEstado;
            lblSubproceso.Visible = !booEstado;
            tbxProceso.Visible = !booEstado;
            tbxSubProceso.Visible = !booEstado;
        }
        #endregion

        protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridMacroproceso(ref strErrMsg);
        }

        #endregion

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex2 = e.NewPageIndex;
            GridView2.PageIndex = PagIndex2;
            GridView2.DataBind();
            string strErrMsg = "";
            clsCaracterizacion objCaracter = new clsCaracterizacion(0,
                mtdObtenerTipoProceso("MP"), Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()), 0, string.Empty);
            mtdLoadInfoGridEntrada(objCaracter,ref strErrMsg);
        }
    }
}