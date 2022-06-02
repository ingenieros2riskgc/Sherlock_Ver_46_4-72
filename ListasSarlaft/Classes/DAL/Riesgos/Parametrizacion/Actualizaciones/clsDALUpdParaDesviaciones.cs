using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaDesviaciones
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaDesviaciones(clsDTOParaDesviaciones ParaDesviaciones, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[8];
                parameter = new OleDbParameter("@DesviacionProbabilidad1", OleDbType.Numeric);
                parameter.Value = ParaDesviaciones.intDesviacionProbabilidad1;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@DesviacionProbabilidad2", OleDbType.Numeric);
                parameter.Value = ParaDesviaciones.intDesviacionProbabilidad2;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@DesviacionProbabilidad3", OleDbType.Numeric);
                parameter.Value = ParaDesviaciones.intDesviacionProbabilidad3;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@DesviacionProbabilidad4", OleDbType.Numeric);
                parameter.Value = ParaDesviaciones.intDesviacionProbabilidad4;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@DesviacionImpacto1", OleDbType.Numeric);
                parameter.Value = ParaDesviaciones.intDesviacionImpacto1;
                parameters[4] = parameter;
                parameter = new OleDbParameter("@DesviacionImpacto2", OleDbType.Numeric);
                parameter.Value = ParaDesviaciones.intDesviacionImpacto2;
                parameters[5] = parameter;
                parameter = new OleDbParameter("@DesviacionImpacto3", OleDbType.Numeric);
                parameter.Value = ParaDesviaciones.intDesviacionImpacto3;
                parameters[6] = parameter;
                parameter = new OleDbParameter("@DesviacionImpacto4", OleDbType.Numeric);
                parameter.Value = ParaDesviaciones.intDesviacionImpacto4;
                parameters[7] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarDesviciones", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar el porcentaje de calificacion del control. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}