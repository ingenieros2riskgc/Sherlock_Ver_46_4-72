using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtSeguimientoIndicador
    {
        /// <summary>
        /// Metodo que realiza la consulta del seguimiento en la BD
        /// </summary>
        /// <param name="objIndicador">Objeto con la informacion del indicador</param>
        /// <param name="dtInformacion">Objeto con la informacion consultada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la trasaccion</returns>
        public bool mtdConsultarSeguimiento(clsIndicador objIndicador, ref DataTable dtInformacion, ref string strErrMsg,
            int periodoAnual)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PSI.[Id] IdSegIndicador, PSI.[CargoResponsable], PSI.[IdIndicador], PDSi.[Id] IdDetSegIndicador,  " +
                    "PDSi.[IdDetPeriodo], PDSi.[FechaDescAnalisis], PDSi.[FechaAccionCorrectiva], " +
                    "PDSi.[DescripcionAnalisis], PDSi.[DescripcionAccionCorrectiva], PDSi.[FechaRegistro], PDSi.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblSeguimientoIndicador] PSI " +
                    "INNER JOIN [Procesos].[tblDetalleSegIndicador] PDSi ON PDSi.[IdSegIndicador] = PSI.[Id] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PDSi.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE  PSI.[IdIndicador] = {0} and PDSi.PeriodoAnual = {1} ",
                    objIndicador.intId, periodoAnual);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el seguimiento del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Metodo que permite actualizar el seguimiento
        /// </summary>
        /// <param name="objDetSegIndicador">Objeto con la informacion del seguimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la trasaccion</returns>
        public bool mtdActualizarSeguimiento(clsDetalleSeguimientoIndicador objDetSegIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblDetalleSegIndicador] " +
                    "SET [IdDetPeriodo] = {1}, [DescripcionAnalisis] = '{2}', [DescripcionAccionCorrectiva] = '{3}', [FechaDescAnalisis] = GETDATE(), [FechaAccionCorrectiva] = GETDATE() " +
                    "WHERE [Id] = {0} ",
                    objDetSegIndicador.intId, objDetSegIndicador.intIdDetPeriodo, objDetSegIndicador.strDescripcionAnalisis, objDetSegIndicador.strDescripcionAccionCorrectiva);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el seguimiento del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Metodo para insertar el seguimiento
        /// </summary>
        /// <param name="objDetSegIndicador">Objeto con la informacion del seguimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la trasaccion</returns>
        public bool mtdInsertarSeguimiento(clsDetalleSeguimientoIndicador objDetSegIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) ExisteDetalle FROM [Procesos].[tblDetalleSegIndicador] PDSi " +
                    "INNER JOIN  [Procesos].[tblSeguimientoIndicador] PSI ON PDSi.[IdSegIndicador] = PSI.[Id] " +
                    "WHERE PSI.[IdIndicador] = {0} AND PDSi.[IdDetPeriodo] = {1} and PDSi.PeriodoAnual={2}",
                    objDetSegIndicador.intIdIndicador, objDetSegIndicador.intIdDetPeriodo, objDetSegIndicador.intPeriodoAnual);

                cDatabase.mtdConectarSql();
                dtInfo = cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                if (Convert.ToInt32(dtInfo.Rows[0][0].ToString().Trim()) == 0)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblSeguimientoIndicador]([CargoResponsable],[IdIndicador]) " +
                        "VALUES({0},{1}) SELECT SCOPE_IDENTITY() ",
                        objDetSegIndicador.intIdUsuario, objDetSegIndicador.intIdIndicador);

                    dtInfo = cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                    if (dtInfo != null)
                        if (dtInfo.Rows.Count > 0)
                        {
                            int intIdSeg = Convert.ToInt32(dtInfo.Rows[0][0].ToString());

                            strConsulta = string.Format("INSERT INTO [Procesos].[tblDetalleSegIndicador] " +
                                "([IdSegIndicador],[IdDetPeriodo],[DescripcionAnalisis],[DescripcionAccionCorrectiva],[IdUsuario], " +
                                "[FechaDescAnalisis],[FechaAccionCorrectiva],[FechaRegistro],[PeriodoAnual]) " +
                                "VALUES ({0},{1},'{2}','{3}',{4},GETDATE(),GETDATE(),GETDATE(),{5}) ",
                                intIdSeg, objDetSegIndicador.intIdDetPeriodo, objDetSegIndicador.strDescripcionAnalisis,
                                objDetSegIndicador.strDescripcionAccionCorrectiva, objDetSegIndicador.intIdUsuario,
                                objDetSegIndicador.intPeriodoAnual);

                            cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                        }

                    booResult = true;
                }
                else
                {
                    booResult = false;
                    strErrMsg = string.Format("El periodo seleccionado ya se encuentra registrado.");

                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al insertar el seguimiento del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Metodo que permite consultar la informacion del cuadro de mando
        /// </summary>
        /// <param name="objProcInd">Objeto con la informacion del proceso del indicador</param>
        /// <param name="objIndicador">Objeto con la informacion del indicador</param>
        /// <param name="dtInformacion">Objeto con la informacion consultada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la trasaccion</returns>
        public bool mtdConsultarInfoCuadro(clsProcesoIndicador objProcInd, clsIndicador objIndicador,
            ref DataTable dtInformacion, ref string strErrMsg, int periodoAnual)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                #region Consultas por proceso
                switch (objProcInd.intIdTipoProceso)
                {
                    case 1:
                        strConsulta = string.Format("SELECT PIn.[Id] IdIndicador, PDP.[Id] IdDetPeriodo ,PDP.[Nombre] Periodo, " +
                            "PV.[Id] IdVariable, ISNULL(PV.[Descripcion],'') Variable, PDV.[Valor], PIn.[Meta] " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Macroproceso] P ON P.[IdMacroProceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "WHERE PPI.[IdTipoProceso] = 1 AND PIn.[Id] = {0} and PDV.PeriodoAnual = {1}",
                            objIndicador.intId, periodoAnual);
                        break;
                    case 2:
                        strConsulta = string.Format("SELECT PIn.[Id] IdIndicador, PDP.[Id] IdDetPeriodo ,PDP.[Nombre] Periodo, " +
                            "PV.[Id] IdVariable, ISNULL(PV.[Descripcion],'') Variable, PDV.[Valor], PIn.[Meta] " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Proceso] P ON P.[IdProceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            " WHERE PPI.[IdTipoProceso] = 2 AND PIn.[Id] = {0} and PDV.PeriodoAnual = {1} ORDER BY IdDetPeriodo",
                            objIndicador.intId, periodoAnual);
                        break;
                    case 3:
                        strConsulta = string.Format("SELECT PIn.[Id] IdIndicador, PDP.[Id] IdDetPeriodo ,PDP.[Nombre] Periodo, " +
                            "PV.[Id] IdVariable, ISNULL(PV.[Descripcion],'') Variable, PDV.[Valor], PIn.[Meta] " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Subproceso] P ON P.[IdSubproceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "WHERE PPI.[IdTipoProceso] = 3 AND PIn.[Id] = {0} and PDV.PeriodoAnual = {1} ",
                            objIndicador.intId,periodoAnual);
                        break;
                }
                #endregion

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el seguimiento del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarInfoCuadro(clsProcesoIndicador objProcInd, clsIndicador objIndicador,
            ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                #region Consultas por proceso
                switch (objProcInd.intIdTipoProceso)
                {
                    case 1:
                        strConsulta = string.Format("SELECT PIn.[Id] IdIndicador, PDP.[Id] IdDetPeriodo ,PDP.[Nombre] Periodo, " +
                            "PV.[Id] IdVariable, ISNULL(PV.[Descripcion],'') Variable, PDV.[Valor], PIn.[Meta] " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Macroproceso] P ON P.[IdMacroProceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "WHERE PPI.[IdTipoProceso] = 1 AND PIn.[Id] = {0}",
                            objIndicador.intId);
                        break;
                    case 2:
                        strConsulta = string.Format("SELECT PIn.[Id] IdIndicador, PDP.[Id] IdDetPeriodo ,PDP.[Nombre] Periodo, " +
                            "PV.[Id] IdVariable, ISNULL(PV.[Descripcion],'') Variable, PDV.[Valor], PIn.[Meta] " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Proceso] P ON P.[IdProceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            " WHERE PPI.[IdTipoProceso] = 2 AND PIn.[Id] = {0}  ORDER BY IdDetPeriodo",
                            objIndicador.intId);
                        break;
                    case 3:
                        strConsulta = string.Format("SELECT PIn.[Id] IdIndicador, PDP.[Id] IdDetPeriodo ,PDP.[Nombre] Periodo, " +
                            "PV.[Id] IdVariable, ISNULL(PV.[Descripcion],'') Variable, PDV.[Valor], PIn.[Meta] " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Subproceso] P ON P.[IdSubproceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "WHERE PPI.[IdTipoProceso] = 3 AND PIn.[Id] = {0}  ",
                            objIndicador.intId);
                        break;
                }
                #endregion

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el seguimiento del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar la informacion de la cabecera del cuadro de mando
        /// </summary>
        /// <param name="objProcInd">Objeto con la informacion del proceso del indicador</param>
        /// <param name="objIndicador">Objeto con la informacion del indicador</param>
        /// <param name="dtInformacion">Objeto con la informacion consultada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la trasaccion</returns>
        public bool mtdConsultarCabCuadro(clsProcesoIndicador objProcInd, clsIndicador objIndicador,
            ref DataTable dtInformacion, ref string strErrMsg, int periodoAnual)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                #region Consultas por proceso
                switch (objProcInd.intIdTipoProceso)
                {
                    case 1: 
                        strConsulta = string.Format("SELECT " +
                            "PIn.[Id] IdIndicador, PIn.[Descripcion] Estrategia, PIn.[Meta], PDP.[Id] IdDetPeriodo, PDP.[Nombre] Periodo, PPer.[NombrePeriodo]," +
                            "POC.[Descripcion] ObjCalidad, P.[Nombre] Proceso, PJO.[NombreHijo] Responsable " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Macroproceso] P ON P.[IdMacroProceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PJO.[idHijo] = P.Responsable " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PPer ON PPer.[Id] = PIn.IdPeriodicidad " +
                            "WHERE PPI.[IdTipoProceso] = 1 AND PIn.[Id] = {0} and PDV.PeriodoAnual = {1}",
                             objIndicador.intId, periodoAnual);
                        break;
                    case 2:
                        strConsulta = string.Format("SELECT " +
                            "PIn.[Id] IdIndicador, PIn.[Descripcion] Estrategia, PIn.[Meta], PDP.[Id] IdDetPeriodo, PDP.[Nombre] Periodo, " +
                            "POC.[Descripcion] ObjCalidad, P.[Nombre] Proceso, PJO.[NombreHijo] Responsable " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Proceso] P ON P.[IdProceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PJO.[idHijo] = P.[IdHijo] " +
                            "WHERE PPI.[IdTipoProceso] = 2 AND PIn.[Id] = {0} and PDV.PeriodoAnual = {1}",
                             objIndicador.intId, periodoAnual);
                        break;
                    case 3:
                        strConsulta = string.Format("SELECT " +
                            "PIn.[Id] IdIndicador, PIn.[Descripcion] Estrategia, PIn.[Meta], PDP.[Id] IdDetPeriodo, PDP.[Nombre] Periodo, " +
                            "POC.[Descripcion] ObjCalidad, P.[Nombre] Proceso, PJO.[NombreHijo] Responsable " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Subproceso] P ON P.[IdSubproceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PJO.[idHijo] = P.[IdHijo] " +
                            "WHERE PPI.[IdTipoProceso] = 3 AND PIn.[Id] = {0} and PDV.PeriodoAnual = {1}",
                             objIndicador.intId, periodoAnual);
                        break;
                }
                #endregion

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el seguimiento del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCabCuadro(clsProcesoIndicador objProcInd, clsIndicador objIndicador,
            ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                #region Consultas por proceso
                switch (objProcInd.intIdTipoProceso)
                {
                    case 1:
                        strConsulta = string.Format("SELECT " +
                            "PIn.[Id] IdIndicador, PIn.[Descripcion] Estrategia, PIn.[Meta], PDP.[Id] IdDetPeriodo, PDP.[Nombre] Periodo, PPer.[NombrePeriodo]," +
                            "POC.[Descripcion] ObjCalidad, P.[Nombre] Proceso, PJO.[NombreHijo] Responsable " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Macroproceso] P ON P.[IdMacroProceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PJO.[idHijo] = P.Responsable " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PPer ON PPer.[Id] = PIn.IdPeriodicidad " +
                            "WHERE PPI.[IdTipoProceso] = 1 AND PIn.[Id] = {0} ",
                             objIndicador.intId);
                        break;
                    case 2:
                        strConsulta = string.Format("SELECT " +
                            "PIn.[Id] IdIndicador, PIn.[Descripcion] Estrategia, PIn.[Meta], PDP.[Id] IdDetPeriodo, PDP.[Nombre] Periodo, " +
                            "POC.[Descripcion] ObjCalidad, P.[Nombre] Proceso, PJO.[NombreHijo] Responsable " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Proceso] P ON P.[IdProceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PJO.[idHijo] = P.[IdHijo] " +
                            "WHERE PPI.[IdTipoProceso] = 2 AND PIn.[Id] = {0}",
                             objIndicador.intId);
                        break;
                    case 3:
                        strConsulta = string.Format("SELECT " +
                            "PIn.[Id] IdIndicador, PIn.[Descripcion] Estrategia, PIn.[Meta], PDP.[Id] IdDetPeriodo, PDP.[Nombre] Periodo, " +
                            "POC.[Descripcion] ObjCalidad, P.[Nombre] Proceso, PJO.[NombreHijo] Responsable " +
                            "FROM [Procesos].[tblIndicador] PIn " +
                            "INNER JOIN [Procesos].[tblProcesoIndicador] PPI ON PPI.[Id] =  PIn.[IdProcesoIndicador] " +
                            "INNER JOIN [Procesos].[Subproceso] P ON P.[IdSubproceso] = PPI.[IdProceso] " +
                            "INNER JOIN [Procesos].[tblPeriodicidad] PP ON PP.[Id] = PIn.[IdPeriodicidad] " +
                            "INNER JOIN [Procesos].[tblDetallePeriodo] PDP ON PDP.[IdPeriodo] = PP.[Id] " +
                            "INNER JOIN [Procesos].[tblObjetivoCalidad] POC ON POC.[Id] = PIn.[IdObjCalidad] " +
                            "INNER JOIN [Procesos].[tblDetalleVariable] PDV ON PDV.[IdIndicador] = PIn.[Id] AND PDV.[IdDetallePeriodo] = PDP.[Id] " +
                            "INNER JOIN [Procesos].[tblVariable] PV ON PV.[Id] = PDV.[IdVariable] " +
                            "LEFT JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON PJO.[idHijo] = P.[IdHijo] " +
                            "WHERE PPI.[IdTipoProceso] = 3 AND PIn.[Id] = {0} ",
                             objIndicador.intId);
                        break;
                }
                #endregion

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el seguimiento del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}