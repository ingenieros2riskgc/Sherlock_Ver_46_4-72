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
using System.Configuration;

namespace ListasSarlaft.UserControls.Eventos
{
    public partial class Eventos : System.Web.UI.UserControl
    {
        #region Variables Globales
        string IdFormulario = "5019";
        string IdFormularioCrearevento = "5011";
        string IdFormularioDatosComple = "5012";
        string IdFormularioContabiliza = "5013";
        string IdFormularioNohuboevent = "5014";
        //string HayResponsableNotificacion = string.Empty;

        string FechaFin = string.Empty;
        string NombreResponsable = string.Empty;
        string strCodigoNHEvento = string.Empty, strCodigoEvento = string.Empty;

        cRiesgo cRiesgo = new cRiesgo();
        cEvento cEvento = new cEvento();
        cControl cControl = new cControl();
        cCuenta cCuenta = new cCuenta();

        //private static int LastInsertId;
        private static int LastInsertIdCE;
        #endregion Variables Globales

        #region Properties
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

        private int rowGridPlanAccionEvento;
        private int RowGridPlanAccionEvento
        {
            get
            {
                rowGridPlanAccionEvento = (int)ViewState["rowGridPlanAccionEvento"];
                return rowGridPlanAccionEvento;
            }
            set
            {
                rowGridPlanAccionEvento = value;
                ViewState["rowGridPlanAccionEvento"] = rowGridPlanAccionEvento;
            }
        }

        private DataTable infoGridPlanAccionEvento;
        private DataTable InfoGridPlanAccionEvento
        {
            get
            {
                infoGridPlanAccionEvento = (DataTable)ViewState["infoGridPlanAccionEvento"];
                return infoGridPlanAccionEvento;
            }
            set
            {
                infoGridPlanAccionEvento = value;
                ViewState["infoGridPlanAccionEvento"] = infoGridPlanAccionEvento;
            }
        }

        private int rowGridArchivoEvento;
        private int RowGridArchivoEvento
        {
            get
            {
                rowGridArchivoEvento = (int)ViewState["rowGridArchivoEvento"];
                return rowGridArchivoEvento;
            }
            set
            {
                rowGridArchivoEvento = value;
                ViewState["rowGridArchivoEvento"] = rowGridArchivoEvento;
            }
        }

        private DataTable infoGridArchivoEvento;
        private DataTable InfoGridArchivoEvento
        {
            get
            {
                infoGridArchivoEvento = (DataTable)ViewState["infoGridArchivoEvento"];
                return infoGridArchivoEvento;
            }
            set
            {
                infoGridArchivoEvento = value;
                ViewState["infoGridArchivoEvento"] = infoGridArchivoEvento;
            }
        }

        private int rowGridComentarioEvento;
        private int RowGridComentarioEvento
        {
            get
            {
                rowGridComentarioEvento = (int)ViewState["rowGridComentarioEvento"];
                return rowGridComentarioEvento;
            }
            set
            {
                rowGridComentarioEvento = value;
                ViewState["rowGridComentarioEvento"] = rowGridComentarioEvento;
            }
        }

        private DataTable infoGridComentarioEvento;
        private DataTable InfoGridComentarioEvento
        {
            get
            {
                infoGridComentarioEvento = (DataTable)ViewState["infoGridComentarioEvento"];
                return infoGridComentarioEvento;
            }
            set
            {
                infoGridComentarioEvento = value;
                ViewState["infoGridComentarioEvento"] = infoGridComentarioEvento;
            }
        }

        private DataTable infoGridEventos;
        private DataTable InfoGridEventos
        {
            get
            {
                infoGridEventos = (DataTable)ViewState["infoGridEventos"];
                return infoGridEventos;
            }
            set
            {
                infoGridEventos = value;
                ViewState["infoGridEventos"] = infoGridEventos;
            }
        }

        private int rowGridEventos;
        private int RowGridEventos
        {
            get
            {
                rowGridEventos = (int)ViewState["rowGridEventos"];
                return rowGridEventos;
            }
            set
            {
                rowGridEventos = value;
                ViewState["rowGridEventos"] = rowGridEventos;
            }
        }

        private DataTable infoGridRiesgoEvento;
        private DataTable InfoGridRiesgoEvento
        {
            get
            {
                infoGridRiesgoEvento = (DataTable)ViewState["infoGridRiesgoEvento"];
                return infoGridRiesgoEvento;
            }
            set
            {
                infoGridRiesgoEvento = value;
                ViewState["infoGridRiesgoEvento"] = infoGridRiesgoEvento;
            }
        }

        private int rowGridRiesgoEvento;
        private int RowGridRiesgoEvento
        {
            get
            {
                rowGridRiesgoEvento = (int)ViewState["rowGridRiesgoEvento"];
                return rowGridRiesgoEvento;
            }
            set
            {
                rowGridRiesgoEvento = value;
                ViewState["rowGridRiesgoEvento"] = rowGridRiesgoEvento;
            }
        }

        private int pagIndexInfoGridEventos;
        private int PagIndexInfoGridEventos
        {
            get
            {
                pagIndexInfoGridEventos = (int)ViewState["pagIndexInfoGridEventos"];
                return pagIndexInfoGridEventos;
            }
            set
            {
                pagIndexInfoGridEventos = value;
                ViewState["pagIndexInfoGridEventos"] = pagIndexInfoGridEventos;
            }
        }

        private DataTable infoGridConsultarRiesgos;
        private DataTable InfoGridConsultarRiesgos
        {
            get
            {
                infoGridConsultarRiesgos = (DataTable)ViewState["infoGridConsultarRiesgos"];
                return infoGridConsultarRiesgos;
            }
            set
            {
                infoGridConsultarRiesgos = value;
                ViewState["infoGridConsultarRiesgos"] = infoGridConsultarRiesgos;
            }
        }

        private int rowGridConsultarRiesgos;
        private int RowGridConsultarRiesgos
        {
            get
            {
                rowGridConsultarRiesgos = (int)ViewState["rowGridConsultarRiesgos"];
                return rowGridConsultarRiesgos;
            }
            set
            {
                rowGridConsultarRiesgos = value;
                ViewState["rowGridConsultarRiesgos"] = rowGridConsultarRiesgos;
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

            if (!Page.IsPostBack)
            {
                resetValuesCamposEventos();
                TabContainerEventos.ActiveTabIndex = 0;
                inicializarValores();
                loadDDLRegion();
                loadDDLCadenaValor();
                loadDDLTipoPerdidaEvento();
                loadDDLTipoRecursoPlanAccion();
                loadDDLEstadoPlanAccion();
                loadGridEventos();
                PopulateTreeView();
                loadDDLServicio();
                loadDDLClaseRiesgo();
                loadDDLEstado();
                loadDDLCanal();
                loadDDLGenerador();
                loadDDLLineaNegocio();
                mtdLoadDDLEmpresa();
                loadDDLMailEvent();
                //mtdLoadGridAudEventoRiesgo();
                //mtdLoadInfoAudEventoRiesgo();
                loadInfoEventosvsUsuario();
                GridView1.Visible = false;
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

        private DataTable infoGridAudEventoRiesgo;
        private DataTable InfoGridAudEventoRiesgo
        {
            get
            {
                infoGridAudEventoRiesgo = (DataTable)ViewState["infoGridAudEventoRiesgo"];
                return infoGridAudEventoRiesgo;
            }
            set
            {
                infoGridAudEventoRiesgo = value;
                ViewState["infoGridAudEventoRiesgo"] = infoGridAudEventoRiesgo;
            }
        }
        
        private void mtdLoadGridAudEventoRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Id", typeof(string));
            grid.Columns.Add("IdEvento", typeof(string));
            grid.Columns.Add("CodigoEvento", typeof(string));
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("Justificacion", typeof(string));
            grid.Columns.Add("Fecha", typeof(string));

            InfoGridAudEventoRiesgo = grid;
            GvDesasociar.DataSource = InfoGridAudEventoRiesgo;
            GvDesasociar.DataBind();
        }

        private void mtdLoadInfoAudEventoRiesgo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.mtdLoadInfoAudEventoRiesgo();

            if (dtInfo.Rows.Count > 0)
            {
                #region Ciclo para poner la informacion de los controles desasociados con el riesgo
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAudEventoRiesgo.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["Id"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),                                                                   
                        dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["Justificacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["Fecha"].ToString().Trim(),
                        });
                }
                #endregion Ciclo para poner la informacion de los controles desasociados con el riesgo

                GvDesasociar.DataSource = InfoGridAudEventoRiesgo;
                GvDesasociar.DataBind();
            }
        }

        private void mtdDesasociarEventoRiesgo(string strId,string strJustificacion)
        {
            cRiesgo.mtdQuitarRelacionEventoRiesgo(strId,strJustificacion);
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridEventoRiesgo = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Desasociar":
                    try
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error " + ex.Message );
                    }
                    break;
            }
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            string strMessage = string.Empty;

                    try
                    {
                        mtdDesasociarEventoRiesgo(InfoGridRiesgoEvento.Rows[RowGridEventoRiesgo]["IdEventoRiesgo"].ToString().Trim(),TBoxJustificacion.Text.Trim());

                        if (string.IsNullOrEmpty(strMessage))
                        {
                            loadGridRiesgoEvento();
                            loadInfoRiesgoEvento();
                            mtdLoadGridAudEventoRiesgo();
                            mtdLoadInfoAudEventoRiesgo();
                            TBoxJustificacion.Text = string.Empty;
                            Mensaje("Desasociación de Riesgo a Evento correctamente");
                        }
                        else
                            Mensaje(strMessage);
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error al eliminar la información. " + ex.Message);
                    }

       }
            

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeViewlast.ExpandAll();
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
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                TreeViewlast.Nodes.Add(newNode);
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

        protected void TreeViewlast_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox34.Text = TreeViewlast.SelectedNode.Text;
            lblIdDependencia1.Text = TreeViewlast.SelectedNode.Value;

        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox33.Text = TreeView2.SelectedNode.Text.Trim();
            lblIdDependencia2.Text = TreeView2.SelectedNode.Value;
        }

        protected void TreeView3_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox35.Text = TreeView3.SelectedNode.Text.Trim();
            lblIdDependencia3.Text = TreeView3.SelectedNode.Value;
        }

        protected void TreeView4_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox51.Text = TreeView4.SelectedNode.Text.Trim();
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview

        #region Load DDL
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
                Mensaje("Error al cargar tipo recurso. " + ex.Message);
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
                Mensaje("Error al cargar estado. " + ex.Message);
            }
        }

        private void loadDDLTipoPerdidaEvento()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cEvento.loadDDLTipoPerdidaEvento();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList8.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoPerdidaEvento"].ToString().Trim(), dtInfo.Rows[i]["IdTipoPerdidaEvento"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar tipo perdida. " + ex.Message);
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
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreRegion"].ToString().Trim(), dtInfo.Rows[i]["IdRegion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar region. " + ex.Message);
            }
        }

        private void loadDDLMailEvent()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cEvento.loadMailEvents();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLmailEvent.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreEvento"].ToString().Trim(), dtInfo.Rows[i]["IdEvento"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Eventos del Correo. " + ex.Message);
            }
        }
        private void loadDDLServicio()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLServicio();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList24.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdServicio"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Servicio. " + ex.Message);
            }
        }

        private void loadDDLClaseRiesgo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClaseRiesgo();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList33.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdClase"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Clase Riesgo. " + ex.Message);
            }
        }

        private void loadDDLLineaNegocio()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLLineaNegocio();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList23.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdLineaNegocio"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Linea Negocio. " + ex.Message);
            }
        }

        private void loadDDLEstado()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLEstado();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList16.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEstado"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Estado. " + ex.Message);
            }
        }

        private void loadDDLCanal()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCanal();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList26.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdCanal"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Estado. " + ex.Message);
            }
        }

        private void loadDDLGenerador()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLGenerador();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList27.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdGenerador"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Generador del evento. " + ex.Message);
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
                            DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombrePais"].ToString().Trim(), dtInfo.Rows[i]["IdPais"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar pais. " + ex.Message);
            }
        }

        private void loadDDLSubServicio(String IdServicio, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubServicio(IdServicio);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList25.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["SubDescripcion"].ToString().Trim(), dtInfo.Rows[i]["IdSubServicio"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar SubServicio. " + ex.Message);
            }
        }

        private void loadDDLSubClaseRiesgo(String IdClase, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubClaseRiesgo(IdClase);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList34.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["SubDescripcion"].ToString().Trim(), dtInfo.Rows[i]["IdSubClase"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar SubClaseRiesgo. " + ex.Message);
            }
        }

        private void loadDDLSubLineaNegocio(String IdLineaNegocio, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubLineaNegocio(IdLineaNegocio);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList29.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["SubDescripcion"].ToString().Trim(), dtInfo.Rows[i]["IdSubLineaNegocio"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar SubLineaNegocioRiesgo. " + ex.Message);
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
                    DropDownList19.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar cadena valor. " + ex.Message);
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
                            DropDownList3.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreDepartamento"].ToString().Trim(), dtInfo.Rows[i]["IdDepartamento"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar departamento. " + ex.Message);
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
                            DropDownList4.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCiudad"].ToString().Trim(), dtInfo.Rows[i]["IdCiudad"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar ciudad. " + ex.Message);
            }
        }

        private void loadDDLOficinaSucursal(String IdCiudad, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                if(IdCiudad != "---")
                    dtInfo = cRiesgo.loadDDLOficinaSucursal(IdCiudad);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList5.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreOficinaSucursal"].ToString().Trim(), dtInfo.Rows[i]["IdOficinaSucursal"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar oficina/Sucursal. " + ex.Message);
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
                        if (DropDownList67.SelectedValue != "---")
                        {
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList9.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            }
                        }
                        break;
                    case 3:
                        if (DropDownList19.SelectedValue != "---")
                        {
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList20.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar macroproceso. " + ex.Message);
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
                        if (DropDownList9.SelectedValue != "---")
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList10.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                            }
                        break;
                    case 3:
                        if (DropDownList20.SelectedValue != "---")
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList21.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                            }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar proceso. " + ex.Message);
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
                        if (DropDownList10.SelectedValue != "---")
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList6.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                            }
                        break;
                    case 3:
                        if (DropDownList21.SelectedValue != "---")
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList22.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                            }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar subproceso. " + ex.Message);
            }
        }

        private void loadDDLActividad(String IdSubproceso, int Tipo)
        {
            if (DropDownList6.SelectedValue != "---")
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
                    }
                }
                catch (Exception ex)
                {
                    Mensaje("Error al cargar actividad. " + ex.Message);
                }
            }
        }

        private void mtdLoadDDLEmpresa()
        {
            DataTable dtInfo = new DataTable();

            try
            {
                dtInfo = cRiesgo.mtdLoadEmpresa(true);
                ddlEmpresa.Items.Insert(0, new ListItem("---", "---"));
                ddlEmpresa1.Items.Insert(0, new ListItem("---", "---"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlEmpresa.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                    ddlEmpresa1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Empresas. " + ex.Message);
            }
        }
        #endregion Load DDL

        #region DDL

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "---"));
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList1.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLPais(DropDownList1.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "---"));
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList2.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLDepartamento(DropDownList2.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList3.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLCiudad(DropDownList3.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList4.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLOficinaSucursal(DropDownList4.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList6.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLActividad(DropDownList6.SelectedValue.ToString().Trim(), 1);
            }
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
            {
                loadDDLProceso(DropDownList9.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList10.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(DropDownList10.SelectedValue.ToString().Trim(), 1);
            }
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
            {
                loadDDLMacroproceso(DropDownList19.SelectedValue.ToString().Trim(), 3);
            }
        }

        protected void DropDownList20_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList20.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList20.SelectedValue.ToString().Trim(), 3);
            }
        }

        protected void DropDownList21_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList21.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(DropDownList21.SelectedValue.ToString().Trim(), 3);
            }
        }

        protected void DropDownList23_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList29.SelectedValue == "---")
            {
                TextBox17.Text = "";
                TrMasLNegocio.Visible = false;
            }
            DropDownList29.Items.Clear();
            DropDownList29.Items.Insert(0, new ListItem("---", "NULL"));
            if (DropDownList23.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubLineaNegocio(DropDownList23.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList24_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList25.Items.Clear();
            DropDownList25.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList24.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubServicio(DropDownList24.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList27_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox51.Text = null;
            lblIdDependencia4.Text = null;
            if (DropDownList27.SelectedValue != "---")
            {
                lresponable.Visible = true;
                tresponsable.Visible = true;
                RequiredFieldValidatorTextBox51.ValidationGroup = "Addne";
                if (DropDownList27.SelectedValue != "1")
                {
                    TextBox51.Enabled = true;
                    imgDependencia4.Visible = false;
                }
                else
                {
                    TextBox51.Enabled = false;
                    imgDependencia4.Visible = true;
                }
            }
            else
            {
                RequiredFieldValidatorTextBox51.ValidationGroup = null;
                lresponable.Visible = false;
                tresponsable.Visible = false;
                TextBox51.Enabled = true;
            }
        }

        protected void DropDownList28_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList28.SelectedValue == "1")
            {
                trecuperacion.Visible = true;
                lrecuperacio.Visible = true;
                TextBox46.Focus();
            }
            else
            {
                trecuperacion.Visible = false;
                lrecuperacio.Visible = false;
                TextBox46.Text = "";
            }
        }

        protected void DropDownList29_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList29.SelectedValue != "---")
            {
                TrMasLNegocio.Visible = true;
            }
            else
            {
                TextBox17.Text = "";
                TrMasLNegocio.Visible = false;
            }
        }

        protected void DropDownList33_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList34.Items.Clear();
            DropDownList34.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList33.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubClaseRiesgo(DropDownList33.SelectedValue.ToString().Trim(), 1);
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
            {
                loadDDLMacroproceso(DropDownList67.SelectedValue.ToString().Trim(), 1);
            }
        }

        #endregion DDL

        #region Reset
        private void reseteartodo()
        {
            lblExisteResponsableNotificacion.Text = string.Empty;
            Label55.Text = "";
            ddlEmpresa1.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;
            DropDownList5.SelectedIndex = 0;
            TextBox43.Text = "";
            TextBox44.Text = "";
            DropDownList24.SelectedIndex = 0;
            DropDownList25.SelectedIndex = 0;
            TextBox45.Text = "";
            DropDownList13.SelectedIndex = 0;
            DropDownList13.SelectedIndex = 0;
            DropDownList14.SelectedIndex = 0;
            TextBox47.Text = "";
            DropDownList68.SelectedIndex = 0;
            DropDownList69.SelectedIndex = 0;
            DropDownList70.SelectedIndex = 0;
            TextBox49.Text = "";
            DropDownList71.SelectedIndex = 0;
            DropDownList72.SelectedIndex = 0;
            DropDownList73.SelectedIndex = 0;
            DropDownList26.SelectedIndex = 0;
            DropDownList27.SelectedIndex = 0;
            TextBox51.Text = "";
            TextBox52.Text = "";
            TextBox39.Text = "";
            TextBox40.Text = "";
            //tab 2
            DropDownList67.SelectedIndex = 0;
            DropDownList9.SelectedIndex = 0;
            DropDownList10.SelectedIndex = 0;
            DropDownList6.SelectedIndex = 0;
            DropDownList11.SelectedIndex = 0;
            TextBox34.Text = "";
            DropDownList33.SelectedIndex = 0;
            DropDownList23.SelectedIndex = 0;
            DropDownList29.SelectedIndex = 0;
            DropDownList34.SelectedIndex = 0;
            DropDownList8.SelectedIndex = 0;
            DropDownList15.SelectedIndex = 0;
            DropDownList16.SelectedIndex = 0;
            TextBox53.Text = "";
            //tab 3
            TextBox33.Text = "";
            TextBox14.Text = "";
            TextBox15.Text = "";
            TextBox16.Text = "";
            TextBox17.Text = "";
            TrMasLNegocio.Visible = false;
            DropDownList74.SelectedIndex = 0;
            TextBox21.Text = "";
            TextBox22.Text = "";
            TextBox23.Text = "";
            DropDownList75.SelectedIndex = 0;
            TextBox25.Text = "";
            TextBox26.Text = "";
            TextBox27.Text = "";
            TextBox28.Text = "";
            DropDownList28.SelectedIndex = 0;
            TextBox46.Text = "";
            ImageButton6.Visible = false;
            ImageButton8.Visible = false;

            if (TreeViewlast.SelectedNode != null)
                TreeViewlast.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;

        }

        private void resetValuesCamposEventos()
        {
            DropDownList1.SelectedIndex = 0;
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "---"));
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            DropDownList67.SelectedIndex = 0;
            DropDownList9.Items.Clear();
            DropDownList9.Items.Insert(0, new ListItem("---", "---"));
            DropDownList10.Items.Clear();
            DropDownList10.Items.Insert(0, new ListItem("---", "0"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            TextBox33.Text = "";
            lblIdDependencia2.Text = "";
            lblIdDependencia2.Visible = false;
            DropDownList8.SelectedIndex = 0;
            TextBox34.Text = "";
            lblIdDependencia1.Text = "";
            lblIdDependencia1.Visible = false;
            TextBox14.Text = "";
            TextBox15.Text = "";
            TextBox16.Text = "";
            TextBox17.Text = "";
            TextBox21.Text = "";
            TextBox22.Text = "";
            TextBox23.Text = "";
            TextBox25.Text = "";
            TextBox26.Text = "";
            TextBox27.Text = "";
            TextBox28.Text = "";
            CBriesgosSinCausa.Checked = false;
            lblExisteResponsableNotificacion.Text = string.Empty;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        private void resetValuesRiesgoEventos()
        {
            trRiesgosEventos.Visible = false;
            TextBox31.Text = "";
            TextBox32.Text = "";
        }

        private void resetValuesConsulta()
        {
            TextBox29.Text = "";
            TextBox30.Text = "";
            DropDownList19.SelectedIndex = 0;
            DropDownList20.Items.Clear();
            DropDownList20.Items.Insert(0, new ListItem("---", "---"));
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
        }

        private void resetValuesJustificacionEvento()
        {
            TextBox11.ReadOnly = false;
            TextBox11.Text = "";
            ImageButton17.Visible = false;
        }

        private void resetValuesJustificacionPlanAccion()
        {
            TextBox38.ReadOnly = false;
            TextBox38.Text = "";
            ImageButton11.Visible = false;
        }

        private void resetValuesEventoPlanAccion()
        {
            TextBox20.Text = "";
            TextBox35.Text = "";
            DropDownList17.SelectedIndex = 0;
            TextBox36.Text = "";
            DropDownList18.SelectedIndex = 0;
            TextBox37.Text = "";
            ImageButton13.Visible = false;
            ImageButton12.Visible = false;
            trAddPlanAccion.Visible = false;
            lblIdDependencia3.Text = "";
            lblIdDependencia3.Visible = false;
            TextBox38.Text = "";
            TextBox38.ReadOnly = false;
            ImageButton11.Visible = false;
            trAdjComPlaAcci.Visible = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

        }
        #endregion Reset

        #region Buttons
        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                resetValuesEventoPlanAccion();
                trAddPlanAccion.Visible = true;
                ImageButton13.Visible = true;
            }
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosAgregar(IdFormularioCrearevento) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                TabContainerEventos.Tabs[1].Visible = true;
                TabContainerEventos.Tabs[2].Visible = true;
                string Horaini = string.Empty, Horafin = string.Empty, Horades = string.Empty;

                if (DropDownList12.SelectedValue != "---" && DropDownList13.SelectedValue != "---" && DropDownList14.SelectedValue != "---")
                    Horaini = DropDownList12.SelectedItem.Text.ToString() + ":" + DropDownList13.SelectedItem.Text.ToString() + " " + DropDownList14.SelectedItem.Text.ToString().Trim();

                if (DropDownList68.SelectedValue != "---" && DropDownList69.SelectedValue != "---" && DropDownList70.SelectedValue != "---")
                    Horafin = "'" + DropDownList68.SelectedItem.Text.ToString() + ":" + DropDownList69.SelectedItem.Text.ToString() + " " + DropDownList70.SelectedItem.Text.ToString() + "'";
                else
                    Horafin = "NULL";

                if (DropDownList71.SelectedValue != "---" && DropDownList72.SelectedValue != "---" && DropDownList73.SelectedValue != "---")
                    Horades = DropDownList71.SelectedItem.Text.ToString() + ":" + DropDownList72.SelectedItem.Text.ToString() + " " + DropDownList73.SelectedItem.Text.ToString().Trim();

                if (Sanitizer.GetSafeHtmlFragment(TextBox47.Text) == "")
                    FechaFin = "Null";
                else
                    FechaFin = "CONVERT(datetime,'" + Sanitizer.GetSafeHtmlFragment(TextBox47.Text.Trim()) + " 12:00:00:000" + "', 120)";

                try
                {
                    strCodigoEvento = string.Empty;
                    if (Convert.ToInt32(Session["IdUsuario"].ToString()) > 0)
                    {
                        cEvento.agregarEvento(ref strCodigoEvento, ddlEmpresa1.SelectedValue.ToString().Trim(), DropDownList1.SelectedValue.ToString().Trim(),
                            DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(),
                            DropDownList5.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox43.Text), Sanitizer.GetSafeHtmlFragment(TextBox44.Text), DropDownList24.SelectedValue.ToString().Trim(),
                            DropDownList25.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox45.Text.Trim()) + " 12:00:00:000", Horaini, FechaFin, Horafin,
                            Sanitizer.GetSafeHtmlFragment(TextBox49.Text.Trim()) + " 12:00:00:000", Horades, DropDownList26.SelectedValue.ToString().Trim(), DropDownList27.SelectedValue.ToString().Trim(),
                            lblIdDependencia4.Text, Sanitizer.GetSafeHtmlFragment(TextBox52.Text), Sanitizer.GetSafeHtmlFragment(TextBox39.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox51.Text.Trim()), Convert.ToInt32(Session["IdUsuario"].ToString()));

                        try
                        {
                            boolEnviarNotificacion(17, Convert.ToInt16("0"), Convert.ToInt16(Session["idJerarquia"].ToString()), "",
                                "<B>Se ha creado el Evento</B> <br /><br /><B>Código Evento: </B>" + strCodigoEvento.Trim() + "<br /><B>Descripción del evento: </B>" + TextBox44.Text.Trim()+ "<br />");
                        }
                        catch (Exception ex)
                        {
                            Mensaje("Error al enviar notificación de creacion de Evento." + ex.Message);
                        }

                        Label55.Text = strCodigoEvento.Trim();

                        /*loadGridEventos();
                        loadInfoEventos();*/
                        loadGridEventos();
                        loadInfoEventosvsUsuario();

                        ImageButton6.Visible = false;
                        ImageButton8.Visible = true;

                        string strMensaje = string.Format("Evento [{0}] creado correctamente", strCodigoEvento.Trim());
                        Mensaje(strMensaje);

                        if (TreeView4.SelectedNode != null)
                            TreeView4.SelectedNode.Selected = false;
                    }
                    else
                    {
                        Mensaje("Error: No es posible guardar el Evento sin información de usuario. La sesión a caducado.");
                    }
                }
                catch (Exception a)
                {
                    Mensaje("Error al guardar Evento." + a.Message);
                }
            }
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarTodo();
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormularioCrearevento) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                string Horaini = string.Empty, Horafin = string.Empty, Horades = string.Empty;

                if (DropDownList12.SelectedValue != "---" && DropDownList13.SelectedValue != "---" && DropDownList14.SelectedValue != "---")
                    Horaini = DropDownList12.SelectedItem.Text.ToString() + ":" + DropDownList13.SelectedItem.Text.ToString() + " " + DropDownList14.SelectedItem.Text.ToString().Trim();

                if (DropDownList68.SelectedValue != "---" && DropDownList69.SelectedValue != "---" && DropDownList70.SelectedValue != "---")
                    Horafin = DropDownList68.SelectedItem.Text.ToString() + ":" + DropDownList69.SelectedItem.Text.ToString() + " " + DropDownList70.SelectedItem.Text.ToString().Trim();

                if (DropDownList71.SelectedValue != "" && DropDownList72.SelectedValue != "---" && DropDownList73.SelectedValue != "---")
                    Horades = DropDownList71.SelectedItem.Text.ToString() + ":" + DropDownList72.SelectedItem.Text.ToString() + " " + DropDownList73.SelectedItem.Text.ToString().Trim();

                //valida si la fecha finalizacion es o no NULL
                if (Sanitizer.GetSafeHtmlFragment(TextBox47.Text) == "")
                    FechaFin = "Null";
                else
                    FechaFin = "CONVERT(datetime,'" + Sanitizer.GetSafeHtmlFragment(TextBox47.Text.Trim()) + " 12:00:00:000" + "', 120)";
                //Valida si el responsable es o no funcionario

                if (DropDownList27.SelectedValue != "1")
                    NombreResponsable = "'" + Sanitizer.GetSafeHtmlFragment(TextBox51.Text.Trim()) + "'";
                else
                    NombreResponsable = "Null";

                try
                {
                    //Camilo
                    cEvento.ModificaEventoTab1(ddlEmpresa1.SelectedValue.ToString().Trim(), DropDownList1.SelectedValue.ToString().Trim(), DropDownList2.SelectedValue.ToString().Trim(),
                        DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), DropDownList5.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox43.Text),
                        Sanitizer.GetSafeHtmlFragment(TextBox44.Text), DropDownList24.SelectedValue.ToString().Trim(), DropDownList25.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox45.Text.Trim()) + " 12:00:00:000",
                        Horaini, FechaFin, Horafin, TextBox49.Text.Trim() + " 12:00:00:000", Horades, DropDownList26.SelectedValue.ToString().Trim(),
                        DropDownList27.SelectedValue.ToString().Trim(), lblIdDependencia4.Text, Sanitizer.GetSafeHtmlFragment(TextBox52.Text), Label55.Text.Trim(), NombreResponsable);

                    /*loadGridEventos();
                    loadInfoEventos();*/
                    loadGridEventos();
                    loadInfoEventosvsUsuario();
                    cargardatosevento();
                    trRiesgosEventos.Visible = true;
                    trAdjComPlaAcci.Visible = true;

                    agregarComentarioEvento();
                    /*loadGridEventos();
                    loadInfoEventos();*/
                    Mensaje("Evento actualizado correctamente");

                    if (TreeView4.SelectedNode != null)
                        TreeView4.SelectedNode.Selected = false;
                }
                catch (Exception a)
                {
                    Mensaje("Error al actualizar Evento." + a.Message);
                }
            }
        }

        protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Sanitizer.GetSafeHtmlFragment(TextBox31.Text.Trim()) == "" && Sanitizer.GetSafeHtmlFragment(TextBox32.Text.Trim()) == "")
                    Mensaje("Debe ingresar por lo menos un parámetro de consulta.");
                else
                {
                    loadGridConsultarRiesgos();
                    loadInfoConsultarRiesgos();
                    //mtdLoadGridAudEventoRiesgo();
                    //mtdLoadInfoAudEventoRiesgo();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesRiesgoEventos();
            trRiesgosEventos.Visible = true;
            loadGridConsultarRiesgos();
        }

        protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesJustificacionPlanAccion();
        }

        protected void ImageButton12_Click1(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormularioCrearevento) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    actualizarPlanAccionEvento();
                    agregarComentarioPlanAccion();
                    if (DropDownList18.SelectedItem.ToString().Trim() == "Cerrado")
                        boolEnviarNotificacion(10, Convert.ToInt16(InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdPlanAccion"].ToString().Trim()), Convert.ToInt16(lblIdDependencia3.Text.Trim()), "",
                            "Descripción de la Acción: " + Sanitizer.GetSafeHtmlFragment(TextBox20.Text.Trim()) +
                            "<br />Fecha de Compromiso: " + Sanitizer.GetSafeHtmlFragment(TextBox37.Text.Trim()) +
                            "<br />Fecha de Cierre: " + DateTime.Now.ToString("u") +
                            "<br />Número del Evento al cual está asociado el Plan de Acción: " + InfoGridEventos.Rows[RowGridEventos]["CodigoEvento"].ToString().Trim() + "<br /><br />");

                    resetValuesEventoPlanAccion();
                    loadGridPlanAccionEvento();
                    loadInfoPlanAccionEvento();
                    Mensaje("Plan de acción modificado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar plan acción. " + ex.Message);
            }
        }

        protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Sanitizer.GetSafeHtmlFragment(TextBox29.Text.Trim()) == "" && Sanitizer.GetSafeHtmlFragment(TextBox30.Text.Trim()) == "" && DropDownList19.SelectedValue.ToString().Trim() == "---" && DropDownList20.SelectedValue.ToString().Trim() == "---" && DropDownList21.SelectedValue.ToString().Trim() == "---" && DropDownList22.SelectedValue.ToString().Trim() == "---")
                    Mensaje("Debe ingresar por lo menos un parámetro de consulta.");
                else
                {
                    inicializarValores();
                    resetValuesCamposEventos();
                    resetValuesRiesgoEventos();
                    resetValuesJustificacionEvento();
                    resetValuesEventoPlanAccion();

                    loadGridEventos();
                    loadInfoEventos();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void ImageButton13_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormularioCrearevento) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    /*if (Convert.ToInt64(Sanitizer.GetSafeHtmlFragment(TextBox37.Text.Trim().Replace("-", ""))) <= Convert.ToInt64(DateTime.Now.Date.ToString("yyyy-MM-dd").Replace("-", "")))
                        Mensaje("Debe ingresar una fecha compromiso valida.");
                    else
                    {*/
                        int IdRegistro = mtdRegistrarPlanAccionEvento();
                        string strTextoAdicional = string.Format("Descripción de la Acción: {0}<br />Fecha de Compromiso: {1}" +
                            "<br />Número del Evento al cual está asociado el Plan de Acción: {2}<br /><br />",
                            Sanitizer.GetSafeHtmlFragment(TextBox20.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox37.Text.Trim()), InfoGridEventos.Rows[RowGridEventos]["CodigoEvento"].ToString().Trim());

                        boolEnviarNotificacion(10, IdRegistro, Convert.ToInt16(lblIdDependencia3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox37.Text.Trim()) + " 12:00:00:000",
                           strTextoAdicional);

                        resetValuesEventoPlanAccion();
                        loadGridPlanAccionEvento();
                        loadInfoPlanAccionEvento();
                        Mensaje("Plan de acción creado con éxito.");
                    //}
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar plan acción. " + ex.Message);
            }
        }

        protected void ImageButton14_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesEventoPlanAccion();
        }

        protected void ImageButton15_Click1(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload2.HasFile)
                    {
                        
                            loadFilePlanAccion();
                            loadGridArchivoPlanAccion();
                            loadInfoArchivoPlanAccion();
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

        protected void ImageButton15_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesConsulta();
            resetValuesCamposEventos();
            resetValuesRiesgoEventos();
            resetValuesJustificacionEvento();
            resetValuesEventoPlanAccion();
            loadGridEventos();
            TbEventos.Visible = false;
        }

        protected void ImageButton16_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        /*if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {*/
                            mtdCargarPdfEvento();
                            loadGridArchivoEvento();
                            loadInfoArchivoEvento();
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

        protected void ImageButton17_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesJustificacionEvento();
        }

        protected void ImageButton19_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormularioDatosComple) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    cEvento.modificarEvento(DropDownList67.SelectedValue.ToString(), DropDownList9.SelectedValue.ToString(), DropDownList10.SelectedValue.ToString(), DropDownList6.SelectedValue.ToString(), DropDownList11.SelectedValue.ToString(), lblIdDependencia1.Text, DropDownList33.SelectedValue.ToString(), DropDownList34.SelectedValue.ToString(), DropDownList8.SelectedValue.ToString(), DropDownList15.SelectedValue.ToString(), DropDownList16.SelectedValue.ToString(), TextBox53.Text.Trim(), Label55.Text, DropDownList23.SelectedItem.Value, DropDownList29.SelectedItem.Value, TextBox17.Text.Trim());
                    if (lblIdDependencia1.Text != "" && string.IsNullOrEmpty(lblExisteResponsableNotificacion.Text))
                        boolEnviarNotificacion(9, Convert.ToInt16("0"), Convert.ToInt16(lblIdDependencia1.Text.Trim()), "", "Ha sido asignado como responsable del Evento: <br/><B>Código Evento: </B>" + Label55.Text.Trim() + "<br/><B> Descripción del evento: </B>"+ TextBox44.Text.Trim() + "<br/>En la aplicación de Sherlock para la Gestión de Riesgos y Control Interno.<br />");
                    
                    loadGridEventos();
                    loadInfoEventos();
                    Mensaje("Evento actualizado correctamente");
                cEvento.modificarEvento(DropDownList67.SelectedValue.ToString().Trim(), DropDownList9.SelectedValue.ToString().Trim(), DropDownList10.SelectedValue.ToString().Trim(), DropDownList6.SelectedValue.ToString().Trim(), DropDownList11.SelectedValue.ToString().Trim(), lblIdDependencia1.Text, DropDownList33.SelectedValue.ToString().Trim(), DropDownList34.SelectedValue.ToString().Trim(), DropDownList8.SelectedValue.ToString().Trim(), DropDownList15.SelectedValue.ToString().Trim(), DropDownList16.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox53.Text.Trim()), Label55.Text, DropDownList23.SelectedItem.Value, DropDownList29.SelectedItem.Value, Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()));
                if (lblIdDependencia1.Text != "")
                    boolEnviarNotificacion(9, Convert.ToInt16("0"), Convert.ToInt16(lblIdDependencia1.Text.Trim()), "", "Ha sido asignado como responsable del Evento: <br /><B>Código Evento: </B>" + Label55.Text.Trim() + "<br/><B> Descripción del evento: </B>" + TextBox44.Text.Trim() + "<br/>En la aplicación de Sherlock para la Gestión de Riesgos y Control Interno.<br/>");

                    /*loadGridEventos();
                    loadInfoEventos();*/
                    loadGridEventos();
                    loadInfoEventosvsUsuario();
                    Mensaje("Evento actualizado correctamente");

                    if (TreeViewlast.SelectedNode != null)
                        TreeViewlast.SelectedNode.Selected = false;
                }
            }
            catch (Exception a)
            {
                Mensaje("Error al actualizar evento." + a.Message);
            }
        }

        protected void ImageButton21_Click(object sender, ImageClickEventArgs e)
        {
            bool booIsFecha = true;

            try
            {
                if (cCuenta.permisosActualizar(IdFormularioContabiliza) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (TextBox1.Text == string.Empty)
                        booIsFecha = false;
                if (Sanitizer.GetSafeHtmlFragment(TextBox1.Text) == string.Empty)
                    booIsFecha = false;

                    string strFechaContab = string.Empty, strHoraContab = string.Empty, fechaRecuperacion = string.Empty;

                if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim())))
                    strFechaContab = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()) + " 12:00:000";
                    if (!string.IsNullOrEmpty(TextBox1.Text.Trim()))
                        strFechaContab = TextBox1.Text.Trim() + " 12:00:000";

                    if (DropDownList7.SelectedValue != "---" &&
                        DropDownList30.SelectedValue != "---" &&
                        DropDownList31.SelectedValue != "---")
                        strHoraContab = string.Format("{0}:{1} {2}",
                            DropDownList7.SelectedItem.Text.ToString(), DropDownList30.SelectedItem.Text.ToString(), DropDownList31.SelectedItem.Text.ToString());
                 if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(txtFechaRecuperacion.Text.Trim())))
                        fechaRecuperacion = Sanitizer.GetSafeHtmlFragment(txtFechaRecuperacion.Text.Trim());
                    if (ddlHoraRecuperacion.SelectedValue != "---" &&
                          ddlMinutoRecuperacion.SelectedValue != "---" &&
                          ddlHorarioRecuperacion.SelectedValue != "---")
                        fechaRecuperacion = string.Format("{3} {0}:{1} {2}",
                            ddlHoraRecuperacion.SelectedItem.Text.ToString(), ddlMinutoRecuperacion.SelectedItem.Text.ToString(), ddlHorarioRecuperacion.SelectedItem.Text.ToString(), fechaRecuperacion);
                    cEvento.mtdModificarEvento2(lblIdDependencia2.Text, Sanitizer.GetSafeHtmlFragment(TextBox14.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox15.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox16.Text.Trim()),
                    DropDownList74.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox21.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox22.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox23.Text.Trim()),
                    DropDownList75.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox25.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox26.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox27.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(TextBox28.Text.Trim()), DropDownList28.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox46.Text.Trim()), Label55.Text,
                    booIsFecha, strFechaContab, strHoraContab, fechaRecuperacion,
                    Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtCuantiaRecuperadaSeguros.Text.Trim())),
                    Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtCuantiaOtrasRecuperaciones.Text.Trim())),
                    Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtCuantiaNetaRecuperaciones.Text.Trim()))
                    );

                    /*loadGridEventos();
                    loadInfoEventos();*/
                    loadGridEventos();
                    loadInfoEventosvsUsuario();
                    Mensaje("Evento actualizado correctamente");

                    if (TreeView2.SelectedNode != null)
                        TreeView2.SelectedNode.Selected = false;
                }
            }
            catch (Exception a)
            {
                Mensaje("Error al actualizar evento." + a.Message);
            }
        }

        protected void BtnCancelaNoeventos_Click(object sender, ImageClickEventArgs e)
        {
            TbNohuboeventos.Visible = false;
            RadioButtonList1.ClearSelection();
        }

        protected void BtnGuardaNoeventos_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormularioNohuboevent) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                agregarNHEvento();
                TbNohuboeventos.Visible = false;
                RadioButtonList1.ClearSelection();
                ddlEmpresa.SelectedIndex = 0;

                string NombreJerarquia = string.Empty;
                string NombreArea = string.Empty;
                string CodNHE = string.Empty;
                int IdEvento = 0;
                /*int IdMailEvent = Convert.ToInt32(DDLmailEvent.SelectedValue.ToString());*/
                cEvento.mtdJerarquiaUsuario(Session["IdUsuario"].ToString(), ref NombreJerarquia, ref NombreArea);
                CodNHE = cEvento.mtdLastNoHuboEvento(ref IdEvento);

                mtdGenerarNotificacionNoHuboEvento(CodNHE,NombreJerarquia, NombreArea, IdEvento, 30);

                string strMensaje = string.Format("Evento (No Hubo Eventos) [{0}] creado y almacenado correctamente", strCodigoNHEvento);
                Mensaje(strMensaje);
            }
        }

        #endregion Imagebuttons

        #region Loads

        private void loadFilePlanAccion()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cControl.loadCodigoArchivoControl();

            if (dtInfo.Rows.Count > 0)
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdPlanAccion"].ToString().Trim() + "-" + FileUpload2.FileName.ToString().Trim();
            else
                nameFile = "1-" + InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdPlanAccion"].ToString().Trim() + "-" + FileUpload2.FileName.ToString().Trim();

            FileUpload2.SaveAs(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/") + nameFile);
            cEvento.agregarArchivoPlanAccion(InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdPlanAccion"].ToString().Trim(), nameFile);
        }

        private void loadInfoArchivoEvento()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cEvento.loadInfoArchivoEvento(InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoEvento.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                                });
                }

                GridView4.DataSource = InfoGridArchivoEvento;
                GridView4.DataBind();
            }
        }

        private void loadGridArchivoEvento()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoEvento = grid;
            GridView4.DataSource = InfoGridArchivoEvento;
            GridView4.DataBind();
        }

        private void loadInfoComentarioEvento()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cEvento.loadInfoComentarioEvento(InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioEvento.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["ComentarioCorto"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                                                                   });
                }

                GridView6.DataSource = InfoGridComentarioEvento;
                GridView6.DataBind();
            }
        }

        private void loadGridComentarioEvento()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("ComentarioCorto", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            InfoGridComentarioEvento = grid;
            GridView6.DataSource = InfoGridComentarioEvento;
            GridView6.DataBind();
        }

        private void loadInfoRiesgoEvento()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cEvento.loadInfoRiesgoEvento(InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRiesgoEvento.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdEventoRiesgo"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim()
                                                               });
                }

                GridView2.DataSource = InfoGridRiesgoEvento;
                GridView2.DataBind();
            }
        }

        private void loadGridRiesgoEvento()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdEventoRiesgo", typeof(string));
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            InfoGridRiesgoEvento = grid;
            GridView2.DataSource = InfoGridRiesgoEvento;
            GridView2.DataBind();
        }

        private void loadInfoConsultarRiesgos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoRiesgos(Sanitizer.GetSafeHtmlFragment(TextBox31.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox32.Text.Trim()), "---", "---", "---", "---", "---");

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridConsultarRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),                                                           
                                                                    dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["ListaCausas"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["IdProbabilidad"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["IdImpacto"].ToString().Trim()
                                                                   });
                }

                GridView8.DataSource = InfoGridConsultarRiesgos;
                GridView8.DataBind();
                GridView8.Visible = true;
            }
            else
            {
                GridView8.Visible = false;
                loadGridConsultarRiesgos();
                Mensaje("El usuario no tiene eventos reportados.");
            }
        }
        private void loadInfoConsultarRiesgosProceso()
        {
            DataTable dtInfo = new DataTable();
            if (InfoGridConsultarRiesgos.Rows.Count > 0)
                loadGridConsultarRiesgos();
            string CadenaValor = DropDownList67.SelectedValue;
            string MacroProceso = DropDownList9.SelectedValue;
            string Proceso = DropDownList10.SelectedValue;
            string Subproceso = DropDownList6.SelectedValue;
            dtInfo = cRiesgo.loadInfoRiesgosProceso(CadenaValor, MacroProceso, Proceso, Subproceso);

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridConsultarRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["ListaCausas"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["IdProbabilidad"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["IdImpacto"].ToString().Trim()
                                                                   });
                }

                GridView8.DataSource = InfoGridConsultarRiesgos;
                GridView8.DataBind();
                GridView8.Visible = true;
            }
            else
            {
                GridView8.Visible = false;
                loadGridConsultarRiesgos();
                //Mensaje("El usuario no tiene eventos reportados.");
            }
        }
        private void loadInfoConsultarRiesgosCausas(string ListCausas)
        {
            DataTable dtInfo = new DataTable();
            
            dtInfo = cRiesgo.loadInfoRiesgosCausas(ListCausas);

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
        private void loadGridConsultarRiesgos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("ListaCausas", typeof(string));
            grid.Columns.Add("IdProbabilidad", typeof(string));
            grid.Columns.Add("IdImpacto", typeof(string));
            InfoGridConsultarRiesgos = grid;
            GridView8.DataSource = InfoGridConsultarRiesgos;
            GridView8.DataBind();
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
        private void loadInfoArchivoPlanAccion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cEvento.loadInfoArchivoPlanAccion(InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdPlanAccion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoPlanAccion.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
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

        private void loadInfoComentarioPlanAccion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cEvento.loadInfoComentarioPlanAccion(InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdPlanAccion"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioPlanAccion.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
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

        private void loadGridEventos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdEvento", typeof(string));
            grid.Columns.Add("CodigoEvento", typeof(string));
            grid.Columns.Add("IdEmpresa", typeof(string));
            grid.Columns.Add("IdRegion", typeof(string));
            grid.Columns.Add("IdPais", typeof(string));
            grid.Columns.Add("IdDepartamento", typeof(string));
            grid.Columns.Add("IdCiudad", typeof(string));
            grid.Columns.Add("IdOficinaSucursal", typeof(string));
            grid.Columns.Add("DetalleUbicacion", typeof(string));
            grid.Columns.Add("DescripcionEvento", typeof(string));
            grid.Columns.Add("IdServicio", typeof(string));
            grid.Columns.Add("IdSubServicio", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            //grid.Columns.Add("HoraInicio", typeof(string));
            grid.Columns.Add("HorI", typeof(string));
            grid.Columns.Add("MinI", typeof(string));
            grid.Columns.Add("amI", typeof(string));
            grid.Columns.Add("FechaFinalizacion", typeof(string));
            //grid.Columns.Add("HoraFinalizacion", typeof(string));
            grid.Columns.Add("HorF", typeof(string));
            grid.Columns.Add("MinF", typeof(string));
            grid.Columns.Add("amF", typeof(string));
            grid.Columns.Add("FechaDescubrimiento", typeof(string));
            //grid.Columns.Add("HoraDescubrimiento", typeof(string));
            grid.Columns.Add("HorD", typeof(string));
            grid.Columns.Add("MinD", typeof(string));
            grid.Columns.Add("amD", typeof(string));
            grid.Columns.Add("IdCanal", typeof(string));
            grid.Columns.Add("IdGeneraEvento", typeof(string));
            grid.Columns.Add("GeneraEvento", typeof(string));
            grid.Columns.Add("GeneradorEvento", typeof(string));
            grid.Columns.Add("cuantiaperdida", typeof(string));
            grid.Columns.Add("FechaEvento", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("IdCadenaValor", typeof(string));
            grid.Columns.Add("IdMacroproceso", typeof(string));
            grid.Columns.Add("IdProceso", typeof(string));
            grid.Columns.Add("IdSubProceso", typeof(string));
            grid.Columns.Add("IdActividad", typeof(string));
            grid.Columns.Add("ResponsableEvento", typeof(string));
            grid.Columns.Add("ResponsableSolucion", typeof(string));
            grid.Columns.Add("IdClase", typeof(string));
            grid.Columns.Add("NombreClaseEvento", typeof(string));
            grid.Columns.Add("IdSubClase", typeof(string));
            grid.Columns.Add("NombreTipoPerdidaEvento", typeof(string));
            grid.Columns.Add("AfectaContinudad", typeof(string));
            grid.Columns.Add("IdEstado", typeof(string));
            grid.Columns.Add("Observaciones", typeof(string));
            grid.Columns.Add("ResponsableContabilidad", typeof(string));
            grid.Columns.Add("NombreResContabilidad", typeof(string));
            grid.Columns.Add("CuentaPUC", typeof(string));
            grid.Columns.Add("CuentaOrden", typeof(string));
            grid.Columns.Add("CuentaPerdida", typeof(string));
            grid.Columns.Add("Moneda1", typeof(string));
            grid.Columns.Add("TasaCambio1", typeof(string));
            grid.Columns.Add("ValorPesos1", typeof(string));
            grid.Columns.Add("ValorRecuperadoTotal", typeof(string));
            grid.Columns.Add("Moneda2", typeof(string));
            grid.Columns.Add("TasaCambio2", typeof(string));
            grid.Columns.Add("ValorPesos2", typeof(string));
            grid.Columns.Add("ValorRecuperadoSeguro", typeof(string));
            grid.Columns.Add("ValorPesos3", typeof(string));
            grid.Columns.Add("Recuperacion", typeof(string));
            grid.Columns.Add("ValorRecuperacion", typeof(string));
            grid.Columns.Add("IdLineaProceso", typeof(string));
            grid.Columns.Add("IdSubLineaProceso", typeof(string));
            grid.Columns.Add("MasLineas", typeof(string));
            grid.Columns.Add("NomGeneradorEvento", typeof(string));
            grid.Columns.Add("FechaContab", typeof(string));
            grid.Columns.Add("HoraContab", typeof(string));
            grid.Columns.Add("MinContab", typeof(string));
            grid.Columns.Add("amContab", typeof(string));
            grid.Columns.Add("fechaRecuperacion", typeof(string));
            grid.Columns.Add("cuantiaRecuperadaSeguros", typeof(string));
            grid.Columns.Add("cuantiaOtrasRecuperaciones", typeof(string));
            grid.Columns.Add("cuantiaNetaRecuperaciones", typeof(string));
            InfoGridEventos = grid;
            GridView1.DataSource = InfoGridEventos;
            GridView1.DataBind();
        }

        private void loadInfoEventos()
        {
            DataTable dtInfo = new DataTable();
            int IdUsuarioJerarquia = Convert.ToInt32(Session["IdJerarquia"].ToString());
            dtInfo = cEvento.loadInfoEventos(Sanitizer.GetSafeHtmlFragment(TextBox29.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox30.Text.Trim()), 
                DropDownList19.SelectedValue.ToString().Trim(), DropDownList20.SelectedValue.ToString().Trim(), DropDownList21.SelectedValue.ToString().Trim(), 
                DropDownList22.SelectedValue.ToString().Trim(), IdUsuarioJerarquia);



            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    TbEventos.Visible = true;
                    GridView1.Visible = true;
                    InfoGridEventos.Rows.Add(new Object[] {
                                                            dtInfo.Rows[rows]["IdEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdEmpresa"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdRegion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdPais"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdDepartamento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCiudad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdOficinaSucursal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["DetalleUbicacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["DescripcionEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdServicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubServicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraInicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaFinalizacion"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraFinalizacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaDescubrimiento"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraDescubrimiento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCanal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdGeneraEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["GeneraEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["GeneradorEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["cuantiaperdida"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCadenaValor"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdMacroproceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdActividad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableSolucion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdClase"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreClaseEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubClase"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreTipoPerdidaEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["AfectaContinudad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdEstado"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Observaciones"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableContabilidad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreResContabilidad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaPUC"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaOrden"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaPerdida"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Moneda1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["TasaCambio1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperadoTotal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Moneda2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["TasaCambio2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperadoSeguro"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos3"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Recuperacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdLineaProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubLineaProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MasLineas"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NomGeneradorEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HoraContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaRecup"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["CuantiaRecuperadaSeguro"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["cuantiaOtrasRecuperaciones"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuantiaNetaRecuperacion"].ToString().Trim()
                                                          });
                }

                GridView1.PageIndex = PagIndexInfoGridEventos;
                GridView1.DataSource = InfoGridEventos;
                GridView1.DataBind();
            }
            else
            {
                loadGridEventos();
                TbEventos.Visible = false;
                Mensaje("El usuario no tiene eventos reportados.");
            }
        }
        private void loadInfoEventosvsUsuario()
        {
            DataTable dtInfo = new DataTable();
            int IdUsuarioJerarquia = Convert.ToInt32(Session["IdJerarquia"].ToString());
            dtInfo = cEvento.loadInfoEventosvsUsuario(IdUsuarioJerarquia);

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    TbEventos.Visible = true;
                    InfoGridEventos.Rows.Add(new Object[] {
                                                            dtInfo.Rows[rows]["IdEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdEmpresa"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdRegion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdPais"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdDepartamento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCiudad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdOficinaSucursal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["DetalleUbicacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["DescripcionEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdServicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubServicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraInicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaFinalizacion"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraFinalizacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaDescubrimiento"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraDescubrimiento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCanal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdGeneraEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["GeneraEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["GeneradorEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["cuantiaperdida"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCadenaValor"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdMacroproceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdActividad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableSolucion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdClase"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreClaseEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubClase"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreTipoPerdidaEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["AfectaContinudad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdEstado"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Observaciones"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableContabilidad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreResContabilidad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaPUC"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaOrden"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaPerdida"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Moneda1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["TasaCambio1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperadoTotal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Moneda2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["TasaCambio2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperadoSeguro"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos3"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Recuperacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdLineaProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubLineaProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MasLineas"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NomGeneradorEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HoraContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amContab"].ToString().Trim()
                                                          });
                }

                GridView1.PageIndex = PagIndexInfoGridEventos;
                GridView1.DataSource = InfoGridEventos;
                GridView1.DataBind();
            }
            else
            {
                loadGridEventos();
                TbEventos.Visible = false;
                Mensaje("El usuario no tiene eventos reportados.");
            }
        }
        private void loadCodigoEvento()
        {
            DataTable dtInfo = new DataTable();

            try
            {
                dtInfo = cEvento.loadCodigoEvento();

                if (dtInfo.Rows.Count > 0)
                    Label55.Text = "E" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    Label55.Text = "E1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código evento. " + ex.Message);
            }
        }

        private void loadCodigoNHEvento()
        {
            DataTable dtInfo = new DataTable();

            try
            {
                dtInfo = cEvento.loadCodigoNHEvento();
                if (dtInfo.Rows.Count > 0)
                    Label113.Text = "NHE" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    Label113.Text = "NHE1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código evento. " + ex.Message);
            }
        }

        private void loadInfoPlanAccionEvento()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cEvento.loadInfoPlanAccionEvento(InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPlanAccionEvento.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlanAccion"].ToString().Trim(),
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

                GridView3.DataSource = InfoGridPlanAccionEvento;
                GridView3.DataBind();
            }
        }

        private void loadGridPlanAccionEvento()
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
            InfoGridPlanAccionEvento = grid;
            GridView3.DataSource = InfoGridPlanAccionEvento;
            GridView3.DataBind();
        }

        #endregion Loads

        #region Metodos usuario

        private void inicializarValores()
        {
            PagIndexInfoGridEventos = 0;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void agregarNHEvento()
        {
            strCodigoNHEvento = string.Empty;
            cEvento.agregarNHEvento(ref strCodigoNHEvento, ddlEmpresa.SelectedItem.Value);
        }

        private void agregarComentarioEvento()
        {
            cEvento.agregarComentarioEvento(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.ToString().Trim()), InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim());
        }

        private void agregarComentarioPlanAccion()
        {
            cEvento.agregarComentarioPlanAccion(Sanitizer.GetSafeHtmlFragment(TextBox38.Text.Trim()), InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdPlanAccion"].ToString().Trim());
        }

        private void cargardatosevento()
        {
            TbConEventos.Visible = true;
            TabContainerEventos.Tabs[1].Visible = true;
            TabContainerEventos.Tabs[2].Visible = true;
            ImageButton6.Visible = false;
            ImageButton8.Visible = true;
            trRiesgosEventos.Visible = true;
            //tab 1
            Label55.Text = InfoGridEventos.Rows[RowGridEventos]["CodigoEvento"].ToString().Trim();
            TextBox43.Text = InfoGridEventos.Rows[RowGridEventos]["DetalleUbicacion"].ToString().Trim();
            TextBox44.Text = InfoGridEventos.Rows[RowGridEventos]["DescripcionEvento"].ToString().Trim();
            //Fecha Inicio
            TextBox45.Text = InfoGridEventos.Rows[RowGridEventos]["FechaInicio"].ToString().Trim();

            for (int i = 0; i < DropDownList12.Items.Count; i++)
            {
                DropDownList12.SelectedIndex = i;
                if (DropDownList12.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["HorI"].ToString().Trim())
                    break;
                else
                    DropDownList12.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList13.Items.Count; i++)
            {
                DropDownList13.SelectedIndex = i;
                if (DropDownList13.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["MinI"].ToString().Trim())
                    break;
                else
                    DropDownList13.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList14.Items.Count; i++)
            {
                DropDownList14.SelectedIndex = i;
                if (DropDownList14.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["amI"].ToString().Trim())
                    break;
                else
                    DropDownList14.SelectedIndex = 0;
            }

            //Fecha Finalizacion
            TextBox47.Text = InfoGridEventos.Rows[RowGridEventos]["FechaFinalizacion"].ToString().Trim();
            for (int i = 0; i < DropDownList68.Items.Count; i++)
            {
                DropDownList68.SelectedIndex = i;
                if (DropDownList68.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["HorF"].ToString().Trim())
                    break;
                else
                    DropDownList68.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList69.Items.Count; i++)
            {
                DropDownList69.SelectedIndex = i;
                if (DropDownList69.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["MinF"].ToString().Trim())
                    break;
                else
                    DropDownList69.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList70.Items.Count; i++)
            {
                DropDownList70.SelectedIndex = i;
                if (DropDownList70.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["amF"].ToString().Trim())
                    break;
                else
                    DropDownList70.SelectedIndex = 0;
            }

            //Fecha Descubrimiento
            TextBox49.Text = InfoGridEventos.Rows[RowGridEventos]["FechaDescubrimiento"].ToString().Trim();
            for (int i = 0; i < DropDownList71.Items.Count; i++)
            {
                DropDownList71.SelectedIndex = i;
                if (DropDownList71.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["HorD"].ToString().Trim())
                    break;
                else
                    DropDownList71.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList72.Items.Count; i++)
            {
                DropDownList72.SelectedIndex = i;
                if (DropDownList72.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["MinD"].ToString().Trim())
                    break;
                else
                    DropDownList72.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList73.Items.Count; i++)
            {
                DropDownList73.SelectedIndex = i;
                if (DropDownList73.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["amD"].ToString().Trim())
                    break;
                else
                    DropDownList73.SelectedIndex = 0;
            }

            lblIdDependencia4.Text = InfoGridEventos.Rows[RowGridEventos]["GeneraEvento"].ToString().Trim();
            TextBox52.Text = InfoGridEventos.Rows[RowGridEventos]["cuantiaperdida"].ToString().Trim();
            TextBox39.Text = InfoGridEventos.Rows[RowGridEventos]["FechaEvento"].ToString().Trim();
            TextBox40.Text = InfoGridEventos.Rows[RowGridEventos]["Usuario"].ToString().Trim();

            for (int i = 0; i < ddlEmpresa1.Items.Count; i++)
            {
                ddlEmpresa1.SelectedIndex = i;
                if (ddlEmpresa1.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdEmpresa"].ToString().Trim())
                    break;
                else
                    ddlEmpresa1.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdRegion"].ToString().Trim())
                    break;
                else
                    DropDownList1.SelectedIndex = 0;
            }

            loadDDLPais(DropDownList1.SelectedValue.ToString().Trim(), 1);
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdPais"].ToString().Trim())
                    break;
                else
                    DropDownList2.SelectedIndex = 0;
            }

            loadDDLDepartamento(DropDownList2.SelectedValue.ToString().Trim(), 1);
            for (int i = 0; i < DropDownList3.Items.Count; i++)
            {
                DropDownList3.SelectedIndex = i;
                if (DropDownList3.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdDepartamento"].ToString().Trim())
                    break;
                else
                    DropDownList3.SelectedIndex = 0;
            }

            loadDDLCiudad(DropDownList3.SelectedValue.ToString().Trim(), 1);
            for (int i = 0; i < DropDownList4.Items.Count; i++)
            {
                DropDownList4.SelectedIndex = i;
                if (DropDownList4.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdCiudad"].ToString().Trim())
                    break;
                else
                    DropDownList4.SelectedIndex = 0;
            }

            loadDDLOficinaSucursal(DropDownList4.SelectedValue.ToString().Trim(), 1);
            for (int i = 0; i < DropDownList5.Items.Count; i++)
            {
                DropDownList5.SelectedIndex = i;
                if (DropDownList5.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdOficinaSucursal"].ToString().Trim())
                    break;
                else
                    DropDownList5.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList24.Items.Count; i++)
            {
                DropDownList24.SelectedIndex = i;
                if (DropDownList24.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdServicio"].ToString().Trim())
                    break;
                else
                    DropDownList24.SelectedIndex = 0;
            }

            if (DropDownList24.SelectedValue != "---")
            {
                loadDDLSubServicio(DropDownList24.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList25.Items.Count; i++)
                {
                    DropDownList25.SelectedIndex = i;
                    if (DropDownList25.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdSubServicio"].ToString().Trim())
                        break;
                    else
                        DropDownList25.SelectedIndex = 0;
                }
            }

            for (int i = 0; i < DropDownList26.Items.Count; i++)
            {
                DropDownList26.SelectedIndex = i;
                if (DropDownList26.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdCanal"].ToString().Trim())
                    break;
                else

                    DropDownList26.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList27.Items.Count; i++)
            {
                DropDownList27.SelectedIndex = i;
                if (DropDownList27.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdGeneraEvento"].ToString().Trim())
                    break;
                else
                    DropDownList27.SelectedIndex = 0;
            }

            //Muetsra la informacion del generador de evento
            if (InfoGridEventos.Rows[RowGridEventos]["IdGeneraEvento"].ToString().Trim() == "1")
            {
                lresponable.Visible = true;
                tresponsable.Visible = true;
                TextBox51.Enabled = false;
                imgDependencia4.Visible = true;
                TextBox51.Text = InfoGridEventos.Rows[RowGridEventos]["GeneradorEvento"].ToString().Trim();
            }
            else
            {
                lresponable.Visible = true;
                tresponsable.Visible = true;
                TextBox51.Enabled = true;
                imgDependencia4.Visible = false;
                TextBox51.Text = InfoGridEventos.Rows[RowGridEventos]["NomGeneradorEvento"].ToString().Trim();
            }

            //tab 2
            #region TAB 2 Datos Complementarios
            TextBox34.Text = InfoGridEventos.Rows[RowGridEventos]["ResponsableSolucion"].ToString().Trim();
            lblIdDependencia1.Text = InfoGridEventos.Rows[RowGridEventos]["ResponsableEvento"].ToString().Trim();
            for (int i = 0; i < DropDownList67.Items.Count; i++)
            {
                DropDownList67.SelectedIndex = i;
                if (DropDownList67.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdCadenaValor"].ToString().Trim())
                    break;
                else
                    DropDownList67.SelectedIndex = 0;
            }

            if (DropDownList67.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(DropDownList67.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList9.Items.Count; i++)
                {
                    DropDownList9.SelectedIndex = i;
                    if (DropDownList9.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdMacroproceso"].ToString().Trim())
                        break;
                    else
                        DropDownList9.SelectedIndex = 0;
                }
            }
            if (DropDownList9.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList9.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList10.Items.Count; i++)
                {
                    DropDownList10.SelectedIndex = i;
                    if (DropDownList10.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdProceso"].ToString().Trim())
                        break;
                    else
                        DropDownList10.SelectedIndex = 0;
                }
            }

            if (DropDownList10.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(DropDownList10.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList6.Items.Count; i++)
                {
                    DropDownList6.SelectedIndex = i;
                    if (DropDownList6.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdSubProceso"].ToString().Trim())
                        break;
                    else
                        DropDownList6.SelectedIndex = 0;
                }
            }

            if (DropDownList6.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLActividad(DropDownList6.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList11.Items.Count; i++)
                {
                    DropDownList11.SelectedIndex = i;
                    if (DropDownList11.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdActividad"].ToString().Trim())
                        break;
                    else
                        DropDownList11.SelectedIndex = 0;
                }
            }

            lblIdDependencia4.Text = InfoGridEventos.Rows[RowGridEventos]["GeneraEvento"].ToString().Trim();

            for (int i = 0; i < DropDownList33.Items.Count; i++)
            {
                DropDownList33.SelectedIndex = i;
                if (DropDownList33.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdClase"].ToString().Trim())
                    break;
                else
                    DropDownList33.SelectedIndex = 0;
            }

            if (DropDownList33.SelectedValue != "---")
            {
                loadDDLSubClaseRiesgo(DropDownList33.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList34.Items.Count; i++)
                {
                    DropDownList34.SelectedIndex = i;
                    if (DropDownList34.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdSubClase"].ToString().Trim())
                        break;
                    else
                        DropDownList34.SelectedIndex = 0;
                }
            }

            for (int i = 0; i < DropDownList8.Items.Count; i++)
            {
                DropDownList8.SelectedIndex = i;
                if (DropDownList8.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["NombreTipoPerdidaEvento"].ToString().Trim())
                    break;
                else
                    DropDownList8.SelectedIndex = 0;
            }

            //cargamos los los tipo y subtipos de lineas
            for (int i = 0; i < DropDownList23.Items.Count; i++)
            {
                DropDownList23.SelectedIndex = i;
                if (DropDownList23.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdLineaProceso"].ToString().Trim())
                    break;
                else
                    DropDownList23.SelectedIndex = 0;
            }

            if (DropDownList23.SelectedValue != "NULL")
            {
                loadDDLSubLineaNegocio(DropDownList23.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList29.Items.Count; i++)
                {
                    DropDownList29.SelectedIndex = i;
                    if (DropDownList29.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdSubLineaProceso"].ToString().Trim())
                        break;
                    else
                        DropDownList29.SelectedIndex = 0;
                }
            }

            string ff = InfoGridEventos.Rows[RowGridEventos]["AfectaContinudad"].ToString().Trim();
            if (ff == "True")
                ff = "1";
            else
                ff = "0";

            for (int i = 0; i < DropDownList15.Items.Count; i++)
            {
                DropDownList15.SelectedIndex = i;
                if (DropDownList15.SelectedValue.ToString().Trim() == ff)
                    break;
                else
                    DropDownList15.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList16.Items.Count; i++)
            {
                DropDownList16.SelectedIndex = i;
                if (DropDownList16.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdEstado"].ToString().Trim())
                    break;
                else
                    DropDownList16.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList74.Items.Count; i++)
            {
                DropDownList74.SelectedIndex = i;
                if (DropDownList74.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["Moneda1"].ToString().Trim())
                    break;
                else
                    DropDownList74.SelectedIndex = 0;
            }

            ff = InfoGridEventos.Rows[RowGridEventos]["Moneda2"].ToString().Trim();
            for (int i = 0; i < DropDownList75.Items.Count; i++)
            {
                DropDownList75.SelectedIndex = i;
                ff = DropDownList75.SelectedValue.ToString().Trim();
                if (DropDownList75.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["Moneda2"].ToString().Trim())
                    break;
                else
                    DropDownList75.SelectedIndex = 0;
            }

            ff = InfoGridEventos.Rows[RowGridEventos]["Recuperacion"].ToString().Trim();
            if (ff == "True")
            {
                ff = "1";
                lrecuperacio.Visible = true;
                trecuperacion.Visible = true;
            }
            else
            {
                ff = "0";
                lrecuperacio.Visible = false;
                trecuperacion.Visible = false;
            }

            for (int i = 0; i < DropDownList28.Items.Count; i++)
            {
                DropDownList28.SelectedIndex = i;
                if (DropDownList28.SelectedValue.ToString().Trim() == ff)
                    break;
                else
                    DropDownList28.SelectedIndex = 0;
            }

            TextBox53.Text = InfoGridEventos.Rows[RowGridEventos]["Observaciones"].ToString().Trim();
            TextBox17.Text = InfoGridEventos.Rows[RowGridEventos]["MasLineas"].ToString().Trim();

            if (TextBox17.Text != "")
                TrMasLNegocio.Visible = true;
            else
                TrMasLNegocio.Visible = false;

            #endregion TAB 2 Contabilizacion

            //tab 3
            #region TAB 3 Contabilizacion
            TextBox33.Text = InfoGridEventos.Rows[RowGridEventos]["NombreResContabilidad"].ToString().Trim();
            lblIdDependencia2.Text = InfoGridEventos.Rows[RowGridEventos]["ResponsableContabilidad"].ToString().Trim();
            TextBox14.Text = InfoGridEventos.Rows[RowGridEventos]["CuentaPUC"].ToString().Trim();
            TextBox15.Text = InfoGridEventos.Rows[RowGridEventos]["CuentaOrden"].ToString().Trim();
            TextBox16.Text = InfoGridEventos.Rows[RowGridEventos]["CuentaPerdida"].ToString().Trim();
            TextBox21.Text = InfoGridEventos.Rows[RowGridEventos]["TasaCambio1"].ToString().Trim();
            TextBox22.Text = InfoGridEventos.Rows[RowGridEventos]["ValorPesos1"].ToString().Trim();
            TextBox23.Text = InfoGridEventos.Rows[RowGridEventos]["ValorRecuperadoTotal"].ToString().Trim();
            TextBox25.Text = InfoGridEventos.Rows[RowGridEventos]["TasaCambio2"].ToString().Trim();
            TextBox26.Text = InfoGridEventos.Rows[RowGridEventos]["ValorPesos2"].ToString().Trim();
            TextBox27.Text = InfoGridEventos.Rows[RowGridEventos]["ValorRecuperadoSeguro"].ToString().Trim();
            TextBox28.Text = InfoGridEventos.Rows[RowGridEventos]["ValorPesos3"].ToString().Trim();
            TextBox46.Text = InfoGridEventos.Rows[RowGridEventos]["ValorRecuperacion"].ToString().Trim();

            //FechaContab
            TextBox1.Text = InfoGridEventos.Rows[RowGridEventos]["FechaContab"].ToString().Trim();

            //TextBox2.Text = string.Format("{0}:{1} {2}",
            //    InfoGridEventos.Rows[RowGridEventos]["HoraContab"].ToString().Trim(),
            //    InfoGridEventos.Rows[RowGridEventos]["MinContab"].ToString().Trim(),
            //    InfoGridEventos.Rows[RowGridEventos]["amContab"].ToString().Trim());

            //HoraContab
            for (int i = 0; i < DropDownList7.Items.Count; i++)
            {
                DropDownList7.SelectedIndex = i;
                if (DropDownList7.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["HoraContab"].ToString().Trim())
                    break;
                else
                    DropDownList7.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList30.Items.Count; i++)
            {
                DropDownList30.SelectedIndex = i;
                if (DropDownList30.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["MinContab"].ToString().Trim())
                    break;
                else
                    DropDownList30.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList31.Items.Count; i++)
            {
                DropDownList31.SelectedIndex = i;
                if (DropDownList31.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["amContab"].ToString().Trim())
                    break;
                else
                    DropDownList31.SelectedIndex = 0;
            }
            string[] fecha = InfoGridEventos.Rows[RowGridEventos]["fechaRecuperacion"].ToString().Split(' ');
            if(fecha.Length > 1)
            {
                string date = fecha[0].ToString();
                string[] horasV = fecha[1].ToString().Split(':');
                int hora = Convert.ToInt32(horasV[0].ToString());
                if (hora > 12)
                {
                    string horas = mtdRetornaHora(hora);
                    ddlHoraRecuperacion.SelectedValue = horas;
                    ddlHorarioRecuperacion.SelectedValue = "pm";
                }
                else
                {
                    ddlHoraRecuperacion.SelectedValue = hora.ToString();
                    ddlHorarioRecuperacion.SelectedValue = "am";
                }
                ddlMinutoRecuperacion.SelectedValue = horasV[1].ToString();
                DateTime dtFechaRep = Convert.ToDateTime(InfoGridEventos.Rows[RowGridEventos]["fechaRecuperacion"].ToString());
                txtFechaRecuperacion.Text = string.Format("{0:yyyy-M-d}", dtFechaRep);
            }
            
            
            txtCuantiaRecuperadaSeguros.Text = InfoGridEventos.Rows[RowGridEventos]["cuantiaRecuperadaSeguros"].ToString().Trim();
            txtCuantiaOtrasRecuperaciones.Text = InfoGridEventos.Rows[RowGridEventos]["cuantiaOtrasRecuperaciones"].ToString().Trim();
            txtCuantiaNetaRecuperaciones.Text = InfoGridEventos.Rows[RowGridEventos]["cuantiaNetaRecuperaciones"].ToString().Trim();
            #endregion TAB 3 Contabilizacion

            ///Validad si es nuevo para el envío de notificacioón
            if (!string.IsNullOrEmpty(lblIdDependencia1.Text.Trim()))
                lblExisteResponsableNotificacion.Text = "S";
        }
        public string mtdRetornaHora(int hora)
        {
            string rhora = string.Empty;
            if (hora == 13)
                rhora = "1";
            if (hora == 14)
                rhora = "2";
            if (hora == 15)
                rhora = "3";
            if (hora == 16)
                rhora = "4";
            if (hora == 17)
                rhora = "5";
            if (hora == 18)
                rhora = "6";
            if (hora == 19)
                rhora = "7";
            if (hora == 20)
                rhora = "8";
            if (hora == 21)
                rhora = "9";
            if (hora == 22)
                rhora = "10";
            if (hora == 23)
                rhora = "11";
            if (hora == 24)
                rhora = "12";
            return rhora;
        }
        private void relacionarRiesgoEvento()
        {
            if (validarRiesgo())
            {
                cEvento.relacionarRiesgoEvento(InfoGridConsultarRiesgos.Rows[RowGridConsultarRiesgos]["IdRiesgo"].ToString().Trim(), InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim());
                loadGridRiesgoEvento();
                loadInfoRiesgoEvento();
                GridView8.Visible = false;
                Mensaje("Asociación de Riesgo a Evento creada correctamente");
            }
            else
                Mensaje("El riesgo ya se encuentra relacionado.");
        }

        private bool validarRiesgo()
        {
            bool validar = true;

            for (int i = 0; i < InfoGridRiesgoEvento.Rows.Count; i++)
            {
                if (InfoGridRiesgoEvento.Rows[i]["IdRiesgo"].ToString().Trim() == InfoGridConsultarRiesgos.Rows[RowGridConsultarRiesgos]["IdRiesgo"].ToString().Trim())
                {
                    validar = false;
                    break;
                }
            }
            return validar;
        }
        private bool validarLimiteRiesgos(int IdRiesgo, int IdFrecuencia, int IdImpacto)
        {
            bool validar = false;
            DataTable dtInfo = cEvento.mtdTotalRiesgosxEvento(IdRiesgo);
            int CantRiesgoxEvento = Convert.ToInt32(dtInfo.Rows[0]["CantRiesgo"].ToString());
            dtInfo = cEvento.mtdLimiteRiesgosxEvento(IdFrecuencia);
            
            if (dtInfo.Rows.Count > 0)
            {
                int CantEventMax = Convert.ToInt32(dtInfo.Rows[0]["EventosMaximos"].ToString());
                if (CantRiesgoxEvento > CantEventMax)
                {
                    if (IdFrecuencia != 5)
                    {
                        IdFrecuencia++;
                        dtInfo = cRiesgo.calificacionInherente(IdFrecuencia.ToString(), IdImpacto.ToString());
                        string CodigoRiesgo = Session["CodigoRiesgo"].ToString();
                        string NombreRiesgoInherente = dtInfo.Rows[0]["NombreRiesgoInherente"].ToString().Trim();
                        cEvento.actualizarFrecuenciaRiesgo(IdRiesgo.ToString(), IdFrecuencia.ToString(), IdImpacto.ToString());
                        if (DropDownList27.SelectedValue == "1")
                            mtdGenerarNotificacion(IdRiesgo, Session["NombreRiesgo"].ToString(), Convert.ToInt32(Session["IdEvento"].ToString()), CodigoRiesgo);
                    }
                }
                validar = true;
            } 
            return validar;
        }
        private void mtdGenerarNotificacion(int IdRiesgo, string NombreRiesgo, int IdEvento, string CodigoRiesgo)
        {
            string TipoArea = cEvento.mtdGetTipoArea(lblIdDependencia4.Text.Trim());
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "MODIFICACION  DE RIESGO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + CodigoRiesgo + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + NombreRiesgo + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el cambio de Frecuencia-Cualitativa del Riesgo.<br>";
                TextoAdicional = TextoAdicional + " Fecha Modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario Modificación : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Modificación : " + Session["nombreUsuario"].ToString() + "<br>";

                boolEnviarNotificacion(19, IdRiesgo, Convert.ToInt16(lblIdDependencia4.Text.Trim()), System.DateTime.Now.ToString().Trim(), TextoAdicional);
                if(TipoArea != "0")
                    boolEnviarNotificacionRiesgos(19, IdRiesgo, Convert.ToInt16(lblIdDependencia4.Text.Trim()), System.DateTime.Now.ToString().Trim(), TextoAdicional, TipoArea);
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }
        private void mtdGenerarNotificacionNoHuboEvento(string CodNHE, string ResponsableUsuario, string Area, int IdEvento, int IdMailEvento)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "REGISTRO NO HUBO EVENTO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código del No Hubo Evento: " + CodNHE + "<br>";
                TextoAdicional = TextoAdicional + " Fecha del No Hubo Evento : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario de Registro : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Registro : " + Session["nombreUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Funcionario : " + ResponsableUsuario + "<br>";
                TextoAdicional = TextoAdicional + " Área Funcionario : " + Area + "<br>";

                boolEnviarNotificacion(IdMailEvento, IdEvento, Convert.ToInt16(Session["IdJerarquia"]), System.DateTime.Now.ToString().Trim(), TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }
        private void verComentarioEvento()
        {
            TextBox11.Text = InfoGridComentarioEvento.Rows[RowGridComentarioEvento]["Comentario"].ToString().Trim();
            TextBox11.ReadOnly = true;
            ImageButton17.Visible = true;
        }

        private void detallePlanAccionSeleccionado()
        {
            resetValuesEventoPlanAccion();
            trAddPlanAccion.Visible = true;
            ImageButton12.Visible = true;
            TextBox20.Text = InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["DescripcionAccion"].ToString().Trim();
            TextBox35.Text = InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["NombreHijo"].ToString().Trim();
            lblIdDependencia3.Text = InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["Responsable"].ToString().Trim();

            for (int i = 0; i < DropDownList17.Items.Count; i++)
            {
                DropDownList17.SelectedIndex = i;
                if (DropDownList17.SelectedValue.ToString().Trim() == InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdTipoRecursoPlanAccion"].ToString().Trim())
                    break;

            }
            TextBox36.Text = InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["ValorRecurso"].ToString().Trim();
            for (int i = 0; i < DropDownList18.Items.Count; i++)
            {
                DropDownList18.SelectedIndex = i;
                if (DropDownList18.SelectedValue.ToString().Trim() == InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdEstadoPlanAccion"].ToString().Trim())
                    break;

            }

            TextBox37.Text = InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["FechaCompromiso"].ToString().Trim();
            trAdjComPlaAcci.Visible = true;
        }

        private int mtdRegistrarPlanAccionEvento()
        {
            int intIdRegistro = 0;
            DataTable dtInfo = new DataTable();

            dtInfo = cEvento.mtdRegistrarPlanAccionEvento(InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox20.Text.Trim()),
                lblIdDependencia3.Text.Trim(), DropDownList17.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox36.Text.Trim()),
                DropDownList18.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox37.Text.Trim()) + " 12:00:00:000");

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intIdRegistro = Convert.ToInt32(dtInfo.Rows[0][0].ToString());
            }

            return intIdRegistro;
        }

        private void actualizarPlanAccionEvento()
        {
            cEvento.actualizarPlanAccionEvento(InfoGridPlanAccionEvento.Rows[RowGridPlanAccionEvento]["IdPlanAccion"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox20.Text.Trim()), lblIdDependencia3.Text.Trim(), DropDownList17.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox36.Text.Trim()), DropDownList18.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox37.Text.Trim()) + " 12:00:00:000");
        }

        private void verComentarioPlanAccion()
        {
            TextBox38.Text = InfoGridComentarioPlanAccion.Rows[RowGridComentarioPlanAccion]["Comentario"].ToString().Trim();
            TextBox38.ReadOnly = true;
            ImageButton11.Visible = true;
        }

        private void descargarArchivoPlanAccion()
        {
            string extension = Path.GetExtension(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim());
            if(extension == ".pdf")
            {
                Response.Clear();
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".pdf");
                Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                Response.End();
            }
            if(extension == ".xlsx")
            {
                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".xlsx");
                Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                Response.End();
            }
            if (extension == ".xls")
            {
                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename="+Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) +".xls");
                Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                Response.End();
            }
            if (extension == ".docx")
            {
                Response.Clear();
                Response.ContentType = "application/application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".docx");
                Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                Response.End();
            }
            if (extension == ".doc")
            {
                Response.Clear();
                Response.ContentType = "application/application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()) + ".doc");
                Response.TransmitFile(Server.MapPath("~/Archivos/PDFsPlanAccionEventos/" + InfoGridArchivoPlanAccion.Rows[RowGridArchivoPlanAccion]["UrlArchivo"].ToString().Trim()));
                Response.End();
            }
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = string.Empty, Copia = string.Empty, Asunto = string.Empty, Otros = string.Empty, Cuerpo = string.Empty, NroDiasRecordatorio = string.Empty;
            string selectCommand = string.Empty, AJefeInmediato = string.Empty, AJefeMediato = string.Empty, RequiereFechaCierre = string.Empty;
            string idJefeInmediato = string.Empty, idJefeMediato = string.Empty;
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
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
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
                #region Restro
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
                #endregion

                try
                {
                    #region Envio Correo
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
        private Boolean boolEnviarNotificacionRiesgos(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional, string TipoArea)
        {
            #region Variables
            bool err = false;
            string Destinatario = string.Empty, Copia = string.Empty, Asunto = string.Empty, Otros = string.Empty, Cuerpo = string.Empty, NroDiasRecordatorio = string.Empty;
            string selectCommand = string.Empty, AJefeInmediato = string.Empty, AJefeMediato = string.Empty, RequiereFechaCierre = string.Empty;
            string idJefeInmediato = string.Empty, idJefeMediato = string.Empty;
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
                        "WHERE JO.TipoArea = " + TipoArea;

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
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
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
                #region Restro
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
                #endregion

                try
                {
                    #region Envio Correo
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
        public void LimpiarTodo()
        {
            TbConEventos.Visible = false;
            RadioButtonList1.ClearSelection();
            reseteartodo();
            TabContainerEventos.Tabs[1].Visible = false;
            TabContainerEventos.Tabs[2].Visible = false;
            trRiesgosEventos.Visible = false;
        }

        #endregion Metodos usuario

        #region Textbox
        protected void TextBox52_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox52.Text));
                TextBox52.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox52.Text = "";
                TextBox52.Focus();
                Mensaje("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]");
            }
        }

        protected void TextBox21_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox21.Text));
                TextBox21.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox21.Text = "";
                TextBox21.Focus();
                Mensaje("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]");
            }
        }

        protected void TextBox22_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox22.Text));
                TextBox22.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox22.Text = "";
                TextBox22.Focus();
                Mensaje("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]");
            }
        }

        protected void TextBox23_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox23.Text));
                TextBox23.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox23.Text = "";
                TextBox23.Focus();
                Mensaje("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]");
            }
        }

        protected void TextBox25_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox25.Text));
                TextBox25.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox25.Text = "";
                TextBox25.Focus();
                Mensaje("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]");
            }
        }

        protected void TextBox26_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox26.Text));
                TextBox26.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox26.Text = "";
                TextBox26.Focus();
                Mensaje("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]");
            }
        }

        protected void TextBox27_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox27.Text));
                TextBox27.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox27.Text = "";
                TextBox27.Focus();
                Mensaje("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]");
            }
        }

        protected void TextBox28_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox28.Text));
                TextBox28.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox28.Text = "";
                TextBox28.Focus();
                Mensaje("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]");
            }
        }
        #endregion Textbox

        #region Gridviews

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            switch (e.CommandName)
            {
                case "Modificar":
                    RowGridEventos = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridEventos) + Convert.ToInt16(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[Convert.ToInt16(e.CommandArgument)];
                    var colsNoVisible = GridView1.DataKeys[Convert.ToInt16(e.CommandArgument)].Values;
                    Session["IdEvento"] = colsNoVisible[0].ToString();
                    reseteartodo();
                    resetValuesCamposEventos();
                    resetValuesRiesgoEventos();
                    resetValuesJustificacionEvento();
                    resetValuesEventoPlanAccion();
                    resetValuesJustificacionPlanAccion();

                    loadGridComentarioEvento();
                    loadInfoComentarioEvento();

                    loadGridArchivoEvento();
                    loadInfoArchivoEvento();

                    loadGridPlanAccionEvento();
                    loadInfoPlanAccionEvento();

                    cargardatosevento();

                    loadGridRiesgoEvento();
                    loadInfoRiesgoEvento();
                    loadGridConsultarRiesgos();

                    mtdLoadGridAudEventoRiesgo();
                    mtdLoadInfoAudEventoRiesgo();

                    #region Carga Riesgos Por Proceso
                    //loadGridConsultarRiesgos();
                    loadInfoConsultarRiesgosProceso();
                    #endregion Carga Riesgos Por Proceso
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridPlanAccionEvento = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    resetValuesEventoPlanAccion();
                    detallePlanAccionSeleccionado();
                    loadGridComentarioPlanAccion();
                    loadInfoComentarioPlanAccion();
                    loadGridArchivoPlanAccion();
                    loadInfoArchivoPlanAccion();
                    break;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoEvento = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdfEvento();
                    //descargarArchivoEvento();
                    break;
            }
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridComentarioEvento = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verComentarioEvento();
                    break;
            }
        }

        protected void GridView8_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridConsultarRiesgos = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GridView8.Rows[RowGridConsultarRiesgos];
            var colsNoVisible = GridView8.DataKeys[RowGridConsultarRiesgos].Values;
            string ListCausas = colsNoVisible[0].ToString();
            Session["IdRiesgo"]  = colsNoVisible[1].ToString();
            Session["CodigoRiesgo"] = row.Cells[0].Text;
            Session["IdProbabilidad"] = colsNoVisible[2].ToString();
            Session["IdImpacto"] = colsNoVisible[3].ToString();
            Session["NombreRiesgo"] = row.Cells[1].Text;
            switch (e.CommandName)
            {
                case "Relacionar":
                    if (ListCausas != "")
                    {
                        ListCausas = ListCausas.Replace("|", ",");
                        loadInfoConsultarRiesgosCausas();
                        loadInfoConsultarRiesgosCausas(ListCausas);
                        
                    }
                    modalPopup.Show();
                    break;
            }
        }
        protected void Bok_Click(object sender, EventArgs e)
        {
            int IdEvento = Convert.ToInt32(Session["IdEvento"].ToString());
            int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
            int IdFrecuencia = Convert.ToInt32(Session["IdProbabilidad"].ToString());
            int IdImpacto = Convert.ToInt32(Session["IdImpacto"].ToString());
            bool flag = false;
            bool eject = false;
            if (validarRiesgo())
            {
                if (CBriesgosSinCausa.Checked == true)
                {
                    relacionarRiesgoEvento();
                    eject = validarLimiteRiesgos(IdRiesgo, IdFrecuencia, IdImpacto);
                    if(eject == false)
                        Mensaje("Error en la validación de la frecuencia, revisar registros de Eventos Maximos por Riesgo.");
                        
                }
                else
                {
                    for (int rowIndex = 0; rowIndex < GVcausasRiesgos.Rows.Count; rowIndex++)
                    {
                        GridViewRow row = GVcausasRiesgos.Rows[rowIndex];
                        //GridViewRow previousRow = GVGestionCompetencias.Rows[rowIndex];
                        int IdCausa = Convert.ToInt32(row.Cells[0].Text);
                        CheckBox asociar = (CheckBox)row.FindControl("CBasociarCausa");
                        if (asociar.Checked == true)
                        {
                            flag = true;
                            cEvento.relacionarRiesgoEventoCausas(IdRiesgo, IdEvento, IdCausa);
                        }
                    }
                    modalPopup.Hide();
                    if (cCuenta.permisosAgregar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        if (flag == true)
                        {
                            relacionarRiesgoEvento();
                            eject = validarLimiteRiesgos(IdRiesgo, IdFrecuencia, IdImpacto);
                            if (eject == false)
                                Mensaje("Error en la validación de la frecuencia, revisar registros de Eventos Maximos por Riesgo.");
                                
                        }
                    }
                }
            }
            else
                Mensaje("El riesgo ya se encuentra relacionado.");
            #region Carga Riesgos Por Proceso
            //loadGridConsultarRiesgos();
            loadInfoConsultarRiesgosProceso();
            #endregion Carga Riesgos Por Proceso
            CBriesgosSinCausa.Checked = false;
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

        protected void GridView10_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoPlanAccion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    descargarArchivoPlanAccion();
                    break;
            }
        }
        #endregion Gridviews

        #region Eventos
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridEventos = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridEventos;
            GridView1.DataSource = InfoGridEventos;
            GridView1.DataBind();
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LimpiarTodo();
            if (RadioButtonList1.SelectedIndex == 0)
            {
                if (cCuenta.permisosConsulta(IdFormularioNohuboevent) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    TbConEventos.Visible = false;
                    TbNohuboeventos.Visible = true;
                    TextBox41.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    TextBox42.Text = Session["loginUsuario"].ToString().Trim();
                    ddlEmpresa.SelectedIndex = 0;
                    //loadCodigoNHEvento();
                }
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                if (cCuenta.permisosConsulta(IdFormularioCrearevento) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    TbNohuboeventos.Visible = false;
                    TbConEventos.Visible = true;
                    ImageButton6.Visible = true;
                    ImageButton8.Visible = false;
                    TextBox39.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    TextBox40.Text = Session["loginUsuario"].ToString().Trim();
                    //loadCodigoEvento();
                }
            }
        }

        protected void TabContainerEventos_ActiveTabChanged(object sender, EventArgs e)
        {
            int a = TabContainerEventos.ActiveTabIndex;
            TabContainerEventos.ActiveTabIndex = 0;

            if (a == 1)
            {
                if (cCuenta.permisosConsulta(IdFormularioDatosComple) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                    TabContainerEventos.ActiveTabIndex = 1;
            }

            if (a == 2)
            {
                if (cCuenta.permisosConsulta(IdFormularioContabiliza) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                    TabContainerEventos.ActiveTabIndex = 2;
            }
        }
        #endregion Eventos

        #region PDF
        private void loadFileEvento()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cControl.loadCodigoArchivoControl();

            if (dtInfo.Rows.Count > 0)
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim() + "-" + FileUpload1.FileName.ToString().Trim();
            else
                nameFile = "1-" + InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim() + "-" + FileUpload1.FileName.ToString().Trim();

            FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFsEventos/") + nameFile);
            cEvento.agregarArchivoEvento(InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim(), nameFile);
        }

        private void descargarArchivoEvento()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFsEventos/" + InfoGridArchivoEvento.Rows[RowGridArchivoEvento]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }

        private void mtdCargarPdfEvento()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty, strIdControl = "6";
            #endregion Vars

            dtInfo = cControl.loadCodigoArchivoControl();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}",
                    InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cEvento.mtdAgregarArchivoPdf(strIdControl, InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfEvento()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoEvento.Rows[RowGridArchivoEvento]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cEvento.mtdDescargarArchivoPdf(strNombreArchivo);
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
        #endregion PDF
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                string customerId = GridView2.DataKeys[e.Row.RowIndex].Value.ToString();
                int IdEvento = Convert.ToInt32(Session["IdEvento"].ToString());
                //int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
                GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
                gvOrders.DataSource = GetData(string.Format("SELECT Causa.IdCausas, Causa.NombreCausas"+
                " FROM[Riesgos].[EventosVsRiesgosCausas] as EVRC"+
                " inner join Parametrizacion.Causas as Causa on Causa.IdCausas = EVRC.IdCausa"+
                " where IdEvento = {0} and IdRiesgo = {1}", IdEvento, customerId));
                gvOrders.DataBind();
            }
        }
        private static DataTable GetData(string query)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (System.Data.DataSet ds = new System.Data.DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
    }
}