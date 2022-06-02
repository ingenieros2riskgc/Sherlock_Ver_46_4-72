using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using clsDTO;
using System.Data.SqlClient;

namespace clsDatos
{
    public class clsDtArchivo
    {
        /// <summary>
        /// Metodo que permite descargar los archivos de la base de datos.
        /// </summary>
        /// <param name="strNombreArchivo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdDescargarArchivo(string strNombreArchivo, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDataBase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdArchivo], [FechaRegistro], [UrlArchivo], [Archivo] FROM [Perfiles].[tblArchivos] WHERE [UrlArchivo] = N'{0}'",
                    strNombreArchivo);

                cDataBase.mtdConectarSql();
                dtInformacion = cDataBase.mtdEjecutarConsultaSQL(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los archivos agregados. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.mtdDesconectarSql();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Metodo que permite consultar el consecutivo que va en el la base de datos
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdConsultarConsecutivoArchivo(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDataBase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT TOP (1) IdArchivo + 1 AS NumRegistros FROM [Perfiles].[tblArchivos] ORDER BY IdArchivo DESC");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los archivos agregados. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        ///  Metodo que permite agregar los archivos
        /// </summary>
        /// <param name="objArchivo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public int mtdAgregarArchivo(clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            int intRetorno = 0;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDataBase = new clsDatabase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblArchivos] ([FechaRegistro], [UrlArchivo], [Archivo]) VALUES (GETDATE(), '{0}', @PdfData) SELECT @@IDENTITY Ultimo",
                    objArchivo.StrUrlArchivo);

                cDataBase.mtdConectarSql();
                intRetorno = cDataBase.mtdEjecutarConsultaSQLRetorno(strConsulta, objArchivo.BArchivo);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al agregar el archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.mtdDesconectarSql();
            }

            return intRetorno;
        }

        /// <summary>
        /// Consulta los archivos cargados
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdConsultarArchivos(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdArchivo], [FechaRegistro], [UrlArchivo], [Archivo] FROM [Perfiles].[tblArchivos]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los archivos agregados. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza el cargue de la informacion del archivo a base de datos campo por campo
        /// </summary>
        /// <param name="intIdArchivo"></param>
        /// <param name="strLineas"></param>
        /// <param name="intNumeroCampos"></param>
        /// <param name="strErrMsg"></param>
        public void mtdCargarInfoArchivo(clsDTOArchivo objArchivo, string[] strLineas,
            int intNumeroCampos, ref string strErrMsg)
        {
            #region Vars
            int intContadorLineas = 0;
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase();
            #endregion Vars

            try
            {

                string[] strCabecero = new string[] { };

                foreach (string strLine in strLineas)
                {
                    intContadorLineas++;

                    if (intContadorLineas == 1)
                    {
                        // Se construye el cabecero del archivo
                        strCabecero = strLine.Split(';');
                        continue;
                    };

                    #region Recorre los campos para insertar
                    if (string.IsNullOrEmpty(strLine.Trim()))
                        continue;

                    // Obtiene cada linea del archivo para insertar
                    string[] strCampos = strLine.Split(';');

                    if (strCampos.Length <= intNumeroCampos)
                    {
                        if (strCampos.Length == intNumeroCampos)
                        {
                            // Se obtiene la posicion en la que va el dato
                            clsDatabase cDataBase = new clsDatabase();
                            using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("Perfiles.SeleccionarEstructura", new List<SqlParameter>()))
                            {
                                string[] cabecerosBd = new string[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    cabecerosBd[i] = dt.Rows[i]["Cabecero"].ToString().ToUpper().Trim();
                                }
                                foreach (string cabeceroArchivo in strCabecero)
                                {
                                    if (!cabecerosBd.Contains(cabeceroArchivo.ToUpper().Trim()))
                                        throw new Exception("Una(s) columnas(s) del archivo no coinciden con la estructura");
                                }
                                for (int i = 0; i < strCampos.Length; i++)
                                {
                                    // Halla el número de la posición a cargar
                                    string cabeceroArchivo = strCabecero[i];
                                    DataRow dr = dt.Select($"Cabecero = '{cabeceroArchivo.ToUpper().Trim()}'").FirstOrDefault();
                                    string posicion = dr["Posicion"].ToString();
                                    strConsulta = string.Format("INSERT INTO [Perfiles].[tblInfoCargueArchivo]([IdArchivo], [ValorCampoArchivo], [Posicion], [NumeroLinea]) " +
                                                "VALUES ({0},'{1}',{2}, {3})", objArchivo.StrIdArchivo, strCampos[i].ToString().Trim(), posicion, intContadorLineas);
                                    cDatabase.conectar();
                                    cDatabase.ejecutarQuery(strConsulta);
                                    cDatabase.desconectar();
                                }
                            }
                        }
                        else
                        {
                            strErrMsg = string.Format("Error al agregar el archivo. El número de campos [{0}] es menor al configurado.", strCampos.Length);
                            break;
                        }
                    }
                    else
                    {
                        strErrMsg = string.Format("Error al agregar el archivo. El número de campos [{0}] es mayor al configurado.", strCampos.Length);
                        break;
                    }
                    #endregion Recorre los campos para insertar
                    }
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                strErrMsg = string.Format("Error al agregar el archivo. [{0}]", ex.Message);
            }
        }

        /// <summary>
        /// Borra la informacion de las tablas tblInfoCargueArchivo y tblArchivos
        /// </summary>
        /// <param name="intOpcion"></param>
        /// <param name="intIdArchivo"></param>
        public void mtdBorrarErroresArchivo(int intOpcion, clsDTOArchivo objArchivo)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase();
            #endregion Vars

            try
            {
                switch (intOpcion)
                {
                    case 1:
                        strConsulta = string.Format("DELETE [Perfiles].[tblInfoCargueArchivo] WHERE IdArchivo = {0}" +
                            " DELETE [Perfiles].[tblHistoricoCalculoPerfil] WHERE IdArchivo = {0}" +
                            " DELETE [Perfiles].[tblArchivos] WHERE IdArchivo = {0}", objArchivo.StrIdArchivo);

                        cDatabase.conectar();
                        cDatabase.ejecutarQuery(strConsulta);
                        break;

                    case 2:

                        break;
                }
            }
            catch
            {
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        /// <summary>
        /// Permite consultar las identificadores de las lineas cargadas en el archivo, 
        /// cantidad de registros en el archivo
        /// </summary>
        /// <param name="objArchivo">Objeto que oermite saber cual es el archivo que se esta trabajando</param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdNumeroRegistrosArchivo(clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT DISTINCT (NumeroLinea) FROM [Perfiles].[tblInfoCargueArchivo] WHERE [IdArchivo] = {0}",
                    objArchivo.StrIdArchivo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
                //intRetorno = Convert.ToInt32(dtInformacion.Rows[0]["NumeroLinea"].ToString().Trim());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los registros del archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Metodo que permite hacer la consulta en base de datos de la informacion cargada del archivo de clientes.
        /// </summary>
        /// <param name="intIdArchivo"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public DataTable mtdConsultarInfoCargada(clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Posicion], [ValorCampoArchivo], [NumeroLinea] FROM [Perfiles].[tblInfoCargueArchivo] " +
                    "WHERE [IdArchivo] = {0} ORDER BY NumeroLinea", objArchivo.StrIdArchivo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los registros del archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        /*
         * Metodos para el Servicio
         */
        public void mtdBorrarErroresArchivo(string strConn, int intOpcion, clsDTOArchivo objArchivo)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase(strConn, 2);
            #endregion Vars

            try
            {
                switch (intOpcion)
                {
                    case 1:
                        strConsulta = string.Format("DELETE [Perfiles].[tblInfoCargueArchivo] WHERE IdArchivo = {0}" +
                            " DELETE [Perfiles].[tblHistoricoCalculoPerfil] WHERE IdArchivo = {0}" +
                            " DELETE [Perfiles].[tblArchivos] WHERE IdArchivo = {0}", objArchivo.StrIdArchivo);

                        cDatabase.conectar();
                        cDatabase.ejecutarQuery(strConsulta);
                        break;

                    case 2:
                        break;
                }
            }
            catch { }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public DataTable mtdConsultarConsecutivoArchivo(string strConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDataBase = new clsDatabase(strConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT TOP (1) IdArchivo + 1 AS NumRegistros FROM [Perfiles].[tblArchivos] ORDER BY IdArchivo DESC");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los archivos agregados. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        public int mtdAgregarArchivo(string strConn, clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            int intRetorno = 0;
            clsDatabase cDataBase = new clsDatabase(strConn, 1);
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Perfiles].[tblArchivos] ([FechaRegistro], [UrlArchivo], [Archivo]) VALUES (GETDATE(), '{0}', @PdfData) SELECT @@IDENTITY Ultimo",
                    objArchivo.StrUrlArchivo);

                cDataBase.mtdConectarSql();
                intRetorno = cDataBase.mtdEjecutarConsultaSQLRetorno(strConsulta, objArchivo.BArchivo);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al agregar el archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.mtdDesconectarSql();
            }

            return intRetorno;
        }

        public void mtdCargarInfoArchivo(string strConn, clsDTOArchivo objArchivo, string[] strLineas,
            int intNumeroCampos, ref string strErrMsg)
        {
            #region Vars
            int intContadorLineas = 0;
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase(strConn, 2);
            #endregion Vars

            try
            {
                foreach (string strLine in strLineas)
                {
                    intContadorLineas++;
                    if (intContadorLineas == 1)
                        continue;

                    #region Recorre los campos para insertar
                    if (string.IsNullOrEmpty(strLine.Trim()))
                        continue;

                    string[] strCampos = strLine.Split(';');

                    if (strCampos.Length <= intNumeroCampos)
                    {
                        if (strCampos.Length == intNumeroCampos)
                        {
                            for (int intContador = 0; intContador < intNumeroCampos; intContador++)
                            {
                                strConsulta = string.Format("INSERT INTO [Perfiles].[tblInfoCargueArchivo]([IdArchivo], [ValorCampoArchivo], [Posicion], [NumeroLinea]) " +
                                    "VALUES ({0},'{1}',{2}, {3})", objArchivo.StrIdArchivo, strCampos[intContador].ToString().Trim(), intContador + 1, intContadorLineas);

                                cDatabase.conectar();
                                cDatabase.ejecutarQuery(strConsulta);
                                cDatabase.desconectar();
                            }
                        }
                        else
                        {
                            strErrMsg = string.Format("Error al agregar el archivo. El número de campos [{0}] es menor al configurado.", strCampos.Length);
                            break;
                        }
                    }
                    else
                    {
                        strErrMsg = string.Format("Error al agregar el archivo. El número de campos [{0}] es mayor al configurado.", strCampos.Length);
                        break;
                    }
                    #endregion Recorre los campos para insertar
                }
            }
            catch (Exception ex)
            {
                cDatabase.desconectar();
                strErrMsg = string.Format("Error al agregar el archivo. [{0}]", ex.Message);
            }
        }

        public DataTable mtdNumeroRegistrosArchivo(string strConn, clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase(strConn, 2);
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT DISTINCT (NumeroLinea) FROM [Perfiles].[tblInfoCargueArchivo] WHERE [IdArchivo] = {0}",
                    objArchivo.StrIdArchivo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
                //intRetorno = Convert.ToInt32(dtInformacion.Rows[0]["NumeroLinea"].ToString().Trim());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los registros del archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarInfoCargada(string strConn, clsDTOArchivo objArchivo, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase(strConn, 2);
            DataTable dtInformacion = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Posicion], [ValorCampoArchivo], [NumeroLinea] FROM [Perfiles].[tblInfoCargueArchivo] " +
                    "WHERE [IdArchivo] = {0} ORDER BY NumeroLinea", objArchivo.StrIdArchivo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los registros del archivo. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarArchivos(string strFechaInicial, string strFechaFinal, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdArchivo], [FechaRegistro], [UrlArchivo], [Archivo] FROM [Perfiles].[tblArchivos] " +
                    "WHERE [FechaRegistro] BETWEEN CONVERT(DATETIME,'{0} 00:00:00',103) AND CONVERT(DATETIME,'{1} 23:59:59',103)", strFechaInicial, strFechaFinal);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los archivos. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
    }
}
