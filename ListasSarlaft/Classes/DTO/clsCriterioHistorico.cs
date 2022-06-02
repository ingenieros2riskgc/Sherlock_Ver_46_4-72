using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCriterioHistorico
    {
        private int _Id;
        private int _IdCompetenciaHistorico;
        private string _Descripcion;
        private DateTime _FechaRegistro;
        private int _IdUsuario;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdCompetenciaHistorico
        {
            get { return _IdCompetenciaHistorico; }
            set { _IdCompetenciaHistorico = value; }
        }

        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
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
        public clsCriterioHistorico()
        {
        }

        public clsCriterioHistorico(int intId, int intIdCompetenciaHistorico, string strDescripcion, 
            int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdCompetenciaHistorico = intIdCompetenciaHistorico;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}