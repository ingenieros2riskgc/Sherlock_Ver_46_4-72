using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ListasSarlaft.Classes.DTO.Calidad;

namespace ListasSarlaft.Classes
{
    public class clsPerfilBLL
    {
        /// <summary>
        /// Realiza la consulta de los Perfiles
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsPerfil> mtdConsultarPerfil(clsPerfil perfil)
        {
            try
            {
                #region Vars
                DataTable dtInfo = new DataTable();
                List<clsPerfil> lstPerfil = new List<clsPerfil>();
                clsDtPerfil cDtPerfil = new clsDtPerfil();
                clsPerfil objPerfil = new clsPerfil();
                #endregion Vars
                dtInfo = cDtPerfil.mtdConsultarPerfil(perfil);

                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            objPerfil = new clsPerfil(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdJO"].ToString().Trim()),
                                dr["NombreJO"].ToString().Trim(),
                                dr["ResumenCargo"].ToString().Trim(),
                                dr["Perfil"].ToString().Trim(),
                                Convert.ToInt32(dr["Estado"].ToString().Trim()),
                                dr["Roles"].ToString().Trim(),
                                dr["Habilidades"].ToString().Trim(),
                                dr["Educacion"].ToString().Trim(),
                                dr["Formacion"].ToString().Trim(),
                                dr["Experiencia"].ToString().Trim(),
                                dr["Funciones"].ToString().Trim(),
                                Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim()),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim(),
                                dr["CorreoUsuario"].ToString(),
                                dr["Codigo"].ToString(),
                                Convert.ToInt32(dr["IdJerarquiaAprueba"].ToString()),
                                Convert.ToInt32(dr["IdJerarquiaUsuario"].ToString()),
                                dr["NombreJerarquiaAprueba"].ToString()
                                );
                            lstPerfil.Add(objPerfil);
                        }
                    }
                    else
                    {
                        lstPerfil = null;
                        //strErrMsg = "No hay información de Perfiles.";
                    }
                }
                else
                    lstPerfil = null;

                return lstPerfil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza la insercion de los campos de Perfil
        /// </summary>
        /// <param name="objPerfil">Informacion de Perfil</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarPerfil(clsPerfil objPerfil, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtPerfil cDtPerfil = new clsDtPerfil();

            booResult = cDtPerfil.mtdInsertarActualizarPerfil(objPerfil, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objPerfil">Informacion de Perfil</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarPerfil(clsPerfil objPerfil, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtPerfil cDtPerfil = new clsDtPerfil();

            booResult = cDtPerfil.mtdInsertarActualizarPerfil(objPerfil, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objPerfil">Informacion de Perfil</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsPerfil objPerfil, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtPerfil cDtPerfil = new clsDtPerfil();

            booResult = cDtPerfil.mtdActualizarEstado(objPerfil, ref strErrMsg);

            return booResult;
        }

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