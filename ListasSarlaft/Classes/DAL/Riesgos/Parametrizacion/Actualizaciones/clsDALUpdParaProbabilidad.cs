using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaProbabilidad
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizarParaProbabilidad(clsDTOParaProbabilidad Probabilidad, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[5];
                parameter = new OleDbParameter("@NombreProbabilidad1", OleDbType.VarChar);
                parameter.Value = Probabilidad.strNombreProbabilidad1;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@NombreProbabilidad2", OleDbType.VarChar);
                parameter.Value = Probabilidad.strNombreProbabilidad2;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@NombreProbabilidad3", OleDbType.VarChar);
                parameter.Value = Probabilidad.strNombreProbabilidad3;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@NombreProbabilidad4", OleDbType.VarChar);
                parameter.Value = Probabilidad.strNombreProbabilidad4;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@NombreProbabilidad5", OleDbType.VarChar);
                parameter.Value = Probabilidad.strNombreProbabilidad5;
                parameters[4] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Parametrizacion.spRIESGOSActualizarProbabilidad", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar la frecuencia. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}