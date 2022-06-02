using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cHistoricoRiesgo : cPropiedades
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;
        private string[] strMonths = new string[12] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" },
            strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
        #endregion Variables Globales

        public DataTable loadInfoHistoricoRiesgo(String CodigoRiesgo, String NombreRiesgo, string strFechaHistoricoDesde, string strFechaHistoricoHasta, String CadenaValor, String Macroproceso, String Proceso, String Subproceso, String ClasificacionRiesgo, String ClasificacionGeneralRiesgo, String RiesgoInherente, String RiesgoResidual, String Empresa)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strCondicion = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                #region Filtros
                #region CodigoRiesgo
                if (!string.IsNullOrEmpty(CodigoRiesgo))
                    strCondicion = "WHERE (CodigoRiesgo = '" + CodigoRiesgo + "') ";
                #endregion CodigoRiesgo

                #region NombreRiesgo
                if (!string.IsNullOrEmpty(NombreRiesgo))
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (NombreRiesgo LIKE '%" + NombreRiesgo + "%') ";
                    else
                        strCondicion += "AND (NombreRiesgo LIKE '%" + NombreRiesgo + "%') ";
                }
                #endregion NombreRiesgo

                #region Fechas Desde y Hasta
                if (!string.IsNullOrEmpty(strFechaHistoricoDesde))
                {
                    strFechaIni = mtdConvertirFecha(strFechaHistoricoDesde, 1) + " 00:00:00:000";
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (FechaHistorico >= CONVERT(datetime, '" + strFechaIni + "', 120)) ";
                    else
                        strCondicion += "AND (FechaHistorico >= CONVERT(datetime, '" + strFechaIni + "', 120)) ";
                }
                if (!string.IsNullOrEmpty(strFechaHistoricoHasta))
                {
                    strFechaFin = mtdConvertirFecha(strFechaHistoricoHasta, 2) + " 23:59:59:998";
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (FechaHistorico <= CONVERT(datetime, '" + strFechaFin + "', 120)) ";
                    else
                        strCondicion += "AND (FechaHistorico <= CONVERT(datetime, '" + strFechaFin + "', 120)) ";
                }
                #endregion Fechas Desde y Hasta

                #region CadenaValor
                if (CadenaValor != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (CadenaValor = '" + CadenaValor + "') ";
                    else
                        strCondicion += "AND (CadenaValor = '" + CadenaValor + "') ";
                }
                #endregion

                #region MacroProceso
                if (Macroproceso != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (Macroproceso = '" + Macroproceso + "') ";
                    else
                        strCondicion += "AND (Macroproceso = '" + Macroproceso + "') ";
                }
                #endregion

                #region Proceso
                if (Proceso != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (Proceso = '" + Proceso + "') ";
                    else
                        strCondicion += "AND (Proceso = '" + Proceso + "') ";
                }
                #endregion

                #region Subproceso
                if (Subproceso != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (Subproceso = '" + Subproceso + "') ";
                    else
                        strCondicion += "AND (Subproceso = '" + Subproceso + "') ";
                }
                #endregion

                #region Clasificacion Riesgo
                if (ClasificacionRiesgo != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (ClasificacionRiesgo = '" + ClasificacionRiesgo + "') ";
                    else
                        strCondicion += "AND (ClasificacionRiesgo = '" + ClasificacionRiesgo + "') ";
                }
                #endregion

                #region Clasificacion General
                if (ClasificacionGeneralRiesgo != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (ClasificacionGeneralRiesgo = '" + ClasificacionGeneralRiesgo + "') ";
                    else
                        strCondicion += "AND (ClasificacionGeneralRiesgo = '" + ClasificacionGeneralRiesgo + "') ";
                }
                #endregion

                #region Riesgo Inherente
                if (RiesgoInherente != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (RiesgoInherente = '" + RiesgoInherente + "') ";
                    else
                        strCondicion += "AND (RiesgoInherente = '" + RiesgoInherente + "') ";
                }
                #endregion

                #region Riesgo Residual
                if (RiesgoResidual != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (RiesgoResidual = '" + RiesgoResidual + "') ";
                    else
                        strCondicion += "AND (RiesgoResidual = '" + RiesgoResidual + "') ";
                }
                #endregion

                #region Empresa
                if (Empresa != "---")
                {
                    if (string.IsNullOrEmpty(strCondicion))
                        strCondicion = "WHERE (Empresa = '" + Empresa + "') ";
                    else
                        strCondicion += "AND (Empresa = '" + Empresa + "') ";
                }
                #endregion

                #endregion Filtros

                strConsulta =
                   //string.Format("SELECT DATENAME(MM,LTRIM(RTRIM(CONVERT(varchar, FechaHistorico, 107)))) + ' ' + CAST(YEAR(LTRIM(RTRIM(CONVERT(varchar, FechaHistorico, 107)))) AS VARCHAR(4)) AS FechaHistorico, LTRIM(RTRIM(CodigoRiesgo)) AS CodigoRiesgo, LTRIM(RTRIM(NombreRiesgo)) AS NombreRiesgo, LTRIM(RTRIM(ResponsableRiesgo)) AS ResponsableRiesgo, LTRIM(RTRIM(CONVERT(varchar, FechaRegistroRiesgo, 107))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ClasificacionRiesgo)) AS ClasificacionRiesgo, LTRIM(RTRIM(ClasificacionGeneralRiesgo)) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ClasificacionParticularRiesgo)) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(TipoRiesgoOperativo)) AS TipoRiesgoOperativo, LTRIM(RTRIM(CadenaValor)) AS CadenaValor, LTRIM(RTRIM(Macroproceso)) AS Macroproceso, LTRIM(RTRIM(Proceso)) AS Proceso, LTRIM(RTRIM(Subproceso)) AS Subproceso, LTRIM(RTRIM(Actividad)) AS Actividad, LTRIM(RTRIM(Frecuencia)) AS FrecuenciaInherente, LTRIM(RTRIM(CodigoFrecuenciaInherente)) AS CodigoFrecuenciaInherente, LTRIM(RTRIM(Impacto)) AS ImpactoInherente, LTRIM(RTRIM(CodigoImpactoInherente)) AS CodigoImpactoInherente, LTRIM(RTRIM(RiesgoInherente)) AS RiesgoInherente, LTRIM(RTRIM(CodigoRiesgoInherente)) AS CodigoRiesgoInherente, LTRIM(RTRIM(FrecuenciaResidual)) AS FrecuenciaResidual, LTRIM(RTRIM(CodigoFrecuenciaResidual)) AS CodigoFrecuenciaResidual, LTRIM(RTRIM(ImpactoResidual)) AS ImpactoResidual, LTRIM(RTRIM(CodigoImpactoResidual)) AS CodigoImpactoResidual, LTRIM(RTRIM(RiesgoResidual)) AS RiesgoResidual, LTRIM(RTRIM(CodigoRiesgoResidual)) AS CodigoRiesgoResidual, LTRIM(RTRIM(CodigoControl)) AS CodigoControl, LTRIM(RTRIM(NombreControl)) AS NombreControl, LTRIM(RTRIM(ResponsableControl)) AS ResponsableControl, LTRIM(RTRIM((CASE WHEN CONVERT(varchar, FechaRegistroControl, 107) = 'Jan 01, 1900' THEN '' ELSE CONVERT(varchar, FechaRegistroControl, 107) END))) AS FechaRegistroControl, LTRIM(RTRIM(NombrePeriodicidad)) AS NombrePeriodicidad, LTRIM(RTRIM(NombreTest)) AS NombreTest, LTRIM(RTRIM(NombreClaseControl)) AS NombreClaseControl, LTRIM(RTRIM(NombreTipoControl)) AS NombreTipoControl, LTRIM(RTRIM(NombreResponsableExperiencia)) AS NombreResponsableExperiencia, LTRIM(RTRIM(NombreDocumentacion)) AS NombreDocumentacion, LTRIM(RTRIM(NombreResponsabilidad)) AS NombreResponsabilidad, LTRIM(RTRIM(NombreEscala)) AS NombreEscala, LTRIM(RTRIM(NombreMitiga)) AS NombreMitiga, LTRIM(RTRIM(Empresa)) AS Empresa, LTRIM(RTRIM(JUSTIFICACION)) AS Justificacion FROM Riesgos.HistoricoRiesgo {0} ORDER BY CodigoRiesgo", strCondicion);
                   // string.Format("SELECT DATENAME(MM,LTRIM(RTRIM(CONVERT(varchar, FechaHistorico, 107)))) + ' ' + CAST(YEAR(LTRIM(RTRIM(CONVERT(varchar, FechaHistorico, 107)))) AS VARCHAR(4)) AS FechaHistorico, LTRIM(RTRIM(CodigoRiesgo)) AS CodigoRiesgo, LTRIM(RTRIM(NombreRiesgo)) AS NombreRiesgo, LTRIM(RTRIM(ResponsableRiesgo)) AS ResponsableRiesgo, LTRIM(RTRIM(CONVERT(varchar, FechaRegistroRiesgo, 107))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ClasificacionRiesgo)) AS ClasificacionRiesgo, LTRIM(RTRIM(ClasificacionGeneralRiesgo)) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ClasificacionParticularRiesgo)) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(TipoRiesgoOperativo)) AS TipoRiesgoOperativo, LTRIM(RTRIM(CadenaValor)) AS CadenaValor, LTRIM(RTRIM(Macroproceso)) AS Macroproceso, LTRIM(RTRIM(Proceso)) AS Proceso, LTRIM(RTRIM(Subproceso)) AS Subproceso, LTRIM(RTRIM(Actividad)) AS Actividad, LTRIM(RTRIM(Frecuencia)) AS Frecuencia, LTRIM(RTRIM(Impacto)) AS Impacto, LTRIM(RTRIM(RiesgoInherente)) AS RiesgoInherente, LTRIM(RTRIM(RiesgoResidual)) AS RiesgoResidual, LTRIM(RTRIM(CodigoControl)) AS CodigoControl, LTRIM(RTRIM(NombreControl)) AS NombreControl, LTRIM(RTRIM(ResponsableControl)) AS ResponsableControl, LTRIM(RTRIM((CASE WHEN CONVERT(varchar, FechaRegistroControl, 107) = 'Jan 01, 1900' THEN '' ELSE CONVERT(varchar, FechaRegistroControl, 107) END))) AS FechaRegistroControl, LTRIM(RTRIM(NombrePeriodicidad)) AS NombrePeriodicidad, LTRIM(RTRIM(NombreTest)) AS NombreTest, LTRIM(RTRIM(NombreClaseControl)) AS NombreClaseControl, LTRIM(RTRIM(NombreTipoControl)) AS NombreTipoControl, LTRIM(RTRIM(NombreResponsableExperiencia)) AS NombreResponsableExperiencia, LTRIM(RTRIM(NombreDocumentacion)) AS NombreDocumentacion, LTRIM(RTRIM(NombreResponsabilidad)) AS NombreResponsabilidad, LTRIM(RTRIM(NombreEscala)) AS NombreEscala, LTRIM(RTRIM(NombreMitiga)) AS NombreMitiga, LTRIM(RTRIM(Empresa)) AS Empresa, LTRIM(RTRIM(JUSTIFICACION)) AS Justificacion FROM Riesgos.HistoricoRiesgo {0} ORDER BY CodigoRiesgo", strCondicion);
                   string.Format("SELECT DATENAME(MM,LTRIM(RTRIM(CONVERT(varchar, FechaHistorico, 107)))) + ' ' + CAST(YEAR(LTRIM(RTRIM(CONVERT(varchar, FechaHistorico, 107)))) AS VARCHAR(4)) AS FechaHistorico, LTRIM(RTRIM(CodigoRiesgo)) AS CodigoRiesgo, LTRIM(RTRIM(NombreRiesgo)) AS NombreRiesgo, LTRIM(RTRIM(ResponsableRiesgo)) AS ResponsableRiesgo, LTRIM(RTRIM(CONVERT(varchar, FechaRegistroRiesgo, 107))) AS FechaRegistroRiesgo, LTRIM(RTRIM(ClasificacionRiesgo)) AS ClasificacionRiesgo, LTRIM(RTRIM(ClasificacionGeneralRiesgo)) AS ClasificacionGeneralRiesgo, LTRIM(RTRIM(ClasificacionParticularRiesgo)) AS ClasificacionParticularRiesgo, LTRIM(RTRIM(TipoRiesgoOperativo)) AS TipoRiesgoOperativo, LTRIM(RTRIM(CadenaValor)) AS CadenaValor, LTRIM(RTRIM(Macroproceso)) AS Macroproceso, LTRIM(RTRIM(Proceso)) AS Proceso, LTRIM(RTRIM(Subproceso)) AS Subproceso, LTRIM(RTRIM(Actividad)) AS Actividad, LTRIM(RTRIM(Frecuencia)) AS FrecuenciaInherente, LTRIM(RTRIM(CodigoFrecuenciaInherente)) AS CodigoFrecuenciaInherente, LTRIM(RTRIM(Impacto)) AS ImpactoInherente, LTRIM(RTRIM(CodigoImpactoInherente)) AS CodigoImpactoInherente, LTRIM(RTRIM(RiesgoInherente)) AS RiesgoInherente, LTRIM(RTRIM(CodigoRiesgoInherente)) AS CodigoRiesgoInherente, LTRIM(RTRIM(FrecuenciaResidual)) AS FrecuenciaResidual, LTRIM(RTRIM(CodigoFrecuenciaResidual)) AS CodigoFrecuenciaResidual, LTRIM(RTRIM(ImpactoResidual)) AS ImpactoResidual, LTRIM(RTRIM(CodigoImpactoResidual)) AS CodigoImpactoResidual, LTRIM(RTRIM(RiesgoResidual)) AS RiesgoResidual, LTRIM(RTRIM(CodigoRiesgoResidual)) AS CodigoRiesgoResidual, LTRIM(RTRIM(Riesgos.HistoricoRiesgo.CodigoControl)) AS CodigoControl, LTRIM(RTRIM(Riesgos.HistoricoRiesgo.NombreControl)) AS NombreControl, LTRIM(RTRIM(ResponsableControl)) AS ResponsableControl, LTRIM(RTRIM((CASE WHEN CONVERT(varchar, FechaRegistroControl, 107) = 'Jan 01, 1900' THEN '' ELSE CONVERT(varchar, FechaRegistroControl, 107) END))) AS FechaRegistroControl, LTRIM(RTRIM(NombrePeriodicidad)) AS NombrePeriodicidad, LTRIM(RTRIM(NombreTest)) AS NombreTest, LTRIM(RTRIM(NombreClaseControl)) AS NombreClaseControl, LTRIM(RTRIM(NombreTipoControl)) AS NombreTipoControl, LTRIM(RTRIM(NombreResponsableExperiencia)) AS NombreResponsableExperiencia, LTRIM(RTRIM(NombreDocumentacion)) AS NombreDocumentacion, LTRIM(RTRIM(NombreResponsabilidad)) AS NombreResponsabilidad, LTRIM(RTRIM(NombreEscala)) AS NombreEscala, LTRIM(RTRIM(NombreMitiga)) AS NombreMitiga, LTRIM(RTRIM(Empresa)) AS Empresa, LTRIM(RTRIM(JUSTIFICACION)) AS Justificacion,RCV.NombreVariable,RCV.NombreCategoria FROM Riesgos.HistoricoRiesgo INNER JOIN Riesgos.Control as Rcntrl on Rcntrl.CodigoControl = Riesgos.HistoricoRiesgo.CodigoControl left join Riesgos.ControlxVariable as RCV on RCV.IdControl = Rcntrl.IdControl {0} ORDER BY CodigoRiesgo", strCondicion);
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

        #region Carga DROPDOWNLISTS
        public DataTable loadDDLCadenaValor()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT DISTINCT CadenaValor FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(CadenaValor)) <> '')");
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

        public DataTable loadDDLMacroproceso()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT DISTINCT Macroproceso FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(Macroproceso)) <> '')");
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

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Se sobrecarga metodo para filtrar el DropDownList de Macroproceso teniendo en cuenta la cadena de valor seleccionada por el usuario
        public DataTable loadDDLMacroproceso(string CadenaValor)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                //Modificacion Heber Jessid Correal 02/04/2018 Se filtra el DropDownList de MacroProceso teniendo en cuenta la cadena de valor seleccionada por el usuario
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(string.Format("SELECT DISTINCT Macroproceso FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(Macroproceso)) <> '') AND (LTRIM(RTRIM(CadenaValor)) = '{0}')", CadenaValor));
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

        public DataTable loadDDLProceso()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT DISTINCT Proceso FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(Proceso)) <> '')");
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

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Se sobrecarga metodo para filtrar el DropDownList de Proceso teniendo en cuenta el Macroproceso seleccionado por el usuario
        public DataTable loadDDLProceso(string MacroProceso)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                //Modificacion Heber Jessid Correal 03/04/2018 Se filtra el DropDownList de Proceso teniendo en cuenta el MacroProceso seleccionado por el usuario
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(string.Format("SELECT DISTINCT Proceso FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(Proceso)) <> '') AND (LTRIM(RTRIM(Macroproceso)) = '{0}')", MacroProceso));
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

        public DataTable loadDDLSubproceso()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT DISTINCT Subproceso FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(Subproceso)) <> '')");
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

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Se sobrecarga metodo para filtrar el DropDownList de Subproceso teniendo en cuenta el Proceso seleccionado por el usuario
        public DataTable loadDDLSubproceso(string Proceso)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(string.Format("SELECT DISTINCT Subproceso FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(Subproceso)) <> '') AND (LTRIM(RTRIM(Proceso)) = '{0}')", Proceso));
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

        public DataTable loadDDLRiesgoGlobal()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT DISTINCT ClasificacionRiesgo FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(ClasificacionRiesgo)) <> '')");
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

        public DataTable loadDDLClasGeneral()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT DISTINCT ClasificacionGeneralRiesgo FROM Riesgos.HistoricoRiesgo WHERE (LTRIM(RTRIM(ClasificacionGeneralRiesgo)) <> '')");
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
        #endregion

        #region Metodos privados

        private string mtdConvertirFecha(string strFechaIn, int intTipoDia)
        {
            string strFechaOut = string.Empty, strMes = string.Empty, strDia = string.Empty;
            string[] strFechaPartida = strFechaIn.Split('-');

            #region Asignar Mes
            for (int i = 0; i < 12; i++)
            {
                if (strFechaPartida[0].ToString().ToUpper() == strMeses[i].ToString() ||
                    strFechaPartida[0].ToString().ToUpper() == strMonths[i].ToString())
                {
                    if ((i + 1) <= 9)
                        strMes = string.Format("0{0}", (i + 1).ToString());
                    else
                        strMes = (i + 1).ToString().Trim();
                    break;
                }
            }
            #endregion Asignar Mes

            #region Asignar Dia
            switch (intTipoDia)
            {
                case 1:
                    strDia = "01";
                    break;
                case 2:
                    switch (strMes)
                    {
                        case "02":
                            strDia = "28";
                            break;
                        case "01":
                        case "03":
                        case "05":
                        case "07":
                        case "08":
                        case "10":
                        case "12":
                            strDia = "31";
                            break;
                        case "04":
                        case "06":
                        case "09":
                        case "11":
                            strDia = "30";
                            break;

                    }
                    break;
            }
            #endregion Asignar Dia

            if (!string.IsNullOrEmpty(strMes))
            {
                strFechaOut = string.Format("{0}-{1}-{2}", strFechaPartida[1].ToString().Trim(), strMes, strDia);
            }

            return strFechaOut;
        }
        #endregion
    }
}