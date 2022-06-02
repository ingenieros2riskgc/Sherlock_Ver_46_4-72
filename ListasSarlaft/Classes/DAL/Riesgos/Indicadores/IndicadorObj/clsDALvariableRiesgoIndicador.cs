using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALvariableRiesgoIndicador
    {
        public bool mtdInsertarVariableRiesgoIndicador(clsDTOvariableRiesgoIndicador objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Riesgos].[RiesgosIndicadoresVariables] ([IdRiesgoindicador],[ValorVariable],[UsuarioCreacion],[FechaCreacion],[IdFormato],[Descripcion])" +
                    "VALUES({0},{1},{2},GETDATE(),{4},'{5}') ", objVariable.intIdRiesgoIndicador,objVariable.dblValorVariable,objVariable.intUsuarioCreacion,objVariable.dtFechaCreacion,objVariable.intIdFormato
                    ,objVariable.strDescripcion
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la variable del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarVariableRiesgoIndicador(ref DataTable dtCaracOut, ref string strErrMsg, int IdRiesgoIndicador)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT RRIV.[IdRiesgoIndicadorVariable],RRIV.Descripcion,RRIV.[IdRiesgoindicador],RRIV.[ValorVariable],RRIV.[IdFormato], GDT.NombreDetalle"
                    + " FROM [Riesgos].[RiesgosIndicadoresVariables] as RRIV"
                    + " INNER JOIN [Gestion].[DetalleTipos] as GDT on GDT.IdDetalleTipo = RRIV.IdFormato"
                    + " where RRIV.IdRiesgoindicador ={0}", IdRiesgoIndicador
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarVariableRiesgoIndicador(clsDTOvariableRiesgoIndicador objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadoresVariables] SET [ValorVariable] = {0},[IdFormato] = {1},[Descripcion] = '{2}'" +
                    " WHERE IdRiesgoIndicadorVariable = {3}", objVariable.dblValorVariable, objVariable.intIdFormato, objVariable.strDescripcion
                    ,objVariable.intIdVariableRiesgoIndicador
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la variable del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdEliminaVariable(string IdVariable, ref string strErrMsg, string IdRiesgoIndicador)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Riesgos].[RiesgosIndicadoresVariables]" +
                    " WHERE IdRiesgoIndicadorVariable = {0}", IdVariable
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);

                strConsulta = string.Format("DELETE FROM [Riesgos].[RiesgosIndicadoresFormulas]" +
                    " WHERE IdRiesgoIndicador = {0}", IdRiesgoIndicador
                    );
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al eliminar la variable del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public double mtdConsultarValorVariable(ref string strErrMsg, string NombreVariable, int IdRiesgoIndicador, string ValorFrecuencia, int IdDetalleFrecuencia,
            string año, string mes)
        {
            #region Vars
            //bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            double valorVariable = 0;
            #endregion Vars

            try
            {
                if (IdDetalleFrecuencia == 0)
                {
                    strConsulta = string.Format("SELECT RRIVV.[valorVariable]"
                        + " FROM [Riesgos].[RiesgosIndicadoresValorVariables] as RRIVV"
                        + " INNER JOIN Riesgos.RiesgosIndicadoresVariables as RRIV on RRIV.IdRiesgoIndicadorVariable = RRIVV.IdVariable"
                        + " where RRIVV.IdRiesgoIndicador = {1} and RRIV.Descripcion = '{0}' and RRIVV.valorFrecuencia = '{2}'"
                        + " and Año='{3}' and Mes='{4}'", NombreVariable, IdRiesgoIndicador, ValorFrecuencia, año, mes
                        );
                }else
                {
                    strConsulta = string.Format("SELECT RRIVV.[valorVariable]"
                                            + " FROM [Riesgos].[RiesgosIndicadoresValorVariables] as RRIVV"
                                            + " INNER JOIN Riesgos.RiesgosIndicadoresVariables as RRIV on RRIV.IdRiesgoIndicadorVariable = RRIVV.IdVariable"
                                            + " where RRIVV.IdRiesgoIndicador = {1} and RRIV.Descripcion = '{0}' and RRIVV.IdDetalleFrecuencia = {2}"
                                            + " and Año='{3}' and Mes='{4}'", NombreVariable, IdRiesgoIndicador, IdDetalleFrecuencia, año,mes
                                            );
                }
                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                if(dtCaracOut.Rows.Count > 0)
                 valorVariable = Convert.ToDouble(dtCaracOut.Rows[0]["ValorVariable"].ToString());
                //booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables del indicador. [{0}]", ex.Message);
                //booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return valorVariable;
        }
        public bool mtdInsertarValorVariable(clsDTOvalorVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Riesgos].[RiesgosIndicadoresValorVariables] ([IdVariable],[IdFrecuencia],[valorFrecuencia],[valorVariable]"
                    + ",[IdRiesgoIndicador],[IdDetalleFrecuencia],[Año],[Mes])" +
                    "VALUES({0},{1},'{2}',{3},{4},{5},'{6}','{7}') ", objVariable.intIdVariable, objVariable.intIdFrecuencia, objVariable.strValorFrecuencia, objVariable.dblValorVariable
                    , objVariable.intIdRiesgoIndicador, objVariable.intIdDetalleFrecuencia, objVariable.strAño, objVariable.strMes
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la variable del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarValorVariable(ref DataTable dtCaracOut, ref string strErrMsg, int IdRiesgoIndicador, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdValorVariable],[IdVariable],[IdFrecuencia], RRIFM.FrecuenciaMedicion,[valorFrecuencia]"
                    + ",[valorVariable],[IdRiesgoIndicador], RRVV.[IdDetalleFrecuencia], RRIDF.Descripcion,RRVV.Año,RRVV.Mes"
                    + " FROM [Riesgos].[RiesgosIndicadoresValorVariables] as RRVV"
                    + " INNER JOIN [Riesgos].[RiesgosIndicadoresFrecuenciaMedicion] as RRIFM on RRIFM.IdFrecuenciaMedicion = RRVV.IdFrecuencia"
                    + " LEFT JOIN  [Riesgos].[RiesgosIndicadoresDetalleFrecuencia] as RRIDF on RRIDF.IdDetalleFecruencia = RRVV.IdDetalleFrecuencia"
                    + " WHERE IdRiesgoIndicador = {0} and IdVariable = {1}", IdRiesgoIndicador, IdVariable
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarValorVariable(clsDTOvalorVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadoresValorVariables] SET [IdFrecuencia] = {0},[valorFrecuencia] = '{1}',[valorVariable] = {2}"
                    + ",[Año] = '{4}', [Mes] = '{5}' "
                    + "WHERE IdValorVariable = {3} ", objVariable.intIdFrecuencia, objVariable.strValorFrecuencia, objVariable.dblValorVariable, objVariable.intIdValorVariable,
                    objVariable.strAño, objVariable.strMes
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la variable del indicador. [{0}]", ex.Message);
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