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

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class GruposAuditoria : System.Web.UI.UserControl
    {
        string IdFormulario = "3001";
        private static int LastInsertId;
        //private static int LastInsertId2;
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                    PopulateTreeView();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                // Carga los datos en la respectiva caja de texto
                txtId.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                lblIdGrupo.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtNombre.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                if (filaDetalle.Visible == true)
                {
                    txtUsuario.Text = GridView1.SelectedDataKey[0].ToString().Trim();
                    txtUsuario.Text = GridView1.SelectedDataKey[1].ToString().Trim();
                    txtFecha.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
                }
                else if (filaArbol.Visible == true)
                {
                    txtCodGrupoA.Text = lblIdGrupo.Text;
                    txtNomGrupoA.Text = Sanitizer.GetSafeHtmlFragment(txtNombre.Text.Trim());
                    PopulateTreeView2();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nroPag, tamPag;

            try
            {
                if (e.CommandArgument.ToString() == "Editar")
                {
                    txtId.Enabled = false;
                    btnImgInsertar.Visible = false;
                    btnImgActualizar.Visible = true;
                    filaGrid.Visible = false;
                    filaDetalle.Visible = true;
                }

                if (e.CommandArgument.ToString() == "DetalleArbol")
                {
                    filaGrid.Visible = false;
                    filaArbol.Visible = true;
                    TreeView2.Nodes.Clear();
                }

                if (e.CommandName == "Eliminar")
                {
                    // Convierte el indice de la fila del GridView almacenado en la propiedad CommandArgument a un tipo entero
                    int index = Convert.ToInt32((e.CommandArgument).ToString());
                    nroPag = GridView1.PageIndex;  // Obtiene el Numero de Pagina en la que se encuentra el GridView
                    tamPag = GridView1.PageSize; // Obtiene el Tamano de cada Pagina del GridView

                    index = (index - tamPag * nroPag); // Calcula el Numero de Fila del GridView dentro de la pagina actual

                    // Recupera la fila que contiene el boton al que se le hizo click por el usuario de la coleccion Rows
                    GridViewRow row = GridView1.Rows[index];

                    // Obtiene el Id del registro a Eliminar
                    txtId.Text = row.Cells[0].Text.Trim();
                }
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error" + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
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
                if (filaGrid.Visible == true)
                {
                    SqlDataSource1.DeleteParameters["IdGrupoAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                    SqlDataSource1.Delete();
                }
                else if (filaArbol.Visible == true)
                {
                    if (TreeView2.SelectedNode.ChildNodes.Count == 0)
                    {
                        SqlDataSource2.DeleteParameters["IdGrupoAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                        SqlDataSource2.DeleteParameters["idHijo"].DefaultValue = TreeView2.SelectedNode.Value; ;
                        SqlDataSource2.Delete();
                    }
                    else
                    {
                        omb.ShowMessage("El nodo a eliminar tiene hijos." + "<br/>" + " Por favor elimine primero los nodos de nivel inferior.", 1, "Atención");
                        return;
                    }
                }
            }
            catch (SqlException except)
            {
                if (except.Number.Equals(547))
                    omb.ShowMessage("Error en la eliminación del nodo en la Base de Datos." + "<br/>" + "El nodo que intenta eliminar esta siendo referenciado en otros módulos de la aplicación.", 1, "Atención");
                else
                    omb.ShowMessage("Error en la eliminación del nodo en la Base de Datos." + "<br/>" + "Descripción: " + except.Message.ToString() + except.Number, 1, "Atención");
                err = true;
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                TreeView2.Nodes.Clear();
                PopulateTreeView2();
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
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
                txtId.Text = "";
                txtId.Enabled = false;
                txtFecha.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                txtNombre.Focus();
                txtNombre.Text = "";
                txtUsuario.Text = Session["loginUsuario"].ToString().Trim(); // Aca va el Codigo de Usuario logueado en la aplicacion
                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaDetalle.Visible = true;
                filaGrid.Visible = false;
            }
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
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource1.UpdateParameters["IdGrupoAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                        SqlDataSource1.UpdateParameters["Nombre"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombre.Text.Trim());
                        SqlDataSource1.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource1.InsertParameters["Nombre"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombre.Text.Trim());
                    SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource1.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalle.Visible = false;
                    filaArbol.Visible = true;
                    // Carga los nodos en el arbol
                    txtCodGrupoA.Text = lblIdGrupo.Text;
                    txtNomGrupoA.Text = Sanitizer.GetSafeHtmlFragment(txtNombre.Text.Trim());
                    TreeView2.Nodes.Clear();
                    //PopulateTreeView();
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void imgBtnAddHijoAud_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                string idPadre = "";

                if (TreeView1.SelectedNode == null)
                {
                    omb.ShowMessage("Por favor seleccione el nodo a adicionar en el Grupo de Auditoría.", 2, "Atención");
                    return;
                }

                if (TreeView2.Nodes.Count == 0)
                    idPadre = "0";
                else
                {
                    if (TreeView2.SelectedNode == null)
                    {
                        omb.ShowMessage("Por favor seleccione el nodo padre en el Grupo de Auditoría.", 2, "Atención");
                        return;
                    }
                    else idPadre = TreeView2.SelectedNode.Value;
                }

                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource2.InsertParameters["idGrupoAuditoria"].DefaultValue = lblIdGrupo.Text;
                    SqlDataSource2.InsertParameters["idHijo"].DefaultValue = TreeView1.SelectedNode.Value;
                    SqlDataSource2.InsertParameters["idPadre"].DefaultValue = idPadre;
                    SqlDataSource2.InsertParameters["NombreHijoAuditoria"].DefaultValue = TreeView1.SelectedNode.Text;
                    SqlDataSource2.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource2.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource2.Insert();

                    omb.ShowMessage("El nodo se insertó con éxito en la Base de Datos.", 3, "Atención");
                    // Carga los nodos en el arbol

                    TreeNode newNode = new TreeNode(TreeView1.SelectedNode.Text, TreeView1.SelectedNode.Value);

                    if (TreeView2.Nodes.Count > 0)
                    {
                        newNode.Expanded = true;
                        newNode.ToolTip = DetalleNodo(2, TreeView1.SelectedNode.Value);
                        TreeView2.SelectedNode.ChildNodes.Add(newNode);
                    }
                    else
                    {
                        newNode.Expanded = true;
                        newNode.ToolTip = DetalleNodo(2, TreeView1.SelectedNode.Value);
                        TreeView2.Nodes.Add(newNode);
                    }
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void imgBtnCancelarDetalle_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaArbol.Visible = false;
        }

        protected void imgBtnDelHijoAud_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (TreeView2.SelectedNode == null)
                {
                    omb.ShowMessage("Por favor seleccione un nodo para eliminar.", 2, "Atención");
                    return;
                }
                else
                {
                    lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                    mpeMsgBox.Show();
                }
            }
        }

        protected void imgBtnEditHijoAud_Click(object sender, ImageClickEventArgs e)
        {
            if (TreeView2.SelectedNode == null)
            {
                omb.ShowMessage("Por favor seleccione el nodo a actualizar en el Grupo de Auditoría.", 2, "Atención");
            }
            else
            {
                filaDetalleJGA.Visible = true;
                filaArbol.Visible = false;
                txtCodJGA.Text = TreeView2.SelectedNode.Value;
                txtNomJGA.Text = TreeView2.SelectedNode.Text;
            }
        }

        protected void btnImgCancelarJGA_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleJGA.Visible = false;
            filaArbol.Visible = true;
        }

        protected void btnImgActualizarJGA_Click(object sender, ImageClickEventArgs e)
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
                        SqlDataSource2.UpdateParameters["idGrupoAuditoria"].DefaultValue = lblIdGrupo.Text;
                        SqlDataSource2.UpdateParameters["idHijo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodJGA.Text);
                        SqlDataSource2.UpdateParameters["NombreHijoAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNomJGA.Text);
                        SqlDataSource2.Update();
                        filaArbol.Visible = true;
                        filaDetalleJGA.Visible = false;
                        TreeView2.Nodes.Clear();
                        PopulateTreeView2();
                        omb.ShowMessage("El nodo se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    }
                    catch (Exception except)
                    {
                        // Handle the Exception.
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void SqlDataSource1_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertId = (int)e.Command.Parameters["@NewParameter"].Value;
            lblIdGrupo.Text = LastInsertId.ToString().Trim();
            GridView1.DataBind();
        }

        protected void SqlDataSource2_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
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
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo,TipoArea FROM [Parametrizacion].[JerarquiaOrganizacional]";
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
            view.RowFilter = "TipoArea = 'A'";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.Expanded = true;
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
                newNode.Expanded = true;
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
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

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeView2()
        {
            DataTable treeViewData = GetTreeViewData2();
            AddTopTreeViewNodes2(treeViewData);
        }

        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        private DataTable GetTreeViewData2()
        {
            // Get JerarquiaOrganizacional table
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijoAuditoria FROM [Auditoria].[JerarquiaGrupoAuditoria] WHERE idGrupoAuditoria = " + lblIdGrupo.Text;
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
        private void AddTopTreeViewNodes2(DataTable treeViewData)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = 0";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijoAuditoria"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.Expanded = true;
                newNode.ToolTip = DetalleNodo(2, row["IdHijo"].ToString());
                TreeView2.Nodes.Add(newNode);
                AddChildTreeViewNodes2(treeViewData, newNode);
            }
        }

        /// <summary>
        /// Recursively add child TreeView items by filtering by ParentID
        /// </summary>
        private void AddChildTreeViewNodes2(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijoAuditoria"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.Expanded = true;
                newNode.ToolTip = DetalleNodo(2, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes2(treeViewData, newNode);
            }
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (filaDetalle.Visible == true)
            {

                if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtNombre.Text.Trim())))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Nombre.", 2, "Atención");
                    txtNombre.Focus();
                }
            }
            else if (filaDetalleJGA.Visible == true)
            {
                if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtNomJGA.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Nombre.", 2, "Atención");
                    txtNomJGA.Focus();
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
    }
}

