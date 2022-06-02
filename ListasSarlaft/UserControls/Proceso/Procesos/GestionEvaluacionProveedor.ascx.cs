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
    public partial class GestionEvaluacionProveedor : System.Web.UI.UserControl
    {
        string IdFormulario = "4024";
        cCuenta cCuenta = new cCuenta();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.btnInsertarNuevo);
            scriptManager.RegisterPostBackControl(this.IBprocess);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    
                    mtdStard();
                    PopulateTreeView();
                    mtdInicializarValores();
                    
                }
            }
        }
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
            if (!mtdLoadEvaluacionProveedor(ref strErrMsg))
                omb.ShowMessage("No hay datos para cargar", 2, "Atención");
        }
        protected void mtdRestFields()
        {
            DVtituloObservaciones.Visible = false;
            DVcontentObservaciones.Visible = false;
            trTotal.Visible = false;
            ResultadoDesempeño.Visible = false;
            TituloProveedorEva.Visible = false;
            BodyFormGEP.Visible = false;
            BodyGridGEP.Visible = true;
            Dbutton.Visible = false;

            txtId.Text = "";
            TXnombreProveedor.Text = "";
            TXfechaEva.Text = "";
            TXfechaEvaInicial.Text = "";
            TXfechaEvaFin.Text = "";
            TXproducto.Text = "";
            TXpuntajeTotal.Text = "";
            LvalorFinalCalificacion.Text = "";
            TXObservaciones.Text = "";
            txtrealizado.Text = "";
            tbxResponsable.Text = "";
            TXfechanext.Text = "";

            txtFecha.Text = "";
            tbxUsuarioCreacion.Text = "";

            TXnombreProveedor.Enabled = true;
            TXfechaEva.Enabled = true;
            TXfechaEvaInicial.Enabled = true;
            TXfechaEvaFin.Enabled = true;
            TXproducto.Enabled = true;
            txtrealizado.Enabled = true;
            tbxResponsable.Enabled = true;

            GVProveedorCriterios.Visible = false;
            GVresultProveedor.Visible = false;
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
        #region ButtonEvents
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            Session["IdEvaProvee"] = 0;
            BodyGridGEP.Visible = false;
            BodyFormGEP.Visible = true;
            //Dbutton.Visible = true;
            trTituloTotal.Visible = true;
            trValorTotal.Visible = true;
            cancel.Visible = true;
            Bcancel.Visible = true;
            mtdLoadData();
        }

       
        #endregion
        #region Metodos
        private void mtdLoadData()
        {
            string strErrMsg = string.Empty;
            mtdLoadProveedorCriterio(ref strErrMsg);
            ResultadoDesempeño.Visible = true;
        }
        #region LoadGrid
        private bool mtdLoadEvaluacionProveedor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionProveedor objProveedor = new clsEvaluacionProveedor();
            List<clsEvaluacionProveedor> lstEvaProveedor = new List<clsEvaluacionProveedor>();
            clsValorEvaluacionProveedorBLL cProveedor = new clsValorEvaluacionProveedorBLL();
            #endregion Vars
            lstEvaProveedor = cProveedor.mtdConsultarEvaluacionProveedor(ref lstEvaProveedor, ref strErrMsg);

            if (lstEvaProveedor != null)
            {
                mtdLoadEvaluacionProveedor();
                mtdLoadEvaluacionProveedor(lstEvaProveedor);
                GVevaluacionProveedor.DataSource = lstEvaProveedor;
                GVevaluacionProveedor.PageIndex = pagIndex2;
                GVevaluacionProveedor.DataBind();
                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadEvaluacionProveedor()
        {
            DataTable gridEva = new DataTable();

            gridEva.Columns.Add("intId", typeof(string));
            //gridEva.Columns.Add("strNombreProveedor", typeof(string));
            gridEva.Columns.Add("intCargoResponsable", typeof(string));
            gridEva.Columns.Add("strNombreResponsable", typeof(string));
            gridEva.Columns.Add("dtFechaEvaluacion", typeof(string));
            gridEva.Columns.Add("dtPeriodoEvaluadoInicial", typeof(string));
            gridEva.Columns.Add("dtPeriodoEvaluadoFinal", typeof(string));
            gridEva.Columns.Add("strServicioOfrecido", typeof(string));
            //gridEva.Columns.Add("strRealizadoPor", typeof(string));
            //gridEva.Columns.Add("strObservaciones", typeof(string));
            gridEva.Columns.Add("dtFechaRegistro", typeof(string));
            gridEva.Columns.Add("strUsuario", typeof(string));
            gridEva.Columns.Add("dtFechaProximaEvaluacion", typeof(string));

            GVevaluacionProveedor.DataSource = gridEva;
            GVevaluacionProveedor.DataBind();
            InfoGrid2 = gridEva;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadEvaluacionProveedor(List<clsEvaluacionProveedor> lstProveedor)
        {
            string strErrMsg = String.Empty;
            clsValorEvaluacionProveedorBLL cCrlInfra = new clsValorEvaluacionProveedorBLL();

            foreach (clsEvaluacionProveedor objEvaComp in lstProveedor)
            {

                InfoGrid2.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    //objEvaComp.strNombreProveedor.ToString().Trim(),
                    objEvaComp.intCargoResponsable.ToString().Trim(),
                    objEvaComp.strNombreResponsable.ToString().Trim(),
                    objEvaComp.dtFechaEvaluacion.ToString().Trim(),
                    objEvaComp.dtPeriodoEvaluadoInicial.ToString().Trim(),
                    objEvaComp.dtPeriodoEvaluadoFinal.ToString().Trim(),
                    objEvaComp.strServicioOfrecido.ToString().Trim(),
                    //objEvaComp.strRealizadoPor.ToString().Trim(),
                    objEvaComp.dtFechaRegistro.ToString().Trim(),
                    objEvaComp.strUsuario.ToString().Trim(),
                    objEvaComp.dtFechaProximaEvaluacion.ToString().Trim()
                    });
            }
        }
        private bool mtdLoadProveedorCriterio(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsProveedorCriterios objProveedor = new clsProveedorCriterios();
            List<clsProveedorCriterios> lstProveedor = new List<clsProveedorCriterios>();
            clsValorEvaluacionProveedorBLL cProveedor = new clsValorEvaluacionProveedorBLL();
            #endregion Vars
            lstProveedor = cProveedor.mtdConsultarProveedorCriterios(ref lstProveedor, ref strErrMsg);

            if (lstProveedor != null)
            {
                mtdLoadProveedorCriterio();
                mtdLoadProveedorCriterio(lstProveedor);
                GVProveedorCriterios.DataSource = lstProveedor;
                GVProveedorCriterios.PageIndex = pagIndex1;
                GVProveedorCriterios.DataBind();
                GVProveedorCriterios.Visible = true;
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadProveedorCriterio()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreAspecto", typeof(string));
            grid.Columns.Add("intValorPorcentaje", typeof(string));
            grid.Columns.Add("strDesCriterio", typeof(string));
            grid.Columns.Add("strDesParametro", typeof(string));

            GVProveedorCriterios.DataSource = grid;
            GVProveedorCriterios.DataBind();
            InfoGrid1 = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadProveedorCriterio(List<clsProveedorCriterios> lstProveedor)
        {
            string strErrMsg = String.Empty;
            clsValorEvaluacionProveedorBLL cCrlInfra = new clsValorEvaluacionProveedorBLL();

            foreach (clsProveedorCriterios objEvaComp in lstProveedor)
            {

                InfoGrid1.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strNombreAspecto.ToString().Trim(),
                    objEvaComp.intValorPorcentaje.ToString().Trim(),
                    objEvaComp.strDesCriterio.ToString().Trim(),
                    objEvaComp.strDesParametro.ToString().Trim()
                    });
            }
        }
        private bool mtdLoadProveedorCriterioH(ref string strErrMsg, ref int IdEvalProveedor)
        {
            #region Vars
            bool booResult = false;
            clsProveedorCriterios objProveedor = new clsProveedorCriterios();
            List<clsProveedorCriterios> lstProveedor = new List<clsProveedorCriterios>();
            clsValorEvaluacionProveedorBLL cProveedor = new clsValorEvaluacionProveedorBLL();
            #endregion Vars
            lstProveedor = cProveedor.mtdConsultarProveedorCriteriosH(ref lstProveedor, ref strErrMsg, ref IdEvalProveedor);

            if (lstProveedor != null)
            {
                mtdLoadProveedorCriterioH();
                mtdLoadProveedorCriterioH(lstProveedor);
                GVresultProveedor.DataSource = lstProveedor;
                GVresultProveedor.PageIndex = pagIndex1;
                GVresultProveedor.DataBind();
                booResult = true;
                GVresultProveedor.Visible = true;
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadProveedorCriterioH()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreAspecto", typeof(string));
            grid.Columns.Add("intValorPorcentaje", typeof(string));
            grid.Columns.Add("strDesCriterio", typeof(string));
            grid.Columns.Add("strDesParametro", typeof(string));
            grid.Columns.Add("intValorPuntaje", typeof(string));
            grid.Columns.Add("intCalificacion", typeof(string));

            GVresultProveedor.DataSource = grid;
            GVresultProveedor.DataBind();
            InfoGrid3 = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadProveedorCriterioH(List<clsProveedorCriterios> lstProveedor)
        {
            string strErrMsg = String.Empty;
            clsValorEvaluacionProveedorBLL cCrlInfra = new clsValorEvaluacionProveedorBLL();

            foreach (clsProveedorCriterios objEvaComp in lstProveedor)
            {

                InfoGrid3.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strNombreAspecto.ToString().Trim(),
                    objEvaComp.intValorPorcentaje.ToString().Trim(),
                    objEvaComp.strDesCriterio.ToString().Trim(),
                    objEvaComp.strDesParametro.ToString().Trim(),
                    objEvaComp.intValorPuntaje.ToString().Trim(),
                    objEvaComp.intCalificacion.ToString().Trim()
                    });
            }
        }
        public static void MergeRows(GridView gridView)
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
                        string text = ((Label)row.FindControl("codigo")).Text;
                        string previousText = ((Label)previousRow.FindControl("codigo")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("NombreAspecto")).Text;
                        string previousText = ((Label)previousRow.FindControl("NombreAspecto")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 3)
                    {
                        string text = ((Label)row.FindControl("DesCriterio")).Text;
                        string previousText = ((Label)previousRow.FindControl("DesCriterio")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 4)
                    {
                        string text = ((Label)row.FindControl("DesParametro")).Text;
                        string previousText = ((Label)previousRow.FindControl("DesParametro")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    /*if (cellIndex == 6)
                    {
                        decimal text =  Convert.ToDecimal(((Label)row.FindControl("Lcalificacion")).Text);
                        decimal previousText = Convert.ToDecimal(((Label)previousRow.FindControl("Lcalificacion")).Text);
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }*/
                }
            }
        }
        public static void MergeRowsH(GridView gridView)
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
                        string text = ((Label)row.FindControl("NombreAspecto")).Text;
                        string previousText = ((Label)previousRow.FindControl("NombreAspecto")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 2)
                    {
                        string text = ((Label)row.FindControl("DesCriterio")).Text;
                        string previousText = ((Label)previousRow.FindControl("DesCriterio")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 3)
                    {
                        string text = ((Label)row.FindControl("DesParametro")).Text;
                        string previousText = ((Label)previousRow.FindControl("DesParametro")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    /*if (cellIndex == 6)
                    {
                        decimal text =  Convert.ToDecimal(((Label)row.FindControl("Lcalificacion")).Text);
                        decimal previousText = Convert.ToDecimal(((Label)previousRow.FindControl("Lcalificacion")).Text);
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }*/
                }
            }
        }
        public static void MergeRowsValue(GridView gridView)
        {
            for (int rowIndex = 1; rowIndex < gridView.Rows.Count; rowIndex++)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex - 1];

                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    
                    if (cellIndex == 6)
                    {
                        string text =  ((Label)row.FindControl("NombreAspecto")).Text;
                        string previousText = ((Label)previousRow.FindControl("NombreAspecto")).Text;
                        if (text == previousText)
                        {
                            previousRow.Cells[cellIndex].RowSpan = row.Cells[cellIndex].RowSpan < 2 ? 2 : row.Cells[cellIndex].RowSpan + 1;
                            row.Cells[cellIndex].Visible = false;
                        }
                    }
                }
            }
        }
        public static void MergeRowsValueH(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    
                    if (cellIndex == 6)
                    {
                        decimal text =  Convert.ToDecimal(((Label)row.FindControl("NombreAspecto")).Text);
                        decimal previousText = Convert.ToDecimal(((Label)previousRow.FindControl("NombreAspecto")).Text);
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                }
            }
        }
        private void mtdTotalEvaluacion(GridView gridView)
        {
            for (int j = 0; j < gridView.Rows.Count; j++)
            {
                GridViewRow rowParametro = gridView.Rows[j];
                string criterio = ((Label)rowParametro.FindControl("DesParametro")).Text;
            }
        }
        #endregion

        protected void GVProveedorCriterios_PreRender(object sender, EventArgs e)
        {
            if (GVProveedorCriterios.Rows.Count > 1)
            {
                MergeRows(GVProveedorCriterios);
            }
        }
        #endregion

        protected void IBprocess_Click(object sender, ImageClickEventArgs e)
        {
            mtdProcessValues();
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }

        protected void mtdProcessValues()
        {
            string nombre = string.Empty;
            //decimal value = 0;
            decimal total = 0;
            decimal totalPrevious = 0;
            string aspectoPrevious = string.Empty;
            //decimal porcentajePrevious = 0;
            decimal valuePrevious = 0;
            foreach (GridViewRow GridViewRow in GVProveedorCriterios.Rows)
            {

                
                string aspecto = ((Label)GridViewRow.FindControl("NombreAspecto")).Text;
                decimal porcentaje = Convert.ToDecimal(((Label)GridViewRow.FindControl("ValorPorcentaje")).Text);
                decimal value = Convert.ToDecimal(((TextBox)GridViewRow.FindControl("TXpuntajeAsignado")).Text);
                porcentaje = porcentaje / 100;
                /*if (aspecto == aspectoPrevious)
                {
                    if(totalPrevious != 0)
                        totalPrevious = totalPrevious + value;
                    else
                        totalPrevious = valuePrevious + value;
                    total = porcentaje * totalPrevious;
                    ((Label)GridViewRow.FindControl("Lcalificacion")).Text = "" + total;
                    
                }
                else
                {*/
                    total = value * porcentaje;
                    ((Label)GridViewRow.FindControl("Lcalificacion")).Text = "" + total;
                    //string LpuntajeAsignado = ((Label)GridViewRow.FindControl("LpuntajeAsignado")).Text;
                    aspectoPrevious = ((Label)GridViewRow.FindControl("NombreAspecto")).Text;
                    //porcentajePrevious = Convert.ToDecimal(((Label)GridViewRow.FindControl("ValorPorcentaje")).Text);
                    valuePrevious = Convert.ToDecimal(((TextBox)GridViewRow.FindControl("TXpuntajeAsignado")).Text);
                //}
            }
            /*for (int rowIndex = GVProveedorCriterios.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = GVProveedorCriterios.Rows[rowIndex];
                GridViewRow previousRow = GVProveedorCriterios.Rows[rowIndex + 1];
                decimal value = 0;
                decimal previousValue = 0;
                
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("NombreAspecto")).Text;
                        string previousText = ((Label)previousRow.FindControl("NombreAspecto")).Text;
                        decimal porcentaje = Convert.ToDecimal(((Label)previousRow.FindControl("ValorPorcentaje")).Text);
                        porcentaje = porcentaje / 100;
                        //value = Convert.ToDecimal(((TextBox)row.FindControl("TXpuntajeAsignado")).Text);
                        previousValue = Convert.ToDecimal(((TextBox)previousRow.FindControl("TXpuntajeAsignado")).Text);
                        if (text == previousText)
                        {
                            //total = value * porcentaje;
                            //total = porcentaje * total;
                            totalPrevious = previousValue * porcentaje;
                            total = total + totalPrevious;
                            ((Label)previousRow.FindControl("Lcalificacion")).Text = "" + total;
                        }
                        else
                        {
                            
                            total = previousValue * porcentaje;
                            ((Label)previousRow.FindControl("Lcalificacion")).Text = "" + total;
                            //total = 0;
                        }
                    }
                }
                ((Label)row.FindControl("Lcalificacion")).Text = "" + total;
                
            }**/

            trTotal.Visible = true;
            //MergeRowsValue(GVProveedorCriterios);
            mtdTotalValues(GVProveedorCriterios);
        }
        protected void mtdProcesGrid(GridView grid)
        {
            string nombre = string.Empty;
            //decimal value = 0;
            decimal total = 0;

            for (int rowIndex = grid.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = grid.Rows[rowIndex];
                GridViewRow previousRow = grid.Rows[rowIndex + 1];
                decimal value = 0;
                decimal previousValue = 0;
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("NombreAspecto")).Text;
                        string previousText = ((Label)previousRow.FindControl("NombreAspecto")).Text;
                        decimal porcentaje = Convert.ToDecimal(((Label)row.FindControl("ValorPorcentaje")).Text);
                        porcentaje = porcentaje / 100;
                        value = Convert.ToDecimal(((Label)row.FindControl("puntaje")).Text);
                        previousValue = Convert.ToDecimal(((Label)previousRow.FindControl("puntaje")).Text);
                        if (text == previousText)
                        {
                            total = value + previousValue;
                            total = porcentaje * total;
                            ((Label)previousRow.FindControl("Lcalificacion")).Text = "" + total;
                        }
                        else
                        {

                            total = value * porcentaje;
                        }
                    }
                }
                ((Label)row.FindControl("Lcalificacion")).Text = "" + total;

            }

            trTotal.Visible = true;
            MergeRowsValueH(grid);
            mtdTotalValues(grid);
        }
        protected void mtdTotalValues(GridView GridView)
        {
            decimal Total = 0;
            foreach (GridViewRow GridViewRow in GridView.Rows)
            {
                decimal porcentaje = Convert.ToDecimal(((Label)GridViewRow.FindControl("ValorPorcentaje")).Text);
                decimal puntaje = Convert.ToDecimal(((TextBox)GridViewRow.FindControl("TXpuntajeAsignado")).Text);
                porcentaje = porcentaje / 100;
                decimal value = puntaje * porcentaje;
                Total += value;
            }
            TXpuntajeTotal.Text = "" + Total;
            string strErrMsg = string.Empty;
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            clsValorEvaluacionProveedorBLL cProveedorBll = new clsValorEvaluacionProveedorBLL();
            /*List<clsDetalleEvaluacionDesempeño> lstDetalleDesempeño = new List<clsDetalleEvaluacionDesempeño>();
            clsValorEvaluacionDesempeñoBLL clsDesempeño = new clsValorEvaluacionDesempeñoBLL();
            clsDetalleEvaluacionDesempeño objDesempeño = new clsDetalleEvaluacionDesempeño();*/
            lstCalificaciones = cProveedorBll.mtdGetCalificaciones(ref strErrMsg);
            if (lstCalificaciones != null)
            {
                mtdLoadCalificaciones(lstCalificaciones, Convert.ToDecimal(Total));
                DVtituloObservaciones.Visible = true;
                DVcontentObservaciones.Visible = true;
            }
            else
            {
                omb.ShowMessage("No hay Calificaciones registradas", 1, "Atención");
                LvalorFinalCalificacion.Text = "No hay Calificaciones registradas";
            }
            
        }
        protected void mtdTotalValuesPrint(GridView GridView)
        {
            decimal Total = 0;
            foreach (GridViewRow GridViewRow in GridView.Rows)
            {
                decimal porcentaje = Convert.ToDecimal(((Label)GridViewRow.FindControl("ValorPorcentaje")).Text);
                decimal puntaje = Convert.ToDecimal(((Label)GridViewRow.FindControl("puntaje")).Text);
                porcentaje = porcentaje / 100;
                decimal value = puntaje * porcentaje;
                Total += value;
            }
            TXpuntajeTotal.Text = "" + Total;
            string strErrMsg = string.Empty;
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            clsValorEvaluacionProveedorBLL cProveedorBll = new clsValorEvaluacionProveedorBLL();
            /*List<clsDetalleEvaluacionDesempeño> lstDetalleDesempeño = new List<clsDetalleEvaluacionDesempeño>();
            clsValorEvaluacionDesempeñoBLL clsDesempeño = new clsValorEvaluacionDesempeñoBLL();
            clsDetalleEvaluacionDesempeño objDesempeño = new clsDetalleEvaluacionDesempeño();*/
            lstCalificaciones = cProveedorBll.mtdGetCalificaciones(ref strErrMsg);
            if (lstCalificaciones != null)
            {
                mtdLoadCalificaciones(lstCalificaciones, Convert.ToDecimal(Total));
                DVtituloObservaciones.Visible = true;
                DVcontentObservaciones.Visible = true;
                trTotal.Visible = true;
            }
            else
            {
                omb.ShowMessage("No hay Calificaciones registradas", 1, "Atención");
            }

        }
        private void mtdLoadCalificaciones(List<clsCalificacionEvaluacion> lstCalificaciones, decimal TotalDesempeño)
        {
            foreach (clsCalificacionEvaluacion objCalificacion in lstCalificaciones)
            {
                if (TotalDesempeño >= objCalificacion.intValorMinimo && TotalDesempeño <= objCalificacion.intValorMaximo)
                {
                    LvalorFinalCalificacion.Text = objCalificacion.strDescripcion;
                }
            }
            string text = LvalorFinalCalificacion.Text;
            if (text == "")
            {
                LvalorFinalCalificacion.Text = "Valor total fuera de la escala de la evaluación";
            }
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdInsertarValorEvaluacionProveedor(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                mtdStard();
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
            if (mtdModificarValorEvaluacionProveedor(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                mtdStard();
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdRestFields();
            mtdStard();
        }
        /// <summary>
        /// Realiza la insercion de la evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        protected bool mtdInsertarValorEvaluacionProveedor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionProveedor objEvaProveeInd = new clsEvaluacionProveedor();
            clsValorEvaluacionProveedorBLL cValorEvaluacionInd = new clsValorEvaluacionProveedorBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objEvaProveeInd.intId = 0;
            objEvaProveeInd.strNombreProveedor = TXnombreProveedor.Text;
            objEvaProveeInd.intCargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
            objEvaProveeInd.dtFechaEvaluacion = TXfechaEva.Text;
            objEvaProveeInd.dtPeriodoEvaluadoInicial = TXfechaEvaInicial.Text;
            objEvaProveeInd.dtPeriodoEvaluadoFinal = TXfechaEvaFin.Text;
            objEvaProveeInd.strServicioOfrecido = TXproducto.Text;
            objEvaProveeInd.strRealizadoPor = txtrealizado.Text;
            objEvaProveeInd.strObservaciones = TXObservaciones.Text;
            objEvaProveeInd.dtFechaRegistro = DateTime.Now;
            objEvaProveeInd.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objEvaProveeInd.dtFechaProximaEvaluacion = TXfechanext.Text;
            booResult = cValorEvaluacionInd.mtdInsertarValorEvaluacionProveedor(objEvaProveeInd, ref strErrMsg);
            booResult = true;
            if (booResult == true)
            {
                int IdEvaProveedor = cValorEvaluacionInd.mtdLastIdEvaluacionProveedor(ref strErrMsg);
                booResult = mtdInsertarAspectos(GVProveedorCriterios, IdEvaProveedor, ref cValorEvaluacionInd,ref strErrMsg);
                strErrMsg = "Evaluación de proveedor registrada exitosamente";
            }
            else
            {
                strErrMsg = "Error al registrar la evaluación proveedor";
            }
            return booResult;
        }
        protected bool mtdInsertarAspectos(GridView Grid, int IdEvaProveedor, ref clsValorEvaluacionProveedorBLL cValorEvaluacionInd, ref string strErrMsg)
        {
            bool booResult = false;
            clsAspectoProveedorHistorico cAspectoHistorico = new clsAspectoProveedorHistorico();
            clsCriterioProvHistorico cCriterioHistorico = new clsCriterioProvHistorico();
            clsParametrosProvHistorico cParametrosHistorico = new clsParametrosProvHistorico();
            foreach (GridViewRow GridViewRow in Grid.Rows)
            {
                cAspectoHistorico.strAspecto = ((Label)GridViewRow.FindControl("NombreAspecto")).Text;
                cAspectoHistorico.intValor = Convert.ToDecimal(((Label)GridViewRow.FindControl("ValorPorcentaje")).Text);
                cAspectoHistorico.intEvaluacionProveedor = IdEvaProveedor;
                cAspectoHistorico.dtFechaRegistro = DateTime.Now;
                cAspectoHistorico.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());

                booResult = cValorEvaluacionInd.mtdInsertarValorAspectoProveedor(cAspectoHistorico, ref strErrMsg);

                int IdAspectoProveedor = cValorEvaluacionInd.mtdLastIdAspectoProveedor(ref strErrMsg);
                cCriterioHistorico.intIdAspectoProveedorHistorico = IdAspectoProveedor;
                cCriterioHistorico.strDescripcion = ((Label)GridViewRow.FindControl("DesCriterio")).Text;
                cCriterioHistorico.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
                cCriterioHistorico.dtFechaRegistro = DateTime.Now;

                booResult = cValorEvaluacionInd.mtdInsertarValorCriterioProveedor(cCriterioHistorico, ref strErrMsg);

                int IdCriterioProveedor = cValorEvaluacionInd.mtdLastIdCriterioProveedor(ref strErrMsg);
                cParametrosHistorico.dtFechaRegistro = DateTime.Now;
                cParametrosHistorico.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
                cParametrosHistorico.strDescripcion = ((Label)GridViewRow.FindControl("DesParametro")).Text;
                cParametrosHistorico.intIdCriterioProvHistorico = IdCriterioProveedor;
                cParametrosHistorico.intValorParametro = Convert.ToDecimal(((TextBox)GridViewRow.FindControl("TXpuntajeAsignado")).Text);

                booResult = cValorEvaluacionInd.mtdInsertarValorParametroProveedor(cParametrosHistorico, ref strErrMsg);
            }
            
            return booResult;
        }
        /// <summary>
        /// Realiza la insercion de la evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        protected bool mtdModificarValorEvaluacionProveedor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionProveedor objEvaProveeInd = new clsEvaluacionProveedor();
            clsValorEvaluacionProveedorBLL cValorEvaluacionInd = new clsValorEvaluacionProveedorBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objEvaProveeInd.intId = Convert.ToInt32(txtId.Text);
            objEvaProveeInd.strNombreProveedor = TXnombreProveedor.Text;
            objEvaProveeInd.intCargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
            objEvaProveeInd.dtFechaEvaluacion = TXfechaEva.Text;
            objEvaProveeInd.dtPeriodoEvaluadoInicial = TXfechaEvaInicial.Text;
            objEvaProveeInd.dtPeriodoEvaluadoFinal = TXfechaEvaFin.Text;
            objEvaProveeInd.strServicioOfrecido = TXproducto.Text;
            objEvaProveeInd.strRealizadoPor = txtrealizado.Text;
            objEvaProveeInd.strObservaciones = TXObservaciones.Text;
            objEvaProveeInd.dtFechaRegistro = Convert.ToDateTime(txtFecha.Text);
            objEvaProveeInd.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objEvaProveeInd.dtFechaProximaEvaluacion = TXfechanext.Text;
            booResult = cValorEvaluacionInd.mtdModificarValorEvaluacionProveedor(objEvaProveeInd, ref strErrMsg);
            booResult = true;
            if (booResult == true)
            {
                strErrMsg = "Evaluación de proveedor actualizada  exitosamente";
            }
            else
            {
                strErrMsg = "Error al actualizada la evaluación proveedor";
            }
            return booResult;
        }
        protected void GVevaluacionProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid1 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid1);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    Dbutton.Visible = true;
                    break;
            }
        }
        private void mtdShowUpdate(int RowGrid1)
        {
            GridViewRow row = GVevaluacionProveedor.Rows[RowGrid1];
            var colsNoVisible = GVevaluacionProveedor.DataKeys[RowGrid1].Values;
            txtId.Text = row.Cells[0].Text;
            Session["IdEvaProvee"] = row.Cells[0].Text;
            lblIdDependencia4.Text = colsNoVisible[2].ToString();
            TXnombreProveedor.Text = ((Label)row.FindControl("NombreProveedor")).Text;
            TXnombreProveedor.Enabled = false;
            TXfechaEva.Text = row.Cells[3].Text;
            TXfechaEva.Enabled = false;
            TXfechaEvaInicial.Text = row.Cells[4].Text;
            TXfechaEvaInicial.Enabled = false;
            TXfechaEvaFin.Text = row.Cells[5].Text;
            TXfechaEvaFin.Enabled = false;
            TXproducto.Text = ((Label)row.FindControl("ServicioOfrecido")).Text;
            TXproducto.Enabled = false;
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();
            TXObservaciones.Text = colsNoVisible[4].ToString();
            txtrealizado.Text = colsNoVisible[3].ToString();
            txtrealizado.Enabled = false;
            tbxResponsable.Text = colsNoVisible[5].ToString();
            tbxResponsable.Enabled = false;
            TXfechanext.Text = row.Cells[13].Text;

            string strErrMsg = string.Empty;
            int IdEvalProveedor = Convert.ToInt32(row.Cells[0].Text);
            mtdLoadProveedorCriterioH(ref strErrMsg, ref IdEvalProveedor);

            BodyGridGEP.Visible = false;
            BodyFormGEP.Visible = true;
            TituloProveedorEva.Visible = true;
            ResultadoDesempeño.Visible = true;
            trTituloTotal.Visible = false;
            trValorTotal.Visible = false;
            cancel.Visible = false;
            Bcancel.Visible = false;
            DVtituloObservaciones.Visible = true;
            DVcontentObservaciones.Visible = true;
        }

        protected void GVresultProveedor_PreRender(object sender, EventArgs e)
        {
            if (Session["IdEvaProvee"].ToString() != "0")
            {
                MergeRowsH(GVresultProveedor);
                mtdTotalValuesPrint(GVresultProveedor);
            }
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteEvaluacionProveedores_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        private void mtdExportPdf()
        {
            #region
            Tools tools = new Tools();
            PdfPTable pdfpTableFactorDesempeño = tools.createPdftable(GVresultProveedor);



            foreach (GridViewRow GridViewRow in GVresultProveedor.Rows)
            {
                string nombre = ((Label)GridViewRow.FindControl("NombreAspecto")).Text;
                string ValorPorcentaje = ((Label)GridViewRow.FindControl("ValorPorcentaje")).Text;
                string desCriterio = ((Label)GridViewRow.FindControl("DesCriterio")).Text;
                string DesParametro = ((Label)GridViewRow.FindControl("DesParametro")).Text;
                string puntaje = ((Label)GridViewRow.FindControl("puntaje")).Text;
                string Lcalificacion = ((Label)GridViewRow.FindControl("Lcalificacion")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        PdfPCell pdfCell = tools.createPdfPCell(GVresultProveedor, nombre);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        PdfPCell pdfCell = tools.createPdfPCell(GVresultProveedor, ValorPorcentaje);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        PdfPCell pdfCell = tools.createPdfPCell(GVresultProveedor, desCriterio);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        PdfPCell pdfCell = tools.createPdfPCell(GVresultProveedor, DesParametro);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    if (iteracion == 4)
                    {
                        PdfPCell pdfCell = tools.createPdfPCell(GVresultProveedor, puntaje);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    if (iteracion == 5)
                    {
                        PdfPCell pdfCell = tools.createPdfPCell(GVresultProveedor, Lcalificacion);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
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
            pdfDocument.AddTitle("Reporte de Evaluación de Proveedor");
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
            font1.Color = new Color(GVresultProveedor.HeaderStyle.ForeColor);
            PdfPTable pdfTableData = new PdfPTable(4);
            /*PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Nombre y Apellidos:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);*/
            PdfPCell pdfCellEncabezado = tools.createPdfPCellgrid("Nombre del Proveedor:", font1, GVresultProveedor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXnombreProveedor.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = tools.createPdfPCellgrid("Fecha de la Evaluación:", font1, GVresultProveedor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaEva.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = tools.createPdfPCellgrid("Fecha de la Evaluación Periodo Inicial:", font1, GVresultProveedor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaEvaInicial.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = tools.createPdfPCellgrid("Fecha de la Evaluación Periodo Final:", font1, GVresultProveedor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaEvaFin.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            #endregion
            #region Tabla Pie Pagina
            PdfPTable pdfTableFooter = new PdfPTable(4);
            PdfPCell pdfCellFooter = tools.createPdfPCellgrid("Puntaje Total:", font1, GVresultProveedor);
            pdfTableFooter.AddCell(pdfCellFooter);
            pdfCellFooter = new PdfPCell(new Phrase(TXpuntajeTotal.Text));
            pdfTableFooter.AddCell(pdfCellFooter);
            pdfCellFooter = tools.createPdfPCellgrid("Calificación Total:", font1, GVresultProveedor);
            pdfTableFooter.AddCell(pdfCellFooter);
            pdfCellFooter = new PdfPCell(new Phrase(LvalorFinalCalificacion.Text));
            pdfTableFooter.AddCell(pdfCellFooter);
            PdfPTable pdfTableFooter2 = new PdfPTable(2);
            PdfPCell pdfCellFooter2 = tools.createPdfPCellgrid("Observaciones:", font1, GVresultProveedor);
            pdfTableFooter2.AddCell(pdfCellFooter2);
            pdfCellFooter2 = new PdfPCell(new Phrase(TXObservaciones.Text));
            pdfTableFooter2.AddCell(pdfCellFooter2);
            PdfPTable pdfTableFooter3 = new PdfPTable(6);
            PdfPCell pdfCellFooter3 = tools.createPdfPCellgrid("Realizado por:", font1, GVresultProveedor);
            pdfTableFooter3.AddCell(pdfCellFooter3);
            pdfCellFooter3 = new PdfPCell(new Phrase(txtrealizado.Text));
            pdfTableFooter3.AddCell(pdfCellFooter3);
            pdfCellFooter3 = tools.createPdfPCellgrid("Cargo:", font1, GVresultProveedor);
            pdfTableFooter3.AddCell(pdfCellFooter3);
            pdfCellFooter3 = new PdfPCell(new Phrase(tbxResponsable.Text));
            pdfTableFooter3.AddCell(pdfCellFooter3);
            pdfCellFooter3 = tools.createPdfPCellgrid("Fecha próxima Evaluación:", font1, GVresultProveedor);
            pdfTableFooter3.AddCell(pdfCellFooter3);
            pdfCellFooter3 = new PdfPCell(new Phrase(TXfechanext.Text));
            pdfTableFooter3.AddCell(pdfCellFooter3);
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
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Evaluación de Proveedor"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableFactorDesempeño);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableFooter);
            pdfDocument.Add(pdfTableFooter2);
            pdfDocument.Add(pdfTableFooter3);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteEvaluacionProveedor.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        public void exportExcel(HttpResponse Response, string filename)
        {
            Tools tools = new Tools();
            #region encabezado
            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Nombre del Proveedor:");
            gridEncabezado.Columns.Add("Fecha de la Evaluación:");
            gridEncabezado.Columns.Add("Fecha de la Evaluación Periodo Inicial:");
            gridEncabezado.Columns.Add("Fecha de la Evaluación Periodo Final:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Nombre del Proveedor:"] = TXnombreProveedor.Text;
            rowEncabezado["Fecha de la Evaluación:"] = TXfechaEva.Text;
            rowEncabezado["Fecha de la Evaluación Periodo Inicial:"] = TXfechaEvaInicial.Text;
            rowEncabezado["Fecha de la Evaluación Periodo Final:"] = TXfechaEvaFin.Text;
            gridEncabezado.Rows.Add(rowEncabezado);
            #endregion
            #region Valor Evaluacion
            DataTable gridEvaValue = new DataTable();
            gridEvaValue.Columns.Add("Nombre Aspecto:");
            gridEvaValue.Columns.Add("Valor Porcentaje:");
            gridEvaValue.Columns.Add("Descripción Criterio:");
            gridEvaValue.Columns.Add("Descripción Parametro:");
            gridEvaValue.Columns.Add("Puntaje Asignado:");
            gridEvaValue.Columns.Add("Calificación:");
            DataRow rowEvaValue;
            foreach (GridViewRow GridViewRow in GVresultProveedor.Rows)
            {
                rowEvaValue = gridEvaValue.NewRow();
                string NombreAspecto = ((Label)GridViewRow.FindControl("NombreAspecto")).Text;
                string ValorPorcentaje = ((Label)GridViewRow.FindControl("ValorPorcentaje")).Text;
                string DesCriterio = ((Label)GridViewRow.FindControl("DesCriterio")).Text;
                string DesParametro = ((Label)GridViewRow.FindControl("DesParametro")).Text;
                string puntaje = ((Label)GridViewRow.FindControl("puntaje")).Text;
                string Lcalificacion = ((Label)GridViewRow.FindControl("Lcalificacion")).Text;
                rowEvaValue["Nombre Aspecto:"] = NombreAspecto;
                rowEvaValue["Valor Porcentaje:"] = ValorPorcentaje;
                rowEvaValue["Descripción Criterio:"] = DesCriterio;
                rowEvaValue["Descripción Parametro:"] = DesParametro;
                rowEvaValue["Puntaje Asignado:"] = puntaje;
                rowEvaValue["Calificación:"] = Lcalificacion;

                gridEvaValue.Rows.Add(rowEvaValue);
            }
            #endregion
            #region Pie de Pagina
            DataTable gridFooter = new DataTable();
            gridFooter.Columns.Add("Puntaje Total:");
            gridFooter.Columns.Add("Calificación Total:");
            gridFooter.Columns.Add("Observaciones:");
            gridFooter.Columns.Add("Realizado por:");
            gridFooter.Columns.Add("Cargo:");
            gridFooter.Columns.Add("Fecha próxima Evaluación:");
            DataRow rowFooter;
            rowFooter = gridFooter.NewRow();
            rowFooter["Puntaje Total:"] = TXpuntajeTotal.Text;
            rowFooter["Calificación Total:"] = LvalorFinalCalificacion.Text;
            rowFooter["Observaciones:"] = TXObservaciones.Text;
            rowFooter["Realizado por:"] = txtrealizado.Text;
            rowFooter["Cargo:"] = tbxResponsable.Text;
            rowFooter["Fecha próxima Evaluación:"] = TXfechanext.Text;
            gridFooter.Rows.Add(rowFooter);
            #endregion
            /*System.Data.DataSet ds = new System.Data.DataSet();
            gridEncabezado.TableName = "Encabezado";
            InfoGrid3.TableName = "ResultadosEvaluacion";
            gridFooter.TableName = "Pie de Pagina";
            ds.Tables.Add(gridEncabezado);
            ds.Tables.Add(InfoGrid3);
            ds.Tables.Add(gridFooter);
            tools.exportExcel(ds, "Proveedor", Response, "Reporte Evaluación Proveedor");*/
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "Encabezado");
            workbook.Worksheets.Add(gridEvaValue, "ResultadosEvaluacion");
            workbook.Worksheets.Add(gridFooter, "Observaciones");
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

        protected void GVevaluacionProveedor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex2 = e.NewPageIndex;
            GVevaluacionProveedor.PageIndex = PagIndex2;
            GVevaluacionProveedor.DataBind();
            string strErrMsg = "";
            mtdLoadEvaluacionProveedor(ref strErrMsg);
        }
    }
}