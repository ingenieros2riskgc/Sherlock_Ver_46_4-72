using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsBLLEventosVsUsuario
    {
        /// <summary>
        /// Metodo para consultar y visualizar los Eventos por Usuario
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de Eventos por usuario</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOEventosVsUsuario> mtdConsultarSerivios(ref List<clsDTOEventosVsUsuario> lstEventsvsUser, ref string strErrMsg, ref int IdUsuarioJerarquia)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALEventosVsUsuario cDtRegistro = new clsDALEventosVsUsuario();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarEventosVsUsuario(ref dtInfo, ref strErrMsg, ref IdUsuarioJerarquia);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOEventosVsUsuario objEventVsUser = new clsDTOEventosVsUsuario();
                            objEventVsUser.intIdEvento = Convert.ToInt32(dr["IdEvento"].ToString().Trim());
                            objEventVsUser.strCodigoEvento = dr["CodigoEvento"].ToString().Trim();
                            objEventVsUser.strDescripcionEvento = dr["DescripcionEvento"].ToString().Trim();
                            objEventVsUser.decCuantiaPerdida = Convert.ToDecimal(dr["CuantiaPerdida"].ToString().Trim());
                            objEventVsUser.intIdGeneraEvento = Convert.ToInt32(dr["IdGeneraEvento"].ToString().Trim());
                            objEventVsUser.intGeneraEvento = Convert.ToInt32(dr["GeneraEvento"].ToString().Trim());
                            objEventVsUser.strNombreGenerador = dr["GeneradorEvento"].ToString().Trim();
                            objEventVsUser.intIdResponsableEvento = Convert.ToInt32(dr["ResponsableEvento"].ToString().Trim());
                            objEventVsUser.strNombreResponsable = dr["ResponsableSolucion"].ToString().Trim();
                            objEventVsUser.intIdClase = Convert.ToInt32(dr["IdClase"].ToString().Trim());
                            objEventVsUser.strNombreClaseEvento = dr["NombreClaseEvento"].ToString().Trim();

                            lstEventsvsUser.Add(objEventVsUser);
                        }
                    }
                    else
                        lstEventsvsUser = null;
                }
                else
                    lstEventsvsUser = null;
            }

            return lstEventsvsUser;
        }
    }
}