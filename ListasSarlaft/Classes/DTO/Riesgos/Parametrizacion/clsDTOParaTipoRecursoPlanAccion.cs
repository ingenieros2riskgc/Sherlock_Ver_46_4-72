using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaTipoRecursoPlanAccion
    {
        #region Variables
        private int _IdTipoRecursoPlanAccion;
        private string _NombreTipoRecursoPlanAccion;
        #endregion Variables
        #region Get/Set
        public int intIdTipoRecursoPlanAccion
        {
            get { return _IdTipoRecursoPlanAccion; }
            set { _IdTipoRecursoPlanAccion = value; }
        }
        public string strNombreTipoRecursoPlanAccion
        {
            get { return _NombreTipoRecursoPlanAccion; }
            set { _NombreTipoRecursoPlanAccion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaTipoRecursoPlanAccion() { }
        #endregion Constructor
    }
}