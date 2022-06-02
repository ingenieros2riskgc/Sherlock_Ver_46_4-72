using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaDesviaciones
    {
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Desviaciones
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaDesviaciones(clsDTOParaDesviaciones ParaDesviaciones, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaDesviaciones cDALDesviaciones = new clsDALUpdParaDesviaciones();

            booResult = cDALDesviaciones.mtdActualizarParaDesviaciones(ParaDesviaciones, ref strErrMsg);

            return booResult;
        }
    }
}