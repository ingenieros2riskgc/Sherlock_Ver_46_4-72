using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ListasSarlaft.Classes
{
    public class cParametrizacion : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;

        public DataTable cargarRegiones()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.Regiones.IdRegion, LTRIM(RTRIM(Parametrizacion.Regiones.NombreRegion)) AS NombreRegion, LTRIM(RTRIM(Listas.Usuarios.Usuario)) AS Usuario, CONVERT(DATETIME,Parametrizacion.Regiones.FechaRegistro, 107) AS FechaRegistro FROM Parametrizacion.Regiones INNER JOIN Listas.Usuarios ON Parametrizacion.Regiones.IdUsuario = Listas.Usuarios.IdUsuario");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        #region Jerarquia
        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        public DataTable GetTreeViewData()
        {
            // Get JerarquiaOrganizacional table
            //Heber Jessid Correal 09/04/2018 Se agrega el campo Estado en la consulta principal
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo, Estado FROM [Parametrizacion].[JerarquiaOrganizacional]";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        public DataTable GetTreeViewDataAudit()
        {
            // Get JerarquiaOrganizacional table
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo,TipoArea, Estado FROM [Parametrizacion].[JerarquiaOrganizacional]";
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
        public void AddTopTreeViewNodesAudit(DataTable treeViewData, TreeView treeview)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "TipoArea = 'A'";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.Expanded = true;
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                treeview.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        //Heber Jessid Correal 11/04/2018 Se modifica metodo para que reciba un Treeview como parametro
        public void AddTopTreeViewNodes(DataTable treeViewData, TreeView treeview)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                treeview.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        /// <summary>
        /// Recursively add child TreeView items by filtering by ParentID
        /// </summary>
        public void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                //Heber Jessid Correal 09/04/2018 se crea variable Treenode para poder deshabilitar e identificar el nodo que tenga estado 0 Desactivado
                TreeNode newNode = null;
                if (row["Estado"].ToString().Trim() == "0")
                {
                    newNode = new TreeNode("<font color='RED'><strong>" + string.Format("{0} (Nodo Desactivado)", row["NombreHijo"].ToString().Trim()) + "</strong></font>", row["IdHijo"].ToString());
                    newNode.SelectAction = TreeNodeSelectAction.None;
                }
                else
                {
                    newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                }
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }



        public string DetalleNodo(int tipoSelect, string idHijo)
        {
            string Detalle = "";
            string selectCommand = "";

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            if (tipoSelect == 1)
                selectCommand = "SELECT NombreResponsable,CorreoResponsable FROM [Parametrizacion].[DetalleJerarquiaOrg] WHERE idHijo = " + idHijo;
            else
                selectCommand = "SELECT NombreResponsable,CorreoResponsable, NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional] LEFT OUTER JOIN [Parametrizacion].[DetalleJerarquiaOrg] ON [DetalleJerarquiaOrg].idHijo = [JerarquiaOrganizacional].idHijo WHERE [JerarquiaOrganizacional].idHijo = " + idHijo;

            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                Detalle = "Responsable: " + row["NombreResponsable"].ToString() + "\r";
                Detalle = Detalle + "Correo: " + row["CorreoResponsable"].ToString().Trim();

                if (tipoSelect == 2)
                    Detalle = Detalle + "\r Nodo Jerarquía Org.: " + row["NombreHijo"].ToString().Trim();
            }

            if (Detalle == "")
                Detalle = "Responsable: \rCorreo:";

            return (Detalle);
        }

        #endregion Treeview

        #region GrupoTrabajo
        public DataTable GetTreeViewDataWorkGroup()
        {
            string selectCommand = "SELECT IdGrupoTrabajo,Nombre,CONVERT(VARCHAR,FechaRegistro,120)FechaRegistro FROM Riesgos.GruposTrabajo WHERE Estado = 1";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        public void AddTopTreeViewNodesWorkGroup(DataTable treeViewData, TreeView treeview)
        {
            DataView view = new DataView(treeViewData);
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["Nombre"].ToString(), row["IdGrupoTrabajo"].ToString());
                newNode.ToolTip = "Nombre Grupo: " + row["Nombre"].ToString() + "\rFechaRegistro: " + row["FechaRegistro"].ToString();
                treeview.Nodes.Add(newNode);
            }
        }

        #endregion
    }
}