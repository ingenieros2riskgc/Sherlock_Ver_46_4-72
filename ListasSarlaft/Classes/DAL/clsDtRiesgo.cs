using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtRiesgo
    {
        /// <summary>
        /// Realiza la consulta para traer todos los Riesgos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarRiesgos(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT RR.[IdRiesgo], RR.[Codigo], RR.[Nombre] NombreRiesgo, RR.[Descripcion], " +
                    "RR.[IdRegion], RR.[IdPais], RR.[IdDepartamento], RR.[IdCiudad], RR.[IdOficinaSucursal], " +
                    "RR.[IdCadenaValor], RR.[IdMacroproceso], RR.[IdProceso], RR.[IdSubProceso], RR.[IdActividad], " +
                    "RR.[IdClasificacionRiesgo], RR.[IdClasificacionGeneralRiesgo], RR.[IdClasificacionParticularRiesgo], " +
                    "RR.[IdFactorRiesgoOperativo], RR.[IdTipoRiesgoOperativo], RR.[IdTipoEventoOperativo], " +
                    "RR.[IdRiesgoAsociadoOperativo], RR.[ListaRiesgoAsociadoLA], RR.[ListaFactorRiesgoLAFT], " +
                    "RR.[ListaCausas], RR.[ListaConsecuencias], RR.[OcurrenciaEventoDesde], RR.[OcurrenciaEventoHasta], " +
                    "RR.[PerdidaEconomicaDesde], RR.[PerdidaEconomicaHasta], RR.[ListaTratamiento], " +
                    "RR.[IdResponsableRiesgo], RR.[IdProbabilidad], RR.[IdProbabilidadResidual], " +
                    "RR.[IdImpacto], RR.[IdImpactoResidual], RR.[Anulado], RR.[IdUsuario], RR.[FechaRegistro] " +
                    "FROM [Riesgos].[Riesgo] RR ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Riesgo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los Riesgos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarRiesgosMP(clsMacroproceso objMP, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT RR.[IdRiesgo], RR.[Codigo], RR.[Nombre] NombreRiesgo, RR.[Descripcion], " +
                    "RR.[IdRegion], RR.[IdPais], RR.[IdDepartamento], RR.[IdCiudad], RR.[IdOficinaSucursal], " +
                    "RR.[IdCadenaValor], RR.[IdMacroproceso], RR.[IdProceso], RR.[IdSubProceso], RR.[IdActividad], " +
                    "RR.[IdClasificacionRiesgo], RR.[IdClasificacionGeneralRiesgo], RR.[IdClasificacionParticularRiesgo], " +
                    "RR.[IdFactorRiesgoOperativo], RR.[IdTipoRiesgoOperativo], RR.[IdTipoEventoOperativo], " +
                    "RR.[IdRiesgoAsociadoOperativo], RR.[ListaRiesgoAsociadoLA], RR.[ListaFactorRiesgoLAFT], " +
                    "RR.[ListaCausas], RR.[ListaConsecuencias], RR.[OcurrenciaEventoDesde], RR.[OcurrenciaEventoHasta], " +
                    "RR.[PerdidaEconomicaDesde], RR.[PerdidaEconomicaHasta], RR.[ListaTratamiento], " +
                    "RR.[IdResponsableRiesgo], RR.[IdProbabilidad], RR.[IdProbabilidadResidual], " +
                    "RR.[IdImpacto], RR.[IdImpactoResidual], RR.[Anulado], RR.[IdUsuario], RR.[FechaRegistro] " +
                    "FROM [Riesgos].[Riesgo] RR " +
                    "WHERE RR.[IdMacroproceso] = {0}", objMP.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Riesgo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los Riesgos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarRiesgosP(clsProceso objProc,ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT RR.[IdRiesgo], RR.[Codigo], RR.[Nombre] NombreRiesgo, RR.[Descripcion], " +
                    "RR.[IdRegion], RR.[IdPais], RR.[IdDepartamento], RR.[IdCiudad], RR.[IdOficinaSucursal], " +
                    "RR.[IdCadenaValor], RR.[IdMacroproceso], RR.[IdProceso], RR.[IdSubProceso], RR.[IdActividad], " +
                    "RR.[IdClasificacionRiesgo], RR.[IdClasificacionGeneralRiesgo], RR.[IdClasificacionParticularRiesgo], " +
                    "RR.[IdFactorRiesgoOperativo], RR.[IdTipoRiesgoOperativo], RR.[IdTipoEventoOperativo], " +
                    "RR.[IdRiesgoAsociadoOperativo], RR.[ListaRiesgoAsociadoLA], RR.[ListaFactorRiesgoLAFT], " +
                    "RR.[ListaCausas], RR.[ListaConsecuencias], RR.[OcurrenciaEventoDesde], RR.[OcurrenciaEventoHasta], " +
                    "RR.[PerdidaEconomicaDesde], RR.[PerdidaEconomicaHasta], RR.[ListaTratamiento], " +
                    "RR.[IdResponsableRiesgo], RR.[IdProbabilidad], RR.[IdProbabilidadResidual], " +
                    "RR.[IdImpacto], RR.[IdImpactoResidual], RR.[Anulado], RR.[IdUsuario], RR.[FechaRegistro] " +
                    "FROM [Riesgos].[Riesgo] RR " +
                    "WHERE RR.[IdProceso] = {0}", objProc.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Riesgo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer todos los Riesgos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarRiesgosSP(clsSubproceso objSubp,ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT RR.[IdRiesgo], RR.[Codigo], RR.[Nombre] NombreRiesgo, RR.[Descripcion], " +
                    "RR.[IdRegion], RR.[IdPais], RR.[IdDepartamento], RR.[IdCiudad], RR.[IdOficinaSucursal], " +
                    "RR.[IdCadenaValor], RR.[IdMacroproceso], RR.[IdProceso], RR.[IdSubProceso], RR.[IdActividad], " +
                    "RR.[IdClasificacionRiesgo], RR.[IdClasificacionGeneralRiesgo], RR.[IdClasificacionParticularRiesgo], " +
                    "RR.[IdFactorRiesgoOperativo], RR.[IdTipoRiesgoOperativo], RR.[IdTipoEventoOperativo], " +
                    "RR.[IdRiesgoAsociadoOperativo], RR.[ListaRiesgoAsociadoLA], RR.[ListaFactorRiesgoLAFT], " +
                    "RR.[ListaCausas], RR.[ListaConsecuencias], RR.[OcurrenciaEventoDesde], RR.[OcurrenciaEventoHasta], " +
                    "RR.[PerdidaEconomicaDesde], RR.[PerdidaEconomicaHasta], RR.[ListaTratamiento], " +
                    "RR.[IdResponsableRiesgo], RR.[IdProbabilidad], RR.[IdProbabilidadResidual], " +
                    "RR.[IdImpacto], RR.[IdImpactoResidual], RR.[Anulado], RR.[IdUsuario], RR.[FechaRegistro] " +
                    "FROM [Riesgos].[Riesgo] RR " +
                    "WHERE RR.[IdSubProceso] = {0}", objSubp.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Riesgo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
    }
}