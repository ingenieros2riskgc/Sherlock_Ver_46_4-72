using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaEstadoLegislacion
    {
        #region Variables
        private int _IdEstadoLegislacion;
        private string _NombreEstadoLegislacion;
        #endregion Variables
        #region Get/Set
        public int intIdEstadoLegislacion
        {
            get { return _IdEstadoLegislacion; }
            set { _IdEstadoLegislacion = value; }
        }
        public string strNombreEstadoLegislacion
        {
            get { return _NombreEstadoLegislacion; }
            set { _NombreEstadoLegislacion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaEstadoLegislacion() { }
        #endregion Constructor
    }
}