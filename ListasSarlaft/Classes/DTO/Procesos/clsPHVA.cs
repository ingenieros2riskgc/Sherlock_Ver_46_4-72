using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsPHVA
    {
        private int _Idphva;
        private string _Codigo;
        private string _Descripcion;
        private string _FechaRegistro;
        private int _IdUsuario;

        public int intIdphva
        {
            get { return _Idphva; }
            set { _Idphva = value; }
        }
        public string strCodigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        #region Construtors
        public clsPHVA()
        {
        }

        public clsPHVA(int intIdphva, string strDescripcion,string codigo , string dtFechaRegistro,int intIdUsuario )
        {
            this.intIdphva = intIdphva;
            this.strCodigo = strCodigo;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}