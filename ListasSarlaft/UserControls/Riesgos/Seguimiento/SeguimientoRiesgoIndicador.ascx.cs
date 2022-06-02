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
using System.Threading;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ListasSarlaft.UserControls.Riesgos.Seguimiento
{
    public partial class SeguimientoRiesgoIndicador : System.Web.UI.UserControl
    {
        string IdFormulario = "5027";
        cCuenta cCuenta = new cCuenta();
        cRiesgo cRiesgo = new cRiesgo();
        private cGestion cGestion = new cGestion();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBconsultar);
            scriptManager.RegisterPostBackControl(this.IBcancel);
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
        private DataTable infoGridRiesgoIndicador;
        private int rowGridRiesgoIndicador;
        private int pagIndexRiesgoIndicador;

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
        #endregion Properties

        private void mtdInicializarValores()
        {
            PagIndexRiesgoIndicador = 0;
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
            if (!IsPostBack)
                PopulateTreeView();
            loadDDLClasificacion();
        }
        private void loadDDLClasificacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlFactorRiesgo.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar clasificación riesgo. " + ex.Message, 1, "Atención");
            }
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
        #region LoadMetodos
        private bool mtdCargarDDLs(ref string strErrMsg)
        {
            bool booResult = false;

            booResult = mtdLoadDDLCadenaValor(ref strErrMsg);

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
        #endregion MetodoLoad
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



            ddlSubproceso.Items.Clear();

            if (mtdLoadDDLSubproceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlSubproceso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion DLLEvents
        protected void IBconsultar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string CodRiesgo = Sanitizer.GetSafeHtmlFragment(txbRiesgo.Text);
            int IdProceso = 0;
            if (ddlSubproceso.SelectedValue.ToString() != "0" && ddlSubproceso.SelectedValue.ToString() != "")
            {
                IdProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
            }
            else
            {
                if (ddlProceso.SelectedValue.ToString() != "0" && ddlProceso.SelectedValue.ToString() != "")
                {
                    IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue.ToString() != "0" && ddlMacroproceso.SelectedValue.ToString() != "")
                    {
                        IdProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                    }
                }
            }
            int Responsable = 0;
            if (lblIdDependencia4.Text != "")
                Responsable = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(lblIdDependencia4.Text));
            Session["CodRiesgo"] = CodRiesgo;
            Session["IdProceso"] = IdProceso;
            Session["Responsable"] = Responsable;
            int IdFactorRiesgo = 0;
            Session["IdFactorRiesgo"] = IdFactorRiesgo;
            if (ddlFactorRiesgo.SelectedValue != "---")
                IdFactorRiesgo = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            else
                IdFactorRiesgo = 0;

            if (!mtdLoadRiesgosIndicadores(ref strErrMsg, CodRiesgo, IdProceso, Responsable, IdFactorRiesgo))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void IBcancel_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetField();
        }
        public void mtdResetField()
        {
            dvGirdData.Visible = false;
            ddlFactorRiesgo.SelectedIndex = 0;
            txbRiesgo.Text = string.Empty;
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            tbxResponsable.Text = string.Empty;
            string strErrMsg = String.Empty;
            /*mtdLoadEvaluacionProveedor(ref strErrMsg);*/
            if (!mtdCargarDDLs(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        #region Metodos
        private bool mtdLoadRiesgosIndicadores(ref string strErrMsg, string CodRiesgo, int IdProceso, int Responsable, int IdFactorRiesgo)
        {
            #region Vars
            bool booResult = false;
            clsDTOriesgosIndicadores objRiesgoInd = new clsDTOriesgosIndicadores();
            List<clsDTOriesgosIndicadores> lstRiesgoInd = new List<clsDTOriesgosIndicadores>();
            clsBLLriesgosIndicadores cRiesgoInd = new clsBLLriesgosIndicadores();
            #endregion Vars
            lstRiesgoInd = cRiesgoInd.mtdConsultarRiesgosIndicadores(booResult, ref strErrMsg, CodRiesgo, IdProceso, Responsable, IdFactorRiesgo);

            if (lstRiesgoInd != null)
            {
                mtdLoadRiesgosIndicadores();
                mtdLoadRiesgosIndicadores(lstRiesgoInd);
                GVseguimientoRiesgoInsicador.DataSource = lstRiesgoInd;
                GVseguimientoRiesgoInsicador.PageIndex = pagIndexRiesgoIndicador;
                GVseguimientoRiesgoInsicador.DataBind();
                dvGirdData.Visible = true;
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
            grid.Columns.Add("strDescripcionFrecuencia", typeof(string));
            grid.Columns.Add("intIdRiesgoAsociado", typeof(string));
            grid.Columns.Add("strCodRiesgo", typeof(string));
            grid.Columns.Add("strNombreRiesgo", typeof(string));
            grid.Columns.Add("intIdFormula", typeof(string));
            grid.Columns.Add("strNominador", typeof(string));
            grid.Columns.Add("strDenominador", typeof(string));
            grid.Columns.Add("intIdMeta", typeof(string));
            grid.Columns.Add("dblMeta", typeof(string));
            grid.Columns.Add("strAño", typeof(string));
            grid.Columns.Add("strMes", typeof(string));
            grid.Columns.Add("dblResultado", typeof(string));
            grid.Columns.Add("strDescripcionSeguimiento", typeof(string));
            grid.Columns.Add("strColor", typeof(string));
            grid.Columns.Add("intIdEsquemaSeguimiento", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaCreacion", typeof(string));
            grid.Columns.Add("booActivo", typeof(string));

            GVseguimientoRiesgoInsicador.DataSource = grid;
            GVseguimientoRiesgoInsicador.DataBind();
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
                    objRiesgoIndicador.strDescripcionFrecuencia.ToString().Trim(),
                    objRiesgoIndicador.intIdRiesgoAsociado.ToString().Trim(),
                    objRiesgoIndicador.strCodRiesgo.ToString().Trim(),
                    objRiesgoIndicador.strNombreRiesgo.ToString().Trim(),
                    objRiesgoIndicador.intIdFormula.ToString().Trim(),
                    objRiesgoIndicador.strNominador.ToString().Trim(),
                    objRiesgoIndicador.strDenominador.ToString().Trim(),
                    objRiesgoIndicador.intIdMeta.ToString().Trim(),
                    objRiesgoIndicador.dblMeta.ToString().Trim(),
                    objRiesgoIndicador.strAño.ToString().Trim(),
                    objRiesgoIndicador.strMes.ToString().Trim(),
                    objRiesgoIndicador.dblResultado.ToString().Trim(),
                    objRiesgoIndicador.strDescripcionSeguimiento.ToString().Trim(),
                    objRiesgoIndicador.strColor.ToString().Trim(),
                    objRiesgoIndicador.intIdEsquemaSeguimiento.ToString().Trim(),
                    objRiesgoIndicador.strUsuario.ToString().Trim(),
                    objRiesgoIndicador.dtFechaCreacion.ToString().Trim(),
                    objRiesgoIndicador.booActivo.ToString().Trim()
                    });
            }
        }

        #endregion Metodos

        protected void GVseguimientoRiesgoInsicador_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexRiesgoIndicador = e.NewPageIndex;
            /*GVevaluacionDesempeño.PageIndex = PagIndex1;
            GVevaluacionDesempeño.DataBind();*/
            string strErrMsg = "";
            string CodRiesgo = Session["CodRiesgo"].ToString();
            int IdProceso = Convert.ToInt32(Session["IdProceso"].ToString());
            int Responsable = Convert.ToInt32(Session["Responsable"].ToString());
            int IdFactorRiesgo = Convert.ToInt32(Session["IdFactorRiesgo"].ToString());
            mtdLoadRiesgosIndicadores(ref strErrMsg, CodRiesgo, IdProceso, Responsable, IdFactorRiesgo);
        }

        protected void GVseguimientoRiesgoInsicador_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < GVseguimientoRiesgoInsicador.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVseguimientoRiesgoInsicador.Rows[rowIndex];
                string strColor = string.Empty;
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 8)
                    {
                        strColor = ((Label)row.FindControl("strColor")).Text;
                        ((Label)row.FindControl("strColor")).Visible = false;
                        ImageButton ImbRango = ((ImageButton)row.FindControl("ImbRango"));
                        if (strColor == "Amarillo")
                            ImbRango.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        if (strColor == "Rojo")
                            ImbRango.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        if (strColor == "")
                            ImbRango.Visible = false;
                    }
                }
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void GenerarReporte_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            string CodRiesgo = Sanitizer.GetSafeHtmlFragment(txbRiesgo.Text);
            int IdProceso = 0;
            if (ddlSubproceso.SelectedValue.ToString() != "0" && ddlSubproceso.SelectedValue.ToString() != "")
            {
                IdProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
            }
            else
            {
                if (ddlProceso.SelectedValue.ToString() != "0" && ddlProceso.SelectedValue.ToString() != "")
                {
                    IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue.ToString() != "0" && ddlMacroproceso.SelectedValue.ToString() != "")
                    {
                        IdProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                    }
                }
            }
            int Responsable = 0;
            if (lblIdDependencia4.Text != "")
                Responsable = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(lblIdDependencia4.Text));
            Session["CodRiesgo"] = CodRiesgo;
            Session["IdProceso"] = IdProceso;
            Session["Responsable"] = Responsable;
            int IdFactorRiesgo = 0;
            Session["IdFactorRiesgo"] = IdFactorRiesgo;
            if (ddlFactorRiesgo.SelectedValue != "---")
                IdFactorRiesgo = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            else
                IdFactorRiesgo = 0;
            try
            {
                bool booResult = false;
                DataTable dtExc = new DataTable();
                dtExc = mtdConsultarRiesgosIndicadores1(ref strErrMsg, CodRiesgo, IdProceso,
                    Responsable, IdFactorRiesgo);

                dtExc.TableName = "Seguimiento Indicadores";

                List<clsDTOriesgosIndicadores> lstRiesgoInd = new List<clsDTOriesgosIndicadores>();
                clsBLLriesgosIndicadores cRiesgoInd = new clsBLLriesgosIndicadores();
                lstRiesgoInd = cRiesgoInd.mtdConsultarRiesgosIndicadores(booResult, ref strErrMsg, CodRiesgo, IdProceso, Responsable, IdFactorRiesgo);

                List<clsDtoOrigenIndicadoresReporte> lstRiesgoIndreporte = new List<clsDtoOrigenIndicadoresReporte>();
                foreach (clsDTOriesgosIndicadores dato in lstRiesgoInd)
                {
                    clsDtoOrigenIndicadoresReporte dato2 = new clsDtoOrigenIndicadoresReporte();

                    dato2.Codigo = dato.intIdRiesgoIndicador;
                    dato2.NombreIndicador = dato.strNombreIndicador;
                    dato2.ObjetivoIndicador = dato.strObjetivoIndicador;
                    dato2.ResponsableMedicion = dato.strResponsableMedicion;
                    dato2.FrecuenciaMedicion = dato.strFrecuenciaMedicion;
                    dato2.DescripcionFrecuencia = dato.strDescripcionFrecuencia;
                    dato2.Meta = dato.dblMeta;
                    dato2.Año = dato.strAño;
                    dato2.Mes = dato.strMes;
                    dato2.Resultado = dato.dblResultado;
                    dato2.DescripcionSeguimiento = dato.strDescripcionSeguimiento;
                    dato2.CodRiesgo = dato.strCodRiesgo;
                    dato2.NombreRiesgo = dato.strNombreRiesgo;
                    lstRiesgoIndreporte.Add(dato2);

                }



                DataTable AUX = new DataTable();

               // AUX = (DataTable)lstRiesgoInd;



                if (dtExc.Rows.Count > 0)
                {
                    exportExcel(lstRiesgoIndreporte, Response, "Seguimiento Indicadores " + DateTime.Now + "");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al generar reporte Excel.", 2, "Aviso");
            }
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {

            //XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add(dt);

            //HttpResponse httpResponse = Response;
            //httpResponse.Clear();
            //httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    workbook.SaveAs(memoryStream);
            //    memoryStream.WriteTo(httpResponse.OutputStream);
            //    memoryStream.Close();

            //}
            //httpResponse.End(); 

            try
            {


                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                Response.ContentEncoding = System.Text.Encoding.Default;
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
                System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
                dg.DataSource = dt;
                dg.DataBind();
                dg.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                HttpContext.Current.Response.End();

            }
            catch (System.Threading.ThreadAbortException e)
            {
                string ees = e.Message;
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                //  omb.ShowMessage($"Error al generar el reporte. {ex.Message}", 1, "Atencion");
            }
        }
        public static void exportExcel(
                  List<clsDtoOrigenIndicadoresReporte> lstRiesgoInd, HttpResponse Response, string filename)
        {


            try
            {


                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                Response.ContentEncoding = System.Text.Encoding.Default;
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
                System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
                dg.DataSource = lstRiesgoInd;
                dg.DataBind();
                dg.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (System.Threading.ThreadAbortException e)
            {
                string ees = e.Message;
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                //  omb.ShowMessage($"Error al generar el reporte. {ex.Message}", 1, "Atencion");
            }
        }

        public DataTable mtdConsultarRiesgosIndicadores1(ref string strErrMsg, string CodRiesgo, int IdProceso,
           int Responsable, int IdFactorRiesgo)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            string condicion = string.Empty;
            #endregion Vars

            try
            {
                if (!string.IsNullOrEmpty(CodRiesgo))
                {

                    string texto = CodRiesgo;
                    Match m = Regex.Match(texto, "(\\d+)");
                    string num = string.Empty;

                    if (m.Success)
                    {
                        num = m.Value;
                    }



                    //string aux = CodRiesgo.Replace("R", string.Empty);
                    // aux = aux.Replace("r", string.Empty);
                    //CodRiesgo.Replace("R", string.Empty);
                    //CodRiesgo.Replace("r", string.Empty);


                    condicion = string.Format(" and ( b.IdRiesgoAsociado = {0})", num);

                    //condicion = string.Format(" and ( Codigo = '{0}')", CodRiesgo);

                }
                if (IdProceso != 0)
                {
                    if (string.IsNullOrEmpty(condicion))
                    {
                        condicion = string.Format(" and ( a.IdProceso = {0})", IdProceso);
                    }
                    else
                    {
                        condicion += string.Format(" AND (a.IdProceso = {0})", IdProceso);
                    }
                }
                if (Responsable != 0)
                {
                    if (string.IsNullOrEmpty(condicion))
                    {
                        condicion = string.Format(" and (a.IdResponsableMedicion = {0})", Responsable);
                    }
                    else
                    {
                        condicion += string.Format(" AND (a.IdResponsableMedicion = {0})", Responsable);
                    }
                }
                if (IdFactorRiesgo != 0)
                {
                    if (string.IsNullOrEmpty(condicion))
                    {
                        condicion = string.Format(" and (a.IdClasificacionRiesgo = {0})", IdFactorRiesgo);
                    }
                    else
                    {
                        condicion += string.Format(" AND (a.IdClasificacionRiesgo = {0})", IdFactorRiesgo);
                    }
                }
                //                strConsulta = string.Format("SELECT a.[IdRiesgoIndicador],a.[NombreIndicador],a.[ObjetivoIndicador]"
                //                    + ",a.[NombreHijo] as Responsable,a.[FrecuenciaMedicion]," +
                //                    "case  when a.[Descripcion] is not null then a.[Descripcion] ELSE c.ValorOtraFrecuencia end as Descripcion,"
                //                    + "a.[Meta],a.[ValorMinimo]"
                //                    + ",a.[ValorMaximo],a.[DescripcionSeguimiento], a.Año, a.mes, d.[Codigo] as CodRiesgo"
                //                   + " FROM[Riesgos].[vwRiesgosIndicadores] as a"
                //+ " left join[Riesgos].[RiesgosIndicadoresAsociados] as b on(a.IdRiesgoIndicador = b.IdIndicador)"
                //+ "left join [Riesgos].[Riesgo]as d on(b.IdRiesgoAsociado=d.IdRiesgo)"
                //+ " inner join[Riesgos].[RiesgosIndicadoresMetas] as c on a.IdMeta = c.IdMeta where a.Activo = 1 {0} ", condicion
                //                    );

                strConsulta = string.Format("SELECT [IdRiesgoIndicador],[NombreIndicador],[ObjetivoIndicador],[IdProcesoIndicador]"
                  + ",[IdProceso],[NombreProceso],[IdResponsableMedicion],[NombreHijo],[IdFrecuenciaMedicion],[FrecuenciaMedicion],[Descripcion],[IdRiesgoAsociado],"
                  + "[Codigo],[Nombre],[IdFormula],[Nominador],[Denominador],[IdMeta],[Meta],[IdEsquemaSeguimiento],[ValorMinimo]"
                  + ",[ValorMaximo],[DescripcionSeguimiento],[Usuario],[FechaCreacion],[Activo],[IdClasificacionRiesgo], Año, mes, porcentaje"
                  + " FROM [Riesgos].[vwRiesgosIndicadores] where Activo = 1 {0}  ", condicion
                  );

                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los indicadores de riesgos. [{0}]", ex.Message);

            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInfo;
        }

      
    }
}