using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace ListasSarlaft.Classes
{
    public class cGestion : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();

        public DataTable VisionEmpresa()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT VE.IdVision,VE.Descripcion,VE.Justificacion,US.Usuario,CONVERT(VARCHAR(10),FechaConsulta,120) AS FechaConsulta FROM Gestion.VisionEmpresa VE, Listas.Usuarios US where VE.IdUsuario=US.IdUsuario and Activo_SN = 'S'");
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

        public DataTable PeriodosCerrados()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select a.IdPeriodo,a.Mes,a.Ano,b.Usuario,CONVERT(VARCHAR(10),a.FechaRegistro,120) as FechaRegistro from Gestion.PeriodoCerrado a, Listas.Usuarios b where a.IdUsuario = b.IdUsuario order by Ano,Mes");
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

        public void agregarPeriodoCerrado(String Mes, String Ano)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.PeriodoCerrado (Mes, Ano, IdUsuario, FechaRegistro) VALUES (" + Mes + ", " + Ano + ", " + IdUsuario.ToString() + ", getdate())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarPeriodoCerrado(string IdPeriodo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.PeriodoCerrado where IdPeriodo = " + IdPeriodo);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarVision(String Descripcion, String Justificacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.VisionEmpresa SET Activo_SN ='N'");
                cDataBase.ejecutarQuery("INSERT INTO Gestion.VisionEmpresa (Descripcion, Justificacion, IdUsuario,FechaConsulta,Activo_SN) VALUES (N'" + Descripcion + "', N'" + Justificacion + "', '" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103),N'S')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable PlanEstrategico()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT PE.IdPlan,PE.CodigoPlan,PE.Nombre,REPLACE(CONVERT(varchar, PE.FechaInicio, 111), '/', '-') AS FechaInicio,REPLACE(CONVERT(varchar, PE.FechaFin, 111), '/', '-') AS FechaFin,US.Usuario,REPLACE(CONVERT(varchar, PE.FechaRegistro, 111), '/', '-') AS FechaRegistro FROM Gestion.PlanEstrategico PE, Listas.Usuarios US WHERE PE.IdUsuario=US.IdUsuario");
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

        public DataTable LoadResponsablesPAI(string IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT distinct g.IdHijo,g.NombreResponsable FROM Gestion.PlanEstrategico a, Gestion.ObjetivosEstrategicos b, Gestion.Estrategias c,Gestion.PlanAccion d, Gestion.Indicadores e,Gestion.IndicadorPlanAccion f,Parametrizacion.DetalleJerarquiaOrg g WHERE a.IdPlan = b.IdPlan and b.IdObjetivo = c.IdObjetivo and c.IdEstrategia = d.IdEstrategia and d.IdPlanAccion = e.IdPlanAccion and e.IdIndicador = f.IdIndicador and f.Responsable = g.IdHijo and a.IdPlan = '" + IdPlan + "' GROUP BY g.IdHijo,g.NombreResponsable ORDER BY g.NombreResponsable");
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

        public DataTable VerVision()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Descripcion  FROM Gestion.VisionEmpresa where Activo_SN = 'S'");
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

        public void modificarPlan(String Nombre, String FechaIni, String FechaFin, String IdPlan)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.PlanEstrategico SET Nombre = N'" + Nombre + "', FechaInicio = CONVERT(datetime,'" + FechaIni + "',120), FechaFin = CONVERT(datetime,'" + FechaFin + " ',120) WHERE (IdPlan = " + IdPlan + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarPlan(String CodigoPlan, String Nombre, String FechaIni, String FechaFin)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.PlanEstrategico (CodigoPlan, Nombre,FechaInicio,FechaFin, IdUsuario,FechaRegistro) VALUES ('" + CodigoPlan + "',N'" + Nombre + "', CONVERT(datetime,'" + FechaIni + "',120),CONVERT(datetime,'" + FechaFin + "',120),'" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarPlan(string IdPlan)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.PlanEstrategico WHERE (IdPlan = " + IdPlan + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable ObjEstrategico(string IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select OBJ.IdObjetivo, OBJ.CodigoObjetivo, OBJ.Descripcion, PER.NombreDetalle, REPLACE(CONVERT(varchar, OBJ.FechaInicio, 111), '/', '-') AS FechaInicio, REPLACE(CONVERT(varchar, OBJ.FechaFin, 111), '/', '-') AS FechaFin, US.Usuario, REPLACE(CONVERT(varchar, OBJ.FechaRegistro, 111), '/', '-') AS FechaRegistro from Gestion.ObjetivosEstrategicos OBJ, Gestion.DetalleTipos PER,Listas.Usuarios US where OBJ.IdDetalleTipo=PER.IdDetalleTipo and OBJ.IdUsuario=US.IdUsuario AND OBJ.IdPlan = " + IdPlan);
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

        public DataTable CausaEfecto(string IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT CE.IdPlan,CE.IdCausaEfecto,CE.IdObjetivoCausa,OC.CodigoObjetivo as CodigoObjetivoC ,OC.Descripcion as DescripcionC,CE.IdObjetivoEfecto,OE.CodigoObjetivo as CodigoObjetivoE,OE.Descripcion as DescripcionE,US.Usuario,CONVERT(VARCHAR(10),CE.FechaRegistro,103) as FechaRegistro FROM Gestion.CausaEfecto CE,Gestion.ObjetivosEstrategicos OC,Gestion.ObjetivosEstrategicos OE,Listas.Usuarios US WHERE CE.IdObjetivoCausa=OC.IdObjetivo AND CE.IdObjetivoEfecto=OE.IdObjetivo AND CE.IdUsuario=US.IdUsuario AND CE.IdPlan= " + IdPlan);
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

        public DataTable PlanAccion(string IdEstrategia)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select PA.IdPlanAccion, PA.CodigoPlanAccion, PA.Descripcion,PA.Responsable as IdResponsable,JO.NombreHijo AS Responsable,PA.Abierto_SN,REPLACE(CONVERT(varchar, PA.FechaInicio, 111), '/', '-') AS FechaInicio,REPLACE(CONVERT(varchar, PA.FechaFin, 111), '/', '-') AS FechaFin, US.Usuario,REPLACE(CONVERT(varchar, PA.FechaRegistro, 111), '/', '-') AS FechaRegistro from Gestion.PlanAccion PA, Listas.Usuarios US, Parametrizacion.JerarquiaOrganizacional JO where PA.IdUsuario=US.IdUsuario AND PA.Responsable=JO.idHijo AND PA.IdEstrategia = " + IdEstrategia);
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

        public DataTable ResponsablePlanAccion(string IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Responsable FROM Gestion.Planaccion WHERE IdPlanAccion = '" + IdPlanAccion + "'");
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

        public DataTable FiltroPlan()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdPlan,CodigoPlan,Nombre,convert(VARCHAR(10),FechaInicio,120)as FechaInicio,convert(VARCHAR(10),FechaFin ,120)as FechaFin FROM Gestion.PlanEstrategico");
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

        public DataTable Perspectivas()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdDetalleTipo,LTRIM(RTRIM(NombreDetalle)) AS NombreDetalle FROM Gestion.DetalleTipos where IdTipo = '1' ORDER BY NombreDetalle");
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

        public DataTable LFormatos()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdDetalleTipo,LTRIM(RTRIM(NombreDetalle)) AS NombreDetalle FROM Gestion.DetalleTipos where IdTipo = '4' ORDER BY NombreDetalle");
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

        public DataTable LFuncion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdDetalleTipo,LTRIM(RTRIM(NombreDetalle)) AS NombreDetalle FROM Gestion.DetalleTipos where IdTipo = '5' ORDER BY NombreDetalle");
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

        public DataTable LOControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdDetalleTipo,LTRIM(RTRIM(NombreDetalle)) AS NombreDetalle FROM Gestion.DetalleTipos where IdTipo = '3' ORDER BY NombreDetalle");
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

        public DataTable loadCodigoArchivoControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdArchivo+1 AS NumRegistros FROM Gestion.Archivos ORDER BY IdArchivo DESC");
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

        public DataTable loadInfoArchivoSeguimiento(String IdSeguimiento, String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdArchivo,IdSeguimiento,IdPlanAccion,NombreArchivo,a.UrlArchivo,b.Usuario,CONVERT(varchar(10),a.FechaRegistro,120) as FechaRegistro from Gestion.Archivos a, Listas.Usuarios b where a.IdUsuario = b.IdUsuario and IdSeguimiento = " + IdSeguimiento + " and IdPlanAccion = " + IdPlanAccion + " order by FechaRegistro");
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

        public DataTable loadInfoArchivoGestion(String IdGestion, String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdArchivo,IdGestion,IdPlanAccion,NombreArchivo,a.UrlArchivo,b.Usuario,CONVERT(varchar(10),a.FechaRegistro,120) as FechaRegistro from Gestion.ArchivosGestion a, Listas.Usuarios b where a.IdUsuario = b.IdUsuario and IdGestion = " + IdGestion + " and IdPlanAccion = " + IdPlanAccion + " order by FechaRegistro");
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

        public void modificarObj(String Descripcion, String IdDetalleTipo, String FechaIni, String FechaFin, String IdObjetivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.ObjetivosEstrategicos SET Descripcion = N'" + Descripcion + "', IdDetalleTipo = " + IdDetalleTipo + ", FechaInicio = CONVERT(datetime,'" + FechaIni + "',120), FechaFin = CONVERT(datetime, '" + FechaFin + "',120) WHERE (IdObjetivo = " + IdObjetivo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarObj(String IdObjetivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.ObjetivosEstrategicos WHERE (IdObjetivo = " + IdObjetivo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable Estrategia(string IdObjetivo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT EST.IdEstrategia,EST.CodigoEstrategia,EST.Descripcion,US.Usuario,CONVERT(VARCHAR(10),EST.FechaRegistro,120) AS FechaRegistro from Gestion.Estrategias EST,Listas.Usuarios US WHERE EST.Idusuario = US.IdUsuario AND EST.IdObjetivo = " + IdObjetivo);
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

        public DataTable Meta(String IdEstrategia)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ME.IdMeta,ME.IdEstrategia,ME.Descripcion,DT.NombreDetalle as Funcion,DT2.NombreDetalle AS Formato, ME.Valor,US.Usuario,CONVERT(VARCHAR(10),ME.FechaRegistro,103) AS FechaRegistro FROM Gestion.Metas ME,Listas.Usuarios US, Gestion.DetalleTipos DT, Gestion.DetalleTipos DT2 WHERE ME.Idusuario = US.IdUsuario AND ME.Funcion=DT.IdDetalleTipo AND ME.Formato=DT2.IdDetalleTipo  AND (IdEstrategia = " + IdEstrategia + ")");
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

        public DataTable Gestion(String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT GE.IdGestion,GE.Descripcion,US.Usuario,CONVERT(VARCHAR(10),GE.FechaRegistro,120) as FechaRegistro FROM Gestion.Gestion GE, Listas.Usuarios US WHERE GE.IdUsuario = US.IdUsuario AND GE.IdPlanAccion =" + IdPlanAccion);
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

        public void agregarGestion(String IdPlanAccion, String Descripcion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.Gestion (IdPlanAccion,Descripcion, IdUsuario,FechaRegistro) VALUES (" + IdPlanAccion + ", N'" + Descripcion + "', '" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarPlanAccion(String Abierto_SN, String IdPlanAccion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.PlanAccion SET Abierto_SN = N'" + Abierto_SN + "' WHERE IdPlanAccion =" + IdPlanAccion);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarGestion(String Descripcion, String IdGestion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.Gestion SET Descripcion = N'" + Descripcion + "' WHERE (IdGestion = " + IdGestion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarEstrategia(String Descripcion, string IdEstrategia)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.Estrategias SET Descripcion = N'" + Descripcion + "' WHERE (IdEstrategia = " + IdEstrategia + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarMetas(String Descripcion, String IdMeta, String Funcion, String Formato, String Valor)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.Metas SET Descripcion = N'" + Descripcion + "',Funcion = N'" + Funcion + "',Formato = N'" + Formato + "',Valor = N'" + Valor + "' WHERE (IdMeta = " + IdMeta + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarEstrategia(String IdEstrategia)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.Estrategias WHERE (IdEstrategia = " + IdEstrategia + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarPA(String IdPlanAccion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.PlanAccion WHERE (IdPlanAccion = " + IdPlanAccion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarCausaEfecto(String IdCausaEfecto)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.CausaEfecto WHERE (IdCausaEfecto = " + IdCausaEfecto + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarMetas(String IdMeta)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.Metas WHERE (IdMeta = " + IdMeta + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarEstrategia(String CodigoEstrategia, String IdObjetivo, String Descripcion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.Estrategias (CodigoEstrategia, IdObjetivo,Descripcion, IdUsuario,FechaRegistro) VALUES ('" + CodigoEstrategia + "'," + IdObjetivo + ",N'" + Descripcion + "','" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarObj(String CodigoObjetivo, String IdPlan, String Descripcion, String IdDetalleTipo, String FechaIni, String FechaFin)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.ObjetivosEstrategicos (CodigoObjetivo, IdPlan,Descripcion,IdDetalleTipo,FechaInicio,FechaFin, IdUsuario,FechaRegistro) VALUES ('" + CodigoObjetivo + "','" + IdPlan + "',N'" + Descripcion + "'," + IdDetalleTipo + ", CONVERT(datetime,'" + FechaIni + "',120), CONVERT(datetime,'" + FechaFin + "',120),'" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarCausaEfecto(String IdObjetivoCausa, String IdObjetivoEfecto, String IdPlan)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.CausaEfecto (IdObjetivoCausa, IdObjetivoEfecto,IdPlan,IdUsuario,FechaRegistro) VALUES (" + IdObjetivoCausa + "," + IdObjetivoEfecto + "," + IdPlan + ",'" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarPAccion(String CodigoPlanAccion, String IdEstrategia, String Descripcion, String FechaIni, String FechaFin, String Responsable)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.PlanAccion (CodigoPlanAccion, IdEstrategia,Descripcion,FechaInicio,FechaFin,Responsable,Abierto_SN,IdUsuario,FechaRegistro) VALUES ('" + CodigoPlanAccion + "'," + IdEstrategia + ",N'" + Descripcion + "',CONVERT(datetime,'" + FechaIni + "',120), CONVERT(datetime,'" + FechaFin + "',120),'" + Responsable + "',N'S','" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable mtdAgregarPlan(string CodigoPlanAccion, string IdEstrategia, string Descripcion,
            string FechaIni, string FechaFin, string Responsable)
        {
            #region Variables
            string strConsulta = string.Empty, strValues = string.Empty,
                strConsultaRetorno = string.Empty, strConsultaFinal = string.Empty;
            DataTable dtInfo = new DataTable();
            #endregion Variables

            try
            {
                #region Consulta
                strConsulta = "INSERT INTO Gestion.PlanAccion (CodigoPlanAccion, IdEstrategia,Descripcion,FechaInicio,FechaFin,Responsable,Abierto_SN,IdUsuario,FechaRegistro)";
                strValues = string.Format("VALUES ('{0}', {1}, N'{2}',CONVERT(DATETIME, '{3}', 120), CONVERT(DATETIME, '{4}', 120), '{5}', N'S', '{6}', CONVERT(SMALLDATETIME, GETDATE(), 103))",
                    CodigoPlanAccion, IdEstrategia, Descripcion, FechaIni, FechaFin, Responsable, IdUsuario.ToString() );
                strConsultaRetorno = "SELECT SCOPE_IDENTITY()";

                strConsultaFinal = string.Format("{0} {1} {2}", strConsulta, strValues, strConsultaRetorno);
                #endregion Consulta

                cDataBase.conectar();
                dtInfo = cDataBase.mtdEjecutarConsultaSQL(strConsultaFinal);
                cDataBase.desconectar();

            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return dtInfo;
        }

        public void modificarPA(String Descripcion, String FechaIni, String FechaFin, String IdPlanAccion, String Responsable)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.PlanAccion SET Responsable = " + Responsable + ", Descripcion = N'" + Descripcion + "', FechaInicio = CONVERT(datetime,'" + FechaIni + "',120), FechaFin = CONVERT(datetime, '" + FechaFin + "',120) WHERE (IdPlanAccion = " + IdPlanAccion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarSeguimientos(String IdSeguimiento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.Seguimiento WHERE (IdSeguimiento = " + IdSeguimiento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarMetas(String IdEstrategia, String Descripcion, String Funcion, String Formato, String Valor)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.Metas (IdEstrategia,Descripcion,Funcion,Formato,Valor,IdUsuario,FechaRegistro) VALUES (" + IdEstrategia + ",N'" + Descripcion + "'," + Funcion + "," + Formato + ",N'" + Valor + "','" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable Seguimiento(string IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT SE.IdSeguimiento,SE.IdPlan,SE.NumeroActa,CONVERT(VARCHAR(10),SE.FechaReunion,120) as FechaReunion,DE.NombreDetalle AS OControl,SE.NumeroSeguimiento,SE.Comentarios,US.Usuario,CONVERT(VARCHAR(10),SE.FechaRegistro,120) as FechaRegistro FROM Gestion.Seguimiento SE,Listas.Usuarios US,Gestion.DetalleTipos DE WHERE SE.IdUsuario=US.IdUsuario AND SE.IdDetalleTipo = DE.IdDetalleTipo AND SE.IdPlan =" + IdPlan);
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

        public void modificarSeguimiento(String NumeroActa, String FechaReunion, String IdDetalleTipo, String NumeroSeguimiento, String Comentarios, String IdSeguimiento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.Seguimiento SET NumeroActa = N'" + NumeroActa + "',FechaReunion = CONVERT(smalldatetime,'" + FechaReunion + "',120), IdDetalleTipo = " + IdDetalleTipo + ", NumeroSeguimiento = " + NumeroSeguimiento + ", Comentarios = N'" + Comentarios + "' WHERE (IdSeguimiento = " + IdSeguimiento + ")");

                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarSeguimiento(String IdPlan, String NumeroActa, String FechaReunion, String IdDetalleTipo, String NumeroSeguimiento, String Comentarios)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.Seguimiento (IdPlan, NumeroActa,FechaReunion,IdDetalleTipo,NumeroSeguimiento,Comentarios, IdUsuario,FechaRegistro) VALUES (" + IdPlan + "," + NumeroActa + ",CONVERT(smalldatetime,'" + FechaReunion + "',120)," + IdDetalleTipo + ", " + NumeroSeguimiento + ",N'" + Comentarios + "' ,'" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable Indicadores()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IC.IdIndicador,IC.CodigoIndicador,IC.Descripcion,IC.IdPlanAccion,DE.NombreDetalle AS Periodicidad,IC.Meta,IC.Nominador,IC.Denominador,IC.Activo_SN,US.Usuario,CONVERT(VARCHAR(10),IC.FechaRegistro,103) AS FechaRegistro FROM Gestion.Indicadores IC,Gestion.DetalleTipos DE, Listas.Usuarios US WHERE IC.Periodicidad=DE.IdDetalleTipo AND IC.IdUsuario=US.IdUsuario");
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

        public DataTable IndicadoresActivosSinAsociar()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IC.IdIndicador,IC.CodigoIndicador,IC.Descripcion,DE.NombreDetalle AS Periodicidad,US.Usuario,CONVERT(VARCHAR(10),IC.FechaRegistro,120) AS FechaRegistro FROM Gestion.Indicadores IC,Gestion.DetalleTipos DE, Listas.Usuarios US WHERE IC.Periodicidad=DE.IdDetalleTipo AND IC.IdUsuario=US.IdUsuario AND IC.Activo_SN='S' AND IC.IdPlanAccion IS NULL");
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

        public DataTable IndicadoresActivosAsociados(String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IC.IdIndicador,IC.CodigoIndicador,IC.Descripcion,DE.NombreDetalle AS Periodicidad,US.Usuario,CONVERT(VARCHAR(10),IC.FechaRegistro,120) AS FechaRegistro FROM Gestion.Indicadores IC,Gestion.DetalleTipos DE, Listas.Usuarios US WHERE IC.Periodicidad=DE.IdDetalleTipo AND IC.IdUsuario=US.IdUsuario AND IC.Activo_SN='S' AND IC.IdPlanAccion = '" + IdPlanAccion + "'");
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

        public DataTable AsociarIndicador(String IdPlanAccion, String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("UPDATE Gestion.Indicadores SET IdPlanAccion ='" + IdPlanAccion + "' where IdIndicador='" + IdIndicador + "'");
                dtInformacion = cDataBase.ejecutarConsulta("update Gestion.PlanAccion set tieneindicador = 1 where IdPlanAccion = " + IdPlanAccion);
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

        public DataTable DesasociarIndicador(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            DataTable dtInf = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInf = cDataBase.ejecutarConsulta("SELECT COUNT(IdIndicador) Indicador FROM Gestion.Indicadores WHERE IdPlanAccion = (SELECT IdPlanAccion FROM Gestion.Indicadores WHERE IdIndicador = '" + IdIndicador + "')");
                if (dtInf.Rows[0]["Indicador"].ToString().Trim() == "1")
                {
                    dtInformacion = cDataBase.ejecutarConsulta("update Gestion.PlanAccion set tieneindicador = 0 where IdPlanAccion = (select IdPlanAccion from Gestion.Indicadores where IdIndicador = '" + IdIndicador + "')");
                }
                dtInformacion = cDataBase.ejecutarConsulta("UPDATE Gestion.Indicadores SET IdPlanAccion = NULL where IdIndicador='" + IdIndicador + "'");
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

        public DataTable Variables(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT VA.IdIndicador,VA.IdVariable,VA.Nombre,DE.NombreDetalle AS Formato FROM Gestion.Variables VA,Gestion.DetalleTipos DE WHERE VA.Formato=DE.IdDetalleTipo AND VA.IdIndicador= " + IdIndicador);
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

        public DataTable Metas(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdMetaValor, IdIndicador, Dia, Mes, Ano, convert(varchar,Ano)+'-'+convert(varchar,Mes)+'-'+convert(varchar,Dia) as Periodo, Meta from Gestion.MetaValor where IdIndicador = " + IdIndicador + "order by Ano,Mes,Dia");
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

        public DataTable ValoresVariables(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT VA.IdVariable,VA.Nombre,D.NombreDetalle AS Formato,VV.Valor,VV.Dia,VV.Mes,VV.Ano FROM Gestion.Variables VA LEFT JOIN Gestion.VariableValor VV ON VA.IdVariable=VV.IdVariable AND VV.Validado_SN = 'N', Gestion.DetalleTipos D WHERE VA.Formato=D.IdDetalleTipo AND VA.IdIndicador = " + IdIndicador);
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

        public DataTable AsignarValoresDia(String IdVariable, String Valor, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("UPDATE Gestion.VariableValor SET Valor=" + Valor + ",Dia = " + Dia + ",Mes = " + Mes + ",Ano = " + Ano + " WHERE Validado_SN ='N' AND IdVariable=" + IdVariable);
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

        public DataTable AsignarValores(String IdVariable, String Valor, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("UPDATE Gestion.VariableValor SET Valor=" + Valor + ",Dia = " + Dia + ",Mes =" + Mes + ",Ano = " + Ano + " WHERE Validado_SN ='N' AND IdVariable=" + IdVariable);
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

        public DataTable VerResultados(String IdIndicador, String dia, String mes, String ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select COUNT(a.Resultado) as ContadorResultados from Gestion.IndicadorResultado a where a.IdIndicador = " + IdIndicador + " and a.Dia = " + dia + " and a.Mes = " + mes + " and a.Ano = " + ano);
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

        public DataTable VerMetaNuevoPeriodo(String IdIndicador, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select Meta from Gestion.MetaValor where IdIndicador = " + IdIndicador + " and Dia = " + Dia + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable NuevoPeriodo(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("EXEC dbo.SP_CalcularIndicadores " + IdIndicador);
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

        public DataTable NuevoPeriodoHoy(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("EXEC dbo.SP_CalcularIndicadoresHoy " + IdIndicador);
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

        public DataTable NuevoPeriodoHoyUpdate(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("EXEC dbo.SP_CalcularIndicadoresHoyUpdate " + IdIndicador);
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

        public DataTable CantidadVariables(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select count(idvariable) as CantVariables from Gestion.Variables where IdIndicador = " + IdIndicador);
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

        public void IndicadorUnaVariable(String IdIndicador, String ResultadoVar, String Dia, String Mes, String Ano)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("insert into Gestion.IndicadorResultado (IdIndicador,Dia,Mes,Ano,Resultado) values ('" + IdIndicador + "','" + Dia + "','" + Mes + "','" + Ano + "',(('" + ResultadoVar + "' * 100) / (select Meta from Gestion.MetaValor where IdIndicador = '" + IdIndicador + "' and Dia = '" + Dia + "' and Mes = '" + Mes + "' and Ano = '" + Ano + "')))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void IndicadorUnaVariableHoyUpdate(String IdIndicador, String ResultadoVar, String Dia, String Mes, String Ano)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("update Gestion.IndicadorResultadoHoy set Resultado = (('" + ResultadoVar + "' * 100) / (select Meta from Gestion.MetaValor where IdIndicador = '" + IdIndicador + "' and Dia = '" + Dia + "' and Mes = '" + Mes + "' and Ano = '" + Ano + "')) where IdIndicador = '" + IdIndicador + "' and Dia = '" + Dia + "' and Mes = '" + Mes + "' and Ano = '" + Ano + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void IndicadorUnaVariablehoy(String IdIndicador, String ResultadoVar, String Dia, String Mes, String Ano)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("insert into Gestion.IndicadorResultadoHoy (IdIndicador,Dia,Mes,Ano,Resultado) values ('" + IdIndicador + "','" + Dia + "','" + Mes + "','" + Ano + "',(('" + ResultadoVar + "' * 100) / (select Meta from Gestion.MetaValor where IdIndicador = '" + IdIndicador + "' and Dia = '" + Dia + "' and Mes = '" + Mes + "' and Ano = '" + Ano + "')))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable CantidadResultados(String IdIndicador, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select count(idvariable) as CantVariablesResultado from Gestion.VariableValor where IdVariable in (select idvariable as CantVariables from Gestion.Variables where IdIndicador = " + IdIndicador + ") and Dia = " + Dia + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable CantidadIndicadorResultados(String IdIndicador, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdIndicador from Gestion.IndicadorResultadoHoy where IdIndicador = " + IdIndicador + " and Dia = " + Dia + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable CumplimientoIndicador(String IdIndicador, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select a.IdMetaValor,a.IdIndicador,a.Dia,a.Mes,a.Ano,a.Meta,b.Resultado,b.Resultado * 100 / a.Meta as cumplimiento from Gestion.MetaValor a,Gestion.IndicadorResultado b where a.IdIndicador = b.IdIndicador and a.Dia=b.Dia and a.Mes=b.Mes and a.Ano=b.Ano and a.IdIndicador = " + IdIndicador + " and a.Dia=" + Dia + " and a.Mes=" + Mes + " and a.Ano=" + Ano);
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

        public DataTable CumplimientoIndicadorHoy(String IdIndicador, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select a.IdMetaValor,a.IdIndicador,a.Dia,a.Mes,a.Ano,a.Meta,b.Resultado,b.Resultado * 100 / a.Meta as cumplimiento from Gestion.MetaValor a,Gestion.IndicadorResultadoHoy b where a.IdIndicador = b.IdIndicador and a.Dia=b.Dia and a.Mes=b.Mes and a.Ano=b.Ano and a.IdIndicador = " + IdIndicador + " and a.Dia=" + Dia + " and a.Mes=" + Mes + " and a.Ano=" + Ano);
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

        public DataTable CumplimientoIndicadorPeriodo(String IdIndicador, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select AVG(cumplimiento) AS Cumplimiento from Gestion.IndicadorCumplimiento where IdIndicador = " + IdIndicador + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable CumplimientoIndicadorPeriodoHoy(String IdIndicador, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select AVG(cumplimiento) AS Cumplimiento from Gestion.IndicadorCumplimientoHoy where IdIndicador = " + IdIndicador + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable CantidadIndicador(String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select COUNT(*) as CantidadObjetos from Gestion.Indicadores where IdPlanAccion = " + IdPlanAccion);
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

        public DataTable CantidadPlanAccion(String IdEstrategia)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                //dtInformacion = cDataBase.ejecutarConsulta("select COUNT(*) as CantidadObjetos from Gestion.PlanAccion where IdEstrategia = " + IdEstrategia);
                //04-02-2016
                dtInformacion = cDataBase.ejecutarConsulta("select COUNT(*) as CantidadObjetos from Gestion.PlanAccion where IdEstrategia = " + IdEstrategia + "AND TieneIndicador = 1");
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

        public DataTable CantidadEstrategia(String IdObjetivo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                //dtInformacion = cDataBase.ejecutarConsulta("select COUNT(*) as CantidadObjetos from Gestion.Estrategias where IdObjetivo = " + IdObjetivo);
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT(a.IdEstrategia) AS CantidadObjetos FROM Gestion.Estrategias a, Gestion.PlanAccion b WHERE a.IdEstrategia = b.IdEstrategia and a.IdObjetivo = '" + IdObjetivo + "' and b.tieneindicador = 1");
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

        public DataTable TotalCumplimiento(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano, String Select)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select SUM(Cumplimiento) as TotalCumplimiento from Gestion.Cumplimiento where TipoObjeto = " + TipoObjeto + " and IdObjeto in (" + Select + " = " + IdObjeto + ") and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable TotalCumplimientoHoy(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano, String Select)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select SUM(Cumplimiento) as TotalCumplimiento from Gestion.CumplimientoHoy where TipoObjeto = " + TipoObjeto + " and IdObjeto in (" + Select + " = " + IdObjeto + ") and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable InsertCumplimiento(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano, String Meta, String Resultado, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("insert into Gestion.Cumplimiento (TipoObjeto,IdObjeto,Dia,Mes,Ano,Meta,Resultado,Cumplimiento,FechaRegistro) values (" + TipoObjeto + "," + IdObjeto + "," + Dia + "," + Mes + "," + Ano + "," + Meta + "," + Resultado + "," + Cumplimiento + ",GETDATE())");
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

        public void DeleteCumplimiento(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("delete Gestion.CumplimientoHoy where TipoObjeto = '" + TipoObjeto + "' and IdObjeto = '" + IdObjeto + "' and Dia = '" + Dia + "' and Mes = '" + Mes + "' and Ano = '" + Ano + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable InsertCumplimientoHoy(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano, String Meta, String Resultado, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("insert into Gestion.CumplimientoHoy (TipoObjeto,IdObjeto,Dia,Mes,Ano,Meta,Resultado,Cumplimiento,FechaRegistro) values (" + TipoObjeto + "," + IdObjeto + "," + Dia + "," + Mes + "," + Ano + "," + Meta + "," + Resultado + "," + Cumplimiento + ",GETDATE())");
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

        public DataTable InsertCumplimientoIndicador(String IdIndicador, String Dia, String Mes, String Ano, String Meta, String Resultado, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("insert into Gestion.IndicadorCumplimiento (IdIndicador,Dia,Mes,Ano,Meta,Resultado,Cumplimiento,FechaRegistro) values (" + IdIndicador + "," + Dia + "," + Mes + "," + Ano + "," + Meta + "," + Resultado + "," + Cumplimiento + ",GETDATE())");
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

        public void DeleteCumplimientoIndicador(String IdIndicador, String Dia, String Mes, String Ano)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("delete Gestion.IndicadorCumplimientoHoy where IdIndicador = '" + IdIndicador + "' and Dia = '" + Dia + "' and Mes = '" + Mes + "' and Ano = '" + Ano + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable InsertCumplimientoIndicadorHoy(String IdIndicador, String Dia, String Mes, String Ano, String Meta, String Resultado, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("insert into Gestion.IndicadorCumplimientoHoy (IdIndicador,Dia,Mes,Ano,Meta,Resultado,Cumplimiento,FechaRegistro) values (" + IdIndicador + "," + Dia + "," + Mes + "," + Ano + "," + Meta + "," + Resultado + "," + Cumplimiento + ",GETDATE())");
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

        public DataTable InsertCumplimientoIndicadorHoyUpdate(String IdIndicador, String Dia, String Mes, String Ano, String Meta, String Resultado, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("update Gestion.IndicadorCumplimientoHoy set Meta = " + Meta + ", Resultado = " + Resultado + ", Cumplimiento = " + Cumplimiento + " where IdIndicador = " + IdIndicador + " and Dia = " + Dia + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable VerCumplimientoIndicador(String IdObjeto, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select * from Gestion.Cumplimiento where TipoObjeto = 1 and IdObjeto = " + IdObjeto + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable IndicadoresHoy(String IdIndicador, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select * from Gestion.IndicadorCumplimientoHoy where IdIndicador = " + IdIndicador + " and Dia= " + Dia + " and Mes=" + Mes + " and Ano = " + Ano);
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

        public DataTable VerCumplimientoIndicadorHoy(String IdObjeto, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select * from Gestion.CumplimientoHoy where TipoObjeto = 1 and IdObjeto = " + IdObjeto + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable UpdateCumplimiento(String TipoObjeto, String IdObjeto, String Mes, String Ano, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("update Gestion.Cumplimiento set Cumplimiento = " + Cumplimiento + " where TipoObjeto = 1 and IdObjeto = " + IdObjeto + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable UpdateCumplimientoHoy(String TipoObjeto, String IdObjeto, String Mes, String Ano, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("update Gestion.CumplimientoHoy set Cumplimiento = " + Cumplimiento + " where TipoObjeto = 1 and IdObjeto = " + IdObjeto + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable UpdateCumplimiento(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("update Gestion.Cumplimiento set Cumplimiento = " + Cumplimiento + " where TipoObjeto = " + TipoObjeto + " and IdObjeto = " + IdObjeto + " and Dia = " + Dia + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable UpdateCumplimientoHoy(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano, String Cumplimiento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("update Gestion.CumplimientoHoy set Cumplimiento = " + Cumplimiento + " where TipoObjeto = " + TipoObjeto + " and IdObjeto = " + IdObjeto + " and Dia = " + Dia + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable VerCumplimiento(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select * from Gestion.Cumplimiento where TipoObjeto = " + TipoObjeto + " and IdObjeto = " + IdObjeto + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public DataTable VerCumplimientoHoy(String TipoObjeto, String IdObjeto, String Dia, String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select * from Gestion.CumplimientoHoy where TipoObjeto = " + TipoObjeto + " and IdObjeto = " + IdObjeto + " and Mes = " + Mes + " and Ano = " + Ano);
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

        public void agregarIndicadorParcial(String CodigoIndicador, String Descripcion, String Periodicidad)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.Indicadores (CodigoIndicador,Descripcion,Periodicidad,IdUsuario,FechaRegistro) VALUES (N'" + CodigoIndicador + "',N'" + Descripcion + "'," + Periodicidad + ",'" + IdUsuario.ToString() + "',convert(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarIndicadorTotal(String Descripcion, String Periodicidad, String Nominador, String Denominador, String IdIndicador, String Activo_SN)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.Indicadores SET Descripcion =N'" + Descripcion + "',Periodicidad=" + Periodicidad + ",Nominador = N'" + Nominador + "',Denominador = N'" + Denominador + "',Activo_SN = N'" + Activo_SN + "' WHERE (IdIndicador = " + IdIndicador + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void ModificarVariables(String Nombre, String Formato, String IdVariable)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.Variables SET Nombre = N'" + Nombre + "',Formato = " + Formato + " WHERE (IdVariable = " + IdVariable + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarVariables(String IdIndicador, String Nombre, String Formato)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.Variables (IdIndicador,Nombre,Formato,IdUsuario,FechaRegistro) " +
                    "VALUES (" + IdIndicador + ",'" + Nombre + "'," + Formato + ",'" + IdUsuario.ToString() + "'," +
                    "CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarVariableValor(String IdVariable)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Gestion.VariableValor (IdVariable,Valor,Validado_SN,IdUsuario,FechaRegistro) VALUES (" + IdVariable + ",0,'N','" + IdUsuario.ToString() + "',CONVERT(smalldatetime,getdate(),103))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable LPeriodicidad()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdDetalleTipo,LTRIM(RTRIM(NombreDetalle)) AS NombreDetalle FROM Gestion.DetalleTipos where IdTipo = '2' ORDER BY IdDetalleTipo");
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

        public String CodigoConsecutivo(String Campo, String Tabla)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT (MAX(" + Campo + ")+1) AS CODIGO FROM " + Tabla + "");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0]["CODIGO"].ToString().Trim();
        }

        public void eliminarVariables(String IdVariable)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.Variables WHERE (IdVariable = " + IdVariable + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarIndicador(String IdIndicador)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.Indicadores WHERE (IdIndicador = " + IdIndicador + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InsertarMeta(String IdIndicador, String Meta, String Dia, String Mes, String Ano)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("insert into Gestion.MetaValor (IdIndicador,Meta,Dia,Mes,Ano,IdUsuario,FechaRegistro) values (" + IdIndicador + "," + Meta + "," + Dia + "," + Mes + "," + Ano + "," + IdUsuario.ToString() + ",GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InsertarMetaDia(String IdIndicador, String Meta, String Fecha)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("insert into Gestion.MetaValor (IdIndicador,Meta,Dia,Mes,Ano,IdUsuario,FechaRegistro) values (" + IdIndicador + "," + Meta + ",day(CONVERT(smalldatetime,'" + Fecha + "',120)),month(CONVERT(smalldatetime,'" + Fecha + "',120)),year(CONVERT(smalldatetime,'" + Fecha + "',120))," + IdUsuario.ToString() + ",GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void ModifficarMeta(String IdMetaValor, String IdIndicador, String Meta, String Dia, String Mes, String Ano)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("update Gestion.MetaValor set Meta = " + Meta + ", Dia = " + Dia + ",Mes = " + Mes + ", Ano = " + Ano + " where IdIndicador = " + IdIndicador + " and IdMetaValor =" + IdMetaValor);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void ModifficarMetaDia(String IdMetaValor, String IdIndicador, String Meta, String Fecha)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("update Gestion.MetaValor set Meta = " + Meta + ", Dia = day(CONVERT(smalldatetime,'" + Fecha + "',120)), Mes = month(CONVERT(smalldatetime,'" + Fecha + "',120)), Ano = year(CONVERT(smalldatetime,'" + Fecha + "',120)) where IdIndicador = " + IdIndicador + " and IdMetaValor = " + IdMetaValor);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void eliminarMeta(String IdMetaValor)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("delete Gestion.MetaValor where IdMetaValor = " + IdMetaValor);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public String ValidarFechaPE(String FechaDato)
        {
            DateTime fechaF = Convert.ToDateTime(FechaDato).Date;
            String res;
            DateTime FechAc = DateTime.Now.Date;

            if (fechaF < FechAc)
                res = "S";
            else
                res = "N";

            return res;
        }

        public DataTable loadCodigo(String Campo, String Tabla)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) " + Campo + "+1 AS NumRegistros FROM " + Tabla + " ORDER BY " + Campo + " DESC");
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

        public DataTable loadCodigo_After(String Campo, String Tabla)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) " + Campo + " AS NumRegistros FROM " + Tabla + " ORDER BY " + Campo + " DESC");
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
        public DataTable loadCodigoOC(String IdPlan, String IdDetalleTipo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT(idseguimiento)+1 as Codigo from Gestion.Seguimiento where IdPlan = " + IdPlan + " and IdDetalleTipo =" + IdDetalleTipo);
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

        public DataTable cmi_objetivos(String IdPlan, String IdDetalleTipo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select obj.IdObjetivo,obj.CodigoObjetivo,obj.Descripcion,convert(varchar,obj.FechaInicio,103) as Inicio,convert(varchar,obj.FechaFin,103) as Fin from Gestion.ObjetivosEstrategicos obj where IdPlan = " + IdPlan + " and obj.IdDetalleTipo =" + IdDetalleTipo);
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

        public DataTable cmi_cumplimientoOBJ(String IdObjetivo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select SUM((I.Resultado*100)/g.Meta)/(count(I.Resultado)) as Total from Gestion.IndicadorResultado I,Gestion.Indicadores g,Gestion.PlanAccion h,Gestion.Estrategias j where I.IdIndicador=g.IdIndicador and g.IdPlanAccion=h.IdPlanAccion and h.IdEstrategia= j.IdEstrategia and j.IdObjetivo=" + IdObjetivo);
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

        public DataTable cmi_cumplimientoEST(String IdEstrategia)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select SUM((I.Resultado*100)/g.Meta)/(count(I.Resultado)) as Total from Gestion.IndicadorResultado I,Gestion.Indicadores g,Gestion.PlanAccion h where I.IdIndicador=g.IdIndicador and g.IdPlanAccion=h.IdPlanAccion and h.IdEstrategia=" + IdEstrategia);
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

        public DataTable cmi_cumplimientoPA(String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select SUM((I.Resultado*100)/g.Meta)/(count(I.Resultado)) as Total from Gestion.IndicadorResultado I,Gestion.Indicadores g where I.IdIndicador=g.IdIndicador and g.IdPlanAccion = " + IdPlanAccion);
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

        public DataTable cmi_cumplimientoIndicador(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select SUM((I.Resultado*100)/g.Meta)/(count(I.Resultado)) as Total from Gestion.IndicadorResultado I,Gestion.Indicadores g where I.IdIndicador=g.IdIndicador and g.IdIndicador= " + IdIndicador);
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

        public DataTable cmi_cumplimientoIndicadorResultado(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select I.IdIndicador,g.Descripcion,I.Dia,CONVERT(int,I.Mes)as elmes,I.Ano,g.Meta,I.Resultado,(I.Resultado*100)/g.Meta as Cumplimiento,de.NombreDetalle as Periodicidad from Gestion.IndicadorResultado I,Gestion.Indicadores g,Gestion.DetalleTipos de where I.IdIndicador=g.IdIndicador and g.Periodicidad=de.IdDetalleTipo and g.IdIndicador = " + IdIndicador + " order by elmes,I.Dia,I.Ano");
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

        public DataTable cmi_vercolor()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select C.Color,C.ColorMinimo,C.ColorMaximo from Gestion.ColorCMI C order by C.ColorMinimo");
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

        public DataTable cmi_parametrizacolor(String ColorMinimo, String ColorMaximo, String Color)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("UPDATE Gestion.ColorCMI SET ColorMinimo = " + ColorMinimo + ", ColorMaximo = " + ColorMaximo + " WHERE Color = '" + Color + "'");
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

        public DataTable cmi_Color(decimal ValorColor)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select c.Color from Gestion.ColorCMI c where c.ColorMinimo <= " + ValorColor + " and c.ColorMaximo >=" + ValorColor);
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

        public DataTable cmi_PerspectivaOjbr(string IdObjetivo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select PER.NombreDetalle as Perspectiva from Gestion.ObjetivosEstrategicos OBJ, Gestion.DetalleTipos PER where OBJ.IdDetalleTipo=PER.IdDetalleTipo AND OBJ.IdObjetivo =" + IdObjetivo);
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

        public DataTable cmifechaspe(String IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT PE.IdPlan,CONVERT(VARCHAR(10),PE.FechaInicio,103) AS FechaInicio,CONVERT(VARCHAR(10),PE.FechaFin,103) AS FechaFin FROM Gestion.PlanEstrategico PE, Listas.Usuarios US  WHERE PE.IdPlan=" + IdPlan);
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

        public DataTable cmiResponsables(String IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select d.Responsable,e.NombreResponsable from Gestion.PlanEstrategico a, Gestion.ObjetivosEstrategicos b, Gestion.Estrategias c, Gestion.PlanAccion d,Parametrizacion.DetalleJerarquiaOrg e where a.IdPlan=b.IdPlan and b.IdObjetivo=c.IdObjetivo and c.IdEstrategia=d.IdEstrategia and d.Responsable=e.idHijo and a.IdPlan = " + IdPlan + " group by d.Responsable,e.NombreResponsable");
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

        public DataTable cmiResponsablesObjetivos(String IdPlan, String Responsable)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select ob.IdObjetivo,ob.Descripcion from Gestion.PlanAccion pa,Gestion.ObjetivosEstrategicos ob,Gestion.Estrategias es,Gestion.PlanEstrategico pe where ob.IdObjetivo=es.IdObjetivo and es.IdEstrategia=pa.IdEstrategia and ob.IdPlan=pe.IdPlan and pe.IdPlan= " + IdPlan + " and pa.Responsable = " + Responsable + " group by ob.IdObjetivo,ob.Descripcion");
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

        public DataTable cmiResponsablesPlanesAccion(String IdObjetivo, String Responsable)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select pa.IdPlanAccion,pa.Descripcion,CONVERT(varchar,pa.FechaInicio,103) as Inicio,CONVERT(varchar,pa.FechaFin,103) as Fin,pa.Abierto_SN from Gestion.PlanAccion pa,Gestion.ObjetivosEstrategicos ob,Gestion.Estrategias es where ob.IdObjetivo=es.IdObjetivo and es.IdEstrategia=pa.IdEstrategia and ob.IdObjetivo= " + IdObjetivo + " and pa.Responsable = " + Responsable + " order by pa.IdPlanAccion");
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

        public DataTable cmi_PAIndicadoresGlobales(String IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select pa.IdPlanAccion,pa.Descripcion as PlanAccion from Gestion.PlanEstrategico pe,Gestion.ObjetivosEstrategicos ob,Gestion.Estrategias es, Gestion.PlanAccion pa where pe.IdPlan=ob.IdPlan and ob.IdObjetivo=es.IdObjetivo and es.IdEstrategia=pa.IdEstrategia and pe.IdPlan= " + IdPlan + " order by pa.IdPlanAccion");
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

        public DataTable cmiIndicadoresGlobales(String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select ind.IdIndicador, ind.Descripcion as Indicador,Meta from Gestion.PlanAccion pa, Gestion.Indicadores ind where pa.IdPlanAccion=ind.IdPlanAccion and pa.IdPlanAccion = " + IdPlanAccion + " order by ind.IdIndicador");
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

        public DataTable cmiIndicadoresDetalle(String IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select ge.IdIndicador,ge.Descripcion,ge.Nominador,ge.Denominador,de.NombreDetalle as Periodicidad,ge.Meta from Gestion.Indicadores ge,Gestion.PlanAccion pa, Gestion.Estrategias es, Gestion.ObjetivosEstrategicos ob, Gestion.PlanEstrategico pe,Gestion.DetalleTipos de where pe.IdPlan=ob.IdPlan and ob.IdObjetivo=es.IdObjetivo and es.IdEstrategia=pa.IdEstrategia and pa.IdPlanAccion=ge.IdPlanAccion and ge.Periodicidad=de.IdDetalleTipo and pe.IdPlan = " + IdPlan + " order by ge.IdIndicador");
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

        public DataTable IndicadorFormula(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select a.IdIndicador,a.Nominador,a.Denominador,b.NombreDetalle as Periodicidad from Gestion.Indicadores a, Gestion.DetalleTipos b where a.Periodicidad=b.IdDetalleTipo and a.IdIndicador =" + IdIndicador);
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

        public DataTable cmi_IndicadoreDetalle(String IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select ob.IdObjetivo,ob.Descripcion as Objetivo,de.NombreDetalle as Perspectiva,pa.IdPlanAccion,pa.Descripcion as PlanAccion,gi.Meta from Gestion.ObjetivosEstrategicos ob, Gestion.Estrategias es, Gestion.PlanAccion pa, Gestion.Indicadores gi,Gestion.DetalleTipos de where ob.IdObjetivo=es.IdObjetivo and es.IdEstrategia=pa.IdEstrategia and pa.IdPlanAccion=gi.IdPlanAccion and ob.IdDetalleTipo=de.IdDetalleTipo and gi.IdIndicador = " + IdIndicador);
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

        public DataTable cmi_PlanesAccion(String IdEstrategia)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select PA.IdPlanAccion, PA.Descripcion,DJ.NombreResponsable AS Responsable, PA.Abierto_SN, CONVERT(VARCHAR(10),PA.FechaInicio,103) as FechaInicio,CONVERT(VARCHAR(10),PA.FechaFin,103) as FechaFin from Gestion.PlanAccion PA, Listas.Usuarios US, Parametrizacion.DetalleJerarquiaOrg DJ where PA.IdUsuario=US.IdUsuario and PA.Responsable=DJ.idHijo AND PA.IdEstrategia = " + IdEstrategia);
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

        public DataTable cmi_Gestiones(String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdPlanAccion, g.Descripcion,convert(varchar(10),g.FechaRegistro,103) as Fecha from Gestion.Gestion g where IdPlanAccion = " + IdPlanAccion);
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

        public DataTable cmi_PlanesAccionIndicador(String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select a.IdIndicador,a.Descripcion,a.Meta,b.NombreDetalle as Periodicidad from Gestion.Indicadores a,Gestion.DetalleTipos b,Gestion.PlanAccion c where a.Periodicidad=b.IdDetalleTipo and a.IdPlanAccion = c.IdPlanAccion and c.IdPlanAccion = " + IdPlanAccion);
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

        public DataTable cmi_VerIndicadoresObjetivos(String IdObjetivo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select count(b.IdIndicador) as CantidadIndicadores from Gestion.PlanAccion a, Gestion.Indicadores b,Gestion.Estrategias c,Gestion.ObjetivosEstrategicos d where a.IdPlanAccion=b.IdPlanAccion and a.IdEstrategia=c.IdEstrategia and d.IdObjetivo=c.IdObjetivo and d.IdObjetivo = " + IdObjetivo);
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

        public DataTable cmi_VerIndicadoresEstrategias(String IdEstrategia)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select count(b.IdIndicador) as CantidadIndicadores from Gestion.PlanAccion a, Gestion.Indicadores b,Gestion.Estrategias c where a.IdPlanAccion=b.IdPlanAccion and a.IdEstrategia=c.IdEstrategia and c.IdEstrategia = " + IdEstrategia);
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

        public DataTable cmi_VerIndicadoresPlaAccion(String IdPlanAccion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select count(b.IdIndicador) as CantidadIndicadores from Gestion.PlanAccion a, Gestion.Indicadores b  where a.IdPlanAccion=b.IdPlanAccion and a.IdPlanAccion = " + IdPlanAccion);
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

        public DataTable VerPeriodoCerrado(String Mes, String Ano)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select COUNT(IdPeriodo) as Periodos from Gestion.PeriodoCerrado where Mes = " + Mes + " and Ano = " + Ano);
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

        #region PDFs


        public DataTable loadCodigoArchivoSeguimiento()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdArchivo+1 AS NumRegistros FROM Gestion.Archivos ORDER BY IdArchivo DESC");
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

        public DataTable loadCodigoArchivoGestion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdArchivo+1 AS NumRegistros FROM Gestion.ArchivosGestion ORDER BY IdArchivo DESC");
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

        public DataTable PlanAccionIndicador(string IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT a.CodigoPAI, a.Descripcion, c.NombreHijo Responsable, CASE a.Estado WHEN 1 THEN 'Abierto' else 'Cerrado' end Estado, convert(varchar, a.FechaCompromiso,103)FechaCompromiso, b.Usuario,convert(varchar, a.FechaRegistro,120)FechaRegistro FROM Gestion.IndicadorPlanAccion a LEFT JOIN Listas.Usuarios b on a.IdUsuario = b.IdUsuario LEFT JOIN Parametrizacion.JerarquiaOrganizacional c on a.Responsable = c.Idhijo WHERE a.Idindicador = " + IdIndicador + " order by FechaRegistro");
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

        public DataTable PlanAccionIndicadorComentarios(string CodigoPAI)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT c.CodigoPAI,RTRIM(a.Comentario)Comentario,CONVERT(VARCHAR,a.FechaRegistro, 120)FechaRegistro,RTRIM(b.Nombres)+' '+RTRIM(b.Apellidos) NombreUsuario FROM Gestion.Comentarios a, Listas.Usuarios b, Gestion.IndicadorPlanAccion c WHERE a.IdPAI = 1 AND a.IdUsuario = b.IdUsuario AND a.IdPAI = c.IdPAI AND c.CodigoPAI = '" + CodigoPAI + "' ORDER BY a.FechaRegistro");
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

        public void AddPlanAccionIndicador(string CodigoPAI, string IdIndicador, string Descripcion, string Responsable, string Estado, string Dia, string Mes, string Ano, string FechaCompromiso)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("INSERT INTO Gestion.IndicadorPlanAccion (CodigoPAI,IdIndicador,Descripcion,Responsable,Estado,Dia,Mes,Ano,FechaCompromiso,IdUsuario,FechaRegistro) VALUES ('" + CodigoPAI + "'," + IdIndicador + ",'" + Descripcion + "'," + Responsable + "," + Estado + "," + Dia + "," + Mes + "," + Ano + ", CONVERT(datetime,'" + FechaCompromiso + "',120)," + IdUsuario.ToString() + ",GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePlanAccionIndicador(string CodigoPAI, string Descripcion, string Responsable, string Estado, string Dia, string Mes, string Ano, string FechaCompromiso,string justificacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Gestion.IndicadorPlanAccion SET Descripcion='" + Descripcion + "',Responsable='" + Responsable + "',Estado='" + Estado + "',Dia='" + Dia + "',Mes='" + Mes + "',Ano='" + Ano + "',FechaCompromiso = CONVERT(datetime,'" + FechaCompromiso + "',120) WHERE CodigoPAI = '" + CodigoPAI + "'");
                cDataBase.ejecutarQuery("INSERT INTO gestion.comentarios (IdPAI,Comentario,IdUsuario,FechaRegistro) VALUES ((SELECT IdPAI FROM Gestion.IndicadorPlanAccion WHERE CodigoPAI = '" + CodigoPAI + "'),'" + justificacion + "'," + IdUsuario.ToString() + ",GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable ModificarPAI(string CodigoPAI)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT a.CodigoPAI, a.Descripcion,a.Responsable CodResponsable, c.NombreHijo Responsable, case a.Estado when 1 then '1' else '0' end Estado,a.Mes,a.Ano,REPLACE(CONVERT(varchar,a.FechaCompromiso, 111), '/', '-')FechaCompromiso FROM Gestion.IndicadorPlanAccion a LEFT JOIN Parametrizacion.JerarquiaOrganizacional c on a.Responsable = c.Idhijo WHERE a.CodigoPAI = '" + CodigoPAI + "'");
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

        #region [Viejo] Archivo Seguimiento
        public DataTable agregarArchivoSeguimiento(String IdSeguimiento, String IdPlanAccion, String UrlArchivo, String NombreArchivo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("insert into  Gestion.Archivos (IdSeguimiento,IdPlanAccion,NombreArchivo,IdUsuario,FechaRegistro,UrlArchivo) values ("
                    + IdSeguimiento + "," + IdPlanAccion + ",'" + NombreArchivo + "'," + IdUsuario.ToString() + ",getdate(),'" + UrlArchivo + "')");
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
        #endregion [Viejo] Archivo Seguimiento

        #region [Viejo] Archivo Gestion
        public DataTable agregarArchivoGestion(String IdGestion, String IdPlanAccion, String UrlArchivo, String NombreArchivo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("insert into  Gestion.ArchivosGestion (IdGestion,IdPlanAccion,NombreArchivo,IdUsuario,FechaRegistro,UrlArchivo) values (" + IdGestion + "," + IdPlanAccion + ",'" + NombreArchivo + "'," + IdUsuario.ToString() + ",getdate(),'" + UrlArchivo + "')");
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
        #endregion [Viejo] Archivo Gestion

        #region [Nuevo] Archivo Seguimiento
        public void mtdAgregarPdfSeguimiento(string strIdSeguimiento, string strIdPlanAccion, string strNombreArchivo, string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO Gestion.Archivos ([IdSeguimiento], [IdPlanAccion], [NombreArchivo], [IdUsuario], [FechaRegistro], [UrlArchivo], [ArchivoPDF]) VALUES ({0}, {1}, N'{3}', {2}, GETDATE(), N'{4}', @PdfData)",
                    strIdSeguimiento, strIdPlanAccion, IdUsuario.ToString().Trim(), strNombreArchivo, strUrlArchivo);

                cDataBase.mtdConectarSql();
                cDataBase.mtdEjecutarConsultaSQL(strConsulta, bPdfData);
                cDataBase.mtdDesconectarSql();
            }
            catch (Exception ex)
            {
                cDataBase.mtdDesconectarSql();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public byte[] mtdDescargarPdfSeguimiento(string strNombreArchivo)
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [Gestion].[Archivos] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);

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
        #endregion [Nuevo] Archivo Seguimiento

        #region [Nuevo] Archivo Gestion
        public void mtdAgregarPdfGestion(string strIdGestion, string strIdPlanAccion, string strNombreArchivo, string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO Gestion.ArchivosGestion ([IdGestion], [IdPlanAccion], [NombreArchivo], [IdUsuario], [FechaRegistro], [UrlArchivo], [ArchivoPDF]) VALUES ({0}, {1}, {2}, '{3}', GETDATE(), N'{4}', @PdfData)",
                    strIdGestion, strIdPlanAccion, IdUsuario.ToString().Trim(), strNombreArchivo, strUrlArchivo);

                cDataBase.mtdConectarSql();
                cDataBase.mtdEjecutarConsultaSQL(strConsulta, bPdfData);
                cDataBase.mtdDesconectarSql();
            }
            catch (Exception ex)
            {
                cDataBase.mtdDesconectarSql();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public byte[] mtdDescargarPdfGestion(string strNombreArchivo)
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [Gestion].[ArchivosGestion] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);

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
        #endregion [Nuevo] Archivo Gestion

        #endregion
    }
}