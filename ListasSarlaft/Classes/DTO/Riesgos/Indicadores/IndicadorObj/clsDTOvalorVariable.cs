using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOvalorVariable
    {
        #region Variables
        private int _IdValorVariable;
        private int _IdVariable;
        private int _IdFrecuencia;
        private string _FrecuenciaMedicion;
        private string _valorFrecuencia;
        private double _ValorVariable;
        private int _IdRiesgoIndicador;
        private int _IdDetalleFrecuencia;
        private string _DescripcionDetalle;
        private string _Año;
        private string _Mes;
        #endregion Variables
        #region Get/Set
        public int intIdValorVariable
        {
            get { return _IdValorVariable; }
            set { _IdValorVariable = value; }
        }
        public int intIdVariable
        {
            get { return _IdVariable; }
            set { _IdVariable = value; }
        }
        public int intIdFrecuencia
        {
            get { return _IdFrecuencia; }
            set { _IdFrecuencia = value; }
        }
        public string strFrecuenciaMedicion
        {
            get { return _FrecuenciaMedicion; }
            set { _FrecuenciaMedicion = value; }
        }
        public string strValorFrecuencia
        {
            get { return _valorFrecuencia; }
            set { _valorFrecuencia = value; }
        }
        public double dblValorVariable
        {
            get { return _ValorVariable; }
            set { _ValorVariable = value; }
        }
        public int intIdRiesgoIndicador
        {
            get { return _IdRiesgoIndicador; }
            set { _IdRiesgoIndicador = value; }
        }
        public int intIdDetalleFrecuencia
        {
            get { return _IdDetalleFrecuencia; }
            set { _IdDetalleFrecuencia = value; }
        }
        public string strDescripcionDetalle
        {
            get { return _DescripcionDetalle; }
            set { _DescripcionDetalle = value; }
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
        #endregion Get/Set
        #region Constructor
        public clsDTOvalorVariable() { }
        #endregion Constructor
    }
}