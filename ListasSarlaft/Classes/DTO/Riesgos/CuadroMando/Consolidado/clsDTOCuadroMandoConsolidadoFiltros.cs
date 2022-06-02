using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroMandoConsolidadoFiltros
    {
        #region Variables
        private int _RiesgoGlobal;
        private string _ClasificacionGeneral;
        private int _IdCadenaValor;
        private int _IdMacroProceso;
        private int _IdProceso;
        private int _IdSubProceso;
        private int _Area;
        private string _AreaRiesgo;
        private int _IdClasificacionGeneral;
        private int _IdFactor;
        private int _IdPlanEstrategico;
        private int _IdObjetivo;
        private DateTime _FechaHistoricoInicial;
        private DateTime _FechaHistoricoFinal;
        private DateTime _FechaEvoPefilInicial;
        private DateTime _FechaEvoPefilFinal;
        #endregion Variables
        #region Get/Set
        public int intRiesgoGlobal
        {
            get { return _RiesgoGlobal; }
            set { _RiesgoGlobal = value; }
        }
        public string strClasificacionGeneral
        {
            get { return _ClasificacionGeneral; }
            set { _ClasificacionGeneral = value; }
        }
        public int intIdCadenaValor
        {
            get { return _IdCadenaValor; }
            set { _IdCadenaValor = value; }
        }
        public int intIdMacroProceso
        {
            get { return _IdMacroProceso; }
            set { _IdMacroProceso = value; }
        }
        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }
        public int intIdSubProceso
        {
            get { return _IdSubProceso; }
            set { _IdSubProceso = value; }
        }
        public int intArea
        {
            get { return _Area; }
            set { _Area = value; }
        }
        public string strAreaRiesgo
        {
            get { return _AreaRiesgo; }
            set { _AreaRiesgo = value; }
        }
        public int intIdClasificacionGeneral
        {
            get { return _IdClasificacionGeneral; }
            set { _IdClasificacionGeneral = value; }
        }
        public int intIdFactor
        {
            get { return _IdFactor; }
            set { _IdFactor = value; }
        }
        public int intIdPlanEstrategico
        {
            get { return _IdPlanEstrategico; }
            set { _IdPlanEstrategico = value; }
        }
        public int intIdObjetivo
        {
            get { return _IdObjetivo; }
            set { _IdObjetivo = value; }
        }
        public DateTime dtFechaHistoricoInicial
        {
            get { return _FechaHistoricoInicial; }
            set { _FechaHistoricoInicial = value; }
        }
        public DateTime dtFechaHistoricoFinal
        {
            get { return _FechaHistoricoFinal; }
            set { _FechaHistoricoFinal = value; }
        }
        public DateTime dtFechaEvoPerfilInicial
        {
            get { return _FechaEvoPefilInicial; }
            set { _FechaEvoPefilInicial = value; }
        }
        public DateTime dtFechaEvoPerfilFinal
        {
            get { return _FechaEvoPefilFinal; }
            set { _FechaEvoPefilFinal = value; }
        }
        #endregion return
        #region Constructor
        public clsDTOCuadroMandoConsolidadoFiltros() { }
        #endregion Constructor
    }
}