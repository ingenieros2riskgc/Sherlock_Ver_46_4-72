using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALCalculoEfectividadControl
    {
        public bool mtdConsultarValorProcentaje(ref DataTable dtCaracOut, ref string strErrMsg, string DescripcionVariable)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("select ValorPorcentajeCalificarControl "
                    + "from Parametrizacion.PorcentajeCalificarControl "
                    + "where NombrePorcentajeCalificarControl = '{0}'", DescripcionVariable);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el valor del porcentaje de la variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarMaxPeso(ref DataTable dtCaracOut, ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT MAX(PasoCategoria) as Peso "
                    + "FROM [Parametrizacion].[vwCategoriasvsVariable] "
                    + "where IdVariable = {0}", IdVariable);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el valor del peso de la categoria. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarMinPeso(ref DataTable dtCaracOut, ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT MIN(PasoCategoria) as Peso "
                    + "FROM [Parametrizacion].[vwCategoriasvsVariable] "
                    + "where IdVariable = {0}", IdVariable);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el valor del peso de la categoria. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCuadroCalificacion(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdCalificacionControl],[NombreEscala],[LimiteInferior],[LimiteSuperior]"
                + " FROM [Parametrizacion].[CalificacionControl]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el cuadro de calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateCuadorCalificacion(clsDTOCalificacionControl objCalificacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Parametrizacion].[CalificacionControl] SET[LimiteInferior] = {0} "+
                    ",[LimiteSuperior] = {1}" +
                    " WHERE IdCalificacionControl = {2} ",
                    objCalificacion.intLimiteInferior, objCalificacion.intLimiteSuperior, objCalificacion.intIdCalificacionControl);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la variable de calificación control. [{0}]", ex.Message);
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