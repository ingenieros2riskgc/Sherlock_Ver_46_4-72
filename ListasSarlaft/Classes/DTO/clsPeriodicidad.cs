using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsPeriodicidad
    {
        private int _Id;
        private string _Nombre;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _NombreUsuario;

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

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        #region Constructors
        public clsPeriodicidad()
        {
        }

        public clsPeriodicidad(int intId, string strNombre, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombre = strNombre;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsPeriodicidad(int intId, string strNombre, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombre = strNombre;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }
        #endregion
    }

    public class clsDetallePeriodo
    {
        private int _Id;
        private string _Nombre;
        private int _IdPeriodo;
        private int _Posicion;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;

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

        public int intIdPeriodo
        {
            get { return _IdPeriodo; }
            set { _IdPeriodo = value; }
        }

        public int intPosicion
        {
            get { return _Posicion; }
            set { _Posicion = value; }
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
        public clsDetallePeriodo()
        {
        }

        public clsDetallePeriodo(int intId, string strNombre, int intIdPeriodo, int intPosicion, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombre = strNombre;
            this.intIdPeriodo = intIdPeriodo;
            this.intPosicion = intPosicion;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }

        public clsDetallePeriodo(int intId, string strNombre, int intIdPeriodo, int intPosicion, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombre = strNombre;
            this.intIdPeriodo = intIdPeriodo;
            this.intPosicion = intPosicion;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.strNombreUsuario = strNombreUsuario;
        }
        #endregion
    }
}