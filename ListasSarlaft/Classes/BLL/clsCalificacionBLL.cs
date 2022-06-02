using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsCalificacionBLL
    {
        /// <summary>
        /// Metodo que permite la consulta de calificacion de la evaluacion
        /// </summary>
        /// <param name="lstCalificacion">Lista con la informacion de Calificaciones</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarCalificacion(ref List<clsCalificacionEvaluacion> lstCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCalificacion cDtCalificacion = new clsDtCalificacion();
            #endregion Vars

            booResult = cDtCalificacion.mtdConsultarCalificacion(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsCalificacionEvaluacion objCompetencia = new clsCalificacionEvaluacion(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdConfigEvaluacion"].ToString().Trim()),
                                 Convert.ToInt32(dr["IdEvaluacion"].ToString().Trim()),
                                dr["NombreEvaluacion"].ToString().Trim(),
                                Convert.ToDecimal(dr["ValorMin"].ToString().Trim()),
                                Convert.ToDecimal(dr["ValorMax"].ToString().Trim()),
                                dr["Descripcion"].ToString().Trim(),
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
        /// Metodo para consultar la calificacion de la evaluacion
        /// </summary>
        /// <param name="objEvaluacion">Objeto con la informacion de la evaluacion</param>
        /// <param name="lstCalificacion">Lista con la informacion de Calificaciones</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarCalificacion(clsEvaluacion objEvaluacion, ref List<clsCalificacionEvaluacion> lstCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCalificacion cDtCalificacion = new clsDtCalificacion();
            #endregion Vars

            booResult = cDtCalificacion.mtdConsultarCalificacion(objEvaluacion, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsCalificacionEvaluacion objCompetencia = new clsCalificacionEvaluacion(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdConfigEvaluacion"].ToString().Trim()),
                                 Convert.ToInt32(dr["IdEvaluacion"].ToString().Trim()),
                                dr["NombreEvaluacion"].ToString().Trim(),
                                Convert.ToDecimal(dr["ValorMin"].ToString().Trim()),
                                Convert.ToDecimal(dr["ValorMax"].ToString().Trim()),
                                dr["Descripcion"].ToString().Trim(),
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
        /// Metodo para insertar la informacion de la calificacion
        /// </summary>
        /// <param name="objCalificacion">Objeto con la informacion de la calificacion</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCalificacion(clsCalificacionEvaluacion objCalificacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCalificacion cDtCalificacion = new clsDtCalificacion();

            booResult = cDtCalificacion.mtdInsertarCalificacion(objCalificacion, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para insertar la informacion de la calificacion
        /// </summary>
        /// <param name="objCalificacion">Objeto con la informacion de la calificacion</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCalificacion(clsCalificacionEvaluacion objCalificacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCalificacion cDtCalificacion = new clsDtCalificacion();

            booResult = cDtCalificacion.mtdActualizarCalificacion(objCalificacion, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para consultar la evaluacion
        /// </summary>
        /// <param name="lstEvaluacion">Lista con la informacion de la evaluacion consultadas</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarEvaluacion(ref List<clsEvaluacion> lstEvaluacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCalificacion cDtCalificacion = new clsDtCalificacion();
            #endregion Vars

            booResult = cDtCalificacion.mtdConsultarEvaluacion(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsEvaluacion objEvaluacion = new clsEvaluacion(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["NombreEvaluacion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["FechaRegistro"].ToString().Trim());
                            lstEvaluacion.Add(objEvaluacion);
                        }
                    }
                    else
                        lstEvaluacion = null;
                }
                else
                    lstEvaluacion = null;
            }

            return booResult;
        }
    }
}