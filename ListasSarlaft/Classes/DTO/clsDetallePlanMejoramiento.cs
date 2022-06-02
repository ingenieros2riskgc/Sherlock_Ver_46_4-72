using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsDetallePlanMejoramiento
    {
        private int _Id;
        private int _IdPlanDeMejoramiento;
        private int _IdMacroProceso;
        private string _Actividad;
        private string _Area;
        private string _Responsable;
        private string _CargoResponsable;
        private string _FechaProgramada;
        private string _FechaRealizada;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        private string _Seguimiento;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdPlanDeMejoramiento
        {
            get { return _IdPlanDeMejoramiento; }
            set { _IdPlanDeMejoramiento = value; }
        }

        public int intIdMacroProceso
        {
            get { return _IdMacroProceso; }
            set { _IdMacroProceso = value; }
        }

        public string strActividad
        {
            get { return _Actividad; }
            set { _Actividad = value; }
        }
        public string strArea
        {
            get { return _Area; }
            set { _Area = value; }
        }
        public string strResponsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }
        public string strCargoResponsable
        {
            get { return _CargoResponsable; }
            set { _CargoResponsable = value; }
        }
        public string dtFechaProgramada
        {
            get { return _FechaProgramada; }
            set { _FechaProgramada = value; }
        }

        public string dtFechaRealizada
        {
            get { return _FechaRealizada; }
            set { _FechaRealizada = value; }
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
        public string strSeguimiento
        {
            get { return _Seguimiento; }
            set { _Seguimiento = value; }
        }
        #region Constructors
        public clsDetallePlanMejoramiento()
        {
        }

        public clsDetallePlanMejoramiento(int intId, int intIdPlanDeMejoramiento, int intIdMacroProceso, string strActividad, string strResponsable,
            string dtFechaProgramada, string dtFechaRealizada, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdPlanDeMejoramiento = intIdPlanDeMejoramiento;
            this.intIdMacroProceso = intIdMacroProceso;
            this.dtFechaProgramada = dtFechaProgramada;
            this.dtFechaRealizada = dtFechaRealizada;
            this.strActividad = strActividad;
            this.strResponsable = strResponsable;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion

    }
}