using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ListasSarlaft.Classes
{
    public class clsDALVariableCalificacionControl
    {
        public int mtdValorEscala(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int ValorEscala = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT MAX(ValorEscala) as ValorEscala from [Parametrizacion].[CalificacionControl]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                ValorEscala = Convert.ToInt32(dtCaracOut.Rows[0]["ValorEscala"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el valor de la escala. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return ValorEscala;
        }

        public bool mtdInsertarVariableCalificacion(clsDTOVariableCalificacionControl objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                 List<SqlParameter> parametros = new List<SqlParameter>()
                {
                   new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value =  objVariable.intIdCalificacionControl },
                   new SqlParameter() { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value = objVariable.strDescripcionVariable},
                   new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  objVariable.intIdUsuario },
                   new SqlParameter() { ParameterName = "@Activo", SqlDbType = SqlDbType.Int, Value =  1 },
                   new SqlParameter() { ParameterName = "@Peso", SqlDbType = SqlDbType.Float, Value =  objVariable.FlPesoVariable },
                   new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };

                cDatabase.EjecutarSPParametrosReturnInteger("[Riesgos].[VariableCalificacionInsertarActualizar]", parametros);

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la variable de calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public DataTable ObtenerVariables(int idVariable)
        {
            cDataBase cDatabase = new cDataBase();
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
               {
                   new SqlParameter() { ParameterName = "@IdVariable", SqlDbType = SqlDbType.Int, Value =  idVariable }
               };
                DataTable dt = cDatabase.EjecutarSPParametrosReturnDatatable("[Riesgos].[VariableCalificacionSeleccionar]", parametros);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public bool mtdConsultarVariablesCalificacionControl(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdVariableCalificacionControl],[DescripcionVariable],PVCC.[UsuarioCreacion]"
                    + ",PVCC.[FechaCreacion],[Activo],Users.Usuario, [PesoVariable]"
                    + " FROM [Parametrizacion].[VariableCalificacionControl] as PVCC"
                    + " inner join Listas.Usuarios as Users on Users.IdUsuario = PVCC.UsuarioCreacion"
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables de calificaión control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarVariablesActivas(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdVariableCalificacionControl],[DescripcionVariable],PVCC.[UsuarioCreacion]" +
                    ", PVCC.[FechaCreacion],[Activo], Users.Usuario, ISNULL(PPCC.IdPorcentajeCalificarControl, 0) as IdPorcentajeCalificarControl" +
                     " FROM[Parametrizacion].[VariableCalificacionControl] as PVCC" +
                     " inner join Listas.Usuarios as Users on Users.IdUsuario = PVCC.UsuarioCreacion" +
                     " left outer join[Parametrizacion].[PorcentajeCalificarControl] as PPCC on PPCC.NombrePorcentajeCalificarControl = PVCC.DescripcionVariable" +
                     " where Activo = 1 and ISNULL(PPCC.IdPorcentajeCalificarControl, 0) = 0");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables de calificaión control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarVariablesActivasControl(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdVariableCalificacionControl],[DescripcionVariable],PVCC.[UsuarioCreacion]" +
                    ", PVCC.[FechaCreacion],[Activo], Users.Usuario, PesoVariable " +
                     " FROM[Parametrizacion].[VariableCalificacionControl] as PVCC" +
                     " inner join Listas.Usuarios as Users on Users.IdUsuario = PVCC.UsuarioCreacion" +
                     " where Activo = 1 ");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables de calificaión control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateVariableCalificacionControl(clsDTOVariableCalificacionControl objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                   new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value =  objVariable.intIdCalificacionControl },
                   new SqlParameter() { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value = objVariable.strDescripcionVariable},
                   new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  objVariable.intIdUsuario },
                   new SqlParameter() { ParameterName = "@Activo", SqlDbType = SqlDbType.Int, Value =  1 },
                   new SqlParameter() { ParameterName = "@Peso", SqlDbType = SqlDbType.Float, Value =  objVariable.FlPesoVariable },
                   new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                cDatabase.EjecutarSPParametrosReturnInteger("[Riesgos].[VariableCalificacionInsertarActualizar]", parametros);

                //strConsulta = string.Format("UPDATE [Parametrizacion].[VariableCalificacionControl] SET [DescripcionVariable] = '{0}'" +
                //    " WHERE IdVariableCalificacionControl = {1}",
                //    objVariable.strDescripcionVariable, objVariable.intIdCalificacionControl);

                //cDatabase.conectar();
                //cDatabase.ejecutarQuery(strConsulta);
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
        public bool mtdLoadValoresLimites(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [LimiteInferior],[LimiteSuperior]"
                    + " FROM [Parametrizacion].[CalificacionControl] order by LimiteSuperior");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los limites de las variables de calificaión control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public int mtdValorMaximo(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int Sumtotal = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(IdVariableCalificacionControl) as Sumtotal from [Parametrizacion].[VariableCalificacionControl] where Activo = 1");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                Sumtotal = Convert.ToInt32(dtCaracOut.Rows[0]["Sumtotal"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el valor total de registros. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return Sumtotal;
        }
        public int mtdCantVariablexControl(ref string strErrMsg, string nombreVar)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int Sumtotal = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(IdControlxVariable) AS CANTVAR FROM [Riesgos].[ControlxVariable] WHERE NombreVariable = '{0}'", nombreVar);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                Sumtotal = Convert.ToInt32(dtCaracOut.Rows[0]["CANTVAR"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cantidad de controles por la variable. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return Sumtotal;
        }

        public bool mtdActivarVariableCalificacionControl(clsDTOVariableCalificacionControl objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                   new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value =  objVariable.intIdCalificacionControl },
                   new SqlParameter() { ParameterName = "@Activo", SqlDbType = SqlDbType.Int, Value = objVariable.booActivo},
                   new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                cDatabase.EjecutarSPParametrosReturnInteger("[Riesgos].[VariableCalificacionActualizarEstado]", parametros);
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