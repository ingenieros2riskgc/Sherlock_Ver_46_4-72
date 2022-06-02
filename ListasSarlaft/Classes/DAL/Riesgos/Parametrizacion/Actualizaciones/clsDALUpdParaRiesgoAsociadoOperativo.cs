using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaRiesgoAsociadoOperativo
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaRiesgoAsociadoOperativo(clsDTOParaRiesgoAsociadoOperativo RiesgoAsociadoOp, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@NombreRiesgoAsociadoOperativo", OleDbType.VarChar);
                parameter.Value = RiesgoAsociadoOp.strNombreRiesgoAsociadoOperativo;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdTipoEventoOperativo", OleDbType.Numeric);
                parameter.Value = RiesgoAsociadoOp.intIdTipoEventoOperativo;
                parameters[1] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarRiesgoAsociadoOperativo", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar los riesgos asociados. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}