using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroMandoIndicadorFiltros
    {
        #region Variables
        private int _IdCadenaValor;
        private int _IdMacroproceso;
        private int _IdProceso;
        private int _IdSubproceso;
        private string _Jerarquia;
        #endregion Variables
        #region Get/Set
        public int intIdCadenaValor
        {
            get { return _IdCadenaValor; }
            set { _IdCadenaValor = value; }
        }
        public int intIdMacroproceso
        {
            get { return _IdMacroproceso; }
            set { _IdMacroproceso = value; }
        }
        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }
        public int intIdSubproceso
        {
            get { return _IdSubproceso; }
            set { _IdSubproceso = value; }
        }
        public string strJerarquia
        {
            get { return _Jerarquia; }
            set { _Jerarquia = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOCuadroMandoIndicadorFiltros() { }
        #endregion Constructor
    }
}