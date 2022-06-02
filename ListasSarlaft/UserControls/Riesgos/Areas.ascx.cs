using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class Areas : System.Web.UI.UserControl
    {
        #region Variables Globales
        string IdFormulario = "5001";
        cCuenta cCuenta = new cCuenta();
        #endregion Variables Globales

        #region Atributos
        private int intRowGridArea;
        private int intPagIndexInfoGridArea;
        private DataTable dtInfoGridArea;
        #endregion Atributos

        #region Propiedades
        private int IntRowGridArea
        {
            get
            {
                intRowGridArea = (int)ViewState["intRowGridArea"];
                return intRowGridArea;
            }
            set
            {
                intRowGridArea = value;
                ViewState["intRowGridArea"] = intRowGridArea;
            }
        }

        private int IntPagIndexInfoGridArea
        {
            get
            {
                intPagIndexInfoGridArea = (int)ViewState["intPagIndexInfoGridArea"];
                return intPagIndexInfoGridArea;
            }
            set
            {
                intPagIndexInfoGridArea = value;
                ViewState["intPagIndexInfoGridArea"] = intPagIndexInfoGridArea;
            }
        }

        private DataTable DtInfoGridArea
        {
            get
            {
                dtInfoGridArea = (DataTable)ViewState["dtInfoGridArea"];
                return dtInfoGridArea;
            }
            set
            {
                dtInfoGridArea = value;
                ViewState["dtInfoGridArea"] = dtInfoGridArea;
            }
        }
        #endregion Propiedades


        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {

            }
        }

        #region Eventos
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IntPagIndexInfoGridArea = e.NewPageIndex;
            GridView1.PageIndex = IntPagIndexInfoGridArea;
            GridView1.DataSource = DtInfoGridArea;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //IntRowGridArea = (Convert.ToInt16(GridView1.PageSize) * IntPagIndexInfoGridArea) + Convert.ToInt16(e.CommandArgument);
            int intNroPag, intTamPag;

            switch (e.CommandName)
            {
                case "Modificar": //Donot insert code here!
                    break;

                case "Eliminar":
                    // Convierte el indice de la fila del GridView almacenado en la propiedad CommandArgument a un tipo entero
                    int intIndex = Convert.ToInt32((e.CommandArgument).ToString());

                    intNroPag = GridView1.PageIndex;  // Obtiene el Numero de Pagina en la que se encuentra el GridView
                    intTamPag = GridView1.PageSize; // Obtiene el Tamano de cada Pagina del GridView
                    intIndex = (intIndex - intTamPag * intNroPag); // Calcula el Numero de Fila del GridView dentro de la pagina actual

                    // Recupera la fila que contiene el boton al que se le hizo click por el usuario de la coleccion Rows
                    GridViewRow row = GridView1.Rows[intIndex];

                    // Obtiene el Id del registro a Eliminar
                    txtIdArea.Text = row.Cells[0].Text.Trim();
                    break;
            }




        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtIdArea.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtNombreArea.Text =  WebUtility.HtmlDecode(GridView1.SelectedRow.Cells[1].Text);
                txtIdUsuario.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
                txtFechaUltModificacion.Text = GridView1.SelectedRow.Cells[3].Text.Trim();

                if (GridView1.SelectedRow.Cells[2].Text == string.Empty || GridView1.SelectedRow.Cells[2].Text == "&nbsp;")
                    TexCodigo.Text = string.Empty;
                else
                    TexCodigo.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
                mtdHabilitarControles(true, 2);
            }
        }

        protected void BtnPreInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
            else
            {
                mtdLimpiarValores();
                mtdHabilitarControles(true, 1);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdHabilitarControles(false, 1);
        }

        protected void btnActualizarArea_Click(object sender, ImageClickEventArgs e)
        {
            string strMensaje = string.Empty;

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            }
            else
            {
                if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(txtNombreArea.Text.Trim())))
                {
                    omb.ShowMessage("Debe ingresar el Nombre del área.", 2, "Atención");
                    txtNombreArea.Focus();
                }
                else
                {
                    #region Ejecucion Consulta
                    try
                    {
                        SqlDataSource1.UpdateParameters["IdArea"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtIdArea.Text);
                        SqlDataSource1.UpdateParameters["NombreArea"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreArea.Text.Trim());
                        SqlDataSource1.UpdateParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                        SqlDataSource1.UpdateParameters["FechaUltModificacion"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        SqlDataSource1.UpdateParameters["Codigo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(TexCodigo.Text.Trim());
                        SqlDataSource1.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        mtdHabilitarControles(false, 2);
                    }
                    catch (Exception except)
                    {
                        strMensaje = string.Format("Error en la actualización de la información.<br/> Descripción: {0}.", except.Message.ToString());
                        omb.ShowMessage(strMensaje, 1, "Atención");
                        mtdHabilitarControles(false, 2);
                    }
                    #endregion Ejecucion Consulta
                }
            }
        }

        protected void btnInsertarArea_Click(object sender, ImageClickEventArgs e)
        {
            string strMensaje = string.Empty;

            if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(txtNombreArea.Text.Trim())))
            {
                omb.ShowMessage("Debe ingresar el Nombre del área.", 2, "Atención");
                txtNombreArea.Focus();
            }
            else
            {
                #region Ejecucion Consulta
                try
                {
                    SqlDataSource1.InsertParameters["NombreArea"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNombreArea.Text.Trim());
                    SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                    SqlDataSource1.InsertParameters["FechaUltModificacion"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    SqlDataSource1.InsertParameters["Codigo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(TexCodigo.Text.Trim());
                    SqlDataSource1.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    mtdHabilitarControles(false, 1);
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    strMensaje = string.Format("Error en la inserción de la información.<br/> Descripción: {0}.", except.Message.ToString());
                    omb.ShowMessage(strMensaje, 1, "Atención");
                    mtdHabilitarControles(false, 1);
                }
                #endregion Ejecucion Consulta
            }
        }

        protected void btnEliminarArea_Click(object sender, ImageClickEventArgs e)
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
            string strMensaje = string.Empty;
            mpeMsgBox.Hide();

            try
            {
                SqlDataSource1.DeleteParameters["IdArea"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtIdArea.Text);
                SqlDataSource1.Delete();

                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
            }
            catch (Exception except)
            {
                strMensaje = string.Format("Error en la eliminación de la información.<br/>Descripción: {0}.", except.Message.ToString());
                omb.ShowMessage(strMensaje, 1, "Atención");
            }
        }

        #endregion Eventos

        #region Metodos Privados
        private void mtdLimpiarValores()
        {
            txtNombreArea.Text = string.Empty;
            TexCodigo.Text = string.Empty;
        }

        private void mtdHabilitarControles(bool booHabilitar, int intTipoAccion)
        {
            switch (intTipoAccion)
            {
                case 1:
                    trFilaDetalle.Visible = booHabilitar;
                    btnImgInsertar.Visible = booHabilitar;
                    btnImgActualizar.Visible = !booHabilitar;
                    break;

                case 2:
                    trFilaDetalle.Visible = booHabilitar;
                    btnImgInsertar.Visible = !booHabilitar;
                    btnImgActualizar.Visible = booHabilitar;
                    break;
            }
        }

        
        #endregion Metodos Privados
    }
}