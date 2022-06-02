using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALfrecuenciaMedicion
    {
        public bool mtdConsultarFrecuenciaMedicion(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdFrecuenciaMedicion],[FrecuenciaMedicion]"
                    + " FROM [Riesgos].[RiesgosIndicadoresFrecuenciaMedicion]"
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las frecuencias de medición. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarDetalleFrecuencia(ref DataTable dtCaracOut, ref string strErrMsg, int IdFrecuencia)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdDetalleFecruencia],[Descripcion]"
                    + " FROM [Riesgos].[RiesgosIndicadoresDetalleFrecuencia]"
                    + " where [IdFrecuenia] = {0}", IdFrecuencia
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los detalles de las frecuencias de medición. [{0}]", ex.Message);
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