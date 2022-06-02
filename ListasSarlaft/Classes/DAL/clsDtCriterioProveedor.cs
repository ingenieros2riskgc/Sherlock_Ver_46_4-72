using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtCriterioProveedor
    {
        public bool mtdConsultarAspecto(ref DataTable dtInformacion,  ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PAP.[Id], PAP.[NombreAspecto], PAP.[ValorPorcentaje] Valor, PAP.[FechaRegistro],PAP.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblAspectoProveedor] PAP " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PAP.IdUsuario = LU.IdUsuario ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Aspecto. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdConsultarAspecto(clsAspectoProveedor objAspectoIn, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PAP.[Id], PAP.[NombreAspecto], PAP.[ValorPorcentaje] Valor, PAP.[FechaRegistro],PAP.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblAspectoProveedor] PAP " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PAP.IdUsuario = LU.IdUsuario " +
                    "WHERE PAP.[Id] = {0} ", objAspectoIn.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Aspecto. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdSumatoriaPonderadoAspecto(ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT ISNULL(SUM(PAP.[ValorPorcentaje]),0) SumatoriaPonderados " +
                    "FROM [Procesos].[tblAspectoProveedor] PAP ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los poderados de los Aspectos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarAspecto(clsAspectoProveedor objAspecto, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblAspectoProveedor]([NombreAspecto],[ValorPorcentaje],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',{1},GETDATE(),{2}) ",
                    objAspecto.strAspecto, objAspecto.decValor, objAspecto.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Aspecto. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdActualizarAspecto(clsAspectoProveedor objAspecto, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblAspectoProveedor] SET [NombreAspecto] = '{0}', [ValorPorcentaje] = {1} " +
                    "WHERE [Id] = {2}",
                    objAspecto.strAspecto, clsUtilidades.mtdQuitarComasAPuntos("" + objAspecto.decValor), objAspecto.intId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Aspecto. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdConsultarCriterio(clsAspectoProveedor objAspecto, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PCP.[Id],PCP.[IdAspectoProveedor],PCP.[Descripcion],PCP.[FechaRegistro],PCP.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblCriterioProveedor] PCP " +
                    "INNER JOIN [Procesos].[tblAspectoProveedor] PAP ON PAP.[Id] = PCP.[IdAspectoProveedor] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PCP.IdUsuario = LU.IdUsuario " +
                    "WHERE PCP.[IdAspectoProveedor]  = {0}", objAspecto.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Criterio del Aspecto. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarCriterio(clsCriterioProveedor objCriterio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblCriterioProveedor] " +
                    "([IdAspectoProveedor],[Descripcion],[FechaRegistro],[IdUsuario]) " +
                    "VALUES ({0},'{1}',GETDATE(),{2}) ",
                    objCriterio.intIdAspecto, objCriterio.strDescripcion, objCriterio.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Criterio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;

        }

        public bool mtdActualizarCriterio(clsCriterioProveedor objCriterio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblCriterioProveedor]  " +
                    "SET [IdAspectoProveedor] = {0} ,[Descripcion] = '{1}' " +
                    "WHERE [Id] = {2} ",
                    objCriterio.intIdAspecto, objCriterio.strDescripcion, objCriterio.intId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Criterio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;

        }

        public bool mtdConsultarParametro(clsCriterioProveedor objCriterio, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPP.[Id], PPP.[IdCriterioProv], PPP.[Descripcion], PPP.[FechaRegistro],PPP.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblParametrosProveedor] PPP " +
                    "INNER JOIN [Procesos].[tblCriterioProveedor] PCP ON PCP.[Id] = PPP.[IdCriterioProv] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PCP.IdUsuario = LU.IdUsuario " +
                    "WHERE PPP.[IdCriterioProv] = {0}", objCriterio.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Parámetro del Criterio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarParametro(clsParametros objParametro, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblParametrosProveedor] " +
                    "([IdCriterioProv],[Descripcion],[FechaRegistro],[IdUsuario]) " +
                    "VALUES({0},'{1}',GETDATE(),{2}) ",
                    objParametro.intIdCriterioProveedor, objParametro.strDescripcionParametro, objParametro.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Parámetro. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;

        }

        public bool mtdActualizarParametro(clsParametros objParametro, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblParametrosProveedor]   " +
                    "SET [IdCriterioProv] = {0}, [Descripcion] = '{1}' " +
                    "WHERE [Id] = {2} ",
                    objParametro.intIdCriterioProveedor, objParametro.strDescripcionParametro, objParametro.intId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Parámetro. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
    }
}