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

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class Cumplimiento : System.Web.UI.UserControl
    {
        string IdFormulario = "5002";
        cRiesgo cRiesgo = new cRiesgo();
        cControl cControl = new cControl();
        cCuenta cCuenta = new cCuenta();
        private static int LastInsertIdCE;
        private static int NuevaLegislacion = 0;
        private static string LastLegislacion = string.Empty;

        #region Properties
        private DataTable infoGridLegislacion;
        private DataTable InfoGridLegislacion
        {
            get
            {
                infoGridLegislacion = (DataTable)ViewState["infoGridLegislacion"];
                return infoGridLegislacion;
            }
            set
            {
                infoGridLegislacion = value;
                ViewState["infoGridLegislacion"] = infoGridLegislacion;
            }
        }

        private int rowGridLegislacion;
        private int RowGridLegislacion
        {
            get
            {
                rowGridLegislacion = (int)ViewState["rowGridLegislacion"];
                return rowGridLegislacion;
            }
            set
            {
                rowGridLegislacion = value;
                ViewState["rowGridLegislacion"] = rowGridLegislacion;
            }
        }

        private DataTable infoGridArchivoLegislacion;
        private DataTable InfoGridArchivoLegislacion
        {
            get
            {
                infoGridArchivoLegislacion = (DataTable)ViewState["infoGridArchivoLegislacion"];
                return infoGridArchivoLegislacion;
            }
            set
            {
                infoGridArchivoLegislacion = value;
                ViewState["infoGridArchivoLegislacion"] = infoGridArchivoLegislacion;
            }
        }

        private int rowGridArchivoLegislacion;
        private int RowGridArchivoLegislacion
        {
            get
            {
                rowGridArchivoLegislacion = (int)ViewState["rowGridArchivoLegislacion"];
                return rowGridArchivoLegislacion;
            }
            set
            {
                rowGridArchivoLegislacion = value;
                ViewState["rowGridArchivoLegislacion"] = rowGridArchivoLegislacion;
            }
        }

        private DataTable infoGridComentarioLegislacion;
        private DataTable InfoGridComentarioLegislacion
        {
            get
            {
                infoGridComentarioLegislacion = (DataTable)ViewState["infoGridComentarioLegislacion"];
                return infoGridComentarioLegislacion;
            }
            set
            {
                infoGridComentarioLegislacion = value;
                ViewState["infoGridComentarioLegislacion"] = infoGridComentarioLegislacion;
            }
        }

        private int rowGridComentarioLegislacion;
        private int RowGridComentarioLegislacion
        {
            get
            {
                rowGridComentarioLegislacion = (int)ViewState["rowGridComentarioLegislacion"];
                return rowGridComentarioLegislacion;
            }
            set
            {
                rowGridComentarioLegislacion = value;
                ViewState["rowGridComentarioLegislacion"] = rowGridComentarioLegislacion;
            }
        }

        private int pagIndexInfoGridCumplimiento;
        private int PagIndexInfoGridCumplimiento
        {
            get
            {
                pagIndexInfoGridCumplimiento = (int)ViewState["pagIndexInfoGridCumplimiento"];
                return pagIndexInfoGridCumplimiento;
            }
            set
            {
                pagIndexInfoGridCumplimiento = value;
                ViewState["pagIndexInfoGridCumplimiento"] = pagIndexInfoGridCumplimiento;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);
            scrtManager.RegisterPostBackControl(ImageButton7);
            scrtManager.RegisterPostBackControl(GridView3);

            if (!Page.IsPostBack)
            {
                inicializarValores();
                loadDDLTipoLegislacion();
                loadDDLEstadoLegislacion();
                loadGridLegislacion();
                loadInfoLegislacion();
                PopulateTreeView();
            }
        }

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView1.ExpandAll();
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

        }
        #endregion

        #region Buttons
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                NuevaLegislacion = 1;
                resetValues();
                resetValuesArchivoComentario();
                loadCodigoLegislacion();
                tbCampos.Visible = true;
                ImageButton4.Visible = true;
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
                    cRiesgo.registrarLegislacion(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()) + " 12:00:00:000", DropDownList7.SelectedValue.ToString().Trim(), lblIdDependencia1.Text.Trim());
                    GetLastLegislacion();

                    if (lblIdDependencia1.Text.Trim() != "0")
                        boolEnviarNotificacion(5, Convert.ToInt16("0"), Convert.ToInt16(lblIdDependencia1.Text.Trim()), "", "Código de la Legislación: " + LastLegislacion + "<br />Nombre de la Legislación: " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()) + "<br />Vigencia Hasta: " + Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + "<br /><br />");

                    resetValues();
                    loadGridLegislacion();
                    loadInfoLegislacion();
                    Mensaje("La Legislación [" + LastLegislacion + "] fue registrada con éxito");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar legislación. " + ex.Message);
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    cRiesgo.actualizarLegislacion(InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()) + " 12:00:00:000", DropDownList7.SelectedValue.ToString().Trim(), lblIdDependencia1.Text.Trim());
                    if (lblIdDependencia1.Text.Trim() != "0" && DropDownList7.SelectedItem.ToString().Trim() == "Cerrada")
                        boolEnviarNotificacion(5, Convert.ToInt16(InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim()), Convert.ToInt16(lblIdDependencia1.Text.Trim()), "", "Código de la Legislación: " + Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()) + "<br />Nombre de la Legislación: " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()) + "<br />Vigencia Hasta: " + Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()) + "<br />Fecha de cierre: " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()) + "<br /><br />");

                    agregarComentarioLegislacion();
                    resetValues();
                    resetValuesArchivoComentario();
                    loadGridLegislacion();
                    loadInfoLegislacion();
                    Mensaje("Legislación modificada con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar legislación. " + ex.Message);
            }
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
            loadGridLegislacion();
            loadInfoLegislacion();
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            switch (lbldummyOkNo.Text.Trim())
            {
                case "Eliminar legislación":
                    try
                    {
                        borrarLegislacion();
                        Mensaje("Legislación eliminada con éxito.");
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error al eliminar la información. " + ex.Message);
                    }
                    break;
            }
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
                        /*if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {*/
                            mtdCargarPdfLegislacion();
                            loadGridArchivoLegislacion();
                            loadInfoArchivoLegislacion();
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

        protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesArchivoComentario();
            trComentariosArchivos.Visible = true;
        }
        #endregion

        #region Gridviews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridLegislacion = /*(Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridCumplimiento) +*/ Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    Label2.Visible = true;
                    TextBox1.Visible = true;
                    detalleLegislacion();
                    loadGridArchivoLegislacion();
                    loadInfoArchivoLegislacion();
                    loadGridComentarioLegislacion();
                    loadInfoComentarioLegislacion();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {

                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Eliminar legislación";

                    }
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoLegislacion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdfLegislacion();
                    break;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridComentarioLegislacion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verComentario();
                    break;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridCumplimiento = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridCumplimiento;
            GridView1.DataSource = InfoGridLegislacion;
            GridView1.DataBind();
        }
        #endregion

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        #region Loads
        private void loadCodigoLegislacion()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cRiesgo.loadCodigoLegislacion();

                //Ajuuste RCC - 15/01/2014
                if (NuevaLegislacion == 1 && dtInfo.Rows.Count > 0)
                {
                    Label2.Visible = false;
                    TextBox1.Visible = false;
                }

                if (dtInfo.Rows.Count > 0)
                    TextBox1.Text = "L" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox1.Text = "L1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código legislación. " + ex.Message);
            }
        }

        private void loadDDLEstadoLegislacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLEstadoLegislacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList7.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreEstadoLegislacion"].ToString().Trim(), dtInfo.Rows[i]["IdEstadoLegislacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar estado legislación. " + ex.Message);
            }
        }

        private void loadDDLTipoLegislacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLTipoLegislacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoLegislacion"].ToString().Trim(), dtInfo.Rows[i]["IdTipoLegislacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar tipo legislación. " + ex.Message);
            }
        }

        private void loadInfoLegislacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoLegislacion();

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridLegislacion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdLegislacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoLegislacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreLegislacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdTipoLegislacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoLegislacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionLegislacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaVigenciaDesde"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaVigenciaHasta"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actualizacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaCierre"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdEstadoLegislacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdResponsable"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreHijo"].ToString().Trim()
                        });
                }
                GridView1.PageIndex = PagIndexInfoGridCumplimiento;
                GridView1.DataSource = InfoGridLegislacion;
                GridView1.DataBind();
            }
        }

        private void loadGridLegislacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdLegislacion", typeof(string));
            grid.Columns.Add("CodigoLegislacion", typeof(string));
            grid.Columns.Add("NombreLegislacion", typeof(string));
            grid.Columns.Add("IdTipoLegislacion", typeof(string));
            grid.Columns.Add("NombreTipoLegislacion", typeof(string));
            grid.Columns.Add("DescripcionLegislacion", typeof(string));
            grid.Columns.Add("FechaVigenciaDesde", typeof(string));
            grid.Columns.Add("FechaVigenciaHasta", typeof(string));
            grid.Columns.Add("Actualizacion", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("FechaCierre", typeof(string));
            grid.Columns.Add("IdEstadoLegislacion", typeof(string));
            grid.Columns.Add("IdResponsable", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            InfoGridLegislacion = grid;
            GridView1.DataSource = InfoGridLegislacion;
            GridView1.DataBind();
        }

        private void loadInfoComentarioLegislacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoComentarioLegislacion(InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioLegislacion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["ComentarioCorto"].ToString().Trim(),
                        dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                    });
                }
                GridView4.DataSource = InfoGridComentarioLegislacion;
                GridView4.DataBind();
            }
        }

        private void loadGridComentarioLegislacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("ComentarioCorto", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            InfoGridComentarioLegislacion = grid;
            GridView4.DataSource = InfoGridComentarioLegislacion;
            GridView4.DataBind();
        }

        private void loadInfoArchivoLegislacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoArchivoLegislacion(InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoLegislacion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                        });
                }
                GridView3.DataSource = InfoGridArchivoLegislacion;
                GridView3.DataBind();
            }
        }

        private void loadGridArchivoLegislacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoLegislacion = grid;
            GridView3.DataSource = InfoGridArchivoLegislacion;
            GridView3.DataBind();
        }
        #endregion

        private void inicializarValores()
        {
            PagIndexInfoGridCumplimiento = 0;
        }

        private void GetLastLegislacion()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cRiesgo.GetLastLegislacion();

                //Ajuuste RCC - 15/01/2014
                if (NuevaLegislacion == 1 && dtInfo.Rows.Count > 0)
                {
                    Label2.Visible = false;
                    TextBox1.Visible = false;
                }

                if (dtInfo.Rows.Count > 0)
                    LastLegislacion = "L" + dtInfo.Rows[0]["LastLegislacion"].ToString().Trim();
                else
                    LastLegislacion = "L1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código legislación. " + ex.Message);
            }
        }

        private void agregarComentarioLegislacion()
        {
            cRiesgo.agregarComentarioLegislacion(Sanitizer.GetSafeHtmlFragment(TextBox12.Text.ToString().Trim()), InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim());
        }

        private void borrarLegislacion()
        {
            try
            {
                cRiesgo.borrarLegislacion(InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim());
                resetValues();
                loadGridLegislacion();
                loadInfoLegislacion();
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar legislación. " + ex.Message);
            }
        }

        private void detalleLegislacion()
        {
            resetValues();
            resetValuesArchivoComentario();
            tbCampos.Visible = true;
            ImageButton5.Visible = true;

            TextBox1.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["CodigoLegislacion"].ToString().Trim();
            TextBox2.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["NombreLegislacion"].ToString().Trim();
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedValue.ToString().Trim() == InfoGridLegislacion.Rows[RowGridLegislacion]["IdTipoLegislacion"].ToString().Trim())
                    break;
                else
                    DropDownList1.SelectedIndex = 0;
            }
            TextBox4.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["DescripcionLegislacion"].ToString().Trim();
            TextBox5.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["FechaVigenciaDesde"].ToString().Trim();
            TextBox6.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["FechaVigenciaHasta"].ToString().Trim();
            TextBox7.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["Actualizacion"].ToString().Trim();
            Label9.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["Usuario"].ToString().Trim();
            Label12.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["FechaRegistro"].ToString().Trim();
            TextBox3.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["FechaCierre"].ToString().Trim();
            for (int i = 0; i < DropDownList7.Items.Count; i++)
            {
                DropDownList7.SelectedIndex = i;
                if (DropDownList7.SelectedValue.ToString().Trim() == InfoGridLegislacion.Rows[RowGridLegislacion]["IdEstadoLegislacion"].ToString().Trim())
                    break;
                else
                    DropDownList7.SelectedIndex = 0;
            }

            TextBox34.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["NombreHijo"].ToString().Trim();
            lblIdDependencia1.Text = InfoGridLegislacion.Rows[RowGridLegislacion]["IdResponsable"].ToString().Trim();
            trComentariosArchivos.Visible = true;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            Label9.Text = "";
            Label12.Text = "";
            TextBox3.Text = "";
            TextBox34.Text = "";
            DropDownList1.SelectedIndex = 0;
            DropDownList7.SelectedIndex = 0;
            lblIdDependencia1.Text = "0";
            lblIdDependencia1.Visible = false;
            tbGridLegislacion.Visible = true;
            tbCampos.Visible = false;
            trUsuario.Visible = false;
            trFecha.Visible = false;
            ImageButton4.Visible = false;
            ImageButton5.Visible = false;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }

        private void verComentario()
        {
            TextBox12.Text = InfoGridComentarioLegislacion.Rows[RowGridComentarioLegislacion]["Comentario"].ToString().Trim();
            TextBox12.ReadOnly = true;
            ImageButton9.Visible = true;
        }

        private void resetValuesArchivoComentario()
        {
            trComentariosArchivos.Visible = false;
            TextBox12.ReadOnly = false;
            TextBox12.Text = "";
            ImageButton9.Visible = false;
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Vars
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion

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
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + row["Cuerpo"].ToString().Trim();
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
                #endregion

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
                #endregion

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

        #region PDFs
        private void loadFile()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cControl.loadCodigoArchivoControl();
            if (dtInfo.Rows.Count > 0)
            {
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" +
                    InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim() + "-" +
                    FileUpload1.FileName.ToString().Trim();
            }
            else
            {
                nameFile = "1-" + InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim() + "-" +
                    FileUpload1.FileName.ToString().Trim();
            }
            FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFsLegislacion/") + nameFile);
            cRiesgo.agregarArchivoLegislacion(InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim(), nameFile);
        }

        private void descargarArchivo()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFsLegislacion/" + InfoGridArchivoLegislacion.Rows[RowGridArchivoLegislacion]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }

        private void mtdCargarPdfLegislacion()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty, strIdControl = "2";
            #endregion Vars

            dtInfo = cControl.loadCodigoArchivoControl();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}",
                    InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cRiesgo.mtdAgregarArchivoPdf(strIdControl, InfoGridLegislacion.Rows[RowGridLegislacion]["IdLegislacion"].ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfLegislacion()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoLegislacion.Rows[RowGridArchivoLegislacion]["UrlArchivo"].ToString().Trim();
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

        #endregion PDFs
    }
}