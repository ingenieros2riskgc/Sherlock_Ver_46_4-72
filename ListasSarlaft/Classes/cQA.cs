using System;
using System.Collections.Generic;
using System.Web;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using DataSets = System.Data;
using clsDatos;
using System.Linq;


namespace ListasSarlaft.Classes
{
    public class cQA : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();

        #region Ventana de registro de requerimientos
        /// 
        ////// Inicio de consultas para registro de requerimientos 
        /// 

        public DataTable loadEvidencia()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) idREGREQ AS NumRegistros FROM QA.RegistroRequerimiento ORDER BY idREGREQ DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadArchivoEvidencia(String StrId)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdEvidencia+1 AS NumRegistros FROM QA.EvidenciaAdjunto ORDER BY IdEvidencia DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public void mtdAgregarEvidencia(string StrId, string strDescripcion, string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [QA].[EvidenciaAdjunto] ([idREGREQ], [UrlArchivo], [Descripcion], [FechaRegistro], [IdUsuario], " +
                    "[ArchivoPDF]) VALUES ({0}, N'{1}', N'{2}', GETDATE(), {3}, @PdfData)",
                    StrId, strUrlArchivo, strDescripcion, IdUsuario.ToString().Trim());

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

        public DataTable NuevoId()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("SELECT TOP (1) idREGREQ+1 AS NumRegistros FROM QA.RegistroRequerimiento ORDER BY IdREGREQ DESC");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ConsulIdEvidencia()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("SELECT TOP (1) idREGREQ AS NumRegistros FROM QA.EvidenciaAdjunto ORDER BY IdEvidencia DESC");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        ///
        ////// Fin de consultas para registro de requerimientos
        ///
        #endregion

        #region Ventana de gestión de grupos y usuarios
        ///
        ////// Inicio de consultas para gestión de grupos y usuarios
        ///

        public DataTable ConsultaIntegrantes()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select idGrupoSoporte from QA.parametrizacionGruposSoporte");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ConsultaEliminar()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select IdUsuarioSoporte from QA.parametrizacionUsuariosSoporte");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable Consulta()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select IdUsuarioSoporte, NombreUsuarioSoporte, CorreUsuarioSoporte, UsuarioSoporte, " +
                    "idGrupoSoporte from QA.parametrizacionUsuariosSoporte");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ConsultaUsers(string strId)
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsultas("select IdUsuarioSoporte, NombreUsuarioSoporte, CorreUsuarioSoporte, UsuarioSoporte, " +
                    "idGrupoSoporte from QA.parametrizacionUsuariosSoporte where idGrupoSoporte = " + strId);
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ConsultaGrupos()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select idGrupoSoporte, NombreGrupoSoporte from QA.parametrizacionGruposSoporte");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        ///
        ////// Fin de consultar para gestión de grupos y usuarios
        ///
        #endregion

        #region
        ///
        ////// Inicio de consultas para gestión de grupos y usuarios
        ///

        public DataTable ConsultaGestionRequerimientos()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select idREGREQ, Usuario, Empresa, NumeroREQ, FechaCreacionREGREQ, TipoFalla, " +
                    "DetalleTipoFalla, Descripcion, Ruta from QA.RegistroRequerimiento order by idREGREQ DESC");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        ///
        ////// Inicio de consultas para gestión de grupos y usuarios
        ///
        #endregion

        #region Ventana de gestión de requerimientos
        ///
        ////// Incio de consultas para gestión de requerimientos
        ///
        public DataTable ConsultaEvidencias(string strIdEvidencia)
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsultaEvidencia("select IdEvidencia, URLArchivo, Descripcion, FechaRegistro, IdUsuario " +
                    "from QA.EvidenciaAdjunto where idREGREQ = " + strIdEvidencia);
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ConsultaComentario()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsultaEvidencia("select IdComentario, URLArchivo, Descripcion, FechaRegistro, IdUsuario " +
                    "from QA.ComentarioAdjunto");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ConsulIdComentario()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("SELECT TOP (1) idGESREQ AS NumRegistros FROM QA.ComentarioAdjunto ORDER BY IdComentario DESC");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadComentario()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) idGESREQ AS NumRegistros FROM QA.GestionRequerimiento ORDER BY idGESREQ DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public void mtdAgregarComentario(string StrId, string strDescripcion, string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                //strConsulta = string.Format("INSERT INTO [QA].[ComentarioAdjunto] ([idGESREQ], [UrlArchivo], [Descripcion], [FechaRegistro], " +
                //    "[IdUsuario], [ArchivoPDF]) VALUES ({0}, N'{1}', N'{2}', GETDATE(), {3}, @PdfData)",
                //    StrId, strUrlArchivo, strDescripcion, IdUsuario.ToString().Trim());

                strConsulta = string.Format("INSERT INTO [QA].[EvidenciaAdjunto] ([idREGREQ], [UrlArchivo], [Descripcion], [FechaRegistro], [IdUsuario], " +
                   "[ArchivoPDF]) VALUES ({0}, N'{1}', N'{2}', GETDATE(), {3}, @PdfData)",
                   StrId, strUrlArchivo, strDescripcion, IdUsuario.ToString().Trim());

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

        public DataTable ConsultarNombre(string valor)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) Estado AS NumRegistros FROM QA.GestionRequerimiento where " +
                    "NumeroREQ = '" + valor  + "' ORDER BY idGESREQ DESC ");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        ///
        ////// Fin de consultas para gestión de requerimientos
        ///
        #endregion

        #region Ventana de reporte de estado de requerimientos
        /// 
        ////// Inicio de consultas para registro de requerimientos 
        /// 
        public DataTable ConsultaReporte(string strEstado, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("select idGESREQ, Empresa, Usuario, NumeroREQ, FechaCreacionGESREQ, TipoFalla, GrupoAsignado, " +
                    "Encargado, Estado, Criticidad, FechaVencimientoGESREQ from QA.GestionRequerimiento where Estado = '" + strEstado + "' " +
                    "order by idGESREQ DESC");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        ///
        ////// Fin de consultas para registro de requerimientos
        ///

        public DataTable mtdConsultaGestion(string strNumeroREQ)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [idGESREQ], [NumeroREQ], [GrupoAsignado], [Encargado], [Estado], [Criticidad], " +
                    "[FechaVencimientoGESREQ], [Comentario] FROM [QA].[GestionRequerimiento] WHERE NumeroREQ = '" + strNumeroREQ + "' Order" +
                    " by idGESREQ DESC");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception)
            {
                
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;

        }

        public DataTable loadNombreGrupo()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select NombreGrupoSoporte from QA.parametrizacionGruposSoporte");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadIntegrante()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select NombreUsuarioSoporte from QA.parametrizacionUsuariosSoporte");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadIdGrupo(string ddlEncargadoSeleccionado)
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select idGrupoSoporte from QA.parametrizacionGruposSoporte where NombreGrupoSoporte = '" + ddlEncargadoSeleccionado + "'");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadNombreResponsable(string ddlEncargadoSeleccionado)
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select NombreUsuarioSoporte from QA.parametrizacionUsuariosSoporte where NombreUsuarioSoporte = '" + ddlEncargadoSeleccionado + "'");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }



        public DataTable loadNombreIntegrantes(string IdGrupoAsignado)
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("select NombreUsuarioSoporte from QA.parametrizacionUsuariosSoporte where idGrupoSoporte = " + IdGrupoAsignado);
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        #endregion
    }
}