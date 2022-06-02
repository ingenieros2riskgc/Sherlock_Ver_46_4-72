using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALCuadroMandoRiesgos
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private string[] strMonths = new string[12] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" },
            strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
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
                        strMes = (i + 1).ToString();
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
                strFechaOut = string.Format("{0}-{1}-{2}", strFechaPartida[1].ToString(), strMes, strDia);
            }

            return strFechaOut;
        }
        public DataTable LoadInfoReporteRiesgos(ref string strErrMsg, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                if (objFiltros.dtFechaHistoricoInicial != default(DateTime))
                {
                    condicion = string.Empty;
                    #region Filtro Cadena Valor
                    if (objFiltros.intIdCadenaValor.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdCadenaValor = {0}", objFiltros.intIdCadenaValor.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, objFiltros.intIdCadenaValor.ToString());
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdMacroproceso = {0}", objFiltros.intIdMacroProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, objFiltros.intIdMacroProceso.ToString());
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (objFiltros.intIdProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdProceso = {0}", objFiltros.intIdProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, objFiltros.intIdProceso.ToString());
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdSubProceso = {0}", objFiltros.intIdSubProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, objFiltros.intIdSubProceso.ToString());
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionRiesgo = {0}", objFiltros.intRiesgoGlobal.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.strAreaRiesgo.ToString() != "")
                    {
                        /*strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());*/
                        condicion =
                        string.Format("{0} and ResponsableRiesgo = '{1}'", condicion, objFiltros.strAreaRiesgo);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(objFiltros.dtFechaHistoricoInicial.ToString()))
                    {
                        strFechaIni = objFiltros.dtFechaHistoricoInicial.ToShortDateString() + " 00:00";
                        strFechaFin = objFiltros.dtFechaHistoricoFinal.ToShortDateString() + " 23:59";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta
                    #region Filtros por Objetivos Estrategicos
                    if (objFiltros.intIdObjetivo != 0)
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + objFiltros.intIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidad, RHR.IdImpacto FROM Riesgos.HistoricoRiesgo RHR {2} {0} )  AS InfoHistorico ", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} GROUP BY InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual ", strSelHistorico, strFromHistorico);
                    }
                    else
                    {
                        /*string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidad, InfoHistorico.IdImpacto");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidad, RHR.IdImpacto FROM Riesgos.HistoricoRiesgo RHR {0} {1})  AS InfoHistorico", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY InfoHistorico.IdProbabilidad,InfoHistorico.IdImpacto", strSelHistorico, strFromHistorico);*/
                        string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1}) AS InfoHistorico GROUP BY InfoHistorico.IdProbabilidadResidual,InfoHistorico.IdImpactoResidual", strSelHistorico, strFromHistorico);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    /*string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                    string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {0} {1}", strFrom, condicion);

                    strConsulta = string.Format("{0} {1}) AS InfoHistorico GROUP BY InfoHistorico.IdProbabilidadResidual,InfoHistorico.IdImpactoResidual", strSelHistorico, strFromHistorico);*/
                }
                else
                {
                    #region Filtros de Consulta
                    if (objFiltros.intIdCadenaValor.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdCadenaValor = " + objFiltros.intIdCadenaValor.ToString() + ")";
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdMacroproceso = " + objFiltros.intIdMacroProceso.ToString() + ")";
                    if (objFiltros.intIdProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdProceso = " + objFiltros.intIdProceso.ToString() + ")";
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdSubProceso = " + objFiltros.intIdSubProceso.ToString() + ")";
                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        condicion = string.Format("{0} AND (Riesgos.Riesgo.IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.intArea.ToString() != "0")
                    {
                        /*strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                            */
                        strFrom = "INNER JOIN Parametrizacion.JerarquiaOrganizacional as PJO on PJO.idHijo =  Riesgos.Riesgo.IdResponsableRiesgo " +
                                "INNER JOIN Parametrizacion.DetalleJerarquiaOrg as PDJO on PDJO.idDetalleJerarquiaOrg = PJO.idHijo ";
                        condicion =
                            string.Format("{0} and PDJO.IdArea = {1}", condicion, objFiltros.intArea.ToString());
                    }
                    #endregion Areas
                    #endregion Filtros de Consulta
                    #region Filtros por Objetivos Estrategicos
                    if (objFiltros.intIdObjetivo != 0)
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + objFiltros.intIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = Riesgos.Riesgo.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo  {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} GROUP BY Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual", strSelNormal, strFromNormal);
                    }else
                    {
                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual");
                        string strFromNormal = string.Format("FROM	Riesgos.Riesgo {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual"
                            + " order by IdProbabilidadResidual, IdImpactoResidual", strSelNormal, strFromNormal);
                        
                    }
                    #endregion Filtros por Objetivos Estrategicos


                }
                //strErrMsg = strConsulta;
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
        public DataTable LoadInfoReporteRiesgosSaro(ref string strErrMsg, clsDTOCuadroMandoConsolidadoFiltros objFiltros, ref string strQuery)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                if (objFiltros.dtFechaHistoricoInicial != default(DateTime) && objFiltros.dtFechaHistoricoInicial != default(DateTime))
                {
                    condicion = string.Empty;
                    #region Filtro Cadena Valor
                    if (objFiltros.intIdCadenaValor.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdCadenaValor = {0}", objFiltros.intIdCadenaValor.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, objFiltros.intIdCadenaValor.ToString());
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdMacroproceso = {0}", objFiltros.intIdMacroProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, objFiltros.intIdMacroProceso.ToString());
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (objFiltros.intIdProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdProceso = {0}", objFiltros.intIdProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, objFiltros.intIdProceso.ToString());
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdSubProceso = {0}", objFiltros.intIdSubProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, objFiltros.intIdSubProceso.ToString());
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionRiesgo = {0}", objFiltros.intRiesgoGlobal.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.intArea.ToString() != "0")
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (objFiltros.dtFechaHistoricoInicial != default(DateTime))
                    {
                        /*strFechaIni = mtdConvertirFecha(objFiltros.dtFechaHistoricoInicial.ToString(), 1) + " 00:00";
                        strFechaFin = mtdConvertirFecha(objFiltros.dtFechaHistoricoFinal.ToString(), 2) + " 23:59";*/
                        strFechaIni = objFiltros.dtFechaHistoricoInicial.ToShortDateString() + " 00:00";
                        strFechaFin = objFiltros.dtFechaHistoricoFinal.ToShortDateString() + " 23:59";
                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 103) AND CONVERT(datetime, '{1}', 103)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 103) AND CONVERT(datetime, '{1}', 103)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta
                    string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                    string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {0} {1}", strFrom, condicion);

                    strConsulta = string.Format("{0} {1}) AS InfoHistorico GROUP BY InfoHistorico.IdProbabilidadResidual,InfoHistorico.IdImpactoResidual", strSelHistorico, strFromHistorico);
                }
                else
                {
                    #region Filtros de Consulta
                    if (objFiltros.intIdCadenaValor.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdCadenaValor = " + objFiltros.intIdCadenaValor.ToString() + ")";
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdMacroproceso = " + objFiltros.intIdMacroProceso.ToString() + ")";
                    if (objFiltros.intIdProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdProceso = " + objFiltros.intIdProceso.ToString() + ")";
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdSubProceso = " + objFiltros.intIdSubProceso.ToString() + ")";
                    if (objFiltros.intIdClasificacionGeneral.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdClasificacionGeneralRiesgo = " + (objFiltros.intIdClasificacionGeneral + ")");
                    #region Filtro Factor Riesgos LAFT
                    if (objFiltros.intIdFactor.ToString() != "0")
                        condicion = condicion + " AND ('" + objFiltros.intIdFactor + "' IN (SELECT COL1 FROM Procesos.FnSplitTable(RR.ListaFactorRiesgoLAFT,'|'))) ";
                    #endregion Filtro Factor Riesgos LAFT
                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        condicion = string.Format("{0} AND (Riesgos.Riesgo.IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.intArea.ToString() != "0")
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                    }
                    #endregion Areas

                    #endregion Filtros de Consulta
                    #region Filtros por Objetivos Estrategicos
                    if (objFiltros.intIdObjetivo.ToString() != "---" && objFiltros.intIdObjetivo.ToString() != "0")
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + objFiltros.intIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT COUNT(RR.IdRiesgo) AS NumeroRegistros, RR.IdProbabilidad, RR.IdImpacto");
                        string strFromNormal = string.Format("FROM Riesgos.Riesgo RR {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} GROUP BY RR.IdProbabilidad, RR.IdImpacto", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual");
                        string strFromNormal = string.Format("FROM	Riesgos.Riesgo {0} {1}", strFrom, condicion);
                        
                        strConsulta = string.Format("{0} {1}  GROUP BY Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual"
                            + " order by IdProbabilidadResidual, IdImpactoResidual", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                }
                strQuery = strConsulta;
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
        public DataTable LoadInfoDetalleRiesgo(ref string strErrMsg, int IdProbabilidadResidual, int IdImpactoResidual)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                condicion = string.Format("WHERE (Riesgos.Riesgo.Anulado = 0) and (Riesgos.Riesgo.IdProbabilidadResidual = {0}) and (Riesgos.Riesgo.IdImpactoResidual = {1})", IdProbabilidadResidual, IdImpactoResidual);
                string strSelNormal = string.Format("SELECT distinct(NombreRiesgoInherente) as NombreRiesgoInherente");
                string strFromNormal = string.Format("FROM Riesgos.Riesgo INNER JOIN Parametrizacion.Probabilidad ON Riesgos.Riesgo.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.RiesgoInherente ON Riesgos.Riesgo.IdProbabilidadResidual = Parametrizacion.RiesgoInherente.IdProbabilidad AND Riesgos.Riesgo.IdImpactoResidual = Parametrizacion.RiesgoInherente.IdImpacto INNER JOIN Parametrizacion.Impacto ON Riesgos.Riesgo.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.Riesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo {0} {1}", strFrom, condicion);

                strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);

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
        public DataTable LoadInfoDetalleRiesgoHistorico(ref string strErrMsg, int IdProbabilidadResidual, int IdImpactoResidual)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                condicion = string.Format("WHERE  (Riesgos.HistoricoRiesgo.IdProbabilidadResidual = {0}) AND (Riesgos.HistoricoRiesgo.IdImpactoResidual = {1})", IdProbabilidadResidual, IdImpactoResidual);
                string strSelNormal = string.Format("SELECT distinct(NombreRiesgoInherente) as NombreRiesgoInherente");
                string strFromNormal = string.Format("FROM Riesgos.HistoricoRiesgo INNER JOIN Parametrizacion.Probabilidad ON Riesgos.HistoricoRiesgo.IdProbabilidadResidual = Parametrizacion.Probabilidad.IdProbabilidad INNER JOIN Parametrizacion.RiesgoInherente ON Riesgos.HistoricoRiesgo.IdProbabilidadResidual = Parametrizacion.RiesgoInherente.IdProbabilidad AND Riesgos.HistoricoRiesgo.IdImpactoResidual = Parametrizacion.RiesgoInherente.IdImpacto INNER JOIN Parametrizacion.Impacto ON Riesgos.HistoricoRiesgo.IdImpactoResidual = Parametrizacion.Impacto.IdImpacto INNER JOIN Parametrizacion.ClasificacionRiesgo ON Riesgos.HistoricoRiesgo.IdClasificacionRiesgo = Parametrizacion.ClasificacionRiesgo.IdClasificacionRiesgo {0} {1}", strFrom, condicion);

                strConsulta = string.Format("{0} {1}", strSelNormal, strFromNormal);

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
        public DataTable LoadInfoReporteRiesgosSarlaft(ref string strErrMsg, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0) and IdClasificacionRiesgo = 2";
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                if (!string.IsNullOrEmpty(objFiltros.dtFechaHistoricoInicial.ToString()) && objFiltros.dtFechaHistoricoInicial.ToString() != "1/1/0001 00:00:00")
                {
                    condicion = string.Empty;
                    #region Filtro Cadena Valor
                    if (objFiltros.intIdCadenaValor.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdCadenaValor = {0}", objFiltros.intIdCadenaValor.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdCadenaValor = {1})", condicion, objFiltros.intIdCadenaValor.ToString());
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdMacroproceso = {0}", objFiltros.intIdMacroProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdMacroproceso = {1})", condicion, objFiltros.intIdMacroProceso.ToString());
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (objFiltros.intIdProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdProceso = {0}", objFiltros.intIdProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdProceso = {1})", condicion, objFiltros.intIdProceso.ToString());
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdSubProceso = {0}", objFiltros.intIdSubProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdSubProceso = {1})", condicion, objFiltros.intIdSubProceso.ToString());
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE RHR.IdClasificacionRiesgo = {0}", objFiltros.intRiesgoGlobal.ToString());
                        else
                            condicion = string.Format("{0} AND (RHR.IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.intArea.ToString() != "0")
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (!string.IsNullOrEmpty(objFiltros.dtFechaHistoricoInicial.ToString()))
                    {
                        strFechaIni = mtdConvertirFecha(objFiltros.dtFechaHistoricoInicial.ToString(), 1) + " 00:00:00:000";
                        strFechaFin = mtdConvertirFecha(objFiltros.dtFechaHistoricoFinal.ToString(), 2) + " 23:59:59:998";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (RHR.FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta
                    string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                    string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {0} {1}", strFrom, condicion);

                    strConsulta = string.Format("{0} {1}) AS InfoHistorico GROUP BY InfoHistorico.IdProbabilidadResidual,InfoHistorico.IdImpactoResidual", strSelHistorico, strFromHistorico);
                }
                else
                {
                    #region Filtros de Consulta
                    if (objFiltros.intIdCadenaValor.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdCadenaValor = " + objFiltros.intIdCadenaValor.ToString() + ")";
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdMacroproceso = " + objFiltros.intIdMacroProceso.ToString() + ")";
                    if (objFiltros.intIdProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdProceso = " + objFiltros.intIdProceso.ToString() + ")";
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                        condicion = condicion + " AND (Riesgos.Riesgo.IdSubProceso = " + objFiltros.intIdSubProceso.ToString() + ")";
                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        condicion = string.Format("{0} AND (Riesgos.Riesgo.IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.intArea.ToString() != "0")
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON Riesgos.Riesgo.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                    }
                    #endregion Areas
                    #endregion Filtros de Consulta
                    string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual");
                    string strFromNormal = string.Format("FROM	Riesgos.Riesgo {0} {1}", strFrom, condicion);

                    strConsulta = string.Format("{0} {1} GROUP BY Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual"
                        + " order by IdProbabilidadResidual, IdImpactoResidual", strSelNormal, strFromNormal);
                }
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