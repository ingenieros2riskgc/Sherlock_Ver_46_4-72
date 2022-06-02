using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLVariableCalificacionControl
    {
        /// <summary>
        /// Metodo para tomar el valor de la escala
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdValorEscala(ref string strErrMsg)
        {
            int ValorEscala = 0;
            clsDALVariableCalificacionControl cDALvariable = new clsDALVariableCalificacionControl();

            ValorEscala = cDALvariable.mtdValorEscala(ref strErrMsg);

            return ValorEscala;
        }
        /// <summary>
        /// Metodo para insertar la variable de calificacion del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarVariableCalificacion(clsDTOVariableCalificacionControl variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALVariableCalificacionControl cDALvariable = new clsDALVariableCalificacionControl();

            booResult = cDALvariable.mtdInsertarVariableCalificacion(variable, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Variables de Calificación Control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOVariableCalificacionControl> mtdConsultarVariablesCalificacionControl(ref List<clsDTOVariableCalificacionControl> lstVariables, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALVariableCalificacionControl cDtRegistro = new clsDALVariableCalificacionControl();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarVariablesCalificacionControl(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
                            objVariable.intIdCalificacionControl = Convert.ToInt32(dr["IdVariableCalificacionControl"].ToString().Trim());
                            objVariable.strDescripcionVariable = dr["DescripcionVariable"].ToString().Trim();
                            objVariable.booActivo = Convert.ToInt32(dr["Activo"].ToString().Trim());
                            objVariable.intIdUsuario = Convert.ToInt32(dr["UsuarioCreacion"].ToString().Trim());
                            objVariable.dtFechaRegistro = Convert.ToDateTime(dr["FechaCreacion"].ToString().Trim());
                            objVariable.FlPesoVariable = Convert.ToInt32(dr["PesoVariable"].ToString().Trim());
                            objVariable.strUsuario = dr["Usuario"].ToString().Trim();

                            lstVariables.Add(objVariable);
                        }
                    }
                    else
                        lstVariables = null;
                }
                else
                    lstVariables = null;
            }

            return lstVariables;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Variables de Calificación Control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOVariableCalificacionControl> mtdConsultarVariablesActivas(ref List<clsDTOVariableCalificacionControl> lstVariables, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALVariableCalificacionControl cDtRegistro = new clsDALVariableCalificacionControl();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarVariablesActivasControl(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOVariableCalificacionControl objVariable = new clsDTOVariableCalificacionControl();
                            objVariable.intIdCalificacionControl = Convert.ToInt32(dr["IdVariableCalificacionControl"].ToString().Trim());
                            objVariable.strDescripcionVariable = dr["DescripcionVariable"].ToString().Trim();
                            objVariable.booActivo = Convert.ToInt32(dr["Activo"].ToString().Trim());
                            objVariable.intIdUsuario = Convert.ToInt32(dr["UsuarioCreacion"].ToString().Trim());
                            objVariable.dtFechaRegistro = Convert.ToDateTime(dr["FechaCreacion"].ToString().Trim());
                            objVariable.strUsuario = dr["Usuario"].ToString().Trim();
                            objVariable.FlPesoVariable = dr["PesoVariable"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dr["PesoVariable"].ToString()) ;
                            lstVariables.Add(objVariable);
                        }
                    }
                    else
                        lstVariables = null;
                }
                else
                    lstVariables = null;
            }

            return lstVariables;
        }
        /// <summary>
        /// Metodo para actualizar el registro de variable de calificacion del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateVariableCalificacionControl(clsDTOVariableCalificacionControl variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALVariableCalificacionControl cDALvariable = new clsDALVariableCalificacionControl();

            booResult = cDALvariable.mtdUpdateVariableCalificacionControl(variable, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Variables de Calificación Control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdLoadValoresLimites(decimal LimiteInferior, decimal LimiteSuperior, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            bool flag = false;
            DataTable dtInfo = new DataTable();
            clsDALVariableCalificacionControl cDtRegistro = new clsDALVariableCalificacionControl();
            #endregion Vars

            booResult = cDtRegistro.mtdLoadValoresLimites(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            if (LimiteInferior >= Convert.ToDecimal(dr["LimiteSuperior"].ToString().Trim()) && LimiteInferior < LimiteSuperior)
                                flag = true;
                            else
                                flag = false;
                            if (LimiteSuperior <= 100 && LimiteSuperior > Convert.ToDecimal(dr["LimiteSuperior"].ToString().Trim()))
                                flag = true;
                            else
                                flag = false;
                        }
                    }
                }
            }

            return flag;
        }
        /// <summary>
        /// Metodo para tomar el valor total de registros
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdValorMaximo(ref string strErrMsg)
        {
            int Valormaximo = 0;
            clsDALVariableCalificacionControl cDALvariable = new clsDALVariableCalificacionControl();

            Valormaximo = cDALvariable.mtdValorMaximo(ref strErrMsg);

            return Valormaximo;
        }
        /// <summary>
        /// Metodo para activar o inactivar el registro de variable de calificacion del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActivarVariableCalificacionControl(clsDTOVariableCalificacionControl variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALVariableCalificacionControl cDALvariable = new clsDALVariableCalificacionControl();

            booResult = cDALvariable.mtdActivarVariableCalificacionControl(variable, ref strErrMsg);

            return booResult;
        }
        
    }
}