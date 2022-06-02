using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsMatrizIndicadores
    {
        private string _PoliticaCalidad;
        private int _IdIndicador;
        private string _DescripcionObjetivo;
        private string _NombreIndicador;
        private string _DescripcionInicador;
        private string _ProcesoIndicador;
        private decimal _Meta;
        private string _NombrePeriodo;
        private string _Formula;
        private DataTable _Cuadro;
        private int _IdPeriodicidad;
        private int _IdProcesoIndicador;
        private int _MetasCumplidas;
        private int _MetasIncumplidas;
        private string _ArrayPeriodo;
        private string _ArrayResultado;
        private string _Responsable;
        public string strPoliticaCalidad
        {
            get { return _PoliticaCalidad; }
            set { _PoliticaCalidad = value; }
        }

        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }

        public string strDescripcionObjetivo
        {
            get { return _DescripcionObjetivo; }
            set { _DescripcionObjetivo = value; }
        }

        public string strNombreIndicador
        {
            get { return _NombreIndicador; }
            set { _NombreIndicador = value; }
        }

        public string strDescripcionInicador
        {
            get { return _DescripcionInicador; }
            set { _DescripcionInicador = value; }
        }

        public string strProcesoIndicador
        {
            get { return _ProcesoIndicador; }
            set { _ProcesoIndicador = value; }
        }

        public decimal intMeta
        {
            get { return _Meta; }
            set { _Meta = value; }
        }

        public string strNombrePeriodo
        {
            get { return _NombrePeriodo; }
            set { _NombrePeriodo = value; }
        }
        public string strFormula
        {
            get { return _Formula; }
            set { _Formula = value; }
        }

        public DataTable dtCuadro
        {
            get { return _Cuadro; }
            set { _Cuadro = value; }
        }

        public int intIdPeriodicidad
        {
            get { return _IdPeriodicidad; }
            set { _IdPeriodicidad = value; }
        }
        public int intIdProcesoIndicador
        {
            get { return _IdProcesoIndicador; }
            set { _IdProcesoIndicador = value; }
        }
        public int intMetasCumplidas
        {
            get { return _MetasCumplidas; }
            set { _MetasCumplidas = value; }
        }
        public int intMetasIncumplidas
        {
            get { return _MetasIncumplidas; }
            set { _MetasIncumplidas = value; }
        }
        public string strArrayPeriodo
        {
            get { return _ArrayPeriodo; }
            set { _ArrayPeriodo = value; }
        }
        public string strArrayResultado
        {
            get { return _ArrayResultado; }
            set { _ArrayResultado = value; }
        }
        public string strResponsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }
        public clsMatrizIndicadores() { }
    }
}