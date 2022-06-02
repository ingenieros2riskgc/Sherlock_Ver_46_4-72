using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaImpacto
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaImpacto(clsDTOParaImpacto Impacto, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[5];
                parameter = new OleDbParameter("@NombreImpacto1", OleDbType.VarChar);
                parameter.Value = Impacto.strNombreImpacto1;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@NombreImpacto2", OleDbType.VarChar);
                parameter.Value = Impacto.strNombreImpacto2;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@NombreImpacto3", OleDbType.VarChar);
                parameter.Value = Impacto.strNombreImpacto3;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@NombreImpacto4", OleDbType.VarChar);
                parameter.Value = Impacto.strNombreImpacto4;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@NombreImpacto5", OleDbType.VarChar);
                parameter.Value = Impacto.strNombreImpacto5;
                parameters[4] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarImpacto", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar el impacto. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}