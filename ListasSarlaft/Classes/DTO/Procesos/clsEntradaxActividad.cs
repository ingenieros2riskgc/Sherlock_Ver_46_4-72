using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsEntradaxActividad
    {
       private int _IdActividadEntrada;
        private int _IdActividad;
      private int _IdEntrada;
      private DateTime _FechaRegistro;
      private int _IdUsuario;
        public int intIdActividadEntrada
        {
            get { return _IdActividadEntrada; }
            set { _IdActividadEntrada = value; }
        }

        public int intIdActividad
        {
            get { return _IdActividad; }
            set { _IdActividad = value; }
        }
        public int intIdEntrada
        {
            get { return _IdEntrada; }
            set { _IdEntrada = value; }
        }
        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        #region Constructors
        public clsEntradaxActividad()
        {
        }

        public clsEntradaxActividad(int intIdActividadEntrada, int intIdActividad, int intIdEntrada,
            DateTime dtFechaRegistro,int intIdUsuario )
        {
            this.intIdActividadEntrada = intIdActividadEntrada;
            this.intIdActividad = intIdActividad;
            this.intIdEntrada = intIdEntrada;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}