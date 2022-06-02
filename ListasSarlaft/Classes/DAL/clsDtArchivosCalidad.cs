using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtArchivosCalidad
    {
        /// <summary>
        /// Realiza la consulta para traer todos los Archivos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarArchivos(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PAC.[Id], PAC.[IdTipoControl], PAC.[IdControl], PAC.[FechaRegistro], PAC.[NombreArchivo], " +
                    "PAC.[ArchivoBinario], PAC.[IdUsuario], LU.[Usuario] NombreUsuario, PAC.[Estado] " +
                    "FROM [Procesos].[tblArchivoCalidad] PAC " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PAC.[IdUsuario] = LU.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                dtInformacion = null;
                strErrMsg = string.Format("Error al consultar el Archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los Archivos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarArchivos(clsArchivosCalidad objArchivoCalidad, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PAC.[Id], PAC.[IdTipoControl], PAC.[IdControl], PAC.[FechaRegistro], PAC.[NombreArchivo], " +
                    "PAC.[ArchivoBinario], PAC.[IdUsuario], LU.[Usuario] NombreUsuario, PAC.[Estado] " +
                    "FROM [Procesos].[tblArchivoCalidad] PAC " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PAC.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PAC.[IdTipoControl] = {0} AND PAC.[IdControl] = {1}", 
                    objArchivoCalidad.intIdTipoControl, objArchivoCalidad.intIdControl);

                cDataBase.mtdConectarSql();
                dtInformacion = cDataBase.mtdEjecutarConsultaSQL(strConsulta);
            }
            catch (Exception ex)
            {
                dtInformacion = null;
                strErrMsg = string.Format("Error al consultar el Archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.mtdDesconectarSql();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer el Archivo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarArchivo(clsArchivosCalidad objArchivoCalidad, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PAC.[Id], PAC.[IdTipoControl], PAC.[IdControl], PAC.[FechaRegistro], PAC.[NombreArchivo], " +
                    "PAC.[ArchivoBinario], PAC.[IdUsuario], LU.[Usuario] NombreUsuario, PAC.[Estado] " +
                    "FROM [Procesos].[tblArchivoCalidad] PAC " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PAC.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PAC.[Id] = {0} ",
                    objArchivoCalidad.intId);

                cDataBase.mtdConectarSql();
                dtInformacion = cDataBase.mtdEjecutarConsultaSQL(strConsulta);
            }
            catch (Exception ex)
            {
                dtInformacion = null;
                strErrMsg = string.Format("Error al consultar el Archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.mtdDesconectarSql();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta del consecutivo de los archivos
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdConsecutivoArchivo(ref string strErrMsg)
        {
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            cDataBase cDataBase = new cDataBase();

            try
            {
                strConsulta = string.Format("SELECT TOP (1) [Id] + 1 AS Consecutivo FROM [Procesos].[tblArchivoCalidad] ORDER BY [Id] DESC");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                dtInformacion = null;
                strErrMsg = string.Format("Error al consultar el consecutivo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion de un Archivo.
        /// </summary>
        /// <param name="objProcedimiento">objeto con la informacion del Archivo</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>booleano que resuelve si la operacion fue exitosa o no</returns>
        public bool mtdInsertarArchivo(clsArchivosCalidad objArchivo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblArchivoCalidad] "+
                    "([IdTipoControl], [IdControl], [FechaRegistro], [NombreArchivo], [ArchivoBinario], [IdUsuario], [Estado]) " +
                    "VALUES({0}, {1}, GETDATE(), '{2}', @PdfData, {3}, {4})",
                    objArchivo.intIdTipoControl, objArchivo.intIdControl, objArchivo.strNombreArchivo, objArchivo.intIdUsuario, objArchivo.booEstado == false ? 0 : 1);

                cDatabase.mtdConectarSql();
                cDatabase.mtdEjecutarConsultaSQL(strConsulta, objArchivo.bArchivoBinario);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el archivo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de la informacion en la base de datos
        /// </summary>
        /// <param name="objArchivo">Informacion del Archivo</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsArchivosCalidad objArchivo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblArchivoCalidad] SET [Estado] = {1} WHERE [Id] = {0}",
                    objArchivo.intId, objArchivo.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Archivo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
    }
}