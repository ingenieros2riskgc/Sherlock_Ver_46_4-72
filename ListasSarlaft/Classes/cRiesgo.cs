using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;

namespace ListasSarlaft.Classes
{
    public class cRiesgo : cPropiedades
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private Object thisLock = new Object();
        cControl cControl = new cControl();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;

        private string[] strMonths = new string[12] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" },
            strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

        #endregion Variables Globales

        //public DataTable ReporteRiesgos(String IdCadenaValor, String IdMacroProceso, String IdProceso, String IdClasificacionRiesgo, String IdClasificacionGeneralRiesgo, String NombreRiesgoInherente, String NombreRiesgoResidual, String IdEmpresa, String numeroQuery, String IdRiesgo)
        //{
        //    DataTable dtInformacion = new DataTable();
        //    String condicion = "";
        //    try
        //    {
        //        if (numeroQuery == "3")
        //        {
        //            if (IdCadenaValor != "---")
        //            {
        //                condicion = "AND (a.IdCadenaValor = " + IdCadenaValor + ") ";
        //            }
        //            if (IdMacroProceso != "---")
        //            {
        //                if (condicion.Trim() == "")
        //                {
        //                    condicion = "AND (a.IdMacroProceso = " + IdMacroProceso + ") ";
        //                }
        //                else
        //                {
        //                    condicion += "AND (a.IdMacroProceso = " + IdMacroProceso + ") ";
        //                }
        //            }
        //            if (IdProceso != "---")
        //            {
        //                if (condicion.Trim() == "")
        //                {
        //                    condicion = "AND (a.IdProceso = " + IdProceso + ") ";
        //                }
        //                else
        //                {
        //                    condicion += "AND (a.IdProceso = " + IdProceso + ") ";
        //                }
        //            }
        //            if (IdEmpresa != "---")
        //            {
        //                if (condicion.Trim() == "")
        //                {
        //                    condicion = " AND a.IdEmpresa = '" + IdEmpresa + "'";
        //                }
        //                else
        //                {
        //                    condicion += " AND a.IdEmpresa = '" + IdEmpresa + "'";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (IdCadenaValor != "---")
        //            {
        //                condicion = "WHERE (a.IdCadenaValor = " + IdCadenaValor + ") ";
        //            }
        //            if (IdMacroProceso != "---")
        //            {
        //                if (condicion.Trim() == "")
        //                {
        //                    condicion = "WHERE (a.IdMacroProceso = " + IdMacroProceso + ") ";
        //                }
        //                else
        //                {
        //                    condicion += "AND (a.IdMacroProceso = " + IdMacroProceso + ") ";
        //                }
        //            }
        //            if (IdProceso != "---")
        //            {
        //                if (condicion.Trim() == "")
        //                {
        //                    condicion = "WHERE (a.IdProceso = " + IdProceso + ") ";
        //                }
        //                else
        //                {
        //                    condicion += "AND (a.IdProceso = " + IdProceso + ") ";
        //                }
        //            }
        //            if (IdEmpresa != "---")
        //            {
        //                if (condicion.Trim() == "")
        //                {
        //                    condicion = " WHERE a.IdEmpresa = '" + IdEmpresa + "'";
        //                }
        //                else
        //                {
        //                    condicion += " AND a.IdEmpresa = '" + IdEmpresa + "'";
        //                }
        //            }
        //        }
        //        cDataBase.conectar();
        //        switch (numeroQuery)
        //        { 
        //            case "1":
        //                //dtInformacion = cDataBase.ejecutarConsulta("select a.CodigoEvento,SUBSTRING(convert(varchar,a.FechaEvento,120),1,10) as FechaEvento,j.NombreResponsable,jo.NombreHijo as Cargo,jj.NombreHijo as Area,a.IdEmpresa as Empresa from Riesgos.NoHuboEventos a left join Listas.Usuarios l on a.IdUsuario = l.IdUsuario left join Parametrizacion.DetalleJerarquiaOrg j on l.IdJerarquia = j.idHijo left join Parametrizacion.JerarquiaOrganizacional jo on j.idHijo = jo.idHijo left join Parametrizacion.JerarquiaOrganizacional jj on jo.idPadre = jj.idHijo " + condicion + "ORDER BY FechaEvento");
        //                dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Riesgos.Riesgo.Codigo)) AS CodigoRiesgo, LTRIM(RTRIM(Riesgos.Riesgo.Nombre)) AS NombreRiesgo, Parametrizacion.JerarquiaOrganizacional.NombreHijo AS ResponsableRiesgo,LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Riesgo.FechaRegistro, 107), ''))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, ''))) AS ClasificacionRiesgo,LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo, ''))) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionParticularRiesgo.NombreClasificacionParticularRiesgo, ''))) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRiesgoOperativo.NombreTipoRiesgoOperativo, ''))) AS TipoRiesgoOperativo, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaCausas, ''))), '|', ',') AS Causas, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaConsecuencias, ''))), '|', ',') AS Consecuencias, LTRIM(RTRIM(ISNULL(Procesos.CadenaValor.NombreCadenaValor, ''))) AS CadenaValor, LTRIM(RTRIM(ISNULL(Procesos.Macroproceso.Nombre, ''))) AS Macroproceso, LTRIM(RTRIM(ISNULL(Procesos.Proceso.Nombre, ''))) AS Proceso, LTRIM(RTRIM(ISNULL(Procesos.Subproceso.Nombre, ''))) AS Subproceso, LTRIM(RTRIM(ISNULL(Procesos.Actividad.Nombre, ''))) AS Actividad, LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.NombreProbabilidad, ''))) AS FrecuenciaInherente,LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.ValorProbabilidad, ''))) AS FrecuenciaInherenteCualitativa, LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.NombreImpacto, ''))) AS ImpactoInherente,  LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.ValorImpacto, ''))) AS ImpactoInherenteCualitativo,LTRIM(RTRIM(ISNULL(Parametrizacion.RiesgoInherente.NombreRiesgoInherente, ''))) AS RiesgoInherente, LTRIM(RTRIM(ISNULL(pr.NombreProbabilidad, ''))) AS FrecuenciaResidual,LTRIM(RTRIM(ISNULL(pr.ValorProbabilidad, ''))) AS FrecuenciaResidualCualitativa,LTRIM(RTRIM(ISNULL(im.NombreImpacto, ''))) AS ImpactoResidual,LTRIM(RTRIM(ISNULL(im.ValorImpacto, ''))) AS ImpactoResidualCualitativo,LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) AS RiesgoResidual FROM Riesgos.Riesgo LEFT JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo ON Riesgos.Riesgo.IdClasificacionGeneralRiesgo = Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo ON Riesgos.Riesgo.IdClasificacionParticularRiesgo = Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo ON Riesgos.Riesgo.IdTipoRiesgoOperativo = Parametrizacion.TipoRiesgoOperativo.IdTipoRiesgoOperativo LEFT JOIN Procesos.CadenaValor ON Procesos.CadenaValor.IdCadenaValor = Riesgos.Riesgo.IdCadenaValor LEFT JOIN Procesos.Macroproceso ON Riesgos.Riesgo.IdMacroproceso = Procesos.Macroproceso.IdMacroProceso LEFT JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso LEFT JOIN Procesos.Subproceso ON Procesos.Subproceso.IdProceso = Riesgos.Riesgo.IdSubProceso LEFT JOIN Procesos.Actividad ON Riesgos.Riesgo.IdActividad = Procesos.Actividad.IdActividad LEFT JOIN Parametrizacion.Probabilidad ON Parametrizacion.Probabilidad.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad pr ON pr.IdProbabilidad = Riesgos.Riesgo.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto ON Parametrizacion.Impacto.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.Impacto im ON im.IdImpacto = Riesgos.Riesgo.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente ON Parametrizacion.RiesgoInherente.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad AND Parametrizacion.RiesgoInherente.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON Riesgos.Riesgo.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad 	AND Riesgos.Riesgo.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Riesgo.IdResponsableRiesgo " + condicion + "ORDER BY Riesgos.Riesgo.IdRiesgo");
        //                break;
        //            case "2":
        //                dtInformacion = cDataBase.ejecutarConsulta("select a.CodigoEvento, b.Descripcion as Empresa,c.NombreRegion,d.NombrePais,e.NombreDepartamento,f.NombreCiudad,g.NombreOficinaSucursal,a.DetalleUbicacion,a.DescripcionEvento,h.Descripcion as Servicio,i.SubDescripcion as Subservicio,substring(convert(varchar,a.FechaInicio,120),1,10) as FechaInicio,a.HoraInicio,substring(convert(varchar,a.FechaFinalizacion,120),1,10) as FechaFinalizacion,a.HoraFinalizacion,substring(convert(varchar,a.FechaDescubrimiento,120),1,10) as FechaDescubrimiento,a.HoraDescubrimiento,j.Descripcion as Canal,k.Descripcion as GeneradorEvento,l.NombreResponsable as Responsable,a.CuantiaPerdida,m.NombreCadenaValor as CadenaValor,n.Nombre as Macroproceso,o.Nombre as Proceso,p.Nombre as Subproceso,q.Nombre as Actividad,r.Descripcion as ClaseRiesgo,s.SubDescripcion as SubClaseRiesgo,t.NombreTipoPerdidaEvento,a.AfectaContinudad,u.Descripcion as Estado,a.Observaciones,SUBSTRING(convert(varchar,a.FechaEvento,120),1,10) as FechaEvento,j1.NombreResponsable as Originador,j2.NombreHijo as Cargo,j3.NombreHijo as Area from Riesgos.Eventos a left join Eventos.Empresa b on a.IdEmpresa = b.IdEmpresa left join Parametrizacion.Regiones c on a.IdRegion = c.IdRegion left join Parametrizacion.Paises d on a.IdPais = d.IdPais left join Parametrizacion.Departamentos e on a.IdDepartamento = e.IdDepartamento left join Parametrizacion.Ciudades f on a.IdCiudad = f.IdCiudad left join Parametrizacion.OficinaSucursal g on a.IdOficinaSucursal = g.IdOficinaSucursal left join Eventos.Servicio h on a.IdServicio = h.IdServicio left join Eventos.SubServicio i on a.IdSubServicio = i.IdSubServicio left join Eventos.Canal j on a.IdCanal = j.IdCanal left join Eventos.Generador k on a.IdGeneraEvento = k.IdGenerador left join Parametrizacion.DetalleJerarquiaOrg l on a.ResponsableEvento = l.idHijo left join Procesos.CadenaValor m on a.IdCadenaValor = m.IdCadenaValor left join Procesos.Macroproceso n on a.IdMacroproceso = n.IdMacroProceso left join Procesos.Proceso o on a.IdProceso = o.IdProceso left join Procesos.Subproceso p on a.IdSubProceso = p.IdSubproceso left join Procesos.Actividad q on a.IdActividad = q.IdActividad left join Eventos.Clase r on a.IdClase = r.IdClase left join Eventos.SubClase s on a.IdSubClase = s.IdSubClase left join Parametrizacion.TipoPerdidaEvento t on a.IdTipoPerdidaEvento = t.IdTipoPerdidaEvento left join Eventos.Estado u on a.IdEstado = u.IdEstado left join Listas.Usuarios v on a.IdUsuario = v.IdUsuario left join Parametrizacion.DetalleJerarquiaOrg j1 on v.IdJerarquia = j1.idHijo left join Parametrizacion.JerarquiaOrganizacional j2 on j1.idHijo = j2.idHijo left join Parametrizacion.JerarquiaOrganizacional j3 on j2.idPadre = j3.idHijo " + condicion + " ORDER BY a.CodigoEvento");                        
        //                break;
        //            case "3":
        //                dtInformacion = cDataBase.ejecutarConsulta("select a.CodigoEvento, b.Descripcion as Empresa,c.NombreRegion,d.NombrePais,e.NombreDepartamento,f.NombreCiudad,g.NombreOficinaSucursal,a.DetalleUbicacion,a.DescripcionEvento,h.Descripcion as Servicio,i.SubDescripcion as Subservicio,substring(convert(varchar,a.FechaInicio,120),1,10) as FechaInicio,a.HoraInicio,substring(convert(varchar,a.FechaFinalizacion,120),1,10) as FechaFinalizacion,a.HoraFinalizacion,substring(convert(varchar,a.FechaDescubrimiento,120),1,10) as FechaDescubrimiento,a.HoraDescubrimiento,j.Descripcion as Canal,k.Descripcion as GeneradorEvento,l.NombreResponsable as Responsable,a.CuantiaPerdida,m.NombreCadenaValor as CadenaValor,n.Nombre as Macroproceso,o.Nombre as Proceso,p.Nombre as Subproceso,q.Nombre as Actividad,r.Descripcion as ClaseRiesgo,s.SubDescripcion as SubClaseRiesgo,t.NombreTipoPerdidaEvento,a.AfectaContinudad,u.Descripcion as Estado,a.Observaciones,SUBSTRING(convert(varchar,a.FechaEvento,120),1,10) as FechaEvento,j1.NombreResponsable as Originador,j2.NombreHijo as Cargo,j3.NombreHijo as Area, Paa.IdPlanAccion, Paa.DescripcionAccion as PlanAccion, Paa.Responsable as ResponsablePlaAccion, Pad.NombreTipoRecursoPlanAccion,  Paa.ValorRecurso as ValorRecursoPlanAccion, Pab.NombreEstadoPlanAccion as EstadoPlanAccion, substring(CONVERT(varchar, Paa.FechaCompromiso, 102),1,10) AS FechaCompromisoPlanAccion,   Pac.NombreHijo as ResponsablePlanAccion from Riesgos.Eventos a left join Eventos.Empresa b on a.IdEmpresa = b.IdEmpresa left join Parametrizacion.Regiones c on a.IdRegion = c.IdRegion left join Parametrizacion.Paises d on a.IdPais = d.IdPais left join Parametrizacion.Departamentos e on a.IdDepartamento = e.IdDepartamento left join Parametrizacion.Ciudades f on a.IdCiudad = f.IdCiudad left join Parametrizacion.OficinaSucursal g on a.IdOficinaSucursal = g.IdOficinaSucursal left join Eventos.Servicio h on a.IdServicio = h.IdServicio left join Eventos.SubServicio i on a.IdSubServicio = i.IdSubServicio left join Eventos.Canal j on a.IdCanal = j.IdCanal left join Eventos.Generador k on a.IdGeneraEvento = k.IdGenerador left join Parametrizacion.DetalleJerarquiaOrg l on a.ResponsableEvento = l.idHijo left join Procesos.CadenaValor m on a.IdCadenaValor = m.IdCadenaValor left join Procesos.Macroproceso n on a.IdMacroproceso = n.IdMacroProceso left join Procesos.Proceso o on a.IdProceso = o.IdProceso left join Procesos.Subproceso p on a.IdSubProceso = p.IdSubproceso left join Procesos.Actividad q on a.IdActividad = q.IdActividad left join Eventos.Clase r on a.IdClase = r.IdClase left join Eventos.SubClase s on a.IdSubClase = s.IdSubClase left join Parametrizacion.TipoPerdidaEvento t on a.IdTipoPerdidaEvento = t.IdTipoPerdidaEvento left join Eventos.Estado u on a.IdEstado = u.IdEstado left join Listas.Usuarios v on a.IdUsuario = v.IdUsuario left join Parametrizacion.DetalleJerarquiaOrg j1 on v.IdJerarquia = j1.idHijo left join Parametrizacion.JerarquiaOrganizacional j2 on j1.idHijo = j2.idHijo left join Parametrizacion.JerarquiaOrganizacional j3 on j2.idPadre = j3.idHijo right join Riesgos.PlanesAccion Paa on Paa.IdRegistro = a.IdEvento left JOIN Parametrizacion.EstadoPlanAccion Pab ON Paa.IdEstadoPlanAccion = Pab.IdEstadoPlanAccion left JOIN Parametrizacion.JerarquiaOrganizacional Pac ON Paa.Responsable = Pac.idHijo left join Parametrizacion.TipoRecursoPlanAccion Pad on Paa.IdTipoRecursoPlanAccion = Pad.IdTipoRecursoPlanAccion where (Paa.IdControlUsuario = 6) " + condicion + " ORDER BY a.CodigoEvento");
        //                break;
        //            case "4":
        //                if (condicion.Trim() == "")
        //                {
        //                    condicion = "WHERE (Riesgos.PlanesAccion.IdControlUsuario = 3) ";
        //                }
        //                else
        //                {
        //                    condicion += "AND (Riesgos.PlanesAccion.IdControlUsuario = 3) ";
        //                }
        //                dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Riesgos.Riesgo.Codigo)) AS CodigoRiesgo, LTRIM(RTRIM(Riesgos.Riesgo.Nombre)) AS NombreRiesgo, Parametrizacion.JerarquiaOrganizacional.NombreHijo AS ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Riesgo.FechaRegistro, 107), ''))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, ''))) AS ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo, ''))) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionParticularRiesgo.NombreClasificacionParticularRiesgo, ''))) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRiesgoOperativo.NombreTipoRiesgoOperativo, ''))) AS TipoRiesgoOperativo, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaCausas, ''))), '|', ',') AS Causas, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaConsecuencias, ''))), '|', ',') AS Consecuencias, LTRIM(RTRIM(ISNULL(Procesos.CadenaValor.NombreCadenaValor, ''))) AS CadenaValor, LTRIM(RTRIM(ISNULL(Procesos.Macroproceso.Nombre, ''))) AS Macroproceso, LTRIM(RTRIM(ISNULL(Procesos.Proceso.Nombre, ''))) AS Proceso, LTRIM(RTRIM(ISNULL(Procesos.Subproceso.Nombre, ''))) AS Subproceso, LTRIM(RTRIM(ISNULL(Procesos.Actividad.Nombre, ''))) AS Actividad, LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.NombreProbabilidad, ''))) AS Frecuencia, LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.NombreImpacto, ''))) AS Impacto, LTRIM(RTRIM(ISNULL(Parametrizacion.RiesgoInherente.NombreRiesgoInherente, ''))) AS RiesgoInherente, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) AS RiesgoResidual, LTRIM(RTRIM(ISNULL(Riesgos.PlanesAccion.DescripcionAccion, ''))) AS DescripcionAccion, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRecursoPlanAccion.NombreTipoRecursoPlanAccion, ''))) AS NombreTipoRecursoPlanAccion, LTRIM(RTRIM(ISNULL(Riesgos.PlanesAccion.ValorRecurso, ''))) AS ValorRecurso, LTRIM(RTRIM(ISNULL(Parametrizacion.EstadoPlanAccion.NombreEstadoPlanAccion, ''))) AS NombreEstadoPlanAccion, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.PlanesAccion.FechaCompromiso, 107), ''))) AS FechaCompromiso, LTRIM(RTRIM(ISNULL(JOPA.NombreHijo, ''))) AS ResponsablePlanAccion FROM Riesgos.Riesgo LEFT JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo ON Riesgos.Riesgo.IdClasificacionGeneralRiesgo = Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo ON Riesgos.Riesgo.IdClasificacionParticularRiesgo = Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo ON Riesgos.Riesgo.IdTipoRiesgoOperativo = Parametrizacion.TipoRiesgoOperativo.IdTipoRiesgoOperativo LEFT JOIN Procesos.CadenaValor ON Procesos.CadenaValor.IdCadenaValor = Riesgos.Riesgo.IdCadenaValor LEFT JOIN Procesos.Macroproceso ON Riesgos.Riesgo.IdMacroproceso = Procesos.Macroproceso.IdMacroProceso LEFT JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso LEFT JOIN Procesos.Subproceso ON Procesos.Subproceso.IdProceso = Riesgos.Riesgo.IdSubProceso LEFT JOIN Procesos.Actividad ON Riesgos.Riesgo.IdActividad = Procesos.Actividad.IdActividad LEFT JOIN Parametrizacion.Probabilidad ON Parametrizacion.Probabilidad.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad LEFT JOIN Parametrizacion.Impacto ON Parametrizacion.Impacto.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente ON Parametrizacion.RiesgoInherente.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad AND Parametrizacion.RiesgoInherente.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON Riesgos.Riesgo.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND Riesgos.Riesgo.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Riesgo.IdResponsableRiesgo LEFT JOIN Riesgos.PlanesAccion ON Riesgos.Riesgo.IdRiesgo = Riesgos.PlanesAccion.IdRegistro LEFT JOIN Parametrizacion.TipoRecursoPlanAccion ON Riesgos.PlanesAccion.IdTipoRecursoPlanAccion = Parametrizacion.TipoRecursoPlanAccion.IdTipoRecursoPlanAccion LEFT JOIN Parametrizacion.EstadoPlanAccion ON Riesgos.PlanesAccion.IdEstadoPlanAccion = Parametrizacion.EstadoPlanAccion.IdEstadoPlanAccion LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS JOPA ON JOPA.idHijo = Riesgos.PlanesAccion.Responsable " + condicion + "ORDER BY Riesgos.Riesgo.IdRiesgo");
        //                break;
        //        }
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //    return dtInformacion;
        //}

        #region Cargue de mapas
        public DataTable loadInfoRiesgosMapaInherente(String IdProbabilidad, String IdImpacto,
            String IdCadenaValor, String IdMacroproceso, String IdProceso,
            String IdSubProceso, String IdClasificacionRiesgo, String IdClasificacionGeneralRiesgo,
            String IdClasificacionParticularRiesgo, string IdAreas, string strFechaHistorico,
            string strIdFactorRiesgoLAFT, string strIdEmpresa, string strNombreEmpresa, string strIdObjetivo)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables

            try
            {
                if (!string.IsNullOrEmpty(strFechaHistorico))
                {
                    #region Filtros de historico
                    condicion = string.Format("WHERE (RHR.IdProbabilidad = {0}) AND (RHR.IdImpacto = {1})", IdProbabilidad, IdImpacto);

                    #region Filtro Cadena Valor
                    if (IdCadenaValor != "---")
                        condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, IdCadenaValor);
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (IdMacroproceso != "---")
                        condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, IdMacroproceso);
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (IdProceso != "---")
                        condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, IdProceso);
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (IdSubProceso != "---")
                        condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, IdSubProceso);
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (IdClasificacionRiesgo != "---")
                        condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, IdClasificacionRiesgo);
                    #endregion Filtro ClasificacionRiesgo

                    #region Filtro ClasificacionGeneralRiesgo
                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = string.Format("{0} AND (RHR.IdClasificacionGeneralRiesgo = {1})", condicion, IdClasificacionGeneralRiesgo);
                    #endregion Filtro ClasificacionGeneralRiesgo

                    #region Filtro ClasificacionParticularRiesgo
                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = string.Format("{0} AND (RHR.IdClasificacionParticularRiesgo = {1})", condicion, IdClasificacionParticularRiesgo);
                    #endregion Filtro ClasificacionParticularRiesgo

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(strFechaHistorico))
                    {
                        strFechaIni = mtdConvertirFecha(strFechaHistorico, 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(strFechaHistorico, 2) + " 23:59:59:998";

                        strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);

                        condicion += strFechaFinal;
                    }
                    #endregion Fechas Desde y Hasta

                    #region Filtro Nombre Empresa
                    if (strNombreEmpresa != "---")
                        condicion = string.Format("{0} AND (RHR.Empresa = '{1}')", condicion, strNombreEmpresa);
                    #endregion Filtro Nombre Empresa

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT DISTINCT RHR.IdRiesgo AS IdRiesgo, RHR.IdProbabilidad, RHR.IdImpacto, RHR.CodigoRiesgo AS Codigo, RHR.NombreRiesgo AS Nombre, RHR.FechaRegistroRiesgo AS FechaRegistro, PRI.NombreRiesgoInherente, PRI.Color, PP.NombreProbabilidad, PIm.NombreImpacto, PCR.NombreClasificacionRiesgo");
                        string strFromNormal = string.Format("FROM Riesgos.HistoricoRiesgo AS RHR INNER JOIN Parametrizacion.RiesgoInherente AS PRI ON RHR.IdProbabilidad = PRI.IdProbabilidad AND RHR.IdImpacto = PRI.IdImpacto INNER JOIN Parametrizacion.Probabilidad AS PP ON RHR.IdProbabilidad = PP.IdProbabilidad INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpacto = PIm.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RHR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo {2} {0} ", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} ", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelHistorico = string.Format("SELECT DISTINCT RHR.IdRiesgo AS IdRiesgo, RHR.IdProbabilidad, RHR.IdImpacto, RHR.CodigoRiesgo AS Codigo, RHR.NombreRiesgo AS Nombre, RHR.FechaRegistroRiesgo AS FechaRegistro, PRI.NombreRiesgoInherente, PRI.Color, PP.NombreProbabilidad, PIm.NombreImpacto, PCR.NombreClasificacionRiesgo");
                        string strFromHistorico = string.Format("FROM Riesgos.HistoricoRiesgo AS RHR INNER JOIN Parametrizacion.RiesgoInherente AS PRI ON RHR.IdProbabilidad = PRI.IdProbabilidad AND RHR.IdImpacto = PRI.IdImpacto INNER JOIN Parametrizacion.Probabilidad AS PP ON RHR.IdProbabilidad = PP.IdProbabilidad INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpacto = PIm.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RHR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} ", strSelHistorico, strFromHistorico);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros de historico

                    
                }
                else
                {
                    #region Filtros
                    condicion = string.Format("WHERE (Riesgos.Riesgo.Anulado = 0) AND (Riesgos.Riesgo.IdProbabilidad = {0}) AND (Riesgos.Riesgo.IdImpacto = {1})", IdProbabilidad, IdImpacto);

                    if (IdCadenaValor != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdCadenaValor = " + IdCadenaValor + ")";
                    if (IdMacroproceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdMacroproceso = " + IdMacroproceso + ")";
                    if (IdProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdProceso = " + IdProceso + ")";
                    if (IdSubProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdSubProceso = " + IdSubProceso + ")";
                    if (IdClasificacionRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")";
                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")";
                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")";

                    #region Filtro Factor Riesgos LAFT
                    if (strIdFactorRiesgoLAFT != "---")
                    {
                        condicion = condicion + " AND ('" + strIdFactorRiesgoLAFT + "' IN (SELECT COL1 FROM Procesos.FnSplitTable(Riesgos.Riesgo.ListaFactorRiesgoLAFT,'|'))) ";
                    }
                    #endregion Filtro Factor Riesgos LAFT

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Filtro Nombre Empresa
                    if (strIdEmpresa != "---" && strIdEmpresa != "0")
                    {
                        if (string.IsNullOrEmpty(strFrom))
                            strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";

                        if (strIdEmpresa == "3")
                        {
                            if (string.IsNullOrEmpty(condicion))
                                condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa = {0}", strIdEmpresa);
                            else
                                condicion = string.Format("{0} AND (Procesos.Proceso.IdEmpresa = {1})", condicion, strIdEmpresa);
                        }
                        else
                        {
                            strIdEmpresa += ",3";
                            if (string.IsNullOrEmpty(condicion))
                                condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa in ({0})", strIdEmpresa);
                            else
                                condicion = string.Format("{0} AND (Procesos.Proceso.IdEmpresa in ({1}))", condicion, strIdEmpresa);
                        }
                    }
                    #endregion Filtro Nombre Empresa
                    string strSelNormal = string.Empty;
                    string strFromNormal = string.Empty;
                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = Riesgos.Riesgo.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        strSelNormal  = string.Format("SELECT Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.IdProbabilidad, Riesgos.Riesgo.IdImpacto, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.FechaRegistro, Parametrizacion.RiesgoInherente.NombreRiesgoInherente, Parametrizacion.RiesgoInherente.Color, Parametrizacion.Probabilidad.NombreProbabilidad, Parametrizacion.Impacto.NombreImpacto, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo");
                        strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.RiesgoInherente ON Riesgos.Riesgo.IdProbabilidad = Parametrizacion.RiesgoInherente.IdProbabilidad AND Riesgos.Riesgo.IdImpacto = Parametrizacion.RiesgoInherente.IdImpacto INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidad = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpacto = Parametrizacion.Impacto.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        strSelNormal = string.Format("SELECT Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.IdProbabilidad, Riesgos.Riesgo.IdImpacto, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.FechaRegistro, Parametrizacion.RiesgoInherente.NombreRiesgoInherente, Parametrizacion.RiesgoInherente.Color, Parametrizacion.Probabilidad.NombreProbabilidad, Parametrizacion.Impacto.NombreImpacto, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo");
                        strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.RiesgoInherente ON Riesgos.Riesgo.IdProbabilidad = Parametrizacion.RiesgoInherente.IdProbabilidad AND Riesgos.Riesgo.IdImpacto = Parametrizacion.RiesgoInherente.IdImpacto INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidad = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpacto = Parametrizacion.Impacto.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos

                    #endregion Filtros

                     /*strSelNormal = string.Format("SELECT Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.IdProbabilidad, Riesgos.Riesgo.IdImpacto, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.FechaRegistro, Parametrizacion.RiesgoInherente.NombreRiesgoInherente, Parametrizacion.RiesgoInherente.Color, Parametrizacion.Probabilidad.NombreProbabilidad, Parametrizacion.Impacto.NombreImpacto, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo");
                     strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.RiesgoInherente ON Riesgos.Riesgo.IdProbabilidad = Parametrizacion.RiesgoInherente.IdProbabilidad AND Riesgos.Riesgo.IdImpacto = Parametrizacion.RiesgoInherente.IdImpacto INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidad = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpacto = Parametrizacion.Impacto.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo {0} {1}", strFrom, condicion);

                    strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);*/
                }

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

        public DataTable loadInfoRiesgosMapaResidual(String IdProbabilidadResidual,
            String IdImpactoResidual, String IdCadenaValor, String IdMacroproceso,
            String IdProceso, String IdSubProceso, String IdClasificacionRiesgo,
            String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo,
            string IdAreas, string strFechaHistorico, string strIdFactorRiesgoLAFT,
            string strIdEmpresa, string strNombreEmpresa, string strIdObjetivo)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables

            try
            {
                if (!string.IsNullOrEmpty(strFechaHistorico))
                {
                    #region Filtros de historico
                    condicion = string.Format("WHERE (RHR.IdProbabilidadResidual = {0}) AND (RHR.IdImpactoResidual = {1})", IdProbabilidadResidual, IdImpactoResidual);

                    #region Filtro Cadena Valor
                    if (IdCadenaValor != "---")
                        condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, IdCadenaValor);
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (IdMacroproceso != "---")
                        condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, IdMacroproceso);
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (IdProceso != "---")
                        condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, IdProceso);
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (IdSubProceso != "---")
                        condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, IdSubProceso);
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (IdClasificacionRiesgo != "---")
                        condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, IdClasificacionRiesgo);
                    #endregion Filtro ClasificacionRiesgo

                    #region Filtro ClasificacionGeneralRiesgo
                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = string.Format("{0} AND (RHR.IdClasificacionGeneralRiesgo = {1})", condicion, IdClasificacionGeneralRiesgo);
                    #endregion Filtro ClasificacionGeneralRiesgo

                    #region Filtro ClasificacionParticularRiesgo
                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = string.Format("{0} AND (RHR.IdClasificacionParticularRiesgo = {1})", condicion, IdClasificacionParticularRiesgo);
                    #endregion Filtro ClasificacionParticularRiesgo

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(strFechaHistorico))
                    {
                        strFechaIni = mtdConvertirFecha(strFechaHistorico, 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(strFechaHistorico, 2) + " 23:59:59:998";

                        strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);

                        condicion += strFechaFinal;
                    }
                    #endregion Fechas Desde y Hasta

                    #region Filtro Nombre Empresa
                    if (strNombreEmpresa != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.Empresa = '{0}'", strNombreEmpresa);
                        else
                            condicion = string.Format("{0} AND (RHR.Empresa = '{1}')", condicion, strNombreEmpresa);
                    }
                    #endregion Filtro Nombre Empresa
                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT DISTINCT RHR.IdRiesgo AS IdRiesgo, RHR.IdProbabilidadResidual  AS IdProbabilidad, RHR.IdImpactoResidual AS IdImpacto, RHR.CodigoRiesgo AS Codigo, RHR.NombreRiesgo AS Nombre, RHR.FechaRegistroRiesgo AS FechaRegistro, PRI.NombreRiesgoInherente, PRI.Color, PP.NombreProbabilidad, PIm.NombreImpacto, PCR.NombreClasificacionRiesgo");
                        string strFromNormal = string.Format("FROM Riesgos.HistoricoRiesgo AS RHR INNER JOIN Parametrizacion.RiesgoInherente AS PRI ON RHR.IdProbabilidadResidual = PRI.IdProbabilidad AND RHR.IdImpactoResidual = PRI.IdImpacto INNER JOIN Parametrizacion.Probabilidad AS PP ON RHR.IdProbabilidadResidual = PP.IdProbabilidad INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpactoResidual = PIm.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RHR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo {2} {0} ", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} ", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelHistorico = string.Format("SELECT DISTINCT RHR.IdRiesgo AS IdRiesgo, RHR.IdProbabilidadResidual  AS IdProbabilidad, RHR.IdImpactoResidual AS IdImpacto, RHR.CodigoRiesgo AS Codigo, RHR.NombreRiesgo AS Nombre, RHR.FechaRegistroRiesgo AS FechaRegistro, PRI.NombreRiesgoInherente, PRI.Color, PP.NombreProbabilidad, PIm.NombreImpacto, PCR.NombreClasificacionRiesgo");
                        string strFromHistorico = string.Format("FROM Riesgos.HistoricoRiesgo AS RHR INNER JOIN Parametrizacion.RiesgoInherente AS PRI ON RHR.IdProbabilidadResidual = PRI.IdProbabilidad AND RHR.IdImpactoResidual = PRI.IdImpacto INNER JOIN Parametrizacion.Probabilidad AS PP ON RHR.IdProbabilidadResidual = PP.IdProbabilidad INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpactoResidual = PIm.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RHR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}", strSelHistorico, strFromHistorico);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros de historico

                    
                }
                else
                {
                    #region Filtros
                    condicion = string.Format("WHERE (Riesgos.Riesgo.Anulado = 0) AND (Riesgos.Riesgo.IdProbabilidadResidual = {0}) AND (Riesgos.Riesgo.IdImpactoResidual = {1})", IdProbabilidadResidual, IdImpactoResidual);

                    if (IdCadenaValor != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdCadenaValor = " + IdCadenaValor + ")";
                    if (IdMacroproceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdMacroproceso = " + IdMacroproceso + ")";
                    if (IdProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdProceso = " + IdProceso + ")";
                    if (IdSubProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdSubProceso = " + IdSubProceso + ")";
                    if (IdClasificacionRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")";
                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")";
                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")";

                    #region Filtro Factor Riesgos LAFT
                    if (strIdFactorRiesgoLAFT != "---")
                    {
                        condicion = condicion + " AND ('" + strIdFactorRiesgoLAFT + "' IN (SELECT COL1 FROM Procesos.FnSplitTable(Riesgos.Riesgo.ListaFactorRiesgoLAFT,'|'))) ";
                    }
                    #endregion Filtro Factor Riesgos LAFT

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Filtro Nombre Empresa
                    if (strIdEmpresa != "---" && strIdEmpresa != "0")
                    {
                        if (string.IsNullOrEmpty(strFrom))
                            strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";

                        if (strIdEmpresa == "3")
                        {
                            if (string.IsNullOrEmpty(condicion))
                                condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa = {0}", strIdEmpresa);
                            else
                                condicion = string.Format("{0} AND (Procesos.Proceso.IdEmpresa = {1})", condicion, strIdEmpresa);
                        }
                        else
                        {
                            strIdEmpresa += ",3";
                            if (string.IsNullOrEmpty(condicion))
                                condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa in ({0})", strIdEmpresa);
                            else
                                condicion = string.Format("{0} AND (Procesos.Proceso.IdEmpresa in ({1}))", condicion, strIdEmpresa);
                        }

                    }
                    #endregion Filtro Nombre Empresa
                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = Riesgos.Riesgo.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.IdProbabilidadResidual AS IdProbabilidad, Riesgos.Riesgo.IdImpactoResidual AS IdImpacto, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.FechaRegistro, Parametrizacion.RiesgoInherente.NombreRiesgoInherente, Parametrizacion.RiesgoInherente.Color, Parametrizacion.Probabilidad.NombreProbabilidad, Parametrizacion.Impacto.NombreImpacto, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.RiesgoInherente ON Riesgos.Riesgo.IdProbabilidadResidual = Parametrizacion.RiesgoInherente.IdProbabilidad AND Riesgos.Riesgo.IdImpactoResidual = Parametrizacion.RiesgoInherente.IdImpacto INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelNormal = string.Format("SELECT Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.IdProbabilidadResidual AS IdProbabilidad, Riesgos.Riesgo.IdImpactoResidual AS IdImpacto, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.FechaRegistro, Parametrizacion.RiesgoInherente.NombreRiesgoInherente, Parametrizacion.RiesgoInherente.Color, Parametrizacion.Probabilidad.NombreProbabilidad, Parametrizacion.Impacto.NombreImpacto, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.RiesgoInherente ON Riesgos.Riesgo.IdProbabilidadResidual = Parametrizacion.RiesgoInherente.IdProbabilidad AND Riesgos.Riesgo.IdImpactoResidual = Parametrizacion.RiesgoInherente.IdImpacto INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros

                    

                }

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
        #endregion Cargue de mapas

        #region CBL
        public DataTable loadCBLRiesgoAsociadoLA()
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

        public DataTable loadCBLFactorRiesgoLAFT()
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

        public DataTable loadCBLCausas()
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

        public DataTable loadCBLConsecuencias()
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

        public DataTable loadCBLTratamiento()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTratamiento, NombreTratamiento FROM Parametrizacion.Tratamiento ORDER BY NombreTratamiento");
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

        public DataTable mtdLoadCausas(string strCausas)
        {
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                strConsulta = string.Format("SELECT IdCausas, NombreCausas FROM Parametrizacion.Causas WHERE IdCausas IN ({0}) ORDER BY NombreCausas", strCausas);

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

        public DataTable mtdLoadConsecuencias(string strConsecuencia)
        {
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                strConsulta = string.Format("SELECT IdConsecuencia, NombreConsecuencia FROM Parametrizacion.Consecuencia WHERE IdConsecuencia IN ({0}) ORDER BY NombreConsecuencia", strConsecuencia);

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
        #endregion CBL

        #region DDL

        public DataTable loadDDLTipoRecursoPlanAccion()
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

        public DataTable loadDDLEstadoPlanAccion()
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

        public DataTable loadDDLPlanes()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdPlan,Nombre from Gestion.PlanEstrategico order by IdPlan");
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

        public DataTable loadDDLObjetivos(String IdPlan)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                //dtInformacion = cDataBase.ejecutarConsulta("SELECT IdObjetivos, NombreObjetivos FROM Parametrizacion.Objetivos");
                //Camilo 12/02/2014
                //dtInformacion = cDataBase.ejecutarConsulta("SELECT IdObjetivo AS IdObjetivos, Descripcion AS NombreObjetivos FROM Gestion.ObjetivosEstrategicos");
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdObjetivo AS IdObjetivos, Descripcion AS NombreObjetivos FROM Gestion.ObjetivosEstrategicos where IdPlan = '" + IdPlan + "'");
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

        public DataTable loadDDLTipoLegislacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoLegislacion, NombreTipoLegislacion FROM Riesgos.TipoLegislacion ORDER BY NombreTipoLegislacion");
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

        public DataTable loadDDLEstadoLegislacion()
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

        public DataTable loadDDLNivelResponsable()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdNivelResponsable, NombreNivelResponsable FROM Riesgos.NivelResponsable ORDER BY NombreNivelResponsable");
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

        //Codigo: 16/12/2013 - Proveedor: Camilo Aponte
        public DataTable loadDDLLineaNegocio()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdLineaNegocio, Descripcion FROM Eventos.LineaNegocio ORDER BY Descripcion");
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

        //Codigo: 16/12/2013 - Proveedor: Camilo Aponte
        public DataTable loadDDLSubLineaNegocio(String IdLineaNegocio)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdSubLineaNegocio, SubDescripcion FROM Eventos.SubLineaNegocio WHERE (IdLineaNegocio = " + IdLineaNegocio + ") ORDER BY SubDescripcion");
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

        public DataTable loadDDLTipoEventoOperativo()
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

        public DataTable loadDDLRiesgoAsociadoOperativo()
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

        public DataTable loadDDLRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdRiesgo, Nombre FROM Riesgos.Riesgo WHERE (Anulado = 0)");
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

        public DataTable loadDDLFactorRiesgoOperativo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdFactorRiesgoOperativo, NombreFactorRiesgoOperativo FROM Parametrizacion.FactorRiesgoOperativo ORDER BY NombreFactorRiesgoOperativo");
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

        public DataTable loadDDLTipoRiesgoOperativo(String IdFactorRiesgoOperativo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoRiesgoOperativo, NombreTipoRiesgoOperativo FROM Parametrizacion.TipoRiesgoOperativo WHERE (IdFactorRiesgoOperativo = " + IdFactorRiesgoOperativo + ") ORDER BY NombreTipoRiesgoOperativo");
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

        public DataTable loadDDLProbabilidad()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdProbabilidad, NombreProbabilidad FROM Parametrizacion.Probabilidad ORDER BY ValorProbabilidad DESC");
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

        public DataTable loadDDLImpacto()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdImpacto, NombreImpacto FROM Parametrizacion.Impacto ORDER BY ValorImpacto");
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

        public DataTable loadDDLCadenaValor()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdCadenaValor, NombreCadenaValor FROM Procesos.CadenaValor where Estado = 1 ORDER BY IdCadenaValor");
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
        public DataTable loadDDLAreas()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdArea],[NombreArea] FROM [Parametrizacion].[Area]");
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
        public DataTable loadDDLMacroproceso(String IdCadenaValor)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdMacroProceso AS IdMacroproceso, Nombre AS NombreMacroproceso FROM Procesos.Macroproceso WHERE (IdCadenaValor = " + IdCadenaValor + " and Estado = 1) ORDER BY Nombre");
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

        public DataTable loadDDLProceso(String IdMacroproceso)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdProceso, Nombre AS NombreProceso FROM Procesos.Proceso WHERE (IdMacroProceso = " + IdMacroproceso + " and Estado = 1) ORDER BY Nombre");
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

        public DataTable loadDDLSubProceso(String IdProceso)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdSubproceso AS IdSubProceso, Nombre AS NombreSubProceso FROM Procesos.Subproceso WHERE (IdProceso = " + IdProceso + " and Estado = 1) ORDER BY Nombre");
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

        public DataTable loadDDLActividad(String IdSubproceso)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdActividad, Nombre AS NombreActividad FROM Procesos.Actividad WHERE (IdSubproceso = " + IdSubproceso + ") ORDER BY Nombre");
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

        public DataTable loadDDLRegion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdRegion, NombreRegion FROM Parametrizacion.Regiones ORDER BY NombreRegion");
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

        public DataTable loadDDLServicio()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdServicio, Descripcion FROM Eventos.Servicio ORDER BY Descripcion");
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

        public DataTable loadDDLSubServicio(String IdServicio)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdSubServicio, SubDescripcion FROM Eventos.SubServicio WHERE (IdServicio = " + IdServicio + ") ORDER BY SubDescripcion");
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

        public DataTable loadDDLSubClaseRiesgo(String IdClase)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdSubClase, SubDescripcion FROM Eventos.SubClase WHERE (IdClase = " + IdClase + ") ORDER BY SubDescripcion");
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

        public DataTable loadDDLClaseRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdClase, Descripcion FROM Eventos.Clase ORDER BY Descripcion");
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

        public DataTable loadDDLCanal()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdCanal, Descripcion FROM Eventos.Canal ORDER BY Descripcion");
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

        public DataTable loadDDLGenerador()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdGenerador, Descripcion FROM Eventos.Generador ORDER BY Descripcion");
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

        public DataTable loadDDLEstado()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdEstado, Descripcion FROM Eventos.Estado ORDER BY Descripcion");
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

        public DataTable loadDDLPais(String IdRegion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdPais, NombrePais FROM Parametrizacion.Paises WHERE (IdRegion = " + IdRegion + ") ORDER BY NombrePais");
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

        public DataTable loadDDLDepartamento(String IdPais)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdDepartamento, NombreDepartamento FROM Parametrizacion.Departamentos WHERE (IdPais = " + IdPais + ") ORDER BY NombreDepartamento");
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

        public DataTable loadDDLCiudad(String IdDepartamento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdCiudad, NombreCiudad FROM Parametrizacion.Ciudades WHERE (IdDepartamento = " + IdDepartamento + ") ORDER BY NombreCiudad");
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

        public DataTable loadDDLOficinaSucursal(String IdCiudad)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdOficinaSucursal, NombreOficinaSucursal FROM Parametrizacion.OficinaSucursal WHERE (IdCiudad = " + IdCiudad + ") ORDER BY NombreOficinaSucursal");
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

        public DataTable loadDDLClasificacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdClasificacionRiesgo, NombreClasificacionRiesgo FROM Parametrizacion.ClasificacionRiesgo ORDER BY NombreClasificacionRiesgo");
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

        public DataTable loadDDLClasificacionGeneral(String IdClasificacionRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdClasificacionGeneralRiesgo, NombreClasificacionGeneralRiesgo FROM Parametrizacion.ClasificacionGeneralRiesgo WHERE (IdClasificacionRiesgo = " + IdClasificacionRiesgo + ") ORDER BY NombreClasificacionGeneralRiesgo");
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

        public DataTable loadDDLClasificacionParticular(String IdClasificacionGeneralRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdClasificacionParticularRiesgo, NombreClasificacionParticularRiesgo FROM Parametrizacion.ClasificacionParticularRiesgo WHERE (IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ") ORDER BY NombreClasificacionParticularRiesgo");
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

        public DataTable mtdLoadEmpresa(bool booEstado)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("SELECT IdEmpresa, Descripcion FROM [Eventos].[Empresa] WHERE Activo = {0} ORDER BY Descripcion", booEstado ? 1 : 0);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        #endregion DDL

        #region Load

        public DataTable loadInfoEventoRiesgo(String IdRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Eventos.IdEvento, Riesgos.Eventos.CodigoEvento, Riesgos.Eventos.DescripcionEvento, Riesgos.Eventos.FechaDescubrimiento, Riesgos.Eventos.ValorRecuperadoTotal FROM Riesgos.Eventos INNER JOIN Riesgos.EventoRiesgo ON Riesgos.Eventos.IdEvento = Riesgos.EventoRiesgo.IdEvento WHERE (Riesgos.EventoRiesgo.IdRiesgo = " + IdRiesgo + ")");
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

        public DataTable loadInfoResponsableRiesgo(String IdRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdResponsableRiesgo, Responsable FROM Riesgos.RelacionResponsableRiesgo WHERE (IdRiesgo = " + IdRiesgo + ")");
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

        public DataTable loadInfoPlanAccionRiesgo(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.PlanesAccion.IdPlanAccion, Riesgos.PlanesAccion.DescripcionAccion, Riesgos.PlanesAccion.Responsable, Riesgos.PlanesAccion.IdTipoRecursoPlanAccion, Riesgos.PlanesAccion.ValorRecurso, Parametrizacion.EstadoPlanAccion.IdEstadoPlanAccion, Parametrizacion.EstadoPlanAccion.NombreEstadoPlanAccion, REPLACE(CONVERT(varchar, Riesgos.PlanesAccion.FechaCompromiso, 102), '.', '-') AS FechaCompromiso, Parametrizacion.JerarquiaOrganizacional.NombreHijo FROM Riesgos.PlanesAccion INNER JOIN Parametrizacion.EstadoPlanAccion ON Riesgos.PlanesAccion.IdEstadoPlanAccion = Parametrizacion.EstadoPlanAccion.IdEstadoPlanAccion INNER JOIN Parametrizacion.JerarquiaOrganizacional ON Riesgos.PlanesAccion.Responsable = Parametrizacion.JerarquiaOrganizacional.idHijo WHERE (Riesgos.PlanesAccion.IdControlUsuario = 3) AND (Riesgos.PlanesAccion.IdRegistro = " + IdRegistro + ")");
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

        public DataTable loadInfoComentarioPlanAccion(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Comentarios.IdComentario, Riesgos.Comentarios.NombreUsuario, Riesgos.Comentarios.FechaRegistro, LTRIM(RTRIM(SUBSTRING(Riesgos.Comentarios.Comentario, 1, 20))) + '...' AS ComentarioCorto, Riesgos.Comentarios.Comentario FROM Riesgos.Comentarios INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Comentarios.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Parametrizacion.ControlesUsuario.IdControlUsuario = 4) AND (Riesgos.Comentarios.IdRegistro = " + IdRegistro + ")");
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

        public DataTable loadInfoArchivoPlanAccion(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Archivos.IdArchivo, Riesgos.Archivos.NombreUsuario, Riesgos.Archivos.FechaRegistro, Riesgos.Archivos.UrlArchivo FROM Riesgos.Archivos INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Archivos.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Riesgos.Archivos.IdRegistro = " + IdRegistro + ") AND (Parametrizacion.ControlesUsuario.IdControlUsuario = 4)");
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

        public DataTable loadInfoObjetivoRiesgo(String IdRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.ObjetivosRiesgo.IdObjetivosRiesgo, Riesgos.ObjetivosRiesgo.IdRiesgo, Riesgos.ObjetivosRiesgo.IdObjetivos, Gestion.ObjetivosEstrategicos.Descripcion AS NombreObjetivos, Riesgos.ObjetivosRiesgo.IdUsuario, Riesgos.ObjetivosRiesgo.FechaRegistro FROM Riesgos.ObjetivosRiesgo INNER JOIN Gestion.ObjetivosEstrategicos ON Riesgos.ObjetivosRiesgo.IdObjetivos = Gestion.ObjetivosEstrategicos.IdObjetivo WHERE (Riesgos.ObjetivosRiesgo.IdRiesgo = " + IdRiesgo + ")");
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

        public DataTable loadInfoComentarioLegislacion(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Comentarios.IdComentario, Riesgos.Comentarios.NombreUsuario, Riesgos.Comentarios.FechaRegistro, LTRIM(RTRIM(SUBSTRING(Riesgos.Comentarios.Comentario, 1, 20))) + '...' AS ComentarioCorto, Riesgos.Comentarios.Comentario FROM Riesgos.Comentarios INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Comentarios.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Parametrizacion.ControlesUsuario.IdControlUsuario = 2) AND (Riesgos.Comentarios.IdRegistro = " + IdRegistro + ")");
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

        public DataTable loadInfoArchivoLegislacion(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Archivos.IdArchivo, Riesgos.Archivos.NombreUsuario, Riesgos.Archivos.FechaRegistro, Riesgos.Archivos.UrlArchivo FROM Riesgos.Archivos INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Archivos.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Riesgos.Archivos.IdRegistro = " + IdRegistro + ") AND (Parametrizacion.ControlesUsuario.IdControlUsuario = 2)");
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

        public DataTable loadInfoLegislacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Legislacion.IdLegislacion, Riesgos.Legislacion.CodigoLegislacion, Riesgos.Legislacion.NombreLegislacion, Riesgos.TipoLegislacion.IdTipoLegislacion, Riesgos.TipoLegislacion.NombreTipoLegislacion, Riesgos.Legislacion.DescripcionLegislacion, REPLACE(CONVERT(varchar, Riesgos.Legislacion.FechaVigenciaDesde, 111), '/', '-') AS FechaVigenciaDesde, (CASE WHEN REPLACE(CONVERT(varchar, Riesgos.Legislacion.FechaVigenciaHasta, 111), '/', '-') = '1900-01-01' THEN '' ELSE REPLACE(CONVERT(varchar, Riesgos.Legislacion.FechaVigenciaHasta, 111), '/', '-') END) AS FechaVigenciaHasta, Riesgos.Legislacion.Actualizacion, LTRIM(RTRIM(Listas.Usuarios.Nombres)) + ' ' + LTRIM(RTRIM(Listas.Usuarios.Apellidos)) AS Usuario, Riesgos.Legislacion.FechaRegistro, (CASE WHEN REPLACE(CONVERT(varchar, Riesgos.Legislacion.FechaCierre, 111), '/', '-') = '1900-01-01' THEN '' ELSE REPLACE(CONVERT(varchar, Riesgos.Legislacion.FechaCierre, 111), '/', '-') END) AS FechaCierre, Riesgos.Legislacion.IdEstadoLegislacion, Riesgos.Legislacion.IdResponsable, ISNULL(Parametrizacion.JerarquiaOrganizacional.NombreHijo,'') AS NombreHijo FROM Riesgos.TipoLegislacion INNER JOIN Riesgos.Legislacion ON Riesgos.TipoLegislacion.IdTipoLegislacion = Riesgos.Legislacion.IdTipoLegislacion INNER JOIN Listas.Usuarios ON Riesgos.Legislacion.IdUsuario = Listas.Usuarios.IdUsuario LEFT JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Legislacion.IdResponsable");
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

        public DataTable loadCodigoLegislacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdLegislacion+1 AS NumRegistros FROM Riesgos.Legislacion ORDER BY IdLegislacion DESC");
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

        public DataTable loadInfoResponsableRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.ResponsableRiesgo.IdResponsableRiesgo, Riesgos.ResponsableRiesgo.CodigoResponsableRiesgo, Riesgos.ResponsableRiesgo.NombreResponsableRiesgo, Riesgos.NivelResponsable.IdNivelResponsable, Riesgos.NivelResponsable.NombreNivelResponsable, Riesgos.ResponsableRiesgo.Email, Riesgos.ResponsableRiesgo.PerteneceURS, LTRIM(RTRIM(Listas.Usuarios.Nombres)) + ' ' + LTRIM(RTRIM(Listas.Usuarios.Apellidos)) AS Usuario, Riesgos.ResponsableRiesgo.FechaRegistro FROM Riesgos.ResponsableRiesgo INNER JOIN Riesgos.NivelResponsable ON Riesgos.ResponsableRiesgo.IdNivelResponsable = Riesgos.NivelResponsable.IdNivelResponsable INNER JOIN Listas.Usuarios ON Riesgos.ResponsableRiesgo.IdUsuario = Listas.Usuarios.IdUsuario");
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

        public DataTable loadCodigoResponsableRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdResponsableRiesgo+1 AS NumRegistros FROM Riesgos.ResponsableRiesgo ORDER BY IdResponsableRiesgo DESC");
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

        public DataTable loadInfoComentarioRiesgo(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Comentarios.IdComentario, Riesgos.Comentarios.NombreUsuario, Riesgos.Comentarios.FechaRegistro, LTRIM(RTRIM(SUBSTRING(Riesgos.Comentarios.Comentario, 1, 20))) + '...' AS ComentarioCorto, Riesgos.Comentarios.Comentario FROM Riesgos.Comentarios INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Comentarios.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Parametrizacion.ControlesUsuario.IdControlUsuario = 3) AND (Riesgos.Comentarios.IdRegistro = " + IdRegistro + ")");
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

        public DataTable loadInfoArchivoRiesgo(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Archivos.IdArchivo, Riesgos.Archivos.NombreUsuario, Riesgos.Archivos.FechaRegistro, Riesgos.Archivos.UrlArchivo FROM Riesgos.Archivos INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Archivos.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Riesgos.Archivos.IdRegistro = " + IdRegistro + ") AND (Parametrizacion.ControlesUsuario.IdControlUsuario = 3)");
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

        public DataTable loadCodigoRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdRiesgo+1 AS NumRegistros FROM Riesgos.Riesgo ORDER BY IdRiesgo DESC");
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

        public DataTable loadLBCadenaValor()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdMacroProceso AS IdMacroproceso, IdCadenaValor, Nombre AS NombreMacroproceso FROM Procesos.Macroproceso ORDER BY IdCadenaValor");
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

        public DataTable loadInfoRiesgo(String IdRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Codigo, Descripcion FROM Riesgos.Riesgo WHERE (IdRiesgo = " + IdRiesgo + ")");
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

        public DataTable loadInfoRiesgoAnulado()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.RiesgoAnulado.IdRiesgoAnulado, Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.Descripcion, Riesgos.RiesgoAnulado.MotivoAnulacion, Riesgos.RiesgoAnulado.FechaAnulacion, LTRIM(RTRIM(Listas.Usuarios.Nombres)) + ' ' + LTRIM(RTRIM(Listas.Usuarios.Apellidos)) AS NombreUsuario FROM Riesgos.RiesgoAnulado INNER JOIN Riesgos.Riesgo ON Riesgos.RiesgoAnulado.IdRiesgo = Riesgos.Riesgo.IdRiesgo INNER JOIN Listas.Usuarios ON Riesgos.RiesgoAnulado.IdUsuario = Listas.Usuarios.IdUsuario WHERE (Riesgos.Riesgo.Anulado = 1)");
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

        public DataTable loadInfoControlesRiesgo(String IdRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                //strConsulta = string.Format("SELECT Riesgos.ControlesRiesgo.IdControlesRiesgo, Riesgos.Control.IdControl, Riesgos.Control.CodigoControl, Riesgos.Control.NombreControl, Parametrizacion.Test.NombreTest, ((((Parametrizacion.ClaseControl.ValorClaseControl) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 1))) + ((Parametrizacion.TipoControl.ValorTipoControl) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 2))) + ((Parametrizacion.ResponsableExperiencia.ValorResponsableExperiencia) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 3))) + ((Parametrizacion.Documentacion.ValorDocumentacion) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 4))) + ((Parametrizacion.Responsabilidad.ValorResponsabilidad) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 5)))) / 100) AS CalificacionControl, Parametrizacion.CalificacionControl.DesviacionProbabilidad, Parametrizacion.CalificacionControl.DesviacionImpacto, Riesgos.Control.IdMitiga, Riesgos.Control.IdCalificacionControl, Parametrizacion.CalificacionControl.Color, Parametrizacion.CalificacionControl.NombreEscala FROM Riesgos.Control INNER JOIN Parametrizacion.ClaseControl ON Riesgos.Control.IdClaseControl = Parametrizacion.ClaseControl.IdClaseControl INNER JOIN Parametrizacion.TipoControl ON Riesgos.Control.IdTipoControl = Parametrizacion.TipoControl.IdTipoControl INNER JOIN Parametrizacion.ResponsableExperiencia ON Riesgos.Control.IdResponsableExperiencia = Parametrizacion.ResponsableExperiencia.IdResponsableExperiencia INNER JOIN Parametrizacion.Documentacion ON Riesgos.Control.IdDocumentacion = Parametrizacion.Documentacion.IdDocumentacion INNER JOIN Parametrizacion.Responsabilidad ON Riesgos.Control.IdResponsabilidad = Parametrizacion.Responsabilidad.IdResponsabilidad INNER JOIN Riesgos.ControlesRiesgo ON Riesgos.Control.IdControl = Riesgos.ControlesRiesgo.IdControl INNER JOIN Parametrizacion.Test ON Riesgos.Control.IdTest = Parametrizacion.Test.IdTest INNER JOIN Parametrizacion.CalificacionControl ON Riesgos.Control.IdCalificacionControl = Parametrizacion.CalificacionControl.IdCalificacionControl WHERE (Riesgos.ControlesRiesgo.IdRiesgo = {0})", IdRiesgo);
                strConsulta = string.Format("SELECT [IdControlesRiesgo],[CodigoControl],[NombreControl],[NombreTest]"
                +",[IdControl],[Expr1],[Expr2],[Expr3],[IdMitiga],[IdCalificacionControl],[DesviacionProbabilidad]"
                +",[DesviacionImpacto],[Color],[NombreEscala],[IdRiesgo],[CalificacionControl] "
                + " FROM [Riesgos].[vwRiesgosCalificacionControl]"
                + " where IdRiesgo = {0}", IdRiesgo);
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

        public DataTable loadInfoRiesgos(String Codigo, String Nombre, String IdCadenaValor, String IdMacroproceso, String IdProceso,
            String IdSubProceso, String IdClasificacionRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = "";
            try
            {
                #region Filtros
                if (Codigo != "")
                    condicion = "AND (Riesgos.Riesgo.Codigo = '" + Codigo + "') ";
                if (Nombre != "")
                    condicion += "AND (Riesgos.Riesgo.Nombre LIKE '%" + Nombre + "%') ";
                if (IdCadenaValor != "---")
                    condicion += "AND (Riesgos.Riesgo.IdCadenaValor = " + IdCadenaValor + ") ";
                if (IdMacroproceso != "---")
                    condicion += "AND (Riesgos.Riesgo.IdMacroproceso = " + IdMacroproceso + " ) ";
                if (IdProceso != "---")
                    condicion += "AND (Riesgos.Riesgo.IdProceso = " + IdProceso + ") ";
                if (IdSubProceso != "---")
                    condicion += "AND (Riesgos.Riesgo.IdSubProceso = " + IdSubProceso + ") ";
                if (IdClasificacionRiesgo != "---")
                    condicion += "AND (Riesgos.Riesgo.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ") ";

                #endregion Filtros

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.IdRegion, Riesgos.Riesgo.IdPais, Riesgos.Riesgo.IdDepartamento, Riesgos.Riesgo.IdCiudad, Riesgos.Riesgo.IdOficinaSucursal, Riesgos.Riesgo.IdCadenaValor, Riesgos.Riesgo.IdMacroproceso, Riesgos.Riesgo.IdProceso, Riesgos.Riesgo.IdSubProceso, Riesgos.Riesgo.IdActividad, Riesgos.Riesgo.IdClasificacionRiesgo, Riesgos.Riesgo.IdClasificacionGeneralRiesgo, Riesgos.Riesgo.IdClasificacionParticularRiesgo, Riesgos.Riesgo.IdFactorRiesgoOperativo, Riesgos.Riesgo.IdTipoRiesgoOperativo, Riesgos.Riesgo.IdTipoEventoOperativo, Riesgos.Riesgo.IdRiesgoAsociadoOperativo, Riesgos.Riesgo.ListaRiesgoAsociadoLA, Riesgos.Riesgo.ListaFactorRiesgoLAFT, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.Descripcion, Riesgos.Riesgo.ListaCausas, Riesgos.Riesgo.ListaConsecuencias, Riesgos.Riesgo.IdResponsableRiesgo, Riesgos.Riesgo.IdProbabilidad, Riesgos.Riesgo.OcurrenciaEventoDesde, Riesgos.Riesgo.OcurrenciaEventoHasta, Riesgos.Riesgo.IdImpacto, Riesgos.Riesgo.PerdidaEconomicaDesde, Riesgos.Riesgo.PerdidaEconomicaHasta, Riesgos.Riesgo.FechaRegistro, LTRIM(RTRIM(Listas.Usuarios.Nombres)) + ' ' + LTRIM(RTRIM(Listas.Usuarios.Apellidos)) AS Nombres, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, Parametrizacion.JerarquiaOrganizacional.NombreHijo, Riesgos.Riesgo.ListaTratamiento FROM Riesgos.Riesgo INNER JOIN Listas.Usuarios ON Riesgos.Riesgo.IdUsuario = Listas.Usuarios.IdUsuario INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo INNER JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Riesgo.IdResponsableRiesgo WHERE (Riesgos.Riesgo.Anulado = 0) " + condicion);
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
        public DataTable loadInfoRiesgosProceso(String IdCadenaValor, String IdMacroproceso, String IdProceso,
            String IdSubProceso)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = "";
            try
            {
                #region Filtros

                if (IdCadenaValor != "---")
                    condicion += "AND (Riesgos.Riesgo.IdCadenaValor = " + IdCadenaValor + ") ";
                if (IdMacroproceso != "---")
                    condicion += "AND (Riesgos.Riesgo.IdMacroproceso = " + IdMacroproceso + " ) ";
                if (IdProceso != "---")
                    condicion += "AND (Riesgos.Riesgo.IdProceso = " + IdProceso + ") ";
                if (IdSubProceso != "---")
                    condicion += "AND (Riesgos.Riesgo.IdSubProceso = " + IdSubProceso + ") ";


                #endregion Filtros

                cDataBase.conectar();
                string str = "SELECT Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.IdRegion, Riesgos.Riesgo.IdPais, Riesgos.Riesgo.IdDepartamento, Riesgos.Riesgo.IdCiudad, Riesgos.Riesgo.IdOficinaSucursal, Riesgos.Riesgo.IdCadenaValor, Riesgos.Riesgo.IdMacroproceso, Riesgos.Riesgo.IdProceso, Riesgos.Riesgo.IdSubProceso, Riesgos.Riesgo.IdActividad, Riesgos.Riesgo.IdClasificacionRiesgo, Riesgos.Riesgo.IdClasificacionGeneralRiesgo, Riesgos.Riesgo.IdClasificacionParticularRiesgo, Riesgos.Riesgo.IdFactorRiesgoOperativo, Riesgos.Riesgo.IdTipoRiesgoOperativo, Riesgos.Riesgo.IdTipoEventoOperativo, Riesgos.Riesgo.IdRiesgoAsociadoOperativo, Riesgos.Riesgo.ListaRiesgoAsociadoLA, Riesgos.Riesgo.ListaFactorRiesgoLAFT, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.Descripcion, Riesgos.Riesgo.ListaCausas, Riesgos.Riesgo.ListaConsecuencias, Riesgos.Riesgo.IdResponsableRiesgo, Riesgos.Riesgo.IdProbabilidad, Riesgos.Riesgo.OcurrenciaEventoDesde, Riesgos.Riesgo.OcurrenciaEventoHasta, Riesgos.Riesgo.IdImpacto, Riesgos.Riesgo.PerdidaEconomicaDesde, Riesgos.Riesgo.PerdidaEconomicaHasta, Riesgos.Riesgo.FechaRegistro, LTRIM(RTRIM(Listas.Usuarios.Nombres)) + ' ' + LTRIM(RTRIM(Listas.Usuarios.Apellidos)) AS Nombres, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, Parametrizacion.JerarquiaOrganizacional.NombreHijo, Riesgos.Riesgo.ListaTratamiento FROM Riesgos.Riesgo INNER JOIN Listas.Usuarios ON Riesgos.Riesgo.IdUsuario = Listas.Usuarios.IdUsuario INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo INNER JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Riesgo.IdResponsableRiesgo WHERE (Riesgos.Riesgo.Anulado = 0) " + condicion;
                dtInformacion = cDataBase.ejecutarConsulta(str);
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
        public DataTable loadInfoRiesgosCausasNew(String ListCausas, string IdControl, string IdRiesgo)
        {
            string causas;
            DataTable dtInformacion = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(ListCausas))
                {
                    if (ListCausas.Substring(ListCausas.Length - 1).Equals(","))
                    {
                        causas = ListCausas.Remove(ListCausas.Length - 1);
                    }
                    else
                        causas = ListCausas;
                    List<SqlParameter> parametros = new List<SqlParameter>()
                    {
                        new SqlParameter() { ParameterName = "@Causas", SqlDbType = SqlDbType.VarChar, Value =  causas},
                    };
                    dtInformacion = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[RiesgoSeleccionarCausasNombre]", parametros);
                }
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable loadInfoRiesgosCausas(String ListCausas)
        {
            DataTable dtInformacion = new DataTable();
            String consulta = string.Format("SELECT IdCausas, NombreCausas FROM Parametrizacion.Causas where IdCausas in ({0})", ListCausas);
            consulta = consulta.Replace(",)", ")");
            /*consulta = string.Format("union all SELECT CS.IdCausas, CS.NombreCausas from Riesgos.RiesgosCausasvsControles RCC " +
                "INNER JOIN Parametrizacion.Causas CS ON CS.IdCausas = RCC.Idcausas WHERE RCC.IdControl = {0} and RCC.IdRiesgo = {1}", IdControl, IdRiesgo);*/
            try
            {

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(consulta);
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
        public DataTable mtdLoadInfoAudRiesgoControl()
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty;

            try
            {
                strQrySelect = "SELECT RACR.Id, RR.IdRiesgo, RR.Codigo CodigoRiesgo, RC.IdControl, RC.CodigoControl CodigoControl, LU.IdUsuario, " +
                    "RTRIM(LTRIM(LU.Usuario)) + ' - ' + RTRIM(LTRIM(LU.Nombres)) + ' ' + RTRIM(LTRIM(LU.Apellidos)) AS NombreUsuario, " +
                    "RACR.FechaRegistro Fecha";

                strQryFrom = "FROM [Riesgos].[AudControlRiesgo] RACR " +
                    "INNER JOIN Riesgos.Riesgo RR ON RR.IdRiesgo = RACR.IdRiesgo " +
                    "INNER JOIN Riesgos.Control RC ON RC.IdControl = RACR.IdControl " +
                    "INNER JOIN Listas.Usuarios LU ON LU.IdUsuario = RACR.IdUsuario";

                strConsulta = string.Format("{0} {1}", strQrySelect, strQryFrom);
                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);
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

        public DataTable mtdLoadInfoAudEventoRiesgo()
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty;

            try
            {
                strQrySelect = "SELECT RACR.Id, RC.IdEvento, RC.CodigoEvento CodigoEvento, RR.IdRiesgo, RR.Codigo CodigoRiesgo, LU.IdUsuario, " +
                    "RTRIM(LTRIM(LU.Usuario)) + ' - ' + RTRIM(LTRIM(LU.Nombres)) + ' ' + RTRIM(LTRIM(LU.Apellidos)) AS NombreUsuario, " +
                    "RACR.Justificacion, RACR.FechaRegistro Fecha ";
                strQryFrom = "FROM Riesgos.AudEventoRiesgo RACR " +
                    "INNER JOIN Riesgos.Riesgo RR ON RR.IdRiesgo = RACR.IdRiesgo " +
                    "INNER JOIN Riesgos.Eventos RC ON RC.IdEvento = RACR.IdEvento " +
                    "INNER JOIN Listas.Usuarios LU ON LU.IdUsuario = RACR.IdUsuario ORDER BY RACR.FechaRegistro";

                strConsulta = string.Format("{0} {1}", strQrySelect, strQryFrom);
                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);
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
        #endregion Load

        #region Consultas
        public DataTable consultaRiesgoInherente(String IdCadenaValor, String IdMacroproceso,
            String IdProceso, String IdSubProceso, String IdClasificacionRiesgo,
            String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo,
            string IdAreas, string strFechaHistorico, string strIdFactorRiesgoLAFT, string strIdEmpresa,
            string strNombreEmpresa, string strIdObjetivo)
        {
            #region Variables Locales
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (RR.Anulado = 0)", strFrom = string.Empty, strConsulta = string.Empty,
                strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty;

            #endregion Variables Locales

            try
            {
                if (!string.IsNullOrEmpty(strFechaHistorico))
                {
                    condicion = string.Empty;

                    #region Filtros de historico

                    #region Filtro Cadena Valor
                    if (IdCadenaValor != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdCadenaValor = {0}", IdCadenaValor);
                        else
                            condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, IdCadenaValor);
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (IdMacroproceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdMacroproceso = {0}", IdMacroproceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, IdMacroproceso);
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (IdProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdProceso = {0}", IdProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, IdProceso);
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (IdSubProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdSubProceso = {0}", IdSubProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, IdSubProceso);
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (IdClasificacionRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionRiesgo = {0}", IdClasificacionRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, IdClasificacionRiesgo);
                    }
                    #endregion Filtro ClasificacionRiesgo

                    #region Filtro ClasificacionGeneralRiesgo
                    if (IdClasificacionGeneralRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionGeneralRiesgo = {0}", IdClasificacionGeneralRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionGeneralRiesgo = {1})", condicion, IdClasificacionGeneralRiesgo);
                    }
                    #endregion Filtro ClasificacionGeneralRiesgo

                    #region Filtro ClasificacionParticularRiesgo
                    if (IdClasificacionParticularRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionParticularRiesgo = {0}", IdClasificacionParticularRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionParticularRiesgo = {1})", condicion, IdClasificacionParticularRiesgo);
                    }
                    #endregion Filtro ClasificacionParticularRiesgo

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";

                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", IdAreas);
                        else
                            condicion =
                                string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(strFechaHistorico))
                    {
                        strFechaIni = mtdConvertirFecha(strFechaHistorico, 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(strFechaHistorico, 2) + " 23:59:59:998";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);

                        condicion += strFechaFinal;
                    }
                    #endregion Fechas Desde y Hasta

                    #region Filtro Nombre Empresa
                    if (strNombreEmpresa != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("WHERE RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("WHERE RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                        else
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("{0} AND RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("{0} AND RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                    }
                    #endregion Filtro Nombre Empresa

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidad, InfoHistorico.IdImpacto");
                        string strFromNormal = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidad, RHR.IdImpacto FROM Riesgos.HistoricoRiesgo RHR {2} {0} )  AS InfoHistorico ", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} GROUP BY InfoHistorico.IdProbabilidad, InfoHistorico.IdImpacto ", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidad, InfoHistorico.IdImpacto");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidad, RHR.IdImpacto FROM Riesgos.HistoricoRiesgo RHR {0} {1})  AS InfoHistorico", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY InfoHistorico.IdProbabilidad,InfoHistorico.IdImpacto", strSelHistorico, strFromHistorico);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros de historico


                }
                else
                {
                    #region Filtros Normales
                    #region Filtros Viejos
                    if (IdCadenaValor != "---")
                        condicion = condicion + " AND (RR.IdCadenaValor = " + IdCadenaValor + ")";

                    if (IdMacroproceso != "---")
                        condicion = condicion + " AND (RR.IdMacroproceso = " + IdMacroproceso + ")";

                    if (IdProceso != "---")
                        condicion = condicion + " AND (RR.IdProceso = " + IdProceso + ")";

                    if (IdSubProceso != "---")
                        condicion = condicion + " AND (RR.IdSubProceso = " + IdSubProceso + ")";

                    if (IdClasificacionRiesgo != "---")
                        condicion = condicion + " AND (RR.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")";

                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = condicion + " AND (RR.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")";

                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = condicion + " AND (RR.IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")";

                    #endregion Filtros Viejos

                    #region Filtro Factor Riesgos LAFT
                    if (strIdFactorRiesgoLAFT != "---")
                        condicion = condicion + " AND ('" + strIdFactorRiesgoLAFT + "' IN (SELECT COL1 FROM Procesos.FnSplitTable(RR.ListaFactorRiesgoLAFT,'|'))) ";
                    #endregion Filtro Factor Riesgos LAFT

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Filtro Nombre Empresa
                    if (strIdEmpresa != "---" && strIdEmpresa != "0")
                    {
                        if (string.IsNullOrEmpty(strFrom))
                            strFrom = "INNER JOIN Procesos.Proceso ON RR.IdProceso = Procesos.Proceso.IdProceso ";

                        if (string.IsNullOrEmpty(condicion))
                        {
                            switch (strIdEmpresa)
                            {
                                case "1":
                                case "2":
                                    condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa IN ({0}, 3)", strIdEmpresa);
                                    break;
                                case "3":
                                    condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa = {0}", strIdEmpresa);
                                    break;
                            }

                        }
                        else
                        {
                            switch (strIdEmpresa)
                            {
                                case "1":
                                case "2":
                                    condicion = string.Format("{0} AND Procesos.Proceso.IdEmpresa IN ({1}, 3)", condicion, strIdEmpresa);
                                    break;
                                case "3":
                                    condicion = string.Format("{0} AND Procesos.Proceso.IdEmpresa = {1}", condicion, strIdEmpresa);
                                    break;
                            }
                        }
                    }
                    #endregion Filtro Nombre Empresa
                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(RR.IdRiesgo) AS NumeroRegistros, RR.IdProbabilidad, RR.IdImpacto");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo RR {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} GROUP BY RR.IdProbabilidad, RR.IdImpacto", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelNormal = string.Format("SELECT COUNT(RR.IdRiesgo) AS NumeroRegistros, RR.IdProbabilidad, RR.IdImpacto");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo RR {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY RR.IdProbabilidad, RR.IdImpacto", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros Normales


                }

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

        public DataTable consultaRiesgoResidual(String IdCadenaValor, String IdMacroproceso,
            String IdProceso, String IdSubProceso, String IdClasificacionRiesgo,
            String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo,
            string IdAreas, string strFechaHistorico, string strIdFactorRiesgoLAFT, string strIdEmpresa, string strNombreEmpresa
            , string strIdObjetivo)
        {
            #region Variables Locales
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)", strFrom = string.Empty, strConsulta = string.Empty,
                strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty;
            #endregion Variables Locales

            try
            {
                if (!string.IsNullOrEmpty(strFechaHistorico))
                {
                    condicion = string.Empty;

                    #region Filtros

                    #region Filtro Cadena Valor
                    if (IdCadenaValor != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdCadenaValor = {0}", IdCadenaValor);
                        else
                            condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, IdCadenaValor);
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (IdMacroproceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdMacroproceso = {0}", IdMacroproceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, IdMacroproceso);
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (IdProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdProceso = {0}", IdProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, IdProceso);
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (IdSubProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdSubProceso = {0}", IdSubProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, IdSubProceso);
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (IdClasificacionRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionRiesgo = {0}", IdClasificacionRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, IdClasificacionRiesgo);
                    }
                    #endregion Filtro ClasificacionRiesgo

                    #region Filtro ClasificacionGeneralRiesgo
                    if (IdClasificacionGeneralRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionGeneralRiesgo = {0}", IdClasificacionGeneralRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionGeneralRiesgo = {1})", condicion, IdClasificacionGeneralRiesgo);
                    }
                    #endregion Filtro ClasificacionGeneralRiesgo

                    #region Filtro ClasificacionParticularRiesgo
                    if (IdClasificacionParticularRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionParticularRiesgo = {0}", IdClasificacionParticularRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionParticularRiesgo = {1})", condicion, IdClasificacionParticularRiesgo);
                    }
                    #endregion Filtro ClasificacionParticularRiesgo

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(strFechaHistorico))
                    {
                        strFechaIni = mtdConvertirFecha(strFechaHistorico, 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(strFechaHistorico, 2) + " 23:59:59:998";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta

                    #region Filtro Nombre Empresa
                    if (strNombreEmpresa != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("WHERE RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("WHERE RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                        else
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("{0} AND RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("{0} AND RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                    }
                    #endregion Filtro Nombre Empresa

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                        string strFromNormal = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {2} {0} ", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} {2}) AS InfoHistorico GROUP BY InfoHistorico.IdProbabilidadResidual,InfoHistorico.IdImpactoResidual ", strSelNormal, strFromNormal, condicion);
                    }
                    else
                    {
                        string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}) AS InfoHistorico GROUP BY InfoHistorico.IdProbabilidadResidual,InfoHistorico.IdImpactoResidual", strSelHistorico, strFromHistorico);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros
                }
                else
                {
                    #region Filtros
                    #region Filtros Viejos
                    if (IdCadenaValor != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdCadenaValor = " + IdCadenaValor + ")";
                    if (IdMacroproceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdMacroproceso = " + IdMacroproceso + ")";
                    if (IdProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdProceso = " + IdProceso + ")";
                    if (IdSubProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdSubProceso = " + IdSubProceso + ")";
                    if (IdClasificacionRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")";
                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")";
                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")";

                    #endregion Filtros Viejos

                    #region Filtro Factor Riesgos LAFT
                    if (strIdFactorRiesgoLAFT != "---")
                    {
                        condicion = condicion + " AND ('" + strIdFactorRiesgoLAFT + "' IN (SELECT COL1 FROM Procesos.FnSplitTable(Riesgos.Riesgo.ListaFactorRiesgoLAFT,'|'))) ";
                    }
                    #endregion Filtro Factor Riesgos LAFT

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Filtro Nombre Empresa
                    if (strIdEmpresa != "---" && strIdEmpresa != "0")
                    {
                        if (string.IsNullOrEmpty(strFrom))
                            strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";

                        if (string.IsNullOrEmpty(condicion))
                        {
                            switch (strIdEmpresa)
                            {
                                case "1":
                                case "2":
                                    condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa IN ({0}, 3)", strIdEmpresa);
                                    break;
                                case "3":
                                    condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa = {0}", strIdEmpresa);
                                    break;
                            }
                        }
                        else
                        {
                            switch (strIdEmpresa)
                            {
                                case "1":
                                case "2":
                                    condicion = string.Format("{0} AND Procesos.Proceso.IdEmpresa IN ({1}, 3)", condicion, strIdEmpresa);
                                    break;
                                case "3":
                                    condicion = string.Format("{0} AND Procesos.Proceso.IdEmpresa = {1}", condicion, strIdEmpresa);
                                    break;
                            }
                        }
                    }
                    #endregion Filtro Nombre Empresa

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = Riesgos.Riesgo.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual");
                        string strFromNormal = string.Format("FROM	Riesgos.Riesgo {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} GROUP BY Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual");
                        string strFromNormal = string.Format("FROM	Riesgos.Riesgo {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros


                }

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

        public DataTable perfilRiesgoInherente(String IdCadenaValor, String IdMacroproceso,
            String IdProceso, String IdSubProceso, String IdClasificacionRiesgo,
            String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo,
            string IdAreas, string strFechaHistorico, string strIdFactorRiesgoLAFT, string strIdEmpresa, string strNombreEmpresa,
            string strIdObjetivo)
        {
            #region Variables Locales
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)", strFrom = string.Empty, strConsulta = string.Empty,
                strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty;
            #endregion Variables Locales

            try
            {
                if (!string.IsNullOrEmpty(strFechaHistorico))
                {
                    condicion = string.Empty;

                    #region Filtros Historico

                    #region Filtro Cadena Valor
                    if (IdCadenaValor != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdCadenaValor = {0}", IdCadenaValor);
                        else
                            condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, IdCadenaValor);
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (IdMacroproceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdMacroproceso = {0}", IdMacroproceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, IdMacroproceso);
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (IdProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdProceso = {0}", IdProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, IdProceso);
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (IdSubProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdSubProceso = {0}", IdSubProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, IdSubProceso);
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (IdClasificacionRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionRiesgo = {0}", IdClasificacionRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, IdClasificacionRiesgo);
                    }
                    #endregion Filtro ClasificacionRiesgo

                    #region Filtro ClasificacionGeneralRiesgo
                    if (IdClasificacionGeneralRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionGeneralRiesgo = {0}", IdClasificacionGeneralRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionGeneralRiesgo = {1})", condicion, IdClasificacionGeneralRiesgo);
                    }
                    #endregion Filtro ClasificacionGeneralRiesgo

                    #region Filtro ClasificacionParticularRiesgo
                    if (IdClasificacionParticularRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionParticularRiesgo = {0}", IdClasificacionParticularRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionParticularRiesgo = {1})", condicion, IdClasificacionParticularRiesgo);
                    }
                    #endregion Filtro ClasificacionParticularRiesgo

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(strFechaHistorico))
                    {
                        strFechaIni = mtdConvertirFecha(strFechaHistorico, 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(strFechaHistorico, 2) + " 23:59:59:998";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta

                    #region Filtro Nombre Empresa
                    if (strNombreEmpresa != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("WHERE RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("WHERE RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                        else
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("{0} AND RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("{0} AND RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                    }
                    #endregion Filtro Nombre Empresa

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, ISNULL(SUM(InfoHistorico.SumatoriaProbabilidad), 0) AS SumatoriaProbabilidad, ISNULL(SUM(InfoHistorico.SumatoriaImpacto ), 0) AS SumatoriaImpacto");
                        string strFromNormal = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, PPr.ValorProbabilidad AS SumatoriaProbabilidad, PIm.ValorImpacto AS SumatoriaImpacto FROM Riesgos.HistoricoRiesgo AS RHR INNER JOIN Parametrizacion.Probabilidad AS PPr ON RHR.IdProbabilidad = PPr.IdProbabilidad INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpacto = PIm.IdImpacto {2} {0} {1}) AS InfoHistorico ", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, ISNULL(SUM(InfoHistorico.SumatoriaProbabilidad), 0) AS SumatoriaProbabilidad, ISNULL(SUM(InfoHistorico.SumatoriaImpacto ), 0) AS SumatoriaImpacto");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, PPr.ValorProbabilidad AS SumatoriaProbabilidad, PIm.ValorImpacto AS SumatoriaImpacto FROM Riesgos.HistoricoRiesgo AS RHR INNER JOIN Parametrizacion.Probabilidad AS PPr ON RHR.IdProbabilidad = PPr.IdProbabilidad INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpacto = PIm.IdImpacto {0} {1}) AS InfoHistorico", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}", strSelHistorico, strFromHistorico);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros Historico


                }
                else
                {
                    #region Filtros Normal
                    #region Filtros Viejos
                    if (IdCadenaValor != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdCadenaValor = " + IdCadenaValor + ")";

                    if (IdMacroproceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdMacroproceso = " + IdMacroproceso + ")";

                    if (IdProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdProceso = " + IdProceso + ")";

                    if (IdSubProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdSubProceso = " + IdSubProceso + ")";

                    if (IdClasificacionRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")";

                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")";

                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")";

                    #endregion Filtros Viejos

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(strFechaHistorico))
                    {
                        strFechaIni = mtdConvertirFecha(strFechaHistorico, 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(strFechaHistorico, 2) + " 23:59:59:998";
                        strFechaFinal = string.Format(" AND (Riesgos.Riesgo.FechaRegistro BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }
                    #endregion Fechas Desde y Hasta

                    #region Filtro Factor Riesgos LAFT
                    if (strIdFactorRiesgoLAFT != "---")
                        condicion = condicion + " AND ('" + strIdFactorRiesgoLAFT + "' IN (SELECT COL1 FROM Procesos.FnSplitTable(Riesgos.Riesgo.ListaFactorRiesgoLAFT,'|'))) ";
                    #endregion Filtro Factor Riesgos LAFT

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Filtro Nombre Empresa
                    if (strIdEmpresa != "---" && strIdEmpresa != "0")
                    {
                        if (string.IsNullOrEmpty(strFrom))
                            strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";

                        if (string.IsNullOrEmpty(condicion))
                        {
                            switch (strIdEmpresa)
                            {
                                case "1":
                                case "2":
                                    condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa IN ({0}, 3)", strIdEmpresa);
                                    break;
                                case "3":
                                    condicion = string.Format("WHERE Procesos.Proceso.IdEmpresa = {0}", strIdEmpresa);
                                    break;
                            }
                        }
                        else
                        {
                            switch (strIdEmpresa)
                            {
                                case "1":
                                case "2":
                                    condicion = string.Format("{0} AND Procesos.Proceso.IdEmpresa IN ({1}, 3)", condicion, strIdEmpresa);
                                    break;
                                case "3":
                                    condicion = string.Format("{0} AND Procesos.Proceso.IdEmpresa = {1}", condicion, strIdEmpresa);
                                    break;
                            }
                        }
                    }
                    #endregion Filtro Nombre Empresa

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = Riesgos.Riesgo.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, ISNULL(SUM(Parametrizacion.Probabilidad.ValorProbabilidad), 0) AS SumatoriaProbabilidad, ISNULL(SUM(Parametrizacion.Impacto.ValorImpacto), 0) AS SumatoriaImpacto");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidad = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpacto = Parametrizacion.Impacto.IdImpacto {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, ISNULL(SUM(Parametrizacion.Probabilidad.ValorProbabilidad), 0) AS SumatoriaProbabilidad, ISNULL(SUM(Parametrizacion.Impacto.ValorImpacto), 0) AS SumatoriaImpacto");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidad = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpacto = Parametrizacion.Impacto.IdImpacto {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros Normal

                }
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

        public DataTable perfilRiesgoResidual(String IdCadenaValor, String IdMacroproceso,
            String IdProceso, String IdSubProceso, String IdClasificacionRiesgo,
            String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo,
            string IdAreas, string strFechaHistorico, string strIdFactorRiesgoLAFT, string strIdEmpresa, string strNombreEmpresa,
            string strIdObjetivo)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)", strFrom = string.Empty, strConsulta = string.Empty,
                strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty;
            #endregion Variables

            try
            {
                if (!string.IsNullOrEmpty(strFechaHistorico))
                {
                    condicion = string.Empty;

                    #region Filtros Historicos

                    #region Filtro Cadena Valor
                    if (IdCadenaValor != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdCadenaValor = {0}", IdCadenaValor);
                        else
                            condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, IdCadenaValor);
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (IdMacroproceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdMacroproceso = {0}", IdMacroproceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, IdMacroproceso);
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (IdProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdProceso = {0}", IdProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, IdProceso);
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (IdSubProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdSubProceso = {0}", IdSubProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, IdSubProceso);
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (IdClasificacionRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionRiesgo = {0}", IdClasificacionRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, IdClasificacionRiesgo);
                    }
                    #endregion Filtro ClasificacionRiesgo

                    #region Filtro ClasificacionGeneralRiesgo
                    if (IdClasificacionGeneralRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionGeneralRiesgo = {0}", IdClasificacionGeneralRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionGeneralRiesgo = {1})", condicion, IdClasificacionGeneralRiesgo);
                    }
                    #endregion Filtro ClasificacionGeneralRiesgo

                    #region Filtro ClasificacionParticularRiesgo
                    if (IdClasificacionParticularRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionParticularRiesgo = {0}", IdClasificacionParticularRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionParticularRiesgo = {1})", condicion, IdClasificacionParticularRiesgo);
                    }
                    #endregion Filtro ClasificacionParticularRiesgo

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(strFechaHistorico))
                    {
                        strFechaIni = mtdConvertirFecha(strFechaHistorico, 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(strFechaHistorico, 2) + " 23:59:59:998";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta

                    #region Filtro Nombre Empresa
                    if (strNombreEmpresa != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("WHERE RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("WHERE RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                        else
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("{0} AND RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("{0} AND RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                    }
                    #endregion Filtro Nombre Empresa

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, ISNULL(SUM(InfoHistorico.SumatoriaProbabilidadResidual), 0) AS SumatoriaProbabilidadResidual, ISNULL(SUM(InfoHistorico.SumatoriaImpactoResidual), 0) AS SumatoriaImpactoResidual");
                        string strFromNormal = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, PPr.ValorProbabilidad AS SumatoriaProbabilidadResidual, PIm.ValorImpacto AS SumatoriaImpactoResidual FROM Riesgos.HistoricoRiesgo AS RHR INNER JOIN Parametrizacion.Probabilidad AS PPr ON RHR.IdProbabilidadResidual = PPr.IdProbabilidad INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpactoResidual = PIm.IdImpacto {2} {0} {1}) AS InfoHistorico", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, ISNULL(SUM(InfoHistorico.SumatoriaProbabilidadResidual), 0) AS SumatoriaProbabilidadResidual, ISNULL(SUM(InfoHistorico.SumatoriaImpactoResidual), 0) AS SumatoriaImpactoResidual");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, PPr.ValorProbabilidad AS SumatoriaProbabilidadResidual, PIm.ValorImpacto AS SumatoriaImpactoResidual FROM Riesgos.HistoricoRiesgo AS RHR INNER JOIN Parametrizacion.Probabilidad AS PPr ON RHR.IdProbabilidadResidual = PPr.IdProbabilidad INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpactoResidual = PIm.IdImpacto {0} {1}) AS InfoHistorico", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}", strSelHistorico, strFromHistorico);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros Historicos


                }
                else
                {
                    #region Filtros Normales
                    #region Filtros Viejos
                    if (IdCadenaValor != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdCadenaValor = " + IdCadenaValor + ")";

                    if (IdMacroproceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdMacroproceso = " + IdMacroproceso + ")";

                    if (IdProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdProceso = " + IdProceso + ")";

                    if (IdSubProceso != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdSubProceso = " + IdSubProceso + ")";

                    if (IdClasificacionRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")";

                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")";

                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")";

                    #endregion Filtros Viejos

                    #region Filtro Factor Riesgos LAFT
                    if (strIdFactorRiesgoLAFT != "---")
                        condicion = condicion + " AND ('" + strIdFactorRiesgoLAFT + "' IN (SELECT COL1 FROM Procesos.FnSplitTable(Riesgos.Riesgo.ListaFactorRiesgoLAFT,'|'))) ";

                    #endregion Filtro Factor Riesgos LAFT

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = Riesgos.Riesgo.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, ISNULL(SUM(Parametrizacion.Probabilidad.ValorProbabilidad), 0) AS SumatoriaProbabilidadResidual, ISNULL(SUM(Parametrizacion.Impacto.ValorImpacto), 0) AS SumatoriaImpactoResidual");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, ISNULL(SUM(Parametrizacion.Probabilidad.ValorProbabilidad), 0) AS SumatoriaProbabilidadResidual, ISNULL(SUM(Parametrizacion.Impacto.ValorImpacto), 0) AS SumatoriaImpactoResidual");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros Normales


                }

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
        public DataTable perfilRiesgoResidualvsSistemaRiesgo(String IdCadenaValor, String IdMacroproceso,
            String IdProceso, String IdSubProceso, String IdClasificacionRiesgo,
            String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo,
            string IdAreas, string strFechaHistorico, string strIdFactorRiesgoLAFT, string strIdEmpresa, string strNombreEmpresa,
            string strIdObjetivo)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (RR.Anulado = 0)", strFrom = string.Empty, strConsulta = string.Empty,
                strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty;
            #endregion Variables

            try
            {
                if (!string.IsNullOrEmpty(strFechaHistorico))
                {
                    condicion = string.Empty;

                    #region Filtros Historicos

                    #region Filtro Cadena Valor
                    if (IdCadenaValor != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdCadenaValor = {0}", IdCadenaValor);
                        else
                            condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, IdCadenaValor);
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (IdMacroproceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdMacroproceso = {0}", IdMacroproceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, IdMacroproceso);
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (IdProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdProceso = {0}", IdProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, IdProceso);
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (IdSubProceso != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdSubProceso = {0}", IdSubProceso);
                        else
                            condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, IdSubProceso);
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (IdClasificacionRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionRiesgo = {0}", IdClasificacionRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, IdClasificacionRiesgo);
                    }
                    #endregion Filtro ClasificacionRiesgo

                    #region Filtro ClasificacionGeneralRiesgo
                    if (IdClasificacionGeneralRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionGeneralRiesgo = {0}", IdClasificacionGeneralRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionGeneralRiesgo = {1})", condicion, IdClasificacionGeneralRiesgo);
                    }
                    #endregion Filtro ClasificacionGeneralRiesgo

                    #region Filtro ClasificacionParticularRiesgo
                    if (IdClasificacionParticularRiesgo != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionParticularRiesgo = {0}", IdClasificacionParticularRiesgo);
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionParticularRiesgo = {1})", condicion, IdClasificacionParticularRiesgo);
                    }
                    #endregion Filtro ClasificacionParticularRiesgo

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(strFechaHistorico))
                    {
                        strFechaIni = mtdConvertirFecha(strFechaHistorico, 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(strFechaHistorico, 2) + " 23:59:59:998";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta

                    #region Filtro Nombre Empresa
                    if (strNombreEmpresa != "---")
                    {
                        if (string.IsNullOrEmpty(condicion))
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("WHERE RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("WHERE RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                        else
                        {
                            switch (strNombreEmpresa)
                            {
                                case "AMBAS":
                                    condicion = string.Format("{0} AND RHR.Empresa = '{0}'", strNombreEmpresa);
                                    break;
                                default:
                                    condicion = string.Format("{0} AND RHR.Empresa IN ('{0}', 'AMBAS')", strNombreEmpresa);
                                    break;
                            }
                        }
                    }
                    #endregion Filtro Nombre Empresa

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, "
                            + "ISNULL(SUM(InfoHistorico.SumatoriaProbabilidadResidual), 0) AS SumatoriaProbabilidadResidual, "
                            + "ISNULL(SUM(InfoHistorico.SumatoriaImpactoResidual), 0) AS SumatoriaImpactoResidual"
                            + ",InfoHistorico.IdClasificacionRiesgo, InfoHistorico.NombreClasificacionRiesgo");
                        string strFromNormal = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, PPr.ValorProbabilidad AS SumatoriaProbabilidadResidual,"
                            + "PIm.ValorImpacto AS SumatoriaImpactoResidual "
                            + ",RHR.IdClasificacionRiesgo, CR.NombreClasificacionRiesgo "
                            + "FROM Riesgos.HistoricoRiesgo AS RHR "
                            + "INNER JOIN Parametrizacion.ClasificacionRiesgo as CR on CR.IdClasificacionRiesgo = RHR.IdClasificacionRiesgo "
                            + "INNER JOIN Parametrizacion.Probabilidad AS PPr ON RHR.IdProbabilidadResidual = PPr.IdProbabilidad "
                            + "INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpactoResidual = PIm.IdImpacto {2} {0} {1}) AS InfoHistorico", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} GROUP BY InfoHistorico.IdClasificacionRiesgo, InfoHistorico.NombreClasificacionRiesgo", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, "
                            + "ISNULL(SUM(InfoHistorico.SumatoriaProbabilidadResidual), 0) AS SumatoriaProbabilidadResidual, "
                            + "ISNULL(SUM(InfoHistorico.SumatoriaImpactoResidual), 0) AS SumatoriaImpactoResidual"
                            + ",InfoHistorico.IdClasificacionRiesgo, InfoHistorico.NombreClasificacionRiesgo");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, PPr.ValorProbabilidad AS SumatoriaProbabilidadResidual,"
                            + "PIm.ValorImpacto AS SumatoriaImpactoResidual "
                            + ",RHR.IdClasificacionRiesgo, CR.NombreClasificacionRiesgo "
                            + "FROM Riesgos.HistoricoRiesgo AS RHR "
                            + "INNER JOIN Parametrizacion.ClasificacionRiesgo as CR on CR.IdClasificacionRiesgo = RHR.IdClasificacionRiesgo "
                            + "INNER JOIN Parametrizacion.Probabilidad AS PPr ON RHR.IdProbabilidadResidual = PPr.IdProbabilidad "
                            + "INNER JOIN Parametrizacion.Impacto AS PIm ON RHR.IdImpactoResidual = PIm.IdImpacto {0} {1}) AS InfoHistorico", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY InfoHistorico.IdClasificacionRiesgo, InfoHistorico.NombreClasificacionRiesgo", strSelHistorico, strFromHistorico);
                    }


                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros Historicos


                }
                else
                {
                    #region Filtros Normales
                    #region Filtros Viejos
                    if (IdCadenaValor != "---")
                        condicion = condicion + " AND (RR.IdCadenaValor = " + IdCadenaValor + ")";

                    if (IdMacroproceso != "---")
                        condicion = condicion + " AND (RR.IdMacroproceso = " + IdMacroproceso + ")";

                    if (IdProceso != "---")
                        condicion = condicion + " AND (RR.IdProceso = " + IdProceso + ")";

                    if (IdSubProceso != "---")
                        condicion = condicion + " AND (RR.IdSubProceso = " + IdSubProceso + ")";

                    if (IdClasificacionRiesgo != "---")
                        condicion = condicion + " AND (RR.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")";

                    if (IdClasificacionGeneralRiesgo != "---")
                        condicion = condicion + " AND (RR.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")";

                    if (IdClasificacionParticularRiesgo != "---")
                        condicion = condicion + " AND (RR.IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ")";

                    #endregion Filtros Viejos

                    #region Filtro Factor Riesgos LAFT
                    if (strIdFactorRiesgoLAFT != "---")
                        condicion = condicion + " AND ('" + strIdFactorRiesgoLAFT + "' IN (SELECT COL1 FROM Procesos.FnSplitTable(RR.ListaFactorRiesgoLAFT,'|'))) ";

                    #endregion Filtro Factor Riesgos LAFT

                    #region Areas
                    if (!string.IsNullOrEmpty(IdAreas))
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, IdAreas);
                    }
                    #endregion

                    #region Filtros por Objetivos Estrategicos
                    if (strIdObjetivo != "---" && strIdObjetivo != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + strIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(RR.IdRiesgo) AS NumeroRegistros,"
                            + "ISNULL(SUM(Parametrizacion.Probabilidad.ValorProbabilidad), 0) AS SumatoriaProbabilidadResidual,"
                            + "ISNULL(SUM(Parametrizacion.Impacto.ValorImpacto), 0) AS SumatoriaImpactoResidual"
                            + ",RR.IdClasificacionRiesgo, CR.NombreClasificacionRiesgo");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo as RR "
                            + "INNER JOIN Parametrizacion.ClasificacionRiesgo as CR on CR.IdClasificacionRiesgo = RR.IdClasificacionRiesgo "
                            + "INNER JOIN Parametrizacion.Probabilidad ON RR.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad "
                            + "INNER JOIN Parametrizacion.Impacto ON RR.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto "
                            + "{2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} GROUP BY RR.IdClasificacionRiesgo, CR.NombreClasificacionRiesgo", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelNormal = string.Format("SELECT COUNT(RR.IdRiesgo) AS NumeroRegistros,"
                            + "ISNULL(SUM(Parametrizacion.Probabilidad.ValorProbabilidad), 0) AS SumatoriaProbabilidadResidual,"
                            + "ISNULL(SUM(Parametrizacion.Impacto.ValorImpacto), 0) AS SumatoriaImpactoResidual"
                            + ",RR.IdClasificacionRiesgo, CR.NombreClasificacionRiesgo");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo as RR "
                            + "INNER JOIN Parametrizacion.ClasificacionRiesgo as CR on CR.IdClasificacionRiesgo = RR.IdClasificacionRiesgo "
                            + "INNER JOIN Parametrizacion.Probabilidad ON RR.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad "
                            + "INNER JOIN Parametrizacion.Impacto ON RR.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto "
                            + "{0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY RR.IdClasificacionRiesgo, CR.NombreClasificacionRiesgo", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    #endregion Filtros Normales


                }

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
        public DataTable causas(String IdCausas)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT NombreCausas FROM Parametrizacion.Causas WHERE IdCausas IN (" + IdCausas + ")");
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

        public DataTable consecuencias(String IdConsecuencia)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT NombreConsecuencia FROM Parametrizacion.Consecuencia WHERE IdConsecuencia IN (" + IdConsecuencia + ")");
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

        public String IdProbabilidad(String ValorProbabilidad)
        {
            DataTable dtInformacion = new DataTable();
            String valor = string.Empty;
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdProbabilidad FROM Parametrizacion.Probabilidad WHERE ValorProbabilidad = " + ValorProbabilidad + "");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            if (dtInformacion != null)
            {
                if (dtInformacion.Rows.Count > 0)
                {
                    valor = dtInformacion.Rows[0]["IdProbabilidad"].ToString().Trim();
                }
            }
            return valor;
        }

        public String IdImpacto(String ValorImpacto)
        {
            DataTable dtInformacion = new DataTable();
            String valor = string.Empty;
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdImpacto FROM Parametrizacion.Impacto WHERE ValorImpacto = " + ValorImpacto + "");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            if(dtInformacion != null)
            {
                if(dtInformacion.Rows.Count > 0)
                {
                    valor = dtInformacion.Rows[0]["IdImpacto"].ToString().Trim();
                }
            }
            return valor;
        }
        #endregion Consultas

        #region Plan de accion
        public void actualizarPlanAccionRiesgo(String IdPlanAccion, String DescripcionAccion, String Responsable, String IdTipoRecursoPlanAccion, String ValorRecurso, String IdEstadoPlanAccion, String FechaCompromiso)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.PlanesAccion SET DescripcionAccion = N'" + DescripcionAccion + "', Responsable = " + Responsable + ", IdTipoRecursoPlanAccion = " + IdTipoRecursoPlanAccion + ", ValorRecurso = N'" + ValorRecurso + "', IdEstadoPlanAccion = " + IdEstadoPlanAccion + ", FechaCompromiso = CONVERT(datetime, '" + FechaCompromiso + "', 120) WHERE (IdPlanAccion = " + IdPlanAccion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void registrarPlanAccionRiesgo(String IdRegistro, String DescripcionAccion, String Responsable, String IdTipoRecursoPlanAccion, String ValorRecurso, String IdEstadoPlanAccion, String FechaCompromiso)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.PlanesAccion (IdControlUsuario, IdRegistro, DescripcionAccion, Responsable, IdTipoRecursoPlanAccion, ValorRecurso, IdEstadoPlanAccion, FechaCompromiso) VALUES (3, " + IdRegistro + ", N'" + DescripcionAccion + "', " + Responsable + ", " + IdTipoRecursoPlanAccion + ", N'" + ValorRecurso + "', " + IdEstadoPlanAccion + ", CONVERT(datetime, '" + FechaCompromiso + "', 120))");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarComentarioPlanAccion(String Comentario, String IdRegistro)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Comentarios (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, Comentario) VALUES (4, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + Comentario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable mtdRegistrarPlanAccionRiesgo(string IdRegistro, string DescripcionAccion,
            string Responsable, string IdTipoRecursoPlanAccion, string ValorRecurso, string IdEstadoPlanAccion,
            string FechaCompromiso)
        {
            #region Variables
            string strConsulta = string.Empty, strValues = string.Empty,
                strConsultaRetorno = string.Empty, strConsultaFinal = string.Empty;
            DataTable dtInfo = new DataTable();
            #endregion Variables

            try
            {
                #region Consulta
                strConsulta = "INSERT INTO Riesgos.PlanesAccion (IdControlUsuario, IdRegistro, DescripcionAccion, Responsable, IdTipoRecursoPlanAccion, ValorRecurso, IdEstadoPlanAccion, FechaCompromiso)";
                strValues = string.Format("VALUES (3, {0}, N'{1}', {2}, {3}, N'{4}', {5}, CONVERT(datetime, '{6}', 120))",
                    IdRegistro, DescripcionAccion, Responsable, IdTipoRecursoPlanAccion, ValorRecurso, IdEstadoPlanAccion, FechaCompromiso);
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
                dtInfo = null;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return dtInfo;
        }

        #endregion Plan de accion

        #region Objetivo Riesgo
        public void desasociarObjetivoRiesgo(String IdObjetivosRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.ObjetivosRiesgo WHERE (IdObjetivosRiesgo = " + IdObjetivosRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void registrarObjetivoRiesgo(String IdRiesgo, String IdObjetivos)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.ObjetivosRiesgo (IdRiesgo, IdObjetivos, IdUsuario, FechaRegistro) VALUES (" + IdRiesgo + ", " + IdObjetivos + ", " + IdUsuario + ", GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion Objetivo Riesgo

        #region Legislacion
        public void agregarComentarioLegislacion(String Comentario, String IdRegistro)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Comentarios (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, Comentario) VALUES (2, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + Comentario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void registrarLegislacion(String CodigoLegislacion, String NombreLegislacion, String IdTipoLegislacion, String DescripcionLegislacion, String FechaVigenciaDesde, String FechaVigenciaHasta, String Actualizacion, String FechaCierre, String IdEstadoLegislacion, String IdResponsable)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Legislacion (CodigoLegislacion, NombreLegislacion, IdTipoLegislacion, DescripcionLegislacion, FechaVigenciaDesde, FechaVigenciaHasta, Actualizacion, IdUsuario, FechaRegistro, FechaCierre, IdEstadoLegislacion, IdResponsable) VALUES ((SELECT 'L'+CAST(MAX(IdLegislacion)+1 AS NVARCHAR(50)) FROM Riesgos.Legislacion), N'" + NombreLegislacion + "', " + IdTipoLegislacion + ", '" + DescripcionLegislacion + "', CONVERT(datetime, '" + FechaVigenciaDesde + "',120), CONVERT(datetime, '" + FechaVigenciaHasta + "',120), N'" + Actualizacion + "', " + IdUsuario + ", GETDATE(), CONVERT(datetime, '" + FechaCierre + "',120), " + IdEstadoLegislacion + ", " + IdResponsable + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void actualizarLegislacion(String IdLegislacion, String NombreLegislacion, String IdTipoLegislacion, String DescripcionLegislacion, String FechaVigenciaDesde, String FechaVigenciaHasta, String Actualizacion, String FechaCierre, String IdEstadoLegislacion, String IdResponsable)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.Legislacion SET NombreLegislacion = N'" + NombreLegislacion + "', IdTipoLegislacion = " + IdTipoLegislacion + ", DescripcionLegislacion = '" + DescripcionLegislacion + "', FechaVigenciaDesde = CONVERT(datetime, '" + FechaVigenciaDesde + "',120), FechaVigenciaHasta = CONVERT(datetime, '" + FechaVigenciaHasta + "',120), Actualizacion = N'" + Actualizacion + "', FechaCierre = CONVERT(datetime, '" + FechaCierre + "',120), IdEstadoLegislacion = " + IdEstadoLegislacion + ", IdResponsable = " + IdResponsable + " WHERE (IdLegislacion = " + IdLegislacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarLegislacion(String IdLegislacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.Legislacion WHERE (IdLegislacion = " + IdLegislacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetLastLegislacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT MAX(IdLegislacion) AS LastLegislacion FROM Riesgos.Legislacion");
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
        #endregion Legislacion

        #region Responsable

        public void desasociarResponsableRiesgo(String IdResponsableRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.RelacionResponsableRiesgo WHERE (IdResponsableRiesgo = " + IdResponsableRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarResponsableRiesgo(String IdRiesgo, String Responsable)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.RelacionResponsableRiesgo (IdRiesgo, Responsable) VALUES (" + IdRiesgo + ", '" + Responsable + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarResponsable(String IdResponsableRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.ResponsableRiesgo WHERE (IdResponsableRiesgo = " + IdResponsableRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void actualizarResponsable(String IdResponsableRiesgo, String NombreResponsableRiesgo, String IdNivelResponsable, String Email, String PerteneceURS)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.ResponsableRiesgo SET NombreResponsableRiesgo = N'" + NombreResponsableRiesgo + "', IdNivelResponsable = " + IdNivelResponsable + ", Email = N'" + Email + "', PerteneceURS = " + PerteneceURS + " WHERE (IdResponsableRiesgo = " + IdResponsableRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void registrarResponsable(String CodigoResponsableRiesgo, String NombreResponsableRiesgo, String IdNivelResponsable, String Email, String PerteneceURS)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.ResponsableRiesgo (CodigoResponsableRiesgo, NombreResponsableRiesgo, IdNivelResponsable, Email, PerteneceURS, IdUsuario, FechaRegistro) VALUES (N'" + CodigoResponsableRiesgo + "', N'" + NombreResponsableRiesgo + "', " + IdNivelResponsable + ", N'" + Email + "', " + PerteneceURS + ", " + IdUsuario + ", GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion Responsable

        #region Riesgos
        public void actualizarRiesgoResidual(String ValorProbabilidad, String ValorImpacto, String IdRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.Riesgo SET IdProbabilidadResidual = (SELECT IdProbabilidad FROM Parametrizacion.Probabilidad WHERE (ValorProbabilidad = " + ValorProbabilidad + ")), IdImpactoResidual = (SELECT IdImpacto FROM Parametrizacion.Impacto WHERE (ValorImpacto = " + ValorImpacto + ")) WHERE (IdRiesgo = " + IdRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarComentarioRiesgo(String Comentario, String IdRegistro)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Comentarios (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, Comentario) VALUES (3, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + Comentario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void registrarRiesgoControl(CRiesgoControl riesgoControl)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdRiesgo", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdRiesgo },
                    new SqlParameter() { ParameterName = "@IdControl", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdControl },
                    new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdUsuario },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                int result = cDataBase.EjecutarSPParametrosReturnInteger("[Riesgos].[RiesgosControlInsertar]", parametros);
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.Message);
                if (ex.Message.Contains("UC_RIEGOS_CONTROLES"))
                {
                    throw new Exception("La asociación Riesgo-Control ya existe en la base de datos.");
                }
                else
                    throw ex;
            }
        }

        public void registrarCausaRiesgoControl(CRiesgoControl riesgoControl)
        {
            try
            {
                string[] lstCausas = riesgoControl.IdCausa.Split('|');
                foreach (var causa in lstCausas)
                {
                    List<SqlParameter> parametros = new List<SqlParameter>()
                    {
                        new SqlParameter() { ParameterName = "@IdRiesgo", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdRiesgo },
                        new SqlParameter() { ParameterName = "@IdControl", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdControl },
                        new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdUsuario },
                        new SqlParameter() { ParameterName = "@IdCausa", SqlDbType = SqlDbType.Int, Value =  causa },
                        new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                    };
                    cDataBase.EjecutarSPParametrosReturnInteger("[Riesgos].[RiesgosControlInsertarCausas]", parametros);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void agregarCausasvsControles(int IdCausa, int IdRiesgo, int IdControl, DateTime FechaRegistro, int UsuarioCreacion, ref bool flag)
        {
            string Consulta = string.Empty;
            Consulta = string.Format("INSERT INTO [Riesgos].[RiesgosCausasvsControles] ([Idcausas],[IdControl] ,[IdRiesgo] ,[FechaRegistro] ,[UsuarioCreacion])"
                    + " VALUES({0},{1},{2},GETDATE(),{3})", IdCausa, IdControl, IdRiesgo,  UsuarioCreacion);//FechaRegistro,
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery(Consulta);
                cDataBase.desconectar();
                flag = true;
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
       
        //public void deleteCausasvsControles(int IdRiesgo, int IdControl)
        public void deleteCausasvsControles(CRiesgoControl riesgoControl)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdRiesgo", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdRiesgo },
                    new SqlParameter() { ParameterName = "@IdControl", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdControl },
                    new SqlParameter() { ParameterName = "@IdCausa", SqlDbType = SqlDbType.Int, Value =  riesgoControl.IdCausa },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                cDataBase.EjecutarSPParametrosReturnInteger("[Riesgos].[RiesgosControlEliminarCausa]", parametros);
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        public DataTable LoadCausasvsControles(int IdRiesgo, int IdControl, int IdCausas)
        {
            DataTable dtInformacion = new DataTable();
            string query = string.Empty;
            query = string.Format("SELECT [Idcausas]  FROM [Riesgos].[RiesgosCausasvsControles] where IdControl = {0} and IdRiesgo = {1} and Idcausas ={2}"
                , IdControl, IdRiesgo, IdCausas);
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(query);
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
        public void anularRiesgo(String IdRiesgo, String MotivoAnulacion)
        {

            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.RiesgoAnulado (IdRiesgo, MotivoAnulacion, FechaAnulacion, IdUsuario) VALUES (" + IdRiesgo + ", N'" + MotivoAnulacion + "', GETDATE(), " + IdUsuario + ")");
                cDataBase.ejecutarQuery("UPDATE Riesgos.Riesgo SET Anulado = 1 WHERE (IdRiesgo = " + IdRiesgo + ")");
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.EventoRiesgo where IdRiesgo = "+ IdRiesgo);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetLastRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                //dtInformacion = cDataBase.ejecutarConsulta("SELECT MAX(IdRiesgo) AS UltRiesgo FROM Riesgos.Riesgo");
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP 1 (IdRiesgo) AS UltRiesgo FROM Riesgos.Riesgo ORDER BY IdRiesgo DESC");
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

        //public void desasociarControlesRiesgo(string IdControlesRiesgo)
        //{

        //    try
        //    {
        //        cDataBase.conectar();
        //        cDataBase.ejecutarQuery("DELETE FROM Riesgos.ControlesRiesgo WHERE (IdControlesRiesgo = " + IdControlesRiesgo + ")");
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //}

        public void mtdQuitarRelacionEventoRiesgo(string strIdEventoRiesgo, string strJustificacion)
        {
            string strQryInsert = string.Empty, strQryDelete = string.Empty, strQrySelect = string.Empty;
            string strIdRiesgo = string.Empty, strIdEvento = string.Empty, strTipoConsulta = string.Empty;
            DataTable dtInfo = new DataTable();

            try
            {
                strQrySelect = string.Format("SELECT [IdRiesgo], [IdEvento] FROM [Riesgos].[EventoRiesgo] WHERE [IdEventoRiesgo] = {0}", strIdEventoRiesgo);
                strTipoConsulta = "1";
                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strQrySelect);
                cDataBase.desconectar();

                if (dtInfo.Rows.Count > 0 && dtInfo != null)
                {
                    strIdRiesgo = dtInfo.Rows[0]["IdRiesgo"].ToString().Trim();
                    strIdEvento = dtInfo.Rows[0]["IdEvento"].ToString().Trim();
                }

                strQryDelete = string.Format("DELETE FROM Riesgos.EventoRiesgo WHERE (IdEventoRiesgo = {0})", strIdEventoRiesgo);
                strTipoConsulta = "2";
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strQryDelete);
                cDataBase.desconectar();

                strQryInsert = string.Format("INSERT INTO Riesgos.AudEventoRiesgo (IdEvento,IdRiesgo,IdUsuario, Justificacion, FechaRegistro) VALUES ({0}, {1}, {2},N'{3}', GETDATE())",
                strIdEvento, strIdRiesgo, IdUsuario, strJustificacion);
                strTipoConsulta = "3";
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strQryInsert);
                cDataBase.desconectar();

                strQryDelete = string.Format("DELETE FROM [Riesgos].[EventosVsRiesgosCausas] WHERE IdEvento = {0} and IdRiesgo={1}",
                strIdEvento, strIdRiesgo);
                strTipoConsulta = "4";
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strQryDelete);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
            }
        }

        public void mtdQuitarRelacionRiesgoControl(string strIdControlesRiesgo, ref string strMessage)
        {
            string strQryInsert = string.Empty, strQryDelete = string.Empty, strQrySelect = string.Empty;
            string strIdRiesgo = string.Empty, strIdControl = string.Empty, strTipoConsulta = string.Empty;
            DataTable dtInfo = new DataTable();

            try
            {
                strQrySelect = string.Format("SELECT [IdRiesgo], [IdControl] FROM [Riesgos].[ControlesRiesgo] WHERE [IdControlesRiesgo] = {0}", strIdControlesRiesgo);
                strTipoConsulta = "1";
                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strQrySelect);
                cDataBase.desconectar();

                if (dtInfo.Rows.Count > 0 && dtInfo != null)
                {
                    strIdRiesgo = dtInfo.Rows[0]["IdRiesgo"].ToString().Trim();
                    strIdControl = dtInfo.Rows[0]["IdControl"].ToString().Trim();
                }

                strQryDelete = string.Format("DELETE FROM Riesgos.ControlesRiesgo WHERE (IdControlesRiesgo = {0})", strIdControlesRiesgo);
                strTipoConsulta = "2";
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strQryDelete);
                cDataBase.desconectar();

                strQryDelete = string.Format("DELETE FROM [Riesgos].[RiesgosCausasvsControles] WHERE IdControl = {1} and IdRiesgo = {0}", strIdRiesgo, strIdControl);
                strTipoConsulta = "2";
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strQryDelete);
                cDataBase.desconectar();

                strQryInsert = string.Format("INSERT INTO [Riesgos].[AudControlRiesgo] ([IdRiesgo],[IdControl],[IdUsuario],[FechaRegistro]) VALUES ({0}, {1}, {2}, GETDATE())",
                    strIdRiesgo, strIdControl, IdUsuario);
                strTipoConsulta = "3";
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strQryInsert);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                strMessage = "[ Error al eliminar la información.(" + strTipoConsulta + ")]: " + ex.Message + ", " + ex.StackTrace;
                cError.errorMessage(strMessage);
            }
        }

        public void registrarControlesRiesgo(String IdRiesgo, String IdControl)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.ControlesRiesgo (IdRiesgo, IdControl, IdUsuario, FechaRegistro) VALUES (" +
                    IdRiesgo + ", " + IdControl + ", " + IdUsuario + ", GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void modificarRiesgo(String IdRegion, String IdPais, String IdDepartamento, String IdCiudad, String IdOficinaSucursal,
            String IdCadenaValor, String IdMacroproceso, String IdProceso, String IdSubProceso, String IdActividad,
            String IdClasificacionRiesgo, String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo, String IdFactorRiesgoOperativo,
            String IdTipoRiesgoOperativo, String IdTipoEventoOperativo, String IdRiesgoAsociadoOperativo, String ListaRiesgoAsociadoLA, String ListaFactorRiesgoLAFT,
            String Nombre, String Descripcion, String ListaCausas, String ListaConsecuencias, String IdResponsableRiesgo,
            String IdProbabilidad, String OcurrenciaEventoDesde, String OcurrenciaEventoHasta, String IdImpacto, String PerdidaEconomicaDesde,
            String PerdidaEconomicaHasta, String ListaTratamiento, String IdRiesgo, string strJustificacion)
        {
            #region Variables
            string strConsultaUpdate = string.Empty;
            string strConsultaModificacion = string.Empty, strCamposModificacion = string.Empty;
            string strCodigoRiesgo = string.Empty;
            #endregion Variables

            try
            {
                #region Consultas
                strCodigoRiesgo = "R" + IdRiesgo;
                strCamposModificacion = "([IdCodigoRiesgo],[CodigoRiesgo],[NombreRiesgo],[IdClasificacionRiesgo],[IdClasificacionGeneralRiesgo],[IdClasificacionParticularRiesgo]," +
                    "[IdTipoRiesgoOperativo],[Causas],[Consecuencias],[JustificacionCambio],[IdResponsableRiesgo],[IdUsuario],[FechaRegistroRiesgo],[FechaModificacion],[IdTipoEventoOperativo])";
                strConsultaModificacion = string.Format("INSERT INTO [Riesgos].[DetalleModificacionRiesgo] {0} VALUES ({1},'{2}','{3}',{4},{5},{6},{7},'{8}','{9}','{10}',{11},{12},GETDATE(),GETDATE(),{13})", strCamposModificacion,
                    IdRiesgo, strCodigoRiesgo, Nombre, IdClasificacionRiesgo, IdClasificacionGeneralRiesgo, IdClasificacionParticularRiesgo,
                    IdTipoRiesgoOperativo, ListaCausas, ListaConsecuencias, strJustificacion, IdResponsableRiesgo, IdUsuario, IdTipoEventoOperativo);

                strConsultaUpdate = "UPDATE Riesgos.Riesgo SET IdRegion = " + IdRegion + ", IdPais = " + IdPais + ", IdDepartamento = " +
                    IdDepartamento + ", IdCiudad = " + IdCiudad + ", IdOficinaSucursal = " + IdOficinaSucursal + ", IdCadenaValor = " +
                    IdCadenaValor + ", IdMacroproceso = " + IdMacroproceso + ", IdProceso = " + IdProceso + ", IdSubProceso = " +
                    IdSubProceso + ", IdActividad = " + IdActividad + ", IdClasificacionRiesgo = " + IdClasificacionRiesgo + ", IdClasificacionGeneralRiesgo = " +
                    IdClasificacionGeneralRiesgo + ", IdClasificacionParticularRiesgo = " + IdClasificacionParticularRiesgo + ", IdFactorRiesgoOperativo = " +
                    IdFactorRiesgoOperativo + ", IdTipoRiesgoOperativo = " + IdTipoRiesgoOperativo + ", IdTipoEventoOperativo = " +
                    IdTipoEventoOperativo + ", IdRiesgoAsociadoOperativo = " + IdRiesgoAsociadoOperativo + ", ListaRiesgoAsociadoLA = N'" +
                    ListaRiesgoAsociadoLA + "', ListaFactorRiesgoLAFT = N'" + ListaFactorRiesgoLAFT + "', Nombre = N'" + Nombre + "', Descripcion = N'" +
                    Descripcion + "', ListaCausas = N'" + ListaCausas + "', ListaConsecuencias = N'" + ListaConsecuencias + "', IdResponsableRiesgo = " +
                    IdResponsableRiesgo + ", IdProbabilidad = " + IdProbabilidad + ", OcurrenciaEventoDesde = N'" + OcurrenciaEventoDesde +
                    "', OcurrenciaEventoHasta = N'" + OcurrenciaEventoHasta + "', IdImpacto = " + IdImpacto + ", PerdidaEconomicaDesde = N'" +
                    PerdidaEconomicaDesde + "', PerdidaEconomicaHasta = N'" + PerdidaEconomicaHasta + "', ListaTratamiento = N'" + ListaTratamiento +
                    "' WHERE (IdRiesgo = " + IdRiesgo + ")";

                #endregion Consultas
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsultaModificacion + "; " + strConsultaUpdate);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void registrarRiesgo(String IdRegion, String IdPais, String IdDepartamento, String IdCiudad, String IdOficinaSucursal,
            String IdCadenaValor, String IdMacroproceso, String IdProceso, String IdSubProceso, String IdActividad,
            String IdClasificacionRiesgo, String IdClasificacionGeneralRiesgo, String IdClasificacionParticularRiesgo,
            String IdFactorRiesgoOperativo, String IdTipoRiesgoOperativo, String IdTipoEventoOperativo,
            String IdRiesgoAsociadoOperativo, String ListaRiesgoAsociadoLA, String ListaFactorRiesgoLAFT,
            String Codigo, String Nombre, String Descripcion, String ListaCausas, String ListaConsecuencias, String IdResponsableRiesgo,
            String IdProbabilidad, String OcurrenciaEventoDesde, String OcurrenciaEventoHasta, String IdImpacto, String PerdidaEconomicaDesde,
            String PerdidaEconomicaHasta, String ListaTratamiento)
        {
            #region Variables
            string strConsultaInsert = string.Empty, strCamposInsert = string.Empty, strValoresInsert = string.Empty, strCodigoRiesgo = string.Empty, strIdRiesgo = string.Empty;
            /*string strCodigoRiesgo = "(SELECT CASE ISNULL(MAX(IdRiesgo),'') WHEN '' THEN 'R1' ELSE (SELECT 'R'+ CAST((SELECT MAX(CAST(SUBSTRING(Codigo, 2, 10)AS INT)) + 1 FROM Riesgos.Riesgo WHERE Codigo LIKE 'R%') AS NVARCHAR(50))) END FROM Riesgos.Riesgo)",
                strIdRiesgo = "(SELECT CASE ISNULL(MAX(IdRiesgo),'') WHEN '' THEN 1 ELSE (SELECT MAX(CAST(SUBSTRING(Codigo, 2, 10)AS INT)) + 1 FROM Riesgos.Riesgo WHERE Codigo LIKE 'R%') END FROM Riesgos.Riesgo)";*/
            //SELECT top 1 IdRiesgo FROM Riesgos.Riesgo order by IdRiesgo desc
            //string consultaLastId = "SELECT count(IdRiesgo) as IdRiesgo FROM Riesgos.Riesgo";
            string consultaLastId = "SELECT TOP 1 (IdRiesgo) AS UltRiesgo FROM Riesgos.Riesgo ORDER BY IdRiesgo DESC";
            DataTable dtInformacion = new DataTable();
            cDataBase.conectar();
            dtInformacion = cDataBase.ejecutarConsulta(consultaLastId);
            cDataBase.desconectar();

            if (dtInformacion.Rows.Count == 0)
            {
                strIdRiesgo = "1";
                strCodigoRiesgo = "R1";
            }
            else
            {
                int sum = Convert.ToInt32(dtInformacion.Rows[0]["UltRiesgo"].ToString().Trim()) + 1;
                strIdRiesgo = sum.ToString();
                strCodigoRiesgo = "R" + strIdRiesgo;
            }

            string strConsultaModificacion = string.Empty, strCamposModificacion = string.Empty;
            #endregion Variables

            try
            {
                #region Inserts
                strCamposModificacion = "([IdCodigoRiesgo],[CodigoRiesgo],[NombreRiesgo],[IdClasificacionRiesgo],[IdClasificacionGeneralRiesgo],[IdClasificacionParticularRiesgo]," +
                    "[IdTipoRiesgoOperativo],[Causas],[Consecuencias],[JustificacionCambio],[IdResponsableRiesgo],[IdUsuario],[FechaRegistroRiesgo],[FechaModificacion], [IdTipoEventoOperativo])";
                strConsultaModificacion = string.Format("INSERT INTO [Riesgos].[DetalleModificacionRiesgo] {0} VALUES ({1},'{2}','{3}',{4},{5},{6},{7},'{8}','{9}','{10}',{11},{12},GETDATE(),GETDATE(),{13})", strCamposModificacion,
                    strIdRiesgo, strCodigoRiesgo, Nombre, IdClasificacionRiesgo, IdClasificacionGeneralRiesgo, IdClasificacionParticularRiesgo,
                    IdTipoRiesgoOperativo, ListaCausas, ListaConsecuencias, "CREACION DE RIESGO", IdResponsableRiesgo, IdUsuario, IdTipoEventoOperativo);

                strCamposInsert = "(IdRegion, IdPais, IdDepartamento, IdCiudad, IdOficinaSucursal, IdCadenaValor, IdMacroproceso, IdProceso, " +
                    "IdSubProceso, IdActividad, IdClasificacionRiesgo, IdClasificacionGeneralRiesgo, IdClasificacionParticularRiesgo, IdFactorRiesgoOperativo, " +
                    "IdTipoRiesgoOperativo, IdTipoEventoOperativo, IdRiesgoAsociadoOperativo, ListaRiesgoAsociadoLA, ListaFactorRiesgoLAFT, " +
                    "Codigo, Nombre, Descripcion, ListaCausas, ListaConsecuencias, IdResponsableRiesgo, IdProbabilidad, IdProbabilidadResidual, OcurrenciaEventoDesde, " +
                    "OcurrenciaEventoHasta, IdImpacto, IdImpactoResidual, PerdidaEconomicaDesde, PerdidaEconomicaHasta, Anulado, FechaRegistro, IdUsuario, ListaTratamiento)";
                strValoresInsert = "";
                strConsultaInsert = string.Format("INSERT INTO Riesgos.Riesgo {0}  VALUES ({2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},N'{19}',N'{20}','{21}',N'{22}',N'{23}',N'{24}',N'{25}',{26},{27},{28},N'{29}',N'{30}',{31},{32},N'{33}',N'{34}',0,GETDATE(),{35},N'{36}')",
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
                    cDataBase.ejecutarQuery(strConsultaModificacion + "; " + strConsultaInsert);
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
        #endregion Riesgos

        #region Reportes
        public DataTable ReporteRiesgos(String IdCadenaValor, String IdMacroProceso,
            String IdProceso, String IdClasificacionRiesgo, String IdClasificacionGeneralRiesgo,
            String NombreRiesgoInherente, String NombreRiesgoResidual, String IdEmpresa,
            String numeroQuery, String IdRiesgo, String IdArea)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty, strConsulta = string.Empty, strFrom = string.Empty, strSelect = string.Empty;
            #endregion Variables

            try
            {
                #region
                if (IdCadenaValor != "---")
                    condicion = "WHERE (PCV.IdCadenaValor = " + IdCadenaValor + ") ";
                #endregion

                #region
                if (IdMacroProceso != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (PM.IdMacroProceso = " + IdMacroProceso + ") ";
                    else
                        condicion += "AND (PM.IdMacroProceso = " + IdMacroProceso + ") ";
                }
                #endregion

                #region
                if (IdProceso != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (PP.IdProceso = " + IdProceso + ") ";
                    else
                        condicion += "AND (PP.IdProceso = " + IdProceso + ") ";
                }
                #endregion

                #region
                if (IdClasificacionRiesgo != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (PCR.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ") ";
                    else
                        condicion += "AND (PCR.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ") ";
                }
                #endregion

                #region
                if (IdClasificacionGeneralRiesgo != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (PCGR.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ") ";
                    else
                        condicion += "AND (PCGR.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ") ";
                }
                #endregion

                #region
                if (NombreRiesgoInherente != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (PRI.NombreRiesgoInherente = N'" + NombreRiesgoInherente + "') ";
                    else
                        condicion += "AND (PRI.NombreRiesgoInherente = N'" + NombreRiesgoInherente + "') ";
                }
                #endregion

                #region
                if (NombreRiesgoResidual != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (RiesgoResidual.NombreRiesgoInherente = N'" + NombreRiesgoResidual + "') ";
                    else
                        condicion += "AND (RiesgoResidual.NombreRiesgoInherente = N'" + NombreRiesgoResidual + "') ";

                }
                #endregion

                #region Area
                if (IdArea != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (PDJ.IdArea = " + IdArea + ") ";
                    else
                        condicion += "AND (PDJ.IdArea = " + IdArea + ") ";
                }
                #endregion Area

                #region
                if (IdEmpresa != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (PP.IdEmpresa IN (" + IdEmpresa + ", 3)) ";
                    else
                        condicion += "AND (PP.IdEmpresa IN (" + IdEmpresa + ", 3)) ";
                }
                #endregion

                #region
                if (IdRiesgo != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (RR.IdRiesgo = " + IdRiesgo + ") ";
                    else
                        condicion += "AND (RR.IdRiesgo = " + IdRiesgo + ") ";
                }
                #endregion

                #region
                if (string.IsNullOrEmpty(condicion.Trim()))
                    condicion = "WHERE (RR.Anulado = 0) ";
                else
                    condicion += "AND (RR.Anulado = 0) ";
                #endregion

                cDataBase.conectar();
                switch (numeroQuery)
                {
                    case "1":
                        #region Riesgos
                        strSelect = "SELECT LTRIM(RTRIM(RR.Codigo)) CodigoRiesgo, LTRIM(RTRIM(LU.Nombres)) + ' ' + LTRIM(RTRIM(LU.Apellidos)) Usuario, LTRIM(RTRIM(RR.Nombre)) NombreRiesgo, LTRIM(RTRIM(RR.Descripcion)) DescripcionRiesgo, PJO.NombreHijo ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RR.FechaRegistro, 107), ''))) FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(PCR.NombreClasificacionRiesgo, ''))) ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(PCGR.NombreClasificacionGeneralRiesgo, ''))) ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(PCPR.NombreClasificacionParticularRiesgo, ''))) ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(PTEO.NombreTipoEventoOperativo, ''))) TipoEvento, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListCausas](RR.ListaCausas, '|'), ''))) Causas, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListConsecuencias](RR.ListaConsecuencias, '|'), ''))) Consecuencias, LTRIM(RTRIM(ISNULL(PCV.NombreCadenaValor, ''))) CadenaValor, LTRIM(RTRIM(ISNULL(PM.Nombre, ''))) Macroproceso, LTRIM(RTRIM(ISNULL(PP.Nombre, ''))) Proceso, LTRIM(RTRIM(ISNULL(PS.Nombre, ''))) Subproceso, LTRIM(RTRIM(ISNULL(PA.Nombre, ''))) Actividad, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListTreatment](RR.ListaTratamiento, '|'), ''))) ListaTratamiento, LTRIM(RTRIM(ISNULL(PPr.NombreProbabilidad, ''))) FrecuenciaInherente, LTRIM(RTRIM(ISNULL(PPr.ValorProbabilidad, ''))) CodigoFrecuenciaInherente, LTRIM(RTRIM(ISNULL(PIm.NombreImpacto, ''))) ImpactoInherente, LTRIM(RTRIM(ISNULL(PIm.ValorImpacto, ''))) CodigoImpactoInherente, LTRIM(RTRIM(ISNULL(PRI.NombreRiesgoInherente, ''))) RiesgoInherente, LTRIM(RTRIM(ISNULL(PRI.ValorRiesgoInherente, ''))) CodigoRiesgoInherente, LTRIM(RTRIM(ISNULL(pr.NombreProbabilidad, ''))) FrecuenciaResidual, LTRIM(RTRIM(ISNULL(pr.ValorProbabilidad, ''))) CodigoFrecuenciaResidual, LTRIM(RTRIM(ISNULL(im.NombreImpacto, ''))) ImpactoResidual, LTRIM(RTRIM(ISNULL(im.ValorImpacto, ''))) CodigoImpactoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.ValorRiesgoInherente, ''))) CodigoRiesgoResidual,LTRIM(RTRIM(ISNULL(PFRO.NombreFactorRiesgoOperativo, ''))) FactorRiesgoOperativo, LTRIM(RTRIM(ISNULL(PRAO.NombreRiesgoAsociadoOperativo, ''))) RiesgoAsociadoOperativo, LTRIM(RTRIM(ISNULL(PTRO.NombreTipoRiesgoOperativo, ''))) SubFactorRiesgoOperativo, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionList](RR.LISTARIESGOASOCIADOLA, '|'), ''))) ListaRiesgoAsociadoLA, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListLAFT](RR.LISTAFACTORRIESGOLAFT, '|'), ''))) ListaFactorRiesgoLAFT, 'Region:' + LTRIM(RTRIM(ISNULL(PReg.NombreRegion, ''))) + ' - Pais:' + LTRIM(RTRIM(ISNULL(PPai.NombrePais, ''))) + ' - Depto:' + LTRIM(RTRIM(ISNULL([NombreDepartamento], ''))) + ' - Ciudad:' + LTRIM(RTRIM(ISNULL([NombreCiudad], ''))) + ' - Of:' + LTRIM(RTRIM(ISNULL([NombreOficinaSucursal], ''))) Ubicacion,Parea.NombreArea";
                        strFrom = "FROM Riesgos.Riesgo AS RR LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo AS PCGR ON RR.IdClasificacionGeneralRiesgo = PCGR.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo AS PCPR ON RR.IdClasificacionParticularRiesgo = PCPR.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo AS PTRO ON RR.IdTipoRiesgoOperativo = PTRO.IdTipoRiesgoOperativo LEFT JOIN Parametrizacion.TipoEventoOperativo AS PTEO ON RR.IdTipoEventoOperativo = PTEO.IdTipoEventoOperativo LEFT JOIN Procesos.CadenaValor AS PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT JOIN Procesos.Macroproceso AS PM ON RR.IdMacroproceso = PM.IdMacroProceso LEFT JOIN Procesos.Proceso AS PP ON RR.IdProceso = PP.IdProceso LEFT JOIN Procesos.Subproceso AS PS ON PS.IdSubProceso = RR.IdSubProceso LEFT JOIN Procesos.Actividad AS PA ON RR.IdActividad = PA.IdActividad LEFT JOIN Parametrizacion.Probabilidad AS PPr ON PPr.IdProbabilidad = RR.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad AS pr ON pr.IdProbabilidad = RR.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto AS PIm ON PIm.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.Impacto AS im ON im.IdImpacto = RR.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS PJO ON PJO.idHijo = RR.IdResponsableRiesgo LEFT JOIN Parametrizacion.FactorRiesgoOperativo AS PFRO ON RR.IdFactorRiesgoOperativo = PFRO.IdFactorRiesgoOperativo LEFT JOIN Parametrizacion.RiesgoAsociadoOperativo AS PRAO ON RR.IdRiesgoAsociadoOperativo = PRAO.IdRiesgoAsociadoOperativo LEFT JOIN Parametrizacion.Regiones AS PReg  ON RR.IdRegion = PReg.IdRegion LEFT JOIN Parametrizacion.Paises AS PPai ON RR.IdPais = PPai.IdPais AND PReg.IdRegion = PPai.IdRegion LEFT JOIN Parametrizacion.Departamentos AS PDep ON RR.IdDepartamento = PDep.IdDepartamento AND PPai.IdPais  = PDep.IdPais LEFT JOIN Parametrizacion.Ciudades AS PCiu ON RR.IdCiudad = PCiu.IdCiudad  AND PDep.IdDepartamento = PCiu.IdDepartamento LEFT JOIN Parametrizacion.OficinaSucursal AS POSuc ON RR.IdOficinaSucursal = POSuc.IdOficinaSucursal AND PCiu.IdCiudad = POSuc.IdCiudad INNER JOIN Listas.Usuarios LU ON RR.IdUsuario = LU.IdUsuario";
                        strFrom += " left JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS PDJ ON PDJ.idHijo = PJO.idHijo"
                                + " left JOIN Parametrizacion.Area as Parea on Parea.IdArea = PDJ.IdArea";
                        strConsulta = string.Format("{0} {1} {2} ORDER BY RR.IdRiesgo", strSelect, strFrom, condicion);

                        dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                        //dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Riesgos.Riesgo.Codigo)) AS CodigoRiesgo, LTRIM(RTRIM(Riesgos.Riesgo.Nombre)) AS NombreRiesgo, Parametrizacion.JerarquiaOrganizacional.NombreHijo AS ResponsableRiesgo,LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Riesgo.FechaRegistro, 107), ''))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, ''))) AS ClasificacionRiesgo,LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo, ''))) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionParticularRiesgo.NombreClasificacionParticularRiesgo, ''))) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRiesgoOperativo.NombreTipoRiesgoOperativo, ''))) AS TipoRiesgoOperativo, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaCausas, ''))), '|', ',') AS Causas, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaConsecuencias, ''))), '|', ',') AS Consecuencias, LTRIM(RTRIM(ISNULL(Procesos.CadenaValor.NombreCadenaValor, ''))) AS CadenaValor, LTRIM(RTRIM(ISNULL(Procesos.Macroproceso.Nombre, ''))) AS Macroproceso, LTRIM(RTRIM(ISNULL(Procesos.Proceso.Nombre, ''))) AS Proceso, LTRIM(RTRIM(ISNULL(Procesos.Subproceso.Nombre, ''))) AS Subproceso, LTRIM(RTRIM(ISNULL(Procesos.Actividad.Nombre, ''))) AS Actividad, LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.NombreProbabilidad, ''))) AS FrecuenciaInherente,LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.ValorProbabilidad, ''))) AS FrecuenciaInherenteCualitativa, LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.NombreImpacto, ''))) AS ImpactoInherente,  LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.ValorImpacto, ''))) AS ImpactoInherenteCualitativo,LTRIM(RTRIM(ISNULL(Parametrizacion.RiesgoInherente.NombreRiesgoInherente, ''))) AS RiesgoInherente, LTRIM(RTRIM(ISNULL(pr.NombreProbabilidad, ''))) AS FrecuenciaResidual,LTRIM(RTRIM(ISNULL(pr.ValorProbabilidad, ''))) AS FrecuenciaResidualCualitativa,LTRIM(RTRIM(ISNULL(im.NombreImpacto, ''))) AS ImpactoResidual,LTRIM(RTRIM(ISNULL(im.ValorImpacto, ''))) AS ImpactoResidualCualitativo,LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) AS RiesgoResidual FROM Riesgos.Riesgo LEFT JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo ON Riesgos.Riesgo.IdClasificacionGeneralRiesgo = Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo ON Riesgos.Riesgo.IdClasificacionParticularRiesgo = Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo ON Riesgos.Riesgo.IdTipoRiesgoOperativo = Parametrizacion.TipoRiesgoOperativo.IdTipoRiesgoOperativo LEFT JOIN Procesos.CadenaValor ON Procesos.CadenaValor.IdCadenaValor = Riesgos.Riesgo.IdCadenaValor LEFT JOIN Procesos.Macroproceso ON Riesgos.Riesgo.IdMacroproceso = Procesos.Macroproceso.IdMacroProceso LEFT JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso LEFT JOIN Procesos.Subproceso ON Procesos.Subproceso.IdProceso = Riesgos.Riesgo.IdSubProceso LEFT JOIN Procesos.Actividad ON Riesgos.Riesgo.IdActividad = Procesos.Actividad.IdActividad LEFT JOIN Parametrizacion.Probabilidad ON Parametrizacion.Probabilidad.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad pr ON pr.IdProbabilidad = Riesgos.Riesgo.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto ON Parametrizacion.Impacto.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.Impacto im ON im.IdImpacto = Riesgos.Riesgo.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente ON Parametrizacion.RiesgoInherente.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad AND Parametrizacion.RiesgoInherente.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON Riesgos.Riesgo.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad 	AND Riesgos.Riesgo.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Riesgo.IdResponsableRiesgo " + condicion + "ORDER BY Riesgos.Riesgo.IdRiesgo");
                        #endregion
                        break;
                    case "2":
                        #region Riesgos-Controles
                        //strSelect = "SELECT LTRIM(RTRIM(RR.Codigo)) CodigoRiesgo, LTRIM(RTRIM(LU.Nombres)) + ' ' + LTRIM(RTRIM(LU.Apellidos)) Usuario, LTRIM(RTRIM(RR.Nombre)) NombreRiesgo, LTRIM(RTRIM(RR.Descripcion)) DescripcionRiesgo, ResponsableRiesgo.NombreHijo ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RR.FechaRegistro, 107), ''))) FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(PCR.NombreClasificacionRiesgo, ''))) ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(PCGR.NombreClasificacionGeneralRiesgo, ''))) ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(PCPR.NombreClasificacionParticularRiesgo, ''))) ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(PTEO.NombreTipoEventoOperativo, ''))) TipoEvento, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListCausas](RR.ListaCausas, '|'), ''))) Causas, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListConsecuencias](RR.ListaConsecuencias, '|'), ''))) Consecuencias, LTRIM(RTRIM(ISNULL(PCV.NombreCadenaValor, ''))) CadenaValor, LTRIM(RTRIM(ISNULL(PM.Nombre, ''))) Macroproceso, LTRIM(RTRIM(ISNULL(PP.Nombre, ''))) Proceso, LTRIM(RTRIM(ISNULL(PS.Nombre, ''))) Subproceso, LTRIM(RTRIM(ISNULL(PA.Nombre, ''))) Actividad, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListTreatment](RR.ListaTratamiento, '|'), ''))) ListaTratamiento, LTRIM(RTRIM(ISNULL(PPr.NombreProbabilidad, ''))) FrecuenciaInherente, LTRIM(RTRIM(ISNULL(PPr.ValorProbabilidad, ''))) CodigoFrecuenciaInherente, LTRIM(RTRIM(ISNULL(PIm.NombreImpacto, ''))) ImpactoInherente, LTRIM(RTRIM(ISNULL(PIm.ValorImpacto, ''))) CodigoImpactoInherente, LTRIM(RTRIM(ISNULL(PRI.NombreRiesgoInherente, ''))) RiesgoInherente, LTRIM(RTRIM(ISNULL(PRI.ValorRiesgoInherente, ''))) CodigoRiesgoInherente, LTRIM(RTRIM(ISNULL(pr.NombreProbabilidad, ''))) FrecuenciaResidual, LTRIM(RTRIM(ISNULL(pr.ValorProbabilidad, ''))) CodigoFrecuenciaResidual, LTRIM(RTRIM(ISNULL(im.NombreImpacto, ''))) ImpactoResidual, LTRIM(RTRIM(ISNULL(im.ValorImpacto, ''))) CodigoImpactoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.ValorRiesgoInherente, ''))) CodigoRiesgoResidual, LTRIM(RTRIM(ISNULL(RCtrl.CodigoControl, ''))) CodigoControl, LTRIM(RTRIM(ISNULL(RCtrl.NombreControl, ''))) NombreControl, LTRIM(RTRIM(ISNULL(RCtrl.DescripcionControl, ''))) DescripcionControl, RCtrl.ResponsableEjecucion ResponsableControlEjecucion, ResponsableControl.NombreHijo ResponsableControlCalificacion, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RCtrl.FechaRegistro, 107), ''))) FechaRegistroControl, LTRIM(RTRIM(ISNULL(PPer.NombrePeriodicidad, ''))) NombrePeriodicidad, LTRIM(RTRIM(ISNULL(PT.NombreTest, ''))) NombreTest, LTRIM(RTRIM(ISNULL(PCC.NombreClaseControl, ''))) NombreClaseControl, LTRIM(RTRIM(ISNULL(PTC.NombreTipoControl, ''))) NombreTipoControl, LTRIM(RTRIM(ISNULL(PRE.NombreResponsableExperiencia, ''))) NombreResponsableExperiencia, LTRIM(RTRIM(ISNULL(PD.NombreDocumentacion, ''))) NombreDocumentacion, LTRIM(RTRIM(ISNULL(PRes.NombreResponsabilidad, ''))) NombreResponsabilidad, LTRIM(RTRIM(ISNULL(PCCtrl.NombreEscala, ''))) NombreEscala, LTRIM(RTRIM(ISNULL(PMCtrl.NombreMitiga, ''))) NombreMitiga, (CASE WHEN RCR.IdControlesRiesgo IS NULL THEN '' WHEN PMCtrl.IdMitiga = 1 THEN '' ELSE LTRIM(RTRIM(ISNULL(PCCtrl.DesviacionImpacto, '')))END) DesviacionImpacto, (CASE WHEN RCR.IdControlesRiesgo IS NULL THEN '' WHEN PMCtrl.IdMitiga = 2 THEN '' ELSE LTRIM(RTRIM(ISNULL(PCCtrl.DesviacionProbabilidad, ''))) END) DesviacionFrecuencia,LTRIM(RTRIM(ISNULL(PFRO.NombreFactorRiesgoOperativo, ''))) FactorRiesgoOperativo, LTRIM(RTRIM(ISNULL(PRAO.NombreRiesgoAsociadoOperativo, ''))) RiesgoAsociadoOperativo, LTRIM(RTRIM(ISNULL(PTRO.NombreTipoRiesgoOperativo, ''))) SubFactorRiesgoOperativo, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionList](RR.LISTARIESGOASOCIADOLA, '|'), ''))) ListaRiesgoAsociadoLA, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListLAFT](RR.LISTAFACTORRIESGOLAFT, '|'), ''))) ListaFactorRiesgoLAFT, 'Region:' + LTRIM(RTRIM(ISNULL(PReg.NombreRegion, ''))) + ' - Pais:' + LTRIM(RTRIM(ISNULL(PPai.NombrePais, ''))) + ' - Depto:' + LTRIM(RTRIM(ISNULL([NombreDepartamento], ''))) + ' - Ciudad:' + LTRIM(RTRIM(ISNULL([NombreCiudad], ''))) + ' - Of:' + LTRIM(RTRIM(ISNULL([NombreOficinaSucursal], ''))) Ubicacion,Parea.NombreArea";
                        strSelect = "SELECT LTRIM(RTRIM(RR.Codigo)) CodigoRiesgo, LTRIM(RTRIM(LU.Nombres)) + ' ' + LTRIM(RTRIM(LU.Apellidos)) Usuario, LTRIM(RTRIM(RR.Nombre)) NombreRiesgo, LTRIM(RTRIM(RR.Descripcion)) DescripcionRiesgo, ResponsableRiesgo.NombreHijo ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RR.FechaRegistro, 107), ''))) FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(PCR.NombreClasificacionRiesgo, ''))) ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(PCGR.NombreClasificacionGeneralRiesgo, ''))) ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(PCPR.NombreClasificacionParticularRiesgo, ''))) ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(PTEO.NombreTipoEventoOperativo, ''))) TipoEvento, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListCausas](RR.ListaCausas, '|'), ''))) Causas, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListConsecuencias](RR.ListaConsecuencias, '|'), ''))) Consecuencias, LTRIM(RTRIM(ISNULL(PCV.NombreCadenaValor, ''))) CadenaValor, LTRIM(RTRIM(ISNULL(PM.Nombre, ''))) Macroproceso, LTRIM(RTRIM(ISNULL(PP.Nombre, ''))) Proceso, LTRIM(RTRIM(ISNULL(PS.Nombre, ''))) Subproceso, LTRIM(RTRIM(ISNULL(PA.Nombre, ''))) Actividad, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListTreatment](RR.ListaTratamiento, '|'), ''))) ListaTratamiento, LTRIM(RTRIM(ISNULL(PPr.NombreProbabilidad, ''))) FrecuenciaInherente, LTRIM(RTRIM(ISNULL(PPr.ValorProbabilidad, ''))) CodigoFrecuenciaInherente, LTRIM(RTRIM(ISNULL(PIm.NombreImpacto, ''))) ImpactoInherente, LTRIM(RTRIM(ISNULL(PIm.ValorImpacto, ''))) CodigoImpactoInherente, LTRIM(RTRIM(ISNULL(PRI.NombreRiesgoInherente, ''))) RiesgoInherente, LTRIM(RTRIM(ISNULL(PRI.ValorRiesgoInherente, ''))) CodigoRiesgoInherente, LTRIM(RTRIM(ISNULL(pr.NombreProbabilidad, ''))) FrecuenciaResidual, LTRIM(RTRIM(ISNULL(pr.ValorProbabilidad, ''))) CodigoFrecuenciaResidual, LTRIM(RTRIM(ISNULL(im.NombreImpacto, ''))) ImpactoResidual, LTRIM(RTRIM(ISNULL(im.ValorImpacto, ''))) CodigoImpactoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.ValorRiesgoInherente, ''))) CodigoRiesgoResidual, LTRIM(RTRIM(ISNULL(RCtrl.CodigoControl, ''))) CodigoControl, LTRIM(RTRIM(ISNULL(RCtrl.NombreControl, ''))) NombreControl, LTRIM(RTRIM(ISNULL(RCtrl.DescripcionControl, ''))) DescripcionControl, RCtrl.ResponsableEjecucion ResponsableControlEjecucion, ResponsableControl.NombreHijo ResponsableControlCalificacion, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RCtrl.FechaRegistro, 107), ''))) FechaRegistroControl, LTRIM(RTRIM(ISNULL(PPer.NombrePeriodicidad, ''))) NombrePeriodicidad, LTRIM(RTRIM(ISNULL(PT.NombreTest, ''))) NombreTest,/* LTRIM(RTRIM(ISNULL(PCC.NombreClaseControl, ''))) NombreClaseControl, LTRIM(RTRIM(ISNULL(PTC.NombreTipoControl, ''))) NombreTipoControl, LTRIM(RTRIM(ISNULL(PRE.NombreResponsableExperiencia, ''))) NombreResponsableExperiencia, LTRIM(RTRIM(ISNULL(PD.NombreDocumentacion, ''))) NombreDocumentacion, LTRIM(RTRIM(ISNULL(PRes.NombreResponsabilidad, ''))) NombreResponsabilidad*/RCV.NombreVariable,RCV.NombreCategoria, LTRIM(RTRIM(ISNULL(PCCtrl.NombreEscala, ''))) NombreEscala, LTRIM(RTRIM(ISNULL(PMCtrl.NombreMitiga, ''))) NombreMitiga, (CASE WHEN RCR.IdControlesRiesgo IS NULL THEN '' WHEN PMCtrl.IdMitiga = 1 THEN '' ELSE LTRIM(RTRIM(ISNULL(PCCtrl.DesviacionImpacto, '')))END) DesviacionImpacto, (CASE WHEN RCR.IdControlesRiesgo IS NULL THEN '' WHEN PMCtrl.IdMitiga = 2 THEN '' ELSE LTRIM(RTRIM(ISNULL(PCCtrl.DesviacionProbabilidad, ''))) END) DesviacionFrecuencia,LTRIM(RTRIM(ISNULL(PFRO.NombreFactorRiesgoOperativo, ''))) FactorRiesgoOperativo, LTRIM(RTRIM(ISNULL(PRAO.NombreRiesgoAsociadoOperativo, ''))) RiesgoAsociadoOperativo, LTRIM(RTRIM(ISNULL(PTRO.NombreTipoRiesgoOperativo, ''))) SubFactorRiesgoOperativo, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionList](RR.LISTARIESGOASOCIADOLA, '|'), ''))) ListaRiesgoAsociadoLA, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListLAFT](RR.LISTAFACTORRIESGOLAFT, '|'), ''))) ListaFactorRiesgoLAFT, 'Region:' + LTRIM(RTRIM(ISNULL(PReg.NombreRegion, ''))) + ' - Pais:' + LTRIM(RTRIM(ISNULL(PPai.NombrePais, ''))) + ' - Depto:' + LTRIM(RTRIM(ISNULL([NombreDepartamento], ''))) + ' - Ciudad:' + LTRIM(RTRIM(ISNULL([NombreCiudad], ''))) + ' - Of:' + LTRIM(RTRIM(ISNULL([NombreOficinaSucursal], ''))) Ubicacion,Parea.NombreArea";
                        //strFrom = "FROM Riesgos.Riesgo RR LEFT JOIN Parametrizacion.ClasificacionRiesgo PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo PCGR ON RR.IdClasificacionGeneralRiesgo = PCGR.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo PCPR ON RR.IdClasificacionParticularRiesgo = PCPR.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo PTRO ON RR.IdTipoRiesgoOperativo = PTRO.IdTipoRiesgoOperativo LEFT JOIN Parametrizacion.TipoEventoOperativo PTEO ON RR.IdTipoEventoOperativo = PTEO.IdTipoEventoOperativo LEFT JOIN Procesos.CadenaValor PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT JOIN Procesos.Macroproceso PM ON RR.IdMacroproceso = PM.IdMacroProceso LEFT JOIN Procesos.Proceso PP ON RR.IdProceso = PP.IdProceso LEFT JOIN Procesos.Subproceso PS ON PS.IdSubProceso = RR.IdSubProceso LEFT JOIN Procesos.Actividad PA ON RR.IdActividad = PA.IdActividad LEFT JOIN Parametrizacion.Probabilidad PPr ON PPr.IdProbabilidad = RR.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad pr ON pr.IdProbabilidad = RR.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto PIm ON PIm.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.Impacto im ON im.IdImpacto = RR.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Riesgos.ControlesRiesgo RCR ON RR.IdRiesgo = RCR.IdRiesgo LEFT JOIN Riesgos.Control RCtrl ON RCR.IdControl = RCtrl.IdControl LEFT JOIN Parametrizacion.Periodicidad PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad LEFT JOIN Parametrizacion.Test PT ON PT.IdTest = RCtrl.IdTest LEFT JOIN Parametrizacion.ClaseControl PCC ON PCC.IdClaseControl = RCtrl.IdClaseControl LEFT JOIN Parametrizacion.TipoControl PTC ON PTC.IdTipoControl = RCtrl.IdTipoControl LEFT JOIN Parametrizacion.ResponsableExperiencia PRE ON PRE.IdResponsableExperiencia = RCtrl.IdResponsableExperiencia LEFT JOIN Parametrizacion.Documentacion PD ON PD.IdDocumentacion = RCtrl.IdDocumentacion LEFT JOIN Parametrizacion.Responsabilidad PRes ON PRes.IdResponsabilidad = RCtrl.IdResponsabilidad LEFT JOIN Parametrizacion.CalificacionControl PCCtrl ON PCCtrl.IdCalificacionControl = RCtrl.IdCalificacionControl LEFT JOIN Parametrizacion.MitigaControl PMCtrl ON PMCtrl.IdMitiga = RCtrl.IdMitiga LEFT JOIN Parametrizacion.JerarquiaOrganizacional ResponsableRiesgo ON ResponsableRiesgo.idHijo = RR.IdResponsableRiesgo LEFT JOIN Parametrizacion.JerarquiaOrganizacional ResponsableControl ON ResponsableControl.idHijo = RCtrl.Responsable LEFT JOIN Parametrizacion.FactorRiesgoOperativo PFRO ON RR.IdFactorRiesgoOperativo = PFRO.IdFactorRiesgoOperativo LEFT JOIN Parametrizacion.RiesgoAsociadoOperativo PRAO ON RR.IdRiesgoAsociadoOperativo = PRAO.IdRiesgoAsociadoOperativo LEFT JOIN Parametrizacion.Regiones PReg ON RR.IdRegion = PReg.IdRegion LEFT JOIN Parametrizacion.Paises PPai ON RR.IdPais = PPai.IdPais AND PReg.IdRegion = PPai.IdRegion LEFT JOIN Parametrizacion.Departamentos PDep ON RR.IdDepartamento = PDep.IdDepartamento AND PPai.IdPais  = PDep.IdPais LEFT JOIN Parametrizacion.Ciudades PCiu ON RR.IdCiudad = PCiu.IdCiudad AND PDep.IdDepartamento = PCiu.IdDepartamento LEFT JOIN Parametrizacion.OficinaSucursal POSuc ON RR.IdOficinaSucursal = POSuc.IdOficinaSucursal AND PCiu.IdCiudad = POSuc.IdCiudad INNER JOIN Listas.Usuarios LU ON RR.IdUsuario = LU.IdUsuario";
                        strFrom = "FROM Riesgos.Riesgo RR LEFT JOIN Parametrizacion.ClasificacionRiesgo PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo PCGR ON RR.IdClasificacionGeneralRiesgo = PCGR.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo PCPR ON RR.IdClasificacionParticularRiesgo = PCPR.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo PTRO ON RR.IdTipoRiesgoOperativo = PTRO.IdTipoRiesgoOperativo LEFT JOIN Parametrizacion.TipoEventoOperativo PTEO ON RR.IdTipoEventoOperativo = PTEO.IdTipoEventoOperativo LEFT JOIN Procesos.CadenaValor PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT JOIN Procesos.Macroproceso PM ON RR.IdMacroproceso = PM.IdMacroProceso LEFT JOIN Procesos.Proceso PP ON RR.IdProceso = PP.IdProceso LEFT JOIN Procesos.Subproceso PS ON PS.IdSubProceso = RR.IdSubProceso LEFT JOIN Procesos.Actividad PA ON RR.IdActividad = PA.IdActividad LEFT JOIN Parametrizacion.Probabilidad PPr ON PPr.IdProbabilidad = RR.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad pr ON pr.IdProbabilidad = RR.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto PIm ON PIm.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.Impacto im ON im.IdImpacto = RR.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Riesgos.ControlesRiesgo RCR ON RR.IdRiesgo = RCR.IdRiesgo LEFT JOIN Riesgos.Control RCtrl ON RCR.IdControl = RCtrl.IdControl LEFT JOIN Parametrizacion.Periodicidad PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad LEFT JOIN Parametrizacion.Test PT ON PT.IdTest = RCtrl.IdTest /*LEFT JOIN Parametrizacion.ClaseControl PCC ON PCC.IdClaseControl = RCtrl.IdClaseControl LEFT JOIN Parametrizacion.TipoControl PTC ON PTC.IdTipoControl = RCtrl.IdTipoControl LEFT JOIN Parametrizacion.ResponsableExperiencia PRE ON PRE.IdResponsableExperiencia = RCtrl.IdResponsableExperiencia LEFT JOIN Parametrizacion.Documentacion PD ON PD.IdDocumentacion = RCtrl.IdDocumentacion LEFT JOIN Parametrizacion.Responsabilidad PRes ON PRes.IdResponsabilidad = RCtrl.IdResponsabilidad*/ LEFT JOIN Parametrizacion.CalificacionControl PCCtrl ON PCCtrl.IdCalificacionControl = RCtrl.IdCalificacionControl LEFT JOIN Parametrizacion.MitigaControl PMCtrl ON PMCtrl.IdMitiga = RCtrl.IdMitiga LEFT JOIN Parametrizacion.JerarquiaOrganizacional ResponsableRiesgo ON ResponsableRiesgo.idHijo = RR.IdResponsableRiesgo LEFT JOIN Parametrizacion.JerarquiaOrganizacional ResponsableControl ON ResponsableControl.idHijo = RCtrl.Responsable LEFT JOIN Parametrizacion.FactorRiesgoOperativo PFRO ON RR.IdFactorRiesgoOperativo = PFRO.IdFactorRiesgoOperativo LEFT JOIN Parametrizacion.RiesgoAsociadoOperativo PRAO ON RR.IdRiesgoAsociadoOperativo = PRAO.IdRiesgoAsociadoOperativo LEFT JOIN Parametrizacion.Regiones PReg ON RR.IdRegion = PReg.IdRegion LEFT JOIN Parametrizacion.Paises PPai ON RR.IdPais = PPai.IdPais AND PReg.IdRegion = PPai.IdRegion LEFT JOIN Parametrizacion.Departamentos PDep ON RR.IdDepartamento = PDep.IdDepartamento AND PPai.IdPais  = PDep.IdPais LEFT JOIN Parametrizacion.Ciudades PCiu ON RR.IdCiudad = PCiu.IdCiudad AND PDep.IdDepartamento = PCiu.IdDepartamento LEFT JOIN Parametrizacion.OficinaSucursal POSuc ON RR.IdOficinaSucursal = POSuc.IdOficinaSucursal AND PCiu.IdCiudad = POSuc.IdCiudad INNER JOIN Listas.Usuarios LU ON RR.IdUsuario = LU.IdUsuario left join Riesgos.ControlxVariable as RCV on RCV.IdControl = RCtrl.IdControl";
                        strFrom += " left JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS PDJ ON PDJ.idHijo = ResponsableControl.idHijo"
                                + " left JOIN Parametrizacion.Area as Parea on Parea.IdArea = PDJ.IdArea";
                        strConsulta = string.Format("{0} {1} {2} ORDER BY RR.IdRiesgo", strSelect, strFrom, condicion);

                        // Se pasa la condicion a la consulta del procedimiento almacenado
                        List<SqlParameter> parametros = new List<SqlParameter>()
                        {
                            new SqlParameter() { ParameterName = "@SentenciaWhere", SqlDbType = SqlDbType.VarChar, Value =  condicion }
                        };
                        dtInformacion = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[RiesgoControlReporte]", parametros);
                        
                        //dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                        //dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Riesgos.Riesgo.Codigo)) AS CodigoRiesgo, LTRIM(RTRIM(Riesgos.Riesgo.Nombre)) AS NombreRiesgo, ResponsableRiesgo.NombreHijo AS ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Riesgo.FechaRegistro, 107), ''))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, ''))) AS ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo, ''))) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionParticularRiesgo.NombreClasificacionParticularRiesgo, ''))) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRiesgoOperativo.NombreTipoRiesgoOperativo, ''))) AS TipoRiesgoOperativo, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaCausas, ''))), '|', ',') AS Causas, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaConsecuencias, ''))), '|', ',') AS Consecuencias, LTRIM(RTRIM(ISNULL(Procesos.CadenaValor.NombreCadenaValor, ''))) AS CadenaValor, LTRIM(RTRIM(ISNULL(Procesos.Macroproceso.Nombre, ''))) AS Macroproceso, LTRIM(RTRIM(ISNULL(Procesos.Proceso.Nombre, ''))) AS Proceso, LTRIM(RTRIM(ISNULL(Procesos.Subproceso.Nombre, ''))) AS Subproceso, LTRIM(RTRIM(ISNULL(Procesos.Actividad.Nombre, ''))) AS Actividad, LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.NombreProbabilidad, ''))) AS Frecuencia, LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.NombreImpacto, ''))) AS Impacto, LTRIM(RTRIM(ISNULL(Parametrizacion.RiesgoInherente.NombreRiesgoInherente, ''))) AS RiesgoInherente, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) AS RiesgoResidual, LTRIM(RTRIM(ISNULL(Riesgos.Control.CodigoControl, ''))) AS CodigoControl, LTRIM(RTRIM(ISNULL(Riesgos.Control.NombreControl, ''))) AS NombreControl, ResponsableControl.NombreHijo AS ResponsableControl, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Control.FechaRegistro, 107), ''))) AS FechaRegistroControl, LTRIM(RTRIM(ISNULL(Parametrizacion.Periodicidad.NombrePeriodicidad, ''))) AS NombrePeriodicidad, LTRIM(RTRIM(ISNULL(Parametrizacion.Test.NombreTest, ''))) AS NombreTest, LTRIM(RTRIM(ISNULL(Parametrizacion.ClaseControl.NombreClaseControl, ''))) AS NombreClaseControl, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoControl.NombreTipoControl, ''))) AS NombreTipoControl, LTRIM(RTRIM(ISNULL(Parametrizacion.ResponsableExperiencia.NombreResponsableExperiencia, ''))) AS NombreResponsableExperiencia, LTRIM(RTRIM(ISNULL(Parametrizacion.Documentacion.NombreDocumentacion, ''))) AS NombreDocumentacion, LTRIM(RTRIM(ISNULL(Parametrizacion.Responsabilidad.NombreResponsabilidad, ''))) AS NombreResponsabilidad, LTRIM(RTRIM(ISNULL(Parametrizacion.CalificacionControl.NombreEscala, ''))) AS NombreEscala, LTRIM(RTRIM(ISNULL(Parametrizacion.MitigaControl.NombreMitiga, ''))) AS NombreMitiga FROM Riesgos.Riesgo LEFT JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo ON Riesgos.Riesgo.IdClasificacionGeneralRiesgo = Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo ON Riesgos.Riesgo.IdClasificacionParticularRiesgo = Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo ON Riesgos.Riesgo.IdTipoRiesgoOperativo = Parametrizacion.TipoRiesgoOperativo.IdTipoRiesgoOperativo LEFT JOIN Procesos.CadenaValor ON Procesos.CadenaValor.IdCadenaValor = Riesgos.Riesgo.IdCadenaValor LEFT JOIN Procesos.Macroproceso ON Riesgos.Riesgo.IdMacroproceso = Procesos.Macroproceso.IdMacroProceso LEFT JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso LEFT JOIN Procesos.Subproceso ON Procesos.Subproceso.IdProceso = Riesgos.Riesgo.IdSubProceso LEFT JOIN Procesos.Actividad ON Riesgos.Riesgo.IdActividad = Procesos.Actividad.IdActividad LEFT JOIN Parametrizacion.Probabilidad ON Parametrizacion.Probabilidad.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad LEFT JOIN Parametrizacion.Impacto ON Parametrizacion.Impacto.IdImpacto = Riesgos.Riesgo.IdImpacto 
                        //LEFT JOIN Parametrizacion.RiesgoInherente ON Parametrizacion.RiesgoInherente.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad AND Parametrizacion.RiesgoInherente.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON Riesgos.Riesgo.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND Riesgos.Riesgo.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Riesgos.ControlesRiesgo ON Riesgos.Riesgo.IdRiesgo = Riesgos.ControlesRiesgo.IdRiesgo LEFT JOIN Riesgos.Control ON Riesgos.ControlesRiesgo.IdControl = Riesgos.Control.IdControl LEFT JOIN Parametrizacion.Periodicidad ON Parametrizacion.Periodicidad.IdPeriodicidad = Riesgos.Control.IdPeriodicidad LEFT JOIN Parametrizacion.Test ON Parametrizacion.Test.IdTest = Riesgos.Control.IdTest LEFT JOIN Parametrizacion.ClaseControl ON Parametrizacion.ClaseControl.IdClaseControl = Riesgos.Control.IdClaseControl LEFT JOIN Parametrizacion.TipoControl ON Parametrizacion.TipoControl.IdTipoControl = Riesgos.Control.IdTipoControl LEFT JOIN Parametrizacion.ResponsableExperiencia ON Parametrizacion.ResponsableExperiencia.IdResponsableExperiencia = Riesgos.Control.IdResponsableExperiencia LEFT JOIN Parametrizacion.Documentacion ON Parametrizacion.Documentacion.IdDocumentacion = Riesgos.Control.IdDocumentacion LEFT JOIN Parametrizacion.Responsabilidad ON Parametrizacion.Responsabilidad.IdResponsabilidad = Riesgos.Control.IdResponsabilidad LEFT JOIN Parametrizacion.CalificacionControl ON Parametrizacion.CalificacionControl.IdCalificacionControl = Riesgos.Control.IdCalificacionControl LEFT JOIN Parametrizacion.MitigaControl ON Parametrizacion.MitigaControl.IdMitiga = Riesgos.Control.IdMitiga LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableRiesgo ON ResponsableRiesgo.idHijo = Riesgos.Riesgo.IdResponsableRiesgo LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableControl ON ResponsableControl.idHijo = Riesgos.Control.Responsable " + condicion + "ORDER BY Riesgos.Riesgo.IdRiesgo");
                        #endregion

                        break;
                    case "3":
                        #region Riesgos-Eventos
                        strSelect = "SELECT LTRIM(RTRIM(RR.Codigo)) CodigoRiesgo, LTRIM(RTRIM(LU.Nombres)) + ' ' + LTRIM(RTRIM(LU.Apellidos)) Usuario, " +
                            "LTRIM(RTRIM(RR.Nombre)) NombreRiesgo, LTRIM(RTRIM(RR.Descripcion)) DescripcionRiesgo, ResponsableRiesgo.NombreHijo ResponsableRiesgo, " +
                            "LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RR.FechaRegistro, 107), ''))) FechaRegistroRiesgo, " +
                            "LTRIM(RTRIM(ISNULL(PCR.NombreClasificacionRiesgo, ''))) ClasificacionRiesgo, " +
                            "LTRIM(RTRIM(ISNULL(PCGR.NombreClasificacionGeneralRiesgo, ''))) ClasificacionGeneralRiesgo, " +
                            "LTRIM(RTRIM(ISNULL(PCPR.NombreClasificacionParticularRiesgo, ''))) ClasificacionParticularRiesgo, " +
                            "LTRIM(RTRIM(ISNULL(PTRO.NombreTipoRiesgoOperativo, ''))) AS TipoRiesgoOperativo, " +
                            "LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListCausas](RR.ListaCausas, '|'), ''))) Causas, " +
                            "LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListConsecuencias](RR.ListaConsecuencias, '|'), ''))) Consecuencias, " +
                            "LTRIM(RTRIM(ISNULL(PCV.NombreCadenaValor, ''))) CadenaValor, LTRIM(RTRIM(ISNULL(PM.Nombre, ''))) Macroproceso, " +
                            "LTRIM(RTRIM(ISNULL(PP.Nombre, ''))) Proceso, LTRIM(RTRIM(ISNULL(PS.Nombre, ''))) Subproceso, LTRIM(RTRIM(ISNULL(PA.Nombre, ''))) Actividad, " +
                            "LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListTreatment](RR.ListaTratamiento, '|'), ''))) ListaTratamiento, LTRIM(RTRIM(ISNULL(PPr.NombreProbabilidad, ''))) FrecuenciaInherente, LTRIM(RTRIM(ISNULL(PPr.ValorProbabilidad, ''))) CodigoFrecuenciaInherente, LTRIM(RTRIM(ISNULL(PIm.NombreImpacto, ''))) ImpactoInherente, LTRIM(RTRIM(ISNULL(PIm.ValorImpacto, ''))) CodigoImpactoInherente, LTRIM(RTRIM(ISNULL(PRI.NombreRiesgoInherente, ''))) RiesgoInherente, LTRIM(RTRIM(ISNULL(PRI.ValorRiesgoInherente, ''))) CodigoRiesgoInherente, LTRIM(RTRIM(ISNULL(PR.NombreProbabilidad, ''))) FrecuenciaResidual, LTRIM(RTRIM(ISNULL(PR.ValorProbabilidad, ''))) CodigoFrecuenciaResidual, LTRIM(RTRIM(ISNULL(IM.NombreImpacto, ''))) ImpactoResidual, LTRIM(RTRIM(ISNULL(im.ValorImpacto, ''))) CodigoImpactoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.ValorRiesgoInherente, ''))) CodigoRiesgoResidual, LTRIM(RTRIM(ISNULL(RE.CodigoEvento, ''))) CodigoEvento, LTRIM(RTRIM(ISNULL(RE.DescripcionEvento, ''))) DescripcionEvento, ResponsableEvento.NombreHijo ResponsableEvento, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RE.FechaEvento, 107), ''))) FechaRegistroEvento, LTRIM(RTRIM(ISNULL(RE.ProcesoInvolucrado, ''))) ProcesoInvolucrado, LTRIM(RTRIM(ISNULL(RE.AplicativoInvolucrado, ''))) AplicativoInvolucrado, LTRIM(RTRIM(ISNULL(RE.ServicioProductoAfectado, ''))) ServicioProductoAfectado, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RE.FechaInicio, 107), ''))) FechaInicio, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RE.FechaFinalizacion, 107), ''))) FechaFinalizacion, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RE.FechaDescubrimiento, 107), ''))) FechaDescubrimiento, LTRIM(RTRIM(ISNULL(RE.CuentaPUC, ''))) CuentaPUC, LTRIM(RTRIM(ISNULL(RE.ValorRecuperadoTotal, ''))) ValorRecuperadoTotal, LTRIM(RTRIM(ISNULL(RE.ValorRecuperadoSeguro, ''))) ValorRecuperadoSeguro, LTRIM(RTRIM(ISNULL(RE.Observaciones, ''))) Observaciones, LTRIM(RTRIM(ISNULL(PDpto.NombreDepartamento, ''))) NombreDepartamento, LTRIM(RTRIM(ISNULL(PCiu.NombreCiudad, ''))) NombreCiudad, LTRIM(RTRIM(ISNULL(POS.NombreOficinaSucursal, ''))) NombreOficinaSucursal, LTRIM(RTRIM(ISNULL(PCE.NombreClaseEvento, ''))) NombreClaseEvento, LTRIM(RTRIM(ISNULL(PTPE.NombreTipoPerdidaEvento, ''))) NombreTipoPerdidaEvento, LTRIM(RTRIM(ISNULL(PFRO.NombreFactorRiesgoOperativo, ''))) FactorRiesgoOperativo, LTRIM(RTRIM(ISNULL(PRAO.NombreRiesgoAsociadoOperativo, ''))) RiesgoAsociadoOperativo, LTRIM(RTRIM(ISNULL(PTRO.NombreTipoRiesgoOperativo, ''))) SubFactorRiesgoOperativo, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionList](RR.LISTARIESGOASOCIADOLA, '|'), ''))) ListaRiesgoAsociadoLA, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListLAFT](RR.LISTAFACTORRIESGOLAFT, '|'), ''))) ListaFactorRiesgoLAFT, 'Region:' + LTRIM(RTRIM(ISNULL(PReg.NombreRegion, ''))) + ' - Pais:' + LTRIM(RTRIM(ISNULL(PPai.NombrePais, ''))) + ' - Depto:' + LTRIM(RTRIM(ISNULL([NombreDepartamento], ''))) + ' - Ciudad:' + LTRIM(RTRIM(ISNULL([NombreCiudad], ''))) + ' - Of:' + LTRIM(RTRIM(ISNULL([NombreOficinaSucursal], ''))) Ubicacion,LTRIM(RTRIM(ISNULL(PTEO.NombreTipoEventoOperativo, ''))) TipoEvento" +
                            ",Parea.NombreArea";
                        strFrom = "FROM Riesgos.Riesgo RR LEFT JOIN Parametrizacion.ClasificacionRiesgo PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo PCGR ON RR.IdClasificacionGeneralRiesgo = PCGR.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo PCPR ON RR.IdClasificacionParticularRiesgo = PCPR.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo PTRO ON RR.IdTipoRiesgoOperativo = PTRO.IdTipoRiesgoOperativo LEFT JOIN Parametrizacion.TipoEventoOperativo PTEO ON RR.IdTipoEventoOperativo = PTEO.IdTipoEventoOperativo LEFT JOIN Procesos.CadenaValor PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT JOIN Procesos.Macroproceso PM ON RR.IdMacroproceso = PM.IdMacroProceso LEFT JOIN Procesos.Proceso PP ON RR.IdProceso = PP.IdProceso LEFT JOIN Procesos.Subproceso PS ON PS.IdSubProceso = RR.IdSubProceso LEFT JOIN Procesos.Actividad PA ON RR.IdActividad = PA.IdActividad LEFT JOIN Parametrizacion.Probabilidad PPr ON PPr.IdProbabilidad = RR.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad PR ON PR.IdProbabilidad = RR.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto PIm ON PIm.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.Impacto IM ON IM.IdImpacto = RR.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto INNER JOIN Riesgos.EventoRiesgo RER ON RR.IdRiesgo = RER.IdRiesgo INNER JOIN Riesgos.Eventos RE ON RER.IdEvento = RE.IdEvento LEFT JOIN Parametrizacion.Departamentos PDpto ON RE.IdDepartamento = PDpto.IdDepartamento LEFT JOIN Parametrizacion.Ciudades PCiu ON RE.IdCiudad = PCiu.IdCiudad LEFT JOIN Parametrizacion.OficinaSucursal POS ON RE.IdOficinaSucursal = POS.IdOficinaSucursal LEFT JOIN Parametrizacion.ClaseEvento PCE ON RE.IdClaseEvento = PCE.IdClaseEvento LEFT JOIN Parametrizacion.TipoPerdidaEvento PTPE ON RE.IdTipoPerdidaEvento = PTPE.IdTipoPerdidaEvento LEFT JOIN Parametrizacion.JerarquiaOrganizacional ResponsableRiesgo ON ResponsableRiesgo.idHijo = RR.IdResponsableRiesgo LEFT JOIN Parametrizacion.JerarquiaOrganizacional ResponsableEvento ON ResponsableEvento.idHijo = RE.ResponsableEvento LEFT JOIN Parametrizacion.FactorRiesgoOperativo PFRO ON RR.IdFactorRiesgoOperativo = PFRO.IdFactorRiesgoOperativo LEFT JOIN Parametrizacion.RiesgoAsociadoOperativo PRAO ON RR.IdRiesgoAsociadoOperativo = PRAO.IdRiesgoAsociadoOperativo LEFT JOIN Parametrizacion.Regiones PReg ON RR.IdRegion = PReg.IdRegion LEFT JOIN Parametrizacion.Paises PPai ON RR.IdPais = PPai.IdPais AND PReg.IdRegion = PPai.IdRegion INNER JOIN Listas.Usuarios LU ON RR.IdUsuario = LU.IdUsuario";
                        strFrom += " left JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS PDJ ON PDJ.idHijo = ResponsableRiesgo.idHijo"
                                + " left JOIN Parametrizacion.Area as Parea on Parea.IdArea = PDJ.IdArea";
                        strConsulta = string.Format("{0} {1} {2} ORDER BY RR.IdRiesgo", strSelect, strFrom, condicion);

                        dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                        //dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Riesgos.Riesgo.Codigo)) AS CodigoRiesgo, LTRIM(RTRIM(Riesgos.Riesgo.Nombre)) AS NombreRiesgo, ResponsableRiesgo.NombreHijo AS ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Riesgo.FechaRegistro, 107), ''))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, ''))) AS ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo, ''))) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionParticularRiesgo.NombreClasificacionParticularRiesgo, ''))) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRiesgoOperativo.NombreTipoRiesgoOperativo, ''))) AS TipoRiesgoOperativo, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaCausas, ''))), '|', ',') AS Causas, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaConsecuencias, ''))), '|', ',') AS Consecuencias, LTRIM(RTRIM(ISNULL(Procesos.CadenaValor.NombreCadenaValor, ''))) AS CadenaValor, LTRIM(RTRIM(ISNULL(Procesos.Macroproceso.Nombre, ''))) AS Macroproceso, LTRIM(RTRIM(ISNULL(Procesos.Proceso.Nombre, ''))) AS Proceso, LTRIM(RTRIM(ISNULL(Procesos.Subproceso.Nombre, ''))) AS Subproceso, LTRIM(RTRIM(ISNULL(Procesos.Actividad.Nombre, ''))) AS Actividad, LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.NombreProbabilidad, ''))) AS Frecuencia, LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.NombreImpacto, ''))) AS Impacto, LTRIM(RTRIM(ISNULL(Parametrizacion.RiesgoInherente.NombreRiesgoInherente, ''))) AS RiesgoInherente, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) AS RiesgoResidual, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.CodigoEvento, ''))) AS CodigoEvento, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.DescripcionEvento, ''))) AS DescripcionEvento, ResponsableEvento.NombreHijo AS ResponsableEvento, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Eventos.FechaEvento, 107), ''))) AS FechaRegistroEvento, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.ProcesoInvolucrado, ''))) AS ProcesoInvolucrado, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.AplicativoInvolucrado, ''))) AS AplicativoInvolucrado, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.ServicioProductoAfectado, ''))) AS ServicioProductoAfectado, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Eventos.FechaInicio, 107), ''))) AS FechaInicio, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Eventos.FechaFinalizacion, 107), ''))) AS FechaFinalizacion, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Eventos.FechaDescubrimiento, 107), ''))) AS FechaDescubrimiento, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.CuentaPUC, ''))) AS CuentaPUC, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.ValorRecuperadoTotal, ''))) AS ValorRecuperadoTotal, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.ValorRecuperadoSeguro, ''))) AS ValorRecuperadoSeguro, LTRIM(RTRIM(ISNULL(Riesgos.Eventos.Observaciones, ''))) AS Observaciones, LTRIM(RTRIM(ISNULL(Parametrizacion.Departamentos.NombreDepartamento, ''))) AS NombreDepartamento, LTRIM(RTRIM(ISNULL(Parametrizacion.Ciudades.NombreCiudad, ''))) AS NombreCiudad, LTRIM(RTRIM(ISNULL(Parametrizacion.OficinaSucursal.NombreOficinaSucursal, ''))) AS NombreOficinaSucursal, LTRIM(RTRIM(ISNULL(Parametrizacion.ClaseEvento.NombreClaseEvento, ''))) AS NombreClaseEvento, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoPerdidaEvento.NombreTipoPerdidaEvento, ''))) AS NombreTipoPerdidaEvento FROM Riesgos.Riesgo LEFT JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo ON Riesgos.Riesgo.IdClasificacionGeneralRiesgo = Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo ON Riesgos.Riesgo.IdClasificacionParticularRiesgo = Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo ON Riesgos.Riesgo.IdTipoRiesgoOperativo = Parametrizacion.TipoRiesgoOperativo.IdTipoRiesgoOperativo LEFT JOIN Procesos.CadenaValor ON Procesos.CadenaValor.IdCadenaValor = Riesgos.Riesgo.IdCadenaValor LEFT JOIN Procesos.Macroproceso ON Riesgos.Riesgo.IdMacroproceso = Procesos.Macroproceso.IdMacroProceso 
                        //LEFT JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso LEFT JOIN Procesos.Subproceso ON Procesos.Subproceso.IdProceso = Riesgos.Riesgo.IdSubProceso LEFT JOIN Procesos.Actividad ON Riesgos.Riesgo.IdActividad = Procesos.Actividad.IdActividad LEFT JOIN Parametrizacion.Probabilidad ON Parametrizacion.Probabilidad.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad LEFT JOIN Parametrizacion.Impacto ON Parametrizacion.Impacto.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente ON Parametrizacion.RiesgoInherente.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad AND Parametrizacion.RiesgoInherente.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON Riesgos.Riesgo.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND Riesgos.Riesgo.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Riesgos.EventoRiesgo ON Riesgos.Riesgo.IdRiesgo = Riesgos.EventoRiesgo.IdRiesgo LEFT JOIN Riesgos.Eventos ON Riesgos.EventoRiesgo.IdEvento = Riesgos.Eventos.IdEvento LEFT JOIN Parametrizacion.Departamentos ON Riesgos.Eventos.IdDepartamento = Parametrizacion.Departamentos.IdDepartamento LEFT JOIN Parametrizacion.Ciudades ON Riesgos.Eventos.IdCiudad = Parametrizacion.Ciudades.IdCiudad LEFT JOIN Parametrizacion.OficinaSucursal ON Riesgos.Eventos.IdOficinaSucursal = Parametrizacion.OficinaSucursal.IdOficinaSucursal LEFT JOIN Parametrizacion.ClaseEvento ON Riesgos.Eventos.IdClaseEvento = Parametrizacion.ClaseEvento.IdClaseEvento LEFT JOIN Parametrizacion.TipoPerdidaEvento ON Riesgos.Eventos.IdTipoPerdidaEvento = Parametrizacion.TipoPerdidaEvento.IdTipoPerdidaEvento LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableRiesgo ON ResponsableRiesgo.idHijo = Riesgos.Riesgo.IdResponsableRiesgo LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableEvento ON ResponsableEvento.idHijo = Riesgos.Eventos.ResponsableEvento " + condicion + "ORDER BY Riesgos.Riesgo.IdRiesgo");
                        #endregion

                        break;
                    case "4":
                        #region Riesgos-Planes Accion
                        if (string.IsNullOrEmpty(condicion.Trim()))
                            condicion = "WHERE (RPA.IdControlUsuario = 3) ";
                        else
                            condicion += "AND (RPA.IdControlUsuario = 3) ";

                        strSelect = "SELECT LTRIM(RTRIM(RR.Codigo)) CodigoRiesgo, LTRIM(RTRIM(LU.Nombres)) + ' ' + LTRIM(RTRIM(LU.Apellidos)) Usuario, LTRIM(RTRIM(RR.Nombre)) NombreRiesgo, LTRIM(RTRIM(RR.Descripcion)) DescripcionRiesgo, PJO.NombreHijo ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RR.FechaRegistro, 107), ''))) FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(PCR.NombreClasificacionRiesgo, ''))) ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(PCGR.NombreClasificacionGeneralRiesgo, ''))) ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(PCPR.NombreClasificacionParticularRiesgo, ''))) ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(PTEO.NombreTipoEventoOperativo, ''))) TipoEvento, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListCausas](RR.ListaCausas, '|'), ''))) Causas, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListConsecuencias](RR.ListaConsecuencias, '|'), ''))) Consecuencias, LTRIM(RTRIM(ISNULL(PCV.NombreCadenaValor, ''))) CadenaValor, LTRIM(RTRIM(ISNULL(PM.Nombre, ''))) Macroproceso, LTRIM(RTRIM(ISNULL(PP.Nombre, ''))) Proceso, LTRIM(RTRIM(ISNULL(PS.Nombre, ''))) Subproceso, LTRIM(RTRIM(ISNULL(PA.Nombre, ''))) Actividad, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListTreatment](RR.ListaTratamiento, '|'), ''))) ListaTratamiento, LTRIM(RTRIM(ISNULL(PPr.NombreProbabilidad, ''))) FrecuenciaInherente, LTRIM(RTRIM(ISNULL(PPr.ValorProbabilidad, ''))) CodigoFrecuenciaInherente, LTRIM(RTRIM(ISNULL(PIm.NombreImpacto, ''))) ImpactoInherente, LTRIM(RTRIM(ISNULL(PIm.ValorImpacto, ''))) CodigoImpactoInherente, LTRIM(RTRIM(ISNULL(PRI.NombreRiesgoInherente, ''))) RiesgoInherente, LTRIM(RTRIM(ISNULL(PRI.ValorRiesgoInherente, ''))) CodigoRiesgoInherente, LTRIM(RTRIM(ISNULL(PR.NombreProbabilidad, ''))) FrecuenciaResidual, LTRIM(RTRIM(ISNULL(PR.ValorProbabilidad, ''))) CodigoFrecuenciaResidual, LTRIM(RTRIM(ISNULL(IM.NombreImpacto, ''))) ImpactoResidual, LTRIM(RTRIM(ISNULL(IM.ValorImpacto, ''))) CodigoImpactoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.ValorRiesgoInherente, ''))) CodigoRiesgoResidual, LTRIM(RTRIM(ISNULL(RPA.DescripcionAccion, ''))) DescripcionAccion, LTRIM(RTRIM(ISNULL(PTRPA.NombreTipoRecursoPlanAccion, ''))) NombreTipoRecursoPlanAccion, LTRIM(RTRIM(ISNULL(RPA.ValorRecurso, ''))) ValorRecurso, LTRIM(RTRIM(ISNULL(PEPA.NombreEstadoPlanAccion, ''))) NombreEstadoPlanAccion, LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR, RPA.FechaCompromiso, 107), ''))) FechaCompromiso, LTRIM(RTRIM(ISNULL(JOPA.NombreHijo, ''))) ResponsablePlanAccion, LTRIM(RTRIM(ISNULL(PFRO.NombreFactorRiesgoOperativo, ''))) FactorRiesgoOperativo, LTRIM(RTRIM(ISNULL(PRAO.NombreRiesgoAsociadoOperativo, ''))) RiesgoAsociadoOperativo, LTRIM(RTRIM(ISNULL(PTRO.NombreTipoRiesgoOperativo, ''))) SubFactorRiesgoOperativo, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionList](RR.LISTARIESGOASOCIADOLA, '|'), ''))) ListaRiesgoAsociadoLA, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListLAFT](RR.LISTAFACTORRIESGOLAFT, '|'), ''))) ListaFactorRiesgoLAFT, 'Region:' + LTRIM(RTRIM(ISNULL(PReg.NombreRegion, ''))) + ' - Pais:' + LTRIM(RTRIM(ISNULL(PPai.NombrePais, ''))) + ' - Depto:' + LTRIM(RTRIM(ISNULL([NombreDepartamento], ''))) + ' - Ciudad:' + LTRIM(RTRIM(ISNULL([NombreCiudad], ''))) + ' - Of:' + LTRIM(RTRIM(ISNULL([NombreOficinaSucursal], ''))) Ubicacion,Parea.NombreArea";
                        strFrom = "FROM Riesgos.Riesgo AS RR LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo AS PCGR ON RR.IdClasificacionGeneralRiesgo = PCGR.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo AS PCPR ON RR.IdClasificacionParticularRiesgo = PCPR.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo AS PTRO ON RR.IdTipoRiesgoOperativo = PTRO.IdTipoRiesgoOperativo LEFT JOIN Parametrizacion.TipoEventoOperativo AS PTEO ON RR.IdTipoEventoOperativo = PTEO.IdTipoEventoOperativo LEFT JOIN Procesos.CadenaValor AS PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT JOIN Procesos.Macroproceso AS PM ON RR.IdMacroproceso = PM.IdMacroProceso LEFT JOIN Procesos.Proceso AS PP ON RR.IdProceso = PP.IdProceso LEFT JOIN Procesos.Subproceso AS PS ON PS.IdSubProceso = RR.IdSubProceso LEFT JOIN Procesos.Actividad AS PA ON RR.IdActividad = PA.IdActividad LEFT JOIN Parametrizacion.Probabilidad AS PPr ON PPr.IdProbabilidad = RR.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad AS PR ON PR.IdProbabilidad = RR.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto AS PIm ON PIm.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.Impacto AS IM ON IM.IdImpacto = RR.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS PJO ON PJO.idHijo = RR.IdResponsableRiesgo LEFT JOIN Riesgos.PlanesAccion AS RPA ON RR.IdRiesgo = RPA.IdRegistro LEFT JOIN Parametrizacion.TipoRecursoPlanAccion  AS PTRPA ON RPA.IdTipoRecursoPlanAccion = PTRPA.IdTipoRecursoPlanAccion LEFT JOIN Parametrizacion.EstadoPlanAccion AS PEPA ON RPA.IdEstadoPlanAccion = PEPA.IdEstadoPlanAccion LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS JOPA ON JOPA.idHijo = RPA.Responsable LEFT JOIN Parametrizacion.FactorRiesgoOperativo AS PFRO ON RR.IdFactorRiesgoOperativo = PFRO.IdFactorRiesgoOperativo LEFT JOIN Parametrizacion.RiesgoAsociadoOperativo AS PRAO ON RR.IdRiesgoAsociadoOperativo = PRAO.IdRiesgoAsociadoOperativo LEFT JOIN Parametrizacion.Regiones AS PReg  ON RR.IdRegion = PReg.IdRegion LEFT JOIN Parametrizacion.Paises AS PPai ON RR.IdPais = PPai.IdPais AND PReg.IdRegion = PPai.IdRegion LEFT JOIN Parametrizacion.Departamentos AS PDep ON RR.IdDepartamento = PDep.IdDepartamento AND PPai.IdPais  = PDep.IdPais LEFT JOIN Parametrizacion.Ciudades AS PCiu ON RR.IdCiudad = PCiu.IdCiudad  AND PDep.IdDepartamento = PCiu.IdDepartamento LEFT JOIN Parametrizacion.OficinaSucursal AS POSuc ON RR.IdOficinaSucursal = POSuc.IdOficinaSucursal AND PCiu.IdCiudad = POSuc.IdCiudad INNER JOIN Listas.Usuarios LU ON RR.IdUsuario = LU.IdUsuario";
                        strFrom += " left JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS PDJ ON PDJ.idHijo = PJO.idHijo"
                                + " left JOIN Parametrizacion.Area as Parea on Parea.IdArea = PDJ.IdArea";
                        strConsulta = string.Format("{0} {1} {2} ORDER BY RR.IdRiesgo", strSelect, strFrom, condicion);

                        dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                        //dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Riesgos.Riesgo.Codigo)) AS CodigoRiesgo, LTRIM(RTRIM(Riesgos.Riesgo.Nombre)) AS NombreRiesgo, Parametrizacion.JerarquiaOrganizacional.NombreHijo AS ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Riesgo.FechaRegistro, 107), ''))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, ''))) AS ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo, ''))) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionParticularRiesgo.NombreClasificacionParticularRiesgo, ''))) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRiesgoOperativo.NombreTipoRiesgoOperativo, ''))) AS TipoRiesgoOperativo, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaCausas, ''))), '|', ',') AS Causas, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaConsecuencias, ''))), '|', ',') AS Consecuencias, LTRIM(RTRIM(ISNULL(Procesos.CadenaValor.NombreCadenaValor, ''))) AS CadenaValor, LTRIM(RTRIM(ISNULL(Procesos.Macroproceso.Nombre, ''))) AS Macroproceso, LTRIM(RTRIM(ISNULL(Procesos.Proceso.Nombre, ''))) AS Proceso, LTRIM(RTRIM(ISNULL(Procesos.Subproceso.Nombre, ''))) AS Subproceso, LTRIM(RTRIM(ISNULL(Procesos.Actividad.Nombre, ''))) AS Actividad, LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.NombreProbabilidad, ''))) AS Frecuencia, LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.NombreImpacto, ''))) AS Impacto, LTRIM(RTRIM(ISNULL(Parametrizacion.RiesgoInherente.NombreRiesgoInherente, ''))) AS RiesgoInherente, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) AS RiesgoResidual, LTRIM(RTRIM(ISNULL(Riesgos.PlanesAccion.DescripcionAccion, ''))) AS DescripcionAccion, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRecursoPlanAccion.NombreTipoRecursoPlanAccion, ''))) AS NombreTipoRecursoPlanAccion, LTRIM(RTRIM(ISNULL(Riesgos.PlanesAccion.ValorRecurso, ''))) AS ValorRecurso, LTRIM(RTRIM(ISNULL(Parametrizacion.EstadoPlanAccion.NombreEstadoPlanAccion, ''))) AS NombreEstadoPlanAccion, LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.PlanesAccion.FechaCompromiso, 107), ''))) AS FechaCompromiso, LTRIM(RTRIM(ISNULL(JOPA.NombreHijo, ''))) AS ResponsablePlanAccion FROM Riesgos.Riesgo LEFT JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo ON Riesgos.Riesgo.IdClasificacionGeneralRiesgo = Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo ON Riesgos.Riesgo.IdClasificacionParticularRiesgo = Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo ON Riesgos.Riesgo.IdTipoRiesgoOperativo = Parametrizacion.TipoRiesgoOperativo.IdTipoRiesgoOperativo LEFT JOIN Procesos.CadenaValor ON Procesos.CadenaValor.IdCadenaValor = Riesgos.Riesgo.IdCadenaValor LEFT JOIN Procesos.Macroproceso ON Riesgos.Riesgo.IdMacroproceso = Procesos.Macroproceso.IdMacroProceso LEFT JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso LEFT JOIN Procesos.Subproceso ON Procesos.Subproceso.IdProceso = Riesgos.Riesgo.IdSubProceso LEFT JOIN Procesos.Actividad ON Riesgos.Riesgo.IdActividad = Procesos.Actividad.IdActividad LEFT JOIN Parametrizacion.Probabilidad ON Parametrizacion.Probabilidad.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad LEFT JOIN Parametrizacion.Impacto ON Parametrizacion.Impacto.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente ON Parametrizacion.RiesgoInherente.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad AND Parametrizacion.RiesgoInherente.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON Riesgos.Riesgo.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND Riesgos.Riesgo.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Riesgo.IdResponsableRiesgo LEFT JOIN Riesgos.PlanesAccion ON Riesgos.Riesgo.IdRiesgo = Riesgos.PlanesAccion.IdRegistro LEFT JOIN Parametrizacion.TipoRecursoPlanAccion ON Riesgos.PlanesAccion.IdTipoRecursoPlanAccion = Parametrizacion.TipoRecursoPlanAccion.IdTipoRecursoPlanAccion LEFT JOIN Parametrizacion.EstadoPlanAccion ON Riesgos.PlanesAccion.IdEstadoPlanAccion = Parametrizacion.EstadoPlanAccion.IdEstadoPlanAccion LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS JOPA ON JOPA.idHijo = Riesgos.PlanesAccion.Responsable " + condicion + "ORDER BY Riesgos.Riesgo.IdRiesgo");
                        #endregion

                        break;
                }
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
        #endregion Reportes

        #region Calculos
        public DataTable calificacionResidual(String valorProbabilidad, String valorImpacto)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.RiesgoInherente.NombreRiesgoInherente, Parametrizacion.RiesgoInherente.Color FROM Parametrizacion.RiesgoInherente INNER JOIN Parametrizacion.Probabilidad ON Parametrizacion.RiesgoInherente.IdProbabilidad = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.Impacto ON Parametrizacion.RiesgoInherente.IdImpacto = Parametrizacion.Impacto.IdImpacto WHERE (Parametrizacion.Probabilidad.ValorProbabilidad = " + valorProbabilidad + ") AND (Parametrizacion.Impacto.ValorImpacto = " + valorImpacto + ")");
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

        public DataTable calificacionInherente(String IdProbabilidad, String IdImpacto)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ISNULL(NombreRiesgoInherente, '') as NombreRiesgoInherente, Color FROM Parametrizacion.RiesgoInherente WHERE (IdProbabilidad = " + IdProbabilidad + ") AND (IdImpacto = " + IdImpacto + ")");
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

        public String ValorProbabilidad(String IdProbabilidad)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorProbabilidad FROM Parametrizacion.Probabilidad WHERE (IdProbabilidad = " + IdProbabilidad + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0]["ValorProbabilidad"].ToString().Trim();
        }

        public String ValorImpacto(String IdImpacto)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorImpacto FROM Parametrizacion.Impacto WHERE (IdImpacto = " + IdImpacto + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0]["ValorImpacto"].ToString().Trim();
        }

        public DataTable loadInfoRiesgoMasivos()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("SELECT IdRiesgo,Codigo,IdProbabilidad,IdImpacto FROM Riesgos.Riesgo WHERE Anulado = 0");
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

        #endregion Calculos

        #region Metodos Publicos
        /// <summary>
        /// Metodo para obtener los colores de riesgo inherente
        /// </summary>
        /// <returns></returns>
        public DataTable mtdGetColoresRiesgoInherente()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SP_GetColoresRiesgoInherente");
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

        /// <summary>
        /// Metodo para obtener los valores de la tabla Parametrizacion.RiesgoInherente
        /// </summary>
        /// <returns></returns>
        public DataTable mtdGetRiesgoInherente()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SP_GetRiesgoInherente");
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

        /// <summary>
        /// Metodo que ejecuta el procedimiento almacenado para actualizar color del mapa
        /// </summary>
        /// <returns></returns>
        public DataTable mtdActualizaColorMapa(string strId, string strColor)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(string.Format("SP_UpdateColorRiesgo '{0}', '{1}'", strId, strColor));
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

        /// <summary>
        /// Metodo que ejecuta el procedimiento almacenado para cargar los colores
        /// </summary>
        /// <returns></returns>
        public DataTable mtdCargarColoresMapa()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SP_CargarColores");
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

        /// <summary>
        /// Metodo que consulta los riesgos asociados a un control
        /// </summary>
        /// <param name="IdControl">Identificador del control</param>
        /// <returns></returns>
        public DataTable mtdConsultaRiesgosControles(string strIdControl)
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("SELECT Riesgos.Riesgo.IdRiesgo, Riesgos.Riesgo.IdRegion, Riesgos.Riesgo.IdPais, Riesgos.Riesgo.IdDepartamento, Riesgos.Riesgo.IdCiudad, Riesgos.Riesgo.IdOficinaSucursal, Riesgos.Riesgo.IdCadenaValor, Riesgos.Riesgo.IdMacroproceso, Riesgos.Riesgo.IdProceso, Riesgos.Riesgo.IdSubProceso, Riesgos.Riesgo.IdActividad, Riesgos.Riesgo.IdClasificacionRiesgo, Riesgos.Riesgo.IdClasificacionGeneralRiesgo, Riesgos.Riesgo.IdClasificacionParticularRiesgo, Riesgos.Riesgo.IdFactorRiesgoOperativo, Riesgos.Riesgo.IdTipoRiesgoOperativo, Riesgos.Riesgo.IdTipoEventoOperativo, Riesgos.Riesgo.IdRiesgoAsociadoOperativo, Riesgos.Riesgo.ListaRiesgoAsociadoLA, Riesgos.Riesgo.ListaFactorRiesgoLAFT, Riesgos.Riesgo.Codigo, Riesgos.Riesgo.Nombre, Riesgos.Riesgo.Descripcion, Riesgos.Riesgo.ListaCausas, Riesgos.Riesgo.ListaConsecuencias, Riesgos.Riesgo.IdResponsableRiesgo, Riesgos.Riesgo.IdProbabilidad, Riesgos.Riesgo.OcurrenciaEventoDesde, Riesgos.Riesgo.OcurrenciaEventoHasta, Riesgos.Riesgo.IdImpacto, Riesgos.Riesgo.PerdidaEconomicaDesde, Riesgos.Riesgo.PerdidaEconomicaHasta, Riesgos.Riesgo.FechaRegistro, LTRIM(RTRIM(Listas.Usuarios.Nombres)) + ' ' + LTRIM(RTRIM(Listas.Usuarios.Apellidos)) AS Nombres, Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, Parametrizacion.JerarquiaOrganizacional.NombreHijo, Riesgos.Riesgo.ListaTratamiento FROM Riesgos.Riesgo INNER JOIN Listas.Usuarios ON Riesgos.Riesgo.IdUsuario = Listas.Usuarios.IdUsuario INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo INNER JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Riesgo.IdResponsableRiesgo INNER JOIN Riesgos.ControlesRiesgo ON Riesgos.Riesgo.IdRiesgo = Riesgos.ControlesRiesgo.IdRiesgo WHERE (Riesgos.Riesgo.Anulado = 0) AND Riesgos.ControlesRiesgo.IdControl = {0}", strIdControl);

                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                dtInfo = null;
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return dtInfo;
        }

        public void mtdActualizarRiesgosControles(string strIdControl)
        {
            #region Variables
            DataTable dtInfoRiesgosControl = new DataTable(), dtInfoControl = new DataTable();
            double dblPromedioControl = 0, dblValorProbabilidad = 0, dblValorImpacto = 0, dblValorFinalProbabilidad = 0, dblValorFinalImpacto = 0,
                dblDesviacionProbabilidad = 0, dblDesviacionImpacto = 0;
            #endregion Variables

            dtInfoRiesgosControl = mtdConsultaRiesgosControles(strIdControl);

            if (dtInfoRiesgosControl != null)
            {
                foreach (DataRow drRiesgos in dtInfoRiesgosControl.Rows)
                {
                    dtInfoControl = new DataTable();
                    dblPromedioControl = 0;
                    dblValorProbabilidad = Convert.ToDouble(ValorProbabilidad(drRiesgos["IdProbabilidad"].ToString().Trim()));
                    dblValorImpacto = Convert.ToDouble(ValorImpacto(drRiesgos["IdImpacto"].ToString().Trim()));

                    //dtInfoControl = mtdInfoControlRiesgo(drRiesgos["IdRiesgo"].ToString().Trim(), strIdControl);
                    dtInfoControl = loadInfoControlesRiesgo(drRiesgos["IdRiesgo"].ToString().Trim());

                    if (dtInfoControl.Rows.Count > 0)
                    {
                        dblValorFinalProbabilidad = 0;
                        dblValorFinalImpacto = 0;

                        #region Calculo de Desviaciones (probabilidad e impacto)
                        foreach (DataRow drControl in dtInfoControl.Rows)
                        {
                            dblDesviacionProbabilidad = 0;
                            dblDesviacionImpacto = 0;
                            dblPromedioControl = dblPromedioControl + Convert.ToDouble(drControl["CalificacionControl"].ToString().Trim());

                            switch (drControl["IdMitiga"].ToString().Trim())
                            {
                                case "1":
                                    dblDesviacionProbabilidad = dblValorProbabilidad - Convert.ToDouble(drControl["DesviacionProbabilidad"].ToString().Trim());
                                    dblDesviacionImpacto = dblValorImpacto;
                                    break;
                                case "2":
                                    dblDesviacionProbabilidad = dblValorProbabilidad;
                                    dblDesviacionImpacto = dblValorImpacto - Convert.ToDouble(drControl["DesviacionImpacto"].ToString().Trim());
                                    break;
                                case "3":
                                    dblDesviacionProbabilidad = dblValorProbabilidad - Convert.ToDouble(drControl["DesviacionProbabilidad"].ToString().Trim());
                                    dblDesviacionImpacto = dblValorImpacto - Convert.ToDouble(drControl["DesviacionImpacto"].ToString().Trim());
                                    break;
                            }

                            if (dblDesviacionProbabilidad <= 0)
                                dblDesviacionProbabilidad = 1;
                            if (dblDesviacionImpacto <= 0)
                                dblDesviacionImpacto = 1;

                            dblValorFinalProbabilidad += dblDesviacionProbabilidad;
                            dblValorFinalImpacto += dblDesviacionImpacto;
                        }
                        #endregion Calculo de Desviaciones (probabilidad e impacto)

                        dblPromedioControl = (dblPromedioControl / dtInfoControl.Rows.Count);
                    }

                    #region Calculo de Probabilidad
                    dblValorProbabilidad = (dblValorFinalProbabilidad / dtInfoControl.Rows.Count);
                    if (dblValorProbabilidad == 1.5)
                    {
                        dblValorProbabilidad = 1.0;
                    }
                    else if (dblValorProbabilidad == 2.5)
                    {
                        dblValorProbabilidad = 2.0;
                    }
                    else if (dblValorProbabilidad == 3.5)
                    {
                        dblValorProbabilidad = 3.0;
                    }
                    else if (dblValorProbabilidad == 4.5)
                    {
                        dblValorProbabilidad = 4.0;
                    }
                    else if (dblValorProbabilidad == 5.5)
                    {
                        dblValorProbabilidad = 5.0;
                    }
                    else
                        dblValorProbabilidad = Math.Round(dblValorFinalProbabilidad / dtInfoControl.Rows.Count);
                    #endregion Calculo de Probabilidad

                    #region Calculo del Impacto
                    dblValorImpacto = (dblValorFinalImpacto / dtInfoControl.Rows.Count);

                    if (dblValorImpacto == 1.5)
                    {
                        dblValorImpacto = 1.0;
                    }
                    else if (dblValorImpacto == 2.5)
                    {
                        dblValorImpacto = 2.0;
                    }
                    else if (dblValorImpacto == 3.5)
                    {
                        dblValorImpacto = 3.0;
                    }
                    else if (dblValorImpacto == 4.5)
                    {
                        dblValorImpacto = 4.0;
                    }
                    else if (dblValorImpacto == 5.5)
                    {
                        dblValorImpacto = 5.0;
                    }
                    else
                        dblValorImpacto = Math.Round(dblValorFinalImpacto / dtInfoControl.Rows.Count);
                    #endregion Calculo del Impacto

                    actualizarRiesgoResidual(dblValorProbabilidad.ToString().Trim(), dblValorImpacto.ToString().Trim(), drRiesgos["IdRiesgo"].ToString().Trim());
                }
            }
        }

        /// <summary>
        /// Metodo que trae la informacion de control-riesgo de acuerdo a al identificador de riesgo y de control 
        /// </summary>
        /// <param name="strIdRiesgo"></param>
        /// <param name="strIdControl"></param>
        /// <returns></returns>
        public DataTable mtdInfoControlRiesgo(string strIdRiesgo, string strIdControl)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("SELECT RCR.IdControlesRiesgo, RC.IdControl, RC.CodigoControl, RC.NombreControl, PT.NombreTest, ((((PCC.ValorClaseControl) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 1))) + ((PTC.ValorTipoControl) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 2))) + ((PRC.ValorResponsableExperiencia) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 3))) + ((PD.ValorDocumentacion) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 4))) + ((PC.ValorResponsabilidad) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 5)))) / 100) AS CalificacionControl, PCFC.DesviacionProbabilidad, PCFC.DesviacionImpacto, RC.IdMitiga FROM Riesgos.Control RC INNER JOIN Parametrizacion.ClaseControl PCC ON RC.IdClaseControl = PCC.IdClaseControl INNER JOIN Parametrizacion.TipoControl PTC ON RC.IdTipoControl = PTC.IdTipoControl INNER JOIN Parametrizacion.ResponsableExperiencia PRC ON RC.IdResponsableExperiencia = PRC.IdResponsableExperiencia INNER JOIN Parametrizacion.Documentacion PD ON RC.IdDocumentacion = PD.IdDocumentacion INNER JOIN Parametrizacion.Responsabilidad PC ON RC.IdResponsabilidad = PC.IdResponsabilidad INNER JOIN Riesgos.ControlesRiesgo RCR ON RC.IdControl = RCR.IdControl INNER JOIN Parametrizacion.Test PT ON RC.IdTest = PT.IdTest INNER JOIN Parametrizacion.CalificacionControl PCFC ON RC.IdCalificacionControl = PCFC.IdCalificacionControl WHERE (RCR.IdRiesgo = {0}) AND (RCR.IdControl = {1})",
                    strIdRiesgo, strIdControl);
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

        public DataTable mtdReporteCambiosControlRiesgo(string strOpcionReporte,
            string strFechaInicial, string strFechaFinal, String IdArea)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strCondicion = string.Empty, strConsulta = string.Empty, strFrom = string.Empty, strSelect = string.Empty;
            #endregion Variables

            try
            {
                #region Generacion de la consulta
                switch (strOpcionReporte)
                {
                    case "1": //Controles
                              //strSelect = "SELECT LTRIM(RTRIM(ISNULL(RDMC.CodigoControl, ''))) AS CodigoControl, LTRIM(RTRIM(ISNULL(RDMC.NombreControl, ''))) AS NombreControl, RespControl.NombreHijo AS ResponsableControl, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RDMC.FechaRegistroControl, 107), ''))) AS FechaRegistroControl, LTRIM(RTRIM(ISNULL(PP.NombrePeriodicidad, ''))) AS NombrePeriodicidad, LTRIM(RTRIM(ISNULL(PT.NombreTest, ''))) AS NombreTest, LTRIM(RTRIM(ISNULL(PCLC.NombreClaseControl, ''))) AS NombreClaseControl, LTRIM(RTRIM(ISNULL(PTC.NombreTipoControl, ''))) AS NombreTipoControl, LTRIM(RTRIM(ISNULL(PRE.NombreResponsableExperiencia, ''))) AS NombreResponsableExperiencia, LTRIM(RTRIM(ISNULL(PD.NombreDocumentacion, ''))) AS NombreDocumentacion, LTRIM(RTRIM(ISNULL(PR.NombreResponsabilidad, ''))) AS NombreResponsabilidad, LTRIM(RTRIM(ISNULL(PCC.NombreEscala, ''))) AS NombreEscala, LTRIM(RTRIM(ISNULL(PMC.NombreMitiga, ''))) AS NombreMitiga, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RDMC.FechaModificacion, 107),''))) AS FechaModificacion, LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuarioCambio, LTRIM(RTRIM(ISNULL(RDMC.JustificacionCambio,''))) AS JustificacionCambio,  LTRIM(RTRIM(ISNULL(RC.DescripcionControl, ''))) DescripcionControl, Parea.NombreArea";
                        strSelect = "SELECT LTRIM(RTRIM(ISNULL(RDMC.CodigoControl, ''))) AS CodigoControl, LTRIM(RTRIM(ISNULL(RDMC.NombreControl, ''))) AS NombreControl, RespControl.NombreHijo AS ResponsableControl, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RDMC.FechaRegistroControl, 107), ''))) AS FechaRegistroControl, LTRIM(RTRIM(ISNULL(PP.NombrePeriodicidad, ''))) AS NombrePeriodicidad, LTRIM(RTRIM(ISNULL(PT.NombreTest, ''))) AS NombreTest, /*LTRIM(RTRIM(ISNULL(PCLC.NombreClaseControl, ''))) AS NombreClaseControl, LTRIM(RTRIM(ISNULL(PTC.NombreTipoControl, ''))) AS NombreTipoControl, LTRIM(RTRIM(ISNULL(PRE.NombreResponsableExperiencia, ''))) AS NombreResponsableExperiencia, LTRIM(RTRIM(ISNULL(PD.NombreDocumentacion, ''))) AS NombreDocumentacion, LTRIM(RTRIM(ISNULL(PR.NombreResponsabilidad, ''))) AS NombreResponsabilidad,*/ RCV.NombreVariable,RCV.NombreCategoria, LTRIM(RTRIM(ISNULL(PCC.NombreEscala, ''))) AS NombreEscala, LTRIM(RTRIM(ISNULL(PMC.NombreMitiga, ''))) AS NombreMitiga, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RDMC.FechaModificacion, 107),''))) AS FechaModificacion, LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuarioCambio, LTRIM(RTRIM(ISNULL(RDMC.JustificacionCambio,''))) AS JustificacionCambio,  LTRIM(RTRIM(ISNULL(RC.DescripcionControl, ''))) DescripcionControl, Parea.NombreArea";
                        strFrom = //"FROM Riesgos.DetalleModificacionControl AS RDMC LEFT JOIN Parametrizacion.Periodicidad AS PP ON PP.IdPeriodicidad = RDMC.IdPeriodicidad LEFT JOIN Parametrizacion.Test AS PT ON PT.IdTest = RDMC.IdTest LEFT JOIN Parametrizacion.ClaseControl PCLC ON PCLC.IdClaseControl = RDMC.IdClaseControl LEFT JOIN Parametrizacion.TipoControl AS PTC ON PTC.IdTipoControl = RDMC.IdTipoControl LEFT JOIN Parametrizacion.ResponsableExperiencia AS PRE ON PRE.IdResponsableExperiencia = RDMC.IdResponsableExperiencia LEFT JOIN Parametrizacion.Documentacion PD ON PD.IdDocumentacion = RDMC.IdDocumentacion LEFT JOIN Parametrizacion.Responsabilidad PR ON PR.IdResponsabilidad = RDMC.IdResponsabilidad LEFT JOIN Parametrizacion.CalificacionControl AS PCC ON PCC.IdCalificacionControl = RDMC.IdCalificacionControl LEFT JOIN Parametrizacion.MitigaControl AS PMC ON PMC.IdMitiga = RDMC.IdMitiga LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS RespControl ON RespControl.idHijo = RDMC.IdResponsableControl LEFT JOIN Listas.Usuarios AS LU ON LU.IdUsuario = RDMC.IdUsuario LEFT JOIN [Riesgos].[Control] RC ON RC.IdControl = RDMC.IdCodigoControl"
                            "FROM Riesgos.DetalleModificacionControl AS RDMC LEFT JOIN Parametrizacion.Periodicidad AS PP ON PP.IdPeriodicidad = RDMC.IdPeriodicidad LEFT JOIN Parametrizacion.Test AS PT ON PT.IdTest = RDMC.IdTest /*LEFT JOIN Parametrizacion.ClaseControl PCLC ON PCLC.IdClaseControl = RDMC.IdClaseControl LEFT JOIN Parametrizacion.TipoControl AS PTC ON PTC.IdTipoControl = RDMC.IdTipoControl LEFT JOIN Parametrizacion.ResponsableExperiencia AS PRE ON PRE.IdResponsableExperiencia = RDMC.IdResponsableExperiencia LEFT JOIN Parametrizacion.Documentacion PD ON PD.IdDocumentacion = RDMC.IdDocumentacion LEFT JOIN Parametrizacion.Responsabilidad PR ON PR.IdResponsabilidad = RDMC.IdResponsabilidad*/left join Riesgos.ControlxVariable as RCV on RCV.IdControl = RDMC.IdCodigoControl  LEFT JOIN Parametrizacion.CalificacionControl AS PCC ON PCC.IdCalificacionControl = RDMC.IdCalificacionControl LEFT JOIN Parametrizacion.MitigaControl AS PMC ON PMC.IdMitiga = RDMC.IdMitiga LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS RespControl ON RespControl.idHijo = RDMC.IdResponsableControl LEFT JOIN Listas.Usuarios AS LU ON LU.IdUsuario = RDMC.IdUsuario LEFT JOIN [Riesgos].[Control] RC ON RC.IdControl = RDMC.IdCodigoControl"
                            + " left JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS PDJ ON PDJ.idHijo = RespControl.idHijo"
                            + " left JOIN Parametrizacion.Area as Parea on Parea.IdArea = PDJ.IdArea";

                        #region Filtros
                        if (!string.IsNullOrEmpty(strFechaInicial.Trim()))
                        {
                            if (!string.IsNullOrEmpty(strFechaFinal.Trim()))
                                strCondicion = string.Format("WHERE CONVERT(VARCHAR(19), RDMC.FechaModificacion, 121)  BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'", strFechaInicial, strFechaFinal);
                            else
                                strCondicion = string.Format("WHERE CONVERT(VARCHAR(19), RDMC.FechaModificacion, 121)  >= '{0} 00:00:00'", strFechaInicial);
                        }
                        else
                            strCondicion = string.Empty;

                        #region Area
                        if (IdArea != "---")
                        {
                            if (string.IsNullOrEmpty(strCondicion.Trim()))
                                strCondicion = "WHERE (PDJ.IdArea = " + IdArea + ") ";
                            else
                                strCondicion += "AND (PDJ.IdArea = " + IdArea + ") ";
                        }
                        #endregion Area
                        #endregion Filtros

                        strConsulta = string.Format("{0} {1} {2}", strSelect, strFrom, strCondicion);
                        break;
                    case "2": //Riesgos
                        strSelect = "SELECT LTRIM(RTRIM(RDMR.CodigoRiesgo)) AS CodigoRiesgo, LTRIM(RTRIM(RDMR.NombreRiesgo)) AS NombreRiesgo, ResponsableRiesgo.NombreHijo AS ResponsableRiesgo, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RDMR.FechaRegistroRiesgo, 107), ''))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(PCR.NombreClasificacionRiesgo, ''))) AS ClasificacionRiesgo, LTRIM(RTRIM(ISNULL(PCGR.NombreClasificacionGeneralRiesgo, ''))) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(PCPR.NombreClasificacionParticularRiesgo, ''))) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(PTEO.NombreTipoEventoOperativo, ''))) AS TipoEvento, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListCausas](RDMR.Causas, '|'), ''))) AS Causas, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListConsecuencias](RDMR.Consecuencias, '|'), ''))) AS Consecuencias, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RDMR.FechaModificacion, 107),''))) AS FechaModificacion, LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuarioCambio, LTRIM(RTRIM(ISNULL(RDMR.JustificacionCambio,''))) AS JustificacionCambio, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListTreatment](RR.ListaTratamiento, '|'), ''))) AS ListaTratamiento, LTRIM(RTRIM(ISNULL(PCV.NombreCadenaValor, ''))) AS CadenaValor, LTRIM(RTRIM(ISNULL(PM.Nombre, ''))) AS Macroproceso, LTRIM(RTRIM(ISNULL(PP.Nombre, ''))) AS Proceso, LTRIM(RTRIM(ISNULL(PS.Nombre, ''))) AS Subproceso, LTRIM(RTRIM(ISNULL(PA.Nombre, ''))) AS Actividad, LTRIM(RTRIM(ISNULL(PPr.NombreProbabilidad, ''))) AS FrecuenciaInherente, LTRIM(RTRIM(ISNULL(PPr.ValorProbabilidad, ''))) AS CodigoFrecuenciaInherente, LTRIM(RTRIM(ISNULL(PIm.NombreImpacto, ''))) AS ImpactoInherente, LTRIM(RTRIM(ISNULL(PIm.ValorImpacto, ''))) AS CodigoImpactoInherente, LTRIM(RTRIM(ISNULL(PRI.NombreRiesgoInherente, ''))) AS RiesgoInherente, LTRIM(RTRIM(ISNULL(PRI.ValorRiesgoInherente, ''))) AS CodigoRiesgoInherente, LTRIM(RTRIM(ISNULL(pr.NombreProbabilidad, ''))) AS FrecuenciaResidual, LTRIM(RTRIM(ISNULL(pr.ValorProbabilidad, ''))) AS CodigoFrecuenciaResidual, LTRIM(RTRIM(ISNULL(IM.NombreImpacto, ''))) AS ImpactoResidual, LTRIM(RTRIM(ISNULL(IM.ValorImpacto, ''))) AS CodigoImpactoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) AS RiesgoResidual, LTRIM(RTRIM(ISNULL(RiesgoResidual.ValorRiesgoInherente, ''))) AS CodigoRiesgoResidual, LTRIM(RTRIM(ISNULL(PFRO.NombreFactorRiesgoOperativo, ''))) AS FactorRiesgoOperativo, LTRIM(RTRIM(ISNULL(PRAO.NombreRiesgoAsociadoOperativo, ''))) AS RiesgoAsociadoOperativo, LTRIM(RTRIM(ISNULL(PTRO.NombreTipoRiesgoOperativo, ''))) AS SubFactorRiesgoOperativo, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionList](RR.LISTARIESGOASOCIADOLA, '|'), ''))) AS ListaRiesgoAsociadoLA, LTRIM(RTRIM(ISNULL([Procesos].[FnDescriptionListLAFT](RR.LISTAFACTORRIESGOLAFT, '|'), ''))) AS ListaFactorRiesgoLAFT, 'Region:' + LTRIM(RTRIM(ISNULL(PReg.NombreRegion, ''))) + ' - Pais:' + LTRIM(RTRIM(ISNULL(PPai.NombrePais, ''))) + ' - Depto:' + LTRIM(RTRIM(ISNULL([NombreDepartamento], '')))   + ' - Ciudad:' + LTRIM(RTRIM(ISNULL([NombreCiudad], ''))) + ' - Of:' + LTRIM(RTRIM(ISNULL([NombreOficinaSucursal], '')))  AS Ubicacion,  LTRIM(RTRIM(ISNULL(RR.Descripcion, ''))) DescripcionRiesgo, Parea.NombreArea";
                        strFrom = "FROM Riesgos.DetalleModificacionRiesgo AS RDMR LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RDMR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo AS PCGR ON RDMR.IdClasificacionGeneralRiesgo = PCGR.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo AS PCPR ON RDMR.IdClasificacionParticularRiesgo = PCPR.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo AS PTRO ON RDMR.IdTipoRiesgoOperativo = PTRO.IdTipoRiesgoOperativo LEFT JOIN Parametrizacion.TipoEventoOperativo AS PTEO ON RDMR.IdTipoEventoOperativo = PTEO.IdTipoEventoOperativo LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableRiesgo ON ResponsableRiesgo.idHijo = RDMR.IdResponsableRiesgo LEFT JOIN Listas.Usuarios AS LU ON LU.IdUsuario = RDMR.IdUsuario INNER JOIN Riesgos.Riesgo RR ON RR.Codigo = RDMR.CodigoRiesgo LEFT JOIN Procesos.CadenaValor AS PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT JOIN Procesos.Macroproceso AS PM ON RR.IdMacroproceso = PM.IdMacroProceso LEFT JOIN Procesos.Proceso AS PP ON RR.IdProceso = PP.IdProceso LEFT JOIN Procesos.Subproceso AS PS ON PS.IdSubProceso = RR.IdSubProceso LEFT JOIN Procesos.Actividad AS PA ON RR.IdActividad = PA.IdActividad LEFT JOIN Parametrizacion.Probabilidad AS PPr ON PPr.IdProbabilidad = RR.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad AS pr ON pr.IdProbabilidad = RR.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto AS PIm ON PIm.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.Impacto AS IM ON IM.IdImpacto = RR.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.FactorRiesgoOperativo AS PFRO ON RR.IdFactorRiesgoOperativo = PFRO.IdFactorRiesgoOperativo LEFT JOIN Parametrizacion.RiesgoAsociadoOperativo AS PRAO ON RR.IdRiesgoAsociadoOperativo = PRAO.IdRiesgoAsociadoOperativo LEFT JOIN Parametrizacion.Regiones AS PReg  ON RR.IdRegion = PReg.IdRegion LEFT JOIN Parametrizacion.Paises AS PPai ON RR.IdPais = PPai.IdPais AND PReg.IdRegion = PPai.IdRegion LEFT JOIN Parametrizacion.Departamentos AS PDep ON RR.IdDepartamento = PDep.IdDepartamento AND PPai.IdPais  = PDep.IdPais LEFT JOIN Parametrizacion.Ciudades AS PCiu ON RR.IdCiudad = PCiu.IdCiudad  AND PDep.IdDepartamento = PCiu.IdDepartamento LEFT JOIN Parametrizacion.OficinaSucursal AS POSuc ON RR.IdOficinaSucursal = POSuc.IdOficinaSucursal AND PCiu.IdCiudad = POSuc.IdCiudad"
                            + " left JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS PDJ ON PDJ.idHijo = ResponsableRiesgo.idHijo"
                            + " left JOIN Parametrizacion.Area as Parea on Parea.IdArea = PDJ.IdArea";
                        #region Filtros
                        if (!string.IsNullOrEmpty(strFechaInicial.Trim()))
                        {
                            if (!string.IsNullOrEmpty(strFechaFinal.Trim()))
                                strCondicion = string.Format("WHERE CONVERT(VARCHAR(19), RDMR.FechaModificacion, 121)  BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'", strFechaInicial, strFechaFinal);
                            else
                                strCondicion = string.Format("WHERE CONVERT(VARCHAR(19), RDMR.FechaModificacion, 121)  >= '{0} 00:00:00'", strFechaInicial);
                        }
                        else
                            strCondicion = string.Empty;
                        #endregion Filtros
                        #region Area
                        if (IdArea != "---")
                        {
                            if (string.IsNullOrEmpty(strCondicion.Trim()))
                                strCondicion = "WHERE (PDJ.IdArea = " + IdArea + ") ";
                            else
                                strCondicion += "AND (PDJ.IdArea = " + IdArea + ") ";
                        }
                        #endregion Area

                        strConsulta = string.Format("{0} {1} {2}", strSelect, strFrom, strCondicion);
                        break;
                }
                #endregion Generacion de la consulta

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

        public DataTable mtdCargarInfoAreas()
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("SELECT [IdArea],[NombreArea] FROM [Parametrizacion].[Area]");
                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);
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

        public DataTable mtdCargarDDLFactorRiesgo(int intTipo)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                switch (intTipo)
                {
                    case 1://FactorRiesgo
                        break;
                    case 2: //FactorRiesgoLAFT
                        strConsulta = "SELECT IdFactorRiesgoLAFT as IdFactorRiesgo, NombreFactorRiesgoLAFT as NombreFactorRiesgo FROM Parametrizacion.FactorRiesgoLAFT";
                        break;
                }

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

        public DataTable mtdReporteControles(string IdCadenaValor,
            string IdMacroProceso, string IdProceso, string IdClasificacionRiesgo,
            string IdClasificacionGeneralRiesgo,
            string NombreRiesgoInherente, string NombreRiesgoResidual, string IdEmpresa
            )
        {
            #region Variables
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty;
            #endregion Variables

            try
            {
                #region Consultas
                //string strSelect = "SELECT LTRIM(RTRIM(ISNULL(RCtrl.CodigoControl, ''))) AS CodigoControl, LTRIM(RTRIM(ISNULL(RCtrl.NombreControl, ''))) AS NombreControl,LTRIM(RTRIM(ISNULL(RCtrl.ResponsableEjecucion, ''))) as ResponsableEjecucion, ResponsableControl.NombreHijo AS ResponsableCalificacion, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RCtrl.FechaRegistro, 107), ''))) AS FechaRegistroControl, LTRIM(RTRIM(ISNULL(PPer.NombrePeriodicidad, ''))) AS NombrePeriodicidad, LTRIM(RTRIM(ISNULL(PT.NombreTest, ''))) AS NombreTest, LTRIM(RTRIM(ISNULL(PCC.NombreClaseControl, ''))) AS NombreClaseControl, LTRIM(RTRIM(ISNULL(PTC.NombreTipoControl, ''))) AS NombreTipoControl, LTRIM(RTRIM(ISNULL(PRE.NombreResponsableExperiencia, ''))) AS NombreResponsableExperiencia, LTRIM(RTRIM(ISNULL(PD.NombreDocumentacion, ''))) AS NombreDocumentacion, LTRIM(RTRIM(ISNULL(PRes.NombreResponsabilidad, ''))) AS NombreResponsabilidad, LTRIM(RTRIM(ISNULL(PCCtrl.NombreEscala, ''))) AS NombreEscala, LTRIM(RTRIM(ISNULL(PMCtrl.NombreMitiga, ''))) AS NombreMitiga, (CASE WHEN PMCtrl.IdMitiga = 1 THEN '' ELSE LTRIM(RTRIM(ISNULL(PCCtrl.DesviacionImpacto, '')))END) AS DesviacionImpacto, (CASE WHEN PMCtrl.IdMitiga = 2 THEN '' ELSE LTRIM(RTRIM(ISNULL(PCCtrl.DesviacionProbabilidad, ''))) END) AS DesviacionFrecuencia, LTRIM(RTRIM(ISNULL(RCtrl.DescripcionControl, ''))) AS DescripcionControl, LTRIM(RTRIM(ISNULL(RCtrl.ObjetivoControl, ''))) AS ObjetivoControl";
                string strSelect = "SELECT LTRIM(RTRIM(ISNULL(RCtrl.CodigoControl, ''))) AS CodigoControl, LTRIM(RTRIM(ISNULL(RCtrl.NombreControl, ''))) AS NombreControl,LTRIM(RTRIM(ISNULL(RCtrl.ResponsableEjecucion, ''))) as ResponsableEjecucion, ResponsableControl.NombreHijo AS ResponsableCalificacion, LTRIM(RTRIM(ISNULL(CONVERT(varchar, RCtrl.FechaRegistro, 107), ''))) AS FechaRegistroControl, LTRIM(RTRIM(ISNULL(PPer.NombrePeriodicidad, ''))) AS NombrePeriodicidad, LTRIM(RTRIM(ISNULL(PT.NombreTest, ''))) AS NombreTest, LTRIM(RTRIM(ISNULL(PCC.NombreClaseControl, ''))) AS NombreClaseControl, LTRIM(RTRIM(ISNULL(PTC.NombreTipoControl, ''))) AS NombreTipoControl, LTRIM(RTRIM(ISNULL(PRE.NombreResponsableExperiencia, ''))) AS NombreResponsableExperiencia, LTRIM(RTRIM(ISNULL(PD.NombreDocumentacion, ''))) AS NombreDocumentacion, LTRIM(RTRIM(ISNULL(PRes.NombreResponsabilidad, ''))) AS NombreResponsabilidad,RCV.NombreVariable,RCV.NombreCategoria, LTRIM(RTRIM(ISNULL(PCCtrl.NombreEscala, ''))) AS NombreEscala, LTRIM(RTRIM(ISNULL(PMCtrl.NombreMitiga, ''))) AS NombreMitiga, (CASE WHEN PMCtrl.IdMitiga = 1 THEN '' ELSE LTRIM(RTRIM(ISNULL(PCCtrl.DesviacionImpacto, '')))END) AS DesviacionImpacto, (CASE WHEN PMCtrl.IdMitiga = 2 THEN '' ELSE LTRIM(RTRIM(ISNULL(PCCtrl.DesviacionProbabilidad, ''))) END) AS DesviacionFrecuencia, LTRIM(RTRIM(ISNULL(RCtrl.DescripcionControl, ''))) AS DescripcionControl, LTRIM(RTRIM(ISNULL(RCtrl.ObjetivoControl, ''))) AS ObjetivoControl";
                //string strFrom = "FROM Riesgos.Control AS RCtrl LEFT JOIN Parametrizacion.Periodicidad AS PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad LEFT JOIN Parametrizacion.Test AS PT ON PT.IdTest = RCtrl.IdTest LEFT JOIN Parametrizacion.ClaseControl AS PCC ON PCC.IdClaseControl = RCtrl.IdClaseControl LEFT JOIN Parametrizacion.TipoControl AS PTC ON PTC.IdTipoControl = RCtrl.IdTipoControl LEFT JOIN Parametrizacion.ResponsableExperiencia AS PRE ON PRE.IdResponsableExperiencia = RCtrl.IdResponsableExperiencia LEFT JOIN Parametrizacion.Documentacion AS PD ON PD.IdDocumentacion = RCtrl.IdDocumentacion LEFT JOIN Parametrizacion.Responsabilidad AS PRes ON PRes.IdResponsabilidad = RCtrl.IdResponsabilidad LEFT JOIN Parametrizacion.CalificacionControl AS PCCtrl ON PCCtrl.IdCalificacionControl = RCtrl.IdCalificacionControl LEFT JOIN Parametrizacion.MitigaControl AS PMCtrl ON PMCtrl.IdMitiga = RCtrl.IdMitiga LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableControl ON ResponsableControl.idHijo = RCtrl.Responsable"; 
                string strFrom = "FROM Riesgos.Control AS RCtrl LEFT JOIN Parametrizacion.Periodicidad AS PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad LEFT JOIN Parametrizacion.Test AS PT ON PT.IdTest = RCtrl.IdTest LEFT JOIN Parametrizacion.ClaseControl AS PCC ON PCC.IdClaseControl = RCtrl.IdClaseControl LEFT JOIN Parametrizacion.TipoControl AS PTC ON PTC.IdTipoControl = RCtrl.IdTipoControl LEFT JOIN Parametrizacion.ResponsableExperiencia AS PRE ON PRE.IdResponsableExperiencia = RCtrl.IdResponsableExperiencia LEFT JOIN Parametrizacion.Documentacion AS PD ON PD.IdDocumentacion = RCtrl.IdDocumentacion LEFT JOIN Parametrizacion.Responsabilidad AS PRes ON PRes.IdResponsabilidad = RCtrl.IdResponsabilidad left join Riesgos.ControlxVariable as RCV on RCV.IdControl = RCtrl.IdControl LEFT JOIN Parametrizacion.CalificacionControl AS PCCtrl ON PCCtrl.IdCalificacionControl = RCtrl.IdCalificacionControl LEFT JOIN Parametrizacion.MitigaControl AS PMCtrl ON PMCtrl.IdMitiga = RCtrl.IdMitiga LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableControl ON ResponsableControl.idHijo = RCtrl.Responsable";
                //INNER JOIN Riesgos.Riesgo AS RR ON RR.IdRiesgo = RCR.IdRiesgo LEFT JOIN Procesos.Proceso AS PP ON RR.IdProceso = PP.IdProceso LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo AS PCGR ON RR.IdClasificacionGeneralRiesgo = PCGR.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo LEFT JOIN Procesos.Macroproceso AS PM ON RR.IdMacroproceso = PM.IdMacroProceso LEFT JOIN Procesos.CadenaValor AS PCV ON PCV.IdCadenaValor = RR.IdCadenaValor";
                string strWhere = string.Empty;
                string strOrder = "ORDER BY RCtrl.IdControl";
                #endregion

                #region Filtros (Comentado)
                //strWhere = "WHERE (RR.Anulado = 0)";

                //if (IdCadenaValor != "---")
                //    strWhere += " AND (PCV.IdCadenaValor = " + IdCadenaValor + ")";

                //if (IdMacroProceso != "---")
                //    strWhere += " AND (PM.IdMacroProceso = " + IdMacroProceso + ")";

                //if (IdProceso != "---")
                //    strWhere += " AND (PP.IdProceso = " + IdProceso + ")";

                //if (IdClasificacionRiesgo != "---")
                //    strWhere += " AND (PCR.IdClasificacionRiesgo = " + IdClasificacionRiesgo + ")";

                //if (IdClasificacionGeneralRiesgo != "---")
                //    strWhere += " AND (PCGR.IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ")";

                //if (NombreRiesgoInherente != "---")
                //    strWhere += " AND (PRI.NombreRiesgoInherente = N'" + NombreRiesgoInherente + "')";

                //if (NombreRiesgoResidual != "---")
                //    strWhere += " AND (RiesgoResidual.NombreRiesgoInherente = N'" + NombreRiesgoResidual + "')";

                //if (IdEmpresa != "---")
                //    strWhere += " AND (PP.IdEmpresa IN (" + IdEmpresa + ", 3))";
                #endregion Filtros


                strConsulta = string.Format("{0} {1} {2} {3}", strSelect, strFrom, strWhere, strOrder);

                cDataBase.conectar();
                //dtInfo = cDataBase.ejecutarConsulta(strConsulta);
                dtInfo = cControl.ReporteControles();
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
        #region Reportes Causas sin Control
        public DataTable ReporteRiesgosCausasSinControl(String IdCadenaValor, String IdMacroProceso,
            String IdProceso, String IdClasificacionRiesgo, String IdClasificacionGeneralRiesgo,
            String NombreRiesgoInherente, String NombreRiesgoResidual, String IdEmpresa,
            String IdRiesgo, String IdArea)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty, strConsulta = string.Empty, strFrom = string.Empty, strSelect = string.Empty;
            #endregion Variables

            try
            {
                #region
                if (IdCadenaValor != "---")
                    condicion = "WHERE (IdCadenaValor = " + IdCadenaValor + ") ";
                #endregion

                #region
                if (IdMacroProceso != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (IdMacroProceso = " + IdMacroProceso + ") ";
                    else
                        condicion += "AND (IdMacroProceso = " + IdMacroProceso + ") ";
                }
                #endregion

                #region
                if (IdProceso != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (IdProceso = " + IdProceso + ") ";
                    else
                        condicion += "AND (IdProceso = " + IdProceso + ") ";
                }
                #endregion

                #region
                if (IdClasificacionRiesgo != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (IdClasificacionRiesgo = " + IdClasificacionRiesgo + ") ";
                    else
                        condicion += "AND (IdClasificacionRiesgo = " + IdClasificacionRiesgo + ") ";
                }
                #endregion

                #region
                if (IdClasificacionGeneralRiesgo != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ") ";
                    else
                        condicion += "AND (IdClasificacionGeneralRiesgo = " + IdClasificacionGeneralRiesgo + ") ";
                }
                #endregion

                #region
                if (NombreRiesgoInherente != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (NombreRiesgoInherente = N'" + NombreRiesgoInherente + "') ";
                    else
                        condicion += "AND (NombreRiesgoInherente = N'" + NombreRiesgoInherente + "') ";
                }
                #endregion

                #region
                if (NombreRiesgoResidual != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (NombreRiesgoInherente = N'" + NombreRiesgoResidual + "') ";
                    else
                        condicion += "AND (NombreRiesgoInherente = N'" + NombreRiesgoResidual + "') ";
                }
                #endregion
                #region Area
                if (IdArea != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (IdArea = " + IdArea + ") ";
                    else
                        condicion += "AND (IdArea = " + IdArea + ") ";
                }
                #endregion Area
                #region
                if (IdEmpresa != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (IdEmpresa IN (" + IdEmpresa + ", 3)) ";
                    else
                        condicion += "AND (IdEmpresa IN (" + IdEmpresa + ", 3)) ";
                }
                #endregion

                #region
                if (IdRiesgo != "---")
                {
                    if (string.IsNullOrEmpty(condicion.Trim()))
                        condicion = "WHERE (IdRiesgo = " + IdRiesgo + ") ";
                    else
                        condicion += "AND (IdRiesgo = " + IdRiesgo + ") ";
                }
                #endregion

                #region
                if (string.IsNullOrEmpty(condicion.Trim()))
                    condicion = "WHERE (Anulado = 0) ";
                else
                    condicion += "AND (Anulado = 0) ";
                #endregion
                cDataBase.conectar();
                #region Riesgos
                strSelect = "SELECT [Codigo] as CodigoRiesgo,[Nombre] as NombreRiesgo,[Descripcion],[ListaCausas],[IdControl],[FrecuenciaInherente],[CodigoFrecuenciaInherente],[ImpactoInherente],[CodigoImpactoInherente]"
                + ",[RiesgoInherente],[CodigoRiesgoInherente],[FrecuenciaResidual],[CodigoFrecuenciaResidual],[ImpactoResidual],[CodigoImpactoResidual]"
                + ",[RiesgoResidual],[CodigoRiesgoResidual],[CodigoEvento],[DescripcionEvento]"
                + ",[NombreArea],[IdArea],[IdRiesgo]";
                strFrom = "FROM [Riesgos].[vwRiesgoReporteCausasSinControl] ";
                strConsulta = string.Format("{0} {1} {2}  order by [IdRiesgo]", strSelect, strFrom, condicion);
                //AND ISNULL(IdCausasvsControles,0) = 0
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                //dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Riesgos.Riesgo.Codigo)) AS CodigoRiesgo, LTRIM(RTRIM(Riesgos.Riesgo.Nombre)) AS NombreRiesgo, Parametrizacion.JerarquiaOrganizacional.NombreHijo AS ResponsableRiesgo,LTRIM(RTRIM(ISNULL(CONVERT(varchar, Riesgos.Riesgo.FechaRegistro, 107), ''))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionRiesgo.NombreClasificacionRiesgo, ''))) AS ClasificacionRiesgo,LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionGeneralRiesgo.NombreClasificacionGeneralRiesgo, ''))) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.ClasificacionParticularRiesgo.NombreClasificacionParticularRiesgo, ''))) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(ISNULL(Parametrizacion.TipoRiesgoOperativo.NombreTipoRiesgoOperativo, ''))) AS TipoRiesgoOperativo, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaCausas, ''))), '|', ',') AS Causas, REPLACE(LTRIM(RTRIM(ISNULL(Riesgos.Riesgo.ListaConsecuencias, ''))), '|', ',') AS Consecuencias, LTRIM(RTRIM(ISNULL(Procesos.CadenaValor.NombreCadenaValor, ''))) AS CadenaValor, LTRIM(RTRIM(ISNULL(Procesos.Macroproceso.Nombre, ''))) AS Macroproceso, LTRIM(RTRIM(ISNULL(Procesos.Proceso.Nombre, ''))) AS Proceso, LTRIM(RTRIM(ISNULL(Procesos.Subproceso.Nombre, ''))) AS Subproceso, LTRIM(RTRIM(ISNULL(Procesos.Actividad.Nombre, ''))) AS Actividad, LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.NombreProbabilidad, ''))) AS FrecuenciaInherente,LTRIM(RTRIM(ISNULL(Parametrizacion.Probabilidad.ValorProbabilidad, ''))) AS FrecuenciaInherenteCualitativa, LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.NombreImpacto, ''))) AS ImpactoInherente,  LTRIM(RTRIM(ISNULL(Parametrizacion.Impacto.ValorImpacto, ''))) AS ImpactoInherenteCualitativo,LTRIM(RTRIM(ISNULL(Parametrizacion.RiesgoInherente.NombreRiesgoInherente, ''))) AS RiesgoInherente, LTRIM(RTRIM(ISNULL(pr.NombreProbabilidad, ''))) AS FrecuenciaResidual,LTRIM(RTRIM(ISNULL(pr.ValorProbabilidad, ''))) AS FrecuenciaResidualCualitativa,LTRIM(RTRIM(ISNULL(im.NombreImpacto, ''))) AS ImpactoResidual,LTRIM(RTRIM(ISNULL(im.ValorImpacto, ''))) AS ImpactoResidualCualitativo,LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) AS RiesgoResidual FROM Riesgos.Riesgo LEFT JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo ON Riesgos.Riesgo.IdClasificacionGeneralRiesgo = Parametrizacion.ClasificacionGeneralRiesgo.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo ON Riesgos.Riesgo.IdClasificacionParticularRiesgo = Parametrizacion.ClasificacionParticularRiesgo.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo ON Riesgos.Riesgo.IdTipoRiesgoOperativo = Parametrizacion.TipoRiesgoOperativo.IdTipoRiesgoOperativo LEFT JOIN Procesos.CadenaValor ON Procesos.CadenaValor.IdCadenaValor = Riesgos.Riesgo.IdCadenaValor LEFT JOIN Procesos.Macroproceso ON Riesgos.Riesgo.IdMacroproceso = Procesos.Macroproceso.IdMacroProceso LEFT JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso LEFT JOIN Procesos.Subproceso ON Procesos.Subproceso.IdProceso = Riesgos.Riesgo.IdSubProceso LEFT JOIN Procesos.Actividad ON Riesgos.Riesgo.IdActividad = Procesos.Actividad.IdActividad LEFT JOIN Parametrizacion.Probabilidad ON Parametrizacion.Probabilidad.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad pr ON pr.IdProbabilidad = Riesgos.Riesgo.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto ON Parametrizacion.Impacto.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.Impacto im ON im.IdImpacto = Riesgos.Riesgo.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente ON Parametrizacion.RiesgoInherente.IdProbabilidad = Riesgos.Riesgo.IdProbabilidad AND Parametrizacion.RiesgoInherente.IdImpacto = Riesgos.Riesgo.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON Riesgos.Riesgo.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad 	AND Riesgos.Riesgo.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Riesgo.IdResponsableRiesgo " + condicion + "ORDER BY Riesgos.Riesgo.IdRiesgo");
                #endregion
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
        #endregion Reportes Causas sin Control
        #endregion Metodos Publicos

        #region Metodos privados

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
        #endregion

        #region Filtro Empresa Mapa Riesgos
        #endregion Filtro Empresa Mapa Riesgos

        #region PDFs

        #region [Viejo] Agregar Archivo Riesgo
        public void agregarArchivoRiesgo(String IdRegistro, String UrlArchivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Archivos (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, UrlArchivo) VALUES (3, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + UrlArchivo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region [Viejo] Agregar Archivo Legislacion
        public void agregarArchivoLegislacion(String IdRegistro, String UrlArchivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Archivos (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, UrlArchivo) VALUES (2, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + UrlArchivo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region [Viejo] Agregar Plan Accion
        public void agregarArchivoPlanAccion(String IdRegistro, String UrlArchivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Archivos (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, UrlArchivo) VALUES (4, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + UrlArchivo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public void mtdAgregarArchivoPdf(string strControlUsuario, string strIdRegistro, string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO Riesgos.Archivos ([IdControlUsuario], [IdRegistro], [NombreUsuario], [FechaRegistro], [UrlArchivo], [ArchivoPDF]) VALUES ({3}, {0}, '{1}', GETDATE(),N'{2}', @PdfData)",
                    strIdRegistro, NombreUsuario, strUrlArchivo, strControlUsuario);

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

        public byte[] mtdDescargarArchivoPdf(string strNombreArchivo)
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [Riesgos].[Archivos] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);

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

        #endregion PDFs
        #region Notificacion Anulacion Riesgo
        public string mtdResponsableRiesgo(int IdRiesgo)
        {
            string Responsable = string.Empty;
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("SELECT PJO.NombreHijo FROM[Riesgos].[Riesgo] as RR  "+
                    "inner join Parametrizacion.JerarquiaOrganizacional as PJO on PJO.idHijo = RR.IdResponsableRiesgo "+
                    "where RR.IdRiesgo ={0}",IdRiesgo);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();

                Responsable = dtInformacion.Rows[0]["NombreHijo"].ToString().Trim();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return Responsable;
        }
        public int mtdIdResponsableRiesgo(int IdRiesgo)
        {
            int IdResponsable = 0;
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("SELECT RR.IdResponsableRiesgo FROM[Riesgos].[Riesgo] as RR  " +
                    "where RR.IdRiesgo ={0}", IdRiesgo);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();

                IdResponsable = Convert.ToInt32(dtInformacion.Rows[0]["IdResponsableRiesgo"].ToString().Trim());
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return IdResponsable;
        }
        #endregion Notificacion Anulacion Riesgo

        public DataTable loadInfoInterfaceFCC(string fechaInicial, string fechaFinal)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                if(fechaInicial != string.Empty && fechaFinal != string.Empty)
                {
                    strConsulta = string.Format("SELECT  [numeroProveedor],[cod],[titre],[RazonSocial],[PrimerApellido],[SegundoApellido],[NombrePerNatural]"
                + ",[critere],[DireccionOfcPrincipal],[coddepart],[Pays],[Langue],[NumCelular],[CorreroContacto],[mode],[siret],[Personnephys]"
                + ",[txe],[activite],[fechaNacimiento],[LugarNacimiento],[sexe],[PROFESSION],[Expr1],[IdBanco],[NumeroCuenta],[ContantoPago]"
                + ",[TipoCuenta],[collectif],[IdCodCIIUTrabajo],[paiemt],[paiement], FechaDiligenciamiento FROM[dbo].[vw_InterfaceExcel] " + "\n"
                + " where IdEstado = 3 and FechaDiligenciamiento between CONVERT(datetime, '" + fechaInicial + "', 103)  and CONVERT(datetime, '" + fechaFinal + "', 103)");
                }
                else
                {
                    strConsulta = string.Format("SELECT  [numeroProveedor],[cod],[titre],[RazonSocial],[PrimerApellido],[SegundoApellido],[NombrePerNatural]"
                + ",[critere],[DireccionOfcPrincipal],[coddepart],[Pays],[Langue],[NumCelular],[CorreroContacto],[mode],[siret],[Personnephys]"
                + ",[txe],[activite],[fechaNacimiento],[LugarNacimiento],[sexe],[PROFESSION],[Expr1],[IdBanco],[NumeroCuenta],[ContantoPago]"
                + ",[TipoCuenta],[collectif],[IdCodCIIUTrabajo],[paiemt],[paiement] FROM[dbo].[vw_InterfaceExcel] where IdEstado = 3");
                }
                
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
        public DataTable loadInfoReporte()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;

            try
            {
                strConsulta = "SELECT IdEstado,Id,RazonSocial,Documento,FechaDiligenciamiento, FechaVerificacion, dc.Telefono as dcTel, dc.CorreoElectronico as dcMail, " + "\n"+
                              "de.Telefono as deTel,de.CorreoElectronico as deMail " + "\n"+
                               "FROM [dbo].[FormularioDatosBasicos] as fdb " + "\n"+
                               "left outer join DomicilioExterior.DomicilioCol as dc on dc.idFormulario = fdb.Id " + "\n" +
                               "left outer join DomicilioExterior.DomicilioExt as de on de.idFormulario = fdb.id " + "\n" +
                               "where FechaDiligenciamiento < DATEADD(YYYY, -1, GETDATE()) and IdEstado = 3";

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
    }
}