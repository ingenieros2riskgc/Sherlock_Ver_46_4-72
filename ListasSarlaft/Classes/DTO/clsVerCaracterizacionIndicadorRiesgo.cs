using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsVerCaracterizacionIndicadorRiesgo
    {
        private string _NombreIndicador;
        private string _DescripcionIndicador;
        private int _IdIndicador;
        private string _CodigoControl;
        private string _NombreControl;

        public string strNombreIndicador
        {
            get { return _NombreIndicador; }
            set { _NombreIndicador = value; }
        }
        public string strDescripcionIndicador
        {
            get { return _DescripcionIndicador; }
            set { _DescripcionIndicador = value; }
        }
        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }

        public string strCodigoControl
        {
            get { return _CodigoControl; }
            set { _CodigoControl = value; }
        }
        public string strNombreControl
        {
            get { return _NombreControl; }
            set { _NombreControl = value; }
        }
        #region Constructors
        public clsVerCaracterizacionIndicadorRiesgo()
        {
        }

        public clsVerCaracterizacionIndicadorRiesgo(string strNombreIndicador, string strDescripcionIndicador, int intIdIndicador, 
             string strCodigoControl, string strNombreControl)
        {
            this.strNombreIndicador = strNombreIndicador;
            this.strDescripcionIndicador = strDescripcionIndicador;
            this.intIdIndicador = intIdIndicador;
            this.strCodigoControl = strCodigoControl;
            this.strNombreControl = strNombreControl;
        }
        public clsVerCaracterizacionIndicadorRiesgo(string strNombreIndicador, string strDescripcionIndicador, int intIdIndicador)
        {
            this.strNombreIndicador = strNombreIndicador;
            this.strDescripcionIndicador = strDescripcionIndicador;
            this.intIdIndicador = intIdIndicador;
        }
        #endregion
    }
}