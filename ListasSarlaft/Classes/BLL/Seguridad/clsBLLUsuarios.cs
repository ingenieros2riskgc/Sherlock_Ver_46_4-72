using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLUsuarios
    {
        /// <summary>
        /// Metodo para insertar el Registro de un Nuevo Usuario
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdRegistrarUsuario(clsDTOUsuarios Users, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALInsUsuarios cDALusuarios = new clsDALInsUsuarios();

            booResult = cDALusuarios.mtdRegistrarUsuario(Users, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los Usuarios
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOUsuarios> mtdConsultarUsuarios(ref List<clsDTOUsuarios> lstUsuarios, ref string strErrMsg, clsDTOUsuarios Users)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALConUsuarios cDtRegistro = new clsDALConUsuarios();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarUsuarios(ref dtInfo, ref strErrMsg, Users);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOUsuarios objUsuarios = new clsDTOUsuarios();
                            objUsuarios.intId = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objUsuarios.intIdRol = Convert.ToInt32(dr["IdRol"].ToString().Trim());
                            objUsuarios.strNombreRol = dr["NombreRol"].ToString().Trim();
                            objUsuarios.intIdJerarquia = Convert.ToInt32(dr["IdJerarquia"].ToString().Trim());
                            objUsuarios.strCargo = dr["NombreHijo"].ToString().Trim();
                            objUsuarios.strNumeroDocumento = dr["NumeroDocumento"].ToString().Trim();
                            objUsuarios.strNombres = dr["Nombres"].ToString().Trim();
                            objUsuarios.strApellidos = dr["Apellidos"].ToString().Trim();
                            objUsuarios.strUsuario =  dr["Usuario"].ToString().Trim();
                            objUsuarios.strNombreHijo = dr["NombreHijo"].ToString().Trim();
                            if (Convert.ToBoolean(dr["Bloqueado"].ToString().Trim()) == false)
                                objUsuarios.intBloqueado = 0;
                            else
                                objUsuarios.intBloqueado = 1;
                            objUsuarios.strContrasenaEncriptada = dr["Contrasena"].ToString().Trim();
                            lstUsuarios.Add(objUsuarios);
                        }
                    }
                    else
                        lstUsuarios = null;
                }
                else
                    lstUsuarios = null;
            }

            return lstUsuarios;
        }
        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objActividad">Informacion del Usuario</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarUsuarios(clsDTOUsuarios Users, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdUsuarios cDtusuarios = new clsDALUpdUsuarios();

            booResult = cDtusuarios.mtdActualizarUsuario(Users, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param name="objActividad">Informacion del Usuario</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtRestPassUsuario(int IdUsuario, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALUpdUsuarios cDtusuarios = new clsDALUpdUsuarios();

            booResult = cDtusuarios.mtRestPassUsuario(IdUsuario, ref strErrMsg);

            return booResult;
        }

    }
}