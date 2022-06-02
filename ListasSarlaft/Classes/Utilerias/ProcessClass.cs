using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.Utilerias
{
    public class ProcessClass
    {
        public double mtdProcesFormulaRiesgoIndicador(List<clsDTOdetalleFormulaRiesgoIndicador> lstDetalle, ref string strErrMsg, 
            int IdRiesgoIndicador, string ValorFrecuencia,
            int IdDetalleFrecuencia, string año, string mes, int booPorcentaje)
        {
            double formulaResultNominador = 0;
            double formulaResultDenominador = 0;
            double total = 0;
            clsBLLvariableRiesgoIndicador cVariable = new clsBLLvariableRiesgoIndicador();
            string strOperacionNominador = string.Empty;
            string strOperacionDenominador = string.Empty;
            #region Formula
            foreach (clsDTOdetalleFormulaRiesgoIndicador objDetalle in lstDetalle)
            {
                double valor = cVariable.mtdGetValueVariable(ref strErrMsg, objDetalle.strVariable, IdRiesgoIndicador, ValorFrecuencia,  IdDetalleFrecuencia,  año,  mes);

                if (objDetalle.strTipo == "Nominador")
                {
                    #region Recorrido ejecucion de formula
                    switch (objDetalle.intIdOperando.ToString())
                    {
                        case "1"://Variable
                                 /*string strFiltro = string.Format("[Id]={0}", drForm["Valor"].ToString()),
                                     strNombreVar = string.Empty;
                                 DataRow[] drVar = dtInfoVariable.Select(strFiltro);

                                 strNombreVar = drVar[0][1].ToString();*/
                            if (string.IsNullOrEmpty(strOperacionNominador))
                                if (string.IsNullOrEmpty(objDetalle.strVariable))
                                {
                                    formulaResultNominador = 0;
                                }
                                else
                                {
                                    formulaResultNominador = valor;
                                }
                            else
                            {
                                #region Operacion
                                switch (strOperacionNominador)
                                {

                                    case "+":
                                        if (string.IsNullOrEmpty(objDetalle.strVariable))
                                        {
                                            formulaResultNominador = 0;
                                            break;
                                        }
                                        else
                                        {

                                            formulaResultNominador = formulaResultNominador +
                                                valor;
                                            break;
                                        }
                                    case "-":
                                        if (string.IsNullOrEmpty(objDetalle.strVariable))
                                        {
                                            formulaResultNominador = 0;
                                            break;
                                        }
                                        else
                                        {
                                            if (formulaResultNominador != 0)
                                            {
                                                formulaResultNominador = formulaResultNominador -
                                                        valor;
                                                break;
                                            }
                                            else {
                                                formulaResultNominador = valor;

                                            }
                                            break;
                                        }
                                    case "X":
                                        if (string.IsNullOrEmpty(objDetalle.strVariable))
                                        {
                                            formulaResultNominador = 0;
                                            break;
                                        }
                                        else
                                        {
                                            formulaResultNominador = formulaResultNominador *
                                                    valor;
                                            break;
                                        }
                                    case "/":
                                        if (valor != 0)
                                            if (string.IsNullOrEmpty(objDetalle.strVariable))
                                            {
                                                formulaResultNominador = 0;
                                            }
                                            else
                                            {
                                                formulaResultNominador = formulaResultNominador /
                                                        valor;
                                            }
                                        else
                                        {
                                            strErrMsg = string.Format("Por favor revisar el valor de la variable [{0}]. [Division por 0] ", objDetalle.strVariable);
                                        }
                                        break;
                                }
                                #endregion Operacion
                            }
                            break;
                        case "2"://Operacion
                            strOperacionNominador = objDetalle.strVariable;
                            break;
                    }
                    #endregion Recorrido ejecucion de formula
                }else
                {
                    #region Recorrido ejecucion de formula
                    switch (objDetalle.intIdOperando.ToString())
                    {
                        case "1"://Variable
                                 /*string strFiltro = string.Format("[Id]={0}", drForm["Valor"].ToString()),
                                     strNombreVar = string.Empty;
                                 DataRow[] drVar = dtInfoVariable.Select(strFiltro);

                                 strNombreVar = drVar[0][1].ToString();*/
                            if (string.IsNullOrEmpty(strOperacionDenominador))
                                if (string.IsNullOrEmpty(objDetalle.strVariable))
                                {
                                    formulaResultDenominador = 0;
                                }
                                else
                                {
                                    formulaResultDenominador = valor;
                                }
                            else
                            {
                                #region Operacion
                                switch (strOperacionDenominador)
                                {

                                    case "+":
                                        if (string.IsNullOrEmpty(objDetalle.strVariable))
                                        {
                                            formulaResultDenominador = 0;
                                            break;
                                        }
                                        else
                                        {

                                            formulaResultDenominador = formulaResultDenominador +
                                                valor;
                                            break;
                                        }
                                    case "-":
                                        if (string.IsNullOrEmpty(objDetalle.strVariable))
                                        {
                                            formulaResultDenominador = 0;
                                            break;
                                        }
                                        else
                                        {
                                            formulaResultDenominador = formulaResultDenominador -
                                                    valor;
                                            break;
                                        }
                                    case "X":
                                        if (string.IsNullOrEmpty(objDetalle.strVariable))
                                        {
                                            formulaResultDenominador = 0;
                                            break;
                                        }
                                        else
                                        {
                                            formulaResultDenominador = formulaResultDenominador *
                                                    valor;
                                            break;
                                        }
                                    case "/":
                                        if (valor != 0)
                                            if (string.IsNullOrEmpty(objDetalle.strVariable))
                                            {
                                                formulaResultDenominador = 0;
                                            }
                                            else
                                            {
                                                formulaResultDenominador = formulaResultDenominador /
                                                        valor;
                                            }
                                        else
                                        {
                                            strErrMsg = string.Format("Por favor revisar el valor de la variable [{0}]. [Division por 0] ", objDetalle.strVariable);
                                        }
                                        break;
                                }
                                #endregion Operacion
                            }
                            break;
                        case "2"://Operacion
                            strOperacionDenominador = objDetalle.strVariable;
                            break;
                    }
                    #endregion Recorrido ejecucion de formula
                }
            }
            #endregion Formula
            if(formulaResultDenominador != 0)
                total = formulaResultNominador / formulaResultDenominador;
            if (booPorcentaje == 1)
                total = total * 100;
            return total;
        }
        public bool mtdValidateMeta(ref double resultFormula, string ValorFrecuencia,
            int IdDetalleFrecuencia, string año, string mes, List<clsDTOdetalleFormulaRiesgoIndicador> lstDetalle, 
            int IdRiesgoIndicador, int booPorcentaje
            )
        {
            bool booResultValidate = false;
            //double result = 0;
            string strErrMsg = string.Empty;
            
                resultFormula = mtdProcesFormulaRiesgoIndicador(lstDetalle, ref strErrMsg, IdRiesgoIndicador, 
                    ValorFrecuencia, IdDetalleFrecuencia, año, mes, booPorcentaje);
                /*if(resultFormula >= objMetas.dblMeta)
                {
                    booResultValidate = true;
                }*/
            
            return booResultValidate;
        }
        public string mtdValidaSeguimiento(double resultFormula, List<clsDTOseguimientoRiesgoIndicador> lstSeguimiento, ref string color)
        {
            string seguimiento = string.Empty;
            foreach (clsDTOseguimientoRiesgoIndicador objSeguimiento in lstSeguimiento)
            {
                if(resultFormula > objSeguimiento.dblValorMinimo && resultFormula <= objSeguimiento.dblValorMaximo)
                {
                    color = objSeguimiento.strColor;
                    seguimiento = objSeguimiento.strDescripcionSeguimiento;
                }
            }
            return seguimiento;
        }
    }
}