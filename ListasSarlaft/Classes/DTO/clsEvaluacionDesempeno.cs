using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsEvaluacionDesempeno
    {
        private int _Id;
        private string _Nombre;
        private int _CargoResponsable;
        private string _Cargo;
        private string _FechaEvaluacion;
        private string _Evaluador;
        private decimal _Calificacion;
        private string _RecomendacionCapacitacion;
        private string _RecomendacionCompromisos;
        private string _Otros;
        private string _DescripcionOtros;
        private string _FechaProximaEvaluacion;
        private int _IdUsuario;
        private string _Usuario;
        private DateTime _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public int intCargoResponsable
        {
            get { return _CargoResponsable; }
            set { _CargoResponsable = value; }
        }

        public string strCargo
        {
            get { return _Cargo; }
            set { _Cargo = value; }
        }

        public string dtFechaEvaluacion
        {
            get { return _FechaEvaluacion; }
            set { _FechaEvaluacion = value; }
        }

        public string strEvaluador
        {
            get { return _Evaluador; }
            set { _Evaluador = value; }
        }

        public decimal intCalificacion
        {
            get { return _Calificacion; }
            set { _Calificacion = value; }
        }

        public string strRecomendacionCapacitacion
        {
            get { return _RecomendacionCapacitacion; }
            set { _RecomendacionCapacitacion = value; }
        }

        public string strRecomendacionCompromisos
        {
            get { return _RecomendacionCompromisos; }
            set { _RecomendacionCompromisos = value; }
        }

        public string strOtros
        {
            get { return _Otros; }
            set { _Otros = value; }
        }

        public string strDescripcionOtros
        {
            get { return _DescripcionOtros; }
            set { _DescripcionOtros = value; }
        }

        public string dtFechaProximaEvaluacion
        {
            get { return _FechaProximaEvaluacion; }
            set { _FechaProximaEvaluacion = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string strUsuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsEvaluacionDesempeno()
        {
        }

        public clsEvaluacionDesempeno(int intId, string strNombre, int intCargoResponsable, string strCargo, string dtFechaEvaluacion,
            string strEvaluador, decimal intCalificacion, string strRecomendacionCapacitacion, string strRecomendacionCompromisos,
            string strOtros, string strDescripcionOtros, string dtFechaProximaEvaluacion, int intIdUsuario, string strUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombre = strNombre;
            this.intCargoResponsable = intCargoResponsable;
            this.strCargo = strCargo;
            this.dtFechaEvaluacion = dtFechaEvaluacion;
            this.strEvaluador = strEvaluador;
            this.intCalificacion = intCalificacion;
            this.strRecomendacionCapacitacion = strRecomendacionCapacitacion;
            this.strRecomendacionCompromisos = strRecomendacionCompromisos;
            this.strOtros = strOtros;
            this.strDescripcionOtros = strDescripcionOtros;
            this.dtFechaProximaEvaluacion = dtFechaProximaEvaluacion;
            this.strUsuario = strUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}