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
using System.Threading;
using System.Web.Script.Services;
using System.Web.Services;
using Microsoft.Security.Application;
using System.Configuration;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class Riesgos : System.Web.UI.UserControl
    {
        #region Variables
        string IdFormulario = "5003";
        string PestanaControl = "5004";
        string PestanaObjetivo = "5005";
        string PestanaPlanAccion = "5006";
        string PestanaEventos = "5007";
        string strPestanaJustifPDF = "5020";
        string strRecalificarRiesgos = "5036";
        cControl cControl = new cControl();
        cRiesgo cRiesgo = new cRiesgo();
        cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        cCuenta cCuenta = new cCuenta();
        private static int LastInsertIdCE;
        private static int NuevoRiesgo = 0;
        //Variables para el nuevo calculo de Reisgo Residual
        private static int CantControlesProbabilidad = 0;
        private static int CantControlesImpacto = 0;
        private static string LastRiesgo = string.Empty;
        string[] strColors = new string[10000];
        #endregion Variables

        #region Properties
        private DataTable infoGridConsultarControles;
        private DataTable InfoGridConsultarControles
        {
            get
            {
                infoGridConsultarControles = (DataTable)ViewState["infoGridConsultarControles"];
                return infoGridConsultarControles;
            }
            set
            {
                infoGridConsultarControles = value;
                ViewState["infoGridConsultarControles"] = infoGridConsultarControles;
            }
        }

        private int rowGridConsultarControles;
        private int RowGridConsultarControles
        {
            get
            {
                rowGridConsultarControles = (int)ViewState["rowGridConsultarControles"];
                return rowGridConsultarControles;
            }
            set
            {
                rowGridConsultarControles = value;
                ViewState["rowGridConsultarControles"] = rowGridConsultarControles;
            }
        }

        private DataTable infoGridRiesgos;
        private DataTable InfoGridRiesgos
        {
            get
            {
                infoGridRiesgos = (DataTable)ViewState["infoGridRiesgos"];
                return infoGridRiesgos;
            }
            set
            {
                infoGridRiesgos = value;
                ViewState["infoGridRiesgos"] = infoGridRiesgos;
            }
        }

        private int pagIndexInfoGridRiesgos;
        private int PagIndexInfoGridRiesgos
        {
            get
            {
                pagIndexInfoGridRiesgos = (int)ViewState["pagIndexInfoGridRiesgos"];
                return pagIndexInfoGridRiesgos;
            }
            set
            {
                pagIndexInfoGridRiesgos = value;
                ViewState["pagIndexInfoGridRiesgos"] = pagIndexInfoGridRiesgos;
            }
        }

        private DataTable infoGridControlesRiesgo;
        private DataTable InfoGridControlesRiesgo
        {
            get
            {
                infoGridControlesRiesgo = (DataTable)ViewState["infoGridControlesRiesgo"];
                return infoGridControlesRiesgo;
            }
            set
            {
                infoGridControlesRiesgo = value;
                ViewState["infoGridControlesRiesgo"] = infoGridControlesRiesgo;
            }
        }

        private DataTable infoGridControlesRiesgoMasivo;
        private DataTable InfoGridControlesRiesgoMasivo
        {
            get
            {
                infoGridControlesRiesgoMasivo = (DataTable)ViewState["infoGridControlesRiesgoMasivo"];
                return infoGridControlesRiesgoMasivo;
            }
            set
            {
                infoGridControlesRiesgoMasivo = value;
                ViewState["infoGridControlesRiesgoMasivo"] = infoGridControlesRiesgoMasivo;
            }
        }

        private int rowGridRiesgos;
        private int RowGridRiesgos
        {
            get
            {
                rowGridRiesgos = (int)ViewState["rowGridRiesgos"];
                return rowGridRiesgos;
            }
            set
            {
                rowGridRiesgos = value;
                ViewState["rowGridRiesgos"] = rowGridRiesgos;
            }
        }

        private int rowGridControlesRiesgo;
        private int RowGridControlesRiesgo
        {
            get
            {
                rowGridControlesRiesgo = (int)ViewState["rowGridControlesRiesgo"];
                return rowGridControlesRiesgo;
            }
            set
            {
                rowGridControlesRiesgo = value;
                ViewState["rowGridControlesRiesgo"] = rowGridControlesRiesgo;
            }
        }

        private double promedioControl;
        private double PromedioControl
        {
            get
            {
                promedioControl = (double)ViewState["promedioControl"];
                return promedioControl;
            }
            set
            {
                promedioControl = value;
                ViewState["promedioControl"] = promedioControl;
            }
        }

        private DataTable infoCalificacionControl;
        private DataTable InfoCalificacionControl
        {
            get
            {
                infoCalificacionControl = (DataTable)ViewState["infoCalificacionControl"];
                return infoCalificacionControl;
            }
            set
            {
                infoCalificacionControl = value;
                ViewState["infoCalificacionControl"] = infoCalificacionControl;
            }
        }

        private String idCalificacionControl;
        private String IdCalificacionControl
        {
            get
            {
                idCalificacionControl = (String)ViewState["idCalificacionControl"];
                return idCalificacionControl;
            }
            set
            {
                idCalificacionControl = value;
                ViewState["idCalificacionControl"] = idCalificacionControl;
            }
        }

        private double desviacionProbabilidad;
        private double DesviacionProbabilidad
        {
            get
            {
                desviacionProbabilidad = (double)ViewState["desviacionProbabilidad"];
                return desviacionProbabilidad;
            }
            set
            {
                desviacionProbabilidad = value;
                ViewState["desviacionProbabilidad"] = desviacionProbabilidad;
            }
        }

        private double desviacionImpacto;
        private double DesviacionImpacto
        {
            get
            {
                desviacionImpacto = (double)ViewState["desviacionImpacto"];
                return desviacionImpacto;
            }
            set
            {
                desviacionImpacto = value;
                ViewState["desviacionImpacto"] = desviacionImpacto;
            }
        }

        private DataTable infoGridArchivoRiesgo;
        private DataTable InfoGridArchivoRiesgo
        {
            get
            {
                infoGridArchivoRiesgo = (DataTable)ViewState["infoGridArchivoRiesgo"];
                return infoGridArchivoRiesgo;
            }
            set
            {
                infoGridArchivoRiesgo = value;
                ViewState["infoGridArchivoRiesgo"] = infoGridArchivoRiesgo;
            }
        }

        private int rowGridArchivoRiesgo;
        private int RowGridArchivoRiesgo
        {
            get
            {
                rowGridArchivoRiesgo = (int)ViewState["rowGridArchivoRiesgo"];
                return rowGridArchivoRiesgo;
            }
            set
            {
                rowGridArchivoRiesgo = value;
                ViewState["rowGridArchivoRiesgo"] = rowGridArchivoRiesgo;
            }
        }

        private DataTable infoGridArchivoPlanAccion;
        private DataTable InfoGridArchivoPlanAccion
        {
            get
            {
                infoGridArchivoPlanAccion = (DataTable)ViewState["infoGridArchivoPlanAccion"];
                return infoGridArchivoPlanAccion;
            }
            set
            {
                infoGridArchivoPlanAccion = value;
                ViewState["infoGridArchivoPlanAccion"] = infoGridArchivoPlanAccion;
            }
        }

        private int rowGridArchivoPlanAccion;
        private int RowGridArchivoPlanAccion
        {
            get
            {
                rowGridArchivoPlanAccion = (int)ViewState["rowGridArchivoPlanAccion"];
                return rowGridArchivoPlanAccion;
            }
            set
            {
                rowGridArchivoPlanAccion = value;
                ViewState["rowGridArchivoPlanAccion"] = rowGridArchivoPlanAccion;
            }
        }

        private DataTable infoGridComentarioPlanAccion;
        private DataTable InfoGridComentarioPlanAccion
        {
            get
            {
                infoGridComentarioPlanAccion = (DataTable)ViewState["infoGridComentarioPlanAccion"];
                return infoGridComentarioPlanAccion;
            }
            set
            {
                infoGridComentarioPlanAccion = value;
                ViewState["infoGridComentarioPlanAccion"] = infoGridComentarioPlanAccion;
            }
        }

        private int rowGridComentarioPlanAccion;
        private int RowGridComentarioPlanAccion
        {
            get
            {
                rowGridComentarioPlanAccion = (int)ViewState["rowGridComentarioPlanAccion"];
                return rowGridComentarioPlanAccion;
            }
            set
            {
                rowGridComentarioPlanAccion = value;
                ViewState["rowGridComentarioPlanAccion"] = rowGridComentarioPlanAccion;
            }
        }

        private DataTable infoGridComentarioRiesgo;
        private DataTable InfoGridComentarioRiesgo
        {
            get
            {
                infoGridComentarioRiesgo = (DataTable)ViewState["infoGridComentarioRiesgo"];
                return infoGridComentarioRiesgo;
            }
            set
            {
                infoGridComentarioRiesgo = value;
                ViewState["infoGridComentarioRiesgo"] = infoGridComentarioRiesgo;
            }
        }

        private int rowGridComentarioRiesgo;
        private int RowGridComentarioRiesgo
        {
            get
            {
                rowGridComentarioRiesgo = (int)ViewState["rowGridComentarioRiesgo"];
                return rowGridComentarioRiesgo;
            }
            set
            {
                rowGridComentarioRiesgo = value;
                ViewState["rowGridComentarioRiesgo"] = rowGridComentarioRiesgo;
            }
        }

        private DataTable infoGridObjetivoRiesgo;
        private DataTable InfoGridObjetivoRiesgo
        {
            get
            {
                infoGridObjetivoRiesgo = (DataTable)ViewState["infoGridObjetivoRiesgo"];
                return infoGridObjetivoRiesgo;
            }
            set
            {
                infoGridObjetivoRiesgo = value;
                ViewState["infoGridObjetivoRiesgo"] = infoGridObjetivoRiesgo;
            }
        }

        private int rowGridObjetivoRiesgo;
        private int RowGridObjetivoRiesgo
        {
            get
            {
                rowGridObjetivoRiesgo = (int)ViewState["rowGridObjetivoRiesgo"];
                return rowGridObjetivoRiesgo;
            }
            set
            {
                rowGridObjetivoRiesgo = value;
                ViewState["rowGridObjetivoRiesgo"] = rowGridObjetivoRiesgo;
            }
        }

        private DataTable infoGridPlanAccionRiesgo;
        private DataTable InfoGridPlanAccionRiesgo
        {
            get
            {
                infoGridPlanAccionRiesgo = (DataTable)ViewState["infoGridPlanAccionRiesgo"];
                return infoGridPlanAccionRiesgo;
            }
            set
            {
                infoGridPlanAccionRiesgo = value;
                ViewState["infoGridPlanAccionRiesgo"] = infoGridPlanAccionRiesgo;
            }
        }

        private int rowGridPlanAccionRiesgo;
        private int RowGridPlanAccionRiesgo
        {
            get
            {
                rowGridPlanAccionRiesgo = (int)ViewState["rowGridPlanAccionRiesgo"];
                return rowGridPlanAccionRiesgo;
            }
            set
            {
                rowGridPlanAccionRiesgo = value;
                ViewState["rowGridPlanAccionRiesgo"] = rowGridPlanAccionRiesgo;
            }
        }

        private DataTable infoGridEventoRiesgo;
        private DataTable InfoGridEventoRiesgo
        {
            get
            {
                infoGridEventoRiesgo = (DataTable)ViewState["infoGridEventoRiesgo"];
                return infoGridEventoRiesgo;
            }
            set
            {
                infoGridEventoRiesgo = value;
                ViewState["infoGridEventoRiesgo"] = infoGridEventoRiesgo;
            }
        }

        private int rowGridEventoRiesgo;
        private int RowGridEventoRiesgo
        {
            get
            {
                rowGridEventoRiesgo = (int)ViewState["rowGridEventoRiesgo"];
                return rowGridEventoRiesgo;
            }
            set
            {
                rowGridEventoRiesgo = value;
                ViewState["rowGridEventoRiesgo"] = rowGridEventoRiesgo;
            }
        }

        private DataTable infoIntervalos;
        private DataTable InfoIntervalos
        {
            get
            {
                infoIntervalos = (DataTable)ViewState["infoIntervalos"];
                return infoIntervalos;
            }
            set
            {
                infoIntervalos = value;
                ViewState["infoIntervalos"] = infoIntervalos;
            }
        }

        private DataTable infoGridAudRiesgoControl;
        private DataTable InfoGridAudRiesgoControl
        {
            get
            {
                infoGridAudRiesgoControl = (DataTable)ViewState["infoGridAudRiesgoControl"];
                return infoGridAudRiesgoControl;
            }
            set
            {
                infoGridAudRiesgoControl = value;
                ViewState["infoGridAudRiesgoControl"] = infoGridAudRiesgoControl;
            }
        }
        private DataTable infoGridConsultarCausasRiesgos;
        private DataTable InfoGridConsultarCausasRiesgos
        {
            get
            {
                infoGridConsultarCausasRiesgos = (DataTable)ViewState["infoGridConsultarCausasRiesgos"];
                return infoGridConsultarCausasRiesgos;
            }
            set
            {
                infoGridConsultarCausasRiesgos = value;
                ViewState["infoGridConsultarCausasRiesgos"] = infoGridConsultarCausasRiesgos;
            }
        }

        private int rowGridConsultarCausasRiesgos;
        private int RowGridConsultarCausasRiesgos
        {
            get
            {
                rowGridConsultarCausasRiesgos = (int)ViewState["rowGridConsultarCausasRiesgos"];
                return rowGridConsultarCausasRiesgos;
            }
            set
            {
                rowGridConsultarCausasRiesgos = value;
                ViewState["rowGridConsultarCausasRiesgos"] = rowGridConsultarCausasRiesgos;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);
            scrtManager.RegisterPostBackControl(ImageButton16);
            scrtManager.RegisterPostBackControl(GridView4);
            scrtManager.RegisterPostBackControl(ImageButton15);
            scrtManager.RegisterPostBackControl(GridView10);
            scrtManager.RegisterPostBackControl(GridView8);
            scrtManager.RegisterPostBackControl(Button6);
            scrtManager.RegisterPostBackControl(Button1);
            scrtManager.RegisterPostBackControl(this.ImbViewJPGfrecuencia);
            scrtManager.RegisterPostBackControl(this.ImbViewJPGimpacto);
            scrtManager.RegisterPostBackControl(this.ImbViewJPGfrecuenciaIns);
            scrtManager.RegisterPostBackControl(this.ImbViewJPGimpactoIns);
            string strErrMsg = string.Empty;
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["CodRiesgo"] == null)
                {
                    
                    loadDDLRegion();
                    loadDDLClasificacion();
                    loadCBLCausas();
                    loadCBLConsecuencias();
                    loadCBLTratamiento();
                    loadDDLCadenaValor();
                    loadDDLProbabilidad();
                    loadDDLFactorRiesgoOperativo();
                    loadDDLTipoEventoOperativo();
                    loadDDLRiesgoAsociadoOperativo();
                    loadCBLRiesgoAsociadoLA();
                    loadCBLFactorRiesgoLAFT();
                    loadDDLImpacto();
                    loadLBCadenaValor();
                    loadGridRiesgos();
                    loadInfoCalificacionControl();
                    //Camilo 12/02/2014
                    //loadDDLObjetivos();
                    loadDDLPlanes();
                    loadDDLTipoRecursoPlanAccion();
                    loadDDLEstadoPlanAccion();
                    //armarIntervalos();
                    inicializarValores();
                    PopulateTreeView();

                    mtdLoadGridAudRiesgoControl();
                    mtdLoadInfoAudRiesgoControl();
                }
                else {
                    LCodRiesgo.Text = Request.QueryString["CodRiesgo"];
                    tbGridRiesgos.Visible = false;
                    loadDDLRegion();
                    loadDDLClasificacion();
                    loadCBLCausas();
                    loadCBLConsecuencias();
                    loadCBLTratamiento();
                    loadDDLCadenaValor();
                    loadDDLProbabilidad();
                    loadDDLFactorRiesgoOperativo();
                    loadDDLTipoEventoOperativo();
                    loadDDLRiesgoAsociadoOperativo();
                    loadCBLRiesgoAsociadoLA();
                    loadCBLFactorRiesgoLAFT();
                    loadDDLImpacto();
                    loadLBCadenaValor();
                    loadGridRiesgos();
                    loadInfoCalificacionControl();
                    //Camilo 12/02/2014
                    //loadDDLObjetivos();
                    loadDDLPlanes();
                    loadDDLTipoRecursoPlanAccion();
                    loadDDLEstadoPlanAccion();
                    //armarIntervalos();
                    PopulateTreeView();
                    RowGridRiesgos = 0;
                    mtdLoadGridAudRiesgoControl();
                    mtdLoadInfoAudRiesgoControl();
                    inicializarValores();
                    resetValuesModificarRiesgo();
                    resetValuesModificarRiesgoControl();
                    resetValuesModificarRiesgoCalificacion();
                    resetValuesModificarRiesgoObjetivos();
                    resetValuesModificarRiesgoPlanAccion();
                    resetValuesModificarRiesgoEventos();
                    resetValuesJustificacion();
                    resetValuesJustificacionPlanAccion();
                    resetValuesAgregarRiesgo();
                    loadGridRiesgos();
                    loadInfoRiesgos();
                    if (InfoGridRiesgos.Rows.Count > 0)
                    {
                        detalleRiesgoSeleccionado();
                        loadGridControlesRiesgo();
                        loadInfoControlesRiesgo(ref strErrMsg);
                        loadGridArchivoRiesgo();
                        loadInfoArchivoRiesgo(ref strErrMsg);
                        loadGridComentarioRiesgo();
                        loadInfoComentarioRiesgo(ref strErrMsg);
                        loadGridObjetivoRiesgo();
                        loadInfoObjetivoRiesgo(ref strErrMsg);
                        loadGridPlanAccionRiesgo();
                        loadInfoPlanAccionRiesgo(ref strErrMsg);
                        loadGridEventoRiesgo();
                        loadInfoEventoRiesgo(ref strErrMsg);
                    }else
                        Mensaje1("El riesgo está anulado");
                }
            }
        }

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView1.ExpandAll();
            TreeView2.ExpandAll();
            TreeView3.ExpandAll();
            TreeView4.ExpandAll();
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
                TreeView1.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                TreeView2.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                TreeView3.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
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
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString().Trim();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox13.Text = TreeView1.SelectedNode.Text;
            lblIdDependencia1.Text = TreeView1.SelectedNode.Value;

        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox20.Text = TreeView2.SelectedNode.Text;
            lblIdDependencia2.Text = TreeView2.SelectedNode.Value;
        }

        protected void TreeView3_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox21.Text = TreeView3.SelectedNode.Text;
            lblIdDependencia3.Text = TreeView3.SelectedNode.Value;
        }

        protected void TreeView4_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox23.Text = TreeView4.SelectedNode.Text;
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview

        #region DDL
        private void loadDDLPlanes()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLPlanes();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList5.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdPlan"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar Planes Estratégicos. " + ex.Message);
            }
        }

        private void loadDDLTipoRecursoPlanAccion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLTipoRecursoPlanAccion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList17.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoRecursoPlanAccion"].ToString().Trim(), dtInfo.Rows[i]["IdTipoRecursoPlanAccion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar tipo recurso. " + ex.Message);
            }
        }

        private void loadDDLEstadoPlanAccion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLEstadoPlanAccion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList18.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreEstadoPlanAccion"].ToString().Trim(), dtInfo.Rows[i]["IdEstadoPlanAccion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar estado. " + ex.Message);
            }
        }

        private void loadDDLObjetivos()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                //Camilo 12/02/2014
                //dtInfo = cRiesgo.loadDDLObjetivos();
                dtInfo = cRiesgo.loadDDLObjetivos(DropDownList5.SelectedValue.ToString());
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList61.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreObjetivos"].ToString().Trim(), dtInfo.Rows[i]["IdObjetivos"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar objetivos. " + ex.Message);
            }
        }

        private void loadDDLTipoEventoOperativo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLTipoEventoOperativo();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList12.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoEventoOperativo"].ToString().Trim(), dtInfo.Rows[i]["IdTipoEventoOperativo"].ToString()));
                    DropDownList15.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoEventoOperativo"].ToString().Trim(), dtInfo.Rows[i]["IdTipoEventoOperativo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar tipo evento operativo. " + ex.Message);
            }
        }

        private void loadDDLRiesgoAsociadoOperativo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLRiesgoAsociadoOperativo();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList13.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreRiesgoAsociadoOperativo"].ToString().Trim(), dtInfo.Rows[i]["IdRiesgoAsociadoOperativo"].ToString()));
                    DropDownList16.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreRiesgoAsociadoOperativo"].ToString().Trim(), dtInfo.Rows[i]["IdRiesgoAsociadoOperativo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar riesgo asociado operativo. " + ex.Message);
            }
        }

        private void loadDDLFactorRiesgoOperativo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLFactorRiesgoOperativo();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList8.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreFactorRiesgoOperativo"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgoOperativo"].ToString()));
                    DropDownList59.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreFactorRiesgoOperativo"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgoOperativo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar factor riesgo operativo. " + ex.Message);
            }
        }

        private void loadDDLTipoRiesgoOperativo(String IdFactorRiesgoOperativo, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLTipoRiesgoOperativo(IdFactorRiesgoOperativo);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList14.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoRiesgoOperativo"].ToString().Trim(), dtInfo.Rows[i]["IdTipoRiesgoOperativo"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList60.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoRiesgoOperativo"].ToString().Trim(), dtInfo.Rows[i]["IdTipoRiesgoOperativo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar tipo riesgo operativo. " + ex.Message);
            }
        }

        private void loadDDLProbabilidad()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLProbabilidad();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList45.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProbabilidad"].ToString().Trim(), dtInfo.Rows[i]["IdProbabilidad"].ToString()));
                    DropDownList66.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProbabilidad"].ToString().Trim(), dtInfo.Rows[i]["IdProbabilidad"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar probabilidad. " + ex.Message);
            }
        }

        private void loadDDLImpacto()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLImpacto();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList46.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreImpacto"].ToString().Trim(), dtInfo.Rows[i]["IdImpacto"].ToString()));
                    DropDownList68.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreImpacto"].ToString().Trim(), dtInfo.Rows[i]["IdImpacto"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar las impacto. " + ex.Message);
            }
        }

        private void loadCodigoRiesgo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cRiesgo.loadCodigoRiesgo();

                //Ajuuste RCC - 15/01/2014
                if (NuevoRiesgo == 1 && dtInfo.Rows.Count > 0)
                {
                    Label1.Visible = false;
                    TextBox8.Visible = false;
                }

                if (dtInfo.Rows.Count > 0)
                    TextBox8.Text = "R" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox8.Text = "R1";
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar el código riesgo. " + ex.Message);
            }
        }
        #endregion DDL

        #region Loads
        private void loadCBLRiesgoAsociadoLA()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadCBLRiesgoAsociadoLA();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    CheckBoxList5.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreRiesgoAsociadoLA"].ToString().Trim(), dtInfo.Rows[i]["IdRiesgoAsociadoLA"].ToString().Trim()));
                    CheckBoxList7.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreRiesgoAsociadoLA"].ToString().Trim(), dtInfo.Rows[i]["IdRiesgoAsociadoLA"].ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar riesgo asociado LA. " + ex.Message);
            }
        }

        private void loadCBLFactorRiesgoLAFT()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadCBLFactorRiesgoLAFT();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    CheckBoxList6.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreFactorRiesgoLAFT"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgoLAFT"].ToString().Trim()));
                    CheckBoxList8.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreFactorRiesgoLAFT"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgoLAFT"].ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar factor riesgo LAFT. " + ex.Message);
            }
        }

        #region Causas y Consecuencias
        private void loadCBLCausas()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cRiesgo.loadCBLCausas();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    CheckBoxList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreCausas"].ToString().Trim(), dtInfo.Rows[i]["IdCausas"].ToString().Trim()));
                    CheckBoxList3.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreCausas"].ToString().Trim(), dtInfo.Rows[i]["IdCausas"].ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar causas. " + ex.Message);
            }
        }

        private void loadCBLConsecuencias()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadCBLConsecuencias();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    CheckBoxList2.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreConsecuencia"].ToString().Trim(), dtInfo.Rows[i]["IdConsecuencia"].ToString().Trim()));
                    CheckBoxList4.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreConsecuencia"].ToString().Trim(), dtInfo.Rows[i]["IdConsecuencia"].ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar consecuencias. " + ex.Message);
            }
        }
        #endregion

        private void loadCBLTratamiento()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadCBLTratamiento();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    CheckBoxList10.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreTratamiento"].ToString().Trim(), dtInfo.Rows[i]["IdTratamiento"].ToString().Trim()));
                    CheckBoxList9.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreTratamiento"].ToString().Trim(), dtInfo.Rows[i]["IdTratamiento"].ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar consecuencias. " + ex.Message);
            }
        }

        private void loadDDLCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCadenaValor();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList67.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                    DropDownList52.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                    DropDownList19.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar cadena valor. " + ex.Message);
            }
        }

        private void loadDDLMacroproceso(String IdCadenaValor, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLMacroproceso(IdCadenaValor);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList9.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList53.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                        }
                        break;
                    case 3:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList20.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar macroproceso. " + ex.Message);
            }
        }

        private void loadDDLProceso(String IdMacroproceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLProceso(IdMacroproceso);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList10.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList54.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                        }
                        break;
                    case 3:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList21.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar proceso. " + ex.Message);
            }
        }

        private void loadDDLSubProceso(String IdProceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubProceso(IdProceso);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList6.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList7.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                        }
                        break;
                    case 3:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList22.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar subproceso. " + ex.Message);
            }
        }

        private void loadDDLActividad(String IdSubproceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLActividad(IdSubproceso);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList11.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreActividad"].ToString().Trim(), dtInfo.Rows[i]["IdActividad"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList55.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreActividad"].ToString().Trim(), dtInfo.Rows[i]["IdActividad"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar actividad. " + ex.Message);
            }
        }

        private void loadDDLRegion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLRegion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList41.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreRegion"].ToString().Trim(), dtInfo.Rows[i]["IdRegion"].ToString()));
                    DropDownList47.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreRegion"].ToString().Trim(), dtInfo.Rows[i]["IdRegion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar region. " + ex.Message);
            }
        }

        private void loadDDLPais(String IdRegion, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLPais(IdRegion);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList42.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombrePais"].ToString().Trim(), dtInfo.Rows[i]["IdPais"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList48.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombrePais"].ToString().Trim(), dtInfo.Rows[i]["IdPais"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar pais. " + ex.Message);
            }
        }

        private void loadDDLDepartamento(String IdPais, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLDepartamento(IdPais);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList43.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreDepartamento"].ToString().Trim(), dtInfo.Rows[i]["IdDepartamento"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList49.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreDepartamento"].ToString().Trim(), dtInfo.Rows[i]["IdDepartamento"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar departamento. " + ex.Message);
            }
        }

        private void loadDDLCiudad(String IdDepartamento, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCiudad(IdDepartamento);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList44.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCiudad"].ToString().Trim(), dtInfo.Rows[i]["IdCiudad"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList50.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCiudad"].ToString().Trim(), dtInfo.Rows[i]["IdCiudad"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar ciudad. " + ex.Message);
            }
        }

        private void loadDDLOficinaSucursal(String IdCiudad, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLOficinaSucursal(IdCiudad);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList63.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreOficinaSucursal"].ToString().Trim(), dtInfo.Rows[i]["IdOficinaSucursal"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList51.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreOficinaSucursal"].ToString().Trim(), dtInfo.Rows[i]["IdOficinaSucursal"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar oficina/Sucursal. " + ex.Message);
            }
        }

        private void loadDDLClasificacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                    DropDownList56.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                    DropDownList4.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar clasificación riesgo. " + ex.Message);
            }
        }

        private void loadDDLClasificacionGeneral(String IdClasificacionRiesgo, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacionGeneral(IdClasificacionRiesgo);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionGeneralRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionGeneralRiesgo"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList57.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionGeneralRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionGeneralRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar clasificación general. " + ex.Message);
            }
        }

        private void loadDDLClasificacionParticular(String IdClasificacionGeneralRiesgo, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacionParticular(IdClasificacionGeneralRiesgo);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList3.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionParticularRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionParticularRiesgo"].ToString()));
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList58.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionParticularRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionParticularRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar clasificación particular. " + ex.Message);
            }
        }

        private void loadLBCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadLBCadenaValor();
                int contador1 = 0;
                int contador2 = 0;
                int contador3 = 0;
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    switch (dtInfo.Rows[i]["IdCadenaValor"].ToString().Trim())
                    {
                        case "1":
                            ListBox1.Items.Insert(contador1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            ListBox4.Items.Insert(contador1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            contador1++;
                            break;
                        case "2":
                            ListBox2.Items.Insert(contador2, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            ListBox5.Items.Insert(contador2, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            contador2++;
                            break;
                        case "3":
                            ListBox3.Items.Insert(contador3, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            ListBox6.Items.Insert(contador3, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            contador3++;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar macroprocesos cadena valor. " + ex.Message);
            }
        }

        private void loadGridConsultarControles()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdControl", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));
            grid.Columns.Add("DescripcionControl", typeof(string));
            InfoGridConsultarControles = grid;
            GridView8.DataSource = InfoGridConsultarControles;
            GridView8.DataBind();
        }

        private void loadInfoConsultarControles()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoControles(Sanitizer.GetSafeHtmlFragment(TextBox18.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox19.Text.Trim()), lblIdDependencia4.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridConsultarControles.Rows.Add(new Object[] { dtInfo.Rows[rows]["IdControl"].ToString().Trim(),
                                                                       dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),
                                                                       dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                                                                       dtInfo.Rows[rows]["DescripcionControl"].ToString().Trim()
                                                                     });
                }
                GridView8.DataSource = InfoGridConsultarControles;
                GridView8.DataBind();
            }
            else
            {
                loadGridConsultarControles();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        private void loadGridRiesgos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("IdRegion", typeof(string));
            grid.Columns.Add("IdPais", typeof(string));
            grid.Columns.Add("IdDepartamento", typeof(string));
            grid.Columns.Add("IdCiudad", typeof(string));
            grid.Columns.Add("IdOficinaSucursal", typeof(string));
            grid.Columns.Add("IdCadenaValor", typeof(string));
            grid.Columns.Add("IdMacroproceso", typeof(string));
            grid.Columns.Add("IdProceso", typeof(string));
            grid.Columns.Add("IdSubProceso", typeof(string));
            grid.Columns.Add("IdActividad", typeof(string));
            grid.Columns.Add("IdClasificacionRiesgo", typeof(string));
            grid.Columns.Add("IdClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("IdClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("NombreClasificacionRiesgo", typeof(string));
            grid.Columns.Add("IdFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("IdTipoRiesgoOperativo", typeof(string));
            grid.Columns.Add("IdTipoEventoOperativo", typeof(string));
            grid.Columns.Add("IdRiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("ListaCausas", typeof(string));
            grid.Columns.Add("ListaConsecuencias", typeof(string));
            grid.Columns.Add("IdResponsableRiesgo", typeof(string));
            grid.Columns.Add("IdProbabilidad", typeof(string));
            grid.Columns.Add("OcurrenciaEventoDesde", typeof(string));
            grid.Columns.Add("OcurrenciaEventoHasta", typeof(string));
            grid.Columns.Add("IdImpacto", typeof(string));
            grid.Columns.Add("PerdidaEconomicaDesde", typeof(string));
            grid.Columns.Add("PerdidaEconomicaHasta", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("Nombres", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            InfoGridRiesgos = grid;
            GridView1.DataSource = InfoGridRiesgos;
            GridView1.DataBind();
        }

        private void loadInfoRiesgos()
        {
            DataTable dtInfo = new DataTable();
            if (LCodRiesgo.Text != "")
            {
                dtInfo = cRiesgo.loadInfoRiesgos(Sanitizer.GetSafeHtmlFragment(LCodRiesgo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()), DropDownList19.SelectedValue.ToString().Trim(), DropDownList20.SelectedValue.ToString().Trim(), DropDownList21.SelectedValue.ToString().Trim(), DropDownList22.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim());
            }
            else
            {
                dtInfo = cRiesgo.loadInfoRiesgos(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()), DropDownList19.SelectedValue.ToString().Trim(), DropDownList20.SelectedValue.ToString().Trim(), DropDownList21.SelectedValue.ToString().Trim(), DropDownList22.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim());
            }
                if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdRegion"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdPais"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdDepartamento"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdCiudad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdOficinaSucursal"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdCadenaValor"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdMacroproceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdProceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdSubProceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdActividad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionGeneralRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionParticularRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdFactorRiesgoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdTipoRiesgoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdTipoEventoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdRiesgoAsociadoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaCausas"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaConsecuencias"].ToString().Trim(),                                                           
                                                           dtInfo.Rows[rows]["IdResponsableRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdProbabilidad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["OcurrenciaEventoDesde"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["OcurrenciaEventoHasta"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdImpacto"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["PerdidaEconomicaDesde"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["PerdidaEconomicaHasta"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreHijo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim()
                                                          });
                }
                GridView1.PageIndex = PagIndexInfoGridRiesgos;
                GridView1.DataSource = InfoGridRiesgos;
                GridView1.DataBind();
            }
            else
            {
                loadGridRiesgos();
                Mensaje("El registro no existe o su información no es suficiente para ser visualizada.");
            }
        }

        private void loadInfoEventoRiesgo(ref string strErrMsg)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoEventoRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridEventoRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdEvento"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["DescripcionEvento"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["FechaDescubrimiento"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["ValorRecuperadoTotal"].ToString().Trim()                                                                    
                                                               });
                }
                GridView5.DataSource = InfoGridEventoRiesgo;
                GridView5.DataBind();
            }else
            {
                strErrMsg = "No hay eventos adicionados al riesgo";
            }
        }

        private void loadGridEventoRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdEvento", typeof(string));
            grid.Columns.Add("CodigoEvento", typeof(string));
            grid.Columns.Add("DescripcionEvento", typeof(string));
            grid.Columns.Add("FechaDescubrimiento", typeof(string));
            grid.Columns.Add("ValorRecuperadoTotal", typeof(string));
            InfoGridEventoRiesgo = grid;
            GridView5.DataSource = InfoGridEventoRiesgo;
            GridView5.DataBind();
        }

        private void loadInfoObjetivoRiesgo(ref string strErrMsg)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoObjetivoRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridObjetivoRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdObjetivosRiesgo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdObjetivos"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreObjetivos"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim()
                                                                 });
                }
                GridView7.DataSource = InfoGridObjetivoRiesgo;
                GridView7.DataBind();
            }else
            {
                strErrMsg = "No hay Información de Objetivo de riesgo";
            }
        }

        private void loadGridObjetivoRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdObjetivosRiesgo", typeof(string));
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("IdObjetivos", typeof(string));
            grid.Columns.Add("NombreObjetivos", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            InfoGridObjetivoRiesgo = grid;
            GridView7.DataSource = InfoGridObjetivoRiesgo;
            GridView7.DataBind();
        }

        private void loadInfoComentarioRiesgo(ref string strErrMsg)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoComentarioRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["ComentarioCorto"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                                                                   });
                }
                GridView6.DataSource = InfoGridComentarioRiesgo;
                GridView6.DataBind();
            }
            else
                strErrMsg = "No hay comentarios para el riesgo";
        }

        private void loadGridComentarioRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("ComentarioCorto", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            InfoGridComentarioRiesgo = grid;
            GridView6.DataSource = InfoGridComentarioRiesgo;
            GridView6.DataBind();
        }

        private void loadInfoArchivoRiesgo(ref string strErrMsg)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoArchivoRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                                });
                }
                GridView4.DataSource = InfoGridArchivoRiesgo;
                GridView4.DataBind();
            }
            else
                strErrMsg = "No hay archivos de riesgos";
        }

        private void loadGridArchivoRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoRiesgo = grid;
            GridView4.DataSource = InfoGridArchivoRiesgo;
            GridView4.DataBind();
        }

        private void loadGridControlesRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdControlesRiesgo", typeof(string));
            grid.Columns.Add("IdControl", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));
            grid.Columns.Add("NombreTest", typeof(string));
            grid.Columns.Add("CalificacionControl", typeof(string));
            grid.Columns.Add("DesviacionProbabilidad", typeof(string));
            grid.Columns.Add("DesviacionImpacto", typeof(string));
            grid.Columns.Add("IdMitiga", typeof(string));
            grid.Columns.Add("NombreEscala", typeof(string));
            grid.Columns.Add("Color", typeof(string));
            InfoGridControlesRiesgo = grid;
            GridView2.DataSource = InfoGridControlesRiesgo;
            GridView2.DataBind();
        }

        private void loadInfoControlesRiesgo(ref string strErrMsg)
        {
            DataTable dtInfo = new DataTable();
            PromedioControl = 0;
            dtInfo = cRiesgo.loadInfoControlesRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                #region Ciclo para poner la informacion de los controles asociados con el riesgo
                int rows;
                for (rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridControlesRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdControlesRiesgo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["IdControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),                                                                   
                                                                   dtInfo.Rows[rows]["CalificacionControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["DesviacionProbabilidad"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["DesviacionImpacto"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["IdMitiga"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreEscala"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["Color"].ToString().Trim()
                                                                  });
                    PromedioControl = PromedioControl + Convert.ToDouble(dtInfo.Rows[rows]["CalificacionControl"].ToString().Trim());
                    strColors[rows] = dtInfo.Rows[rows]["Color"].ToString().Trim();
                    Label35.Text = dtInfo.Rows[rows]["NombreEscala"].ToString().Trim();
                    Panel6.BackColor = System.Drawing.Color.FromName(dtInfo.Rows[rows]["Color"].ToString().Trim());
                }
                #endregion Ciclo para poner la informacion de los controles asociados con el riesgo

                PromedioControl = (PromedioControl / rows);
                GridView2.DataSource = InfoGridControlesRiesgo;
                GridView2.DataBind();
                
            }
            else
            {
                strErrMsg = "No hay Controles adicionados";
                Label35.Text = "Sin controles";
                Panel6.BackColor = System.Drawing.Color.FromName("Transparent");
                Label39.Text = "";
                Panel7.BackColor = System.Drawing.Color.FromName("Transparent");
            }
            // Comentado Heber Correal para que aparezca riesgo residual.
            //promedioCalificacionControl();
            calificacionResidual();
        }

        private void loadInfoCalificacionControl()
        {
            InfoCalificacionControl = cControl.loadInfoCalificacionControl();
        }

        private DataTable loadGridReporteRiesgosEventos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("TipoRiesgoOperativo", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("Frecuencia", typeof(string));
            grid.Columns.Add("Impacto", typeof(string));
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoEvento", typeof(string));
            grid.Columns.Add("DescripcionEvento", typeof(string));
            grid.Columns.Add("ResponsableEvento", typeof(string));
            grid.Columns.Add("FechaRegistroEvento", typeof(string));
            grid.Columns.Add("ProcesoInvolucrado", typeof(string));
            grid.Columns.Add("AplicativoInvolucrado", typeof(string));
            grid.Columns.Add("ServicioProductoAfectado", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFinalizacion", typeof(string));
            grid.Columns.Add("FechaDescubrimiento", typeof(string));
            grid.Columns.Add("CuentaPUC", typeof(string));
            grid.Columns.Add("ValorRecuperadoTotal", typeof(string));
            grid.Columns.Add("ValorRecuperadoSeguro", typeof(string));
            grid.Columns.Add("Observaciones", typeof(string));
            grid.Columns.Add("NombreDepartamento", typeof(string));
            grid.Columns.Add("NombreCiudad", typeof(string));
            grid.Columns.Add("NombreOficinaSucursal", typeof(string));
            grid.Columns.Add("NombreClaseEvento", typeof(string));
            grid.Columns.Add("NombreTipoPerdidaEvento", typeof(string));
            return grid;
        }

        #region Plan de Accion
        private void loadInfoComentarioPlanAccion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoComentarioPlanAccion(InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioPlanAccion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["ComentarioCorto"].ToString().Trim(),
                        dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                        });
                }
                GridView9.DataSource = InfoGridComentarioPlanAccion;
                GridView9.DataBind();
            }
        }

        private void loadGridComentarioPlanAccion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("ComentarioCorto", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            InfoGridComentarioPlanAccion = grid;
            GridView9.DataSource = InfoGridComentarioPlanAccion;
            GridView9.DataBind();
        }

        private void loadInfoArchivoPlanAccion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoArchivoPlanAccion(InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoPlanAccion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                    });
                }

                GridView10.DataSource = InfoGridArchivoPlanAccion;
                GridView10.DataBind();
            }
        }

        private void loadGridArchivoPlanAccion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoPlanAccion = grid;
            GridView10.DataSource = InfoGridArchivoPlanAccion;
            GridView10.DataBind();
        }

        private void loadInfoPlanAccionRiesgo(ref string strErrMsg)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoPlanAccionRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPlanAccionRiesgo.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdPlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdTipoRecursoPlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecurso"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdEstadoPlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreEstadoPlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaCompromiso"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreHijo"].ToString().Trim()
                        });
                }

                GridView3.DataSource = InfoGridPlanAccionRiesgo;
                GridView3.DataBind();
            }else
            {
                strErrMsg = "No hay información de Planes de Acción";
            }
        }

        private void loadGridPlanAccionRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlanAccion", typeof(string));
            grid.Columns.Add("DescripcionAccion", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            grid.Columns.Add("IdTipoRecursoPlanAccion", typeof(string));
            grid.Columns.Add("ValorRecurso", typeof(string));
            grid.Columns.Add("IdEstadoPlanAccion", typeof(string));
            grid.Columns.Add("NombreEstadoPlanAccion", typeof(string));
            grid.Columns.Add("FechaCompromiso", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            InfoGridPlanAccionRiesgo = grid;
            GridView3.DataSource = InfoGridPlanAccionRiesgo;
            GridView3.DataBind();
        }
        #endregion

        #endregion Loads

        #region DropDownList

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            trRiesgoOperativo1.Visible = false;
            trRiesgoOperativo2.Visible = false;
            trLavadoActivos1.Visible = false;

            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "0"));
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "0"));
            DropDownList8.SelectedIndex = 0;
            DropDownList12.SelectedIndex = 0;
            DropDownList13.SelectedIndex = 0;
            DropDownList14.Items.Clear();
            DropDownList14.Items.Insert(0, new ListItem("---", "0"));

            for (int i = 0; i < CheckBoxList5.Items.Count; i++)
            {
                CheckBoxList5.Items[i].Selected = false;
            }

            for (int i = 0; i < CheckBoxList6.Items.Count; i++)
            {
                CheckBoxList6.Items[i].Selected = false;
            }

            if (DropDownList1.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLClasificacionGeneral(DropDownList1.SelectedValue.ToString().Trim(), 1);

                if (DropDownList1.SelectedValue.ToString().Trim() == "1")
                {
                    trRiesgoOperativo1.Visible = true;
                    trRiesgoOperativo2.Visible = true;
                    trLavadoActivos1.Visible = false;
                }

                if (DropDownList1.SelectedValue.ToString().Trim() == "2")
                {
                    trRiesgoOperativo1.Visible = false;
                    trRiesgoOperativo2.Visible = false;
                    trLavadoActivos1.Visible = true;
                }
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList2.SelectedValue.ToString().Trim() != "0")
                loadDDLClasificacionParticular(DropDownList2.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList61.Items.Clear();
            DropDownList61.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList5.SelectedValue.ToString().Trim() != "---")
                loadDDLObjetivos();
        }

        protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList6.SelectedValue.ToString().Trim() != "0")
                loadDDLActividad(DropDownList6.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList55.Items.Clear();
            DropDownList55.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList7.SelectedValue.ToString().Trim() != "0")
                loadDDLActividad(DropDownList7.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList14.Items.Clear();
            DropDownList14.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList8.SelectedValue.ToString().Trim() != "0")
                loadDDLTipoRiesgoOperativo(DropDownList8.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList10.Items.Clear();
            DropDownList10.Items.Insert(0, new ListItem("---", "0"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList9.SelectedValue.ToString().Trim() != "---")
                loadDDLProceso(DropDownList9.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList10.SelectedValue.ToString().Trim() != "---")
                loadDDLSubProceso(DropDownList10.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList20.Items.Clear();
            DropDownList20.Items.Insert(0, new ListItem("---", "---"));
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList19.SelectedValue.ToString().Trim() != "---")
                loadDDLMacroproceso(DropDownList19.SelectedValue.ToString().Trim(), 3);
        }

        protected void DropDownList20_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList20.SelectedValue.ToString().Trim() != "---")
                loadDDLProceso(DropDownList20.SelectedValue.ToString().Trim(), 3);
        }

        protected void DropDownList21_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList21.SelectedValue.ToString().Trim() != "---")
                loadDDLSubProceso(DropDownList21.SelectedValue.ToString().Trim(), 3);
        }

        protected void DropDownList41_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList42.Items.Clear();
            DropDownList42.Items.Insert(0, new ListItem("---", "---"));
            DropDownList43.Items.Clear();
            DropDownList43.Items.Insert(0, new ListItem("---", "---"));
            DropDownList44.Items.Clear();
            DropDownList44.Items.Insert(0, new ListItem("---", "---"));
            DropDownList63.Items.Clear();
            DropDownList63.Items.Insert(0, new ListItem("---", "---"));

            if (DropDownList41.SelectedValue.ToString().Trim() != "---")
                loadDDLPais(DropDownList41.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList42_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList43.Items.Clear();
            DropDownList43.Items.Insert(0, new ListItem("---", "---"));
            DropDownList44.Items.Clear();
            DropDownList44.Items.Insert(0, new ListItem("---", "---"));
            DropDownList63.Items.Clear();
            DropDownList63.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList42.SelectedValue.ToString().Trim() != "---")
                loadDDLDepartamento(DropDownList42.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList43_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList44.Items.Clear();
            DropDownList44.Items.Insert(0, new ListItem("---", "---"));
            DropDownList63.Items.Clear();
            DropDownList63.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList43.SelectedValue.ToString().Trim() != "---")
                loadDDLCiudad(DropDownList43.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList44_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList63.Items.Clear();
            DropDownList63.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList44.SelectedValue.ToString().Trim() != "---")
                loadDDLOficinaSucursal(DropDownList44.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList45_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label13.Text = "";
            if (DropDownList45.SelectedValue.ToString().Trim() != "---")
            {
                Label13.Text = cRiesgo.ValorProbabilidad(DropDownList45.SelectedValue.ToString().Trim());
                calificacionInherente(1);
            }
        }

        protected void DropDownList46_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label177.Text = "";
            if (DropDownList46.SelectedValue.ToString().Trim() != "---")
            {
                Label177.Text = cRiesgo.ValorImpacto(DropDownList46.SelectedValue.ToString().Trim());
                calificacionInherente(1);
            }
        }

        protected void DropDownList47_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList48.Items.Clear();
            DropDownList48.Items.Insert(0, new ListItem("---", "---"));
            DropDownList49.Items.Clear();
            DropDownList49.Items.Insert(0, new ListItem("---", "---"));
            DropDownList50.Items.Clear();
            DropDownList50.Items.Insert(0, new ListItem("---", "---"));
            DropDownList51.Items.Clear();
            DropDownList51.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList47.SelectedValue.ToString().Trim() != "---")
                loadDDLPais(DropDownList47.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList48_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList49.Items.Clear();
            DropDownList49.Items.Insert(0, new ListItem("---", "---"));
            DropDownList50.Items.Clear();
            DropDownList50.Items.Insert(0, new ListItem("---", "---"));
            DropDownList51.Items.Clear();
            DropDownList51.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList48.SelectedValue.ToString().Trim() != "---")
                loadDDLDepartamento(DropDownList48.SelectedValue.ToString().Trim(), 2);

        }

        protected void DropDownList49_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList50.Items.Clear();
            DropDownList50.Items.Insert(0, new ListItem("---", "---"));
            DropDownList51.Items.Clear();
            DropDownList51.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList49.SelectedValue.ToString().Trim() != "---")
                loadDDLCiudad(DropDownList49.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList50_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList51.Items.Clear();
            DropDownList51.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList50.SelectedValue.ToString().Trim() != "---")
                loadDDLOficinaSucursal(DropDownList50.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList52_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList53.Items.Clear();
            DropDownList53.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "0"));
            DropDownList7.Items.Clear();
            DropDownList7.Items.Insert(0, new ListItem("---", "0"));
            DropDownList55.Items.Clear();
            DropDownList55.Items.Insert(0, new ListItem("---", "0"));

            if (DropDownList52.SelectedValue.ToString().Trim() != "---")
                loadDDLMacroproceso(DropDownList52.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList53_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "0"));
            DropDownList7.Items.Clear();
            DropDownList7.Items.Insert(0, new ListItem("---", "0"));
            DropDownList55.Items.Clear();
            DropDownList55.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList53.SelectedValue.ToString().Trim() != "---")
                loadDDLProceso(DropDownList53.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList54_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList7.Items.Clear();
            DropDownList7.Items.Insert(0, new ListItem("---", "0"));
            DropDownList55.Items.Clear();
            DropDownList55.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList54.SelectedValue.ToString().Trim() != "---")
                loadDDLSubProceso(DropDownList54.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList56_SelectedIndexChanged(object sender, EventArgs e)
        {
            trRiesgoOperativo3.Visible = false;
            trRiesgoOperativo4.Visible = false;
            trLavadoActivos2.Visible = false;
            DropDownList59.SelectedIndex = 0;
            DropDownList60.Items.Clear();
            DropDownList60.Items.Insert(0, new ListItem("---", "0"));
            DropDownList15.SelectedIndex = 0;
            DropDownList16.SelectedIndex = 0;
            DropDownList57.Items.Clear();
            DropDownList57.Items.Insert(0, new ListItem("---", "0"));
            DropDownList58.Items.Clear();
            DropDownList58.Items.Insert(0, new ListItem("---", "0"));

            for (int i = 0; i < CheckBoxList7.Items.Count; i++)
            {
                CheckBoxList7.Items[i].Selected = false;
            }

            for (int i = 0; i < CheckBoxList8.Items.Count; i++)
            {
                CheckBoxList8.Items[i].Selected = false;
            }

            if (DropDownList56.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLClasificacionGeneral(DropDownList56.SelectedValue.ToString().Trim(), 2);
                if (DropDownList56.SelectedValue.ToString().Trim() == "1")
                {
                    trRiesgoOperativo3.Visible = true;
                    trRiesgoOperativo4.Visible = true;
                    trLavadoActivos2.Visible = false;
                }
                if (DropDownList56.SelectedValue.ToString().Trim() == "2")
                {
                    trRiesgoOperativo3.Visible = false;
                    trRiesgoOperativo4.Visible = false;
                    trLavadoActivos2.Visible = true;
                }
            }
        }

        protected void DropDownList57_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList58.Items.Clear();
            DropDownList58.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList57.SelectedValue.ToString().Trim() != "0")
                loadDDLClasificacionParticular(DropDownList57.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList59_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList60.Items.Clear();
            DropDownList60.Items.Insert(0, new ListItem("---", "0"));

            if (DropDownList59.SelectedValue.ToString().Trim() != "0")
                loadDDLTipoRiesgoOperativo(DropDownList59.SelectedValue.ToString().Trim(), 2);
        }

        protected void DropDownList66_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label172.Text = "";
            if (DropDownList66.SelectedValue.ToString().Trim() != "---")
            {
                Label172.Text = cRiesgo.ValorProbabilidad(DropDownList66.SelectedValue.ToString().Trim());
                calificacionInherente(2);
            }
        }

        protected void DropDownList67_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList9.Items.Clear();
            DropDownList9.Items.Insert(0, new ListItem("---", "---"));
            DropDownList10.Items.Clear();
            DropDownList10.Items.Insert(0, new ListItem("---", "0"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList67.SelectedValue.ToString().Trim() != "---")
                loadDDLMacroproceso(DropDownList67.SelectedValue.ToString().Trim(), 1);
        }

        protected void DropDownList68_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label193.Text = "";
            if (DropDownList68.SelectedValue.ToString().Trim() != "---")
            {
                Label193.Text = cRiesgo.ValorImpacto(DropDownList68.SelectedValue.ToString().Trim());
                calificacionInherente(2);
            }
        }
        #endregion DropDownList

        #region Gridview
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            
            switch (e.CommandName)
            {
                case "Modificar":
                    RowGridRiesgos = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridRiesgos) + Convert.ToInt16(e.CommandArgument);
                    string strErrMsg = string.Empty;
                    try
                    {
                        int Index = Convert.ToInt16(e.CommandArgument);
                        var colsNoVisible = GridView1.DataKeys[Index].Values;
                        Session["IdRiesgo"] = colsNoVisible[0].ToString();
                        Session["ListaCausas"] = colsNoVisible[1].ToString();
                        resetValuesModificarRiesgo();
                        resetValuesModificarRiesgoControl();
                        resetValuesModificarRiesgoCalificacion();
                        resetValuesModificarRiesgoObjetivos();
                        resetValuesModificarRiesgoPlanAccion();
                        resetValuesModificarRiesgoEventos();
                        resetValuesJustificacion();
                        resetValuesJustificacionPlanAccion();
                        detalleRiesgoSeleccionado();
                        loadGridControlesRiesgo();
                        loadInfoControlesRiesgo(ref strErrMsg);
                        loadGridArchivoRiesgo();
                        loadInfoArchivoRiesgo(ref strErrMsg);
                        loadGridComentarioRiesgo();
                        loadInfoComentarioRiesgo(ref strErrMsg);
                        loadGridObjetivoRiesgo();
                        loadInfoObjetivoRiesgo(ref strErrMsg);
                        loadGridPlanAccionRiesgo();
                        loadInfoPlanAccionRiesgo(ref strErrMsg);
                        loadGridEventoRiesgo();
                        loadInfoEventoRiesgo(ref strErrMsg);

                        Session["IdMacroProcesoRiesgo"] = InfoGridRiesgos.Rows[RowGridRiesgos]["IdMacroProceso"].ToString().Trim();
                        Session["IdProcesoRiesgo"] = InfoGridRiesgos.Rows[RowGridRiesgos]["IdProceso"].ToString().Trim();
                        
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error: "+ strErrMsg);
                    }
                    break;
            }
        }
        
        
        
        
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridRiesgos = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridRiesgos;
            GridView1.DataSource = InfoGridRiesgos;
            GridView1.DataBind();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridControlesRiesgo = Convert.ToInt16(e.CommandArgument);
            var colsNoVisible = GridView2.DataKeys[RowGridControlesRiesgo].Values;
            Session["IdControl"] = colsNoVisible[0].ToString();
            string IdRiesgo = Session["IdRiesgo"].ToString();
            string IdControl = colsNoVisible[0].ToString();
            switch (e.CommandName)
            {
                case "Borrar":
                    if ((Session["VerTodosProcesos"].ToString() == "0") || (Session["VerTodosProcesos"].ToString() == string.Empty))
                    {
                        if (Session["IdProcesoRiesgo"].ToString() != Session["IdProcesoUsuario"].ToString())
                            Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción. El proceso del Usuario no pertenece al proceso del riesgo.");
                        else
                        {
                            if (cCuenta.permisosBorrar(PestanaControl) == "False")
                                Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                            else
                            {
                                lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                                mpeMsgBoxOkNo.Show();
                                lbldummyOkNo.Text = "CodigoControl";
                            }
                        }
                    }
                    else
                    {
                        if (cCuenta.permisosBorrar(PestanaControl) == "False")
                            Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                        else
                        {
                            lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                            mpeMsgBoxOkNo.Show();
                            lbldummyOkNo.Text = "CodigoControl";
                        }
                    }
                    break;
                case "Asociar":
                    if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    {
                        omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 3, "Atención");
                        return;
                    }
                    string ListCausas = Session["ListaCausas"].ToString();
                    if (ListCausas != "")
                    {
                        ListCausas = ListCausas.Replace("|", ",");
                        loadInfoConsultarRiesgosCausas();
                        loadInfoConsultarRiesgosCausas(ListCausas,IdControl,IdRiesgo);
                        LtextoCausas.Visible = false;
                    }
                    else
                    {
                        LtextoCausas.Visible = true;
                    }
                    modalPopup.Show();
                    break;
            }
        }
        protected void GVcausasRiesgos_PreRender(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if(GVcausasRiesgos.Rows.Count > 0)
                { 
                int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
                int IdControl = Convert.ToInt32(Session["IdControl"].ToString());
                DataTable dtInfoCausas = new DataTable();
                    for (int rowIndex = 0; rowIndex < GVcausasRiesgos.Rows.Count ; rowIndex++)
                    {
                        GridViewRow row = GVcausasRiesgos.Rows[rowIndex];
                        //GridViewRow previousRow = GVcausasRiesgos.Rows[rowIndex + 1];

                        for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                        {
                            //string text = ((Label)row.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;

                            //string previousText = ((Label)previousRow.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;
                            if (cellIndex == 0)
                            {
                                int IdCausa = Convert.ToInt32(row.Cells[cellIndex].Text);
                                dtInfoCausas = cRiesgo.LoadCausasvsControles(IdRiesgo, IdControl, IdCausa);

                                CheckBox ch = (CheckBox)row.FindControl("CBasociarCausa");
                                if (dtInfoCausas.Rows.Count > 0)
                                    ch.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        protected void Bok_Click(object sender, EventArgs e)
        {
            CRiesgoControl riesgoControl = new CRiesgoControl();
            int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
            int IdControl = Convert.ToInt32(Session["IdControl"].ToString());
            DateTime FechaRegistro = DateTime.Now;
            int UsuarioCreacion = Convert.ToInt32(Session["IdUsuario"].ToString());
            bool flag = false;

            try
            {
                // llena el objeto riesgoControl
                riesgoControl.IdRiesgo = IdRiesgo;
                riesgoControl.IdControl = IdControl;
                riesgoControl.IdUsuario = UsuarioCreacion;

                for (int rowIndex = 0; rowIndex < GVcausasRiesgos.Rows.Count; rowIndex++)
                {
                    GridViewRow row = GVcausasRiesgos.Rows[rowIndex];
                    //GridViewRow previousRow = GVGestionCompetencias.Rows[rowIndex];
                    int IdCausa = Convert.ToInt32(row.Cells[0].Text);

                    // Continúa llenando el objeto riesgoControl
                    riesgoControl.IdCausa = IdCausa.ToString();

                    CheckBox asociar = (CheckBox)row.FindControl("CBasociarCausa");
                    if (asociar.Checked == true)
                    {
                        //cRiesgo.agregarCausasvsControles(IdCausa, IdRiesgo, IdControl, FechaRegistro, UsuarioCreacion, ref flag);
                        cRiesgo.registrarCausaRiesgoControl(riesgoControl);
                        flag = true;
                    }
                    else
                        cRiesgo.deleteCausasvsControles(riesgoControl);
                }
                if (flag == true)
                    Mensaje("Causas Asignadas satisfactoriamente");
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message);
            }

            modalPopup.Hide();

        }
        private void loadInfoConsultarRiesgosCausas(string ListCausas, string IdControl, string IdRiesgo)
        {
            try
            {
                DataTable dtInfo = new DataTable();

                dtInfo = cRiesgo.loadInfoRiesgosCausasNew(ListCausas, IdControl, IdRiesgo);


                if (dtInfo.Rows.Count > 0)
                {
                    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                    {
                        InfoGridConsultarCausasRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdCausas"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["NombreCausas"].ToString().Trim()
                                                                   });

                    }

                    GVcausasRiesgos.DataSource = InfoGridConsultarCausasRiesgos;
                    GVcausasRiesgos.DataBind();
                    GVcausasRiesgos.Visible = true;
                }
                else
                {
                    GVcausasRiesgos.Visible = false;
                    //loadGridConsultarRiesgos();
                    Mensaje("No hay causas para el riesgo seleccionado.");
                }
            }
            catch (Exception ex)
            {
                Mensaje1($"Error al mostrar las causas. {ex.Message} ");
            }
           
        }
        private void loadInfoConsultarRiesgosCausas()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdCausas", typeof(string));
            grid.Columns.Add("NombreCausas", typeof(string));

            InfoGridConsultarCausasRiesgos = grid;
            GVcausasRiesgos.DataSource = InfoGridConsultarCausasRiesgos;
            GVcausasRiesgos.DataBind();
        }
        
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.FromName(strColors[e.Row.RowIndex]);
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridPlanAccionRiesgo = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    if (cCuenta.permisosActualizar(PestanaPlanAccion) == "False")
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        resetValuesModificarRiesgoPlanAccion();
                        detallePlanAccionSeleccionado();
                        loadGridComentarioPlanAccion();
                        loadInfoComentarioPlanAccion();
                        loadGridArchivoPlanAccion();
                        loadInfoArchivoPlanAccion();
                    }
                    break;
            }
        }

        protected void GridView8_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridConsultarControles = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Relacionar":
                    if (cCuenta.permisosAgregar(PestanaControl) == "False")
                    {
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    }
                    else
                    {
                        registrarControlesRiesgo();
                    }
                    break;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoRiesgo = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    //descargarArchivo();
                    mtdDescargarPdfRiesgo();
                    break;
            }
        }

        protected void GridView10_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoPlanAccion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    //descargarArchivoPlanAccion();
                    mtdDescargarPdfPlanAccion();
                    break;
            }
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridComentarioRiesgo = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verComentario();
                    break;
            }
        }

        protected void GridView7_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridObjetivoRiesgo = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Borrar":
                    if ((Session["VerTodosProcesos"].ToString() == "0") || (Session["VerTodosProcesos"].ToString() == string.Empty))
                    {
                        if (Session["IdProcesoRiesgo"].ToString() != Session["IdProcesoUsuario"].ToString())
                        {
                            Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción. El proceso del Usuario no esta asociado al riesgo.");
                        }
                        else
                        {
                            if (cCuenta.permisosBorrar(PestanaObjetivo) == "False")
                            {
                                Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                            }
                            else
                            {
                                lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                                mpeMsgBoxOkNo.Show();
                                lbldummyOkNo.Text = "NombreObjetivos";
                            }
                        }
                    }
                    else
                    {
                        if (cCuenta.permisosBorrar(PestanaObjetivo) == "False")
                        {
                            Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                        }
                        else
                        {
                            lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                            mpeMsgBoxOkNo.Show();
                            lbldummyOkNo.Text = "NombreObjetivos";
                        }
                    }
                    break;
            }
        }

        protected void GridView9_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridComentarioPlanAccion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verComentarioPlanAccion();
                    break;
            }
        }
        #endregion Gridview

        #region Buttons

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(PestanaEventos) == "False")
                Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.ReporteRiesgos("---", "---", "---", "---", "---", "---", "---", "---", "3",
                    InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(), "---");
                DataTable InfoGridReporteRiesgosEventos = new DataTable();
                InfoGridReporteRiesgosEventos = loadGridReporteRiesgosEventos();

                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    String Causas = "", Consecuencias = "";

                    InfoGridReporteRiesgosEventos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoRiesgoOperativo"].ToString().Trim(),
                        Causas,
                        Consecuencias,
                        dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                        dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["ProcesoInvolucrado"].ToString().Trim(),
                        dtInfo.Rows[rows]["AplicativoInvolucrado"].ToString().Trim(),
                        dtInfo.Rows[rows]["ServicioProductoAfectado"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaFinalizacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaDescubrimiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["CuentaPUC"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecuperadoTotal"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecuperadoSeguro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Observaciones"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreDepartamento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreCiudad"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreOficinaSucursal"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreClaseEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoPerdidaEvento"].ToString().Trim()
                     });
                }

                exportExcel(InfoGridReporteRiesgosEventos, Response, "Eventos");
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(PestanaControl) == "False")
                Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                for (int rows = 0; rows < InfoGridControlesRiesgo.Rows.Count; rows++)
                {
                    dt.Rows.Add(new Object[] {
                        InfoGridControlesRiesgo.Rows[rows]["CodigoControl"].ToString().Trim(),
                        InfoGridControlesRiesgo.Rows[rows]["NombreControl"].ToString().Trim()
                    });
                }

                exportExcel(dt, Response, "Controles");
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if ((Session["VerTodosProcesos"].ToString() == "0") || (Session["VerTodosProcesos"].ToString() == string.Empty))
            {
                if (Session["IdProcesoRiesgo"].ToString() != Session["IdProcesoUsuario"].ToString())
                    Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción. El proceso del Usuario no esta asociado al riesgo.");
                else
                {
                    #region Comentado Viejo
                    if (cCuenta.permisosAgregar(PestanaPlanAccion) == "False")
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        resetValuesModificarRiesgoPlanAccion();
                        trAddPlanAccion.Visible = true;
                        ImageButton13.Visible = true;
                        Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                    }
                    #endregion

                    #region NUEVO
                    ////Valida permisos de escritura plan de accion
                    //if (cCuenta.permisosAgregar(PestanaPlanAccion) == "False")
                    //{
                    //    #region Validacion Consulta PA
                    //    //Valida permisos de consulta de plan de accion.
                    //    if (cCuenta.permisosConsulta(PestanaPlanAccion) == "False")
                    //        Mensaje("No tiene los permisos suficientes para consultar los planes de acción.");
                    //    else
                    //    {
                    //        #region Validacion Escritura Justificacion
                    //        //Valida permisos de escritura de justificacion y Adjuntar                             
                    //        if (cCuenta.permisosAgregar(strPestanaJustifPDF) == "False")
                    //        {
                    //            #region  Validacion consulta Justificacion
                    //            //Valida permisos de consulta de justificacion y Adjuntar
                    //            if (cCuenta.permisosConsulta(strPestanaJustifPDF) == "False")
                    //                Mensaje("No tiene los permisos para consultar la Justificación y Adjuntar archivos.");
                    //            else
                    //            {
                    //                #region Habilita a Ninguno
                    //                //Inhabilitar para que todo sea en modo consulta.
                    //                resetValuesModificarRiesgoPlanAccion();
                    //                trAddPlanAccion.Visible = true;
                    //                trAdjComPlaAcci.Visible = true;
                    //                ImageButton13.Visible = false;
                    //                mtdHabilitarSegmento_PA(false, 1);

                    //                Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                    //                #endregion
                    //            }
                    //            #endregion
                    //        }
                    //        else
                    //        {
                    //            #region Habilita a NO Plan Accion SI Justificacion
                    //            resetValuesModificarRiesgoPlanAccion();
                    //            trAddPlanAccion.Visible = true;
                    //            trAdjComPlaAcci.Visible = true;
                    //            ImageButton13.Visible = true;
                    //            mtdHabilitarSegmento_PA(true, 3);

                    //            Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                    //            #endregion
                    //        }
                    //        #endregion
                    //    }
                    //    #endregion
                    //}
                    //else
                    //{
                    //    #region Validacion Escritura Justificacion
                    //    //Valida permisos de escritura de justificacion y Adjuntar                             
                    //    if (cCuenta.permisosAgregar(strPestanaJustifPDF) == "False")
                    //    {
                    //        #region  Validacion consulta Justificacion
                    //        //Valida permisos de consulta de justificacion y Adjuntar
                    //        if (cCuenta.permisosConsulta(strPestanaJustifPDF) == "False")
                    //            Mensaje("No tiene los permisos para consultar la Justificación y Adjuntar archivos.");
                    //        else
                    //        {
                    //            #region Habilita a SI Plan Accion NO Justificacion
                    //            resetValuesModificarRiesgoPlanAccion();
                    //            trAddPlanAccion.Visible = true;
                    //            trAdjComPlaAcci.Visible = true;
                    //            ImageButton13.Visible = true;
                    //            mtdHabilitarSegmento_PA(true, 2);

                    //            TextBox22.Text = ".";
                    //            Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                    //            #endregion
                    //        }
                    //        #endregion
                    //    }
                    //    else
                    //    {
                    //        #region Habilita Todo
                    //        resetValuesModificarRiesgoPlanAccion();
                    //        trAddPlanAccion.Visible = true;
                    //        trAdjComPlaAcci.Visible = true;
                    //        ImageButton13.Visible = true;
                    //        mtdHabilitarSegmento_PA(true, 1);

                    //        Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                    //        #endregion
                    //    }
                    //    #endregion
                    //}
                    #endregion
                }
            }
            else
            {
                #region Comentado Viejo
                if (cCuenta.permisosAgregar(PestanaPlanAccion) == "False")
                    Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    resetValuesModificarRiesgoPlanAccion();
                    trAddPlanAccion.Visible = true;
                    ImageButton13.Visible = true;
                    Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                }
                #endregion

                #region NUEVO
                ////Valida permisos de escritura plan de accion
                //if (cCuenta.permisosAgregar(PestanaPlanAccion) == "False")
                //{
                //    #region Validacion Consulta PA
                //    //Valida permisos de consulta de plan de accion.
                //    if (cCuenta.permisosConsulta(PestanaPlanAccion) == "False")
                //        Mensaje("No tiene los permisos suficientes para consultar los planes de acción.");
                //    else
                //    {
                //        #region Validacion Escritura Justificacion
                //        //Valida permisos de escritura de justificacion y Adjuntar                             
                //        if (cCuenta.permisosAgregar(strPestanaJustifPDF) == "False")
                //        {
                //            #region  Validacion consulta Justificacion
                //            //Valida permisos de consulta de justificacion y Adjuntar
                //            if (cCuenta.permisosConsulta(strPestanaJustifPDF) == "False")
                //                Mensaje("No tiene los permisos para consultar la Justificación y Adjuntar archivos.");
                //            else
                //            {
                //                #region Habilita a Ninguno
                //                //Inhabilitar para que todo sea en modo consulta.
                //                resetValuesModificarRiesgoPlanAccion();
                //                trAddPlanAccion.Visible = true;
                //                trAdjComPlaAcci.Visible = true;
                //                ImageButton13.Visible = false;
                //                mtdHabilitarSegmento_PA(false, 1);

                //                Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                //                #endregion
                //            }
                //            #endregion
                //        }
                //        else
                //        {
                //            #region Habilita a NO Plan Accion SI Justificacion
                //            resetValuesModificarRiesgoPlanAccion();
                //            trAddPlanAccion.Visible = true;
                //            trAdjComPlaAcci.Visible = true;
                //            ImageButton13.Visible = true;
                //            mtdHabilitarSegmento_PA(true, 3);

                //            Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                //            #endregion
                //        }
                //        #endregion
                //    }
                //    #endregion
                //}
                //else
                //{
                //    #region Validacion Escritura Justificacion
                //    //Valida permisos de escritura de justificacion y Adjuntar                             
                //    if (cCuenta.permisosAgregar(strPestanaJustifPDF) == "False")
                //    {
                //        #region  Validacion consulta Justificacion
                //        //Valida permisos de consulta de justificacion y Adjuntar
                //        if (cCuenta.permisosConsulta(strPestanaJustifPDF) == "False")
                //            Mensaje("No tiene los permisos para consultar la Justificación y Adjuntar archivos.");
                //        else
                //        {
                //            #region Habilita a SI Plan Accion NO Justificacion
                //            resetValuesModificarRiesgoPlanAccion();
                //            trAddPlanAccion.Visible = true;
                //            trAdjComPlaAcci.Visible = true;
                //            ImageButton13.Visible = true;
                //            mtdHabilitarSegmento_PA(true, 2);

                //            TextBox22.Text = ".";
                //            Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                //            #endregion
                //        }
                //        #endregion
                //    }
                //    else
                //    {
                //        #region Habilita Todo
                //        resetValuesModificarRiesgoPlanAccion();
                //        trAddPlanAccion.Visible = true;
                //        trAdjComPlaAcci.Visible = true;
                //        ImageButton13.Visible = true;
                //        mtdHabilitarSegmento_PA(true, 1);

                //        Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                //        #endregion
                //    }
                //    #endregion
                //}
                #endregion
            }
        }

        #region Plan Accion
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            try
            {
                if (cCuenta.permisosActualizar(PestanaPlanAccion) == "False")
                {
                    Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    actualizarPlanAccionRiesgo();
                    agregarComentarioPlanAccion();
                    if (DropDownList18.SelectedItem.ToString().Trim() == "Cerrado")
                    {
                        boolEnviarNotificacionCierre(6, Convert.ToInt16(InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim()), Convert.ToInt16(lblIdDependencia1.Text.Trim()), "", "Descripción de la Acción: " + TextBox12.Text.Trim() + "<br />Fecha de Compromiso: " + TextBox15.Text.Trim() + "<br />Fecha de Cierre: " + DateTime.Now.ToString("u") + "<br />Número del Riesgo al cual está asociado el Plan de Acción: " + InfoGridRiesgos.Rows[RowGridRiesgos]["Codigo"].ToString().Trim() + "<br /><br />");
                    }
                    resetValuesModificarRiesgoPlanAccion();
                    loadGridPlanAccionRiesgo();
                    loadInfoPlanAccionRiesgo(ref strErrMsg);
                    Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                    Mensaje("Plan de acción actualizado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al actualizar plan acción. " + ex.Message);
            }
        }

        protected void ImageButton13_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            try
            {
                if (cCuenta.permisosAgregar(PestanaPlanAccion) == "False")
                    Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (Convert.ToInt64(TextBox15.Text.Trim().Replace("-", "")) <= Convert.ToInt64(DateTime.Now.Date.ToString("yyyy-MM-dd").Replace("-", "")))
                        Mensaje1("Debe ingresar una fecha valida de compromiso.");
                    else
                    {
                        //registrarPlanAccionRiesgo();
                        int IdRegistro = mtdRegistrarPlanAccionRiesgo();
                        boolEnviarNotificacion(6, IdRegistro, Convert.ToInt16(lblIdDependencia1.Text.Trim()), TextBox15.Text.Trim() + " 12:00:00:000",
                            "Descripción de la Acción: " + TextBox12.Text.Trim() +
                            "<br />Fecha de Compromiso: " + TextBox15.Text.Trim() +
                            "<br />Número del Riesgo al cual está asociado el Plan de Acción: " + InfoGridRiesgos.Rows[RowGridRiesgos]["Codigo"].ToString().Trim() + "<br /><br />"+
                            "<br />Para mayor información del Plan de Acción ingresar a: " + ConfigurationManager.AppSettings.Get("URL").ToString() + "<br />"+
                            "<br />En el Modulo Riesgo/Riesgo Pestaña Plan de Acción <br />");
                        resetValuesModificarRiesgoPlanAccion();
                        loadGridPlanAccionRiesgo();
                        loadInfoPlanAccionRiesgo(ref strErrMsg);
                        Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
                        Mensaje("Plan de acción agregado con éxito.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al registrar plan acción. " + ex.Message);
            }
        }
        #endregion

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if ((Session["VerTodosProcesos"].ToString() == "0") || (Session["VerTodosProcesos"].ToString() == string.Empty))
                {
                    if (DropDownList10.SelectedValue.ToString() != Session["IdProcesoUsuario"].ToString())
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción. El proceso del Usuario no esta asociado al riesgo.");
                    else
                    {
                        if (cCuenta.permisosAgregar(IdFormulario) == "False")
                            Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                        else
                        {
                            registrarRiesgo();
                            resetValuesAgregarRiesgo();
                            loadGridRiesgos();
                            GetLastRiesgo();
                            Mensaje("El Riesgo <b><FONT COLOR=BLUE> [" + LastRiesgo + "] </FONT></b> fue registrado con éxito");
                        }
                    }
                }
                else
                {
                    if (cCuenta.permisosAgregar(IdFormulario) == "False")
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        registrarRiesgo();
                        resetValuesAgregarRiesgo();
                        loadGridRiesgos();
                        GetLastRiesgo();
                        //Mensaje("El Riesgo [" + LastRiesgo + "] fue registrado con éxito");
                        Mensaje("El Riesgo <b><FONT COLOR=BLUE> [" + LastRiesgo + "] </FONT></b> fue registrado con éxito");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al registrar riesgo. " + ex.Message);
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesModificarRiesgoControl();
            Label37.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesAgregarRiesgo();
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesJustificacionPlanAccion();
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                NuevoRiesgo = 1;
                resetValuesAgregarRiesgo();
                loadCodigoRiesgo();
                tbAgregarRiesgo.Visible = true;
                tbModificarRiesgo.Visible = false;
                TabContainer1.ActiveTabIndex = 0;
                TabContainer2.ActiveTabIndex = 0;
                
            }
        }

        protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if ((Session["VerTodosProcesos"].ToString() == "0") || (Session["VerTodosProcesos"].ToString() == string.Empty))
                {
                    if (Session["IdProcesoRiesgo"].ToString() != Session["IdProcesoUsuario"].ToString())
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción. El proceso del Usuario no esta asociado al riesgo.");
                    else
                    {
                        if (cCuenta.permisosActualizar(IdFormulario) == "False")
                            Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                        else
                        {
                            modificarRiesgo();
                            agregarComentarioRiesgo();
                            mtdGenerarNotificacion();
                            resetValuesModificarRiesgo();
                            resetValuesModificarRiesgoControl();
                            resetValuesModificarRiesgoCalificacion();
                            resetValuesModificarRiesgoObjetivos();
                            resetValuesModificarRiesgoPlanAccion();
                            resetValuesModificarRiesgoEventos();
                            resetValuesJustificacion();
                            resetValuesJustificacionPlanAccion();
                            loadGridRiesgos();
                            Mensaje("Riesgo actualizado con éxito");
                        }
                    }
                }
                else
                {
                    if (cCuenta.permisosActualizar(IdFormulario) == "False")
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        modificarRiesgo();
                        agregarComentarioRiesgo();
                        mtdGenerarNotificacion();
                        resetValuesModificarRiesgo();
                        resetValuesModificarRiesgoControl();
                        resetValuesModificarRiesgoCalificacion();
                        resetValuesModificarRiesgoObjetivos();
                        resetValuesModificarRiesgoPlanAccion();
                        resetValuesModificarRiesgoEventos();
                        resetValuesJustificacion();
                        resetValuesJustificacionPlanAccion();
                        loadGridRiesgos();
                        Mensaje("Riesgo actualizado con éxito");
                    }
                }
                calificacionResidual();
            }
            catch (Exception ex)
            {
                Mensaje1("Error al modificar riesgo. " + ex.Message);
            }
        }

        protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            try
            {
                if ((Session["VerTodosProcesos"].ToString() == "0") || (Session["VerTodosProcesos"].ToString() == string.Empty))
                {
                    if (Session["IdProcesoRiesgo"].ToString() != Session["IdProcesoUsuario"].ToString())
                    {
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción. El proceso del Usuario no esta asociado al riesgo.");
                    }
                    else
                    {
                        if (cCuenta.permisosAgregar(PestanaObjetivo) == "False")
                        {
                            Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                        }
                        else
                        {
                            registrarObjetivoRiesgo();
                            DropDownList61.SelectedIndex = 0;
                            DropDownList5.SelectedIndex = 0;
                            loadGridObjetivoRiesgo();
                            loadInfoObjetivoRiesgo(ref strErrMsg);
                            Mensaje("Objetivo relacionado con éxito.");
                        }
                    }
                }
                else
                {
                    if (cCuenta.permisosAgregar(PestanaObjetivo) == "False")
                    {
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    }
                    else
                    {
                        registrarObjetivoRiesgo();
                        DropDownList61.SelectedIndex = 0;
                        DropDownList5.SelectedIndex = 0;
                        loadGridObjetivoRiesgo();
                        loadInfoObjetivoRiesgo(ref strErrMsg);
                        Mensaje("Objetivo relacionado con éxito.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al relacionar el objetivo. " + ex.Message);
            }
        }

        protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (TextBox11.Text.Trim() == "" && TextBox17.Text.Trim() == "" &&
                    DropDownList19.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList20.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList21.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList22.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList4.SelectedValue.ToString().Trim() == "---")
                    Mensaje1("Debe ingresar por lo menos un parámetro de consulta.");
                else
                {
                    inicializarValores();
                    resetValuesModificarRiesgo();
                    resetValuesModificarRiesgoControl();
                    resetValuesModificarRiesgoCalificacion();
                    resetValuesModificarRiesgoObjetivos();
                    resetValuesModificarRiesgoPlanAccion();
                    resetValuesModificarRiesgoEventos();
                    resetValuesJustificacion();
                    resetValuesJustificacionPlanAccion();
                    resetValuesAgregarRiesgo();
                    loadGridRiesgos();
                    loadInfoRiesgos();
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void ImageButton14_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesModificarRiesgoPlanAccion();
            Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
        }

        protected void ImageButton15_Click1(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(PestanaPlanAccion) == "False")
                    Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload2.HasFile)
                    {
                        /*if (System.IO.Path.GetExtension(FileUpload2.FileName).ToLower().ToString().Trim() == ".pdf")
                        {*/
                            mtdCargarPdfPlanAccion();
                            loadGridArchivoPlanAccion();
                            loadInfoArchivoPlanAccion();
                            omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                        /*}
                        else
                            Mensaje("El archivo a cargar debe ser en formato PDF.");*/
                    }
                    else
                        Mensaje("No hay archivos para cargar.");
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al agregar el archivo. " + ex.Message);
            }
        }

        protected void ImageButton15_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesConsulta();
            resetValuesModificarRiesgo();
            resetValuesModificarRiesgoControl();
            resetValuesModificarRiesgoCalificacion();
            resetValuesModificarRiesgoObjetivos();
            resetValuesModificarRiesgoPlanAccion();
            resetValuesModificarRiesgoEventos();
            resetValuesJustificacion();
            resetValuesJustificacionPlanAccion();
            resetValuesAgregarRiesgo();
            loadGridRiesgos();
        }

        protected void ImageButton16_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        /*if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {*/
                            mtdCargarPdfRiesgo();
                            loadGridArchivoRiesgo();
                            loadInfoArchivoRiesgo(ref strErrMsg);
                            omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                        /*}
                        else
                            Mensaje1("Archivo sin guardar. Solo archivos en formato .pdf");*/
                    }
                    else
                        Mensaje("No hay archivos para cargar.");
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al agregar el archivo. " + ex.Message);
            }
        }

        protected void ImageButton17_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesJustificacion();
        }

        protected void ImageButton25_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if ((Session["VerTodosProcesos"].ToString() == "0") || (Session["VerTodosProcesos"].ToString() == string.Empty))
                {
                    if (Session["IdProcesoRiesgo"].ToString() != Session["IdProcesoUsuario"].ToString())
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción. El proceso del Usuario no pertenece al proceso del riesgo.");
                    else
                    {
                        if (cCuenta.permisosConsulta(PestanaControl) == "False")
                            Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                        else
                        {
                            if (TextBox18.Text.Trim() == "" && TextBox19.Text.Trim() == "" && TextBox23.Text.Trim() == "")
                                Mensaje("Debe ingresar por lo menos un parámetro de consulta.");
                            else
                            {
                                loadGridConsultarControles();
                                loadInfoConsultarControles();
                            }
                        }
                    }
                }
                else
                {
                    if (cCuenta.permisosConsulta(PestanaControl) == "False")
                        Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        if (TextBox18.Text.Trim() == "" && TextBox19.Text.Trim() == "" && TextBox23.Text.Trim() == "")
                            Mensaje("Debe ingresar por lo menos un parámetro de consulta.");
                        else
                        {
                            loadGridConsultarControles();
                            loadInfoConsultarControles();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al realizar la consulta. " + ex.Message);
            }
        }

        #region Eliminacion relacion Riesgos con controles
        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            string strMessage = string.Empty;
            string strErrMsg = string.Empty;
            switch (lbldummyOkNo.Text.Trim())
            {
                case "CodigoControl":
                    
                    try
                    {
                        //desasociarControlesRiesgo();
                        mtdDesasociarRiesgoControl(InfoGridControlesRiesgo.Rows[RowGridControlesRiesgo]["IdControlesRiesgo"].ToString().Trim(), ref strMessage);
                        loadGridControlesRiesgo();
                        loadInfoControlesRiesgo(ref strErrMsg);

                        if (string.IsNullOrEmpty(strMessage))
                        {
                            mtdLoadGridAudRiesgoControl();
                            mtdLoadInfoAudRiesgoControl();
                            Mensaje("Control desasociado con éxito.");
                        }
                        else
                            Mensaje(strMessage);
                    }
                    catch (Exception ex)
                    {
                        Mensaje1("Error al eliminar la información. " + ex.Message);
                    }
                    break;
                case "NombreObjetivos":
                    try
                    {
                        desasociarObjetivoRiesgo();
                        DropDownList61.SelectedIndex = 0;
                        loadGridObjetivoRiesgo();
                        loadInfoObjetivoRiesgo(ref strErrMsg);
                        Mensaje("Objetivo desasociado con éxito.");
                    }
                    catch (Exception ex)
                    {
                        Mensaje1("Error al eliminar la información. " + ex.Message);
                    }
                    break;
                case "Recalificar":
                    try
                    {
                        DataTable DtInfoRiesgos = new DataTable();
                        DtInfoRiesgos = cRiesgo.loadInfoRiesgoMasivos();

                        for (int x = 0; x < DtInfoRiesgos.Rows.Count; x++)
                        {
                            calificacionResidual_Masiva(DtInfoRiesgos.Rows[x]["IdProbabilidad"].ToString().Trim(), DtInfoRiesgos.Rows[x]["IdImpacto"].ToString().Trim(), DtInfoRiesgos.Rows[x]["IdRiesgo"].ToString().Trim());
                        }
                        Mensaje("Todos los registros de Riesgos Activos, fueron recalificados correctamente");
                    }
                    catch (Exception ex)
                    {
                        Mensaje1("Error al Recalificar Riesgos. " + ex.Message);
                    }
                    break;
            }
        }
        #endregion Eliminacion relacion Riesgos con controles

        #region Causas y Consecuencias
        protected void btnModCausasConsecuencias_Click(object sender, EventArgs e)
        {
            checkBoxPanel1.Enabled = true;
            checkBoxPanel2.Enabled = true;
        }

        protected void btnAsignarCausa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxCausas.Text.Trim()))
                Mensaje("Por favor ingrese alguna causa.");
            else
            {
                for (int j = 0; j < CheckBoxList3.Items.Count; j++)
                {
                    if (tbxCausas.Text.Trim() == CheckBoxList3.Items[j].Text.ToLower().Trim())
                    {
                        CheckBoxList3.Items[j].Selected = true;
                        tbxCausas.Text = string.Empty;
                        break;
                    }
                }

            }
        }

        protected void btnAsignarConsecuencia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxConsecuencias.Text.Trim()))
                Mensaje("Por favor ingrese alguna Consecuencia.");
            else
            {
                for (int j = 0; j < CheckBoxList4.Items.Count; j++)
                {
                    if (tbxConsecuencias.Text.Trim() == CheckBoxList4.Items[j].Text.ToLower().Trim())
                    {
                        CheckBoxList4.Items[j].Selected = true;
                        tbxConsecuencias.Text = string.Empty;
                        break;
                    }
                }

            }
        }
        #endregion

        #endregion Buttons

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        #region Metodos
        #region Mensajes
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
        #endregion

        #region Resets
        protected void ResetValues_ModificarRiesgo(object sender, ImageClickEventArgs e)
        {
            resetValuesModificarRiesgo();
            resetValuesModificarRiesgoControl();
            resetValuesModificarRiesgoCalificacion();
            resetValuesModificarRiesgoObjetivos();
            resetValuesModificarRiesgoPlanAccion();
            resetValuesModificarRiesgoEventos();
            resetValuesJustificacion();
            resetValuesJustificacionPlanAccion();
        }

        private void resetValuesAgregarRiesgo()
        {
            #region DROPDOWNS
            DropDownList41.SelectedIndex = 0;
            DropDownList42.Items.Clear();
            DropDownList42.Items.Insert(0, new ListItem("---", "---"));
            DropDownList43.Items.Clear();
            DropDownList43.Items.Insert(0, new ListItem("---", "---"));
            DropDownList44.Items.Clear();
            DropDownList44.Items.Insert(0, new ListItem("---", "---"));
            DropDownList63.Items.Clear();
            DropDownList63.Items.Insert(0, new ListItem("---", "---"));
            DropDownList67.SelectedIndex = 0;
            DropDownList9.Items.Clear();
            DropDownList9.Items.Insert(0, new ListItem("---", "---"));
            DropDownList10.Items.Clear();
            DropDownList10.Items.Insert(0, new ListItem("---", "0"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            DropDownList1.SelectedIndex = 0;
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "0"));
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "0"));
            DropDownList8.SelectedIndex = 0;
            DropDownList14.Items.Clear();
            DropDownList14.Items.Insert(0, new ListItem("---", "0"));
            DropDownList12.SelectedIndex = 0;
            DropDownList13.SelectedIndex = 0;
            #endregion DROPDOWNS

            #region CHECKBOXLISTS
            for (int i = 0; i < CheckBoxList5.Items.Count; i++)
            {
                CheckBoxList5.Items[i].Selected = false;
            }
            for (int i = 0; i < CheckBoxList6.Items.Count; i++)
            {
                CheckBoxList6.Items[i].Selected = false;
            }
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                CheckBoxList2.Items[i].Selected = false;
            }
            for (int i = 0; i < CheckBoxList9.Items.Count; i++)
            {
                CheckBoxList9.Items[i].Selected = false;
            }
            #endregion CHECKBOXLISTS

            trRiesgoOperativo1.Visible = false;
            trRiesgoOperativo2.Visible = false;
            trLavadoActivos1.Visible = false;
            lblIdDependencia2.Text = "";
            lblIdDependencia2.Visible = false;
            DropDownList45.SelectedIndex = 0;
            DropDownList46.SelectedIndex = 0;
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox1.Text = "";
            TextBox20.Text = "";
            TextBox40.Text = "";
            TextBox41.Text = "";
            TextBox42.Text = "";
            TextBox43.Text = "";
            Label174.Text = "";
            Panel1.BackColor = System.Drawing.Color.FromName("Transparent");
            tbGridRiesgos.Visible = true;
            tbModificarRiesgo.Visible = false;
            tbAgregarRiesgo.Visible = false;

            #region TREEVIEWS
            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;
            #endregion TREEVIEWS
        }

        private void resetValuesModificarRiesgo()
        {
            #region DDL
            DropDownList7.Items.Clear();
            DropDownList7.Items.Insert(0, new ListItem("---", "0"));

            DropDownList15.SelectedIndex = 0;
            DropDownList16.SelectedIndex = 0;
            DropDownList47.SelectedIndex = 0;

            DropDownList48.Items.Clear();
            DropDownList48.Items.Insert(0, new ListItem("---", "---"));
            DropDownList49.Items.Clear();
            DropDownList49.Items.Insert(0, new ListItem("---", "---"));
            DropDownList50.Items.Clear();
            DropDownList50.Items.Insert(0, new ListItem("---", "---"));
            DropDownList51.Items.Clear();
            DropDownList51.Items.Insert(0, new ListItem("---", "---"));
            DropDownList52.SelectedIndex = 0;
            DropDownList53.Items.Clear();
            DropDownList53.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "0"));
            DropDownList55.Items.Clear();
            DropDownList55.Items.Insert(0, new ListItem("---", "0"));
            DropDownList56.SelectedIndex = 0;
            DropDownList57.Items.Clear();
            DropDownList57.Items.Insert(0, new ListItem("---", "0"));
            DropDownList58.Items.Clear();
            DropDownList58.Items.Insert(0, new ListItem("---", "0"));
            DropDownList59.SelectedIndex = 0;
            DropDownList60.Items.Clear();
            DropDownList60.Items.Insert(0, new ListItem("---", "0"));

            DropDownList66.SelectedIndex = 0;
            DropDownList68.SelectedIndex = 0;

            #endregion

            #region CheckList

            #region Causas y Consecuencias
            checkBoxPanel1.Enabled = false;
            checkBoxPanel2.Enabled = false;
            ckbCausaAsoc.Items.Clear();
            ckbConsecuenciaAsoc.Items.Clear();

            for (int i = 0; i < CheckBoxList3.Items.Count; i++)
            {
                CheckBoxList3.Items[i].Selected = false;
            }

            for (int i = 0; i < CheckBoxList4.Items.Count; i++)
            {
                CheckBoxList4.Items[i].Selected = false;
            }
            #endregion

            for (int i = 0; i < CheckBoxList7.Items.Count; i++)
            {
                CheckBoxList7.Items[i].Selected = false;
            }
            for (int i = 0; i < CheckBoxList8.Items.Count; i++)
            {
                CheckBoxList8.Items[i].Selected = false;
            }
            for (int i = 0; i < CheckBoxList10.Items.Count; i++)
            {
                CheckBoxList10.Items[i].Selected = false;
            }
            #endregion

            #region Treeview
            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;
            #endregion

            #region Otros Campos
            trRiesgoOperativo3.Visible = false;
            trRiesgoOperativo4.Visible = false;
            trLavadoActivos2.Visible = false;
            tbGridRiesgos.Visible = true;
            tbModificarRiesgo.Visible = false;
            tbAgregarRiesgo.Visible = false;

            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox10.Text = "";
            TextBox21.Text = "";

            lblIdDependencia3.Text = "";
            lblIdDependencia3.Visible = false;

            Panel4.BackColor = System.Drawing.Color.FromName("Transparent");
            #endregion
        }

        private void resetValuesModificarRiesgoControl()
        {
            Label37.Text = "";
            TextBox18.Text = "";
            TextBox19.Text = "";
            TextBox23.Text = "";
            lblIdDependencia4.Text = "";
            loadGridConsultarControles();


            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;
        }

        private void resetValuesJustificacionPlanAccion()
        {
            TextBox22.ReadOnly = false;
            TextBox22.Text = "";
            ImageButton7.Visible = false;
        }

        private void resetValuesJustificacion()
        {
            TextBox16.ReadOnly = false;
            TextBox16.Text = "";
            ImageButton17.Visible = false;
        }

        private void resetValuesConsulta()
        {
            TextBox11.Text = "";
            TextBox17.Text = "";
            DropDownList19.SelectedIndex = 0;
            DropDownList20.Items.Clear();
            DropDownList20.Items.Insert(0, new ListItem("---", "---"));
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            DropDownList4.SelectedIndex = 0;
        }

        private void resetValuesModificarRiesgoCalificacion()
        {
            Label33.Text = "";
            Label28.Text = "";
            Panel5.BackColor = System.Drawing.Color.FromName("Transparent");
            Label35.Text = "";
            Panel6.BackColor = System.Drawing.Color.FromName("Transparent");
            Label39.Text = "";
            Panel7.BackColor = System.Drawing.Color.FromName("Transparent");


            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;
        }

        private void resetValuesModificarRiesgoObjetivos()
        {
            Label56.Text = "";
            DropDownList61.SelectedIndex = 0;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;
        }

        private void resetValuesModificarRiesgoPlanAccion()
        {
            Label119.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            TextBox14.Text = "";
            TextBox15.Text = "";
            DropDownList17.SelectedIndex = 0;
            DropDownList18.SelectedIndex = 0;

            ImageButton13.Visible = false;
            ImageButton3.Visible = false;
            trAddPlanAccion.Visible = false;
            ImageButton7.Visible = false;
            trAdjComPlaAcci.Visible = false;

            lblIdDependencia1.Text = "";
            lblIdDependencia1.Visible = false;

            TextBox22.Text = "";
            TextBox22.ReadOnly = false;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;
        }

        private void resetValuesModificarRiesgoEventos()
        {
            Label62.Text = "";

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;
        }
        #endregion Resets

        #region Objetivo
        private void registrarObjetivoRiesgo()
        {
            cRiesgo.registrarObjetivoRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(), DropDownList61.SelectedValue.ToString().Trim());
        }

        private void desasociarObjetivoRiesgo()
        {
            cRiesgo.desasociarObjetivoRiesgo(InfoGridObjetivoRiesgo.Rows[RowGridObjetivoRiesgo]["IdObjetivosRiesgo"].ToString().Trim());
        }
        #endregion Objetivo

        #region Eliminacion relacion Riesgos con controles
        //private void desasociarControlesRiesgo()
        //{
        //    cRiesgo.desasociarControlesRiesgo(InfoGridControlesRiesgo.Rows[RowGridControlesRiesgo]["IdControlesRiesgo"].ToString().Trim());
        //}

        private void mtdDesasociarRiesgoControl(string strIdControlRiesgo, ref string strMessage)
        {
            cRiesgo.mtdQuitarRelacionRiesgoControl(strIdControlRiesgo, ref strMessage);
        }

        private void mtdLoadGridAudRiesgoControl()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Id", typeof(string));
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("IdControl", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("Fecha", typeof(string));

            InfoGridAudRiesgoControl = grid;
            GridView11.DataSource = InfoGridAudRiesgoControl;
            GridView11.DataBind();
        }

        private void mtdLoadInfoAudRiesgoControl()
        {
            DataTable dtInfo = new DataTable();
            PromedioControl = 0;
            dtInfo = cRiesgo.mtdLoadInfoAudRiesgoControl();

            if (dtInfo.Rows.Count > 0)
            {
                #region Ciclo para poner la informacion de los controles desasociados con el riesgo
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAudRiesgoControl.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["Id"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdControl"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),                                                                   
                        dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha"].ToString().Trim(),
                        });
                }
                #endregion Ciclo para poner la informacion de los controles desasociados con el riesgo

                GridView11.DataSource = InfoGridAudRiesgoControl;
                GridView11.DataBind();
            }
        }
        #endregion Eliminacion relacion Riesgos con controles

        #region Calificacion
        private void promedioCalificacionControl()
        {
            for (int i = 0; i < InfoIntervalos.Rows.Count; i++)
            {
                if (PromedioControl > Convert.ToDouble(InfoIntervalos.Rows[i]["limiteInferior"].ToString().Trim()) && PromedioControl <= Convert.ToDouble(InfoIntervalos.Rows[i]["limiteSuperior"].ToString().Trim()))
                {
                    IdCalificacionControl = InfoIntervalos.Rows[i]["IdCalificacionControl"].ToString().Trim();
                    mostrarCalificacionControl();
                    break;
                }
            }
        }

        private void mostrarCalificacionControl()
        {
            for (int i = 0; i < InfoCalificacionControl.Rows.Count; i++)
            {
                if (IdCalificacionControl == InfoCalificacionControl.Rows[i]["IdCalificacionControl"].ToString().Trim())
                {
                    Label35.Text = InfoCalificacionControl.Rows[i]["NombreEscala"].ToString().Trim();
                    Panel6.BackColor = System.Drawing.Color.FromName(InfoCalificacionControl.Rows[i]["Color"].ToString().Trim());
                    break;
                }
            }
        }

        /*private void calificacionResidual()
        {
            DataTable dtInfoProbabilidad = new DataTable();
            DataTable dtInfoImpacto = new DataTable();
            int CountControlProbabilidad = 0;
            int CountControlImpacto = 0;
            
            double valorProbabilidad = Convert.ToDouble(cRiesgo.ValorProbabilidad(InfoGridRiesgos.Rows[RowGridRiesgos]["IdProbabilidad"].ToString().Trim()));
            double valorImpacto = Convert.ToDouble(cRiesgo.ValorImpacto(InfoGridRiesgos.Rows[RowGridRiesgos]["IdImpacto"].ToString().Trim()));
            if (InfoGridControlesRiesgo.Rows.Count > 0)
            {
                double valorFinalProbabilidad = 0;
                double valorFinalImpacto = 0;

                DesviacionProbabilidad = 0;
                DesviacionImpacto = 0;

                for (int x = 0; x < InfoGridControlesRiesgo.Rows.Count; x++)
                {
                    switch (InfoGridControlesRiesgo.Rows[x]["IdMitiga"].ToString().Trim())
                    {
                        //Reduce Probabilidad
                        case "1":
                            DesviacionProbabilidad += valorProbabilidad - Convert.ToDouble(InfoGridControlesRiesgo.Rows[x]["DesviacionProbabilidad"].ToString().Trim());
                            //DesviacionImpacto = valorImpacto;
                            //DesviacionImpacto = 0;
                            CountControlProbabilidad += 1;
                            break;
                        //Reduce Impacto
                        case "2":
                            //DesviacionProbabilidad = valorProbabilidad;
                            //DesviacionProbabilidad = 0;
                            DesviacionImpacto += valorImpacto - Convert.ToDouble(InfoGridControlesRiesgo.Rows[x]["DesviacionImpacto"].ToString().Trim());
                            CountControlImpacto += 1;
                            break;
                        //Reduce ambas
                        case "3":
                            DesviacionProbabilidad += valorProbabilidad - Convert.ToDouble(InfoGridControlesRiesgo.Rows[x]["DesviacionProbabilidad"].ToString().Trim());
                            DesviacionImpacto += valorImpacto - Convert.ToDouble(InfoGridControlesRiesgo.Rows[x]["DesviacionImpacto"].ToString().Trim());
                            CountControlProbabilidad += 1;
                            CountControlImpacto += 1;
                            break;
                    }
                }

                if (DesviacionProbabilidad <= 0)
                    {
                        DesviacionProbabilidad = 1;
                    }
                    if (DesviacionImpacto <= 0)
                    {
                        DesviacionImpacto = 1;
                    }

                if (CountControlProbabilidad == 0)
                {
                    valorFinalProbabilidad = valorProbabilidad;
                    CountControlProbabilidad = 1;
                }
                else
                    valorFinalProbabilidad = DesviacionProbabilidad;

                if (CountControlImpacto == 0)
                {
                    valorFinalImpacto = valorImpacto;
                    CountControlImpacto = 1;
                }
                else
                    valorFinalImpacto = DesviacionImpacto;
                
                //Se define el nuevo valor de la Probabilidad
                //valorProbabilidad = (valorFinalProbabilidad / InfoGridControlesRiesgo.Rows.Count);
                valorProbabilidad = (valorFinalProbabilidad / CountControlProbabilidad);

                
                if (valorProbabilidad == 1.5)
                {
                    valorProbabilidad = 1.0;
                }
                else if (valorProbabilidad == 2.5)
                {
                    valorProbabilidad = 2.0;
                }
                else if (valorProbabilidad == 3.5)
                {
                    valorProbabilidad = 3.0;
                }
                else if (valorProbabilidad == 4.5)
                {
                    valorProbabilidad = 4.0;
                }
                else if (valorProbabilidad == 5.5)
                {
                    valorProbabilidad = 5.0;
                }
                else
                {
                    //valorProbabilidad = Math.Round(valorFinalProbabilidad / InfoGridControlesRiesgo.Rows.Count);
                    valorProbabilidad = Math.Round(valorFinalProbabilidad / CountControlProbabilidad);
                }

                //Se define el nuevo valor de la Impacto
                //valorImpacto = (valorFinalImpacto / InfoGridControlesRiesgo.Rows.Count);
                valorImpacto = (valorFinalImpacto / CountControlImpacto);
                
                if (valorImpacto == 1.5)
                {
                    valorImpacto = 1.0;
                }
                else if (valorImpacto == 2.5)
                {
                    valorImpacto = 2.0;
                }
                else if (valorImpacto == 3.5)
                {
                    valorImpacto = 3.0;
                }
                else if (valorImpacto == 4.5)
                {
                    valorImpacto = 4.0;
                }
                else if (valorImpacto == 5.5)
                {
                    valorImpacto = 5.0;
                }
                else
                {
                    //valorImpacto = Math.Round(valorFinalImpacto / InfoGridControlesRiesgo.Rows.Count);
                    valorImpacto = Math.Round(valorFinalImpacto / CountControlImpacto);
                }
                if(valorProbabilidad < 1)
                {
                    valorProbabilidad = 1;
                }
                if(valorProbabilidad > 5)
                {
                    valorProbabilidad = 5;
                }
                if(valorImpacto < 1)
                {
                    valorImpacto = 1;
                }
                if(valorImpacto > 5)
                {
                    valorImpacto = 5;
                }
            }
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.calificacionResidual(valorProbabilidad.ToString().Trim(), valorImpacto.ToString());
            cRiesgo.actualizarRiesgoResidual(valorProbabilidad.ToString().Trim(), valorImpacto.ToString().Trim(), InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            Label39.Text = dtInfo.Rows[0]["NombreRiesgoInherente"].ToString().Trim();
            Panel7.BackColor = System.Drawing.Color.FromName(dtInfo.Rows[0]["Color"].ToString().Trim());
        }*/

        private void calificacionResidual()
        {
            if (InfoGridRiesgos.Rows.Count == 0)
            {
                loadInfoRiesgos();
            }
            int CantidadProbabilidad = 0;
            int CantidadImpacto = 0;
            double valorProbabilidad = Convert.ToDouble(cRiesgo.ValorProbabilidad(InfoGridRiesgos.Rows[RowGridRiesgos]["IdProbabilidad"].ToString().Trim()));
            double valorImpacto = Convert.ToDouble(cRiesgo.ValorImpacto(InfoGridRiesgos.Rows[RowGridRiesgos]["IdImpacto"].ToString().Trim()));
            if (InfoGridControlesRiesgo.Rows.Count > 0)
            {
                double valorFinalProbabilidad = 0;
                double valorFinalImpacto = 0;
                for (int x = 0; x < InfoGridControlesRiesgo.Rows.Count; x++)
                {
                    DesviacionProbabilidad = 0;
                    DesviacionImpacto = 0;
                    switch (InfoGridControlesRiesgo.Rows[x]["IdMitiga"].ToString().Trim())
                    {
                        case "1":
                            CantidadProbabilidad += 1;
                            DesviacionProbabilidad = valorProbabilidad - Convert.ToDouble(InfoGridControlesRiesgo.Rows[x]["DesviacionProbabilidad"].ToString().Trim());
                            //DesviacionImpacto = valorImpacto;
                            if (DesviacionProbabilidad <= 0)
                            {
                                DesviacionProbabilidad = 1;
                            }
                            valorFinalProbabilidad += DesviacionProbabilidad;
                            break;
                        case "2":
                            CantidadImpacto += 1;
                            //DesviacionProbabilidad = valorProbabilidad;
                            DesviacionImpacto = valorImpacto - Convert.ToDouble(InfoGridControlesRiesgo.Rows[x]["DesviacionImpacto"].ToString().Trim());
                            if (DesviacionImpacto <= 0)
                            {
                                DesviacionImpacto = 1;
                            }
                            valorFinalImpacto += DesviacionImpacto;
                            break;
                        case "3":
                            CantidadProbabilidad += 1;
                            CantidadImpacto += 1;
                            DesviacionProbabilidad = valorProbabilidad - Convert.ToDouble(InfoGridControlesRiesgo.Rows[x]["DesviacionProbabilidad"].ToString().Trim());
                            DesviacionImpacto = valorImpacto - Convert.ToDouble(InfoGridControlesRiesgo.Rows[x]["DesviacionImpacto"].ToString().Trim());
                            if (DesviacionProbabilidad <= 0)
                            {
                                DesviacionProbabilidad = 1;
                            }
                            if (DesviacionImpacto <= 0)
                            {
                                DesviacionImpacto = 1;
                            }
                            valorFinalProbabilidad += DesviacionProbabilidad;
                            valorFinalImpacto += DesviacionImpacto;
                            break;
                    }
                    //if (DesviacionProbabilidad <= 0)
                    //{
                    //    DesviacionProbabilidad = 1;
                    //}
                    //if (DesviacionImpacto <= 0)
                    //{
                    //    DesviacionImpacto = 1;
                    //}
                    //valorFinalProbabilidad += DesviacionProbabilidad;
                    //valorFinalImpacto += DesviacionImpacto;
                }
                //valorProbabilidad = Math.Round(valorFinalProbabilidad / InfoGridControlesRiesgo.Rows.Count);
                //valorImpacto = Math.Round(valorFinalImpacto / InfoGridControlesRiesgo.Rows.Count);

                if (CantidadProbabilidad > 0)
                {
                    valorProbabilidad = (valorFinalProbabilidad / CantidadProbabilidad);
                    //valorProbabilidad = (valorFinalProbabilidad / InfoGridControlesRiesgo.Rows.Count);
                    if (valorProbabilidad == 1.5)
                    {
                        valorProbabilidad = 1.0;
                    }
                    else if (valorProbabilidad == 2.5)
                    {
                        valorProbabilidad = 2.0;
                    }
                    else if (valorProbabilidad == 3.5)
                    {
                        valorProbabilidad = 3.0;
                    }
                    else if (valorProbabilidad == 4.5)
                    {
                        valorProbabilidad = 4.0;
                    }
                    else if (valorProbabilidad == 5.5)
                    {
                        valorProbabilidad = 5.0;
                    }
                    else
                    {
                        //valorProbabilidad = Math.Round(valorFinalProbabilidad / InfoGridControlesRiesgo.Rows.Count);
                        valorProbabilidad = Math.Round(valorFinalProbabilidad / CantidadProbabilidad);
                    }
                }

                if (CantidadImpacto > 0)
                {
                    valorImpacto = (valorFinalImpacto / CantidadImpacto);
                    //valorImpacto = (valorFinalImpacto / InfoGridControlesRiesgo.Rows.Count);

                    if (valorImpacto == 1.5)
                    {
                        valorImpacto = 1.0;
                    }
                    else if (valorImpacto == 2.5)
                    {
                        valorImpacto = 2.0;
                    }
                    else if (valorImpacto == 3.5)
                    {
                        valorImpacto = 3.0;
                    }
                    else if (valorImpacto == 4.5)
                    {
                        valorImpacto = 4.0;
                    }
                    else if (valorImpacto == 5.5)
                    {
                        valorImpacto = 5.0;
                    }
                    else
                    {
                        //valorImpacto = Math.Round(valorFinalImpacto / InfoGridControlesRiesgo.Rows.Count);
                        valorImpacto = Math.Round(valorFinalImpacto / CantidadImpacto);
                    }
                }
            }
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.calificacionResidual(valorProbabilidad.ToString().Trim(), valorImpacto.ToString());
            cRiesgo.actualizarRiesgoResidual(valorProbabilidad.ToString().Trim(), valorImpacto.ToString().Trim(), InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            Label39.Text = dtInfo.Rows[0]["NombreRiesgoInherente"].ToString().Trim();
            Panel7.BackColor = System.Drawing.Color.FromName(dtInfo.Rows[0]["Color"].ToString().Trim());
        }

        private void calificacionInherente(int Tipo)
        {
            switch (Tipo)
            {
                case 1:
                    if (DropDownList45.SelectedValue.ToString().Trim() != "---" && DropDownList46.SelectedValue.ToString().Trim() != "---")
                    {
                        DataTable dtInfo = new DataTable();
                        dtInfo = cRiesgo.calificacionInherente(DropDownList45.SelectedValue.ToString().Trim(), DropDownList46.SelectedValue.ToString().Trim());
                        Label174.Text = dtInfo.Rows[0]["NombreRiesgoInherente"].ToString().Trim();
                        Panel1.BackColor = System.Drawing.Color.FromName(dtInfo.Rows[0]["Color"].ToString().Trim());
                    }
                    break;
                case 2:
                    if (DropDownList66.SelectedValue.ToString().Trim() != "---" && DropDownList68.SelectedValue.ToString().Trim() != "---")
                    {
                        DataTable dtInfo = new DataTable();
                        dtInfo = cRiesgo.calificacionInherente(DropDownList66.SelectedValue.ToString().Trim(), DropDownList68.SelectedValue.ToString().Trim());
                        Label197.Text = dtInfo.Rows[0]["NombreRiesgoInherente"].ToString().Trim();
                        Panel4.BackColor = System.Drawing.Color.FromName(dtInfo.Rows[0]["Color"].ToString().Trim());
                    }
                    break;
                case 3:
                    if (DropDownList66.SelectedValue.ToString().Trim() != "---" && DropDownList68.SelectedValue.ToString().Trim() != "---")
                    {
                        DataTable dtInfo = new DataTable();
                        dtInfo = cRiesgo.calificacionInherente(DropDownList66.SelectedValue.ToString().Trim(), DropDownList68.SelectedValue.ToString().Trim());
                        Label197.Text = dtInfo.Rows[0]["NombreRiesgoInherente"].ToString().Trim();
                        Label28.Text = dtInfo.Rows[0]["NombreRiesgoInherente"].ToString().Trim();
                        Panel4.BackColor = System.Drawing.Color.FromName(dtInfo.Rows[0]["Color"].ToString().Trim());
                        Panel5.BackColor = System.Drawing.Color.FromName(dtInfo.Rows[0]["Color"].ToString().Trim());
                    }
                    break;
            }
        }
        #endregion Calificacion

        #region Causas Consecuencias
        private String causas(String Causas)
        {
            DataTable dtInfoCausas = new DataTable();
            dtInfoCausas = cRiesgo.causas(Causas);
            Causas = "";
            for (int ca = 0; ca < dtInfoCausas.Rows.Count; ca++)
            {
                Causas += dtInfoCausas.Rows[ca]["NombreCausas"].ToString().Trim() + ". ";
            }
            return Causas;
        }

        private String consecuencias(String Consecuencias)
        {
            DataTable dtInfoConsecuencias = new DataTable();
            dtInfoConsecuencias = cRiesgo.consecuencias(Consecuencias);
            Consecuencias = "";
            for (int con = 0; con < dtInfoConsecuencias.Rows.Count; con++)
            {
                Consecuencias += dtInfoConsecuencias.Rows[con]["NombreConsecuencia"].ToString().Trim() + ". ";
            }
            return Consecuencias;
        }
        #endregion

        #region Notificacion
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
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
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
                        //14/11/2014
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

                #region Insertar el Registro
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
                #endregion
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
                #region registro en el log de correos enviados
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
                #endregion

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
                    #region Actualiza el Estado del Correo Enviado
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                    #endregion
                }
            }

            return (err);
        }

        private Boolean boolEnviarNotificacionCierre(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            try
            {
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre FROM [Notificaciones].[CorreosDestinatarios] AS CD, [Notificaciones].[Evento] AS E WHERE E. IdEvento = " + idEvento + " AND CD.IdEvento = E.IdEvento";
                SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
                DataTable dtblDiscuss = new DataTable();
                dad.Fill(dtblDiscuss);
                DataView view = new DataView(dtblDiscuss);

                foreach (DataRowView row in view)
                {
                    Copia = row["Copia"].ToString().Trim();
                    Otros = row["Otros"].ToString().Trim();
                    Asunto = "CERRADO - " + row["Asunto"].ToString().Trim();
                    Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
                    NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                    AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                    AJefeMediato = row["AJefeMediato"].ToString().Trim();
                    RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                }

                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO, [Parametrizacion].[DetalleJerarquiaOrg] AS DJ WHERE JO.idHijo = " + idNodoJerarquia + " AND DJ.idHijo = JO.idHijo";
                dad = new SqlDataAdapter(selectCommand, conString);
                dtblDiscuss.Clear();
                dad.Fill(dtblDiscuss);
                view = new DataView(dtblDiscuss);

                foreach (DataRowView row in view)
                {
                    Destinatario = row["CorreoResponsable"].ToString().Trim();
                    idJefeInmediato = row["idPadre"].ToString().Trim();
                }

                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO, [Parametrizacion].[DetalleJerarquiaOrg] AS DJ WHERE JO.idHijo = " + idJefeInmediato + " AND DJ.idHijo = JO.idHijo";
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

                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO, [Parametrizacion].[DetalleJerarquiaOrg] AS DJ WHERE JO.idHijo = " + idJefeMediato + " AND DJ.idHijo = JO.idHijo";
                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                    }
                }

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

                    #region
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region
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

        /// <summary>
        /// Metodo para la estructura y envio de la notificación.
        /// </summary>
        private void mtdGenerarNotificacion()
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "MODIFICACION  DE RIESGO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + TextBox2.Text + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + TextBox3.Text + "<br>";
                TextoAdicional = TextoAdicional + " Descripción : " + TextBox4.Text + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : " + TextBox16.Text.Trim() + "<br>";
                TextoAdicional = TextoAdicional + " Fecha Modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario Modificación : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Modificación : " + Session["nombreUsuario"].ToString() + "<br>";

                boolEnviarNotificacion(19, 1, Convert.ToInt16(lblIdDependencia3.Text.Trim()), System.DateTime.Now.ToString().Trim(), TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje1("Error al generar la notificacion. " + ex.Message);
            }
        }
        private void mtdGenerarNotificacion(int IdRiesgo, string NombreRiesgo, int IdEvento)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "MODIFICACION  DE RIESGO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + IdRiesgo + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + NombreRiesgo + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el cambio de Frecuencia-Cualitativa del Riesgo.<br>";
                TextoAdicional = TextoAdicional + " Fecha Modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario Modificación : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Modificación : " + Session["nombreUsuario"].ToString() + "<br>";

                boolEnviarNotificacion(IdEvento, 1, Convert.ToInt16(lblIdDependencia3.Text.Trim()), System.DateTime.Now.ToString().Trim(), TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje1("Error al generar la notificacion. " + ex.Message);
            }
        }
        #endregion Notificacion

        #region Plan de Accion

        private void agregarComentarioPlanAccion()
        {
            cRiesgo.agregarComentarioPlanAccion(TextBox22.Text.Trim(), InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim());
        }

        private void detallePlanAccionSeleccionado()
        {
            #region NUEVO
            //Valida permisos de escritura plan de accion
            if (cCuenta.permisosAgregar(PestanaPlanAccion) == "False")
            {
                #region Validacion Consulta PA
                //Valida permisos de consulta de plan de accion.
                if (cCuenta.permisosConsulta(PestanaPlanAccion) == "False")
                    Mensaje1("No tiene los permisos suficientes para consultar los planes de acción.");
                else
                {
                    #region Validacion Escritura Justificacion
                    //Valida permisos de escritura de justificacion y Adjuntar                             
                    if (cCuenta.permisosAgregar(strPestanaJustifPDF) == "False")
                    {
                        #region  Validacion consulta Justificacion
                        //Valida permisos de consulta de justificacion y Adjuntar
                        if (cCuenta.permisosConsulta(strPestanaJustifPDF) == "False")
                            Mensaje1("No tiene los permisos para consultar la Justificación y Adjuntar archivos.");
                        else
                        {
                            #region Habilita a Ninguno
                            //Inhabilitar para que todo sea en modo consulta.
                            resetValuesModificarRiesgoPlanAccion();
                            ImageButton3.Visible = false;
                            mtdHabilitarSegmento_PA(false, 1);
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Habilita a NO Plan Accion SI Justificacion
                        resetValuesModificarRiesgoPlanAccion();
                        ImageButton3.Visible = true;
                        mtdHabilitarSegmento_PA(true, 3);
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                #region Validacion Escritura Justificacion
                //Valida permisos de escritura de justificacion y Adjuntar                             
                if (cCuenta.permisosAgregar(strPestanaJustifPDF) == "False")
                {
                    #region  Validacion consulta Justificacion
                    //Valida permisos de consulta de justificacion y Adjuntar
                    if (cCuenta.permisosConsulta(strPestanaJustifPDF) == "False")
                        Mensaje1("No tiene los permisos para consultar la Justificación y Adjuntar archivos.");
                    else
                    {
                        #region Habilita a SI Plan Accion NO Justificacion
                        resetValuesModificarRiesgoPlanAccion();
                        ImageButton3.Visible = true;
                        mtdHabilitarSegmento_PA(true, 2);

                        TextBox22.Text = ".";
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region Habilita Todo
                    resetValuesModificarRiesgoPlanAccion();
                    ImageButton3.Visible = true;
                    mtdHabilitarSegmento_PA(true, 1);
                    #endregion
                }
                #endregion
            }
            #endregion

            //resetValuesModificarRiesgoPlanAccion();
            //ImageButton3.Visible = true;

            trAddPlanAccion.Visible = true;
            trAdjComPlaAcci.Visible = true;


            #region Llenado informacion
            TextBox12.Text = InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["DescripcionAccion"].ToString().Trim();
            TextBox13.Text = InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["NombreHijo"].ToString().Trim();
            lblIdDependencia1.Text = InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["Responsable"].ToString().Trim();
            TextBox15.Text = InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["FechaCompromiso"].ToString().Trim();
            Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
            TextBox14.Text = InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["ValorRecurso"].ToString().Trim();

            for (int i = 0; i < DropDownList17.Items.Count; i++)
            {
                DropDownList17.SelectedIndex = i;
                if (DropDownList17.SelectedValue.ToString().Trim() == InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdTipoRecursoPlanAccion"].ToString().Trim())
                    break;
            }

            for (int i = 0; i < DropDownList18.Items.Count; i++)
            {
                DropDownList18.SelectedIndex = i;
                if (DropDownList18.SelectedValue.ToString().Trim() == InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdEstadoPlanAccion"].ToString().Trim())
                    break;
            }
            #endregion
        }

        private void registrarPlanAccionRiesgo()
        {
            cRiesgo.registrarPlanAccionRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(), TextBox12.Text.Trim(), lblIdDependencia1.Text.Trim(), DropDownList17.SelectedValue.ToString().Trim(), TextBox14.Text.Trim(), DropDownList18.SelectedValue.ToString().Trim(), TextBox15.Text.Trim() + " 12:00:00:000");
        }

        private void actualizarPlanAccionRiesgo()
        {
            cRiesgo.actualizarPlanAccionRiesgo(InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim(), TextBox12.Text.Trim(), lblIdDependencia1.Text.Trim(), DropDownList17.SelectedValue.ToString().Trim(), TextBox14.Text.Trim(), DropDownList18.SelectedValue.ToString().Trim(), TextBox15.Text.Trim() + " 12:00:00:000");
        }

        private void verComentarioPlanAccion()
        {
            TextBox22.Text = InfoGridComentarioPlanAccion.Rows[RowGridComentarioPlanAccion]["Comentario"].ToString().Trim();
            TextBox22.ReadOnly = true;
            ImageButton7.Visible = true;
        }

        private int mtdRegistrarPlanAccionRiesgo()
        {
            int intIdRegistro = 0;
            DataTable dtInfo = new DataTable();

            dtInfo = cRiesgo.mtdRegistrarPlanAccionRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(), TextBox12.Text.Trim(), lblIdDependencia1.Text.Trim(), DropDownList17.SelectedValue.ToString().Trim(), TextBox14.Text.Trim(), DropDownList18.SelectedValue.ToString().Trim(), TextBox15.Text.Trim() + " 12:00:00:000");

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intIdRegistro = Convert.ToInt32(dtInfo.Rows[0][0].ToString());
            }

            return intIdRegistro;
        }

        private void mtdHabilitarSegmento_PA(bool booEstado, int intOpcion)
        {
            switch (intOpcion)
            {
                case 1:
                case 4:
                    // (ninguno) ni PlanAccion ni Justifi_PDF o
                    // (Todos) si PlanAccion si Justifi_PDF

                    //PlanAccion
                    mtdHabilitar_PA(booEstado);

                    //PDF y Justifi
                    mtdHabilitar_JustPDF(booEstado);

                    break;

                case 2: //si PlanAccion no Justifi_PDF

                    //PlanAccion
                    mtdHabilitar_PA(booEstado);

                    //PDF y Justifi
                    mtdHabilitar_JustPDF(!booEstado);
                    break;
                case 3:  //no PlanAccion si Justifi_PDF
                    //PlanAccion
                    mtdHabilitar_PA(!booEstado);

                    //PDF y Justifi
                    mtdHabilitar_JustPDF(booEstado);
                    break;

            }


        }

        private void mtdHabilitar_PA(bool booEstado)
        {
            TextBox12.Enabled = booEstado;
            imgDependencia1.Enabled = booEstado;
            TextBox14.Enabled = booEstado;
            TextBox15.Enabled = booEstado;
            DropDownList17.Enabled = booEstado;
            DropDownList18.Enabled = booEstado;
        }

        private void mtdHabilitar_JustPDF(bool booEstado)
        {
            //PDF
            FileUpload2.Enabled = booEstado;
            ImageButton15.Enabled = booEstado;
            GridView10.Enabled = booEstado;

            //Justifi
            TextBox22.Enabled = booEstado;
            ImageButton7.Enabled = booEstado;
            GridView9.Enabled = booEstado;
        }

        #endregion Plan de Accion

        #region PDFs
        #region PDF Plan Accion

        private void loadFilePlanAccion()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cControl.loadCodigoArchivoControl();
            if (dtInfo.Rows.Count > 0)
            {
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim() + "-" + FileUpload2.FileName.ToString().Trim();
            }
            else
            {
                nameFile = "1-" + InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim() + "-" + FileUpload2.FileName.ToString().Trim();
            }
            FileUpload2.SaveAs(Server.MapPath("~/Archivos/PDFsPlanAccion/") + nameFile);
            cRiesgo.agregarArchivoPlanAccion(InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim(), nameFile);
        }

        private void descargarArchivoPlanAccion()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccion/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }
        #endregion PDF Plan Accion

        #region PDF Riesgos
        private void loadFile()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cControl.loadCodigoArchivoControl();
            if (dtInfo.Rows.Count > 0)
            {
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" +
                    InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim() + "-" +
                    FileUpload1.FileName.ToString().Trim();
            }
            else
            {
                nameFile = "1-" + InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim() + "-" +
                    FileUpload1.FileName.ToString().Trim();
            }
            FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFsRiesgo/") + nameFile);
            cRiesgo.agregarArchivoRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(), nameFile);
        }

        private void descargarArchivo()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFsRiesgo/" + InfoGridArchivoRiesgo.Rows[RowGridArchivoRiesgo]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }
        #endregion PDF Riesgos

        private void mtdCargarPdfRiesgo()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty, strIdControl = "3";
            #endregion Vars

            dtInfo = cControl.loadCodigoArchivoControl();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() +
                    "-" + InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim() +
                    "-" + FileUpload1.FileName.ToString().Trim();
            else
                strNombreArchivo = "1-" + InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim() +
                    "-" + FileUpload1.FileName.ToString().Trim();
            #endregion Nombre Archivo

            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);

            cRiesgo.mtdAgregarArchivoPdf(strIdControl, InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfRiesgo()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoRiesgo.Rows[RowGridArchivoRiesgo]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cRiesgo.mtdDescargarArchivoPdf(strNombreArchivo);
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

        private void mtdCargarPdfPlanAccion()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty, strIdControl = "4";
            #endregion Vars

            dtInfo = cControl.loadCodigoArchivoControl();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() +
                    "-" + InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim() +
                    "-" + FileUpload2.FileName.ToString().Trim();
            else
                strNombreArchivo = "1-" + InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim() +
                    "-" + FileUpload2.FileName.ToString().Trim();
            #endregion Nombre Archivo

            Stream fs = FileUpload2.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);

            cRiesgo.mtdAgregarArchivoPdf(strIdControl, InfoGridPlanAccionRiesgo.Rows[RowGridPlanAccionRiesgo]["IdPlanAccion"].ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfPlanAccion()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cRiesgo.mtdDescargarArchivoPdf(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                /*Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();*/
                string extension = Path.GetExtension(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim());
                if (extension == ".pdf")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".pdf");
                    //Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bPdfData);
                    Response.End();
                }
                if (extension == ".xlsx")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".xlsx");
                    //Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bPdfData);
                    Response.End();
                }
                if (extension == ".xls")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".xls");
                    //Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bPdfData);
                    Response.End();
                }
                if (extension == ".docx")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".docx");
                    //Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bPdfData);
                    Response.End();
                }
                if (extension == ".doc")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".doc");
                    //Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bPdfData);
                    Response.End();
                }
            }
        }
        #endregion PDFs

        private void GetLastRiesgo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cRiesgo.GetLastRiesgo();

                if (dtInfo.Rows.Count > 0)
                    LastRiesgo = "R" + dtInfo.Rows[0]["UltRiesgo"].ToString().Trim();
                else
                    LastRiesgo = "R1";
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar el código riesgo. " + ex.Message);
            }
        }

        private void GetLastLegislacion()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cRiesgo.GetLastLegislacion();

                //Ajuste RCC - 15/01/2014
                if (NuevoRiesgo == 1 && dtInfo.Rows.Count > 0)
                {
                    Label1.Visible = false;
                    TextBox8.Visible = false;
                }

                if (dtInfo.Rows.Count > 0)
                    TextBox8.Text = "R" + dtInfo.Rows[0]["LastLegislacion"].ToString().Trim();
                else
                    TextBox8.Text = "R1";
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar el código riesgo. " + ex.Message);
            }
        }

        private void registrarControlesRiesgo()
        {
            string strErrMsg = string.Empty;
            if (validarControl())
            {
                try
                {
                    cRiesgo.registrarControlesRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(), InfoGridConsultarControles.Rows[RowGridConsultarControles]["IdControl"].ToString().Trim());
                    loadGridControlesRiesgo();
                    loadInfoControlesRiesgo(ref strErrMsg);
                    Mensaje("Control relacionado con éxito.");
                    mtdGenerarNotificacionControl(InfoGridRiesgos.Rows[RowGridRiesgos]["Codigo"].ToString().Trim(), InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim());
                }
                catch (Exception ex)
                {
                    //Mensaje("Error: Se está intentando registrar varias veces el mismo control.<br /><br />Por favor consulte nuevamente el Riesgo.");
                    loadGridControlesRiesgo();
                    loadInfoControlesRiesgo(ref strErrMsg);
                    Mensaje("Control relacionado con éxito.");
                }
            }
            else
                Mensaje("El control ya se encuentra relacionado.");
        }
        private void mtdGenerarNotificacionControl(string CodigoRiesgo, string NombreRiesgo)
        {

            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "ASIGNACIÓN DE CONTROL" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código Riesgo: " + CodigoRiesgo + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Riesgo: " + NombreRiesgo + "<br>";
                TextoAdicional = TextoAdicional + " Código Control : " + InfoGridConsultarControles.Rows[RowGridConsultarControles]["CodigoControl"].ToString().Trim() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Control : " + InfoGridConsultarControles.Rows[RowGridConsultarControles]["NombreControl"].ToString().Trim() + "<br>";
                TextoAdicional = TextoAdicional + " Descripcíon Control : " + InfoGridConsultarControles.Rows[RowGridConsultarControles]["DescripcionControl"].ToString().Trim() + ".<br>";
                TextoAdicional = TextoAdicional + " Fecha Asignación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario Asignación : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Asignación : " + Session["nombreUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " URL de Ingreso a la Aplicación : " + ConfigurationManager.AppSettings.Get("URL").ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Por favor ingresar y dirigirte al modulo Riesgos/Riesgos pestaña Control <br>";

                boolEnviarNotificacion(19, Convert.ToInt32(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim()), Convert.ToInt32(InfoGridRiesgos.Rows[RowGridRiesgos]["IdResponsableRiesgo"].ToString().Trim()), System.DateTime.Now.ToString().Trim(), TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }
        private bool validarControl()
        {
            bool validar = true;
            for (int i = 0; i < InfoGridControlesRiesgo.Rows.Count; i++)
            {
                if (InfoGridControlesRiesgo.Rows[i]["IdControl"].ToString().Trim() == InfoGridConsultarControles.Rows[RowGridConsultarControles]["IdControl"].ToString().Trim())
                {
                    validar = false;
                    break;
                }
            }
            return validar;
        }

        private void agregarComentarioRiesgo()
        {
            cRiesgo.agregarComentarioRiesgo(TextBox16.Text.ToString().Trim(), InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
        }

        private void modificarRiesgo()
        {
            #region Variables
            String ListaCausas = "";
            String ListaConsecuencias = "";
            String ListaRiesgoAsociadoLA = "";
            String ListaFactorRiesgoLAFT = "";
            String ListaTratamiento = "";
            #endregion Variables

            #region Ciclo Checkbox
            for (int i = 0; i < CheckBoxList3.Items.Count; i++)
            {
                if (CheckBoxList3.Items[i].Selected)
                    ListaCausas += CheckBoxList3.Items[i].Value.ToString().Trim() + "|";
            }

            for (int i = 0; i < CheckBoxList4.Items.Count; i++)
            {
                if (CheckBoxList4.Items[i].Selected)
                    ListaConsecuencias += CheckBoxList4.Items[i].Value.ToString().Trim() + "|";
            }

            for (int i = 0; i < CheckBoxList7.Items.Count; i++)
            {
                if (CheckBoxList7.Items[i].Selected)
                    ListaRiesgoAsociadoLA += CheckBoxList7.Items[i].Value.ToString().Trim() + "|";
            }

            for (int i = 0; i < CheckBoxList8.Items.Count; i++)
            {
                if (CheckBoxList8.Items[i].Selected)
                    ListaFactorRiesgoLAFT += CheckBoxList8.Items[i].Value.ToString().Trim() + "|";

            }

            for (int i = 0; i < CheckBoxList10.Items.Count; i++)
            {
                if (CheckBoxList10.Items[i].Selected)
                    ListaTratamiento += CheckBoxList10.Items[i].Value.ToString().Trim() + "|";

            }
            #endregion Ciclo Checkbox

            cRiesgo.modificarRiesgo(DropDownList47.SelectedValue.ToString().Trim(), DropDownList48.SelectedValue.ToString().Trim(),
                DropDownList49.SelectedValue.ToString().Trim(), DropDownList50.SelectedValue.ToString().Trim(), DropDownList51.SelectedValue.ToString().Trim(),
                DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList7.SelectedValue.ToString().Trim(), DropDownList55.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(),
                DropDownList57.SelectedValue.ToString().Trim(), DropDownList58.SelectedValue.ToString().Trim(), DropDownList59.SelectedValue.ToString().Trim(),
                DropDownList60.SelectedValue.ToString().Trim(), DropDownList15.SelectedValue.ToString().Trim(), DropDownList16.SelectedValue.ToString().Trim(),
                ListaRiesgoAsociadoLA, ListaFactorRiesgoLAFT, TextBox3.Text.Trim(), TextBox4.Text.Trim(), ListaCausas, ListaConsecuencias, lblIdDependencia3.Text.Trim(),
                DropDownList66.SelectedValue.ToString().Trim(), TextBox5.Text.Trim(), TextBox6.Text.Trim(), DropDownList68.SelectedValue.ToString().Trim(),
                TextBox7.Text.Trim(), TextBox10.Text.Trim(), ListaTratamiento, InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(),
                TextBox16.Text.ToString().Trim());
        }

        private void registrarRiesgo()
        {
            #region Variables
            String ListaCausas = "";
            String ListaConsecuencias = "";
            String ListaRiesgoAsociadoLA = "";
            String ListaFactorRiesgoLAFT = "";
            String ListaTratamiento = "";
            #endregion Variables

            #region Ciclos de CheckBox
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    ListaCausas += CheckBoxList1.Items[i].Value.ToString().Trim() + "|";
                }
            }
            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                if (CheckBoxList2.Items[i].Selected)
                {
                    ListaConsecuencias += CheckBoxList2.Items[i].Value.ToString().Trim() + "|";
                }
            }
            for (int i = 0; i < CheckBoxList5.Items.Count; i++)
            {
                if (CheckBoxList5.Items[i].Selected)
                {
                    ListaRiesgoAsociadoLA += CheckBoxList5.Items[i].Value.ToString().Trim() + "|";
                }
            }
            for (int i = 0; i < CheckBoxList6.Items.Count; i++)
            {
                if (CheckBoxList6.Items[i].Selected)
                {
                    ListaFactorRiesgoLAFT += CheckBoxList6.Items[i].Value.ToString().Trim() + "|";
                }
            }
            for (int i = 0; i < CheckBoxList9.Items.Count; i++)
            {
                if (CheckBoxList9.Items[i].Selected)
                {
                    ListaTratamiento += CheckBoxList9.Items[i].Value.ToString().Trim() + "|";
                }
            }
            #endregion Ciclos de CheckBox

            cRiesgo.registrarRiesgo(DropDownList41.SelectedValue.ToString().Trim(), DropDownList42.SelectedValue.ToString().Trim(),
                DropDownList43.SelectedValue.ToString().Trim(), DropDownList44.SelectedValue.ToString().Trim(), DropDownList63.SelectedValue.ToString().Trim(),
                DropDownList67.SelectedValue.ToString().Trim(), DropDownList9.SelectedValue.ToString().Trim(), DropDownList10.SelectedValue.ToString().Trim(),
                DropDownList6.SelectedValue.ToString().Trim(), DropDownList11.SelectedValue.ToString().Trim(), DropDownList1.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList8.SelectedValue.ToString().Trim(),
                DropDownList14.SelectedValue.ToString().Trim(), DropDownList12.SelectedValue.ToString().Trim(), DropDownList13.SelectedValue.ToString().Trim(),
                ListaRiesgoAsociadoLA, ListaFactorRiesgoLAFT, TextBox8.Text.Trim(), TextBox9.Text.Trim(), TextBox1.Text.Trim(), ListaCausas, ListaConsecuencias,
                lblIdDependencia2.Text.Trim(), DropDownList45.SelectedValue.ToString().Trim(), TextBox40.Text.Trim(), TextBox41.Text.Trim(),
                DropDownList46.SelectedValue.ToString().Trim(), TextBox42.Text.Trim(), TextBox43.Text.Trim(), ListaTratamiento);
        }

        private void detalleRiesgoSeleccionado()
        {
            tbModificarRiesgo.Visible = true;
            tbAgregarRiesgo.Visible = false;

            #region Region
            for (int i = 0; i < DropDownList47.Items.Count; i++)
            {
                DropDownList47.SelectedIndex = i;
                if(InfoGridRiesgos.Rows.Count > 0)
                { 
                if (DropDownList47.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdRegion"].ToString().Trim())
                    break;
                else
                    DropDownList47.SelectedIndex = 0;
                }else
                    DropDownList47.SelectedIndex = 0;
            }
            #endregion

            #region Pais
            loadDDLPais(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRegion"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList48.Items.Count; i++)
            {
                DropDownList48.SelectedIndex = i;
                if (DropDownList48.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdPais"].ToString().Trim())
                    break;
                else
                    DropDownList48.SelectedIndex = 0;
            }
            #endregion

            #region Dpto
            loadDDLDepartamento(InfoGridRiesgos.Rows[RowGridRiesgos]["IdPais"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList49.Items.Count; i++)
            {
                DropDownList49.SelectedIndex = i;
                if (DropDownList49.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdDepartamento"].ToString().Trim())
                    break;
                else
                    DropDownList49.SelectedIndex = 0;
            }
            #endregion

            #region Ciudad
            loadDDLCiudad(InfoGridRiesgos.Rows[RowGridRiesgos]["IdDepartamento"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList50.Items.Count; i++)
            {
                DropDownList50.SelectedIndex = i;
                if (DropDownList50.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdCiudad"].ToString().Trim())
                    break;
                else
                    DropDownList50.SelectedIndex = 0;
            }
            #endregion

            #region Sucursal
            loadDDLOficinaSucursal(InfoGridRiesgos.Rows[RowGridRiesgos]["IdCiudad"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList51.Items.Count; i++)
            {
                DropDownList51.SelectedIndex = i;
                if (DropDownList51.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdOficinaSucursal"].ToString().Trim())
                    break;
                else
                    DropDownList51.SelectedIndex = 0;
            }
            #endregion

            #region Cadena Valor
            for (int i = 0; i < DropDownList52.Items.Count; i++)
            {
                DropDownList52.SelectedIndex = i;
                if (DropDownList52.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdCadenaValor"].ToString().Trim())
                    break;
                else
                    DropDownList52.SelectedIndex = 0;
            }
            #endregion

            #region MacroProceso
            loadDDLMacroproceso(InfoGridRiesgos.Rows[RowGridRiesgos]["IdCadenaValor"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList53.Items.Count; i++)
            {
                DropDownList53.SelectedIndex = i;
                if (DropDownList53.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdMacroproceso"].ToString().Trim())
                    break;
                else
                    DropDownList53.SelectedIndex = 0;
            }
            #endregion

            #region Proceso
            loadDDLProceso(InfoGridRiesgos.Rows[RowGridRiesgos]["IdMacroproceso"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList54.Items.Count; i++)
            {
                DropDownList54.SelectedIndex = i;
                if (DropDownList54.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdProceso"].ToString().Trim())
                    break;
                else
                    DropDownList54.SelectedIndex = 0;
            }
            #endregion

            #region SubProceso
            loadDDLSubProceso(infoGridRiesgos.Rows[RowGridRiesgos]["IdProceso"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList7.Items.Count; i++)
            {
                DropDownList7.SelectedIndex = i;
                if (DropDownList7.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdSubProceso"].ToString().Trim())
                    break;
                else
                    DropDownList7.SelectedIndex = 0;
            }
            #endregion

            #region Actividad
            loadDDLActividad(InfoGridRiesgos.Rows[RowGridRiesgos]["IdSubProceso"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList55.Items.Count; i++)
            {
                DropDownList55.SelectedIndex = i;
                if (DropDownList55.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdActividad"].ToString().Trim())
                    break;
                else
                    DropDownList55.SelectedIndex = 0;
            }
            #endregion

            #region Clasificacion
            for (int i = 0; i < DropDownList56.Items.Count; i++)
            {
                DropDownList56.SelectedIndex = i;
                if (DropDownList56.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdClasificacionRiesgo"].ToString().Trim())
                    break;
                else
                    DropDownList56.SelectedIndex = 0;
            }
            #endregion

            #region Clasificacion Gral
            loadDDLClasificacionGeneral(InfoGridRiesgos.Rows[RowGridRiesgos]["IdClasificacionRiesgo"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList57.Items.Count; i++)
            {
                DropDownList57.SelectedIndex = i;
                if (DropDownList57.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdClasificacionGeneralRiesgo"].ToString().Trim())
                    break;
                else
                    DropDownList57.SelectedIndex = 0;
            }
            #endregion

            #region Clasificacion Particular
            loadDDLClasificacionParticular(InfoGridRiesgos.Rows[RowGridRiesgos]["IdClasificacionGeneralRiesgo"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList58.Items.Count; i++)
            {
                DropDownList58.SelectedIndex = i;
                if (DropDownList58.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdClasificacionParticularRiesgo"].ToString().Trim())
                    break;
                else
                    DropDownList58.SelectedIndex = 0;
            }
            #endregion

            #region Riesgo
            if (DropDownList56.SelectedValue.ToString().Trim() == "1")
            {
                trRiesgoOperativo3.Visible = true;
                trRiesgoOperativo4.Visible = true;
                trLavadoActivos2.Visible = false;
            }
            #endregion

            #region Riesgo
            if (DropDownList56.SelectedValue.ToString().Trim() == "2")
            {
                trRiesgoOperativo3.Visible = false;
                trRiesgoOperativo4.Visible = false;
                trLavadoActivos2.Visible = true;
            }
            #endregion

            #region Factor Riesgo
            for (int i = 0; i < DropDownList59.Items.Count; i++)
            {
                DropDownList59.SelectedIndex = i;
                if (DropDownList59.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdFactorRiesgoOperativo"].ToString().Trim())
                    break;
                else
                    DropDownList59.SelectedIndex = 0;

            }
            #endregion

            #region Tipo Riesgo
            loadDDLTipoRiesgoOperativo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdFactorRiesgoOperativo"].ToString().Trim(), 2);
            for (int i = 0; i < DropDownList60.Items.Count; i++)
            {
                DropDownList60.SelectedIndex = i;
                if (DropDownList60.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdTipoRiesgoOperativo"].ToString().Trim())
                    break;
                else
                {
                    DropDownList60.SelectedIndex = 0;
                }
            }
            #endregion

            #region Tipo Evento
            for (int i = 0; i < DropDownList15.Items.Count; i++)
            {
                DropDownList15.SelectedIndex = i;
                if (DropDownList15.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdTipoEventoOperativo"].ToString().Trim())
                {
                    break;
                }
                else
                {
                    DropDownList15.SelectedIndex = 0;
                }
            }
            #endregion

            #region Riesgo Asociado
            for (int i = 0; i < DropDownList16.Items.Count; i++)
            {
                DropDownList16.SelectedIndex = i;
                if (DropDownList16.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgoAsociadoOperativo"].ToString().Trim())
                    break;
                else
                    DropDownList16.SelectedIndex = 0;

            }
            #endregion

            #region Riesgo asociado LA
            char[] delimiter = { '|' };
            String[] ListaRiesgoAsociadoLA = InfoGridRiesgos.Rows[RowGridRiesgos]["ListaRiesgoAsociadoLA"].ToString().Trim().Split(delimiter);
            for (int i = 0; i < ListaRiesgoAsociadoLA.Length; i++)
            {
                for (int j = 0; j < CheckBoxList7.Items.Count; j++)
                {
                    if (ListaRiesgoAsociadoLA[i].ToString().Trim() == CheckBoxList7.Items[j].Value.ToString().Trim())
                    {
                        CheckBoxList7.Items[j].Selected = true;
                    }
                }
            }
            #endregion

            #region Riesgo asociado LAFT
            String[] ListaFactorRiesgoLAFT = InfoGridRiesgos.Rows[RowGridRiesgos]["ListaFactorRiesgoLAFT"].ToString().Trim().Split(delimiter);
            for (int i = 0; i < ListaFactorRiesgoLAFT.Length; i++)
            {
                for (int j = 0; j < CheckBoxList8.Items.Count; j++)
                {
                    if (ListaFactorRiesgoLAFT[i].ToString().Trim() == CheckBoxList8.Items[j].Value.ToString().Trim())
                    {
                        CheckBoxList8.Items[j].Selected = true;
                    }
                }
            }
            #endregion

            #region Info Riesgo
            TextBox2.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Codigo"].ToString().Trim();
            TextBox3.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
            Label37.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
            Label33.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
            Label56.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
            Label119.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
            Label62.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
            TextBox4.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Descripcion"].ToString().Trim();
            #endregion

            #region Causas
            string strCausa = string.Empty;
            string[] listaCausas = InfoGridRiesgos.Rows[RowGridRiesgos]["listaCausas"].ToString().Trim().Split(delimiter);

            for (int i = 0; i < listaCausas.Length; i++)
            {
                #region Nuevo
                if (!string.IsNullOrEmpty(listaCausas[i].ToString().Trim()))
                {
                    if (string.IsNullOrEmpty(strCausa))
                        strCausa = listaCausas[i].ToString().Trim();
                    else
                        strCausa = strCausa + "," + listaCausas[i].ToString().Trim();
                }
                #endregion

                #region Viejo
                for (int j = 0; j < CheckBoxList3.Items.Count; j++)
                {
                    if (listaCausas[i].ToString().Trim() == CheckBoxList3.Items[j].Value.ToString().Trim())
                    {
                        CheckBoxList3.Items[j].Selected = true;
                        break;
                    }
                }
                #endregion
            }

            #region Causas asociadas
            DataTable dtInfo = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(strCausa))
                    dtInfo = cRiesgo.mtdLoadCausas(strCausa);

                ckbCausaAsoc.Items.Clear();
                if (dtInfo != null)
                    for (int i = 0; i < dtInfo.Rows.Count; i++)
                    {
                        ckbCausaAsoc.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreCausas"].ToString().Trim(), dtInfo.Rows[i]["IdCausas"].ToString().Trim()));
                        ckbCausaAsoc.Items[i].Selected = true;
                    }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar causas. " + ex.Message);
            }
            #endregion
            #endregion

            #region Consecuencias
            string strConsecuencias = string.Empty;
            string[] listaConsecuencias = InfoGridRiesgos.Rows[RowGridRiesgos]["listaConsecuencias"].ToString().Trim().Split(delimiter);
            for (int i = 0; i < listaConsecuencias.Length; i++)
            {
                #region Nuevo
                if (!string.IsNullOrEmpty(listaConsecuencias[i].ToString().Trim()))
                {
                    if (string.IsNullOrEmpty(strConsecuencias))
                        strConsecuencias = listaConsecuencias[i].ToString().Trim();
                    else
                        strConsecuencias = strConsecuencias + "," + listaConsecuencias[i].ToString().Trim();
                }
                #endregion

                #region Viejo
                for (int j = 0; j < CheckBoxList4.Items.Count; j++)
                {
                    if (listaConsecuencias[i].ToString().Trim() == CheckBoxList4.Items[j].Value.ToString().Trim())
                    {
                        CheckBoxList4.Items[j].Selected = true;
                        break;
                    }
                }
                #endregion
            }

            #region Consecuencias asociadas
            dtInfo = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(strConsecuencias))
                    dtInfo = cRiesgo.mtdLoadConsecuencias(strConsecuencias);

                ckbConsecuenciaAsoc.Items.Clear();
                if (dtInfo != null)
                    for (int i = 0; i < dtInfo.Rows.Count; i++)
                    {
                        ckbConsecuenciaAsoc.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreConsecuencia"].ToString().Trim(), dtInfo.Rows[i]["IdConsecuencia"].ToString().Trim()));
                        ckbConsecuenciaAsoc.Items[i].Selected = true;
                    }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar causas. " + ex.Message);
            }
            #endregion
            #endregion

            #region Dependencia
            TextBox21.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["NombreHijo"].ToString().Trim();
            lblIdDependencia3.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["IdResponsableRiesgo"].ToString().Trim();
            for (int i = 0; i < DropDownList66.Items.Count; i++)
            {
                DropDownList66.SelectedIndex = i;
                if (DropDownList66.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdProbabilidad"].ToString().Trim())
                    break;
                else
                    DropDownList66.SelectedIndex = 0;
            }
            #endregion

            #region Ocurrencia
            string valor = DropDownList66.SelectedValue.ToString().Trim();
            if (valor == "---")
            {
                Label172.Text = cRiesgo.ValorProbabilidad("0");
            }
            else
            {
                Label172.Text = cRiesgo.ValorProbabilidad(DropDownList66.SelectedValue.ToString().Trim());
            }
            TextBox5.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["OcurrenciaEventoDesde"].ToString().Trim();
            TextBox6.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["OcurrenciaEventoHasta"].ToString().Trim();
            for (int i = 0; i < DropDownList68.Items.Count; i++)
            {
                DropDownList68.SelectedIndex = i;
                if (DropDownList68.SelectedValue.ToString().Trim() == InfoGridRiesgos.Rows[RowGridRiesgos]["IdImpacto"].ToString().Trim())
                {
                    break;
                }
                else
                {
                    DropDownList68.SelectedIndex = 0;
                }
            }
            #endregion

            #region Tratamiento
            Label193.Text = cRiesgo.ValorImpacto(DropDownList68.SelectedValue.ToString().Trim());
            TextBox7.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["PerdidaEconomicaDesde"].ToString().Trim();
            TextBox10.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["PerdidaEconomicaHasta"].ToString().Trim();
            String[] listaTratamiento = InfoGridRiesgos.Rows[RowGridRiesgos]["ListaTratamiento"].ToString().Trim().Split(delimiter);
            for (int i = 0; i < listaTratamiento.Length; i++)
            {
                for (int j = 0; j < CheckBoxList10.Items.Count; j++)
                {
                    if (listaTratamiento[i].ToString().Trim() == CheckBoxList10.Items[j].Value.ToString().Trim())
                    {
                        CheckBoxList10.Items[j].Selected = true;
                        break;
                    }
                }
            }
            #endregion

            calificacionInherente(3);
        }

        private void armarIntervalos()
        {
            DataTable dtInfo = new DataTable(), dtIntervalos = new DataTable(), dtVariables = new DataTable();
            double maximo = 0, minimo = 0, intervalo = 0, delta = 0, sumMaximo = 0, sumMinimo = 0;

            try
            {
                //dtInfo = cControl.valorMaxMinIntervalo();
                dtVariables = cControl.variablesControl();
                if (dtInfo != null)
                {
                    if (dtVariables.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtVariables.Rows.Count; i++)
                        {
                            dtInfo = cControl.valorMax_MinVariables(Convert.ToInt32(dtVariables.Rows[i]["IdVariableCalificacionControl"].ToString().Trim()));
                            sumMaximo = sumMaximo + Convert.ToDouble(dtInfo.Rows[0]["Maximo"].ToString().Trim());
                            sumMinimo = sumMinimo + Convert.ToDouble(dtInfo.Rows[0]["Minimo"].ToString().Trim());
                        }
                    }
                    if (sumMaximo != 0)
                    {
                        minimo = sumMinimo;
                        intervalo = sumMaximo - sumMinimo;
                        delta = intervalo / 4;
                        dtIntervalos.Columns.Add("limiteInferior", typeof(string));
                        dtIntervalos.Columns.Add("limiteSuperior", typeof(string));
                        dtIntervalos.Columns.Add("IdCalificacionControl", typeof(string));

                        for (int rows = 4; rows > 0; rows--)
                        {
                            maximo = minimo + delta;
                            dtIntervalos.Rows.Add(new Object[] { minimo.ToString().Trim(), maximo.ToString().Trim(), rows.ToString() });
                            minimo = maximo;
                        }
                        dtIntervalos.Rows[0]["limiteInferior"] = "0";
                        dtIntervalos.Rows[3]["limiteSuperior"] = "100";
                        InfoIntervalos = dtIntervalos;
                    }
                    else
                        Mensaje1("No hay información en las tablas maestras de parametrización. ");
                }
                else
                    Mensaje1("No hay información en las tablas maestras de parametrización. ");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al armar intervalos. " + ex.Message);
            }
        }

        private void verComentario()
        {
            TextBox16.Text = InfoGridComentarioRiesgo.Rows[RowGridComentarioRiesgo]["Comentario"].ToString().Trim();
            TextBox16.ReadOnly = true;
            ImageButton17.Visible = true;
        }

        private void inicializarValores()
        {
            PagIndexInfoGridRiesgos = 0;
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
            dg.DataSource = dt;
            dg.DataBind();
            dg.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        private void calificacionResidual_Masiva(string IdProbabilidad, string IdImpacto, string IdRiesgo)
        {
            int CountControlProbabilidad = 0;
            int CountControlImpacto = 0;
            //Carga de controles asignados al Riesgo
            DataTable dtInfoControles = new DataTable();
            dtInfoControles = cRiesgo.loadInfoControlesRiesgo(IdRiesgo);


            double valorProbabilidad = Convert.ToDouble(cRiesgo.ValorProbabilidad(IdProbabilidad));
            double valorImpacto = Convert.ToDouble(cRiesgo.ValorImpacto(IdImpacto));
            if (dtInfoControles.Rows.Count > 0)
            {
                double valorFinalProbabilidad = 0;
                double valorFinalImpacto = 0;

                DesviacionProbabilidad = 0;
                DesviacionImpacto = 0;

                for (int x = 0; x < dtInfoControles.Rows.Count; x++)
                {
                    switch (dtInfoControles.Rows[x]["IdMitiga"].ToString().Trim())
                    {
                        //Reduce Probabilidad
                        case "1":
                            DesviacionProbabilidad += valorProbabilidad - Convert.ToDouble(dtInfoControles.Rows[x]["DesviacionProbabilidad"].ToString().Trim());
                            //DesviacionImpacto = valorImpacto;
                            //DesviacionImpacto = 0;
                            CountControlProbabilidad += 1;
                            break;
                        //Reduce Impacto
                        case "2":
                            //DesviacionProbabilidad = valorProbabilidad;
                            //DesviacionProbabilidad = 0;
                            DesviacionImpacto += valorImpacto - Convert.ToDouble(dtInfoControles.Rows[x]["DesviacionImpacto"].ToString().Trim());
                            CountControlImpacto += 1;
                            break;
                        //Reduce ambas
                        case "3":
                            DesviacionProbabilidad += valorProbabilidad - Convert.ToDouble(dtInfoControles.Rows[x]["DesviacionProbabilidad"].ToString().Trim());
                            DesviacionImpacto += valorImpacto - Convert.ToDouble(dtInfoControles.Rows[x]["DesviacionImpacto"].ToString().Trim());
                            CountControlProbabilidad += 1;
                            CountControlImpacto += 1;
                            break;
                    }
                }

                if (DesviacionProbabilidad <= 0)
                {
                    DesviacionProbabilidad = 1;
                }
                if (DesviacionImpacto <= 0)
                {
                    DesviacionImpacto = 1;
                }

                if (CountControlProbabilidad == 0)
                {
                    valorFinalProbabilidad = valorProbabilidad;
                    CountControlProbabilidad = 1;
                }
                else
                    valorFinalProbabilidad = DesviacionProbabilidad;

                if (CountControlImpacto == 0)
                {
                    valorFinalImpacto = valorImpacto;
                    CountControlImpacto = 1;
                }
                else
                    valorFinalImpacto = DesviacionImpacto;

                //Se define el nuevo valor de la Probabilidad
                //valorProbabilidad = (valorFinalProbabilidad / InfoGridControlesRiesgo.Rows.Count);
                valorProbabilidad = (valorFinalProbabilidad / CountControlProbabilidad);


                if (valorProbabilidad == 1.5)
                {
                    valorProbabilidad = 1.0;
                }
                else if (valorProbabilidad == 2.5)
                {
                    valorProbabilidad = 2.0;
                }
                else if (valorProbabilidad == 3.5)
                {
                    valorProbabilidad = 3.0;
                }
                else if (valorProbabilidad == 4.5)
                {
                    valorProbabilidad = 4.0;
                }
                else if (valorProbabilidad == 5.5)
                {
                    valorProbabilidad = 5.0;
                }
                else
                {
                    //valorProbabilidad = Math.Round(valorFinalProbabilidad / InfoGridControlesRiesgo.Rows.Count);
                    valorProbabilidad = Math.Round(valorFinalProbabilidad / CountControlProbabilidad);
                }

                //Se define el nuevo valor de la Impacto
                //valorImpacto = (valorFinalImpacto / InfoGridControlesRiesgo.Rows.Count);
                valorImpacto = (valorFinalImpacto / CountControlImpacto);

                if (valorImpacto == 1.5)
                {
                    valorImpacto = 1.0;
                }
                else if (valorImpacto == 2.5)
                {
                    valorImpacto = 2.0;
                }
                else if (valorImpacto == 3.5)
                {
                    valorImpacto = 3.0;
                }
                else if (valorImpacto == 4.5)
                {
                    valorImpacto = 4.0;
                }
                else if (valorImpacto == 5.5)
                {
                    valorImpacto = 5.0;
                }
                else
                {
                    //valorImpacto = Math.Round(valorFinalImpacto / InfoGridControlesRiesgo.Rows.Count);
                    valorImpacto = Math.Round(valorFinalImpacto / CountControlImpacto);
                }
                if (valorProbabilidad < 1)
                {
                    valorProbabilidad = 1;
                }
                if (valorProbabilidad > 5)
                {
                    valorProbabilidad = 5;
                }
                if (valorImpacto < 1)
                {
                    valorImpacto = 1;
                }
                if (valorImpacto > 5)
                {
                    valorImpacto = 5;
                }
            }
            cRiesgo.actualizarRiesgoResidual(valorProbabilidad.ToString().Trim(), valorImpacto.ToString().Trim(), IdRiesgo);
            
        }

        #endregion Metodos

        //Metodo creado para realizar la calificación masiva de todos los riesgos creados en la base de datos
        protected void BtnCalificacionMasiva_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (cCuenta.permisosAgregar(strRecalificarRiesgos) == "False")
                    //cCuenta.permisosConsulta
                    Mensaje1("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {*/
                //    lblMsgBoxOkNo.Text = "Desea Recalificar todos los resigos existentes en la Base de Datos?";
                //    mpeMsgBoxOkNo.Show();
                //    lbldummyOkNo.Text = "Recalificar";
                //DataTable DtInfoRiesgos = new DataTable();
                //DtInfoRiesgos = cRiesgo.loadInfoRiesgoMasivos();
                //for (int x = 0; x < DtInfoRiesgos.Rows.Count; x++)
                //{
                    //calificacionResidual_Masiva(DtInfoRiesgos.Rows[x]["IdProbabilidad"].ToString().Trim(), DtInfoRiesgos.Rows[x]["IdImpacto"].ToString().Trim(), DtInfoRiesgos.Rows[x]["IdRiesgo"].ToString().Trim());
                //}
                //Mensaje("Todos los registros de Riesgos Activos, fueron recalificados correctamente");
                //}
            }
            catch (Exception ex)
            {
                Mensaje1("Error al calificar riesgos masivos" + ex.Message);
            }
        }

        protected void ImbViewJPGimpacto_Click(object sender, ImageClickEventArgs e)
        {
            string str;
            str = "window.open('ViewImg/ViewImg.aspx?op=1','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        protected void ImbViewJPGfrecuencia_Click(object sender, ImageClickEventArgs e)
        {
            string str;
            str = "window.open('ViewImg/ViewImg.aspx?op=2','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        protected void ImbViewJPGfrecuenciaIns_Click(object sender, ImageClickEventArgs e)
        {
            string str;
            str = "window.open('ViewImg/ViewImg.aspx?op=2','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        protected void ImbViewJPGimpactoIns_Click(object sender, ImageClickEventArgs e)
        {
            string str;
            str = "window.open('ViewImg/ViewImg.aspx?op=1','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }
    }
}