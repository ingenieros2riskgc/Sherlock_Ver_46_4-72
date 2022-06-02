using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsVerCaracterizacion
    {
        private int _Id;
        private string _FechaRegistro;
        private int _IdUsuario;
        private int _IdProceso;
        private string _nombreProceso;
        private string _objetivo;

        public string NombreResponsable { get; set; }
        public string CargoResponsable { get; set; }
        public string Recursos { get; set; }
        public string Numerales { get; set; }
        public string Responsables { get; set; }
        public string Codigo { get; set; }

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }
        public string strnombreProceso
        {
            get { return _nombreProceso; }
            set { _nombreProceso = value; }
        }
        public string strObjetivo
        {
            get { return _objetivo; }
            set { _objetivo = value; }
        }
        #region Constructors
        public clsVerCaracterizacion()
        {
        }

        public clsVerCaracterizacion(int intId, string dtFechaRegistro, int intIdUsuario, int IdProceso, string strnombreProceso, string strObjetivo)
        {
            this.intId = intId;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.intIdProceso = IdProceso;
            this.strnombreProceso = strnombreProceso;
            this.strObjetivo = strObjetivo;
        }

        public clsVerCaracterizacion(int intId, string dtFechaRegistro, int intIdUsuario, int IdProceso, string strnombreProceso, string strObjetivo, string nombreResponsable, string cargoResponsable, string recursos, string numerales, string responsables, string codigo)
        {
            this.intId = intId;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.intIdProceso = IdProceso;
            this.strnombreProceso = strnombreProceso;
            this.strObjetivo = strObjetivo;
            NombreResponsable = nombreResponsable;
            CargoResponsable = cargoResponsable;
            Responsables = responsables;
            Numerales = numerales;
            Responsables = responsables;
            Codigo = codigo;
        }
        #endregion
    }
}