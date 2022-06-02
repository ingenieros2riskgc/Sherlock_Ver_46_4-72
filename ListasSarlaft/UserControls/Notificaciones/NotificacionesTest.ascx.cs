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
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Notificaciones
{
    public partial class NotificacionesTest : System.Web.UI.UserControl
    {
        string IdFormulario = "2002";
        cCuenta cCuenta = new cCuenta();

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
                        Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1", false);
                    else
                    {

                    }
                }
            }
            catch
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
        }
    }
}