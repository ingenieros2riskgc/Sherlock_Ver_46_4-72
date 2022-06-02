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
using ListasSarlaft.Classes.Utilerias;
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
    public partial class GestionEvaluacionDesempeño : System.Web.UI.UserControl
    {
        string IdFormulario = "4023";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.IBprocess);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    string strErrMsg = string.Empty;
                    if (!mtdCargarEvaDesempeño(ref strErrMsg))
                        omb.ShowMessage("No hay evaluacion a cargar", 2, "Atención");
                }
            }
        }
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
            //txtFecha.Text = ""+DateTime.Now;
            PagIndex3 = 0;
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
        #region GridEventos
        protected void GVfactoresDesempeño_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid1 = /*(Convert.ToInt16(GVfactoresDesempeño.PageSize) * PagIndex1) + */Convert.ToInt16(e.CommandArgument);
            string strErrMsg = String.Empty;
            switch (e.CommandName)
            {
                case "Criterios":
                    
                    if (!mtdLoadCriterios(RowGrid1, ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    modalPopup.Show();
                    break;
            }
        }
        protected void GVevaluacionDesempeño_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid3 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid3);
                    Dbutton.Visible = true;
                    break;
            }
        }        
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            mtdShowForm();
        }
        #region ButtonEvents
        protected void Bok_Click(object sender, EventArgs e)
        {
            decimal valor = Convert.ToDecimal(TXvalorCalificacion.Text);
            int intIdFactor = Convert.ToInt32(Session["intIdFactor"].ToString());
            for (int j = 0; j < GVfactoresDesempeño.Rows.Count; j++)
            {
                GridViewRow rowCompetencia = GVfactoresDesempeño.Rows[j];
                int id = Convert.ToInt32(InfoGrid1.Rows[j][0].ToString().Trim());
                if (id == intIdFactor)
                {

                    ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignado")).Text = "" + valor;
                }
            }
            TXvalorCalificacion.Text = "";
            modalPopup.Hide();
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarForm();
        }
        protected void IBprocess_Click(object sender, ImageClickEventArgs e)
        {
            decimal valorTotal = 0;
            string strErrMsg = String.Empty;
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            clsFactoresDesempenoBLL cFactorBll = new clsFactoresDesempenoBLL();
            lstCalificaciones = cFactorBll.mtdGetCalificaciones(ref strErrMsg);

            for (int j = 0; j < GVfactoresDesempeño.Rows.Count; j++)
            {
                GridViewRow rowCompetencia = GVfactoresDesempeño.Rows[j];
                decimal valor = Convert.ToDecimal(((TextBox)rowCompetencia.FindControl("TXpuntajeAsignado")).Text);
                valorTotal = valorTotal + valor;
            }
            TXpuntajeTotal.Text = "" + valorTotal;
            mtdLoadCalificaciones(lstCalificaciones, valorTotal);

            trTotal.Visible = true;
            DVtituloRecomendacion.Visible = true;
            DVcontentRecomendacion.Visible = true;
            //IBinsertGVC.Visible = true;
            trEspecifique.Visible = false;
            TRcancel.Visible = false;
            TRcancelButton.Visible = false;
        }
        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdInsertarValorEvaluacionDesempeño(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdLimpiarForm();
                BodyGridGED.Visible = true;
                if (!mtdCargarEvaDesempeño(ref strErrMsg))
                    omb.ShowMessage("No hay evaluacion a cargar", 1, "Atención");
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
                mtdLimpiarForm();
                BodyGridGED.Visible = true;
                if (!mtdCargarEvaDesempeño(ref strErrMsg))
                    omb.ShowMessage("No hay evaluacion a cargar", 1, "Atención");
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
                if (mtdUpdateEvaluacionDesempeño(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdLimpiarForm();
                BodyGridGED.Visible = true;
                mtdCargarEvaDesempeño(ref strErrMsg);
            }
        }
        #endregion
        #endregion
        #region Metodos
        private void mtdShowForm()
        {
            string strErrMsg = String.Empty;
            BodyGridGED.Visible = false;
            BodyFormGED.Visible = true;
            mtdCargarFactores(ref strErrMsg);
            TituloDesempeñoEva.Visible = true;
            ResultadoDesempeño.Visible = true;
            PopulateTreeView();
            trTituloTotal.Visible = true;
            trValorTotal.Visible = true;
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
            TRcancel.Visible = true;
            TRcancelButton.Visible = true;
        }
        private void mtdShowFormActualizacion()
        {
            string strErrMsg = String.Empty;
            BodyGridGED.Visible = false;
            BodyFormGED.Visible = true;
            //mtdCargarFactores(ref strErrMsg);
            TituloDesempeñoEva.Visible = true;
            ResultadoDesempeño.Visible = true;
            trTotal.Visible = true;
            PopulateTreeView();
            trTituloTotal.Visible = false;
            trValorTotal.Visible = false;
            DVtituloRecomendacion.Visible = true;
            DVcontentRecomendacion.Visible = true;
        }
        private void mtdLimpiarForm()
        {
            BodyGridGED.Visible = true;
            BodyFormGED.Visible = false;
            TituloDesempeñoEva.Visible = false;
            ResultadoDesempeño.Visible = false;
            DVtituloRecomendacion.Visible = false;
            DVcontentRecomendacion.Visible = false;
            Dbutton.Visible = false;
            trTituloTotal.Visible = false;
            trTotal.Visible = false;

            mtdLimpiarControls();
        }
        private void mtdLimpiarControls()
        {
            txtId.Text = "";
            TXnombreEvaluado.Text = "";
            TXnombreEvaluado.Enabled = true;
            tbxResponsable.Text = "";
            tbxResponsable.Enabled = true;
            txtNombreEva.Text = "";
            txtNombreEva.Enabled = true;
            TXfechaEva.Text = "";
            TXfechaEva.Enabled = true;
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
            DDLrecomendacion.SelectedIndex = 0;
            DDLrecomendacion.Enabled = true;
            TXespecifique.Text = "";
            TXespecifique.Enabled = true;
            TXcompromisos.Text = "";
            TXcompromisos.Enabled = true;
            TXfechanext.Text = "";
            TXfechanext.Enabled = true;
            TXpuntajeTotal.Text = "";
            TXpuntajeTotal.Enabled = true;
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
        #region Gridview Evaluacion Desempeño
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarEvaDesempeño(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridEvaDesempeño();
            booResult = mtdLoadInfoGridEvaDesempeño(ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridEvaDesempeño()
        {
            DataTable gridDesempeño = new DataTable();

            gridDesempeño.Columns.Add("intId", typeof(string));
            gridDesempeño.Columns.Add("strNombre", typeof(string));
            gridDesempeño.Columns.Add("intCargoResponsable", typeof(string));
            gridDesempeño.Columns.Add("strCargo", typeof(string));
            gridDesempeño.Columns.Add("dtFechaEvaluacion", typeof(string));
            gridDesempeño.Columns.Add("strEvaluador", typeof(string));
            gridDesempeño.Columns.Add("intCalificacion", typeof(string));
            gridDesempeño.Columns.Add("strRecomendacionCapacitacion", typeof(string));
            gridDesempeño.Columns.Add("strRecomendacionCompromisos", typeof(string));
            gridDesempeño.Columns.Add("strOtros", typeof(string));
            gridDesempeño.Columns.Add("strDescripcionOtros", typeof(string));
            gridDesempeño.Columns.Add("dtFechaProximaEvaluacion", typeof(string));
            gridDesempeño.Columns.Add("strUsuario", typeof(string));
            gridDesempeño.Columns.Add("dtFechaRegistro", typeof(string));

            GVevaluacionDesempeño.DataSource = gridDesempeño;
            GVevaluacionDesempeño.DataBind();
            InfoGrid3 = gridDesempeño;
        }
        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la evaluacion de desempeño al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridEvaDesempeño(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsEvaluacionDesempeno> lstEvaluacionDesempeño = new List<clsEvaluacionDesempeno>();
            clsValorEvaluacionDesempeñoBLL cEvaDesempeño = new clsValorEvaluacionDesempeñoBLL();
            clsEvaluacionDesempeno objEvaDempOut = new clsEvaluacionDesempeno();
            lstEvaluacionDesempeño = cEvaDesempeño.mtdConsultarEvaluacionDesempeño(ref objEvaDempOut, ref strErrMsg);

                if (lstEvaluacionDesempeño != null)
                {
                    mtdLoadInfoGridDesempeño(lstEvaluacionDesempeño);
                    GVevaluacionDesempeño.DataSource = lstEvaluacionDesempeño;
                    GVevaluacionDesempeño.PageIndex = PagIndex1;
                    GVevaluacionDesempeño.DataBind();

                    booResult = true;
                }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los Factores</param>
        private void mtdLoadInfoGridDesempeño(List<clsEvaluacionDesempeno> lstEvaluacionDesempeño)
        {
            foreach (clsEvaluacionDesempeno objEvaDesempeño in lstEvaluacionDesempeño)
            {
                InfoGrid3.Rows.Add(new Object[] {
                    objEvaDesempeño.intId.ToString().Trim(),
                    objEvaDesempeño.strNombre.ToString().Trim(),
                    objEvaDesempeño.intCargoResponsable.ToString().Trim(),
                    objEvaDesempeño.strCargo.ToString().Trim(),
                    objEvaDesempeño.dtFechaEvaluacion.ToString().Trim(),
                    objEvaDesempeño.strEvaluador.ToString().Trim(),
                    objEvaDesempeño.intCalificacion.ToString().Trim(),
                    objEvaDesempeño.strRecomendacionCapacitacion.ToString().Trim(),
                    objEvaDesempeño.strRecomendacionCompromisos.ToString().Trim(),
                    objEvaDesempeño.strOtros.ToString().Trim(),
                    objEvaDesempeño.strDescripcionOtros.ToString().Trim(),
                    objEvaDesempeño.dtFechaProximaEvaluacion.ToString().Trim(),
                    objEvaDesempeño.strUsuario.ToString().Trim(),
                    objEvaDesempeño.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #region Gridview Factores
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarFactores(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridFactores();
            booResult = mtdLoadInfoGridFactores(ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridFactores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strFactoresEvaluacion", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GVfactoresDesempeño.DataSource = grid;
            GVfactoresDesempeño.DataBind();
            InfoGrid1 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos del factor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridFactores(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsFactoresDesempeno> lstFactor = new List<clsFactoresDesempeno>();
            clsFactoresDesempenoBLL cFactor = new clsFactoresDesempenoBLL();

            booResult = cFactor.mtdConsultarFactor(ref lstFactor, ref strErrMsg);

            if (booResult)
                if (lstFactor != null)
                {
                    mtdLoadInfoGridFactores(lstFactor);
                    GVfactoresDesempeño.DataSource = lstFactor;
                    GVfactoresDesempeño.DataBind();
                }

            return booResult;
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los Factores</param>
        private void mtdLoadInfoGridFactores(List<clsFactoresDesempeno> lstFactor)
        {
            foreach (clsFactoresDesempeno objFactor in lstFactor)
            {
                InfoGrid1.Rows.Add(new Object[] {
                    objFactor.intId.ToString().Trim(),
                    objFactor.strFactoresEvaluacion.ToString().Trim(),
                    objFactor.intIdUsuario.ToString().Trim(),
                    objFactor.strNombreUsuario.ToString().Trim(),
                    objFactor.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        private bool mtdLoadCriterios(int rowIndex, ref string strErrMsg)
        {
            //Response.Write("<script>window.open('../../UserControls/Procesos/Popup/PopupEvaluacionDesempeño.aspx','popup','width=800,height=500') </script>");
            bool booResult = false;

            Session["intIdFactor"] = Convert.ToInt32(InfoGrid1.Rows[rowIndex][0].ToString().Trim());
            clsFactoresDesempeno objFactor = new clsFactoresDesempeno(
                Convert.ToInt32(InfoGrid1.Rows[rowIndex][0].ToString().Trim()),
                string.Empty, 0, string.Empty);

            booResult = mtdCargarDetallesFactor(objFactor, ref strErrMsg);

            return booResult;
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
        #region Gridview Detalles Factores
        private bool mtdCargarDetallesFactor(clsFactoresDesempeno objFactor, ref string strErrMsg)
        {
            bool booResult = false;
            List<clsDetalleFactorDesempeno> lstDetFactor = new List<clsDetalleFactorDesempeno>();
            clsFactoresDesempenoBLL cFactor = new clsFactoresDesempenoBLL();

            booResult = cFactor.mtdConsultarDetFactor(objFactor, ref lstDetFactor, ref strErrMsg);

            if (booResult)
            {
                mtdLoadGridDetFactor();
                mtdLoadInfoGridDetFactor(lstDetFactor, ref strErrMsg);
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridDetFactor()
        {
            DataTable grid2 = new DataTable();

            grid2.Columns.Add("intId", typeof(string));
            grid2.Columns.Add("strDescripcion", typeof(string));
            grid2.Columns.Add("intIdFactoresDesempeno", typeof(string));
            grid2.Columns.Add("strNombreFactor", typeof(string));
            grid2.Columns.Add("intIdCalificacion", typeof(string));
            grid2.Columns.Add("strNombreCalificacion", typeof(string));
            grid2.Columns.Add("intIdUsuario", typeof(string));
            grid2.Columns.Add("strNombreUsuario", typeof(string));
            grid2.Columns.Add("dtFechaRegistro", typeof(string));
            grid2.Columns.Add("decCriterioMinimo", typeof(string));
            grid2.Columns.Add("decCriterioMaximo", typeof(string));

            GVfactorDesempeño.DataSource = grid2;
            GVfactorDesempeño.DataBind();
            InfoGrid2 = grid2;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de los Detalles Factor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridDetFactor(List<clsDetalleFactorDesempeno> lstDetFactor, ref string strErrMsg)
        {
            if (lstDetFactor != null)
            {
                mtdLoadInfoGridDetFactor(lstDetFactor);
                GVfactorDesempeño.DataSource = lstDetFactor;
                GVfactorDesempeño.DataBind();
            }
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los criterios</param>
        private void mtdLoadInfoGridDetFactor(List<clsDetalleFactorDesempeno> lstDetFactor)
        {
            foreach (clsDetalleFactorDesempeno objDetFactor in lstDetFactor)
            {
                InfoGrid2.Rows.Add(new Object[] {
                    objDetFactor.intId.ToString().Trim(),
                    objDetFactor.strDescripcion.ToString().Trim(),
                    objDetFactor.intIdFactoresDesempeno.ToString().Trim(),
                    objDetFactor.strNombreFactor.ToString().Trim(),
                    objDetFactor.intIdCalificacion.ToString().Trim(),
                    objDetFactor.strNombreCalificacion.ToString().Trim(),
                    objDetFactor.intIdUsuario.ToString().Trim(),
                    objDetFactor.strNombreUsuario.ToString().Trim(),
                    objDetFactor.dtFechaRegistro.ToString().Trim(),
                    objDetFactor.decCriterioMinimo,
                    objDetFactor.decCriterioMaximo
                    });
            }
        }
        /// <summary>
        /// Realiza la insercion del Indicador
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        private bool mtdInsertarValorEvaluacionDesempeño(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionDesempeno objEvaCompInd = new clsEvaluacionDesempeno();
            clsValorEvaluacionDesempeñoBLL cValorEvaluacionInd = new clsValorEvaluacionDesempeñoBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objEvaCompInd.strNombre = TXnombreEvaluado.Text;
            objEvaCompInd.intCargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
            objEvaCompInd.dtFechaEvaluacion = TXfechaEva.Text;
            objEvaCompInd.strEvaluador = txtNombreEva.Text;
            objEvaCompInd.intCalificacion = Convert.ToDecimal(TXpuntajeTotal.Text);
            string[] textos = TXcompromisos.Text.Split('\n');
            string Compromisos = string.Empty;
            if (textos.Length > 0)
            {
                for (int i = 0; i < textos.Length; i++)
                {
                    if (textos[i].ToString() != "")
                    Compromisos += "|" + (i + 1) + "." + textos[i].ToString();
                }
            }
            int Recomendacion = Convert.ToInt32(DDLrecomendacion.SelectedValue);
            if (Recomendacion == 1)
            {
                objEvaCompInd.strRecomendacionCapacitacion = Compromisos;
                objEvaCompInd.strRecomendacionCompromisos = "N/A";
                objEvaCompInd.strOtros = "N/A";
                objEvaCompInd.strDescripcionOtros = "N/A";
            }
            if (Recomendacion == 2)
            {
                objEvaCompInd.strRecomendacionCapacitacion = "N/A";
                objEvaCompInd.strRecomendacionCompromisos = Compromisos;
                objEvaCompInd.strOtros = "N/A";
                objEvaCompInd.strDescripcionOtros = "N/A";
            }
            if (Recomendacion == 3)
            {
                objEvaCompInd.strRecomendacionCapacitacion = "N/A";
                objEvaCompInd.strRecomendacionCompromisos = "N/A";
                objEvaCompInd.strOtros = TXespecifique.Text;
                objEvaCompInd.strDescripcionOtros = Compromisos;
            }
            objEvaCompInd.dtFechaProximaEvaluacion = TXfechanext.Text;
            objEvaCompInd.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            objEvaCompInd.dtFechaRegistro = DateTime.Now;
            booResult = cValorEvaluacionInd.mtdInsertarValorEvaluacionDesempeño(objEvaCompInd, ref strErrMsg);
            int IdEvaluacionDesempeño = cValorEvaluacionInd.mtdLastIdEvaluacionDesempeño(ref strErrMsg);
            for (int j = 0; j < GVfactoresDesempeño.Rows.Count; j++)
            {
                GridViewRow rowCompetencia = GVfactoresDesempeño.Rows[j];
                decimal valor = Convert.ToDecimal(((TextBox)rowCompetencia.FindControl("TXpuntajeAsignado")).Text);
                string nombreFactor = ((Label)rowCompetencia.FindControl("Factor")).Text;
                booResult = cValorEvaluacionInd.mtdInsertarValorDetalleDesempeño(ref nombreFactor,ref  valor, ref IdEvaluacionDesempeño, ref strErrMsg);
            }
            if (booResult == true)
            {
                strErrMsg = "Evaluación desempeño registrada exitosamente";
            }
            
            return booResult;
        }
        private bool mtdUpdateEvaluacionDesempeño(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionDesempeno objEvaCompInd = new clsEvaluacionDesempeno();
            clsValorEvaluacionDesempeñoBLL cValorEvaluacionInd = new clsValorEvaluacionDesempeñoBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objEvaCompInd.intId = Convert.ToInt32(txtId.Text);
            objEvaCompInd.strNombre = TXnombreEvaluado.Text;
            objEvaCompInd.intCargoResponsable = 0;
            objEvaCompInd.dtFechaEvaluacion = TXfechaEva.Text;
            objEvaCompInd.strEvaluador = txtNombreEva.Text;
            objEvaCompInd.intCalificacion = Convert.ToDecimal(TXpuntajeTotal.Text);
            string[] textos = TXcompromisos.Text.Split('\n');
            string Compromisos = string.Empty;
            if (textos.Length > 0)
            {
                
                    for (int i = 0; i < textos.Length; i++)
                    {
                        if (textos[i].ToString() != "")
                        {
                        Compromisos += "|" + textos[i].ToString();
                        }
                    }
            }
            int Recomendacion = Convert.ToInt32(DDLrecomendacion.SelectedValue);
            if (Recomendacion == 1)
            {
                objEvaCompInd.strRecomendacionCapacitacion = Compromisos;
                objEvaCompInd.strRecomendacionCompromisos = "N/A";
                objEvaCompInd.strOtros = "N/A";
                objEvaCompInd.strDescripcionOtros = "N/A";
            }
            if (Recomendacion == 2)
            {
                objEvaCompInd.strRecomendacionCapacitacion = "N/A";
                objEvaCompInd.strRecomendacionCompromisos = Compromisos;
                objEvaCompInd.strOtros = "N/A";
                objEvaCompInd.strDescripcionOtros = "N/A";
            }
            if (Recomendacion == 3)
            {
                objEvaCompInd.strRecomendacionCapacitacion = "N/A";
                objEvaCompInd.strRecomendacionCompromisos = "N/A";
                objEvaCompInd.strOtros = TXespecifique.Text;
                objEvaCompInd.strDescripcionOtros = Compromisos;
            }
            objEvaCompInd.dtFechaProximaEvaluacion = TXfechanext.Text;
            objEvaCompInd.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            //objEvaCompInd.dtFechaRegistro = DateTime.Now;

            booResult = cValorEvaluacionInd.mtdUpdateEvaluacionDesempeño(ref objEvaCompInd, ref strErrMsg);

            if (booResult == true)
            {
                strErrMsg = "Evaluación desempeño actualizada exitosamente";
            }
            else
            {
                strErrMsg = "Error al registrar la evaluación desempeño";
            }
            return booResult;
        }
        private void mtdShowUpdate(int RowGrid3)
        {
            GridViewRow row = GVevaluacionDesempeño.Rows[RowGrid3];
            var colsNoVisible = GVevaluacionDesempeño.DataKeys[RowGrid3].Values;
            txtId.Text = row.Cells[0].Text;
            TXnombreEvaluado.Text = ((Label)row.FindControl("NombreEvaluado")).Text;
            TXnombreEvaluado.Enabled = false;
            lblIdDependencia4.Text = colsNoVisible[0].ToString();
            tbxResponsable.Text = ((Label)row.FindControl("strCargo")).Text;
            tbxResponsable.Enabled = false;
            imgDependencia4.Visible = false;
            txtNombreEva.Enabled = false;
            TXfechaEva.Enabled = false;
            txtNombreEva.Text = ((Label)row.FindControl("nombreEvaluador")).Text;
            TXfechaEva.Text = row.Cells[4].Text;
            TXpuntajeTotal.Text = colsNoVisible[3].ToString();
            string reco = colsNoVisible[4].ToString();
            if (reco != "N/A")
            {
                DDLrecomendacion.SelectedIndex = 1;
                string[] texto = reco.Split('|');
                for (int i = 0; i < texto.Length; i++)
                {
                    TXcompromisos.Text += texto[i].ToString();// + Environment.NewLine;
                }
            }
            string comp = colsNoVisible[5].ToString();
            if (comp != "N/A")
            {
                DDLrecomendacion.SelectedIndex = 2;
                string[] texto = comp.Split('|');
                for (int i = 0; i < texto.Length; i++)
                {
                    TXcompromisos.Text += texto[i].ToString();// + Environment.NewLine;
                }
            }
            string otro = colsNoVisible[6].ToString();
            string otroDescrip = colsNoVisible[7].ToString();
            if (otro != "N/A")
            {
                DDLrecomendacion.SelectedIndex = 3;
                trEspecifique.Visible = true;
                TXespecifique.Text = otro;
                string[] texto = otroDescrip.Split('|');
                for (int i = 0; i < texto.Length; i++)
                {
                    if (texto[i].ToString() != "")
                        TXcompromisos.Text += texto[i].ToString();//+ Environment.NewLine;
                }
            }
            TXfechanext.Text = row.Cells[11].Text;
            tbxUsuarioCreacion.Text = colsNoVisible[1].ToString();
            txtFecha.Text = colsNoVisible[2].ToString();
            IBinsertGVC.Visible = false;
            IBupdateGVC.Visible = true;
            mtdShowFormActualizacion();
            bool booResult = false;
            string strErrMsg = String.Empty;
            mtdLoadGridFactores();
            booResult = mtdLoadInfoGridFactores(ref strErrMsg);
            List<clsCalificacionEvaluacion> lstCalificaciones = new List<clsCalificacionEvaluacion>();
            clsFactoresDesempenoBLL cFactorBll = new clsFactoresDesempenoBLL();
            List<clsDetalleEvaluacionDesempeño> lstDetalleDesempeño = new List<clsDetalleEvaluacionDesempeño>();
            clsValorEvaluacionDesempeñoBLL clsDesempeño = new clsValorEvaluacionDesempeñoBLL();
            clsDetalleEvaluacionDesempeño objDesempeño = new clsDetalleEvaluacionDesempeño();
            lstCalificaciones = cFactorBll.mtdGetCalificaciones(ref strErrMsg);
            int IdEvaluacionDesempeño = Convert.ToInt32(row.Cells[0].Text);
            lstDetalleDesempeño = clsDesempeño.mtdConsultarDetalleDesempeño(ref objDesempeño, ref IdEvaluacionDesempeño, ref strErrMsg);
            IBinsertGVC.Visible = false;
            TRcancelButton.Visible = false;
            TRcancel.Visible = false;
            DataTable gridPrint = new DataTable();
            DataRow rowData;
            gridPrint.Columns.Add("strFactoresEvaluacion");
            gridPrint.Columns.Add("intPuntajeAsignado");
            InfoGridPrint = gridPrint;
            if (lstDetalleDesempeño != null)
            {
                for (int j = 0; j < GVfactoresDesempeño.Rows.Count; j++)
                {
                    foreach (clsDetalleEvaluacionDesempeño objDetFactor in lstDetalleDesempeño)
                    {
                        GridViewRow rowCompetencia = GVfactoresDesempeño.Rows[j];
                        string nombre = ((Label)rowCompetencia.FindControl("Factor")).Text;
                        if (objDetFactor.strNombreFactor == nombre)
                        {
                            ((TextBox)rowCompetencia.FindControl("TXpuntajeAsignado")).Text = objDetFactor.intvalorFactor.ToString();
                            rowData = gridPrint.NewRow();
                            rowData["strFactoresEvaluacion"] = nombre;
                            rowData["intPuntajeAsignado"] = objDetFactor.intvalorFactor.ToString();
                            gridPrint.Rows.Add(rowData);
                            //InfoGridPrint.Rows.Add(rowData);
                        }
                    }
                }
            }
            GVfactoresDesempeñoPrint.DataSource = gridPrint;
            GVfactoresDesempeñoPrint.DataBind();
            mtdLoadCalificaciones(lstCalificaciones, Convert.ToDecimal(colsNoVisible[3].ToString()));
        }
        #endregion
        

        protected void DDLrecomendacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLrecomendacion.SelectedValue == "3")
            {
                trEspecifique.Visible = true;
            }
            else
            {
                trEspecifique.Visible = false;
            }
        }
        #endregion

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteEvaluacionDesempeño_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }

        private void mtdExportPdf()
        {
            #region 
            Tools tools = new Tools();
            PdfPTable pdfpTableFactorDesempeño = tools.createPdftable(GVfactoresDesempeñoPrint);

            

            foreach (GridViewRow GridViewRow in GVfactoresDesempeñoPrint.Rows)
            {
                string nombre = ((Label)GridViewRow.FindControl("Factor")).Text;
                string puntaje = ((Label)GridViewRow.FindControl("puntaje")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVfactoresDesempeñoPrint.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(nombre));
                        pdfCell.BackgroundColor = new Color(GVfactoresDesempeñoPrint.RowStyle.BackColor);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVfactoresDesempeñoPrint.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(puntaje));
                        pdfCell.BackgroundColor = new Color(GVfactoresDesempeñoPrint.RowStyle.BackColor);
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
            font1.Color = new Color(GVfactoresDesempeño.HeaderStyle.ForeColor);
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Nombre y Apellidos:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXnombreEvaluado.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Cargo:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxResponsable.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Evaluación:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaEva.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Evaluador:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtNombreEva.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            #endregion
            #region Taba Datos Evaluacion
            PdfPTable pdfTableDataEva = new PdfPTable(4);
            PdfPCell pdfCellDataEva = new PdfPCell(new Phrase("Puntaje Total:", font1));
            pdfCellDataEva.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
            pdfTableDataEva.AddCell(pdfCellDataEva);
            pdfCellDataEva = new PdfPCell(new Phrase(TXpuntajeTotal.Text));
            pdfTableDataEva.AddCell(pdfCellDataEva);
            pdfCellDataEva = new PdfPCell(new Phrase("Calificación Total:", font1));
            pdfCellDataEva.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
            pdfTableDataEva.AddCell(pdfCellDataEva);
            pdfCellDataEva = new PdfPCell(new Phrase(LvalorFinalCalificacion.Text));
            pdfTableDataEva.AddCell(pdfCellDataEva);
            #endregion
            #region Taba Datos Recomendaciones
            int inrecom = Convert.ToInt32(DDLrecomendacion.SelectedIndex.ToString());
            PdfPTable pdfTableDataRecom;
            if (inrecom != 3)
            {
                pdfTableDataRecom = new PdfPTable(2);
                PdfPCell pdfCellDataRecom = new PdfPCell(new Phrase("Recomendación:", font1));
                pdfCellDataRecom.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase(DDLrecomendacion.SelectedItem.ToString()));
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase("Compromisos adquiridos por el Evaluado:", font1));
                pdfCellDataRecom.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase(TXcompromisos.Text));
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase("Fecha próxima Evaluación:", font1));
                pdfCellDataRecom.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase(TXfechanext.Text));
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
            }
            else
            {
                pdfTableDataRecom = new PdfPTable(4);
                PdfPCell pdfCellDataRecom = new PdfPCell(new Phrase("Recomendación:", font1));
                pdfCellDataRecom.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase(DDLrecomendacion.SelectedItem.ToString()));
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase("Especificación:", font1));
                pdfCellDataRecom.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase(TXespecifique.Text));
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase("Compromisos adquiridos por el Evaluado:", font1));
                pdfCellDataRecom.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase(TXcompromisos.Text));
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase("Fecha próxima Evaluación:", font1));
                pdfCellDataRecom.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
                pdfCellDataRecom = new PdfPCell(new Phrase(TXfechanext.Text));
                pdfTableDataRecom.AddCell(pdfCellDataRecom);
            }
            #endregion
            #region Taba Datos Usuario
            PdfPTable pdfTableDataUser = new PdfPTable(4);
            PdfPCell pdfCellDataUser = new PdfPCell(new Phrase("Usuario Registro:", font1));
            pdfCellDataUser.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
            pdfTableDataUser.AddCell(pdfCellDataUser);
            pdfCellDataUser = new PdfPCell(new Phrase(tbxUsuarioCreacion.Text));
            pdfTableDataUser.AddCell(pdfCellDataUser);
            pdfCellDataUser = new PdfPCell(new Phrase("Fecha registro:", font1));
            pdfCellDataUser.BackgroundColor = new Color(GVfactoresDesempeño.HeaderStyle.BackColor);
            pdfTableDataUser.AddCell(pdfCellDataUser);
            pdfCellDataUser = new PdfPCell(new Phrase(txtFecha.Text));
            pdfTableDataUser.AddCell(pdfCellDataUser);
            #endregion Usuario
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
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Evaluacion de Desempeño"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableFactorDesempeño);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableDataEva);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableDataRecom);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableDataUser);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteEvaluacionDesempeño.pdf");
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
            System.Web.UI.WebControls.DataGrid dgUser = new System.Web.UI.WebControls.DataGrid();

            #region encabezado
            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Nombre y Apellidos:");
            gridEncabezado.Columns.Add("Cargo:");
            gridEncabezado.Columns.Add("Fecha Evaluación:");
            gridEncabezado.Columns.Add("Evaluador:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Nombre y Apellidos:"] = TXnombreEvaluado.Text;
            rowEncabezado["Cargo:"] = tbxResponsable.Text;
            rowEncabezado["Fecha Evaluación:"] = TXfechaEva.Text;
            rowEncabezado["Evaluador:"] = txtNombreEva.Text;
            gridEncabezado.Rows.Add(rowEncabezado);
            
            dgEncabezado.DataSource = gridEncabezado;
            dgEncabezado.DataBind();
            dgEncabezado.RenderControl(htmlWrite);
            #endregion
            DataTable gridData = new DataTable();
            gridData.Columns.Add("Puntaje Total:");
            gridData.Columns.Add("Calificación Total:");

            DataRow rowData;
            rowData = gridData.NewRow();
            rowData["Puntaje Total:"] = TXpuntajeTotal.Text;
            rowData["Calificación Total:"] = LvalorFinalCalificacion.Text;
            gridData.Rows.Add(rowData);

            #region Taba Datos Recomendaciones
            int inrecom = Convert.ToInt32(DDLrecomendacion.SelectedIndex.ToString());
            DataTable gridRecomendacion = new DataTable();
            DataRow rowRecom;
            if (inrecom != 3)
            {
                gridRecomendacion.Columns.Add("Recomendacion:");
                gridRecomendacion.Columns.Add("Compromisos adquiridos por el Evaluado:");
                gridRecomendacion.Columns.Add("Fecha proxima Evaluación:");
                rowRecom = gridRecomendacion.NewRow();
                rowRecom["Recomendacion:"] = DDLrecomendacion.SelectedValue.ToString();
                rowRecom["Compromisos adquiridos por el Evaluado:"] = TXcompromisos.Text;
                rowRecom["Fecha proxima Evaluación:"] = TXfechanext.Text;
                gridRecomendacion.Rows.Add(rowRecom);
            }
            else
            {
                gridRecomendacion.Columns.Add("Recomendacion:");
                gridRecomendacion.Columns.Add("Especificacion:");
                gridRecomendacion.Columns.Add("Compromisos adquiridos por el Evaluado:");
                gridRecomendacion.Columns.Add("Fecha proxima Evaluación:");
                rowRecom = gridRecomendacion.NewRow();
                rowRecom["Recomendacion:"] = DDLrecomendacion.SelectedValue.ToString();
                rowRecom["Especificacion:"] = TXespecifique.Text;
                rowRecom["Compromisos adquiridos por el Evaluado:"] = TXcompromisos.Text;
                rowRecom["Fecha proxima Evaluación:"] = TXfechanext.Text;
                gridRecomendacion.Rows.Add(rowRecom);
            }
            #endregion
            DataTable gridUser = new DataTable();
            gridUser.Columns.Add("Usuario");
            gridUser.Columns.Add("Fecha Registro");

            DataRow rowUser;
            rowUser = gridUser.NewRow();
            rowUser["Usuario"] = tbxUsuarioCreacion.Text;
            rowUser["Fecha Registro"] = txtFecha.Text;
            gridUser.Rows.Add(rowUser);
            dg1.DataSource = InfoGridPrint;
            dg1.DataBind();
            dg1.RenderControl(htmlWrite);

            dgData.DataSource = gridData;
            dgData.DataBind();
            dgData.RenderControl(htmlWrite);

            dg2.DataSource = gridRecomendacion;
            dg2.DataBind();
            dg2.RenderControl(htmlWrite);

            dgUser.DataSource = gridUser;
            dgUser.DataBind();
            dgUser.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();*/
            #region encabezado
            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Nombre y Apellidos:");
            gridEncabezado.Columns.Add("Cargo:");
            gridEncabezado.Columns.Add("Fecha Evaluación:");
            gridEncabezado.Columns.Add("Evaluador:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Nombre y Apellidos:"] = TXnombreEvaluado.Text;
            rowEncabezado["Cargo:"] = tbxResponsable.Text;
            rowEncabezado["Fecha Evaluación:"] = TXfechaEva.Text;
            rowEncabezado["Evaluador:"] = txtNombreEva.Text;
            gridEncabezado.Rows.Add(rowEncabezado);
            #endregion
            DataTable gridData = new DataTable();
            gridData.Columns.Add("Puntaje Total:");
            gridData.Columns.Add("Calificación Total:");

            DataRow rowData;
            rowData = gridData.NewRow();
            rowData["Puntaje Total:"] = TXpuntajeTotal.Text;
            rowData["Calificación Total:"] = LvalorFinalCalificacion.Text;
            gridData.Rows.Add(rowData);

            #region Taba Datos Recomendaciones
            int inrecom = Convert.ToInt32(DDLrecomendacion.SelectedIndex.ToString());
            DataTable gridRecomendacion = new DataTable();
            DataRow rowRecom;
            if (inrecom != 3)
            {
                gridRecomendacion.Columns.Add("Recomendación:");
                gridRecomendacion.Columns.Add("Compromisos adquiridos por el Evaluado:");
                gridRecomendacion.Columns.Add("Fecha próxima Evaluación:");
                rowRecom = gridRecomendacion.NewRow();
                rowRecom["Recomendación:"] = DDLrecomendacion.SelectedItem.ToString();
                rowRecom["Compromisos adquiridos por el Evaluado:"] = TXcompromisos.Text;
                rowRecom["Fecha próxima Evaluación:"] = TXfechanext.Text;
                gridRecomendacion.Rows.Add(rowRecom);
            }
            else
            {
                gridRecomendacion.Columns.Add("Recomendación:");
                gridRecomendacion.Columns.Add("Especificación:");
                gridRecomendacion.Columns.Add("Compromisos adquiridos por el Evaluado:");
                gridRecomendacion.Columns.Add("Fecha próxima Evaluación:");
                rowRecom = gridRecomendacion.NewRow();
                rowRecom["Recomendación:"] = DDLrecomendacion.SelectedItem.ToString();
                rowRecom["Especificación:"] = TXespecifique.Text;
                rowRecom["Compromisos adquiridos por el Evaluado:"] = TXcompromisos.Text;
                rowRecom["Fecha próxima Evaluación:"] = TXfechanext.Text;
                gridRecomendacion.Rows.Add(rowRecom);
            }
            #endregion
            DataTable gridEvaValue = new DataTable();
            gridEvaValue.Columns.Add("Factor Desempeño:");
            gridEvaValue.Columns.Add("Puntaje Asignado:");
            DataRow rowEvaValue;
            foreach (GridViewRow GridViewRow in GVfactoresDesempeñoPrint.Rows)
            {
                rowEvaValue = gridEvaValue.NewRow();
                string Factor = ((Label)GridViewRow.FindControl("Factor")).Text;
                string puntaje = ((Label)GridViewRow.FindControl("puntaje")).Text;
                rowEvaValue["Factor Desempeño:"] = Factor;
                rowEvaValue["Puntaje Asignado:"] = puntaje;
                
                gridEvaValue.Rows.Add(rowEvaValue);
            }

            DataTable gridUser = new DataTable();
            gridUser.Columns.Add("Usuario");
            gridUser.Columns.Add("Fecha Registro");

            DataRow rowUser;
            rowUser = gridUser.NewRow();
            rowUser["Usuario"] = tbxUsuarioCreacion.Text;
            rowUser["Fecha Registro"] = txtFecha.Text;
            gridUser.Rows.Add(rowUser);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "Encabezado");
            workbook.Worksheets.Add(gridEvaValue, "Factor Desempeño");
            workbook.Worksheets.Add(gridData, "Puntaje Total");
            workbook.Worksheets.Add(gridRecomendacion, "Recomendaciones");
            workbook.Worksheets.Add(gridUser, "Información Usuario");
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

        protected void GVevaluacionDesempeño_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex1 = e.NewPageIndex;
            /*GVevaluacionDesempeño.PageIndex = PagIndex1;
            GVevaluacionDesempeño.DataBind();*/
            string strErrMsg = "";
            mtdCargarEvaDesempeño(ref strErrMsg);
        }

        protected void btnImgCancelarProcess_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarForm();
        }
    }
}