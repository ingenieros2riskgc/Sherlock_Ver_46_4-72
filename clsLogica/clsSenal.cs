using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using clsDatos;
using clsDTO;

namespace clsLogica
{
    public class clsSenal
    {
        // Trae las posiciones donde se guardan estos campos
        readonly string SenalAlertaPosTipoIdenCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIdenCabecero"].ToString();
        readonly string SenalAlertaPosNumeroIdenCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIdenCabecero"].ToString();
        readonly string SenalAlertaPosNombreCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombreCabecero"].ToString();
        readonly string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        readonly string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        readonly string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();
        #region Senal
        public List<clsDTOSenal> mtdCargarInfoSenal(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenal objSenal = new clsDTOSenal();
            List<clsDTOSenal> lstSenal = new List<clsDTOSenal>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaSenal(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSenal = new clsDTOSenal(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdSenal"].ToString().Trim(),
                            dr["CodigoSenal"].ToString().Trim(),
                            dr["DescripcionSenal"].ToString().Trim(),
                            dr["EsAutomatico"].ToString().Trim() == "True" ? true : false);

                        lstSenal.Add(objSenal);
                    }
                }
                else
                {
                    lstSenal = null;
                    strErrMsg = "No hay información de señales de alerta.";
                }
            }
            else
                lstSenal = null;

            return lstSenal;
        }


        public List<clsDTOSenalMod> mtdCargarInfoSenalMod(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenalMod objSenal = new clsDTOSenalMod();
            List<clsDTOSenalMod> lstSenal = new List<clsDTOSenalMod>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaSenalMod(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSenal = new clsDTOSenalMod(
                            dr["IdSenal"].ToString().Trim(),
                            dr["CodigoSenal"].ToString().Trim(),
                            dr["DescripcionSenal"].ToString().Trim(),
                            dr["Usuario"].ToString().Trim(),
                            dr["IdUsuario"].ToString().Trim(),
                            dr["FechaModificacion"].ToString().Trim()
                            );

                        lstSenal.Add(objSenal);
                    }
                }
                else
                {
                    lstSenal = null;
                    strErrMsg = "No hay información de señales de alerta.";
                }
            }
            else
                lstSenal = null;

            return lstSenal;
        }

        public List<clsDTOConsulSenalMod> mtdCargarInfoConsulSenalMod(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOConsulSenalMod objSenal = new clsDTOConsulSenalMod();
            List<clsDTOConsulSenalMod> lstSenal = new List<clsDTOConsulSenalMod>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaConsulSenalMod(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSenal = new clsDTOConsulSenalMod(
                            dr["IdSenal"].ToString().Trim(),
                            dr["CodigoSenal"].ToString().Trim(),
                            dr["DescripcionSenal"].ToString().Trim(),
                            dr["FechaInicial"].ToString().Trim(),
                            dr["FechaFinal"].ToString().Trim(),
                            dr["NumeroCoincidencias"].ToString().Trim(),
                            dr["Usuario"].ToString().Trim(),
                            dr["IdUsuario"].ToString().Trim(),
                            dr["FechaConsulta"].ToString().Trim()
                            );

                        lstSenal.Add(objSenal);
                    }
                }
                else
                {
                    lstSenal = null;
                    strErrMsg = "No hay información de señales de alerta.";
                }
            }
            else
                lstSenal = null;

            return lstSenal;
        }

        public List<clsDTOFactorRiesgoMod> mtdCargarInfoConsulFactorRiesgo(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOFactorRiesgoMod objSenal = new clsDTOFactorRiesgoMod();
            List<clsDTOFactorRiesgoMod> lstSenal = new List<clsDTOFactorRiesgoMod>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaConsulFactorRiesgoMod(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSenal = new clsDTOFactorRiesgoMod(
                            dr["IdFactorRiesgo"].ToString().Trim(),
                            dr["CodigoFactorRiesgo"].ToString().Trim(),
                            dr["DescFactorRiesgo"].ToString().Trim(),
                            dr["Usuario"].ToString().Trim(),
                            dr["IdUsuario"].ToString().Trim(),
                            dr["FechaModificacion"].ToString().Trim()
                            );

                        lstSenal.Add(objSenal);
                    }
                }
                else
                {
                    lstSenal = null;
                    strErrMsg = "No hay información de factores de riesgo";
                }
            }
            else
                lstSenal = null;

            return lstSenal;
        }


        public int mtdAgregarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            int intUltimoInsertado = 0;

            intUltimoInsertado = cDtSenal.mtdAgregarSenalRet(objSenal, ref strErrMsg);

            return intUltimoInsertado;
        }

        public void mtdActualizarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdActualizarSenal(objSenal, ref strErrMsg);
        }

        public void mtdEliminarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable(objSenal.StrIdSenal, string.Empty, string.Empty, string.Empty, string.Empty, false);

            cDtSenal.mtdEliminarFormula(objFormula, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                cDtSenal.mtdEliminarSenal(objSenal, ref strErrMsg);
        }

        public clsDTOSenal mtdConsultarSenal(clsDTOSenal objSenalIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenal objSenalOut = new clsDTOSenal();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultarSenal(objSenalIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdSenal], [CodigoSenal], [DescripcionSenal], [EsAutomatico]
                    objSenalOut = new clsDTOSenal(
                        dtInfo.Rows[0]["IdUsuario"].ToString().Trim(),
                        dtInfo.Rows[0]["IdSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["CodigoSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["DescripcionSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["EsAutomatico"].ToString().Trim() == "True" ? true : false);
                }
            }
            return objSenalOut;
        }

        #region Tabla Procesos
        public void mtdInsertarNroRegistrosSA(int intNroRegistros, string strNombreUsuario, string strDescripcion, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdInsertarNroRegistrosSA(intNroRegistros, strNombreUsuario, strDescripcion, ref strErrMsg);
        }

        public int mtdConteoRegistros(ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            int intConteo = 0;

            intConteo = cDtSenal.mtdConteoRegistros(ref strErrMsg);

            return intConteo;
        }

        public void mtdInsertarRegOperacion(int intIdUsuario,
            string strIdentificacion, string strNombreApellido,
            int intConteoTblConteoRegistro,// IdConteo
            int intConteoOcurrencias,// Cant
            int intValor, int intFrecuencia, string strTipoCliente,
            string IdSenal,// IdSenal
            string strDescripcionSenal,// DescSenal
            string strIndicador, ref string strErrMsg, string codSenal)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            cDtSenal.mtdInsertarRegOperacion(intIdUsuario,
                strIdentificacion, strNombreApellido,
                intConteoTblConteoRegistro,// IdConteo
                intConteoOcurrencias,// Cant
                intValor, intFrecuencia, strTipoCliente,
                IdSenal,// IdSenal
                strDescripcionSenal, strIndicador, ref strErrMsg, codSenal);
        }
        #endregion
        #endregion

        #region Operadores
        public List<clsDTOOperador> mtdCargarInfoOps(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperador objOp = new clsDTOOperador();
            List<clsDTOOperador> lstOps = new List<clsDTOOperador>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaOperador(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objOp = new clsDTOOperador(dr["IdOperador"].ToString().Trim(),
                            dr["NombreOperador"].ToString().Trim(), dr["IdentificadorOperador"].ToString().Trim());

                        lstOps.Add(objOp);
                    }
                }
                else
                {
                    lstOps = null;
                    strErrMsg = "No hay información de operadores.";
                }
            }
            else
                lstOps = null;

            return lstOps;
        }

        public clsDTOOperador mtdBuscarOperador(clsDTOOperador objOpIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperador objOp = new clsDTOOperador();
            #endregion Vars

            dtInfo = cDtSenal.mtdBuscarOperador(objOpIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdOperador], [NombreOperador], [IdentificadorOperador]
                    objOp = new clsDTOOperador(dtInfo.Rows[0]["IdOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["NombreOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["IdentificadorOperador"].ToString().Trim());
                }
            }
            return objOp;
        }

        public List<clsDTOOperadorGlobal> mtdCargarInfoOpGlobal(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperadorGlobal objOp = new clsDTOOperadorGlobal();
            List<clsDTOOperadorGlobal> lstOps = new List<clsDTOOperadorGlobal>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaOperadorGlobal(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objOp = new clsDTOOperadorGlobal(dr["IdOperador"].ToString().Trim(),
                            dr["NombreOperador"].ToString().Trim(), dr["IdentificadorOperador"].ToString().Trim());

                        lstOps.Add(objOp);
                    }
                }
                else
                {
                    lstOps = null;
                    strErrMsg = "No hay información de operadores.";
                }
            }
            else
                lstOps = null;

            return lstOps;
        }

        public clsDTOOperadorGlobal mtdBuscarOperadorGlobal(clsDTOOperadorGlobal objOpIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperadorGlobal objOp = new clsDTOOperadorGlobal();
            #endregion Vars

            dtInfo = cDtSenal.mtdBuscarOperadorGlobal(objOpIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdOperador], [NombreOperador], [IdentificadorOperador]
                    objOp = new clsDTOOperadorGlobal(dtInfo.Rows[0]["IdOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["NombreOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["IdentificadorOperador"].ToString().Trim());
                }
            }
            return objOp;
        }
        #endregion

        #region Formulas
        public void mtdGuardarFormula(List<object> LstFormula, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdGuardarFormula(LstFormula, ref strErrMsg);
        }

        public string mtdCargarFormulas(clsDTOSenal objSenal, ref List<object> LstFormula, ref bool booOk, ref string strErrMsg)
        {
            #region Vars
            bool booIsFirst = true, booIsGlobal = false;
            string strFormula = string.Empty;
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsParamArchivo cParam = new clsParamArchivo();

            //clsDTOVariable objVarIn, objVarOut = new clsDTOVariable();
            clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();
            clsDTOOperador objOpIn, objOpOut = new clsDTOOperador();
            clsDTOOperadorGlobal objOpGIn, objOpGOut = new clsDTOOperadorGlobal();
            #endregion

            lstFormula = mtdConsultarFormula(objSenal, ref strErrMsg);
            int contadorVariables = 0;
            int contadorExpresion = 0;
            #region Recorrido Formula
            foreach (clsDTOSenalVariable objFormula in lstFormula)
            {

                // Se valida si están comparando 2 variables en la expresión
                if (contadorExpresion < 3)
                {
                    if (objFormula.StrIdOperando == "1")
                    {
                        contadorVariables++;
                    }
                }
                else
                {
                    if (objFormula.StrIdOperando == "1")
                    {
                        contadorVariables = 1;
                        contadorExpresion = 0;
                    }
                    else
                    {
                        contadorExpresion = 0;
                        contadorVariables = 0;
                    }
                }

                #region Operacion
                switch (objFormula.StrIdOperando)
                {
                    case "1"://Variable
                        #region Variable
                        objCampoIn = new clsDTOEstructuraCampo(string.Empty, objFormula.StrValor, string.Empty,
                               string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, true, false);
                        objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);

                        if (string.IsNullOrEmpty(strFormula))
                            strFormula = objCampoOut.StrNombreCampo + " ";
                        else if (contadorVariables == 2)
                            strFormula = strFormula + objCampoOut.StrNombreCampo + " ";
                        else
                        {
                            if (booIsGlobal)
                            {
                                strFormula = strFormula + " ( " + objCampoOut.StrNombreCampo + " ) ";
                                booIsGlobal = false;
                            }
                            else
                                strFormula = strFormula + " y " + objCampoOut.StrNombreCampo + " ";
                        }

                        LstFormula.Add(objFormula);
                        booOk = true;
                        #endregion

                        break;
                    case "2": //Operador
                        #region Operador
                        objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                        objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);
                        strFormula += objOpOut.StrIdentificadorOperador + " ";
                        LstFormula.Add(objFormula);
                        booOk = false;
                        #endregion

                        break;
                    case "3": //Otro
                        #region Otro
                        strFormula += objFormula.StrValor + " ";
                        LstFormula.Add(objFormula);
                        booOk = true;
                        #endregion

                        break;
                    case "4": //Rango
                        #region Rango
                        if (booIsFirst)
                        {
                            strFormula = strFormula + " " + objFormula.StrValor;
                            booIsFirst = false;
                        }
                        else
                            strFormula = strFormula + " y " + objFormula.StrValor;

                        LstFormula.Add(objFormula);
                        booOk = true;
                        #endregion

                        break;
                    case "5": //Global
                        #region Operador Global
                        booIsGlobal = true;
                        objOpGIn = new clsDTOOperadorGlobal(objFormula.StrValor, string.Empty, string.Empty);
                        objOpGOut = mtdBuscarOperadorGlobal(objOpGIn, ref strErrMsg);

                        if (string.IsNullOrEmpty(strFormula))
                            strFormula = objOpGOut.StrIdentificadorOperador + " ";
                        else
                            strFormula = strFormula + " y " + objOpGOut.StrIdentificadorOperador + " ";
                        #endregion

                        break;
                }
                #endregion

                if (!string.IsNullOrEmpty(strErrMsg))
                {
                    LstFormula = new List<object>();
                    strFormula = string.Empty;
                    booOk = false;
                    break;
                }

                contadorExpresion++;
            }
            #endregion

            return strFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormula(clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormulaXSenal(objSenal, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion], [EsGlobal]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormulas(ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormulas(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion], [EsGlobal]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormSenalAuto(bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormSenalAuto(booAutomatico, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormSenalAuto(clsDTOSenal objSenal, bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormSenalAuto(objSenal, booAutomatico, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion], [EsGlobal]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
                else
                    lstFormula = null;
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public void mtdEliminarFormula(clsDTOSenalVariable objFormula, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdEliminarFormula(objFormula, ref strErrMsg);
        }

        public void mtdEjecutarSenalGlobal(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, int intIdUsuario, string strNombreUsuario, ref string strErrMsg)
        {
            #region Variables Formulas para filtros
            int intContador = 1, intPos = 0, intResultado = 0;
            bool booFormGlobal = false, booIsFirst = true, booResult = false, booIsThereGlobal = false;
            string strFormulaGlobal = string.Empty, strFiltro = string.Empty, strCondGlobal = string.Empty,
                strVariableRango = string.Empty, strFiltroTotal = string.Empty, strTextoCliente = string.Empty;

            DataTable dtSerializedData = new DataTable(), dtInfoId, dtFiltrados;
            DataRow dr;

            clsTupla objFormGlobal = new clsTupla(), objCondGlobal = new clsTupla();
            List<clsTupla> lstFormulaGlobal = new List<clsTupla>(), lstCondicionGlobal = new List<clsTupla>();

            clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();
            clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
            clsDTOOperadorGlobal objOpGIn = new clsDTOOperadorGlobal(), objOpGOut = new clsDTOOperadorGlobal();
            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();

            object objInfoRegistro = new object();
            List<object> lstInfoRegistro = new List<object>();
            #endregion

            #region Crea la estructura
            foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
            {
                dtSerializedData.Columns.Add(objEstructura.StrNombreCampo, typeof(string));
            }
            #endregion

            #region Serializacion de los datos e insercion en la estructura
            dr = dtSerializedData.NewRow();
            foreach (DataRow drInfo in dtInfoCargada.Rows)
            {
                if (Convert.ToInt32(drInfo["Posicion"].ToString()) < intContador)
                {
                    dtSerializedData.Rows.Add(dr);
                    dr = dtSerializedData.NewRow();
                    intContador = 1;
                }

                dr[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString();
                intContador++;
            }
            dtSerializedData.Rows.Add(dr);
            #endregion

            #region Creacion de Formulas para filtros
            //Primero genero la formula de filtros individuales
            //Segundo genero la formula de filtros globales
            #region Recorrido Formula

            foreach (clsDTOSenalVariable objFormula in lstFormulas)
            {
                if (objFormula.BooEsGlobal)
                {
                    booIsThereGlobal = true;

                    #region Operacion
                    switch (objFormula.StrIdOperando)
                    {
                        case "1"://Variable
                            #region Variable
                            objCampoIn = new clsDTOEstructuraCampo(string.Empty, objFormula.StrValor, string.Empty,
                                string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, true, false);
                            objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);

                            if (booFormGlobal)
                            {
                                strFormulaGlobal = strFormulaGlobal + " ( [" + objCampoOut.StrNombreCampo + "] ) ";
                                objFormGlobal = new clsTupla(intPos, strFormulaGlobal, objFormula.StrIdSenal);
                                lstFormulaGlobal.Add(objFormGlobal);
                                strFormulaGlobal = string.Empty;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(strFiltro))
                                    strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                                else
                                    strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                                strVariableRango = objCampoOut.StrNombreCampo;
                            }
                            #endregion

                            break;
                        case "2": //Operador
                            #region Operador
                            objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                            objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                            if (booFormGlobal)
                            {
                                if (objOpOut.StrIdentificadorOperador == "Entre")
                                    strCondGlobal = ">= ";
                                else
                                    strCondGlobal = objOpOut.StrIdentificadorOperador + " ";
                            }
                            else
                            {
                                if (objOpOut.StrIdentificadorOperador == "Entre")
                                    strFiltro += ">= ";
                                else
                                    strFiltro += objOpOut.StrIdentificadorOperador + " ";
                            }
                            #endregion

                            break;
                        case "3": //Otro Valor
                            #region Otro Valor

                            if (booFormGlobal)
                            {
                                strCondGlobal = strCondGlobal + "" + objFormula.StrValor + " ";
                                objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                lstCondicionGlobal.Add(objCondGlobal);
                                strCondGlobal = string.Empty;
                                booFormGlobal = false;
                            }
                            else
                                strFiltro = strFiltro + " " + objFormula.StrValor + " ";

                            #endregion

                            break;
                        case "4": //Rangos
                            #region Rangos
                            if (booIsFirst)
                            {
                                #region Rango Inferior
                                if (booFormGlobal)
                                {
                                    strCondGlobal = strCondGlobal + " " + objFormula.StrValor + " ";
                                    objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                    lstCondicionGlobal.Add(objCondGlobal);
                                    strCondGlobal = string.Empty;
                                }
                                else
                                    strFiltro = strFiltro + " " + objFormula.StrValor + " ";
                                #endregion

                                booIsFirst = false;
                            }
                            else
                            {
                                #region Rango Superior
                                if (booFormGlobal)
                                {
                                    strCondGlobal = "<= " + objFormula.StrValor + " ";
                                    objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                    lstCondicionGlobal.Add(objCondGlobal);
                                    strCondGlobal = string.Empty;
                                    booFormGlobal = false;
                                }
                                else
                                    strFiltro = strFiltro + " AND " + strVariableRango + " <= " + objFormula.StrValor + " ";
                                #endregion
                            }
                            #endregion

                            break;
                        case "5": //Global
                            #region Operador Global
                            intPos++;
                            booFormGlobal = true;
                            objOpGIn = new clsDTOOperadorGlobal(objFormula.StrValor, string.Empty, string.Empty);
                            objOpGOut = mtdBuscarOperadorGlobal(objOpGIn, ref strErrMsg);

                            strFormulaGlobal = objOpGOut.StrIdentificadorOperador + " ";

                            #endregion

                            break;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                }
            }
            #endregion
            #endregion
            if (booIsThereGlobal)
            {
                #region Generacion de Registros
                //creo ciclo 1 para que se realice busquedas sobre cada registro de acuerdo al filtro de identificacion 
                dtInfoId = dtSerializedData.DefaultView.ToTable(true, SenalAlertaPosNumeroIden);

                foreach (DataRow drId in dtInfoId.Rows)
                {
                    #region Filtros
                    strFiltroTotal = string.Format($"[{SenalAlertaPosNumeroIden}] = '{0}' AND {1}",
                        drId[0].ToString(), strFiltro);
                    dtFiltrados = dtSerializedData.Select(strFiltroTotal).CopyToDataTable();

                    if (dtFiltrados.Rows.Count == 0)
                        continue;
                    #endregion

                    #region Recorrido para hacer comparaciones y Enviar Alertas
                    //creo ciclo 2 para realizar los filtros de las formulas y operaciones globales.
                    foreach (clsTupla objT1 in lstFormulaGlobal)
                    {
                        #region Recorrido para hacer comparaciones globales
                        intResultado = (int)dtFiltrados.Compute(objT1.StrInfo, "");
                        booResult = false;
                        foreach (clsTupla objT2 in lstCondicionGlobal)
                        {
                            if (objT1.IntPos == objT2.IntPos)
                            {
                                booResult = mtdCompararValores(objT2.StrInfo.Trim(), intResultado, ref strErrMsg);

                                if (!string.IsNullOrEmpty(strErrMsg))
                                    break;
                            }
                        }
                        #endregion

                        if ((string.IsNullOrEmpty(strErrMsg)) && booResult)
                        {
                            string strNroId = dtFiltrados.Rows[0][SenalAlertaPosNumeroIden].ToString(),
                                strTipoId = dtFiltrados.Rows[0][SenalAlertaPosTipoIden].ToString(),
                                strNombreCliente = dtFiltrados.Rows[0][SenalAlertaPosNombre].ToString();

                            #region Notificacion
                            //genero alerta y notificacion
                            strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                                strNombreCliente, strTipoId, strNroId);

                            clsDTOSenal objSenalIn = new clsDTOSenal(objT1.StrIdSenal, string.Empty, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                            objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);

                            cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                                strTextoCliente, ref strErrMsg);
                            #endregion

                            #region Registro de operacion

                            mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                            string strIndicador = string.Format("{0} = {1}", objT1.StrInfo, intResultado);

                            mtdInsertarRegOperacion(intIdUsuario, strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                                1,// IdConteo,
                                1,// Cant,
                                0,// Valor, 
                                0,// Frecuencia,
                                string.Empty,// TipoCliente,
                                objSenalOut.StrIdSenal, objSenalOut.StrDescripcionSenal, strIndicador,
                                ref strErrMsg, objSenalOut.StrCodigoSenal);
                            #endregion
                        }
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                }
                #endregion
            }
        }

        public void mtdEjecutarSenalGlobal(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, string strIdUsuario, string strNombreUsuario, ref int intOcurrencias, ref string strErrMsg)
        {
            #region Variables Formulas para filtros
            int intContador = 1, intPos = 0;
            decimal decResultado = 0;
            bool booFormGlobal = false, booIsFirst = true, booResult = false, booIsThereGlobal = false, booIsSUM = false;
            string strFormulaGlobal = string.Empty, strFiltro = string.Empty, strCondGlobal = string.Empty,
                strVariableRango = string.Empty, strFiltroTotal = string.Empty, strTextoCliente = string.Empty;

            DataTable dtSerializedData = new DataTable(), dtInfoId, dtFiltrados;
            DataRow dr;

            clsTupla objFormGlobal = new clsTupla(), objCondGlobal = new clsTupla();
            List<clsTupla> lstFormulaGlobal = new List<clsTupla>(), lstCondicionGlobal = new List<clsTupla>();

            clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();
            clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
            clsDTOOperadorGlobal objOpGIn = new clsDTOOperadorGlobal(), objOpGOut = new clsDTOOperadorGlobal();
            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();

            object objInfoRegistro = new object();
            List<object> lstInfoRegistro = new List<object>();
            List<string> lstCamposSUM = new List<string>();
            List<string> lstCamposPos = new List<string>();
            #endregion

            #region Creacion de Formulas para filtros
            //Primero genero la formula de filtros individuales
            //Segundo genero la formula de filtros globales
            #region Recorrido Formula

            int contador = 0;

            foreach (clsDTOSenalVariable objFormula in lstFormulas)
            {

                if (objFormula.BooEsGlobal)
                {
                    booIsThereGlobal = true;

                    #region Operacion
                    switch (objFormula.StrIdOperando)
                    {
                        case "1"://Variable
                            #region Variable
                            contador = contador + 1;
                            objCampoIn = new clsDTOEstructuraCampo(Convert.ToString(contador), objFormula.StrValor, string.Empty,
                                string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, true, false);
                            objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);

                            if (booFormGlobal)
                            {
                                strFormulaGlobal = strFormulaGlobal + " ( [" + objCampoOut.StrNombreCampo + "] ) ";
                                objFormGlobal = new clsTupla(intPos, strFormulaGlobal, objFormula.StrIdSenal);
                                lstFormulaGlobal.Add(objFormGlobal);
                                strFormulaGlobal = string.Empty;

                                if (booIsSUM)
                                    lstCamposSUM.Add(objCampoOut.StrNombreCampo);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(strFiltro))
                                    strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                                else
                                    strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                                strVariableRango = objCampoOut.StrNombreCampo;
                            }
                            #endregion

                            break;
                        case "2": //Operador
                            #region Operador
                            objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                            objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                            if (booFormGlobal)
                            {
                                if (objOpOut.StrIdentificadorOperador == "Entre")
                                    strCondGlobal = ">= ";
                                else
                                    strCondGlobal = objOpOut.StrIdentificadorOperador + " ";
                            }
                            else
                            {
                                if (objOpOut.StrIdentificadorOperador == "Entre")
                                    strFiltro += ">= ";
                                else
                                    strFiltro += objOpOut.StrIdentificadorOperador + " ";
                            }
                            #endregion

                            break;
                        case "3": //Otro Valor
                            #region Otro Valor

                            if (booFormGlobal)
                            {
                                strCondGlobal = strCondGlobal + "" + objFormula.StrValor + " ";
                                objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                lstCondicionGlobal.Add(objCondGlobal);
                                strCondGlobal = string.Empty;
                                booFormGlobal = false;
                            }
                            else
                                strFiltro = strFiltro + " '" + objFormula.StrValor + "' ";

                            #endregion

                            break;
                        case "4": //Rangos
                            #region Rangos
                            if (booIsFirst)
                            {
                                #region Rango Inferior
                                if (booFormGlobal)
                                {
                                    strCondGlobal = strCondGlobal + " " + objFormula.StrValor + " ";
                                    objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                    lstCondicionGlobal.Add(objCondGlobal);
                                    strCondGlobal = string.Empty;
                                }
                                else
                                    strFiltro = strFiltro + " '" + objFormula.StrValor + "' ";
                                #endregion

                                booIsFirst = false;
                            }
                            else
                            {
                                #region Rango Superior
                                if (booFormGlobal)
                                {
                                    strCondGlobal = "<= " + objFormula.StrValor + " ";
                                    objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                    lstCondicionGlobal.Add(objCondGlobal);
                                    strCondGlobal = string.Empty;
                                    booFormGlobal = false;
                                }
                                else
                                    strFiltro = strFiltro + " AND " + strVariableRango + " <= '" + objFormula.StrValor + "' ";
                                #endregion
                            }
                            #endregion

                            break;
                        case "5": //Global
                            #region Operador Global
                            intPos++;
                            booFormGlobal = true;
                            objOpGIn = new clsDTOOperadorGlobal(objFormula.StrValor, string.Empty, string.Empty);
                            objOpGOut = mtdBuscarOperadorGlobal(objOpGIn, ref strErrMsg);

                            strFormulaGlobal = objOpGOut.StrIdentificadorOperador + " ";

                            if (objOpGOut.StrIdentificadorOperador == "SUM")
                                booIsSUM = true;
                            #endregion

                            break;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                }
            }
            #endregion
            #endregion

            if (booIsThereGlobal)
            {
                #region Crea la estructura
                bool flagNumerico1 = false;
                bool flagNumerico2 = false;
                int posicion1 = 0;
                int posicion2 = 0;
                foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                {
                    bool booIsInteger = false;
                    /*foreach (string strCampoTmp in strCamposEnteros)
                    {
                        if (strCampoTmp == objEstructura.StrNombreCampo)
                        {
                            booIsInteger = true;
                            break;
                        }
                    }*/
                    string Delimiter = ">";
                    string[] formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = "<";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = "=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = ">=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = "<=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    /*if (booIsInteger)
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo.ToUpper().Trim(), typeof(decimal));
                    else
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo.ToUpper().Trim(), typeof(string));*/
                }
                #endregion
                #region Crea la estructura
                bool booIsDec = false;
                foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                {
                    foreach (string strCampoSum in lstCamposSUM)
                    {
                        if (strCampoSum == objEstructura.StrNombreCampo)
                        {
                            booIsDec = true;
                            break;
                        }
                    }

                    if (booIsDec)
                    {
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo, typeof(decimal));
                        lstCamposPos.Add(objEstructura.StrPosicion);
                        booIsDec = false;
                    }
                    else
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo, typeof(string));
                }
                #endregion

                #region Serializacion de los datos e insercion en la estructura
                booIsDec = false;
                dr = dtSerializedData.NewRow();
                string campo = string.Empty;
                foreach (DataRow drInfo in dtInfoCargada.Rows)
                {
                    if (Convert.ToInt32(drInfo["Posicion"].ToString()) < intContador)
                    {
                        dtSerializedData.Rows.Add(dr);
                        dr = dtSerializedData.NewRow();
                        intContador = 1;
                    }

                    foreach (string strCampoPos in lstCamposPos)
                    {
                        if (Convert.ToInt32(strCampoPos) == (Convert.ToInt32(drInfo["Posicion"].ToString())))
                        {
                            campo = strCampoPos;
                            booIsDec = true;
                            break;
                        }
                    }

                    if (booIsDec)
                    {
                        dr[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = Convert.ToDecimal(drInfo["ValorCampoArchivo"].ToString());
                        booIsDec = false;
                    }
                    else
                        dr[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString();
                    intContador++;
                }
                dtSerializedData.Rows.Add(dr);
                #endregion

                #region Generacion de Registros

                if (flagNumerico1 == true)
                {
                    strFiltro = strFiltro.Replace("[", "Convert([");
                    strFiltro = strFiltro.Replace("]", "], System.Decimal)");
                    //strFiltro = "Convert(DEPOSITOS, System.Decimal) > 10000000";
                    if (dtSerializedData.Select(strFiltro).Count() > 0)
                        dtSerializedData = dtSerializedData.Select(strFiltro).CopyToDataTable();
                    else
                        dtSerializedData = new DataTable();
                }
                else if (dtSerializedData.Select(strFiltro.ToUpper().Trim()).Count() > 0)
                    dtSerializedData = dtSerializedData.Select(strFiltro).CopyToDataTable();
                else
                    dtSerializedData = new DataTable();

                //creo ciclo 1 para que se realice busquedas sobre cada registro de acuerdo al filtro de identificacion 
                dtInfoId = new DataTable();
                if (dtSerializedData.Rows.Count > 0)
                    dtInfoId = dtSerializedData.DefaultView.ToTable(true, SenalAlertaPosNumeroIdenCabecero);
                /*dtSerializedData.DefaultView.Sort = SenalAlertaPosNumeroIdenCabecero;
                foreach (DataRow dtRowData in dtSerializedData.Rows)
                {

                }*/
                string i_sAggregateColumn = string.Empty;
                foreach (clsTupla objT2 in lstFormulaGlobal)
                {
                    string[] strPartes = objT2.StrInfo.Replace("'", "").Split(' ');
                    i_sAggregateColumn = strPartes[3].ToString();
                }
                //dtSerializedData = Sum(SenalAlertaPosNumeroIdenCabecero, i_sAggregateColumn, dtSerializedData);
                int recorrido = 0;
                int count = 0;
                foreach (DataRow drId in dtInfoId.Rows)
                {
                    dtFiltrados = null;
                    #region Filtros
                    try
                    {
                        dtFiltrados = dtSerializedData;
                    }
                    catch(Exception ex)
                    {
                        strErrMsg = ex.Message;
                    }
                    count++;
                    //dtFiltrados = dtInfoId;

                    /*strFiltroTotal = string.Format($"[{SenalAlertaPosNumeroIdenCabecero}] = '{0}' AND {1}",
                        drId[0].ToString(), strFiltro);
                    if (dtSerializedData.Select(strFiltroTotal).Count() > 0)
                        dtFiltrados = dtSerializedData.Select(strFiltroTotal).CopyToDataTable();
                    else
                        continue;*/
                    #endregion

                    #region Recorrido para hacer comparaciones y Enviar Alertas
                    //creo ciclo 2 para realizar los filtros de las formulas y operaciones globales.
                    try
                    {
                        if (dtFiltrados.Rows.Count > 0)
                        {
                            int identificador = 0;
                            
                            foreach (clsTupla objT1 in lstFormulaGlobal)
                            {
                                string filtro = string.Empty;
                                string drIdstr = string.Empty;
                                string dato = string.Empty;
                                
                                try
                                {
                                    
                                    filtro = SenalAlertaPosNumeroIdenCabecero + " = " + dtFiltrados.Rows[identificador][Convert.ToInt32(SenalAlertaPosNumeroIden) - 1].ToString();
                                    drIdstr = dtFiltrados.Rows[identificador][Convert.ToInt32(SenalAlertaPosNumeroIden) - 1].ToString();//drId[identificador].ToString();
                                    if (drIdstr != "")
                                    {
                                        //drIdstr = dtFiltrados.Rows[identificador][Convert.ToInt32(SenalAlertaPosNumeroIden) - 1].ToString();
                                        //dato = dtFiltrados.Compute("Sum(" + i_sAggregateColumn + ")", SenalAlertaPosNumeroIdenCabecero + " = '" + drId[identificador] + "'").ToString();
                                        dato = dtFiltrados.Compute("Sum(" + i_sAggregateColumn + ")", SenalAlertaPosNumeroIdenCabecero + " = '" + drIdstr + "'").ToString();
                                        //decResultado = Convert.ToDecimal(dtFiltrados.Compute("Sum(" + i_sAggregateColumn + ")", SenalAlertaPosNumeroIdenCabecero + " = '" + drId[identificador] + "'"));
                                        decResultado = Convert.ToDecimal(dtFiltrados.Compute("Sum(" + i_sAggregateColumn + ")", SenalAlertaPosNumeroIdenCabecero + " = '" + drIdstr + "'"));
                                    }
                                        
                                }
                                catch(Exception Ex)
                                {
                                    strErrMsg = "Datos: "+ filtro+" id: "+ drIdstr+" data: "+ dato+" " +" info: "+ decResultado +" "+ Ex.Message;

                                }
                                try
                                {
                                    #region Recorrido para hacer comparaciones globales
                                    
                                    //decResultado = Convert.ToDecimal(dtFiltrados.Compute(objT1.StrInfo, filtro));
                                    //decResultado = Convert.ToDecimal(dtFiltrados.Compute(objT1.StrInfo, ""));
                                    //decimal valor = Convert.ToDecimal(dtFiltrados.Rows[identificador][Convert.ToInt32(campo)-1].ToString());
                                    //decResultado = Convert.ToDecimal(drId.Compute(objT1.StrInfo, ""));
                                    booResult = false;
                                    foreach (clsTupla objT2 in lstCondicionGlobal)
                                    {
                                        if (objT1.IntPos == objT2.IntPos)
                                        {
                                            //booResult = mtdCompararValores(objT2.StrInfo.Trim(), decResultado, ref strErrMsg);
                                            booResult = mtdCompararValores(objT2.StrInfo.Trim(), decResultado, ref strErrMsg);
                                            if (!string.IsNullOrEmpty(strErrMsg))
                                                break;
                                        }
                                    }
                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    strErrMsg = ex.Message;
                                }
                                

                                if ((string.IsNullOrEmpty(strErrMsg)) && booResult)
                                {
                                    if (booIsSUM)
                                        intOcurrencias = intOcurrencias + 1;
                                    else
                                        intOcurrencias = intOcurrencias + (int)decResultado;
                                    try
                                    {
                                        int PosNumeroIden = Convert.ToInt32(SenalAlertaPosNumeroIden);
                                        int PosTipoIden = Convert.ToInt32(SenalAlertaPosTipoIden);
                                        int PosNombre = Convert.ToInt32(SenalAlertaPosNombre);
                                    }
                                    catch(Exception ex)
                                    {
                                        strErrMsg = ex.Message;
                                    }
                                    DataTable tblFiltered = new DataTable();
                                    try
                                    {
                                        tblFiltered = dtFiltrados.AsEnumerable()
                                         .Where(r => r.Field<String>(SenalAlertaPosNumeroIdenCabecero) == drId[0].ToString())
                                         .CopyToDataTable();
                                    }
                                    catch (Exception ex)
                                    {
                                        strErrMsg = ex.Message;
                                    }

                                    //DataRow[] rowDatosAlerta = dtFiltrados.Select(SenalAlertaPosNumeroIdenCabecero+" = " +drId[0].ToString());
                                    /*string strNroId = dtFiltrados.Rows[recorrido][PosNumeroIden - 1].ToString(),
                                        strTipoId = dtFiltrados.Rows[recorrido][PosTipoIden - 1].ToString(),
                                        strNombreCliente = dtFiltrados.Rows[recorrido][PosNombre - 1].ToString();*/
                                    /*string strNroId = dtFiltrados.Rows[0][SenalAlertaPosNumeroIden].ToString(),
                                        strTipoId = dtFiltrados.Rows[0][SenalAlertaPosTipoIden].ToString(),
                                        strNombreCliente = dtFiltrados.Rows[0][SenalAlertaPosNombre].ToString();*/
                                    /*string strNroId = rowDatosAlerta[0][Convert.ToInt32(SenalAlertaPosNumeroIden) - 1].ToString(),
                                    strTipoId = rowDatosAlerta[0][Convert.ToInt32(SenalAlertaPosTipoIden) - 1].ToString(),
                                    strNombreCliente = rowDatosAlerta[0][Convert.ToInt32(SenalAlertaPosNombre) - 1].ToString();*/
                                    string strNroId = string.Empty,
                                    strTipoId = string.Empty,
                                    strNombreCliente = string.Empty;
                                    try
                                    {
                                        strNroId = tblFiltered.Rows[0][Convert.ToInt32(SenalAlertaPosNumeroIden) - 1].ToString();
                                        strTipoId = tblFiltered.Rows[0][Convert.ToInt32(SenalAlertaPosTipoIden) - 1].ToString();
                                    strNombreCliente = tblFiltered.Rows[0][Convert.ToInt32(SenalAlertaPosNombre) - 1].ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        strErrMsg = ex.Message;
                                    }
                                    
                                    #region Notificacion
                                    //genero alerta y notificacion
                                    strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                                        strNombreCliente, strTipoId, strNroId);
                                    clsDTOSenal objSenalIn = new clsDTOSenal(objT1.StrIdSenal, string.Empty, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                                    try
                                    {
                                        
                                        objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);
                                    }
                                    catch (Exception ex)
                                    {
                                        strErrMsg = ex.Message;
                                    }
                                    

                                    //cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                                    //    strTextoCliente, ref strErrMsg);
                                    #endregion

                                    #region Registro de operacion
                                    try
                                    {
                                        mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                                        string strIndicador = string.Format("{0} = {1}", objT1.StrInfo, decResultado);

                                        mtdInsertarRegOperacion(Convert.ToInt32(strIdUsuario), strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                                            1, 1, 0, 0, string.Empty,// IdConteo,// Cant,// Valor, // Frecuencia,// TipoCliente,
                                            objSenalOut.StrIdSenal, objSenalOut.StrDescripcionSenal, strIndicador,
                                            ref strErrMsg, objSenalOut.StrCodigoSenal);
                                    }
                                    catch(Exception ex)
                                    {
                                        strErrMsg = ex.Message;
                                    }
                                    
                                    #endregion
                                }
                                identificador++;
                            }
                        }

                    }
                    catch(Exception ex)
                    {
                        strErrMsg = ex.Message;
                    }
                    

                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                    recorrido++;
                }
                #endregion
            }
        }

        public static DataTable Sum(string i_sGroupByColumn, string i_sAggregateColumn, DataTable i_dSourceTable)
        {
            DataView dv = new DataView(i_dSourceTable);

            //getting distinct values for group column
            DataTable dtResult = i_dSourceTable.Clone();
            DataTable dtSum = dv.ToTable(true, new string[] { i_sGroupByColumn });
            //DataTable dtSum = dtSum = i_dSourceTable.Clone();
            //adding column for the row count
            //dtResult.Columns.Add("Result", typeof(decimal));
            dtSum.Columns.Add("Sum", typeof(decimal));
            //dtSum.Columns.Add("Count", typeof(decimal));

            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtSum.Rows)
            {
                dr["Sum"] = i_dSourceTable.Compute("Sum(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
                //dr["Count"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
                //dr[i_sAggregateColumn] = i_dSourceTable.Compute("Sum(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
            }

            //returning grouped/counted result
            return dtSum;
        }

        private bool mtdCompararValores(string strOperacion, decimal decValorEvaluar, ref string strErrMsg)
        {
            bool booResult = false, booIsNumber = false;
            string[] strPartes = strOperacion.Replace("'", "").Split(' ');

            //if (clsUtilidades.mtdEsNumero(strPartes[1]))
            try
            {
                if (clsUtilidades.mtdEsNumeroNew(strPartes[1]))
                    booIsNumber = true;
                else
                    strErrMsg = "No se puede evaluar la señal por el tipo de comparación";
            }
            catch(Exception ex)
            {
                strErrMsg = ex.Message;
            }
            

            if (booIsNumber)
            {
                try
                {
                    switch (strPartes[0])
                    {
                        case "<":
                            if (decValorEvaluar < Convert.ToDecimal(strPartes[1]))
                                booResult = true;
                            break;
                        case ">":
                            if (decValorEvaluar > Convert.ToDecimal(strPartes[1]))
                                booResult = true;
                            break;
                        case "<=":
                            if (decValorEvaluar <= Convert.ToDecimal(strPartes[1]))
                                booResult = true;
                            break;
                        case ">=":
                            if (decValorEvaluar >= Convert.ToDecimal(strPartes[1]))
                                booResult = true;
                            break;
                        case "=":
                            if (decValorEvaluar == Convert.ToDecimal(strPartes[1]))
                                booResult = true;
                            break;
                    }
                }
                catch(Exception ex)
                {
                    strErrMsg = ex.Message;
                }
                
            }

            return booResult;
        }

        public void mtdEjecutarSenalManual(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, int intIdUsuario, string strNombreUsuario, ref string strErrMsg)
        {
            #region Variables Formulas para filtros
            int intContador = 1;
            string strVariableRango = string.Empty, strFiltro = string.Empty, strFiltroTotal = string.Empty;
            bool booIsFirst = true;
            DataTable dtSerializedData = new DataTable(), dtFiltrados;
            DataRow dr;

            clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
            clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();

            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();
            #endregion

            #region Crea la estructura
            foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
            {
                dtSerializedData.Columns.Add(objEstructura.StrNombreCampo, typeof(string));
            }
            #endregion

            #region Serializacion de los datos e insercion en la estructura
            dr = dtSerializedData.NewRow();
            foreach (DataRow drInfo in dtInfoCargada.Rows)
            {
                if (Convert.ToInt32(drInfo["Posicion"].ToString()) < intContador)
                {
                    dtSerializedData.Rows.Add(dr);
                    dr = dtSerializedData.NewRow();
                    intContador = 1;
                }

                dr[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString();
                intContador++;
            }
            dtSerializedData.Rows.Add(dr);
            #endregion

            #region Creacion de Formulas para filtros
            //Primero genero la formula de filtros individuales
            #region Recorrido Formula
            string strIdSenal = string.Empty;
            List<string> lstCamposResultado = new List<string>();

            foreach (clsDTOSenalVariable objFormula in lstFormulas)
            {
                strIdSenal = objFormula.StrIdSenal;

                #region Operacion
                switch (objFormula.StrIdOperando)
                {
                    case "1"://Variable
                        #region Variable
                        objCampoIn = new clsDTOEstructuraCampo(string.Empty, objFormula.StrValor, string.Empty,
                            string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, true, false);
                        objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);
                        lstCamposResultado.Add(objCampoOut.StrNombreCampo);

                        if (string.IsNullOrEmpty(strFiltro))
                            strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                        else
                            strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                        strVariableRango = objCampoOut.StrNombreCampo;
                        #endregion

                        break;
                    case "2": //Operador
                        #region Operador
                        objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                        objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                        if (objOpOut.StrIdentificadorOperador == "Entre")
                            strFiltro += ">= ";
                        else
                            strFiltro += objOpOut.StrIdentificadorOperador + " ";
                        #endregion

                        break;
                    case "3": //Otro Valor
                        #region Otro Valor

                        strFiltro = strFiltro + " " + objFormula.StrValor + " ";

                        #endregion

                        break;
                    case "4": //Rangos
                        #region Rangos
                        if (booIsFirst)
                        {
                            #region Rango Inferior
                            strFiltro = strFiltro + " " + objFormula.StrValor + " ";
                            #endregion

                            booIsFirst = false;
                        }
                        else
                        {
                            #region Rango Superior
                            strFiltro = strFiltro + " AND " + strVariableRango + " <= " + objFormula.StrValor + " ";
                            #endregion
                        }
                        #endregion

                        break;
                }
                #endregion

                if (!string.IsNullOrEmpty(strErrMsg))
                    break;
            }
            #endregion
            #endregion

            if (string.IsNullOrEmpty(strErrMsg))
            {
                #region Filtros
                dtFiltrados = dtSerializedData.Select(strFiltro).CopyToDataTable();
                #endregion

                #region Generacion de Registros
                if (dtFiltrados.Rows.Count != 0)
                {
                    foreach (DataRow drFiltrados in dtFiltrados.Rows)
                    {
                        string strNroId = drFiltrados[SenalAlertaPosNumeroIden].ToString(),
                            strTipoId = drFiltrados[SenalAlertaPosTipoIden].ToString(),
                            strNombreCliente = drFiltrados[SenalAlertaPosNombre].ToString();

                        #region Notificacion
                        //genero alerta y notificacion
                        string strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                            strNombreCliente, strTipoId, strNroId);

                        clsDTOSenal objSenalIn = new clsDTOSenal(strIdSenal, string.Empty, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                        objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);

                        cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                            strTextoCliente, ref strErrMsg);
                        #endregion

                        #region Recorrido prara generar el indicador
                        string strIndicador = "Filtro: " + strFiltro + " Resultado: ";
                        foreach (string strCampoResultado in lstCamposResultado)
                        {
                            strIndicador = strIndicador + " " + strCampoResultado + "->" + drFiltrados[strCampoResultado].ToString();
                        }
                        #endregion

                        #region Registro de operacion

                        mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                        mtdInsertarRegOperacion(intIdUsuario, strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                            1, 1, 0, 0,// IdConteo,// Cant,// Valor, // Frecuencia,
                            string.Empty,// TipoCliente,
                            objSenalOut.StrIdSenal, objSenalOut.StrDescripcionSenal, strIndicador,
                            ref strErrMsg, objSenalOut.StrCodigoSenal);
                        #endregion
                    }
                }
                #endregion
            }
        }

        public void mtdEjecutarSenalManual(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, int intIdUsuario, string strNombreUsuario, ref int intOcurrencias, ref string strErrMsg)
        {
            try
            {
                #region Variables Formulas para filtros
                string strVariableRango = string.Empty, strFiltro = string.Empty, strFiltroTotal = string.Empty;
                bool booIsFirst = true;
                DataTable dtSerializedData = new DataTable(), dtFiltrados = new DataTable();
                bool booFormulaEntre = false;
                clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
                clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();

                clsParamArchivo cParam = new clsParamArchivo();
                clsUtilidades cUtil = new clsUtilidades();
                #endregion

                #region Creacion de Formulas para filtros
                //Primero genero la formula de filtros individuales
                #region Recorrido Formula
                string strIdSenal = string.Empty;
                List<string> lstCamposResultado = new List<string>();
                string[] strCamposEnteros = new string[10];
                int intCounterI = 0;
                // Contador variables
                int contadorVariables = 0;
                int contadorExpresion = 0;
                foreach (clsDTOSenalVariable objFormula in lstFormulas)
                {
                    strIdSenal = objFormula.StrIdSenal;
                    if (objFormula.BooEsGlobal)
                        continue;

                    // Se valida si están comparando 2 variables en la expresión
                    if (contadorExpresion < 3)
                    {
                        if (objFormula.StrIdOperando == "1")
                        {
                            contadorVariables++;
                        }
                    }
                    else
                    {
                        if (objFormula.StrIdOperando == "1")
                        {
                            contadorVariables = 1;
                            contadorExpresion = 0;
                        }
                        else
                        {
                            contadorExpresion = 0;
                            contadorVariables = 0;
                        }
                    }

                    #region Operacion
                    switch (objFormula.StrIdOperando)
                    {
                        case "1"://Variable
                            #region Variable
                            objCampoIn = new clsDTOEstructuraCampo(string.Empty, objFormula.StrValor, string.Empty,
                                string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, true, false);
                            objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);
                            lstCamposResultado.Add(objCampoOut.StrNombreCampo);

                            if (string.IsNullOrEmpty(strFiltro))
                                strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                            else if (contadorVariables == 2)
                                strFiltro = strFiltro + "[" + objCampoOut.StrNombreCampo + "] ";
                            else
                                strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                            strVariableRango = objCampoOut.StrNombreCampo;
                            #endregion

                            break;
                        case "2": //Operador
                            #region Operador
                            objOpIn = new clsDTOOperador(objFormula.StrValor.Replace('.', ','), string.Empty, string.Empty);
                            objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                            if (objOpOut.StrIdentificadorOperador == "Entre")
                            {
                                booFormulaEntre = true;
                                strFiltro += ">= ";
                            }
                            else
                                strFiltro += objOpOut.StrIdentificadorOperador + " ";
                            #endregion

                            break;
                        case "3": //Otro Valor
                            #region Otro Valor

                            strFiltro = strFiltro + $" '{objFormula.StrValor.Replace('.', ',')}' ";

                            #region Campo Entero
                            if (mtdEsNumero(objFormula.StrValor))
                            {
                                strCamposEnteros[intCounterI] = strVariableRango;
                                intCounterI++;

                            }
                            #endregion
                            #endregion

                            break;
                        case "4": //Rangos
                            #region Rangos
                            if (booIsFirst)
                            {
                                #region Rango Inferior
                                strFiltro = strFiltro + " " + objFormula.StrValor + " ";
                                #endregion

                                #region Campo Entero
                                if (mtdEsNumero(objFormula.StrValor))
                                {
                                    strCamposEnteros[intCounterI] = strVariableRango;
                                    intCounterI++;

                                }
                                #endregion

                                booIsFirst = false;
                            }
                            else
                            {
                                #region Rango Superior
                                strFiltro = strFiltro + " AND " + strVariableRango + " <= " + objFormula.StrValor + " ";
                                #endregion
                            }
                            #endregion

                            break;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;

                    contadorExpresion++;
                }
                #endregion
                #endregion

                #region Crea la estructura
                bool flagNumerico1 = false;
                bool flagNumerico2 = false;
                int posicion1 = 0;
                int posicion2 = 0;
                foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                {
                    bool booIsInteger = false;
                    foreach (string strCampoTmp in strCamposEnteros)
                    {
                        if (strCampoTmp == objEstructura.StrNombreCampo)
                        {
                            booIsInteger = true;
                            break;
                        }
                    }
                    string Delimiter = ">";
                    string[] formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    Delimiter = "<";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    Delimiter = "=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    Delimiter = ">=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = "<=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    if (booIsInteger)
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo.ToUpper().Trim(), typeof(decimal));
                    else
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo.ToUpper().Trim(), typeof(string));
                }
                #endregion

                #region Serializacion de los datos e insercion en la estructura
                int lineaActual = 2;
                object[] array = new object[dtSerializedData.Columns.Count];

                // Llena el DataTable donde se aplicarán los filtros
                int iteracion = 1;
                foreach (DataRow drInfo in dtInfoCargada.Rows)
                {
                    if (lineaActual == Convert.ToInt32(drInfo["NumeroLinea"].ToString()))
                    {
                        bool flag = mtdEsNumero(drInfo["ValorCampoArchivo"].ToString().Replace("\"", ""));
                        if (flag == true)
                            array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "").Replace('.', ',');
                        else
                            array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "");
                        if (dtInfoCargada.Rows.Count == iteracion)
                            dtSerializedData.Rows.Add(array);
                    }
                    else
                    {
                        dtSerializedData.Rows.Add(array);
                        lineaActual = Convert.ToInt32(drInfo["NumeroLinea"].ToString());
                        array = new object[dtSerializedData.Columns.Count];
                        bool flag = mtdEsNumero(drInfo["ValorCampoArchivo"].ToString().Replace("\"", ""));
                        if (flag == true)
                            array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "").Replace('.', ',');
                        else
                            array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "");
                    }
                    iteracion++;
                }
                //dtSerializedData.Rows.Add(dr);
                #endregion
                int a = dtSerializedData.Rows.Count;
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    //if (!string.IsNullOrEmpty(strFiltro))
                    //{
                    #region Filtros
                    /*string selectFiltro = string.Format("{0}",
                        strFiltro.ToUpper().Trim(), strFiltro);*/
                    if (flagNumerico1 == true && flagNumerico2 == true)
                    {
                        dtFiltrados = dtSerializedData.Clone();
                        foreach (DataRow row in dtSerializedData.Rows)
                        {

                            double valor1 = Convert.ToDouble(row[posicion1 - 1].ToString());
                            double valor2 = Convert.ToDouble(row[posicion2 - 1].ToString());
                            foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                            {
                                string Delimiter = ">=";
                                string[] formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                if (formula.Count() > 1)
                                {
                                    if (valor1 >= valor2)
                                    {
                                        dtFiltrados.Rows.Add(row.ItemArray);
                                        break;
                                    }

                                }
                                Delimiter = "<=";
                                formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                if (formula.Count() > 1)
                                {
                                    if (valor1 <= valor2)
                                    {
                                        dtFiltrados.Rows.Add(row.ItemArray);
                                        break;
                                    }
                                }
                                Delimiter = ">";
                                formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                if (formula.Count() > 1)
                                {
                                    if (valor1 > valor2)
                                    {
                                        dtFiltrados.Rows.Add(row.ItemArray);
                                        break;
                                    }

                                }
                                Delimiter = "<";
                                formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                if (formula.Count() > 1)
                                {
                                    if (valor1 < valor2)
                                    {
                                        dtFiltrados.Rows.Add(row.ItemArray);
                                        break;
                                    }
                                }
                                Delimiter = "=";
                                formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                if (formula.Count() > 1)
                                {
                                    if (valor1 == valor2)
                                    {
                                        dtFiltrados.Rows.Add(row.ItemArray);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (booFormulaEntre == true)
                    {
                        string newFiltro = string.Empty;
                        dtFiltrados = dtSerializedData.Clone();
                        DataTable dtPrefiltro = dtSerializedData.Clone();
                        string DelimiterAnd = "AND";
                        string[] variablesFiltro = strFiltro.Split(new[] { DelimiterAnd }, StringSplitOptions.None);
                        int varAnt = variablesFiltro.Count() - 2;
                        if (variablesFiltro.Count() > 1)
                        {
                            //string DelimiterForFiltro = ">=";
                            for (int i = 0; i < variablesFiltro.Count(); i++)
                            {
                                //string[] formula = variablesFiltro[i].Split(new[] { DelimiterForFiltro }, StringSplitOptions.None);
                                if (i < varAnt)//formula.Count() == 1 &&
                                {
                                    if (newFiltro == string.Empty)
                                        newFiltro = variablesFiltro[i].ToString();
                                    else
                                        newFiltro = newFiltro + " AND " + variablesFiltro[i].ToString();
                                }/*else
                                {
                                    DelimiterForFiltro = "<=";
                                }*/
                            }
                        }
                        if (dtSerializedData.Select(newFiltro.ToUpper().Trim()).Count() > 0)
                            dtPrefiltro = dtSerializedData.Select(newFiltro).CopyToDataTable();
                        if (dtPrefiltro.Rows.Count == 0)
                            dtPrefiltro = dtSerializedData;
                        foreach (DataRow row in dtPrefiltro.Rows)
                        {
                            int posicion = 0;
                            string variable = string.Empty;
                            decimal valor1 = 0;
                            decimal valor2 = 0;
                            foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                            {
                                string Delimiter = "AND";
                                string[] variables = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                varAnt = variables.Count() - 2;
                                if (variables.Count() > 1)
                                {
                                    string DelimiterFor = ">=";
                                    for (int i = 0; i < variables.Count(); i++)
                                    {
                                        string[] formula = variables[i].Split(new[] { DelimiterFor }, StringSplitOptions.None);
                                        if (formula.Count() > 1 && i >= varAnt)
                                        {
                                            variable = formula[0].ToString().Trim();
                                            valor1 = Convert.ToDecimal(formula[1].Replace('.', ',').ToString().Trim());
                                        }
                                    }
                                    DelimiterFor = "<=";
                                    for (int i = 0; i < variables.Count(); i++)
                                    {
                                        string[] formula = variables[i].Split(new[] { DelimiterFor }, StringSplitOptions.None);
                                        if (formula.Count() > 1 && i >= varAnt)
                                        {
                                            valor2 = Convert.ToDecimal(formula[1].Replace('.', ',').ToString().Trim());
                                        }

                                    }
                                }
                                variable = variable.Replace("[", string.Empty);
                                variable = variable.Replace("]", string.Empty);
                                if (objEstructura.StrNombreCampo == variable)
                                {
                                    posicion = Convert.ToInt32(objEstructura.StrPosicion);
                                }


                            }

                            decimal valorData = Convert.ToDecimal(row[posicion - 1].ToString());//.Replace('.', ',')
                            if (valorData >= valor1 && valorData <= valor2)
                                dtFiltrados.Rows.Add(row.ItemArray);
                        }
                    }
                    else if (dtSerializedData.Select(strFiltro.ToUpper().Trim()).Count() > 0)
                        dtFiltrados = dtSerializedData.Select(strFiltro).CopyToDataTable();
                    //DataRow[] foundRows;
                    //foundRows = dtSerializedData.Select("[TIPO IDENTIFICACION] = '1'");
                    //foundRows = dtSerializedData.Select(strFiltro);
                    // Print column 0 of each returned row.
                    /*for (int i = 0; i < foundRows.Length; i++)
                    {
                        Console.WriteLine(foundRows[i][0]);
                    }*/
                    #endregion

                    #region Generacion de Registros
                    if (dtFiltrados.Rows.Count != 0)
                    {
                        intOcurrencias = intOcurrencias + dtFiltrados.Rows.Count;
                        foreach (DataRow drFiltrados in dtFiltrados.Rows)
                        {
                            try
                            {
                                string strNroId = drFiltrados[SenalAlertaPosNumeroIdenCabecero].ToString(),
                                    strTipoId = drFiltrados[SenalAlertaPosTipoIdenCabecero].ToString(),
                                    strNombreCliente = drFiltrados[SenalAlertaPosNombreCabecero].ToString();

                                #region Notificacion
                                //genero alerta y notificacion
                                string strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                                    strNombreCliente, strTipoId, strNroId);

                                clsDTOSenal objSenalIn = new clsDTOSenal(strIdSenal, string.Empty, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                                objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);
                                /*cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                                    strTextoCliente, ref strErrMsg);*/
                                //Se inhabilita por no estar acorde a la funcionalidad.
                                #endregion

                                #region Recorrido prara generar el indicador
                                string strIndicador = "Filtro: " + strFiltro + " Resultado: ";
                                foreach (string strCampoResultado in lstCamposResultado)
                                {
                                    strIndicador = strIndicador + " " + strCampoResultado + "->" + drFiltrados[strCampoResultado].ToString();
                                }
                                #endregion

                                #region Registro de operacion

                                mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                                mtdInsertarRegOperacion(intIdUsuario, strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                                    1, 1, 0, 0,// IdConteo,// Cant,// Valor, // Frecuencia,
                                    string.Empty,// TipoCliente,
                                    objSenalOut.StrIdSenal, objSenalOut.StrDescripcionSenal, strIndicador.Replace("'", " ").Trim(),
                                    ref strErrMsg, objSenalOut.StrCodigoSenal);
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    #endregion
                    //}
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public void mtdEjecutarSenalManual(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, int intIdUsuario, string strNombreUsuario, ref int intOcurrencias, ref string strErrMsg, int idJerarquia)
        {
            try
            {
                #region Variables Formulas para filtros
                string strVariableRango = string.Empty, strFiltro = string.Empty, strFiltroTotal = string.Empty;
                bool booIsFirst = true;
                DataTable dtSerializedData = new DataTable(), dtFiltrados = new DataTable();
                bool booFormulaEntre = false;
                clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
                clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();

                clsParamArchivo cParam = new clsParamArchivo();
                clsUtilidades cUtil = new clsUtilidades();
                #endregion

                #region Creacion de Formulas para filtros
                //Primero genero la formula de filtros individuales
                #region Recorrido Formula
                string strIdSenal = string.Empty;
                List<string> lstCamposResultado = new List<string>();
                string[] strCamposEnteros = new string[10];
                int intCounterI = 0;
                // Contador variables
                int contadorVariables = 0;
                int contadorExpresion = 0;
                foreach (clsDTOSenalVariable objFormula in lstFormulas)
                {
                    strIdSenal = objFormula.StrIdSenal;
                    if (objFormula.BooEsGlobal)
                        continue;

                    // Se valida si están comparando 2 variables en la expresión
                    if (contadorExpresion < 3)
                    {
                        if (objFormula.StrIdOperando == "1")
                        {
                            contadorVariables++;
                        }
                    }
                    else
                    {
                        if (objFormula.StrIdOperando == "1")
                        {
                            contadorVariables = 1;
                            contadorExpresion = 0;
                        }
                        else
                        {
                            contadorExpresion = 0;
                            contadorVariables = 0;
                        }
                    }

                    #region Operacion
                    switch (objFormula.StrIdOperando)
                    {
                        case "1"://Variable
                            #region Variable
                            objCampoIn = new clsDTOEstructuraCampo(string.Empty, objFormula.StrValor, string.Empty,
                                string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, true, false);
                            objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);
                            lstCamposResultado.Add(objCampoOut.StrNombreCampo);

                            if (string.IsNullOrEmpty(strFiltro))
                                strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                            else if (contadorVariables == 2)
                                strFiltro = strFiltro + "[" + objCampoOut.StrNombreCampo + "] ";
                            else
                                strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                            strVariableRango = objCampoOut.StrNombreCampo;
                            #endregion

                            break;
                        case "2": //Operador
                            #region Operador
                            objOpIn = new clsDTOOperador(objFormula.StrValor.Replace('.', ','), string.Empty, string.Empty);
                            objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                            if (objOpOut.StrIdentificadorOperador == "Entre")
                            {
                                booFormulaEntre = true;
                                strFiltro += ">= ";
                            }
                            else
                                strFiltro += objOpOut.StrIdentificadorOperador + " ";
                            #endregion

                            break;
                        case "3": //Otro Valor
                            #region Otro Valor

                            strFiltro = strFiltro + $" '{objFormula.StrValor.Replace('.', ',')}' ";

                            #region Campo Entero
                            if (mtdEsNumero(objFormula.StrValor))
                            {
                                strCamposEnteros[intCounterI] = strVariableRango;
                                intCounterI++;

                            }
                            #endregion
                            #endregion

                            break;
                        case "4": //Rangos
                            #region Rangos
                            if (booIsFirst)
                            {
                                #region Rango Inferior
                                strFiltro = strFiltro + " " + objFormula.StrValor + " ";
                                #endregion

                                #region Campo Entero
                                if (mtdEsNumero(objFormula.StrValor))
                                {
                                    strCamposEnteros[intCounterI] = strVariableRango;
                                    intCounterI++;

                                }
                                #endregion

                                booIsFirst = false;
                            }
                            else
                            {
                                #region Rango Superior
                                strFiltro = strFiltro + " AND " + strVariableRango + " <= " + objFormula.StrValor + " ";
                                #endregion
                            }
                            #endregion

                            break;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;

                    contadorExpresion++;
                }
                #endregion
                #endregion

                #region Crea la estructura
                bool flagNumerico1 = false;
                bool flagNumerico2 = false;
                int posicion1 = 0;
                int posicion2 = 0;
                foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                {
                    bool booIsInteger = false;
                    foreach (string strCampoTmp in strCamposEnteros)
                    {
                        if (strCampoTmp == objEstructura.StrNombreCampo)
                        {
                            booIsInteger = true;
                            break;
                        }
                    }
                    string Delimiter = ">";
                    string[] formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = "<";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = "=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = ">=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    Delimiter = "<=";
                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[0].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico1 = true;
                                posicion1 = Convert.ToInt32(objEstructura.StrPosicion);
                            }
                        }
                    }
                    if (formula.Count() > 1)
                    {
                        if ("[" + objEstructura.StrNombreCampo + "]" == formula[1].ToString().Trim())
                        {
                            if (objEstructura.BoolNumerico == true)
                            {
                                flagNumerico2 = true;
                                posicion2 = Convert.ToInt32(objEstructura.StrPosicion);
                            }

                        }
                    }
                    if (booIsInteger)
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo.ToUpper().Trim(), typeof(decimal));
                    else
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo.ToUpper().Trim(), typeof(string));
                }
                #endregion

                #region Serializacion de los datos e insercion en la estructura
                int lineaActual = 2;
                object[] array = new object[dtSerializedData.Columns.Count];

                // Llena el DataTable donde se aplicarán los filtros
                int iteracion = 1;
                foreach (DataRow drInfo in dtInfoCargada.Rows)
                {
                    if (lineaActual == Convert.ToInt32(drInfo["NumeroLinea"].ToString()))
                    {
                        bool flag = mtdEsNumero(drInfo["ValorCampoArchivo"].ToString().Replace("\"", ""));
                        if (flag == true)
                            array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "").Replace('.', ',');
                        else
                            array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "");
                        if (dtInfoCargada.Rows.Count == iteracion)
                            dtSerializedData.Rows.Add(array);
                    }
                    else
                    {
                        dtSerializedData.Rows.Add(array);
                        lineaActual = Convert.ToInt32(drInfo["NumeroLinea"].ToString());
                        array = new object[dtSerializedData.Columns.Count];
                        bool flag = mtdEsNumero(drInfo["ValorCampoArchivo"].ToString().Replace("\"", ""));
                        if (flag == true)
                            array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "").Replace('.', ',');
                        else
                            array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "");
                    }
                    iteracion++;
                }
                //dtSerializedData.Rows.Add(dr);
                #endregion
                int a = dtSerializedData.Rows.Count;
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (!string.IsNullOrEmpty(strFiltro))
                    {
                        //
                        //{
                        #region Filtros
                        /*string selectFiltro = string.Format("{0}",
                            strFiltro.ToUpper().Trim(), strFiltro);*/
                        if (flagNumerico1 == true && flagNumerico2 == true)
                        {
                            dtFiltrados = dtSerializedData.Clone();
                            foreach (DataRow row in dtSerializedData.Rows)
                            {

                                double valor1 = Convert.ToDouble(row[posicion1 - 1].ToString());
                                double valor2 = Convert.ToDouble(row[posicion2 - 1].ToString());
                                foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                                {
                                    string Delimiter = ">=";
                                    string[] formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                    if (formula.Count() > 1)
                                    {
                                        if (valor1 >= valor2)
                                        {
                                            dtFiltrados.Rows.Add(row.ItemArray);
                                            break;
                                        }

                                    }
                                    Delimiter = "<=";
                                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                    if (formula.Count() > 1)
                                    {
                                        if (valor1 <= valor2)
                                        {
                                            dtFiltrados.Rows.Add(row.ItemArray);
                                            break;
                                        }
                                    }
                                    Delimiter = ">";
                                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                    if (formula.Count() > 1)
                                    {
                                        if (valor1 > valor2)
                                        {
                                            dtFiltrados.Rows.Add(row.ItemArray);
                                            break;
                                        }

                                    }
                                    Delimiter = "<";
                                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                    if (formula.Count() > 1)
                                    {
                                        if (valor1 < valor2)
                                        {
                                            dtFiltrados.Rows.Add(row.ItemArray);
                                            break;
                                        }
                                    }
                                    Delimiter = "=";
                                    formula = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                    if (formula.Count() > 1)
                                    {
                                        if (valor1 == valor2)
                                        {
                                            dtFiltrados.Rows.Add(row.ItemArray);
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                        else if (booFormulaEntre == true)
                        {
                            string newFiltro = string.Empty;
                            dtFiltrados = dtSerializedData.Clone();
                            DataTable dtPrefiltro = dtSerializedData.Clone();
                            string DelimiterAnd = "AND";
                            string[] variablesFiltro = strFiltro.Split(new[] { DelimiterAnd }, StringSplitOptions.None);
                            int varAnt = variablesFiltro.Count() - 2;
                            if (variablesFiltro.Count() > 1)
                            {
                                //string DelimiterForFiltro = ">=";
                                for (int i = 0; i < variablesFiltro.Count(); i++)
                                {
                                    //string[] formula = variablesFiltro[i].Split(new[] { DelimiterForFiltro }, StringSplitOptions.None);
                                    if (i < varAnt)//formula.Count() == 1 &&
                                    {
                                        if (newFiltro == string.Empty)
                                            newFiltro = variablesFiltro[i].ToString();
                                        else
                                            newFiltro = newFiltro + " AND " + variablesFiltro[i].ToString();
                                    }/*else
                                {
                                    DelimiterForFiltro = "<=";
                                }*/
                                }
                            }
                            if (dtSerializedData.Select(newFiltro.ToUpper().Trim()).Count() > 0)
                                dtPrefiltro = dtSerializedData.Select(newFiltro).CopyToDataTable();
                            if (dtPrefiltro.Rows.Count == 0)
                                dtPrefiltro = dtSerializedData;
                            foreach (DataRow row in dtPrefiltro.Rows)
                            {
                                int posicion = 0;
                                string variable = string.Empty;
                                decimal valor1 = 0;
                                decimal valor2 = 0;
                                foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                                {
                                    string Delimiter = "AND";
                                    string[] variables = strFiltro.Split(new[] { Delimiter }, StringSplitOptions.None);
                                    varAnt = variables.Count() - 2;
                                    if (variables.Count() > 1)
                                    {
                                        string DelimiterFor = ">=";
                                        for (int i = 0; i < variables.Count(); i++)
                                        {
                                            string[] formula = variables[i].Split(new[] { DelimiterFor }, StringSplitOptions.None);
                                            if (formula.Count() > 1 && i >= varAnt)
                                            {
                                                variable = formula[0].ToString().Trim();
                                                valor1 = Convert.ToDecimal(formula[1].Replace('.', ',').ToString().Trim());
                                            }

                                        }
                                        DelimiterFor = "<=";
                                        for (int i = 0; i < variables.Count(); i++)
                                        {
                                            string[] formula = variables[i].Split(new[] { DelimiterFor }, StringSplitOptions.None);
                                            if (formula.Count() > 1 && i >= varAnt)
                                            {
                                                valor2 = Convert.ToDecimal(formula[1].Replace('.', ',').ToString().Trim());
                                            }

                                        }
                                    }
                                    variable = variable.Replace("[", string.Empty);
                                    variable = variable.Replace("]", string.Empty);
                                    if (objEstructura.StrNombreCampo == variable)
                                    {
                                        posicion = Convert.ToInt32(objEstructura.StrPosicion);
                                    }


                                }

                                decimal valorData = Convert.ToDecimal(row[posicion - 1].ToString());//.Replace('.', ',')
                                if (valorData >= valor1 && valorData <= valor2)
                                    dtFiltrados.Rows.Add(row.ItemArray);
                            }
                        }
                        else if (flagNumerico1 == true)
                        {
                            strFiltro = strFiltro.Replace("[", "Convert([");
                            strFiltro = strFiltro.Replace("]", "], System.Decimal)");
                            //strFiltro = "Convert(DEPOSITOS, System.Decimal) > 10000000";
                            if (dtSerializedData.Select(strFiltro).Count() > 0)
                                dtFiltrados = dtSerializedData.Select(strFiltro).CopyToDataTable();
                        }
                        else if (dtSerializedData.Select(strFiltro.ToUpper().Trim()).Count() > 0)
                            dtFiltrados = dtSerializedData.Select(strFiltro).CopyToDataTable();
                        //DataRow[] foundRows;
                        //foundRows = dtSerializedData.Select("[TIPO IDENTIFICACION] = '1'");
                        //foundRows = dtSerializedData.Select(strFiltro);
                        // Print column 0 of each returned row.
                        /*for (int i = 0; i < foundRows.Length; i++)
                        {
                            Console.WriteLine(foundRows[i][0]);
                        }*/
                        #endregion

                        #region Generacion de Registros
                        if (dtFiltrados.Rows.Count != 0)
                        {
                            intOcurrencias = intOcurrencias + dtFiltrados.Rows.Count;
                            foreach (DataRow drFiltrados in dtFiltrados.Rows)
                            {
                                try
                                {
                                    string strNroId = drFiltrados[SenalAlertaPosNumeroIdenCabecero].ToString(),
                                        strTipoId = drFiltrados[SenalAlertaPosTipoIdenCabecero].ToString(),
                                        strNombreCliente = drFiltrados[SenalAlertaPosNombreCabecero].ToString();

                                    #region Notificacion
                                    //genero alerta y notificacion
                                    string strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                                        strNombreCliente, strTipoId, strNroId);

                                    clsDTOSenal objSenalIn = new clsDTOSenal(string.Empty, strIdSenal, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                                    objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);
                                    /*cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                                        strTextoCliente, ref strErrMsg, idJerarquia);*/
                                    //Se inhabilita por no estar acorde a la funcionalidad.
                                    #endregion

                                    #region Recorrido prara generar el indicador
                                    string strIndicador = "Filtro: " + strFiltro + " Resultado: ";
                                    foreach (string strCampoResultado in lstCamposResultado)
                                    {
                                        strIndicador = strIndicador + " " + strCampoResultado + "->" + drFiltrados[strCampoResultado].ToString();
                                    }
                                    #endregion

                                    #region Registro de operacion

                                    mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                                    mtdInsertarRegOperacion(intIdUsuario, strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                                        1, 1, 0, 0,// IdConteo,// Cant,// Valor, // Frecuencia,
                                        string.Empty,// TipoCliente,
                                        objSenalOut.StrIdSenal, objSenalOut.StrDescripcionSenal, strIndicador.Replace("'", " ").Trim(),
                                        ref strErrMsg, objSenalOut.StrCodigoSenal);
                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public void mtdCapturaDatos()
        {

        }


        /// <summary>
        /// Metodo que permite evaluar si una cadena es un numero.
        /// </summary>
        /// <param name="strNumero"></param>
        /// <returns></returns>
        public bool mtdEsNumero(string strNumero)
        {
            bool booResult = false;
            //Regex regex = new Regex(@"^[0-9]+$");
            Regex regex = new Regex(@"^[0-9]([.][0-9]{1,3})?$");
            if (regex.IsMatch(strNumero))
                booResult = true;

            return booResult;
        }

        #endregion

        /*
         * Metodos para el Servicio
         */
        #region Servicio
        public clsDTOSenal mtdConsultarSenal(string strOleConn, clsDTOSenal objSenalIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenal objSenalOut = new clsDTOSenal();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultarSenal(strOleConn, objSenalIn, ref strErrMsg);

            if (dtInfo != null)
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdSenal], [CodigoSenal], [DescripcionSenal], [EsAutomatico]
                    objSenalOut = new clsDTOSenal(
                        dtInfo.Rows[0]["IdUsuario"].ToString().Trim(),
                        dtInfo.Rows[0]["IdSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["CodigoSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["DescripcionSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["EsAutomatico"].ToString().Trim() == "True" ? true : false);
                }

            return objSenalOut;
        }

        public clsDTOOperador mtdBuscarOperador(string strOleConn, clsDTOOperador objOpIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperador objOp = new clsDTOOperador();
            #endregion Vars

            dtInfo = cDtSenal.mtdBuscarOperador(strOleConn, objOpIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdOperador], [NombreOperador], [IdentificadorOperador]
                    objOp = new clsDTOOperador(dtInfo.Rows[0]["IdOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["NombreOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["IdentificadorOperador"].ToString().Trim());
                }
            }
            return objOp;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormulas(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormulas(strOleConn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormSenalAuto(string strOleConn, bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormSenalAuto(strOleConn, booAutomatico, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        #region Tabla Procesos
        public void mtdInsertarNroRegistrosSA(string strOleConn, int intNroRegistros, string strNombreUsuario, string strDescripcion, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdInsertarNroRegistrosSA(strOleConn, intNroRegistros, strNombreUsuario, strDescripcion, ref strErrMsg);
        }

        public int mtdConteoRegistros(string strOleConn, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            int intConteo = 0;

            intConteo = cDtSenal.mtdConteoRegistros(strOleConn, ref strErrMsg);

            return intConteo;
        }

        public void mtdInsertarRegOperacion(string strOleConn, int intIdUsuario,
            string strIdentificacion, string strNombreApellido,
            int intConteoTblConteoRegistro, int intConteoOcurrencias,// IdConteo // Cant
            int intValor, int intFrecuencia, string strTipoCliente,
            string strCodigoSenal, string strDescripcionSenal,// IdSenal // DescSenal
            string strIndicador,
            ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            cDtSenal.mtdInsertarRegOperacion(strOleConn, intIdUsuario,
                strIdentificacion, strNombreApellido,
                intConteoTblConteoRegistro,// IdConteo
                intConteoOcurrencias,// Cant
                intValor, intFrecuencia, strTipoCliente,
                strCodigoSenal,// IdSenal
                strDescripcionSenal, strIndicador, ref strErrMsg);
        }
        #endregion
        #endregion
    }

    public class clsTupla
    {
        int _intPos;
        string _strInfo;
        string _strIdSenal;

        public string StrIdSenal
        {
            get { return _strIdSenal; }
            set { _strIdSenal = value; }
        }

        public int IntPos
        {
            get { return _intPos; }
            set { _intPos = value; }
        }

        public string StrInfo
        {
            get { return _strInfo; }
            set { _strInfo = value; }
        }

        public clsTupla()
        {
        }

        public clsTupla(int intPos, string strInfo, string strIdSenal)
        {
            IntPos = intPos;
            StrInfo = strInfo;
            StrIdSenal = strIdSenal;
        }
    }
}
