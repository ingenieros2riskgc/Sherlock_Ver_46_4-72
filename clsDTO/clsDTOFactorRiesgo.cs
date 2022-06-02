using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOFactorRiesgo
    {
        #region Properties
        private string strIdFactorRiesgo;
        private string strCodigoFactorRiesgo;
        private string strDescFactorRiesgo;
        private string strIdUsuario;

        public string StrIdFactorRiesgo
        {
            get { return strIdFactorRiesgo; }
            set { strIdFactorRiesgo = value; }
        }

        public string StrCodigoFactorRiesgo
        {
            get { return strCodigoFactorRiesgo; }
            set { strCodigoFactorRiesgo = value; }
        }

        public string StrDescFactorRiesgo
        {
            get { return strDescFactorRiesgo; }
            set { strDescFactorRiesgo = value; }
        }

        public string StrIdUsuario
        {
            get { return strIdUsuario; }
            set { strIdUsuario = value; }
        }

        #endregion

        public clsDTOFactorRiesgo()
        {
        }

        public clsDTOFactorRiesgo(string strIdFactorRiesgo, string strCodigoFactorRiesgo, string strDescFactorRiesgo, string strIdUsuario)
        {
            this.StrIdFactorRiesgo = strIdFactorRiesgo;
            this.StrCodigoFactorRiesgo = strCodigoFactorRiesgo;
            this.StrDescFactorRiesgo = strDescFactorRiesgo;
            this.strIdUsuario = strIdUsuario;
        }
    }

    public class clsDTOFactorSenal
    {
        #region Properties
        private string strIdFactorSenal;
        private string strIdFactorRiesgo;
        private string strIdSenal;

        public string StrIdFactorRiesgo
        {
            get { return strIdFactorRiesgo; }
            set { strIdFactorRiesgo = value; }
        }

        public string StrIdFactorSenal
        {
            get { return strIdFactorSenal; }
            set { strIdFactorSenal = value; }
        }

        public string StrIdSenal
        {
            get { return strIdSenal; }
            set { strIdSenal = value; }
        }

        #endregion

        public clsDTOFactorSenal()
        {
        }

        public clsDTOFactorSenal(string strIdFactorSenal, string strIdFactorRiesgo, string StrIdSenal)
        {
            this.StrIdFactorRiesgo = strIdFactorRiesgo;
            this.StrIdFactorSenal = strIdFactorSenal;
            this.StrIdSenal = StrIdSenal;
        }

    }

}
