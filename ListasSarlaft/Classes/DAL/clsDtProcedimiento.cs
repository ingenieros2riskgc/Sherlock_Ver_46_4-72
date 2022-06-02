using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtProcedimiento
    {
        /// <summary>
        /// Realiza la consulta para traer todas los Procedimiento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProcedimiento(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[Id], PP.[Descripcion], PP.[Estado], PP.[IdActividad], PA.[Descripcion] DescActividad, " +
                    "PP.[FechaRegistro], PP.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblProcedimiento] PP " +
                    "INNER JOIN [Procesos].[tblActividad] PA ON PA.[Id] = PP.[IdActividad] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PP.[IdUsuario] = LU.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Procedimiento. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion de un Procedimiento.
        /// </summary>
        /// <param name="objProcedimiento">objeto con la informacion del Procedimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>booleano que resuelve si la operacion fue exitosa o no</returns>
        public bool mtdInsertarProcedimiento(clsProcedimiento objProcedimiento, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblProcedimiento]([Descripcion],[Estado],[IdActividad],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',{1},{2},GETDATE(),{3})",
                    objProcedimiento.strDescripcion, objProcedimiento.booEstado == false ? 0 : 1, objProcedimiento.intIdActividad, objProcedimiento.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Procedimiento. [{0}]", ex.Message);
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
        /// <param name="objProcedimiento">Informacion de Procedimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarProcedimiento(clsProcedimiento objProcedimiento, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblProcedimiento] SET [Descripcion] = '{1}', [IdActividad] = {2}, [Estado] = {3} " +
                    "WHERE [Id] = {0}",
                    objProcedimiento.intId, objProcedimiento.strDescripcion, objProcedimiento.intIdActividad, objProcedimiento.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Procedimiento. [{0}]", ex.Message);
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
        /// <param name="objProcedimiento">Informacion de la Procedimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsProcedimiento objProcedimiento, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblProcedimiento] SET [Estado] = {1} WHERE [Id] = {0}",
                    objProcedimiento.intId, objProcedimiento.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Procedimiento. [{0}]", ex.Message);
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