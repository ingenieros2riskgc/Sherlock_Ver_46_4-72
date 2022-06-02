using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaEstadoLegislacion
    {
        /// <summary>
        /// Metodo para insertar el registro de Parametrizacion Estado Legislacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaEstadoLegislacion(clsDTOParaEstadoLegislacion EstadoLegislacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaEstadoLegislacion cDALEstadoLegislacion = new clsDALInsParaEstadoLegislacion();

            booResult = cDALEstadoLegislacion.mtdInsertarParaEstadoLegislacion(EstadoLegislacion, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Estado Legislacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaEstadoLegislacion(clsDTOParaEstadoLegislacion EstadoLegislacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaEstadoLegislacion cDALEstadoLegislacion = new clsDALUpdParaEstadoLegislacion();

            booResult = cDALEstadoLegislacion.mtdActualizarParaEstadoLegislacion(EstadoLegislacion, ref strErrMsg);

            return booResult;
        }
    }
}