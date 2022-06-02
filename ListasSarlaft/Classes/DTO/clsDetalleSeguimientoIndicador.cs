using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsDetalleSeguimientoIndicador
    {
        private int _Id;
        private int _IdSeguimientoIndicador;
        private int _IdIndicador;
        private string _DescripcionAnalisis;
        private string _DescripcionAccionCorrectiva;
        private int _IdDetPeriodo;
        private int _PeriodoAnual;
        private string _FechaDescripcionAnalisis;
        private string _FechaAccionCorrectiva;
        private int _IdUsuario;
        private string _NombreUsuario;

        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int intPeriodoAnual
        {
            get { return _PeriodoAnual; }
            set { _PeriodoAnual = value; }
        }
        public int intIdSeguimientoIndicador
        {
            get { return _IdSeguimientoIndicador; }
            set { _IdSeguimientoIndicador = value; }
        }

        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }

        public string strDescripcionAnalisis
        {
            get { return _DescripcionAnalisis; }
            set { _DescripcionAnalisis = value; }
        }

        public string strDescripcionAccionCorrectiva
        {
            get { return _DescripcionAccionCorrectiva; }
            set { _DescripcionAccionCorrectiva = value; }
        }

        public int intIdDetPeriodo
        {
            get { return _IdDetPeriodo; }
            set { _IdDetPeriodo = value; }
        }

        public string dtFechaDescripcionAnalisis
        {
            get { return _FechaDescripcionAnalisis; }
            set { _FechaDescripcionAnalisis = value; }
        }

        public string dtFechaAccionCorrectiva
        {
            get { return _FechaAccionCorrectiva; }
            set { _FechaAccionCorrectiva = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        #region Constructors
        public clsDetalleSeguimientoIndicador()
        {
        }

        public clsDetalleSeguimientoIndicador(int intId, int intIdSeguimientoIndicador, 
            string strDescripcionAnalisis, string strDescripcionAccionCorrectiva,
            int intIdDetPeriodo, string dtFechaDescripcionAnalisis, string dtFechaAccionCorrectiva, 
            int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdSeguimientoIndicador = intIdSeguimientoIndicador;
            this.strDescripcionAnalisis = strDescripcionAnalisis;
            this.strDescripcionAccionCorrectiva = strDescripcionAccionCorrectiva;
            this.intIdDetPeriodo = intIdDetPeriodo;
            this.dtFechaDescripcionAnalisis = dtFechaDescripcionAnalisis;
            this.dtFechaAccionCorrectiva = dtFechaAccionCorrectiva;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsDetalleSeguimientoIndicador(int intId, int intIdSeguimientoIndicador, int intIdIndicador, 
            string strDescripcionAnalisis, string strDescripcionAccionCorrectiva,
            int intIdDetPeriodo, string dtFechaDescripcionAnalisis, string dtFechaAccionCorrectiva, 
            int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdSeguimientoIndicador = intIdSeguimientoIndicador;
            this.intIdIndicador = intIdIndicador;
            this.strDescripcionAnalisis = strDescripcionAnalisis;
            this.strDescripcionAccionCorrectiva = strDescripcionAccionCorrectiva;
            this.intIdDetPeriodo = intIdDetPeriodo;
            this.dtFechaDescripcionAnalisis = dtFechaDescripcionAnalisis;
            this.dtFechaAccionCorrectiva = dtFechaAccionCorrectiva;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }
        public clsDetalleSeguimientoIndicador(int intId, int intIdSeguimientoIndicador, int intIdIndicador,
            string strDescripcionAnalisis, string strDescripcionAccionCorrectiva,
            int intIdDetPeriodo, string dtFechaDescripcionAnalisis, string dtFechaAccionCorrectiva,
            int intIdUsuario, string strNombreUsuario, string dtFechaRegistro, int intPeriodoAnual)
        {
            this.intId = intId;
            this.intIdSeguimientoIndicador = intIdSeguimientoIndicador;
            this.intIdIndicador = intIdIndicador;
            this.strDescripcionAnalisis = strDescripcionAnalisis;
            this.strDescripcionAccionCorrectiva = strDescripcionAccionCorrectiva;
            this.intIdDetPeriodo = intIdDetPeriodo;
            this.dtFechaDescripcionAnalisis = dtFechaDescripcionAnalisis;
            this.dtFechaAccionCorrectiva = dtFechaAccionCorrectiva;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intPeriodoAnual = intPeriodoAnual;
        }
        #endregion
    }
}