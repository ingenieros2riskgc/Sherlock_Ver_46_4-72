using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using ListasSarlaft.Classes.Utilidades;

namespace ListasSarlaft.Classes
{
    public class clsDALLUpdRoles
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        private cEncriptacion cEncrypt = new cEncriptacion();
        private clsToolsSeguridad toolsSeguridad = new clsToolsSeguridad();
        #endregion Variables Globales
        public bool mtActualizarRol(String NombreRol, String IdRol)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                //Users.strContrasenaEncriptada = toolsSeguridad.mtdEncriptarContrasena(Users.strContrasenaEncriptada);
                //string Contraseña = toolsSeguridad.mtdEncriptarContrasena("Sherlock+");
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@IdRol", OleDbType.Numeric);
                parameter.Value = IdRol;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@NombreRol", OleDbType.VarChar);
                parameter.Value = NombreRol;
                parameters[1] = parameter;
                /*parameter = new OleDbParameter("@login", OleDbType.Numeric);
                parameter.Value = 0;
                parameters[10] = parameter;*/
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Listas.spSEGURIDADActualizarRoles", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //strErrMsg = string.Format("Error reiniciar la contraseña del usuario. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}