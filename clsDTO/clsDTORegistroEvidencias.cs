using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTORegistroEvidencias
    {
        private string strIdEvidencia;
        private string urlArchivo;
        private string descripcion;
        private string strFechaRegistroEvidencia;

        public string StrIdEvidencia
        {
            get { return strIdEvidencia; }
            set { strIdEvidencia = value; }
        }
        public string URLArchivo
        {
            get { return urlArchivo; }
            set { urlArchivo = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string StrFechaRegistroEvidencia
        {
            get { return strFechaRegistroEvidencia; }
            set { strFechaRegistroEvidencia = value; }
        }

        public clsDTORegistroEvidencias(string strIdEvidencia, string urlArchivo, string descripcion, string strFechaRegistroEvidencia)
        {
            this.StrIdEvidencia = strIdEvidencia;
            this.URLArchivo = urlArchivo;
            this.Descripcion = descripcion;
            this.StrFechaRegistroEvidencia = strFechaRegistroEvidencia;
        }

        public clsDTORegistroEvidencias()
        {
        }
    }
}
