using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Naturaleza : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtId.Enabled = false;
                txtDescripicion.Focus();
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                filaGrid.Visible = false;
                filaDetalle.Visible = true;

                // Carga los datos en la respectiva caja de texto
                txtId.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtDescripicion.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                // txtUsuario.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
                txtUsuario.Text = GridView1.SelectedDataKey[1].ToString().Trim();
                txtFecha.Text = GridView1.SelectedRow.Cells[2].Text.Trim();

            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
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
                SqlDataSource1.DeleteParameters["IdNaturaleza"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
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

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {

            txtId.Text = "";
            txtId.Enabled = false;
            txtDescripicion.Focus();
            txtFecha.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDescripicion.Text = "";
            txtUsuario.Text = ""; // Aca va el Codigo de Usuario logueado en la aplicacion
            btnImgInsertar.Visible = true;
            btnImgActualizar.Visible = false;
            filaDetalle.Visible = true;
            filaGrid.Visible = false;
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            SqlDataSource1.UpdateParameters["IdNaturaleza"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
            SqlDataSource1.UpdateParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripicion.Text.Trim());

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
            }

            filaDetalle.Visible = false;
            filaGrid.Visible = true;

        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            //Label2.set_Text("The record was updated successfully!");

            SqlDataSource1.InsertParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripicion.Text.Trim());
            SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = "1"; //Aca va el id del Usuario de la BD
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
            }

            filaDetalle.Visible = false;
            filaGrid.Visible = true;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int nroPag, tamPag;

            try
            {

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
    }
}