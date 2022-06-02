using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaProbabilidad
    {
        #region Variables
        private string _NombreProbabilidad1;
        private string _NombreProbabilidad2;
        private string _NombreProbabilidad3;
        private string _NombreProbabilidad4;
        private string _NombreProbabilidad5;
        #endregion Variables
        #region Get/Set
        public string strNombreProbabilidad1
        {
            get { return _NombreProbabilidad1; }
            set { _NombreProbabilidad1 = value; }
        }
        public string strNombreProbabilidad2
        {
            get { return _NombreProbabilidad2; }
            set { _NombreProbabilidad2 = value; }
        }
        public string strNombreProbabilidad3
        {
            get { return _NombreProbabilidad3; }
            set { _NombreProbabilidad3 = value; }
        }
        public string strNombreProbabilidad4
        {
            get { return _NombreProbabilidad4; }
            set { _NombreProbabilidad4 = value; }
        }
        public string strNombreProbabilidad5
        {
            get { return _NombreProbabilidad5; }
            set { _NombreProbabilidad5 = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaProbabilidad() { }
        #endregion Constructor
    }
}