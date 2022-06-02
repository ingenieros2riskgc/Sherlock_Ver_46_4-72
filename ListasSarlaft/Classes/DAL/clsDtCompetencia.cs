using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ListasSarlaft.Classes
{
    public class clsDtCompetencia
    {
        /// <summary>
        /// Realiza la consulta para traer todas las Competencia
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarCompetencia(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PC.[Id],PC.[NombreCompetencia],PC.[Ponderacion],PC.[FechaRegistro],PC.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblCompetencia] PC " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PC.IdUsuario = LU.IdUsuario ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Competencia. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        public bool mtdConsultarCompetencia(clsCompetencia objCompetenciaIn, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion 

            try
            {
                strConsulta = string.Format("SELECT PC.[Id],PC.[NombreCompetencia],PC.[Ponderacion],PC.[FechaRegistro],PC.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblCompetencia] PC " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PC.IdUsuario = LU.IdUsuario " +
                    "WHERE PC.[Id] = {0}", objCompetenciaIn.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarCompetencia(clsCompetencia objCompetencia, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblCompetencia]([NombreCompetencia],[Ponderacion],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',{1},GETDATE(),{2}) ",
                    objCompetencia.strNombre, objCompetencia.intPonderacion, objCompetencia.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de la informacion en la base de datos
        /// </summary>
        /// <param name="objCompetencia">Informacion de la Competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCompetencia(clsCompetencia objCompetencia, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblCompetencia] SET [NombreCompetencia] = '{1}',[Ponderacion] = {2} " +
                    "WHERE [Id] = {0} ",
                    objCompetencia.intId, objCompetencia.strNombre, objCompetencia.intPonderacion);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdSumatoriaPonderadoCompetencia(ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT ISNULL(SUM(PC.[Ponderacion]),0) SumatoriaPonderados " +
                    "FROM [Procesos].[tblCompetencia] PC ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los poderados de las Competencias. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la consulta para traer todas los Criterios
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public bool mtdConsultarCriterio(clsCompetencia objCompetencia, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PCC.[Id],PCC.[Descripcion],PCC.[IdCompetencia],PC.[NombreCompetencia],PCC.[FechaRegistro],PCC.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblCriterioCompetencia] PCC " +
                    "INNER JOIN [Procesos].[tblCompetencia] PC ON PC.[Id] = PCC.[IdCompetencia] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PCC.IdUsuario = LU.IdUsuario " +
                    "WHERE PCC.[IdCompetencia] = {0}", objCompetencia.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Criterio de Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarCriterio(clsCriterioCompetencia objCriterio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblCriterioCompetencia]([IdCompetencia],[Descripcion],[FechaRegistro],[IdUsuario]) " +
                    "VALUES({0},'{1}',GETDATE(),{2}) ",
                    objCriterio.intIdCompetencia, objCriterio.strDescripcion, objCriterio.intIdUsuario);

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

        public bool mtdActualizarCriterio(clsCriterioCompetencia objCriterio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblCriterioCompetencia] SET [IdCompetencia] = {1},[Descripcion] = '{2}' " +
                    "WHERE [Id] = {0} ",
                    objCriterio.intId, objCriterio.intIdCompetencia, objCriterio.strDescripcion);

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
        #region Gestion Evaluacion Competencias
        public DataTable mtdGetCriteriosCompetencias(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = "SELECT [Descripcion] ,[NombreCompetencia] FROM [dbo].[vwGestionEvaluacionCompetencias]";

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Criterio. [{0}]", ex.Message);
                
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        public DataTable mtdGetCriteriosCompetenciasEvaVal(ref int IdEvaluacionCompetencia, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = "SELECT [Competencia],[Criterio],[PuntajeAsignado] FROM [Procesos].[tblValorEvaluacionCompetencias]"
                    + " WHERE [IdEvaluacionCompetencia] = "+IdEvaluacionCompetencia;

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Criterio. [{0}]", ex.Message);

            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        public DataTable mtdGetCalificaciones(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = "SELECT [Descripcion],[ValorMin],[ValorMax],[Id],[NombreEvaluacion] FROM [dbo].[vwCalificacionEvaluaciones]"
                +" where Id = 2";

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Criterio. [{0}]", ex.Message);

            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        #endregion
    }
}