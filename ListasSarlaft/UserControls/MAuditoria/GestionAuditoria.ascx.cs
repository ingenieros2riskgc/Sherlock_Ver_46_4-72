using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Net.Mail;
using System.IO;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class GestionAuditoria : System.Web.UI.UserControl
    {
        string IdFormulario = "3004";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();

        private static int LastInsertIdCE;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
                scripManager.RegisterPostBackControl(imgBtnAdjuntar);
                scripManager.RegisterPostBackControl(GridView100);
                scripManager.RegisterPostBackControl(imgBtnInsertarArchivo);
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.BtnAdjuntar);

                if (!Page.IsPostBack)
                {
                    PopulateTreeView();

                    GridView1.DataBind();

                    TabContainer1.ActiveTabIndex = 0;
                }
            }
        }

        #region Treview
        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeView()
        {
            TreeNodeCollection nodes = this.TreeView1.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData, 1);
            }

            nodes = this.TreeView2.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData2 = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData2, 2);
            }

            nodes = this.TreeView3.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData3 = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData3, 3);
            }
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
        private void AddTopTreeViewNodes(DataTable treeViewData, int Arbol)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                if (Arbol == 1)
                    TreeView1.Nodes.Add(newNode);
                else if (Arbol == 2)
                    TreeView2.Nodes.Add(newNode);
                else if (Arbol == 3)
                    TreeView3.Nodes.Add(newNode);
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
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (filaDetalleRecomendacion.Visible == true)
            {
                txtDependenciaRec1.Text = TreeView1.SelectedNode.Text;
                lblIdDependenciaRec1.Text = TreeView1.SelectedNode.Value;
            }

        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependenciaRec2.Text = TreeView2.SelectedNode.Text;
            lblIdDependenciaRec2.Text = TreeView2.SelectedNode.Value;

            string subcadena;
            subcadena = TreeView2.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);
            lblCorreoDepedenciaRec2.Text = subcadena;
        }

        protected void TreeView3_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependenciaRie.Text = TreeView3.SelectedNode.Text;
            lblIdDependenciaRie.Text = TreeView3.SelectedNode.Value;

            string subcadena;
            subcadena = TreeView3.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);
            lblCorreoDependenciaRie.Text = subcadena;
        }
        #endregion Treview

        #region DDL
        protected void ddlRecPoD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRecPoD.SelectedItem.Value == "Procesos")
            {
                filaDependenciaRec.Visible = false;
                filaProcesoRec.Visible = true;
            }
            else
            {
                filaDependenciaRec.Visible = true;
                filaProcesoRec.Visible = false;
            }
        }

        protected void ddlRiePoD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRiePoD.SelectedItem.Value == "Procesos")
            {
                filaDependenciaRie.Visible = false;
                filaProcesoRie.Visible = true;
            }
            else
            {
                filaDependenciaRie.Visible = true;
                filaProcesoRie.Visible = false;
            }
        }

        protected void ddlMacroProceso_DataBound(object sender, EventArgs e)
        {
            ddlMacroProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProceso_DataBound(object sender, EventArgs e)
        {
            ddlProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlMacroProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProceso.Items.Clear();
            ddlProceso.DataBind();
            txtProcesoRec.Text = "";

        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filaDetalleRecomendacion.Visible == true)
            {
                txtProcesoRec.Text = ddlProceso.SelectedItem.Text;
                lblIdProcesoRec.Text = ddlProceso.SelectedValue;
            }
            else if (filaDetalleRiesgo.Visible == true)
            {
                txtProcesoRie.Text = ddlProceso.SelectedItem.Text;
                lblIdProcesoRie.Text = ddlProceso.SelectedValue;
            }
        }

        protected void ddlMacroProcesoRie_DataBound(object sender, EventArgs e)
        {
            ddlMacroProcesoRie.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProcesoRie_DataBound(object sender, EventArgs e)
        {
            ddlProcesoRie.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlMacroProcesoRie_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesoRie.Items.Clear();
            ddlProcesoRie.DataBind();
            txtProcesoRie.Text = "";
        }

        protected void ddlProcesoRie_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProcesoRie.Text = ddlProcesoRie.SelectedItem.Text;
            lblIdProcesoRie.Text = ddlProcesoRie.SelectedValue;
        }

        protected void ddlTipoHallazgo_DataBound(object sender, EventArgs e)
        {
            ddlTipoHallazgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEstadoHallazgo_DataBound(object sender, EventArgs e)
        {
            ddlEstadoHallazgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlTipoRiesgo_DataBound(object sender, EventArgs e)
        {
            ddlTipoRiesgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlProbabilidad_DataBound(object sender, EventArgs e)
        {
            ddlProbabilidad.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlImpacto_DataBound(object sender, EventArgs e)
        {
            ddlImpacto.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlNivelRiesgo_DataBound(object sender, EventArgs e)
        {
            ddlNivelRiesgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        #endregion

        #region Buttons
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalle.Visible = false;
            //filaAuditoria.Visible = true;
            filaBtnTemas.Visible = false;
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
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
                if (TabContainer1.ActiveTabIndex.ToString() == "0")
                {
                    SqlDataSource1.DeleteParameters["IdHallazgo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodHallazgo.Text);
                    SqlDataSource1.Delete();
                }
                else if (TabContainer1.ActiveTabIndex.ToString() == "1")
                {
                    SqlDataSource22.DeleteParameters["IdRecomendacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodRec.Text);
                    SqlDataSource22.Delete();
                }
                else if (TabContainer1.ActiveTabIndex.ToString() == "2")
                {
                    SqlDataSource23.DeleteParameters["IdRiesgo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodRiesgo.Text);
                    SqlDataSource23.Delete();
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
                if (TabContainer1.ActiveTabIndex.ToString() == "0")
                {
                    TabContainer1.Tabs[1].Enabled = false;
                    TabContainer1.Tabs[2].Enabled = false;
                }
            }
        }

        protected void btnImgCancelarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleHallazgo.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;
        }

        protected void btnImgCancelarRec_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRecomendacion.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgCancelarRie_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRiesgo.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgEliminarRec_Click(object sender, ImageClickEventArgs e)
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

        protected void btnImgEliminarRie_Click(object sender, ImageClickEventArgs e)
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

        #region Hallazgo
        protected void btnImgInsertarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            if (VerificarCampos())
            {
                SqlDataSource1.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
                SqlDataSource1.InsertParameters["IdDetalleEnfoque"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodLiteralSel.Text);
                SqlDataSource1.InsertParameters["IdDetalleTipoHallazgo"].DefaultValue = ddlTipoHallazgo.SelectedValue;
                SqlDataSource1.InsertParameters["IdEstado"].DefaultValue = ddlEstadoHallazgo.SelectedValue;
                SqlDataSource1.InsertParameters["Hallazgo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtHallazgo.Text);
                SqlDataSource1.InsertParameters["ComentarioAuditado"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtComentario.Text);
                SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource1.InsertParameters["IdNivelRiesgo"].DefaultValue = ddlNivelRiesgo.SelectedValue;

                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource1.Insert();
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaDetalleHallazgo.Visible = false;
                    filaAcciones.Visible = true;
                }
            }
        }

        protected void btnImgActualizarHallazgo_Click(object sender, ImageClickEventArgs e)
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
                        SqlDataSource1.UpdateParameters["IdDetalleTipoHallazgo"].DefaultValue = ddlTipoHallazgo.SelectedValue;
                        SqlDataSource1.UpdateParameters["IdEstado"].DefaultValue = ddlEstadoHallazgo.SelectedValue;
                        SqlDataSource1.UpdateParameters["Hallazgo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtHallazgo.Text);
                        SqlDataSource1.UpdateParameters["IdHallazgo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodHallazgo.Text);
                        SqlDataSource1.UpdateParameters["ComentarioAuditado"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtComentario.Text);
                        SqlDataSource1.UpdateParameters["IdNivelRiesgo"].DefaultValue = ddlNivelRiesgo.SelectedValue;

                        SqlDataSource1.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaDetalleHallazgo.Visible = false;
                        filaAcciones.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgEliminarHallazgo_Click(object sender, ImageClickEventArgs e)
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
        #endregion

        protected void btnImgInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            if (VerificarCampos())
            {
                SqlDataSource22.InsertParameters["IdHallazgo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodHallazgoGen.Text);
                SqlDataSource22.InsertParameters["Tipo"].DefaultValue = ddlRecPoD.SelectedValue;

                if (ddlRecPoD.SelectedValue == "Dependencia")
                    SqlDataSource22.InsertParameters["IdDependenciaAuditada"].DefaultValue = lblIdDependenciaRec2.Text;
                else
                    SqlDataSource22.InsertParameters["IdSubproceso"].DefaultValue = lblIdProcesoRec.Text;

                SqlDataSource22.InsertParameters["IdDependenciaRespuesta"].DefaultValue = lblIdDependenciaRec1.Text;
                SqlDataSource22.InsertParameters["Estado"].DefaultValue = "Formulado";
                SqlDataSource22.InsertParameters["Observacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtRecomendacion.Text);
                SqlDataSource22.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource22.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource22.Insert();
                }
                catch (Exception except)
                {
                    //Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaDetalleRecomendacion.Visible = false;
                    filaAcciones.Visible = true;
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgActualizarRec_Click(object sender, ImageClickEventArgs e)
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
                        SqlDataSource22.UpdateParameters["IdRecomendacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodRec.Text);
                        SqlDataSource22.UpdateParameters["Tipo"].DefaultValue = ddlRecPoD.SelectedValue;

                        if (ddlRecPoD.SelectedValue == "Dependencia")
                        {
                            SqlDataSource22.UpdateParameters["IdDependenciaAuditada"].DefaultValue = lblIdDependenciaRec2.Text;
                            SqlDataSource22.UpdateParameters["IdSubproceso"].DefaultValue = null;
                        }
                        else
                        {
                            SqlDataSource22.UpdateParameters["IdSubproceso"].DefaultValue = lblIdProcesoRec.Text;
                            SqlDataSource22.UpdateParameters["IdDependenciaAuditada"].DefaultValue = null;
                        }

                        SqlDataSource22.UpdateParameters["IdDependenciaRespuesta"].DefaultValue = lblIdDependenciaRec1.Text;
                        SqlDataSource22.UpdateParameters["Observacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtRecomendacion.Text);
                        SqlDataSource22.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaDetalleRecomendacion.Visible = false;
                        filaAcciones.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            if (VerificarCampos())
            {
                SqlDataSource23.InsertParameters["IdHallazgo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodHallazgoGen.Text);
                SqlDataSource23.InsertParameters["Nombre"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNomRiesgo.Text);
                SqlDataSource23.InsertParameters["IdDetalleTipoRiesgo"].DefaultValue = ddlTipoRiesgo.SelectedValue;
                SqlDataSource23.InsertParameters["Tipo"].DefaultValue = ddlRiePoD.SelectedValue;

                if (ddlRiePoD.SelectedValue == "Dependencia")
                {
                    SqlDataSource23.InsertParameters["IdDependencia"].DefaultValue = lblIdDependenciaRie.Text;
                    SqlDataSource23.InsertParameters["IdSubproceso"].DefaultValue = null;
                }
                else
                {
                    SqlDataSource23.InsertParameters["IdDependencia"].DefaultValue = null;
                    SqlDataSource23.InsertParameters["IdSubproceso"].DefaultValue = lblIdProcesoRie.Text;
                }

                SqlDataSource23.InsertParameters["Estado"].DefaultValue = "Formulado";
                SqlDataSource23.InsertParameters["Observacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescRiesgo.Text);
                SqlDataSource23.InsertParameters["IdDetalleTipoProbabilidad"].DefaultValue = ddlProbabilidad.SelectedValue;
                SqlDataSource23.InsertParameters["IdDetalleTipoImpacto"].DefaultValue = ddlImpacto.SelectedValue;

                SqlDataSource23.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource23.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource23.Insert();
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaDetalleRiesgo.Visible = false;
                    filaAcciones.Visible = true;
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgActualizarRie_Click(object sender, ImageClickEventArgs e)
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
                        SqlDataSource23.UpdateParameters["IdRiesgo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodRiesgo.Text);
                        SqlDataSource23.UpdateParameters["Nombre"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNomRiesgo.Text);
                        SqlDataSource23.UpdateParameters["IdDetalleTipoRiesgo"].DefaultValue = ddlTipoRiesgo.SelectedValue;
                        SqlDataSource23.UpdateParameters["Tipo"].DefaultValue = ddlRiePoD.SelectedValue;

                        if (ddlRiePoD.SelectedValue == "Dependencia")
                        {
                            SqlDataSource23.UpdateParameters["IdDependencia"].DefaultValue = lblIdDependenciaRie.Text;
                            SqlDataSource23.UpdateParameters["IdSubproceso"].DefaultValue = null;
                        }
                        else
                        {
                            SqlDataSource23.UpdateParameters["IdDependencia"].DefaultValue = null;
                            SqlDataSource23.UpdateParameters["IdSubproceso"].DefaultValue = lblIdProcesoRie.Text;
                        }

                        SqlDataSource23.UpdateParameters["Observacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescRiesgo.Text);
                        SqlDataSource23.UpdateParameters["IdDetalleTipoProbabilidad"].DefaultValue = ddlProbabilidad.SelectedValue;
                        SqlDataSource23.UpdateParameters["IdDetalleTipoImpacto"].DefaultValue = ddlImpacto.SelectedValue;
                        SqlDataSource23.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaDetalleRiesgo.Visible = false;
                        filaAcciones.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgActualizarMetodologia_Click(object sender, ImageClickEventArgs e)
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
                    SqlDataSource9.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
                    SqlDataSource9.UpdateParameters["Metodologia"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtMetodologia.Text);
                    SqlDataSource9.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }

                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgCancelarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        protected void btnImgActualizarConclusion_Click(object sender, ImageClickEventArgs e)
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
                    SqlDataSource12.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
                    SqlDataSource12.UpdateParameters["Conclusion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtConclusion.Text);
                    SqlDataSource12.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }

                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgCancelarTareaP_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleTareasP.Visible = false;
            filaGridTareasP.Visible = true;
        }

        protected void btnImgActualizarEnc_Click(object sender, ImageClickEventArgs e)
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
                    SqlDataSource18.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
                    SqlDataSource18.UpdateParameters["Encabezado"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtEncabezado.Text);
                    SqlDataSource18.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgCancelarEnc_Click(object sender, ImageClickEventArgs e)
        {
            filaEncabezado.Visible = false;
            filaAcciones.Visible = true;
        }

        protected void btnImgActualizarObs_Click(object sender, ImageClickEventArgs e)
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
                    SqlDataSource14.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text.Trim());
                    SqlDataSource14.UpdateParameters["Observaciones"].DefaultValue = Sanitizer.GetSafeHtmlFragment(tbxObs.Text.Trim());
                    SqlDataSource14.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnVolverArchivo_Click(object sender, EventArgs e)
        {
            filaTabGestion.Visible = true;
            filaAnexos.Visible = false;
            filaAcciones.Visible = true;
        }

        protected void imgBtnRiesgo_Click(object sender, EventArgs e)
        {
            TabContainer1.Tabs[1].Enabled = true;
            TabContainer1.Tabs[2].Enabled = true;
            GridView11.DataBind();
            TabContainer1.ActiveTabIndex = 2;
        }

        protected void imgBtnRecomendacion_Click(object sender, EventArgs e)
        {

            TabContainer1.Tabs[1].Enabled = true;
            TabContainer1.Tabs[2].Enabled = true;
            GridView13.DataBind();
            TabContainer1.ActiveTabIndex = 1;
        }

        protected void imgBtnInsertarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtCodHallazgo.Text = "";
                ddlTipoHallazgo.SelectedValue = null;
                ddlTipoHallazgo.Focus();
                txtHallazgo.Text = "";
                txtComentario.Text = "";
                ddlEstadoHallazgo.SelectedValue = null;
                txtFecCreacionHallazgo.Text = "";
                filaTabGestion.Visible = false;
                filaDetalleHallazgo.Visible = true;
                txtUsuarioHallazgo.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionHallazgo.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarHallazgo.Visible = false;
                btnImgInsertarHallazgo.Visible = true;
                filaAcciones.Visible = false;
            }
        }

        protected void imgBtnInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlRecPoD.SelectedValue = "Procesos";
                ddlRecPoD.Focus();
                filaProcesoRec.Visible = true;
                filaDependenciaRec.Visible = false;
                txtCodRec.Text = "";
                txtDependenciaRec1.Text = "";
                txtDependenciaRec2.Text = "";
                lblCorreoDepedenciaRec2.Text = "";
                txtProcesoRec.Text = "";
                txtRecomendacion.Text = "";
                txtUsuarioRec.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;
                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;
                txtFecCreacionRec.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarRec.Visible = false;
                btnImgInsertarRec.Visible = true;
                filaTabGestion.Visible = false;
                filaDetalleRecomendacion.Visible = true;
                filaAcciones.Visible = false;
            }
        }

        protected void imgBtnInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtNomRiesgo.Text = "";
                ddlTipoRiesgo.Focus();
                ddlTipoRiesgo.SelectedValue = null;
                ddlRiePoD.SelectedValue = "Procesos";
                filaProcesoRie.Visible = true;
                filaDependenciaRie.Visible = false;
                txtCodRiesgo.Text = "";
                txtDependenciaRie.Text = "";
                lblCorreoDependenciaRie.Text = "";
                txtProcesoRie.Text = "";
                txtDescRiesgo.Text = "";
                txtUsuarioRie.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                ddlProbabilidad.SelectedValue = null;
                ddlImpacto.SelectedValue = null;
                txtFecCreacionRie.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarRie.Visible = false;
                btnImgInsertarRie.Visible = true;
                filaTabGestion.Visible = false;
                filaDetalleRiesgo.Visible = true;
                filaAcciones.Visible = false;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            filaGridTareasP.Visible = false;

            if (txtNomLiteralSel.Text != "")
                filaTabGestion.Visible = true;
            else
                filaTabGestion.Visible = false;

            TabContainer1.ActiveTabIndex = 0;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaAcciones.Visible = true;
        }

        #region Barra Acciones abajo
        protected void btnPreCierreHa_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                //txtCodAuditoriaSel
                cAu.PrecierreHallazgos(Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text), Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text), "PRE INFORME");
                cAu.ActualizarLogHistoricoAudutoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text), "NULL", "GETDATE()", "NULL", "NO");
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaMetodologia.Visible = false;
                filaConclusion.Visible = false;
                filaObs.Visible = false;
                filaEncabezado.Visible = false;
                filaGridTareasP.Visible = false;

                omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
            }

            //29-04-2014
            try
            {
                #region Envio de Notificacion precierre
                //enviar la notificacion
                DataTable DtLider = new DataTable();
                DtLider = cAu.LiderGrAudutoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text));

                if (DtLider != null)
                {
                    if (DtLider.Rows.Count > 0)
                    {
                        #region Notificacion 1
                        string InfAdicional = "Se ha realizado el siguiente Precierre de Hallazgos:<br /><br />";
                        InfAdicional += "Planeación: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br />";
                        InfAdicional += "Auditoría: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoriaSel.Text) + "<br />";
                        InfAdicional += "Fecha Precierre: " + System.DateTime.Now.ToString() + "<br />";

                        boolEnviarNotificacion(20, Convert.ToInt16("0"), Convert.ToInt16(DtLider.Rows[0]["idPadre"].ToString()), "", InfAdicional);
                        #endregion

                        #region Notificacion 2
                        string strAsunto = string.Format("Generación Hallazgos Auditoria {0}", Sanitizer.GetSafeHtmlFragment(txtNomAuditoriaSel.Text.Trim()));
                        string strPlaneacion = string.Format("Código Planeación: {0} <br /> Descripción Planeación: {1} ",
                           Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text.Trim()));
                        string strAuditoria = string.Format("Código Auditoría: {0} <br /> Descripción Auditoría: {1} ",
                            Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text), Sanitizer.GetSafeHtmlFragment(txtNomAuditoriaSel.Text));

                        string strHallazgo = mtdConsultaHallazgos(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text.Trim()));

                        boolEnviarNotificacion(20, 0, Convert.ToInt16(DtLider.Rows[0]["idPadre"].ToString()),
                            strAsunto, strPlaneacion, strAuditoria, strHallazgo);
                        #endregion
                    }
                    else
                        omb.ShowMessage("No ha información configurada para enviar la notificación.<br/>", 1, "Atención");
                }
                else
                    omb.ShowMessage("No ha información configurada para enviar la notificación.<br/>", 1, "Atención");
                #endregion

            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al enviar la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
            }

            #region Limpiar campos
            txtCodPlaneacion.Text = string.Empty;
            txtNomPlaneacion.Text = string.Empty;
            txtCodAuditoriaSel.Text = string.Empty;
            txtNomAuditoriaSel.Text = string.Empty;
            txtCodObjetivoSel.Text = string.Empty;
            txtNomObjetivoSel.Text = string.Empty;
            txtCodEnfoqueSel.Text = string.Empty;
            txtNomEnfoqueSel.Text = string.Empty;
            txtCodLiteralSel.Text = string.Empty;
            txtNomLiteralSel.Text = string.Empty;
            #endregion
        }

        protected void btnEncabezado_Click(object sender, EventArgs e)
        {
            filaTabGestion.Visible = false;
            filaAcciones.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaEncabezado.Visible = true;

            txtEncabezado.Focus();
        }

        protected void btnTareasPendientes_Click(object sender, EventArgs e)
        {
            filaGridTareasP.Visible = true;
            filaTabGestion.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaAcciones.Visible = false;
        }

        #region Reporte
        protected void bntInformeRec_Click(object sender, EventArgs e)
        {
            string str;
            str = "window.open('AudAdmReporteSeguimiento.aspx?Ca=" + Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text) + "','Reporte','width=800,height=600,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }
        #endregion

        protected void btnTemasAud_Click(object sender, EventArgs e)
        {
            //filaAuditoria.Visible = true;
            filaDetalle.Visible = false;
            filaBtnTemas.Visible = false;
            GridView1.DataBind();
        }
        #endregion

        #region  Adjuntar
        protected void imgBtnAdjuntar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    /*if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                    {*/
                        mtdCargarPdfGestAudit();
                        GridView100.DataBind();
                        omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                    /*}
                    else
                        omb.ShowMessage("Solamente se permiten cargar archivos PDF!", 2, "Atención");*/
                }
                else
                {
                    //omb.ShowMessage("No hay archivos para cargar.", 2, "Atención");
                    lblMsgBox.Text = "No hay archivos para cargar.";
                    mpeMsgBox.Show();
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al agregar el archivo. [Error: " + ex.Message + " ]", 2, "Atención");
            }
        }
        #endregion

        protected void imgBtnInsertarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                filaGridAnexos.Visible = false;
                filaSubirAnexos.Visible = true;
                FileUpload1.Focus();
                txtDescArchivo.Text = "";
            }
        }
        #endregion Buttons

        #region GridViews

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaDetalleHallazgo.Visible = true;
                btnImgActualizarHallazgo.Visible = true;
                btnImgInsertarHallazgo.Visible = false;
                filaAcciones.Visible = false;
            }
            else if (e.CommandArgument.ToString() == "Anexar")
            {
                filaAnexos.Visible = true;
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;
                filaAcciones.Visible = false;

                //filaVolver.Visible = false;
                filaTabGestion.Visible = false;
                //GridView100.DataBind();
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaGridTareasP.Visible = false;
                filaDetalleTareasP.Visible = true;
            }
        }

        protected void GridView11_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaDetalleRecomendacion.Visible = true;
                btnImgInsertarRec.Visible = false;
                btnImgActualizarRec.Visible = true;
                filaAcciones.Visible = false;
            }
        }

        protected void GridView13_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaDetalleRiesgo.Visible = true;
                btnImgInsertarRie.Visible = false;
                btnImgActualizarRie.Visible = true;
                filaAcciones.Visible = false;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodHallazgo.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                lblIdHallazgo.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                ddlTipoHallazgo.SelectedValue = GridView1.SelectedDataKey[1].ToString().Trim();
                ddlTipoHallazgo.Focus();
                txtHallazgo.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
                txtComentario.Text = GridView1.SelectedDataKey[3].ToString().Trim();
                ddlEstadoHallazgo.SelectedValue = GridView1.SelectedDataKey[2].ToString().Trim();
                txtUsuarioHallazgo.Text = GridView1.SelectedDataKey[5].ToString().Trim();
                txtFecCreacionHallazgo.Text = GridView1.SelectedRow.Cells[7].Text.Trim();
                txtHallazgoGen.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
                txtCodHallazgoGen.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtHallazgoRec.Text = txtHallazgoGen.Text;
                txtHallazgoRie.Text = txtHallazgoGen.Text;

                if (GridView1.SelectedDataKey[6].ToString() != "0")
                    ddlNivelRiesgo.SelectedValue = GridView1.SelectedDataKey[6].ToString().Trim();

                TabContainer1.Tabs[1].Enabled = true;
                TabContainer1.Tabs[2].Enabled = true;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoTP.Text = GridView2.SelectedRow.Cells[0].Text.Trim();
            txtTarea.Text = GridView2.SelectedRow.Cells[2].Text.Trim();
            ddlEstado.SelectedValue = GridView2.SelectedRow.Cells[3].Text.Trim();
            txtRecurso.Text = GridView2.SelectedDataKey[4].ToString().Trim();
            txtTarea.Focus();
            txtUsuarioTP.Text = GridView2.SelectedDataKey[2].ToString().Trim(); ; //Aca va el codigo de usuario logueado
            txtFecCreacionTP.Text = GridView2.SelectedRow.Cells[4].Text.Trim(); ;
        }

        protected void GridView6_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodAuditoriaSel.Text = GridView6.SelectedRow.Cells[0].Text.Trim();
            txtNomAuditoriaSel.Text = GridView6.SelectedRow.Cells[1].Text.Trim();
            txtMetodologiaGen.Text = GridView6.SelectedDataKey[0].ToString().Trim();
            txtConclusionGen.Text = GridView6.SelectedDataKey[1].ToString().Trim();
            tbxObsGen.Text = GridView6.SelectedDataKey[2].ToString().Trim();
            txtEncabezado.Text = GridView6.SelectedDataKey[3].ToString().Trim();

            txtCodObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            //txtNomEnfoqueSel.Height = 18;
            //txtNomEnfoqueSel.Width = 402;
            //txtNomLiteralSel.Height = 18;
            //txtNomLiteralSel.Width = 402;

            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            popupAuditoria.Cancel();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            filaAcciones.Visible = true;
            btnTareasPEndientes.Visible = false;

            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
        }

        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodObjetivoSel.Text = GridView7.SelectedRow.Cells[0].Text.Trim();
            txtNomObjetivoSel.Text = GridView7.SelectedRow.Cells[1].Text.Trim();
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            //txtNomEnfoqueSel.Height = 18;
            //txtNomEnfoqueSel.Width = 402;
            //txtNomLiteralSel.Height = 18;
            //txtNomLiteralSel.Width = 402;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            popupObjetivo.Cancel();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;
            filaAcciones.Visible = true;
            GridView2.DataBind();

            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
        }

        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();

            txtCodAuditoriaSel.Text = "";
            txtCodObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomAuditoriaSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            //txtNomEnfoqueSel.Height = 18;
            ////xtNomEnfoqueSel.Width = 402;
            //txtNomLiteralSel.Height = 18;
            //txtNomLiteralSel.Width = 402;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            filaAcciones.Visible = false;

            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            popupPlanea.Cancel();
        }

        protected void GridView9_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodEnfoqueSel.Text = GridView9.SelectedRow.Cells[0].Text.Trim();
            txtNomEnfoqueSel.Text = GridView9.SelectedRow.Cells[1].Text.Trim();
            txtCodLiteralSel.Text = "";
            txtNomLiteralSel.Text = "";

            //txtNomEnfoqueSel.Height = 18;
            //txtNomEnfoqueSel.Width = 402;
            // txtNomLiteralSel.Height = 18;
            //txtNomLiteralSel.Width = 402;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            filaAcciones.Visible = true;
            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
            popupEnfoque.Cancel();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;

            //int rows = 0, longMax = 0, rowsAdd = 0;
            //double temp = 0;

            //Cambia la altura y el ancho del labol de Enfoque
            ////Revisa la longitud max del texto y el número de líneas
            //foreach (string strItem in Regex.Split(GridView9.SelectedRow.Cells[1].Text, "</div>"))
            //{
            //    rows = rows + 1;
            //    if (strItem.Length > longMax) longMax = strItem.Length;
            //    if (strItem.Length > 126)
            //    {
            //        temp = strItem.Length / 126;
            //        rowsAdd = rowsAdd + (int)Math.Truncate(temp);
            //    }
            //}

            //if (rows + rowsAdd > 1) txtNomEnfoqueSel.Height = (rows + rowsAdd) * 18;

            //if (longMax > 72)
            //    txtNomEnfoqueSel.Width = 700;

            //else
            //    txtNomEnfoqueSel.Width = 402;
        }

        protected void GridView10_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rows = 0, longMax = 0, rowsAdd = 0;
            double temp = 0;

            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;

            //string[] lines = Regex.Split(value, "</div");
            //Revisa la longitud max del texto y el número de líneas
            foreach (string strItem in Regex.Split(GridView10.SelectedRow.Cells[1].Text, "</div>"))
            {
                rows = rows + 1;
                if (strItem.Length > longMax) longMax = strItem.Length;
                if (strItem.Length > 126)
                {
                    temp = strItem.Length / 126;
                    rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                }
            }

            txtNomLiteralSel.Text = GridView10.SelectedRow.Cells[1].Text.Trim();
            txtCodLiteralSel.Text = GridView10.SelectedRow.Cells[0].Text.Trim();

            if (rows + rowsAdd > 1) txtNomLiteralSel.Height = (rows + rowsAdd) * 18;

            if (longMax > 72)
                txtNomLiteralSel.Width = 700;

            else
                txtNomLiteralSel.Width = 402;

            //popupLiteral.Cancel();

            filaTabGestion.Visible = true;
            filaDetalleHallazgo.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            filaAcciones.Visible = true;
            btnTareasPEndientes.Visible = true;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;

            GridView1.DataBind();
            ddlTipoHallazgo.Items.Clear();
            ddlTipoHallazgo.DataBind();

            txtMetodologia.Text = Sanitizer.GetSafeHtmlFragment(txtMetodologiaGen.Text);
            txtConclusion.Text = Sanitizer.GetSafeHtmlFragment(txtConclusionGen.Text);
            tbxObs.Text = Sanitizer.GetSafeHtmlFragment(tbxObsGen.Text);

            TabContainer1.ActiveTabIndex = 0;
        }

        protected void GridView11_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (GridView11.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlRecPoD.SelectedValue = GridView11.SelectedRow.Cells[3].Text.Trim();
                ddlRecPoD.Focus();
                if (GridView11.SelectedRow.Cells[3].Text == "Procesos")
                {
                    txtProcesoRec.Text = GridView11.SelectedDataKey[1].ToString().Trim();
                    lblIdProcesoRec.Text = GridView11.SelectedDataKey[4].ToString().Trim();
                    lblIdDependenciaRec2.Text = "";
                    txtDependenciaRec2.Text = "";
                    lblCorreoDepedenciaRec2.Text = "";
                    filaProcesoRec.Visible = true;
                    filaDependenciaRec.Visible = false;
                }
                else
                {
                    txtDependenciaRec2.Text = GridView11.SelectedDataKey[1].ToString().Trim();
                    lblIdDependenciaRec2.Text = GridView11.SelectedDataKey[0].ToString().Trim();
                    lblCorreoDepedenciaRec2.Text = GridView11.SelectedDataKey[8].ToString().Trim();
                    lblIdProcesoRec.Text = "";
                    txtProcesoRec.Text = "";
                    filaProcesoRec.Visible = false;
                    filaDependenciaRec.Visible = true;
                }

                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;
                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;

                txtCodRec.Text = GridView11.SelectedRow.Cells[0].Text.Trim();
                txtDependenciaRec1.Text = GridView11.SelectedDataKey[3].ToString().Trim();
                lblIdDependenciaRec1.Text = GridView11.SelectedDataKey[2].ToString().Trim();
                txtRecomendacion.Text = GridView11.SelectedRow.Cells[10].Text.Trim();
                txtUsuarioRec.Text = GridView11.SelectedDataKey[7].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = GridView11.SelectedRow.Cells[11].Text.Trim();

            }
        }

        protected void GridView13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView13.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodRiesgo.Text = GridView13.SelectedRow.Cells[0].Text.Trim();
                ddlTipoRiesgo.SelectedValue = GridView13.SelectedDataKey[0].ToString().Trim();
                ddlTipoRiesgo.Focus();
                txtNomRiesgo.Text = GridView13.SelectedRow.Cells[2].Text.Trim();
                ddlRiePoD.SelectedValue = GridView13.SelectedDataKey[1].ToString().Trim();

                if (GridView13.SelectedDataKey[1].ToString() == "Procesos")
                {
                    txtProcesoRie.Text = GridView13.SelectedDataKey[4].ToString().Trim();
                    lblIdProcesoRie.Text = GridView13.SelectedDataKey[3].ToString().Trim();
                    lblIdDependenciaRie.Text = "";
                    lblCorreoDependenciaRie.Text = "";
                    txtDependenciaRie.Text = "";
                    filaProcesoRie.Visible = true;
                    filaDependenciaRie.Visible = false;
                }
                else
                {
                    txtDependenciaRie.Text = GridView13.SelectedDataKey[4].ToString().Trim();
                    lblIdDependenciaRie.Text = GridView13.SelectedDataKey[2].ToString().Trim();
                    lblCorreoDependenciaRie.Text = GridView13.SelectedDataKey[11].ToString().Trim();
                    lblIdProcesoRie.Text = "";
                    txtProcesoRie.Text = "";
                    filaProcesoRie.Visible = false;
                    filaDependenciaRie.Visible = true;
                }

                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;

                txtDescRiesgo.Text = GridView13.SelectedDataKey[6].ToString().Trim();
                ddlProbabilidad.SelectedValue = GridView13.SelectedDataKey[7].ToString().Trim();
                ddlImpacto.SelectedValue = GridView13.SelectedDataKey[8].ToString().Trim();
                txtUsuarioRie.Text = GridView13.SelectedDataKey[10].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRie.Text = GridView13.SelectedRow.Cells[13].Text;
            }
        }

        protected void GridView100_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nameFile = GridView100.SelectedRow.Cells[1].Text.Trim();
            //descargarArchivo(nameFile);
            mtdDescargarPdfGestAudit(nameFile);
        }
        #endregion GridViews

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
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

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 3)
                filaMetodologia.Visible = true;
            else
                filaMetodologia.Visible = false;

            if (TabContainer1.ActiveTabIndex == 4)
                filaConclusion.Visible = true;
            else
                filaConclusion.Visible = false;

            if (TabContainer1.ActiveTabIndex == 5)
                filaObs.Visible = true;
            else
                filaObs.Visible = false;
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (TabContainer1.ActiveTabIndex == 0)
            {
                if (ddlTipoHallazgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Tipo de Hallazgo.", 2, "Atención");
                    ddlTipoHallazgo.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtHallazgo.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Hallazgo.", 2, "Atención");
                    txtHallazgo.Focus();
                }
                else if (ddlEstadoHallazgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Estado del Hallazgo.", 2, "Atención");
                    ddlEstadoHallazgo.Focus();
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                if (ddlRecPoD.SelectedValue == "Procesos" && ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtProcesoRec.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar un Proceso.", 2, "Atención");
                    txtProcesoRec.Focus();
                }
                else if (ddlRecPoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDependenciaRec2.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia.", 2, "Atención");
                    txtDependenciaRec2.Focus();
                }
                else if (ddlRecPoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(lblCorreoDepedenciaRec2.Text.Trim()))
                {
                    err = false;
                    omb.ShowMessage("El nodo seleccionado de la Jerarquía Organizacional no tiene un correo asociado. \r Actualice la informacion en el Módulo de Parametrización o seleccione otro nodo de la Jerarquía Organizacional.", 2, "Atención");
                    txtDependenciaRec2.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDependenciaRec1.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia de Respuesta.", 2, "Atención");
                    txtDependenciaRec1.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtRecomendacion.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Recomendacion.", 2, "Atención");
                    txtRecomendacion.Focus();
                }
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                if (ddlTipoRiesgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Tipo de Riesgo.", 2, "Atención");
                    ddlTipoRiesgo.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtNomRiesgo.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Nombre del Riesgo.", 2, "Atención");
                    txtNomRiesgo.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDescRiesgo.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Descripcion del Riesgo.", 2, "Atención");
                    txtDescRiesgo.Focus();
                }
                else if (ddlRiePoD.SelectedValue == "Procesos" && ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtProcesoRie.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar un Proceso.", 2, "Atención");
                    txtProcesoRie.Focus();
                }
                else if (ddlRiePoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDependenciaRie.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia.", 2, "Atención");
                    txtDependenciaRie.Focus();
                }
                else if (ddlRiePoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(lblCorreoDependenciaRie.Text.Trim()))
                {
                    err = false;
                    omb.ShowMessage("El nodo seleccionado de la Jerarquía Organizacional no tiene un correo asociado. \r Actualice la informacion en el Módulo de Parametrización o seleccione otro nodo de la Jerarquía Organizacional.", 2, "Atención");
                    txtDependenciaRie.Focus();
                }
                else if (ddlProbabilidad.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar la Probabilidad.", 2, "Atención");
                    ddlProbabilidad.Focus();
                }
                else if (ddlImpacto.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Impacto.", 2, "Atención");
                    ddlImpacto.Focus();
                }
            }
            return err;
        }

        protected Boolean ValidarCadenaVacia(string cadena)
        {
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

        #region PDFs

        private void descargarArchivo(string nameFile)
        {
            string filePath = Server.MapPath("~/Archivos/PDFsGestionVerificacion/" + nameFile);

            try
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nameFile + ";");
                Response.TransmitFile(filePath);
                Response.End();
            }
            catch (Exception ex)
            {
                omb.ShowMessage("El archivo no pudo ser descargado del servidor de datos." + "<br/>" + "Se presento el siguiente error:" + ex.Message, 1, "Atencion");
            }
        }

        private void loadFile()
        {
            #region Vars
            bool err = false;
            string IdMaximo;
            string nameFile;

            //Calcula el siguiente codigo a asignar como id de la tabla Auditoria.Archivo
            DataView dvArchivo = (DataView)this.SqlDataSource101.Select(new DataSourceSelectArguments());
            IdMaximo = dvArchivo[0]["Maximo"].ToString().Trim();

            //En la formacion del nombre del archivo el segundo item, es decir -9-, corresponde al codigo de la tabla
            // Parametrizacion.ControlesUsuario , en este caso al control de usuario Gestión de Auditoría
            nameFile = IdMaximo + "-9-" + lblIdHallazgo.Text.Trim() + "-" + FileUpload1.FileName.ToString().Trim();
            #endregion Vars

            try
            {
                try
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFsGestionVerificacion/") + nameFile);
                }
                catch
                {
                    err = true;
                }

                if (!err)
                {
                    #region SQL
                    SqlDataSource100.InsertParameters["IdControlUsuario"].DefaultValue = "9";
                    SqlDataSource100.InsertParameters["IdRegistro"].DefaultValue = lblIdHallazgo.Text.Trim();
                    SqlDataSource100.InsertParameters["UrlArchivo"].DefaultValue = nameFile;
                    SqlDataSource100.InsertParameters["Descripcion"].DefaultValue = txtDescArchivo.Text;
                    SqlDataSource100.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource100.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                    SqlDataSource100.Insert();
                    #endregion SQL
                }
            }
            catch (Exception ex)
            {
                #region Catch
                omb.ShowMessage("El archivo no pudo ser cargado." + "<br/>" + "Se presento el siguiente error:" + ex.Message, 1, "Atencion");
                err = true;
                #endregion Catch
            }

            if (!err)
            {
                #region
                omb.ShowMessage("El archivo se cargo con exito en el servidor de datos." + "<br/>" + "Tamaño del Archivo: " + FileUpload1.FileBytes.Length / 1024 + " Kb" + "<br/> Nombre del Archivo: " + nameFile, 3, "Atención");
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;
                #endregion
            }
        }

        private void mtdCargarPdfGestAudit()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty, strIdControl = "9";
            #endregion Vars

            dtInfo = cAu.loadCodigoArchivo();

            #region Nombre Archivo

            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(), strIdControl,
                    lblIdHallazgo.Text.Trim(), FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}-{2}",
                    strIdControl, lblIdHallazgo.Text.Trim(), FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cAu.mtdAgregarArchivoPdf(lblIdHallazgo.Text.Trim(), strIdControl, Sanitizer.GetSafeHtmlFragment(txtDescArchivo.Text.Trim()),
                strNombreArchivo, bPdfData);

            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        private void mtdDescargarPdfGestAudit(string strNombreArchivo)
        {
            #region Vars
            //string strNombreArchivo = InfoGridControles.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cAu.mtdDescargarArchivoPdf(strNombreArchivo);
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

        #region Notificacion
        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Vars
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion

            #region Informacion basica de correo
            try
            {
                #region Informacion basica
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre FROM [Notificaciones].[CorreosDestinatarios] AS CD, [Notificaciones].[Evento] AS E WHERE E. IdEvento = " + idEvento + " AND CD.IdEvento = E.IdEvento";
                SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
                DataTable dtblDiscuss = new DataTable();
                dad.Fill(dtblDiscuss);

                DataView view = new DataView(dtblDiscuss);

                foreach (DataRowView row in view)
                {
                    #region
                    Copia = row["Copia"].ToString().Trim();
                    Otros = row["Otros"].ToString().Trim();
                    Asunto = row["Asunto"].ToString().Trim();
                    Cuerpo = textoAdicional + row["Cuerpo"].ToString().Trim();
                    NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                    AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                    AJefeMediato = row["AJefeMediato"].ToString().Trim();
                    RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    #endregion
                }
                #endregion

                #region Destinatario
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
                #endregion

                #region Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    #region
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
                    #endregion
                }
                #endregion

                #region Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    #region
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO, [Parametrizacion].[DetalleJerarquiaOrg] AS DJ WHERE JO.idHijo = " + idJefeMediato + " AND DJ.idHijo = JO.idHijo";
                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                    }
                    #endregion
                }
                #endregion

                #region Registro en la tabla de Correos Enviados
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
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource200.Insert();
                #endregion
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }
            #endregion

            #region LOG y envio de correo
            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    #region inserta el registro tabla CorreosRecordatorio
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
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");

                    message.From = fromAddress;//here you can set address
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }

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

                #region Actualizar Correo Enviado
                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                }
                #endregion
            }
            #endregion

            return (err);
        }

        private bool boolEnviarNotificacion(int intIdEvento, int idRegistro, int idNodoJerarquia,
            string strAsunto, string strPlaneacion, string strAuditoria, string strHallazgo)
        {
            #region Vars
            bool booResult = true;
            string strDestinatario = string.Empty, strCopia = string.Empty, strOtros = string.Empty, strCuerpo = string.Empty;
            string strConsulta = string.Empty, strJefeInmediato = string.Empty, strJefeMediato = string.Empty,
                strIdJefeInmediato = string.Empty, strIdJefeMediato = string.Empty, strNroDiasRecordatorio = string.Empty,
                strRequiereFechaCierre = string.Empty;
            string strConnString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion

            #region Informacion basica y envio de correo
            try
            {
                #region Informacion basica
                #region Consulta
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                strConsulta = string.Format("SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                    "FROM [Notificaciones].[CorreosDestinatarios] CD INNER JOIN [Notificaciones].[Evento] E ON CD.IdEvento = E.IdEvento WHERE E.IdEvento = {0}", intIdEvento);

                SqlDataAdapter sdaAdapter = new SqlDataAdapter(strConsulta, strConnString);
                DataTable dtInfo = new DataTable();
                sdaAdapter.Fill(dtInfo);

                DataView dvInfo = new DataView(dtInfo);
                #endregion

                #region Recorrido de informacion
                foreach (DataRowView dr in dvInfo)
                {
                    #region Llenado de info
                    strCopia = dr["Copia"].ToString().Trim();
                    strOtros = dr["Otros"].ToString().Trim();
                    strCuerpo = strPlaneacion + "<br/> " + strAuditoria + "<br/> <br/> " + strHallazgo;
                    strJefeInmediato = dr["AJefeInmediato"].ToString().Trim();
                    strJefeMediato = dr["AJefeMediato"].ToString().Trim();
                    strNroDiasRecordatorio = dr["NroDiasRecordatorio"].ToString().Trim();
                    strRequiereFechaCierre = dr["RequiereFechaCierre"].ToString().Trim();
                    #endregion
                }
                #endregion
                #endregion

                #region Info Destinatario
                #region Consulta
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                strConsulta = string.Format("SELECT DJ.CorreoResponsable, JO.idPadre " +
                    "FROM [Parametrizacion].[JerarquiaOrganizacional] JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] DJ ON DJ.idHijo = JO.idHijo " +
                    "WHERE JO.idHijo = {0}", idNodoJerarquia);

                sdaAdapter = new SqlDataAdapter(strConsulta, strConnString);
                dtInfo.Clear();
                sdaAdapter.Fill(dtInfo);
                dvInfo = new DataView(dtInfo);
                #endregion

                #region Recorrido de informacion
                foreach (DataRowView dr in dvInfo)
                {
                    strDestinatario = dr["CorreoResponsable"].ToString().Trim();
                    strIdJefeInmediato = dr["idPadre"].ToString().Trim();
                }
                #endregion
                #endregion

                #region Info Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (strIdJefeInmediato == "SI")
                {
                    #region Consulta
                    strConsulta = string.Format("SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = {0}", strIdJefeInmediato);

                    sdaAdapter = new SqlDataAdapter(strConsulta, strConnString);
                    dtInfo.Clear();
                    sdaAdapter.Fill(dtInfo);
                    dvInfo = new DataView(dtInfo);
                    #endregion

                    #region Recorrido de informacion
                    foreach (DataRowView row in dvInfo)
                    {
                        strDestinatario = strDestinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                        strIdJefeMediato = row["idPadre"].ToString().Trim();
                    }
                    #endregion
                }
                #endregion

                #region Info Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (strJefeMediato == "SI")
                {
                    #region Consulta
                    strConsulta = string.Format("SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] DJ ON DJ.idHijo = JO.idHijo" +
                        "WHERE JO.idHijo = ", strIdJefeMediato);

                    sdaAdapter = new SqlDataAdapter(strConsulta, strConnString);
                    dtInfo.Clear();
                    sdaAdapter.Fill(dtInfo);
                    dvInfo = new DataView(dtInfo);
                    #endregion

                    #region Recorrido de informacion
                    foreach (DataRowView row in dvInfo)
                    {
                        strDestinatario = strDestinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                    }
                    #endregion
                }
                #endregion

                #region Registro en la tabla de Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = strDestinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = strCopia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = strOtros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = strAsunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = strCuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = intIdEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource200.Insert();
                #endregion

                #region Inserta el registro tabla CorreosRecordatorio
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (strRequiereFechaCierre == "SI")
                {
                    #region Inserta el registro
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = strNroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = string.Empty;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource201.Insert();
                    #endregion
                }
                #endregion

                #region Envio Correo
                #region Vars Email
                MailMessage mmMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");

                mmMessage.IsBodyHtml = true;//To determine email body is html or not
                #endregion

                #region From
                mmMessage.From = fromAddress;//here you can set address
                foreach (string substr in strDestinatario.Split(';'))
                {
                    if (!string.IsNullOrEmpty(substr.Trim()))
                        mmMessage.To.Add(substr);
                }
                #endregion

                #region Copy
                if (!string.IsNullOrEmpty(strCopia.Trim()))
                    foreach (string substr in strCopia.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            mmMessage.CC.Add(substr);
                    }

                if (!string.IsNullOrEmpty(strOtros.Trim()))
                    foreach (string substr in strOtros.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            mmMessage.CC.Add(substr);
                    }
                #endregion

                #region Subject and Body
                mmMessage.Subject = strAsunto;//subject of email
                mmMessage.Body = strCuerpo;
                #endregion

                smtpClient.Send(mmMessage);
                #endregion
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                booResult = false;
            }
            #endregion

            #region Actualizar Correo Enviado
            if (booResult)
            {
                //Actualiza el Estado del Correo Enviado
                SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.Update();
            }
            #endregion

            return booResult;
        }

        private string mtdConsultaHallazgos(string strIdAuditoria)
        {
            #region Vars
            string strConnString = string.Empty, strConsulta = string.Empty, strHallazgos = string.Empty;
            #endregion

            #region Consulta
            strConnString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            strConsulta = string.Format("SELECT AH.IdHallazgo, AH.[IdAuditoria], AH.[Hallazgo], AH.[IdNivelRiesgo], PDT.NombreDetalle " +
                "FROM [Auditoria].[Hallazgo] AH INNER JOIN [Parametrizacion].[DetalleTipos] PDT ON PDT.IdDetalleTipo = AH.IdNivelRiesgo " +
                "WHERE AH.[IdAuditoria] = {0}", strIdAuditoria);

            SqlDataAdapter sdaAdapter = new SqlDataAdapter(strConsulta, strConnString);
            DataTable dtInfo = new DataTable();
            sdaAdapter.Fill(dtInfo);

            DataView dvInfo = new DataView(dtInfo);
            #endregion

            #region Recorrido de informacion
            foreach (DataRowView dr in dvInfo)
            {
                #region Llenado de info
                strHallazgos = strHallazgos + string.Format("Código Hallazgo: {0} <br /> Descripción: {1} <br /> Nivel de riesgo: {2} <br /><br /> ",
                    dr["IdHallazgo"].ToString().Trim(), dr["Hallazgo"].ToString().Trim(), dr["NombreDetalle"].ToString());
                #endregion
            }
            #endregion

            return strHallazgos;
        }
        #endregion

        protected void bntAdjuntos_Click(object sender, EventArgs e)
        {
            VerAdjuntos();
        }

        private void VerAdjuntos()
        {
            filaGridAud.Visible = true;
            filaEncabezado.Visible = false;
            filaTabGestion.Visible = false;
            TabContainer1.ActiveTabIndex = 0;
            filaAcciones.Visible = false;
            filaSubirAud.Visible = false;
            txtDescArchivoAud.Text = string.Empty;
            string CodRegAuditoria = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
            loadGridAdjuntos();
            loadInfoAdjuntos(CodRegAuditoria);
        }

        protected void BtnAdjuntar_Click(object sender, EventArgs e)
        {
            if (fuCargueArchivos.HasFile)
            {
                if (Path.GetExtension(fuCargueArchivos.FileName).ToLower().ToString().Trim() == ".pdf")
                {
                    mtdCargarPdfVerificacion();
                    string CodRegAuditoria = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
                    loadGridAdjuntos();
                    loadInfoAdjuntos(CodRegAuditoria);
                    omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                }
                else
                    omb.ShowMessage("Solamente se permiten cargar archivos PDF!", 2, "Atención");
            }
            else
            {//omb.ShowMessage("¡Debe seleccionar un archivo PDF!", 2, "Atención");
                btnImgokEliminar.Visible = false;
                lblMsgBox.Text = "No hay archivos para cargar.";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgCancelarAdjunto_Click(object sender, ImageClickEventArgs e)
        {
            filaGridAud.Visible = false;
            filaSubirAud.Visible = false;
            filaAcciones.Visible = true;
        }
        protected void imgBtnInsertarArchivoAud_Click(object sender, ImageClickEventArgs e)
        {
            filaSubirAud.Visible = true;
        }

        protected void btnImgCancelarArchivoAud_Click(object sender, ImageClickEventArgs e)
        {
            filaSubirAud.Visible = false;
        }
        private void loadGridAdjuntos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridAdjuntos = grid;
            GridView3.DataSource = InfoGridAdjuntos;
            GridView3.DataBind();

        }

        private void loadInfoAdjuntos(string CodRegAuditoria)
        {
            DataTable dtInfo = new DataTable();
            cAuditoria cAuditoria = new cAuditoria();
            dtInfo = cAuditoria.ArchivoAud(CodRegAuditoria);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAdjuntos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                        });
                }
                GridView3.DataSource = InfoGridAdjuntos;
                GridView3.DataBind();
            }
        }
        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoAud = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    string strNombreArchivo = InfoGridAdjuntos.Rows[RowGridArchivoAud]["UrlArchivo"].ToString().Trim();
                    mtdDescargarPdfAud(strNombreArchivo);
                    break;
            }
        }
        protected void mtdDescargarPdfAud(string strNombreArchivo)
        {
            #region Vars
            cAuditoria cAuditoria = new cAuditoria();
            byte[] bPdfData = cAuditoria.mtdDescargarArchivoAud(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                //Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }
        private void mtdCargarPdfVerificacion()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            cAuditoria cAuditoria = new cAuditoria();
            string strNombreArchivo = string.Empty, strIdControl = "10";
            #endregion Vars

            dtInfo = cAuditoria.loadCodigoArchivo();

            #region Nombre Archivo

            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    strIdControl, txtCodAuditoriaSel.Text.Trim(), fuCargueArchivos.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}-{2}",
                    strIdControl, txtCodAuditoriaSel.Text.Trim(), fuCargueArchivos.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = fuCargueArchivos.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            /*cAuditoria.mtdAgregarArchivoPdf(txtCodAuditoriaSel.Text.Trim(), strIdControl, txtDescArchivoAud.Text.Trim(),
                strNombreArchivo, bPdfData);*/
            cAuditoria.mtdAgregarArchivoAud(txtCodAuditoriaSel.Text.Trim(), txtDescArchivoAud.Text.Trim(),
                strNombreArchivo, bPdfData);
            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        private DataTable infoGridAdjuntos;
        private DataTable InfoGridAdjuntos
        {
            get
            {
                infoGridAdjuntos = (DataTable)ViewState["infoGridAdjuntos"];
                return infoGridAdjuntos;
            }
            set
            {
                infoGridAdjuntos = value;
                ViewState["infoGridAdjuntos"] = infoGridAdjuntos;
            }
        }

        private int rowGridArchivoAud;
        private int RowGridArchivoAud
        {
            get
            {
                rowGridArchivoAud = (int)ViewState["rowGridArchivoAud"];
                return rowGridArchivoAud;
            }
            set
            {
                rowGridArchivoAud = value;
                ViewState["rowGridArchivoAud"] = rowGridArchivoAud;
            }
        }
        
    }
}