using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALPorcentajeCalificacion
    {
        public bool mtdInsertarPorcentajeCalificacion(clsDTOPorcentajeCalificacion objPorcentaje, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Parametrizacion].[PorcentajeCalificarControl]([NombrePorcentajeCalificarControl],[ValorPorcentajeCalificarControl])"+
                    "VALUES('{0}',{1}) ",
                    objPorcentaje.strNombrePorcentajeCalificarControl, objPorcentaje.intValorPorcentajeCalificarControl);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el porcentaje de calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public int mtdCantidadTotalValorPorcentaje(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int Sumtotal = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT ISNULL(sum([ValorPorcentajeCalificarControl]),0) as Sumtotal FROM [Parametrizacion].[PorcentajeCalificarControl]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                Sumtotal = Convert.ToInt32(dtCaracOut.Rows[0]["Sumtotal"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el valor total del peso. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return Sumtotal;
        }

        public int mtdCantidadTotalValorPorcentajeUp(ref string strErrMsg, int IdPorcentajeCalificarControl)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int Sumtotal = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT ISNULL(sum([ValorPorcentajeCalificarControl]),0) as Sumtotal FROM [Parametrizacion].[PorcentajeCalificarControl]"
                    + " WHERE IdPorcentajeCalificarControl <> {0}", IdPorcentajeCalificarControl);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                Sumtotal = Convert.ToInt32(dtCaracOut.Rows[0]["Sumtotal"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el valor total del peso. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return Sumtotal;
        }
        public int mtdValidarValorPorcentaje(ref string strErrMsg, string strNombreVariable)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int CantVariable = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT([IdPorcentajeCalificarControl]) as CantVariable FROM [Parametrizacion].[PorcentajeCalificarControl] where NombrePorcentajeCalificarControl = '{0}'",strNombreVariable);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                CantVariable = Convert.ToInt32(dtCaracOut.Rows[0]["CantVariable"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el valor total del peso. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return CantVariable;
        }
        public bool mtdConsultarPorcentajes(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdPorcentajeCalificarControl],[NombrePorcentajeCalificarControl],[ValorPorcentajeCalificarControl]"
                    + " FROM [Parametrizacion].[PorcentajeCalificarControl]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los porcentajes de la calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdatePorcentaje(clsDTOPorcentajeCalificacion objPorcentaje, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Parametrizacion].[PorcentajeCalificarControl] SET [NombrePorcentajeCalificarControl] = '{0}'" +
                    ",[ValorPorcentajeCalificarControl] = {1}" +
                    " WHERE IdPorcentajeCalificarControl = {2}",
                    objPorcentaje.strNombrePorcentajeCalificarControl, objPorcentaje.intValorPorcentajeCalificarControl, objPorcentaje.intIdPorcentajeCalificarControl);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el porcentaje de calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarPorcentajesxVariable(ref DataTable dtCaracOut, ref string strErrMsg, ref string variable)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            //Calificacion
            try
            {

                List<SqlParameter> parametros = new List<SqlParameter>()
               {
                   new SqlParameter() { ParameterName = "@DescripcionVariable", SqlDbType = SqlDbType.VarChar, Value =  variable.Trim().ToString() }
               };
               DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[VariableCalificacionSeleccionarDescripcion]", parametros);

                //strConsulta = string.Format("SELECT [ValorPorcentajeCalificarControl]"
                //    + " FROM [Parametrizacion].[PorcentajeCalificarControl]"
                //    + " where NombrePorcentajeCalificarControl = '{0}'", variable);

                //cDataBase.conectar();
                dtCaracOut = dt;
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los porcentajes de la calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCalificacionControl(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdCalificacionControl],[NombreEscala],[ValorEscala],[LimiteInferior],[LimiteSuperior]"+
                ",[PromedioInferior],[PromedioSuperior],[DesviacionProbabilidad],[DesviacionImpacto],[Color]"
                    + " FROM [Parametrizacion].[CalificacionControl]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las calificaciones del control. [{0}]", ex.Message);
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