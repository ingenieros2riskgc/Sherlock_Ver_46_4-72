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
    public partial class RpteTrazaSarlaft : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        private cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        
        string IdTipoRegistro = "0";
        string IdEstado = "0";
        
        String IdFormulario = "6009";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                LoadTipoRegistro();
                LoadEstadoOperacion();
            }
        }
        
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)Session["infGrid"];
                return infGrid;
            }
        }

        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            
            #region Tipo Registro
            IdTipoRegistro = "";
            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                IdTipoRegistro += ListBox2.Items[i].Value.ToString() + ",";
            }
            IdTipoRegistro += "0";
            LabelTipoRegistro.Text = IdTipoRegistro;
            #endregion

            #region Estado
            IdEstado = "";
            for (int i = 0; i < ListBox4.Items.Count; i++)
            {
                IdEstado += ListBox4.Items[i].Value.ToString() + ",";
            }
            IdEstado += "0";
            LabelEstadoOperacion.Text = IdEstado;
            #endregion

                TbTraza.Visible = true;
                loadGridReporteTraza();
                LoadInfoReporteTraza();
            
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            TbTraza.Visible = false;
        }

        protected void BtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                ListBox2.Items.Insert(i, new ListItem(ListBox1.Items[i].Text, ListBox1.Items[i].Value));
            }
            ListBox1.Items.Clear();
            ListBox2.Visible = true;
            ListBox1.Visible = false;
        }

        protected void BtnSelectOne_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem != null)
            {
                int Treg = ListBox2.Items.Count;
                if (ListBox1.SelectedIndex != -1)
                {
                    if (Treg == 0)
                        ListBox2.Items.Insert(0, new ListItem(ListBox1.SelectedItem.Text, ListBox1.SelectedItem.Value));
                    else
                        ListBox2.Items.Insert(Treg, new ListItem(ListBox1.SelectedItem.Text, ListBox1.SelectedItem.Value));

                    ListBox1.Items.Remove(ListBox1.SelectedItem);
                }
                ListBox2.ClearSelection();
                ListBox2.Visible = true;
            }
            if (ListBox1.Items.Count == 0)
                ListBox1.Visible = false;
        }

        protected void BtnUnSelectAll_Click(object sender, EventArgs e)
        {
            ListBox1.Visible = true;
            ListBox2.Visible = false;
            if (ListBox2.Items.Count > 0)
            {
                for (int i = 0; i < ListBox2.Items.Count; i++)
                {
                    ListBox2.SelectedIndex = i;
                    ListBox1.Items.Insert(i, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));
                }
                ListBox2.Items.Clear();
            }
        }

        protected void BtnUnSelectOne_Click(object sender, EventArgs e)
        {
            if (ListBox2.SelectedItem != null)
            {
                ListBox1.Visible = true;
                if (ListBox2.SelectedIndex != -1)
                {
                    int Treg = ListBox1.Items.Count;
                    if (Treg == 0)
                        ListBox1.Items.Insert(0, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));
                    else
                        ListBox1.Items.Insert(Treg, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));

                    ListBox2.Items.Remove(ListBox2.SelectedItem);
                }
                ListBox1.ClearSelection();
                if (ListBox2.Items.Count == 0)
                {
                    ListBox2.Visible = false;
                    ListBox2.Items.Clear();
                    ListBox1.Visible = true;
                }
            }
        }

        private void LoadTipoRegistro()
        {
            try
            {
                DataTable DtInfo = new DataTable();
                DtInfo = cRegistroOperacion.LoadListTipoRegistro();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox1.Items.Insert(i, new ListItem(DtInfo.Rows[i]["NombreTipoRegistro"].ToString().Trim(), DtInfo.Rows[i]["IdTipoRegistro"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Tipo Registros." + ex.Message);
            }
        }

        private void LoadEstadoOperacion()
        {
            try
            {
                DataTable DtInfo = new DataTable();
                DtInfo = cRegistroOperacion.LoadListEstado();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox3.Items.Insert(i, new ListItem(DtInfo.Rows[i]["NombreEstado"].ToString().Trim(), DtInfo.Rows[i]["IdEstadoOperacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Estado Operacion." + ex.Message);
            }
        }

        protected void BtnSelectAll_ClickEstado(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox3.Items.Count; i++)
            {
                ListBox4.Items.Insert(i, new ListItem(ListBox3.Items[i].Text, ListBox3.Items[i].Value));
            }
            ListBox3.Items.Clear();
            ListBox4.Visible = true;
            ListBox3.Visible = false;
        }

        protected void BtnSelectOne_ClickEstado(object sender, EventArgs e)
        {
            if (ListBox3.SelectedItem != null)
            {
                int Treg = ListBox4.Items.Count;
                if (ListBox3.SelectedIndex != -1)
                {
                    if (Treg == 0)
                        ListBox4.Items.Insert(0, new ListItem(ListBox3.SelectedItem.Text, ListBox3.SelectedItem.Value));
                    else
                        ListBox4.Items.Insert(Treg, new ListItem(ListBox3.SelectedItem.Text, ListBox3.SelectedItem.Value));

                    ListBox3.Items.Remove(ListBox3.SelectedItem);
                }
                ListBox4.ClearSelection();
                ListBox4.Visible = true;
            }
            if (ListBox3.Items.Count == 0)
                ListBox3.Visible = false;
        }

        protected void BtnUnSelectAll_ClickEstado(object sender, EventArgs e)
        {
            ListBox3.Visible = true;
            ListBox4.Visible = false;
            if (ListBox4.Items.Count > 0)
            {
                for (int i = 0; i < ListBox4.Items.Count; i++)
                {
                    ListBox4.SelectedIndex = i;
                    ListBox3.Items.Insert(i, new ListItem(ListBox4.SelectedItem.Text, ListBox4.SelectedItem.Value));
                }
                ListBox4.Items.Clear();
            }
        }

        protected void BtnUnSelectOne_ClickEstado(object sender, EventArgs e)
        {
            if (ListBox4.SelectedItem != null)
            {
                ListBox3.Visible = true;
                if (ListBox4.SelectedIndex != -1)
                {
                    int Treg = ListBox3.Items.Count;
                    if (Treg == 0)
                        ListBox3.Items.Insert(0, new ListItem(ListBox4.SelectedItem.Text, ListBox4.SelectedItem.Value));
                    else
                        ListBox3.Items.Insert(Treg, new ListItem(ListBox4.SelectedItem.Text, ListBox4.SelectedItem.Value));

                    ListBox4.Items.Remove(ListBox2.SelectedItem);
                }
                ListBox3.ClearSelection();
                if (ListBox4.Items.Count == 0)
                {
                    ListBox4.Visible = false;
                    ListBox4.Items.Clear();
                    ListBox3.Visible = true;
                }
            }
        }

                  
        private DataTable infoGridReporteTraza;
        private DataTable InfoGridReporteTraza
        {
            get
            {
                infoGridReporteTraza = (DataTable)ViewState["infoGridReporteTraza"];
                return infoGridReporteTraza;
            }
            set
            {
                infoGridReporteTraza = value;
                ViewState["infoGridReporteTraza"] = infoGridReporteTraza;
            }
        }

        private void loadGridReporteTraza()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Identificacion", typeof(string));
            grid.Columns.Add("NombreApellido", typeof(string));
            grid.Columns.Add("TipoRegistro", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            grid.Columns.Add("UsuarioAsignado", typeof(string));
            grid.Columns.Add("Indicador", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Mensaje", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("FechaDeteccion", typeof(string));
            grid.Columns.Add("FechaPosibleSolucion", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistroComentario", typeof(string));
            InfoGridReporteTraza = grid;
            GridView2.DataSource = InfoGridReporteTraza;
            GridView2.DataBind();
        }

        private void LoadInfoReporteTraza()
        {
            DataTable DtTraza = new DataTable();
            int Mes = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(TextBox2.Text)).Month;
            int Ano = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(TextBox2.Text)).Year;
            DtTraza = cRegistroOperacion.loadRteTraza(Convert.ToString(Mes),Convert.ToString(Ano),IdTipoRegistro,IdEstado);
            if (DtTraza.Rows.Count > 0)
            {
                for (int rows = 0; rows < DtTraza.Rows.Count; rows++)
                {
                    InfoGridReporteTraza.Rows.Add(new Object[] {
                            DtTraza.Rows[rows]["Identificacion"].ToString().Trim(),
                            DtTraza.Rows[rows]["NombreApellido"].ToString().Trim(),
                            DtTraza.Rows[rows]["TipoRegistro"].ToString().Trim(),
                            DtTraza.Rows[rows]["Estado"].ToString().Trim(),
                            DtTraza.Rows[rows]["UsuarioAsignado"].ToString().Trim(),
                            DtTraza.Rows[rows]["Indicador"].ToString().Trim(),
                            DtTraza.Rows[rows]["Descripcion"].ToString().Trim(),
                            DtTraza.Rows[rows]["Mensaje"].ToString().Trim(),
                            DtTraza.Rows[rows]["FechaRegistro"].ToString().Trim(),
                            DtTraza.Rows[rows]["FechaDeteccion"].ToString().Trim(),
                            DtTraza.Rows[rows]["FechaPosibleSolucion"].ToString().Trim(),
                            DtTraza.Rows[rows]["Comentario"].ToString().Trim(),
                            DtTraza.Rows[rows]["NombreUsuario"].ToString().Trim(),
                            DtTraza.Rows[rows]["FechaRegistroComentario"].ToString().Trim()
                    });
                }
                GridView2.DataSource = InfoGridReporteTraza;
                GridView2.DataBind();
            }
            else
            {
                TbTraza.Visible = false;
                Mensaje("No existe información para el periodo seleccionado");
            }
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

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(InfoGridReporteTraza, Response, "Reporte Trazabilidad Consolidado " + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }


    }
}