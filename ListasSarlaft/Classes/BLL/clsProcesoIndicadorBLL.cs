using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsProcesoIndicadorBLL
    {
        /// <summary>
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public object[] mtdConsultarProcIndicador(clsProcesoIndicador objProcIN, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            object[] objOut = new object[2];
            List<clsProcesoIndicador> lstProceso = new List<clsProcesoIndicador>();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            #endregion Vars

            dtInfo = cDtProcInd.mtdConsultarProcIndicador(objProcIN, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProcInd = new clsProcesoIndicador(
                             Convert.ToInt32(dr["Id"].ToString().Trim()),
                             Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                             dr["FechaRegistro"].ToString().Trim());
                    }

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
                                string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty,string.Empty,string.Empty),
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
                }
                else
                    objOut = null;
            }
            else
                objOut = null;

            return objOut;
        }
        /// <summary>
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public object[] mtdConsultarProcIndicadorMod(clsProcesoIndicador objProcIN, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            object[] objOut = new object[2];
            List<clsProcesoIndicador> lstProceso = new List<clsProcesoIndicador>();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            #endregion Vars

            dtInfo = cDtProcInd.mtdConsultarProcIndicadorMod(objProcIN, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProcInd = new clsProcesoIndicador(
                             Convert.ToInt32(dr["Id"].ToString().Trim()),
                             Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                             dr["FechaRegistro"].ToString().Trim());
                    }

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
                                string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty),
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
                }
                else
                    objOut = null;
            }
            else
                objOut = null;

            return objOut;
        }
        /// <summary>
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public object[] mtdConsultarProcIndicadorIdProceso(clsProcesoIndicador objProcIN, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            object[] objOut = new object[2];
            List<clsProcesoIndicador> lstProceso = new List<clsProcesoIndicador>();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            #endregion Vars

            dtInfo = cDtProcInd.mtdConsultarProcIndicadorIdProceso(objProcIN, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProcInd = new clsProcesoIndicador(
                             Convert.ToInt32(dr["Id"].ToString().Trim()),
                             Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                             dr["FechaRegistro"].ToString().Trim());
                    }

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
                                string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty),
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
                }
                else
                    objOut = null;
            }
            else
                objOut = null;

            return objOut;
        }
        /// <summary>
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public object[] mtdConsultarProcesoNoConformidad(clsProcesoIndicador objProcIN, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            object[] objOut = new object[2];
            List<clsProcesoIndicador> lstProceso = new List<clsProcesoIndicador>();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            #endregion Vars

            dtInfo = cDtProcInd.mtdConsultarProcesoNoConformidad(objProcIN, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProcInd = new clsProcesoIndicador(
                             Convert.ToInt32(dr["IdNocoformidadProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             0,
                             "");
                    }

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
                                string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty),
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
                }
                else
                    objOut = null;
            }
            else
                objOut = null;

            return objOut;
        }
        public object[] mtdConsultarProcIndicadorIdProcesoDV(clsProcesoIndicador objProcIN, ref string strErrMsg, int IdTipoProceso)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            object[] objOut = new object[2];
            List<clsProcesoIndicador> lstProceso = new List<clsProcesoIndicador>();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            #endregion Vars

            dtInfo = cDtProcInd.mtdConsultarProcIndicadorIdProcesoDV(objProcIN, ref strErrMsg, IdTipoProceso);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProcInd = new clsProcesoIndicador(
                             Convert.ToInt32(dr["Id"].ToString().Trim()),
                             Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                             dr["FechaRegistro"].ToString().Trim());
                    }

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
                                string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty),
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
                }
                else
                    objOut = null;
            }
            else
                objOut = null;

            return objOut;
        }
        public object[] mtdConsultarProcesoVersion(ref string strErrMsg,int IdProceso, int IdTipoProceso, int idVersion)
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
                             idVersion,
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
                                string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty),
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
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public object[] mtdConsultarProcesoControl(int IdControl, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            object[] objOut = new object[2];
            List<clsProcesoIndicador> lstProceso = new List<clsProcesoIndicador>();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            #endregion Vars

            dtInfo = cDtProcInd.mtdConsultarProcesoControl(IdControl, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProcInd = new clsProcesoIndicador(
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdControlProducto"].ToString().Trim()),
                             null);
                    }

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
                }
                else
                    objOut = null;
            }
            else
                objOut = null;

            return objOut;
        }
        /// <summary>
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public object[] mtdConsultarProcesoEvaCompetencia(int IdEvaluacion, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            object[] objOut = new object[2];
            List<clsProcesoIndicador> lstProceso = new List<clsProcesoIndicador>();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            #endregion Vars

            dtInfo = cDtProcInd.mtdConsultarProcesoEvaCompetencia(IdEvaluacion, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProcInd = new clsProcesoIndicador(
                             Convert.ToInt32(dr["IdEvaTipoProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdEvaluacion"].ToString().Trim()),
                             null);
                    }

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
                                string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty),
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
                }
                else
                    objOut = null;
            }
            else
                objOut = null;

            return objOut;
        }
        /// <summary>
        /// Inserta el proceso del indicador
        /// </summary>
        /// <param name="objProcIN">Objeto con la informacion del proceso del indicador</param>
        /// <param name="intIdProcInd">Informacion del proceso del indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la transaccion</returns>
        public bool mtdInsertarProcesoIndicador(clsProcesoIndicador objProcIN, ref int intIdProcInd, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();

            booResult = cDtProcInd.mtdInsertarProcesoIndicador(objProcIN, ref intIdProcInd, ref  strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Actualiza el proceso del indicador
        /// </summary>
        /// <param name="objProcIN">Objeto con la informacion del proceso del indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la transaccion</returns>
        public bool mtdActualizarProcesoIndicador(clsProcesoIndicador objProcIN, ref string strErrMsg)
        {
            bool booResult = false;

            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();

            booResult = cDtProcInd.mtdActualizarProcesoIndicador(objProcIN, ref  strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Consulta el proceso del indicador
        /// </summary>
        /// <param name="objProcIN">Objeto con la informacion del proceso del indicador</param>
        /// <param name="lstProcesoIndicador">Lista con la informacion del proceso del indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la transaccion</returns>
        public bool mtdConsultarProcIndicador(clsProcesoIndicador objProcIN, ref List<clsProcesoIndicador> lstProcesoIndicador, 
            ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtInformacion = new DataTable();
            clsDtProcesoIndicador cDtProcInd = new clsDtProcesoIndicador();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();

            try
            {
                booResult = cDtProcInd.mtdConsultarProcIndicador(objProcIN, ref dtInformacion, ref strErrMsg);

                if (booResult)
                    if (dtInformacion != null)
                    {
                        if (dtInformacion.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtInformacion.Rows)
                            {
                                objProcInd = new clsProcesoIndicador(
                                 Convert.ToInt32(dr["Id"].ToString().Trim()),
                                 Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                                 Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                                 Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                 dr["FechaRegistro"].ToString().Trim());
                                lstProcesoIndicador.Add(objProcInd);
                            }
                        }
                        else
                            dtInformacion = null;
                    }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            return booResult;
        }
    }
}