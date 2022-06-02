using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOfrecuenciaMedicion
    {
        #region Variables
        private int _IdFrecuenciaMedicion;
        private string _FrecuenciaMedicion;
        #endregion Variables
        #region Get/Set
        public int intIdFrecuenciaMedicion
        {
            get { return _IdFrecuenciaMedicion; }
            set { _IdFrecuenciaMedicion = value; }
        }
        public string strFrecuenciaMedicion
        {
            get { return _FrecuenciaMedicion; }
            set { _FrecuenciaMedicion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOfrecuenciaMedicion() { }
        #endregion Constructor
    }
}