using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsCriterioServicioBLL
    {
        /// <summary>
        /// Metodo para insertar el control del documento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCriterioServicio(clsCriterioServicio cCriterioServicio, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCriterioServicio cDtServicio = new clsDtCriterioServicio();

            booResult = cDtServicio.mtdInsertarCriterioServicio(cCriterioServicio, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Auditorias por el MacroProceso
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsCriterioServicio> mtdConsultarCriterioServicio(ref List<clsCriterioServicio> lstCriterioServicio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCriterioServicio cDtRegistro = new clsDtCriterioServicio();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarCriterioServicio(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsCriterioServicio objControl = new clsCriterioServicio();
                            objControl.intIdCriterioServicio = Convert.ToInt32(dr["IdCriterioServicio"].ToString().Trim());
                            objControl.intRangoInicial = Convert.ToDecimal(dr["RangoInicial"].ToString().Trim());
                            objControl.intRangoFinal = Convert.ToDecimal(dr["RangoFinal"].ToString().Trim());
                            objControl.strDescripcion = dr["DescripcionAprobacion"].ToString().Trim();
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objControl.strNombreUsuario = dr["Usuario"].ToString().Trim();

                            lstCriterioServicio.Add(objControl);
                        }
                    }
                    else
                        lstCriterioServicio = null;
                }
                else
                    lstCriterioServicio = null;
            }

            return lstCriterioServicio;
        }
        /// <summary>
        /// Metodo para insertar el control del documento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateCriterioServicio(clsCriterioServicio cCriterioServicio, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCriterioServicio cDtServicio = new clsDtCriterioServicio();

            booResult = cDtServicio.mtdUpdateCriterioServicio(cCriterioServicio, ref strErrMsg);

            return booResult;
        }
    }
}