using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsParametros
    {
        private int _Id;
        private string _DescripcionParametro;
        private int _IdCriterioProveedor;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _NombreUsuario;

        public int intId
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public string strDescripcionParametro
        {
            get
            {
                return _DescripcionParametro;
            }

            set
            {
                _DescripcionParametro = value;
            }
        }

        public int intIdCriterioProveedor
        {
            get
            {
                return _IdCriterioProveedor;
            }

            set
            {
                _IdCriterioProveedor = value;
            }
        }

        public string dtFechaRegistro
        {
            get
            {
                return _FechaRegistro;
            }

            set
            {
                _FechaRegistro = value;
            }
        }

        public int intIdUsuario
        {
            get
            {
                return _IdUsuario;
            }

            set
            {
                _IdUsuario = value;
            }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        #region Constructors
        public clsParametros()
        {

        }

        public clsParametros(int intId, string strDescripcionParametro, int intIdCriterioProveedor, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcionParametro = strDescripcionParametro;
            this.intIdCriterioProveedor = intIdCriterioProveedor;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsParametros(int intId, string strDescripcionParametro, int intIdCriterioProveedor, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strDescripcionParametro = strDescripcionParametro;
            this.intIdCriterioProveedor = intIdCriterioProveedor;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }
        #endregion
    }
}