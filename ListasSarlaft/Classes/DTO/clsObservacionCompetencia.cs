using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsObservacionProveedor
    {
        private int _Id;
        private int _IdEvaluacionProveedor;
        private string _Descripcion;
        private string _FechaProximaEvaluacion;
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

        public int intIdEvaluacionProveedor
        {
            get
            {
                return _IdEvaluacionProveedor;
            }

            set
            {
                _IdEvaluacionProveedor = value;
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

        public string dtFechaProximaEvaluacion
        {
            get
            {
                return _FechaProximaEvaluacion;
            }

            set
            {
                _FechaProximaEvaluacion = value;
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
        public clsObservacionProveedor()
        {

        }

        public clsObservacionProveedor(int intId, int intIdEvaluacionProveedor, string strDescripcion,
            string dtFechaProximaEvaluacion, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdEvaluacionProveedor = intIdEvaluacionProveedor;
            this.strDescripcion = strDescripcion;
            this.dtFechaProximaEvaluacion = dtFechaProximaEvaluacion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        public clsObservacionProveedor(string strDescripcion, string dtFechaProximaEvaluacion)
        {
            
            this.strDescripcion = strDescripcion;
            this.dtFechaProximaEvaluacion = dtFechaProximaEvaluacion;
        }
        #endregion
    }
}