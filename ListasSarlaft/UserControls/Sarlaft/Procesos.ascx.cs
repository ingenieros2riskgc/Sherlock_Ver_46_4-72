using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.UserControls
{
    public partial class Procesos : System.Web.UI.UserControl
    {
        private cProcesos cProcesos = new cProcesos();
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "40";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if(!Page.IsPostBack)
            {
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cProcesos.SPCargarInformacion();
                    Mensaje("Proceso terminado con éxito.");
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar el proceso. " + ex.Message);
            }            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cProcesos.SPSegmentacion();
                    Mensaje("Proceso terminado con éxito.");
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar el proceso. " + ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cProcesos.SPAnalisisPEPS();
                    Mensaje("Proceso terminado con éxito.");
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar el proceso. " + ex.Message);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosBorrar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cProcesos.SPReiniciarSistema();
                    Mensaje("Proceso terminado con éxito.");
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar el proceso. " + ex.Message);
            }
        }
    }
}