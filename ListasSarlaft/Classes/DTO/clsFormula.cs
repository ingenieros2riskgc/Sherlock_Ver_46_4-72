using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsFormula
    {
        private int _Id;
        private int _IdOperando;
        private string _NombreOperando;
        private int _IdIndicador;
        private string _Valor;
        private int _Posicion;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intOperando
        {
            get { return _IdOperando; }
            set { _IdOperando = value; }
        }

        public string strNombreOperando
        {
            get { return _NombreOperando; }
            set { _NombreOperando = value; }
        }

        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }

        public string strValor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        public int intPosicion
        {
            get { return _Posicion; }
            set { _Posicion = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsFormula()
        {
        }

        public clsFormula(int intId, int intOperando, int intIdIndicador, string strValor, int intPosicion, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intOperando = intOperando;
            this.intIdIndicador = intIdIndicador;
            this.strValor = strValor;
            this.intPosicion = intPosicion;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }

        public clsFormula(int intId, int intOperando, string strNombreOperando, int intIdIndicador, string strValor, int intPosicion, 
            int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intOperando = intOperando;
            this.intIdIndicador = intIdIndicador;
            this.strValor = strValor;
            this.intPosicion = intPosicion;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.strNombreOperando = strNombreOperando;
            this.dtFechaRegistro = dtFechaRegistro;
        }
        #endregion
    }
}