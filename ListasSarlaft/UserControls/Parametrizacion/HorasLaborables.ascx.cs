using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Parametrizacion
{
    public partial class HorasLaborables : System.Web.UI.UserControl
    {
        string IdFormulario = "3001";
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    string selectCommand = "";
                    string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
                    selectCommand = "SELECT [HorasDiarias] FROM [Parametrizacion].[HorasLaborables] WHERE [Id] = 1";

                    SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
                    DataTable dtblDiscuss = new DataTable();
                    dad.Fill(dtblDiscuss);

                    DataView view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        txtHL.Text = row["HorasDiarias"].ToString().Trim();
                    }
                }
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
                try
                {
                    if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(txtHL.Text)))
                    {
                        if (int.Parse(Sanitizer.GetSafeHtmlFragment(txtHL.Text)) > 24 || int.Parse(Sanitizer.GetSafeHtmlFragment(txtHL.Text)) < 0)
                            omb.ShowMessage("El número de horas no puede ser mayor de 24 horas ni menor de 1 hora.", 2, "Atención");
                        else
                        {
                            SqlDataSource1.UpdateParameters["Id"].DefaultValue = "1";
                            SqlDataSource1.UpdateParameters["HorasDiarias"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtHL.Text);
                            SqlDataSource1.Update();
                            omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        }
                    }
                    else
                        omb.ShowMessage("Este campo no puede ir vacío. Por favor diligenciar.", 2, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtHL.Text)))
            {
                err = false;
                omb.ShowMessage("Debe ingresar el Número de Horas Laborables.", 2, "Atención");
                txtHL.Focus();
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