using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsControlNoConformidad
    {
        private int _Id;
        private int _IdMacroProceso;
        private string _Proceso;
        private string _Descripcion;
        private string _FechaInicio;
        private string _Seguimiento;
        private string _FechaFinal;
        private int _Responsable;
        private string _CargoResponsable;
        private int _IdUsuario;
        private string _Usuario;
        private DateTime _FechaRegistro;
        private string _pathFile;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdMacroProceso
        {
            get { return _IdMacroProceso; }
            set { _IdMacroProceso = value; }
        }
        public string strProceso
        {
            get { return _Proceso; }
            set { _Proceso = value; }
        }

        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string dtFechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }
        public string strSeguimiento
        {
            get { return _Seguimiento; }
            set { _Seguimiento = value; }
        }
        public string dtFechaFinal
        {
            get { return _FechaFinal; }
            set { _FechaFinal = value; }
        }

        public int intResponsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }
        public string strCargoResponsable
        {
            get { return _CargoResponsable; }
            set { _CargoResponsable = value; }
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

        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public string strPathFile
        {
            get { return _pathFile; }
            set { _pathFile = value; }
        }
        #region Constructors
        public clsControlNoConformidad()
        {
        }

        public clsControlNoConformidad(int intId, int intIdMacroProceso, int intResponsable,
            string strDescripcion, string dtFechaInicio, string dtFechaFinal, 
            int intIdUsuario, DateTime dtFechaRegistro, string strPathFile)
        {
            this.intId = intId;
            this.intIdMacroProceso = intIdMacroProceso;
            this.intResponsable = intResponsable;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaInicio = dtFechaInicio;
            this.dtFechaFinal = dtFechaFinal;
            this.intIdUsuario = intIdUsuario;
            this.strPathFile = strPathFile;
        }
        #endregion
    }
}