using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaCalificacionRiesgo
    {
        /// <summary>
        /// Metodo para insertar el registro de Calificacion Riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaCalificacionRiesgo(clsDTOParaCalificacionRiesgo CalificacionRiesgo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaCalificacionRiesgo cDALCalificacionRiesgo = new clsDALInsParaCalificacionRiesgo();

            booResult = cDALCalificacionRiesgo.mtdInsertarParaCalificacionRiesgo(CalificacionRiesgo, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Calificacion Riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaCalificacionRiesgo(clsDTOParaCalificacionRiesgo CalificacionRiesgo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaCalificacionRiesgo cDALCalificacionRiesgo = new clsDALUpdParaCalificacionRiesgo();

            booResult = cDALCalificacionRiesgo.mtdActualizarParaCalificacionRiesgo(CalificacionRiesgo, ref strErrMsg);

            return booResult;
        }
    }
}