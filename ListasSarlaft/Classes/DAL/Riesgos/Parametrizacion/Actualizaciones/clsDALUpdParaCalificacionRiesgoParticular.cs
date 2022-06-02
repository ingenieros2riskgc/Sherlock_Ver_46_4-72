using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaCalificacionRiesgoParticular
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaCalificacionRiesgoParticular(clsDTOParaCalificacionRiesgoParticular CalificacionRiesgoParticular, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[5];
                parameter = new OleDbParameter("@NombreClasificacionParticularRiesgo", OleDbType.VarChar);
                parameter.Value = CalificacionRiesgoParticular.strNombreClasificacionParticularRiesgo;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Numeric);
                parameter.Value = CalificacionRiesgoParticular.intIdUsuario;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@FechaRegistro", OleDbType.Date);
                parameter.Value = CalificacionRiesgoParticular.dtFechaRegistro;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@IdClasificacionRiesgo", OleDbType.Numeric);
                parameter.Value = CalificacionRiesgoParticular.intIdClasificacionRiesgo;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@IdClasificacionParticularRiesgo", OleDbType.Numeric);
                parameter.Value = CalificacionRiesgoParticular.intIdClasificacionParticularRiesgo;
                parameters[4] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarCalificacionRiesgoParticular", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar la calificacion del riesgo particular. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}