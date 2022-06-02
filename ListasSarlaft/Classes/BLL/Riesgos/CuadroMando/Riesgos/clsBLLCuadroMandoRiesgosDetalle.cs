using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLCuadroMandoRiesgosDetalle
    {
        /// <summary>
        /// Metodo que permite consultar Info Reporte Riesgos 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteRiesgos(ref string strErrMsg, ref List<clsDTOCuadroMandoRiesgosDetalle> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros
            ,string TipoReporte)
        {
            bool booResult = false;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            double promedio = 0;
            string ListaCausas = string.Empty;
            string IdCausasvsControles = string.Empty;
            dtInfo = cDALreporte.LoadInfoReporteDetalleRiesgos(ref strErrMsg, objFiltros, TipoReporte);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoRiesgosDetalle objreporte = new clsDTOCuadroMandoRiesgosDetalle();
                        int CantCausas = 0;
                        DataTable dtInfoControl = new DataTable();
                        dtInfoControl = cDALreporte.LoadInfoControlesRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()));
                        objreporte.intIdRiesgo = Convert.ToInt32(dr["IdRiesgo"].ToString());
                        objreporte.strCodigoRiesgo = dr["CodigoRiesgo"].ToString().Trim();
                        objreporte.strNombreRiesgo = dr["NombreRiesgo"].ToString().Trim();
                        objreporte.intIdProbabilidadResidual = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                        objreporte.intIdImpactoResidual = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                        if(objFiltros.dtFechaHistoricoInicial != default(DateTime))
                            objreporte.strRiesgoInherente = LoadInfoDetalleRiesgoHistorico(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                        else
                            objreporte.strRiesgoInherente = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                        objreporte.intCantControl = Convert.ToInt32(LoadInfoCantControlRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString())));
                        promedio = LoadInfoPromedioControlesRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()));
                        objreporte.intCantEvento = Convert.ToInt32(LoadInfoCantEventoRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString())));
                        ListaCausas = dr["ListaCausas"].ToString().Trim();
                        string[] causas = ListaCausas.Split('|');
                        foreach (var IdCausa in causas)
                        {
                            if(IdCausa != "")
                            {
                                if (dtInfoControl != null)
                                {
                                    if (dtInfoControl.Rows.Count > 0)
                                    {
                                        foreach (DataRow drControl in dtInfoControl.Rows)
                                        {
                                            IdCausasvsControles = LoadInfoCantCusasSinControl(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()), Convert.ToInt32(IdCausa), Convert.ToInt32(drControl["IdControl"].ToString()));
                                            if (IdCausasvsControles == null || IdCausasvsControles == "")
                                            {
                                                CantCausas++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CantCausas++;
                                    }
                                }
                                /*else
                                {
                                    CantCausas++;
                                }*/
                            }
                        }
                        objreporte.intCausasSinControl = CantCausas;
                       lstInfo.Add(objreporte);
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
        /// Metodo que permite obtener el Nombre del Riesgo Residual
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string LoadInfoDetalleRiesgo(ref string strErrMsg, int IdProbabilidadResidual, int IdImpactoResidual)
        {
            bool booResult = false;
            string NombreRiesgo = string.Empty;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
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
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
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
        /// Metodo que permite obtener la cantidad de controles por riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string LoadInfoCantControlRiesgo(ref string strErrMsg, int IdRiesgo)
        {
            bool booResult = false;
            string CantControl = string.Empty;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoCantControlRiesgo(ref strErrMsg, IdRiesgo);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        CantControl = dr["CantControl"].ToString();
                    }
                }
            }

            return CantControl;
        }
        /// <summary>
        /// Metodo que permite obtener la cantidad de eventos por riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string LoadInfoCantEventoRiesgo(ref string strErrMsg, int IdRiesgo)
        {
            bool booResult = false;
            string CantEvento = string.Empty;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoCantEventolRiesgo(ref strErrMsg, IdRiesgo);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        CantEvento = dr["CantEvento"].ToString();
                    }
                }
            }

            return CantEvento;
        }
        /// <summary>
        /// Metodo que permite obtener el promedio de la calificacion del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public double LoadInfoPromedioControlesRiesgo(ref string strErrMsg, int IdRiesgo)
        {
            bool booResult = false;
            double PromedioControl = 0;
            int cant = 0;
            string CantControl = string.Empty;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoControlesRiesgo(ref strErrMsg, IdRiesgo);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        PromedioControl = PromedioControl + Convert.ToDouble(dr["CalificacionControl"].ToString().Trim());
                        cant++;
                    }
                }
            }
            PromedioControl = (PromedioControl / cant);
            return PromedioControl;
        }
        /// <summary>
        /// Metodo que permite obtener las causas sin control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string LoadInfoCantCusasSinControl(ref string strErrMsg, int IdRiesgo, int IdCausa, int IdControl)
        {
            bool booResult = false;
            string IdCausasvsControles = string.Empty;
            
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoGetCausas(ref strErrMsg, IdRiesgo, IdCausa, IdControl);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        IdCausasvsControles = dr["IdCausasvsControles"].ToString();
                        
                    }
                }
            }

            return IdCausasvsControles;
        }
        /// <summary>
        /// Metodo que permite consultar Info Reporte Riesgos 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarReporteCausasSinControl(ref string strErrMsg, ref List<clsDTOCuadroMandoRiesgosDetalle> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros
            ,string TipoReporte)
        {
            bool booResult = false;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            double promedio = 0;
            string ListaCausas = string.Empty;
            string IdCausasvsControles = string.Empty;
            
            dtInfo = cDALreporte.LoadInfoReporteDetalleRiesgos(ref strErrMsg, objFiltros, TipoReporte);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoRiesgosDetalle objreporte = new clsDTOCuadroMandoRiesgosDetalle();
                        int CantCausas = 0;
                        DataTable dtInfoControl = new DataTable();
                        dtInfoControl = cDALreporte.LoadInfoControlesRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()));
                        objreporte.intIdRiesgo = Convert.ToInt32(dr["IdRiesgo"].ToString());
                        objreporte.strCodigoRiesgo = dr["CodigoRiesgo"].ToString().Trim();
                        objreporte.strNombreRiesgo = dr["NombreRiesgo"].ToString().Trim();
                        objreporte.intIdProbabilidadResidual = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                        objreporte.intIdImpactoResidual = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                        objreporte.strRiesgoInherente = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                        //objreporte.intCantControl = Convert.ToInt32(LoadInfoCantControlRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString())));
                        //promedio = LoadInfoPromedioControlesRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()));
                        //objreporte.intCantEvento = Convert.ToInt32(LoadInfoCantEventoRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString())));
                        /*ListaCausas = dr["ListaCausas"].ToString().Trim();
                        string[] causas = ListaCausas.Split('|');
                        foreach (var IdCausa in causas)
                        {
                            if (IdCausa != "")
                            {
                                if (dtInfoControl != null)
                                {
                                    if (dtInfoControl.Rows.Count > 0)
                                    {
                                        foreach (DataRow drControl in dtInfoControl.Rows)
                                        {
                                            IdCausasvsControles = LoadInfoCantCusasSinControl(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()), Convert.ToInt32(IdCausa), Convert.ToInt32(drControl["IdControl"].ToString()));
                                            if (IdCausasvsControles == null || IdCausasvsControles == "")
                                            {
                                                CantCausas++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CantCausas++;
                                    }
                                }
                                /*else
                                {
                                    CantCausas++;
                                }
                            }
                        }
                        objreporte.intCausasSinControl = CantCausas;*/
                        lstInfo.Add(objreporte);
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
                strErrMsg = "No hay datos para generar el reporte";
                lstInfo = null;
            }

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar Info Reporte Riesgos 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteRiesgosSinControl(ref string strErrMsg, ref List<clsDTOCuadroMandoRiesgosDetalle> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            bool booResult = false;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            double promedio = 0;
            string ListaCausas = string.Empty;
            string IdCausasvsControles = string.Empty;
            dtInfo = cDALreporte.LoadInfoReporteRiesgosSinControl(ref strErrMsg, objFiltros);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoRiesgosDetalle objreporte = new clsDTOCuadroMandoRiesgosDetalle();
                        int CantCausas = 0;
                        DataTable dtInfoControl = new DataTable();
                        dtInfoControl = cDALreporte.LoadInfoControlesRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()));
                        objreporte.intIdRiesgo = Convert.ToInt32(dr["IdRiesgo"].ToString());
                        objreporte.strCodigoRiesgo = dr["CodigoRiesgo"].ToString().Trim();
                        objreporte.strNombreRiesgo = dr["NombreRiesgo"].ToString().Trim();
                        objreporte.intIdProbabilidadResidual = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                        objreporte.intIdImpactoResidual = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                        objreporte.strRiesgoInherente = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                        //objreporte.intCantControl = Convert.ToInt32(LoadInfoCantControlRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString())));
                        //promedio = LoadInfoPromedioControlesRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()));
                        //objreporte.intCantEvento = Convert.ToInt32(LoadInfoCantEventoRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString())));
                        /*ListaCausas = dr["ListaCausas"].ToString().Trim();
                        string[] causas = ListaCausas.Split('|');
                        foreach (var IdCausa in causas)
                        {
                            if (IdCausa != "")
                            {
                                if (dtInfoControl != null)
                                {
                                    if (dtInfoControl.Rows.Count > 0)
                                    {
                                        foreach (DataRow drControl in dtInfoControl.Rows)
                                        {
                                            IdCausasvsControles = LoadInfoCantCusasSinControl(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()), Convert.ToInt32(IdCausa), Convert.ToInt32(drControl["IdControl"].ToString()));
                                            if (IdCausasvsControles == null || IdCausasvsControles == "")
                                            {
                                                CantCausas++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CantCausas++;
                                    }
                                }
                                /*else
                                {
                                    CantCausas++;
                                }
                            }
                        }*/
                        //objreporte.intCausasSinControl = CantCausas;
                        lstInfo.Add(objreporte);
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
                strErrMsg = "No hay datos para generar el reporte";
                lstInfo = null;
            }

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar Info Reporte Riesgos 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteRiesgosPlanes(ref string strErrMsg, ref List<clsDTOCuadroMandoRiesgosDetalle> lstInfo, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            bool booResult = false;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            double promedio = 0;
            string ListaCausas = string.Empty;
            string IdCausasvsControles = string.Empty;
            dtInfo = cDALreporte.LoadInfoReporteRiesgosPlanes(ref strErrMsg, objFiltros);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoRiesgosDetalle objreporte = new clsDTOCuadroMandoRiesgosDetalle();
                        int CantCausas = 0;
                        DataTable dtInfoControl = new DataTable();
                        dtInfoControl = cDALreporte.LoadInfoControlesRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()));
                        objreporte.intIdRiesgo = Convert.ToInt32(dr["IdRiesgo"].ToString());
                        objreporte.strCodigoRiesgo = dr["CodigoRiesgo"].ToString().Trim();
                        objreporte.strNombreRiesgo = dr["NombreRiesgo"].ToString().Trim();
                        objreporte.intIdProbabilidadResidual = Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim());
                        objreporte.intIdImpactoResidual = Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim());
                        objreporte.strRiesgoInherente = LoadInfoDetalleRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidadResidual"].ToString().Trim()), Convert.ToInt32(dr["IdImpactoResidual"].ToString().Trim()));
                        //objreporte.intCantControl = Convert.ToInt32(LoadInfoCantControlRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString())));
                        //promedio = LoadInfoPromedioControlesRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()));
                        //objreporte.intCantEvento = Convert.ToInt32(LoadInfoCantEventoRiesgo(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString())));
                        /*ListaCausas = dr["ListaCausas"].ToString().Trim();
                        string[] causas = ListaCausas.Split('|');
                        foreach (var IdCausa in causas)
                        {
                            if (IdCausa != "")
                            {
                                if (dtInfoControl != null)
                                {
                                    if (dtInfoControl.Rows.Count > 0)
                                    {
                                        foreach (DataRow drControl in dtInfoControl.Rows)
                                        {
                                            IdCausasvsControles = LoadInfoCantCusasSinControl(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()), Convert.ToInt32(IdCausa), Convert.ToInt32(drControl["IdControl"].ToString()));
                                            if (IdCausasvsControles == null || IdCausasvsControles == "")
                                            {
                                                CantCausas++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CantCausas++;
                                    }
                                }
                                /*else
                                {
                                    CantCausas++;
                                }
                            }
                        }*/
                        //objreporte.intCausasSinControl = CantCausas;
                        lstInfo.Add(objreporte);
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
                strErrMsg = "No hay datos para generar el reporte";
                lstInfo = null;
            }

            return booResult;
        }
    }
}