using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLCuadroMandoConsolidado
    {
        /// <summary>
        /// Metodo que permite consultar Info Reporte XYZ
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteXYZ(ref string strErrMsg, ref List<clsDTOCuadroMandoConsolidado> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            try
            {
                
                clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
                
                dtInfo = cDALreporte.LoadInfoReporteXYZ(ref strErrMsg, objFiltros);
            }
            catch (Exception ex)
            {
                strErrMsg = "Error en la consulta de los datos: " + ex;
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
                            if (objFiltros.dtFechaHistoricoInicial != default(DateTime))
                            {
                                objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgoHistorico(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                            }
                            else
                            {
                                objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                            }
                                
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
            clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoDetalleRiesgo(ref strErrMsg,IdProbabilidadResidual, IdImpactoResidual);
            
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
            clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
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
        /// Metodo que permite consultar Info Reporte NLK
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteNLK(ref string strErrMsg, ref List<clsDTOCuadroMandoConsolidado> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            try
            {
                clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
                
                dtInfo = cDALreporte.LoadInfoReporteNLK(ref strErrMsg, objFiltros);
            }
            catch (Exception ex)
            {
                strErrMsg = "Error en la consulta de datos: " + ex;
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
                            objPreporte.intIdProbabilidadInherente = Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim());
                            objPreporte.intIdImpactoInherente = Convert.ToInt32(dr["IdImpacto"].ToString().Trim());
                            if (objFiltros.dtFechaHistoricoInicial != default(DateTime))
                            {
                                objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgoHistorico(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));
                            }
                            else
                            {
                                objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));
                            }
                            lstInfo.Add(objPreporte);
                        }
                        booResult = true;
                    }
                    else
                    {
                        strErrMsg = "No hay datos para generar el reporte";
                        lstInfo = null;
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
        public string LoadInfoDetalleReporteNLK(ref string strErrMsg, int IdProbabilidad, int IdImpacto)
        {
            bool booResult = false;
            string NombreRiesgo = string.Empty;
            clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoDetalleReporteNLK(ref strErrMsg, IdProbabilidad, IdImpacto);

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
        /// Metodo que permite consultar Info Reporte NLK
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteNLKPerfilRI(ref string strErrMsg, ref List<clsDTOCuadroMandoConsolidado> lstInfo)
        {
            bool booResult = false;
            clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteNLKPerfilRI(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoConsolidado objPreporte = new clsDTOCuadroMandoConsolidado();
                        objPreporte.intNumeroRegistros = Convert.ToInt32(dr["NumeroRegistros"].ToString().Trim());
                        objPreporte.intSumatoriaProbabilidad = Convert.ToInt32(dr["SumatoriaProbabilidad"].ToString().Trim());
                        objPreporte.intSumatoriaImpacto = Convert.ToInt32(dr["SumatoriaImpacto"].ToString().Trim());
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
        /// <summary>
        /// Metodo que permite consultar Info Reporte NLK
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteNLKPerfilRR(ref string strErrMsg, ref List<clsDTOCuadroMandoConsolidado> lstInfo)
        {
            bool booResult = false;
            clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteNLKPerfilRR(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoConsolidado objPreporte = new clsDTOCuadroMandoConsolidado();
                        objPreporte.intNumeroRegistros = Convert.ToInt32(dr["NumeroRegistros"].ToString().Trim());
                        objPreporte.intSumatoriaProbabilidad = Convert.ToInt32(dr["SumatoriaProbabilidadResidual"].ToString().Trim());
                        objPreporte.intSumatoriaImpacto = Convert.ToInt32(dr["SumatoriaImpactoResidual"].ToString().Trim());
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
        /// <summary>
        /// Metodo que permite consultar Info Evolucion Perfil Riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoEvoPerfilRiesgo(ref string strErrMsg, ref List<clsDTOCuadroMandoConsolidado> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            bool booResult = false;
            clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
            DataTable dtInfo = new DataTable();
            
                if(objFiltros.dtFechaEvoPerfilInicial.Year != objFiltros.dtFechaEvoPerfilFinal.Year)
                {
                    booResult = false;
                    strErrMsg = "El perfil sólo se puede evaluar dentro del mismo año, por favor seleccione otras fechas";
                }
                else
                {
                for (int i = objFiltros.dtFechaEvoPerfilInicial.Month; i <= objFiltros.dtFechaEvoPerfilFinal.Month; i++)
                {
                    dtInfo = cDALreporte.LoadInfoEvoPerfilRiesgo(ref strErrMsg, objFiltros, i);
                    if (dtInfo != null)
                    {
                        if (dtInfo.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtInfo.Rows)
                            {
                                clsDTOCuadroMandoConsolidado objPreporte = new clsDTOCuadroMandoConsolidado();
                                objPreporte.intNumeroRegistros = Convert.ToInt32(dr["NumeroRegistros"].ToString().Trim());
                                objPreporte.intSumatoriaProbabilidad = Convert.ToInt32(dr["SumatoriaProbabilidadResidual"].ToString().Trim());
                                objPreporte.intSumatoriaImpacto = Convert.ToInt32(dr["SumatoriaImpactoResidual"].ToString().Trim());
                                //objPreporte.strNombreRiesgo = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                                lstInfo.Add(objPreporte);
                            }
                            booResult = true;
                        }
                        else
                            lstInfo = null;
                    }
                    else
                        lstInfo = null;
                }
                
            }
            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar Severidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdGetSeveridad(ref string strErrMsg, ref string severidad, ref string color, string IdProbabilidad, string IdImpacto)
        {
            bool booResult = false;
            clsDALCuadroMandoConsolidado cDALreporte = new clsDALCuadroMandoConsolidado();
            DataTable dtInfo = new DataTable();
            
                dtInfo = cDALreporte.mtdGetSeveridad(ref strErrMsg, IdProbabilidad, IdImpacto);
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            severidad =dr["NombreRiesgoInherente"].ToString().Trim();
                            color = dr["Color"].ToString().Trim();
                            
                        }
                        booResult = true;
                    }
                    
                }
                
            return booResult;
        }
    }
}