using clsDTO;
using clsLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class SenalAlertaComparativa : UserControl
    {
        private clsPerfil cPerfil = new clsPerfil();
        clsCuenta cCuenta = new clsCuenta();
        Classes.cCuenta ccCuenta = new Classes.cCuenta();
        string IdFormulario = "11007";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                CargarDropDownLists();
            }
        }

        public void CargarDropDownLists()
        {
            try
            {
                List<ArchivoSegmentacion> lst = cPerfil.ConsultarArchivos();
                List<CabeceroArchivo> lst1 = cPerfil.ConsultarCabecero().ToList();
                ddlArchivo1.DataSource = lst;
                ddlArchivo1.DataTextField = "UrlArchivo";
                ddlArchivo1.DataValueField = "IdArchivo";
                ddlArchivo1.DataBind();
                ddlArchivo2.DataSource = lst;
                ddlArchivo2.DataTextField = "UrlArchivo";
                ddlArchivo2.DataValueField = "IdArchivo";
                ddlArchivo2.DataBind();
                ddlVariable1.DataSource = lst1;
                ddlVariable1.DataTextField = "Cabecero";
                ddlVariable1.DataValueField = "Posicion";
                ddlVariable1.DataBind();
                ddlVariable2.DataSource = lst1;
                ddlVariable2.DataTextField = "Cabecero";
                ddlVariable2.DataValueField = "Posicion";
                ddlVariable2.DataBind();
                ddlArchivo1.Items.Insert(0, new ListItem("Seleccione...", ""));
                ddlArchivo2.Items.Insert(0, new ListItem("Seleccione...", ""));
                ddlVariable1.Items.Insert(0, new ListItem("Seleccione...", ""));
                ddlVariable2.Items.Insert(0, new ListItem("Seleccione...", ""));
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los archivos. {ex.Message}", 1, "Atencion");
            }
        }

        protected void ibtnAnalizar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int idArchivo1 = ddlArchivo1.SelectedValue.Equals("") ? 0 : Convert.ToInt32(ddlArchivo1.SelectedValue);
                int idArchivo2 = ddlArchivo2.SelectedValue.Equals("") ? 0 : Convert.ToInt32(ddlArchivo2.SelectedValue);
                int idVariable1 = ddlVariable1.SelectedValue.Equals("") ? 0 : Convert.ToInt32(ddlVariable1.SelectedValue);
                int idVariable2 = ddlVariable2.SelectedValue.Equals("") ? 0 : Convert.ToInt32(ddlVariable2.SelectedValue);
                List<ComparacionArchivo> lst = cPerfil.ComparacionArchivo(new ComparacionArchivo()
                {
                    IdArchivo1 = idArchivo1,
                    IdArchivo2 = idArchivo2,
                    IdCampoEvaluar1 = idVariable1,
                    IdCampoEvaluar2 = idVariable2,
                    Operador = ddlOperador.SelectedValue
                });
                if (lst.Count > 0)
                {
                    foreach (var item in lst)
                    {
                        item.IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                    }
                    cPerfil.RegistroOperacion(lst);
                }

                #region Linq
                //List<InfoArchivoSegmentacion> lst = cPerfil.ConsultarInfoArchivo(idArchivo1);

                //var archivo1 = lst.GroupBy(linea => linea.NumeroLinea, linea => linea, (IdLinea, ItemsLinea) => new
                //{
                //    RowId = IdLinea,
                //    Nombre = ItemsLinea.FirstOrDefault(o => o.Posicion == 3).ValorCampoArchivo,
                //    Cedula = ItemsLinea.FirstOrDefault(o => o.Posicion == 2).ValorCampoArchivo,
                //    CustomParam1 = ItemsLinea.FirstOrDefault(o => o.Posicion == 15).ValorCampoArchivo,
                //    CustomParam2 = ItemsLinea.FirstOrDefault(o => o.Posicion == 20).ValorCampoArchivo,
                //    CustomParam3 = ItemsLinea.FirstOrDefault(o => o.Posicion == 21).ValorCampoArchivo,
                //    CustomParam4 = ItemsLinea.FirstOrDefault(o => o.Posicion == 22).ValorCampoArchivo
                //}).ToList();

                //lst.Clear();

                //lst = cPerfil.ConsultarInfoArchivo(idArchivo2);

                //var archivo2 = lst.GroupBy(linea => linea.NumeroLinea, linea => linea, (IdLinea, ItemsLinea) => new
                //{
                //    RowId = IdLinea,
                //    Nombre = ItemsLinea.FirstOrDefault(o => o.Posicion == 3).ValorCampoArchivo,
                //    Cedula = ItemsLinea.FirstOrDefault(o => o.Posicion == 2).ValorCampoArchivo,
                //    CustomParam1 = ItemsLinea.FirstOrDefault(o => o.Posicion == 15).ValorCampoArchivo,
                //    CustomParam2 = ItemsLinea.FirstOrDefault(o => o.Posicion == 20).ValorCampoArchivo,
                //    CustomParam3 = ItemsLinea.FirstOrDefault(o => o.Posicion == 21).ValorCampoArchivo,
                //    CustomParam4 = ItemsLinea.FirstOrDefault(o => o.Posicion == 22).ValorCampoArchivo
                //}).ToList();

                //if (archivo1 != null && archivo2 != null)
                //{
                //    if (ddlOperador.Text == "<")
                //    {
                //        var result = (from a1 in archivo1
                //                      join a2 in archivo2 on a1.Cedula equals a2.Cedula
                //                      where Convert.ToDouble(a1.CustomParam1) < Convert.ToDouble(a2.CustomParam1)
                //                      select new
                //                      {
                //                          a1.Nombre,
                //                          a1.Cedula,
                //                          cust1 = a1.CustomParam1,
                //                          cust2 = a2.CustomParam1
                //                      }).ToList();
                //    }
                //omb.ShowMessage($"Se ha realizado el análisis de las señales comparativas exitósamente. Se encontraron {result.Count} coincidencias.", 2, "Atención");
                //}
                #endregion

                omb.ShowMessage($"Se ha realizado el análisis de las señales comparativas exitósamente. Se encontraron {lst.Count} coincidencias.", 2, "Atención");
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al analizar la señal de alerta. {ex.Message}", 1);
            }

        }

        protected void ibtnLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            ddlArchivo1.ClearSelection();
            ddlArchivo2.ClearSelection();
            ddlVariable1.ClearSelection();
            ddlVariable2.ClearSelection();
            ddlOperador.ClearSelection();
        }
    }
}