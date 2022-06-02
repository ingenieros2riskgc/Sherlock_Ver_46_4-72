using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsControlPropiedadCP
    {
        private int _IdCrlPropiedad;
        private string _Descripcion;
        private string _Caracteristicas;
        private string _ProveedorCliente;
        private string _FechaIngreso;
        private string _FechaSalida;
        private string _Observaciones;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        private string _Usuario;
        private string _Nombre;
        public int intIdCrlPropiedad
        {
            get { return _IdCrlPropiedad; }
            set { _IdCrlPropiedad = value; }
        }
        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public string strCaracteristicas
        {
            get { return _Caracteristicas; }
            set { _Caracteristicas = value; }
        }
        public string strProveedorCliente
        {
            get { return _ProveedorCliente; }
            set { _ProveedorCliente = value; }
        }
        public string dtFechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
        }
        public string dtFechaSalida
        {
            get { return _FechaSalida; }
            set { _FechaSalida = value; }
        }
        public string strObservaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }
        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
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
        public string strNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public clsControlPropiedadCP() { }
    }
}