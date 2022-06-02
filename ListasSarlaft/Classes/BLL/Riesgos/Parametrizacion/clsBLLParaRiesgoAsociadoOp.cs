using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaRiesgoAsociadoOp
    {
        /// <summary>
        /// Metodo para insertar el registro de Parametrizacion Tipo Evento Operativo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaRiesgoAsociadoOperativo(clsDTOParaRiesgoAsociadoOperativo RiesgoAsociadoOP, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaRiesgoAsociadoOperativo cDALRiesgo = new clsDALInsParaRiesgoAsociadoOperativo();

            booResult = cDALRiesgo.mtdInsertarParaRiesgoAsociadoOperativo(RiesgoAsociadoOP, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Tipo Evento Operativo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaRiesgoAsociadoOperativo(clsDTOParaRiesgoAsociadoOperativo RiesgoAsociadoOP, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaRiesgoAsociadoOperativo cDALRiesgo = new clsDALUpdParaRiesgoAsociadoOperativo();

            booResult = cDALRiesgo.mtdActualizarParaRiesgoAsociadoOperativo(RiesgoAsociadoOP, ref strErrMsg);

            return booResult;
        }
    }
}