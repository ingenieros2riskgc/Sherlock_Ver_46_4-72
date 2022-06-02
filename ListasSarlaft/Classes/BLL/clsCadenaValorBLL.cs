using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsCadenaValorBLL
    {
        /// <summary>
        /// Realiza la consulta de la cadena de valor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsCadenaValor> mtdConsultarCadenaValor(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsDtCadenaValor cDtCadenaValor = new clsDtCadenaValor();
            clsCadenaValor objCadenaValor = new clsCadenaValor();
            #endregion Vars

            dtInfo = cDtCadenaValor.mtdConsultarCadenaValor(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objCadenaValor = new clsCadenaValor(
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            dr["NombreCadenaValor"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["Usuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstCadenaValor.Add(objCadenaValor);
                    }
                }
                else
                {
                    lstCadenaValor = null;
                    //strErrMsg = "No hay información de cadenas de valor.";
                }
            }
            else
                lstCadenaValor = null;

            return lstCadenaValor;
        }

        /// <summary>
        /// Realiza la consulta de la cadena de valor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsCadenaValor> mtdConsultarCadenaValor(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsDtCadenaValor cDtCadenaValor = new clsDtCadenaValor();
            clsCadenaValor objCadenaValor = new clsCadenaValor();
            #endregion Vars

            dtInfo = cDtCadenaValor.mtdConsultarCadenaValor(booEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objCadenaValor = new clsCadenaValor(
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            dr["NombreCadenaValor"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["Usuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstCadenaValor.Add(objCadenaValor);
                    }
                }
                else
                {
                    lstCadenaValor = null;
                    strErrMsg = "No hay información de cadenas de valor.";
                }
            }
            else
                lstCadenaValor = null;

            return lstCadenaValor;
        }

        /// <summary>
        /// Metodo para consultar la cadena de valor
        /// </summary>
        /// <param name="booEstado">Estado a consultar</param>
        /// <param name="lstCadenaValor">Lista con la informacion de la cadena de valor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarCadenaValor(bool booEstado, ref List<clsCadenaValor> lstCadenaValor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCadenaValor cDtCadenaValor = new clsDtCadenaValor();
            clsCadenaValor objCadenaValor = new clsCadenaValor();
            #endregion Vars

            booResult = cDtCadenaValor.mtdConsultarCadenaValor(booEstado, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            objCadenaValor = new clsCadenaValor(
                                Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                                dr["NombreCadenaValor"].ToString().Trim(),
                                dr["Estado"].ToString().Trim() == "True" ? true : false,
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["Usuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());
                            lstCadenaValor.Add(objCadenaValor);
                        }
                    }
                    else
                    {
                        lstCadenaValor = null;
                        strErrMsg = "No hay información de cadenas de valor.";
                    }
                }
                else
                    lstCadenaValor = null;
            }
            else
            {
                strErrMsg = "No hay información de cadenas de valor.";
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la insercion de los campos de la cadena de valor
        /// </summary>
        /// <param name="objCadenaValor">Informacion de la cadena de valor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCadenaValor(clsCadenaValor objCadenaValor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCadenaValor cDtCadenaValor = new clsDtCadenaValor();

            booResult = cDtCadenaValor.mtdInsertarCadenaValor(objCadenaValor, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objCadenaValor">Informacion de la cadena de valor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCadenaValor(clsCadenaValor objCadenaValor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCadenaValor cDtCadenaValor = new clsDtCadenaValor();

            booResult = cDtCadenaValor.mtdActualizarCadenaValor(objCadenaValor, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objCadenaValor">Informacion de Cadena de Valor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsCadenaValor objCadenaValor, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCadenaValor cDtCadenaValor = new clsDtCadenaValor();

            booResult = cDtCadenaValor.mtdActualizarEstado(objCadenaValor, ref strErrMsg);

            return booResult;
        }
    }
}