using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOGrupos
    {
        
        private int idGrupoSoporte;
        private string nombreGrupoSoporte;
       
        public int IdGrupoSoporte
        {
            get { return idGrupoSoporte; }
            set { idGrupoSoporte = value; }
        }
        public string NombreGruposoporte
        {
            get { return nombreGrupoSoporte; }
            set { nombreGrupoSoporte = value; }
        }

        public clsDTOGrupos() { }
        
    }
}