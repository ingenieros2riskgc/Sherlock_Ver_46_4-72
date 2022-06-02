using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLTipoLegislacion
    {
        /// <summary>
        /// Metodo para insertar el registro de Tipo Legislacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarTipoLegislacion(clsDTOTipoLegislacion TipoLegislacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsTipoLegislacion cDALTipoLegislacion = new clsDALInsTipoLegislacion();

            booResult = cDALTipoLegislacion.mtdInsertarTipoLegislacion(TipoLegislacion, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Tipo Legislacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarTipoLegislacion(clsDTOTipoLegislacion TipoLegislacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdTipoLegislacion cDALTipoLegislacion = new clsDALUpdTipoLegislacion();

            booResult = cDALTipoLegislacion.mtdActualizarTipoLegislacion(TipoLegislacion, ref strErrMsg);

            return booResult;
        }
    }
}