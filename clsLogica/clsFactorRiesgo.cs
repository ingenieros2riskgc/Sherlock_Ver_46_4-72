using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using clsDatos;
using clsDTO;

namespace clsLogica
{
    public class clsFactorRiesgo
    {
        #region Factores
        public List<clsDTOFactorRiesgo> mtdConsultarFactor(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtFactorRiesgo cDtFactor = new clsDtFactorRiesgo();
            clsDTOFactorRiesgo objFactor = new clsDTOFactorRiesgo();
            List<clsDTOFactorRiesgo> lstFactor = new List<clsDTOFactorRiesgo>();
            #endregion

            dtInfo = cDtFactor.mtdConsultarFactor(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdFactorRiesgo], [CodigoFactorRiesgo], [DescFactorRiesgo] 
                        objFactor = new clsDTOFactorRiesgo(
                            dr["IdFactorRiesgo"].ToString().Trim(),
                            dr["CodigoFactorRiesgo"].ToString().Trim(),
                            dr["DescFactorRiesgo"].ToString().Trim(),
                            dr["IdUsuario"].ToString().Trim());

                        lstFactor.Add(objFactor);
                    }
                }
            }
            else
                lstFactor = null;

            return lstFactor;
        }

        public int mtdAgregarFactor(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            clsDtFactorRiesgo cDtFactor = new clsDtFactorRiesgo();
            int intUltimoInsertado = 0;

            intUltimoInsertado = cDtFactor.mtdAgregarFactorRet(objFactor, ref strErrMsg);

            return intUltimoInsertado;
        }

        public void mtdActualizarFactor(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            clsDtFactorRiesgo cDtFactor = new clsDtFactorRiesgo();

            cDtFactor.mtdActualizarFactor(objFactor, ref strErrMsg);
        }

        public void mtdEliminarFactor(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            clsDtFactorRiesgo cDtFactor = new clsDtFactorRiesgo();
            cDtFactor.mtdEliminarRelacion(objFactor, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                cDtFactor.mtdEliminarFactor(objFactor, ref strErrMsg);
        }
        #endregion

        #region Relación Factor de Riesgo - Señal
        public void mtdEliminarRelacion(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            clsDtFactorRiesgo cDtFactor = new clsDtFactorRiesgo();

            cDtFactor.mtdEliminarRelacion(objFactor, ref strErrMsg);
        }

        public void mtdGuardarRelacion(clsDTOFactorSenal objRelacion, ref string strErrMsg)
        {
            clsDtFactorRiesgo cDtFactor = new clsDtFactorRiesgo();

            cDtFactor.mtdGuardarRelacion(objRelacion, ref strErrMsg);
        }

        public List<clsDTOFactorSenal> mtdConsultarRelacion(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDTOFactorSenal ObjRelacion = null;
            List<clsDTOFactorSenal> lstRelacion = new List<clsDTOFactorSenal>();
            clsDtFactorRiesgo cDtFactor = new clsDtFactorRiesgo();
            #endregion

            dtInfo = cDtFactor.mtdConsultarRelacion(objFactor, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdFactorSenal],[IdFactorRiesgo],[IdSenal]
                        ObjRelacion = new clsDTOFactorSenal(
                            dr["IdFactorSenal"].ToString().Trim(),
                            dr["IdFactorRiesgo"].ToString().Trim(),
                            dr["IdSenal"].ToString().Trim());

                        lstRelacion.Add(ObjRelacion);
                    }
                }
            }

            return lstRelacion;
        }

        public DataSet mtdConsultarRelRiesgoSenal(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            #region Vars
            DataSet dsInfo = new DataSet();
            clsDtFactorRiesgo cDtFactor = new clsDtFactorRiesgo();
            #endregion

            dsInfo = cDtFactor.mtdConsultarRelRiesgoSenal(objFactor, ref strErrMsg);

            return dsInfo;
        }
        #endregion
    }
}
