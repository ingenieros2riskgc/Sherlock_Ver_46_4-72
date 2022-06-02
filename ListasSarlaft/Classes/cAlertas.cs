using System;
using System.Collections.Generic;
using System.Web;
using System.Data.OleDb;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using System.Data;
using System.Configuration;
using System.Linq;

namespace ListasSarlaft.Classes
{
    public class cAlertas : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();

        public DataTable VerAlertas()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdSenal,NombreSenal,TipoCliente,Frecuencia,CantidadGiros,SumaGiros,ValorMinGiro,ValorMaxGiro,CantidadOficinas,TipoIdentificacion FROM Proceso.SenalesAlerta");
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

        public void modificarAlerta(String condicion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery(condicion);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void insertarrAlerta(String NombreSenal, String TipoCliente, String Frecuencia, String CantidadGiros, String SumaGiros, String ValorMinGiro, String ValorMaxGiro, String TipoIdentificacion)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("insert into Proceso.SenalesAlerta (NombreSenal,TipoCliente,Frecuencia,CantidadGiros,SumaGiros,ValorMinGiro,ValorMaxGiro,TipoIdentificacion) values ('" + NombreSenal + "','" + TipoCliente + "','" + Frecuencia + "'," + CantidadGiros + "," + SumaGiros + "," + ValorMinGiro + "," + ValorMaxGiro + ",'" + TipoIdentificacion + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public DataTable AnalizarAlertas(String condicion)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(condicion);
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

        public DataTable CantidadConteos()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select COUNT(*) as TConteo from Proceso.ConteoRegistro");
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

        public void truncateTable(String table)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("TRUNCATE TABLE " + table);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void loadGiros(DataTable dtInfo)
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
                            sqlBCopy.DestinationTableName = "Proceso.ArchivoGiros";
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

        public void loadPremios(DataTable dtInfo)
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
                            sqlBCopy.DestinationTableName = "Proceso.ArchivoPremios";
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

        public void loadOficinas(DataTable dtInfo)
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
                            sqlBCopy.DestinationTableName = "Proceso.ArchivoOficinas";
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

        public void loadOficinasRangos(DataTable dtInfo)
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
                            sqlBCopy.DestinationTableName = "Proceso.ArchivoOficinasRangos";
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

        public void InsertConteoRegistro(int CantRegistros)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("insert into Proceso.ConteoRegistro (NombreUsuario,FechaRegistro,RegistrosCargue,RegistrosOperacion,Descripcion) values ('Oficial de Cumplimiento',GETDATE(),1," + CantRegistros + ",'Señal de alerta')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InsertRegistroOperacion(String IdUsuario, String Identificacion,String NombreApellido,int IdConteo,String Cant, String Valor, String Frecuencia,String TipoCliente,String IdSenal,String DescSenal)
        {
            try
            {
                cDataBase.conectar();
                if(IdSenal=="16")
                {
                    cDataBase.ejecutarQuery("insert into Proceso.RegistroOperacion (IdTipoRegistro,IdEstadoOperacion,IdUsuario,Identificacion,NombreApellido,IdIndicador,NombreIndicador,Indicador,MensajeCorreo,FechaRegistro,FechaDeteccion,IdConteo) values (1,1," + IdUsuario + "," + Identificacion + ",'" + NombreApellido + "',0,'Señal de alerta No." + IdSenal + "','Punto de Venta con categoría " + Cant + " y giros por valor de " + Valor + "','Señal de Alerta: " + DescSenal + "',GETDATE(),GETDATE()," + IdConteo + ")");
                }
                else
                {
                    cDataBase.ejecutarQuery("insert into Proceso.RegistroOperacion (IdTipoRegistro,IdEstadoOperacion,IdUsuario,Identificacion,NombreApellido,IdIndicador,NombreIndicador,Indicador,MensajeCorreo,FechaRegistro,FechaDeteccion,IdConteo) values (1,1," + IdUsuario + "," + Identificacion + ",'" + NombreApellido + "',0,'Señal de alerta No." + IdSenal + "','" + TipoCliente + " con frecuencia " + Frecuencia + " con " + Cant + " operaciones por valor de " + Valor + "','Señal de Alerta: " + DescSenal + "',GETDATE(),GETDATE()," + IdConteo + ")");
                }
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