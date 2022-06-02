using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class MAuditoriaGestion : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TreeNodeCollection nodes = this.TreeView1.Nodes;
            if (nodes.Count <= 0)
                PopulateTreeView();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                TextBox1.Text = "";
                //txtrowid .Text  = Gridview1.SelectedRow.Cells[1].Text.Trim(); en C#
                //TextBox1.Text = GridView1.SelectedRow.Cells(0).Text
                //txtNombre.Text = GridView1.SelectedRow.Cells(1).Text
                //txtMod.Text = GridView1.SelectedRow.Cells(2).Text
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                Panel11.Visible = true;
            }
        }


        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Panel11.Visible = false;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }


        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            TextBox1.Text = "";
            TextBox1.Enabled = true;
            btnImgInsertar.Visible = true;
            btnImgActualizar.Visible = false;
            Panel11.Visible = true;
        }



        protected void btnGestion_Click(object sender, EventArgs e)
        {
            pnlAuditoria.Visible = false;
            pnlGestion.Visible = true;
        }

        protected void bntIrAuditoria3_Click(object sender, EventArgs e)
        {
            pnlAuditoria.Visible = true;
            pnlGestion.Visible = false;
        }

        protected void bntIrAuditoria2_Click(object sender, EventArgs e)
        {
            pnlAuditoria.Visible = true;
            pnlGestion.Visible = false;
        }

        protected void bntIrAuditoria1_Click(object sender, EventArgs e)
        {
            pnlAuditoria.Visible = true;
            pnlGestion.Visible = false;
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            Panel20.Visible = true;
        }

        protected void imgBtnRiesgo_Click(object sender, EventArgs e)
        {
            TabContainer1.ActiveTabIndex = 2;
        }

        protected void imgBtnRecomendacion_Click(object sender, EventArgs e)
        {

            TabContainer1.ActiveTabIndex = 1;

        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCodHallazgoRec.Text = GridView4.SelectedRow.Cells[3].Text.Trim();
            lblHallazgoRec.Text = GridView4.SelectedRow.Cells[4].Text.Trim();
            lblCodHallazgoRie.Text = GridView4.SelectedRow.Cells[3].Text.Trim();
            lblHallazgoRie.Text = GridView4.SelectedRow.Cells[4].Text.Trim();
        }

        protected void imgBtnInsertarP2_Click(object sender, ImageClickEventArgs e)
        {
            Panel20.Visible = true;
        }

        protected void btnImgCancelarP1_Click(object sender, ImageClickEventArgs e)
        {
            Panel20.Visible = false;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel3.Visible = true;
        }

        protected void imgBtnInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            Panel3.Visible = true;
        }

        protected void btnImgCancelarRec_Click_Click(object sender, ImageClickEventArgs e)
        {
            Panel3.Visible = false;
        }

        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        protected void imgBtnInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            Panel1.Visible = true;
        }

        protected void btnImgCancelarRie_Click_Click(object sender, ImageClickEventArgs e)
        {
            Panel1.Visible = false;
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedItem.Value == "Procesos")
            {
                filaDependencia.Visible = false;
                filaProceso.Visible = true;
            }
            else
            {
                filaDependencia.Visible = true;
                filaProceso.Visible = false;
            }
        }

        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
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

        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();
            //TextBox1.Text = GridView1.SelectedRow.Cells(0).Text
            //txtNombre.Text = GridView1.SelectedRow.Cells(1).Text
            //txtMod.Text = GridView1.SelectedRow.Cells(2).Text
        }

    }
}