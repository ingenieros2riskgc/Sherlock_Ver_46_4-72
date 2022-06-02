using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAnulacionRiesgoBLL
    {
        /// <summary>
        /// Metodo para consultar y visualizar las Categorias de las Variables de Calificación Control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsAnulacionRiesgoDTO> mtdEventosRiesgos(ref List<clsAnulacionRiesgoDTO> lstCategoria, ref string strErrMsg, int IdRiesgo)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsAnulacionRiesgoDAL cDtRegistro = new clsAnulacionRiesgoDAL();
            #endregion Vars

            booResult = cDtRegistro.mtdEventosRiesgos(ref dtInfo, ref strErrMsg, IdRiesgo);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsAnulacionRiesgoDTO objCategoria = new clsAnulacionRiesgoDTO();
                            objCategoria.intIdEventoRiesgo = Convert.ToInt32(dr["IdEventoRiesgo"].ToString().Trim());
                            objCategoria.intIdRiesgo = Convert.ToInt32(dr["IdRiesgo"].ToString().Trim());
                            objCategoria.intIdEvento = Convert.ToInt32(dr["IdEvento"].ToString().Trim());
                            objCategoria.strCodigoEvento = dr["CodigoEvento"].ToString().Trim();
                            objCategoria.strDescripcionEvento = dr["DescripcionEvento"].ToString().Trim();
                            

                            lstCategoria.Add(objCategoria);
                        }
                    }
                    else
                        lstCategoria = null;
                }
                else
                    lstCategoria = null;
            }

            return lstCategoria;
        }
    }
}