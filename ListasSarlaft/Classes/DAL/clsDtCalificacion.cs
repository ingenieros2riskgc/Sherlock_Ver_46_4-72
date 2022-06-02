using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtCalificacion
    {
        public bool mtdConsultarCalificacion(ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PCE.[Id], PCE.[IdConfigEvaluacion], PCEv.[IdEvaluacion], PE.[NombreEvaluacion], PCE.[Descripcion], " +
                    "PCE.[ValorMin], PCE.[ValorMax], PCE.[FechaRegistro], PCE.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblCalificacionEvaluacion] PCE " +
                    "INNER JOIN [Procesos].[tblConfigEvaluacion] PCEv ON PCEV.[Id] = PCE.[IdConfigEvaluacion] " +
                    "INNER JOIN [Procesos].[tblEvaluacion] PE ON PE.[Id] = PCEv.[IdEvaluacion] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON LU.[IdUsuario] = PCE.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdConsultarCalificacion(clsEvaluacion objEvaluacion, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PCE.[Id], PCE.[IdConfigEvaluacion], PCEv.[IdEvaluacion], PE.[NombreEvaluacion], PCE.[Descripcion], " +
                    "PCE.[ValorMin], PCE.[ValorMax], PCE.[FechaRegistro], PCE.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblCalificacionEvaluacion] PCE " +
                    "INNER JOIN [Procesos].[tblConfigEvaluacion] PCEv ON PCEV.[Id] = PCE.[IdConfigEvaluacion] " +
                    "INNER JOIN [Procesos].[tblEvaluacion] PE ON PE.[Id] = PCEv.[IdEvaluacion] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON LU.[IdUsuario] = PCE.[IdUsuario] " +
                    "WHERE PE.[Id] = {0}", objEvaluacion.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarCalificacion(clsCalificacionEvaluacion objCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            int intIdIndicador = 0;
            string strConsulta = string.Empty;
            DataTable dtInfo = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblConfigEvaluacion]([IdEvaluacion],[FechaRegistro],[IdUsuario]) " +
                    "VALUES ({0},GETDATE(),{1}) " +
                    "SELECT SCOPE_IDENTITY() ",
                    objCalificacion.intIdEvaluacion, objCalificacion.intIdUsuario);

                cDatabase.mtdConectarSql();
                dtInfo = cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        intIdIndicador = Convert.ToInt32(dtInfo.Rows[0][0].ToString());

                strConsulta = string.Format("INSERT INTO [Procesos].[tblCalificacionEvaluacion]([IdConfigEvaluacion],[Descripcion],[ValorMin],[ValorMax],[FechaRegistro],[IdUsuario]) " +
                    "VALUES ({0},'{1}',{2},{3},GETDATE(),{4}) ",
                    intIdIndicador, objCalificacion.strDescripcion,
                    clsUtilidades.mtdQuitarComasAPuntos(objCalificacion.intValorMinimo.ToString()), 
                    clsUtilidades.mtdQuitarComasAPuntos(objCalificacion.intValorMaximo.ToString()), 
                    objCalificacion.intIdUsuario);

                cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdActualizarCalificacion(clsCalificacionEvaluacion objCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInfo = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion 

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblConfigEvaluacion] SET [IdEvaluacion] = {0} " +
                    "WHERE [Id] = {1} ",
                    objCalificacion.intIdEvaluacion, objCalificacion.intIdConfiguracionEvaluacion);

                cDatabase.mtdConectarSql();
                cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                strConsulta = string.Format("UPDATE [Procesos].[tblCalificacionEvaluacion] SET [Descripcion] = '{0}', [ValorMin] = {1}, [ValorMax] = {2} " +
                    "WHERE [Id] = {3} ",
                    objCalificacion.strDescripcion,
                    clsUtilidades.mtdQuitarComasAPuntos(objCalificacion.intValorMinimo.ToString()),
                    clsUtilidades.mtdQuitarComasAPuntos(objCalificacion.intValorMaximo.ToString()),
                    objCalificacion.intId);

                cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la Calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdConsultarEvaluacion(ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[NombreEvaluacion],[FechaRegistro],[IdUsuario] " +
                    "FROM [Procesos].[tblEvaluacion] where [Id] <> 4");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Evaluación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}