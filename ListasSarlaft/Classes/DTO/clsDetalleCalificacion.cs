using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsDetalleCalificacion
    {
        private int _Id;

        private int _IdIndicador;
        
        private decimal _ValorMin;
        private decimal _ValorMax;

        private int _IdSemaforo;
        private string _NombreSemaforo;

        private string  _NombreCumplimiento;

        private int _IdUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }

        public decimal intValorMin
        {
            get { return _ValorMin; }
            set { _ValorMin = value; }
        }

        public decimal intValorMax
        {
            get { return _ValorMax; }
            set { _ValorMax = value; }
        }

        public int intIdSemaforo
        {
            get { return _IdSemaforo; }
            set { _IdSemaforo = value; }
        }

        public string strNombreSemaforo
        {
            get { return _NombreSemaforo; }
            set { _NombreSemaforo = value; }
        }

        public string strNombreCumplimiento
        {
            get { return _NombreCumplimiento; }
            set { _NombreCumplimiento = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsDetalleCalificacion()
        {
        }

        public clsDetalleCalificacion(int intId, int intIdIndicador, decimal intValorMin, decimal intValorMax,
            int intIdSemaforo, string strNombreCumplimiento, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdIndicador = intIdIndicador;
            this.intValorMin = intValorMin;
            this.intValorMax = intValorMax;
            this.intIdSemaforo = intIdSemaforo;
            this.strNombreCumplimiento = strNombreCumplimiento;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }

        public clsDetalleCalificacion(int intId, int intIdIndicador, decimal intValorMin, decimal intValorMax,
           int intIdSemaforo, string strNombreSemaforo, string strNombreCumplimiento, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdIndicador = intIdIndicador;
            this.intValorMin = intValorMin;
            this.intValorMax = intValorMax;
            this.intIdSemaforo = intIdSemaforo;
            this.strNombreSemaforo = strNombreSemaforo;
            this.strNombreCumplimiento = strNombreCumplimiento;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }

        #endregion
    }
}