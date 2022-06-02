using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLMatrizIndicadores
    {
        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsMatrizIndicadores> mtdConsultarMatriz(ref string strErrMsg, ref int year)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsMatrizIndicadores> lstMatriz = new List<clsMatrizIndicadores>();
            clsDALMatrizIndicadores cDtMatriz = new clsDALMatrizIndicadores();
            clsMatrizIndicadores objMatriz = new clsMatrizIndicadores();

            //clsIndicador objIndicador = new clsIndicador();
            #endregion Vars



            dtInfo = cDtMatriz.mtdConsultarMatriz(ref strErrMsg, ref year);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        //objIndicador.intId = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                        clsIndicador objIndicador = new clsIndicador(
                        Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                        string.Empty, string.Empty,
                        Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim()),
                        0, true, 0,
                        Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim())
                        , 0, string.Empty);
                        objMatriz = new clsMatrizIndicadores();
                        objMatriz.strPoliticaCalidad = dr["PoliticaCalidad"].ToString().Trim();
                        objMatriz.intIdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                        objMatriz.strDescripcionObjetivo = dr["DescripcionObjetivo"].ToString().Trim();
                        objMatriz.strNombreIndicador = dr["NombreIndicador"].ToString().Trim();
                        objMatriz.strDescripcionInicador = dr["DescripcionInicador"].ToString().Trim();
                        objMatriz.strProcesoIndicador = dr["ProcesoIndicador"].ToString().Trim();
                        objMatriz.intMeta = Convert.ToDecimal(dr["Meta"].ToString().Trim());
                        objMatriz.strNombrePeriodo =   dr["NombrePeriodo"].ToString().Trim();
                        objMatriz.strFormula = mtdMakeFormula(objIndicador, ref strErrMsg);
                        objMatriz.intIdPeriodicidad = Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim());
                        objMatriz.intIdProcesoIndicador = Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim());
                        /*DataTable dtCuadro;
                        dtCuadro = mtdMakeSeguimiento(objIndicador, ref strErrMsg);
                        objMatriz.dtCuadro = dtCuadro;*/
                        lstMatriz.Add(objMatriz);
                        
                    }
                }
                else
                {
                    lstMatriz = null;
                    //strErrMsg = "No hay información de Actividad.";
                }
            }
            else
                lstMatriz = null;

            return lstMatriz;
        }
        public string mtdMakeFormula(clsIndicador objIndicador, ref string strErrMsg)
        {
            #region VarsFormula
            bool booResult = false;
            DataRow drCuadro, drCuadroOut;
            DataTable dtInfoPeriodo = new DataTable(), dtInfoVariable = new DataTable(), dtInfoCuadro = new DataTable(),
                dtCuadroMandoTemp = new DataTable(), dtInfoForm = new DataTable(), dtInfoCump = new DataTable();

            List<clsFormula> lstFormula = new List<clsFormula>();
            //clsPeriodicidad objPeriodicidad = new clsPeriodicidad(objIndicador.intIdPeriodicidad, string.Empty, 0, string.Empty);
            clsFormulaBLL cFormula = new clsFormulaBLL();
            clsDtPeriodicidad cPeriodo = new clsDtPeriodicidad();
            clsDtVariable cVariable = new clsDtVariable();
            clsDtSeguimientoIndicador cSegIndicador = new clsDtSeguimientoIndicador();
            clsDtFormula cForm = new clsDtFormula();
            clsDtCumplimiento cCumplimiento = new clsDtCumplimiento();
            #endregion VarsFormula
            

            string strFormula = string.Empty;
            
            booResult = cForm.mtdConsultarFormula(objIndicador, ref dtInfoForm, ref strErrMsg);

            if (booResult)
            {
                #region Crear formula
                cFormula.mtdCrearListaFormula(dtInfoForm, ref lstFormula, ref strErrMsg);

                foreach (clsFormula objFormula in lstFormula)
                {
                    switch (objFormula.intOperando)
                    {
                        case 1: //Variable
                            clsVariable objVarIn = new clsVariable(Convert.ToInt32(objFormula.strValor), string.Empty, string.Empty, true, 0, string.Empty),
                                objVarOut = new clsVariable();
                            clsVariableBLL cVar = new clsVariableBLL();
                            booResult = cVar.mtdConsultarVariable(objVarIn, ref objVarOut, ref strErrMsg);

                            if (booResult)
                            {
                                if (string.IsNullOrEmpty(strFormula.Trim()))
                                    strFormula = objVarOut.strDescripcion;
                                else
                                    strFormula = strFormula + " " + objVarOut.strDescripcion;
                            }

                            break;
                        case 2://Operacion
                        case 3:// Otro valor
                            {
                                if (string.IsNullOrEmpty(strFormula.Trim()))
                                    strFormula = objFormula.strValor;
                                else
                                    strFormula = strFormula + " " + objFormula.strValor;
                            }
                            break;
                    }
                }
                #endregion
            }

            return strFormula;
        }
        public DataTable mtdMakeSeguimiento(clsIndicador objIndicador, ref string strErrMsg)
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
            #region Valido el proceso de acuerdo a los filtros
            //clsProcesoIndicador objProcInd = new clsProcesoIndicador();
            objProcInd.intId = objIndicador.intIdProcesoIndicador;
            //objProcInd.intId = objIndicador.intIdProcesoIndicador;
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcIndicadorMod(objProcInd, ref strErrMsg);
            if(objProcesos[0].ToString() == "M")
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
            /*string[] x = new string[dtCuadro.Rows.Count];
            decimal[] y = new decimal[dtCuadro.Rows.Count];
            int intNroCols = dtCuadro.Columns.Count;
            int intNroRegs = dtCuadro.Rows.Count;
            
            for (int i = 0; i < intNroRegs; i++)
            {
                x[i] = dtCuadro.Rows[i][0].ToString();
                y[i] = Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]);
            }*/
            return dtCuadro;
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
                if(Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]) >= meta)
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
        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsMatrizIndicadores> mtdConsultarIndicadorCumplido(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsMatrizIndicadores> lstMatriz = new List<clsMatrizIndicadores>();
            clsDALMatrizIndicadores cDtMatriz = new clsDALMatrizIndicadores();
            clsMatrizIndicadores objMatriz = new clsMatrizIndicadores();

            //clsIndicador objIndicador = new clsIndicador();
            #endregion Vars



            dtInfo = cDtMatriz.mtdConsultarIndicadorCumplido(ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        //objIndicador.intId = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                        clsIndicador objIndicador = new clsIndicador(
                        Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                        string.Empty, string.Empty,
                        Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim()),
                        0, true, 0,
                        Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim())
                        , 0, string.Empty);
                        objMatriz = new clsMatrizIndicadores();
                        objMatriz.strPoliticaCalidad = dr["PoliticaCalidad"].ToString().Trim();
                        objMatriz.intIdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                        objMatriz.strDescripcionObjetivo = dr["DescripcionObjetivo"].ToString().Trim();
                        objMatriz.strNombreIndicador = dr["NombreIndicador"].ToString().Trim();
                        objMatriz.strDescripcionInicador = dr["DescripcionInicador"].ToString().Trim();
                        objMatriz.strProcesoIndicador = dr["ProcesoIndicador"].ToString().Trim();
                        
                        objMatriz.intMeta = Convert.ToDecimal(dr["Meta"].ToString().Trim());
                        objMatriz.strNombrePeriodo = dr["NombrePeriodo"].ToString().Trim();
                        objMatriz.strFormula = mtdMakeFormula(objIndicador, ref strErrMsg);
                        objMatriz.intIdPeriodicidad = Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim());
                        objMatriz.intIdProcesoIndicador = Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim());
                        DataTable dtCuadro;
                        dtCuadro = mtdMakeSeguimiento(objIndicador, ref strErrMsg);
                        //objMatriz.dtCuadro = dtCuadro;
                        string[] x = new string[dtCuadro.Rows.Count];
                        decimal[] y = new decimal[dtCuadro.Rows.Count];
                        int intNroCols = dtCuadro.Columns.Count;
                        int intNroRegs = dtCuadro.Rows.Count;
                        string Periodo = string.Empty;
                        string Resultado = string.Empty;
                        decimal meta = Convert.ToDecimal(dr["Meta"].ToString().Trim());
                        for (int i = 0; i < intNroRegs; i++)
                        {
                            if (Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]) >= meta)
                            {
                                Periodo += dtCuadro.Rows[i][0].ToString() + "|";
                                Resultado += dtCuadro.Rows[i][intNroCols - 3] + "|";
                            }
                        }
                        objMatriz.strArrayPeriodo = Periodo;
                        objMatriz.strArrayResultado = Resultado;
                        
                        objMatriz.intMetasCumplidas = mtdMetasCumplidas(objIndicador, ref strErrMsg, meta);
                        if (objMatriz.intMetasCumplidas > 0)
                        {
                            lstMatriz.Add(objMatriz);
                        }

                    }
                }
                else
                {
                    lstMatriz = null;
                    //strErrMsg = "No hay información de Actividad.";
                }
            }
            else
                lstMatriz = null;

            return lstMatriz;
        }
        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsMatrizIndicadores> mtdConsultarIndicadorIncumplido(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsMatrizIndicadores> lstMatriz = new List<clsMatrizIndicadores>();
            clsDALMatrizIndicadores cDtMatriz = new clsDALMatrizIndicadores();
            clsMatrizIndicadores objMatriz = new clsMatrizIndicadores();

            //clsIndicador objIndicador = new clsIndicador();
            #endregion Vars



            dtInfo = cDtMatriz.mtdConsultarIndicadorIncumplido(ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        //objIndicador.intId = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                        clsIndicador objIndicador = new clsIndicador(
                        Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                        string.Empty, string.Empty,
                        Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim()),
                        0, true, 0,
                        Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim())
                        , 0, string.Empty);
                        objMatriz = new clsMatrizIndicadores();
                        objMatriz.strPoliticaCalidad = dr["PoliticaCalidad"].ToString().Trim();
                        objMatriz.intIdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                        objMatriz.strDescripcionObjetivo = dr["DescripcionObjetivo"].ToString().Trim();
                        objMatriz.strNombreIndicador = dr["NombreIndicador"].ToString().Trim();
                        objMatriz.strDescripcionInicador = dr["DescripcionInicador"].ToString().Trim();
                        objMatriz.strProcesoIndicador = dr["ProcesoIndicador"].ToString().Trim();

                        objMatriz.intMeta = Convert.ToDecimal(dr["Meta"].ToString().Trim());
                        objMatriz.strNombrePeriodo = dr["NombrePeriodo"].ToString().Trim();
                        objMatriz.strFormula = mtdMakeFormula(objIndicador, ref strErrMsg);
                        objMatriz.intIdPeriodicidad = Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim());
                        objMatriz.intIdProcesoIndicador = Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim());
                        DataTable dtCuadro;
                        dtCuadro = mtdMakeSeguimiento(objIndicador, ref strErrMsg);
                        //objMatriz.dtCuadro = dtCuadro;
                        string[] x = new string[dtCuadro.Rows.Count];
                        decimal[] y = new decimal[dtCuadro.Rows.Count];
                        int intNroCols = dtCuadro.Columns.Count;
                        int intNroRegs = dtCuadro.Rows.Count;
                        string Periodo = string.Empty;
                        string Resultado = string.Empty;
                        decimal meta = Convert.ToDecimal(dr["Meta"].ToString().Trim());
                        for (int i = 0; i < intNroRegs; i++)
                        {
                            if (Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]) < meta)
                            {
                                Periodo += dtCuadro.Rows[i][0].ToString() + "|";
                                Resultado += dtCuadro.Rows[i][intNroCols - 3] + "|";
                            }
                        }
                        objMatriz.strArrayPeriodo = Periodo;
                        objMatriz.strArrayResultado = Resultado;

                        objMatriz.intMetasIncumplidas = mtdMetasIncumplidas(objIndicador, ref strErrMsg, meta);
                        if (objMatriz.intMetasIncumplidas > 0)
                        {
                            lstMatriz.Add(objMatriz);
                        }

                    }
                }
                else
                {
                    lstMatriz = null;
                    //strErrMsg = "No hay información de Actividad.";
                }
            }
            else
                lstMatriz = null;

            return lstMatriz;
        }
        /// <summary>
        /// Realiza la consulta de la Actividad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsMatrizIndicadores> mtdConsultarIndicadorPorProceso(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal, ref int proceso, ref int IdTipoProceso)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsMatrizIndicadores> lstMatriz = new List<clsMatrizIndicadores>();
            clsDALMatrizIndicadores cDtMatriz = new clsDALMatrizIndicadores();
            clsMatrizIndicadores objMatriz = new clsMatrizIndicadores();

            //clsIndicador objIndicador = new clsIndicador();
            #endregion Vars



            dtInfo = cDtMatriz.mtdConsultarIndicadorPorProceso(ref strErrMsg, ref fechaInicial, ref fechaFinal, ref proceso, ref IdTipoProceso);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        //objIndicador.intId = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                        clsIndicador objIndicador = new clsIndicador(
                        Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                        string.Empty, string.Empty,
                        Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim()),
                        0, true, 0,
                        Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim())
                        , 0, string.Empty);
                        objMatriz = new clsMatrizIndicadores();
                        objMatriz.strPoliticaCalidad = dr["PoliticaCalidad"].ToString().Trim();
                        objMatriz.intIdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                        objMatriz.strDescripcionObjetivo = dr["DescripcionObjetivo"].ToString().Trim();
                        objMatriz.strNombreIndicador = dr["NombreIndicador"].ToString().Trim();
                        objMatriz.strDescripcionInicador = dr["DescripcionInicador"].ToString().Trim();
                        objMatriz.strProcesoIndicador = dr["ProcesoIndicador"].ToString().Trim();

                        objMatriz.intMeta = Convert.ToDecimal(dr["Meta"].ToString().Trim());
                        objMatriz.strNombrePeriodo = dr["NombrePeriodo"].ToString().Trim();
                        objMatriz.strFormula = mtdMakeFormula(objIndicador, ref strErrMsg);
                        objMatriz.intIdPeriodicidad = Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim());
                        objMatriz.intIdProcesoIndicador = Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim());
                        objMatriz.strResponsable = dr["Responsable"].ToString().Trim();
                        DataTable dtCuadro;
                        dtCuadro = mtdMakeSeguimiento(objIndicador, ref strErrMsg);
                        //objMatriz.dtCuadro = dtCuadro;
                        string[] x = new string[dtCuadro.Rows.Count];
                        decimal[] y = new decimal[dtCuadro.Rows.Count];
                        int intNroCols = dtCuadro.Columns.Count;
                        int intNroRegs = dtCuadro.Rows.Count;
                        string Periodo = string.Empty;
                        string Resultado = string.Empty;
                        decimal meta = Convert.ToDecimal(dr["Meta"].ToString().Trim());
                        for (int i = 0; i < intNroRegs; i++)
                        {
                            /*if (Convert.ToDecimal(dtCuadro.Rows[i][intNroCols - 3]) >= meta)
                            {*/
                                Periodo += dtCuadro.Rows[i][0].ToString() + "|";
                                Resultado += dtCuadro.Rows[i][intNroCols - 3] + "|";
                            //}
                        }
                        objMatriz.strArrayPeriodo = Periodo;
                        objMatriz.strArrayResultado = Resultado;

                        //objMatriz.intMetasCumplidas = mtdMetasCumplidas(objIndicador, ref strErrMsg, meta);
                        /*if (objMatriz.intMetasCumplidas > 0)
                        {*/
                            lstMatriz.Add(objMatriz);
                        //}

                    }
                }
                else
                {
                    lstMatriz = null;
                    //strErrMsg = "No hay información de Actividad.";
                }
            }
            else
                lstMatriz = null;

            return lstMatriz;
        }
    }
}