using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaDesviaciones
    {
        #region Variables
        private int _DesviacionProbabilidad1;
        private int _DesviacionProbabilidad2;
        private int _DesviacionProbabilidad3;
        private int _DesviacionProbabilidad4;

        private int _DesviacionImpacto1;
        private int _DesviacionImpacto2;
        private int _DesviacionImpacto3;
        private int _DesviacionImpacto4;
        #endregion Variables
        #region Get/Set
        public int intDesviacionProbabilidad1
        {
            get { return _DesviacionProbabilidad1; }
            set { _DesviacionProbabilidad1 = value; }
        }
        public int intDesviacionProbabilidad2
        {
            get { return _DesviacionProbabilidad2; }
            set { _DesviacionProbabilidad2 = value; }
        }
        public int intDesviacionProbabilidad3
        {
            get { return _DesviacionProbabilidad3; }
            set { _DesviacionProbabilidad3 = value; }
        }
        public int intDesviacionProbabilidad4
        {
            get { return _DesviacionProbabilidad4; }
            set { _DesviacionProbabilidad4 = value; }
        }

        public int intDesviacionImpacto1
        {
            get { return _DesviacionImpacto1; }
            set { _DesviacionImpacto1 = value; }
        }
        public int intDesviacionImpacto2
        {
            get { return _DesviacionImpacto2; }
            set { _DesviacionImpacto2 = value; }
        }
        public int intDesviacionImpacto3
        {
            get { return _DesviacionImpacto3; }
            set { _DesviacionImpacto3 = value; }
        }
        public int intDesviacionImpacto4
        {
            get { return _DesviacionImpacto4; }
            set { _DesviacionImpacto4 = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaDesviaciones() { }
        #endregion Constructor
    }
}