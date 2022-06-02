using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOArchivo
    {

        #region Properties
        private string strIdArchivo;
        private string strFechaRegistro;
        private string strUrlArchivo;
        private byte[] bArchivo;

        public string StrIdArchivo
        {
            get { return strIdArchivo; }
            set { strIdArchivo = value; }
        }

        public string StrUrlArchivo
        {
            get { return strUrlArchivo; }
            set { strUrlArchivo = value; }
        }

        public string StrFechaRegistro
        {
            get { return strFechaRegistro; }
            set { strFechaRegistro = value; }
        }

        public byte[] BArchivo
        {
            get { return bArchivo; }
            set { bArchivo = value; }
        }
        #endregion Properties

        public clsDTOArchivo() { }

        public clsDTOArchivo(string strIdArchivo, string strFechaRegistro, string strUrlArchivo, byte[] bArchivo)
        {
            this.StrIdArchivo = strIdArchivo;
            this.StrFechaRegistro = strFechaRegistro;
            this.StrUrlArchivo = strUrlArchivo;
            this.BArchivo = bArchivo;
        }
    }
}
