using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.AdminUsers
{
    public partial class CambioContrasena : System.Web.UI.UserControl
    {
        private cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            bool booActivate = false;

            if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    string strContrasenaBD = cCuenta.contrasenaUsuario();

                    if (cCuenta.mtdCompararContrasenasEncriptadas("Sherlock+", strContrasenaBD)) //Vieja contraseña sherlock2012
                    {
                        Mensaje("Debe cambiar su contraseña");
                        booActivate = true;
                    }
                    else
                    {
                        int intDiasCaducaContrasena = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DiasCaducaContrasena"].ToString().Trim());
                        object objFecha = cCuenta.mtdFechaCaducidadContrasena();

                        if (!cCuenta.mtdValidarCaducaContrasena(objFecha, intDiasCaducaContrasena))
                        {
                            Mensaje("Se debe cambiar la contraseña porque ha caducado");
                            booActivate = true;
                        }
                    }

                    if (booActivate)
                        cCuenta.mtdBloquearUsuario(1, Session["IdUsuario"].ToString());
                }
            }
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            string strMensaje = string.Empty;

            if (cCuenta.mtdCompararContrasenasEncriptadas(CurrentPassword.Text.Trim(), cCuenta.contrasenaUsuario()))
            {
                if (!cCuenta.mtdValidarContrasena(ConfirmNewPassword.Text.Trim(), ref strMensaje))
                {
                    Mensaje(strMensaje);
                }
                else if (!cCuenta.mtdCompararContrasenasEncriptadas(ConfirmNewPassword.Text.Trim(), cCuenta.contrasenaUsuario()))
                {
                    cCuenta.mtdActualizarLoginSN(1);
                    cCuenta.actualizarContrasena(ConfirmNewPassword.Text.Trim());
                    cCuenta.mtdBloquearUsuario(0, Session["IdUsuario"].ToString());
                    Mensaje("Contraseña cambiada con éxito.");
                }
                else
                {
                    Mensaje("Cambio de contraseña sin éxito. Tu nueva contraseña no puede ser igual a tu antigua contraseña.");
                    cCuenta.mtdBloquearUsuario(1, Session["IdUsuario"].ToString());
                }
            }
            else
            {
                Mensaje("Cambio de contraseña sin éxito. Has escrito mal tu antigua contraseña.");
                cCuenta.mtdBloquearUsuario(1, Session["IdUsuario"].ToString());
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
    }
}