using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroMandoRiesgosDetalle
    {
        private int _IdRiesgo;
        private string _CodigoRiesgo;
        private string _NombreRiesgo;
        private string _RiesgoInherente;
        private int _IdProbabilidadResidual;
        private int _IdImpactoResidual;
        private int _CantControl;
        private int _CantEvento;
        private int _CausasSinControl;
        #region Get/Set
        public int intIdRiesgo
        {
            get { return _IdRiesgo; }
            set { _IdRiesgo = value; }
        }
        public string strCodigoRiesgo
        {
            get { return _CodigoRiesgo; }
            set { _CodigoRiesgo = value; }
        }
        public string strNombreRiesgo
        {
            get { return _NombreRiesgo; }
            set { _NombreRiesgo = value; }
        }
        public string strRiesgoInherente
        {
            get { return _RiesgoInherente; }
            set { _RiesgoInherente = value; }
        }
        public int intIdProbabilidadResidual
        {
            get { return _IdProbabilidadResidual; }
            set { _IdProbabilidadResidual = value; }
        }
        public int intIdImpactoResidual
        {
            get { return _IdImpactoResidual; }
            set { _IdImpactoResidual = value; }
        }
        public int intCantControl
        {
            get { return _CantControl; }
            set { _CantControl = value; }
        }
        public int intCantEvento
        {
            get { return _CantEvento; }
            set { _CantEvento = value; }
        }
        public int intCausasSinControl
        {
            get { return _CausasSinControl; }
            set { _CausasSinControl = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOCuadroMandoRiesgosDetalle() { }
        #endregion Constructor
    }
}