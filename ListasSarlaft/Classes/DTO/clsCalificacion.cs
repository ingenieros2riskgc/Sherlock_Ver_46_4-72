using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsCalificacion
    {
        private int _Id;
        private string _NombreCumplimiento;
        private string _FechaRegistro;
        private int _IdUsuario;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombreCumplimiento
        {
            get { return _NombreCumplimiento; }
            set { _NombreCumplimiento = value; }
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

        #region Constructors
        public clsCalificacion()
        {
        }

        public clsCalificacion(int intId, string strNombreCumplimiento, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreCumplimiento = strNombreCumplimiento;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}