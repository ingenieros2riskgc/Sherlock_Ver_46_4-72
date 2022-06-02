using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsControlPropiedadExternos
    {
        private int _Id;
        private string _Descripcion;
        private string _Caracteristicas;
        private string _Provedor;
        private DateTime _FechaIngreso;
        private DateTime _FechaSalida;
        private int _IdUsuario;
        private DateTime _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
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

        public string strProvedor
        {
            get { return _Provedor; }
            set { _Provedor = value; }
        }

        public DateTime dtFechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
        }

        public DateTime dtFechaSalida
        {
            get { return _FechaSalida; }
            set { _FechaSalida = value; }
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

        #region Constructors
        public clsControlPropiedadExternos()
        {
        }

        public clsControlPropiedadExternos(int intId, string strDescripcion, string strCaracteristicas, string strProvedor,
            DateTime dtFechaIngreso, DateTime dtFechaSalida, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.strCaracteristicas = strCaracteristicas;
            this.strProvedor = strProvedor;
            this.dtFechaIngreso = dtFechaIngreso;
            this.dtFechaSalida = dtFechaSalida;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}