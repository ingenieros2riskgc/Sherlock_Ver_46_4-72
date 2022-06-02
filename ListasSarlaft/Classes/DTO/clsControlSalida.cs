using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsControlSalida
    {
        private int _Id;
        private int _IdMacroProceso;
        private string _NombreProceso;
        private int _IdEstadoControlSalida;
        private string _NombreEstado;
        private string _NoConformidad;
        private string _AccionesTomadas;
        private int _CargoResponsable;
        private string _PersonaAutoriza;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        private string _Usuario;
        private string _Observaciones;
        
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
        public int intIdEstadoControlSalida
        {
            get { return _IdEstadoControlSalida; }
            set { _IdEstadoControlSalida = value; }
        }
        public string strNombreEstado
        {
            get { return _NombreEstado; }
            set { _NombreEstado = value; }
        }
        public string strNoConformidad
        {
            get { return _NoConformidad; }
            set { _NoConformidad = value; }
        }

        public string strAccionesTomadas
        {
            get { return _AccionesTomadas; }
            set { _AccionesTomadas = value; }
        }
        public int intCargoResponsable
        {
            get { return _CargoResponsable; }
            set { _CargoResponsable = value; }
        }
        public string strPersonaAutoriza
        {
            get { return _PersonaAutoriza; }
            set { _PersonaAutoriza = value; }
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
        public string strObservaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }
        
        #region Constructors
        public clsControlSalida()
        {
        }

        public clsControlSalida(int intId, int intIdMacroProceso, int intIdEstadoControlSalida, string strNoConformidad,
            string strAccionesTomadas, string strPersonaAutoriza, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdMacroProceso = intIdMacroProceso;
            this.intIdEstadoControlSalida = intIdEstadoControlSalida;
            this.strNoConformidad = strNoConformidad;
            this.strAccionesTomadas = strAccionesTomadas;
            this.strPersonaAutoriza = strPersonaAutoriza;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
    public class clsControlProceso
    {
        private int _IdTipoProceso;
        private int _IdControl;
        private int _IdProcesos;
        public int intIdTipoProceso
        {
            get { return _IdTipoProceso; }
            set { _IdTipoProceso = value; }
        }
        public int intIdControl
        {
            get { return _IdControl; }
            set { _IdControl = value; }
        }
        public int intIdProcesos
        {
            get { return _IdProcesos; }
            set { _IdProcesos = value; }
        }
        public clsControlProceso() { }
    }
}