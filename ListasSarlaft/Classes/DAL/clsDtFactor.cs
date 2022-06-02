using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtFactor
    {
        public bool mtdConsultarFactor(ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion

            try
            {
                strConsulta = string.Format("SELECT PFD.[Id], PFD.[FactoresEvaluacion], PFD.[FechaRegistro], PFD.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblFactorDesempeno] PFD " +
                    "INNER JOIN [Listas].[Usuarios] LU ON LU.[IdUsuario] = PFD.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Factor. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarFactor(clsFactoresDesempeno objFactor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblFactorDesempeno]([FactoresEvaluacion],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',GETDATE(),{1}) ",
                    objFactor.strFactoresEvaluacion, objFactor.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Factor. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdActualizarFactor(clsFactoresDesempeno objFactor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion 

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblFactorDesempeno] SET [FactoresEvaluacion] = '{0}' " +
                    "WHERE [Id] = {1} ",
                    objFactor.strFactoresEvaluacion, objFactor.intId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el Factor. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }


        public bool mtdConsultarDetFactor(clsFactoresDesempeno objFactor, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PDFD.[Id], PDFD.[IdFactorDesempeno], PFD.[FactoresEvaluacion] NombreFactor,  " +
                    "PDFD.[IdCalificacionEvaluacion], PCE.[Descripcion] CalificacionEvaluacion, PDFD.[Descripcion], " +
                    "PDFD.[IdUsuario], LU.[Usuario] NombreUsuario, PDFD.[FechaRegistro], ISNULL(PDFD.CriterioMinimo, 0) as CriterioMinimo, ISNULL(PDFD.CriterioMaximo, 0) as CriterioMaximo " +
                    "FROM [Procesos].[tblDetalleFactorDesempeno] PDFD " +
                    "INNER JOIN [Procesos].[tblFactorDesempeno] PFD ON PFD.[Id] = PDFD.[IdFactorDesempeno] "+
                    "INNER JOIN [Listas].[Usuarios] LU ON LU.[IdUsuario] = PDFD.[IdUsuario] "+
                    "INNER JOIN [Procesos].[tblCalificacionEvaluacion] PCE ON PCE.[Id] = PDFD.[IdCalificacionEvaluacion] "+
                    "WHERE PDFD.[IdFactorDesempeno] = {0} ", objFactor.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el detalle del factor de desempeño. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarDetFactorEva(clsFactoresDesempeno objFactor, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[IdFactorDesempeno],[NombreFactor],[IdCalificacionEvaluacion],[CalificacionEvaluacion],[Descripcion],"+
                    "[IdUsuario],[NombreUsuario],[FechaRegistro],ISNULL([CriterioMinimo], 0) as CriterioMinimo,ISNULL([CriterioMaximo], 0) as CriterioMaximo" +
                    " FROM [dbo].[vwFactorDesempeño]" +
                    "where IdFactorDesempeno ={0}", objFactor.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el detalle del factor de desempeño. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarDetFactor(clsDetalleFactorDesempeno objDetFactor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblDetalleFactorDesempeno] " +
                    "([IdFactorDesempeno],[IdCalificacionEvaluacion],[Descripcion],[IdUsuario],[FechaRegistro],[CriterioMinimo],[CriterioMaximo]) " +
                    "VALUES({0},{1},'{2}',{3},GETDATE(),{4},{5}) ",
                    objDetFactor.intIdFactoresDesempeno, objDetFactor.intIdCalificacion, objDetFactor.strDescripcion, objDetFactor.intIdUsuario
                    , clsUtilidades.mtdQuitarComasAPuntos(objDetFactor.decCriterioMinimo.ToString()),clsUtilidades.mtdQuitarComasAPuntos(objDetFactor.decCriterioMaximo.ToString()));

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el detalle del factor de desempeño. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdActualizarDetFactor(clsDetalleFactorDesempeno objDetFactor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblDetalleFactorDesempeno] SET [IdCalificacionEvaluacion] = {0}, [Descripcion] = '{1}', " 
                    +" [CriterioMinimo] ={3}, [CriterioMaximo]={4}" +
                    " WHERE [Id] = {2} ",
                    objDetFactor.intIdCalificacion, objDetFactor.strDescripcion, objDetFactor.intId, clsUtilidades.mtdQuitarComasAPuntos(objDetFactor.decCriterioMinimo.ToString())
                    ,clsUtilidades.mtdQuitarComasAPuntos(objDetFactor.decCriterioMaximo.ToString()));

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el detalle del factor de desempeño. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
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
                + " where Id = 1";

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

    }
}