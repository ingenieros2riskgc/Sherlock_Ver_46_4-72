using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;
using System.IO;


namespace clsDatos
{
    public class clsDatabase
    {
        private OleDbConnection oleDbCnn;
        private SqlConnection sqlCnn;

        public clsDatabase()
        {
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");

            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["ListasConnectionString"];
                oleDbCnn = new OleDbConnection(connString.ToString());


                System.Configuration.ConnectionStringSettings SqlconnStr = rootWebConfig.ConnectionStrings.ConnectionStrings["SarlaftConnectionString"];
                sqlCnn = new SqlConnection(SqlconnStr.ToString());
            }
        }

        public clsDatabase(string strConn, int intConnOp)
        {
            switch (intConnOp)
            {
                case 1:
                    sqlCnn = new SqlConnection(strConn.ToString());
                    break;

                case 2: oleDbCnn = new OleDbConnection(strConn.ToString());
                    break;
            }
        }


        public void conectar()
        {
            oleDbCnn.Open();
        }

        public void desconectar()
        {
            oleDbCnn.Close();
        }

        public DataTable ejecutarConsulta(String txtQuery)
        {
            DataTable dtInformation = new DataTable();
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(txtQuery, oleDbCnn);
            oleDbDataAdapter.SelectCommand.CommandType = CommandType.Text;
            oleDbDataAdapter.SelectCommand.CommandTimeout = 3600;
            oleDbDataAdapter.Fill(dtInformation);
            return dtInformation;
        }

        public DataTable ejecutarConsultas(String txtQuery)
        {
            DataTable dtInformation = new DataTable();
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(txtQuery, oleDbCnn);
            oleDbDataAdapter.SelectCommand.CommandType = CommandType.Text;
            oleDbDataAdapter.SelectCommand.CommandTimeout = 3600;
            oleDbDataAdapter.Fill(dtInformation);
            return dtInformation;
        }

        public DataTable ejecutarConsultaEvidencia(String txtQuery)
        {
            DataTable dtInformation = new DataTable();
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(txtQuery, oleDbCnn);
            oleDbDataAdapter.SelectCommand.CommandType = CommandType.Text;
            oleDbDataAdapter.SelectCommand.CommandTimeout = 3600;
            oleDbDataAdapter.Fill(dtInformation);
            return dtInformation;
        }

        public void ejecutarQuery(String txtQuery)
        {
            OleDbCommand oleDbCmn = new OleDbCommand(txtQuery, oleDbCnn);
            oleDbCmn.CommandType = CommandType.Text;
            oleDbCmn.CommandTimeout = 3600;
            oleDbCmn.ExecuteNonQuery();
        }

        public void ejecutarSP(String txtNombreSP)
        {
            OleDbCommand oleDbCmn = new OleDbCommand(txtNombreSP, oleDbCnn);
            oleDbCmn.CommandType = CommandType.StoredProcedure;
            oleDbCmn.CommandTimeout = 3600;
            oleDbCmn.ExecuteNonQuery();
        }

        public void ejecutarSPParametros(String txtNombreSP, OleDbParameter[] objParameter)
        {
            OleDbCommand oleDbCmm = new OleDbCommand(txtNombreSP, oleDbCnn);
            oleDbCmm.CommandType = CommandType.StoredProcedure;
            foreach (OleDbParameter objParametro in objParameter)
            {
                oleDbCmm.Parameters.Add(objParametro);
            }
            oleDbCmm.CommandTimeout = 3600;
            oleDbCmm.ExecuteNonQuery();
        }

        public void mtdConectarSql()
        {
            sqlCnn.Open();
        }

        public void mtdDesconectarSql()
        {
            sqlCnn.Close();
        }

        public void mtdEjecutarConsultaSQL(string strConsulta, byte[] bParam)
        {
            //SarlaftConnectionString
            using (SqlCommand sqlComm = new SqlCommand(strConsulta, sqlCnn))
            {
                sqlComm.Parameters.Add("@PdfData", bParam);
                sqlComm.CommandTimeout = 3600;
                sqlComm.ExecuteNonQuery();
            }
        }

        public int mtdEjecutarConsultaSQLRetorno(string strConsulta, byte[] bParam)
        {
            int intUltimo = 0;

            //SarlaftConnectionString
            using (SqlCommand sqlComm = new SqlCommand(strConsulta, sqlCnn))
            {
                sqlComm.Parameters.Add("@PdfData", bParam);
                sqlComm.CommandTimeout = 3600;
                //sqlComm.ExecuteNonQuery();
                SqlDataReader dr = sqlComm.ExecuteReader(System.Data.CommandBehavior.Default);
                if (dr.Read())
                    intUltimo = Convert.ToInt32(dr.GetValue(0).ToString());

                dr.Close();
            }

            return intUltimo;
        }

        public int mtdEjecutarConsultaSQLRetorno(string strConsulta)
        {
            int intUltimo = 0;

            //SarlaftConnectionString
            using (SqlCommand sqlComm = new SqlCommand(strConsulta, sqlCnn))
            {
                sqlComm.CommandTimeout = 3600;
                //sqlComm.ExecuteNonQuery();
                SqlDataReader dr = sqlComm.ExecuteReader(System.Data.CommandBehavior.Default);
                if (dr.Read())
                    intUltimo = Convert.ToInt32(dr.GetValue(0).ToString());

                dr.Close();
            }

            return intUltimo;
        }

        public DataTable mtdEjecutarConsultaSQL(string strConsulta)
        {
            DataTable dtInfo = new DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strConsulta, sqlCnn);
            sqlAdapter.SelectCommand.CommandType = CommandType.Text;
            sqlAdapter.SelectCommand.CommandTimeout = 3600;
            sqlAdapter.Fill(dtInfo);

            return dtInfo;
        }

        public DataSet mtdEjecutarSPParametroSQL(string strNombreSP, string strDSName, SqlParameter[] objParameter)
        {
            //DataTable dtInfo = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            DataSet dsInfo = new DataSet(strDSName);

            cmd = new SqlCommand(strNombreSP, sqlCnn);

            foreach (SqlParameter objParametro in objParameter)
            {
                cmd.Parameters.Add(objParametro);
            }

            cmd.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand = cmd;
            sqlAdapter.Fill(dsInfo, "DataSet1");

            return dsInfo;
        }

        public byte[] mtdEjecutarConsultaSqlPdf(string strConsulta)
        {
            byte[] bInfo = null;

            using (SqlCommand sqlComm = new SqlCommand(strConsulta, sqlCnn))
            {
                SqlDataReader dr = sqlComm.ExecuteReader(System.Data.CommandBehavior.Default);
                if (dr.Read())
                {
                    bInfo = (byte[])dr.GetValue(1);
                }
                dr.Close();
            }
            return bInfo;
        }

        /// <summary>
        /// Ejecuta procedimiento almacenado que devuelve un valor integer. El procedimiento a llamar debe tener el parametro de salida @Resultado
        /// </summary>
        /// <param name="NombreSp">Nombre del procedimiento</param>
        /// <param name="Parametros">Lista de parametros</param>
        /// <returns>Int</returns>
        public int EjecutarSPParametrosReturnInteger(string NombreSp, List<SqlParameter> Parametros)
        {
            SqlConnection cnn = sqlCnn;
            try
            {
                cnn.Open();
                SqlCommand sqlCmd = new SqlCommand(NombreSp, cnn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddRange(Parametros.ToArray());
                sqlCmd.CommandTimeout = 3600;
                sqlCmd.ExecuteNonQuery();
                int resultado = (int)sqlCmd.Parameters["@Resultado"].Value;
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public DataTable EjecutarSPParametrosReturnDatatable(string NombreSp, List<SqlParameter> Parametros)
        {
            SqlConnection cnn = sqlCnn;
            try
            {
                DataTable dt = new DataTable();
                cnn.Open();
                SqlCommand sqlCmd = new SqlCommand(NombreSp, cnn);
                SqlDataAdapter sqlAdt = new SqlDataAdapter();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddRange(Parametros.ToArray());
                sqlCmd.CommandTimeout = 3600;
                sqlAdt.SelectCommand = sqlCmd;
                sqlAdt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
