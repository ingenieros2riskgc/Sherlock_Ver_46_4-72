using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALInsParaImpacto
    {
        public bool Guardar(string nombrearchivo, int length, byte[] archivo, string modulo, ref string strErrMsg, string extension)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty, strDelquery = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars
            try
            {
                strDelquery = "DELETE FROM [Parametrizacion].[Archivos] WHERE Modulo = 'Impacto'";
                cDatabase.mtdConectarSql();
                cDatabase.ejecutarConsulta(strDelquery);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al borrar el archivo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            try
            {
                strConsulta = string.Format("INSERT INTO [Parametrizacion].[Archivos] " +
                    "([Nombre],[length],[archivo],[Modulo],[extension],[IdRegistro]) " +
                    "VALUES ('{0}', {1}, @PdfData, '{2}', '{3}', 0)",
                    nombrearchivo, length, modulo, extension);

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
    }
}