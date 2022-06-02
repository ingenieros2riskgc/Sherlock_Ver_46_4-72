using System;
using System.Collections.Generic;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;

namespace ListasSarlaft.Classes
{
    public class cListas : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;

        public String conteoRegistrosLista()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT COUNT(*) FROM Listas.Lista");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();                
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0][0].ToString().Trim();
        }

        public void truncateTables()
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("TRUNCATE TABLE Listas.ClientesAnalisisPEPS");
                cDataBase.ejecutarQuery("TRUNCATE TABLE Listas.ClientesAnalisisPEPSEncontrados");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable listaListas()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT	IdClaseLista, LTRIM(RTRIM(CodigoClaseLisa)) AS CodigoClaseLisa, LTRIM(RTRIM(NombreClaseLista)) AS NombreClaseLista FROM	Parametrizacion.ClasesLista");
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

        public void modificarLista(String IdClaseLista, String NombreClaseLista)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Parametrizacion.ClasesLista SET NombreClaseLista = '" + NombreClaseLista.Trim() + "' WHERE (IdClaseLista = " + IdClaseLista.Trim() + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void truncateTableLista()
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("TRUNCATE TABLE Listas.Lista");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();                
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void loadClientes(DataTable dtInfo)
        {
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["SQLConnectionString"];
                using (SqlConnection connection = new SqlConnection(connString.ToString()))
                {
                    connection.Open();
                    using (SqlBulkCopy sqlBCopy = new SqlBulkCopy(connection))
                    {
                        try
                        {
                            sqlBCopy.DestinationTableName = "Listas.ClientesAnalisisPEPS";
                            sqlBCopy.WriteToServer(dtInfo);
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }

        public void updateInfoLista(DataTable dtInfo)
        {
           
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["SQLConnectionString"];
                using (SqlConnection connection = new SqlConnection(connString.ToString()))
                {
                    connection.Open();
                    using (SqlBulkCopy sqlBCopy = new SqlBulkCopy(connection))
                    {
                        try
                        {
                            sqlBCopy.BatchSize = 10000;
                            sqlBCopy.BulkCopyTimeout = 0;
                            sqlBCopy.DestinationTableName = "Listas.Lista";
                            sqlBCopy.WriteToServer(dtInfo);
                            connection.Close();
                        }
                        catch (Exception ex)
                        {                            
                            cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }            
        }

        public DataTable TipoLista()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT CodigoClaseLisa, LTRIM(RTRIM(NombreClaseLista)) AS NombreClaseLista FROM Parametrizacion.ClasesLista ORDER BY NombreClaseLista");
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

        public DataTable ConsultaListas(Int32 tipoLista, String documento, String nombre, String alias)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty;
            try
            {
                if (tipoLista != 0)
                {
                    condicion = "WHERE (Parametrizacion.ClasesLista.CodigoClaseLisa = " + tipoLista.ToString().Trim() + ") ";
                }

                if (documento != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.Lista.DocumentoIdentidad LIKE '%" + documento.ToString().Trim() + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.Lista.DocumentoIdentidad LIKE '%" + documento.ToString().Trim() + "%') ";
                    }

                }

                if (nombre != string.Empty)
                {
                    nombre = nombre.Trim();
                    string[] strSeparator = new string[] { " " };
                    string[] arrName = nombre.Split(strSeparator, StringSplitOptions.None);
                    int i = arrName.Length;
                    for (int j = 0; j < i; j++)
                    {
                        if (condicion.ToString().Trim() == "")
                        {
                            condicion = "WHERE (Listas.Lista.NombreCompleto LIKE '%" + arrName[j] + "%') ";
                        }
                        else
                        {
                            condicion = condicion + "AND (Listas.Lista.NombreCompleto LIKE '%" + arrName[j] + "%') ";
                        }
                    }
                }

                if (alias != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.Lista.Alias LIKE '%" + alias.ToString().Trim() + "%')";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.Lista.Alias LIKE '%" + alias.ToString().Trim() + "%')";
                    }
                }
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Listas.Lista.IdLista, Listas.Lista.TipoDocumento, Listas.Lista.DocumentoIdentidad, Listas.Lista.NombreCompleto, Parametrizacion.ClasesLista.CodigoClaseLisa, Parametrizacion.ClasesLista.NombreClaseLista, Listas.Lista.FuenteConsulta, Listas.Lista.TipoPersona, Listas.Lista.Alias, Listas.Lista.Delito, Listas.Lista.Zona, Listas.Lista.Link, Listas.Lista.Imagen, Listas.Lista.OtraInformacion, (CASE WHEN Listas.Lista.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END) AS Estado FROM Listas.Lista LEFT JOIN Parametrizacion.ClasesLista ON Listas.Lista.IdTipoLista = Parametrizacion.ClasesLista.CodigoClaseLisa " + condicion);                
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

        public void agregarLista(String NombreClaseLista)
        {
            try
            {
                parameters = new OleDbParameter[1];
                parameter = new OleDbParameter("@NombreClaseLista", OleDbType.Char);
                parameter.Value = NombreClaseLista;
                parameters[0] = parameter;
                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("agregarLista", parameters);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

        }

        public DataTable loadInfoAnalisisClientes()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT	Nombre, Identificacion FROM Listas.ClientesAnalisisPEPSEncontrados");
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

        public void analisisClientes()
        {
            try
            {
                parameters = new OleDbParameter[1];
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Integer);
                parameter.Value = IdUsuario;
                parameters[0] = parameter;
                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("SP_AnalisisPEPSClientes", parameters);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable ConsultaListas(String nombre, String documento)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty;
            try
            {
                nombre = nombre.Trim();
                if (nombre != string.Empty)
                {
                    string[] strSeparator = new string[] { " " };
                    string[] arrName = nombre.Split(strSeparator, StringSplitOptions.None);
                    int i = arrName.Length;
                    for (int j = 0; j < i; j++)
                    {
                        if (condicion.ToString().Trim() == "")
                        {
                            condicion = "WHERE (Listas.Lista.NombreCompleto LIKE '%" + arrName[j] + "%') ";
                        }
                        else
                        {
                            condicion = condicion + "AND (Listas.Lista.NombreCompleto LIKE '%" + arrName[j] + "%') ";
                        }
                    }
                }
                documento = documento.Trim();
                if (documento != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.Lista.DocumentoIdentidad LIKE '%" + documento.ToString().Trim() + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.Lista.DocumentoIdentidad LIKE '%" + documento.ToString().Trim() + "%') ";
                    }

                }
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT DISTINCT LTRIM(RTRIM(Parametrizacion.ClasesLista.NombreClaseLista)) AS NombreClaseLista FROM Listas.Lista LEFT JOIN Parametrizacion.ClasesLista ON Listas.Lista.IdTipoLista = Parametrizacion.ClasesLista.CodigoClaseLisa " + condicion);
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
    }    
}