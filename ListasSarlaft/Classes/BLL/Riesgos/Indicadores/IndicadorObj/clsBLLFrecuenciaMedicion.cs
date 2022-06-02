using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLFrecuenciaMedicion
    {
        /// <summary>
        /// Realiza la consulta de las frecuencias de Medición
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOfrecuenciaMedicion> mtdConsultarFrecuenciaMedicion(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOfrecuenciaMedicion> lstFrecuencias = new List<clsDTOfrecuenciaMedicion>();
            clsDALfrecuenciaMedicion cDtFrecuenciaMedicion = new clsDALfrecuenciaMedicion();
            clsDTOfrecuenciaMedicion objFrecuencia = new clsDTOfrecuenciaMedicion();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtFrecuenciaMedicion.mtdConsultarFrecuenciaMedicion(ref dtInfo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objFrecuencia = new clsDTOfrecuenciaMedicion();
                        objFrecuencia.intIdFrecuenciaMedicion = Convert.ToInt32(dr["IdFrecuenciaMedicion"].ToString().Trim());
                        objFrecuencia.strFrecuenciaMedicion = dr["FrecuenciaMedicion"].ToString().Trim();
                        lstFrecuencias.Add(objFrecuencia);
                    }
                }
                else
                {
                    lstFrecuencias = null;
                    strErrMsg = "No hay datos de la Frecuencia de Medición.";
                }
            }
            else
                objFrecuencia = null;

            return lstFrecuencias;
        }
        /// <summary>
        /// Realiza la consulta de los detalles de la frecuencia de Medición
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public DataTable mtdConsultarDetalleFrecuencia(ref string strErrMsg, int IdFrecuencia)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDALfrecuenciaMedicion cDtFrecuenciaMedicion = new clsDALfrecuenciaMedicion();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtFrecuenciaMedicion.mtdConsultarDetalleFrecuencia(ref dtInfo, ref strErrMsg, IdFrecuencia);

            return dtInfo;
        }
    }
}