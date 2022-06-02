using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaTipoPrueba
    {
        /// <summary>
        /// Metodo para insertar el registro de Parametrizacion Tipo prueba de Plan Evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaTipoPrueba(clsDTOParaTipoPrueba TipoPrueba, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaTipoPrueba cDALTipoPrueba = new clsDALInsParaTipoPrueba();

            booResult = cDALTipoPrueba.mtdInsertarParaTipoPrueba(TipoPrueba, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Tipo prueba de Plan Evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaTipoPrueba(clsDTOParaTipoPrueba TipoPrueba, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaTipoPrueba cDALTipoPrueba = new clsDALUpdParaTipoPrueba();

            booResult = cDALTipoPrueba.mtdActualizarParaTipoPrueba(TipoPrueba, ref strErrMsg);

            return booResult;
        }
    }
}