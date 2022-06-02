using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOGestionGrupos
    {
        private string strNombreGrupoTrabajo;
        private string strIdGrupoTrabajo;

        public string StrIdGrupoTrabajo
        {
            get { return strIdGrupoTrabajo; }
            set { strIdGrupoTrabajo = value; }
        }

        public string StrNombreGrupoTrabajo
        {
            get { return strNombreGrupoTrabajo; }
            set { strNombreGrupoTrabajo = value; }
        }

        public clsDTOGestionGrupos(
            string strIdGrupoTrabajo,
            string strNombreGrupoTrabajo)
        {
            this.StrIdGrupoTrabajo = strIdGrupoTrabajo;
            this.StrNombreGrupoTrabajo = strNombreGrupoTrabajo;
        }

        public clsDTOGestionGrupos()
        {
        }
    }
}
