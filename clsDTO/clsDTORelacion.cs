using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTORelacion
    {
        private string strIdPerfil;
        private string strNombrePerfil;
        private string strIdRelacion;
        private string strIdConfCampo;
        private string strNombreCampo;
        private string strPosicion;
        private bool booActivo;

        public string StrIdPerfil
        {
            get { return strIdPerfil; }
            set { strIdPerfil = value; }
        }

        public string StrNombrePerfil
        {
            get { return strNombrePerfil; }
            set { strNombrePerfil = value; }
        }

        public string StrIdRelacion
        {
            get { return strIdRelacion; }
            set { strIdRelacion = value; }
        }

        public string StrIdConfCampo
        {
            get { return strIdConfCampo; }
            set { strIdConfCampo = value; }
        }

        public string StrNombreCampo
        {
            get { return strNombreCampo; }
            set { strNombreCampo = value; }
        }

        public string StrPosicion
        {
            get { return strPosicion; }
            set { strPosicion = value; }
        }

        public bool BooActivo
        {
            get { return booActivo; }
            set { booActivo = value; }
        }
        
        public clsDTORelacion() { }

        public clsDTORelacion(string strIdRelacion, string strIdPerfil, string strNombrePerfil, string strIdConfCampo, string strNombreCampo, string strPosicion, bool booActivo)
        {
            this.StrIdRelacion = strIdRelacion;
            this.StrIdPerfil = strIdPerfil;
            this.StrNombrePerfil = strNombrePerfil;
            this.StrIdConfCampo = strIdConfCampo;
            this.StrNombreCampo = strNombreCampo;
            this.StrPosicion = strPosicion;
            this.BooActivo = booActivo;
        }

    }
}
