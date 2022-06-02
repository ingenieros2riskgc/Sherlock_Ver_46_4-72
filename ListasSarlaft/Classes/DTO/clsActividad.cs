using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsActividad
    {
        private int _Id;
        private string _Descripcion;
        private bool _Estado;
        private int _CargoResponsable;
        private string _NombreCargoResponsable;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _NombreUsuario;
        private int _idphva;
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

        public bool booEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public int intCargoResponsable
        {
            get { return _CargoResponsable; }
            set { _CargoResponsable = value; }
        }

        public string strNombreCargoResponsable
        {
            get { return _NombreCargoResponsable; }
            set { _NombreCargoResponsable = value; }
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

        public int intIdphva
        {
            get { return _idphva; }
            set { _idphva = value; }
        }

        #region Construtors
        public clsActividad()
        {
        }

        public clsActividad(int intId, string strDescripcion, bool booEstado,
            int intCargoResponsable, int intIdUsuario, string dtFechaRegistro, int intIdphva)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.booEstado = booEstado;
            this.intCargoResponsable = intCargoResponsable;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.intIdphva = intIdphva;
        }

        public clsActividad(int intId, string strDescripcion, bool booEstado,
            int intCargoResponsable, string strNombreCargoResponsable, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro, int intIdphva)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.booEstado = booEstado;
            this.intCargoResponsable = intCargoResponsable;
            this.strNombreCargoResponsable = strNombreCargoResponsable;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.intIdphva = intIdphva;
        }
        #endregion
    }
}