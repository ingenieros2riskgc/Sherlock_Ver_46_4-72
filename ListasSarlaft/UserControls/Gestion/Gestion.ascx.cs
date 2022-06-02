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
using System.IO;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class Gestion : System.Web.UI.UserControl
    {
        #region VARIABLES GLOBALES

        string IdFormulario = "7010";
        string IdGestion, IdPlan, Dia, Mes, Ano;

        private cGestion cGestion = new cGestion();
        private static int LastInsertIdCE;

        cCuenta cCuenta = new cCuenta();

        DataTable DtResIndicador = new DataTable();
        DataTable DtResIndicadorPeriodo = new DataTable();
        DataTable DtResTottalCumplimiento = new DataTable();
        DataTable DtResIndicadorPA = new DataTable();
        DataTable DtResIndicadorES = new DataTable();
        DataTable DtPeriodos = new DataTable();
        DataTable dtVerResultados = new DataTable();
        DataTable dtVerMeta = new DataTable();
        DataTable VerPeriodoCerrado = new DataTable();
        #endregion VARIABLES GLOBALES

        #region Properties

        private DataTable infoGridControles;
        private DataTable InfoGridControles
        {
            get
            {
                infoGridControles = (DataTable)ViewState["infoGridControles"];
                return infoGridControles;
            }
            set
            {
                infoGridControles = value;
                ViewState["infoGridControles"] = infoGridControles;
            }
        }

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

        private DataTable infGridIndicadores;
        private DataTable InfoGridIndicadores
        {
            get
            {
                infGridIndicadores = (DataTable)ViewState["infGridIndicadores"];
                return infGridIndicadores;
            }
            set
            {
                infGridIndicadores = value;
                ViewState["infGridIndicadores"] = infGridIndicadores;
            }
        }

        private DataTable infGridGestion;
        private DataTable InfoGridGestion
        {
            get
            {
                infGridGestion = (DataTable)ViewState["infGridGestion"];
                return infGridGestion;
            }
            set
            {
                infGridGestion = value;
                ViewState["infGridGestion"] = infGridGestion;
            }
        }

        private DataTable infoGridVariable;
        private DataTable InfoGridVariable
        {
            get
            {
                infoGridVariable = (DataTable)ViewState["infoGridVariable"];
                return infoGridVariable;
            }
            set
            {
                infoGridVariable = value;
                ViewState["infoGridVariable"] = infoGridVariable;
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

        private int idexRowGestion;
        private int IdexRowGestion
        {
            get
            {
                idexRowGestion = (int)ViewState["idexRowGestion"];
                return idexRowGestion;
            }
            set
            {
                idexRowGestion = value;
                ViewState["idexRowGestion"] = idexRowGestion;
            }
        }

        private int idexRowPa;
        private int IdexRowPa
        {
            get
            {
                idexRowPa = (int)ViewState["idexRowPa"];
                return idexRowPa;
            }
            set
            {
                idexRowPa = value;
                ViewState["idexRowPa"] = idexRowPa;
            }
        }

        private int idexRowIndicador;
        private int IdexRowIndicador
        {
            get
            {
                idexRowIndicador = (int)ViewState["idexRowIndicador"];
                return idexRowIndicador;
            }
            set
            {
                idexRowIndicador = value;
                ViewState["idexRowIndicador"] = idexRowIndicador;
            }
        }

        private int idexRowIndicadorPAI;
        private int IdexRowIndicadorPAI
        {
            get
            {
                idexRowIndicadorPAI = (int)ViewState["idexRowIndicadorPAI"];
                return idexRowIndicadorPAI;
            }
            set
            {
                idexRowIndicadorPAI = value;
                ViewState["idexRowIndicadorPAI"] = idexRowIndicadorPAI;
            }
        }

        private int idexRowVariable;
        private int IdexRowVariable
        {
            get
            {
                idexRowVariable = (int)ViewState["idexRowVariable"];
                return idexRowVariable;
            }
            set
            {
                idexRowVariable = value;
                ViewState["idexRowVariable"] = idexRowVariable;
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

        private DataTable infGridPAI;
        private DataTable InfoGridPAI
        {
            get
            {
                infGridPAI = (DataTable)ViewState["infGridPAI"];
                return infGridPAI;
            }
            set
            {
                infGridPAI = value;
                ViewState["infGridPAI"] = infGridPAI;
            }
        }

        private DataTable infGridPAIComentarios;
        private DataTable InfoGridPAIComentarios
        {
            get
            {
                infGridPAIComentarios = (DataTable)ViewState["infGridPAIComentarios"];
                return infGridPAIComentarios;
            }
            set
            {
                infGridPAIComentarios = value;
                ViewState["infGridPAIComentarios"] = infGridPAIComentarios;
            }
        }

        private int rowGridArchivoControl;

        private int RowGridArchivoControl
        {
            get
            {
                rowGridArchivoControl = (int)ViewState["rowGridArchivoControl"];
                return rowGridArchivoControl;
            }
            set
            {
                rowGridArchivoControl = value;
                ViewState["rowGridArchivoControl"] = rowGridArchivoControl;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!IsPostBack)
            {
                loadGridPlan();
                cargarInfoGridPlan();
                inicializarValores();
                PopulateTreeView();
            }
        }

        private void inicializarValores()
        {
            IdexRow = 0;
            IdexRowGestion = 0;
            IdexRowPa = 0;
            IdexRowIndicador = 0;
            IdexRowVariable = 0;
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
            grid.Columns.Add("Abierto_SN", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGridPA = grid;
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

        private void loadGridGestion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdGestion", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridViewGestiones.DataSource = grid;
            GridViewGestiones.DataBind();
            InfoGridGestion = grid;
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

        private void loadGridIndicador()
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
            InfoGridIndicadores = grid;
        }

        private void loadGridVariable()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdVariable", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Formato", typeof(string));
            grid.Columns.Add("Valor", typeof(string));
            grid.Columns.Add("Dia", typeof(string));
            grid.Columns.Add("Mes", typeof(string));
            grid.Columns.Add("Ano", typeof(string));
            GridViewVariables.DataSource = grid;
            GridViewVariables.DataBind();
            InfoGridVariable = grid;
        }

        private void loadGridArchivoControl()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("IdGestion", typeof(string));
            grid.Columns.Add("IdPlanAccion", typeof(string));
            grid.Columns.Add("NombreArchivo", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            InfoGridControles = grid;
            GridViewArchivos.DataSource = InfoGridControles;
            GridViewArchivos.DataBind();
        }

        private void loadInfoArchivoControl()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.loadInfoArchivoGestion(IdGestion, IdPlan);

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridControles.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdGestion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdPlanAccion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreArchivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim()
                                                                 });
                }
                GridViewArchivos.DataSource = InfoGridControles;
                GridViewArchivos.DataBind();
            }
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
                                                    dtInfo.Rows[rows]["Abierto_SN"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView1.DataSource = InfoGridPA;
                GridView1.DataBind();
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
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdEstrategia"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoEstrategia"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridViewEST.DataSource = InfoGrid;
                GridViewEST.DataBind();
            }
        }

        private void cargarInfoGridGestion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.Gestion(LabelIdPlanAccion.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridGestion.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdGestion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }

                GridViewGestiones.DataSource = InfoGridGestion;
                GridViewGestiones.DataBind();

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

        private void cargarInfoGridOBJ()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.ObjEstrategico(LabelIdPlan.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridOBJ.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdObjetivo"].ToString().Trim(),
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
            dtInfo = cGestion.IndicadoresActivosAsociados(InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridIndicadores.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdIndicador"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["CodigoIndicador"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["Periodicidad"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                               });
                }
                GridView3.DataSource = InfoGridIndicadores;
                GridView3.DataBind();
            }
        }

        private void cargarInfoGridVariable()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.ValoresVariables(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridVariable.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdVariable"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Formato"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Valor"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Dia"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Mes"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Ano"].ToString().Trim(),
                                                            });
                }
                GridViewVariables.DataSource = InfoGridVariable;
                GridViewVariables.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowPa = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "FiltrarPA":
                    modificar();
                    break;
                case "Indicadores":
                    Indicadores();
                    break;
            }
        }

        protected void GridViewGestiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowGestion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ModificarGestion":
                    ModificarGestion();
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowIndicador = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecVariables":
                    SelecVariables();
                    break;
                case "SelecPAI":
                    VerPAI();
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowIndicadorPAI = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ModificarPAI":
                    VerModificarPAI();
                    VerComentariosPAI();
                    break;
            }
        }

        protected void GridViewArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoControl = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    //descargarArchivo();
                    mtdDescargarPdfGestion();
                    break;
            }
        }

        protected void GridViewVariables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowVariable = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Valores":
                    Valores();
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

        protected void GridViewPlanEstratagico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView3.ExpandAll();
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
            TextBox18.Text = TreeView3.SelectedNode.Text;
            Label73.Text = TreeView3.SelectedNode.Value;
        }

        protected void VerPlanEstrategico_Click(object sender, EventArgs e)
        {
            FiltroPE.Visible = true;
            loadGridPlan();
            cargarInfoGridPlan();
            FiltroAplicado.Visible = false;
            FiltroOBJ.Visible = false;
            FiltroAplicadoOBJ.Visible = false;
            TbAdicionarGestion.Visible = false;
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
            TbArchivos.Visible = false;
        }

        protected void VerEstrategias_Click(object sender, EventArgs e)
        {
            TbFiltroPlanAccion.Visible = false;
            FiltroAplicadoEST.Visible = false;
            TbEstrategia.Visible = false;
            FiltroEST.Visible = true;
            VerObjetivos.Visible = true;
            TbIndicadores.Visible = false;
            TbVariables.Visible = false;
            TbIndicadorSeleccionado.Visible = false;
        }

        protected void BtnGuardaGestion_Click(object sender, ImageClickEventArgs e)
        {
            String a = Sanitizer.GetSafeHtmlFragment(TextBox1.Text);
            try
            {
                cGestion.agregarGestion(LabelIdPlanAccion.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()));
                cGestion.modificarPlanAccion(DropDownListEstadoPA1.SelectedItem.Value.ToString().Trim(), LabelIdPlanAccion.Text.Trim());
                TbAdicionarGestion.Visible = false;
                loadGridGestion();
                cargarInfoGridGestion();
                loadGridPA();
                cargarInfoGridPA();
                BtnAddGestion.Visible = true;
                resetValues();
                if (DropDownListEstadoPA1.SelectedItem.Value == "N")
                {
                    try
                    {
                        string TextoAdicional = "";
                        TextoAdicional = "CIERRE DE PLAN DE ACCIÓN - GESTIÓN ESTRATÉGICA" + "<br>";
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
                        TextoAdicional = TextoAdicional + " Código : " + Label29.Text + "<br>";
                        TextoAdicional = TextoAdicional + " Nombre : " + Label31.Text + "<br>";
                        TextoAdicional = TextoAdicional + " Descripción de la gestión : " + a + "<br>";
                        TextoAdicional = TextoAdicional + " Fecha de cierre : " + System.DateTime.Now.ToString("yyyy-MM-dd") + "<br>";
                        //14/11/2014
                        boolEnviarNotificacion(11, 1, Convert.ToInt32(Label61.Text), "", TextoAdicional, "1");
                        Label60.Text = DropDownListEstadoPA1.SelectedItem.Value.ToString().Trim();
                    }
                    catch (Exception ex)
                    {
                        Mensaje1("Error al enviar notificación Plan de Acción. " + ex.Message);
                    }
                }

                Mensaje("Gestión almacenada correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar Gestión " + ex.Message);
            }
        }

        protected void BtnCancelaModPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbModificarGestion.Visible = false;
            BtnAddGestion.Visible = true;
            TbArchivos.Visible = false;
        }

        protected void BtnAddGestion_Click(object sender, ImageClickEventArgs e)
        {
            if (Label60.Text == "S")
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
                        TbAdicionarGestion.Visible = true;
                        BtnAddGestion.Visible = false;
                        TextBox8.Text = Session["loginUsuario"].ToString().Trim();
                        TextBox9.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
            }
            else
            {
                Mensaje1("No se pueden adicionar más gestiones. Plan de Acción cerrado.");
            }
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
                        cGestion.modificarGestion(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), InfoGridGestion.Rows[IdexRowGestion]["IdGestion"].ToString().Trim());
                        cGestion.modificarPlanAccion(DropDownListEstadoPA.SelectedItem.Value.ToString().Trim(), LabelIdPlanAccion.Text.Trim());
                        TbModificarGestion.Visible = false;
                        BtnAddGestion.Visible = true;
                        loadGridGestion();
                        cargarInfoGridGestion();
                        loadGridPA();
                        cargarInfoGridPA();
                        Mensaje("Gestión modificada correctamente.");
                        if (DropDownListEstadoPA.SelectedItem.Value == "N")
                        {
                            try
                            {
                                string TextoAdicional = "";
                                TextoAdicional = "CIERRE DE PLAN DE ACCIÓN - GESTIÓN ESTRATÉGICA" + "<br>";
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
                                TextoAdicional = TextoAdicional + " Código : " + Label29.Text + "<br>";
                                TextoAdicional = TextoAdicional + " Nombre : " + Label31.Text + "<br>";
                                TextoAdicional = TextoAdicional + " Descripción de la gestión : " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text) + "<br>";
                                TextoAdicional = TextoAdicional + " Fecha de cierre : " + System.DateTime.Now.ToString("yyyy-MM-dd") + "<br>";
                                //14/11/2014
                                //boolEnviarNotificacion(12, 1, Convert.ToInt32(Label61.Text), "", TextoAdicional);
                                boolEnviarNotificacion(11, 1, Convert.ToInt32(Label61.Text), "", TextoAdicional, "1");
                            }
                            catch (Exception ex)
                            {
                                Mensaje1("Error al enviar notificación Plan de Acción. " + ex.Message);
                            }
                            Label60.Text = DropDownListEstadoPA.SelectedItem.Value.ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al modificar Gestión." + ex.Message);
            }
        }

        protected void BtnCancelaPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbAdicionarGestion.Visible = false;
            BtnAddGestion.Visible = true;
            resetValues();
            TbArchivos.Visible = false;
        }

        protected void VerPlanAccion_Click(object sender, EventArgs e)
        {
            TbFiltroPlanAccion.Visible = false;
            TbEstrategia.Visible = true;
            BtnVerEstrategias.Visible = true;
            TbGestionesPA.Visible = false;
            TbArchivos.Visible = false;
        }

        protected void CerrarIndicadores_Click(object sender, ImageClickEventArgs e)
        {
            TbIndicadores.Visible = false;
        }

        protected void CerrarVariables_Click(object sender, ImageClickEventArgs e)
        {
            TbVariables.Visible = false;
            CerrarIndicadores.Visible = true;
            TbIndicadorSeleccionado.Visible = false;
            TbFechaPeriodo.Visible = false;
        }

        protected void BtnCancelarValores_Click(object sender, ImageClickEventArgs e)
        {
            TbAsignarValores.Visible = false;
            //ocultarperiodicidad();
            resetValues();
        }

        protected void BtnAsignarValores_Click(object sender, ImageClickEventArgs e)
        {
            if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
            {
                Mensaje1("Plan Estratégico vencido. Solo Lectura");
            }
            else
            {
                try
                {
                    CalcularDiaMesAno(Label45.Text.Trim());
                    VerPeriodoCerrado = cGestion.VerPeriodoCerrado(Mes, Ano);
                    if (VerPeriodoCerrado.Rows[0]["Periodos"].ToString() == "1")
                    {
                        Mensaje1("El perido indicado|" + Mes + "-" + Ano + "| se encuentra cerrado.");
                    }
                    else
                    {
                        verResultados();
                        String a = dtVerResultados.Rows[0]["ContadorResultados"].ToString().Trim();
                        if (dtVerResultados.Rows[0]["ContadorResultados"].ToString() == "0")
                        {

                            if (InfoGridVariable.Rows[IdexRowVariable]["Valor"].ToString().Trim() == "")
                            {
                                cGestion.agregarVariableValor(InfoGridVariable.Rows[IdexRowVariable]["IdVariable"].ToString().Trim());
                            }
                            cGestion.AsignarValoresDia(InfoGridVariable.Rows[IdexRowVariable]["IdVariable"].ToString().Trim(), TextBox3.Text.Trim(), Dia, Mes, Ano);


                            loadGridVariable();
                            cargarInfoGridVariable();
                            TbAsignarValores.Visible = false;
                            resetValues();
                            Mensaje("Valor modificado correctamente.");
                        }
                        else
                        {
                            Mensaje1("Ya existen valores ingresados para el periodo selecionado. |" + Mes + "-" + Ano + "|");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Mensaje1("Error al modificar Valor." + ex.Message);
                }
            }
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                Mensaje1("Plan Estratégico vencido. Solo Lectura");
            else
            {
                if (Label60.Text == "S")
                {
                    IdGestion = "";
                    IdPlan = "";
                    IdGestion = InfoGridGestion.Rows[IdexRowGestion]["IdGestion"].ToString().Trim();
                    IdPlan = LabelIdPlanAccion.Text;
                    try
                    {
                        if (FileUpload1.HasFile)
                        {
                            if (Path.GetExtension(FileUpload1.FileName).ToLower() == ".pdf")
                            {
                                mtdCargarPdfGestion();
                                loadGridArchivoControl();
                                loadInfoArchivoControl();
                                Mensaje("Archivo cargado exitósamente.");
                            }
                            else
                                Mensaje("Unicamente archivos .pdf");
                        }
                        else
                            Mensaje("No hay archivos para cargar.");
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error al actualizar la lista. " + ex.Message);
                    }
                }
                else
                    Mensaje1("No se pueden adjuntar más archivos a la Gestión. Plan de Acción cerrado.");
            }
        }

        protected void BtnActualizarPeriodo_Click(object sender, EventArgs e)
        {
            if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
            {
                Mensaje1("Plan Estratégico vencido. Solo Lectura");
            }
            else
            {
                DataTable Variables = new DataTable();
                DataTable Resultados = new DataTable();
                DataTable IndicadorResultado = new DataTable();
                DataTable DtResIndicadorHoy = new DataTable();
                DataTable DtResIndicadorPeriodoHoy = new DataTable();
                DataTable DtPeriodosHoy = new DataTable();
                DataTable DtResTottalCumplimientoHoy = new DataTable();
                DataTable DtResIndicadorPAHoy = new DataTable();
                DataTable DtIndiadoresHoy = new DataTable();
                DataTable CantVariables = new DataTable();

                CalcularDiaMesAno(Label45.Text.Trim());
                VerPeriodoCerrado = cGestion.VerPeriodoCerrado(Mes, Ano);

                //Validamos si el periodo se encuenta cerrado
                if (VerPeriodoCerrado.Rows[0]["Periodos"].ToString() == "1")
                {
                    Mensaje1("El perido indicado |" + Mes + "-" + Ano + "| se encuentra cerrado.");
                }
                else
                {
                    verMeta();
                    //Validamos si la meta está creadda
                    if (dtVerMeta.Rows.Count > 0)
                    {
                        Variables = cGestion.CantidadVariables(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim());
                        Resultados = cGestion.CantidadResultados(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
                        IndicadorResultado = cGestion.CantidadIndicadorResultados(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
                        try
                        {
                            if (Variables.Rows[0]["CantVariables"].ToString() == Resultados.Rows[0]["CantVariablesResultado"].ToString())
                            {
                                //Validamos si ya existen valores ingresados para el indicador
                                //validamos si solo tiene una variable
                                CantVariables = cGestion.CantidadVariables(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim());
                                if (IndicadorResultado.Rows.Count > 0)
                                {
                                    //modificamos el indicador hoy
                                    if (CantVariables.Rows[0]["CantVariables"].ToString().Trim() == "1")
                                        cGestion.IndicadorUnaVariableHoyUpdate(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Dia, Mes, Ano);
                                    else
                                        cGestion.NuevoPeriodoHoyUpdate(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim());
                                    //IndicadorUnaVariableHoyUpdate
                                }
                                else
                                {
                                    //Insertamos en indicador de hoy
                                    if (CantVariables.Rows[0]["CantVariables"].ToString().Trim() == "1")
                                        cGestion.IndicadorUnaVariablehoy(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Dia, Mes, Ano);
                                    else
                                        cGestion.NuevoPeriodoHoy(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim());
                                }

                                //calculamos cumplimiento indicador
                                DtResIndicadorHoy = cGestion.CumplimientoIndicadorHoy(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
                                //insertamos el cumplimiento del indicador
                                //Calculado si ya existen valores
                                DtIndiadoresHoy = cGestion.IndicadoresHoy(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
                                if (DtIndiadoresHoy.Rows.Count > 0)
                                {
                                    cGestion.InsertCumplimientoIndicadorHoyUpdate(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano, DtResIndicadorHoy.Rows[0]["Meta"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Resultado"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Cumplimiento"].ToString());
                                }
                                else
                                {
                                    cGestion.InsertCumplimientoIndicadorHoy(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano, DtResIndicadorHoy.Rows[0]["Meta"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Resultado"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Cumplimiento"].ToString());
                                }

                                //Validamos si ya existe cumplimiento para el indicador
                                DtPeriodosHoy = cGestion.VerCumplimientoIndicadorHoy(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Mes, Ano);
                                if (DtPeriodosHoy.Rows.Count > 0)
                                {
                                    //Actualizamos los valores actuales
                                    DtResIndicadorPeriodoHoy = cGestion.CumplimientoIndicadorPeriodoHoy(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Mes, Ano);
                                    cGestion.UpdateCumplimientoHoy("1", InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Mes, Ano, DtResIndicadorPeriodoHoy.Rows[0]["Cumplimiento"].ToString());
                                }
                                else
                                {
                                    cGestion.InsertCumplimientoHoy("1", InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano, DtResIndicadorHoy.Rows[0]["Meta"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Resultado"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Cumplimiento"].ToString());
                                }

                                //Calculamos cumplimiento Plan de acción
                                //Calcular la cantidad de indicadores
                                DtPeriodosHoy = cGestion.CantidadIndicador(InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim());
                                int TotalIndicadoresHoy = Convert.ToInt16(DtPeriodosHoy.Rows[0]["CantidadObjetos"].ToString());
                                //Calcular el total cumplimiento por indicadores
                                DtResTottalCumplimientoHoy = cGestion.TotalCumplimientoHoy("1", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Dia"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), "select IdIndicador from Gestion.Indicadores where IdPlanAccion");
                                int TotalCumplimientoIndicadorHoy = Convert.ToInt16(DtResTottalCumplimientoHoy.Rows[0]["TotalCumplimiento"].ToString());
                                String CumplimientoPAHoy = Convert.ToString(TotalCumplimientoIndicadorHoy / TotalIndicadoresHoy);
                                
                                //Insertamos cumplimiento del plan de accion
                                //Validamos si ya existe el cumplimiento para el plan de accion
                                DtResIndicadorPAHoy = cGestion.VerCumplimientoHoy("2", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Dia"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString());
                                if (DtResIndicadorPAHoy.Rows.Count < 1)
                                {
                                    //Se inserta el cumplimientoHoy
                                    cGestion.InsertCumplimientoHoy("2", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), "1", DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), "0", "0", CumplimientoPAHoy);
                                }
                                else
                                {
                                    //Se modifica el cumplimientoHoy
                                    cGestion.UpdateCumplimientoHoy("2", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), "1", DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), CumplimientoPAHoy);
                                }

                                //Calculamos cumplimieto Estrategia
                                //Calcular la cantidad de planes de accion
                                DtPeriodosHoy = cGestion.CantidadPlanAccion(InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim());
                                int TotalPlanesAccionHoy = Convert.ToInt16(DtPeriodosHoy.Rows[0]["CantidadObjetos"].ToString());
                                //Calcular el total cumplimiento por plan de accion
                                DtResTottalCumplimientoHoy = cGestion.TotalCumplimientoHoy("2", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Dia"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), "select IdPlanAccion from Gestion.PlanAccion where IdEstrategia");
                                int TotalCumplimientoPlanesAccionHoy = Convert.ToInt16(DtResTottalCumplimientoHoy.Rows[0]["TotalCumplimiento"].ToString());
                                String CumplimientoEstrategiaHoy = Convert.ToString(TotalCumplimientoPlanesAccionHoy / TotalPlanesAccionHoy);
                                //Insertamos cumplimiento de la estrategia
                                //Validamos si ya existe el cumplimiento para la estrategia
                                DtResIndicadorPAHoy = cGestion.VerCumplimientoHoy("3", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Dia"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString());
                                if (DtResIndicadorPAHoy.Rows.Count < 1)
                                {
                                    //Se inserta el cumplimiento
                                    cGestion.InsertCumplimientoHoy("3", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), "1", DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), "0", "0", CumplimientoEstrategiaHoy);
                                }
                                else
                                {
                                    //Se modifica el cumplimiento
                                    cGestion.UpdateCumplimientoHoy("3", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), "1", DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), CumplimientoEstrategiaHoy);
                                }

                                //Calculamos cumplimieto Objetivo
                                //Calcular la cantidad de estrategias
                                //DtPeriodos = cGestion.CantidadEstrategia(InfoGridOBJ.Rows[IdexRow]["IdObjetivo"].ToString().Trim());
                                DtPeriodosHoy = cGestion.CantidadEstrategia(LabelIdOBJ.Text);
                                int TotalEstrategiasHoy = Convert.ToInt16(DtPeriodosHoy.Rows[0]["CantidadObjetos"].ToString());
                                //Calcular el total cumplimiento por plan de accion
                                DtResTottalCumplimientoHoy = cGestion.TotalCumplimientoHoy("3", LabelIdOBJ.Text, DtResIndicadorHoy.Rows[0]["Dia"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), "select IdEstrategia from Gestion.Estrategias where IdObjetivo");
                                int TotalCumplimientoEstrategiaHoy = Convert.ToInt16(DtResTottalCumplimientoHoy.Rows[0]["TotalCumplimiento"].ToString());
                                String CumplimientoObjetivoHoy = Convert.ToString(TotalCumplimientoEstrategiaHoy / TotalEstrategiasHoy);
                                //Insertamos cumplimiento del objetivo
                                //Validamos si ya existe el cumplimiento para el objetivo
                                DtResIndicadorPAHoy = cGestion.VerCumplimientoHoy("4", LabelIdOBJ.Text, DtResIndicadorHoy.Rows[0]["Dia"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString());
                                if (DtResIndicadorPAHoy.Rows.Count < 1)
                                {
                                    //Se inserta el cumplimiento
                                    cGestion.InsertCumplimientoHoy("4", LabelIdOBJ.Text, "1", DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), "0", "0", CumplimientoObjetivoHoy);
                                }
                                else
                                {
                                    //Se modifica el cumplimiento
                                    cGestion.UpdateCumplimientoHoy("4", LabelIdOBJ.Text, "1", DtResIndicadorHoy.Rows[0]["Mes"].ToString().Trim(), DtResIndicadorHoy.Rows[0]["Ano"].ToString().Trim(), CumplimientoObjetivoHoy);
                                }

                                Mensaje("Indicador actualizado correctamente");
                            }
                            else
                            {
                                Mensaje1("Hacen falta resultados para algunas variables");
                            }
                        }
                        catch (Exception ex)
                        {
                            Mensaje1("Error al actualizar valores." + ex.Message);
                        }
                    }
                    else
                    {
                        Mensaje1("No es posible actualizar el indicador. Hace falta crear la meta para el periodo seleccionado |" + Mes + "-" + Ano + "|");
                    }
                }
            }
        }

        protected void BtnNuevoPeriodo_Click(object sender, EventArgs e)
        {
            if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                Mensaje1("Plan Estratégico vencido. Solo Lectura");
            else
            {
                try
                {
                    CalcularDiaMesAno(Label45.Text.Trim());
                    DataTable CantVariables = new DataTable();
                    verResultados();
                    verMeta();
                    //Se valida si ya está la meta creada
                    if (dtVerMeta.Rows.Count > 0)
                    {
                        //Se validad si ya hay valores para el periodo actual
                        String a = dtVerResultados.Rows[0]["ContadorResultados"].ToString().Trim();
                        if (dtVerResultados.Rows[0]["ContadorResultados"].ToString() == "0")
                        {
                            //ingresamos nuevo periodo para el indicador
                            //Revisamos si el indicador solo tiene una sola variable
                            CantVariables = cGestion.CantidadVariables(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim());
                            if (CantVariables.Rows[0]["CantVariables"].ToString().Trim() == "1")
                                cGestion.IndicadorUnaVariable(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Dia, Mes, Ano);
                            else
                                cGestion.NuevoPeriodo(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim());
                            //calculamos cumplimiento indicador
                            DtResIndicador = cGestion.CumplimientoIndicador(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
                            //insertamos el cumplimiento del indicador
                            Int64 CumplimientoNofificar = Convert.ToInt64(DtResIndicador.Rows[0]["Cumplimiento"].ToString());
                            if (CumplimientoNofificar < 100)
                            {
                                EnviarNotificacionCumplimientoIndicador(CumplimientoNofificar);
                            }
                            cGestion.InsertCumplimientoIndicador(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano, DtResIndicador.Rows[0]["Meta"].ToString().Trim(), DtResIndicador.Rows[0]["Resultado"].ToString().Trim(), DtResIndicador.Rows[0]["Cumplimiento"].ToString());
                            //3-2-1014 Borrarmos en hoy
                            cGestion.DeleteCumplimientoIndicador(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
                            //Validamos si ya existe cumplimiento para el indicador
                            DtPeriodos = cGestion.VerCumplimientoIndicador(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Mes, Ano);
                            if (DtPeriodos.Rows.Count > 0)
                            {
                                //Actualizamos los valores actuales
                                DtResIndicadorPeriodo = cGestion.CumplimientoIndicadorPeriodo(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Mes, Ano);
                                cGestion.UpdateCumplimiento("1", InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Mes, Ano, DtResIndicadorPeriodo.Rows[0]["Cumplimiento"].ToString());
                            }
                            else
                            {
                                cGestion.InsertCumplimiento("1", InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano, DtResIndicador.Rows[0]["Meta"].ToString().Trim(), DtResIndicador.Rows[0]["Resultado"].ToString().Trim(), DtResIndicador.Rows[0]["Cumplimiento"].ToString());
                            }
                            //3-2-1014 Borrarmos en hoy
                            cGestion.DeleteCumplimiento("1", InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
                            //Calculamos cumplimiento Plan de acción
                            //Calcular la cantidad de indicadores
                            DtPeriodos = cGestion.CantidadIndicador(InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim());
                            int TotalIndicadores = Convert.ToInt16(DtPeriodos.Rows[0]["CantidadObjetos"].ToString());
                            //Calcular el total cumplimiento por indicadores
                            DtResTottalCumplimiento = cGestion.TotalCumplimiento("1", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), DtResIndicador.Rows[0]["Dia"].ToString().Trim(), DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), "select IdIndicador from Gestion.Indicadores where IdPlanAccion");
                            int TotalCumplimientoIndicador = Convert.ToInt16(DtResTottalCumplimiento.Rows[0]["TotalCumplimiento"].ToString());
                            String CumplimientoPA = Convert.ToString(TotalCumplimientoIndicador / TotalIndicadores);

                            //Insertamos cumplimiento del plan de accion
                            //Validamos si ya existe el cumplimiento para el plan de accion
                            DtResIndicadorPA = cGestion.VerCumplimiento("2", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), DtResIndicador.Rows[0]["Dia"].ToString().Trim(), DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString());
                            if (DtResIndicadorPA.Rows.Count < 1)
                            {
                                //Se inserta el cumplimiento
                                cGestion.InsertCumplimiento("2", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), "0", "0", CumplimientoPA);
                            }
                            else
                            {
                                //Se modifica el cumplimiento
                                cGestion.UpdateCumplimiento("2", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), CumplimientoPA);
                            }
                            //3-2-1014 Borrarmos en hoy
                            cGestion.DeleteCumplimiento("2", InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim(), "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString());

                            //Calculamos cumplimieto Estrategia
                            //Calcular la cantidad de planes de accion
                            DtPeriodos = cGestion.CantidadPlanAccion(InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim());
                            int TotalPlanesAccion = Convert.ToInt16(DtPeriodos.Rows[0]["CantidadObjetos"].ToString());
                            //Calcular el total cumplimiento por plan de accion
                            DtResTottalCumplimiento = cGestion.TotalCumplimiento("2", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), DtResIndicador.Rows[0]["Dia"].ToString().Trim(), DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), "select IdPlanAccion from Gestion.PlanAccion where IdEstrategia");
                            int TotalCumplimientoPlanesAccion = Convert.ToInt16(DtResTottalCumplimiento.Rows[0]["TotalCumplimiento"].ToString());
                            String CumplimientoEstrategia = Convert.ToString(TotalCumplimientoPlanesAccion / TotalPlanesAccion);

                            //Insertamos cumplimiento de la estrategia
                            //Validamos si ya existe el cumplimiento para la estrategia
                            DtResIndicadorPA = cGestion.VerCumplimiento("3", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), DtResIndicador.Rows[0]["Dia"].ToString().Trim(), DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString());
                            if (DtResIndicadorPA.Rows.Count < 1)
                            {
                                //Se inserta el cumplimiento
                                cGestion.InsertCumplimiento("3", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), "0", "0", CumplimientoEstrategia);
                            }
                            else
                            {
                                //Se modifica el cumplimiento
                                cGestion.UpdateCumplimiento("3", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), CumplimientoEstrategia);
                            }
                            //3-2-1014 Borrarmos en hoy
                            cGestion.DeleteCumplimiento("3", InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim(), "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString());

                            //Calculamos cumplimieto Objetivo
                            //Calcular la cantidad de estrategias
                            //DtPeriodos = cGestion.CantidadEstrategia(InfoGridOBJ.Rows[IdexRow]["IdObjetivo"].ToString().Trim());
                            DtPeriodos = cGestion.CantidadEstrategia(LabelIdOBJ.Text);
                            int TotalEstrategias = Convert.ToInt16(DtPeriodos.Rows[0]["CantidadObjetos"].ToString());
                            //Calcular el total cumplimiento por plan de accion
                            DtResTottalCumplimiento = cGestion.TotalCumplimiento("3", LabelIdOBJ.Text, DtResIndicador.Rows[0]["Dia"].ToString().Trim(), DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), "select IdEstrategia from Gestion.Estrategias where IdObjetivo");
                            int TotalCumplimientoEstrategia = Convert.ToInt16(DtResTottalCumplimiento.Rows[0]["TotalCumplimiento"].ToString());
                            String CumplimientoObjetivo = Convert.ToString(TotalCumplimientoEstrategia / TotalEstrategias);
                            //Insertamos cumplimiento del objetivo
                            //Validamos si ya existe el cumplimiento para el objetivo
                            DtResIndicadorPA = cGestion.VerCumplimiento("4", LabelIdOBJ.Text, DtResIndicador.Rows[0]["Dia"].ToString().Trim(), DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString());
                            if (DtResIndicadorPA.Rows.Count < 1)
                            {
                                //Se inserta el cumplimiento
                                cGestion.InsertCumplimiento("4", LabelIdOBJ.Text, "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), "0", "0", CumplimientoObjetivo);
                            }
                            else
                            {
                                //Se modifica el cumplimiento
                                cGestion.UpdateCumplimiento("4", LabelIdOBJ.Text, "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString().Trim(), CumplimientoObjetivo);
                            }
                            //3-2-1014 Borrarmos en hoy
                            cGestion.DeleteCumplimiento("4", LabelIdOBJ.Text, "1", DtResIndicador.Rows[0]["Mes"].ToString().Trim(), DtResIndicador.Rows[0]["Ano"].ToString());

                            loadGridVariable();
                            cargarInfoGridVariable();
                            resetValues1();
                            Mensaje("Periodo inicializado correctamente");
                        }
                        else
                            Mensaje1("Ya existen valores ingresados para el periodo selecionado. |" + Mes + "-" + Ano + "|");

                    }
                    else
                        Mensaje1("No es posible iniciar un nuevo periodo. Hace falta crear la meta para el periodo seleccionado. |" + Mes + "-" + Ano + "|");
                }
                catch (Exception ex)
                {
                    Mensaje1("Errores al inicializar periodo." + ex.Message);
                }
            }
        }

        private void VerPAI()
        {
            TrVerPAI.Visible = true;
            TblPAI.Visible = true;
            Label42.Text = InfoGridIndicadores.Rows[IdexRowIndicador]["CodigoIndicador"].ToString().Trim();
            Label43.Text = InfoGridIndicadores.Rows[IdexRowIndicador]["Descripcion"].ToString().Trim();
            loadGridPAI();
            cargarInfoGridPAI();
        }

        private void VerAddPAI()
        {
            TrAddPAI.Visible = true;
            BtnNewPAI.Visible = true;
            BtnGuardaPAI.Visible = false;
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
        }

        private void VerModificarPAI()
        {
            TrAddPAI.Visible = true;
            BtnNewPAI.Visible = false;
            BtnGuardaPAI.Visible = true;
            //Cargo info PAI
            DataTable DtPai = new DataTable();
            DtPai = cGestion.ModificarPAI(InfoGridPAI.Rows[IdexRowIndicadorPAI]["CodigoPAI"].ToString().Trim());

            TextBox10.Text = DtPai.Rows[0]["CodigoPAI"].ToString().Trim();
            TextBox13.Text = DtPai.Rows[0]["Descripcion"].ToString().Trim();
            TextBox18.Text = DtPai.Rows[0]["Responsable"].ToString().Trim();
            Label73.Text = DtPai.Rows[0]["CodResponsable"].ToString().Trim();
            TextBox16.Text = DtPai.Rows[0]["FechaCompromiso"].ToString().Trim();

            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Value == DtPai.Rows[0]["Estado"].ToString())
                {
                    break;
                }
            }
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedItem.Value == DtPai.Rows[0]["Mes"].ToString())
                {
                    break;
                }
            }
            for (int i = 0; i < DropDownList3.Items.Count; i++)
            {
                DropDownList3.SelectedIndex = i;
                if (DropDownList3.SelectedItem.Value == DtPai.Rows[0]["Ano"].ToString())
                {
                    break;
                }
            }

        }

        private void VerComentariosPAI()
        {
            loadGridPAIComentarios();
            cargarInfoGridPAIComentarios();
            TrComentariosPAI.Visible = true;
        }

        private void SelecVariables()
        {
            TbVariables.Visible = true;
            TbAsignarValores.Visible = false;
            TbIndicadorSeleccionado.Visible = true;
            Label42.Text = InfoGridIndicadores.Rows[IdexRowIndicador]["CodigoIndicador"].ToString().Trim();
            Label43.Text = InfoGridIndicadores.Rows[IdexRowIndicador]["Descripcion"].ToString().Trim();
            Label45.Text = InfoGridIndicadores.Rows[IdexRowIndicador]["Periodicidad"].ToString().Trim();

            CerrarIndicadores.Visible = false;
            loadGridVariable();
            cargarInfoGridVariable();

            TbFechaPeriodo.Visible = true;

            ocultarperiodicidad();
            resetValues1();
            VerPeriodicidad();
            AsignarPeriodicidad();
        }

        private void Valores()
        {
            TextBox21.Text = InfoGridVariable.Rows[IdexRowVariable]["Nombre"].ToString().Trim();
            TextBox4.Text = InfoGridVariable.Rows[IdexRowVariable]["Formato"].ToString().Trim();
            TextBox3.Text = InfoGridVariable.Rows[IdexRowVariable]["Valor"].ToString().Trim();

            TbAsignarValores.Visible = true;
        }

        private void ModificarGestion()
        {
            TbArchivos.Visible = true;
            IdGestion = InfoGridGestion.Rows[IdexRowGestion]["IdGestion"].ToString().Trim();
            IdPlan = LabelIdPlanAccion.Text;
            loadGridArchivoControl();
            loadInfoArchivoControl();
            TbAdicionarGestion.Visible = false;
            TbModificarGestion.Visible = true;
            TextBox2.Text = InfoGridGestion.Rows[IdexRowGestion]["Descripcion"].ToString().Trim();
            TextBox7.Text = InfoGridGestion.Rows[IdexRowGestion]["Usuario"].ToString().Trim();
            TextBox6.Text = InfoGridGestion.Rows[IdexRowGestion]["FechaRegistro"].ToString().Trim();
            for (int i = 0; i < DropDownListEstadoPA.Items.Count; i++)
            {
                DropDownListEstadoPA.SelectedIndex = i;
                if (DropDownListEstadoPA.SelectedItem.Value == InfoGridPA.Rows[IdexRowPa]["Abierto_SN"].ToString().Trim())
                {
                    break;
                }
            }
            if (DropDownListEstadoPA.SelectedItem.Value == "N")
            {
                TextBox2.Enabled = false;
                DropDownListEstadoPA.Enabled = false;
                BtnModificaPlan.Visible = false;
            }
            else
            {
                TextBox2.Enabled = true;
                DropDownListEstadoPA.Enabled = true;
                BtnModificaPlan.Visible = true;
            }
            loadGridGestion();
            cargarInfoGridGestion();
        }

        private void AsignarPeriodicidad()
        {
            if (Label45.Text == "Diaria")
            {
                if (InfoGridVariable.Rows[IdexRowVariable]["Dia"].ToString().Trim() == "")
                {
                    TextBox5.Text = "";
                }
                else
                {
                    TextBox5.Text = InfoGridVariable.Rows[IdexRowVariable]["Dia"].ToString().Trim() + "/" + InfoGridVariable.Rows[IdexRowVariable]["Mes"].ToString().Trim() + "/" + InfoGridVariable.Rows[IdexRowVariable]["Ano"].ToString().Trim();
                }
            }
            if (Label45.Text == "Semanal")
            {
                for (int i = 0; i < DropDownListSemana.Items.Count; i++)
                {
                    DropDownListSemana.SelectedIndex = i;
                    if (DropDownListSemana.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Dia"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListSemana.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListMes.Items.Count; i++)
                {
                    DropDownListMes.SelectedIndex = i;
                    if (DropDownListMes.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListMes.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            if (Label45.Text == "Quincenal")
            {
                for (int i = 0; i < DropDownListQuincena.Items.Count; i++)
                {
                    DropDownListQuincena.SelectedIndex = i;
                    if (DropDownListQuincena.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Dia"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListQuincena.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListMes.Items.Count; i++)
                {
                    DropDownListMes.SelectedIndex = i;
                    if (DropDownListMes.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListMes.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            if (Label45.Text == "Mensual")
            {
                for (int i = 0; i < DropDownListMes.Items.Count; i++)
                {
                    DropDownListMes.SelectedIndex = i;
                    if (DropDownListMes.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListMes.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            if (Label45.Text == "Bimestral")
            {
                for (int i = 0; i < DropDownListBimestre.Items.Count; i++)
                {
                    DropDownListBimestre.SelectedIndex = i;
                    if (DropDownListBimestre.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListBimestre.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            if (Label45.Text == "Trimestral")
            {
                for (int i = 0; i < DropDownListTrimestre.Items.Count; i++)
                {
                    DropDownListTrimestre.SelectedIndex = i;
                    if (DropDownListTrimestre.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListTrimestre.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            if (Label45.Text == "Semestral")
            {
                for (int i = 0; i < DropDownListSemestre.Items.Count; i++)
                {
                    DropDownListSemestre.SelectedIndex = i;
                    if (DropDownListSemestre.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListSemestre.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            if (Label45.Text == "Anual")
            {
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridVariable.Rows[IdexRowVariable]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                }
            }
        }

        private void VerPeriodicidad()
        {
            if (Label45.Text == "Diaria")
            {
                Trdia.Visible = true;
                RequiredFieldValidatorDia.ValidationGroup = "UpdateValorVariable";
                Trano.Visible = false;
            }
            if (Label45.Text == "Semanal")
            {
                Trsemana.Visible = true;
                CompareValidatorSemana.ValidationGroup = "UpdateValorVariable";
                Trmes.Visible = true;
                CompareValidatorMes.ValidationGroup = "UpdateValorVariable";
                Trano.Visible = true;
                CompareValidatorAno.ValidationGroup = "UpdateValorVariable";
            }
            if (Label45.Text == "Quincenal")
            {
                Trquincena.Visible = true;
                CompareValidatorQuincena.ValidationGroup = "UpdateValorVariable";
                Trmes.Visible = true;
                CompareValidatorMes.ValidationGroup = "UpdateValorVariable";
                Trano.Visible = true;
                CompareValidatorAno.ValidationGroup = "UpdateValorVariable";
            }
            if (Label45.Text == "Mensual")
            {
                Trmes.Visible = true;
                CompareValidatorMes.ValidationGroup = "UpdateValorVariable";
                Trano.Visible = true;
                CompareValidatorAno.ValidationGroup = "UpdateValorVariable";
            }
            if (Label45.Text == "Bimestral")
            {
                Trbimiestre.Visible = true;
                CompareValidatorBimestre.ValidationGroup = "UpdateValorVariable";
                Trano.Visible = true;
                CompareValidatorAno.ValidationGroup = "UpdateValorVariable";
            }
            if (Label45.Text == "Trimestral")
            {
                Trtrimestre.Visible = true;
                CompareValidatorTrimestre.ValidationGroup = "UpdateValorVariable";
                Trano.Visible = true;
                CompareValidatorAno.ValidationGroup = "UpdateValorVariable";
            }
            if (Label45.Text == "Semestral")
            {
                Trsemestre.Visible = true;
                CompareValidatorSemestre.ValidationGroup = "UpdateValorVariable";
                Trano.Visible = true;
                CompareValidatorAno.ValidationGroup = "UpdateValorVariable";
            }
            if (Label45.Text == "Anual")
            {
                Trano.Visible = true;
                CompareValidatorAno.ValidationGroup = "UpdateValorVariable";
            }
        }

        private void ocultarperiodicidad()
        {
            Trdia.Visible = false;
            RequiredFieldValidatorDia.ValidationGroup = "";
            Trsemana.Visible = false;
            CompareValidatorSemana.ValidationGroup = "";
            Trquincena.Visible = false;
            CompareValidatorQuincena.ValidationGroup = "";
            Trmes.Visible = false;
            CompareValidatorMes.ValidationGroup = "";
            Trbimiestre.Visible = false;
            CompareValidatorBimestre.ValidationGroup = "";
            Trtrimestre.Visible = false;
            CompareValidatorTrimestre.ValidationGroup = "";
            Trsemestre.Visible = false;
            CompareValidatorSemestre.ValidationGroup = "";
            Trano.Visible = false;
            CompareValidatorAno.ValidationGroup = "";
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
        }

        private void filtrarEST()
        {
            FiltroAplicadoEST.Visible = true;
            FiltroEST.Visible = false;
            VerObjetivos.Visible = false;
        }

        private void filtrarObjetivo()
        {
            LabelIdOBJ.Text = InfoGridOBJ.Rows[IdexRow]["IdObjetivo"].ToString().Trim();
            LabelCodigoOBJ.Text = InfoGridOBJ.Rows[IdexRow]["CodigoObjetivo"].ToString().Trim();
            LabelDescOBJ.Text = InfoGridOBJ.Rows[IdexRow]["Descripcion"].ToString().Trim();
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
            LabelIdPlanAccion.Text = InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim();
            Label29.Text = InfoGridPA.Rows[IdexRowPa]["CodigoPlanAccion"].ToString().Trim();
            Label31.Text = InfoGridPA.Rows[IdexRowPa]["Descripcion"].ToString().Trim();
            TextBox19.Text = InfoGridPA.Rows[IdexRowPa]["FechaInicio"].ToString().Trim();
            TextBox20.Text = InfoGridPA.Rows[IdexRowPa]["FechaFin"].ToString().Trim();
            Label60.Text = InfoGridPA.Rows[IdexRowPa]["Abierto_SN"].ToString().Trim();
            Label61.Text = InfoGridPA.Rows[IdexRowPa]["IdResponsable"].ToString().Trim();

            TbFiltroPlanAccion.Visible = true;
            TbGestionesPA.Visible = true;
            BtnVerEstrategias.Visible = false;
            loadGridGestion();
            cargarInfoGridGestion();
            TbEstrategia.Visible = false;

            TbIndicadores.Visible = false;
            TbVariables.Visible = false;
            TbAsignarValores.Visible = false;

            TbFechaPeriodo.Visible = false;
            TbIndicadorSeleccionado.Visible = false;

        }

        private void Indicadores()
        {
            TbIndicadores.Visible = true;

            LabelIdPlanAccion.Text = InfoGridPA.Rows[IdexRowPa]["IdPlanAccion"].ToString().Trim();
            Label29.Text = InfoGridPA.Rows[IdexRowPa]["CodigoPlanAccion"].ToString().Trim();
            Label31.Text = InfoGridPA.Rows[IdexRowPa]["Descripcion"].ToString().Trim();

            loadGridIndicador();
            cargarInfoGridIndicador();

            TbVariables.Visible = false;
            TbAsignarValores.Visible = false;

            TbFechaPeriodo.Visible = false;
            TbIndicadorSeleccionado.Visible = false;

        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void Mensaje1(String Mensaje1)
        {
            lblMsgBox1.Text = Mensaje1;
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
        }

        private void resetValues1()
        {
            TextBox5.Text = "";
            DropDownListSemana.SelectedIndex = (0);
            DropDownListQuincena.SelectedIndex = (0);
            DropDownListMes.SelectedIndex = (0);
            DropDownListBimestre.SelectedIndex = (0);
            DropDownListTrimestre.SelectedIndex = (0);
            DropDownListSemestre.SelectedIndex = (0);
            DropDownListAno.SelectedIndex = (0);
        }

        private void CalcularDiaMesAno(String Periodicidad)
        {
            switch (Periodicidad)
            {
                case "Diaria":
                    DateTime fecha;
                    fecha = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(TextBox5.Text));
                    Dia = Convert.ToString(fecha.Day);
                    Mes = Convert.ToString(fecha.Month);
                    Ano = Convert.ToString(fecha.Year);
                    break;
                case "Semanal":
                    Dia = DropDownListSemana.SelectedItem.Value.ToString().Trim();
                    Mes = DropDownListMes.SelectedItem.Value.ToString().Trim();
                    Ano = DropDownListAno.SelectedItem.Value.ToString().Trim();
                    break;
                case "Quincenal":
                    Dia = DropDownListQuincena.SelectedItem.Value.ToString().Trim();
                    Mes = DropDownListMes.SelectedItem.Value.ToString().Trim();
                    Ano = DropDownListAno.SelectedItem.Value.ToString().Trim();
                    break;
                case "Mensual":
                    Dia = "1";
                    Mes = DropDownListMes.SelectedItem.Value.ToString().Trim();
                    Ano = DropDownListAno.SelectedItem.Value.ToString().Trim();
                    break;
                case "Bimestral":
                    Dia = "1";
                    Mes = DropDownListBimestre.SelectedItem.Value.ToString().Trim();
                    Ano = DropDownListAno.SelectedItem.Value.ToString().Trim();
                    break;
                case "Trimestral":
                    Dia = "1";
                    Mes = DropDownListTrimestre.SelectedItem.Value.ToString().Trim();
                    Ano = DropDownListAno.SelectedItem.Value.ToString().Trim();
                    break;
                case "Semestral":
                    Dia = "1";
                    Mes = DropDownListSemestre.SelectedItem.Value.ToString().Trim();
                    Ano = DropDownListAno.SelectedItem.Value.ToString().Trim();
                    break;
                case "Anual":
                    Dia = "1";
                    Mes = "1";
                    Ano = DropDownListAno.SelectedItem.Value.ToString().Trim();
                    break;
            }
        }

        private void verResultados()
        {
            dtVerResultados = cGestion.VerResultados(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
        }

        private void verMeta()
        {
            dtVerMeta = cGestion.VerMetaNuevoPeriodo(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Dia, Mes, Ano);
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional, string Cierre)
        {
            #region Variables
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Variables

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
                        if (Cierre == "1")
                            Asunto = "CIERRE - " + row["Asunto"].ToString().Trim();
                        else
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
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CIERRE";
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
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
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

        private void loadGridPAI()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoPAI", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            grid.Columns.Add("FechaCompromiso", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGridPAI = grid;
        }

        private void loadGridPAIComentarios()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoPAI", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            GridView6.DataSource = grid;
            GridView6.DataBind();
            InfoGridPAIComentarios = grid;
        }

        private void cargarInfoGridPAIComentarios()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.PlanAccionIndicadorComentarios(InfoGridPAI.Rows[IdexRowIndicadorPAI]["CodigoPAI"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPAIComentarios.Rows.Add(new Object[] {dtInfo.Rows[rows]["CodigoPAI"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Comentario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                    });
                }
                GridView6.DataSource = InfoGridPAIComentarios;
                GridView6.DataBind();
            }
        }

        private void cargarInfoGridPAI()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.PlanAccionIndicador(InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPAI.Rows.Add(new Object[] {dtInfo.Rows[rows]["CodigoPAI"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Estado"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaCompromiso"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView2.DataSource = InfoGridPAI;
                GridView2.DataBind();
            }
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        #region PDFs

        private void loadFile()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cGestion.loadCodigoArchivoGestion();

            if (dtInfo.Rows.Count > 0)
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + IdPlan + "-" + IdGestion + "-" + FileUpload1.FileName.ToString().Trim();
            else
                nameFile = "1-" + IdPlan + "-" + IdGestion + "-" + FileUpload1.FileName.ToString().Trim();

            FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFGestion/") + nameFile);
            cGestion.agregarArchivoGestion(IdGestion, IdPlan, nameFile, FileUpload1.FileName);
            Mensaje("Archivo cargado correctamente.");
        }

        private void descargarArchivo()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + InfoGridControles.Rows[RowGridArchivoControl]["NombreArchivo"].ToString().Trim());
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFGestion/" + InfoGridControles.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }

        private void mtdCargarPdfGestion()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cGestion.loadCodigoArchivoGestion();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    IdPlan, IdGestion, FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}-{2}",
                   IdPlan, IdGestion, FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cGestion.mtdAgregarPdfGestion(IdGestion, IdPlan, FileUpload1.FileName, strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfGestion()
        {
            #region Vars
            string strNombreArchivo = InfoGridControles.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cGestion.mtdDescargarPdfSeguimiento(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }

        #endregion

        protected void BtnCancelPAI_Click(object sender, ImageClickEventArgs e)
        {
            TrAddPAI.Visible = false;
            ResetPAI();
        }

        protected void BtnAddPAI_Click(object sender, ImageClickEventArgs e)
        {
            VerAddPAI();
        }

        protected void BtnCancelAddPAI_Click(object sender, ImageClickEventArgs e)
        {
            TrVerPAI.Visible = false;
            TblPAI.Visible = false;
        }

        protected void BtnNewPAI_Click(object sender, ImageClickEventArgs e)
        {
            string ResponsableNotificacion = string.Empty;
            ResponsableNotificacion = Label73.Text.Trim();
            try
            {
                TextBox10.Text = cGestion.CodigoConsecutivo("IdPAI", "Gestion.IndicadorPlanAccion");
                if (TextBox10.Text == "")
                {
                    TextBox10.Text = "PAI1";
                }
                else
                {
                    TextBox10.Text = "PAI" + cGestion.CodigoConsecutivo("IdPAI", "Gestion.IndicadorPlanAccion");
                }
                cGestion.AddPlanAccionIndicador(Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), InfoGridIndicadores.Rows[IdexRowIndicador]["IdIndicador"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox13.Text.Trim()), Label73.Text.Trim(), DropDownList1.SelectedValue.ToString().Trim(), "1", DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox16.Text.Trim()) + " 12:00:00:000");
                loadGridPAI();
                cargarInfoGridPAI();
                string TextoAdicional;
                TextoAdicional = "PLAN DE ACCIÓN - INDICADORES" + "<br>";
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
                TextoAdicional = TextoAdicional + " Código : " + Label29.Text + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + Label31.Text + "<br>";
                TextoAdicional = TextoAdicional + "<br>" + "<B>Indicador</B>" + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + Label42.Text + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + Label43.Text + "<br>";
                TextoAdicional = TextoAdicional + "<br>" + "<B>Plan Acción Indicador</B>" + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + TextBox10.Text + "<br>";
                TextoAdicional = TextoAdicional + " Descripción : " + TextBox13.Text + "<br>";
                TextoAdicional = TextoAdicional + " Periodo : " + DropDownList2.SelectedItem.Text.ToString() + "-" + DropDownList3.SelectedItem.Text.ToString() + "<br>";
                ResetPAI();
                TrAddPAI.Visible = false;
                //ENVIAR NOTIFICACION
                boolEnviarNotificacion(28, 1, Convert.ToInt32(ResponsableNotificacion.ToString()), "", TextoAdicional, "0");
                Mensaje("Plan de Acción Indicadores creado correctamente");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al crear Plan de Acción Indicadores " + ex.Message);
            }
        }

        protected void BtnGuardaPAI_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                cGestion.UpdatePlanAccionIndicador(Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox13.Text.Trim()), Label73.Text.Trim(), DropDownList1.SelectedValue.ToString().Trim(), "1", DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox16.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox14.Text.Trim()));
                loadGridPAI();
                cargarInfoGridPAI();
                if (DropDownList1.SelectedValue.ToString() == "0")
                {
                    string TextoAdicional;
                    TextoAdicional = "PLAN DE ACCIÓN - INDICADORES" + "<br>";
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
                    TextoAdicional = TextoAdicional + " Código : " + Label29.Text + "<br>";
                    TextoAdicional = TextoAdicional + " Nombre : " + Label31.Text + "<br>";
                    TextoAdicional = TextoAdicional + "<br>" + "<B>Indicador</B>" + "<br>";
                    TextoAdicional = TextoAdicional + " Código : " + Label42.Text + "<br>";
                    TextoAdicional = TextoAdicional + " Nombre : " + Label43.Text + "<br>";
                    TextoAdicional = TextoAdicional + "<br>" + "<B>Plan Acción Indicador</B>" + "<br>";
                    TextoAdicional = TextoAdicional + " Código : " + TextBox10.Text + "<br>";
                    TextoAdicional = TextoAdicional + " Descripción : " + TextBox13.Text + "<br>";
                    TextoAdicional = TextoAdicional + " Periodo : " + DropDownList2.SelectedItem.Text.ToString() + "-" + DropDownList3.SelectedItem.Text.ToString() + "<br>";
                    boolEnviarNotificacion(28, 1, Convert.ToInt32(Label73.Text.Trim()), "", TextoAdicional, "1");
                }
                ResetPAI();
                TrAddPAI.Visible = false;
                Mensaje("Información actualizada correctamente");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al actualizar Plan de Acción Indicadores " + ex.Message);
            }
        }

        public void ResetPAI()
        {
            TextBox10.Text = string.Empty;
            TextBox13.Text = string.Empty;
            Label73.Text = string.Empty;
            TextBox18.Text = string.Empty;
            TextBox16.Text = string.Empty;
            TextBox14.Text = string.Empty;
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            TrComentariosPAI.Visible = false;
        }

        public void EnviarNotificacionCumplimientoIndicador(Int64 Cumplimiento)
        {
            string TextoAdicional;
            TextoAdicional = "CUMPLIMIENTO - INDICADORES" + "<br>";
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
            TextoAdicional = TextoAdicional + " Código : " + Label29.Text + "<br>";
            TextoAdicional = TextoAdicional + " Nombre : " + Label31.Text + "<br>";
            TextoAdicional = TextoAdicional + "<br>" + "<B>Indicador</B>" + "<br>";
            TextoAdicional = TextoAdicional + " Código : " + Label42.Text + "<br>";
            TextoAdicional = TextoAdicional + " Nombre : " + Label43.Text + "<br>";
            TextoAdicional = TextoAdicional + " Periodo : " + DropDownListMes.SelectedValue.ToString() + "-" + DropDownListAno.SelectedValue.ToString() + "<br>";
            TextoAdicional = TextoAdicional + " Cumplimiento : " + Cumplimiento.ToString() + " %" + "<br>";
            DataTable DtResponsablePA = new DataTable();
            DtResIndicadorPA = cGestion.ResponsablePlanAccion(LabelIdPlanAccion.Text.Trim());

            boolEnviarNotificacion(29, 1, Convert.ToInt32(DtResIndicadorPA.Rows[0]["Responsable"].ToString().Trim()), "", TextoAdicional, "0");

        }
    }
}