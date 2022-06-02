using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsSubprocesoBLL
    {
        /// <summary>
        /// Realiza la consulta de los Subprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsSubproceso> mtdConsultarSubProceso(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsDtSubproceso cDtSubproceso = new clsDtSubproceso();
            clsSubproceso objSubproceso = new clsSubproceso();
            #endregion Vars

            dtInfo = cDtSubproceso.mtdConsultarSubproceso(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//
                        objSubproceso = new clsSubproceso(
                            Convert.ToInt32(dr["IdSubproceso"].ToString().Trim()),
                            dr["NombreSubproceso"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Objetivo"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            dr["CargoResponsable"].ToString().Trim(),
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                            dr["NombreProceso"].ToString().Trim(),
                            Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstSubproceso.Add(objSubproceso);
                    }
                }
                else
                {
                    lstSubproceso = null;
                    //strErrMsg = "No hay información de Subprocesos.";
                }
            }
            else
                lstSubproceso = null;

            return lstSubproceso;
        }

        public DataTable ConsultarSubprocesos(int? idProceso)
        {
            try
            {
                clsDtSubproceso cDtSubProceso = new clsDtSubproceso();
                DataTable dt = cDtSubProceso.ConsultarSubprocesos(idProceso);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza la consulta de los Subprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public clsSubproceso mtdConsultarSubProceso(clsSubproceso objSubpIN, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsDtSubproceso cDtSubproceso = new clsDtSubproceso();
            clsSubproceso objSubproceso = new clsSubproceso();
            #endregion Vars

            dtInfo = cDtSubproceso.mtdConsultarSubproceso(objSubpIN, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//
                        objSubproceso = new clsSubproceso(
                            Convert.ToInt32(dr["IdSubproceso"].ToString().Trim()),
                            dr["NombreSubproceso"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Objetivo"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            dr["CargoResponsable"].ToString().Trim(),
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                            dr["NombreProceso"].ToString().Trim(),
                            Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                            dr["NombreMacroProceso"].ToString().Trim(),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            dr["NombreCadenaValor"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstSubproceso.Add(objSubproceso);
                    }
                }
                else
                {
                    objSubproceso = null;
                    //strErrMsg = "No hay información de Subprocesos.";
                }
            }
            else
                objSubproceso = null;

            return objSubproceso;
        }

        /// <summary>
        /// Realiza la consulta de los Subprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsSubproceso> mtdConsultarSubProceso(bool booEstado, clsProceso objProceso, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsDtSubproceso cDtSubproceso = new clsDtSubproceso();
            clsSubproceso objSubproceso = new clsSubproceso();
            #endregion Vars

            dtInfo = cDtSubproceso.mtdConsultarSubproceso(booEstado, objProceso, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSubproceso = new clsSubproceso(
                            Convert.ToInt32(dr["IdSubproceso"].ToString().Trim()),
                            dr["NombreSubproceso"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Objetivo"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            dr["CargoResponsable"].ToString().Trim(),
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                            dr["NombreProceso"].ToString().Trim(),
                            Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());

                        lstSubproceso.Add(objSubproceso);
                    }
                }
                else
                {
                    lstSubproceso = null;
                    strErrMsg = "No hay información de Subprocesos.";
                }
            }
            else
                lstSubproceso = null;

            return lstSubproceso;
        }

        /// <summary>
        /// Realiza la insercion de los campos de Subproceso
        /// </summary>
        /// <param name="objSubProceso">Informacion de Subproceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarSubproceso(clsSubproceso objSubProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtSubproceso cDtSubproceso = new clsDtSubproceso();

            booResult = cDtSubproceso.mtdInsertarSubproceso(objSubProceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objSubproceso">Informacion de Subproceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarSubproceso(clsSubproceso objSubProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtSubproceso cDtSubproceso = new clsDtSubproceso();

            booResult = cDtSubproceso.mtdActualizarSubproceso(objSubProceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objSubproceso">Informacion de Proceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsSubproceso objSubproceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtSubproceso cDtSubproceso = new clsDtSubproceso();

            booResult = cDtSubproceso.mtdActualizarEstado(objSubproceso, ref strErrMsg);

            return booResult;
        }
    }
}