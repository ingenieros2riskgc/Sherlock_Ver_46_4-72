using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaTipoRiesgoOperativo
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaTipoRiesgoOperativo(clsDTOParaTipoRiesgoOperativo TipoRiesgoOp, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[3];
                parameter = new OleDbParameter("@NombreTipoRiesgoOperativo", OleDbType.VarChar);
                parameter.Value = TipoRiesgoOp.strNombreFactorRiesgoOperativo;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdFactorRiesgoOperativo", OleDbType.Numeric);
                parameter.Value = TipoRiesgoOp.intIdFactorRiesgoOperativo;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@IdTipoRiesgoOperativo", OleDbType.Numeric);
                parameter.Value = TipoRiesgoOp.intIdTipoRiesgoOperativo;
                parameters[2] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarTipoRiesgoOperativo", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar el tipo riego operativo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}