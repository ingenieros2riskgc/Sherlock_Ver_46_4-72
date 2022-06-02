using System;
using System.Collections.Generic;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using clsDatos;

namespace ListasSarlaft.Classes
{
    public class cCuenta : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        private cEncriptacion cEncrypt = new cEncriptacion();
        
        public void notAuthenticated()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            IdUsuario = 0;
        }

        public void isAuthenticated(string NombreRol, Int64 idUsuario, String nombreUsuario, String loginUsuario, String idRol, String idJerarquia)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(NombreRol, false);
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            LoginUsuario = loginUsuario;
            IdRol = idRol;
            IdJerarquia = idJerarquia;
            NNombreRol = NombreRol;
        }

        public void actualizarRol(String NombreRol, String IdRol)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.Roles SET NombreRol = N'" + NombreRol + "' WHERE (IdRol = " + IdRol + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public void agregarRol(String NombreRol)
        {
            try
            {
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@IdRol", OleDbType.Integer, 18);
                parameter.Direction = ParameterDirection.Output;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@NombreRol", OleDbType.Char);
                parameter.Value = NombreRol;
                parameters[1] = parameter;
                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("agregarRol", parameters);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public DataTable Roles()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdRol, LTRIM(RTRIM(NombreRol)) AS NombreRol FROM Listas.Roles ORDER BY NombreRol");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable RolesVer()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdRol, LTRIM(RTRIM(NombreRol)) AS NombreRol FROM Listas.Roles where IdRol <> 99 ORDER BY NombreRol");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ContarUsuarios(String IdRol)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select COUNT(idusuario) as Total from Listas.Usuarios where IdRol <> " + IdRol + " and Bloqueado = 0");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ContarUsuariosEvento(String IdRol)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select COUNT(idusuario) as Total from Listas.Usuarios where IdRol = " + IdRol + " and Bloqueado = 0");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable Login_SN(String IdUsuario, int Login)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("update Listas.Usuarios set Login = " + Login + " where IdUsuario = " + IdUsuario);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable MensajeLogin(String Usuario)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuario, LU.Contrasena, LU.Bloqueado, LU.login, LU.FechaUltActualPassword, LU.IdUsuario, LTRIM(RTRIM(LR.NombreRol)) AS NombreRol, LTRIM(RTRIM(LU.Usuario)) AS Usuario, LR.IdRol, LU.IdJerarquia, LU.IdMacroProcesoU, LU.IdProcesoU, LU.VerTodosProcesos FROM Listas.Usuarios LU INNER JOIN Listas.Roles LR ON LU.IdRol = LR.IdRol WHERE Usuario = N'" + Usuario + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public String contrasenaUsuario()
        {
            DataTable dtInformacion = new DataTable();
            string strResult = string.Empty;

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Contrasena)) AS Contrasena FROM Listas.Usuarios WHERE (IdUsuario = " + IdUsuario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }

            if (IdUsuario != 0)
                strResult = dtInformacion.Rows[0]["Contrasena"].ToString().Trim();

            return strResult;
        }

        public void updateUser(String IdRol, String NumeroDocumento, String Nombres, String Apellidos, String Usuario, 
            String Contrasena, String Bloqueado, String UsuarioId, String IdJerarquia)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.Usuarios SET IdRol = " + IdRol.Trim() + ", NumeroDocumento = N'" + NumeroDocumento.Trim() + 
                    "', Nombres = N'" + Nombres.Trim() + "', Apellidos = N'" + Apellidos.Trim() + "', Usuario = N'" + Usuario.Trim() + 
                    "', Contrasena = N'" + Contrasena.Trim() + "', Bloqueado = " + Bloqueado.Trim() + ", IdJerarquia = " + IdJerarquia + 
                    ", FechaUltActualPassword = GETDATE() WHERE (IdUsuario = " + UsuarioId.ToString() + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public DataTable ConsultarUsuario(String NumeroDocumento, String Nombres, String Apellidos)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty;

            try
            {
                if (NumeroDocumento != string.Empty)
                    condicion = "WHERE (Listas.Usuarios.NumeroDocumento LIKE N'%" + NumeroDocumento.Trim() + "%') ";

                if (Nombres != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (Listas.Usuarios.Nombres LIKE N'%" + Nombres.Trim() + "%') ";
                    else
                        condicion = condicion + "AND (Listas.Usuarios.Nombres LIKE N'%" + Nombres.Trim() + "%') ";
                }
                if (Apellidos != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (Listas.Usuarios.Apellidos LIKE N'%" + Apellidos.Trim() + "%') ";

                    else
                        condicion = condicion + "AND (Listas.Usuarios.Apellidos LIKE N'%" + Apellidos.Trim() + "%') ";
                }

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Listas.Usuarios.IdUsuario, Listas.Roles.IdRol, Listas.Roles.NombreRol, Listas.Usuarios.NumeroDocumento, Listas.Usuarios.Nombres, Listas.Usuarios.Apellidos, Listas.Usuarios.Usuario, Listas.Usuarios.Bloqueado, Listas.Usuarios.Contrasena, Listas.Usuarios.IdJerarquia, Parametrizacion.JerarquiaOrganizacional.NombreHijo, Listas.Usuarios.IdMacroProcesoU, Listas.Usuarios.IdProcesoU, Listas.Usuarios.VerTodosProcesos, FechaUltActualPassword FROM Listas.Usuarios INNER JOIN Listas.Roles ON Listas.Usuarios.IdRol = Listas.Roles.IdRol INNER JOIN Parametrizacion.JerarquiaOrganizacional ON Listas.Usuarios.IdJerarquia = Parametrizacion.JerarquiaOrganizacional.idHijo " + condicion);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public void actualizarPermisos(String Consultar, String Agregar, String Actualizar, String Borrar, String IdPermiso)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.Permisos SET Consultar = " + Consultar + ", Agregar = " + Agregar + ", Actualizar = " + Actualizar + ", Borrar = " + Borrar + " WHERE (IdPermiso = " + IdPermiso + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public DataTable validateUser(String Usuario)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Usuario FROM Listas.Usuarios WHERE (Usuario = N'" + Usuario + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadInfoPermisos(String IdRol)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Listas.Permisos.IdPermiso, Listas.Formularios.NombreModulo , Listas.Formularios.NombreSubModulo , Listas.Formularios.NombreFormulario, (CASE WHEN Listas.Permisos.Consultar = 0 THEN 'False' ELSE 'True' END) AS Consultar, (CASE WHEN Listas.Permisos.Agregar = 0 THEN 'False' ELSE 'True' END) AS Agregar, (CASE WHEN Listas.Permisos.Actualizar = 0 THEN 'False' ELSE 'True' END) AS Actualizar, (CASE WHEN Listas.Permisos.Borrar = 0 THEN 'False' ELSE 'True' END) AS Borrar FROM Listas.Permisos INNER JOIN Listas.Formularios ON Listas.Permisos.IdFormulario = Listas.Formularios.IdFormulario WHERE (Listas.Permisos.IdRol = " + IdRol + ") ORDER BY Listas.Formularios.IdFormulario");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public DataTable autenticarUsuario(string Usuario, string Contrasena)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty, strEncryptPass = string.Empty;
            #endregion Variables

            try
            {
                strEncryptPass = mtdEncriptarContrasena(Contrasena);
                //06/11/2014
                //strConsulta = "SELECT LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuario, LU.IdUsuario, LTRIM(RTRIM(LR.NombreRol)) AS NombreRol, LTRIM(RTRIM(LU.Usuario)) AS Usuario, LR.IdRol, LU.IdJerarquia, LU.IdMacroProcesoU, LU.IdProcesoU, LU.VerTodosProcesos, LU.FechaUltActualPassword FROM Listas.Usuarios LU INNER JOIN Listas.Roles LR ON LU.IdRol = LR.IdRol WHERE (LU.Usuario = N'" + Usuario + "') AND (LU.Contrasena COLLATE Modern_Spanish_CS_AS = N'" + strEncryptPass + "') AND (LU.Bloqueado = 0) AND (LU.Login = 0)";
                strConsulta = "SELECT LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuario, LU.IdUsuario, LTRIM(RTRIM(LR.NombreRol)) AS NombreRol, LTRIM(RTRIM(LU.Usuario)) AS Usuario, LR.IdRol, LU.IdJerarquia, LU.IdMacroProcesoU, LU.IdProcesoU, LU.VerTodosProcesos, LU.FechaUltActualPassword FROM Listas.Usuarios LU INNER JOIN Listas.Roles LR ON LU.IdRol = LR.IdRol WHERE (LU.Usuario = N'" + Usuario + "') AND (LU.Contrasena COLLATE Modern_Spanish_CS_AS = N'" + strEncryptPass + "') AND (LU.Bloqueado = 0)";
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }

            return dtInformacion;
        }

        public String permisosConsulta(String IdFormulario)
        {
            string strResultado = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                if (mtdEstaBloqueado())
                    strResultado = "False";
                else
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta("SELECT Consultar FROM Listas.Permisos WHERE (IdRol = " + IdRol + ") AND (IdFormulario = " + IdFormulario + ")");
                    cDataBase.desconectar();

                    if (dtInformacion.Rows[0][0] == DBNull.Value)
                        strResultado = "False";
                    else
                        strResultado = dtInformacion.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                strResultado = "False";
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
            return strResultado;
        }

        public String permisosAgregar(String IdFormulario)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Agregar FROM Listas.Permisos WHERE (IdRol = " + IdRol + ") AND (IdFormulario = " + IdFormulario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion.Rows[0][0].ToString().Trim();
        }

        public String permisosActualizar(String IdFormulario)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Actualizar FROM Listas.Permisos WHERE (IdRol = " + IdRol + ") AND (IdFormulario = " + IdFormulario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }

            return dtInformacion.Rows[0][0].ToString().Trim();
        }

        public String permisosBorrar(String IdFormulario)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Borrar FROM Listas.Permisos WHERE (IdRol = " + IdRol + ") AND (IdFormulario = " + IdFormulario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion.Rows[0][0].ToString().Trim();
        }

        public String NombreDetalleJerarquia(String idHijo)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT NombreResponsable FROM Parametrizacion.DetalleJerarquiaOrg WHERE (idHijo = " + idHijo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }

            if (dtInformacion.Rows.Count > 0)
                return dtInformacion.Rows[0][0].ToString().Trim();
            else
                return "";
        }

        public void registerUser(String IdRol, String NumeroDocumento, String Nombres, String Apellidos, String Usuario, 
            String Contrasena, String Bloqueado, String IdJerarquia)
        {
            string strConsulta = string.Empty, strContrasenaEncriptada = string.Empty;

            try
            {
                strContrasenaEncriptada = mtdEncriptarContrasena(Contrasena.Trim());
                strConsulta = string.Format("INSERT Listas.Usuarios (IdRol, NumeroDocumento, Nombres, Apellidos, Usuario, Contrasena, Bloqueado, IdJerarquia, FechaUltActualPassword, Login)" +
                    " VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, GETDATE(), 0)", IdRol.Trim(), NumeroDocumento.Trim(), Nombres.Trim(),
                    Apellidos.Trim(), Usuario.Trim(), strContrasenaEncriptada, Bloqueado.Trim(), IdJerarquia);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Procedimiento para agregar el campo Macroproceso, Proceso y ver todos los procesos.
        /// </summary>
        /// <param name="IdRol"></param>
        /// <param name="NumeroDocumento"></param>
        /// <param name="Nombres"></param>
        /// <param name="Apellidos"></param>
        /// <param name="Usuario"></param>
        /// <param name="Contrasena"></param>
        /// <param name="Bloqueado"></param>
        /// <param name="IdJerarquia"></param>
        public void mtdRegistrarUsuario(string IdRol, string NumeroDocumento, string Nombres, string Apellidos, string Usuario, 
            string Contrasena, string Bloqueado, string IdJerarquia, string IdMacroproceso, string IdProceso, bool booTodosProcesos)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;

            try
            {
                #region Creacion Consulta
                if (booTodosProcesos)
                    strTodosProcesos = "1";
                else
                    strTodosProcesos = "0";

                strContrasenaEncriptada = mtdEncriptarContrasena(Contrasena.Trim());
                if (!string.IsNullOrEmpty(IdProceso))
                {
                    strConsulta = string.Format("INSERT Listas.Usuarios (IdRol, NumeroDocumento, Nombres, Apellidos, Usuario, Contrasena, Bloqueado, IdJerarquia, IdMacroprocesoU, IdProcesoU, VerTodosProcesos, FechaUltActualPassword, Login)" +
                        " VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, {9}, {10}, GETDATE(), 0) ",
                        IdRol.Trim(), NumeroDocumento.Trim(), Nombres.Trim(), Apellidos.Trim(), Usuario.Trim(), strContrasenaEncriptada,
                        Bloqueado.Trim(), IdJerarquia, IdMacroproceso, IdProceso, strTodosProcesos);
                }
                else
                {
                    strConsulta = string.Format("INSERT Listas.Usuarios (IdRol, NumeroDocumento, Nombres, Apellidos, Usuario, Contrasena, Bloqueado, IdJerarquia, IdMacroprocesoU, VerTodosProcesos, FechaUltActualPassword, Login)" +
                        " VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, {9}, GETDATE(), 0) ",
                        IdRol.Trim(), NumeroDocumento.Trim(), Nombres.Trim(), Apellidos.Trim(), Usuario.Trim(), strContrasenaEncriptada,
                        Bloqueado.Trim(), IdJerarquia, IdMacroproceso, strTodosProcesos);
                }
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public bool mtdRegistrarUsuario(string IdRol, string NumeroDocumento, string Nombres, string Apellidos,
            string Usuario, string Contrasena, string Bloqueado,
            string IdJerarquia, string IdMacroproceso, string IdProceso, bool booTodosProcesos,
            ref string strErrMsg)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                if (booTodosProcesos)
                    strTodosProcesos = "1";
                else
                    strTodosProcesos = "0";

                strContrasenaEncriptada = mtdEncriptarContrasena(Contrasena.Trim());
                if (!string.IsNullOrEmpty(IdProceso))
                {
                    strConsulta = string.Format("INSERT Listas.Usuarios (IdRol, NumeroDocumento, Nombres, Apellidos, Usuario, Contrasena, Bloqueado, IdJerarquia, IdMacroprocesoU, IdProcesoU, VerTodosProcesos, FechaUltActualPassword, Login)" +
                        " VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, {9}, {10}, GETDATE(), 0) ",
                        IdRol.Trim(), NumeroDocumento.Trim(), Nombres.Trim(), Apellidos.Trim(), Usuario.Trim(), strContrasenaEncriptada,
                        Bloqueado.Trim(), IdJerarquia, IdMacroproceso, IdProceso, strTodosProcesos);
                }
                else
                {
                    strConsulta = string.Format("INSERT Listas.Usuarios (IdRol, NumeroDocumento, Nombres, Apellidos, Usuario, Contrasena, Bloqueado, IdJerarquia, IdMacroprocesoU, VerTodosProcesos, FechaUltActualPassword, Login)" +
                        " VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, {9}, GETDATE(), 0) ",
                        IdRol.Trim(), NumeroDocumento.Trim(), Nombres.Trim(), Apellidos.Trim(), Usuario.Trim(), strContrasenaEncriptada,
                        Bloqueado.Trim(), IdJerarquia, IdMacroproceso, strTodosProcesos);
                }
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al insertar el nuevo usuario. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Procedimiento para actualizar agregandose los campos Macroproceso, Proceso y ver todos los procesos
        /// </summary>
        /// <param name="IdRol"></param>
        /// <param name="NumeroDocumento"></param>
        /// <param name="Nombres"></param>
        /// <param name="Apellidos"></param>
        /// <param name="Usuario"></param>
        /// <param name="Contrasena"></param>
        /// <param name="Bloqueado"></param>
        /// <param name="UsuarioId"></param>
        /// <param name="IdJerarquia"></param>
        /// <param name="IdMacroproceso"></param>
        /// <param name="IdProceso"></param>
        /// <param name="booTodosProcesos"></param>
        public void mtdActualizarUsuario(String IdRol, String NumeroDocumento, String Nombres, String Apellidos, String Usuario, String Contrasena,
            String Bloqueado, String UsuarioId, String IdJerarquia, string IdMacroproceso, string IdProceso, bool booTodosProcesos)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;

            try
            {
                #region Creacion Consulta
                if (booTodosProcesos)
                    strTodosProcesos = "1";
                else
                    strTodosProcesos = "0";

                if (!string.IsNullOrEmpty(IdProceso))
                {
                    strConsulta = string.Format("UPDATE Listas.Usuarios SET IdRol = {0} , NumeroDocumento = '{1}', Nombres = '{2}', Apellidos ='{3}', Usuario = '{4}', Bloqueado = {6}, IdJerarquia = {7}, IdMacroprocesoU = {8},  IdProcesoU = {9}, VerTodosProcesos = {10} WHERE (IdUsuario = {11})",
                        IdRol.Trim(), NumeroDocumento.Trim(), Nombres.Trim(), Apellidos.Trim(), Usuario.Trim(), strContrasenaEncriptada, Bloqueado.Trim(), IdJerarquia, IdMacroproceso, IdProceso, strTodosProcesos, UsuarioId.ToString());
                }
                else
                {
                    strConsulta = string.Format("UPDATE Listas.Usuarios SET IdRol = {0} , NumeroDocumento = '{1}', Nombres = '{2}', Apellidos ='{3}', Usuario = '{4}', Bloqueado = {6}, IdJerarquia = {7}, IdMacroprocesoU = {8}, VerTodosProcesos = {9} WHERE (IdUsuario = {10})",
                      IdRol.Trim(), NumeroDocumento.Trim(), Nombres.Trim(), Apellidos.Trim(), Usuario.Trim(), strContrasenaEncriptada, Bloqueado.Trim(), IdJerarquia, IdMacroproceso, strTodosProcesos, UsuarioId.ToString());
                }
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public DataTable LoadMacroProceso()
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdMacroProceso], [Nombre] FROM [Procesos].[Macroproceso]");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public DataTable LoadProceso(string strMacroProceso)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdProceso], [Nombre] FROM [Procesos].[proceso] WHERE [IdMacroProceso] = " + strMacroProceso);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public bool mtdActualizarLoginSN(int intLogin)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            bool booResult = true;

            try
            {
                strConsulta = "UPDATE Listas.Usuarios SET Login = " + intLogin + " WHERE IdUsuario = " + this.IdUsuario;
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                booResult = false;
                //throw new Exception(ex.Message);
            }

            return booResult;
        }

        #region Contrasena
        public void actualizarContrasena(String Contrasena)
        {
            string strConsulta = string.Empty, strPassEncrypted = string.Empty;

            try
            {
                strPassEncrypted = mtdEncriptarContrasena(Contrasena);
                strConsulta = "UPDATE Listas.Usuarios SET Contrasena = N'" + strPassEncrypted + "' , FechaUltActualPassword = GETDATE() WHERE (IdUsuario = " + IdUsuario + ")";
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }

        public void ResetContrasena(String IdUsuario)
        {
            string strConsulta = string.Empty, strContrasenaEncriptada = string.Empty;

            try
            {
                strContrasenaEncriptada = mtdEncriptarContrasena("Sherlock+"); //Vieja contraseña sherlock2012
                strConsulta = string.Format("UPDATE Listas.Usuarios SET Contrasena = N'{0}', FechaUltActualPassword = GETDATE() WHERE (IdUsuario = {1})", strContrasenaEncriptada, IdUsuario);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public bool mtdValidarContrasena(string strContrasena, ref string strMensaje)
        {
            #region Variables
            bool booResult = true;
            string strRegExpr = @"(?=^.{8,}$)(?=.*?[0-9]*)(?=.*[!-/:-@{-~Ç-■^_])(?=.*[A-Z]+)(?=.*[a-z]*).*$";
            Regex regExpresion = new Regex(strRegExpr);
            #endregion

            #region Validacion Longitudes
            if (strContrasena.Length < 8)
            {
                booResult = false;
                strMensaje = "La contraseña es menor a 8 caracteres.";
            }
            #endregion Validacion Longitudes

            #region Validacion de caracteres
            if (booResult)
            {
                if (!regExpresion.IsMatch(strContrasena))
                {
                    booResult = false;
                    strMensaje = "La contraseña debe tener al menos una letra mayúscula y un caracter especial, letras minúsculas y/o números, sin espacios.";
                }
            }
            #endregion Validacion de caracteres

            return booResult;
        }

        public bool mtdValidarCaducaContrasena(object objFechaUltActual, int intDias)
        {
            #region Variables
            bool booResult = true;
            DateTime dtFechaActual = DateTime.Now;
            #endregion Variables

            if (objFechaUltActual != null)
            {
                if (!string.IsNullOrEmpty(objFechaUltActual.ToString().Trim()))
                {
                    DateTime dtFechaBD = Convert.ToDateTime(objFechaUltActual.ToString().Trim());
                    TimeSpan tsDiferencia = dtFechaActual.Subtract(dtFechaBD);

                    if (tsDiferencia.Days >= intDias)
                        booResult = false;
                }
                else
                    booResult = false;
            }
            else
                booResult = false;

            return booResult;
        }

        /// <summary>
        /// Encripta la contraseña de usuario y la compara con la contraseña de la BD
        /// </summary>
        /// <param name="strPassUser"></param>
        /// <param name="strPassBD"></param>
        /// <returns></returns>
        public bool mtdCompararContrasenasEncriptadas(string strPassUser, string strPassBD)
        {
            #region Variables
            bool booResult = true;
            string strPassUserCrypt = string.Empty;
            #endregion Variables

            if (strPassUser != strPassBD)
            {
                strPassUserCrypt = mtdEncriptarContrasena(strPassUser);

                /*if (strPassUserCrypt != strPassBD)*/
                int intValue = string.Compare(strPassUserCrypt, strPassBD);

                if (intValue != 0)
                    booResult = false;
            }
            return booResult;
        }

        private string mtdEncriptarContrasena(string strCadena)
        {
            string strResult = string.Empty;

            cEncrypt.EncryptionKey = "ab48495fdjk4950dj39405fk";//"ab48495fdjk4950dj39405fk"
            cEncrypt.mtdInClearText = strCadena;
            cEncrypt.mtdEncrypt();
            strResult = (cEncrypt.CryptedText).Replace("'", "''");


            return strResult;
        }

        private string mtdDesEncrptarContrasena(string strCadena)
        {
            string strResult = string.Empty;

            cEncrypt.EncryptionKey = "ab48495fdjk4950dj39405fk";
            cEncrypt.CryptedText = strCadena;
            cEncrypt.mtdDecrypt();
            strResult = (cEncrypt.CryptedText).Replace("''", "'");

            return strResult;
        }

        public void mtdAuthCambioContrasena(string strNombreRol, int intIdUsuario,
            string strNombreUsuario, string strLoginUsuario, string strIdRol, string strIdJerarquia)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(strNombreRol, false);
            IdUsuario = intIdUsuario;
            NombreUsuario = strNombreUsuario;
            LoginUsuario = strLoginUsuario;
            IdRol = strIdRol;
            IdJerarquia = strIdJerarquia;
        }

        public bool ReestablecerContrasena(string strContrasena, string strIdUsuario)
        {
            string strConsulta = string.Empty, strPassEncrypted = string.Empty;
            bool booResult = true;

            try
            {
                strPassEncrypted = mtdEncriptarContrasena(strContrasena);
                /*strConsulta = string.Format("UPDATE Listas.Usuarios SET Contrasena = N'{0}', FechaUltActualPassword = DATEADD(MM, -1, GETDATE()) WHERE (IdUsuario = {1})",
                    strPassEncrypted, strIdUsuario);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);*/
                #region Creacion Consulta
                parameters = new OleDbParameter[2];
                parameter = new OleDbParameter("@Contraseña", OleDbType.VarChar);
                parameter.Value = strPassEncrypted;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@Usuario", OleDbType.VarChar);
                parameter.Value = strIdUsuario;
                parameters[1] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("Listas.SpRestablecerContraseña", parameters);
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public object mtdFechaCaducidadContrasena()
        {
            DataTable dtInformacion = new DataTable();
            object objResult = null;

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT FechaUltActualPassword AS FechaUltActualPassword FROM Listas.Usuarios WHERE (IdUsuario = " + IdUsuario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }

            objResult = dtInformacion.Rows[0]["FechaUltActualPassword"];

            return objResult;
        }
        #endregion

        public static void mtdActualizarLogin(int intLogin)
        {
        }

        public bool mtdBloquearUsuario(int intEstado, string strIdUsuario)
        {
            bool booResult = false;
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("UPDATE Listas.Usuarios SET ActualizarPassword = {0} WHERE (IdUsuario = {1})", intEstado, strIdUsuario);
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }

            return booResult;
        }

        public bool mtdEstaBloqueado()
        {
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                strConsulta = string.Format("SELECT ActualizarPassword FROM Listas.Usuarios WHERE (IdUsuario = {0})", IdUsuario);
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();

                if (dtInformacion.Rows[0][0].ToString() == "True")
                    booResult = true;
                else
                    booResult = false;

            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }

            return booResult;
        }

        public DataTable mtdUsuarioBloqueado(string Usuario, string Contrasena)
        {
            #region Variables
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty, strEncryptPass = string.Empty;
            #endregion Variables

            try
            {
                strEncryptPass = mtdEncriptarContrasena(Contrasena);
                strConsulta = "SELECT LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuario, LU.IdUsuario, LTRIM(RTRIM(LR.NombreRol)) AS NombreRol, LTRIM(RTRIM(LU.Usuario)) AS Usuario, LR.IdRol, LU.IdJerarquia, LU.IdMacroProcesoU, LU.IdProcesoU, LU.VerTodosProcesos, LU.FechaUltActualPassword, LU.Bloqueado FROM Listas.Usuarios LU INNER JOIN Listas.Roles LR ON LU.IdRol = LR.IdRol WHERE (LU.Usuario = N'" + Usuario + "') AND (LU.Contrasena COLLATE Modern_Spanish_CS_AS = N'" + strEncryptPass + "') AND (LU.Login = 0)";
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public bool mtdConsultarUsuario(string strUsuario, string strNroId, ref DataTable dtInformacion, ref string strErrMsg)
        {
            bool booResult = false;
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("SELECT PDJO.NombreResponsable, PDJO.CorreoResponsable, LU.IdUsuario FROM [Parametrizacion].[JerarquiaOrganizacional] PJO " +
                    "INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] PDJO ON PJO.idHijo = PDJO.idHijo " +
                    "INNER JOIN [Listas].[Usuarios] LU ON LU.IdJerarquia = PJO.idHijo " +
                    "WHERE LU.NumeroDocumento = '{0}' AND LU.Usuario = '{1}'", strNroId, strUsuario);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);

                if (dtInformacion != null && dtInformacion.Rows.Count > 0)
                    booResult = true;
            }
            catch (Exception ex)
            {
                dtInformacion = null;
                strErrMsg = "No existe información para el Usuario : "+ strUsuario;
            }
            finally
            {
                cDataBase.desconectar();
            }
            return booResult;
        }

        public DataTable mtdConsultarCorreo(int intIdEvento)
        {
            string strErrMsg = string.Empty;
            string strConsulta = string.Format("SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, " +
                "CD.AJefeMediato, E.RequiereFechaCierre " +
                "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                "WHERE E. IdEvento = {0}", intIdEvento);

            DataTable dtInfo = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los correos a notificar. [{0}]", ex.Message);
                dtInfo = null;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInfo;
        }

        public DataTable mtdConsultarIdJerarquia(string strIdUsuario)
        {
            string strErrMsg = string.Empty;
            string strConsulta = string.Format("SELECT PJO.idHijo FROM [Listas].[Usuarios] LU INNER JOIN [Parametrizacion].[JerarquiaOrganizacional] PJO ON LU.IdJerarquia = PJO.idHijo " +
                "WHERE LU.IdUsuario = {0}", strIdUsuario);

            DataTable dtInfo = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la jerarquía del usuario. [{0}]", ex.Message);
                dtInfo = null;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInfo;
        }

        public DataTable mtdConsultarCorreoDestinatario(string strNodoJerarquia)
        {
            string strErrMsg = string.Empty;
            string strConsulta = string.Format("SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = {0}", strNodoJerarquia);

            DataTable dtInfo = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la correo del destinatario. [{0}]", ex.Message);
                dtInfo = null;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInfo;
        }

        public void mtdInsertLogSenalManual(string IdSenal, string CodigoSenal, string Descripcionsenal, string fechainicial,
            string fechafinal, int numerocoincidencias, string idusuario)
        {
            string strErrMsg = string.Empty;
            string strConsulta = string.Empty;
            strConsulta = string.Format("insert into Perfiles.tblSenalAlerta_ConsulSenales (IdSenal,CodigoSenal,Descripcionsenal,fechainicial," +
                    "fechafinal, numerocoincidencias,idusuario) values (" + IdSenal + ", '" + CodigoSenal + "', '" + Descripcionsenal + "', '" + fechainicial + "','" +
                    fechafinal + "'," + numerocoincidencias + "," + idusuario + ")");
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al registrar la consulta. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }
        }

        public DataTable loadPeriodoAnual()
        {
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();

            try
            {
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta("SELECT distinct DATEPART (yy,FechaRegistro) as FechaRegistro from dbo.vwIndicador");
                cDatabase.desconectar();
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public String loadDetallePeriodo(string IdDetalle)
        {
            clsDatabase cDatabase = new clsDatabase();
            string strErrMsg = string.Empty;
            string strConsulta = string.Empty;
            strConsulta = string.Format("select IdDetallePeriodo from [Procesos].[tblDetalleVariable] where Id = " + IdDetalle + "");

            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return strConsulta;

        }

    }
}