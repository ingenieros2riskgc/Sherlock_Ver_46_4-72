using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtMacroproceso
    {
        /// <summary>
        /// Realiza la consulta para traer todos los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarMacroproceso(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PMP.[IdMacroProceso], PMP.[Nombre], PMP.[FechaRegistro], PMP.[Descripcion], PMP.[Objetivo], " +
                    "PMP.[IdCadenaValor], PCV.[NombreCadenaValor], PMP.[IdUsuario], LU.[Usuario], PMP.[Estado], " +
                    "ISNULL(PMP.[Responsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable " +
                    "FROM [Procesos].[Macroproceso] PMP " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PMP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PMP.[Responsable] = PJO.[idHijo] " +
                    "WHERE PCV.[Estado] = 1");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los macroprocesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarMacroproceso(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PMP.[IdMacroProceso], PMP.[Nombre], PMP.[FechaRegistro], PMP.[Descripcion], PMP.[Objetivo], " +
                    "PMP.[IdCadenaValor], PCV.[NombreCadenaValor], PMP.[IdUsuario], LU.[Usuario], PMP.[Estado], " +
                    "ISNULL(PMP.[Responsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable " +
                    "FROM [Procesos].[Macroproceso] PMP " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PMP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PMP.[Responsable] = PJO.[idHijo] " +
                    "WHERE PCV.[Estado] = 1 AND PMP.[Estado] = {0}", booEstado == false ? 0 : 1);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los macroprocesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }


        public DataTable ConsultarMacroProcesos(int idCadenaValor)
        {
            try
            {
                cDataBase cDataBase = new cDataBase();
                string query = $"SELECT IdMacroProceso, Nombre FROM Procesos.Macroproceso WHERE IdCadenaValor = {idCadenaValor} AND Estado = 1";
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
        /// Realiza la consulta para traer todos los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarMacroproceso(bool booEstado, clsCadenaValor objCadenaValor, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PMP.[IdMacroProceso], PMP.[Nombre], PMP.[FechaRegistro], PMP.[Descripcion], PMP.[Objetivo], " +
                    "PMP.[IdCadenaValor], PCV.[NombreCadenaValor], PMP.[IdUsuario], LU.[Usuario], PMP.[Estado], " +
                    "ISNULL(PMP.[Responsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable " +
                    "FROM [Procesos].[Macroproceso] PMP " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PMP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PMP.[Responsable] = PJO.[idHijo] " +
                    "WHERE PMP.[Estado] = {0} AND PCV.[Estado] = {1} AND PCV.[IdCadenaValor] = {2}",
                    booEstado == false ? 0 : 1, objCadenaValor.booEstado == false ? 0 : 1, objCadenaValor.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los macroprocesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        public bool mtdConsultarMacroproceso(bool booEstado, clsCadenaValor objCadenaValor, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PMP.[IdMacroProceso], PMP.[Nombre], PMP.[FechaRegistro], PMP.[Descripcion], PMP.[Objetivo], " +
                    "PMP.[IdCadenaValor], PCV.[NombreCadenaValor], PMP.[IdUsuario], LU.[Usuario], PMP.[Estado], " +
                    "ISNULL(PMP.[Responsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable " +
                    "FROM [Procesos].[Macroproceso] PMP " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PMP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PMP.[Responsable] = PJO.[idHijo] " +
                    "WHERE PMP.[Estado] = {0} AND PCV.[Estado] = {1} AND PCV.[IdCadenaValor] = {2}",
                    booEstado == false ? 0 : 1, objCadenaValor.booEstado == false ? 0 : 1, objCadenaValor.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los macroprocesos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarMacroproceso(clsMacroproceso objMPIn, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PMP.[IdMacroProceso], PMP.[Nombre], PMP.[FechaRegistro], PMP.[Descripcion], PMP.[Objetivo], " +
                    "PMP.[IdCadenaValor], PCV.[NombreCadenaValor], PMP.[IdUsuario], LU.[Usuario], PMP.[Estado], " +
                    "ISNULL(PMP.[Responsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable " +
                    "FROM [Procesos].[Macroproceso] PMP " +
                    "INNER JOIN [Procesos].[CadenaValor] PCV ON PMP.[IdCadenaValor] = PCV.[IdCadenaValor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PMP.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PMP.[Responsable] = PJO.[idHijo] " +
                    "WHERE PMP.[IdMacroProceso] = {0}", objMPIn.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los macroprocesos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion del macroproceso.
        /// </summary>
        /// <param name="objMacroproceso">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdInsertarMacroproceso(clsMacroproceso objMacroproceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[Macroproceso]([IdCadenaValor],[Nombre],[Descripcion],[Objetivo],[IdUsuario],[FechaRegistro],[Estado],[Responsable])" +
                    "VALUES ({0},'{1}','{2}','{3}',{4},GETDATE(),{5},{6})",
                    objMacroproceso.intIdCadenaDeValor, objMacroproceso.strNombreMacroproceso, objMacroproceso.strDescripcion, objMacroproceso.strObjetivo,
                    objMacroproceso.intIdUsuario, objMacroproceso.booEstado == false ? 0 : 1, objMacroproceso.intCargoResponsable);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el macroproceso. [{0}]", ex.Message);
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
        /// <param name="objMacroproceso">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarMacroproceso(clsMacroproceso objMacroproceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[Macroproceso] " +
                    "SET [IdCadenaValor] = {1},[Nombre] = '{2}',[Descripcion] = '{3}',[Objetivo] = '{4}',[Estado] = {5},[Responsable] = {6} " +
                    "WHERE [IdMacroProceso] = {0}",
                    objMacroproceso.intId, objMacroproceso.intIdCadenaDeValor, objMacroproceso.strNombreMacroproceso, objMacroproceso.strDescripcion,
                    objMacroproceso.strObjetivo, objMacroproceso.booEstado == false ? 0 : 1, objMacroproceso.intCargoResponsable);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el macroproceso. [{0}]", ex.Message);
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
        /// <param name="objMacroproceso">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsMacroproceso objMacroproceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[Macroproceso]  SET [Estado] = {1}  WHERE [IdMacroProceso] = {0}",
                    objMacroproceso.intId, objMacroproceso.booEstado == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el macroproceso. [{0}]", ex.Message);
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