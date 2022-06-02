using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsArchivosRegistroRequerimientos
    {
        /// <summary>
        /// Realiza la consulta de Archivos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        

        public int mtdConsecutivoArchivo(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            int intResult = 0;
            clsDtArchivosCalidad cDtArchivoCalidad = new clsDtArchivosCalidad();
            #endregion Vars

            dtInfo = cDtArchivoCalidad.mtdConsecutivoArchivo(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intResult = Convert.ToInt32(dtInfo.Rows[0]["Consecutivo"].ToString().Trim());
                else
                    intResult = 1;
            }

            return intResult;
        }

        public bool mtdInsertarArchivo(clsArchivosCalidad objArchivo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtArchivosCalidad cDtArchivoCalidad = new clsDtArchivosCalidad();

            booResult = cDtArchivoCalidad.mtdInsertarArchivo(objArchivo, ref strErrMsg);

            return booResult;
        }

    }
}