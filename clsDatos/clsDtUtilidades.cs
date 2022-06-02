using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace clsDatos
{
    public class clsDtUtilidades
    {
        public DataTable mtdConsultarInfoBasica(int intIdEvento, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre, CD.[Destinatario] " +
                        "FROM [Notificaciones].[CorreosDestinatarios] CD INNER JOIN [Notificaciones].[Evento] E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = {0}", intIdEvento);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la información básica de la notificación. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarInfoCorreo(int intId, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = {0}", intId);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la información de correo de la notificación. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public int mtdAgregarCorreoEnviado(int intIdEvento, string strDestinatario, string strCopia, string strOtros,
            string strAsunto, string strCuerpo, string strEstado, int intIdRegistro, string strFechaEnvio,
            int intIdUsuario, string strTipo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            int intRetorno = 0;
            clsDatabase cDataBase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                if (string.IsNullOrEmpty(strFechaEnvio.Trim()))
                    strConsulta = string.Format("INSERT INTO [Notificaciones].[CorreosEnviados] " +
                        "([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaRegistro], [IdUsuario], [Tipo]) " +
                        "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, GETDATE(), {8}, '{9}') SELECT @@IDENTITY Ultimo",
                        intIdEvento, strDestinatario, strCopia, strOtros, strAsunto, strCuerpo, strEstado, intIdRegistro,
                        intIdUsuario, strTipo);
                else
                    strConsulta = string.Format("INSERT INTO [Notificaciones].[CorreosEnviados] " +
                       "([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario], [Tipo]) " +
                       "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', GETDATE(), {9}, '{10}') SELECT @@IDENTITY Ultimo",
                       intIdEvento, strDestinatario, strCopia, strOtros, strAsunto, strCuerpo, strEstado, intIdRegistro,
                       strFechaEnvio, intIdUsuario, strTipo);

                cDataBase.mtdConectarSql();
                intRetorno = cDataBase.mtdEjecutarConsultaSQLRetorno(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al agregar el registro de correos enviados. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.mtdDesconectarSql();
            }

            return intRetorno;
        }

        public void mtdAgregarCorreoRecordatorio(int intLastInsertIdCE, string strNroDiasRecordatorio,
            string strEstado, string strFechaFinal, int intIdUsuario, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDataBase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Notificaciones].[CorreosRecordatorio] " +
                   "([IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario]) " +
                   "VALUES ({0}, {1}, CONVERT(datetime, '{2}', 120), '{3}', GETDATE(), @IdUsuario)",
                   intLastInsertIdCE, strNroDiasRecordatorio, strFechaFinal, strEstado, intIdUsuario);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al agregar el registro de correos recordatorio. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }
        }

        public void mtdActualizarCorreoEnviado(int intLastInsertIdCE, string strEstado,
            ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDataBase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Notificaciones].[CorreosEnviados] SET [FechaEnvio] = GETDATE(), [Estado] = '{0}' " +
                     "WHERE [IdCorreosEnviados] = {1}", strEstado, intLastInsertIdCE);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el registro de correos enviados. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }
        }

        /*
         * Metodos para el Servicio
         */
        public DataTable mtdConsultarInfoBasica(string strOleConn, int intIdEvento, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre, CD.[Destinatario] " +
                        "FROM [Notificaciones].[CorreosDestinatarios] CD INNER JOIN [Notificaciones].[Evento] E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = {0}", intIdEvento);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la información básica de la notificación. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarInfoCorreo(string strOleConn, int intId, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = {0}", intId);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la información de correo de la notificación. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public int mtdAgregarCorreoEnviado(string strSQLConn, int intIdEvento, string strDestinatario, string strCopia,
            string strOtros, string strAsunto, string strCuerpo, string strEstado, int intIdRegistro, string strFechaEnvio,
            int intIdUsuario, string strTipo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            int intRetorno = 0;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDataBase = new clsDatabase(strSQLConn, 1);
            #endregion Vars

            try
            {
                if (string.IsNullOrEmpty(strFechaEnvio.Trim()))
                    strConsulta = string.Format("INSERT INTO [Notificaciones].[CorreosEnviados] " +
                        "([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaRegistro], [IdUsuario], [Tipo]) " +
                        "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, GETDATE(), {8}, '{9}') SELECT @@IDENTITY Ultimo",
                        intIdEvento, strDestinatario, strCopia, strOtros, strAsunto, strCuerpo, strEstado, intIdRegistro,
                        intIdUsuario, strTipo);
                else
                    strConsulta = string.Format("INSERT INTO [Notificaciones].[CorreosEnviados] " +
                       "([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario], [Tipo]) " +
                       "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', GETDATE(), {9}, '{10}') SELECT @@IDENTITY Ultimo",
                       intIdEvento, strDestinatario, strCopia, strOtros, strAsunto, strCuerpo, strEstado, intIdRegistro,
                       strFechaEnvio, intIdUsuario, strTipo);

                cDataBase.mtdConectarSql();
                intRetorno = cDataBase.mtdEjecutarConsultaSQLRetorno(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al agregar el registro de correos enviados. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.mtdDesconectarSql();
            }

            return intRetorno;
        }

        public void mtdAgregarCorreoRecordatorio(string strOleConn, int intLastInsertIdCE, string strNroDiasRecordatorio,
            string strEstado, string strFechaFinal, int intIdUsuario, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDataBase = new clsDatabase(strOleConn, 2);
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Notificaciones].[CorreosRecordatorio] " +
                    "([IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario]) " +
                    "VALUES ({0}, {1}, CONVERT(datetime, '{2}', 120), '{3}', GETDATE(), @IdUsuario)",
                    intLastInsertIdCE, strNroDiasRecordatorio, strFechaFinal, strEstado, intIdUsuario);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al agregar el registro de correos recordatorio. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }
        }

        public void mtdActualizarCorreoEnviado(string strOleConn, int intLastInsertIdCE, string strEstado,
            ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDataBase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Notificaciones].[CorreosEnviados] SET [FechaEnvio] = GETDATE(), [Estado] = '{0}' " +
                    "WHERE [IdCorreosEnviados] = {1}", strEstado, intLastInsertIdCE);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el registro de correos enviados. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }
        }
    }
}
