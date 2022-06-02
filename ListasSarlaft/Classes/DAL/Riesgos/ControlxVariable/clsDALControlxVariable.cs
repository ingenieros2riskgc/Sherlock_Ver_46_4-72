using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALControlxVariable
    {
        public bool mtdInsertarControlxVariable(clsDTOControlxVariable objControlxVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Riesgos].[ControlxVariable] ([IdControl],[NombreVariable],[IdCategoria],[NombreCategoria])" +
                    "VALUES({0},'{1}',{2},'{3}') ",
                    objControlxVariable.intIdControl,objControlxVariable.strNombreVariable, objControlxVariable.intIdCategoria, objControlxVariable.strNombreCategoria);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control vs la variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public int mtdLastIdControl(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int LastId = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT MAX(IdControl) as LastId from [Riesgos].[Control]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                LastId = Convert.ToInt32(dtCaracOut.Rows[0]["LastId"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ultimo Id de control. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return LastId;
        }
        public int mtdGetIdControl(ref string strErrMsg, string CodControl)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int IdControl = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdControl] from [Riesgos].[Control] where [CodigoControl] = '{0}'", CodControl);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                IdControl = Convert.ToInt32(dtCaracOut.Rows[0]["IdControl"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ultimo Id de control. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return IdControl;
        }
        public string mtdGetNameControl(ref string strErrMsg, string IdControl)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            string NameControl = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT NombreControl from [Riesgos].[Control] where [IdControl] = '{0}'", IdControl);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                NameControl = dtCaracOut.Rows[0]["NombreControl"].ToString();
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ultimo Id de control. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return NameControl;
        }
        public string mtdGetDesControl(ref string strErrMsg, string IdControl)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            string DesControl = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT DescripcionControl from [Riesgos].[Control] where [IdControl] = '{0}'", IdControl);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                DesControl = dtCaracOut.Rows[0]["DescripcionControl"].ToString();
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ultimo Id de control. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return DesControl;
        }
        public bool mtdConsultarVariablexContol(ref DataTable dtCaracOut, ref string strErrMsg, int CodControl)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [NombreVariable],[IdCategoria],[NombreCategoria]"
                    + " FROM [Riesgos].[ControlxVariable]"
                    + "where IdControl = {0}"
                    , CodControl);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables registradas al control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateCategoria(clsDTOControlxVariable objControlxVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[ControlxVariable] SET [IdCategoria] = {0},[NombreCategoria] = '{1}'" +
                    " WHERE IdControl = {2} and NombreVariable = '{3}'",
                    objControlxVariable.intIdCategoria, objControlxVariable.strNombreCategoria, objControlxVariable.intIdControl, objControlxVariable.strNombreVariable);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control vs la variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateControlCalificacion(int IdCalificacion, int IdControl, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[Control] set [IdCalificacionControl] = {0}" +
                    " WHERE IdControl = {1}",
                    IdCalificacion, IdControl);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la calificacion del control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
    }
}