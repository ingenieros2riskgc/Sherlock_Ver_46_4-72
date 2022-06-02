using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCadenaValor
    {
        private int _Id;
        private string _NombreCadenaValor;
        private bool _Estado;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _NombreUsuario;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombreCadenaValor
        {
            get { return _NombreCadenaValor; }
            set { _NombreCadenaValor = value; }
        }

        public bool booEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
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

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        #region Constructors
        public clsCadenaValor()
        {
        }

        public clsCadenaValor(int intId, string strNombreCadenaValor, bool booEstado, int intIdUsuario, string strNombreUsuario,
            string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreCadenaValor = strNombreCadenaValor;
            this.booEstado = booEstado;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario; 
        }
        #endregion
    }
}