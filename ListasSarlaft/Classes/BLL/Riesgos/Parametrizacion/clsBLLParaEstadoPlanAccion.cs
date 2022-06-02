using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaEstadoPlanAccion
    {
        /// <summary>
        /// Metodo para insertar el registro de Estado del Plan de Accion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaEstadoAccion(clsDTOParaEstadoPlanAccion EstadoPlanAccion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaEstadoPlanAccion cDALEstadoPlanAccion = new clsDALInsParaEstadoPlanAccion();

            booResult = cDALEstadoPlanAccion.mtdInsertarParaEstadoPlanAccion(EstadoPlanAccion, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Estado del Plan de Accion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaEstadoAccion(clsDTOParaEstadoPlanAccion EstadoPlanAccion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaEstadoPlanAccion cDALEstadoPlanAccion = new clsDALUpdParaEstadoPlanAccion();

            booResult = cDALEstadoPlanAccion.mtdActualizarParaEstadoPlanAccion(EstadoPlanAccion, ref strErrMsg);

            return booResult;
        }
    }
}