using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsInfoGeneralDAL
    {
        public bool mtdConsultarReporteInfoGeneralRespuestaServicio(ref DataTable dtCaracOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdRespuestaServicio],[NumClientesEncuestados],[PromedioRespuesta],[FechaRegistro]"
                    + " FROM [dbo].[vwRespuestaServicio]"
                    + " WHERE [FechaRegistro] between '{0} 00:00:00' and '{1} 23:59:59'"
                    , fechaInicial, fechaFinal);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar de respuesta de la evaluacion de servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCriterioEvaluacionServicio(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [RangoInicial],[RangoFinal] ,[DescripcionAprobacion] "
                    + " FROM [Procesos].[tblCriterioServicio]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar de respuesta de la evaluacion de servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarReporteInfoGeneralIndicadores(ref DataTable dtCaracOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdIndicador],[NombreIndicador],[DescripcionIndicador],[IdPeriodicidad],[IdObjCalidad]"
                + ",[IdProcesoIndicador],[IdProceso],[Proceso],[NumNoConformidad],[NumNoConformidadCierre],[NumAuditoria],[NumAuditoriaCumplida],[FechaRegistro],[Meta]"
                    + " FROM [dbo].[vwInformacionGeneral]"
                    + " WHERE [FechaRegistro] between '{0} 00:00:00' and '{1} 23:59:59' and EstadoCadena = 1"
                    , fechaInicial, fechaFinal);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar de indicadores para el reporte. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarReporteNoConformidadProceso(ref DataTable dtCaracOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(1) as NumNoConformidad"
                    + " FROM [dbo].[vwControlNoConformidad]"
                    + " where FechaRegistro between '{0} 00:00:00' and '{1} 23:59:59'"
                    , fechaInicial, fechaFinal);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar de indicadores para el reporte. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarReporteNoConformidadCierre(ref DataTable dtCaracOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(1) as NumNoConformidadCierre"
                    + " FROM [dbo].[vwControlNoConformidad]"
                    + " where [FechaFinal] = '' and FechaRegistro between '{0} 00:00:00' and '{1} 23:59:59'"
                    , fechaInicial, fechaFinal);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar de indicadores para el reporte. [{0}]", ex.Message);
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