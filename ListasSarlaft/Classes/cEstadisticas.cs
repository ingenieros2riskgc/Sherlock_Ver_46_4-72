using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cEstadisticas
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;

        public DataTable conteoROIEstudio(String Segmento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT(*) AS NumeroRegistros, LTRIM(RTRIM(Segmento)) AS Segmento FROM Proceso.ROI WHERE Segmento LIKE '%" + Segmento + "%' GROUP BY Segmento");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable conteoInusualidadFCPN(String Conve, String Estado)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT(*) AS NumeroRegistros FROM Listas.ConocimientoCliente INNER JOIN Listas.InfoFormPN ON Listas.ConocimientoCliente.IdConocimientoCliente = Listas.InfoFormPN.IdConocimientoCliente INNER JOIN Listas.InfoFormDocsInu ON Listas.ConocimientoCliente.IdConocimientoCliente = Listas.InfoFormDocsInu.IdConocimientoCliente WHERE ((LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNNombres, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNNumeroDocumento, ''))) != '')) AND ((Listas.InfoFormDocsInu." + Conve.Trim() + " = N'" + Estado.Trim() + "'))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable conteoInusualidadFCPJ(String Conve, String Estado)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT(*) AS NumeroRegistros FROM Listas.ConocimientoCliente INNER JOIN Listas.InfoFormDocsInu ON Listas.ConocimientoCliente.IdConocimientoCliente = Listas.InfoFormDocsInu.IdConocimientoCliente INNER JOIN Listas.InfoFormPJ ON Listas.ConocimientoCliente.IdConocimientoCliente = Listas.InfoFormPJ.IdConocimientoCliente WHERE ((LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJNombres, ''))) !='') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJNumeroDocumento, ''))) != '')) AND ((Listas.InfoFormDocsInu." + Conve.Trim() + " = N'" + Estado.Trim() + "')) ");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
    }
}