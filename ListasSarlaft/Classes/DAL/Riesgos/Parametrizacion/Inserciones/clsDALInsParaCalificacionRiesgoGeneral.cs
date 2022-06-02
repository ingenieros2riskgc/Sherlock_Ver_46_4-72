using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALInsParaCalificacionRiesgoGeneral
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdInsertarParaCalificacionRiesgoGeneral(clsDTOParaCalificacionRiesgoGeneral CalificacionRiesgoGeneral, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[4];
                parameter = new OleDbParameter("@IdClasificacionRiesgo", OleDbType.Numeric);
                parameter.Value = CalificacionRiesgoGeneral.intIdClasificacionRiesgo;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@NombreClasificacionGeneralRiesgo", OleDbType.VarChar);
                parameter.Value = CalificacionRiesgoGeneral.strNombreClasificacionGeneralRiesgo;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Numeric);
                parameter.Value = CalificacionRiesgoGeneral.intIdUsuario;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@FechaRegistro", OleDbType.Date);
                parameter.Value = CalificacionRiesgoGeneral.dtFechaRegistro;
                parameters[3] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSInsertaCalificacionRiesgoGeneral", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al insertar la calificacion del riesgo general. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}