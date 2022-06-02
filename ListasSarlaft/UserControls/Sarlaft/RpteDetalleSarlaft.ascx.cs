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
    public partial class RpteDetalleSarlaft : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        private cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        private string[] strMonths = new string[12] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" },
        strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
        string IdTipoRegistro = "0";
        string IdEstado = "0";
        string CodigoArea = "0";
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
                loadDDLArea();
            }
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            
            #region Fechas Desde y Hasta
            if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim())))
            {
                Label8.Text = mtdConvertirFecha(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), 1) + " 00:00:00:000";
            }
            if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim())))
            {
                Label9.Text = mtdConvertirFecha(Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), 2) + " 23:59:59:998";
            }
            #endregion Fechas Desde y Hasta

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

            #region Area
            CodigoArea = "";
            for (int i = 0; i < ListBox6.Items.Count; i++)
            {
                CodigoArea += ListBox6.Items[i].Value.ToString() + ",";
            }
            CodigoArea += "0";
            LabelArea.Text = CodigoArea;
            #endregion

            ReportViewer2.LocalReport.Refresh();
           
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            ReportViewer2.LocalReport.Refresh();
        }

        

        private string mtdConvertirFecha(string strFechaIn, int intTipoDia)
        {
            string strFechaOut = string.Empty, strMes = string.Empty, strDia = string.Empty;
            string[] strFechaPartida = strFechaIn.Split('-');

            #region Asignar Mes
            for (int i = 0; i < 12; i++)
            {
                if (strFechaPartida[0].ToString().ToUpper() == strMeses[i].ToString() ||
                    strFechaPartida[0].ToString().ToUpper() == strMonths[i].ToString())
                {
                    if ((i + 1) <= 9)
                        strMes = string.Format("0{0}", (i + 1).ToString());
                    else
                        strMes = (i + 1).ToString().Trim();
                    break;
                }
            }
            #endregion Asignar Mes

            #region Asignar Dia
            switch (intTipoDia)
            {
                case 1:
                    strDia = "01";
                    break;
                case 2:
                    switch (strMes)
                    {
                        case "02":
                            strDia = "28";
                            break;
                        case "01":
                        case "03":
                        case "05":
                        case "07":
                        case "08":
                        case "10":
                        case "12":
                            strDia = "31";
                            break;
                        case "04":
                        case "06":
                        case "09":
                        case "11":
                            strDia = "30";
                            break;

                    }
                    break;
            }
            #endregion Asignar Dia

            if (!string.IsNullOrEmpty(strMes))
            {
                strFechaOut = string.Format("{0}-{1}-{2}", strFechaPartida[1].ToString().Trim(), strMes, strDia);
            }

            return strFechaOut;
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

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
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

                    ListBox4.Items.Remove(ListBox4.SelectedItem);
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

        private void loadDDLArea()
        {
            try
            {
                DataTable DtInfo = new DataTable();
                DtInfo = cRegistroOperacion.LoadCodigoAreas();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox5.Items.Insert(i, new ListItem(DtInfo.Rows[i]["NombreArea"].ToString().Trim(), DtInfo.Rows[i]["Codigo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Estado Operacion." + ex.Message);
            }
        }

        protected void BtnSelectAll_ClickArea(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox5.Items.Count; i++)
            {
                ListBox6.Items.Insert(i, new ListItem(ListBox5.Items[i].Text, ListBox5.Items[i].Value));
            }
            ListBox5.Items.Clear();
            ListBox6.Visible = true;
            ListBox5.Visible = false;
        }

        protected void BtnSelectOne_ClickArea(object sender, EventArgs e)
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
            }
            if (ListBox5.Items.Count == 0)
                ListBox5.Visible = false;
        }

        protected void BtnUnSelectAll_ClickArea(object sender, EventArgs e)
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
            }
        }

        protected void BtnUnSelectOne_ClickArea(object sender, EventArgs e)
        {
            if (ListBox6.SelectedItem != null)
            {
                ListBox5.Visible = true;
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
                if (ListBox6.Items.Count == 0)
                {
                    ListBox6.Visible = false;
                    ListBox6.Items.Clear();
                    ListBox5.Visible = true;
                }
            }
        }
    }
}