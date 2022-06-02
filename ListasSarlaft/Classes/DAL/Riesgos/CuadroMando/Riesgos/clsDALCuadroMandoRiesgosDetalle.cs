using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALCuadroMandoRiesgosDetalle
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
        public DataTable LoadInfoReporteDetalleRiesgos(ref string strErrMsg, clsDTOCuadroMandoConsolidadoFiltros objFiltros, string TipoReporte)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Anulado = 0)";
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
                            condicion = string.Format("WHERE ClasificacionRiesgo = '{0}'", objFiltros.strClasificacionGeneral);
                        else
                            condicion = string.Format("{0} AND (ClasificacionRiesgo = '{1}')", condicion, objFiltros.strClasificacionGeneral);
                    }
                    if (objFiltros.strClasificacionGeneral.ToString() != "")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE ClasificacionRiesgo = '{0}'", objFiltros.strClasificacionGeneral);
                        else
                            condicion = string.Format("{0} AND (ClasificacionRiesgo = '{1}')", condicion, objFiltros.strClasificacionGeneral);
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.strAreaRiesgo != "")
                    {
                        /*strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());*/
                        /*strFrom = "INNER JOIN Parametrizacion.JerarquiaOrganizacional as PJO on PJO.idHijo =  Riesgos.Riesgo.IdResponsableRiesgo " +
                            "INNER JOIN Parametrizacion.DetalleJerarquiaOrg as PDJO on PDJO.idDetalleJerarquiaOrg = PJO.idHijo ";*/
                        condicion =
                            string.Format("{0} and ResponsableRiesgo = '{1}'", condicion, objFiltros.strAreaRiesgo);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (objFiltros.dtFechaHistoricoInicial != default(DateTime))
                    {
                        strFechaIni = objFiltros.dtFechaHistoricoInicial.ToShortDateString() + " 00:00";
                        strFechaFin = objFiltros.dtFechaHistoricoFinal.ToShortDateString() + " 23:59";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta
                    if (TipoReporte == "3")
                    {
                        condicion = string.Format("{0} and ListaCausas <> '' and ListaCausas <> '0'", condicion, TipoReporte);
                    }
                    #region Filtros por Objetivos Estrategicos
                    if (objFiltros.intIdObjetivo != 0)
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + objFiltros.intIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = RHR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelHistorico = string.Format("SELECT [CodigoRiesgo],[NombreRiesgo],[CadenaValor],[IdCadenaValor],[Macroproceso],[IdMacroproceso],[Proceso],[IdProceso],[Subproceso],[IdSubProceso]"
                    + ",[FrecuenciaInherente],[CodigoFrecuenciaInherente],[ImpactoInherente],[CodigoImpactoInherente],[IdProbabilidadResidual],[IdImpactoResidual],[ListaCausas]"
                    + ",[IdRiesgo]");
                        string strFromHistorico = string.Format("FROM [Riesgos].[vwCuadroMandoRiesgosRiesgosHistorico] {0} {1}  ", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} ", strSelHistorico, strFromHistorico);
                    }
                    else
                    {
                        /*string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidad, InfoHistorico.IdImpacto");
                        string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidad, RHR.IdImpacto FROM Riesgos.HistoricoRiesgo RHR {0} {1})  AS InfoHistorico", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY InfoHistorico.IdProbabilidad,InfoHistorico.IdImpacto", strSelHistorico, strFromHistorico);*/
                        string strSelNormal = string.Format("SELECT [CodigoRiesgo],[NombreRiesgo],[CadenaValor],[IdCadenaValor],[Macroproceso],[IdMacroproceso],[Proceso],[IdProceso],[Subproceso],[IdSubProceso]"
                    + ",[FrecuenciaInherente],[CodigoFrecuenciaInherente],[ImpactoInherente],[CodigoImpactoInherente],[IdProbabilidadResidual],[IdImpactoResidual],[ListaCausas]"
                    + ",[IdRiesgo]");
                        string strFromNormal = string.Format("FROM [Riesgos].[vwCuadroMandoRiesgosRiesgosHistorico] {0} {1} ", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} ", strSelNormal, strFromNormal);
                    }
                    #endregion Filtros por Objetivos Estrategicos
                    /*string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                    //string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {0} {1} and ListaCausas <> '' and ListaCausas <> '0'", strFrom, condicion);
                    string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {0} {1}", strFrom, condicion);
                    strConsulta = string.Format("{0} {1}) AS InfoHistorico  GROUP BY InfoHistorico.IdProbabilidadResidual,InfoHistorico.IdImpactoResidual", strSelHistorico, strFromHistorico);*/
                    
                }
                else
                {
                    #region Filtros de Consulta
                    if (objFiltros.intIdCadenaValor.ToString() != "0")
                        condicion = condicion + " AND (IdCadenaValor = " + objFiltros.intIdCadenaValor.ToString() + ")";
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                        condicion = condicion + " AND (IdMacroproceso = " + objFiltros.intIdMacroProceso.ToString() + ")";
                    if (objFiltros.intIdProceso.ToString() != "0")
                        condicion = condicion + " AND (IdProceso = " + objFiltros.intIdProceso.ToString() + ")";
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                        condicion = condicion + " AND (IdSubProceso = " + objFiltros.intIdSubProceso.ToString() + ")";
                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        condicion = string.Format("{0} AND (IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.intArea.ToString() != "0")
                    {
                        /*strFrom = "INNER JOIN Procesos.Proceso as PP ON CMRR.IdProceso = PP.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(PP.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                            */
                        /*strFrom = "INNER JOIN Parametrizacion.JerarquiaOrganizacional as PJO on PJO.idHijo =  Riesgos.Riesgo.IdResponsableRiesgo "+
                                    "INNER JOIN Parametrizacion.DetalleJerarquiaOrg as PDJO on PDJO.idDetalleJerarquiaOrg = PJO.idHijo ";*/
                        condicion =
                            string.Format("{0} and [IdArea] = {1}", condicion , objFiltros.intArea.ToString());
                    }

                    
                    #endregion Areas
                    if(TipoReporte == "3")
                    {
                        DataTable causas = LoadRiescosControlesCusas();
                        string Causas = "";
                        for (int i = 0; i < causas.Rows.Count; i++)
                        {

                            Causas= Causas + causas.Rows[0]["a"].ToString().Trim();
                        }

                        Causas = Causas.Replace("|", ",");
                        //string Causas = causas.Rows[0]["a"].ToString().Trim();
                        Causas = Causas.TrimEnd(',');
                        condicion = string.Format("{0} and ListaCausas <> '' and ListaCausas <> '0' AND b.Idcausas not in ({2}) ", condicion, TipoReporte,Causas);
                    }
                    #endregion Filtros de Consulta
                    #region Filtros por Objetivos Estrategicos
                    if (objFiltros.intIdObjetivo != 0)
                    {
                        condicion = condicion + " AND (GOE.IdObjetivo = " + objFiltros.intIdObjetivo + ")";
                        string strInnerJoinObjetivos = "inner join [Riesgos].[ObjetivosRiesgo] as ROR on  ROR.IdRiesgo = CMRR.IdRiesgo"
                        + " inner join Gestion.ObjetivosEstrategicos as GOE on GOE.IdObjetivo = ROR.IdObjetivos";

                        string strSelNormal = string.Format("SELECT [CodigoRiesgo],[NombreRiesgo],[CadenaValor],[IdCadenaValor],[Macroproceso],[IdMacroproceso],[Proceso],[IdProceso],[Subproceso],[IdSubProceso]"
                + ",[FrecuenciaInherente],[CodigoFrecuenciaInherente],[ImpactoInherente],[CodigoImpactoInherente],[IdProbabilidadResidual],[IdImpactoResidual],[ListaCausas]"
                + ",CMRR.[IdRiesgo],[IdArea] ");
                        string strFromNormal = string.Format("FROM [Riesgos].[vwCuadroMandoRiesgosRiesgos] as CMRR  {2} {0} {1}", strFrom, condicion, strInnerJoinObjetivos);

                        strConsulta = string.Format("{0} {1} ", strSelNormal, strFromNormal);
                    }
                    else
                    {
                        /*string strSelNormal = string.Format("SELECT COUNT(Riesgos.Riesgo.IdRiesgo) AS NumeroRegistros, Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual");
                        string strFromNormal = string.Format("FROM	Riesgos.Riesgo {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} GROUP BY Riesgos.Riesgo.IdProbabilidadResidual, Riesgos.Riesgo.IdImpactoResidual"
                            + " order by IdProbabilidadResidual, IdImpactoResidual", strSelNormal, strFromNormal);*/
                        string strSelNormal = string.Format("SELECT distinct CMRR.[CodigoRiesgo],CMRR.[NombreRiesgo],CMRR.[CadenaValor],CMRR.[IdCadenaValor],CMRR.[Macroproceso],CMRR.[IdMacroproceso],CMRR.[Proceso],CMRR.[IdProceso],CMRR.[Subproceso],CMRR.[IdSubProceso]"
                + ",CMRR.[FrecuenciaInherente],CMRR.[CodigoFrecuenciaInherente],CMRR.[ImpactoInherente],CMRR.[CodigoImpactoInherente],CMRR.[IdProbabilidadResidual],CMRR.[IdImpactoResidual],CMRR.[ListaCausas]"
                + ",CMRR.[IdRiesgo],CMRR.[IdArea]");
                        string strFromNormal = string.Format("FROM [Riesgos].[vwCuadroMandoRiesgosRiesgos] as CMRR inner join [Riesgos].[RiesgosCausasvsControles] as b on CMRR.IdRiesgo=b.IdRiesgo {0} {1} ", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} ", strSelNormal, strFromNormal);

                    }
                    #endregion Filtros por Objetivos Estrategicos
                    
                }
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
                //strErrMsg = strConsulta;
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;

           
    }
        public DataTable LoadRiescosControlesCusas()
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = "select ListaCausas as a from [Riesgos].[vwCuadroMandoRiesgosRiesgos]";
            //string strConsulta = "SELECT REPLACE(STRING_AGG(REPLACE(REPLACE(RTRIM(ListaCausas),'|',','),',,',','),',0'),',,',',')as a FROM [Riesgos].[vwCuadroMandoRiesgosRiesgos]";
            try {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            } catch (Exception ex)
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
                condicion = string.Format("WHERE (Riesgos.Riesgo.Anulado = 0) AND (Riesgos.Riesgo.IdProbabilidadResidual = {0}) AND (Riesgos.Riesgo.IdImpactoResidual = {1})", IdProbabilidadResidual, IdImpactoResidual);
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
        public int mtdGetIdRiesgoGlobal(ref string strErrMsg, string riesgo)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int LastId = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdClasificacionRiesgo]  FROM [Parametrizacion].[ClasificacionRiesgo] where NombreClasificacionRiesgo = '{0}'",riesgo);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                LastId = Convert.ToInt32(dtCaracOut.Rows[0]["IdClasificacionRiesgo"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ultimo id registrado. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return LastId;
        }
        public DataTable LoadInfoCantControlRiesgo(ref string strErrMsg, int IdRiesgo)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                condicion = string.Format("WHERE IdRiesgo = {0}", IdRiesgo);
                string strSelNormal = string.Format("SELECT COUNT([IdControlesRiesgo]) as CantControl");
                string strFromNormal = string.Format("FROM [Riesgos].[ControlesRiesgo] {0}", condicion);

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
        public DataTable LoadInfoControlesRiesgo(ref string strErrMsg, int IdRiesgo)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                strConsulta = string.Format("SELECT Riesgos.ControlesRiesgo.IdControlesRiesgo, Riesgos.Control.IdControl, Riesgos.Control.CodigoControl, Riesgos.Control.NombreControl, Parametrizacion.Test.NombreTest, ((((Parametrizacion.ClaseControl.ValorClaseControl) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 1))) + ((Parametrizacion.TipoControl.ValorTipoControl) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 2))) + ((Parametrizacion.ResponsableExperiencia.ValorResponsableExperiencia) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 3))) + ((Parametrizacion.Documentacion.ValorDocumentacion) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 4))) + ((Parametrizacion.Responsabilidad.ValorResponsabilidad) * (SELECT ValorPorcentajeCalificarControl FROM Parametrizacion.PorcentajeCalificarControl WHERE (IdPorcentajeCalificarControl = 5)))) / 100) AS CalificacionControl, Parametrizacion.CalificacionControl.DesviacionProbabilidad, Parametrizacion.CalificacionControl.DesviacionImpacto, Riesgos.Control.IdMitiga, Riesgos.Control.IdCalificacionControl, Parametrizacion.CalificacionControl.Color, Parametrizacion.CalificacionControl.NombreEscala FROM Riesgos.Control INNER JOIN Parametrizacion.ClaseControl ON Riesgos.Control.IdClaseControl = Parametrizacion.ClaseControl.IdClaseControl INNER JOIN Parametrizacion.TipoControl ON Riesgos.Control.IdTipoControl = Parametrizacion.TipoControl.IdTipoControl INNER JOIN Parametrizacion.ResponsableExperiencia ON Riesgos.Control.IdResponsableExperiencia = Parametrizacion.ResponsableExperiencia.IdResponsableExperiencia INNER JOIN Parametrizacion.Documentacion ON Riesgos.Control.IdDocumentacion = Parametrizacion.Documentacion.IdDocumentacion INNER JOIN Parametrizacion.Responsabilidad ON Riesgos.Control.IdResponsabilidad = Parametrizacion.Responsabilidad.IdResponsabilidad INNER JOIN Riesgos.ControlesRiesgo ON Riesgos.Control.IdControl = Riesgos.ControlesRiesgo.IdControl INNER JOIN Parametrizacion.Test ON Riesgos.Control.IdTest = Parametrizacion.Test.IdTest INNER JOIN Parametrizacion.CalificacionControl ON Riesgos.Control.IdCalificacionControl = Parametrizacion.CalificacionControl.IdCalificacionControl WHERE (Riesgos.ControlesRiesgo.IdRiesgo = {0})", IdRiesgo);

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
        public DataTable LoadInfoCantEventolRiesgo(ref string strErrMsg, int IdRiesgo)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                condicion = string.Format("WHERE IdRiesgo = {0}", IdRiesgo);
                /*string strSelNormal = string.Format("SELECT COUNT([IdEventoRiesgo]) as CantEvento");
                string strFromNormal = string.Format(" FROM [Riesgos].[EventoRiesgo] {0}", condicion);*/
                string strSelNormal = string.Format("SELECT COUNT([CodigoEvento]) as CantEvento");
                string strFromNormal = string.Format(" FROM[Riesgos].[vwCuadroMandoRiesgoEventosRiesgos] {0}", condicion);
                

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
        public DataTable LoadInfoGetCausas(ref string strErrMsg, int IdRiesgo, int IdCausa, int IdControl)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                condicion = string.Format("where Idcausas = {0} and IdControl = {1} and IdRiesgo = {2}", IdCausa, IdControl, IdRiesgo);
                string strSelNormal = string.Format("SELECT [IdCausasvsControles]");
                string strFromNormal = string.Format(" FROM [Riesgos].[RiesgosCausasvsControles] {0}", condicion);

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
        public DataTable LoadInfoGetNombreCausas(ref string strErrMsg, int IdCausa)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = string.Empty;
            string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {
                condicion = string.Format("where Idcausas = {0} ", IdCausa);
                string strSelNormal = string.Format("SELECT [NombreCausas]");
                string strFromNormal = string.Format(" FROM [Parametrizacion].[Causas] {0}", condicion);

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
        public DataTable LoadInfoReporteRiesgosSinControl(ref string strErrMsg, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Anulado = 0) and ISNULL(RCR.IdControlesRiesgo,0) = 0";
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
                            condicion = string.Format("WHERE IdCadenaValor = {0}", objFiltros.intIdCadenaValor.ToString());
                        else
                            condicion = string.Format("{0} AND (IdCadenaValor = {1})", condicion, objFiltros.intIdCadenaValor.ToString());
                    }
                    #endregion Filtro Cadena Valor

                    #region Filtro Macroproceso
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE IdMacroproceso = {0}", objFiltros.intIdMacroProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (IdMacroproceso = {1})", condicion, objFiltros.intIdMacroProceso.ToString());
                    }
                    #endregion Filtro Macroproceso

                    #region Filtro Proceso
                    if (objFiltros.intIdProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE IdProceso = {0}", objFiltros.intIdProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (IdProceso = {1})", condicion, objFiltros.intIdProceso.ToString());
                    }
                    #endregion Filtro Proceso

                    #region Filtro SubProceso
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE IdSubProceso = {0}", objFiltros.intIdSubProceso.ToString());
                        else
                            condicion = string.Format("{0} AND (IdSubProceso = {1})", condicion, objFiltros.intIdSubProceso.ToString());
                    }
                    #endregion Filtro SubProceso

                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        if (string.IsNullOrEmpty(condicion))
                            condicion = string.Format("WHERE IdClasificacionRiesgo = {0}", objFiltros.intRiesgoGlobal.ToString());
                        else
                            condicion = string.Format("{0} AND (IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    /*if (objFiltros.intArea.ToString() != "0")
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                    }*/
                    if (objFiltros.strAreaRiesgo != "")
                    {
                        /*strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());*/
                        /*strFrom = "INNER JOIN Parametrizacion.JerarquiaOrganizacional as PJO on PJO.idHijo =  Riesgos.Riesgo.IdResponsableRiesgo " +
                            "INNER JOIN Parametrizacion.DetalleJerarquiaOrg as PDJO on PDJO.idDetalleJerarquiaOrg = PJO.idHijo ";*/
                        condicion =
                            string.Format("{0} and ResponsableRiesgo = '{1}'", condicion, objFiltros.strAreaRiesgo);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (objFiltros.dtFechaHistoricoInicial != default(DateTime))
                    {
                        strFechaIni = objFiltros.dtFechaHistoricoInicial.ToShortDateString() + " 00:00";
                        strFechaFin = objFiltros.dtFechaHistoricoFinal.ToShortDateString() + " 23:59";

                        if (string.IsNullOrEmpty(condicion))
                            strFechaFinal = string.Format(" WHERE (FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        else
                            strFechaFinal = string.Format(" AND (FechaHistorico BETWEEN CONVERT(datetime, '{0}', 120) AND CONVERT(datetime, '{1}', 120)) ", strFechaIni, strFechaFin);
                        condicion += strFechaFinal;
                    }

                    #endregion Fechas Desde y Hasta
                    /*string strSelHistorico = string.Format("SELECT COUNT(InfoHistorico.CodigoRiesgo) AS NumeroRegistros, InfoHistorico.IdProbabilidadResidual, InfoHistorico.IdImpactoResidual");
                    string strFromHistorico = string.Format("FROM (SELECT DISTINCT RHR.CodigoRiesgo, RHR.IdProbabilidadResidual, RHR.IdImpactoResidual FROM Riesgos.HistoricoRiesgo RHR {0} {1}", strFrom, condicion);

                    strConsulta = string.Format("{0} {1}) AS InfoHistorico GROUP BY InfoHistorico.IdProbabilidadResidual,InfoHistorico.IdImpactoResidual", strSelHistorico, strFromHistorico);
                    */
                    string strSelHistorico = string.Format("SELECT [CodigoRiesgo],[NombreRiesgo],[CadenaValor],[IdCadenaValor],[Macroproceso],[IdMacroproceso]"+
                    ",[Proceso],[IdProceso],[Subproceso],[IdSubProceso],[FrecuenciaInherente],[CodigoFrecuenciaInherente],[ImpactoInherente],[CodigoImpactoInherente]"+
                    ",[IdProbabilidadResidual],[IdImpactoResidual],[ListaCausas],[Anulado],RR.[IdRiesgo] as IdRiesgo,[ClasificacionRiesgo],[FechaHistorico]");

                    string strFromHistorico = string.Format("FROM [Riesgos].[vwCuadroMandoRiesgosRiesgosHistorico] as RR LEFT OUTER JOIN[Riesgos].[ControlesRiesgo] AS RCR " +
                        "on RCR.IdRiesgo = RR.IdRiesgo {0} {1}", strFrom, condicion);

                    strConsulta = string.Format("{0} {1} ", strSelHistorico, strFromHistorico);
                }
                else
                {
                    #region Filtros de Consulta
                    if (objFiltros.intIdCadenaValor.ToString() != "0")
                        condicion = condicion + " AND (IdCadenaValor = " + objFiltros.intIdCadenaValor.ToString() + ")";
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                        condicion = condicion + " AND (IdMacroproceso = " + objFiltros.intIdMacroProceso.ToString() + ")";
                    if (objFiltros.intIdProceso.ToString() != "0")
                        condicion = condicion + " AND (IdProceso = " + objFiltros.intIdProceso.ToString() + ")";
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                        condicion = condicion + " AND (IdSubProceso = " + objFiltros.intIdSubProceso.ToString() + ")";
                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        condicion = string.Format("{0} AND (IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.intArea.ToString() != "0")
                    {
                        /*strFrom = "INNER JOIN Procesos.Proceso ON RR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                            */
                        condicion =
                        string.Format("{0} and [IdArea] = {1}", condicion, objFiltros.intArea.ToString());
                    }
                    #endregion Areas
                    #endregion Filtros de Consulta
                    string strSelNormal = string.Format("SELECT [CodigoRiesgo],[NombreRiesgo],[CadenaValor],[IdCadenaValor],[Macroproceso],RR.[IdMacroproceso],[Proceso],RR.[IdProceso],[Subproceso],RR.[IdSubProceso]"
                    + ",[FrecuenciaInherente],[CodigoFrecuenciaInherente],[ImpactoInherente],[CodigoImpactoInherente],[IdProbabilidadResidual],[IdImpactoResidual],[ListaCausas]"
                    + ",RR.[IdRiesgo]");
                    string strFromNormal = string.Format("FROM [Riesgos].[vwCuadroMandoRiesgosRiesgos] as RR LEFT OUTER JOIN[Riesgos].[ControlesRiesgo] AS RCR on RCR.IdRiesgo = RR.IdRiesgo {0} {1}", strFrom, condicion);

                    strConsulta = string.Format("{0} {1} ", strSelNormal, strFromNormal);
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
        public DataTable LoadInfoReporteRiesgosPlanes(ref string strErrMsg, clsDTOCuadroMandoConsolidadoFiltros objFiltros)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string condicion = "WHERE (Anulado = 0) and ISNULL(RPA.IdRegistro,0) = 0 ";
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
                    /*if (objFiltros.intArea.ToString() != "0")
                    {
                        strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                    }*/
                    if (objFiltros.strAreaRiesgo != "")
                    {
                        /*strFrom = "INNER JOIN Procesos.Proceso ON RHR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());*/
                        /*strFrom = "INNER JOIN Parametrizacion.JerarquiaOrganizacional as PJO on PJO.idHijo =  Riesgos.Riesgo.IdResponsableRiesgo " +
                            "INNER JOIN Parametrizacion.DetalleJerarquiaOrg as PDJO on PDJO.idDetalleJerarquiaOrg = PJO.idHijo ";*/
                        condicion =
                            string.Format("{0} and ResponsableRiesgo = '{1}'", condicion, objFiltros.strAreaRiesgo);
                    }
                    #endregion Areas

                    #region Fechas Desde y Hasta
                    if (objFiltros.dtFechaHistoricoInicial != default(DateTime))
                    {
                        strFechaIni = mtdConvertirFecha(objFiltros.dtFechaHistoricoInicial.ToString(), 1) + " 00:00";
                        strFechaFin = mtdConvertirFecha(objFiltros.dtFechaHistoricoFinal.ToString(), 2) + " 23:59";

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
                        condicion = condicion + " AND (IdCadenaValor = " + objFiltros.intIdCadenaValor.ToString() + ")";
                    if (objFiltros.intIdMacroProceso.ToString() != "0")
                        condicion = condicion + " AND (IdMacroproceso = " + objFiltros.intIdMacroProceso.ToString() + ")";
                    if (objFiltros.intIdProceso.ToString() != "0")
                        condicion = condicion + " AND (IdProceso = " + objFiltros.intIdProceso.ToString() + ")";
                    if (objFiltros.intIdSubProceso.ToString() != "0")
                        condicion = condicion + " AND (IdSubProceso = " + objFiltros.intIdSubProceso.ToString() + ")";
                    #region Filtro ClasificacionRiesgo
                    if (objFiltros.intRiesgoGlobal.ToString() != "0")
                    {
                        condicion = string.Format("{0} AND (RR.IdClasificacionRiesgo = {1})", condicion, objFiltros.intRiesgoGlobal.ToString());
                    }
                    #endregion Filtro ClasificacionRiesgo
                    #region Areas
                    if (objFiltros.intArea.ToString() != "0")
                    {
                        /*strFrom = "INNER JOIN Procesos.Proceso ON RR.IdProceso = Procesos.Proceso.IdProceso ";
                        condicion =
                            string.Format("{0} AND (SELECT COUNT(*) Conteo FROM Procesos.FnSplitTable(Procesos.Proceso.IdArea,',') T WHERE T.Col1 IN (SELECT COL1 FROM Procesos.FnSplitTable('{1}',','))) > 0", condicion, objFiltros.intArea.ToString());
                            */
                        condicion =
                        string.Format("{0} and [IdArea] = {1}", condicion, objFiltros.intArea.ToString());
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
                    }
                    else
                    {
                        string strSelNormal = string.Format("SELECT [CodigoRiesgo],[NombreRiesgo],[CadenaValor],[IdCadenaValor],[Macroproceso],[IdMacroproceso],[Proceso],[IdProceso],[Subproceso],[IdSubProceso]"
                    + ",[FrecuenciaInherente],[CodigoFrecuenciaInherente],[ImpactoInherente],[CodigoImpactoInherente],[IdProbabilidadResidual],[IdImpactoResidual],[ListaCausas]"
                    + ",RR.[IdRiesgo]");
                        string strFromNormal = string.Format("FROM [Riesgos].[vwCuadroMandoRiesgosRiesgos] as RR LEFT OUTER JOIN [Riesgos].[PlanesAccion] as RPA on RPA.IdRegistro = RR.IdRiesgo {0} {1}", strFrom, condicion);

                        strConsulta = string.Format("{0} {1} ", strSelNormal, strFromNormal);

                    }
                    #endregion Filtros por Objetivos Estrategicos
                    
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