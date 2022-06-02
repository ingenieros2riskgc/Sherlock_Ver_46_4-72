using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaPorcentajeCalificacion
    {
        /// <summary>
        /// Metodo para actualizar el registro de Parametrizacion Porcentaje Calificacion del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizacionParaPorcentajeCalificacion(clsDTOParaPorcentajeCalificacion PorcentajeCalificacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaPorcentajeCalificacion cDALPorcentajeCalificacion = new clsDALUpdParaPorcentajeCalificacion();

            booResult = cDALPorcentajeCalificacion.mtdActualizacionParaPorcentajeCalificacion(PorcentajeCalificacion, ref strErrMsg);

            return booResult;
        }
    }
}