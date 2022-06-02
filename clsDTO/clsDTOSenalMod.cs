using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOSenalMod
    {
       
        private string strIdSenal;
        private string strCodigoSenal;
        private string strDescripcionSenal;
        private string strUsuario;
        private string strIdUsuario;
        private string strFechaModificacion;

        public string StrIdSenal
        {
            get { return strIdSenal; }
            set { strIdSenal = value; }
        }

        public string StrCodigoSenal
        {
            get { return strCodigoSenal; }
            set { strCodigoSenal = value; }
        }

        public string StrDescripcionSenal
        {
            get { return strDescripcionSenal; }
            set { strDescripcionSenal = value; }
        }

        
        public string StrUsuario
        {
            get { return strUsuario; }
            set { strUsuario = value; }
        }
        public string StrIdUsuario
        {
            get { return strIdUsuario; }
            set { strIdUsuario = value; }
        }

        public string StrFechaModificacion
        {
            get { return strFechaModificacion; }
            set { strFechaModificacion = value; }
        }


        public clsDTOSenalMod()
        {
        }

        public clsDTOSenalMod(string strIdSenal, string strCodigoSenal, string strDescripcionSenal, string strUsuario,
            string strIdUsuario, string strFechaModificacion)
        {
            this.StrIdSenal = strIdSenal;
            this.StrCodigoSenal = strCodigoSenal;
            this.StrDescripcionSenal = strDescripcionSenal;
            this.StrUsuario = strUsuario;
            this.StrIdUsuario = strIdUsuario;
            this.StrFechaModificacion = strFechaModificacion;
        }
    }

    //public class clsDTOSenalVariable
    //{
    //    #region Properties
    //    private string strIdSenal;
    //    private string strIdSenalVariable;
    //    private string strIdOperando;
    //    private string strValor;
    //    private string strPos;
    //    private bool booEsGlobal;

    //    public string StrIdSenal
    //    {
    //        get { return strIdSenal; }
    //        set { strIdSenal = value; }
    //    }

    //    public string StrIdSenalVariable
    //    {
    //        get { return strIdSenalVariable; }
    //        set { strIdSenalVariable = value; }
    //    }

    //    public string StrIdOperando
    //    {
    //        get { return strIdOperando; }
    //        set { strIdOperando = value; }
    //    }

    //    public string StrValor
    //    {
    //        get { return strValor; }
    //        set { strValor = value; }
    //    }

    //    public string StrPos
    //    {
    //        get { return strPos; }
    //        set { strPos = value; }
    //    }

    //    public bool BooEsGlobal
    //    {
    //        get { return booEsGlobal; }
    //        set { booEsGlobal = value; }
    //    }
    //    #endregion

    //    public clsDTOSenalVariable()
    //    {
    //    }

    //    public clsDTOSenalVariable(string strIdSenal, string strIdSenalVariable, string strIdOperando, 
    //        string strValor, string strPos, bool booEsGlobal)
    //    {
    //        this.StrIdSenal = strIdSenal;
    //        this.StrIdSenalVariable = strIdSenalVariable;
    //        this.StrIdOperando = strIdOperando;
    //        this.StrValor = strValor;
    //        this.StrPos = strPos;
    //        this.BooEsGlobal = booEsGlobal;
    //    }

    //    public string mtdGetPosicion()
    //    {
    //        return this.StrPos;
    //    }

    //    public string mtdGetIdSenal()
    //    {
    //        return this.StrIdSenal;
    //    }
    //}

    //public class clsDTOOperador
    //{
    //    #region Properties
    //    private string strIdOperador;
    //    private string strNombreOperador;
    //    private string strIdentificadorOperador;


    //    public string StrIdOperador
    //    {
    //        get { return strIdOperador; }
    //        set { strIdOperador = value; }
    //    }

    //    public string StrNombreOperador
    //    {
    //        get { return strNombreOperador; }
    //        set { strNombreOperador = value; }
    //    }

    //    public string StrIdentificadorOperador
    //    {
    //        get { return strIdentificadorOperador; }
    //        set { strIdentificadorOperador = value; }
    //    }
    //    #endregion

    //    public clsDTOOperador()
    //    {
    //    }

    //    public clsDTOOperador(string strIdOperador, string strNombreOperador, string strIdentificadorOperador)
    //    {
    //        this.StrIdOperador = strIdOperador;
    //        this.StrNombreOperador = strNombreOperador;
    //        this.StrIdentificadorOperador = strIdentificadorOperador;
    //    }
    //}

    //public class clsDTOOperadorGlobal
    //{
    //    #region Properties
    //    private string strIdOperador;
    //    private string strNombreOperador;
    //    private string strIdentificadorOperador;

    //    public string StrIdOperador
    //    {
    //        get { return strIdOperador; }
    //        set { strIdOperador = value; }
    //    }

    //    public string StrNombreOperador
    //    {
    //        get { return strNombreOperador; }
    //        set { strNombreOperador = value; }
    //    }

    //    public string StrIdentificadorOperador
    //    {
    //        get { return strIdentificadorOperador; }
    //        set { strIdentificadorOperador = value; }
    //    }
    //    #endregion

    //    public clsDTOOperadorGlobal()
    //    {
    //    }

    //    public clsDTOOperadorGlobal(string strIdOperador, string strNombreOperador, string strIdentificadorOperador)
    //    {
    //        this.StrIdOperador = strIdOperador;
    //        this.StrNombreOperador = strNombreOperador;
    //        this.StrIdentificadorOperador = strIdentificadorOperador;
    //    }
    //}

}
