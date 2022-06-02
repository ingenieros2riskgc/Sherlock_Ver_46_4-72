using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaRiesgoAsociadoLaft
    {
        /// <summary>
        /// Metodo para insertar el registro de Riesgo Asociado Laft
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaRiesgoAsociadoLaft(clsDTOParaRiesgoAsociadoLaft RiesgoAsociadoLaft, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaRiesgoAsociadoLaft cDALRiesgoLaft = new clsDALInsParaRiesgoAsociadoLaft();

            booResult = cDALRiesgoLaft.mtdInsertarParaRiesgoAsociadoLaft(RiesgoAsociadoLaft, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Riesgo Asociado Laft
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaRiesgoAsociadoLaft(clsDTOParaRiesgoAsociadoLaft RiesgoAsociadoLaft, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaRiesgoAsociadoLaft cDALRiesgoLaft = new clsDALUpdParaRiesgoAsociadoLaft();

            booResult = cDALRiesgoLaft.mtdActualizarParaRiesgoAsociadoLaft(RiesgoAsociadoLaft, ref strErrMsg);

            return booResult;
        }
    }
}