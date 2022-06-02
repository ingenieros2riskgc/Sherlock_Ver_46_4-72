using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOActualizarGestionRequerimientos
    {
        private string IdGESREQ;
        private string numeroREQ;
        private string grupoAsignado;
        private string encargado;
        private string estado;
        private string criticidad;
        private string fechaVencimientoGESREQ;
        private string comentario;

        public string idGESREQ
        {
            get { return IdGESREQ; }
            set { IdGESREQ = value; }
        }
        public string NumeroREQ
        {
            get { return numeroREQ; }
            set { numeroREQ = value; }
        }
        public string GrupoAsignado
        {
            get { return grupoAsignado; }
            set { grupoAsignado = value; }
        }
        public string Encargado
        {
            get { return encargado; }
            set { encargado = value; }
        }
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        public string Criticidad
        {
            get { return criticidad; }
            set { criticidad = value; }
        }
        public string FechaVencimientoGESREQ
        {
            get { return fechaVencimientoGESREQ; }
            set { fechaVencimientoGESREQ = value; }
        }

        public string Comentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        public clsDTOActualizarGestionRequerimientos(string IdGESREQ, string numeroREQ, string grupoAsignado, string encargado, 
            string estado, string criticidad, string fechaVencimientoGESREQ, string comentario)
        {
            this.idGESREQ = IdGESREQ;
            this.NumeroREQ = numeroREQ;
            this.GrupoAsignado = grupoAsignado;
            this.Encargado = encargado;
            this.Estado = estado;
            this.Criticidad = criticidad;
            this.FechaVencimientoGESREQ = fechaVencimientoGESREQ;
            this.Comentario = comentario;
        }
        public clsDTOActualizarGestionRequerimientos()
        {
        }

    }
}
