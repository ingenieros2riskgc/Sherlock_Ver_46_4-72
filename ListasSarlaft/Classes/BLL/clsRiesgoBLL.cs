using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsRiesgoBLL
    {
        /// <summary>
        /// Realiza la consulta del Riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsRiesgos> mtdConsultarRiesgos(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsRiesgos> lstRiesgo = new List<clsRiesgos>();
            clsDtRiesgo cDtRiesgo = new clsDtRiesgo();
            clsRiesgos objRiesgo = new clsRiesgos();
            #endregion Vars

            dtInfo = cDtRiesgo.mtdConsultarRiesgos(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRiesgo = new clsRiesgos(
                            Convert.ToInt32(dr["IdRiesgo"].ToString().Trim()),
                             dr["Codigo"].ToString().Trim(),
                             dr["NombreRiesgo"].ToString().Trim(),
                             dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdRegion"].ToString().Trim()),
                            Convert.ToInt32(dr["IdPais"].ToString().Trim()),
                            Convert.ToInt32(dr["IdDepartamento"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCiudad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdOficinaSucursal"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdSubProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdActividad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionGeneralRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionParticularRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdFactorRiesgoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoRiesgoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoEventoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdRiesgoAsociadoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdResponsableRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()),
                            Convert.ToInt32(dr["IdImpacto"].ToString().Trim()),
                            Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()),
                            dr["ListaRiesgoAsociadoLA"].ToString().Trim(),
                            dr["ListaFactorRiesgoLAFT"].ToString().Trim(),
                            dr["ListaCausas"].ToString().Trim(),
                            dr["ListaConsecuencias"].ToString().Trim(),
                            dr["OcurrenciaEventoHasta"].ToString().Trim(),
                            dr["OcurrenciaEventoDesde"].ToString().Trim(),
                            dr["PerdidaEconomicaDesde"].ToString().Trim(),
                            dr["PerdidaEconomicaHasta"].ToString().Trim(),
                            dr["ListaTratamiento"].ToString().Trim(),
                            dr["Anulado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["FechaRegistro"].ToString().Trim());

                        lstRiesgo.Add(objRiesgo);
                    }
                }
                else
                {
                    lstRiesgo = null;
                    //strErrMsg = "No hay información de Actividad.";
                }
            }
            else
                lstRiesgo = null;

            return lstRiesgo;
        }

        /// <summary>
        /// Realiza la consulta del Riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsRiesgos> mtdConsultarRiesgosMP(clsMacroproceso objMPIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsRiesgos> lstRiesgo = new List<clsRiesgos>();
            clsDtRiesgo cDtRiesgo = new clsDtRiesgo();
            clsRiesgos objRiesgo = new clsRiesgos();
            #endregion Vars

            dtInfo = cDtRiesgo.mtdConsultarRiesgosMP(objMPIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRiesgo = new clsRiesgos(
                            Convert.ToInt32(dr["IdRiesgo"].ToString().Trim()),
                             dr["Codigo"].ToString().Trim(),
                             dr["NombreRiesgo"].ToString().Trim(),
                             dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdRegion"].ToString().Trim()),
                            Convert.ToInt32(dr["IdPais"].ToString().Trim()),
                            Convert.ToInt32(dr["IdDepartamento"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCiudad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdOficinaSucursal"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdSubProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdActividad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionGeneralRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionParticularRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdFactorRiesgoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoRiesgoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoEventoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdRiesgoAsociadoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdResponsableRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()),
                            Convert.ToInt32(dr["IdImpacto"].ToString().Trim()),
                            Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()),
                            dr["ListaRiesgoAsociadoLA"].ToString().Trim(),
                            dr["ListaFactorRiesgoLAFT"].ToString().Trim(),
                            dr["ListaCausas"].ToString().Trim(),
                            dr["ListaConsecuencias"].ToString().Trim(),
                            dr["OcurrenciaEventoHasta"].ToString().Trim(),
                            dr["OcurrenciaEventoDesde"].ToString().Trim(),
                            dr["PerdidaEconomicaDesde"].ToString().Trim(),
                            dr["PerdidaEconomicaHasta"].ToString().Trim(),
                            dr["ListaTratamiento"].ToString().Trim(),
                            dr["Anulado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["FechaRegistro"].ToString().Trim());

                        lstRiesgo.Add(objRiesgo);
                    }
                }
                else
                    lstRiesgo = null;
            }
            else
                lstRiesgo = null;

            return lstRiesgo;
        }

        /// <summary>
        /// Realiza la consulta del Riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsRiesgos> mtdConsultarRiesgosP(clsProceso objProcIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsRiesgos> lstRiesgo = new List<clsRiesgos>();
            clsDtRiesgo cDtRiesgo = new clsDtRiesgo();
            clsRiesgos objRiesgo = new clsRiesgos();
            #endregion Vars

            dtInfo = cDtRiesgo.mtdConsultarRiesgosP(objProcIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRiesgo = new clsRiesgos(
                            Convert.ToInt32(dr["IdRiesgo"].ToString().Trim()),
                             dr["Codigo"].ToString().Trim(),
                             dr["NombreRiesgo"].ToString().Trim(),
                             dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdRegion"].ToString().Trim()),
                            Convert.ToInt32(dr["IdPais"].ToString().Trim()),
                            Convert.ToInt32(dr["IdDepartamento"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCiudad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdOficinaSucursal"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdSubProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdActividad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionGeneralRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionParticularRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdFactorRiesgoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoRiesgoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoEventoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdRiesgoAsociadoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdResponsableRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()),
                            Convert.ToInt32(dr["IdImpacto"].ToString().Trim()),
                            Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()),
                            dr["ListaRiesgoAsociadoLA"].ToString().Trim(),
                            dr["ListaFactorRiesgoLAFT"].ToString().Trim(),
                            dr["ListaCausas"].ToString().Trim(),
                            dr["ListaConsecuencias"].ToString().Trim(),
                            dr["OcurrenciaEventoHasta"].ToString().Trim(),
                            dr["OcurrenciaEventoDesde"].ToString().Trim(),
                            dr["PerdidaEconomicaDesde"].ToString().Trim(),
                            dr["PerdidaEconomicaHasta"].ToString().Trim(),
                            dr["ListaTratamiento"].ToString().Trim(),
                            dr["Anulado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["FechaRegistro"].ToString().Trim());

                        lstRiesgo.Add(objRiesgo);
                    }
                }
                else
                    lstRiesgo = null;
            }
            else
                lstRiesgo = null;

            return lstRiesgo;
        }

        /// <summary>
        /// Realiza la consulta del Riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsRiesgos> mtdConsultarRiesgosSP(clsSubproceso objSubpIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsRiesgos> lstRiesgo = new List<clsRiesgos>();
            clsDtRiesgo cDtRiesgo = new clsDtRiesgo();
            clsRiesgos objRiesgo = new clsRiesgos();
            #endregion Vars

            dtInfo = cDtRiesgo.mtdConsultarRiesgosSP(objSubpIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRiesgo = new clsRiesgos(
                            Convert.ToInt32(dr["IdRiesgo"].ToString().Trim()),
                             dr["Codigo"].ToString().Trim(),
                             dr["NombreRiesgo"].ToString().Trim(),
                             dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdRegion"].ToString().Trim()),
                            Convert.ToInt32(dr["IdPais"].ToString().Trim()),
                            Convert.ToInt32(dr["IdDepartamento"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCiudad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdOficinaSucursal"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdSubProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdActividad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionGeneralRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdClasificacionParticularRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdFactorRiesgoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoRiesgoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoEventoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdRiesgoAsociadoOperativo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdResponsableRiesgo"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()),
                            Convert.ToInt32(dr["IdImpacto"].ToString().Trim()),
                            Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()),
                            dr["ListaRiesgoAsociadoLA"].ToString().Trim(),
                            dr["ListaFactorRiesgoLAFT"].ToString().Trim(),
                            dr["ListaCausas"].ToString().Trim(),
                            dr["ListaConsecuencias"].ToString().Trim(),
                            dr["OcurrenciaEventoHasta"].ToString().Trim(),
                            dr["OcurrenciaEventoDesde"].ToString().Trim(),
                            dr["PerdidaEconomicaDesde"].ToString().Trim(),
                            dr["PerdidaEconomicaHasta"].ToString().Trim(),
                            dr["ListaTratamiento"].ToString().Trim(),
                            dr["Anulado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["FechaRegistro"].ToString().Trim());

                        lstRiesgo.Add(objRiesgo);
                    }
                }
                else
                    lstRiesgo = null;
            }
            else
                lstRiesgo = null;

            return lstRiesgo;
        }
    }
}