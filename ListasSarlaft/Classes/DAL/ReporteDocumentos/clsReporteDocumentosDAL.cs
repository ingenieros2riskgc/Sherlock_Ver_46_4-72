using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReporteDocumentosDAL
    {
        /// <summary>
        /// Realiza la consulta para traer todas las Actividades
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarReporteDocumentos(ref string strErrMsg, int IdTipoDocumento)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {

                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdTipoDocumento", SqlDbType = SqlDbType.VarChar, Value =  IdTipoDocumento }
                };
                dtInformacion = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarDocumentosReportes]", parametros);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el reporte de documentos. [{0}]", ex.Message);
            }

            return dtInformacion;
        }
    }
}