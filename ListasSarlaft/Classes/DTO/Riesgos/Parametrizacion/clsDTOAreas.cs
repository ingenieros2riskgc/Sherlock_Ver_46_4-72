using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOAreas
    {
        #region Variables
        private int _IdArea;
        private string _NombreArea;
        #endregion Variables
        #region Get/Set
        public int intIdArea
        {
            get { return _IdArea; }
            set { _IdArea = value; }
        }
        public string strNombreArea
        {
            get { return _NombreArea; }
            set { _NombreArea = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOAreas() { }
        #endregion Constructor
    }
}