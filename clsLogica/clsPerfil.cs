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
    public class clsPerfil
    {

        // Trae las posiciones donde se guardan estos campos
        readonly string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        readonly string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        readonly string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();
        private clsDatos.clsDatabase cDataBase = new clsDatos.clsDatabase();

        /// <summary>
        /// Metodo que consulta todos los perfiles configurados.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public List<clsDTOPerfil> mtdCargarInfoPerfiles(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            clsDTOPerfil objPerfil = new clsDTOPerfil();
            List<clsDTOPerfil> lstPerfil = new List<clsDTOPerfil>();
            #endregion Vars

            dtInfo = cDtPerfil.mtdConsultaPerfiles(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objPerfil = new clsDTOPerfil(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdPerfil"].ToString().Trim(),
                            dr["NombrePerfil"].ToString().Trim(),
                            dr["ValorMinimo"].ToString().Trim(),
                            dr["ValorMaximo"].ToString().Trim()
                            );

                        lstPerfil.Add(objPerfil);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstPerfil = null;
                    strErrMsg = "No hay información de perfiles.";
                }
            }
            else
                lstPerfil = null;

            return lstPerfil;
        }

        public List<clsDTOPerfilMod> mtdCargarInfoPerfilesMod(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            clsDTOPerfilMod objPerfil = new clsDTOPerfilMod();
            List<clsDTOPerfilMod> lstPerfil = new List<clsDTOPerfilMod>();
            #endregion Vars

            dtInfo = cDtPerfil.mtdConsultaPerfilesMod(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objPerfil = new clsDTOPerfilMod(
                            dr["IdPerfil"].ToString().Trim(),
                            dr["NombrePerfil"].ToString().Trim(),
                            dr["ValorMinimo"].ToString().Trim(),
                            dr["ValorMaximo"].ToString().Trim(),
                            dr["IdUsuario"].ToString().Trim(),
                            dr["Usuario"].ToString().Trim(),
                            dr["FechaModificacion"].ToString().Trim()
                            );

                        lstPerfil.Add(objPerfil);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstPerfil = null;
                    strErrMsg = "No hay información de perfiles.";
                }
            }
            else
                lstPerfil = null;

            return lstPerfil;
        }

        public void mtdAgregarPerfilCreate(clsDTOPerfilCreate objPerfil, ref string strErrMsg)
        {
            clsDtPerfil cDtPerfil = new clsDtPerfil();

            cDtPerfil.mtdAgregarPerfilCreate(objPerfil, ref strErrMsg);
        }
        public void mtdAgregarPerfil(clsDTOPerfil objPerfil, ref string strErrMsg)
        {
            clsDtPerfil cDtPerfil = new clsDtPerfil();

            cDtPerfil.mtdAgregarPerfil(objPerfil, ref strErrMsg);
        }

        public void mtdActualizarPerfil(clsDTOPerfil objPerfil, ref string strErrMsg)
        {
            clsDtPerfil cDtPerfil = new clsDtPerfil();

            cDtPerfil.mtdActualizarPerfil(objPerfil, ref strErrMsg);
        }

        /// <summary>
        /// Metodo que permite hacer la evaluacion de los registros de acuerdo a los perfiles configurados 
        /// </summary>
        /// <param name="drLinea">Numero de Linea.</param>
        /// <param name="dtInfoCargue">Informacion del archivo cargado.</param>
        /// <param name="lstEstruc">Lista con la estructura del archivo.</param>
        /// <param name="lstPerfil">Lista con los perfiles configurados.</param>
        /// <param name="objArchivo">Estructura de los campos.</param>
        /// <param name="strErrMsg">Mensaje de error.</param>
        public void mtdEvaluarPerfiles(DataRow drLinea, DataTable dtInfoCargue,
            List<clsDTOEstructuraCampo> lstEstruc, List<clsDTOPerfil> lstPerfil,
            List<clsDTOSenalVariable> lstFormulas, clsDTOArchivo objArchivo,
            int intIdUsuario, string strNombreUsuario, int ultimoArchivo,
            ref string strErrMsg)
        {
            #region Vars
            bool booContinuar = true;
            decimal decSumPerfil = 0;
            double douSumPerfil = 0;
            int intConteoOcurrencias = 0;
            string strPosNombreCliente = string.Empty, strPosNroID = string.Empty, strPosProducto = string.Empty, strPosTipoDoc = string.Empty,
                strTipoDoc = string.Empty, strNombreCliente = string.Empty, strNroID = string.Empty, strProducto = string.Empty,
                strFiltro = string.Empty, strTextoCliente = string.Empty, strSenalesAlertas = string.Empty,
                strIndicadorROI = string.Empty;
            clsDTOParametrizacion objCategoria = new clsDTOParametrizacion();
            clsDTOVariable objVariable = new clsDTOVariable();
            clsDTOHistoricoCalculoPerfil objHist = new clsDTOHistoricoCalculoPerfil();
            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            List<clsDTOSenal> lstSenal = new List<clsDTOSenal>();
            List<clsDTOSenal> lstSenalTemp = new List<clsDTOSenal>();
            List<object> lstInfoRegistro = new List<object>();
            // Crea el objeto para el informe detalle calificacion
            List<SalidaDetalleCalificacion> lstSalidaDetalle = new List<SalidaDetalleCalificacion>();
            List<clsDTOParametrizacion> lstPar = new List<clsDTOParametrizacion>();
            #endregion Vars

            try
            {
                #region Re-inicio vars
                mtdConsultarPosiciones(ref strPosNombreCliente, ref strPosNroID, ref strPosProducto, ref strPosTipoDoc);

                strNombreCliente = string.Empty;
                strNroID = string.Empty;
                strProducto = string.Empty;
                strTipoDoc = string.Empty;
                strFiltro = "[NumeroLinea]=" + drLinea["NumeroLinea"].ToString().Trim();

                #endregion Re-inicio vars
                int iteracion = 0;
                #region Recorre todos los campos para realizar calculos
                foreach (clsDTOEstructuraCampo objEstruct in lstEstruc)
                {
                    #region Valida si debe continuar
                    /*if (!booContinuar)
                        break;*/
                    #endregion

                    #region Recorre la informacion del archivo cargado

                    foreach (DataRow drInfo in dtInfoCargue.Select(strFiltro))
                    {

                        if (drInfo["Posicion"].ToString().Trim() != objEstruct.StrPosicion)
                        {
                            int posicion = Convert.ToInt32(drInfo["Posicion"].ToString().Trim()) ;// -1
                            #region Trae la informacion obligatoria del cliente
                            if (posicion.ToString() == SenalAlertaPosNombre)
                                strNombreCliente = drInfo["ValorCampoArchivo"].ToString().Trim();

                            if (/*drInfo["Posicion"].ToString().Trim()*/posicion.ToString() == SenalAlertaPosNumeroIden)
                                strNroID = drInfo["ValorCampoArchivo"].ToString().Trim();

                            if (/*drInfo["Posicion"].ToString().Trim()*/posicion.ToString() == strPosProducto)
                                strProducto = drInfo["ValorCampoArchivo"].ToString().Trim();

                            if (/*drInfo["Posicion"].ToString().Trim()*/posicion.ToString() == SenalAlertaPosTipoIden)
                                strTipoDoc = drInfo["ValorCampoArchivo"].ToString().Trim();
                            #endregion

                            continue;
                        }
                        else
                        {
                            #region Procesamiento de Calculos y Alertas
                            if (string.IsNullOrEmpty(drInfo["ValorCampoArchivo"].ToString().Trim()))
                                continue;
                            else
                            {
                                lstPar = new List<clsDTOParametrizacion>();
                                #region Realiza los calculos
                                #region Consulta Calificacion Categoria
                                //saca informacion de la categoria y la computa con la infor de la variable
                                string valorAr = drInfo["ValorCampoArchivo"].ToString().Trim();
                                /*objCategoria =
                                    (cParam.mtdConsultarCalifParam(
                                        objEstruct.StrIdEstructCampo, drInfo["ValorCampoArchivo"].ToString().Trim(), ref strErrMsg)) == null ?
                                        null :
                                        (cParam.mtdConsultarCalifParam(objEstruct.StrIdEstructCampo, drInfo["ValorCampoArchivo"].ToString().Trim(), ref strErrMsg))[0];*/
                                lstPar = (cParam.mtdConsultarCalifParam(objEstruct.StrIdEstructCampo, drInfo["ValorCampoArchivo"].ToString().Trim(), ref strErrMsg));
                                #endregion
                                //objCategoria == null
                                #region control de errores
                                if (lstPar == null)
                                {
                                    if (strErrMsg == "No hay información de categorías.")
                                    {
                                        strErrMsg = string.Empty;
                                        continue;
                                    }
                                    else
                                    {
                                        booContinuar = false;
                                        break;
                                    }
                                }
                                #endregion

                                objVariable = cParam.mtdConsultaTipoParametro(objEstruct, ref strErrMsg);

                                #region control de errores
                                if (!string.IsNullOrEmpty(strErrMsg))
                                {
                                    booContinuar = false;
                                    break;
                                }
                                #endregion
                                objCategoria = lstPar[0];
                                decSumPerfil += ((Convert.ToDecimal(objCategoria.StrCalificacionCategoria) * Convert.ToDecimal(objVariable.StrCalificacion) / 100));
                                douSumPerfil = ((Convert.ToDouble(objCategoria.StrCalificacionCategoria) * Convert.ToDouble(objVariable.StrCalificacion) / 100));
                                #endregion
                                
                                // Agrega información para el informe detalle calificacion
                                int pos = Convert.ToInt32(drInfo["Posicion"].ToString().Trim());
                                string var = objVariable.StrNombreVariable;
                                double peso = Convert.ToDouble(objVariable.StrCalificacion);
                                string cat = objCategoria.StrNombreCategoria;
                                double pesoCat = Convert.ToDouble(objCategoria.StrCalificacionCategoria);
                                string valor = drInfo["ValorCampoArchivo"].ToString();
                                lstSalidaDetalle.Add(new SalidaDetalleCalificacion()
                                {
                                    Posicion = pos,
                                    NombreVariable = var,
                                    PesoVariable = peso,
                                    NombreCategoria = cat,
                                    PesoCategoria = pesoCat,
                                    Calificacion = douSumPerfil,
                                    Valor = valor,
                                    NumeroDocumento = strNroID,
                                    UltimoArchivo = ultimoArchivo
                                });

                                mtdGenerarAlerta(drInfo, objEstruct, lstFormulas, intIdUsuario, ref lstSenalTemp, ref lstSenal, ref strIndicadorROI, ref strErrMsg);
                            }
                            #endregion

                            #region Revision de Lista de Señales de alerta
                            foreach (clsDTOSenal objTemp in lstSenal)
                            {
                                lstSenalTemp.RemoveAll(x => x.StrIdSenal == objTemp.StrIdSenal);
                            }
                            #endregion
                        }
                    }
                    iteracion++;
                    #endregion
                }
                #endregion

                #region Lista Info Cliente del registro y Conteo
                if (lstSenal.Count > 0)
                {
                    object objInfoRegistro = new object();
                    objInfoRegistro = strNombreCliente + "|" + strTipoDoc + "|" + strNroID;
                    lstInfoRegistro.Add(objInfoRegistro);
                    intConteoOcurrencias++;
                }
                #endregion

                #region Envio de Notificaciones
                strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ", strNombreCliente, strTipoDoc, strNroID);

                foreach (clsDTOSenal objSenal in lstSenal)
                {
                    if (string.IsNullOrEmpty(strSenalesAlertas))
                        strSenalesAlertas = objSenal.StrCodigoSenal;
                    else
                        strSenalesAlertas = strSenalesAlertas + " | " + objSenal.StrCodigoSenal;

                    cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenal.StrCodigoSenal, objSenal.StrDescripcionSenal, intIdUsuario,
                        strTextoCliente, ref strErrMsg);
                }
                #endregion

                #region Registro de operacion
                mtdRegistroOperacion(intConteoOcurrencias, lstFormulas, strNombreUsuario, intIdUsuario, lstInfoRegistro, strIndicadorROI, ref strErrMsg);
                #endregion

                #region Recorre todos los perfiles para evaluar a que perfil pertenece el cliente
                if (string.IsNullOrEmpty(strErrMsg))
                    foreach (clsDTOPerfil objPerfil in lstPerfil)
                    {
                        if (decSumPerfil >= (Convert.ToDecimal(objPerfil.StrValorMinimo)) && (decSumPerfil < Convert.ToDecimal(objPerfil.StrValorMaximo)))
                        {
                            //* Guardar la informacion */
                            objHist = new clsDTOHistoricoCalculoPerfil(string.Empty, objPerfil.StrIdPerfil, objArchivo.StrIdArchivo, strTipoDoc, strNroID, strNombreCliente,
                                decSumPerfil.ToString(), strProducto, string.Empty, strSenalesAlertas, string.Empty, drLinea["NumeroLinea"].ToString().Trim());

                            cDtPerfil.mtdAgregarHistPerfil(objHist, ref strErrMsg);

                            if (!string.IsNullOrEmpty(strErrMsg))
                                break;
                        }
                    }
                #endregion

                // Reinicia la variable de calificación
                decSumPerfil = 0;
                douSumPerfil = 0;


                #region Detalle Calificacion
                if (lstSalidaDetalle.Count > 0)
                {
                    foreach (SalidaDetalleCalificacion registro in lstSalidaDetalle)
                    {
                        List<SqlParameter> parametros = new List<SqlParameter>()
                        {
                            new SqlParameter() { ParameterName = "@Posicion", SqlDbType = SqlDbType.Int, Value =  registro.Posicion },
                            new SqlParameter() { ParameterName = "@NombreVariable", SqlDbType = SqlDbType.VarChar, Value =  registro.NombreVariable },
                            new SqlParameter() { ParameterName = "@PesoVariable", SqlDbType = SqlDbType.Float, Value =  registro.PesoVariable },
                            new SqlParameter() { ParameterName = "@NombreCategoria", SqlDbType = SqlDbType.VarChar, Value =  registro.NombreCategoria },
                            new SqlParameter() { ParameterName = "@PesoCategoria", SqlDbType = SqlDbType.Float, Value =  registro.PesoCategoria },
                            new SqlParameter() { ParameterName = "@Calificacion", SqlDbType = SqlDbType.Decimal, Value =  registro.Calificacion },
                            new SqlParameter() { ParameterName = "@Valor", SqlDbType = SqlDbType.VarChar, Value =  registro.Valor },
                            new SqlParameter() { ParameterName = "@NumeroDocumento", SqlDbType = SqlDbType.VarChar, Value =  registro.NumeroDocumento },
                            new SqlParameter() { ParameterName = "@IdArchivo ", SqlDbType = SqlDbType.VarChar, Value =  registro.UltimoArchivo },
                            new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                        };
                        cDataBase.EjecutarSPParametrosReturnInteger("Perfiles.InsertarDetalleCalificacion", parametros);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #region Otros metodos
        public void mtdConsultarPosiciones(ref string strNombreCliente, ref string strNroID,
            ref string strProducto, ref string strTipoDoc)
        {
            string strErrMsg = string.Empty;
            clsParamArchivo cParam = new clsParamArchivo();

            strNroID = cParam.mtdConsultarPosXCampo(SenalAlertaPosNumeroIden, ref strErrMsg);
            strNombreCliente = cParam.mtdConsultarPosXCampo(SenalAlertaPosNombre, ref strErrMsg);
            strProducto = cParam.mtdConsultarPosXCampo("PRODUCTO", ref strErrMsg);
            strTipoDoc = cParam.mtdConsultarPosXCampo(SenalAlertaPosTipoIden, ref strErrMsg);
        }

        /// <summary>
        /// Metodo que consulta el historico y toma el mayor perfil de acuerdo al cliente.
        /// </summary>
        /// <param name="objArchivo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public List<clsDTOHistoricoCalculoPerfil> mtdConsultarHistPerfiles(clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            clsDTOHistoricoCalculoPerfil objHist = new clsDTOHistoricoCalculoPerfil();
            List<clsDTOHistoricoCalculoPerfil> lstHist = new List<clsDTOHistoricoCalculoPerfil>();
            #endregion Vars

            dtInfo = cDtPerfil.mtdConsultaHistorico(objArchivo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objHist = new clsDTOHistoricoCalculoPerfil(
                            dr["IdHistorico"].ToString().Trim(),
                            dr["IdPerfil"].ToString().Trim(),
                            objArchivo.StrIdArchivo,
                            dr["TipoDocumento"].ToString().Trim(),
                            dr["NumeroDocumento"].ToString().Trim(),
                            dr["NombreCliente"].ToString().Trim(),
                            dr["Calificacion"].ToString().Trim(),
                            dr["Producto"].ToString().Trim(),
                            dr["InfoInspektor"].ToString().Trim(),
                            dr["SenalesAlerta"].ToString().Trim(),
                            dr["FechaGeneracion"].ToString().Trim(),
                            dr["Linea"].ToString().Trim());

                        lstHist.Add(objHist);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstHist = null;
                    strErrMsg = "No hay información de histórico de perfiles.";
                }
            }
            else
                lstHist = null;

            return lstHist;
        }

        public DataSet mtdConsultarRptPerfiles(string strFechaInicial, string strFechaFinal, string strNroIdentificacion,
            string strPerfil, ref string strErrMsg)
        {
            DataSet dsInfo = new DataSet();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            strFechaInicial = strFechaInicial + " 00:00";
            strFechaFinal = strFechaFinal + " 23:59";
            dsInfo = cDtPerfil.mtdConsultaRptPerfiles(strFechaInicial, strFechaFinal, strNroIdentificacion, strPerfil, ref strErrMsg);

            return dsInfo;
        }

        public List<SalidaDetalleCalificacion> ConsultarDetalle(string numeroDocumento, int idArchivo)
        {
            try
            {
                List<SalidaDetalleCalificacion> lst = new List<SalidaDetalleCalificacion>();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@NumeroDocumento", SqlDbType = SqlDbType.VarChar, Value =  numeroDocumento },
                    new SqlParameter() { ParameterName = "@IdArchivo", SqlDbType = SqlDbType.Int, Value =  idArchivo }
                };
                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("Perfiles.SeleccionarDetalleCalificacion", parametros))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new SalidaDetalleCalificacion()
                            {
                                IdDetalleCalificacion = Convert.ToInt32(Row["IdDetalleCalificacion"].ToString()),
                                Posicion = Convert.ToInt32(Row["Posicion"].ToString()),
                                NombreVariable = Row["NombreVariable"].ToString(),
                                PesoVariable = Convert.ToDouble(Row["PesoVariable"].ToString()),
                                NombreCategoria = Row["NombreCategoria"].ToString(),
                                PesoCategoria = Convert.ToDouble(Row["PesoCategoria"].ToString()),
                                Calificacion = Convert.ToDouble(Row["Calificacion"].ToString()),
                                Valor = Row["Valor"].ToString(),
                                NumeroDocumento = Row["NumeroDocumento"].ToString(),
                                UltimoArchivo = Convert.ToInt32(Row["IdArchivo"].ToString())
                            });
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet mtdConsultarRptPerfilesDetalle(string strFechaInicial, string strFechaFinal, string strNroIdentificacion,
            string strPerfil, ref string strErrMsg)
        {
            #region Variables
            string strLinea = string.Empty;
            DataSet dsInfo = new DataSet(), dsInfoTemp = new DataSet();
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            #endregion

            #region Consulta y recorrido de la informacion detalle
            dsInfoTemp = cDtPerfil.mtdConsultaRptPerfilesDetalle(strFechaInicial, strFechaFinal, strNroIdentificacion, strPerfil, ref strErrMsg);

            #region Comentario de parte para linealizar campos
            //DataRow workRow = null;
            //DataTable workTable = dsInfo.Tables.Add();   //new DataTable();

            #region Columnas nuevas a linealizar
            //List<clsDTOEstructuraCampo> lstEC = new List<clsDTOEstructuraCampo>();
            //lstEC = cParamArchivo.mtdCargarInfoEstructura(ref strErrMsg);

            #region Columnas fijas
            //workTable.Columns.Add("IdHistorico", typeof(String));
            //workTable.Columns.Add("NombrePerfil", typeof(String));
            //workTable.Columns.Add("TipoDocumento", typeof(String));
            //workTable.Columns.Add("NumeroDocumento", typeof(String));
            //workTable.Columns.Add("NombreCliente", typeof(String));
            //workTable.Columns.Add("FechaGeneracion", typeof(String));
            //workTable.Columns.Add("SenalesAlerta", typeof(String));
            //workTable.Columns.Add("InfoInspektor", typeof(String));
            #endregion

            #region Columnas Variables
            //foreach (clsDTOEstructuraCampo objEC in lstEC)
            //{
            //    workTable.Columns.Add(objEC.StrNombreCampo.Trim(), typeof(String));
            //}
            #endregion

            #endregion

            #region Recorrido
            //foreach (DataRow dr in dsInfoTemp.Tables[0].Rows)
            //{
            //    if (strLinea.Trim() != dr["NumeroLinea"].ToString().Trim())
            //    {
            //        if (workRow != null)
            //            workTable.Rows.Add(workRow);

            //        workRow = workTable.NewRow();

            //        strLinea = dr["NumeroLinea"].ToString().Trim();

            //        #region Info que va una sola vez
            //        workRow["IdHistorico"] = dr["IdHistorico"].ToString().Trim();
            //        workRow["NombrePerfil"] = dr["NombrePerfil"].ToString().Trim();
            //        workRow["TipoDocumento"] = dr["TipoDocumento"].ToString().Trim();
            //        workRow["NumeroDocumento"] = dr["NumeroDocumento"].ToString().Trim();
            //        workRow["FechaGeneracion"] = dr["FechaGeneracion"].ToString().Trim();
            //        workRow["NombreCliente"] = dr["NombreCliente"].ToString().Trim();
            //        workRow["SenalesAlerta"] = dr["SenalesAlerta"].ToString().Trim();
            //        workRow["InfoInspektor"] = dr["InfoInspektor"].ToString().Trim();
            //        #endregion
            //    }

            //    if (workRow != null)
            //        workRow[dr["NombreCampo"].ToString().Trim()] = dr["ValorCampoArchivo"].ToString().Trim();
            //}
            #endregion

            //if (workRow != null)
            //    workTable.Rows.Add(workRow);
            #endregion
            #endregion

            return dsInfoTemp;
        }

        private void mtdGenerarAlerta(DataRow drInfo, clsDTOEstructuraCampo objEstruc,
            List<clsDTOSenalVariable> lstFormulas, int intIdUsuario, ref List<clsDTOSenal> lstSenalIn,
            ref List<clsDTOSenal> lstSenalOut, ref string strIndicadorROI, ref string strErrMsg)
        {
            #region Vars
            int intNroCondiciones = 0, intNroSenales = 0;
            bool booIsVar = false, booIsOp = false, booIsValue = false, booIsFirst = true, booIsSenal = false;
            string strOtroValor = string.Empty, strRangoDesde = string.Empty, strRangoHasta = string.Empty;
            string strIdSenal = string.Empty, strIdSenalTmp = string.Empty;
            clsSenal cSenal = new clsSenal();
            clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
            clsDTOSenal objSenalIn = new clsDTOSenal(), objSenalOut = null;
            #endregion

            if (drInfo["Posicion"].ToString().Trim() == objEstruc.StrPosicion)
            {
                foreach (clsDTOSenalVariable objFormula in lstFormulas)
                {
                    if (!objFormula.BooEsGlobal)
                    {
                        #region Control Error
                        if (!string.IsNullOrEmpty(strErrMsg))
                            break;
                        #endregion

                        #region Toma Informacion para buscar la informacion de la senal
                        if (strIdSenal != objFormula.StrIdSenal)
                        {
                            strIdSenal = objFormula.StrIdSenal;
                            objSenalOut = null;
                            booIsVar = booIsOp = booIsValue = false;
                        }
                        #endregion

                        #region Recoge la informacion de los campos de la formula para evaluar
                        switch (objFormula.StrIdOperando)
                        {
                            case "1": //Variable
                                #region Variable
                                objSenalOut = new clsDTOSenal();
                                objSenalOut = cSenal.mtdConsultarSenal(new clsDTOSenal(objFormula.StrIdSenal, string.Empty, string.Empty, string.Empty, false), ref strErrMsg);
                                booIsSenal = true;
                                //if (objEstruc.StrIdVariable == objFormula.StrValor)
                                if (objEstruc.StrIdEstructCampo == objFormula.StrValor)
                                    booIsVar = true;
                                #endregion
                                break;

                            case "2"://Operador
                                #region Operador
                                objOpOut = cSenal.mtdBuscarOperador(new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty), ref strErrMsg);
                                booIsOp = true;
                                #endregion
                                break;

                            case "3"://Otro
                                #region Otro
                                strOtroValor = objFormula.StrValor;
                                booIsValue = true;
                                #endregion
                                break;

                            case "4"://Rango
                                #region Rango
                                if (booIsFirst)
                                {
                                    strRangoDesde = objFormula.StrValor;
                                    booIsFirst = false;
                                }
                                else
                                {
                                    booIsValue = true;
                                    strRangoHasta = objFormula.StrValor;
                                }
                                #endregion
                                break;
                        }
                        #endregion

                        #region Verifica que cumplio todas las condiciones
                        if (booIsValue && booIsOp && booIsVar)
                        {
                            if (mtdHayAlerta(drInfo["ValorCampoArchivo"].ToString().Trim(), objOpOut,
                                strOtroValor, strRangoDesde, strRangoHasta))
                            {
                                lstSenalIn.Add(objSenalOut);

                                if (string.IsNullOrEmpty(strIndicadorROI))
                                    strIndicadorROI = objEstruc.StrNombreCampo + " = " + drInfo["ValorCampoArchivo"].ToString().Trim();
                                else
                                    strIndicadorROI = strIndicadorROI + " | " + objEstruc.StrNombreCampo + " = " + drInfo["ValorCampoArchivo"].ToString().Trim();
                            }
                            else
                                objSenalOut = null;

                            booIsSenal = booIsVar = booIsOp = booIsValue = false;
                        }

                        if (booIsSenal && booIsOp && booIsValue)
                            booIsSenal = booIsVar = booIsOp = booIsValue = false;
                        #endregion
                    }
                }
            }

            #region Verificacion de Condiciones de señales
            clsDTOSenal objSenalTemp = new clsDTOSenal();
            foreach (clsDTOSenal objSenal in lstSenalIn)
            {
                if (strIdSenalTmp != objSenal.StrIdSenal)
                    strIdSenalTmp = objSenal.StrIdSenal;
                else
                    continue;

                intNroSenales = mtdCantidadSenales(objSenal.StrIdSenal, lstSenalIn);
                intNroCondiciones = mtdCantidadCondiciones(objSenal.StrIdSenal, lstFormulas);

                if (intNroCondiciones == intNroSenales)
                    lstSenalOut.Add(objSenal);
            }
            #endregion
        }

        private bool mtdHayAlerta(string strCampoEvaluar, clsDTOOperador objOp,
            string strValorFormula, string strValorDesde, string strValorHasta)
        {
            bool booResult = false;

            //strCampoEvaluar.Replace(".", "")
            if (clsUtilidades.mtdEsNumero(strCampoEvaluar.Replace(',', '.')))
            {
                string strVarTempo = strCampoEvaluar.Replace(',', '.');
                switch (objOp.StrIdentificadorOperador)
                {
                    case ">":
                        if (Convert.ToInt64(strVarTempo) > Convert.ToInt64(strValorFormula))
                            booResult = true;
                        break;

                    case "<":
                        if (Convert.ToInt64(strVarTempo) < Convert.ToInt64(strValorFormula))
                            booResult = true;
                        break;

                    case ">=":
                        if (Convert.ToInt64(strVarTempo) >= Convert.ToInt64(strValorFormula))
                            booResult = true;
                        break;

                    case "<=":
                        if (Convert.ToInt64(strVarTempo) <= Convert.ToInt64(strValorFormula))
                            booResult = true;
                        break;

                    case "=":
                        if (Convert.ToInt64(strVarTempo) == Convert.ToInt64(strValorFormula))
                            booResult = true;
                        break;

                    case "Entre":
                        if ((Convert.ToInt64(strVarTempo) >= Convert.ToInt64(strValorDesde)) &&
                            (Convert.ToInt64(strVarTempo) <= Convert.ToInt64(strValorHasta)))
                            booResult = true;
                        break;
                }
            }
            else
            {
                if (objOp.StrIdentificadorOperador == "=")
                    if (strCampoEvaluar == strValorFormula)
                        booResult = true;
            }

            return booResult;
        }

        public void mtdActualizarHistoricoInfoInspektor(clsDTOHistoricoCalculoPerfil objHistorico, ref string strErrMsg)
        {
            #region Vars
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            #endregion Vars

            cDtPerfil.mtdActualizarHistoricoInfoInspektor(objHistorico, ref strErrMsg);
        }

        private int mtdCantidadCondiciones(string strIdSenal, List<clsDTOSenalVariable> lstFormulas)
        {
            int intResult = 0;

            foreach (clsDTOSenalVariable objCondicion in lstFormulas)
            {
                if (objCondicion.StrIdSenal == strIdSenal && objCondicion.StrIdOperando == "1")
                    intResult++;
            }

            return intResult;
        }

        private int mtdCantidadSenales(string strIdSenal, List<clsDTOSenal> lstSenalIn)
        {
            int intResult = 0;

            foreach (clsDTOSenal objSenal in lstSenalIn)
            {
                if (objSenal.StrIdSenal == strIdSenal)
                    intResult++;
            }

            return intResult;
        }
        #endregion

        /*
         * Metodos para el Servicio
         */
        #region Metodos del Servicio
        public void mtdEvaluarPerfiles(string strSQLConn, string strOleConn, DataRow drLinea, DataTable dtInfoCargue,
            List<clsDTOEstructuraCampo> lstEstruc, List<clsDTOPerfil> lstPerfil,
            List<clsDTOSenalVariable> lstFormulas, clsDTOArchivo objArchivo, int intIdUsuario, string strNombreUsuario,
            ref string strErrMsg)
        {
            #region Vars
            bool booContinuar = true;
            decimal decSumPerfil = 0;
            int intConteoOcurrencias = 0;
            string strPosNombreCliente = string.Empty, strPosNroID = string.Empty, strPosProducto = string.Empty, strPosTipoDoc = string.Empty,
                strTipoDoc = string.Empty, strNombreCliente = string.Empty, strNroID = string.Empty, strProducto = string.Empty,
                strFiltro = string.Empty, strTextoCliente = string.Empty, strSenalesAlertas = string.Empty, strIndicadorROI = string.Empty;
            clsDTOParametrizacion objCategoria = new clsDTOParametrizacion();
            clsDTOVariable objVariable = new clsDTOVariable();
            clsDTOHistoricoCalculoPerfil objHist = new clsDTOHistoricoCalculoPerfil();
            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            List<clsDTOSenal> lstSenal = new List<clsDTOSenal>();
            List<clsDTOSenal> lstSenalTemp = new List<clsDTOSenal>();
            List<object> lstInfoRegistro = new List<object>();
            #endregion Vars

            #region Re-inicio vars
            mtdConsultarPosiciones(strOleConn, ref strPosNombreCliente, ref strPosNroID, ref strPosProducto, ref strPosTipoDoc);

            strNombreCliente = string.Empty;
            strNroID = string.Empty;
            strProducto = string.Empty;
            strTipoDoc = string.Empty;
            strFiltro = "[NumeroLinea]=" + drLinea["NumeroLinea"].ToString().Trim();
            #endregion Re-inicio vars

            #region Recorre todos los campos para realizar calculos
            foreach (clsDTOEstructuraCampo objEstruct in lstEstruc)
            {
                #region Valida si debe continuar
                if (!booContinuar)
                    break;
                #endregion

                #region Recorre la informacion del archivo cargado
                foreach (DataRow drInfo in dtInfoCargue.Select(strFiltro))
                {
                    if (drInfo["Posicion"].ToString().Trim() != objEstruct.StrPosicion)
                    {
                        #region Trae la informacion del cliente
                        if (drInfo["Posicion"].ToString().Trim() == strPosNombreCliente)
                            strNombreCliente = drInfo["ValorCampoArchivo"].ToString().Trim();

                        if (drInfo["Posicion"].ToString().Trim() == strPosNroID)
                            strNroID = drInfo["ValorCampoArchivo"].ToString().Trim();

                        if (drInfo["Posicion"].ToString().Trim() == strPosProducto)
                            strProducto = drInfo["ValorCampoArchivo"].ToString().Trim();

                        if (drInfo["Posicion"].ToString().Trim() == strPosTipoDoc)
                            strTipoDoc = drInfo["ValorCampoArchivo"].ToString().Trim();
                        #endregion

                        continue;
                    }
                    else
                    {
                        #region Realiza Calculos y Alertas
                        if (string.IsNullOrEmpty(drInfo["ValorCampoArchivo"].ToString().Trim()))
                            continue;
                        else
                        {
                            #region Realiza los calculos
                            #region Consulta Calificacion Categoria
                            //saca informacion de la categoria y la computa con la infor de la variable
                            objCategoria =
                                (cParam.mtdConsultarCalifParam(strOleConn,
                                    objEstruct.StrIdEstructCampo, drInfo["ValorCampoArchivo"].ToString().Trim(), ref strErrMsg)) == null ? null :
                                    (cParam.mtdConsultarCalifParam(strOleConn, objEstruct.StrIdEstructCampo, drInfo["ValorCampoArchivo"].ToString().Trim(), ref strErrMsg))[0];
                            #endregion

                            #region control de errores
                            if (objCategoria == null)
                            {
                                if (strErrMsg == "No hay información de categorías.")
                                {
                                    strErrMsg = string.Empty;
                                    continue;
                                }
                                else
                                {
                                    booContinuar = false;
                                    break;
                                }
                            }
                            #endregion

                            objVariable = cParam.mtdConsultaTipoParametro(strOleConn, objEstruct, ref strErrMsg);

                            #region control de errores
                            if (!string.IsNullOrEmpty(strErrMsg))
                            {
                                booContinuar = false;
                                break;
                            }
                            #endregion

                            decSumPerfil += ((Convert.ToDecimal(objCategoria.StrCalificacionCategoria) * Convert.ToDecimal(objVariable.StrCalificacion) / 100));
                            #endregion

                            mtdGenerarAlerta(strOleConn, drInfo, objEstruct, lstFormulas, intIdUsuario, ref lstSenalTemp, ref lstSenal, ref strIndicadorROI, ref strErrMsg);
                        }

                        #region Quita informacion de alertas temporales para evitar duplicidad
                        foreach (clsDTOSenal objTemp in lstSenal)
                        {
                            lstSenalTemp.RemoveAll(x => x.StrIdSenal == objTemp.StrIdSenal);
                        }
                        #endregion
                        #endregion
                    }
                }
                #endregion
            }
            #endregion

            #region Lista Info Cliente del registro y Conteo
            if (lstSenal.Count > 0)
            {
                object objInfoRegistro = new object();
                objInfoRegistro = strNombreCliente + "|" + strTipoDoc + "|" + strNroID;
                lstInfoRegistro.Add(objInfoRegistro);
                intConteoOcurrencias++;
            }
            #endregion

            #region Envio de Notificaciones
            strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ", strNombreCliente, strTipoDoc, strNroID);
            foreach (clsDTOSenal objSenal in lstSenal)
            {
                if (string.IsNullOrEmpty(strSenalesAlertas))
                    strSenalesAlertas = objSenal.StrCodigoSenal;
                else
                    strSenalesAlertas = strSenalesAlertas + " | " + objSenal.StrCodigoSenal;

                cUtil.mtdGenerarNotificacion(strSQLConn, strOleConn, 24, "SEÑAL DE ALERTA", objSenal.StrCodigoSenal, objSenal.StrDescripcionSenal, intIdUsuario,
                    strTextoCliente, ref strErrMsg);
            }
            #endregion

            #region Registro de operacion
            mtdRegistroOperacion(strSQLConn, strOleConn, intConteoOcurrencias, lstFormulas, strNombreUsuario, intIdUsuario, lstInfoRegistro, strIndicadorROI, ref strErrMsg);
            #endregion

            #region Recorre todos los perfiles para evaluar a que perfil pertenece el cliente
            if (string.IsNullOrEmpty(strErrMsg))
                foreach (clsDTOPerfil objPerfil in lstPerfil)
                {
                    if ((Convert.ToDecimal(objPerfil.StrValorMinimo) <= decSumPerfil) && (Convert.ToDecimal(objPerfil.StrValorMaximo) >= decSumPerfil))
                    {
                        //* Guardar la informacion */
                        objHist = new clsDTOHistoricoCalculoPerfil(string.Empty, objPerfil.StrIdPerfil, objArchivo.StrIdArchivo, strTipoDoc, strNroID, strNombreCliente,
                            decSumPerfil.ToString(), strProducto, string.Empty, strSenalesAlertas, string.Empty, drLinea["NumeroLinea"].ToString().Trim());

                        cDtPerfil.mtdAgregarHistPerfil(strOleConn, objHist, ref strErrMsg);

                        if (!string.IsNullOrEmpty(strErrMsg))
                            break;
                    }
                }
            #endregion
        }

        public void mtdConsultarPosiciones(string strOleConn, ref string strNombreCliente, ref string strNroID,
           ref string strProducto, ref string strTipoDoc)
        {
            string strErrMsg = string.Empty;
            clsParamArchivo cParam = new clsParamArchivo();

            strNroID = cParam.mtdConsultarPosXCampo(strOleConn, SenalAlertaPosNumeroIden, ref strErrMsg);
            strNombreCliente = cParam.mtdConsultarPosXCampo(strOleConn, SenalAlertaPosNombre, ref strErrMsg);
            strProducto = cParam.mtdConsultarPosXCampo(strOleConn, "PRODUCTO", ref strErrMsg);
            strTipoDoc = cParam.mtdConsultarPosXCampo(strOleConn, SenalAlertaPosTipoIden, ref strErrMsg);
        }

        private void mtdGenerarAlerta(string strOleConn, DataRow drInfo, clsDTOEstructuraCampo objEstruc,
            List<clsDTOSenalVariable> lstFormulas, int intIdUsuario, ref List<clsDTOSenal> lstSenalIn,
            ref List<clsDTOSenal> lstSenalOut, ref string strIndicadorROI, ref string strErrMsg)
        {
            #region Vars
            int intNroCondiciones = 0, intNroSenales = 0;
            bool booIsVar = false, booIsOp = false, booIsValue = false, booIsFirst = true, booIsSenal = false;
            string strOtroValor = string.Empty, strRangoDesde = string.Empty, strRangoHasta = string.Empty;
            string strIdSenal = string.Empty, strIdSenalTmp = string.Empty;
            clsSenal cSenal = new clsSenal();
            clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
            clsDTOSenal objSenalIn = new clsDTOSenal(), objSenalOut = null;
            #endregion

            if (drInfo["Posicion"].ToString().Trim() == objEstruc.StrPosicion)
            {
                foreach (clsDTOSenalVariable objFormula in lstFormulas)
                {
                    #region Control Error
                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                    #endregion

                    #region Toma Informacion para buscar la informacion de la senal
                    if (strIdSenal != objFormula.StrIdSenal)
                    {
                        strIdSenal = objFormula.StrIdSenal;
                        objSenalOut = null;
                        booIsVar = booIsOp = booIsValue = false;
                    }
                    #endregion

                    #region Recoge la informacion de los campos de la formula para evaluar
                    switch (objFormula.StrIdOperando)
                    {
                        case "1": //Variable
                            #region Variable
                            objSenalOut = new clsDTOSenal();
                            objSenalOut = cSenal.mtdConsultarSenal(strOleConn, new clsDTOSenal(objFormula.StrIdSenal, string.Empty, string.Empty, string.Empty, false), ref strErrMsg);
                            booIsSenal = true;
                            if (objEstruc.StrIdVariable == objFormula.StrValor)
                                booIsVar = true;
                            #endregion
                            break;

                        case "2"://Operador
                            #region Operador
                            objOpOut = cSenal.mtdBuscarOperador(strOleConn, new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty), ref strErrMsg);
                            booIsOp = true;
                            #endregion
                            break;

                        case "3"://Otro
                            #region Otro
                            strOtroValor = objFormula.StrValor;
                            booIsValue = true;
                            #endregion
                            break;

                        case "4"://Rango
                            #region Rango
                            if (booIsFirst)
                            {
                                strRangoDesde = objFormula.StrValor;
                                booIsFirst = false;
                            }
                            else
                            {
                                booIsValue = true;
                                strRangoHasta = objFormula.StrValor;
                            }
                            #endregion
                            break;
                    }
                    #endregion

                    #region Verifica que cumplio todas las condiciones
                    if (booIsValue && booIsOp && booIsVar)
                    {
                        if (mtdHayAlerta(drInfo["ValorCampoArchivo"].ToString().Trim(), objOpOut,
                                strOtroValor, strRangoDesde, strRangoHasta))
                        {
                            lstSenalIn.Add(objSenalOut);

                            if (string.IsNullOrEmpty(strIndicadorROI))
                                strIndicadorROI = objEstruc.StrNombreCampo + " = " + drInfo["ValorCampoArchivo"].ToString().Trim();
                            else
                                strIndicadorROI = strIndicadorROI + " | " + objEstruc.StrNombreCampo + " = " + drInfo["ValorCampoArchivo"].ToString().Trim();
                        }
                        else
                            objSenalOut = null;

                        booIsSenal = booIsVar = booIsOp = booIsValue = false;
                    }

                    if (booIsSenal && booIsOp && booIsValue)
                        booIsSenal = booIsVar = booIsOp = booIsValue = false;
                    #endregion
                }
            }

            #region Verificacion de Condiciones de señales
            clsDTOSenal objSenalTemp = new clsDTOSenal();
            foreach (clsDTOSenal objSenal in lstSenalIn)
            {
                if (strIdSenalTmp != objSenal.StrIdSenal)
                    strIdSenalTmp = objSenal.StrIdSenal;
                else
                    continue;

                intNroSenales = mtdCantidadSenales(objSenal.StrIdSenal, lstSenalIn);
                intNroCondiciones = mtdCantidadCondiciones(objSenal.StrIdSenal, lstFormulas);

                if (intNroCondiciones == intNroSenales)
                    lstSenalOut.Add(objSenal);
            }
            #endregion
        }

        public List<clsDTOPerfil> mtdCargarInfoPerfiles(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            clsDTOPerfil objPerfil = new clsDTOPerfil();
            List<clsDTOPerfil> lstPerfil = new List<clsDTOPerfil>();
            #endregion Vars

            dtInfo = cDtPerfil.mtdConsultaPerfiles(strOleConn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objPerfil = new clsDTOPerfil(
                            dr["IdUsuario"].ToString().Trim(),
                            dr["IdPerfil"].ToString().Trim(),
                            dr["NombrePerfil"].ToString().Trim(),
                            dr["ValorMinimo"].ToString().Trim(),
                            dr["ValorMaximo"].ToString().Trim());

                        lstPerfil.Add(objPerfil);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstPerfil = null;
                    strErrMsg = "No hay información de perfiles.";
                }
            }
            else
                lstPerfil = null;

            return lstPerfil;
        }

        public void mtdActualizarHistoricoInfoInspektor(string strOleConn, clsDTOHistoricoCalculoPerfil objHistorico, ref string strErrMsg)
        {
            #region Vars
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            #endregion Vars

            cDtPerfil.mtdActualizarHistoricoInfoInspektor(strOleConn, objHistorico, ref strErrMsg);
        }

        public void mtdRegistroOperacion(string strSQLConn, string strOleConn, int intConteoOcurrencias, List<clsDTOSenalVariable> lstFormulas,
            string strNombreUsuario, int intIdUsuario, List<object> lstInfoRegistro, string strIndicadorROI, ref string strErrMsg)
        {
            int intConteoTblConteoRegistro = 0;
            clsSenal cSenal = new clsSenal();

            #region Registro de operacion
            if (string.IsNullOrEmpty(strErrMsg))
                if (intConteoOcurrencias > 0)
                {
                    clsDTOSenal objSenal = new clsDTOSenal();
                    objSenal = cSenal.mtdConsultarSenal(strOleConn, new clsDTOSenal(lstFormulas[0].StrIdSenal, string.Empty, string.Empty, string.Empty, false), ref strErrMsg);

                    cSenal.mtdInsertarNroRegistrosSA(strOleConn, intConteoOcurrencias, strNombreUsuario, "Señal de alerta", ref strErrMsg);
                    intConteoTblConteoRegistro = cSenal.mtdConteoRegistros(strOleConn, ref strErrMsg);

                    if (intConteoTblConteoRegistro == 0)
                        intConteoTblConteoRegistro = 1;

                    #region Recorrido para insertar el registro de operacion
                    foreach (object objInfoRegistro in lstInfoRegistro)
                    {
                        //string strInfo = (string)objInfoRegistro;
                        string[] strPartesInfo = ((string)objInfoRegistro).Split('|');

                        cSenal.mtdInsertarRegOperacion(strOleConn, intIdUsuario,
                            strPartesInfo[2].Trim(), strPartesInfo[0].Trim(),// Identificacion // NombreApellido                            
                            intConteoTblConteoRegistro, intConteoOcurrencias,// IdConteo, // Cant,
                            0, 0,// Valor, // Frecuencia,                            
                            string.Empty,// TipoCliente,
                            objSenal.StrCodigoSenal, objSenal.StrDescripcionSenal,// IdSenal,
                            strIndicadorROI,
                            ref strErrMsg);
                    }
                    #endregion
                }
            #endregion
        }
        #endregion

        /*
        * Metodos para el Historico de Inspektor
        */
        #region Historico Inspektor
        public List<clsDTOHistoricoCalculoPerfil> mtdConsultarHistPerfiles(string strOleConn, clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            clsDTOHistoricoCalculoPerfil objHist = new clsDTOHistoricoCalculoPerfil();
            List<clsDTOHistoricoCalculoPerfil> lstHist = new List<clsDTOHistoricoCalculoPerfil>();
            #endregion Vars

            dtInfo = cDtPerfil.mtdConsultaHistorico(strOleConn, objArchivo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objHist = new clsDTOHistoricoCalculoPerfil(
                            dr["IdHistorico"].ToString().Trim(),
                            dr["IdPerfil"].ToString().Trim(),
                            objArchivo.StrIdArchivo,
                            dr["TipoDocumento"].ToString().Trim(),
                            dr["NumeroDocumento"].ToString().Trim(),
                            dr["NombreCliente"].ToString().Trim(),
                            dr["Calificacion"].ToString().Trim(),
                            dr["Producto"].ToString().Trim(),
                            dr["InfoInspektor"].ToString().Trim(),
                            dr["SenalesAlerta"].ToString().Trim(),
                            dr["FechaGeneracion"].ToString().Trim(),
                            dr["Linea"].ToString().Trim());

                        lstHist.Add(objHist);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstHist = null;
                    strErrMsg = "No hay información de histórico de perfiles.";
                }
            }
            else
                lstHist = null;

            return lstHist;
        }

        public DataSet mtdConsultarRptHistInspektor(string strFechaInicial, string strFechaFinal, string strNroIdentificacion,
           ref string strErrMsg)
        {
            DataSet dsInfo = new DataSet();
            clsDtPerfil cDtPerfil = new clsDtPerfil();

            dsInfo = cDtPerfil.mtdConsultarRptHistInspektor(strFechaInicial, strFechaFinal, strNroIdentificacion, ref strErrMsg);

            return dsInfo;
        }

        public List<clsDTOHistoricoCalculoPerfil> mtdConsultarHistClientes(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            clsDTOHistoricoCalculoPerfil objHist = new clsDTOHistoricoCalculoPerfil();
            List<clsDTOHistoricoCalculoPerfil> lstHist = new List<clsDTOHistoricoCalculoPerfil>();
            #endregion Vars

            dtInfo = cDtPerfil.mtdConsultaHistClientes(strOleConn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objHist = new clsDTOHistoricoCalculoPerfil(
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            dr["TipoDocumento"].ToString().Trim(),
                            dr["NumeroDocumento"].ToString().Trim(),
                            dr["NombreCliente"].ToString().Trim(),
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty);

                        lstHist.Add(objHist);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstHist = null;
                    strErrMsg = "No hay información de histórico de perfiles.";
                }
            }
            else
                lstHist = null;
            return lstHist;
        }

        public void mtdInsHistClienteInfoInspektor( clsDTOHistoricoCalculoPerfil objHistorico, ref string strErrMsg)
        {
            #region Vars
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            #endregion Vars

            cDtPerfil.mtdInsHistClienteInfoInspektor(objHistorico, ref strErrMsg);
        }
        #endregion

        #region Señales Manuales
        public void mtdEjecutarSenales(List<clsDTOArchivo> lstArchivos, List<clsDTOSenalVariable> lstFormulas,
            int intIdUsuario, string strNombreUsuario,
            ref string strErrMsg)
        {
            #region Vars
            int intConteoOcurrencias = 0;
            string strIndicadorROI = string.Empty;
            DataTable dtInfoRegArchivo = new DataTable(), dtInfoCargue = new DataTable();
            clsArchivo cArchivo = new clsArchivo();
            clsParamArchivo cParam = new clsParamArchivo();
            clsSenal cSenal = new clsSenal();
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
            List<clsDTOEstructuraCampo> lstEstruc = new List<clsDTOEstructuraCampo>();
            List<clsDTOPerfil> lstPerfil = new List<clsDTOPerfil>();
            List<object> lstInfoRegistro = new List<object>();
            #endregion

            #region Recorrido de archivos
            #region Consulta de parametros evaluar las senales
            lstEstruc = cParam.mtdCargarInfoEstructura(objVariable, ref strErrMsg);
            lstPerfil = mtdCargarInfoPerfiles(ref strErrMsg);
            #endregion

            foreach (clsDTOArchivo objArchivo in lstArchivos)
            {
                dtInfoRegArchivo = cArchivo.mtdNumeroRegistrosArchivo(objArchivo, ref strErrMsg);

                if (dtInfoRegArchivo.Rows.Count > 0)
                {
                    #region Consulta de informacion a recorrer y poder evaluar las senales
                    dtInfoCargue = cArchivo.mtdConsultarInfoCargada(objArchivo, ref strErrMsg);
                    #endregion

                    #region Recorrido del archivo linea por linea
                    foreach (DataRow drLinea in dtInfoRegArchivo.Rows)
                    {
                        mtdEvaluarSenal(drLinea, dtInfoCargue, lstEstruc, lstPerfil, lstFormulas,
                            objArchivo, intIdUsuario, strNombreUsuario, ref lstInfoRegistro, ref intConteoOcurrencias,
                            ref strIndicadorROI, ref strErrMsg);

                        if (!string.IsNullOrEmpty(strErrMsg))
                            break;
                    }
                    #endregion
                }
            }
            #endregion

            #region Registro de operacion
            mtdRegistroOperacion(intConteoOcurrencias, lstFormulas, strNombreUsuario, intIdUsuario, lstInfoRegistro, strIndicadorROI, ref strErrMsg);
            #endregion
        }

        public void mtdEvaluarSenal(DataRow drLinea, DataTable dtInfoCargue,
            List<clsDTOEstructuraCampo> lstEstruc, List<clsDTOPerfil> lstPerfil,
            List<clsDTOSenalVariable> lstFormulas, clsDTOArchivo objArchivo, int intIdUsuario, string strNombreUsuario,
            ref List<object> lstInfoRegistro,
            ref int intConteoOcurrencias, ref string strIndicadorROI, ref string strErrMsg)
        {
            #region Vars
            bool booContinuar = true;
            decimal decSumPerfil = 0;
            string strPosNombreCliente = string.Empty, strPosNroID = string.Empty, strPosProducto = string.Empty, strPosTipoDoc = string.Empty,
                strTipoDoc = string.Empty, strNombreCliente = string.Empty, strNroID = string.Empty, strProducto = string.Empty,
                strFiltro = string.Empty, strTextoCliente = string.Empty, strSenalesAlertas = string.Empty;
            clsDTOParametrizacion objCategoria = new clsDTOParametrizacion();
            clsDTOVariable objVariable = new clsDTOVariable();
            clsDTOHistoricoCalculoPerfil objHist = new clsDTOHistoricoCalculoPerfil();
            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();
            clsDtPerfil cDtPerfil = new clsDtPerfil();
            List<clsDTOSenal> lstSenal = new List<clsDTOSenal>();
            List<clsDTOSenal> lstSenalTemp = new List<clsDTOSenal>();
            #endregion Vars

            #region Re-inicio vars
            mtdConsultarPosiciones(ref strPosNombreCliente, ref strPosNroID, ref strPosProducto, ref strPosTipoDoc);

            strNombreCliente = string.Empty;
            strNroID = string.Empty;
            strProducto = string.Empty;
            strTipoDoc = string.Empty;
            strFiltro = "[NumeroLinea]=" + drLinea["NumeroLinea"].ToString().Trim();
            #endregion Re-inicio vars

            #region Recorre todos los campos para realizar calculos
            foreach (clsDTOEstructuraCampo objEstruct in lstEstruc)
            {
                #region Valida si debe continuar
                if (!booContinuar)
                    break;
                #endregion

                #region Recorre la informacion del archivo cargado
                foreach (DataRow drInfo in dtInfoCargue.Select(strFiltro))
                {
                    if (drInfo["Posicion"].ToString().Trim() != objEstruct.StrPosicion)
                    {
                        #region Trae la informacion obligatoria del cliente
                        if (drInfo["Posicion"].ToString().Trim() == strPosNombreCliente)
                            strNombreCliente = drInfo["ValorCampoArchivo"].ToString().Trim();

                        if (drInfo["Posicion"].ToString().Trim() == strPosNroID)
                            strNroID = drInfo["ValorCampoArchivo"].ToString().Trim();

                        if (drInfo["Posicion"].ToString().Trim() == strPosProducto)
                            strProducto = drInfo["ValorCampoArchivo"].ToString().Trim();

                        if (drInfo["Posicion"].ToString().Trim() == strPosTipoDoc)
                            strTipoDoc = drInfo["ValorCampoArchivo"].ToString().Trim();
                        #endregion

                        continue;
                    }
                    else
                    {
                        #region Procesamiento de Calculos y Alertas
                        if (string.IsNullOrEmpty(drInfo["ValorCampoArchivo"].ToString().Trim()))
                            continue;
                        else
                        {
                            #region Realiza los calculos
                            #region Consulta Calificacion Categoria
                            //saca informacion de la categoria y la computa con la infor de la variable
                            objCategoria =
                                (cParam.mtdConsultarCalifParam(
                                    objEstruct.StrIdEstructCampo, drInfo["ValorCampoArchivo"].ToString().Trim(), ref strErrMsg)) == null ?
                                    null :
                                    (cParam.mtdConsultarCalifParam(objEstruct.StrIdEstructCampo, drInfo["ValorCampoArchivo"].ToString().Trim(), ref strErrMsg))[0];
                            #endregion

                            #region control de errores
                            if (objCategoria == null)
                            {
                                if (strErrMsg == "No hay información de categorías.")
                                {
                                    strErrMsg = string.Empty;
                                    continue;
                                }
                                else
                                {
                                    booContinuar = false;
                                    break;
                                }
                            }
                            #endregion

                            objVariable = cParam.mtdConsultaTipoParametro(objEstruct, ref strErrMsg);

                            #region control de errores
                            if (!string.IsNullOrEmpty(strErrMsg))
                            {
                                booContinuar = false;
                                break;
                            }
                            #endregion

                            decSumPerfil += ((Convert.ToDecimal(objCategoria.StrCalificacionCategoria) * Convert.ToDecimal(objVariable.StrCalificacion) / 100));
                            #endregion

                            mtdGenerarAlerta(drInfo, objEstruct, lstFormulas, intIdUsuario, ref lstSenalTemp, ref lstSenal, ref strIndicadorROI, ref strErrMsg);
                        }
                        #endregion

                        #region Revision de Lista de Señales de alerta
                        foreach (clsDTOSenal objTemp in lstSenal)
                        {
                            lstSenalTemp.RemoveAll(x => x.StrIdSenal == objTemp.StrIdSenal);
                        }
                        #endregion
                    }
                }
                #endregion
            }
            #endregion

            #region Lista Info Cliente del registro y Conteo
            if (lstSenal.Count > 0)
            {
                object objCliente = new Object();
                objCliente = strNombreCliente + "|" + strTipoDoc + "|" + strNroID;
                lstInfoRegistro.Add(objCliente);

                intConteoOcurrencias++;
            }
            #endregion

            #region Envio de Notificaciones
            strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ", strNombreCliente, strTipoDoc, strNroID);

            foreach (clsDTOSenal objSenal in lstSenal)
            {
                if (string.IsNullOrEmpty(strSenalesAlertas))
                    strSenalesAlertas = objSenal.StrCodigoSenal;
                else
                    strSenalesAlertas = strSenalesAlertas + " | " + objSenal.StrCodigoSenal;

                cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenal.StrCodigoSenal, objSenal.StrDescripcionSenal, intIdUsuario,
                    strTextoCliente, ref strErrMsg);
            }
            #endregion
        }

        /// <summary>
        /// Metodo para registrar operaciones desde las señales Manuales
        /// </summary>
        /// <param name="intConteoOcurrencias"></param>
        /// <param name="lstFormulas"></param>
        /// <param name="strNombreUsuario"></param>
        /// <param name="intIdUsuario"></param>
        /// <param name="lstInfoRegistro"></param>
        /// <param name="strErrMsg"></param>
        public void mtdRegistroOperacion(int intConteoOcurrencias, List<clsDTOSenalVariable> lstFormulas,
            string strNombreUsuario, int intIdUsuario, List<object> lstInfoRegistro, string strIndicadorROI, ref string strErrMsg)
        {
            int intConteoTblConteoRegistro = 0;
            clsSenal cSenal = new clsSenal();

            #region Registro de operacion
            if (string.IsNullOrEmpty(strErrMsg))
                if (intConteoOcurrencias > 0)
                {
                    clsDTOSenal objSenal = new clsDTOSenal();
                    objSenal = cSenal.mtdConsultarSenal(new clsDTOSenal(lstFormulas[0].StrIdSenal, string.Empty, string.Empty, string.Empty, false), ref strErrMsg);

                    cSenal.mtdInsertarNroRegistrosSA(intConteoOcurrencias, strNombreUsuario, "Señal de alerta", ref strErrMsg);
                    intConteoTblConteoRegistro = cSenal.mtdConteoRegistros(ref strErrMsg);

                    if (intConteoTblConteoRegistro == 0)
                        intConteoTblConteoRegistro = 1;

                    #region Recorrido para insertar el registro de operacion
                    foreach (object objInfoRegistro in lstInfoRegistro)
                    {
                        //string strInfo = (string)objInfoRegistro;
                        string[] strPartesInfo = ((string)objInfoRegistro).Split('|');

                        cSenal.mtdInsertarRegOperacion(intIdUsuario,
                            strPartesInfo[2].Trim(), strPartesInfo[0].Trim(),//Identificacion, NombreApellido
                            intConteoTblConteoRegistro, intConteoOcurrencias,// IdConteo,// Cant,                            
                            0, 0, // Valor, Frecuencia,                             
                            string.Empty,// TipoCliente,
                            objSenal.StrIdSenal, objSenal.StrDescripcionSenal, strIndicadorROI, ref strErrMsg
                            , objSenal.StrCodigoSenal);
                    }
                    #endregion
                }
            #endregion
        }

        /// <summary>
        /// Metodo para registrar operaciones desde las señales automaticas
        /// </summary>
        /// <param name="intConteoOcurrencias"></param>
        /// <param name="objSenal"></param>
        /// <param name="strNombreUsuario"></param>
        /// <param name="intIdUsuario"></param>
        /// <param name="lstInfoRegistro"></param>
        /// <param name="strErrMsg"></param>
        public void mtdRegistroOperacion(int intConteoOcurrencias, clsDTOSenal objSenal,
           string strNombreUsuario, int intIdUsuario, List<object> lstInfoRegistro, string strIndicadorROI, ref string strErrMsg)
        {
            int intConteoTblConteoRegistro = 0;
            clsSenal cSenal = new clsSenal();

            #region Registro de operacion
            if (!string.IsNullOrEmpty(strErrMsg))
                if (intConteoOcurrencias > 0)
                {
                    //clsDTOSenal objSenal = new clsDTOSenal();
                    //objSenal = cSenal.mtdConsultarSenal(new clsDTOSenal(lstFormulas[0].StrIdSenal, string.Empty, string.Empty, false), ref strErrMsg);

                    cSenal.mtdInsertarNroRegistrosSA(intConteoOcurrencias, strNombreUsuario, "Señal de alerta", ref strErrMsg);
                    intConteoTblConteoRegistro = cSenal.mtdConteoRegistros(ref strErrMsg);

                    if (intConteoTblConteoRegistro == 0)
                        intConteoTblConteoRegistro = 1;

                    #region Recorrido para insertar el registro de operacion
                    foreach (object objInfoRegistro in lstInfoRegistro)
                    {
                        //string strInfo = (string)objInfoRegistro;
                        string[] strPartesInfo = ((string)objInfoRegistro).Split('|');

                        cSenal.mtdInsertarRegOperacion(intIdUsuario,
                            strPartesInfo[2].Trim(), strPartesInfo[0].Trim(),// Identificacion // NombreApellido                            
                            intConteoTblConteoRegistro, intConteoOcurrencias,// IdConteo,// Cant,                            
                            0, 0,// Valor, // Frecuencia,                           
                            string.Empty,// TipoCliente,// IdSenal,
                            objSenal.StrIdSenal, objSenal.StrDescripcionSenal, strIndicadorROI,
                            ref strErrMsg, objSenal.StrCodigoSenal);
                    }
                    #endregion
                }
            #endregion
        }
        #endregion

        #region Señales Comparativas

        public List<ArchivoSegmentacion> ConsultarArchivos()
        {
            try
            {
                List<ArchivoSegmentacion> lst = new List<ArchivoSegmentacion>();
                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("Perfiles.SeleccionarArchivos", new List<SqlParameter>()))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new ArchivoSegmentacion()
                            {
                                IdArchivo = Convert.ToInt32(Row["IdArchivo"].ToString()),
                                FechaRegistro = Row["FechaRegistro"].ToString(),
                                UrlArchivo = Row["UrlArchivo"].ToString()
                            });
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CabeceroArchivo> ConsultarCabecero()
        {
            try
            {
                List<CabeceroArchivo> lst = new List<CabeceroArchivo>();
                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("Perfiles.SeleccionarEstructura", new List<SqlParameter>()))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new CabeceroArchivo()
                            {
                                Posicion = Convert.ToInt32(Row["Posicion"].ToString()),
                                Cabecero = Row["Cabecero"].ToString(),
                                //Estado = Convert.ToInt32(Row["Estado"])
                            });
                        }
                    }
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<InfoArchivoSegmentacion> ConsultarInfoArchivo(int idArchivo)
        {
            try
            {
                List<InfoArchivoSegmentacion> lst = new List<InfoArchivoSegmentacion>();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdArchivo", SqlDbType = SqlDbType.Int, Value =  idArchivo },
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Perfiles].[SeleccionarInfoArchivo]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        lst.Add(new InfoArchivoSegmentacion()
                        {
                            IdInfoArchivo = Convert.ToInt32(Row["IdInfoArchivo"].ToString()),
                            IdArchivo = Convert.ToInt32(Row["IdArchivo"].ToString()),
                            ValorCampoArchivo = Row["ValorCampoArchivo"].ToString(),
                            Posicion = Convert.ToInt32(Row["Posicion"].ToString()),
                            NumeroLinea = Convert.ToInt32(Row["NumeroLinea"].ToString()),
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComparacionArchivo> ComparacionArchivo(ComparacionArchivo comparacion)
        {
            try
            {
                List<ComparacionArchivo> lst = new List<ComparacionArchivo>();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@vIdArchivo", SqlDbType = SqlDbType.Int, Value =  comparacion.IdArchivo1 },
                    new SqlParameter() { ParameterName = "@vIdArchivo2", SqlDbType = SqlDbType.Int, Value =  comparacion.IdArchivo2 },
                    new SqlParameter() { ParameterName = "@vCampoEvaluar", SqlDbType = SqlDbType.Int, Value =  comparacion.IdCampoEvaluar1 },
                    new SqlParameter() { ParameterName = "@vCampoEvaluar2", SqlDbType = SqlDbType.Int, Value =  comparacion.IdCampoEvaluar2 },
                    new SqlParameter() { ParameterName = "@Operador", SqlDbType = SqlDbType.VarChar, Value =  comparacion.Operador },
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Perfiles].[CompararArchivo]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        lst.Add(new ComparacionArchivo()
                        {
                            Filtro = Row["Filtro"].ToString(),
                            Nombre = Row["Nombre1"].ToString(),
                            Documento = Row["Cedula1"].ToString(),
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RegistroOperacion(List<ComparacionArchivo> lstComparacion)
        {
            try
            {
                int result = 0;
                foreach (var comparacion in lstComparacion)
                {
                    List<SqlParameter> parametros = new List<SqlParameter>()
                    {
                        new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value =  comparacion.IdUsuario },
                        new SqlParameter() { ParameterName = "@Identificacion", SqlDbType = SqlDbType.VarChar, Value =  comparacion.Documento },
                        new SqlParameter() { ParameterName = "@NombreApellido", SqlDbType = SqlDbType.VarChar, Value =  comparacion.Nombre },
                        new SqlParameter() { ParameterName = "@Indicador", SqlDbType = SqlDbType.NChar, Value =  comparacion.Filtro.Replace("LIKE","=") },
                        new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Value =  ParameterDirection.Output },
                    };
                    result = cDataBase.EjecutarSPParametrosReturnInteger("[Proceso].[InsertarRegistroOperacion]", parametros);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}