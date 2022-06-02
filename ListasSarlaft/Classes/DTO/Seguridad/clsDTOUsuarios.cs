using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOUsuarios
    {
        #region Variables
        private int _Id;
        private int _IdRol;
        private string _NombreRol;
        private string _NumeroDocumento;
        private string _Nombres;
        private string _Apellidos;
        private string _Usuario;
        private string _ContrasenaEncriptada;
        private int _Bloqueado;
        private int _IdJerarquia;
        private string _NombreHijo;
        private string _Cargo;
        private int _IdMacroprocesoU;
        private int _IdProcesoU;
        private int _VerTodosProcesos;
        private DateTime _FechaUltActualPassword;
        private int _Login;
        private int _ActualizarPassword;
        private DateTime _FechaUltLogin;
        #endregion Variables
        #region Get/Set
        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int intIdRol
        {
            get { return _IdRol; }
            set { _IdRol = value; }
        }
        public string strNombreRol
        {
            get { return _NombreRol; }
            set { _NombreRol = value; }
        }
        public string strNumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }
        public string strNombres
        {
            get { return _Nombres; }
            set { _Nombres = value; }
        }
        public string strApellidos
        {
            get { return _Apellidos; }
            set { _Apellidos = value; }
        }
        public string strUsuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public string strContrasenaEncriptada
        {
            get { return _ContrasenaEncriptada; }
            set { _ContrasenaEncriptada = value; }
        }
        public int intBloqueado
        {
            get { return _Bloqueado; }
            set { _Bloqueado = value; }
        }
        public int intIdJerarquia
        {
            get { return _IdJerarquia; }
            set { _IdJerarquia = value; }
        }
        public string strNombreHijo
        {
            get { return _NombreHijo; }
            set { _NombreHijo = value; }
        }
        public string strCargo
        {
            get { return _Cargo; }
            set { _Cargo = value; }
        }
        public int intIdMacroprocesoU
        {
            get { return _IdMacroprocesoU; }
            set { _IdMacroprocesoU = value; }
        }
        public int intIdProcesoU
        {
            get { return _IdProcesoU; }
            set { _IdProcesoU = value; }
        }
        public int intVerTodosProcesos
        {
            get { return _VerTodosProcesos; }
            set { _VerTodosProcesos = value; }
        }
        public DateTime dtFechaUltActualPassword
        {
            get { return _FechaUltActualPassword; }
            set { _FechaUltActualPassword = value; }
        }
        public int intActualizarPassword
        {
            get { return _ActualizarPassword; }
            set { _ActualizarPassword = value; }
        }
        public DateTime dtFechaUltLogin
        {
            get { return _FechaUltLogin; }
            set { _FechaUltLogin = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOUsuarios() { }
        #endregion Constructor
    }
}