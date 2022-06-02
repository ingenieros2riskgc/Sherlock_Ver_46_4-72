using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaPeriodicidad
    {
        /// <summary>
        /// Metodo para insertar el registro de Parametrizacion Periodicidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaPeriodicidad(clsDTOParaPeriodicidad ParaPeriodicidad, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaPeriodicidad cDALPeriodicidad = new clsDALInsParaPeriodicidad();

            booResult = cDALPeriodicidad.mtdInsertarParaPeriodicidad(ParaPeriodicidad, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Periodicidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaPeriodicidad(clsDTOParaPeriodicidad ParaPeriodicidad, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaPeriodicidad cDALPeriodicidad = new clsDALUpdParaPeriodicidad();

            booResult = cDALPeriodicidad.mtdActualizacionParaPeriodicidad(ParaPeriodicidad, ref strErrMsg);

            return booResult;
        }
    }
}