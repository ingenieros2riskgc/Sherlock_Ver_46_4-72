using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOGestionRequerimientos
    {
        private string IdGESREQ;
        private string usuario;
        private string empresa;
        private string numeroREQ;
        private string fechaCreacionGESREQ;
        private string tipoFalla;
        private string detalleTipoFalla;
        private string descripcion;
        private string ruta;
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
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public string Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }
        public string NumeroREQ
        {
            get { return numeroREQ; }
            set { numeroREQ = value; }
        }
        public string FechaCreacionGESREQ
        {
            get { return fechaCreacionGESREQ; }
            set { fechaCreacionGESREQ = value; }
        }
        public string TipoFalla
        {
            get { return tipoFalla; }
            set { tipoFalla = value; }
        }
        public string DetalleTipoFalla
        {
            get { return detalleTipoFalla; }
            set { detalleTipoFalla = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string Ruta
        {
            get { return ruta; }
            set { ruta = value; }
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

        public clsDTOGestionRequerimientos(string IdGESREQ, string usuario, string empresa, string numeroREQ, string fechaCreacionGESREQ, 
            string tipoFalla, string detalleTipoFalla, string descripcion, string ruta, string grupoAsignado, string encargado, 
            string estado, string criticidad, string fechaVencimientoGESREQ, string comentario)
        {
            this.idGESREQ = IdGESREQ;
            this.Usuario = usuario;
            this.Empresa = empresa;
            this.NumeroREQ = numeroREQ;
            this.FechaCreacionGESREQ = fechaCreacionGESREQ;
            this.TipoFalla = tipoFalla;
            this.DetalleTipoFalla = detalleTipoFalla;
            this.Descripcion = descripcion;
            this.Ruta = ruta;
            this.GrupoAsignado = grupoAsignado;
            this.Encargado = encargado;
            this.Estado = estado;
            this.Criticidad = criticidad;
            this.FechaVencimientoGESREQ = fechaVencimientoGESREQ;
            this.Comentario = comentario;
        }
        public clsDTOGestionRequerimientos()
        {
        }

    }
}
