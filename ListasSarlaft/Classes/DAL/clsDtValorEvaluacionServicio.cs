using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDtValorEvaluacionServicio
    {
        public bool mtdConsultarEncuesta(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdEncuesta)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[IdEncuesta],a.[NombreEncuesta],a.[CantPregunta],a.[DescripcionEmpresa],b.TextoPregunta,b.[IdPregunta]"
                    + " FROM [Procesos].[Encuestas] as a"
                    + " inner join [Procesos].[EncuestasPreguntas] as b on b.IdEncuesta = a.IdEncuesta"
                    + " where a.[IdEncuesta] ={0}",IdEncuesta);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdInsertarValorEvaluacionServicio(clsEvaluacionServicio objEvaluacionServicio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblEvaluacionServicio] ([CiudadEncuesta],[FechaEncuesta],[IdTipoEncuesta],[NombreCliente],[Nombre],[Cargo]"+
                ",[Funcionarios],[FechaRegistro],[IdUsuario])" +
                    "VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8}) ",
                    objEvaluacionServicio.strCiudad, objEvaluacionServicio.dtFecha, objEvaluacionServicio.intIdTipoEncuesta, objEvaluacionServicio.strNombreCliente, objEvaluacionServicio.strNombre,
                    objEvaluacionServicio.strCargo, objEvaluacionServicio.strFuncionarios, objEvaluacionServicio.dtFechaRegistro, objEvaluacionServicio.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdEvaluacionServicio(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblEvaluacionServicio]");

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
        public bool mtdInsertarValorRespuesta(clsValorRespuesta objRespuesta, ref string strErrMsg, ref int IdEncuesta)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[EncuestaResultados] ([IdEncuesta],[IdEvaluacionServicio],[IdPregunta],[Respuesta])" +
                    "VALUES({0},{1},{2},'{3}') ",
                    IdEncuesta, objRespuesta.intIdEvaluacionServicio, objRespuesta.intIdPregunta, objRespuesta.strRespuesta);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdIdEncuesta(ref string strErrMsg, ref int IdEvaluacionServicio)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [IdEncuesta] FROM [Procesos].[EncuestaResultados] where [IdEvaluacionServicio] = {0}",IdEvaluacionServicio);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error el extraer id encuesta. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarValorObservacion(clsObservacionServicio objObservacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblObservacionEvalServicio] ([IdEvalServicio],[Descripcion],[FechaRegistro],[IdUsuario])" +
                    "VALUES({0},'{1}','{2}',{3}) ",
                    objObservacion.intIdEvaServicio, objObservacion.strObservacion, objObservacion.dtfecha, objObservacion.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarEvaluacionServicio(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[Id],[CiudadEncuesta],a.[FechaEncuesta],a.[IdTipoEncuesta],a.[NombreCliente],a.[Nombre],a.[Cargo],a.[Funcionarios],a.[FechaRegistro],a.[IdUsuario],e.[Usuario]"
                    + " FROM [Procesos].[tblEvaluacionServicio] as a"
                    + " INNER JOIN  Listas.Usuarios AS e ON a.IdUsuario = e.IdUsuario"
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la evaluacion de servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarEvaluacionRespuestas(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdEvaServicio)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[Respuesta],b.TextoPregunta"
                    + " FROM [Procesos].[EncuestaResultados] as a"
                    + " inner join Procesos.EncuestasPreguntas as b on b.IdPregunta = a.IdPregunta"
                    + " where a.[IdEvaluacionServicio] ={0}", IdEvaServicio
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las respuestas. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarEvaluacionObservacion(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdEvaServicio)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Descripcion]"
                    + " FROM [Procesos].[tblObservacionEvalServicio]"
                    + " where [IdEvalServicio] ={0}", IdEvaServicio
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las respuestas. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarValorObservacion(ref string strErrMsg, ref string observaciones, ref int IdEvaServicio)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblObservacionEvalServicio] SET [Descripcion] = '{1}'" +
                    " WHERE [IdEvalServicio] = {0} ",
                    IdEvaServicio, observaciones);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
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