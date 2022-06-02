using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaTipoPrueba
    {
        #region Variables
        private int _IdTipoPruebaPlanEvaluacion;
        private string _NombreTipoPruebaPlanEvaluacion;
        #endregion Variables
        #region Get/Set
        public int intIdTipoPruebaPlanEvaluacion
        {
            get { return _IdTipoPruebaPlanEvaluacion; }
            set { _IdTipoPruebaPlanEvaluacion = value; }
        }
        public string strNombreTipoPruebaPlanEvaluacion
        {
            get { return _NombreTipoPruebaPlanEvaluacion; }
            set { _NombreTipoPruebaPlanEvaluacion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaTipoPrueba() { }
        #endregion Constructor
    }
}