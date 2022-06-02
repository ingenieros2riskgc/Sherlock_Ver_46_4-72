using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsPlanMejoramientoBLL
    {
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarPlanMejoramiento(clsPlanDeMejoramiento objPlan, ref string strErrMsg)
        {
            bool booResult = false;
            clsPlanMejoramientoDAL cDTPlan = new clsPlanMejoramientoDAL();

            booResult = cDTPlan.mtdInsertarPlanMejoramiento(objPlan, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarPlanMejoramientoProceso(clsPlanDeMejoramiento objPlan, ref string strErrMsg, int IdPlan)
        {
            bool booResult = false;
            clsPlanMejoramientoDAL cDTPlan = new clsPlanMejoramientoDAL();

            booResult = cDTPlan.mtdInsertarPlanMejoramientoProceso(objPlan, ref strErrMsg, IdPlan);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de proveedor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdPlanMejoramiento(ref string strErrMsg)
        {
            int LastId = 0;
            clsPlanMejoramientoDAL cDTPlan = new clsPlanMejoramientoDAL();
            DataTable dt = cDTPlan.mtdLastIdPlanMejoramiento(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarDetallePlanMejoramiento(clsDetallePlanMejoramiento objDetallePlan, ref string strErrMsg)
        {
            bool booResult = false;
            clsPlanMejoramientoDAL cDTPlan = new clsPlanMejoramientoDAL();

            booResult = cDTPlan.mtdInsertarDetallePlanMejoramiento(objDetallePlan, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar todos los Controles de la Version
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsPlanDeMejoramiento> mtdConsultarPlanMejoramiento(ref string strErrMsg, ref List<clsPlanDeMejoramiento> lstPlan)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsPlanMejoramientoDAL cDtPlan = new clsPlanMejoramientoDAL();

            booResult = cDtPlan.mtdConsultarPlanMejoramiento(ref dtInfo, ref strErrMsg);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsPlanDeMejoramiento objControl = new clsPlanDeMejoramiento();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objControl.intIdMacroProceso = Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim());
                            objControl.strProceso = dr["Nombre"].ToString().Trim();
                            objControl.strDescripcionActividad = dr["DescripcionActividad"].ToString().Trim();
                            objControl.dtPeriodoEvaluarInicial = dr["PeriodoEvaluarInicial"].ToString().Trim();
                            objControl.dtPeriodoEvaluarFinal = dr["PeriodoEvaluarFinal"].ToString().Trim();
                            objControl.strPlanMejoramiento = dr["PlanMejoramiento"].ToString().Trim();
                            objControl.strRecursos = dr["Recursos"].ToString().Trim();
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            objControl.strUsuario = dr["Usuario"].ToString().Trim();
                            objControl.intIdTipoProceso = Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim());

                            lstPlan.Add(objControl);
                        }
                    }
                    else
                        lstPlan = null;
                }
                else
                    lstPlan = null;
            }
            return lstPlan;
        }
        /// <summary>
        /// Metodo que permite consultar todos los Controles de la Version
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDetallePlanMejoramiento> mtdConsultarDetallePlanMejoramiento(ref string strErrMsg, ref List<clsDetallePlanMejoramiento> lstPlan, ref int IdPlan)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsPlanMejoramientoDAL cDtPlan = new clsPlanMejoramientoDAL();

            booResult = cDtPlan.mtdConsultarDetallePlanMejoramiento(ref dtInfo, ref strErrMsg, ref IdPlan);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDetallePlanMejoramiento objControl = new clsDetallePlanMejoramiento();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objControl.intIdMacroProceso = Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim());
                            objControl.strActividad = dr["Actividad"].ToString().Trim();
                            objControl.dtFechaProgramada = dr["FechaProgramado"].ToString().Trim();
                            objControl.dtFechaRealizada = dr["FechaRealizado"].ToString().Trim();
                            objControl.strResponsable = dr["Responsable"].ToString().Trim();
                            objControl.strCargoResponsable = dr["NombreHijo"].ToString().Trim();
                            objControl.strArea = dr["Area"].ToString().Trim();
                            objControl.strSeguimiento = dr["Seguimiento"].ToString().Trim();
                            lstPlan.Add(objControl);
                        }
                    }
                    else
                        lstPlan = null;
                }
                else
                    lstPlan = null;
            }
            return lstPlan;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateDetallePlanMejoramiento(clsDetallePlanMejoramiento objDetallePlan, ref string strErrMsg)
        {
            bool booResult = false;
            clsPlanMejoramientoDAL cDTPlan = new clsPlanMejoramientoDAL();

            booResult = cDTPlan.mtdUpdateDetallePlanMejoramiento(objDetallePlan, ref strErrMsg);

            return booResult;
        }
    }
}