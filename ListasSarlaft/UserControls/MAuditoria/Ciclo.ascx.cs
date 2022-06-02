using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Ciclo : System.Web.UI.UserControl
    {
        string IdFormulario = "3001";
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtId.Enabled = false;
                txtDescripcion.Focus();
                btnImgActualizar.Visible = true;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;

                // Carga los datos en la respectiva caja de texto
                txtId.Text = GridView1.SelectedDataKey[1].ToString().Trim();
                txtNombre.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtDescripcion.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
                txtUsuario.Text = GridView1.SelectedDataKey[0].ToString().Trim();
                txtFecha.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
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
                        SqlDataSource1.UpdateParameters["IdCiclo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
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

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtNombre.Text.Trim())))
            {
                err = false;
                omb.ShowMessage("Debe ingresar el Nombre.", 2, "Atención");
                txtNombre.Focus();
            }
            else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDescripcion.Text)))
            {
                err = false;
                omb.ShowMessage("Debe ingresar la Descripción.", 2, "Atención");
                txtDescripcion.Focus();
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