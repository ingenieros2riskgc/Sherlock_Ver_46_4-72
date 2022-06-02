using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsEntradaBLL
    {
        /// <summary>
        /// Realiza la consulta de la entradas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEntrada> mtdConsultarEntrada(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsEntrada> lstEntrada = new List<clsEntrada>();
            clsDtEntrada cDtEntrada = new clsDtEntrada();
            clsEntrada objEntrada = new clsEntrada();
            #endregion Vars

            dtInfo = cDtEntrada.mtdConsultarEntrada(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEntrada = new clsEntrada(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Proveedor"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstEntrada.Add(objEntrada);
                    }
                }
                else
                {
                    lstEntrada = null;
                    //strErrMsg = "No hay información de Entrada.";
                }
            }
            else
                lstEntrada = null;

            return lstEntrada;
        }

        /// <summary>
        /// Realiza la consulta de la entradas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEntrada> mtdConsultarEntrada(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsEntrada> lstEntrada = new List<clsEntrada>();
            clsDtEntrada cDtEntrada = new clsDtEntrada();
            clsEntrada objEntrada = new clsEntrada();
            #endregion Vars

            dtInfo = cDtEntrada.mtdConsultarEntrada(objCaracter, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEntrada = new clsEntrada(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Proveedor"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstEntrada.Add(objEntrada);
                    }
                }
                else
                    lstEntrada = null;
            }
            else
                lstEntrada = null;

            return lstEntrada;
        }

        /// <summary>
        /// Realiza la consulta de la entradas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEntrada> mtdConsultarEntrada(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsEntrada> lstEntrada = new List<clsEntrada>();
            clsDtEntrada cDtEntrada = new clsDtEntrada();
            clsEntrada objEntrada = new clsEntrada();
            #endregion Vars

            dtInfo = cDtEntrada.mtdConsultarEntrada(booEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEntrada = new clsEntrada(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Proveedor"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstEntrada.Add(objEntrada);
                    }
                }
                else
                    lstEntrada = null;
            }
            else
                lstEntrada = null;

            return lstEntrada;
        }
        /// <summary>
        /// Realiza la consulta de la entradas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEntradaxActividad> mtdConsultarEntradaxActividad(int idActividad, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsEntradaxActividad> lstEntrada = new List<clsEntradaxActividad>();
            clsDtEntrada cDtEntrada = new clsDtEntrada();
            clsEntradaxActividad objEntrada = new clsEntradaxActividad();
            #endregion Vars

            dtInfo = cDtEntrada.mtdConsultarEntradaxActividad(idActividad, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEntrada = new clsEntradaxActividad(
                            Convert.ToInt32(dr["IdActividadEntrada"].ToString().Trim()),
                            Convert.ToInt32(dr["IdActividad"].ToString().Trim()),
                            Convert.ToInt32(dr["IdEntrada"].ToString().Trim()),
                            Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim())
                            );
                        lstEntrada.Add(objEntrada);
                    }
                }
                else
                {
                    lstEntrada = null;
                    strErrMsg = "Hay datos seleccionados de las Entradas";
                }
                    
            }
            else
            {
                lstEntrada = null;
                strErrMsg = "Hay datos seleccionados de las Entradas";
            }
                

            return lstEntrada;
        }
        /// <summary>
        /// Realiza la insercion de los campos de la entrada
        /// </summary>
        /// <param name="objEntrada">Informacion de la entrada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarEntrada(clsEntrada objEntrada, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtEntrada cDtEntrada = new clsDtEntrada();

            booResult = cDtEntrada.mtdInsertarEntrada(objEntrada, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objEntrada">Informacion de la entrada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEntrada(clsEntrada objEntrada, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtEntrada cDtEntrada = new clsDtEntrada();

            booResult = cDtEntrada.mtdActualizarEntrada(objEntrada, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objEntrada">Informacion de la entrada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsEntrada objEntrada, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtEntrada cDtEntrada = new clsDtEntrada();

            booResult = cDtEntrada.mtdActualizarEstado(objEntrada, ref strErrMsg);

            return booResult;
        }
    }
}