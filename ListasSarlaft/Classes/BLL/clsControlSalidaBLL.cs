using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsControlSalidaBLL
    {
        /// <summary>
        /// Realiza la consulta de la cadena de valor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEstadoControlSalida> mtdConsultarEstados(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsEstadoControlSalida> lstEstados = new List<clsEstadoControlSalida>();
            clsControlSalidaDAL cDtSalida = new clsControlSalidaDAL();
            clsEstadoControlSalida objEstado = new clsEstadoControlSalida();
            #endregion Vars

            dtInfo = cDtSalida.mtdConsultarEstados(booEstado, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEstado = new clsEstadoControlSalida(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["NombreEstado"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim()));
                        lstEstados.Add(objEstado);
                    }
                }
                else
                {
                    lstEstados = null;
                    strErrMsg = "No hay información de Estados.";
                }
            }
            else
                lstEstados = null;

            return lstEstados;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarControlSalida(clsControlSalida objControlSalida, ref string strErrMsg)
        {
            bool booResult = false;
            clsControlSalidaDAL cDTsalida = new clsControlSalidaDAL();

            booResult = cDTsalida.mtdInsertarControlSalida(objControlSalida, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la evaluacion de proveedor
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdControlSalida(ref string strErrMsg)
        {
            int LastId = 0;
            clsControlSalidaDAL cDtControlSalida = new clsControlSalidaDAL();
            DataTable dt = cDtControlSalida.mtdLastIdControlSalida(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarObservacion(clsObservacionControlSalida objObservacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsControlSalidaDAL cDtServicio = new clsControlSalidaDAL();

            booResult = cDtServicio.mtdInsertarValorObservacion(objObservacion, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarControlProcesos(clsControlProceso objControlProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsControlSalidaDAL cDtServicio = new clsControlSalidaDAL();

            booResult = cDtServicio.mtdInsertarControlProceso(objControlProceso, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar todos los Controles de la Version
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsControlSalida> mtdConsultarControlSalida(ref string strErrMsg, ref List<clsControlSalida> lstCrlSalida)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsControlSalidaDAL cDtControl = new clsControlSalidaDAL();

            booResult = cDtControl.mtdConsultarControlSalida(ref dtInfo, ref strErrMsg);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsControlSalida objControl = new clsControlSalida();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objControl.intIdMacroProceso = Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim());
                            objControl.strNombreProceso = dr["Nombre"].ToString().Trim();
                            objControl.intIdEstadoControlSalida = Convert.ToInt32(dr["IdEstadoCtrlSalida"].ToString().Trim());
                            objControl.strNombreEstado = dr["NombreEstado"].ToString().Trim();
                            objControl.strNoConformidad = dr["NoConformidad"].ToString().Trim();
                            objControl.strAccionesTomadas = dr["AccionesTomadas"].ToString().Trim();
                            objControl.intCargoResponsable = Convert.ToInt32(dr["PersonaAutoriza"].ToString().Trim());
                            objControl.strPersonaAutoriza = dr["NombreHijo"].ToString().Trim();
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            objControl.strUsuario = dr["Usuario"].ToString().Trim();
                            objControl.strObservaciones = dr["Descripcion"].ToString().Trim();

                            lstCrlSalida.Add(objControl);
                        }
                    }
                    else
                    {
                        lstCrlSalida = null;
                        strErrMsg = "No hay datos registrados";
                    }
                }
                else
                    lstCrlSalida = null;
            }
            return lstCrlSalida;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarControlSalida(clsControlSalida objCrlSalida, ref string strErrMsg)
        {
            bool booResult = false;
            clsControlSalidaDAL cDtServicio = new clsControlSalidaDAL();

            booResult = cDtServicio.mtdActualizarControlSalida(objCrlSalida, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite la insercion de la evaluacion de proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de proveedor</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarObservacion(clsObservacionControlSalida objObservacion, ref string strErrMsg)
        {
            bool booResult = false;
            clsControlSalidaDAL cDtServicio = new clsControlSalidaDAL();

            booResult = cDtServicio.mtdActualizarObservacion(objObservacion, ref strErrMsg);

            return booResult;
        }
    }
}