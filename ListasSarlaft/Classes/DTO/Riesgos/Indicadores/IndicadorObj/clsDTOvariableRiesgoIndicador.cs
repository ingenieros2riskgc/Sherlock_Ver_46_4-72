using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOvariableRiesgoIndicador
    {
        #region Variables
        private int _IdVariableRiesgoIndicador;
        private int _IdRiesgoIndicador;
        private string _Descripcion;
        private double _ValorVariable;
        private int _IdFormato;
        private string _Formato;
        private int _UsuarioCreacion;
        private DateTime _FechaCreacion;
        #endregion Variables
        #region Get/Set
        public int intIdVariableRiesgoIndicador
        {
            get { return _IdVariableRiesgoIndicador; }
            set { _IdVariableRiesgoIndicador = value; }
        }
        public int intIdRiesgoIndicador
        {
            get { return _IdRiesgoIndicador; }
            set { _IdRiesgoIndicador = value; }
        }
        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public double dblValorVariable
        {
            get { return _ValorVariable; }
            set { _ValorVariable = value; }
        }
        public int intIdFormato
        {
            get { return _IdFormato; }
            set { _IdFormato = value; }
        }
        public string strFormato
        {
            get { return _Formato; }
            set { _Formato = value; }
        }
        public int intUsuarioCreacion
        {
            get { return _UsuarioCreacion; }
            set { _UsuarioCreacion = value; }
        }
        public DateTime dtFechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOvariableRiesgoIndicador() { }
        #endregion Constructor
    }
}