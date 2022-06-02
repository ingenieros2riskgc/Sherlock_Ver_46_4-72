using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOTipoDato
    {
        #region Properties
        private string strIdTipoDato;
        private string strNombreTipoDato;

        public string StrIdTipoDato
        {
            get { return strIdTipoDato; }
            set { strIdTipoDato = value; }
        }

        public string StrNombreTipoDato
        {
            get { return strNombreTipoDato; }
            set { strNombreTipoDato = value; }
        }
        #endregion Properties
    }
}
