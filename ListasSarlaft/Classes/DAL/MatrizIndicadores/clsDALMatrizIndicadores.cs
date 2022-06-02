using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALMatrizIndicadores
    {
        /// <summary>
        /// Realiza la consulta para traer todas las Actividades
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarMatriz(ref string strErrMsg, ref int year)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [PoliticaCalidad],[IdIndicador],[DescripcionObjetivo],[NombreIndicador],[DescripcionInicador],[ProcesoIndicador]"+
                ",[Meta],[NombrePeriodo],[IdPeriodicidad],[IdObjCalidad],[IdProcesoIndicador],[Estado],[FechaRegistro],[IdUsuario],[NombreUsuario]"+
                ",[IdTipoProceso],[EstadoCadena] FROM [dbo].[vwMatrizIndicadores] where YEAR(FechaRegistro) ={0}", year);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la matriz de indicadores. [{0}]", ex.Message);
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
        public DataTable mtdConsultarIndicadorCumplido(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [PoliticaCalidad],[IdIndicador],[DescripcionObjetivo],[NombreIndicador],[DescripcionInicador],[ProcesoIndicador]" +
                ",[Meta],[NombrePeriodo],[IdPeriodicidad],[IdObjCalidad],[IdProcesoIndicador],[Estado],[FechaRegistro],[IdUsuario],[NombreUsuario]" +
                ",[IdTipoProceso],[EstadoCadena] FROM [dbo].[vwMatrizIndicadores] where FechaRegistro between CONVERT (datetime, '{0} 00:00:00', 120) and CONVERT (datetime, '{1} 23:59:59', 120)", fechaInicial, fechaFinal);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la matriz de indicadores. [{0}]", ex.Message);
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
        public DataTable mtdConsultarIndicadorPorProceso(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal, ref int proceso, ref int IdTipoProceso)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [PoliticaCalidad],[IdIndicador],[DescripcionObjetivo],[NombreIndicador],[DescripcionInicador],[ProcesoIndicador]" +
                ",[Meta],[NombrePeriodo],[IdPeriodicidad],[IdObjCalidad],[IdProcesoIndicador],[Estado],[FechaRegistro],[IdUsuario],[NombreUsuario]" +
                ",[IdTipoProceso],[EstadoCadena],[Responsable] FROM [dbo].[vwMatrizIndicadores] where FechaRegistro between '{0} 00:00:00' and '{1} 23:59:59' and [IdProceso] = {2} and [IdTipoProceso] = {3}", fechaInicial, fechaFinal, proceso, IdTipoProceso);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la matriz de indicadores. [{0}]", ex.Message);
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
        public DataTable mtdConsultarIndicadorIncumplido(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [PoliticaCalidad],[IdIndicador],[DescripcionObjetivo],[NombreIndicador],[DescripcionInicador],[ProcesoIndicador]" +
                ",[Meta],[NombrePeriodo],[IdPeriodicidad],[IdObjCalidad],[IdProcesoIndicador],[Estado],[FechaRegistro],[IdUsuario],[NombreUsuario]" +
                ",[IdTipoProceso],[EstadoCadena] FROM [dbo].[vwMatrizIndicadores] where FechaRegistro between '{0} 00:00:00' and '{1} 23:59:59'", fechaInicial, fechaFinal);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la matriz de indicadores. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
    }
}