using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace clsLogica
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

        public DataTable mtdEjecutarConsultaSQL(string strConsulta)
        {

            DataTable dtInfo = new DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strConsulta, sqlCnn);
            sqlAdapter.SelectCommand.CommandType = CommandType.Text;
            sqlAdapter.SelectCommand.CommandTimeout = 3600;
            sqlAdapter.Fill(dtInfo);

            return dtInfo;
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
    }
}
