using ListasSarlaft.Classes.DAL;
using ListasSarlaft.Classes.DTO.Calidad;
using System;
using System.Collections.Generic;

namespace ListasSarlaft.Classes.BLL
{
    public class GestionAcmBLL : IDisposable
    {
        public void Dispose()
        {

        }

        public int InsertOrigenNoConformidad(OrigenNoConformidad noConformidad)
        {
            try
            {
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.InsertOrigenNoConformidad(noConformidad);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenNoConformidad> SelectOrigenNoConformidad()
        {
            try
            {
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SelectOrigenNoConformidad();
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarEstadosAm();
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarEstadosPaActividad();
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.InsertarActualizarAcm(acm);
                }
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    objData.InsertarActualizarActividades(actividades);
                }
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.ActualizarEstadoActividad(actividad);
                }
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    objData.InsertarActualizarResponsablesAcm(responsables);
                }
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    objData.InsertarActualizarGruposTrabajoAcm(gruposTrabajo);
                }
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarAcms();
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarResponsablesAcm(idAcm);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AcmGrupoTrabajo> SeleccionarGruposTrabajoAcm(int idAcm)
        {
            try
            {
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarGruposTrabajoAcm(idAcm);
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarActividades(idAcm);
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarActividadesCierre(idAcm);
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarResponsablesPaActividad(idActividad);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertarSeguimiento(Seguimiento seguimiento)
        {
            try
            {
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.InsertarSeguimiento(seguimiento);
                }
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarSeguimientos(idActividad);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Seguimiento> SeleccionarSeguimientosPorAcm(int idacm)
        {
            try
            {
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarSeguimientosPorAcm(idacm);
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
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarAreaJerarquia(idJerarquia);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SeleccionarActividadesPendientes(int IdAcm)
        {
            try
            {
                using (GestionAcmDAL objData = new GestionAcmDAL())
                {
                    return objData.SeleccionarActividadesPendientes(IdAcm);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}