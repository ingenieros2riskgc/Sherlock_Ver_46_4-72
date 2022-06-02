using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsProcesoIndicador
    {
        private int _Id;
        private int _IdTipoProceso;
        private int _IdProceso;
        private string _FechaRegistro;
        private int _IdUsuario;
        private int _periodoanual;
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

        public int intIdTipoProceso
        {
            get
            {
                return _IdTipoProceso;
            }

            set
            {
                _IdTipoProceso = value;
            }
        }

        public int intIdProceso
        {
            get
            {
                return _IdProceso;
            }

            set
            {
                _IdProceso = value;
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

        public int intPeridoAnual
        {
            get
            {
                return _periodoanual;
            }

            set
            {
                _periodoanual = value;
            }
        }

        #region
        public clsProcesoIndicador()
        {

        }

        public clsProcesoIndicador(int intId, int intIdTipoProceso, int intIdProceso, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdTipoProceso = intIdTipoProceso;
            this.intIdProceso = intIdProceso;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsProcesoIndicador(int intId, int intIdTipoProceso, int intIdProceso, int intIdUsuario, string dtFechaRegistro, int intPriodoAnual)
        {
            this.intId = intId;
            this.intIdTipoProceso = intIdTipoProceso;
            this.intIdProceso = intIdProceso;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.intPeridoAnual = intPeridoAnual;
        }
        #endregion
    }
}