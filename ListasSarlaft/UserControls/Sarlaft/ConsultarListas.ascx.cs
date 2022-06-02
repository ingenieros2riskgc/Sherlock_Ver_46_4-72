using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls
{
    public partial class ConsultarListas : System.Web.UI.UserControl
    {
        private cListas cListas = new cListas();
        private cAuditoria cAuditoria = new cAuditoria();
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "45";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!IsPostBack)
            {
                loadDDLTipoListas();
                inicializarValores();
                loadGrid();
            }
        }        

        private void inicializarValores()
        {
            PagIndex = 0;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int row = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verReporte(row);
                    break;
            }
        }

        private void verReporte(int row)
        {
            string str;
            str = "window.open('Reporte.aspx?row=" + row + "','Reporte','width=800,height=600,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        private void resetValues()
        {
            DropDownList1.SelectedIndex = 0;
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            MensajeInformativo.Text = "";
            loadGrid();
        }

        private void loadDDLTipoListas()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cListas.TipoLista();
                for (int i = 1; i <= dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i - 1]["NombreClaseLista"].ToString().Trim(), dtInfo.Rows[i - 1]["CodigoClaseLisa"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los tipos de listas. " + ex.Message);
            }
            
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void MensajeInfo(String Mensaje)
        {
            MensajeInformativo.Text = Mensaje;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                inicializarValores();
                loadGrid();
                cargarInfoGrid();
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar la información. " + ex.Message);
            }            
        }

        private void cargarInfoGrid()
        {
            DataTable informacionListas = null;
            informacionListas = cListas.ConsultaListas(Convert.ToInt32(DropDownList1.SelectedItem.Value), Sanitizer.GetSafeHtmlFragment(TextBox1.Text), Sanitizer.GetSafeHtmlFragment(TextBox2.Text), Sanitizer.GetSafeHtmlFragment(TextBox3.Text));
            cAuditoria.auditoriaListas(Convert.ToInt32(DropDownList1.SelectedItem.Value), DropDownList1.SelectedItem.Text, Sanitizer.GetSafeHtmlFragment(TextBox1.Text), Sanitizer.GetSafeHtmlFragment(TextBox2.Text), Sanitizer.GetSafeHtmlFragment(TextBox3.Text), informacionListas.Rows.Count, String.Format("{0:yyyy MM dd}", DateTime.Now).Replace(" ", ""));
            if (informacionListas.Rows.Count >= 100)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Mensaje("La consulta devuelve mas de 100 registros.");
                MensajeInfo("");
            }
            else
            {                
                if (informacionListas.Rows.Count > 0)
                {
                    string strLink = string.Empty;
                    for (int rows = 0; rows < informacionListas.Rows.Count; rows++)
                    {
                        if (informacionListas.Rows[rows]["Link"].ToString().Trim() != "")
                        {
                            strLink = "<a href=" + "javascript:popUp('" + informacionListas.Rows[rows]["Link"].ToString() + "')" + ">" + informacionListas.Rows[rows]["Link"].ToString() + "</a>";
                        }
                        else
                        {
                            strLink = "";
                        }
                        InfoGrid.Rows.Add(new Object[] { informacionListas.Rows[rows]["IdLista"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["TipoDocumento"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["DocumentoIdentidad"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["NombreCompleto"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["CodigoClaseLisa"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["NombreClaseLista"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["FuenteConsulta"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["TipoPersona"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["Alias"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["Delito"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["Zona"].ToString().Trim(),
                                                         strLink,
                                                         informacionListas.Rows[rows]["Imagen"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["OtraInformacion"].ToString().Trim(),
                                                         informacionListas.Rows[rows]["Estado"].ToString()});
                    }
                    GridView1.PageIndex = PagIndex;
                    GridView1.DataSource = InfoGrid;
                    GridView1.DataBind();
                    MensajeInfo("");
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    MensajeInfo("No existen registros asociados a los parámetros de consulta.");
                }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            resetValues();
            inicializarValores();
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;            
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();            
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdLista", typeof(string));
            grid.Columns.Add("TipoDocumento", typeof(string));
            grid.Columns.Add("DocumentoIdentidad", typeof(string));
            grid.Columns.Add("NombreCompleto", typeof(string));
            grid.Columns.Add("CodigoClaseLisa", typeof(string));
            grid.Columns.Add("NombreClaseLista", typeof(string));
            grid.Columns.Add("FuenteConsulta", typeof(string));
            grid.Columns.Add("TipoPersona", typeof(string));
            grid.Columns.Add("Alias", typeof(string));
            grid.Columns.Add("Delito", typeof(string));
            grid.Columns.Add("Zona", typeof(string));
            grid.Columns.Add("Link", typeof(string));
            grid.Columns.Add("Imagen", typeof(string));
            grid.Columns.Add("OtraInformacion", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        #region Propierties

        private int pagIndex;
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

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)Session["infGrid"];
                return infGrid;
            }
            set
            {
                infGrid = value;
                Session["infGrid"] = infGrid;
            }
        }

        #endregion

    }
}