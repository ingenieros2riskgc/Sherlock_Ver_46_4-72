using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class ParGruposTrabajo : System.Web.UI.UserControl
    {
        string IdFormulario = "5001";
        cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private int rowGrid;
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

        private DataTable infoGrid;
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

        private int pagIndexInfoGrid;
        private int PagIndexInfoGrid
        {
            get
            {
                pagIndexInfoGrid = (int)ViewState["pagIndexInfoGrid"];
                return pagIndexInfoGrid;
            }
            set
            {
                pagIndexInfoGrid = value;
                ViewState["pagIndexInfoGrid"] = pagIndexInfoGrid;
            }
        }

        private int rowGridRecursos;
        private int RowGridRecursos
        {
            get
            {
                rowGridRecursos = (int)ViewState["rowGridRecursos"];
                return rowGridRecursos;
            }
            set
            {
                rowGridRecursos = value;
                ViewState["rowGridRecursos"] = rowGridRecursos;
            }
        }

        private DataTable infoGridRecursos;
        private DataTable InfoGridRecursos
        {
            get
            {
                infoGridRecursos = (DataTable)ViewState["infoGridRecursos"];
                return infoGridRecursos;
            }
            set
            {
                infoGridRecursos = value;
                ViewState["infoGridRecursos"] = infoGridRecursos;
            }
        }

        private int pagIndexInfoGridRecursos;
        private int PagIndexInfoGridRecursos
        {
            get
            {
                pagIndexInfoGridRecursos = (int)ViewState["pagIndexInfoGridRecursos"];
                return pagIndexInfoGridRecursos;
            }
            set
            {
                pagIndexInfoGridRecursos = value;
                ViewState["pagIndexInfoGridRecursos"] = pagIndexInfoGridRecursos;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                inicializarValores();
                loadGrid();
                loadInfo();
                PopulateTreeView();
                PopulateTreeViewParam();
            }
        }

        #region Loads
        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdGrupoTrabajo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            InfoGrid = grid;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        private void loadInfo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParametrizacionRiesgos.loadInfoGruposTrabajo();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdGrupoTrabajo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                        dtInfo.Rows[rows]["Estado"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim()
                    });
                }
                GridView1.PageIndex = PagIndexInfoGrid;
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
            }
        }

        private void loadGridRecursos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdGrupoTrabajo", typeof(string));
            grid.Columns.Add("IdGruposTrabajoRescurso", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Correo", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            InfoGridRecursos = grid;
            GVRecursos.DataSource = InfoGridRecursos;
            GVRecursos.DataBind();
            
        }

        private void loadInfoRecursos(string IdGrupoTrabajo)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParametrizacionRiesgos.loadInfoGruposTrabajoRecursos(IdGrupoTrabajo);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRecursos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdGrupoTrabajo"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdGruposTrabajoRescurso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                        dtInfo.Rows[rows]["Correo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Estado"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim()
                    });
                }
                GVRecursos.PageIndex = pagIndexInfoGridRecursos;
                GVRecursos.DataSource = InfoGridRecursos;
                GVRecursos.DataBind();
            }
        }
        #endregion

        #region GridView
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGrid = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGrid;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGrid) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    resetValuesCampos();
                    detalleRegistro();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                    {
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    }
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "GrupoTrabajo";
                    }
                    break;
                case "Recursos":
                    if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    {
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    }
                    else
                    {
                        loadGridRecursos();
                        loadInfoRecursos(InfoGrid.Rows[RowGrid]["IdGrupoTrabajo"].ToString().Trim());
                        trRecursos.Visible = true;
                    }
                    break;
            }
        }

        protected void GVRecursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndexInfoGridRecursos = e.NewPageIndex;
            GVRecursos.PageIndex = pagIndexInfoGridRecursos;
            GVRecursos.DataSource = InfoGridRecursos;
            GVRecursos.DataBind();
        }

        protected void GVRecursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridRecursos = (Convert.ToInt16(GVRecursos.PageSize) * pagIndexInfoGridRecursos) + Convert.ToInt16(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ModificarRecurso":
                    resetValuesCamposRecursos();
                    TrRecurosNuevos.Visible = true;
                    detalleRegistroRecurso();
                    break;
                case "BorrarRecurso":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                    {
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    }
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "BorrarRecurso";
                    }
                    break;
            }
        }
        #endregion

        #region Buttons
        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            switch (lbldummyOkNo.Text.Trim())
            {
                case "GrupoTrabajo":
                    try
                    {
                        resetValuesCampos();
                        borrarRegistro();
                        loadGrid();
                        loadInfo();
                        Mensaje("Información eliminada con éxito.");
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error al eliminar la información.<br/>El grupo seleccionado contiene recursos  asignados. <br/>Para eliminar un grupo, primero se deben eliminar todos sus recursos.");
                    }
                    break;
                case "BorrarRecurso":
                    try
                    {
                        resetValuesCamposRecursos();
                        borrarRegistroRecursos();
                        loadGridRecursos();
                        loadInfoRecursos(InfoGrid.Rows[RowGrid]["IdGrupoTrabajo"].ToString().Trim());
                        Mensaje("Información eliminada con éxito.");
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error al eliminar la información." + ex.Message);
                    }
                    break;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                resetValuesCampos();
                ImageButton2.Visible = true;
                trCampos.Visible = true;
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesCampos();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    cParametrizacionRiesgos.agregarRegistroGruposTrabajo(TxbNombre.Text.Trim(), DdlEstado.SelectedItem.Value.ToString());
                    resetValuesCampos();
                    loadGrid();
                    loadInfo();
                    Mensaje("Registro agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el registro. " + ex.Message);
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    cParametrizacionRiesgos.modificarRegistroGruposTrabajo(InfoGrid.Rows[RowGrid]["IdGrupoTrabajo"].ToString().Trim(), TxbNombre.Text.Trim(), DdlEstado.SelectedValue.ToString());
                    resetValuesCampos();
                    loadGrid();
                    loadInfo();
                    Mensaje("Registro modificado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el registro. " + ex.Message);
            }
        }
        #endregion

        private void inicializarValores()
        {
            PagIndexInfoGrid = 0;
            pagIndexInfoGridRecursos = 0;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void borrarRegistro()
        {
            cParametrizacionRiesgos.borrarRegistroGrupoTrabajo(InfoGrid.Rows[RowGrid]["IdGrupoTrabajo"].ToString().Trim());
        }

        private void borrarRegistroRecursos()
        {
            cParametrizacionRiesgos.borrarRegistroGrupoTrabajoRecurso(InfoGridRecursos.Rows[RowGridRecursos]["IdGruposTrabajoRescurso"].ToString().Trim());
        }

        private void detalleRegistro()
        {
            TxbCodigo.Text = InfoGrid.Rows[RowGrid]["IdGrupoTrabajo"].ToString().Trim();
            TxbNombre.Text = InfoGrid.Rows[RowGrid]["Nombre"].ToString().Trim();
            if (InfoGrid.Rows[RowGrid]["Estado"].ToString().Trim() == "Activo")
                DdlEstado.SelectedIndex = 1;
            else
                DdlEstado.SelectedIndex = 0;

            ImageButton3.Visible = true;
            trCampos.Visible = true;
        }

        private void detalleRegistroRecurso()
        {

            TxbNombreRecurso.Text = InfoGridRecursos.Rows[RowGridRecursos]["Nombre"].ToString().Trim();
            TxbCorreo.Text = InfoGridRecursos.Rows[RowGridRecursos]["Correo"].ToString().Trim();
            
            if (InfoGridRecursos.Rows[RowGridRecursos]["Estado"].ToString().Trim() == "Activo")
                DdlEstadoRecurso.SelectedIndex = 1;
            else
                DdlEstadoRecurso.SelectedIndex = 0;
            ImageButton6.Visible = true;
            ImageButton5.Visible = false;
        }

        private void resetValuesCampos()
        {
            trCampos.Visible = false;
            TxbCodigo.Text = string.Empty;
            TxbNombre.Text = string.Empty;
            DdlEstado.SelectedIndex = 1;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
        }

        private void resetValuesCamposRecursos()
        {
            TrRecurosNuevos.Visible = false;
            TxbCorreo.Text = string.Empty;
            TxbNombreRecurso.Text = string.Empty;
            DdlEstadoRecurso.SelectedIndex = 1;
            ImageButton5.Visible = false;
            ImageButton6.Visible = false;
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesCamposRecursos();
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                resetValuesCamposRecursos();
                ImageButton5.Visible = true;
                TrRecurosNuevos.Visible = true;
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    cParametrizacionRiesgos.agregarRegistroGruposTrabajoRecurso(InfoGrid.Rows[RowGrid]["IdGrupoTrabajo"].ToString().Trim(), TxbNombreRecurso.Text.Trim(), TxbCorreo.Text.Trim(), DdlEstadoRecurso.SelectedValue.ToString());
                    resetValuesCamposRecursos();
                    resetValuesCampos();
                    loadGridRecursos();
                    loadInfoRecursos(InfoGrid.Rows[RowGrid]["IdGrupoTrabajo"].ToString().Trim());
                    Mensaje("Registro agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el registro. " + ex.Message);
            }
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    cParametrizacionRiesgos.modificarRegistroGruposTrabajoRecurso(InfoGridRecursos.Rows[RowGridRecursos]["IdGruposTrabajoRescurso"].ToString().Trim(), TxbNombreRecurso.Text.Trim(), TxbCorreo.Text.Trim(), DdlEstadoRecurso.SelectedValue.ToString());
                    resetValuesCamposRecursos();
                    loadGridRecursos();
                    loadInfoRecursos(InfoGrid.Rows[RowGrid]["IdGrupoTrabajo"].ToString().Trim());
                    Mensaje("Registro modificado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el registro. " + ex.Message);
            }
        }

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeViewJerarquiaOrg.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT Parametrizacion.JerarquiaOrganizacional.IdHijo, Parametrizacion.JerarquiaOrganizacional.IdPadre, Parametrizacion.JerarquiaOrganizacional.NombreHijo, Parametrizacion.DetalleJerarquiaOrg.NombreResponsable, Parametrizacion.DetalleJerarquiaOrg.CorreoResponsable FROM Parametrizacion.JerarquiaOrganizacional LEFT JOIN Parametrizacion.DetalleJerarquiaOrg ON Parametrizacion.JerarquiaOrganizacional.idHijo = Parametrizacion.DetalleJerarquiaOrg.idHijo";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreResponsable"].ToString(), row["IdHijo"].ToString());
                TreeViewJerarquiaOrg.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreResponsable"].ToString(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeViewJerarquiaOrg_SelectedNodeChanged(object sender, EventArgs e)
        {
            TxbNombreRecurso.Text = TreeViewJerarquiaOrg.SelectedNode.Text;
            TxbCorreo.Text = cParametrizacionRiesgos.loadCorreoJerarquiaOrg(TreeViewJerarquiaOrg.SelectedNode.Value);
        }

        private void PopulateTreeViewParam()
        {
            DataTable treeViewData = GetTreeViewDataParam();
            AddTopTreeViewNodesParam(treeViewData);
            TreeViewTablaParam.ExpandAll();
        }

        private DataTable GetTreeViewDataParam()
        {
            string selectCommand = "SELECT Nombre,IdGruposTrabajoParam,Correo FROM Riesgos.GruposTrabajoParam where Estado = 1";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodesParam(DataTable treeViewData)
        {

            DataView view = new DataView(treeViewData);
            //view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["Nombre"].ToString(), row["IdGruposTrabajoParam"].ToString());
                newNode.ToolTip = "Nombre: " + row["Nombre"].ToString() + "\rCorreo: " + row["Correo"].ToString();
                TreeViewTablaParam.Nodes.Add(newNode);
                //AddChildTreeViewNodesParam(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodesParam(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            //view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["Nombre"].ToString(), row["IdGruposTrabajoParam"].ToString());
                newNode.ToolTip = "Nombre: " + row["Nombre"].ToString() + "\rCorreo: " + row["Correo"].ToString();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodesParam(treeViewData, newNode);
            }
        }

        protected void TreeViewTablaParam_SelectedNodeChanged(object sender, EventArgs e)
        {
            TxbNombreRecurso.Text = TreeViewTablaParam.SelectedNode.Text;
            TxbCorreo.Text = cParametrizacionRiesgos.loadCorreoTablaParam(TreeViewTablaParam.SelectedNode.Value);
        }
        #endregion Treeview

        protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
        {
            trRecursos.Visible = false;
        }

    }
}