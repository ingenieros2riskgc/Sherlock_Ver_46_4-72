using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ListasSarlaft.Classes.DTO.Calidad;

namespace ListasSarlaft.Classes
{
    public class clsRegistroRequerimientosBLL
    {
        /// <summary>
        /// Realiza la consulta de los Perfiles
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        //public List<clsRegistroRequerimientoss> mtdInsertarRequerimientos(clsRegistroRequerimientoss objRegReq, ref string strErrMsg)
        //{
        //    try
        //    {
        //        #region Vars
        //        DataTable dtInfo = new DataTable();
        //        List<clsRegistroRequerimientoss> lstRegReq = new List<clsRegistroRequerimientoss>();
        //        clsDtRegistroRequerimientoss cDtRegReq = new clsDtRegistroRequerimientoss(); 
        //        clsRegistroRequerimientoss objRegReqq = new clsRegistroRequerimientoss();
        //        #endregion Vars
        //        dtInfo = cDtRegReq.mtdInsertarRequerimientos(objRegReq, ref strErrMsg);

        //        if (dtInfo != null)
        //        {
        //            if (dtInfo.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dtInfo.Rows)
        //                {
        //                    objRegReq = new clsRegistroRequerimientoss(
        //                        Convert.ToInt32(dr["Id"].ToString().Trim()),
        //                        Convert.ToInt32(dr["IdJO"].ToString().Trim()),
        //                        dr["NombreJO"].ToString().Trim(),
        //                        dr["ResumenCargo"].ToString().Trim(),
        //                        dr["Perfil"].ToString().Trim(),
        //                        Convert.ToInt32(dr["Estado"].ToString().Trim()),
        //                        dr["Roles"].ToString().Trim(),
        //                        dr["Habilidades"].ToString().Trim(),
        //                        dr["Educacion"].ToString().Trim(),
        //                        dr["Formacion"].ToString().Trim(),
        //                        dr["Experiencia"].ToString().Trim(),
        //                        dr["Funciones"].ToString().Trim(),
        //                        Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
        //                        Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
        //                        dr["NombreUsuario"].ToString().Trim(),
        //                        dr["FechaRegistro"].ToString().Trim(),
        //                        dr["CorreoUsuario"].ToString(),
        //                        dr["Codigo"].ToString(),
        //                        Convert.ToInt32(dr["IdJerarquiaAprueba"].ToString()),
        //                        Convert.ToInt32(dr["IdJerarquiaUsuario"].ToString()),
        //                        dr["NombreJerarquiaAprueba"].ToString()
        //                        );
        //                    lstRegReq.Add(objRegReq);
        //                }
        //            }
        //            else
        //            {
        //                lstRegReq = null;
        //                //strErrMsg = "No hay información de requerimientos.";
        //            }
        //        }
        //        else
        //            lstRegReq = null;

        //        return lstRegReq;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<Macroproceso> Macroprocesos()
        {
            try
            {
                using (clsDtPerfil objData = new clsDtPerfil())
                {
                    return objData.Macroprocesos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoDocumento> OpcionesEstadoPerfil()
        {
            try
            {
                using (clsDtPerfil objData = new clsDtPerfil())
                {
                    return objData.OpcionesEstadoPerfil();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}