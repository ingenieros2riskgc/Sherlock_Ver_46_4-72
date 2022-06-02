using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsEvaluacionCompetencia
    {
        private int _Id;
        private string _NombreEvaluado;
        private int _CargoResponsable;
        private string _NombreCargo;
        private string _JefeInmediato;
        private string _NombreProceso;
        private int _IdMacroProceso;
        private int _IdUsuario;
        private string _Usuario;
        private string _FechaRegistro;
        private int _IdTipoProceso;
        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombreEvaluado
        {
            get { return _NombreEvaluado; }
            set { _NombreEvaluado = value; }
        }

        public int intCargoResponsable
        {
            get { return _CargoResponsable; }
            set { _CargoResponsable = value; }
        }

        public string strNombreCargo
        {
            get { return _NombreCargo; }
            set { _NombreCargo = value; }
        }

        public string strJefeInmediato
        {
            get { return _JefeInmediato; }
            set { _JefeInmediato = value; }
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
        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public int intIdTipoProceso
        {
            get { return _IdTipoProceso; }
            set { _IdTipoProceso = value; }
        }
        #region Constructors
        public clsEvaluacionCompetencia()
        {
        }

        public clsEvaluacionCompetencia(int intId, string strNombreEvaluado, int intCargoResponsable, string strJefeInmediato,
            int intIdMacroProceso, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreEvaluado = strNombreEvaluado;
            this.intCargoResponsable = intCargoResponsable;
            this.strJefeInmediato = strJefeInmediato;
            this.intIdMacroProceso = intIdMacroProceso;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsEvaluacionCompetencia(int intId, string strNombreEvaluado, int intCargoResponsable, string strNombreCargo, string strJefeInmediato,
            string strNombreProceso, int intIdMacroProceso, int intIdUsuario, string strUsuario, string dtFechaRegistro, int intIdTipoProceso)
        {
            this.intId = intId;
            this.strNombreEvaluado = strNombreEvaluado;
            this.intCargoResponsable = intCargoResponsable;
            this.strNombreCargo = strNombreCargo;
            this.strJefeInmediato = strJefeInmediato;
            this.strNombreProceso = strNombreProceso;
            this.intIdMacroProceso = intIdMacroProceso;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strUsuario = strUsuario;
            this.intIdTipoProceso = intIdTipoProceso;
        }
        #endregion
    }
}