using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOprocesoRiesgoIndicador
    {
        #region Variables
        private int _IdprocesoRiesgoIndicador;
        private int _IdRiesgoIndicador;
        private int _Idproceso;
        private string _NombreProceso;
        private int _TipoProceso;
        #endregion Variables
        #region Get/Set
        public int intIdprocesoRiesgoIndicador
        {
            get { return _IdprocesoRiesgoIndicador; }
            set { _IdprocesoRiesgoIndicador = value; }
        }
        public int intIdRiesgoIndicador
        {
            get { return _IdRiesgoIndicador; }
            set { _IdRiesgoIndicador = value; }
        }
        public int intIdProceso
        {
            get { return _Idproceso; }
            set { _Idproceso = value; }
        }
        public string strNombreProceso
        {
            get { return _NombreProceso; }
            set { _NombreProceso = value; }
        }
        public int intTipoProceso
        {
            get { return _TipoProceso; }
            set { _TipoProceso = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOprocesoRiesgoIndicador() { }
        #endregion Constructor
    }
}