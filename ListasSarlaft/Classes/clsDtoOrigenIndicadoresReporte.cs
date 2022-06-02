using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDtoOrigenIndicadoresReporte
    {
        #region Variables
        private int _IdRiesgoIndicador;
        private string _NombreIndicador;
        private string _ObjetivoIndicador;
        private string _ResponsableMedicion;
        private string _FrecuenciaMedicion;
        private string _DescripcionFrecuencia;
        private string _CodRiesgo;
        private string _NombreRiesgo;
        private double _Meta;
        private double _Resultado;
        private string _DescripcionSeguimiento;
        private string _Año;
        private string _Mes;
        #endregion Variables
        #region Get/Set
        public int Codigo
        {
            get { return _IdRiesgoIndicador; }
            set { _IdRiesgoIndicador = value; }
        }
        public string NombreIndicador
        {
            get { return _NombreIndicador; }
            set { _NombreIndicador = value; }
        }
        public string ObjetivoIndicador
        {
            get { return _ObjetivoIndicador; }
            set { _ObjetivoIndicador = value; }
        }

        public string ResponsableMedicion
        {
            get { return _ResponsableMedicion; }
            set { _ResponsableMedicion = value; }
        }

        public string FrecuenciaMedicion
        {
            get { return _FrecuenciaMedicion; }
            set { _FrecuenciaMedicion = value; }
        }

        public string DescripcionFrecuencia
        {
            get { return _DescripcionFrecuencia; }
            set { _DescripcionFrecuencia = value; }
        }

        public double Meta
        {
            get { return _Meta; }
            set { _Meta = value; }
        }

        public string Año
        {
            get { return _Año; }
            set { _Año = value; }

        }

        public string Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        public double Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }


        public string DescripcionSeguimiento
        {
            get { return _DescripcionSeguimiento; }
            set { _DescripcionSeguimiento = value; }
        }

       
        public string CodRiesgo
        {
            get { return _CodRiesgo; }
            set { _CodRiesgo = value; }
        }
        public string NombreRiesgo
        {
            get { return _NombreRiesgo; }
            set { _NombreRiesgo = value; }
        }
       
        #endregion Get/Set
        #region Constructor
        public clsDtoOrigenIndicadoresReporte() { }
        #endregion Constructor
    }
}