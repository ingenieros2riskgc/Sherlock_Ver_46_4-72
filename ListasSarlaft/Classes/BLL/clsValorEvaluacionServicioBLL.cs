using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsValorEvaluacionServicioBLL
    {
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsValorEvaluacionServicio> mtdConsultarEncuesta(ref clsValorEvaluacionServicio objEvaCompOut, ref string strErrMsg, ref int IdEncuesta)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsValorEvaluacionServicio> lstEvaluacionServicio = new List<clsValorEvaluacionServicio>();
            booResult = cDtServicio.mtdConsultarEncuesta(ref dtCaracOut, ref strErrMsg, ref IdEncuesta);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsValorEvaluacionServicio();
                            objEvaCompOut.strNombreEncuesta = dr["NombreEncuesta"].ToString().Trim();
                            objEvaCompOut.intCantidadPregunta = Convert.ToInt32(dr["CantPregunta"].ToString().Trim());
                            objEvaCompOut.strDescripcionEmpresa = dr["DescripcionEmpresa"].ToString().Trim();
                            objEvaCompOut.strTextoPregunta = dr["TextoPregunta"].ToString().Trim();
                            objEvaCompOut.intIdPregunta = Convert.ToInt32(dr["IdPregunta"].ToString().Trim());
                            lstEvaluacionServicio.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionServicio = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionServicio = null;
            }

            return lstEvaluacionServicio;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarValorEvaluacionServicio(clsEvaluacionServicio objEvaluacionServicio, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();

            booResult = cDtServicio.mtdInsertarValorEvaluacionServicio(objEvaluacionServicio, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de proveedor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdEvaluacionServicio(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();
            DataTable dt = cDtServicio.mtdLastIdEvaluacionServicio(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de proveedor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdIdEncuesta(ref string strErrMsg, ref int IdEvaluacionServicio)
        {
            int LastId = 0;
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();
            DataTable dt = cDtServicio.mtdIdEncuesta(ref strErrMsg, ref IdEvaluacionServicio);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["IdEncuesta"].ToString());
            }
            return LastId;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarValorRespuesta(clsValorRespuesta objRespuesta, ref string strErrMsg, ref int IdEncuesta)
        {
            bool booResult = false;
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();

            booResult = cDtServicio.mtdInsertarValorRespuesta(objRespuesta, ref strErrMsg, ref IdEncuesta);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarObservacion(clsObservacionServicio objObservacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();

            booResult = cDtServicio.mtdInsertarValorObservacion(objObservacion, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEvaluacionServicio> mtdConsultarEvaluacionServicio(ref clsEvaluacionServicio objEvaCompOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsEvaluacionServicio> lstEvaluacionServicio = new List<clsEvaluacionServicio>();
            booResult = cDtServicio.mtdConsultarEvaluacionServicio(ref dtCaracOut, ref strErrMsg);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsEvaluacionServicio();
                            objEvaCompOut.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objEvaCompOut.strCiudad = dr["CiudadEncuesta"].ToString().Trim();
                            objEvaCompOut.dtFecha = dr["FechaEncuesta"].ToString().Trim();
                            objEvaCompOut.intIdTipoEncuesta = Convert.ToInt32(dr["IdTipoEncuesta"].ToString().Trim());
                            if (Convert.ToInt32(dr["IdTipoEncuesta"].ToString().Trim()) == 1)
                            {
                                objEvaCompOut.strTipoEncuesta = "Presencial";
                            }
                            else
                            {
                                objEvaCompOut.strTipoEncuesta = "Telefónica";
                            }
                            objEvaCompOut.strNombreCliente = dr["NombreCliente"].ToString().Trim();
                            objEvaCompOut.strNombre = dr["Nombre"].ToString().Trim();
                            objEvaCompOut.strCargo = dr["Cargo"].ToString().Trim();
                            objEvaCompOut.strFuncionarios = dr["Funcionarios"].ToString().Trim();
                            objEvaCompOut.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objEvaCompOut.intIdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            objEvaCompOut.strUsuario = dr["Usuario"].ToString().Trim();
                            lstEvaluacionServicio.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionServicio = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionServicio = null;
            }

            return lstEvaluacionServicio;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsValorRespuesta> mtdConsultarEvaluacionRespuestas(ref clsValorRespuesta objEvaCompOut, ref string strErrMsg, ref int IdEvaServicio)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsValorRespuesta> lstEvaluacionServicio = new List<clsValorRespuesta>();
            booResult = cDtServicio.mtdConsultarEvaluacionRespuestas(ref dtCaracOut, ref strErrMsg, ref IdEvaServicio);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsValorRespuesta();
                            if (dr["respuesta"].ToString().Trim() == "0")
                                objEvaCompOut.strRespuesta = "NC";
                            else
                                objEvaCompOut.strRespuesta = dr["respuesta"].ToString().Trim();
                            objEvaCompOut.strPregunta = dr["TextoPregunta"].ToString().Trim();
                            
                            lstEvaluacionServicio.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionServicio = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionServicio = null;
            }

            return lstEvaluacionServicio;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsObservacionServicio> mtdConsultarEvaluacionObservacion(ref clsObservacionServicio objEvaCompOut, ref string strErrMsg, ref int IdEvaServicio)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsObservacionServicio> lstEvaluacionServicio = new List<clsObservacionServicio>();
            booResult = cDtServicio.mtdConsultarEvaluacionObservacion(ref dtCaracOut, ref strErrMsg, ref IdEvaServicio);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objEvaCompOut = new clsObservacionServicio();
                            objEvaCompOut.strObservacion = dr["descripcion"].ToString();

                            lstEvaluacionServicio.Add(objEvaCompOut);
                        }
                    }
                    else
                        lstEvaluacionServicio = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstEvaluacionServicio = null;
            }

            return lstEvaluacionServicio;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarValorObservacion(ref string strErrMsg, ref string observaciones, ref int IdEvaServicio)
        {
            bool booResult = false;
            clsDtValorEvaluacionServicio cDtServicio = new clsDtValorEvaluacionServicio();

            booResult = cDtServicio.mtdActualizarValorObservacion(ref strErrMsg, ref observaciones, ref IdEvaServicio);

            return booResult;
        }
    }
}