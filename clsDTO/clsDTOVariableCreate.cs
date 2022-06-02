using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOVariableCreate
    {
        #region Properties
        private string strIdUsuario;
        private string strNombreVariable;
        private string strCalificacion;
        private bool booActivo;
        public string StrIdUsuario
        {
            get { return strIdUsuario; }
            set { strIdUsuario = value; }
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

        public clsDTOVariableCreate()
        {
        }

        public clsDTOVariableCreate(string strIdUsuario, string strNombreVariable, string strCalificacion, bool booActivo)
        {
            this.StrIdUsuario = strIdUsuario;
            this.StrNombreVariable = strNombreVariable;
            this.StrCalificacion = strCalificacion;
            this.BooActivo = booActivo;
        }
    }
}
