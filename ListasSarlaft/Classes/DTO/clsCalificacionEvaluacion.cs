using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCalificacionEvaluacion
    {
        private int _Id;
        private int _IdConfiguracionEvaluacion;
        private int _IdEvaluacion;
        private string _NombreEvaluacion;
        private string _Descripcion;
        private decimal _ValorMinimo;
        private decimal _ValorMaximo;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdConfiguracionEvaluacion
        {
            get { return _IdConfiguracionEvaluacion; }
            set { _IdConfiguracionEvaluacion = value; }
        }

        public int intIdEvaluacion
        {
            get { return _IdEvaluacion; }
            set { _IdEvaluacion = value; }
        }

        public string strNombreEvaluacion
        {
            get { return _NombreEvaluacion; }
            set { _NombreEvaluacion = value; }
        }

        public decimal intValorMinimo
        {
            get { return _ValorMinimo; }
            set { _ValorMinimo = value; }
        }

        public decimal intValorMaximo
        {
            get { return _ValorMaximo; }
            set { _ValorMaximo = value; }
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

        #region Constructors
        public clsCalificacionEvaluacion()
        {
        }

        public clsCalificacionEvaluacion(int intId, decimal intValorMinimo, decimal intValorMaximo, string strDescripcion, string strNombreEvaluacion)
        {
            this.intId = intId;
            this.intValorMinimo = intValorMinimo;
            this.intValorMaximo = intValorMaximo;
            this.strDescripcion = strDescripcion;
            this.strNombreEvaluacion = strNombreEvaluacion;
        }

        public clsCalificacionEvaluacion(int intId, int intIdConfiguracionEvaluacion, int intIdEvaluacion,
            decimal intValorMinimo, decimal intValorMaximo, string strDescripcion, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdConfiguracionEvaluacion = intIdConfiguracionEvaluacion;
            this.intIdEvaluacion = intIdEvaluacion;
            this.intValorMinimo = intValorMinimo;
            this.intValorMaximo = intValorMaximo;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsCalificacionEvaluacion(int intId, int intIdConfiguracionEvaluacion, int intIdEvaluacion, string strNombreEvaluacion,
            decimal intValorMinimo, decimal intValorMaximo, string strDescripcion, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdConfiguracionEvaluacion = intIdConfiguracionEvaluacion;
            this.intIdEvaluacion = intIdEvaluacion;
            this.strNombreEvaluacion = strNombreEvaluacion;
            this.intValorMinimo = intValorMinimo;
            this.intValorMaximo = intValorMaximo;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }

        #endregion
    }
}