using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOParametrizacionMod : clsDTOVariable
    {
        #region Properties
        private string strIdCategoria;
        private string strNombreCategoria;
        private string strCodigoCategoria;
        private string strCalificacionCategoria;
        private bool booEsFormula;
        private string strUsuario;
        private string strIdUsuario;
        private string strFechaModificacion;

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

        public clsDTOParametrizacionMod() { }

        public clsDTOParametrizacionMod(string strIdParametros, string strIdVariable, string strNombreParametro, string strCodigoParametro,
            string strCalificacionParametro, string strNombreTipoParametro, bool booEsFormula, string strUsuario,
            string strIdUsuario, string strFechaModificacion)
        {
            this.StrIdVariable = strIdVariable;
            this.StrNombreVariable = strNombreTipoParametro;
            this.StrCodigoCategoria = strCodigoParametro;
            this.StrIdCategoria = strIdParametros;
            this.StrNombreCategoria = strNombreParametro;
            this.StrCalificacionCategoria = strCalificacionParametro;
            this.BooEsFormula = booEsFormula;
            this.StrUsuario = strUsuario;
            this.StrIdUsuario = strIdUsuario;
            this.StrFechaModificacion = strFechaModificacion;
        }
    }
}
