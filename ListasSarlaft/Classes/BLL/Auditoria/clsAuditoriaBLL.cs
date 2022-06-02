using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAuditoriaBLL
    {
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarAuditoria(clsAuditoriaDTO cAuditoria, ref string strErrMsg)
        {
            bool booResult = false;
            clsAuditoriaDAL cDtAuditoria = new clsAuditoriaDAL();

            booResult = cDtAuditoria.mtdInsertarAuditoria(cAuditoria, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarAuditoriaProceso(int IdAuditoria, int IdProceso, int IdTipoProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsAuditoriaDAL cDtAuditoria = new clsAuditoriaDAL();

            booResult = cDtAuditoria.mtdInsertarAuditoriaProceso(IdAuditoria,  IdProceso,  IdTipoProceso, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la no corformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdAuditoria(ref string strErrMsg)
        {
            int LastId = 0;
            clsAuditoriaDAL cDtAuditoria = new clsAuditoriaDAL();
            DataTable dt = cDtAuditoria.mtdLastIdAuditoria(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
    }
    
}