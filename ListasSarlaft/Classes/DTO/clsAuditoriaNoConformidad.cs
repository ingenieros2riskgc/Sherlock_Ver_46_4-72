using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAuditoriaNoConformidad
    {
        private int _IdAuditoria;
        private string _Tema;
        private string _Estandar;

        public int intIdAuditoria
        {
            get
            {
                return _IdAuditoria;
            }

            set
            {
                _IdAuditoria = value;
            }
        }

        public string strTema
        {
            get
            {
                return _Tema;
            }

            set
            {
                _Tema = value;
            }
        }
        public string strEstandar
        {
            get
            {
                return _Estandar;
            }

            set
            {
                _Estandar = value;
            }
        }

        #region
        public clsAuditoriaNoConformidad()
        {

        }

        public clsAuditoriaNoConformidad(int intIdAuditoria, string strTema, string strEstandar)
        {
            this.intIdAuditoria = intIdAuditoria;
            this.strTema = strTema;
            this.strEstandar = strEstandar;
        }
        #endregion
    }
}