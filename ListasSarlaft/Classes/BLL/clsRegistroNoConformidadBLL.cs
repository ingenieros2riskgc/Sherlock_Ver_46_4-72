using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsRegistroNoConformidadBLL
    {
        /// <summary>
        /// Metodo para consultar y visualizar las Auditorias por el MacroProceso
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsAuditoriaNoConformidad> mtdConsultarAuditoriaMacroproceso(ref List<clsAuditoriaNoConformidad> lstAuditoria, ref string strErrMsg, ref int IdMacroProceso)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtRegistroNoConformidad cDtRegistro = new clsDtRegistroNoConformidad();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarAuditoriaMacroproceso(ref dtInfo, ref strErrMsg, ref IdMacroProceso);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsAuditoriaNoConformidad objControl = new clsAuditoriaNoConformidad();
                            objControl.intIdAuditoria = Convert.ToInt32(dr["IdAuditoria"].ToString().Trim());
                            objControl.strTema = dr["Tema"].ToString().Trim();
                            objControl.strEstandar = dr["Nombre"].ToString().Trim();

                            lstAuditoria.Add(objControl);
                        }
                    }
                    else
                        lstAuditoria = null;
                }
                else
                    lstAuditoria = null;
            }

            return lstAuditoria;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Auditorias por el Proceso
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsAuditoriaNoConformidad> mtdConsultarAuditoriaProceso(ref List<clsAuditoriaNoConformidad> lstAuditoria, ref string strErrMsg, ref int Proceso)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtRegistroNoConformidad cDtRegistro = new clsDtRegistroNoConformidad();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarAuditoriaProceso(ref dtInfo, ref strErrMsg, ref Proceso);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsAuditoriaNoConformidad objControl = new clsAuditoriaNoConformidad();
                            objControl.intIdAuditoria = Convert.ToInt32(dr["IdAuditoria"].ToString().Trim());
                            objControl.strTema = dr["Tema"].ToString().Trim();
                            objControl.strEstandar = dr["Nombre"].ToString().Trim();

                            lstAuditoria.Add(objControl);
                        }
                    }
                    else
                        lstAuditoria = null;
                }
                else
                    lstAuditoria = null;
            }

            return lstAuditoria;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarNoConformidad(clsControlNoConformidad cCrlNo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtRegistroNoConformidad cDtCrlNo = new clsDtRegistroNoConformidad();

            booResult = cDtCrlNo.mtdInsertarNoConformidad(cCrlNo, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarNoConformidadProceso(int IdNoconformidad, int IdProceso, int TipoProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtRegistroNoConformidad cDtCrlNo = new clsDtRegistroNoConformidad();

            booResult = cDtCrlNo.mtdInsertarNoConformidadProceso(IdNoconformidad, IdProceso, TipoProceso, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de competencia
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdNoConformidad(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtRegistroNoConformidad cDtCrlNo = new clsDtRegistroNoConformidad();
            DataTable dt = cDtCrlNo.mtdLastIdNoConformidad(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las No Conformidades
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsControlNoConformidad> mtdConsultarControlNoConformidad(ref List<clsControlNoConformidad> lstCrlConformidad, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtRegistroNoConformidad cDtRegistro = new clsDtRegistroNoConformidad();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarControlNoConformidad(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsControlNoConformidad objControl = new clsControlNoConformidad();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objControl.intIdMacroProceso = Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim());
                            objControl.strProceso = dr["Nombre"].ToString().Trim();
                            objControl.strDescripcion = dr["Descripcion"].ToString().Trim();
                            objControl.dtFechaInicio = dr["FechaInicio"].ToString().Trim();
                            objControl.strSeguimiento = dr["Seguimiento"].ToString().Trim();
                            //if(dr["FechaFinal"].ToString().Trim() != "")
                            objControl.dtFechaFinal = dr["FechaFinal"].ToString().Trim();
                            objControl.intResponsable = Convert.ToInt32(dr["CargoResponsable"].ToString().Trim());
                            objControl.strCargoResponsable = dr["NombreHijo"].ToString().Trim();
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objControl.strUsuario = dr["Usuario"].ToString().Trim();
                            objControl.strPathFile = dr["PathFile"].ToString().Trim();

                            lstCrlConformidad.Add(objControl);
                        }
                    }
                    else
                        lstCrlConformidad = null;
                }
                else
                    lstCrlConformidad = null;
            }

            return lstCrlConformidad;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las No Conformidades
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsControlNoConformidad> mtdConsultarControlNoConformidadSinCierre(ref List<clsControlNoConformidad> lstCrlConformidad, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtRegistroNoConformidad cDtRegistro = new clsDtRegistroNoConformidad();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarControlNoConformidadSinCierre(ref dtInfo, ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsControlNoConformidad objControl = new clsControlNoConformidad();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objControl.intIdMacroProceso = Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim());
                            objControl.strProceso = dr["Nombre"].ToString().Trim();
                            objControl.strDescripcion = dr["Descripcion"].ToString().Trim();
                            objControl.dtFechaInicio = dr["FechaInicio"].ToString().Trim();
                            objControl.strSeguimiento = dr["Seguimiento"].ToString().Trim();
                            //if (dr["FechaFinal"].ToString().Trim() != "")
                            objControl.dtFechaFinal = dr["FechaFinal"].ToString().Trim();
                            objControl.intResponsable = Convert.ToInt32(dr["CargoResponsable"].ToString().Trim());
                            objControl.strCargoResponsable = dr["NombreHijo"].ToString().Trim();
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objControl.strUsuario = dr["Usuario"].ToString().Trim();
                            objControl.strPathFile = dr["PathFile"].ToString().Trim();

                            lstCrlConformidad.Add(objControl);
                        }
                    }
                    else
                        lstCrlConformidad = null;
                }
                else
                    lstCrlConformidad = null;
            }

            return lstCrlConformidad;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la no corformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdControlNoConformidad(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtRegistroNoConformidad cDtControl = new clsDtRegistroNoConformidad();
            DataTable dt = cDtControl.mtdLastIdControlNoConformidad(ref strErrMsg);
            if (LastId != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    LastId = Convert.ToInt32(dr["LastId"].ToString());
                }
            }
            return LastId;
        }

        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarAuditoriaConformidad(ref string strErrMsg, ref int IdAuditoria, ref int IdControl)
        {
            bool booResult = false;
            clsDtRegistroNoConformidad cDtCrlNo = new clsDtRegistroNoConformidad();

            booResult = cDtCrlNo.mtdInsertarAuditoriaConformidad(ref strErrMsg, ref IdAuditoria, ref IdControl);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Auditorias por el MacroProceso
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsAuditoriaNoConformidad> mtdConsultarControlNoConformidadAud(ref List<clsAuditoriaNoConformidad> lstAuditoria, ref string strErrMsg, ref int IdControl)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtRegistroNoConformidad cDtRegistro = new clsDtRegistroNoConformidad();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarControlNoConformidadAud(ref dtInfo, ref strErrMsg, ref IdControl);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsAuditoriaNoConformidad objControl = new clsAuditoriaNoConformidad();
                            objControl.intIdAuditoria = Convert.ToInt32(dr["IdAuditoria"].ToString().Trim());
                            objControl.strTema = dr["Tema"].ToString().Trim();
                            objControl.strEstandar = dr["Nombre"].ToString().Trim();

                            lstAuditoria.Add(objControl);
                        }
                    }
                    else
                        lstAuditoria = null;
                }
                else
                    lstAuditoria = null;
            }

            return lstAuditoria;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdModificarNoConformidadSinArchivo(clsControlNoConformidad cCrlNo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtRegistroNoConformidad cDtCrlNo = new clsDtRegistroNoConformidad();

            booResult = cDtCrlNo.mtdModificarNoConformidadSinArchivo(cCrlNo, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdModificarNoConformidadConArchivo(clsControlNoConformidad cCrlNo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtRegistroNoConformidad cDtCrlNo = new clsDtRegistroNoConformidad();

            booResult = cDtCrlNo.mtdModificarNoConformidadConArchivo(cCrlNo, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la no corformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int[] mtdIdPlaneacion(ref string strErrMsg, ref int IdAuditoria)
        {
            int[] data = new int[2];
            clsDtRegistroNoConformidad cDtControl = new clsDtRegistroNoConformidad();
            DataTable dt = cDtControl.mtdIdPlaneacion(ref strErrMsg, ref IdAuditoria);
            foreach (DataRow dr in dt.Rows)
            {
                data[0] = Convert.ToInt32(dr["IdPlaneacion"].ToString());
                data[1] = Convert.ToInt32(dr["IdEstandar"].ToString());
            }
            return data;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la no corformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string mtdPlaneacion(ref string strErrMsg, ref int IdPlaneacion)
        {
            string data = string.Empty;
            clsDtRegistroNoConformidad cDtControl = new clsDtRegistroNoConformidad();
            DataTable dt = cDtControl.mtdPlaneacion(ref strErrMsg, ref IdPlaneacion);
            foreach (DataRow dr in dt.Rows)
            {
                data = dr["Nombre"].ToString();
            }
            return data;
        }
    }
}
