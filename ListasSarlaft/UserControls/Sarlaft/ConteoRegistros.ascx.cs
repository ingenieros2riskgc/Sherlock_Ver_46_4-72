using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Sarlaft
{
    public partial class ConteoRegistros : System.Web.UI.UserControl
    {
        cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "44";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                inicializarValores();
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGridConteoRegistros = 0;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                inicializarValores();
                loadGridConteoRegistros();
                loadInfoConteoRegistros();
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }
        
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            resetValues();
            loadGridConteoRegistros();
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            Button3.Visible = false;
        }

        private void loadGridConteoRegistros()
        { 
            DataTable grid = new DataTable();
            grid.Columns.Add("IdConteo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("RegistrosCargue", typeof(string));
            grid.Columns.Add("RegistrosOperacion", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGridConteoRegistros = grid;
            GridView1.DataSource = InfoGridConteoRegistros;
            GridView1.DataBind();
        }

        private void loadInfoConteoRegistros()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRegistroOperacion.loadInfoConteoRegistros(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()) + " 00:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()) + " 23:59:59:998");
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridConteoRegistros.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdConteo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["RegistrosCargue"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["RegistrosOperacion"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["Descripcion"].ToString().Trim()
                                                                  });
                }
                GridView1.PageIndex = PagIndexInfoGridConteoRegistros;
                GridView1.DataSource = InfoGridConteoRegistros;
                GridView1.DataBind();
                Button3.Visible = true;
            }
            else
            {
                Button3.Visible = false;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridConteoRegistros, Response, "Conteo Registros");
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
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
            Response.End();            
        }

        #region Propierties
        private DataTable infoGridConteoRegistros;
        private DataTable InfoGridConteoRegistros
        {
            get
            {
                infoGridConteoRegistros = (DataTable)ViewState["infoGridConteoRegistros"];
                return infoGridConteoRegistros;
            }
            set
            {
                infoGridConteoRegistros = value;
                ViewState["infoGridConteoRegistros"] = infoGridConteoRegistros;
            }
        }

        private int pagIndexInfoGridConteoRegistros;
        private int PagIndexInfoGridConteoRegistros
        {
            get
            {
                pagIndexInfoGridConteoRegistros = (int)ViewState["pagIndexInfoGridConteoRegistros"];
                return pagIndexInfoGridConteoRegistros;
            }
            set
            {
                pagIndexInfoGridConteoRegistros = value;
                ViewState["pagIndexInfoGridConteoRegistros"] = pagIndexInfoGridConteoRegistros;
            }
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridConteoRegistros = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridConteoRegistros;
            GridView1.DataSource = InfoGridConteoRegistros;
            GridView1.DataBind();
        }
       
    }
}