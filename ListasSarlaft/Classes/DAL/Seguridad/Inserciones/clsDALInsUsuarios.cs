using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes.Utilidades;

namespace ListasSarlaft.Classes
{
    public class clsDALInsUsuarios
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        private cEncriptacion cEncrypt = new cEncriptacion();
        private clsToolsSeguridad toolsSeguridad = new clsToolsSeguridad();
        #endregion Variables Globales

        public bool mtdRegistrarUsuario(clsDTOUsuarios Users,ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                Users.strContrasenaEncriptada = toolsSeguridad.mtdEncriptarContrasena(Users.strContrasenaEncriptada);
                #region Creacion Consulta
                parameters = new OleDbParameter[11];
                parameter = new OleDbParameter("@IdRol", OleDbType.Numeric);
                parameter.Value = Users.intIdRol;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@NumeroDocumento", OleDbType.VarChar);
                parameter.Value = Users.strNumeroDocumento;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@Nombres", OleDbType.VarChar);
                parameter.Value = Users.strNombres;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@Apellidos", OleDbType.VarChar);
                parameter.Value = Users.strApellidos;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@Usuario", OleDbType.VarChar);
                parameter.Value = Users.strUsuario;
                parameters[4] = parameter;
                parameter = new OleDbParameter("@Contrasena", OleDbType.VarChar);
                parameter.Value = Users.strContrasenaEncriptada;
                parameters[5] = parameter;
                parameter = new OleDbParameter("@Bloqueado", OleDbType.Numeric);
                parameter.Value = Users.intBloqueado;
                parameters[6] = parameter;
                parameter = new OleDbParameter("@IdJerarquia", OleDbType.Numeric);
                parameter.Value = Users.intIdJerarquia;
                parameters[7] = parameter;
                parameter = new OleDbParameter("@IdMacroprocesoU", OleDbType.Numeric);
                parameter.Value = Users.intIdMacroprocesoU;
                parameters[8] = parameter;
                parameter = new OleDbParameter("@IdProcesoU", OleDbType.Numeric);
                parameter.Value = Users.intIdProcesoU;
                parameters[9] = parameter;
                parameter = new OleDbParameter("@VerTodosProcesos", OleDbType.Numeric);
                parameter.Value = Users.intVerTodosProcesos;
                parameters[10] = parameter;
                /*parameter = new OleDbParameter("@login", OleDbType.Numeric);
                parameter.Value = 0;
                parameters[10] = parameter;*/
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Listas.spSEGURIDADRegistroUsuarios", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al insertar el nuevo usuario. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}