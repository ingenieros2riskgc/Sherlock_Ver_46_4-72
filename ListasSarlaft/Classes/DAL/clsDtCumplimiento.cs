using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtCumplimiento
    {
        /// <summary>
        /// Realiza la insercion de la Calificacion
        /// </summary>
        /// <param name="objCalificacion">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdInsertarCalificacion(clsCalificacion objCalificacion, ref int intIdCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInfo = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblCalificacion]([NombreCumplimiento],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',GETDATE(), {1}); " +
                    "SELECT SCOPE_IDENTITY() ",
                    objCalificacion.strNombreCumplimiento, objCalificacion.intIdUsuario);

                cDatabase.mtdConectarSql();
                dtInfo = cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        intIdCalificacion = Convert.ToInt32(dtInfo.Rows[0][0].ToString());

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

        /// <summary>
        /// Inserta la informacion de los detalles de la calificacion
        /// </summary>
        /// <param name="lstDetalleCal">Lista de detalles</param>
        /// <param name="intIdCalificacion">Identificador de la calificacion</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si la operacion fue exitosa</returns>
        public bool mtdInsertarDetalleCalificacion(List<clsDetalleCalificacion> lstDetalleCal, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                cDatabase.conectar();

                foreach (clsDetalleCalificacion objDetalle in lstDetalleCal)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblDetalleCalificacion] " +
                        "([IdIndicador],[IdSemaforo],[NombreCumplimiento],[ValorMinimo],[ValorMaximo],[FechaRegistro],[IdUsuario]) " +
                        "VALUES({0},{1},'{2}',{3},{4},GETDATE(),{5}) ",
                        objDetalle.intIdIndicador, objDetalle.intIdSemaforo, objDetalle.strNombreCumplimiento,
                        clsUtilidades.mtdQuitarComasAPuntos(objDetalle.intValorMin.ToString()),
                        clsUtilidades.mtdQuitarComasAPuntos(objDetalle.intValorMax.ToString()),
                        objDetalle.intIdUsuario);
                    cDatabase.ejecutarQuery(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el detalle de la Calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la consulta de la Calificacion
        /// </summary>
        /// <param name="objCalificacionIn">Informacion a consultar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdConsultarCalificacion(clsCalificacion objCalificacionIn, ref DataTable dtInfoCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id], [NombreCumplimiento], [FechaRegistro], [IdUsuario] " +
                    "FROM [Procesos].[tblCalificacion] " +
                    "WHERE [Id] = {0} ",
                    objCalificacionIn.intId);

                cDatabase.conectar();
                dtInfoCalificacion = cDatabase.ejecutarConsulta(strConsulta);

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la Consulta del detalle de la Calificacion
        /// </summary>
        /// <param name="objCalificacionIn">Informacion a consultar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdConsultarDetalleCalificacion(clsIndicador objIndicadorIn, ref DataTable dtInfoDetCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[IdIndicador],[IdSemaforo],[NombreCumplimiento],[ValorMinimo],[ValorMaximo],[FechaRegistro],[IdUsuario] " +
                    "FROM [Procesos].[tblDetalleCalificacion] " +
                    "WHERE [IdIndicador] = {0} ",
                    objIndicadorIn.intId);

                cDatabase.conectar();
                dtInfoDetCalificacion = cDatabase.ejecutarConsulta(strConsulta);

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el detalle de la Calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la Consulta del detalle de la Calificacion
        /// </summary>
        /// <param name="objCalificacionIn">Informacion a consultar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdConsultarDetalleCalificacion(clsDetalleCalificacion objDetCalificacionIn, ref DataTable dtInfoDetCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[IdIndicador],[IdSemaforo],[NombreCumplimiento],[ValorMinimo],[ValorMaximo],[FechaRegistro],[IdUsuario] " +
                    "FROM [Procesos].[tblDetalleCalificacion] " +
                    "WHERE [IdIndicador] = {0} AND [IdSemaforo] = {1} ",
                    objDetCalificacionIn.intIdIndicador, objDetCalificacionIn.intIdSemaforo);

                cDatabase.conectar();
                dtInfoDetCalificacion = cDatabase.ejecutarConsulta(strConsulta);

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el detalle de la Calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdActualizarDetalleCalificacion(List<clsDetalleCalificacion> lstDetalleCump, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                cDatabase.conectar();

                foreach (clsDetalleCalificacion objDetCal in lstDetalleCump)
                {
                    strConsulta = string.Format("UPDATE [Procesos].[tblDetalleCalificacion] " +
                        "SET [ValorMinimo] = {1}, [ValorMaximo] = {2}, [NombreCumplimiento] = '{3}' " +
                        "WHERE [Id] = {0} AND [IdSemaforo] = {4}",
                        objDetCal.intId,
                        clsUtilidades.mtdQuitarComasAPuntos(objDetCal.intValorMin.ToString()),
                        clsUtilidades.mtdQuitarComasAPuntos(objDetCal.intValorMax.ToString()),
                        objDetCal.strNombreCumplimiento.Trim(),
                        objDetCal.intIdSemaforo);

                    cDatabase.ejecutarConsulta(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar del detalle de la Calificación. [{0}]", ex.Message);
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