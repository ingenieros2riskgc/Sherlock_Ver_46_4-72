using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALCuadroMandoIndicadores
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        public DataTable LoadInfoReporteIndicadoresRiesgos(ref string strErrMsg)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            string strSelect = string.Empty;
            string strFrom = string.Empty;
            string strWhere = string.Empty;
            string strOrder = string.Empty;
            string strFechaIni = string.Empty;
            string strFechaFin = string.Empty;
            //string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            //string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {

                strSelect = "SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdRiesgoAsociado],[IdProbabilidadResidual]"+
                ",[IdImpactoResidual],[Codigo],[Nombre]";

                strFrom = "FROM [Riesgos].[vwCuadroMandoIndicadorRiesgo]";

                strWhere = string.Empty;

                strOrder = string.Empty;


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
        public DataTable LoadInfoReporteIndicadoresProcesos(ref string strErrMsg, clsDTOCuadroMandoIndicadorFiltros objFiltros)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            string strSelect = string.Empty;
            string strFrom = string.Empty;
            string strWhere = string.Empty;
            string strOrder = string.Empty;
            string strFechaIni = string.Empty;
            string strFechaFin = string.Empty;
            //string condicion = "WHERE (Riesgos.Riesgo.Anulado = 0)";
            //string strFrom = string.Empty, strFechaIni = string.Empty, strFechaFin = string.Empty, strFechaFinal = string.Empty, strConsulta = string.Empty;
            #endregion Variables
            try
            {

                /*strWhere = string.Format("WHERE [IdCadenaValor] = {0} and [IdMacroproceso] = {1} and " +
                "[IdProceso] = {2} and[IdSubProceso] = {3} ", objFiltros.intIdCadenaValor, objFiltros.intIdMacroproceso,
                objFiltros.intIdProceso, objFiltros.intIdSubproceso);

            strSelect = "SELECT ISNULL([IdRiesgoIndicador], 0) AS IdRiesgoIndicador,[NombreIndicador],[ObjetivoIndicador],ISNULL([IdRiesgoAsociado],0) AS IdRiesgoAsociado,[IdProbabilidad]" +
            ",[IdImpacto],[Codigo],[Nombre],[IdCadenaValor],[CadenaValor],[IdMacroproceso],[Macroproceso],[IdProceso]"+
            ",[Proceso],[IdSubProceso],[Subproceso]";

            strFrom = "FROM [Riesgos].[vwCuadroMandoIndicadorProceso] ";*/

                if (objFiltros.intIdCadenaValor != 0)
                {
                    strWhere = string.Format("WHERE IdCadenaValor = {0} ", objFiltros.intIdCadenaValor);
                }



                if (objFiltros.intIdSubproceso != 0)
                {
                    if (string.IsNullOrEmpty(strWhere))
                    {
                        strWhere = string.Format("WHERE IdProceso = {0} and TipoProceso = 3 ", objFiltros.intIdSubproceso);
                    }
                    else {
                        strWhere = string.Format("and IdProceso = {0} and TipoProceso = 3 ", objFiltros.intIdSubproceso);
                    }
                }
                else
                {
                    if (objFiltros.intIdProceso != 0)
                    {
                        if (string.IsNullOrEmpty(strWhere))
                        {
                            strWhere = string.Format("WHERE IdProceso = {0} and TipoProceso = 2 ", objFiltros.intIdProceso);
                        }
                        else {
                            strWhere = string.Format("and IdProceso = {0} and TipoProceso = 2 ", objFiltros.intIdProceso);
                        }
                    }else
                    {
                        if (objFiltros.intIdMacroproceso != 0)
                        {
                            if (string.IsNullOrEmpty(strWhere))
                            {
                                strWhere = string.Format("WHERE IdProceso = {0} and TipoProceso = 1 ",
                                  objFiltros.intIdMacroproceso);
                            }
                            else {
                                strWhere = string.Format("and IdProceso = {0} and TipoProceso = 1 ",
                                     objFiltros.intIdMacroproceso);
                            }
                        }
                    }
                }
                //strSelect = "SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdProcesoIndicador],[IdProceso],"+
                //"[NombreProceso],[IdResponsableMedicion],[NombreHijo],[IdFrecuenciaMedicion],[FrecuenciaMedicion],[IdRiesgoAsociado],[Codigo],"+
                //"[Nombre],[IdFormula],[Nominador],[Denominador],[IdMeta],[Meta],[IdEsquemaSeguimiento],[ValorMinimo],[ValorMaximo],"+
                //"[DescripcionSeguimiento],[Usuario],[FechaCreacion],[Activo],[IdProbabilidad],[IdImpacto] ";

                strSelect = "SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdProceso],[IdRiesgoAsociado],[Codigo],[Nombre],[IdProbabilidadResidual],[IdImpactoResidual] ";

                strFrom = "FROM [Riesgos].[vwCuadroMandoIndicadorProceso]";
                strOrder = string.Empty;


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
        public DataTable LoadInfoReporteIndicadorResponsable(ref string strErrMsg, string IdJerarquias)
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

                strSelect = "SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdResponsableMedicion]"+
                ",[NombreHijo],[IdFrecuenciaMedicion],[FrecuenciaMedicion],[IdRiesgoAsociado],[IdFormula],[Codigo],[Nombre],[IdProbabilidad],[IdImpacto]" +
                ",[IdCadenaValor],[CadenaValor],[IdMacroproceso],[Macroproceso],[IdProceso],[Proceso],[IdSubProceso],[Subproceso],porcentaje,[idMeta], IdProbabilidadResidual,IdImpactoResidual";

                strFrom = "FROM [Riesgos].[vwCuadroMandoIndicadorJerarquias] ";

                strWhere = string.Format(" WHERE IdResponsableMedicion in ({0})", IdJerarquias);
                strWhere = strWhere.Replace(",)", ")");
                strOrder = string.Empty;



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