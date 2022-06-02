using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsInfoGeneralBLL
    {
        /// <summary>
        /// Metodo para consultar y visualizar las Evaluacion de Competencias
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsInformacionGeneral> mtdConsultarReporteInformacionGeneral(ref clsInformacionGeneral objInfoGeneralOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            bool booResult = false;
            DataTable dtEncuesta = new DataTable();
            DataTable dtIndicador = new DataTable();
            
            clsInfoGeneralDAL cDTinfoGeneral = new clsInfoGeneralDAL();
            
            //clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsInformacionGeneral> lstInfoGeneral = new List<clsInformacionGeneral>();
            booResult = cDTinfoGeneral.mtdConsultarReporteInfoGeneralRespuestaServicio(ref dtEncuesta, ref strErrMsg, ref fechaInicial, ref fechaFinal);
            booResult = cDTinfoGeneral.mtdConsultarReporteInfoGeneralIndicadores(ref dtIndicador, ref strErrMsg, ref fechaInicial, ref fechaFinal);

            int NumClientesAprobados = mtdClientesAprobados(ref fechaInicial, ref fechaFinal);
            if (booResult)
            {
                if (dtEncuesta != null)
                {
                    if (dtEncuesta.Rows.Count > 0)
                    {
                        //foreach (DataRow dr in dtEncuesta.Rows)
                        int rowsEncuesta = dtEncuesta.Rows.Count;
                        int rowsIndicadores = dtIndicador.Rows.Count;
                        if (rowsEncuesta >= rowsIndicadores)
                        {
                            
                            
                            for (int i = 0; i < dtEncuesta.Rows.Count; i++)
                            {
                                objInfoGeneralOut = new clsInformacionGeneral();

                                objInfoGeneralOut.intIdRespuestaServicio = Convert.ToInt32(dtEncuesta.Rows[i]["IdRespuestaServicio"].ToString().Trim());
                                objInfoGeneralOut.intNumClientesEncuestados = Convert.ToInt32(dtEncuesta.Rows[i]["NumClientesEncuestados"].ToString().Trim());
                                decimal promedioRespuesta = Convert.ToDecimal(dtEncuesta.Rows[i]["PromedioRespuesta"].ToString().Trim());
                                objInfoGeneralOut.intNumClientesAprobados = mtdNumClientesAprovados(ref promedioRespuesta);
                                
                                    if (i < rowsIndicadores)
                                {
                                    objInfoGeneralOut.intIdIndicador = Convert.ToInt32(dtIndicador.Rows[i]["IdIndicador"].ToString().Trim());
                                    objInfoGeneralOut.strNombreIndicador = dtIndicador.Rows[i]["NombreIndicador"].ToString().Trim();
                                    objInfoGeneralOut.strDescripcionIndicador = dtIndicador.Rows[i]["DescripcionIndicador"].ToString().Trim();
                                    clsIndicador objIndicador = new clsIndicador(
                                    Convert.ToInt32(dtIndicador.Rows[i]["IdIndicador"].ToString().Trim()),
                                    string.Empty, string.Empty,
                                    Convert.ToInt32(dtIndicador.Rows[i]["IdPeriodicidad"].ToString().Trim()),
                                    0, true, 0,
                                    Convert.ToInt32(dtIndicador.Rows[i]["IdProcesoIndicador"].ToString().Trim())
                                    , 0, string.Empty);
                                    decimal meta = Convert.ToDecimal(dtIndicador.Rows[i]["Meta"].ToString().Trim());
                                    objInfoGeneralOut.intNumMetasCumplidas = mtdMetasCumplidas(objIndicador, ref strErrMsg, meta);
                                    objInfoGeneralOut.intNumMetasIncumplidas = mtdMetasIncumplidas(objIndicador, ref strErrMsg, meta);
                                    objInfoGeneralOut.strProceso = dtIndicador.Rows[i]["Proceso"].ToString().Trim();
                                    /*int IdProceso = Convert.ToInt32(dtIndicador.Rows[i]["IdProceso"].ToString().Trim());
                                    Boolean booResultNC = false;
                                    booResultNC = cDTinfoGeneral.mtdConsultarReporteNoConformidadProceso(ref dtNoConformidad, ref strErrMsg, ref fechaInicial, ref fechaFinal, ref IdProceso);
                                    objInfoGeneralOut.intNumNoConformidad = Convert.ToInt32(dtNoConformidad.Rows[0]["NumNoConformidad"].ToString().Trim());
                                    objInfoGeneralOut.intNumNoConformidadCierre = Convert.ToInt32(dtIndicador.Rows[i]["NumNoConformidadCierre"].ToString().Trim());*/
                                    objInfoGeneralOut.intNumAuditoria = Convert.ToInt32(dtIndicador.Rows[i]["NumAuditoria"].ToString().Trim());
                                    objInfoGeneralOut.intNumAuditoriaCumplida = Convert.ToInt32(dtIndicador.Rows[i]["NumAuditoriaCumplida"].ToString().Trim());
                                }
                                else
                                {
                                    objInfoGeneralOut.intIdIndicador = 0;
                                    objInfoGeneralOut.strNombreIndicador = "";
                                    objInfoGeneralOut.strDescripcionIndicador = "";
                                    objInfoGeneralOut.intNumMetasCumplidas = 0;
                                    objInfoGeneralOut.intNumMetasIncumplidas = 0;
                                    objInfoGeneralOut.strProceso = "";
                                    /*objInfoGeneralOut.intNumNoConformidad = 0;
                                    objInfoGeneralOut.intNumNoConformidadCierre = 0;*/
                                    objInfoGeneralOut.intNumAuditoria = 0;
                                    objInfoGeneralOut.intNumAuditoriaCumplida = 0;
                                }
                                lstInfoGeneral.Add(objInfoGeneralOut);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dtIndicador.Rows.Count; i++)
                            {
                                objInfoGeneralOut = new clsInformacionGeneral();
                                if (i < rowsEncuesta)
                                {
                                    objInfoGeneralOut.intIdRespuestaServicio = Convert.ToInt32(dtEncuesta.Rows[i]["IdRespuestaServicio"].ToString().Trim());
                                    objInfoGeneralOut.intNumClientesEncuestados = Convert.ToInt32(dtEncuesta.Rows[i]["NumClientesEncuestados"].ToString().Trim());
                                    decimal promedioRespuesta = Convert.ToDecimal(dtEncuesta.Rows[i]["PromedioRespuesta"].ToString().Trim());
                                    objInfoGeneralOut.intNumClientesAprobados = mtdNumClientesAprovados(ref promedioRespuesta);
                                }
                                else
                                {
                                    objInfoGeneralOut.intIdRespuestaServicio = 0;
                                    objInfoGeneralOut.intNumClientesEncuestados = 0;
                                    objInfoGeneralOut.intNumClientesAprobados = 0;
                                }
                                if (i < rowsIndicadores)
                                {
                                    objInfoGeneralOut.intIdIndicador = Convert.ToInt32(dtIndicador.Rows[i]["IdIndicador"].ToString().Trim());
                                    objInfoGeneralOut.strNombreIndicador = dtIndicador.Rows[i]["NombreIndicador"].ToString().Trim();
                                    objInfoGeneralOut.strDescripcionIndicador = dtIndicador.Rows[i]["DescripcionIndicador"].ToString().Trim();
                                    clsIndicador objIndicador = new clsIndicador(
                                    Convert.ToInt32(dtIndicador.Rows[i]["IdIndicador"].ToString().Trim()),
                                    string.Empty, string.Empty,
                                    Convert.ToInt32(dtIndicador.Rows[i]["IdPeriodicidad"].ToString().Trim()),
                                    0, true, 0,
                                    Convert.ToInt32(dtIndicador.Rows[i]["IdProcesoIndicador"].ToString().Trim())
                                    , 0, string.Empty);
                                    decimal meta = Convert.ToDecimal(dtIndicador.Rows[i]["Meta"].ToString().Trim());
                                    objInfoGeneralOut.intNumMetasCumplidas = mtdMetasCumplidas(objIndicador, ref strErrMsg, meta);
                                    objInfoGeneralOut.intNumMetasIncumplidas = mtdMetasIncumplidas(objIndicador, ref strErrMsg, meta);
                                    objInfoGeneralOut.strProceso = dtIndicador.Rows[i]["Proceso"].ToString().Trim();
                                    //objInfoGeneralOut.intNumNoConformidad = Convert.ToInt32(dtIndicador.Rows[i]["NumNoConformidad"].ToString().Trim());
                                    /*int IdProceso = Convert.ToInt32(dtIndicador.Rows[i]["IdProceso"].ToString().Trim());
                                    Boolean booResultNC = false;
                                    booResultNC = cDTinfoGeneral.mtdConsultarReporteNoConformidadProceso(ref dtNoConformidad, ref strErrMsg, ref fechaInicial, ref fechaFinal, ref IdProceso);
                                    objInfoGeneralOut.intNumNoConformidad = Convert.ToInt32(dtNoConformidad.Rows[i]["NumNoConformidad"].ToString().Trim());
                                    objInfoGeneralOut.intNumNoConformidadCierre = Convert.ToInt32(dtIndicador.Rows[i]["NumNoConformidadCierre"].ToString().Trim());*/
                                    objInfoGeneralOut.intNumAuditoria = Convert.ToInt32(dtIndicador.Rows[i]["NumAuditoria"].ToString().Trim());
                                    objInfoGeneralOut.intNumAuditoriaCumplida = Convert.ToInt32(dtIndicador.Rows[i]["NumAuditoriaCumplida"].ToString().Trim());
                                }
                                else
                                {
                                    objInfoGeneralOut.intIdIndicador = 0;
                                    objInfoGeneralOut.strNombreIndicador = "";
                                    objInfoGeneralOut.strDescripcionIndicador = "";
                                    objInfoGeneralOut.intNumMetasCumplidas = 0;
                                    objInfoGeneralOut.intNumMetasIncumplidas = 0;
                                    objInfoGeneralOut.strProceso = "";
                                    /*objInfoGeneralOut.intNumNoConformidad = 0;
                                    objInfoGeneralOut.intNumNoConformidadCierre = 0;*/
                                    objInfoGeneralOut.intNumAuditoria = 0;
                                    objInfoGeneralOut.intNumAuditoriaCumplida = 0;
                                }
                                lstInfoGeneral.Add(objInfoGeneralOut);
                            }
                        }
                    }
                    else
                        lstInfoGeneral = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    lstInfoGeneral = null;
            }

            return lstInfoGeneral;
        }
        public int mtdNumClientesAprovados(ref decimal promedio)
        {
            decimal rangoInicial = 0;
            decimal rangoFinal = 0;
            int NumClientesAprovados = 0;
            string Descripcion = string.Empty;
            DataTable dtCriterios = new DataTable();
            string strErrMsg = string.Empty;
            bool booResult = false;
            clsInfoGeneralDAL cDTinfoGeneral = new clsInfoGeneralDAL();
            booResult = cDTinfoGeneral.mtdConsultarCriterioEvaluacionServicio(ref dtCriterios, ref strErrMsg);
            if (booResult)
            {
                if (dtCriterios != null)
                {
                    if (dtCriterios.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCriterios.Rows.Count; i++)
                        {
                            rangoInicial = Convert.ToDecimal(dtCriterios.Rows[i]["RangoInicial"].ToString().Trim());
                            rangoFinal = Convert.ToDecimal(dtCriterios.Rows[i]["RangoFinal"].ToString().Trim());
                            Descripcion = dtCriterios.Rows[i]["DescripcionAprobacion"].ToString().Trim();
                            if (promedio >= rangoInicial && promedio <= rangoFinal)
                            {
                                
                                    if (Descripcion == "CUMPLIO")
                                        NumClientesAprovados++;
                               
                            }
                        }
                    }
                }
            }

            return NumClientesAprovados;
        }
        public int mtdNumNoConformidad(ref string fechaInicial, ref string fechaFinal)
        {
            int NumNoConformidad = 0;
            DataTable dtNoConformidad = new DataTable();
            string strErrMsg = string.Empty;
            bool booResult = false;
            clsInfoGeneralDAL cDTinfoGeneral = new clsInfoGeneralDAL();
            booResult = cDTinfoGeneral.mtdConsultarReporteNoConformidadProceso(ref dtNoConformidad, ref strErrMsg, ref fechaInicial, ref fechaFinal);
            if (booResult)
            {
                if (dtNoConformidad != null)
                {
                    if (dtNoConformidad.Rows.Count > 0)
                    {
                        NumNoConformidad = Convert.ToInt32(dtNoConformidad.Rows[0]["NumNoConformidad"].ToString().Trim());
                    }
                }
            }

            return NumNoConformidad;
        }
        public int mtdNumNoConformidadCierre(ref string fechaInicial, ref string fechaFinal)
        {
            int NumNoConformidadCierre = 0;
            DataTable dtNoConformidad = new DataTable();
            string strErrMsg = string.Empty;
            bool booResult = false;
            clsInfoGeneralDAL cDTinfoGeneral = new clsInfoGeneralDAL();
            booResult = cDTinfoGeneral.mtdConsultarReporteNoConformidadCierre(ref dtNoConformidad, ref strErrMsg, ref fechaInicial, ref fechaFinal);
            if (booResult)
            {
                if (dtNoConformidad != null)
                {
                    if (dtNoConformidad.Rows.Count > 0)
                    {
                        NumNoConformidadCierre = Convert.ToInt32(dtNoConformidad.Rows[0]["NumNoConformidadCierre"].ToString().Trim());
                    }
                }
            }

            return NumNoConformidadCierre;
        }
        public int mtdClientesAprobados(ref string fechaInicial, ref string fechaFinal)
        {
            int NumClientesAprobados = 0;
            clsCriterioServicio clsCriterioServicio = new clsCriterioServicio();
            clsCriterioServicioBLL CriterioServicioBLL = new clsCriterioServicioBLL();
            List<clsCriterioServicio> LstCriterio = new List<clsCriterioServicio>();
            string strErrMsg = string.Empty;
            LstCriterio = CriterioServicioBLL.mtdConsultarCriterioServicio(ref LstCriterio, ref strErrMsg);
            decimal rangoInicial = 0;
            decimal rangoFinal = 0;
            DataTable dtEncuesta = new DataTable();
            bool booResult = false;
            clsInfoGeneralDAL cDTinfoGeneral = new clsInfoGeneralDAL();
            booResult = cDTinfoGeneral.mtdConsultarReporteInfoGeneralRespuestaServicio(ref dtEncuesta, ref strErrMsg, ref fechaInicial, ref fechaFinal);
            if (booResult)
            {
                if (dtEncuesta != null)
                {
                    if (dtEncuesta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtEncuesta.Rows.Count; i++)
                        {
                            decimal Promedio = Convert.ToDecimal(dtEncuesta.Rows[i]["PromedioRespuesta"].ToString().Trim());
                            foreach (clsCriterioServicio CriterioServicio in LstCriterio)
                            {
                                rangoInicial = CriterioServicio.intRangoInicial;
                                rangoFinal = CriterioServicio.intRangoFinal;
                                if (Promedio >= rangoInicial && Promedio <= rangoFinal)
                                {
                                    NumClientesAprobados++;
                                }
                            }
                        }
                    }
                }
            }

            return NumClientesAprobados;
        }
        public int mtdMetasCumplidas(clsIndicador objIndicador, ref string strErrMsg, decimal meta)
        {
            #region Vars
            bool booResult = false;
            object objProceso = new object();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            clsSeguimientoIndicadorBLL cSegIndicador = new clsSeguimientoIndicadorBLL();
            #endregion
            #region Consulta de Periodos, variables, cabeceras y cuadro de mando
            DataTable dtCuadro = new DataTable();
            DataTable dtCabCuadro = new DataTable();
            string strFormula = string.Empty;
            int cantMetasCumplidas = 0;
            #region Valido el proceso de acuerdo a los filtros
            //clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            objProcInd.intId = objIndicador.intIdProcesoIndicador;
            //objProcInd.intId = objIndicador.intIdProcesoIndicador;
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcIndicadorMod(objProcInd, ref strErrMsg);
            if (objProcesos[0].ToString() == "M")
            {
                objProcInd.intIdTipoProceso = 1;
            }
            if (objProcesos[0].ToString() == "P")
            {
                objProcInd.intIdTipoProceso = 2;
            }
            if (objProcesos[0].ToString() == "S")
            {
                objProcInd.intIdTipoProceso = 3;
            }
            #endregion
            #endregion
            cSegIndicador.mtdConsultarCuadroMando(objProcInd, objIndicador, ref dtCuadro, ref dtCabCuadro, ref strFormula, ref strErrMsg);
            string[] x = new string[dtCuadro.Rows.Count];
            decimal[] y = new decimal[dtCuadro.Rows.Count];
            int intNroCols = dtCuadro.Columns.Count;
            int intNroRegs = dtCuadro.Rows.Count;

            for (int i = 0; i < intNroRegs; i++)
            {
                x[i] = dtCuadro.Rows[i][0].ToString();
                y[i] = Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]);
                if (Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]) >= meta)
                {
                    cantMetasCumplidas++;
                }
            }
            return cantMetasCumplidas;
        }
        public int mtdMetasIncumplidas(clsIndicador objIndicador, ref string strErrMsg, decimal meta)
        {
            #region Vars
            bool booResult = false;
            object objProceso = new object();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            clsSeguimientoIndicadorBLL cSegIndicador = new clsSeguimientoIndicadorBLL();
            #endregion
            #region Consulta de Periodos, variables, cabeceras y cuadro de mando
            DataTable dtCuadro = new DataTable();
            DataTable dtCabCuadro = new DataTable();
            string strFormula = string.Empty;
            int cantMetasIncumplidas = 0;
            #region Valido el proceso de acuerdo a los filtros
            //clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            objProcInd.intId = objIndicador.intIdProcesoIndicador;
            //objProcInd.intId = objIndicador.intIdProcesoIndicador;
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcIndicadorMod(objProcInd, ref strErrMsg);
            if (objProcesos[0].ToString() == "M")
            {
                objProcInd.intIdTipoProceso = 1;
            }
            if (objProcesos[0].ToString() == "P")
            {
                objProcInd.intIdTipoProceso = 2;
            }
            if (objProcesos[0].ToString() == "S")
            {
                objProcInd.intIdTipoProceso = 3;
            }
            #endregion
            #endregion
            cSegIndicador.mtdConsultarCuadroMando(objProcInd, objIndicador, ref dtCuadro, ref dtCabCuadro, ref strFormula, ref strErrMsg);
            string[] x = new string[dtCuadro.Rows.Count];
            decimal[] y = new decimal[dtCuadro.Rows.Count];
            int intNroCols = dtCuadro.Columns.Count;
            int intNroRegs = dtCuadro.Rows.Count;

            for (int i = 0; i < intNroRegs; i++)
            {
                x[i] = dtCuadro.Rows[i][0].ToString();
                y[i] = Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]);
                if (Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]) < meta)
                {
                    cantMetasIncumplidas++;
                }
            }
            return cantMetasIncumplidas;
        }
    }
}