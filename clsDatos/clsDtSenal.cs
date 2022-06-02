using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using clsDTO;

namespace clsDatos
{
    public class clsDtSenal
    {
        #region Senal
        public DataTable mtdConsultaSenal(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdUsuario], [IdSenal], [CodigoSenal], [DescripcionSenal], ISNULL([EsAutomatico], 'False') EsAutomatico FROM [Perfiles].[tblSenalAlerta]");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las señales de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaSenalMod(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.IdSenal, a.CodigoSenal, a.DescripcionSenal, a.IdUsuario, " +
                    "a.FechaModificacion, rtrim(b.Usuario)Usuario FROM Perfiles.tblSenalAlerta_logAuditoria a " +
                    "LEFT JOIN Listas.Usuarios b on a.IdUsuario = b.IdUsuario ORDER BY a.FechaModificacion DESC");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las señales de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaConsulSenalMod(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.IdSenal, a.CodigoSenal, a.DescripcionSenal, a.FechaInicial, a.FechaFinal, " +
                    "a.NumeroCoincidencias, a.IdUsuario, rtrim(b.Usuario)Usuario, FechaConsulta FROM Perfiles.tblSenalAlerta_ConsulSenales a " +
                    "LEFT JOIN Listas.Usuarios b on a.IdUsuario = b.IdUsuario ORDER BY a.FechaConsulta DESC");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las señales de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaConsulFactorRiesgoMod(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdFactorRiesgo], PP.[CodigoFactorRiesgo], PP.[DescFactorRiesgo], PP.[IdUsuario], " +
                    "PP.[FechaModificacion], rtrim(b.Usuario)Usuario FROM [Perfiles].[tblFactorRiesgo_logAuditoria] PP " +
                    "LEFT JOIN Listas.Usuarios b ON PP.IdUsuario = b.IdUsuario INNER JOIN [Perfiles].[tblFactorRiesgo] PTP " +
                    "ON PTP.[IdFactorRiesgo] = PP.[IdFactorRiesgo] ORDER BY PP.IdFactorRiesgo, PP.FechaModificacion DESC");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las señales de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable ConsulModSenales(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.IdSenal, a.CodigoSenal, a.DescripcionSenal, a.FechaInicial, a.FechaFinal, " +
                    "a.NumeroCoincidencias, rtrim(b.Usuario)Usuario, FechaConsulta FROM Perfiles.tblSenalAlerta_ConsulSenales a " +
                    "LEFT JOIN Listas.Usuarios b on a.IdUsuario = b.IdUsuario ORDER BY a.IdSenal, a.FechaConsulta DESC");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las señales de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable ConsulModFactorRiesgo(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PP.[IdFactorRiesgo], PP.[CodigoFactorRiesgo], PP.[DescFactorRiesgo], PP.[IdUsuario], " +
                    "PP.[FechaModificacion], rtrim(b.Usuario)Usuario FROM [Perfiles].[tblFactorRiesgo_logAuditoria] PP " +
                    "LEFT JOIN Listas.Usuarios b ON PP.IdUsuario = b.IdUsuario INNER JOIN [Perfiles].[tblFactorRiesgo] PTP " +
                    "ON PTP.[IdFactorRiesgo] = PP.[IdFactorRiesgo] ORDER BY PP.IdFactorRiesgo, PP.FechaModificacion DESC");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las señales de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable ConsulSenales(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.CodigoSenal, a.DescripcionSenal, a.FechaModificacion, rtrim(b.Usuario)Usuario " +
                    "FROM Perfiles.tblSenalAlerta_logAuditoria a LEFT JOIN Listas.Usuarios b " +
                    "on a.IdUsuario = b.IdUsuario ORDER BY a.IdSenal, a.FechaModificacion DESC");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las señales de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }


        public void mtdAgregarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT [Perfiles].[tblSenalAlerta] ([CodigoSenal], [DescripcionSenal], [EsAutomatico]) VALUES ('{0}', '{1}', {2})",
                    objSenal.StrCodigoSenal, objSenal.StrDescripcionSenal, objSenal.BooEsAutomatico == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la señal de alerta. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public int mtdAgregarSenalRet(clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            int intRetorno = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT into [Perfiles].[tblSenalAlerta] ([IdUsuario], [CodigoSenal], [DescripcionSenal], " +
                    "[EsAutomatico]) VALUES ({0}, '{1}', '{2}', {3}) SELECT @@IDENTITY Ultimo",
                    objSenal.StrIdUsuario, objSenal.StrCodigoSenal, objSenal.StrDescripcionSenal, objSenal.BooEsAutomatico == false ? 0 : 1);
                cDatabase.mtdConectarSql();
                intRetorno = cDatabase.mtdEjecutarConsultaSQLRetorno(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la señal de alerta. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return intRetorno;
        }


        public void mtdActualizarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblSenalAlerta] SET [IdUsuario]= {1}, [CodigoSenal] = '{2}', [DescripcionSenal] = '{3}', [EsAutomatico] = {4} WHERE [IdSenal] = {0}",
                     objSenal.StrIdSenal, objSenal.StrIdUsuario, objSenal.StrCodigoSenal, objSenal.StrDescripcionSenal, objSenal.BooEsAutomatico == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la señal de alerta. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdEliminarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Perfiles].[tblSenalAlerta] WHERE [IdSenal] = {0}",
                        objSenal.StrIdSenal);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    strErrMsg = "Error en la eliminación de la información. <br/> La información a borrar tiene relación con algun objeto. <br/> Por favor revise la información.";
                else
                    strErrMsg = string.Format("Error en la eliminación de la información.<br/> Descripción: {0}.", odbcEx.Message.ToString());
            }
            catch (OleDbException odbcEx)
            {
                strErrMsg = mtdOdbcError(odbcEx);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al borrar la señal de alerta. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public DataTable mtdConsultarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                /*strConsulta = string.Format("SELECT [IdSenal], [CodigoSenal], [DescripcionSenal], [EsAutomatico] " +
                    "FROM [Perfiles].[tblSenalAlerta] WHERE [IdSenal] = {0}",
                    objSenal.StrIdSenal);*/
                strConsulta = string.Format("SELECT [IdSenal], [IdUsuario], [CodigoSenal], [DescripcionSenal], [EsAutomatico] " +
                    "FROM [Perfiles].[tblSenalAlerta] WHERE [IdSenal] = {1}",
                    objSenal.StrIdUsuario, objSenal.StrIdSenal);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar señal de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarFormSenalAuto(bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PSV.[IdSenalVariable], PSV.[IdSenal], PSV.[IdOperando], PSV.[Valor], PSV.[Posicion], PSV.[EsGlobal] " +
                    "FROM [Perfiles].[tblSenalVariable] PSV " +
                    "INNER JOIN [Perfiles].[tblSenalAlerta] PSA ON PSV.[IdSenal] = PSA.[IdSenal] " +
                    "WHERE PSA.[EsAutomatico] = {0} " +
                    "ORDER BY PSV.[IdSenalVariable]", booAutomatico == true ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Fórmulas. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarFormSenalAuto(clsDTOSenal objSenal, bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PSV.[IdSenalVariable], PSV.[IdSenal], PSV.[IdOperando], PSV.[Valor], PSV.[Posicion], PSV.[EsGlobal] " +
                    "FROM [Perfiles].[tblSenalVariable] PSV " +
                    "INNER JOIN [Perfiles].[tblSenalAlerta] PSA ON PSV.[IdSenal] = PSA.[IdSenal] " +
                    "WHERE ISNULL(PSA.[EsAutomatico], 0) = {0} AND PSV.[IdSenal] = {1} ORDER BY PSV.[IdSenalVariable]",
                    booAutomatico == true ? 1 : 0, objSenal.StrIdSenal, objSenal.StrIdUsuario);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Fórmulas. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        #endregion

        #region Tabla Procesos
        public void mtdInsertarNroRegistrosSA(int intNroRegistros, string strNombreUsuario, string strDescripcion, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT [Proceso].[ConteoRegistro] ([NombreUsuario], [FechaRegistro], [RegistrosCargue], [RegistrosOperacion], [Descripcion]) " +
                    "VALUES ('{0}', GETDATE(), 1, {1}, '{2}')",
                    strNombreUsuario, intNroRegistros, strDescripcion);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el número de registros. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

        }

        public int mtdConteoRegistros(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            int intRetorno = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) Ultimo FROM [Proceso].[ConteoRegistro] ");
                cDatabase.mtdConectarSql();
                intRetorno = cDatabase.mtdEjecutarConsultaSQLRetorno(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los conteos. [{0} - Tabla: Proceso.ConteoRegistro]", ex.Message);
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return intRetorno;
        }

        public void mtdInsertarRegOperacion(int intIdUsuario, string strIdentificacion, string strNombreApellido,
            int intConteoTblConteoRegistro, int intConteoOcurrencias,//IdConteo // Cant
            int intValor, int intFrecuencia, string strTipoCliente,
            string IdSenal, string strDescripcionSenal,// IdSenal // DescSenal
            string strIndicadorROI, ref string strErrMsg, string codSenal)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Proceso].[RegistroOperacion] " +
                    "(IdTipoRegistro, IdEstadoOperacion, IdUsuario, Identificacion, NombreApellido, IdIndicador ,NombreIndicador, Indicador, MensajeCorreo, FechaRegistro, FechaDeteccion, IdConteo) " +
                    //"VALUES (1, 1, {0}, {1}, '{2}', 0, 'Señal de alerta No. {3}','{4} con frecuencia {5} con {6} operaciones por valor de {7}','Señal de Alerta: {8}', GETDATE(), GETDATE(), {9})",
                    "VALUES (1, 1, {0}, '{1}', '{2}', {3}, 'Señal de alerta No. {11}','{10}','Señal de Alerta: {8}', GETDATE(), GETDATE(), {9})",
                    intIdUsuario, strIdentificacion, strNombreApellido, IdSenal, strTipoCliente, intFrecuencia, intConteoOcurrencias, intValor, strDescripcionSenal,
                    intConteoTblConteoRegistro, strIndicadorROI, codSenal);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al insertar el registro de operación. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }
        #endregion

        #region Operador
        public DataTable mtdConsultaOperador(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdOperador], [NombreOperador], [IdentificadorOperador] FROM [Perfiles].[tblOperador]");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar operadores. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdBuscarOperador(clsDTOOperador cOpIn, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdOperador], [NombreOperador], [IdentificadorOperador] FROM [Perfiles].[tblOperador] WHERE [IdOperador] = {0}",
                    cOpIn.StrIdOperador);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar operadores. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultaOperadorGlobal(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdOperador], [NombreOperador], [IdentificadorOperador] " +
                    "FROM [Perfiles].[tblOperadorGlobal]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar operadores. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdBuscarOperadorGlobal(clsDTOOperadorGlobal cOpIn, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdOperador], [NombreOperador], [IdentificadorOperador] " +
                    "FROM [Perfiles].[tblOperadorGlobal] WHERE [IdOperador] = {0}",
                    cOpIn.StrIdOperador);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar operadores. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }
        #endregion

        #region Formula
        public void mtdGuardarFormula(List<object> LstFormula, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            int intPos = 0;
            #endregion Vars

            try
            {
                cDatabase.conectar();
                foreach (object objFormula in LstFormula)
                {
                    intPos++;
                    clsDTOSenalVariable objFormTemp = new clsDTOSenalVariable();
                    objFormTemp = (clsDTOSenalVariable)objFormula;
                    if (objFormTemp.StrValor != string.Empty)
                    {
                        strConsulta = string.Format("INSERT [Perfiles].[tblSenalVariable] ([IdSenal],[IdOperando],[Valor],[Posicion],[EsGlobal]) " +
                        "VALUES ({0}, {1}, '{2}', {3}, {4})",
                        objFormTemp.StrIdSenal, objFormTemp.StrIdOperando, objFormTemp.StrValor, intPos.ToString(), objFormTemp.BooEsGlobal == false ? 0 : 1);
                        cDatabase.ejecutarQuery(strConsulta);
                    }
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Fórmula. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public DataTable mtdConsultarFormulas(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion], [EsGlobal] " +
                    "FROM [Perfiles].[tblSenalVariable] " +
                    "ORDER BY [IdSenalVariable]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Fórmulas. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarFormulaXSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion], [EsGlobal] " +
                    "FROM [Perfiles].[tblSenalVariable] " +
                    "WHERE [IdSenal] = {0} ORDER BY [IdSenalVariable]",
                    objSenal.StrIdSenal);
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Fórmulas. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public void mtdEliminarFormula(clsDTOSenalVariable objFormula, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Perfiles].[tblSenalVariable] WHERE [IdSenal] = {0}",
                        objFormula.StrIdSenal);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    strErrMsg = "Error en la eliminación de la información. <br/> La información a borrar tiene relación con algun objeto. <br/> Por favor revise la información.";
                else
                    strErrMsg = string.Format("Error en la eliminación de la información.<br/> Descripción: {0}.", odbcEx.Message.ToString());
            }
            catch (OleDbException odbcEx)
            {
                strErrMsg = mtdOdbcError(odbcEx);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al borrar la Fórmula. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }
        #endregion

        /*
         * Metodos para el Servicio
         */
        #region Servicio
        public DataTable mtdConsultarSenal(string strOleConn, clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdSenal], [CodigoSenal], [DescripcionSenal], ISNULL([EsAutomatico], 'False') EsAutomatico FROM [Perfiles].[tblSenalAlerta] WHERE [IdSenal] = {0}",
                    objSenal.StrIdSenal);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar señal de alerta. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdBuscarOperador(string strOleConn, clsDTOOperador cOpIn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdOperador], [NombreOperador], [IdentificadorOperador] FROM [Perfiles].[tblOperador] WHERE [IdOperador] = {0}",
                    cOpIn.StrIdOperador);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar operadores. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarFormulas(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion] FROM [Perfiles].[tblSenalVariable] " +
                    "ORDER BY [IdSenalVariable]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Fórmulas. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataTable mtdConsultarFormSenalAuto(string strOleConn, bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PSV.[IdSenalVariable], PSV.[IdSenal], PSV.[IdOperando], PSV.[Valor], PSV.[Posicion] " +
                    "FROM [Perfiles].[tblSenalVariable] PSV " +
                    "INNER JOIN [Perfiles].[tblSenalAlerta] PSA ON PSV.[IdSenal] = PSA.[IdSenal] " +
                    "WHERE PSA.[EsAutomatico] = {0} " +
                    "ORDER BY PSV.[IdSenalVariable]", booAutomatico == true ? 1 : 0);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Fórmulas. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public void mtdInsertarNroRegistrosSA(string strOleConn, int intNroRegistros, string strNombreUsuario, string strDescripcion, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT [Proceso].[ConteoRegistro] ([NombreUsuario], [FechaRegistro], [RegistrosCargue], [RegistrosOperacion], [Descripcion]) " +
                    "VALUES ('{0}', GETDATE(), 1, {1}, '{2}')",
                    strNombreUsuario, intNroRegistros, strDescripcion);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el número de registros. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

        }

        public int mtdConteoRegistros(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            int intRetorno = 0;
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) Ultimo FROM [Proceso].[ConteoRegistro] ");
                cDatabase.mtdConectarSql();
                intRetorno = cDatabase.mtdEjecutarConsultaSQLRetorno(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los conteos. [{0} - Tabla: Proceso.ConteoRegistro]", ex.Message);
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return intRetorno;
        }

        public void mtdInsertarRegOperacion(string strOleConn, int intIdUsuario,
            string strIdentificacion, string strNombreApellido,
            int intConteoTblConteoRegistro,// IdConteo
            int intConteoOcurrencias,// Cant
            int intValor, int intFrecuencia, string strTipoCliente,
            string strCodigoSenal,// IdSenal
            string strDescripcionSenal,// DescSenal
             string strIndicadorROI,
            ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase(strOleConn, 2);
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Proceso].[RegistroOperacion] " +
                    "(IdTipoRegistro, IdEstadoOperacion, IdUsuario, Identificacion, NombreApellido, IdIndicador ,NombreIndicador, Indicador, MensajeCorreo, FechaRegistro, FechaDeteccion, IdConteo) " +
                    //"VALUES (1, 1, {0}, {1}, '{2}', 0, 'Señal de alerta No. {3}','{4} con frecuencia {5} con {6} operaciones por valor de {7}','Señal de Alerta: {8}', GETDATE(), GETDATE(), {9})",
                    "VALUES (1, 1, {0}, {1}, '{2}', 0, 'Señal de alerta No. {3}','{10}','Señal de Alerta: {8}', GETDATE(), GETDATE(), {9})",
                    intIdUsuario, strIdentificacion, strNombreApellido, strCodigoSenal, strTipoCliente, intFrecuencia, intConteoOcurrencias, intValor, strDescripcionSenal,
                    intConteoTblConteoRegistro, strIndicadorROI);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al insertar el registro de operación. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }
        #endregion


        private string mtdOdbcError(OleDbException Ex)
        {
            string strError = string.Empty;

            switch (Ex.ErrorCode)
            {
                case -2147217873:
                    strError = "<br/> La información a borrar tiene relación con otro objeto. <br/> Por favor revise la información.";
                    break;
                default:
                    strError = "Descripción: " + Ex.Message.ToString();
                    break;
            }

            return strError;
        }
    }
}
