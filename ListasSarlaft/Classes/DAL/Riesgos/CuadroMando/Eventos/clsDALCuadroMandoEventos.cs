using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ListasSarlaft.Classes
{
    public class clsDALCuadroMandoEventos
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private string[] strMonths = new string[12] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" },
            strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
        public DataTable LoadInfoReporteEstadosEventos(ref string strErrMsg, clsDTOCuadroMandoEventosFiltro objFiltro)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            string strSelect = string.Empty;
            string strFrom = string.Empty;
            string strWhere = string.Empty;
            string strOrder = string.Empty;
            string strFechaIni = string.Empty;
            string strFechaFin = string.Empty;
            //string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            //string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                string fechaIni = objFiltro.dtFechaInicial.ToString("yyyy-MM-dd");
                string fechaFin = objFiltro.dtFechaFinal.ToString("yyyy-MM-dd");


                strSelect = "SELECT COUNT(IdEvento) as NumEvento, RE.IdEstado,  EE.Descripcion";

                    strFrom = "FROM [Riesgos].[Eventos] as RE " +
                    "INNER JOIN Eventos.Estado as EE on EE.IdEstado = RE.IdEstado ";
                if(objFiltro.dtFechaInicial != default(DateTime) && objFiltro.dtFechaFinal != default(DateTime))
                    strWhere = string.Format(" WHERE (FechaEvento BETWEEN CONVERT(datetime, '{0} 00:00', 120) AND CONVERT(datetime, '{1} 23:59', 120)) ", fechaIni, fechaFin);

                strOrder = "GROUP BY RE.IdEstado,EE.Descripcion";


                strConsulta = string.Format("{0} {1} {2} {3}", strSelect, strFrom, strWhere, strOrder);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable GetAllEventos(ref string strErrMsg)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            //string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            //string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                string strSelect = "SELECT COUNT(IdEvento) as CantEventos ";

                string strFrom = "FROM Riesgos.Eventos ";
                string strWhere = string.Empty;

                string strOrder = string.Empty;

                strConsulta = string.Format("{0} {1} {2} {3}", strSelect, strFrom, strWhere, strOrder);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable LoadInfoReporteEventosConsolidado(ref string strErrMsg, clsDTOCuadroMandoEventosFiltro objFiltro)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            string strSelect = string.Empty;
            string strFrom = string.Empty;
            string strWhere = string.Empty;
            string strOrder = string.Empty;
            string strFechaIni = string.Empty;
            string strFechaFin = string.Empty;
            cDataBase cDataBase = new cDataBase();
            SqlConnection LaConexion = new SqlConnection();
            string strConnection;
            dtInformacion.TableName = "Info";
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
            strConnection = connString.ToString().Trim();
            #endregion Variables
            try
            {

                LaConexion.ConnectionString = strConnection;
                LaConexion.Open();
                if(objFiltro.dtFechaFinal != default(DateTime) && objFiltro.dtFechaInicial != default(DateTime))
                {
                    SqlCommand comando = new SqlCommand("Riesgos.spReporteEventosConsolidado", LaConexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@FechaInicial", objFiltro.dtFechaInicial);
                    comando.Parameters.AddWithValue("@FechaFinal", objFiltro.dtFechaFinal);
                    //comando.ExecuteNonQuery();
                    using (SqlDataAdapter adapater = new SqlDataAdapter())
                    {
                        adapater.SelectCommand = comando;
                        adapater.Fill(dtInformacion);
                    }
                }else
                {
                    SqlCommand comando = new SqlCommand("Riesgos.spReporteEventosConsolidadoSinFecha", LaConexion);
                    //comando.ExecuteNonQuery();
                    using (SqlDataAdapter adapater = new SqlDataAdapter())
                    {
                        adapater.SelectCommand = comando;
                        adapater.Fill(dtInformacion);
                    }

                }
                
                
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        private string mtdConvertirFecha(string strFechaIn, int intTipoDia)
        {
            string strFechaOut = string.Empty, strMes = string.Empty, strDia = string.Empty;
            string[] strFechaPartida = strFechaIn.Split('-');

            #region Asignar Mes
            for (int i = 0; i < 12; i++)
            {
                if (strFechaPartida[0].ToString().ToUpper() == strMeses[i].ToString() ||
                    strFechaPartida[0].ToString().ToUpper() == strMonths[i].ToString())
                {
                    if ((i + 1) <= 9)
                        strMes = string.Format("0{0}", (i + 1).ToString());
                    else
                        strMes = (i + 1).ToString();
                    break;
                }
            }
            #endregion Asignar Mes

            #region Asignar Dia
            switch (intTipoDia)
            {
                case 1:
                    strDia = "01";
                    break;
                case 2:
                    switch (strMes)
                    {
                        case "02":
                            strDia = "28";
                            break;
                        case "01":
                        case "03":
                        case "05":
                        case "07":
                        case "08":
                        case "10":
                        case "12":
                            strDia = "31";
                            break;
                        case "04":
                        case "06":
                        case "09":
                        case "11":
                            strDia = "30";
                            break;

                    }
                    break;
            }
            #endregion Asignar Dia

            if (!string.IsNullOrEmpty(strMes))
            {
                strFechaOut = string.Format("{0}-{1}-{2}", strFechaPartida[1].ToString(), strMes, strDia);
            }

            return strFechaOut;
        }
    }
}