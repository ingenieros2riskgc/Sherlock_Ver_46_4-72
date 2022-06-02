using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCompetenciaHistorico
    {
        private int _Id;
        private int _IdEvaluacionCompetencia;
        private string _Nombre;
        private int _Ponderacion;
        private int _IdUsuario;
        private DateTime _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int IdEvaluacionCompetencia
        {
            get { return _IdEvaluacionCompetencia; }
            set { _IdEvaluacionCompetencia = value; }
        }

        public string strNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public int intPonderacion
        {
            get { return _Ponderacion; }
            set { _Ponderacion = value; }
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
        public clsCompetenciaHistorico()
        {
        }

        public clsCompetenciaHistorico(int intId, int IdEvaluacionCompetencia, int intPonderacion, string strNombre, 
            int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.IdEvaluacionCompetencia = IdEvaluacionCompetencia;
            this.intPonderacion = intPonderacion;
            this.strNombre = strNombre;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}