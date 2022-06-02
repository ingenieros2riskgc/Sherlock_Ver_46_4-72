using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cMail
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private Object thisLock = new Object();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;

        private string[] strMonths = new string[12] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" },
            strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

        #endregion Variables Globales
        public int LastCodModulo(string modulo)
        {
            DataTable dtInformacion = new DataTable();
            string consulta = string.Empty;
            int LastCodModulo = 0;
            consulta = string.Format("SELECT top 1 ([CodOrdenamiento]) as CodModulo FROM [Notificaciones].[Evento] where Modulo = '{0}' order by 1 desc", modulo);
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(consulta);
                cDataBase.desconectar();
                LastCodModulo = Convert.ToInt32(dtInformacion.Rows[0]["CodModulo"].ToString().Trim());

            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return LastCodModulo;
        }
        public void insertEventoMail(string NombreEvento, string Modulo, int CodModulo, string RequiereFechaCierre)
        {
            string consulta = string.Empty;
            consulta = string.Format("INSERT INTO [Notificaciones].[Evento] ([NombreEvento],[Modulo],[CodOrdenamiento],[RequiereFechaCierre])"
                + " VALUES('{0}','{1}',{2},'{3}')",NombreEvento, Modulo, CodModulo, RequiereFechaCierre);
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery(consulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        public int LastIdEventoMail()
        {
            DataTable dtInformacion = new DataTable();
            string consulta = string.Empty;
            int LastEvent = 0;
            consulta = string.Format("SELECT max(IdEvento) as LastId FROM[Notificaciones].[Evento]");
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(consulta);
                cDataBase.desconectar();
                LastEvent = Convert.ToInt32(dtInformacion.Rows[0]["LastId"].ToString().Trim());

            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return LastEvent;
        }
    }
}