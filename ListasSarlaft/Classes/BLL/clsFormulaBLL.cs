using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsFormulaBLL
    {
        /// <summary>
        /// Realiza la insercion de los campos de Formula
        /// </summary>
        /// <param name="lstFormula">Informacion de Formulas</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarFormula(List<object> lstFormula, int intIdIndicador, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtFormula cDtFormula = new clsDtFormula();

            booResult = cDtFormula.mtdInsertarFormula(lstFormula, intIdIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Consultar la formula
        /// </summary>
        /// <param name="objIndicadorIn">Indicador </param>
        /// <param name="lstFormula">Informacion de Formulas</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la transaccion</returns>
        public bool mtdConsultarFormula(clsIndicador objIndicadorIn, ref List<object> lstFormula, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsFormula objFormula = new clsFormula();
            clsDtFormula cDtFormula = new clsDtFormula();

            booResult = cDtFormula.mtdConsultarFormula(objIndicadorIn, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            objFormula = new clsFormula(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdOperando"].ToString().Trim()),
                                Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                                dr["Valor"].ToString().Trim(),
                                Convert.ToInt32(dr["Posicion"].ToString().Trim()),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["FechaRegistro"].ToString().Trim());

                            lstFormula.Add(objFormula);
                        }
            }

            return booResult;
        }

        /// <summary>
        /// Actualizar la formula
        /// </summary>
        /// <param name="lstFormula">Informacion de Formulas</param>
        /// <param name="intIdIndicador">objeto con la informacion del indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la transaccion</returns>
        public bool mtdActualizarFormula(List<object> lstFormula, int intIdIndicador, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtFormula cDtFormula = new clsDtFormula();

            booResult = cDtFormula.mtdActualizarFormula(lstFormula, intIdIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Crear la formula
        /// </summary>
        /// <param name="dtInfo">objeto con la informacion de la formula</param>
        /// <param name="lstFormula">Informacion de Formulas</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Estado de la transaccion</returns>
        public bool mtdCrearListaFormula(DataTable dtInfo, ref List<clsFormula> lstFormula, ref string strErrMsg)
        {
            bool booResult = false;

            if (dtInfo != null)
                if (dtInfo.Rows.Count > 0)
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsFormula objFormula = new clsFormula(
                             Convert.ToInt32(dr["Id"].ToString().Trim()),
                             Convert.ToInt32(dr["IdOperando"].ToString().Trim()),
                             Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                             dr["Valor"].ToString().Trim(),
                             Convert.ToInt32(dr["Posicion"].ToString().Trim()),
                             Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                             dr["FechaRegistro"].ToString().Trim());

                        lstFormula.Add(objFormula);
                    }

            return booResult;
        }
    }
}