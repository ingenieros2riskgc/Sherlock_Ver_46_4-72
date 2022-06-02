using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALConServicios
    {
        #region VariablesGlobales
        SqlConnection LaConexion = new SqlConnection();
        string strConnection;
        #endregion VariablesGlobales
        public bool mtdConsultarServicios(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            dtCaracOut.TableName = "Info";
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
            strConnection = connString.ToString().Trim();
            #endregion Vars

            try
            {
                string condicion = string.Empty;
                LaConexion.ConnectionString = strConnection;
                LaConexion.Open();
                SqlCommand comando = new SqlCommand("Listas.spRIESGOSConsultarServicios", LaConexion);
                /*comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CONDICION", condicion);*/
                //comando.ExecuteNonQuery();
                using (SqlDataAdapter adapater = new SqlDataAdapter())
                {
                    adapater.SelectCommand = comando;
                    adapater.Fill(dtCaracOut);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}