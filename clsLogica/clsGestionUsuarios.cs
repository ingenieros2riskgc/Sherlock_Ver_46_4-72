using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using clsDatos;
using clsDTO;
using System.Data.SqlClient;

namespace clsLogica
{
    public class clsGestionUsuarios
    {

        // Trae las posiciones donde se guardan estos campos
        readonly string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        readonly string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        readonly string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();
        private clsDatos.clsDatabase cDataBase = new clsDatos.clsDatabase();

        /// <summary>
        /// Metodo que consulta todos los perfiles configurados.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public List<clsDTOGestionGrupos> mtdConsultaGrupoTrabajo(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtGestionGruposUsuarios cDtGrupUsu = new clsDtGestionGruposUsuarios();
            clsDTOGestionGrupos objGrupUsu = new clsDTOGestionGrupos();
            List<clsDTOGestionGrupos> lstGrupoUsuario = new List<clsDTOGestionGrupos>();
            #endregion Vars

            dtInfo = cDtGrupUsu.mtdConsulGrupoTrabajo(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count >= 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objGrupUsu = new clsDTOGestionGrupos(
                            dr["idGrupoSoporte"].ToString().Trim(),
                            dr["NombreGrupoSoporte"].ToString().Trim()
                            );

                        lstGrupoUsuario.Add(objGrupUsu);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstGrupoUsuario = null;
                    strErrMsg = "No hay información sobre registros.";
                }
            }
            else
                lstGrupoUsuario = null;
            return lstGrupoUsuario;
        }

        public List<clsDTOGestionUsuarios> mtdCargarIntegrantes(string strIds, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtGestionGruposUsuarios cDtCarUsu = new clsDtGestionGruposUsuarios();
            clsDTOGestionUsuarios objCarUsu = new clsDTOGestionUsuarios();
            List<clsDTOGestionUsuarios> lstDatoIntegrante = new List<clsDTOGestionUsuarios>();
            #endregion Vars

            dtInfo = cDtCarUsu.mtdConsulIntegrante(strIds, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objCarUsu = new clsDTOGestionUsuarios(
                            dr["IdUsuarioSoporte"].ToString().Trim(),
                            dr["NombreUsuarioSoporte"].ToString().Trim(),
                            dr["UsuarioSoporte"].ToString().Trim(),
                            dr["CorreUsuarioSoporte"].ToString().Trim(),
                            dr["idGrupoSoporte"].ToString().Trim()
                            );

                        lstDatoIntegrante.Add(objCarUsu);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstDatoIntegrante = null;
                    strErrMsg = "No hay información sobre registros.";
                }
            }
            else
                lstDatoIntegrante = null;
            return lstDatoIntegrante;
        }

        public bool mtdActualizarGrupoTrabajo(clsDTOGestionGrupos objUpdReq, ref string strErrMsg, Boolean booResult)
        {
            clsDtGestionGruposUsuarios cDtGrupUsu = new clsDtGestionGruposUsuarios();

            booResult = cDtGrupUsu.mtdActualizarGrupo(objUpdReq, ref strErrMsg, booResult);
            return booResult;
        }

        public void mtdActualizarUsuario(clsDTOGestionUsuarios objUpdUsu, ref string strErrMsg)
        {
            clsDtGestionGruposUsuarios cDtGrupUsu = new clsDtGestionGruposUsuarios();

            cDtGrupUsu.mtdActualizarIntegrante(objUpdUsu, ref strErrMsg);
        }


        public void mtdInsertarRequerimientos(clsDTORegistroRequerimientos objRegReq, ref string strErrMsg)
        {
            clsDtRegistroRequerimientos cDtRegReq = new clsDtRegistroRequerimientos();

            cDtRegReq.mtdInsertarRequerimientos(objRegReq, ref strErrMsg);
        }


        public void mtdInsertarGrupoUsuario(clsDTOGestionGrupos objGrupUsu, ref string strErrMsg)
        {
            clsDtGestionGruposUsuarios cDtRegReq = new clsDtGestionGruposUsuarios();

            cDtRegReq.mtdInsertarGrupoUsuario(objGrupUsu, ref strErrMsg);
        }

        public void mtdInsertarIntegrante(clsDTOGestionUsuarios objUsuario, ref string strErrMsg)
        {
            clsDtGestionGruposUsuarios cDtUsuario = new clsDtGestionGruposUsuarios();

            cDtUsuario.mtdInsertarUsuario(objUsuario, ref strErrMsg);
        }
    }
}