using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsVariableBLL
    {
        /// <summary>
        /// Realiza la consulta de la Variable
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVariable> mtdConsultarVariable(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtVariable cDtVariable = new clsDtVariable();
            clsVariable objVariable = new clsVariable();
            List<clsVariable> lstVariable = new List<clsVariable>();
            #endregion Vars

            dtInfo = cDtVariable.mtdConsultarVariable(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objVariable = new clsVariable(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Formato"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());

                        lstVariable.Add(objVariable);
                    }
                }
                else
                    lstVariable = null;
            }
            else
                lstVariable = null;

            return lstVariable;
        }

        /// <summary>
        /// Realiza la consulta de la Variable
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVariable> mtdConsultarVariable(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtVariable cDtVariable = new clsDtVariable();
            clsVariable objVariable = new clsVariable();
            List<clsVariable> lstVariable = new List<clsVariable>();
            #endregion Vars

            dtInfo = cDtVariable.mtdConsultarVariable(booEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objVariable = new clsVariable(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Formato"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());

                        lstVariable.Add(objVariable);
                    }
                }
                else
                    lstVariable = null;
            }
            else
                lstVariable = null;

            return lstVariable;
        }

        /// <summary>
        /// Metodo para consultar la informacion de variables
        /// </summary>
        /// <param name="booEstado">Estado de la variable</param>
        /// <param name="objIndicador">Objeto con la informacion del indicador</param>
        /// <param name="lstVariable">Lista con la informacion de variables</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarVariable(bool booEstado, clsIndicador objIndicador,
            ref List<clsVariable> lstVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtVariable cDtVariable = new clsDtVariable();
            clsVariable objVariable = new clsVariable();
            #endregion Vars

            booResult = cDtVariable.mtdConsultarVariable(booEstado, objIndicador, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                           
                            objVariable = new clsVariable();
                            objVariable.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objVariable.strDescripcion = dr["Descripcion"].ToString().Trim();
                            objVariable.strFormato = dr["Formato"].ToString().Trim();
                            objVariable.booEstado = dr["Estado"].ToString().Trim() == "True" ? true : false;
                            objVariable.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objVariable.strNombreUsuario = dr["NombreUsuario"].ToString().Trim();
                            objVariable.intIdDetalleVariable = Convert.ToInt32(dr["IdDetalleVariable"].ToString().Trim());
                            objVariable.strPeriodoAnual = dr["PeriodoAnual"].ToString().Trim();
                            objVariable.decValor = Convert.ToDecimal(dr["Valor"].ToString().Trim());
                            if(dr["IdDetallePeriodo"].ToString() == ""){
                                objVariable.intIdDetallePeriodo = 0;
                            }else{
                                objVariable.intIdDetallePeriodo = Convert.ToInt32(dr["IdDetallePeriodo"].ToString().Trim());
                            }
                            if (dr["Periodo"].ToString() == "")
                            {
                                objVariable.strNombreDetPeriodo = "";
                            }
                            else {
                                objVariable.strNombreDetPeriodo = dr["Periodo"].ToString().Trim();
                            }
                            
                            objVariable.intIdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                            DateTime time = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objVariable.dtFechaRegistro = ""+time.Year;

                            lstVariable.Add(objVariable);
                        }
                    }
                    else
                        lstVariable = null;
                }
                else
                    lstVariable = null;
            }

            return booResult;
        }
        public bool mtdConsultarVariableByYear(bool booEstado, clsIndicador objIndicador,
            ref List<clsVariable> lstVariable, ref string strErrMsg, int year)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtVariable cDtVariable = new clsDtVariable();
            clsVariable objVariable = new clsVariable();
            #endregion Vars

            booResult = cDtVariable.mtdConsultarVariableByYear(booEstado, objIndicador, ref dtInfo, ref strErrMsg, year);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {

                            objVariable = new clsVariable();
                            objVariable.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objVariable.strDescripcion = dr["Descripcion"].ToString().Trim();
                            objVariable.strFormato = dr["Formato"].ToString().Trim();
                            objVariable.booEstado = dr["Estado"].ToString().Trim() == "True" ? true : false;
                            objVariable.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objVariable.strNombreUsuario = dr["NombreUsuario"].ToString().Trim();
                            objVariable.intIdDetalleVariable = Convert.ToInt32(dr["IdDetalleVariable"].ToString().Trim());
                            objVariable.strPeriodoAnual = dr["PeriodoAnual"].ToString().Trim();
                            objVariable.decValor = Convert.ToDecimal(dr["Valor"].ToString().Trim());
                            if (dr["IdDetallePeriodo"].ToString() == "")
                            {
                                objVariable.intIdDetallePeriodo = 0;
                            }
                            else
                            {
                                objVariable.intIdDetallePeriodo = Convert.ToInt32(dr["IdDetallePeriodo"].ToString().Trim());
                            }
                            if (dr["Periodo"].ToString() == "")
                            {
                                objVariable.strNombreDetPeriodo = "";
                            }
                            else
                            {
                                objVariable.strNombreDetPeriodo = dr["Periodo"].ToString().Trim();
                            }

                            objVariable.intIdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                            DateTime time = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objVariable.dtFechaRegistro = "" + time.Year;

                            lstVariable.Add(objVariable);
                        }
                    }
                    else
                        lstVariable = null;
                }
                else
                    lstVariable = null;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la consulta de la Variable
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarVariable(clsVariable objVarIn, ref clsVariable objVarOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtVariable cDtVariable = new clsDtVariable();
            List<clsVariable> lstVariable = new List<clsVariable>();
            #endregion Vars

            booResult = cDtVariable.mtdConsultarVariable(objVarIn, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            objVarOut = new clsVariable(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["Descripcion"].ToString().Trim(),
                                dr["Formato"].ToString().Trim(),
                                dr["Estado"].ToString().Trim() == "True" ? true : false,
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());
                        }
            }

            return booResult;
        }

        /// <summary>
        /// Permite la Insertar del Variable
        /// </summary>
        /// <param name="objVariable"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public bool mtdInsertarVariable(clsVariable objVariable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtVariable cDtVariable = new clsDtVariable();

            booResult = cDtVariable.mtdInsertarVariable(objVariable, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite la Actualizar del Variable
        /// </summary>
        /// <param name="objVariable"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public bool mtdActualizarVariable(clsVariable objVariable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtVariable cDtVariable = new clsDtVariable();

            booResult = cDtVariable.mtdActualizarVariable(objVariable, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objIndicador">Informacion de Indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsVariable objVariable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtVariable cDtVariable = new clsDtVariable();

            booResult = cDtVariable.mtdActualizarEstado(objVariable, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para insertar el detalle de la variable
        /// </summary>
        /// <param name="objDetalle">Objecto con la informacion del detalle de la variable</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarDetalle(clsDetalleVariable objDetalle, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtVariable cDtVariable = new clsDtVariable();

            booResult = cDtVariable.mtdInsertarDetalle(objDetalle, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el detalle de la variable
        /// </summary>
        /// <param name="objDetalle">Objecto con la informacion del detalle de la variable</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarDetalle(clsDetalleVariable objDetalle, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtVariable cDtVariable = new clsDtVariable();

            booResult = cDtVariable.mtdActualizarDetalle(objDetalle, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para validar los valores de las variables
        /// </summary>
        /// <param name="objDetVar">Objecto con la informacion del detalle de la variable</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdValidarValoresVars(clsDetalleVariable objDetVar, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            int intNroCalifs = 0, intNroVars = 0;
            DataTable dtInfo = new DataTable();
            clsDtVariable cDtVariable = new clsDtVariable();
            #endregion Vars

            booResult = cDtVariable.mtdConsultarNroVariables(objDetVar, ref intNroVars, ref strErrMsg);
            if (booResult)
            {
                booResult = cDtVariable.mtdConsultarNroCalificaciones(objDetVar, ref intNroCalifs, ref strErrMsg);
                if (booResult)
                {
                    if ((intNroVars == intNroCalifs) && (intNroVars != 0))
                        booResult = true;
                    else
                    {
                        booResult = false;
                        strErrMsg = "No hay información registrada para efectuar el seguimiento";
                    }
                }
            }

            return booResult;
        }
    }
}