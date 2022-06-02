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
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class Proceso : System.Web.UI.UserControl
    {
        string IdFormulario = "4003";
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

                    if (mtdCargarProceso(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");

                    mtdCargarAreas();
                    mtdLoadDDLEmpresa();

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
                        mtdConsultarP();
                        break;
                }
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error" + "<br/>" + "Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }
        #endregion Gridview

        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        #endregion

        #region Buttons

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                mtdResetValores();

                ddlEmpresa.Focus();

                txtId.Enabled = false;
                ChBEstado.Enabled = true;
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
                #region Inserta PROCESO
                if (mtdInsertarProceso(ref strErrMsg))
                {
                    omb.ShowMessage("Proceso registrado exitosamente.", 3, "Atención");

                    filaGrid.Visible = true;
                    filaDetalle.Visible = false;
                    filaConsulta.Visible = false;

                    if (TreeView2.SelectedNode != null)
                        TreeView2.SelectedNode.Selected = false;

                    mtdResetValores();
                    mtdCargarProceso(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");

                #endregion Insert
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar el proceso. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            filaConsulta.Visible = false;

            mtdLimpiarAreas();

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;
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
                    if (mtdActualizarProceso(ref strErrMsg))
                    {
                        omb.ShowMessage("Proceso modificado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;
                        filaConsulta.Visible = false;

                        mtdResetValores();
                        mtdCargarProceso(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }

                if (TreeView2.SelectedNode != null)
                    TreeView2.SelectedNode.Selected = false;
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al modificar el proceso. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
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
                        omb.ShowMessage("Proceso (in)activado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        filaDetalle.Visible = false;
                        filaConsulta.Visible = false;

                        mtdResetValores();
                        mtdCargarProceso(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al (in)activar el proceso. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
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

        #region Eventos de Botones Area
        protected void BtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                ListBox2.Items.Insert(i, new ListItem(ListBox1.Items[i].Text, ListBox1.Items[i].Value));
            }
            ListBox1.Items.Clear();
            ListBox1.Visible = false;
            ListBox2.Visible = true;
        }

        protected void BtnSelectOne_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem != null)
            {
                int Treg = ListBox2.Items.Count;
                if (ListBox1.SelectedIndex != -1)
                {
                    if (Treg == 0)
                        ListBox2.Items.Insert(0, new ListItem(ListBox1.SelectedItem.Text, ListBox1.SelectedItem.Value));
                    else
                        ListBox2.Items.Insert(Treg, new ListItem(ListBox1.SelectedItem.Text, ListBox1.SelectedItem.Value));

                    ListBox1.Items.Remove(ListBox1.SelectedItem);
                }
                ListBox2.ClearSelection();
                ListBox2.Visible = true;
            }
            if (ListBox1.Items.Count == 0)
                ListBox1.Visible = false;
        }

        protected void BtnUnSelectOne_Click(object sender, EventArgs e)
        {
            ListBox1.Visible = true;
            if (ListBox2.SelectedItem != null)
            {
                if (ListBox2.SelectedIndex != -1)
                {
                    int Treg = ListBox1.Items.Count;
                    if (Treg == 0)
                        ListBox1.Items.Insert(0, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));
                    else
                        ListBox1.Items.Insert(Treg, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));

                    ListBox2.Items.Remove(ListBox2.SelectedItem);
                }
                ListBox1.ClearSelection();
            }
            if (ListBox2.Items.Count == 0)
                ListBox2.Visible = false;
        }

        protected void BtnUnSelectAll_Click(object sender, EventArgs e)
        {
            ListBox1.Visible = true;
            ListBox2.Visible = false;
            if (ListBox2.Items.Count > 0)
            {
                for (int i = 0; i < ListBox2.Items.Count; i++)
                {
                    ListBox2.SelectedIndex = i;
                    ListBox1.Items.Insert(i, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));
                }
                ListBox2.Items.Clear();
            }
        }
        #endregion Eventos de Botones Area

        #endregion

        #region Metodos
        #region Limpiar Campos
        private void mtdResetValores()
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            TxtDescripcion.Text = string.Empty;
            txtObjetivo.Text = string.Empty;
            txtResponsable.Text = string.Empty;
            lblIdDependencia.Text = string.Empty;

            ChBEstado.Checked = true;

            txtUsuario.Text = string.Empty;
            txtFecha.Text = string.Empty;
            ddlEmpresa.SelectedValue = "0";
            ddlCadenaValor.SelectedValue = "0";
            ddlMacroproceso.SelectedValue = "0";
            txtContextoExt.Text = string.Empty;
            txtContextoInt.Text = string.Empty;
        }

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
            tbxCadenaValor.Text = string.Empty;
            tbxMacroproceso.Text = string.Empty;
            tbxProceso.Text = string.Empty;
            tbxDescripcion.Text = string.Empty;
            tbxObjetivo.Text = string.Empty;
            tbxCargo.Text = string.Empty;
        }
        #endregion

        private void Mensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Cargas
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
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());

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

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtResponsable.Text = TreeView2.SelectedNode.Text;
            lblIdDependencia.Text = TreeView2.SelectedNode.Value;
        }
        #endregion Treeview

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
        /// Realiza el cargue de empresas
        /// </summary>
        private void mtdLoadDDLEmpresa()
        {
            DataTable dtInfo = new DataTable();
            cRiesgo cRiesgo = new cRiesgo();

            try
            {
                dtInfo = cRiesgo.mtdLoadEmpresa(true);
                ddlEmpresa.Items.Insert(0, new ListItem("", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlEmpresa.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Empresas. " + ex.Message);
            }
        }
        #endregion

        #region Load Areas

        /// <summary>
        /// Procedimiento para cargar las areas asociadas
        /// </summary>
        private void mtdCargarAreas()
        {
            DataTable DtInfo = new DataTable();
            cRiesgo clsRiesgo = new cRiesgo();

            try
            {
                DtInfo = clsRiesgo.mtdCargarInfoAreas();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox1.Items.Insert(i, new ListItem(DtInfo.Rows[i]["NombreArea"].ToString().Trim(), DtInfo.Rows[i]["IdArea"].ToString()));
                    ListBox1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Areas." + ex.Message);
            }
        }

        private void mtdManipularAreas(string strLista)
        {
            string[] strAreas = strLista.Split(',');
            ListBox lsbTemp = ListBox1, lsbTemp2 = new ListBox();

            foreach (ListItem liArea in ListBox1.Items)
            {
                lsbTemp2.Items.Add(liArea);
            }

            foreach (string strArea in strAreas)
            {
                if (!string.IsNullOrEmpty(strArea))
                {
                    for (int i = 0; i < lsbTemp.Items.Count; i++)
                    {
                        if (lsbTemp.Items[i].Value == strArea)
                            ListBox1.Items.Remove(lsbTemp.Items[i]);
                    }

                    foreach (ListItem liArea in lsbTemp2.Items)
                    {
                        if (liArea.Value == strArea)
                        {
                            lsbTemp.Items.Remove(liArea);
                            ListBox2.Items.Add(liArea);
                            ListBox2.Visible = true;
                        }
                    }
                }
            }
        }

        private void mtdLimpiarAreas()
        {
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            mtdCargarAreas();
        }
        #endregion Areas

        #region Gridview 1
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarProceso(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridProceso();
            mtdLoadInfoGridProceso(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridProceso()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("intIdMacroProceso", typeof(string));
            grid.Columns.Add("intIdCadenaValor", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("strObjetivo", typeof(string));
            grid.Columns.Add("intCargoResponsable", typeof(string));
            grid.Columns.Add("strCargoResponsable", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intIdEmpresa", typeof(string));
            grid.Columns.Add("strArea", typeof(string));
            grid.Columns.Add("strContextoInt", typeof(string));
            grid.Columns.Add("strContextExt", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del proceso al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridProceso(ref string strErrMsg)
        {
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();

            lstProceso = cProceso.mtdConsultarProceso(ref strErrMsg);

            if (lstProceso != null)
            {
                mtdLoadInfoGridProceso(lstProceso);
                GridView1.DataSource = lstProceso;
                GridView1.PageIndex = PagIndex;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstProceso">Lista con los procesos</param>
        private void mtdLoadInfoGridProceso(List<clsProceso> lstProceso)
        {
            foreach (clsProceso objProceso in lstProceso)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objProceso.intId.ToString().Trim(),
                    objProceso.strNombreProceso.ToString().Trim(),
                    objProceso.intIdMacroProceso.ToString().Trim(),                   
                    objProceso.intIdCadenaValor.ToString().Trim(),                    
                    objProceso.strDescripcion.ToString().Trim(),
                    objProceso.strObjetivo.ToString().Trim(),
                    objProceso.intCargoResponsable.ToString().Trim(),
                    objProceso.strCargoResponsable.ToString().Trim(),
                    objProceso.booEstado.ToString().Trim(),
                    objProceso.intIdUsuario.ToString().Trim(),
                    objProceso.strNombreUsuario.ToString().Trim(),
                    objProceso.dtFechaRegistro.ToString().Trim(),
                    objProceso.intIdEmpresa.ToString().Trim(),
                    objProceso.strArea.ToString().Trim().Trim(),
                    objProceso.ContextoInterno.ToString().Trim().Trim(),
                    objProceso.ContextoExterno.ToString().Trim().Trim()
                    
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
        private bool mtdCargarRiesgos(clsProceso objProcIn, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridRiesgos();
            mtdLoadInfoGridRiesgos(objProcIn, ref strErrMsg);

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
        private void mtdLoadInfoGridRiesgos(clsProceso objProcIn, ref string strErrMsg)
        {
            List<clsRiesgos> lstRiesgo = new List<clsRiesgos>();
            clsRiesgoBLL cRiesgo = new clsRiesgoBLL();

            lstRiesgo = cRiesgo.mtdConsultarRiesgosP(objProcIn, ref strErrMsg);

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
        private bool mtdCargarIndicador(clsProceso objPIn, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridIndicador();
            mtdLoadInfoGridIndicador(objPIn, ref strErrMsg);

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
        private void mtdLoadInfoGridIndicador(clsProceso objPIn, ref string strErrMsg)
        {
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsIndicadorBLL cIndicador = new clsIndicadorBLL();

            lstIndicador = cIndicador.mtdConsultarIndicador(2, objPIn, ref strErrMsg);

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
        /// Realiza la insercion del Proceso
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        private bool mtdInsertarProceso(ref string strErrMsg)
        {
            bool booResult = false;
            string strListaArea = string.Empty, strComma = string.Empty;

            foreach (ListItem liArea in ListBox2.Items)
            {
                strListaArea += strComma + liArea.Value.ToString();
                strComma = ",";
            }

            clsProceso objProceso = new clsProceso(0,
                Convert.ToInt32(ddlEmpresa.SelectedValue.Trim()),
                txtNombre.Text.Trim(),
                TxtDescripcion.Text.Trim(),
                txtObjetivo.Text.Trim(),
                strListaArea,
                Convert.ToInt32(lblIdDependencia.Text.Trim()),
                Convert.ToInt32(ddlMacroproceso.SelectedValue.Trim()),
                ChBEstado.Checked,
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty,
                txtContextoInt.Text.Trim(),
                txtContextoExt.Text.Trim());
            clsProcesoBLL cProceso = new clsProcesoBLL();

            booResult = cProceso.mtdInsertarProceso(objProceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            string strErrMsg = string.Empty;

            ddlEmpresa.Focus();

            ChBEstado.Enabled = false;
            txtId.Enabled = false;

            btnImgInsertar.Visible = false;
            btnImgActualizar.Visible = true;
            btnCancelarConsultar.Visible = false;
            btnCancelarConsultar.Enabled = false;

            filaGrid.Visible = false;
            filaDetalle.Visible = true;
            filaConsulta.Visible = false;

            // Carga los datos en la respectiva caja de texto
            txtId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            ddlCadenaValor.SelectedValue = InfoGrid.Rows[RowGrid][3].ToString().Trim();
            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");

            ddlMacroproceso.SelectedValue = InfoGrid.Rows[RowGrid][2].ToString().Trim();

            txtNombre.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();
            TxtDescripcion.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim();
            txtObjetivo.Text = InfoGrid.Rows[RowGrid][5].ToString().Trim();

            lblIdDependencia.Text = InfoGrid.Rows[RowGrid][6].ToString().Trim();
            txtResponsable.Text = InfoGrid.Rows[RowGrid][7].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][8].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            txtUsuario.Text = InfoGrid.Rows[RowGrid][10].ToString().Trim();
            txtFecha.Text = InfoGrid.Rows[RowGrid][11].ToString().Trim();
            txtContextoInt.Text = InfoGrid.Rows[RowGrid][14].ToString().Trim();
            txtContextoExt.Text =InfoGrid.Rows[RowGrid][15].ToString().Trim();

            ddlEmpresa.SelectedValue = InfoGrid.Rows[RowGrid][12].ToString().Trim();

            mtdManipularAreas(InfoGrid.Rows[RowGrid][13].ToString().Trim());
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarProceso(ref string strErrMsg)
        {
            bool booResult = false;
            string strListaArea = string.Empty, strComma = string.Empty;

            foreach (ListItem liArea in ListBox2.Items)
            {
                strListaArea += strComma + liArea.Value.ToString();
                strComma = ",";
            }

            clsProceso objProceso = new clsProceso(Convert.ToInt32(txtId.Text.Trim()),
                Convert.ToInt32(ddlEmpresa.SelectedValue.Trim()),
                txtNombre.Text.Trim(),
                TxtDescripcion.Text.Trim(),
                txtObjetivo.Text.Trim(),
                strListaArea,
                Convert.ToInt32(lblIdDependencia.Text.Trim()),
                Convert.ToInt32(ddlMacroproceso.SelectedValue.Trim()),
                ChBEstado.Checked,
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty,
                txtContextoInt.Text.Trim(),
                txtContextoExt.Text.Trim());
            clsProcesoBLL cProceso = new clsProcesoBLL();

            booResult = cProceso.mtdActualizarProceso(objProceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite mostar el mensaje de activar o inactivar el proceso.
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
                bool booEstado = InfoGrid.Rows[RowGrid][8].ToString().Trim() == "True" ? true : false;

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} el proceso?", strEstado);
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

            clsProceso objProceso = new clsProceso(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0,
                !(InfoGrid.Rows[RowGrid][8].ToString().Trim() == "True" ? true : false),
                0, string.Empty,string.Empty,string.Empty);
            clsProcesoBLL cProceso = new clsProcesoBLL();

            booResult = cProceso.mtdActualizarEstado(objProceso, ref strErrMsg);

            return booResult;
        }

        private void mtdConsultarP()
        {
            string strErrMsg = string.Empty;

            filaGrid.Visible = false;
            filaDetalle.Visible = false;
            filaConsulta.Visible = true;

            btnCancelarConsultar.Visible = true;
            btnCancelarConsultar.Enabled = true;

            if (mtdCargarProceso("P", InfoGrid.Rows[RowGrid][0].ToString().Trim(), ref strErrMsg))
                omb.ShowMessage(strErrMsg, 3, "Atención");
        }

        private bool mtdCargarProceso(string strProceso, string strIdProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsCaracterizacion objCaracter = new clsCaracterizacion(0,
                mtdObtenerTipoProceso(strProceso), Convert.ToInt32(strIdProceso), 0, string.Empty);

            clsProceso objProcIn = new clsProceso(Convert.ToInt32(strIdProceso),
                0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty),
                         objProcOut;
            clsProcesoBLL cProc = new clsProcesoBLL();

            objProcOut = cProc.mtdConsultarProceso(objProcIn, ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;
            else
            {
                mtdLlenarCamposP(objProcOut);

                if (mtdCargarEntradas(objCaracter, ref strErrMsg))
                    booResult = true;

                if (!booResult)
                    if (mtdCargarActividades(objCaracter, ref strErrMsg))
                        booResult = true;

                if (!booResult)
                    if (mtdCargarSalidas(objCaracter, ref strErrMsg))
                        booResult = true;

                if (!booResult)
                    if (mtdCargarIndicador(objProcIn, ref strErrMsg))
                        booResult = true;

                if (!booResult)
                    if (mtdCargarRiesgos(objProcIn, ref strErrMsg))
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

        private void mtdLlenarCamposP(clsProceso objProcIn)
        {
            tbxCadenaValor.Text = objProcIn.strNombreCadenaValor;
            tbxMacroproceso.Text = objProcIn.strNombreMacroProceso;
            tbxProceso.Text = objProcIn.strNombreProceso;
            tbxDescripcion.Text = objProcIn.strDescripcion;
            tbxObjetivo.Text = objProcIn.strObjetivo;
            tbxCargo.Text = objProcIn.strCargoResponsable;

            mtdHabilitarCampos(true);
        }

        void mtdHabilitarCampos(bool booEstado)
        {
            SubProceso.Visible = !booEstado;
            lblSubproceso.Visible = !booEstado;
            tbxSubProceso.Visible = !booEstado;
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridProceso(ref strErrMsg);
        }

        #endregion
    }
}