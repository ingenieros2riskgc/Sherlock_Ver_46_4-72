using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLseguimientoRiesgoIndicador
    {
        /// <summary>
        /// Metodo para insertar el seguimiento del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarSeguimientoRiesgoIndicador(clsDTOseguimientoRiesgoIndicador seguimiento, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALseguimientoRiesgoIndicador cDALseguimiento = new clsDALseguimientoRiesgoIndicador();

            booResult = cDALseguimiento.mtdInsertarSeguimientoRiesgoIndicador(seguimiento, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Realiza la consulta de los seguimientos del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOseguimientoRiesgoIndicador> mtdConsultarSeguimientoIndicador(bool booEstado, ref string strErrMsg, int IdRiesgoIndicador)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOseguimientoRiesgoIndicador> lstSeguimiento = new List<clsDTOseguimientoRiesgoIndicador>();
            clsDALseguimientoRiesgoIndicador cDtSeguimiento = new clsDALseguimientoRiesgoIndicador();
            clsDTOseguimientoRiesgoIndicador objSeguimiento = new clsDTOseguimientoRiesgoIndicador();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtSeguimiento.mtdConsultarSeguimientoIndicador(ref dtInfo, ref strErrMsg, IdRiesgoIndicador);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSeguimiento = new clsDTOseguimientoRiesgoIndicador();
                        objSeguimiento.intIdEsquemaSeguimiento = Convert.ToInt32(dr["IdEsquemaSeguimiento"].ToString().Trim());
                        objSeguimiento.intIdRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim());
                        objSeguimiento.dblValorMinimo = Convert.ToDouble(dr["ValorMinimo"].ToString().Trim());
                        objSeguimiento.dblValorMaximo = Convert.ToDouble(dr["ValorMaximo"].ToString().Trim());
                        objSeguimiento.strDescripcionSeguimiento = dr["DescripcionSeguimiento"].ToString().Trim();
                        objSeguimiento.strColor = dr["Color"].ToString().Trim();

                        lstSeguimiento.Add(objSeguimiento);
                    }
                }
                else
                {
                    lstSeguimiento = null;
                    strErrMsg = "No hay datos del seguimiento.";
                }
            }
            else
                objSeguimiento = null;

            return lstSeguimiento;
        }
        /// <summary>
        /// Metodo para actualizar el seguimiento del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizaSeguimientoRiesgoIndicador(clsDTOseguimientoRiesgoIndicador seguimiento, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALseguimientoRiesgoIndicador cDALseguimiento = new clsDALseguimientoRiesgoIndicador();

            booResult = cDALseguimiento.mtdActualizaSeguimientoRiesgoIndicador(seguimiento, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para eliminar el seguimiento del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdDeleteSeguimientoRiesgoIndicador(clsDTOseguimientoRiesgoIndicador seguimiento, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALseguimientoRiesgoIndicador cDALseguimiento = new clsDALseguimientoRiesgoIndicador();

            booResult = cDALseguimiento.mtdDeleteSeguimientoRiesgoIndicador(seguimiento, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para tomar el valor del ultimo id registrado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdGetLastId(ref string strErrMsg)
        {
            int LastId = 0;
            clsDALseguimientoRiesgoIndicador cDALseguimiento = new clsDALseguimientoRiesgoIndicador();

            LastId = cDALseguimiento.mtdGetLastId(ref strErrMsg);

            return LastId;
        }
    }
}