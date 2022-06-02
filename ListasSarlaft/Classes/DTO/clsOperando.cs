using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsOperando
    {
        private string _Nombre;
        private int _IdTipoOperacion;
        private int _Id;
        private DateTime _FechaRegistro;
        private int _IdUsuario;

        public string strNombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }

        public int intIdTipoOperacion
        {
            get
            {
                return _IdTipoOperacion;
            }

            set
            {
                _IdTipoOperacion = value;
            }
        }

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

        public DateTime dtFechaRegistro
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

        #region
        public clsOperando()
        {

        }

        public clsOperando(int intId, string strNombre, int intIdTipoOperacion, int _IdUsuario, DateTime dtFechaRegistro)
        {
            this.strNombre = strNombre;
            this.intIdTipoOperacion = intIdTipoOperacion;
            this.intId = intId;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}