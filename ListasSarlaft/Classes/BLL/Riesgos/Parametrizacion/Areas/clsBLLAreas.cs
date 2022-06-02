using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLAreas
    {
        /// <summary>
        /// Metodo para consultar y visualizar las Frecuencia de los eventos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOAreas> mtdConsultarAreas(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALAreas cDtAreas = new clsDALAreas();
            List<clsDTOAreas> lstAreas = new List<clsDTOAreas>();
            #endregion Vars

            booResult = cDtAreas.mtdConsultarAreas(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOAreas objArea = new clsDTOAreas();
                            objArea.intIdArea = Convert.ToInt32(dr["IdArea"].ToString().Trim());
                            objArea.strNombreArea = dr["NombreArea"].ToString().Trim();

                            lstAreas.Add(objArea);
                        }
                    }
                    else
                        lstAreas = null;
                }
                else
                    lstAreas = null;
            }

            return lstAreas;
        }
    }
}