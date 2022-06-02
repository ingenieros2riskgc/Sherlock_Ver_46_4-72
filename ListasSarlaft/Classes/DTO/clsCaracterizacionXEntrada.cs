using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCaracterizacionXEntrada
    {
        private int _Id;
        private int _IdCaracterizacion;
        private int _IdEntrada;
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

        public int intIdEntrada
        {
            get { return _IdEntrada; }
            set { _IdEntrada = value; }
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
        public clsCaracterizacionXEntrada()
        {
        }

        public clsCaracterizacionXEntrada(int intId, int intIdCaracterizacion, int intIdEntrada, int intIdUsuario,
            string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdCaracterizacion = intIdCaracterizacion;
            this.intIdEntrada = intIdEntrada;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}