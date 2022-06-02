using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLmetaRiesgoIndicador
    {
        /// <summary>
        /// Metodo para insertar la meta del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarMetaRiesgoIndicador(clsDTOmetasRiesgoIndicador meta, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALmetaRiesgoIndicador cDALmeta = new clsDALmetaRiesgoIndicador();

            booResult = cDALmeta.mtdInsertarMetaRiesgoIndicador(meta, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Realiza la consulta de las metas del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOmetasRiesgoIndicador> mtdConsultarMetaRiesgoIndicador(bool booEstado, ref string strErrMsg, int IdRiesgoIndicador)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOmetasRiesgoIndicador> lstMetas = new List<clsDTOmetasRiesgoIndicador>();
            clsDALmetaRiesgoIndicador cDtmetas = new clsDALmetaRiesgoIndicador();
            clsDTOmetasRiesgoIndicador objMeta = new clsDTOmetasRiesgoIndicador();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtmetas.mtdConsultarMetaRiesgoIndicador(ref dtInfo, ref strErrMsg, IdRiesgoIndicador);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objMeta = new clsDTOmetasRiesgoIndicador();
                        objMeta.intIdMeta = Convert.ToInt32(dr["IdMeta"].ToString().Trim());
                        objMeta.intIdRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim());
                        objMeta.dblMeta = Convert.ToDouble(dr["Meta"].ToString().Trim());
                        objMeta.intIdDetalleFrecuencia = Convert.ToInt32(dr["IdDetalleFrecuenciaMedicion"].ToString().Trim());
                        objMeta.strDetalleFrecuencia = dr["Descripcion"].ToString().Trim();
                        objMeta.strValorOtraFrecuencia = dr["ValorOtraFrecuencia"].ToString().Trim();
                        objMeta.strAño = dr["Año"].ToString().Trim();
                        objMeta.strMes = dr["mes"].ToString().Trim();
                        lstMetas.Add(objMeta);
                    }
                }
                else
                {
                    lstMetas = null;
                    strErrMsg = "No hay registros de las variables.";
                }
            }
            else
                objMeta = null;

            return lstMetas;
        }
        public List<clsDTOmetasRiesgoIndicador> mtdConsultarMetaRiesgoIndicadorxMeta(bool booEstado, ref string strErrMsg, int IdRiesgoIndicador, int IdMeta)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOmetasRiesgoIndicador> lstMetas = new List<clsDTOmetasRiesgoIndicador>();
            clsDALmetaRiesgoIndicador cDtmetas = new clsDALmetaRiesgoIndicador();
            clsDTOmetasRiesgoIndicador objMeta = new clsDTOmetasRiesgoIndicador();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtmetas.mtdConsultarMetaRiesgoIndicadorxMeta(ref dtInfo, ref strErrMsg, IdRiesgoIndicador, IdMeta);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objMeta = new clsDTOmetasRiesgoIndicador();
                        objMeta.intIdMeta = Convert.ToInt32(dr["IdMeta"].ToString().Trim());
                        objMeta.intIdRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim());
                        objMeta.dblMeta = Convert.ToDouble(dr["Meta"].ToString().Trim());
                        objMeta.intIdDetalleFrecuencia = Convert.ToInt32(dr["IdDetalleFrecuenciaMedicion"].ToString().Trim());
                        objMeta.strDetalleFrecuencia = dr["Descripcion"].ToString().Trim();
                        objMeta.strValorOtraFrecuencia = dr["ValorOtraFrecuencia"].ToString().Trim();
                        objMeta.strAño = dr["Año"].ToString().Trim();
                        objMeta.strMes = dr["mes"].ToString().Trim();
                        lstMetas.Add(objMeta);
                    }
                }
                else
                {
                    lstMetas = null;
                    strErrMsg = "No hay registros de las variables.";
                }
            }
            else
                objMeta = null;

            return lstMetas;
        }
        /// <summary>
        /// Metodo para actualizar la meta del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarMetaRiesgoIndicador(clsDTOmetasRiesgoIndicador meta, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALmetaRiesgoIndicador cDALmeta = new clsDALmetaRiesgoIndicador();

            booResult = cDALmeta.mtdActualizarMetaRiesgoIndicador(meta, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para eliminar la meta del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdEliminarMetaRiesgoIndicador(string IdMeta, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALmetaRiesgoIndicador cDALmeta = new clsDALmetaRiesgoIndicador();

            booResult = cDALmeta.mtdEliminaMeta(IdMeta, ref strErrMsg);

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
            clsDALmetaRiesgoIndicador cDALmeta = new clsDALmetaRiesgoIndicador();

            LastId = cDALmeta.mtdGetLastId(ref strErrMsg);

            return LastId;
        }
    }
}