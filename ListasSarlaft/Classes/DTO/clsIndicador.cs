using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsIndicador
    {
        private int _Id;
        private string _NombreIndicador;
        private string _Descripcion;
        private int _Idperiodicidad;
        private string _NombrePeriodicidad;
        private decimal _Meta;
        private bool _Estado;
        private int _IdObjetivoCalidad;
        private string _DescObjetivo;
        private int _IdProcesoIndicador;
        private string _Proceso;
        private int _IdUsuario;
        private string _NombreUsuario;
        private string _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string strNombreIndicador
        {
            get { return _NombreIndicador; }
            set { _NombreIndicador = value; }
        }

        public int intIdPeriodicidad
        {
            get { return _Idperiodicidad; }
            set { _Idperiodicidad = value; }
        }

        public string strNombrePeriodicidad
        {
            get { return _NombrePeriodicidad; }
            set { _NombrePeriodicidad = value; }
        }

        public decimal intMeta
        {
            get { return _Meta; }
            set { _Meta = value; }
        }

        public bool booEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public int intIdObjetivoCalidad
        {
            get { return _IdObjetivoCalidad; }
            set { _IdObjetivoCalidad = value; }
        }

        public string strDescObjetivo
        {
            get { return _DescObjetivo; }
            set { _DescObjetivo = value; }
        }

        public int intIdProcesoIndicador
        {
            get { return _IdProcesoIndicador; }
            set { _IdProcesoIndicador = value; }
        }

        public string strProceso
        {
            get { return _Proceso; }
            set { _Proceso = value; }
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

        public string strNombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        #region Constructors
        public clsIndicador()
        {
        }

        public clsIndicador(int intId, string strNombreIndicador,string strDescripcion, int intIdPeriodicidad,
           decimal intMeta, bool booEstado, int intIdObjetivoCalidad,
           int intIdProcesoIndicador, int intIdUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreIndicador = strNombreIndicador;
            this.strDescripcion = strDescripcion;
            this.intIdPeriodicidad = intIdPeriodicidad;
            this.intIdObjetivoCalidad = intIdObjetivoCalidad;
            this.strDescObjetivo = strDescObjetivo;
            this.intMeta = intMeta;
            this.booEstado = booEstado;
            this.intIdProcesoIndicador = intIdProcesoIndicador;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }

        public clsIndicador(int intId, string strNombreIndicador,string strDescripcion, int intIdPeriodicidad,
            decimal intMeta, bool booEstado, int intIdObjetivoCalidad, string strDescObjetivo,
            int intIdProcesoIndicador, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro, string strProceso)
        {
            this.intId = intId;
            this.strNombreIndicador = strNombreIndicador;
            this.strDescripcion = strDescripcion;
            this.intIdPeriodicidad = intIdPeriodicidad;
            this.intIdObjetivoCalidad = intIdObjetivoCalidad;
            this.strDescObjetivo = strDescObjetivo;
            this.intMeta = intMeta;
            this.booEstado = booEstado;
            this.intIdProcesoIndicador = intIdProcesoIndicador;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
            this.strProceso = strProceso;
        }

        public clsIndicador(int intId, string strNombreIndicador ,string strDescripcion, int intIdPeriodicidad, string strNombrePeriodicidad,
            decimal intMeta, bool booEstado, int intIdObjetivoCalidad, string strDescObjetivo,
            int intIdProcesoIndicador, int intIdUsuario, string strNombreUsuario, string dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombreIndicador = strNombreIndicador;
            this.strDescripcion = strDescripcion;
            this.intIdPeriodicidad = intIdPeriodicidad;
            this.strNombrePeriodicidad = strNombrePeriodicidad;
            this.intIdObjetivoCalidad = intIdObjetivoCalidad;
            this.strDescObjetivo = strDescObjetivo;
            this.intMeta = intMeta;
            this.booEstado = booEstado;
            this.intIdProcesoIndicador = intIdProcesoIndicador;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            this.strNombreUsuario = strNombreUsuario;
        }
        #endregion

    }
}