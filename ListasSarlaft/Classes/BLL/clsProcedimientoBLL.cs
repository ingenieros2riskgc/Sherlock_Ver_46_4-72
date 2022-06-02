using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsProcedimientoBLL
    {
        /// <summary>
        /// Realiza la consulta del Procedimiento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsProcedimiento> mtdConsultarProcedimiento(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsProcedimiento> lstProcedimiento = new List<clsProcedimiento>();
            clsDtProcedimiento cDtProcedimiento = new clsDtProcedimiento();
            clsProcedimiento objProcedimiento = new clsProcedimiento();
            #endregion Vars

            dtInfo = cDtProcedimiento.mtdConsultarProcedimiento(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objProcedimiento = new clsProcedimiento(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["Descripcion"].ToString().Trim(),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdActividad"].ToString().Trim()),
                            dr["DescActividad"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                        lstProcedimiento.Add(objProcedimiento);
                    }
                }
                else
                {
                    lstProcedimiento = null;
                    //strErrMsg = "No hay información de Procedimiento.";
                }
            }
            else
                lstProcedimiento = null;

            return lstProcedimiento;
        }

        /// <summary>
        /// Realiza la insercion de los campos del Procedimiento
        /// </summary>
        /// <param name="objProcedimiento">Informacion del Procedimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarProcedimiento(clsProcedimiento objProcedimiento, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtProcedimiento cDtProcedimiento = new clsDtProcedimiento();

            booResult = cDtProcedimiento.mtdInsertarProcedimiento(objProcedimiento, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objProcedimiento">Informacion del Procedimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarProcedimiento(clsProcedimiento objProcedimiento, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtProcedimiento cDtProcedimiento = new clsDtProcedimiento();

            booResult = cDtProcedimiento.mtdActualizarProcedimiento(objProcedimiento, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objProcedimiento">Informacion del Procedimiento</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsProcedimiento objProcedimiento, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtProcedimiento cDtProcedimiento = new clsDtProcedimiento();

            booResult = cDtProcedimiento.mtdActualizarEstado(objProcedimiento, ref strErrMsg);

            return booResult;
        }
    }
}