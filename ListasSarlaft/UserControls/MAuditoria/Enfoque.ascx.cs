using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Enfoque : System.Web.UI.UserControl
    {
        string IdFormulario = "3001";
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                GridView1.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
                GridView2.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
                if (!Page.IsPostBack)
                {
                    ddlEstandar.Items.Clear();
                    ddlEstandar.DataBind();
                    GridView1.DataBind();
                    txtId.Text = "0";
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rows = 0, longMax = 0, rowsAdd = 0;
            double temp = 0;

            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                //Carga los datos en la respectiva caja de texto
                txtEstandar.Text = ddlEstandar.Items[ddlEstandar.SelectedIndex].Text;
                txtEstandarDL.Text = ddlEstandar.Items[ddlEstandar.SelectedIndex].Text;
                txtEstandarL.Text = ddlEstandar.Items[ddlEstandar.SelectedIndex].Text;
                txtObjetivo.Text = ddlObjetivo.Items[ddlObjetivo.SelectedIndex].Text;
                txtObjetivoL.Text = ddlObjetivo.Items[ddlObjetivo.SelectedIndex].Text;
                txtObjetivoDL.Text = ddlObjetivo.Items[ddlObjetivo.SelectedIndex].Text;
                txtNumeroE.Text = GridView1.SelectedDataKey[3].ToString().Trim();
                txtId.Text = GridView1.SelectedDataKey[1].ToString().Trim();
                txtDescripcion.Text = GridView1.SelectedRow.Cells[3].Text.Trim();
                txtUsuario.Text = GridView1.SelectedDataKey[0].ToString().Trim();
                txtFecha.Text = GridView1.SelectedRow.Cells[4].Text.Trim();

                txtEnfoque.Height = 18;
                txtEnfoque.Width = 402;
                txtEnfoqueDL.Height = 18;
                txtEnfoqueDL.Width = 402;
                //Revisa la longitud max del texto y el número de líneas
                foreach (string strItem in Regex.Split(GridView1.SelectedRow.Cells[3].Text, "</div>"))
                {
                    rows = rows + 1;
                    if (strItem.Length > longMax) longMax = strItem.Length;

                    if (strItem.Length > 126)
                    {
                        temp = strItem.Length / 126;
                        rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                    }
                }

                txtEnfoque.Text = GridView1.SelectedRow.Cells[3].Text.Trim();
                txtEnfoqueDL.Text = GridView1.SelectedRow.Cells[3].Text.Trim();

                if (rows + rowsAdd > 1)
                {
                    txtEnfoque.Height = (rows + rowsAdd) * 18;
                    txtEnfoqueDL.Height = (rows + rowsAdd) * 18;
                }

                if (longMax > 72)
                {
                    txtEnfoque.Width = 700;
                    txtEnfoqueDL.Width = 700;
                }

                else
                {
                    txtEnfoque.Width = 402;
                    txtEnfoqueDL.Width = 402;
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;
                txtId.Enabled = false;
                txtDescripcion.Focus();
            }

            if (e.CommandArgument.ToString() == "Literal")
            {
                filaGrid.Visible = false;
                filaLiteral.Visible = true;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoDL.Text = GridView2.SelectedDataKey[1].ToString().Trim();
            txtDescripcionDL.Text = GridView2.SelectedRow.Cells[3].Text.Trim();
            txtUsuarioDL.Text = GridView2.SelectedDataKey[0].ToString().Trim();
            txtNumeroDL.Text = GridView2.SelectedDataKey[3].ToString().Trim();
            txtFechaDL.Text = GridView2.SelectedRow.Cells[4].Text.Trim();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "SelectDL")
            {
                txtDescripcionDL.Focus();
                btnImgInsertarDL.Visible = false;
                btnImgActualizarDL.Visible = true;
                filaLiteral.Visible = false;
                filaDetalleLiteral.Visible = true;
            }

            if (e.CommandName == "Eliminar")
            {
                int nroPag, tamPag;

                // Convierte el indice de la fila del GridView almacenado en la propiedad CommandArgument a un tipo entero
                int index = Convert.ToInt32((e.CommandArgument).ToString());

                nroPag = GridView2.PageIndex;  // Obtiene el Numero de Pagina en la que se encuentra el GridView
                tamPag = GridView2.PageSize; // Obtiene el Tamano de cada Pagina del GridView

                index = (index - tamPag * nroPag); // Calcula el Numero de Fila del GridView dentro de la pagina actual

                // Recupera la fila que contiene el boton al que se le hizo click por el usuario de la coleccion Rows
                GridViewRow row = GridView2.Rows[index];

                // Obtiene el Id del registro a Eliminar
                row.Cells[0].Visible = true;
                GridView2.SelectedDataKey[1].ToString().Trim();
                txtCodigoDL.Text = row.Cells[0].Text.Trim();
            }
        }

        protected void ddlEstandar_DataBound(object sender, EventArgs e)
        {
            ddlEstandar.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEstandar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlObjetivo.SelectedValue = null;
            GridView1.DataBind();
            imgBtnInsertar.Visible = false;
        }

        protected void ddlObjetivo_DataBound(object sender, EventArgs e)
        {
            ddlObjetivo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlObjetivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlObjetivo.Text == "0")
            {
                imgBtnInsertar.Visible = false;
                ddlObjetivo.SelectedValue = null;
                GridView1.DataBind();
                GridView2.DataBind();
            }
            else
                imgBtnInsertar.Visible = true;
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
                        SqlDataSource1.UpdateParameters["IdEnfoque"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                        SqlDataSource1.UpdateParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripcion.Text);
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
                    SqlDataSource1.InsertParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripcion.Text);
                    SqlDataSource1.InsertParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                    SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource1.InsertParameters["Numero"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNumeroE.Text);
                    SqlDataSource1.Insert();
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalle.Visible = false;
                    filaGrid.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
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
                try
                {
                    SqlDataSource6.SelectParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                    DataView dvEnfoque = (DataView)this.SqlDataSource6.Select(new DataSourceSelectArguments());
                    txtNumeroE.Text = dvEnfoque[0]["Maximo"].ToString().Trim();

                    txtId.Text = "";
                    txtEstandar.Text = ddlEstandar.Items[ddlEstandar.SelectedIndex].Text;
                    txtObjetivo.Text = ddlObjetivo.Items[ddlObjetivo.SelectedIndex].Text;
                    txtDescripcion.Text = "";
                    txtDescripcion.Focus();
                    txtUsuario.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                    txtFecha.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    btnImgInsertar.Visible = true;
                    btnImgActualizar.Visible = false;
                    filaDetalle.Visible = true;
                    filaGrid.Visible = false;
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }

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
                    SqlDataSource1.DeleteParameters["IdEnfoque"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                    SqlDataSource1.Delete();
                }
                else if (filaLiteral.Visible == true)
                {
                    SqlDataSource4.DeleteParameters["IdDetalleEnfoque"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodigoDL.Text);
                    SqlDataSource4.Delete();
                }
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    omb.ShowMessage("Error en la eliminación de la información. <br/> La información a borrar tiene relación con los literales. <br/> Por favor revise la información.", 1, "Atención");
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

        protected void btnImgLiteral_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void imgBtnInsertarDL_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                // Calcula el número maximo para el consecutivo del literal por enfoque
                SqlDataSource5.SelectParameters["IdEnfoque"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                DataView dvDetJerarquia = (DataView)this.SqlDataSource5.Select(new DataSourceSelectArguments());
                txtNumeroDL.Text = dvDetJerarquia[0]["Maximo"].ToString().Trim();
                txtDescripcionDL.Text = "";
                txtUsuarioDL.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaDL.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                txtDescripcionDL.Focus();
                btnImgInsertarDL.Visible = true;
                btnImgActualizarDL.Visible = false;
                filaDetalleLiteral.Visible = true;
                filaLiteral.Visible = false;
            }
        }

        protected void btnImgEliminarLiteral_Click(object sender, ImageClickEventArgs e)
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

        protected void btnVolverEnfoque_Click(object sender, EventArgs e)
        {
            filaGrid.Visible = true;
            filaLiteral.Visible = false;
        }

        protected void btnImgCancelarDL_Click(object sender, ImageClickEventArgs e)
        {
            filaLiteral.Visible = true;
            filaDetalleLiteral.Visible = false;
        }

        protected void btnImgActualizarDL_Click(object sender, ImageClickEventArgs e)
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
                        SqlDataSource4.UpdateParameters["IdDetalleEnfoque"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodigoDL.Text);
                        SqlDataSource4.UpdateParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripcionDL.Text);
                        SqlDataSource4.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalleLiteral.Visible = false;
                        filaLiteral.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgInsertarDL_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource4.InsertParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripcionDL.Text);
                    SqlDataSource4.InsertParameters["Numero"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNumeroDL.Text);
                    SqlDataSource4.InsertParameters["IdEnfoque"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                    SqlDataSource4.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource4.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource4.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleLiteral.Visible = false;
                    filaLiteral.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (filaDetalle.Visible == true)
            {
                if (ddlObjetivo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Objetivo.", 2, "Atención");
                    ddlObjetivo.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDescripcion.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Nombre.", 2, "Atención");
                    txtDescripcion.Focus();
                }
            }
            else if (filaDetalleLiteral.Visible == true)
            {
                if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDescripcionDL.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Descripción.", 2, "Atención");
                    txtDescripcionDL.Focus();
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