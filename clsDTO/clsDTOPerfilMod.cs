using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOPerfilMod
    {
        private string strIdPerfil;
        private string strNombrePerfil;
        private string strValorMinimo;
        private string strValorMaximo;
        private string strUsuario;
        private string strIdUsuario;
        private string strFechaModificacion;

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

        public string StrValorMinimo
        {
            get { return strValorMinimo; }
            set { strValorMinimo = value; }
        }

        public string StrValorMaximo
        {
            get { return strValorMaximo; }
            set { strValorMaximo = value; }
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
        public clsDTOPerfilMod()
        {
        }

        public clsDTOPerfilMod(string strIdPerfil, string strNombrePerfil, string strValorMinimo, string strValorMaximo, 
            string strIdUsuario, string strUsuario, string strFechaModificacion)
        {
            this.StrIdPerfil = strIdPerfil;
            this.StrNombrePerfil = strNombrePerfil;
            this.StrValorMinimo = strValorMinimo;
            this.StrValorMaximo = strValorMaximo;
            this.StrUsuario = strUsuario;
            this.StrIdUsuario = strIdUsuario;
            this.StrFechaModificacion = strFechaModificacion;
        }
    }
}
