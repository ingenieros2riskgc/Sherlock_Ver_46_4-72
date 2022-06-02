using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsMacroProcesoBLL
    {
        /// <summary>
        /// Realiza la consulta de los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsMacroproceso> mtdConsultarMacroproceso(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsMacroproceso> lstMacroProceso = new List<clsMacroproceso>();
            clsDtMacroproceso cDtMacroproceso = new clsDtMacroproceso();
            clsMacroproceso objMacroproceso = new clsMacroproceso();
            #endregion Vars

            dtInfo = cDtMacroproceso.mtdConsultarMacroproceso(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objMacroproceso = new clsMacroproceso(
                            Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                            dr["Nombre"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Objetivo"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["Usuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["NombreCadenaValor"].ToString().Trim(),
                            dr["CargoResponsable"].ToString().Trim());

                        lstMacroProceso.Add(objMacroproceso);
                    }
                }
                else
                {
                    lstMacroProceso = null;
                    //strErrMsg = "No hay información de Macroprocesos.";
                }
            }
            else
                lstMacroProceso = null;

            return lstMacroProceso;
        }

        /// <summary>
        /// Realiza la consulta de los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsMacroproceso> mtdConsultarMacroproceso(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsMacroproceso> lstMacroProceso = new List<clsMacroproceso>();
            clsDtMacroproceso cDtMacroproceso = new clsDtMacroproceso();
            clsMacroproceso objMacroproceso = new clsMacroproceso();
            #endregion Vars

            dtInfo = cDtMacroproceso.mtdConsultarMacroproceso(booEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objMacroproceso = new clsMacroproceso(
                            Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                            dr["Nombre"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Objetivo"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["Usuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["NombreCadenaValor"].ToString().Trim(),
                            dr["CargoResponsable"].ToString().Trim());

                        lstMacroProceso.Add(objMacroproceso);
                    }
                }
                else
                {
                    lstMacroProceso = null;
                    strErrMsg = "No hay información de Macroprocesos.";
                }
            }
            else
                lstMacroProceso = null;

            return lstMacroProceso;
        }

        /// <summary>
        /// Realiza la consulta de los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsMacroproceso> mtdConsultarMacroproceso(bool booEstado, clsCadenaValor objCadenaValor, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsMacroproceso> lstMacroProceso = new List<clsMacroproceso>();
            clsDtMacroproceso cDtMacroproceso = new clsDtMacroproceso();
            clsMacroproceso objMacroproceso = new clsMacroproceso();
            #endregion Vars


            dtInfo = cDtMacroproceso.mtdConsultarMacroproceso(booEstado, objCadenaValor, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objMacroproceso = new clsMacroproceso(
                            Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                            dr["Nombre"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Objetivo"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["Usuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["NombreCadenaValor"].ToString().Trim(),
                            dr["CargoResponsable"].ToString().Trim());

                        lstMacroProceso.Add(objMacroproceso);
                    }
                }
                else
                {
                    lstMacroProceso = null;
                    strErrMsg = "No hay información de Macroprocesos.";
                }
            }
            else
                lstMacroProceso = null;

            return lstMacroProceso;
        }

        public DataTable ConsultarMacroProcesos(int idCadenaValor)
        {
            try
            {
                clsDtMacroproceso cDtMacroproceso = new clsDtMacroproceso();
                DataTable dt = cDtMacroproceso.ConsultarMacroProcesos(idCadenaValor);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza la consulta de los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public clsMacroproceso mtdConsultarMacroproceso(clsMacroproceso objMPIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtMacroproceso cDtMacroproceso = new clsDtMacroproceso();
            clsMacroproceso objMacroproceso = new clsMacroproceso();
            #endregion Vars

            dtInfo = cDtMacroproceso.mtdConsultarMacroproceso(objMPIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objMacroproceso = new clsMacroproceso(
                            Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                            dr["Nombre"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Objetivo"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                            Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["Usuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["NombreCadenaValor"].ToString().Trim(),
                            dr["CargoResponsable"].ToString().Trim());
                    }
                }
                else
                    objMacroproceso = null;
            }
            else
                objMacroproceso = null;

            return objMacroproceso;
        }

        /// <summary>
        /// Realiza la consulta de los macroprocesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarMacroproceso(bool booEstado, clsCadenaValor objCadenaValor, 
            ref List<clsMacroproceso> lstMacroProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtMacroproceso cDtMacroproceso = new clsDtMacroproceso();
            clsMacroproceso objMacroproceso = new clsMacroproceso();
            #endregion Vars

            booResult = cDtMacroproceso.mtdConsultarMacroproceso(booEstado, objCadenaValor, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            objMacroproceso = new clsMacroproceso(
                                Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                                dr["Nombre"].ToString().Trim(),
                                dr["Descripcion"].ToString().Trim(),
                                dr["Objetivo"].ToString().Trim(),
                                dr["Estado"].ToString().Trim() == "True" ? true : false,
                                Convert.ToInt32(dr["IdCargoResponsable"].ToString().Trim()),
                                Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim()),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["Usuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim(),
                                dr["NombreCadenaValor"].ToString().Trim(),
                                dr["CargoResponsable"].ToString().Trim());

                            lstMacroProceso.Add(objMacroproceso);
                        }
                    }
                    else
                    {
                        lstMacroProceso = null;
                        strErrMsg = "No hay información de Macroprocesos.";
                    }
                }
                else
                    lstMacroProceso = null;
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la insercion de los campos de macroproceso
        /// </summary>
        /// <param name="objMacroproceso">Informacion de macroproceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarMacroproceso(clsMacroproceso objMacroproceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtMacroproceso cDtMacroproceso = new clsDtMacroproceso();

            booResult = cDtMacroproceso.mtdInsertarMacroproceso(objMacroproceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objMacroproceso">Informacion de macroproceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarMacroproceso(clsMacroproceso objMacroproceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtMacroproceso cDtMacroproceso = new clsDtMacroproceso();

            booResult = cDtMacroproceso.mtdActualizarMacroproceso(objMacroproceso, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objMacroproceso">Informacion de Macroproceso</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsMacroproceso objMacroproceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtMacroproceso cDtMProceso = new clsDtMacroproceso();

            booResult = cDtMProceso.mtdActualizarEstado(objMacroproceso, ref strErrMsg);

            return booResult;
        }

        public DataTable mtdDataMacroproceso(List<clsMacroproceso> lstMacroP)
        {
            DataTable dtTable = new DataTable();
            DataRow dr;

            dtTable.Columns.Add("intId", typeof(string));
            dtTable.Columns.Add("strNombreMacroproceso", typeof(string));
            dtTable.Columns.Add("intIdCadenaDeValor", typeof(string));
            dtTable.Columns.Add("strNombreCadenaValor", typeof(string));
            dtTable.Columns.Add("strDescripcion", typeof(string));
            dtTable.Columns.Add("strObjetivo", typeof(string));
            dtTable.Columns.Add("booEstado", typeof(bool));
            dtTable.Columns.Add("intIdUsuario", typeof(string));
            dtTable.Columns.Add("strNombreUsuario", typeof(string));
            dtTable.Columns.Add("intCargoResponsable", typeof(string));
            dtTable.Columns.Add("strNombreResponsable", typeof(string));
            dtTable.Columns.Add("dtFechaRegistro", typeof(string));

            foreach (clsMacroproceso objMP in lstMacroP)
            {
                dr = dtTable.NewRow();
                dr["intId"] = objMP.intId.ToString();
                dr["strNombreMacroproceso"] = objMP.strNombreMacroproceso.ToString();
                dr["intIdCadenaDeValor"] = objMP.intIdCadenaDeValor.ToString();
                dr["strNombreCadenaValor"] = objMP.strNombreCadenaValor.ToString();
                dr["strDescripcion"] = objMP.strDescripcion.ToString();
                dr["strObjetivo"] = objMP.strObjetivo.ToString();
                dr["booEstado"] = objMP.booEstado;
                dr["intIdUsuario"] = objMP.intIdUsuario.ToString();
                dr["strNombreUsuario"] = objMP.strNombreUsuario.ToString();
                dr["intCargoResponsable"] = objMP.intCargoResponsable.ToString();
                dr["strNombreResponsable"] = objMP.strNombreResponsable.ToString();
                dr["dtFechaRegistro"] = objMP.dtFechaRegistro.ToString();
                dtTable.Rows.Add(dr);
            }

            return dtTable;
        }
    }
}