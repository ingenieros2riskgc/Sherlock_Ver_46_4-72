using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALUpdParaPeriodicidad
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdActualizacionParaPeriodicidad(clsDTOParaPeriodicidad Periodicidad, ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@NombrePeriodicidad", OleDbType.VarChar);
                parameter.Value = Periodicidad.strNombrePeriodicidad;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdPeriodicidad", OleDbType.VarChar);
                parameter.Value = Periodicidad.intIdPeriodicidad;
                parameters[1] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Listas.spRIESGOParaActualizacionPeriodicidad", parameters);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al actualizar la periodicidad. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}