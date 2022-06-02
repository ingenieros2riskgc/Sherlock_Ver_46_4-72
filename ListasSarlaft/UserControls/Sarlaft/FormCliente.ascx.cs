using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.IO;
using System.Data;
using System.Web.SessionState;

namespace ListasSarlaft.UserControls
{
    public partial class FormCliente : System.Web.UI.UserControl
    {
        string IdFormulario = "6007";
        private cKnowClient cKnowClient = new cKnowClient();
        cCuenta cCuenta = new cCuenta();

        #region Properties

        private int idConocimientoCliente;
        private int IdConocimientoCliente
        {
            get
            {
                idConocimientoCliente = (int)ViewState["idConocimientoCliente"];
                return idConocimientoCliente;
            }
            set
            {
                idConocimientoCliente = value;
                ViewState["idConocimientoCliente"] = idConocimientoCliente;
            }
        }

        private DataTable infoGridArchivos;
        private DataTable InfoGridArchivos
        {
            get
            {
                infoGridArchivos = (DataTable)ViewState["infoGridArchivos"];
                return infoGridArchivos;
            }
            set
            {
                infoGridArchivos = value;
                ViewState["infoGridArchivos"] = infoGridArchivos;
            }
        }

        private int rowGridArchivos;
        private int RowGridArchivos
        {
            get
            {
                rowGridArchivos = (int)ViewState["rowGridArchivos"];
                return rowGridArchivos;
            }
            set
            {
                rowGridArchivos = value;
                ViewState["rowGridArchivos"] = rowGridArchivos;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
                initValues();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue.ToString() == "OTRA")
            {
                Label6.Visible = true;
                TextBox2.Visible = true;
                TextBox2.Text = "";
            }
            else
            {
                Label6.Visible = false;
                TextBox2.Visible = false;
                TextBox2.Text = "";
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedValue.ToString() == "Otra")
            {
                Label9.Visible = true;
                TextBox3.Visible = true;
                TextBox3.Text = "";
            }
            else
            {
                Label9.Visible = false;
                TextBox3.Visible = false;
                TextBox3.Text = "";
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList3.SelectedValue.ToString() == "Otra")
            {
                Label11.Visible = true;
                TextBox4.Visible = true;
                TextBox4.Text = "";
            }
            else
            {
                Label11.Visible = false;
                TextBox4.Visible = false;
                TextBox4.Text = "";
            }
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList4.SelectedValue.ToString() == "Otra")
            {
                Label13.Visible = true;
                TextBox5.Visible = true;
                TextBox5.Text = "";
            }
            else
            {
                Label13.Visible = false;
                TextBox5.Visible = false;
                TextBox5.Text = "";
            }
        }

        protected void DropDownList12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList12.SelectedValue.ToString() == "OTRA")
            {
                Label69.Visible = true;
                TextBox52.Visible = true;
                TextBox52.Text = "";
            }
            else
            {
                Label69.Visible = false;
                TextBox52.Visible = false;
                TextBox52.Text = "";
            }
        }

        protected void DropDownList18_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList18.SelectedValue.ToString() == "SI")
            {
                Label95.Visible = true;
                DropDownList19.Visible = true;
                DropDownList19.SelectedIndex = 0;
                Label96.Visible = false;
                TextBox70.Visible = false;
            }
            else
            {
                Label95.Visible = false;
                DropDownList19.Visible = false;
                DropDownList19.SelectedIndex = 0;
                Label96.Visible = false;
                TextBox70.Visible = false;
            }
        }

        protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList19.SelectedValue.ToString() == "OTRA")
            {
                Label96.Visible = true;
                TextBox70.Visible = true;
                TextBox70.Text = "";
            }
            else
            {
                Label96.Visible = false;
                TextBox70.Visible = false;
                TextBox70.Text = "";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivos = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdf();
                    //descargarArchivo();
                    break;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                    cKnowClient.UpdateInfoFormDocsInu(IdConocimientoCliente, DropDownList23.SelectedValue.ToString().Trim(), DropDownList24.SelectedValue.ToString().Trim(), DropDownList25.SelectedValue.ToString().Trim(), DropDownList26.SelectedValue.ToString().Trim(), DropDownList27.SelectedValue.ToString().Trim(), DropDownList28.SelectedValue.ToString().Trim(), DropDownList29.SelectedValue.ToString().Trim(), DropDownList30.SelectedValue.ToString().Trim(), DropDownList39.SelectedValue.ToString().Trim(), DropDownList31.SelectedValue.ToString().Trim(), DropDownList32.SelectedValue.ToString().Trim(), DropDownList33.SelectedValue.ToString().Trim(), DropDownList34.SelectedValue.ToString().Trim(), DropDownList35.SelectedValue.ToString().Trim(), DropDownList36.SelectedValue.ToString().Trim(), DropDownList37.SelectedValue.ToString().Trim(), DropDownList38.SelectedValue.ToString().Trim(), DropDownList40.SelectedValue.ToString().Trim(), DropDownList41.SelectedValue.ToString().Trim(), DropDownList42.SelectedValue.ToString().Trim(), DropDownList43.SelectedValue.ToString().Trim(), DropDownList44.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar la información. " + ex.Message);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {
                            mtdCargarPdf();
                            //loadFile();
                            loadGridArchivos();
                            loadInfoArchivos();
                            Mensaje("Archivo cargado exitósamente.");
                        }
                        else
                            Mensaje("El archivo a cargar debe ser en formato PDF.");
                    }
                    else
                        Mensaje("No hay archivos para cargar.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al adjuntar el documento. " + ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    registrarFormulario();
                    Mensaje("Cliente registrado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar la información. " + ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string str;
            str = "window.open('ReporteKnowCliente.aspx?IdConocimientoCliente=" + IdConocimientoCliente + "','Reporte','width=950px,height=900px,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            initValues();
            resetValues();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            resetValues();
            enableCampos();
            initValues();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Button5.Visible = false;
            Button6.Visible = true;
            tbFormulario.Visible = false;
            tbDocumentos.Visible = true;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Button5.Visible = true;
            Button6.Visible = false;
            tbFormulario.Visible = true;
            tbDocumentos.Visible = false;
        }

        private void initValues()
        {
            IdConocimientoCliente = 0;
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            DropDownList1.SelectedIndex = 0;
            CboSexo.SelectedIndex = 0;
            Label6.Visible = false;
            TextBox2.Visible = false;
            TextBox2.Text = "";
            DropDownList2.SelectedIndex = 0;
            Label9.Visible = false;
            TextBox3.Visible = false;
            TextBox3.Text = "";
            DropDownList3.SelectedIndex = 0;
            Label11.Visible = false;
            TextBox4.Visible = false;
            TextBox4.Text = "";
            DropDownList4.SelectedIndex = 0;
            Label13.Visible = false;
            TextBox5.Visible = false;
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            DropDownList5.SelectedIndex = 0;
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            //TextBox14.Text = "";
            //TextBox15.Text = "";
            CboProfesion.SelectedIndex = 0;
            DropDownList6.SelectedIndex = 0;
            TextBox16.Text = "";
            TextBox17.Text = "";
            TextBox18.Text = "";
            TextBox19.Text = "";
            TextBox20.Text = "";
            TextBox21.Text = "";
            TextBox22.Text = "";
            TextBox23.Text = "";
            TextBox24.Text = "";
            TextBox25.Text = "";
            TextBox26.Text = "";
            TextBox27.Text = "";
            TextBox28.Text = "";
            DropDownList7.SelectedIndex = 0;
            DropDownList8.SelectedIndex = 0;
            DropDownList9.SelectedIndex = 0;
            TextBox29.Text = "";
            //TextBox30.Text = "";
            CboIngresos.SelectedIndex = 0;
            TextBox31.Text = "";
            //TextBox32.Text = "";
            CboEgresos.SelectedIndex = 0;
            TextBox33.Text = "";
            //TextBox34.Text = "";
            CboOtrosIngresos.SelectedIndex = 0;
            TextBox35.Text = "";
            TextBox36.Text = "";
            TextBox37.Text = "";
            TextBox38.Text = "";
            TextBox39.Text = "";
            TextBox40.Text = "";
            DropDownList10.SelectedIndex = 0;
            TextBox41.Text = "";
            TextBox43.Text = "";
            //TextBox42.Text = "";
            TextBox44.Text = "";
            TextBox45.Text = "";
            TextBox46.Text = "";
            TextBox47.Text = "";
            TextBox48.Text = "";
            TextBox49.Text = "";
            TextBox50.Text = "";
            TextBox51.Text = "";
            DropDownList11.SelectedIndex = 0;
            DropDownList12.SelectedIndex = 0;
            Label69.Visible = false;
            TextBox52.Visible = false;
            TextBox52.Text = "";
            TextBox53.Text = "";
            TextBox54.Text = "";
            TextBox55.Text = "";
            TextBox56.Text = "";
            TextBox57.Text = "";
            TextBox58.Text = "";
            TextBox59.Text = "";
            TextBox60.Text = "";
            TextBox61.Text = "";
            TextBox62.Text = "";
            TextBox63.Text = "";
            DropDownList13.SelectedIndex = 0;
            DropDownList14.SelectedIndex = 0;
            DropDownList15.SelectedIndex = 0;
            DropDownList16.SelectedIndex = 0;
            DropDownList17.SelectedIndex = 0;
            //TextBox64.Text = "";
            CboIngresosPJ.SelectedIndex = 0;
            TextBox65.Text = "";
            //TextBox66.Text = "";
            CboEgresosPJ.SelectedIndex = 0;
            TextBox67.Text = "";
            //TextBox68.Text = "";
            CboOtrosIngresosPJ.SelectedIndex = 0;
            TextBox69.Text = "";
            DropDownList18.SelectedIndex = 0;
            Label95.Visible = false;
            DropDownList19.Visible = false;
            DropDownList19.SelectedIndex = 0;
            Label96.Visible = false;
            TextBox70.Visible = false;
            TextBox70.Text = "";
            TextBox71.Text = "";
            TextBox72.Text = "";
            TextBox73.Text = "";
            TextBox74.Text = "";
            TextBox75.Text = "";
            TextBox76.Text = "";
            TextBox77.Text = "";
            TextBox78.Text = "";
            TextBox79.Text = "";
            TextBox80.Text = "";
            TextBox81.Text = "";
            TextBox82.Text = "";
            TextBox83.Text = "";
            TextBox84.Text = "";
            TextBox85.Text = "";
            TextBox86.Text = "";
            TextBox87.Text = "";
            TextBox88.Text = "";
            DropDownList20.SelectedIndex = 0;
            TextBox89.Text = "";
            TextBox90.Text = "";
            TextBox91.Text = "";
            TextBox92.Text = "";
            DropDownList21.SelectedIndex = 0;
            TextBox93.Text = "";
            TextBox94.Text = "";
            TextBox95.Text = "";
            TextBox96.Text = "";
            DropDownList22.SelectedIndex = 0;
            TextBox97.Text = "";
            TextBox98.Text = "";
            TextBox100.Text = "";
            TextBox101.Text = "";
            TextBox102.Text = "";
            TextBox104.Text = "";
            DropDownList23.SelectedIndex = 0;
            DropDownList24.SelectedIndex = 0;
            DropDownList25.SelectedIndex = 0;
            DropDownList26.SelectedIndex = 0;
            DropDownList27.SelectedIndex = 0;
            DropDownList28.SelectedIndex = 0;
            DropDownList29.SelectedIndex = 0;
            DropDownList30.SelectedIndex = 0;
            FileUpload1.Dispose();
            DropDownList39.SelectedIndex = 0;
            DropDownList31.SelectedIndex = 0;
            DropDownList32.SelectedIndex = 0;
            DropDownList33.SelectedIndex = 0;
            DropDownList34.SelectedIndex = 0;
            DropDownList35.SelectedIndex = 0;
            DropDownList36.SelectedIndex = 0;
            DropDownList37.SelectedIndex = 0;
            DropDownList38.SelectedIndex = 0;
            DropDownList40.SelectedIndex = 0;
            DropDownList41.SelectedIndex = 0;
            DropDownList42.SelectedIndex = 0;
            DropDownList43.SelectedIndex = 0;
            DropDownList44.SelectedIndex = 0;
            Button1.Visible = true;
            Button2.Visible = false;
            Button3.Visible = true;
            Button4.Visible = false;
            Button5.Visible = false;
            Button6.Visible = false;
            tbFormulario.Visible = true;
            tbDocumentos.Visible = false;
            CboSexoRepLegal.SelectedIndex = 0;
            TxtCorreoPrincipal.Text = "";
            TxtCorreoSucursal.Text = "";
        }

        private void disableCampos()
        {
            TextBox1.Enabled = false;
            DropDownList1.Enabled = false;
            TextBox2.Enabled = false;
            DropDownList2.Enabled = false;
            TextBox3.Enabled = false;
            DropDownList3.Enabled = false;
            TextBox4.Enabled = false;
            DropDownList4.Enabled = false;
            TextBox5.Enabled = false;
            TextBox6.Enabled = false;
            TextBox7.Enabled = false;
            TextBox8.Enabled = false;
            DropDownList5.Enabled = false;
            TextBox9.Enabled = false;
            TextBox10.Enabled = false;
            TextBox11.Enabled = false;
            TextBox12.Enabled = false;
            TextBox13.Enabled = false;
            //TextBox14.Enabled = false;
            //TextBox15.Enabled = false;
            CboProfesion.Enabled = false;
            DropDownList6.Enabled = false;
            TextBox16.Enabled = false;
            TextBox17.Enabled = false;
            TextBox18.Enabled = false;
            TextBox19.Enabled = false;
            TextBox20.Enabled = false;
            TextBox21.Enabled = false;
            TextBox22.Enabled = false;
            TextBox23.Enabled = false;
            TextBox24.Enabled = false;
            TextBox25.Enabled = false;
            TextBox26.Enabled = false;
            TextBox27.Enabled = false;
            TextBox28.Enabled = false;
            DropDownList7.Enabled = false;
            DropDownList8.Enabled = false;
            DropDownList9.Enabled = false;
            TextBox29.Enabled = false;
            //TextBox30.Enabled = false;
            CboIngresos.Enabled = false;
            TextBox31.Enabled = false;
            //TextBox32.Enabled = false;
            CboEgresos.Enabled = false;
            TextBox33.Enabled = false;
            //TextBox34.Enabled = false;
            CboOtrosIngresos.Enabled = false;
            TextBox35.Enabled = false;
            TextBox36.Enabled = false;
            TextBox37.Enabled = false;
            TextBox38.Enabled = false;
            TextBox39.Enabled = false;
            TextBox40.Enabled = false;
            DropDownList10.Enabled = false;
            TextBox41.Enabled = false;
            TextBox43.Enabled = false;
            //TextBox42.Enabled = false;
            TextBox44.Enabled = false;
            TextBox45.Enabled = false;
            TextBox46.Enabled = false;
            TextBox47.Enabled = false;
            TextBox48.Enabled = false;
            TextBox49.Enabled = false;
            TextBox50.Enabled = false;
            TextBox51.Enabled = false;
            DropDownList11.Enabled = false;
            DropDownList12.Enabled = false;
            TextBox52.Enabled = false;
            TextBox53.Enabled = false;
            TextBox54.Enabled = false;
            TextBox55.Enabled = false;
            TextBox56.Enabled = false;
            TextBox57.Enabled = false;
            TextBox58.Enabled = false;
            TextBox59.Enabled = false;
            TextBox60.Enabled = false;
            TextBox61.Enabled = false;
            TextBox62.Enabled = false;
            TextBox63.Enabled = false;
            DropDownList13.Enabled = false;
            DropDownList14.Enabled = false;
            DropDownList15.Enabled = false;
            DropDownList16.Enabled = false;
            DropDownList17.Enabled = false;
            //TextBox64.Enabled = false;
            CboIngresosPJ.Enabled = false;
            TextBox65.Enabled = false;
            //TextBox66.Enabled = false;
            CboEgresosPJ.Enabled = false;
            TextBox67.Enabled = false;
            //TextBox68.Enabled = false;
            CboOtrosIngresosPJ.Enabled = false;
            TextBox69.Enabled = false;
            DropDownList18.Enabled = false;
            DropDownList19.Enabled = false;
            TextBox70.Enabled = false;
            TextBox71.Enabled = false;
            TextBox72.Enabled = false;
            TextBox73.Enabled = false;
            TextBox74.Enabled = false;
            TextBox75.Enabled = false;
            TextBox76.Enabled = false;
            TextBox77.Enabled = false;
            TextBox78.Enabled = false;
            TextBox79.Enabled = false;
            TextBox80.Enabled = false;
            TextBox81.Enabled = false;
            TextBox82.Enabled = false;
            TextBox83.Enabled = false;
            TextBox84.Enabled = false;
            TextBox85.Enabled = false;
            TextBox86.Enabled = false;
            TextBox87.Enabled = false;
            TextBox88.Enabled = false;
            DropDownList20.Enabled = false;
            TextBox89.Enabled = false;
            TextBox90.Enabled = false;
            TextBox91.Enabled = false;
            TextBox92.Enabled = false;
            DropDownList21.Enabled = false;
            TextBox93.Enabled = false;
            TextBox94.Enabled = false;
            TextBox95.Enabled = false;
            TextBox96.Enabled = false;
            DropDownList22.Enabled = false;
            TextBox97.Enabled = false;
            TextBox98.Enabled = false;
            TextBox100.Enabled = false;
            TextBox101.Enabled = false;
            TextBox102.Enabled = false;
            TextBox104.Enabled = false;
            CboSexo.Enabled = false;
            TxtPNCorreoElectronico.Enabled = false;
            CboSexoRepLegal.Enabled = false;
            TxtCorreoPrincipal.Enabled = false;
            TxtCorreoSucursal.Enabled = false;
        }

        private void enableCampos()
        {
            TextBox1.Enabled = true;
            DropDownList1.Enabled = true;
            TextBox2.Enabled = true;
            DropDownList2.Enabled = true;
            TextBox3.Enabled = true;
            DropDownList3.Enabled = true;
            TextBox4.Enabled = true;
            DropDownList4.Enabled = true;
            TextBox5.Enabled = true;
            TextBox6.Enabled = true;
            TextBox7.Enabled = true;
            TextBox8.Enabled = true;
            DropDownList5.Enabled = true;
            TextBox9.Enabled = true;
            TextBox10.Enabled = true;
            TextBox11.Enabled = true;
            TextBox12.Enabled = true;
            TextBox13.Enabled = true;
            //TextBox14.Enabled = true;
            //TextBox15.Enabled = true;
            CboProfesion.Enabled = true;
            DropDownList6.Enabled = true;
            TextBox16.Enabled = true;
            TextBox17.Enabled = true;
            TextBox18.Enabled = true;
            TextBox19.Enabled = true;
            TextBox20.Enabled = true;
            TextBox21.Enabled = true;
            TextBox22.Enabled = true;
            TextBox23.Enabled = true;
            TextBox24.Enabled = true;
            TextBox25.Enabled = true;
            TextBox26.Enabled = true;
            TextBox27.Enabled = true;
            TextBox28.Enabled = true;
            DropDownList7.Enabled = true;
            DropDownList8.Enabled = true;
            DropDownList9.Enabled = true;
            TextBox29.Enabled = true;
            //TextBox30.Enabled = true;
            CboIngresos.Enabled = true;
            TextBox31.Enabled = true;
            //TextBox32.Enabled = true;
            CboEgresos.Enabled = true;
            TextBox33.Enabled = true;
            //TextBox34.Enabled = true;
            CboOtrosIngresos.Enabled = true;
            TextBox35.Enabled = true;
            TextBox36.Enabled = true;
            TextBox37.Enabled = true;
            TextBox38.Enabled = true;
            TextBox39.Enabled = true;
            TextBox40.Enabled = true;
            DropDownList10.Enabled = true;
            TextBox41.Enabled = true;
            TextBox43.Enabled = true;
            //TextBox42.Enabled = true;
            TextBox44.Enabled = true;
            TextBox45.Enabled = true;
            TextBox46.Enabled = true;
            TextBox47.Enabled = true;
            TextBox48.Enabled = true;
            TextBox49.Enabled = true;
            TextBox50.Enabled = true;
            TextBox51.Enabled = true;
            DropDownList11.Enabled = true;
            DropDownList12.Enabled = true;
            TextBox52.Enabled = true;
            TextBox53.Enabled = true;
            TextBox54.Enabled = true;
            TextBox55.Enabled = true;
            TextBox56.Enabled = true;
            TextBox57.Enabled = true;
            TextBox58.Enabled = true;
            TextBox59.Enabled = true;
            TextBox60.Enabled = true;
            TextBox61.Enabled = true;
            TextBox62.Enabled = true;
            TextBox63.Enabled = true;
            DropDownList13.Enabled = true;
            DropDownList14.Enabled = true;
            DropDownList15.Enabled = true;
            DropDownList16.Enabled = true;
            DropDownList17.Enabled = true;
            //TextBox64.Enabled = true;
            CboIngresosPJ.Enabled = true;
            TextBox65.Enabled = true;
            //TextBox66.Enabled = true;
            CboEgresosPJ.Enabled = true;
            TextBox67.Enabled = true;
            //TextBox68.Enabled = true;
            CboOtrosIngresosPJ.Enabled = true;
            TextBox69.Enabled = true;
            DropDownList18.Enabled = true;
            DropDownList19.Enabled = true;
            TextBox70.Enabled = true;
            TextBox71.Enabled = true;
            TextBox72.Enabled = true;
            TextBox73.Enabled = true;
            TextBox74.Enabled = true;
            TextBox75.Enabled = true;
            TextBox76.Enabled = true;
            TextBox77.Enabled = true;
            TextBox78.Enabled = true;
            TextBox79.Enabled = true;
            TextBox80.Enabled = true;
            TextBox81.Enabled = true;
            TextBox82.Enabled = true;
            TextBox83.Enabled = true;
            TextBox84.Enabled = true;
            TextBox85.Enabled = true;
            TextBox86.Enabled = true;
            TextBox87.Enabled = true;
            TextBox88.Enabled = true;
            DropDownList20.Enabled = true;
            TextBox89.Enabled = true;
            TextBox90.Enabled = true;
            TextBox91.Enabled = true;
            TextBox92.Enabled = true;
            DropDownList21.Enabled = true;
            TextBox93.Enabled = true;
            TextBox94.Enabled = true;
            TextBox95.Enabled = true;
            TextBox96.Enabled = true;
            DropDownList22.Enabled = true;
            TextBox97.Enabled = true;
            TextBox98.Enabled = true;
            TextBox100.Enabled = true;
            TextBox101.Enabled = true;
            TextBox102.Enabled = true;
            TextBox104.Enabled = true;
            CboSexo.Enabled = true;
            TxtPNCorreoElectronico.Enabled = true;
            CboSexoRepLegal.Enabled = true;
            TxtCorreoPrincipal.Enabled = true;
            TxtCorreoSucursal.Enabled = true;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void loadInfoArchivos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cKnowClient.loadInfoGridArchivos(IdConocimientoCliente.ToString());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                        });
                }
                GridView1.DataSource = InfoGridArchivos;
                GridView1.DataBind();
            }
        }

        private void loadFile()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;

            dtInfo = cKnowClient.loadCodigoInfoFormArchivo();

            if (dtInfo.Rows.Count > 0)
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + IdConocimientoCliente.ToString() + "-" + FileUpload1.FileName.ToString().Trim();
            else
                nameFile = "1-" + IdConocimientoCliente.ToString() + "-" + FileUpload1.FileName.ToString().Trim();

            FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFs/") + nameFile);
            cKnowClient.agregarArchivo(IdConocimientoCliente.ToString().Trim(), nameFile);
        }

        private void descargarArchivo()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFs/" + InfoGridArchivos.Rows[RowGridArchivos]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }

        private void registrarFormulario()
        {
            IdConocimientoCliente = cKnowClient.agregarConocimientoCliente(String.Format("{0:yyyy MM dd}", DateTime.Now).Replace(" ", ""), String.Format("{0:yyyy}", DateTime.Now));
            cKnowClient.InfoFormCliente(IdConocimientoCliente, TextBox1.Text.Trim(), DropDownList1.SelectedValue.ToString().Trim(), TextBox2.Text.Trim(), DropDownList2.SelectedValue.ToString().Trim(), TextBox3.Text.Trim(), DropDownList3.SelectedValue.ToString().Trim(), TextBox4.Text.Trim(), DropDownList4.SelectedValue.ToString().Trim(), TextBox5.Text.Trim());
            cKnowClient.InfoFormPN(IdConocimientoCliente, TextBox6.Text.Trim(), TextBox7.Text.Trim(), TextBox8.Text.Trim(), DropDownList5.SelectedValue.ToString().Trim(), TextBox9.Text.Trim(), TextBox10.Text.Trim(), TextBox11.Text.Trim(), TextBox12.Text.Trim(), TextBox13.Text.Trim(), DropDownList6.SelectedValue.ToString().Trim(), CboProfesion.SelectedValue.ToString().Trim(), TextBox16.Text.Trim(), TextBox17.Text.Trim(), TextBox18.Text.Trim(), TextBox19.Text.Trim(), TextBox20.Text.Trim(), TextBox21.Text.Trim(), TextBox22.Text.Trim(), TextBox23.Text.Trim(), TextBox24.Text.Trim(), TextBox25.Text.Trim(), TextBox26.Text.Trim(), TextBox27.Text.Trim(), TextBox28.Text.Trim(), DropDownList7.SelectedValue.ToString().Trim(), DropDownList8.SelectedValue.ToString().Trim(), DropDownList9.SelectedValue.ToString().Trim(), TextBox29.Text.Trim(), CboIngresos.SelectedValue.ToString().Trim(), TextBox31.Text.Trim(), CboEgresos.SelectedValue.ToString().Trim(), TextBox33.Text.Trim(), CboOtrosIngresos.SelectedValue.ToString().Trim(), TextBox35.Text.Trim(), CboSexo.SelectedValue.ToString().Trim(), TxtPNCorreoElectronico.Text.Trim());
            cKnowClient.InfoFormPJ(IdConocimientoCliente, TextBox36.Text.Trim(), TextBox37.Text.Trim(), TextBox38.Text.Trim(), TextBox39.Text.Trim(), TextBox40.Text.Trim(), DropDownList10.SelectedValue.ToString().Trim(), TextBox41.Text.Trim(), TextBox43.Text.Trim(), "", TextBox44.Text.Trim(), TextBox45.Text.Trim(), TextBox46.Text.Trim(), TextBox47.Text.Trim(), TextBox48.Text.Trim(), TextBox49.Text.Trim(), TextBox50.Text.Trim(), TextBox51.Text.Trim(), DropDownList11.SelectedValue.ToString().Trim(), DropDownList12.SelectedValue.ToString().Trim(), TextBox52.Text.Trim(), TextBox53.Text.Trim(), TextBox54.Text.Trim(), DropDownList13.SelectedValue.ToString().Trim(), TextBox56.Text.Trim(), TextBox55.Text.Trim(), DropDownList14.SelectedValue.ToString().Trim(), TextBox57.Text.Trim(), TextBox58.Text.Trim(), DropDownList15.SelectedValue.ToString().Trim(), TextBox59.Text.Trim(), TextBox60.Text.Trim(), DropDownList16.SelectedValue.ToString().Trim(), TextBox61.Text.Trim(), TextBox62.Text.Trim(), DropDownList17.SelectedValue.ToString().Trim(), TextBox63.Text.Trim(), CboIngresosPJ.SelectedValue.ToString().Trim(), TextBox65.Text.Trim(), CboEgresosPJ.SelectedValue.ToString().Trim(), TextBox67.Text.Trim(), CboOtrosIngresosPJ.SelectedValue.ToString().Trim(), TextBox69.Text.Trim(), CboSexoRepLegal.SelectedValue.ToString().Trim(), TxtCorreoPrincipal.Text.Trim(), TxtCorreoSucursal.Text.Trim());
            cKnowClient.InfoFormPF(IdConocimientoCliente, DropDownList18.SelectedValue.ToString().Trim(), DropDownList19.SelectedValue.ToString().Trim(), TextBox70.Text.Trim(), TextBox71.Text.Trim(), TextBox72.Text.Trim(), TextBox73.Text.Trim(), TextBox74.Text.Trim(), TextBox75.Text.Trim(), TextBox76.Text.Trim(), TextBox77.Text.Trim(), TextBox78.Text.Trim(), TextBox79.Text.Trim(), TextBox80.Text.Trim(), TextBox81.Text.Trim(), TextBox82.Text.Trim(), TextBox83.Text.Trim(), TextBox84.Text.Trim());
            cKnowClient.InfoFormSeguros(IdConocimientoCliente, TextBox85.Text.Trim(), TextBox86.Text.Trim(), TextBox87.Text.Trim(), TextBox88.Text.Trim(), DropDownList20.SelectedValue.ToString().Trim(), TextBox89.Text.Trim(), TextBox90.Text.Trim(), TextBox91.Text.Trim(), TextBox92.Text.Trim(), DropDownList21.SelectedValue.ToString().Trim(), TextBox93.Text.Trim());
            cKnowClient.InfoFormEntrevista(IdConocimientoCliente, TextBox94.Text.Trim(), TextBox95.Text.Trim(), TextBox96.Text.Trim(), DropDownList22.SelectedValue.ToString().Trim(), TextBox97.Text.Trim(), TextBox98.Text.Trim(), TextBox100.Text.Trim(), TextBox101.Text.Trim(), TextBox102.Text.Trim(), TextBox104.Text.Trim());
            cKnowClient.InfoFormDocsInu(IdConocimientoCliente, DropDownList23.SelectedValue.ToString().Trim(), DropDownList24.SelectedValue.ToString().Trim(), DropDownList25.SelectedValue.ToString().Trim(), DropDownList26.SelectedValue.ToString().Trim(), DropDownList27.SelectedValue.ToString().Trim(), DropDownList28.SelectedValue.ToString().Trim(), DropDownList29.SelectedValue.ToString().Trim(), DropDownList30.SelectedValue.ToString().Trim(), "", DropDownList39.SelectedValue.ToString().Trim(), DropDownList31.SelectedValue.ToString().Trim(), DropDownList32.SelectedValue.ToString().Trim(), DropDownList33.SelectedValue.ToString().Trim(), DropDownList34.SelectedValue.ToString().Trim(), DropDownList35.SelectedValue.ToString().Trim(), DropDownList36.SelectedValue.ToString().Trim(), DropDownList37.SelectedValue.ToString().Trim(), DropDownList38.SelectedValue.ToString().Trim(), DropDownList40.SelectedValue.ToString().Trim(), DropDownList41.SelectedValue.ToString().Trim(), DropDownList42.SelectedValue.ToString().Trim(), DropDownList43.SelectedValue.ToString().Trim(), DropDownList44.SelectedValue.ToString());
            Button5.Visible = true;
            Button6.Visible = false;
            Button1.Visible = false;
            Button2.Visible = true;
            Button3.Visible = false;
            Button4.Visible = true;
            tbFormulario.Visible = true;
            tbDocumentos.Visible = false;
            disableCampos();
            loadGridArchivos();
        }

        private void loadGridArchivos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivos = grid;
            GridView1.DataSource = InfoGridArchivos;
            GridView1.DataBind();
        }

        #region PDF
        private void mtdCargarPdf()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cKnowClient.loadCodigoInfoFormArchivo();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() +
                    "-" + IdConocimientoCliente.ToString() +
                    "-" + FileUpload1.FileName.ToString().Trim();
            else
                strNombreArchivo = "1-" + IdConocimientoCliente.ToString() +
                    "-" + FileUpload1.FileName.ToString().Trim();
            #endregion Nombre Archivo

            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);

            cKnowClient.mtdAgregarArchivoPdf(IdConocimientoCliente.ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdf()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivos.Rows[RowGridArchivos]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cKnowClient.mtdDescargarArchivoPdf(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }
        #endregion

    }
}