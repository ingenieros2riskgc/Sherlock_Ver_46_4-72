using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDetalleEvaluacionDesempeño
    {
        private int _IdDetalle;
        private string _NombreFactor;
        private decimal _valorFactor;
        private int _IdEvaluacionDesempeño;

        public int intIdDetalle
        {
            get { return _IdDetalle; }
            set { _IdDetalle = value; }
        }

        public string strNombreFactor
        {
            get { return _NombreFactor; }
            set { _NombreFactor = value; }
        }

        public decimal intvalorFactor
        {
            get { return _valorFactor; }
            set { _valorFactor = value; }
        }

        public int intIdEvaluacionDesempeño
        {
            get { return _IdEvaluacionDesempeño; }
            set { _IdEvaluacionDesempeño = value; }
        }
        #region Constructors
        public clsDetalleEvaluacionDesempeño()
        {
        }

        public clsDetalleEvaluacionDesempeño(int intIdDetalle, string strNombreFactor, decimal intvalorFactor, int intIdEvaluacionDesempeño)
        {
            this.intIdDetalle = intIdDetalle;
            this.strNombreFactor = strNombreFactor;
            this.intvalorFactor = intvalorFactor;
            this.intIdEvaluacionDesempeño = intIdEvaluacionDesempeño;
        }
        #endregion
    }
}