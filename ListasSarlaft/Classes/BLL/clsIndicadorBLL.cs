using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsIndicadorBLL
    {
        /// <summary>
        /// Realiza la consulta del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsIndicador> mtdConsultarIndicador(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtIndicador cDtIndicador = new clsDtIndicador();
            clsIndicador objIndicador = new clsIndicador();
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            #endregion Vars

            dtInfo = cDtIndicador.mtdConsultarIndicador(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objIndicador = new clsIndicador(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["NombreIndicador"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim()),
                            Convert.ToDecimal(dr["Meta"].ToString().Trim()),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdObjCalidad"].ToString().Trim()),
                            dr["DescObjetivo"].ToString().Trim(),
                            Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["Proceso"].ToString().Trim()
                            );
                        lstIndicador.Add(objIndicador);
                    }
                }
                else
                    lstIndicador = null;
            }
            else
                lstIndicador = null;

            return lstIndicador;
        }
        /// <summary>
        /// Realiza la consulta del Indicador por Id
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsIndicador> mtdConsultarIndicadorById(ref string strErrMsg, int intIdIndicador)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtIndicador cDtIndicador = new clsDtIndicador();
            clsIndicador objIndicador = new clsIndicador();
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            #endregion Vars

            dtInfo = cDtIndicador.mtdConsultarIndicadorById(ref strErrMsg, intIdIndicador);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objIndicador = new clsIndicador(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["NombreIndicador"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim()),
                            Convert.ToDecimal(dr["Meta"].ToString().Trim()),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdObjCalidad"].ToString().Trim()),
                            dr["DescObjetivo"].ToString().Trim(),
                            Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim(),
                            dr["Proceso"].ToString().Trim()
                            );
                        lstIndicador.Add(objIndicador);
                    }
                }
                else
                    lstIndicador = null;
            }
            else
                lstIndicador = null;

            return lstIndicador;
        }
        /// <summary>
        /// Realiza la consulta del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsIndicador> mtdConsultarIndicador(int intTipoProceso, object objProceso, ref string strErrMsg)
        {
            #region Vars
            int intProceso = 0;
            DataTable dtInfo = new DataTable();
            clsIndicador objIndicador = new clsIndicador();
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsDtIndicador cDtIndicador = new clsDtIndicador();
            #endregion Vars

            switch (intTipoProceso)
            {
                case 1:
                    intProceso = ((clsMacroproceso)objProceso).intId;
                    break;
                case 2:
                    intProceso = ((clsProceso)objProceso).intId;
                    break;
                case 3:
                    intProceso = ((clsSubproceso)objProceso).intId;
                    break;
            }

            dtInfo = cDtIndicador.mtdConsultarIndicador(intTipoProceso, intProceso, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objIndicador = new clsIndicador(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["NombreIndicador"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim()),
                            dr["NombrePeriodo"].ToString().Trim(),
                            Convert.ToDecimal(dr["Meta"].ToString().Trim()),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdObjCalidad"].ToString().Trim()),
                            dr["DescObjetivo"].ToString().Trim(),
                            Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim()
                            );
                        lstIndicador.Add(objIndicador);
                    }
                }
                else
                    lstIndicador = null;
            }
            else
                lstIndicador = null;

            return lstIndicador;
        }
        /// <summary>
        /// Realiza la consulta del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsIndicador> mtdConsultarIndicador(int intTipoProceso, object objProceso, ref string strErrMsg, int intPeriodoAnual)
        {
            #region Vars
            int intProceso = 0;
            DataTable dtInfo = new DataTable();
            clsIndicador objIndicador = new clsIndicador();
            List<clsIndicador> lstIndicador = new List<clsIndicador>();
            clsDtIndicador cDtIndicador = new clsDtIndicador();
            #endregion Vars

            switch (intTipoProceso)
            {
                case 1:
                    intProceso = ((clsMacroproceso)objProceso).intId;
                    break;
                case 2:
                    intProceso = ((clsProceso)objProceso).intId;
                    break;
                case 3:
                    intProceso = ((clsSubproceso)objProceso).intId;
                    break;
            }

            dtInfo = cDtIndicador.mtdConsultarIndicador(intTipoProceso, intProceso, ref strErrMsg, intPeriodoAnual);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objIndicador = new clsIndicador(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["NombreIndicador"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdPeriodicidad"].ToString().Trim()),
                            dr["NombrePeriodo"].ToString().Trim(),
                            Convert.ToDecimal(dr["Meta"].ToString().Trim()),
                            dr["Estado"].ToString().Trim() == "True" ? true : false,
                            Convert.ToInt32(dr["IdObjCalidad"].ToString().Trim()),
                            dr["DescObjetivo"].ToString().Trim(),
                            Convert.ToInt32(dr["IdProcesoIndicador"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim()
                            );
                        lstIndicador.Add(objIndicador);
                    }
                }
                else
                    lstIndicador = null;
            }
            else
                lstIndicador = null;

            return lstIndicador;
        }

        /// <summary>
        /// Realiza la insercion de los campos de Indicador
        /// </summary>
        /// <param name="objIndicador">Informacion de Indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarIndicador(clsIndicador objIndicador, ref int intIdIndicador, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtIndicador cDtIndicador = new clsDtIndicador();

            booResult = cDtIndicador.mtdInsertarIndicador(objIndicador, ref intIdIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objIndicador">Informacion de Indicador</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsIndicador objIndicador, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtIndicador cDtIndicador = new clsDtIndicador();

            booResult = cDtIndicador.mtdActualizarEstado(objIndicador, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Permite la actualizacion del Indicador
        /// </summary>
        /// <param name="objIndicador"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public bool mtdActualizarIndicador(clsIndicador objIndicador, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtIndicador cDtIndicador = new clsDtIndicador();

            booResult = cDtIndicador.mtdActualizarIndicador(objIndicador, ref strErrMsg);

            return booResult;
        }
    }
}