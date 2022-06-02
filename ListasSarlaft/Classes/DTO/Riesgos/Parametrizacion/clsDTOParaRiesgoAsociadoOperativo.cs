using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaRiesgoAsociadoOperativo
    {
        #region Variables
        private int _IdTipoEventoOperativo;
        private string _NombreRiesgoAsociadoOperativo;
        #endregion Variables
        #region Get/Set
        public int intIdTipoEventoOperativo
        {
            get { return _IdTipoEventoOperativo; }
            set { _IdTipoEventoOperativo = value; }
        }
        public string strNombreRiesgoAsociadoOperativo
        {
            get { return _NombreRiesgoAsociadoOperativo; }
            set { _NombreRiesgoAsociadoOperativo = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaRiesgoAsociadoOperativo() { }
        #endregion Constructor
    }
}