using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaTipoPrueba
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaTipoPrueba(clsDTOParaTipoPrueba TipoPrueba, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@NombreTipoPruebaPlanEvaluacion", OleDbType.VarChar);
                parameter.Value = TipoPrueba.strNombreTipoPruebaPlanEvaluacion;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdTipoPruebaPlanEvaluacion", OleDbType.Numeric);
                parameter.Value = TipoPrueba.intIdTipoPruebaPlanEvaluacion;
                parameters[1] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarTipoPrueba", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar el Tipo de Prueba del plan de evaluación. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}