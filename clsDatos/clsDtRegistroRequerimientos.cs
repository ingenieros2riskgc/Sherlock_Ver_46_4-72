using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using clsDTO;

namespace clsDatos
{
    public class clsDtRegistroRequerimientos
    {

        public DataTable mtdInsertarRequerimientos(clsDTORegistroRequerimientos objRegReq, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [QA].[RegistroRequerimiento] ([Usuario], [Empresa], [NumeroREQ], " +
                    "[FechaCreacionREGREQ], [TipoFalla], [DetalleTipoFalla], [Descripcion], [Ruta]) VALUES ('{0}', '{1}', '{2}', " +
                    "GETDATE(), '{4}', '{5}', '{6}', '{7}' )", objRegReq.StrNombre, objRegReq.StrEmpresa, objRegReq.StrNumReq + objRegReq.StrId, 
                    "GETDATE()", objRegReq.StrTipoFalla, objRegReq.DDLText, objRegReq.StrDescripcion, objRegReq.StrRutaError);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el requerimiento. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
            return dtInformacion;
        }

        public DataTable mtdInsertarControlRequerimientos(clsDTOGestionRequerimientos objGesReq, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [QA].[GestionRequerimiento] ([Usuario], [Empresa], [NumeroREQ], [FechaCreacionGESREQ], " +
                    "[TipoFalla], [DetalleTipoFalla], [Descripcion], [Ruta], [GrupoAsignado], [Encargado], [Estado], [Criticidad], " +
                    "[FechaVencimientoGESREQ]) VALUES ('{0}', '{1}', '{2}', GETDATE(), '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', " +
                    "CONVERT(date, '{12}', 103))", objGesReq.Usuario, objGesReq.Empresa, objGesReq.NumeroREQ, "GETDATE()", 
                    objGesReq.TipoFalla, objGesReq.DetalleTipoFalla, objGesReq.Descripcion, objGesReq.Ruta, objGesReq.GrupoAsignado, 
                    objGesReq.Encargado, objGesReq.Estado, objGesReq.Criticidad, objGesReq.FechaVencimientoGESREQ);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de requerimiento. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
            return dtInformacion;
        }

        public DataTable mtdActualizarControlRequerimientos(string strNumeroGES, clsDTOActualizarGestionRequerimientos objGesReq, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [QA].[GestionRequerimiento] SET [NumeroREQ] = '{0}', GrupoAsignado = '{1}', " +
                    "Encargado = '{2}', Estado = '{3}', Criticidad = '{4}', FechaVencimientoGESREQ = CONVERT(date, '{5}',103) WHERE " +
                    "NumeroREQ = '" + strNumeroGES + "'", objGesReq.NumeroREQ,
                    objGesReq.GrupoAsignado, objGesReq.Encargado, objGesReq.Estado, objGesReq.Criticidad, objGesReq.FechaVencimientoGESREQ);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el control de requerimiento. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
            return dtInformacion;
        }

        public DataTable mtdConsultaEvidencias(ref string strErrMsg, string strId)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdEvidencia], [URLArchivo], [Descripcion], [FechaRegistro] FROM QA.EvidenciaAdjunto where [idREGREQ] = {0}", strId);
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


        public DataTable mtdConsultaComentarios(ref string strErrMsg, string strIdComentarios)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdComentario], [URLArchivo], [Descripcion], [FechaRegistro] FROM QA.ComentarioAdjunto where [idGESREQ] = {0}", strIdComentarios);
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


        public DataTable mtdConsultaDatos(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [idREGREQ], [Usuario], [Empresa], [NumeroREQ], [FechaCreacionREGREQ], [TipoFalla], " +
                    "[DetalleTipoFalla], [Descripcion], [Ruta] FROM [QA].[RegistroRequerimiento] Order by idREGREQ DESC");
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


        public DataTable mtdConsultaGestion(string strNumeroREQ, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [idGESREQ], [NumeroREQ], [GrupoAsignado], [Encargado], [Estado], [Criticidad], " +
                    "[FechaVencimientoGESREQ], [Comentario] FROM [QA].[GestionRequerimiento] WHERE NumeroREQ = '" + strNumeroREQ + "' Order by idGESREQ DESC");
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


        public DataTable mtdConsultaReporte(string strEstado, ref string strErrMsg)
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
        public DataTable mtdConsultaReportes(string NomIntegrante, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("select idGESREQ, Empresa, Usuario, NumeroREQ, FechaCreacionGESREQ, TipoFalla, GrupoAsignado, " +
                    "Encargado, Estado, Criticidad, FechaVencimientoGESREQ from QA.GestionRequerimiento where Encargado = '" + NomIntegrante + "' " +
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

        /*public DataTable mtdConsultaReportes(string NomIntegrante, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("select idGESREQ, Empresa, Usuario, NumeroREQ, FechaCreacionGESREQ, TipoFalla, GrupoAsignado, " +
                    "Encargado, Estado, Criticidad, FechaVencimientoGESREQ from QA.GestionRequerimiento where Encargado = '" + NomIntegrante + "' " +
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

        }*/

    }
}
