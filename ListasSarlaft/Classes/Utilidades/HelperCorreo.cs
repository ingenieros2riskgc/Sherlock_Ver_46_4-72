using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ListasSarlaft.Classes.Utilidades
{
    public static class HelperCorreo
    {

        public static void EnviarCorreo(string destinario, string cuerpo, string asunto)
        {
            cError error = new cError();
            try
            {
                MailMessage objMessage = new MailMessage();
                SmtpClient objSmtpClient = new SmtpClient();
                MailAddress maFromAddress = new MailAddress(((System.Net.NetworkCredential)(objSmtpClient.Credentials)).UserName, "Software Sherlock");
                objMessage.IsBodyHtml = true;
                objMessage.From = maFromAddress;
                objMessage.Subject = asunto;
                objMessage.Body = $"Buen día, <br /><br />{cuerpo}<br /><br /><br />***Nota: Este correo es enviado autómaticamente por Sherlock.";
                objMessage.To.Add(destinario);
                objSmtpClient.Send(objMessage);
            }
            catch (Exception ex)
            {
                error.errorMessage($"Error en el envío del correo. {ex.Message}");
                throw ex;
            }
        }
    }
}