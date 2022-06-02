using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsResultadoEvaluacionDesempeno
    {
        private int _Id;
        private int _IdEvaluacionDesempeno;
        private int _IdFactorDesempeno;
        private int _Puntaje;
        private DateTime _FechaRegistro;
        private int _IdUsuario;

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

        public int intIdEvaluacionDesempeno
        {
            get
            {
                return _IdEvaluacionDesempeno;
            }

            set
            {
                _IdEvaluacionDesempeno = value;
            }
        }

        public int intIdFactorDesempeno
        {
            get
            {
                return _IdFactorDesempeno;
            }

            set
            {
                _IdFactorDesempeno = value;
            }
        }

        public int intPuntaje
        {
            get
            {
                return _Puntaje;
            }

            set
            {
                _Puntaje = value;
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

        #region
        public clsResultadoEvaluacionDesempeno()
        {

        }

        public clsResultadoEvaluacionDesempeno(int intId, int intIdEvaluacionDesempeno, int intIdFactorDesempeno, int intPuntaje,
            int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdEvaluacionDesempeno = intIdEvaluacionDesempeno;
            this.intIdFactorDesempeno = intIdFactorDesempeno;
            this.intPuntaje = intPuntaje;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}