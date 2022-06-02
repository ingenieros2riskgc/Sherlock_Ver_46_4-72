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
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Parametrizacion
{
    public partial class JerarquiaOrganizacional : System.Web.UI.UserControl
    {
        // variable global
        string IdFormulario = "2004";
        cCuenta cCuenta = new cCuenta();

        //private string id;
        private static int LastInsertId;
        private static string DetJerarquiaId;

        /// <summary>
        /// Only populate the TreeView when the page first loads
        /// </summary>
        void Page_Load()
        {
            if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
            else
            {
                if (cCuenta.permisosConsulta(IdFormulario) == "False")
                    Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
                else
                {
                    if (!Page.IsPostBack)
                    {
                        // Carga los nodos en el arbol
                        PopulateTreeView();
                        ddlArea.DataBind();
                        // Verifica si existen nodos en el arbol
                        TreeNodeCollection nodes = TreeView1.Nodes;
                        if (nodes.Count == 0)
                        {
                            // Si existen nodos habilita el boton para insertar nodo
                            filaAccion.Visible = true;
                            btnImgInsertar.Visible = true;
                            btnImgEditar.Visible = false;
                            btnImgEliminar.Visible = false;
                        }
                    }
                }
            }
        }

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
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
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
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            filaAccion.Visible = true;
            lblIdHijo.Text = TreeView1.SelectedNode.Value;

            TreeNode padre = TreeView1.SelectedNode.Parent;
            if (padre == null)
            {
                lblNodoPadre.Text = "---";
                lblIdPadre.Text = "-1";
            }
            else
            {
                lblNodoPadre.Text = Sanitizer.GetSafeHtmlFragment(padre.Text);
                lblIdPadre.Text = padre.Value;
            }

            btnImgEditar.Visible = true;
            btnImgInsertar.Visible = true;

            TreeNodeCollection nodosHijos = TreeView1.SelectedNode.ChildNodes;
            if (nodosHijos.Count > 0)
                btnImgEliminar.Visible = false;
            else
                btnImgEliminar.Visible = true;

            ddlTipoArea.SelectedValue = ConsultarTipoArea(1, "");
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

        private string ConsultarTipoArea(int TipoQuery, string TipoArea)
        {
            string selectCommand = "", columna = "";

            if (TipoQuery == 1)
            {
                selectCommand = "SELECT TipoArea FROM [Parametrizacion].[JerarquiaOrganizacional] where IdHijo = " + lblIdHijo.Text;
                columna = "TipoArea";
            }
            else if (TipoQuery == 2)
            {
                selectCommand = "SELECT NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional] where TipoArea = '" + TipoArea + "'";
                columna = "NombreHijo";
            }
            else if (TipoQuery == 3)
            {
                selectCommand = "SELECT NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional] where TipoArea = '" + TipoArea + "' AND IdHijo <> " + lblIdHijo.Text;
                columna = "NombreHijo";
            }

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter consulta = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblArea = new DataTable();
            consulta.Fill(dtblArea);

            if (dtblArea.Rows.Count > 0)
            {
                DataRow row = dtblArea.Rows[0];
                return row[columna].ToString().Trim();
            }
            else
            {
                return "";
            }
        }

        protected void btnImgEditar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 3, "Atención");
                return;
            }
            pnlDetalle.Visible = true;
            pnlJerarquia.Visible = false;
            btnImgGrabar.Visible = false;
            btnImgActualizar.Visible = true;
            txtNombreHijo.Text = TreeView1.SelectedNode.Text;

            //Consulta el detalle del nodo a actualizar y lo carga en sus respectivas cajas de texto
            SqlDataSource2.SelectParameters["idHijo"].DefaultValue = lblIdHijo.Text.ToString().Trim();
            DataView dvDetJerarquia = (DataView)this.SqlDataSource2.Select(new DataSourceSelectArguments());
            /*DataTable ddlAreaDT = (DataTable)this.SqlDataSource3.Select(new DataSourceSelectArguments());
            ddlArea.DataSource = ddlAreaDT;*/
            ddlArea.DataBind();
            //Verifica que ya exista un detalle para el nodo en la BD
            if (dvDetJerarquia.Count > 0)
            {
                DetJerarquiaId = dvDetJerarquia[0]["idDetalleJerarquiaOrg"].ToString();
                this.txtNombreResponsable.Text = dvDetJerarquia[0]["NombreResponsable"].ToString();
                this.txtCorreoResponsable.Text = dvDetJerarquia[0]["CorreoResponsable"].ToString();
                this.txtCargoResponsable.Text = dvDetJerarquia[0]["CargoResponsable"].ToString();

                //if (!string.IsNullOrEmpty(dvDetJerarquia[0]["IdArea"].ToString()))
                //    ddlArea.SelectedValue = dvDetJerarquia[0]["IdArea"].ToString();
                try
                {
                    if (!string.IsNullOrEmpty(dvDetJerarquia[0]["IdArea"].ToString()))
                        ddlArea.SelectedValue = dvDetJerarquia[0]["IdArea"].ToString().Trim();
                }
                catch
                {
                    ddlArea.SelectedIndex = 0;
                    //lblMsgBox.Text = "Error: No existe información de Código Área";
                    //mpeMsgBox.Show();
                }
            }
            else
            {
                DetJerarquiaId = "";
                this.txtNombreResponsable.Text = "";
                this.txtCorreoResponsable.Text = "";
                this.txtCargoResponsable.Text = "";
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            pnlDetalle.Visible = false;
            pnlJerarquia.Visible = true;
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 3, "Atención");
            }
            else
            {
                // Verifica si existen nodos en el arbol
                TreeNodeCollection nodes = TreeView1.Nodes;
                if (nodes.Count == 0)
                {
                    // Si existen nodos habilita el boton para insertar nodo
                    lblNodoPadre.Text = "---";
                    lblIdPadre.Text = "-1";
                    lblIdHijo.Text = "-1";
                }
                else
                    lblNodoPadre.Text = TreeView1.SelectedNode.Text;

                pnlDetalle.Visible = true;
                pnlJerarquia.Visible = false;
                txtNombreHijo.Text = "";
                txtNombreHijo.Focus();
                this.txtNombreResponsable.Text = "";
                this.txtCorreoResponsable.Text = "";
                this.txtCargoResponsable.Text = "";
                ddlTipoArea.SelectedValue = "";
                btnImgGrabar.Visible = true;
                btnImgActualizar.Visible = false;
                ddlArea.SelectedIndex = -1;
            }
        }

        protected void btnImgGrabar_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false, correoValido = false;
            string NombreArea = "";

            if (txtCorreoResponsable.Text != "")
            {
                correoValido = RegexUtilities.IsValidEmail(Sanitizer.GetSafeHtmlFragment(txtCorreoResponsable.Text));
                if (!correoValido)
                {
                    omb.ShowMessage("La dirección de correo suministrada es invalida.", 1, "Atención");
                    txtCorreoResponsable.Focus();
                }
            }

            if (correoValido)
            {
                //if (ddlTipoArea.SelectedValue != "")
                //    NombreArea = ConsultarTipoArea(2, ddlTipoArea.SelectedValue);

                if (NombreArea == "")
                {
                    pnlDetalle.Visible = false;
                    pnlJerarquia.Visible = true;

                    //Inserta el maestro del nodo hijo
                    try
                    {
                        SqlDataSource1.InsertParameters["idPadre"].DefaultValue = lblIdHijo.Text;
                        SqlDataSource1.InsertParameters["TipoArea"].DefaultValue = ddlTipoArea.SelectedValue;
                        SqlDataSource1.InsertParameters["NombreHijo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreHijo.Text.ToString().Trim());
                        SqlDataSource1.Insert();
                    }
                    catch (Exception except)
                    {
                        // Handle the Exception.
                        omb.ShowMessage("Error en la inserción del nodo hijo." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }

                    if (!err)
                    {
                        //Inserta el detalle del nodo hijo
                        try
                        {
                            //   
                            if (Sanitizer.GetSafeHtmlFragment(txtCorreoResponsable.Text.Trim()) != "")
                            {
                                //if (Regex.Match(txtCorreoResponsable.Text, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$").Success == true)
                                if (RegexUtilities.IsValidEmail(Sanitizer.GetSafeHtmlFragment(txtCorreoResponsable.Text.Trim())))
                                {
                                    if (Sanitizer.GetSafeHtmlFragment(txtNombreResponsable.Text.Trim()) != "" || Sanitizer.GetSafeHtmlFragment(txtCorreoResponsable.Text.Trim()) != "" || Sanitizer.GetSafeHtmlFragment(txtCargoResponsable.Text.Trim()) != "")
                                    {
                                        SqlDataSource2.InsertParameters["idHijo"].DefaultValue = LastInsertId.ToString().Trim();
                                        SqlDataSource2.InsertParameters["NombreResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreResponsable.Text.ToString().Trim());
                                        SqlDataSource2.InsertParameters["CorreoResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCorreoResponsable.Text.ToString().Trim());
                                        SqlDataSource2.InsertParameters["CargoResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCargoResponsable.Text.ToString().Trim());
                                        SqlDataSource2.InsertParameters["IdArea"].DefaultValue = ddlArea.SelectedValue;
                                        SqlDataSource2.Insert();
                                    }
                                }
                                else 
                                    omb.ShowMessage("La dirección de correo suministrada es invalida.", 1, "Atención");
                            }
                        }
                        catch (Exception except)
                        {
                            // Handle the Exception.
                            omb.ShowMessage("Error en la inserción del detalle del nodo hijo." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                            err = true;
                        }

                        if (!err)
                            omb.ShowMessage("El nodo se grabo con éxito en la Base de Datos.", 3, "Atención");

                        TreeView1.Nodes.Clear();
                        PopulateTreeView();
                        TreeView1.ExpandAll();
                        lblIdHijo.Text = "";
                        lblIdPadre.Text = "";
                        filaAccion.Visible = false;
                    }
                }
                else
                {
                    omb.ShowMessage("No se pudo grabar la información!" + "<br/>" + "El nodo '" + NombreArea + "' ya fue asignado como Area de " + ddlTipoArea.SelectedItem.Text, 2, "Atención");
                    txtNombreHijo.Focus();
                }
            }
        }

        protected void btnImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 3, "Atención");
            }
            else
            {
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 3, "Atención");
                return;
            }
            else
            {
                bool err = false, correoValido = true;
                string NombreArea = "";

                if (txtCorreoResponsable.Text.Trim() != "")
                {
                    correoValido = RegexUtilities.IsValidEmail(txtCorreoResponsable.Text.Trim());
                    if (!correoValido)
                    {
                        omb.ShowMessage("La dirección de correo suministrada es invalida.", 1, "Atención");
                        txtCorreoResponsable.Focus();
                    }
                }

                if (correoValido)
                {
                    //if (ddlTipoArea.SelectedValue != "")
                    //    NombreArea = ConsultarTipoArea(3, ddlTipoArea.SelectedValue);

                    if (NombreArea == "")
                    {
                        pnlDetalle.Visible = false;
                        pnlJerarquia.Visible = true;

                        try
                        {
                            SqlDataSource1.UpdateParameters["idPadre"].DefaultValue = lblIdPadre.Text;
                            SqlDataSource1.UpdateParameters["idHijo"].DefaultValue = lblIdHijo.Text;
                            SqlDataSource1.UpdateParameters["TipoArea"].DefaultValue = ddlTipoArea.SelectedValue;
                            SqlDataSource1.UpdateParameters["NombreHijo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreHijo.Text.ToString().Trim());
                            SqlDataSource1.Update();
                        }
                        catch (Exception except)
                        {
                            omb.ShowMessage("Error en la actualización del nodo." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                            err = true;
                        }

                        if (!err)
                        {
                            try
                            {
                                //Si existe ya un detalle para el nodo lo actualiza, de lo contrario procede a crear uno
                                if (DetJerarquiaId != "")
                                {
                                    SqlDataSource2.UpdateParameters["idDetalleJerarquiaOrg"].DefaultValue = DetJerarquiaId.ToString().Trim();
                                    SqlDataSource2.UpdateParameters["NombreResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreResponsable.Text.Trim());
                                    SqlDataSource2.UpdateParameters["CargoResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCargoResponsable.Text.Trim());
                                    SqlDataSource2.UpdateParameters["CorreoResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCorreoResponsable.Text.Trim());
                                    SqlDataSource2.UpdateParameters["IdArea"].DefaultValue = ddlArea.SelectedValue;
                                    SqlDataSource2.Update();
                                }
                                else
                                {
                                    if (Sanitizer.GetSafeHtmlFragment(txtNombreResponsable.Text) != "" || Sanitizer.GetSafeHtmlFragment(txtCorreoResponsable.Text) != "" || Sanitizer.GetSafeHtmlFragment(txtCargoResponsable.Text) != "")
                                    {
                                        SqlDataSource2.InsertParameters["idHijo"].DefaultValue = lblIdHijo.Text;
                                        SqlDataSource2.InsertParameters["NombreResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreResponsable.Text.Trim());
                                        SqlDataSource2.InsertParameters["CorreoResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCorreoResponsable.Text.Trim());
                                        SqlDataSource2.InsertParameters["CargoResponsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCargoResponsable.Text.Trim());
                                        SqlDataSource2.InsertParameters["IdArea"].DefaultValue = ddlArea.SelectedValue;
                                        SqlDataSource2.Insert();
                                    }
                                }
                            }
                            catch (Exception except)
                            {
                                omb.ShowMessage("Error en la actualización del detalle del nodo." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                                err = true;
                            }

                            if (!err)
                                omb.ShowMessage("El nodo se actualizó con éxito en la Base de Datos.", 3, "Atención");

                            TreeView1.Nodes.Clear();
                            PopulateTreeView();
                            TreeView1.ExpandAll();
                            lblIdHijo.Text = "";
                            lblIdPadre.Text = "";
                            filaAccion.Visible = false;
                        }
                    }
                    else
                    {
                        omb.ShowMessage("No se pudo actualizar la información!" + "<br/>" + "El nodo '" + NombreArea + "' ya fue asignado como Area de " + ddlTipoArea.SelectedItem.Text, 1, "Atención");
                        txtNombreHijo.Focus();
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool err = false;

            mpeMsgBox.Hide();

            //Consulta el detalle del nodo a eliminar 
            SqlDataSource2.SelectParameters["idHijo"].DefaultValue = lblIdHijo.Text;
            DataView dvDetJerarquia = (DataView)this.SqlDataSource2.Select(new DataSourceSelectArguments());
            //Verifica que ya exista un detalle para el nodo en la BD
            if (dvDetJerarquia.Count > 0)
                DetJerarquiaId = dvDetJerarquia[0]["idDetalleJerarquiaOrg"].ToString().Trim();
            else
                DetJerarquiaId = "";

            if (DetJerarquiaId != "")
            {
                try
                {
                    SqlDataSource1.DeleteParameters["idPadre"].DefaultValue = lblIdPadre.Text;
                    SqlDataSource1.DeleteParameters["idHijo"].DefaultValue = lblIdHijo.Text;
                    SqlDataSource1.Delete();
                }
                catch (SqlException except)
                {
                    if (except.Number.Equals(547))
                        omb.ShowMessage("Error en la eliminación del nodo en la Base de Datos." + "<br/>" + "El nodo que intenta eliminar esta siendo referenciado en otros módulos de la aplicación.", 1, "Atención");
                    else
                        omb.ShowMessage("Error en la eliminación del nodo en la Base de Datos." + "<br/>" + "Descripción: " + except.Message.ToString() + except.Number, 1, "Atención");
                    err = true;
                }

            }

            if (!err)
            {
                try
                {
                    SqlDataSource2.DeleteParameters["idDetalleJerarquiaOrg"].DefaultValue = DetJerarquiaId;
                    SqlDataSource2.Delete();
                }
                catch (SqlException except)
                {
                    omb.ShowMessage("Error en la eliminación del detalle del nodo en la Base de Datos." + "<br/>" + "Descripción: " + except.Message.ToString() + except.Number, 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    TreeView1.Nodes.Clear();
                    PopulateTreeView();
                    TreeView1.ExpandAll();
                    lblIdHijo.Text = "";
                    lblIdPadre.Text = "";

                    // Verifica si existen nodos en el arbol
                    TreeNodeCollection nodes = TreeView1.Nodes;
                    if (nodes.Count == 0)
                    {
                        filaAccion.Visible = true;
                        btnImgInsertar.Visible = true;
                        btnImgEditar.Visible = false;
                        btnImgEliminar.Visible = false;
                    }
                    else
                        filaAccion.Visible = false;

                    omb.ShowMessage("El nodo se eliminó con éxito en la Base de Datos.", 3, "Atención");
                }
            }
        }

        protected void SqlDataSource1_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertId = (int)e.Command.Parameters["@NewParameter"].Value;
        }

        protected void ddlTipoArea_DataBound(object sender, EventArgs e)
        {
            ddlTipoArea.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        public class RegexUtilities
        {
            public static bool IsValidEmail(string strIn)
            {
                // Return true if strIn is in valid e-mail format.
                return Regex.IsMatch(strIn,
                    //@"^[-_a-z0-9\'+*$^&%=~!?{}]+(?:\.[-_a-z0-9\'+*$^&%=~!?{}]+)*@(?:(?![-.])[-a-z0-9.]+(?<![-.])\.[az]{2,6}|\d{1,3}(?:\.\d{1,3}){3})(?::\d+)?$ " );
                       @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            }
        }
    }

}