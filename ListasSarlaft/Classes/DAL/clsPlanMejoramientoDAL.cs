using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsPlanMejoramientoDAL
    {
        public bool mtdInsertarPlanMejoramiento(clsPlanDeMejoramiento objPlan, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblPlanMejoramiento] ([IdMacroProceso]," + 
                    "[DescripcionActividad],[PeriodoEvaluarInicial],[PeriodoEvaluarFinal],[PlanMejoramiento]" + 
                    ",[Recursos],[FechaRegistro],[IdUsuario],[IdTipoProceso])" + 
                    "VALUES({0},'{1}','{2}','{3}','{4}','{5}',{6},{7},{8}) ", 
                    objPlan.intIdMacroProceso, objPlan.strDescripcionActividad, objPlan.dtPeriodoEvaluarInicial, 
                    objPlan.dtPeriodoEvaluarFinal, objPlan.strPlanMejoramiento, objPlan.strRecursos, "GETDATE()", 
                    objPlan.intIdUsuario, objPlan.intIdTipoProceso);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdInsertarPlanMejoramientoProceso(clsPlanDeMejoramiento objPlan, ref string strErrMsg, int IdPlan)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblPlanMejoramientoProceso] ([IdPlanMejoramiento],[IdProceso],[IdTipoProceso])" +
                    "VALUES({0},{1},{2}) ",
                    IdPlan, objPlan.intIdMacroProceso, objPlan.intIdTipoProceso);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdPlanMejoramiento(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblPlanMejoramiento]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar Id Plan Mejoramiento. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarDetallePlanMejoramiento(clsDetallePlanMejoramiento objDetallePlan, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblDetallePlanMejoramiento] ([IdPlanMejoramiento]," + 
                    "[IdMacroproceso],[Actividad],[FechaProgramado],[FechaRealizado]" + 
                    ",[Responsable],[FechaRegistro],[IdUsuario],[Area],[Seguimiento])" + 
                    "VALUES({0},{1},'{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}') ", 
                    objDetallePlan.intIdPlanDeMejoramiento, objDetallePlan.intIdMacroProceso, objDetallePlan.strActividad, 
                    objDetallePlan.dtFechaProgramada, objDetallePlan.dtFechaRealizada, objDetallePlan.strResponsable, "GETDATE()", 
                    objDetallePlan.intIdUsuario, objDetallePlan.strArea, objDetallePlan.strSeguimiento);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarPlanMejoramiento(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [Id],[IdMacroProceso],[DescripcionActividad],[PeriodoEvaluarInicial],[PeriodoEvaluarFinal],[PlanMejoramiento],[Recursos]" +
                ",[FechaRegistro],[IdUsuario],[Nombre],[Usuario],[IdTipoProceso]" +
                " FROM [dbo].[vwPlanMejoramiento]");

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public bool mtdConsultarDetallePlanMejoramiento(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdPlanMejoramiento)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [Id],[IdPlanMejoramiento],[IdMacroproceso],[Actividad],[FechaProgramado],[FechaRealizado],[Responsable],[NombreHijo],[Area],[Seguimiento]" +
                " FROM [dbo].[vwDetallePlanMejoramiento]" +
                " where [IdPlanMejoramiento] = {0}", IdPlanMejoramiento);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public bool mtdUpdateDetallePlanMejoramiento(clsDetallePlanMejoramiento objDetallePlan, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblDetallePlanMejoramiento] SET [FechaRealizado] = '{1}', [Seguimiento] = '{2}'" +
                    " WHERE IdPlanMejoramiento = {0} ",
                    objDetallePlan.intIdPlanDeMejoramiento, objDetallePlan.dtFechaRealizada, objDetallePlan.strSeguimiento);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
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