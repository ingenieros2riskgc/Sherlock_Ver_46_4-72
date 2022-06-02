using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cParametrizacionRiesgos : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;

        #region Loads
        public DataTable loadDDLClasificacionGeneralRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdClasificacionGeneralRiesgo, NombreClasificacionGeneralRiesgo FROM Parametrizacion.ClasificacionGeneralRiesgo");
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

        public DataTable loadInfoTipoPerdida()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoPerdidaEvento, NombreTipoPerdidaEvento FROM Parametrizacion.TipoPerdidaEvento");
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

        public DataTable loadInfoClaseEvento()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdClaseEvento, NombreClaseEvento FROM Parametrizacion.ClaseEvento");
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

        public DataTable loadInfoPeriodicidad()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdPeriodicidad, NombrePeriodicidad FROM Parametrizacion.Periodicidad");
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

        public DataTable loadInfoGruposTrabajoParam()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT a.IdGruposTrabajoParam, a.Nombre,a.Correo, case a.Estado when 1 then 'Activo' else 'Inactivo' end 'Estado', convert(varchar, a.FechaRegistro,120) FechaRegistro, rtrim(b.Usuario) Usuario FROM Riesgos.GruposTrabajoParam a left join Listas.Usuarios b on a.IdUsuario = b.IdUsuario");
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

        public DataTable loadInfoGruposTrabajo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT a.IdGrupoTrabajo, a.Nombre, case a.Estado when 1 then 'Activo' else 'Inactivo' end 'Estado', convert(varchar, a.FechaRegistro,120) FechaRegistro, rtrim(b.Usuario) Usuario FROM Riesgos.GruposTrabajo a left join Listas.Usuarios b on a.IdUsuario = b.IdUsuario");
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

        public DataTable loadInfoGruposTrabajoRecursos(string IdGrupoTrabajo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT a.IdGrupoTrabajo, a.IdGruposTrabajoRescurso, a.Nombre, a.Correo, case a.Estado when 1 then 'Activo' else 'Inactivo' end 'Estado', convert(varchar, a.FechaRegistro,120) FechaRegistro, rtrim(b.Usuario) Usuario FROM Riesgos.GruposTrabajoRescurso a left join Listas.Usuarios b on a.IdUsuario = b.IdUsuario WHERE a.IdGrupoTrabajo = '" + IdGrupoTrabajo + "'");
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

        public DataTable loadInfoPorcentajeCalificacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl ORDER BY IdPorcentajeCalificarControl");
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

        public DataTable loadInfoTipoLegislacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoLegislacion, NombreTipoLegislacion FROM Riesgos.TipoLegislacion");
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

        public DataTable loadInfoEstadoPlanEvaluacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdEstadoPlanEvaluacion, NombreEstadoPlanEvaluacion FROM Parametrizacion.EstadoPlanEvaluacion");
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

        public DataTable loadInfoTipoPruebaPlanEvaluacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoPruebaPlanEvaluacion, NombreTipoPruebaPlanEvaluacion FROM Parametrizacion.TipoPruebaPlanEvaluacion");
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

        public DataTable loadInfoEstadoPlanAccion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdEstadoPlanAccion, NombreEstadoPlanAccion FROM Parametrizacion.EstadoPlanAccion");
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

        public DataTable loadInfoTipoRecursoPlanAccion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoRecursoPlanAccion, NombreTipoRecursoPlanAccion FROM Parametrizacion.TipoRecursoPlanAccion");
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

        public DataTable loadInfoFactorRiesgoLAFT()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdFactorRiesgoLAFT, NombreFactorRiesgoLAFT FROM Parametrizacion.FactorRiesgoLAFT");
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

        public DataTable loadInfoFactorRiesgoOperativo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdFactorRiesgoOperativo, NombreFactorRiesgoOperativo FROM Parametrizacion.FactorRiesgoOperativo");
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

        public DataTable loadInfoSubFactorRiesgoOperativo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.TipoRiesgoOperativo.IdTipoRiesgoOperativo, Parametrizacion.FactorRiesgoOperativo.IdFactorRiesgoOperativo, Parametrizacion.FactorRiesgoOperativo.NombreFactorRiesgoOperativo, Parametrizacion.TipoRiesgoOperativo.NombreTipoRiesgoOperativo FROM Parametrizacion.TipoRiesgoOperativo INNER JOIN Parametrizacion.FactorRiesgoOperativo ON Parametrizacion.TipoRiesgoOperativo.IdFactorRiesgoOperativo = Parametrizacion.FactorRiesgoOperativo.IdFactorRiesgoOperativo");
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

        public DataTable loadInfoClasificacionRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdClasificacionRiesgo, NombreClasificacionRiesgo FROM Parametrizacion.ClasificacionRiesgo");
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

        public DataTable loadInfoClasificacionGeneralRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo, Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo FROM Parametrizacion.ClasificacionGeneralRiesgo INNER JOIN Parametrizacion.ClasificacionRiesgo ON Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo");
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

        public DataTable loadInfoClasificacionParticularRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionParticularRiesgo, Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo, Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo, Parametrizacion.ClasificacionParticularRiesgo.NombreClasificacionParticularRiesgo FROM Parametrizacion.ClasificacionGeneralRiesgo INNER JOIN Parametrizacion.ClasificacionParticularRiesgo ON Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo = Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionGeneralRiesgo");
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

        public DataTable loadInfoDesviaciones()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT DesviacionProbabilidad, DesviacionImpacto FROM Parametrizacion.CalificacionControl ORDER BY IdCalificacionControl");
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

        public DataTable loadInfoEstadoLegislacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdEstadoLegislacion, NombreEstadoLegislacion FROM Parametrizacion.EstadoLegislacion");
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

        public DataTable loadInfoProbabilidad()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT NombreProbabilidad FROM Parametrizacion.Probabilidad ORDER BY IdProbabilidad");
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

        public DataTable loadInfoImpacto()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT NombreImpacto FROM Parametrizacion.Impacto ORDER BY IdImpacto");
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

        public DataTable loadInfoTratamiento()
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

        public DataTable loadInfoTipoEventoOperativo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoEventoOperativo, NombreTipoEventoOperativo FROM Parametrizacion.TipoEventoOperativo");
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

        public DataTable loadInfoRiesgoAsociadoOperativo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdRiesgoAsociadoOperativo, NombreRiesgoAsociadoOperativo FROM Parametrizacion.RiesgoAsociadoOperativo");
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

        public DataTable loadInfoRiesgoAsociadoLA()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdRiesgoAsociadoLA, NombreRiesgoAsociadoLA FROM Parametrizacion.RiesgoAsociadoLA");
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
        #endregion

        public void borrarRegistroTipoPerdida(String IdTipoPerdidaEvento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.TipoPerdidaEvento WHERE (IdTipoPerdidaEvento = " + IdTipoPerdidaEvento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroTipoPerdida(String NombreTipoPerdidaEvento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.TipoPerdidaEvento (NombreTipoPerdidaEvento) VALUES ('" + NombreTipoPerdidaEvento + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroTipoPerdida(String NombreTipoPerdidaEvento, String IdTipoPerdidaEvento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.TipoPerdidaEvento SET NombreTipoPerdidaEvento = '" + NombreTipoPerdidaEvento + "' WHERE (IdTipoPerdidaEvento = " + IdTipoPerdidaEvento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroClaseEvento(String IdClaseEvento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.ClaseEvento WHERE (IdClaseEvento = " + IdClaseEvento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroClaseEvento(String NombreClaseEvento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.ClaseEvento (NombreClaseEvento) VALUES ('" + NombreClaseEvento + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroClaseEvento(String NombreClaseEvento, String IdClaseEvento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.ClaseEvento SET NombreClaseEvento = '" + NombreClaseEvento + "' WHERE (IdClaseEvento = " + IdClaseEvento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroPeriodicidad(String IdPeriodicidad)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.Periodicidad WHERE (IdPeriodicidad = " + IdPeriodicidad + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroGrupoTrabajo(string IdGrupoTrabajo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.GruposTrabajo WHERE IdGrupoTrabajo = '" + IdGrupoTrabajo + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroGrupoTrabajoParam(string GruposTrabajoParam)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.GruposTrabajoParam WHERE IdGruposTrabajoParam = '" + GruposTrabajoParam + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroGrupoTrabajoRecurso(string IdGrupoTrabajoRecurso)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.GruposTrabajoRescurso WHERE IdGruposTrabajoRescurso = '" + IdGrupoTrabajoRecurso + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroPeriodicidad(String NombrePeriodicidad)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.Periodicidad (NombrePeriodicidad) VALUES ('" + NombrePeriodicidad + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public string loadCorreoJerarquiaOrg(string IdHijo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT CorreoResponsable FROM Parametrizacion.DetalleJerarquiaOrg WHERE idHijo = '" + IdHijo + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0]["CorreoResponsable"].ToString().Trim();
        }

        public string loadCorreoTablaParam(string IdGruposTrabajoParam)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Correo FROM Riesgos.GruposTrabajoParam where IdGruposTrabajoParam = '" + IdGruposTrabajoParam + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0]["Correo"].ToString().Trim();
        }

        public void agregarRegistroGruposTrabajo(string Nombre,string Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.GruposTrabajo (Nombre,Estado,FechaRegistro,IdUsuario) VALUES ('" + Nombre + "','" + Estado + "',getdate()," + IdUsuario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroGruposTrabajoRecurso(string IdGrupoTrabajo, string Nombre, string Correo, string Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.GruposTrabajoRescurso (IdGrupoTrabajo,Nombre,Correo,Estado,FechaRegistro,IdUsuario) VALUES ('" + IdGrupoTrabajo + "','" + Nombre + "','" + Correo + "','" + Estado + "',getdate(),'" + IdUsuario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroGruposTrabajoParam(string Nombre, string Correo, string Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.GruposTrabajoParam (Nombre,Correo,Estado,FechaRegistro,IdUsuario ) VALUES ('" + Nombre + "','" + Correo + "','" + Estado + "',GETDATE(),'" + IdUsuario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroPeriodicidad(String NombrePeriodicidad, String IdPeriodicidad)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Periodicidad SET NombrePeriodicidad = '" + NombrePeriodicidad + "' WHERE (IdPeriodicidad = " + IdPeriodicidad + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroGruposTrabajo(string IdGrupoTrabajo, string Nombre, string Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.GruposTrabajo SET Nombre = '" + Nombre + "', Estado = '" + Estado + "', FechaRegistro = GETDATE(), IdUsuario = '" + IdUsuario + "' WHERE IdGrupoTrabajo = '" + IdGrupoTrabajo + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroGruposTrabajoParam(string IdGruposTrabajoParam, string Nombre, string Correo, string Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.GruposTrabajoParam SET Nombre = '" + Nombre + "', Correo = '" + Correo + "', Estado = '" + Estado + "', IdUsuario = '" + IdUsuario + "', FechaRegistro = GETDATE() WHERE IdGruposTrabajoParam = '" + IdGruposTrabajoParam + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroGruposTrabajoRecurso(string IdGruposTrabajoRescurso, string Nombre, string Correo, string Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.GruposTrabajoRescurso SET Nombre = '" + Nombre + "',Correo  = '" + Correo + "', Estado  = '" + Estado + "' WHERE IdGruposTrabajoRescurso = '" + IdGruposTrabajoRescurso + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroPorcentajeCalificacion(String ValorPorcentajeCalificarControl1, 
            String ValorPorcentajeCalificarControl2, String ValorPorcentajeCalificarControl3, String ValorPorcentajeCalificarControl4, 
            String ValorPorcentajeCalificarControl5)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.PorcentajeCalificarControl SET ValorPorcentajeCalificarControl = " + ValorPorcentajeCalificarControl1 + " WHERE (IdPorcentajeCalificarControl = 1)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.PorcentajeCalificarControl SET ValorPorcentajeCalificarControl = " + ValorPorcentajeCalificarControl2 + " WHERE (IdPorcentajeCalificarControl = 2)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.PorcentajeCalificarControl SET ValorPorcentajeCalificarControl = " + ValorPorcentajeCalificarControl3 + " WHERE (IdPorcentajeCalificarControl = 3)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.PorcentajeCalificarControl SET ValorPorcentajeCalificarControl = " + ValorPorcentajeCalificarControl4 + " WHERE (IdPorcentajeCalificarControl = 4)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.PorcentajeCalificarControl SET ValorPorcentajeCalificarControl = " + ValorPorcentajeCalificarControl5 + " WHERE (IdPorcentajeCalificarControl = 5)");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroTipoLegislacion(String IdTipoLegislacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.TipoLegislacion WHERE (IdTipoLegislacion = " + IdTipoLegislacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroTipoLegislacion(String NombreTipoLegislacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.TipoLegislacion (NombreTipoLegislacion) VALUES (N'" + NombreTipoLegislacion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroTipoLegislacion(String NombreTipoLegislacion, String IdTipoLegislacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.TipoLegislacion SET NombreTipoLegislacion = N'" + NombreTipoLegislacion + "' WHERE (IdTipoLegislacion = " + IdTipoLegislacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroEstadoPlanEvaluacion(String IdEstadoPlanEvaluacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.EstadoPlanEvaluacion WHERE (IdEstadoPlanEvaluacion = " + IdEstadoPlanEvaluacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroEstadoPlanEvaluacion(String NombreEstadoPlanEvaluacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.EstadoPlanEvaluacion (NombreEstadoPlanEvaluacion) VALUES ('" + NombreEstadoPlanEvaluacion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroEstadoPlanEvaluacion(String NombreEstadoPlanEvaluacion, String IdEstadoPlanEvaluacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.EstadoPlanEvaluacion SET NombreEstadoPlanEvaluacion = '" + NombreEstadoPlanEvaluacion + "' WHERE (IdEstadoPlanEvaluacion = " + IdEstadoPlanEvaluacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroTipoPruebaPlanEvaluacion(String IdTipoPruebaPlanEvaluacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.TipoPruebaPlanEvaluacion WHERE (IdTipoPruebaPlanEvaluacion = " + IdTipoPruebaPlanEvaluacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroTipoPruebaPlanEvaluacion(String NombreTipoPruebaPlanEvaluacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.TipoPruebaPlanEvaluacion (NombreTipoPruebaPlanEvaluacion) VALUES ('" + NombreTipoPruebaPlanEvaluacion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroTipoPruebaPlanEvaluacion(String NombreTipoPruebaPlanEvaluacion, String IdTipoPruebaPlanEvaluacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.TipoPruebaPlanEvaluacion SET NombreTipoPruebaPlanEvaluacion = '" + NombreTipoPruebaPlanEvaluacion + "' WHERE (IdTipoPruebaPlanEvaluacion = " + IdTipoPruebaPlanEvaluacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroEstadoPlanAccion(String IdEstadoPlanAccion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.EstadoPlanAccion WHERE (IdEstadoPlanAccion = " + IdEstadoPlanAccion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroEstadoPlanAccion(String NombreEstadoPlanAccion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.EstadoPlanAccion (NombreEstadoPlanAccion) VALUES ('" + NombreEstadoPlanAccion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroEstadoPlanAccion(String NombreEstadoPlanAccion, String IdEstadoPlanAccion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.EstadoPlanAccion SET NombreEstadoPlanAccion = '" + NombreEstadoPlanAccion + "' WHERE (IdEstadoPlanAccion = " + IdEstadoPlanAccion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroTipoRecursoPlanAccion(String IdTipoRecursoPlanAccion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.TipoRecursoPlanAccion WHERE (IdTipoRecursoPlanAccion = " + IdTipoRecursoPlanAccion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroTipoRecursoPlanAccion(String NombreTipoRecursoPlanAccion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.TipoRecursoPlanAccion (NombreTipoRecursoPlanAccion) VALUES ('" + NombreTipoRecursoPlanAccion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroTipoRecursoPlanAccion(String NombreTipoRecursoPlanAccion, String IdTipoRecursoPlanAccion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.TipoRecursoPlanAccion SET NombreTipoRecursoPlanAccion = '" + NombreTipoRecursoPlanAccion + "' WHERE (IdTipoRecursoPlanAccion = " + IdTipoRecursoPlanAccion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        #region Causas
        public DataTable loadInfoCausas()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdCausas, NombreCausas FROM Parametrizacion.Causas ORDER BY NombreCausas");
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

        public void borrarRegistroCausas(String IdCausas)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.Causas WHERE (IdCausas = " + IdCausas + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroCausas(String NombreCausas)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.Causas (NombreCausas) VALUES ('" + NombreCausas + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroCausas(String NombreCausas, String IdCausas)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Causas SET NombreCausas = '" + NombreCausas + "' WHERE (IdCausas = " + IdCausas + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable mtdInfoCausas()
        {
            string strConsulta = "SELECT IdCausas, NombreCausas FROM Parametrizacion.Causas ORDER BY NombreCausas";
            DataTable dtInformacion = new DataTable();

            try
            {
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

        public bool mtdExisteCausa(string strNombreCausa)
        {
            bool booExiste = false;
            string strConsulta = string.Format("SELECT Count(IdCausas) Cantidad FROM Parametrizacion.Causas WHERE NombreCausas LIKE '{0}'",
                strNombreCausa);
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);                
                
                if (Convert.ToInt32(dtInformacion.Rows[0]["Cantidad"].ToString()) > 0)
                    booExiste = true;
            }
            catch (Exception ex)
            {                
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booExiste;
        }
        #endregion

        #region Consecuencia
        public DataTable loadInfoConsecuencia()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdConsecuencia, NombreConsecuencia FROM Parametrizacion.Consecuencia ORDER BY NombreConsecuencia");
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

        public void borrarRegistroConsecuencia(String IdConsecuencia)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.Consecuencia WHERE (IdConsecuencia = " + IdConsecuencia + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroConsecuencia(String NombreConsecuencia)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.Consecuencia (NombreConsecuencia) VALUES ('" + NombreConsecuencia + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroConsecuencia(String NombreConsecuencia, String IdConsecuencia)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Consecuencia SET NombreConsecuencia = '" + NombreConsecuencia + "' WHERE (IdConsecuencia = " + IdConsecuencia + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable mtdInfoConsecuencia()
        {
            string strConsulta = "SELECT IdConsecuencia, NombreConsecuencia FROM Parametrizacion.Consecuencia ORDER BY NombreConsecuencia";
            DataTable dtInformacion = new DataTable();

            try
            {
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

        public bool mtdExisteConsecuencia(string strNombreConsecuencia)
        {
            bool booExiste = false;
            string strConsulta = string.Format("SELECT Count(IdConsecuencia) Cantidad FROM Parametrizacion.Consecuencia WHERE NombreConsecuencia LIKE '{0}'",
                strNombreConsecuencia);
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);

                if (Convert.ToInt32(dtInformacion.Rows[0]["Cantidad"].ToString()) > 0)
                    booExiste = true;
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booExiste;
        }

        #endregion

        public void borrarRegistroTratamiento(String IdTratamiento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.Tratamiento WHERE (IdTratamiento = " + IdTratamiento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroTratamiento(String NombreTratamiento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.Tratamiento (NombreTratamiento) VALUES ('" + NombreTratamiento + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroTratamiento(String NombreTratamiento, String IdTratamiento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Tratamiento SET NombreTratamiento = '" + NombreTratamiento + "' WHERE (IdTratamiento = " + IdTratamiento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroTipoEventoOperativo(String IdTipoEventoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.TipoEventoOperativo WHERE (IdTipoEventoOperativo = " + IdTipoEventoOperativo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroTipoEventoOperativo(String NombreTipoEventoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.TipoEventoOperativo (NombreTipoEventoOperativo) VALUES ('" + NombreTipoEventoOperativo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroTipoEventoOperativo(String NombreTipoEventoOperativo, String IdTipoEventoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.TipoEventoOperativo SET NombreTipoEventoOperativo = '" + NombreTipoEventoOperativo + "' WHERE (IdTipoEventoOperativo = " + IdTipoEventoOperativo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroRiesgoAsociadoOperativo(String IdRiesgoAsociadoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.RiesgoAsociadoOperativo WHERE (IdRiesgoAsociadoOperativo = " + IdRiesgoAsociadoOperativo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroRiesgoAsociadoOperativo(String NombreRiesgoAsociadoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.RiesgoAsociadoOperativo (NombreRiesgoAsociadoOperativo) VALUES ('" + NombreRiesgoAsociadoOperativo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroRiesgoAsociadoOperativo(String NombreRiesgoAsociadoOperativo, String IdRiesgoAsociadoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.RiesgoAsociadoOperativo SET NombreRiesgoAsociadoOperativo = '" + NombreRiesgoAsociadoOperativo + "' WHERE (IdRiesgoAsociadoOperativo = " + IdRiesgoAsociadoOperativo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroRiesgoAsociadoLA(String IdRiesgoAsociadoLA)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.RiesgoAsociadoLA WHERE (IdRiesgoAsociadoLA = " + IdRiesgoAsociadoLA + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroRiesgoAsociadoLA(String NombreRiesgoAsociadoLA)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.RiesgoAsociadoLA (NombreRiesgoAsociadoLA) VALUES ('" + NombreRiesgoAsociadoLA + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroRiesgoAsociadoLA(String NombreRiesgoAsociadoLA, String IdRiesgoAsociadoLA)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.RiesgoAsociadoLA SET NombreRiesgoAsociadoLA = '" + NombreRiesgoAsociadoLA + "' WHERE (IdRiesgoAsociadoLA = " + IdRiesgoAsociadoLA + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroFactorRiesgoLAFT(String IdFactorRiesgoLAFT)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.FactorRiesgoLAFT WHERE (IdFactorRiesgoLAFT = " + IdFactorRiesgoLAFT + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroFactorRiesgoLAFT(String NombreFactorRiesgoLAFT)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.FactorRiesgoLAFT (NombreFactorRiesgoLAFT) VALUES ('" + NombreFactorRiesgoLAFT + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroFactorRiesgoLAFT(String NombreFactorRiesgoLAFT, String IdFactorRiesgoLAFT)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.FactorRiesgoLAFT SET NombreFactorRiesgoLAFT = '" + NombreFactorRiesgoLAFT + "' WHERE (IdFactorRiesgoLAFT = " + IdFactorRiesgoLAFT + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroFactorRiesgoOperativo(String IdFactorRiesgoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.FactorRiesgoOperativo WHERE (IdFactorRiesgoOperativo = " + IdFactorRiesgoOperativo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroFactorRiesgoOperativo(String NombreFactorRiesgoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.FactorRiesgoOperativo (NombreFactorRiesgoOperativo) VALUES ('" + NombreFactorRiesgoOperativo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroFactorRiesgoOperativo(String NombreFactorRiesgoOperativo, String IdFactorRiesgoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.FactorRiesgoOperativo SET NombreFactorRiesgoOperativo = '" + NombreFactorRiesgoOperativo + "' WHERE (IdFactorRiesgoOperativo = " + IdFactorRiesgoOperativo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroSubFactorRiesgoOperativo(String IdTipoRiesgoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.TipoRiesgoOperativo WHERE (IdTipoRiesgoOperativo = " + IdTipoRiesgoOperativo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroSubFactorRiesgoOperativo(String IdFactorRiesgoOperativo, String NombreTipoRiesgoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.TipoRiesgoOperativo (IdFactorRiesgoOperativo, NombreTipoRiesgoOperativo) VALUES (" + IdFactorRiesgoOperativo + ", '" + NombreTipoRiesgoOperativo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroSubFactorRiesgoOperativo(String IdFactorRiesgoOperativo, String NombreTipoRiesgoOperativo, String IdTipoRiesgoOperativo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.TipoRiesgoOperativo SET IdFactorRiesgoOperativo = " + IdFactorRiesgoOperativo + ", NombreTipoRiesgoOperativo = '" + NombreTipoRiesgoOperativo + "' WHERE (IdTipoRiesgoOperativo = " + IdTipoRiesgoOperativo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroClasificacionRiesgo(String IdClasificacionRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.ClasificacionRiesgo WHERE (IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroClasificacionRiesgo(String NombreClasificacionRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.ClasificacionRiesgo (NombreClasificacionRiesgo, IdUsuario, FechaRegistro) VALUES ('" + NombreClasificacionRiesgo + "', " + IdUsuario + ", GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroClasificacionRiesgo(String NombreClasificacionRiesgo, String IdClasificacionRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.ClasificacionRiesgo SET NombreClasificacionRiesgo = '" + NombreClasificacionRiesgo + "', IdUsuario = " + IdUsuario + ", FechaRegistro = GETDATE() WHERE (IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroClasificacionGeneralRiesgo(String IdClasificacionGeneralRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.ClasificacionGeneralRiesgo WHERE (IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroClasificacionGeneralRiesgo(String IdClasificacionRiesgo, String NombreClasificacionGeneralRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.ClasificacionGeneralRiesgo (IdClasificacionRiesgo, NombreClasificacionGeneralRiesgo, IdUsuario, FechaRegistro) VALUES (" + IdClasificacionRiesgo + ", '" + NombreClasificacionGeneralRiesgo + "', " + IdUsuario + ", GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroClasificacionGeneralRiesgo(String IdClasificacionRiesgo, String NombreClasificacionGeneralRiesgo, String IdClasificacionGeneralRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.ClasificacionGeneralRiesgo SET IdClasificacionRiesgo = " + IdClasificacionRiesgo + ", NombreClasificacionGeneralRiesgo = '" + NombreClasificacionGeneralRiesgo + "', IdUsuario = " + IdUsuario + ", FechaRegistro = GETDATE() WHERE (IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroClasificacionParticularRiesgo(String IdClasificacionParticularRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.ClasificacionParticularRiesgo WHERE (IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroClasificacionParticularRiesgo(String IdClasificacionGeneralRiesgo, String NombreClasificacionParticularRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.ClasificacionParticularRiesgo (IdClasificacionGeneralRiesgo, NombreClasificacionParticularRiesgo, IdUsuario, FechaRegistro) VALUES (" + IdClasificacionGeneralRiesgo + ", '" + NombreClasificacionParticularRiesgo + "', " + IdUsuario + ", GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroClasificacionParticularRiesgo(String IdClasificacionGeneralRiesgo, String NombreClasificacionParticularRiesgo, String IdClasificacionParticularRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.ClasificacionParticularRiesgo SET IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ", NombreClasificacionParticularRiesgo = '" + NombreClasificacionParticularRiesgo + "', IdUsuario = " + IdUsuario + ", FechaRegistro = GETDATE() WHERE (IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroDesviaciones(String DesviacionProbabilidad1, String DesviacionImpacto1, String DesviacionProbabilidad2, String DesviacionImpacto2, String DesviacionProbabilidad3, String DesviacionImpacto3, String DesviacionProbabilidad4, String DesviacionImpacto4)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.CalificacionControl SET DesviacionProbabilidad = " + DesviacionProbabilidad1 + ", DesviacionImpacto = " + DesviacionImpacto1 + " WHERE (IdCalificacionControl = 1)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.CalificacionControl SET DesviacionProbabilidad = " + DesviacionProbabilidad2 + ", DesviacionImpacto = " + DesviacionImpacto2 + " WHERE (IdCalificacionControl = 2)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.CalificacionControl SET DesviacionProbabilidad = " + DesviacionProbabilidad3 + ", DesviacionImpacto = " + DesviacionImpacto3 + " WHERE (IdCalificacionControl = 3)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.CalificacionControl SET DesviacionProbabilidad = " + DesviacionProbabilidad4 + ", DesviacionImpacto = " + DesviacionImpacto4 + " WHERE (IdCalificacionControl = 4)");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarRegistroEstadoLegislacion(String IdEstadoLegislacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.EstadoLegislacion WHERE (IdEstadoLegislacion = " + IdEstadoLegislacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarRegistroEstadoLegislacion(String NombreEstadoLegislacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.EstadoLegislacion (NombreEstadoLegislacion) VALUES ('" + NombreEstadoLegislacion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroEstadoLegislacion(String NombreEstadoLegislacion, String IdEstadoLegislacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.EstadoLegislacion SET NombreEstadoLegislacion = '" + NombreEstadoLegislacion + "' WHERE (IdEstadoLegislacion = " + IdEstadoLegislacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroProbabilidad(String NombreProbabilidad1, String NombreProbabilidad2, String NombreProbabilidad3, String NombreProbabilidad4, String NombreProbabilidad5)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Probabilidad SET NombreProbabilidad = '" + NombreProbabilidad1 + "' WHERE (IdProbabilidad = 1)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Probabilidad SET NombreProbabilidad = '" + NombreProbabilidad2 + "' WHERE (IdProbabilidad = 2)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Probabilidad SET NombreProbabilidad = '" + NombreProbabilidad3 + "' WHERE (IdProbabilidad = 3)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Probabilidad SET NombreProbabilidad = '" + NombreProbabilidad4 + "' WHERE (IdProbabilidad = 4)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Probabilidad SET NombreProbabilidad = '" + NombreProbabilidad5 + "' WHERE (IdProbabilidad = 5)");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRegistroImpacto(String NombreImpacto1, String NombreImpacto2, String NombreImpacto3, String NombreImpacto4, String NombreImpacto5)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Impacto SET NombreImpacto = '" + NombreImpacto1 + "' WHERE (IdImpacto = 1)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Impacto SET NombreImpacto = '" + NombreImpacto2 + "' WHERE (IdImpacto = 2)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Impacto SET NombreImpacto = '" + NombreImpacto3 + "' WHERE (IdImpacto = 3)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Impacto SET NombreImpacto = '" + NombreImpacto4 + "' WHERE (IdImpacto = 4)");
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Impacto SET NombreImpacto = '" + NombreImpacto5 + "' WHERE (IdImpacto = 5)");
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