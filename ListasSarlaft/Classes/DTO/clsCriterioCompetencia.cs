using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCriterioCompetencia
    {
        private int _Id;
        private int _IdCompetencia;
        private string _NombreCompetencia;
        private string _Descripcion;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdCompetencia
        {
            get { return _IdCompetencia; }
            set { _IdCompetencia = value; }
        }

        public string strNombreCompetencia
        {
            get { return _NombreCompetencia; }
            set { _NombreCompetencia = value; }
        }

        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsCriterioCompetencia()
        {
        }

        public clsCriterioCompetencia(int intId, int intIdCompetencia, string strDescripcion, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdCompetencia = intIdCompetencia;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }

        public clsCriterioCompetencia(int intId, int intIdCompetencia, string strNombreCompetencia, string strDescripcion, 
            int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdCompetencia = intIdCompetencia;
            this.strNombreCompetencia = strNombreCompetencia;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }
        #endregion
    }
}