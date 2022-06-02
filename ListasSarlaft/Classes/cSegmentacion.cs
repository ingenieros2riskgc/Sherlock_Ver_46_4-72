using System;
using System.Collections.Generic;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cSegmentacion
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();

        public DataTable loadCodigoIndicador()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdIndicador+1 AS NumRegistros FROM Parametrizacion.Indicador ORDER BY IdIndicador DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadCodigoPerfilSegmento()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdPerfil+1 AS NumRegistros FROM Parametrizacion.PerfilSegmento ORDER BY IdPerfil DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadCodigoAtributo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdAtributo+1 AS NumRegistros FROM Parametrizacion.AtributoSegmentacion ORDER BY IdAtributo DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadCodigoTipoSegmento()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdTipoSegmento+1 AS NumRegistros FROM Parametrizacion.TipoSegmento ORDER BY IdTipoSegmento DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadCodigoSegmento()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdSegmento+1 AS NumRegistros FROM Parametrizacion.Segmento ORDER BY IdSegmento DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadCodigoFactorRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdFactorRiesgo+1 AS NumRegistros FROM Parametrizacion.FactorRiesgo ORDER BY IdFactorRiesgo DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadInfoFactorRiesgo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select IdFactorRiesgo, CodigoFactorRiesgo as Codigo, DescFactorRiesgo as Nombre from [Perfiles].[tblFactorRiesgo]");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadInfoSegmento(String IdFactorRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdSegmento, Codigo, Nombre, Descripcion FROM Parametrizacion.Segmento WHERE (IdFactorRiesgo = " + IdFactorRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadInfoTipoSegmento(String IdSegmento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdTipoSegmento, Codigo, Nombre, Descripcion FROM Parametrizacion.TipoSegmento WHERE (IdSegmento = " + IdSegmento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadInfoAtributo(String IdTipoSegmento)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdAtributo, Codigo, Nombre, Descripcion FROM Parametrizacion.AtributoSegmentacion WHERE (IdTipoSegmento = " + IdTipoSegmento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadInfoPerfilSegmento(String IdAtributo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdPerfil, Codigo, Nombre, Descripcion FROM Parametrizacion.PerfilSegmento WHERE (IdAtributo = " + IdAtributo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadInfoIndicador(String IdPerfil)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdIndicador, Codigo, Nombre, Indicador, MensajeSenalAlerta, NombreAtributo1, NombreRango1, NombreAtributo2, NombreRango2 FROM Parametrizacion.Indicador WHERE (IdPerfil = " + IdPerfil + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadInfoSenalAlerta(String IdFactorRiesgo)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select [Perfiles].[tblFactorSenal].[IdSenal] as IdIndicador, [Perfiles].[tblSenalAlerta].[DescripcionSenal] as SenalAlerta from [Perfiles].[tblFactorSenal] inner join [Perfiles].[tblSenalAlerta] on [Perfiles].[tblFactorSenal].[IdSenal] = [Perfiles].[tblSenalAlerta].[IdSenal] where [Perfiles].[tblFactorSenal].IdFactorRiesgo =" + IdFactorRiesgo);
                //dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.FactorRiesgo.Nombre AS FactorRiesgo, Parametrizacion.Segmento.Nombre AS Segmento, Parametrizacion.TipoSegmento.Nombre AS TipoSegmento, Parametrizacion.AtributoSegmentacion.Nombre AS Atributo, Parametrizacion.PerfilSegmento.Nombre AS PerfilSegmento, Parametrizacion.Indicador.Nombre AS SenalAlerta, Parametrizacion.Indicador.Indicador, Parametrizacion.Indicador.MensajeSenalAlerta, Parametrizacion.Indicador.NombreAtributo1, Parametrizacion.Indicador.NombreRango1, Parametrizacion.Indicador.NombreAtributo2, Parametrizacion.Indicador.NombreRango2, Parametrizacion.Indicador.IdIndicador FROM Parametrizacion.FactorRiesgo INNER JOIN Parametrizacion.Segmento ON Parametrizacion.FactorRiesgo.IdFactorRiesgo = Parametrizacion.Segmento.IdFactorRiesgo INNER JOIN Parametrizacion.TipoSegmento ON Parametrizacion.Segmento.IdSegmento = Parametrizacion.TipoSegmento.IdSegmento INNER JOIN Parametrizacion.AtributoSegmentacion ON Parametrizacion.TipoSegmento.IdTipoSegmento = Parametrizacion.AtributoSegmentacion.IdTipoSegmento INNER JOIN Parametrizacion.PerfilSegmento ON Parametrizacion.AtributoSegmentacion.IdAtributo = Parametrizacion.PerfilSegmento.IdAtributo INNER JOIN Parametrizacion.Indicador ON Parametrizacion.PerfilSegmento.IdPerfil = Parametrizacion.Indicador.IdPerfil WHERE (Parametrizacion.FactorRiesgo.IdFactorRiesgo = " + IdFactorRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public void addFactorRiesgo(String Codigo, String Nombre, String Indicador, String Descripcion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.FactorRiesgo (Codigo, Nombre, Indicador, Descripcion) VALUES (N'" + Codigo + "', N'" + Nombre + "', N'" + Indicador + "', N'" + Descripcion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void addSegmento(String IdFactorRiesgo, String Codigo, String Nombre, String Descripcion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.Segmento (IdFactorRiesgo, Codigo, Nombre, Descripcion) VALUES (" + IdFactorRiesgo + ", N'" + Codigo + "', N'" + Nombre + "', N'" + Descripcion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void addTipoSegmento(String IdSegmento, String Codigo, String Nombre, String Descripcion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.TipoSegmento (IdSegmento, Codigo, Nombre, Descripcion) VALUES (" + IdSegmento + ", N'" + Codigo + "', N'" + Nombre + "', N'" + Descripcion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void addAtributo(String IdTipoSegmento, String Codigo, String Nombre, String Descripcion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.AtributoSegmentacion (IdTipoSegmento, Codigo, Nombre, Descripcion) VALUES (" + IdTipoSegmento + ", N'" + Codigo + "', N'" + Nombre + "', N'" + Descripcion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void addPerfilSegmento(String IdAtributo, String Codigo, String Nombre, String Descripcion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.PerfilSegmento (IdAtributo, Codigo, Nombre, Descripcion) VALUES (" + IdAtributo + ", N'" + Codigo + "', N'" + Nombre + "', N'" + Descripcion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void addIndicador(String IdPerfil, String Codigo, String Nombre, String Indicador, String MensajeSenalAlerta, String NombreAtributo1, String NombreRango1, String NombreAtributo2, String NombreRango2, String ValorInferior1, String ValorSuperior1, String ValorInferior2, String ValorSuperior2)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Parametrizacion.Indicador (IdPerfil, Codigo, Nombre, Indicador, MensajeSenalAlerta, NombreAtributo1, NombreRango1, NombreAtributo2, NombreRango2, ValorInferior1, ValorSuperior1, ValorInferior2, ValorSuperior2) VALUES (" + IdPerfil + ", N'" + Codigo + "', N'" + Nombre + "', N'" + Indicador + "', N'" + MensajeSenalAlerta + "', '" + NombreAtributo1 + "', '" + NombreRango1 + "', '" + NombreAtributo2 + "', '" + NombreRango2 + "', '" + ValorInferior1 + "', '" + ValorSuperior1 + "', '" + ValorInferior2 + "', '" + ValorSuperior2 + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarFactorRiesgo(String IdFactorRiesgo)
        {
            string strError = string.Empty;

            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.FactorRiesgo WHERE (IdFactorRiesgo = " + IdFactorRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (OleDbException odbcEx)
            {
                cDataBase.desconectar();
                strError = mtdOdbcError(odbcEx);
                cError.errorMessage(strError + ", " + odbcEx.StackTrace);
                throw new Exception(strError);
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarSegmento(String IdSegmento)
        {
            string strError = string.Empty;

            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.Segmento WHERE (IdSegmento = " + IdSegmento + ")");
                cDataBase.desconectar();
            }
            catch (OleDbException odbcEx)
            {
                cDataBase.desconectar();
                strError = mtdOdbcError(odbcEx);
                cError.errorMessage(strError + ", " + odbcEx.StackTrace);
                throw new Exception(strError);
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarTipoSegmento(String IdTipoSegmento)
        {
            string strError = string.Empty;

            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.TipoSegmento WHERE (IdTipoSegmento = " + IdTipoSegmento + ")");
                cDataBase.desconectar();
            }
            catch (OleDbException odbcEx)
            {
                cDataBase.desconectar();
                strError = mtdOdbcError(odbcEx);
                cError.errorMessage(strError + ", " + odbcEx.StackTrace);
                throw new Exception(strError);
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarAtributo(String IdAtributo)
        {
            string strError = string.Empty;

            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.AtributoSegmentacion WHERE (IdAtributo = " + IdAtributo + ")");
                cDataBase.desconectar();
            }
            catch (OleDbException odbcEx)
            {
                cDataBase.desconectar();
                strError = mtdOdbcError(odbcEx);
                cError.errorMessage(strError + ", " + odbcEx.StackTrace);
                throw new Exception(strError);
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarPerfilSegmento(String IdPerfil)
        {
            string strError = string.Empty;

            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.PerfilSegmento WHERE (IdPerfil = " + IdPerfil + ")");
                cDataBase.desconectar();
            }
            catch (OleDbException odbcEx)
            {
                cDataBase.desconectar();
                strError = mtdOdbcError(odbcEx);
                cError.errorMessage(strError + ", " + odbcEx.StackTrace);
                throw new Exception(strError);
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void borrarIndicador(String IdIndicador)
        {
            string strError = string.Empty;

            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM Parametrizacion.Indicador WHERE (IdIndicador = " + IdIndicador + ")");
                cDataBase.desconectar();
            }
            catch (OleDbException odbcEx)
            {
                cDataBase.desconectar();
                strError = mtdOdbcError(odbcEx);
                cError.errorMessage(strError + ", " + odbcEx.StackTrace);
                throw new Exception(strError);
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateFactorRiesgo(String Codigo, String Nombre, String Indicador, String Descripcion, String IdFactorRiesgo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.FactorRiesgo SET Codigo = N'" + Codigo + "', Nombre = N'" + Nombre + "', Indicador = N'" + Indicador + "', Descripcion = N'" + Descripcion + "' WHERE (IdFactorRiesgo = " + IdFactorRiesgo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateSegmento(String Codigo, String Nombre, String Descripcion, String IdSegmento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Segmento SET Codigo = N'" + Codigo + "', Nombre = N'" + Nombre + "', Descripcion = N'" + Descripcion + "' WHERE (IdSegmento = " + IdSegmento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateTipoSegmento(String Codigo, String Nombre, String Descripcion, String IdTipoSegmento)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.TipoSegmento SET Codigo = N'" + Codigo + "', Nombre = N'" + Nombre + "', Descripcion = N'" + Descripcion + "' WHERE (IdTipoSegmento = " + IdTipoSegmento + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateAtributo(String Codigo, String Nombre, String Descripcion, String IdAtributo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.AtributoSegmentacion SET Codigo = N'" + Codigo + "', Nombre = N'" + Nombre + "', Descripcion = N'" + Descripcion + "' WHERE (IdAtributo = " + IdAtributo + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updatePerfilSegmento(String Codigo, String Nombre, String Descripcion, String IdPerfil)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.PerfilSegmento SET Codigo = N'" + Codigo + "', Nombre = N'" + Nombre + "', Descripcion = N'" + Descripcion + "' WHERE (IdPerfil = " + IdPerfil + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void updateIndicador(String Codigo, String Nombre, String Indicador, String MensajeSenalAlerta, String NombreAtributo1, String NombreRango1, String NombreAtributo2, String NombreRango2, String ValorInferior1, String ValorSuperior1, String ValorInferior2, String ValorSuperior2, String IdIndicador)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.Indicador SET Codigo = N'" + Codigo + "', Nombre = N'" + Nombre + "', Indicador = N'" + Indicador + "', MensajeSenalAlerta = N'" + MensajeSenalAlerta + "', NombreAtributo1 = '" + NombreAtributo1 + "', NombreRango1 = '" + NombreRango1 + "', NombreAtributo2 = '" + NombreAtributo2 + "', NombreRango2 = '" + NombreRango2 + "', ValorInferior1 = '" + ValorInferior1 + "', ValorSuperior1 = '" + ValorSuperior1 + "', ValorInferior2 = '" + ValorInferior2 + "', ValorSuperior2 = '" + ValorSuperior2 + "' WHERE (IdIndicador = " + IdIndicador + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        private string mtdOdbcError(OleDbException Ex)
        {
            string strError = string.Empty;

            switch (Ex.ErrorCode)
            {
                case -2147217873:
                    strError = "<br/> La información a borrar tiene relación con otro objeto. <br/> Por favor revise la información.";
                    break;
                default:
                    strError = "Descripción: " + Ex.Message.ToString().Trim();
                    break;
            }

            return strError;
        }
    }
}