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
using System.Net.Mail;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Notificaciones
{
    public partial class CorreosEnviados : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "2002";

        private static int LastInsertIdCE;

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
                    {
                        Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1",false);
                    }
                }
            }
            catch
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
        }

        #region Treeview
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string subcadena;
            subcadena = TreeView1.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);
            txtDestinatario.Text = subcadena;
        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            string subcadena;
            subcadena = TreeView2.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);
            txtCopia1.Text = subcadena;
        }

        protected void TreeView3_SelectedNodeChanged(object sender, EventArgs e)
        {
            string subcadena;
            subcadena = TreeView3.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);
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
                if (numTV == 1)
                    TreeView1.Nodes.Add(newNode);
                else if (numTV == 2)
                    TreeView2.Nodes.Add(newNode);
                else if (numTV == 3)
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

        #region Buttons
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
            filaBoton.Visible = true;
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            txtUsuario.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
            txtFecha.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnImgInsertar.Visible = true;
            btnImgActualizar.Visible = false;
            filaDetalle.Visible = true;
            filaGrid.Visible = false;
            filaBoton.Visible = false;
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
            bool err = false;

            if (VerificarCampos())
            {
                //SqlDataSource1.UpdateParameters["IdCorreosDestinatarios"].DefaultValue = lblIdCorreosDestino.Text;
                SqlDataSource1.UpdateParameters["Destinatario"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDestinatario.Text);
                SqlDataSource1.UpdateParameters["Copia"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCopia1.Text);
                SqlDataSource1.UpdateParameters["Otros"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtOtros.Text);
                SqlDataSource1.UpdateParameters["Asunto"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAsunto.Text);
                SqlDataSource1.UpdateParameters["Cuerpo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCuerpo.Text);
                //SqlDataSource1.UpdateParameters["NroDiasRecordatorio"].DefaultValue = txtNroDias.Text;

                try
                {
                    SqlDataSource1.Update();
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                    filaDetalle.Visible = false;
                    filaGrid.Visible = true;
                    filaBoton.Visible = true;
                }
            }
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            if (VerificarCampos())
            {
                SqlDataSource1.InsertParameters["IdControlUsuario"].DefaultValue = lblIdControl.Text;
                SqlDataSource1.InsertParameters["Destinatario"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDestinatario.Text);
                SqlDataSource1.InsertParameters["Copia"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCopia1.Text);
                SqlDataSource1.InsertParameters["Otros"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtOtros.Text);
                SqlDataSource1.InsertParameters["Asunto"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAsunto.Text);
                SqlDataSource1.InsertParameters["Cuerpo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCuerpo.Text);
                SqlDataSource1.InsertParameters["Estado"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtEstado.Text);
                SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();

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
                    filaDetalle.Visible = false;
                    filaGrid.Visible = true;
                    filaBoton.Visible = true;
                }
            }
        }

        protected void imgBorrarDest_Click(object sender, ImageClickEventArgs e)
        {
            txtDestinatario.Text = "";
        }

        protected void imgBorrarCopia1_Click(object sender, ImageClickEventArgs e)
        {
            txtCopia1.Text = "";
        }

        protected void imgBorrarCopia2_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void btnEnviarCorreos_Click(object sender, EventArgs e)
        {
            boolEnviarNotificacion();
        }
        #endregion

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtModulo.Enabled = false;
                txtEtapa.Enabled = false;

                // Carga los datos en la respectiva caja de texto
                txtModulo.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtEtapa.Text = GridView1.SelectedRow.Cells[2].Text.Trim();

                txtUsuario.Text = GridView1.SelectedDataKey[0].ToString().Trim();
                lblIdControl.Text = GridView1.SelectedDataKey[1].ToString().Trim();
                lblIdCorreosEnviados.Text = GridView1.SelectedDataKey[2].ToString().Trim();
                txtDestinatario.Text = GridView1.SelectedDataKey[3].ToString().Trim();
                txtCopia1.Text = GridView1.SelectedDataKey[4].ToString().Trim();
                txtOtros.Text = GridView1.SelectedDataKey[5].ToString().Trim();
                txtAsunto.Text = GridView1.SelectedDataKey[6].ToString().Trim();
                txtCuerpo.Text = GridView1.SelectedDataKey[7].ToString().Trim();
                txtEstado.Text = GridView1.SelectedDataKey[8].ToString().Trim();
                txtFecha.Text = GridView1.SelectedDataKey[9].ToString().Trim();
                txtFechaEnvio.Text = GridView1.SelectedDataKey[10].ToString().Trim();

                filaGrid.Visible = false;
                filaDetalle.Visible = true;
                filaBoton.Visible = false;
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

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDestinatario.Text)))
            {
                err = false;
                omb.ShowMessage("Debe ingresar el Destinatario.", 2, "Atención");
                txtDestinatario.Focus();
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

        private void boolEnviarNotificacion()
        {
            bool err = false;
            string Destinatario = "", Copia1 = "", /*Copia2 = "", */Asunto = "", Otros = "", Cuerpo = "", /*NroDiasRecordatorio = "", */IdCorreosEnviados = "";
            string selectCommand = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
            selectCommand = "SELECT IdCorreosEnviados,Destinatario,Copia,Otros,Asunto,Cuerpo FROM [Notificaciones].[CorreosEnviados] WHERE Estado = 'POR ENVIAR'";

            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            foreach (DataRowView row in view)
            {
                err = false;

                Destinatario = row["Destinatario"].ToString().Trim();
                Copia1 = row["Copia"].ToString().Trim();
                Otros = row["Otros"].ToString().Trim();
                Asunto = row["Asunto"].ToString().Trim();
                Cuerpo = row["Cuerpo"].ToString().Trim();
                IdCorreosEnviados = row["IdCorreosEnviados"].ToString().Trim();

                try
                {
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");
                    message.From = fromAddress;//here you can set address

                    #region
                    //here you can add multiple to
                    foreach (string strDest in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(strDest))
                            message.To.Add(strDest);
                    }
                    #endregion

                    #region
                    //ccing the same email to other email address
                    if (!string.IsNullOrEmpty(Copia1.Trim()))
                    {
                        foreach (string strCopy in Copia1.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(strCopy))
                                message.CC.Add(strCopy);
                        }
                    }
                    #endregion

                    #region
                    if (!string.IsNullOrEmpty(Otros.Trim()))
                    {
                        foreach (string substr in Otros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr))
                                message.CC.Add(substr);
                        }
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
                    omb.ShowMessage("Error en el envio de la notificacion." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = IdCorreosEnviados;
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                    GridView1.DataBind();
                }
            }
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }
    }
}