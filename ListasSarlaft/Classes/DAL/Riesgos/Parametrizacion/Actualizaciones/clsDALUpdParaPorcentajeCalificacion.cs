using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;


namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaPorcentajeCalificacion
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizacionParaPorcentajeCalificacion(clsDTOParaPorcentajeCalificacion PorcentajeCalificacion, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[5];
                parameter = new OleDbParameter("@ValorPorcentajeCalificarControl1", OleDbType.Numeric);
                parameter.Value = PorcentajeCalificacion.intValorPorcentajeCalificarControl1;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@ValorPorcentajeCalificarControl2", OleDbType.Numeric);
                parameter.Value = PorcentajeCalificacion.intValorPorcentajeCalificarControl2;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@ValorPorcentajeCalificarControl3", OleDbType.Numeric);
                parameter.Value = PorcentajeCalificacion.intValorPorcentajeCalificarControl3;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@ValorPorcentajeCalificarControl4", OleDbType.Numeric);
                parameter.Value = PorcentajeCalificacion.intValorPorcentajeCalificarControl4;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@ValorPorcentajeCalificarControl5", OleDbType.Numeric);
                parameter.Value = PorcentajeCalificacion.intValorPorcentajeCalificarControl5;
                parameters[4] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSPorcentajeCalicarControl", parameters);
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