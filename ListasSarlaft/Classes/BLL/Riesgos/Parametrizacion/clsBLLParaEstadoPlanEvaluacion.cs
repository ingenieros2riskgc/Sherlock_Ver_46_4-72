using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaEstadoPlanEvaluacion
    {
        /// <summary>
        /// Metodo para insertar el registro de Estado del Plan de Evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaEstadoEvaluacion(clsDTOParaEstadoPlanEvaluacion EstadoPlanEva, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaEstadoPlanEva cDALEstadoPlanEva = new clsDALInsParaEstadoPlanEva();

            booResult = cDALEstadoPlanEva.mtdInsertarParaEstadoPlanEva(EstadoPlanEva, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Estado del Plan de Evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaEstadoEvaluacion(clsDTOParaEstadoPlanEvaluacion EstadoPlanEva, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaEstadoPlanEva cDALEstadoPlanEva = new clsDALUpdParaEstadoPlanEva();

            booResult = cDALEstadoPlanEva.mtdActualizarParaEstadoPlanEva(EstadoPlanEva, ref strErrMsg);

            return booResult;
        }
    }
}