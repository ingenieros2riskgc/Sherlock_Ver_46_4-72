using ListasSarlaft.Classes.DTO.Calidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ListasSarlaft.Classes.DAL
{
    public class GestionAcmDAL : IDisposable
    {
        private cDataBase cDataBase = new cDataBase();

        public void Dispose()
        {

        }

        public int InsertOrigenNoConformidad(OrigenNoConformidad noConformidad)
        {
            try
            {
                // Se crea la lista de parametros para insertar la no conformidad

                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdOrigenNoConformidad", SqlDbType = SqlDbType.Int, Value =  noConformidad.IdOrigenNoConformidad },
                    new SqlParameter() { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value =  noConformidad.Nombre },
                    new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value =  noConformidad.IdUsuario },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                int scope = cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[InsertarActualizarOrigenNoConformidad]", parametros);
                return scope;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<OrigenNoConformidad> SelectOrigenNoConformidad()
        {
            try
            {
                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarOrigenNoConformidad]", new List<SqlParameter>()))
                {
                    List<OrigenNoConformidad> lst = new List<OrigenNoConformidad>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new OrigenNoConformidad()
                            {
                                IdOrigenNoConformidad = Convert.ToInt32(Row["IdOrigenNoConformidad"].ToString()),
                                Nombre = Row["Nombre"].ToString(),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                IdUsuario = Convert.ToInt32(Row["Usuario"].ToString()),
                                FechaModificacion = Row["FechaModificacion"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaModificacion"].ToString()),
                                UsuarioModifica = Row["UsuarioModifica"].Equals(DBNull.Value) ? new int?() : Convert.ToInt32(Row["UsuarioModifica"].ToString()),
                                NombreUsuario = Row["NombreUsuario"].ToString()
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EstadosAcm> SeleccionarEstadosAm()
        {
            try
            {
                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarEstadosAcm]", new List<SqlParameter>()))
                {
                    List<EstadosAcm> lst = new List<EstadosAcm>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new EstadosAcm()
                            {
                                IdEstadoAcm = Convert.ToInt32(Row["IdEstadoAcm"].ToString()),
                                Nombre = Row["Nombre"].ToString()
                            });
                        }
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoActividadPlanAccion> SeleccionarEstadosPaActividad()
        {
            try
            {
                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarEstadosPaActividad]", new List<SqlParameter>()))
                {
                    List<EstadoActividadPlanAccion> lst = new List<EstadoActividadPlanAccion>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new EstadoActividadPlanAccion()
                            {
                                IdEstadoActividadPlanAccion = Convert.ToInt32(Row["IdEstadoActividadPlanAccion"].ToString()),
                                Nombre = Row["Nombre"].ToString()
                            });
                        }
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertarActualizarAcm(GestionAcm acm)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  acm.IdAcm },
                    new SqlParameter() { ParameterName = "@Proceso", SqlDbType = SqlDbType.Int, Value =  acm.Proceso },
                    new SqlParameter() { ParameterName = "@MacroProceso", SqlDbType = SqlDbType.Int, Value =  acm.MacroProceso },
                    new SqlParameter() { ParameterName = "@Cadenavalor", SqlDbType = SqlDbType.Int, Value =  acm.CadenaValor },
                    new SqlParameter() { ParameterName = "@IdSubproceso", SqlDbType = SqlDbType.Int, Value =  acm.Subproceso },
                    new SqlParameter() { ParameterName = "@DescripcionNoConformidad", SqlDbType = SqlDbType.VarChar, Value =  acm.DescripcionNoConformidad },
                    new SqlParameter() { ParameterName = "@OrigenNoConformidad", SqlDbType = SqlDbType.Int, Value =  acm.OrigenNoConformidad },
                    new SqlParameter() { ParameterName = "@CausasRaiz", SqlDbType = SqlDbType.VarChar, Value =  acm.CausasRaiz },
                    new SqlParameter() { ParameterName = "@Codigo", SqlDbType = SqlDbType.VarChar, Value =  acm.Codigo },
                    new SqlParameter() { ParameterName = "@AnalisisCausa", SqlDbType = SqlDbType.VarBinary, Value =  (byte[])acm.AnalisisCausa },
                    new SqlParameter() { ParameterName = "@Estado", SqlDbType = SqlDbType.Int, Value =  acm.Estado },
                    new SqlParameter() { ParameterName = "@VerificacionEficacia", SqlDbType = SqlDbType.VarChar, Value =  acm.VerificacionEficacia },
                    new SqlParameter() { ParameterName = "@Observaciones", SqlDbType = SqlDbType.VarChar, Value =  acm.Observaciones },
                    new SqlParameter() { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value =  acm.UsuarioRegistra },
                    new SqlParameter() { ParameterName = "@UsuarioRevisa", SqlDbType = SqlDbType.Int, Value =  acm.UsuarioRevisa },
                    new SqlParameter() { ParameterName = "@UsuarioAprueba", SqlDbType = SqlDbType.Int, Value =  acm.UsuarioAprueba },
                    new SqlParameter() { ParameterName = "@UsuarioModifica", SqlDbType = SqlDbType.Int, Value =  acm.UsuarioModifica },
                    new SqlParameter() { ParameterName = "@NombreArchivo", SqlDbType = SqlDbType.VarChar, Value =  acm.NombreArchivo },
                    new SqlParameter() { ParameterName = "@Extension", SqlDbType = SqlDbType.VarChar, Value =  acm.Extension },
                    //Adicionamos grupo trabajo descripción
                    new SqlParameter() { ParameterName = "@GrupoTrabajo", SqlDbType = SqlDbType.VarChar, Value=acm.GrupoTrabajo },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output },
                    
                };
                return cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[InsertarActualizarAcm]", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarActualizarResponsablesAcm(List<AcmResponsable> responsables)
        {
            try
            {
                responsables.ForEach(
                    resp =>
                    {
                        List<SqlParameter> parametros = new List<SqlParameter>()
                        {
                            new SqlParameter() { ParameterName = "@IdAcmResponsable", SqlDbType = SqlDbType.Int, Value =  resp.IdAcmResponsable },
                            new SqlParameter() { ParameterName = "@IdResponsable", SqlDbType = SqlDbType.Int, Value =  resp.IdResponsable },
                            new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  resp.IdAcm },
                            new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  resp.Usuario },
                            new SqlParameter() { ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = resp.Flag },
                            new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                        };
                        cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[InsertarActualizarAcmResponsable]", parametros);
                    }
                  );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarActualizarGruposTrabajoAcm(List<AcmGrupoTrabajo> gruposTrabajo)
        {
            try
            {
                gruposTrabajo.ForEach(
                    grupo =>
                    {
                        List<SqlParameter> parametros = new List<SqlParameter>()
                        {
                            new SqlParameter() { ParameterName = "@IdAcmGrupoTrabajo", SqlDbType = SqlDbType.Int, Value =  grupo.IdAcmGrupoTrabajo },
                            new SqlParameter() { ParameterName = "@IdGrupoTrabajo", SqlDbType = SqlDbType.Int, Value =  grupo.IdGrupoTrabajo },
                            new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  grupo.IdAcm },
                            new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  grupo.Usuario },
                            new SqlParameter() { ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = grupo.Flag },
                            new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                        };
                        cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[InsertarActualizarAcmGrupoTrabajo]", parametros);
                    }
                  );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarActualizarActividades(List<PlanAccionActividad> actividades)
        {
            try
            {
                actividades.ForEach(
                    actividad =>
                    {
                        List<SqlParameter> parametros = new List<SqlParameter>()
                        {
                            new SqlParameter() { ParameterName = "@IdActividad", SqlDbType = SqlDbType.Int, Value =  actividad.IdActividad },
                            new SqlParameter() { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value =  actividad.Nombre },
                            new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  actividad.IdAcm },
                            new SqlParameter() { ParameterName = "@FechaInicio", SqlDbType = SqlDbType.DateTime, Value =  actividad.FechaInicio },
                            new SqlParameter() { ParameterName = "@FechaFin", SqlDbType = SqlDbType.DateTime, Value =  actividad.FechaFin },
                            new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  actividad.Usuario },
                            new SqlParameter() { ParameterName = "@UsuarioModifica", SqlDbType = SqlDbType.Int, Value =  actividad.UsuarioModifica },
                            new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                        };
                        int idActividad = cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[InsertarActualizarPlanAccionActividad]", parametros);
                        if (idActividad > 0)
                        {
                            int contador = 0;

                            // Registra los responsables de cada actividad
                            actividad.Responsables.Substring(0, actividad.Responsables.Length).Split(',').ToList().ForEach(
                            str =>
                            {
                                List<SqlParameter> parametros1 = new List<SqlParameter>()
                                    {
                                        new SqlParameter() { ParameterName = "@IdPlanAccionActividadResponsable", SqlDbType = SqlDbType.Int, Value =  actividad.IdPlanAccionActividadResponsable },
                                        new SqlParameter() { ParameterName = "@IdPlanAccionActividad", SqlDbType = SqlDbType.VarChar, Value =  idActividad },
                                        new SqlParameter() { ParameterName = "@IdResponsable", SqlDbType = SqlDbType.Int, Value =  Convert.ToInt32(str) },
                                        new SqlParameter() { ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = contador},
                                        new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  actividad.Usuario },
                                        new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                                    };
                                cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[InsertarActualizarPaActividadResponsable]", parametros1);
                                contador++;
                            }
                        );
                        }
                    }
                  );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ActualizarEstadoActividad(PlanAccionActividad actividad)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdActividad", SqlDbType = SqlDbType.Int, Value =  actividad.IdActividad },
                    new SqlParameter() { ParameterName = "@Estado", SqlDbType = SqlDbType.Int, Value =  actividad.Estado },
                    new SqlParameter() { ParameterName = "@Observaciones", SqlDbType = SqlDbType.VarChar, Value =  actividad.Observaciones },
                    new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                return cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[ActualizarEstadoActividad]", parametros);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<GestionAcm> SeleccionarAcms()
        {
            try
            {
                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarAcm]", new List<SqlParameter>()))
                {
                    List<GestionAcm> lst = new List<GestionAcm>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new GestionAcm()
                            {
                                IdAcm = Convert.ToInt32(Row["IdAcm"].ToString()),
                                Proceso = Convert.ToInt32(Row["IdProceso"].ToString()),
                                AnalisisCausa = Row["AnalisisCausa"].Equals(DBNull.Value) ? null : (byte[])Row["AnalisisCausa"],
                                NombreArchivo = Row["NombreArchivo"].ToString(),
                                Extension = Row["Extension"].ToString(),
                                MacroProceso = Convert.ToInt32(Row["IdMacroProceso"].ToString()),
                                CadenaValor = Convert.ToInt32(Row["IdCadenaValor"].ToString()),
                                Subproceso = Row["IdSubproceso"].Equals(DBNull.Value) ? new int?() : Convert.ToInt32(Row["IdSubproceso"].ToString()),
                                DescripcionNoConformidad = Row["DescripcionNoConformidad"].ToString(),
                                OrigenNoConformidad = Convert.ToInt32(Row["OrigenNoConformidad"].ToString()),
                                CausasRaiz = Row["CausasRaiz"].ToString(),
                                Codigo = Row["Codigo"].ToString().ToUpper(),
                                Estado = Convert.ToInt32(Row["Estado"].ToString()),
                                NombreEstado = Row["NombreEstado"].ToString(),
                                VerificacionEficacia = Row["VerificacionEficacia"].ToString(),
                                Observaciones = Row["Observaciones"].ToString(),
                                UsuarioRegistra = Convert.ToInt32(Row["UsuarioRegistra"].ToString()),
                                UsuarioRevisa = Row["UsuarioRevisa"].Equals(DBNull.Value) ? new int?() : Convert.ToInt32(Row["UsuarioRevisa"].ToString()),
                                UsuarioAprueba = Row["UsuarioAprueba"].Equals(DBNull.Value) ? new int?() : Convert.ToInt32(Row["UsuarioAprueba"].ToString()),
                                UsuarioModifica = Row["UsuarioModifica"].Equals(DBNull.Value) ? new int?() : Convert.ToInt32(Row["UsuarioModifica"].ToString()),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                FechaCierre = Row["FechaCierre"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaCierre"].ToString()),
                                FechaModificacion = Row["FechaModificacion"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaModificacion"].ToString()),
                                NombreProceso = Row["NombreProceso"].ToString(),
                                NombreOrigenNoConformidad = Row["NombreOrigenNoConformidad"].ToString(),
                                NombreEstadoAcm = Row["NombreEstadoAcm"].ToString(),
                                NombreUsuarioRegistra = Row["NombreUsuarioRegistra"].ToString(),
                                NombreUsuarioRevisa = Row["NombreUsuarioRevisa"].ToString(),
                                NombreUsuarioAprueba = Row["NombreUsuarioAprueba"].ToString(),
                                NombreUsuarioModifica = Row["NombreUsuarioModifica"].ToString(),
                                GrupoTrabajo = Row["GrupoTrabajo"].ToString(),
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AcmResponsable> SeleccionarResponsablesAcm(int idAcm)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                     new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  idAcm }
                };

                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarResponsablesAcm]", parametros))
                {
                    List<AcmResponsable> lst = new List<AcmResponsable>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new AcmResponsable()
                            {
                                IdAcmResponsable = Convert.ToInt32(Row["IdAcmResponsable"].ToString()),
                                IdResponsable = Convert.ToInt32(Row["IdResponsable"].ToString()),
                                IdAcm = Convert.ToInt32(Row["IdAcm"].ToString()),
                                Nombre = Row["Nombre"].ToString(),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                Usuario = Convert.ToInt32(Row["Usuario"].ToString()),
                                FechaModificacion = Row["FechaModificacion"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaModificacion"].ToString()),
                                UsuarioModifica = Row["UsuarioModifica"].Equals(DBNull.Value) ? new int?() : Convert.ToInt32(Row["UsuarioModifica"].ToString()),
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AcmGrupoTrabajo> SeleccionarGruposTrabajoAcm(int idAcm)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                     new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  idAcm }
                };

                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarGruposTrabajoAcm]", parametros))
                {
                    List<AcmGrupoTrabajo> lst = new List<AcmGrupoTrabajo>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new AcmGrupoTrabajo()
                            {
                                IdAcmGrupoTrabajo = Convert.ToInt32(Row["IdAcmGrupoTrabajo"].ToString()),
                                IdGrupoTrabajo = Convert.ToInt32(Row["IdGrupoTrabajo"].ToString()),
                                IdAcm = Convert.ToInt32(Row["IdAcm"].ToString()),
                                Nombre = Row["Nombre"].ToString(),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                Usuario = Convert.ToInt32(Row["Usuario"].ToString()),
                                FechaModificacion = Row["FechaModificacion"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaModificacion"].ToString()),
                                UsuarioModifica = Row["UsuarioModifica"].Equals(DBNull.Value) ? new int?() : Convert.ToInt32(Row["UsuarioModifica"].ToString()),
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PlanAccionActividad> SeleccionarActividades(int idAcm)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                     new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  idAcm }
                };

                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarActividadesAcm]", parametros))
                {
                    List<PlanAccionActividad> lst = new List<PlanAccionActividad>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new PlanAccionActividad()
                            {
                                IdActividad = Convert.ToInt32(Row["IdActividad"].ToString()),
                                Estado = Convert.ToInt32(Row["Estado"].ToString()),
                                NombreEstado = Row["NombreEstado"].ToString(),
                                IdAcm = Convert.ToInt32(Row["IdAcm"].ToString()),
                                Nombre = Row["Nombre"].ToString(),
                                Observaciones = Row["Observaciones"].ToString(),
                                FechaInicio = Convert.ToDateTime(Row["FechaInicio"].ToString()),
                                FechaFin = Convert.ToDateTime(Row["FechaFin"].ToString()),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                Usuario = Convert.ToInt32(Row["Usuario"].ToString()),
                                FechaModificacion = Row["FechaModificacion"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaModificacion"].ToString()),
                                FechaCierre = Row["FechaCierre"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaCierre"].ToString())
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PlanAccionActividad> SeleccionarActividadesCierre(int idAcm)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                     new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  idAcm }
                };

                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarActividadesAcm]", parametros))
                {
                    List<PlanAccionActividad> lst = new List<PlanAccionActividad>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new PlanAccionActividad()
                            {
                                IdActividad = Convert.ToInt32(Row["IdActividad"].ToString()),
                                Estado = Convert.ToInt32(Row["Estado"].ToString()),
                                NombreEstado = Row["NombreEstado"].ToString(),
                                IdAcm = Convert.ToInt32(Row["IdAcm"].ToString()),
                                Nombre = Row["Nombre"].ToString(),
                                Observaciones = Row["Observaciones"].ToString(),
                                FechaInicio = Convert.ToDateTime(Row["FechaInicio"].ToString()),
                                FechaFin = Convert.ToDateTime(Row["FechaFin"].ToString()),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                Usuario = Convert.ToInt32(Row["Usuario"].ToString()),
                                FechaModificacion = Row["FechaModificacion"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaModificacion"].ToString()),
                                FechaCierre = Row["FechaCierre"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaCierre"].ToString()),
                                ActividadCierre = Row["ActividadCierre"].ToString()
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PaActividadResponsable> SeleccionarResponsablesPaActividad(int idActividad)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                     new SqlParameter() { ParameterName = "@IdActividad", SqlDbType = SqlDbType.Int, Value =  idActividad }
                };

                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarResponsablesPaActividad]", parametros))
                {
                    List<PaActividadResponsable> lst = new List<PaActividadResponsable>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new PaActividadResponsable()
                            {
                                IdPlanAccionActividadResponsable = Convert.ToInt32(Row["IdPlanAccionActividadResponsable"].ToString()),
                                IdPlanAccionActividad = Convert.ToInt32(Row["IdPlanAccionActividad"].ToString()),
                                IdResponsable = Convert.ToInt32(Row["IdResponsable"].ToString()),
                                Nombre = Row["Nombre"].ToString(),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                Usuario = Convert.ToInt32(Row["Usuario"].ToString()),
                                FechaModificacion = Row["FechaModificacion"].Equals(DBNull.Value) ? new DateTime?() : Convert.ToDateTime(Row["FechaModificacion"].ToString()),
                                UsuarioModifica = Row["UsuarioModifica"].Equals(DBNull.Value) ? new int?() : Convert.ToInt32(Row["UsuarioModifica"].ToString())
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertarSeguimiento( Seguimiento seguimiento)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                 {
                    new SqlParameter() { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value =  seguimiento.Descripcion },
                    new SqlParameter() { ParameterName = "@IdPlanAccionActividad", SqlDbType = SqlDbType.Int, Value =  seguimiento.IdPlanAccionActividad },
                    new SqlParameter() { ParameterName = "@Usuario", SqlDbType = SqlDbType.Int, Value =  seguimiento.Usuario },
                     new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                 };
                return cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[InsertarSeguimiento]", parametros);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Seguimiento> SeleccionarSeguimientos(int idActividad)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                     new SqlParameter() { ParameterName = "@IdPlanAccionActividad", SqlDbType = SqlDbType.Int, Value =  idActividad }
                };

                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarSeguimiento]", parametros))
                {
                    List<Seguimiento> lst = new List<Seguimiento>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new Seguimiento()
                            {
                                IdSeguimientoPlanAccionActividad = Convert.ToInt32(Row["IdSeguimientoPlanAccionActividad"].ToString()),
                                Descripcion = Row["Descripcion"].ToString(),
                                IdPlanAccionActividad = Convert.ToInt32(Row["IdPlanAccionActividad"].ToString()),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                Usuario = Convert.ToInt32(Row["Usuario"].ToString()),
                                NombreUsuario = Row["NombreUsuario"].ToString()
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Seguimiento> SeleccionarSeguimientosPorAcm(int idacm)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                     new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.Int, Value =  idacm }
                };

                using (DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarSeguimientoIdAcm]", parametros))
                {
                    List<Seguimiento> lst = new List<Seguimiento>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            lst.Add(new Seguimiento()
                            {
                                IdActividad = Row["IdActividad"].ToString(),
                                NombreActividad = Row["NombreActividad"].ToString(),
                                Descripcion = Row["Descripcion"].ToString(),
                                FechaRegistro = Convert.ToDateTime(Row["FechaRegistro"].ToString()),
                                NombreUsuario = Row["NombreUsuario"].ToString()
                            });
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SeleccionarAreaJerarquia(int idJerarquia)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                 {
                    new SqlParameter() { ParameterName = "@IdJerarquia", SqlDbType = SqlDbType.VarChar, Value =  idJerarquia },
                     new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                 };
                return cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[SeleccionarAreaJerarquia]", parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int SeleccionarActividadesPendientes(int IdAcm)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                 {
                    new SqlParameter() { ParameterName = "@IdAcm", SqlDbType = SqlDbType.VarChar, Value =  IdAcm },
                     new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                 };
                return cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[SeleccionarPaActividadesPendientes]", parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}