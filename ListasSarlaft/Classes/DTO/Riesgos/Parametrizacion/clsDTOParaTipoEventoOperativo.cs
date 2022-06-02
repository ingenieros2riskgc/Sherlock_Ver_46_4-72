using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaTipoEventoOperativo
    {
        #region Variables
        private int _IdTipoRiesgoOperativo;
        private int _IdFactorRiesgoOperativo;
        private string _NombreTipoRiesgoOperativo;
        #endregion Variables
        #region Get/Set
        public int intIdTipoRiesgoOperativo
        {
            get { return _IdTipoRiesgoOperativo; }
            set { _IdTipoRiesgoOperativo = value; }
        }
        public int intIdFactorRiesgoOperativo
        {
            get { return _IdFactorRiesgoOperativo; }
            set { _IdFactorRiesgoOperativo = value; }
        }
        public string strNombreTipoRiesgoOperativo
        {
            get { return _NombreTipoRiesgoOperativo; }
            set { _NombreTipoRiesgoOperativo = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaTipoEventoOperativo() { }
        #endregion Constructor
    }
}