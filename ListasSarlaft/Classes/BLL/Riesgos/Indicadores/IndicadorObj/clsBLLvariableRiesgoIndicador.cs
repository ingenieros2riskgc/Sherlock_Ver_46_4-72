using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLvariableRiesgoIndicador
    {
        /// <summary>
        /// Metodo para insertar la variable del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarVariableRiesgoIndicador(clsDTOvariableRiesgoIndicador variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALvariableRiesgoIndicador cDALvariable = new clsDALvariableRiesgoIndicador();

            booResult = cDALvariable.mtdInsertarVariableRiesgoIndicador(variable, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Realiza la consulta de las variables del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOvariableRiesgoIndicador> mtdConsultarVariableRiesgoIndicador(bool booEstado, ref string strErrMsg, int IdRiesgoIndicador)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOvariableRiesgoIndicador> lstVariables = new List<clsDTOvariableRiesgoIndicador>();
            clsDALvariableRiesgoIndicador cDtvariables = new clsDALvariableRiesgoIndicador();
            clsDTOvariableRiesgoIndicador objVariable = new clsDTOvariableRiesgoIndicador();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtvariables.mtdConsultarVariableRiesgoIndicador(ref dtInfo, ref strErrMsg, IdRiesgoIndicador);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objVariable = new clsDTOvariableRiesgoIndicador();
                        objVariable.intIdVariableRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicadorVariable"].ToString().Trim());
                        objVariable.strDescripcion = dr["Descripcion"].ToString().Trim();
                        objVariable.dblValorVariable = Convert.ToDouble(dr["ValorVariable"].ToString().Trim());
                        objVariable.intIdFormato = Convert.ToInt32(dr["IdFormato"].ToString().Trim());
                        objVariable.strFormato = dr["NombreDetalle"].ToString().Trim();
                        lstVariables.Add(objVariable);
                    }
                }
                else
                {
                    lstVariables = null;
                    strErrMsg = "No hay registros de las variables.";
                }
            }
            else
                objVariable = null;

            return lstVariables;
        }
        /// <summary>
        /// Metodo para actualizar la variable del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarVariableRiesgoIndicador(clsDTOvariableRiesgoIndicador variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALvariableRiesgoIndicador cDALvariable = new clsDALvariableRiesgoIndicador();

            booResult = cDALvariable.mtdActualizarVariableRiesgoIndicador(variable, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para eliminar la variable del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdEliminarVariableRiesgoIndicador(string IdVariable, ref string strErrMsg, string IdRiesgoIndicador)
        {
            bool booResult = false;
            clsDALvariableRiesgoIndicador cDALvariable = new clsDALvariableRiesgoIndicador();

            booResult = cDALvariable.mtdEliminaVariable(IdVariable, ref strErrMsg, IdRiesgoIndicador);

            return booResult;
        }
        /// <summary>
        /// Metodo para tomar el valor de la variable por el nombre
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public double mtdGetValueVariable(ref string strErrMsg, string NombreVariable, int IdRiesgoIndicador, string ValorFrecuencia, int IdDetalleFrecuencia,
            string año, string mes)
        {
            double valorVariable = 0;
            clsDALvariableRiesgoIndicador cDALindicador = new clsDALvariableRiesgoIndicador();

            valorVariable = cDALindicador.mtdConsultarValorVariable(ref strErrMsg, NombreVariable, IdRiesgoIndicador, ValorFrecuencia, IdDetalleFrecuencia,  año,  mes);

            return valorVariable;
        }
        /// <summary>
        /// Metodo para insertar el valor variable del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarValorVariable(clsDTOvalorVariable variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALvariableRiesgoIndicador cDALvariable = new clsDALvariableRiesgoIndicador();

            booResult = cDALvariable.mtdInsertarValorVariable(variable, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Realiza la consulta de las variables del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOvalorVariable> mtdConsultarValorVariable(bool booEstado, ref string strErrMsg, int IdRiesgoIndicador, int IdVariable)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOvalorVariable> lstVariables = new List<clsDTOvalorVariable>();
            clsDALvariableRiesgoIndicador cDtvariables = new clsDALvariableRiesgoIndicador();
            clsDTOvalorVariable objVariable = new clsDTOvalorVariable();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtvariables.mtdConsultarValorVariable(ref dtInfo, ref strErrMsg, IdRiesgoIndicador,  IdVariable);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objVariable = new clsDTOvalorVariable();
                        objVariable.intIdValorVariable = Convert.ToInt32(dr["IdValorVariable"].ToString().Trim());
                        objVariable.intIdVariable = Convert.ToInt32(dr["IdVariable"].ToString().Trim());
                        objVariable.intIdRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim());
                        objVariable.intIdFrecuencia = Convert.ToInt32(dr["IdFrecuencia"].ToString().Trim());
                        objVariable.strFrecuenciaMedicion = dr["FrecuenciaMedicion"].ToString().Trim();
                        objVariable.strValorFrecuencia = dr["valorFrecuencia"].ToString().Trim();
                        objVariable.dblValorVariable = Convert.ToDouble(dr["valorVariable"].ToString().Trim());
                        objVariable.intIdDetalleFrecuencia = Convert.ToInt32(dr["IdDetalleFrecuencia"].ToString().Trim());
                        objVariable.strDescripcionDetalle = dr["Descripcion"].ToString().Trim();
                        objVariable.strAño = dr["Año"].ToString().Trim();
                        objVariable.strMes = dr["Mes"].ToString().Trim();
                        lstVariables.Add(objVariable);
                    }
                }
                else
                {
                    lstVariables = null;
                    strErrMsg = "No hay registros de las variables.";
                }
            }
            else
                objVariable = null;

            return lstVariables;
        }
        /// <summary>
        /// Metodo para insertar el valor variable del indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarValorVariable(clsDTOvalorVariable variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALvariableRiesgoIndicador cDALvariable = new clsDALvariableRiesgoIndicador();

            booResult = cDALvariable.mtdActualizarValorVariable(variable, ref strErrMsg);

            return booResult;
        }
    }
}