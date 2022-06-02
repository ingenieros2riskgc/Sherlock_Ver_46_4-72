using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsValorEvaluacionCompetencia
    {
        private int _IdValorEvaluacionCompetencia;
        private int _IdEvaluacionCompetencia;
        private string _Competencia;
        private string _Criterio;
        private int _PuntajeAsignado;

        public int intIdValorEvaluacionCompetencia
        {
            get { return _IdValorEvaluacionCompetencia; }
            set { _IdValorEvaluacionCompetencia = value; }
        }
        public int intIdEvaluacionCompetencia
        {
            get { return _IdEvaluacionCompetencia; }
            set { _IdEvaluacionCompetencia = value; }
        }
        public string strCompetencia
        {
            get { return _Competencia; }
            set { _Competencia = value; }
        }
        public string strCriterio
        {
            get { return _Criterio; }
            set { _Criterio = value; }
        }
        public int intPuntajeAsignado
        {
            get { return _PuntajeAsignado; }
            set { _PuntajeAsignado = value; }
        }
         #region Constructors
        public clsValorEvaluacionCompetencia()
        {
        }

        public clsValorEvaluacionCompetencia(int intIdValorEvaluacionCompetencia, int intIdEvaluacionCompetencia, string strCompetencia,
            string strCriterio, int intPuntajeAsignado)
        {
            this.intIdValorEvaluacionCompetencia = intIdValorEvaluacionCompetencia;
            this.intIdEvaluacionCompetencia = intIdEvaluacionCompetencia;
            this.strCompetencia = strCompetencia;
            this.strCriterio = strCriterio;
            this.intPuntajeAsignado = intPuntajeAsignado;
        }
        #endregion
    }
}