using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOControlxVariable
    {
        #region Variables
        private int _IdControlxVariable;
        private int _IdControl;
        private string _NombreVariable;
        private int _IdCategoria;
        private string _NombreCategoria;
        #endregion Variables
        #region Get/Set
        public int intIdControlxVariable
        {
            get { return _IdControlxVariable; }
            set { _IdControlxVariable = value; }
        }
        public int intIdControl
        {
            get { return _IdControl; }
            set { _IdControl = value; }
        }
        public string strNombreVariable
        {
            get { return _NombreVariable; }
            set { _NombreVariable = value; }
        }
        public int intIdCategoria
        {
            get { return _IdCategoria; }
            set { _IdCategoria = value; }
        }
        public string strNombreCategoria
        {
            get { return _NombreCategoria; }
            set { _NombreCategoria = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOControlxVariable() { }
        #endregion Constructor
    }
}