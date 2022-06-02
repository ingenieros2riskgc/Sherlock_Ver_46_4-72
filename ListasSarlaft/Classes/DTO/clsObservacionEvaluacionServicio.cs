using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsObservacionEvaluacionServicio
    {
        private int _IdEvaluacionServicio;
        private int _Id;
        private string _Descripcion;
        private int _IdUsuario;
        private DateTime _FechaRegistro;

        public int intIdEvaluacionServicio
        {
            get
            {
                return _IdEvaluacionServicio;
            }

            set
            {
                _IdEvaluacionServicio = value;
            }
        }

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

        #region
        public clsObservacionEvaluacionServicio()
        {

        }

        public clsObservacionEvaluacionServicio(int intId, int intIdEvaluacionServicio, string strDescripcion,
            int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intIdEvaluacionServicio = intIdEvaluacionServicio;
            this.intId = intId;
            this.strDescripcion = strDescripcion;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }
        #endregion
    }
}