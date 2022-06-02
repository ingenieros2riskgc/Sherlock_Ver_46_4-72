using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsParametrosProvHistorico
    {
        private int _Id;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        private string _Descripcion;
        private int _IdCriterioProvHistorico;
        private decimal _valorParametro;

        public int intId
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public DateTime dtFechaRegistro
        {
            get
            {
                return _FechaRegistro;
            }

            set
            {
                _FechaRegistro = value;
            }
        }

        public int intIdUsuario
        {
            get
            {
                return _IdUsuario;
            }

            set
            {
                _IdUsuario = value;
            }
        }

        public string strDescripcion
        {
            get
            {
                return _Descripcion;
            }

            set
            {
                _Descripcion = value;
            }
        }

        public int intIdCriterioProvHistorico
        {
            get
            {
                return _IdCriterioProvHistorico;
            }

            set
            {
                _IdCriterioProvHistorico = value;
            }
        }

        public decimal intValorParametro
        {
            get
            {
                return _valorParametro;
            }

            set
            {
                _valorParametro = value;
            }
        }

        #region
        public clsParametrosProvHistorico()
        {

        }

        public clsParametrosProvHistorico(int intId, string strDescripcion, int intIdCriterioProvHistorico, 
            int _IdUsuario, DateTime dtFechaRegistro, decimal intValorParametro)
        {
            this.intId = intId;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strDescripcion = strDescripcion;
            this.intIdCriterioProvHistorico = intIdCriterioProvHistorico;
            this.intValorParametro = intValorParametro;
        }
        #endregion
    }
}