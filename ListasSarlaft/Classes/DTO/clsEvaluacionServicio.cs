using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsEvaluacionServicio
    {
        private int _Id;
        private string _Ciudad;
        private string _Fecha;
        private int _IdTipoEncuesta;
        private string _TipoEncuesta;
        private string _NombreCliente;
        private string _Nombre;
        private string _Cargo;
        private string _Funcionarios;
        private int _Calificacion1;
        private int _Calificacion2;
        private int _Calificacion3;
        private int _Calificacion4;
        private int _Calificacion5;
        private int _Calificacion6;
        private int _IdUsuario;
        private string _Usuario;
        private DateTime _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strCiudad
        {
            get { return _Ciudad; }
            set { _Ciudad = value; }
        }

        public string dtFecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        public int intIdTipoEncuesta
        {
            get { return _IdTipoEncuesta; }
            set { _IdTipoEncuesta = value; }
        }
        public string strTipoEncuesta
        {
            get { return _TipoEncuesta; }
            set { _TipoEncuesta = value; }
        }

        public string strNombreCliente
        {
            get { return _NombreCliente; }
            set { _NombreCliente = value; }
        }

        public string strNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string strCargo
        {
            get { return _Cargo; }
            set { _Cargo = value; }
        }

        public string strFuncionarios
        {
            get { return _Funcionarios; }
            set { _Funcionarios = value; }
        }

        public int intCalificacion1
        {
            get { return _Calificacion1; }
            set { _Calificacion1 = value; }
        }

        public int intCalificacion2
        {
            get { return _Calificacion2; }
            set { _Calificacion2 = value; }
        }

        public int intCalificacion3
        {
            get { return _Calificacion3; }
            set { _Calificacion3 = value; }
        }

        public int intCalificacion4
        {
            get { return _Calificacion4; }
            set { _Calificacion4 = value; }
        }

        public int intCalificacion5
        {
            get { return _Calificacion5; }
            set { _Calificacion5 = value; }
        }

        public int intCalificacion6
        {
            get { return _Calificacion6; }
            set { _Calificacion6 = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string strUsuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsEvaluacionServicio()
        {
        }

        public clsEvaluacionServicio(int intId, string strCiudad, string dtFecha,
            int intIdTipoEncuesta, string strNombreCliente, string strNombre, string strCargo,
            string strFuncionarios, int intCalificacion1, int intCalificacion2, int intCalificacion3,
            int intCalificacion4, int intCalificacion5, int intCalificacion6, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.strCiudad = strCiudad;
            this.intIdTipoEncuesta = intIdTipoEncuesta;
            this.strNombreCliente = strNombreCliente;
            this.strNombre = strNombre;
            this.strCargo = strCargo;
            this.strFuncionarios = strFuncionarios;
            this.intCalificacion1 = intCalificacion1;
            this.intCalificacion2 = intCalificacion2;
            this.intCalificacion3 = intCalificacion3;
            this.intCalificacion4 = intCalificacion4;
            this.intCalificacion5 = intCalificacion5;
            this.intCalificacion6 = intCalificacion6;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}