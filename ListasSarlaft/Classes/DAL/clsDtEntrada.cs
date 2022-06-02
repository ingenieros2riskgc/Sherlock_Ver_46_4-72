using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtEntrada
    {
        /// <summary>
        /// Realiza la consulta para traer todas las entradas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarEntrada(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PE.[Id], PE.[Descripcion], PE.[Estado], PE.[Proveedor], PE.[FechaRegistro], PE.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblEntrada] PE INNER JOIN [Listas].[Usuarios] LU ON PE.[IdUsuario] = LU.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la entrada. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todas las entradas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarEntrada(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PE.[Id], PE.[Descripcion], PE.[Estado], PE.[Proveedor], " +
                    "PE.[FechaRegistro], PE.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblEntrada] PE " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PE.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Procesos].[tblCaractXEntrada] PCE ON PE.Id = PCE.IdEntrada " +
                    "LEFT JOIN [Procesos].[tblCaracterizacion] PC ON PC.Id = PCE.IdCaracterizacion " +
                    "WHERE PC.IdTipoProceso = {0} AND PC.IdProceso = {1}",
                    objCaracter.intIdTipoProceso, objCaracter.intIdProceso);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la entrada. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todas las entradas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarEntrada(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PE.[Id], PE.[Descripcion], PE.[Estado], PE.[Proveedor], " +
                    "PE.[FechaRegistro], PE.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblEntrada] PE " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PE.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PE.[Estado] = {0}",
                    booEstado == true ? 1 : 0);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la entrada. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todas las entradas asociadas a la actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarEntradaxActividad(int idActividad, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdActividadEntrada],[IdActividad],[IdEntrada],[FechaRegistro],[IdUsuario] FROM [Procesos].[tblActividadEntrada] " +
                    "WHERE [IdActividad] = {0}",
                    idActividad);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la entrada. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion de una entrada.
        /// </summary>
        /// <param name="objEntrada">objeto con la informacion de la entrada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>booleano que resuelve si la operacion fue exitosa o no</returns>
        public bool mtdInsertarEntrada(clsEntrada objEntrada, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblEntrada]([Descripcion],[Estado],[Proveedor],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',{1},'{2}',GETDATE(),{3})",
                    objEntrada.strDescripcion, objEntrada.booEstado == false ? 0 : 1, objEntrada.strProveedor, objEntrada.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la entrada. [{0}]", ex.Message);
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
        /// <param name="objEntrada">Informacion de la entrada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEntrada(clsEntrada objEntrada, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblEntrada] " +
                    "SET [Descripcion] = '{1}', [Proveedor] = '{2}' WHERE [Id] = {0}",
                    objEntrada.intId, objEntrada.strDescripcion, objEntrada.strProveedor);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la entrada. [{0}]", ex.Message);
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
        /// <param name="objEntrada">Informacion de la entrada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsEntrada objEntrada, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblEntrada] SET [Estado] = {1} WHERE [Id] = {0}",
                    objEntrada.intId, objEntrada.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la entrada. [{0}]", ex.Message);
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