using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReporteDocumentoBLL
    {
        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsReporteDocumentos> mtdConsultarReporteDocumentos(ref string strErrMsg, int IdTipoDocumento)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsReporteDocumentos> lstReporteDocumentos = new List<clsReporteDocumentos>();
            clsReporteDocumentosDAL cDtReporte = new clsReporteDocumentosDAL();
            clsReporteDocumentos objReporteDocumentos = new clsReporteDocumentos();
            #endregion Vars

            dtInfo = cDtReporte.mtdConsultarReporteDocumentos(ref strErrMsg, IdTipoDocumento);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objReporteDocumentos = new clsReporteDocumentos();
                        objReporteDocumentos.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                        objReporteDocumentos.strNombreDocumento = dr["NombreDocumento"].ToString().Trim();
                        objReporteDocumentos.strNombreArchivo = dr["Nombre Archivo"].ToString().Trim();
                        objReporteDocumentos.strCodigoDocumento = dr["CodigoDocumento"].ToString().Trim();
                        objReporteDocumentos.strFechaImplementacion = dr["FechaImplementacion"].ToString().Trim();
                        objReporteDocumentos.strVersion = dr["Version"].ToString().Trim();
                        objReporteDocumentos.strNombreProceso = dr["NombreProceso"].ToString().Trim();
                        objReporteDocumentos.strNombreResponsable = dr["NombreHijo"].ToString().Trim();
                        objReporteDocumentos.strFechaModificacion = dr["FechaModificacion"].ToString().Trim();
                        lstReporteDocumentos.Add(objReporteDocumentos);
                    }
                }
                else
                {
                    lstReporteDocumentos = null;
                    //strErrMsg = "No hay información de Actividad.";
                }
            }
            else
                lstReporteDocumentos = null;

            return lstReporteDocumentos;
        }
    }
}