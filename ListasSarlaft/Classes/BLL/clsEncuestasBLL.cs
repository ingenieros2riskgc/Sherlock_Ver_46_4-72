using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsEncuestasBLL
    {
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarEncuestas(clsEncuestas cEncuestas, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtEncuestas cDtEncuestas = new clsDtEncuestas();

            booResult = cDtEncuestas.mtdInsertarEncuestas(cEncuestas, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Auditorias por el MacroProceso
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsEncuestas> mtdConsultarEncuesta(ref List<clsEncuestas> lstEncuestas, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtEncuestas cDtRegistro = new clsDtEncuestas();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarEncuestas(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsEncuestas objControl = new clsEncuestas();
                            objControl.intIdEncuesta = Convert.ToInt32(dr["IdEncuesta"].ToString().Trim());
                            objControl.strNombreEncuesta = dr["NombreEncuesta"].ToString().Trim();
                            objControl.intCantPreguntas = Convert.ToInt32(dr["CantPregunta"].ToString().Trim());
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objControl.strDescripcionEmpresa = dr["DescripcionEmpresa"].ToString();
                            objControl.strUsuario = dr["Usuario"].ToString();
                            lstEncuestas.Add(objControl);
                        }
                    }
                    else
                        lstEncuestas = null;
                }
                else
                    lstEncuestas = null;
            }

            return lstEncuestas;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Auditorias por el MacroProceso
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsPreguntasEncuestas> mtdConsultarPreguntas(ref List<clsPreguntasEncuestas> lstPreguntas, ref string strErrMsg, ref int IdEncuesta)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtEncuestas cDtRegistro = new clsDtEncuestas();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarPreguntas(ref dtInfo, ref strErrMsg, ref IdEncuesta);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsPreguntasEncuestas objControl = new clsPreguntasEncuestas();
                            objControl.intIdPregunta = Convert.ToInt32(dr["IdPregunta"].ToString().Trim());
                            objControl.intIdEncuesta = Convert.ToInt32(dr["IdEncuesta"].ToString().Trim());
                            objControl.strTextoPregunta = dr["TextoPregunta"].ToString().Trim();
                            objControl.intConsecutivo = Convert.ToInt32(dr["Consecutivo"].ToString().Trim());

                            lstPreguntas.Add(objControl);
                        }
                    }
                    else
                        lstPreguntas = null;
                }
                else
                    lstPreguntas = null;
            }

            return lstPreguntas;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la no corformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdEncuesta(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtEncuestas cDtEncuesta = new clsDtEncuestas();
            DataTable dt = cDtEncuesta.mtdLastIdEncuesta(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarPregunta(clsPreguntasEncuestas cPregunta, ref string strErrMsg, ref int IdEncuesta)
        {
            bool booResult = false;
            clsDtEncuestas cDtEncuestas = new clsDtEncuestas();

            booResult = cDtEncuestas.mtdInsertarPregunta(cPregunta, ref strErrMsg, ref IdEncuesta);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarPregunta(clsPreguntasEncuestas cPregunta, ref string strErrMsg, ref int IdEncuesta)
        {
            bool booResult = false;
            clsDtEncuestas cDtEncuestas = new clsDtEncuestas();

            booResult = cDtEncuestas.mtdActualizarPregunta(cPregunta, ref strErrMsg, ref IdEncuesta);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEncuesta(clsEncuestas cEncuesta, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtEncuestas cDtEncuestas = new clsDtEncuestas();

            booResult = cDtEncuestas.mtdActualizarEncuesta(cEncuesta, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdConsecutivoQuestions(ref string strErrMsg, int IdEncuesta)
        {
            int Consecutivo = 0;
            clsDtEncuestas cDtEncuesta = new clsDtEncuestas();
            DataTable dt = cDtEncuesta.mtdConsecutivoQuestions(ref strErrMsg, IdEncuesta);
            foreach (DataRow dr in dt.Rows)
            {
                Consecutivo = Convert.ToInt32(dr["Consecutivo"].ToString());
            }
            return Consecutivo;
        }
    }
}