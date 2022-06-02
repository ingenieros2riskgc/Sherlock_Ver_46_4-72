using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsAsistentesCapacitacion
    {
        private int _Id;
        private int _IdProgramacioncapacitacion;
        private string _NombreAsistente;
        private int _IdUsuario;
        private DateTime _FechaResponsable;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdProgramacionCapacitacion
        {
            get { return _IdProgramacioncapacitacion; }
            set { _IdProgramacioncapacitacion = value; }
        }

        public string strNombreAsistente
        {
            get { return _NombreAsistente; }
            set { _NombreAsistente = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public DateTime dtFechaResponsable
        {
            get { return _FechaResponsable; }
            set { _FechaResponsable = value; }
        }

        #region Constructors
        public clsAsistentesCapacitacion()
        {
        }

        public clsAsistentesCapacitacion(int intId, int intIdProgramacionCapacitacion, string strNombreAsistente, 
            int intIdUsuario, DateTime dtFechaResponsable)
        {
            this.intId = intId;
            this.intIdProgramacionCapacitacion = intIdProgramacionCapacitacion;
            this.strNombreAsistente = strNombreAsistente;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaResponsable = dtFechaResponsable;
        }
        #endregion
    }
}