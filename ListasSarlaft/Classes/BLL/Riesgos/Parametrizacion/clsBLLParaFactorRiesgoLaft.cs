using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaFactorRiesgoLaft
    {
        /// <summary>
        /// Metodo para insertar el registro de Factor Riesgo Laft
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaFactorRiesgoLaft(clsDTOParaFactorRiesgoLaft FactorRiesgoLaft, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaFactorRiesgoLaft cDALFactorLaft = new clsDALInsParaFactorRiesgoLaft();

            booResult = cDALFactorLaft.mtdInsertarParaFactorRiesgoLaft(FactorRiesgoLaft, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Factor Riesgo Laft
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaFactorRiesgoLaft(clsDTOParaFactorRiesgoLaft FactorRiesgoLaft, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaFactorRiesgoLaft cDALFactorLaft = new clsDALUpdParaFactorRiesgoLaft();

            booResult = cDALFactorLaft.mtdActualizaeParaFactorRiesgoLaft(FactorRiesgoLaft, ref strErrMsg);

            return booResult;
        }
    }
}