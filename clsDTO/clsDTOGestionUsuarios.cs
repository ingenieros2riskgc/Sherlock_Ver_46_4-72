using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOGestionUsuarios
    {
        private string strIdIntegrante;
        private string strNombreIntegrante;
        private string strUsuarioIntegrante;
        private string strCorreoIntegrante;
        private string strIdGrupoTrabajo;

        public string StrIdIntegrante
        {
            get { return strIdIntegrante; }
            set { strIdIntegrante = value; }
        }
        public string StrNombreIntegrante
        {
            get { return strNombreIntegrante; }
            set { strNombreIntegrante = value; }
        }
        public string StrUsuarioIntegrante
        {
            get { return strUsuarioIntegrante; }
            set { strUsuarioIntegrante = value; }
        }
        public string StrCorreoIntegrante
        {
            get { return strCorreoIntegrante; }
            set { strCorreoIntegrante = value; }
        }
        public string StrIdGrupoTrabajo
        {
            get { return strIdGrupoTrabajo; }
            set { strIdGrupoTrabajo = value; }
        }

        public clsDTOGestionUsuarios(string strIdIntegrante, string strNombreIntegrante, string strUsuarioIntegrante, 
            string strCorreoIntegrante, string strIdGrupoTrabajo)
        {
            this.StrIdIntegrante = strIdIntegrante;
            this.StrNombreIntegrante = strNombreIntegrante;
            this.StrUsuarioIntegrante = strUsuarioIntegrante;
            this.StrCorreoIntegrante = strCorreoIntegrante;
            this.StrIdGrupoTrabajo = strIdGrupoTrabajo;
        }

        public clsDTOGestionUsuarios()
        {
        }
    }
}
