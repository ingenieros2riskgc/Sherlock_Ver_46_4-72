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
    public class cControl : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private Object thisLock = new Object();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;

        #region Load
        public DataTable loadInfoArchivoPlanAccion(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Archivos.IdArchivo, Riesgos.Archivos.NombreUsuario, Riesgos.Archivos.FechaRegistro, Riesgos.Archivos.UrlArchivo FROM Riesgos.Archivos INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Archivos.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Riesgos.Archivos.IdRegistro = " + IdRegistro + ") AND (Parametrizacion.ControlesUsuario.IdControlUsuario = 5)");
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

        public DataTable loadInfoComentarioPlanEvaluacion(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Comentarios.IdComentario, Riesgos.Comentarios.NombreUsuario, Riesgos.Comentarios.FechaRegistro, LTRIM(RTRIM(SUBSTRING(Riesgos.Comentarios.Comentario, 1, 20))) + '...' AS ComentarioCorto, Riesgos.Comentarios.Comentario FROM Riesgos.Comentarios INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Comentarios.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Parametrizacion.ControlesUsuario.IdControlUsuario = 5) AND (Riesgos.Comentarios.IdRegistro = " + IdRegistro + ")");
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

        public DataTable loadInfoComentarioControl(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Comentarios.IdComentario, Riesgos.Comentarios.NombreUsuario, Riesgos.Comentarios.FechaRegistro, LTRIM(RTRIM(SUBSTRING(Riesgos.Comentarios.Comentario, 1, 20))) + '...' AS ComentarioCorto, Riesgos.Comentarios.Comentario FROM Riesgos.Comentarios INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Comentarios.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Parametrizacion.ControlesUsuario.IdControlUsuario = 1) AND (Riesgos.Comentarios.IdRegistro = " + IdRegistro + ") ORDER BY Riesgos.Comentarios.IdComentario DESC");
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

        public DataTable loadCodigoArchivoControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdArchivo+1 AS NumRegistros FROM Riesgos.Archivos ORDER BY IdArchivo DESC");
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

        public DataTable loadInfoArchivoControl(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Archivos.IdArchivo, Riesgos.Archivos.NombreUsuario, Riesgos.Archivos.FechaRegistro, Riesgos.Archivos.UrlArchivo FROM Riesgos.Archivos INNER JOIN Parametrizacion.ControlesUsuario ON Riesgos.Archivos.IdControlUsuario = Parametrizacion.ControlesUsuario.IdControlUsuario WHERE (Riesgos.Archivos.IdRegistro = " + IdRegistro + ") AND (Parametrizacion.ControlesUsuario.IdControlUsuario = 1)");
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

        public DataTable loadCodigoControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdControl+1 AS NumRegistros FROM Riesgos.Control ORDER BY IdControl DESC");
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
        
        public string getLastCodigoControl()
        {
            DataTable dtInformacion = new DataTable();
            string codControl = string.Empty;
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT count(IdControl)+1 AS NumRegistros FROM Riesgos.Control");
                cDataBase.desconectar();

                codControl = dtInformacion.Rows[0]["NumRegistros"].ToString();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return codControl;
        }
        public DataTable loadInfoPorcentajeCalificarControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdPorcentajeCalificarControl, NombrePorcentajeCalificarControl, ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl");
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

        public DataTable loadInfoCalificacionControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdCalificacionControl, NombreEscala, ValorEscala, LimiteInferior, LimiteSuperior, PromedioInferior, PromedioSuperior, DesviacionProbabilidad, DesviacionImpacto, Color FROM Parametrizacion.CalificacionControl");
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

        public DataTable loadInfoControles(String CodigoControl, String NombreControl, String Responsable)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = "";
            try
            {
                if (CodigoControl != "")
                {
                    condicion = "WHERE (Riesgos.Control.CodigoControl = '" + CodigoControl + "') ";
                }
                if (NombreControl != "")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (Riesgos.Control.NombreControl LIKE '%" + NombreControl + "%') ";
                    }
                    else
                    {
                        condicion += "AND (Riesgos.Control.NombreControl LIKE '%" + NombreControl + "%') ";
                    }
                }
                if (Responsable != "")
                {
                    if (condicion.Trim() == "")
                    {
                        condicion = "WHERE (Riesgos.Control.Responsable = " + Responsable + ")";
                    }
                    else
                    {
                        condicion += "AND (Riesgos.Control.Responsable = " + Responsable + ")";
                    }
                }
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.Control.IdControl, Riesgos.Control.CodigoControl, Riesgos.Control.NombreControl, Riesgos.Control.DescripcionControl, Riesgos.Control.ObjetivoControl, Riesgos.Control.Responsable, Riesgos.Control.IdPeriodicidad, Riesgos.Control.IdTest, Parametrizacion.Test.NombreTest, Riesgos.Control.IdClaseControl, Riesgos.Control.IdTipoControl, Riesgos.Control.IdResponsableExperiencia, Riesgos.Control.IdDocumentacion, Riesgos.Control.IdResponsabilidad, Riesgos.Control.IdCalificacionControl, Riesgos.Control.FechaRegistro, Riesgos.Control.IdUsuario, Riesgos.Control.IdMitiga, Parametrizacion.JerarquiaOrganizacional.NombreHijo,Riesgos.Control.ResponsableEjecucion FROM Riesgos.Control INNER JOIN Parametrizacion.Test ON Riesgos.Control.IdTest = Parametrizacion.Test.IdTest LEFT JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Control.Responsable " + condicion);
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

        public DataTable LoadInfoControlesDefault(string IdUsuario)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            try
            {
                strConsulta = string.Format("SELECT Riesgos.Control.IdControl, Riesgos.Control.CodigoControl, Riesgos.Control.NombreControl, Riesgos.Control.DescripcionControl, Riesgos.Control.ObjetivoControl, Riesgos.Control.Responsable, Riesgos.Control.IdPeriodicidad, Riesgos.Control.IdTest, Parametrizacion.Test.NombreTest, Riesgos.Control.IdClaseControl, Riesgos.Control.IdTipoControl, Riesgos.Control.IdResponsableExperiencia, Riesgos.Control.IdDocumentacion, Riesgos.Control.IdResponsabilidad, Riesgos.Control.IdCalificacionControl, Riesgos.Control.FechaRegistro, Riesgos.Control.IdUsuario, Riesgos.Control.IdMitiga, Parametrizacion.JerarquiaOrganizacional.NombreHijo,Riesgos.Control.ResponsableEjecucion FROM Riesgos.Control INNER JOIN Parametrizacion.Test ON Riesgos.Control.IdTest = Parametrizacion.Test.IdTest INNER JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.Control.Responsable WHERE Riesgos.Control.Responsable = {0}", IdUsuario);
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

        #endregion Load

        #region DDL
        public DataTable loadDDLEstadoPlanEvaluacion()
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
                //throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadDDLTipoPruebaPlanEvaluacion()
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
                //throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadDDLResponsabilidad()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdResponsabilidad, NombreResponsabilidad FROM Parametrizacion.Responsabilidad");
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

        public DataTable loadDDLDocumentacion()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdDocumentacion, NombreDocumentacion FROM Parametrizacion.Documentacion");
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

        public DataTable loadDDLResponsableExperiencia()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdResponsableExperiencia, NombreResponsableExperiencia FROM Parametrizacion.ResponsableExperiencia");
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

        public DataTable loadDDLPeriodicidad()
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
                //throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadDDLControles()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdControl, NombreControl FROM Riesgos.Control ORDER BY NombreControl");
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

        public DataTable loadDDLClaseControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdClaseControl, NombreClaseControl FROM Parametrizacion.ClaseControl");
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

        public DataTable loadDDLTipoControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoControl, NombreTipoControl FROM Parametrizacion.TipoControl");
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

        public DataTable loadDDLTest()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTest, NombreTest FROM Parametrizacion.Test");
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

        public DataTable loadDDLMitiga()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdMitiga, NombreMitiga FROM Parametrizacion.MitigaControl");
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

        #region Metodos
        public DataTable valorMaxMinIntervalo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT (SELECT (((SELECT MAX(ValorClaseControl) FROM Parametrizacion.ClaseControl)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 1))+((SELECT MAX(ValorTipoControl) FROM Parametrizacion.TipoControl)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 2))+((SELECT MAX(ValorResponsableExperiencia) FROM Parametrizacion.ResponsableExperiencia)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 3))+((SELECT MAX(ValorDocumentacion) FROM Parametrizacion.Documentacion)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 4))+((SELECT MAX(ValorResponsabilidad) FROM Parametrizacion.Responsabilidad)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 5)))/100) AS Maximo, (SELECT (((SELECT TOP (1) ValorClaseControl FROM Parametrizacion.ClaseControl ORDER BY ValorClaseControl ASC)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 1))+((SELECT TOP (1) ValorTipoControl FROM Parametrizacion.TipoControl ORDER BY ValorTipoControl ASC)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 2))+((SELECT TOP (1) ValorResponsableExperiencia FROM Parametrizacion.ResponsableExperiencia ORDER BY ValorResponsableExperiencia)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 3))+((SELECT TOP (1) ValorDocumentacion FROM Parametrizacion.Documentacion ORDER BY ValorDocumentacion)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 4))+((SELECT TOP (1) ValorResponsabilidad FROM Parametrizacion.Responsabilidad ORDER BY ValorResponsabilidad)*(SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE IdPorcentajeCalificarControl = 5)))/100) AS Minimo");
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
        public DataTable variablesControl()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdVariableCalificacionControl],[DescripcionVariable] FROM[Parametrizacion].[VariableCalificacionControl] where Activo = 1");
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
        public DataTable valorMax_MinVariables(int IdVariable)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(string.Format("SELECT "
      + "ISNULL(MAX((PPCC.ValorPorcentajeCalificarControl * PCC.PasoCategoria) / 100),0) as Maximo,"
      + "ISNULL(MIN((PPCC.ValorPorcentajeCalificarControl * PCC.PasoCategoria) / 100),0) as Minimo "
        + "FROM [Parametrizacion].[VariablexCategoriaControl] PVCC "
  + "INNER JOIN Parametrizacion.VariableCalificacionControl as PVC on PVC.IdVariableCalificacionControl = PVCC.IdVariable "
  + "INNER JOIN Parametrizacion.CategoriaVariableControl as PCC on PCC.IdCategoriaVariableControl = PVCC.IdCategoria "
  + "INNER JOIN Parametrizacion.PorcentajeCalificarControl as PPCC on PPCC.NombrePorcentajeCalificarControl = PVC.DescripcionVariable "
  + "WHERE PVC.Activo = 1 and[IdVariable] = {0}", IdVariable));
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
        public void agregarComentarioPlanEvaluacion(String Comentario, String IdRegistro)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Comentarios (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, Comentario) VALUES (5, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + Comentario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public void agregarComentarioControl(String Comentario, String IdRegistro)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Comentarios (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, Comentario) VALUES (1, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + Comentario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public double valorResponsableExperiencia(String IdResponsableExperiencia)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorResponsableExperiencia FROM Parametrizacion.ResponsableExperiencia WHERE (IdResponsableExperiencia = " + IdResponsableExperiencia + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorResponsableExperiencia"].ToString().Trim());
        }

        public double valorDocumentacion(String IdDocumentacion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorDocumentacion FROM Parametrizacion.Documentacion WHERE (IdDocumentacion = " + IdDocumentacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorDocumentacion"].ToString().Trim());
        }

        public DataTable GetLastControl()
        {
            DataTable dtInformacion = new DataTable();
            //int id = 0;
            try
            {
                cDataBase.conectar();
                //dtInformacion = cDataBase.ejecutarConsulta("SELECT MAX(IdControl) AS LastControl FROM Riesgos.Control");//ORDER BY IdControl DESC
                dtInformacion = cDataBase.ejecutarConsulta("SELECT count(IdControl) AS LastControl FROM Riesgos.Control ");
                //id = Convert.ToInt32(dtInformacion.Rows[0]["LastControl"].ToString());
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

        public double valorResponsabilidad(String IdResponsabilidad)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorResponsabilidad FROM Parametrizacion.Responsabilidad WHERE (IdResponsabilidad = " + IdResponsabilidad + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorResponsabilidad"].ToString().Trim());
        }

        public double valorClaseControl(String IdClaseControl)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorClaseControl FROM Parametrizacion.ClaseControl WHERE (IdClaseControl = " + IdClaseControl + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorClaseControl"].ToString().Trim());
        }

        public double valorTipoControl(String IdTipoControl)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorTipoControl FROM Parametrizacion.TipoControl WHERE (IdTipoControl = " + IdTipoControl + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorTipoControl"].ToString().Trim());
        }

        public double valorDisenoControl(String IdDisenoControl)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorDisenoControl FROM Parametrizacion.DisenoControl WHERE (IdDisenoControl = " + IdDisenoControl + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorDisenoControl"].ToString().Trim());
        }

        public double valorAplicacionControl(String IdAplicacionControl)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorAplicacionControl FROM Parametrizacion.AplicacionControl WHERE (IdAplicacionControl = " + IdAplicacionControl + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorAplicacionControl"].ToString().Trim());
        }

        public double valorTest(String IdTest)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorTest FROM Parametrizacion.Test WHERE (IdTest = " + IdTest + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorTest"].ToString().Trim());
        }

        public double valorCostoBeneficio(String IdCostoBeneficio)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT ValorDivisor FROM Parametrizacion.CostoBeneficio WHERE (IdCostoBeneficio = " + IdCostoBeneficio + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return Convert.ToDouble(dtInformacion.Rows[0]["ValorDivisor"].ToString().Trim());
        }
        public void agregarControl(string CodigoControl, string NombreControl, string DescripcionControl,
            string ObjetivoControl, string Responsable, string IdPeriodicidad, string IdTest,
            string IdClaseControl, string IdTipoControl, string IdResponsableExperiencia,
            string IdDocumentacion, string IdResponsabilidad, string IdCalificacionControl,
            string IdMitiga, ref string strError, string strIdUsuario, string ResponsableEjecucion)
        {
            #region Variables
            string strConsultaModificacion = string.Empty, strCamposModificacion = string.Empty;
            string strConsultaInsertar = string.Empty, strCamposInsertar = string.Empty;
            string strIdControl = "(SELECT CASE ISNULL(MAX(IdControl),'') WHEN '' THEN 1 ELSE (SELECT MAX(CAST(SUBSTRING(CodigoControl, 2, 10) AS INT)) + 1 FROM Riesgos.Control WHERE CodigoControl LIKE 'C%') END FROM Riesgos.Control)",
                strCodigoControl = "(SELECT CASE ISNULL(MAX(IdControl),'') WHEN '' THEN 'C1' ELSE (SELECT 'C'+ CAST ((SELECT MAX(CAST(SUBSTRING(CodigoControl, 2, 10) AS INT)) + 1 FROM Riesgos.Control WHERE CodigoControl LIKE 'C%') AS NVARCHAR(50))) END FROM Riesgos.Control)";
            #endregion Variables

            try
            {
                #region CodControl
                string codControl = "C"+ getLastCodigoControl();
                #endregion CodControl
                #region Inserts
                strCamposModificacion = "([IdCodigoControl],[CodigoControl],[NombreControl],[IdResponsableControl],[IdPeriodicidad],[IdTest],[IdClaseControl],[IdTipoControl],[IdResponsableExperiencia],[IdDocumentacion],[IdResponsabilidad],[IdCalificacionControl],[IdMitiga],[JustificacionCambio],[IdUsuario],[FechaRegistroControl],[FechaModificacion])";
                strConsultaModificacion = string.Format("INSERT INTO [Riesgos].[DetalleModificacionControl] {0} VALUES ({1},{2},'{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},'{14}',{15},GETDATE(),GETDATE())",
                    strCamposModificacion, strIdControl, codControl, NombreControl, Responsable, IdPeriodicidad, IdTest, IdClaseControl, IdTipoControl,
                    IdResponsableExperiencia, IdDocumentacion, IdResponsabilidad, IdCalificacionControl, IdMitiga, "CREACION DEL CONTROL", strIdUsuario);

                strCamposInsertar = "(CodigoControl, NombreControl, DescripcionControl, ObjetivoControl, Responsable, IdPeriodicidad, IdTest, IdClaseControl, IdTipoControl, IdResponsableExperiencia, IdDocumentacion, IdResponsabilidad, IdCalificacionControl, FechaRegistro, IdUsuario, IdMitiga,ResponsableEjecucion)";
                strConsultaInsertar = string.Format("INSERT INTO Riesgos.Control {0} VALUES ({1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},GETDATE(),{14},{15},'{16}')", strCamposInsertar,
                    codControl, NombreControl, DescripcionControl, ObjetivoControl, Responsable, IdPeriodicidad,
                    IdTest, IdClaseControl, IdTipoControl, IdResponsableExperiencia, IdDocumentacion, IdResponsabilidad, IdCalificacionControl, strIdUsuario, IdMitiga, ResponsableEjecucion);
                #endregion Inserts

                lock (thisLock)
                {
                    cDataBase.conectar();
                    cDataBase.ejecutarQuery(strConsultaModificacion + "; " + strConsultaInsertar);
                    cDataBase.desconectar();
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message + ", " + ex.StackTrace;
                cDataBase.desconectar();
                cError.errorMessage(strError);
                //throw new Exception(ex.Message);
            }
        }

        public int InsertControl(cControlEntity controlEntity)
        {
            #region Variables
            string strConsultaModificacion = string.Empty, strCamposModificacion = string.Empty;
            string strConsultaInsertar = string.Empty, strCamposInsertar = string.Empty;
            string strIdControl = "(SELECT CASE ISNULL(MAX(IdControl),'') WHEN '' THEN 1 ELSE (SELECT MAX(CAST(SUBSTRING(CodigoControl, 2, 10) AS INT)) + 1 FROM Riesgos.Control WHERE CodigoControl LIKE 'C%') END FROM Riesgos.Control)",
                strCodigoControl = "(SELECT CASE ISNULL(MAX(IdControl),'') WHEN '' THEN 'C1' ELSE (SELECT 'C'+ CAST ((SELECT MAX(CAST(SUBSTRING(CodigoControl, 2, 10) AS INT)) + 1 FROM Riesgos.Control WHERE CodigoControl LIKE 'C%') AS NVARCHAR(50))) END FROM Riesgos.Control)";
            #endregion Variables
            //controlEntity.CodigoControl = strCodigoControl;
            controlEntity.CodigoControl = "C" + getLastCodigoControl();
            try
            {
                #region Inserts

                // Se crea la lista de parametros para insertar el control

                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdControl },
                    new SqlParameter() { ParameterName = "@NombreControl", SqlDbType = SqlDbType.VarChar, Value =  controlEntity.NombreControl },
                    new SqlParameter() { ParameterName = "@DescripcionControl", SqlDbType = SqlDbType.VarChar, Value =  controlEntity.DescripcionControl },
                    new SqlParameter() { ParameterName = "@ObjetivoControl", SqlDbType = SqlDbType.VarChar, Value =  controlEntity.ObjetivoControl },
                    new SqlParameter() { ParameterName = "@Responsable", SqlDbType = SqlDbType.Int, Value =  controlEntity.Responsable },
                    new SqlParameter() { ParameterName = "@IdPeriodicidad", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdPeriodicidad },
                    new SqlParameter() { ParameterName = "@IdTest", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdTest },
                    new SqlParameter() { ParameterName = "@IdClaseControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdClaseControl },
                    new SqlParameter() { ParameterName = "@IdTipoControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdTipoControl },
                    new SqlParameter() { ParameterName = "@IdResponsableExperiencia", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdResponsableExperiencia },
                    new SqlParameter() { ParameterName = "@IdDocumentacion", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdDocumentacion },
                    new SqlParameter() { ParameterName = "@IdResponsabilidad", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdResponsabilidad },
                    new SqlParameter() { ParameterName = "@IdCalificacionControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdCalificacionControl },
                    new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdUsuario },
                    new SqlParameter() { ParameterName = "@IdMitiga", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdMitiga },
                    new SqlParameter() { ParameterName = "@ResponsableEjecucion", SqlDbType = SqlDbType.VarChar, Value =  controlEntity.ResponsableEjecucion },
                    new SqlParameter() { ParameterName = "@Variable6", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable6 },
                    new SqlParameter() { ParameterName = "@Variable7", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable7 },
                    new SqlParameter() { ParameterName = "@Variable8", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable8 },
                    new SqlParameter() { ParameterName = "@Variable9", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable9 },
                    new SqlParameter() { ParameterName = "@Variable10", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable10 },
                    new SqlParameter() { ParameterName = "@Variable11", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable11 },
                    new SqlParameter() { ParameterName = "@Variable12", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable12 },
                    new SqlParameter() { ParameterName = "@Variable13", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable13 },
                    new SqlParameter() { ParameterName = "@Variable14", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable14 },
                    new SqlParameter() { ParameterName = "@Variable15", SqlDbType = SqlDbType.Int, Value =  controlEntity.Variable15 },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                int scope = cDataBase.EjecutarSPParametrosReturnInteger("[Riesgos].[ControlInsertarActualizar]", parametros);
                return scope;
                #endregion Inserts
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }
        }

        public void InsertJustificacion(cControlEntity controlEntity)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdCodigoControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdControl },
                    new SqlParameter() { ParameterName = "@NombreControl", SqlDbType = SqlDbType.VarChar, Value =  controlEntity.NombreControl },
                    new SqlParameter() { ParameterName = "@IdResponsableControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.Responsable },
                    new SqlParameter() { ParameterName = "@IdPeriodicidad", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdPeriodicidad },
                    new SqlParameter() { ParameterName = "@IdTest", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdTest },
                    new SqlParameter() { ParameterName = "@IdClaseControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdClaseControl },
                    new SqlParameter() { ParameterName = "@IdTipoControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdTipoControl },
                    new SqlParameter() { ParameterName = "@IdResponsableExperiencia", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdResponsableExperiencia },
                    new SqlParameter() { ParameterName = "@IdDocumentacion", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdDocumentacion },
                    new SqlParameter() { ParameterName = "@IdResponsabilidad", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdResponsabilidad },
                    new SqlParameter() { ParameterName = "@IdCalificacionControl", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdCalificacionControl },
                    new SqlParameter() { ParameterName = "@IdMitiga", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdMitiga },
                    new SqlParameter() { ParameterName = "@JustificacionCambio", SqlDbType = SqlDbType.VarChar, Value =  controlEntity.JustificacionCambios },
                    new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdUsuario },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlJustificacionCambiosInsertar]", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertComentario(cControlEntity controlEntity)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdControlUsuario", SqlDbType = SqlDbType.Int, Value =  1 },
                    new SqlParameter() { ParameterName = "@IdRegistro", SqlDbType = SqlDbType.Int, Value =  controlEntity.IdControl },
                    new SqlParameter() { ParameterName = "@NombreUsuario", SqlDbType = SqlDbType.VarChar, Value =  controlEntity.NombreUsuario },
                    new SqlParameter() { ParameterName = "@Comentario", SqlDbType = SqlDbType.VarChar, Value =  controlEntity.JustificacionCambios },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlComentariosInsertar]", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SeleccionarUltimoControl()
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                int result = cDataBase.EjecutarSPParametrosReturnInteger("[Riesgos].[ControlUltimoSeleccionar]", parametros);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ValidarExistenciaControl(int idControl)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdControl", SqlDbType = SqlDbType.Int, Value =  idControl},
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                int result = cDataBase.EjecutarSPParametrosReturnInteger("[Riesgos].[ControlValidarExistencia]", parametros);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> SeleccionarRiesgosAsociadosControl(int IdControl)
        {
            try
            {
                List<string> RiesgosAsociados = new List<string>();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdControl", SqlDbType = SqlDbType.Int, Value =  IdControl }
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlSeleccionarRiesgoAsociado]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        RiesgosAsociados.Add(Row["IdRiesgo"].ToString());
                    }
                }
                return RiesgosAsociados;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CalcularRiesgoResidual(string RiesgosAsociados)
        {
            try
            {

                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@RiesgosAsociados", SqlDbType = SqlDbType.VarChar, Value =  RiesgosAsociados }
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlSeleccionarInformacionRiesgosAsociados]", parametros);
                // Se itera por cada riesgo al que esté asociado el control
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        calificacionResidual(Row);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SeleccionarRiesgoCalificacionControl(int IdRiesgo)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdRiesgo", SqlDbType = SqlDbType.VarChar, Value =  IdRiesgo }
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlSeleccionarRiesgoCalificacionControl]", parametros);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SeleccionarRiesgo(int? IdRiesgo)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdRiesgo", SqlDbType = SqlDbType.VarChar, Value =  IdRiesgo }
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[RiesgoSeleccionarCausas]", parametros);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void calificacionResidual(DataRow Row)
        {
            try
            {
                double valorProbabilidad = Convert.ToDouble(Row["IdProbabilidad"].ToString());
                double valorImpacto = Convert.ToDouble(Row["IdImpacto"].ToString());
                double valorFinalProbabilidad = 0;
                double valorFinalImpacto = 0;
                double DesviacionProbabilidad = 0;
                double DesviacionImpacto = 0;
                int CantidadProbabilidad = 0;
                int CantidadImpacto = 0;

                // Consulta la vista vwRiesgosCalificacionControl
                DataTable dt = SeleccionarRiesgoCalificacionControl(Convert.ToInt32(Row["IdRiesgo"].ToString()));

                // Se itera sobre el resultado de la consulta si trae datos
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (dt.Rows[i]["IdMitiga"].ToString().Trim())
                        {
                            case "1":
                                CantidadProbabilidad += 1;
                                DesviacionProbabilidad = valorProbabilidad - Convert.ToDouble(dt.Rows[i]["DesviacionProbabilidad"].ToString().Trim());
                                if (DesviacionProbabilidad <= 0)
                                {
                                    DesviacionProbabilidad = 1;
                                }
                                valorFinalProbabilidad += DesviacionProbabilidad;
                                break;
                            case "2":
                                CantidadImpacto += 1;
                                DesviacionImpacto = valorImpacto - Convert.ToDouble(dt.Rows[i]["DesviacionImpacto"].ToString().Trim());
                                if (DesviacionImpacto <= 0)
                                {
                                    DesviacionImpacto = 1;
                                }
                                valorFinalImpacto += DesviacionImpacto;
                                break;
                            case "3":
                                CantidadProbabilidad += 1;
                                CantidadImpacto += 1;
                                DesviacionProbabilidad = valorProbabilidad - Convert.ToDouble(dt.Rows[i]["DesviacionProbabilidad"].ToString().Trim());
                                DesviacionImpacto = valorImpacto - Convert.ToDouble(dt.Rows[i]["DesviacionImpacto"].ToString().Trim());
                                if (DesviacionProbabilidad <= 0)
                                {
                                    DesviacionProbabilidad = 1;
                                }
                                if (DesviacionImpacto <= 0)
                                {
                                    DesviacionImpacto = 1;
                                }
                                valorFinalProbabilidad += DesviacionProbabilidad;
                                valorFinalImpacto += DesviacionImpacto;
                                break;
                        }
                    }
                    if (CantidadProbabilidad > 0)
                    {
                        valorProbabilidad = (valorFinalProbabilidad / CantidadProbabilidad);
                        if (valorProbabilidad == 1.5)
                        {
                            valorProbabilidad = 1.0;
                        }
                        else if (valorProbabilidad == 2.5)
                        {
                            valorProbabilidad = 2.0;
                        }
                        else if (valorProbabilidad == 3.5)
                        {
                            valorProbabilidad = 3.0;
                        }
                        else if (valorProbabilidad == 4.5)
                        {
                            valorProbabilidad = 4.0;
                        }
                        else if (valorProbabilidad == 5.5)
                        {
                            valorProbabilidad = 5.0;
                        }
                        else
                        {
                            valorProbabilidad = Math.Round(valorFinalProbabilidad / CantidadProbabilidad);
                        }
                    }
                    if (CantidadImpacto > 0)
                    {
                        valorImpacto = (valorFinalImpacto / CantidadImpacto);

                        if (valorImpacto == 1.5)
                        {
                            valorImpacto = 1.0;
                        }
                        else if (valorImpacto == 2.5)
                        {
                            valorImpacto = 2.0;
                        }
                        else if (valorImpacto == 3.5)
                        {
                            valorImpacto = 3.0;
                        }
                        else if (valorImpacto == 4.5)
                        {
                            valorImpacto = 4.0;
                        }
                        else if (valorImpacto == 5.5)
                        {
                            valorImpacto = 5.0;
                        }
                        else
                        {
                            valorImpacto = Math.Round(valorFinalImpacto / CantidadImpacto);
                        }
                    }
                    cRiesgo Riesgo = new cRiesgo();
                    // Actualiza la calificación residual del riesgo
                    Riesgo.actualizarRiesgoResidual(valorProbabilidad.ToString().Trim(), valorImpacto.ToString().Trim(), Row["IdRiesgo"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RegistrarLimites(cLimite limite)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@ExcelenteLimiteInferior", SqlDbType = SqlDbType.Float, Value =  limite.ExcelenteLimiteInferior },
                    new SqlParameter() { ParameterName = "@ExcelenteLimiteSuperior", SqlDbType = SqlDbType.Float, Value =  limite.ExcelenteLimiteSuperior },
                    new SqlParameter() { ParameterName = "@BuenoLimiteInferior", SqlDbType = SqlDbType.Float, Value =  limite.BuenoLimiteInferior },
                    new SqlParameter() { ParameterName = "@BuenoLimiteSuperior", SqlDbType = SqlDbType.Float, Value =  limite.BuenoLimiteSuperior },
                    new SqlParameter() { ParameterName = "@RegularLimiteInferior", SqlDbType = SqlDbType.Float, Value =  limite.RegularLimiteInferior },
                    new SqlParameter() { ParameterName = "@RegularLimiteSuperior", SqlDbType = SqlDbType.Float, Value =  limite.RegularLimiteSuperior },
                    new SqlParameter() { ParameterName = "@DeficienteLimiteInferior", SqlDbType = SqlDbType.Float, Value =  limite.DeficienteLimiteInferior },
                    new SqlParameter() { ParameterName = "@DeficienteLimiteSuperior", SqlDbType = SqlDbType.Float, Value =  limite.DeficienteLimiteSuperior },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                int result = cDataBase.EjecutarSPParametrosReturnInteger("[Riesgos].[CalificarControlActualizarLimites]", parametros);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public cLimite SeleccionarLimites()
        {
            cLimite Limites = new cLimite();
            List<SqlParameter> parametros = new List<SqlParameter>();
            DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[CalificarControlSeleccionarLimites]", parametros);
            if (dt != null && dt.Rows.Count > 0)
            {
                Limites.ExcelenteLimiteInferior = dt.Rows[0]["ExcelenteLimiteInferior"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["ExcelenteLimiteInferior"]);
                Limites.ExcelenteLimiteSuperior = dt.Rows[0]["ExcelenteLimiteSuperior"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["ExcelenteLimiteSuperior"]);
                Limites.BuenoLimiteInferior = dt.Rows[0]["BuenoLimiteInferior"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["BuenoLimiteInferior"]);
                Limites.BuenoLimiteSuperior = dt.Rows[0]["BuenoLimiteSuperior"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["BuenoLimiteSuperior"]);
                Limites.RegularLimiteInferior = dt.Rows[0]["RegularLimiteInferior"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["RegularLimiteInferior"]);
                Limites.RegularLimiteSuperior = dt.Rows[0]["RegularLimiteSuperior"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["RegularLimiteSuperior"]);
                Limites.DeficienteLimiteInferior = dt.Rows[0]["DeficienteLimiteInferior"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["DeficienteLimiteInferior"]);
                Limites.DeficienteLimiteSuperior = dt.Rows[0]["DeficienteLimiteSuperior"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["DeficienteLimiteSuperior"]);
            }
            return Limites;
        }

        public void modificarControl(String CodigoControl, String NombreControl, String DescripcionControl, String ObjetivoControl, String Responsable,
            String IdPeriodicidad, String IdTest,
            String IdControl, String IdMitiga, string strJustificacion, string ResponsableEjecucion)
        {
            #region Variables
            string strConsultaUpdate = string.Empty;
            string strConsultaModificacion = string.Empty, strCamposModificacion = string.Empty;
            #endregion Variables

            try
            {
                strCamposModificacion = "([IdCodigoControl],[CodigoControl],[NombreControl],[IdResponsableControl],[IdPeriodicidad],[IdTest],[IdMitiga],[JustificacionCambio],[IdUsuario],[FechaRegistroControl],[FechaModificacion])";
                strConsultaModificacion = string.Format("INSERT INTO [Riesgos].[DetalleModificacionControl] {0} VALUES ({1},'{2}','{3}',{4},{5},{6},{7},'{8}','{9}',GETDATE(),GETDATE())", strCamposModificacion,
                    IdControl, CodigoControl,
                    NombreControl, Responsable, IdPeriodicidad, IdTest, IdMitiga, strJustificacion, IdUsuario);
                strConsultaUpdate = string.Format("UPDATE Riesgos.Control SET CodigoControl = N'{0}', NombreControl = '{1}', DescripcionControl = '{2}', ObjetivoControl = '{3}', Responsable = {4}, IdPeriodicidad = {5}, IdTest = {6}, IdMitiga = {7}, ResponsableEjecucion = '{8}' WHERE (IdControl = {9})",
                    CodigoControl, NombreControl, DescripcionControl, ObjetivoControl, Responsable, IdPeriodicidad, IdTest, IdMitiga, ResponsableEjecucion, IdControl);
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsultaModificacion + "; " + strConsultaUpdate);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public DataTable SeleccionarCategoriasControl(int IdControl)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdControl", SqlDbType = SqlDbType.Int, Value =  IdControl },
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlSeleccionarCategorias]", parametros);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cDataBase.desconectar();
            }
        }

        public clsDTOCategoriasVariableControl SeleccionarCategorias(int? Idcategoria)
        {
            clsDTOCategoriasVariableControl obj = new clsDTOCategoriasVariableControl();
            List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdCategoria", SqlDbType = SqlDbType.Int, Value =  Idcategoria },
                };
            DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[CategoriaCalificacionSeleccionar]", parametros);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow Row in dt.Rows)
                {
                    obj.intPesoCategoria = Convert.ToInt32(Row["PasoCategoria"].ToString());
                    obj.intIdCategoriaVariableControl = Convert.ToInt32(Row["IdCategoriaVariableControl"].ToString());
                    obj.IdVariable = Convert.ToInt32(Row["IdVariable"].ToString());
                }

            }
            return obj;
        }

        public List<clsDTOVariableCalificacionControl> SeleccionarVariables()
        {
            List<clsDTOVariableCalificacionControl> obj = new List<clsDTOVariableCalificacionControl>();
            List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "IdVariable", SqlDbType = SqlDbType.Int, Value =  0 },
                };
            DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[VariableCalificacionSeleccionar]", parametros);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow Row in dt.Rows)
                {
                    obj.Add(new clsDTOVariableCalificacionControl()
                    {
                        FlPesoVariable = Convert.ToDouble(Row["PesoVariable"].ToString()),
                        strDescripcionVariable = Row["DescripcionVariable"].ToString(),
                        intIdCalificacionControl = Convert.ToInt32(Row["IdVariableCalificacionControl"].ToString())
                    });
                }

            }
            return obj;
        }

        public DataTable ReporteControles()
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlReporteControles]", parametros);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cDataBase.desconectar();
            }
        }

        public int SeleccionaCalificacion(int IdControl)
        {
            try
            {
                int calificacion = 0;
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdControl", SqlDbType = SqlDbType.Int, Value =  IdControl },
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[ControlSeleccionarCalificacion]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    calificacion = Convert.ToInt32(dt.Rows[0]["IdCalificacionControl"].ToString());
                }
                return calificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void borrarControl(String IdControl)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Riesgos.Control WHERE (IdControl = " + IdControl + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        #region Plan de Evaluacion
        public void actualizarPlanEvaluacion(String IdPlanEvaluacion, String Responsable, String FechaInicio, String FechaProyectadaFin, String FechaRealCierre, String IdTipoPruebaPlanEvaluacion, String DescripcionEvaluacion, String Recursos, String Resultados, String IdEstadoPlanEvaluacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Riesgos.PlanesEvaluacion SET Responsable = " + Responsable + ", FechaInicio = CONVERT(datetime, '" + FechaInicio + "',120), FechaProyectadaFin = CONVERT(datetime, '" + FechaProyectadaFin + "',120), FechaRealCierre = CONVERT(datetime, '" + FechaRealCierre + "',120), IdTipoPruebaPlanEvaluacion = " + IdTipoPruebaPlanEvaluacion + ", DescripcionEvaluacion = N'" + DescripcionEvaluacion + "', Recursos = N'" + Recursos + "', Resultados = N'" + Resultados + "', IdEstadoPlanEvaluacion = " + IdEstadoPlanEvaluacion + " WHERE (IdPlanEvaluacion = " + IdPlanEvaluacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void agregarPlanEvaluacion(String IdRegistro, String Responsable, String FechaInicio, String FechaProyectadaFin, String IdTipoPruebaPlanEvaluacion, String DescripcionEvaluacion, String Recursos, String IdEstadoPlanEvaluacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.PlanesEvaluacion (IdControlUsuario, IdRegistro, Responsable, FechaInicio, FechaProyectadaFin, IdTipoPruebaPlanEvaluacion, DescripcionEvaluacion, Recursos, IdEstadoPlanEvaluacion) VALUES (1, " + IdRegistro + ", " + Responsable + ", CONVERT(datetime, '" + FechaInicio + "',120), CONVERT(datetime, '" + FechaProyectadaFin + "',120), " + IdTipoPruebaPlanEvaluacion + ", N'" + DescripcionEvaluacion + "', N'" + Recursos + "', " + IdEstadoPlanEvaluacion + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable loadInfoPlanEvaluacion(String IdRegistro)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Riesgos.PlanesEvaluacion.IdPlanEvaluacion, Riesgos.PlanesEvaluacion.Responsable, REPLACE(CONVERT(varchar, Riesgos.PlanesEvaluacion.FechaInicio, 102),'.','-') AS FechaInicio, REPLACE(CONVERT(varchar, Riesgos.PlanesEvaluacion.FechaProyectadaFin, 102),'.','-') AS FechaProyectadaFin, REPLACE(ISNULL(CONVERT(varchar, Riesgos.PlanesEvaluacion.FechaRealCierre, 102), ''),'.','-') AS FechaRealCierre, Riesgos.PlanesEvaluacion.IdTipoPruebaPlanEvaluacion, Parametrizacion.TipoPruebaPlanEvaluacion.NombreTipoPruebaPlanEvaluacion, Riesgos.PlanesEvaluacion.DescripcionEvaluacion, Riesgos.PlanesEvaluacion.Recursos,  ISNULL(Riesgos.PlanesEvaluacion.Resultados, '') AS Resultados, Riesgos.PlanesEvaluacion.IdEstadoPlanEvaluacion, Parametrizacion.EstadoPlanEvaluacion.NombreEstadoPlanEvaluacion, Parametrizacion.JerarquiaOrganizacional.NombreHijo FROM Riesgos.PlanesEvaluacion INNER JOIN Parametrizacion.TipoPruebaPlanEvaluacion ON Riesgos.PlanesEvaluacion.IdTipoPruebaPlanEvaluacion = Parametrizacion.TipoPruebaPlanEvaluacion.IdTipoPruebaPlanEvaluacion INNER JOIN Parametrizacion.EstadoPlanEvaluacion ON Riesgos.PlanesEvaluacion.IdEstadoPlanEvaluacion = Parametrizacion.EstadoPlanEvaluacion.IdEstadoPlanEvaluacion INNER JOIN Parametrizacion.JerarquiaOrganizacional ON Parametrizacion.JerarquiaOrganizacional.idHijo = Riesgos.PlanesEvaluacion.Responsable WHERE (Riesgos.PlanesEvaluacion.IdControlUsuario = 1) AND (Riesgos.PlanesEvaluacion.IdRegistro = " + IdRegistro + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }


        public DataTable mtdAgregarPlanEvaluacion(String IdRegistro, String Responsable, String FechaInicio, String FechaProyectadaFin, String IdTipoPruebaPlanEvaluacion, String DescripcionEvaluacion, String Recursos, String IdEstadoPlanEvaluacion)
        {
            #region Variables
            string strConsulta = string.Empty, strValues = string.Empty,
                strConsultaRetorno = string.Empty, strConsultaFinal = string.Empty;
            DataTable dtInfo = new DataTable();
            #endregion Variables

            try
            {
                #region Consulta
                strConsulta = "INSERT INTO Riesgos.PlanesEvaluacion (IdControlUsuario, IdRegistro, Responsable, FechaInicio, FechaProyectadaFin, IdTipoPruebaPlanEvaluacion, DescripcionEvaluacion, Recursos, IdEstadoPlanEvaluacion)";
                strValues = string.Format("VALUES (1, {0}, {1}, CONVERT(datetime, '{2}', 120), CONVERT(datetime, '{3}', 120), {4},  N'{5}', N'{6}', {7})",
                    IdRegistro, Responsable, FechaInicio, FechaProyectadaFin, IdTipoPruebaPlanEvaluacion, DescripcionEvaluacion, Recursos, IdEstadoPlanEvaluacion);
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


        #endregion Plan de Evaluacion

        #region PDFs

        public void agregarArchivoPlanEvaluacion(String IdRegistro, String UrlArchivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Archivos (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, UrlArchivo) VALUES (5, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + UrlArchivo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public void agregarArchivoControl(String IdRegistro, String UrlArchivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Riesgos.Archivos (IdControlUsuario, IdRegistro, NombreUsuario, FechaRegistro, UrlArchivo) VALUES (1, " + IdRegistro + ", '" + NombreUsuario + "', GETDATE(), N'" + UrlArchivo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public void mtdAgregarPdf(string strIdControl, string strIdRegistro, string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO Riesgos.Archivos ([IdControlUsuario], [IdRegistro], [NombreUsuario], [FechaRegistro], [UrlArchivo], [ArchivoPDF]) VALUES ({3}, {0}, '{1}', GETDATE(),N'{2}', @PdfData)",
                    strIdRegistro, NombreUsuario, strUrlArchivo, strIdControl);

                cDataBase.mtdConectarSql();
                cDataBase.mtdEjecutarConsultaSQL(strConsulta, bPdfData);
                cDataBase.mtdDesconectarSql();
            }
            catch (Exception ex)
            {
                cDataBase.mtdDesconectarSql();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public byte[] mtdDescargarPdf(string strNombreArchivo)
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
                //throw new Exception(ex.Message);
            }

            return bInfo;
        }
        #endregion PDFs

        public string NombreJerarquia(string IdHijo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                //Heber Jessid Correal 09/05/2017 Se cambia el nombre del responsable por el nombre del cargo. Claxon 132167  
                dtInformacion = cDataBase.ejecutarConsulta("SELECT 'JO-'+CargoResponsable+'| ' 'NombreResponsable' FROM Parametrizacion.DetalleJerarquiaOrg WHERE idHijo = '" + IdHijo + "'");
                cDataBase.desconectar();
                //Heber Jessid Correal 05/04/2017 Se controla la respuesta del metodo retornando una cadena vacia si no existen registros.
                if (dtInformacion.Rows.Count > 0)
                    return dtInformacion.Rows[0]["NombreResponsable"].ToString().Trim();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public string NombreGrupoTrabajo(string IdGrupoTrabajo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT 'GT-'+Nombre+'| ' 'Nombre' FROM Riesgos.GruposTrabajo WHERE IdGrupoTrabajo = '" + IdGrupoTrabajo + "'");
                cDataBase.desconectar();
                //Heber Jessid Correal 05/04/2017 Se controla la respuesta del metodo retornando una cadena vacia si no existen registros.
                if (dtInformacion.Rows.Count > 0)
                    return dtInformacion.Rows[0]["Nombre"].ToString().Replace('|', ' ').Trim();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public string CorreoJerarquia(string IdHijo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT CorreoResponsable+';' 'CorreoResponsable'  FROM Parametrizacion.DetalleJerarquiaOrg WHERE idHijo = '" + IdHijo + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0]["CorreoResponsable"].ToString().Trim();
        }

        public string CorreoGrupoTrabajo(string IdGrupoTrabajo)
        {
            DataTable dtInformacion = new DataTable();
            string correos = string.Empty;
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Correo+';' 'CorreoResponsable' FROM Riesgos.GruposTrabajoRescurso WHERE Estado = 1 AND IdGrupoTrabajo = '" + IdGrupoTrabajo + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            for (int rows = 0; rows < dtInformacion.Rows.Count; rows++)
            {
                correos += dtInformacion.Rows[rows]["CorreoResponsable"].ToString().Trim();
            }
            return correos;
            //return dtInformacion.Rows[0]["CorreoResponsable"].ToString().Trim();
        }

        #endregion metodos

    }
}