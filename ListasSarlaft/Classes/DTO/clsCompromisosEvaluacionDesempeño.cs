using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCompromisosEvaluacionDesempeno
    {
        private int _Id;
        private int _IdEvaluacionDesempeno;
        private string _Descripcion;
        private int _IdUsuario;
        private DateTime _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdEvaluacionDesempeno
        {
            get { return _IdEvaluacionDesempeno; }
            set { _IdEvaluacionDesempeno = value; }
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
        public clsCompromisosEvaluacionDesempeno()
        {
        }

        public clsCompromisosEvaluacionDesempeno(int intId, int intIdEvaluacionDesempeno, string strDescripcion, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdEvaluacionDesempeno = intIdEvaluacionDesempeno;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}