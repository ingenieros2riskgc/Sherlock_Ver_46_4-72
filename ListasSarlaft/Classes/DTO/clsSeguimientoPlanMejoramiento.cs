using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsSeguimientoPlanMejoramiento
    {
        private int _Id;
        private int _IdDetallePlanMejoramiento;
        private string _Descripcion;
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

        public int intIdDetallePlanMejoramiento
        {
            get
            {
                return _IdDetallePlanMejoramiento;
            }

            set
            {
                _IdDetallePlanMejoramiento = value;
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
        public clsSeguimientoPlanMejoramiento()
        {

        }

        public clsSeguimientoPlanMejoramiento(int intId, int intIdDetallePlanMejoramiento, string strDescripcion, int intIdUsuario,
            DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdDetallePlanMejoramiento = intIdDetallePlanMejoramiento;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}