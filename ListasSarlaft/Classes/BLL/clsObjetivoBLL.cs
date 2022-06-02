using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsObjetivoBLL
    {
        /// <summary>
        /// Realiza la consulta del Objetivo Calidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsObjetivoCalidad> mtdConsultarObjetivoCalidad(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtObjetivoCalidad cDtObjetivoCalidad = new clsDtObjetivoCalidad();
            clsObjetivoCalidad objObjetivoCalidad = new clsObjetivoCalidad();
            List<clsObjetivoCalidad> lstObjetivo = new List<clsObjetivoCalidad>();
            #endregion Vars

            dtInfo = cDtObjetivoCalidad.mtdConsultarObjetivoCalidad(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objObjetivoCalidad = new clsObjetivoCalidad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim()
                            );
                        lstObjetivo.Add(objObjetivoCalidad);
                    }
                }
                else
                {
                    lstObjetivo = null;
                    //strErrMsg = "No hay información de Objetivo de calidad.";
                }
            }
            else
                lstObjetivo = null;

            return lstObjetivo;
        }

        /// <summary>
        /// Realiza la consulta del Objetivo Calidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsObjetivoCalidad> mtdConsultarObjetivoCalidad(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtObjetivoCalidad cDtObjetivoCalidad = new clsDtObjetivoCalidad();
            clsObjetivoCalidad objObjetivoCalidad = new clsObjetivoCalidad();
            List<clsObjetivoCalidad> lstObjetivo = new List<clsObjetivoCalidad>();
            #endregion Vars

            dtInfo = cDtObjetivoCalidad.mtdConsultarObjetivoCalidad(booEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objObjetivoCalidad = new clsObjetivoCalidad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim()
                            );
                        lstObjetivo.Add(objObjetivoCalidad);
                    }
                }
                else
                {
                    lstObjetivo = null;
                    //strErrMsg = "No hay información de Objetivo de calidad.";
                }
            }
            else
                lstObjetivo = null;

            return lstObjetivo;
        }

        /// <summary>
        /// Permite la insercion de la Objetivo de calidad
        /// </summary>
        /// <param name="objObjetivo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public bool mtdInsertarObjetivoCalidad(clsObjetivoCalidad objObjetivo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtObjetivoCalidad cDtObjetivo = new clsDtObjetivoCalidad();

            booResult = cDtObjetivo.mtdInsertarObjetivoCalidad(objObjetivo, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite la actualizacion de la Objetivo de calidad
        /// </summary>
        /// <param name="objObjetivo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public bool mtdActualizarObjetivoCalidad(clsObjetivoCalidad objObjetivo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtObjetivoCalidad cDtObjetivo = new clsDtObjetivoCalidad();

            booResult = cDtObjetivo.mtdActualizarObjetivoCalidad(objObjetivo, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objObjetivo">Informacion de Objetivo</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsObjetivoCalidad objObjetivo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtObjetivoCalidad cDtObjetivo = new clsDtObjetivoCalidad();

            booResult = cDtObjetivo.mtdActualizarEstado(objObjetivo, ref strErrMsg);

            return booResult;
        }

    }
}