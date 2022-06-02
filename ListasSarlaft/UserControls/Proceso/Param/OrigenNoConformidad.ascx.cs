using ListasSarlaft.Classes;
using ListasSarlaft.Classes.BLL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Proceso.Param
{
    public partial class OrigenNoConformidad : UserControl
    {
        private cCuenta cCuenta = new cCuenta();
        string IdFormulario = "4051";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            if (!Page.IsPostBack)
            {
                try
                {
                    // Controla si se va a insertar o actualizar
                    Session["IdActualiza"] = 0;
                    CargarInformacion();
                }
                catch (Exception ex)
                {
                    omb.ShowMessage($"Error al cargar la información. {ex.Message}", 1, "Atención");
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Modificar": Modificar(Convert.ToInt32(e.CommandArgument)); break;

                default:
                    break;
            }
        }

        protected void BtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if ((int)Session["IdActualiza"] > 0)
                {
                    if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    {
                        omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                        return;
                    }
                }
                else
                {
                    if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    {
                        omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                        return;
                    }
                }
                Classes.DTO.Calidad.OrigenNoConformidad noConformidad = new Classes.DTO.Calidad.OrigenNoConformidad
                {
                    IdOrigenNoConformidad = Session["IdActualiza"].Equals(0) ? 0 : (int)Session["IdActualiza"],
                    Nombre = txtNombre.Text,
                    IdUsuario = Convert.ToInt32(Session["IdUsuario"])
                };
                int result = InsertarActualizarOrigen(noConformidad);
                if (result > 0)
                {
                    if (noConformidad.IdOrigenNoConformidad == 0)
                        omb.ShowMessage("Origen de no conformidad insertado con éxito.", 3, "Información");
                    else
                        omb.ShowMessage("Origen de no conformidad actualizado con éxito.", 3, "Información");
                    CargarInformacion();
                    Reset();
                }
                else
                    omb.ShowMessage("No se insertó el valor.", 2, "Información");

                Session["IdActualiza"] = 0;

            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al insertar el origen de no conformidad. {ex.Message}", 1, "Atención");
            }
        }

        protected void BtnLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            Reset();
        }

        public void CargarInformacion()
        {
            try
            {
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    GridView1.DataSource = objData.SelectOrigenNoConformidad();
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Reset()
        {
            txtNombre.Text = string.Empty;
            Session["IdActualiza"] = 0;
        }

        public int InsertarActualizarOrigen(Classes.DTO.Calidad.OrigenNoConformidad noConformidad)
        {
            try
            {
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    int result = objData.InsertOrigenNoConformidad(noConformidad);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(int rowIndex)
        {
            try
            {
                GridViewRow row = GridView1.Rows[rowIndex];
                txtNombre.Text = row.Cells[1].Text;
                Session["IdActualiza"] = Convert.ToInt32(row.Cells[0].Text);

            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar el origen de no conformidad. {ex.Message}", 1, "Atención");
            }
        }
    }
}