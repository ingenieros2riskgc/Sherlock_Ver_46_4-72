using ListasSarlaft.Classes.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLCuadroMandoIndicadores
    {
        /// <summary>
        /// Metodo que permite consultar Info Reporte Indicador - Riesgos 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteIndicadoresRiesgos(ref string strErrMsg, ref List<clsDTOCuadroMandoIndicadores> lstInfo)
        {
            bool booResult = false;
            clsDALCuadroMandoIndicadores cDALreporte = new clsDALCuadroMandoIndicadores();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteIndicadoresRiesgos(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoIndicadores objReporte = new clsDTOCuadroMandoIndicadores();
                        objReporte.intIdRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim());
                        objReporte.strNombreIndicador = dr["NombreIndicador"].ToString().Trim();
                        objReporte.strObjetivoIndicador = dr["ObjetivoIndicador"].ToString().Trim();
                        objReporte.intIdRiesgoAsociado = Convert.ToInt32(dr["IdRiesgoAsociado"].ToString().Trim());
                        objReporte.intIdProbabilidad = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                        objReporte.intIdImpacto = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                        objReporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                        if (objReporte.strNombreRiesgo == "Extremo")
                            objReporte.strColor = "Rojo";
                        if (objReporte.strNombreRiesgo == "Alto")
                            objReporte.strColor = "Naranja";
                        if (objReporte.strNombreRiesgo == "Moderado")
                            objReporte.strColor = "Amarillo";
                        if (objReporte.strNombreRiesgo == "Bajo")
                            objReporte.strColor = "Verde";

                        lstInfo.Add(objReporte);
                    }
                    booResult = true;
                }
                else
                {
                    lstInfo = null;
                    strErrMsg = "No hay datos para generar el repote";
                }
                    
            }
            else
            {
                lstInfo = null;
                strErrMsg = "No hay datos para generar el repote";
            }
                

            return booResult;
        }
        /// <summary>
        /// Metodo que permite obtener el Nombre del Riesgo Residual
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string LoadInfoDetalleRiesgo(ref string strErrMsg, int IdProbabilidadResidual, int IdImpactoResidual)
        {
            bool booResult = false;
            string NombreRiesgo = string.Empty;
            clsDALCuadroMandoRiesgos cDALreporte = new clsDALCuadroMandoRiesgos();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoDetalleRiesgo(ref strErrMsg, IdProbabilidadResidual, IdImpactoResidual);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        NombreRiesgo = dr["NombreRiesgoInherente"].ToString();
                    }
                }
            }

            return NombreRiesgo;
        }
        /// <summary>
        /// Metodo que permite consultar Info Reporte Indicador - Proceso 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteIndicadoresProceso(ref string strErrMsg, ref List<clsDTOCuadroMandoIndicadores> lstInfo, clsDTOCuadroMandoIndicadorFiltros objFiltros)
        {
            bool booResult = false;
            clsDALCuadroMandoIndicadores cDALreporte = new clsDALCuadroMandoIndicadores();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteIndicadoresProcesos(ref strErrMsg, objFiltros);
            int i = 0;
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoIndicadores objReporte = new clsDTOCuadroMandoIndicadores();
                        objReporte.intIdRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim());
                        objReporte.strNombreIndicador = dr["NombreIndicador"].ToString().Trim();
                        objReporte.strObjetivoIndicador = dr["ObjetivoIndicador"].ToString().Trim();
                        objReporte.intIdRiesgoAsociado = Convert.ToInt32(dr["IdRiesgoAsociado"].ToString().Trim());
                        if (dr["IdProbabilidadResidual"].ToString().Trim() != "")
                            objReporte.intIdProbabilidad = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                        else
                            objReporte.intIdProbabilidad = 0;
                        if (dr["IdImpactoResidual"].ToString().Trim() != "")
                            objReporte.intIdImpacto = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                        else
                            objReporte.intIdImpacto = 0;

                        
                         if (dr["IdProbabilidadResidual"].ToString().Trim() != "" && dr["IdImpactoResidual"].ToString().Trim() != "")
                             objReporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                         else
                             objReporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, 0, 0);

                        i++;
                        /*objReporte.strCadenaValor = dr["CadenaValor"].ToString().Trim();
                        objReporte.strMacroproceso = dr["Macroproceso"].ToString().Trim();
                        objReporte.strProceso = dr["Proceso"].ToString().Trim();
                        objReporte.strSubproceso = dr["Subproceso"].ToString().Trim();*/
                        if (objReporte.strNombreRiesgo == "Extremo")
                            objReporte.strColor = "Rojo";
                        if (objReporte.strNombreRiesgo == "Alto")
                            objReporte.strColor = "Naranja";
                        if (objReporte.strNombreRiesgo == "Moderado")
                            objReporte.strColor = "Amarillo";
                        if (objReporte.strNombreRiesgo == "Bajo")
                            objReporte.strColor = "Verde";
                        lstInfo.Add(objReporte);
                    }
                    booResult = true;
                }
                else
                {
                    lstInfo = null;
                    strErrMsg = "No hay datos para generar el reporte";
                }
            }
            else
            {
                lstInfo = null;
                strErrMsg = "No hay datos para generar el reporte";
            }
                

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar Info Reporte Indicador - Proceso 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteIndicadorResponsable(ref string strErrMsg, ref List<clsDTOCuadroMandoIndicadores> lstInfo, string IdJerarquias)
        {
            bool booResult = false;
            clsDALCuadroMandoIndicadores cDALreporte = new clsDALCuadroMandoIndicadores();
            List<clsDTOriesgosIndicadores> lstRiesgoIndicador = new List<clsDTOriesgosIndicadores>();
            clsDALriesgoIndicador cDtRiesgoInd = new clsDALriesgoIndicador();
            clsDTOriesgosIndicadores objRiesgoIndicador = new clsDTOriesgosIndicadores();
            clsDTOdetalleFormulaRiesgoIndicador objDetalleFormula = new clsDTOdetalleFormulaRiesgoIndicador();
            List<clsDTOdetalleFormulaRiesgoIndicador> lstDetalle = new List<clsDTOdetalleFormulaRiesgoIndicador>();
            List<clsDTOseguimientoRiesgoIndicador> lstSeguimiento = new List<clsDTOseguimientoRiesgoIndicador>();
            clsBLLformulaRiesgoIndicador cFormula = new clsBLLformulaRiesgoIndicador();
            clsBLLvariableRiesgoIndicador cVariable = new clsBLLvariableRiesgoIndicador();
            clsBLLseguimientoRiesgoIndicador cSeguimiento = new clsBLLseguimientoRiesgoIndicador();
            List<clsDTOmetasRiesgoIndicador> lstMetas = new List<clsDTOmetasRiesgoIndicador>();
            clsBLLmetaRiesgoIndicador cMetas = new clsBLLmetaRiesgoIndicador();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteIndicadorResponsable(ref strErrMsg, IdJerarquias);
            ProcessClass proceso = new ProcessClass();
            bool booResutl = false;
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    try {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            string Descripcion = string.Empty;
                            string color = string.Empty;
                            double resultFormula = 0;
                            clsDTOCuadroMandoIndicadores objReporte = new clsDTOCuadroMandoIndicadores();
                            objReporte.intIdRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim());
                            objReporte.strNombreIndicador = dr["NombreIndicador"].ToString().Trim();
                            objReporte.strObjetivoIndicador = dr["ObjetivoIndicador"].ToString().Trim();
                            objReporte.strResponsable = dr["NombreHijo"].ToString().Trim();
                            objReporte.intIdRiesgoAsociado = Convert.ToInt32(dr["IdRiesgoAsociado"].ToString().Trim());
                            objReporte.intIdProbabilidad = Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim());
                            objReporte.intIdImpacto = Convert.ToInt32(dr["IdImpacto"].ToString().Trim());
                            objReporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                            objReporte.strCadenaValor = dr["CadenaValor"].ToString().Trim();
                            objReporte.strMacroproceso = dr["Macroproceso"].ToString().Trim();
                            objReporte.strProceso = dr["Proceso"].ToString().Trim();
                            objReporte.strSubproceso = dr["Subproceso"].ToString().Trim();
                            if (Convert.ToBoolean(dr["porcentaje"].ToString().Trim()) == true)
                                objReporte.booPorcentaje = 1;
                            else
                                objReporte.booPorcentaje = 0;
                            lstDetalle = cFormula.mtdConsultarDetalleFormula(booResult, ref strErrMsg, Convert.ToInt32(dr["IdFormula"].ToString().Trim()));
                            lstMetas = cMetas.mtdConsultarMetaRiesgoIndicadorxMeta(booResult, ref strErrMsg, Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim()), Convert.ToInt32(dr["IdMeta"].ToString().Trim()));
                            if (lstMetas != null)
                            {
                                foreach (clsDTOmetasRiesgoIndicador objMetas in lstMetas)
                                {
                                    booResutl = proceso.mtdValidateMeta(ref resultFormula, objMetas.strValorOtraFrecuencia,
                                        objMetas.intIdDetalleFrecuencia, objMetas.strAño, objMetas.strMes, lstDetalle,
                                        Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim()), objReporte.booPorcentaje);
                                }
                            }
                            objRiesgoIndicador.dblResultado = resultFormula;
                            lstSeguimiento = cSeguimiento.mtdConsultarSeguimientoIndicador(booResult, ref strErrMsg, Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim()));
                            if (lstSeguimiento != null)
                                Descripcion = proceso.mtdValidaSeguimiento(resultFormula, lstSeguimiento, ref color);
                            objReporte.strColor = color;
                            lstInfo.Add(objReporte);
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrMsg = string.Format("Por favor validar Data, todos Indicadores deben tener metas", ex.Message);
                        lstInfo = null;
                        return false;
                    }
                   
                    booResult = true;
                }
                else
                {
                    lstInfo = null;
                    strErrMsg = "No hay datos para generar el reporte";
                }
            }
            else
            {
                lstInfo = null;
                strErrMsg = "No hay datos para generar el reporte";
            }
                

            return booResult;
        }
    }
}