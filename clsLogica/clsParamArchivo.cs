using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using clsDatos;
using clsDTO;
using System.Data.SqlClient;

namespace clsLogica
{
    public class clsParamArchivo
    {
        public clsParamArchivo()
        {
        }

        #region Variables
        public List<clsDTOVariable> mtdCargarInfoVariables(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOVariable objTipoParam = new clsDTOVariable();
            List<clsDTOVariable> lstTipoParam = new List<clsDTOVariable>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaTipoParametro(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objTipoParam = new clsDTOVariable(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreParametro"].ToString().Trim(),
                            dr["Calificacion"].ToString().Trim(),
                            dr["Activo"].ToString().Trim() == "True" ? true : false);

                        lstTipoParam.Add(objTipoParam);
                    }
                }
                else
                {
                    lstTipoParam = null;
                    strErrMsg = "No hay información de Variables.";
                }
            }
            else
                lstTipoParam = null;

            return lstTipoParam;
        }

        public List<clsDTOVariableMod> mtdCargarInfoVariablesMod(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOVariableMod objTipoParam = new clsDTOVariableMod();
            List<clsDTOVariableMod> lstTipoParam = new List<clsDTOVariableMod>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaTipoParametroMod(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objTipoParam = new clsDTOVariableMod(
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreParametro"].ToString().Trim(),
                            dr["Calificacion"].ToString().Trim(),
                            dr["Activo"].ToString().Trim() == "True" ? true : false,
                            dr["Usuario"].ToString().Trim(),
                            dr["IdUsuario"].ToString().Trim(),
                            dr["FechaModificacion"].ToString().Trim()
                            );

                        lstTipoParam.Add(objTipoParam);
                    }
                }
                else
                {
                    lstTipoParam = null;
                    strErrMsg = "No hay información de Variables.";
                }
            }
            else
                lstTipoParam = null;

            return lstTipoParam;
        }


        public List<clsDTOVariable> mtdCargarInfoVariables(clsDTOVariable objVariableIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOVariable objTipoParam = new clsDTOVariable();
            List<clsDTOVariable> lstTipoParam = new List<clsDTOVariable>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaTipoParametroXEstado(objVariableIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objTipoParam = new clsDTOVariable(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreParametro"].ToString().Trim(),
                            dr["Calificacion"].ToString().Trim(),
                            dr["Activo"].ToString().Trim() == "True" ? true : false);

                        lstTipoParam.Add(objTipoParam);
                    }
                }
                else
                {
                    lstTipoParam = null;
                    strErrMsg = "No hay información de Variables.";
                }
            }
            else
                lstTipoParam = null;

            return lstTipoParam;
        }

        public clsDTOVariable mtdBuscarVariable(clsDTOVariable objVariableIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOVariable objTipoParam = new clsDTOVariable();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaTipoParametro(objVariableIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    objTipoParam.StrIdVariable = dtInfo.Rows[0]["IdTipoParametro"].ToString().Trim();
                    objTipoParam.StrNombreVariable = dtInfo.Rows[0]["NombreParametro"].ToString().Trim();
                }
            }

            return objTipoParam;
        }

        public clsDTOVariable mtdConsultaTipoParametro(clsDTOEstructuraCampo objEstruct, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOVariable objVariable = new clsDTOVariable();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaTipoParametro(objEstruct, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    objVariable = new clsDTOVariable(
                        dtInfo.Rows[0]["IdUsuario"].ToString().Trim(),
                        dtInfo.Rows[0]["IdTipoParametro"].ToString().Trim(),
                        dtInfo.Rows[0]["NombreParametro"].ToString().Trim(),
                        dtInfo.Rows[0]["Calificacion"].ToString().Trim(),
                        dtInfo.Rows[0]["Activo"].ToString().Trim() == "True" ? true : false);
            }

            return objVariable;
        }

        public int mtdConsultaSumatoria(ref string strErrMsg)
        {
            #region Vars
            int intResult = 0;
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaSumatoria(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    // intResult = String.IsNullOrEmpty( Convert.ToInt32(dtInfo.Rows[0]["Sumatoria"].ToString().Trim());
                    intResult = !String.IsNullOrEmpty(dtInfo.Rows[0]["Sumatoria"].ToString().Trim()) ? Convert.ToInt32(dtInfo.Rows[0]["Sumatoria"].ToString().Trim()) : 0;
            }

            return intResult;
        }

        public void mtdActualizarVariable(clsDTOVariable objVariableIn, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdActualizarTipoParametro(objVariableIn, ref strErrMsg);
        }

        public void mtdAgregarVariable(clsDTOVariable objTipoParamIn, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdAgregarTipoParametro(objTipoParamIn, ref strErrMsg);
        }

        public void mtdAgregarVariableCreate(clsDTOVariableCreate objTipoParamIn, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdAgregarTipoParametroCreate(objTipoParamIn, ref strErrMsg);
        }

        /*
         * Metodos para el Servicio  
         */
        #region Metodos Servicio
        public List<clsDTOVariable> mtdCargarInfoVariables(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOVariable objTipoParam = new clsDTOVariable();
            List<clsDTOVariable> lstTipoParam = new List<clsDTOVariable>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaTipoParametro(strOleConn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objTipoParam = new clsDTOVariable(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreParametro"].ToString().Trim(),
                            dr["Calificacion"].ToString().Trim(),
                            dr["Activo"].ToString().Trim() == "True" ? true : false);

                        lstTipoParam.Add(objTipoParam);
                    }
                }
                else
                {
                    lstTipoParam = null;
                    strErrMsg = "No hay información de Variables.";
                }
            }
            else
                lstTipoParam = null;

            return lstTipoParam;
        }

        public clsDTOVariable mtdBuscarVariable(string strOleConn, clsDTOVariable objVariableIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOVariable objTipoParam = new clsDTOVariable();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaTipoParametro(strOleConn, objVariableIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    objTipoParam.StrIdVariable = dtInfo.Rows[0]["IdTipoParametro"].ToString().Trim();
                    objTipoParam.StrNombreVariable = dtInfo.Rows[0]["NombreParametro"].ToString().Trim();
                }
            }

            return objTipoParam;
        }

        public clsDTOVariable mtdConsultaTipoParametro(string strOleConn, clsDTOEstructuraCampo objEstruct, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOVariable objVariable = new clsDTOVariable();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaTipoParametro(strOleConn, objEstruct, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    objVariable = new clsDTOVariable(
                        dtInfo.Rows[0]["IdUsuario"].ToString().Trim(),
                        dtInfo.Rows[0]["IdTipoParametro"].ToString().Trim(),
                        dtInfo.Rows[0]["NombreParametro"].ToString().Trim(),
                        dtInfo.Rows[0]["Calificacion"].ToString().Trim(),
                        dtInfo.Rows[0]["Activo"].ToString().Trim() == "True" ? true : false);
            }

            return objVariable;
        }

        public int mtdConsultaSumatoria(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            int intResult = 0;
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaSumatoria(strOleConn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intResult = Convert.ToInt32(dtInfo.Rows[0]["Sumatoria"].ToString().Trim());
            }

            return intResult;
        }
        #endregion
        #endregion Variables

        #region Categorias
        public List<clsDTOParametrizacion> mtdCargarInfoParametrizacion(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOParametrizacion objParametrizacion = new clsDTOParametrizacion();
            List<clsDTOParametrizacion> lstParametrizacion = new List<clsDTOParametrizacion>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaParametrizacion(objVariable, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Llena informacion consultada
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objParametrizacion = new clsDTOParametrizacion(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdParametros"].ToString().Trim(),
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreParametro"].ToString().Trim(),
                            dr["CodigoParametro"].ToString().Trim(),
                            dr["CalificacionParametro"].ToString().Trim(),
                            dr["NombreTipoParametro"].ToString().Trim(),
                            dr["EsFormula"].ToString().Trim() == "True" ? true : false);

                        lstParametrizacion.Add(objParametrizacion);
                    }
                    #endregion
                }
                else
                {
                    lstParametrizacion = null;
                    strErrMsg = "No hay información de categorías.";
                }
            }
            else
                lstParametrizacion = null;

            return lstParametrizacion;
        }
        public List<clsDTOParametrizacionMod> mtdCargarInfoParametrizacionMod(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOParametrizacionMod objParametrizacion = new clsDTOParametrizacionMod();
            List<clsDTOParametrizacionMod> lstParametrizacion = new List<clsDTOParametrizacionMod>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaParametrizacionMod(objVariable, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Llena informacion consultada
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objParametrizacion = new clsDTOParametrizacionMod(
                            dr["IdParametros"].ToString().Trim(),
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreParametro"].ToString().Trim(),
                            dr["CodigoParametro"].ToString().Trim(),
                            dr["CalificacionParametro"].ToString().Trim(),
                            dr["NombreTipoParametro"].ToString().Trim(),
                            dr["EsFormula"].ToString().Trim() == "True" ? true : false,
                            dr["Usuario"].ToString().Trim(),
                            dr["IdUsuario"].ToString().Trim(),
                            dr["FechaModificacion"].ToString().Trim()
                            );

                        lstParametrizacion.Add(objParametrizacion);
                    }
                    #endregion
                }
                else
                {
                    lstParametrizacion = null;
                    strErrMsg = "No hay información de categorías.";
                }
            }
            else
                lstParametrizacion = null;

            return lstParametrizacion;
        }

        public void mtdActualizarParametrizacion(clsDTOParametrizacion objParametrizacion, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdActualizarParametrizacion(objParametrizacion, ref strErrMsg);
        }

        public void mtdAgregarParametrizacion(clsDTOParametrizacion objParametrizacion, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdAgregarParametrizacion(objParametrizacion, ref strErrMsg);
        }
        public void mtdAgregarParametrizacionCreate(clsDTOParametrizacionCreate objParametrizacion, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdAgregarParametrizacionCreate(objParametrizacion, ref strErrMsg);
        }

        public List<clsDTOParametrizacion> mtdConsultarCalifParam(string strIdCampo, string strValorIn, ref string strErrMsg)
        {
            try
            {
                #region Vars
                bool booIsFilled = false;
                DataTable dtInfo = new DataTable(), dtInfoIsForm = new DataTable();
                clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
                clsDTOParametrizacion objParametrizacion = new clsDTOParametrizacion();
                List<clsDTOParametrizacion> lstParametrizacion = new List<clsDTOParametrizacion>();
                #endregion Vars

                dtInfoIsForm = cDtParamArchivo.mtdConsultaParametrizacion(strIdCampo, ref strErrMsg);

                if (dtInfoIsForm != null)
                {
                    if (dtInfoIsForm.Rows.Count > 0)
                    {
                        foreach (DataRow drIsForm in dtInfoIsForm.Rows)
                        {
                            if (booIsFilled)
                                break;

                            if (drIsForm["EsFormula"].ToString().Trim() == "True")
                            {
                                #region Consulta Valores y evaluacion cuando es formula
                                if (mtdEvaluarValor(drIsForm, strValorIn))
                                {
                                    objParametrizacion = new clsDTOParametrizacion(
                                               drIsForm["IdUsuario"].ToString().Trim(),
                                               drIsForm["IdParametros"].ToString().Trim(),
                                               drIsForm["IdTipoParametro"].ToString().Trim(),
                                               drIsForm["NombreParametro"].ToString().Trim(),
                                               drIsForm["CodigoParametro"].ToString().Trim(),
                                               drIsForm["Calificacion"].ToString().Trim(),
                                               drIsForm["NombreTipoParametro"].ToString().Trim(),
                                               drIsForm["EsFormula"].ToString().Trim() == "True" ? true : false);

                                    if (lstParametrizacion == null)
                                        lstParametrizacion = new List<clsDTOParametrizacion>();

                                    lstParametrizacion.Add(objParametrizacion);
                                    booIsFilled = true;
                                }
                                else
                                    continue;
                                #endregion
                            }
                            else
                            {
                                #region Consulta de valores cuando no es formula
                                dtInfo = cDtParamArchivo.mtdConsultaParametrizacion(strIdCampo, strValorIn, ref strErrMsg);

                                if (dtInfo != null)
                                {
                                    if (dtInfo.Rows.Count > 0)
                                    {
                                        #region Llena informacion consultada
                                        foreach (DataRow dr in dtInfo.Rows)
                                        {
                                            objParametrizacion = new clsDTOParametrizacion(
                                                dr["IdUsuario"].ToString().Trim(),
                                                dr["IdParametros"].ToString().Trim(),
                                                dr["IdTipoParametro"].ToString().Trim(),
                                                dr["NombreParametro"].ToString().Trim(),
                                                dr["CodigoParametro"].ToString().Trim(),
                                                dr["Calificacion"].ToString().Trim(),
                                                dr["NombreTipoParametro"].ToString().Trim(),
                                                dr["EsFormula"].ToString().Trim() == "True" ? true : false);

                                            lstParametrizacion.Add(objParametrizacion);
                                        }
                                        #endregion

                                        booIsFilled = true;
                                    }
                                    else
                                    {
                                        if (lstParametrizacion != null)
                                            if (lstParametrizacion.Count <= 0)
                                                lstParametrizacion = null;
                                        strErrMsg = "No hay información de categorías.";
                                    }
                                }
                                else
                                    if (lstParametrizacion.Count <= 0)
                                    lstParametrizacion = null;
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        lstParametrizacion = null;
                        strErrMsg = "No hay información de categorías.";
                    }
                }
                else
                    lstParametrizacion = null;

                if (lstParametrizacion != null)
                    if (lstParametrizacion.Count <= 0)
                        lstParametrizacion = null;

                return lstParametrizacion;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool mtdEvaluarValor(DataRow drIsForm, string strValorIn)
        {
            try
            {
                bool booIsValue = false;
                string[] strPartesForm = drIsForm["CodigoParametro"].ToString().Trim().Split(' ');
                //clsUtilidades.mtdEsNumero(strValorIn)
                if (clsUtilidades.mtdEsNumeroNew(strValorIn)) 
                {
                    if (strPartesForm.Length > 1)
                    {
                        #region Compara el valor de acuerdo al operador
                        switch (strPartesForm[0])
                        {
                            case "<":
                                if (Convert.ToInt64(strValorIn) < Convert.ToInt64(strPartesForm[1]))
                                    booIsValue = true;
                                break;
                            case ">":
                                if (Convert.ToInt64(strValorIn) > Convert.ToInt64(strPartesForm[1]))
                                    booIsValue = true;
                                break;
                            case "=":
                                if (Convert.ToInt64(strValorIn) == Convert.ToInt64(strPartesForm[1]))
                                    booIsValue = true;
                                break;
                            case ">=":
                                if (Convert.ToInt64(strValorIn) >= Convert.ToInt64(strPartesForm[1]))
                                    booIsValue = true;
                                break;
                            case "<=":
                                if (Convert.ToInt64(strValorIn) <= Convert.ToInt64(strPartesForm[1]))
                                    booIsValue = true;
                                break;

                            case "Entre":
                                if ((Convert.ToInt64(strValorIn) >= Convert.ToInt64(strPartesForm[1])) &&
                                    (Convert.ToInt64(strValorIn) <= Convert.ToInt64(strPartesForm[3])))
                                    booIsValue = true;
                                break;
                        }
                        #endregion
                    }
                }
                else
                {
                    if (strValorIn == strPartesForm[1])
                        booIsValue = true;
                }
                return booIsValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void mtdEliminarCategoria(clsDTOParametrizacion objParam, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParam = new clsDtParamArchivo();

            cDtParam.mtdEliminarCategoria(objParam, ref strErrMsg);
        }

        /*
         * Metodos para el Servicio  
         */
        #region Servicio
        public List<clsDTOParametrizacion> mtdConsultarCalifParam(string strOleConn, string strIdCampo, string strValorIn, ref string strErrMsg)
        {
            #region Vars
            bool booIsFilled = false;
            DataTable dtInfo = new DataTable(), dtInfoIsForm = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOParametrizacion objParametrizacion = new clsDTOParametrizacion();
            List<clsDTOParametrizacion> lstParametrizacion = new List<clsDTOParametrizacion>();
            #endregion Vars

            dtInfoIsForm = cDtParamArchivo.mtdConsultaCategoria(strOleConn, strIdCampo, ref strErrMsg);

            if (dtInfoIsForm != null)
            {
                if (dtInfoIsForm.Rows.Count > 0)
                {
                    #region Recorre informacion
                    foreach (DataRow drIsForm in dtInfoIsForm.Rows)
                    {
                        if (booIsFilled)
                            break;

                        if (drIsForm["EsFormula"].ToString().Trim() == "True")
                        {
                            #region Consulta Valores y evaluacion cuando es formula
                            if (mtdEvaluarValor(drIsForm, strValorIn))
                            {
                                objParametrizacion = new clsDTOParametrizacion(
                                           drIsForm["IdUsuario"].ToString().Trim(),
                                           drIsForm["IdParametros"].ToString().Trim(),
                                           drIsForm["IdTipoParametro"].ToString().Trim(),
                                           drIsForm["NombreParametro"].ToString().Trim(),
                                           drIsForm["CodigoParametro"].ToString().Trim(),
                                           drIsForm["Calificacion"].ToString().Trim(),
                                           drIsForm["NombreTipoParametro"].ToString().Trim(),
                                           drIsForm["EsFormula"].ToString().Trim() == "True" ? true : false);

                                lstParametrizacion.Add(objParametrizacion);
                                booIsFilled = true;
                            }
                            else
                                continue;
                            #endregion
                        }
                        else
                        {
                            #region Consulta de valores cuando no es formula
                            dtInfo = cDtParamArchivo.mtdConsultaCategoria(strOleConn, strIdCampo, strValorIn, ref strErrMsg);

                            if (dtInfo != null)
                            {
                                if (dtInfo.Rows.Count > 0)
                                {
                                    #region Llena informacion consultada
                                    foreach (DataRow dr in dtInfo.Rows)
                                    {
                                        objParametrizacion = new clsDTOParametrizacion(
                                            dr["IdUsuario"].ToString().Trim(),
                                            dr["IdParametros"].ToString().Trim(),
                                            dr["IdTipoParametro"].ToString().Trim(),
                                            dr["NombreParametro"].ToString().Trim(),
                                            dr["CodigoParametro"].ToString().Trim(),
                                            dr["Calificacion"].ToString().Trim(),
                                            dr["NombreTipoParametro"].ToString().Trim(),
                                            dr["EsFormula"].ToString().Trim() == "True" ? true : false);

                                        lstParametrizacion.Add(objParametrizacion);
                                    }
                                    #endregion

                                    booIsFilled = true;
                                }
                                else
                                {
                                    lstParametrizacion = null;
                                    strErrMsg = "No hay información de categorías.";
                                }
                            }
                            else
                                lstParametrizacion = null;
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    lstParametrizacion = null;
                    strErrMsg = "No hay información de categorías.";
                }
            }
            else
                lstParametrizacion = null;

            return lstParametrizacion;
        }
        #endregion
        #endregion Categorias

        #region Estructura
        /// <summary>
        /// Metodo de consulta que trae la informacion de la estructura ingresada o configurada 
        /// para el archivo de entrada a cargar al sistema
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public List<clsDTOEstructuraCampo> mtdCargarInfoEstructura(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOEstructuraCampo objEstructura = new clsDTOEstructuraCampo();
            List<clsDTOEstructuraCampo> lstEstructura = new List<clsDTOEstructuraCampo>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaEstructura(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEstructura = new clsDTOEstructuraCampo(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdCampo"].ToString().Trim(),
                            dr["NombreCampo"].ToString().Trim(),
                            dr["IdTipoDato"].ToString().Trim(),
                            dr["Longitud"].ToString().Trim(),
                            dr["Parametriza"].ToString().Trim() == "True" ? true : false,
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreTipoParametro"].ToString().Trim(),
                            dr["NombreTipoDato"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            //Convert.ToInt32(dr["Estado"]),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Numerico"].ToString().Trim() == "True" ? true : false);

                        lstEstructura.Add(objEstructura);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstEstructura = null;
                    strErrMsg = "No hay información de la estructura de campos.";
                }
            }
            else
                lstEstructura = null;

            return lstEstructura;
        }

        public List<clsDTOEstructuraCampo> mtdCargarInfoEstructura(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOEstructuraCampo objEstructura = new clsDTOEstructuraCampo();
            List<clsDTOEstructuraCampo> lstEstructura = new List<clsDTOEstructuraCampo>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaEstructura(objVariable, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        Boolean booNumerico = false;
                        Boolean booEstado = false;
                        if (dr["Numerico"].ToString().Trim() == "True")
                            booNumerico = true;
                        if (dr["Estado"].ToString().Trim() == "True")
                            booEstado = true;
                        objEstructura = new clsDTOEstructuraCampo(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdCampo"].ToString().Trim(),
                            dr["NombreCampo"].ToString().Trim(),
                            dr["IdTipoDato"].ToString().Trim(),
                            dr["Longitud"].ToString().Trim(),
                            dr["Parametriza"].ToString().Trim() == "True" ? true : false,
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreTipoParametro"].ToString().Trim(),
                            dr["NombreTipoDato"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            booEstado,
                            booNumerico);

                        lstEstructura.Add(objEstructura);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstEstructura = null;
                    strErrMsg = "No hay información de la estructura de campos.";
                }
            }
            else
                lstEstructura = null;

            return lstEstructura;
        }

        public List<clsDTOEstructuraCampoMod> mtdCargarInfoEstructuraMod(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOEstructuraCampoMod objEstructura = new clsDTOEstructuraCampoMod();
            List<clsDTOEstructuraCampoMod> lstEstructura = new List<clsDTOEstructuraCampoMod>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaEstructuraMod(objVariable, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        Boolean booNumerico = false;
                        if (dr["Numerico"].ToString().Trim() == "True")
                            booNumerico = true;
                        objEstructura = new clsDTOEstructuraCampoMod(
                            dr["IdCampo"].ToString().Trim(),
                            dr["NombreCampo"].ToString().Trim(),
                            dr["IdTipoDato"].ToString().Trim(),
                            dr["Longitud"].ToString().Trim(),
                            dr["Parametriza"].ToString().Trim() == "True" ? true : false,
                            dr["NombreParametro"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["IdTipoDato"].ToString().Trim(),
                            dr["IdTipoParametro"].ToString().Trim(),            
                            dr["IdUsuario"].ToString().Trim(),
                            dr["Usuario"].ToString().Trim(),
                            Convert.ToInt32(dr["Estado"]),
                            booNumerico,
                            dr["FechaModificacion"].ToString().Trim()
                            //dr["NombreTipoParametro"].ToString().Trim(),

                            );

                        lstEstructura.Add(objEstructura);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstEstructura = null;
                    strErrMsg = "No hay información de la estructura de campos.";
                }
            }
            else
                lstEstructura = null;

            return lstEstructura;
        }

        public void mtdAgregarEstructuraCampo(clsDTOEstructuraCampo objEstructura, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdAgregarEstructuraCampo(objEstructura, ref strErrMsg);
        }

        public void mtdAgregarEstructuraCampoCreate(clsDTOEstructuraCampo objEstructura, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdAgregarEstructuraCampoCreate(objEstructura, ref strErrMsg);
        }

        public void mtdActualizarEstructura(clsDTOEstructuraCampo objEstructura, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdActualizarEstructura(objEstructura, ref strErrMsg);
        }

        public int mtdCantidadCamposEstructura(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            int intNumeroCampos = 0;
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdCantidadCamposEstructura(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intNumeroCampos = Convert.ToInt32(dtInfo.Rows[0][0].ToString().Trim());
                else
                    strErrMsg = "No hay información de la estructura de campos.";
            }

            return intNumeroCampos;
        }

        public int mtdCantidadCamposEstructura(clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            int intNumeroCampos = 0;
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdCantidadCamposEstructura(objVariable, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intNumeroCampos = Convert.ToInt32(dtInfo.Rows[0][0].ToString().Trim());
                else
                    strErrMsg = "No hay información de la estructura de campos.";
            }

            return intNumeroCampos;
        }

        public string mtdConsultarPosXCampo(string strNombreCampo, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            string strPosicion = string.Empty;
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultarPosXCampo(strNombreCampo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    strPosicion = dtInfo.Rows[0][0].ToString().Trim();
                else
                    strErrMsg = "No hay información de la estructura de campos.";
            }

            return strPosicion;
        }

        public clsDTOEstructuraCampo mtdBuscarCampo(clsDTOEstructuraCampo objCampoIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOEstructuraCampo objCampo = new clsDTOEstructuraCampo();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaCampo(objCampoIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    objCampo.StrIdEstructCampo = dtInfo.Rows[0]["IdCampo"].ToString().Trim();
                    objCampo.StrNombreCampo = dtInfo.Rows[0]["NombreCampo"].ToString().Trim();
                    objCampo.StrLongitud = dtInfo.Rows[0]["Longitud"].ToString().Trim();
                    objCampo.BooEsParametrico = dtInfo.Rows[0]["Parametriza"].ToString().Trim() == "False" ? false : true;
                    objCampo.StrIdVariable = dtInfo.Rows[0]["IdTipoParametro"].ToString().Trim();
                    objCampo.StrIdTipoDato = dtInfo.Rows[0]["IdTipoDato"].ToString().Trim();
                    objCampo.StrPosicion = dtInfo.Rows[0]["Posicion"].ToString().Trim();
                    if (dtInfo.Rows[0]["Numerico"] != null)
                        objCampo.BooEsParametrico = dtInfo.Rows[0]["Numerico"].ToString().Trim() == "False" ? false : true;
                    else
                        objCampo.BooEsParametrico = false;
                    //objCampo. = dtInfo.Rows[0][""].ToString().Trim();
                    //objCampo. = dtInfo.Rows[0][""].ToString().Trim();
                }
            }

            return objCampo;
        }

        /*
         * Metodos para el Servicio  
         */
        #region Metodos Servicio
        public List<clsDTOEstructuraCampo> mtdCargarInfoEstructura(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOEstructuraCampo objEstructura = new clsDTOEstructuraCampo();
            List<clsDTOEstructuraCampo> lstEstructura = new List<clsDTOEstructuraCampo>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaEstructura(strOleConn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEstructura = new clsDTOEstructuraCampo(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdCampo"].ToString().Trim(),
                            dr["NombreCampo"].ToString().Trim(),
                            dr["IdTipoDato"].ToString().Trim(),
                            dr["Longitud"].ToString().Trim(),
                            dr["Parametriza"].ToString().Trim() == "True" ? true : false,
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreTipoParametro"].ToString().Trim(),
                            dr["NombreTipoDato"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            //Convert.ToInt32(dr["Estado"].ToString()),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Numerico"].ToString().Trim() == "True" ? true : false);

                        lstEstructura.Add(objEstructura);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstEstructura = null;
                    strErrMsg = "No hay información de la estructura de campos.";
                }
            }
            else
                lstEstructura = null;

            return lstEstructura;
        }

        public List<clsDTOEstructuraCampo> mtdCargarInfoEstructura(string strOleConn, clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTOEstructuraCampo objEstructura = new clsDTOEstructuraCampo();
            List<clsDTOEstructuraCampo> lstEstructura = new List<clsDTOEstructuraCampo>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultaEstructura(strOleConn, objVariable, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEstructura = new clsDTOEstructuraCampo(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdCampo"].ToString().Trim(),
                            dr["NombreCampo"].ToString().Trim(),
                            dr["IdTipoDato"].ToString().Trim(),
                            dr["Longitud"].ToString().Trim(),
                            dr["Parametriza"].ToString().Trim() == "True" ? true : false,
                            dr["IdTipoParametro"].ToString().Trim(),
                            dr["NombreTipoParametro"].ToString().Trim(),
                            dr["NombreTipoDato"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            //Convert.ToInt32(dr["Estado"].ToString()),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            dr["Numerico"].ToString().Trim() == "True" ? true : false);

                        lstEstructura.Add(objEstructura);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstEstructura = null;
                    strErrMsg = "No hay información de la estructura de campos.";
                }
            }
            else
                lstEstructura = null;

            return lstEstructura;
        }

        public string mtdConsultarPosXCampo(string strOleConn, string strNombreCampo, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            string strPosicion = string.Empty;
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultarPosXCampo(strOleConn, strNombreCampo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    strPosicion = dtInfo.Rows[0][0].ToString().Trim();
                else
                    strErrMsg = "No hay información de la estructura de campos.";
            }

            return strPosicion;
        }

        public int mtdCantidadCamposEstructura(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            int intNumeroCampos = 0;
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdCantidadCamposEstructura(strOleConn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intNumeroCampos = Convert.ToInt32(dtInfo.Rows[0][0].ToString().Trim());
                else
                    strErrMsg = "No hay información de la estructura de campos.";
            }

            return intNumeroCampos;
        }

        public int mtdCantidadCamposEstructura(string strOleConn, clsDTOVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            int intNumeroCampos = 0;
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdCantidadCamposEstructura(strOleConn, objVariable, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intNumeroCampos = Convert.ToInt32(dtInfo.Rows[0][0].ToString().Trim());
                else
                    strErrMsg = "No hay información de la estructura de campos.";
            }

            return intNumeroCampos;
        }
        #endregion
        #endregion

        #region Relacion
        public List<clsDTORelacion> mtdConsultarRelaciones(clsDTOPerfil objPerfil, bool booActivos, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            clsDTORelacion objRel = new clsDTORelacion();
            List<clsDTORelacion> lstRel = new List<clsDTORelacion>();
            #endregion Vars

            dtInfo = cDtParamArchivo.mtdConsultarRelaciones(objPerfil, booActivos, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRel = new clsDTORelacion(
                            dr["IdConfPerfil"].ToString().Trim(),
                            dr["IdPerfil"].ToString().Trim(),
                            dr["NombrePerfil"].ToString().Trim(),
                            dr["IdConfCampo"].ToString().Trim(),
                            dr["NombreCampo"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["Activo"].ToString().Trim() == "1" ? true : false
                            );
                        lstRel.Add(objRel);
                    }
                }
                else
                {
                    lstRel = null;
                    strErrMsg = "No hay información de Relaciones.";
                }
            }
            else
                lstRel = null;

            return lstRel;
        }

        public void mtdAgregarRelacion(clsDTORelacion objRelacion, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdAgregarRelacion(objRelacion, ref strErrMsg);
        }

        public void mtdActualizarRelacion(clsDTORelacion objRelacion, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdActualizarRelacion(objRelacion, ref strErrMsg);
        }
        #endregion

        #region Perfil Variables
        public List<clsDTOPerfilVariable> mtdConsultarPerfilVariable(clsDTOPerfil objPerfil, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDTOPerfilVariable ObjPerfilVar = null;
            List<clsDTOPerfilVariable> lstPerfilVar = new List<clsDTOPerfilVariable>();
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
            #endregion

            dtInfo = cDtParamArchivo.mtdConsultarPerfilVariable(objPerfil, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdPerfilVariable], [IdPerfil], [IdVariable]
                        ObjPerfilVar = new clsDTOPerfilVariable(
                            
                            dr["IdPerfilVariable"].ToString().Trim(),
                            dr["IdPerfil"].ToString().Trim(),
                            dr["IdVariable"].ToString().Trim());

                        lstPerfilVar.Add(ObjPerfilVar);
                    }
                }
            }

            return lstPerfilVar;
        }

        public void mtdEliminarPerfilVariable(clsDTOPerfil objPerfil, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdEliminarPerfilVariable(objPerfil, ref strErrMsg);
        }

        public void mtdGuardarPerfilVariable(clsDTOPerfilVariable objPerfilVar, ref string strErrMsg)
        {
            clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();

            cDtParamArchivo.mtdGuardarPerfilVariable(objPerfilVar, ref strErrMsg);
        }
        #endregion

        // Activar desactivar estados
        public int ActualizarEstadoCampo(int IdCampo, int Estado)
        {
            try
            {
                clsDtParamArchivo cDtParamArchivo = new clsDtParamArchivo();
                int result = cDtParamArchivo.ActualizarEstadoCampo(IdCampo, Estado);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
