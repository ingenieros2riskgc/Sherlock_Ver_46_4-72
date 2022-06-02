using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaCausas
    {
        #region Variables
        private int _IdCausas;
        private string _NombreCausas;
        #endregion Variables
        #region Get/Set
        public int intIdCausas
        {
            get { return _IdCausas; }
            set { _IdCausas = value; }
        }
        public string strNombreCausas
        {
            get { return _NombreCausas; }
            set { _NombreCausas = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaCausas() { }
        #endregion Constructor
    }
}