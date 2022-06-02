using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaEstadoPlanEvaluacion
    {
        #region Variables
        private int _IdEstadoPlanEvaluacion;
        private string _NombreEstadoPlanEvaluacion;
        #endregion Variables
        #region Get/Set
        public int intIdEstadoPlanEvaluacion
        {
            get { return _IdEstadoPlanEvaluacion; }
            set { _IdEstadoPlanEvaluacion = value; }
        }
        public string strNombreEstadoPlanEvaluacion
        {
            get { return _NombreEstadoPlanEvaluacion; }
            set { _NombreEstadoPlanEvaluacion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaEstadoPlanEvaluacion() { }
        #endregion Constructor
    }
}