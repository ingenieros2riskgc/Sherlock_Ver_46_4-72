using System;
using System.Web;

namespace ListasSarlaft.Classes.DTO
{
    public class clsVerCaracterizacionRiesgos
    {

        private string _CodigoRiesgo;
        private string _NombreRiesgo;
        private string _DescripcionRiesgo;
        private int _IdRiesgo;
        private string _CodigoControl;
        private string _NombreControl;

        public string strCodigoRiesgo
        {
            get { return _CodigoRiesgo; }
            set { _CodigoRiesgo = value; }
        }
        public string strNombreRiesgo
        {
            get { return _NombreRiesgo; }
            set { _NombreRiesgo = value; }
        }
        public string strDescripcionRiesgo
        {
            get { return _DescripcionRiesgo; }
            set { _DescripcionRiesgo = value; }
        }
        public int intIdRiesgo
        {
            get { return _IdRiesgo; }
            set { _IdRiesgo = value; }
        }
        public string strCodigoControl
        {
            get { return _CodigoControl; }
            set { _CodigoControl = value; }
        }
        public string strNombreControl
        {
            get { return _NombreControl; }
            set { _NombreControl = value; }
        }

        public clsVerCaracterizacionRiesgos()
        {
        }

        public clsVerCaracterizacionRiesgos(string strCodigoRiesgo, string strNombreRiesgo, string strDescripcionRiesgo, 
            int intIdRiesgo, string strCodigoControl, string strNombreControl)
        {
            this.strCodigoRiesgo = strCodigoRiesgo;
            this.strNombreRiesgo = strNombreRiesgo;
            this.strDescripcionRiesgo = strDescripcionRiesgo;
            this.intIdRiesgo = intIdRiesgo;
            this.strCodigoControl = strCodigoControl;
            this.strNombreControl = strNombreControl;
        }
        public clsVerCaracterizacionRiesgos(string strCodigoRiesgo, string strNombreRiesgo, string strDescripcionRiesgo, int intIdRiesgo)
        {
            this.strCodigoRiesgo = strCodigoRiesgo;
            this.strNombreRiesgo = strNombreRiesgo;
            this.strDescripcionRiesgo = strDescripcionRiesgo;
            this.intIdRiesgo = intIdRiesgo;
        }

    }
}
