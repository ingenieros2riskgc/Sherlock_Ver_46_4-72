using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;
//using System.Data;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class PlanAccion : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "7007";
        private cGestion cGestion = new cGestion();
        private static int LastInsertIdCE;

        #region Properties

        private int pagIndex;
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

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)ViewState["infGrid"];
                return infGrid;
            }
            set
            {
                infGrid = value;
                ViewState["infGrid"] = infGrid;
            }
        }

        private DataTable infGridPlan;
        private DataTable InfoGridPlan
        {
            get
            {
                infGridPlan = (DataTable)ViewState["infGridPlan"];
                return infGridPlan;
            }
            set
            {
                infGridPlan = value;
                ViewState["infGridPlan"] = infGridPlan;
            }
        }

        private DataTable infGridOBJ;
        private DataTable InfoGridOBJ
        {
            get
            {
                infGridOBJ = (DataTable)ViewState["infGridOBJ"];
                return infGridOBJ;
            }
            set
            {
                infGridOBJ = value;
                ViewState["infGridOBJ"] = infGridOBJ;
            }
        }

        private DataTable infGridIndicador;
        private DataTable InfoGridIndicador
        {
            get
            {
                infGridIndicador = (DataTable)ViewState["infGridIndicador"];
                return infGridIndicador;
            }
            set
            {
                infGridIndicador = value;
                ViewState["infGridIndicador"] = infGridIndicador;
            }
        }

        private DataTable infGridIndicadorAs;
        private DataTable InfoGridIndicadorAs
        {
            get
            {
                infGridIndicadorAs = (DataTable)ViewState["infGridIndicadorAs"];
                return infGridIndicadorAs;
            }
            set
            {
                infGridIndicadorAs = value;
                ViewState["infGridIndicadorAs"] = infGridIndicadorAs;
            }
        }

        private DataTable infGridPA;
        private DataTable InfoGridPA
        {
            get
            {
                infGridPA = (DataTable)ViewState["infGridPA"];
                return infGridPA;
            }
            set
            {
                infGridPA = value;
                ViewState["infGridPA"] = infGridPA;
            }
        }

        private int idexRow;
        private int IdexRow
        {
            get
            {
                idexRow = (int)ViewState["idexRow"];
                return idexRow;
            }
            set
            {
                idexRow = value;
                ViewState["idexRow"] = idexRow;
            }
        }

        private int idexRowPA;
        private int IdexRowPA
        {
            get
            {
                idexRowPA = (int)ViewState["idexRowPA"];
                return idexRowPA;
            }
            set
            {
                idexRowPA = value;
                ViewState["idexRowPA"] = idexRowPA;
            }
        }

        private int idexRowAsociar;
        private int IdexRowAsociar
        {
            get
            {
                idexRowAsociar = (int)ViewState["idexRowAsociar"];
                return idexRowAsociar;
            }
            set
            {
                idexRowAsociar = value;
                ViewState["idexRowAsociar"] = idexRowAsociar;
            }
        }

        private int idexRowDesasociar;
        private int IdexRowDesasociar
        {
            get
            {
                idexRowDesasociar = (int)ViewState["idexRowDesasociar"];
                return idexRowDesasociar;
            }
            set
            {
                idexRowDesasociar = value;
                ViewState["idexRowDesasociar"] = idexRowDesasociar;
            }
        }


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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!IsPostBack)
            {
                loadGridPlan();
                cargarInfoGridPlan();
                inicializarValores();
                PopulateTreeView();
            }
        }

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView3.ExpandAll();
            TreeView2.ExpandAll();
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
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                TreeView3.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                TreeView2.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString().Trim();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView3_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox18.Text = TreeView3.SelectedNode.Text.Trim();
            Label3.Text = TreeView3.SelectedNode.Value;
        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox15.Text = TreeView2.SelectedNode.Text.Trim();
            Label55.Text = TreeView2.SelectedNode.Value;
        }
        #endregion Treeview

        private void inicializarValores()
        {
            IdexRow = 0;
            IdexRowPA = 0;
            IdexRowAsociar = 0;
            IdexRowDesasociar = 0;
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdEstrategia", typeof(string));
            grid.Columns.Add("CodigoEstrategia", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridViewEST.DataSource = grid;
            GridViewEST.DataBind();
            InfoGrid = grid;
        }

        private void loadGridPA()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlanAccion", typeof(string));
            grid.Columns.Add("CodigoPlanAccion", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFin", typeof(string));
            grid.Columns.Add("IdResponsable", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGridPA = grid;
        }

        private void loadGridIndicador()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdIndicador", typeof(string));
            grid.Columns.Add("CodigoIndicador", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Periodicidad", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGridIndicador = grid;
        }

        private void loadGridIndicadorAsociado()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdIndicador", typeof(string));
            grid.Columns.Add("CodigoIndicador", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Periodicidad", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGridIndicadorAs = grid;
        }

        private void loadGridPlan()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlan", typeof(string));
            grid.Columns.Add("CodigoPlan", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFin", typeof(string));
            GridViewPlanEstratagico.DataSource = grid;
            GridViewPlanEstratagico.DataBind();
            InfoGridPlan = grid;
        }

        private void loadGridOBJ()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdObjetivo", typeof(string));
            grid.Columns.Add("CodigoObjetivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            GridViewOBJ.DataSource = grid;
            GridViewOBJ.DataBind();
            InfoGridOBJ = grid;
        }

        private void cargarInfoGridPA()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.PlanAccion(Label49.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPA.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlanAccion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoPlanAccion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaFin"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["IdResponsable"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView1.DataSource = InfoGridPA;
                GridView1.DataBind();
            }
        }

        private void cargarInfoGridPlan()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.FiltroPlan();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPlan.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlan"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoPlan"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaFin"].ToString().Trim(),
                                                    });
                }

                GridViewPlanEstratagico.DataSource = InfoGridPlan;
                GridViewPlanEstratagico.DataBind();

            }
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.Estrategia(LabelIdOBJ.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdEstrategia"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoEstrategia"].ToString().Trim(),
                        dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        });
                }
                //GridView1.DataSource = InfoGrid;
                //GridView1.DataBind();
                GridViewEST.DataSource = InfoGrid;
                GridViewEST.DataBind();
            }
        }

        private void cargarInfoGridOBJ()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.ObjEstrategico(LabelIdPlan.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridOBJ.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdObjetivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoObjetivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                        });
                }
                GridViewOBJ.DataSource = InfoGridOBJ;
                GridViewOBJ.DataBind();
            }
        }

        private void cargarInfoGridIndicador()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.IndicadoresActivosSinAsociar();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridIndicador.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdIndicador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoIndicador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Periodicidad"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView2.DataSource = InfoGridIndicador;
                GridView2.DataBind();
            }
        }

        private void cargarInfoGridIndicadorAsociado()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.IndicadoresActivosAsociados(InfoGridPA.Rows[IdexRowPA]["IdPlanAccion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridIndicadorAs.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdIndicador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoIndicador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Periodicidad"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView3.DataSource = InfoGridIndicadorAs;
                GridView3.DataBind();
            }
        }

        protected void VerPlanEstrategico_Click(object sender, EventArgs e)
        {
            FiltroPE.Visible = true;
            loadGridPlan();
            cargarInfoGridPlan();

            FiltroAplicado.Visible = false;
            FiltroOBJ.Visible = false;
            FiltroAplicadoOBJ.Visible = false;
        }

        protected void VerObjetivos_Click(object sender, EventArgs e)
        {
            FiltroOBJ.Visible = true;
            loadGridOBJ();
            cargarInfoGridOBJ();

            TbEstrategia.Visible = false;
            FiltroAplicadoOBJ.Visible = false;
            VerPlanEstrategico.Visible = true;
            FiltroEST.Visible = false;
            FiltroAplicadoEST.Visible = false;
        }

        protected void BtnAdicionaVision_Click(object sender, ImageClickEventArgs e)
        {
            TbAdicionarVision.Visible = true;
            TbModificarVision.Visible = false;
        }

        protected void BtnAdicionaMeta_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void VerEstrategias_Click(object sender, EventArgs e)
        {
            TbEstrategia.Visible = false;
            FiltroAplicadoEST.Visible = false;
            FiltroEST.Visible = true;
            loadGrid();
            cargarInfoGrid();

            resetValues();
            VerObjetivos.Visible = true;
            VerEstrategias.Visible = false;
            TbIndicadores.Visible = false;
        }

        protected void GuardaAsoIndicadores_Click(object sender, ImageClickEventArgs e)
        {
            TbIndicadores.Visible = false;
            BtnAdicionaPlan.Visible = true;
        }
        
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowPA = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    verModificar();
                    modificar();
                    break;
                case "Eliminar":
                    eliminar();
                    break;
                case "Indicadores":
                    Indicadores();
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowAsociar = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecIndicador":
                    SelecIndicador();
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowDesasociar = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Desasociar":
                    Desasociar();
                    break;
            }
        }
        
        protected void GridViewPlanEstratagico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Filtrar":
                    filtrar();
                    filtrarplan();
                    loadGridOBJ();
                    cargarInfoGridOBJ();

                    break;
            }
        }

        protected void GridViewOBJ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "FiltrarOBJ":
                    filtrarOBJ();
                    filtrarObjetivo();
                    break;
            }
        }

        protected void GridViewEST_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "FiltrarEST":
                    filtrarEST();
                    filtrarEstrategia();
                    break;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        protected void GridViewPlanEstratagico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void Mensaje1(String Mensaje)
        {
            lblMsgBox1.Text = Mensaje;
            mpeMsgBox1.Show();
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox16.Text = "";
            TextBox17.Text = "";
            TextBox13.Text = "";
            TextBox14.Text = "";
            TextBox15.Text = "";
            TextBox18.Text = "";
            Label3.Text = "";
            TextBox19.Text = "";
            TextBox20.Text = "";
            TbAdicionarVision.Visible = false;
            TbModificarVision.Visible = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        private void modificarLista()
        {
            cGestion.modificarPA(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox13.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox14.Text.Trim()) + " 12:00:00:000", InfoGridPA.Rows[IdexRowPA]["IdPlanAccion"].ToString().Trim(), Label55.Text.Trim());
        }

        private void verModificar()
        {
            TbModificarVision.Visible = true;
            TbIndicadores.Visible = false;
        }

        private void Desasociar()
        {
            if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
            {
                Mensaje1("Plan Estratégico vencido. Solo Lectura");
            }
            else
            {
                cGestion.DesasociarIndicador(InfoGridIndicadorAs.Rows[IdexRowDesasociar]["IdIndicador"].ToString().Trim());
                FiltroAplicado.Visible = true;
                FiltroPE.Visible = false;
                FiltroOBJ.Visible = true;
                loadGridIndicadorAsociado();
                cargarInfoGridIndicadorAsociado();
                loadGridIndicador();
                cargarInfoGridIndicador();
            }
        }

        private void SelecIndicador()
        {
            if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
            {
                Mensaje1("Plan Estratégico vencido. Solo Lectura");
            }
            else
            {
                cGestion.AsociarIndicador(InfoGridPA.Rows[IdexRowPA]["IdPlanAccion"].ToString().Trim(), InfoGridIndicador.Rows[IdexRowAsociar]["IdIndicador"].ToString().Trim());
                FiltroAplicado.Visible = true;
                FiltroPE.Visible = false;
                FiltroOBJ.Visible = true;
                loadGridIndicadorAsociado();
                cargarInfoGridIndicadorAsociado();
                loadGridIndicador();
                cargarInfoGridIndicador();
            }
        }

        private void filtrar()
        {
            FiltroAplicado.Visible = true;
            FiltroPE.Visible = false;
            FiltroOBJ.Visible = true;
        }

        private void filtrarplan()
        {
            LabelIdPlan.Text = InfoGridPlan.Rows[IdexRow]["IdPlan"].ToString().Trim();
            TextBox12.Text = InfoGridPlan.Rows[IdexRow]["FechaInicio"].ToString().Trim();
            TextBox11.Text = InfoGridPlan.Rows[IdexRow]["FechaFin"].ToString().Trim();
            LabelCodigoPlan.Text = InfoGridPlan.Rows[IdexRow]["CodigoPlan"].ToString().Trim();
            LabelNombrePlan.Text = InfoGridPlan.Rows[IdexRow]["Nombre"].ToString().Trim();
        }

        private void filtrarOBJ()
        {
            FiltroAplicadoOBJ.Visible = true;
            FiltroOBJ.Visible = false;
            FiltroEST.Visible = true;
            VerPlanEstrategico.Visible = false;
            //VerPlanEstrategico.Visible = false;
        }

        private void filtrarEST()
        {
            FiltroAplicadoEST.Visible = true;
            FiltroEST.Visible = false;
            //FiltroEST.Visible = true;
            //VerPlanEstrategico.Visible = false;
            VerEstrategias.Visible = true;
            VerObjetivos.Visible = false;
        }

        private void filtrarObjetivo()
        {
            LabelIdOBJ.Text = InfoGridOBJ.Rows[IdexRow]["IdObjetivo"].ToString().Trim();
            LabelCodigoOBJ.Text = InfoGridOBJ.Rows[IdexRow]["CodigoObjetivo"].ToString().Trim();
            LabelDescOBJ.Text = InfoGridOBJ.Rows[IdexRow]["Descripcion"].ToString().Trim();
            //TbEstrategia.Visible = true;
            loadGrid();
            cargarInfoGrid();

        }

        private void filtrarEstrategia()
        {
            Label49.Text = InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim();
            Label51.Text = InfoGrid.Rows[IdexRow]["CodigoEstrategia"].ToString().Trim();
            Label53.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
            TbEstrategia.Visible = true;
            loadGridPA();
            cargarInfoGridPA();

        }

        private void modificar()
        {
            TextBox20.Text = InfoGridPA.Rows[IdexRowPA]["CodigoPlanAccion"].ToString().Trim();
            TextBox2.Text = InfoGridPA.Rows[IdexRowPA]["Descripcion"].ToString().Trim();
            TextBox13.Text = InfoGridPA.Rows[IdexRowPA]["FechaInicio"].ToString().Trim();
            TextBox14.Text = InfoGridPA.Rows[IdexRowPA]["FechaFin"].ToString().Trim();
            TextBox15.Text = InfoGridPA.Rows[IdexRowPA]["Responsable"].ToString().Trim();
            TextBox7.Text = InfoGridPA.Rows[IdexRowPA]["Usuario"].ToString().Trim();
            TextBox6.Text = InfoGridPA.Rows[IdexRowPA]["FechaRegistro"].ToString().Trim();
            Label55.Text = InfoGridPA.Rows[IdexRowPA]["IdResponsable"].ToString().Trim();
            TbAdicionarVision.Visible = false;
        }

        private void FiltrarEstrategia()
        {
            LabelIdEstrategia.Text = InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim();
            Label29.Text = InfoGrid.Rows[IdexRow]["CodigoEstrategia"].ToString().Trim();
            Label31.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();

        }

        private void eliminar()
        {
            try
            {
                if (cCuenta.permisosBorrar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                    {
                        Mensaje1("Plan Estratégico vencido. Solo Lectura");
                    }
                    else
                    {
                        cGestion.eliminarPA(InfoGridPA.Rows[IdexRowPA]["IdPlanAccion"].ToString().Trim());
                        loadGridPA();
                        cargarInfoGridPA();
                        Mensaje("Plan de Acción eliminado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al eliminar Plan de Acción. " + ex.Message);
            }
        }

        private void loadCodigo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigo("IdPlanAccion", "Gestion.PlanAccion");
                if (dtInfo.Rows.Count > 0)
                {
                    TextBox19.Text = "PA" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                }
                else
                {
                    TextBox19.Text = "PA1";
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar código. " + ex.Message);
            }
        }

        private void Indicadores()
        {
            BtnAdicionaPlan.Visible = false;
            TbIndicadores.Visible = true;
            TbModificarVision.Visible = false;
            TbAdicionarVision.Visible = false;
            loadGridIndicador();
            cargarInfoGridIndicador();
            loadGridIndicadorAsociado();
            cargarInfoGridIndicadorAsociado();
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Vars
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Vars

            try
            {
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Destinatario
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = " + idNodoJerarquia;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = row["CorreoResponsable"].ToString().Trim();
                        idJefeInmediato = row["idPadre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeInmediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeInmediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                            idJefeMediato = row["idPadre"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region correo del Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeMediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeMediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.Insert();
                #endregion Correos Enviados
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 

                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource201.Insert();
                }

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    //MailAddress fromAddress = new MailAddress("risksherlock@hotmail.com", "Software Sherlock");
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");

                    message.From = fromAddress;//here you can set address

                    #region Destinatario
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region Copia
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region Otros
                    if (Otros.Trim() != "")
                        foreach (string substr in Otros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    message.Subject = Asunto;//subject of email
                    message.IsBodyHtml = true;//To determine email body is html or not
                    message.Body = Cuerpo;

                    smtpClient.Send(message);
                    #endregion
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                }
            }

            return (err);
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        #region Plan de Accion

        protected void BtnGuardaPlan_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string TextoAdicional = "";

                TextoAdicional = "CREACIÓN DE PLAN DE ACCIÓN - GESTIÓN ESTRATÉGICA" + "<br>";
                TextoAdicional = TextoAdicional + "<br>" + "<B>Plan Estratégico</B>" + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + LabelCodigoPlan.Text + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + LabelNombrePlan.Text + "<br>";
                TextoAdicional = TextoAdicional + "<br>" + "<B>Objetivo Estratégico</B>" + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + LabelCodigoOBJ.Text + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + LabelDescOBJ.Text + "<br>";
                TextoAdicional = TextoAdicional + "<br>" + "<B>Estrategia</B>" + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + Label51.Text + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + Label53.Text + "<br>";
                TextoAdicional = TextoAdicional + "<br>" + "<B>Plan de Acción</B>" + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(TextBox19.Text) + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + Sanitizer.GetSafeHtmlFragment(TextBox1.Text) + "<br>";
                TextoAdicional = TextoAdicional + " Fecha Final : " + Sanitizer.GetSafeHtmlFragment(TextBox17.Text) + "<br>";

                //agregarLista();
                int IdRegistro = mtdAgregarPlan();
                loadGridPA();
                cargarInfoGridPA();
                boolEnviarNotificacion(11, IdRegistro, Convert.ToInt32(Label3.Text), Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()), TextoAdicional);
                Mensaje("Plan de Acción almacenado correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al guardar Plan de Acción. " + ex.Message);
            }
            resetValues();
        }

        protected void BtnCancelaPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbAdicionarVision.Visible = false;
            resetValues();
        }

        protected void BtnModificaPlan_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                    {
                        Mensaje1("Plan Estratégico vencido. Solo Lectura");
                    }
                    else
                    {
                        modificarLista();
                        loadGridPA();
                        cargarInfoGridPA();
                        resetValues();
                        Mensaje("Plan de Acción modificado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el Plan de Acción." + ex.Message);
            }
        }

        protected void BtnCancelaModPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbModificarVision.Visible = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;
        }

        protected void BtnAdicionaPlan_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                {
                    Mensaje1("Plan Estratégico vencido. Solo Lectura");
                }
                else
                {
                    TbAdicionarVision.Visible = true;
                    TbModificarVision.Visible = false;
                    TextBox1.Focus();
                    TextBox8.Text = Session["loginUsuario"].ToString().Trim();
                    TextBox9.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    loadCodigo();
                }
            }
        }

        private void agregarLista()
        {
            cGestion.agregarPAccion(Sanitizer.GetSafeHtmlFragment(TextBox19.Text.Trim()), Label49.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox16.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()) + " 12:00:00:000", Label3.Text.Trim());
        }

        private int mtdAgregarPlan()
        {
            //cGestion.agregarPAccion(TextBox19.Text.Trim(), Label49.Text.Trim(), TextBox1.Text.Trim(), TextBox16.Text.Trim() + " 12:00:00:000", TextBox17.Text.Trim() + " 12:00:00:000", Label3.Text.Trim());

            int intIdRegistro = 0;
            DataTable dtInfo = new DataTable();

            dtInfo = cGestion.mtdAgregarPlan(Sanitizer.GetSafeHtmlFragment(TextBox19.Text.Trim()), Label49.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox16.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()) + " 12:00:00:000", Label3.Text.Trim());

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intIdRegistro = Convert.ToInt32(dtInfo.Rows[0][0].ToString());
            }

            return intIdRegistro;
        }

        #endregion Plan de Accion
    }
}