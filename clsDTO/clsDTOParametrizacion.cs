using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOParametrizacion : clsDTOVariable
    {
        #region Properties
        private string strIdUsuario;
        private string strIdCategoria;
        private string strNombreCategoria;
        private string strCodigoCategoria;
        private string strCalificacionCategoria;
        private bool booEsFormula;
        public string StrIdUsuario
        {
            get { return strIdUsuario; }
            set { strIdUsuario = value; }
        }

        public string StrIdCategoria
        {
            get { return strIdCategoria; }
            set { strIdCategoria = value; }
        }

        public string StrNombreCategoria
        {
            get { return strNombreCategoria; }
            set { strNombreCategoria = value; }
        }

        public string StrCalificacionCategoria
        {
            get { return strCalificacionCategoria; }
            set { strCalificacionCategoria = value; }
        }

        public string StrCodigoCategoria
        {
            get { return strCodigoCategoria; }
            set { strCodigoCategoria = value; }
        }

        public bool BooEsFormula
        {
            get { return booEsFormula; }
            set { booEsFormula = value; }
        }
        #endregion Properties

        public clsDTOParametrizacion() { }

        public clsDTOParametrizacion(string strIdUsuario, string strIdParametros, string strIdVariable, string strNombreParametro, 
            string strCodigoParametro, string strCalificacionParametro, string strNombreTipoParametro, bool booEsFormula)
        {
            this.StrIdUsuario = strIdUsuario;
            this.StrIdVariable = strIdVariable;
            this.StrNombreVariable = strNombreTipoParametro;
            this.StrCodigoCategoria = strCodigoParametro;
            this.StrIdCategoria = strIdParametros;
            this.StrNombreCategoria = strNombreParametro;
            this.StrCalificacionCategoria = strCalificacionParametro;
            this.BooEsFormula = booEsFormula;
        }
    }
}
