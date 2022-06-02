using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsFactoresDesempeno
    {
        private int _Id;
        private string _FactoresEvaluacion;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strFactoresEvaluacion
        {
            get { return _FactoresEvaluacion; }
            set { _FactoresEvaluacion = value; }
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

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        #region Constructors
        public clsFactoresDesempeno()
        {
        }

        public clsFactoresDesempeno(int intId, string strFactoresEvaluacion, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strFactoresEvaluacion = strFactoresEvaluacion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsFactoresDesempeno(int intId, string strFactoresEvaluacion, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strFactoresEvaluacion = strFactoresEvaluacion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }
        #endregion
    }

    public class clsDetalleFactorDesempeno
    {
        private int _Id;
        private int _IdFactoresDesempeno;
        private string _NombreFactor;
        private int _IdCalificacion;
        private string _NombreCalificacion;
        private string _Descripcion;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;
        private decimal _CriterioMinimo;
        private decimal _CriterioMaximo;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdFactoresDesempeno
        {
            get { return _IdFactoresDesempeno; }
            set { _IdFactoresDesempeno = value; }
        }

        public string strNombreFactor
        {
            get { return _NombreFactor; }
            set { _NombreFactor = value; }
        }

        public int intIdCalificacion
        {
            get { return _IdCalificacion; }
            set { _IdCalificacion = value; }
        }

        public string strNombreCalificacion
        {
            get { return _NombreCalificacion; }
            set { _NombreCalificacion = value; }
        }

        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public decimal decCriterioMinimo
        {
            get { return _CriterioMinimo; }
            set { _CriterioMinimo = value; }
        }

        public decimal decCriterioMaximo
        {
            get { return _CriterioMaximo; }
            set { _CriterioMaximo = value; }
        }
        
        #region Constructors
        public clsDetalleFactorDesempeno()
        {
        }

        public clsDetalleFactorDesempeno(int intId, int intIdFactoresDesempeno, int intIdCalificacion, string strDescripcion, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdFactoresDesempeno = intIdFactoresDesempeno;
            this.intIdCalificacion = intIdCalificacion;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsDetalleFactorDesempeno(int intId, int intIdFactoresDesempeno, int intIdCalificacion, string strDescripcion, int intIdUsuario, string dtFechaRegistro
            ,decimal decCriterioMinimo, decimal decCriterioMaximo)
        {
            this.intId = intId;
            this.intIdFactoresDesempeno = intIdFactoresDesempeno;
            this.intIdCalificacion = intIdCalificacion;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.decCriterioMinimo = decCriterioMinimo;
            this.decCriterioMaximo = decCriterioMaximo;
        }

        public clsDetalleFactorDesempeno(int intId, int intIdFactoresDesempeno, string strNombreFactor, int intIdCalificacion, string strNombreCalificacion,
            string strDescripcion, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro, decimal decCriterioMinimo, decimal decCriterioMaximo)
        {
            this.intId = intId;
            this.intIdFactoresDesempeno = intIdFactoresDesempeno;
            this.strNombreFactor = strNombreFactor;
            this.intIdCalificacion = intIdCalificacion;
            this.strNombreCalificacion = strNombreCalificacion;
            this.strDescripcion = strDescripcion;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.decCriterioMinimo = decCriterioMinimo;
            this.decCriterioMaximo = decCriterioMaximo;
        }
        #endregion
    }
}