using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsAspectoProveedor
    {
        private int _Id;
        private string _Aspecto;
        private decimal _Valor;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _NombreUsuario;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strAspecto
        {
            get { return _Aspecto; }
            set { _Aspecto = value; }
        }

        public decimal decValor
        {
            get { return _Valor; }
            set { _Valor = value; }
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
        public clsAspectoProveedor()
        {
        }

        public clsAspectoProveedor(int intId, string strAspecto, decimal decValor, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strAspecto = strAspecto;
            this.decValor = decValor;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsAspectoProveedor(int intId, string strAspecto, decimal decValor, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strAspecto = strAspecto;
            this.decValor = decValor;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }
        #endregion
    }
}