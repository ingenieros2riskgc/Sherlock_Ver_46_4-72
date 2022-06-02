using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaCalificacionRiesgoParticular
    {
        /// <summary>
        /// Metodo para insertar el registro de Calificacion Riesgo Particular
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaCalificacionRiesgoParticular(clsDTOParaCalificacionRiesgoParticular CalificacionRiesgoParticular, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaCalificacionRiesgoParticular cDALCalificacionRiesgo = new clsDALInsParaCalificacionRiesgoParticular();

            booResult = cDALCalificacionRiesgo.mtdInsertarParaCalificacionRiesgoParticular(CalificacionRiesgoParticular, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Calificacion Riesgo Particular
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaCalificacionRiesgoParticular(clsDTOParaCalificacionRiesgoParticular CalificacionRiesgoParticular, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaCalificacionRiesgoParticular cDALCalificacionRiesgo = new clsDALUpdParaCalificacionRiesgoParticular();

            booResult = cDALCalificacionRiesgo.mtdActualizarParaCalificacionRiesgoParticular(CalificacionRiesgoParticular, ref strErrMsg);

            return booResult;
        }
    }
}