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
using AjaxControlToolkit;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Sarlaft
{
    public partial class RegistroOperacion : System.Web.UI.UserControl
    {
        string IdFormulario = "6005";
        string IdFormularioVerROI = "6010";
        string IdFormularioAPNG = "6011";
        private static int LastInsertIdCE;
        cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        cSegmentacion cSegmentacion = new cSegmentacion();
        cCuenta cCuenta = new cCuenta();
        //Variable definida para visualizar los botones de apobar y rechar ROI Seguros Estado
        string BtnsROISE = System.Configuration.ConfigurationManager.AppSettings["BtnsROISE"].ToString();

        #region Properties
        private DataTable infoGridArchivoRegistroOperacion;
        private DataTable InfoGridArchivoRegistroOperacion
        {
            get
            {
                infoGridArchivoRegistroOperacion = (DataTable)ViewState["infoGridArchivoRegistroOperacion"];
                return infoGridArchivoRegistroOperacion;
            }
            set
            {
                infoGridArchivoRegistroOperacion = value;
                ViewState["infoGridArchivoRegistroOperacion"] = infoGridArchivoRegistroOperacion;
            }
        }

        private int rowGridArchivoRegistroOperacion;
        private int RowGridArchivoRegistroOperacion
        {
            get
            {
                rowGridArchivoRegistroOperacion = (int)ViewState["rowGridArchivoRegistroOperacion"];
                return rowGridArchivoRegistroOperacion;
            }
            set
            {
                rowGridArchivoRegistroOperacion = value;
                ViewState["rowGridArchivoRegistroOperacion"] = rowGridArchivoRegistroOperacion;
            }
        }

        private DataTable infoGridRegistroOperacion;
        private DataTable InfoGridRegistroOperacion
        {
            get
            {
                infoGridRegistroOperacion = (DataTable)ViewState["infoGridRegistroOperacion"];
                return infoGridRegistroOperacion;
            }
            set
            {
                infoGridRegistroOperacion = value;
                ViewState["infoGridRegistroOperacion"] = infoGridRegistroOperacion;
            }
        }

        private int rowGridRegistroOperacion;
        private int RowGridRegistroOperacion
        {
            get
            {
                rowGridRegistroOperacion = (int)ViewState["rowGridRegistroOperacion"];
                return rowGridRegistroOperacion;
            }
            set
            {
                rowGridRegistroOperacion = value;
                ViewState["rowGridRegistroOperacion"] = rowGridRegistroOperacion;
            }
        }

        private DataTable infoGridComentarioRegistroOperacion;
        private DataTable InfoGridComentarioRegistroOperacion
        {
            get
            {
                infoGridComentarioRegistroOperacion = (DataTable)ViewState["infoGridComentarioRegistroOperacion"];
                return infoGridComentarioRegistroOperacion;
            }
            set
            {
                infoGridComentarioRegistroOperacion = value;
                ViewState["infoGridComentarioRegistroOperacion"] = infoGridComentarioRegistroOperacion;
            }
        }

        private int rowGridComentarioRegistroOperacion;
        private int RowGridComentarioRegistroOperacion
        {
            get
            {
                rowGridComentarioRegistroOperacion = (int)ViewState["rowGridComentarioRegistroOperacion"];
                return rowGridComentarioRegistroOperacion;
            }
            set
            {
                rowGridComentarioRegistroOperacion = value;
                ViewState["rowGridComentarioRegistroOperacion"] = rowGridComentarioRegistroOperacion;
            }
        }

        private string[] parametrosConsulta = new string[7];
        private string this[int index]
        {
            get
            {
                parametrosConsulta = (string[])ViewState["parametrosConsulta"];
                return parametrosConsulta[index];
            }
            set
            {
                if (ViewState["parametrosConsulta"] != null)
                {
                    parametrosConsulta = (string[])ViewState["parametrosConsulta"];
                }
                parametrosConsulta[index] = value;
                ViewState["parametrosConsulta"] = parametrosConsulta;
            }
        }

        private int pagIndexInfoGridRegistroOperacion;
        private int PagIndexInfoGridRegistroOperacion
        {
            get
            {
                pagIndexInfoGridRegistroOperacion = (int)ViewState["pagIndexInfoGridRegistroOperacion"];
                return pagIndexInfoGridRegistroOperacion;
            }
            set
            {
                pagIndexInfoGridRegistroOperacion = value;
                ViewState["pagIndexInfoGridRegistroOperacion"] = pagIndexInfoGridRegistroOperacion;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);
            scrtManager.RegisterPostBackControl(Button5);
            scrtManager.RegisterPostBackControl(VerAdjuntos);
            scrtManager.RegisterPostBackControl(GridView1);


            if (!Page.IsPostBack && !Page.IsCallback && !AjaxFileUpload1.IsInFileUploadPostBack)
            {
                loadDDLTipoRegistro();
                loadDDLEstado();
                loadDDLFactorRiesgo();
                LoadAreas();
                inicializarValores();
                PopulateTreeView();
                TxbFechaHoy.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (BtnsROISE == "S")
                TrBotonesSegurosEstado.Visible = true;
            else
                TrBotonesDefault.Visible = true;
        }

        #region TreeView
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView1.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional]";
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
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                TreeView1.Nodes.Add(newNode);
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
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox34.Text = TreeView1.SelectedNode.Text;
            lblIdDependencia1.Text = TreeView1.SelectedNode.Value;

        }
        #endregion TreeView

        #region Buttons

        protected void Button1_Click(object sender, EventArgs e)
        {
            EstadoAprobadoClic("4");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            EstadoCerradoClic("5");
        }

        public void EstadoCerradoClic(string Estado)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateEstadoRechazado(Estado);
                    resetValuesConsulta();
                    resetValuesResultadoConsulta();
                    resetValuesComentarios();
                    resetValuesAsignar();
                    resetValuesDocumentado();
                    resetValuesAprobadoRechazado();
                    resetValuesBtnsSE();
                    loadGridRegistroOperacion();
                    loadInfoRegistroOperacion();

                    Mensaje("Registro rechazado exitósamente.");

                    if (TreeView1.SelectedNode != null)
                        TreeView1.SelectedNode.Selected = false;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al rechazar el registro. " + ex.Message);
            }
 
        }

        public void EstadoAprobadoClic(string Estado)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateEstadoAprobado(Estado);
                    resetValuesConsulta();
                    resetValuesResultadoConsulta();
                    resetValuesComentarios();
                    resetValuesAsignar();
                    resetValuesDocumentado();
                    resetValuesAprobadoRechazado();
                    resetValuesBtnsSE();
                    loadGridRegistroOperacion();
                    loadInfoRegistroOperacion();

                    Mensaje("Registro aprobado exitósamente.");

                    if (TreeView1.SelectedNode != null)
                        TreeView1.SelectedNode.Selected = false;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al aprobar el registro. " + ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateEstadoDocumentado();
                    boolEnviarNotificacion(3, Convert.ToInt64(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim()), Convert.ToInt64(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["idNodoJerarquia"].ToString().Trim()), "", "Tipo de registro: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreTipoRegistro"].ToString().Trim() + "<br />Estado: Documentado<br />Identificación: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["Identificacion"].ToString().Trim() + "<br />Nombre y Apellido: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreApellido"].ToString().Trim() + "<br />Indicador: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreIndicador"].ToString().Trim() + "<br />Descripción: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["Indicador"].ToString().Trim() + "<br />Mensaje: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["MensajeCorreo"].ToString().Trim() + "<br />Comentario: " + Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()) + "<br />Fecha posible de solución: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["FechaPosibleSolucion"].ToString().Trim() + "<br /><br />");
                    resetValuesConsulta();
                    resetValuesResultadoConsulta();
                    resetValuesComentarios();
                    resetValuesAsignar();
                    resetValuesDocumentado();
                    resetValuesAprobadoRechazado();
                    loadGridRegistroOperacion();
                    loadInfoRegistroOperacion();
                    Mensaje("Documentación finalizada exitósamente.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar como documentado el registro. " + ex.Message);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()) == "" || Convert.ToInt64(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim().Replace("-", ""))) < Convert.ToInt64(DateTime.Now.Date.ToString("yyyy-MM-dd").Replace("-", "")))
                        Mensaje("Debe ingresar una fecha valida de posible solución.");
                    else
                    {
                        updateFechaSolucion();
                        updateUsuarioAsignado();
                        boolEnviarNotificacion(4, Convert.ToInt64(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim()), Convert.ToInt64(lblIdDependencia1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()) + " 12:00:00:000", "Tipo de registro: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreTipoRegistro"].ToString().Trim() + "<br />Estado: Asignado<br />Identificación: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["Identificacion"].ToString().Trim() + "<br />Nombre y Apellido: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreApellido"].ToString().Trim() + "<br />Indicador: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreIndicador"].ToString().Trim() + "<br />Descripción: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["Indicador"].ToString().Trim() + "<br />Mensaje: " + InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["MensajeCorreo"].ToString().Trim() + "<br />Comentario: " + Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()) + "<br />Fecha posible de solución: " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()) + "<br /><br />");
                        resetValuesConsulta();
                        resetValuesResultadoConsulta();
                        resetValuesComentarios();
                        resetValuesAsignar();
                        resetValuesDocumentado();
                        resetValuesAprobadoRechazado();
                        loadGridRegistroOperacion();
                        loadInfoRegistroOperacion();
                        Mensaje("Información asignada exitósamente.");
                    }
                }

                if (TreeView1.SelectedNode != null)
                    TreeView1.SelectedNode.Selected = false;
            }
            catch (Exception ex)
            {
                Mensaje("Error al asignar el registro. " + ex.Message);
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            bool booBuscar = true;
            try
            {
                if ((DropDownList3.SelectedValue.ToString().Trim() == "---") &&
                    (DropDownList4.SelectedValue.ToString().Trim() == "---") &&
                    (DropDownList5.SelectedValue.ToString().Trim() == "---") &&
                    (DropDownList6.SelectedValue.ToString().Trim() == "---") &&
                    (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()))) &&
                    (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()))) &&
                    (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()))))
                { booBuscar = false; }

                if (booBuscar)
                {
                    this[0] = "";
                    if (DropDownList3.SelectedValue.ToString().Trim() != "---")
                        this[0] = DropDownList3.SelectedValue.ToString().Trim();

                    this[1] = "";
                    if (DropDownList4.SelectedValue.ToString().Trim() != "---")
                        this[1] = DropDownList4.SelectedValue.ToString().Trim();

                    this[2] = "";
                    if (DropDownList5.SelectedValue.ToString().Trim() != "---")
                        this[2] = DropDownList5.SelectedValue.ToString().Trim();

                    this[3] = "";
                    if (DropDownList6.SelectedValue.ToString().Trim() != "---")
                        this[3] = DropDownList6.SelectedValue.ToString().Trim();

                    this[4] = Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim());
                    this[5] = "";
                    if (Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) != "")
                        this[5] = Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + " 00:00:00:000";

                    this[6] = "";
                    if (Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()) != "")
                        this[6] = Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()) + " 23:59:59:998";

                    //TextBox8.Text = string.Empty;
                    //DropDownList1.SelectedIndex = DropDownList1.Items.Count - 1; 
                    trArea.Visible = false;

                    loadGridRegistroOperacion();
                    loadInfoRegistroOperacion();

                    resetValuesConsulta();
                    inicializarValores();
                }
                else
                    Mensaje("Por favor ingresar información en los filtros. ");
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridRegistroOperacion, Response, "Registro operaciones");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            resetValuesConsulta();
            resetValuesResultadoConsulta();
            resetValuesComentarios();
            resetValuesAsignar();
            resetValuesDocumentado();
            resetValuesAprobadoRechazado();
            loadGridRegistroOperacion();
            inicializarValores();
            DropDownList1.SelectedIndex = DropDownList1.Items.Count - 1;
            TextBox8.Text = string.Empty;
            trArea.Visible = false;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesConsulta();
            resetValuesResultadoConsulta();
            resetValuesComentarios();
            resetValuesAsignar();
            resetValuesDocumentado();
            resetValuesAprobadoRechazado();
            loadGridRegistroOperacion();
            loadInfoRegistroOperacion();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el archivo. " + ex.Message);
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    agregarComentario();
                    resetValuesComentarios();
                    loadGridComentarioRegistroOperacion();
                    loadInfoComentarioRegistroOperacion();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el comentario. " + ex.Message);
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesComentarios();
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateFechaSolucion();
                    resetValuesConsulta();
                    resetValuesResultadoConsulta();
                    resetValuesComentarios();
                    resetValuesAsignar();
                    resetValuesDocumentado();
                    resetValuesAprobadoRechazado();
                    loadGridRegistroOperacion();
                    loadInfoRegistroOperacion();
                    Mensaje("Información actualizada exitósamente.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la fecha de solución. " + ex.Message);
            }
        }

        protected void VerAdjuntos_Click(object sender, EventArgs e)
        {
            mtdLoadArchivoRO();
        }

        #endregion Buttons

        #region Gridviews
        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridRegistroOperacion = (Convert.ToInt32(GridView3.PageSize) * PagIndexInfoGridRegistroOperacion) + Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    modificarRegistroOperacion();
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridComentarioRegistroOperacion = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verComentario();
                    break;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoRegistroOperacion = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdfRegOperacion();
                    break;
            }
        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridRegistroOperacion = e.NewPageIndex;
            GridView3.PageIndex = PagIndexInfoGridRegistroOperacion;
            GridView3.DataSource = InfoGridRegistroOperacion;
            GridView3.DataBind();
        }
        #endregion Gridviews

        #region DDL
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList3.SelectedItem.ToString().Trim() == "ROI Manual")
            {
                trArea.Visible = true;
                DropDownList1.SelectedIndex = DropDownList1.Items.Count - 1;
            }
            else
                trArea.Visible = false;

        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            TextBox8.Text = Sanitizer.GetSafeHtmlFragment(DropDownList1.SelectedValue);
            
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "---"));

            if (DropDownList5.SelectedValue.ToString().Trim() != "---")
                loadDDLIndicador(DropDownList5.SelectedValue.ToString().Trim());
        }
        #endregion

        #region DataSource
        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }
        #endregion

        protected void TextBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList1.SelectedValue = Sanitizer.GetSafeHtmlFragment(TextBox8.Text.Trim());
            }   
            catch
            {
                DropDownList1.SelectedIndex = DropDownList1.Items.Count - 1;
                Mensaje("No existe Área para el código ingresado");
            }
        }

        #region

        #region Loads
        private void loadDDLTipoRegistro()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRegistroOperacion.loadDDLTipoRegistro();
                DropDownList3.Items.Insert(0, new ListItem("---", "---"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList3.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreTipoRegistro"].ToString().Trim(), dtInfo.Rows[i]["IdTipoRegistro"].ToString()));
                }
                DropDownList3.SelectedIndex = DropDownList3.Items.Count;
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los tipos de registro. " + ex.Message);
            }
        }

        private void LoadAreas()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = cRegistroOperacion.LoadAreas();
                DropDownList1.Items.Insert(0, new ListItem("---", "---"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dt.Rows[i]["NombreArea"].ToString().Trim(), dt.Rows[i]["Codigo"].ToString()));
                }
                DropDownList1.SelectedIndex = DropDownList1.Items.Count;
            }
            catch
            {
            }

        }

        private void loadDDLEstado()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRegistroOperacion.loadDDLEstado();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList4.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreEstado"].ToString().Trim(), dtInfo.Rows[i]["IdEstadoOperacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los estados. " + ex.Message);
            }
        }

        private void loadDDLFactorRiesgo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cSegmentacion.loadInfoFactorRiesgo();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList5.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los factores de riesgo. " + ex.Message);
            }
        }

        private void loadDDLIndicador(String IdFactorRiesgo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cSegmentacion.loadInfoSenalAlerta(IdFactorRiesgo);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList6.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["SenalAlerta"].ToString().Trim(), dtInfo.Rows[i]["IdIndicador"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los indicadores/señales de alerta. " + ex.Message);
            }
        }

        #region Archivo Reg. Operac
        private void mtdLoadArchivoRO()
        {
            mtdLoadGridArchivoRO();
            mtdLoadInfoArchivoRO();
        }

        private void mtdLoadInfoArchivoRO()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRegistroOperacion.loadInfoGridArchivoRegistroOperacion(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoRegistroOperacion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                        });
                }

                GridView1.DataSource = InfoGridArchivoRegistroOperacion;
                GridView1.DataBind();
            }
        }

        private void mtdLoadGridArchivoRO()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoRegistroOperacion = grid;

            GridView1.DataSource = InfoGridArchivoRegistroOperacion;
            GridView1.DataBind();
        }
        #endregion

        private void loadInfoRegistroOperacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRegistroOperacion.loadInfoRegistroOperacion(this[0].ToString().Trim(), this[1].ToString().Trim(),
                this[2].ToString().Trim(), this[3].ToString().Trim(), this[4].ToString().Trim(), this[5].ToString().Trim(),

                this[6].ToString().Trim(), Page.User.Identity.Name.ToString().Trim(), DropDownList1.SelectedValue.ToString().Trim(), cCuenta.permisosConsulta(IdFormularioVerROI));

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRegistroOperacion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdRegistroOperacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreEstado"].ToString().Trim(),
                        dtInfo.Rows[rows]["UsuarioAsignado"].ToString().Trim(),
                        dtInfo.Rows[rows]["idNodoJerarquia"].ToString().Trim(),
                        dtInfo.Rows[rows]["Identificacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreIndicador"].ToString().Trim(),
                        dtInfo.Rows[rows]["Indicador"].ToString().Trim(),
                        dtInfo.Rows[rows]["MensajeCorreo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaDeteccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaPosibleSolucion"].ToString().Trim(),
                        dtInfo.Rows[rows]["RegistrosCargue"].ToString().Trim(),
                        dtInfo.Rows[rows]["RegistrosOperacion"].ToString().Trim()
                    });
                }

                GridView3.PageIndex = PagIndexInfoGridRegistroOperacion;
                GridView3.DataSource = InfoGridRegistroOperacion;
                GridView3.DataBind();
                Button6.Visible = true;
            }
        }
        private void loadInfoComentarioRegistroOperacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRegistroOperacion.loadInfoComentarioRegistroOperacion(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioRegistroOperacion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["ComentarioCorto"].ToString().Trim(),
                        dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                        });
                }
                GridView2.DataSource = InfoGridComentarioRegistroOperacion;
                GridView2.DataBind();
            }
        }

        private void loadGridRegistroOperacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRegistroOperacion", typeof(string));
            grid.Columns.Add("NombreTipoRegistro", typeof(string));
            grid.Columns.Add("NombreEstado", typeof(string));
            grid.Columns.Add("UsuarioAsignado", typeof(string));
            grid.Columns.Add("idNodoJerarquia", typeof(string));
            grid.Columns.Add("Identificacion", typeof(string));
            grid.Columns.Add("NombreApellido", typeof(string));
            grid.Columns.Add("NombreIndicador", typeof(string));
            grid.Columns.Add("Indicador", typeof(string));
            grid.Columns.Add("MensajeCorreo", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("FechaDeteccion", typeof(string));
            grid.Columns.Add("FechaPosibleSolucion", typeof(string));
            grid.Columns.Add("RegistrosCargue", typeof(string));
            grid.Columns.Add("RegistrosOperacion", typeof(string));
            InfoGridRegistroOperacion = grid;
            GridView3.DataSource = InfoGridRegistroOperacion;
            GridView3.DataBind();
        }

        private void loadGridComentarioRegistroOperacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("ComentarioCorto", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            InfoGridComentarioRegistroOperacion = grid;
            GridView2.DataSource = InfoGridComentarioRegistroOperacion;
            GridView2.DataBind();
        }
        #endregion Loads
        private void inicializarValores()
        {
            PagIndexInfoGridRegistroOperacion = 0;
            DropDownList3.SelectedIndex = 4;
            DropDownList4.SelectedIndex = 5;
        }

        private void modificarRegistroOperacion()
        {
            //TrBotonesSegurosEstado.Visible = true;
            TrBotonesSegurosEstadoAutorizado.Visible = false;
            TrBotonesSegurosEstadoNegado.Visible = false;
            
            VerAdjuntos.Enabled = true;
            AjaxFileUpload1.Enabled = true;

            tbConsulta.Visible = false;
            tbResultadoConsulta.Visible = true;

            Label12.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreTipoRegistro"].ToString().Trim();
            Label14.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreEstado"].ToString().Trim();
            Label16.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["UsuarioAsignado"].ToString().Trim();
            TextBox34.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["UsuarioAsignado"].ToString().Trim();
            lblIdDependencia1.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["idNodoJerarquia"].ToString().Trim();
            Label18.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["Identificacion"].ToString().Trim();
            Label20.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreApellido"].ToString().Trim();
            Label22.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreIndicador"].ToString().Trim();
            TextBox3.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["Indicador"].ToString().Trim();
            TextBox4.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["MensajeCorreo"].ToString().Trim();
            Label28.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["FechaRegistro"].ToString().Trim();
            Label30.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["FechaDeteccion"].ToString().Trim();
            TextBox2.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["FechaPosibleSolucion"].ToString().Trim();
            Label38.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["RegistrosCargue"].ToString().Trim();
            Label40.Text = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["RegistrosOperacion"].ToString().Trim();
            Session["IdRO"] = InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim();

            loadGridComentarioRegistroOperacion();
            loadInfoComentarioRegistroOperacion();
            mtdLoadArchivoRO();
            validatePerfil();

        }

        #region UpdatesButton8_Click1
        private void updateEstadoAprobado(string Estado)
        {
            cRegistroOperacion.updateEstadoAprobado(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["NombreTipoRegistro"].ToString().Trim(),
                InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(), Estado);
        }

        private void updateEstadoRechazado(string Estado)
        {
            cRegistroOperacion.updateEstadoRechazado(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),Estado);
            cRegistroOperacion.agregarComentario(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
                "Cambio de estado a Rechazado.");
        }

        private void updateEstadoDocumentado()
        {
            cRegistroOperacion.updateEstadoDocumentado(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim());
            cRegistroOperacion.agregarComentario(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
                "Cambio de estado a Documentado.");
        }

        private void updateUsuarioAsignado()
        {
            cRegistroOperacion.updateUsuarioAsignado(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
                lblIdDependencia1.Text.Trim());
            cRegistroOperacion.agregarComentario(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
                "Asignado a " + Sanitizer.GetSafeHtmlFragment(TextBox34.Text.Trim()) + ". Cambio de estado a Asignado.");
        }

        private void updateFechaSolucion()
        {
            if (Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()) != "")
                cRegistroOperacion.updateFechaSolucion(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()) + " 12:00:00:000");
        }
        #endregion

        private void agregarComentario()
        {
            if (Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()) != "")
                cRegistroOperacion.agregarComentario(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()));
        }

        private void verComentario()
        {
            TextBox1.Text = InfoGridComentarioRegistroOperacion.Rows[RowGridComentarioRegistroOperacion]["Comentario"].ToString().Trim();
            TextBox1.ReadOnly = true;
            ImageButton4.Visible = true;
            ImageButton3.Visible = false;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
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

        private void validatePerfil()
        {
            string APNG= cCuenta.permisosConsulta(IdFormularioAPNG);
            //if (Page.User.Identity.Name.ToString().Trim() == "Oficial de cumplimiento" || Page.User.Identity.Name.ToString().Trim() == "Analista de cumplimiento" || Page.User.Identity.Name.ToString().Trim() == "Gestor Sarlaft")
            if(APNG != "False")
            {
                TextBox2.Enabled = true;
                TextBox2.ReadOnly = false;
                ImageButton5.Visible = true;
                tbAsignar.Visible = true;
                tbDocumentado.Visible = true;
                tbAprobadoRechazado.Visible = true;
            }
            else
            {
                TextBox2.Enabled = false;
                TextBox2.ReadOnly = true;
                ImageButton5.Visible = false;
                tbAsignar.Visible = false;
                tbDocumentado.Visible = true;
                tbAprobadoRechazado.Visible = false;
            }
        }

        private Boolean boolEnviarNotificacion(Int64 idEvento, Int64 idRegistro, Int64 idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion

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
                        Cuerpo = textoAdicional + row["Cuerpo"].ToString().Trim();
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
                    omb.ShowMessage("Error en el envio de la notificacion." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
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

        #region Resets
        private void resetValuesConsulta()
        {
            tbConsulta.Visible = true;
            DropDownList3.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;
            DropDownList5.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            TextBox8.Text = string.Empty;
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "---"));
            TextBox5.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox7.Text = string.Empty;
            Button6.Visible = false;
            //IdRegistro = string.Empty;
            //lblIdProceso.Text = string.Empty;
        }

        private void resetValuesResultadoConsulta()
        {
            tbResultadoConsulta.Visible = false;
            Label12.Text = "";
            Label14.Text = "";
            Label16.Text = "";
            Label18.Text = "";
            Label20.Text = "";
            Label22.Text = "";
            Label28.Text = "";
            Label30.Text = "";
            Label38.Text = "";
            Label40.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox2.Enabled = false;
            TextBox2.ReadOnly = true;
            ImageButton5.Visible = false;
        }

        private void resetValuesComentarios()
        {
            TextBox1.Text = "";
            TextBox1.ReadOnly = false;
            ImageButton4.Visible = false;
            ImageButton3.Visible = true;
        }

        private void resetValuesAsignar()
        {
            tbAsignar.Visible = false;
            TextBox34.Text = "";
            lblIdDependencia1.Text = "";
        }

        private void resetValuesDocumentado()
        {
            tbDocumentado.Visible = false;
        }

        private void resetValuesAprobadoRechazado()
        {
            tbAprobadoRechazado.Visible = false;
        }

        private void resetValuesBtnsSE()
        {
            TrBotonesSegurosEstadoAutorizado.Visible = false;
            TrBotonesSegurosEstadoAutorizadoAmpliado.Visible = false;
            TrBotonesSegurosEstadoNegado.Visible = false;
            TrBotonesSegurosEstadoNegadoAmpliado.Visible = false;
        }

        #endregion Resets

        #region PDFs

        //private void loadFile()
        //{
        //    string nameFile;
        //    DataTable dtInfo = new DataTable();

        //    dtInfo = cRegistroOperacion.loadCodigoArchivoRegistroOperacion();

        //if (dtInfo.Rows.Count > 0)
        //    nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" +
        //        InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim() + "-" +
        //        FileUpload1.FileName.ToString().Trim();
        //else
        //    nameFile = "1-" + 
        //        InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim() + "-" + 
        //        FileUpload1.FileName.ToString().Trim();

        //FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFsRegistroOperacion/") + nameFile);
        //cRegistroOperacion.agregarArchivo(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(), nameFile);
        //}

        //private void descargarArchivo()
        //{
        //    Response.Clear();
        //    Response.ContentType = "Application/pdf";
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
        //    Response.TransmitFile(Server.MapPath("~/Archivos/PDFsRegistroOperacion/" +
        //        InfoGridArchivoRegistroOperacion.Rows[RowGridArchivoRegistroOperacion]["UrlArchivo"].ToString().Trim()));
        //    Response.End();
        //}

        //private void mtdCargarPdfRegOperacion()
        //{
        //    #region Vars
        //    DataTable dtInfo = new DataTable();
        //    string strNombreArchivo = string.Empty;
        //    #endregion Vars

        //    dtInfo = cRegistroOperacion.loadCodigoArchivoRegistroOperacion();

        //    #region Nombre Archivo
        //    if (dtInfo.Rows.Count > 0)
        //        strNombreArchivo = string.Format("{0}-{1}-{2}",
        //            dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
        //            InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
        //            FileUpload1.FileName.ToString().Trim());
        //    else
        //        strNombreArchivo = string.Format("1-{0}-{1}",
        //            InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
        //            FileUpload1.FileName.ToString().Trim());
        //    #endregion Nombre Archivo

        //    #region Archivo
        //    Stream fs = FileUpload1.PostedFile.InputStream;
        //    BinaryReader br = new BinaryReader(fs);
        //    Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
        //    #endregion Archivo

        //    cRegistroOperacion.mtdAgregarArchivoPdf(InfoGridRegistroOperacion.Rows[RowGridRegistroOperacion]["IdRegistroOperacion"].ToString().Trim(),
        //        strNombreArchivo, bPdfData);
        //}

        private void mtdDescargarPdfRegOperacion()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoRegistroOperacion.Rows[RowGridArchivoRegistroOperacion]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cRegistroOperacion.mtdDescargarArchivoPdf(strNombreArchivo);
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

        protected void AjaxFileUploadEvent(object sender, AjaxFileUploadEventArgs e)
        {
            try
            {
                string filename = System.IO.Path.GetFileName(e.FileName);
                byte[] pdf = e.GetContents();
                string strIdRegistro = Session["IdRO"].ToString().Trim();
                mtdCargarPdfRegOperacionMultiples(filename, pdf, strIdRegistro);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message);
            }
        }

        private void mtdCargarPdfRegOperacionMultiples(string NomArchivo, byte[] bPdfData, string strIdRegistro)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cRegistroOperacion.loadCodigoArchivoRegistroOperacion();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    strIdRegistro.Trim(),
                    NomArchivo.ToString());
            else
                strNombreArchivo = string.Format("1-{0}-{1}",
                    strIdRegistro.Trim(),
                    NomArchivo.ToString());
            #endregion Nombre Archivo

            cRegistroOperacion.mtdAgregarArchivoPdf(strIdRegistro, strNombreArchivo, bPdfData);
        }

        #endregion PDFs

        protected void BtnAutorizado_Click(object sender, EventArgs e)
        {
            TrBotonesSegurosEstadoNegado.Visible = false;
            TrBotonesSegurosEstadoAutorizado.Visible = true;
            TrBotonesSegurosEstadoAutorizadoAmpliado.Visible = false;
            TrBotonesSegurosEstadoNegadoAmpliado.Visible = false;
            BtnAutorizadoCerrado.Visible = true;
        }
        

        protected void BtnNegado_Click(object sender, EventArgs e)
        {
            TrBotonesSegurosEstadoAutorizado.Visible = false;
            TrBotonesSegurosEstadoNegado.Visible = true;
            TrBotonesSegurosEstadoAutorizadoAmpliado.Visible = false;
            TrBotonesSegurosEstadoNegadoAmpliado.Visible = false;
            BtnNegadoCerrado.Visible = true;
        }
        #endregion

        protected void BtnAutorizadoAmpliado_Click(object sender, EventArgs e)
        {
            BtnAutorizadoCerrado.Visible = false;
            TrBotonesSegurosEstadoAutorizadoAmpliado.Visible = true;
            TrBotonesSegurosEstadoNegadoAmpliado.Visible = false;
        }

        protected void BtnNegadoAmpliado_Click(object sender, EventArgs e)
        {
            TrBotonesSegurosEstadoNegadoAmpliado.Visible = true;
            TrBotonesSegurosEstadoAutorizadoAmpliado.Visible = false;
            BtnNegadoCerrado.Visible = false;
        }

        
        protected void BtnNegadoCerrado_Click(object sender, EventArgs e)
        {
            EstadoCerradoClic("11");
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            EstadoCerradoClic("10");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            EstadoCerradoClic("9");
        }

        protected void BtnAutorizadoCerrado_Click(object sender, EventArgs e)
        {
            EstadoAprobadoClic("8");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            EstadoAprobadoClic("7");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            EstadoAprobadoClic("6");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}