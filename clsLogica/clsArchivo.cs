using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using clsDatos;
using clsDTO;

namespace clsLogica
{
    public class clsArchivo
    {
        public clsArchivo()
        {
        }

        public clsDTOArchivo mtdDescargarArchivo(string strNombreArchivo, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDTOArchivo objArchivo = new clsDTOArchivo();
            clsDtArchivo cDtArchivo = new clsDtArchivo();
            #endregion Vars

            dtInfo = cDtArchivo.mtdDescargarArchivo(strNombreArchivo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objArchivo = new clsDTOArchivo(
                            dr["IdArchivo"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["UrlArchivo"].ToString().Trim(),
                            (byte[])dr["Archivo"]);
                    }
                }
                else
                {
                    objArchivo = null;
                    strErrMsg = "No hay información de Tipos de Parámetros.";
                }
            }
            else
                objArchivo = null;

            return objArchivo;
        }

        public int mtdConsultarConsecutivoArchivo(ref string strErrMsg)
        {
            int intConsecutivo = 0;
            DataTable dtInfo = new DataTable();
            clsDtArchivo cDtArchivo = new clsDtArchivo();

            dtInfo = cDtArchivo.mtdConsultarConsecutivoArchivo(ref strErrMsg);

            if (dtInfo != null)
                if (dtInfo.Rows.Count > 0)
                    intConsecutivo = Convert.ToInt32(dtInfo.Rows[0]["NumRegistros"].ToString().Trim());

            return intConsecutivo;
        }

        public List<clsDTOArchivo> mtdConsultarArchivos(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDTOArchivo objArchivo = new clsDTOArchivo();
            clsDtArchivo cDtArchivo = new clsDtArchivo();
            List<clsDTOArchivo> lstArchivo = new List<clsDTOArchivo>();
            #endregion Vars

            dtInfo = cDtArchivo.mtdConsultarArchivos(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objArchivo = new clsDTOArchivo(
                            dr["IdArchivo"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["UrlArchivo"].ToString().Trim(),
                            (byte[])dr["Archivo"]);
                        lstArchivo.Add(objArchivo);
                    }
                }
                else
                {
                    lstArchivo = null;
                    strErrMsg = "No hay información de Archivos.";
                }
            }
            else
                lstArchivo = null;

            return lstArchivo;
        }

        public void mtdAgregarArchivo(clsDTOArchivo objArchivo, int intNumeroCampos, int intIdUsuario, string strNombreUsuario,
            ref int intRegArchivo, ref string strErrMsg, ref int intOcurrencias, ref int bitFormulaAut)
        {
            #region Vars
            int intUltimoInsertado = 0;
            DataTable dtInfoReg = new DataTable(), dtInfoCargue = new DataTable();
            clsDtArchivo cDtArchivo = new clsDtArchivo();
            clsPerfil cPerfil = new clsPerfil();
            clsParamArchivo cParam = new clsParamArchivo();
            clsSenal cSenal = new clsSenal();
            clsDTOArchivo objArchivoTemp = new clsDTOArchivo();
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
            List<clsDTOEstructuraCampo> lstEstruc = new List<clsDTOEstructuraCampo>();
            List<clsDTOPerfil> lstPerfil = new List<clsDTOPerfil>();
            List<clsDTOSenalVariable> lstFormulas = new List<clsDTOSenalVariable>();
            //int intOcurrencias = 0;
            #endregion

            try
            {
                intUltimoInsertado = cDtArchivo.mtdAgregarArchivo(objArchivo, ref strErrMsg);

                if (intUltimoInsertado > 0)
                {
                    #region Instanciamiento de Variables
                    intRegArchivo = intUltimoInsertado;
                    string[] strLineas = mtdGenerarLineas(System.Text.Encoding.Default.GetString(objArchivo.BArchivo), '\n');
                    objArchivoTemp = new clsDTOArchivo(intUltimoInsertado.ToString(), string.Empty, string.Empty, null);
                    #endregion

                    if (strLineas.Length > 0)
                    {
                        mtdCargarInfoArchivo(objArchivoTemp, strLineas, intNumeroCampos, ref strErrMsg);

                        /* se realiza busqueda y calculos para encontrar el perfil del registro*/
                        #region Procesamiento
                        if (string.IsNullOrEmpty(strErrMsg))
                        {
                            dtInfoReg = mtdNumeroRegistrosArchivo(objArchivoTemp, ref strErrMsg);

                            if (dtInfoReg.Rows.Count > 0)
                            {
                                #region Consulta de informacion a recorrer y evaluar los perfiles
                                dtInfoCargue = mtdConsultarInfoCargada(objArchivoTemp, ref strErrMsg);
                                lstEstruc = cParam.mtdCargarInfoEstructura(objVariable, ref strErrMsg);
                                lstPerfil = cPerfil.mtdCargarInfoPerfiles(ref strErrMsg);
                                lstFormulas = cSenal.mtdConsultarFormSenalAuto(true, ref strErrMsg);
                                #endregion

                                #region Recorrido del archivo linea por linea
                                if (string.IsNullOrEmpty(strErrMsg))
                                    foreach (DataRow drLinea in dtInfoReg.Rows)
                                    {
                                        cPerfil.mtdEvaluarPerfiles(drLinea, dtInfoCargue, lstEstruc, lstPerfil, lstFormulas,
                                            objArchivoTemp, intIdUsuario, strNombreUsuario, intUltimoInsertado, ref strErrMsg);

                                        if (!string.IsNullOrEmpty(strErrMsg))
                                            break;
                                    }
                                #endregion

                                if (string.IsNullOrEmpty(strErrMsg))
                                {
                                    if(lstFormulas.Count > 0)
                                    {
                                        cSenal.mtdEjecutarSenalGlobal(lstFormulas, lstEstruc, dtInfoCargue, Convert.ToString(intIdUsuario), strNombreUsuario, ref intOcurrencias, ref strErrMsg);
                                        cSenal.mtdEjecutarSenalManual(lstFormulas, lstEstruc, dtInfoCargue, intIdUsuario, strNombreUsuario, ref intOcurrencias, ref strErrMsg);
                                        bitFormulaAut = 1;
                                    }

                                }
                                    
                            }
                            else
                                strErrMsg = "No hay registros en el archivo.";
                        }
                        #endregion
                    }
                    else
                        strErrMsg = "No hay registros en el archivo.";
                }
            }
            catch(Exception Ex)
            {
                strErrMsg = "Error durante el procesamiento del archivo. " + strErrMsg + " " + Ex.Message;
            }

            if (!string.IsNullOrEmpty(strErrMsg) && (intUltimoInsertado > 0))
                cDtArchivo.mtdBorrarErroresArchivo(1, objArchivoTemp);
        }

        /// <summary>
        /// Permite que aparezca el archivo con sus respectivos retornos o enters
        /// </summary>
        /// <param name="strLinea"></param>
        /// <param name="cSeparador"></param>
        /// <returns></returns>
        private string[] mtdGenerarLineas(string strLinea, char cSeparador)
        {
            string[] strLineas = strLinea.Replace("\r", "").Split(cSeparador);

            return strLineas;
        }

        /// <summary>
        /// Permite llamar la funcion del cargue de la informacion del archivo a base de datos campo por campo
        /// </summary>
        /// <param name="intIdArchivo"></param>
        /// <param name="strLineas"></param>
        /// <param name="intNumeroCampos"></param>
        /// <param name="strErrMsg"></param>
        private void mtdCargarInfoArchivo(clsDTOArchivo objArchivo, string[] strLineas, int intNumeroCampos, ref string strErrMsg)
        {
            clsDtArchivo cDtArchivo = new clsDtArchivo();

            cDtArchivo.mtdCargarInfoArchivo(objArchivo, strLineas, intNumeroCampos, ref strErrMsg);
        }

        /// <summary>
        /// Permite traer la informacion de los registros que hay en la base de datos. Numero de lineas de registro
        /// </summary>
        /// <param name="intIdArchivo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdNumeroRegistrosArchivo(clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            clsDtArchivo cDtArchivo = new clsDtArchivo();

            return cDtArchivo.mtdNumeroRegistrosArchivo(objArchivo, ref strErrMsg);
        }

        /// <summary>
        /// Metodo que permite consultar la informacion que se cargo del archivo de clientes y esta en la base de datos.
        /// </summary>
        /// <param name="intIdArchivo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdConsultarInfoCargada(clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            clsDtArchivo cDtArchivo = new clsDtArchivo();

            return cDtArchivo.mtdConsultarInfoCargada(objArchivo, ref strErrMsg);
        }

        /*
         * Metodos para el Servicio
         */
        public void mtdAgregarArchivo(string strSQLConn, string strOleConn, clsDTOArchivo objArchivo, int intNumeroCampos,
            int intIdUsuario, string strNombreUsuario, ref int intRegArchivo, ref string strErrMsg)
        {
            #region Vars
            int intUltimoInsertado = 0;
            DataTable dtInfoReg = new DataTable(), dtInfoCargue = new DataTable();
            clsDtArchivo cDtArchivo = new clsDtArchivo();
            clsPerfil cPerfil = new clsPerfil();
            clsParamArchivo cParam = new clsParamArchivo();
            clsSenal cSenal = new clsSenal();
            clsDTOArchivo objArchivoTemp = new clsDTOArchivo();
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty,string.Empty, string.Empty, string.Empty, true);
            List<clsDTOEstructuraCampo> lstEstruc = new List<clsDTOEstructuraCampo>();
            List<clsDTOPerfil> lstPerfil = new List<clsDTOPerfil>();
            List<clsDTOSenalVariable> lstFormulas = new List<clsDTOSenalVariable>();
            #endregion

            intUltimoInsertado = cDtArchivo.mtdAgregarArchivo(strSQLConn, objArchivo, ref strErrMsg);

            if (intUltimoInsertado > 0)
            {
                intRegArchivo = intUltimoInsertado;
                string[] strLineas = mtdGenerarLineas(System.Text.Encoding.Default.GetString(objArchivo.BArchivo), '\n');
                objArchivoTemp = new clsDTOArchivo(intUltimoInsertado.ToString(), string.Empty, string.Empty, null);

                if (strLineas.Length > 0)
                {
                    mtdCargarInfoArchivo(strOleConn, objArchivoTemp, strLineas, intNumeroCampos, ref strErrMsg);

                    /* se realiza busqueda y calculos para encontrar el perfil del registro*/
                    #region Procesamiento
                    if (string.IsNullOrEmpty(strErrMsg))
                    {
                        dtInfoReg = mtdNumeroRegistrosArchivo(strOleConn, objArchivoTemp, ref strErrMsg);

                        if (dtInfoReg.Rows.Count > 0)
                        {
                            #region Consulta de informacion para recorrer y evaluar en los perfiles
                            dtInfoCargue = mtdConsultarInfoCargada(strOleConn, objArchivoTemp, ref strErrMsg);
                            lstEstruc = cParam.mtdCargarInfoEstructura(strOleConn, objVariable, ref strErrMsg);
                            lstPerfil = cPerfil.mtdCargarInfoPerfiles(strOleConn, ref strErrMsg);
                            lstFormulas = cSenal.mtdConsultarFormulas(strOleConn, ref strErrMsg);
                            #endregion

                            #region Recorrido del archivo linea por linea
                            foreach (DataRow drLinea in dtInfoReg.Rows)
                            {
                                cPerfil.mtdEvaluarPerfiles(strSQLConn, strOleConn, drLinea, dtInfoCargue,
                                    lstEstruc, lstPerfil, lstFormulas, objArchivoTemp, intIdUsuario, strNombreUsuario, ref strErrMsg);

                                if (!string.IsNullOrEmpty(strErrMsg))
                                    break;
                            }
                            #endregion
                        }
                        else
                            strErrMsg = "No hay registros en el archivo.";
                    }
                    #endregion
                }
                else
                    strErrMsg = "No hay registros en el archivo.";
            }

            if (!string.IsNullOrEmpty(strErrMsg) && (intUltimoInsertado > 0))
                cDtArchivo.mtdBorrarErroresArchivo(strOleConn, 1, objArchivoTemp);
        }

        public int mtdConsultarConsecutivoArchivo(string strOleConn, ref string strErrMsg)
        {
            int intConsecutivo = 0;
            DataTable dtInfo = new DataTable();
            clsDtArchivo cDtArchivo = new clsDtArchivo();

            dtInfo = cDtArchivo.mtdConsultarConsecutivoArchivo(strOleConn, ref strErrMsg);

            if (dtInfo != null)
                if (dtInfo.Rows.Count > 0)
                    intConsecutivo = Convert.ToInt32(dtInfo.Rows[0]["NumRegistros"].ToString().Trim());

            return intConsecutivo;
        }

        private void mtdCargarInfoArchivo(string strOleConn, clsDTOArchivo objArchivo, string[] strLineas, int intNumeroCampos, ref string strErrMsg)
        {
            clsDtArchivo cDtArchivo = new clsDtArchivo();

            cDtArchivo.mtdCargarInfoArchivo(strOleConn, objArchivo, strLineas, intNumeroCampos, ref strErrMsg);
        }

        private DataTable mtdNumeroRegistrosArchivo(string strOleConn, clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            clsDtArchivo cDtArchivo = new clsDtArchivo();

            return cDtArchivo.mtdNumeroRegistrosArchivo(strOleConn, objArchivo, ref strErrMsg);
        }

        private DataTable mtdConsultarInfoCargada(string strOleConn, clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            clsDtArchivo cDtArchivo = new clsDtArchivo();

            return cDtArchivo.mtdConsultarInfoCargada(strOleConn, objArchivo, ref strErrMsg);
        }

        public List<clsDTOArchivo> mtdConsultarArchivos(string strFechaInicial, string strFechaFinal, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDTOArchivo objArchivo = new clsDTOArchivo();
            clsDtArchivo cDtArchivo = new clsDtArchivo();
            List<clsDTOArchivo> lstArchivo = new List<clsDTOArchivo>();
            #endregion Vars

            dtInfo = cDtArchivo.mtdConsultarArchivos(strFechaInicial, strFechaFinal, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objArchivo = new clsDTOArchivo(
                            dr["IdArchivo"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["UrlArchivo"].ToString().Trim(),
                            (byte[])dr["Archivo"]);
                        lstArchivo.Add(objArchivo);
                    }
                }
                else
                {
                    lstArchivo = null;
                    strErrMsg = "No hay información de Archivos.";
                }
            }
            else
                lstArchivo = null;

            return lstArchivo;
        }
    }
}
