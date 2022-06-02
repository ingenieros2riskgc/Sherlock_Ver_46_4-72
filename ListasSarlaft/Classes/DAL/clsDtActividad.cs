using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtActividad
    {
        /// <summary>
        /// Realiza la consulta para traer todas las Actividades
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarActividad(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PA.[Id], PA.[Descripcion], PA.[Estado], " +
                    "ISNULL(PA.[CargoResponsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable, " +
                    "PA.[FechaRegistro], PA.[IdUsuario], LU.[Usuario] NombreUsuario,PA.[idphva] " +
                    "FROM [Procesos].[tblActividad] PA " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PA.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PA.[CargoResponsable] = PJO.[idHijo]");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Actividad. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todas las Actividades
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarActividadActivas(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PA.[Id], PA.[Descripcion], PA.[Estado], " +
                    "ISNULL(PA.[CargoResponsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable, " +
                    "PA.[FechaRegistro], PA.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblActividad] PA " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PA.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PA.[CargoResponsable] = PJO.[idHijo]" +
                    " Where PA.[Estado] = 1");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Actividad. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todas las Actividades
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarActividad(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PA.[Id], PA.[Descripcion], PA.[Estado], " +
                    "ISNULL(PA.[CargoResponsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable, " +
                    "PA.[FechaRegistro], PA.[IdUsuario], LU.[Usuario] NombreUsuario,PA.[idphva] " +
                    "FROM [Procesos].[tblActividad] PA " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PA.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PA.[CargoResponsable] = PJO.[idHijo] " +
                    "WHERE PA.[Estado] = {0}", booEstado == false ? 0 : 1);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Actividad. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todas las Actividades
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarActividad(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PA.[Id], PA.[Descripcion], PA.[Estado], " +
                    "ISNULL(PA.[CargoResponsable],0) IdCargoResponsable, ISNULL(PJO.[NombreHijo],' ') CargoResponsable, " +
                    "PA.[FechaRegistro], PA.[IdUsuario], LU.[Usuario] NombreUsuario,PA.[idphva] " +
                    "FROM [Procesos].[tblActividad] PA " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PA.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PA.[CargoResponsable] = PJO.[idHijo] " +
                    "LEFT JOIN [Procesos].[tblCaractXActividad] PCA ON PA.Id = PCA.IdActividad " +
                    "LEFT JOIN [Procesos].[tblCaracterizacion] PC ON PC.Id = PCA.IdCaracterizacion " +
                    "WHERE PC.IdTipoProceso = {0} AND PC.IdProceso = {1}",
                    objCaracter.intIdTipoProceso, objCaracter.intIdProceso);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Actividad. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la insercion de una Actividad.
        /// </summary>
        /// <param name="objActividad">objeto con la informacion de la Actividad</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>booleano que resuelve si la operacion fue exitosa o no</returns>
        public bool mtdInsertarActividad(clsActividad objActividad, ref string strErrMsg, List<clsCaracterizacionXEntrada> lstEntrada,
            List<clsCaracterizacionXSalida> lstSalida)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInfo = new DataTable();
            cDataBase cDatabase = new cDataBase();
            clsActividad objLastAct = null;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblActividad]([Descripcion],[Estado],[CargoResponsable],[FechaRegistro],[IdUsuario],[idphva]) " +
                    "VALUES('{0}',{1},'{2}',GETDATE(),{3},{4})" +
                    "SELECT SCOPE_IDENTITY() ",
                    objActividad.strDescripcion, objActividad.booEstado == false ? 0 : 1, objActividad.intCargoResponsable, objActividad.intIdUsuario,
                    objActividad.intIdphva);

                /*cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);*/
                cDatabase.mtdConectarSql();
                dtInfo = cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        objLastAct = new clsActividad(Convert.ToInt32(dtInfo.Rows[0][0].ToString()),
                            objActividad.strDescripcion, objActividad.booEstado, objActividad.intCargoResponsable, objActividad.intIdUsuario, objActividad.dtFechaRegistro,
                            objActividad.intIdphva);
                
                foreach (clsCaracterizacionXEntrada objTemp in lstEntrada)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblActividadEntrada] ([IdActividad],[IdEntrada],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objLastAct.intId, objTemp.intIdEntrada, objActividad.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }
                foreach (clsCaracterizacionXSalida objTemp in lstSalida)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblActividadSalida] ([IdActividad],[IdSalida],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objLastAct.intId, objTemp.intIdSalida, objActividad.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Actividad. [{0}]", ex.Message);
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
        /// <param name="objActividad">Informacion de la Actividad</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarActividad(clsActividad objActividad, ref string strErrMsg, List<clsCaracterizacionXEntrada> lstEntrada,
            List<clsCaracterizacionXSalida> lstSalida)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblActividad] " +
                    "SET [Descripcion] = '{1}', [CargoResponsable] = '{2}', [idphva] = {3} WHERE [Id] = {0}",
                    objActividad.intId, objActividad.strDescripcion, objActividad.intCargoResponsable, objActividad.intIdphva);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);

                strConsulta = string.Format("DELETE FROM [Procesos].[tblActividadEntrada] "+
                                "WHERE IdActividad =  {0}",
                    objActividad.intId);
                cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                strConsulta = string.Format("DELETE FROM [Procesos].[tblActividadSalida] " +
                                "WHERE IdActividad =  {0}",
                    objActividad.intId);
                cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                foreach (clsCaracterizacionXEntrada objTemp in lstEntrada)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblActividadEntrada] ([IdActividad],[IdEntrada],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objActividad.intId, objTemp.intIdEntrada, objActividad.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }
                foreach (clsCaracterizacionXSalida objTemp in lstSalida)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblActividadSalida] ([IdActividad],[IdSalida],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objActividad.intId, objTemp.intIdSalida, objTemp.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la Actividad. [{0}]", ex.Message);
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
        /// <param name="objActividad">Informacion de la Actividad</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsActividad objActividad, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblActividad] SET [Estado] = {1} WHERE [Id] = {0}",
                    objActividad.intId, objActividad.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la Actividad. [{0}]", ex.Message);
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