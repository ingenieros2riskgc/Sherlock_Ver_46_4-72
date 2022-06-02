using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOriesgosIndicadores
    {
        #region Variables
        private int _IdRiesgoIndicador;
        private string _NombreIndicador;
        private string _ObjetivoIndicador;
        private int _IProcesoIndicador;
        private int _IdProceso;
        private string _NombreProceso;
        private int _IdResponsableMedicion;
        private string _ResponsableMedicion;
        private int _IdFrecuenciaMedicion;
        private string _FrecuenciaMedicion;
        private string _DescripcionFrecuencia;
        private int _IdRiesgoAsociado;
        private string _CodRiesgo;
        private string _NombreRiesgo;
        private int _IdFormula;
        private string _Nominador;
        private string _Denominador;
        private int _IdMeta;
        private double _Meta;
        private double _Resultado;
        private int _IdEsquemaSeguimiento;
        private string _DescripcionSeguimiento;
        private string _Color;
        private int _Activo;
        private string _Año;
        private string _Mes;
        private int _UsuarioCreacion;
        private string _Usuario;
        private DateTime _FechaCreacion;
        private int _porcentaje;
        #endregion Variables
        #region Get/Set
        public int intIdRiesgoIndicador
        {
            get { return _IdRiesgoIndicador; }
            set { _IdRiesgoIndicador = value; }
        }
        public string strNombreIndicador
        {
            get { return _NombreIndicador; }
            set { _NombreIndicador = value; }
        }
        public string strObjetivoIndicador
        {
            get { return _ObjetivoIndicador; }
            set { _ObjetivoIndicador = value; }
        }
        public int intIProcesoIndicador
        {
            get { return _IProcesoIndicador; }
            set { _IProcesoIndicador = value; }
        }
        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }
        public string strNombreProceso
        {
            get { return _NombreProceso; }
            set { _NombreProceso = value; }
        }
        public int intIdResponsableMedicion
        {
            get { return _IdResponsableMedicion; }
            set { _IdResponsableMedicion = value; }
        }
        public string strResponsableMedicion
        {
            get { return _ResponsableMedicion; }
            set { _ResponsableMedicion = value; }
        }
        public int intIdFrecuenciaMedicion
        {
            get { return _IdFrecuenciaMedicion; }
            set { _IdFrecuenciaMedicion = value; }
        }
        public string strFrecuenciaMedicion
        {
            get { return _FrecuenciaMedicion; }
            set { _FrecuenciaMedicion = value; }
        }
        public string strDescripcionFrecuencia
        {
            get { return _DescripcionFrecuencia; }
            set { _DescripcionFrecuencia = value; }
        }
        public int intIdRiesgoAsociado
        {
            get { return _IdRiesgoAsociado; }
            set { _IdRiesgoAsociado = value; }
        }
        public string strCodRiesgo
        {
            get { return _CodRiesgo; }
            set { _CodRiesgo = value; }
        }
        public string strNombreRiesgo
        {
            get { return _NombreRiesgo; }
            set { _NombreRiesgo = value; }
        }
        public int intIdFormula
        {
            get { return _IdFormula; }
            set { _IdFormula = value; }
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
        public int intIdMeta
        {
            get { return _IdMeta; }
            set { _IdMeta = value; }
        }
        public double dblMeta
        {
            get { return _Meta; }
            set { _Meta = value; }
        }
        public double dblResultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }
        public int intIdEsquemaSeguimiento
        {
            get { return _IdEsquemaSeguimiento; }
            set { _IdEsquemaSeguimiento = value; }
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
        public int booActivo
        {
            get { return _Activo; }
            set { _Activo = value; }
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
        public int booPorcentaje
        {
            get { return _porcentaje; }
            set { _porcentaje = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOriesgosIndicadores() { }
        #endregion Constructor
    }
}