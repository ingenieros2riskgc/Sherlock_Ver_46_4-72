using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOVariableMod
    {
        #region Properties
        private string strIdVariable;
        private string strNombreVariable;
        private string strCalificacion;
        private bool booActivo;
        private string strUsuario;
        private string strIdUsuario;
        private string strFechaModificacion;

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
        public string StrUsuario
        {
            get { return strUsuario; }
            set { strUsuario = value; }
        }
        public string StrIdUsuario
        {
            get { return strIdUsuario; }
            set { strIdUsuario = value; }
        }

        public string StrFechaModificacion
        {
            get { return strFechaModificacion; }
            set { strFechaModificacion = value; }
        }
        #endregion Properties

        public clsDTOVariableMod()
        {
        }

        public clsDTOVariableMod(string strIdVariable, string strNombreVariable, string strCalificacion, bool booActivo,
            string strUsuario, string strIdUsuario, string strFechaModificacion)
        {
            this.StrIdVariable = strIdVariable;
            this.StrNombreVariable = strNombreVariable;
            this.StrCalificacion = strCalificacion;
            this.BooActivo = booActivo;
            this.StrUsuario = strUsuario;
            this.StrIdUsuario = strIdUsuario;
            this.StrFechaModificacion = strFechaModificacion;
        }
    }
}
