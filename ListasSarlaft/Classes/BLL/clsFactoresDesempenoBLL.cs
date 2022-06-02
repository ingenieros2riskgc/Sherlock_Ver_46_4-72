using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsFactoresDesempenoBLL
    {
        /// <summary>
        /// Metodo para consultar el factor
        /// </summary>
        /// <param name="lstCalificacion">Lista con la informacion de la calificacion</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarFactor(ref List<clsFactoresDesempeno> lstCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtFactor cDtFactor = new clsDtFactor();
            #endregion Vars

            booResult = cDtFactor.mtdConsultarFactor(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsFactoresDesempeno objCompetencia = new clsFactoresDesempeno(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["FactoresEvaluacion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());
                            lstCalificacion.Add(objCompetencia);
                        }
                    }
                    else
                        lstCalificacion = null;
                }
                else
                    lstCalificacion = null;
            }

            return booResult;
        }

        /// <summary>
        /// Metodo para insertar el factor
        /// </summary>
        /// <param name="objFactor">objeto con la informacion del factor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarFactor(clsFactoresDesempeno objFactor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtFactor cDtFactor = new clsDtFactor();

            booResult = cDtFactor.mtdInsertarFactor(objFactor, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para actualizar el factor
        /// </summary>
        /// <param name="objFactor">objeto con la informacion del factor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarFactor(clsFactoresDesempeno objFactor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtFactor cDtFactor = new clsDtFactor();

            booResult = cDtFactor.mtdActualizarFactor(objFactor, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para consultar el detalle del factor
        /// </summary>
        /// <param name="objFactor">objeto con la informacion del factor</param>
        /// <param name="lstDetFactor">lista con la informacion del factor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarDetFactor(clsFactoresDesempeno objFactor, ref List<clsDetalleFactorDesempeno> lstDetFactor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtFactor cDtFactor = new clsDtFactor();
            #endregion

            booResult = cDtFactor.mtdConsultarDetFactorEva(objFactor, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDetalleFactorDesempeno objDetFactor = new clsDetalleFactorDesempeno(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdFactorDesempeno"].ToString().Trim()),
                                dr["NombreFactor"].ToString().Trim(),
                                 Convert.ToInt32(dr["IdCalificacionEvaluacion"].ToString().Trim()),
                                  dr["CalificacionEvaluacion"].ToString().Trim(),
                                dr["Descripcion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim(),
                                Convert.ToDecimal(dr["CriterioMinimo"].ToString().Trim()),
                                Convert.ToDecimal(dr["CriterioMaximo"].ToString().Trim()));
                            lstDetFactor.Add(objDetFactor);
                        }
                    }
                    else
                        lstDetFactor = null;
                }
                else
                    lstDetFactor = null;
            }

            return booResult;
        }

        /// <summary>
        /// Metodo para insertar el detalle del factor
        /// </summary>
        /// <param name="objDetFactor">objeto con la informacion del factor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarDetFactor(clsDetalleFactorDesempeno objDetFactor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtFactor cDtFactor = new clsDtFactor();

            booResult = cDtFactor.mtdInsertarDetFactor(objDetFactor, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para actualizar el detalle del factor
        /// </summary>
        /// <param name="objDetFactor">objeto con la informacion del factor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarDetFactor(clsDetalleFactorDesempeno objDetFactor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtFactor cDtFactor = new clsDtFactor();

            booResult = cDtFactor.mtdActualizarDetFactor(objDetFactor, ref strErrMsg);

            return booResult;
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
            clsDtFactor cDtFactor = new clsDtFactor();
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            #endregion Vars

            dtInfo = cDtFactor.mtdGetCalificaciones(ref strErrMsg);

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
    }
}