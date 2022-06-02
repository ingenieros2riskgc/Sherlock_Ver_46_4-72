using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtPeriodicidad
    {
        /// <summary>
        /// Realiza la consulta para traer todos los Periodos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarPeriodos(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[Id], PP.[NombrePeriodo], PP.[FechaRegistro], PP.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblPeriodicidad] PP " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PP.[IdUsuario] = LU.[IdUsuario] " );

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Periodos. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        public bool mtdConsultarDetallePeriodo(clsPeriodicidad objPeriodicidad, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PDP.[Id], PDP.[IdPeriodo], PDP.[Nombre], PDP.[Posicion], PDP.[IdUsuario], LU.[Usuario] NombreUsuario, PDP.[FechaRegistro] " +
                    "FROM [Procesos].[tblDetallePeriodo] PDP " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PDP.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PDP.[IdPeriodo] = {0} " +
                    "ORDER BY PDP.[Posicion] ", objPeriodicidad.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Periodos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}