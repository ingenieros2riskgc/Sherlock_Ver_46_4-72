using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOVariable
    {
        #region Properties
        private string strIdUsuario;
        private string strIdVariable;
        private string strNombreVariable;
        private string strCalificacion;
        private bool booActivo;
        public string StrIdUsuario
        {
            get { return strIdUsuario; }
            set { strIdUsuario = value; }
        }

        public string StrIdVariable
        {
            get { return strIdVariable; }
            set { strIdVariable = value; }
        }

        public string StrNombreVariable
        {
            get { return strNombreVariable; }
            set { strNombreVariable = value; }
        }

        public string StrCalificacion
        {
            get { return strCalificacion; }
            set { strCalificacion = value; }
        }

        public bool BooActivo
        {
            get { return booActivo; }
            set { booActivo = value; }
        }
        #endregion Properties

        public clsDTOVariable()
        {
        }

        public clsDTOVariable(string strIdUsuario, string strIdVariable, string strNombreVariable, string strCalificacion, bool booActivo)
        {
            this.StrIdUsuario = strIdUsuario;
            this.StrIdVariable = strIdVariable;
            this.StrNombreVariable = strNombreVariable;
            this.StrCalificacion = strCalificacion;
            this.BooActivo = booActivo;
        }
    }
}
