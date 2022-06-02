using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ListasSarlaft.Classes
{
    public class cCargaMasivaROI
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private Object thisLock = new Object();

        #endregion Variables Globales

        public DataTable loadInfoROI(string idROIs)
        {
            DataTable dtInformacion = new DataTable();
            string strROI = "select IdTipoIden, Identificacion, NombreApellido, Indicador, MensajeCorreo, FechaDeteccion FROM Proceso.RegistroOperacion where IdRegistroOperacion Between (" + idROIs + "+1) and (select MAX(IdRegistroOperacion) from Proceso.RegistroOperacion)";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strROI);
                    cDataBase.desconectar();
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

        public void registarROI(string Tipo, string TipoId, string Id, string NombreApellido, string Descripcion, string Mensaje, string Fecha, string RM)
        {
            #region Variables
            string strConsultaInsertar = string.Empty, strCamposInsertar = string.Empty;
            #endregion Variables

            try
            {
                #region Inserts
                strCamposInsertar = "(IdTipoRegistro, IdEstadoOperacion, Identificacion, NombreApellido, NombreIndicador, Indicador, MensajeCorreo, FechaRegistro, FechaDeteccion, IdTipoIden)";
                strConsultaInsertar = string.Format("INSERT INTO Proceso.RegistroOperacion {0} VALUES ({1}, 1, {2}, '{3}', '{4}', '{5}', '{6}', GETDATE(), CONVERT(date,'{7}',126), {8})", strCamposInsertar, Tipo,
                    Id, NombreApellido, RM, Descripcion, Mensaje, Fecha, TipoId);
                #endregion Inserts

                lock (thisLock)
                {
                    cDataBase.conectar();
                    cDataBase.ejecutarQuery(strConsultaInsertar);
                    cDataBase.desconectar();
                }
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        
        public byte[] mtdDescargarPlantillaROIs()
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo],[ArchivoExcel] FROM [Riesgos].[RiesgosPlantillaMasiva] where [IdArchivos] = 1");

                cDataBase.mtdConectarSql();
                bInfo = cDataBase.mtdEjecutarConsultaSqlPdf(strConsulta);
                cDataBase.mtdDesconectarSql();
            }
            catch (Exception ex)
            {
                cDataBase.mtdDesconectarSql();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return bInfo;
        }

        public DataTable DataTipoId()
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdDetalleTipo,NombreDetalle FROM Parametrizacion.DetalleTipos WHERE IdTipo = 11");
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

        public DataTable NumeroActual()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            try
            {
                strConsulta = string.Format("select MAX(IdRegistroOperacion) from Proceso.RegistroOperacion");
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

        public DataTable IdRm()
        {
            DataTable strCodigoControl = new DataTable();
            try
            {
                cDataBase.conectar();
                strCodigoControl = cDataBase.ejecutarConsulta("SELECT DISTINCT'RM_'+CONVERT(varchar(10),(SELECT (MAX(SUBSTRING(NombreIndicador,4,10)+1)) FROM Proceso.RegistroOperacion WHERE NombreIndicador LIKE 'RM_%'))FROM Proceso.RegistroOperacion");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return strCodigoControl;
        }

        public DataTable IdTipo()
        {
            DataTable strCodigoControl = new DataTable();
            try
            {
                cDataBase.conectar();
                strCodigoControl = cDataBase.ejecutarConsulta("select IdTipoRegistro from Proceso.TipoRegistro where NombreTipoRegistro = 'ROI Masivo'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return strCodigoControl;
        }
    }
}