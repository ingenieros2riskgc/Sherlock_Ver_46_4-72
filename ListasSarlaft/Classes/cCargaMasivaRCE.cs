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
    public class cCargaMasivaRCE
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
        public DataTable DataUbicacionRiesgo()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdRegion],[NombreRegion],[IdPais],[NombrePais],[IdDepartamento],[NombreDepartamento],[IdCiudad]" +
                ",[NombreCiudad],[IdOficinaSucursal],[NombreOficinaSucursal] FROM[Riesgos].[vwUbicacionRiesgo]");
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
        public DataTable DataProcesosRiesgo()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdCadenaValor],[NombreCadenaValor],[IdMacroProceso],[MacroProceso],[IdProceso],[Proceso]" +
                ",[IdSubproceso],[SubProceso],[IdActividad] ,[NombreActividad] FROM [Riesgos].[vwProcesosRiesgos]");
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
        public DataTable DataClasificacionRiesgo()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdClasificacionRiesgo] as 'Id Riesgo Global',[NombreClasificacionRiesgo] as 'Riesgo Global',[IdClasificacionGeneralRiesgo],[NombreClasificacionGeneralRiesgo]" +
                ",[IdClasificacionParticularRiesgo],[NombreClasificacionParticularRiesgo] FROM [Riesgos].[vwClasificacionRiesgos]");
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
        public DataTable DataRiesgoOperativo()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdFactorRiesgoOperativo],[NombreFactorRiesgoOperativo],[IdTipoRiesgoOperativo] as 'Id Sub Factor Riesgo Operativo',[NombreTipoRiesgoOperativo] as 'Sub Factor Riesgo Operativo'" +
                " FROM [Riesgos].[vwRiesgoOperativo]");
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
        public DataTable DataTipoEvento()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoEventoOperativo, NombreTipoEventoOperativo " +
                " FROM Parametrizacion.TipoEventoOperativo");
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
        public DataTable DataRiesgoAsociadoOperativo()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdRiesgoAsociadoOperativo, NombreRiesgoAsociadoOperativo " +
                " FROM Parametrizacion.RiesgoAsociadoOperativo");
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
        public DataTable DataRiesgoAsociadoLA()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdRiesgoAsociadoLA, NombreRiesgoAsociadoLA " +
                " FROM Parametrizacion.RiesgoAsociadoLA");
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
        public DataTable DataRiesgoAsociadoLAFT()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdFactorRiesgoLAFT, NombreFactorRiesgoLAFT  " +
                " FROM Parametrizacion.FactorRiesgoLAFT");
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
        public DataTable DataCausas()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdCausas, NombreCausas FROM Parametrizacion.Causas ");
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
        public DataTable DataConsecuencia()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdConsecuencia, NombreConsecuencia FROM Parametrizacion.Consecuencia");
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
        public DataTable DataRiesgos()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdRiesgo],[Codigo],[NombreRiesgo],[Descripcion],[ListaCausas],[ListaConsecuencias]" +
                ",[OcurrenciaEventoDesde],[OcurrenciaEventoHasta],[PerdidaEconomicaDesde],[PerdidaEconomicaHasta],[Nombres],[NombreClasificacionRiesgo],[Responsable],[ListaTratamiento]" +
                " FROM [Riesgos].[vwRiesgos]");
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
        public DataTable DataTratamiento()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTratamiento, NombreTratamiento FROM Parametrizacion.Tratamiento");
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
        public DataTable DataResponsable()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [idHijo] as IdCargoResponsable,[NombreHijo] as CargoResponsable FROM[Parametrizacion].[JerarquiaOrganizacional]");
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
        public DataTable DataImpacto()
        {
            DataTable dtInformacion = new DataTable();

            try
            {


                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdImpacto],[NombreImpacto] FROM[Parametrizacion].[Impacto]");
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
        public DataTable lastCod()
        {
            DataTable dtInformacion = new DataTable();
            //string strCodigoRiesgo = "(SELECT CASE ISNULL(MAX(IdRiesgo),'') WHEN '' THEN 1 ELSE (SELECT MAX(CAST(SUBSTRING(Codigo, 2, 10)AS INT)) + 1 FROM Riesgos.Riesgo WHERE Codigo LIKE 'R%') END FROM Riesgos.Riesgo)";
            ///Ajustes claxon 109855
            string strCodigoRiesgo = "(SELECT CASE ISNULL(MAX(IdRiesgo),'') WHEN '' THEN 1 ELSE (SELECT MAX(IdRiesgo) FROM Riesgos.Riesgo WHERE Codigo LIKE 'R%') END FROM Riesgos.Riesgo)"; 
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(/*strConsultaModificacion + "; " +*/ strCodigoRiesgo);
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
        public DataTable DataRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            string strRiesgo = "select IdRiesgo,Codigo,Nombre,Descripcion FROM Riesgos.Riesgo where Anulado = 0";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(/*strConsultaModificacion + "; " +*/ strRiesgo);
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
        public DataTable DataControles()
        {
            DataTable dtInformacion = new DataTable();
            string strRiesgo = "SELECT [IdControl],[CodigoControl],[NombreControl],[DescripcionControl] FROM [Riesgos].[Control]";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(/*strConsultaModificacion + "; " +*/ strRiesgo);
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
        public DataTable DataCausasRiesgos()
        {
            DataTable dtInformacion = new DataTable();
            string strRiesgo = "SELECT [IdRiesgo],[IdControl],[Idcausas] FROM [Riesgos].[RiesgosCausasvsControles]";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(/*strConsultaModificacion + "; " +*/ strRiesgo);
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
        public DataTable GridRiesgos(string IdRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            //string strRiesgo = "select Codigo,Nombre,Descripcion, IdRiesgo FROM Riesgos.Riesgo where IdRiesgo > " + IdRiesgo;
            ///Ajustes Calxon
            string strRiesgo = "select Codigo,Nombre,Descripcion, IdRiesgo FROM Riesgos.Riesgo where IdRiesgo > " + IdRiesgo;
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(/*strConsultaModificacion + "; " +*/ strRiesgo);
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
        public DataTable GridEventos(string IdEvento)
        {
            DataTable dtInformacion = new DataTable();
            string strRiesgo = "SELECT [CodigoEvento],[DescripcionEvento],[NomGeneradorEvento] FROM [Riesgos].[Eventos] where IdEvento > " + IdEvento;
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(/*strConsultaModificacion + "; " +*/ strRiesgo);
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
        public DataTable GridControles(string IdControl)
        {
            DataTable dtInformacion = new DataTable();
            string strRiesgo = "SELECT [CodigoControl],[NombreControl],[DescripcionControl] FROM[Riesgos].[Control] where IdControl > " + IdControl;
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(/*strConsultaModificacion + "; " +*/ strRiesgo);
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
        public DataTable GridRiesgosvsControl(string IdControlesRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            string strRiesgo = "SELECT [IdControlesRiesgo],[Codigo],[Nombre],[CodigoControl],[NombreControl] FROM [Riesgos].[vwRiesgovsControlMasivo]"
                + " where IdControlesRiesgo > "+ IdControlesRiesgo;
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(/*strConsultaModificacion + "; " +*/ strRiesgo);
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
        public DataTable DataEmpresa()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdEmpresa, Descripcion FROM [Eventos].[Empresa] WHERE Activo = 1";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataServicio()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT [IdServicio],[DescripcionServicio],[IdSubServicio],[DescripcionSubServicio] FROM[Eventos].[vwServicioSubServicio]";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataCanal()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdCanal, Descripcion FROM Eventos.Canal";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataGenerador()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdGenerador, Descripcion FROM Eventos.Generador";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataPeriodicidad()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdPeriodicidad, NombrePeriodicidad FROM Parametrizacion.Periodicidad";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataTest()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdTest, NombreTest FROM Parametrizacion.Test";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataMitigaControl()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdMitiga as 'Id Reduce', NombreMitiga as Reduce FROM Parametrizacion.MitigaControl";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataClaseControl()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdClaseControl, NombreClaseControl FROM Parametrizacion.ClaseControl";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataTipoControl()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdTipoControl, NombreTipoControl FROM Parametrizacion.TipoControl";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataExperiencia()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdResponsableExperiencia, NombreResponsableExperiencia FROM Parametrizacion.ResponsableExperiencia";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataDocumentacion()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdDocumentacion, NombreDocumentacion FROM Parametrizacion.Documentacion";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataGruposTrabajo()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT [IdGrupoTrabajo],[Nombre]  FROM[Riesgos].[GruposTrabajo]";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataVariablesCalificacionesControles()
        {
            DataTable dtInformacion = new DataTable();
            //string strConsulta = "SELECT [IdVariableCalificacionControl],[DescripcionVariable] from  [Parametrizacion].[VariableCalificacionControl]";
            string strConsulta = "SELECT [IdVariable],[DescripcionVariable],[IdCategoria]"+
                    ",[DescripcionCategoria] "+
                    "FROM [Parametrizacion].[vwCategoriasvsVariable]";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataResponsabilidad()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdResponsabilidad, NombreResponsabilidad FROM Parametrizacion.Responsabilidad";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public DataTable DataFrecuencia()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "SELECT IdProbabilidad, NombreProbabilidad, ValorProbabilidad FROM Parametrizacion.Probabilidad";
            try
            {
                lock (thisLock)
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
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
        public void registrarRiesgo(String IdRegion, String IdPais, String IdDepartamento, String IdCiudad, String IdOficinaSucursal,
            String IdCadenaValor, String IdMacroproceso, String IdProceso, String IdSubProceso, String IdActividad,
            String IdClasificacionRiesgo, String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo,
            String IdFactorRiesgoOperativo, String IdTipoRiesgoOperativo, String IdTipoEventoOperativo,
            String IdRiesgoAsociadoOperativo, String ListaRiesgoAsociadoLA, String ListaFactorRiesgoLAFT,
            String Nombre, String Descripcion, String ListaCausas, String ListaConsecuencias, String IdResponsableRiesgo,
            String IdProbabilidad, String OcurrenciaEventoDesde, String OcurrenciaEventoHasta, String IdImpacto, String PerdidaEconomicaDesde,
            String PerdidaEconomicaHasta, String ListaTratamiento, int IdUsuario, string fechaRegistro)
        {
            #region Variables
            string strConsultaInsert = string.Empty, strCamposInsert = string.Empty, strValoresInsert = string.Empty;
            string strCodigoRiesgo = "(SELECT CASE ISNULL(MAX(IdRiesgo),'') WHEN '' THEN 'R1' ELSE (SELECT 'R'+ CAST((SELECT MAX(CAST(SUBSTRING(Codigo, 2, 10)AS INT)) + 1 FROM Riesgos.Riesgo WHERE Codigo LIKE 'R%') AS NVARCHAR(50))) END FROM Riesgos.Riesgo)";
            //strIdRiesgo = "(SELECT CASE ISNULL(MAX(IdRiesgo),'') WHEN '' THEN 1 ELSE (SELECT MAX(CAST(SUBSTRING(Codigo, 2, 10)AS INT)) + 1 FROM Riesgos.Riesgo WHERE Codigo LIKE 'R%') END FROM Riesgos.Riesgo)";
            //string strConsultaModificacion = string.Empty, strCamposModificacion = string.Empty;
            #endregion Variables
            try
            {
                #region Inserts
                /*strCamposModificacion = "([IdCodigoRiesgo],[CodigoRiesgo],[NombreRiesgo],[IdClasificacionRiesgo],[IdClasificacionGeneralRiesgo],[IdClasificacionParticularRiesgo]," +
                    "[IdTipoRiesgoOperativo],[Causas],[Consecuencias],[JustificacionCambio],[IdResponsableRiesgo],[IdUsuario],[FechaRegistroRiesgo],[FechaModificacion], [IdTipoEventoOperativo])";
                strConsultaModificacion = string.Format("INSERT INTO [Riesgos].[DetalleModificacionRiesgo] {0} VALUES ({1},{2},'{3}',{4},{5},{6},{7},'{8}','{9}','{10}',{11},{12},GETDATE(),GETDATE(),{13})", strCamposModificacion,
                    strIdRiesgo, strCodigoRiesgo, Nombre, IdClasificacionRiesgo, IdClasificacionGeneralRiesgo, IdClasificacionParticularRiesgo,
                    IdTipoRiesgoOperativo, ListaCausas, ListaConsecuencias, "CREACION DE RIESGO", IdResponsableRiesgo, IdUsuario, IdTipoEventoOperativo);
                    */
                strCamposInsert = "(IdRegion, IdPais, IdDepartamento, IdCiudad, IdOficinaSucursal, IdCadenaValor, IdMacroproceso, IdProceso, " +
                    "IdSubProceso, IdActividad, IdClasificacionRiesgo, IdClasificacionGeneralRiesgo, IdClasificacionParticularRiesgo, IdFactorRiesgoOperativo, " +
                    "IdTipoRiesgoOperativo, IdTipoEventoOperativo, IdRiesgoAsociadoOperativo, ListaRiesgoAsociadoLA, ListaFactorRiesgoLAFT, " +
                    "Codigo, Nombre, Descripcion, ListaCausas, ListaConsecuencias, IdResponsableRiesgo, IdProbabilidad, IdProbabilidadResidual, OcurrenciaEventoDesde, " +
                    "OcurrenciaEventoHasta, IdImpacto, IdImpactoResidual, PerdidaEconomicaDesde, PerdidaEconomicaHasta, Anulado, FechaRegistro, IdUsuario, ListaTratamiento)";
                strValoresInsert = "";
                strConsultaInsert = string.Format("INSERT INTO Riesgos.Riesgo {0}  VALUES ({2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},N'{19}',N'{20}',{21},N'{22}',N'{23}',N'{24}',N'{25}',{26},{27},{28},N'{29}',N'{30}',{31},{32},N'{33}',N'{34}',0,GETDATE(),{35},N'{36}')",
                    strCamposInsert, strValoresInsert,
                    IdRegion, IdPais, IdDepartamento, IdCiudad, IdOficinaSucursal, IdCadenaValor, IdMacroproceso, IdProceso, IdSubProceso, IdActividad,
                    IdClasificacionRiesgo, IdClasificacionGeneralRiesgo, IdClasificacionParticularRiesgo, IdFactorRiesgoOperativo, IdTipoRiesgoOperativo,
                    IdTipoEventoOperativo, IdRiesgoAsociadoOperativo, ListaRiesgoAsociadoLA, ListaFactorRiesgoLAFT, strCodigoRiesgo, Nombre, Descripcion, ListaCausas,
                    ListaConsecuencias, IdResponsableRiesgo, IdProbabilidad, IdProbabilidad, OcurrenciaEventoDesde, OcurrenciaEventoHasta, IdImpacto, IdImpacto,
                    PerdidaEconomicaDesde, PerdidaEconomicaHasta, IdUsuario, ListaTratamiento);
                #endregion Inserts

                lock (thisLock)
                {
                    cDataBase.conectar();
                    cDataBase.ejecutarQuery(/*strConsultaModificacion + "; " +*/ strConsultaInsert);
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
        public void registrarEvento(String IdEmpresa, String IdRegion, String IdPais,
            String IdDepartamento, String IdCiudad, String IdOficinaSucursal, String DetalleUbicacion,
            String DescripcionEvento, String IdServicio, String IdSubServicio, String FechaInicio,
            String HoraInicio, String FechaFinalizacion, String HoraFinalizacion, String FechaDescubrimiento,
            String HoraDescubrimiento, String IdCanal, String IdGeneraEvento, String GeneraEvento,
            String cuantiaperdida, String FechaEvento, String NomGeneradorEvento, int IdUsuario)
        {
            #region Variables

            DataTable dtInformacion = new DataTable();
            string strCodigoEvento = string.Empty, strPrefixEvento = string.Empty;
            string strConsulta = string.Empty, strCamposInsert = string.Empty, strValoresInsert = string.Empty;
            #endregion Variables
            try
            {
                switch (IdEmpresa.Trim())
                {
                    case "1":
                        strPrefixEvento = "EV";
                        break;
                    case "2":
                        strPrefixEvento = "EG";
                        break;
                    case "3":
                        strPrefixEvento = "EE";
                        break;
                }

                lock (thisLock)
                {
                    strCodigoEvento = string.Format("(CASE WHEN (SELECT MAX(CAST(SUBSTRING(CodigoEvento, 3, 10)AS INT)) + 1 FROM Riesgos.Eventos WHERE CodigoEvento LIKE '{0}%')IS NULL THEN '{0}1' ELSE (SELECT '{0}'+ CAST ((SELECT MAX(CAST(SUBSTRING(CodigoEvento, 3, 10)AS INT)) + 1 FROM Riesgos.Eventos WHERE CodigoEvento LIKE '{0}%') AS NVARCHAR(50))) END )", strPrefixEvento);
                    strCamposInsert = "(CodigoEvento, IdEmpresa, IdRegion, IdPais, IdDepartamento, IdCiudad, IdOficinaSucursal, DetalleUbicacion, DescripcionEvento, IdServicio, IdSubServicio, FechaInicio, HoraInicio, FechaFinalizacion, HoraFinalizacion, FechaDescubrimiento, HoraDescubrimiento, IdCanal, IdGeneraEvento, GeneraEvento, cuantiaperdida, FechaEvento, NomGeneradorEvento, IdUsuario)";
                    strValoresInsert = "(" + strCodigoEvento + ",'" + IdEmpresa + "','" + IdRegion + "','" + IdPais + "','" + IdDepartamento + "','" + IdCiudad + "','" + IdOficinaSucursal + "','" + DetalleUbicacion + "','" + DescripcionEvento + "','" + IdServicio + "','" + IdSubServicio + "','" + FechaInicio + "','" + HoraInicio + "','" + FechaFinalizacion + "','" + HoraFinalizacion + "', '" + FechaDescubrimiento + "','" + HoraDescubrimiento + "','" + IdCanal + "','" + IdGeneraEvento + "','" + GeneraEvento + "','" + cuantiaperdida + "','" + FechaEvento + "','" + NomGeneradorEvento + "','" + IdUsuario + "')";
                    strConsulta = string.Format("INSERT INTO Riesgos.Eventos {0} VALUES {1}", strCamposInsert, strValoresInsert);

                    cDataBase.conectar();
                    cDataBase.ejecutarQuery(strConsulta);
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
 
        public void registrarControl(string NombreControl, string DescripcionControl,
            string ObjetivoControl, string Responsable, string IdPeriodicidad, string IdTest,
            /*string IdClaseControl, string IdTipoControl, string IdResponsableExperiencia,
            string IdDocumentacion, string IdResponsabilidad,*/ string IdCalificacionControl,
            string NombreVariable, string IdCategoria, string NombreCategoria,
            string IdMitiga, int strIdUsuario, string ResponsableEjecucion)
        {
            #region Variables
            string strConsultaModificacion = string.Empty, strCamposModificacion = string.Empty;
            string strConsultaInsertar = string.Empty, strCamposInsertar = string.Empty;
            string /*strIdControl = "(SELECT CASE ISNULL(MAX(IdControl),'') WHEN '' THEN 1 ELSE (SELECT MAX(CAST(SUBSTRING(CodigoControl, 2, 10) AS INT)) + 1 FROM Riesgos.Control WHERE CodigoControl LIKE 'C%') END FROM Riesgos.Control)",*/
                strCodigoControl = "(SELECT CASE ISNULL(MAX(IdControl),'') WHEN '' THEN 'C1' ELSE (SELECT 'C'+ CAST ((SELECT MAX(CAST(SUBSTRING(CodigoControl, 2, 10) AS INT)) + 1 FROM Riesgos.Control WHERE CodigoControl LIKE 'C%') AS NVARCHAR(50))) END FROM Riesgos.Control)";
            #endregion Variables

            try
            {
                #region Inserts
                /*strCamposModificacion = "([IdCodigoControl],[CodigoControl],[NombreControl],[IdResponsableControl],[IdPeriodicidad],[IdTest],[IdClaseControl],[IdTipoControl],[IdResponsableExperiencia],[IdDocumentacion],[IdResponsabilidad],[IdCalificacionControl],[IdMitiga],[JustificacionCambio],[IdUsuario],[FechaRegistroControl],[FechaModificacion])";
                strConsultaModificacion = string.Format("INSERT INTO [Riesgos].[DetalleModificacionControl] {0} VALUES ({1},{2},'{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},'{14}',{15},GETDATE(),GETDATE())",
                    strCamposModificacion, strIdControl, strCodigoControl, NombreControl, Responsable, IdPeriodicidad, IdTest, IdClaseControl, IdTipoControl,
                    IdResponsableExperiencia, IdDocumentacion, IdResponsabilidad, IdCalificacionControl, IdMitiga, "CREACION DEL CONTROL", strIdUsuario);*/

                /*strCamposInsertar = "(CodigoControl, NombreControl, DescripcionControl, ObjetivoControl, Responsable, IdPeriodicidad, IdTest, IdClaseControl, IdTipoControl, IdResponsableExperiencia, IdDocumentacion, IdResponsabilidad, IdCalificacionControl, FechaRegistro, IdUsuario, IdMitiga,ResponsableEjecucion)";
                strConsultaInsertar = string.Format("INSERT INTO Riesgos.Control {0} VALUES ({1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},GETDATE(),{14},{15},'{16}')", strCamposInsertar,
                    strCodigoControl, NombreControl, DescripcionControl, ObjetivoControl, Responsable, IdPeriodicidad,
                    IdTest, IdClaseControl, IdTipoControl, IdResponsableExperiencia, IdDocumentacion, IdResponsabilidad, IdCalificacionControl, strIdUsuario, IdMitiga, ResponsableEjecucion);*/
                strCamposInsertar = "(CodigoControl, NombreControl, DescripcionControl, ObjetivoControl, Responsable, IdPeriodicidad, IdTest, IdClaseControl, IdTipoControl, IdResponsableExperiencia, IdDocumentacion, IdResponsabilidad, IdCalificacionControl, FechaRegistro, IdUsuario, IdMitiga,ResponsableEjecucion)";
                strConsultaInsertar = string.Format("INSERT INTO Riesgos.Control {0} VALUES ({1},'{2}','{3}','{4}',{5},{6},null,null,null,null,null,{7},{8},GETDATE(),{9},{10},'{11}')", strCamposInsertar,
                    strCodigoControl, NombreControl, DescripcionControl, ObjetivoControl, Responsable, IdPeriodicidad,
                    IdTest, /*IdClaseControl, IdTipoControl, IdResponsableExperiencia, IdDocumentacion, IdResponsabilidad,*/ IdCalificacionControl, strIdUsuario, IdMitiga, ResponsableEjecucion);
                #endregion Inserts

                lock (thisLock)
                {
                    cDataBase.conectar();
                    cDataBase.ejecutarQuery(strConsultaInsertar);
                    cDataBase.desconectar();
                }
                DataTable dt = LastCodControl();
                string IdControl = dt.Rows[0][0].ToString();
                #region InsertControlVariable
                strCamposInsertar = "([IdControl],[NombreVariable],[IdCategoria],[NombreCategoria])";
                strConsultaInsertar = string.Format("INSERT INTO [Riesgos].[ControlxVariable] {0} VALUES ({1},'{2}',{3},'{4}')", strCamposInsertar,
                    IdControl, NombreVariable, IdCategoria, NombreCategoria);
                #endregion InsertControlVariable
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
                //throw new Exception(ex.Message);
            }
        }
        public DataTable LastCodEvento()
        {
            DataTable dtInformacion = new DataTable();
            string strCodigoEvento = string.Empty, strConsulta = string.Empty;
            try
            {
                #region Traer Valor Evento
                strConsulta = string.Format("SELECT MAX([IdEvento]) AS LastEvent FROM Riesgos.Eventos");
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();

                //CodigoEvento = strPrefixEvento + dtInformacion.Rows[0]["LastEvent"].ToString().Trim();
                #endregion Traer Valor Evento
            }catch(Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable LastCodRiesgovsControl()
        {
            DataTable dtInformacion = new DataTable();
            string strCodigoEvento = string.Empty, strConsulta = string.Empty;
            try
            {
                #region Traer Valor Evento
                strConsulta = string.Format("SELECT MAX([IdControlesRiesgo]) FROM [Riesgos].[ControlesRiesgo]");
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();

                //CodigoEvento = strPrefixEvento + dtInformacion.Rows[0]["LastEvent"].ToString().Trim();
                #endregion Traer Valor Evento
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable LastCodControl()
        {
            DataTable dtInformacion = new DataTable();
            string strCodigoEvento = string.Empty, strConsulta = string.Empty;
            try
            {
                #region Traer Valor Evento
                strConsulta = string.Format("SELECT MAX(IdControl) FROM Riesgos.Control ");
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();

                //CodigoEvento = strPrefixEvento + dtInformacion.Rows[0]["LastEvent"].ToString().Trim();
                #endregion Traer Valor Evento
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public bool Guardar(string nombrearchivo, int length, byte[] archivo, ref string strErrMsg, string FechaRegistro, string UrlArchivo, byte[] bExcelData)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars
            //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ListasConnectionString"].ToString()))
            //{
                try
                {
                    strConsulta = string.Format("INSERT INTO [Riesgos].[RiesgosPlantillaMasiva] ([FechaRegistro],[UrlArchivo],[ArchivoExcel]) VALUES (GETDATE(), '{1}', @PdfData)",
                        FechaRegistro, UrlArchivo);

                    cDataBase.mtdConectarSql();
                    cDataBase.mtdEjecutarConsultaSQL(strConsulta, bExcelData);
                    cDataBase.mtdDesconectarSql();
                    

                    booResult = true;
                }
                catch (Exception ex)
                {
                    strErrMsg = string.Format("Error al Cargar el Archivo. [{0}]", ex.Message);
                }
                return booResult;
            //}

        }
        public byte[] mtdDescargarPlantillaRiesgos()
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
        public byte[] mtdDescargarPlantillaControles()
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo],[ArchivoExcel] FROM [Riesgos].[RiesgosPlantillaMasiva] where [IdArchivos] = 9");

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
        public byte[] mtdDescargarPlantillaEventos()
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo],[ArchivoExcel] FROM [Riesgos].[RiesgosPlantillaMasiva] where [IdArchivos] = 6");

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
        public byte[] mtdDescargarPlantillaRisgovsControles()
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo],[ArchivoExcel] FROM [Riesgos].[RiesgosPlantillaMasiva] where [IdArchivos] = 4");

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

        public DataTable DescargarPlantillaCargaControles()
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlSeleccionarPlantilla]", parametros);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SeleccionarParametrosVariable()
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlSeleccionarParametrosVariablePlantilla]", parametros);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CalcularEficacia(cControlEntity controlEntity, string ultimoControl)
        {
            try
            {
                string strErrMsg = string.Empty;
                double valorCalificacion = 0;
                double porcentajeVariable = 0;
                int calificacionCategoria = 0;
                int calificacionControl = 0;
                double totalCalificacion = 0;
                int tryParse = 0;
                cControl control = new cControl();
                clsBLLPorcentajeCalificacion cPorcentaje = new clsBLLPorcentajeCalificacion();
                List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
                clsDTOCategoriasVariableControl categoriasVariableControl = new clsDTOCategoriasVariableControl();
                List<clsDTOCategoriasVariableControl> listCategorias = new List<clsDTOCategoriasVariableControl>();
                lstVariables = control.SeleccionarVariables();

                // Vector donde se guardan las calificaciones de las categorias
                int?[] categorias = new int?[15];
                categorias[0] = controlEntity.IdClaseControl;
                categorias[1] = controlEntity.IdTipoControl;
                categorias[2] = controlEntity.IdResponsableExperiencia;
                categorias[3] = controlEntity.IdDocumentacion;
                categorias[4] = controlEntity.IdResponsabilidad;
                categorias[5] = controlEntity.Variable6;
                categorias[6] = controlEntity.Variable7;
                categorias[7] = controlEntity.Variable8;
                categorias[8] = controlEntity.Variable9;
                categorias[9] = controlEntity.Variable10;
                categorias[10] = controlEntity.Variable11;
                categorias[11] = controlEntity.Variable12;
                categorias[12] = controlEntity.Variable13;
                categorias[13] = controlEntity.Variable14;
                categorias[14] = controlEntity.Variable15;

                for (int i = 0; i < categorias.Length; i++)
                {
                    if (int.TryParse(categorias[i].ToString(), out tryParse))
                        categoriasVariableControl = control.SeleccionarCategorias(categorias[i]);
                    else
                        categoriasVariableControl = null;

                    if (categoriasVariableControl != null)
                    {
                        calificacionCategoria = categoriasVariableControl.intPesoCategoria;

                        // Se encuentra el peso de la variable de acuerdo a la categoria
                        var item = lstVariables.FirstOrDefault(x => x.intIdCalificacionControl == categoriasVariableControl.IdVariable);

                        // Si la variable tiene un peso se calcula la calificación
                        if (item != null)
                        {
                            // Cálculo de la calificación
                            porcentajeVariable = Convert.ToInt32(item.FlPesoVariable);
                            valorCalificacion = valorCalificacion + (porcentajeVariable * calificacionCategoria);
                            valorCalificacion = (valorCalificacion / 100);
                            totalCalificacion = totalCalificacion + valorCalificacion;
                        }
                    }
                }

                // Ubica la calificación en la tabla de límites
                List<clsDTOCalificacionControl> lstCalificacion = new List<clsDTOCalificacionControl>();
                lstCalificacion = cPorcentaje.mtdConsultarCalificacionControl(ref lstCalificacion, ref strErrMsg);
                if (lstCalificacion != null)
                {
                    foreach (clsDTOCalificacionControl objCalificacion in lstCalificacion)
                    {
                        if (totalCalificacion >= objCalificacion.intLimiteInferior && totalCalificacion <= objCalificacion.intLimiteSuperior)
                        {
                            calificacionControl = objCalificacion.intIdCalificacionControl;
                            break;
                        }
                    }
                }
                cError.errorMessage("[Carga Masiva] Total calificación: " + totalCalificacion + " Control > " + ultimoControl);
                return calificacionControl;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}