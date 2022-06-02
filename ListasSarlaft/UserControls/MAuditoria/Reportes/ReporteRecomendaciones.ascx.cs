using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.MAuditoria.Reportes
{
    public partial class ReporteRecomendaciones : System.Web.UI.UserControl
    {
        cAuditoria cAu = new cAuditoria();
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "3010";
        string IdPlaneaciones="0";
        string IdAuditorias="0";
        string IdProcesos="0";
        string IdHijos="0";
        string IdUsuarios = "0";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                LoadPlanesAccion();
                LoadUsuarios();
                PagIndexInfoGrid = 0;
            }
            
        }

       
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void LoadPlanesAccion()
        {
            try
            {
                DataTable DtInfo = new DataTable();
                DtInfo = cAu.LoadListPlaneacion();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox1.Items.Insert(i, new ListItem(DtInfo.Rows[i]["Nombre"].ToString().Trim(), DtInfo.Rows[i]["IdPlaneacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Planeación." + ex.Message);
            }
        }

        private void LoadAuditorias(string Condicion)
        {
            try
            {
                DataTable DtInfo = new DataTable();
                ListBox3.Items.Clear();
                TblAuditoria.Visible = false;
                DtInfo = cAu.LoadListAuditoria(Condicion);
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox3.Items.Insert(i, new ListItem(DtInfo.Rows[i]["Tema"].ToString().Trim(), DtInfo.Rows[i]["IdAuditoria"].ToString()));
                    TblAuditoria.Visible = true;
                    ListBox3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Planeación." + ex.Message);
            }
        }

        private void LoadMacroProcesos()
        {
            try
            {
                DataTable DtInfo = new DataTable();
                DtInfo = cAu.LoadListMacroProcesos();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox5.Items.Insert(i, new ListItem(DtInfo.Rows[i]["Nombre"].ToString().Trim(), DtInfo.Rows[i]["IdMacroProceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar MacroProceso." + ex.Message);
            }
        }

        private void LoadProcesos(string Condicion)
        {
            try
            {
                DataTable DtInfo = new DataTable();
                ListBox7.Items.Clear();
                TblProceso.Visible = false;
                DtInfo = cAu.LoadListProcesos(Condicion);
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox7.Items.Insert(i, new ListItem(DtInfo.Rows[i]["Nombre"].ToString().Trim(), DtInfo.Rows[i]["IdProceso"].ToString()));
                    TblProceso.Visible = true;
                    ListBox7.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Proceso." + ex.Message);
            }
        }

        private void LoadDependencias()
        {
            try
            {
                DataTable DtInfo = new DataTable();
                DtInfo = cAu.LoadListDependencias();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox9.Items.Insert(i, new ListItem(DtInfo.Rows[i]["NombreHijo"].ToString().Trim(), DtInfo.Rows[i]["idHijo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar MacroProceso." + ex.Message);
            }
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
            ListBox4.Items.Clear();
            ListBox4.Visible = false;
            VerIdPlanesAccion();
            TblUsuarios.Visible = true;
            TblDependenciaProceso.Visible = true;
            RadioButtonList1.ClearSelection();
            TblAuditoria.Visible = true;

            TblAuditoria.Visible = true;
            TblUsuarios.Visible = true;
            TblDependenciaProceso.Visible = true;
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
                ListBox4.Items.Clear();
                ListBox4.Visible = false;
                TblAuditoria.Visible = true;
                TblUsuarios.Visible = true;
                TblDependenciaProceso.Visible = true;
                VerIdPlanesAccion();
                
            }
            if (ListBox1.Items.Count == 0)
                ListBox1.Visible = false;
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
                    ListBox3.Items.Clear();
                    ListBox4.Items.Clear();
                    ListBox4.Visible = false;
                    TblAuditoria.Visible = false;
                    ListBox10.Items.Clear();
                    ListBox10.Visible = false;
                    ListBox9.Items.Clear();
                    ListBox8.Items.Clear();
                    ListBox8.Visible = false;
                    ListBox7.Items.Clear();
                    ListBox6.Items.Clear();
                    ListBox6.Visible = false;
                    ListBox5.Items.Clear();
                    ListBox5.Visible = false;
                    TblDependenciaProceso.Visible = false;
                    RadioButtonList1.ClearSelection();
                    TblDependencia.Visible = false;
                    TblProceso.Visible = false;
                    TblMacroproceso.Visible = false;

                    TblUsuarios.Visible = false;
                    TblReporte.Visible = false;
                    TblDependencia.Visible = false;
                    TblProceso.Visible = false;
                    TblMacroproceso.Visible = false;
                    TblDependenciaProceso.Visible = false;
                    RadioButtonList1.ClearSelection();
                    TblAuditoria.Visible = false;
                    ListBox2.Visible = false;
                    ListBox1.Visible = true;

                }
                ListBox4.Items.Clear();
                ListBox4.Visible = false;
                RadioButtonList1.ClearSelection();
                TblDependenciaProceso.Visible = false;
                VerIdPlanesAccion();
            }
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
                TblAuditoria.Visible = false;
                ListBox2.Items.Clear();
                ListBox3.Items.Clear();
                ListBox4.Items.Clear();
                ListBox4.Visible = false;
                ListBox10.Items.Clear();
                ListBox10.Visible = false;
                ListBox9.Items.Clear();
                ListBox8.Items.Clear();
                ListBox8.Visible = false;
                ListBox7.Items.Clear();
                ListBox6.Items.Clear();
                ListBox6.Visible = false;
                ListBox5.Items.Clear();
                ListBox5.Visible = false;
                RadioButtonList1.ClearSelection();
                TblDependenciaProceso.Visible = false;
                TblDependencia.Visible = false;
                TblProceso.Visible = false;
                TblMacroproceso.Visible = false;

                TblUsuarios.Visible = false;
                TblReporte.Visible = false;
                TblDependencia.Visible = false;
                TblProceso.Visible = false;
                TblMacroproceso.Visible = false;
                TblDependenciaProceso.Visible = false;
                RadioButtonList1.ClearSelection();
                TblAuditoria.Visible = false;
                ListBox2.Visible = false;
                ListBox1.Visible = true;
            }
        }

        private void VerIdPlanesAccion()
        {
            string Ids;
            Ids = "";
            IdPlaneaciones = "";
            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                Ids = Ids + ListBox2.Items[i].Value.ToString() + ",";
            }
            Ids = Ids + "0";
            LoadAuditorias(Ids);
        }

        private void VerIdProcesos()
        {
            string Ids;
            Ids = "";
            for (int i = 0; i < ListBox6.Items.Count; i++)
            {
                Ids = Ids + ListBox6.Items[i].Value.ToString() + ",";
            }
            Ids = Ids + "0";
            LoadProcesos(Ids);
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                TblDependencia.Visible = true;
                TblMacroproceso.Visible = false;
                TblProceso.Visible = false;
                ListBox5.Items.Clear();
                ListBox6.Items.Clear();
                ListBox7.Items.Clear();
                ListBox8.Items.Clear();
                ListBox9.Items.Clear();
                ListBox10.Items.Clear();
                LoadDependencias();
                 
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                ListBox5.Visible = true;
                TblMacroproceso.Visible = true;
                TblDependencia.Visible = false;
                ListBox5.Items.Clear();
                ListBox6.Items.Clear();
                ListBox6.Visible = false;
                ListBox7.Items.Clear();
                ListBox8.Items.Clear();
                ListBox8.Visible = false;
                ListBox9.Items.Clear();
                ListBox10.Items.Clear();
                ListBox10.Visible = false;
                LoadMacroProcesos();
            }
        }

        protected void AuBtnSelectOne_Click(object sender, EventArgs e)
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ListBox5.SelectedItem != null)
            {
                int Treg = ListBox6.Items.Count;
                if (ListBox5.SelectedIndex != -1)
                {
                    if (Treg == 0)
                        ListBox6.Items.Insert(0, new ListItem(ListBox5.SelectedItem.Text, ListBox5.SelectedItem.Value));
                    else
                        ListBox6.Items.Insert(Treg, new ListItem(ListBox5.SelectedItem.Text, ListBox5.SelectedItem.Value));

                    ListBox5.Items.Remove(ListBox5.SelectedItem);
                }
                ListBox6.ClearSelection();
                ListBox6.Visible = true;
                VerIdProcesos();
            }
            if (ListBox5.Items.Count == 0)
                ListBox5.Visible = false;
        }

        protected void AuBtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox3.Items.Count; i++)
            {
                ListBox4.Items.Insert(i, new ListItem(ListBox3.Items[i].Text, ListBox3.Items[i].Value));
            }
            ListBox3.Items.Clear();
            ListBox3.Visible=false;
            ListBox4.Visible = true;
        }

        protected void AuBtnUnSelectOne_Click(object sender, EventArgs e)
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

                    ListBox4.Items.Remove(ListBox4.SelectedItem);
                }
                ListBox3.ClearSelection();

                if (ListBox4.Items.Count == 0)
                {
                    ListBox4.Visible = false;
                }
            }
        }

        protected void AuBtnUnSelectAll_Click(object sender, EventArgs e)
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox5.Items.Count; i++)
            {
                ListBox6.Items.Insert(i, new ListItem(ListBox5.Items[i].Text, ListBox5.Items[i].Value));
            }
            ListBox5.Items.Clear();
            ListBox5.Visible = false;
            ListBox6.Visible = true;
            VerIdProcesos();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ListBox5.Visible = true;
            ListBox6.Visible = false;
            if (ListBox6.Items.Count > 0)
            {
                for (int i = 0; i < ListBox6.Items.Count; i++)
                {
                    ListBox6.SelectedIndex = i;
                    ListBox5.Items.Insert(i, new ListItem(ListBox6.SelectedItem.Text, ListBox6.SelectedItem.Value));
                }
                ListBox6.Items.Clear();
                TblProceso.Visible = false;
            }
            ListBox8.Items.Clear();
            ListBox8.Visible = false; 
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            ListBox5.Visible = true;
            if (ListBox6.SelectedItem != null)
            {
                if (ListBox6.SelectedIndex != -1)
                {
                    int Treg = ListBox5.Items.Count;
                    if (Treg == 0)
                        ListBox5.Items.Insert(0, new ListItem(ListBox6.SelectedItem.Text, ListBox6.SelectedItem.Value));
                    else
                        ListBox5.Items.Insert(Treg, new ListItem(ListBox6.SelectedItem.Text, ListBox6.SelectedItem.Value));

                    ListBox6.Items.Remove(ListBox6.SelectedItem);
                }
                ListBox5.ClearSelection();
                ListBox8.Items.Clear();
                ListBox8.Visible = false; 
                VerIdProcesos();
            }
            if (ListBox6.Items.Count == 0)
                ListBox6.Visible = false;
            
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (ListBox7.SelectedItem != null)
            {
                int Treg = ListBox8.Items.Count;
                if (ListBox7.SelectedIndex != -1)
                {
                    if (Treg == 0)
                        ListBox8.Items.Insert(0, new ListItem(ListBox7.SelectedItem.Text, ListBox7.SelectedItem.Value));
                    else
                        ListBox8.Items.Insert(Treg, new ListItem(ListBox7.SelectedItem.Text, ListBox7.SelectedItem.Value));

                    ListBox7.Items.Remove(ListBox7.SelectedItem);
                }
                ListBox8.Visible = true;
                ListBox8.ClearSelection();
            }
            if (ListBox7.Items.Count == 0)
                ListBox7.Visible = false;
                   
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            if (ListBox9.SelectedItem != null)
            {
                int Treg = ListBox10.Items.Count;
                if (ListBox9.SelectedIndex != -1)
                {
                    if (Treg == 0)
                        ListBox10.Items.Insert(0, new ListItem(ListBox9.SelectedItem.Text, ListBox9.SelectedItem.Value));
                    else
                        ListBox10.Items.Insert(Treg, new ListItem(ListBox9.SelectedItem.Text, ListBox9.SelectedItem.Value));

                    ListBox9.Items.Remove(ListBox9.SelectedItem);
                }
                ListBox10.ClearSelection();
                ListBox10.Visible = true;
            }
            if (ListBox9.Items.Count == 0)
                ListBox9.Visible = false;
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox9.Items.Count; i++)
            {
                ListBox10.Items.Insert(i, new ListItem(ListBox9.Items[i].Text, ListBox9.Items[i].Value));
            }
            ListBox9.Items.Clear();
            ListBox9.Visible = false;
            ListBox10.Visible = true;
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            ListBox9.Visible = true;
            if (ListBox10.SelectedItem != null)
            {
                if (ListBox10.SelectedIndex != -1)
                {
                    int Treg = ListBox9.Items.Count;
                    if (Treg == 0)
                        ListBox9.Items.Insert(0, new ListItem(ListBox10.SelectedItem.Text, ListBox10.SelectedItem.Value));
                    else
                        ListBox9.Items.Insert(Treg, new ListItem(ListBox10.SelectedItem.Text, ListBox10.SelectedItem.Value));

                    ListBox10.Items.Remove(ListBox10.SelectedItem);
                }
                ListBox9.ClearSelection();
            }
            if (ListBox10.Items.Count == 0)
                ListBox10.Visible = false;
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            ListBox9.Visible = true;
            ListBox10.Visible = false;
            if (ListBox10.Items.Count > 0)
            {
                for (int i = 0; i < ListBox10.Items.Count; i++)
                {
                    ListBox10.SelectedIndex = i;
                    ListBox9.Items.Insert(i, new ListItem(ListBox10.SelectedItem.Text, ListBox10.SelectedItem.Value));
                }
                ListBox10.Items.Clear();
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox7.Items.Count; i++)
            {
                ListBox8.Items.Insert(i, new ListItem(ListBox7.Items[i].Text, ListBox7.Items[i].Value));
            }
            ListBox7.Items.Clear();
            ListBox7.Visible = false;
            ListBox8.Visible = true;
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            ListBox7.Visible = true;
            if (ListBox8.SelectedItem != null)
            {
                if (ListBox8.SelectedIndex != -1)
                {
                    int Treg = ListBox7.Items.Count;
                    if (Treg == 0)
                        ListBox7.Items.Insert(0, new ListItem(ListBox8.SelectedItem.Text, ListBox8.SelectedItem.Value));
                    else
                        ListBox7.Items.Insert(Treg, new ListItem(ListBox8.SelectedItem.Text, ListBox8.SelectedItem.Value));

                    ListBox8.Items.Remove(ListBox8.SelectedItem);
                }
                ListBox7.ClearSelection();
            }
            if (ListBox8.Items.Count == 0)
                ListBox8.Visible = false;
         
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            ListBox7.Visible = true;
            ListBox8.Visible = false;
            if (ListBox8.Items.Count > 0)
            {
                for (int i = 0; i < ListBox8.Items.Count; i++)
                {
                    ListBox8.SelectedIndex = i;
                    ListBox7.Items.Insert(i, new ListItem(ListBox8.SelectedItem.Text, ListBox8.SelectedItem.Value));
                }
                ListBox8.Items.Clear();
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (ListBox2.Items.Count == 0)
            {
                Mensaje("Seleccione una Planeación");
            }
            else
            {
                loadGridReporteRiesgos();
                loadInfoReporteRiesgos();
                TblReporte.Visible = true;
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

        private void loadGridReporteRiesgos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Planeación", typeof(string));
            grid.Columns.Add("Id Auditoría", typeof(string));
            grid.Columns.Add("Nombre Auditoría", typeof(string));
            grid.Columns.Add("Programa / Estandar", typeof(string));
            grid.Columns.Add("Objetivo", typeof(string));
            grid.Columns.Add("Id Hallazgo", typeof(string));
            grid.Columns.Add("Estado de la Recomendación", typeof(string));
            grid.Columns.Add("Recomendación", typeof(string));
            grid.Columns.Add("Responsable / Gerencia", typeof(string));
            grid.Columns.Add("Estado de la Auditoría", typeof(string));
            grid.Columns.Add("Nombre de Usuario", typeof(string));
            InfoGrid = grid;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        private void loadInfoReporteRiesgos()
        {
            DataTable dtInfo = new DataTable();
            IdPlaneaciones = "";
            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                IdPlaneaciones = IdPlaneaciones + ListBox2.Items[i].Value.ToString() + ",";
            }
            IdPlaneaciones = IdPlaneaciones + "0";

            IdAuditorias = "";
            for (int i = 0; i < ListBox4.Items.Count; i++)
            {
                IdAuditorias = IdAuditorias + ListBox4.Items[i].Value.ToString() + ",";
            }
            IdAuditorias = IdAuditorias + "0";

            IdProcesos = "";
            for (int i = 0; i < ListBox8.Items.Count; i++)
            {
                IdProcesos = IdProcesos + ListBox8.Items[i].Value.ToString() + ",";
            }
            IdProcesos = IdProcesos + "0";

            IdHijos = "";
            for (int i = 0; i < ListBox10.Items.Count; i++)
            {
                IdHijos = IdHijos + ListBox10.Items[i].Value.ToString() + ",";
            }
            IdHijos = IdHijos + "0";

            IdUsuarios = "";
            for (int i = 0; i < ListBox12.Items.Count; i++)
            {
                IdUsuarios = IdUsuarios + ListBox12.Items[i].Value.ToString() + ",";
            }
            IdUsuarios = IdUsuarios + "0";

            dtInfo = cAu.LoadReporteRecomendaciones(IdPlaneaciones, IdAuditorias, IdProcesos, IdHijos, IdUsuarios);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {

                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["Planeación"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Id Auditoría"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Nombre Auditoría"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Programa / Estandar"].ToString().Trim(),                                                                  
                                                                  dtInfo.Rows[rows]["Objetivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Id Hallazgo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Estado de la Recomendación"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Recomendación"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Responsable / Gerencia"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Estado de la Auditoría"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Nombre de Usuario"].ToString().Trim()
                                                                 });
                }
                GridView1.PageIndex = PagIndexInfoGrid;
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
            }
            else
            {
                loadGridReporteRiesgos();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGrid = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGrid;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        #region Propierties

        private DataTable infoGrid;
        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

        private int pagIndexInfoGrid;
        private int PagIndexInfoGrid
        {
            get
            {
                pagIndexInfoGrid = (int)ViewState["pagIndexInfoGrid"];
                return pagIndexInfoGrid;
            }
            set
            {
                pagIndexInfoGrid = value;
                ViewState["pagIndexInfoGrid"] = pagIndexInfoGrid;
            }
        }
        
        #endregion        

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(InfoGrid, Response, "Reportes Recomendaciones " + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            TblReporte.Visible = false;
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            ListBox3.Items.Clear();
            ListBox4.Items.Clear();
            ListBox5.Items.Clear();
            ListBox6.Items.Clear();
            ListBox7.Items.Clear();
            ListBox8.Items.Clear();
            ListBox9.Items.Clear();
            ListBox10.Items.Clear();

            TblUsuarios.Visible = false;
            TblReporte.Visible = false;
            TblDependencia.Visible = false;
            TblProceso.Visible = false;
            TblMacroproceso.Visible = false;
            TblDependenciaProceso.Visible = false;
            RadioButtonList1.ClearSelection();
            TblAuditoria.Visible = false;
            ListBox2.Visible = false;
            ListBox1.Visible = true;
            
            LoadPlanesAccion();
        }
        //07
        private void LoadUsuarios()
        {
            try
            {
                DataTable DtInfo = new DataTable();
                DtInfo = cAu.LoadListUsuarios();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox11.Items.Insert(i, new ListItem(DtInfo.Rows[i]["Nombre"].ToString().Trim(), DtInfo.Rows[i]["IdUsuario"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar usuarios." + ex.Message);
            }
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox11.Items.Count; i++)
            {
                ListBox12.Items.Insert(i, new ListItem(ListBox11.Items[i].Text, ListBox11.Items[i].Value));
            }
            ListBox11.Items.Clear();
            ListBox11.Visible = false;
            ListBox12.Visible = true;
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            if (ListBox11.SelectedItem != null)
            {
                int Treg = ListBox12.Items.Count;
                if (ListBox11.SelectedIndex != -1)
                {
                    if (Treg == 0)
                        ListBox12.Items.Insert(0, new ListItem(ListBox11.SelectedItem.Text, ListBox11.SelectedItem.Value));
                    else
                        ListBox12.Items.Insert(Treg, new ListItem(ListBox11.SelectedItem.Text, ListBox11.SelectedItem.Value));

                    ListBox11.Items.Remove(ListBox11.SelectedItem);
                }
                ListBox12.ClearSelection();
                ListBox12.Visible = true;
            }
            if (ListBox11.Items.Count == 0)
                ListBox11.Visible = false;
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            ListBox11.Visible = true;
            ListBox12.Visible = false;
            if (ListBox12.Items.Count > 0)
            {
                for (int i = 0; i < ListBox12.Items.Count; i++)
                {
                    ListBox12.SelectedIndex = i;
                    ListBox11.Items.Insert(i, new ListItem(ListBox12.SelectedItem.Text, ListBox12.SelectedItem.Value));
                }
                ListBox12.Items.Clear();
            }
        }

        protected void Button16_Click(object sender, EventArgs e)
        {
            if (ListBox12.SelectedItem != null)
            {
                ListBox11.Visible = true;
                if (ListBox12.SelectedIndex != -1)
                {
                    int Treg = ListBox11.Items.Count;
                    if (Treg == 0)
                        ListBox11.Items.Insert(0, new ListItem(ListBox12.SelectedItem.Text, ListBox12.SelectedItem.Value));
                    else
                        ListBox11.Items.Insert(Treg, new ListItem(ListBox12.SelectedItem.Text, ListBox12.SelectedItem.Value));

                    ListBox12.Items.Remove(ListBox12.SelectedItem);
                }
                ListBox11.ClearSelection();

                if (ListBox12.Items.Count == 0)
                {
                    ListBox4.Visible = false;
                }
            }
        }
    }
}