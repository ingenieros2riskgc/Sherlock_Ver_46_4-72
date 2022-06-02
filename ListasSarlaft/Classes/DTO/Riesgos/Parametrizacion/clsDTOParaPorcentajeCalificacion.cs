using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaPorcentajeCalificacion
    {
        #region Variables
        private int _ValorPorcentajeCalificarControl1;
        private int _ValorPorcentajeCalificarControl2;
        private int _ValorPorcentajeCalificarControl3;
        private int _ValorPorcentajeCalificarControl4;
        private int _ValorPorcentajeCalificarControl5;
        #endregion Variables
        #region Get/Set
        public int intValorPorcentajeCalificarControl1
        {
            get { return _ValorPorcentajeCalificarControl1; }
            set { _ValorPorcentajeCalificarControl1 = value; }
        }
        public int intValorPorcentajeCalificarControl2
        {
            get { return _ValorPorcentajeCalificarControl2; }
            set { _ValorPorcentajeCalificarControl2 = value; }
        }
        public int intValorPorcentajeCalificarControl3
        {
            get { return _ValorPorcentajeCalificarControl3; }
            set { _ValorPorcentajeCalificarControl3 = value; }
        }
        public int intValorPorcentajeCalificarControl4
        {
            get { return _ValorPorcentajeCalificarControl4; }
            set { _ValorPorcentajeCalificarControl4 = value; }
        }
        public int intValorPorcentajeCalificarControl5
        {
            get { return _ValorPorcentajeCalificarControl5; }
            set { _ValorPorcentajeCalificarControl5 = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaPorcentajeCalificacion() { }
        #endregion Constructor
    }
}