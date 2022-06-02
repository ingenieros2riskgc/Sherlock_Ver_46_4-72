using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ListasSarlaft.Classes
{
    public class clsDtValorEvaluacionCompetencias
    {
        public bool mtdInsertarEvaluacionCompetencia(clsEvaluacionCompetencia objEvaluacionCompetencia, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblEvaluacionCompetencia]([FechaRegistro],[NombreEvaluado],[CargoResponsable],[JefeInmediato],[IdMacroproceso],[IdUsuario]) " +
                    "VALUES('{5}','{0}',{1},'{2}',{3},{4}) ",
                    objEvaluacionCompetencia.strNombreEvaluado, objEvaluacionCompetencia.intCargoResponsable, objEvaluacionCompetencia.strJefeInmediato, objEvaluacionCompetencia.intIdMacroProceso, objEvaluacionCompetencia.intIdUsuario, objEvaluacionCompetencia.dtFechaRegistro);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdInsertarTipoProcesoEvaluacionCompetencia(int IdTipoProceso,int IdProceso, int IdEvaluacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblEvaluacionCompetenciaTipoProceso] ([IdTipoProceso],[IdProceso],[IdEvaluacion])" +
                    "VALUES ({0}, {1}, {2}) ",
                    IdTipoProceso,IdProceso, IdEvaluacion);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdEvaluacionCompetencia(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblEvaluacionCompetencia]");

                cDatabase.conectar();
                dt =  cDatabase.ejecutarConsulta(strConsulta);
                
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public DataTable mtdNombreProceso(ref int IdMacroProceso,ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [Nombre] FROM [Procesos].[Macroproceso]" + " where IdMacroProceso ="+IdMacroProceso);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarVECtable(clsValorEvaluacionCompetencia objValorEvaluacionCompetencia, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblValorEvaluacionCompetencias] ([IdEvaluacionCompetencia],[Competencia],[Criterio],[PuntajeAsignado]) " +
                    "VALUES({0},'{1}','{2}',{3}) ",
                    objValorEvaluacionCompetencia.intIdValorEvaluacionCompetencia, objValorEvaluacionCompetencia.strCompetencia, objValorEvaluacionCompetencia.strCriterio, objValorEvaluacionCompetencia.intPuntajeAsignado);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool Guardar(string nombrearchivo, int length, byte[] archivo, int IdRegistro, ref string strErrMsg, string extension)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString()))
            {
                try
                {

                    conn.Open();

                    string query = @"INSERT INTO Procesos.Archivos (nombre, length, archivo, Proceso, IdRegistro, extension)
                             VALUES (@name, @length, @archivo, @Proceso, @IdRegistro, @extension)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@name", nombrearchivo);
                    cmd.Parameters.AddWithValue("@length", length);
                    cmd.Parameters.AddWithValue("@Proceso", "EvaluacionCompetencias");
                    cmd.Parameters.AddWithValue("@IdRegistro", IdRegistro);
                    cmd.Parameters.AddWithValue("@extension", extension);

                    SqlParameter archParam = cmd.Parameters.Add("@archivo", SqlDbType.VarBinary);
                    archParam.Value = archivo;

                    cmd.ExecuteNonQuery();

                    booResult = true;
                }
                catch (Exception ex)
                {
                    strErrMsg = string.Format("Error al Cargar el Archivo. [{0}]", ex.Message);
                }
                return booResult;
            }

        }
        public bool mtdDownLoadFile(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdRegistro, string filename)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {

                strConsulta = string.Format("SELECT [archivo]" +
                " FROM [Procesos].[Archivos]" +
                " where Proceso = 'EvaluacionCompetencias' and IdRegistro = {0} and Nombre = '{1}'"
                , IdRegistro, filename);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public bool mtdConsultarDocumento(ref DataTable dtCaracOut, ref string strErrMsg, ref int Idregistro)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {

                strConsulta = string.Format("SELECT [IdArchivo],[Nombre],[length],[archivo],[Proceso],[IdRegistro],[extension]" +
                " FROM [Procesos].[Archivos]" +
                " where IdRegistro = {0} AND Proceso = 'EvaluacionCompetencias'"
                , Idregistro);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public bool mtdInsertarObservaciones(clsObservacionProveedor objObservaciones, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblObservacionEvaluacionCompetencia] ([IdEvaluacionCompetencia],[Observaciones],[FechaProxEvaluacion]) " +
                    "VALUES({0},'{1}','{2}') ",
                    objObservaciones.intIdEvaluacionProveedor, objObservaciones.strDescripcion, objObservaciones.dtFechaProximaEvaluacion);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia observaciones. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarObservaciones(clsObservacionProveedor objObservaciones, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblObservacionEvaluacionCompetencia] SET [Observaciones] = '"+objObservaciones.strDescripcion+
                "',[FechaProxEvaluacion] = '"+ objObservaciones.dtFechaProximaEvaluacion+
                "' where [IdEvaluacionCompetencia] = "+objObservaciones.intIdEvaluacionProveedor);
                
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la Evaluacion de Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarEvaluacionesCompetencias(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[FechaRegistro],[NombreEvaluado],[CargoResponsable],[NombreHijo],[JefeInmediato],[Nombre],[IdMacroproceso],[IdUsuario],[Usuario],[IdTipoProceso]"
                    + " FROM [Procesos].[vwEvalucionCompetencias] inner join [Procesos].[tblEvaluacionCompetenciaTipoProceso] as ECTP on ECTP.IdEvaluacion = [Procesos].[vwEvalucionCompetencias].Id order by IdTipoProceso, Id");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Evaluacion de Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarObservaciones(ref int IdEvaluacionCompetencia,ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Observaciones],[FechaProxEvaluacion]"
                    + " FROM [Procesos].[tblObservacionEvaluacionCompetencia]"
                    + " WHERE [IdEvaluacionCompetencia] = " + IdEvaluacionCompetencia
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Observaciones. [{0}]", ex.Message);
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