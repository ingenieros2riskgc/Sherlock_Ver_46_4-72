using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsProveedorCriterios
    {
        private int _Id;
        private string _NombreAspecto;
        private decimal _ValorPorcentaje;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _DesCriterio;
        private string _DesParametro;
        private string _NombreUsuario;
        private decimal _ValorPuntaje;
        private decimal _Calificacion;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombreAspecto
        {
            get { return _NombreAspecto; }
            set { _NombreAspecto = value; }
        }

        public decimal intValorPorcentaje
        {
            get { return _ValorPorcentaje; }
            set { _ValorPorcentaje = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string strDesCriterio
        {
            get { return _DesCriterio; }
            set { _DesCriterio = value; }
        }

        public string strDesParametro
        {
            get { return _DesParametro; }
            set { _DesParametro = value; }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        public decimal intValorPuntaje
        {
            get { return _ValorPuntaje; }
            set { _ValorPuntaje = value; }
        }

        public decimal intCalificacion
        {
            get { return _Calificacion; }
            set { _Calificacion = value; }
        }
        #region Constructors
        public clsProveedorCriterios()
        {
        }

        public clsProveedorCriterios(int intId, string strNombreAspecto, decimal intValorPorcentaje, string dtFechaRegistro,
            int intIdUsuario, string strDesCriterio, string strDesParametro, string strNombreUsuario, decimal intValorPuntaje)
        {
            this.intId = intId;
            this.strNombreAspecto = strNombreAspecto;
            this.intValorPorcentaje = intValorPorcentaje;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strDesCriterio = strDesCriterio;
            this.strDesParametro = strDesParametro;
            this.strNombreUsuario = strNombreUsuario;
            this.intValorPuntaje = intValorPuntaje;
        }
        public clsProveedorCriterios(int intId, string strNombreAspecto, decimal intValorPorcentaje, string dtFechaRegistro,
            int intIdUsuario, string strDesCriterio, string strDesParametro, string strNombreUsuario, decimal intValorPuntaje, decimal intCalificacion)
        {
            this.intId = intId;
            this.strNombreAspecto = strNombreAspecto;
            this.intValorPorcentaje = intValorPorcentaje;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strDesCriterio = strDesCriterio;
            this.strDesParametro = strDesParametro;
            this.strNombreUsuario = strNombreUsuario;
            this.intValorPuntaje = intValorPuntaje;
            this.intCalificacion = intCalificacion;
        }
        #endregion
    }
}