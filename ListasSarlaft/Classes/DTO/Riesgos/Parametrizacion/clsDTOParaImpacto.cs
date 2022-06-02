using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaImpacto
    {
        #region Variables
        private string _NombreImpacto1;
        private string _NombreImpacto2;
        private string _NombreImpacto3;
        private string _NombreImpacto4;
        private string _NombreImpacto5;
        #endregion Variables
        #region Get/Set
        public string strNombreImpacto1
        {
            get { return _NombreImpacto1; }
            set { _NombreImpacto1 = value; }
        }
        public string strNombreImpacto2
        {
            get { return _NombreImpacto2; }
            set { _NombreImpacto2 = value; }
        }
        public string strNombreImpacto3
        {
            get { return _NombreImpacto3; }
            set { _NombreImpacto3 = value; }
        }
        public string strNombreImpacto4
        {
            get { return _NombreImpacto4; }
            set { _NombreImpacto4 = value; }
        }
        public string strNombreImpacto5
        {
            get { return _NombreImpacto5; }
            set { _NombreImpacto5 = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaImpacto() { }
        #endregion Constructor
    }
}