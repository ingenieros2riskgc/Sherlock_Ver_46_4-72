using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAnulacionRiesgoDAL
    {
        public bool mtdEventosRiesgos(ref DataTable dtCaracOut, ref string strErrMsg, int IdRiesgos)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT Riesgos.EventoRiesgo.IdEventoRiesgo, Riesgos.EventoRiesgo.IdRiesgo, "+
"Riesgos.Eventos.IdEvento, Riesgos.Eventos.CodigoEvento, Riesgos.Eventos.DescripcionEvento,"+
"Riesgos.EventoRiesgo.FechaRegistro"+
" FROM Riesgos.EventoRiesgo"+
" INNER JOIN Riesgos.Riesgo ON Riesgos.EventoRiesgo.IdRiesgo = Riesgos.Riesgo.IdRiesgo"+
" INNER JOIN Riesgos.Eventos on Riesgos.Eventos.IdEvento = Riesgos.EventoRiesgo.IdEvento"+
" WHERE(Riesgo.IdRiesgo = {0})", IdRiesgos);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al cosultar la cantidad de eventos del riesgo. [{0}]", ex.Message);
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