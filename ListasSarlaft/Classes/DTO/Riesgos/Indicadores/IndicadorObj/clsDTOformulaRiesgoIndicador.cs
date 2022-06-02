using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOformulaRiesgoIndicador
    {
        #region Variables
        private int _IdFormula;
        private int _IdRiesgoIndicador;
        private string _Nominador;
        private string _Denominador;
        private int _UsuarioCreacion;
        private string _Usuario;
        private DateTime _FechaCreacion;
        private int _Porcentaje;
        #endregion Variables
        #region Get/Set
        public int intIdFormula
        {
            get { return _IdFormula; }
            set { _IdFormula = value; }
        }
        public int intIdRiesgoIndicador
        {
            get { return _IdRiesgoIndicador; }
            set { _IdRiesgoIndicador = value; }
        }
        public string strNominador
        {
            get { return _Nominador; }
            set { _Nominador = value; }
        }
        public string strDenominador
        {
            get { return _Denominador; }
            set { _Denominador = value; }
        }
        public int intUsuarioCreacion
        {
            get { return _UsuarioCreacion; }
            set { _UsuarioCreacion = value; }
        }
        public string strUsuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public DateTime dtFechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }
        public int intPorcentaje
        {
            get { return _Porcentaje; }
            set { _Porcentaje = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOformulaRiesgoIndicador() { }
        #endregion Constructor
    }
}