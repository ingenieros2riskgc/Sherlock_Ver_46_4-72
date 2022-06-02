using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using ClosedXML.Excel;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class GestionEvaluacionCompetencia : System.Web.UI.UserControl
    {
        string IdFormulario = "4022";
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.IBprocess);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            scriptManager.RegisterPostBackControl(this.GVGestionCompetencias);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    PagIndex = 0;
                    mtdStartLoad();
                }
            }
        }
        private void mtdStartLoad()
        {
            string strErrMsg = string.Empty;
            mtdLoadGridEvaluacionCompetencia();
            if (!mtdLoadInfoGridEvaluacionCompetencias(ref strErrMsg))
                omb.ShowMessage("No hay datos para cargar", 2, "Atención");
            mtdInicializarValores();
            mtdHabilitarCampos(1);
            if (!mtdCargarDDLs(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            PopulateTreeView();
            BodyGridGEC.Visible = true;
        }
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridCompetencias;
        private int rowGridCompetencias;
        private int pagIndexCompetencias;

        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }

        private int PagIndex
        {
            get
            {
                pagIndex = (int)ViewState["pagIndex"];
                return pagIndex;
            }
            set
            {
                pagIndex = value;
                ViewState["pagIndex"] = pagIndex;
            }
        }
        private DataTable InfoGridCompetencias
        {
            get
            {
                infoGridCompetencias = (DataTable)ViewState["infoGridCompetencias"];
                return infoGridCompetencias;
            }
            set
            {
                infoGridCompetencias = value;
                ViewState["infoGridCompetencias"] = infoGridCompetencias;
            }
        }

        private int RowGridCompetencias
        {
            get
            {
                rowGridCompetencias = (int)ViewState["rowGridCompetencias"];
                return rowGridCompetencias;
            }
            set
            {
                rowGridCompetencias = value;
                ViewState["rowGridCompetencias"] = rowGridCompetencias;
            }
        }

        private int PagIndexCompetencias
        {
            get
            {
                pagIndexCompetencias = (int)ViewState["pagIndexCompetencias"];
                return pagIndexCompetencias;
            }
            set
            {
                pagIndexCompetencias = value;
                ViewState["pagIndexCompetencias"] = pagIndexCompetencias;
            }
        }
        private DataTable infoGridArchivos;
        private DataTable InfoGridArchivos
        {
            get
            {
                infoGridArchivos = (DataTable)ViewState["infoGridArchivos"];
                return infoGridArchivos;
            }
            set
            {
                infoGridArchivos = value;
                ViewState["infoGridArchivos"] = infoGridArchivos;
            }
        }
        private int rowGridArchivos;
        private int RowGridArchivos
        {
            get
            {
                rowGridArchivos = (int)ViewState["rowGridArchivos"];
                return rowGridArchivos;
            }
            set
            {
                rowGridArchivos = value;
                ViewState["rowGridArchivos"] = rowGridArchivos;
            }
        }
        #endregion
        #region GridViewEventos
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    GridViewRow row = GVevaluacionCompetencias.Rows[RowGrid];
                    var colsNoVisible = GVevaluacionCompetencias.DataKeys[RowGrid].Values;
                    txtId.Text = row.Cells[0].Text;
                    int IdEvaluacionCompetencias = Convert.ToInt32(row.Cells[0].Text);
                    txtFecha.Text = row.Cells[5].Text;
                    TXfecharegistro.Text = row.Cells[5].Text;
                    TXfecharegistro.Enabled = false;
                    tbxUsuarioCreacion.Text = colsNoVisible[1].ToString().Trim();
                    tbxUsuarioCreacion.Enabled = false;
                    txtNombreEva.Text = ((Label)row.FindControl("NombreEvaluado")).Text;
                    txtNombreEva.Enabled = false;
                    tbxResponsable.Text = row.Cells[3].Text;
                    tbxResponsable.Enabled = false;
                    TXjefe.Text = ((Label)row.FindControl("JefeInmediato")).Text;
                    TXjefe.Enabled = false;
                    ddlCadenaValor.Enabled = false;
                    ddlMacroproceso.Enabled = false;
                    ddlProceso.Enabled = false;
                    ddlSubproceso.Enabled = false;
                    imgDependencia4.Visible = false;
                    Session["proceso"] = row.Cells[7].Text;
                    mtdShowModificar(IdEvaluacionCompetencias, RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    Dbutton.Visible = true;
                    TRcancel.Visible = false;
                    TRcancelButton.Visible = false;
                    break;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVevaluacionCompetencias.PageIndex = PagIndex;
            GVevaluacionCompetencias.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridEvaluacionCompetencias(ref strErrMsg);
        }
        #endregion
        #region ButtonEventos
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            btnInsertarNuevo.Visible = false;
            BodyGridGEC.Visible = false;
            BodyFormGEC.Visible = true;
            string strErrMsg = string.Empty;
            mtdLoadGridGestionCompetencia();
            if (!mtdGetGestionCompetencia(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            TituloCompetenciasEva.Visible = true;
            GVgestionCompetencia.Visible = true;
            if (mtdCargarCompetencias(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            GVGestionCompetencias.Visible = true;
            DVprocess.Visible = true;
            GVgestionCompetencia.Visible = true;
            TRcancel.Visible = true;
            TRcancelButton.Visible = true;
            imgDependencia4.Visible = true;
            txtNombreEva.Visible = true;
            tbxResponsable.Visible = true;
            ddlCadenaValor.Enabled = true;
            ddlMacroproceso.Enabled = true;
            ddlProceso.Enabled = true;
            ddlSubproceso.Enabled = true;
            TXjefe.Enabled = true;
            IBinsertGVC.Visible = true;
            //TRcancel.Visible = true;
            //TRcancelButton.Visible = true;
        }
        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdInsertarValorEvaluacionCompetencia(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                mtdStartLoad();
                BodyGridGEC.Visible = true;

            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
                mtdRestFields();
                mtdStartLoad();
                BodyGridGEC.Visible = true;
            }
        }
        private void mtdCargarArchivo(int IdEvaluacion)
        {
            string strErrMsg = string.Empty;
            loadGridArchivos();
            loadInfoArchivos(ref strErrMsg, ref IdEvaluacion);
        }
        private void loadGridArchivos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("extension", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivos = grid;
            GridView2.DataSource = InfoGridArchivos;
            GridView2.DataBind();
        }

        private void loadInfoArchivos(ref string strErrMsg, ref int IdRegistro)
        {
            DataTable dtInfo = new DataTable();
            bool booResult = false;
            clsDtValorEvaluacionCompetencias cDtCrlVersion = new clsDtValorEvaluacionCompetencias();
            booResult = cDtCrlVersion.mtdConsultarDocumento(ref dtInfo, ref strErrMsg, ref IdRegistro);
            if (booResult == true)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                        dtInfo.Rows[rows]["extension"].ToString().Trim(),
                        dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                        });
                }
                GridView2.DataSource = InfoGridArchivos;
                GridView2.DataBind();
            }
        }
        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdActulizarObservaciones(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                BodyGridGEC.Visible = true;
                mtdStartLoad();
            }
        }
        protected void IBprocess_Click(object sender, ImageClickEventArgs e)
        {
            mtdLoadTotalCompetencias();
            Total.Visible = true;
            IBupdateGVC.Visible = false;
            DVCompetencias.Visible = true;
            //omb.ShowMessage(mensaje, 2, "Atencion");
            TRcancel.Visible = false;
            TRcancelButton.Visible = false;
        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdRestFields();
            BodyGridGEC.Visible = true;
            mtdStartLoad();
        }
        #endregion
        #region DLLeventos
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();

            if (!mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();

            if (!mtdLoadDDLProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (ddlProceso.SelectedValue == "0")
                rfvProceso.Enabled = false;
            else
                rfvProceso.Enabled = true;

            ddlSubproceso.Items.Clear();

            if (!mtdLoadDDLSubproceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlSubproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProceso.SelectedValue == "0")
                rfvSubproceso.Enabled = false;
            else
                rfvSubproceso.Enabled = true;
        }
        #endregion
        #region Metodos
        private void mtdInicializarValores()
        {
            /*PagIndex1 = 0;
            PagIndex2 = 0;*/
            txtNombreEva.Text = "";
            //TXfecharegistro.Text = ""+DateTime.Now;
            tbxResponsable.Text = "";
            TXjefe.Text = "";
        }
        private void mtdRestFields() 
        {
            BodyFormGEC.Visible = false;
            TituloCompetenciasEva.Visible = false;
            GVgestionCompetencia.Visible = false;
            GVGestionCompetenciasEvaVal.Visible = false;
            GVprintEvaComp.Visible = false;
            txtId.Text = "";
            txtNombreEva.Text = "";
            txtNombreEva.Enabled = true;
            tbxResponsable.Text = "";
            TXjefe.Text = "";
            TXobservaciones.Text = "";
            TXfechanext.Text = "";
            txtFecha.Text = "";
            tbxUsuarioCreacion.Text = "";
            TXfecharegistro.Text = "";
            TXfecharegistro.Enabled = true;
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.ClearSelection();
            ddlMacroproceso.Items.Clear();
            ddlProceso.ClearSelection();
            ddlProceso.Items.Clear();
            ddlSubproceso.ClearSelection();
            ddlSubproceso.Items.Clear();
            DVCompetencias.Visible = false;
            DVprocess.Visible = false;
            Dbutton.Visible = false;
            btnInsertarNuevo.Visible = true;

        }
        private void mtdHabilitarCampos(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1:
                    break;
            }
        }
        /// <summary>
        /// Realiza la insercion del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        private bool mtdInsertarValorEvaluacionCompetencia(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionCompetencia objEvaCompInd = new clsEvaluacionCompetencia();
            clsValorEvaluacionCompetenciasBLL cValorEvaluacionInd = new clsValorEvaluacionCompetenciasBLL();
            clsValorEvaluacionCompetencia objeValorEvaluacionCompetencia = new clsValorEvaluacionCompetencia();
            clsObservacionProveedor objObservacionEvaluacion = new clsObservacionProveedor();
            #endregion

            #region Ins EvaluacionCompetencia
            objEvaCompInd.strNombreEvaluado = txtNombreEva.Text;
            int Dependencia = Convert.ToInt32(lblIdDependencia4.Text);
            objEvaCompInd.intCargoResponsable = Dependencia;
            objEvaCompInd.strJefeInmediato = TXjefe.Text;
            int IdTipoProceso = 0;
            int IdProceso = 0;
            //objEvaCompInd.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
            if (ddlSubproceso.SelectedValue != "" && ddlSubproceso.SelectedValue != "0")
            {
                IdTipoProceso = 3;
                objEvaCompInd.intIdMacroProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                IdProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
            }
            else
            {
                if (ddlProceso.SelectedValue != "" && ddlProceso.SelectedValue != "0")
                {
                    IdTipoProceso = 2;
                    objEvaCompInd.intIdMacroProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                    IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue != "" && ddlMacroproceso.SelectedValue != "0")
                    {
                        IdTipoProceso = 1;
                        objEvaCompInd.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                        IdProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                    }
                }
            }
            objEvaCompInd.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString().Trim());
            objEvaCompInd.dtFechaRegistro = TXfecharegistro.Text;
            booResult = cValorEvaluacionInd.mtdInsertarEvaluacionCompetencia(objEvaCompInd, ref strErrMsg);
            int LastIdEC = cValorEvaluacionInd.mtdLastIdEvaluacionCompetencias(ref strErrMsg);
            booResult = cValorEvaluacionInd.mtdInsertarTipoProcesoEvaluacionCompetencia(IdTipoProceso, IdProceso, LastIdEC, ref strErrMsg);
            //int LastIdEC = 0;
            #endregion
            #region ValorCompetencia
            objeValorEvaluacionCompetencia.intIdEvaluacionCompetencia = 0;
            objeValorEvaluacionCompetencia.intIdValorEvaluacionCompetencia = LastIdEC;
            for (int rowIndex = 0; rowIndex < GVGestionCompetencias.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVGestionCompetencias.Rows[rowIndex];
                //GridViewRow previousRow = GVGestionCompetencias.Rows[rowIndex];
                string nombreCompetencia = ((Label)row.FindControl("NombreCompetencia")).Text;
                int puntajeAsignado = Convert.ToInt32(((TextBox)row.FindControl("TXpuntajeAsignado")).Text);
                string criterio = ((Label)row.FindControl("DescripcionCompetencia")).Text;

                objeValorEvaluacionCompetencia.strCompetencia = nombreCompetencia;
                objeValorEvaluacionCompetencia.strCriterio = criterio;
                objeValorEvaluacionCompetencia.intPuntajeAsignado = puntajeAsignado;

                booResult = cValorEvaluacionInd.mtdInsertarVECtable(objeValorEvaluacionCompetencia, ref strErrMsg);
            }
            #endregion
            #region Observaciones
            objObservacionEvaluacion.intIdEvaluacionProveedor = LastIdEC;
            objObservacionEvaluacion.strDescripcion = TXobservaciones.Text;
            objObservacionEvaluacion.dtFechaProximaEvaluacion = TXfechanext.Text;
            objObservacionEvaluacion.dtFechaRegistro = DateTime.Now;
            objObservacionEvaluacion.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            booResult = cValorEvaluacionInd.mtdInsertarObservaciones(objObservacionEvaluacion, ref strErrMsg);
            #endregion
            #region File
            string pathFile = string.Empty;
            if (fuArchivoPerfil.HasFile)
            {
                if (System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".pdf" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".docx" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".doc" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".xlsx")
                {
                    pathFile = "EvaComp." + fuArchivoPerfil.FileName;
                    Byte[] archivo = fuArchivoPerfil.FileBytes;
                    int length = Convert.ToInt32(fuArchivoPerfil.FileContent.Length);
                    string extension = System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim();
                    saveFile("EvaComp." + fuArchivoPerfil.FileName, length, archivo, LastIdEC, extension);
                    //mtdCargarArchivo();
                }
                else
                    omb.ShowMessage("Archivo sin guardar. Solo archivos en formato pdf,word,excel", 2, "Atención");
            }
            #endregion
            if (booResult == true)
            {
                strErrMsg = "Evaluación registrada exitosamente";
            }
            return booResult;
        }
        private void saveFile(string NombreArchivo, int Length, byte[] archivo, int IdRegistro, string extension)
        {
            string strErrMsg = string.Empty;
            clsValorEvaluacionCompetenciasBLL cVersion = new clsValorEvaluacionCompetenciasBLL();

            if (!cVersion.mtdInsertarArchivo(NombreArchivo, Length, archivo, IdRegistro, ref strErrMsg, extension))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        public Boolean mtdActulizarObservaciones(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionCompetencia objEvaCompInd = new clsEvaluacionCompetencia();
            clsValorEvaluacionCompetenciasBLL cValorEvaluacionInd = new clsValorEvaluacionCompetenciasBLL();
            clsValorEvaluacionCompetencia objeValorEvaluacionCompetencia = new clsValorEvaluacionCompetencia();
            clsObservacionProveedor objObservacionEvaluacion = new clsObservacionProveedor();
            #endregion
            #region Observaciones
            objObservacionEvaluacion.intIdEvaluacionProveedor = Convert.ToInt32(txtId.Text);
            objObservacionEvaluacion.strDescripcion = TXobservaciones.Text;
            objObservacionEvaluacion.dtFechaProximaEvaluacion = TXfechanext.Text;
            objObservacionEvaluacion.dtFechaRegistro = Convert.ToDateTime(TXfecharegistro.Text);
            objObservacionEvaluacion.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            booResult = cValorEvaluacionInd.mtdActualizacionObservaciones(objObservacionEvaluacion, ref strErrMsg);
            #endregion
            if (booResult == true)
            {
                strErrMsg = "Evaluación de competencias actualizada exitosamente";
            }
            return booResult;
        }
        
        #region Gridview Competencias
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarCompetencias(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridCompetencias();
            mtdLoadInfoGridCompetencias(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;
            
            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridCompetencias()
        {
            DataTable gridCompetencias = new DataTable();

            gridCompetencias.Columns.Add("intId", typeof(string));
            gridCompetencias.Columns.Add("strNombre", typeof(string));
            gridCompetencias.Columns.Add("intPonderacion", typeof(string));
            gridCompetencias.Columns.Add("intIdUsuario", typeof(string));
            gridCompetencias.Columns.Add("strNombreUsuario", typeof(string));
            gridCompetencias.Columns.Add("dtFechaRegistro", typeof(string));

            GVcompentecia.DataSource = gridCompetencias;
            GVcompentecia.DataBind();
            InfoGridCompetencias = gridCompetencias;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la Competencia al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridCompetencias(ref string strErrMsg)
        {
            List<clsCompetencia> lstCompetencia = new List<clsCompetencia>();
            clsCompetenciaBLL cCompetencia = new clsCompetenciaBLL();

            lstCompetencia = cCompetencia.mtdConsultarCompetencia(ref strErrMsg);

            if (lstCompetencia != null)
            {
                mtdLoadInfoGridCompetencia(lstCompetencia);
                GVcompentecia.DataSource = lstCompetencia;
                GVcompentecia.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con las Actividades</param>
        private void mtdLoadInfoGridCompetencia(List<clsCompetencia> lstCompetencia)
        {
            foreach (clsCompetencia objCompetencia in lstCompetencia)
            {
                InfoGridCompetencias.Rows.Add(new Object[] {
                    objCompetencia.intId.ToString().Trim(),
                    objCompetencia.strNombre.ToString().Trim(),
                    objCompetencia.intPonderacion.ToString().Trim(),
                    objCompetencia.intIdUsuario.ToString().Trim(),
                    objCompetencia.strNombreUsuario.ToString().Trim(),
                    objCompetencia.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        private void mtdShowModificar(int IdEvaluacionCompetencia, int row)
        {
            string strErrMgs = String.Empty;
            var colsNoVisible = GVevaluacionCompetencias.DataKeys[row].Values;
            mtdCargarDDLs(ref strErrMgs);
            int IdTipoProceso = Convert.ToInt32(colsNoVisible[2].ToString());
            #region Procesos
            //Se DEBE CARGAR INFORMACION DEBE PROCESOS DEBE ACUERDO A LA INFORMACION DEL GRID
            tbxProcIndica.Text = colsNoVisible[0].ToString();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador(0, 0, Convert.ToInt32(colsNoVisible[0].ToString()), 0, string.Empty);
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcesoEvaCompetencia(IdEvaluacionCompetencia, ref strErrMgs);

            switch (objProcesos[0].ToString())
            {
                case "M":
                    clsMacroproceso objMP = (clsMacroproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objMP.intIdCadenaDeValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMgs);
                    ddlMacroproceso.SelectedValue = objMP.intId.ToString();
                    mtdLoadDDLProceso(ref strErrMgs);
                    ddlProceso.SelectedValue = "0";
                    break;
                    case "P":
                        clsProceso objP = (clsProceso)objProcesos[1];
                        ddlCadenaValor.SelectedValue = objP.intIdCadenaValor.ToString();
                        mtdLoadDDLMacroProceso(ref strErrMgs);
                        ddlMacroproceso.SelectedValue = objP.intIdMacroProceso.ToString();
                        mtdLoadDDLProceso(ref strErrMgs);
                        ddlProceso.SelectedValue = objP.intId.ToString();
                        mtdLoadDDLSubproceso(ref strErrMgs);
                        ddlSubproceso.SelectedValue = "0";
                        break;
                    case "S":
                        clsSubproceso objSP = (clsSubproceso)objProcesos[1];
                        ddlCadenaValor.SelectedValue = objSP.intIdCadenaValor.ToString();
                        mtdLoadDDLMacroProceso(ref strErrMgs);
                        ddlMacroproceso.SelectedValue = objSP.intIdMacroProceso.ToString();
                        mtdLoadDDLProceso(ref strErrMgs);
                        ddlProceso.SelectedValue = objSP.intIdProceso.ToString();
                        mtdLoadDDLSubproceso(ref strErrMgs);
                        ddlSubproceso.SelectedValue = objSP.intId.ToString();
                        break;
            }
            #endregion
            BodyGridGEC.Visible = false;
            BodyFormGEC.Visible = true;
            TituloCompetenciasEva.Visible = true;
            GVgestionCompetencia.Visible = true;
            mtdCargarCompetencias(ref strErrMgs);
            mtdLoadValorEvaluacion(ref IdEvaluacionCompetencia);
            mtdLoadObservaciones(ref IdEvaluacionCompetencia, ref strErrMgs);
            mtdCargarArchivo(IdEvaluacionCompetencia);
        }
        private void mtdLoadObservaciones(ref int IdEvaluacionCompetencia, ref string strErrMsg)
        {
            bool booResult = false;
            List<clsObservacionProveedor> lstObservacionesCompetencia = new List<clsObservacionProveedor>();
            clsValorEvaluacionCompetenciasBLL cValorEvaluacionInd = new clsValorEvaluacionCompetenciasBLL();
            clsObservacionProveedor objObservacionesCompOut = new clsObservacionProveedor();
            lstObservacionesCompetencia = cValorEvaluacionInd.mtdConsultarObservaciones(ref IdEvaluacionCompetencia, ref objObservacionesCompOut, ref strErrMsg);

            if (lstObservacionesCompetencia != null)
            {
                mtdLoadInfoObservacionesCompetencia(lstObservacionesCompetencia);
                
                booResult = true;
            }
        }
        private void mtdLoadInfoObservacionesCompetencia(List<clsObservacionProveedor> lstObservacionesCompetencia)
        {
            foreach (clsObservacionProveedor objCompetencia in lstObservacionesCompetencia)
            {
                TXobservaciones.Text = objCompetencia.strDescripcion;
                TXfechanext.Text = objCompetencia.dtFechaProximaEvaluacion;
            }
        }
        private void mtdLoadValorEvaluacion(ref int IdEvaluacionCompetencia)
        {
            string strErrMgs = String.Empty;
            mtdLoadGridGestionCompetenciaVal();
            GVGestionCompetencias.Visible = false;
            GVGestionCompetenciasEvaVal.Visible = true;
            mtdLoadGridGestionCompetenciaVal();
            mtdGetGestionCompetenciaVal(ref IdEvaluacionCompetencia, ref strErrMgs);
            mtdLoadTotalCompetenciasMod();
            Total.Visible = true;
            DVCompetencias.Visible = true;
        }
        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView4.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT PJO.IdHijo, PJO.IdPadre, PJO.NombreHijo, PDJ.NombreResponsable, PDJ.CorreoResponsable " +
                "FROM Parametrizacion.JerarquiaOrganizacional PJO LEFT JOIN Parametrizacion.DetalleJerarquiaOrg PDJ ON PJO.idHijo = PDJ.idHijo";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";

            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                TreeView4.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView4_SelectedNodeChanged(object sender, EventArgs e)
        {
            tbxResponsable.Text = TreeView4.SelectedNode.Text;
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview
        #endregion
        #region Cargas
        public void mtdLoadTotalCompetencias()
        {
            string mensaje = String.Empty;
            int valorCompetencia = 0;
            string strErrMsg = String.Empty;
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            clsCompetenciaBLL cCompetenciaBll = new clsCompetenciaBLL();
            lstCalificaciones = cCompetenciaBll.mtdGetCalificaciones(ref strErrMsg);

            for (int rowIndex = 0; rowIndex < GVGestionCompetencias.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVGestionCompetencias.Rows[rowIndex];
                //GridViewRow previousRow = GVGestionCompetencias.Rows[rowIndex];
                string nombreCompetencia = ((Label)row.FindControl("NombreCompetencia")).Text;
                int puntajeAsignado = Convert.ToInt32(((TextBox)row.FindControl("TXpuntajeAsignado")).Text);
                for (int j = 0; j < GVcompentecia.Rows.Count; j++)
                {
                    GridViewRow rowCompetencia = GVcompentecia.Rows[j];
                    string competencia = ((Label)rowCompetencia.FindControl("Nombre")).Text;
                    if (nombreCompetencia == competencia)
                    {
                        string valor = ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignadoTotal")).Text;
                        if (valor != "")
                        {
                            valorCompetencia = Convert.ToInt32(valor) + puntajeAsignado;
                            ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignadoTotal")).Text = "" + valorCompetencia;
                        }
                        else
                        {
                            valorCompetencia = 0 + puntajeAsignado;
                            ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignadoTotal")).Text = "" + valorCompetencia;
                        }

                    }
                }
            }
            decimal TotalCompetencia = 0;
            for (int j = 0; j < GVcompentecia.Rows.Count; j++)
            {
                GridViewRow rowCompetencia = GVcompentecia.Rows[j];
                decimal ponderacion = Convert.ToDecimal(((Label)rowCompetencia.FindControl("ponderacion")).Text);
                string valor = ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignadoTotal")).Text;
                decimal porcentaje = ponderacion / 100;
                if (valor != "")
                {
                    decimal valorTotal = porcentaje * Convert.ToInt32(valor);
                    ((Label)rowCompetencia.FindControl("total")).Text = "" + valorTotal;
                    TotalCompetencia += valorTotal;
                }
            }
            LvalorTotalCompetencia.Text = "" + TotalCompetencia;
            mtdLoadCalificaciones(lstCalificaciones,TotalCompetencia);
        }
        private void mtdLoadCalificaciones(List<clsCalificacionEvaluacion> lstCalificaciones, decimal TotalCompetencia)
        {
            if (lstCalificaciones != null) {
                foreach (clsCalificacionEvaluacion objCalificacion in lstCalificaciones)
                {
                    if (TotalCompetencia >= objCalificacion.intValorMinimo && TotalCompetencia <= objCalificacion.intValorMaximo)
                    {
                        LvalorCalificacion.Text = objCalificacion.strDescripcion;
                        LvalorCalificacion.Visible = true;
                    }
                }
            }
        }
        private String remplazarCaracteres(String cadena)
        {
            return cadena.Replace("'", "").Replace(",", "").Replace(".", "").Replace(";", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(":", "").Replace("" + (char)34, "").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("Ñ", "N").Replace("ñ", "n").Replace("" + (char)13, "").Replace("" + (char)10, "").Replace("´", "").Replace("|", String.Empty).ToUpper();
        }
        public void mtdLoadTotalCompetenciasMod()
        {
            string mensaje = String.Empty;
            int valorCompetencia = 0;
            string strErrMsg = String.Empty;
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            clsCompetenciaBLL cCompetenciaBll = new clsCompetenciaBLL();
            lstCalificaciones = cCompetenciaBll.mtdGetCalificaciones(ref strErrMsg);

            for (int rowIndex = 0; rowIndex < GVGestionCompetenciasEvaVal.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVGestionCompetenciasEvaVal.Rows[rowIndex];
                //GridViewRow previousRow = GVGestionCompetencias.Rows[rowIndex];
                string nombreCompetencia = ((Label)row.FindControl("NombreCompetencia")).Text;
                int puntajeAsignado = Convert.ToInt32(((Label)row.FindControl("LpuntajeAsignado")).Text);
                for (int j = 0; j < GVcompentecia.Rows.Count; j++)
                {
                    GridViewRow rowCompetencia = GVcompentecia.Rows[j];
                    string competencia = ((Label)rowCompetencia.FindControl("Nombre")).Text;
                    if (nombreCompetencia == competencia)
                    {
                        string valor = ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignadoTotal")).Text;
                        if (valor != "")
                        {
                            valorCompetencia = Convert.ToInt32(valor) + puntajeAsignado;
                            ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignadoTotal")).Text = "" + valorCompetencia;
                        }
                        else
                        {
                            valorCompetencia = 0 + puntajeAsignado;
                            ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignadoTotal")).Text = "" + valorCompetencia;
                        }

                    }
                }
            }
            
            DataTable gridTotal = new DataTable();
            gridTotal.Columns.Add("intId");
            gridTotal.Columns.Add("strNombre");
            gridTotal.Columns.Add("intPuntajeTotal");
            gridTotal.Columns.Add("intPonderacion");
            gridTotal.Columns.Add("intTotal");
            DataRow rowData;
            decimal TotalCompetencia = 0;
            for (int j = 0; j < GVcompentecia.Rows.Count; j++)
            {
                GridViewRow rowCompetencia = GVcompentecia.Rows[j];
                
                string Codigo = rowCompetencia.Cells[0].Text;
                string nombreCompetencia = ((Label)rowCompetencia.FindControl("Nombre")).Text;
                decimal ponderacion = Convert.ToDecimal(((Label)rowCompetencia.FindControl("ponderacion")).Text);
                string valor = ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignadoTotal")).Text;
                if (valor == "")
                    valor = "0";
                decimal porcentaje = ponderacion / 100;
                decimal valorTotal = 0;
                if (valor != "")
                {
                     valorTotal = porcentaje * Convert.ToInt32(valor);
                    ((Label)rowCompetencia.FindControl("total")).Text = "" + valorTotal;
                    TotalCompetencia += valorTotal;
                }else
                    TotalCompetencia = 0;
                rowData = gridTotal.NewRow();
                rowData["intId"] = Codigo;
                rowData["strNombre"] = nombreCompetencia;
                rowData["intPuntajeTotal"] = valor;
                rowData["intPonderacion"] = ponderacion;
                rowData["intTotal"] = valorTotal;

                gridTotal.Rows.Add(rowData);
            }
            GVtotal.DataSource = gridTotal;
            GVtotal.DataBind();
            LvalorTotalCompetencia.Text = "" + TotalCompetencia;
            mtdLoadCalificaciones(lstCalificaciones, TotalCompetencia);
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridGestionCompetencia()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strNombreCompetencia", typeof(string));
            grid.Columns.Add("strDescripcionCompetencia", typeof(string));

            GVGestionCompetencias.DataSource = grid;
            //GVGestionCompetencias.PageIndex = pagIndex;
            GVGestionCompetencias.DataBind();
            InfoGrid = grid;
        }
        private void mtdLoadGridGestionCompetenciaVal()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strNombreCompetencia", typeof(string));
            grid.Columns.Add("strDescripcionCompetencia", typeof(string));
            grid.Columns.Add("intPuntajeAsignado", typeof(string));
            GVGestionCompetenciasEvaVal.DataSource = grid;
            GVGestionCompetenciasEvaVal.DataBind();
            InfoGrid = grid;

            GVprintEvaComp.DataSource = grid;
            GVprintEvaComp.DataBind();
        }
        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdGetGestionCompetencia(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsGestionEvaluacionCompetencias> lstGestionCompetencia = new List<clsGestionEvaluacionCompetencias>();
            clsCompetenciaBLL cCompetenciaBll = new clsCompetenciaBLL();
            lstGestionCompetencia = cCompetenciaBll.mtdGetCompetencia(ref strErrMsg);
            if (lstGestionCompetencia != null)
            {
                mtdLoadInfoGridGestionCompetencia(lstGestionCompetencia);
                GVGestionCompetencias.DataSource = lstGestionCompetencia;
                GVevaluacionCompetencias.PageIndex = pagIndex;
                GVGestionCompetencias.DataBind();
                
                booResult = true;
            }
            return booResult;
        }
        private bool mtdGetGestionCompetenciaVal(ref int IdEvaluacionCompetencia, ref string strErrMsg)
        {
            bool booResult = false;
            List<clsGestionEvaluacionCompetencias> lstGestionCompetencia = new List<clsGestionEvaluacionCompetencias>();
            clsCompetenciaBLL cCompetenciaBll = new clsCompetenciaBLL();
            lstGestionCompetencia = cCompetenciaBll.mtdGetCompetenciaEvaVal(ref IdEvaluacionCompetencia, ref strErrMsg);
            if (lstGestionCompetencia != null)
            {
                mtdLoadInfoGridGestionCompetenciaVal(lstGestionCompetencia);
                GVGestionCompetenciasEvaVal.DataSource = lstGestionCompetencia;
                //GVevaluacionCompetencias.PageIndex = PageI;
                GVGestionCompetenciasEvaVal.DataBind();
                GVprintEvaComp.DataSource = lstGestionCompetencia;
                GVprintEvaComp.DataBind();
                booResult = true;
            }
            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadInfoGridGestionCompetencia(List<clsGestionEvaluacionCompetencias> lstGestionCompetencia)
        {
            foreach (clsGestionEvaluacionCompetencias objGestionCompetencia in lstGestionCompetencia)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objGestionCompetencia.strNombreCompetencia.ToString().Trim(),
                    objGestionCompetencia.strDescripcionCompetencia.ToString().Trim()
                    });
            }
        }
        private void mtdLoadInfoGridGestionCompetenciaVal(List<clsGestionEvaluacionCompetencias> lstGestionCompetencia)
        {
            foreach (clsGestionEvaluacionCompetencias objGestionCompetencia in lstGestionCompetencia)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objGestionCompetencia.strDescripcionCompetencia.ToString().Trim(),
                    objGestionCompetencia.strNombreCompetencia.ToString().Trim(),
                    objGestionCompetencia.intPuntajeAsignado.ToString().Trim()
                    });

            }
        }
        private bool mtdCargarDDLs(ref string strErrMsg)
        {
            bool booResult = false;

            booResult = mtdLoadDDLCadenaValor(ref strErrMsg);
            //if (booResult)
            //    booResult = mtdLoadDDLMacroProceso(ref strErrMsg);

            return booResult;
        }

        private bool mtdLoadDDLCadenaValor(ref string strErrMsg)
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
                        booResult = true;
                    }
                    else
                        booResult = false;
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

        private bool mtdLoadDDLMacroProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCadenaValor objCadenaValor = new clsCadenaValor();
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                objCadenaValor = new clsCadenaValor(Convert.ToInt32(ddlCadenaValor.SelectedValue.ToString().Trim()), string.Empty, true, 0, string.Empty, string.Empty);
                lstMacroproceso = cMacroproceso.mtdConsultarMacroproceso(true, objCadenaValor, ref strErrMsg);
                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstMacroproceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsMacroproceso objMacroproceso in lstMacroproceso)
                        {
                            ddlMacroproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objMacroproceso.strNombreMacroproceso, objMacroproceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

        private bool mtdLoadDDLProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {
                objMProceso = new clsMacroproceso(Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()), string.Empty, string.Empty, string.Empty,
                    true, 0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty);
                lstProceso = cProceso.mtdConsultarProceso(true, objMProceso, ref strErrMsg);
                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstProceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsProceso objProceso in lstProceso)
                        {
                            ddlProceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objProceso.strNombreProceso, objProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

        private bool mtdLoadDDLSubproceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsProceso objProceso = new clsProceso();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();
            #endregion Vars

            try
            {
                objProceso = new clsProceso(Convert.ToInt32(ddlProceso.SelectedValue.ToString().Trim()),
                    0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty);
                lstSubproceso = cSubproceso.mtdConsultarSubProceso(true, objProceso, ref strErrMsg);
                ddlSubproceso.Items.Clear();
                ddlSubproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstSubproceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsSubproceso objSubProceso in lstSubproceso)
                        {
                            ddlSubproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objSubProceso.strNombreSubproceso, objSubProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Subprocesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridEvaluacionCompetencia()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreEvaluado", typeof(string));
            grid.Columns.Add("strNombreCargo", typeof(string));
            grid.Columns.Add("strJefeInmediato", typeof(string));
            grid.Columns.Add("intIdMacroProceso", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GVevaluacionCompetencias.DataSource = grid;
            GVevaluacionCompetencias.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridEvaluacionCompetencias(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsEvaluacionCompetencia> lstEvaluacionCompetencia = new List<clsEvaluacionCompetencia>();
            clsValorEvaluacionCompetenciasBLL cValorEvaluacionInd = new clsValorEvaluacionCompetenciasBLL();
            clsEvaluacionCompetencia objEvaCompOut = new clsEvaluacionCompetencia();
            lstEvaluacionCompetencia = cValorEvaluacionInd.mtdConsultarEvaluacionCompetencias(ref objEvaCompOut, ref strErrMsg);

            if (lstEvaluacionCompetencia != null)
            {
                mtdLoadInfoGridEvaluacionCompetencias(lstEvaluacionCompetencia);
                GVevaluacionCompetencias.DataSource = lstEvaluacionCompetencia;
                GVevaluacionCompetencias.PageIndex = pagIndex;
                GVevaluacionCompetencias.DataBind();
                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadInfoGridEvaluacionCompetencias(List<clsEvaluacionCompetencia> lstEvaluacionCompetencia)
        {
            string strErrMsg = String.Empty;
            clsValorEvaluacionCompetenciasBLL cValorEvaluacionInd = new clsValorEvaluacionCompetenciasBLL();
            
            foreach (clsEvaluacionCompetencia objEvaComp in lstEvaluacionCompetencia)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strNombreEvaluado.ToString().Trim(),
                    objEvaComp.strNombreCargo.ToString().Trim(),
                    objEvaComp.strJefeInmediato.ToString().Trim(),
                    objEvaComp.intIdMacroProceso.ToString().Trim(),
                    objEvaComp.strNombreProceso.ToString().Trim(),
                    //objEvaComp.intIdUsuario.ToString().Trim(),
                    objEvaComp.strUsuario.ToString().Trim(),
                    objEvaComp.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }

        
        
        #endregion

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteEvaluacionCompetencias_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }

        private void mtdExportPdf()
        {
            #region Competencias
            PdfPTable pdfpTable = new PdfPTable(GVGestionCompetenciasEvaVal.HeaderRow.Cells.Count);

            foreach (TableCell headerCell in GVGestionCompetenciasEvaVal.HeaderRow.Cells)
            {
                Font font = new Font();
                font.Color = new Color(GVGestionCompetenciasEvaVal.HeaderStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                pdfCell.BackgroundColor = new Color(GVGestionCompetenciasEvaVal.HeaderStyle.BackColor);
                pdfpTable.AddCell(pdfCell);
            }

            foreach (GridViewRow GridViewRow in GVprintEvaComp.Rows)
            {
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    Font font = new Font();
                    font.Color = new Color(GVprintEvaComp.RowStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                    pdfCell.BackgroundColor = new Color(GVprintEvaComp.RowStyle.BackColor);
                    pdfpTable.AddCell(pdfCell);
                }
            }
            #endregion
            #region Totales
            PdfPTable pdfpTableTotales = new PdfPTable(GVtotal.HeaderRow.Cells.Count);

            foreach (TableCell headerCell in GVtotal.HeaderRow.Cells)
            {
                Font font = new Font();
                font.Color = new Color(GVtotal.HeaderStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                pdfCell.BackgroundColor = new Color(GVtotal.HeaderStyle.BackColor);
                pdfpTableTotales.AddCell(pdfCell);
            }
            
            foreach (GridViewRow GridViewRow in GVtotal.Rows)
            {
                string intId = ((Label)GridViewRow.FindControl("intId")).Text;
                string Nombre = ((Label)GridViewRow.FindControl("Nombre")).Text;
                string intPuntajeTotal = ((Label)GridViewRow.FindControl("intPuntajeTotal")).Text;
                string intPonderacion = ((Label)GridViewRow.FindControl("intPonderacion")).Text;
                string intTotal = ((Label)GridViewRow.FindControl("intTotal")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    /*if (iteracion != 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }
                    else
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(nombre));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }*/
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intId));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(Nombre));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intPuntajeTotal));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intPonderacion));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }
                    if (iteracion == 4)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intTotal));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }
                    
                    iteracion++;
                }
                
            }
            #endregion
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4, 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de Evaluacion de Competencias");
            //....header
            // we Add a Header that will show up on PAGE 1
            // Creamos la imagen y le ajustamos el tamaño
            string pathImg = Server.MapPath("~") + "Imagenes/Logos/Risk.png";
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(pathImg);
            pathImg = Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
            iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(pathImg);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            PdfPTable pdftblImage = new PdfPTable(2);
            PdfPCell pdfcellImage = new PdfPCell(imagen, true);
            pdfcellImage.FixedHeight = 40f;
            /*pdfcellImage.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcellImage.VerticalAlignment = Element.ALIGN_LEFT;*/
            pdfcellImage.Border = Rectangle.NO_BORDER;
            pdfcellImage.Border = Rectangle.NO_BORDER;
            /**/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();

            phHeader.Add(pdftblImage);
            //phHeader.Add(chnCompany);
            #region Tabla de Datos Principales
            Font font2 = new Font();
            font2.Color = new Color(GVGestionCompetencias.HeaderStyle.ForeColor);
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCell2 = new PdfPCell(new Phrase("Nombre y Apellidos de Evaluado:", font2));
            pdfCell2.BackgroundColor = new Color(GVGestionCompetencias.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCell2);
            pdfCell2 = new PdfPCell(new Phrase(txtNombreEva.Text));
            pdfTableData.AddCell(pdfCell2);
            pdfCell2 = new PdfPCell(new Phrase("Cargo:", font2));
            pdfCell2.BackgroundColor = new Color(GVGestionCompetencias.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCell2);
            pdfCell2 = new PdfPCell(new Phrase(tbxResponsable.Text));
            pdfTableData.AddCell(pdfCell2);
            pdfCell2 = new PdfPCell(new Phrase("Proceso:", font2));
            pdfCell2.BackgroundColor = new Color(GVGestionCompetencias.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCell2);
            string proceso = string.Empty;
            if (ddlSubproceso.SelectedValue != "" && ddlSubproceso.SelectedValue != "0")
            {
                proceso = ddlSubproceso.SelectedItem.Text;
            }
            else
            {
                if (ddlProceso.SelectedValue != "" && ddlProceso.SelectedValue != "0")
                {
                    proceso = ddlProceso.SelectedItem.Text;
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue != "" && ddlMacroproceso.SelectedValue != "0")
                    {
                        proceso = ddlMacroproceso.SelectedItem.Text;
                    }
                }
            }
            pdfCell2 = new PdfPCell(new Phrase(proceso));
            pdfTableData.AddCell(pdfCell2);
            pdfCell2 = new PdfPCell(new Phrase("Jefe Inmediato:", font2));
            pdfCell2.BackgroundColor = new Color(GVGestionCompetencias.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCell2);
            pdfCell2 = new PdfPCell(new Phrase(TXjefe.Text));
            pdfTableData.AddCell(pdfCell2);
            #endregion
            #region TablaDatos
            Font font3 = new Font();
            font3.Color = new Color(GVGestionCompetencias.HeaderStyle.ForeColor);
            PdfPTable pdfTableFooterData = new PdfPTable(4);
            PdfPCell pdfCell3 = new PdfPCell(new Phrase("Valor total de la Evaluación:", font2));
            pdfCell3.BackgroundColor = new Color(GVGestionCompetencias.HeaderStyle.BackColor);
            pdfTableFooterData.AddCell(pdfCell3);
            pdfCell3 = new PdfPCell(new Phrase(LvalorTotalCompetencia.Text));
            pdfTableFooterData.AddCell(pdfCell3);
            pdfCell3 = new PdfPCell(new Phrase("La calificación final de la Evaluación:", font2));
            pdfCell3.BackgroundColor = new Color(GVGestionCompetencias.HeaderStyle.BackColor);
            pdfTableFooterData.AddCell(pdfCell3);
            pdfCell3 = new PdfPCell(new Phrase(LvalorCalificacion.Text));
            pdfTableFooterData.AddCell(pdfCell3);
            pdfCell3 = new PdfPCell(new Phrase("Observaciones:", font2));
            pdfCell3.BackgroundColor = new Color(GVGestionCompetencias.HeaderStyle.BackColor);
            pdfTableFooterData.AddCell(pdfCell3);
            pdfCell3 = new PdfPCell(new Phrase(TXobservaciones.Text));
            pdfTableFooterData.AddCell(pdfCell3);
            pdfCell3 = new PdfPCell(new Phrase("Fecha Próxima Evaluación:", font2));
            pdfCell3.BackgroundColor = new Color(GVGestionCompetencias.HeaderStyle.BackColor);
            pdfTableFooterData.AddCell(pdfCell3);
            pdfCell3 = new PdfPCell(new Phrase(TXfechanext.Text));
            pdfTableFooterData.AddCell(pdfCell3);
            #endregion
            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Evaluación de Competencias"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTable);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableTotales);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableFooterData);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteEvaluacionCompetencias.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        public DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();
            if (dtg.HeaderRow != null)
            {
                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.RowHeaderColumn[i].ToString());
                }
            }

            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            /*Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg1 = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dgEncabezado = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dgData = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dg2 = new System.Web.UI.WebControls.DataGrid();

            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Nombre y Apellidos de Evaluado:");
            gridEncabezado.Columns.Add("Cargo:");
            gridEncabezado.Columns.Add("Proceso:");
            gridEncabezado.Columns.Add("Jefe Inmediato:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Nombre y Apellidos de Evaluado:"] = txtNombreEva.Text;
            rowEncabezado["Cargo:"] = tbxResponsable.Text;
            rowEncabezado["Proceso:"] = Session["proceso"].ToString();
            rowEncabezado["Jefe Inmediato:"] = TXjefe.Text;
            gridEncabezado.Rows.Add(rowEncabezado);

                dgEncabezado.DataSource = gridEncabezado;
                dgEncabezado.DataBind();
                dgEncabezado.RenderControl(htmlWrite);

                DataTable gridData = new DataTable();
                gridData.Columns.Add("Valor total de la Evaluación:");
                gridData.Columns.Add("La calificacion final de la Evaluación:");
                gridData.Columns.Add("Observaciones:");
                gridData.Columns.Add("Fecha Proxima Evaluación:");

                DataRow rowData;
                rowData = gridData.NewRow();
                rowData["Valor total de la Evaluacion:"] = LvalorTotalCompetencia.Text;
                rowData["La calificacion final de la Evaluación:"] = LvalorCalificacion.Text;
                rowData["Observaciones:"] = TXobservaciones.Text;
                rowData["Fecha Proxima Evaluación:"] = TXfechanext.Text;
                gridData.Rows.Add(rowData);
                

            dg1.DataSource = GetDataTable(GVprintEvaComp);
            dg1.DataBind();
            dg1.RenderControl(htmlWrite);

            dg2.DataSource = GetDataTable(GVtotal);
            dg2.DataBind();
            dg2.RenderControl(htmlWrite);

            dgData.DataSource = gridData;
            dgData.DataBind();
            dgData.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();*/
            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Nombre y Apellidos de Evaluado:");
            gridEncabezado.Columns.Add("Cargo:");
            gridEncabezado.Columns.Add("Proceso:");
            gridEncabezado.Columns.Add("Jefe Inmediato:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Nombre y Apellidos de Evaluado:"] = txtNombreEva.Text;
            rowEncabezado["Cargo:"] = tbxResponsable.Text;
            string proceso = string.Empty;
            if (ddlSubproceso.SelectedValue != "" && ddlSubproceso.SelectedValue != "0")
            {
                proceso = ddlSubproceso.SelectedItem.Text;
            }
            else
            {
                if (ddlProceso.SelectedValue != "" && ddlProceso.SelectedValue != "0")
                {
                    proceso = ddlProceso.SelectedItem.Text;
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue != "" && ddlMacroproceso.SelectedValue != "0")
                    {
                        proceso = ddlMacroproceso.SelectedItem.Text;
                    }
                }
            }
            rowEncabezado["Proceso:"] = proceso;
            rowEncabezado["Jefe Inmediato:"] = TXjefe.Text;
            gridEncabezado.Rows.Add(rowEncabezado);

            DataTable gridEvaValue = new DataTable();
            gridEvaValue.Columns.Add("Nombre Competencia:");
            gridEvaValue.Columns.Add("Descripción Criterio Competencia:");
            gridEvaValue.Columns.Add("Puntaje Asignado:");
            DataRow rowEvaValue;
            foreach (GridViewRow GridViewRow in GVGestionCompetenciasEvaVal.Rows)
            {
                rowEvaValue = gridEvaValue.NewRow();
                string strNombreCompetencia = ((Label)GridViewRow.FindControl("NombreCompetencia")).Text;
                string DescripcionCompetencia = ((Label)GridViewRow.FindControl("DescripcionCompetencia")).Text;
                string LpuntajeAsignado = ((Label)GridViewRow.FindControl("LpuntajeAsignado")).Text;
                rowEvaValue["Nombre Competencia:"] = strNombreCompetencia;
                rowEvaValue["Descripción Criterio Competencia:"] = DescripcionCompetencia;
                rowEvaValue["Puntaje Asignado:"] = LpuntajeAsignado;
                gridEvaValue.Rows.Add(rowEvaValue);
            }

            DataTable gridPonderado = new DataTable();
            gridPonderado.Columns.Add("Código:");
            gridPonderado.Columns.Add("Nombre Competencia:");
            gridPonderado.Columns.Add("Puntaje Asignado Total:");
            gridPonderado.Columns.Add("Ponderación:");
            gridPonderado.Columns.Add("Total:");
            DataRow rowPonderado;
            foreach (GridViewRow GridViewRow in GVtotal.Rows)
            {
                rowPonderado = gridPonderado.NewRow();
                string intId = ((Label)GridViewRow.FindControl("intId")).Text;
                string Nombre = ((Label)GridViewRow.FindControl("Nombre")).Text;
                string intPuntajeTotal = ((Label)GridViewRow.FindControl("intPuntajeTotal")).Text;
                string intPonderacion = ((Label)GridViewRow.FindControl("intPonderacion")).Text;
                string intTotal = ((Label)GridViewRow.FindControl("intTotal")).Text;
                rowPonderado["Código:"] = intId;
                rowPonderado["Nombre Competencia:"] = Nombre;
                rowPonderado["Puntaje Asignado Total:"] = intPuntajeTotal;
                rowPonderado["Ponderación:"] = intPonderacion;
                rowPonderado["Total:"] = intTotal;
                gridPonderado.Rows.Add(rowPonderado);
            }

            DataTable gridData = new DataTable();
            gridData.Columns.Add("Valor total de la Evaluación:");
            gridData.Columns.Add("La calificacion final de la Evaluación:");
            gridData.Columns.Add("Observaciones:");
            gridData.Columns.Add("Fecha Próxima Evaluación:");

            DataRow rowData;
            rowData = gridData.NewRow();
            rowData["Valor total de la Evaluación:"] = LvalorTotalCompetencia.Text;
            rowData["La calificacion final de la Evaluación:"] = LvalorCalificacion.Text;
            rowData["Observaciones:"] = TXobservaciones.Text;
            rowData["Fecha Próxima Evaluación:"] = TXfechanext.Text;
            gridData.Rows.Add(rowData);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "Encabezado");
            workbook.Worksheets.Add(gridEvaValue, "Puntaje Asignado");
            workbook.Worksheets.Add(gridPonderado, "Ponderado");
            workbook.Worksheets.Add(gridData, "Valor Evaluación");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDownloadFile(RowGrid);
                    break;
            }
        }
        public void mtdDownloadFile(int RowGrid)
        {
            
            #region Vars
            string strNombreArchivo = InfoGridArchivos.Rows[RowGrid]["UrlArchivo"].ToString().Trim();
            int IdRegistro =Convert.ToInt32(InfoGridArchivos.Rows[RowGrid]["strCodigoDocumento"].ToString().Trim());
            clsValorEvaluacionCompetenciasBLL cDtCrlVersion = new clsValorEvaluacionCompetenciasBLL();
            string strErrMsg = string.Empty;
            DataTable dt = new DataTable();
            byte[] bPdfData = cDtCrlVersion.mtdDownLoadFile(ref strErrMsg, ref IdRegistro, strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        
        }

        protected void btnImgCancelarProcess_Click(object sender, ImageClickEventArgs e)
        {
            mtdRestFields();
            mtdStartLoad();
        }

        
    }
}