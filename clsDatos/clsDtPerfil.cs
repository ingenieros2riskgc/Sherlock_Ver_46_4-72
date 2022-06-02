using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using clsDTO;

namespace clsDatos
{
    public class clsDtPerfil
    {
        /// <summary>
        /// Metodo para consultar todo los perfiles que hay en BD
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdConsultaPerfiles(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdUsuario], [IdPerfil], [Nombre] NombrePerfil, [ValorMinimo], [ValorMaximo] FROM [Perfiles].[tblPerfil]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaPerfilesMod(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.IdPerfil, a.Nombre NombrePerfil, a.ValorMinimo, a.ValorMaximo,a.IdUsuario,a.FechaModificacion, " +
                    "rtrim(b.Usuario)Usuario FROM Perfiles.tblPerfil_logAuditoria a LEFT JOIN Listas.Usuarios b on a.IdUsuario=b.IdUsuario " +
                    "ORDER BY a.IdPerfil,a.FechaModificacion DESC");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable ConsulPerfiles(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.Nombre NombrePerfil, a.ValorMinimo, a.ValorMaximo,a.FechaModificacion, " +
                    "rtrim(b.Usuario)Usuario FROM Perfiles.tblPerfil_logAuditoria a " +
                    "LEFT JOIN Listas.Usuarios b on a.IdUsuario=b.IdUsuario ORDER BY a.IdPerfil,a.FechaModificacion DESC");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Metodo para insertar perfiles a la BD
        /// </summary>
        /// <param name="objPerfil"></param>
        /// <param name="strErrMsg"></param>
        /// 

        public void mtdAgregarPerfilCreate(clsDTOPerfilCreate objPerfil, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblPerfil] ([IdUsuario],[Nombre], [ValorMinimo], [ValorMaximo]) " +
                    "VALUES ({0}, '{1}', {2}, {3})",
                    objPerfil.StrIdUsuario, objPerfil.StrNombrePerfil, Convert.ToInt32(objPerfil.StrValorMinimo), Convert.ToInt32(objPerfil.StrValorMaximo));

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la parametrización. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdAgregarPerfil(clsDTOPerfil objPerfil, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblPerfil] ([IdUsuario],[Nombre], [ValorMinimo], [ValorMaximo]) " +
                    "VALUES ({0}, '{1}', {2}, {3})",
                    objPerfil.StrIdUsuario, objPerfil.StrNombrePerfil, Convert.ToInt32(objPerfil.StrValorMinimo), Convert.ToInt32(objPerfil.StrValorMaximo));

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la parametrización. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        /// <summary>
        /// Metodo para actualizar perfiles en la BD
        /// </summary>
        /// <param name="objPerfil"></param>
        /// <param name="strErrMsg"></param>
        public void mtdActualizarPerfil(clsDTOPerfil objPerfil, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblPerfil] " +
                    "SET [IdUsuario] = {0}, [Nombre] = '{1}', [ValorMinimo] = {2}, [ValorMaximo] = {3} WHERE IdPerfil = {4}",
                    objPerfil.StrIdUsuario, objPerfil.StrNombrePerfil, objPerfil.StrValorMinimo, objPerfil.StrValorMaximo, objPerfil.StrIdPerfil);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la parametrización. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        /// <summary>
        /// Metodo para agregar los historicos de los perfiles evaluados
        /// </summary>
        /// <param name="objHistPerfil"></param>
        /// <param name="strErrMsg"></param>
        public void mtdAgregarHistPerfil(clsDTOHistoricoCalculoPerfil objHistPerfil, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblHistoricoCalculoPerfil] " +
                    "([IdPerfil], [IdArchivo], [TipoDocumento], [NumeroDocumento], [NombreCliente], [CalificacionPerfil], [Producto], [FechaGeneracion], [SenalesAlerta], [Linea]) " +
                    "VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', GETDATE(), '{7}', '{8}')",
                    Convert.ToInt32(objHistPerfil.StrIdPerfil), Convert.ToInt32(objHistPerfil.StrIdArchivo), objHistPerfil.StrTipoDocCliente
                    , objHistPerfil.StrNroDocCliente, objHistPerfil.StrNombreCliente,
                    objHistPerfil.StrCalificacionPerfil, objHistPerfil.StrCodProducto, objHistPerfil.StrSenalesAlerta, objHistPerfil.StrLinea);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la parametrización. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public DataTable mtdConsultaHistorico(clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PHCP.IdHistorico, PHCP.IdPerfil, Temp.TipoDocumento, Temp.NumeroDocumento, Temp.NombreCliente, Temp.Calificacion, PHCP.Producto, PHCP.InfoInspektor, PHCP.SenalesAlerta, PHCP.FechaGeneracion, PHCP.Linea " +
                    "FROM [Perfiles].[tblHistoricoCalculoPerfil] PHCP " +
                    "INNER JOIN (SELECT MAX(CAST(REPLACE(CalificacionPerfil,',','.') AS DECIMAL(18,2))) Calificacion, TipoDocumento, NumeroDocumento, NombreCliente " +
                    "FROM [Perfiles].[tblHistoricoCalculoPerfil] WHERE IdArchivo = {0} " +
                    "GROUP BY NumeroDocumento, NombreCliente, TipoDocumento) Temp ON CAST(REPLACE(PHCP.CalificacionPerfil,',','.') AS DECIMAL(18,2)) = Temp.Calificacion " +
                    "AND Temp.NumeroDocumento = PHCP.NumeroDocumento " +
                    "WHERE PHCP. IdArchivo = {0} ", objArchivo.StrIdArchivo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los históricos de perfiles. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataSet mtdConsultaRptPerfiles(string strFechaInicial, string strFechaFinal, string strNroIdentificacion,
            string strPerfil, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            //DataTable dtInformacion = new DataTable();
            DataSet dsInformacion = new DataSet();
            string strConsulta = string.Empty;
            SqlParameter[] objSqlParams = new SqlParameter[4];
            #endregion Vars

            try
            {
                objSqlParams[0] = new SqlParameter("@FechaInicial", strFechaInicial);
                objSqlParams[1] = new SqlParameter("@FechaFinal", strFechaFinal);
                objSqlParams[2] = new SqlParameter("@NroDocumento", strNroIdentificacion);
                objSqlParams[3] = new SqlParameter("@Perfil", strPerfil);

                cDatabase.conectar();
                dsInformacion = cDatabase.mtdEjecutarSPParametroSQL("SP_RptPerfiles", "dsRptPerfiles", objSqlParams);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el reporte de perfiles. [{0}]", ex.Message);
                dsInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dsInformacion;
        }

        public void mtdActualizarHistoricoInfoInspektor(clsDTOHistoricoCalculoPerfil objHistorico, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblHistoricoCalculoPerfil] " +
                    "SET [InfoInspektor] = '{0}' WHERE [IdHistorico] = {1}",
                    objHistorico.StrInfoInspektor, objHistorico.StrIdHistorico);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el histórico de perfiles. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        /*
         * Metodos para el Servicio
         */
        public void mtdAgregarHistPerfil(string strOleConn, clsDTOHistoricoCalculoPerfil objHistPerfil, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblHistoricoCalculoPerfil] " +
                    "([IdPerfil], [IdArchivo], [TipoDocumento], [NumeroDocumento], [NombreCliente], [CalificacionPerfil], [Producto], [FechaGeneracion], [SenalesAlerta]) " +
                    "VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', GETDATE(), '{7}')",
                    Convert.ToInt32(objHistPerfil.StrIdPerfil), Convert.ToInt32(objHistPerfil.StrIdArchivo),
                    objHistPerfil.StrTipoDocCliente, objHistPerfil.StrNroDocCliente, objHistPerfil.StrNombreCliente,
                    objHistPerfil.StrCalificacionPerfil, objHistPerfil.StrCodProducto, objHistPerfil.StrSenalesAlerta);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la parametrización. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public DataTable mtdConsultaPerfiles(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdPerfil], [Nombre] NombrePerfil, [ValorMinimo], [ValorMaximo] FROM [Perfiles].[tblPerfil]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        
        public void mtdActualizarHistoricoInfoInspektor(string strOleConn, clsDTOHistoricoCalculoPerfil objHistorico, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblHistoricoCalculoPerfil] " +
                    "SET [InfoInspektor] = '{0}' WHERE [IdHistorico] = {1}",
                    objHistorico.StrInfoInspektor, objHistorico.StrIdHistorico);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el histórico de perfiles. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public DataTable mtdConsultaHistorico(string strOleConn, clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PHCP.IdHistorico, PHCP.IdPerfil, Temp.TipoDocumento, Temp.NumeroDocumento, Temp.NombreCliente, Temp.Calificacion, PHCP.Producto, PHCP.InfoInspektor, PHCP.SenalesAlerta, PHCP.FechaGeneracion, PHCP.Linea " +
                    "FROM [Perfiles].[tblHistoricoCalculoPerfil] PHCP " +
                    "INNER JOIN (SELECT MAX(CAST(REPLACE(CalificacionPerfil,',','.') AS DECIMAL(18,2))) Calificacion, TipoDocumento, NumeroDocumento, NombreCliente " +
                    "FROM [Perfiles].[tblHistoricoCalculoPerfil] WHERE IdArchivo = {0} " +
                    "GROUP BY NumeroDocumento, NombreCliente, TipoDocumento) Temp ON CAST(REPLACE(PHCP.CalificacionPerfil,',','.') AS DECIMAL(18,2)) = Temp.Calificacion " +
                    "AND Temp.NumeroDocumento = PHCP.NumeroDocumento " +
                    "WHERE PHCP. IdArchivo = {0} ", objArchivo.StrIdArchivo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los históricos de perfiles. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        
        public DataSet mtdConsultaRptPerfilesDetalle(string strFechaInicial, string strFechaFinal, string strNroIdentificacion,
            string strPerfil, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            //DataTable dtInformacion = new DataTable();
            DataSet dsInformacion = new DataSet();
            string strConsulta = string.Empty;
            SqlParameter[] objSqlParams = new SqlParameter[4];
            #endregion Vars

            try
            {
                objSqlParams[0] = new SqlParameter("@FechaInicial", strFechaInicial);
                objSqlParams[1] = new SqlParameter("@FechaFinal", strFechaFinal);
                objSqlParams[2] = new SqlParameter("@NroDocumento", strNroIdentificacion);
                objSqlParams[3] = new SqlParameter("@Perfil", strPerfil);

                cDatabase.conectar();
                dsInformacion = cDatabase.mtdEjecutarSPParametroSQL("SP_RptPerfilesDetalles", "dsRptPerfiles", objSqlParams);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el reporte de perfiles. [{0}]", ex.Message);
                dsInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dsInformacion;
        }
        /*
        * Metodos para el Historico de Inspektor
        */
        public DataSet mtdConsultarRptHistInspektor(string strFechaInicial, string strFechaFinal, string strNroIdentificacion,
            ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            //DataTable dtInformacion = new DataTable();
            DataSet dsInformacion = new DataSet();
            string strConsulta = string.Empty;
            SqlParameter[] objSqlParams = new SqlParameter[3];
            #endregion Vars

            try
            {
                objSqlParams[0] = new SqlParameter("@FechaInicial", strFechaInicial);
                objSqlParams[1] = new SqlParameter("@FechaFinal", strFechaFinal);
                objSqlParams[2] = new SqlParameter("@NroDocumento", strNroIdentificacion);
                
                cDatabase.conectar();
                dsInformacion = cDatabase.mtdEjecutarSPParametroSQL("SP_RptHistoricoInspektor", "dsRptPerfiles", objSqlParams);

                
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el reporte de histórico INSPEKTOR. [{0}]", ex.Message);
                dsInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dsInformacion;
        }

        public DataTable mtdConsultaHistClientes(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PHCP.NumeroDocumento, PHCP.NombreCliente, PHCP.TipoDocumento " +
                    "FROM Perfiles.tblHistoricoCalculoPerfil PHCP " +
                    "WHERE PHCP.FechaGeneracion >= DATEADD(month, -12, GETDATE()) " +
                    "GROUP BY PHCP.NUMERODOCUMENTO, PHCP.NombreCliente, PHCP.TipoDocumento");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los históricos de perfiles. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public void mtdInsHistClienteInfoInspektor( clsDTOHistoricoCalculoPerfil objHistorico, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            //clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            clsDatabase cDatabase = new clsDatabase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblHistoricoInspektor] " +
                    "([TipoDocumento],[NumeroDocumento],[NombreCliente],[CalificacionPerfil],[Producto],[InfoInspektor],[SenalesAlerta],[FechaGeneracion]) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', GETDATE())",
                    objHistorico.StrTipoDocCliente, objHistorico.StrNroDocCliente, objHistorico.StrNombreCliente,
                    objHistorico.StrCalificacionPerfil, objHistorico.StrCodProducto, objHistorico.StrInfoInspektor,
                    objHistorico.StrSenalesAlerta);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el histórico de INSPEKTOR. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

    }
}
