using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOPorcentajeCalificacion
    {
        #region Variables
        private int _IdPorcentajeCalificarControl;
        private string _NombrePorcentajeCalificarControl;
        private int _ValorPorcentajeCalificarControl;
        #endregion Variables
        #region Get/Set
        public int intIdPorcentajeCalificarControl
        {
            get { return _IdPorcentajeCalificarControl; }
            set { _IdPorcentajeCalificarControl = value; }
        }
        public string strNombrePorcentajeCalificarControl
        {
            get { return _NombrePorcentajeCalificarControl; }
            set { _NombrePorcentajeCalificarControl = value; }
        }
        public int intValorPorcentajeCalificarControl
        {
            get { return _ValorPorcentajeCalificarControl; }
            set { _ValorPorcentajeCalificarControl = value; }
        }
    
    #endregion Get/Set
    #region Constructor
    public clsDTOPorcentajeCalificacion(){}
    #endregion Constructor
}
}