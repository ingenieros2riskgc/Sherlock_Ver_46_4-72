using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsNecesidadesCompetencia
    {
        private int _Id;
        private int _IdUsuario;
        private DateTime _FechaRegistro;
        private int _IdEvaluacionCompetencia;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdEvaluacionCompetencia
        {
            get { return _IdEvaluacionCompetencia; }
            set { _IdEvaluacionCompetencia = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsNecesidadesCompetencia()
        {
        }

        public clsNecesidadesCompetencia(int intId, int intIdEvaluacionCompetencia, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdEvaluacionCompetencia = intIdEvaluacionCompetencia;
        }
        #endregion
    }
}