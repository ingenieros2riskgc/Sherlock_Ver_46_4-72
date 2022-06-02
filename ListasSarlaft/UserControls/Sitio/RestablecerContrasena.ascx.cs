using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Sitio
{
    public partial class RestablecerContrasena : System.Web.UI.UserControl
    {
        private cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string strMensaje = string.Empty;
            string strUsuario = Sanitizer.GetSafeHtmlFragment(tbxUsername.Text.Trim()), strNroIdendificacion = Sanitizer.GetSafeHtmlFragment(tbxNroId.Text.Trim());
            DataTable dtInformacion = new DataTable();

            if (cCuenta.mtdConsultarUsuario(strUsuario, strNroIdendificacion, ref dtInformacion, ref strMensaje))
            {
                string strNewPass = mtdCrearRandomPassword(10) + "*";
                if (cCuenta.ReestablecerContrasena(strNewPass, dtInformacion.Rows[0]["IdUsuario"].ToString().Trim()))
                {
                    if (mtdEnviarCorreoContraseña(strUsuario, dtInformacion.Rows[0]["NombreResponsable"].ToString().Trim(),
                        dtInformacion.Rows[0]["CorreoResponsable"].ToString().Trim(), strNewPass, ref  strMensaje))
                    {
                        Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
                        strMensaje = "El correo con la contraseña reestablecida ha sido enviado.";
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(strMensaje))
                    strMensaje = string.Format("No hay información para el usuario. [{0}]", tbxUsername.Text.Trim());
            }
            Mensaje(strMensaje);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private bool mtdEnviarCorreoContraseña(string strUsuario, string strNombre, string strCorreo, string strContrasena, ref string strErrMsg)
        {
            string strCuerpo = string.Empty;
            bool booResult = true;

            try
            {
                if (string.IsNullOrEmpty(strCorreo))
                {
                    strErrMsg = string.Format("No existe Correo Electronico para el usuario {0}.", strUsuario);
                    booResult = false;
                }

                if (booResult)
                {
                    #region Envio Correo
                    MailMessage objMessage = new MailMessage();
                    SmtpClient objSmtpClient = new SmtpClient();
                    MailAddress maFromAddress = new MailAddress(((System.Net.NetworkCredential)(objSmtpClient.Credentials)).UserName, "Software Sherlock");

                    strCuerpo = string.Format("<DIV>La contraseña para el usuario {0} [{1}] ha sido reestablecida, la nueva contraseña es: {2}<BR></DIV> " +
                        "<DIV> Este correo es enviado autómaticamente por Sherlock.<BR></DIV> " +
                        "<DIV><BR></DIV><DIV>Por favor no responderlo.</DIV><DIV><BR></DIV><DIV>Gracias.</DIV>",
                        strUsuario, strNombre, strContrasena);

                    objMessage.IsBodyHtml = true;//To determine email body is html or not
                    objMessage.From = maFromAddress;//here you can set address
                    objMessage.Subject = "SHERLOCK: CORREO PARA REESTABLECER LA CONTRASEÑA";//subject of email
                    objMessage.Body = strCuerpo;

                    objMessage.To.Add(strCorreo);

                    objSmtpClient.Send(objMessage);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error en el envio del correo electrónico. [{0}].", ex.Message);
                booResult = false;
            }

            return booResult;
        }

        private string mtdCrearRandomPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                Random randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }
    }
}