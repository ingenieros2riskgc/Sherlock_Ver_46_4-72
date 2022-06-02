using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsSalidaBLL
    {
        /// <summary>
        /// Realiza la consulta de la salidas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsSalida> mtdConsultarSalida(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsSalida> lstSalida = new List<clsSalida>();
            clsDtSalida cDtSalida = new clsDtSalida();
            clsSalida objSalida = new clsSalida();
            #endregion Vars

            dtInfo = cDtSalida.mtdConsultarSalida(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSalida = new clsSalida(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Cliente"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstSalida.Add(objSalida);
                    }
                }
                else
                {
                    lstSalida = null;
                    //strErrMsg = "No hay información de Salida.";
                }
            }
            else
                lstSalida = null;

            return lstSalida;
        }

        /// <summary>
        /// Realiza la consulta de la salidas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsSalida> mtdConsultarSalida(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsSalida> lstSalida = new List<clsSalida>();
            clsDtSalida cDtSalida = new clsDtSalida();
            clsSalida objSalida = new clsSalida();
            #endregion Vars

            dtInfo = cDtSalida.mtdConsultarSalida(objCaracter, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSalida = new clsSalida(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Cliente"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstSalida.Add(objSalida);
                    }
                }
                else
                    lstSalida = null;
            }
            else
                lstSalida = null;

            return lstSalida;
        }

        /// <summary>
        /// Realiza la consulta de la salidas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsSalida> mtdConsultarSalida(bool booResult, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsSalida> lstSalida = new List<clsSalida>();
            clsDtSalida cDtSalida = new clsDtSalida();
            clsSalida objSalida = new clsSalida();
            #endregion Vars

            dtInfo = cDtSalida.mtdConsultarSalida(booResult, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSalida = new clsSalida(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Cliente"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstSalida.Add(objSalida);
                    }
                }
                else
                    lstSalida = null;
            }
            else
                lstSalida = null;

            return lstSalida;
        }
        /// <summary>
        /// Realiza la consulta de la salidas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsSalidaxActividad> mtdConsultarSalidaxActividad(int idActividad, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsSalidaxActividad> lstSalida = new List<clsSalidaxActividad>();
            clsDtSalida cDtSalida = new clsDtSalida();
            clsSalidaxActividad objSalida = new clsSalidaxActividad();
            #endregion Vars

            dtInfo = cDtSalida.mtdConsultarSalidaxActividad(idActividad, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSalida = new clsSalidaxActividad(
                            Convert.ToInt32(dr["IdActividadSalida"].ToString().Trim()),
                            Convert.ToInt32(dr["IdActividad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdSalida"].ToString().Trim()),
                            Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()));
                        lstSalida.Add(objSalida);
                    }
                }
                else
                {
                    lstSalida = null;
                    strErrMsg = "Hay datos seleccionados de Salidas";
                }
                    
            }
            else
            {
                lstSalida = null;
                strErrMsg = "Hay datos seleccionados de Salidas";
            }
                

            return lstSalida;
        }
        /// <summary>
        /// Realiza la insercion de los campos de la salida
        /// </summary>
        /// <param name="objSalida">Informacion de la salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarSalida(clsSalida objSalida, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtSalida cDtSalida = new clsDtSalida();

            booResult = cDtSalida.mtdInsertarSalida(objSalida, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objEntrada">Informacion de la salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarSalida(clsSalida objSalida, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtSalida cDtSalida = new clsDtSalida();

            booResult = cDtSalida.mtdActualizarSalida(objSalida, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objEntrada">Informacion de la salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsSalida objSalida, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtSalida cDtSalida = new clsDtSalida();

            booResult = cDtSalida.mtdActualizarEstado(objSalida, ref strErrMsg);

            return booResult;
        }
    }
}