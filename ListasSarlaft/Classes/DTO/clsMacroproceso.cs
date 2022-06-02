using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsMacroproceso
    {
        private int _Id;
        private string _NombreMacroproceso;
        private string _Descripcion;
        private string _Objetivo;
        private bool _Estado;
        private int _Responsable;
        private int _IdCadenaDeValor;
        private int _IdUsuario;
        private string _FechaRegistro;
        private string _NombreResponsable;
        private string _NombreUsuario;
        private string _NombreCadenaValor;

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

        public string strNombreMacroproceso
        {
            get
            {
                return _NombreMacroproceso;
            }

            set
            {
                _NombreMacroproceso = value;
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

        public string strObjetivo
        {
            get
            {
                return _Objetivo;
            }

            set
            {
                _Objetivo = value;
            }
        }

        public bool booEstado
        {
            get
            {
                return _Estado;
            }

            set
            {
                _Estado = value;
            }
        }

        public int intCargoResponsable
        {
            get
            {
                return _Responsable;
            }

            set
            {
                _Responsable = value;
            }
        }

        public int intIdCadenaDeValor
        {
            get
            {
                return _IdCadenaDeValor;
            }

            set
            {
                _IdCadenaDeValor = value;
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

        public string strNombreResponsable
        {
            get { return _NombreResponsable; }
            set { _NombreResponsable = value; }
        }
        
        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        public string strNombreCadenaValor
        {
            get { return _NombreCadenaValor; }
            set { _NombreCadenaValor = value; }
        }

        #region Constructors
        public clsMacroproceso()
        {

        }

        public clsMacroproceso(int intId, string strNombreMacroproceso, string strDescripcion, string strObjetivo, bool booEstado,
            int intCargoResponsable, int intIdCadenaDeValor, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreMacroproceso = strNombreMacroproceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.booEstado = booEstado;
            this.intCargoResponsable = intCargoResponsable;
            this.intIdCadenaDeValor = intIdCadenaDeValor;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }

        public clsMacroproceso(int intId, string strNombreMacroproceso, string strDescripcion, string strObjetivo, bool booEstado,
            int intCargoResponsable, int intIdCadenaDeValor, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro,
            string strNombreCadenaValor, string strNombreResponsable)
        {
            this.intId = intId;
            this.strNombreMacroproceso = strNombreMacroproceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.booEstado = booEstado;
            this.intCargoResponsable = intCargoResponsable;
            this.intIdCadenaDeValor = intIdCadenaDeValor;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.strNombreCadenaValor = strNombreCadenaValor;
            this.strNombreResponsable = strNombreResponsable;
            this.strNombreUsuario = strNombreUsuario; 
        }
        #endregion
    }
}