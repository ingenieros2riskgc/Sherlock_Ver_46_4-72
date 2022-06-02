using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ListasSarlaft.Formularios.Auditoria.Admin
{
    public partial class ArbolOnDemandAjax : System.Web.UI.Page
    {
        /// <summary>
        /// Only populate the TreeView when the page first loads
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            PopulateTopNodes();
        }

        /// <summary>
        /// Get the top level nodes (nodes with a null ParentId)
        /// </summary>
        private void PopulateTopNodes()
        {
        string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional] WHERE IdPadre = 0";
        string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

        SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
        DataTable dtblMessages = new DataTable();
        dad.Fill(dtblMessages);
        foreach (DataRow row in dtblMessages.Rows)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.PopulateOnDemand = true;
                TreeView1.Nodes.Add(newNode);
            }
        }

        /// <summary>
        /// Get the child nodes of the expanded node
        /// </summary>
        protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
        string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional] WHERE IdPadre=@IdPadre";
        string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

        SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
        dad.SelectCommand.Parameters.AddWithValue("@IdPadre", e.Node.Value);
        DataTable dtblMessages = new DataTable();
        dad.Fill(dtblMessages);
        foreach (DataRow row in dtblMessages.Rows)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.PopulateOnDemand = true;
                e.Node.ChildNodes.Add(newNode);
            }
        }
    }
}