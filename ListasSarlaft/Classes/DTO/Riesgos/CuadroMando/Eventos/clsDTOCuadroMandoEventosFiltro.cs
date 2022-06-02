using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroMandoEventosFiltro
    {
        #region Variables
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        #endregion Variables
        #region Get/Set
        public DateTime dtFechaInicial
        {
            get { return _FechaInicial; }
            set { _FechaInicial = value; }
        }
        public DateTime dtFechaFinal
        {
            get { return _FechaFinal; }
            set { _FechaFinal = value; }
        }
        
        #endregion Get/Set
        #region Constructor
        public clsDTOCuadroMandoEventosFiltro() { }
        #endregion Constructor
    }
}