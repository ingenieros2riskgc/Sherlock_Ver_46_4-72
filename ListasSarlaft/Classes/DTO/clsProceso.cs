using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsProceso
    {
        private int _Id;
        private int _IdEmpresa;
        private string _NombreProceso;
        private string _Descripcion;
        private string _Objetivo;
        private string _IdArea;
        private int _Responsable;
        private string _NombreResponsable;
        private int _IdMacroproceso;
        private string _NombreMacroproceso;
        private int _IdCadenaValor;
        private string _NombreCadenaValor;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;
        private bool _Estado;
        private string _ContextoInterno;
        private string _ContextoExterno;


            public string ContextoInterno
        {
            get
            {
                return _ContextoInterno;
            }

            set
            {
                _ContextoInterno = value;
            }


        }

     

        public string ContextoExterno
        {
            get
            {
                return _ContextoExterno;
            }

            set
            {
                _ContextoExterno = value;
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

        public int intIdEmpresa
        {
            get
            {
                return _IdEmpresa;
            }

            set
            {
                _IdEmpresa = value;
            }
        }

        public string strNombreProceso
        {
            get
            {
                return _NombreProceso;
            }

            set
            {
                _NombreProceso = value;
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

        public string strArea
        {
            get
            {
                return _IdArea;
            }

            set
            {
                _IdArea = value;
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

        public int intIdMacroProceso
        {
            get
            {
                return _IdMacroproceso;
            }

            set
            {
                _IdMacroproceso = value;
            }
        }

        public string strNombreMacroProceso
        {
            get { return _NombreMacroproceso; }
            set { _NombreMacroproceso = value; }
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

        public int intIdCadenaValor
        {
            get { return _IdCadenaValor; }
            set { _IdCadenaValor = value; }
        }

        public string strNombreCadenaValor
        {
            get { return _NombreCadenaValor; }
            set { _NombreCadenaValor = value; }
        }

        public string strCargoResponsable
        {
            get { return _NombreResponsable; }
            set { _NombreResponsable = value; }
        }

        #region Constructors
        public clsProceso()
        {

        }

        public clsProceso(int intId, int intIdEmpresa, string strNombreProceso, string strDescripcion, string strObjetivo, string strArea,
            int intCargoResponsable, int intIdMacroProceso, bool booEstado, int intIdUsuario, string dtFechaRegistro, string strContextoInterno, string strContextoExterno)
        {
            this.intId = intId;
            this.intIdEmpresa = intIdEmpresa;
            this.strNombreProceso = strNombreProceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.strArea = strArea;
            this.intCargoResponsable = intCargoResponsable;
            this.intIdMacroProceso = intIdMacroProceso;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.booEstado = booEstado;
            this.ContextoInterno = strContextoInterno;
            this.ContextoExterno = strContextoExterno;
        }
        public clsProceso(int intId, int intIdEmpresa, string strNombreProceso, string strDescripcion, string strObjetivo, string strArea,
           int intCargoResponsable, int intIdMacroProceso, bool booEstado, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdEmpresa = intIdEmpresa;
            this.strNombreProceso = strNombreProceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.strArea = strArea;
            this.intCargoResponsable = intCargoResponsable;
            this.intIdMacroProceso = intIdMacroProceso;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.booEstado = booEstado;

        }

        public clsProceso(int intId, int intIdEmpresa, string strNombreProceso, string strDescripcion, string strObjetivo, string strArea,
            int intCargoResponsable, string strCargoResponsable, int intIdMacroProceso, string strNombreMacroProceso, int intIdCadenaValor,
            bool booEstado, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro, string strContextoInt, string strContextoExt)
        {
            this.intId = intId;
            this.intIdEmpresa = intIdEmpresa;
            this.strNombreProceso = strNombreProceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.strArea = strArea;
            this.intCargoResponsable = intCargoResponsable;
            this.strCargoResponsable = strCargoResponsable;
            this.intIdMacroProceso = intIdMacroProceso;
            this.strNombreMacroProceso = strNombreMacroProceso;
            this.intIdCadenaValor = intIdCadenaValor;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.booEstado = booEstado;
            this.ContextoInterno = strContextoInt;
            this.ContextoExterno = strContextoExt;
        }
        public clsProceso(int intId, int intIdEmpresa, string strNombreProceso, string strDescripcion, string strObjetivo, string strArea,
           int intCargoResponsable, string strCargoResponsable, int intIdMacroProceso, string strNombreMacroProceso, int intIdCadenaValor,
           bool booEstado, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdEmpresa = intIdEmpresa;
            this.strNombreProceso = strNombreProceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.strArea = strArea;
            this.intCargoResponsable = intCargoResponsable;
            this.strCargoResponsable = strCargoResponsable;
            this.intIdMacroProceso = intIdMacroProceso;
            this.strNombreMacroProceso = strNombreMacroProceso;
            this.intIdCadenaValor = intIdCadenaValor;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.booEstado = booEstado;

        }

        public clsProceso(int intId, int intIdEmpresa, string strNombreProceso, string strDescripcion, string strObjetivo, string strArea,
           int intCargoResponsable, string strCargoResponsable, int intIdMacroProceso, string strNombreMacroProceso,
           int intIdCadenaValor, string strNombreCadenaValor, bool booEstado, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdEmpresa = intIdEmpresa;
            this.strNombreProceso = strNombreProceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.strArea = strArea;
            this.intCargoResponsable = intCargoResponsable;
            this.strCargoResponsable = strCargoResponsable;
            this.intIdMacroProceso = intIdMacroProceso;
            this.strNombreMacroProceso = strNombreMacroProceso;
            this.intIdCadenaValor = intIdCadenaValor;
            this.strNombreCadenaValor = strNombreCadenaValor;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.booEstado = booEstado;
        }
        #endregion
    }
}