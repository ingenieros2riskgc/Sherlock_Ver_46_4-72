using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cProcesos : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;

        public void SPCargarInformacion()
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarSP("SP_CargarInformacion");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void SPReiniciarSistema()
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarSP("SPReiniciarSistema");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void SPSegmentacion()
        {
            try
            {
                parameters = new OleDbParameter[1];                
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Integer);
                parameter.Value = IdUsuario;
                parameters[0] = parameter;
                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("SP_Segmentacion", parameters);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void SPAnalisisPEPS()
        {
            try
            {
                parameters = new OleDbParameter[1];
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Integer);
                parameter.Value = IdUsuario;
                parameters[0] = parameter;
                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("SP_AnalisisPEPS", parameters);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable conteoROI()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT (*) AS NumeroRegistros, (SELECT TOP (1) CONVERT(varchar, FechaRegistro, 109) FROM Proceso.ROI) AS FechaRegistroInicial, (SELECT TOP (1) CONVERT(varchar, FechaRegistro, 109) FROM Proceso.ROI ORDER BY FechaRegistro DESC) AS FechaRegistroFinal FROM Proceso.ROI");
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

        public void ActivarControlVersion(string IdArControlVersion, int Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("UPDATE Procesos.tblControlVersion SET bitActivo = " + Estado + " WHERE ID='" + IdArControlVersion + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
    }
}