using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOdetalleFormulaRiesgoIndicador
    {
        #region Variables
        private string _Variable;
        private string _Tipo;
        private int _Secuencia;
        private int _IdOperando;
        #endregion Variables
        #region Get/Set
        public string strVariable
        {
            get { return _Variable; }
            set { _Variable = value; }
        }
        public string strTipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public int intSecuencia
        {
            get { return _Secuencia; }
            set { _Secuencia = value; }
        }
        public int intIdOperando
        {
            get { return _IdOperando; }
            set { _IdOperando = value; }
        }
        #endregion return
        #region Constructor
        public clsDTOdetalleFormulaRiesgoIndicador() { }
        #endregion Constructor
    }
}