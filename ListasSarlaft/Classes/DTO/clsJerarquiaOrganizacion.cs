using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsJerarquiaOrganizacion
    {
        private int _IdHijo;
        private string _NombreHijo;
        private int _IdPadre;
        private int _TipoArea;
        private string _FechaRegistro;
        private int _IdUsuario;

        public int intIdHijo
        {
            get { return _IdHijo; }
            set { _IdHijo = value; }
        }

        public string strNombreHijo
        {
            get { return _NombreHijo; }
            set { _NombreHijo = value; }
        }

        public int intIdPadre
        {
            get { return _IdPadre; }
            set { _IdPadre = value; }
        }

        public int intTipoArea
        {
            get { return _TipoArea; }
            set { _TipoArea = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region
        public clsJerarquiaOrganizacion()
        { }

        public clsJerarquiaOrganizacion(int intIdHijo, string strNombreHijo, int intIdPadre, int intTipoArea,
            int intIdUsuario, string dtFechaRegistro)
        {
            this.intIdHijo = intIdHijo;
            this.strNombreHijo = strNombreHijo;
            this.intIdPadre = intIdPadre;
            this.intTipoArea = intTipoArea;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }
        #endregion
    }
}