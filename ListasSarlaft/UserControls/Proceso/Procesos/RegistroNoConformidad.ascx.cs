using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
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
using System.Configuration;
using ClosedXML.Excel;
using System.Net;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class RegistroNoConformidad : System.Web.UI.UserControl
    {
        string IdFormulario = "4027";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.ImButtonDownload);
            scriptManager.RegisterPostBackControl(this.GVcontrolNoConformidad);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdInicializarValores();

                }
            }
        }
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridControl;
        private int rowGridControl;
        private int pagIndexControl;

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
        private DataTable InfoGridControl
        {
            get
            {
                infoGridControl = (DataTable)ViewState["infoGridControl"];
                return infoGridControl;
            }
            set
            {
                infoGridControl = value;
                ViewState["infoGridControl"] = infoGridControl;
            }
        }

        private int RowGridControl
        {
            get
            {
                rowGridControl = (int)ViewState["rowGridControl"];
                return rowGridControl;
            }
            set
            {
                rowGridControl = value;
                ViewState["rowGridControl"] = rowGridControl;
            }
        }

        private int PagIndexControl
        {
            get
            {
                pagIndexControl = (int)ViewState["pagIndexControl"];
                return pagIndexControl;
            }
            set
            {
                pagIndexControl = value;
                ViewState["pagIndexControl"] = pagIndexControl;
            }
        }
        #endregion
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
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            /*mtdLoadEvaluacionProveedor(ref strErrMsg);*/
            if (!mtdCargarDDLs(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            PopulateTreeView();
            if (!mtdLoadControlNoConformidad(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            
        }
        protected void mtdResetFields()
        {
            BodyFormRNC.Visible = false;
            BodyGridRNC.Visible = true;
            TituloRNC.Visible = false;
            ResultadoAuditoria.Visible = false;
            AuditoriaInfo.Visible = false;
            Dbutton.Visible = false;
            txtId.Text = "";
            ddlCadenaValor.ClearSelection();
            ddlCadenaValor.Enabled = true;
            ddlMacroproceso.ClearSelection();
            ddlMacroproceso.Enabled = true;
            ddlProceso.ClearSelection();
            ddlProceso.Enabled = true;
            TXidauditoria.Text = "";
            TXtema.Text = "";
            TXestandar.Text = "";
            TXdescripcion.Text = "";
            TXdescripcion.Enabled = true;
            TXfechaInicio.Text = "";
            TXfechaInicio.Enabled = true;
            TXseguimiento.Text = "";
            TXseguimiento.Enabled = true;
            TXfechaFinal.Text = "";
            TXfechaFinal.Enabled = true;
            tbxResponsable.Text = "";
            tbxResponsable.Enabled = true;
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
        }
        protected void mtdShowAuditoria(int Rowgrid)
        {
            ResultadoAuditoria.Visible = false;
            AuditoriaInfo.Visible = true;

            GridViewRow row = GVauditorias.Rows[Rowgrid];
            TXidauditoria.Text = ((Label)row.FindControl("idAuditoria")).Text;
            TXtema.Text = ((Label)row.FindControl("tema")).Text;
            TXestandar.Text = ((Label)row.FindControl("estandar")).Text;
        }
        protected void mtdShowUpdate(int Rowgrid)
        {
            BodyFormRNC.Visible = true;
            BodyGridRNC.Visible = false;
            TituloRNC.Visible = true;
            Dbutton.Visible = true;
            ResultadoAuditoria.Visible = false;
            AuditoriaInfo.Visible = true;
            string strErrMsg = String.Empty;
            GridViewRow row = GVcontrolNoConformidad.Rows[Rowgrid];
            var colsNoVisible = GVcontrolNoConformidad.DataKeys[Rowgrid].Values;
            string[] list = new string[4];
            list[0] = colsNoVisible[0].ToString();
            list[1] = colsNoVisible[1].ToString();
            list[2] = colsNoVisible[2].ToString();
            list[3] = colsNoVisible[3].ToString();
            int IdControl = Convert.ToInt32(row.Cells[0].Text);
            if (!mtdConsultarControlNoConformidadAud(ref strErrMsg, ref IdControl))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            mtdUpdateRegist(row, list);
            IBinsertGVC.Visible = false;
            IBupdateGVC.Visible = true;
            Link.Visible = true;
            Dbutton.Visible = true;
        }
        #region DLLeventos
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (!mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }
        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (mtdLoadGridAuditoriaMacroproceso(ref strErrMsg) == false)
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                TituloRNC.Visible = true;
                ResultadoAuditoria.Visible = true;
                AuditoriaInfo.Visible = false;
            }
        }
        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            if (mtdLoadGridAuditoriaProceso(ref strErrMsg) == false)
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                TituloRNC.Visible = true;
                ResultadoAuditoria.Visible = true;
                AuditoriaInfo.Visible = false;
            }
        }
        #endregion
        #region LoadMetodos
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
        /// <summary>
        /// Consulta los Procesos y carga el DDL de los macroprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Consulta las auditorias por el Macroproceso
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadGridAuditoriaMacroproceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsAuditoriaNoConformidad objAudtoria = new clsAuditoriaNoConformidad();
            List<clsAuditoriaNoConformidad> lstAuditoria = new List<clsAuditoriaNoConformidad>();
            clsRegistroNoConformidadBLL cRNC = new clsRegistroNoConformidadBLL();
            #endregion Vars
            int idMacroproceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            lstAuditoria = cRNC.mtdConsultarAuditoriaMacroproceso(ref lstAuditoria, ref strErrMsg, ref idMacroproceso);
            if (lstAuditoria != null)
            {
                mtdLoadGridAuditoriaMacroproceso();
                mtdLoadGridAuditoriaMacroproceso(lstAuditoria);
                GVauditorias.DataSource = lstAuditoria;
                GVauditorias.PageIndex = pagIndex;
                GVauditorias.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay auditorias para el Macroproceso";
            }
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridAuditoriaMacroproceso()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdAuditoria", typeof(string));
            grid.Columns.Add("strTema", typeof(string));
            grid.Columns.Add("strEstandar", typeof(string));

            GVauditorias.DataSource = grid;
            GVauditorias.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridAuditoriaMacroproceso(List<clsAuditoriaNoConformidad> lstAuditoria)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsAuditoriaNoConformidad objEvaComp in lstAuditoria)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intIdAuditoria.ToString().Trim(),
                    objEvaComp.strTema.ToString().Trim(),
                    objEvaComp.strEstandar.ToString().Trim()
                    });
            }
        }
        /// <summary>
        /// Consulta las auditorias por el Proceso
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadGridAuditoriaProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsAuditoriaNoConformidad objAudtoria = new clsAuditoriaNoConformidad();
            List<clsAuditoriaNoConformidad> lstAuditoria = new List<clsAuditoriaNoConformidad>();
            clsRegistroNoConformidadBLL cRNC = new clsRegistroNoConformidadBLL();
            #endregion Vars
            int Proceso = Convert.ToInt32(ddlProceso.SelectedValue);
            lstAuditoria = cRNC.mtdConsultarAuditoriaProceso(ref lstAuditoria, ref strErrMsg, ref Proceso);
            if (lstAuditoria != null)
            {
                mtdLoadGridAuditoriaProceso();
                mtdLoadGridAuditoriaProceso(lstAuditoria);
                GVauditorias.DataSource = lstAuditoria;
                GVauditorias.PageIndex = pagIndex;
                GVauditorias.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay auditorias para el Proceso";
            }
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridAuditoriaProceso()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdAuditoria", typeof(string));
            grid.Columns.Add("strTema", typeof(string));
            grid.Columns.Add("strEstandar", typeof(string));

            GVauditorias.DataSource = grid;
            GVauditorias.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridAuditoriaProceso(List<clsAuditoriaNoConformidad> lstAuditoria)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsAuditoriaNoConformidad objEvaComp in lstAuditoria)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intIdAuditoria.ToString().Trim(),
                    objEvaComp.strTema.ToString().Trim(),
                    objEvaComp.strEstandar.ToString().Trim()
                    });
            }
        }
        #endregion
        #region ButtonsEvents
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            BodyFormRNC.Visible = true;
            BodyGridRNC.Visible = false;

            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
            //Dbutton.Visible = true;
        }
        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = string.Empty;

            if (!mtdModificaControl(ref strErrMsg))
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                mtdResetFields();
                mtdStard();
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
            mtdStard();
        }
        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = string.Empty;

            if (!mtdInsertarControl(ref strErrMsg))
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else {
                mtdResetFields();
                mtdStard();
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }   
            
        }
        #endregion

        protected void GVauditorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowAuditoria(RowGrid);
                    
                    break;
            }
        }
        /// <summary>
        /// Realiza la insercion del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>+
        string pathFile = string.Empty;
        private bool mtdInsertarControl(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsRegistroNoConformidadBLL cRegistroNo = new clsRegistroNoConformidadBLL();
            int IdProceso = 0;
            int TipoProceso = 0;
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            if (fuArchivoPerfil.HasFile)
            {
                if (System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower() == ".pdf")
                {
                    pathFile = fuArchivoPerfil.FileName;
                    saveFile(pathFile);
                    if (ddlProceso.SelectedValue != "0")
                    {
                        clsControlNoConformidad objCrlNo = new clsControlNoConformidad();
                        objCrlNo.intIdMacroProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                        IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                        TipoProceso = 2;
                        objCrlNo.intResponsable =  Convert.ToInt32(lblIdDependencia4.Text);
                        objCrlNo.strDescripcion = TXdescripcion.Text;
                        objCrlNo.dtFechaInicio = TXfechaInicio.Text;
                        objCrlNo.strSeguimiento = TXseguimiento.Text;
                        //if(TXfechaFinal.Text != "")
                        objCrlNo.dtFechaFinal = TXfechaFinal.Text;
                        objCrlNo.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                        objCrlNo.dtFechaRegistro = DateTime.Now;
                        objCrlNo.strPathFile =  pathFile;

                        booResult = cRegistroNo.mtdInsertarNoConformidad(objCrlNo, ref strErrMsg);
                    }
                    else
                    {
                        clsControlNoConformidad objCrlNo = new clsControlNoConformidad();
                        objCrlNo.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                        IdProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                        TipoProceso = 1;
                        objCrlNo.intResponsable = Convert.ToInt32(lblIdDependencia4.Text);
                        objCrlNo.strDescripcion = TXdescripcion.Text;
                        objCrlNo.dtFechaInicio = TXfechaInicio.Text;
                        objCrlNo.strSeguimiento = TXseguimiento.Text;
                        objCrlNo.dtFechaFinal = TXfechaFinal.Text;
                        objCrlNo.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                        objCrlNo.dtFechaRegistro = DateTime.Now;
                        objCrlNo.strPathFile = pathFile;

                        booResult = cRegistroNo.mtdInsertarNoConformidad(objCrlNo, ref strErrMsg);
                    }
                    
                }
                else
                {
                    strErrMsg = "Unicamente documentos en PDF";
                    return booResult;
                }
            }
            else
            {
                if (ddlProceso.SelectedValue != "0")
                {
                    clsControlNoConformidad objCrlNo = new clsControlNoConformidad();
                    objCrlNo.intIdMacroProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                    IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                    TipoProceso = 2;
                    objCrlNo.intResponsable = Convert.ToInt32(lblIdDependencia4.Text);
                    objCrlNo.strDescripcion = TXdescripcion.Text;
                    objCrlNo.dtFechaInicio = TXfechaInicio.Text;
                    objCrlNo.strSeguimiento = TXseguimiento.Text;
                    objCrlNo.dtFechaFinal = TXfechaFinal.Text;
                    objCrlNo.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                    objCrlNo.dtFechaRegistro = DateTime.Now;
                    objCrlNo.strPathFile = pathFile;

                    booResult = cRegistroNo.mtdInsertarNoConformidad(objCrlNo, ref strErrMsg);
                }
                else
                {
                    clsControlNoConformidad objCrlNo = new clsControlNoConformidad();
                    objCrlNo.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                    IdProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                    TipoProceso = 1;
                    objCrlNo.intResponsable = Convert.ToInt32(lblIdDependencia4.Text);
                    objCrlNo.strDescripcion = TXdescripcion.Text;
                    objCrlNo.dtFechaInicio = TXfechaInicio.Text;
                    objCrlNo.strSeguimiento = TXseguimiento.Text;
                    objCrlNo.dtFechaFinal = TXfechaFinal.Text;
                    objCrlNo.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                    objCrlNo.dtFechaRegistro = DateTime.Now;
                    objCrlNo.strPathFile = pathFile;

                    booResult = cRegistroNo.mtdInsertarNoConformidad(objCrlNo, ref strErrMsg);
                }
            }
            int LastId = cRegistroNo.mtdLastIdControlNoConformidad(ref strErrMsg);
            int IdAuditoria = Convert.ToInt32(TXidauditoria.Text);
            booResult = cRegistroNo.mtdInsertarNoConformidadProceso(LastId, IdProceso, TipoProceso, ref strErrMsg);
            booResult = cRegistroNo.mtdInsertarAuditoriaConformidad(ref strErrMsg,ref  IdAuditoria,ref  LastId);
            if (booResult == true)
            {
                strErrMsg = "No conformidad de calidad registrada exitosamente";
            }
            /*else
            {
                strErrMsg = "Error al registrar la no conformidad";
            }*/
            return booResult;
        }
        private void saveFile(string NombreArchivo)
        {
            string path = ConfigurationManager.AppSettings.Get("DirectorioDocumentos").ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //string fullPath = Server.MapPath("/");
            fuArchivoPerfil.PostedFile.SaveAs(path + NombreArchivo);
        }
        /// <summary>
        /// Realiza la modificacion del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>+
        private bool mtdModificaControl(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsRegistroNoConformidadBLL cRegistroNo = new clsRegistroNoConformidadBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            if (fuArchivoPerfil.HasFile)
            {
                if (System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower() == ".pdf")
                {
                    pathFile = fuArchivoPerfil.FileName;
                    saveFile(pathFile);
                        clsControlNoConformidad objCrlNo = new clsControlNoConformidad();
                        objCrlNo.intId = Convert.ToInt32(txtId.Text);
                        objCrlNo.strSeguimiento = TXseguimiento.Text;
                        objCrlNo.dtFechaFinal = TXfechaFinal.Text;
                        objCrlNo.strPathFile = pathFile;

                        booResult = cRegistroNo.mtdModificarNoConformidadConArchivo(objCrlNo, ref strErrMsg);

                }
                else
                {
                    strErrMsg = "Unicamente documentos en PDF";
                    return booResult;
                }
            }
            else
            {
                clsControlNoConformidad objCrlNo = new clsControlNoConformidad();
                objCrlNo.intId = Convert.ToInt32(txtId.Text);
                objCrlNo.strSeguimiento = TXseguimiento.Text;
                objCrlNo.dtFechaFinal = TXfechaFinal.Text;
                objCrlNo.strPathFile = pathFile;

                booResult = cRegistroNo.mtdModificarNoConformidadSinArchivo(objCrlNo, ref strErrMsg);
            }
            if (booResult == true)
            {
                strErrMsg = "No conformidad de calidad actualizada exitosamente";
            }
            else
            {
                strErrMsg = "Error al actualizar la no conformidad";
            }
            return booResult;
        }
        private bool mtdLoadControlNoConformidad(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsControlNoConformidad objCrlInfra = new clsControlNoConformidad();
            List<clsControlNoConformidad> lstCrlConformidad = new List<clsControlNoConformidad>();
            clsRegistroNoConformidadBLL cCrtno = new clsRegistroNoConformidadBLL();
            #endregion Vars
            lstCrlConformidad = cCrtno.mtdConsultarControlNoConformidad(ref lstCrlConformidad, ref strErrMsg);

            if (lstCrlConformidad != null)
            {
                mtdLoadControlNoConformidad();
                mtdLoadControlNoConformidad(lstCrlConformidad);
                GVcontrolNoConformidad.DataSource = lstCrlConformidad;
                GVcontrolNoConformidad.PageIndex = pagIndex;
                GVcontrolNoConformidad.DataBind();
                booResult = true;
            }else
            {
                strErrMsg = "No Conformidades sin registrar";
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadControlNoConformidad()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("intIdMacroProceso", typeof(string));
            grid.Columns.Add("strProceso", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("dtFechaInicio", typeof(string));
            grid.Columns.Add("strSeguimiento", typeof(string));
            grid.Columns.Add("dtFechaFinal", typeof(string));
            grid.Columns.Add("intResponsable", typeof(string));
            grid.Columns.Add("strCargoResponsable", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("strPathFile", typeof(string));

            GVcontrolNoConformidad.DataSource = grid;
            GVcontrolNoConformidad.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadControlNoConformidad(List<clsControlNoConformidad> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsControlNoConformidad objEvaComp in lstControl)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.intIdMacroProceso.ToString().Trim(),
                    objEvaComp.strProceso.ToString().Trim(),
                    objEvaComp.strDescripcion.ToString().Trim(),
                    objEvaComp.dtFechaInicio.ToString().Trim(),
                    objEvaComp.strSeguimiento.ToString().Trim(),
                    objEvaComp.dtFechaFinal.ToString().Trim(),
                    objEvaComp.intResponsable.ToString().Trim(),
                    objEvaComp.strCargoResponsable.ToString().Trim(),
                    objEvaComp.intIdUsuario.ToString().Trim(),
                    objEvaComp.strUsuario.ToString().Trim(),
                    objEvaComp.dtFechaRegistro.ToString().Trim(),
                    objEvaComp.strPathFile.ToString().Trim()
                    });
            }
        }
        /// <summary>
        /// Consulta las auditorias por el Proceso
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdConsultarControlNoConformidadAud(ref string strErrMsg, ref int IdControl)
        {
            #region Vars
            bool booResult = false;
            clsAuditoriaNoConformidad objAudtoria = new clsAuditoriaNoConformidad();
            List<clsAuditoriaNoConformidad> lstAuditoria = new List<clsAuditoriaNoConformidad>();
            clsRegistroNoConformidadBLL cRNC = new clsRegistroNoConformidadBLL();
            #endregion Vars
            lstAuditoria = cRNC.mtdConsultarControlNoConformidadAud(ref lstAuditoria, ref strErrMsg, ref IdControl);
            if (lstAuditoria != null)
            {
                foreach (clsAuditoriaNoConformidad objEvaComp in lstAuditoria)
                {
                    TXidauditoria.Text = "" +objEvaComp.intIdAuditoria;
                    TXtema.Text = objEvaComp.strTema;
                    TXestandar.Text = objEvaComp.strEstandar;
                }
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay auditorias para el Proceso";
            }
            return booResult;
        }
        /// <summary>
        /// Consulta las auditorias por el Proceso
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private void mtdUpdateRegist(GridViewRow row, string[] colsNoVisible)
        {
            string strErrMsg = string.Empty;
            mtdCargarDDLs(ref strErrMsg);
            ddlCadenaValor.Enabled = false;
            ddlMacroproceso.Enabled = false;
            ddlProceso.Enabled = false;
            txtId.Text = row.Cells[0].Text;
            TXdescripcion.Text = ((Label)row.FindControl("descipcion")).Text;
            TXdescripcion.Enabled = false;
            TXfechaInicio.Text = row.Cells[4].Text;
            TXfechaInicio.Enabled = false;
            TXseguimiento.Text = ((Label)row.FindControl("seguimiento")).Text;
            //if (row.Cells[6].Text != "0001-01-01 0:00:00")
            TXfechaFinal.Text = ((Label)row.FindControl("dtFechaFinal")).Text;
            /*else
                TXfechaFinal.Text = "";*/
            tbxResponsable.Text = ((Label)row.FindControl("NombreHijo")).Text;
            tbxResponsable.Enabled = false;
            lblIdDependencia4.Text = colsNoVisible[2].ToString();
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();
            LnombreArchivo.Text = ((Label)row.FindControl("parthFile")).Text;
            LnombreArchivo.Visible = true;
            Lproceso.Text = ((Label)row.FindControl("proceso")).Text;
            #region Procesos
            //Se DEBE CARGAR INFORMACION DEBE PROCESOS DEBE ACUERDO A LA INFORMACION DEL GRID
            tbxProcIndica.Text = colsNoVisible[3].ToString();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador(0, 0, Convert.ToInt32(colsNoVisible[3].ToString()), 0, string.Empty);
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcesoNoConformidad(objProcInd, ref strErrMsg);

            switch (objProcesos[0].ToString())
            {
                case "M":
                    clsMacroproceso objMP = (clsMacroproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objMP.intIdCadenaDeValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objMP.intId.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = "0";
                    break;
                    case "P":
                        clsProceso objP = (clsProceso)objProcesos[1];
                        ddlCadenaValor.SelectedValue = objP.intIdCadenaValor.ToString();
                        mtdLoadDDLMacroProceso(ref strErrMsg);
                        ddlMacroproceso.SelectedValue = objP.intIdMacroProceso.ToString();
                        mtdLoadDDLProceso(ref strErrMsg);
                        ddlProceso.SelectedValue = objP.intId.ToString();
                        //mtdLoadDDLSubproceso(ref strErrMsg);
                        //ddlSubproceso.SelectedValue = "0";
                        break;
                    /*case "S":
                        clsSubproceso objSP = (clsSubproceso)objProcesos[1];
                        ddlCadenaValor.SelectedValue = objSP.intIdCadenaValor.ToString();
                        mtdLoadDDLMacroProceso(ref strErrMsg);
                        ddlMacroproceso.SelectedValue = objSP.intIdMacroProceso.ToString();
                        mtdLoadDDLProceso(ref strErrMsg);
                        ddlProceso.SelectedValue = objSP.intIdProceso.ToString();
                        mtdLoadDDLSubproceso(ref strErrMsg);
                        ddlSubproceso.SelectedValue = objSP.intId.ToString();
                        break;*/
            }
            #endregion
        }
        protected void GVcontrolNoConformidad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridControl = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGridControl);
                    //Dbutton.Visible = true;
                    break;
            }
        }

        protected void IBlinkAuditoria_Click(object sender, ImageClickEventArgs e)
        {
            int IdAuditoria =Convert.ToInt32(TXidauditoria.Text);
            string strErrMsg = string.Empty;
            clsRegistroNoConformidadBLL cRNC = new clsRegistroNoConformidadBLL();
            int[] data = cRNC.mtdIdPlaneacion(ref strErrMsg,ref IdAuditoria);
            int IdPlaneacion = data[0];
            int IdEstandar = data[1];
            string planeacion = cRNC.mtdPlaneacion(ref strErrMsg, ref IdPlaneacion);
            Response.Redirect("~\\Formularios\\Auditoria\\Admin\\AudAdmAuditoria.aspx?IdAuditoria=" + IdAuditoria + "&IdPlaneacion=" + IdPlaneacion + "&IdEstandar=" + IdEstandar + "&planeacion=" + planeacion);
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel();
        }
        private void mtdExportPdf()
        {
            #region
            Tools tools = new Tools();
            #endregion
            #region IniciacionPdf
            //Creamos el Document del PDF
            string NombrePDF = "Reporte de No Conformidades";
            Document pdfDocument = tools.createpdfDocument(NombrePDF, Response);
            // Creamos la imagen del encabezado
            PdfPTable HeaderImage = tools.createImageHeaders();
            #region Create Datos
            string[] Encabezados = new string[2];
            Encabezados[0] = "Código";
            Encabezados[1] = "Proceso";
            string[] datos = new string[2];
            datos[0] = txtId.Text;
            datos[1] = Lproceso.Text;
            PdfPTable pdfEncabezado = tools.createPDFdataContent(Encabezados, datos, 4, GVcontrolNoConformidad);
            #endregion
            Phrase phHeader = new Phrase();
            phHeader.Add(HeaderImage);
            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();

            #region Auditoria
            string[] EncabezadosAuditoria = new string[3];
            EncabezadosAuditoria[0] = "Id Auditoría:";
            EncabezadosAuditoria[1] = "Tema de la Auditoría:";
            EncabezadosAuditoria[2] = "Estandar:";
            string[] datosAuditoria = new string[3];
            datosAuditoria[0] = TXidauditoria.Text;
            datosAuditoria[1] = TXtema.Text;
            datosAuditoria[2] = TXestandar.Text;

            PdfPTable pdfAuditoria = tools.createPDFdataContent(EncabezadosAuditoria, datosAuditoria, 2, GVcontrolNoConformidad);
            #endregion
            #region DatosSeguimiento
            string[] EncabezadosSeguimiento = new string[4];
            EncabezadosSeguimiento[0] = "Descripción:";
            EncabezadosSeguimiento[1] = "Fecha de Inicio:";
            EncabezadosSeguimiento[2] = "Seguimiento:";
            EncabezadosSeguimiento[3] = "Fecha Final:";
            string[] datosSeguimiento = new string[4];
            datosSeguimiento[0] = TXdescripcion.Text;
            datosSeguimiento[1] = TXfechaInicio.Text;
            datosSeguimiento[2] = TXseguimiento.Text;
            datosSeguimiento[3] = TXfechaFinal.Text;
            PdfPTable pdfSeguimiento = tools.createPDFdataContent(EncabezadosSeguimiento, datosSeguimiento, 4, GVcontrolNoConformidad);

            string[] EncabezadosFooter = new string[3];
            EncabezadosFooter[0] = "Cargue de Documentos:";
            EncabezadosFooter[1] = "Usuario:";
            EncabezadosFooter[2] = "Fecha Registro:";
            string[] datosFooter = new string[3];
            datosFooter[0] = LnombreArchivo.Text;
            datosFooter[1] = tbxUsuarioCreacion.Text;
            datosFooter[2] = txtFecha.Text;
            PdfPTable pdfFooter = tools.createPDFdataContent(EncabezadosFooter, datosFooter, 2, GVcontrolNoConformidad);
            #endregion
            #endregion

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase(NombrePDF));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfEncabezado);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfAuditoria);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfSeguimiento);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfFooter);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            tools.SaveDocument(Response, pdfDocument, NombrePDF);
        }
        public void exportExcel()
        {
            Tools tools = new Tools();
            #region encabezado
            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Código");
            gridEncabezado.Columns.Add("Proceso");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Código"] = txtId.Text;
            rowEncabezado["Proceso"] = Lproceso.Text;

            gridEncabezado.Rows.Add(rowEncabezado);
            DataTable gridAuditoria = new DataTable();
            gridAuditoria.Columns.Add("Id Auditoría:");
            gridAuditoria.Columns.Add("Tema de la Auditoría:");
            gridAuditoria.Columns.Add("Estandar:");
            DataRow rowAuditoria;
            rowAuditoria = gridAuditoria.NewRow();
            rowAuditoria["Id Auditoría:"] = TXidauditoria.Text;
            rowAuditoria["Tema de la Auditoría:"] = TXtema.Text;
            rowAuditoria["Estandar:"] = TXestandar.Text;

            gridAuditoria.Rows.Add(rowAuditoria);
            #endregion
            #region Pie de Pagina
            DataTable gridData = new DataTable();
            gridData.Columns.Add("Descripción:");
            gridData.Columns.Add("Fecha de Inicio:");
            gridData.Columns.Add("Seguimiento:");
            gridData.Columns.Add("Fecha Final:");
            gridData.Columns.Add("Nombre Documento:");
            gridData.Columns.Add("Usuario:");
            gridData.Columns.Add("Fecha Registro:");
            DataRow rowData;
            rowData = gridData.NewRow();
            rowData["Descripción:"] = TXdescripcion.Text;
            rowData["Fecha de Inicio:"] = TXfechaInicio.Text;
            rowData["Seguimiento:"] = TXseguimiento.Text;
            rowData["Fecha Final:"] = TXfechaFinal.Text;
            rowData["Nombre Documento:"] = LnombreArchivo.Text;
            rowData["Usuario:"] = tbxUsuarioCreacion.Text;
            rowData["Fecha Registro:"] = txtFecha.Text;
            gridData.Rows.Add(rowData);
            #endregion
            System.Data.DataSet ds = new System.Data.DataSet();
            gridEncabezado.TableName = "Encabezado";
            gridAuditoria.TableName = "Datos Auditoria";
            gridData.TableName = "Datos Seguimiento";
            ds.Tables.Add(gridEncabezado);
            ds.Tables.Add(gridAuditoria);
            ds.Tables.Add(gridData);
            tools.exportExcel(ds, "NoConformidad", Response, "Reporte De No Conformidades");
        }

        protected void ImButtonDownload_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string path = ConfigurationManager.AppSettings.Get("DirectorioDocumentos").ToString();
                string file = LnombreArchivo.Text;
                if (file != "")
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + file);
                    Response.TransmitFile(path + "\\" + file);
                    Response.End();
                }else
                {
                    omb.ShowMessage("NO hay documentos para descargar", 1, "Atención");
                }
            }catch(Exception ex)
            {
                omb.ShowMessage("Error: "+ex, 1, "Atención");
            }
        }

        protected void GVcontrolNoConformidad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndex = e.NewPageIndex;
            string strErrMsg = "";
            mtdLoadControlNoConformidad(ref strErrMsg);
        }
    }
}