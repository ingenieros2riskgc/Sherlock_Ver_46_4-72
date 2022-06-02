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
    public class clsDtParamArchivo
    {
        private readonly object cDataBase;

        public clsDtParamArchivo() { }

        #region Variables

        public DataTable mtdConsultaTipoParametro(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdUsuario], [IdTipoParametro], [NombreParametro], [Calificacion], [Activo] FROM [Perfiles].[tblTipoParametros]");
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

        public DataTable mtdConsultaTipoParametroMod(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.IdTipoParametro, a.NombreParametro, a.Calificacion, a.Activo, a.IdUsuario, " +
                    "a.FechaModificacion, rtrim(b.Usuario)Usuario FROM Perfiles.tblTipoParametros_logAuditoria a " +
                    "LEFT JOIN Listas.Usuarios b on a.IdUsuario = b.IdUsuario " +
                    "ORDER BY a.IdTipoParametro, a.FechaModificacion DESC");
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


        public DataTable ConsulVariables(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.NombreParametro, a.Calificacion, a.Activo, rtrim(b.Usuario)Usuario, " +
                    "a.FechaModificacion FROM Perfiles.tblTipoParametros_logAuditoria a " +
                    "INNER JOIN Listas.Usuarios b ON a.IdUsuario = b.IdUsuario " +
                    "ORDER BY a.IdTipoParametro, a.FechaModificacion DESC");
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


        public DataTable mtdConsultaTipoParametroXEstado(clsDTOVariable objVariableIn, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdUsuario], [IdTipoParametro], [NombreParametro], [Calificacion], [Activo] " +
                    "FROM [Perfiles].[tblTipoParametros] " +
                    "WHERE [Activo] = {0}", objVariableIn.BooActivo == false ? 0 : 1);
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

        public DataTable mtdConsultaTipoParametro(clsDTOVariable objVariableIn, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdTipoParametro], [NombreParametro], [Activo] FROM [Perfiles].[tblTipoParametros] WHERE [IdTipoParametro] = {0}",
                    objVariableIn.StrIdVariable);
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la variable. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaTipoParametro(clsDTOEstructuraCampo objEstruct, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PTP.[IdUsuario], PTP.[IdTipoParametro], PTP.[NombreParametro], PTP.[Calificacion], " +
                    "PTP.[Activo] FROM [Perfiles].[tblTipoParametros] PTP " +
                    "INNER JOIN [Perfiles].[tblEstructuraCampos] PEC ON PEC.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "WHERE PEC.[IdCampo] = {0}",
                    objEstruct.StrIdEstructCampo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la variable. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaSumatoria(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT SUM(Calificacion) Sumatoria  FROM [Perfiles].[tblTipoParametros] WHERE Activo = 1");
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

        public void mtdActualizarTipoParametro(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblTipoParametros] SET [IdUsuario] = {1},[NombreParametro] = '{2}', " +
                    "[Calificacion] = {3}, [Activo] = {4} WHERE [IdTipoParametro] = {0}",
                    objVariable.StrIdVariable, objVariable.StrIdUsuario, objVariable.StrNombreVariable, objVariable.StrCalificacion, objVariable.BooActivo == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la variable. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdAgregarTipoParametro(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT [Perfiles].[tblTipoParametros] ([NombreParametro], [Calificacion], [Activo]) VALUES ('{0}', {1}, {2})",
                    objVariable.StrNombreVariable, objVariable.StrCalificacion, objVariable.BooActivo == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la variable. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }
        public void mtdAgregarTipoParametroCreate(clsDTOVariableCreate objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT [Perfiles].[tblTipoParametros] ([IdUsuario],[NombreParametro], [Calificacion], " +
                    "[Activo]) VALUES ({0}, '{1}', {2}, {3})",
                    objVariable.StrIdUsuario, objVariable.StrNombreVariable, objVariable.StrCalificacion, objVariable.BooActivo == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la variable. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        /*
         * Metodos para el Servicio
         */
        #region Metodos Servicio
        public DataTable mtdConsultaTipoParametro(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdTipoParametro], [NombreParametro], [Calificacion], [Activo] FROM [Perfiles].[tblTipoParametros]");
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

        public DataTable mtdConsultaTipoParametro(string strOleConn, clsDTOVariable objVariableIn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdTipoParametro], [NombreParametro], [Activo] FROM [Perfiles].[tblTipoParametros] WHERE [IdTipoParametro] = {0}",
                    objVariableIn.StrIdVariable);
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la variable. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaTipoParametro(string strOleConn, clsDTOEstructuraCampo objEstruct, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PTP.[IdTipoParametro], PTP.[NombreParametro], PTP.[Calificacion], PTP.[Activo] " +
                    "FROM [Perfiles].[tblTipoParametros] PTP " +
                    "INNER JOIN [Perfiles].[tblEstructuraCampos] PEC ON PEC.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "WHERE PEC.[IdCampo] = {0}",
                    objEstruct.StrIdEstructCampo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la variable. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaSumatoria(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT SUM(Calificacion) Sumatoria  FROM [Perfiles].[tblTipoParametros]");
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
        #endregion
        #endregion Variables

        #region Categorias
        public DataTable mtdConsultaParametrizacion(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdParametros], PP.[IdTipoParametro], PP.[NombreParametro], PP.[CalificacionParametro], PP.[CodigoParametro], " +
                    "PTP.[NombreParametro] NombreTipoParametro, PP.[EsFormula] " +
                    "FROM [Perfiles].[tblParametrizacion] PP INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PP.[IdTipoParametro]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorías. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaParametrizacion(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdUsuario], PP.[IdParametros], PP.[IdTipoParametro], PP.[NombreParametro], " +
                    "PP.[CalificacionParametro], PP.[CodigoParametro], PTP.[NombreParametro] NombreTipoParametro, PP.[EsFormula] " +
                    "FROM [Perfiles].[tblParametrizacion] PP " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PP.[IdTipoParametro] AND PTP.[Activo] = {0}",
                    objVariable.BooActivo == false ? 0 : 1);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorías. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        public DataTable mtdConsultaParametrizacionMod(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdParametros], PP.[IdTipoParametro], PP.[NombreParametro], " +
                    "PP.[CalificacionParametro], PP.[CodigoParametro], PTP.[NombreParametro] NombreTipoParametro, PP.[EsFormula], " +
                    "PP.[IdUsuario], PP.[FechaModificacion], rtrim(b.Usuario)Usuario FROM [Perfiles].[tblParametrizacion_logAuditoria] " +
                    "PP LEFT JOIN Listas.Usuarios b on PP.IdUsuario = b.IdUsuario " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PP.[IdTipoParametro] " +
                    "ORDER BY IdParametrosLog DESC"
                    );

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorías. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }


        public DataTable ConsulCategorias(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[NombreParametro], PP.[CalificacionParametro], PP.[CodigoParametro], " +
                    "PTP.[NombreParametro] NombreTipoParametro, PP.[FechaModificacion], rtrim(b.Usuario)Usuario " +
                    "FROM [Perfiles].[tblParametrizacion_logAuditoria] PP LEFT JOIN Listas.Usuarios b on PP.IdUsuario = b.IdUsuario " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PP.[IdTipoParametro] " +
                    "ORDER BY IdParametrosLog DESC"
                    );

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorías. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }


        public DataTable mtdConsultaParametrizacion(string strIdCampo, string strValorIn, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPar.[IdUsuario], PPar.[IdParametros], PPar.[IdTipoParametro], PPar.[NombreParametro], PPar.[CalificacionParametro] Calificacion, PPar.[CodigoParametro], PTP.[NombreParametro] NombreTipoParametro, PPar.[EsFormula] " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PEC.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "INNER JOIN [Perfiles].[tblParametrizacion] PPar ON PPar.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "WHERE PEC.[IdCampo] = {0} AND PPar.CodigoParametro = '{1}'", strIdCampo, strValorIn);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorías. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaParametrizacion(string strIdCampo, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPar.[IdUsuario], PPar.[IdParametros], PPar.[IdTipoParametro], PPar.[NombreParametro], PPar.[CalificacionParametro] Calificacion, PPar.[CodigoParametro], PTP.[NombreParametro] NombreTipoParametro, PPar.[EsFormula] " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PEC.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "INNER JOIN [Perfiles].[tblParametrizacion] PPar ON PPar.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "WHERE PEC.[IdCampo] = {0}", strIdCampo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorías. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public void mtdActualizarParametrizacion(clsDTOParametrizacion objParametrizacion, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblParametrizacion] SET [IdUsuario] = {0}, [IdTipoParametro] = {2}, " +
                    "[NombreParametro] = '{3}', [CalificacionParametro] = '{4}', " +
                    "[CodigoParametro] = '{5}', [EsFormula] = {6} WHERE [IdParametros] = {1}",
                    objParametrizacion.StrIdUsuario, objParametrizacion.StrIdCategoria, objParametrizacion.StrIdVariable,
                    objParametrizacion.StrNombreCategoria, objParametrizacion.StrCalificacionCategoria,
                    objParametrizacion.StrCodigoCategoria, objParametrizacion.BooEsFormula == true ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la Categoría. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }
        public void mtdAgregarParametrizacion(clsDTOParametrizacion objParametrizacion, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblParametrizacion] " +
                    "([IdTipoParametro], [NombreParametro], [CalificacionParametro], [CodigoParametro], [EsFormula]) VALUES ({0}, '{1}', '{2}', '{3}', {4})",
                    objParametrizacion.StrIdVariable, objParametrizacion.StrNombreCategoria, objParametrizacion.StrCalificacionCategoria,
                    objParametrizacion.StrCodigoCategoria, objParametrizacion.BooEsFormula == true ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Categoria. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdAgregarParametrizacionCreate(clsDTOParametrizacionCreate objParametrizacion, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblParametrizacion] ([IdUsuario], " +
                    "[IdTipoParametro], [NombreParametro], [CalificacionParametro], [CodigoParametro], [EsFormula]) " +
                    "VALUES ({0}, {1}, '{2}', '{3}', '{4}', {5})",
                    objParametrizacion.StrIdUsuario, objParametrizacion.StrIdVariable, objParametrizacion.StrNombreCategoria, 
                    objParametrizacion.StrCalificacionCategoria,
                    objParametrizacion.StrCodigoCategoria, objParametrizacion.BooEsFormula == true ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Categoria. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdEliminarCategoria(clsDTOParametrizacion objParametrizacion, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Perfiles].[tblParametrizacion] WHERE [IdParametros] = {0}",
                        objParametrizacion.StrIdCategoria);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    strErrMsg = string.Format("Error en la eliminación de la información. <br/> La información a borrar tiene relación con variables. <br/> Por favor revise la información.");
                else
                    strErrMsg = string.Format("Error en la eliminación de la información." + "<br/>" + "Descripción: {0}", odbcEx.Message.ToString());
            }
            catch (OleDbException odbcEx)
            {
                strErrMsg = mtdOdbcError(odbcEx);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al borrar la categoría. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }
        /*
         * Metodos para el Servicio
         */
        #region Servicio
        public DataTable mtdConsultaCategoria(string strOleConn, string strIdCampo, string strValorIn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPar.[IdParametros], PPar.[IdTipoParametro], PPar.[NombreParametro], PPar.[CalificacionParametro] Calificacion, PPar.[CodigoParametro], PTP.[NombreParametro] NombreTipoParametro, PPar.[EsFormula] " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PEC.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "INNER JOIN [Perfiles].[tblParametrizacion] PPar ON PPar.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "WHERE PEC.[IdCampo] = {0} AND PPar.CodigoParametro = '{1}'", strIdCampo, strValorIn);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorías. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Este metodo hace lo mismo que mtdConsultaParametrizacion adicionandole el parametro de cadena de conexion
        /// </summary>
        /// <param name="strConn"></param>
        ///  <param name="strIdCampo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdConsultaCategoria(string strOleConn, string strIdCampo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPar.[IdParametros], PPar.[IdTipoParametro], PPar.[NombreParametro], PPar.[CalificacionParametro] Calificacion, PPar.[CodigoParametro], PTP.[NombreParametro] NombreTipoParametro, PPar.[EsFormula] " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PEC.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "INNER JOIN [Perfiles].[tblParametrizacion] PPar ON PPar.[IdTipoParametro] = PTP.[IdTipoParametro] " +
                    "WHERE PEC.[IdCampo] = {0}", strIdCampo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorías. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        #endregion
        #endregion

        #region Estructura
        public DataTable mtdConsultaEstructura(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                //strConsulta = string.Format("SELECT PEC.[IdCampo], PEC.[NombreCampo], PEC.[Longitud], PEC.[Parametriza], PEC.[IdTipoParametro], " +
                //    "PEC.[IdTipoDato], PTP.[NombreParametro] NombreTipoParametro, PEC.[Posicion], PTD.[NombreTipoDato], PEC.Estado,PEC.Numerico " +
                //    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                //    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PEC.[IdTipoParametro] " +
                //    "INNER JOIN [Perfiles].[tblTipoDato] PTD ON PTD.[IdTipoDato] = PEC.[IdTipoDato] ORDER BY PEC.Posicion");

            strConsulta = string.Format("SELECT PEC.IdCampo, PEC.NombreCampo, PEC.Longitud, PEC.Parametriza, PEC.IdTipoParametro," + 
                "PEC.IdTipoDato, PTP.NombreParametro NombreTipoParametro, PEC.Posicion, PTD.NombreTipoDato, PEC.Estado, PEC.Numerico, " +
                "PEC.IdUsuario FROM[Perfiles].[tblEstructuraCampos] PEC " +
                "INNER JOIN[Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PEC.[IdTipoParametro]" +
                "INNER JOIN[Perfiles].[tblTipoDato] PTD ON PTD.[IdTipoDato] = PEC.[IdTipoDato] ORDER BY PEC.Posicion");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la estructura de campos. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaEstructura(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PEC.[IdUsuario], PEC.[IdCampo], PEC.[NombreCampo], PEC.[Longitud], PEC.[Parametriza], PEC.[IdTipoParametro], " +
                    "PEC.[IdTipoDato], PTP.[NombreParametro] NombreTipoParametro, PEC.[Posicion], PTD.[NombreTipoDato], PEC.Estado, PEC.Numerico " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PEC.[IdTipoParametro] " +
                    "INNER JOIN [Perfiles].[tblTipoDato] PTD ON PTD.[IdTipoDato] = PEC.[IdTipoDato] ORDER BY PEC.Posicion", objVariable.BooActivo ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la estructura de campos. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        public DataTable mtdConsultaEstructuraMod(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.IdCampo,a.NombreCampo, a.Longitud, a.Parametriza,c.NombreParametro,a.Posicion,a.IdTipoParametro," +
                    "a.IdTipoDato,a.Estado, a.Numerico,a.IdUsuario,a.FechaModificacion, rtrim(b.Usuario)Usuario " +
                    "FROM Perfiles.tblEstructuraCampos_logAuditoria a  LEFT JOIN Listas.Usuarios b on a.IdUsuario=b.IdUsuario " +
                    "INNER JOIN Perfiles.tblTipoParametros c on a.IdTipoParametro=c.IdTipoParametro " +
                    "ORDER BY a.IdCampo,a.FechaModificacion DESC");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la estructura de campos. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable ConsulEstructuraArchivos(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.NombreCampo, a.Parametriza,c.NombreParametro,a.Posicion, a.Numerico," +
                    "rtrim(b.Usuario)Usuario, a.FechaModificacion FROM Perfiles.tblEstructuraCampos_logAuditoria a  " +
                    "LEFT JOIN Listas.Usuarios b on a.IdUsuario=b.IdUsuario INNER JOIN Perfiles.tblTipoParametros c " +
                    "on a.IdTipoParametro=c.IdTipoParametro ORDER BY a.IdCampo,a.FechaModificacion DESC");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la estructura de campos. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }


        public DataTable ConsulEstructura(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.IdCampo,a.NombreCampo, a.Longitud, a.Parametriza,c.NombreParametro,a.Posicion,a.IdTipoParametro," +
                    "a.IdTipoDato,a.Estado, a.Numerico,a.IdUsuario,a.FechaModificacion, rtrim(b.Usuario)Usuario " +
                    "FROM Perfiles.tblEstructuraCampos_logAuditoria a  LEFT JOIN Listas.Usuarios b on a.IdUsuario=b.IdUsuario " +
                    "INNER JOIN Perfiles.tblTipoParametros c on a.IdTipoParametro=c.IdTipoParametro " +
                    "ORDER BY a.IdCampo,a.FechaModificacion DESC");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la estructura de campos. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }


        public void mtdAgregarEstructuraCampo(clsDTOEstructuraCampo objEstructura, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblEstructuraCampos] ([NombreCampo], [Longitud], [Parametriza], [IdTipoParametro], " +
                    "[IdTipoDato], [Posicion], [Estado], [Numerico]) " +
                    "VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, 1, {6})",
                    objEstructura.StrNombreCampo, Convert.ToInt32(objEstructura.StrLongitud),
                    objEstructura.BooEsParametrico == true ? 1 : 0,
                    Convert.ToInt32(objEstructura.StrIdVariable), Convert.ToInt32(objEstructura.StrIdTipoDato),
                    Convert.ToInt32(objEstructura.StrPosicion), objEstructura.BoolNumerico == true ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdAgregarEstructuraCampoCreate(clsDTOEstructuraCampo objEstructura, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblEstructuraCampos] ([IdUsuario], [NombreCampo], [Longitud], [Parametriza], [IdTipoParametro], " +
                    "[IdTipoDato], [Posicion], [Estado], [Numerico]) " +
                    "VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8})",
                    objEstructura.StrIdUsuario, objEstructura.StrNombreCampo, Convert.ToInt32(objEstructura.StrLongitud),
                    objEstructura.BooEsParametrico == true ? 1 : 0,
                    Convert.ToInt32(objEstructura.StrIdVariable), Convert.ToInt32(objEstructura.StrIdTipoDato),
                    Convert.ToInt32(objEstructura.StrPosicion), objEstructura.BoolEstado == true ? 1 : 0,
                       objEstructura.BoolNumerico == true ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdActualizarEstructura(clsDTOEstructuraCampo objEstructura, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblEstructuraCampos] " +
                    "SET [IdUsuario] = {0}, [NombreCampo] = '{1}', [Longitud] = {2}, [Parametriza] = {3}, [IdTipoParametro] = {4}, " +
                    "[IdTipoDato] = {5}, [Posicion]= {6}, [Numerico] = {7}, [Estado] = {8}" +
                    "WHERE [IdCampo] = {9}",
                    objEstructura.StrIdUsuario,
                    objEstructura.StrNombreCampo, Convert.ToInt32(objEstructura.StrLongitud),
                    objEstructura.BooEsParametrico == true ? 1 : 0,
                    Convert.ToInt32(objEstructura.StrIdVariable), Convert.ToInt32(objEstructura.StrIdTipoDato),
                    Convert.ToInt32(objEstructura.StrPosicion), objEstructura.BoolNumerico == true ? 1 : 0, objEstructura.BoolEstado == true ? 1 : 0,
                    Convert.ToInt32(objEstructura.StrIdEstructCampo));

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public DataTable mtdCantidadCamposEstructura(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) NumeroCamposEstructura FROM [Perfiles].[tblEstructuraCampos]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdCantidadCamposEstructura(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) NumeroCamposEstructura " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC WHERE Estado = 1 " //+
                    /*"INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PEC.[IdTipoParametro] AND PTP.[Activo] = {0}"*/,
                    objVariable.BooActivo ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarPosXCampo(string strNombreCampo, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Posicion] FROM [Perfiles].[tblEstructuraCampos] WHERE [NombreCampo] LIKE '{0}'",
                    strNombreCampo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaCampo(clsDTOEstructuraCampo objCampoIn, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdCampo], [NombreCampo], [Longitud], [Parametriza], [IdTipoParametro], [IdTipoDato], [Posicion], [Numerico] " +
                    "FROM [Perfiles].[tblEstructuraCampos] WHERE [IdCampo] = {0}",
                    objCampoIn.StrIdEstructCampo);
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la variable. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        /*
        * Metodos para el Servicio  
        */
        #region Metodos Servicio
        public DataTable mtdConsultaEstructura(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PEC.[IdCampo], PEC.[NombreCampo], PEC.[Longitud], PEC.[Parametriza], PEC.[IdTipoParametro], " +
                    "PEC.[IdTipoDato], PTP.[NombreParametro] NombreTipoParametro, PEC.[Posicion], PTD.[NombreTipoDato] " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PEC.[IdTipoParametro] " +
                    "INNER JOIN [Perfiles].[tblTipoDato] PTD ON PTD.[IdTipoDato] = PEC.[IdTipoDato]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la estructura de campos. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaEstructura(string strOleConn, clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PEC.[IdCampo], PEC.[NombreCampo], PEC.[Longitud], PEC.[Parametriza], PEC.[IdTipoParametro], " +
                    "PEC.[IdTipoDato], PTP.[NombreParametro] NombreTipoParametro, PEC.[Posicion], PTD.[NombreTipoDato] " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                    "INNER JOIN [Perfiles].[tblTipoParametros] PTP ON PTP.[IdTipoParametro] = PEC.[IdTipoParametro] AND PTP.[Activo] = {0}" +
                    "INNER JOIN [Perfiles].[tblTipoDato] PTD ON PTD.[IdTipoDato] = PEC.[IdTipoDato]",
                    objVariable.BooActivo ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la estructura de campos. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarPosXCampo(string strConn, string strNombreCampo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Posicion] FROM [Perfiles].[tblEstructuraCampos] WHERE [NombreCampo] LIKE '{0}'",
                    strNombreCampo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdCantidadCamposEstructura(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) NumeroCamposEstructura FROM [Perfiles].[tblEstructuraCampos]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdCantidadCamposEstructura(string strOleConn, clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) NumeroCamposEstructura FROM [Perfiles].[tblEstructuraCampos]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la parametrización. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        #endregion
        #endregion

        #region Relacion
        public DataTable mtdConsultarRelaciones(clsDTOPerfil objPerfil, bool booActivos, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty, strAnexo1 = string.Empty, strAnexo2 = string.Empty;
            #endregion Vars

            try
            {
                if (booActivos)
                {
                    strAnexo1 = "WHERE PCP.[Activo] = 1";
                    strAnexo2 = "AND PCP.[Activo] = 1";
                }

                strConsulta = string.Format("SELECT ISNULL(PCP.[IdConfiguracionPerfil],'') IdConfPerfil, " +
                    "ISNULL(PP.[IdPerfil],'') IdPerfil, ISNULL(PP.[Nombre],'') NombrePerfil, ISNULL(PEC.[IdCampo],'') IdConfCampo, " +
                    "PEC.[NombreCampo], PEC.[Posicion], " +
                    "CASE  WHEN  PCP.[IdConfiguracionPerfil] IS NULL THEN 0 ELSE PCP.[Activo] END Activo " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                    "LEFT JOIN [Perfiles].[tblConfPerfil] PCP ON PCP.[IdEstructuraCampo] = PEC.IdCampo " +
                    "LEFT JOIN [Perfiles].[tblPerfil] PP ON PP.[IdPerfil] = PCP.[IdPerfil] {1} " +
                    "UNION SELECT ISNULL(PCP.[IdConfiguracionPerfil],'') IdConfPerfil, " +
                    "ISNULL(PP.[IdPerfil],'') IdPerfil, ISNULL(PP.[Nombre],'') NombrePerfil, ISNULL(PEC.[IdCampo],'') IdConfCampo, " +
                    "PEC.[NombreCampo] NombreCampo, PEC.[Posicion], " +
                    "CASE  WHEN  PCP.[IdConfiguracionPerfil] IS NULL THEN 0 ELSE PCP.[Activo] END Activo " +
                    "FROM [Perfiles].[tblEstructuraCampos] PEC " +
                    "LEFT JOIN [Perfiles].[tblConfPerfil] PCP ON PCP.[IdEstructuraCampo] = PEC.IdCampo " +
                    "LEFT JOIN [Perfiles].[tblPerfil] PP ON PP.[IdPerfil] = PCP.[IdPerfil] " +
                    "WHERE PCP.[IdPerfil] =  {0} {2} ORDER BY 5",
                    objPerfil.StrIdPerfil, strAnexo1, strAnexo2);
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la relación entre el perfil y el archivo. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public void mtdAgregarRelacion(clsDTORelacion objRelacion, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblConfPerfil]([IdPerfil], [IdEstructuraCampo], [Activo]) VALUES ({0}, {1}, {2})",
                    objRelacion.StrIdPerfil, objRelacion.StrIdConfCampo, objRelacion.BooActivo == true ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la relación. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdActualizarRelacion(clsDTORelacion objRelacion, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblConfPerfil] SET [Activo] = {0} WHERE [IdConfiguracionPerfil] = {1}",
                    objRelacion.BooActivo == true ? 1 : 0, objRelacion.StrIdRelacion);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la relación. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }
        #endregion Relacion

        #region Perfil Variables
        public DataTable mtdConsultarPerfilVariable(clsDTOPerfil objPerfil, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdPerfilVariable], [IdPerfil], [IdVariable] FROM [Perfiles].[tblPerfilVariable] " +
                    "WHERE [IdPerfil] = {0}",
                    objPerfil.StrIdPerfil);
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles y las variables asociadas. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public void mtdEliminarPerfilVariable(clsDTOPerfil objPerfil, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Perfiles].[tblPerfilVariable] WHERE [IdPerfil] = {0}",
                        objPerfil.StrIdPerfil);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    strErrMsg = "Error en la eliminación de la información. <br/> La información a borrar tiene relación con algun perfil y sus variables. <br/> Por favor revise la información.";
                else
                    strErrMsg = string.Format("Error en la eliminación de la información.<br/> Descripción: {0}.", odbcEx.Message.ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al borrar el perfil y sus variables. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdGuardarPerfilVariable(clsDTOPerfilVariable objPerfilVar, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                cDatabase.conectar();

                strConsulta = string.Format("INSERT [Perfiles].[tblPerfilVariable] ([IdPerfil], [IdVariable]) VALUES ({0}, {1})",
                        objPerfilVar.StrIdPerfil, objPerfilVar.StrIdVariable);
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Relación Factor de Riesgo - Señal. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }
        #endregion

        private string mtdOdbcError(OleDbException Ex)
        {
            string strError = string.Empty;

            switch (Ex.ErrorCode)
            {
                case -2147217873:
                    strError = "<br/> La información a borrar tiene relación con otro objeto. <br/> Por favor revise la información.";
                    break;
                default:
                    strError = "Descripción: " + Ex.Message.ToString();
                    break;
            }

            return strError;
        }

        public int ActualizarEstadoCampo(int IdCampo, int Estado)
        {
            try
            {
                clsDatabase cDataBase = new clsDatabase();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdCampo", SqlDbType = SqlDbType.Int, Value = IdCampo },
                    new SqlParameter() { ParameterName = "@Estado", SqlDbType = SqlDbType.Int, Value = Estado },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                int result = cDataBase.EjecutarSPParametrosReturnInteger("[Perfiles].[ActivarDesactivarCampo]", parametros);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
