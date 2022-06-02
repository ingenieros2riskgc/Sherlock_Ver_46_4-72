using iTextSharp.text;
using iTextSharp.text.pdf;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.BLL;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = iTextSharp.text.Image;

namespace ListasSarlaft.UserControls.Proceso.Acm
{
    public partial class GestionAcm : UserControl
    {
        cParametrizacion Parametrizacion = new cParametrizacion();
        private cCuenta cCuenta = new cCuenta();
        private static int LastInsertIdCE;
        string IdFormulario = "4050";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                Session["AcmCerrado"] = false;
                Session["IdAcm"] = 0;
                Session["IdActividad"] = 0;
                Session["AnalisisCausa"] = null;
                Session["NombreArchivo"] = string.Empty;
                Session["Extension"] = string.Empty;
                CargarGrillaAcm();
                PopulateTreeView();
                PopulateTreeViewParam();
                CargarDDLs();
                SetInitialRow();
            }
        }

        #region Treeviews
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (txtNResponsableEjecucion.Text.Length > 0)
            {
                txtNResponsableEjecucion.Text += $", {TreeView1.SelectedNode.Text}";
                lblIdDependencia1.Text += $",{TreeView1.SelectedNode.Value}";
            }
            else
            {
                txtNResponsableEjecucion.Text += $"{TreeView1.SelectedNode.Text}";
                lblIdDependencia1.Text += $"{TreeView1.SelectedNode.Value}";
            }
        }

        protected void TreeViewTablaParam_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (txtGrupo.Text.Length > 0)
            {
                txtGrupo.Text += $", {TreeViewTablaParam.SelectedNode.Text}";
                lDependeciaGrupo.Text += $",{TreeViewTablaParam.SelectedNode.Value}";
            }
            else
            {
                txtGrupo.Text += $"{TreeViewTablaParam.SelectedNode.Text}";
                lDependeciaGrupo.Text += $"{TreeViewTablaParam.SelectedNode.Value}";
            }
        }

        private void PopulateTreeViews(TreeView treeView)
        {
            DataTable treeViewData = Parametrizacion.GetTreeViewData();
            Parametrizacion.AddTopTreeViewNodes(treeViewData, treeView);
            treeView.ExpandAll();
        }

        private void PopulateTreeView()
        {
            DataTable treeViewData = Parametrizacion.GetTreeViewData();
            Parametrizacion.AddTopTreeViewNodes(treeViewData, TreeView1);
            TreeView1.ExpandAll();
        }

        private void PopulateTreeViewParam()
        {
            DataTable treeViewData = Parametrizacion.GetTreeViewDataWorkGroup();
            Parametrizacion.AddTopTreeViewNodesWorkGroup(treeViewData, TreeViewTablaParam);
            TreeViewTablaParam.ExpandAll();
        }

        #endregion

        #region Metodos

        private enum TipoNotificacion
        {
            Acm = 1,
            Actividad = 2
        }

        private void Reset()
        {
            try
            {
                trNuevoAcm.Visible = false;
                trBotones.Visible = false;
                trGrillaAcm.Visible = true;
                trCierreAcm.Visible = false;
                txtNResponsableEjecucion.Text = string.Empty;
                lblIdDependencia1.Text = string.Empty;
                txtGrupo.Text = string.Empty;
                lDependeciaGrupo.Text = string.Empty;
                ddlCadenaValor.ClearSelection();
                ddlMacroproceso.ClearSelection();
                ddlProceso.ClearSelection();
                ddlSubproceso.ClearSelection();
                txtDescNoConformidad.Text = string.Empty;
                ddlOrigenNoConformidad.ClearSelection();
                ddlEstadoAcm.ClearSelection();
                txtCausasRaiz.Text = string.Empty;
                txtCodigo.Text = string.Empty;
                ddlEstadoAcm.Enabled = true;
                chkAprobado.Checked = false;
                chkRevisado.Checked = false;
                btnGuardar.Visible = true;
                ddlCadenaValor.Enabled = true;
                ddlMacroproceso.Enabled = true;
                ddlProceso.Enabled = true;
                ddlSubproceso.Enabled = true;
                txtDescNoConformidad.Enabled = true;
                ddlOrigenNoConformidad.Enabled = true;
                txtCausasRaiz.Enabled = true;
                txtCodigo.Enabled = true;
                fuAnalisisCausa.Enabled = true;
                btnDescargarArchivo.Visible = false;
                txtVerificacionEficacia.Enabled = true;
                txtVerificacionEficacia.Text = string.Empty;
                txtObservacionesAcm.Enabled = true;
                txtObservacionesAcm.Text = string.Empty;
                ddlEstadoAcm.Enabled = true;
                chkRevisado.Enabled = true;
                chkAprobado.Enabled = true;
                imgDependencia1.Enabled = true;
                btnBorrarResponsables.Enabled = true;
                btnBorrarResponsables.Enabled = true;
                btnBorrarGrupos.Enabled = true;
                ImageButtonTablaParam.Enabled = true;
                txtNResponsableEjecucion.Enabled = true;
                txtGrupo.Enabled = true;
                btnDescargarAcm.Visible = false;
                trFechaCierreAct.Visible = false;
                trFechaCierreAcm.Visible = false;
                txtFechaCreacion.Text = string.Empty;
                // Reinicia variables de sesión utilizadas
                Session["IdAcm"] = 0;
                Session["IdActividad"] = 0;
                Session["AcmCerrado"] = false;
                Session["AnalisisCausa"] = null;
                Session["NombreArchivo"] = string.Empty;
                Session["Extension"] = string.Empty;
                SetInitialRow();
                CargarGrillaAcm();
                gvAdjuntosCierre.DataSource = null;
                gvAdjuntosCierre.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeshabilitarControles()
        {
            ddlCadenaValor.Enabled = false;
            ddlMacroproceso.Enabled = false;
            ddlProceso.Enabled = false;
            ddlSubproceso.Enabled = false;
            txtDescNoConformidad.Enabled = false;
            ddlOrigenNoConformidad.Enabled = false;
            txtCausasRaiz.Enabled = false;
            txtCodigo.Enabled = false;
            fuAnalisisCausa.Enabled = false;
            txtVerificacionEficacia.Enabled = false;
            txtObservacionesAcm.Enabled = false;
            ddlEstadoAcm.Enabled = false;
            btnGuardar.Visible = false;
            chkRevisado.Enabled = false;
            chkAprobado.Enabled = false;
            btnBorrarResponsables.Enabled = false;
            imgDependencia1.Enabled = false;
            btnBorrarGrupos.Enabled = false;
            ImageButtonTablaParam.Enabled = false;
            txtNResponsableEjecucion.Enabled = false;
            txtGrupo.Enabled = false;
            trFechaCierreAcm.Visible = true;
            Session["AcmCerrado"] = true;
            foreach (GridViewRow row in gvActividades.Rows)
            {
                ((ImageButton)row.FindControl("btnConfigurar")).Enabled = false;
            }
        }

        private void CargarDDLs()
        {
            try
            {
                string strErrMsg = string.Empty;
                LoadDDLCadenaValor(ref strErrMsg);
                CargarOrigenNoConformidad();
                CargarEstadosAcm();
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar la información. {ex.Message}", 1, "Atención");
            }
        }

        private bool LoadDDLCadenaValor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsCadenaValorBLL cCadenaValor = new clsCadenaValorBLL();
            #endregion Vars

            try
            {
                lstCadenaValor = cCadenaValor.mtdConsultarCadenaValor(true, ref strErrMsg);

                ddlCadenaValor.Items.Clear();
                ddlCadenaValor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
                throw ex;
            }

            return booResult;
        }

        private bool LoadDDLMacroProceso(int IdCadenaValor)
        {
            #region Vars
            bool booResult = false;
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                DataTable dt = cMacroproceso.ConsultarMacroProcesos(IdCadenaValor);
                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        int intCounter = 1;
                        ddlMacroproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(Row["Nombre"].ToString(), Row["IdMacroProceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                throw ex;
            }

            return booResult;
        }

        private bool LoadDDLProceso(int IdMacroproceso)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {

                DataTable dt = cProceso.ConsultarProcesos(IdMacroproceso);
                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    int intCounter = 1;
                    foreach (DataRow Row in dt.Rows)
                    {
                        ddlProceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(Row["Nombre"].ToString(), Row["IdProceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = true;
                throw ex;
            }

            return booResult;
        }

        private bool LoadDDLSubproceso(int? idProceso)
        {
            #region Vars
            bool booResult = false;
            clsProceso objProceso = new clsProceso();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();
            #endregion Vars

            try
            {
                DataTable dt = cSubproceso.ConsultarSubprocesos(idProceso);

                ddlSubproceso.Items.Clear();
                ddlSubproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    int intCounter = 1;
                    foreach (DataRow Row in dt.Rows)
                    {
                        ddlSubproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(Row["Nombre"].ToString(), Row["IdSubproceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                throw ex;
            }

            return booResult;
        }

        private void CargarOrigenNoConformidad()
        {
            try
            {
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    ddlOrigenNoConformidad.DataSource = objData.SelectOrigenNoConformidad();
                    ddlOrigenNoConformidad.DataTextField = "Nombre";
                    ddlOrigenNoConformidad.DataValueField = "IdOrigenNoConformidad";
                    ddlOrigenNoConformidad.DataBind();
                    ddlOrigenNoConformidad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CargarEstadoActividad(int idActividad)
        {
            try
            {
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    ddlEstadoActividad.DataSource = objData.SeleccionarEstadosPaActividad();
                    ddlEstadoActividad.DataTextField = "Nombre";
                    ddlEstadoActividad.DataValueField = "IdEstadoActividadPlanAccion";
                    ddlEstadoActividad.DataBind();

                    trFechaCierreAct.Visible = false;

                    //Se obtiene la información de la actividad para conocer el estado
                    List<Classes.DTO.Calidad.PlanAccionActividad> lstActividad = objData.SeleccionarActividades(Convert.ToInt32(Session["IdActividad"]));
                    Classes.DTO.Calidad.PlanAccionActividad actividad = lstActividad.Where(x => x.IdActividad == idActividad).FirstOrDefault();
                    ddlEstadoActividad.SelectedValue = actividad.Estado.ToString();

                    // Se llena la observacion
                    txtObservacionesCierreAct.Text = actividad.Observaciones;
                    if (actividad.Estado == 2)
                    {
                        trFechaCierreAct.Visible = true;
                        txtFechaCierreAct.Text = actividad.FechaCierre.ToString();
                    }

                    //Valida si el usuario actual pertenece al área de riesgos
                    int result = objData.SeleccionarAreaJerarquia(Convert.ToInt32(Session["IdJerarquia"].ToString()));
                    if (result == 0)
                    {
                        ddlEstadoActividad.Enabled = false;
                    }

                    // Si la actividad esta cerrada se bloquean los controles para insertar comentarios de lo contrario se habilitan
                    if (ddlEstadoActividad.SelectedValue == "2")
                    {
                        trObservacioneCierreAct.Visible = true;
                        ddlEstadoActividad.Enabled = false;
                        txtComentario.Enabled = false;
                        txtComentario.Text = string.Empty;
                        txtObservacionesCierreAct.Enabled = false;
                        btnInsertarSeguimiento.Visible = false;
                    }
                    else if (ddlEstadoActividad.SelectedValue != "2" && result > 0)
                    {
                        trObservacioneCierreAct.Visible = false;
                        ddlEstadoActividad.Enabled = true;
                        txtComentario.Enabled = true;
                        txtComentario.Text = string.Empty;
                        txtObservacionesCierreAct.Enabled = true;
                        txtObservacionesCierreAct.Text = string.Empty;
                        btnInsertarSeguimiento.Visible = true;
                    }
                    else
                    {
                        trObservacioneCierreAct.Visible = false;
                        ddlEstadoActividad.Enabled = false;
                        txtComentario.Enabled = true;
                        txtComentario.Text = string.Empty;
                        txtObservacionesCierreAct.Enabled = true;
                        txtObservacionesCierreAct.Text = string.Empty;
                        btnInsertarSeguimiento.Visible = true;
                    }
                    ObtenerActividades();
                }
                mtdLoadAdjunto(idActividad);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void mtdLoadAdjunto(int idActividad)
        {
            clsAcmCierreAdjuntosBLL blAdjuntos = new clsAcmCierreAdjuntosBLL();
            List<clsAcmCierreAdjuntos> lstAdjunto = new List<clsAcmCierreAdjuntos>();
            string strErrMsg = string.Empty;
            lstAdjunto = blAdjuntos.mdtConsultarAdjuntosCierre(ref strErrMsg, idActividad);
            if (strErrMsg == string.Empty)
            {
                gvDocsAdjuntados.DataSource = lstAdjunto;
                gvDocsAdjuntados.DataBind();
            }
        }






        private void CargarEstadosAcm()
        {
            try
            {
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    ddlEstadoAcm.DataSource = objData.SeleccionarEstadosAm();
                    ddlEstadoAcm.DataTextField = "Nombre";
                    ddlEstadoAcm.DataValueField = "IdEstadoAcm";
                    ddlEstadoAcm.DataBind();
                    ddlEstadoAcm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerActividades()
        {
            try
            {
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {

                    // Obtiene las actividades y responsables
                    List<Classes.DTO.Calidad.PlanAccionActividad> lstActividad = objData.SeleccionarActividades((int)Session["IdAcm"]);
                    List<Classes.DTO.Calidad.PaActividadResponsable> lstActividadResponsable = new List<Classes.DTO.Calidad.PaActividadResponsable>();
                    if (lstActividad.Count > 0)
                    {
                        Session["PrimerIngreso"] = true;
                        string responsablesActividad = string.Empty;
                        string codigosResponsableActividad = string.Empty;
                        lstActividad.ForEach(actividad =>
                        {
                            lstActividadResponsable = objData.SeleccionarResponsablesPaActividad(actividad.IdActividad);
                            if (lstActividadResponsable.Count > 0)
                            {
                                responsablesActividad = string.Join(", ", lstActividadResponsable.Select(x => x.Nombre));
                                codigosResponsableActividad = string.Join(",", lstActividadResponsable.Select(x => x.IdResponsable));
                            }
                            AddNewRowToGridWithData(actividad, responsablesActividad, codigosResponsableActividad);
                        });
                    }
                    else
                        ddlEstadoAcm.Items.FindByValue("2").Enabled = false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CargarInfoEditarAcm(int idAcm)
        {
            try
            {
                Session["IdAcm"] = idAcm;
                trNuevoAcm.Visible = true;
                trBotones.Visible = true;
                btnDescargarAcm.Visible = true;
                trCierreAcm.Visible = true;
                trGrillaAcm.Visible = false;
                gvActividades.Columns[4].Visible = true;
                CargarEstadosAcm();
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    // Obtiene la primera parte del acm
                    Classes.DTO.Calidad.GestionAcm acm = objData.SeleccionarAcms().Where(x => x.IdAcm == idAcm).FirstOrDefault();
                    if (acm != null)
                    {
                        if (acm.AnalisisCausa != null)
                        {
                            Session["AnalisisCausa"] = acm.AnalisisCausa;
                            Session["NombreArchivo"] = acm.NombreArchivo;
                            Session["Extension"] = acm.Extension;
                            btnDescargarArchivo.Visible = true;
                            RequiredFieldValidator_fuAnalisisCausa.Enabled = false;
                        }
                        txtObservacionesAcm.Text = acm.Observaciones;
                        txtVerificacionEficacia.Text = acm.VerificacionEficacia;
                        txtDescNoConformidad.Text = acm.DescripcionNoConformidad;
                        txtCausasRaiz.Text = acm.CausasRaiz;
                        txtCodigo.Text = acm.Codigo;
                        ddlCadenaValor.SelectedValue = acm.CadenaValor.ToString();
                        LoadDDLMacroProceso(acm.CadenaValor);
                        ddlMacroproceso.SelectedValue = acm.MacroProceso.ToString();
                        LoadDDLProceso(acm.MacroProceso);
                        ddlProceso.SelectedValue = acm.Proceso.ToString();
                        LoadDDLSubproceso(acm.Proceso);
                        if (acm.Subproceso != null)
                            ddlSubproceso.SelectedValue = acm.Subproceso.ToString();
                        ddlOrigenNoConformidad.SelectedValue = acm.OrigenNoConformidad.ToString();
                        ddlEstadoAcm.SelectedValue = acm.Estado.ToString();
                        lblRegistrado.Text = acm.NombreUsuarioRegistra;
                        lblRevisadoPor.Text = acm.NombreUsuarioRevisa;
                        lblCerradoPor.Text = acm.NombreUsuarioAprueba;
                        txtFechaCierreAcm.Text = acm.FechaCierre.ToString();
                        if (acm.UsuarioRevisa != null)
                        {
                            chkRevisado.Checked = true;
                            chkRevisado.Enabled = false;
                        }
                        if (acm.UsuarioAprueba != null)
                        {
                            chkAprobado.Checked = true;
                            chkAprobado.Enabled = false;
                        }

                        // Si el estado es cerrado se deshabilita el control
                        if (acm.Estado == 2)
                        {
                            DeshabilitarControles();
                        }

                        // Validaciones área de riesgos
                        int result = objData.SeleccionarAreaJerarquia(Convert.ToInt32(Session["IdJerarquia"].ToString()));
                        if (result == 0)
                        {
                            chkRevisado.Enabled = false;
                            chkAprobado.Enabled = false;
                            ddlEstadoAcm.Enabled = false;
                        }
                        else
                        {
                            if (acm.UsuarioRevisa == null && acm.Estado != 2)
                                chkRevisado.Enabled = true;
                            if (acm.UsuarioAprueba == null && acm.Estado != 2)
                                chkAprobado.Enabled = true;
                        }
                        //Se cargar los grupos de trabajo descriptivos
                        txtGrupo.Text = acm.GrupoTrabajo;
                        txtFechaCreacion.Text = Convert.ToString(acm.FechaRegistro);

                    }

                    // Obtiene los responsables
                    List<Classes.DTO.Calidad.AcmResponsable> lstAcmResponsable = objData.SeleccionarResponsablesAcm(idAcm);
                    if (lstAcmResponsable.Count > 0)
                    {
                        txtNResponsableEjecucion.Text = string.Join(", ", lstAcmResponsable.Select(x => x.Nombre));
                        lblIdDependencia1.Text = string.Join(",", lstAcmResponsable.Select(x => x.IdResponsable));
                    }

                    // Obtiene los grupos de trabajo
                    List<Classes.DTO.Calidad.AcmGrupoTrabajo> lstAcmGrupoTrabajo = objData.SeleccionarGruposTrabajoAcm(idAcm);
                    if (lstAcmGrupoTrabajo.Count > 0)
                    {
                        txtGrupo.Text = string.Join(", ", lstAcmGrupoTrabajo.Select(x => x.Nombre));
                        lDependeciaGrupo.Text = string.Join(",", lstAcmGrupoTrabajo.Select(x => x.IdGrupoTrabajo));
                    }

                    // Obtiene las actividades y responsables
                    ObtenerActividades();
                }
                //CargarEstadoActividad(idAcm);
                
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar la información del Acm {ex.Message}", 1, "Atención");
            }
        }

        public void mtdLoadAdjuntos(int idAcm)
        {
            clsAcmCierreAdjuntosBLL blAdjuntos = new clsAcmCierreAdjuntosBLL();
            List<clsAcmCierreAdjuntos> lstAdjuntos = new List<clsAcmCierreAdjuntos>();
            string strErrMsg = string.Empty;
            lstAdjuntos = blAdjuntos.mdtConsultarAdjuntosCierre(ref strErrMsg, idAcm);
            if (strErrMsg == string.Empty)
            {
                gvDocsAdjuntados.DataSource = lstAdjuntos;
                gvDocsAdjuntados.DataBind();
            }
        }
        private void CrearPdf()
        {
            try
            {
                //cargar grilla seguimientos
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    gvSeguimientoAcm.DataSource = objData.SeleccionarSeguimientosPorAcm((int)Session["IdAcm"]);
                    gvSeguimientoAcm.DataBind();

                    gvObservacionesCierreAct.DataSource = objData.SeleccionarActividadesCierre((int)Session["IdAcm"]).Where(x => x.Estado == 2);
                    gvObservacionesCierreAct.DataBind();
                }

                // Creamos el tipo de Font que vamos utilizar
                Font titleFont = new Font(Font.HELVETICA, 10, Font.BOLD, Color.BLACK);
                Font textFont = new Font(Font.HELVETICA, 8, Font.NORMAL, Color.BLACK);
                Font subtitleFont = new Font(Font.HELVETICA, 9, Font.BOLD, Color.BLACK);
                Font headertableFont = new Font(Font.HELVETICA, 10, Font.NORMAL, Color.WHITE);
                List<PdfPRow> pRows = new List<PdfPRow>();

                #region Responsables
                // Crea la informacion de la primera parte del Acm

                PdfPTable tableResponsables = new PdfPTable(4);
                tableResponsables.DefaultCell.Border = Rectangle.NO_BORDER;
                List<PdfPCell> rowTable1 = new List<PdfPCell>
                    {
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Responsable Ejecución:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtNResponsableEjecucion.Text), textFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Grupos de trabajo:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtGrupo.Text), textFont)){ Border = 0},
                    };
                pRows.Add(new PdfPRow(rowTable1.ToArray()));
                tableResponsables.Rows.AddRange(pRows);

                pRows.Clear();

                rowTable1 = new List<PdfPCell>
                    {
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Cadena de valor:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(ddlCadenaValor.SelectedItem.Text), textFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("MacroProceso:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(ddlMacroproceso.SelectedItem.Text), textFont)){ Border = 0},
                    };
                pRows.Add(new PdfPRow(rowTable1.ToArray()));
                tableResponsables.Rows.AddRange(pRows);

                pRows.Clear();

                rowTable1 = new List<PdfPCell>
                    {
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Proceso:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(ddlProceso.SelectedItem.Text), textFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Subproceso"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(ddlSubproceso.SelectedItem.Text), textFont)){ Border = 0},
                    };
                pRows.Add(new PdfPRow(rowTable1.ToArray()));
                tableResponsables.Rows.AddRange(pRows);

                pRows.Clear();

                rowTable1 = new List<PdfPCell>
                    {
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Origen no conformidad:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(ddlOrigenNoConformidad.SelectedItem.Text), textFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Código:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtCodigo.Text), textFont)){ Border = 0},
                    };
                pRows.Add(new PdfPRow(rowTable1.ToArray()));
                tableResponsables.Rows.AddRange(pRows);

                pRows.Clear();

                var cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode("Descripción no conformidad:"), subtitleFont)) { Border = 0 };
                tableResponsables.AddCell(cell);
                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtDescNoConformidad.Text), textFont)) { Border = 0, Colspan = 3 };
                tableResponsables.AddCell(cell);

                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode("Causas Raíz:"), subtitleFont)) { Border = 0 };
                tableResponsables.AddCell(cell);
                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtCausasRaiz.Text), textFont)) { Border = 0, Colspan = 3 };
                tableResponsables.AddCell(cell);

                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode("Fecha Creación:"), subtitleFont)) { Border = 0 };
                tableResponsables.AddCell(cell);
                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtFechaCreacion.Text), textFont)) { Border = 0, Colspan = 3 };
                tableResponsables.AddCell(cell);


                #endregion

                #region Actividades
                // Crea la informacion de las actividades en el pdf
                PdfPTable tableActividades = new PdfPTable(5);
                float[] anchoDeColumnas = new float[] { 30f, 20f, 20f, 10f, 30f };
                tableActividades.SetWidths(anchoDeColumnas);
                if (gvActividades.Rows.Count > 0)
                {
                    int contador = 0;
                    // Crea el encabezado de la tabla
                    foreach (TableCell headerCell in gvActividades.HeaderRow.Cells)
                    {
                        PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text.Replace("&#243;", "ó"), headertableFont))
                        {
                            BackgroundColor = new Color(gvActividades.HeaderStyle.BackColor),
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        contador++;
                        if (new[] { 2, 3, 4, 5 ,6}.Any(x => x == contador))
                        {
                            tableActividades.AddCell(pdfCell);
                        }
                    }
                    foreach (GridViewRow Row in gvActividades.Rows)
                    {
                        //string txtNo = Convert.ToString(Row.RowIndex+1);
                        string txtActividad = ((TextBox)Row.FindControl("TextBox1")).Text;
                        string txtFechaInicio = ((TextBox)Row.FindControl("TextBox2")).Text;
                        string txtFechaFin = ((TextBox)Row.FindControl("TextBox3")).Text;
                        string txtResponsables = ((TextBox)Row.FindControl("TextBox4")).Text;
                        string lblEstado = ((Label)Row.FindControl("lblEstadoGv")).Text;

                        List<PdfPCell> rowTable = new List<PdfPCell>
                    {
                        //new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtNo), textFont)){HorizontalAlignment = Element.ALIGN_CENTER },
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtActividad), textFont)){HorizontalAlignment = Element.ALIGN_LEFT },
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtFechaInicio), textFont)){HorizontalAlignment = Element.ALIGN_CENTER },
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtFechaFin), textFont)){HorizontalAlignment = Element.ALIGN_CENTER },
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(lblEstado), textFont)){HorizontalAlignment = Element.ALIGN_CENTER },
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtResponsables), textFont)){HorizontalAlignment = Element.ALIGN_CENTER }
                    };

                        pRows.Add(new PdfPRow(rowTable.ToArray()));
                    }
                    tableActividades.Rows.AddRange(pRows);

                    pRows.Clear();
                }
                #endregion

                #region CierreActividades
                PdfPTable tableCierreActividades = new PdfPTable(4);
                tableCierreActividades.SetWidths(new float[] { 5, 30, 60, 20 });
                if (gvObservacionesCierreAct.Rows.Count > 0)
                {
                    foreach (TableCell headerCell in gvObservacionesCierreAct.HeaderRow.Cells)
                    {
                        Font font = new Font
                        {
                            Color = new Color(gvObservacionesCierreAct.HeaderStyle.ForeColor)
                        };
                        PdfPCell pdfCell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(headerCell.Text), headertableFont))
                        {
                            BackgroundColor = new Color(gvObservacionesCierreAct.HeaderStyle.BackColor),
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        tableCierreActividades.AddCell(pdfCell);
                    }
                    foreach (GridViewRow Row in gvObservacionesCierreAct.Rows)
                    {
                        List<PdfPCell> rowdocumentos = new List<PdfPCell>();
                        for (int i = 0; i < gvObservacionesCierreAct.HeaderRow.Cells.Count; i++)
                        {
                            rowdocumentos.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(Row.Cells[i].Text), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        }
                        pRows.Add(new PdfPRow(rowdocumentos.ToArray()));
                    }
                    tableCierreActividades.Rows.AddRange(pRows);

                    pRows.Clear();
                }
                #endregion

                #region Seguimientos
                // Crea la informacion de los seguimientos en el pdf
                PdfPTable tableSeguimientos = new PdfPTable(5);
                float[] anchoDeColumnasSeg = new float[] { 5f, 30f, 20f, 60f, 20f };
                tableSeguimientos.SetWidths(anchoDeColumnasSeg);

                if (gvSeguimientoAcm.Rows.Count > 0)
                {
                    foreach (TableCell headerCell in gvSeguimientoAcm.HeaderRow.Cells)
                    {
                        Font font = new Font
                        {
                            Color = new Color(gvSeguimientoAcm.HeaderStyle.ForeColor)
                        };
                        PdfPCell pdfCell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(headerCell.Text), headertableFont))
                        {
                            BackgroundColor = new Color(gvSeguimientoAcm.HeaderStyle.BackColor),
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        tableSeguimientos.AddCell(pdfCell);
                    }
                    foreach (GridViewRow Row in gvSeguimientoAcm.Rows)
                    {
                        List<PdfPCell> rowdocumentos = new List<PdfPCell>();
                        for (int i = 0; i < 5; i++)
                        {
                            rowdocumentos.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(Row.Cells[i].Text), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        }
                        pRows.Add(new PdfPRow(rowdocumentos.ToArray()));
                    }
                    tableSeguimientos.Rows.AddRange(pRows);

                    pRows.Clear();
                }
                #endregion

                #region CierreAcm

                PdfPTable tableCierreAcm = new PdfPTable(4);
                tableCierreAcm.DefaultCell.Border = Rectangle.NO_BORDER;
                rowTable1 = new List<PdfPCell>
                    {
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Estado Acm:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(ddlEstadoAcm.SelectedItem.Text), textFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Fecha Cierre:"), subtitleFont)){ Border = 0},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtFechaCierreAcm.Text), textFont)){ Border = 0},
                    };
                pRows.Add(new PdfPRow(rowTable1.ToArray()));
                tableCierreAcm.Rows.AddRange(pRows);

                pRows.Clear();

                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode("Verificación Eficacia:"), subtitleFont)) { Border = 0 };
                tableCierreAcm.AddCell(cell);
                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtVerificacionEficacia.Text), textFont)) { Border = 0, Colspan = 3 };
                tableCierreAcm.AddCell(cell);
                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode("Observaciones:"), subtitleFont)) { Border = 0 };
                tableCierreAcm.AddCell(cell);
                cell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(txtObservacionesAcm.Text), textFont)) { Border = 0, Colspan = 3 };
                tableCierreAcm.AddCell(cell);

                #endregion Revisiones

                #region Revisiones
                PdfPTable tableRevisiones = new PdfPTable(6);
                tableRevisiones.SetWidths(new float[] { 9, 24, 9, 24, 9, 24 });
                rowTable1 = new List<PdfPCell>
                    {
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Registra:"), subtitleFont)){ Border = 1},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(lblRegistrado.Text), textFont)){ Border = 1},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Revisa:"), subtitleFont)){ Border = 1},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(lblRevisadoPor.Text.Trim()), textFont)){ Border = 1, HorizontalAlignment = Element.ALIGN_LEFT},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode("Aprueba:"), subtitleFont)){ Border = 1},
                        new PdfPCell(new Phrase(Context.Server.HtmlDecode(lblCerradoPor.Text), textFont)){ Border = 1},
                    };
                pRows.Add(new PdfPRow(rowTable1.ToArray()));
                tableRevisiones.Rows.AddRange(pRows);
                pRows.Clear();
                #endregion

                PdfPTable separador = new PdfPTable(1);
                cell = new PdfPCell(new Phrase("")) { Border = 1 };
                separador.AddCell(cell);

                Document pdfDocument = new Document(PageSize.LETTER, 0, 0, 10, 30);
                PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
                pdfDocument.AddAuthor("Sherlock");
                pdfDocument.AddCreator("Sherlock");
                pdfDocument.AddCreationDate();
                pdfDocument.AddTitle("Reporte Gestión Acm");

                string pathImg = Server.MapPath("~") + "Imagenes/Logos/logo-sherlock.png";
                Image imagen = Image.GetInstance(pathImg);
                pathImg = Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
                Image imagenEmpresa = Image.GetInstance(pathImg);
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_RIGHT;
                PdfPTable pdftblImage = new PdfPTable(2);
                PdfPCell pdfcellImage = new PdfPCell(imagen, true)
                {
                    FixedHeight = 40f,
                    Border = Rectangle.NO_BORDER
                };
                pdfcellImage.Border = Rectangle.NO_BORDER;
                pdftblImage.AddCell(pdfcellImage);
                PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true)
                {
                    FixedHeight = 40f,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT,
                    Border = Rectangle.NO_BORDER
                };
                pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
                pdftblImage.AddCell(pdfcellImageEmpresa);
                Phrase phHeader = new Phrase
                {
                    pdftblImage
                };
                pdftblImage.SpacingAfter = 20;
                HeaderFooter header = new HeaderFooter(phHeader, false)
                {
                    Border = Rectangle.NO_BORDER,
                    Alignment = Element.ALIGN_CENTER,
                };
                pdfDocument.Header = header;
                pdfDocument.Open();
                Paragraph Titulo = new Paragraph(new Phrase("Reporte Gestión Acm", titleFont));
                Titulo.SetAlignment("Center");
                pdfDocument.Add(Titulo);
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(Chunk.NEWLINE);

                Paragraph paragraph = new Paragraph(new Phrase("Causas y Responsables", subtitleFont));
                paragraph.SetAlignment("Center");
                pdfDocument.Add(paragraph);
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(tableResponsables);
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(new Phrase(""));

                paragraph = new Paragraph(new Phrase("Actividades plan de acción", subtitleFont));
                paragraph.SetAlignment("Center");
                pdfDocument.Add(paragraph);
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(tableActividades);
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(new Phrase(""));

                paragraph = new Paragraph(new Phrase("Seguimiento a Actividades", subtitleFont));
                paragraph.SetAlignment("Center");
                pdfDocument.Add(paragraph);
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(tableSeguimientos);
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(new Phrase(""));

                paragraph = new Paragraph(new Phrase("Actividades Cerradas - Observaciones", subtitleFont));
                paragraph.SetAlignment("Center");
                pdfDocument.Add(paragraph);
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(tableCierreActividades);
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(new Phrase(""));

                pdfDocument.Add(separador);
                paragraph = new Paragraph(new Phrase("Cierre Acm", subtitleFont));
                paragraph.SetAlignment("Center");
                pdfDocument.Add(paragraph);
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(tableCierreAcm);
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(new Phrase(""));

                paragraph = new Paragraph(new Phrase("", subtitleFont));
                paragraph.SetAlignment("Center");
                pdfDocument.Add(paragraph);
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(tableRevisiones);

                pdfDocument.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Reporte Acm.pdf");
                Response.Write(pdfDocument);
                Response.Flush();
                Response.End();
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {

                omb.ShowMessage($"Error al generar el documento. {ex.Message}", 1, "Atención");
            }
        }

        private bool EnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            try
            {
                #region informacion basica
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString();
                        Otros = row["Otros"].ToString();
                        Asunto = row["Asunto"].ToString();
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region Consulta el correo del Destinatario
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = " + idNodoJerarquia;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = row["CorreoResponsable"].ToString().Trim();
                        idJefeInmediato = row["idPadre"].ToString().Trim();
                    }
                }
                #endregion Consulta el correo del Destinatario

                #region Consulta el correo del Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeInmediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeInmediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                            idJefeMediato = row["idPadre"].ToString().Trim();
                        }
                    }
                }
                #endregion Consulta el correo del Jefe Inmediato

                #region Consulta el correo del Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeMediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeMediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                        }
                    }
                }
                #endregion Consulta el correo del Jefe Mediato

                //Insertar el Registro en la tabla de Correos Enviados
                #region Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.Insert();
                #endregion Insertar el Registro en la tabla de Correos Enviados
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                #region
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource201.Insert();
                }
                #endregion

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");

                    message.From = fromAddress;//here you can set address

                    #region
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region
                    if (Otros.Trim() != "")
                        foreach (string substr in Otros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    message.Subject = Asunto;//subject of email
                    message.IsBodyHtml = true;//To determine email body is html or not
                    message.Body = Cuerpo;

                    smtpClient.Send(message);
                    #endregion Envio Correo
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    #region Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString();
                    SqlDataSource200.Update();
                    #endregion Actualiza el Estado del Correo Enviado
                }
            }

            return (err);
        }

        private void GenerarNotificacion(TipoNotificacion tipoNotificacion, List<Classes.DTO.Calidad.PlanAccionActividad> actividades = null, int? idAcm = null)
        {
            try
            {
                string TextoAdicional = string.Empty;

                if (tipoNotificacion.ToString() == "Acm")
                {
                    TextoAdicional = "ASIGNACIÓN DE ACM" + "<br>";
                    TextoAdicional = TextoAdicional + "<br>";
                    TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(txtCodigo.Text.Trim()) + "<br>";
                    TextoAdicional = TextoAdicional + " Origen no conformidad : " + Sanitizer.GetSafeHtmlFragment(ddlOrigenNoConformidad.SelectedItem.Text) + "<br>";
                    TextoAdicional = TextoAdicional + " Proceso: " + Sanitizer.GetSafeHtmlFragment(ddlProceso.SelectedItem.Text) + "<br>";
                    TextoAdicional = TextoAdicional + " Causas Raiz : " + Sanitizer.GetSafeHtmlFragment(txtCausasRaiz.Text) + "<br>";

                    string[] responsables = lblIdDependencia1.Text.Split(',');
                    if (responsables.Length > 0)
                    {
                        foreach (string responsable in responsables)
                        {
                            EnviarNotificacion(33, 1, Convert.ToInt16(responsable.Trim()), DateTime.Now.ToString().Trim(), TextoAdicional);
                        }
                    }
                }
                else
                {
                    //Obtiene el código del Acm.
                    using (GestionAcmBLL objData = new GestionAcmBLL())
                    {
                        Classes.DTO.Calidad.GestionAcm acm = objData.SeleccionarAcms().Where(x => x.IdAcm == idAcm).FirstOrDefault();
                        TextoAdicional = "ASIGNACIÓN DE ACTIVIDAD" + "<br>";
                        TextoAdicional = TextoAdicional + "<br>";

                        // Recorre las actividades para enviar la notificación.
                        actividades.ForEach(actividad =>
                        {
                            TextoAdicional = TextoAdicional + " Nombre Actividad : " + Sanitizer.GetSafeHtmlFragment(actividad.Nombre) + "<br>";
                            TextoAdicional = TextoAdicional + " Código Acm : " + Sanitizer.GetSafeHtmlFragment(acm.Codigo.Trim()) + "<br>";
                            actividad.Responsables.Split(',').ToList().ForEach(resp =>
                            {
                                if (actividad.IdActividad == 0)
                                {
                                    EnviarNotificacion(34, 1, Convert.ToInt16(resp.Trim()), DateTime.Now.ToString().Trim(), TextoAdicional);
                                }
                                else
                                {
                                    EnviarNotificacion(34, 1, Convert.ToInt16(resp.Trim()), DateTime.Now.ToString().Trim(), TextoAdicional);
                                }
                            });

                        });
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GridViews
        private void SetInitialRow()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("Column1", typeof(string)));
                dt.Columns.Add(new DataColumn("Column2", typeof(string)));
                dt.Columns.Add(new DataColumn("Column3", typeof(string)));
                dt.Columns.Add(new DataColumn("Column4", typeof(string)));
                dt.Columns.Add(new DataColumn("Column5", typeof(string)));
                dt.Columns.Add(new DataColumn("Column6", typeof(string)));
                dt.Columns.Add(new DataColumn("Column7", typeof(string)));
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;
                dr["Column3"] = string.Empty;
                dr["Column4"] = string.Empty;
                dr["Column5"] = string.Empty;
                dr["Column6"] = string.Empty;
                dr["Column7"] = string.Empty;
                dt.Rows.Add(dr);
                // Guarda el Datatable en Viewstate
                ViewState["CurrentTable"] = dt;
                gvActividades.DataSource = dt;
                gvActividades.DataBind();

                // Buscar controles
                TreeView tree = (TreeView)gvActividades.Rows[0].Cells[4].FindControl("TreeViewGv");
                if (tree != null)
                {
                    PopulateTreeViews(tree);
                }

                foreach (GridViewRow row in gvActividades.Rows)
                {
                    TextBox txt2 = (TextBox)row.Cells[3].FindControl("TextBox2");
                    if (txt2 != null)
                    {
                        txt2.Attributes.Add("readonly", "true");
                    }
                    TextBox txt3 = (TextBox)row.Cells[3].FindControl("TextBox3");
                    if (txt3 != null)
                    {
                        txt3.Attributes.Add("readonly", "true");
                    }
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al agregar la fila de la actividad. {ex.Message}", 1, "Atención");
            }
        }

        private void AddNewRowToGrid()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            // Se extraen los valores
                            TextBox box1 = (TextBox)gvActividades.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                            TextBox box2 = (TextBox)gvActividades.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                            TextBox box3 = (TextBox)gvActividades.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                            TextBox box4 = (TextBox)gvActividades.Rows[rowIndex].Cells[3].FindControl("TextBox4");
                            Label label = (Label)gvActividades.Rows[rowIndex].Cells[3].FindControl("lblIdDependenciaGv");

                            // Valida si es posible agregar otra fila
                            if (new[] { box1.Text, box2.Text, box3.Text, box4.Text, label.Text }.Any(x => x == string.Empty))
                            {
                                omb.ShowMessage("Para agregar otra actividad, termine de editar la actividad actual.", 2, "Atención");
                                return;
                            }

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["RowNumber"] = i + 1;
                            dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                            dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                            dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                            dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                            dtCurrentTable.Rows[i - 1]["Column5"] = label.Text;
                            rowIndex++;
                        }
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;
                        gvActividades.DataSource = dtCurrentTable;
                        gvActividades.DataBind();
                    }
                    // Buscar controles
                    foreach (TableRow row in gvActividades.Rows)
                    {
                        TreeView tree = (TreeView)row.Cells[4].FindControl("TreeViewGv");
                        if (tree != null)
                        {
                            PopulateTreeViews(tree);
                        }
                        TextBox txt2 = (TextBox)row.Cells[3].FindControl("TextBox2");
                        if (txt2 != null)
                        {
                            txt2.Attributes.Add("readonly", "true");
                        }
                        TextBox txt3 = (TextBox)row.Cells[3].FindControl("TextBox3");
                        if (txt3 != null)
                        {
                            txt3.Attributes.Add("readonly", "true");
                        }
                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }
                //Establece los datos anteriores
                SetPreviousData();
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al crear la fila de la actividad. {ex.Message}", 1, "Atención");
            }

        }

        private void AddNewRowToGridWithData(Classes.DTO.Calidad.PlanAccionActividad actividad, string responsables, string codigosResponsable)
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        // Valida si el es primer ingreso para borrar la fila en blanco
                        if ((bool)Session["PrimerIngreso"])
                        {
                            ((DataTable)ViewState["CurrentTable"]).Rows.Clear();
                            Session["PrimerIngreso"] = false;
                        }

                        // Controla el Index del GridView
                        int rowNumber = dtCurrentTable.Rows.Count + 1;

                        Label labelIdActividad = (Label)gvActividades.Rows[rowIndex].Cells[1].FindControl("lblIdActividadGv");
                        TextBox box1 = (TextBox)gvActividades.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)gvActividades.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)gvActividades.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)gvActividades.Rows[rowIndex].Cells[3].FindControl("TextBox4");
                        Label labelEstado = (Label)gvActividades.Rows[rowIndex].Cells[4].FindControl("lblEstadoGv");
                        Label labelResponsables = (Label)gvActividades.Rows[rowIndex].Cells[3].FindControl("lblIdDependenciaGv");
                        box1.Text = actividad.Nombre;
                        box2.Text = Convert.ToDateTime(actividad.FechaInicio).ToShortDateString();
                        box3.Text = Convert.ToDateTime(actividad.FechaFin).ToShortDateString();
                        box4.Text = responsables;
                        labelResponsables.Text = codigosResponsable;
                        labelIdActividad.Text = actividad.IdActividad.ToString();
                        labelEstado.Text = actividad.NombreEstado;
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow[0] = rowNumber;
                        drCurrentRow[1] = box1.Text;
                        drCurrentRow[2] = box2.Text;
                        drCurrentRow[3] = box3.Text;
                        drCurrentRow[4] = box4.Text;
                        drCurrentRow[5] = labelResponsables.Text;
                        drCurrentRow[6] = labelIdActividad.Text;
                        drCurrentRow[7] = labelEstado.Text;
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;
                        gvActividades.DataSource = dtCurrentTable;
                        gvActividades.DataBind();

                    }
                    // Buscar controles
                    foreach (TableRow row in gvActividades.Rows)
                    {
                        TreeView tree = (TreeView)row.Cells[4].FindControl("TreeViewGv");
                        if (tree != null)
                        {
                            PopulateTreeViews(tree);
                        }
                        TextBox txt2 = (TextBox)row.Cells[3].FindControl("TextBox2");
                        if (txt2 != null)
                        {
                            txt2.Attributes.Add("readonly", "true");
                        }
                        TextBox txt3 = (TextBox)row.Cells[3].FindControl("TextBox3");
                        if (txt3 != null)
                        {
                            txt3.Attributes.Add("readonly", "true");
                        }
                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }
                // Establece los datos anteriores
                SetPreviousData();
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al crear la fila de la actividad. {ex.Message}", 1, "Atención");
            }
        }

        private void SetPreviousData()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];

                    // Recupera los datos anteriores del Viewstate

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TextBox box1 = (TextBox)gvActividades.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                            TextBox box2 = (TextBox)gvActividades.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                            TextBox box3 = (TextBox)gvActividades.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                            TextBox box4 = (TextBox)gvActividades.Rows[rowIndex].Cells[3].FindControl("TextBox4");
                            Label label = (Label)gvActividades.Rows[rowIndex].Cells[4].FindControl("lblIdDependenciaGv");
                            Label labelActividad = (Label)gvActividades.Rows[rowIndex].Cells[4].FindControl("lblIdActividadGv");
                            Label labelEstado = (Label)gvActividades.Rows[rowIndex].Cells[4].FindControl("lblEstadoGv");
                            box1.Text = dt.Rows[i]["Column1"].ToString();
                            box2.Text = dt.Rows[i]["Column2"].ToString();
                            box3.Text = dt.Rows[i]["Column3"].ToString();
                            box4.Text = dt.Rows[i]["Column4"].ToString();
                            label.Text = dt.Rows[i]["Column5"].ToString();
                            labelActividad.Text = dt.Rows[i]["Column6"].ToString();
                            labelEstado.Text = dt.Rows[i]["Column7"].ToString();
                            box4.ToolTip = box4.Text;

                            // Si el estado es cerrado bloquea los controles de edición
                            if (labelEstado.Text == "Cerrado")
                            {
                                ImageButton btn1 = (ImageButton)gvActividades.Rows[rowIndex].Cells[1].FindControl("btnBorrarGruposGv");
                                ImageButton btn2 = (ImageButton)gvActividades.Rows[rowIndex].Cells[1].FindControl("imgDependenciaGv");
                                box1.Enabled = false;
                                box2.Enabled = false;
                                box3.Enabled = false;
                                box4.Enabled = false;
                                btn1.Enabled = false;
                                btn2.Enabled = false;
                            }
                            rowIndex++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                omb.ShowMessage($"Error al recuperar la fila de la actividad. {ex.Message}", 1, "Atención");
            }
        }

        private void CargarGrillaAcm()
        {
            try
            {
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    gvAcm.DataSource = objData.SeleccionarAcms();
                    gvAcm.DataBind();
                    if (gvAcm.Rows.Count == 0)
                        omb.ShowMessage("No se han registrado Acm", 3, "Información");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los Acm. {ex.Message}", 1, "Atención");
            }
        }

        private void CargarGrillaSeguimientos(int idActividad)
        {
            try
            {
                Session["IdActividad"] = idActividad;
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    gvSeguimiento.DataSource = objData.SeleccionarSeguimientos(idActividad);
                    gvSeguimiento.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Eventos

        protected void DdlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (LoadDDLMacroProceso(Convert.ToInt32(ddlCadenaValor.SelectedValue)))
            {
                ddlMacroproceso.ClearSelection();
                ddlProceso.Items.Clear();
                ddlSubproceso.Items.Clear();
            };
        }

        protected void DdlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (LoadDDLProceso(Convert.ToInt32(ddlMacroproceso.SelectedValue)))
            {
                ddlProceso.ClearSelection();
                ddlSubproceso.Items.Clear();
            }
        }


        protected void DdlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadDDLSubproceso(Convert.ToInt32(ddlProceso.SelectedValue)))
            {
                ddlSubproceso.ClearSelection();
                if (ddlSubproceso.Items.Count == 1)
                {
                    omb.ShowMessage("No hay información de Subprocesos", 2, "Atención");
                }
            }
        }

        protected void BtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if ((int)Session["IdAcm"] > 0)
                {
                    if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    {
                        omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                        return;
                    }
                }
                else
                {
                    if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    {
                        omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                        return;
                    }
                }

                // Inicio validaciones para cerrar el Acm

                if (gvActividades.Rows.Count <= 1 && ddlEstadoAcm.SelectedValue == "2")
                {
                    TextBox txt = (TextBox)gvActividades.Rows[0].Cells[0].FindControl("TextBox1");
                    if (txt.Text == string.Empty)
                    {
                        omb.ShowMessage("Debe registrar al menos una actividad de seguimiento.", 2, "Atención");
                        return;
                    }
                }
                if (chkAprobado.Checked && ddlEstadoAcm.SelectedValue != "2")
                {
                    omb.ShowMessage("El estado debe estar cerrado para aprobar.", 2, "Atención");
                    return;
                }
                else if (ddlEstadoAcm.SelectedValue == "2" && !chkAprobado.Checked)
                {
                    omb.ShowMessage("Para cerrar el Acm debe estar aprobado.", 2, "Atención");
                    return;
                }
                else if (chkAprobado.Checked && !chkRevisado.Checked)
                {
                    omb.ShowMessage("Para aprobar un Acm debe estar revisado.", 2, "Atención");
                    return;
                }
                if (ddlEstadoAcm.SelectedValue == "2" && txtVerificacionEficacia.Text == string.Empty)
                {
                    omb.ShowMessage("Debe escribir la verificación de eficacia antes de cerrar el Acm.", 2, "Atención");
                    return;
                }
                if (ddlEstadoAcm.SelectedValue == "2" && txtObservacionesAcm.Text == string.Empty)
                {
                    omb.ShowMessage("Debe esribir las observaciones del cierre.", 2, "Atención");
                    return;
                }
                // Fin validaciones para cerrar el Acm

                #region ArchivoCargado
                string pathFile = string.Empty;
                string extension = string.Empty;
                string nombreArchivo = string.Empty;
                byte[] archivo = null;

                string estadoAcm = ddlEstadoAcm.SelectedValue;

                if (fuAnalisisCausa.HasFile)
                {
                    if (new[] { ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".txt", ".ppt", ".pptx" }.Any(x => x == System.IO.Path.GetExtension(fuAnalisisCausa.FileName).ToLower().ToString().Trim()))
                    {
                        pathFile = "Analisis." + fuAnalisisCausa.FileName;
                        nombreArchivo = fuAnalisisCausa.FileName;
                        archivo = fuAnalisisCausa.FileBytes;
                        int length = Convert.ToInt32(fuAnalisisCausa.FileContent.Length);
                        extension = System.IO.Path.GetExtension(fuAnalisisCausa.FileName).ToLower().ToString().Trim();
                    }
                    else
                    {
                        omb.ShowMessage("Solo archivos en formato pdf, word,excel, power point", 2, "Atención");
                        return;
                    }
                }
                #endregion
                int idAcm = (int)Session["IdAcm"];
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    int? usuarioRevisa = null;
                    int? usuarioAprueba = null;

                    // Se valida el estado de los checkbox
                    if (chkRevisado.Checked && chkRevisado.Enabled)
                        usuarioRevisa = Convert.ToInt32(Session["IdUsuario"]);

                    if (chkAprobado.Checked && chkAprobado.Enabled)
                        usuarioAprueba = Convert.ToInt32(Session["IdUsuario"]);



                    // Registra la primera parte del Acm
                    Classes.DTO.Calidad.GestionAcm acm = new Classes.DTO.Calidad.GestionAcm
                    {
                        IdAcm = (int)Session["IdAcm"],
                        Proceso = Convert.ToInt32(ddlProceso.SelectedValue),
                        MacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue),
                        CadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue),
                        Subproceso = ddlSubproceso.SelectedValue.Equals("0") ? new int?() : Convert.ToInt32(ddlSubproceso.SelectedValue),
                        DescripcionNoConformidad = txtDescNoConformidad.Text,
                        OrigenNoConformidad = Convert.ToInt32(ddlOrigenNoConformidad.SelectedValue),
                        CausasRaiz = txtCausasRaiz.Text,
                        Codigo = txtCodigo.Text,
                        AnalisisCausa = archivo,
                        NombreArchivo = nombreArchivo,
                        Extension = extension,
                        Estado = ddlEstadoAcm.SelectedValue.Equals("0") ? 1 : Convert.ToInt32(ddlEstadoAcm.SelectedValue),
                        VerificacionEficacia = txtVerificacionEficacia.Text,
                        Observaciones = txtObservacionesAcm.Text,
                        UsuarioRegistra = Convert.ToInt32(Session["IdUsuario"]),
                        UsuarioModifica = Convert.ToInt32(Session["IdUsuario"]),
                        UsuarioRevisa = usuarioRevisa,
                        UsuarioAprueba = usuarioAprueba,
                        GrupoTrabajo = txtGrupo.Text.Trim(),
                    };

                    // Valida si hay actividades pendientes
                    if (objData.SeleccionarActividadesPendientes(acm.IdAcm) > 0 && ddlEstadoAcm.SelectedValue == "2")
                    {
                        omb.ShowMessage("No se puede cerrar el Acm porque hay actividades en curso.", 2, "Atención");
                        return;
                    }

                    int IdAcm = objData.InsertarActualizarAcm(acm);
                    if (IdAcm > 0)
                    {
                        // Controla que se borren las asociaciones anteriores
                        int contador = 0;

                        // Realiza la segunda parte del Insert del Acm
                        List<Classes.DTO.Calidad.AcmResponsable> lstAcmResponsable = new List<Classes.DTO.Calidad.AcmResponsable>();
                        lblIdDependencia1.Text.Substring(0, lblIdDependencia1.Text.Length).Split(',').ToList().ForEach(
                            str =>
                            {
                                lstAcmResponsable.Add(new Classes.DTO.Calidad.AcmResponsable()
                                {
                                    IdAcmResponsable = 0,
                                    IdResponsable = Convert.ToInt32(str),
                                    IdAcm = IdAcm,
                                    Usuario = Convert.ToInt32(Session["IdUsuario"]),
                                    Flag = contador
                                });
                                contador++;
                            }
                            );
                        objData.InsertarActualizarResponsablesAcm(lstAcmResponsable);

                        contador = 0;

                        //List<Classes.DTO.Calidad.AcmGrupoTrabajo> lstAcmGrupoTrabajo = new List<Classes.DTO.Calidad.AcmGrupoTrabajo>();
                        //lDependeciaGrupo.Text.Substring(0, lDependeciaGrupo.Text.Length).Split(',').ToList().ForEach(
                        //    str =>
                        //    {
                        //        lstAcmGrupoTrabajo.Add(new Classes.DTO.Calidad.AcmGrupoTrabajo()
                        //        {
                        //            IdAcmGrupoTrabajo = 0,
                        //            IdGrupoTrabajo = Convert.ToInt32(str),
                        //            IdAcm = IdAcm,
                        //            Usuario = Convert.ToInt32(Session["IdUsuario"]),
                        //            Flag = contador
                        //        });
                        //        contador++;
                        //    }
                        //    );
                        //objData.InsertarActualizarGruposTrabajoAcm(lstAcmGrupoTrabajo);

                        // Registra las actividades y responsables del plan de acción

                        List<Classes.DTO.Calidad.PlanAccionActividad> lstPlanAccionActividad = new List<Classes.DTO.Calidad.PlanAccionActividad>();
                        foreach (GridViewRow row in gvActividades.Rows)
                        {
                            TextBox txtNombre = (TextBox)row.Cells[0].FindControl("TextBox1");
                            TextBox txtFechaInicio = (TextBox)row.Cells[0].FindControl("TextBox2");
                            TextBox txtFechaFin = (TextBox)row.Cells[0].FindControl("TextBox3");
                            Label lblResponsables = (Label)row.Cells[4].FindControl("lblIdDependenciaGv");
                            Label lblIdActividad = (Label)row.Cells[1].FindControl("lblIdActividadGv");

                            if (!new[] { txtNombre.Text, txtFechaInicio.Text, txtFechaFin.Text, lblResponsables.Text }.Any(x => x == string.Empty))
                            {
                                string[] lastname = txtNombre.Text.Split('.');
                                string newname = "";
                                if (lastname.Count() > 1)
                                {
                                    newname = lastname[1];
                                    lstPlanAccionActividad.Add(new Classes.DTO.Calidad.PlanAccionActividad()
                                    {
                                        IdActividad = lblIdActividad.Text.Equals("") ? 0 : Convert.ToInt32(lblIdActividad.Text),
                                        Nombre = newname,
                                        //Nombre = txtNombre.Text.Substring(txtNombre.Text.IndexOf(".    ") +5, (txtNombre.Text.Length - txtNombre.Text.IndexOf(".")-5)),
                                        //string a = txtNombre.Text.Substring(txtNombre.Text.IndexOf("."), txtNombre.Text.Length - txtNombre.Text.IndexOf(".")),
                                        IdAcm = IdAcm,
                                        FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                                        FechaFin = Convert.ToDateTime(txtFechaFin.Text),
                                        Responsables = lblResponsables.Text,
                                        Usuario = Convert.ToInt32(Session["IdUsuario"]),
                                        UsuarioModifica = Convert.ToInt32(Session["IdUsuario"]),
                                        IdPlanAccionActividadResponsable = 0
                                    });
                                }
                                else
                                {
                                    lstPlanAccionActividad.Add(new Classes.DTO.Calidad.PlanAccionActividad()
                                    {
                                        IdActividad = lblIdActividad.Text.Equals("") ? 0 : Convert.ToInt32(lblIdActividad.Text),
                                        Nombre = txtNombre.Text,
                                        //Nombre = txtNombre.Text.Substring(txtNombre.Text.IndexOf(".    ") +5, (txtNombre.Text.Length - txtNombre.Text.IndexOf(".")-5)),
                                        //string a = txtNombre.Text.Substring(txtNombre.Text.IndexOf("."), txtNombre.Text.Length - txtNombre.Text.IndexOf(".")),
                                        IdAcm = IdAcm,
                                        FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                                        FechaFin = Convert.ToDateTime(txtFechaFin.Text),
                                        Responsables = lblResponsables.Text,
                                        Usuario = Convert.ToInt32(Session["IdUsuario"]),
                                        UsuarioModifica = Convert.ToInt32(Session["IdUsuario"]),
                                        IdPlanAccionActividadResponsable = 0
                                    });
                                }

                                objData.InsertarActualizarActividades(lstPlanAccionActividad);

                                if (lstPlanAccionActividad.Count > 0)
                                {
                                    GenerarNotificacion(TipoNotificacion.Actividad, lstPlanAccionActividad, IdAcm);
                                }

                                lstPlanAccionActividad.Clear();
                            }

                            //if (!new[] { txtNombre.Text, txtFechaInicio.Text, txtFechaFin.Text, lblResponsables.Text }.Any(x => x == string.Empty))
                            //{
                            //    /*string[] lastname = txtNombre.Text.Split('.');
                            //    string newname = lastname[1];*/
                            //    lstPlanAccionActividad.Add(new Classes.DTO.Calidad.PlanAccionActividad()
                            //    {
                            //        IdActividad = lblIdActividad.Text.Equals("") ? 0 : Convert.ToInt32(lblIdActividad.Text),
                            //        Nombre = txtNombre.Text,
                            //        //Nombre = txtNombre.Text.Substring(txtNombre.Text.IndexOf(".    ") +5, (txtNombre.Text.Length - txtNombre.Text.IndexOf(".")-5)),
                            //        //string a = txtNombre.Text.Substring(txtNombre.Text.IndexOf("."), txtNombre.Text.Length - txtNombre.Text.IndexOf(".")),
                            //        IdAcm = IdAcm,
                            //        FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                            //        FechaFin = Convert.ToDateTime(txtFechaFin.Text),
                            //        Responsables = lblResponsables.Text,
                            //        Usuario = Convert.ToInt32(Session["IdUsuario"]),
                            //        UsuarioModifica = Convert.ToInt32(Session["IdUsuario"]),
                            //        IdPlanAccionActividadResponsable = 0
                            //    });
                            //    objData.InsertarActualizarActividades(lstPlanAccionActividad);

                            //    if (lstPlanAccionActividad.Count > 0)
                            //    {
                            //        GenerarNotificacion(TipoNotificacion.Actividad, lstPlanAccionActividad, IdAcm);
                            //    }

                            //    lstPlanAccionActividad.Clear();
                            //}

                        }

                        // Envia correo cuando se crea el Acm
                        if (ddlEstadoAcm.SelectedValue == "0")
                        {
                            GenerarNotificacion(TipoNotificacion.Acm);
                        }
                    }
                }
                /*if(gvAdjuntosCierre.Rows.Count > 0)
                {
                    clsAcmCierreAdjuntosBLL blCierreAdjuntos = new clsAcmCierreAdjuntosBLL();
                    string strErrMsg = string.Empty;
                    dtAdjuntos = (DataTable)Session["dtAdjuntos"];
                    int iteracion = 0;
                    foreach (DataRow rowArchivo in dtAdjuntos.Rows)
                    {
                        string fileName = rowArchivo[0].ToString();
                        FileStream fs = new FileStream(fileName, FileMode.Open);
                        BinaryReader reader = new BinaryReader(fs);
                        byte[] data = reader.ReadBytes((int)fs.Length);
                        fs.Close();
                        
                        clsAcmCierreAdjuntos objAdjuntos = new clsAcmCierreAdjuntos();
                        objAdjuntos.strnombreArchivo = rowArchivo[0].ToString();
                        objAdjuntos.strpathFile = rowArchivo[1].ToString();
                        objAdjuntos.strextension = rowArchivo[2].ToString();
                        objAdjuntos.btArchivo = data;
                        objAdjuntos.intIdAcm = idAcm;

                        blCierreAdjuntos.mtdInsertarAdjuntosCierre(objAdjuntos, ref strErrMsg);
                        if(strErrMsg != string.Empty)
                        {
                            omb.ShowMessage(strErrMsg,1);
                            return;
                        }
                        iteracion++;
                    }
                }*/
                if ((int)Session["IdAcm"] == 0)
                    omb.ShowMessage("Acm registrado con éxito.", 3, "Información");
                else if ((int)Session["IdAcm"] > 0 && ddlEstadoAcm.SelectedValue != "2")
                    omb.ShowMessage("Acm actualizado con éxito.", 3, "Información");
                else if ((int)Session["IdAcm"] > 0 && ddlEstadoAcm.SelectedValue == "2")
                    omb.ShowMessage("Acm cerrado con éxito.", 3, "Información");
                Reset();
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al registrar el Acm. {ex.Message}", 1, "Atención");
            }
        }

        protected void BtnAgregarPlan_Click(object sender, EventArgs e)
        {
            if (!(bool)Session["AcmCerrado"])
            {
                AddNewRowToGrid();
                // No se habilita el cierre del acm si apenas se crea una actividad
                ddlEstadoAcm.Items.FindByValue("2").Enabled = false;
            }
            else
                omb.ShowMessage("No se puede agregar una nueva actividad a un Acm cerrado", 2, "Atención");
        }

        protected void TreeViewGv_SelectedNodeChanged(object sender, EventArgs e)
        {
            //Se obtiene el Treeview de la grilla que origina el evento
            TreeView treeCurrent = (TreeView)sender;
            GridViewRow gridViewRow = ((GridViewRow)treeCurrent.Parent.Parent.Parent);
            TreeView treeViewCurrentRow = (TreeView)gridViewRow.FindControl("TreeViewGv");
            if (treeViewCurrentRow != null)
            {
                Label labelRow = (Label)gridViewRow.FindControl("lblIdDependenciaGv");
                TextBox box4 = (TextBox)gridViewRow.FindControl("TextBox4");

                if (labelRow.Text.Length > 0)
                {
                    labelRow.Text += $",{treeViewCurrentRow.SelectedNode.Value}";
                    box4.Text += $", {treeViewCurrentRow.SelectedNode.Text}";
                }
                else
                {
                    labelRow.Text += $"{treeViewCurrentRow.SelectedNode.Value}";
                    box4.Text += $"{treeViewCurrentRow.SelectedNode.Text}";
                }

            }
        }

        protected void GvAcm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Seleccionar":
                    CargarInfoEditarAcm(Convert.ToInt32(gvAcm.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text));
                    //CargarEstadoActividad(Convert.ToInt32(gvAcm.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text));
                    break;
            }
        }

        protected void BtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            trNuevoAcm.Visible = true;
            trBotones.Visible = true;
            trGrillaAcm.Visible = false;
            gvActividades.Columns[4].Visible = false;
            txtFechaCreacion.Text = System.DateTime.Now.ToString();
        }

        protected void BtnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Reset();
        }

        protected void BtnBorrarResponsables_Click(object sender, ImageClickEventArgs e)
        {
            txtNResponsableEjecucion.Text = string.Empty;
            lblIdDependencia1.Text = string.Empty;
        }

        protected void BtnBorrarGrupos_Click(object sender, ImageClickEventArgs e)
        {
            txtGrupo.Text = string.Empty;
            lDependeciaGrupo.Text = string.Empty;
        }

        protected void BtnBorrarGruposGv_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow gridViewRow = ((GridViewRow)btn.Parent.Parent);
            Label label = (Label)gridViewRow.FindControl("lblIdDependenciaGv");
            label.Text = string.Empty;
            TextBox box = (TextBox)gridViewRow.FindControl("TextBox4");
            box.Text = string.Empty;
        }

        protected void BtnInsertarSeguimiento_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                    return;
                }

                Page.Validate();
                if (!Page.IsValid)
                {
                    modalPopup.Show();
                    return;
                }
                using (GestionAcmBLL objData = new GestionAcmBLL())
                {
                    Classes.DTO.Calidad.Seguimiento seguimiento = new Classes.DTO.Calidad.Seguimiento
                    {
                        Descripcion = txtComentario.Text,
                        IdPlanAccionActividad = (int)Session["IdActividad"],
                        Usuario = Convert.ToInt32(Session["IdUsuario"])
                    };
                    int result = objData.InsertarSeguimiento(seguimiento);

                    // Actualiza el estado de la actividad
                    Classes.DTO.Calidad.PlanAccionActividad actividad = new Classes.DTO.Calidad.PlanAccionActividad
                    {
                        IdActividad = (int)Session["IdActividad"],
                        Estado = Convert.ToInt32(ddlEstadoActividad.SelectedValue),
                        Observaciones = txtObservacionesCierreAct.Text
                    };

                    result = objData.ActualizarEstadoActividad(actividad);

                    if (result > 0)
                    {
                        ObtenerActividades();
                        if (ddlEstadoActividad.SelectedValue == "1")
                            omb.ShowMessage("Seguimiento registrado con éxito.", 3, "Información");
                        else
                            omb.ShowMessage("Actividad cerrada con éxito.", 3, "Información");

                        ddlEstadoActividad.ClearSelection();
                        Session["IdActividad"] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al registrar el seguimiento. {ex.Message}", 1, "Atención");
            }
        }

        protected void DdlEstadoActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            modalPopup.Show();
            if (ddlEstadoActividad.SelectedValue == "2")
                trObservacioneCierreAct.Visible = true;
            else
                trObservacioneCierreAct.Visible = false;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (ddlEstadoActividad.SelectedValue == "2" && txtObservacionesCierreAct.Text == string.Empty)
            {
                CustomValidator1.ErrorMessage = "*";
                args.IsValid = false;
            }
        }

        protected void BtnConfigurar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                GridViewRow gridViewRow = ((GridViewRow)btn.Parent.Parent);
                int index = gridViewRow.RowIndex;
                Label lblIdActividad = (Label)gvActividades.Rows[index].Cells[1].FindControl("lblIdActividadGv");
                if (lblIdActividad.Text == string.Empty)
                {
                    omb.ShowMessage("No se pueden adicionar seguimientos a una actividad que no esta creada", 2, "Atención");
                    return;
                }
                modalPopup.Show();
                string strNoActividad = Convert.ToString(index + 1);
                TextBox txtNombre = (TextBox)gvActividades.Rows[index].Cells[1].FindControl("TextBox1");
                lblActividad.Text = "Actividad No: " + strNoActividad + " - " + txtNombre.Text;
                CargarGrillaSeguimientos(lblIdActividad.Text.Equals("") ? 0 : Convert.ToInt32(lblIdActividad.Text));
                CargarEstadoActividad(lblIdActividad.Text.Equals("") ? 0 : Convert.ToInt32(lblIdActividad.Text));
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los seguimientos. {ex.Message}", 1, "Atención");
            }
        }

        #endregion

        protected void BtnDescargarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", $"attachment; filename = {(string)Session["NombreArchivo"]}");
                //Set the content type as file extension type
                Response.ContentType = $"{(string)Session["Extension"]}";
                //Write the file content
                Response.BinaryWrite((byte[])Session["AnalisisCausa"]);
                Response.End();
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al descargar el archivo {ex.Message}", 1, "Error");
            }

        }

        protected void BtnDescargarAcm_Click(object sender, ImageClickEventArgs e)
        {
            CrearPdf();
        }

        protected void SqlDataSource200_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }
        /*********************************************************************************************
         * Autor: John Restrepo
         * Descripcion: Boton para adicionar mas de un adjunto.
         * Fecha: 11-12-2018
         * *******************************************************************************************/
        protected void imbAddAdjunto_Click(object sender, ImageClickEventArgs e)
        {
            #region ArchivosAdjuntos
            string pathFile = string.Empty;
            string extension = string.Empty;
            string nombreArchivo = string.Empty;
            byte[] archivo = null;
            int idAcm = (int)Session["IdAcm"];
            string estadoAcm = ddlEstadoAcm.SelectedValue;

            if (fuCierreAdjuntos.HasFile)
            {
                if (new[] { ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".txt", ".ppt", ".pptx" }.Any(x => x == System.IO.Path.GetExtension(fuCierreAdjuntos.FileName).ToLower().ToString().Trim()))
                {
                    pathFile = "Cierre." + fuCierreAdjuntos.FileName;
                    nombreArchivo = fuCierreAdjuntos.FileName;
                    archivo = fuCierreAdjuntos.FileBytes;
                    int length = Convert.ToInt32(fuCierreAdjuntos.FileContent.Length);
                    extension = System.IO.Path.GetExtension(fuCierreAdjuntos.FileName).ToLower().ToString().Trim();

                    clsAcmCierreAdjuntosBLL blCierreAdjuntos = new clsAcmCierreAdjuntosBLL();
                    string strErrMsg = string.Empty;

                    
                    clsAcmCierreAdjuntos objAdjuntos = new clsAcmCierreAdjuntos();
                        objAdjuntos.strnombreArchivo = nombreArchivo;
                        objAdjuntos.strpathFile = pathFile;
                        objAdjuntos.strextension = extension;
                        objAdjuntos.btArchivo = archivo;
                        objAdjuntos.intIdAcm = idAcm;

                        blCierreAdjuntos.mtdInsertarAdjuntosCierre(objAdjuntos, ref strErrMsg);
                        if (strErrMsg != string.Empty)
                        {
                            omb.ShowMessage(strErrMsg, 1);
                            return;
                        }
                        
                    /*bool res = AgregarFilaAdjunto(nombreArchivo, pathFile, extension, archivo);
                    
                    if (res == true)
                    {
                        dtAdjuntos = (DataTable)Session["dtAdjuntos"];
                        gvAdjuntosCierre.DataSource = dtAdjuntos;
                        gvAdjuntosCierre.DataBind();
                    }*/
                }
                else
                {
                    omb.ShowMessage("Solo archivos en formato pdf, word,excel, power point", 2, "Atención");
                    return;
                }
            }
            mtdLoadAdjuntos(idAcm);
            #endregion ArchivosAdjuntos
        }
        DataTable dtAdjuntos;
        private bool CrearTablaAdjunto()
        {
            try
            {
                dtAdjuntos = new DataTable();
                dtAdjuntos.Columns.Add("nombreArchivo");
                dtAdjuntos.Columns.Add("pathFile");
                dtAdjuntos.Columns.Add("extension");
                dtAdjuntos.Columns.Add("archivo");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AgregarFilaAdjunto(string nombreArchivo, string pathFile, string extension, byte[] archivo)
        {
            dtAdjuntos = (DataTable)Session["dtAdjuntos"];
            if (dtAdjuntos == null)
            {
                if (CrearTablaAdjunto() == false)
                    return false;
            }
            try
            {
                DataRow dr;
                dr = dtAdjuntos.NewRow();
                dr["nombreArchivo"] = nombreArchivo;
                dr["pathFile"] = pathFile;
                dr["extension"] = extension;
                dr["archivo"] = archivo;
                
                dtAdjuntos.Rows.Add(dr);
                Session["dtAdjuntos"] = dtAdjuntos;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void gvAdjuntosCierre_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName.Equals("Eliminar"))
            {
                dtAdjuntos = (DataTable)Session["dtAdjuntos"];

                dtAdjuntos.Rows.RemoveAt(index);

                Session["dtAdjuntos"] = dtAdjuntos;
                gvAdjuntosCierre.DataSource = Session["dtAdjuntos"];
                gvAdjuntosCierre.DataBind();
            }
        }

        protected void gvDocsAdjuntados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if(e.CommandName.Equals("Descargar"))
            {
                GridViewRow row = gvDocsAdjuntados.Rows[index];
                try
                {
                    var colsNoVisible = gvDocsAdjuntados.DataKeys[index].Values;
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", $"attachment; filename = {row.Cells[0].Text}");
                    //Set the content type as file extension type
                    Response.ContentType = $"{row.Cells[1].Text}";
                    //Write the file content
                    Response.BinaryWrite((byte[])colsNoVisible[1]);
                    Response.End();
                }
                catch (Exception ex)
                {
                    omb.ShowMessage($"Error al descargar el archivo {ex.Message}", 1, "Error");
                }
            }
            if (e.CommandName.Equals("Eliminar"))
            {
                int idAcm = (int)Session["IdAcm"];
                var colsNoVisible = gvDocsAdjuntados.DataKeys[index].Values;
                int idAdjunto = Convert.ToInt32(colsNoVisible[0].ToString());
                string strErrMsg = string.Empty;
                clsAcmCierreAdjuntosBLL blAdjuntos = new clsAcmCierreAdjuntosBLL();
                bool result = blAdjuntos.mtdDeleteAdjuntoCierre(idAcm, idAdjunto, ref strErrMsg);
                if (strErrMsg != string.Empty)
                    omb.ShowMessage(strErrMsg, 1);
                mtdLoadAdjuntos(idAcm);
            }
            }

        protected void gvAcm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}