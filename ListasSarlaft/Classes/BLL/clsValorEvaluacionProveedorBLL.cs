using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsValorEvaluacionProveedorBLL
    {
        /// <summary>
        /// Metodo para consultar y visualizar los Criterios por Proveedor
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsProveedorCriterios> mtdConsultarProveedorCriterios(ref List<clsProveedorCriterios> lstProveedor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();
            #endregion Vars

            booResult = cDtProveedor.mtdConsultarProveedorCriterios(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsProveedorCriterios objControl = new clsProveedorCriterios(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["NombreAspecto"].ToString().Trim(),
                                Convert.ToDecimal(dr["ValorPorcentaje"].ToString().Trim()),
                                dr["FechaRegistro"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["DesCriterio"].ToString().Trim(),
                                dr["DesParametro"].ToString().Trim(),
                                dr["Usuario"].ToString().Trim(),
                                0
                                );
                            lstProveedor.Add(objControl);
                        }
                    }
                    else
                        lstProveedor = null;
                }
                else
                    lstProveedor = null;
            }

            return lstProveedor;
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
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            #endregion Vars

            dtInfo = cDtProveedor.mtdGetCalificaciones(ref strErrMsg);

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
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarValorEvaluacionProveedor(clsEvaluacionProveedor objEvaluacionProveedor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();

            booResult = cDtProveedor.mtdInsertarValorEvaluacionProveedor(objEvaluacionProveedor, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de proveedor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdEvaluacionProveedor(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();
            DataTable dt = cDtProveedor.mtdLastIdEvaluacionProveedor(ref strErrMsg);
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
        public bool mtdInsertarValorAspectoProveedor(clsAspectoProveedorHistorico objAspectoProveedor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();

            booResult = cDtProveedor.mtdInsertarValorAspectoProveedor(objAspectoProveedor, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de proveedor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdAspectoProveedor(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();
            DataTable dt = cDtProveedor.mtdLastIdAspectoProveedor(ref strErrMsg);
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
        public bool mtdInsertarValorCriterioProveedor(clsCriterioProvHistorico objCriterioProveedor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();

            booResult = cDtProveedor.mtdInsertarValorCriterioProveedor(objCriterioProveedor, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de proveedor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdCriterioProveedor(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();
            DataTable dt = cDtProveedor.mtdLastIdCriterioProveedor(ref strErrMsg);
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
        public bool mtdInsertarValorParametroProveedor(clsParametrosProvHistorico objParametroProveedor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();

            booResult = cDtProveedor.mtdInsertarValorParametroProveedor(objParametroProveedor, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los Criterios por Proveedor
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEvaluacionProveedor> mtdConsultarEvaluacionProveedor(ref List<clsEvaluacionProveedor> lstProveedor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();
            #endregion Vars

            booResult = cDtProveedor.mtdConsultarEvaluacionProveedor(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsEvaluacionProveedor objControl = new clsEvaluacionProveedor(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["NombreProveedor"].ToString().Trim(),
                                Convert.ToInt32(dr["CargoResponsable"].ToString().Trim()),
                                dr["NombreHijo"].ToString().Trim(),
                                dr["FechaEvaluacion"].ToString().Trim(),
                                dr["PeriodoEvaluacionInicial"].ToString().Trim(),
                                dr["PeriodoEvaluacionFinal"].ToString().Trim(),
                                dr["ServicioOfrecido"].ToString().Trim(),
                                dr["RealizadoPor"].ToString().Trim(),
                                
                                dr["Observaciones"].ToString().Trim(),
                                
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["Usuario"].ToString().Trim(),
                                Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim()),
                                dr["FechaProximaEvaluacion"].ToString().Trim()
                                );
                            lstProveedor.Add(objControl);
                        }
                    }
                    else
                        lstProveedor = null;
                }
                else
                    lstProveedor = null;
            }

            return lstProveedor;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los Criterios por Proveedor
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsProveedorCriterios> mtdConsultarProveedorCriteriosH(ref List<clsProveedorCriterios> lstProveedor, ref string strErrMsg, ref int IdEvalProveedor)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();
            #endregion Vars

            booResult = cDtProveedor.mtdConsultarProveedorCriteriosH(ref dtInfo, ref strErrMsg, ref IdEvalProveedor);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            decimal calificacion = 0;
                            decimal porcentaje = Convert.ToDecimal(dr["ValorPorcentaje"].ToString().Trim()) / 100;
                            decimal valor = Convert.ToDecimal(dr["Valor"].ToString().Trim());
                            calificacion = porcentaje * valor;
                            clsProveedorCriterios objControl = new clsProveedorCriterios(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["NombreAspecto"].ToString().Trim(),
                                Convert.ToDecimal(dr["ValorPorcentaje"].ToString().Trim()),
                                dr["FechaRegistro"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["DesCriterio"].ToString().Trim(),
                                dr["DesParametro"].ToString().Trim(),
                                dr["IdUsuario"].ToString().Trim(),
                                Convert.ToDecimal(dr["Valor"].ToString().Trim()),
                                calificacion
                                );
                            lstProveedor.Add(objControl);
                        }
                    }
                    else
                        lstProveedor = null;
                }
                else
                    lstProveedor = null;
            }

            return lstProveedor;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdModificarValorEvaluacionProveedor(clsEvaluacionProveedor objEvaluacionProveedor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtValorEvaluacionProveedor cDtProveedor = new clsDtValorEvaluacionProveedor();

            booResult = cDtProveedor.mtdModificarValorEvaluacionProveedor(objEvaluacionProveedor, ref strErrMsg);

            return booResult;
        }
    }
}