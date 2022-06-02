using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaTipoRiesgoOperativo
    {
        /// <summary>
        /// Metodo para insertar el registro de Parametrizacion Tipo Riesgo Operativo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaTipoRiesgoOperativo(clsDTOParaTipoRiesgoOperativo TipoRiesgoOp, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaTipoRiesgoOperativo cDALTipoRiesgo = new clsDALInsParaTipoRiesgoOperativo();

            booResult = cDALTipoRiesgo.mtdInsertarParaTipoRiesgoOperativo(TipoRiesgoOp, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Tipo Riesgo Operativo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaTipoRiesgoOperativo(clsDTOParaTipoRiesgoOperativo TipoRiesgoOp, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaTipoRiesgoOperativo cDALTipoRiesgo = new clsDALUpdParaTipoRiesgoOperativo();

            booResult = cDALTipoRiesgo.mtdActualizarParaTipoRiesgoOperativo(TipoRiesgoOp, ref strErrMsg);

            return booResult;
        }
    }
}