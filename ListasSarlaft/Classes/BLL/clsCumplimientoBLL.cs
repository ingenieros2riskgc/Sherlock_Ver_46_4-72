using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsCumplimientoBLL
    {
        /// <summary>
        /// Permite insertar la informacion de la calificacion y su detalle
        /// </summary>
        /// <param name="objCalificacion">Calificacion</param>
        /// <param name="lstDetalleCal">Detalle de calificacion</param>
        /// <param name="intIdCalificacion">Id de Calificacion</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCumplimiento(List<clsDetalleCalificacion> lstDetalleCal,
            ref int intIdCalificacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCumplimiento cDtCumplimiento = new clsDtCumplimiento();

            booResult = cDtCumplimiento.mtdInsertarDetalleCalificacion(lstDetalleCal, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Consulta el cumplimiento del indicador
        /// </summary>
        /// <param name="objCalificacionIn">Parametros de Calificacion de entrada</param>
        /// <param name="objCalificacionOut">Calificacion Consultada</param>
        /// <param name="lstDetalleCal">Lista de Detalles de la calificacion consultada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarCumplimiento(clsIndicador objIndicadorIn,
            ref List<clsDetalleCalificacion> lstDetalleCal, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCumplimiento cDtCumplimiento = new clsDtCumplimiento();
            #endregion

            #region Detalle Calificacion
            booResult = cDtCumplimiento.mtdConsultarDetalleCalificacion(objIndicadorIn, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDetalleCalificacion objDetCal = new clsDetalleCalificacion(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                                Convert.ToDecimal(dr["ValorMinimo"].ToString().Trim()),
                                Convert.ToDecimal(dr["ValorMaximo"].ToString().Trim()),
                                Convert.ToInt32(dr["IdSemaforo"].ToString().Trim()),
                                dr["NombreCumplimiento"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["FechaRegistro"].ToString().Trim());

                            lstDetalleCal.Add(objDetCal);
                        }
            }
            #endregion

            return booResult;
        }

        /// <summary>
        /// Metodo para consultar el cumplimiento
        /// </summary>
        /// <param name="objDetCalificacionIn">Objeto con la informacion del Detalle de calificacion de entrada</param>
        /// <param name="objDetCalificacionOut">Objeto con la informacion del Detalle de calificacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarDetalleCumplimiento(clsDetalleCalificacion objDetCalificacionIn, ref clsDetalleCalificacion objDetCalificacionOut,
           ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCumplimiento cDtCumplimiento = new clsDtCumplimiento();
            #endregion

            booResult = cDtCumplimiento.mtdConsultarDetalleCalificacion(objDetCalificacionIn, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            objDetCalificacionOut = new clsDetalleCalificacion(
                                 Convert.ToInt32(dr["Id"].ToString().Trim()),
                                    Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                                    Convert.ToDecimal(dr["ValorMinimo"].ToString().Trim()),
                                    Convert.ToDecimal(dr["ValorMaximo"].ToString().Trim()),
                                    Convert.ToInt32(dr["IdSemaforo"].ToString().Trim()),
                                    dr["NombreCumplimiento"].ToString().Trim(),
                                    Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                    dr["FechaRegistro"].ToString().Trim());
                            break;
                        }
                    }
            }

            return booResult;
        }

        /// <summary>
        /// Metodo para actualizar el cumplimiento
        /// </summary>
        /// <param name="lstDetalleCump">Lista con la informacion del Detalle del cumplimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCumplimiento(List<clsDetalleCalificacion> lstDetalleCump, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCumplimiento cDtCumplimiento = new clsDtCumplimiento();

            booResult = cDtCumplimiento.mtdActualizarDetalleCalificacion(lstDetalleCump, ref strErrMsg);

            return booResult;
        }
    }
}