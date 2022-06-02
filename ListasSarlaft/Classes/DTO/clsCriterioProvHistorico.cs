using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCriterioProvHistorico
    {
        private int _Id;
        private int _IdAspectoProveedorHistorico;
        private string _Descripcion;
        private int _IdUsuario;
        private DateTime _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdAspectoProveedorHistorico
        {
            get { return _IdAspectoProveedorHistorico; }
            set { _IdAspectoProveedorHistorico = value; }
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

        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsCriterioProvHistorico()
        {
        }

        public clsCriterioProvHistorico(int intId, int intIdAspectoProveedorHistorico, string strDescripcion, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdAspectoProveedorHistorico = intIdAspectoProveedorHistorico;
            this.strDescripcion = strDescripcion;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}