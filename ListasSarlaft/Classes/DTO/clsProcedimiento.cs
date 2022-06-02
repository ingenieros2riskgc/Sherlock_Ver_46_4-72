using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsProcedimiento
    {
        private int _Id;
        private string _Descripcion;
        private bool _Estado;
        private int _IdActividad;
        private string _DescActividad;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _NombreUsuario;

        public int intId
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public string strDescripcion
        {
            get
            {
                return _Descripcion;
            }

            set
            {
                _Descripcion = value;
            }
        }

        public bool booEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public int intIdActividad
        {
            get
            {
                return _IdActividad;
            }

            set
            {
                _IdActividad = value;
            }
        }

        public string strDescActividad
        {
            get { return _DescActividad; }
            set { _DescActividad = value; }
        }

        public string dtFechaRegistro
        {
            get
            {
                return _FechaRegistro;
            }

            set
            {
                _FechaRegistro = value;
            }
        }

        public int intIdUsuario
        {
            get
            {
                return _IdUsuario;
            }

            set
            {
                _IdUsuario = value;
            }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        #region
        public clsProcedimiento()
        {

        }

        public clsProcedimiento(int intId, string strDescripcion, bool booEstado, int intIdActividad, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.booEstado = booEstado;
            this.intIdActividad = intIdActividad;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsProcedimiento(int intId, string strDescripcion, bool booEstado, int intIdActividad, string strDescActividad, 
            int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.booEstado = booEstado;
            this.intIdActividad = intIdActividad;
            this.strDescActividad = strDescActividad;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }
        #endregion
    }
}