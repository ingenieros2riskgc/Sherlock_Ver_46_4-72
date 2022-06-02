using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOFrecuenciavsEventos
    {
        #region Variables
        private int _IdFrecuenciaEventos;
        private int _EventosMaximos;
        private int _CodigoFrecuencia;
        private string _NombreFrecuencia;
        private int _IdUsuario;
        private string _Usuario;
        private DateTime _FechaRegistro;
        #endregion Variables
        #region Get/Set
        public int intIdFrecuenciaEventos
        {
            get { return _IdFrecuenciaEventos; }
            set { _IdFrecuenciaEventos = value; }
        }
        public int intEventosMaximos
        {
            get { return _EventosMaximos; }
            set { _EventosMaximos = value; }
        }
        public int intCodigoFrecuencia
        {
            get { return _CodigoFrecuencia; }
            set { _CodigoFrecuencia = value; }
        }
        public string strNombreFrecuencia
        {
            get { return _NombreFrecuencia; }
            set { _NombreFrecuencia = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string strUsuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        #endregion
        #region Constructor
        public clsDTOFrecuenciavsEventos() { }
        #endregion Constructor
    }
}