using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCaracterizacion
    {
        private int _Id;
        private int _IdTipoProceso;
        private int _IdProceso;
        private int _IdUsuario;
        private string _FechaRegistro;

        public string Recursos { get; set; }
        public string Numerales { get; set; }
        public string Responsables { get; set; }
        public string Codigo { get; set; }

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdTipoProceso
        {
            get { return _IdTipoProceso; }
            set { _IdTipoProceso = value; }
        }

        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsCaracterizacion()
        {
        }

        public clsCaracterizacion(int intId, int intIdTipoProceso, int intIdProceso, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdTipoProceso = intIdTipoProceso;
            this.intIdProceso = intIdProceso;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        public clsCaracterizacion(int intId, int intIdTipoProceso, int intIdProceso, int intIdUsuario, string dtFechaRegistro, string recursos, string numerales, string responsables, string codigo)
        {
            this.intId = intId;
            this.intIdTipoProceso = intIdTipoProceso;
            this.intIdProceso = intIdProceso;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            Recursos = recursos;
            Numerales = numerales;
            Responsables = responsables;
            Codigo = codigo;
        }

        #endregion
    }
}