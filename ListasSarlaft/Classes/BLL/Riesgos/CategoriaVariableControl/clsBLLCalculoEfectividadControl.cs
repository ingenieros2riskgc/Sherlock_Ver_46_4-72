using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLCalculoEfectividadControl
    {
        /// <summary>
        /// Metodo para consultar y visualizar Valor porcrentaje Calificacion
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdConsultarValorProcentaje(ref string strErrMsg, string DescripcionVariable)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALCalculoEfectividadControl cDtRegistro = new clsDALCalculoEfectividadControl();
            int ValorPorcentaje = 0;
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarValorProcentaje(ref dtInfo, ref strErrMsg, DescripcionVariable);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            ValorPorcentaje = Convert.ToInt32(dr["ValorPorcentajeCalificarControl"].ToString());
                        }
                    }
                    else
                        ValorPorcentaje = 0;
                }
                else
                    ValorPorcentaje = 0;
            }

            return ValorPorcentaje;
        }
        /// <summary>
        /// Metodo para consultar y visualizar Valor maximo de la variable
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdConsultarMaxPeso(ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALCalculoEfectividadControl cDtRegistro = new clsDALCalculoEfectividadControl();
            int PesoCategoria = 0;
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarMaxPeso(ref dtInfo, ref strErrMsg, IdVariable);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            if (dr["Peso"].ToString() != "")
                                PesoCategoria = Convert.ToInt32(dr["Peso"].ToString());
                            else
                                PesoCategoria = 0;
                        }
                    }
                    else
                        PesoCategoria = 0;
                }
                else
                    PesoCategoria = 0;
            }

            return PesoCategoria;
        }
        /// <summary>
        /// Metodo para consultar y visualizar minimo de la variable
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdConsultarMinPeso(ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALCalculoEfectividadControl cDtRegistro = new clsDALCalculoEfectividadControl();
            int PesoCategoria = 0;
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarMinPeso(ref dtInfo, ref strErrMsg, IdVariable);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            if (dr["Peso"].ToString() != "")
                                PesoCategoria = Convert.ToInt32(dr["Peso"].ToString());
                            else
                                PesoCategoria = 0;
                        }
                    }
                    else
                        PesoCategoria = 0;
                }
                else
                    PesoCategoria = 0;
            }

            return PesoCategoria;
        }
        /// <summary>
        /// Metodo para consultar y visualizar el cuadro de calificacion
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOCalificacionControl> mtdConsultarCuadroCalificacion(ref List<clsDTOCalificacionControl> lstCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALCalculoEfectividadControl cDtRegistro = new clsDALCalculoEfectividadControl();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarCuadroCalificacion(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOCalificacionControl objCalificacion = new clsDTOCalificacionControl();
                            objCalificacion.intIdCalificacionControl = Convert.ToInt32(dr["IdCalificacionControl"].ToString().Trim());
                            objCalificacion.strNombreEscala = dr["NombreEscala"].ToString().Trim();
                            objCalificacion.intLimiteInferior = Convert.ToDouble(dr["LimiteInferior"].ToString().Trim());
                            objCalificacion.intLimiteSuperior = Convert.ToDouble(dr["LimiteSuperior"].ToString().Trim());

                            lstCalificacion.Add(objCalificacion);
                        }
                    }
                    else
                        lstCalificacion = null;
                }
                else
                    lstCalificacion = null;
            }

            return lstCalificacion;
        }
        /// <summary>
        /// Metodo para actualizar el registro de la calificacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateCuadorCalificacion(clsDTOCalificacionControl calificacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALCalculoEfectividadControl cDALcategoria = new clsDALCalculoEfectividadControl();

            booResult = cDALcategoria.mtdUpdateCuadorCalificacion(calificacion, ref strErrMsg);

            return booResult;
        }
    }
}