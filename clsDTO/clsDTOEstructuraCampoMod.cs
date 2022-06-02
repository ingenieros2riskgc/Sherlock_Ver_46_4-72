using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOEstructuraCampoMod : clsDTOVariable
    {
        #region Properties
        private string strIdEstructCampo;
        private string strNombreCampo;
        private string strIdTipoDato;
        private string strLongitud;
        private bool booEsParametrico;
        private string strNombreParametro;
        //private string strNombreTipoDato;
        private string strPosicion;
        private bool booNumerico;
        private string strIdUsuario;
        private string strUsuario;
        private string strFechaModificacion;
        public string StrIdEstructCampo
        {
            get { return strIdEstructCampo; }
            set { strIdEstructCampo = value; }
        }

        public string StrNombreCampo
        {
            get { return strNombreCampo; }
            set { strNombreCampo = value; }
        }

        public string StrIdTipoDato
        {
            get { return strIdTipoDato; }
            set { strIdTipoDato = value; }
        }

        public string StrLongitud
        {
            get { return strLongitud; }
            set { strLongitud = value; }
        }

        public bool BooEsParametrico
        {
            get { return booEsParametrico; }
            set { booEsParametrico = value; }
        }
        public string StrNombreParametro
        {
            get { return strNombreParametro; }
            set { strNombreParametro = value; }
        }

        public string StrPosicion
        {
            get { return strPosicion; }
            set { strPosicion = value; }
        }

        public int Estado { get; set; }

        public bool BoolNumerico
        {
            get { return booNumerico; }
            set { booNumerico = value; }
        }    

        public string StrIdUsuario
        {
            get { return strIdUsuario; }
            set { strIdUsuario = value; }
        }
        public string StrUsuario
        {
            get { return strUsuario; }
            set { strUsuario = value; }
        }

        public string StrFechaModificacion
        {
            get { return strFechaModificacion; }
            set { strFechaModificacion = value; }
        }
        #endregion Properties

        public clsDTOEstructuraCampoMod() { }

        public clsDTOEstructuraCampoMod(string strIdEstructCampo, string strNombreCampo, string strIdTipoDato, string strLongitud,
            bool booEsParametrico, string strNombreParametro, string strPosicion, string strIdVariable, string strNombreVariable,
            string strIdUsuario, string strUsuario, int estado, bool booNumerico, string strFechaModificacion)
        {
            this.StrIdEstructCampo = strIdEstructCampo;
            this.StrNombreCampo = strNombreCampo;
            this.StrIdTipoDato = strIdTipoDato;
            this.StrLongitud = strLongitud;
            this.BooEsParametrico = booEsParametrico;
            this.StrNombreParametro = strNombreParametro;
            this.StrPosicion = strPosicion;
            this.StrIdVariable = strIdVariable;
            this.StrNombreVariable = strNombreVariable;
            Estado = estado;
            this.BoolNumerico = booNumerico;
            this.StrIdUsuario = strIdUsuario;
            this.StrUsuario = strUsuario;
            this.StrFechaModificacion = strFechaModificacion;
        }

        public string mtdConsultarPosicion()
        {
            return this.strPosicion;
        }
    }
}
