using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.OleDb;

namespace clsLogica
{
    public class clsCuenta
    {
        #region Vars
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        private clsDatabase cDataBase = new clsDatabase();
        private clsError cError = new clsError();
        private clsEncriptacion cEncrypt = new clsEncriptacion();
        #endregion Vars

        #region Properties
        private string strMapPath;

        public string StrMapPath
        {
            get { return strMapPath; }
            set { strMapPath = value; }
        }
        #endregion Properties

        public clsCuenta()
        {
            this.StrMapPath = System.Web.HttpContext.Current.Server.MapPath("~/Archivos/Error/Error.txt");
        }

        public void notAuthenticated(int intIdUsuario)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            //intIdUsuario = 0;
        }

        public void isAuthenticated(string strNombreRol, int intIdUsuario, string strNombreUsuario, string strLoginUsuario, string strIdRol, string strIdJerarquia)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(strNombreRol, false);
            //IdUsuario = idUsuario;
            //NombreUsuario = nombreUsuario;
            //LoginUsuario = loginUsuario;
            //IdRol = idRol;
            //IdJerarquia = idJerarquia;
        }

        public void actualizarRol(string NombreRol, string IdRol)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
        }

        public void agregarRol(string NombreRol)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public DataTable ContarUsuarios(string IdRol)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public DataTable ContarUsuariosEvento(string IdRol)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public DataTable Login_SN(string IdUsuario, int Login)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public DataTable MensajeLogin(string Usuario)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuario, LU.Contrasena, LU.Bloqueado, LU.login, LU.FechaUltActualPassword, LU.IdUsuario, LTRIM(RTRIM(LR.NombreRol)) AS NombreRol, LTRIM(RTRIM(LU.Usuario)) AS Usuario, LR.IdRol, LU.IdJerarquia, LU.IdMacroProcesoU, LU.IdProcesoU, LU.VerTodosProcesos FROM Listas.Usuarios LU INNER JOIN Listas.Roles LR ON LU.IdRol = LR.IdRol WHERE Usuario = N'" + 
                    Usuario + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public String contrasenaUsuario(int IdUsuario)
        {
            DataTable dtInformacion = new DataTable();
            string strResult = string.Empty;

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT LTRIM(RTRIM(Contrasena)) AS Contrasena FROM Listas.Usuarios WHERE (IdUsuario = " + 
                    IdUsuario.ToString() + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }

            if (IdUsuario != 0)
                strResult = dtInformacion.Rows[0]["Contrasena"].ToString();

            return strResult;
        }

        public void actualizarContrasena(int IdUsuario, string Contrasena)
        {
            string strConsulta = string.Empty, strPassEncrypted = string.Empty;

            try
            {
                strPassEncrypted = mtdEncriptarContrasena(Contrasena);
                strConsulta = "UPDATE Listas.Usuarios SET Contrasena = N'" + strPassEncrypted + "' , FechaUltActualPassword = GETDATE() WHERE (IdUsuario = " + 
                    IdUsuario.ToString() + ")";
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
        }

        public void updateUser(string IdRol, string NumeroDocumento, string Nombres, string Apellidos, string Usuario,
            string Contrasena, string Bloqueado, string UsuarioId, string IdJerarquia)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.Usuarios SET IdRol = " + IdRol.Trim() + ", NumeroDocumento = N'" + 
                    NumeroDocumento.Trim() + "', Nombres = N'" + Nombres.Trim() + "', Apellidos = N'" + 
                    Apellidos.Trim() + "', Usuario = N'" + Usuario.Trim() + "', Contrasena = N'" + 
                    Contrasena.Trim() + "', Bloqueado = " + Bloqueado.Trim() + ", IdJerarquia = " + 
                    IdJerarquia + ", FechaUltActualPassword = GETDATE() WHERE (IdUsuario = " + 
                    UsuarioId.ToString() + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
        }

        public DataTable ConsultarUsuario(string NumeroDocumento, string Nombres, string Apellidos)
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
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Listas.Usuarios.IdUsuario, Listas.Roles.IdRol, Listas.Roles.NombreRol, Listas.Usuarios.NumeroDocumento, Listas.Usuarios.Nombres, Listas.Usuarios.Apellidos, Listas.Usuarios.Usuario, Listas.Usuarios.Bloqueado, Listas.Usuarios.Contrasena, Listas.Usuarios.IdJerarquia, Parametrizacion.JerarquiaOrganizacional.NombreHijo, Listas.Usuarios.IdMacroProcesoU, Listas.Usuarios.IdProcesoU, Listas.Usuarios.VerTodosProcesos, FechaUltActualPassword FROM Listas.Usuarios INNER JOIN Listas.Roles ON Listas.Usuarios.IdRol = Listas.Roles.IdRol INNER JOIN Parametrizacion.JerarquiaOrganizacional ON Listas.Usuarios.IdJerarquia = Parametrizacion.JerarquiaOrganizacional.idHijo " + 
                    condicion);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public void actualizarPermisos(string Consultar, string Agregar, string Actualizar, string Borrar, string IdPermiso)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.Permisos SET Consultar = " + Consultar + ", Agregar = " + 
                    Agregar + ", Actualizar = " + Actualizar + ", Borrar = " + Borrar + 
                    " WHERE (IdPermiso = " + IdPermiso + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
        }

        public void registerUser(string IdRol, string NumeroDocumento, string Nombres, string Apellidos, string Usuario,
            string Contrasena, string Bloqueado, string IdJerarquia)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
        }

        public DataTable validateUser(string Usuario)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public DataTable loadInfoPermisos(string IdRol)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Listas.Permisos.IdPermiso, Listas.Formularios.NombreModulo, Listas.Formularios.NombreSubModulo, Listas.Formularios.NombreFormulario, (CASE WHEN Listas.Permisos.Consultar = 0 THEN 'False' ELSE 'True' END) AS Consultar, (CASE WHEN Listas.Permisos.Agregar = 0 THEN 'False' ELSE 'True' END) AS Agregar, (CASE WHEN Listas.Permisos.Actualizar = 0 THEN 'False' ELSE 'True' END) AS Actualizar, (CASE WHEN Listas.Permisos.Borrar = 0 THEN 'False' ELSE 'True' END) AS Borrar FROM Listas.Permisos INNER JOIN Listas.Formularios ON Listas.Permisos.IdFormulario = Listas.Formularios.IdFormulario WHERE (Listas.Permisos.IdRol = " + 
                    IdRol + ") ORDER BY Listas.Formularios.IdFormulario");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
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
                strConsulta = "SELECT LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuario, LU.IdUsuario, LTRIM(RTRIM(LR.NombreRol)) AS NombreRol, LTRIM(RTRIM(LU.Usuario)) AS Usuario, LR.IdRol, LU.IdJerarquia, LU.IdMacroProcesoU, LU.IdProcesoU, LU.VerTodosProcesos, LU.FechaUltActualPassword FROM Listas.Usuarios LU INNER JOIN Listas.Roles LR ON LU.IdRol = LR.IdRol WHERE (LU.Usuario = N'" + Usuario + "') AND (LU.Contrasena COLLATE Modern_Spanish_CS_AS = N'" + 
                    strEncryptPass + "') AND (LU.Bloqueado = 0)";
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }

            return dtInformacion;
        }

        public void ResetContrasena(string IdUsuario)
        {
            string strConsulta = string.Empty, strContrasenaEncriptada = string.Empty;

            try
            {
                strContrasenaEncriptada = mtdEncriptarContrasena("Sherlock+"); //Vieja contraseña sherlock2012
                strConsulta = string.Format("UPDATE Listas.Usuarios SET Contrasena = N'{0}', FechaUltActualPassword = GETDATE() WHERE (IdUsuario = {1})", 
                    strContrasenaEncriptada, IdUsuario);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
        }

        public String permisosConsulta(int IdUsuario, int IdRol, string IdFormulario)
        {
            string strResultado = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                if (mtdEstaBloqueado(IdUsuario))
                    strResultado = "False";
                else
                {
                    cDataBase.conectar();
                    dtInformacion = cDataBase.ejecutarConsulta("SELECT Consultar FROM Listas.Permisos WHERE (IdRol = " + 
                        IdRol.ToString() + ") AND (IdFormulario = " + IdFormulario + ")");
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return strResultado;
        }

        public String permisosAgregar(int IdRol, string IdFormulario)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Agregar FROM Listas.Permisos WHERE (IdRol = " + 
                    IdRol.ToString() + ") AND (IdFormulario = " + IdFormulario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }

            return dtInformacion.Rows[0][0].ToString().Trim();
        }

        public String permisosActualizar(int IdRol, string IdFormulario)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Actualizar FROM Listas.Permisos WHERE (IdRol = " + 
                    IdRol + ") AND (IdFormulario = " + IdFormulario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }

            return dtInformacion.Rows[0][0].ToString().Trim();
        }

        public String permisosBorrar(int IdRol, string IdFormulario)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Borrar FROM Listas.Permisos WHERE (IdRol = " + 
                    IdRol + ") AND (IdFormulario = " + IdFormulario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion.Rows[0][0].ToString().Trim();
        }

        public String NombreDetalleJerarquia(string idHijo)
        {
            DataTable dtInformacion = new DataTable();

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT NombreResponsable FROM Parametrizacion.DetalleJerarquiaOrg WHERE (idHijo = " + 
                    idHijo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }

            if (dtInformacion.Rows.Count > 0)
                return dtInformacion.Rows[0][0].ToString().Trim();
            else
                return string.Empty;
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
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
        public void mtdActualizarUsuario(string IdRol, string NumeroDocumento, string Nombres, string Apellidos, string Usuario, string Contrasena,
            string Bloqueado, string UsuarioId, string IdJerarquia, string IdMacroproceso, string IdProceso, bool booTodosProcesos)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public bool mtdActualizarLoginSN(int IdUsuario, int intLogin)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            bool booResult = true;

            try
            {
                strConsulta = "UPDATE Listas.Usuarios SET Login = " + intLogin + " WHERE IdUsuario = " + IdUsuario;
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
                booResult = false;
            }

            return booResult;
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
            strResult = cEncrypt.CryptedText;

            return strResult;
        }

        private string mtdDesEncrptarContrasena(string strCadena)
        {
            string strResult = string.Empty;

            cEncrypt.EncryptionKey = "ab48495fdjk4950dj39405fk";
            cEncrypt.CryptedText = strCadena;
            cEncrypt.mtdDecrypt();
            strResult = cEncrypt.CryptedText;

            return strResult;
        }

        public bool mtdBloquearUsuario(int intEstado, string strIdUsuario)
        {
            bool booResult = false;
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("UPDATE Listas.Usuarios SET ActualizarPassword = {0} WHERE (IdUsuario = {1})", 
                    intEstado, strIdUsuario);
                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }

            return booResult;
        }

        public bool mtdEstaBloqueado(int IdUsuario)
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
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }

            return booResult;
        }

        public void mtdAuthCambioContrasena(string strNombreRol, int intIdUsuario,
            string strNombreUsuario, string strLoginUsuario, string strIdRol, string strIdJerarquia)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(strNombreRol, false);
            //IdUsuario = intIdUsuario;
            //NombreUsuario = strNombreUsuario;
            //LoginUsuario = strLoginUsuario;
            //IdRol = strIdRol;
            //IdJerarquia = strIdJerarquia;
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
                strConsulta = "SELECT LTRIM(RTRIM(ISNULL(LU.Nombres,''))) + ' ' + LTRIM(RTRIM(ISNULL(LU.Apellidos,''))) AS NombreUsuario, LU.IdUsuario, LTRIM(RTRIM(LR.NombreRol)) AS NombreRol, LTRIM(RTRIM(LU.Usuario)) AS Usuario, LR.IdRol, LU.IdJerarquia, LU.IdMacroProcesoU, LU.IdProcesoU, LU.VerTodosProcesos, LU.FechaUltActualPassword, LU.Bloqueado FROM Listas.Usuarios LU INNER JOIN Listas.Roles LR ON LU.IdRol = LR.IdRol WHERE (LU.Usuario = N'" + Usuario + "') AND (LU.Contrasena COLLATE Modern_Spanish_CS_AS = N'" + 
                    strEncryptPass + "') AND (LU.Login = 0)";
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }
            return dtInformacion;
        }

        public object mtdFechaCaducidadContrasena(int IdUsuario)
        {
            DataTable dtInformacion = new DataTable();
            object objResult = null;

            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT FechaUltActualPassword AS FechaUltActualPassword FROM Listas.Usuarios WHERE (IdUsuario = " + 
                    IdUsuario + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(this.StrMapPath, ex.Message + ", " + ex.StackTrace);
            }

            objResult = dtInformacion.Rows[0]["FechaUltActualPassword"];

            return objResult;
        }
    }
}
