using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsDetalleVariable
    {
        private int _Id;
        private int _IdVariable;
        private int _Valor;
        private string _FechaRegistro;
        private int _IdUsuario;
        private int _IdIndicador;
        private int _IdDetPeriodo;
        private string _PeriodoAnual;

        //private string _FechaDesde;
        //private string _FechaHasta;

        //public string strFechaDesde
        //{
        //    get { return _FechaDesde; }
        //    set { _FechaDesde = value; }
        //}

        //public string strFechaHasta
        //{
        //    get { return _FechaHasta; }
        //    set { _FechaHasta = value; }
        //}

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdVariable
        {
            get { return _IdVariable; }
            set { _IdVariable = value; }
        }

        public int intValor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }

        public int intIdDetallePeriodo
        {
            get { return _IdDetPeriodo; }
            set { _IdDetPeriodo = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public string intPeriodoAnual
        {
            get { return _PeriodoAnual; }
            set { _PeriodoAnual = value; }
        }

        public string IdDetalleVariable { get; set; }

        public bool Flag { get; set; }

        #region Constructors
        public clsDetalleVariable()
        {
        }

        public clsDetalleVariable(int intId, int intIdVariable, int intValor, int intIdUsuario,
            int intIdIndicador, int intIdDetallePeriodo, string intPeriodoAnual, string dtFechaRegistro, string idDetalleVariable, bool flag)
        {
            this.intId = intId;
            this.intIdVariable = intIdVariable;
            this.intValor = intValor;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.intIdIndicador = intIdIndicador;
            this.intIdDetallePeriodo = intIdDetallePeriodo;
            this.IdDetalleVariable = idDetalleVariable;
            this.intPeriodoAnual = intPeriodoAnual;
            this.Flag = flag;
        }
        #endregion
    }
}