using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Notificaciones
{
    public partial class CorreosDestino : System.Web.UI.UserControl
    {
        string IdFormulario = "2002";
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
                {
                    Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
                }
                else
                {
                    if (cCuenta.permisosConsulta(IdFormulario) == "False")
                        Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1",false);
                    else
                    {
                        Page.Form.Attributes.Add("enctype", "multipart/form-data");
                        ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);

                        if (!Page.IsPostBack)
                        {
                            TreeNodeCollection nodes = this.TreeView2.Nodes;
                            if (nodes.Count <= 0)
                                PopulateTreeView(2);
                        }
                    }
                }
            }
            catch
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
        }

        #region Buttons
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;

            TreeView2.Nodes.Clear();
            PopulateTreeView(2);
            TreeView2.ExpandAll();
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            txtUsuario.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
            txtFecha.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            chkJefeInmediato.Checked = false;
            chkJefeMediato.Checked = false;
            btnImgInsertar.Visible = true;
            btnImgActualizar.Visible = false;
            filaDetalle.Visible = true;
            filaGrid.Visible = false;
        }

        protected void btnImgEliminar_Click(object sender, ImageClickEventArgs e)
        {

            lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
            mpeMsgBox.Show();
        }

        protected void btnImgokEliminar_Click(object sender, EventArgs e)
        {
            bool err = false;

            mpeMsgBox.Hide();

            try
            {
                //SqlDataSource1.DeleteParameters["IdPais"].DefaultValue = txtId.Text.Trim();
                SqlDataSource1.Delete();
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
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
                        SqlDataSource1.UpdateParameters["IdCorreosDestinatarios"].DefaultValue = lblIdCorreosDestino.Text;
                        SqlDataSource1.UpdateParameters["Destinatario"].DefaultValue = "";
                        SqlDataSource1.UpdateParameters["Copia"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCopia1.Text);
                        SqlDataSource1.UpdateParameters["Otros"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtOtros.Text);
                        SqlDataSource1.UpdateParameters["Asunto"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAsunto.Text);
                        SqlDataSource1.UpdateParameters["Cuerpo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCuerpo.Text);
                        SqlDataSource1.UpdateParameters["NroDiasRecordatorio"].DefaultValue = txtNroDias.Text;
                        if (chkJefeInmediato.Checked)
                            SqlDataSource1.UpdateParameters["AJefeInmediato"].DefaultValue = "SI";
                        else
                            SqlDataSource1.UpdateParameters["AJefeInmediato"].DefaultValue = "NO";
                        if (chkJefeMediato.Checked)
                            SqlDataSource1.UpdateParameters["AJefeMediato"].DefaultValue = "SI";
                        else
                            SqlDataSource1.UpdateParameters["AJefeMediato"].DefaultValue = "NO";

                        SqlDataSource1.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;
                        TreeView2.Nodes.Clear();
                        PopulateTreeView(2);
                        TreeView2.ExpandAll();
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
            bool err = false;
            cMail cMail = new cMail();
            string NombreEvento = Sanitizer.GetSafeHtmlFragment(TXnombreEvento.Text);
            string modulo = DDLmodulos.SelectedValue;
            //int CodModulo = Convert.ToInt32(lblCodModulo.Text);
            string RequiereFechaCierre = string.Empty;
            RequiereFechaCierre = RBLrequiereFechaCierre.SelectedValue;
            //cMail.insertEventoMail(NombreEvento, modulo, CodModulo, RequiereFechaCierre);
            int LasId = cMail.LastIdEventoMail();
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource1.InsertParameters["IdEvento"].DefaultValue = LasId.ToString();
                    SqlDataSource1.InsertParameters["Destinatario"].DefaultValue = "";
                    SqlDataSource1.InsertParameters["Copia"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCopia1.Text);
                    SqlDataSource1.InsertParameters["Otros"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtOtros.Text);
                    SqlDataSource1.InsertParameters["Asunto"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAsunto.Text);
                    SqlDataSource1.InsertParameters["Cuerpo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCuerpo.Text);
                    SqlDataSource1.InsertParameters["NroDiasRecordatorio"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNroDias.Text);
                    SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    if (chkJefeInmediato.Checked)
                        SqlDataSource1.InsertParameters["AJefeInmediato"].DefaultValue = "SI";
                    else
                        SqlDataSource1.InsertParameters["AJefeInmediato"].DefaultValue = "NO";
                    if (chkJefeMediato.Checked)
                        SqlDataSource1.InsertParameters["AJefeMediato"].DefaultValue = "SI";
                    else
                        SqlDataSource1.InsertParameters["AJefeMediato"].DefaultValue = "NO";

                    SqlDataSource1.Insert();
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalle.Visible = false;
                    filaGrid.Visible = true;

                    TreeView2.Nodes.Clear();
                    PopulateTreeView(2);
                    TreeView2.ExpandAll();
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }
        #endregion Buttons

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row;
            double div;

            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtModulo.Enabled = false;
                txtEtapa.Enabled = false;
                chkJefeInmediato.Checked = false;
                chkJefeMediato.Checked = false;

                // Carga los datos en la respectiva caja de texto
                txtModulo.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtEtapa.Text = GridView1.SelectedRow.Cells[2].Text.Trim();

                txtUsuario.Text = GridView1.SelectedDataKey[0].ToString().Trim();
                lblIdControl.Text = GridView1.SelectedDataKey[1].ToString().Trim();
                lblIdCorreosDestino.Text = GridView1.SelectedDataKey[2].ToString().Trim();
                txtCopia1.Text = GridView1.SelectedDataKey[4].ToString().Trim();
                txtOtros.Text = GridView1.SelectedDataKey[5].ToString().Trim();
                txtAsunto.Text = GridView1.SelectedDataKey[6].ToString().Trim();
                txtCuerpo.Text = GridView1.SelectedDataKey[7].ToString().Trim();
                txtNroDias.Text = GridView1.SelectedDataKey[8].ToString().Trim();
                txtFecha.Text = GridView1.SelectedDataKey[9].ToString().Trim();

                if (GridView1.SelectedDataKey[10].ToString().Trim() == "SI") chkJefeInmediato.Checked = true;
                if (GridView1.SelectedDataKey[11].ToString().Trim() == "SI") chkJefeMediato.Checked = true;
                if (GridView1.SelectedDataKey[12].ToString().Trim() == "SI")
                    filaNroDias.Visible = true;
                else
                    filaNroDias.Visible = false;

                if (txtUsuario.Text == "")
                {
                    txtUsuario.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                    txtFecha.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    btnImgInsertar.Visible = true;
                    btnImgActualizar.Visible = false;
                }
                else
                {
                    btnImgInsertar.Visible = false;
                    btnImgActualizar.Visible = true;
                }

                //Modifica la altura del campo de correos de acuerdo a la longitud del texto
                if (txtCopia1.Text.Trim().Length < 99)
                {
                    txtCopia1.Height = 15;
                    txtCopia1.TextMode = TextBoxMode.SingleLine;
                }
                else
                {
                    div = txtCopia1.Text.Trim().Length / 99;
                    row = (int)Math.Truncate(div);
                    txtCopia1.Height = 15 * (row + 1);
                    txtCopia1.TextMode = TextBoxMode.MultiLine;
                }

                filaGrid.Visible = false;
                filaDetalle.Visible = true;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nroPag, tamPag;

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
                //txtId.Text = row.Cells[0].Text.Trim();
            }
        }

        #region Treeview
        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            string subcadena = string.Empty;
            int row;
            double div;

            subcadena = TreeView2.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);

            if (txtCopia1.Text != "")
                txtCopia1.Text = Sanitizer.GetSafeHtmlFragment(txtCopia1.Text.Trim()) + ";" + subcadena.Trim();
            else
                txtCopia1.Text = subcadena;

            //Va modificando la altura del campo de correos de acuerdo a la longitud del texto
            if (txtCopia1.Text.Trim().Length < 99)
            {
                txtCopia1.Height = 15;
                txtCopia1.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
                div = txtCopia1.Text.Trim().Length / 99;
                row = (int)Math.Truncate(div);
                txtCopia1.Height = 15 * (row + 1);
                txtCopia1.TextMode = TextBoxMode.MultiLine;
            }

            popupCopia1.Cancel();
        }

        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeView(int numTV)
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData, numTV);
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
        private void AddTopTreeViewNodes(DataTable treeViewData, int numTV)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                if (numTV == 2)
                    TreeView2.Nodes.Add(newNode);
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

        protected void imgBorrarCopia1_Click(object sender, ImageClickEventArgs e)
        {
            txtCopia1.Text = "";
        }

        protected void txtCopia1_TextChanged(object sender, EventArgs e)
        {
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;
            string correos = "";

            //if (ValidarCadenaVacia(txtDestinatario.Text))
            //{
            //    err = false;
            //    omb.ShowMessage("Debe ingresar el Destinatario.", 2, "Atención");
            //    txtDestinatario.Focus();
            //}

            correos = Sanitizer.GetSafeHtmlFragment(txtOtros.Text);

            if (txtOtros.Text != "")
            {
                foreach (string substr in correos.Split(';'))
                {
                    if (!RegexUtilities.IsValidEmail(substr))
                    {
                        err = false;
                        omb.ShowMessage("El correo " + substr + " en el campo Otros es invalido.", 2, "Atención");
                        txtOtros.Focus();
                        break;
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

        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = false;
            filaDetalle.Visible = true;

            btnImgInsertar.Visible = true;
            btnImgActualizar.Visible = false;
        }

        protected void DDLmodulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string modulo = DDLmodulos.SelectedValue;
            cMail cMail = new cMail();
            int LastCodModulo = cMail.LastCodModulo(modulo);
            LastCodModulo++;
            lblCodModulo.Text = LastCodModulo.ToString();
        }
    }
}