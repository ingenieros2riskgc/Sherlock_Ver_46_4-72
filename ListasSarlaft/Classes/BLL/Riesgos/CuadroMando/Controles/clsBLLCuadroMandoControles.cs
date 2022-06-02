using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLCuadroMandoControles
    {
        /// <summary>
        /// Metodo que permite consultar Info Reporte Controles 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteControles(ref string strErrMsg, ref List<clsDTOCuadroMandoControles> lstInfo, int IdJerarquia, ref int valorTotalRiesgo)
        {
            bool booResult = false;
            clsDALCuadroMandoControles cDALreporte = new clsDALCuadroMandoControles();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteControles(ref strErrMsg, IdJerarquia);
            //
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoControles objReporte = new clsDTOCuadroMandoControles();
                        objReporte.intIdControl = Convert.ToInt32(dr["IdControl"].ToString());
                        objReporte.strEfectividad = dr["NombreEscala"].ToString().Trim();
                        objReporte.strCodigoControl = dr["CodigoControl"].ToString().Trim();
                        objReporte.strNombreControl = dr["NombreControl"].ToString().Trim();
                        objReporte.strResponsable = dr["NombreHijo"].ToString().Trim();
                        int CantRiesgo = 0;
                        if (GetCantRiesgos(ref strErrMsg, ref CantRiesgo, Convert.ToInt32(dr["IdControl"].ToString())) == true)
                        {
                            objReporte.intCantRiesgo = CantRiesgo;
                            valorTotalRiesgo = valorTotalRiesgo + CantRiesgo;
                        }
                            
                        else
                            objReporte.intCantRiesgo = 0;
                        /*objReporte.strCodigoRiesgo = dr["Codigo"].ToString().Trim();
                        objReporte.strNombreRiesgo = dr["Nombre"].ToString().Trim();
                        objReporte.strRiesgoGlobal = dr["NombreClasificacionRiesgo"].ToString().Trim();*/
                        lstInfo.Add(objReporte);
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
        /// Metodo que permite consultar Info Reporte Controles 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteControlesxJerarquias(ref string strErrMsg, ref List<clsDTOCuadroMandoControles> lstInfo, string IdJerarquias)
        {
            bool booResult = false;
            clsDALCuadroMandoControles cDALreporte = new clsDALCuadroMandoControles();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteControlesxJerarquias(ref strErrMsg, IdJerarquias);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoControles objReporte = new clsDTOCuadroMandoControles();
                        objReporte.intIdControl = Convert.ToInt32(dr["IdControl"].ToString());
                        objReporte.strEfectividad = dr["NombreEscala"].ToString().Trim();
                        objReporte.strCodigoControl = dr["CodigoControl"].ToString().Trim();
                        objReporte.strNombreControl = dr["NombreControl"].ToString().Trim();
                        int CantRiesgo = 0;
                        if (GetCantRiesgos(ref strErrMsg, ref CantRiesgo, Convert.ToInt32(dr["IdControl"].ToString())) == true)
                            objReporte.intCantRiesgo = CantRiesgo;
                        else
                            objReporte.intCantRiesgo = 0;
                        /*objReporte.strCodigoRiesgo = dr["Codigo"].ToString().Trim();
                        objReporte.strNombreRiesgo = dr["Nombre"].ToString().Trim();
                        objReporte.strRiesgoGlobal = dr["NombreClasificacionRiesgo"].ToString().Trim();*/
                        lstInfo.Add(objReporte);
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
        /// Metodo que permite consultar cantidad Controles
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool GetAllControles(ref string strErrMsg, ref int CantControls)
        {
            bool booResult = false;
            clsDALCuadroMandoControles cDALreporte = new clsDALCuadroMandoControles();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.GetAllControles(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        CantControls = Convert.ToInt32(dr["CantControls"].ToString());
                    }
                    booResult = true;
                }
                else
                    CantControls = 0;
            }
            else
                CantControls = 0;

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar cantidad Controles
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool GetCantRiesgos(ref string strErrMsg, ref int CantRiesgo, int IdControl)
        {
            bool booResult = false;
            clsDALCuadroMandoControles cDALreporte = new clsDALCuadroMandoControles();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.GetCantRiesgos(ref strErrMsg, IdControl);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        CantRiesgo = Convert.ToInt32(dr["CantRiesgo"].ToString());
                    }
                    booResult = true;
                }
                else
                    CantRiesgo = 0;
            }
            else
                CantRiesgo = 0;

            return booResult;
        }
    }
}