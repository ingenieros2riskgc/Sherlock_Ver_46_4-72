using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaCausas
    {
        /// <summary>
        /// Metodo para insertar el registro de Causas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaCausas(clsDTOParaCausas Causas, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaCausas cDALCausas = new clsDALInsParaCausas();

            booResult = cDALCausas.mtdInsertarParaCausas(Causas, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Causas
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaCausas(clsDTOParaCausas Causas, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaCausas cDALCausas = new clsDALUpdParaCausas();

            booResult = cDALCausas.mtdActualizarParaCausas(Causas, ref strErrMsg);

            return booResult;
        }
    }
}