using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaTipoRecursoPlanAccion
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaTipoRecursoPlanAccion(clsDTOParaTipoRecursoPlanAccion TipoRecurso, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@NombreTipoRecursoPlanAccion", OleDbType.VarChar);
                parameter.Value = TipoRecurso.strNombreTipoRecursoPlanAccion;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdTipoRecursoPlanAccion", OleDbType.Numeric);
                parameter.Value = TipoRecurso.intIdTipoRecursoPlanAccion;
                parameters[1] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarTipoRecursoPlanEvaluacion", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar el tipo recurso del plan de accion. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}