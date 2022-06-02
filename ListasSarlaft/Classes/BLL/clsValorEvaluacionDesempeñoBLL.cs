using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsValorEvaluacionDesempeñoBLL
    {
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarValorEvaluacionDesempeño(clsEvaluacionDesempeno objEvaluacionDesempeño, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionDesempeño cDtDesempeño = new clsDtValorEvaluacionDesempeño();

            booResult = cDtDesempeño.mtdInsertarValorEvaluacionDesempeño(objEvaluacionDesempeño, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de desempeño
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdEvaluacionDesempeño(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtValorEvaluacionDesempeño cDtDesempeño = new clsDtValorEvaluacionDesempeño();
            DataTable dt = cDtDesempeño.mtdLastIdEvaluacionDesempeño(ref strErrMsg);
            if (LastId != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    LastId = Convert.ToInt32(dr["LastId"].ToString());
                }
            }
                return LastId;
        }
        /// <summary>
        /// Metodo que permite la insercion del detalle de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarValorDetalleDesempeño(ref string nombreFactor,ref decimal valorPuntaje, ref int IdEvaluacionDesempeño, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionDesempeño cDtDesempeño = new clsDtValorEvaluacionDesempeño();

            booResult = cDtDesempeño.mtdInsertarValorDetalleDesempeño(ref nombreFactor, ref valorPuntaje, ref IdEvaluacionDesempeño, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEvaluacionDesempeno> mtdConsultarEvaluacionDesempeño(ref clsEvaluacionDesempeno objEvaCompOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionDesempeño cDtDesempeño = new clsDtValorEvaluacionDesempeño();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsEvaluacionDesempeno> lstEvaluacionDesempeño = new List<clsEvaluacionDesempeno>();
            booResult = cDtDesempeño.mtdConsultarEvaluacionesDesempeño(ref dtCaracOut, ref strErrMsg);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsEvaluacionDesempeno(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                               dr["NombreEvaluacion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                                dr["NombreHijo"].ToString().Trim(),
                                dr["FechaEvaluacion"].ToString().Trim(),
                                dr["Evaluador"].ToString().Trim(),
                                Convert.ToDecimal(dr["Calificacion"].ToString().Trim()),
                                dr["RecomCapacitacion"].ToString().Trim(),
                                dr["RecomCompromisos"].ToString().Trim(),
                                dr["Otros"].ToString().Trim(),
                                dr["DescripcionOtros"].ToString().Trim(),
                                dr["FechaProximaEvaluacion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["Usuario"].ToString().Trim(),
                                Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim())
                                );
                            lstEvaluacionDesempeño.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionDesempeño = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionDesempeño = null;
            }

            return lstEvaluacionDesempeño;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEvaluacionDesempeno> mtdConsultarReporteEvaluacionDesempeño(ref clsEvaluacionDesempeno objEvaCompOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionDesempeño cDtDesempeño = new clsDtValorEvaluacionDesempeño();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsEvaluacionDesempeno> lstEvaluacionDesempeño = new List<clsEvaluacionDesempeno>();
            booResult = cDtDesempeño.mtdConsultarReporteEvaluacionesDesempeño(ref dtCaracOut, ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsEvaluacionDesempeno(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                               dr["NombreEvaluacion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                                dr["NombreHijo"].ToString().Trim(),
                                dr["FechaEvaluacion"].ToString().Trim(),
                                dr["Evaluador"].ToString().Trim(),
                                Convert.ToDecimal(dr["Calificacion"].ToString().Trim()),
                                dr["RecomCapacitacion"].ToString().Trim(),
                                dr["RecomCompromisos"].ToString().Trim(),
                                dr["Otros"].ToString().Trim(),
                                dr["DescripcionOtros"].ToString().Trim(),
                                dr["FechaProximaEvaluacion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["Usuario"].ToString().Trim(),
                                Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim())
                                );
                            lstEvaluacionDesempeño.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionDesempeño = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionDesempeño = null;
            }

            return lstEvaluacionDesempeño;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEvaluacionDesempeno> mtdConsultarReporteEvaluacionDesempeñoResponsable(ref clsEvaluacionDesempeno objEvaCompOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal, ref int IdResponsable)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionDesempeño cDtDesempeño = new clsDtValorEvaluacionDesempeño();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsEvaluacionDesempeno> lstEvaluacionDesempeño = new List<clsEvaluacionDesempeno>();
            booResult = cDtDesempeño.mtdConsultarReporteEvaluacionesDesempeñoResponsable(ref dtCaracOut, ref strErrMsg, ref fechaInicial, ref fechaFinal, ref IdResponsable);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsEvaluacionDesempeno(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                               dr["NombreEvaluacion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                                dr["NombreHijo"].ToString().Trim(),
                                dr["FechaEvaluacion"].ToString().Trim(),
                                dr["Evaluador"].ToString().Trim(),
                                Convert.ToDecimal(dr["Calificacion"].ToString().Trim()),
                                dr["RecomCapacitacion"].ToString().Trim(),
                                dr["RecomCompromisos"].ToString().Trim(),
                                dr["Otros"].ToString().Trim(),
                                dr["DescripcionOtros"].ToString().Trim(),
                                dr["FechaProximaEvaluacion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["Usuario"].ToString().Trim(),
                                Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim())
                                );
                            lstEvaluacionDesempeño.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionDesempeño = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionDesempeño = null;
            }

            return lstEvaluacionDesempeño;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDetalleEvaluacionDesempeño> mtdConsultarDetalleDesempeño(ref clsDetalleEvaluacionDesempeño objEvaCompOut, ref int IdEvaluacionDesempeño, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionDesempeño cDtDesempeño = new clsDtValorEvaluacionDesempeño();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsDetalleEvaluacionDesempeño> lstEvaluacionDesempeño = new List<clsDetalleEvaluacionDesempeño>();
            booResult = cDtDesempeño.mtdConsultarDetalleDesempeño(ref dtCaracOut, ref IdEvaluacionDesempeño, ref strErrMsg);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsDetalleEvaluacionDesempeño(
                                Convert.ToInt32(dr["IdValorFactor"].ToString().Trim()),
                                dr["nombreFactor"].ToString().Trim(),
                                Convert.ToDecimal(dr["valorFactor"].ToString().Trim()),
                                Convert.ToInt32(dr["IdEvaluacionDesempeño"].ToString().Trim())
                                );
                            lstEvaluacionDesempeño.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionDesempeño = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionDesempeño = null;
            }

            return lstEvaluacionDesempeño;
        }
        /// <summary>
        /// Metodo que permite la insercion del detalle de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateEvaluacionDesempeño(ref clsEvaluacionDesempeno objEvaluacionDesempeño, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionDesempeño cDtDesempeño = new clsDtValorEvaluacionDesempeño();

            booResult = cDtDesempeño.mtdUpdateEvaluacionDesempeño(ref objEvaluacionDesempeño, ref strErrMsg);

            return booResult;
        }
    }
}