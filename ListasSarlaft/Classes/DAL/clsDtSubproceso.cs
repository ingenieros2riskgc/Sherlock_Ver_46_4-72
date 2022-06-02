using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtSubproceso
    {
        /// <summary>
        /// Realiza la consulta para traer todos los Subprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarSubproceso(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PS.[IdSubproceso], PS.[Nombre] NombreSubproceso, PS.[Descripcion], PS.[Objetivo], PS.[Estado], " +
                    "PS.[IdHijo] IdCargoResponsable, PJO.[NombreHijo] CargoResponsable, PS.[IdProceso], PP.[Nombre] NombreProceso, " +
                    "PMP.[IdMacroProceso], PMP.[Nombre] NombreMacroProceso, PCV.[IdCadenaValor], PCV.[NombreCadenaValor], PS.[FechaRegistro], "+
                    "PS.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[Subproceso] PS " +
                    "INNER JOIN [Procesos].[Proceso] PP ON PP.[IdProceso] = PS.[IdProceso] " +
                    "INNER JOIN [Procesos].[Macroproceso] PMP ON PP.IdMacroProceso = PMP.IdMacroProceso " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PS.[idHijo] = PJO.[idHijo] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PS.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PCV.[Estado] = 1 AND PMP.[Estado] = 1 AND PP.[Estado] = 1");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Subprocesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable ConsultarSubprocesos(int? idProceso)
        {
            try
            {
                cDataBase cDataBase = new cDataBase();
                string query = $"SELECT IdSubproceso, Nombre FROM Procesos.Subproceso WHERE IdProceso = {idProceso} AND Estado = 1";
                cDataBase.conectar();
                DataTable dt = cDataBase.ejecutarConsulta(query);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza la consulta para traer todos los Subprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarSubproceso(clsSubproceso objSubpIN, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PS.[IdSubproceso], PS.[Nombre] NombreSubproceso, PS.[Descripcion], PS.[Objetivo], PS.[Estado], " +
                    "PS.[IdHijo] IdCargoResponsable, PJO.[NombreHijo] CargoResponsable, PS.[IdProceso], PP.[Nombre] NombreProceso, " +
                    "PMP.[IdMacroProceso], PMP.[Nombre] NombreMacroProceso, PCV.[IdCadenaValor], PCV.[NombreCadenaValor], PS.[FechaRegistro], " +
                    "PS.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[Subproceso] PS " +
                    "INNER JOIN [Procesos].[Proceso] PP ON PP.[IdProceso] = PS.[IdProceso] " +
                    "INNER JOIN [Procesos].[Macroproceso] PMP ON PP.IdMacroProceso = PMP.IdMacroProceso " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PS.[idHijo] = PJO.[idHijo] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PS.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PS.[IdSubproceso] = {0}", objSubpIN.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Subprocesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los Subprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarSubproceso(bool booEstado, clsProceso objProceso, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PS.[IdSubproceso], PS.[Nombre] NombreSubproceso, PS.[Descripcion], PS.[Objetivo], PS.[Estado], " +
                    "PS.[IdHijo] IdCargoResponsable, PJO.[NombreHijo] CargoResponsable, PS.[IdProceso], PP.[Nombre] NombreProceso, " +
                    "PMP.[IdMacroProceso], PMP.[Nombre] NombreMacroProceso, PCV.[IdCadenaValor], PCV.[NombreCadenaValor], PS.[FechaRegistro], " +
                    "PS.[IdUsuario], LU.[Usuario] NombreUsuario " +
                     "FROM [Procesos].[Subproceso] PS " +
                    "INNER JOIN [Procesos].[Proceso] PP ON PP.[IdProceso] = PS.[IdProceso] " +
                    "INNER JOIN [Procesos].[Macroproceso] PMP ON PP.IdMacroProceso = PMP.IdMacroProceso " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PS.[idHijo] = PJO.[idHijo] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PS.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PCV.[Estado] = 1 AND PMP.[Estado] = 1 AND PP.[Estado] = 1 AND PS.[IdProceso] = {2}",
                    booEstado == false ? 0 : 1, objProceso.booEstado == false ? 0 : 1, objProceso.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los subprocesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion del SubProceso.
        /// </summary>
        /// <param name="objProceso">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdInsertarSubproceso(clsSubproceso objSubproceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[Subproceso]([IdProceso], [Nombre], [Descripcion], [Objetivo], " +
                    "[Estado],[IdHijo],[IdUsuario],[FechaRegistro])" +
                    "VALUES({0},'{1}','{2}','{3}',{4},'{5}',{6},GETDATE())",
                    objSubproceso.intIdProceso, objSubproceso.strNombreSubproceso, objSubproceso.strDescripcion, objSubproceso.strObjetivo,
                    objSubproceso.booEstado == false ? 0 : 1, objSubproceso.intIdCargoResponsable, objSubproceso.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Subproceso. [{0}]", ex.Message);
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
        /// <param name="objProceso">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarSubproceso(clsSubproceso objSubproceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[Subproceso] " +
                    "SET [IdProceso] = {1},[Nombre] = '{2}',[Descripcion] = '{3}',[Objetivo] = '{4}',[Estado] = {5},[IdHijo] = '{6}' " +
                    "WHERE [IdSubproceso] = {0}",
                    objSubproceso.intId, objSubproceso.intIdProceso, objSubproceso.strNombreSubproceso, objSubproceso.strDescripcion,
                    objSubproceso.strObjetivo, objSubproceso.booEstado == false ? 0 : 1, objSubproceso.intIdCargoResponsable);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el subproceso. [{0}]", ex.Message);
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
        /// <param name="objProceso">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsSubproceso objSubproceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[Subproceso] SET [Estado] = {1} WHERE [IdSubproceso] = {0}",
                    objSubproceso.intId, objSubproceso.booEstado == false ? 0 : 1);


                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Subproceso. [{0}]", ex.Message);
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