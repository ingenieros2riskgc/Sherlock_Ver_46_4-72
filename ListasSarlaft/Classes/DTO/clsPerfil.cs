using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsPerfil
    {
        private int _Id;
        private int _IdJerarquiaOrganizacional;
        private string _NombreJOrganizacional;
        private int _IdMacroproceso;
        private string _ResumenCargo;
        private string _Funciones;
        private string _Rol;
        private string _Perfil;
        private string _Educacion;
        private string _Habilidades;
        private string _Formacion;
        private string _Experiencia;
        private bool _Estado;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _NombreUsuario;

        public string Codigo { get; set; }

        public int EstadoPerfil { get; set; }

        public string CorreoUsuario { get; set; }

        public int IdJerarquiaAprueba { get; set; }

        public string NombreJerarquiaAprueba { get; set; }

        public int IdJerarquiaUsuario { get; set; }

        public int intIdJOrganizacional
        {
            get
            {
                return _IdJerarquiaOrganizacional;
            }

            set
            {
                _IdJerarquiaOrganizacional = value;
            }
        }

        public string strNombreJOrganizacional
        {
            get { return _NombreJOrganizacional; }
            set { _NombreJOrganizacional = value; }
        }

        public int intId
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public string strResumenCargo
        {
            get { return _ResumenCargo; }
            set { _ResumenCargo = value; }
        }

        public string strPerfil
        {
            get
            {
                return _Perfil;
            }

            set
            {
                _Perfil = value;
            }
        }

        public bool booEstado
        {
            get
            {
                return _Estado;
            }

            set
            {
                _Estado = value;
            }
        }

        public string strRol
        {
            get
            {
                return _Rol;
            }

            set
            {
                _Rol = value;
            }
        }

        public string strHabilidades
        {
            get { return _Habilidades; }
            set { _Habilidades = value; }
        }

        public string strEducacion
        {
            get
            {
                return _Educacion;
            }

            set
            {
                _Educacion = value;
            }
        }

        public string strFormacion
        {
            get
            {
                return _Formacion;
            }

            set
            {
                _Formacion = value;
            }
        }

        public string strExperiencia
        {
            get
            {
                return _Experiencia;
            }

            set
            {
                _Experiencia = value;
            }
        }

        public string strFunciones
        {
            get
            {
                return _Funciones;
            }

            set
            {
                _Funciones = value;
            }
        }

        public string dtFechaRegistro
        {
            get
            {
                return _FechaRegistro;
            }

            set
            {
                _FechaRegistro = value;
            }
        }

        public int intIdUsuario
        {
            get
            {
                return _IdUsuario;
            }

            set
            {
                _IdUsuario = value;
            }
        }

        public int intIdMacroproceso
        {
            get
            {
                return _IdMacroproceso;
            }

            set
            {
                _IdMacroproceso = value;
            }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        #region Constructors
        public clsPerfil()
        {

        }

        public clsPerfil(int _intId, int _intIdJerarquiaOrganizacional, int _intIdMacroproceso, int _intIdUsuario, int estadoDocumento, string codigo, string _strPerfil, int IdJerarquiaAprueba)
        {
            intId = _intId;
            intIdJOrganizacional = _intIdJerarquiaOrganizacional;
            intIdMacroproceso = _intIdMacroproceso;
            intIdUsuario = _intIdUsuario;
            EstadoPerfil = estadoDocumento;
            Codigo = codigo;
            strPerfil = _strPerfil;
            this.IdJerarquiaAprueba = IdJerarquiaAprueba;
        }

        public clsPerfil(int intId, int intIdJerarquiaOrganizacional, string strResumenCargo, string strPerfil, int estadoDocumento,
            string strRol, string strHabilidades, string strEducacion, string strFormacion, string strExperiencia, string strFunciones,
            int intIdMacroproceso, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdJOrganizacional = intIdJerarquiaOrganizacional;
            this.strResumenCargo = strResumenCargo;
            this.strPerfil = strPerfil;
            EstadoPerfil = estadoDocumento;
            this.strRol = strRol;
            this.strHabilidades = strHabilidades;
            this.strEducacion = strEducacion;
            this.strFormacion = strFormacion;
            this.strExperiencia = strExperiencia;
            this.strFunciones = strFunciones;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.intIdMacroproceso = intIdMacroproceso;
        }

        public clsPerfil(int intId, int intIdJerarquiaOrganizacional, string strNombreJOrganizacional, string strResumenCargo, string strPerfil, int estadoDocumento,
            string strRol, string strHabilidades, string strEducacion, string strFormacion, string strExperiencia, string strFunciones,
            int intIdMacroproceso, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro, string correoUsuario, string codigo, int IdJerarquiaAprueba, 
            int IdJerarquiaUsuario, string NombreJerarquiaAprueba)
        {
            this.intId = intId;
            this.intIdJOrganizacional = intIdJerarquiaOrganizacional;
            this.strNombreJOrganizacional = strNombreJOrganizacional;
            this.strResumenCargo = strResumenCargo;
            this.strPerfil = strPerfil;
            EstadoPerfil = estadoDocumento;
            CorreoUsuario = correoUsuario;
            this.strRol = strRol;
            this.strHabilidades = strHabilidades;
            this.strEducacion = strEducacion;
            this.strFormacion = strFormacion;
            this.strExperiencia = strExperiencia;
            this.strFunciones = strFunciones;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.intIdMacroproceso = intIdMacroproceso;
            this.IdJerarquiaAprueba = IdJerarquiaAprueba;
            this.IdJerarquiaUsuario = IdJerarquiaUsuario;
            this.NombreJerarquiaAprueba = NombreJerarquiaAprueba;
            Codigo = codigo;
        }
        #endregion
    }
}