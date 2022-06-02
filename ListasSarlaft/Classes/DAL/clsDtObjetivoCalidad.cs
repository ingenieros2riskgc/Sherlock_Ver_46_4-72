using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtObjetivoCalidad
    {
        /// <summary>
        /// Realiza la consulta para traer la Objetivo Calidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarObjetivoCalidad(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT POC.[Id], POC.[Descripcion], POC.[Estado], POC.[FechaRegistro], POC.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblObjetivoCalidad] POC " +
                    "INNER JOIN [Listas].[Usuarios] LU ON POC.[IdUsuario] = LU.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Objetivo Calidad. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer la Objetivo Calidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarObjetivoCalidad(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT POC.[Id], POC.[Descripcion], POC.[Estado], POC.[FechaRegistro], POC.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblObjetivoCalidad] POC " +
                    "INNER JOIN [Listas].[Usuarios] LU ON POC.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE POC.[Estado] = {0} ", booEstado == false ? 0 : 1);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Objetivo Calidad. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion de un Objetivo.
        /// </summary>
        /// <param name="objObjetivo">objeto con la informacion de la Objetivo</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>booleano que resuelve si la operacion fue exitosa o no</returns>
        public bool mtdInsertarObjetivoCalidad(clsObjetivoCalidad objObjetivo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblObjetivoCalidad]([Descripcion],[Estado],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',{1},GETDATE(),{2})",
                    objObjetivo.strDescripcion, objObjetivo.booEstado == false ? 0 : 1, objObjetivo.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Objetivo de Calidad. [{0}]", ex.Message);
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
        /// <param name="objObjetivo">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarObjetivoCalidad(clsObjetivoCalidad objObjetivo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblObjetivoCalidad] SET [Descripcion] = '{1}' " +
                    "WHERE [Id] = {0}",
                    objObjetivo.intId, objObjetivo.strDescripcion);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Objetivo de calidad. [{0}]", ex.Message);
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
        /// <param name="objObjetivo">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsObjetivoCalidad objObjetivo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblObjetivoCalidad]  SET [Estado] = {1}  WHERE [Id] = {0}",
                    objObjetivo.intId, objObjetivo.booEstado == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Objetivo de calidad. [{0}]", ex.Message);
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