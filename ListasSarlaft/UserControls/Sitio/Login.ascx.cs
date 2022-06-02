using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls
{
    public partial class Login : System.Web.UI.UserControl
    {
        private cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserName.Focus();
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            #region Variables
            string strMensaje = string.Empty;
            inicializarValores();
            DataTable dtInformacion = new DataTable();
            int intDiasCaducaContrasena = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DiasCaducaContrasena"].ToString().Trim());
            #endregion

            try
            {
                dtInformacion = cCuenta.autenticarUsuario(Sanitizer.GetSafeHtmlFragment(UserName.Text.ToLower().Trim()), Password.Text.Trim());

                if (dtInformacion.Rows.Count > 0)
                {
                    #region Hay informacion
                    if (!cCuenta.mtdValidarCaducaContrasena(dtInformacion.Rows[0]["FechaUltActualPassword"], intDiasCaducaContrasena))
                    {
                        #region Cambio Contraseña
                        Session["Usuario"] = dtInformacion.Rows[0]["Usuario"].ToString().Trim();
                        Session["NombreUsuario"] = dtInformacion.Rows[0]["NombreUsuario"].ToString().Trim();
                        Session["IdMacroProcesoUsuario"] = dtInformacion.Rows[0]["IdMacroProcesoU"].ToString().Trim();
                        Session["IdProcesoUsuario"] = dtInformacion.Rows[0]["IdProcesoU"].ToString().Trim();
                        Session["VerTodosProcesos"] = dtInformacion.Rows[0]["VerTodosProcesos"].ToString().Trim();
                        Session["IdUsuario"] = dtInformacion.Rows[0]["IdUsuario"].ToString().Trim();

                        cCuenta.mtdAuthCambioContrasena(dtInformacion.Rows[0]["NombreRol"].ToString().Trim(),
                            Convert.ToInt32(dtInformacion.Rows[0]["IdUsuario"]), dtInformacion.Rows[0]["NombreUsuario"].ToString().Trim(),
                            dtInformacion.Rows[0]["Usuario"].ToString().Trim(), dtInformacion.Rows[0]["IdRol"].ToString().Trim(),
                            dtInformacion.Rows[0]["IdJerarquia"].ToString().Trim());

                        Response.Redirect("~/Formularios/AdminUsers/CambioContrasena.aspx", false);
                        #endregion Cambio Contraseña
                    }
                    else
                    {
                        if (dtInformacion.Columns.Contains("Usuario") && dtInformacion.Columns.Contains("NombreUsuario"))
                        {
                            Session["Usuario"] = dtInformacion.Rows[0]["Usuario"].ToString().Trim();
                            Session["NombreUsuario"] = dtInformacion.Rows[0]["NombreUsuario"].ToString().Trim();
                            Session["IdMacroProcesoUsuario"] = dtInformacion.Rows[0]["IdMacroProcesoU"].ToString().Trim();
                            Session["IdProcesoUsuario"] = dtInformacion.Rows[0]["IdProcesoU"].ToString().Trim();
                            Session["VerTodosProcesos"] = dtInformacion.Rows[0]["VerTodosProcesos"].ToString().Trim();
                            Session["IdJerarquia"] = dtInformacion.Rows[0]["IdJerarquia"].ToString().Trim();
                        }
                        Session["IdUsuario"] = dtInformacion.Rows[0]["IdUsuario"].ToString().Trim();

                        cCuenta.isAuthenticated(dtInformacion.Rows[0]["NombreRol"].ToString().Trim(), Convert.ToInt64(dtInformacion.Rows[0]["IdUsuario"]),
                            dtInformacion.Rows[0]["NombreUsuario"].ToString().Trim(), dtInformacion.Rows[0]["Usuario"].ToString().Trim(),
                            dtInformacion.Rows[0]["IdRol"].ToString().Trim(), dtInformacion.Rows[0]["IdJerarquia"].ToString().Trim());

                        if (Password.Text.Trim() == "Sherlock+") //Vieja contraseña sherlock2012
                        {
                            Response.Redirect("~/Formularios/AdminUsers/CambioContrasena.aspx", false);
                        }
                        else
                        {
                            cCuenta.Login_SN(dtInformacion.Rows[0]["IdUsuario"].ToString().Trim(), 1);
                            Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx", false);
                        }
                    }
                    #endregion Hay informacion
                }
                else
                {
                    dtInformacion = cCuenta.mtdUsuarioBloqueado(Sanitizer.GetSafeHtmlFragment(UserName.Text.ToLower().Trim()), Sanitizer.GetSafeHtmlFragment(Password.Text.Trim()));
                    if (dtInformacion.Rows.Count > 0)
                    {
                        if (dtInformacion.Rows[0]["Bloqueado"].ToString() == "True")
                        {
                            Label1.Visible = true;
                            Label1.Text = string.Empty;
                            Label1.Text = "El usuario |" + Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()) + "| está bloqueado";
                        }
                        else
                        {
                            #region NO Hay informacion
                            Label1.Visible = true;
                            Label1.Text = string.Empty;
                            dtInformacion = cCuenta.MensajeLogin(Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()));
                            if (dtInformacion.Rows.Count == 0)
                            {
                                Label1.Text = "El usuario |" + Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()) + "| no está registrado en la aplicación Sherlock";
                            }
                            else
                            {
                                if (dtInformacion.Rows[0]["FechaUltActualPassword"] == DBNull.Value)
                                {
                                    Session["Usuario"] = dtInformacion.Rows[0]["Usuario"].ToString().Trim();
                                    Session["NombreUsuario"] = dtInformacion.Rows[0]["NombreUsuario"].ToString().Trim();
                                    Session["IdMacroProcesoUsuario"] = dtInformacion.Rows[0]["IdMacroProcesoU"].ToString().Trim();
                                    Session["IdProcesoUsuario"] = dtInformacion.Rows[0]["IdProcesoU"].ToString().Trim();
                                    Session["VerTodosProcesos"] = dtInformacion.Rows[0]["VerTodosProcesos"].ToString().Trim();
                                    Session["IdUsuario"] = dtInformacion.Rows[0]["IdUsuario"].ToString().Trim();

                                    cCuenta.isAuthenticated(dtInformacion.Rows[0]["NombreRol"].ToString().Trim(), Convert.ToInt64(dtInformacion.Rows[0]["IdUsuario"]),
                                        dtInformacion.Rows[0]["NombreUsuario"].ToString().Trim(), dtInformacion.Rows[0]["Usuario"].ToString().Trim(),
                                        dtInformacion.Rows[0]["IdRol"].ToString().Trim(), dtInformacion.Rows[0]["IdJerarquia"].ToString().Trim());

                                    Response.Redirect("~/Formularios/AdminUsers/CambioContrasena.aspx", false);
                                }
                                else if (!cCuenta.mtdCompararContrasenasEncriptadas(Password.Text.Trim(), dtInformacion.Rows[0]["Contrasena"].ToString().Trim()))
                                {
                                    Label1.Text = "El usuario |" + Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()) + "| no tiene acceso a Sherlock";
                                }
                                else if (dtInformacion.Rows[0]["Bloqueado"].ToString().Trim() == "True")
                                {
                                    Label1.Text = "El usuario |" + Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()) + "| está bloqueado";
                                }
                                else if (dtInformacion.Rows[0]["login"].ToString().Trim() == "True")
                                {
                                    Label1.Text = "El usuario |" + Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()) + "| ya tiene una sesión abierta";
                                }
                                else if (!cCuenta.mtdValidarContrasena(Sanitizer.GetSafeHtmlFragment(Password.Text.Trim()), ref strMensaje))
                                {
                                    Label1.Text = strMensaje;
                                }
                            }
                            #endregion Hay informacion
                        }
                    }
                    else
                    {
                        #region NO Hay informacion
                        Label1.Visible = true;
                        Label1.Text = string.Empty;
                        dtInformacion = cCuenta.MensajeLogin(Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()));
                        if (dtInformacion.Rows.Count == 0)
                        {
                            Label1.Text = "El usuario |" + Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()) + "| no está registrado en la aplicación Sherlock";
                        }
                        else
                        {
                            if (dtInformacion.Rows[0]["FechaUltActualPassword"] == DBNull.Value)
                            {
                                Session["Usuario"] = dtInformacion.Rows[0]["Usuario"].ToString().Trim();
                                Session["NombreUsuario"] = dtInformacion.Rows[0]["NombreUsuario"].ToString().Trim();
                                Session["IdMacroProcesoUsuario"] = dtInformacion.Rows[0]["IdMacroProcesoU"].ToString().Trim();
                                Session["IdProcesoUsuario"] = dtInformacion.Rows[0]["IdProcesoU"].ToString().Trim();
                                Session["VerTodosProcesos"] = dtInformacion.Rows[0]["VerTodosProcesos"].ToString().Trim();
                                Session["IdUsuario"] = dtInformacion.Rows[0]["IdUsuario"].ToString().Trim();

                                cCuenta.isAuthenticated(dtInformacion.Rows[0]["NombreRol"].ToString().Trim(), Convert.ToInt64(dtInformacion.Rows[0]["IdUsuario"]),
                                    dtInformacion.Rows[0]["NombreUsuario"].ToString().Trim(), dtInformacion.Rows[0]["Usuario"].ToString().Trim(),
                                    dtInformacion.Rows[0]["IdRol"].ToString().Trim(), dtInformacion.Rows[0]["IdJerarquia"].ToString().Trim());

                                Response.Redirect("~/Formularios/AdminUsers/CambioContrasena.aspx", false);
                            }
                            else if (!cCuenta.mtdCompararContrasenasEncriptadas(Password.Text.Trim(), dtInformacion.Rows[0]["Contrasena"].ToString().Trim()))
                            {
                                string a = dtInformacion.Rows[0]["Contrasena"].ToString().Trim();
                                //Label1.Text = "El usuario |" + Login1.UserName + "| no tiene acceso a Sherlock";
                                Label1.Text = "La contraseña no corresponde";
                            }
                            else if (dtInformacion.Rows[0]["Bloqueado"].ToString().Trim() == "True")
                            {
                                Label1.Text = "El usuario |" + Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()) + "| está bloqueado";
                            }
                            else if (dtInformacion.Rows[0]["login"].ToString().Trim() == "True")
                            {
                                Label1.Text = "El usuario |" + Sanitizer.GetSafeHtmlFragment(UserName.Text.Trim()) + "| ya tiene una sesión abierta";
                            }
                            else if (!cCuenta.mtdValidarContrasena(Sanitizer.GetSafeHtmlFragment(Password.Text.Trim()), ref strMensaje))
                            {
                                Label1.Text = strMensaje;
                            }
                        }
                        #endregion Hay informacion
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = "Error de conexión. Revise el tag connectionStrings o contacte al administrador." + ex.Message;
            }
        }

        private void inicializarValores()
        {
            cCuenta.notAuthenticated();
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }



    }
}