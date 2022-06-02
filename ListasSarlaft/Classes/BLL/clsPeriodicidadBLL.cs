using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsPeriodicidadBLL
    {
        /// <summary>
        /// Realiza la consulta de los Periodos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsPeriodicidad> mtdConsultarPeriodos(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsPeriodicidad> lstPeriodos = new List<clsPeriodicidad>();
            clsDtPeriodicidad cDtPeriodicidad = new clsDtPeriodicidad();
            clsPeriodicidad objPeriodo = new clsPeriodicidad();
            #endregion Vars

            dtInfo = cDtPeriodicidad.mtdConsultarPeriodos(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//
                        objPeriodo = new clsPeriodicidad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["NombrePeriodo"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstPeriodos.Add(objPeriodo);
                    }
                }
                else
                    lstPeriodos = null;
            }
            else
                lstPeriodos = null;

            return lstPeriodos;
        }

        /// <summary>
        /// Consulta el detalle del periodo
        /// </summary>
        /// <param name="objPeriodicidad">Objeto con la informacion de la periodicidad</param>
        /// <param name="lstPeriodo">Lista con la informacion del detalle del periodo</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la transaccion</returns>
        public bool mtdConsultarDetallePeriodo(clsPeriodicidad objPeriodicidad, 
            ref List<clsDetallePeriodo> lstPeriodo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDetallePeriodo objPeriodo = new clsDetallePeriodo();
            clsDtPeriodicidad cDtPeriodicidad = new clsDtPeriodicidad();
            #endregion Vars

            booResult = cDtPeriodicidad.mtdConsultarDetallePeriodo(objPeriodicidad, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {//
                            objPeriodo = new clsDetallePeriodo(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["Nombre"].ToString().Trim(),
                                Convert.ToInt32(dr["IdPeriodo"].ToString().Trim()),
                                Convert.ToInt32(dr["Posicion"].ToString().Trim()),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());
                            lstPeriodo.Add(objPeriodo);
                        }
                    }
                    else
                        lstPeriodo = null;
                }
                else
                    lstPeriodo = null;
            }

            return booResult;
        }
    }
}