using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cRegistroOperacion : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;

        public DataTable loadDDLTipoRegistro()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoRegistro, NombreTipoRegistro FROM Proceso.TipoRegistro");
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

        public DataTable loadDDLTipoIden()
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

        public DataTable loadRteTraza(string Mes, string Ano, string IdRegistro, string IdEstado)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                string cond = string.Empty;
                if (IdRegistro != "0")
                    cond = " AND a.IdTipoRegistro IN (" + IdRegistro + ") ";

                if (IdEstado != "0")
                    cond += " AND a.IdTipoRegistro IN (" + IdEstado + ") ";
                string query = "SELECT RTRIM(a.Identificacion)Identificacion,RTRIM(a.NombreApellido)NombreApellido,"+"\n"+
                    "RTRIM(b.NombreTipoRegistro)TipoRegistro,RTRIM(c.NombreEstado)Estado,RTRIM(d.NombreResponsable)UsuarioAsignado," + "\n" +
                    "RTRIM(a.NombreIndicador)Indicador,RTRIM(a.Indicador)Descripcion,RTRIM(a.MensajeCorreo)Mensaje," + "\n" +
                    "CONVERT(VARCHAR,a.FechaRegistro,120)FechaRegistro,CONVERT(VARCHAR,a.FechaDeteccion,120)FechaDeteccion," + "\n" +
                    "CONVERT(VARCHAR,a.FechaPosibleSolucion,120)FechaPosibleSolucion,RTRIM(e.Comentario)Comentario,RTRIM(e.NombreUsuario)NombreUsuario," + "\n" +
                    "CONVERT(VARCHAR,e.FechaRegistro,120) FechaRegistroComentario " + "\n" +
                    "FROM Proceso.RegistroOperacion a " + "\n" +
                    "INNER JOIN Proceso.TipoRegistro b on a.IdTipoRegistro = b.IdTipoRegistro " + "\n" +
                    "INNER JOIN Proceso.EstadoOperacion c ON a.IdEstadoOperacion = c.IdEstadoOperacion " + "\n" +
                    "LEFT JOIN Parametrizacion.DetalleJerarquiaOrg d ON a.IdUsuario = d.idHijo " + "\n" +
                    "LEFT JOIN Proceso.ComentarioRegistroOperacion e ON a.IdRegistroOperacion = e.IdRegistroOperacion " + "\n" + 
                    "WHERE (a.IdEstadoOperacion in (" + IdEstado + ")) AND (a.IdTipoRegistro in (" + IdRegistro + ")) AND " + "\n" + 
                    "(MONTH(a.FechaRegistro) = '" + Mes + "' AND YEAR(a.FechaRegistro)= '" + Ano + "') " + 
                    " ORDER BY a.Identificacion, e.FechaRegistro";/*cond + OR (MONTH(a.FechaRegistro) = '" + Mes + 
                    "' AND YEAR(a.FechaRegistro)= '" + Ano + "') */
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

        public DataTable LoadAreaUsuario()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT a.IdUsuario,c.IdArea,c.NombreArea,c.Codigo FROM Listas.Usuarios a, Parametrizacion.DetalleJerarquiaOrg b,Parametrizacion.Area c where a.IdJerarquia = b.idHijo and a.IdUsuario = '" + IdUsuario.ToString() + "' and b.IdArea = c.IdArea");
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

        public DataTable LoadIndicador(String IdArea)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT(*)+1 Registros FROM Proceso.RegistroOperacion WHERE CodigoArea = '" + IdArea + "'");
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

        public string LoadIndicadorTotal()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT(*) Registros FROM Proceso.RegistroOperacion");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0]["Registros"].ToString().Trim();
        }

        public DataTable LoadAreas()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdArea,NombreArea,Codigo FROM Parametrizacion.Area ORDER BY NombreArea");
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
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdEstadoOperacion, NombreEstado FROM Proceso.EstadoOperacion WHERE IdEstadoOperacion < 6");
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

        public DataTable loadDDLArea()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT	IdRol AS IdArea, NombreRol AS NombreArea FROM Listas.Roles");
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

        public DataTable loadDDLPersona(String IdArea)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdUsuario, LTRIM(RTRIM(Nombres)) + ' ' + LTRIM(RTRIM(Apellidos)) AS NombreApellido FROM Listas.Usuarios WHERE (IdRol = " + IdArea + ")");
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

        public DataTable loadInfoConteoRegistros(String FechaRegistroDesde, String FechaRegistroHasta)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdConteo, NombreUsuario, CONVERT(varchar, FechaRegistro, 109) AS FechaRegistro, RegistrosCargue, RegistrosOperacion, Descripcion FROM Proceso.ConteoRegistro WHERE (FechaRegistro >= CONVERT(varchar, '" + FechaRegistroDesde + "', 120)) AND (FechaRegistro <= CONVERT(varchar, '" + FechaRegistroHasta + "', 120))");
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

        public DataTable loadInfoRegistroOperacion(String IdTipoRegistro, String IdEstadoOperacion, String IdFactorRiesgo, String IdIndicador, String Identificacion, String FechaDeteccionDesde, String FechaDeteccionHasta, String User, String IdArea, string VerROI)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty;
            try
            {
                if (IdTipoRegistro != "")
                {
                    condicion = "WHERE (Proceso.TipoRegistro.IdTipoRegistro = " + IdTipoRegistro + ") ";
                }
                //if (NNombreRol != "Oficial de cumplimiento" && NNombreRol != "Analista de cumplimiento" && NNombreRol != "Gestor Sarlaft")
                if (VerROI == "False")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (Proceso.EstadoOperacion.IdEstadoOperacion = 2 ) AND (Proceso.RegistroOperacion.IdUsuario = (select idHijo from Parametrizacion.JerarquiaOrganizacional where idHijo = (select IdJerarquia from listas.Usuarios where IdUsuario = " + IdUsuario.ToString() + " )))";
                    }
                    else
                    {
                        condicion += "AND (Proceso.EstadoOperacion.IdEstadoOperacion = 2 ) AND (Proceso.RegistroOperacion.IdUsuario = (select idHijo from Parametrizacion.JerarquiaOrganizacional where idHijo = (select IdJerarquia from listas.Usuarios where IdUsuario = " + IdUsuario.ToString() + " )))";
                    }
                }
                else
                {
                    if (IdEstadoOperacion != "")
                    {
                        if (condicion.Trim() == "")
                        {
                            condicion = "WHERE (Proceso.EstadoOperacion.IdEstadoOperacion = " + IdEstadoOperacion + ") ";
                        }
                        else
                        {
                            condicion += "AND (Proceso.EstadoOperacion.IdEstadoOperacion = " + IdEstadoOperacion + ") ";
                        }
                    }
                }
                if (IdFactorRiesgo != "")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (/*Parametrizacion.FactorRiesgo.IdFactorRiesgo*/ fr.IdFactorRiesgo= " + IdFactorRiesgo + ") ";
                    }
                    else
                    {
                        condicion += "AND (/*Parametrizacion.FactorRiesgo.IdFactorRiesgo*/ fr.IdFactorRiesgo= " + IdFactorRiesgo + ") ";
                    }
                }
                if (IdIndicador != "")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (Proceso.RegistroOperacion.IdIndicador = " + IdIndicador + ") ";
                    }
                    else
                    {
                        condicion += "AND (Proceso.RegistroOperacion.IdIndicador = " + IdIndicador + ") ";
                    }
                }
                if (Identificacion != "")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (Proceso.RegistroOperacion.Identificacion LIKE N'%" + Identificacion + "%') ";
                    }
                    else
                    {
                        condicion += "AND (Proceso.RegistroOperacion.Identificacion LIKE N'%" + Identificacion + "%') ";
                    }
                }
                if (FechaDeteccionDesde != "")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (Proceso.RegistroOperacion.FechaDeteccion >= CONVERT(datetime, '" + FechaDeteccionDesde + "',120)) ";
                    }
                    else
                    {
                        condicion += "AND (Proceso.RegistroOperacion.FechaDeteccion >= CONVERT(datetime, '" + FechaDeteccionDesde + "',120)) ";
                    }
                }
                if (FechaDeteccionHasta != "")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (Proceso.RegistroOperacion.FechaDeteccion <= CONVERT(datetime, '" + FechaDeteccionHasta + "',120)) ";
                    }
                    else
                    {
                        condicion += "AND (Proceso.RegistroOperacion.FechaDeteccion <= CONVERT(datetime, '" + FechaDeteccionHasta + "',120)) ";
                    }
                }
                //if (IdArea != "" && IdArea != "---")
                //{
                //    if (condicion.Trim() == "")
                //    {
                //        condicion = "WHERE (Proceso.RegistroOperacion.CodigoArea = '" + IdArea + "') ";
                //    }
                //    else
                //    {
                //        condicion += "AND (Proceso.RegistroOperacion.CodigoArea = '" + IdArea + "') ";
                //    }
                //}
                /*if (User != "Oficial de cumplimiento")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (Proceso.RegistroOperacion.IdUsuario = " + IdJerarquia + ")";
                    }
                    else
                    {
                        condicion += "AND (Proceso.RegistroOperacion.IdUsuario = " + IdJerarquia + ")";
                    }
                }*/
                //string query = "SELECT	Proceso.RegistroOperacion.IdRegistroOperacion, Proceso.TipoRegistro.NombreTipoRegistro, Proceso.EstadoOperacion.NombreEstado, ISNULL(Parametrizacion.JerarquiaOrganizacional.NombreHijo, '') AS UsuarioAsignado, Proceso.RegistroOperacion.IdUsuario AS idNodoJerarquia, Proceso.RegistroOperacion.Identificacion, Proceso.RegistroOperacion.NombreApellido, Proceso.RegistroOperacion.NombreIndicador, Proceso.RegistroOperacion.Indicador, Proceso.RegistroOperacion.MensajeCorreo, REPLACE(CONVERT(varchar, Proceso.RegistroOperacion.FechaRegistro, 111), '/', '-') AS FechaRegistro, REPLACE(CONVERT(varchar, Proceso.RegistroOperacion.FechaDeteccion, 111), '/', '-') AS FechaDeteccion, ISNULL(REPLACE(CONVERT(varchar, Proceso.RegistroOperacion.FechaPosibleSolucion, 111), '/', '-'), '') AS FechaPosibleSolucion, ISNULL(Proceso.ConteoRegistro.RegistrosCargue, 0) AS RegistrosCargue, ISNULL(Proceso.ConteoRegistro.RegistrosOperacion, 0) AS RegistrosOperacion FROM	Parametrizacion.PerfilSegmento INNER JOIN Parametrizacion.Indicador ON Parametrizacion.PerfilSegmento.IdPerfil = Parametrizacion.Indicador.IdPerfil INNER JOIN Parametrizacion.AtributoSegmentacion ON Parametrizacion.PerfilSegmento.IdAtributo = Parametrizacion.AtributoSegmentacion.IdAtributo AND Parametrizacion.PerfilSegmento.IdAtributo = Parametrizacion.AtributoSegmentacion.IdAtributo AND Parametrizacion.PerfilSegmento.IdAtributo = Parametrizacion.AtributoSegmentacion.IdAtributo AND Parametrizacion.PerfilSegmento.IdAtributo = Parametrizacion.AtributoSegmentacion.IdAtributo INNER JOIN Parametrizacion.TipoSegmento ON Parametrizacion.AtributoSegmentacion.IdTipoSegmento = Parametrizacion.TipoSegmento.IdTipoSegmento INNER JOIN Parametrizacion.Segmento ON Parametrizacion.TipoSegmento.IdSegmento = Parametrizacion.Segmento.IdSegmento AND Parametrizacion.TipoSegmento.IdSegmento = Parametrizacion.Segmento.IdSegmento AND Parametrizacion.TipoSegmento.IdSegmento = Parametrizacion.Segmento.IdSegmento AND Parametrizacion.TipoSegmento.IdSegmento = Parametrizacion.Segmento.IdSegmento AND Parametrizacion.TipoSegmento.IdSegmento = Parametrizacion.Segmento.IdSegmento INNER JOIN Parametrizacion.FactorRiesgo ON Parametrizacion.Segmento.IdFactorRiesgo = Parametrizacion.FactorRiesgo.IdFactorRiesgo AND Parametrizacion.Segmento.IdFactorRiesgo = Parametrizacion.FactorRiesgo.IdFactorRiesgo AND Parametrizacion.Segmento.IdFactorRiesgo = Parametrizacion.FactorRiesgo.IdFactorRiesgo AND Parametrizacion.Segmento.IdFactorRiesgo = Parametrizacion.FactorRiesgo.IdFactorRiesgo AND Parametrizacion.Segmento.IdFactorRiesgo = Parametrizacion.FactorRiesgo.IdFactorRiesgo RIGHT OUTER JOIN Proceso.RegistroOperacion INNER JOIN Proceso.TipoRegistro ON Proceso.RegistroOperacion.IdTipoRegistro = Proceso.TipoRegistro.IdTipoRegistro INNER JOIN Proceso.EstadoOperacion ON Proceso.RegistroOperacion.IdEstadoOperacion = Proceso.EstadoOperacion.IdEstadoOperacion ON Parametrizacion.Indicador.IdIndicador = Proceso.RegistroOperacion.IdIndicador LEFT OUTER JOIN Parametrizacion.JerarquiaOrganizacional ON Proceso.RegistroOperacion.IdUsuario = Parametrizacion.JerarquiaOrganizacional.idHijo LEFT JOIN Proceso.ConteoRegistro ON Proceso.ConteoRegistro.IdConteo = Proceso.RegistroOperacion.IdConteo " + condicion;
                string query = "SELECT	Proceso.RegistroOperacion.IdRegistroOperacion, Proceso.TipoRegistro.NombreTipoRegistro,"+"\n"+
"Proceso.EstadoOperacion.NombreEstado, ISNULL(Parametrizacion.JerarquiaOrganizacional.NombreHijo, '') AS UsuarioAsignado,"+"\n"+
"Proceso.RegistroOperacion.IdUsuario AS idNodoJerarquia, Proceso.RegistroOperacion.Identificacion, "+"\n"+
"Proceso.RegistroOperacion.NombreApellido, Proceso.RegistroOperacion.NombreIndicador, Proceso.RegistroOperacion.Indicador, " + "\n" +
"Proceso.RegistroOperacion.MensajeCorreo, " + "\n" +
"REPLACE(CONVERT(varchar, Proceso.RegistroOperacion.FechaRegistro, 111), '/', '-') AS FechaRegistro, " + "\n" +
"REPLACE(CONVERT(varchar, Proceso.RegistroOperacion.FechaDeteccion, 111), '/', '-') AS FechaDeteccion, " + "\n" +
"ISNULL(REPLACE(CONVERT(varchar, Proceso.RegistroOperacion.FechaPosibleSolucion, 111), '/', '-'), '') AS FechaPosibleSolucion, " + "\n" +
"ISNULL(Proceso.ConteoRegistro.RegistrosCargue, 0) AS RegistrosCargue, " + "\n" +
"ISNULL(Proceso.ConteoRegistro.RegistrosOperacion, 0) AS RegistrosOperacion " + "\n" +
"FROM Parametrizacion.PerfilSegmento " + "\n" +
"INNER JOIN Parametrizacion.Indicador ON Parametrizacion.PerfilSegmento.IdPerfil = Parametrizacion.Indicador.IdPerfil " + "\n" +
"INNER JOIN Parametrizacion.AtributoSegmentacion " + "\n" +
"ON Parametrizacion.PerfilSegmento.IdAtributo = Parametrizacion.AtributoSegmentacion.IdAtributo " + "\n" +
"INNER JOIN Parametrizacion.TipoSegmento " + "\n" +
"ON Parametrizacion.AtributoSegmentacion.IdTipoSegmento = Parametrizacion.TipoSegmento.IdTipoSegmento " + "\n" +
"INNER JOIN Parametrizacion.Segmento ON Parametrizacion.TipoSegmento.IdSegmento = Parametrizacion.Segmento.IdSegmento " + "\n" +
//"INNER JOIN Parametrizacion.FactorRiesgo " + "\n" +
//"ON Parametrizacion.Segmento.IdFactorRiesgo = Parametrizacion.FactorRiesgo.IdFactorRiesgo " + "\n" +
"RIGHT OUTER JOIN Proceso.RegistroOperacion " + "\n" +
"ON Parametrizacion.Indicador.IdIndicador = Proceso.RegistroOperacion.IdIndicador " + "\n" +
"LEFT OUTER JOIN [Perfiles].[tblFactorSenal] AS fs on fs.IdSenal = Proceso.RegistroOperacion.IdIndicador "+"\n"+
"LEFT OUTER JOIN Perfiles.tblFactorRiesgo as fr on fr.IdFactorRiesgo = fs.IdFactorRiesgo " + "\n" +
"INNER JOIN Proceso.TipoRegistro ON Proceso.RegistroOperacion.IdTipoRegistro = Proceso.TipoRegistro.IdTipoRegistro " + "\n" +
"INNER JOIN Proceso.EstadoOperacion ON " + "\n" +
"Proceso.RegistroOperacion.IdEstadoOperacion = Proceso.EstadoOperacion.IdEstadoOperacion " + "\n" +
"LEFT OUTER JOIN Parametrizacion.JerarquiaOrganizacional ON " + "\n" +
"Proceso.RegistroOperacion.IdUsuario = Parametrizacion.JerarquiaOrganizacional.idHijo " + "\n" +
"LEFT JOIN Proceso.ConteoRegistro ON Proceso.ConteoRegistro.IdConteo = Proceso.RegistroOperacion.IdConteo "
+ condicion;
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

        public void updateFechaSolucion(String IdRegistroOperacion, String FechaPosibleSolucion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Proceso.RegistroOperacion SET FechaPosibleSolucion = CONVERT(datetime, '" + FechaPosibleSolucion + "',120) WHERE (IdRegistroOperacion = " + IdRegistroOperacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateUsuarioAsignado(String IdRegistroOperacion, String IdUsuarioAsignado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Proceso.RegistroOperacion SET IdEstadoOperacion = 2, IdUsuario = " + IdUsuarioAsignado + " WHERE (IdRegistroOperacion = " + IdRegistroOperacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateEstadoDocumentado(String IdRegistroOperacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Proceso.RegistroOperacion SET IdEstadoOperacion = 3 WHERE (IdRegistroOperacion = " + IdRegistroOperacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateEstadoRechazado(String IdRegistroOperacion, String Estado)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Proceso.RegistroOperacion SET IdEstadoOperacion = '" + Estado + "' WHERE (IdRegistroOperacion = " + IdRegistroOperacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateEstadoAprobado(String NombreTipoRegistro, String IdRegistroOperacion, string Estado)
        {
            try
            {
                cDataBase.conectar();
                switch (NombreTipoRegistro)
                {
                    case "ROS":
                        cDataBase.ejecutarQuery("UPDATE Proceso.RegistroOperacion SET IdEstadoOperacion = '" + Estado + "' WHERE (IdRegistroOperacion = " + IdRegistroOperacion + ")");
                        cDataBase.ejecutarQuery("INSERT INTO Proceso.ComentarioRegistroOperacion (IdRegistroOperacion, NombreUsuario, FechaRegistro, Comentario) VALUES (" + IdRegistroOperacion + ", '" + NombreUsuario + "', GETDATE(), N'Cambio de estado a Aprobado.')");
                        break;
                    default:
                        cDataBase.ejecutarQuery("UPDATE Proceso.RegistroOperacion SET IdTipoRegistro = 2, IdEstadoOperacion = '" + Estado + "', IdUsuario = 0, FechaDeteccion = GETDATE() WHERE (IdRegistroOperacion = " + IdRegistroOperacion + ")");
                        cDataBase.ejecutarQuery("INSERT INTO Proceso.ComentarioRegistroOperacion (IdRegistroOperacion, NombreUsuario, FechaRegistro, Comentario) VALUES (" + IdRegistroOperacion + ", '" + NombreUsuario + "', GETDATE(), N'Detectado como ROS.')");
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
        }

        public DataTable loadInfoComentarioRegistroOperacion(String IdRegistroOperacion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdComentario, NombreUsuario, FechaRegistro, LTRIM(RTRIM(SUBSTRING(Comentario, 1, 20))) + '...' AS ComentarioCorto, Comentario FROM Proceso.ComentarioRegistroOperacion WHERE (IdRegistroOperacion = " + IdRegistroOperacion + ")");
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

        //camilo 12-10-2015
        public DataTable loadInfoGridArchivoRegistroOperacion(String IdRegistroOperacion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdArchivo, NombreUsuario, FechaRegistro, UrlArchivo FROM Proceso.ArchivoRegistroOperacion WHERE (IdRegistroOperacion = " + IdRegistroOperacion + ")");
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

        public DataTable loadInfoIdRegistro_Indicador(string IdIndicador)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdArchivo, NombreUsuario, FechaRegistro, UrlArchivo FROM Proceso.ArchivoRegistroOperacion WHERE (IdRegistroOperacion = ( select IdRegistroOperacion from Proceso.RegistroOperacion WHERE NombreIndicador = '" + IdIndicador + "' ))");
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

        public void agregarComentario(String IdRegistroOperacion, String Comentario)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Proceso.ComentarioRegistroOperacion (IdRegistroOperacion, NombreUsuario, FechaRegistro, Comentario) VALUES (" + IdRegistroOperacion + ", '" + NombreUsuario + "', GETDATE(), N'" + Comentario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable loadCodigoArchivoRegistroOperacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdArchivo+1 AS NumRegistros FROM Proceso.ArchivoRegistroOperacion ORDER BY IdArchivo DESC");
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

        public void agregarRegistroOperacion(String IdTipoRegistro, String Identificacion, String NombreApellido, String NombreIndicador, String Indicador, String MensajeCorreo, String FechaDeteccion, String Descripcion, String Comentario, String IdTipoIden, String DV, String CodigoArea)
        {
            try
            {
                parameters = new OleDbParameter[13];
                parameter = new OleDbParameter("@IdTipoRegistro", OleDbType.Integer);
                parameter.Value = IdTipoRegistro;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@Identificacion", OleDbType.Char);
                parameter.Value = Identificacion;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@NombreApellido", OleDbType.Char);
                parameter.Value = NombreApellido;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@NombreIndicador", OleDbType.Char);
                parameter.Value = NombreIndicador;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@Indicador", OleDbType.Char);
                parameter.Value = Indicador;
                parameters[4] = parameter;
                parameter = new OleDbParameter("@MensajeCorreo", OleDbType.Char);
                parameter.Value = MensajeCorreo;
                parameters[5] = parameter;
                parameter = new OleDbParameter("@FechaDeteccion", OleDbType.Char);
                parameter.Value = FechaDeteccion;
                parameters[6] = parameter;
                parameter = new OleDbParameter("@IdUsuarioRegistro", OleDbType.Integer);
                parameter.Value = IdUsuario;
                parameters[7] = parameter;
                parameter = new OleDbParameter("@Descripcion", OleDbType.Char);
                parameter.Value = Descripcion;
                parameters[8] = parameter;
                parameter = new OleDbParameter("@Comentario", OleDbType.Char);
                parameter.Value = Comentario;
                parameters[9] = parameter;
                parameter = new OleDbParameter("@IdTipoIden", OleDbType.Char);
                parameter.Value = IdTipoIden;
                parameters[10] = parameter;
                parameter = new OleDbParameter("@DV", OleDbType.Char);
                parameter.Value = DV;
                parameters[11] = parameter;
                parameter = new OleDbParameter("@IdArea", OleDbType.Char);
                parameter.Value = CodigoArea;
                parameters[12] = parameter;
                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("agregarRegistroOperacion", parameters);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable loadMaxIdRegistroOperacion()
        {
            DataTable dtIdRegistroOperacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtIdRegistroOperacion = cDataBase.ejecutarConsulta("SELECT MAX(IdRegistroOperacion) AS MaxId FROM [Proceso].[RegistroOperacion]");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtIdRegistroOperacion;
        }

        public DataTable LoadListTipoRegistro()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoRegistro,NombreTipoRegistro FROM Proceso.TipoRegistro ORDER BY 1");
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

        public DataTable LoadListEstado()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdEstadoOperacion,NombreEstado FROM Proceso.EstadoOperacion ORDER BY 1");
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

        public DataTable LoadCodigoAreas()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Codigo,NombreArea FROM Parametrizacion.Area WHERE Codigo IS NOT NULL ORDER BY NombreArea");
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

        public void agregarArchivo(String IdRegistroOperacion, String UrlArchivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Proceso.ArchivoRegistroOperacion (IdRegistroOperacion, NombreUsuario, FechaRegistro, UrlArchivo) VALUES (" + IdRegistroOperacion + ", '" + NombreUsuario + "', GETDATE(), N'" + UrlArchivo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void mtdAgregarArchivoPdf(string strIdRegistroOperacion,
            string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Proceso].[ArchivoRegistroOperacion]([IdRegistroOperacion], [NombreUsuario], [FechaRegistro], [UrlArchivo], [ArchivoPDF]) VALUES ({0}, N'{1}', GETDATE(), N'{2}', @PdfData)",
                    strIdRegistroOperacion, NombreUsuario, strUrlArchivo);

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
                strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [Proceso].[ArchivoRegistroOperacion] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);
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
    }
}