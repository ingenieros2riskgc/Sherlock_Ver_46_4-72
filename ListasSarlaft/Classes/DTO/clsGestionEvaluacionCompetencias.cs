using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsGestionEvaluacionCompetencias
    {
        private string _DescripcionCompetencia;
        private string _NombreCompetencia;
        private int _PuntajeAsignado;

        public string strDescripcionCompetencia
        {
            get { return _DescripcionCompetencia; }
            set { _DescripcionCompetencia = value; }
        }
        public string strNombreCompetencia
        {
            get { return _NombreCompetencia; }
            set { _NombreCompetencia = value; }
        }
        public int intPuntajeAsignado
        {
            get { return _PuntajeAsignado; }
            set { _PuntajeAsignado = value; }
        }
        #region Constructors
        public clsGestionEvaluacionCompetencias()
        {
        }

        public clsGestionEvaluacionCompetencias(string strDescripcionCompetencia, string strNombreCompetencia)
        {
            this.strDescripcionCompetencia = strDescripcionCompetencia;
            this.strNombreCompetencia = strNombreCompetencia;
        }
        public clsGestionEvaluacionCompetencias(string strDescripcionCompetencia, string strNombreCompetencia, int intPuntajeAsignado)
        {
            this.strDescripcionCompetencia = strDescripcionCompetencia;
            this.strNombreCompetencia = strNombreCompetencia;
            this.intPuntajeAsignado = intPuntajeAsignado;
        }
        #endregion
    }
}