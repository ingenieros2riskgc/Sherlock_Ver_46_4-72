using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsActividadBLL
    {
        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsActividad> mtdConsultarActividad(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsDtActividad cDtActividad = new clsDtActividad();
            clsActividad objActividad = new clsActividad();
            #endregion Vars

            dtInfo = cDtActividad.mtdConsultarActividad(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        int idphva = 0;
                        if (dr["idphva"].ToString() != string.Empty)
                            idphva = Convert.ToInt32(dr["idphva"].ToString());
                        objActividad = new clsActividad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            dr["CargoResponsable"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            idphva);
                        lstActividad.Add(objActividad);
                    }
                }
                else
                {
                    lstActividad = null;
                    //strErrMsg = "No hay información de Actividad.";
                }
            }
            else
                lstActividad = null;

            return lstActividad;
        }
        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsActividad> mtdConsultarActividadActivas(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsDtActividad cDtActividad = new clsDtActividad();
            clsActividad objActividad = new clsActividad();
            #endregion Vars

            dtInfo = cDtActividad.mtdConsultarActividadActivas(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objActividad = new clsActividad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            dr["CargoResponsable"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            0);
                        lstActividad.Add(objActividad);
                    }
                }
                else
                {
                    lstActividad = null;
                    //strErrMsg = "No hay información de Actividad.";
                }
            }
            else
                lstActividad = null;

            return lstActividad;
        }
        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsActividad> mtdConsultarActividad(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsDtActividad cDtActividad = new clsDtActividad();
            clsActividad objActividad = new clsActividad();
            #endregion Vars

            dtInfo = cDtActividad.mtdConsultarActividad(booEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        int idphva = 0;
                        if (dr["idphva"].ToString() != string.Empty)
                            idphva = Convert.ToInt32(dr["idphva"].ToString());
                        objActividad = new clsActividad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            dr["CargoResponsable"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            idphva);
                        lstActividad.Add(objActividad);
                    }
                }
                else
                    lstActividad = null;
            }
            else
                lstActividad = null;

            return lstActividad;
        }

        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsActividad> mtdConsultarActividad(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsDtActividad cDtActividad = new clsDtActividad();
            clsActividad objActividad = new clsActividad();
            #endregion Vars

            dtInfo = cDtActividad.mtdConsultarActividad(objCaracter, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        int idphva = 0;
                        if (dr["idphva"].ToString() != string.Empty)
                            idphva = Convert.ToInt32(dr["idphva"].ToString());
                        objActividad = new clsActividad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            dr["CargoResponsable"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            idphva);
                        lstActividad.Add(objActividad);
                    }
                }
                else
                    lstActividad = null;
            }
            else
                lstActividad = null;

            return lstActividad;
        }

        /// <summary>
        /// Realiza la insercion de los campos de la Actividad
        /// </summary>
        /// <param name="objActividad">Informacion de la Actividad</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarActividad(clsActividad objActividad, ref string strErrMsg, List<clsCaracterizacionXEntrada> lstEntrada,
            List<clsCaracterizacionXSalida> lstSalida)
        {
            bool booResult = false;
            clsDtActividad cDtActividad = new clsDtActividad();

            booResult = cDtActividad.mtdInsertarActividad(objActividad, ref strErrMsg, lstEntrada, lstSalida);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objActividad">Informacion de la Actividad</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarActividad(clsActividad objActividad, ref string strErrMsg, List<clsCaracterizacionXEntrada> lstEntrada,
            List<clsCaracterizacionXSalida> lstSalida)
        {
            bool booResult = false;
            clsDtActividad cDtActividad = new clsDtActividad();

            booResult = cDtActividad.mtdActualizarActividad(objActividad, ref strErrMsg,lstEntrada,lstSalida);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objActividad">Informacion de la Actividad</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsActividad objActividad, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtActividad cDtActividad = new clsDtActividad();

            booResult = cDtActividad.mtdActualizarEstado(objActividad, ref strErrMsg);

            return booResult;
        }
    }
}