using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsPHVADLL
    {
        /// <summary>
        /// Realiza la consulta para traer la lista de registros PHVA
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarPHVA(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT 0 as idphva,'O' as codigo,'--Seleccione--' as descripcion,null as fecharegistro,0 as idusuario " +
                    "union all "+
                    "SELECT [idphva],[codigo],[descripcion],[fecharegistro],[idusuario] FROM [Procesos].[tblPHVA]");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la lista de PHVA. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
    }
}