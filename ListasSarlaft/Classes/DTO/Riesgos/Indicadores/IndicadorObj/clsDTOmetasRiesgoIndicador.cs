using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOmetasRiesgoIndicador
    {
        #region Variables
        private int _IdMeta;
        private int _IdRiesgoIndicador;
        private double _Meta;
        private string _Año;
        private string _Mes;
        private int _IdFrecuenciaMedicion;
        private string _DetalleFrecuencia;
        private string _ValorOtraFrecuencia;
        private int _UsuarioCreacion;
        private string _Usuario;
        private DateTime _FechaCreacion;
        #endregion Variables
        #region Get/Set
        public int intIdMeta
        {
            get { return _IdMeta; }
            set { _IdMeta = value; }
        }
        public int intIdRiesgoIndicador
        {
            get { return _IdRiesgoIndicador; }
            set { _IdRiesgoIndicador = value; }
        }
        public double dblMeta
        {
            get { return _Meta; }
            set { _Meta = value; }
        }
        public string strAño
        {
            get { return _Año; }
            set { _Año = value; }
        }
        public string strMes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }
        public int intIdDetalleFrecuencia
        {
            get { return _IdFrecuenciaMedicion; }
            set { _IdFrecuenciaMedicion = value; }
        }
        public string strDetalleFrecuencia
        {
            get { return _DetalleFrecuencia; }
            set { _DetalleFrecuencia = value; }
        }
        public string strValorOtraFrecuencia
        {
            get { return _ValorOtraFrecuencia; }
            set { _ValorOtraFrecuencia = value; }
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
        public clsDTOmetasRiesgoIndicador() { }
        #endregion Constructor
    }
}