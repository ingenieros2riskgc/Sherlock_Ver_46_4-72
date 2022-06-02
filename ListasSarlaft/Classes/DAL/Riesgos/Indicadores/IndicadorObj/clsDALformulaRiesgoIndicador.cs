using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALformulaRiesgoIndicador
    {
        public bool mtdInsertarFormulaRiesgoIndicador(clsDTOformulaRiesgoIndicador objFormula, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Riesgos].[RiesgosIndicadoresFormulas] ([IdRiesgoIndicador],[Nominador],[Denominador],[UsuarioCreacion],[FechaCreacion],[booPorcentaje])" +
                    "VALUES({0},'{1}','{2}',{3},GETDATE(),{4}) ", objFormula.intIdRiesgoIndicador, objFormula.strNominador, objFormula.strDenominador, objFormula.intUsuarioCreacion, objFormula.intPorcentaje
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la formula del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarFormulaRiesgosIndicadores(ref DataTable dtCaracOut, ref string strErrMsg, int IdRiesgoIndicador)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdFormula],[IdRiesgoIndicador],[Nominador],[Denominador],[UsuarioCreacion],[FechaCreacion],ISNULL([booPorcentaje],0) AS booPorcentaje"
                    + " FROM [Riesgos].[RiesgosIndicadoresFormulas]"
                    + " WHERE [IdRiesgoIndicador] ={0}", IdRiesgoIndicador
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la formula del indicadores de riesgos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarFormulaRiesgoIndicador(clsDTOformulaRiesgoIndicador objFormula, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadoresFormulas] SET [Nominador] = '{0}',[Denominador] ='{1}'" +
                    "WHERE IdFormula = {2} ", objFormula.strNominador, objFormula.strDenominador, objFormula.intIdFormula
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la formula del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public int mtdGetLastId(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int LastId = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT max([IdFormula]) LastId FROM [Riesgos].[RiesgosIndicadoresFormulas]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                LastId = Convert.ToInt32(dtCaracOut.Rows[0]["LastId"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ultimo id registrado. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return LastId;
        }
        public bool mtdInsertarDetalleFormulaRiesgoIndicador(int IdFormula, string Variable, string Tipo, int Secuencia, int IdOperando, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Riesgos].[RiesgosIndicadoresDetalleFormulas] ([IdFormula],[Variable],[Tipo],[Secuencia],IdOperando)" +
                    "VALUES ({0},'{1}','{2}',{3},{4})", IdFormula, Variable, Tipo, Secuencia, IdOperando
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al ingresar el detalle de la formula. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarDetalleFormula(ref DataTable dtCaracOut, ref string strErrMsg, int IdFormula)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdDetalleformula],[IdFormula] ,[Variable],[Tipo] ,[Secuencia], IdOperando"
                    + " FROM [Riesgos].[RiesgosIndicadoresDetalleFormulas]"
                    + " WHERE [IdFormula] ={0}", IdFormula
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el detalle de la formula. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarDetalleFormulaRiesgoIndicador(int IdFormula, string Variable, string Tipo, int Secuencia, int IdOperando, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Riesgos].[RiesgosIndicadoresDetalleFormulas] WHERE IdFormula = {0}", IdFormula);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();

                strConsulta = string.Format("INSERT INTO [Riesgos].[RiesgosIndicadoresDetalleFormulas] ([IdFormula],[Variable],[Tipo],[Secuencia],IdOperando)" +
                    "VALUES ({0},'{1}','{2}',{3},{4})", IdFormula, Variable, Tipo, Secuencia, IdOperando
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al ingresar el detalle de la formula. [{0}]", ex.Message);
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