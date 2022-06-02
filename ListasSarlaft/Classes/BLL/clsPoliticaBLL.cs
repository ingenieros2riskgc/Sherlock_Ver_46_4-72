using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsPoliticaBLL
    {
        /// <summary>
        /// Realiza la consulta de la Politica Calidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public clsPoliticaCalidad mtdConsultarPoliticaCalidad(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPoliticaCalidad cDtPoliticaCalidad = new clsDtPoliticaCalidad();
            clsPoliticaCalidad objPoliticaCalidad = new clsPoliticaCalidad();
            #endregion Vars

            dtInfo = cDtPoliticaCalidad.mtdConsultarPoliticaCalidad(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objPoliticaCalidad = new clsPoliticaCalidad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["NombreUsuario"].ToString().Trim());
                    }
                }
                else
                {
                    objPoliticaCalidad = null;
                    //strErrMsg = "No hay información de Politica de calidad.";
                }
            }
            else
                objPoliticaCalidad = null;

            return objPoliticaCalidad;
        }

        /// <summary>
        /// Realiza la consulta de la Politica Calidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsPoliticaCalidad> mtdConsultarPoliticasCalidad(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPoliticaCalidad cDtPoliticaCalidad = new clsDtPoliticaCalidad();
            clsPoliticaCalidad objPoliticaCalidad = new clsPoliticaCalidad();
            List<clsPoliticaCalidad> lstPolitica = new List<clsPoliticaCalidad>();
            #endregion Vars

            dtInfo = cDtPoliticaCalidad.mtdConsultarPoliticaCalidad(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objPoliticaCalidad = new clsPoliticaCalidad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["NombreUsuario"].ToString().Trim());
                        lstPolitica.Add(objPoliticaCalidad);
                    }
                }
                else
                {
                    lstPolitica = null;
                    //strErrMsg = "No hay información de Politica de calidad.";
                }
            }
            else
                lstPolitica = null;

            return lstPolitica;
        }

        /// <summary>
        /// Permite la insercion de la politica de calidad
        /// </summary>
        /// <param name="objPolitica"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public bool mtdInsertarPoliticaCalidad(clsPoliticaCalidad objPolitica, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtPoliticaCalidad cDtPolitica = new clsDtPoliticaCalidad();

            booResult = cDtPolitica.mtdInsertarPoliticaCalidad(objPolitica, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite la actualizacion de la politica de calidad
        /// </summary>
        /// <param name="objPolitica"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public bool mtdActualizarPoliticaCalidad(clsPoliticaCalidad objPolitica, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtPoliticaCalidad cDtPolitica = new clsDtPoliticaCalidad();

            booResult = cDtPolitica.mtdActualizarPoliticaCalidad(objPolitica, ref strErrMsg);

            return booResult;
        }

    }
}