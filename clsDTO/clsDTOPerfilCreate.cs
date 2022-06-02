using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOPerfilCreate
    {
        private string strIdUsuario;
        private string strNombrePerfil;
        private string strValorMinimo;
        private string strValorMaximo;

        public string StrIdUsuario
        {
            get { return strIdUsuario; }
            set { strIdUsuario = value; }
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

        public clsDTOPerfilCreate()
        {
        }

        public clsDTOPerfilCreate(string strIdUsuario, string strNombrePerfil, string strValorMinimo, string strValorMaximo)
        {
            this.StrIdUsuario = strIdUsuario;
            this.StrNombrePerfil = strNombrePerfil;
            this.StrValorMinimo = strValorMinimo;
            this.StrValorMaximo = strValorMaximo;
        }
    }
}
