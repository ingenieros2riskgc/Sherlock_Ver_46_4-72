using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALriesgoIndicador
    {
        public bool mtdInsertarRiesgoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Riesgos].[RiesgosIndicadores] ([NombreIndicador],[ObjetivoIndicador],[IdResponsableMedicion],[IdFrecuenciaMedicion],[UsuarioCreacion],[FechaCreacion],[Activo])" +
                    "VALUES('{0}','{1}',{2},{3},{4},GETDATE(),1) ", objIndicador.strNombreIndicador, objIndicador.strObjetivoIndicador, objIndicador.intIdResponsableMedicion,objIndicador.intIdFrecuenciaMedicion, objIndicador.intUsuarioCreacion
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el indicador del riesgo. [{0}]", ex.Message);
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
                strConsulta = string.Format("SELECT max([IdRiesgoIndicador]) as LastId FROM [Riesgos].[RiesgosIndicadores]");

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
        public bool mtdActualizaProcesoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadores] SET [IdProcesoIndicador] = {0} WHERE IdRiesgoIndicador = {1}"
                    , objIndicador.intIProcesoIndicador,objIndicador.intIdRiesgoIndicador
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el indicador del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarRiesgosIndicadores(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdProcesoIndicador]"
                    + ",[IdProceso],[NombreProceso],[IdResponsableMedicion],[NombreHijo],[IdFrecuenciaMedicion],[FrecuenciaMedicion],[IdRiesgoAsociado],"
                    + "[Codigo],[Nombre],[IdFormula],[Nominador],[Denominador],[IdMeta],[Meta],[IdEsquemaSeguimiento],[ValorMinimo]"
                    + ",[ValorMaximo],[DescripcionSeguimiento],[Usuario],[FechaCreacion],[Activo],porcentaje"
                    + " FROM [Riesgos].[vwRiesgosIndicadoresCreate]"
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los indicadores de riesgos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarRiesgosIndicadores(ref DataTable dtCaracOut, ref string strErrMsg, string CodRiesgo, int IdProceso, int Responsable, int IdFactorRiesgo)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            string condicion = string.Empty;
            #endregion Vars

            try
            {
                if(!string.IsNullOrEmpty(CodRiesgo))
                {
                    condicion = string.Format(" and ( Codigo = '{0}')",CodRiesgo);
                }
                if(IdProceso != 0)
                {
                    if(string.IsNullOrEmpty(condicion))
                    {
                        condicion = string.Format(" and ( IdProceso = {0})", IdProceso);
                    }
                    else
                    {
                        condicion += string.Format(" AND (IdProceso = {0})", IdProceso);
                    }
                }
                if(Responsable != 0)
                {
                    if(string.IsNullOrEmpty(condicion))
                    {
                        condicion = string.Format(" and (IdResponsableMedicion = {0})", Responsable);
                    }else
                    {
                        condicion += string.Format(" AND (IdResponsableMedicion = {0})",Responsable);
                    }
                }
                if(IdFactorRiesgo != 0)
                {
                    if(string.IsNullOrEmpty(condicion))
                    {
                        condicion = string.Format(" and (IdClasificacionRiesgo = {0})", IdFactorRiesgo);
                    }
                    else
                    {
                        condicion += string.Format(" AND (IdClasificacionRiesgo = {0})", IdFactorRiesgo);
                    }
                }
                strConsulta = string.Format("SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdProcesoIndicador]"
                    + ",[IdProceso],[NombreProceso],[IdResponsableMedicion],[NombreHijo],[IdFrecuenciaMedicion],[FrecuenciaMedicion],[Descripcion],[IdRiesgoAsociado],"
                    + "[Codigo],[Nombre],[IdFormula],[Nominador],[Denominador],[IdMeta],[Meta],[IdEsquemaSeguimiento],[ValorMinimo]"
                    + ",[ValorMaximo],[DescripcionSeguimiento],[Usuario],[FechaCreacion],[Activo],[IdClasificacionRiesgo], Año, mes, porcentaje"
                    + " FROM [Riesgos].[vwRiesgosIndicadores] where Activo = 1 {0}  ", condicion
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los indicadores de riesgos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizaRiesgoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadores] SET [NombreIndicador] ='{0}',[ObjetivoIndicador] ='{1}'"
                    + ",[IdResponsableMedicion] ={2},[IdFrecuenciaMedicion] ={3}"
                    + " WHERE IdRiesgoIndicador = {4}"
                    , objIndicador.strNombreIndicador,objIndicador.strObjetivoIndicador,objIndicador.intIdResponsableMedicion, objIndicador.intIdFrecuenciaMedicion,
                    objIndicador.intIdRiesgoIndicador
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el indicador del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdAsociarRiesgoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadores] SET [IdRiesgoAsociado] = {0} WHERE IdRiesgoIndicador = {1}"
                    , objIndicador.intIdRiesgoAsociado, objIndicador.intIdRiesgoIndicador
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al asociar el riesgo al indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarRiesgosAsociado(ref DataTable dtCaracOut, ref string strErrMsg, int IdRiesgoIndicador)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT RRI.IdRiesgoIndicador,RRI.IdRiesgoAsociado"
                    + ",RR.Codigo, RR.Nombre"
                    + " FROM [Riesgos].[RiesgosIndicadores] as RRI"
                    + " INNER JOIN [Riesgos].Riesgo as RR on RR.IdRiesgo = RRI.IdRiesgoAsociado"
                    + " WHERE RRI.IdRiesgoIndicador = {0}"
                    , IdRiesgoIndicador
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los indicadores de riesgos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdDesasociarRiesgoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadores] SET [IdRiesgoAsociado] = NULL WHERE IdRiesgoIndicador = {0}"
                    , objIndicador.intIdRiesgoIndicador
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al desasociar el riesgo al indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizaEstadoRiesgoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadores] SET [Activo] ='{0}'"
                    + " WHERE IdRiesgoIndicador = {1}"
                    , objIndicador.booActivo,  objIndicador.intIdRiesgoIndicador
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el indicador del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizaMetaRiesgoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadores] SET [IdMeta] ='{0}'"
                    + " WHERE IdRiesgoIndicador = {1}"
                    , objIndicador.intIdMeta, objIndicador.intIdRiesgoIndicador
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la meta del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizaFormulaRiesgoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadores] SET [IdFormula] ='{0}'"
                    + " WHERE IdRiesgoIndicador = {1}"
                    , objIndicador.intIdFormula, objIndicador.intIdRiesgoIndicador
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
        public bool mtdActualizaSeguimientoRiesgoIndicador(clsDTOriesgosIndicadores objIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Riesgos].[RiesgosIndicadores] SET [IdEsquemaSeguimiento] ='{0}'"
                    + " WHERE IdRiesgoIndicador = {1}"
                    , objIndicador.intIdEsquemaSeguimiento, objIndicador.intIdRiesgoIndicador
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el seguimiento del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarRiesgosIndicadoresGestion(ref DataTable dtCaracOut, ref string strErrMsg, int IdUsuario)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            string TipoArea = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PJO.TipoArea FROM Parametrizacion.JerarquiaOrganizacional AS PJO"
                    + " INNER JOIN Listas.Usuarios AS Luser on Luser.IdJerarquia = PJO.idHijo"
                    + " WHERE Luser.IdUsuario = {0}", IdUsuario);
                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
                TipoArea = dtCaracOut.Rows[0]["TipoArea"].ToString().Trim();
                if (TipoArea == "R")
                {
                    strConsulta = string.Format("SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdProcesoIndicador]"
                        + ",[IdProceso],[NombreProceso],[IdResponsableMedicion],[NombreHijo],[IdFrecuenciaMedicion],[FrecuenciaMedicion],[IdRiesgoAsociado],"
                        + "[Codigo],[Nombre],[IdFormula],[Nominador],[Denominador],[IdMeta],[Meta],[IdEsquemaSeguimiento],[ValorMinimo]"
                        + ",[ValorMaximo],[DescripcionSeguimiento],[Usuario],[FechaCreacion],[Activo],[IdUsuario]"
                        + " FROM [Riesgos].[vwRiesgosIndicadoresCreate] where Activo = 1"
                        );
                }else
                {
                    strConsulta = string.Format("SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdProcesoIndicador]"
                                            + ",[IdProceso],[NombreProceso],[IdResponsableMedicion],[NombreHijo],[IdFrecuenciaMedicion],[FrecuenciaMedicion],[IdRiesgoAsociado],"
                                            + "[Codigo],[Nombre],[IdFormula],[Nominador],[Denominador],[IdMeta],[Meta],[IdEsquemaSeguimiento],[ValorMinimo]"
                                            + ",[ValorMaximo],[DescripcionSeguimiento],[Usuario],[FechaCreacion],[Activo],[IdUsuario]"
                                            + " FROM [Riesgos].[vwRiesgosIndicadoresCreate] where IdUsuario = {0} and Activo = 1", IdUsuario
                                            );
                }
                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los indicadores de riesgos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}