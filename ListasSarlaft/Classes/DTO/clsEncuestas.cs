using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsEncuestas
    {
        private int _IdEncuesta;
        private string _NombreEncuesta;
        private int _CantPreguntas;
        private string _DescripcionEmpresa;
        private int _IdUsuario;
        private DateTime _FechaRegistro;
        private string _Usuario;

        public int intIdEncuesta
        {
            get { return _IdEncuesta; }
            set { _IdEncuesta = value; }
        }
        public string strNombreEncuesta
        {
            get { return _NombreEncuesta; }
            set { _NombreEncuesta = value; }
        }
        public string strDescripcionEmpresa
        {
            get { return _DescripcionEmpresa; }
            set { _DescripcionEmpresa = value; }
        }
        public int intCantPreguntas
        {
            get { return _CantPreguntas; }
            set { _CantPreguntas = value; }
        }

        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
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
        public clsEncuestas()
        {
        }
    }
    public class clsPreguntasEncuestas
    {
        private int _IdPregunta;
        private int _IdEncuesta;
        private string _TextoPregunta;
        private int _Consecutivo;

        public int intIdPregunta
        {
            get { return _IdPregunta; }
            set { _IdPregunta = value; }
        }
        public int intIdEncuesta
        {
            get { return _IdEncuesta; }
            set { _IdEncuesta = value; }
        }
        public string strTextoPregunta
        {
            get { return _TextoPregunta; }
            set { _TextoPregunta = value; }
        }
        public int intConsecutivo
        {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }
        public clsPreguntasEncuestas() { }
    }
}