using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaTipoEventoOperativo
    {
        /// <summary>
        /// Metodo para insertar el registro de Parametrizacion Tipo Evento Operativo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParaTipoEventoOperativo(clsDTOParaTipoEventoOperativo TipoEventiOp, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsParaTipoEventoOperativo cDALConsecuencias = new clsDALInsParaTipoEventoOperativo();

            booResult = cDALConsecuencias.mtdInsertarParaTipoEventoOperativo(TipoEventiOp, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Tipo Evento Operativo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaTipoEventoOperativo(clsDTOParaTipoEventoOperativo TipoEventiOp, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaTipoEventoOperativo cDALConsecuencias = new clsDALUpdParaTipoEventoOperativo();

            booResult = cDALConsecuencias.mtdActualizarParaTipoEventoOperativo(TipoEventiOp, ref strErrMsg);

            return booResult;
        }
    }
}