using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using System.IO;

namespace ListasSarlaft.UserControls
{
    public partial class ConsultarFormClienteZurich : System.Web.UI.UserControl
    {
        string IdFormulario = "6008";
        private cKnowClient cKnowClient = new cKnowClient();
        private cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
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

        private int idexRow;
        private int IdexRow
        {
            get
            {
                idexRow = (int)ViewState["idexRow"];
                return idexRow;
            }
            set
            {
                idexRow = value;
                ViewState["idexRow"] = idexRow;
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

        private int pagIndexInfoGridFormCliente;
        private int PagIndexInfoGridFormCliente
        {
            get
            {
                pagIndexInfoGridFormCliente = (int)ViewState["pagIndexInfoGridFormCliente"];
                return pagIndexInfoGridFormCliente;
            }
            set
            {
                pagIndexInfoGridFormCliente = value;
                ViewState["pagIndexInfoGridFormCliente"] = pagIndexInfoGridFormCliente;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                tbFormulario.Visible = false;
                inicializarValores();
                initValues();
                disableCampos();

                if ((Request.QueryString["ValueTipoPersona"] != null) && (Request.QueryString["ValueInusualidad"] != null) && (Request.QueryString["Estado"] != null))
                    if ((Request.QueryString["ValueTipoPersona"] != string.Empty) &&
                        (Request.QueryString["ValueInusualidad"] != string.Empty) &&
                        (Request.QueryString["Estado"] != string.Empty))
                        buscarClientForm(Request.QueryString["ValueTipoPersona"].ToString().Trim(), "", "", "", "", "", "");
            }
        }

        #region Buttons
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateClienteForm();

                    resetValues();
                    resetValuesConsulta();
                    disableCampos();
                    loadGrid();
                    Mensaje("Información actualizada con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la información. " + ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            imprimirFormulario();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            resetValues();
            disableCampos();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            try
            {
                if (mensaje.Trim() == string.Empty)
                {
                    inicializarValores();
                    buscarClientForm(DropDownList39.SelectedValue.ToString().Trim(),
                        TextBox99.Text.Trim(), TextBox103.Text.Trim(), TextBox105.Text.Trim(),
                        TextBox106.Text.Trim(), TextBox110.Text.Trim(), TextBox111.Text.Trim());
                }
                else
                    Mensaje(mensaje);
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            resetValuesConsulta();
            loadGrid();
            resetValues();
            disableCampos();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Convertexcel(InfoGrid, Response, "Export Clientes");
        }

        public static void Convertexcel(DataTable dt, HttpResponse Response, string filename)
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
                        if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {
                            mtdCargarPdfFormCliente();
                            loadGridArchivos();
                            loadInfoArchivos();
                        }
                        else
                            Mensaje("Archivo sin guardar. Solo archivos en formato .pdf");
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
        #endregion

        #region GridViews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridFormCliente) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Imprimir":
                    IdConocimientoCliente = Convert.ToInt32(InfoGrid.Rows[IdexRow]["IdConocimientoCliente"]);
                    imprimirFormulario();

                    break;
                case "Modificar":
                    IdConocimientoCliente = Convert.ToInt32(InfoGrid.Rows[IdexRow]["IdConocimientoCliente"]);
                    cargarInfo();

                    loadGridArchivos();
                    loadInfoArchivos();
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivos = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdfFormCliente();
                    break;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridFormCliente = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridFormCliente;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }
        #endregion

        protected void DropDownList39_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList39.SelectedValue.ToString() == "1")
            {
                TextBox110.Visible = true;
                TextBox111.Visible = true;
                Label195.Visible = true;
                Label196.Visible = true;
            }
            else
            {
                TextBox110.Visible = false;
                TextBox111.Visible = false;
                Label195.Visible = false;
                Label196.Visible = false;
            }
        }

        protected void ddlTipoFormulario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA NATURAL")
                mtdHabilitarTipoFormulario(1, true);

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA JURÍDICA")
                mtdHabilitarTipoFormulario(2, true);

            if (ddlTipoFormulario.SelectedValue.ToString() == "---")
                mtdHabilitarTipoFormulario(0, false);
        }

        private void inicializarValores()
        {
            PagIndexInfoGridFormCliente = 0;
        }

        private void initValues()
        {
            IdConocimientoCliente = 0;
            IdexRow = 0;
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            TextBox18.Text = "";
            TextBox20.Text = "";
            TextBox21.Text = "";
            TextBox22.Text = "";
            TextBox23.Text = "";
            TextBox24.Text = "";
            TextBox33.Text = "";
            TextBox31.Text = "";
            TextBox35.Text = "";
            TextBox36.Text = "";
            TextBox37.Text = "";
            TextBox38.Text = "";
            TextBox39.Text = "";
            TextBox40.Text = "";
            TextBox41.Text = "";
            TextBox43.Text = "";
            TextBox44.Text = "";
            TextBox45.Text = "";
            TextBox46.Text = "";
            TextBox47.Text = "";
            TextBox48.Text = "";
            TextBox49.Text = "";
            TextBox50.Text = "";
            TextBox51.Text = "";
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
            TextBox65.Text = "";
            TextBox67.Text = "";
            TextBox69.Text = "";
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
            TextBox89.Text = "";
            TextBox90.Text = "";
            TextBox91.Text = "";
            TextBox92.Text = "";
            TextBox93.Text = "";
            TextBox94.Text = "";
            TextBox95.Text = "";
            TextBox96.Text = "";
            TextBox97.Text = "";
            TextBox98.Text = "";
            TextBox102.Text = "";

            DropDownList21.SelectedIndex = 0;
            DropDownList20.SelectedIndex = 0;
            DropDownList22.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;
            DropDownList5.SelectedIndex = 0;
            DropDownList10.SelectedIndex = 0;
            DropDownList11.SelectedIndex = 0;
            DropDownList12.SelectedIndex = 0;
            DropDownList13.SelectedIndex = 0;
            DropDownList14.SelectedIndex = 0;
            DropDownList15.SelectedIndex = 0;
            DropDownList16.SelectedIndex = 0;
            DropDownList19.SelectedIndex = 0;
            DropDownList18.SelectedIndex = 0;

            FileUpload1.Dispose();

            Label6.Visible = false;
            Label69.Visible = false;
            Label95.Visible = false;
            Label96.Visible = false;
            TextBox52.Visible = false;
            TextBox70.Visible = false;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            Button7.Visible = false;
            DropDownList19.Visible = false;
            tbConsulta.Visible = true;
            tbFormulario.Visible = false;

            //TextBox2.Visible = false;
            //TextBox3.Visible = false;
            //TextBox4.Visible = false;
            //TextBox5.Visible = false;
            //TextBox2.Text = "";
            //TextBox3.Text = "";
            //TextBox4.Text = "";
            //TextBox5.Text = "";
            //CboProfesion.SelectedIndex = 0;
            //DropDownList6.SelectedIndex = 0;
            //TextBox16.Text = "";
            //TextBox17.Text = "";
            //TextBox19.Text = "";
            //TextBox42.Text = "";
            //TextBox62.Text = "";
            //TextBox63.Text = "";
            //TextBox25.Text = "";
            //TextBox26.Text = "";
            //TextBox27.Text = "";
            //TextBox28.Text = "";
            //DropDownList7.SelectedIndex = 0;
            //DropDownList8.SelectedIndex = 0;
            //DropDownList9.SelectedIndex = 0;
            //TextBox29.Text = "";
            //CboIngresos.SelectedIndex = 0;
            //CboEgresos.SelectedIndex = 0;
            //CboOtrosIngresos.SelectedIndex = 0;
            //DropDownList17.SelectedIndex = 0;
            //CboIngresosPJ.SelectedIndex = 0;
            //CboEgresosPJ.SelectedIndex = 0;
            //CboOtrosIngresosPJ.SelectedIndex = 0;
            //TextBox100.Text = "";
            //TextBox101.Text = "";   
            //TextBox104.Text = "";
            //DropDownList23.SelectedIndex = 0;
            //DropDownList24.SelectedIndex = 0;
            //DropDownList25.SelectedIndex = 0;
            //DropDownList26.SelectedIndex = 0;
            //DropDownList27.SelectedIndex = 0;
            //DropDownList28.SelectedIndex = 0;
            //DropDownList29.SelectedIndex = 0;
            //DropDownList30.SelectedIndex = 0;
            //DropDownList40.SelectedIndex = 0;
            //DropDownList31.SelectedIndex = 0;
            //DropDownList32.SelectedIndex = 0;
            //DropDownList33.SelectedIndex = 0;
            //DropDownList34.SelectedIndex = 0;
            //DropDownList35.SelectedIndex = 0;
            //DropDownList36.SelectedIndex = 0;
            //DropDownList37.SelectedIndex = 0;
            //CboSexoRepLegal.SelectedIndex = 0;
            //DropDownList43.SelectedIndex = 0;
            //DropDownList44.SelectedIndex = 0;
            //DropDownList45.SelectedIndex = 0;
            //DropDownList46.SelectedIndex = 0;
            //DropDownList47.SelectedIndex = 0;
            //DropDownList48.SelectedIndex = 0;
        }

        private void resetValuesConsulta()
        {
            DropDownList39.SelectedIndex = 0;

            TextBox99.Text = "";
            TextBox103.Text = "";
            TextBox105.Text = "";
            TextBox106.Text = "";
            TextBox110.Text = "";
            TextBox111.Text = "";

            TextBox110.Visible = false;
            TextBox111.Visible = false;
            Label195.Visible = false;
            Label196.Visible = false;

            //TextBox107.Text = "";
            //TextBox108.Text = "";
            //TextBox109.Text = "";
            //DropDownList38.SelectedIndex = 0;
            //DropDownList41.SelectedIndex = 0;
            //DropDownList42.SelectedIndex = 0;
            //CboSexoRepLegal.SelectedIndex = 0;
        }

        private void disableCampos()
        {
            TextBox1.Enabled = false;
            DropDownList1.Enabled = false;
            DropDownList2.Enabled = false;
            DropDownList3.Enabled = false;
            DropDownList4.Enabled = false;
            TextBox6.Enabled = false;
            TextBox7.Enabled = false;
            TextBox8.Enabled = false;
            DropDownList5.Enabled = false;
            TextBox9.Enabled = false;
            TextBox10.Enabled = false;
            TextBox11.Enabled = false;
            TextBox12.Enabled = false;
            TextBox13.Enabled = false;
            TextBox20.Enabled = false;
            TextBox21.Enabled = false;
            TextBox22.Enabled = false;
            TextBox23.Enabled = false;
            TextBox24.Enabled = false;
            TextBox31.Enabled = false;
            TextBox33.Enabled = false;
            TextBox35.Enabled = false;
            TextBox36.Enabled = false;
            TextBox37.Enabled = false;
            TextBox38.Enabled = false;
            TextBox39.Enabled = false;
            TextBox40.Enabled = false;
            DropDownList10.Enabled = false;
            TextBox41.Enabled = false;
            TextBox43.Enabled = false;
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
            DropDownList13.Enabled = false;
            DropDownList14.Enabled = false;
            DropDownList15.Enabled = false;
            DropDownList16.Enabled = false;
            TextBox67.Enabled = false;
            TextBox65.Enabled = false;
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
            TextBox102.Enabled = false;
            FileUpload1.Enabled = false;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;

        }

        private void enableCampos()
        {
            TextBox1.Enabled = true;
            DropDownList1.Enabled = true;
            DropDownList2.Enabled = true;
            DropDownList3.Enabled = true;
            DropDownList4.Enabled = true;
            TextBox6.Enabled = true;
            TextBox7.Enabled = true;
            TextBox8.Enabled = true;
            DropDownList5.Enabled = true;
            TextBox9.Enabled = true;
            TextBox10.Enabled = true;
            TextBox11.Enabled = true;
            TextBox12.Enabled = true;
            TextBox13.Enabled = true;
            TextBox18.Enabled = true;
            TextBox20.Enabled = true;
            TextBox21.Enabled = true;
            TextBox22.Enabled = true;
            TextBox23.Enabled = true;
            TextBox24.Enabled = true;
            TextBox33.Enabled = true;
            TextBox31.Enabled = true;
            TextBox35.Enabled = true;
            TextBox36.Enabled = true;
            TextBox37.Enabled = true;
            TextBox38.Enabled = true;
            TextBox39.Enabled = true;
            TextBox40.Enabled = true;
            DropDownList10.Enabled = true;
            TextBox41.Enabled = true;
            TextBox43.Enabled = true;
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
            DropDownList13.Enabled = true;
            DropDownList14.Enabled = true;
            DropDownList15.Enabled = true;
            DropDownList16.Enabled = true;
            TextBox69.Enabled = true;
            TextBox65.Enabled = true;
            TextBox67.Enabled = true;
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
            TextBox102.Enabled = true;
            TextBox98.Enabled = true;
            FileUpload1.Enabled = true;
            Button1.Visible = true;
            Button2.Visible = true;
            Button3.Visible = true;

        }

        private void Mensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void buscarClientForm(string NaOJu, string PrimerApellido, string SegundoApellido, string Nombre,
            string NumeroDocumento, string RazonSocial, string NIT)
        {
            DataTable dtInfo = new DataTable();

            loadGrid();
            resetValues();
            disableCampos();


            if (NaOJu == "0")
            {
                dtInfo = cKnowClient.buscarClientFormPN(PrimerApellido, SegundoApellido, Nombre, NumeroDocumento);
                GridView1.Columns[8].Visible = false;
                GridView1.Columns[9].Visible = false;
            }
            else
            {
                dtInfo = cKnowClient.buscarClientFormPJ(PrimerApellido, SegundoApellido, Nombre, NumeroDocumento, RazonSocial, NIT);

                GridView1.Columns[8].Visible = true;
                GridView1.Columns[9].Visible = true;
            }

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdConocimientoCliente"].ToString().Trim(),
                        dtInfo.Rows[rows]["PrimerApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["SegundoApellido"].ToString().Trim(),
                        dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NumeroDocumento"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ano"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["RazonDenominacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NIT"].ToString()                                                    
                        });
                }

                GridView1.PageIndex = PagIndexInfoGridFormCliente;
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
                Button7.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Button7.Visible = false;
                Mensaje("No se encontraron registros asociados a los parámetros de búsqueda.");
            }
        }

        private void cargarInfo()
        {
            DataTable dtInfo = new DataTable();

            enableCampos();

            #region Encabezado
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente, false);

            TextBox1.Text = dtInfo.Rows[0]["FechaFormulario"].ToString().Trim();
            tbxSucursal.Text = dtInfo.Rows[0]["Sucursal"].ToString().Trim();

            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["ClaseVinculacion"].ToString().Trim())
                    break;
            }

            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TomadorAsegurado"].ToString().Trim())
                    break;
            }

            for (int i = 0; i < DropDownList3.Items.Count; i++)
            {
                DropDownList3.SelectedIndex = i;
                if (DropDownList3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TomadorBeneficiario"].ToString().Trim())
                    break;
            }

            for (int i = 0; i < DropDownList4.Items.Count; i++)
            {
                DropDownList4.SelectedIndex = i;
                if (DropDownList4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["AseguradoBeneficiario"].ToString().Trim())
                    break;
            }
            #endregion

            if (DropDownList39.SelectedValue.ToString() == "0")
            {
                mtdHabilitarTipoFormulario(1, true);

                for (int i = 0; i < ddlTipoFormulario.Items.Count; i++)
                {
                    ddlTipoFormulario.SelectedIndex = i;
                    if (ddlTipoFormulario.SelectedItem.Text.Trim() == "PERSONA NATURAL")
                        break;
                }

                #region Persona Natural
                dtInfo = cKnowClient.InfoFormPN(IdConocimientoCliente);

                TextBox6.Text = dtInfo.Rows[0]["PNPrimerApellido"].ToString().Trim();
                TextBox7.Text = dtInfo.Rows[0]["PNSegunApellido"].ToString().Trim();

                string[] strPartes = dtInfo.Rows[0]["PNNombres"].ToString().Split(' ');
                TextBox8.Text = strPartes[0];
                tbxSdoNombrePN.Text = strPartes.Length == 1 ? string.Empty : strPartes[1];

                for (int i = 0; i < DropDownList5.Items.Count; i++)
                {
                    DropDownList5.SelectedIndex = i;
                    if (DropDownList5.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNTipoDocumento"].ToString().Trim())
                        break;
                }

                TextBox9.Text = dtInfo.Rows[0]["PNNumeroDocumento"].ToString().Trim();
                TextBox10.Text = dtInfo.Rows[0]["PNFechaExpedicion"].ToString().Trim();
                TextBox11.Text = dtInfo.Rows[0]["PNLugar"].ToString().Trim();

                for (int i = 0; i < CboSexo.Items.Count; i++)
                {
                    CboSexo.SelectedIndex = i;
                    if (CboSexo.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNSexo"].ToString().Trim())
                        break;
                }

                TextBox12.Text = dtInfo.Rows[0]["PNFechaNacimiento"].ToString().Trim();
                TextBox13.Text = dtInfo.Rows[0]["PNNacionalidad"].ToString().Trim();
                tbxNacionalidadPN2.Text = dtInfo.Rows[0]["PNNacionalidad2"].ToString().Trim();
                tbxProfesionPN.Text = dtInfo.Rows[0]["PNOcupacionOficio"].ToString().Trim();

                for (int i = 0; i < ddlEstadoCivil.Items.Count; i++)
                {
                    ddlEstadoCivil.SelectedIndex = i;
                    if (ddlEstadoCivil.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNEstadoCivil"].ToString().Trim())
                        break;
                }

                TextBox25.Text = dtInfo.Rows[0]["PNDireccionResidencia"].ToString().Trim();
                TextBox26.Text = dtInfo.Rows[0]["PNCiudad1"].ToString().Trim();

                TextBox27.Text = dtInfo.Rows[0]["PNTelefono1"].ToString().Trim();
                TextBox28.Text = dtInfo.Rows[0]["PNCelular"].ToString().Trim();
                tbxPNCorreoElectronico.Text = dtInfo.Rows[0]["PNCorreoElectronico"].ToString().Trim();

                for (int i = 0; i < ddlActivEconoPpal.Items.Count; i++)
                {
                    ddlActivEconoPpal.SelectedIndex = i;
                    if (ddlActivEconoPpal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNActividadEconomica"].ToString().Trim())
                        break;
                }
                tbxIndependiente.Text = dtInfo.Rows[0]["PNOtraActEconomica"].ToString().Trim();

                TextBox18.Text = dtInfo.Rows[0]["PNEmpresaTrabajo"].ToString().Trim();
                TextBox20.Text = dtInfo.Rows[0]["PNCargo"].ToString().Trim();

                TextBox21.Text = dtInfo.Rows[0]["PNCiudad2"].ToString().Trim();
                TextBox22.Text = dtInfo.Rows[0]["PNDireccion"].ToString().Trim();
                TextBox23.Text = dtInfo.Rows[0]["PNTelefono2"].ToString().Trim();
                TextBox24.Text = dtInfo.Rows[0]["PNFax"].ToString().Trim();

                tbxPNIngresosMen.Text = dtInfo.Rows[0]["PNIngresosMensuales"].ToString().Trim();
                TextBox31.Text = dtInfo.Rows[0]["PNActivos"].ToString().Trim();
                tbxPNEgresosMen.Text = dtInfo.Rows[0]["PNEgresoMensuales"].ToString().Trim();
                TextBox33.Text = dtInfo.Rows[0]["PNPasivos"].ToString().Trim();
                tbxPNOtrosIngresos.Text = dtInfo.Rows[0]["PNOtrosIngresos"].ToString().Trim();
                TextBox35.Text = dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString().Trim();


                for (int i = 0; i < ddlPNObligacionPais.Items.Count; i++)
                {
                    ddlPNObligacionPais.SelectedIndex = i;
                    if (ddlPNObligacionPais.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta1"].ToString().Trim())
                        break;
                }
                tbxPNObligacionPais.Text = dtInfo.Rows[0]["PNPregunta2"].ToString().Trim();
                #endregion
            }
            else
            {
                mtdHabilitarTipoFormulario(2, true);

                for (int i = 0; i < ddlTipoFormulario.Items.Count; i++)
                {
                    ddlTipoFormulario.SelectedIndex = i;
                    if (ddlTipoFormulario.SelectedItem.Text.Trim() == "PERSONA JURÍDICA")
                        break;
                }

                #region Persona Juridica
                dtInfo = cKnowClient.InfoFormPJ(IdConocimientoCliente);

                TextBox36.Text = dtInfo.Rows[0]["PJRazonDenominacion"].ToString().Trim();
                TextBox37.Text = dtInfo.Rows[0]["PJNIT"].ToString().Trim();

                for (int i = 0; i < ddlTipoSociedad.Items.Count; i++)
                {
                    ddlTipoSociedad.SelectedIndex = i;
                    if (ddlTipoSociedad.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoSociedad"].ToString().Trim())
                        break;
                }
                tbxTipoSociedad.Text = dtInfo.Rows[0]["PJOtroTipoSociedad"].ToString().Trim();

                TextBox38.Text = dtInfo.Rows[0]["PJPrimerApellido"].ToString().Trim();
                TextBox39.Text = dtInfo.Rows[0]["PJSegundoApellido"].ToString().Trim();
                string[] strPartes = dtInfo.Rows[0]["PJNombres"].ToString().Split(' ');
                TextBox40.Text = strPartes[0];
                tbxPJSdoNombre.Text = strPartes[1];

                for (int i = 0; i < DropDownList10.Items.Count; i++)
                {
                    DropDownList10.SelectedIndex = i;
                    if (DropDownList10.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocumento"].ToString().Trim())
                        break;
                }
                tbxPJOtroDoc.Text = dtInfo.Rows[0]["PJOtroTipoDoc"].ToString().Trim();
                TextBox41.Text = dtInfo.Rows[0]["PJNumeroDocumento"].ToString().Trim();
                TextBox43.Text = dtInfo.Rows[0]["PJLugarExpedicion"].ToString().Trim();
                tbxPJNacionalidad1.Text = dtInfo.Rows[0]["PJNacionalidad1"].ToString().Trim();
                tbxPJNacionalidad2.Text = dtInfo.Rows[0]["PJNacionalidad2"].ToString().Trim();

                TextBox44.Text = dtInfo.Rows[0]["PJDireccionOficina"].ToString().Trim();
                TextBox45.Text = dtInfo.Rows[0]["PJCiudad1"].ToString().Trim();
                TextBox46.Text = dtInfo.Rows[0]["PJTelefono1"].ToString().Trim();
                TextBox47.Text = dtInfo.Rows[0]["PJFax1"].ToString().Trim();

                tbxPJPagWeb.Text = dtInfo.Rows[0]["PJPagWeb"].ToString().Trim();
                tbxPJCorreoPrincipal.Text = dtInfo.Rows[0]["PJCorreoPrincipal"].ToString().Trim();

                TextBox48.Text = dtInfo.Rows[0]["PJDireccionSucursal"].ToString().Trim();
                TextBox49.Text = dtInfo.Rows[0]["PJCiudad2"].ToString().Trim();
                TextBox50.Text = dtInfo.Rows[0]["PJTelefono2"].ToString().Trim();
                TextBox51.Text = dtInfo.Rows[0]["PJFax2"].ToString().Trim();

                for (int i = 0; i < DropDownList11.Items.Count; i++)
                {
                    DropDownList11.SelectedIndex = i;
                    if (DropDownList11.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoEmpresa"].ToString().Trim())
                        break;
                }

                for (int i = 0; i < DropDownList12.Items.Count; i++)
                {
                    DropDownList12.SelectedIndex = i;
                    if (DropDownList12.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJActividadEconomica"].ToString().Trim())
                    {
                        break;
                    }
                }

                TextBox52.Text = dtInfo.Rows[0]["PJOtraActividadEconomica"].ToString().Trim();
                TextBox53.Text = dtInfo.Rows[0]["PJCIIU"].ToString().Trim();
                tbxPJDescObjSoc.Text = dtInfo.Rows[0]["PJDescObjSocial"].ToString().Trim();

                tbxPJConsecutivo1.Text = dtInfo.Rows[0]["PJConsecAS1"].ToString().Trim();
                for (int i = 0; i < DropDownList13.Items.Count; i++)
                {
                    DropDownList13.SelectedIndex = i;
                    if (DropDownList13.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS1"].ToString().Trim())
                        break;
                }
                TextBox56.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS1"].ToString().Trim();
                TextBox54.Text = dtInfo.Rows[0]["PJNombreAS1"].ToString().Trim();

                tbxPJConsecutivo2.Text = dtInfo.Rows[0]["PJConsecAS2"].ToString().Trim();
                for (int i = 0; i < DropDownList14.Items.Count; i++)
                {
                    DropDownList14.SelectedIndex = i;
                    if (DropDownList14.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS2"].ToString().Trim())
                        break;
                }
                TextBox57.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS2"].ToString().Trim();
                TextBox55.Text = dtInfo.Rows[0]["PJNombreAS2"].ToString().Trim();

                tbxPJConsecutivo3.Text = dtInfo.Rows[0]["PJConsecAS3"].ToString().Trim();
                for (int i = 0; i < DropDownList15.Items.Count; i++)
                {
                    DropDownList15.SelectedIndex = i;
                    if (DropDownList15.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS3"].ToString().Trim())
                        break;
                }
                TextBox59.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS3"].ToString().Trim();
                TextBox58.Text = dtInfo.Rows[0]["PJNombreAS3"].ToString().Trim();

                tbxPJConsecutivo4.Text = dtInfo.Rows[0]["PJConsecAS4"].ToString().Trim();
                for (int i = 0; i < DropDownList16.Items.Count; i++)
                {
                    DropDownList16.SelectedIndex = i;
                    if (DropDownList16.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS4"].ToString().Trim())
                        break;
                }
                TextBox61.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS4"].ToString().Trim();
                TextBox60.Text = dtInfo.Rows[0]["PJNombreAS4"].ToString().Trim();

                tbxPJIngresosMen.Text = dtInfo.Rows[0]["PJIngresosMensuales"].ToString().Trim();
                TextBox65.Text = dtInfo.Rows[0]["PJActivos"].ToString().Trim();
                tbxPJEgresosMen.Text = dtInfo.Rows[0]["PJEgresoMensuales"].ToString().Trim();
                TextBox67.Text = dtInfo.Rows[0]["PJPasivos"].ToString().Trim();
                tbxPJOtroIngresos.Text = dtInfo.Rows[0]["PJOtrosIngresos"].ToString().Trim();
                TextBox69.Text = dtInfo.Rows[0]["PJConceptoOtrosIngresos"].ToString().Trim();

                #endregion
            }

            #region Financiera
            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);

            for (int i = 0; i < DropDownList18.Items.Count; i++)
            {
                DropDownList18.SelectedIndex = i;
                if (DropDownList18.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim())
                    break;
            }

            for (int i = 0; i < DropDownList19.Items.Count; i++)
            {
                DropDownList19.SelectedIndex = i;
                if (DropDownList19.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TipoTransaccion"].ToString().Trim())
                    break;
            }

            TextBox70.Text = dtInfo.Rows[0]["OtroTipoTransaccion"].ToString().Trim();

            for (int i = 0; i < ddlPFProdExterior.Items.Count; i++)
            {
                ddlPFProdExterior.SelectedIndex = i;
                if (ddlPFProdExterior.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PFProdExterior"].ToString().Trim())
                    break;
            }

            TextBox71.Text = dtInfo.Rows[0]["PFTipoProducto1"].ToString().Trim();
            TextBox72.Text = dtInfo.Rows[0]["PFNumeroProducto1"].ToString().Trim();
            TextBox73.Text = dtInfo.Rows[0]["PFEntidad1"].ToString().Trim();
            TextBox74.Text = dtInfo.Rows[0]["PFMonto1"].ToString().Trim();
            TextBox75.Text = dtInfo.Rows[0]["PFCiudad1"].ToString().Trim();
            TextBox76.Text = dtInfo.Rows[0]["PFPais1"].ToString().Trim();
            TextBox77.Text = dtInfo.Rows[0]["PFMoneda1"].ToString().Trim();

            TextBox78.Text = dtInfo.Rows[0]["PFTipoProducto2"].ToString().Trim();
            TextBox79.Text = dtInfo.Rows[0]["PFNumeroProducto2"].ToString().Trim();
            TextBox80.Text = dtInfo.Rows[0]["PFEntidad2"].ToString().Trim();
            TextBox81.Text = dtInfo.Rows[0]["PFMonto2"].ToString().Trim();
            TextBox82.Text = dtInfo.Rows[0]["PFCiudad2"].ToString().Trim();
            TextBox83.Text = dtInfo.Rows[0]["PFPais2"].ToString().Trim();
            TextBox84.Text = dtInfo.Rows[0]["PFMoneda2"].ToString().Trim();

            TextBox78.Text = dtInfo.Rows[0]["PFTipoProducto2"].ToString().Trim();
            TextBox79.Text = dtInfo.Rows[0]["PFNumeroProducto2"].ToString().Trim();
            TextBox80.Text = dtInfo.Rows[0]["PFEntidad2"].ToString().Trim();
            TextBox81.Text = dtInfo.Rows[0]["PFMonto2"].ToString().Trim();
            TextBox82.Text = dtInfo.Rows[0]["PFCiudad2"].ToString().Trim();
            TextBox83.Text = dtInfo.Rows[0]["PFPais2"].ToString().Trim();
            TextBox84.Text = dtInfo.Rows[0]["PFMoneda2"].ToString().Trim();

            tbxPFTipoPdto2.Text = dtInfo.Rows[0]["PFTipoProducto3"].ToString().Trim();
            tbxPFIdPdto2.Text = dtInfo.Rows[0]["PFNumeroProducto3"].ToString().Trim();
            tbxPFEntidadPdto2.Text = dtInfo.Rows[0]["PFEntidad3"].ToString().Trim();
            tbxPFMontoPdto2.Text = dtInfo.Rows[0]["PFMonto3"].ToString().Trim();
            tbxPFCiudadPdto2.Text = dtInfo.Rows[0]["PFCiudad3"].ToString().Trim();
            tbxPFPaisPdto2.Text = dtInfo.Rows[0]["PFPais3"].ToString().Trim();
            tbxPFMonedaPdto2.Text = dtInfo.Rows[0]["PFMoneda3"].ToString().Trim();
            #endregion

            #region Seguros
            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);

            TextBox93.Text = dtInfo.Rows[0]["OrigenFondos"].ToString().Trim();

            TextBox85.Text = dtInfo.Rows[0]["SeguroAno1"].ToString().Trim();
            TextBox86.Text = dtInfo.Rows[0]["SeguroRamo1"].ToString().Trim();
            TextBox87.Text = dtInfo.Rows[0]["SeguroCompania1"].ToString().Trim();
            TextBox88.Text = dtInfo.Rows[0]["SeguroValor1"].ToString().Trim();
            for (int i = 0; i < DropDownList20.Items.Count; i++)
            {
                DropDownList20.SelectedIndex = i;
                if (DropDownList20.SelectedItem.Text.Trim() == dtInfo.Rows[0]["SeguroTipo1"].ToString().Trim())
                    break;
            }

            TextBox89.Text = dtInfo.Rows[0]["SeguroAno2"].ToString().Trim();
            TextBox90.Text = dtInfo.Rows[0]["SeguroRamo2"].ToString().Trim();
            TextBox91.Text = dtInfo.Rows[0]["SeguroCompania2"].ToString().Trim();
            TextBox92.Text = dtInfo.Rows[0]["SeguroValor2"].ToString().Trim();
            for (int i = 0; i < DropDownList21.Items.Count; i++)
            {
                DropDownList21.SelectedIndex = i;
                if (DropDownList21.SelectedItem.Text.Trim() == dtInfo.Rows[0]["SeguroTipo2"].ToString().Trim())
                    break;
            }
            #endregion

            #region Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);

            TextBox94.Text = dtInfo.Rows[0]["LugarEntrevista"].ToString().Trim();
            TextBox95.Text = dtInfo.Rows[0]["FechaEntrevista"].ToString().Trim();
            TextBox96.Text = dtInfo.Rows[0]["HoraEntrevista"].ToString().Trim();
            TextBox98.Text = dtInfo.Rows[0]["NombreResponsable"].ToString().Trim();

            for (int i = 0; i < DropDownList22.Items.Count; i++)
            {
                DropDownList22.SelectedIndex = i;
                if (DropDownList22.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Resultado"].ToString().Trim())
                    break;
            }

            tbxCCResponsableEntrevista.Text = dtInfo.Rows[0]["CCRespEntrevista"].ToString().Trim();
            TextBox97.Text = dtInfo.Rows[0]["Observaciones1"].ToString().Trim();

            TextBox102.Text = dtInfo.Rows[0]["NombreVerifica"].ToString().Trim();
            tbxCCResponsableVerificaEntrevista.Text = dtInfo.Rows[0]["CCVerificaEntrevista"].ToString().Trim();
            tbxObsResponsableVerificaEntrevista.Text = dtInfo.Rows[0]["Observaciones2"].ToString().Trim();
            #endregion

            tbConsulta.Visible = false;
            tbFormulario.Visible = true;
            tbDocumentos.Visible = true;
        }

        private void loadGridArchivos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivos = grid;
            GridView2.DataSource = InfoGridArchivos;
            GridView2.DataBind();
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
                GridView2.DataSource = InfoGridArchivos;
                GridView2.DataBind();
            }
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdConocimientoCliente", typeof(string));
            grid.Columns.Add("PrimerApellido", typeof(string));
            grid.Columns.Add("SegundoApellido", typeof(string));
            grid.Columns.Add("Nombres", typeof(string));
            grid.Columns.Add("TipoDocumento", typeof(string));
            grid.Columns.Add("NumeroDocumento", typeof(string));
            grid.Columns.Add("Ano", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("RazonDenominacion", typeof(string));
            grid.Columns.Add("NIT", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void imprimirFormulario()
        {
            string strPage = string.Empty, strIdForm = string.Empty;

            if (DropDownList39.SelectedValue.ToString() == "0")
                strIdForm = "0";
            else
                strIdForm = "1";

            strPage = string.Format("window.open('ReporteKnowClienteZurich.aspx?IdConocimientoCliente={0}" +
                "&IdTipoForm={1}','Reporte','width=950px,height=900px,scrollbars=yes,resizable=yes')", IdConocimientoCliente, strIdForm);

            Response.Write("<script languaje=javascript>" + strPage + "</script>");
        }

        private void updateClienteForm()
        {
            IdConocimientoCliente = cKnowClient.agregarConocimientoCliente(
                String.Format("{0:yyyy MM dd}", DateTime.Now).Replace(" ", ""), String.Format("{0:yyyy}", DateTime.Now),
                tbxSucursal.Text.Trim(), ddlTipoFormulario.SelectedValue.ToString());

            cKnowClient.InfoFormCliente(IdConocimientoCliente, TextBox1.Text.Trim(),
                DropDownList1.SelectedValue.ToString().Trim(), string.Empty,
                DropDownList2.SelectedValue.ToString().Trim(), string.Empty,
                DropDownList3.SelectedValue.ToString().Trim(), string.Empty,
                DropDownList4.SelectedValue.ToString().Trim(), string.Empty);

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA NATURAL")
                cKnowClient.InfoFormPN(IdConocimientoCliente, TextBox6.Text.Trim(), TextBox7.Text.Trim(), TextBox8.Text.Trim(), tbxSdoNombrePN.Text.Trim(),
                    DropDownList5.SelectedValue.ToString().Trim(), TextBox9.Text.Trim(), TextBox11.Text.Trim(), TextBox10.Text.Trim(), CboSexo.SelectedValue.ToString().Trim(),
                    TextBox12.Text.Trim(), TextBox13.Text.Trim(), tbxNacionalidadPN2.Text.Trim(), tbxProfesionPN.Text.Trim(), ddlEstadoCivil.SelectedValue.ToString().Trim(),
                    TextBox25.Text.Trim(), TextBox26.Text.Trim(), TextBox27.Text.Trim(), TextBox28.Text.Trim(), tbxPNCorreoElectronico.Text.Trim(),
                    ddlActivEconoPpal.SelectedValue.ToString().Trim(), tbxIndependiente.Text.Trim(),
                    TextBox18.Text.Trim(), TextBox20.Text.Trim(), TextBox21.Text.Trim(), TextBox22.Text.Trim(), TextBox23.Text.Trim(), TextBox24.Text.Trim(),
                    tbxPNIngresosMen.Text.Trim(), TextBox31.Text.Trim(), tbxPNEgresosMen.Text.Trim(), TextBox33.Text.Trim(), tbxPNOtrosIngresos.Text.Trim(), TextBox35.Text.Trim(),
                    ddlPNObligacionPais.SelectedValue.ToString().Trim(), tbxPNObligacionPais.Text.Trim());

            if (ddlTipoFormulario.SelectedValue.ToString() == "PERSONA JURÍDICA")
                cKnowClient.InfoFormPJ(IdConocimientoCliente, TextBox36.Text.Trim(), TextBox37.Text.Trim(),
                    ddlTipoSociedad.SelectedValue.ToString().Trim(), tbxTipoSociedad.Text.Trim(),
                    TextBox38.Text.Trim(), TextBox39.Text.Trim(), TextBox40.Text.Trim(), tbxPJSdoNombre.Text.Trim(),
                    DropDownList10.SelectedValue.ToString().Trim(), tbxPJOtroDoc.Text.Trim(), TextBox41.Text.Trim(), TextBox43.Text.Trim(),
                    tbxPJNacionalidad1.Text.Trim(), tbxPJNacionalidad2.Text.Trim(),
                    TextBox44.Text.Trim(), TextBox45.Text.Trim(), TextBox46.Text.Trim(), TextBox47.Text.Trim(),
                    tbxPJPagWeb.Text.Trim(), tbxPJCorreoPrincipal.Text.Trim(),
                    TextBox48.Text.Trim(), TextBox49.Text.Trim(), TextBox50.Text.Trim(), TextBox51.Text.Trim(),
                    DropDownList11.SelectedValue.ToString().Trim(), DropDownList12.SelectedValue.ToString().Trim(), TextBox52.Text.Trim(),
                    TextBox53.Text.Trim(), tbxPJDescObjSoc.Text.Trim(),
                    tbxPJConsecutivo1.Text.Trim(), TextBox54.Text.Trim(), DropDownList13.SelectedValue.ToString().Trim(), TextBox56.Text.Trim(),
                    tbxPJConsecutivo2.Text.Trim(), TextBox55.Text.Trim(), DropDownList14.SelectedValue.ToString().Trim(), TextBox57.Text.Trim(),
                    tbxPJConsecutivo3.Text.Trim(), TextBox58.Text.Trim(), DropDownList15.SelectedValue.ToString().Trim(), TextBox59.Text.Trim(),
                    tbxPJConsecutivo4.Text.Trim(), TextBox60.Text.Trim(), DropDownList16.SelectedValue.ToString().Trim(), TextBox61.Text.Trim(),
                    tbxPJIngresosMen.Text.Trim(), TextBox65.Text.Trim(),
                    tbxPJEgresosMen.Text.Trim(), TextBox67.Text.Trim(),
                    tbxPJOtroIngresos.Text.Trim(), TextBox69.Text.Trim());

            cKnowClient.InfoFormPF(IdConocimientoCliente, DropDownList18.SelectedValue.ToString().Trim(),
                DropDownList19.SelectedValue.ToString().Trim(), TextBox70.Text.Trim(),
                ddlPFProdExterior.SelectedValue.ToString().Trim(),
                TextBox71.Text.Trim(), TextBox72.Text.Trim(), TextBox73.Text.Trim(), TextBox74.Text.Trim(), TextBox75.Text.Trim(), TextBox76.Text.Trim(), TextBox77.Text.Trim(),
                TextBox78.Text.Trim(), TextBox79.Text.Trim(), TextBox80.Text.Trim(), TextBox81.Text.Trim(), TextBox82.Text.Trim(), TextBox83.Text.Trim(), TextBox84.Text.Trim(),
                tbxPFTipoPdto2.Text.Trim(), tbxPFIdPdto2.Text.Trim(), tbxPFEntidadPdto2.Text.Trim(), tbxPFMontoPdto2.Text.Trim(), tbxPFCiudadPdto2.Text.Trim(),
                tbxPFPaisPdto2.Text.Trim(), tbxPFMonedaPdto2.Text.Trim());

            cKnowClient.InfoFormSeguros(IdConocimientoCliente,
                TextBox85.Text.Trim(), TextBox86.Text.Trim(), TextBox87.Text.Trim(), TextBox88.Text.Trim(), DropDownList20.SelectedValue.ToString().Trim(),
                TextBox89.Text.Trim(), TextBox90.Text.Trim(), TextBox91.Text.Trim(), TextBox92.Text.Trim(), DropDownList21.SelectedValue.ToString().Trim(), TextBox93.Text.Trim());

            cKnowClient.InfoFormEntrevista(IdConocimientoCliente,
                TextBox94.Text.Trim(), TextBox95.Text.Trim(), TextBox96.Text.Trim(),
                TextBox98.Text.Trim(), DropDownList22.SelectedValue.ToString().Trim(),
                tbxCCResponsableEntrevista.Text.Trim(), TextBox97.Text.Trim(),
                TextBox102.Text.Trim(), tbxCCResponsableVerificaEntrevista.Text.Trim(),
                tbxObsResponsableVerificaEntrevista.Text.Trim(), false);
        }

        #region PDFs

        private void mtdCargarPdfFormCliente()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cKnowClient.loadCodigoInfoFormArchivo();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    IdConocimientoCliente.ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}",
                    IdConocimientoCliente.ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cKnowClient.mtdAgregarArchivoPdf(IdConocimientoCliente.ToString().Trim(),
                strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfFormCliente()
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

        #endregion PDFs

        private void mtdHabilitarTipoFormulario(int intTipoFormulario, bool booVisible)
        {
            switch (intTipoFormulario)
            {
                case 0:
                    infoTituloPN.Visible = booVisible;
                    infoPN.Visible = booVisible;
                    infoTituloPJ.Visible = booVisible;
                    infoPJ.Visible = booVisible;

                    #region
                    Label1.Text = "FORMULARIO DE CONOCIMIENTO DEL CLIENTE";
                    Label103.Text = "";
                    Label98.Text = "";
                    Label99.Text = "";
                    Label103.Text = "";
                    Label104.Text = "";
                    #endregion

                    #region
                    Label117.Text = "";
                    Label119.Text = "";
                    Label121.Text = "";
                    Label122.Text = "";
                    Label123.Text = "";
                    Label124.Text = "";
                    Label128.Text = "";
                    Label129.Text = "";
                    Label130.Text = "";
                    Label131.Text = "";
                    Label132.Text = "";
                    Label133.Text = "";
                    Label134.Text = "";
                    Label135.Text = "";
                    Label138.Text = "";
                    Label139.Text = "";
                    Label140.Text = "";
                    Label141.Text = "";
                    Label142.Text = "";
                    Label143.Text = "";
                    Label144.Text = "";
                    Label145.Text = "";
                    Label148.Text = "";
                    Label159.Text = "";
                    Label160.Text = "";
                    Label163.Text = "";
                    Label198.Text = "";
                    Label199.Text = "";
                    Label200.Text = "";
                    Label201.Text = "";
                    Label202.Text = "";
                    Label203.Text = "";
                    Label205.Text = "";
                    Label206.Text = "";
                    #endregion

                    #region
                    Label113.Text = "";
                    TextBox93.Text = "";
                    Label114.Text = "";
                    Label115.Text = "";
                    Label116.Text = "";
                    TextBox93.Visible = false;
                    #endregion

                    break;
                case 1: //Natural
                    infoTituloPN.Visible = booVisible;
                    infoPN.Visible = booVisible;
                    infoTituloPJ.Visible = !booVisible;
                    infoPJ.Visible = !booVisible;

                    Label1.Text = "FORMULARIO DE CONOCIMIENTO DEL CLIENTE - PERSONA NATURAL";
                    Label207.Text = "Nota: Espacio exclusivamente a ser diligenciado por la Aseguradora y/o Intermediario";

                    #region Documentos Minimos
                    Label98.Text = "1. Fotocopia del documento de identificación (cédula de ciudadanía de hologramas y/o contraseña certificada, tarjeta de identidad, cédula de extranjería, pasaporte o carné diplomático).";
                    Label99.Text = "Nota: La declaración de renta (si declara), será un documento que en cualquier momento podrá requerirse para validar o verificar información financiera.";
                    Label103.Text = "";
                    Label104.Text = "";
                    #endregion

                    #region Datos personales
                    Label117.Text = "Autorización Tratamiento de Datos Personales";
                    Label119.Text = "En cumplimiento a lo dispuesto en la ley 1581 de 2012  y las demás disposiciones que buscan la protección de datos personales y para efectos de acceder a la prestación de servicios por parte de Zurich Colombia Seguros S.A. (En adelante Zurich Seguros), que he suministrado datos personales para la finalidad y tratamiento descritos en la presente autorización. Así mismo, autorizo que durante la etapa precontractual y contractual,  Zurich pueda acceder a otras bases de datos para obtener información no suministrada en el presente documento, para los fines y tratamiento descrito a continuación:";
                    Label121.Text = "Declaro que Zurich Seguros me ha informado de manera expresa:";
                    Label122.Text = "1. Que los datos suministrados serán objeto de Tratamiento únicamente para los fines que se autorizan en esta autorización.";
                    Label123.Text = "2. Que mis datos personales serán tratados por Zurich Seguros,  para las siguientes finalidades:";
                    Label124.Text = "a. El trámite de vinculación como consumidor financiero.";
                    Label128.Text = "b. El proceso de negociación contractual, incluyendo pero no limitado a la determinación de primas y la selección de riesgos.";
                    Label129.Text = "c. Verificación del estado del riesgo que se pretende trasladar a  Zurich Seguros de manera previa a la suscripción de la póliza, durante la vigencia del contrato y ante el acaecimiento del siniestro, para comprobar las circunstancias bajo las cuales se presentó.";
                    Label130.Text = "d. La ejecución y el cumplimiento de los contratos que celebre con  Zurich Seguros.";
                    Label206.Text = "e. El control y la prevención del fraude.";
                    Label131.Text = "f. La liquidación y pago de siniestros.";
                    Label132.Text = "g. En general, la gestión integral del seguro contratado.";
                    Label133.Text = "h. La elaboración de estudios técnico-actuariales, estadísticas, encuestas,  análisis de tendencias del mercado.";
                    Label134.Text = "i. Envío de información y ofertas comerciales de Zurich Seguros.";
                    Label135.Text = "j. Realización de encuestas sobre satisfacción en los servicios prestados por Zurich Seguros, así como la verificación, referenciación y actualización de datos.";
                    Label138.Text = "k. Consulta, almacenamiento, administración, transferencia, procesamiento y reporte de información a las a las Centrales de Información o bases de datos debidamente constituidas referentes al comportamiento crediticio, financiero y comercial.";
                    Label139.Text = "l. Cuando aplique, para controlar el cumplimiento de requisitos para acceder al Sistema General de Seguridad Social Integral.";
                    Label140.Text = "m. Para las demás finalidades en cumplimiento de deberes legales y reportes regulatorios conforme a lo señalado en la ley.";
                    Label141.Text = "n. Para el envío de las modificaciones en la política de tratamiento de datos.";
                    Label142.Text = "3. Que, para efectos del cumplimiento de las finalidades indicadas en el numeral anterior, el tratamiento podrá realizarse aún en el caso de que no se llegare a formalizar una relación contractual con La Equidad, o que ella ya hubiere terminado y conforme al término de vigencia del tratamiento.";
                    Label143.Text = "4. Que los datos podrán ser compartidos, trasmitidos, entregados, transferidos o divulgados para las finalidades mencionadas, a:";
                    Label144.Text = "a. Las personas jurídicas que tienen la calidad de filiales, subsidiarias o vinculadas, o de matriz de Zurich Seguros.";
                    Label145.Text = "b. Los  operadores necesarios para el cumplimiento de derechos y obligaciones derivados de los contratos de seguro celebrados con Zurich Seguros, tales como, pero no limitados a: ajustadores, call centers, investigadores, compañías de asistencia, abogados externos, gestores de cartera, entre otros.";
                    Label148.Text = "c. Los intermediarios de seguros que intervengan en el proceso de celebración, ejecución y terminación del contrato de seguro.";
                    Label159.Text = "d. Las entidades jurídicas con las cuales Zurich Seguros adelante gestiones para efectos de Coaseguro o Reaseguro.";
                    Label160.Text = "e. FASECOLDA e INVERFAS S.A., personas jurídicas que administran bases de datos para efectos de prevención y control de fraudes, la selección de riesgos, y control de requisitos para acceder al Sistema General de Seguridad Social Integral y la selección de riesgos, así como la elaboración de estudios estadísticos actuariales.";
                    Label163.Text = "5. Que, para las finalidades indicadas en esta autorización, Zurich Seguros,  podrá consultar las bases de datos a que hace referencia el literal e) del numeral 4 de este documento.";
                    Label198.Text = "6. Que son facultativas las respuestas a las preguntas que me han hecho o me hagan sobre datos personales sensibles, de conformidad con la definición legal vigente. En consecuencia, no he sido obligado a responderlas.";
                    Label199.Text = "7. Que autorizo expresamente para que se lleve a cabo el tratamiento de mis datos sensibles, en especial, si la información suministrada es relativa a la salud y a los datos biométricos.";
                    Label200.Text = "8. Que son facultativas las respuestas a las preguntas sobre datos de niñas, niños y adolescentes. En consecuencia, no he sido obligado a responderlas.";
                    Label201.Text = "9. Que como titular de la información, me asisten los derechos previstos en la ley 1581 de 2012 y el decreto 1377 de 2013. En especial, me asiste el derecho a conocer, actualizar y rectificar las informaciones que hayan sido objeto de tratamiento.";
                    Label202.Text = "10. Que el responsable del tratamiento de la información es  Zurich Seguros,  cuya dirección es Calle 100 n. 7 - 33  Piso 5, el teléfono es 5188482.";
                    Label203.Text = "11. Que el responsable del tratamiento de los datos que se comparta, transfiera, trasmitan, entreguen o divulguen, en desarrollo de lo previsto en el literal e) del numeral 4 anterior, será FASECOLDA, cuya dirección es carrera Cra 7 No 26-20 Piso 11 y el teléfono es 3443080.";
                    Label205.Text = "12. Que con la suscripción del presente documento, autorizo el tratamiento de los datos personales, por las personas, para las finalidades y en los términos que me fueron informados en esta autorización.";
                    #endregion

                    #region Declaracion
                    Label113.Text = "1. Los recursos que poseo provienen de las siguientes fuentes (detalle ocupacion, oficio, actividad o negocio):";
                    TextBox93.Text = "";
                    Label114.Text = "2. Tanto mi actividad, profesión u oficio es lícita y la ejerzo dentro del marco legal y los recursos que poseo no provienen ni se destinan a actividades ilícitas de las contempladas en el Código Penal Colombiano.";
                    Label115.Text = "3. La información que he suministrado en la solicitud y en este documento es veraz y verificable y me obligo a actualizarla anualmente.";
                    Label116.Text = "4. Los recursos que se deriven del desarrollo de este contrato no se destinarás a la financiación del terrorismo, grupos terroristas o actividades terroristas.";
                    TextBox93.Visible = true;
                    #endregion

                    break;
                case 2://Juridico
                    infoTituloPJ.Visible = booVisible;
                    infoPJ.Visible = booVisible;
                    infoTituloPN.Visible = !booVisible;
                    infoPN.Visible = !booVisible;

                    Label1.Text = "FORMULARIO DE CONOCIMIENTO DEL CLIENTE - PERSONA JURÍDICA";
                    Label207.Text = "Nota: Espacio exclusivamente a ser diligenciado por la aseguradora y/o Broker";

                    #region Documentos Minimos
                    Label98.Text = "1. Fotocopia del documento de identificación del representante legal.";
                    Label99.Text = "2. Certificado de existencia y representación legal (Cámara de Comercio o certificación expedida por la entidad respectiva no superior a 3 meses de expedida).";
                    Label103.Text = "3. Copia del documento del registro Único Tributario (RUT)";
                    Label104.Text = "4. Copia de la Declaración de renta del último año gravable. Para las entidades exentas, según el artículo 598 del Estatuto Tributario, adjuntar Estados financieros.";
                    #endregion

                    #region Datos personales
                    Label117.Text = "AVISO DE PRIVACIDAD LEY DE PROTECCIÓN DE DATOS PERSONALES 1581 DE 2012";
                    Label119.Text = "La Zurich Colombia Seguros S.A., (En adelante Zurich Seguros) se permiten informar a todos sus Clientes y potenciales Clientes, que en cumplimiento de lo establecido en la Ley 1581 de 2012 y el decreto 1377 de 2013, los datos personales que le han sido, le son y le serán suministrados en virtud de la celebración y ejecución de los contratos de seguros que se han o se lleguen a celebrar, son objeto de protección y se someterán a lo establecido en la Política de Tratamiento de Datos que tiene implementada Zurich Seguros, en la cual constan nuestros deberes y los derechos del titular de los datos.";
                    Label121.Text = "En virtud de lo anterior, Zurich Seguros,  con domicilio principal en la Calle 100 N. 7 - 33 piso 5 de la ciudad de Bogotá D.C., teléfono en Bogotá 5188482; como RESPONSABLE DEL TRATAMIENTO DE LA INFORMACIÓN, se permite indicar que los datos de carácter personal de sus clientes (tomadores, asegurados y beneficiarios) y potenciales clientes serán objeto de Tratamiento según sea el caso para las siguientes finalidades:";
                    Label122.Text = "1. El trámite de vinculación como consumidor financiero.";
                    Label123.Text = "2. El proceso de negociación contractual, incluyendo pero no limitado a la determinación de primas y la selección de riesgos";
                    Label124.Text = "3. Verificación del estado del riesgo que se pretende trasladar a Zurich Seguros  de manera previa a la suscripción de la póliza, durante la vigencia del contrato y ante el acaecimiento del siniestro, para comprobar las circunstancias bajo las cuales se presentó.";
                    Label128.Text = "4. La ejecución y el cumplimiento de los contratos que celebre con  Zurich Seguros";
                    Label129.Text = "5. El control y la prevención del fraude.";
                    Label130.Text = "6. La liquidación y pago de siniestros.";
                    Label131.Text = "7. En general, la gestión integral del seguro contratado.";
                    Label132.Text = "8. La elaboración de estudios técnico-actuariales, estadísticas, encuestas, análisis de tendencias del mercado. 8. Envío de información y ofertas comerciales de Zurich Seguros.";
                    Label133.Text = "9. Realización de encuestas sobre satisfacción en los servicios prestados por Zurich Seguros, así como la verificación, referenciación y actualización de datos.";
                    Label134.Text = "10. Consulta, almacenamiento, administración, transferencia, procesamiento y reporte de información a las a las Centrales de Información o bases de datos debidamente constituidas referentes al comportamiento crediticio, financiero y comercial.";
                    Label135.Text = "11. Cuando aplique, para controlar el cumplimiento de requisitos para acceder al Sistema General de Seguridad Social Integral.";
                    Label138.Text = "12. Para las demás finalidades en cumplimiento de deberes legales y reportes regulatorios conforme a lo señalado en la ley.";
                    Label139.Text = "13. Para el envío de las modificaciones en la política de tratamiento de datos.";
                    Label140.Text = "";
                    Label141.Text = "";
                    Label142.Text = "";
                    Label143.Text = "";
                    Label144.Text = "";
                    Label145.Text = "";
                    Label148.Text = "";
                    Label159.Text = "";
                    Label160.Text = "";
                    Label163.Text = "";
                    Label198.Text = "";
                    Label199.Text = "";
                    Label200.Text = "";
                    Label201.Text = "";
                    Label202.Text = "";
                    Label203.Text = "";
                    Label205.Text = "";
                    #endregion

                    #region Declaracion
                    Label113.Text = "1. La actividad, profesión u oficio de la compañía es lícita y se ejerce dentro del marco legal y los recursos de la misma no provienen de actividades ilícitas de las contempladas en el Código Penal Colombiano.";
                    TextBox93.Text = "";
                    Label114.Text = "2. La información suministrada en la solicitud y en este documento es veraz y verificable y la sociedad se compromete a actualizarla anualmente.";
                    Label115.Text = "3. Los recursos que se deriven del desarrollo de este contrato no se destinaran a la financiación del terrorismo, grupos terroristas o actividades terroristas.";
                    Label116.Text = "4. Los recursos que posee la compañía provienen de la(s) actividades descritas anteriormente";
                    TextBox93.Visible = false;
                    #endregion

                    break;
            }

            Encabezado.Visible = booVisible;
            InfoClaseVinculacion.Visible = booVisible;
            infoNota2.Visible = booVisible;
            infoVinculos.Visible = booVisible;

            infoNota1.Visible = booVisible;

            infoTituloPF.Visible = booVisible;
            infoPF1.Visible = booVisible;

            infoDeclaracionFondos.Visible = booVisible;
            infoTituloSeguros.Visible = booVisible;
            infoSeguros1.Visible = booVisible;
            infoSeguros2.Visible = booVisible;

            infoAutorizacion.Visible = booVisible;

            infoTituloDocRequeridos.Visible = booVisible;
            infoDocRequeridos.Visible = booVisible;

            InfoFirmaHuella1.Visible = booVisible;
            InfoFirmaHuella2.Visible = booVisible;

            infoNota3.Visible = booVisible;

            infoEntrevista.Visible = booVisible;

        }

    }
}