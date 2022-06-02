using System;
using System.Collections.Generic;
using System.Web;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using DataSets = System.Data;
using clsDatos;


namespace ListasSarlaft.Classes
{
    public class cAuditoria : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();

        public void auditoriaListas(Int32 tipoLista, String nombreLista, String documento, String nombre, String alias, Int32 rows, String NumeroFecha)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT Listas.Auditoria (IdUsuario, FechaConsulta, NumeroFecha, CodigoClaseLisa, NombreClaseLista, Documento, Nombre, Alias, NumeroRegistros) VALUES (" + IdUsuario.ToString() + ", GETDATE(), " + NumeroFecha.Trim().ToString() + ", " + tipoLista + ", '" + nombreLista + "', N'" + documento + "', N'" + nombre + "', N'" + alias + "', " + rows + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable consultarAuditoria(String Nombres, String Apellidos, String Usuario, String FechaDesde, String FechaHasta)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty;
            try
            {
                if (Nombres != string.Empty)
                {
                    condicion = "WHERE (Listas.Usuarios.Nombres LIKE N'%" + Nombres.Trim().ToString() + "%') ";
                }
                if (Apellidos != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.Usuarios.Apellidos LIKE N'%" + Apellidos.Trim().ToString() + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.Usuarios.Apellidos LIKE N'%" + Apellidos.Trim().ToString() + "%') ";
                    }
                }
                if (Usuario != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.Usuarios.Usuario LIKE N'%" + Usuario.Trim().ToString() + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.Usuarios.Usuario LIKE N'%" + Usuario.Trim().ToString() + "%') ";
                    }
                }
                if (FechaDesde != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.Auditoria.NumeroFecha >= " + FechaDesde.Trim().ToString() + ") ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.Auditoria.NumeroFecha >= " + FechaDesde.Trim().ToString() + ") ";
                    }
                }
                if (FechaHasta != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.Auditoria.NumeroFecha <= " + FechaHasta.Trim().ToString() + ") ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.Auditoria.NumeroFecha <= " + FechaHasta.Trim().ToString() + ") ";
                    }
                }
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Listas.Usuarios.Nombres, Listas.Usuarios.Apellidos, Listas.Usuarios.Usuario, CONVERT(varchar, Listas.Auditoria.FechaConsulta, 109) AS FechaConsulta, Listas.Auditoria.NombreClaseLista, Listas.Auditoria.Documento, Listas.Auditoria.Nombre, Listas.Auditoria.Alias, Listas.Auditoria.NumeroRegistros FROM Listas.Auditoria INNER JOIN Listas.Usuarios ON Listas.Auditoria.IdUsuario = Listas.Usuarios.IdUsuario " + condicion);
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

        public void AnularAuditoria(String IdAuditoria, String MotivoAnulacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("insert into Auditoria.AuditoriaAnulada (IdAuditoria,MotivoAnulacion,FechaAnulacion,IdUsuario) values ('" + IdAuditoria + "','" + MotivoAnulacion + "',GETDATE()," + IdUsuario.ToString() + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        //26-02-2014 Ajustes Camilo Aponte
        public DataTable LoadListPlaneacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdPlaneacion,Nombre from Auditoria.Planeacion order by IdPlaneacion");
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

        public DataTable LoadListAuditoria(string condicion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdAuditoria,Tema from Auditoria.Auditoria where IdPlaneacion in (" + condicion + ") order by IdPlaneacion,IdAuditoria");
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

        public DataTable LoadListMacroProcesos()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select b.IdMacroProceso,b.Nombre from Procesos.CadenaValor a, Procesos.Macroproceso b where a.IdCadenaValor = b.IdCadenaValor order by b.IdMacroProceso");
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

        public DataTable LoadListProcesos(string condicion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdProceso,Nombre from Procesos.Proceso where IdMacroProceso in (" + condicion + ") order by IdMacroProceso,IdProceso");
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

        public DataTable LoadListDependencias()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select idHijo,NombreHijo from Parametrizacion.JerarquiaOrganizacional order by idHijo");
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

        public DataTable LoadListUsuarios()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select distinct d.IdUsuario, c.NombreResponsable as Nombre from Auditoria.GrupoAuditoria a inner join Auditoria.JerarquiaGrupoAuditoria b on a.IdGrupoAuditoria = b.idGrupoAuditoria inner join Parametrizacion.DetalleJerarquiaOrg c on b.idHijo = c.idHijo inner join listas.usuarios d on c.idHijo = d.idjerarquia order by d.IdUsuario");
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

        //11-03-2014
        public DataTable LoadListUsuariosGesAud()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select distinct a.idHijo,b.NombreResponsable from Auditoria.JerarquiaGrupoAuditoria a, Parametrizacion.DetalleJerarquiaOrg b where a.idHijo = b.idHijo");
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

        public DataTable VerFechaEtapas(String Etapa, String IdAuditoria)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select " + Etapa + " as fecha from Auditoria.HistoricoAuditoria where IdAuditoria = '" + IdAuditoria + "'");
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

        public DataTable LoadReporteHallazgos(String IdPlaneaciones, String IdAuditorias, String IdProcesos, String IdHijos, String IdUsuarios)
        {
            DataTable DtInfo = new DataTable();
            string condicion = "";
            try
            {
                if (IdPlaneaciones != "0")
                {
                    condicion = " pl.IdPlaneacion in (" + IdPlaneaciones + ")";
                }
                if (condicion != "")
                {
                    if (IdAuditorias != "0")
                    {
                        condicion += " and au.IdAuditoria in (" + IdAuditorias + ")";
                    }
                }
                if (condicion != "")
                {
                    if (IdUsuarios != "0")
                    {
                        condicion += "  and ha.IdUsuario in (" + IdUsuarios + ")";
                    }
                }
                if (condicion != "")
                {
                    if (IdProcesos != "0")
                    {
                        condicion += " and pro.IdProceso in (" + IdProcesos + ")";
                    }
                }
                if (condicion != "")
                {
                    if (IdHijos != "0")
                    {
                        condicion += " and je.idHijo in (" + IdHijos + ")";
                    }
                }
                cDataBase.conectar();
                DtInfo = cDataBase.ejecutarConsulta("select pl.Nombre as 'Planeación',au.IdAuditoria as 'Id Auditoría',au.Tema as 'Nombre Auditoría',es.Nombre as 'Programa / Estandar',auob.Nombre as Objetivo,IdHallazgo as 'Id Hallazgo',de.NombreDetalle as 'Tipo de Hallazgo',de1.NombreDetalle as 'Estado del Hallazgo',ha.Hallazgo as 'Descripción del Hallazgo',case au.IdDependencia when ISNULL(au.IdDependencia,'') then je.NombreHijo else pro.Nombre end as 'Responsable / Gerencia',au.Estado as 'Estado de la Auditoría',rtrim(usu.Nombres) + ' ' + RTRIM(usu.Apellidos) as 'Nombre de Usuario' from Auditoria.Planeacion pl inner join Auditoria.Auditoria au on pl.IdPlaneacion = au.IdPlaneacion inner join Auditoria.Estandar es on au.IdEstandar = es.IdEstandar inner join Auditoria.Hallazgo ha on au.IdAuditoria = ha.IdAuditoria inner join Parametrizacion.DetalleTipos de on ha.IdDetalleTipoHallazgo = de.IdDetalleTipo inner join Parametrizacion.DetalleTipos de1 on ha.IdEstado = de1.IdDetalleTipo inner join Auditoria.DetalleEnfoque aude on ha.IdDetalleEnfoque = aude.IdDetalleEnfoque inner join Auditoria.Enfoque auen on aude.IdEnfoque = auen.IdEnfoque inner join Auditoria.Objetivo auob on auen.IdObjetivo = auob.IdObjetivo left join Parametrizacion.JerarquiaOrganizacional je on au.IdDependencia = je.idHijo left join Procesos.Proceso pro on au.IdProceso = pro.IdProceso left join Listas.Usuarios usu on ha.IdUsuario = usu.IdUsuario where " + condicion);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return DtInfo;
        }

        public DataTable LoadReporteGestionAuditoriaHistorico(String IdPlaneaciones, String IdAuditorias, String IdProcesos, String IdHijos, String IdUsuarios)
        {
            DataTable DtInfo = new DataTable();
            string condicion = "";

            try
            {
                if (IdPlaneaciones != "0")
                    condicion = " pl.IdPlaneacion in (" + IdPlaneaciones + ")";

                if (condicion != "")
                {
                    if (IdAuditorias != "0")
                        condicion += " and au.IdAuditoria in (" + IdAuditorias + ")";
                }

                if (condicion != "")
                {
                    if (IdUsuarios != "0")
                        condicion += "  and dj.idHijo in (" + IdUsuarios + ")";
                }

                if (condicion != "")
                {
                    if (IdProcesos != "0")
                        condicion += " and pro.IdProceso in (" + IdProcesos + ")";
                }

                if (condicion != "")
                {
                    if (IdHijos != "0")
                        condicion += " and je.idHijo in (" + IdHijos + ")";
                }
                cDataBase.conectar();
                DtInfo = cDataBase.ejecutarConsulta("SELECT pl.Nombre AS 'Planeación', au.IdAuditoria, au.Tema AS 'Nombre Auditoría', CASE au.IdDependencia WHEN ISNULL(au.IdDependencia,'') THEN je.NombreHijo ELSE pro.Nombre END AS 'Dependencia / Proceso', REPLACE(CONVERT(VARCHAR, au.FechaInicio, 111), '/', '-') AS 'Fecha de Inicio', REPLACE(CONVERT(VARCHAR, au.FechaCierre, 111), '/', '-') AS 'Fecha Fin', DATEDIFF(day,au.FechaInicio,au.FechaCierre) AS 'Tiempo Estimado', dj.NombreResponsable AS Recurso, ar.Etapa, au.Estado 'Estado Actual', REPLACE(CONVERT(VARCHAR, ar.FechaInicial, 111), '/', '-') AS 'Fecha inicio Etapa', REPLACE(CONVERT(VARCHAR, ar.FechaFinal, 111), '/', '-') AS 'Fecha Fin Etapa', ar.HorasPlaneadas AS 'Horas asignadas por Etapa', au.Estado AS 'Estado actual Auditoría', REPLACE(CONVERT(VARCHAR, ar.FechaInicial, 111), '/', '-') AS 'Fecha inicio Etapa Real', '' AS 'Fecha Fin Etapa Real', '' AS 'Diferencia en Días', hisa.DEVUELTA AS Devuelta FROM Auditoria.Planeacion pl INNER JOIN Auditoria.Auditoria au ON pl.IdPlaneacion = au.IdPlaneacion INNER JOIN Auditoria.AudObjRecurso ar ON au.IdAuditoria = ar.IdAuditoria INNER JOIN Parametrizacion.DetalleJerarquiaOrg dj ON ar.IdHijo = dj.idHijo LEFT JOIN Parametrizacion.JerarquiaOrganizacional je ON au.IdDependencia = je.idHijo LEFT JOIN Procesos.Proceso pro ON au.IdProceso = pro.IdProceso LEFT JOIN Auditoria.HistoricoAuditoria hisa ON hisa.IdAuditoria = au.IdAuditoria WHERE " + condicion + " ORDER BY au.IdAuditoria, ar.FechaInicial");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return DtInfo;
        }

        public DataTable LoadReporteGestionAuditoriaActual(String IdPlaneaciones, String IdAuditorias, String IdProcesos, String IdHijos, String IdUsuarios)
        {
            DataTable DtInfo = new DataTable();
            string condicion = "";
            try
            {
                if (IdPlaneaciones != "0")
                    condicion = " pl.IdPlaneacion in (" + IdPlaneaciones + ")";

                if (condicion != "")
                {
                    if (IdAuditorias != "0")
                        condicion += " and au.IdAuditoria in (" + IdAuditorias + ")";
                }

                if (condicion != "")
                {
                    if (IdUsuarios != "0")
                        condicion += "  and dj.idHijo in (" + IdUsuarios + ")";
                }

                if (condicion != "")
                {
                    if (IdProcesos != "0")
                        condicion += " and pro.IdProceso in (" + IdProcesos + ")";
                }

                if (condicion != "")
                {
                    if (IdHijos != "0")
                        condicion += " and je.idHijo in (" + IdHijos + ")";
                }

                cDataBase.conectar();
                //07-07-2014
                //DtInfo = cDataBase.ejecutarConsulta("select pl.Nombre as 'Planeación',au.IdAuditoria,au.Tema as 'Nombre Auditoría',case au.IdDependencia when ISNULL(au.IdDependencia,'') then je.NombreHijo else pro.Nombre end as 'Dependencia / Proceso', REPLACE(CONVERT(varchar, au.FechaInicio, 111), '/', '-') AS 'Fecha de Inicio',REPLACE(CONVERT(varchar, au.FechaCierre, 111), '/', '-') AS 'Fecha Fin',DATEDIFF(day,au.FechaInicio,au.FechaCierre) as 'Tiempo Estimado', dj.NombreResponsable as Recurso,au.Estado as Etapa,REPLACE(CONVERT(varchar, ar.FechaInicial, 111), '/', '-') AS 'Fecha inicio Etapa',REPLACE(CONVERT(varchar, ar.FechaFinal, 111), '/', '-') AS 'Fecha Fin Etapa', ar.HorasPlaneadas as 'Horas asignadas por Etapa',au.Estado as 'Estado actual Auditoría', REPLACE(CONVERT(varchar, ar.FechaInicial, 111), '/', '-') AS 'Fecha inicio Etapa Real', '' as 'Fecha Fin Etapa Real','' as 'Diferencia en Días',hisa.DEVUELTA as Devuelta from Auditoria.Planeacion pl inner join Auditoria.Auditoria au on pl.IdPlaneacion = au.IdPlaneacion inner join Auditoria.AudObjRecurso ar on au.IdAuditoria = ar.IdAuditoria and ar.Etapa = au.Estado inner join Parametrizacion.DetalleJerarquiaOrg dj on ar.IdHijo = dj.idHijo left join Parametrizacion.JerarquiaOrganizacional je on au.IdDependencia = je.idHijo left join Procesos.Proceso pro on au.IdProceso = pro.IdProceso left join Auditoria.HistoricoAuditoria hisa on hisa.IdAuditoria = au.IdAuditoria where " + condicion + " order by au.FechaInicio");
                DtInfo = cDataBase.ejecutarConsulta("select '' 'Estado Actual', pl.Nombre as 'Planeación',au.IdAuditoria,au.Tema as 'Nombre Auditoría',case au.IdDependencia when ISNULL(au.IdDependencia,'') then je.NombreHijo else pro.Nombre end as 'Dependencia / Proceso', REPLACE(CONVERT(varchar, au.FechaInicio, 111), '/', '-') AS 'Fecha de Inicio',REPLACE(CONVERT(varchar, au.FechaCierre, 111), '/', '-') AS 'Fecha Fin',DATEDIFF(day,au.FechaInicio,au.FechaCierre) as 'Tiempo Estimado', dj.NombreResponsable as Recurso,au.Estado as Etapa,REPLACE(CONVERT(varchar, ar.FechaInicial, 111), '/', '-') AS 'Fecha inicio Etapa',REPLACE(CONVERT(varchar, ar.FechaFinal, 111), '/', '-') AS 'Fecha Fin Etapa', ar.HorasPlaneadas as 'Horas asignadas por Etapa',au.Estado as 'Estado actual Auditoría', REPLACE(CONVERT(varchar, ar.FechaInicial, 111), '/', '-') AS 'Fecha inicio Etapa Real', '' as 'Fecha Fin Etapa Real','' as 'Diferencia en Días',hisa.DEVUELTA as Devuelta from Auditoria.Planeacion pl inner join Auditoria.Auditoria au on pl.IdPlaneacion = au.IdPlaneacion left join Auditoria.AudObjRecurso ar on au.IdAuditoria = ar.IdAuditoria and ar.Etapa = au.Estado left join Parametrizacion.DetalleJerarquiaOrg dj on ar.IdHijo = dj.idHijo left join Parametrizacion.JerarquiaOrganizacional je on au.IdDependencia = je.idHijo left join Procesos.Proceso pro on au.IdProceso = pro.IdProceso left join Auditoria.HistoricoAuditoria hisa on hisa.IdAuditoria = au.IdAuditoria where " + condicion + " order by au.FechaInicio");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return DtInfo;
        }

        public DataTable LoadReporteRecomendaciones(String IdPlaneaciones, String IdAuditorias, String IdProcesos, String IdHijos, String IdUsuarios)
        {
            DataTable DtInfo = new DataTable();
            string condicion = "";
            try
            {
                if (IdPlaneaciones != "0")
                {
                    condicion = " pl.IdPlaneacion in (" + IdPlaneaciones + ")";
                }
                if (condicion != "")
                {
                    if (IdAuditorias != "0")
                    {
                        condicion += " and au.IdAuditoria in (" + IdAuditorias + ")";
                    }
                }
                if (condicion != "")
                {
                    if (IdUsuarios != "0")
                    {
                        condicion += "  and ha.IdUsuario in (" + IdUsuarios + ")";
                    }
                }
                if (condicion != "")
                {
                    if (IdProcesos != "0")
                    {
                        condicion += " and pro.IdProceso in (" + IdProcesos + ")";
                    }
                }
                if (condicion != "")
                {
                    if (IdHijos != "0")
                    {
                        condicion += " and je.idHijo in (" + IdHijos + ")";
                    }
                }
                cDataBase.conectar();
                DtInfo = cDataBase.ejecutarConsulta("select pl.Nombre as 'Planeación',au.IdAuditoria as 'Id Auditoría',au.Tema as 'Nombre Auditoría',es.Nombre as 'Programa / Estandar',auob.Nombre as Objetivo,ha.IdHallazgo as 'ID Hallazgo',aur.Estado as 'Estado de la Recomendación',aur.Observacion as 'Recomendación',case au.IdDependencia when ISNULL(au.IdDependencia,'') then je.NombreHijo else pro.Nombre end as 'Responsable / Gerencia',au.Estado as 'Estado de la Auditoría',rtrim(usu.Nombres) + ' ' + RTRIM(usu.Apellidos) as 'Nombre de Usuario' from Auditoria.Planeacion pl inner join Auditoria.Auditoria au on pl.IdPlaneacion = au.IdPlaneacion inner join Auditoria.Estandar es on au.IdEstandar = es.IdEstandar inner join Auditoria.Hallazgo ha on au.IdAuditoria = ha.IdAuditoria inner join Auditoria.DetalleEnfoque aude on ha.IdDetalleEnfoque = aude.IdDetalleEnfoque inner join Auditoria.Enfoque auen on aude.IdEnfoque = auen.IdEnfoque inner join Auditoria.Objetivo auob on auen.IdObjetivo = auob.IdObjetivo left join Parametrizacion.JerarquiaOrganizacional je on au.IdDependencia = je.idHijo left join Procesos.Proceso pro on au.IdProceso = pro.IdProceso left join Auditoria.Recomendacion aur on ha.IdHallazgo = aur.IdHallazgo left join Listas.Usuarios usu on aur.IdUsuario = usu.IdUsuario where " + condicion);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return DtInfo;
        }

        public DataTable VerObjetivoEstandar(String IdEstandar)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select min(IdObjetivo) as IdObjetivo  from Auditoria.Objetivo where IdEstandar = " + IdEstandar);
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

        public DataTable mtdVerObjetivoEstandar(string strIdEstandar, string strIdAuditoria)
        {
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                strConsulta = string.Format("SELECT MIN(AO.IdObjetivo) IdObjetivo FROM Auditoria.Objetivo AO " +
                    "INNER JOIN Auditoria.AuditoriaObjetivo AAO ON AO.IdObjetivo = AAO.IdObjetivo " +
                    "WHERE AO.IdEstandar = {0} AND AAO.IdAuditoria = {1}", strIdEstandar, strIdAuditoria);

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

        public DataTable mtdConsultarObjetivo(string strIdObjetivo, string strIdAuditoria)
        {
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                strConsulta = string.Format("SELECT AAO.[IdObjetivo], AO.[Nombre], AAO.[Alcance], CONVERT(VARCHAR(10),AAO.[FechaInicial],120) AS FechaInicial, " +
                        "CONVERT(VARCHAR(10),AAO.[FechaFinal],120) FechaFinal, CONVERT(VARCHAR(10),AAO.[FechaRegistro],120) FechaRegistro, " +
                        "AAO.[IdUsuario], LU.[Usuario], AAO.[IdGrupoAuditoria] " +
                    "FROM [Auditoria].[AuditoriaObjetivo] AAO " +
                    "INNER JOIN [Listas].[Usuarios] LU ON AAO.IdUsuario = LU.IdUsuario " +
                    "INNER JOIN [Auditoria].[Objetivo] AO ON AO.IdObjetivo = AAO.IdObjetivo " +
                    "WHERE AAO.[IdObjetivo] = {0} AND AAO.IdAuditoria = {1}", strIdObjetivo, strIdAuditoria);

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

        public void InseratObjetivoGrupoAuditoria(String IdAuditoria, String IdObjetivo, String IdGrupoAuditoria, String FechaRegistro, String IdUsuario)
        {
            try
            {
                cDataBase.conectar();
                //24-04-2014
                cDataBase.ejecutarConsulta("insert into Auditoria.AuditoriaObjetivo (IdAuditoria,IdObjetivo,IdGrupoAuditoria,FechaRegistro,IdUsuario) values (" + IdAuditoria + "," + IdObjetivo + "," + IdGrupoAuditoria + ",CONVERT(datetime, '" + FechaRegistro + "', 120)," + IdUsuario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void LogHistoricoAudutoria(String IdAuditoria, String EJECUCION, String PREINFORME, String INFORME, String DEVUELTA, String IdUsuario)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("insert into Auditoria.HistoricoAuditoria (IdAuditoria,EJECUCION,PREINFORME,INFORME,DEVUELTA,IdUsuario,FechaRegistro) values ('" + IdAuditoria + "'," + EJECUCION + "," + PREINFORME + "," + INFORME + ",'" + DEVUELTA + "','" + IdUsuario + "',GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarLogHistoricoAudutoria(String IdAuditoria, String EJECUCION, String PREINFORME, String INFORME, String DEVUELTA)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("UPDATE Auditoria.HistoricoAuditoria set EJECUCION = " + EJECUCION + ", PREINFORME = " + PREINFORME + ",INFORME=" + INFORME + ",DEVUELTA='" + DEVUELTA + "' WHERE IdAuditoria = '" + IdAuditoria + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void CerrarLogHistoricoAudutoria(String IdAuditoria, String EJECUCION, String PREINFORME, String INFORME, String DEVUELTA, String FechaRegistro)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("UPDATE Auditoria.HistoricoAuditoria set EJECUCION = " + EJECUCION + ", PREINFORME = " + PREINFORME + ",INFORME=" + INFORME + ",FechaRegistro=" + FechaRegistro + " WHERE IdAuditoria = '" + IdAuditoria + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void ModificarObjetivoGrupoAuditoria(String IdAuditoria, String IdObjetivo, String IdGrupoAuditoria)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("update Auditoria.AuditoriaObjetivo set IdGrupoAuditoria = '" + IdGrupoAuditoria + "' where IdAuditoria = '" + IdAuditoria + "' and IdObjetivo = '" + IdObjetivo + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        //04-04-2014
        //select IdGrupoAuditoria from Auditoria.AuditoriaObjetivo where IdAuditoria = 62 and IdObjetivo = 13
        public DataTable VerIdGrupoAuditoriaGuardado(String IdAuditoria)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select min(IdGrupoAuditoria) as IdGrupoAuditoria from Auditoria.AuditoriaObjetivo where IdAuditoria = '" + IdAuditoria + "'");
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

        public DataTable LoadGruposAuditoria(String IdGrupoAuditoria)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select a.IdGrupoAuditoria,b.idHijo,b.NombreHijoAuditoria,c.NombreResponsable from Auditoria.GrupoAuditoria a left join Auditoria.JerarquiaGrupoAuditoria b on a.IdGrupoAuditoria = b.idGrupoAuditoria left join Parametrizacion.DetalleJerarquiaOrg c on b.idHijo = c.idHijo where a.IdGrupoAuditoria = '" + IdGrupoAuditoria + "' order by b.idHijo");
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

        public void InsertarRecursosGruposAuditoria(String IdAuditoria, String IdObjetivo, String IdGrupoAuditoria, String IdHijo, String Etapa, String FechaInicial, String FechaFinal, String HorasPlaneadas, String FechaRegistro, String IdUsuario)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("insert into Auditoria.AudObjRecurso (IdAuditoria,IdObjetivo,IdGrupoAuditoria,IdHijo,Etapa,FechaInicial,FechaFinal,HorasPlaneadas,FechaRegistro,IdUsuario) values (" + IdAuditoria + "," + IdObjetivo + "," + IdGrupoAuditoria + "," + IdHijo + ",'" + Etapa + "',CONVERT(datetime, '" + FechaInicial + "', 120),CONVERT(datetime, '" + FechaFinal + "', 120)," + HorasPlaneadas + ", getdate()," + IdUsuario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void ModificarRecursosGruposAuditoria(String IdAuditoria, String IdObjetivo, String IdGrupoAuditoria, String IdHijo, String Etapa, String FechaInicial, String FechaFinal, String HorasPlaneadas)
        {
            try
            {
                cDataBase.conectar();
                //24-04-2014
                //cDataBase.ejecutarConsulta("update Auditoria.AudObjRecurso set IdHijo = '" + IdHijo + "', Etapa = '" + Etapa + "',FechaInicial = CONVERT(datetime, '" + FechaInicial + "', 120), FechaFinal = CONVERT(datetime, '" + FechaFinal + "', 120), HorasPlaneadas = '" + HorasPlaneadas + "' where IdAuditoria = '" + IdAuditoria + "' and IdObjetivo = '" + IdObjetivo + "' and IdGrupoAuditoria = '" + IdGrupoAuditoria + "'");
                cDataBase.ejecutarConsulta("update Auditoria.AudObjRecurso set Etapa = '" + Etapa + "',FechaInicial = CONVERT(datetime, '" + FechaInicial + "', 120), FechaFinal = CONVERT(datetime, '" + FechaFinal + "', 120), HorasPlaneadas = '" + HorasPlaneadas + "' where IdAuditoria = '" + IdAuditoria + "' and IdObjetivo = '" + IdObjetivo + "' and IdGrupoAuditoria = '" + IdGrupoAuditoria + "' and IdHijo = '" + IdHijo + "' and Etapa = '" + Etapa + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void EliminarRecursosGruposAuditoria(String IdAuditoria, String IdObjetivo, String IdGrupoAuditoria, String IdHijo, String Etapa)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("delete Auditoria.AudObjRecurso where IdAuditoria = '" + IdAuditoria + "' and IdObjetivo = '" + IdObjetivo + "' and IdGrupoAuditoria = '" + IdGrupoAuditoria + "' and IdHijo = '" + IdHijo + "' and Etapa = '" + Etapa + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable LoasRecursosPlaneacion(String IdAuditoria, String IdObjetivo, String IdGrupoAuditoria)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                //24-04-2014
                //dtInformacion = cDataBase.ejecutarConsulta("select a.Etapa,a.IdHijo,c.NombreResponsable,REPLACE(CONVERT(varchar, a.FechaInicial, 111), '/', '-') AS FechaInicial,REPLACE(CONVERT(varchar, a.FechaFinal, 111), '/', '-') AS FechaFinal,a.HorasPlaneadas from Auditoria.AudObjRecurso a left join Parametrizacion.JerarquiaOrganizacional b on a.IdHijo = b.idHijo left join Parametrizacion.DetalleJerarquiaOrg c on b.idHijo =  c.idHijo where a.IdAuditoria = '" + IdAuditoria + "' and a.IdObjetivo = '" + IdObjetivo + "' and IdGrupoAuditoria = '" + IdGrupoAuditoria + "'");
                dtInformacion = cDataBase.ejecutarConsulta("select a.Etapa,a.IdHijo,c.NombreResponsable,REPLACE(CONVERT(varchar, a.FechaInicial, 111), '/', '-') AS FechaInicial,REPLACE(CONVERT(varchar, a.FechaFinal, 111), '/', '-') AS FechaFinal,a.HorasPlaneadas from Auditoria.AudObjRecurso a left join Parametrizacion.JerarquiaOrganizacional b on a.IdHijo = b.idHijo left join Parametrizacion.DetalleJerarquiaOrg c on b.idHijo =  c.idHijo where a.IdAuditoria = '" + IdAuditoria + "' and a.IdObjetivo = '" + IdObjetivo + "' and IdGrupoAuditoria = '" + IdGrupoAuditoria + "' order by a.FechaRegistro");
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

        //04-04-2014
        public void PrecierreHallazgos(String IdPlaneacion, String IdAuditoria, String Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("update Auditoria.Auditoria set Estado = '" + Estado + "' WHERE IdPlaneacion = '" + IdPlaneacion + "' AND IdAuditoria = '" + IdAuditoria + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        //traer colores
        public DataTable LoadColores()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdColor,Color,ColorMinimo,ColorMaximo FROM Auditoria.ColorRpt order by IdColor");
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


        public byte[] mtdDescargarEvidencia(string strNombreArchivo)
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [QA].[EvidenciaAdjunto] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);

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
        public void ActualizarColores(String min1, String max1, String min2, String max2, String min3, String max3)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta("update Auditoria.ColorRpt set ColorMinimo = '" + min1 + "', ColorMaximo = '" + max1 + "' where IdColor = 1");
                cDataBase.ejecutarConsulta("update Auditoria.ColorRpt set ColorMinimo = '" + min2 + "', ColorMaximo = '" + max2 + "' where IdColor = 2");
                cDataBase.ejecutarConsulta("update Auditoria.ColorRpt set ColorMinimo = '" + min3 + "', ColorMaximo = '" + max3 + "' where IdColor = 3");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        //24-04-2014
        public DataTable CalcularDias(String Inicio, String Fin)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                //dtInformacion = cDataBase.ejecutarConsulta("select convert(varchar(10),Parametrizacion.FN_Dias_Laborables(CONVERT(varchar, '" + Inicio + "'),CONVERT(varchar, '" + Fin + "'))) Dias");
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.FN_Dias_Laborables(CONVERT(datetime, '" + Inicio + "',120), CONVERT(datetime, '" + Fin + "',120)) Dias");
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

        public DataTable LiderGrAudutoria(String IdAudioria)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select top 1 a.IdAuditoria,b.IdGrupoAuditoria,d.idPadre from Auditoria.Auditoria a left join Auditoria.AudObjRecurso b on a.IdAuditoria = b.IdAuditoria left join Auditoria.JerarquiaGrupoAuditoria d on b.IdGrupoAuditoria = d.idGrupoAuditoria where d.idPadre <> 0 and a.IdAuditoria = " + IdAudioria);
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
        public DataTable loadCodigoArchivo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdArchivo+1 AS NumRegistros FROM Auditoria.Archivo ORDER BY IdArchivo DESC");
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

        public DataTable loadCodigoArchivoAud()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdArchivo+1 AS NumRegistros FROM Auditoria.ArchivoHallazgoAuditoria ORDER BY IdArchivo DESC");
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

        public void mtdAgregarArchivoPdf(string strIdRegistro, string strIdControlUsuario,
            string strDescripcion, string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Auditoria].[Archivo] ([UrlArchivo], [Descripcion], [FechaRegistro], [IdUsuario], [IdRegistro], [IdControlUsuario], [ArchivoPDF]) VALUES (N'{0}', N'{1}', GETDATE(), {2}, {3}, {4}, @PdfData)",
                    strUrlArchivo, strDescripcion, IdUsuario.ToString().Trim(), strIdRegistro, strIdControlUsuario);

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

        public void mtdAgregarArchivoAud(string strCodAuditoria, string strDescripcion,  string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Auditoria].[ArchivoHallazgoAuditoria] ([IdRegistroAuditoria], [NombreUsuario], [Descripcion], [UrlArchivo], [Archivo], [FechaRegistro]) VALUES ({0}, '{1}', N'{2}', N'{3}', @PdfData, GETDATE())",
                    strCodAuditoria, NombreUsuario.ToString().Trim(), strDescripcion, strUrlArchivo);

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


        public DataTable ArchivoAud(String CodRegAuditoria)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                string query = string.Format("SELECT [IdArchivo],[NombreUsuario],[FechaRegistro],[Descripcion],[UrlArchivo]" +"\n"+
                "FROM [Auditoria].[ArchivoHallazgoAuditoria]  WHERE [IdRegistroAuditoria] = {0}" + "\n" +
                "union all" + "\n" +
                "SELECT a.[IdArchivo],(D.Nombres + ' ' + D.Apellidos) as NombreUsuario,a.FechaRegistro,a.Descripcion,a.UrlArchivo" + "\n" +
                "FROM [Auditoria].[Archivo] A " + "\n" +
                "INNER JOIN Auditoria.Hallazgo B on b.IdHallazgo = a.IdRegistro " + "\n" +
                "INNER JOIN Auditoria.Auditoria C on c.IdAuditoria = b.IdAuditoria " + "\n" +
                "INNER JOIN Listas.Usuarios D on D.IdUsuario = a.IdUsuario " + "\n" +
                "where c.IdAuditoria = {0}", CodRegAuditoria);
                //string query = string.Format("SELECT * FROM [Auditoria].[ArchivoHallazgoAuditoria] WHERE [IdRegistroAuditoria] = {0} ", CodRegAuditoria);
                //string strConsulta = ;
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

        public byte[] mtdDescargarArchivoPdf(string strNombreArchivo)
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo],Archivo " + "\n" +
                "FROM [Auditoria].[ArchivoHallazgoAuditoria]  WHERE [UrlArchivo] = N'{0}'" + "\n" +
                "union all" + "\n" +
                "SELECT a.[UrlArchivo], a.[ArchivoPDF]" + "\n" +
                "FROM [Auditoria].[Archivo] A " + "\n" +
                "INNER JOIN Auditoria.Hallazgo B on b.IdHallazgo = a.IdRegistro " + "\n" +
                "INNER JOIN Auditoria.Auditoria C on c.IdAuditoria = b.IdAuditoria " + "\n" +
                "INNER JOIN Listas.Usuarios D on D.IdUsuario = a.IdUsuario " + "\n" +
                "where a.UrlArchivo = N'{0}'", strNombreArchivo);
                //strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [Auditoria].[Archivo] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);

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


        public byte[] mtdDescargarArchivoAud(string strNombreArchivo)
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo],Archivo " + "\n" +
                "FROM [Auditoria].[ArchivoHallazgoAuditoria]  WHERE [UrlArchivo] = N'{0}'" + "\n" +
                "union all" + "\n" +
                "SELECT a.[UrlArchivo], a.[ArchivoPDF]" + "\n" +
                "FROM [Auditoria].[Archivo] A " + "\n" +
                "INNER JOIN Auditoria.Hallazgo B on b.IdHallazgo = a.IdRegistro " + "\n" +
                "INNER JOIN Auditoria.Auditoria C on c.IdAuditoria = b.IdAuditoria " + "\n" +
                "INNER JOIN Listas.Usuarios D on D.IdUsuario = a.IdUsuario " + "\n" +
                "where a.UrlArchivo = N'{0}'", strNombreArchivo);
                //strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [Auditoria].[Archivo] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);

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
        #endregion

        public DataSets.DataSet mtdConsultarHallazgoVsPlanAccion(string strNivelRiesgo, string strEdadHallazgo,
            string strProceso, string strEstado, ref string strMessage)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataSets.DataSet dsInformacion = new DataSets.DataSet();
            SqlParameter[] objSqlParams = new SqlParameter[4];
            #endregion Vars

            try
            {
                objSqlParams[0] = new SqlParameter("@IdNivelRiesgo", strNivelRiesgo);
                objSqlParams[1] = new SqlParameter("@EdadHallazgo", strEdadHallazgo);
                objSqlParams[2] = new SqlParameter("@IdProceso", strProceso);
                objSqlParams[3] = new SqlParameter("@IdEstado", strEstado);

                cDatabase.conectar();
                dsInformacion = cDatabase.mtdEjecutarSPParametroSQL("SP_HallazgoVSPlanAccion", "DSHallazgoVsPlanAccionTableAdapters", objSqlParams);
            }
            catch (Exception ex)
            {
                dsInformacion = null;
                cDatabase.desconectar();
                strMessage = string.Format("Error al consultar los Hallazgos VS Planes Accion. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dsInformacion;
        }

        public DataSets.DataSet mtdConsultaHallazgos(string strIdUsuario, string strIdPlaneacion, int intTipoBusqueda,
            string strConn,
            ref string strMessage)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataSets.DataSet dsInfo = new DataSets.DataSet();
            #endregion Vars

            try
            {
                #region Creacion Consulta
                switch (intTipoBusqueda)
                {
                    case 1:
                        #region Consulta
                        strConsulta = string.Format("SELECT A.[IdAuditoria], A.[Tema] NombreAuditoria, H.[IdHallazgo], H.[Hallazgo], " +
                            "ISNULL(R.[IdPlanAccion], '') IdPlanAccion, ISNULL(R.[Descripcion], '') Descripcion, " +
                            "DATEDIFF(DAY, H.FechaRegistro, GETDATE()) EdadHallazgo,ISNULL(PDT.IdDetalleTipo, '') IdNivelRiesgo, ISNULL(PDT.NombreDetalle, '') NivelRiesgo " +
                            "FROM [Auditoria].[Auditoria] A " +
                            "INNER JOIN [Auditoria].[Hallazgo] H ON H.IdAuditoria = A.IdAuditoria " +
                            "LEFT JOIN (SELECT DISTINCT H.IdHallazgo, CONVERT(VARCHAR(MAX), H.[Hallazgo]) Hallazgo, " +
                                "ISNULL(PA.[IdPlanAccion], '') IdPlanAccion, CONVERT(VARCHAR(MAX),ISNULL(PA.[Descripcion], '')) Descripcion, PA.IdUsuario " +
                                "FROM [Auditoria].[Hallazgo] H " +
                                "LEFT JOIN [Auditoria].[Recomendacion] R ON H.[IdHallazgo] = R.[IdHallazgo] " +
                                "LEFT JOIN [Auditoria].[PlanAccion] PA ON R.[IdRecomendacion] = PA.[IdForanea] AND PA.[TipoForanea] = 'RECOMENDACION') R ON H.[IdHallazgo] = R.[IdHallazgo] " +
                            "LEFT JOIN [Parametrizacion].[Tipos] T ON T.[IdTipo] =  H.[IdNivelRiesgo] AND T.[NombreTipo] = 'Nivel de Riesgo' " +
                            "LEFT JOIN [Parametrizacion].[DetalleTipos] PDT ON T.IdTipo = PDT.IdTipo " +
                            "WHERE A.[Estado] = 'CUMPLIDA' AND A.[IdPlaneacion] = {0} ", strIdPlaneacion);
                        #endregion
                        break;
                    case 2:
                        #region Consulta
                        strConsulta = string.Format("SELECT A.[IdAuditoria], A.[Tema] NombreAuditoria, H.[IdHallazgo], CONVERT(VARCHAR(MAX),H.[Hallazgo]) Hallazgo, " +
                            "ISNULL(R.[IdPlanAccion], '') IdPlanAccion, ISNULL(R.[Descripcion], '') Descripcion, " +
                            "DATEDIFF(DAY, H.FechaRegistro, GETDATE()) EdadHallazgo,ISNULL(PDT.IdDetalleTipo, '') IdNivelRiesgo, ISNULL(PDT.NombreDetalle, '') NivelRiesgo " +
                            "FROM [Auditoria].[Auditoria] A " +
                            "INNER JOIN [Auditoria].[Hallazgo] H ON H.IdAuditoria = A.IdAuditoria " +
                            "LEFT JOIN (SELECT DISTINCT H.IdHallazgo, CONVERT(VARCHAR(MAX), H.[Hallazgo]) Hallazgo, " +
                                "ISNULL(PA.[IdPlanAccion], '') IdPlanAccion, CONVERT(VARCHAR(MAX),ISNULL(PA.[Descripcion], '')) Descripcion, PA.IdUsuario " +
                                "FROM [Auditoria].[Hallazgo] H " +
                                "LEFT JOIN [Auditoria].[Recomendacion] R ON H.[IdHallazgo] = R.[IdHallazgo] " +
                                "LEFT JOIN [Auditoria].[PlanAccion] PA ON R.[IdRecomendacion] = PA.[IdForanea] AND PA.[TipoForanea] = 'RECOMENDACION') R ON H.[IdHallazgo] = R.[IdHallazgo] " +
                            "LEFT JOIN [Parametrizacion].[Tipos] T ON T.[IdTipo] =  H.[IdNivelRiesgo] AND T.[NombreTipo] = 'Nivel de Riesgo' " +
                            "LEFT JOIN [Parametrizacion].[DetalleTipos] PDT ON T.IdTipo = PDT.IdTipo " +
                            "INNER JOIN [Listas].[Usuarios] LU ON LU.IdJerarquia = A.IdDependencia " +
                            "WHERE A.[Estado] = 'CUMPLIDA' AND A.[IdPlaneacion] = {0} AND LU.[IdUsuario] = {1} " +
                            "UNION " +
                            "SELECT A.[IdAuditoria], A.[Tema] NombreAuditoria, H.[IdHallazgo], CONVERT(VARCHAR(MAX),H.[Hallazgo]) Hallazgo, " +
                            "ISNULL(R.[IdPlanAccion], '') IdPlanAccion, ISNULL(R.[Descripcion], '') Descripcion, " +
                            "DATEDIFF(DAY, H.FechaRegistro, GETDATE()) EdadHallazgo,ISNULL(PDT.IdDetalleTipo, '') IdNivelRiesgo, ISNULL(PDT.NombreDetalle, '') NivelRiesgo " +
                            "FROM [Auditoria].[Auditoria] A " +
                            "INNER JOIN [Auditoria].[Hallazgo] H ON H.IdAuditoria = A.IdAuditoria " +
                            "LEFT JOIN (SELECT DISTINCT H.IdHallazgo, CONVERT(VARCHAR(MAX), H.[Hallazgo]) Hallazgo, " +
                                "ISNULL(PA.[IdPlanAccion], '') IdPlanAccion, CONVERT(VARCHAR(MAX),ISNULL(PA.[Descripcion], '')) Descripcion, PA.IdUsuario " +
                                "FROM [Auditoria].[Hallazgo] H " +
                                "LEFT JOIN [Auditoria].[Recomendacion] R ON H.[IdHallazgo] = R.[IdHallazgo] " +
                                "LEFT JOIN [Auditoria].[PlanAccion] PA ON R.[IdRecomendacion] = PA.[IdForanea] AND PA.[TipoForanea] = 'RECOMENDACION') R ON H.[IdHallazgo] = R.[IdHallazgo] " +
                            "LEFT JOIN [Parametrizacion].[Tipos] T ON T.[IdTipo] =  H.[IdNivelRiesgo] AND T.[NombreTipo] = 'Nivel de Riesgo' " +
                            "LEFT JOIN [Parametrizacion].[DetalleTipos] PDT ON T.IdTipo = PDT.IdTipo " +
                            "INNER JOIN [Procesos].[Proceso] PP ON PP.IdProceso = A.IdProceso " +
                            "INNER JOIN [Listas].[Usuarios] LU ON LU.IdJerarquia = PP.IdHijo  " +
                            "WHERE A.[Estado] = 'CUMPLIDA' AND A.[IdPlaneacion] = {0} AND LU.[IdUsuario] = {1}",
                            strIdPlaneacion, strIdUsuario);
                        #endregion
                        break;
                }
                #endregion

                SqlConnection sqlCnn = new SqlConnection(strConn);
                SqlCommand sqlCmd = new SqlCommand(strConsulta, sqlCnn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd);

                sqlAdapter.Fill(dsInfo, "DataSet1");
            }
            catch (Exception ex)
            {
                dsInfo = null;
                strMessage = string.Format("Error al consultar los Hallazgos. [{0}]", ex.Message);
            }

            return dsInfo;
        }

        public DataSets.DataSet mtdConsultaRecomendacion(string strIdUsuario, string strIdPlaneacion, string strIdHallazgo,
            int intTipoBusqueda, string strConn, ref string strMessage)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataSets.DataSet dsInfo = new DataSets.DataSet();
            #endregion Vars

            try
            {
                #region Creacion Consulta
                switch (intTipoBusqueda)
                {
                    case 1:
                        #region Consulta
                        strConsulta = string.Format("SELECT R.IdRecomendacion, CONVERT(VARCHAR(MAX), R.Observacion) Recomendacion, " +
                            "R.Estado, O.Nombre, R.Tipo, J.NombreHijo Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) FechaRegistro " +
                            "FROM Auditoria.Recomendacion R " +
                            "INNER JOIN Auditoria.Hallazgo H ON R.IdHallazgo = H.IdHallazgo " +
                            "INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria " +
                            "INNER JOIN Parametrizacion.JerarquiaOrganizacional J ON J.idHijo = R.IdDependenciaAuditada " +
                            "INNER JOIN Auditoria.DetalleEnfoque DE ON DE.IdDetalleEnfoque = H.IdDetalleEnfoque " +
                            "INNER JOIN Auditoria.Enfoque E ON E.IdEnfoque = DE.IdEnfoque " +
                            "INNER JOIN Auditoria.Objetivo O ON O.IdObjetivo = E.IdObjetivo " +
                            "WHERE R.Tipo = 'Dependencia' AND A.[Estado] = 'CUMPLIDA' " +
                            "AND A.[IdPlaneacion] = {0} AND R.[IdHallazgo] = {1} " +
                            "UNION " +
                            "SELECT R.IdRecomendacion, CONVERT(VARCHAR(MAX), R.Observacion) Recomendacion, " +
                            "R.Estado, O.Nombre, R.Tipo, P.Nombre Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) FechaRegistro " +
                            "FROM Auditoria.Recomendacion R " +
                            "INNER JOIN Auditoria.Hallazgo H ON R.IdHallazgo = H.IdHallazgo " +
                            "INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria " +
                            "INNER JOIN Procesos.Proceso P ON P.IdProceso = R.IdSubproceso " +
                            "INNER JOIN Auditoria.DetalleEnfoque DE ON DE.IdDetalleEnfoque = H.IdDetalleEnfoque " +
                            "INNER JOIN Auditoria.Enfoque E ON E.IdEnfoque = DE.IdEnfoque " +
                            "INNER JOIN Auditoria.Objetivo O ON O.IdObjetivo = E.IdObjetivo " +
                            "WHERE R.Tipo = 'Procesos' AND A.[Estado] = 'CUMPLIDA' " +
                            "AND A.[IdPlaneacion] = {0} AND R.[IdHallazgo] = {1}", strIdPlaneacion, strIdHallazgo);
                        #endregion
                        break;
                    case 2:
                        #region Consulta
                        strConsulta = string.Format("SELECT R.IdRecomendacion, CONVERT(VARCHAR(MAX), R.Observacion) Recomendacion, " +
                            "R.Estado, O.Nombre, R.Tipo, J.NombreHijo Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) FechaRegistro " +
                            "FROM Auditoria.Recomendacion R " +
                            "INNER JOIN Auditoria.Hallazgo H ON R.IdHallazgo = H.IdHallazgo " +
                            "INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria " +
                            "INNER JOIN Parametrizacion.JerarquiaOrganizacional J ON J.idHijo = R.IdDependenciaAuditada " +
                            "INNER JOIN Auditoria.DetalleEnfoque DE ON DE.IdDetalleEnfoque = H.IdDetalleEnfoque " +
                            "INNER JOIN Auditoria.Enfoque E ON E.IdEnfoque = DE.IdEnfoque " +
                            "INNER JOIN Auditoria.Objetivo O ON O.IdObjetivo = E.IdObjetivo " +
                            "INNER JOIN [Listas].[Usuarios] LU ON LU.IdJerarquia = A.IdDependencia " +
                            "WHERE R.Tipo = 'Dependencia' AND A.[Estado] = 'CUMPLIDA' " +
                            "AND A.[IdPlaneacion] = {0} AND R.[IdHallazgo] = {1} AND LU.[IdUsuario] = {2}" +
                            "UNION " +
                            "SELECT R.IdRecomendacion, CONVERT(VARCHAR(MAX), R.Observacion) Recomendacion, " +
                            "R.Estado, O.Nombre, R.Tipo, P.Nombre Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) FechaRegistro " +
                            "FROM Auditoria.Recomendacion R " +
                            "INNER JOIN Auditoria.Hallazgo H ON R.IdHallazgo = H.IdHallazgo " +
                            "INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria " +
                            "INNER JOIN Procesos.Proceso P ON P.IdProceso = R.IdSubproceso " +
                            "INNER JOIN Auditoria.DetalleEnfoque DE ON DE.IdDetalleEnfoque = H.IdDetalleEnfoque " +
                            "INNER JOIN Auditoria.Enfoque E ON E.IdEnfoque = DE.IdEnfoque " +
                            "INNER JOIN Auditoria.Objetivo O ON O.IdObjetivo = E.IdObjetivo " +
                            "INNER JOIN [Procesos].[Proceso] PP ON PP.IdProceso = A.IdProceso " +
                            "INNER JOIN [Listas].[Usuarios] LU ON LU.IdJerarquia = PP.IdHijo  " +
                            "WHERE R.Tipo = 'Procesos' AND A.[Estado] = 'CUMPLIDA' " +
                            "AND A.[IdPlaneacion] = {0} AND R.[IdHallazgo] = {1} AND LU.[IdUsuario] = {2}",
                            strIdPlaneacion, strIdHallazgo, strIdUsuario);
                        #endregion
                        break;
                }
                #endregion

                SqlConnection sqlCnn = new SqlConnection(strConn);
                SqlCommand sqlCmd = new SqlCommand(strConsulta, sqlCnn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd);

                sqlAdapter.Fill(dsInfo, "DataSet1");
            }
            catch (Exception ex)
            {
                dsInfo = null;
                strMessage = string.Format("Error al consultar los recomendaciones. [{0}]", ex.Message);
            }

            return dsInfo;
        }

        /*public byte[] mtdDescargarEvidencia(string strNombreArchivo)
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [QA].[EvidenciaAdjunto] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);

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
        }*/
    }
}