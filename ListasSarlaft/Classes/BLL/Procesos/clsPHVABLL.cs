using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsPHVABLL
    {
        /// <summary>
        /// Realiza la consulta de la lsita de PHVA
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsPHVA> mtdConsultarPHVA(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsPHVA> lstPHVA = new List<clsPHVA>();
            clsPHVADLL cDtphva = new clsPHVADLL();
            clsPHVA objPHVA = new clsPHVA();
            #endregion Vars

            dtInfo = cDtphva.mtdConsultarPHVA(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objPHVA = new clsPHVA(
                            Convert.ToInt32(dr["Idphva"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Codigo"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim())
                            );
                        lstPHVA.Add(objPHVA);
                    }
                }
                else
                {
                    lstPHVA = null;
                    strErrMsg = "No hay información de la lista de PHVA.";
                }
            }
            else
                lstPHVA = null;

            return lstPHVA;
        }
    }
}