using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOPerfilVariable
    {
        #region Properties
        private string strIdPerfilVariable;
        private string strIdPerfil;
        private string strIdVariable;

        public string StrIdPerfilVariable
        {
            get { return strIdPerfilVariable; }
            set { strIdPerfilVariable = value; }
        }

        public string StrIdPerfil
        {
            get { return strIdPerfil; }
            set { strIdPerfil = value; }
        }

        public string StrIdVariable
        {
            get { return strIdVariable; }
            set { strIdVariable = value; }
        }

        #endregion

        public clsDTOPerfilVariable()
        {
        }

        public clsDTOPerfilVariable(string strIdPerfilVariable, string strIdPerfil, string strIdVariable)
        {
            this.StrIdPerfilVariable = strIdPerfilVariable;
            this.StrIdPerfil = strIdPerfil;
            this.StrIdVariable = strIdVariable;
        }
    }

}
