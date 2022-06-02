using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsCaracterizacionBLL
    {
        /// <summary>
        /// Metodo para consultar la caracterizacion
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="objCaracOut">Objecto con la informacion de caracterizacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarCaracterizacion(clsCaracterizacion objCaracIn, ref clsCaracterizacion objCaracOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtCaracterizacion cCarat = new clsDtCaracterizacion();

            booResult = cCarat.mtdConsultarCaracterizacion(objCaracIn, ref dtCaracOut, ref strErrMsg);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objCaracOut = new clsCaracterizacion(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim()),
                                Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["FechaRegistro"].ToString().Trim(),
                                dr["Recursos"].ToString().Trim(),
                                dr["Numerales"].ToString().Trim(),
                                dr["Responsables"].ToString().Trim(),
                                dr["Codigo"].ToString().Trim()
                                );
                        }
                    }
                    else
                        objCaracOut = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    objCaracOut = null;
            }

            return booResult;
        }
            
        /// <summary>
        /// Metodo para insertar una caracterizacion actualmente asignada
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="lstEntrada">Lista con la informacion de entradas</param>
        /// <param name="lstActividad">Lista con la informacion de actividades</param>
        /// <param name="lstSalida">Lista con la informacion de salidas</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCaracterizacion(clsCaracterizacion objCaracIn,
            List<clsCaracterizacionXEntrada> lstEntrada, List<clsCaracterizacionXActividad> lstActividad, 
            List<clsCaracterizacionXSalida> lstSalida, 
            ref string strErrMsg)
        {
            #region
            bool booResult = false;
            clsCaracterizacion objCaracOut = new clsCaracterizacion();
            clsDtCaracterizacion cCarat = new clsDtCaracterizacion();
            #endregion

            booResult = cCarat.mtdInsertarCaracterizacion(objCaracIn, ref objCaracOut, ref strErrMsg);

            if (booResult)
                booResult = cCarat.mtdInsertarCaracterEntrada(lstEntrada, objCaracOut, ref strErrMsg);

            if (booResult)
                booResult = cCarat.mtdInsertarCaracterActividad(lstActividad, objCaracOut, ref strErrMsg);

            if (booResult)
                booResult = cCarat.mtdInsertarCaracterSalida(lstSalida, objCaracOut, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para actualizar una caracterizacion actualmente asignada
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="lstEntrada">Lista con la informacion de entradas</param>
        /// <param name="lstActividad">Lista con la informacion de actividades</param>
        /// <param name="lstSalida">Lista con la informacion de salidas</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCaracterizacion(clsCaracterizacion objCaracIn, 
            List<clsCaracterizacionXEntrada> lstEntrada, List<clsCaracterizacionXActividad> lstActividad, 
            List<clsCaracterizacionXSalida> lstSalida,
            ref string strErrMsg)
        {
            #region
            bool booResult = false;
            clsDtCaracterizacion cCarat = new clsDtCaracterizacion();
            #endregion

            booResult = cCarat.mtdActualizarCaracterizacion(objCaracIn, ref strErrMsg);

            if (booResult)
                booResult = cCarat.mtdActualizarCaracterEntrada(lstEntrada, objCaracIn, ref strErrMsg);

            if (booResult)
                booResult = cCarat.mtdActualizarCaracterActividad(lstActividad, objCaracIn, ref strErrMsg);

            if (booResult)
                booResult = cCarat.mtdActualizarCaracterSalida(lstSalida, objCaracIn, ref strErrMsg);

            return booResult;
        }
    }
}