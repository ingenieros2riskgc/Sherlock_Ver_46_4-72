using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;

namespace ListasSarlaft.Classes
{
    public class clsSeguimientoIndicadorBLL
    {
        /// <summary>
        /// Metodo para consultar la informacion de seguimiento del indicador
        /// </summary>
        /// <param name="objIndicador">Objet con la informacion del indicador</param>
        /// <param name="lstDetSegIndicador">Lista de detalles del seguimiento del indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        public bool mtdConsultarSeguimiento(clsIndicador objIndicador,
           ref List<clsDetalleSeguimientoIndicador> lstDetSegIndicador, ref string strErrMsg, int periodoAnual)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtSeguimientoIndicador cDtSegIndicador = new clsDtSeguimientoIndicador();
            clsDetalleSeguimientoIndicador objDetSegIndicador = new clsDetalleSeguimientoIndicador();
            #endregion Vars

            booResult = cDtSegIndicador.mtdConsultarSeguimiento(objIndicador, ref dtInfo, ref strErrMsg, periodoAnual);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            objDetSegIndicador = new clsDetalleSeguimientoIndicador(
                                Convert.ToInt32(dr["IdDetSegIndicador"].ToString().Trim()),
                                Convert.ToInt32(dr["IdSegIndicador"].ToString().Trim()),
                                Convert.ToInt32(dr["IdIndicador"].ToString().Trim()),
                                dr["DescripcionAnalisis"].ToString().Trim(),
                                dr["DescripcionAccionCorrectiva"].ToString().Trim(),
                                Convert.ToInt32(dr["IdDetPeriodo"].ToString().Trim()),
                                dr["FechaDescAnalisis"].ToString().Trim(),
                                dr["FechaAccionCorrectiva"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());

                            lstDetSegIndicador.Add(objDetSegIndicador);
                        }
                    }
                    else
                        lstDetSegIndicador = null;
                }
                else
                    lstDetSegIndicador = null;
            }

            return booResult;
        }

        /// <summary>
        /// Metodo que permite la actualizacion del seguimiento
        /// </summary>
        /// <param name="objDetSegIndicador">Objeto con la informacionce del detalle del seguimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        public bool mtdActualizarSeguimiento(clsDetalleSeguimientoIndicador objDetSegIndicador, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtSeguimientoIndicador cDtSegIndicador = new clsDtSeguimientoIndicador();

            booResult = cDtSegIndicador.mtdActualizarSeguimiento(objDetSegIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo que permite la insercionde un seguimiento
        /// </summary>
        /// <param name="objDetSegIndicador">Objeto con la informacion del seguimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        public bool mtdInsertarSeguimiento(clsDetalleSeguimientoIndicador objDetSegIndicador, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtSeguimientoIndicador cDtSegIndicador = new clsDtSeguimientoIndicador();

            booResult = cDtSegIndicador.mtdInsertarSeguimiento(objDetSegIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metod que consulta toda la informacion para llenar la data del cuadro de mando
        /// </summary>
        /// <param name="objProcInd">Objeto con la informacion del proceso del indicador</param>
        /// <param name="objIndicador">objeto con la informacion del indicador</param>
        /// <param name="dtCuadroMandoOut">Objeto con la informacion del cuadro de mando</param>
        /// <param name="dtCabCuadroOut">Objeto con la informacion de la cabecera del cuadro de mando</param>
        /// <param name="strFormula">Objeto con la indormacion de la formula realizada</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Transaccion exitosa o no</returns>
        public bool mtdConsultarCuadroMando(clsProcesoIndicador objProcInd, clsIndicador objIndicador,
            ref DataTable dtCuadroMandoOut, ref DataTable dtCabCuadroOut, ref string strFormula, ref string strErrMsg
            , int periodoAnual)
        {
            #region Vars
            bool booResult = false;
            DataRow drCuadro, drCuadroOut;
            DataTable dtInfoPeriodo = new DataTable(), dtInfoVariable = new DataTable(), dtInfoCuadro = new DataTable(),
                dtCuadroMandoTemp = new DataTable(), dtInfoForm = new DataTable(), dtInfoCump = new DataTable();

            List<clsFormula> lstFormula = new List<clsFormula>();
            clsPeriodicidad objPeriodicidad = new clsPeriodicidad(objIndicador.intIdPeriodicidad, string.Empty, 0, string.Empty);
            clsFormulaBLL cFormula = new clsFormulaBLL();
            clsDtPeriodicidad cPeriodo = new clsDtPeriodicidad();
            clsDtVariable cVariable = new clsDtVariable();
            clsDtSeguimientoIndicador cSegIndicador = new clsDtSeguimientoIndicador();
            clsDtFormula cForm = new clsDtFormula();
            clsDtCumplimiento cCumplimiento = new clsDtCumplimiento();
            #endregion Vars

            try
            {
                #region Consultas de los diferentes objetos que pertenecen al cuadro de mando

                booResult = cPeriodo.mtdConsultarDetallePeriodo(objPeriodicidad, ref dtInfoPeriodo, ref strErrMsg);
                if (booResult)
                    booResult = cVariable.mtdConsultarVariable(objIndicador, ref dtInfoVariable, ref strErrMsg);

                if (booResult)
                    booResult = cSegIndicador.mtdConsultarCabCuadro(objProcInd, objIndicador, ref dtCabCuadroOut, ref strErrMsg,
                        periodoAnual);

                if (booResult)
                    booResult = cSegIndicador.mtdConsultarInfoCuadro(objProcInd, objIndicador, ref dtInfoCuadro, ref strErrMsg,
                        periodoAnual);

                if (booResult)
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

                if (booResult)
                    booResult = cCumplimiento.mtdConsultarDetalleCalificacion(objIndicador, ref dtInfoCump, ref strErrMsg);
                #endregion

                if (booResult)
                {
                    #region Crear Estructura
                    dtCuadroMandoTemp.Columns.Add("PERIODO", typeof(string));
                    dtCuadroMandoOut.Columns.Add("PERIODO", typeof(string));

                    //VARIABLES
                    foreach (DataRow dr in dtInfoVariable.Rows)
                    {
                        dtCuadroMandoTemp.Columns.Add(dr[1].ToString().Trim(), typeof(string));
                        dtCuadroMandoOut.Columns.Add(dr[1].ToString().Trim(), typeof(string));
                    }

                    dtCuadroMandoTemp.Columns.Add("RESULTADO", typeof(string));
                    dtCuadroMandoTemp.Columns.Add("META", typeof(string));

                    dtCuadroMandoOut.Columns.Add("RESULTADO", typeof(string));
                    dtCuadroMandoOut.Columns.Add("META", typeof(string));
                    dtCuadroMandoOut.Columns.Add("CUMPLIMIENTO", typeof(string));

                    //DataColumn dc = new DataColumn("CUMPLIMIENTO");
                    //dc.DataType = System.Type.GetType("System.Byte[]");
                    //dtCuadroMandoOut.Columns.Add(dc);

                    #endregion

                    #region Ingresar informacion a estructura temporal
                    string strMeta = string.Empty;
                    foreach (DataRow drPeriodo in dtInfoPeriodo.Rows)
                    {
                        drCuadro = dtCuadroMandoTemp.NewRow();
                        drCuadro["PERIODO"] = drPeriodo[2].ToString();

                        string strFiltro = string.Format("[IdDetPeriodo]='{0}'", drPeriodo[0].ToString());
                        foreach (DataRow drInfoCuadro in dtInfoCuadro.Select(strFiltro))
                        {
                            drCuadro[drInfoCuadro["Variable"].ToString().Trim()] = drInfoCuadro["Valor"].ToString();
                            strMeta = drInfoCuadro["Meta"].ToString();
                        }

                        drCuadro["META"] = strMeta;
                        dtCuadroMandoTemp.Rows.Add(drCuadro);
                    }
                    #endregion

                    #region Recorrido para generar el cuadro de mando final
                    foreach (DataRow drTemp in dtCuadroMandoTemp.Rows)
                    {
                        int intCantidad = dtCuadroMandoTemp.Columns.Count;
                        decimal decResultado = 0;
                        string strOperacion = string.Empty;

                        drCuadroOut = dtCuadroMandoOut.NewRow();

                        if (!booResult)
                            break;

                        #region Formula
                        foreach (DataRow drForm in dtInfoForm.Rows)
                        {
                            if (!booResult)
                                break;
                            
                            #region Recorrido ejecucion de formula
                            switch (drForm["IdOperando"].ToString())
                            {
                                case "1"://Variable
                                    string strFiltro = string.Format("[Id]={0}", drForm["Valor"].ToString()),
                                        strNombreVar = string.Empty;
                                    DataRow[] drVar = dtInfoVariable.Select(strFiltro);

                                    strNombreVar = drVar[0][1].ToString();
                                    if (string.IsNullOrEmpty(strOperacion))
                                        //decResultado = Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString());
                                        ///Debug.Assert(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()));
                                        //decResultado = Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                        if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                        {
                                            decResultado = 0;
                                        }
                                        else {
                                            decResultado = Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                        }
                                        else
                                        {
                                            #region Operacion
                                            switch (strOperacion)
                                            {

                                                case "+":
                                                    if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                                    {
                                                        decResultado = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        decResultado = decResultado +
                                                            Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                                        break;
                                                    }
                                                case "-":
                                                    if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                                    {
                                                        decResultado = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        decResultado = decResultado -
                                                            Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                                        break;
                                                    }
                                                case "X":
                                                    if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                                    {
                                                        decResultado = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        decResultado = decResultado *
                                                            Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                                        break;
                                                    }
                                                case "/":
                                                    if (Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString()) != 0)
                                                        if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                                        {
                                                            decResultado = 0;
                                                        }
                                                        else
                                                        {
                                                            decResultado = decResultado /
                                                                Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                                        }
                                                    else
                                                    {
                                                        booResult = false;
                                                        strErrMsg = string.Format("Por favor revisar el valor de la variable [{0}]. [Division por 0] ", strNombreVar);
                                                    }
                                                    break;
                                            }
                                            #endregion
                                        }
                                    break;
                                case "2"://Operacion
                                    strOperacion = drForm["Valor"].ToString();
                                    break;
                                case "3"://OtroValor
                                    #region Operacion
                                    switch (strOperacion)
                                    {
                                        case "+":
                                            if (decResultado == 0)
                                            {
                                                decResultado = 0;
                                                break;
                                            }
                                            else
                                            {
                                                decResultado = decResultado + Convert.ToDecimal(drForm["Valor"].ToString());
                                                break;
                                            }
                                        case "-":
                                            if (decResultado == 0)
                                            {
                                                decResultado = 0;
                                                break;
                                            }
                                            else
                                            {
                                                decResultado = decResultado - Convert.ToDecimal(drForm["Valor"].ToString());
                                                break;
                                            }
                                        case "X":
                                            if (decResultado == 0)
                                            {
                                                decResultado = 0;
                                                break;
                                            }
                                            else
                                            {
                                                decResultado = decResultado * Convert.ToDecimal(drForm["Valor"].ToString());
                                                break;
                                            }
                                        case "/":
                                            if (Convert.ToDecimal(drForm["Valor"].ToString()) != 0)
                                                if (decResultado == 0)
                                                {
                                                    decResultado = 0;
                                                    break;
                                                }
                                                else
                                                {
                                                    decResultado = decResultado / Convert.ToDecimal(drForm["Valor"].ToString());
                                                }
                                            else
                                            {
                                                booResult = false;
                                                strErrMsg = string.Format("Por favor revisar el valor de la formula [{0}]. [Division por 0] ");
                                            }
                                            break;
                                    }
                                    #endregion
                                    break;
                            }
                            #endregion
                        }
                        #endregion

                        #region Cumplimiento
                        string strCumplimiento = string.Empty;
                        //System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        if (booResult)
                        {
                            #region Recorrido de cumplimientos
                            foreach (DataRow drCump in dtInfoCump.Rows)
                            {
                                decimal decValorMin = Convert.ToDecimal(drCump["ValorMinimo"].ToString()),
                                    decValorMax = Convert.ToDecimal(drCump["ValorMaximo"].ToString());

                                if (!(decResultado.ToString().Length == 1 && decResultado == 0))
                                    if ((decValorMin <= decResultado) &&
                                        (decValorMax >= decResultado))
                                    {
                                        strCumplimiento = drCump["NombreCumplimiento"].ToString();

                                        #region Selecciona Imagen Semaforo
                                        //img.ID = "img1";
                                        //img.Visible = true;
                                        //img.AlternateText = drCump["NombreCumplimiento"].ToString();
                                        //switch (drCump["IdSemaforo"].ToString())
                                        //{
                                        //    case "1":

                                        //        //img.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                        //        break;
                                        //    case "2":

                                        //        //img.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                        //        break;
                                        //    case "3":
                                        //        //img.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                        //        break;
                                        //}
                                        #endregion

                                        break;
                                    }
                            }
                            #endregion
                        }
                        #endregion

                        #region Generacion del cuadro de mando
                        if (booResult)
                        {
                            for (int i = 0; i < intCantidad; i++)
                            {
                                drCuadroOut[i] = drTemp[i].ToString();
                            }

                            drCuadroOut["RESULTADO"] = decResultado.ToString("#.##");

                            //var imageConverter = new System.Drawing.ImageConverter();
                            //drCuadroOut["CUMPLIMIENTO"] = imageConverter.ConvertTo(img, System.Type.GetType("System.Byte[]"));
                            drCuadroOut["CUMPLIMIENTO"] = strCumplimiento;
                            dtCuadroMandoOut.Rows.Add(drCuadroOut);
                        }
                        #endregion
                    }
                    #endregion

                    booResult = true;
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el cuadro de mando. [{0}]", ex.Message);
                booResult = false;
            }
            return booResult;
        }
        public bool mtdConsultarCuadroMando(clsProcesoIndicador objProcInd, clsIndicador objIndicador,
                    ref DataTable dtCuadroMandoOut, ref DataTable dtCabCuadroOut, ref string strFormula, 
                    ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataRow drCuadro, drCuadroOut;
            DataTable dtInfoPeriodo = new DataTable(), dtInfoVariable = new DataTable(), dtInfoCuadro = new DataTable(),
                dtCuadroMandoTemp = new DataTable(), dtInfoForm = new DataTable(), dtInfoCump = new DataTable();

            List<clsFormula> lstFormula = new List<clsFormula>();
            clsPeriodicidad objPeriodicidad = new clsPeriodicidad(objIndicador.intIdPeriodicidad, string.Empty, 0, string.Empty);
            clsFormulaBLL cFormula = new clsFormulaBLL();
            clsDtPeriodicidad cPeriodo = new clsDtPeriodicidad();
            clsDtVariable cVariable = new clsDtVariable();
            clsDtSeguimientoIndicador cSegIndicador = new clsDtSeguimientoIndicador();
            clsDtFormula cForm = new clsDtFormula();
            clsDtCumplimiento cCumplimiento = new clsDtCumplimiento();
            #endregion Vars

            try
            {
                #region Consultas de los diferentes objetos que pertenecen al cuadro de mando

                booResult = cPeriodo.mtdConsultarDetallePeriodo(objPeriodicidad, ref dtInfoPeriodo, ref strErrMsg);
                if (booResult)
                    booResult = cVariable.mtdConsultarVariable(objIndicador, ref dtInfoVariable, ref strErrMsg);

                if (booResult)
                    booResult = cSegIndicador.mtdConsultarCabCuadro(objProcInd, objIndicador, ref dtCabCuadroOut, ref strErrMsg);

                if (booResult)
                    booResult = cSegIndicador.mtdConsultarInfoCuadro(objProcInd, objIndicador, ref dtInfoCuadro, ref strErrMsg);

                if (booResult)
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

                if (booResult)
                    booResult = cCumplimiento.mtdConsultarDetalleCalificacion(objIndicador, ref dtInfoCump, ref strErrMsg);
                #endregion

                if (booResult)
                {
                    #region Crear Estructura
                    dtCuadroMandoTemp.Columns.Add("PERIODO", typeof(string));
                    dtCuadroMandoOut.Columns.Add("PERIODO", typeof(string));

                    //VARIABLES
                    foreach (DataRow dr in dtInfoVariable.Rows)
                    {
                        dtCuadroMandoTemp.Columns.Add(dr[1].ToString().Trim(), typeof(string));
                        dtCuadroMandoOut.Columns.Add(dr[1].ToString().Trim(), typeof(string));
                    }

                    dtCuadroMandoTemp.Columns.Add("RESULTADO", typeof(string));
                    dtCuadroMandoTemp.Columns.Add("META", typeof(string));

                    dtCuadroMandoOut.Columns.Add("RESULTADO", typeof(string));
                    dtCuadroMandoOut.Columns.Add("META", typeof(string));
                    dtCuadroMandoOut.Columns.Add("CUMPLIMIENTO", typeof(string));

                    //DataColumn dc = new DataColumn("CUMPLIMIENTO");
                    //dc.DataType = System.Type.GetType("System.Byte[]");
                    //dtCuadroMandoOut.Columns.Add(dc);

                    #endregion

                    #region Ingresar informacion a estructura temporal
                    string strMeta = string.Empty;
                    foreach (DataRow drPeriodo in dtInfoPeriodo.Rows)
                    {
                        drCuadro = dtCuadroMandoTemp.NewRow();
                        drCuadro["PERIODO"] = drPeriodo[2].ToString();

                        string strFiltro = string.Format("[IdDetPeriodo]='{0}'", drPeriodo[0].ToString());
                        foreach (DataRow drInfoCuadro in dtInfoCuadro.Select(strFiltro))
                        {
                            drCuadro[drInfoCuadro["Variable"].ToString().Trim()] = drInfoCuadro["Valor"].ToString();
                            strMeta = drInfoCuadro["Meta"].ToString();
                        }

                        drCuadro["META"] = strMeta;
                        dtCuadroMandoTemp.Rows.Add(drCuadro);
                    }
                    #endregion

                    #region Recorrido para generar el cuadro de mando final
                    foreach (DataRow drTemp in dtCuadroMandoTemp.Rows)
                    {
                        int intCantidad = dtCuadroMandoTemp.Columns.Count;
                        decimal decResultado = 0;
                        string strOperacion = string.Empty;

                        drCuadroOut = dtCuadroMandoOut.NewRow();

                        if (!booResult)
                            break;

                        #region Formula
                        foreach (DataRow drForm in dtInfoForm.Rows)
                        {
                            if (!booResult)
                                break;

                            #region Recorrido ejecucion de formula
                            switch (drForm["IdOperando"].ToString())
                            {
                                case "1"://Variable
                                    string strFiltro = string.Format("[Id]={0}", drForm["Valor"].ToString()),
                                        strNombreVar = string.Empty;
                                    DataRow[] drVar = dtInfoVariable.Select(strFiltro);

                                    strNombreVar = drVar[0][1].ToString();
                                    if (string.IsNullOrEmpty(strOperacion))
                                        //decResultado = Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString());
                                        ///Debug.Assert(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()));
                                        //decResultado = Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                        if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                        {
                                            decResultado = 0;
                                        }
                                        else
                                        {
                                            decResultado = Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                        }
                                    else
                                    {
                                        #region Operacion
                                        switch (strOperacion)
                                        {

                                            case "+":
                                                if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                                {
                                                    decResultado = 0;
                                                    break;
                                                }
                                                else
                                                {
                                                    decResultado = decResultado +
                                                        Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                                    break;
                                                }
                                            case "-":
                                                if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                                {
                                                    decResultado = 0;
                                                    break;
                                                }
                                                else
                                                {
                                                    decResultado = decResultado -
                                                        Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                                    break;
                                                }
                                            case "X":
                                                if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                                {
                                                    decResultado = 0;
                                                    break;
                                                }
                                                else
                                                {
                                                    decResultado = decResultado *
                                                        Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                                    break;
                                                }
                                            case "/":
                                                if (Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString()) != 0)
                                                    if (string.IsNullOrEmpty(drTemp[strNombreVar].ToString()))
                                                    {
                                                        decResultado = 0;
                                                    }
                                                    else
                                                    {
                                                        decResultado = decResultado /
                                                            Convert.ToDecimal(string.IsNullOrEmpty(drTemp[strNombreVar].ToString()) ? "0" : drTemp[strNombreVar].ToString());
                                                    }
                                                else
                                                {
                                                    booResult = false;
                                                    strErrMsg = string.Format("Por favor revisar el valor de la variable [{0}]. [Division por 0] ", strNombreVar);
                                                }
                                                break;
                                        }
                                        #endregion
                                    }
                                    break;
                                case "2"://Operacion
                                    strOperacion = drForm["Valor"].ToString();
                                    break;
                                case "3"://OtroValor
                                    #region Operacion
                                    switch (strOperacion)
                                    {
                                        case "+":
                                            if (decResultado == 0)
                                            {
                                                decResultado = 0;
                                                break;
                                            }
                                            else
                                            {
                                                decResultado = decResultado + Convert.ToDecimal(drForm["Valor"].ToString());
                                                break;
                                            }
                                        case "-":
                                            if (decResultado == 0)
                                            {
                                                decResultado = 0;
                                                break;
                                            }
                                            else
                                            {
                                                decResultado = decResultado - Convert.ToDecimal(drForm["Valor"].ToString());
                                                break;
                                            }
                                        case "X":
                                            if (decResultado == 0)
                                            {
                                                decResultado = 0;
                                                break;
                                            }
                                            else
                                            {
                                                decResultado = decResultado * Convert.ToDecimal(drForm["Valor"].ToString());
                                                break;
                                            }
                                        case "/":
                                            if (Convert.ToDecimal(drForm["Valor"].ToString()) != 0)
                                                if (decResultado == 0)
                                                {
                                                    decResultado = 0;
                                                    break;
                                                }
                                                else
                                                {
                                                    decResultado = decResultado / Convert.ToDecimal(drForm["Valor"].ToString());
                                                }
                                            else
                                            {
                                                booResult = false;
                                                strErrMsg = string.Format("Por favor revisar el valor de la formula [{0}]. [Division por 0] ");
                                            }
                                            break;
                                    }
                                    #endregion
                                    break;
                            }
                            #endregion
                        }
                        #endregion

                        #region Cumplimiento
                        string strCumplimiento = string.Empty;
                        //System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        if (booResult)
                        {
                            #region Recorrido de cumplimientos
                            foreach (DataRow drCump in dtInfoCump.Rows)
                            {
                                decimal decValorMin = Convert.ToDecimal(drCump["ValorMinimo"].ToString()),
                                    decValorMax = Convert.ToDecimal(drCump["ValorMaximo"].ToString());

                                if (!(decResultado.ToString().Length == 1 && decResultado == 0))
                                    if ((decValorMin <= decResultado) &&
                                        (decValorMax >= decResultado))
                                    {
                                        strCumplimiento = drCump["NombreCumplimiento"].ToString();

                                        #region Selecciona Imagen Semaforo
                                        //img.ID = "img1";
                                        //img.Visible = true;
                                        //img.AlternateText = drCump["NombreCumplimiento"].ToString();
                                        //switch (drCump["IdSemaforo"].ToString())
                                        //{
                                        //    case "1":

                                        //        //img.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                        //        break;
                                        //    case "2":

                                        //        //img.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                        //        break;
                                        //    case "3":
                                        //        //img.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                        //        break;
                                        //}
                                        #endregion

                                        break;
                                    }
                            }
                            #endregion
                        }
                        #endregion

                        #region Generacion del cuadro de mando
                        if (booResult)
                        {
                            for (int i = 0; i < intCantidad; i++)
                            {
                                drCuadroOut[i] = drTemp[i].ToString();
                            }

                            drCuadroOut["RESULTADO"] = decResultado.ToString();

                            //var imageConverter = new System.Drawing.ImageConverter();
                            //drCuadroOut["CUMPLIMIENTO"] = imageConverter.ConvertTo(img, System.Type.GetType("System.Byte[]"));
                            drCuadroOut["CUMPLIMIENTO"] = strCumplimiento;
                            dtCuadroMandoOut.Rows.Add(drCuadroOut);
                        }
                        #endregion
                    }
                    #endregion

                    booResult = true;
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el cuadro de mando. [{0}]", ex.Message);
                booResult = false;
            }
            return booResult;
        }
    }
}