using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsSalidaxActividad
    {
        private int _IdActividadSalida;
        private int _IdActividad;
        private int _IdSalida;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        public int intIdActividadSalida
        {
            get { return _IdActividadSalida; }
            set { _IdActividadSalida = value; }
        }

        public int intIdActividad
        {
            get { return _IdActividad; }
            set { _IdActividad = value; }
        }
        public int intIdSalida
        {
            get { return _IdSalida; }
            set { _IdSalida = value; }
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
        public clsSalidaxActividad()
        {
        }

        public clsSalidaxActividad(int intIdActividadSalida, int intIdActividad, int intIdSalida,
            DateTime dtFechaRegistro, int intIdUsuario)
        {
            this.intIdActividadSalida = intIdActividadSalida;
            this.intIdActividad = intIdActividad;
            this.intIdSalida = intIdSalida;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}