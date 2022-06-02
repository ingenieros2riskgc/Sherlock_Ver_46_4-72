using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Auuditoriaa : System.Web.UI.UserControl
    {
        string IdFormulario = "3003";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();
        cRiesgo cRiesgo = new cRiesgo();

        private static int LastInsertIdCE;
        private static int LastInsertId;

        #region Properties

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {

                    if (Request.QueryString["IdAuditoria"] != null)
                    {
                        LidAuditoria.Text = Request.QueryString["IdAuditoria"];
                        txtCodAuditoriaGen.Text = Request.QueryString["IdAuditoria"];
                        TXIdPlaneacionCNC.Text = Request.QueryString["IdPlaneacion"];
                        TXIdPlaneacionCNC.Visible = true;
                        txtCodPlaneacion.Visible = false;
                        txtCodEstandarObj.Text = Request.QueryString["IdEstandar"];
                        txtNomPlaneacion.Text = Request.QueryString["planeacion"];
                        GridView7.Visible = true;
                    }
                    else
                    {
                        PagIndex = 0;
                        txtCodAuditoria.Text = "";
                        txtCodObjetivo.Text = "";
                        TreeNodeCollection nodes = this.TreeView1.Nodes;

                        if (nodes.Count <= 0)
                            PopulateTreeView();

                        if (Page.PreviousPage != null)
                        {
                            Control placeHolder = Page.PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                            Control usercontrol = placeHolder.FindControl("Planeacion");
                            TextBox txtIdPlaneacion = (TextBox)usercontrol.FindControl("txtId");
                            TextBox txtNombrePlaneacion = (TextBox)usercontrol.FindControl("txtNombre");

                            if (txtIdPlaneacion != null)
                            {
                                txtCodPlaneacion.Text = Sanitizer.GetSafeHtmlFragment(txtIdPlaneacion.Text);
                                txtNomPlaneacion.Text = Sanitizer.GetSafeHtmlFragment(txtNombrePlaneacion.Text);
                            }
                        }
                        else
                            txtCodPlaneacion.Text = "0";

                        GridView1.DataBind();
                        TabContainer2.ActiveTabIndex = 0;
                        GridView4.DataBind();
                        ddlNaturaleza.DataBind();
                        mtdLoadDDLEmpresa();

                    }
                }
            }

        }
        #region Loads
        private void mtdLoadDDLEmpresa()
        {
            DataTable dtInfo = new DataTable();
            cRiesgo cRiesgo = new cRiesgo();

            try
            {
                dtInfo = cRiesgo.mtdLoadEmpresa(true);
                ddlEmpresa.Items.Insert(0, new ListItem("---", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlEmpresa.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                }
                //ddlEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                //Mensaje("Error al cargar Empresas. " + ex.Message);
                omb.ShowMessage("Error al cargar Empresas.", 2, "Atención");
            }
        }
        #endregion

        #region Treeview
        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
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
                //newNode.SelectAction = TreeNodeSelectAction.Select;
                //newNode.NavigateUrl = "javascript:void(0)";
                //newNode.Target = "";
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependencia.Text = TreeView1.SelectedNode.Text;
            lblIdDependencia.Text = TreeView1.SelectedNode.Value;
        }

        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeViewGA()
        {
            DataTable treeViewData = GetTreeViewDataGA();
            AddTopTreeViewNodesGA(treeViewData);
        }

        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        private DataTable GetTreeViewDataGA()
        {
            // Get JerarquiaOrganizacional table
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijoAuditoria FROM [Auditoria].[JerarquiaGrupoAuditoria] WHERE idGrupoAuditoria = " + ddlGrupoAud.SelectedValue;
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
        private void AddTopTreeViewNodesGA(DataTable treeViewData)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = 0";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijoAuditoria"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.Expanded = true;
                newNode.ToolTip = DetalleNodo(2, row["IdHijo"].ToString());
                TVGrupoAud.Nodes.Add(newNode);
                AddChildTreeViewNodesGA(treeViewData, newNode);
            }
        }

        /// <summary>
        /// Recursively add child TreeView items by filtering by ParentID
        /// </summary>
        private void AddChildTreeViewNodesGA(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijoAuditoria"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.Expanded = true;
                newNode.ToolTip = DetalleNodo(2, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodesGA(treeViewData, newNode);
            }
        }

        private string DetalleNodo(int tipoSelect, string idHijo)
        {
            string Detalle = "";
            string selectCommand = "";

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

        #endregion Treeview

        #region Gridview
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodAuditoria.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtCodAuditoriaGen.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtCodEstandarObj.Text = GridView1.SelectedDataKey.Values[1].ToString().Trim();
                txtAuditoriaObj.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtAuditoriaRICC.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtNomAuditoria.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtCodObjetivo.Text = "0";
                txtObjetivoEnf.Text = "0";
                ddlNaturaleza.DataBind();
                txtObjetivo.Text = GridView1.SelectedDataKey[6].ToString().Trim();
                txtRecursos.Text = GridView1.SelectedDataKey[5].ToString().Trim();
                txtAlcance.Text = GridView1.SelectedDataKey[7].ToString().Trim();
                txtFecIniA.Text = GridView1.SelectedDataKey[10].ToString().Trim();
                txtFecFinA.Text = GridView1.SelectedDataKey[11].ToString().Trim();
                ddlNaturaleza.SelectedValue = GridView1.SelectedDataKey[9].ToString().Trim();
                ddlNivelImportancia.SelectedValue = GridView1.SelectedDataKey[8].ToString().Trim();
                ddlEmpresa.SelectedValue = GridView1.SelectedDataKey[12].ToString().Trim();
                ddlEstandar.SelectedValue = GridView1.SelectedDataKey[1].ToString().Trim();
                ddlTipo.SelectedValue = GridView1.SelectedRow.Cells[5].Text.Trim();
                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;

                if (GridView1.SelectedRow.Cells[5].Text == "Procesos")
                {
                    txtProceso.Text = GridView1.SelectedDataKey[4].ToString().Trim();
                    lblIdProceso.Text = GridView1.SelectedDataKey[3].ToString().Trim();
                    lblIdDependencia.Text = "";
                    txtDependencia.Text = "";
                    filaProceso.Visible = true;
                    filaDependencia.Visible = false;
                }
                else if (GridView1.SelectedRow.Cells[5].Text == "Dependencia")
                {
                    txtDependencia.Text = GridView1.SelectedDataKey[4].ToString().Trim();
                    lblIdDependencia.Text = GridView1.SelectedDataKey[2].ToString().Trim();
                    lblIdProceso.Text = "";
                    txtProceso.Text = "";
                    filaProceso.Visible = false;
                    filaDependencia.Visible = true;
                }
                txtUsuario.Text = GridView1.SelectedDataKey[0].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecha.Text = GridView1.SelectedRow.Cells[11].Text.Trim();

                txtPlaneacionObj.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);
                txtPlaneacionRICC.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);
                txtEstandarObj.Text = GridView1.SelectedRow.Cells[3].Text.Trim();
                txtEstandarRICC.Text = GridView1.SelectedRow.Cells[3].Text.Trim();
                ddlObjetivo.DataBind();
                ddlCC.DataBind();
                ddlCiclo.DataBind();
                ddlCicloOF.DataBind();
                ddlRI.DataBind();
                ddlOF.DataBind();
                ddlGrupoAud.DataBind();
                if (ddlEstandar.SelectedValue != "")
                    ddlGrupoAud2.DataBind();

                TabContainer2.ActiveTabIndex = 0;
                TabContainer2.Tabs[1].Enabled = true;
                TabContainer2.Tabs[2].Enabled = true;
                filaGridOF.Visible = true;
                filaGridRICC.Visible = true;
                filaGridObjetivo.Visible = true;
                filaDetalleOF.Visible = false;
                filaDetalleRICC.Visible = false;
                filaDetalleObjetivo.Visible = false;
                filaDetalleObjEnfoque.Visible = false;
                FilaGridObjEnfoque.Visible = false;
                FilaGridObjetivoSE.Visible = true;
                FilaGridObjRecursos.Visible = false;
                filaBtnTemas.Visible = true;
                filaDetalleAuditoria.Visible = true;    
            }
        }
        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView7.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodAuditoria.Text = GridView7.SelectedRow.Cells[0].Text;
                txtCodAuditoriaGen.Text = GridView7.SelectedRow.Cells[0].Text;
                txtCodEstandarObj.Text = GridView7.SelectedDataKey.Values[1].ToString();
                txtAuditoriaObj.Text = GridView7.SelectedRow.Cells[1].Text;
                txtAuditoriaRICC.Text = GridView7.SelectedRow.Cells[1].Text;
                txtNomAuditoria.Text = GridView7.SelectedRow.Cells[1].Text;
                txtCodObjetivo.Text = "0";
                txtObjetivoEnf.Text = "0";
                ddlNaturaleza.DataBind();
                txtObjetivo.Text = GridView7.SelectedDataKey[6].ToString();
                txtRecursos.Text = GridView7.SelectedDataKey[5].ToString();
                txtAlcance.Text = GridView7.SelectedDataKey[7].ToString();
                txtFecIniA.Text = GridView7.SelectedDataKey[10].ToString();
                txtFecFinA.Text = GridView7.SelectedDataKey[11].ToString();
                ddlNaturaleza.SelectedValue = GridView7.SelectedDataKey[9].ToString();
                ddlNivelImportancia.SelectedValue = GridView7.SelectedDataKey[8].ToString();
                ddlEmpresa.SelectedValue = GridView7.SelectedDataKey[12].ToString();
                ddlEstandar.SelectedValue = GridView7.SelectedDataKey[1].ToString();
                ddlTipo.SelectedValue = GridView7.SelectedRow.Cells[5].Text;
                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;

                if (GridView7.SelectedRow.Cells[5].Text == "Procesos")
                {
                    txtProceso.Text = GridView7.SelectedDataKey[4].ToString();
                    lblIdProceso.Text = GridView7.SelectedDataKey[3].ToString();
                    lblIdDependencia.Text = "";
                    txtDependencia.Text = "";
                    filaProceso.Visible = true;
                    filaDependencia.Visible = false;
                }
                else if (GridView7.SelectedRow.Cells[5].Text == "Dependencia")
                {
                    txtDependencia.Text = GridView7.SelectedDataKey[4].ToString();
                    lblIdDependencia.Text = GridView7.SelectedDataKey[2].ToString();
                    lblIdProceso.Text = "";
                    txtProceso.Text = "";
                    filaProceso.Visible = false;
                    filaDependencia.Visible = true;
                }
                txtUsuario.Text = GridView7.SelectedDataKey[0].ToString(); //Aca va el codigo de usuario logueado
                txtFecha.Text = GridView7.SelectedRow.Cells[11].Text;

                txtPlaneacionObj.Text = txtNomPlaneacion.Text;
                txtPlaneacionRICC.Text = txtNomPlaneacion.Text;
                txtEstandarObj.Text = GridView7.SelectedRow.Cells[3].Text;
                txtEstandarRICC.Text = GridView7.SelectedRow.Cells[3].Text;
                ddlObjetivo.DataBind();
                ddlCC.DataBind();
                ddlCiclo.DataBind();
                ddlCicloOF.DataBind();
                ddlRI.DataBind();
                ddlOF.DataBind();
                ddlGrupoAud.DataBind();
                if (ddlEstandar.SelectedValue != "")
                    ddlGrupoAud2.DataBind();

                TabContainer2.ActiveTabIndex = 0;
                TabContainer2.Tabs[1].Enabled = true;
                TabContainer2.Tabs[2].Enabled = true;
                filaGridOF.Visible = true;
                filaGridRICC.Visible = true;
                filaGridObjetivo.Visible = true;
                filaDetalleOF.Visible = false;
                filaDetalleRICC.Visible = false;
                filaDetalleObjetivo.Visible = false;
                filaDetalleObjEnfoque.Visible = false;
                FilaGridObjEnfoque.Visible = false;
                FilaGridObjetivoSE.Visible = true;
                FilaGridObjRecursos.Visible = false;
                filaBtnTemas.Visible = true;
                filaDetalleAuditoria.Visible = true;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoUA.Text = "1";

            if (GridView2.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlCiclo.SelectedValue = GridView2.SelectedDataKey[1].ToString().Trim();
                ddlRI.SelectedValue = GridView2.SelectedDataKey[2].ToString().Trim();
                ddlCC.SelectedValue = GridView2.SelectedDataKey[3].ToString().Trim();
                txtUsuarioRICC.Text = GridView2.SelectedDataKey[4].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionRICC.Text = GridView2.SelectedRow.Cells[7].Text.Trim();
            }
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string t;
            ddlObjetivo.DataBind();

            txtAlcanceObj.Text = GridView3.SelectedDataKey[1].ToString().Trim();
            txtCodObjetivo.Text = GridView3.SelectedDataKey[0].ToString().Trim();
            ddlObjetivo.SelectedValue = GridView3.SelectedDataKey[0].ToString().Trim();
            txtUsuarioObj.Text = GridView3.SelectedDataKey[2].ToString().Trim();
            txtFechaCreacionObj.Text = GridView3.SelectedRow.Cells[5].Text.Trim();
            t = GridView3.SelectedDataKey[3].ToString().Trim();

            ddlEnfoque.DataBind();

            GridView4.DataSource = SqlDataSource15;
            GridView4.DataBind();
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoUA.Text = "4";

            if (GridView4.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlEnfoque.DataBind();
                txtObjetivoEnf.Text = ddlObjetivo.SelectedItem.Text;
                ddlEnfoque.SelectedValue = GridView4.SelectedDataKey[2].ToString().Trim();
                txtUsuarioObjEnf.Text = GridView4.SelectedDataKey[4].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionObjEnf.Text = GridView4.SelectedRow.Cells[5].Text.Trim();
            }
        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoUA.Text = "5";

            if (GridView5.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                lblCodNodoGA.Text = GridView5.SelectedDataKey[3].ToString().Trim();
                txtRecurso.Text = GridView5.SelectedRow.Cells[5].Text.Trim();
                txtFecIniRec.Text = GridView5.SelectedRow.Cells[8].Text.Trim();
                txtFecFinRec.Text = GridView5.SelectedRow.Cells[9].Text.Trim();
                txtHorasPlan.Text = GridView5.SelectedRow.Cells[10].Text.Trim();
                ddlEtapa.SelectedValue = GridView5.SelectedRow.Cells[7].Text.Trim();
                txtUsuarioRec.Text = GridView5.SelectedDataKey[5].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = GridView5.SelectedRow.Cells[11].Text.Trim();
                GridView6.DataBind();
                DiasHorasLaborables();
                imgMR.Visible = true;
            }
        }

        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();
            //TextBox1.Text = GridView1.SelectedRow.Cells(0).Text
            //txtNombre.Text = GridView1.SelectedRow.Cells(1).Text
            //txtMod.Text = GridView1.SelectedRow.Cells(2).Text
        }

        protected void GridView12_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoUA.Text = "2";

            if (GridView12.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlOF.SelectedValue = GridView12.SelectedDataKey[1].ToString().Trim();
                ddlCicloOF.SelectedValue = GridView12.SelectedDataKey[3].ToString().Trim();
                txtUsuarioOF.Text = GridView12.SelectedDataKey[2].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionOF.Text = GridView12.SelectedRow.Cells[5].Text.Trim();
                ddlCicloOF.Enabled = false;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                filaAuditoria.Visible = false;
                filaDetalle.Visible = true;
                txtNomAuditoria.Focus();
            }
        }
        protected void GridView7_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                filaAuditoria.Visible = false;
                filaDetalle.Visible = true;
                txtNomAuditoria.Focus();
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                ddlCiclo.Enabled = false;
                ddlRI.Enabled = false;
                ddlCC.Enabled = false;
                btnImgInsertarRICC.Visible = false;
                btnImgActualizarRICC.Visible = false;
                filaDetalleRICC.Visible = true;
                filaGridRICC.Visible = false;
                filaDetalleOF.Visible = false;
                filaGridOF.Visible = false;
            }

            if (e.CommandArgument.ToString() == "Eliminar")
            {
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblTipoUA.Text = "3";

            if (e.CommandArgument.ToString() == "Editar")
            {
                ddlObjetivo.Enabled = true;
                txtAlcanceObj.Focus();
                btnImgInsertarObjetivo.Visible = false;
                btnImgActualizarObjetivo.Visible = true;
                filaDetalleObjetivo.Visible = true;
                filaGridObjetivo.Visible = false;
            }

            if (e.CommandArgument.ToString() == "Actividades")
            {
                FilaGridObjEnfoque.Visible = true;
                FilaGridObjRecursos.Visible = false;
                GridView4.DataSource = SqlDataSource15;
                GridView4.DataBind();
            }

            if (e.CommandArgument.ToString() == "Recursos")
            {
                FilaGridObjEnfoque.Visible = false;
                FilaGridObjRecursos.Visible = true;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                ddlEnfoque.Enabled = false;
                btnImgInsertarObjEnfoque.Visible = false;
                btnImgActualizarObjEnfoque.Visible = false;
                filaGridObjetivo.Visible = false;
                filaDetalleObjEnfoque.Visible = true;
            }

            if (e.CommandArgument.ToString() == "Eliminar")
            {
            }
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                btnImgInsertarRecursos.Visible = false;
                btnImgActualizarRecursos.Visible = true;
                filaGridObjetivo.Visible = false;
                filaDetalleRecursos.Visible = true;
                ddlEtapa.Enabled = false;
                imgGrupoRec.Enabled = false;

                filaGridObjetivo.Visible = false;
                filaDetalleRecursos.Visible = true;
            }
        }

        protected void GridView12_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandArgument.ToString() == "Editar")
            {
                ddlOF.Enabled = false;
                btnImgInsertarOF.Visible = false;
                btnImgActualizarOF.Visible = false;
                filaDetalleOF.Visible = true;
                filaDetalleRICC.Visible = false;
                filaGridRICC.Visible = false;
                filaGridOF.Visible = false;
            }

            if (e.CommandArgument.ToString() == "Eliminar")
            {
            }
        }

        protected void GdRecursos_RowsCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GdRecursos.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);

            if (e.CommandName == "Modificar")
            {
                TbAddEtapas.Visible = true;
                TrVerEtapas.Visible = false;
                ImageButton11.Visible = false;
                ImageButton10.Visible = true;
                loadDDLGrupoAuditoria();
                LoadAsignacionRecursos();
            }
            else if (e.CommandName == "Eliminar")
            {
                EliminarAsignacionRecursos();
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
            }
        }

        protected void GdRecursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GdRecursos.PageIndex = PagIndex;
            GdRecursos.DataSource = InfoGrid;
            GdRecursos.DataBind();
        }
        #endregion Gridview

        #region DDL
        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedItem.Value == "Procesos")
            {
                filaDependencia.Visible = false;
                filaProceso.Visible = true;
            }
            else
            {
                filaDependencia.Visible = true;
                filaProceso.Visible = false;
            }
        }

        protected void ddlMacroProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProceso.Items.Clear();
            ddlProceso.DataBind();
            txtProceso.Text = ddlMacroProceso.SelectedItem.Text;
            lblIdProceso.Text = ddlMacroProceso.SelectedValue;
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProceso.Text = ddlProceso.SelectedItem.Text;
            lblIdProceso.Text = ddlProceso.SelectedValue;

        }

        protected void ddlGrupoAud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGrupoAud.SelectedValue == "0")
                imgGrupoAud.Enabled = false;
            else
            {
                imgGrupoAud.Enabled = true;
                TVGrupoAud.Nodes.Clear();
                PopulateTreeViewGA();
            }
        }

        protected void ddlObjetivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAu.mtdConsultarObjetivo(ddlObjetivo.SelectedValue.ToString().Trim(), txtCodAuditoria.Text.Trim());

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    txtCodObjetivo.Text = dtInfo.Rows[0]["IdObjetivo"].ToString().Trim();
                    txtAlcanceObj.Text = dtInfo.Rows[0]["Alcance"].ToString().Trim();
                    txtUsuarioObj.Text = dtInfo.Rows[0]["Usuario"].ToString().Trim();
                    txtFechaCreacionObj.Text = dtInfo.Rows[0]["FechaRegistro"].ToString().Trim();
                }
            }
        }

        protected void ddlObjetivo_DataBound(object sender, EventArgs e)
        {
            ddlObjetivo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEstandar_DataBound(object sender, EventArgs e)
        {
            ddlEstandar.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio    
        }

        protected void ddlMacroProceso_DataBound(object sender, EventArgs e)
        {
            ddlMacroProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProceso_DataBound(object sender, EventArgs e)
        {
            ddlProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlCiclo_DataBound(object sender, EventArgs e)
        {
            ddlCiclo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlRI_DataBound(object sender, EventArgs e)
        {
            ddlRI.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlCC_DataBound(object sender, EventArgs e)
        {
            ddlCC.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlOF_DataBound(object sender, EventArgs e)
        {
            ddlOF.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEnfoque_DataBound(object sender, EventArgs e)
        {
            ddlEnfoque.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlGrupoAud_DataBound(object sender, EventArgs e)
        {
            ddlGrupoAud.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlCicloOF_DataBound(object sender, EventArgs e)
        {
            ddlCicloOF.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlNaturaleza_DataBound(object sender, EventArgs e)
        {
            ddlNaturaleza.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio       
        }

        //03-03-2014 Camilo Aponte
        protected void ddlGrupoAud2_DataBound(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                ddlGrupoAud2.Items.Insert(0, new ListItem("---", "0")); // Inserta el Item con texto Vacio
                //cargar el idgrupoauditoria
                CargarIdGrupoAud();

                if (ddlGrupoAud2.SelectedItem.ToString() != "---")
                {
                    DataTable DtInfo = new DataTable();
                    DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                    txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString();
                    loadGrid();
                    loadInfo();
                    TrVerEtapas.Visible = true;
                }
            }
        }

        #endregion DDL

        #region Buttons
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalle.Visible = false;
            filaAuditoria.Visible = true;
            filaBtnTemas.Visible = false;
            filaDetalleAuditoria.Visible = false;
            ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;
            int IdProceso = 0;
            int IdTipoProceso = 0;
            if (ddlTipo.SelectedValue == "Procesos" && lblIdProceso.Text == "")
            {
                omb.ShowMessage("Por favor seleccione un Proceso.", 2, "Atención");
                err = true;
            }
            else if (ddlTipo.SelectedValue == "Dependencia" && lblIdDependencia.Text == "")
            {
                omb.ShowMessage("Por favor seleccione una Dependencia.", 2, "Atención");
                err = true;
            }

            if (err == false)
            {
                DataTable DtInfo = new DataTable();
                if (VerificarCampos())
                {
                    //Inserta el maestro del nodo hijo
                    try
                    {
                        clsAuditoriaDTO auditoria = new clsAuditoriaDTO();
                        clsAuditoriaBLL auditoriaBLL = new clsAuditoriaBLL();
                        Boolean booResult = false;
                        /*SqlDataSource1.InsertParameters["Tema"].DefaultValue = txtNomAuditoria.Text;
                        SqlDataSource1.InsertParameters["IdEstandar"].DefaultValue = ddlEstandar.SelectedValue.ToString();
                        SqlDataSource1.InsertParameters["IdPlaneacion"].DefaultValue = txtCodPlaneacion.Text;
                        SqlDataSource1.InsertParameters["Tipo"].DefaultValue = ddlTipo.SelectedValue.ToString();*/
                        auditoria.strTema = txtNomAuditoria.Text;
                        auditoria.intIdEstandar = Convert.ToInt32(ddlEstandar.SelectedValue.ToString());
                        auditoria.intIdPlaneacion = Convert.ToInt32(txtCodPlaneacion.Text);
                        auditoria.strTipo = ddlTipo.SelectedValue.ToString();
                        //04-04-2014
                        //SqlDataSource1.InsertParameters["Estado"].DefaultValue = "ABIERTA";
                        //SqlDataSource1.InsertParameters["Estado"].DefaultValue = "EJECUCIÓN";
                        auditoria.strEstado = "EJECUCIÓN";
                        if (ddlTipo.SelectedValue == "Procesos")
                        {
                            /*SqlDataSource1.InsertParameters["IdDependencia"].DefaultValue = "0";
                            SqlDataSource1.InsertParameters["IdProceso"].DefaultValue = lblIdProceso.Text;*/
                            auditoria.intIdDependencia = 0;
                            auditoria.intIdProceso = Convert.ToInt32(lblIdProceso.Text);
                            if(ddlProceso.SelectedValue != "0")
                            {
                                IdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                                IdTipoProceso = 2;
                            }
                            else
                            {
                                if(ddlMacroProceso.SelectedValue != "0")
                                {
                                    IdProceso = Convert.ToInt32(ddlMacroProceso.SelectedValue);
                                    IdTipoProceso = 1;
                                }
                            }
                        }
                        else
                        {
                            /*SqlDataSource1.InsertParameters["IdDependencia"].DefaultValue = "" + lblIdDependencia.Text;
                            SqlDataSource1.InsertParameters["IdProceso"].DefaultValue = "0";*/
                            auditoria.intIdProceso = 0;
                            auditoria.intIdDependencia = Convert.ToInt32(lblIdDependencia.Text);
                        }
                        /*SqlDataSource1.InsertParameters["IdEmpresa"].DefaultValue = ddlEmpresa.SelectedValue;
                        SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString(); //Aca va el id del Usuario de la BD
                        SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        SqlDataSource1.InsertParameters["Recursos"].DefaultValue = txtRecursos.Text;*/
                        auditoria.intIdEmpresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
                        auditoria.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                        auditoria.strFechaRegistro = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        auditoria.strRecursos = txtRecursos.Text;

                        /*SqlDataSource1.InsertParameters["Objetivo"].DefaultValue = txtObjetivo.Text;
                        SqlDataSource1.InsertParameters["Alcance"].DefaultValue = txtAlcance.Text;
                        SqlDataSource1.InsertParameters["FechaInicio"].DefaultValue = txtFecIniA.Text;
                        SqlDataSource1.InsertParameters["FechaCierre"].DefaultValue = txtFecFinA.Text;
                        SqlDataSource1.InsertParameters["IdDetalleTipo_TipoNaturaleza"].DefaultValue = ddlNaturaleza.SelectedValue;
                        SqlDataSource1.InsertParameters["NivelImportancia"].DefaultValue = ddlNivelImportancia.SelectedValue;*/
                        auditoria.strObjetivo = txtObjetivo.Text;
                        auditoria.strAlcance = txtAlcance.Text;
                        auditoria.strFechaInicio = txtFecIniA.Text;
                        auditoria.strFechaCierre = txtFecFinA.Text;
                        auditoria.intIdDetalle_TipoNaturaleza = Convert.ToInt32(ddlNaturaleza.SelectedValue);
                        auditoria.strNivelImportancia = ddlNivelImportancia.SelectedValue;

                        //DataTable DtInfo = new DataTable();
                        //03-03-2014 Camilo Aponte
                        txtCodAuditoriaGen.Text = LastInsertId.ToString().Trim();
                        //DataTable DtInfo = new DataTable();
                        DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                        if (DtInfo.Rows[0]["IdObjetivo"].ToString() != "")
                        {
                            string strErrMsg = string.Empty;
                            try
                            {
                                booResult = auditoriaBLL.mtdInsertarAuditoria(auditoria, ref strErrMsg);
                            }catch(Exception ex)
                            {
                                omb.ShowMessage("Error: "+ex, 2, "Atención");
                            }
                            if (booResult == true)
                            {
                                int UltimoId = auditoriaBLL.mtdLastIdAuditoria(ref strErrMsg);
                                booResult = auditoriaBLL.mtdInsertarAuditoriaProceso(UltimoId, IdProceso, IdTipoProceso, ref strErrMsg);
                                //SqlDataSource1.Insert();

                                //03-03-2014 Camilo Aponte
                                //txtCodAuditoriaGen.Text = LastInsertId.ToString();
                                txtCodAuditoriaGen.Text = UltimoId.ToString();

                                //DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                                txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString();
                                cAu.InseratObjetivoGrupoAuditoria(txtCodAuditoriaGen.Text, txtCodObjetivo.Text, ddlGrupoAud2.SelectedValue.ToString(), System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Session["idUsuario"].ToString());
                                cAu.LogHistoricoAudutoria(txtCodAuditoriaGen.Text, "getdate()", "NULL", "NULL", "NO", Session["idUsuario"].ToString());
                                loadGrid();
                                loadInfo();
                                TrVerEtapas.Visible = true;
                            }
                            else
                            {
                                omb.ShowMessage("Error: No se ha llevado a cabo la inserción, " + strErrMsg, 1, "Atención");
                                err = true;
                            }
                        }
                        else
                        {
                            omb.ShowMessage("Error: El estandar no tiene asignado un Objetivo", 2, "Atención");
                            err = true;
                        }
                        //LOG DE HISORICO
                    }
                    catch (Exception except)
                    {
                        // Handle the Exception.
                        omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }

                    if (!err && DtInfo.Rows[0]["IdObjetivo"].ToString() != "")
                    {
                        omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                        TabContainer2.Tabs[1].Enabled = true;
                        TabContainer2.Tabs[2].Enabled = true;
                        btnImgActualizar.Visible = true;
                        btnImgInsertar.Visible = false;

                        txtPlaneacionObj.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);
                        txtPlaneacionRICC.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);

                        //txtCodAuditoriaGen.Text = LastInsertId.ToString();
                        txtCodAuditoria.Text = txtCodAuditoriaGen.Text.Trim();
                        txtCodEstandarObj.Text = ddlEstandar.SelectedValue;
                        txtAuditoriaObj.Text = Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text);
                        txtAuditoriaRICC.Text = Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text);
                        txtEstandarObj.Text = ddlEstandar.SelectedItem.Text;
                        txtEstandarRICC.Text = ddlEstandar.SelectedItem.Text;
                        GridView1.DataBind();
                    }
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                bool err = false;
                if (ddlTipo.SelectedValue == "Procesos" && Sanitizer.GetSafeHtmlFragment(txtProceso.Text) == "")
                {
                    omb.ShowMessage("Por favor seleccione un Proceso.", 2, "Atención");
                    txtProceso.Focus();
                    err = true;
                }
                else if (ddlTipo.SelectedValue == "Dependencia" && Sanitizer.GetSafeHtmlFragment(txtDependencia.Text) == "")
                {
                    omb.ShowMessage("Por favor seleccione una Dependencia.", 2, "Atención");
                    txtProceso.Focus();
                    err = true;
                }

                if (err == false)
                {
                    if (VerificarCampos())
                    {
                        try
                        {
                            SqlDataSource1.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                            SqlDataSource1.UpdateParameters["Tema"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text);
                            SqlDataSource1.UpdateParameters["IdEstandar"].DefaultValue = ddlEstandar.SelectedValue;
                            SqlDataSource1.UpdateParameters["IdPlaneacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text);
                            SqlDataSource1.UpdateParameters["Tipo"].DefaultValue = ddlTipo.SelectedValue;
                            if (ddlTipo.SelectedValue == "Procesos")
                            {
                                SqlDataSource1.UpdateParameters["IdDependencia"].DefaultValue = "";
                                SqlDataSource1.UpdateParameters["IdProceso"].DefaultValue = lblIdProceso.Text;
                            }
                            else
                            {
                                SqlDataSource1.UpdateParameters["IdDependencia"].DefaultValue = lblIdDependencia.Text;
                                SqlDataSource1.UpdateParameters["IdProceso"].DefaultValue = "";
                            }
                            SqlDataSource1.UpdateParameters["Recursos"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtRecursos.Text);
                            SqlDataSource1.UpdateParameters["Objetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtObjetivo.Text);
                            SqlDataSource1.UpdateParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcance.Text);
                            SqlDataSource1.UpdateParameters["FechaInicio"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIniA.Text);
                            SqlDataSource1.UpdateParameters["FechaCierre"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFinA.Text);
                            SqlDataSource1.UpdateParameters["IdDetalleTipo_TipoNaturaleza"].DefaultValue = ddlNaturaleza.SelectedValue;
                            SqlDataSource1.UpdateParameters["NivelImportancia"].DefaultValue = ddlNivelImportancia.SelectedValue;
                            SqlDataSource1.UpdateParameters["IdEmpresa"].DefaultValue = ddlEmpresa.SelectedValue; ;
                            SqlDataSource1.Update();

                            //03-03-2014 Camilo Aponte
                            //txtCodAuditoriaGen.Text = LastInsertId.ToString().Trim();
                            DataTable DtInfo = new DataTable();
                            DtInfo = cAu.mtdVerObjetivoEstandar(ddlEstandar.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text.Trim()));
                            txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString().Trim();
                            cAu.ModificarObjetivoGrupoAuditoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text), Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text), ddlGrupoAud2.SelectedValue.ToString());
                            loadGrid();
                            loadInfo();
                            TrVerEtapas.Visible = true;

                        }
                        catch (Exception except)
                        {
                            omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                            err = true;
                        }

                        if (!err)
                        {
                            omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                            GridView1.DataBind();
                        }
                    }
                }

            }
            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }

        protected void btnImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgokEliminar_Click(object sender, EventArgs e)
        {
            bool err = false;

            mpeMsgBox.Hide();

            try
            {
                if (TabContainer2.ActiveTabIndex.ToString() == "0")
                {
                    SqlDataSource1.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                    SqlDataSource1.Delete();
                }
                else if (TabContainer2.ActiveTabIndex.ToString() == "1")
                {
                    if (lblTipoUA.Text == "1")
                    {
                        SqlDataSource4.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource4.DeleteParameters["IdCiclo"].DefaultValue = ddlCiclo.SelectedValue;
                        SqlDataSource4.DeleteParameters["IdRiesgoInherente"].DefaultValue = ddlRI.SelectedValue;
                        SqlDataSource4.DeleteParameters["IdCalificacionControl"].DefaultValue = ddlCC.SelectedValue;
                        SqlDataSource4.Delete();
                    }
                    else if (lblTipoUA.Text == "2")
                    {
                        SqlDataSource12.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource12.DeleteParameters["IdOtrosFactores"].DefaultValue = ddlOF.SelectedValue;
                        SqlDataSource12.Delete();
                    }
                }
                else if (TabContainer2.ActiveTabIndex.ToString() == "2")
                {
                    if (lblTipoUA.Text == "3")
                    {
                        SqlDataSource2.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource2.DeleteParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDataSource2.Delete();
                    }
                    else if (lblTipoUA.Text == "4")
                    {
                        SqlDataSource15.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource15.DeleteParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                        SqlDataSource15.DeleteParameters["IdEnfoque"].DefaultValue = ddlEnfoque.SelectedValue;
                        SqlDataSource15.Delete();
                    }
                    else if (lblTipoUA.Text == "5")
                    {
                        SqlDataSource17.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource17.DeleteParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                        SqlDataSource17.DeleteParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                        SqlDataSource17.DeleteParameters["IdHijo"].DefaultValue = lblCodNodoGA.Text;
                        SqlDataSource17.DeleteParameters["Etapa"].DefaultValue = ddlEtapa.SelectedValue;
                        SqlDataSource17.Delete();
                    }
                }
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    omb.ShowMessage("Error en la eliminación de la información. <br/> La información a borrar tiene relacionada con el histórico de auditoría. <br/> Por favor revise la información.", 1, "Atención");
                else
                    omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + odbcEx.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
        }

        protected void btnImgEliminarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgCancelarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
            filaDetalleObjetivo.Visible = false;
            filaGridObjetivo.Visible = true;
            FilaGridObjEnfoque.Visible = false;
        }

        protected void btnImgActualizarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        #region Objetivo
                        SqlDataSource2.DeleteParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaGen.Text;
                        SqlDataSource2.DeleteParameters["IdObjetivo"].DefaultValue = txtCodObjetivo.Text;

                        SqlDataSource2.InsertParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaGen.Text;
                        SqlDataSource2.InsertParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDataSource2.InsertParameters["Alcance"].DefaultValue = txtAlcanceObj.Text;
                        if (ddlGrupoAud.SelectedValue == "0")
                            SqlDataSource2.InsertParameters["IdGrupoAuditoria"].DefaultValue = null;
                        else
                            SqlDataSource2.InsertParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                        SqlDataSource2.InsertParameters["FechaInicial"].DefaultValue = txtFecIni.Text;
                        SqlDataSource2.InsertParameters["FechaFinal"].DefaultValue = txtFecFin.Text;
                        SqlDataSource2.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                        SqlDataSource2.InsertParameters["FechaRegistro"].DefaultValue = txtFechaCreacionObj.Text;

                        #endregion

                        #region Objetivo - Enfoque
                        SqlDataSource2A.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaGen.Text;
                        SqlDataSource2A.UpdateParameters["IdObjetivo"].DefaultValue = txtCodObjetivo.Text;
                        SqlDataSource2A.UpdateParameters["IdObjetivoNuevo"].DefaultValue = ddlObjetivo.SelectedValue;
                        #endregion

                        SqlDataSource2.Insert();
                        SqlDataSource2A.Update();
                        SqlDataSource2.Delete();

                        GridView3.DataBind();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalleObjetivo.Visible = false;
                        filaGridObjetivo.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgInsertarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource2.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource2.InsertParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                    SqlDataSource2.InsertParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text);
                    if (ddlGrupoAud.SelectedValue == "0")
                        SqlDataSource2.InsertParameters["IdGrupoAuditoria"].DefaultValue = null;
                    else
                        SqlDataSource2.InsertParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                    SqlDataSource2.InsertParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIni.Text);
                    SqlDataSource2.InsertParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFin.Text);
                    SqlDataSource2.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource2.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");

                    txtFecIni.Text = "";
                    txtFecFin.Text = "";
                    SqlDataSource2.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleObjetivo.Visible = false;
                    filaGridObjetivo.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgInsertarRICC_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource4.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource4.InsertParameters["IdCiclo"].DefaultValue = ddlCiclo.SelectedValue;
                    SqlDataSource4.InsertParameters["IdRiesgoInherente"].DefaultValue = ddlRI.SelectedValue;
                    SqlDataSource4.InsertParameters["IdCalificacionControl"].DefaultValue = ddlCC.SelectedValue;
                    SqlDataSource4.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource4.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");

                    SqlDataSource4.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleRICC.Visible = false;
                    filaGridRICC.Visible = true;
                    filaDetalleOF.Visible = false;
                    filaGridOF.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarRICC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource2.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource2.UpdateParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDataSource2.UpdateParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text);
                        SqlDataSource2.UpdateParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIni.Text);
                        SqlDataSource2.UpdateParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFin.Text);

                        SqlDataSource2.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalleObjetivo.Visible = false;
                        filaGridObjetivo.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgCancelarRICC_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRICC.Visible = false;
            filaGridRICC.Visible = true;
            filaDetalleOF.Visible = false;
            filaGridOF.Visible = true;
        }

        protected void btnImgEliminarRICC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgEliminarOF_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgInsertarOF_Click(object sender, ImageClickEventArgs e)
        {
            //Inserta el maestro del nodo hijo
            try
            {
                SqlDataSource12.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                SqlDataSource12.InsertParameters["IdCiclo"].DefaultValue = ddlCicloOF.SelectedValue;
                SqlDataSource12.InsertParameters["IdOtrosFactores"].DefaultValue = ddlOF.SelectedValue;
                SqlDataSource12.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource12.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");

                SqlDataSource12.Insert();

                omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                filaDetalleRICC.Visible = false;
                filaGridRICC.Visible = true;
                filaDetalleOF.Visible = false;
                filaGridOF.Visible = true;
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
            }
        }

        protected void btnImgActualizarOF_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                try
                {
                    SqlDataSource12.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource12.UpdateParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                    SqlDataSource12.UpdateParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text);
                    SqlDataSource12.UpdateParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIni.Text);
                    SqlDataSource12.UpdateParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFin.Text);

                    SqlDataSource12.Update();

                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleObjetivo.Visible = false;
                    filaGridObjetivo.Visible = true;
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgCancelarOF_Click(object sender, ImageClickEventArgs e)
        {
            ddlGrupoAud2.SelectedIndex = 0;
            filaDetalleOF.Visible = false;
            filaDetalleRICC.Visible = false;
            filaGridRICC.Visible = true;
            filaGridOF.Visible = true;
        }

        protected void btnTemasAud_Click(object sender, EventArgs e)
        {
            filaAuditoria.Visible = true;
            filaDetalle.Visible = false;
            filaBtnTemas.Visible = false;
            filaDetalleAuditoria.Visible = false;
            GridView1.DataBind();
            ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
        }

        protected void btnImgCancelarObjEnfoque_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleObjEnfoque.Visible = false;
            filaGridObjetivo.Visible = true;
        }

        protected void btnImgInsertarObjEnfoque_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource15.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource15.InsertParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                    SqlDataSource15.InsertParameters["IdEnfoque"].DefaultValue = ddlEnfoque.SelectedValue;
                    SqlDataSource15.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource15.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                    SqlDataSource15.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleObjEnfoque.Visible = false;
                    filaGridObjetivo.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarObjEnfoque_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource12.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource12.UpdateParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDataSource12.UpdateParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text);
                        SqlDataSource12.UpdateParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIni.Text);
                        SqlDataSource12.UpdateParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFin.Text);

                        SqlDataSource12.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalleObjetivo.Visible = false;
                        filaGridObjetivo.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgCancelarRecursos_Click(object sender, ImageClickEventArgs e)
        {
            ddlGrupoAud2.SelectedIndex = 0;
            filaGridObjetivo.Visible = true;
            filaDetalleRecursos.Visible = false;
        }

        protected void btnImgInsertarRecursos_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource17.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource17.InsertParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                    SqlDataSource17.InsertParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                    SqlDataSource17.InsertParameters["IdHijo"].DefaultValue = lblCodNodoGA.Text;
                    SqlDataSource17.InsertParameters["Etapa"].DefaultValue = ddlEtapa.SelectedValue;
                    SqlDataSource17.InsertParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIniRec.Text);
                    SqlDataSource17.InsertParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFinRec.Text);
                    SqlDataSource17.InsertParameters["HorasPlaneadas"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtHorasPlan.Text);
                    SqlDataSource17.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource17.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource17.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaGridObjetivo.Visible = true;
                    //capo
                    filaDetalleRecursos.Visible = true;
                    GridView5.DataBind();
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }

                try
                {
                    //enviar la notificacion 
                    string InfAdicional = "Ha sido asignado como responsable de la auditoría:<br /><br />";
                    InfAdicional += "Planeación: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br />";
                    InfAdicional += "Auditoría: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text) + "<br />";
                    InfAdicional += "Objetivo: " + Sanitizer.GetSafeHtmlFragment(txtObjetivoRec.Text) + "<br />";
                    InfAdicional += "Fecha Inicio: " + Sanitizer.GetSafeHtmlFragment(txtFecIniRec.Text) + "<br />" + "Fecha Fin: " + Sanitizer.GetSafeHtmlFragment(txtFecFinRec.Text) + "<br />" + "Horas: " + Sanitizer.GetSafeHtmlFragment(txtHorasPlan.Text) + "<br /><br />";
                    boolEnviarNotificacion(15, Convert.ToInt16("0"), Convert.ToInt16(lblCodNodoGA.Text.Trim()), "", InfAdicional);
                }
                catch (Exception ex)
                {
                    omb.ShowMessage("Error al enviar la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarRecursos_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource17.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource17.UpdateParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                        SqlDataSource17.UpdateParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                        SqlDataSource17.UpdateParameters["IdHijo"].DefaultValue = lblCodNodoGA.Text;
                        SqlDataSource17.UpdateParameters["Etapa"].DefaultValue = ddlEtapa.SelectedValue;
                        SqlDataSource17.UpdateParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIniRec.Text);
                        SqlDataSource17.UpdateParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFinRec.Text);
                        SqlDataSource17.UpdateParameters["HorasPlaneadas"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtHorasPlan.Text);
                        SqlDataSource17.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaGridObjetivo.Visible = true;
                        filaDetalleRecursos.Visible = false;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void imgBtnInsertarRICC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlCiclo.Focus();
                ddlCiclo.SelectedValue = null;
                ddlRI.SelectedValue = null;
                ddlCC.SelectedValue = null;
                txtUsuarioRICC.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionRICC.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                ddlCiclo.Enabled = true;
                ddlRI.Enabled = true;
                ddlCC.Enabled = true;
                btnImgInsertarRICC.Visible = true;
                btnImgActualizarRICC.Visible = false;

                filaDetalleRICC.Visible = true;
                filaGridRICC.Visible = false;
                filaDetalleOF.Visible = false;
                filaGridOF.Visible = false;
            }
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtCodAuditoria.Text = "";
                txtNomAuditoria.Text = "";
                txtNomAuditoria.Focus();
                txtDependencia.Text = "";
                txtProceso.Text = "";
                ddlEstandar.SelectedValue = null;
                txtUsuario.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecha.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                txtAlcance.Text = "";
                txtObjetivo.Text = "";
                txtRecursos.Text = "";
                txtFecIniA.Text = "";
                txtFecFinA.Text = "";
                ddlEmpresa.SelectedValue = "0";
                ddlNaturaleza.SelectedValue = "0";
                ddlNivelImportancia.SelectedValue = "0";
                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaAuditoria.Visible = false;
                filaDetalle.Visible = true;
                TabContainer2.Tabs[1].Enabled = false;
                TabContainer2.Tabs[2].Enabled = false;
                filaGridOF.Visible = true;
                filaGridRICC.Visible = true;
                filaGridObjetivo.Visible = true;
                filaDetalleOF.Visible = false;
                filaDetalleRICC.Visible = false;
                filaDetalleObjetivo.Visible = false;
                filaDetalleObjEnfoque.Visible = false;
                FilaGridObjEnfoque.Visible = false;
                FilaGridObjetivoSE.Visible = true;
                FilaGridObjRecursos.Visible = false;
                filaBtnTemas.Visible = true;
                filaBtnTemas.Visible = true;
                filaDetalleAuditoria.Visible = true;
            }
        }

        protected void imgBtnInsertarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                filaDetalleObjetivo.Visible = true;
                filaGridObjetivo.Visible = false;
                txtAlcanceObj.Text = "";
                txtFecFin.Text = "";
                txtFecIni.Text = "";
                ddlObjetivo.SelectedValue = null;
                ddlObjetivo.Enabled = true;
                ddlGrupoAud.SelectedValue = null;
                imgGrupoAud.Enabled = false;
                txtUsuarioObj.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionObj.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgInsertarObjetivo.Visible = true;
                btnImgActualizarObjetivo.Visible = false;
                ddlObjetivo.Focus();
            }
        }

        protected void imgBtnInsertarOF_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtUsuarioOF.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionOF.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgInsertarOF.Visible = true;
                btnImgActualizarOF.Visible = false;
                ddlOF.Enabled = true;
                ddlCicloOF.Enabled = true;
                ddlOF.SelectedValue = null;
                ddlCicloOF.SelectedValue = null;

                filaDetalleOF.Visible = true;
                filaDetalleRICC.Visible = false;
                filaGridRICC.Visible = false;
                filaGridOF.Visible = false;
            }
        }

        protected void imgBtnInsertarObjEnfoque_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtUsuarioObjEnf.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionObjEnf.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgInsertarObjEnfoque.Visible = true;
                btnImgActualizarObjEnfoque.Visible = false;
                ddlEnfoque.Enabled = true;
                ddlEnfoque.SelectedValue = null;
                txtObjetivoEnf.Text = ddlObjetivo.SelectedItem.Text;

                filaDetalleObjEnfoque.Visible = true;
                filaGridObjetivo.Visible = false;
            }
        }

        protected void imgBtnInsertarObjRecursos_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlEtapa.SelectedValue = "";
                ddlEtapa.Focus();

                txtRecurso.Text = "";
                txtFecIniRec.Text = "";
                txtFecFinRec.Text = "";
                txtHorasPlan.Text = "";
                ddlEtapa.SelectedValue = "";

                if (TVGrupoAud.SelectedNode != null)
                    TVGrupoAud.SelectedNode.Selected = false;

                txtUsuarioRec.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                lblCodNodoGA.Text = "0";
                GridView6.DataBind();
                btnImgInsertarRecursos.Visible = true;
                btnImgActualizarRecursos.Visible = false;
                ddlEtapa.Enabled = true;
                imgGrupoRec.Enabled = true;
                imgMR.Visible = false;

                filaGridObjetivo.Visible = false;
                filaDetalleRecursos.Visible = true;
            }
        }

        protected void imgBtnInsertarMasEtapa_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlEtapa.SelectedValue = "";
                ddlEtapa.Focus();

                //txtRecurso.Text = "";
                txtFecIniRec.Text = "";
                txtFecFinRec.Text = "";
                txtHorasPlan.Text = "";
                ddlEtapa.SelectedValue = "";

                if (TVGrupoAud.SelectedNode != null)
                    TVGrupoAud.SelectedNode.Selected = false;

                txtUsuarioRec.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                lblCodNodoGA.Text = "0";
                GridView6.DataBind();
                btnImgInsertarRecursos.Visible = true;
                btnImgActualizarRecursos.Visible = false;
                ddlEtapa.Enabled = true;
                imgGrupoRec.Enabled = true;
                imgMR.Visible = false;

                filaGridObjetivo.Visible = false;
                filaDetalleRecursos.Visible = true;
            }
        }

        protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
        {
            TrVerEtapas.Visible = false;
            TbAddEtapas.Visible = true;
            ImageButton11.Visible = true;
            ImageButton10.Visible = false;
            loadDDLGrupoAuditoria();
        }

        protected void ImageButton13_Click(object sender, ImageClickEventArgs e)
        {
            loadGrid();
            loadInfo();
            TrVerEtapas.Visible = true;
            TbAddEtapas.Visible = false;
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            DropDownList1.SelectedIndex = 0;

            //TbAddEtapas.Visible = false;
            //TrVerEtapas.Visible = true;
            //ddlGrupoAud2.SelectedIndex = 0;
            //TrVerEtapas.Visible = false;
        }

        protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
        {
            DataTable DtInfo = new DataTable();

            try
            {
                DtInfo = cAu.mtdVerObjetivoEstandar(ddlEstandar.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text.Trim()));
                cAu.InsertarRecursosGruposAuditoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text.Trim()), DtInfo.Rows[0]["IdObjetivo"].ToString().Trim(),
                    ddlGrupoAud2.SelectedValue.ToString().Trim(), DropDownList2.SelectedValue.ToString().Trim(),
                    DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text), Sanitizer.GetSafeHtmlFragment(TextBox2.Text), Sanitizer.GetSafeHtmlFragment(TextBox3.Text),
                    System.DateTime.Now.ToString("yyyy-MM-dd"), Session["idUsuario"].ToString());

                //enviar la notificacion 
                //Revisar 04-04-2014 camilo aponte
                string InfAdicional = "Ha sido asignado como responsable de la auditoría:<br /><br />";
                InfAdicional += "Planeación: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br />";
                InfAdicional += "Auditoría: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text) + "<br />";
                InfAdicional += "Objetivo: " + Sanitizer.GetSafeHtmlFragment(txtObjetivoRec.Text) + "<br />";
                InfAdicional += "Fecha Inicio: " + Sanitizer.GetSafeHtmlFragment(TextBox1.Text) + "<br />" + "Fecha Fin: " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text) + "<br />" + "Horas: " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text) + "<br /><br />";
                try
                {
                    boolEnviarNotificacion(15, Convert.ToInt16("0"), Convert.ToInt16(DropDownList2.SelectedValue.ToString()), "", InfAdicional);
                }
                catch (Exception ex)
                {
                    omb.ShowMessage("Error al enviar la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                }

                loadGrid();
                loadInfo();
                TrVerEtapas.Visible = true;
                TbAddEtapas.Visible = false;
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                DropDownList1.SelectedIndex = 0;
                omb.ShowMessage("La información se ingresó con éxito en la Base de Datos.", 3, "Atención");
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al guardar la información." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
            }

        }

        protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
        {
            DataTable DtInfo = new DataTable();
            DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
            cAu.ModificarRecursosGruposAuditoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text), DtInfo.Rows[0]["IdObjetivo"].ToString().Trim(), ddlGrupoAud2.SelectedValue.ToString().Trim(), DropDownList2.SelectedValue.ToString().Trim(), DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text), Sanitizer.GetSafeHtmlFragment(TextBox2.Text), Sanitizer.GetSafeHtmlFragment(TextBox3.Text));
            loadGrid();
            loadInfo();
            TrVerEtapas.Visible = true;
            TbAddEtapas.Visible = false;
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            DropDownList1.SelectedIndex = 0;
            GridView61.DataBind();
            omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
        }

        #endregion Buttons

        protected void txtCodEstandarObj_TextChanged(object sender, EventArgs e)
        {
            ddlObjetivo.Items.Clear();
            ddlObjetivo.DataBind();
        }

        protected void TVGrupoAud_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (filaDetalleRecursos.Visible == true)
            {
                txtRecurso.Text = TVGrupoAud.SelectedNode.Text;
                lblCodNodoGA.Text = TVGrupoAud.SelectedNode.Value;
                GridView6.DataBind();
                DiasHorasLaborables();
                imgMR.Visible = true;
            }
        }

        protected void TabContainer2_ActiveTabChanged(object sender, EventArgs e)
        {
            if ((TabContainer2.ActiveTabIndex.ToString() == "0" || TabContainer2.ActiveTabIndex.ToString() == "1") && filaDetalleObjetivo.Visible == true)
            {
                filaDetalleObjetivo.Visible = false;
                filaGridObjetivo.Visible = true;
            }

            if ((TabContainer2.ActiveTabIndex.ToString() == "0"))
            {
                filaDetalleAuditoria.Visible = true;
            }
            else if ((TabContainer2.ActiveTabIndex.ToString() != "0"))
            {
                filaDetalleAuditoria.Visible = false;
            }
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        protected void SqlDataSource1_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertId = (int)e.Command.Parameters["@NewParameter"].Value;
        }

        private void EliminarAsignacionRecursos()
        {
            DataTable DtInfo = new DataTable();
            DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
            cAu.EliminarRecursosGruposAuditoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text), DtInfo.Rows[0]["IdObjetivo"].ToString().Trim(), ddlGrupoAud2.SelectedValue.ToString().Trim(), InfoGrid.Rows[RowGrid]["IdHijo"].ToString().Trim(), InfoGrid.Rows[RowGrid]["Etapa"].ToString().Trim());
            loadGrid();
            loadInfo();
        }

        private void CargarIdGrupoAud()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAu.VerIdGrupoAuditoriaGuardado(Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text));

            for (int i = 0; i < ddlGrupoAud2.Items.Count; i++)
            {
                ddlGrupoAud2.SelectedIndex = i;
                if (ddlGrupoAud2.SelectedValue.ToString().Trim() == dtInfo.Rows[0]["IdGrupoAuditoria"].ToString().Trim())
                {
                    break;
                }
                else
                {
                    ddlGrupoAud2.SelectedIndex = 0;
                }
            }


        }

        private void LoadAsignacionRecursos()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            //DropDownList1.SelectedIndex = 0;
            //DropDownList2.SelectedIndex = 0;

            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Text.ToString().Trim() == InfoGrid.Rows[RowGrid]["Etapa"].ToString().Trim())
                {
                    break;
                }
                else
                {
                    DropDownList1.SelectedIndex = 0;
                }
            }
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedItem.Text.ToString().Trim() == InfoGrid.Rows[RowGrid]["NombreResponsable"].ToString().Trim())
                {
                    break;
                }
                else
                {
                    DropDownList2.SelectedIndex = 0;
                }
            }

            TextBox1.Text = InfoGrid.Rows[RowGrid]["FechaInicial"].ToString().Trim();
            TextBox2.Text = InfoGrid.Rows[RowGrid]["FechaFinal"].ToString().Trim();
            TextBox3.Text = InfoGrid.Rows[RowGrid]["HorasPlaneadas"].ToString().Trim();
            lblCodNodoGA1.Text = InfoGrid.Rows[RowGrid]["IdHijo"].ToString().Trim();
            DiasHorasLaborables1();
        }

        private void loadDDLGrupoAuditoria()
        {
            DataTable dtInfo = new DataTable();
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));
            dtInfo = cAu.LoadGruposAuditoria(ddlGrupoAud2.SelectedValue.ToString());
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreResponsable"].ToString().Trim(), dtInfo.Rows[i]["idHijo"].ToString()));
            }
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Etapa", typeof(string));
            grid.Columns.Add("IdHijo", typeof(int));
            grid.Columns.Add("NombreResponsable", typeof(string));
            grid.Columns.Add("FechaInicial", typeof(string));
            grid.Columns.Add("FechaFinal", typeof(string));
            grid.Columns.Add("HorasPlaneadas", typeof(string));
            InfoGrid = grid;
            GdRecursos.DataSource = InfoGrid;
            GdRecursos.DataBind();
        }

        private void loadInfo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAu.LoasRecursosPlaneacion(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text), Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text), ddlGrupoAud2.SelectedValue.ToString());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {
                            dtInfo.Rows[rows]["Etapa"].ToString().Trim(),
                            dtInfo.Rows[rows]["IdHijo"].ToString().Trim(),                                          
                            dtInfo.Rows[rows]["NombreResponsable"].ToString().Trim(),
                            dtInfo.Rows[rows]["FechaInicial"].ToString().Trim(),                                                                  
                            dtInfo.Rows[rows]["FechaFinal"].ToString().Trim(),
                            dtInfo.Rows[rows]["HorasPlaneadas"].ToString().Trim()
                        });
                }

                GdRecursos.DataSource = InfoGrid;
                GdRecursos.DataBind();
                ddlGrupoAud2.Enabled = false;
            }
            else
            {
                TrVerEtapas.Visible = false;
                ddlGrupoAud2.Enabled = true;
            }
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (TabContainer2.ActiveTabIndex == 0)
            {
                if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Tema.", 2, "Atención");
                    txtNomAuditoria.Focus();
                }

                if (ddlEmpresa.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar la Empresa.", 2, "Atención");
                    txtNomAuditoria.Focus();
                }
                else if (ddlEstandar.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Programa/Estandár.", 2, "Atención");
                    ddlEstandar.Focus();
                }
                //04-04-2014
                else if (ddlGrupoAud2.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Grupo de Auditoría.", 2, "Atención");
                    ddlGrupoAud2.Focus();
                }
                else if (ddlNaturaleza.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar la Naturaleza.", 2, "Atención");
                    ddlNaturaleza.Focus();
                }
                else if (ddlNivelImportancia.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Nivel de Importancia.", 2, "Atención");
                    ddlNivelImportancia.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecIniA.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Fecha de Inicio de la Auditoría.", 2, "Atención");
                    txtFecIni.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecFinA.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Fecha Proyectada de Cierre de la Auditoría.", 2, "Atención");
                    txtFecFin.Focus();
                }
            }
            else if (TabContainer2.ActiveTabIndex == 1)
            {
                if (filaDetalleRICC.Visible == true)
                {
                    if (ddlCiclo.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Ciclo.", 2, "Atención");
                        ddlCiclo.Focus();
                    }
                    else if (ddlRI.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Riesgo Inherente.", 2, "Atención");
                        ddlRI.Focus();
                    }
                    else if (ddlCC.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar la Calificación Control.", 2, "Atención");
                        ddlCC.Focus();
                    }
                }
                else if (filaDetalleOF.Visible == true)
                {
                    if (ddlOF.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar Otros Factores.", 2, "Atención");
                        ddlOF.Focus();
                    }
                }
            }
            else if (TabContainer2.ActiveTabIndex == 2)
            {
                if (filaDetalleObjetivo.Visible == true)
                {
                    if (ddlObjetivo.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Objetivo.", 2, "Atención");
                        ddlObjetivo.Focus();
                    }
                    else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe ingresar el Alcance.", 2, "Atención");
                        txtAlcanceObj.Focus();
                    }
                    //04-04-2014
                    //else if (ddlGrupoAud.SelectedValue == "0")
                    //{
                    //    err = false;
                    //    omb.ShowMessage("Debe seleccionar el Grupo de Auditoria.", 2, "Atención");
                    //    ddlGrupoAud.Focus();
                    //}
                    //else if (ValidarCadenaVacia(txtFecIni.Text))
                    //{
                    //    err = false;
                    //    omb.ShowMessage("Debe ingresar la Fecha Inicial.", 2, "Atención");
                    //    txtFecIni.Focus();
                    //}
                    //else if (ValidarCadenaVacia(txtFecFin.Text))
                    //{
                    //    err = false;
                    //    omb.ShowMessage("Debe ingresar la Fecha Final.", 2, "Atención");
                    //    txtFecFin.Focus();
                    //}
                }
                else if (filaDetalleRecursos.Visible == true)
                {
                    if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtRecurso.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Recurso.", 2, "Atención");
                        //txtAlcanceObj.Focus();
                    }
                    else if (ddlEtapa.SelectedValue == "")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar la Etapa.", 2, "Atención");
                        ddlEtapa.Focus();
                    }
                    else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecIniRec.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe ingresar la Fecha Inicial.", 2, "Atención");
                        txtFecIniRec.Focus();
                    }
                    else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecFinRec.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe ingresar la Fecha Final.", 2, "Atención");
                        txtFecFinRec.Focus();
                    }
                    else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtHorasPlan.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe ingresar el Número de Horas Planeadas.", 2, "Atención");
                        txtHorasPlan.Focus();
                    }
                }
                else if (filaDetalleObjEnfoque.Visible == true)
                {
                    if (ddlEnfoque.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Enfoque.", 2, "Atención");
                        ddlEnfoque.Focus();
                    }
                }
            }
            return err;
        }

        protected Boolean ValidarCadenaVacia(string cadena)
        {
            Regex rx = new Regex(@"^-?\d+(\.\d{2})?$");
            string Espacio = "<br>";
            string Div = "<div>";
            string Div2 = "</div>";
            string b = "<b>";
            string b2 = "</b>";
            string cadena2 = "";

            cadena2 = Regex.Replace(cadena, Espacio, " ");
            cadena2 = Regex.Replace(cadena2, Div, " ");
            cadena2 = Regex.Replace(cadena2, Div2, " ");
            cadena2 = Regex.Replace(cadena2, b, " ");
            cadena2 = Regex.Replace(cadena2, b2, " ");

            if (cadena2.Trim() == "")
                return (true);
            else
                return (false);
        }

        protected void DiasHorasLaborables()
        {
            string selectCommand = "";
            int HorasDisp;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            selectCommand = "SELECT CONVERT(VARCHAR(10),Min(FechaInicial),103) AS FechaInicial, CONVERT(VARCHAR(10),Max(FechaFinal),103) AS FechaFinal, Parametrizacion.FN_Horas_Laborables(" + lblCodNodoGA.Text.Trim() + ") AS HorasLab FROM Auditoria.AudObjRecurso WHERE IdHijo = " + lblCodNodoGA.Text.Trim();
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                txtFecIniMR.Text = row["FechaInicial"].ToString().Trim();
                txtFecFinMR.Text = row["FechaFinal"].ToString().Trim();
                if (row["HorasLab"].ToString().Trim() == "0")
                    txtHorasLab.Text = "";
                else
                    txtHorasLab.Text = row["HorasLab"].ToString().Trim();
            }

            selectCommand = "SELECT SUM(HorasPlaneadas) as Suma FROM Auditoria.AudObjRecurso WHERE IdHijo = " + lblCodNodoGA.Text.Trim();
            dad = new SqlDataAdapter(selectCommand, conString);
            dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                if (row["Suma"].ToString().Trim() == "0")
                    txtHorasUtil.Text = "";
                else
                    txtHorasUtil.Text = row["Suma"].ToString().Trim();
            }

            if (txtHorasLab.Text != "")
            {
                HorasDisp = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtHorasLab.Text.Trim())) - Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtHorasUtil.Text.Trim()));
                txtHorasDisp.Text = HorasDisp.ToString().Trim();
                if (HorasDisp <= 0)
                    txtHorasDisp.Attributes["style"] = "color:red;";
                else
                    txtHorasDisp.Attributes["style"] = "color:green;";
            }
        }

        protected void DiasHorasLaborables1()
        {
            string selectCommand = "";
            int HorasDisp;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            selectCommand = "SELECT CONVERT(VARCHAR(10),Min(FechaInicial),103) AS FechaInicial, CONVERT(VARCHAR(10),Max(FechaFinal),103) AS FechaFinal, Parametrizacion.FN_Horas_Laborables(" + lblCodNodoGA1.Text.Trim() + ") AS HorasLab FROM Auditoria.AudObjRecurso WHERE IdHijo = " + lblCodNodoGA1.Text.Trim();
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                txtFecIniMR1.Text = row["FechaInicial"].ToString().Trim();
                txtFecFinMR1.Text = row["FechaFinal"].ToString().Trim();
                if (row["HorasLab"].ToString().Trim() == "0")
                    txtHorasLab1.Text = "";
                else
                    txtHorasLab1.Text = row["HorasLab"].ToString().Trim();
            }

            selectCommand = "SELECT SUM(HorasPlaneadas) as Suma FROM Auditoria.AudObjRecurso WHERE IdHijo = " + lblCodNodoGA1.Text.Trim();
            dad = new SqlDataAdapter(selectCommand, conString);
            dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                if (row["Suma"].ToString().Trim() == "0")
                    txtHorasUtil1.Text = "";
                else
                    txtHorasUtil1.Text = row["Suma"].ToString().Trim();
            }

            if (txtHorasLab1.Text != "")
            {
                HorasDisp = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtHorasLab1.Text.Trim())) - Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtHorasUtil1.Text.Trim()));
                txtHorasDisp1.Text = HorasDisp.ToString().Trim();
                if (HorasDisp <= 0)
                    txtHorasDisp1.Attributes["style"] = "color:red;";
                else
                    txtHorasDisp1.Attributes["style"] = "color:green;";
            }
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
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
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    #region
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource201.Insert();
                    #endregion
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
                    #region
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
    }
}

