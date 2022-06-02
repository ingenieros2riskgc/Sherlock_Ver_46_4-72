using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cAtributos
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();

        public DataTable cargarAtributos()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdAtributos, LTRIM(RTRIM(NombreAtributo)) AS NombreAtributo FROM Parametrizacion.Atributos ORDER BY NombreAtributo");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable cargarValoresRangosAtributos(String IdRangosAtributo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT	ValorInferior, ValorSuperior FROM Parametrizacion.RangosAtributo WHERE (IdRangosAtributo = " + IdRangosAtributo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable cargarRangosAtributo(String IdAtributos)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT	IdRangosAtributo, NombreRango FROM Parametrizacion.RangosAtributo WHERE (IdAtributos = " + IdAtributos +")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadDDLUnidades()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdUnidad, NombreUnidad FROM Parametrizacion.Unidades ORDER BY NombreUnidad");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public DataTable consultarAtributos()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.Atributos.IdAtributos, Parametrizacion.Atributos.NombreAtributo, Parametrizacion.Atributos.Descripcion, Parametrizacion.Unidades.NombreUnidad FROM Parametrizacion.Atributos INNER JOIN Parametrizacion.Unidades ON Parametrizacion.Atributos.IdUnidad = Parametrizacion.Unidades.IdUnidad");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public void eliminarAtributo(String IdAtributos)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.Atributos WHERE (IdAtributos = " + IdAtributos + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateAtributos(String NombreAtributo, String Descripcion, String IdUnidad, String IdAtributos)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Atributos SET NombreAtributo = '" + NombreAtributo + "', Descripcion = N'" + Descripcion + "', IdUnidad = " + IdUnidad + " WHERE (IdAtributos = " + IdAtributos + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void registrarAtributo(String NombreAtributo, String Descripcion, String IdUnidad)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.Atributos (NombreAtributo, Descripcion, IdUnidad) VALUES ('" + NombreAtributo + "', N'" + Descripcion + "', " + IdUnidad + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public DataTable consultarRangosAtributos(String IdAtributos)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.RangosAtributo.IdRangosAtributo, Parametrizacion.Atributos.NombreAtributo, Parametrizacion.RangosAtributo.NombreRango, Parametrizacion.RangosAtributo.ValorInferior, Parametrizacion.RangosAtributo.ValorSuperior FROM Parametrizacion.RangosAtributo INNER JOIN Parametrizacion.Atributos ON Parametrizacion.RangosAtributo.IdAtributos = Parametrizacion.Atributos.IdAtributos WHERE (Parametrizacion.Atributos.IdAtributos = " + IdAtributos + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public void eliminarRangosAtributos(String IdRangosAtributo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.RangosAtributo WHERE (IdRangosAtributo = " + IdRangosAtributo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateRangoAtributos(String NombreRango, String ValorInferior, String ValorSuperior, String IdRangosAtributo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.RangosAtributo SET NombreRango = '" + NombreRango + "', ValorInferior = " + ValorInferior + ", ValorSuperior = " + ValorSuperior + " WHERE (IdRangosAtributo = " + IdRangosAtributo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void registrarRangoAtributos(String IdAtributos, String NombreRango, String ValorInferior, String ValorSuperior)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.RangosAtributo (IdAtributos, NombreRango, ValorInferior, ValorSuperior) VALUES (" + IdAtributos + ", '" + NombreRango + "', " + ValorInferior + ", " + ValorSuperior + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
 
        }
    }
}