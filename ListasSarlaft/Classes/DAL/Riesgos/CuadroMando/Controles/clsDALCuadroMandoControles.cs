using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALCuadroMandoControles
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        public DataTable LoadInfoReporteControles(ref string strErrMsg, int IdJerarquia)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            string strSelect = string.Empty;
            string strFrom = string.Empty;
            string strWhere = string.Empty;
            string strOrder = string.Empty;
            //string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            //string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                if(IdJerarquia == 0)
                {
                    strSelect = "SELECT LTRIM(RTRIM(ISNULL(RCtrl.CodigoControl, ''))) AS CodigoControl," +
                    "RCtrl.IdControl," +
                "LTRIM(RTRIM(ISNULL(RCtrl.NombreControl, ''))) AS NombreControl," +
                "LTRIM(RTRIM(ISNULL(PCCtrl.NombreEscala, ''))) AS NombreEscala " +
                ",ResponsableControl.NombreHijo ";

                    strFrom = "FROM Riesgos.Control AS RCtrl " +
                    "LEFT JOIN Parametrizacion.Periodicidad AS PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad " +
                    "LEFT JOIN Parametrizacion.Test AS PT ON PT.IdTest = RCtrl.IdTest " +
                    "LEFT JOIN Parametrizacion.CalificacionControl AS PCCtrl ON PCCtrl.IdCalificacionControl = RCtrl.IdCalificacionControl " +
                    "LEFT JOIN Parametrizacion.MitigaControl AS PMCtrl ON PMCtrl.IdMitiga = RCtrl.IdMitiga " +
                    "LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableControl ON ResponsableControl.idHijo = RCtrl.Responsable ";

                    strWhere = string.Empty;

                    strOrder = "ORDER BY RCtrl.IdControl";
                }else
                {
                    strSelect = "SELECT LTRIM(RTRIM(ISNULL(RCtrl.CodigoControl, ''))) AS CodigoControl," +
                    "RCtrl.IdControl," +
                "LTRIM(RTRIM(ISNULL(RCtrl.NombreControl, ''))) AS NombreControl," +
                "LTRIM(RTRIM(ISNULL(PCCtrl.NombreEscala, ''))) AS NombreEscala " +
                ",ResponsableControl.NombreHijo ";

                    strFrom = "FROM Riesgos.Control AS RCtrl " +
                    "LEFT JOIN Parametrizacion.Periodicidad AS PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad " +
                    "LEFT JOIN Parametrizacion.Test AS PT ON PT.IdTest = RCtrl.IdTest " +
                    "LEFT JOIN Parametrizacion.CalificacionControl AS PCCtrl ON PCCtrl.IdCalificacionControl = RCtrl.IdCalificacionControl " +
                    "LEFT JOIN Parametrizacion.MitigaControl AS PMCtrl ON PMCtrl.IdMitiga = RCtrl.IdMitiga " +
                    "LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableControl ON ResponsableControl.idHijo = RCtrl.Responsable ";

                    strWhere = string.Format(" WHERE ResponsableControl.idHijo = {0}", IdJerarquia);

                    strOrder = "ORDER BY RCtrl.IdControl";
                }
                

                strConsulta = string.Format("{0} {1} {2} {3}", strSelect, strFrom, strWhere, strOrder);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable LoadInfoReporteControlesxJerarquias(ref string strErrMsg, string IdJerarquias)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            string strSelect = string.Empty;
            string strFrom = string.Empty;
            string strWhere = string.Empty;
            string strOrder = string.Empty;
            //string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            //string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                
                    strSelect = "SELECT LTRIM(RTRIM(ISNULL(RCtrl.CodigoControl, ''))) AS CodigoControl," +
                    "RCtrl.IdControl," +
                "LTRIM(RTRIM(ISNULL(RCtrl.NombreControl, ''))) AS NombreControl," +
                "LTRIM(RTRIM(ISNULL(PCCtrl.NombreEscala, ''))) AS NombreEscala "+
                "ResponsableControl.NombreHijo";

                    strFrom = "FROM Riesgos.Control AS RCtrl " +
                    "LEFT JOIN Parametrizacion.Periodicidad AS PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad " +
                    "LEFT JOIN Parametrizacion.Test AS PT ON PT.IdTest = RCtrl.IdTest " +
                    "LEFT JOIN Parametrizacion.CalificacionControl AS PCCtrl ON PCCtrl.IdCalificacionControl = RCtrl.IdCalificacionControl " +
                    "LEFT JOIN Parametrizacion.MitigaControl AS PMCtrl ON PMCtrl.IdMitiga = RCtrl.IdMitiga " +
                    "LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS ResponsableControl ON ResponsableControl.idHijo = RCtrl.Responsable ";

                    strWhere = string.Format(" WHERE ResponsableControl.idHijo in ({0})", IdJerarquias);
                strWhere = strWhere.Replace(",)", ")");
                strOrder = "ORDER BY RCtrl.IdControl";
                


                strConsulta = string.Format("{0} {1} {2} {3}", strSelect, strFrom, strWhere, strOrder);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable GetAllControles(ref string strErrMsg)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            //string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            //string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                string strSelect = "SELECT COUNT(IdControl) as CantControls";

                string strFrom = "FROM Riesgos.Control ";
                string strWhere = string.Empty;

                string strOrder = string.Empty;

                strConsulta = string.Format("{0} {1} {2} {3}", strSelect, strFrom, strWhere, strOrder);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable GetCantRiesgos(ref string strErrMsg, int IdControl)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Variables
            try
            {
                string strSelect = "SELECT COUNT(IdRiesgo) as CantRiesgo ";
                string strFrom = "FROM [Riesgos].[ControlesRiesgo] ";
                string strWhere = string.Format("where IdControl = {0}", IdControl);

                string strOrder = string.Empty;

                strConsulta = string.Format("{0} {1} {2} {3}", strSelect, strFrom, strWhere, strOrder);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

    }
}