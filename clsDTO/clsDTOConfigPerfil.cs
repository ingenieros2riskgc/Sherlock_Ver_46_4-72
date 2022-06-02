using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOConfigPerfil
    {
        private string strIdPerfil;
        private string strConfigPerfil;
        private string strCalificacion;
        private string strIdEstructCampo;


        public string StrIdPerfil
        {
            get { return strIdPerfil; }
            set { strIdPerfil = value; }
        }

        public string StrConfigPerfil
        {
            get { return strConfigPerfil; }
            set { strConfigPerfil = value; }
        }

        public string StrCalificacion
        {
            get { return strCalificacion; }
            set { strCalificacion = value; }
        }

        public string StrIdEstructCampo
        {
            get { return strIdEstructCampo; }
            set { strIdEstructCampo = value; }
        }

        public clsDTOConfigPerfil()
        {
        }

        public clsDTOConfigPerfil(string strIdPerfil, string strConfigPerfil, string strCalificacion, string strIdEstructCampo)
        {
            this.StrIdPerfil = strIdPerfil;
            this.StrConfigPerfil = strConfigPerfil;
            this.StrCalificacion = strCalificacion;
            this.StrIdEstructCampo = strIdEstructCampo;
        }
    }
}
