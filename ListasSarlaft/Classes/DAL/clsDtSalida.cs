using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtSalida
    {
        /// <summary>
        /// Realiza la consulta para traer todas las salidas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarSalida(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PS.[Id], PS.[Descripcion], PS.[Estado], PS.[Cliente], PS.[FechaRegistro], PS.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblSalida] PS INNER JOIN [Listas].[Usuarios] LU ON PS.[IdUsuario] = LU.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la salida. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todas las salidas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarSalidaxActividad(int idActividad,ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdActividadSalida],[IdActividad],[IdSalida],[FechaRegistro],[IdUsuario] FROM [Procesos].[tblActividadSalida] " +
                    "WHERE [IdActividad] = {0} ",idActividad);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la salida. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todas las salidas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarSalida(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PS.[Id], PS.[Descripcion], PS.[Estado], PS.[Cliente], PS.[FechaRegistro], PS.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblSalida] PS " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PS.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Procesos].[tblCaractXSalida] PCA ON PS.[Id] = PCA.[IdSalida] " +
                    "LEFT JOIN [Procesos].[tblCaracterizacion] PC ON PC.Id = PCA.IdCaracterizacion " +
                    "WHERE PC.[IdTipoProceso] = {0} AND PC.[IdProceso] = {1} ",
                    objCaracter.intIdTipoProceso, objCaracter.intIdProceso);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la salida. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todas las salidas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarSalida(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PS.[Id], PS.[Descripcion], PS.[Estado], PS.[Cliente], PS.[FechaRegistro], PS.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblSalida] PS " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PS.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PS.[Estado] = {0} ",
                    booEstado == true ? 1 : 0);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la salida. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion de una salida.
        /// </summary>
        /// <param name="objSalida">objeto con la informacion de la salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>booleano que resuelve si la operacion fue exitosa o no</returns>
        public bool mtdInsertarSalida(clsSalida objSalida, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblSalida]([Descripcion],[Estado],[Cliente],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',{1},'{2}',GETDATE(),{3})",
                    objSalida.strDescripcion, objSalida.booEstado == false ? 0 : 1, objSalida.strCliente, objSalida.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la salida. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de la informacion en la base de datos
        /// </summary>
        /// <param name="objSalida">Informacion de la salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarSalida(clsSalida objSalida, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblSalida] " +
                    "SET [Descripcion] = '{1}', [Cliente] = '{2}' WHERE [Id] = {0}",
                    objSalida.intId, objSalida.strDescripcion, objSalida.strCliente);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la salida. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de la informacion en la base de datos
        /// </summary>
        /// <param name="objSalida">Informacion de la salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsSalida objSalida, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblSalida] SET [Estado] = {1} WHERE [Id] = {0}",
                    objSalida.intId, objSalida.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la salida. [{0}]", ex.Message);
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