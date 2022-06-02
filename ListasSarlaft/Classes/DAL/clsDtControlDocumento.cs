using ListasSarlaft.Classes.DTO.Calidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDtControlDocumento : IDisposable
    {

        private cDataBase cDataBase = new cDataBase();

        public bool mtdInsertarActualizarControlDocumento(clsVersionDocumento objCrlVersion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                if (objCrlVersion.intId == 0)
                {
                    strConsulta = "INSERT INTO [Procesos].[tblVersionDocumento] ([IdMacroProceso], [IdProceso], [IdSubproceso], [CodigoDocumento],[FechaImplementacion],[IdTipoDocumento],[CargoResponsable] ,[UbicacionAlmacemiento],[Recuperacion],[TiempoRetencionActivo],[TiempoRetencionInactivo],[DisposionFinal],[MedioSoporte],[Formato],[FechaRegistro],[IdUsuario],[NombreDocumento],[FechaModificacion],[FechaEliminacion], [IdTipoProceso], [IdEstadoDocumento])" +
                    $"VALUES  ({objCrlVersion.intIdMacroProceso}, {(objCrlVersion.IdProceso.Equals(0) ? (object)"NULL" : objCrlVersion.IdProceso) }, {(objCrlVersion.IdSubproceso.Equals(0) ? (object)"NULL" : objCrlVersion.IdSubproceso)}, '{objCrlVersion.strCodigoDocumento}',CONVERT(date,'{objCrlVersion.dtFechaImplementacion}', 126),{objCrlVersion.intIdTipoDocumento},{objCrlVersion.intidCargoResponsable},'{objCrlVersion.strUbicacionAlmacenamiento}','{objCrlVersion.strRecuperacion}'," +
                    $"'{objCrlVersion.strTiempoRetencionActivo}','{objCrlVersion.strTiempoRetencionInactivo}','{objCrlVersion.strDisposicionFinal}','{objCrlVersion.strMedioSoporte}', '{objCrlVersion.strFormato}', GETDATE(), '{objCrlVersion.intIdUsuario}','{objCrlVersion.strNombreDocumento}',CONVERT(date,'{objCrlVersion.dtFechaModificacion}', 126),CONVERT(date, '{objCrlVersion.dtFechaEliminacion}', 126), '{objCrlVersion.intIdTipoProceso}', 1)";

                }
                else {
                    strConsulta = $"UPDATE [Procesos].[tblVersionDocumento] SET [IdMacroProceso] = {objCrlVersion.intIdMacroProceso}, [IdProceso] = {(objCrlVersion.IdProceso.Equals(0) ? (object)"NULL" : objCrlVersion.IdProceso) }" +
                        $", [IdSubproceso] = {(objCrlVersion.IdSubproceso.Equals(0) ? (object)"NULL" : objCrlVersion.IdSubproceso)}, [CodigoDocumento] = '{objCrlVersion.strCodigoDocumento}', [FechaImplementacion] = CONVERT(date, '{objCrlVersion.dtFechaImplementacion}', 126)" +
                        $", [IdTipoDocumento] = {objCrlVersion.intIdTipoDocumento}, [CargoResponsable] = {objCrlVersion.intidCargoResponsable}, [UbicacionAlmacemiento] = '{objCrlVersion.strUbicacionAlmacenamiento}', [Recuperacion] = '{objCrlVersion.strRecuperacion}'" +
                        $", [TiempoRetencionActivo] = '{objCrlVersion.strTiempoRetencionActivo}', [TiempoRetencionInactivo] = '{objCrlVersion.strTiempoRetencionInactivo}', [DisposionFinal] = '{objCrlVersion.strDisposicionFinal}', [MedioSoporte] = '{objCrlVersion.strMedioSoporte}'" +
                        $", [Formato] = '{objCrlVersion.strFormato}', [NombreDocumento] = '{objCrlVersion.strNombreDocumento}', [FechaModificacion] = CONVERT(date, '{objCrlVersion.dtFechaModificacion}', 126), [FechaEliminacion] = '{objCrlVersion.dtFechaEliminacion}', [IdTipoProceso] = '{objCrlVersion.intIdTipoProceso}'" +
                        $", [IdEstadoDocumento] = {objCrlVersion.IdEstadoDocumento} WHERE [Id] = {objCrlVersion.intId}";
                }
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
        public DataTable mtdLastIdControlVersion(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblVersionDocumento]");

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
        public bool mtdInsertarControlVersion(clsControlVersion objCrlVersion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblControlVersion] SET [bitActivo] = 0" +
                            "where IdVersionDocumento={0}",
                            objCrlVersion.intIdVersionDocumento);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();

                strConsulta = "INSERT INTO [Procesos].[tblControlVersion] ([IdVersionDocumento] ,[Version] ,[FechaModificacion],[FechaEliminacion],[Observaciones],[FechaRegistro],[IdUsuario],[pathFile],[bitActivo],[IdTipoDocumento],[IdMacroProceso],[IdProceso],[IdSubproceso],[CodigoDocumento],[FechaImplementacion],[CargoResponsable],[UbicacionAlmacemiento],[Recuperacion],[TiempoRetencionActivo],[TiempoRetencionInactivo],[DisposionFinal],[MedioSoporte],[Formato],[NombreDocumento],[IdTipoProceso],[JustificacionCambios],[IdEstadoDocumento])" +
                $"VALUES  ({objCrlVersion.intIdVersionDocumento},'{objCrlVersion.strVersion}','{objCrlVersion.dtFechaModificacion}','{objCrlVersion.dtFechaEliminacion}','{objCrlVersion.strObservaciones}',GETDATE(),{objCrlVersion.intIdUsuario},'{objCrlVersion.strPathFIle}',1,{objCrlVersion.intIdTipoDocumento}" +
                $", {objCrlVersion.IdMacroProceso}, {(objCrlVersion.IdProceso.Equals(0) ? (object)"NULL" : objCrlVersion.IdProceso)}, {(objCrlVersion.IdSubproceso.Equals(0) ? (object)"NULL" : objCrlVersion.IdSubproceso)}, '{objCrlVersion.CodigoDocumento}', '{objCrlVersion.FechaImplementacion}', {objCrlVersion.CargoResponsable}" +
                $", '{objCrlVersion.UbicacionAlmacemiento}', '{objCrlVersion.Recuperacion}', '{objCrlVersion.TiempoRetencionActivo}', '{objCrlVersion.TiempoRetencionInactivo}', '{objCrlVersion.DisposionFinal}', '{objCrlVersion.MedioSoporte}', '{objCrlVersion.Formato}', '{objCrlVersion.NombreDocumento}', {objCrlVersion.IdProceso}" +
                $", '{objCrlVersion.JustificacionCambios}', {(objCrlVersion.IdEstadoDocumento.Equals(0) ? 1 : objCrlVersion.IdEstadoDocumento)})";

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
        public bool mtdUpdateControlVersion(clsControlVersion objCrlVersion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                if(string.IsNullOrEmpty(objCrlVersion.JustificacionCambios) == false)
                {
                    strConsulta = string.Format("UPDATE [Procesos].[tblControlVersion] SET [FechaModificacion] = '{0}'" +
                            ",[JustificacionCambios] = '{1}' " +
                            "where Id={2}", objCrlVersion.dtFechaModificacion, objCrlVersion.JustificacionCambios,
                            objCrlVersion.intId);
                }else
                {
                    strConsulta = string.Format("UPDATE [Procesos].[tblControlVersion] SET [FechaAprobacion] = GETDATE()" +
                            ",[JustificacionAprobacion] = '{0}',  [UsuarioAprobacion] = {1}" +
                            "where Id={2} ", objCrlVersion.JustificacionAprobacion, objCrlVersion.UsuarioAprobacion,
                            objCrlVersion.intId);
                }
                
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();

                /*strConsulta = "INSERT INTO [Procesos].[tblControlVersion] ([IdVersionDocumento] ,[Version] ,[FechaModificacion],[FechaEliminacion],[Observaciones],[FechaRegistro],[IdUsuario],[pathFile],[bitActivo],[IdTipoDocumento],[IdMacroProceso],[IdProceso],[IdSubproceso],[CodigoDocumento],[FechaImplementacion],[CargoResponsable],[UbicacionAlmacemiento],[Recuperacion],[TiempoRetencionActivo],[TiempoRetencionInactivo],[DisposionFinal],[MedioSoporte],[Formato],[NombreDocumento],[IdTipoProceso],[JustificacionCambios],[IdEstadoDocumento])" +
                $"VALUES  ({objCrlVersion.intIdVersionDocumento},'{objCrlVersion.strVersion}','{objCrlVersion.dtFechaModificacion}','{objCrlVersion.dtFechaEliminacion}','{objCrlVersion.strObservaciones}',GETDATE(),{objCrlVersion.intIdUsuario},'{objCrlVersion.strPathFIle}',1,{objCrlVersion.intIdTipoDocumento}" +
                $", {objCrlVersion.IdMacroProceso}, {(objCrlVersion.IdProceso.Equals(0) ? (object)"NULL" : objCrlVersion.IdProceso)}, {(objCrlVersion.IdSubproceso.Equals(0) ? (object)"NULL" : objCrlVersion.IdSubproceso)}, '{objCrlVersion.CodigoDocumento}', '{objCrlVersion.FechaImplementacion}', {objCrlVersion.CargoResponsable}" +
                $", '{objCrlVersion.UbicacionAlmacemiento}', '{objCrlVersion.Recuperacion}', '{objCrlVersion.TiempoRetencionActivo}', '{objCrlVersion.TiempoRetencionInactivo}', '{objCrlVersion.DisposionFinal}', '{objCrlVersion.MedioSoporte}', '{objCrlVersion.Formato}', '{objCrlVersion.NombreDocumento}', {objCrlVersion.IdProceso}" +
                $", '{objCrlVersion.JustificacionCambios}', {(objCrlVersion.IdEstadoDocumento.Equals(0) ? 1 : objCrlVersion.IdEstadoDocumento)})";

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);*/
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

        public bool mtdConsultarControlVersion(ref DataTable dtCaracOut, ref string strErrMsg, Dictionary<string, string> filtrosBusqueda)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdCadenaValor", SqlDbType = SqlDbType.VarChar, Value =  filtrosBusqueda["IdCadenaValor"].Equals("0") ? "" : filtrosBusqueda["IdCadenaValor"]},
                    new SqlParameter() { ParameterName = "@IdMacroproceso", SqlDbType = SqlDbType.VarChar, Value =  filtrosBusqueda["IdMacroProceso"].Equals("0") ? "" : filtrosBusqueda["IdMacroProceso"]},
                    new SqlParameter() { ParameterName = "@IdProceso", SqlDbType = SqlDbType.VarChar, Value =  filtrosBusqueda["IdProceso"].Equals("0") ? "" : filtrosBusqueda["IdProceso"] },
                    new SqlParameter() { ParameterName = "@IdSubproceso", SqlDbType = SqlDbType.VarChar, Value =  filtrosBusqueda["IdSubproceso"].Equals("0") ? "" : filtrosBusqueda["IdSubproceso"]},
                    new SqlParameter() { ParameterName = "@NombreDocumento", SqlDbType = SqlDbType.VarChar, Value =  filtrosBusqueda["NombreDocumento"] },
                    new SqlParameter() { ParameterName = "@CodigoDocumento", SqlDbType = SqlDbType.VarChar, Value =  filtrosBusqueda["CodigoDocumento"] },
                    new SqlParameter() { ParameterName = "@FechaImplementacion", SqlDbType = SqlDbType.VarChar, Value =  filtrosBusqueda["FechaImplementacion"] },
                    new SqlParameter() { ParameterName = "@IdTipoDocumento", SqlDbType = SqlDbType.VarChar, Value =  filtrosBusqueda["IdTipoDocumento"].Equals("0") ? "" :filtrosBusqueda["IdTipoDocumento"] }
                };

                dtCaracOut = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarDocumentosControl]", parametros);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }

            return boolResult;
        }
        public bool Guardar(string nombrearchivo, int length, byte[] archivo, int IdRegistro, ref string strErrMsg, string extension)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars
            /*using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString()))
            {
                try
                {

                    conn.Open();

                    string query = @"INSERT INTO Procesos.Archivos (nombre, length, archivo, Proceso, IdRegistro, extension)
                             VALUES (@name, @length, @archivo, @Proceso, @IdRegistro, @extension)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@name", nombrearchivo);
                    cmd.Parameters.AddWithValue("@length", length);
                    cmd.Parameters.AddWithValue("@Proceso", "ControlDocumento");
                    cmd.Parameters.AddWithValue("@IdRegistro", IdRegistro);
                    cmd.Parameters.AddWithValue("@extension", extension);

                    SqlParameter archParam = cmd.Parameters.Add("@archivo", SqlDbType.VarBinary);
                    archParam.Value = archivo;

                    cmd.ExecuteNonQuery();

                    booResult = true;
                }
                catch (Exception ex)
                {
                    strErrMsg = string.Format("Error al Cargar el Archivo. [{0}]", ex.Message);
                }
                return booResult;
            }*/
            try
            {
                strConsulta = string.Format("INSERT INTO Procesos.Archivos " +
                    "(nombre, length, archivo, Proceso, IdRegistro, extension) " +
                    "VALUES ('{0}', {1}, @PdfData, '{2}', {3}, '{4}')",
                    nombrearchivo, length, "ControlDocumento", IdRegistro, extension);

                cDatabase.mtdConectarSql();
                cDatabase.mtdEjecutarConsultaSQL(strConsulta, archivo);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el archivo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;

        }
        public bool mtdConsultarVersion(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdControlDocumento)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {

                strConsulta = string.Format("SELECT [Id],[IdVersionDocumento],[Version],[FechaModificacion],[FechaEliminacion],[Observaciones],[pathFile],[bitActivo],IdTipoDocumento, [JustificacionCambios]" +
                    ",[JustificacionAprobacion],[UsuarioAprobacion],[FechaAprobacion],[CargoResponsable],[IdUsuario] " +
                " FROM [Procesos].[tblControlVersion]" +
                " where IdVersionDocumento = {0} order by Id desc"
                , IdControlDocumento);

                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdControlDocumento", SqlDbType = SqlDbType.Int, Value =  IdControlDocumento},
                };
                dtCaracOut = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarVersiones]", parametros);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }

            return boolResult;
        }
        public bool mtdUpdateVersion(clsControlVersion objCrlVersion, ref string strErrMsg, int IdDocumento)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            string strConsultaDocumento = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblControlVersion] SET [bitActivo] = 0" +
                            "where IdVersionDocumento={0}",
                            objCrlVersion.intIdVersionDocumento);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                DateTime date1 = new DateTime(0001, 1, 1, 0, 0, 0);

                strConsultaDocumento = string.Format("UPDATE [Procesos].[tblVersionDocumento] SET [IdTipoDocumento] = {0}"
                    + " WHERE Id = {1}", IdDocumento, objCrlVersion.intIdVersionDocumento);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsultaDocumento);
                cDatabase.desconectar();
                /*int result = DateTime.Compare(objCrlVersion.dtFechaEliminacion, date1);
                if (result == 0)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblControlVersion] ([IdVersionDocumento] ,[Version] ,[FechaModificacion],[FechaEliminacion],[Observaciones],[FechaRegistro],[IdUsuario],[pathFile],[bitActivo])" +
                            "VALUES  ({0},'{1}','{2}','{3}','{4}','{5}',{6},'{7}',1)",
                            objCrlVersion.intIdVersionDocumento, objCrlVersion.strVersion, objCrlVersion.dtFechaModificacion, null, objCrlVersion.strObservaciones,
                            objCrlVersion.dtFechaRegistro, objCrlVersion.intIdUsuario, objCrlVersion.strPathFIle);
                }
                else
                {*/
                strConsulta = "INSERT INTO [Procesos].[tblControlVersion] ([IdVersionDocumento] ,[Version] ,[FechaModificacion],[FechaEliminacion],[Observaciones],[FechaRegistro],[IdUsuario],[pathFile],[bitActivo],[IdTipoDocumento])" +
                            $"VALUES  ({objCrlVersion.intIdVersionDocumento},'{objCrlVersion.strVersion}','{objCrlVersion.dtFechaModificacion}','{objCrlVersion.dtFechaEliminacion}','{objCrlVersion.strObservaciones}',GETDATE(),{objCrlVersion.intIdUsuario},'{objCrlVersion.strPathFIle}',1,{objCrlVersion.intIdTipoDocumento})";
                //}
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Factor. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdDownLoadFile(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdRegistro, string filename)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {

                strConsulta = string.Format("SELECT [archivo]" +
                " FROM [Procesos].[Archivos]" +
                " where Proceso = 'ControlDocumento' and IdRegistro = {0} and Nombre = '{1}'"
                , IdRegistro, filename);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public DataTable mtdDownLoadFileData(ref DataTable dtCaracOut, ref string strErrMsg, ref int IdRegistro, string filename)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {

                strConsulta = string.Format("SELECT [Extension]" +
                " FROM [Procesos].[Archivos]" +
                " where Proceso = 'ControlDocumento' and IdRegistro = {0} and Nombre = '{1}'"
                , IdRegistro, filename);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtCaracOut;
        }

        public List<EstadoDocumento> opcionesEstadoDocumento()
        {
            try
            {
                List<EstadoDocumento> lst = new List<EstadoDocumento>();
                List<SqlParameter> parametros = new List<SqlParameter>();
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarEstadosDocumento]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        lst.Add(new EstadoDocumento()
                        {
                            IdEstadoDocumento = Convert.ToInt32(row["IdEstadoDocumento"]),
                            NombreEstadoDocumento = row["NombreEstadoDocumento"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerDireccionCorreo(int idJerarquia)
        {
            try
            {
                string direccionCorreo = string.Empty;
                List<EstadoDocumento> lst = new List<EstadoDocumento>();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdJerarquia", SqlDbType = SqlDbType.Int, Value = idJerarquia  },
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarCorreo]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    direccionCorreo = dt.Rows[0][0].ToString();
                }
                return direccionCorreo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {

        }
    }
}