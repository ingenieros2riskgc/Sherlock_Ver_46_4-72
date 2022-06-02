using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Parametrizacion
{
    public partial class OtrosTipos : System.Web.UI.UserControl
    {
        string IdFormulario = "2003";
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
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
                        txtId.Text = "0";
                        GridView2.DataBind();
                    }
                }
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
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
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

            if (filaGrid.Visible == true)
            {
                try
                {
                    SqlDataSource1.DeleteParameters["IdTipo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                    SqlDataSource1.Delete();
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }
            }
            else if (filaOpcion.Visible == true)
            {
                try
                {
                    SqlDataSource2.DeleteParameters["IdDetalleTipo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodigoOpcion.Text);
                    SqlDataSource2.Delete();
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }
            }

            if (!err)
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
            else
            {
                txtId.Text = "";
                txtId.Enabled = false;
                txtDescripicion.Focus();
                txtFecha.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtDescripicion.Text = "";
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
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource1.UpdateParameters["IdTipo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                        SqlDataSource1.UpdateParameters["NombreTipo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripicion.Text.Trim());
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
                    SqlDataSource1.InsertParameters["NombreTipo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripicion.Text.Trim());
                    SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
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

        protected void btnImgLiteral_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void imgBtnInsertarOpcion_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
            else
            {
                txtCodigoOpcion.Text = "";
                txtNombreOpcion.Text = "";
                txtFechaOpcion.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtNombreOpcion.Focus();
                btnImgInsertarOp.Visible = true;
                btnImgActualizarOp.Visible = false;
                filaDetalleOpcion.Visible = true;
                filaOpcion.Visible = false;
            }
        }

        protected void btnImgEliminarOpcion_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
            else
            {
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnVolverTipos_Click(object sender, EventArgs e)
        {
            filaGrid.Visible = true;
            filaOpcion.Visible = false;
        }

        protected void btnImgCancelarOp_Click(object sender, ImageClickEventArgs e)
        {
            filaOpcion.Visible = true;
            filaDetalleOpcion.Visible = false;
        }

        protected void btnImgActualizarOp_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource2.UpdateParameters["IdDetalleTipo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodigoOpcion.Text);
                        SqlDataSource2.UpdateParameters["NombreDetalle"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreOpcion.Text);
                        SqlDataSource2.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalleOpcion.Visible = false;
                        filaOpcion.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgInsertarOp_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource2.InsertParameters["NombreDetalle"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreOpcion.Text);
                    SqlDataSource2.InsertParameters["IdTipo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
                    SqlDataSource2.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource2.Insert();
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleOpcion.Visible = false;
                    filaOpcion.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                // Carga los datos en la respectiva caja de texto
                txtId.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtTipo.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtDescripicion.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtFecha.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Seleccionar")
            {
                txtId.Enabled = false;
                txtDescripicion.Focus();
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;
            }

            if (e.CommandArgument.ToString() == "Opcion")
            {
                filaGrid.Visible = false;
                filaOpcion.Visible = true;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoOpcion.Text = GridView2.SelectedRow.Cells[0].Text.Trim();
            txtNombreOpcion.Text = GridView2.SelectedRow.Cells[1].Text.Trim();
            txtFechaOpcion.Text = GridView2.SelectedRow.Cells[2].Text.Trim();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "SelectOp")
            {
                txtNombreOpcion.Focus();
                btnImgInsertarOp.Visible = false;
                btnImgActualizarOp.Visible = true;
                filaOpcion.Visible = false;
                filaDetalleOpcion.Visible = true;
            }
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (filaDetalle.Visible == true)
            {
                if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDescripicion.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Descripción.", 2, "Atención");
                    txtDescripicion.Focus();
                }
            }
            else if (filaDetalleOpcion.Visible == true)
            {
                if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtNombreOpcion.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Nombre.", 2, "Atención");
                    txtNombreOpcion.Focus();
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