using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDtEncuestas
    {
        public bool mtdInsertarEncuestas(clsEncuestas objEnc, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("INSERT INTO [Procesos].[Encuestas] ([NombreEncuesta],[CantPregunta],[IdUsuario],[FechaRegistro],[DescripcionEmpresa]) " +
                    "VALUES ('{0}',{1},{2},GETDATE(),'{3}')",
                    objEnc.strNombreEncuesta, objEnc.intCantPreguntas, objEnc.intIdUsuario, objEnc.strDescripcionEmpresa);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la encuesta. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarEncuestas(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[IdEncuesta],a.[NombreEncuesta],a.[CantPregunta],a.[IdUsuario],a.[FechaRegistro],a.[DescripcionEmpresa],b.Usuario"
                    + " FROM [Procesos].[Encuestas] as a INNER JOIN"
                    + " Listas.Usuarios AS b ON a.IdUsuario = b .IdUsuario");

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
        public bool mtdConsultarPreguntas(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdEncuesta)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdPregunta],[IdEncuesta],[TextoPregunta],[Consecutivo]"
                    + " FROM [Procesos].[EncuestasPreguntas]"
                    + " where IdEncuesta ={0} order by Consecutivo", IdEncuesta);

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
        public DataTable mtdLastIdEncuesta(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(IdEncuesta) LastId FROM [Procesos].[Encuestas]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la Encuesta. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarPregunta(clsPreguntasEncuestas objEnc, ref string strErrMsg, ref int IdEncuesta)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("INSERT INTO [Procesos].[EncuestasPreguntas] ([IdEncuesta],[TextoPregunta],[Consecutivo]) " +
                    "VALUES ({0},'{1}',{2})",
                    IdEncuesta, objEnc.strTextoPregunta, objEnc.intConsecutivo);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de No conformidad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarPregunta(clsPreguntasEncuestas objEnc, ref string strErrMsg, ref int IdEncuesta)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Procesos].[EncuestasPreguntas] WHERE [IdEncuesta] ={0} and [IdPregunta] = {1}", IdEncuesta, objEnc.intIdPregunta);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                strConsulta = string.Format("INSERT INTO [Procesos].[EncuestasPreguntas] ([IdEncuesta],[TextoPregunta],[Consecutivo]) " +
                    "VALUES ({0},'{1}',{2})",
                    IdEncuesta, objEnc.strTextoPregunta, objEnc.intConsecutivo);

                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de No conformidad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarEncuesta(clsEncuestas objEnc, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[Encuestas] SET [NombreEncuesta] ='{0}' ,[CantPregunta] = {1},[IdUsuario] = {2}" +
                    " ,[FechaRegistro] ='{3}',[DescripcionEmpresa] = '{4}'"+
                    " where IdEncuesta = {5}",
                    objEnc.strNombreEncuesta, objEnc.intCantPreguntas, objEnc.intIdUsuario, objEnc.dtFechaRegistro, objEnc.strDescripcionEmpresa, objEnc.intIdEncuesta);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la encuesta. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdConsecutivoQuestions(ref string strErrMsg, int IdEncuesta)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT ISNULL(MAX([Consecutivo]),0) as Consecutivo  FROM[Procesos].[EncuestasPreguntas] where IdEncuesta = {0}", IdEncuesta);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el consecutivo de la Encuesta. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
    }
}