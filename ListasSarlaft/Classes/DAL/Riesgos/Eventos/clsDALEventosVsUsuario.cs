using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALEventosVsUsuario
    {
        #region VariablesGlobales
        SqlConnection LaConexion = new SqlConnection();
        //string strConnection;
        #endregion VariablesGlobales
        public bool mtdConsultarEventosVsUsuario(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdUsuarioJerarquia)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [IdEvento],[CodigoEvento],[DescripcionEvento],[CuantiaPerdida],[IdGeneraEvento],[GeneraEvento]"+
                ",[GeneradorEvento],[ResponsableEvento],[ResponsableSolucion],[IdClase],[NombreClaseEvento]" +
                " FROM [Riesgos].[vwEventosVsUsuario]"+
                " where [ResponsableEvento] = {0} or [GeneraEvento] = {0}", IdUsuarioJerarquia);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los eventos del usuario. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public DataTable mtdLastIdEncuesta(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la Encuesta. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
    }
}