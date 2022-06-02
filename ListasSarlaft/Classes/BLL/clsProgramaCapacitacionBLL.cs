using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsProgramaCapacitacionBLL
    {
        /// <summary>
        /// Metodo que permite la insercion de la programacion de capacitaciones
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la programacion de capacitaciones</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarProgramaCapacitacion(clsProgramacionCapacitacion objPrograma, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtprogramaCapacitacion cDtPrograma = new clsDtprogramaCapacitacion();

            booResult = cDtPrograma.mtdInsertarProgramaCapacitacion(objPrograma, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los Programas de las Capacitaciones
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsProgramacionCapacitacion> mtdConsultarPrograma(ref List<clsProgramacionCapacitacion> lstPrograma, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtprogramaCapacitacion cDtPrograma = new clsDtprogramaCapacitacion();
            #endregion Vars

            booResult = cDtPrograma.mtdConsultarPrograma(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsProgramacionCapacitacion objControl = new clsProgramacionCapacitacion();
                                objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                                objControl.IdMacroProceso = Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim());
                                objControl.strNombreProceso = dr["Nombre"].ToString().Trim();
                                objControl.IdCargoResponsable = Convert.ToInt32(dr["CargoResponsable"].ToString().Trim());
                                objControl.strCargoResponsable = dr["NombreHijo"].ToString().Trim();
                                objControl.strActividad = dr["Actividad"].ToString().Trim();
                                objControl.strDirigidoA = dr["Dirigido"].ToString().Trim();
                                objControl.strAsistentes = dr["asistentes"].ToString().Trim();
                                objControl.dtFechaProgramada = dr["FechaProgramada"].ToString().Trim();
                                //if(dr["FechaRealizada"].ToString().Trim() != "")
                                objControl.dtFechaRealizada = dr["FechaRealizada"].ToString().Trim();
                                objControl.intEvaluacion = dr["Evaluacion"].ToString().Trim();
                                objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                                objControl.strUsuario = dr["Usuario"].ToString().Trim();
                                objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                                
                            lstPrograma.Add(objControl);
                        }
                    }
                    else
                        lstPrograma = null;
                }
                else
                    lstPrograma = null;
            }

            return lstPrograma;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los Programas de las Capacitaciones
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsProgramacionCapacitacion> mtdConsultarReportePrograma(ref List<clsProgramacionCapacitacion> lstPrograma, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtprogramaCapacitacion cDtPrograma = new clsDtprogramaCapacitacion();
            #endregion Vars

            booResult = cDtPrograma.mtdConsultarReportePrograma(ref dtInfo, ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsProgramacionCapacitacion objControl = new clsProgramacionCapacitacion();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objControl.IdMacroProceso = Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim());
                            objControl.strNombreProceso = dr["Nombre"].ToString().Trim();
                            objControl.IdCargoResponsable = Convert.ToInt32(dr["CargoResponsable"].ToString().Trim());
                            objControl.strCargoResponsable = dr["NombreHijo"].ToString().Trim();
                            objControl.strActividad = dr["Actividad"].ToString().Trim();
                            objControl.strDirigidoA = dr["Dirigido"].ToString().Trim();
                            objControl.strAsistentes = dr["asistentes"].ToString().Trim();
                            objControl.dtFechaProgramada = dr["FechaProgramada"].ToString().Trim();
                            //if(dr["FechaRealizada"].ToString().Trim() != "")
                            objControl.dtFechaRealizada = dr["FechaRealizada"].ToString().Trim();
                            objControl.intEvaluacion = dr["Evaluacion"].ToString().Trim();
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objControl.strUsuario = dr["Usuario"].ToString().Trim();
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());

                            lstPrograma.Add(objControl);
                        }
                    }
                    else
                        lstPrograma = null;
                }
                else
                    lstPrograma = null;
            }

            return lstPrograma;
        }
        /// <summary>
        /// Metodo que permite la insercion del detalle de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateProgramaCapacitacion(ref clsProgramacionCapacitacion objPrograma, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtprogramaCapacitacion cDtPrograma = new clsDtprogramaCapacitacion();

            booResult = cDtPrograma.mtdUpdateProgramaCapacitacion(ref objPrograma, ref strErrMsg);

            return booResult;
        }
    }
}