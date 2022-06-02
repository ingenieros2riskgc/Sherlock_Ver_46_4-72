using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsVerCaracterizacionEntradas
    {
        private string _DescripcionEntrada;
        private string _Proveedor;
        public string strDescripcionEntrada
        {
            get { return _DescripcionEntrada; }
            set { _DescripcionEntrada = value; }
        }
        public string strProveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }
        #region Constructors
        public clsVerCaracterizacionEntradas()
        {
        }

        public clsVerCaracterizacionEntradas(string strDescripcionEntrada, string strProveedor)
        {
            this.strDescripcionEntrada = strDescripcionEntrada;
            this.strProveedor = strProveedor;
        }
        #endregion
    }
}