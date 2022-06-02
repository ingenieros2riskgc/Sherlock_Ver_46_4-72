using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLformulaRiesgoIndicador
    {
        /// <summary>
        /// Metodo para insertar la formulda del indicador del riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarFormulaRiesgoIndicador(clsDTOformulaRiesgoIndicador formula, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALformulaRiesgoIndicador cDALformula = new clsDALformulaRiesgoIndicador();

            booResult = cDALformula.mtdInsertarFormulaRiesgoIndicador(formula, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Realiza la consulta los registros de la formula de los Indicadores
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public clsDTOformulaRiesgoIndicador mtdConsultarFormulaRiesgosIndicadores(bool booEstado, ref string strErrMsg, int IdRiesgoIndicador)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDALformulaRiesgoIndicador cDtFormulaRiesgoInd = new clsDALformulaRiesgoIndicador();
            clsDTOformulaRiesgoIndicador objFormulaRiesgoIndicador = new clsDTOformulaRiesgoIndicador();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtFormulaRiesgoInd.mtdConsultarFormulaRiesgosIndicadores(ref dtInfo, ref strErrMsg, IdRiesgoIndicador);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objFormulaRiesgoIndicador = new clsDTOformulaRiesgoIndicador();
                        objFormulaRiesgoIndicador.intIdFormula = Convert.ToInt32(dr["IdFormula"].ToString().Trim());
                        objFormulaRiesgoIndicador.intIdRiesgoIndicador = Convert.ToInt32(dr["IdRiesgoIndicador"].ToString().Trim());
                        objFormulaRiesgoIndicador.strNominador = dr["Nominador"].ToString().Trim();
                        objFormulaRiesgoIndicador.strDenominador = dr["Denominador"].ToString().Trim();
                        if(Convert.ToBoolean(dr["booPorcentaje"].ToString()) == true)
                            objFormulaRiesgoIndicador.intPorcentaje = 1;
                        else
                            objFormulaRiesgoIndicador.intPorcentaje = 0;
                    }
                }
                else
                {
                    objFormulaRiesgoIndicador = null;
                    strErrMsg = "No hay registros de la formula del indicador.";
                }
            }
            else
                objFormulaRiesgoIndicador = null;

            return objFormulaRiesgoIndicador;
        }
        /// <summary>
        /// Metodo para actualizar la formulda del indicador del riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarFormulaRiesgoIndicador(clsDTOformulaRiesgoIndicador formula, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALformulaRiesgoIndicador cDALformula = new clsDALformulaRiesgoIndicador();

            booResult = cDALformula.mtdActualizarFormulaRiesgoIndicador(formula, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para tomar el valor del ultimo id registrado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdGetLastId(ref string strErrMsg)
        {
            int LastId = 0;
            clsDALformulaRiesgoIndicador cDALformula = new clsDALformulaRiesgoIndicador();

            LastId = cDALformula.mtdGetLastId(ref strErrMsg);

            return LastId;
        }
        /// <summary>
        /// Metodo para ingresar el detalle la formulda del indicador del riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarDetalleFormulaRiesgoIndicador(int IdFormula, string Variable, string Tipo, int Secuencia, int IdOperando, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALformulaRiesgoIndicador cDALformula = new clsDALformulaRiesgoIndicador();

            booResult = cDALformula.mtdInsertarDetalleFormulaRiesgoIndicador(IdFormula,Variable,Tipo,Secuencia,IdOperando, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Realiza la consulta los registros del detalle de la formula
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOdetalleFormulaRiesgoIndicador> mtdConsultarDetalleFormula(bool booEstado, ref string strErrMsg, int IdFormula)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDALformulaRiesgoIndicador cDtFormulaRiesgoInd = new clsDALformulaRiesgoIndicador();
            clsDTOdetalleFormulaRiesgoIndicador objDetalleFormula = new clsDTOdetalleFormulaRiesgoIndicador();
            List<clsDTOdetalleFormulaRiesgoIndicador> lstDetalle = new List<clsDTOdetalleFormulaRiesgoIndicador>();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtFormulaRiesgoInd.mtdConsultarDetalleFormula(ref dtInfo, ref strErrMsg, IdFormula);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objDetalleFormula = new clsDTOdetalleFormulaRiesgoIndicador();
                        objDetalleFormula.strVariable = dr["Variable"].ToString().Trim();
                        objDetalleFormula.strTipo = dr["Tipo"].ToString().Trim();
                        objDetalleFormula.intSecuencia = Convert.ToInt32(dr["Secuencia"].ToString().Trim());
                        objDetalleFormula.intIdOperando = Convert.ToInt32(dr["IdOperando"].ToString().Trim());
                        lstDetalle.Add(objDetalleFormula);
                    }
                }
                else
                {
                    objDetalleFormula = null;
                    strErrMsg = "No hay registros del detalle de la formula.";
                }
            }
            else
                objDetalleFormula = null;

            return lstDetalle;
        }
        /// <summary>
        /// Metodo para ingresar el detalle la formulda del indicador del riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarDetalleFormulaRiesgoIndicador(int IdFormula, string Variable, string Tipo, int Secuencia, int IdOperando, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALformulaRiesgoIndicador cDALformula = new clsDALformulaRiesgoIndicador();

            booResult = cDALformula.mtdActualizarDetalleFormulaRiesgoIndicador(IdFormula, Variable, Tipo, Secuencia, IdOperando, ref strErrMsg);

            return booResult;
        }
    }
}