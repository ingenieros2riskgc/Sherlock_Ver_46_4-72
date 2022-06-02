using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLTratamientos
    {
        /// <summary>
        /// Metodo para consultar y visualizar los Tratamientos del Riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOTratamientos> mtdConsultarTratamientos(ref List<clsDTOTratamientos> lstTratamiento, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALTratamientos cDtTratamientos = new clsDALTratamientos();
            #endregion Vars

            booResult = cDtTratamientos.mtdConsultarTratamientos(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOTratamientos objTratamiento = new clsDTOTratamientos();
                            objTratamiento.intIdTratamiento = Convert.ToInt32(dr["IdTratamiento"].ToString().Trim());
                            objTratamiento.strTratamiento = dr["NombreTratamiento"].ToString().Trim();
                            objTratamiento.intIdUsuario = Convert.ToInt32(dr["UsuarioCreacion"].ToString().Trim());
                            objTratamiento.strUsuario = dr["Usuario"].ToString().Trim();
                            objTratamiento.dtFechaRegistro = Convert.ToDateTime(dr["FechaCreacion"].ToString().Trim());

                            lstTratamiento.Add(objTratamiento);
                        }
                    }
                    else
                        lstTratamiento = null;
                }
                else
                    lstTratamiento = null;
            }

            return lstTratamiento;
        }
        /// <summary>
        /// Metodo para insertar el registro de el tratamiento del riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarTratamiento(clsDTOTratamientos objTratamiento, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALTratamientos cDALTratamiento = new clsDALTratamientos();

            booResult = cDALTratamiento.mtdInsertarTratamiento(objTratamiento, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizacion el registro de el tratamiento del riesgo
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateTratamiento(clsDTOTratamientos objTratamiento, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALTratamientos cDALTratamiento = new clsDALTratamientos();

            booResult = cDALTratamiento.mtdUpdateTratamiento(objTratamiento, ref strErrMsg);

            return booResult;
        }
    }
}