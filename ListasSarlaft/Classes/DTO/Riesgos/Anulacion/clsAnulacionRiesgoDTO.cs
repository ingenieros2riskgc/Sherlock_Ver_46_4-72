using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAnulacionRiesgoDTO
    {
        #region Variables
        private int _IdEventoRiesgo;
        private int _IdRiesgo;
        private int _IdEvento;
        private string _CodigoEvento;
        private string _DescripcionEvento;
        #endregion Variables
        #region Get/Set
        public int intIdEventoRiesgo
        {
            get { return _IdEventoRiesgo; }
            set { _IdEventoRiesgo = value; }
        }
        public int intIdRiesgo
        {
            get { return _IdRiesgo; }
            set { _IdRiesgo = value; }
        }
        public int intIdEvento
        {
            get { return _IdEvento; }
            set { _IdEvento = value; }
        }
        public string strCodigoEvento
        {
            get { return _CodigoEvento; }
            set { _CodigoEvento = value; }
        }
        public string strDescripcionEvento
        {
            get { return _DescripcionEvento; }
            set { _DescripcionEvento = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsAnulacionRiesgoDTO() { }
        #endregion Constructor
    }
}