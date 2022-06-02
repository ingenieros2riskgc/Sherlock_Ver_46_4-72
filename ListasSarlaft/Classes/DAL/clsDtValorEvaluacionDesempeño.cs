using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtValorEvaluacionDesempeño
    {
        public bool mtdInsertarValorEvaluacionDesempeño(clsEvaluacionDesempeno objEvaluacionDesempeño, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblEvaluacionDesempeno] ([NombreEvaluacion],[IdCargoResponsable],[FechaEvaluacion],[Evaluador]" +
               ",[Calificacion],[RecomCapacitacion],[RecomCompromisos],[Otros],[DescripcionOtros],[FechaProximaEvaluacion],[IdUsuario],[FechaRegistro]) " +
                    "VALUES('{0}','{1}',CONVERT(date,'{2}',126),'{3}',{4},'{5}','{6}','{7}','{8}',CONVERT(date,'{9}',126),{10},GETDATE()) ",
                    objEvaluacionDesempeño.strNombre, objEvaluacionDesempeño.intCargoResponsable, objEvaluacionDesempeño.dtFechaEvaluacion, objEvaluacionDesempeño.strEvaluador,
                    clsUtilidades.mtdQuitarComasAPuntos("" + objEvaluacionDesempeño.intCalificacion), objEvaluacionDesempeño.strRecomendacionCapacitacion, objEvaluacionDesempeño.strRecomendacionCompromisos, objEvaluacionDesempeño.strOtros,
                    objEvaluacionDesempeño.strDescripcionOtros, objEvaluacionDesempeño.dtFechaProximaEvaluacion, objEvaluacionDesempeño.intIdUsuario, objEvaluacionDesempeño.dtFechaRegistro);

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
        public DataTable mtdLastIdEvaluacionDesempeño(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblEvaluacionDesempeno]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al obtener el Id de la ultima Evaluacion de Competencia. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarValorDetalleDesempeño(ref string nombreFactor, ref decimal valorFactor, ref int IdEvaluacionDesempeño, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblDetalleEvaluacionDesempeño] ([nombreFactor],[valorFactor],[IdEvaluacionDesempeño])" +
                    "VALUES ('{0}',{1},{2}) ",
                    nombreFactor, clsUtilidades.mtdQuitarComasAPuntos("" + valorFactor), IdEvaluacionDesempeño);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Detalle de la Evaluacion de Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarEvaluacionesDesempeño(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[Id],a.[NombreEvaluacion],a.[IdCargoResponsable],a.[FechaEvaluacion],a.[Evaluador],a.[Calificacion],a.[RecomCapacitacion]"
                    + ",a.[RecomCompromisos],a.[Otros],a.[DescripcionOtros],a.[FechaProximaEvaluacion],a.[IdUsuario],a.[FechaRegistro], c.NombreHijo, b.Usuario as Usuario"
                    + " FROM [Procesos].[tblEvaluacionDesempeno] as a"
                    + " inner join Listas.Usuarios as b on b.IdUsuario = a.IdUsuario"
                    + " INNER JOIN Parametrizacion.JerarquiaOrganizacional AS c ON c.idHijo = a.IdCargoResponsable");

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
        public bool mtdConsultarReporteEvaluacionesDesempeño(ref DataTable dtCaracOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[Id],a.[NombreEvaluacion],a.[IdCargoResponsable],a.[FechaEvaluacion],a.[Evaluador],a.[Calificacion],a.[RecomCapacitacion]"
                    + ",a.[RecomCompromisos],a.[Otros],a.[DescripcionOtros],a.[FechaProximaEvaluacion],a.[IdUsuario],a.[FechaRegistro], c.NombreHijo, b.Usuario as Usuario"
                    + " FROM [Procesos].[tblEvaluacionDesempeno] as a"
                    + " inner join Listas.Usuarios as b on b.IdUsuario = a.IdUsuario"
                    + " INNER JOIN Parametrizacion.JerarquiaOrganizacional AS c ON c.idHijo = a.IdCargoResponsable where a.[FechaRegistro] between '{0} 00:00:00' and '{1} 23:59:59'"
                    ,fechaInicial, fechaFinal);

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
        public bool mtdConsultarReporteEvaluacionesDesempeñoResponsable(ref DataTable dtCaracOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal, ref int IdResponsable)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[Id],a.[NombreEvaluacion],a.[IdCargoResponsable],a.[FechaEvaluacion],a.[Evaluador],a.[Calificacion],a.[RecomCapacitacion]"
                    + ",a.[RecomCompromisos],a.[Otros],a.[DescripcionOtros],a.[FechaProximaEvaluacion],a.[IdUsuario],a.[FechaRegistro], c.NombreHijo, b.Usuario as Usuario"
                    + " FROM [Procesos].[tblEvaluacionDesempeno] as a"
                    + " inner join Listas.Usuarios as b on b.IdUsuario = a.IdUsuario"
                    + " INNER JOIN Parametrizacion.JerarquiaOrganizacional AS c ON c.idHijo = a.IdCargoResponsable where a.[FechaRegistro] between '{0} 00:00:00' and '{1} 23:59:59' and a.IdCargoResponsable = {2}"
                    , fechaInicial, fechaFinal, IdResponsable);

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
        public bool mtdConsultarDetalleDesempeño(ref DataTable dtCaracOut, ref int IdEvaluacionDesempeño, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdValorFactor],[nombreFactor],[valorFactor],IdEvaluacionDesempeño"
                    + " FROM [Procesos].[tblDetalleEvaluacionDesempeño]"
                    + " where [IdEvaluacionDesempeño] ={0}",IdEvaluacionDesempeño);

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
        public bool mtdUpdateEvaluacionDesempeño(ref clsEvaluacionDesempeno objEvaluacionDesempeño, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblEvaluacionDesempeno] SET [RecomCapacitacion] = '{0}' ,[RecomCompromisos] = '{1}'"+
                ",[Otros] = '{2}' ,[DescripcionOtros] = '{3}' ,[FechaProximaEvaluacion] = '{4}'" +
                    " WHERE Id = {5} ",
                    objEvaluacionDesempeño.strRecomendacionCapacitacion, objEvaluacionDesempeño.strRecomendacionCompromisos, objEvaluacionDesempeño.strOtros,
                    objEvaluacionDesempeño.strDescripcionOtros, objEvaluacionDesempeño.dtFechaProximaEvaluacion, objEvaluacionDesempeño.intId);

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