using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaTipoEventoOperativo
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaTipoEventoOperativo(clsDTOParaTipoEventoOperativo TipoEventoOp, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@NombreTipoEventoOperativo", OleDbType.VarChar);
                parameter.Value = TipoEventoOp.strNombreTipoRiesgoOperativo;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdTipoEventoOperativo", OleDbType.Numeric);
                parameter.Value = TipoEventoOp.intIdTipoRiesgoOperativo;
                parameters[1] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarTipoEventoOperativo", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al insertar las Tipo Evento Operativo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}