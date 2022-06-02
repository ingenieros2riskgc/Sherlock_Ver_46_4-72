using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLServicios
    {
        /// <summary>
        /// Metodo para insertar el registro de servicio
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarServicios(clsDTOServicios servicios, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsServicios cDALservicios = new clsDALInsServicios();

            booResult = cDALservicios.mtdInsertarServicios(servicios, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los Servicios
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOServicios> mtdConsultarSerivios(ref List<clsDTOServicios> lstServicios, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALConServicios cDtRegistro = new clsDALConServicios();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarServicios(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOServicios objServicios = new clsDTOServicios();
                            objServicios.intId = Convert.ToInt32(dr["IdServicio"].ToString().Trim());
                            objServicios.strDescripcion = dr["Descripcion"].ToString().Trim();
                            objServicios.strUsuario = dr["Usuario"].ToString().Trim();
                            objServicios.dtFechaRegistro = dr["FechaRegistro"].ToString().Trim();
                            
                            lstServicios.Add(objServicios);
                        }
                    }
                    else
                        lstServicios = null;
                }
                else
                    lstServicios = null;
            }

            return lstServicios;
        }
        /// <summary>
        /// Metodo para actualizar el registro de servicio
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarServicios(clsDTOServicios servicios, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdServicios cDALservicios = new clsDALUpdServicios();

            booResult = cDALservicios.mtdActualizarServicios(servicios, ref strErrMsg);

            return booResult;
        }
    }
}