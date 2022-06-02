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
    public partial class Auditoria : System.Web.UI.UserControl
    {
        cAuditoria cAuditoria = new cAuditoria();
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "47";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if(!Page.IsPostBack)
            {
                inicializarValores();
                loadGrid();
            }            
        }

        private void inicializarValores()
        {
            PagIndex = 0;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            loadGrid();
            cargarInfoGrid();
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = null;
            String FechaDesde = "";
            if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()) != string.Empty)
            {
                FechaDesde = Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim().Replace(" ", ""));
            }
            String FechaHasta = "";
            if (Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) != string.Empty)
            {
                FechaHasta = Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim().Replace(" ", ""));
            }
            dtInfo = cAuditoria.consultarAuditoria(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.ToString().Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.ToString().Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.ToString().Trim()), FechaDesde, FechaHasta);
            if(dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++ )
                {
                    InfoGrid.Rows.Add(new Object[] { dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                                                     dtInfo.Rows[rows]["Apellidos"].ToString().Trim(),
                                                     dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                     dtInfo.Rows[rows]["FechaConsulta"].ToString().Trim(),
                                                     dtInfo.Rows[rows]["NombreClaseLista"].ToString().Trim(),
                                                     dtInfo.Rows[rows]["Documento"].ToString().Trim(),
                                                     dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                     dtInfo.Rows[rows]["Alias"].ToString().Trim(),
                                                     dtInfo.Rows[rows]["NumeroRegistros"].ToString().Trim()});
                }
                GridView1.PageIndex = PagIndex;
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();         
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();                    
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            resetValues();
            inicializarValores();
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Nombres", typeof(string));
            grid.Columns.Add("Apellidos", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaConsulta", typeof(string));
            grid.Columns.Add("NombreClaseLista", typeof(string));
            grid.Columns.Add("Documento", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Alias", typeof(string));
            grid.Columns.Add("NumeroRegistros", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            loadGrid();
        }

        #region Propierties

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)ViewState["informacionGrid"];
                return infGrid;
            }
            set
            {
                infGrid = value;
                ViewState["informacionGrid"] = infGrid;
            }
        }

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

        #endregion

    }
}