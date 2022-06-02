using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaFactorRiesgoLaft
    {
        #region Variables
        private int _IdFactorRiesgoLAFT;
        private string _NombreFactorRiesgoLAFT;
        #endregion Variables
        #region Get/Set
        public int intIdFactorRiesgoLAFT
        {
            get { return _IdFactorRiesgoLAFT; }
            set { _IdFactorRiesgoLAFT = value; }
        }
        public string strNombreFactorRiesgoLAFT
        {
            get { return _NombreFactorRiesgoLAFT; }
            set { _NombreFactorRiesgoLAFT = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaFactorRiesgoLaft() { }
        #endregion Constructor
    }
}