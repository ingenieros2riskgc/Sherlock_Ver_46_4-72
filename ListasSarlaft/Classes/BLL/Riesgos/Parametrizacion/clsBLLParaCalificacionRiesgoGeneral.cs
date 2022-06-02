using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaCalificacionRiesgoGeneral
    {
        /// <summary>
        /// Metodo para insertar el registro de Calificacion Riesgo General
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaCalificacionRiesgoGeneral(clsDTOParaCalificacionRiesgoGeneral CalificacionRiesgoGeneral, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaCalificacionRiesgoGeneral cDALCalificacionRiesgo = new clsDALInsParaCalificacionRiesgoGeneral();

            booResult = cDALCalificacionRiesgo.mtdInsertarParaCalificacionRiesgoGeneral(CalificacionRiesgoGeneral, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Calificacion Riesgo General
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaCalificacionRiesgoGeneral(clsDTOParaCalificacionRiesgoGeneral CalificacionRiesgoGeneral, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaCalificacionRiesgoGeneral cDALCalificacionRiesgo = new clsDALUpdParaCalificacionRiesgoGeneral();

            booResult = cDALCalificacionRiesgo.mtdActualizarParaCalificacionRiesgoGeneral(CalificacionRiesgoGeneral, ref strErrMsg);

            return booResult;
        }
    }
}