using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCalificacionControl
    {
        #region Variables
        private int _IdCalificacionControl;
        private string _NombreEscala;
        private double _LimiteInferior;
        private double _LimiteSuperior;
        private string _Color;
        #endregion Variables
        #region Get/Set
        public int intIdCalificacionControl
        {
            get { return _IdCalificacionControl; }
            set { _IdCalificacionControl = value; }
        }
        public string strNombreEscala
        {
            get { return _NombreEscala; }
            set { _NombreEscala = value; }
        }
        public double intLimiteInferior
        {
            get { return _LimiteInferior; }
            set { _LimiteInferior = value; }
        }
        public double intLimiteSuperior
        {
            get { return _LimiteSuperior; }
            set { _LimiteSuperior = value; }
        }
        public string strColor
        {
            get { return _Color; }
            set { _Color = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOCalificacionControl() { }
        #endregion Constructor
    }
}