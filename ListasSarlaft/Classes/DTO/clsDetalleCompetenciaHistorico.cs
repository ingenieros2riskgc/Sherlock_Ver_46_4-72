using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsDetalleCompetenciaHistorico
    {
        private int _Id;
        private int _IdCompetenciaHistorico;
        private int _Valor;
        private int _IdUsuario;
        private DateTime _FechaRegistro;

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

        public int intValor
        {
            get { return _Valor; }
            set { _Valor = value; }
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
        public clsDetalleCompetenciaHistorico()
        {
        }

        public clsDetalleCompetenciaHistorico(int intId, int intIdCompetenciaHistorico, int intValor, 
            int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdCompetenciaHistorico = intIdCompetenciaHistorico;
            this.intValor = intValor;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}