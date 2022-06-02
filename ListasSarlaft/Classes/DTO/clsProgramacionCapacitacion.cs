using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsProgramacionCapacitacion
    {
        private int _Id;
        private int _Proceso;
        private string _NombreProceso;
        private int _Responsable;
        private string _CargoResponsable;
        private string _Actividad;
        private string _DirigidoA;
        private string _Asistentes;
        private string _FechaProgramada;
        private string _FechaRealizada;
        private string _Evaluacion;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        private string _Usuario;

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

        public int IdMacroProceso
        {
            get
            {
                return _Proceso;
            }

            set
            {
                _Proceso = value;
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

        public int IdCargoResponsable
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
        public string strCargoResponsable
        {
            get
            {
                return _CargoResponsable;
            }

            set
            {
                _CargoResponsable = value;
            }
        }
        public string strActividad
        {
            get
            {
                return _Actividad;
            }

            set
            {
                _Actividad = value;
            }
        }

        public string strDirigidoA
        {
            get
            {
                return _DirigidoA;
            }

            set
            {
                _DirigidoA = value;
            }
        }
        public string strAsistentes
        {
            get
            {
                return _Asistentes;
            }

            set
            {
                _Asistentes = value;
            }
        }
        public string dtFechaProgramada
        {
            get
            {
                return _FechaProgramada;
            }

            set
            {
                _FechaProgramada = value;
            }
        }

        public string dtFechaRealizada
        {
            get
            {
                return _FechaRealizada;
            }

            set
            {
                _FechaRealizada = value;
            }
        }

        public string intEvaluacion
        {
            get
            {
                return _Evaluacion;
            }

            set
            {
                _Evaluacion = value;
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
        public string strUsuario
        {
            get
            {
                return _Usuario;
            }

            set
            {
                _Usuario = value;
            }
        }

        #region
        public clsProgramacionCapacitacion()
        {

        }

        public clsProgramacionCapacitacion(int intId, int IdMacroProceso, int IdCargoResponsable, string strActividad, string strDirigidoA,
            string strAsistentes, string dtFechaProgramada, string dtFechaRealizada, string intEvaluacion, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.IdMacroProceso = IdMacroProceso;
            this.IdCargoResponsable = IdCargoResponsable;
            this.strActividad = strActividad;
            this.strDirigidoA = strDirigidoA;
            this.strAsistentes = strAsistentes;
            this.dtFechaProgramada = dtFechaProgramada;
            this.dtFechaRealizada = dtFechaRealizada;
            this.intEvaluacion = intEvaluacion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsProgramacionCapacitacion(int intId, int IdMacroProceso,string NombreProceso, int IdCargoResponsable, string CargoResponsable, string strActividad, string strDirigidoA,
            string strAsistentes, string dtFechaProgramada, string dtFechaRealizada, string intEvaluacion, int intIdUsuario,string strUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.IdMacroProceso = IdMacroProceso;
            this.strNombreProceso = strNombreProceso;
            this.IdCargoResponsable = IdCargoResponsable;
            this.strCargoResponsable = strCargoResponsable;
            this.strActividad = strActividad;
            this.strDirigidoA = strDirigidoA;
            this.strAsistentes = strAsistentes;
            this.dtFechaProgramada = dtFechaProgramada;
            this.dtFechaRealizada = dtFechaRealizada;
            this.intEvaluacion = intEvaluacion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strUsuario = strUsuario;
        }
        #endregion
    }
}