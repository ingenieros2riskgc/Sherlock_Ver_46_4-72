using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsSubproceso
    {
        private int _Id;
        private string _NombreSubproceso;
        private string _Descripcion;
        private string _Objetivo;
        private bool _Estado;
        private int _IdCargoResponsable;
        private string _CargoResponsable;
        private int _IdProceso;
        private string _NombreProceso;
        private int _IdMacroProceso;
        private string _NombreMacroProceso;
        private int _IdCadenaValor;
        private string _NombreCadenaValor;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombreSubproceso
        {
            get { return _NombreSubproceso; }
            set { _NombreSubproceso = value; }
        }

        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string strObjetivo
        {
            get { return _Objetivo; }
            set { _Objetivo = value; }
        }

        public bool booEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public int intIdCargoResponsable
        {
            get { return _IdCargoResponsable; }
            set { _IdCargoResponsable = value; }
        }

        public string strCargoResponsable
        {
            get { return _CargoResponsable; }

            set { _CargoResponsable = value; }
        }

        public int intIdProceso
        {
            get
            {
                return _IdProceso;
            }

            set
            {
                _IdProceso = value;
            }
        }

        public string strNombreProceso
        {
            get { return _NombreProceso; }

            set { _NombreProceso = value; }
        }

        public int intIdMacroProceso
        {
            get { return _IdMacroProceso; }
            set { _IdMacroProceso = value; }
        }

        public string strNombreMacroProceso
        {
            get { return _NombreMacroProceso; }
            set { _NombreMacroProceso = value; }
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
        public clsSubproceso()
        {

        }

        public clsSubproceso(int intId, string strNombreSubproceso, string strDescripcion, string strObjetivo, bool booEstado,
            int intIdCargoResponsable, int intIdProceso, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreSubproceso = strNombreSubproceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.booEstado = booEstado;
            this.intIdCargoResponsable = intIdCargoResponsable;
            this.intIdProceso = intIdProceso;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsSubproceso(int intId, string strNombreSubproceso, string strDescripcion, string strObjetivo, bool booEstado,
            int intIdCargoResponsable, string strCargoResponsable, int intIdProceso, string strNombreProceso, int intIdMacroProceso, int intIdCadenaValor,
            int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreSubproceso = strNombreSubproceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.booEstado = booEstado;
            this.intIdCargoResponsable = intIdCargoResponsable;
            this.strCargoResponsable = strCargoResponsable;
            this.intIdProceso = intIdProceso;
            this.strNombreProceso = strNombreProceso;
            this.intIdMacroProceso = intIdMacroProceso;
            this.intIdCadenaValor = intIdCadenaValor;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }

        public clsSubproceso(int intId, string strNombreSubproceso, string strDescripcion, string strObjetivo, bool booEstado,
            int intIdCargoResponsable, string strCargoResponsable, int intIdProceso, string strNombreProceso,
            int intIdMacroProceso, string strNombreMacroProceso, int intIdCadenaValor, string strNombreCadenaValor,
            int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreSubproceso = strNombreSubproceso;
            this.strDescripcion = strDescripcion;
            this.strObjetivo = strObjetivo;
            this.booEstado = booEstado;
            this.intIdCargoResponsable = intIdCargoResponsable;
            this.strCargoResponsable = strCargoResponsable;
            this.intIdProceso = intIdProceso;
            this.strNombreProceso = strNombreProceso;
            this.intIdMacroProceso = intIdMacroProceso;
            this.strNombreMacroProceso = strNombreMacroProceso;
            this.intIdCadenaValor = intIdCadenaValor;
            this.strNombreCadenaValor = strNombreCadenaValor;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }
        #endregion
    }
}