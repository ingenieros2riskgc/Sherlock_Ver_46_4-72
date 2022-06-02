using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsEvaluacionProveedor
    {
        private int _Id;
        private string _NombreProveedor;
        private string _FechaEvaluacion;
        private string _PeriodoEvaluadoInicial;
        private string _PeriodoEvaluadoFinal;
        private string _ServicioOfrecido;
        private string _RealizadoPor;
        private int _CargoResponsable;
        private string _NombreResponsable;
        private string _Observaciones;
        private int _IdUsuario;
        private string _Usuario;
        private DateTime _FechaRegistro;
        private string _FechaProximaEvaluacion;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombreProveedor
        {
            get { return _NombreProveedor; }
            set { _NombreProveedor = value; }
        }

        public int intCargoResponsable
        {
            get { return _CargoResponsable; }
            set { _CargoResponsable = value; }
        }

        public string strNombreResponsable
        {
            get { return _NombreResponsable; }
            set { _NombreResponsable = value; }
        }

        public string dtFechaEvaluacion
        {
            get { return _FechaEvaluacion; }
            set { _FechaEvaluacion = value; }
        }

        public string dtPeriodoEvaluadoInicial
        {
            get { return _PeriodoEvaluadoInicial; }
            set { _PeriodoEvaluadoInicial = value; }
        }

        public string dtPeriodoEvaluadoFinal
        {
            get { return _PeriodoEvaluadoFinal; }
            set { _PeriodoEvaluadoFinal = value; }
        }

        public string strServicioOfrecido
        {
            get { return _ServicioOfrecido; }
            set { _ServicioOfrecido = value; }
        }

        public string strRealizadoPor
        {
            get { return _RealizadoPor; }
            set { _RealizadoPor = value; }
        }

        public string strObservaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
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

        public string dtFechaProximaEvaluacion
        {
            get { return _FechaProximaEvaluacion; }
            set { _FechaProximaEvaluacion = value; }
        }

        #region Constructors
        public clsEvaluacionProveedor()
        {
        }

        public clsEvaluacionProveedor(int intId, string strNombreProveedor, int intCargoResponsable, string strNombreResponsable, string dtFechaEvaluacion,
            string dtPeriodoEvaluadoInicial, string dtPeriodoEvaluadoFinal, string strServicioOfrecido, string strRealizadoPor,
            string strObservaciones, int intIdUsuario, string strUsuario, DateTime dtFechaRegistro, string dtFechaProximaEvaluacion)
        {
            this.intId = intId;
            this.strNombreProveedor = strNombreProveedor;
            this.intCargoResponsable = intCargoResponsable;
            this.strNombreResponsable = strNombreResponsable;
            this.dtFechaEvaluacion = dtFechaEvaluacion;
            this.dtPeriodoEvaluadoInicial = dtPeriodoEvaluadoInicial;
            this.dtPeriodoEvaluadoFinal = dtPeriodoEvaluadoFinal;
            this.strServicioOfrecido = strServicioOfrecido;
            this.strRealizadoPor = strRealizadoPor;
            this.strObservaciones = strObservaciones;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strUsuario = strUsuario;
            this.dtFechaProximaEvaluacion = dtFechaProximaEvaluacion;
        }
        #endregion
    }
}