using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLprocesoRiesgoIndicador
    {
        /// <summary>
        /// Metodo para insertar el proceso del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarProcesoRiesgoIndicador(clsDTOprocesoRiesgoIndicador proceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALprocesoRiesgoIndicador cDALproceso = new clsDALprocesoRiesgoIndicador();

            booResult = cDALproceso.mtdInsertarProcesoRiesgoIndicador(proceso, ref strErrMsg);

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
            clsDALprocesoRiesgoIndicador cDALproceso = new clsDALprocesoRiesgoIndicador();

            LastId = cDALproceso.mtdGetLastId(ref strErrMsg);

            return LastId;
        }
        /// <summary>
        /// Metodo para consultar el arbol del procesos al que pertenece el proceso del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna Array de Objetos con los Procesos</returns>
        public object[] mtdConsultarProcesos(ref string strErrMsg, int IdProceso, int IdTipoProceso)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            object[] objOut = new object[2];
            List<clsProcesoIndicador> lstProceso = new List<clsProcesoIndicador>();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            #endregion Vars

            /*dtInfo = cDtProcInd.mtdConsultarProcIndicadorIdProcesoDV(objProcIN, ref strErrMsg, IdTipoProceso);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {*/
            objProcInd = new clsProcesoIndicador(
                 0,
                 IdTipoProceso,
                 IdProceso,
                 0,
                 null);
            //}

            switch (objProcInd.intIdTipoProceso)
            {
                case 1://MACROPROCESO
                    clsMacroproceso objMPIn = new clsMacroproceso(objProcInd.intIdProceso,
                        string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty, string.Empty),
                        objMPOut = new clsMacroproceso();
                    clsMacroProcesoBLL cMP = new clsMacroProcesoBLL();
                    objMPOut = cMP.mtdConsultarMacroproceso(objMPIn, ref strErrMsg);
                    objOut[0] = "M";
                    objOut[1] = objMPOut;
                    break;
                case 2://PROCESO
                    clsProceso objPIn = new clsProceso(objProcInd.intIdProceso, 0,
                        string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty),
                        objPOut = new clsProceso();
                    clsProcesoBLL cP = new clsProcesoBLL();
                    objPOut = cP.mtdConsultarProceso(objPIn, ref strErrMsg);
                    objOut[0] = "P";
                    objOut[1] = objPOut;
                    break;
                case 3://SUBPROCESO
                    clsSubproceso objSPIn = new clsSubproceso(objProcInd.intIdProceso,
                        string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty),
                        objSPOut = new clsSubproceso();
                    clsSubprocesoBLL cSP = new clsSubprocesoBLL();
                    objSPOut = cSP.mtdConsultarSubProceso(objSPIn, ref strErrMsg);
                    objOut[0] = "S";
                    objOut[1] = objSPOut;
                    break;
            }
            /*}
            else
                objOut = null;
        }
        else
            objOut = null;*/

            return objOut;
        }
        /// <summary>
        /// Metodo para tomar el valor del tipo del proceso
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdGetTipoProceso(ref string strErrMsg, int IdProceso, int IdRiesgoProceso)
        {
            int LastId = 0;
            clsDALprocesoRiesgoIndicador cDALproceso = new clsDALprocesoRiesgoIndicador();

            LastId = cDALproceso.mtdGetTipoProceso(ref strErrMsg, IdProceso, IdRiesgoProceso);

            return LastId;
        }
    }
}