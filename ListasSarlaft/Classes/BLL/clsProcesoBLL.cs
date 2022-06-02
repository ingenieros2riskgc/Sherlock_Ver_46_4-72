using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsProcesoBLL
    {
        /// <summary>
        /// Realiza la consulta de los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsProceso> mtdConsultarProceso(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsDtProceso cDtProceso = new clsDtProceso();
            clsProceso objProceso = new clsProceso();
            #endregion Vars

            dtInfo = cDtProceso.mtdConsultarProceso(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProceso = new clsProceso(
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                            Convert.ToInt32(dr["IdEmpresa"].ToString().Trim()),
                            dr["NombreProceso"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Objetivo"].ToString().Trim(),
                            dr["IdArea"].ToString().Trim(),
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            dr["CargoResponsable"].ToString().Trim(),
                            Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                            dr["NombreMacroProceso"].ToString().Trim(),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["Usuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["ContextoInterno"].ToString().Trim(),
                            dr["ContextoExterno"].ToString().Trim());
                        lstProceso.Add(objProceso);
                    }
                }
                else
                {
                    lstProceso = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
            }
            else
                lstProceso = null;

            return lstProceso;
        }

        public DataTable ConsultarProcesos(int idMacroProceso)
        {
            try
            {
                clsDtProceso cDtProceso = new clsDtProceso();
                DataTable dt = cDtProceso.ConsultarProcesos(idMacroProceso);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsProceso> mtdConsultarProceso(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsDtProceso cDtProceso = new clsDtProceso();
            clsProceso objProceso = new clsProceso();
            #endregion Vars

            dtInfo = cDtProceso.mtdConsultarProceso(booEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProceso = new clsProceso(
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdEmpresa"].ToString().Trim()),
                             dr["NombreProceso"].ToString().Trim(),
                             dr["Descripcion"].ToString().Trim(),
                             dr["Objetivo"].ToString().Trim(),
                             dr["IdArea"].ToString().Trim(),
                             Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                             dr["CargoResponsable"].ToString().Trim(),
                             Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                             dr["NombreMacroProceso"].ToString().Trim(),
                             Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                             dr["Estado"].ToString().Trim() == "True" ? true : false,
                             Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                             dr["Usuario"].ToString().Trim(),
                             dr["FechaRegistro"].ToString().Trim());

                        lstProceso.Add(objProceso);
                    }
                }
                else
                {
                    lstProceso = null;
                    strErrMsg = "No hay información de Procesos.";
                }
            }
            else
                lstProceso = null;

            return lstProceso;
        }

        /// <summary>
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsProceso> mtdConsultarProceso(bool booEstado, clsMacroproceso objMacroproceso, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsDtProceso cDtProceso = new clsDtProceso();
            clsProceso objProceso = new clsProceso();
            #endregion Vars

            dtInfo = cDtProceso.mtdConsultarProceso(booEstado, objMacroproceso, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProceso = new clsProceso(
                            Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdEmpresa"].ToString().Trim()),
                             dr["NombreProceso"].ToString().Trim(),
                             dr["Descripcion"].ToString().Trim(),
                             dr["Objetivo"].ToString().Trim(),
                             dr["IdArea"].ToString().Trim(),
                             Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                             dr["CargoResponsable"].ToString().Trim(),
                             Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                             dr["NombreMacroProceso"].ToString().Trim(),
                             Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                             dr["Estado"].ToString().Trim() == "True" ? true : false,
                             Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                             dr["Usuario"].ToString().Trim(),
                             dr["FechaRegistro"].ToString().Trim());

                        lstProceso.Add(objProceso);
                    }
                }
                else
                {
                    lstProceso = null;
                    strErrMsg = "No hay información de Procesos.";
                }
            }
            else
                lstProceso = null;

            return lstProceso;
        }

        /// <summary>
        /// Realiza la consulta de los Procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public clsProceso mtdConsultarProceso(clsProceso ObjProIN, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsDtProceso cDtProceso = new clsDtProceso();
            clsProceso objProceso = new clsProceso();
            #endregion Vars

            dtInfo = cDtProceso.mtdConsultarProceso(ObjProIN, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProceso = new clsProceso(
                             Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                             Convert.ToInt32(dr["IdEmpresa"].ToString().Trim()),
                             dr["NombreProceso"].ToString().Trim(),
                             dr["Descripcion"].ToString().Trim(),
                             dr["Objetivo"].ToString().Trim(),
                             dr["IdArea"].ToString().Trim(),
                             Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                             dr["CargoResponsable"].ToString().Trim(),
                             Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                             dr["NombreMacroProceso"].ToString().Trim(),
                             Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                             dr["NombreCadenaValor"].ToString().Trim(),
                             dr["Estado"].ToString().Trim() == "True" ? true : false,
                             Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                             dr["Usuario"].ToString().Trim(),
                             dr["FechaRegistro"].ToString().Trim());
                    }
                }
                else
                    objProceso = null;
            }
            else
                objProceso = null;

            return objProceso;
        }
                
        /// <summary>
        /// Realiza la insercion de los campos de proceso
        /// </summary>
        /// <param name="objProceso">Informacion de proceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarProceso(clsProceso objProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtProceso cDtProceso = new clsDtProceso();

            booResult = cDtProceso.mtdInsertarProceso(objProceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objProceso">Informacion de Proceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarProceso(clsProceso objProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtProceso cDtProceso = new clsDtProceso();

            booResult = cDtProceso.mtdActualizarProceso(objProceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objProceso">Informacion de Proceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsProceso objProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtProceso cDtProceso = new clsDtProceso();

            booResult = cDtProceso.mtdActualizarEstado(objProceso, ref strErrMsg);

            return booResult;
        }
    }
}