using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroMandoIndicadores
    {
        #region Variables
        private int _IdRiesgoIndicador;
        private string _NombreIndicador;
        private string _ObjetivoIndicador;
        private string _Responsable;
        private int _IdRiesgoAsociado;
        private int _IdProbabilidad;
        private int _IdImpacto;
        private string _Codigo;
        private string _Nombre;
        private string _NombreRiesgo;
        private string _CadenaValor;
        private string _Macroproceso;
        private string _Proceso;
        private string _Subproceso;
        private string _color;
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
        public string strResponsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }
        public int intIdRiesgoAsociado
        {
            get { return _IdRiesgoAsociado; }
            set { _IdRiesgoAsociado = value; }
        }
        public int intIdProbabilidad
        {
            get { return _IdProbabilidad; }
            set { _IdProbabilidad = value; }
        }
        public int intIdImpacto
        {
            get { return _IdImpacto; }
            set { _IdImpacto = value; }
        }
        public string strCodigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        public string strNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string strNombreRiesgo
        {
            get { return _NombreRiesgo; }
            set { _NombreRiesgo = value; }
        }
        public string strCadenaValor
        {
            get { return _CadenaValor; }
            set { _CadenaValor = value; }
        }
        public string strMacroproceso
        {
            get { return _Macroproceso; }
            set { _Macroproceso = value; }
        }
        public string strProceso
        {
            get { return _Proceso; }
            set { _Proceso = value; }
        }
        public string strSubproceso
        {
            get { return _Subproceso; }
            set { _Subproceso = value; }
        }
        public string strColor
        {
            get { return _color; }
            set { _color = value; }
        }
        public int booPorcentaje
        {
            get { return _porcentaje; }
            set { _porcentaje = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOCuadroMandoIndicadores() { }
        #endregion Constructor
    }
}