using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaRiesgoAsociadoLaft
    {
        #region Variables
        private int _IdRiesgoAsociadoLA;
        private string _NombreRiesgoAsociadoLA;
        #endregion Variables
        #region Get/Set
        public int intIdRiesgoAsociadoLA
        {
            get { return _IdRiesgoAsociadoLA; }
            set { _IdRiesgoAsociadoLA = value; }
        }
        public string strNombreRiesgoAsociadoLA
        {
            get { return _NombreRiesgoAsociadoLA; }
            set { _NombreRiesgoAsociadoLA = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaRiesgoAsociadoLaft() { }
        #endregion Constructor
    }
}