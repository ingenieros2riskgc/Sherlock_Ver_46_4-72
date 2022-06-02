using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAcmCierreAdjuntosBLL
    {
        /// <summary>
        /// Realiza la insercion de los campos de los adjuntos para el cierre del ACM
        /// </summary>
        /// <param name="objActividad">Informacion de los Adjuntos</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarAdjuntosCierre(clsAcmCierreAdjuntos objAdjuntos, ref string strErrMsg)
        {
            bool booResult = false;
            clsAcmCierreAdjuntosDLL dbAdjuntos = new clsAcmCierreAdjuntosDLL();

            booResult = dbAdjuntos.mtdInsertarAdjuntosCierre(objAdjuntos, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Realiza la consulta de  los adjuntos para el cierre del ACM
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsAcmCierreAdjuntos> mdtConsultarAdjuntosCierre(ref string strErrMsg, int idacm)
        {
            try
            {
                clsAcmCierreAdjuntosDLL objData = new clsAcmCierreAdjuntosDLL();
                
                    return objData.mtdConsultarCierreAdjuntos(ref strErrMsg, idacm);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Realiza la eliminacion del archivo adjunto del Cierre de ACM
        /// </summary>
        /// <param name="intIdAcm">Id del acm</param>
        /// <param name="intIdAdjuntoAcm">Id del adjunto del acm</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdDeleteAdjuntoCierre(int idAcm, int idAdjuntoAcm, ref string strErrMsg)
        {
            bool booResult = false;
            clsAcmCierreAdjuntosDLL dbAdjuntos = new clsAcmCierreAdjuntosDLL();

            booResult = dbAdjuntos.mtdDeleteAdjuntoCierre(idAcm, idAdjuntoAcm, ref strErrMsg);

            return booResult;
        }
    }
}