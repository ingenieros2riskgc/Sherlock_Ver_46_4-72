using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsVariable
    {
        private int _Id;
        private string _Descripcion;
        private string _Formato;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;
        private bool _Estado;
        private int _IdDetalleVariable;
        private decimal _Valor;
        private int _IdDetallePeriodo;
        private string _NombreDetPeriodo;
        private int _IdIndicador;
        private string _PeriodoAnual;

        public int intId
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public string strDescripcion
        {
            get
            {
                return _Descripcion;
            }

            set
            {
                _Descripcion = value;
            }
        }

        public string strFormato
        {
            get { return _Formato; }
            set { _Formato = value; }
        }

        public int intIdUsuario
        {
            get
            {
                return _IdUsuario;
            }

            set
            {
                _IdUsuario = value;
            }
        }

        public string dtFechaRegistro
        {
            get
            {
                return _FechaRegistro;
            }

            set
            {
                _FechaRegistro = value;
            }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        public bool booEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public decimal decValor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        public int intIdDetallePeriodo
        {
            get { return _IdDetallePeriodo; }
            set { _IdDetallePeriodo = value; }
        }

        public string strNombreDetPeriodo
        {
            get { return _NombreDetPeriodo; }
            set { _NombreDetPeriodo = value; }
        }

        public int intIdDetalleVariable
        {
            get { return _IdDetalleVariable; }
            set { _IdDetalleVariable = value; }
        }

        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }

        public string strPeriodoAnual
        {
            get { return _PeriodoAnual; }
            set { _PeriodoAnual = value; }
        }

        #region Constructors
        public clsVariable()
        {

        }

        public clsVariable(int intId, string strDescripcion, string strFormato, bool booEstado, int intIdUsuario,
            string dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.strFormato = strFormato;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.booEstado = booEstado;
        }

        public clsVariable(int intId, string strDescripcion, string strFormato, bool booEstado, int intIdUsuario,
           string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.strFormato = strFormato;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.strNombreUsuario = strNombreUsuario;
            this.booEstado = booEstado;
        }

        public clsVariable(int intId, string strDescripcion, string strFormato, bool booEstado, int intIdUsuario, string strNombreUsuario,
           int intIdDetalleVariable, decimal decValor, int intIdDetallePeriodo, string strNombreDetPeriodo, string strPeriodoAnual, int intIdIndicador, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.strFormato = strFormato;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.strNombreUsuario = strNombreUsuario;
            this.booEstado = booEstado;
            this.intIdIndicador = intIdIndicador;
            this.intIdDetalleVariable = intIdDetalleVariable;
            this.decValor = decValor;
            this.intIdDetallePeriodo = intIdDetallePeriodo;
            this.strNombreDetPeriodo = strNombreDetPeriodo;
            this.strPeriodoAnual = strPeriodoAnual;
        }
        #endregion
    }
}