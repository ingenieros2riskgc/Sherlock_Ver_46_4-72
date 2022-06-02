using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsSeguimientoIndicador
    {
        private int _Id;
        private int _Responsable;
        private int _IdIndicador;
        private string _FechaRegistro;
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

        public int IdCargoResponsable
        {
            get
            {
                return _Responsable;
            }

            set
            {
                _Responsable = value;
            }
        }

        public int intIdIndicador
        {
            get
            {
                return _IdIndicador;
            }

            set
            {
                _IdIndicador = value;
            }
        }

        public string dtFechaRegistro
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
        public clsSeguimientoIndicador()
        {

        }

        public clsSeguimientoIndicador(int intId, int IdCargoResponsable, int intIdIndicador, int intIdUsuario,
            string dtFechaRegistro)
        {
            this.intId = intId;
            this.IdCargoResponsable = IdCargoResponsable;
            this.intIdIndicador = intIdIndicador;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}