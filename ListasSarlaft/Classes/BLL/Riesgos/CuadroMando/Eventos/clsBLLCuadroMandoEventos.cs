using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLCuadroMandoEventos
    {
        /// <summary>
        /// Metodo que permite consultar Info Reporte Estados Eventos 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteEstadosEventos(ref string strErrMsg, ref List<clsDTOCuadroMandoEventos> lstInfo, clsDTOCuadroMandoEventosFiltro objFiltro, ref int total)
        {
            bool booResult = false;
            clsDALCuadroMandoEventos cDALreporte = new clsDALCuadroMandoEventos();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteEstadosEventos(ref strErrMsg, objFiltro);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoEventos objReporte = new clsDTOCuadroMandoEventos();
                        total = total + Convert.ToInt32(dr["NumEvento"].ToString());
                        objReporte.intNumEventos = Convert.ToInt32(dr["NumEvento"].ToString());
                        objReporte.intIdEstado = Convert.ToInt32(dr["IdEstado"].ToString().Trim());
                        objReporte.strDescripcionEstado = dr["Descripcion"].ToString().Trim();
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
        /// Metodo que permite consultar cantidad Eventos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool GetAllEventos(ref string strErrMsg, ref double CantEventos)
        {
            bool booResult = false;
            clsDALCuadroMandoEventos cDALreporte = new clsDALCuadroMandoEventos();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.GetAllEventos(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        CantEventos = Convert.ToDouble(dr["CantEventos"].ToString());
                    }
                    booResult = true;
                }
                else
                    CantEventos = 0;
            }
            else
                CantEventos = 0;

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar Info Reporte Eventos Consolidado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool LoadInfoReporteEventosConsolidado(ref string strErrMsg, ref List<clsDTOCuadroMandoEventos> lstInfo, clsDTOCuadroMandoEventosFiltro objFiltro)
        {
            bool booResult = false;
            clsDALCuadroMandoEventos cDALreporte = new clsDALCuadroMandoEventos();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoReporteEventosConsolidado(ref strErrMsg, objFiltro);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsDTOCuadroMandoEventos objReporte = new clsDTOCuadroMandoEventos();
                        objReporte.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                        objReporte.strEmpresa = dr["Empresa"].ToString().Trim();
                        objReporte.dtFechaEvento = Convert.ToDateTime(dr["FechaEvento"].ToString().Trim());
                        objReporte.strRegistro = dr["Registro"].ToString().Trim();
                        objReporte.intIdEvento = Convert.ToInt32(dr["IdEvento"].ToString().Trim());
                        objReporte.strArea = dr["Area"].ToString().Trim();
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
    }
}