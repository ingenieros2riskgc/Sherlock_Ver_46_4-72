using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.IO;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class Subproceso : System.Web.UI.UserControl
    {
        string IdFormulario = "4004";
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

                    if (mtdLoadDDLCadenaValor(ref strErrMsg))
                    {
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                        strErrMsg = string.Empty;
                    }

                    if (mtdCargarSubProceso(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");

                    TreeNodeCollection nodes = this.TreeView2.Nodes;
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
                        mtdConsultarSP();
                        break;
                }
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error" + "<br/>" + "Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }
        #endregion

        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        #endregion

        #region Buttons

        protected void imgBtnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                mtdResetCampos();

                ddlCadenaValor.Focus();

                ChBEstado.Enabled = true;
                txtId.Enabled = false;

                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;

                filaGrid.Visible = false;
                filaDetalle.Visible = true;
                filaConsulta.Visible = false;
            }

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                #region Inserta Subproceso
                if (mtdInsertarSubproceso(ref strErrMsg))
                {
                    omb.ShowMessage("subproceso registrado exitosamente.", 3, "Atención");

                    filaGrid.Visible = true;
                    filaDetalle.Visible = false;
                    filaConsulta.Visible = false;

                    if (TreeView2.SelectedNode != null)
                        TreeView2.SelectedNode.Selected = false;

                    mtdResetCampos();
                    mtdCargarSubProceso(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");

                #endregion Insert
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar el subproceso." + "<br/>" + "Descripción: " +
                    except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            filaConsulta.Visible = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            mtdResetCampos();
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
                    if (mtdActualizarSubproceso(ref strErrMsg))
                    {
                        omb.ShowMessage("Subproceso modificado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;
                        filaConsulta.Visible = false;

                        mtdResetCampos();
                        mtdCargarSubProceso(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }

                if (TreeView2.SelectedNode != null)
                    TreeView2.SelectedNode.Selected = false;
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al modificar el subproceso. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
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
                        omb.ShowMessage("Subproceso (ina)ctivado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;
                        filaConsulta.Visible = false;

                        mtdResetCampos();
                        mtdCargarSubProceso(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al (in)activar el subproceso. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnCancelarConsultar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            filaConsulta.Visible = false;
            btnCancelarConsultar.Visible = false;
            btnCancelarConsultar.Enabled = false;

            mtdResetCamposC();
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

        private void mtdResetCampos()
        {
            txtId.Text = string.Empty;
            TxtDescripcion.Text = string.Empty;
            txtObjetivo.Text = string.Empty;
            txtResponsable.Text = string.Empty;
            txtNombre.Text = string.Empty;
            lblIdDependencia.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtUsuario.Text = string.Empty;

            ChBEstado.Checked = true;

            ddlCadenaValor.SelectedValue = "0";
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
        }

        private void mtdResetCamposC()
        {
            tbxCadenaValor.Text = string.Empty;
            tbxMacroproceso.Text = string.Empty;
            tbxProceso.Text = string.Empty;
            tbxDescripcion.Text = string.Empty;
            tbxObjetivo.Text = string.Empty;
            tbxCargo.Text = string.Empty;
        }

        #endregion

        #region Cargas
        #region Loads DDLs
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
                    //else
                    //    booResult = true;
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

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLMacroProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCadenaValor objCadenaValor = new clsCadenaValor();
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                objCadenaValor = new clsCadenaValor(Convert.ToInt32(ddlCadenaValor.SelectedValue.ToString().Trim()), string.Empty, true, 0, string.Empty, string.Empty);
                lstMacroproceso = cMacroproceso.mtdConsultarMacroproceso(true, objCadenaValor, ref strErrMsg);

                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstMacroproceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsMacroproceso objMacroproceso in lstMacroproceso)
                        {
                            ddlMacroproceso.Items.Insert(intCounter, new ListItem(objMacroproceso.strNombreMacroproceso, objMacroproceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los macroprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {
                objMProceso = new clsMacroproceso(Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()), string.Empty, string.Empty, string.Empty,
                    true, 0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty);
                lstProceso = cProceso.mtdConsultarProceso(true, objMProceso, ref strErrMsg);

                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstProceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsProceso objProceso in lstProceso)
                        {
                            ddlProceso.Items.Insert(intCounter, new ListItem(objProceso.strNombreProceso, objProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        #endregion

        #region Treeview
        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeView(int numTV)
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData, numTV);
        }

        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        private DataTable GetTreeViewData()
        {
            // Get JerarquiaOrganizacional table
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional]";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        /// <summary>
        /// Filter the data to get only the rows that have a
        /// null ParentID (these are the top-level TreeView items)
        /// </summary>
        private void AddTopTreeViewNodes(DataTable treeViewData, int numTV)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                if (numTV == 2)
                    TreeView2.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        /// <summary>
        /// Recursively add child TreeView items by filtering by ParentID
        /// </summary>
        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private string DetalleNodo(int tipoSelect, string idHijo)
        {
            string Detalle = "", selectCommand = "",
                conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

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

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            string subcadena = TreeView2.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);

            txtResponsable.Text = TreeView2.SelectedNode.Text;
            lblIdDependencia.Text = TreeView2.SelectedNode.Value;
        }
        #endregion

        #region Gridview 1
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarSubProceso(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridSubProceso();
            mtdLoadInfoGridSubProceso(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridSubProceso()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreSubproceso", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("strObjetivo", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdCargoResponsable", typeof(string));
            grid.Columns.Add("strCargoResponsable", typeof(string));
            grid.Columns.Add("intIdProceso", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("intIdMacroProceso", typeof(string));
            grid.Columns.Add("intIdCadenaValor", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del proceso al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridSubProceso(ref string strErrMsg)
        {
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();

            lstSubproceso = cSubproceso.mtdConsultarSubProceso(ref strErrMsg);

            if (lstSubproceso != null)
            {
                mtdLoadInfoGridProceso(lstSubproceso);
                GridView1.DataSource = lstSubproceso;
                GridView1.PageIndex = PagIndex;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSubproceso">Lista con los Subprocesos</param>
        private void mtdLoadInfoGridProceso(List<clsSubproceso> lstSubProceso)
        {
            foreach (clsSubproceso objSubProceso in lstSubProceso)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objSubProceso.intId.ToString().Trim(),
                    objSubProceso.strNombreSubproceso.ToString().Trim(),
                    objSubProceso.strDescripcion.ToString().Trim(),                   
                    objSubProceso.strObjetivo.ToString().Trim(),                    
                    objSubProceso.booEstado.ToString().Trim(),
                    objSubProceso.intIdCargoResponsable.ToString().Trim(),
                    objSubProceso.strCargoResponsable.ToString().Trim(),
                    objSubProceso.intIdProceso.ToString().Trim(),
                    objSubProceso.strNombreProceso.ToString().Trim(),
                    objSubProceso.intIdMacroProceso.ToString().Trim(),
                    objSubProceso.intIdCadenaValor.ToString().Trim(),
                    objSubProceso.dtFechaRegistro.ToString().Trim(),
                    objSubProceso.intIdUsuario.ToString().Trim(),
                    objSubProceso.strNombreUsuario.ToString().Trim()});
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
        /// Hace el llamado y la instancia de los campos de la cadena de valor al grid.
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
        /// Hace el llamado y la instancia de los campos de la actividad al grid.
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
        /// Hace el llamado y la instancia de los campos de la cadena de valor al grid.
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
        private bool mtdCargarRiesgos(clsSubproceso objSubpIn, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridRiesgos();
            mtdLoadInfoGridRiesgos(objSubpIn, ref strErrMsg);

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
        private void mtdLoadInfoGridRiesgos(clsSubproceso objSubpIn, ref string strErrMsg)
        {
            List<clsRiesgos> lstRiesgo = new List<clsRiesgos>();
            clsRiesgoBLL cRiesgo = new clsRiesgoBLL();

            lstRiesgo = cRiesgo.mtdConsultarRiesgosSP(objSubpIn, ref strErrMsg);

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
        private bool mtdCargarIndicador(clsSubproceso objSubPIn, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridIndicador();
            mtdLoadInfoGridIndicador(objSubPIn, ref strErrMsg);

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
        private void mtdLoadInfoGridIndicador(clsSubproceso objSubPIn, ref string strErrMsg)
        {
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsIndicadorBLL cIndicador = new clsIndicadorBLL();

            lstIndicador = cIndicador.mtdConsultarIndicador(3, objSubPIn, ref strErrMsg);

            if (lstIndicador != null)
            {
                GridView5.DataSource = lstIndicador;
                GridView5.DataBind();
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Realiza la insercion del Subproceso
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        private bool mtdInsertarSubproceso(ref string strErrMsg)
        {
            bool booResult = false;

            clsSubproceso objSubproceso = new clsSubproceso(0,
                txtNombre.Text.Trim(),
                TxtDescripcion.Text.Trim(),
                txtObjetivo.Text.Trim(),
                ChBEstado.Checked,
                Convert.ToInt32(lblIdDependencia.Text.Trim()),
                Convert.ToInt32(ddlProceso.SelectedValue.Trim()),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();

            booResult = cSubproceso.mtdInsertarSubproceso(objSubproceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            string strErrMsg = string.Empty;

            ChBEstado.Enabled = false;
            txtId.Enabled = false;

            btnImgInsertar.Visible = false;
            btnImgActualizar.Visible = true;
            btnCancelarConsultar.Visible = false;
            btnCancelarConsultar.Enabled = false;

            filaGrid.Visible = false;
            filaDetalle.Visible = true;
            filaConsulta.Visible = false;

            txtId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            txtNombre.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();
            TxtDescripcion.Text = InfoGrid.Rows[RowGrid][2].ToString().Trim();
            txtObjetivo.Text = InfoGrid.Rows[RowGrid][3].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][4].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            lblIdDependencia.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();
            txtResponsable.Text = InfoGrid.Rows[RowGrid][6].ToString().Trim();

            ddlCadenaValor.SelectedValue = InfoGrid.Rows[RowGrid][10].ToString().Trim();

            #region Macroproceso
            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");

            ddlMacroproceso.SelectedValue = InfoGrid.Rows[RowGrid][9].ToString().Trim();

            #endregion

            #region Proceso
            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");

            ddlProceso.SelectedValue = InfoGrid.Rows[RowGrid][7].ToString().Trim();
            #endregion

            txtUsuario.Text = InfoGrid.Rows[RowGrid][13].ToString().Trim();
            txtFecha.Text = InfoGrid.Rows[RowGrid][11].ToString().Trim();
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarSubproceso(ref string strErrMsg)
        {
            bool booResult = false;

            clsSubproceso objProceso = new clsSubproceso(
                Convert.ToInt32(txtId.Text.Trim()),
                txtNombre.Text.Trim(),
                TxtDescripcion.Text.Trim(),
                txtObjetivo.Text.Trim(),
                ChBEstado.Checked,
                Convert.ToInt32(lblIdDependencia.Text.Trim()),
                Convert.ToInt32(ddlProceso.SelectedValue.Trim()),
                0, string.Empty);

            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();

            booResult = cSubproceso.mtdActualizarSubproceso(objProceso, ref strErrMsg);

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
                bool booEstado = InfoGrid.Rows[RowGrid][4].ToString().Trim() == "True" ? true : false;

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} el subproceso?", strEstado);
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

            clsSubproceso objProceso = new clsSubproceso(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty, string.Empty, string.Empty,
                !(InfoGrid.Rows[RowGrid][4].ToString().Trim() == "True" ? true : false),
                0, 0, 0, string.Empty);

            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();

            booResult = cSubproceso.mtdActualizarEstado(objProceso, ref strErrMsg);

            return booResult;
        }

        private void mtdConsultarSP()
        {
            string strErrMsg = string.Empty;

            filaGrid.Visible = false;
            filaDetalle.Visible = false;
            filaConsulta.Visible = true;

            btnCancelarConsultar.Visible = true;
            btnCancelarConsultar.Enabled = true;

            if (mtdCargarProceso("SP", InfoGrid.Rows[RowGrid][0].ToString().Trim(), ref strErrMsg))
                omb.ShowMessage(strErrMsg, 3, "Atención");
        }

        private bool mtdCargarProceso(string strProceso, string strIdProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsCaracterizacion objCaracter = new clsCaracterizacion(0,
                mtdObtenerTipoProceso(strProceso), Convert.ToInt32(strIdProceso), 0, string.Empty);

            clsSubproceso objSubpIn = new clsSubproceso(Convert.ToInt32(strIdProceso),
                string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty),
                         objSubpOut;
            clsSubprocesoBLL cSubp = new clsSubprocesoBLL();

            objSubpOut = cSubp.mtdConsultarSubProceso(objSubpIn, ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;
            else
            {
                mtdLlenarCamposSP(objSubpOut);

                if (mtdCargarEntradas(objCaracter, ref strErrMsg))
                    booResult = true;

                if (!booResult)
                    if (mtdCargarActividades(objCaracter, ref strErrMsg))
                        booResult = true;

                if (!booResult)
                    if (mtdCargarSalidas(objCaracter, ref strErrMsg))
                        booResult = true;

                if (!booResult)
                    if (mtdCargarIndicador(objSubpIn, ref strErrMsg))
                        booResult = true;

                if (!booResult)
                    if (mtdCargarRiesgos(objSubpIn, ref strErrMsg))
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

        private void mtdLlenarCamposSP(clsSubproceso objSubpIn)
        {
            tbxCadenaValor.Text = objSubpIn.strNombreCadenaValor;
            tbxMacroproceso.Text = objSubpIn.strNombreMacroProceso;
            tbxProceso.Text = objSubpIn.strNombreProceso;
            tbxSubProceso.Text = objSubpIn.strNombreSubproceso;
            tbxDescripcion.Text = objSubpIn.strDescripcion;
            tbxObjetivo.Text = objSubpIn.strObjetivo;
            tbxCargo.Text = objSubpIn.strCargoResponsable;
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridSubProceso(ref strErrMsg);
        }
    }
}