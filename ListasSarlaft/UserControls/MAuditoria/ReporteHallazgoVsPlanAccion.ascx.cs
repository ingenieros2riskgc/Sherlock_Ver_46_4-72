using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using DataSets = System.Data;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.MAuditoria
{

    public partial class ReporteHallazgoVsPlanAccion : System.Web.UI.UserControl
    {
        string IdFormulario = "3010";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TreeNodeCollection nodes = this.TreeView1.Nodes;

                if (nodes.Count <= 0)
                    PopulateTreeView();
            }
        }

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
        }

        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        private DataTable GetTreeViewData()
        {
            // Get JerarquiaOrganizacional table
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional]";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        /// <summary>
        /// Filter the data to get only the rows that have a
        /// null ParentID (these are the top-level TreeView items)
        /// </summary>
        private void AddTopTreeViewNodes(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                TreeView1.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        /// <summary>
        /// Recursively add child TreeView items by filtering by ParentID
        /// </summary>
        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;

            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());

                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependencia.Text = TreeView1.SelectedNode.Text;
            lblIdDependencia.Text = TreeView1.SelectedNode.Value;
        }
        #endregion

        #region DDL

        protected void ddlMacroProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProceso.Items.Clear();
            ddlProceso.DataBind();
            txtProceso.Text = string.Empty;
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProceso.Text = ddlProceso.SelectedItem.Text;
            lblIdProceso.Text = ddlProceso.SelectedValue;
        }

        protected void ddlMacroProceso_DataBound(object sender, EventArgs e)
        {
            ddlMacroProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProceso_DataBound(object sender, EventArgs e)
        {
            ddlProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlNivelRiesgo_DataBound(object sender, EventArgs e)
        {
            ddlNivelRiesgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEstado_DataBound(object sender, EventArgs e)
        {
            ddlEstado.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }
        #endregion

        #region Buttons
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            bool booOK = true;
            string strErrMsg = string.Empty, strIdProceso = string.Empty, strIdEstado = string.Empty,
                strIdNivelRiesgo = string.Empty, strEdadHallazgo = string.Empty;

            DataSets.DataSet dsReporte = new DataSets.DataSet();

            if (ddlProceso.SelectedValue.Trim() != "0")
                strIdProceso = lblIdProceso.Text.Trim();

            if (ddlEstado.SelectedValue.Trim() != "0")
                strIdEstado = ddlEstado.SelectedValue.Trim();

            if (ddlNivelRiesgo.SelectedValue.Trim() != "0")
                strIdNivelRiesgo = ddlNivelRiesgo.SelectedValue.Trim();

            if (!string.IsNullOrEmpty(tbxEdadHallazgo.Text.Trim()))
            {
                if (mtdValidarNumero(tbxEdadHallazgo.Text.Trim()))
                    strEdadHallazgo = tbxEdadHallazgo.Text.Trim();
                else
                {
                    booOK = false;
                    mtdMensaje("La edad de Hallazgo debe ser un número. Por favor verificar la información ingresada.");
                }
            }

            if (booOK)
            {
                trRptFactorSenal.Visible = true;

                dsReporte = cAu.mtdConsultarHallazgoVsPlanAccion(strIdNivelRiesgo, strEdadHallazgo,
                    strIdProceso, strIdEstado, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    ReportDataSource rdsSource = new ReportDataSource("DSHallazgoVsPlanAccion", dsReporte.Tables["DataSet1"]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rdsSource);
                }
                else
                {
                    trRptFactorSenal.Visible = false;
                    mtdMensaje(strErrMsg);
                }
            }

            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mtdLimpiarCampos();
        }
        #endregion

        #region Methods
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdLimpiarCampos()
        {
            ddlEstado.SelectedIndex = 0;
            ddlProceso.SelectedIndex = 0;
            ddlMacroProceso.SelectedIndex = 0;
            ddlNivelRiesgo.SelectedIndex = 0;
            txtDependencia.Text = string.Empty;
            txtProceso.Text = string.Empty;
            lblIdDependencia.Text = string.Empty;
            lblIdProceso.Text = string.Empty;
            tbxEdadHallazgo.Text = string.Empty;
            trRptFactorSenal.Visible = false;
        }

        private bool mtdValidarNumero(string strCadena)
        {
            bool booResult = true;

            if (!System.Text.RegularExpressions.Regex.IsMatch(strCadena, "^[0-9]*$"))
                booResult = false;

            return booResult;
        }
        #endregion Methods
    }
}