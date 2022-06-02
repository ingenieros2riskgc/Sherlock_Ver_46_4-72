using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaConsecuencias
    {
        #region Variables
        private int _IdConsecuencia;
        private string _NombreConsecuencia;
        #endregion Variables
        #region Get/Set
        public int intIdConsecuencia
        {
            get { return _IdConsecuencia; }
            set { _IdConsecuencia = value; }
        }
        public string strNombreConsecuencia
        {
            get { return _NombreConsecuencia; }
            set { _NombreConsecuencia = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaConsecuencias() { }
        #endregion Constructor
    }
}