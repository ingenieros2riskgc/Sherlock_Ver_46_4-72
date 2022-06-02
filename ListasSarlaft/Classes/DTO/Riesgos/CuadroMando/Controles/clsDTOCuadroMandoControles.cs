using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroMandoControles
    {
        private string _Efectividad;
        private int _IdControl;
        private string _CodigoControl;
        private string _NombreControl;
        private int _IdRiesgo;
        private int _CantRiesgo;
        private string _CodigoRiesgo;
        private string _NombreRiesgo;
        private string _RiesgoGlobal;
        private string _Responsable;
        #region Get/Set
        public string strEfectividad
        {
            get { return _Efectividad; }
            set { _Efectividad = value; }
        }
        public int intIdControl
        {
            get { return _IdControl; }
            set { _IdControl = value; }
        }
        public string strCodigoControl
        {
            get { return _CodigoControl; }
            set { _CodigoControl = value; }
        }
        public string strNombreControl
        {
            get { return _NombreControl; }
            set { _NombreControl = value; }
        }
        public int intIdRiesgo
        {
            get { return _IdRiesgo; }
            set { _IdRiesgo = value; }
        }
        public int intCantRiesgo
        {
            get { return _CantRiesgo; }
            set { _CantRiesgo = value; }
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
        public string strRiesgoGlobal
        {
            get { return _RiesgoGlobal; }
            set { _RiesgoGlobal = value; }
        }
        public string strResponsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOCuadroMandoControles() { }
        #endregion Constructor
    }
}