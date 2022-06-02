using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsArchivosCalidadBLL
    {
        /// <summary>
        /// Realiza la consulta de Archivos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsArchivosCalidad> mtdConsultarArchivos(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsArchivosCalidad> lstArchivos = new List<clsArchivosCalidad>();
            clsDtArchivosCalidad cDtArchivoCalidad = new clsDtArchivosCalidad();
            clsArchivosCalidad objArchivo = new clsArchivosCalidad();
            #endregion Vars

            dtInfo = cDtArchivoCalidad.mtdConsultarArchivos(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objArchivo = new clsArchivosCalidad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoControl"].ToString().Trim()),
                            Convert.ToInt32(dr["IdControl"].ToString().Trim()),
                            dr["NombreArchivo"].ToString().Trim(),
                            (byte[])dr["ArchivoBinario"],
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstArchivos.Add(objArchivo);
                    }
                }
                else
                {
                    lstArchivos = null;
                    //strErrMsg = "No hay información de Archivos de Calidad.";
                }
            }
            else
                lstArchivos = null;

            return lstArchivos;
        }

        /// <summary>
        /// Realiza la consulta de Archivos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsArchivosCalidad> mtdConsultarArchivos(clsArchivosCalidad objArchivoIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsArchivosCalidad> lstArchivos = new List<clsArchivosCalidad>();
            clsDtArchivosCalidad cDtArchivoCalidad = new clsDtArchivosCalidad();
            clsArchivosCalidad objArchivo = new clsArchivosCalidad();
            #endregion Vars

            dtInfo = cDtArchivoCalidad.mtdConsultarArchivos(objArchivoIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objArchivo = new clsArchivosCalidad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoControl"].ToString().Trim()),
                            Convert.ToInt32(dr["IdControl"].ToString().Trim()),
                            dr["NombreArchivo"].ToString().Trim(),
                            (byte[])dr["ArchivoBinario"],
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstArchivos.Add(objArchivo);
                    }
                }
                else
                {
                    lstArchivos = null;
                    //strErrMsg = "No hay información de Archivos de Calidad.";
                }
            }
            else
                lstArchivos = null;

            return lstArchivos;
        }

        /// <summary>
        /// Realiza la consulta de Archivo a descargar
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public clsArchivosCalidad mtdDescargarArchivo(clsArchivosCalidad objArchivoIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsArchivosCalidad> lstArchivos = new List<clsArchivosCalidad>();
            clsDtArchivosCalidad cDtArchivoCalidad = new clsDtArchivosCalidad();
            clsArchivosCalidad objArchivoOUT = new clsArchivosCalidad();
            #endregion Vars

            dtInfo = cDtArchivoCalidad.mtdConsultarArchivo(objArchivoIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objArchivoOUT = new clsArchivosCalidad(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            Convert.ToInt32(dr["IdTipoControl"].ToString().Trim()),
                            Convert.ToInt32(dr["IdControl"].ToString().Trim()),
                            dr["NombreArchivo"].ToString().Trim(),
                            (byte[])dr["ArchivoBinario"],
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                    }
                }
                else
                {
                    objArchivoOUT = null;
                    strErrMsg = "No hay información de Archivo de Calidad.";
                }
            }
            else
                objArchivoOUT = null;

            return objArchivoOUT;
        }

        public int mtdConsecutivoArchivo(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            int intResult = 0;
            clsDtArchivosCalidad cDtArchivoCalidad = new clsDtArchivosCalidad();
            #endregion Vars

            dtInfo = cDtArchivoCalidad.mtdConsecutivoArchivo(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                    intResult = Convert.ToInt32(dtInfo.Rows[0]["Consecutivo"].ToString().Trim());
                else
                    intResult = 1;
            }

            return intResult;
        }

        /// <summary>
        /// Realiza la insercion de los campos del Archivo
        /// </summary>
        /// <param name="objProcedimiento">Informacion del Archivo</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarArchivo(clsArchivosCalidad objArchivo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtArchivosCalidad cDtArchivoCalidad = new clsDtArchivosCalidad();

            booResult = cDtArchivoCalidad.mtdInsertarArchivo(objArchivo, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objProcedimiento">Informacion del Archivo</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsArchivosCalidad objArchivo, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtArchivosCalidad cDtArchivoCalidad = new clsDtArchivosCalidad();

            booResult = cDtArchivoCalidad.mtdActualizarEstado(objArchivo, ref strErrMsg);

            return booResult;
        }
    }
}