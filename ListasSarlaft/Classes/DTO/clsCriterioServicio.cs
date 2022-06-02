using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsCriterioServicio
    {
        private int _IdCriterioServicio;
        private decimal _RangoInicial;
        private decimal _RangoFinal;
        private string _Descripcion;
        private int _IdUsuario;
        private string _NombreUsuario;
        private DateTime _FechaRegistro;

        public int intIdCriterioServicio
        {
            get { return _IdCriterioServicio; }
            set { _IdCriterioServicio = value; }
        }

        public decimal intRangoInicial
        {
            get { return _RangoInicial; }
            set { _RangoInicial = value; }
        }
        public decimal intRangoFinal
        {
            get { return _RangoFinal; }
            set { _RangoFinal = value; }
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

        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        public clsCriterioServicio()
        {
        }
    }
}