using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLFrecuenciavsEventos
    {
        /// <summary>
        /// Metodo para insertar el registro de Frecuencia de los eventos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarFrecuenciavsEventos(clsDTOFrecuenciavsEventos frequencyEvents, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALFrecuenciavsEventos cDALFrequencyEvents = new clsDALFrecuenciavsEventos();

            booResult = cDALFrequencyEvents.mtdInsertarFrecuenciavsEventos(frequencyEvents, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para validar antes de insertar el registro de Frecuencia de los eventos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdValidaInsertarFrecuenciavsEventos(clsDTOFrecuenciavsEventos frequencyEvents, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALFrecuenciavsEventos cDALFrequencyEvents = new clsDALFrecuenciavsEventos();

            booResult = cDALFrequencyEvents.mtdValidarInsertarFrecuenciavsEventos(frequencyEvents, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Frecuencia de los eventos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOFrecuenciavsEventos> mtdConsultarFrecuenciavsEventos(ref List<clsDTOFrecuenciavsEventos> lstFrequencyEvents, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALFrecuenciavsEventos cDtFrequencyEvents = new clsDALFrecuenciavsEventos();
            #endregion Vars

            booResult = cDtFrequencyEvents.mtdConsultarFrecuenciavsEventos(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOFrecuenciavsEventos objFrequencyEvents = new clsDTOFrecuenciavsEventos();
                            objFrequencyEvents.intIdFrecuenciaEventos = Convert.ToInt32(dr["IdFrecuenciaEventos"].ToString().Trim());
                            objFrequencyEvents.intEventosMaximos = Convert.ToInt32(dr["EventosMaximos"].ToString().Trim());
                            objFrequencyEvents.intCodigoFrecuencia = Convert.ToInt32(dr["CodigoFrecuencia"].ToString().Trim());
                            objFrequencyEvents.strNombreFrecuencia = dr["NombreFrecuencia"].ToString().Trim();
                            objFrequencyEvents.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objFrequencyEvents.intIdUsuario = Convert.ToInt32(dr["UsuarioCreacion"].ToString().Trim());
                            objFrequencyEvents.strUsuario = dr["Usuario"].ToString().Trim();

                            lstFrequencyEvents.Add(objFrequencyEvents);
                        }
                    }
                    else
                        lstFrequencyEvents = null;
                }
                else
                    lstFrequencyEvents = null;
            }

            return lstFrequencyEvents;
        }
        /// <summary>
        /// Metodo que permite la actualizacion de las frecuencias de eventos
        /// </summary>
        /// <param name="objFrequencyEvents">objeto con la informacion de las frecuencias de eventos</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateFrecuenciavsEventos(ref clsDTOFrecuenciavsEventos objFrequencyEvents, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALFrecuenciavsEventos cDtFrequencyEvents = new clsDALFrecuenciavsEventos();

            booResult = cDtFrequencyEvents.mtdUpdateFrecuenciavsEventos(ref objFrequencyEvents, ref strErrMsg);

            return booResult;
        }
    }
}