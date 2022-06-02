using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Net.Mail;
using clsDatos;

namespace clsLogica
{
    public class clsUtilidades
    {
        /// <summary>
        /// Metodo que permite evaluar si una cadena es un numero.
        /// </summary>
        /// <param name="strNumero"></param>
        /// <returns></returns>
        public static bool mtdEsNumero(string strNumero)
        {
            bool booResult = false;
            Regex regex = new Regex(@"^[0-99]+$");
            //Regex regex = new Regex(@"^[0-9]([.][0-9]{1,3})?$");
            if (regex.IsMatch(strNumero))
                booResult = true;

            return booResult;
        }
        public static bool mtdEsNumeroNew(string strNumero)
        {
            bool booResult = false;
            //Regex regex = new Regex(@"^[0-9]+$");
            /*Regex regex = new Regex(@"^[0-9]([.][0-9]{1,3})?$");
            if (regex.IsMatch(strNumero))
                booResult = true;*/
            //string numString = "1287543"; //"1287543.0" will return false for a long
            long number1 = 0;
            bool canConvert = long.TryParse(strNumero, out number1);
            if (canConvert == true)
                booResult = true;
            return booResult;
        }
        #region Notificacion

        public void mtdGenerarNotificacion(int intIdEvento, string strTitulo, string strCodigo, string strDescripcion,
            int intIdUsuario, string strInfoAdicional, ref string strErrMsg)
        {
            string strTextoAdicional = string.Empty;

            try
            {
                strTextoAdicional = strTitulo + " < br>";
                strTextoAdicional = strTextoAdicional + "<br>";
                strTextoAdicional = strTextoAdicional + " Código : " + strCodigo + "<br>";
                strTextoAdicional = strTextoAdicional + " Nombre : " + strDescripcion + "<br>";

                if (!string.IsNullOrEmpty(strInfoAdicional))
                    strTextoAdicional = strTextoAdicional + " " + strInfoAdicional.Trim() + "<br>";

                mtdEnviarNotificacion(intIdEvento, 1, 0, intIdUsuario, System.DateTime.Now.ToString(), strTextoAdicional, ref strErrMsg);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al generar la notificación. [{0}]", ex.Message);
            }
        }
        public void mtdGenerarNotificacion(int intIdEvento, string strTitulo, string strCodigo, string strDescripcion,
            int intIdUsuario, string strInfoAdicional, ref string strErrMsg, int intIdNodoJerarquia)
        {
            string strTextoAdicional = string.Empty;

            try
            {
                strTextoAdicional = strTitulo + " < br>";
                strTextoAdicional = strTextoAdicional + "<br>";
                strTextoAdicional = strTextoAdicional + " Código : " + strCodigo + "<br>";
                strTextoAdicional = strTextoAdicional + " Nombre : " + strDescripcion + "<br>";

                if (!string.IsNullOrEmpty(strInfoAdicional))
                    strTextoAdicional = strTextoAdicional + " " + strInfoAdicional.Trim() + "<br>";

                mtdEnviarNotificacion(intIdEvento, 1, intIdNodoJerarquia, intIdUsuario, System.DateTime.Now.ToString(), strTextoAdicional, ref strErrMsg);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al generar la notificación. [{0}]", ex.Message);
            }
        }

        private void mtdEnviarNotificacion(int intIdEvento, int intIdRegistro, int intIdNodoJerarquia,
            int intIdUsuario, string strFechaFinal, string strTextoAdicional, ref string strErrMsg)
        {
            #region Vars
            string strCopia = string.Empty, strAsunto = string.Empty, strOtros = string.Empty, strCuerpo = string.Empty,
                strNroDiasRecordatorio = string.Empty, strAJefeInmediato = string.Empty, strAJefeMediato = string.Empty, strRequiereFechaCierre = string.Empty;
            string strDestinatario = string.Empty, strIdJefeInmediato = string.Empty, strIdJefeMediato = string.Empty;
            int intLastInsertIdCE = 0;
            clsDtUtilidades cUtil = new clsDtUtilidades();
            DataTable dtInfo = new DataTable();
            #endregion

            try
            {
                #region Consulta de informacion basica
                dtInfo = cUtil.mtdConsultarInfoBasica(intIdEvento, ref strErrMsg);

                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        strCopia = dtInfo.Rows[0]["Copia"].ToString().Trim();
                        strOtros = dtInfo.Rows[0]["Otros"].ToString().Trim();
                        strAsunto = dtInfo.Rows[0]["Asunto"].ToString().Trim();
                        strCuerpo = strTextoAdicional + dtInfo.Rows[0]["Cuerpo"].ToString().Trim();
                        strNroDiasRecordatorio = dtInfo.Rows[0]["NroDiasRecordatorio"].ToString().Trim();
                        strAJefeInmediato = dtInfo.Rows[0]["AJefeInmediato"].ToString().Trim();
                        strAJefeMediato = dtInfo.Rows[0]["AJefeMediato"].ToString().Trim();
                        strRequiereFechaCierre = dtInfo.Rows[0]["RequiereFechaCierre"].ToString().Trim();
                        strDestinatario = dtInfo.Rows[0]["Destinatario"].ToString().Trim();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strErrMsg))
                            strErrMsg = "No se puede enviar la notificación, no hay información básica.";
                    }
                }
                #endregion

                #region Consulta el correo del Destinatario

                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(intIdNodoJerarquia.ToString().Trim()))
                {
                    dtInfo = new DataTable();
                    dtInfo = cUtil.mtdConsultarInfoCorreo(intIdNodoJerarquia, ref strErrMsg);

                    if (dtInfo != null)
                        if (dtInfo.Rows.Count > 0)
                        {
                            strDestinatario = strDestinatario + "; " + dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                            strIdJefeInmediato = dtInfo.Rows[0]["idPadre"].ToString().Trim();
                        }
                }
                #endregion Consulta el correo del Destinatario

                #region Consulta el correo del Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (strAJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(strIdJefeInmediato.Trim()))
                    {
                        dtInfo = new DataTable();
                        dtInfo = cUtil.mtdConsultarInfoCorreo(Convert.ToInt32(strIdJefeInmediato), ref strErrMsg);

                        if (dtInfo != null)
                            if (dtInfo.Rows.Count > 0)
                            {
                                strDestinatario = strDestinatario + "; " + dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                                strIdJefeMediato = dtInfo.Rows[0]["idPadre"].ToString().Trim();
                            }
                    }
                }
                #endregion Consulta el correo del Jefe Inmediato

                #region Consulta el correo del Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (strAJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(strIdJefeMediato.Trim()))
                    {
                        dtInfo = new DataTable();
                        dtInfo = cUtil.mtdConsultarInfoCorreo(Convert.ToInt32(strIdJefeMediato), ref strErrMsg);

                        if (dtInfo != null)
                            if (dtInfo.Rows.Count > 0)
                                strDestinatario = strDestinatario + "; " + dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                    }
                }
                #endregion Consulta el correo del Jefe Mediato

                #region Insertar el Registro en la tabla de Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                intLastInsertIdCE = cUtil.mtdAgregarCorreoEnviado(intIdEvento, strDestinatario.Trim(), strCopia.Trim(), strOtros,
                    strAsunto, strCuerpo, "POR ENVIAR", intIdRegistro, string.Empty,
                    intIdUsuario, "CREACION", ref strErrMsg);
                #endregion Insertar el Registro en la tabla de Correos Enviados

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    #region Insertar correo recordatorio
                    /* Si no existe error en la creacion del registro en el log de correos enviados se procede
                     * a escribir en la tabla CorreosRecordatorios y a enviar el correo */
                    if (strRequiereFechaCierre == "SI" && strFechaFinal != "")
                    {
                        //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                        cUtil.mtdAgregarCorreoRecordatorio(intLastInsertIdCE, strNroDiasRecordatorio, "POR ENVIAR",
                            strFechaFinal, intIdUsuario, ref strErrMsg);
                    }
                    #endregion

                    #region Envio Correo
                    MailMessage objMessage = new MailMessage();
                    SmtpClient objSmtpClient = new SmtpClient();
                    MailAddress maFromAddress = new MailAddress(((System.Net.NetworkCredential)(objSmtpClient.Credentials)).UserName, "Software Sherlock");

                    objMessage.IsBodyHtml = true;//To determine email body is html or not
                    objMessage.From = maFromAddress;//here you can set address
                    objMessage.Subject = strAsunto;//subject of email
                    objMessage.Body = strCuerpo;

                    #region Correo Destinatario
                    foreach (string strCorreo in strDestinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(strCorreo.Trim()))
                            objMessage.To.Add(strCorreo);
                    }
                    #endregion

                    #region Correo Copia
                    if (!string.IsNullOrEmpty(strCopia.Trim()))
                        foreach (string strCorreo in strCopia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(strCorreo.Trim()))
                                objMessage.CC.Add(strCorreo);
                        }
                    #endregion

                    #region Correo Otros
                    if (!string.IsNullOrEmpty(strOtros.Trim()))
                        foreach (string strCorreo in strOtros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(strCorreo.Trim()))
                                objMessage.CC.Add(strCorreo);
                        }
                    #endregion

                    objSmtpClient.Send(objMessage);
                    #endregion Envio Correo

                    #region Actualiza el Estado del Correo Enviado
                    if (string.IsNullOrEmpty(strErrMsg))
                        cUtil.mtdActualizarCorreoEnviado(intLastInsertIdCE, "ENVIADO", ref strErrMsg);
                    #endregion
                }
            }
            catch (Exception exc)
            {
                // Handle the Exception.
                strErrMsg = "Error en el envío de la notificación." + "<br/>" + "Descripción: " + exc.Message.ToString();
            }
        }

        /*
         * Metodos para el Servicio
         */
        public void mtdGenerarNotificacion(string strSQLConn, string strOleConn, int intIdEvento, string strTitulo, string strCodigo,
            string strDescripcion, int intIdUsuario, string strInfoAdicional, ref string strErrMsg)
        {
            string strTextoAdicional = string.Empty;

            try
            {
                #region Texto a enviar
                strTextoAdicional = strTitulo + "<br>";
                strTextoAdicional = strTextoAdicional + "<br>";
                strTextoAdicional = strTextoAdicional + " Código : " + strCodigo + "<br>";
                strTextoAdicional = strTextoAdicional + " Nombre : " + strDescripcion + "<br>";

                if (!string.IsNullOrEmpty(strInfoAdicional))
                    strTextoAdicional = strTextoAdicional + " " + strInfoAdicional.Trim() + "<br>";
                #endregion

                mtdEnviarNotificacion(strSQLConn, strOleConn, intIdEvento, 1, 0, intIdUsuario, System.DateTime.Now.ToString(), strTextoAdicional, ref strErrMsg);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al generar la notificación. [{0}]", ex.Message);
            }
        }

        private void mtdEnviarNotificacion(string strSQLConn, string strOleConn, int intIdEvento, int intIdRegistro, int intIdNodoJerarquia,
            int intIdUsuario, string strFechaFinal, string strTextoAdicional, ref string strErrMsg)
        {
            #region Vars
            string strCopia = string.Empty, strAsunto = string.Empty, strOtros = string.Empty, strCuerpo = string.Empty,
                strNroDiasRecordatorio = string.Empty, strAJefeInmediato = string.Empty, strAJefeMediato = string.Empty,
                strRequiereFechaCierre = string.Empty, strDestinatario = string.Empty, strIdJefeInmediato = string.Empty,
                strIdJefeMediato = string.Empty;
            int intLastInsertIdCE = 0;
            clsDtUtilidades cUtil = new clsDtUtilidades();
            DataTable dtInfo = new DataTable();
            #endregion

            try
            {
                #region Consulta de informacion basica
                dtInfo = cUtil.mtdConsultarInfoBasica(strOleConn, intIdEvento, ref strErrMsg);

                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        strCopia = dtInfo.Rows[0]["Copia"].ToString().Trim();
                        strOtros = dtInfo.Rows[0]["Otros"].ToString().Trim();
                        strAsunto = dtInfo.Rows[0]["Asunto"].ToString().Trim();
                        strCuerpo = strTextoAdicional + dtInfo.Rows[0]["Cuerpo"].ToString().Trim();
                        strNroDiasRecordatorio = dtInfo.Rows[0]["NroDiasRecordatorio"].ToString().Trim();
                        strAJefeInmediato = dtInfo.Rows[0]["AJefeInmediato"].ToString().Trim();
                        strAJefeMediato = dtInfo.Rows[0]["AJefeMediato"].ToString().Trim();
                        strRequiereFechaCierre = dtInfo.Rows[0]["RequiereFechaCierre"].ToString().Trim();
                        strDestinatario = dtInfo.Rows[0]["Destinatario"].ToString().Trim();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strErrMsg))
                            strErrMsg = "No se puede enviar la notificación, no hay información básica.";
                    }
                }
                #endregion

                #region Consulta el correo del Destinatario
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(intIdNodoJerarquia.ToString().Trim()))
                {
                    dtInfo = new DataTable();
                    dtInfo = cUtil.mtdConsultarInfoCorreo(strOleConn, intIdNodoJerarquia, ref strErrMsg);

                    if (dtInfo != null)
                        if (dtInfo.Rows.Count > 0)
                        {
                            strDestinatario = strDestinatario + "; " + dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                            strIdJefeInmediato = dtInfo.Rows[0]["idPadre"].ToString().Trim();
                        }
                }
                #endregion Consulta el correo del Destinatario

                #region Consulta el correo del Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (strAJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(strIdJefeInmediato.Trim()))
                    {
                        dtInfo = new DataTable();
                        dtInfo = cUtil.mtdConsultarInfoCorreo(strOleConn, Convert.ToInt32(strIdJefeInmediato), ref strErrMsg);

                        if (dtInfo != null)
                            if (dtInfo.Rows.Count > 0)
                            {
                                strDestinatario = strDestinatario + "; " + dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                                strIdJefeMediato = dtInfo.Rows[0]["idPadre"].ToString().Trim();
                            }
                    }
                }
                #endregion Consulta el correo del Jefe Inmediato

                #region Consulta el correo del Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (strAJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(strIdJefeMediato.Trim()))
                    {
                        dtInfo = new DataTable();
                        dtInfo = cUtil.mtdConsultarInfoCorreo(strOleConn, Convert.ToInt32(strIdJefeMediato), ref strErrMsg);

                        if (dtInfo != null)
                            if (dtInfo.Rows.Count > 0)
                                strDestinatario = strDestinatario + "; " + dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                    }
                }
                #endregion Consulta el correo del Jefe Mediato

                #region Insertar el Registro en la tabla de Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                intLastInsertIdCE = cUtil.mtdAgregarCorreoEnviado(strSQLConn, intIdEvento, strDestinatario.Trim(),
                    strCopia.Trim(), strOtros, strAsunto, strCuerpo, "POR ENVIAR", intIdRegistro, string.Empty,
                    intIdUsuario, "CREACION", ref strErrMsg);
                #endregion Insertar el Registro en la tabla de Correos Enviados

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    #region Insertar correo recordatorio
                    /* Si no existe error en la creacion del registro en el log de correos enviados se procede
                     * a escribir en la tabla CorreosRecordatorios y a enviar el correo */
                    if (strRequiereFechaCierre == "SI" && strFechaFinal != "")
                    {
                        //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                        cUtil.mtdAgregarCorreoRecordatorio(strOleConn, intLastInsertIdCE, strNroDiasRecordatorio,
                            "POR ENVIAR", strFechaFinal, intIdUsuario, ref strErrMsg);
                    }

                    #endregion

                    #region Envio Correo
                    MailMessage objMessage = new MailMessage();
                    SmtpClient objSmtpClient = new SmtpClient();
                    MailAddress maFromAddress = new MailAddress(((System.Net.NetworkCredential)(objSmtpClient.Credentials)).UserName, "Software Sherlock");

                    objMessage.IsBodyHtml = true;//To determine email body is html or not
                    objMessage.From = maFromAddress;//here you can set address
                    objMessage.Subject = strAsunto;//subject of email
                    objMessage.Body = strCuerpo;

                    #region Correo Destinatario
                    foreach (string strCorreo in strDestinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(strCorreo.Trim()))
                            objMessage.To.Add(strCorreo);
                    }
                    #endregion

                    #region Correo Copia
                    if (!string.IsNullOrEmpty(strCopia.Trim()))
                        foreach (string strCorreo in strCopia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(strCorreo.Trim()))
                                objMessage.CC.Add(strCorreo);
                        }
                    #endregion

                    #region Correo Otros
                    if (!string.IsNullOrEmpty(strOtros.Trim()))
                        foreach (string strCorreo in strOtros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(strCorreo.Trim()))
                                objMessage.CC.Add(strCorreo);
                        }
                    #endregion

                    objSmtpClient.Send(objMessage);
                    #endregion Envio Correo

                    #region Actualiza el Estado del Correo Enviado
                    if (string.IsNullOrEmpty(strErrMsg))
                        cUtil.mtdActualizarCorreoEnviado(strOleConn, intLastInsertIdCE, "ENVIADO", ref strErrMsg);
                    #endregion
                }
            }
            catch (Exception exc)
            {
                // Handle the Exception.
                strErrMsg = "Error en el envío de la notificación." + "<br/>" + "Descripción: " + exc.Message;
            }
        }
        #endregion Notificacion
    }
}
