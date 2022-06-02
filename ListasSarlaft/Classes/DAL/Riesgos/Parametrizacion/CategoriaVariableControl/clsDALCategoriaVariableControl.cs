using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALCategoriaVariableControl
    {
        public bool mtdInsertarCategoriaVariableControl(clsDTOCategoriasVariableControl objCategoria, ref string strErrMsg)
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
                   new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value =  objCategoria.intIdCategoriaVariableControl },
                   new SqlParameter() { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value = objCategoria.strDescripcionCategoria},
                   new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  objCategoria.intIdUsuario },
                   new SqlParameter() { ParameterName = "@Activo", SqlDbType = SqlDbType.Int, Value =  1 },
                   new SqlParameter() { ParameterName = "@Peso", SqlDbType = SqlDbType.Int, Value =  objCategoria.intPesoCategoria },
                   new SqlParameter() { ParameterName = "@IdVariable", SqlDbType = SqlDbType.Int, Value =  objCategoria.IdVariable },
                   new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                cDatabase.EjecutarSPParametrosReturnInteger("[Riesgos].[CategoriaCalificacionInsertarActualizar]", parametros);
                //strConsulta = string.Format("INSERT INTO [Parametrizacion].[CategoriaVariableControl]([DescripcionCategoria],[PasoCategoria],[UsuarioCreacion],[FechaCreacion],[Activo])" +
                //    "VALUES('{0}',{1},{2},'{3}',1) ",
                //    objCategoria.strDescripcionCategoria, objCategoria.intPesoCategoria, objCategoria.intIdUsuario, objCategoria.dtFechaRegistro);

                //cDatabase.conectar();
                //cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la categoría de la variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCategoriaVariablesControl(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                   new SqlParameter() { ParameterName = "@IdCategoria", SqlDbType = SqlDbType.Int, Value =  0 }
                };
                
                dtCaracOut = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[CategoriaCalificacionSeleccionar]", parametros);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las categorias de las variables de calificaión control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCategoriaActivas(ref DataTable dtCaracOut, ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {

                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                   new SqlParameter() { ParameterName = "@IdVariable", SqlDbType = SqlDbType.Int, Value =  IdVariable },
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Riesgos].[CategoriaCalificacionSeleccionarVariable]", parametros);

                //strConsulta = string.Format("SELECT PCVC.[IdCategoriaVariableControl], PCVC.[DescripcionCategoria]"
                //    + " FROM [Parametrizacion].[CategoriaVariableControl] as PCVC"
                //    + " inner join Listas.Usuarios as Users on Users.IdUsuario = PCVC.UsuarioCreacion"
                //    + " INNER JOIN Parametrizacion.VariablexCategoriaControl as PVCC on PVCC.IdCategoria = PCVC.IdCategoriaVariableControl"
                //    + " where PCVC.[Activo] = 1 and PVCC.IdVariable = {0}", IdVariable);

                //cDataBase.conectar();
                //dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                dtCaracOut = dt;
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las categorias de las variables de calificaión control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdActivarVariableCalificacionControl(clsDTOCategoriasVariableControl objCategoria, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Parametrizacion].[CategoriaVariableControl] SET [Activo] = {0}" +
                    " WHERE IdCategoriaVariableControl = {1}",
                    objCategoria.booActivo, objCategoria.intIdCategoriaVariableControl);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                if(objCategoria.booActivo == 0)
                {
                    strConsulta = string.Format("DELETE from Parametrizacion.VariablexCategoriaControl WHERE IdCategoria = {0}", objCategoria.intIdCategoriaVariableControl);
                    cDatabase.ejecutarQuery(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la categoria de la variable de calificación control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateCategoriaVariableControl(clsDTOCategoriasVariableControl objCategoria, ref string strErrMsg)
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
                   new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value =  objCategoria.intIdCategoriaVariableControl },
                   new SqlParameter() { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value = objCategoria.strDescripcionCategoria},
                   new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  objCategoria.intIdUsuario },
                   new SqlParameter() { ParameterName = "@Activo", SqlDbType = SqlDbType.Int, Value =  1 },
                   new SqlParameter() { ParameterName = "@Peso", SqlDbType = SqlDbType.Int, Value =  objCategoria.intPesoCategoria },
                   new SqlParameter() { ParameterName = "@IdVariable", SqlDbType = SqlDbType.Int, Value =  objCategoria.IdVariable },
                   new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                cDatabase.EjecutarSPParametrosReturnInteger("[Riesgos].[CategoriaCalificacionInsertarActualizar]", parametros);
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
        public int mtdCantidadTotalPeso(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int Sumtotal = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT ISNULL(sum([PasoCategoria]),0) as Sumtotal FROM [Parametrizacion].[CategoriaVariableControl] where Activo = 1");

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
        public int mtdPesoCategoria(ref string strErrMsg, int IdCategoria)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int PesoCategoria = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [PasoCategoria] FROM [Parametrizacion].[CategoriaVariableControl] where IdCategoriaVariableControl ={0}",
                    IdCategoria);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                PesoCategoria = Convert.ToInt32(dtCaracOut.Rows[0]["PasoCategoria"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el peso de la categoria. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return PesoCategoria;
        }
    }
}