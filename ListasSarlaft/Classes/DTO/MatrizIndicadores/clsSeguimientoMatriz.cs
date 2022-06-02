using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsSeguimientoMatriz
    {
        private string _Periodo;
        private decimal _Cumplimiento;

        public string Periodo
        {
            get { return _Periodo; }
            set { _Periodo = value; }
        }
        public decimal Cumplimiento
        {
            get { return _Cumplimiento; }
            set { _Cumplimiento = value; }
        }
    }
}