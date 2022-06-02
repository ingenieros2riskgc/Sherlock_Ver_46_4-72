using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class CalificacionControlIni : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Limpia el DropDownList Region y agrega el item vacio
                ListItem item = new ListItem();
                item.Text = "";
                item.Value = "-1";

                ddlRiesgo.Items.Clear();
                ddlRiesgo.Items.Add(item);
                ddlRiesgo.DataBind();
            }
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {


                txtId.Enabled = false;

                // Carga los datos en la respectiva caja de texto
                txtId.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtDescripcion.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
                txtUsuario.Text = GridView1.SelectedDataKey[0].ToString().Trim();
                txtFecha.Text = GridView1.SelectedRow.Cells[6].Text.Trim();
                ddlRiesgo.SelectedValue = GridView1.SelectedDataKey[1].ToString().Trim();

                filaGrid.Visible = false;
                filaDetalle.Visible = true;
                ddlRiesgo.Focus();
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;

            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            txtId.Text = "";
            txtId.Enabled = false;
            ddlRiesgo.SelectedValue = null;
            ddlRiesgo.Focus();
            txtDescripcion.Text = "";
            txtUsuario.Text = ""; //Aca va el codigo de usuario logueado
            txtFecha.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
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
                SqlDataSource1.DeleteParameters["IdCalificacionControl"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
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

            SqlDataSource1.UpdateParameters["IdCalificacionControl"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtId.Text.Trim());
            SqlDataSource1.UpdateParameters["IdRiesgoInherente"].DefaultValue = ddlRiesgo.SelectedValue;
            SqlDataSource1.UpdateParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripcion.Text);

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

            SqlDataSource1.InsertParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescripcion.Text);
            SqlDataSource1.InsertParameters["IdRiesgoInherente"].DefaultValue = ddlRiesgo.SelectedValue;
            SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = "1"; //Aca va el id del Usuario de la BD
            SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");

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

    }
}