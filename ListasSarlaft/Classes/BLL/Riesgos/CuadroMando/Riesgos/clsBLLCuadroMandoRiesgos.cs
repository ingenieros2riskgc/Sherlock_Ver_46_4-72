using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLCuadroMandoRiesgos
    {
        /// <summary>
        /// Metodo que permite consultar Info Reporte Riesgos 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteRiesgos(ref string strErrMsg, ref List<clsDTOCuadroMandoConsolidado> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            try
            {
                clsDALCuadroMandoRiesgos cDALreporte = new clsDALCuadroMandoRiesgos();

                dtInfo = cDALreporte.LoadInfoReporteRiesgos(ref strErrMsg, objFiltros);
            }catch(Exception ex)
            {
                strErrMsg = "Error consulta del reporte: " + ex;
            }
            try
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOCuadroMandoConsolidado objPreporte = new clsDTOCuadroMandoConsolidado();
                            objPreporte.intNumeroRegistros = Convert.ToInt32(dr["NumeroRegistros"].ToString().Trim());
                            objPreporte.intIdProbabilidadResidual = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                            objPreporte.intIdImpactoResidual = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                            if (objFiltros.dtFechaHistoricoFinal != default(DateTime))
                                objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgoHistorico(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                            else
                                objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                            lstInfo.Add(objPreporte);
                        }
                        booResult = true;
                    }
                    else
                    {
                        lstInfo = null;
                        //strErrMsg = "No hay datos para generar el reporte";
                    }
                        
                }
                else
                {
                    lstInfo = null;
                    //strErrMsg = "No hay datos para generar el reporte";
                }
                    
            }catch(Exception ex)
            {
                strErrMsg = "Error en la construccion del reporte: " + ex;
            }
            

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar Info Reporte Riesgos Saro
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteRiesgosSaro(ref string strErrMsg, ref List<clsDTOCuadroMandoConsolidado> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros, ref string strQuery)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            try
            {
                clsDALCuadroMandoRiesgos cDALreporte = new clsDALCuadroMandoRiesgos();

                dtInfo = cDALreporte.LoadInfoReporteRiesgosSaro(ref strErrMsg, objFiltros, ref strQuery);
            }
            catch(Exception ex)
            {
                strErrMsg = "Error en la consulta del reporte: " + ex;
            }
            try
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOCuadroMandoConsolidado objPreporte = new clsDTOCuadroMandoConsolidado();
                            objPreporte.intNumeroRegistros = Convert.ToInt32(dr["NumeroRegistros"].ToString().Trim());
                            objPreporte.intIdProbabilidadResidual = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                            objPreporte.intIdImpactoResidual = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                            if(objFiltros.dtFechaHistoricoInicial != default(DateTime))
                                objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgoHistorico(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                            else
                                objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                            lstInfo.Add(objPreporte);
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
                    
            }catch(Exception ex)
            {
                strErrMsg = "Error en la construccion del reporte: " + ex;
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
        /// Metodo que permite obtener el Nombre del Riesgo Residual
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string LoadInfoDetalleRiesgoHistorico(ref string strErrMsg, int IdProbabilidadResidual, int IdImpactoResidual)
        {
            bool booResult = false;
            string NombreRiesgo = string.Empty;
            clsDALCuadroMandoRiesgos cDALreporte = new clsDALCuadroMandoRiesgos();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoDetalleRiesgoHistorico(ref strErrMsg, IdProbabilidadResidual, IdImpactoResidual);

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
        /// Metodo que permite consultar Info Reporte Riesgos Sarlaft
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteRiesgosSarlaft(ref string strErrMsg, ref List<clsDTOCuadroMandoConsolidado> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            bool booResult = false;
            clsDALCuadroMandoRiesgos cDALreporte = new clsDALCuadroMandoRiesgos();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteRiesgosSarlaft(ref strErrMsg, objFiltros);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoConsolidado objPreporte = new clsDTOCuadroMandoConsolidado();
                        objPreporte.intNumeroRegistros = Convert.ToInt32(dr["NumeroRegistros"].ToString().Trim());
                        objPreporte.intIdProbabilidadResidual = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                        objPreporte.intIdImpactoResidual = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                        objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                        lstInfo.Add(objPreporte);
                    }
                    booResult = true;
                }
                else
                    lstInfo = null;
            }
            else
                lstInfo = null;

            return booResult;
        }
    }
}