using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtCadenaValor
    {
        /// <summary>
        /// Realiza la consulta para traer todas las cadenas de valor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarCadenaValor(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PCV.[IdCadenaValor], PCV.[NombreCadenaValor], PCV.[IdUsuario], LU.[Usuario], PCV.[FechaRegistro], PCV.[Estado] " +
                    "FROM [Procesos].[CadenaValor] PCV INNER JOIN  [Listas].[Usuarios] LU ON LU.[IdUsuario] = PCV.[IdUsuario] ORDER BY PCV.[IdCadenaValor] ASC");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cadena de valor. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarCadenaValor(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PCV.[IdCadenaValor], PCV.[NombreCadenaValor], PCV.[IdUsuario], LU.[Usuario], PCV.[FechaRegistro], PCV.[Estado] " +
                    "FROM [Procesos].[CadenaValor] PCV INNER JOIN  [Listas].[Usuarios] LU ON LU.[IdUsuario] = PCV.[IdUsuario] " + 
                    "WHERE PCV.[Estado] = {0} ORDER BY PCV.[IdCadenaValor] ASC", booEstado == false ? 0 : 1);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cadena de valor. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        public bool mtdConsultarCadenaValor(bool booEstado, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                //strConsulta = string.Format("SELECT PCV.[IdCadenaValor], PCV.[NombreCadenaValor], PCV.[IdUsuario], LU.[Usuario], PCV.[FechaRegistro], PCV.[Estado] " +
                //    "FROM [Procesos].[CadenaValor] PCV INNER JOIN  [Listas].[Usuarios] LU ON LU.[IdUsuario] = PCV.[IdUsuario] " +
                //    "WHERE PCV.[Estado] = {0} ORDER BY PCV.[IdCadenaValor] ASC", booEstado == false ? 0 : 1);
                strConsulta = string.Format("EXEC [Procesos].[SeleccionarMapaProcesos]", booEstado == false ? 0 : 1);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cadena de valor. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la insercion de una cadena de valor.
        /// </summary>
        /// <param name="objCadenaValor">objeto con la informacion de la cadena de valor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>booleano que resuelve si la operacion fue exitosa o no</returns>
        public bool mtdInsertarCadenaValor(clsCadenaValor objCadenaValor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[CadenaValor]([NombreCadenaValor], [IdUsuario], [FechaRegistro], [Estado]) " +
                    "VALUES ('{0}', {1}, GETDATE(), {2})", objCadenaValor.strNombreCadenaValor, objCadenaValor.intIdUsuario, objCadenaValor.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la cadena de valor. [{0}]", ex.Message);
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
        /// <param name="objCadenaValor">Informacion de la cadena de valor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCadenaValor(clsCadenaValor objCadenaValor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {                
                strConsulta = string.Format("UPDATE [Procesos].[CadenaValor] " +
                    "SET [NombreCadenaValor] = '{1}',[Estado] = {2} WHERE [IdCadenaValor] = {0}",
                    objCadenaValor.intId, objCadenaValor.strNombreCadenaValor, objCadenaValor.booEstado == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la cadena de valor. [{0}]", ex.Message);
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
        /// <param name="objCadenaValor">Informacion de la cadena de valor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsCadenaValor objCadenaValor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[CadenaValor] SET [Estado] = '{1}' WHERE [IdCadenaValor] = {0}",
                    objCadenaValor.intId, objCadenaValor.booEstado == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la cadena de valor. [{0}]", ex.Message);
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