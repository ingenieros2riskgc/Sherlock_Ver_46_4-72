using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaConsecuencias
    {
        /// <summary>
        /// Metodo para insertar el registro de Parametrizacion Consecuencias
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaConsecuencias(clsDTOParaConsecuencias Consecuencias, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaConsecuencias cDALConsecuencias = new clsDALInsParaConsecuencias();

            booResult = cDALConsecuencias.mtdInsertarParaConsecuencias(Consecuencias, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Consecuencias
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaConsecuencias(clsDTOParaConsecuencias Consecuencias, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaConsecuencias cDALConsecuencias = new clsDALUpdParaConsecuencias();

            booResult = cDALConsecuencias.mtdActualizarParaConsecuencias(Consecuencias, ref strErrMsg);

            return booResult;
        }
    }
}