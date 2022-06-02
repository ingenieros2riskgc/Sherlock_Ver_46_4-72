using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsObservacionControlSalida
    {
        private int _Id;
        private string _Descripcion;
        private int _IdControlSalida;
        private DateTime _FechaRegistro;
        private int _IdUsuario;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public int intIdControlSalida
        {
            get { return _IdControlSalida; }
            set { _IdControlSalida = value; }
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

        #region
        public clsObservacionControlSalida()
        {
        }

        public clsObservacionControlSalida(int intId, string strDescripcion, int intIdControlSalida,
            int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.intIdControlSalida = intIdControlSalida;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}