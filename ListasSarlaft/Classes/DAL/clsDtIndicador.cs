using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtIndicador
    {
        /// <summary>
        /// Realiza la consulta para traer el Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarIndicador(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT  [Id],[NombreIndicador],[Descripcion],[IdPeriodicidad],[IdObjCalidad],[IdProcesoIndicador]" +
                    ",[DescObjetivo],[Meta],[Estado],[FechaRegistro],[IdUsuario],[NombreUsuario],[IdTipoProceso],[EstadoCadena], [Proceso] " +
                    "FROM [dbo].[vwIndicador]" +
                    "where  [EstadoCadena] = 1");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Indicador. No hay datos para consultar");
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer el Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarIndicadorById(ref string strErrMsg, int intIdIndicador)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT  [Id],[NombreIndicador],[Descripcion],[IdPeriodicidad],[IdObjCalidad],[IdProcesoIndicador]" +
                    ",[DescObjetivo],[Meta],[Estado],[FechaRegistro],[IdUsuario],[NombreUsuario],[IdTipoProceso],[EstadoCadena], [Proceso] " +
                    "FROM  [dbo].[vwIndicador]" +
                    "where  [EstadoCadena] = 1 and Id ={0}", intIdIndicador);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Indicador. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer el Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarIndicador(int intTipoProceso, int intProceso, ref string strErrMsg, int intPeriodoAnual)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PIn.[Id], PIn.[NombreIndicador], PIn.[Descripcion], PIn.[IdPeriodicidad], PPer.[NombrePeriodo], " +"\n"+
                    "PIn.[IdObjCalidad], PIn.[IdProcesoIndicador], POC.[Descripcion] DescObjetivo, PIn.[Meta], PIn.[Estado], " + "\n" +
                    "PIn.[FechaRegistro], PIn.[IdUsuario], LU.[Usuario] NombreUsuario " + "\n" +
                    "FROM [Procesos].[tblIndicador] PIn " + "\n" +
                    "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON  PIn.[IdProcesoIndicador]= PPI.Id " + "\n" +
                    "INNER JOIN [Procesos].[tblPeriodicidad] PPer ON PIn.[IdPeriodicidad] = PPer.[ID] " + "\n" +
                    "INNER JOIN [Listas].[Usuarios] LU ON PIn.[IdUsuario] = LU.[IdUsuario] " + "\n" +
                    "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " + "\n" +
                    "INNER JOIN [Procesos].[tblDetalleVariable] pdv on pdv.IdIndicador = PIn.Id"+ "\n" +
                    "WHERE PPI.[IdTipoProceso] = {0} AND PPI.[IdProceso] = {1} AND pdv.PeriodoAnual = {2}", intTipoProceso, intProceso, intPeriodoAnual);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Indicador. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer el Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarIndicador(int intTipoProceso, int intProceso, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PIn.[Id], PIn.[NombreIndicador], PIn.[Descripcion], PIn.[IdPeriodicidad], PPer.[NombrePeriodo], " + "\n" +
                    "PIn.[IdObjCalidad], PIn.[IdProcesoIndicador], POC.[Descripcion] DescObjetivo, PIn.[Meta], PIn.[Estado], " + "\n" +
                    "PIn.[FechaRegistro], PIn.[IdUsuario], LU.[Usuario] NombreUsuario " + "\n" +
                    "FROM [Procesos].[tblIndicador] PIn " + "\n" +
                    "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON  PIn.[IdProcesoIndicador]= PPI.Id " + "\n" +
                    "INNER JOIN [Procesos].[tblPeriodicidad] PPer ON PIn.[IdPeriodicidad] = PPer.[ID] " + "\n" +
                    "INNER JOIN [Listas].[Usuarios] LU ON PIn.[IdUsuario] = LU.[IdUsuario] " + "\n" +
                    "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " + "\n" +
                    "WHERE PPI.[IdTipoProceso] = {0} AND PPI.[IdProceso] = {1} ", intTipoProceso, intProceso);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Indicador. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la insercion del Indicador.
        /// </summary>
        /// <param name="objProceso">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdInsertarIndicador(clsIndicador objIndicador, ref int intIdIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInfo = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblIndicador] " +
                    "([NombreIndicador],[Descripcion], [IdPeriodicidad],[IdObjCalidad],[IdProcesoIndicador],[Meta],[Estado],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}','{1}', {2}, {3}, {4}, {5}, {6}, GETDATE(), {7}) " +
                    "SELECT SCOPE_IDENTITY() ",objIndicador.strNombreIndicador,
                    objIndicador.strDescripcion, objIndicador.intIdPeriodicidad, objIndicador.intIdObjetivoCalidad, 
                    objIndicador.intIdProcesoIndicador, objIndicador.intMeta, objIndicador.booEstado == false ? 0 : 1, 
                    objIndicador.intIdUsuario);

                cDatabase.mtdConectarSql();
                dtInfo = cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        intIdIndicador = Convert.ToInt32(dtInfo.Rows[0][0].ToString());

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Indicador. [{0}]", ex.Message);
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
        /// <param name="objObjetivo">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsIndicador objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblIndicador]  SET [Estado] = {1}  WHERE [Id] = {0}",
                    objIndicador.intId, objIndicador.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Indicador. [{0}]", ex.Message);
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
        /// <param name="objIndicador">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarIndicador(clsIndicador objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblIndicador] " +
                    "SET [NombreIndicador] = '{1}', [Descripcion] = '{2}', [IdPeriodicidad] = {3}, [IdObjCalidad] = {4}, [IdProcesoIndicador] = {5}, [Meta] = {6} " +
                    "WHERE [Id] = {0}",
                    objIndicador.intId, objIndicador.strNombreIndicador, objIndicador.strDescripcion, objIndicador.intIdPeriodicidad, 
                    objIndicador.intIdObjetivoCalidad, objIndicador.intIdProcesoIndicador, 
                    clsUtilidades.mtdQuitarComasAPuntos(objIndicador.intMeta.ToString()));

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Indicador. [{0}]", ex.Message);
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