using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOFactorRiesgoMod
    {
        #region Properties
        private string idFactorRiesgo;
        private string codigoFactorRiesgo;
        private string descFactorRiesgo;
        private string usuario;
        private string idUsuario;
        private string fechaModificacion;

        public string IdFactorRiesgo
        {
            get { return idFactorRiesgo; }
            set { idFactorRiesgo = value; }
        }

        public string CodigoFactorRiesgo
        {
            get { return codigoFactorRiesgo; }
            set { codigoFactorRiesgo = value; }
        }

        public string DescFactorRiesgo
        {
            get { return descFactorRiesgo; }
            set { descFactorRiesgo = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        public string FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }

        #endregion

        public clsDTOFactorRiesgoMod()
        {
        }

        public clsDTOFactorRiesgoMod(string idFactorRiesgo, string codigoFactorRiesgo, string descFactorRiesgo, string usuario,
            string idUsuario, string fechaModificacion)
        {
            this.IdFactorRiesgo = idFactorRiesgo;
            this.CodigoFactorRiesgo = codigoFactorRiesgo;
            this.DescFactorRiesgo = descFactorRiesgo;
            this.Usuario = usuario;
            this.IdUsuario = idUsuario;
            this.FechaModificacion = fechaModificacion;
        }
    }

    //public class clsDTOFactorSenal
    //{
    //    #region Properties
    //    private string strIdFactorSenal;
    //    private string strIdFactorRiesgo;
    //    private string strIdSenal;

    //    public string StrIdFactorRiesgo
    //    {
    //        get { return strIdFactorRiesgo; }
    //        set { strIdFactorRiesgo = value; }
    //    }

    //    public string StrIdFactorSenal
    //    {
    //        get { return strIdFactorSenal; }
    //        set { strIdFactorSenal = value; }
    //    }

    //    public string StrIdSenal
    //    {
    //        get { return strIdSenal; }
    //        set { strIdSenal = value; }
    //    }

    //    #endregion

    //    public clsDTOFactorSenal()
    //    {
    //    }

    //    public clsDTOFactorSenal(string strIdFactorSenal, string strIdFactorRiesgo, string StrIdSenal)
    //    {
    //        this.StrIdFactorRiesgo = strIdFactorRiesgo;
    //        this.StrIdFactorSenal = strIdFactorSenal;
    //        this.StrIdSenal = StrIdSenal;
    //    }

    //}

}
