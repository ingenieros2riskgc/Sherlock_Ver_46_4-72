using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaTipoRiesgoOperativo
    {
        #region Variables
        private int _IdTipoRiesgoOperativo;
        private string _NombreFactorRiesgoOperativo;
        private int _IdFactorRiesgoOperativo;
        #endregion Variables
        #region Get/Set
        public int intIdTipoRiesgoOperativo
        {
            get { return _IdTipoRiesgoOperativo; }
            set { _IdTipoRiesgoOperativo = value; }
        }
        public string strNombreFactorRiesgoOperativo
        {
            get { return _NombreFactorRiesgoOperativo; }
            set { _NombreFactorRiesgoOperativo = value; }
        }
        public int intIdFactorRiesgoOperativo
        {
            get { return _IdFactorRiesgoOperativo; }
            set { _IdFactorRiesgoOperativo = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaTipoRiesgoOperativo() { }
        #endregion Constructor
    }
}