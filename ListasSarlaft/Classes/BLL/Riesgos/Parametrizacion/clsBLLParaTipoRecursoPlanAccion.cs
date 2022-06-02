using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaTipoRecursoPlanAccion
    {
        /// <summary>
        /// Metodo para insertar el registro de Tipo Recurso del Plan de Evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaTipoRecurso(clsDTOParaTipoRecursoPlanAccion TipoRecurso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaTipoRecursoPlanAccion cDALTipoRecurso = new clsDALInsParaTipoRecursoPlanAccion();

            booResult = cDALTipoRecurso.mtdInsertarParaTipoRecursoPlanAccion(TipoRecurso, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Tipo Recurso del Plan de Evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaTipoRecurso(clsDTOParaTipoRecursoPlanAccion TipoRecurso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaTipoRecursoPlanAccion cDALTipoRecurso = new clsDALUpdParaTipoRecursoPlanAccion();

            booResult = cDALTipoRecurso.mtdActualizarParaTipoRecursoPlanAccion(TipoRecurso, ref strErrMsg);

            return booResult;
        }
    }
}