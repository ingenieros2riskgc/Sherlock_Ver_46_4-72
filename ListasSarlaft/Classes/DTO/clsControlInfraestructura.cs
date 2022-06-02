using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsControlInfraestructura
    {
        private int _Id;
        private int _IdMacroProceso;
        private string _NombreProceso;
        private int _Responsable;
        private string _CargoResponsable;
        private string _Actividad;
        private string _FechaProgramada;
        private string _FechaCumplimiento;
        private DateTime _FechaRegistro;
        private string _Observaciones;
        private int _IdUsuario;
        private string _userName;
        private int _AllProcess;
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

        public string strNombreProceso
        {
            get { return _NombreProceso; }
            set { _NombreProceso = value; }
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

        public string strActividad
        {
            get { return _Actividad; }
            set { _Actividad = value; }
        }

        public string dtFechaProgramada
        {
            get { return _FechaProgramada; }
            set { _FechaProgramada = value; }
        }

        public string dtFechaCumplimiento
        {
            get { return _FechaCumplimiento; }
            set { _FechaCumplimiento = value; }
        }

        public string strObservaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
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

        public string struserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public int intAllProcess
        {
            get { return _AllProcess; }
            set { _AllProcess = value; }
        }
        #region Constructors
        public clsControlInfraestructura()
        {
        }

        public clsControlInfraestructura(int intId, string strObservaciones, string dtFechaCumplimiento)
        {
            this.intId = intId;
            this.strObservaciones = strObservaciones;
            this.dtFechaCumplimiento = dtFechaCumplimiento;
        }

        public clsControlInfraestructura(int intId, int intIdMacroProceso, string strNombreProceso, int intResponsable,string strCargoResponsable,
            string strActividad, string dtFechaProgramada, string dtFechaCumplimiento, string strObservaciones,
            int intIdUsuario, DateTime dtFechaRegistro, string struserName)
        {
            this.intId = intId;
            this.intIdMacroProceso = intIdMacroProceso;
            this.strNombreProceso = strNombreProceso;
            this.intResponsable = intResponsable;
            this.strCargoResponsable = strCargoResponsable;
            this.strActividad = strActividad;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaProgramada = dtFechaProgramada;
            this.dtFechaCumplimiento = dtFechaCumplimiento;
            this.strObservaciones = strObservaciones;
            this.intIdUsuario = intIdUsuario;
            this.struserName = struserName;
        }
        public clsControlInfraestructura(int intId, int intIdMacroProceso, string strNombreProceso, int intResponsable, string strCargoResponsable,
            string strActividad, string dtFechaProgramada, string dtFechaCumplimiento, string strObservaciones,
            int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdMacroProceso = intIdMacroProceso;
            this.strNombreProceso = strNombreProceso;
            this.intResponsable = intResponsable;
            this.strCargoResponsable = strCargoResponsable;
            this.strActividad = strActividad;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaProgramada = dtFechaProgramada;
            this.dtFechaCumplimiento = dtFechaCumplimiento;
            this.strObservaciones = strObservaciones;
            this.intIdUsuario = intIdUsuario;
        }

        #endregion
    }
}