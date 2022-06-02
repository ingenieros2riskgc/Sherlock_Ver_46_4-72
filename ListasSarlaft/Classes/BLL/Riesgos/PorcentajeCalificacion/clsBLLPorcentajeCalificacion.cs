using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsBLLPorcentajeCalificacion
    {
        /// <summary>
        /// Metodo para insertar el porcentaje de la calificacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarPorcentajeCalificacion(clsDTOPorcentajeCalificacion porcentaje, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALPorcentajeCalificacion cDALporcentaje = new clsDALPorcentajeCalificacion();

            booResult = cDALporcentaje.mtdInsertarPorcentajeCalificacion(porcentaje, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para tomar el valor total de porcentajes
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdCantidadTotalPeso(ref string strErrMsg)
        {
            int Valormaximo = 0;
            clsDALPorcentajeCalificacion cDALporcentaje = new clsDALPorcentajeCalificacion();

            Valormaximo = cDALporcentaje.mtdCantidadTotalValorPorcentaje(ref strErrMsg);

            return Valormaximo;
        }
        /// <summary>
        /// Metodo para tomar el valor total de porcentajes
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdCantidadTotalPesoUp(ref string strErrMsg, int IdPorcentajeCalificarControl)
        {
            int Valormaximo = 0;
            clsDALPorcentajeCalificacion cDALporcentaje = new clsDALPorcentajeCalificacion();

            Valormaximo = cDALporcentaje.mtdCantidadTotalValorPorcentajeUp(ref strErrMsg, IdPorcentajeCalificarControl);

            return Valormaximo;
        }
        /// <summary>
        /// Metodo para validar la variable
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdValidarValorPorcentaje(ref string strErrMsg, string strNombreVariable)
        {
            int Valormaximo = 0;
            clsDALPorcentajeCalificacion cDALporcentaje = new clsDALPorcentajeCalificacion();

            Valormaximo = cDALporcentaje.mtdValidarValorPorcentaje(ref strErrMsg, strNombreVariable);

            return Valormaximo;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los porcentajes de la calificacion
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOPorcentajeCalificacion> mtdConsultarPorcentajes(ref List<clsDTOPorcentajeCalificacion> lstPorcentajes, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALPorcentajeCalificacion cDtporcentraje = new clsDALPorcentajeCalificacion();
            #endregion Vars

            booResult = cDtporcentraje.mtdConsultarPorcentajes(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOPorcentajeCalificacion objCategoria = new clsDTOPorcentajeCalificacion();
                            objCategoria.intIdPorcentajeCalificarControl = Convert.ToInt32(dr["IdPorcentajeCalificarControl"].ToString().Trim());
                            objCategoria.strNombrePorcentajeCalificarControl = dr["NombrePorcentajeCalificarControl"].ToString().Trim();
                            objCategoria.intValorPorcentajeCalificarControl = Convert.ToInt32(dr["ValorPorcentajeCalificarControl"].ToString().Trim());

                            lstPorcentajes.Add(objCategoria);
                        }
                    }
                    else
                        lstPorcentajes = null;
                }
                else
                    lstPorcentajes = null;
            }

            return lstPorcentajes;
        }
        /// <summary>
        /// Metodo para actualizar el registro del porcentaje de la calificacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdatePorcentaje(clsDTOPorcentajeCalificacion porcentaje, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALPorcentajeCalificacion cDALporcentaje = new clsDALPorcentajeCalificacion();

            booResult = cDALporcentaje.mtdUpdatePorcentaje(porcentaje, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los porcentajes de la calificacion x variable
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdConsultarPorcentajesxVariable(ref string strErrMsg, ref string variable)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALPorcentajeCalificacion cDtporcentraje = new clsDALPorcentajeCalificacion();
            int valorPorcentaje = 0;
            #endregion Vars

            booResult = cDtporcentraje.mtdConsultarPorcentajesxVariable(ref dtInfo, ref strErrMsg, ref variable);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        valorPorcentaje = Convert.ToInt32(dtInfo.Rows[0]["PesoVariable"].ToString());
                    }
                    
                }
            }

            return valorPorcentaje;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las calificaciones del control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOCalificacionControl> mtdConsultarCalificacionControl(ref List<clsDTOCalificacionControl> lstCalificacionControl, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALPorcentajeCalificacion cDtporcentraje = new clsDALPorcentajeCalificacion();
            #endregion Vars

            booResult = cDtporcentraje.mtdConsultarCalificacionControl(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOCalificacionControl objCalificacionControl = new clsDTOCalificacionControl();
                            objCalificacionControl.intIdCalificacionControl = Convert.ToInt32(dr["IdCalificacionControl"].ToString().Trim());
                            objCalificacionControl.strNombreEscala = dr["NombreEscala"].ToString().Trim();
                            objCalificacionControl.intLimiteInferior = Convert.ToDouble( dr["LimiteInferior"].ToString().Trim());
                            objCalificacionControl.intLimiteSuperior = Convert.ToDouble(dr["LimiteSuperior"].ToString().Trim());
                            objCalificacionControl.strColor = dr["Color"].ToString().Trim();

                            lstCalificacionControl.Add(objCalificacionControl);
                        }
                    }
                    else
                        lstCalificacionControl = null;
                }
                else
                    lstCalificacionControl = null;
            }

            return lstCalificacionControl;
        }
    }
}