using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOseguimientoRiesgoIndicador
    {
        #region Variables
        private int _IdEsquemaSeguimiento;
        private int _IdRiesgoIndicador;
        private double _ValorMinimo;
        private double _ValorMaximo;
        private string _DescripcionSeguimiento;
        private string _Color;
        private int _UsuarioCreacion;
        private string _Usuario;
        private DateTime _FechaCreacion;
        #endregion Variables
        #region Get/Set
        public int intIdEsquemaSeguimiento
        {
            get { return _IdEsquemaSeguimiento; }
            set { _IdEsquemaSeguimiento = value; }
        }
        public int intIdRiesgoIndicador
        {
            get { return _IdRiesgoIndicador; }
            set { _IdRiesgoIndicador = value; }
        }
        public double dblValorMinimo
        {
            get { return _ValorMinimo; }
            set { _ValorMinimo = value; }
        }
        public double dblValorMaximo
        {
            get { return _ValorMaximo; }
            set { _ValorMaximo = value; }
        }
        public string strDescripcionSeguimiento
        {
            get { return _DescripcionSeguimiento; }
            set { _DescripcionSeguimiento = value; }
        }
        public string strColor
        {
            get { return _Color; }
            set { _Color = value; }
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
        #endregion Get/Set
        #region Constructor
        public clsDTOseguimientoRiesgoIndicador() { }
        #endregion Constructor
    }
}