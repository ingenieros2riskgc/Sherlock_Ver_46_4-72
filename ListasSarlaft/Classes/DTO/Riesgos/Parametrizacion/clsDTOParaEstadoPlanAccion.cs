using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaEstadoPlanAccion
    {
        #region Variables
        private int _IdEstadoPlanAccion;
        private string _NombreEstadoPlanAccion;
        #endregion Variables
        #region Get/Set
        public int intIdEstadoPlanAccion
        {
            get { return _IdEstadoPlanAccion; }
            set { _IdEstadoPlanAccion = value; }
        }
        public string strNombreEstadoPlanAccion
        {
            get { return _NombreEstadoPlanAccion; }
            set { _NombreEstadoPlanAccion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaEstadoPlanAccion() { }
        #endregion Constructor
    }
}