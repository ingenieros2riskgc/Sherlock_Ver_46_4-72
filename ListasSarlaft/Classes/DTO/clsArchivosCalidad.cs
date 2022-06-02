using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsArchivosCalidad
    {
        private int _Id;
        private int _IdControl;
        private int _IdTipoControl;
        private string _NombreArchivo;
        private byte[] _ArchivoBinario;
        private bool _Estado;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdControl
        {
            get { return _IdControl; }
            set { _IdControl = value; }
        }

        public int intIdTipoControl
        {
            get { return _IdTipoControl; }
            set { _IdTipoControl = value; }
        }

        public string strNombreArchivo
        {
            get { return _NombreArchivo; }
            set { _NombreArchivo = value; }
        }

        public byte[] bArchivoBinario
        {
            get { return _ArchivoBinario; }
            set { _ArchivoBinario = value; }
        }

        public bool booEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
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
        public clsArchivosCalidad()
        {
        }

        public clsArchivosCalidad(int intId, int intIdControl, int intIdTipoControl,
            string strNombreArchivo, byte[] bArchivoBinario, bool booEstado, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId=intId;
            this.intIdControl = intIdControl;
            this.intIdTipoControl = intIdTipoControl;
            this.strNombreArchivo = strNombreArchivo;
            this.bArchivoBinario = bArchivoBinario;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.booEstado = booEstado;
        }

        public clsArchivosCalidad(int intId, int intIdControl, int intIdTipoControl,
           string strNombreArchivo, byte[] bArchivoBinario, bool booEstado, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdControl = intIdControl;
            this.intIdTipoControl = intIdTipoControl;
            this.strNombreArchivo = strNombreArchivo;
            this.bArchivoBinario = bArchivoBinario;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
            this.strNombreUsuario = strNombreUsuario;
            this.booEstado = booEstado;
        }
        #endregion
    }
}