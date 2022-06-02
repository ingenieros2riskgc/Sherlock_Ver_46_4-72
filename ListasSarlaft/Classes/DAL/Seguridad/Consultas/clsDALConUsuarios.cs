using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALConUsuarios
    {
        #region VariablesGlobales
        SqlConnection LaConexion = new SqlConnection();
        string strConnection;
        #endregion VariablesGlobales
        public bool mtdConsultarUsuarios(ref DataTable dtCaracOut, ref string strErrMsg, clsDTOUsuarios UserConsulta)
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
                if (UserConsulta.strNumeroDocumento.ToString() != "")
                    condicion = "WHERE (a.NumeroDocumento LIKE '%" + UserConsulta.strNumeroDocumento.ToString().Trim() + "%') ";

                if (UserConsulta.strNombres.ToString() != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.Nombres LIKE '%" + UserConsulta.strNombres.ToString().Trim() + "%') ";
                    else
                        condicion = condicion + "AND (a.Nombres LIKE '%" + UserConsulta.strNombres.ToString().Trim() + "%') ";
                }
                if (UserConsulta.strApellidos.ToString() != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.Apellidos LIKE '%" + UserConsulta.strApellidos.ToString().Trim() + "%') ";

                    else
                        condicion = condicion + "AND (a.Apellidos LIKE '%" + UserConsulta.strApellidos.ToString().Trim() + "%') ";
                }
                LaConexion.ConnectionString = strConnection;
                LaConexion.Open();
                SqlCommand comando = new SqlCommand("Listas.spSEGURIDADcargarUsuarios", LaConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CONDICION", condicion);
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
                strErrMsg = string.Format("Error al consultar el usuario. [{0}]", ex.Message);
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