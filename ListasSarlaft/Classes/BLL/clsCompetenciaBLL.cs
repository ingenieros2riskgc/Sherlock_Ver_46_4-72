using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsCompetenciaBLL
    {
        #region Competencia
        /// <summary>
        /// Metodo para consultar las competencias
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Lista con la informacion de competencias</returns>
        public List<clsCompetencia> mtdConsultarCompetencia(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsCompetencia> lstCompetencia = new List<clsCompetencia>();
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();
            #endregion Vars

            dtInfo = cDtCompetencia.mtdConsultarCompetencia(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsCompetencia objCompetencia = new clsCompetencia(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            Convert.ToInt32(dr["Ponderacion"].ToString().Trim()),
                            dr["NombreCompetencia"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstCompetencia.Add(objCompetencia);
                    }
                }
                else
                    lstCompetencia = null;
            }
            else
                lstCompetencia = null;

            return lstCompetencia;
        }

        /// <summary>
        /// Metodo para consultar la competencia
        /// </summary>
        /// <param name="objCompetenciaIn">Objeto con la competencia de entrada</param>
        /// <param name="objCompetenciaOut">Objeto con la competencia de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarCompetencia(clsCompetencia objCompetenciaIn, ref clsCompetencia objCompetenciaOut, 
            ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();
            #endregion Vars

            booResult = cDtCompetencia.mtdConsultarCompetencia(objCompetenciaIn, ref dtInfo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objCompetenciaOut = new clsCompetencia(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            Convert.ToInt32(dr["Ponderacion"].ToString().Trim()),
                            dr["NombreCompetencia"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                    }
                }
                else
                    objCompetenciaOut = null;
            }
            else
                objCompetenciaOut = null;

            return booResult;
        }

        /// <summary>
        /// Metodo que permite la insercion de la competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCompetencia(clsCompetencia objCompetencia, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();

            booResult = cDtCompetencia.mtdInsertarCompetencia(objCompetencia, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo que permite la actualizacion de la competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCompetencia(clsCompetencia objCompetencia, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();

            booResult = cDtCompetencia.mtdActualizarCompetencia(objCompetencia, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para realizar la sumatoria del ponderado
        /// </summary>
        /// <param name="intPonderado">Numero par el ponderado</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdSumatoriaPonderado(ref int intPonderado, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();
            #endregion Vars

            booResult = cDtCompetencia.mtdSumatoriaPonderadoCompetencia(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            intPonderado = Convert.ToInt32(dr[0].ToString().Trim());
                        }
                    }
                    else
                        intPonderado = 0;
                }
                else
                    intPonderado = 0;
            }

            return booResult;
        }
        #endregion

        #region Criterio
        /// <summary>
        /// Metodo que permite la consulta de la competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la competencia</param>
        /// <param name="lstCriterio">Lista con la informacion de criterios</param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public bool mtdConsultarCriterio(clsCompetencia objCompetencia, ref List<clsCriterioCompetencia> lstCriterio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();
            #endregion Vars

            booResult = cDtCompetencia.mtdConsultarCriterio(objCompetencia, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsCriterioCompetencia objCriterio = new clsCriterioCompetencia(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdCompetencia"].ToString().Trim()),
                                dr["NombreCompetencia"].ToString().Trim(),
                                dr["Descripcion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());
                            lstCriterio.Add(objCriterio);
                        }
                    }
                    else
                        lstCriterio = null;
                }
                else
                    lstCriterio = null;
            }

            return booResult;
        }

        /// <summary>
        /// Metodo que permite la insercion del criterio
        /// </summary>
        /// <param name="objCriterio">objeto con la informacion del criterio</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCriterio(clsCriterioCompetencia objCriterio, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();

            booResult = cDtCompetencia.mtdInsertarCriterio(objCriterio, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo que permite la actualizacion del criterio
        /// </summary>
        /// <param name="objCriterio">objeto con la informacion del criterio</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCriterio(clsCriterioCompetencia objCriterio, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();

            booResult = cDtCompetencia.mtdActualizarCriterio(objCriterio, ref strErrMsg);

            return booResult;
        }
        #endregion
        #region Gestion Evaluacion Competencias
        /// <summary>
        /// Metodo que permite la consulta de la Gestion Evaluacion Competencias
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la competencia</param>
        /// <param name="lstCriterio">Lista con la informacion de criterios</param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public List<clsGestionEvaluacionCompetencias> mtdGetCompetencia(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();
            List<clsGestionEvaluacionCompetencias> lstGestionCompetencia = new List<clsGestionEvaluacionCompetencias>();
            #endregion Vars

            dtInfo = cDtCompetencia.mtdGetCriteriosCompetencias(ref strErrMsg);

            if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsGestionEvaluacionCompetencias objCriterio = new clsGestionEvaluacionCompetencias(
                                dr["Descripcion"].ToString().Trim(),
                                dr["NombreCompetencia"].ToString().Trim());
                            lstGestionCompetencia.Add(objCriterio);
                        }
                    }
                    else
                        lstGestionCompetencia = null;
                }
                else
                    lstGestionCompetencia = null;


            return lstGestionCompetencia;
        }
        /// <summary>
        /// Metodo que permite la consulta de la Gestion Evaluacion Competencias
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la competencia</param>
        /// <param name="lstCriterio">Lista con la informacion de criterios</param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public List<clsGestionEvaluacionCompetencias> mtdGetCompetenciaEvaVal(ref int IdEvaluacionCompetencia, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();
            List<clsGestionEvaluacionCompetencias> lstGestionCompetencia = new List<clsGestionEvaluacionCompetencias>();
            #endregion Vars

            dtInfo = cDtCompetencia.mtdGetCriteriosCompetenciasEvaVal(ref IdEvaluacionCompetencia,ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsGestionEvaluacionCompetencias objCriterio = new clsGestionEvaluacionCompetencias(
                            dr["Criterio"].ToString().Trim(),
                            dr["Competencia"].ToString().Trim(),
                            Convert.ToInt32(dr["PuntajeAsignado"].ToString().Trim())
                            );
                        lstGestionCompetencia.Add(objCriterio);
                    }
                }
                else
                    lstGestionCompetencia = null;
            }
            else
                lstGestionCompetencia = null;


            return lstGestionCompetencia;
        }
        /// <summary>
        /// Metodo que permite la consulta de las Calificaciones para las Evaluaciones
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la Calificaciones para las Evaluaciones</param>
        /// <param name="lstCriterio">Lista con la informacion de criterios</param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public List<clsCalificacionEvaluacion> mtdGetCalificaciones(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtCompetencia cDtCompetencia = new clsDtCompetencia();
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            #endregion Vars

            dtInfo = cDtCompetencia.mtdGetCalificaciones(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsCalificacionEvaluacion objCriterio = new clsCalificacionEvaluacion(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            Convert.ToDecimal(dr["ValorMin"].ToString().Trim()),
                            Convert.ToDecimal(dr["ValorMax"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["NombreEvaluacion"].ToString().Trim());
                        lstCalificaciones.Add(objCriterio);
                    }
                }
                else
                    lstCalificaciones = null;
            }
            else
                lstCalificaciones = null;


            return lstCalificaciones;
        }
        #endregion
    }
}