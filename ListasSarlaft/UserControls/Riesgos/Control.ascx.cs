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
using Microsoft.Security.Application;
using System.Configuration;
using System.Web.UI.HtmlControls;
using ListasSarlaft.UserControls.Riesgos.Iframe;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class Control : System.Web.UI.UserControl
    {
        string IdFormulario = "5009";
        cControl cControl = new cControl();
        cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        cCuenta cCuenta = new cCuenta();
        private static int LastInsertIdCE;
        private static int NuevoControl = 0;
        private static string LastControl = string.Empty;

        //Realizar

        //Peso para variable, Calificacion categoria. Realizar validacion para que la suma de los pesos sea 100
        //Guardar el peso de la variable en el misma tabla donde se crea formulario "Variables de Calificación Controles"
        //A una variable se le pueden asignar variaas categorias
        //Cada categoria solo debe pertenecer a una variable se debe validar eso
        //en la tabla variable a categoria de uno a muchos para eliminar 5 tablas que hay ahora
        //Actualizar las calificaciones existentes por el valor en lugar del id de la tabla
        //Cargue de actualizacion de variables de calificacion
        //Generar plantilla para nuevo cargue
        //Ajustar las pestañas para poder seleccionar las categorias correctamente

        #region Properties
        private DataTable infoGridComentarioPlanEvaluacion;
        private DataTable InfoGridComentarioPlanEvaluacion
        {
            get
            {
                infoGridComentarioPlanEvaluacion = (DataTable)ViewState["infoGridComentarioPlanEvaluacion"];
                return infoGridComentarioPlanEvaluacion;
            }
            set
            {
                infoGridComentarioPlanEvaluacion = value;
                ViewState["infoGridComentarioPlanEvaluacion"] = infoGridComentarioPlanEvaluacion;
            }
        }

        private int rowGridComentarioPlanEvaluacion;
        private int RowGridComentarioPlanEvaluacion
        {
            get
            {
                rowGridComentarioPlanEvaluacion = (int)ViewState["rowGridComentarioPlanEvaluacion"];
                return rowGridComentarioPlanEvaluacion;
            }
            set
            {
                rowGridComentarioPlanEvaluacion = value;
                ViewState["rowGridComentarioPlanEvaluacion"] = rowGridComentarioPlanEvaluacion;
            }
        }

        private int rowGridArchivoPlanEvaluacion;
        private int RowGridArchivoPlanEvaluacion
        {
            get
            {
                rowGridArchivoPlanEvaluacion = (int)ViewState["rowGridArchivoPlanEvaluacion"];
                return rowGridArchivoPlanEvaluacion;
            }
            set
            {
                rowGridArchivoPlanEvaluacion = value;
                ViewState["rowGridArchivoPlanEvaluacion"] = rowGridArchivoPlanEvaluacion;
            }
        }

        private DataTable infoGridArchivoPlanEvaluacion;
        private DataTable InfoGridArchivoPlanEvaluacion
        {
            get
            {
                infoGridArchivoPlanEvaluacion = (DataTable)ViewState["infoGridArchivoPlanEvaluacion"];
                return infoGridArchivoPlanEvaluacion;
            }
            set
            {
                infoGridArchivoPlanEvaluacion = value;
                ViewState["infoGridArchivoPlanEvaluacion"] = infoGridArchivoPlanEvaluacion;
            }
        }

        private int rowGridPlanEvaluacion;
        private int RowGridPlanEvaluacion
        {
            get
            {
                rowGridPlanEvaluacion = (int)ViewState["rowGridPlanEvaluacion"];
                return rowGridPlanEvaluacion;
            }
            set
            {
                rowGridPlanEvaluacion = value;
                ViewState["rowGridPlanEvaluacion"] = rowGridPlanEvaluacion;
            }
        }

        private DataTable infoGridPlanEvaluacion;
        private DataTable InfoGridPlanEvaluacion
        {
            get
            {
                infoGridPlanEvaluacion = (DataTable)ViewState["infoGridPlanEvaluacion"];
                return infoGridPlanEvaluacion;
            }
            set
            {
                infoGridPlanEvaluacion = value;
                ViewState["infoGridPlanEvaluacion"] = infoGridPlanEvaluacion;
            }
        }

        private DataTable infoGridComentarioControl;
        private DataTable InfoGridComentarioControl
        {
            get
            {
                infoGridComentarioControl = (DataTable)ViewState["infoGridComentarioControl"];
                return infoGridComentarioControl;
            }
            set
            {
                infoGridComentarioControl = value;
                ViewState["infoGridComentarioControl"] = infoGridComentarioControl;
            }
        }

        private DataTable infoPorcentajeCalificarControl;
        private DataTable InfoPorcentajeCalificarControl
        {
            get
            {
                infoPorcentajeCalificarControl = (DataTable)ViewState["infoPorcentajeCalificarControl"];
                return infoPorcentajeCalificarControl;
            }
            set
            {
                infoPorcentajeCalificarControl = value;
                ViewState["infoPorcentajeCalificarControl"] = infoPorcentajeCalificarControl;
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

        private int rowGridControles;
        private int RowGridControles
        {
            get
            {
                rowGridControles = (int)ViewState["rowGridControles"];
                return rowGridControles;
            }
            set
            {
                rowGridControles = value;
                ViewState["rowGridControles"] = rowGridControles;
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

        private DataTable infoGridArchivoControl;
        private DataTable InfoGridArchivoControl
        {
            get
            {
                infoGridArchivoControl = (DataTable)ViewState["infoGridArchivoControl"];
                return infoGridArchivoControl;
            }
            set
            {
                infoGridArchivoControl = value;
                ViewState["infoGridArchivoControl"] = infoGridArchivoControl;
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

        private int rowGridComentarioControl;
        private int RowGridComentarioControl
        {
            get
            {
                rowGridComentarioControl = (int)ViewState["rowGridComentarioControl"];
                return rowGridComentarioControl;
            }
            set
            {
                rowGridComentarioControl = value;
                ViewState["rowGridComentarioControl"] = rowGridComentarioControl;
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

        private int pagIndexInfoGridControles;
        private int PagIndexInfoGridControles
        {
            get
            {
                pagIndexInfoGridControles = (int)ViewState["pagIndexInfoGridControles"];
                return pagIndexInfoGridControles;
            }
            set
            {
                pagIndexInfoGridControles = value;
                ViewState["pagIndexInfoGridControles"] = pagIndexInfoGridControles;
            }
        }
        #endregion

        #region ResposableEjecucion
        private void PopulateTreeViewJerOrg()
        {
            DataTable treeViewData = GetTreeViewDataJerOrg();
            AddTopTreeViewNodesJerOrg(treeViewData);
            TreeViewJerarquiaOrg.ExpandAll();
        }

        private DataTable GetTreeViewDataJerOrg()
        {
            string selectCommand = "SELECT Parametrizacion.JerarquiaOrganizacional.IdHijo, Parametrizacion.JerarquiaOrganizacional.IdPadre, Parametrizacion.JerarquiaOrganizacional.NombreHijo, Parametrizacion.DetalleJerarquiaOrg.NombreResponsable, Parametrizacion.DetalleJerarquiaOrg.CorreoResponsable FROM Parametrizacion.JerarquiaOrganizacional LEFT JOIN Parametrizacion.DetalleJerarquiaOrg ON Parametrizacion.JerarquiaOrganizacional.idHijo = Parametrizacion.DetalleJerarquiaOrg.idHijo";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodesJerOrg(DataTable treeViewData)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                TreeViewJerarquiaOrg.Nodes.Add(newNode);
                AddChildTreeViewNodesJerOrg(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodesJerOrg(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodesJerOrg(treeViewData, newNode);
            }
        }

        protected void TreeViewJerarquiaOrg_SelectedNodeChanged(object sender, EventArgs e)
        {
            TxbResponsableEjecución.Text += "JO-" + TreeViewJerarquiaOrg.SelectedNode.Text + "| ";
            LblResponsableEjecucion.Text += "JO-" + TreeViewJerarquiaOrg.SelectedNode.Value + "|";
            if (Session["modificar"].ToString() == "1")
                mtdShowCalificacion();
        }

        private void PopulateTreeViewParam()
        {
            DataTable treeViewData = GetTreeViewDataParam();
            AddTopTreeViewNodesParam(treeViewData);
            TreeViewTablaParam.ExpandAll();
        }

        private DataTable GetTreeViewDataParam()
        {
            string selectCommand = "SELECT IdGrupoTrabajo,Nombre,CONVERT(VARCHAR,FechaRegistro,120)FechaRegistro FROM Riesgos.GruposTrabajo WHERE Estado = 1";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodesParam(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["Nombre"].ToString(), row["IdGrupoTrabajo"].ToString());
                newNode.ToolTip = "Nombre Grupo: " + row["Nombre"].ToString() + "\rFechaRegistro: " + row["FechaRegistro"].ToString();
                TreeViewTablaParam.Nodes.Add(newNode);
            }
        }

        protected void TreeViewTablaParam_SelectedNodeChanged(object sender, EventArgs e)
        {
            TxbResponsableEjecución.Text += "GT-" + TreeViewTablaParam.SelectedNode.Text + "| ";
            LblResponsableEjecucion.Text += "GT-" + TreeViewTablaParam.SelectedNode.Value + "|";
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);
            scrtManager.RegisterPostBackControl(IBprocess);
            scrtManager.RegisterPostBackControl(ImageButton7);
            scrtManager.RegisterPostBackControl(GridView3);
            scrtManager.RegisterPostBackControl(ImageButton17);
            scrtManager.RegisterPostBackControl(GridView6);
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["CodControl"] != null)
                {
                    TextBox14.Text = Request.QueryString["CodControl"].ToString();

                    inicializarValores();
                    resetValuesControl();
                    resetValuesComentariosArchivos();
                    resetValuesCamposDetalleEvaluacion();
                    resetValuesJustificacionPlanEvaluacion();
                    loadGridControles();
                    loadInfoControles();
                    CommandEventArgs args = new CommandEventArgs("Modificar", null);
                    GridViewRow row = GridView1.SelectedRow;
                    //gvPrincipal_RowCommand(this, new GridViewCommandEventArgs(row, ImgBtnDetail, args));
                    GridView1_RowCommand(this,new GridViewCommandEventArgs(row,null,args));
                }
                else if(Session["LastControl"] != null)
                {
                    if (Session["LastControl"].ToString() != "")
                        omb.ShowMessage("El Control [" + Session["LastControl"].ToString() + "] fue registrado con éxito", 3, "Atención");

                    
                    //armarIntervalos();
                    loadGridControles();
                    mtdLoadInfoControles(Session["idJerarquia"].ToString());
                    
                }
                loadDDLClaseControl();
                loadDDLTipoControl();
                loadDDLResponsableExperiencia();
                loadDDLDocumentacion();
                loadDDLResponsabilidad();
                loadDDLPeriodicidad();
                loadDDLTest();
                loadDDLMitiga();
                loadDDLTipoPruebaPlanEvaluacion();
                loadDDLEstadoPlanEvaluacion();
                loadInfoPorcentajeCalificarControl();
                loadInfoCalificacionControl();
                //inicializarValores();
                PopulateTreeView();
                PopulateTreeViewJerOrg();
                PopulateTreeViewParam();
            }
            else
            {
                if (Session["end"] != null)
                {
                    if (Session["end"].ToString() == "1")
                    {
                        Mensaje("El Control [C" + Session["LastControl"].ToString() + "] fue registrado con éxito");
                        Session["end"] = "0";
                        resetValuesControl();
                        resetValuesComentariosArchivos();
                        resetValuesCamposDetalleEvaluacion();
                        resetValuesJustificacionPlanEvaluacion();

                    }

                }
                Session["total"] = 0;
                Session["LastControl"] = "";
            }
        }
        public void mtdStard()
        {
            trResultado.Visible = false;
            loadDDLClaseControl();
            loadDDLTipoControl();
            loadDDLResponsableExperiencia();
            loadDDLDocumentacion();
            loadDDLResponsabilidad();
            loadDDLPeriodicidad();
            loadDDLTest();
            loadDDLMitiga();
            loadDDLTipoPruebaPlanEvaluacion();
            loadDDLEstadoPlanEvaluacion();
            loadInfoPorcentajeCalificarControl();
            loadInfoCalificacionControl();
            //armarIntervalos();
            loadGridControles();
            mtdLoadInfoControles(Session["idJerarquia"].ToString());
            inicializarValores();
            PopulateTreeView();
            PopulateTreeViewJerOrg();
            PopulateTreeViewParam();
        }
        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView1.ExpandAll();
            TreeView2.ExpandAll();
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
            TextBox34.Text = TreeView1.SelectedNode.Text;
            lblIdDependencia1.Text = TreeView1.SelectedNode.Value;
            if (Session["modificar"].ToString() == "1")
                mtdShowCalificacion();
        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox33.Text = TreeView2.SelectedNode.Text;
            lblIdDependencia2.Text = TreeView2.SelectedNode.Value;
            if (Session["modificar"].ToString() == "1")
                mtdShowCalificacion();
        }

        protected void TreeView3_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox21.Text = TreeView3.SelectedNode.Text;
            lblIdDependencia3.Text = TreeView3.SelectedNode.Value;
            if (Session["modificar"].ToString() == "1")
                mtdShowCalificacion();
        }
        #endregion Treeview

        #region DDL
        private void loadDDLResponsabilidad()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLResponsabilidad();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList12.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreResponsabilidad"].ToString().Trim(), dtInfo.Rows[i]["IdResponsabilidad"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar responsabilidad. " + ex.Message);
            }
        }

        private void loadDDLDocumentacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLDocumentacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList11.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreDocumentacion"].ToString().Trim(), dtInfo.Rows[i]["IdDocumentacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar documentacion. " + ex.Message);
            }
        }

        private void loadDDLResponsableExperiencia()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLResponsableExperiencia();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList8.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreResponsableExperiencia"].ToString().Trim(), dtInfo.Rows[i]["IdResponsableExperiencia"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar responsable experiencia. " + ex.Message);
            }
        }

        private void loadDDLEstadoPlanEvaluacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLEstadoPlanEvaluacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList7.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreEstadoPlanEvaluacion"].ToString().Trim(), dtInfo.Rows[i]["IdEstadoPlanEvaluacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar estado. " + ex.Message);
            }
        }

        private void loadDDLTipoPruebaPlanEvaluacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLTipoPruebaPlanEvaluacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList5.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoPruebaPlanEvaluacion"].ToString().Trim(), dtInfo.Rows[i]["IdTipoPruebaPlanEvaluacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar tipo prueba. " + ex.Message);
            }
        }

        private void loadDDLClaseControl()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLClaseControl();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList9.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClaseControl"].ToString().Trim(), dtInfo.Rows[i]["IdClaseControl"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clase control. " + ex.Message);
            }
        }

        private void loadDDLPeriodicidad()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLPeriodicidad();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList6.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombrePeriodicidad"].ToString().Trim(), dtInfo.Rows[i]["IdPeriodicidad"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar periodicidad. " + ex.Message);
            }
        }

        private void loadDDLTipoControl()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLTipoControl();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList10.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoControl"].ToString().Trim(), dtInfo.Rows[i]["IdTipoControl"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar tipo control. " + ex.Message);
            }
        }

        private void loadDDLTest()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLTest();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTest"].ToString().Trim(), dtInfo.Rows[i]["IdTest"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar test. " + ex.Message);
            }
        }

        private void loadDDLMitiga()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cControl.loadDDLMitiga();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMitiga"].ToString().Trim(), dtInfo.Rows[i]["IdMitiga"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar mitigación. " + ex.Message);
            }
        }
        #endregion DDL

        #region Buttons

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                // Borra el diccionario de variables
                Iframe.IFcategorias.tempValores.Clear();
                trResultado.Visible = false;
                IBprocess.Visible = true;
                Session["modificar"] = 0;
                NuevoControl = 1;
                resetValuesControl();
                resetValuesComentariosArchivos();
                resetValuesCamposDetalleEvaluacion();
                resetValuesJustificacionPlanEvaluacion();
                loadCodigoControl();
                tbCampos.Visible = true;
                //ImageButton4.Visible = true;
                string strErrMsg = string.Empty;
                /*bool result = mtdLoadCalificacionControl(ref strErrMsg);
                if(result == false)
                {
                    Mensaje("No fue posible cargar la Cobertura Del Control.");
                }*/
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdAgregarControl();

                    resetValuesControl();
                    resetValuesComentariosArchivos();
                    resetValuesCamposDetalleEvaluacion();
                    resetValuesJustificacionPlanEvaluacion();
                    loadGridControles();
                    Mensaje("El Control <b><FONT COLOR=BLUE> [" + LastControl + "] </FONT></b> fue registrado con éxito");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el control. " + ex.Message);
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            ModificarControl();
        }

        private void Page_Load1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void IFcategorias_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesControl();
            resetValuesComentariosArchivos();
            resetValuesCamposDetalleEvaluacion();
            resetValuesJustificacionPlanEvaluacion();
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        /*if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {*/
                        mtdCargarPdfControl();
                        loadGridArchivoControl();
                        loadInfoArchivoControl();
                        Mensaje("Archivo cargado exitósamente.");
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
                Mensaje("Error al agregar el archivo. " + ex.Message);
            }
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                resetValuesCamposDetalleEvaluacion();
                resetValuesJustificacionPlanEvaluacion();
                trGridDetalleEvaluacion.Visible = true;
                trCamposDetalleEvaluacion.Visible = true;
                ImageButton13.Visible = true;
            }
        }

        protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesComentariosArchivos();
            trComentariosArchivos.Visible = true;
        }

        protected void ImageButton14_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesCamposDetalleEvaluacion();
            resetValuesJustificacionPlanEvaluacion();
            trGridDetalleEvaluacion.Visible = true;
            mtdStard();
        }

        protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["ControlCambios"] = false;
                if (Sanitizer.GetSafeHtmlFragment(TextBox14.Text.Trim()) == "" && Sanitizer.GetSafeHtmlFragment(TextBox15.Text.Trim()) == "" && Sanitizer.GetSafeHtmlFragment(TextBox21.Text.Trim()) == "")
                    Mensaje("Debe ingresar por lo menos un parámetro de consulta.");
                else
                {
                    inicializarValores();
                    resetValuesControl();
                    resetValuesComentariosArchivos();
                    resetValuesCamposDetalleEvaluacion();
                    resetValuesJustificacionPlanEvaluacion();
                    loadGridControles();
                    loadInfoControles();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void ImageButton15_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesConsulta();
            resetValuesControl();
            resetValuesComentariosArchivos();
            resetValuesCamposDetalleEvaluacion();
            resetValuesJustificacionPlanEvaluacion();
            loadGridControles();
        }

        protected void ImageButton16_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesJustificacionPlanEvaluacion();
        }

        protected void ImageButton17_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload2.HasFile)
                    {

                        mtdCargarPlanEvaluacion();
                        loadGridArchivoPlanEvaluacion();
                        loadInfoArchivoPlanEvaluacion();
                        Mensaje("Archivo cargado exitósamente.");

                    }
                    else
                        Mensaje("No hay archivos para cargar.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el archivo. " + ex.Message);
            }
        }
        #endregion Buttons

        #region Loads

        private void mtdLoadInfoControles(string IdUsuarioSession)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.LoadInfoControlesDefault(IdUsuarioSession);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridControles.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["DescripcionControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["ObjetivoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdPeriodicidad"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdTest"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdClaseControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdTipoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdResponsableExperiencia"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdDocumentacion"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdResponsabilidad"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdCalificacionControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdMitiga"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreHijo"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["ResponsableEjecucion"].ToString().Trim()
                                                            });
                }
                GridView1.DataSource = InfoGridControles;
                GridView1.DataBind();
            }
            else
                loadGridControles();
        }

        private void loadCodigoControl()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cControl.loadCodigoControl();

                //Ajuste RCC - 15/01/2014
                if (NuevoControl == 1 && dtInfo.Rows.Count > 0)
                {
                    TextBox1.Visible = false;
                    Label2.Visible = false;
                }

                if (dtInfo.Rows.Count > 0)
                    TextBox1.Text = "C" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox1.Text = "C1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código control. " + ex.Message);
            }
        }

        private void loadInfoPlanEvaluacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoPlanEvaluacion(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPlanEvaluacion.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlanEvaluacion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaProyectadaFin"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaRealCierre"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdTipoPruebaPlanEvaluacion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreTipoPruebaPlanEvaluacion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["DescripcionEvaluacion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Recursos"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Resultados"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdEstadoPlanEvaluacion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreEstadoPlanEvaluacion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreHijo"].ToString().Trim()
                                                                 });
                }
                GridView2.DataSource = InfoGridPlanEvaluacion;
                GridView2.DataBind();
            }
        }

        private void loadGridPlanEvaluacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlanEvaluacion", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaProyectadaFin", typeof(string));
            grid.Columns.Add("FechaRealCierre", typeof(string));
            grid.Columns.Add("IdTipoPruebaPlanEvaluacion", typeof(string));
            grid.Columns.Add("NombreTipoPruebaPlanEvaluacion", typeof(string));
            grid.Columns.Add("DescripcionEvaluacion", typeof(string));
            grid.Columns.Add("Recursos", typeof(string));
            grid.Columns.Add("Resultados", typeof(string));
            grid.Columns.Add("IdEstadoPlanEvaluacion", typeof(string));
            grid.Columns.Add("NombreEstadoPlanEvaluacion", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            InfoGridPlanEvaluacion = grid;
            GridView2.DataSource = InfoGridPlanEvaluacion;
            GridView2.DataBind();
        }

        private void loadInfoComentarioControl()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoComentarioControl(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioControl.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["ComentarioCorto"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                                                                     });
                }
                GridView4.DataSource = InfoGridComentarioControl;
                GridView4.DataBind();
            }
        }

        private void loadGridComentarioControl()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("ComentarioCorto", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            InfoGridComentarioControl = grid;
            GridView4.DataSource = InfoGridComentarioControl;
            GridView4.DataBind();
        }

        private void loadGridArchivoControl()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoControl = grid;
            GridView3.DataSource = InfoGridArchivoControl;
            GridView3.DataBind();
        }

        private void loadInfoArchivoControl()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoArchivoControl(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoControl.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                                 });
                }
                GridView3.DataSource = InfoGridArchivoControl;
                GridView3.DataBind();
            }
        }

        private void loadInfoPorcentajeCalificarControl()
        {
            InfoPorcentajeCalificarControl = cControl.loadInfoPorcentajeCalificarControl();
        }

        private void loadInfoCalificacionControl()
        {
            InfoCalificacionControl = cControl.loadInfoCalificacionControl();
        }

        private void loadInfoControles()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoControles(Sanitizer.GetSafeHtmlFragment(TextBox14.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox15.Text.Trim()), lblIdDependencia3.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridControles.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["DescripcionControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["ObjetivoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdPeriodicidad"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdTest"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdClaseControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdTipoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdResponsableExperiencia"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdDocumentacion"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdResponsabilidad"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdCalificacionControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdMitiga"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreHijo"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["ResponsableEjecucion"].ToString().Trim()
                                                            });
                }
                GridView1.DataSource = InfoGridControles;
                GridView1.DataBind();
            }
            else
            {
                loadGridControles();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        private void loadGridControles()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdControl", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));
            grid.Columns.Add("DescripcionControl", typeof(string));
            grid.Columns.Add("ObjetivoControl", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            grid.Columns.Add("IdPeriodicidad", typeof(string));
            grid.Columns.Add("IdTest", typeof(string));
            grid.Columns.Add("NombreTest", typeof(string));
            grid.Columns.Add("IdClaseControl", typeof(string));
            grid.Columns.Add("IdTipoControl", typeof(string));
            grid.Columns.Add("IdResponsableExperiencia", typeof(string));
            grid.Columns.Add("IdDocumentacion", typeof(string));
            grid.Columns.Add("IdResponsabilidad", typeof(string));
            grid.Columns.Add("IdCalificacionControl", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));
            grid.Columns.Add("IdMitiga", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            grid.Columns.Add("ResponsableEjecucion", typeof(string));
            InfoGridControles = grid;
            GridView1.DataSource = InfoGridControles;
            GridView1.DataBind();
        }

        private void loadGridComentarioPlanEvaluacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("ComentarioCorto", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            InfoGridComentarioPlanEvaluacion = grid;
            GridView5.DataSource = InfoGridComentarioPlanEvaluacion;
            GridView5.DataBind();
        }

        private void loadInfoComentarioPlanEvaluacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoComentarioPlanEvaluacion(InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioPlanEvaluacion.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
                                                                            dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                            dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                            dtInfo.Rows[rows]["ComentarioCorto"].ToString().Trim(),
                                                                            dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                                                                           });
                }
                GridView5.DataSource = InfoGridComentarioPlanEvaluacion;
                GridView5.DataBind();
            }
        }

        private void loadGridArchivoPlanEvaluacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoPlanEvaluacion = grid;
            GridView6.DataSource = InfoGridArchivoPlanEvaluacion;
            GridView6.DataBind();
        }

        private void loadInfoArchivoPlanEvaluacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoArchivoPlanAccion(InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoPlanEvaluacion.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                                         dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                         dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                         dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                                        });
                }
                GridView6.DataSource = InfoGridArchivoPlanEvaluacion;
                GridView6.DataBind();
            }
        }

        #endregion Loads

        #region Gridview

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridControles = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridControles) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    IBprocess.Visible = false;
                    Session["modificar"] = 1;
                    TextBox1.Visible = true;
                    Label2.Visible = true;
                    detalleControlSeleccionado();
                    loadGridArchivoControl();
                    loadInfoArchivoControl();
                    loadGridComentarioControl();
                    loadInfoComentarioControl();
                    loadGridPlanEvaluacion();
                    loadInfoPlanEvaluacion();
                    mtdShowCalificacion();
                    break;
                case "Borrar":
                    borrarControl();
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoControl = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdfControl();
                    break;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridComentarioControl = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verComentario();
                    break;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridControles = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridControles;
            GridView1.DataSource = InfoGridControles;
            GridView1.DataBind();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridPlanEvaluacion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    resetValuesCamposDetalleEvaluacion();
                    resetValuesJustificacionPlanEvaluacion();
                    detallePlanEvaluacionSeleccionado();
                    loadGridComentarioPlanEvaluacion();
                    loadInfoComentarioPlanEvaluacion();
                    loadGridArchivoPlanEvaluacion();
                    loadInfoArchivoPlanEvaluacion();
                    break;
            }
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridComentarioPlanEvaluacion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verComentarioPlanEvaluacion();
                    break;
            }
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoPlanEvaluacion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdfPlanEvaluacion();
                    break;
            }
        }
        #endregion Gridview

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        #region Metodos
        private void inicializarValores()
        {
            PagIndexInfoGridControles = 0;
        }

        private void armarIntervalos()
        {
            DataTable dtInfo = new DataTable(), dtIntervalos = new DataTable();
            double maximo = 0, minimo = 0, intervalo = 0, delta = 0;

            try
            {
                dtInfo = cControl.valorMaxMinIntervalo();

                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        minimo = Convert.ToDouble(dtInfo.Rows[0]["Minimo"].ToString().Trim());
                        intervalo = (Convert.ToDouble(dtInfo.Rows[0]["Maximo"].ToString().Trim())) - (Convert.ToDouble(dtInfo.Rows[0]["Minimo"].ToString().Trim()));
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
                        Mensaje("No hay información en las tablas maestras de parametrización. ");
                }
                else
                    Mensaje("No hay información en las tablas maestras de parametrización. ");
            }
            catch (Exception ex)
            {
                Mensaje("Error al armar intervalos. " + ex.Message);
            }
        }

        private void GetLastControl()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cControl.GetLastControl();

                if (dtInfo.Rows.Count > 0)
                    LastControl = "C" + dtInfo.Rows[0]["LastControl"].ToString().Trim();
                else
                    LastControl = "C1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código control. " + ex.Message);
            }
        }

        private void agregarComentarioControl()
        {
            cControl.agregarComentarioControl(Sanitizer.GetSafeHtmlFragment(TextBox12.Text.ToString().Trim()), InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim());
        }

        private void detalleControlSeleccionado()
        {
            resetValuesControl();
            TextBox1.Text = InfoGridControles.Rows[RowGridControles]["CodigoControl"].ToString().Trim();
            TextBox2.Text = InfoGridControles.Rows[RowGridControles]["NombreControl"].ToString().Trim();
            TextBox3.Text = InfoGridControles.Rows[RowGridControles]["DescripcionControl"].ToString().Trim();
            TextBox4.Text = InfoGridControles.Rows[RowGridControles]["ObjetivoControl"].ToString().Trim();
            TextBox34.Text = InfoGridControles.Rows[RowGridControles]["NombreHijo"].ToString().Trim();
            lblIdDependencia1.Text = InfoGridControles.Rows[RowGridControles]["Responsable"].ToString().Trim();

            #region DropDownLists
            for (int i = 0; i < DropDownList6.Items.Count; i++)
            {
                DropDownList6.SelectedIndex = i;
                if (DropDownList6.SelectedValue.ToString().Trim() == InfoGridControles.Rows[RowGridControles]["IdPeriodicidad"].ToString().Trim())
                    break;
                else
                    DropDownList6.SelectedIndex = 0;
            }
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedValue.ToString().Trim() == InfoGridControles.Rows[RowGridControles]["IdTest"].ToString().Trim())
                    break;
                else
                    DropDownList2.SelectedIndex = 0;
            }
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedValue.ToString().Trim() == InfoGridControles.Rows[RowGridControles]["IdMitiga"].ToString().Trim())
                    break;
                else
                    DropDownList1.SelectedIndex = 0;
            }
            for (int i = 0; i < DropDownList9.Items.Count; i++)
            {
                DropDownList9.SelectedIndex = i;
                if (DropDownList9.SelectedValue.ToString().Trim() == InfoGridControles.Rows[RowGridControles]["IdClaseControl"].ToString().Trim())
                    break;
                else
                    DropDownList9.SelectedIndex = 0;
            }
            for (int i = 0; i < DropDownList10.Items.Count; i++)
            {
                DropDownList10.SelectedIndex = i;
                if (DropDownList10.SelectedValue.ToString().Trim() == InfoGridControles.Rows[RowGridControles]["IdTipoControl"].ToString().Trim())
                    break;
                else
                    DropDownList10.SelectedIndex = 0;
            }
            for (int i = 0; i < DropDownList8.Items.Count; i++)
            {
                DropDownList8.SelectedIndex = i;
                if (DropDownList8.SelectedValue.ToString().Trim() == InfoGridControles.Rows[RowGridControles]["IdResponsableExperiencia"].ToString().Trim())
                    break;
                else
                    DropDownList8.SelectedIndex = 0;
            }
            for (int i = 0; i < DropDownList11.Items.Count; i++)
            {
                DropDownList11.SelectedIndex = i;
                if (DropDownList11.SelectedValue.ToString().Trim() == InfoGridControles.Rows[RowGridControles]["IdDocumentacion"].ToString().Trim())
                    break;
                else
                    DropDownList11.SelectedIndex = 0;
            }
            for (int i = 0; i < DropDownList12.Items.Count; i++)
            {
                DropDownList12.SelectedIndex = i;
                if (DropDownList12.SelectedValue.ToString().Trim() == InfoGridControles.Rows[RowGridControles]["IdResponsabilidad"].ToString().Trim())
                    break;
                else
                    DropDownList12.SelectedIndex = 0;
            }
            #endregion DropDownLists

            IdCalificacionControl = InfoGridControles.Rows[RowGridControles]["IdCalificacionControl"].ToString().Trim();
            string strErrMsg = string.Empty;
            int IdControl = 0;
            List<clsDTOControlxVariable> lstControlxVariable = new List<clsDTOControlxVariable>();
            clsBLLControlxVariable cControlxVariable = new clsBLLControlxVariable();
            IdControl = cControlxVariable.mtdGetIdControl(ref strErrMsg, InfoGridControles.Rows[RowGridControles]["CodigoControl"].ToString().Trim());
            lstControlxVariable = cControlxVariable.mtdConsultarVariablexContol(ref lstControlxVariable, ref strErrMsg, IdControl);
            if (lstControlxVariable == null)
                mostrarCalificacionControl();
            else
                trResultado.Visible = false;

            tbCampos.Visible = true;
            ImageButton5.Visible = true;
            resetValuesComentariosArchivos();
            trComentariosArchivos.Visible = true;
            resetValuesCamposDetalleEvaluacion();
            resetValuesJustificacionPlanEvaluacion();
            trGridDetalleEvaluacion.Visible = true;
            LblResponsableEjecucion.Text = InfoGridControles.Rows[RowGridControles]["ResponsableEjecucion"].ToString().Trim();
            TxbResponsableEjecución.Text = mtdBuscarNombresRespEjecucion(InfoGridControles.Rows[RowGridControles]["ResponsableEjecucion"].ToString().Trim());

        }

        private string mtdBuscarNombresRespEjecucion(string IdResponsableEjecucion)
        {
            try
            {
                string NombresResponsablesEjecucion = string.Empty;
                string[] srtSeparator = new string[] { "|" };
                string[] arrNombres = IdResponsableEjecucion.Split(srtSeparator, StringSplitOptions.None);
                string IdNombre = string.Empty;
                int i = arrNombres.Length;

                for (int j = 0; j < i; j++)
                {
                    int a = -1;
                    //Heber Jessid Correal 05/04/2017 Se valida que tenga 3 o mas caracteres para poder remover 3 caracteres
                    if (arrNombres[j].Length >= 3)
                    {
                        //Heber Jessid Correal 05/04/2017 Se controla que el valor enviado al metodo sea número.
                        if (int.TryParse(arrNombres[j].Remove(0, 3), out a))
                        {
                            if (arrNombres[j].Contains("JO"))
                                NombresResponsablesEjecucion += cControl.NombreJerarquia(arrNombres[j].Remove(0, 3));
                            else if (arrNombres[j].Contains("GT"))
                                NombresResponsablesEjecucion += cControl.NombreGrupoTrabajo(arrNombres[j].Remove(0, 3));
                        }
                    }
                }
                //Heber Jessid Correal 09/04/2017 Se agrega responsable de calificacion en responsable ejecucion. Claxon 132167 
                if (!string.IsNullOrEmpty(InfoGridControles.Rows[RowGridControles]["NombreHijo"].ToString().Trim()))
                    NombresResponsablesEjecucion = string.Format(NombresResponsablesEjecucion + "{0}|", InfoGridControles.Rows[RowGridControles]["NombreHijo"].ToString().Trim());
                return NombresResponsablesEjecucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string mtdBuscarCorreosRespEjecucion(string IdResponsableEjecucion)
        {
            string CorreosResponsablesEjecucion = string.Empty;
            string[] srtSeparator = new string[] { "|" };
            string[] arrNombres = IdResponsableEjecucion.Split(srtSeparator, StringSplitOptions.None);
            string IdNombre = string.Empty;
            int i = arrNombres.Length;

            for (int j = 0; j < i; j++)
            {
                if (arrNombres[j].Contains("JO"))
                    CorreosResponsablesEjecucion += cControl.CorreoJerarquia(arrNombres[j].Remove(0, 3));
                else if (arrNombres[j].Contains("GT"))
                    CorreosResponsablesEjecucion += cControl.CorreoGrupoTrabajo(arrNombres[j].Remove(0, 3));
            }
            return CorreosResponsablesEjecucion;
        }

        private void borrarControl()
        {
            cControl.borrarControl(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim());
            resetValuesControl();
            loadGridControles();
        }

        private void modificarControl()
        {
            try
            {
                cControl.modificarControl(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), lblIdDependencia1.Text.Trim(),
                DropDownList6.SelectedValue.ToString().Trim(), DropDownList2.SelectedValue.ToString().Trim(),
                InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim(),
                DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox12.Text.ToString().Trim()), LblResponsableEjecucion.Text.Trim());
                agregarComentarioControl();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void agregarControl()
        {
            string strError = string.Empty;

            cControl.agregarControl(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), 
                Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()),
                lblIdDependencia1.Text.Trim(), DropDownList6.SelectedValue.ToString().Trim(), DropDownList2.SelectedValue.ToString().Trim(),
                DropDownList9.SelectedValue.ToString().Trim(), DropDownList10.SelectedValue.ToString().Trim(), DropDownList8.SelectedValue.ToString().Trim(),
                DropDownList11.SelectedValue.ToString().Trim(), DropDownList12.SelectedValue.ToString().Trim(), IdCalificacionControl.ToString().Trim(),
                DropDownList1.SelectedValue.ToString().Trim(), ref strError, Session["idUsuario"].ToString(), LblResponsableEjecucion.Text.Trim());
        }

        protected void calcularEficacia(object sender, EventArgs e)
        {
            calcularCalificacionControl();
        }

        private void calcularCalificacionControl()
        {
            double calificacionControl = 0;
            double claseControl = 0;
            if (DropDownList9.SelectedValue.ToString().Trim() != "0")
                claseControl = cControl.valorClaseControl(DropDownList9.SelectedValue.ToString().Trim());

            double tipoControl = 0;
            if (DropDownList10.SelectedValue.ToString().Trim() != "0")
                tipoControl = cControl.valorTipoControl(DropDownList10.SelectedValue.ToString().Trim());

            double responsableExperiencia = 0;
            if (DropDownList8.SelectedValue.ToString().Trim() != "0")
                responsableExperiencia = cControl.valorResponsableExperiencia(DropDownList8.SelectedValue.ToString().Trim());

            double documentacion = 0;
            if (DropDownList11.SelectedValue.ToString().Trim() != "0")
                documentacion = cControl.valorDocumentacion(DropDownList11.SelectedValue.ToString().Trim());

            double responsabilidad = 0;
            if (DropDownList12.SelectedValue.ToString().Trim() != "0")
                responsabilidad = cControl.valorResponsabilidad(DropDownList12.SelectedValue.ToString().Trim());

            calificacionControl = claseControl * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[0]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl += tipoControl * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[1]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl += responsableExperiencia * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[2]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl += documentacion * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[3]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl += responsabilidad * Convert.ToDouble(InfoPorcentajeCalificarControl.Rows[4]["ValorPorcentajeCalificarControl"].ToString().Trim());
            calificacionControl = (calificacionControl / 100);
            for (int i = 0; i < InfoIntervalos.Rows.Count; i++)
            {
                if (calificacionControl > Convert.ToDouble(InfoIntervalos.Rows[i]["limiteInferior"].ToString().Trim()) && calificacionControl <= Convert.ToDouble(InfoIntervalos.Rows[i]["limiteSuperior"].ToString().Trim()))
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
                    Label14.Text = InfoCalificacionControl.Rows[i]["NombreEscala"].ToString().Trim();
                    Panel1.BackColor = System.Drawing.Color.FromName(InfoCalificacionControl.Rows[i]["Color"].ToString().Trim());
                    trResultado.Visible = true;
                    break;
                }
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void verComentario()
        {
            TextBox12.Text = InfoGridComentarioControl.Rows[RowGridComentarioControl]["Comentario"].ToString().Trim();
            TextBox12.ReadOnly = true;
            ImageButton9.Visible = true;
        }

        private void mtdActualizarRiesgosControles()
        {
            cRiesgo cRisk = new cRiesgo();

            cRisk.mtdActualizarRiesgosControles(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim());
        }

        #region Resets
        private void resetValuesControl()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox34.Text = "";
            lblIdDependencia1.Text = "";
            lblIdDependencia1.Visible = false;
            DropDownList6.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            DropDownList9.SelectedIndex = 0;
            DropDownList10.SelectedIndex = 0;
            DropDownList8.SelectedIndex = 0;
            DropDownList11.SelectedIndex = 0;
            DropDownList12.SelectedIndex = 0;
            Label14.Text = "";
            Panel1.BackColor = System.Drawing.Color.FromName("Transparent");
            ImageButton4.Visible = false;
            ImageButton5.Visible = false;
            tbCampos.Visible = false;
            IFcategorias.Visible = false;
            trClaseControl.Visible = false;
            trResponsableExp.Visible = false;
            trResponsabilidad.Visible = false;
            DropDownList9.SelectedIndex = 0;
            DropDownList10.SelectedIndex = 0;
            DropDownList8.SelectedIndex = 0;
            DropDownList11.SelectedIndex = 0;
            DropDownList12.SelectedIndex = 0;
            TxbResponsableEjecución.Text = string.Empty;
            LblResponsableEjecucion.Text = string.Empty;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        private void resetValuesComentariosArchivos()
        {
            trComentariosArchivos.Visible = false;
            TextBox12.Text = "";
            TextBox12.ReadOnly = false;
            ImageButton9.Visible = false;
        }

        private void resetValuesConsulta()
        {
            TextBox14.Text = "";
            TextBox15.Text = "";
            TextBox21.Text = "";
            lblIdDependencia3.Text = "";
        }

        private void resetValuesCamposDetalleEvaluacion()
        {
            TextBox33.Text = "";
            lblIdDependencia2.Text = "";
            lblIdDependencia2.Visible = false;
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox10.Text = "";
            DropDownList5.SelectedIndex = 0;
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            ImageButton13.Visible = false;
            ImageButton2.Visible = false;
            trCamposDetalleEvaluacion.Visible = false;
            trFechaRealCierre.Visible = false;
            trResultados.Visible = false;
            DropDownList7.SelectedIndex = 0;
            trGridDetalleEvaluacion.Visible = false;
            TextBox16.Text = "";
            ImageButton16.Visible = false;
            trComArcPlanEva.Visible = false;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }
        #endregion Resets

        #region Plan Evaluacion
        private void detallePlanEvaluacionSeleccionado()
        {
            ImageButton2.Visible = true;
            trCamposDetalleEvaluacion.Visible = true;
            trFechaRealCierre.Visible = true;
            trResultados.Visible = true;
            trGridDetalleEvaluacion.Visible = true;
            TextBox33.Text = InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["NombreHijo"].ToString().Trim();
            lblIdDependencia2.Text = InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["Responsable"].ToString().Trim();            //
            TextBox5.Text = InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["FechaInicio"].ToString().Trim();
            TextBox6.Text = InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["FechaProyectadaFin"].ToString().Trim();
            TextBox10.Text = InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["FechaRealCierre"].ToString().Trim();
            for (int i = 0; i < DropDownList5.Items.Count; i++)
            {
                DropDownList5.SelectedIndex = i;
                if (DropDownList5.SelectedValue.ToString().Trim() == InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdTipoPruebaPlanEvaluacion"].ToString().Trim())
                    break;
            }
            TextBox7.Text = InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["DescripcionEvaluacion"].ToString().Trim();
            TextBox8.Text = InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["Recursos"].ToString().Trim();
            TextBox9.Text = InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["Resultados"].ToString().Trim();
            for (int i = 0; i < DropDownList7.Items.Count; i++)
            {
                DropDownList7.SelectedIndex = i;
                if (DropDownList7.SelectedValue.ToString().Trim() == InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdEstadoPlanEvaluacion"].ToString().Trim())
                    break;
            }
            trComArcPlanEva.Visible = true;
        }

        private void resetValuesJustificacionPlanEvaluacion()
        {
            TextBox16.ReadOnly = false;
            TextBox16.Text = "";
            ImageButton16.Visible = false;
        }

        private void verComentarioPlanEvaluacion()
        {
            TextBox16.Text = InfoGridComentarioPlanEvaluacion.Rows[RowGridComentarioPlanEvaluacion]["Comentario"].ToString().Trim();
            TextBox16.ReadOnly = true;
            ImageButton16.Visible = true;
        }

        private void agregarComentarioPlanEvaluacion()
        {
            cControl.agregarComentarioPlanEvaluacion(TextBox16.Text.Trim(), InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim());
        }
        #endregion Plan Evaluacion

        #region Notificacion
        private void mtdGenerarNotificacion()
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "MODIFICACION  DE CONTROL" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(TextBox1.Text) + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text) + "<br>";
                TextoAdicional = TextoAdicional + " Descripcíon Control : " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text) + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : " + Sanitizer.GetSafeHtmlFragment(TextBox12.Text.Trim()) + "<br>";
                TextoAdicional = TextoAdicional + " Fecha Modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario Modificación : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Modificación : " + Session["nombreUsuario"].ToString() + "<br>";

                boolEnviarNotificacion(18, 1, Convert.ToInt16(lblIdDependencia1.Text.Trim()), System.DateTime.Now.ToString().Trim(), TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }

        private void mtdGenerarNotificacionEjecución()
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "MODIFICACION  DE CONTROL" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + TextBox1.Text + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + TextBox2.Text + "<br>";
                TextoAdicional = TextoAdicional + " Descripcíon Control : " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text) + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : " + TextBox12.Text.Trim() + "<br>";
                TextoAdicional = TextoAdicional + " Fecha Modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario Modificación : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Modificación : " + Session["nombreUsuario"].ToString() + "<br>";
                //boolEnviarNotificacion(18, 1, Convert.ToInt16(lblIdDependencia1.Text.Trim()), System.DateTime.Now.ToString(), TextoAdicional);
                boolEnviarNotificacionEjecucion(18, 1, LblResponsableEjecucion.Text.Trim(), System.DateTime.Now.ToString(), TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            try
            {
                #region informacion basica
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString();
                        Otros = row["Otros"].ToString();
                        Asunto = row["Asunto"].ToString() + " - Responsable Calificación";
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region Consulta el correo del Destinatario
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
                #endregion Consulta el correo del Destinatario

                #region Consulta el correo del Jefe Inmediato
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
                #endregion Consulta el correo del Jefe Inmediato

                #region Consulta el correo del Jefe Mediato
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
                #endregion Consulta el correo del Jefe Mediato

                //Insertar el Registro en la tabla de Correos Enviados
                #region Insertar el Registro en la tabla de Correos Enviados
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
                #endregion Insertar el Registro en la tabla de Correos Enviados
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
                #region
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
                    #endregion Envio Correo
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    #region Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString();
                    SqlDataSource200.Update();
                    #endregion Actualiza el Estado del Correo Enviado
                }
            }

            return (err);
        }

        private Boolean boolEnviarNotificacionEjecucion(int idEvento, int idRegistro, string idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            bool err = false;
            string Destinatario = "", Cuerpo = "", selectCommand = "", Otros = "", Copia = "", Asunto = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            try
            {
                #region informacion basica
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString();
                        Otros = row["Otros"].ToString();
                        Asunto = row["Asunto"].ToString() + " - Responsable Ejecución";
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString();
                        //NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        //AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        //AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        //RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                        Destinatario = mtdBuscarCorreosRespEjecucion(idNodoJerarquia);
                    }
                }
                #endregion

                //Insertar el Registro en la tabla de Correos Enviados
                #region Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario;
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = "";
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = "";
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString();
                SqlDataSource200.Insert();
                #endregion Insertar el Registro en la tabla de Correos Enviados
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                try
                {
                    #region Envio Correo
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
                    #endregion Envio Correo
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
                    #region Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                    #endregion Actualiza el Estado del Correo Enviado
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
                #region informacion basica
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = "CIERRE - " + row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region Consulta el correo del Destinatario
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
                #endregion Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional

                #region Consulta el correo del Jefe Inmediato
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
                #endregion Consulta el correo del Jefe Inmediato

                //Consulta el correo del Jefe Mediato
                #region Consulta el correo del Jefe Mediato
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
                #endregion Consulta el correo del Jefe Mediato

                //Insertar el Registro en la tabla de Correos Enviados
                #region Insertar el Registro en la tabla de Correos Enviados
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
                #endregion Insertar el Registro en la tabla de Correos Enviados
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
                #region
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
                    #region Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                    #endregion Actualiza el Estado del Correo Enviado
                }
            }

            return (err);
        }
        #endregion Notificacion

        #region Plan Accion

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    actualizarPlanEvaluacion();
                    agregarComentarioPlanEvaluacion();
                    if (DropDownList7.SelectedItem.ToString().Trim() == "Cerrado")
                        boolEnviarNotificacionCierre(8, Convert.ToInt16(InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim()), Convert.ToInt16(lblIdDependencia2.Text.Trim()), "", "Fecha Inicio: " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + "<br />Fecha Proyectada: " + Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + "<br />Descripcion Evaluacion: " + Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()) + "<br />Número del control al que está asociado el Plan de Evaluación: " + InfoGridControles.Rows[RowGridControles]["CodigoControl"].ToString().Trim() + "<br />Fecha Real Cierre: " + Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()) + "<br /><br />");

                    resetValuesCamposDetalleEvaluacion();
                    resetValuesJustificacionPlanEvaluacion();
                    trGridDetalleEvaluacion.Visible = true;
                    loadGridPlanEvaluacion();
                    loadInfoPlanEvaluacion();

                    if (TreeView1.SelectedNode != null)
                        TreeView1.SelectedNode.Selected = false;

                    if (TreeView2.SelectedNode != null)
                        TreeView2.SelectedNode.Selected = false;

                    if (TreeView3.SelectedNode != null)
                        TreeView3.SelectedNode.Selected = false;

                    Mensaje("Plan de evaluación modificado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el plan evaluación. " + ex.Message);
            }
        }

        protected void ImageButton13_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (Convert.ToInt64(Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim().Replace("-", ""))) <= Convert.ToInt64(DateTime.Now.Date.ToString("yyyy-MM-dd").Replace("-", "")))
                        Mensaje("Debe ingresar una fecha proyectada fin valida.");
                    else
                    {
                        int IdRegistro = mtdAgregarPlanEvaluacion();
                        boolEnviarNotificacion(8, IdRegistro, Convert.ToInt16(lblIdDependencia2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + " 12:00:00:000",
                            "Fecha Inicio: " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) +
                            "<br />Fecha Proyectada: " + Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) +
                            "<br />Descripcion Evaluacion: " + Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()) +
                            "<br />Número del control al que está asociado el Plan de Evaluación: " + InfoGridControles.Rows[RowGridControles]["CodigoControl"].ToString().Trim() + "<br /><br />" +
                            "<br />Para mayor información del Plan de Evaluación ingresar a: " + ConfigurationManager.AppSettings.Get("URL").ToString() + "<br />" +
                            "<br />En el Modulo Riesgo/Controles Pestaña Plan de Evaluación <br />");
                        resetValuesCamposDetalleEvaluacion();
                        resetValuesJustificacionPlanEvaluacion();
                        trGridDetalleEvaluacion.Visible = true;
                        loadGridPlanEvaluacion();
                        loadInfoPlanEvaluacion();
                        Mensaje("Plan de evaluación agregado con éxito.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el plan evaluación. " + ex.Message);
            }
        }
        
        private void agregarPlanEvaluacion()
        {
            cControl.agregarPlanEvaluacion(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim(), lblIdDependencia2.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + " 12:00:00:000", DropDownList5.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox7.Text), Sanitizer.GetSafeHtmlFragment(TextBox8.Text), DropDownList7.SelectedValue.ToString().Trim());
        }

        private int mtdAgregarPlanEvaluacion()
        {
            int intIdRegistro = 0;
            DataTable dtInfo = new DataTable();

            dtInfo = cControl.mtdAgregarPlanEvaluacion(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim(), lblIdDependencia2.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + " 12:00:00:000", DropDownList5.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox7.Text), Sanitizer.GetSafeHtmlFragment(TextBox8.Text), DropDownList7.SelectedValue.ToString().Trim());

            if (dtInfo != null)
                if (dtInfo.Rows.Count > 0)
                    intIdRegistro = Convert.ToInt32(dtInfo.Rows[0][0].ToString());

            return intIdRegistro;
        }

        private void actualizarPlanEvaluacion()
        {
            cControl.actualizarPlanEvaluacion(InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim(), lblIdDependencia2.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()) + " 12:00:00:000", DropDownList5.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox8.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox9.Text.Trim()), DropDownList7.SelectedValue.ToString().Trim());
        }

        #endregion Plan Accion

        #region PDFs

        #region [VIEJO] PDF Control
        private void loadFile()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cControl.loadCodigoArchivoControl();
            if (dtInfo.Rows.Count > 0)
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim() + "-" + FileUpload1.FileName.ToString().Trim();
            else
                nameFile = "1-" + InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim() + "-" + FileUpload1.FileName.ToString().Trim();

            FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFsControl/") + nameFile);
            cControl.agregarArchivoControl(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim(), nameFile);
        }

        private void descargarArchivo()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFsControl/" + InfoGridArchivoControl.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }
        #endregion PDF Control

        #region [VIEJO] PDF Plan Evaluacion
        private void loadFilePlanEvaluacion()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cControl.loadCodigoArchivoControl();
            if (dtInfo.Rows.Count > 0)
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim() + "-" + FileUpload2.FileName.ToString().Trim();
            else
                nameFile = "1-" + InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim() + "-" + FileUpload2.FileName.ToString().Trim();
            FileUpload2.SaveAs(Server.MapPath("~/Archivos/PDFsPlanEvaluacion/") + nameFile);
            cControl.agregarArchivoPlanEvaluacion(InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim(), nameFile);
        }

        private void descargarArchivoPlanEvaluacion()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanEvaluacion/" + InfoGridArchivoPlanEvaluacion.Rows[RowGridArchivoPlanEvaluacion]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }
        #endregion PDF Plan Evaluacion

        #region PDF Control
        private void mtdCargarPdfControl()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cControl.loadCodigoArchivoControl();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() +
                    "-" + InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim() +
                    "-" + FileUpload1.FileName.ToString().Trim();
            else
                strNombreArchivo = "1-" + InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim() +
                    "-" + FileUpload1.FileName.ToString().Trim();
            #endregion Nombre Archivo

            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);

            cControl.mtdAgregarPdf("1", InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfControl()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoControl.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cControl.mtdDescargarPdf(strNombreArchivo);
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
        #endregion PDF Control

        #region PDF Plan Evaluacion
        private void mtdCargarPlanEvaluacion()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cControl.loadCodigoArchivoControl();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() +
                    "-" + InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim() +
                    "-" + FileUpload2.FileName.ToString().Trim();
            else
                strNombreArchivo = "1-" + InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim() +
                    "-" + FileUpload2.FileName.ToString().Trim();
            #endregion Nombre Archivo

            Stream fs = FileUpload2.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);

            cControl.mtdAgregarPdf("5", InfoGridPlanEvaluacion.Rows[RowGridPlanEvaluacion]["IdPlanEvaluacion"].ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfPlanEvaluacion()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoPlanEvaluacion.Rows[RowGridArchivoPlanEvaluacion]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cControl.mtdDescargarPdf(strNombreArchivo);
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
        #endregion PDF Plan Evaluacion

        #endregion PDFs

        void mtdAgregarControl()
        {
            agregarControl();

            GetLastControl();
            //Notificacion de creacion de Control
            if (lblIdDependencia1.Text.Trim() != "")
            {
                boolEnviarNotificacion(7, Convert.ToInt16("0"), Convert.ToInt16(lblIdDependencia1.Text.Trim()), "",
                    "Ha sido asignado como responsable de calificación de un control, código del control: " + LastControl + ", en la aplicación de Sherlock para la Gestión de Riesgos y Control Interno.<br /><br />");
            }
            if (LblResponsableEjecucion.Text.Trim() != "")
            {
                boolEnviarNotificacionEjecucion(7, Convert.ToInt16("0"), LblResponsableEjecucion.Text.Trim(), "",
                    "Ha sido asignado como responsable de ejecución de un control, código del control: " + LastControl + ", en la aplicación de Sherlock para la Gestión de Riesgos y Control Interno.<br /><br />");
            }

        }
        #region Calificacion Control

        public bool mtdLoadCalificacionControl(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
            lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);
            List<clsDTOCategoriasVariableControl> lstCategoria = new List<clsDTOCategoriasVariableControl>();
            clsDALCategoriaVariableControl cCategoria = new clsDALCategoriaVariableControl();
            /*clsBLLCategoriaVariableControl cCategoria = new clsBLLCategoriaVariableControl();
            lstCategoria = cCategoria.mtdConsultarCategoriaActiva(ref lstCategoria, ref strErrMsg);*/
            DataTable dtInfo = new DataTable();

            if (lstVariables != null)
            {
                //string tablestring = "";
                //dt is datatable object which holds DB results.
                //tablestring = tablestring + "<table width='100 % ' border='1' cellspacing='0' cellpadding='2' bordercolor='White'><tr align='left'>";
                int i = 0;
                System.Web.UI.HtmlControls.HtmlTableRow tRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                //tRow.BgColor = "#BBBBBB";
                foreach (clsDTOVariableCalificacionControl objVariable in lstVariables)
                {
                    bool flag = cCategoria.mtdConsultarCategoriaActivas(ref dtInfo, ref strErrMsg, objVariable.intIdCalificacionControl);
                    DropDownList ddlCategorias = new DropDownList();
                    ddlCategorias.ID = "ddl" + i;
                    ddlCategorias.Width = 300;
                    ddlCategorias.Items.Insert(0, new ListItem("---", "---"));
                    ddlCategorias.CausesValidation = true;
                    /*RequiredFieldValidator rfvCategoria = new RequiredFieldValidator();
                    rfvCategoria.ID= "rfv"+ objVariable.strDescripcionVariable.ToString().Trim();
                    rfvCategoria.ValidationGroup = "validateControl";
                    rfvCategoria.ToolTip = "Debe seleccionar una Categoría";
                    rfvCategoria.Text = "*";
                    rfvCategoria.ForeColor = System.Drawing.Color.Red;
                    rfvCategoria.InitialValue = "0";*/
                    for (int iteracion = 0; iteracion < dtInfo.Rows.Count; iteracion++)
                    {
                        ddlCategorias.Items.Insert(iteracion + 1, new ListItem(dtInfo.Rows[iteracion]["DescripcionCategoria"].ToString().Trim(), dtInfo.Rows[iteracion]["IdCategoriaVariableControl"].ToString()));
                    }
                    Label lblVariable = new Label();
                    lblVariable.ID = "lbl" + objVariable.strDescripcionVariable.ToString().Trim();
                    lblVariable.Text = objVariable.strDescripcionVariable.ToString().Trim();
                    if (i == 0)
                    {
                        //tablestring = tablestring + "<td bgcolor='#BBBBBB'>" + objVariable.strDescripcionVariable.ToString().Trim() + "</td><td><asp:DropDownList runat='server' ID='ddl"+ objVariable.strDescripcionVariable.ToString().Trim() + "' DataSource='" + lstCategoria + "'></asp:DropDownList></td>";

                        System.Web.UI.HtmlControls.HtmlTableCell tCell = new System.Web.UI.HtmlControls.HtmlTableCell();
                        tCell.Controls.Add(lblVariable);
                        tCell.BgColor = "#BBBBBB";
                        tRow.Cells.Add(tCell);
                        tCell = new System.Web.UI.HtmlControls.HtmlTableCell();
                        tCell.Controls.Add(ddlCategorias);
                        //tCell.Controls.Add(rfvCategoria);
                        tRow.Cells.Add(tCell);
                    }
                    else
                    {
                        if (i % 2 != 0)
                        {
                            //tablestring = tablestring + "<td bgcolor='#BBBBBB'>" + objVariable.strDescripcionVariable.ToString().Trim() + "</td><td>" + ddlCategorias + "</td></tr><tr>";
                            System.Web.UI.HtmlControls.HtmlTableCell tCell = new System.Web.UI.HtmlControls.HtmlTableCell();
                            tCell.Controls.Add(lblVariable);
                            tCell.BgColor = "#BBBBBB";
                            tRow.Cells.Add(tCell);
                            tCell = new System.Web.UI.HtmlControls.HtmlTableCell();
                            tCell.Controls.Add(ddlCategorias);
                            //tCell.Controls.Add(rfvCategoria);
                            tRow.Cells.Add(tCell);
                            tbVariableCategoriaControl.Rows.Add(tRow);
                            tRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                            //tRow.BgColor = "#BBBBBB";
                        }
                        else
                        {
                            if (i == (lstVariables.Count - 1))
                            //tablestring = tablestring + "<td bgcolor='#BBBBBB'>" + objVariable.strDescripcionVariable.ToString().Trim() + "</td><td>" + ddlCategorias + "</td></tr>";
                            {
                                System.Web.UI.HtmlControls.HtmlTableCell tCell = new System.Web.UI.HtmlControls.HtmlTableCell();
                                tCell.Controls.Add(lblVariable);
                                tCell.BgColor = "#BBBBBB";
                                tRow.Cells.Add(tCell);
                                tCell = new System.Web.UI.HtmlControls.HtmlTableCell();
                                tCell.Controls.Add(ddlCategorias);
                                //tCell.Controls.Add(rfvCategoria);
                                tRow.Cells.Add(tCell);
                                tbVariableCategoriaControl.Rows.Add(tRow);
                            }
                            else
                            {
                                System.Web.UI.HtmlControls.HtmlTableCell tCell = new System.Web.UI.HtmlControls.HtmlTableCell();
                                tCell.Controls.Add(lblVariable);
                                tCell.BgColor = "#BBBBBB";
                                tRow.Cells.Add(tCell);
                                tCell = new System.Web.UI.HtmlControls.HtmlTableCell();
                                tCell.Controls.Add(ddlCategorias);
                                //tCell.Controls.Add(rfvCategoria);
                                tRow.Cells.Add(tCell);
                            }

                            //tablestring = tablestring + "<td bgcolor='#BBBBBB'>" + objVariable.strDescripcionVariable.ToString().Trim() + "</td><td>" + ddlCategorias + "</td>";
                        }
                    }
                    i++;
                }
                //tablestring = tablestring + "</table>";

                //divTable.InnerHtml = tablestring;
                booResult = true;
            }
            return booResult;
        }
        #endregion Calificacion Control
        #endregion Metodos

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            LblResponsableEjecucion.Text = string.Empty;
            TxbResponsableEjecución.Text = string.Empty;
        }

        protected void IBprocess_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            try
            {

                string NombreControl = Sanitizer.GetSafeHtmlFragment(TextBox2.Text);
                string DescripcionControl = Sanitizer.GetSafeHtmlFragment(TextBox3.Text);
                string ObjetivoControl = Sanitizer.GetSafeHtmlFragment(TextBox4.Text);
                string ResponsableEjecucion = Sanitizer.GetSafeHtmlFragment(TxbResponsableEjecución.Text);
                string IdResponsablesEjecucion = Sanitizer.GetSafeHtmlFragment(LblResponsableEjecucion.Text);
                string IdResponsableCalificacion = Sanitizer.GetSafeHtmlFragment(lblIdDependencia1.Text);
                string Periodicidad = DropDownList6.SelectedValue;
                string test = DropDownList2.SelectedValue;
                string Reduce = DropDownList1.Text;
                HtmlGenericControl Iframe = new HtmlGenericControl("Iframe");
                Iframe.Attributes["width"] = "100%";
                Iframe.Attributes["src"] = "../../../UserControls/Riesgos/Iframe/IFcategorias.aspx?NombreControl=" + NombreControl + "&DescripcionControl=" + DescripcionControl +
                    "&ObjetivoControl=" + ObjetivoControl + "&ResponsableEjecucion=" + ResponsableEjecucion + "&IdResponsablesEjecucion=" + IdResponsablesEjecucion +
                    "&IdResponsableCalificacion=" + IdResponsableCalificacion + "&Periodicidad=" + Periodicidad + "&test=" + test + "&Reduce=" + Reduce;
                dvIframe.Controls.Add(Iframe);
                trTituloCobertura.Visible = true;
                trCobertura.Visible = true;
                trIframe.Visible = true;
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error: " + ex, 1, "Atención");
            }

        }

        private void mtdShowCalificacion()
        {
            string codControl = Sanitizer.GetSafeHtmlFragment(TextBox1.Text);
            string NombreControl = Sanitizer.GetSafeHtmlFragment(TextBox2.Text);
            string DescripcionControl = Sanitizer.GetSafeHtmlFragment(TextBox3.Text);
            string ObjetivoControl = Sanitizer.GetSafeHtmlFragment(TextBox4.Text);
            string ResponsableEjecucion = Sanitizer.GetSafeHtmlFragment(TxbResponsableEjecución.Text);
            string IdResponsablesEjecucion = Sanitizer.GetSafeHtmlFragment(LblResponsableEjecucion.Text);
            string IdResponsableCalificacion = Sanitizer.GetSafeHtmlFragment(lblIdDependencia1.Text);
            string Periodicidad = DropDownList6.SelectedValue;
            string test = DropDownList2.SelectedValue;
            string Reduce = DropDownList1.Text;
            /***************************Variables de Calificacion anteriores("Quemadas")*******************************/
            string IdClaseControl = InfoGridControles.Rows[RowGridControles]["IdClaseControl"].ToString().Trim();
            string IdTipoControl = InfoGridControles.Rows[RowGridControles]["IdTipoControl"].ToString().Trim();
            string IdResponsableExperiencia = InfoGridControles.Rows[RowGridControles]["IdResponsableExperiencia"].ToString().Trim();
            string IdDocumentacion = InfoGridControles.Rows[RowGridControles]["IdDocumentacion"].ToString().Trim();
            string IdResponsabilidad = InfoGridControles.Rows[RowGridControles]["IdResponsabilidad"].ToString().Trim();
            trTituloCobertura.Visible = true;
            trCobertura.Visible = true;
            //if (IdClaseControl != "" && IdTipoControl != "" && IdResponsableExperiencia != "" && IdDocumentacion != "" && IdResponsabilidad != "")
            //{
            //    trClaseControl.Visible = true;
            //    trResponsableExp.Visible = true;
            //    trResponsabilidad.Visible = true;

            //    trIframe.Visible = false;
            //}
            //else
            //{
            //    trClaseControl.Visible = false;
            //    trResponsableExp.Visible = false;
            //    trResponsabilidad.Visible = false;

            //    HtmlGenericControl Iframe = new HtmlGenericControl("Iframe");
            //    Iframe.Attributes["width"] = "100%";
            //    Iframe.Attributes["src"] = "../../../UserControls/Riesgos/Iframe/IFcategorias.aspx?CodControl="+codControl+"&NombreControl=" + NombreControl + "&DescripcionControl=" + DescripcionControl +
            //        "&ObjetivoControl=" + ObjetivoControl + "&ResponsableEjecucion=" + ResponsableEjecucion + "&IdResponsablesEjecucion=" + IdResponsablesEjecucion +
            //        "&IdResponsableCalificacion=" + IdResponsableCalificacion + "&Periodicidad=" + Periodicidad + "&test=" + test + "&Reduce=" + Reduce;
            //    dvIframe.Controls.Add(Iframe);
            //    trIframe.Visible = true;
            //}
            trClaseControl.Visible = false;
            trResponsableExp.Visible = false;
            trResponsabilidad.Visible = false;

            HtmlGenericControl Iframe = new HtmlGenericControl("Iframe");
            Iframe.Attributes["width"] = "100%";
            Iframe.Attributes["heigth"] = "800px";
            Iframe.Attributes["src"] = "../../../UserControls/Riesgos/Iframe/IFcategorias.aspx?CodControl=" + codControl + "&NombreControl=" + NombreControl + "&DescripcionControl=" + DescripcionControl +
                "&ObjetivoControl=" + ObjetivoControl + "&ResponsableEjecucion=" + ResponsableEjecucion + "&IdResponsablesEjecucion=" + IdResponsablesEjecucion +
                "&IdResponsableCalificacion=" + IdResponsableCalificacion + "&Periodicidad=" + Periodicidad + "&test=" + test + "&Reduce=" + Reduce;
            dvIframe.Controls.Add(Iframe);
            trIframe.Visible = true;
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            ModificarControl();
        }

        private void ModificarControl()
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    modificarControl();
                    mtdActualizarRiesgosControles();
                    mtdGenerarNotificacion();
                    if (LblResponsableEjecucion.Text.Trim() != "")
                        mtdGenerarNotificacionEjecución();
                    resetValuesControl();
                    resetValuesComentariosArchivos();
                    resetValuesCamposDetalleEvaluacion();
                    resetValuesJustificacionPlanEvaluacion();
                    Session["IframeGuardarCambios"] = false;
                    loadGridControles();
                    Mensaje("El Control fue modificado con éxito");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el control. " + ex.Message);
            }
        }
    }
}