using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaFactorRiesgoOperativo
    {
        #region Variables
        private int _IdFactorRiesgoOperativo;
        private string _NombreFactorRiesgoOperativo;
        #endregion Variables
        #region Get/Set
        public int intIdFactorRiesgoOperativo
        {
            get { return _IdFactorRiesgoOperativo; }
            set { _IdFactorRiesgoOperativo = value; }
        }
        public string strNombreFactorRiesgoOperativo
        {
            get { return _NombreFactorRiesgoOperativo; }
            set { _NombreFactorRiesgoOperativo = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaFactorRiesgoOperativo() { }
        #endregion Constructor
    }
}