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
using Microsoft.Security.Application;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using System.Configuration;
using ClosedXML.Excel;
using System.Net.Mail;

namespace ListasSarlaft.UserControls.Riesgos.Indicadores
{
    public partial class RiesgoIndicador : System.Web.UI.UserControl
    {
        string IdFormulario = "5026";
        cCuenta cCuenta = new cCuenta();
        private cGestion cGestion = new cGestion();
        private static int LastInsertIdCE;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.GVriesgosIndicadores);
            scriptManager.RegisterPostBackControl(this.GridViewMetas);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.IBinsertFormula);
            scriptManager.RegisterPostBackControl(this.IBupdateFormula);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdInicializarValores();
                    List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = new List<clsDTOdetalleFormulaRiesgoIndicador>();
                    List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = new List<clsDTOdetalleFormulaRiesgoIndicador>();
                    int IteracionFormulaNominador = 0;
                    int IteracionFormulaDenominador = 0;
                    Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
                    Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
                    Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                    Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                }
            }
        }
        #region Properties
        private DataTable infoGridRiesgoIndicador;
        private int rowGridRiesgoIndicador;
        private int pagIndexRiesgoIndicador;
        private int pagIndexDefinicionMetas;

        private DataTable InfoGridRiesgoIndicador
        {
            get
            {
                infoGridRiesgoIndicador = (DataTable)ViewState["infoGridRiesgoIndicador"];
                return infoGridRiesgoIndicador;
            }
            set
            {
                infoGridRiesgoIndicador = value;
                ViewState["infoGridRiesgoIndicador"] = infoGridRiesgoIndicador;
            }
        }

        private int RowGridRiesgoIndicador
        {
            get
            {
                rowGridRiesgoIndicador = (int)ViewState["rowGridRiesgoIndicador"];
                return rowGridRiesgoIndicador;
            }
            set
            {
                rowGridRiesgoIndicador = value;
                ViewState["rowGridRiesgoIndicador"] = rowGridRiesgoIndicador;
            }
        }

        private int PagIndexRiesgoIndicador
        {
            get
            {
                pagIndexRiesgoIndicador = (int)ViewState["pagIndexRiesgoIndicador"];
                return pagIndexRiesgoIndicador;
            }
            set
            {
                pagIndexRiesgoIndicador = value;
                ViewState["pagIndexRiesgoIndicador"] = pagIndexRiesgoIndicador;
            }
        }

        private int PagIndexDefinicionMetas
        {
            get
            {
                pagIndexDefinicionMetas = (int)ViewState["pagIndexDefinicionMetas"];
                return pagIndexDefinicionMetas;
            }
            set
            {
                pagIndexDefinicionMetas = value;
                ViewState["pagIndexDefinicionMetas"] = pagIndexDefinicionMetas;
            }
        }

        private DataTable infoGridVarRiesgoIndicador;
        private int rowGridVarRiesgoIndicador;
        private int pagIndexVarRiesgoIndicador;

        private DataTable InfoGridVarRiesgoIndicador
        {
            get
            {
                infoGridVarRiesgoIndicador = (DataTable)ViewState["infoGridVarRiesgoIndicador"];
                return infoGridVarRiesgoIndicador;
            }
            set
            {
                infoGridVarRiesgoIndicador = value;
                ViewState["infoGridVarRiesgoIndicador"] = infoGridVarRiesgoIndicador;
            }
        }
        private int RowGridVarRiesgoIndicador
        {
            get
            {
                rowGridVarRiesgoIndicador = (int)ViewState["rowGridVarRiesgoIndicador"];
                return rowGridVarRiesgoIndicador;
            }
            set
            {
                rowGridVarRiesgoIndicador = value;
                ViewState["rowGridVarRiesgoIndicador"] = rowGridVarRiesgoIndicador;
            }
        }
        private int PagIndexVarRiesgoIndicador
        {
            get
            {
                pagIndexVarRiesgoIndicador = (int)ViewState["pagIndexVarRiesgoIndicador"];
                return pagIndexVarRiesgoIndicador;
            }
            set
            {
                pagIndexVarRiesgoIndicador = value;
                ViewState["pagIndexVarRiesgoIndicador"] = pagIndexVarRiesgoIndicador;
            }
        }
        private DataTable infoGridMetaRiesgoIndicador;
        private int rowGridMetaRiesgoIndicador;
        private int pagIndexMetaRiesgoIndicador;

        private DataTable InfoGridMetaRiesgoIndicador
        {
            get
            {
                infoGridMetaRiesgoIndicador = (DataTable)ViewState["infoGridMetaRiesgoIndicador"];
                return infoGridMetaRiesgoIndicador;
            }
            set
            {
                infoGridMetaRiesgoIndicador = value;
                ViewState["infoGridMetaRiesgoIndicador"] = infoGridMetaRiesgoIndicador;
            }
        }
        private int RowGridMetaRiesgoIndicador
        {
            get
            {
                rowGridMetaRiesgoIndicador = (int)ViewState["rowGridMetaRiesgoIndicador"];
                return rowGridMetaRiesgoIndicador;
            }
            set
            {
                rowGridMetaRiesgoIndicador = value;
                ViewState["rowGridMetaRiesgoIndicador"] = rowGridMetaRiesgoIndicador;
            }
        }
        private int PagIndexMetaRiesgoIndicador
        {
            get
            {
                pagIndexMetaRiesgoIndicador = (int)ViewState["pagIndexMetaRiesgoIndicador"];
                return pagIndexMetaRiesgoIndicador;
            }
            set
            {
                pagIndexMetaRiesgoIndicador = value;
                ViewState["pagIndexMetaRiesgoIndicador"] = pagIndexMetaRiesgoIndicador;
            }
        }
        private DataTable infoGridVarForRiesgoIndicador;
        private int rowGridVarForRiesgoIndicador;
        private int pagIndexVarForRiesgoIndicador;

        private DataTable InfoGridVarForRiesgoIndicador
        {
            get
            {
                infoGridVarForRiesgoIndicador = (DataTable)ViewState["infoGridVarForRiesgoIndicador"];
                return infoGridVarForRiesgoIndicador;
            }
            set
            {
                infoGridVarForRiesgoIndicador = value;
                ViewState["infoGridVarForRiesgoIndicador"] = infoGridVarForRiesgoIndicador;
            }
        }
        private int RowGridVarForRiesgoIndicador
        {
            get
            {
                rowGridVarForRiesgoIndicador = (int)ViewState["rowGridVarForRiesgoIndicador"];
                return rowGridVarForRiesgoIndicador;
            }
            set
            {
                rowGridVarForRiesgoIndicador = value;
                ViewState["rowGridVarForRiesgoIndicador"] = rowGridVarForRiesgoIndicador;
            }
        }
        private int PagIndexVarForRiesgoIndicador
        {
            get
            {
                pagIndexVarForRiesgoIndicador = (int)ViewState["pagIndexVarForRiesgoIndicador"];
                return pagIndexVarForRiesgoIndicador;
            }
            set
            {
                pagIndexVarForRiesgoIndicador = value;
                ViewState["pagIndexVarForRiesgoIndicador"] = pagIndexVarForRiesgoIndicador;
            }
        }
        private DataTable infoGridSegRiesgoIndicador;
        private int rowGridSegRiesgoIndicador;
        private int pagIndexSegRiesgoIndicador;

        private DataTable InfoGridSegRiesgoIndicador
        {
            get
            {
                infoGridSegRiesgoIndicador = (DataTable)ViewState["infoGridSegRiesgoIndicador"];
                return infoGridSegRiesgoIndicador;
            }
            set
            {
                infoGridSegRiesgoIndicador = value;
                ViewState["infoGridSegRiesgoIndicador"] = infoGridSegRiesgoIndicador;
            }
        }
        private int RowGridSegRiesgoIndicador
        {
            get
            {
                rowGridSegRiesgoIndicador = (int)ViewState["rowGridSegRiesgoIndicador"];
                return rowGridSegRiesgoIndicador;
            }
            set
            {
                rowGridSegRiesgoIndicador = value;
                ViewState["rowGridSegRiesgoIndicador"] = rowGridSegRiesgoIndicador;
            }
        }
        private int PagIndexSegRiesgoIndicador
        {
            get
            {
                pagIndexSegRiesgoIndicador = (int)ViewState["pagIndexSegRiesgoIndicador"];
                return pagIndexSegRiesgoIndicador;
            }
            set
            {
                pagIndexSegRiesgoIndicador = value;
                ViewState["pagIndexSegRiesgoIndicador"] = pagIndexSegRiesgoIndicador;
            }
        }
        private DataTable infoGridConsultarRiesgos;
        private DataTable InfoGridConsultarRiesgos
        {
            get
            {
                infoGridConsultarRiesgos = (DataTable)ViewState["infoGridConsultarRiesgos"];
                return infoGridConsultarRiesgos;
            }
            set
            {
                infoGridConsultarRiesgos = value;
                ViewState["infoGridConsultarRiesgos"] = infoGridConsultarRiesgos;
            }
        }

        private int rowGridConsultarRiesgos;
        private int RowGridConsultarRiesgos
        {
            get
            {
                rowGridConsultarRiesgos = (int)ViewState["rowGridConsultarRiesgos"];
                return rowGridConsultarRiesgos;
            }
            set
            {
                rowGridConsultarRiesgos = value;
                ViewState["rowGridConsultarRiesgos"] = rowGridConsultarRiesgos;
            }
        }
        private DataTable infoGridRiesgoIndicadorAsoc;
        private int rowGridRiesgoIndicadorAsoc;
        private int pagIndexRiesgoIndicadorAsoc;

        private DataTable InfoGridRiesgoIndicadorAsoc
        {
            get
            {
                infoGridRiesgoIndicadorAsoc = (DataTable)ViewState["infoGridRiesgoIndicadorAsoc"];
                return infoGridRiesgoIndicadorAsoc;
            }
            set
            {
                infoGridRiesgoIndicadorAsoc = value;
                ViewState["infoGridRiesgoIndicadorAsoc"] = infoGridRiesgoIndicadorAsoc;
            }
        }
        private int RowGridRiesgoIndicadorAsoc
        {
            get
            {
                rowGridRiesgoIndicadorAsoc = (int)ViewState["rowGridRiesgoIndicadorAsoc"];
                return rowGridRiesgoIndicadorAsoc;
            }
            set
            {
                rowGridRiesgoIndicadorAsoc = value;
                ViewState["rowGridRiesgoIndicadorAsoc"] = rowGridRiesgoIndicadorAsoc;
            }
        }
        private int PagIndexRiesgoIndicadorAsoc
        {
            get
            {
                pagIndexRiesgoIndicadorAsoc = (int)ViewState["pagIndexRiesgoIndicadorAsoc"];
                return pagIndexRiesgoIndicadorAsoc;
            }
            set
            {
                pagIndexRiesgoIndicadorAsoc = value;
                ViewState["pagIndexRiesgoIndicadorAsoc"] = pagIndexRiesgoIndicadorAsoc;
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
        #region LoadMetodos
        private bool mtdCargarDDLs(ref string strErrMsg)
        {
            bool booResult = false;

            booResult = mtdLoadDDLCadenaValor(ref strErrMsg);
            booResult = mtdLoadDDLFrecuenciaMedicion(ref strErrMsg);

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
                {
                    strErrMsg = "No hay Macroprocesos para mostrar";
                    booResult = false;
                }
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
                        rfvProceso.Enabled = true;
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
                        booResult = false;
                        rfvSubproceso.Enabled = true;
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
        private bool mtdLoadDDLFrecuenciaMedicion(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsDTOfrecuenciaMedicion> lstFrecuencias = new List<clsDTOfrecuenciaMedicion>();
            clsBLLFrecuenciaMedicion cFrecuencia = new clsBLLFrecuenciaMedicion();
            #endregion Vars

            try
            {
                lstFrecuencias = cFrecuencia.mtdConsultarFrecuenciaMedicion(true, ref strErrMsg);
                ddlFrecuenciaMedicion.Items.Clear();
                
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstFrecuencias != null)
                    {
                        int intCounter = 0;

                        foreach (clsDTOfrecuenciaMedicion objFrecuencia in lstFrecuencias)
                        {
                            ddlFrecuenciaMedicion.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objFrecuencia.strFrecuenciaMedicion, objFrecuencia.intIdFrecuenciaMedicion.ToString().Trim()));
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
                strErrMsg = string.Format("Error durante la consulta de las frecuencias. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }
        private void loadDDLFormatoAdd()
        {
            if (DropDownListFormato.Items.Count > 0)
                DropDownListFormato.Items.Clear();
            if (DropDownListFormatoUp.Items.Count > 0)
                DropDownListFormatoUp.Items.Clear();
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.LFormatos();
                DropDownListFormato.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "---"));
                DropDownListFormatoUp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "---"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownListFormato.Items.Insert(i+1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                    DropDownListFormatoUp.Items.Insert(i+1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Erro al cargar Formatos. " + ex.Message, 2,"Atención");
            }
        }
        private void loadDDLDetalleFrecuencias()
        {
            if (ddlDetalleFrecuencias.Items.Count > 0)
                ddlDetalleFrecuencias.Items.Clear();
            try
            {
                string strErrMsg = string.Empty;
                DataTable dtInfo = new DataTable();
                clsBLLFrecuenciaMedicion cFrecuencia = new clsBLLFrecuenciaMedicion();
                dtInfo = cFrecuencia.mtdConsultarDetalleFrecuencia(ref strErrMsg, Convert.ToInt32(ddlFrecuenciaMedicion.SelectedValue));

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlDetalleFrecuencias.Items.Insert(i, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleFecruencia"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Erro al cargar El detalle de la frecuencia. " + ex.Message, 2, "Atención");
            }
        }
        #endregion LoadMetodos
        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndexRiesgoIndicador = 0;
            PagIndexDefinicionMetas = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            /*mtdLoadEvaluacionProveedor(ref strErrMsg);*/
            if (!mtdCargarDDLs(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if(!IsPostBack)
                PopulateTreeView();
            if (!mtdLoadRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }
        protected void mtdResetFields()
        {
            BodyFormRI.Visible = false;
            BodyGridRI.Visible = true;

            txtId.Text = string.Empty;
            txtNombreIndicador.Text = string.Empty;
            txtObjetivoIndicador.Text = string.Empty;
            ddlCadenaValor.SelectedIndex = 0;
            ddlCadenaValor.Enabled = true;
            ddlMacroproceso.Items.Clear();
            ddlMacroproceso.Enabled = true;
            ddlProceso.Items.Clear();
            ddlProceso.Enabled = true;
            rfvProceso.Enabled = false;
            ddlSubproceso.Items.Clear();
            ddlSubproceso.Enabled = true;
            rfvSubproceso.Enabled = false;
            tbxResponsable.Text = string.Empty;
            ddlFrecuenciaMedicion.SelectedIndex = 0;
            TextBox5.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox8.Text = string.Empty;
            mtdResetFieldVariables();
            mtdResetFieldMetas();
            mtdResetFieldSeguimiento();
            dvContentDetalleIndicador.Visible = false;
            Dbutton.Visible = false;

            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;
            Session["IdVariable"] = "";
            Session["IdRiesgoIndicador"] = "";
            Session["IdSeguimiento"] = "";
        }
        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }
        private bool mtdInsertarRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOriesgosIndicadores objRiesgoInd = new clsDTOriesgosIndicadores();
            clsDTOprocesoRiesgoIndicador objProcesoInd = new clsDTOprocesoRiesgoIndicador();
            clsBLLriesgosIndicadores cRiesgoInd = new clsBLLriesgosIndicadores();
            clsBLLprocesoRiesgoIndicador cProcesoInd = new clsBLLprocesoRiesgoIndicador();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objRiesgoInd.strNombreIndicador = Sanitizer.GetSafeHtmlFragment(txtNombreIndicador.Text);
            objRiesgoInd.strObjetivoIndicador = Sanitizer.GetSafeHtmlFragment(txtObjetivoIndicador.Text);
            objRiesgoInd.intIdResponsableMedicion = Convert.ToInt32(lblIdDependencia4.Text);
            objRiesgoInd.intIdFrecuenciaMedicion = Convert.ToInt32(ddlFrecuenciaMedicion.SelectedValue);
            objRiesgoInd.dtFechaCreacion = DateTime.Now;
            objRiesgoInd.intUsuarioCreacion = Convert.ToInt32(Session["IdUsuario"].ToString());

            booResult = cRiesgoInd.mtdInsertarRiesgoIndicador(objRiesgoInd, ref strErrMsg);
            if (ddlSubproceso.SelectedValue.ToString() != "0" && ddlSubproceso.SelectedValue.ToString() != "")
            {
                objProcesoInd.intIdProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                objProcesoInd.strNombreProceso = ddlSubproceso.SelectedItem.Text;
                objProcesoInd.intTipoProceso = 3;
            }
            else
            {
                if (ddlProceso.SelectedValue.ToString() != "0" && ddlProceso.SelectedValue.ToString() != "")
                {
                    objProcesoInd.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                    objProcesoInd.strNombreProceso = ddlProceso.SelectedItem.Text;
                    objProcesoInd.intTipoProceso = 2;
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue.ToString() != "0" && ddlMacroproceso.SelectedValue.ToString() != "")
                    {
                        objProcesoInd.intIdProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                        objProcesoInd.strNombreProceso = ddlMacroproceso.SelectedItem.Text;
                        objProcesoInd.intTipoProceso = 1;
                    }
                    else
                    {
                        strErrMsg = "Debe seleccionar el Macroproceso para seguir";
                        return booResult;
                    }
                }
            }
            int IdRiesgoIndicador = cRiesgoInd.mtdGetLastId(ref strErrMsg);
            txtId.Text = IdRiesgoIndicador.ToString();
            Session["IdRiesgoIndicador"] = IdRiesgoIndicador;
            objProcesoInd.intIdRiesgoIndicador = IdRiesgoIndicador;
            booResult = cProcesoInd.mtdInsertarProcesoRiesgoIndicador(objProcesoInd, ref strErrMsg);

            objRiesgoInd.intIdRiesgoIndicador = IdRiesgoIndicador;
            objRiesgoInd.intIProcesoIndicador = cProcesoInd.mtdGetLastId(ref strErrMsg);
            booResult = cRiesgoInd.mtdActualizaProcesoIndicador(objRiesgoInd, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Indicador del Riesgo registrado exitosamente";
            }
            else
            {
                strErrMsg = "Error al registrar el Indicador del Riesgo";
            }
            mtdGenerarNotificacion(IdRiesgoIndicador, Sanitizer.GetSafeHtmlFragment(txtNombreIndicador.Text));
            return booResult;
        }
        private bool mtdActualizarRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOriesgosIndicadores objRiesgoInd = new clsDTOriesgosIndicadores();
            clsDTOprocesoRiesgoIndicador objProcesoInd = new clsDTOprocesoRiesgoIndicador();
            clsBLLriesgosIndicadores cRiesgoInd = new clsBLLriesgosIndicadores();
            clsBLLprocesoRiesgoIndicador cProcesoInd = new clsBLLprocesoRiesgoIndicador();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            try
            {
                
                objRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
                objRiesgoInd.strNombreIndicador = Sanitizer.GetSafeHtmlFragment(txtNombreIndicador.Text);
                objRiesgoInd.strObjetivoIndicador = Sanitizer.GetSafeHtmlFragment(txtObjetivoIndicador.Text);
                objRiesgoInd.intIdResponsableMedicion = Convert.ToInt32(lblIdDependencia4.Text);
                objRiesgoInd.intIdFrecuenciaMedicion = Convert.ToInt32(ddlFrecuenciaMedicion.SelectedValue);


                booResult = cRiesgoInd.mtdActualizarRiesgoIndicador(objRiesgoInd, ref strErrMsg);

                if (booResult == true)
                {
                    strErrMsg = "Indicador del Riesgo actualizado exitosamente";
                }
                else
                {
                    strErrMsg = "Error al actualizar el Indicador del Riesgo";
                }
            }catch(Exception ex)
            {
                strErrMsg = "Error en la ejecución: " + ex;
            }
            
            return booResult;
        }
        private bool mtdLoadRiesgosIndicadores(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOriesgosIndicadores objRiesgoInd = new clsDTOriesgosIndicadores();
            List<clsDTOriesgosIndicadores> lstRiesgoInd = new List<clsDTOriesgosIndicadores>();
            clsBLLriesgosIndicadores cRiesgoInd = new clsBLLriesgosIndicadores();
            #endregion Vars
            lstRiesgoInd = cRiesgoInd.mtdConsultarRiesgosIndicadores(booResult, ref strErrMsg);

            if (lstRiesgoInd != null)
            {
                mtdLoadRiesgosIndicadores();
                mtdLoadRiesgosIndicadores(lstRiesgoInd);
                GVriesgosIndicadores.DataSource = lstRiesgoInd;
                GVriesgosIndicadores.PageIndex = pagIndexRiesgoIndicador;
                GVriesgosIndicadores.DataBind();
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
        private void mtdLoadRiesgosIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdRiesgoIndicador", typeof(string));
            grid.Columns.Add("strNombreIndicador", typeof(string));
            grid.Columns.Add("strObjetivoIndicador", typeof(string));
            grid.Columns.Add("intIProcesoIndicador", typeof(string));
            grid.Columns.Add("intIdProceso", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("intIdResponsableMedicion", typeof(string));
            grid.Columns.Add("strResponsableMedicion", typeof(string));
            grid.Columns.Add("intIdFrecuenciaMedicion", typeof(string));
            grid.Columns.Add("strFrecuenciaMedicion", typeof(string));
            grid.Columns.Add("intIdRiesgoAsociado", typeof(string));
            grid.Columns.Add("strCodRiesgo", typeof(string));
            grid.Columns.Add("strNombreRiesgo", typeof(string));
            grid.Columns.Add("intIdFormula", typeof(string));
            grid.Columns.Add("strNominador", typeof(string));
            grid.Columns.Add("strDenominador", typeof(string));
            grid.Columns.Add("intIdMeta", typeof(string));
            grid.Columns.Add("intIdEsquemaSeguimiento", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaCreacion", typeof(string));
            grid.Columns.Add("booActivo", typeof(string));

            GVriesgosIndicadores.DataSource = grid;
            GVriesgosIndicadores.DataBind();
            InfoGridRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstRiesgoInd">Lista con los Indicadores de riesgos</param>
        private void mtdLoadRiesgosIndicadores(List<clsDTOriesgosIndicadores> lstRiesgoInd)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOriesgosIndicadores objRiesgoIndicador in lstRiesgoInd)
            {

                InfoGridRiesgoIndicador.Rows.Add(new Object[] {
                    objRiesgoIndicador.intIdRiesgoIndicador.ToString().Trim(),
                    objRiesgoIndicador.strNombreIndicador.ToString().Trim(),
                    objRiesgoIndicador.strObjetivoIndicador.ToString().Trim(),
                    objRiesgoIndicador.intIProcesoIndicador.ToString().Trim(),
                    objRiesgoIndicador.intIdProceso.ToString().Trim(),
                    objRiesgoIndicador.strNombreProceso.ToString().Trim(),
                    objRiesgoIndicador.intIdResponsableMedicion.ToString().Trim(),
                    objRiesgoIndicador.strResponsableMedicion.ToString().Trim(),
                    objRiesgoIndicador.intIdFrecuenciaMedicion.ToString().Trim(),
                    objRiesgoIndicador.strFrecuenciaMedicion.ToString().Trim(),
                    objRiesgoIndicador.intIdRiesgoAsociado.ToString().Trim(),
                    objRiesgoIndicador.strCodRiesgo.ToString().Trim(),
                    objRiesgoIndicador.strNombreRiesgo.ToString().Trim(),
                    objRiesgoIndicador.intIdFormula.ToString().Trim(),
                    objRiesgoIndicador.strNominador.ToString().Trim(),
                    objRiesgoIndicador.strDenominador.ToString().Trim(),
                    objRiesgoIndicador.intIdMeta.ToString().Trim(),
                    objRiesgoIndicador.intIdEsquemaSeguimiento.ToString().Trim(),
                    objRiesgoIndicador.strUsuario.ToString().Trim(),
                    objRiesgoIndicador.dtFechaCreacion.ToString().Trim(),
                    objRiesgoIndicador.booActivo.ToString().Trim()
                    });
            }
        }
        private void mtdShowUpdate(int RowGrid)
        {
            GridViewRow row = GVriesgosIndicadores.Rows[RowGrid];
            var colsNoVisible = GVriesgosIndicadores.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;

            BodyGridRI.Visible = false;
            BodyFormRI.Visible = true;

            #region DatosControl
            clsDTOprocesoRiesgoIndicador objProceso = new clsDTOprocesoRiesgoIndicador();
            clsBLLprocesoRiesgoIndicador cProcInd = new clsBLLprocesoRiesgoIndicador();
            mtdCargarDDLs(ref strErrMsg);
            mtdLoadDDLFrecuenciaMedicion(ref strErrMsg);

            txtId.Text = row.Cells[0].Text;
            Session["IdRiesgoIndicador"] = "";
            int IdRiesgoIndicador = Convert.ToInt32(row.Cells[0].Text);
            int IdProceso = Convert.ToInt32(colsNoVisible[7].ToString());
            int IdTipoProceso = cProcInd.mtdGetTipoProceso(ref strErrMsg, IdProceso,  IdRiesgoIndicador);
            
            
            //LtexProceso.Text = ((Label)row.FindControl("NombreProceso")).Text;

            //object[] objProcesos = cProcInd.mtdConsultarProcIndicadorIdProcesoDV(objProcInd, ref strErrMsg, IdTipoProceso);
            object[] objProcesos = cProcInd.mtdConsultarProcesos(ref strErrMsg, IdProceso, IdTipoProceso);
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
            //LtexProceso.Visible = true;
            ddlSubproceso.Enabled = false;
            ddlProceso.Enabled = false;
            ddlMacroproceso.Enabled = false;
            ddlCadenaValor.Enabled = false;

            txtNombreIndicador.Text = ((Label)row.FindControl("strNombreIndicador")).Text;
            txtObjetivoIndicador.Text = ((Label)row.FindControl("strObjetivoIndicador")).Text;
            lblIdDependencia4.Text = colsNoVisible[9].ToString();
            tbxResponsable.Text = ((Label)row.FindControl("strResponsableMedicion")).Text;

            string IdFrecuencia =  colsNoVisible[6].ToString();
            ddlFrecuenciaMedicion.SelectedValue = IdFrecuencia;

            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();

            dvContentDetalleIndicador.Visible = true;
            Dbutton.Visible = true;
            #endregion
            #region Load DetalleIndicador
            //loadDDLFormatoAdd();
            if (!mtdLoadVariablesRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            loadDDLDetalleFrecuencias();
            if (!mtdLoadMetasRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (!mtdLoadVariablesFormulaRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (!mtdLoadSeguimientoRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (!mtdLoadFormulaRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if(!mtdLoadRiesgosAsociado(ref strErrMsg))
            {
                trTitutloConsultarRiesgo.Visible = true;
                trCodRiesgoSearch.Visible = true;
                trNombreRiesgoSearch.Visible = true;
                trButtonsSearch.Visible = true;
            }
            #endregion Load DetalleIndicador
        }
        private void mtdGenerarNotificacion(int IdRiesgoIndicador, string NombreRiesgo)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "REGISTRO INDICADOR DE RIESGO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + IdRiesgoIndicador + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Indicador de Riesgo : " + NombreRiesgo + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado cabo el registro de un Indicador de Riesgo, el cual has sido asignado como responsable.<br>";
                TextoAdicional = TextoAdicional + " Fecha Registro : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario Registro : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Registro : " + Session["nombreUsuario"].ToString() + "<br>";
                string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;
                string selectCommand = string.Empty;
                int IdEventoNotificacion = 0;
                selectCommand = "SELECT [IdEvento] FROM [Notificaciones].[Evento] "+
                    " where NombreEvento = 'Riesgos(Creación Indicador Riesgo)'";

                dad = new SqlDataAdapter(selectCommand, conString);
                dad.Fill(dtblDiscuss);
                view = new DataView(dtblDiscuss);

                foreach (DataRowView row in view)
                {
                    IdEventoNotificacion = Convert.ToInt32(row["IdEvento"].ToString().Trim());
                }
                boolEnviarNotificacion(IdEventoNotificacion, IdRiesgoIndicador, Convert.ToInt16(lblIdDependencia4.Text.Trim()), System.DateTime.Now.ToString().Trim(), TextoAdicional);

            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al generar la notificacion. " + ex.Message, 1 , "Atención");
            }
        }
        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = string.Empty, Copia = string.Empty, Asunto = string.Empty, Otros = string.Empty, Cuerpo = string.Empty, NroDiasRecordatorio = string.Empty;
            string selectCommand = string.Empty, AJefeInmediato = string.Empty, AJefeMediato = string.Empty, RequiereFechaCierre = string.Empty;
            string idJefeInmediato = string.Empty, idJefeMediato = string.Empty;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Variables

            try
            {
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Destinatario
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
                #endregion

                #region correo del Jefe Inmediato
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
                #endregion

                #region correo del Jefe Mediato
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
                #endregion

                #region Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.Insert();
                #endregion
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                #region Restro
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
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

                    #region Destinatario
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region Copia
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region Otros
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
                    #endregion
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                }
            }

            return (err);
        }
        #endregion Metodos
        #region ButtonsEvents
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            BodyFormRI.Visible = true;
            BodyGridRI.Visible = false;

            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertarRiesgoIndicador(ref strErrMsg) == true)
            {
                IBinsertGVC.Visible = false;
                IBupdateGVC.Visible = true;
                omb.ShowMessage(strErrMsg, 3, "Atención");
                dvContentDetalleIndicador.Visible = true;
                #region Load DetalleIndicador
                loadDDLFormatoAdd();
                if (!mtdLoadVariablesRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                loadDDLDetalleFrecuencias();
                if (!mtdLoadMetasRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                if (!mtdLoadVariablesFormulaRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                if (!mtdLoadSeguimientoRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                if (!mtdLoadFormulaRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                if (!mtdLoadRiesgosAsociado(ref strErrMsg))
                {
                    trTitutloConsultarRiesgo.Visible = true;
                    trCodRiesgoSearch.Visible = true;
                    trNombreRiesgoSearch.Visible = true;
                    trButtonsSearch.Visible = true;
                }
                #endregion Load DetalleIndicador
            }
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdActualizarRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
            mtdStard();
        }
        #endregion ButtonsEvents
        #region DLLEvents
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
        #endregion DLLEvents

        protected void GVriesgosIndicadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    break;
                case "Activar":
                    mtdActivar(RowGrid);
                    break;
            }
        }
        private void mtdActivar(int Rowgrid)
        {
            string strEstado = string.Empty;
            //int booEstado = Convert.ToInt32(GVriesgosIndicadores.Rows[Rowgrid].ToString().Trim());
            
            GridViewRow row = GVriesgosIndicadores.Rows[Rowgrid];
            Session["IdRiesgoIndicador"] = row.Cells[0].Text;
            int booEstado = Convert.ToInt32(((Label)row.FindControl("booActivo")).Text);
            if (booEstado == 1)
            {
                Session["activo"] = 0;
                strEstado = "inactivar";
            }
            else
            {
                Session["activo"] = 1;
                strEstado = "activar";
            }

            lblMsgBox.Text = string.Format("Desea {0} el Indicador del Riesgo?", strEstado);
            mpeMsgBox.Show();
        }
        protected void btnModificarEstado_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            mpeMsgBox.Hide();
            if(Session["IdRiesgoIndicador"].ToString() != "" && Session["IdRiesgoIndicador"] != null)
            {
                try
                {
                    if (mtdActualizarEstado(ref strErrMsg) == true)
                    {
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                        mtdResetFields();
                        mtdStard();
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
                catch (Exception ex)
                {
                    omb.ShowMessage("Error al inactivar el Indicador del Riesgo.<br/>Descripción: " + ex.Message.ToString(), 1, "Atención");
                }
            }
            if (Session["IdSeguimiento"] != null)
            {
                if(Session["IdSeguimiento"].ToString() != "")
                    mtdEliminarSeguimiento(Convert.ToInt32(Session["RowGrid"].ToString()));
            }
            if (Session["IdVariable"] != null)
            {
                if(Session["IdVariable"].ToString() != "")
                    mtdEliminarVariable(Convert.ToInt32(Session["RowGrid"].ToString()));
            }
            Session["IdRiesgoIndicador"] = "";
            Session["IdSeguimiento"] = "";
            Session["IdVariable"] = "";


        }
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;

            clsDTOriesgosIndicadores objRiesgoInd = new clsDTOriesgosIndicadores();
            int booEstado = Convert.ToInt32(Session["activo"].ToString());
            string strEstado = string.Empty;
             //Convert.ToInt32(Session["activo"].ToString());
            objRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Session["IdRiesgoIndicador"].ToString());
            clsBLLriesgosIndicadores cCategoria = new clsBLLriesgosIndicadores();
            if (booEstado == 1)
            {
                objRiesgoInd.booActivo = Convert.ToInt32(Session["activo"].ToString());
                strEstado = "activó";
            }
            else
            {
                objRiesgoInd.booActivo = Convert.ToInt32(Session["activo"].ToString());
                strEstado = "inactivó";
            }
            booResult = cCategoria.mtdActualizaEstadoRiesgoIndicador(objRiesgoInd, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "El indicador se "+ strEstado +" exitosamente";

            return booResult;
        }
        #region Metodos Variables
        private bool mtdInsertarVariableRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOvariableRiesgoIndicador objVarRiesgoInd = new clsDTOvariableRiesgoIndicador();
            clsBLLvariableRiesgoIndicador cVarRiesgoInd = new clsBLLvariableRiesgoIndicador();

            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objVarRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
            objVarRiesgoInd.strDescripcion = Sanitizer.GetSafeHtmlFragment(TextBox1.Text);
            objVarRiesgoInd.dblValorVariable = Convert.ToDouble(1);
            objVarRiesgoInd.intIdFormato = Convert.ToInt32(DropDownListFormato.SelectedValue);
            objVarRiesgoInd.dtFechaCreacion = DateTime.Now;
            objVarRiesgoInd.intUsuarioCreacion = Convert.ToInt32(Session["IdUsuario"].ToString());

            booResult = cVarRiesgoInd.mtdInsertarVariableRiesgoIndicador(objVarRiesgoInd, ref strErrMsg);
            
            if (booResult == true)
            {
                strErrMsg = "Variable del Indicador registrada exitosamente";
                TabContainerIndicadores.ActiveTabIndex = 1;
                mtdResetFieldVariables();
                if (!mtdLoadVariablesFormulaRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                strErrMsg = "Error al registrar la Variable del Indicador";
            }
            return booResult;
        }
        private bool mtdLoadVariablesRiesgosIndicadores(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOvariableRiesgoIndicador objVarRiesgoInd = new clsDTOvariableRiesgoIndicador();
            List<clsDTOvariableRiesgoIndicador> lstVarRiesgoInd = new List<clsDTOvariableRiesgoIndicador>();
            clsBLLvariableRiesgoIndicador cVarRiesgoInd = new clsBLLvariableRiesgoIndicador();
            #endregion Vars
            lstVarRiesgoInd = cVarRiesgoInd.mtdConsultarVariableRiesgoIndicador(booResult, ref strErrMsg, Convert.ToInt32(txtId.Text));

            if (lstVarRiesgoInd != null)
            {
                mtdLoadVariablesRiesgosIndicadores();
                mtdLoadVariablesRiesgosIndicadores(lstVarRiesgoInd);
                GridViewVariables.DataSource = lstVarRiesgoInd;
                GridViewVariables.PageIndex = pagIndexVarRiesgoIndicador;
                GridViewVariables.DataBind();
                GridViewVariables.Visible = true;
                booResult = true;
            }
            else
            {
                GridViewVariables.Visible = false;
                GridViewVariables.DataSource = null;
                GridViewVariables.DataBind();
                strErrMsg = "No hay registros de Variables";
            }

            return booResult;
        }
        private void mtdResetFieldVariables()
        {
            TbCrearVariables.Visible = true;
            TbAddVaiables.Visible = false;
            TbModificaVariable.Visible = false;

            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            txtValorVariable.Text = string.Empty;
            txtValorVariableUp.Text = string.Empty;
            DropDownListFormato.Items.Clear();
            DropDownListFormatoUp.Items.Clear();

        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadVariablesRiesgosIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdVariableRiesgoIndicador", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("dblValorVariable", typeof(string));
            grid.Columns.Add("intIdFormato", typeof(string));
            grid.Columns.Add("strFormato", typeof(string));

            GridViewVariables.DataSource = grid;
            GridViewVariables.DataBind();
            InfoGridVarRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstVarRiesgoInd">Lista con las Variables de Indicadores de riesgos</param>
        private void mtdLoadVariablesRiesgosIndicadores(List<clsDTOvariableRiesgoIndicador> lstVarRiesgoInd)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOvariableRiesgoIndicador objVarRiesgoIndicador in lstVarRiesgoInd)
            {

                InfoGridVarRiesgoIndicador.Rows.Add(new Object[] {
                    objVarRiesgoIndicador.intIdVariableRiesgoIndicador.ToString().Trim(),
                    objVarRiesgoIndicador.strDescripcion.ToString().Trim(),
                    objVarRiesgoIndicador.dblValorVariable.ToString().Trim(),
                    objVarRiesgoIndicador.intIdFormato.ToString().Trim(),
                    objVarRiesgoIndicador.strFormato.ToString().Trim()
                    });
            }
        }
        private void mtdShowUpdateVariable(int RowGrid)
        {
            GridViewRow row = GridViewVariables.Rows[RowGrid];
            loadDDLFormatoAdd();
            var colsNoVisible = GridViewVariables.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;
            TextBox2.Text = ((Label)row.FindControl("strDescripcion")).Text;
            txtValorVariableUp.Text = "1";
            DropDownListFormatoUp.SelectedValue = colsNoVisible[0].ToString().Trim();
            Label17.Text = colsNoVisible[1].ToString().Trim();

            TbCrearVariables.Visible = false;
            TbAddVaiables.Visible = false;
            TbModificaVariable.Visible = true;
        }
        private void mtdEliminarVariable(int RowGrid)
        {
            GridViewRow row = GridViewVariables.Rows[RowGrid];
            var colsNoVisible = GridViewVariables.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;
            string IdVariable = colsNoVisible[1].ToString().Trim();
            string IdRiesgoIndicador = txtId.Text;
            bool booResult = false;
            clsBLLvariableRiesgoIndicador cVariable = new clsBLLvariableRiesgoIndicador();
            booResult = cVariable.mtdEliminarVariableRiesgoIndicador(IdVariable, ref strErrMsg, IdRiesgoIndicador);

            if (booResult == true)
            {
                strErrMsg = "Variable del Indicador eliminada exitosamente";
                omb.ShowMessage(strErrMsg, 3, "Atención");
                TabContainerIndicadores.ActiveTabIndex = 1;
                mtdResetFieldVariables();
                if (!mtdLoadVariablesRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                if (!mtdLoadVariablesFormulaRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                if (!mtdLoadFormulaRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                strErrMsg = "Error al eliminar la Variable del Indicador";
                omb.ShowMessage(strErrMsg, 1, "Atención");
            }

        }
        private bool mtdActualizarVariableRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOvariableRiesgoIndicador objVarRiesgoInd = new clsDTOvariableRiesgoIndicador();
            clsBLLvariableRiesgoIndicador cVarRiesgoInd = new clsBLLvariableRiesgoIndicador();

            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objVarRiesgoInd.intIdVariableRiesgoIndicador = Convert.ToInt32(Label17.Text);
            objVarRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
            objVarRiesgoInd.strDescripcion = Sanitizer.GetSafeHtmlFragment(TextBox2.Text);
            objVarRiesgoInd.dblValorVariable = Convert.ToDouble(1);
            objVarRiesgoInd.intIdFormato = Convert.ToInt32(DropDownListFormatoUp.SelectedValue);

            booResult = cVarRiesgoInd.mtdActualizarVariableRiesgoIndicador(objVarRiesgoInd, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Variable del Indicador actualizada exitosamente";
                TabContainerIndicadores.ActiveTabIndex = 1;
                mtdResetFieldVariables();
            }
            else
            {
                strErrMsg = "Error al actualizar la Variable del Indicador";
            }
            return booResult;
        }
        #endregion Metodos Variables
        #region Eventos Variables
        protected void BtnAdicionaVariable_Click1(object sender, ImageClickEventArgs e)
        {
            TbCrearVariables.Visible = false;
            TbAddVaiables.Visible = true;
            TbModificaVariable.Visible = false;

            loadDDLFormatoAdd();
        }

        protected void BtnNewVariable_Click(object sender, ImageClickEventArgs e)
        {
            TbCrearVariables.Visible = false;
            TbAddVaiables.Visible = true;
        }

        protected void BtnGuardarVariable_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertarVariableRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                if (!mtdLoadVariablesRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }
        protected void GridViewVariables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ModificarVariable":
                    mtdShowUpdateVariable(RowGrid);
                    break;
                case "EliminarVariable":
                    //mtdEliminarVariable(RowGrid);
                    Session["RowGrid"] = RowGrid;
                    mtdActivarVariable(RowGrid);
                    break;
            }
        }
        private void mtdActivarVariable(int Rowgrid)
        {
            string strEstado = string.Empty;
            GridViewRow row = GridViewVariables.Rows[Rowgrid];
            var colsNoVisible = GridViewVariables.DataKeys[Rowgrid].Values;
            string strErrMsg = string.Empty;
            string IdVariable = colsNoVisible[1].ToString().Trim();
            //int booEstado = Convert.ToInt32(InfoGridCVC.Rows[RowGridCVC][6].ToString().Trim());
            //GridViewRow row = GVcategoriasVariablesControl.Rows[Rowgrid];
            Session["IdVariable"] = IdVariable;
            strEstado = "Eliminar";
            /*if (booEstado == 1)
            {
                Session["activo"] = 0;
                strEstado = "inactivar";
            }
            else
            {
                Session["activo"] = 1;
                strEstado = "activar";
            }*/

            lblMsgBox.Text = string.Format("Desea {0} la variable del Indicador?", strEstado);
            mpeMsgBox.Show();
        }
        protected void BtnModificaVariable_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdActualizarVariableRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                if (!mtdLoadVariablesRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }

        protected void BtnCancelaVariable_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFieldVariables();
        }

        protected void BtnCancelaAUpVariable_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFieldVariables();
        }
        #endregion Eventos Variables

        #region Metodos Metas
        private bool mtdInsertarMetaRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOmetasRiesgoIndicador objMetaRiesgoInd = new clsDTOmetasRiesgoIndicador();
            clsBLLmetaRiesgoIndicador cMetaRiesgoInd = new clsBLLmetaRiesgoIndicador();
            clsDTOriesgosIndicadores objRieInd = new clsDTOriesgosIndicadores();
            clsBLLriesgosIndicadores cRieInd = new clsBLLriesgosIndicadores();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objMetaRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
            objMetaRiesgoInd.dblMeta = Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(TextBox10.Text));
            int IdFrecuencia = Convert.ToInt32(ddlFrecuenciaMedicion.SelectedValue);
            if (IdFrecuencia == 2)
            {
                objMetaRiesgoInd.intIdDetalleFrecuencia = 0;
                objMetaRiesgoInd.strValorOtraFrecuencia = Sanitizer.GetSafeHtmlFragment(TextBox12.Text);
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
            {
                objMetaRiesgoInd.intIdDetalleFrecuencia = Convert.ToInt32(ddlDetalleFrecuencias.SelectedValue);
                objMetaRiesgoInd.strValorOtraFrecuencia = Sanitizer.GetSafeHtmlFragment(TextBox12.Text);
                if(IdFrecuencia == 3 || IdFrecuencia == 4)
                {
                    objMetaRiesgoInd.strMes = ddlMesMetas.SelectedValue;
                }else
                {
                    objMetaRiesgoInd.strMes = ddlDetalleFrecuencias.SelectedItem.Text;
                }
            }
                
            if(IdFrecuencia == 9)
            {
                objMetaRiesgoInd.intIdDetalleFrecuencia = 0;
                objMetaRiesgoInd.strValorOtraFrecuencia = Sanitizer.GetSafeHtmlFragment(txtFrecuenciaAno.Text);
            }
            objMetaRiesgoInd.dtFechaCreacion = DateTime.Now;
            objMetaRiesgoInd.intUsuarioCreacion = Convert.ToInt32(Session["IdUsuario"].ToString());
            objMetaRiesgoInd.strAño = Sanitizer.GetSafeHtmlFragment(txbAñoMeta.Text);
            
            booResult = cMetaRiesgoInd.mtdInsertarMetaRiesgoIndicador(objMetaRiesgoInd, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Meta del Indicador registrada exitosamente";
                TabContainerIndicadores.ActiveTabIndex = 2;
                mtdResetFieldMetas();
                if (!mtdLoadMetasRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                objRieInd.intIdMeta = cMetaRiesgoInd.mtdGetLastId(ref strErrMsg);
                objRieInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
                cRieInd.mtdActualizaMetaRiesgoIndicador(objRieInd, ref strErrMsg);
            }
            else
            {
                strErrMsg = "Error al registrar la Meta del Indicador";
            }
            return booResult;
        }
        private bool mtdLoadMetasRiesgosIndicadores(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOmetasRiesgoIndicador objMetaRiesgoInd = new clsDTOmetasRiesgoIndicador();
            List<clsDTOmetasRiesgoIndicador> lstMetaRiesgoInd = new List<clsDTOmetasRiesgoIndicador>();
            clsBLLmetaRiesgoIndicador cMetaRiesgoInd = new clsBLLmetaRiesgoIndicador();
            #endregion Vars
            lstMetaRiesgoInd = cMetaRiesgoInd.mtdConsultarMetaRiesgoIndicador(booResult, ref strErrMsg, Convert.ToInt32(txtId.Text));

            if (lstMetaRiesgoInd != null)
            {
                mtdLoadMetasRiesgosIndicadores();
                mtdLoadMetasRiesgosIndicadores(lstMetaRiesgoInd);
                GridViewMetas.DataSource = lstMetaRiesgoInd;
                GridViewMetas.PageIndex = pagIndexMetaRiesgoIndicador;
                GridViewMetas.DataBind();
                Td56.Visible = true;
                booResult = true;
                if (ddlFrecuenciaMedicion.SelectedValue == "2" || ddlFrecuenciaMedicion.SelectedValue == "3")
                {
                    TdAñoMeta.Visible = true;
                    TdValorAñoMeta.Visible = true;
                    TdmesMeta.Visible = true;
                    TdvalorMesMeta.Visible = true;
                }else
                {
                    TdAñoMeta.Visible = false;
                    TdValorAñoMeta.Visible = false;
                    TdmesMeta.Visible = false;
                    TdvalorMesMeta.Visible = false;
                }
            }
            else
            {
                GridViewMetas.DataSource = null;
                GridViewMetas.DataBind();
                GridViewMetas.Visible = true;
                strErrMsg = "No hay registro de Metas";
            }

            return booResult;
        }
        private void mtdResetFieldMetas()
        {
            TbMetas.Visible = true;
            TbAddMetas.Visible = false;
            Td56.Visible = false;
            TextBox10.Text = string.Empty;
            ddlDetalleFrecuencias.ClearSelection();
            txtFrecuenciaAno.Text = string.Empty;
            txtFrecuenciaAno.Visible = false;
            TextBox12.Text = string.Empty;
            TextBox12.Visible = false;
            txbAñoMeta.Text = string.Empty;
            ddlMesMetas.SelectedIndex = 0;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadMetasRiesgosIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdMeta", typeof(string));
            grid.Columns.Add("intIdRiesgoIndicador", typeof(string));
            grid.Columns.Add("dblMeta", typeof(string));
            grid.Columns.Add("intIdDetalleFrecuencia", typeof(string));
            grid.Columns.Add("strDetalleFrecuencia", typeof(string));
            grid.Columns.Add("strValorOtraFrecuencia", typeof(string));
            grid.Columns.Add("strAño", typeof(string));
            grid.Columns.Add("strMes", typeof(string));

            GridViewMetas.DataSource = grid;
            GridViewMetas.DataBind();
            InfoGridMetaRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstVarRiesgoInd">Lista con las Variables de Indicadores de riesgos</param>
        private void mtdLoadMetasRiesgosIndicadores(List<clsDTOmetasRiesgoIndicador> lstMetasRiesgoInd)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOmetasRiesgoIndicador objMetaRiesgoIndicador in lstMetasRiesgoInd)
            {

                InfoGridMetaRiesgoIndicador.Rows.Add(new Object[] {
                    objMetaRiesgoIndicador.intIdMeta .ToString().Trim(),
                    objMetaRiesgoIndicador.intIdRiesgoIndicador.ToString().Trim(),
                    objMetaRiesgoIndicador.strDetalleFrecuencia.ToString().Trim(),
                    objMetaRiesgoIndicador.dblMeta.ToString().Trim(),
                    objMetaRiesgoIndicador.intIdDetalleFrecuencia.ToString().Trim(),
                    objMetaRiesgoIndicador.strValorOtraFrecuencia,
                    objMetaRiesgoIndicador.strAño.ToString().Trim(),
                    objMetaRiesgoIndicador.strMes.ToString().Trim()
                    });
            }
        }
        private void mtdShowUpdateMeta(int RowGrid)
        {
            GridViewRow row = GridViewMetas.Rows[RowGrid];
            var colsNoVisible = GridViewMetas.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;
            string IdDetalleFrecuencia = ((Label)row.FindControl("strDetalleFrecuencia")).Text;
            if (IdDetalleFrecuencia != "0" && IdDetalleFrecuencia != "")
            {
                ddlDetalleFrecuencias.SelectedItem.Text = IdDetalleFrecuencia;
            }
            lblIdMeta.Text = colsNoVisible[0].ToString().Trim();
            
            int IdFrecuencia = Convert.ToInt32(ddlFrecuenciaMedicion.SelectedValue);
            if (IdFrecuencia == 2)
            {
                TextBox12.Visible = true;
                TextBox12.Text = ((Label)row.FindControl("strValorOtraFrecuencia")).Text;
                ddlDetalleFrecuencias.Visible = false;
                txtFrecuenciaAno.Visible = false;
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
            {
                ddlDetalleFrecuencias.Visible = true;
                TextBox12.Visible = false;
                txtFrecuenciaAno.Visible = false;
            }
            
            if (IdFrecuencia == 9)
            {
                txtFrecuenciaAno.Visible = true;
                txtFrecuenciaAno.Text = ((Label)row.FindControl("strValorOtraFrecuencia")).Text;
            }
            TextBox10.Text = row.Cells[4].Text;
            txbAñoMeta.Text = row.Cells[5].Text;
            if (row.Cells[6].Text != "" && row.Cells[6].Text != "0" && row.Cells[6].Text != "&nbsp;")
            {
                //ddlMesMetas.SelectedValue = row.Cells[6].Text;
                int index = ddlMesMetas.Items.IndexOf(ddlMesMetas.Items.FindByText(row.Cells[6].Text));

                ddlMesMetas.SelectedIndex = index;
            }
            else
                ddlMesMetas.SelectedIndex = 0;
            if (ddlFrecuenciaMedicion.SelectedItem.Text == "Semanal" || ddlFrecuenciaMedicion.SelectedItem.Text == "Quincenal")
            {
                
                TdAñoMeta.Visible = true;
                TdValorAñoMeta.Visible = true;
                TdmesMeta.Visible = true;
                TdvalorMesMeta.Visible = true;
            }
            else
            {
                if(IdFrecuencia != 2 && IdFrecuencia != 9)
                {
                    TrfechasIns.Visible = true;
                    TdAñoMeta.Visible = true;
                    TdValorAñoMeta.Visible = true;
                    TdmesMeta.Visible = false;
                    TdvalorMesMeta.Visible = false;
                }
                
            }

            TbMetas.Visible = false;
            TbAddMetas.Visible = true;

            BtnGuardarMeta.Visible = false;
            BtnUpdaterMeta.Visible = true;
        }
        private bool mtdActualizarMetaRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOmetasRiesgoIndicador objMetaRiesgoInd = new clsDTOmetasRiesgoIndicador();
            clsBLLmetaRiesgoIndicador cMetaRiesgoInd = new clsBLLmetaRiesgoIndicador();

            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objMetaRiesgoInd.intIdMeta = Convert.ToInt32(lblIdMeta.Text);
            objMetaRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
            objMetaRiesgoInd.dblMeta = Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(TextBox10.Text));
            int IdFrecuencia = Convert.ToInt32(ddlFrecuenciaMedicion.SelectedValue);
            if (IdFrecuencia == 2)
            {
                objMetaRiesgoInd.intIdDetalleFrecuencia = 0;
                objMetaRiesgoInd.strValorOtraFrecuencia = Sanitizer.GetSafeHtmlFragment(TextBox12.Text);
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
            {
                objMetaRiesgoInd.intIdDetalleFrecuencia = Convert.ToInt32(ddlDetalleFrecuencias.SelectedValue);
                if (IdFrecuencia == 3 || IdFrecuencia == 4)
                {
                    objMetaRiesgoInd.strMes = ddlMesMetas.SelectedValue;
                }
                else
                {
                    objMetaRiesgoInd.strMes = ddlDetalleFrecuencias.SelectedItem.Text;
                }
            }

            if (IdFrecuencia == 9)
            {
                objMetaRiesgoInd.intIdDetalleFrecuencia = 0;
                objMetaRiesgoInd.strValorOtraFrecuencia = Sanitizer.GetSafeHtmlFragment(txtFrecuenciaAno.Text);
            }
            objMetaRiesgoInd.strAño = txbAñoMeta.Text;
            booResult = cMetaRiesgoInd.mtdActualizarMetaRiesgoIndicador(objMetaRiesgoInd, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Meta del Indicador actualizado exitosamente";
                TabContainerIndicadores.ActiveTabIndex = 1;
                mtdResetFieldMetas();
                if (!mtdLoadMetasRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                strErrMsg = "Error al actualizar la Meta del Indicador";
            }
            return booResult;
        }
        private void mtdEliminarMeta(int RowGrid)
        {
            GridViewRow row = GridViewMetas.Rows[RowGrid];
            var colsNoVisible = GridViewMetas.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;
            string IdMeta = colsNoVisible[0].ToString().Trim();
            bool booResult = false;
            clsBLLmetaRiesgoIndicador cMeta = new clsBLLmetaRiesgoIndicador();
            booResult = cMeta.mtdEliminarMetaRiesgoIndicador(IdMeta, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Meta del Indicador eliminada exitosamente";
                omb.ShowMessage(strErrMsg, 3, "Atención");
                TabContainerIndicadores.ActiveTabIndex = 1;
                mtdResetFieldMetas();
                if (!mtdLoadMetasRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                strErrMsg = "Error al eliminar la Meta del Indicador";
                omb.ShowMessage(strErrMsg, 1, "Atención");
            }

        }
        #endregion Metodos Metas

        protected void BtnAdicionaMeta_Click(object sender, ImageClickEventArgs e)
        {
            TbMetas.Visible = false;
            TbAddMetas.Visible = true;
            BtnGuardarMeta.Visible = true;
            BtnUpdaterMeta.Visible = false;
            int IdFrecuencia = Convert.ToInt32(ddlFrecuenciaMedicion.SelectedValue);
            if (IdFrecuencia == 2)
            {
                TextBox12.Visible = true;
                ddlDetalleFrecuencias.Visible = false;
                txtFrecuenciaAno.Visible = false;
            }
            if (IdFrecuencia == 3 || IdFrecuencia == 4 || IdFrecuencia == 5 || IdFrecuencia == 6 || IdFrecuencia == 7 || IdFrecuencia == 8)
            {
                ddlDetalleFrecuencias.Visible = true;
                TextBox12.Visible = false;
                txtFrecuenciaAno.Visible = false;
            }
            if (IdFrecuencia == 9)
            {
                txtFrecuenciaAno.Visible = true;
                ddlDetalleFrecuencias.Visible = false;
                TextBox12.Visible = false;
            }
            if (ddlFrecuenciaMedicion.SelectedItem.Text == "Semanal" || ddlFrecuenciaMedicion.SelectedItem.Text == "Quincenal")
            {
                TrfechasIns.Visible = true;
                TdAñoMeta.Visible = true;
                TdValorAñoMeta.Visible = true;
                TdmesMeta.Visible = true;
                TdvalorMesMeta.Visible = true;
            }else
            {
                if(IdFrecuencia != 9 && IdFrecuencia != 2)
                {
                    TrfechasIns.Visible = true;
                    TdAñoMeta.Visible = true;
                    TdValorAñoMeta.Visible = true;
                    TdmesMeta.Visible = false;
                    TdvalorMesMeta.Visible = false;
                }
                
            }
            
        }

        protected void BtnGuardarMeta_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertarMetaRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }
        }

        protected void GridViewMetas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ModificarMeta":
                    mtdShowUpdateMeta(RowGrid);
                    break;
                case "EliminarMeta":
                    mtdEliminarMeta(RowGrid);
                    break;
            }
        }

        protected void BtnUpdaterMeta_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdActualizarMetaRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }
        }
        #region Metodos Variables Formula
        private bool mtdLoadVariablesFormulaRiesgosIndicadores(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOvariableRiesgoIndicador objVarRiesgoInd = new clsDTOvariableRiesgoIndicador();
            List<clsDTOvariableRiesgoIndicador> lstVarRiesgoInd = new List<clsDTOvariableRiesgoIndicador>();
            clsBLLvariableRiesgoIndicador cVarRiesgoInd = new clsBLLvariableRiesgoIndicador();
            #endregion Vars
            lstVarRiesgoInd = cVarRiesgoInd.mtdConsultarVariableRiesgoIndicador(booResult, ref strErrMsg, Convert.ToInt32(txtId.Text));

            if (lstVarRiesgoInd != null)
            {
                mtdLoadVariablesFormulaRiesgosIndicadores();
                mtdLoadVariablesFormulaRiesgosIndicadores(lstVarRiesgoInd);
                GridViewSeleccVariables.DataSource = lstVarRiesgoInd;
                GridViewSeleccVariables.PageIndex = pagIndexVarForRiesgoIndicador;
                GridViewSeleccVariables.DataBind();
                TbSeleccionarVariables.Visible = true;
                GridViewSeleccVariables.Visible = true;
                booResult = true;
            }
            else
            {
                GridViewSeleccVariables.DataSource = null;
                GridViewSeleccVariables.DataBind();
                GridViewSeleccVariables.Visible = false;
                strErrMsg = "No hay registros de Variables";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadVariablesFormulaRiesgosIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdVariableRiesgoIndicador", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("dblValorVariable", typeof(string));
            grid.Columns.Add("intIdFormato", typeof(string));
            grid.Columns.Add("strFormato", typeof(string));

            GridViewSeleccVariables.DataSource = grid;
            GridViewSeleccVariables.DataBind();
            InfoGridVarForRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstVarRiesgoInd">Lista con las Variables de Indicadores de riesgos</param>
        private void mtdLoadVariablesFormulaRiesgosIndicadores(List<clsDTOvariableRiesgoIndicador> lstVarRiesgoInd)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOvariableRiesgoIndicador objVarRiesgoIndicador in lstVarRiesgoInd)
            {

                InfoGridVarForRiesgoIndicador.Rows.Add(new Object[] {
                    objVarRiesgoIndicador.intIdVariableRiesgoIndicador.ToString().Trim(),
                    objVarRiesgoIndicador.strDescripcion.ToString().Trim(),
                    objVarRiesgoIndicador.dblValorVariable.ToString().Trim(),
                    objVarRiesgoIndicador.intIdFormato.ToString().Trim(),
                    objVarRiesgoIndicador.strFormato.ToString().Trim()
                    });
            }
        }
        #endregion Metodos Variables Formula
        #region Buttons Formula
        clsDTOdetalleFormulaRiesgoIndicador objDetalleFormula = new clsDTOdetalleFormulaRiesgoIndicador();
        protected void BtnNominador_Click(object sender, EventArgs e)
        {
            Label26.Text = "Nominador";
            ImageNominadr.Visible = true;
            BtnDenominador.Visible = true;
            BtnNominador.Visible = false;
            ImageDenominador.Visible = false;
            
        }

        protected void BtnDenominador_Click(object sender, EventArgs e)
        {
            Label26.Text = "Denominador";
            ImageDenominador.Visible = true;
            BtnNominador.Visible = true;
            ImageNominadr.Visible = false;
            BtnDenominador.Visible = false;
        }
        protected void ButtonMas_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " + ";
                TextBox7.Text = TextBox7.Text + " + ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable =  " + ";
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " + ";
                TextBox8.Text = TextBox8.Text + " + ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable =  " + ";
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void ButtonPor_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " * ";
                TextBox7.Text = TextBox7.Text + " * ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable =  " * ";
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " * ";
                TextBox8.Text = TextBox8.Text + " * ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable =  " * ";
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void ButtonAbreP_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " ( ";
                TextBox7.Text = TextBox7.Text + " ( ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable = " ( ";
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " ( ";
                TextBox8.Text = TextBox8.Text + " ( ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable = " ( ";
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void ButtonCierraP_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " ) ";
                TextBox7.Text = TextBox7.Text + " ) ";
                objDetalleFormula.strVariable =  " ) ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " ) ";
                TextBox8.Text = TextBox8.Text + " ) ";
                objDetalleFormula.strVariable =  " ) ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void ButtonDel_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = "";
                TextBox7.Text = "";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                IteracionFormulaNominador = 0;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = "";
                TextBox8.Text = "";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                IteracionFormulaDenominador = 0;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
            }
            if(Label26.Text == "Label")
            {
                omb.ShowMessage("Por favor seleccionar el Nominador o el Denominador para borrar la data",2, "Atención");
            }
        }

        protected void ButtonMenos_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " - ";
                TextBox7.Text = TextBox7.Text + " - ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable =  " - ";
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " - ";
                TextBox8.Text = TextBox8.Text + " - ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable =  " - ";
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void ButtonDivide_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " / ";
                TextBox7.Text = TextBox7.Text + " / ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable = " / ";
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " / ";
                TextBox8.Text = TextBox8.Text + " / ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable =  " / ";
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void ButtonPorc_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " % ";
                TextBox7.Text = TextBox7.Text + " % ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable =  " % ";
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " % ";
                TextBox8.Text = TextBox8.Text + " % ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable =  " % ";
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 2;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void ButtonCero_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " 0 ";
                TextBox7.Text = TextBox7.Text + " 0 ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable =  " 0 ";
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 1;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " 0 ";
                TextBox8.Text = TextBox8.Text + " 0 ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable =  " 0 ";
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 1;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void ButtonCien_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + " 100 ";
                TextBox7.Text = TextBox7.Text + " 100 ";
                int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable =  " 100 ";
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 1;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + " 100 ";
                TextBox8.Text = TextBox8.Text + " 100 ";
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable =  " 100 ";
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 1;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }
        protected void GridViewSeleccVariables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowIndex = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecVariable":
                    SelecVariable(RowIndex);
                    break;
            }
        }
        private void SelecVariable(int RowIndex)
        {
            
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = TextBox5.Text + InfoGridVarForRiesgoIndicador.Rows[RowIndex]["strDescripcion"].ToString().Trim();
                TextBox7.Text = TextBox7.Text + InfoGridVarForRiesgoIndicador.Rows[RowIndex]["strDescripcion"].ToString().Trim();
                int IteracionFormulaNominador = Convert.ToInt32( Session["IteracionFormulaNominador"].ToString());
                objDetalleFormula.strVariable =  InfoGridVarForRiesgoIndicador.Rows[RowIndex]["strDescripcion"].ToString().Trim();
                objDetalleFormula.strTipo = "Nominador";
                objDetalleFormula.intSecuencia = IteracionFormulaNominador;
                objDetalleFormula.intIdOperando = 1;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                strFormulaVariableNominador.Add(objDetalleFormula);
                IteracionFormulaNominador++;
                Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = TextBox6.Text + InfoGridVarForRiesgoIndicador.Rows[RowIndex]["strDescripcion"].ToString().Trim();
                TextBox8.Text = TextBox8.Text + InfoGridVarForRiesgoIndicador.Rows[RowIndex]["strDescripcion"].ToString().Trim();
                int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                objDetalleFormula.strVariable = InfoGridVarForRiesgoIndicador.Rows[RowIndex]["strDescripcion"].ToString().Trim();
                objDetalleFormula.strTipo = "Denominador";
                objDetalleFormula.intSecuencia = IteracionFormulaDenominador;
                objDetalleFormula.intIdOperando = 1;
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                strFormulaVariableDenominador.Add(objDetalleFormula);
                IteracionFormulaDenominador++;
                Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
            }
        }

        protected void GridViewSeleccVariables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion Buttons Formula
        protected void ImbNewSeguimiento_Click(object sender, ImageClickEventArgs e)
        {
            dvContentSeguimiento.Visible = false;
            dvFormSeguimiento.Visible = true;

            IBinsertSeguimiento.Visible = true;
            IBUpdateSeguimiento.Visible = false;
        }

        protected void IBinsertSeguimiento_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertSeguimientoRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }
        }
        #region Metodos Seguimiento
        private bool mtdInsertSeguimientoRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOseguimientoRiesgoIndicador objSegRiesgoInd = new clsDTOseguimientoRiesgoIndicador();
            clsBLLseguimientoRiesgoIndicador cSegRiesgoInd = new clsBLLseguimientoRiesgoIndicador();
            clsDTOriesgosIndicadores objRieInd = new clsDTOriesgosIndicadores();
            clsBLLriesgosIndicadores cRieInd = new clsBLLriesgosIndicadores();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objSegRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
            objSegRiesgoInd.dblValorMinimo = Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtValorMinimo.Text));
            objSegRiesgoInd.dblValorMaximo = Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtValorMaximo.Text));
            objSegRiesgoInd.strDescripcionSeguimiento = Sanitizer.GetSafeHtmlFragment(txtSeguimiento.Text);
            objSegRiesgoInd.strColor = ddlColor.SelectedValue;
            objSegRiesgoInd.dtFechaCreacion = DateTime.Now;
            objSegRiesgoInd.intUsuarioCreacion = Convert.ToInt32(Session["IdUsuario"].ToString());

            booResult = cSegRiesgoInd.mtdInsertarSeguimientoRiesgoIndicador(objSegRiesgoInd, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Seguimiento del Indicador registrada exitosamente";
                objRieInd.intIdEsquemaSeguimiento = cSegRiesgoInd.mtdGetLastId(ref strErrMsg);
                objRieInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
                cRieInd.mtdActualizaSeguimientoRiesgoIndicador(objRieInd, ref strErrMsg);
                mtdResetFieldSeguimiento();
                if (!mtdLoadSeguimientoRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                strErrMsg = "Error al registrar el Seguimiento del Indicador";
            }
            return booResult;
        }
        private bool mtdLoadSeguimientoRiesgosIndicadores(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOseguimientoRiesgoIndicador objSegRiesgoInd = new clsDTOseguimientoRiesgoIndicador();
            List<clsDTOseguimientoRiesgoIndicador> lstSegRiesgoInd = new List<clsDTOseguimientoRiesgoIndicador>();
            clsBLLseguimientoRiesgoIndicador cSegRiesgoInd = new clsBLLseguimientoRiesgoIndicador();
            #endregion Vars
            lstSegRiesgoInd = cSegRiesgoInd.mtdConsultarSeguimientoIndicador(booResult, ref strErrMsg, Convert.ToInt32(txtId.Text));

            if (lstSegRiesgoInd != null)
            {
                mtdLoadSeguimientoRiesgosIndicadores();
                mtdLoadSeguimientoRiesgosIndicadores(lstSegRiesgoInd);
                GVseguimiento.DataSource = lstSegRiesgoInd;
                GVseguimiento.PageIndex = pagIndexSegRiesgoIndicador;
                GVseguimiento.DataBind();
                trGridSeguimiento.Visible = true;
                GVseguimiento.Visible = true;
                booResult = true;
            }
            else
            {
                GVseguimiento.DataSource = null;
                GVseguimiento.DataBind();
                GVseguimiento.Visible = false;
                strErrMsg = "No hay registro de Seguimientos";
            }

            return booResult;
        }
        private void mtdResetFieldSeguimiento()
        {
            dvContentSeguimiento.Visible = true;
            trGridSeguimiento.Visible = false;
            dvFormSeguimiento.Visible = false;

            IBinsertSeguimiento.Visible = false;
            IBUpdateSeguimiento.Visible = false;

            txtValorMinimo.Text = string.Empty;
            txtValorMaximo.Text = string.Empty;
            txtSeguimiento.Text = string.Empty;
            txtCodSeg.Text = string.Empty;

            ddlColor.SelectedIndex = 0;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadSeguimientoRiesgosIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdEsquemaSeguimiento", typeof(string));
            grid.Columns.Add("intIdRiesgoIndicador", typeof(string));
            grid.Columns.Add("dblValorMinimo", typeof(string));
            grid.Columns.Add("dblValorMaximo", typeof(string));
            grid.Columns.Add("strDescripcionSeguimiento", typeof(string));
            grid.Columns.Add("strColor", typeof(string));

            GVseguimiento.DataSource = grid;
            GVseguimiento.DataBind();
            InfoGridSegRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstVarRiesgoInd">Lista con las Variables de Indicadores de riesgos</param>
        private void mtdLoadSeguimientoRiesgosIndicadores(List<clsDTOseguimientoRiesgoIndicador> lstSegRiesgoInd)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOseguimientoRiesgoIndicador objSegRiesgoIndicador in lstSegRiesgoInd)
            {

                InfoGridSegRiesgoIndicador.Rows.Add(new Object[] {
                    objSegRiesgoIndicador.intIdEsquemaSeguimiento.ToString().Trim(),
                    objSegRiesgoIndicador.intIdRiesgoIndicador.ToString().Trim(),
                    objSegRiesgoIndicador.dblValorMinimo.ToString().Trim(),
                    objSegRiesgoIndicador.dblValorMaximo.ToString().Trim(),
                    objSegRiesgoIndicador.strDescripcionSeguimiento.ToString().Trim(),
                    objSegRiesgoIndicador.strColor.ToString().Trim()
                    });
            }
        }
        private void mtdShowUpdateSeguimiento(int RowGrid)
        {
            GridViewRow row = GVseguimiento.Rows[RowGrid];
            var colsNoVisible = GVseguimiento.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;
            txtCodSeg.Text = ((Label)row.FindControl("intIdEsquemaSeguimiento")).Text;
            txtValorMinimo.Text = ((Label)row.FindControl("dblValorMinimo")).Text;
            txtValorMaximo.Text = ((Label)row.FindControl("dblValorMaximo")).Text;
            txtSeguimiento.Text = ((Label)row.FindControl("strDescripcionSeguimiento")).Text;
            ddlColor.SelectedValue = colsNoVisible[0].ToString();

            dvContentSeguimiento.Visible = false;
            dvFormSeguimiento.Visible = true;

            IBinsertSeguimiento.Visible = false;
            IBUpdateSeguimiento.Visible = true;
        }
        private bool mtdUpdateSeguimientoRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOseguimientoRiesgoIndicador objSegRiesgoInd = new clsDTOseguimientoRiesgoIndicador();
            clsBLLseguimientoRiesgoIndicador cSegRiesgoInd = new clsBLLseguimientoRiesgoIndicador();

            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objSegRiesgoInd.intIdEsquemaSeguimiento = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodSeg.Text));
            objSegRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
            objSegRiesgoInd.dblValorMinimo = Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtValorMinimo.Text));
            objSegRiesgoInd.dblValorMaximo = Convert.ToDouble(Sanitizer.GetSafeHtmlFragment(txtValorMaximo.Text));
            objSegRiesgoInd.strDescripcionSeguimiento = Sanitizer.GetSafeHtmlFragment(txtSeguimiento.Text);
            objSegRiesgoInd.strColor = ddlColor.SelectedValue;

            booResult = cSegRiesgoInd.mtdActualizaSeguimientoRiesgoIndicador(objSegRiesgoInd, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Seguimiento del Indicador actualizado exitosamente";
                mtdResetFieldSeguimiento();
                if (!mtdLoadSeguimientoRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                strErrMsg = "Error al actualizar el Seguimiento del Indicador";
            }
            return booResult;
        }
        private void mtdEliminarSeguimiento(int RowGrid)
        {
            GridViewRow row = GVseguimiento.Rows[RowGrid];
            //var colsNoVisible = GVseguimiento.DataKeys[RowGrid].Values;
            string strErrMsg = string.Empty;
            clsDTOseguimientoRiesgoIndicador objSegRiesgoInd = new clsDTOseguimientoRiesgoIndicador();
            clsBLLseguimientoRiesgoIndicador cSegRiesgoInd = new clsBLLseguimientoRiesgoIndicador();
            bool booResult = false;
            objSegRiesgoInd.intIdEsquemaSeguimiento = Convert.ToInt32(((Label)row.FindControl("intIdEsquemaSeguimiento")).Text);

            booResult = cSegRiesgoInd.mtdDeleteSeguimientoRiesgoIndicador(objSegRiesgoInd, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Seguimiento del Indicador eliminado exitosamente";
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFieldSeguimiento();
                if (!mtdLoadSeguimientoRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
            }
            else
            {
                strErrMsg = "Error al eliminar el Seguimiento del Indicador";
                omb.ShowMessage(strErrMsg, 1, "Atención");
            }
        }
        #endregion Metodos Seguimiento

        protected void GVseguimiento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt32(e.CommandArgument);
            Session["RowGrid"] = RowGrid;
            switch (e.CommandName)
            {
                case "ModificarSeguimiento":
                    mtdShowUpdateSeguimiento(RowGrid);
                    break;
                case "EliminarSeguimiento":
                    mtdEliminarSeguimientoMSG(RowGrid);
                    break;
            }
        }
        private void mtdEliminarSeguimientoMSG(int Rowgrid)
        {
            string strEstado = string.Empty;
            GridViewRow row = GVseguimiento.Rows[Rowgrid];
            //var colsNoVisible = GVseguimiento.DataKeys[Rowgrid].Values;
            string strErrMsg = string.Empty;
            string IdSeguimiento = InfoGridSegRiesgoIndicador.Rows[Rowgrid][0].ToString().Trim();
            //int booEstado = Convert.ToInt32(InfoGridCVC.Rows[RowGridCVC][6].ToString().Trim());
            //GridViewRow row = GVcategoriasVariablesControl.Rows[Rowgrid];
            Session["IdSeguimiento"] = IdSeguimiento;
            strEstado = "Eliminar";
            /*if (booEstado == 1)
            {
                Session["activo"] = 0;
                strEstado = "inactivar";
            }
            else
            {
                Session["activo"] = 1;
                strEstado = "activar";
            }*/

            lblMsgBox.Text = string.Format("Desea {0} el seguimiento del Indicador?", strEstado);
            mpeMsgBox.Show();
        }
        protected void IBUpdateSeguimiento_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdUpdateSeguimientoRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }
        }

        protected void IBCancelSeguimiento_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFieldSeguimiento();
            string strErrMsg = string.Empty;
            if (!mtdLoadSeguimientoRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void BtnCancelaMeta_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            mtdResetFieldMetas();
            if (!mtdLoadMetasRiesgosIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void IBinsertFormula_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertaFormulaRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }
        }
        protected void IBupdateFormula_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            /*if (mtdActualizarFormulaRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }*/
            if (mtdInsertaFormulaRiesgoIndicador(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
            }
        }
        #region Metodos Formula
        private bool mtdInsertaFormulaRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOformulaRiesgoIndicador objFormulaRiesgoInd = new clsDTOformulaRiesgoIndicador();
            clsBLLformulaRiesgoIndicador cFormulaRiesgoInd = new clsBLLformulaRiesgoIndicador();
            clsDTOriesgosIndicadores objRieInd = new clsDTOriesgosIndicadores();
            clsBLLriesgosIndicadores cRieInd = new clsBLLriesgosIndicadores();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            try
            {
                objFormulaRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
                objFormulaRiesgoInd.strNominador = TextBox7.Text;
                objFormulaRiesgoInd.strDenominador = TextBox8.Text;
                objFormulaRiesgoInd.dtFechaCreacion = DateTime.Now;
                objFormulaRiesgoInd.intUsuarioCreacion = Convert.ToInt32(Session["IdUsuario"].ToString());
                if (ckbResultPor.Checked == true)
                    objFormulaRiesgoInd.intPorcentaje = 1;
                else
                    objFormulaRiesgoInd.intPorcentaje = 0;
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la captura de los datos: " + ex, 2, "Atención");
            }

            try
            {
                booResult = cFormulaRiesgoInd.mtdInsertarFormulaRiesgoIndicador(objFormulaRiesgoInd, ref strErrMsg);
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en al insertar: " + ex, 2, "Atención");
            }
            

            if (booResult == true)
            {
                strErrMsg = "Formula del Indicador registrada exitosamente";
                objRieInd.intIdFormula = cFormulaRiesgoInd.mtdGetLastId(ref strErrMsg);
                objRieInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
                cRieInd.mtdActualizaFormulaRiesgoIndicador(objRieInd, ref strErrMsg);
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                
                foreach (clsDTOdetalleFormulaRiesgoIndicador objDetalle in strFormulaVariableNominador)
                {
                    
                    cFormulaRiesgoInd.mtdInsertarDetalleFormulaRiesgoIndicador(cFormulaRiesgoInd.mtdGetLastId(ref strErrMsg), objDetalle.strVariable, objDetalle.strTipo, objDetalle.intSecuencia,objDetalle.intIdOperando, ref strErrMsg);
                }
                foreach(clsDTOdetalleFormulaRiesgoIndicador objDetalle in strFormulaVariableDenominador)
                {
                    cFormulaRiesgoInd.mtdInsertarDetalleFormulaRiesgoIndicador(cFormulaRiesgoInd.mtdGetLastId(ref strErrMsg), objDetalle.strVariable, objDetalle.strTipo, objDetalle.intSecuencia, objDetalle.intIdOperando, ref strErrMsg);
                }
                IBinsertFormula.Visible = false;
                IBupdateFormula.Visible = true;
                /*mtdResetFieldSeguimiento();
                if (!mtdLoadSeguimientoRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 1, "Atención");*/
            }
            else
            {
                strErrMsg = "Error al registrar la Formula del Indicador";
            }
            return booResult;
        }
        private bool mtdLoadFormulaRiesgosIndicadores(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOformulaRiesgoIndicador objFormulaRiesgoInd = new clsDTOformulaRiesgoIndicador();
            clsBLLformulaRiesgoIndicador cFormulaRiesgoInd = new clsBLLformulaRiesgoIndicador();
            #endregion Vars
            objFormulaRiesgoInd = cFormulaRiesgoInd.mtdConsultarFormulaRiesgosIndicadores(booResult, ref strErrMsg, Convert.ToInt32(txtId.Text));

            if (objFormulaRiesgoInd != null)
            {
                Label13.Text = "Código: " + objFormulaRiesgoInd.intIdFormula.ToString();
                lblIdFormula.Text = objFormulaRiesgoInd.intIdFormula.ToString();
                TextBox5.Text = objFormulaRiesgoInd.strNominador;
                TextBox6.Text = objFormulaRiesgoInd.strDenominador;
                TextBox7.Text = objFormulaRiesgoInd.strNominador;
                TextBox8.Text = objFormulaRiesgoInd.strDenominador;

                IBinsertFormula.Visible = false;
                IBupdateFormula.Visible = true;
                booResult = true;

                if (objFormulaRiesgoInd.intPorcentaje == 1)
                    ckbResultPor.Checked = true;

                Boolean flag = false;
                clsBLLformulaRiesgoIndicador dbFormula = new clsBLLformulaRiesgoIndicador();
                List<clsDTOdetalleFormulaRiesgoIndicador> lstDetalleForula = dbFormula.mtdConsultarDetalleFormula(flag, ref strErrMsg, objFormulaRiesgoInd.intIdFormula);
                if(lstDetalleForula.Count > 0)
                {
                    
                    List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                    List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                    foreach (clsDTOdetalleFormulaRiesgoIndicador objNewDetalle in lstDetalleForula)
                    {
                        if(objNewDetalle.strTipo == "Nominador")
                        {
                            int IteracionFormulaNominador = Convert.ToInt32(Session["IteracionFormulaNominador"].ToString());
                            strFormulaVariableNominador.Add(objNewDetalle);
                            IteracionFormulaNominador++;
                            Session["IteracionFormulaNominador"] = IteracionFormulaNominador;
                            Session["strFormulaVariableNominador"] = strFormulaVariableNominador;
                        }else
                        {
                            int IteracionFormulaDenominador = Convert.ToInt32(Session["IteracionFormulaDenominador"].ToString());
                            strFormulaVariableDenominador.Add(objNewDetalle);
                            IteracionFormulaDenominador++;
                            Session["IteracionFormulaDenominador"] = IteracionFormulaDenominador;
                            Session["strFormulaVariableDenominador"] = strFormulaVariableDenominador;
                        }
                    }
                    
                }
            }
            else
            {
                Label13.Text = string.Empty;
                lblIdFormula.Text = string.Empty;
                TextBox5.Text = string.Empty;
                TextBox6.Text = string.Empty;
                TextBox7.Text = string.Empty;
                TextBox8.Text = string.Empty;
                strErrMsg = "No hay registro de Formula para el indicador";
            }

            return booResult;
        }
        private bool mtdActualizarFormulaRiesgoIndicador(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOformulaRiesgoIndicador objFormulaRiesgoInd = new clsDTOformulaRiesgoIndicador();
            clsBLLformulaRiesgoIndicador cFormulaRiesgoInd = new clsBLLformulaRiesgoIndicador();

            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            try
            {
                objFormulaRiesgoInd.intIdFormula = Convert.ToInt32(lblIdFormula.Text);
                objFormulaRiesgoInd.intIdRiesgoIndicador = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtId.Text));
                objFormulaRiesgoInd.strNominador = TextBox7.Text;
                objFormulaRiesgoInd.strDenominador = TextBox8.Text;
                objFormulaRiesgoInd.dtFechaCreacion = DateTime.Now;
                objFormulaRiesgoInd.intUsuarioCreacion = Convert.ToInt32(Session["IdUsuario"].ToString());
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la captura de los datos para actualizar: " + ex, 2, "Atención");
            }

            try
            {
                booResult = cFormulaRiesgoInd.mtdActualizarFormulaRiesgoIndicador(objFormulaRiesgoInd, ref strErrMsg);
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en al actualizar: " + ex, 2, "Atención");
            }
            

            if (booResult == true)
            {
                strErrMsg = "Formula del Indicador actualizada exitosamente";
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableNominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableNominador"];
                List<clsDTOdetalleFormulaRiesgoIndicador> strFormulaVariableDenominador = (List<clsDTOdetalleFormulaRiesgoIndicador>)Session["strFormulaVariableDenominador"];
                foreach (clsDTOdetalleFormulaRiesgoIndicador objDetalle in strFormulaVariableNominador)
                {

                    cFormulaRiesgoInd.mtdActualizarDetalleFormulaRiesgoIndicador(cFormulaRiesgoInd.mtdGetLastId(ref strErrMsg), objDetalle.strVariable, objDetalle.strTipo, objDetalle.intSecuencia, objDetalle.intIdOperando, ref strErrMsg);
                }
                foreach (clsDTOdetalleFormulaRiesgoIndicador objDetalle in strFormulaVariableDenominador)
                {
                    cFormulaRiesgoInd.mtdActualizarDetalleFormulaRiesgoIndicador(cFormulaRiesgoInd.mtdGetLastId(ref strErrMsg), objDetalle.strVariable, objDetalle.strTipo, objDetalle.intSecuencia, objDetalle.intIdOperando, ref strErrMsg);
                }
                /*mtdResetFieldSeguimiento();
                if (!mtdLoadSeguimientoRiesgosIndicadores(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 1, "Atención");*/
            }
            else
            {
                strErrMsg = "Error al actualizar la Formula del Indicador";
            }
            return booResult;
        }

        #endregion Metodos Formula

        #region Asociar Riesgos
        protected void IBconsultarRiesgo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Sanitizer.GetSafeHtmlFragment(TextBox31.Text.Trim()) == "" && Sanitizer.GetSafeHtmlFragment(TextBox32.Text.Trim()) == "")
                    omb.ShowMessage("Debe ingresar por lo menos un parámetro de consulta.",1,"Atención");
                else
                {
                    loadGridConsultarRiesgos();
                    loadInfoConsultarRiesgos();
                    //mtdLoadGridAudEventoRiesgo();
                    //mtdLoadInfoAudEventoRiesgo();
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al realizar la consulta. " + ex.Message,1,"Atención");
            }
        }
        private void loadGridConsultarRiesgos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("ListaCausas", typeof(string));
            grid.Columns.Add("IdProbabilidad", typeof(string));
            grid.Columns.Add("IdImpacto", typeof(string));
            InfoGridConsultarRiesgos = grid;
            GVriesgos.DataSource = InfoGridConsultarRiesgos;
            GVriesgos.DataBind();
        }
        private void loadInfoConsultarRiesgos()
        {
            cRiesgo cRiesgo = new cRiesgo();
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoRiesgos(Sanitizer.GetSafeHtmlFragment(TextBox31.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox32.Text.Trim()), "---", "---", "---", "---", "---");

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridConsultarRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["ListaCausas"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["IdProbabilidad"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["IdImpacto"].ToString().Trim()
                                                                   });
                }

                GVriesgos.DataSource = InfoGridConsultarRiesgos;
                GVriesgos.DataBind();
                GVriesgos.Visible = true;
                trTituloRiesgoA.Visible = true;
                trRiesgoA.Visible = true;
            }
            else
            {
                GVriesgos.Visible = false;
                loadGridConsultarRiesgos();
                omb.ShowMessage("No se ha encontrado el riesgo que se está consultando.",2,"Atención");
            }
        }

        private bool mtdLoadRiesgosAsociado(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOriesgosIndicadores objRiesgoInd = new clsDTOriesgosIndicadores();
            List<clsDTOriesgosIndicadores> lstRiesgoInd = new List<clsDTOriesgosIndicadores>();
            clsBLLriesgosIndicadores cRiesgoInd = new clsBLLriesgosIndicadores();
            #endregion Vars
            lstRiesgoInd = cRiesgoInd.mtdConsultarRiesgosAsociado(booResult, ref strErrMsg, Convert.ToInt32(txtId.Text));

            if (lstRiesgoInd != null)
            {
                mtdLoadRiesgosAsociado();
                mtdLoadRiesgosAsociado(lstRiesgoInd);
                GVriesgoAsociado.DataSource = lstRiesgoInd;
                GVriesgoAsociado.PageIndex = pagIndexRiesgoIndicadorAsoc;
                GVriesgoAsociado.DataBind();
                trRiesgoAsociadosGrid.Visible = true;
                trRiesgoAsociadosText.Visible = true;

                trTitutloConsultarRiesgo.Visible = false;
                trCodRiesgoSearch.Visible = false;
                trNombreRiesgoSearch.Visible = false;
                trButtonsSearch.Visible = false;
                trTituloRiesgoA.Visible = false;
                trRiesgoA.Visible = false;

                booResult = true;
            }else
            {
                GVriesgoAsociado.DataSource = null;
                GVriesgoAsociado.DataBind();
                trRiesgoAsociadosGrid.Visible = false;
            }
            

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadRiesgosAsociado()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdRiesgoIndicador", typeof(string));
            grid.Columns.Add("intIdRiesgoAsociado", typeof(string));
            grid.Columns.Add("strCodRiesgo", typeof(string));
            grid.Columns.Add("strNombreRiesgo", typeof(string));

            GVriesgoAsociado.DataSource = grid;
            GVriesgoAsociado.DataBind();
            InfoGridRiesgoIndicadorAsoc = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstVarRiesgoInd">Lista con las Variables de Indicadores de riesgos</param>
        private void mtdLoadRiesgosAsociado(List<clsDTOriesgosIndicadores> lstVarRiesgoInd)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOriesgosIndicadores objVarRiesgoIndicador in lstVarRiesgoInd)
            {

                InfoGridRiesgoIndicadorAsoc.Rows.Add(new Object[] {
                    objVarRiesgoIndicador.intIdRiesgoIndicador.ToString().Trim(),
                    objVarRiesgoIndicador.intIdRiesgoAsociado.ToString().Trim(),
                    objVarRiesgoIndicador.strCodRiesgo.ToString().Trim(),
                    objVarRiesgoIndicador.strNombreRiesgo.ToString().Trim()
                    });
            }
        }
        #endregion Asociar Riesgo

        protected void GVriesgos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridConsultarRiesgos = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVriesgos.Rows[RowGridConsultarRiesgos];
            var colsNoVisible = GVriesgos.DataKeys[RowGridConsultarRiesgos].Values;
            clsDTOriesgosIndicadores objIndicador = new clsDTOriesgosIndicadores();
            clsBLLriesgosIndicadores cRiesgoIndicador = new clsBLLriesgosIndicadores();
            objIndicador.intIdRiesgoIndicador = Convert.ToInt32(txtId.Text);
            objIndicador.intIdRiesgoAsociado = Convert.ToInt32(colsNoVisible[1].ToString());
            bool booResult = false;
            string strErrMsg = string.Empty;
            /*Session["IdProbabilidad"] = colsNoVisible[2].ToString();
            Session["IdImpacto"] = colsNoVisible[3].ToString();
            Session["NombreRiesgo"] = row.Cells[1].Text;*/
            switch (e.CommandName)
            {
                case "Relacionar":
                    booResult = cRiesgoIndicador.mtdAsociarRiesgoIndicador(objIndicador, ref strErrMsg);
                    if (booResult == true)
                    {
                        strErrMsg = "Riesgo: "+ row.Cells[1].Text + " asociado  exitosamente";
                        omb.ShowMessage(strErrMsg,3,"Atención");
                        mtdLoadRiesgosAsociado(ref strErrMsg);
                    }
                    else
                    {
                        strErrMsg = "Error al asociar el riesgo al Indicador";
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    }
                    
                    break;
            }
        }

        protected void GVriesgoAsociado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridRiesgoIndicadorAsoc = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVriesgoAsociado.Rows[RowGridRiesgoIndicadorAsoc];
            var colsNoVisible = GVriesgoAsociado.DataKeys[RowGridRiesgoIndicadorAsoc].Values;
            clsDTOriesgosIndicadores objIndicador = new clsDTOriesgosIndicadores();
            clsBLLriesgosIndicadores cRiesgoIndicador = new clsBLLriesgosIndicadores();
            objIndicador.intIdRiesgoIndicador = Convert.ToInt32(txtId.Text);
            objIndicador.intIdRiesgoAsociado = Convert.ToInt32(colsNoVisible[1].ToString());
            bool booResult = false;
            string strErrMsg = string.Empty;
            /*Session["IdProbabilidad"] = colsNoVisible[2].ToString();
            Session["IdImpacto"] = colsNoVisible[3].ToString();
            Session["NombreRiesgo"] = row.Cells[1].Text;*/
            switch (e.CommandName)
            {
                case "Desasociar":
                    booResult = cRiesgoIndicador.mtdDesasociarRiesgoIndicador(objIndicador, ref strErrMsg);
                    if (booResult == true)
                    {
                        strErrMsg = "Riesgo: " + row.Cells[1].Text + " desasociado  exitosamente";
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                        trTitutloConsultarRiesgo.Visible = true;
                        trCodRiesgoSearch.Visible = true;
                        trNombreRiesgoSearch.Visible = true;
                        trButtonsSearch.Visible = true;

                        trRiesgoAsociadosText.Visible = false;
                        trRiesgoAsociadosGrid.Visible = false;
                    }
                    else
                    {
                        strErrMsg = "Error al desasociar el riesgo al Indicador";
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    }

                    break;
            }
        }

        protected void GVriesgosIndicadores_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < GVriesgosIndicadores.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVriesgosIndicadores.Rows[rowIndex];
                int booActivo = 0;
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 10)
                    {
                        booActivo = Convert.ToInt32(((Label)row.FindControl("booActivo")).Text);
                        ImageButton ImgBnt = ((ImageButton)row.FindControl("ImgBtnInact"));
                        if (booActivo == 0)
                            ImgBnt.ImageUrl = "~/Imagenes/Icons/switch-off-icon.png";
                    }
                }
            }
            //MergeRowsMatrizData(GVriesgosIndicadores);
        }
        public void MergeRowsMatrizData(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    //string text = ((Label)row.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;

                    //string previousText = ((Label)previousRow.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;
                    if (cellIndex == 0)
                    {
                        string text = row.Cells[cellIndex].Text;
                        string previousText = previousRow.Cells[cellIndex].Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("strNombreIndicador")).Text;
                        string previousText = ((Label)previousRow.FindControl("strNombreIndicador")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 2)
                    {
                        string text = ((Label)row.FindControl("strObjetivoIndicador")).Text;
                        string previousText = ((Label)previousRow.FindControl("strObjetivoIndicador")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 3)
                    {
                        string text = ((Label)row.FindControl("strNombreProceso")).Text;
                        string previousText = ((Label)previousRow.FindControl("strNombreProceso")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 4)
                    {
                        string text = ((Label)row.FindControl("strResponsableMedicion")).Text;
                        string previousText = ((Label)previousRow.FindControl("strResponsableMedicion")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 5)
                    {
                        string text = ((Label)row.FindControl("strFrecuenciaMedicion")).Text;
                        string previousText = ((Label)previousRow.FindControl("strFrecuenciaMedicion")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 6)
                    {
                        string text = ((Label)row.FindControl("strCodRiesgo")).Text;
                        string previousText = ((Label)previousRow.FindControl("strCodRiesgo")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 7)
                    {
                        string text = ((Label)row.FindControl("strNombreRiesgo")).Text;
                        string previousText = ((Label)previousRow.FindControl("strNombreRiesgo")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 11)
                    {
                        string text = row.Cells[cellIndex].Text;
                        string previousText = previousRow.Cells[cellIndex].Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 12)
                    {
                        string text = row.Cells[cellIndex].Text;
                        string previousText = previousRow.Cells[cellIndex].Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                }
            }
        }
        protected void GVriesgosIndicadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndexRiesgoIndicador = e.NewPageIndex;
            /*GVevaluacionDesempeño.PageIndex = PagIndex1;
            GVevaluacionDesempeño.DataBind();*/
            string strErrMsg = "";
            mtdLoadRiesgosIndicadores(ref strErrMsg);
        }
      

           protected void GridViewMetas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndexMetaRiesgoIndicador = e.NewPageIndex;
            /*GVevaluacionDesempeño.PageIndex = PagIndex1;
            GVevaluacionDesempeño.DataBind();*/
            string strErrMsg = "";
            mtdLoadMetasRiesgosIndicadores(ref strErrMsg);
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
            pdfDocument.AddTitle("Hoja de vida del Indicador de Riesgo");
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
            font1.Color = new Color(GVriesgosIndicadores.HeaderStyle.ForeColor);
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Código:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtId.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Nombre Indicador:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtNombreIndicador.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Objetivo del Indicador:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtObjetivoIndicador.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Proceso del Indicador:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
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
            pdfCellEncabezado = new PdfPCell(new Phrase("Cargo Responsable:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxResponsable.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Frecuencia de Medición:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlFrecuenciaMedicion.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            #endregion
            #region Tabla RiesgoAsociado
            PdfPTable pdfpTableRiesgoAsociado = new PdfPTable(2);
            int Iteracion = 0;
            foreach (TableCell headerCell in GVriesgoAsociado.HeaderRow.Cells)
            {
                if (Iteracion != 2)
                {
                    Font font = new Font();
                    font.Color = new Color(GVriesgoAsociado.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                    pdfCell.BackgroundColor = new Color(GVriesgoAsociado.HeaderStyle.BackColor);
                    pdfpTableRiesgoAsociado.AddCell(pdfCell);
                }
                Iteracion++;
            }
            
            foreach (GridViewRow GridViewRow in GVriesgoAsociado.Rows)
            {
                Iteracion = 0;
                string strCodRiesgo = ((Label)GridViewRow.FindControl("strCodRiesgo")).Text;
                string strNombreRiesgo = ((Label)GridViewRow.FindControl("strNombreRiesgo")).Text;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (Iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVriesgoAsociado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strCodRiesgo));
                        pdfCell.BackgroundColor = new Color(GVriesgoAsociado.RowStyle.BackColor);
                        pdfpTableRiesgoAsociado.AddCell(pdfCell);
                    }
                    if(Iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVriesgoAsociado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strNombreRiesgo));
                        pdfCell.BackgroundColor = new Color(GVriesgoAsociado.RowStyle.BackColor);
                        pdfpTableRiesgoAsociado.AddCell(pdfCell);
                    }
                    Iteracion++;
                }
            }
            #endregion Tabla RiesgoAsociado

            #region Tabla Variables
            PdfPTable pdfpTableVariables = new PdfPTable(2);
            Iteracion = 0;
            foreach (TableCell headerCell in GridViewVariables.HeaderRow.Cells)
            {
                if (Iteracion < 3 && Iteracion > 0)
                {
                    Font font = new Font();
                    font.Color = new Color(GridViewVariables.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                    pdfCell.BackgroundColor = new Color(GridViewVariables.HeaderStyle.BackColor);
                    pdfpTableVariables.AddCell(pdfCell);
                }
                Iteracion++;
            }
            
            foreach (GridViewRow GridViewRow in GridViewVariables.Rows)
            {
                Iteracion = 0;
                string strDescripcion = ((Label)GridViewRow.FindControl("strDescripcion")).Text;
                string strFormato = ((Label)GridViewRow.FindControl("strFormato")).Text;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (Iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GridViewVariables.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strDescripcion));
                        pdfCell.BackgroundColor = new Color(GridViewVariables.RowStyle.BackColor);
                        pdfpTableVariables.AddCell(pdfCell);
                    }
                    if (Iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GridViewVariables.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strFormato));
                        pdfCell.BackgroundColor = new Color(GridViewVariables.RowStyle.BackColor);
                        pdfpTableVariables.AddCell(pdfCell);
                    }
                    /*if (Iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GridViewVariables.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                        pdfCell.BackgroundColor = new Color(GridViewVariables.RowStyle.BackColor);
                        pdfpTableVariables.AddCell(pdfCell);
                    }*/
                    Iteracion++;
                }
            }
            #endregion Tabla Variables
            #region Tabla Metas
            PdfPTable pdfpTableMetas = new PdfPTable(3);
            Iteracion = 0;
            foreach (TableCell headerCell in GridViewMetas.HeaderRow.Cells)
            {
                if (Iteracion < 5 && Iteracion > 1)
                {
                    Font font = new Font();
                    font.Color = new Color(GridViewMetas.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                    pdfCell.BackgroundColor = new Color(GridViewMetas.HeaderStyle.BackColor);
                    pdfpTableMetas.AddCell(pdfCell);
                }
                Iteracion++;
            }
            
            foreach (GridViewRow GridViewRow in GridViewMetas.Rows)
            {
                Iteracion = 0;
                string strDetalleFrecuencia = ((Label)GridViewRow.FindControl("strDetalleFrecuencia")).Text;
                string strValorOtraFrecuencia = ((Label)GridViewRow.FindControl("strValorOtraFrecuencia")).Text;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (Iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GridViewMetas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strDetalleFrecuencia));
                        pdfCell.BackgroundColor = new Color(GridViewMetas.RowStyle.BackColor);
                        pdfpTableMetas.AddCell(pdfCell);
                    }
                    if (Iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GridViewMetas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strValorOtraFrecuencia));
                        pdfCell.BackgroundColor = new Color(GridViewMetas.RowStyle.BackColor);
                        pdfpTableMetas.AddCell(pdfCell);
                    }
                    if (Iteracion == 4)
                    {
                        Font font = new Font();
                        font.Color = new Color(GridViewMetas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                        pdfCell.BackgroundColor = new Color(GridViewMetas.RowStyle.BackColor);
                        pdfpTableMetas.AddCell(pdfCell);
                    }
                    Iteracion++;
                }
            }
            #endregion Tabla Metas
            #region Tabla Formula
            font1.Color = new Color(GVriesgosIndicadores.HeaderStyle.ForeColor);
            PdfPTable pdfTableFormula = new PdfPTable(2);
            PdfPCell pdfCellFormula = new PdfPCell(new Phrase("Nominador:", font1));
            pdfCellFormula.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableFormula.AddCell(pdfCellFormula);
            pdfCellFormula = new PdfPCell(new Phrase(TextBox7.Text));
            pdfTableFormula.AddCell(pdfCellFormula);
            pdfCellFormula = new PdfPCell(new Phrase("Denominador:", font1));
            pdfCellFormula.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableFormula.AddCell(pdfCellFormula);
            pdfCellFormula = new PdfPCell(new Phrase(TextBox8.Text));
            pdfTableFormula.AddCell(pdfCellFormula);
            #endregion Tabla Formula
            #region Tabla Seguimiento
            PdfPTable pdfpTableSeguimiento = new PdfPTable(4);
            Iteracion = 0;
            foreach (TableCell headerCell in GVseguimiento.HeaderRow.Cells)
            {
                if (Iteracion < 4)
                {
                    Font font = new Font();
                    font.Color = new Color(GVseguimiento.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                    pdfCell.BackgroundColor = new Color(GVseguimiento.HeaderStyle.BackColor);
                    pdfpTableSeguimiento.AddCell(pdfCell);
                }
                Iteracion++;
            }
            
            foreach (GridViewRow GridViewRow in GVseguimiento.Rows)
            {
                Iteracion = 0;
                string intIdEsquemaSeguimiento = ((Label)GridViewRow.FindControl("intIdEsquemaSeguimiento")).Text;
                string strDescripcionSeguimiento = ((Label)GridViewRow.FindControl("strDescripcionSeguimiento")).Text;
                string dblValorMinimo = ((Label)GridViewRow.FindControl("dblValorMinimo")).Text;
                string dblValorMaximo = ((Label)GridViewRow.FindControl("dblValorMaximo")).Text;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (Iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVseguimiento.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intIdEsquemaSeguimiento));
                        pdfCell.BackgroundColor = new Color(GVseguimiento.RowStyle.BackColor);
                        pdfpTableSeguimiento.AddCell(pdfCell);
                    }
                    if (Iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVseguimiento.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strDescripcionSeguimiento));
                        pdfCell.BackgroundColor = new Color(GVseguimiento.RowStyle.BackColor);
                        pdfpTableSeguimiento.AddCell(pdfCell);
                    }
                    if (Iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVseguimiento.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dblValorMinimo));
                        pdfCell.BackgroundColor = new Color(GVseguimiento.RowStyle.BackColor);
                        pdfpTableSeguimiento.AddCell(pdfCell);
                    }
                    if (Iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVseguimiento.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dblValorMaximo));
                        pdfCell.BackgroundColor = new Color(GVseguimiento.RowStyle.BackColor);
                        pdfpTableSeguimiento.AddCell(pdfCell);
                    }
                    Iteracion++;
                }
            }
            #endregion Tabla Seguimiento
            #region Tabla Footer
            font1.Color = new Color(GVriesgosIndicadores.HeaderStyle.ForeColor);
            PdfPTable pdfTableFooter = new PdfPTable(2);
            PdfPCell pdfCellFooter = new PdfPCell(new Phrase("Usuario Creación:", font1));
            pdfCellFooter.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableFooter.AddCell(pdfCellFooter);
            pdfCellFooter = new PdfPCell(new Phrase(tbxUsuarioCreacion.Text));
            pdfTableFooter.AddCell(pdfCellFooter);
            pdfCellFooter = new PdfPCell(new Phrase("Fecha Creación:", font1));
            pdfCellFooter.BackgroundColor = new Color(GVriesgosIndicadores.HeaderStyle.BackColor);
            pdfTableFooter.AddCell(pdfCellFooter);
            pdfCellFooter = new PdfPCell(new Phrase(txtFecha.Text));
            pdfTableFooter.AddCell(pdfCellFooter);
            #endregion Tabla Footer

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
            Paragraph Titulo = new Paragraph(new Phrase("Hoja de vida del Indicador de Riesgo"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);

            Paragraph TituloRiesgoAsociado = new Paragraph(new Phrase("Riesgo Asociado"));
            TituloRiesgoAsociado.SetAlignment("Center");
            pdfDocument.Add(TituloRiesgoAsociado);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableRiesgoAsociado);
            Paragraph TituloVariables = new Paragraph(new Phrase("Variables del Indicador:"));
            TituloVariables.SetAlignment("Center");
            pdfDocument.Add(TituloVariables);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableVariables);
            Paragraph TituloMetas = new Paragraph(new Phrase("Metas del Indicador:"));
            TituloMetas.SetAlignment("Center");
            pdfDocument.Add(TituloMetas);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableMetas);
            Paragraph TituloFormula = new Paragraph(new Phrase("Formula del Indicador:"));
            TituloFormula.SetAlignment("Center");
            pdfDocument.Add(TituloFormula);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableFormula);
            Paragraph TituloSeguimiento = new Paragraph(new Phrase("Seguimiento del Indicador:"));
            TituloSeguimiento.SetAlignment("Center");
            pdfDocument.Add(TituloSeguimiento);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableSeguimiento);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableFooter);
            pdfDocument.Add(Chunk.NEWLINE);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=IndicadorRiesgo_"+System.DateTime.Now.ToString("yyyy-MM-dd")+".pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "IndicardorRiesgo_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Código:");
            gridEncabezado.Columns.Add("Nombre Indicador:");
            gridEncabezado.Columns.Add("Objetivo del Indicador:");
            gridEncabezado.Columns.Add("Proceso:");
            gridEncabezado.Columns.Add("Cargo Responsable:");
            gridEncabezado.Columns.Add("Frecuencia de Medición:");
            gridEncabezado.Columns.Add("Usuario Creación:");
            gridEncabezado.Columns.Add("Fecha Creación:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Código:"] = txtId.Text;
            rowEncabezado["Nombre Indicador:"] = txtNombreIndicador.Text;
            rowEncabezado["Objetivo del Indicador:"] = txtObjetivoIndicador.Text;
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
            rowEncabezado["Cargo Responsable:"] = tbxResponsable.Text;
            rowEncabezado["Frecuencia de Medición:"] = ddlFrecuenciaMedicion.SelectedItem.Text;
            rowEncabezado["Usuario Creación:"] = tbxUsuarioCreacion.Text;
            rowEncabezado["Fecha Creación:"] = txtFecha.Text;
            gridEncabezado.Rows.Add(rowEncabezado);

            DataTable gridFormula = new DataTable();
            gridFormula.Columns.Add("Nominador:");
            gridFormula.Columns.Add("Denominador:");
            
            DataRow rowFormula;
            rowFormula = gridFormula.NewRow();
            rowFormula["Nominador:"] = TextBox7.Text;
            rowFormula["Denominador:"] = TextBox8.Text;
            gridFormula.Rows.Add(rowFormula);

            DataTable gridRiesgoAsociado = new DataTable();
            gridRiesgoAsociado.Columns.Add("Código Riesgo:");
            gridRiesgoAsociado.Columns.Add("Nombre Riesgo:");
            DataRow rowRiesgoAsociado;
            foreach (GridViewRow GridViewRow in GVriesgoAsociado.Rows)
            {
                rowRiesgoAsociado = gridRiesgoAsociado.NewRow();
                string strCodRiesgo = ((Label)GridViewRow.FindControl("strCodRiesgo")).Text;
                string strNombreRiesgo = ((Label)GridViewRow.FindControl("strNombreRiesgo")).Text;

                rowRiesgoAsociado["Código Riesgo:"] = strCodRiesgo;
                rowRiesgoAsociado["Nombre Riesgo:"] = strNombreRiesgo;
                
                gridRiesgoAsociado.Rows.Add(rowRiesgoAsociado);
            }
            DataTable gridVariables = new DataTable();
            gridVariables.Columns.Add("Descripción:");
            gridVariables.Columns.Add("Formato:");
            
            DataRow rowVariables;
            
            foreach (GridViewRow GridViewRow in GridViewVariables.Rows)
            {
                int Iteracion = 0;
                rowVariables = gridVariables.NewRow();
                string strDescripcion = ((Label)GridViewRow.FindControl("strDescripcion")).Text;
                string strFormato = ((Label)GridViewRow.FindControl("strFormato")).Text;
                rowVariables["Descripción:"] = strDescripcion;
                rowVariables["Formato:"] = strFormato;
                

                gridVariables.Rows.Add(rowVariables);
            }

            DataTable gridMetas = new DataTable();
            gridMetas.Columns.Add("Tipo Frecuencia:");
            gridMetas.Columns.Add("Valor Frecuencia:");
            gridMetas.Columns.Add("Meta:");
            DataRow rowMetas;

            foreach (GridViewRow GridViewRow in GridViewMetas.Rows)
            {
                int Iteracion = 0;
                rowMetas = gridMetas.NewRow();
                string strDetalleFrecuencia = ((Label)GridViewRow.FindControl("strDetalleFrecuencia")).Text;
                string strValorOtraFrecuencia = ((Label)GridViewRow.FindControl("strValorOtraFrecuencia")).Text;
                rowMetas["Tipo Frecuencia:"] = strDetalleFrecuencia;
                rowMetas["Valor Frecuencia:"] = strValorOtraFrecuencia;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (Iteracion == 4)
                        rowMetas["Meta:"] = Convert.ToInt32(tableCell.Text);
                    Iteracion++;
                }

                gridMetas.Rows.Add(rowMetas);
            }

            DataTable gridSeguimiento = new DataTable();
            gridSeguimiento.Columns.Add("Código:");
            gridSeguimiento.Columns.Add("Descripción Seguimiento:");
            gridSeguimiento.Columns.Add("Valor Minimo:");
            gridSeguimiento.Columns.Add("Valor Máximo:");
            DataRow rowSeguimiento;

            foreach (GridViewRow GridViewRow in GVseguimiento.Rows)
            {
                rowSeguimiento = gridSeguimiento.NewRow();
                string intIdEsquemaSeguimiento = ((Label)GridViewRow.FindControl("intIdEsquemaSeguimiento")).Text;
                string strDescripcionSeguimiento = ((Label)GridViewRow.FindControl("strDescripcionSeguimiento")).Text;
                double dblValorMinimo = Convert.ToDouble(((Label)GridViewRow.FindControl("dblValorMinimo")).Text);
                double dblValorMaximo = Convert.ToDouble(((Label)GridViewRow.FindControl("dblValorMaximo")).Text);
                rowSeguimiento["Código:"] = intIdEsquemaSeguimiento;
                rowSeguimiento["Descripción Seguimiento:"] = strDescripcionSeguimiento;
                rowSeguimiento["Valor Minimo:"] = dblValorMinimo;
                rowSeguimiento["Valor Máximo:"] = dblValorMaximo;

                gridSeguimiento.Rows.Add(rowSeguimiento);
            }

            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "Encabezado");
            workbook.Worksheets.Add(gridRiesgoAsociado, "Riesgo Asociado");
            workbook.Worksheets.Add(gridVariables, "Variables");
            workbook.Worksheets.Add(gridMetas, "Metas");
            workbook.Worksheets.Add(gridFormula, "Formula");
            workbook.Worksheets.Add(gridSeguimiento, "Seguimiento");
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

        protected void IBcancelConsultaR_Click(object sender, ImageClickEventArgs e)
        {
            TextBox31.Text = string.Empty;
            TextBox32.Text = string.Empty;
            GVriesgos.Visible = false;
        }

        protected void GridViewMetas_PreRender(object sender, EventArgs e)
        {
            for (int rowindex = 0; rowindex < GridViewMetas.Rows.Count; rowindex++)
            {
                GridViewRow row = GridViewMetas.Rows[rowindex];
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 6)
                    {
                        string text = row.Cells[cellIndex].Text;
                        if (text == "0")
                        {
                            row.Cells[cellIndex].Text = "";
                        }
                        
                    }
                }
            }
        }
    }
}