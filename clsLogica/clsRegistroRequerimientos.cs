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
    public class clsRegistroRequerimientos
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
        public List<clsDTORegistroEvidencias> mtdCargarEvidencias(ref string strErrMsg, string strId)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtRegistroRequerimientos cDtRegReq = new clsDtRegistroRequerimientos();
            clsDTORegistroEvidencias objRegEvi = new clsDTORegistroEvidencias();
            List<clsDTORegistroEvidencias> lstRegistroEvidencias = new List<clsDTORegistroEvidencias>();
            #endregion Vars

            dtInfo = cDtRegReq.mtdConsultaEvidencias(ref strErrMsg, strId);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRegEvi = new clsDTORegistroEvidencias(
                            dr["IdEvidencia"].ToString().Trim(),
                            dr["URLArchivo"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim()
                            );

                        lstRegistroEvidencias.Add(objRegEvi);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstRegistroEvidencias = null;
                    strErrMsg = "No hay información sobre registros.";
                }
            }
            else
                lstRegistroEvidencias = null;
            return lstRegistroEvidencias;
        }

        public void mtdInsertarRequerimientos(clsDTORegistroRequerimientos objRegReq, ref string strErrMsg)
        {
            clsDtRegistroRequerimientos cDtRegReq = new clsDtRegistroRequerimientos();

            cDtRegReq.mtdInsertarRequerimientos(objRegReq, ref strErrMsg);
        }
        public void mtdInsertarControlRequerimientos(clsDTOGestionRequerimientos objGesReq, ref string strErrMsg)
        {
            clsDtRegistroRequerimientos cDtGesReq = new clsDtRegistroRequerimientos();

            cDtGesReq.mtdInsertarControlRequerimientos(objGesReq, ref strErrMsg);
        }

        public void mtdActualizarControlRequerimientos(string strNumeroGES, clsDTOActualizarGestionRequerimientos objGesReq, ref string strErrMsg)
        {
            clsDtRegistroRequerimientos cDtGesReq = new clsDtRegistroRequerimientos();

            cDtGesReq.mtdActualizarControlRequerimientos(strNumeroGES, objGesReq, ref strErrMsg);
        }


        public List<clsDTOReporteRequerimientos> mtdCargarReporte(string strEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtRegistroRequerimientos cDtRegReq = new clsDtRegistroRequerimientos();
            clsDTOReporteRequerimientos objRegEvi = new clsDTOReporteRequerimientos();
            List<clsDTOReporteRequerimientos> lstRegistroEvidencias = new List<clsDTOReporteRequerimientos>();
            #endregion Vars

            dtInfo = cDtRegReq.mtdConsultaReporte(strEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRegEvi = new clsDTOReporteRequerimientos(
                            dr["idGESREQ"].ToString().Trim(),
                            dr["Empresa"].ToString().Trim(),
                            dr["Usuario"].ToString().Trim(),
                            dr["NumeroREQ"].ToString().Trim(),
                            dr["FechaCreacionGESREQ"].ToString().Trim(),
                            dr["TipoFalla"].ToString().Trim(),
                            dr["GrupoAsignado"].ToString().Trim(),
                            dr["Encargado"].ToString().Trim(),
                            dr["Estado"].ToString().Trim(),
                            dr["Criticidad"].ToString().Trim(),
                            dr["FechaVencimientoGESREQ"].ToString().Trim()
                            );

                        lstRegistroEvidencias.Add(objRegEvi);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstRegistroEvidencias = null;
                    strErrMsg = "No hay información sobre registros.";
                }
            }
            else
                lstRegistroEvidencias = null;
            return lstRegistroEvidencias;
        }

        public List<clsDTOReporteRequerimientos> mtdCargarReportes(string NomIntegrante, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtRegistroRequerimientos cDtRegReq = new clsDtRegistroRequerimientos();
            clsDTOReporteRequerimientos objRegEvi = new clsDTOReporteRequerimientos();
            List<clsDTOReporteRequerimientos> lstRegistroEvidencias = new List<clsDTOReporteRequerimientos>();
            #endregion Vars

            dtInfo = cDtRegReq.mtdConsultaReportes(NomIntegrante, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRegEvi = new clsDTOReporteRequerimientos(
                            dr["idGESREQ"].ToString().Trim(),
                            dr["Empresa"].ToString().Trim(),
                            dr["Usuario"].ToString().Trim(),
                            dr["NumeroREQ"].ToString().Trim(),
                            dr["FechaCreacionGESREQ"].ToString().Trim(),
                            dr["TipoFalla"].ToString().Trim(),
                            dr["GrupoAsignado"].ToString().Trim(),
                            dr["Encargado"].ToString().Trim(),
                            dr["Estado"].ToString().Trim(),
                            dr["Criticidad"].ToString().Trim(),
                            dr["FechaVencimientoGESREQ"].ToString().Trim()
                            );

                        lstRegistroEvidencias.Add(objRegEvi);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstRegistroEvidencias = null;
                    strErrMsg = "No hay información sobre registros.";
                }
            }
            else
                lstRegistroEvidencias = null;
            return lstRegistroEvidencias;
        }

        public List<clsDTORegistroRequerimientos> mtdCargarDatos(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtRegistroRequerimientos cDtRegReq = new clsDtRegistroRequerimientos();
            clsDTORegistroRequerimientos objRegEvi = new clsDTORegistroRequerimientos();
            List<clsDTORegistroRequerimientos> lstRegistroEvidencias = new List<clsDTORegistroRequerimientos>();
            #endregion Vars

            dtInfo = cDtRegReq.mtdConsultaDatos(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRegEvi = new clsDTORegistroRequerimientos(
                            dr["idREGREQ"].ToString().Trim(),
                            dr["Usuario"].ToString().Trim(),
                            dr["Empresa"].ToString().Trim(),
                            dr["NumeroREQ"].ToString().Trim(),
                            dr["FechaCreacionREGREQ"].ToString().Trim(),
                            dr["TipoFalla"].ToString().Trim(),
                            dr["DetalleTipoFalla"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Ruta"].ToString().Trim()
                            );

                        lstRegistroEvidencias.Add(objRegEvi);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstRegistroEvidencias = null;
                    strErrMsg = "No hay información sobre registros.";
                }
            }
            else
                lstRegistroEvidencias = null;
            return lstRegistroEvidencias;
        }

        public List<clsDTOActualizarGestionRequerimientos> mtdCargarGestion(string strNumeroREQ, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtRegistroRequerimientos cDtRegReq = new clsDtRegistroRequerimientos();
            clsDTOActualizarGestionRequerimientos objRegEvi = new clsDTOActualizarGestionRequerimientos();
            List<clsDTOActualizarGestionRequerimientos> lstRegistroEvidencias = new List<clsDTOActualizarGestionRequerimientos>();
            #endregion Vars

            dtInfo = cDtRegReq.mtdConsultaGestion(strNumeroREQ, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRegEvi = new clsDTOActualizarGestionRequerimientos(
                            dr["idGESREQ"].ToString().Trim(),
                            dr["NumeroREQ"].ToString().Trim(),
                            dr["GrupoAsignado"].ToString().Trim(),
                            dr["Encargado"].ToString().Trim(),
                            dr["Estado"].ToString().Trim(),
                            dr["Criticidad"].ToString().Trim(),
                            dr["FechaVencimientoGESREQ"].ToString().Trim(),
                            dr["Comentario"].ToString().Trim()
                            );

                        lstRegistroEvidencias.Add(objRegEvi);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstRegistroEvidencias = null;
                    strErrMsg = "No hay información sobre registros.";
                }
            }
            else
                lstRegistroEvidencias = null;
            return lstRegistroEvidencias;
        }

        public DataTable mtdCargarComentarios(ref string strErrMsg, string IdComentarios)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtRegistroRequerimientos cDtRegReq = new clsDtRegistroRequerimientos();
            clsDTORegistroComentarios objRegCom = new clsDTORegistroComentarios();
            List<clsDTORegistroComentarios> lstRegistroCom = new List<clsDTORegistroComentarios>();
            #endregion Vars

            dtInfo = cDtRegReq.mtdConsultaComentarios(ref strErrMsg, IdComentarios);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objRegCom = new clsDTORegistroComentarios(
                            dr["IdComentario"].ToString().Trim(),
                            dr["URLArchivo"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim()
                            );

                        lstRegistroCom.Add(objRegCom);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstRegistroCom = null;
                    strErrMsg = "No hay información sobre registros.";
                }
            }
            else
                lstRegistroCom = null;
            return dtInfo;
        }


    }
}