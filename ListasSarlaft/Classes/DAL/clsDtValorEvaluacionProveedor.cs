using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDtValorEvaluacionProveedor
    {
        public bool mtdConsultarProveedorCriterios(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[NombreAspecto],[ValorPorcentaje],[FechaRegistro],[IdUsuario],[DesCriterio],[DesParametro],[Usuario]"
                    + " FROM [dbo].[vwEvaluacionProveedor]"
                    + " ORDER BY ID");

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
                + " where Id = 3";

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
        public bool mtdInsertarValorEvaluacionProveedor(clsEvaluacionProveedor objEvaluacionProveedor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblEvaluacionProveedor] ([NombreProveedor],[FechaEvaluacion],[PeriodoEvaluacionInicial],[PeriodoEvaluacionFinal]" +
                ",[ServicioOfrecido],[RealizadoPor],[CargoResponsable],[FechaRegistro],[IdUsuario],[Observaciones],[FechaProximaEvaluacion])" +
                    "VALUES('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}',{8},'{9}','{10}') ",
                    objEvaluacionProveedor.strNombreProveedor, objEvaluacionProveedor.dtFechaEvaluacion, objEvaluacionProveedor.dtPeriodoEvaluadoInicial, objEvaluacionProveedor.dtPeriodoEvaluadoFinal,
                    objEvaluacionProveedor.strServicioOfrecido, objEvaluacionProveedor.strRealizadoPor, objEvaluacionProveedor.intCargoResponsable, objEvaluacionProveedor.dtFechaRegistro, objEvaluacionProveedor.intIdUsuario,
                    objEvaluacionProveedor.strObservaciones, objEvaluacionProveedor.dtFechaProximaEvaluacion);

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
        public DataTable mtdLastIdEvaluacionProveedor(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblEvaluacionProveedor]");

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
        public bool mtdInsertarValorAspectoProveedor(clsAspectoProveedorHistorico objAspectoProveedor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblAspectoProveedorHistorico] ([NombreAspecto],[ValorPorcentaje],[IdEvalProveedor],[FechaRegistro],[IdUsuario])" +
                    "VALUES('{0}',{1},{2},'{3}',{4}) ",
                    objAspectoProveedor.strAspecto, clsUtilidades.mtdQuitarComasAPuntos(""+objAspectoProveedor.intValor), objAspectoProveedor.intEvaluacionProveedor, objAspectoProveedor.dtFechaRegistro, objAspectoProveedor.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear Aspecto Proveedor Historico. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdAspectoProveedor(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblAspectoProveedorHistorico]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear Aspecto Proveedor Historico. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarValorCriterioProveedor(clsCriterioProvHistorico objCriterioProveedor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblCriterioProvHistorico] ([IdAspectoProvHistorico],[Descripcion],[FechaRegistro],[IdUsuario])" +
                    "VALUES({0},'{1}','{2}',{3}) ",
                    objCriterioProveedor.intIdAspectoProveedorHistorico, objCriterioProveedor.strDescripcion, objCriterioProveedor.dtFechaRegistro, objCriterioProveedor.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear Criterio Proveedor Historico. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdCriterioProveedor(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblCriterioProvHistorico]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear Criterio Proveedor Historico. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarValorParametroProveedor(clsParametrosProvHistorico objParametroProveedor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblParametrosProvHistorico] ([IdCriterioProvHist],[Descripcion],[Valor],[FechaRegistro],[IdUsuario])" +
                    "VALUES({0},'{1}',{2},'{3}',{4}) ",
                    objParametroProveedor.intIdCriterioProvHistorico, objParametroProveedor.strDescripcion, clsUtilidades.mtdQuitarComasAPuntos(""+objParametroProveedor.intValorParametro), objParametroProveedor.dtFechaRegistro, objParametroProveedor.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear Criterio Proveedor Historico. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarEvaluacionProveedor(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[Id],a.[NombreProveedor],a.[FechaEvaluacion],a.[PeriodoEvaluacionInicial],a.[PeriodoEvaluacionFinal],a.[ServicioOfrecido]"+
                ",a.[RealizadoPor],a.[CargoResponsable],a.[FechaRegistro],a.[IdUsuario],b.NombreHijo,c.Usuario,a.[Observaciones],a.[FechaProximaEvaluacion]"
                    + " FROM [Procesos].[tblEvaluacionProveedor] as a"
                    + " inner join [Parametrizacion].[JerarquiaOrganizacional] as b on b.idHijo = a.CargoResponsable"
                    + " inner join [Listas].[Usuarios] as c on c.IdUsuario = a.IdUsuario");

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
        public bool mtdConsultarProveedorCriteriosH(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdEvalProveedor)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[NombreAspecto],[ValorPorcentaje],[DesCriterio],[DesParametro],[Valor],[IdEvalProveedor],[FechaRegistro],[IdUsuario]"
                    + " FROM [dbo].[vwEvaluacionProveedorHistorico]"
                    + " where [IdEvalProveedor] ={0}" , IdEvalProveedor);

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
        public bool mtdModificarValorEvaluacionProveedor(clsEvaluacionProveedor objEvaluacionProveedor, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblEvaluacionProveedor]" +
                " SET [Observaciones] = '{1}',[FechaProximaEvaluacion] = '{2}'" +
                    " WHERE Id={0}",
                    objEvaluacionProveedor.intId,objEvaluacionProveedor.strObservaciones,objEvaluacionProveedor.dtFechaProximaEvaluacion);

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
    }
}