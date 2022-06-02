using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAcmCierreAdjuntos
    {
        private int _idAcmCierreAdjunto;
        private string _nombreArchivo;
        private string _pathFile;
        private string _extension;
        private byte[] _archivo;
        private int _idAcm;

        public int intidAcmCierreAdjunto
        {
            get { return _idAcmCierreAdjunto; }
            set { _idAcmCierreAdjunto = value; }
        }
        public string strnombreArchivo
        {
            get { return _nombreArchivo; }
            set { _nombreArchivo = value; }
        }
        public string strpathFile
        {
            get { return _pathFile; }
            set { _pathFile = value; }
        }
        public string strextension
        {
            get { return _extension; }
            set { _extension = value; }
        }
        public byte[] btArchivo
        {
            get { return _archivo; }
            set { _archivo = value; }
        }

        public int intIdAcm
        {
            get { return _idAcm; }
            set { _idAcm = value; }
        }
        #region Construtors
        public clsAcmCierreAdjuntos()
        {
        }

        public clsAcmCierreAdjuntos(int intidAcmCierreAdjunto, string strnombreArchivo, string strpathFile, string strextension, int intIdAcm)
        {
            this.intidAcmCierreAdjunto = intidAcmCierreAdjunto;
            this.strnombreArchivo = strnombreArchivo;
            this.strpathFile = strpathFile;
            this.strextension = strextension;
            this.intIdAcm = intIdAcm;
        }
        #endregion
    }
}