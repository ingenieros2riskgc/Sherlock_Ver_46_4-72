using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaCalificacionRiesgo
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaCalificacionRiesgo(clsDTOParaCalificacionRiesgo CalificacionRiesgo, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[4];
                parameter = new OleDbParameter("@NombreClasificacionRiesgo", OleDbType.VarChar);
                parameter.Value = CalificacionRiesgo.strNombreClasificacionRiesgo;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Numeric);
                parameter.Value = CalificacionRiesgo.intIdClasificacionRiesgo;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@FechaRegistro", OleDbType.Date);
                parameter.Value = CalificacionRiesgo.dtFechaRegistro;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@IdClasificacionRiesgo", OleDbType.Numeric);
                parameter.Value = CalificacionRiesgo.intIdClasificacionRiesgo;
                parameters[3] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarCalificacionRiesgo", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar el estado del plan de accion. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}