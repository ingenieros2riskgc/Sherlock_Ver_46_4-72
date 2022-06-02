using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaFactorRiesgoOperativo
    {
        /// <summary>
        /// Metodo para insertar el registro de Factor Riesgo Operativo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaFactorRiesgoOperativo(clsDTOParaFactorRiesgoOperativo FactorRiesgoOp, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaFactorRiesgoOperativo cDALFactor = new clsDALInsParaFactorRiesgoOperativo();

            booResult = cDALFactor.mtdInsertarParaFactorRiesgoOperativo(FactorRiesgoOp, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Factor Riesgo Operativo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaFactorRiesgoOperativo(clsDTOParaFactorRiesgoOperativo FactorRiesgoOp, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaFactorRiesgoOperativo cDALFactor = new clsDALUpdParaFactorRiesgoOperativo();

            booResult = cDALFactor.mtdActualizarParaFactorRiesgoOperativo(FactorRiesgoOp, ref strErrMsg);

            return booResult;
        }
    }
}