using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALParametrizacionAsignarCategoriaVariable
    {
        public bool mtdConsultarCategoriaxVariablesControl(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PCVC.[IdCategoriaVariableControl], PCVC.[DescripcionCategoria], PCVC.[PasoCategoria], PCVC.[UsuarioCreacion]"
                    + ", PCVC.[FechaCreacion], PCVC.[Activo], Users.Usuario, PVCC.IdVariable"
                    + " FROM [Parametrizacion].[CategoriaVariableControl] as PCVC"
                    + " inner join Listas.Usuarios as Users on Users.IdUsuario = PCVC.UsuarioCreacion"
                    + " LEFT JOIN Parametrizacion.VariablexCategoriaControl as PVCC on PVCC.IdCategoria = PCVC.IdCategoriaVariableControl"
                    + " WHERE ISNULL(PVCC.IdVariableCategoria, 0) = 0 and Activo = 1");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
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
        public bool mtdInsertarCategoriaxVariablesControl(clsDTOParametrizacionAsignarCategoriaVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                //strConsulta = string.Format("INSERT INTO [Parametrizacion].[VariablexCategoriaControl] ([IdVariable],[IdCategoria]"+
                //",[UsuarioCreacion],[FechaCreacion])" +
                //    "VALUES({0},{1},{2},'{3}') ",
                //    objVariable.intIdVariable, objVariable.intIdCategoria, objVariable.intIdUsuario, objVariable.dtFechaRegistro);
                strConsulta = string.Format("INSERT INTO [Parametrizacion].[VariablexCategoriaControl] ([IdVariable],[IdCategoria]" +
                ",[UsuarioCreacion],[FechaCreacion])" +
                    "VALUES({0},{1},{2},GETDATE()) ",
                    objVariable.intIdVariable, objVariable.intIdCategoria, objVariable.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la asignación de la categoria a la variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCategoriasAsignadas(ref DataTable dtCaracOut, ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdVariableCategoria],[IdVariable],[DescripcionVariable],[IdCategoria]"
                    +",[DescripcionCategoria],[UsuarioCreacion],[Usuario],[FechaCreacion]"
                    + " FROM [Parametrizacion].[vwCategoriasvsVariable]"
                    + " WHERE IdVariable = {0}", IdVariable);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Categorias Asignadas. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdActializarCategoriaxVariablesControl(clsDTOParametrizacionAsignarCategoriaVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Parametrizacion].[VariablexCategoriaControl]" +
                    " SET [IdCategoria] = {0} "+
                    " WHERE IdVariableCategoria = {1}",
                    objVariable.intIdCategoria, objVariable.intIdVariableCategoria);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la asignación de la categoria a la variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdDesasociarVariable(int IdER, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Parametrizacion].[VariablexCategoriaControl] where IdVariableCategoria = {0}", IdER);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al desasociar la variable. [{0}]", ex.Message);
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