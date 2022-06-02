using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOTipoLegislacion
    {
        #region Variables
        private int _IdTipoLegislacion;
        private string _NombreTipoLegislacion;
        #endregion Variables
        #region Get/Set
        public int intIdTipoLegislacion
        {
            get { return _IdTipoLegislacion; }
            set { _IdTipoLegislacion = value; }
        }
        public string strNombreTipoLegislacion
        {
            get { return _NombreTipoLegislacion; }
            set { _NombreTipoLegislacion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOTipoLegislacion() { }
        #endregion Constructor
    }
}