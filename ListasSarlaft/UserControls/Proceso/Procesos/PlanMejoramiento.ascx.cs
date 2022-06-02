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
using Microsoft.Reporting.WebForms;
using System.Configuration;
using ClosedXML.Excel;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class PlanMejoramiento : System.Web.UI.UserControl
    {
        string IdFormulario = "4032";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.btnInsertarNuevo);
            scriptManager.RegisterPostBackControl(this.GVplanmejoramiento);
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
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;
        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;
        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

        private DataTable infoGridPrint;
        private int rowGridPrint;

        private DataTable InfoGrid1
        {
            get
            {
                infoGrid1 = (DataTable)ViewState["infoGrid1"];
                return infoGrid1;
            }
            set
            {
                infoGrid1 = value;
                ViewState["infoGrid1"] = infoGrid1;
            }
        }

        private int RowGrid1
        {
            get
            {
                rowGrid1 = (int)ViewState["rowGrid1"];
                return rowGrid1;
            }
            set
            {
                rowGrid1 = value;
                ViewState["rowGrid1"] = rowGrid1;
            }
        }

        private int PagIndex1
        {
            get
            {
                pagIndex1 = (int)ViewState["pagIndex1"];
                return pagIndex1;
            }
            set
            {
                pagIndex1 = value;
                ViewState["pagIndex1"] = pagIndex1;
            }
        }

        private DataTable InfoGrid2
        {
            get
            {
                infoGrid2 = (DataTable)ViewState["infoGrid2"];
                return infoGrid2;
            }
            set
            {
                infoGrid2 = value;
                ViewState["infoGrid2"] = infoGrid2;
            }
        }

        private int RowGrid2
        {
            get
            {
                rowGrid2 = (int)ViewState["rowGrid2"];
                return rowGrid2;
            }
            set
            {
                rowGrid2 = value;
                ViewState["rowGrid2"] = rowGrid2;
            }
        }

        private int PagIndex2
        {
            get
            {
                pagIndex2 = (int)ViewState["pagIndex2"];
                return pagIndex2;
            }
            set
            {
                pagIndex2 = value;
                ViewState["pagIndex2"] = pagIndex2;
            }
        }

        private DataTable InfoGrid3
        {
            get
            {
                infoGrid3 = (DataTable)ViewState["infoGrid3"];
                return infoGrid3;
            }
            set
            {
                infoGrid3 = value;
                ViewState["infoGrid3"] = infoGrid3;
            }
        }
        private int RowGrid3
        {
            get
            {
                rowGrid3 = (int)ViewState["rowGrid3"];
                return rowGrid3;
            }
            set
            {
                rowGrid3 = value;
                ViewState["rowGrid3"] = rowGrid3;
            }
        }

        private int PagIndex3
        {
            get
            {
                pagIndex3 = (int)ViewState["pagIndex3"];
                return pagIndex3;
            }
            set
            {
                pagIndex3 = value;
                ViewState["pagIndex3"] = pagIndex3;
            }
        }


        private DataTable InfoGridPrint
        {
            get
            {
                infoGridPrint = (DataTable)ViewState["infoGridPrint"];
                return infoGridPrint;
            }
            set
            {
                infoGridPrint = value;
                ViewState["infoGridPrint"] = infoGridPrint;
            }
        }

        private int RowGridPrint
        {
            get
            {
                rowGridPrint = (int)ViewState["rowGridPrint"];
                return rowGridPrint;
            }
            set
            {
                rowGridPrint = value;
                ViewState["rowGridPrint"] = rowGridPrint;
            }
        }
        #endregion
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
            //txtFecha.Text = "" + DateTime.Now;
            PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            if (!mtdLoadPlanMejoramiento(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            mtdCargarDDLs();
            PopulateTreeView();
        }
        protected void mtdResetFields()
        {
            BodyGridPM.Visible = true;
            BodyFormPM.Visible = false;
            Dbutton.Visible = false;
            txtId.Text = "";
            ddlCadenaValor.ClearSelection();
            ddlCadenaValor.Enabled = true;
            ddlMacroproceso.ClearSelection();
            ddlMacroproceso.Items.Clear();
            ddlMacroproceso.Enabled = true;
            ddlProceso.ClearSelection();
            ddlProceso.Items.Clear();
            ddlProceso.Enabled = true;
            ddlSubproceso.ClearSelection();
            ddlSubproceso.Items.Clear();
            ddlSubproceso.Enabled = true;
            TXdescripcionActividad.Text = "";
            TXdescripcionActividad.Enabled = true;
            TXfechaIni.Text = "";
            TXfechaIni.Enabled = true;
            TXfechaFin.Text = "";
            TXfechaFin.Enabled = true;
            TXplan.Text = "";
            TXplan.Enabled = true;
            TXrecursos.Text = "";
            TXrecursos.Enabled = true;
            TXarea.Text = "";
            TXarea.Enabled = true;
            TXactividad.Text = "";
            TXactividad.Enabled = true;
            TXfechaProgramada.Text = "";
            TXfechaProgramada.Enabled = true;
            TXfechaRealizada.Text = "";
            TXfechaRealizada.Enabled = true;
            tbxResponsable.Text = "";
            tbxResponsable.Enabled = true;
            TXseguimiento.Text = "";
            TXseguimiento.Enabled = true;
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";

            imgDependencia4.Visible = true;

        }
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            BodyGridPM.Visible = false;
            BodyFormPM.Visible = true;

            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }
        #region DDLs
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;
            mtdLoadDDLCadenaValor(ref strErrMsg);
            mtdLoadDDLMacroProceso(ref strErrMsg);
        }
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = true;
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
        /// Consulta los Procesos y carga el DDL de los subprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                    0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty);
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
                strErrMsg = string.Format("Error durante la consulta de Subprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
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
        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(ref strErrMsg))
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

            if (mtdLoadDDLSubproceso(ref strErrMsg))
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

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdInsertarPlanMejoramiento(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
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
            if (mtdUpdateDetallePlanMejoramiento(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdStard();
                mtdResetFields();
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
        }
        /// <summary>
        /// Realiza la insercion de la evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        protected bool mtdInsertarPlanMejoramiento(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsPlanDeMejoramiento objPlanMejoramiento = new clsPlanDeMejoramiento();
            clsDetallePlanMejoramiento objDetallePlan = new clsDetallePlanMejoramiento();
            clsPlanMejoramientoBLL cPlan = new clsPlanMejoramientoBLL();
            int IdProceso = 0;
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            if (ddlSubproceso.SelectedValue.ToString() != "0" && ddlSubproceso.SelectedValue.ToString() != "")
            {
                objPlanMejoramiento.intIdMacroProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                IdProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                objPlanMejoramiento.intIdTipoProceso = 3;
            }
            else
            {
                if (ddlProceso.SelectedValue.ToString() != "0" && ddlProceso.SelectedValue.ToString() != "")
                {
                    objPlanMejoramiento.intIdMacroProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                    IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                    objPlanMejoramiento.intIdTipoProceso = 2;
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue.ToString() != "0" && ddlMacroproceso.SelectedValue.ToString() != "")
                    {
                        objPlanMejoramiento.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                        IdProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                        objPlanMejoramiento.intIdTipoProceso = 1;
                    }
                    else
                    {
                        strErrMsg = "Debe seleccionar el Macroproceso para seguir";
                        return booResult;
                    }
                }
            }
            objPlanMejoramiento.strDescripcionActividad = TXdescripcionActividad.Text;
            objPlanMejoramiento.dtPeriodoEvaluarInicial = TXfechaIni.Text;
            objPlanMejoramiento.dtPeriodoEvaluarFinal = TXfechaFin.Text;
            objPlanMejoramiento.strPlanMejoramiento = TXplan.Text;
            objPlanMejoramiento.strRecursos = TXrecursos.Text;
            objPlanMejoramiento.dtFechaRegistro = DateTime.Now;
            objPlanMejoramiento.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            
            booResult = cPlan.mtdInsertarPlanMejoramiento(objPlanMejoramiento, ref strErrMsg);
            if (booResult == true)
            {
                int IdPlan = cPlan.mtdLastIdPlanMejoramiento(ref strErrMsg);
                booResult = cPlan.mtdInsertarPlanMejoramientoProceso(objPlanMejoramiento, ref strErrMsg, IdPlan);
                //booResult = true;
                objDetallePlan.intIdMacroProceso = objPlanMejoramiento.intIdMacroProceso;
                objDetallePlan.intIdPlanDeMejoramiento = IdPlan;
                objDetallePlan.strActividad = TXactividad.Text;
                objDetallePlan.strArea = TXarea.Text;
                objDetallePlan.dtFechaProgramada = TXfechaProgramada.Text;
                objDetallePlan.dtFechaRealizada = TXfechaRealizada.Text;
                objDetallePlan.strResponsable = lblIdDependencia4.Text;
                objDetallePlan.strSeguimiento = TXseguimiento.Text;
                objDetallePlan.dtFechaRegistro = DateTime.Now;
                objDetallePlan.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());

                booResult = cPlan.mtdInsertarDetallePlanMejoramiento(objDetallePlan, ref strErrMsg);
                strErrMsg = "Plan mejoramiento registrado exitosamente";
                if (booResult != true)
                    strErrMsg = "Error al registrar el plan de mejoramiento";
            }
            return booResult;
        }
        private bool mtdLoadPlanMejoramiento(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsPlanDeMejoramiento objPlan = new clsPlanDeMejoramiento();
            List<clsPlanDeMejoramiento> lstPlan = new List<clsPlanDeMejoramiento>();
            clsPlanMejoramientoBLL cPlan = new clsPlanMejoramientoBLL();
            #endregion Vars
            lstPlan = cPlan.mtdConsultarPlanMejoramiento(ref strErrMsg, ref lstPlan);

            if (lstPlan != null)
            {
                mtdLoadPlanMejoramiento();
                mtdLoadPlanMejoramiento(lstPlan);
                GVplanmejoramiento.DataSource = lstPlan;
                GVplanmejoramiento.PageIndex = pagIndex1;
                GVplanmejoramiento.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay datos para mostrar";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadPlanMejoramiento()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("intIdMacroProceso", typeof(string));
            grid.Columns.Add("strProceso", typeof(string));
            grid.Columns.Add("strDescripcionActividad", typeof(string));
            grid.Columns.Add("dtPeriodoEvaluarInicial", typeof(string));
            grid.Columns.Add("dtPeriodoEvaluarFinal", typeof(string));
            grid.Columns.Add("strPlanMejoramiento", typeof(string));
            grid.Columns.Add("strRecursos", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));

            GVplanmejoramiento.DataSource = grid;
            GVplanmejoramiento.DataBind();
            InfoGrid1 = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadPlanMejoramiento(List<clsPlanDeMejoramiento> lstPlan)
        {
            string strErrMsg = String.Empty;

            foreach (clsPlanDeMejoramiento objPlan in lstPlan)
            {

                InfoGrid1.Rows.Add(new Object[] {
                    objPlan.intId.ToString().Trim(),
                            objPlan.intIdMacroProceso.ToString().Trim(),
                            objPlan.strProceso.ToString().Trim(),
                            objPlan.strDescripcionActividad.ToString().Trim(),
                            objPlan.dtPeriodoEvaluarInicial.ToString().Trim(),
                            objPlan.dtPeriodoEvaluarFinal.ToString().Trim(),
                            objPlan.strPlanMejoramiento.ToString().Trim(),
                            objPlan.strRecursos.ToString().Trim(),
                            objPlan.dtFechaRegistro.ToString().Trim(),
                            objPlan.intIdUsuario.ToString().Trim(),
                            objPlan.strUsuario.ToString().Trim(),
                    });
            }
        }

        protected void GVplanmejoramiento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    Dbutton.Visible = true;
                    break;
            }
        }
        private void mtdShowUpdate(int RowGrid)
        {
            GridViewRow row = GVplanmejoramiento.Rows[RowGrid];
            var colsNoVisible = GVplanmejoramiento.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;

            List<clsDetallePlanMejoramiento> lstPlan = new List<clsDetallePlanMejoramiento>();
            clsDetallePlanMejoramiento objDetallePlan = new clsDetallePlanMejoramiento();
            clsPlanMejoramientoBLL cPlan = new clsPlanMejoramientoBLL();

            BodyGridPM.Visible = false;
            BodyFormPM.Visible = true;

            #region DatosControl
            txtId.Text = row.Cells[0].Text;
            int IdPlan = Convert.ToInt32(row.Cells[0].Text);
            int IdTipoProceso = Convert.ToInt32(colsNoVisible[3].ToString());
            //LtexProceso.Text = ((Label)row.FindControl("NombreProceso")).Text;
            mtdCargarDDLs();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador(Convert.ToInt32(colsNoVisible[2].ToString()), 0, 0, 0, string.Empty);
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            //object[] objProcesos = cProcInd.mtdConsultarProcIndicadorIdProcesoDV(objProcInd, ref strErrMsg, IdTipoProceso);
            object[] objProcesos = cProcInd.mtdConsultarProcesoVersion(ref strErrMsg, Convert.ToInt32(colsNoVisible[2].ToString()), IdTipoProceso, IdPlan);
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
                    mtdLoadDDLSubproceso(ref strErrMsg);
                    ddlSubproceso.SelectedValue = "0";
                    break;
                case "S":
                    clsSubproceso objSP = (clsSubproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objSP.intIdCadenaValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objSP.intIdMacroProceso.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = objSP.intIdProceso.ToString();
                    mtdLoadDDLSubproceso(ref strErrMsg);
                    ddlSubproceso.SelectedValue = objSP.intId.ToString();
                    break;
            }
            LtexProceso.Visible = true;
            ddlSubproceso.Enabled = false;
            ddlProceso.Enabled = false;
            ddlMacroproceso.Enabled = false;
            ddlCadenaValor.Enabled = false;
            
            TXdescripcionActividad.Text = ((Label)row.FindControl("DescripcionActividad")).Text;
            TXdescripcionActividad.Enabled = false;
            TXfechaIni.Text = row.Cells[4].Text;
            TXfechaIni.Enabled = false;
            TXfechaFin.Text = row.Cells[5].Text;
            TXfechaFin.Enabled = false;
            TXplan.Text = ((Label)row.FindControl("PlanMejoramiento")).Text;
            TXplan.Enabled = false;
            TXrecursos.Text = ((Label)row.FindControl("Recursos")).Text;
            TXrecursos.Enabled = false;

            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();

            lstPlan = cPlan.mtdConsultarDetallePlanMejoramiento(ref strErrMsg,ref lstPlan, ref IdPlan);
            if (lstPlan != null)
            {
                foreach (clsDetallePlanMejoramiento objPlan in lstPlan)
                {
                    TXactividad.Text = objPlan.strActividad;
                    TXactividad.Enabled = false;
                    TXarea.Text = objPlan.strArea;
                    TXarea.Enabled = false;
                    TXfechaProgramada.Text = "" + objPlan.dtFechaProgramada;
                    TXfechaProgramada.Enabled = false;
                    TXfechaRealizada.Text = "" + objPlan.dtFechaRealizada;
                    lblIdDependencia4.Text = objPlan.strResponsable;
                    tbxResponsable.Text = objPlan.strCargoResponsable;
                    tbxResponsable.Enabled = false;
                    imgDependencia4.Visible = false;
                    TXseguimiento.Text = objPlan.strSeguimiento;
                }
            }
            #endregion
        }
        private bool mtdUpdateDetallePlanMejoramiento(ref string strErrMsg)
        {
            bool booResult = false;
            clsDetallePlanMejoramiento objDetallePlan = new clsDetallePlanMejoramiento();
            clsPlanMejoramientoBLL cPlan = new clsPlanMejoramientoBLL();
            objDetallePlan.intIdPlanDeMejoramiento = Convert.ToInt32(txtId.Text);
            objDetallePlan.dtFechaRealizada = TXfechaRealizada.Text;
            objDetallePlan.strSeguimiento = TXseguimiento.Text;

            booResult = cPlan.mtdUpdateDetallePlanMejoramiento(objDetallePlan, ref strErrMsg);

            if (booResult == true)
                strErrMsg = "Plan de mejoramiento actualizado exitosamente";
            else
                strErrMsg = "Error al actualizar el plan de mejoramiento";
            return booResult;
        }

        protected void GVplanmejoramiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex1 = e.NewPageIndex;
            /*GVevaluacionDesempeño.PageIndex = PagIndex1;
            GVevaluacionDesempeño.DataBind();*/
            string strErrMsg = "";
            mtdLoadPlanMejoramiento(ref strErrMsg);
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }
        private void mtdExportPdf()
        {
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
            pdfDocument.AddTitle("Reporte de Plan de Mejoramiento");
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
            Font font1 = new Font();
            font1.Color = new Color(GVplanmejoramiento.HeaderStyle.ForeColor);
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Código:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtId.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Macroproceso:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            if (ddlSubproceso.SelectedValue.ToString() != "0" && ddlSubproceso.SelectedValue.ToString() != "")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlSubproceso.SelectedItem.Text));
            }
            else
            {
                if (ddlProceso.SelectedValue.ToString() != "0" && ddlProceso.SelectedValue.ToString() != "")
                {
                    pdfCellEncabezado = new PdfPCell(new Phrase(ddlProceso.SelectedItem.Text));
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue.ToString() != "0" && ddlMacroproceso.SelectedValue.ToString() != "")
                    {
                        pdfCellEncabezado = new PdfPCell(new Phrase(ddlMacroproceso.SelectedItem.Text));
                    }
                }
            }
            
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Descripción Actividad:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXdescripcionActividad.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Periodo Inicial:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaIni.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Periodo Final:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaFin.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Plan de Mejoramiento:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXplan.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Recursos que se requieren:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXrecursos.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Área:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXarea.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Actividad:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXactividad.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Programación:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaProgramada.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Realización:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaRealizada.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Cargo Responsable:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxResponsable.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            

            PdfPTable pdfTableObservaciones = new PdfPTable(2);
            pdfCellEncabezado = new PdfPCell(new Phrase("Seguimiento:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXseguimiento.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Usuario Registro:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxUsuarioCreacion.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Registro:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVplanmejoramiento.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtFecha.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
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
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Plan de Mejoramiento"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableObservaciones);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReportePlanMejoramiento.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReportePlanMejoramiento_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Código:");
            gridEncabezado.Columns.Add("Macroproceso:");
            gridEncabezado.Columns.Add("Descripción Actividad:");
            gridEncabezado.Columns.Add("Fecha Periodo Inicial:");
            gridEncabezado.Columns.Add("Fecha Periodo Final:");
            gridEncabezado.Columns.Add("Plan de Mejoramiento:");
            gridEncabezado.Columns.Add("Recursos que se requieren:");
            gridEncabezado.Columns.Add("Área:");
            gridEncabezado.Columns.Add("Actividad:");
            gridEncabezado.Columns.Add("Fecha Programación:");
            gridEncabezado.Columns.Add("Fecha Realización:");
            gridEncabezado.Columns.Add("Cargo Responsable:");
            gridEncabezado.Columns.Add("Seguimiento:");
            gridEncabezado.Columns.Add("Usuario:");
            gridEncabezado.Columns.Add("Fecha Registro:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Código:"] = txtId.Text;
            //rowEncabezado["Macroproceso:"] = Session["NombreProceso"].ToString();
            if (ddlSubproceso.SelectedValue.ToString() != "0" && ddlSubproceso.SelectedValue.ToString() != "")
            {
                rowEncabezado["Macroproceso:"] = ddlSubproceso.SelectedItem.Text;
            }
            else
            {
                if (ddlProceso.SelectedValue.ToString() != "0" && ddlProceso.SelectedValue.ToString() != "")
                {
                    rowEncabezado["Macroproceso:"] = ddlProceso.SelectedItem.Text;
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue.ToString() != "0" && ddlMacroproceso.SelectedValue.ToString() != "")
                    {
                        rowEncabezado["Macroproceso:"] = ddlMacroproceso.SelectedItem.Text;
                    }
                }
            }
            rowEncabezado["Descripción Actividad:"] = TXdescripcionActividad.Text;
            rowEncabezado["Fecha Periodo Inicial:"] = TXfechaIni.Text;
            rowEncabezado["Fecha Periodo Final:"] = TXfechaFin.Text;
            rowEncabezado["Plan de Mejoramiento:"] = TXplan.Text;
            rowEncabezado["Recursos que se requieren:"] = TXrecursos.Text;
            rowEncabezado["Área:"] = TXarea.Text;
            rowEncabezado["Actividad:"] = TXactividad.Text;
            rowEncabezado["Fecha Programación:"] = TXfechaProgramada.Text;
            rowEncabezado["Fecha Realización:"] = TXfechaRealizada.Text;
            rowEncabezado["Cargo Responsable:"] = tbxResponsable.Text;
            rowEncabezado["Seguimiento:"] = TXseguimiento.Text;
            rowEncabezado["Usuario:"] = tbxUsuarioCreacion.Text;
            rowEncabezado["Fecha Registro:"] = txtFecha.Text;
            gridEncabezado.Rows.Add(rowEncabezado);


            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "PlanMejoramiento");
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
    }
}