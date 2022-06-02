using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsValorEvaluacionCompetenciasBLL
    {
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarEvaluacionCompetencia(clsEvaluacionCompetencia objEvaluacionCompetencia, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();

            booResult = cDtCompetencia.mtdInsertarEvaluacionCompetencia(objEvaluacionCompetencia, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarTipoProcesoEvaluacionCompetencia(int IdTipoProceso, int IdProceso, int IdEvaluacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();

            booResult = cDtCompetencia.mtdInsertarTipoProcesoEvaluacionCompetencia(IdTipoProceso, IdProceso, IdEvaluacion, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de competencia
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdEvaluacionCompetencias(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();
            DataTable dt = cDtCompetencia.mtdLastIdEvaluacionCompetencia(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
        public string mtdNombreProceso(ref int idMacroProceso,ref string strErrMsg)
        {
            string NombreProceso = String.Empty;
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();
            DataTable dt = cDtCompetencia.mtdNombreProceso(ref idMacroProceso, ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                NombreProceso = dr["Nombre"].ToString();
            }
            return NombreProceso;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarVECtable(clsValorEvaluacionCompetencia objEvaluacionCompetencia, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();

            booResult = cDtCompetencia.mtdInsertarVECtable(objEvaluacionCompetencia, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el control del documento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarArchivo(string nombrearchivo, int length, byte[] archivo, int IdRegistro, ref string strErrMsg, string extension)
        {
            bool booResult = false;
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();

            booResult = cDtCompetencia.Guardar(nombrearchivo, length, archivo, IdRegistro, ref strErrMsg, extension);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite la insercion de la observacion de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la observacion la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarObservaciones(clsObservacionProveedor objObservaciones, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();

            booResult = cDtCompetencia.mtdInsertarObservaciones(objObservaciones, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite la actualizacion de la observacion de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la observacion la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizacionObservaciones(clsObservacionProveedor objObservaciones, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();

            booResult = cDtCompetencia.mtdActualizarObservaciones(objObservaciones, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEvaluacionCompetencia> mtdConsultarEvaluacionCompetencias(ref clsEvaluacionCompetencia objEvaCompOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsEvaluacionCompetencia> lstEvaluacionCompetencia = new List<clsEvaluacionCompetencia>();
            booResult = cDtCompetencia.mtdConsultarEvaluacionesCompetencias(ref dtCaracOut, ref strErrMsg);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsEvaluacionCompetencia(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                               dr["NombreEvaluado"].ToString().Trim(),
                                Convert.ToInt32(dr["CargoResponsable"].ToString().Trim()),
                                dr["NombreHijo"].ToString().Trim(),
                                dr["JefeInmediato"].ToString().Trim(),
                                dr["Nombre"].ToString().Trim(),
                                Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim()),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["Usuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim(),
                                Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim())
                                );
                            lstEvaluacionCompetencia.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionCompetencia = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionCompetencia = null;
            }

            return lstEvaluacionCompetencia;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Observaciones de la Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de las Observaciones de la evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsObservacionProveedor> mtdConsultarObservaciones(ref int IdEvaluacionCompetencia,ref clsObservacionProveedor objObservacionesCompOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionCompetencias cDtCompetencia = new clsDtValorEvaluacionCompetencias();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsObservacionProveedor> lstObservacionesCompetencia = new List<clsObservacionProveedor>();
            booResult = cDtCompetencia.mtdConsultarObservaciones(ref IdEvaluacionCompetencia,ref dtCaracOut, ref strErrMsg);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objObservacionesCompOut = new clsObservacionProveedor(
                               dr["Observaciones"].ToString().Trim(),
                                dr["FechaProxEvaluacion"].ToString().Trim()
                                );
                            lstObservacionesCompetencia.Add(objObservacionesCompOut);
                        }
                    }
                    else
                        lstObservacionesCompetencia = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstObservacionesCompetencia = null;
            }

            return lstObservacionesCompetencia;
        }
        /// <summary>
        /// Metodo que permite consultar todos los Controles de la Version
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public byte[] mtdDownLoadFile(ref string strErrMsg, ref int IdVersionDocumento, string filename)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtValorEvaluacionCompetencias cDtControl = new clsDtValorEvaluacionCompetencias();
            byte[] file = null;
            booResult = cDtControl.mtdDownLoadFile(ref dtInfo, ref strErrMsg, ref IdVersionDocumento, filename);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            file = (byte[])dr["archivo"];
                        }
                    }
                    else
                        file = null;
                }
                else
                    file = null;
            }
            return file;
        }
    }
}