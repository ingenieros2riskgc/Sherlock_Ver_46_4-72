using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroMandoConsolidado
    {
        #region Variables
        private int _NumeroRegistros;
        private int _IdProbabilidadResidual;
        private int _IdImpactoResidual;
        private int _IdProbabilidadInherente;
        private int _IdImpactoInherente;
        private string _NombreRiesgo;
        private string _NombreRiesgoInherente;
        private int _SumatoriaProbabilidad;
        private int _SumatoriaImpacto;
        #endregion Variables
        #region Get/Set
        public int intNumeroRegistros
        {
            get { return _NumeroRegistros; }
            set { _NumeroRegistros = value; }
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
        public int intIdProbabilidadInherente
        {
            get { return _IdProbabilidadInherente; }
            set { _IdProbabilidadInherente = value; }
        }
        public int intIdImpactoInherente
        {
            get { return _IdImpactoInherente; }
            set { _IdImpactoInherente = value; }
        }
        public string strNombreRiesgo
        {
            get { return _NombreRiesgo; }
            set { _NombreRiesgo = value; }
        }
        public string strNombreRiesgoInherente
        {
            get { return _NombreRiesgoInherente; }
            set { _NombreRiesgoInherente = value; }
        }
        public int intSumatoriaProbabilidad
        {
            get { return _SumatoriaProbabilidad; }
            set { _SumatoriaProbabilidad = value; }
        }
        public int intSumatoriaImpacto
        {
            get { return _SumatoriaImpacto; }
            set { _SumatoriaImpacto = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOCuadroMandoConsolidado() { }
        #endregion Constructor
    }
}