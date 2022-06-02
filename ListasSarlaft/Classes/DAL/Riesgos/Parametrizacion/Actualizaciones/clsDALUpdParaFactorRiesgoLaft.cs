using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaFactorRiesgoLaft
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizaeParaFactorRiesgoLaft(clsDTOParaFactorRiesgoLaft FactorRiesgoLaft, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@NombreFactorRiesgoLAFT", OleDbType.VarChar);
                parameter.Value = FactorRiesgoLaft.strNombreFactorRiesgoLAFT;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdFactorRiesgoLAFT", OleDbType.Numeric);
                parameter.Value = FactorRiesgoLaft.intIdFactorRiesgoLAFT;
                parameters[1] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarFactorRiesgoLaft", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al insertar el factor de riesgo laft. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}