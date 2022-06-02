using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDtRegistroNoConformidad
    {
        public bool mtdConsultarAuditoriaMacroproceso(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdMacroProceso)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[IdAuditoria],a.Tema,b.Nombre"
                    +" FROM [Auditoria].[Auditoria] as a"
                    + " inner join Auditoria.Estandar as b on b.IdEstandar = a.IdEstandar"
                    + " inner join Procesos.Macroproceso as c on c.IdMacroProceso = a.IdProceso"
                    + " where c.IdMacroProceso ={0}", IdMacroProceso);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarAuditoriaProceso(ref DataTable dtCaracOut, ref string strErrMsg, ref int Proceso)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[IdAuditoria],a.Tema,b.Nombre"
                    + " FROM [Auditoria].[Auditoria] as a"
                    + " inner join Auditoria.Estandar as b on b.IdEstandar = a.IdEstandar"
                    + " inner join Procesos.Proceso as c on c.IdProceso = a.IdProceso"
                    + " where a.IdProceso ={0}", Proceso);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdInsertarNoConformidad(clsControlNoConformidad objCrlNo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                //DateTime date1 = new DateTime(0001, 1, 1, 0, 0, 0);
                /*int result = DateTime.Compare(objCrlNo.dtFechaFinal, date1);
                if (result == 0)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblControlNoConformidad] ([IdMacroProceso],[Descripcion],[FechaInicio],[Seguimiento],[FechaFinal],[CargoResponsable],[FechaRegistro],[IdUsuario],[PathFile]) " +
                        "VALUES ({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}')",
                        objCrlNo.intIdMacroProceso, objCrlNo.strDescripcion, objCrlNo.dtFechaInicio, objCrlNo.strSeguimiento, null, objCrlNo.intResponsable, objCrlNo.dtFechaRegistro, objCrlNo.intIdUsuario, objCrlNo.strPathFile);
                }
                else
                {*/
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblControlNoConformidad] ([IdMacroProceso]," +
                        "[Descripcion],[FechaInicio],[Seguimiento],[FechaFinal],[CargoResponsable],[FechaRegistro]," +
                        "[IdUsuario],[PathFile]) " + "VALUES ({0},'{1}',CONVERT(date,'{2}',126),'{3}',CONVERT(date,'{4}',126),{5},GETDATE(),{7},'{8}')",
                        objCrlNo.intIdMacroProceso, objCrlNo.strDescripcion, objCrlNo.dtFechaInicio, objCrlNo.strSeguimiento, 
                        objCrlNo.dtFechaFinal, objCrlNo.intResponsable, "GETDATE()", objCrlNo.intIdUsuario, objCrlNo.strPathFile);
                //}
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de No conformidad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdNoConformidad(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblControlNoConformidad]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarNoConformidadProceso(int IdNoconformidad, int IdProceso, int TipoProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                //DateTime date1 = new DateTime(0001, 1, 1, 0, 0, 0);
                /*int result = DateTime.Compare(objCrlNo.dtFechaFinal, date1);
                if (result == 0)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblControlNoConformidad] ([IdMacroProceso],[Descripcion],[FechaInicio],[Seguimiento],[FechaFinal],[CargoResponsable],[FechaRegistro],[IdUsuario],[PathFile]) " +
                        "VALUES ({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}')",
                        objCrlNo.intIdMacroProceso, objCrlNo.strDescripcion, objCrlNo.dtFechaInicio, objCrlNo.strSeguimiento, null, objCrlNo.intResponsable, objCrlNo.dtFechaRegistro, objCrlNo.intIdUsuario, objCrlNo.strPathFile);
                }
                else
                {*/
                strConsulta = string.Format("INSERT INTO [Procesos].[tblControlNoConformidadProceso] ([IdNoconformidad],[IdProceso],[IdTipoProceso])" +
                    "VALUES ({0},{1},{2})",
                    IdNoconformidad, IdProceso, TipoProceso);
                //}
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de No conformidad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarControlNoConformidad(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT DISTINCT [Id],[IdMacroProceso],[Nombre],[Descripcion],[FechaInicio],[Seguimiento],[FechaFinal],[CargoResponsable],[NombreHijo],[FechaRegistro],[IdUsuario],[Usuario],[PathFile]"
                    + " FROM [dbo].[vwControlNoConformidad] order by Id"
                    
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarControlNoConformidadSinCierre(ref DataTable dtCaracOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[IdMacroProceso],[Nombre],[Descripcion],[FechaInicio],[Seguimiento],[FechaFinal],[CargoResponsable],[NombreHijo],[FechaRegistro],[IdUsuario],[Usuario],[PathFile]"
                    + " FROM [dbo].[vwControlNoConformidad] where [FechaFinal] = '' and FechaRegistro between '{0} 00:00:00' and '{1} 23:59:59'", fechaInicial, fechaFinal

                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdControlNoConformidad(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblControlNoConformidad]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarAuditoriaConformidad(ref string strErrMsg,ref  int IdAuditoria,ref  int IdControl)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("INSERT INTO [Procesos].[tblNoConformidadAuditoria] ([IdAuditoria],[IdControlNoConformidad]) " +
                    "VALUES ({0},{1})",
                    IdAuditoria, IdControl);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de Auditoria No conformidad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarControlNoConformidadAud(ref DataTable dtCaracOut, ref string strErrMsg,ref int IdControl)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[IdAuditoria],b.tema,c.Nombre"
                    + " FROM [Procesos].[tblNoConformidadAuditoria] as a"
                    + " inner join [Auditoria].[Auditoria] as b on b.IdAuditoria = a.IdAuditoria"
                    + " inner join Auditoria.Estandar as c on c.IdEstandar = b.IdEstandar"
                    + " where [IdControlNoConformidad]  = {0}", IdControl); 

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdModificarNoConformidadSinArchivo(clsControlNoConformidad objCrlNo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Procesos].[tblControlNoConformidad] SET [FechaFinal] = '{0}',[Seguimiento] = '{1}'" +
                    "WHERE Id = {2}",
                    objCrlNo.dtFechaFinal, objCrlNo.strSeguimiento, objCrlNo.intId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de No conformidad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdModificarNoConformidadConArchivo(clsControlNoConformidad objCrlNo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Procesos].[tblControlNoConformidad] SET [FechaFinal] = '{0}',[Seguimiento] = '{1}', PathFile ='{3}'" +
                    "WHERE Id = {2}",
                    objCrlNo.dtFechaFinal, objCrlNo.strSeguimiento, objCrlNo.intId, objCrlNo.strPathFile);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de No conformidad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdIdPlaneacion(ref string strErrMsg,ref int IdAuditoria)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [IdPlaneacion],IdEstandar FROM [Auditoria].[Auditoria] where IdAuditoria = {0}", IdAuditoria);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de el IdPlaneacion. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public DataTable mtdPlaneacion(ref string strErrMsg, ref int IdPlaneacion)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [Nombre] FROM [Auditoria].[Planeacion] where IdPlaneacion = {0}", IdPlaneacion);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
    }
}