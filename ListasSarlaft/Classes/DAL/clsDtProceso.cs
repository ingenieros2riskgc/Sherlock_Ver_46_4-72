using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtProceso
    {
        /// <summary>
        /// Realiza la consulta para traer todos los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProceso(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdProceso], PP.[Nombre] NombreProceso, PP.[IdMacroProceso], PMP.[Nombre] NombreMacroProceso, " +
                    "PP.[Descripcion], PP.[Objetivo], PP.[Estado], ISNULL(PP.[IdHijo], 0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable, " +
                    "PP.[IdUsuario], LU.[Usuario], PP.[FechaRegistro], PP.[IdEmpresa], PP.[IdArea], PCV.[IdCadenaValor], PP.[ContextoInterno],PP.[ContextoExterno] " +
                    "FROM [Procesos].[Proceso] PP " +
                    "INNER JOIN [Procesos].[Macroproceso] PMP ON PP.IdMacroProceso = PMP.IdMacroProceso " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PP.[idHijo] = PJO.[idHijo] " +
                    "WHERE PCV.[Estado] = 1 AND PMP.[Estado] = 1");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable ConsultarProcesos(int idMacroProceso)
        {
            try
            {
                cDataBase cDataBase = new cDataBase();
                string query = $"SELECT IdProceso, Nombre from Procesos.Proceso WHERE IdMacroProceso = {idMacroProceso} AND Estado = 1";
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
        /// Realiza la consulta para traer todos los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProceso(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdProceso], PP.[Nombre] NombreProceso, PP.[IdMacroProceso], PMP.[Nombre] NombreMacroProceso, " +
                    "PP.[Descripcion], PP.[Objetivo], PP.[Estado], ISNULL(PP.[IdHijo], 0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable, " +
                    "PP.[IdUsuario], LU.[Usuario], PP.[FechaRegistro], PP.[IdEmpresa], PP.[IdArea], PCV.[NombreCadenaValor] " +
                    "FROM [Procesos].[Proceso] PP " +
                    "INNER JOIN [Procesos].[Macroproceso] PMP ON PP.IdMacroProceso = PMP.IdMacroProceso " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PMP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PP.[idHijo] = PJO.[idHijo] " +
                    "WHERE PCV.[Estado] = 1 AND PMP.[Estado] = 1 AND PP.[Estado] = {0}", booEstado == false ? 0 : 1);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProceso(bool booEstado, clsMacroproceso objMacroproceso, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdProceso], PP.[Nombre] NombreProceso, PP.[IdMacroProceso], PMP.[Nombre] NombreMacroProceso, " +
                    "PP.[Descripcion], PP.[Objetivo], PP.[Estado], ISNULL(PP.[IdHijo], 0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable, " +
                    "PP.[IdUsuario], LU.[Usuario], PP.[FechaRegistro], PP.[IdEmpresa], PP.[IdArea], PCV.[NombreCadenaValor], PCV.[IdCadenaValor] " +
                    "FROM [Procesos].[Proceso] PP " +
                    "INNER JOIN [Procesos].[Macroproceso] PMP ON PP.IdMacroProceso = PMP.IdMacroProceso " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PMP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PP.[idHijo] = PJO.[idHijo] " +
                    "WHERE PCV.[Estado] = 1 AND PMP.[Estado] = {1} AND PP.[Estado] = {0} AND PMP.[IdMacroProceso] = {2}",
                    booEstado == false ? 0 : 1, objMacroproceso.booEstado == false ? 0 : 1, objMacroproceso.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los procesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProceso(clsProceso ObjProIN, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdProceso], PP.[Nombre] NombreProceso, PP.[IdMacroProceso], PMP.[Nombre] NombreMacroProceso, " +
                    "PP.[Descripcion], PP.[Objetivo], PP.[Estado], ISNULL(PP.[IdHijo], 0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable, " +
                    "PP.[IdUsuario], LU.[Usuario], PP.[FechaRegistro], PP.[IdEmpresa], PP.[IdArea], PCV.[NombreCadenaValor], PCV.[IdCadenaValor] " +
                    "FROM [Procesos].[Proceso] PP " +
                    "INNER JOIN [Procesos].[Macroproceso] PMP ON PP.IdMacroProceso = PMP.IdMacroProceso " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PMP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PP.[idHijo] = PJO.[idHijo] " +
                    "WHERE PP.[IdProceso] = {0}", ObjProIN.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion del Proceso.
        /// </summary>
        /// <param name="objProceso">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdInsertarProceso(clsProceso objProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[Proceso]([IdMacroProceso],[Nombre],[Descripcion],[Objetivo],[Estado],[IdHijo],[IdUsuario],[FechaRegistro],[IdEmpresa],[IdArea],[ContextoInterno],[ContextoExterno])" +
                    "VALUES({0},'{1}','{2}','{3}',{4},'{5}',{6},GETDATE(),{7},'{8}','{9}','{10}')",
                    objProceso.intIdMacroProceso, objProceso.strNombreProceso, objProceso.strDescripcion, objProceso.strObjetivo,
                    objProceso.booEstado == false ? 0 : 1, objProceso.intCargoResponsable, objProceso.intIdUsuario, objProceso.intIdEmpresa, objProceso.strArea, objProceso.ContextoInterno, objProceso.ContextoExterno);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Proceso. [{0}]", ex.Message);
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
        public bool mtdActualizarProceso(clsProceso objProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[Proceso] " +
                    "SET [IdMacroProceso] = {1}, [Nombre] = '{2}', [Descripcion] = '{3}', [Objetivo] = '{4}', [IdHijo] = '{6}',[IdEmpresa] = {7},[IdArea] = '{8}',[ContextoInterno]='{9}',[ContextoExterno]='{10}' " +
                    "WHERE [IdProceso] = {0}",
                    objProceso.intId, objProceso.intIdMacroProceso, objProceso.strNombreProceso, objProceso.strDescripcion, objProceso.strObjetivo,
                    objProceso.booEstado == false ? 0 : 1, objProceso.intCargoResponsable, objProceso.intIdEmpresa, objProceso.strArea,objProceso.ContextoInterno,objProceso.ContextoExterno);


                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Proceso. [{0}]", ex.Message);
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
        public bool mtdActualizarEstado(clsProceso objProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[Proceso] SET [Estado] = {1} WHERE [IdProceso] = {0}",
                    objProceso.intId, objProceso.booEstado == false ? 0 : 1);


                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Proceso. [{0}]", ex.Message);
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