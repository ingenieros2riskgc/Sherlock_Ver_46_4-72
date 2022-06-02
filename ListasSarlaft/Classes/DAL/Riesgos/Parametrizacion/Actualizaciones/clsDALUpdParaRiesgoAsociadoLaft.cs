using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaRiesgoAsociadoLaft
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaRiesgoAsociadoLaft(clsDTOParaRiesgoAsociadoLaft RiesgoAsociadoLaft, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@NombreRiesgoAsociadoLA", OleDbType.VarChar);
                parameter.Value = RiesgoAsociadoLaft.strNombreRiesgoAsociadoLA;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdRiesgoAsociadoLA", OleDbType.Numeric);
                parameter.Value = RiesgoAsociadoLaft.intIdRiesgoAsociadoLA;
                parameters[1] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarRiesgoAsociadoLaft", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar el riesgo asociado laft. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}