using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCaracterizacionXSalida
    {
        private int _Id;
        private int _IdCaracterizacion;
        private int _IdSalida;
        private int _IdUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdCaracterizacion
        {
            get { return _IdCaracterizacion; }
            set { _IdCaracterizacion = value; }
        }

        public int intIdSalida
        {
            get { return _IdSalida; }
            set { _IdSalida = value; }
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
        public clsCaracterizacionXSalida()
        {
        }

        public clsCaracterizacionXSalida(int intId, int intIdCaracterizacion, int intIdSalida, int intIdUsuario,
            string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdCaracterizacion = intIdCaracterizacion;
            this.intIdSalida = intIdSalida;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}