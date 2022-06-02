using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParaProbabilidad
    {
        /// <summary>
        /// Metodo para actualizar el registro de Probabilidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParaProbabilidad(clsDTOParaProbabilidad Probabilidad, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdParaProbabilidad cDALProbabilidad = new clsDALUpdParaProbabilidad();

            booResult = cDALProbabilidad.mtdActualizarParaProbabilidad(Probabilidad, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar la imagen para Frecuencia
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarArchivo(ref bool booResult,string nombrearchivo, int length, byte[] archivo, string modulo, ref string strErrMsg, string extension)
        {
            //bool booResult = false;
            clsDALInsParaProbabilidad cDtProbabilidad = new clsDALInsParaProbabilidad();
            try
            {
                booResult = cDtProbabilidad.Guardar(nombrearchivo, length, archivo, modulo, ref strErrMsg, extension);
            }
            catch (Exception ex)
            {
                strErrMsg = "nombrearchivo: " + nombrearchivo + " length: " + length  + " extension:" + extension;
            }
            return booResult;
        }
        /// <summary>
        /// Metodo que permite extraer la imagen de frecuencia
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public byte[] mtdDownLoadFile(ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALselParaProbabilidad cDtProbabilidad = new clsDALselParaProbabilidad();
            byte[] file = null;
            booResult = cDtProbabilidad.mtdDownLoadFile(ref dtInfo, ref strErrMsg);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            file = (byte[])dr["archivo"];
                        }
                    }
                    else
                        file = null;
                }
                else
                    file = null;
            }
            return file;
        }
    }
}