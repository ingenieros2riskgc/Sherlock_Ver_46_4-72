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
    public partial class ConsultarFormCliente : System.Web.UI.UserControl
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
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                inicializarValores();
                initValues();
                disableCampos();
                if ((Request.QueryString["ValueTipoPersona"] != null) && (Request.QueryString["ValueInusualidad"] != null) && (Request.QueryString["Estado"] != null))
                {
                    if ((Request.QueryString["ValueTipoPersona"] != string.Empty) && (Request.QueryString["ValueInusualidad"] != string.Empty) && (Request.QueryString["Estado"] != string.Empty))
                    {
                        buscarClientForm(Request.QueryString["ValueTipoPersona"].ToString().Trim(), "", "", "", "", "", "", "", "", "", "SI", Request.QueryString["ValueInusualidad"].ToString().Trim(), Request.QueryString["Estado"].ToString().Trim());
                    }
                }
            }
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
                if (TextBox107.Text.Trim() != "")
                {
                    int number;
                    bool result = Int32.TryParse(TextBox107.Text.Trim(), out number);

                    if (!result)
                        mensaje = "Campo Año no valido. ";
                }

                if (TextBox108.Text.Trim() != "")
                {
                    try
                    {
                        System.DateTime.Parse(TextBox108.Text.Trim());
                    }
                    catch
                    {
                        mensaje = mensaje + "Fecha desde no valida. ";
                    }
                }

                if (TextBox109.Text.Trim() != "")
                {
                    try
                    {
                        System.DateTime.Parse(TextBox109.Text.Trim());
                    }
                    catch
                    {
                        mensaje = mensaje + "Fecha hasta no valida.";
                    }
                }

                if (mensaje.Trim() == string.Empty)
                {
                    inicializarValores();
                    buscarClientForm(DropDownList39.SelectedValue.ToString().Trim(), TextBox99.Text.Trim(), TextBox103.Text.Trim(), TextBox105.Text.Trim(), TextBox106.Text.Trim(), TextBox107.Text.Trim(), TextBox108.Text.Trim().Replace(" ", ""), TextBox109.Text.Trim().Replace(" ", ""), TextBox110.Text.Trim(), TextBox111.Text.Trim(), DropDownList38.SelectedValue.ToString().Trim(), DropDownList41.SelectedValue.ToString().Trim(), DropDownList42.SelectedValue.ToString().Trim());
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

        protected void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if ((DropDownList40.SelectedValue.ToString().Trim() == "NO") && (DropDownList31.SelectedValue.ToString().Trim() == "NO") && (DropDownList32.SelectedValue.ToString().Trim() == "NO") && (DropDownList33.SelectedValue.ToString().Trim() == "NO") && (DropDownList34.SelectedValue.ToString().Trim() == "NO") && (DropDownList35.SelectedValue.ToString().Trim() == "NO") && (DropDownList36.SelectedValue.ToString().Trim() == "NO") && (DropDownList37.SelectedValue.ToString().Trim() == "NO"))
                        Mensaje("Debe seleccionar al menos una inusualidad con valor de (SI), para poder ser ingresado como ROI.");
                    else
                    {
                        agregarRegistroOperacion();
                        Mensaje("ROI registrado con éxito.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar la información. " + ex.Message);
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Convertexcel(InfoGrid, Response, "Export Clientes");
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

        private void resetValues()
        {
            TextBox1.Text = "";
            DropDownList1.SelectedIndex = 0;
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
            DropDownList40.SelectedIndex = 0;
            DropDownList31.SelectedIndex = 0;
            DropDownList32.SelectedIndex = 0;
            DropDownList33.SelectedIndex = 0;
            DropDownList34.SelectedIndex = 0;
            DropDownList35.SelectedIndex = 0;
            DropDownList36.SelectedIndex = 0;
            DropDownList37.SelectedIndex = 0;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            tbConsulta.Visible = true;
            tbFormulario.Visible = false;
            Button7.Visible = false;
            CboSexoRepLegal.SelectedIndex = 0;
            DropDownList43.SelectedIndex = 0;
            DropDownList44.SelectedIndex = 0;
            DropDownList45.SelectedIndex = 0;
            DropDownList46.SelectedIndex = 0;
            DropDownList47.SelectedIndex = 0;
            DropDownList48.SelectedIndex = 0;
        }

        private void resetValuesConsulta()
        {
            DropDownList39.SelectedIndex = 0;
            TextBox99.Text = "";
            TextBox103.Text = "";
            TextBox105.Text = "";
            TextBox106.Text = "";
            TextBox107.Text = "";
            TextBox108.Text = "";
            TextBox109.Text = "";
            TextBox110.Text = "";
            TextBox111.Text = "";
            TextBox110.Visible = false;
            TextBox111.Visible = false;
            Label195.Visible = false;
            Label196.Visible = false;
            DropDownList38.SelectedIndex = 0;
            DropDownList41.SelectedIndex = 0;
            DropDownList42.SelectedIndex = 0;
            CboSexoRepLegal.SelectedIndex = 0;
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

        private void buscarClientForm(string NaOJu, string PrimerApellido, string SegundoApellido, string Nombre, string NumeroDocumento, string Ano,
            string FechaDesde, string FechaHasta, string RazonSocial, string NIT, string SINOConve, string Conve, string Estado)
        {
            string strFechaDesde = FechaDesde.Replace("-", ""), strFechaHasta = FechaHasta.Replace("-", "");
            DataTable dtInfo = new DataTable();

            loadGrid();
            resetValues();
            disableCampos();

            if (NaOJu == "0")
            {
                dtInfo = cKnowClient.buscarClientFormPN(PrimerApellido, SegundoApellido, Nombre, NumeroDocumento, Ano, strFechaDesde, strFechaHasta,
                    SINOConve, Conve, Estado);
                GridView1.Columns[8].Visible = false;
                GridView1.Columns[9].Visible = false;
            }
            else
            {
                dtInfo = cKnowClient.buscarClientFormPJ(PrimerApellido, SegundoApellido, Nombre, NumeroDocumento, Ano, FechaDesde, FechaHasta,
                    RazonSocial, NIT, SINOConve, Conve, Estado);
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
            CboOtrosIngresosPJ.Enabled = false;
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
            DropDownList23.Enabled = false;
            DropDownList24.Enabled = false;
            DropDownList25.Enabled = false;
            DropDownList26.Enabled = false;
            DropDownList27.Enabled = false;
            DropDownList28.Enabled = false;
            DropDownList29.Enabled = false;
            DropDownList30.Enabled = false;
            FileUpload1.Enabled = false;
            DropDownList40.Enabled = false;
            DropDownList31.Enabled = false;
            DropDownList32.Enabled = false;
            DropDownList33.Enabled = false;
            DropDownList34.Enabled = false;
            DropDownList35.Enabled = false;
            DropDownList36.Enabled = false;
            DropDownList37.Enabled = false;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            CboSexoRepLegal.Enabled = false;
            DropDownList43.Enabled = false;
            DropDownList44.Enabled = false;
            DropDownList45.Enabled = false;
            DropDownList46.Enabled = false;
            DropDownList47.Enabled = false;
            DropDownList48.Enabled = false;
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
            DropDownList23.Enabled = true;
            DropDownList24.Enabled = true;
            DropDownList25.Enabled = true;
            DropDownList26.Enabled = true;
            DropDownList27.Enabled = true;
            DropDownList28.Enabled = true;
            DropDownList29.Enabled = true;
            DropDownList30.Enabled = true;
            FileUpload1.Enabled = true;
            DropDownList40.Enabled = true;
            DropDownList31.Enabled = true;
            DropDownList32.Enabled = true;
            DropDownList33.Enabled = true;
            DropDownList34.Enabled = true;
            DropDownList35.Enabled = true;
            DropDownList36.Enabled = true;
            DropDownList37.Enabled = true;
            Button1.Visible = true;
            Button2.Visible = true;
            Button3.Visible = true;
            CboSexoRepLegal.Enabled = true;
            DropDownList43.Enabled = true;
            DropDownList44.Enabled = true;
            DropDownList45.Enabled = true;
            DropDownList46.Enabled = true;
            DropDownList47.Enabled = true;
            DropDownList48.Enabled = true;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void cargarInfo()
        {
            enableCampos();
            DataTable dtInfo = new DataTable();
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente);
            TextBox1.Text = dtInfo.Rows[0]["FechaFormulario"].ToString().Trim();
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Text.Trim() == dtInfo.Rows[0]["ClaseVinculacion"].ToString().Trim())
                {
                    if (DropDownList1.SelectedValue.ToString() == "OTRA")
                    {
                        Label6.Visible = true;
                        TextBox2.Visible = true;
                    }
                    break;
                }
            }
            TextBox2.Text = dtInfo.Rows[0]["OtraClaseVinculacion"].ToString().Trim();
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TomadorAsegurado"].ToString().Trim())
                {
                    if (DropDownList2.SelectedValue.ToString() == "Otra")
                    {
                        Label9.Visible = true;
                        TextBox3.Visible = true;
                    }
                    break;
                }
            }
            TextBox3.Text = dtInfo.Rows[0]["OtraTomadorAsegurado"].ToString().Trim();
            for (int i = 0; i < DropDownList3.Items.Count; i++)
            {
                DropDownList3.SelectedIndex = i;
                if (DropDownList3.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TomadorBeneficiario"].ToString().Trim())
                {
                    if (DropDownList3.SelectedValue.ToString() == "Otra")
                    {
                        Label11.Visible = true;
                        TextBox4.Visible = true;
                    }
                    break;
                }
            }
            TextBox4.Text = dtInfo.Rows[0]["OtraTomadorBeneficiario"].ToString().Trim();
            for (int i = 0; i < DropDownList4.Items.Count; i++)
            {
                DropDownList4.SelectedIndex = i;
                if (DropDownList4.SelectedItem.Text.Trim() == dtInfo.Rows[0]["AseguradoBeneficiario"].ToString().Trim())
                {
                    if (DropDownList4.SelectedValue.ToString() == "Otra")
                    {
                        Label13.Visible = true;
                        TextBox5.Visible = true;
                    }
                    break;
                }
            }
            TextBox5.Text = dtInfo.Rows[0]["OtraAseguradoBeneficiario"].ToString().Trim();

            dtInfo = cKnowClient.InfoFormPN(IdConocimientoCliente);
            TextBox6.Text = dtInfo.Rows[0]["PNPrimerApellido"].ToString().Trim();
            TextBox7.Text = dtInfo.Rows[0]["PNSegunApellido"].ToString().Trim();
            TextBox8.Text = dtInfo.Rows[0]["PNNombres"].ToString().Trim();
            for (int i = 0; i < DropDownList5.Items.Count; i++)
            {
                DropDownList5.SelectedIndex = i;
                if (DropDownList5.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNTipoDocumento"].ToString().Trim())
                    break;
            }
            TextBox9.Text = dtInfo.Rows[0]["PNNumeroDocumento"].ToString().Trim();
            TextBox10.Text = dtInfo.Rows[0]["PNFechaExpedicion"].ToString().Trim();
            TextBox11.Text = dtInfo.Rows[0]["PNLugar"].ToString().Trim();
            TextBox12.Text = dtInfo.Rows[0]["PNFechaNacimiento"].ToString().Trim();
            TextBox13.Text = dtInfo.Rows[0]["PNNacionalidad"].ToString().Trim();
            //TextBox14.Text=dtInfo.Rows[0]["PNOcupacionOficio"].ToString().Trim();
            //TextBox15.Text=dtInfo.Rows[0]["PNProfesion"].ToString().Trim();

            for (int i = 0; i < CboProfesion.Items.Count; i++)
            {
                CboProfesion.SelectedIndex = i;
                if (CboProfesion.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNProfesion"].ToString().Trim())
                    break;
            }

            for (int i = 0; i < DropDownList6.Items.Count; i++)
            {
                DropDownList6.SelectedIndex = i;
                if (DropDownList6.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNOcupacionOficio"].ToString().Trim())
                    break;
            }
            TextBox16.Text = dtInfo.Rows[0]["PNActividadEconomica"].ToString().Trim();
            TextBox17.Text = dtInfo.Rows[0]["PNCIIU"].ToString().Trim();
            TextBox18.Text = dtInfo.Rows[0]["PNEmpresaTrabajo"].ToString().Trim();
            TextBox19.Text = dtInfo.Rows[0]["PNArea"].ToString().Trim();
            TextBox20.Text = dtInfo.Rows[0]["PNCargo"].ToString().Trim();
            TextBox21.Text = dtInfo.Rows[0]["PNCiudad1"].ToString().Trim();
            TextBox22.Text = dtInfo.Rows[0]["PNDireccion"].ToString().Trim();
            TextBox23.Text = dtInfo.Rows[0]["PNTelefono1"].ToString().Trim();
            TextBox24.Text = dtInfo.Rows[0]["PNFax"].ToString().Trim();
            TextBox25.Text = dtInfo.Rows[0]["PNDireccionResidencia"].ToString().Trim();
            TextBox26.Text = dtInfo.Rows[0]["PNCiudad2"].ToString().Trim();
            TextBox27.Text = dtInfo.Rows[0]["PNTelefono2"].ToString().Trim();
            TextBox28.Text = dtInfo.Rows[0]["PNCelular"].ToString().Trim();
            for (int i = 0; i < DropDownList7.Items.Count; i++)
            {
                DropDownList7.SelectedIndex = i;
                if (DropDownList7.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta1"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList8.Items.Count; i++)
            {
                DropDownList8.SelectedIndex = i;
                if (DropDownList8.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta2"].ToString().Trim())
                    break;
            }

            for (int i = 0; i < DropDownList9.Items.Count; i++)
            {
                DropDownList9.SelectedIndex = i;
                if (DropDownList9.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNPregunta3"].ToString().Trim())
                    break;
            }
            TextBox29.Text = dtInfo.Rows[0]["PNEspecificacionPreguntas"].ToString().Trim();

            //TextBox30.Text=dtInfo.Rows[0]["PNIngresosMensuales"].ToString().Trim();
            for (int i = 0; i < CboIngresos.Items.Count; i++)
            {
                CboIngresos.SelectedIndex = i;
                if (CboIngresos.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNIngresosMensuales"].ToString().Trim())
                    break;
            }

            TextBox31.Text = dtInfo.Rows[0]["PNActivos"].ToString().Trim();

            //TextBox32.Text=dtInfo.Rows[0]["PNEgresoMensuales"].ToString().Trim();
            for (int i = 0; i < CboEgresos.Items.Count; i++)
            {
                CboEgresos.SelectedIndex = i;
                if (CboEgresos.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNEgresoMensuales"].ToString().Trim())
                    break;
            }
            TextBox33.Text = dtInfo.Rows[0]["PNPasivos"].ToString().Trim();

            //TextBox34.Text=dtInfo.Rows[0]["PNOtrosIngresos"].ToString().Trim();
            for (int i = 0; i < CboOtrosIngresos.Items.Count; i++)
            {
                CboOtrosIngresos.SelectedIndex = i;
                if (CboOtrosIngresos.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNOtrosIngresos"].ToString().Trim())
                    break;
            }
            TextBox35.Text = dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString().Trim();

            for (int i = 0; i < CboSexo.Items.Count; i++)
            {
                CboSexo.SelectedIndex = i;
                if (CboSexo.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PNSexo"].ToString().Trim())
                    break;
            }

            TxtPNCorreoElectronico.Text = dtInfo.Rows[0]["PNCorreoElectronico"].ToString().Trim();

            dtInfo = cKnowClient.InfoFormPJ(IdConocimientoCliente);
            TextBox36.Text = dtInfo.Rows[0]["PJRazonDenominacion"].ToString().Trim();
            TextBox37.Text = dtInfo.Rows[0]["PJNIT"].ToString().Trim();
            TextBox38.Text = dtInfo.Rows[0]["PJPrimerApellido"].ToString().Trim();
            TextBox39.Text = dtInfo.Rows[0]["PJSegundoApellido"].ToString().Trim();
            TextBox40.Text = dtInfo.Rows[0]["PJNombres"].ToString().Trim();
            for (int i = 0; i < DropDownList10.Items.Count; i++)
            {
                DropDownList10.SelectedIndex = i;
                if (DropDownList10.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoDocumento"].ToString().Trim())
                    break;
            }
            TextBox41.Text = dtInfo.Rows[0]["PJNumeroDocumento"].ToString().Trim();
            TextBox43.Text = dtInfo.Rows[0]["PJLugarExpedicion"].ToString().Trim();
            //TextBox42.Text = dtInfo.Rows[0]["PJFechaExpedicion"].ToString().Trim();
            TextBox44.Text = dtInfo.Rows[0]["PJDireccionOficina"].ToString().Trim();
            TextBox45.Text = dtInfo.Rows[0]["PJCiudad1"].ToString().Trim();
            TextBox46.Text = dtInfo.Rows[0]["PJTelefono1"].ToString().Trim();
            TextBox47.Text = dtInfo.Rows[0]["PJFax1"].ToString().Trim();
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
                    if (DropDownList12.SelectedValue.ToString() == "OTRA")
                    {
                        Label69.Visible = true;
                        TextBox52.Visible = true;
                    }
                    break;
                }
            }
            TextBox52.Text = dtInfo.Rows[0]["PJOtraActividadEconomica"].ToString().Trim();
            TextBox53.Text = dtInfo.Rows[0]["PJCIIU"].ToString().Trim();
            TextBox54.Text = dtInfo.Rows[0]["PJNombreAS1"].ToString().Trim();
            for (int i = 0; i < DropDownList13.Items.Count; i++)
            {
                DropDownList13.SelectedIndex = i;
                if (DropDownList13.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS1"].ToString().Trim())
                    break;
            }
            TextBox56.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS1"].ToString().Trim();
            TextBox55.Text = dtInfo.Rows[0]["PJNombreAS2"].ToString().Trim();
            for (int i = 0; i < DropDownList14.Items.Count; i++)
            {
                DropDownList14.SelectedIndex = i;
                if (DropDownList14.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS2"].ToString().Trim())
                    break;
            }
            TextBox57.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS2"].ToString().Trim();
            TextBox58.Text = dtInfo.Rows[0]["PJNombreAS3"].ToString().Trim();
            for (int i = 0; i < DropDownList15.Items.Count; i++)
            {
                DropDownList15.SelectedIndex = i;
                if (DropDownList15.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS3"].ToString().Trim())
                    break;
            }
            TextBox59.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS3"].ToString().Trim();
            TextBox60.Text = dtInfo.Rows[0]["PJNombreAS4"].ToString().Trim();
            for (int i = 0; i < DropDownList16.Items.Count; i++)
            {
                DropDownList16.SelectedIndex = i;
                if (DropDownList16.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS4"].ToString().Trim())
                    break;
            }
            TextBox61.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS4"].ToString().Trim();
            TextBox62.Text = dtInfo.Rows[0]["PJNombreAS5"].ToString().Trim();
            for (int i = 0; i < DropDownList17.Items.Count; i++)
            {
                DropDownList17.SelectedIndex = i;
                if (DropDownList17.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJTipoIdentificacionAS5"].ToString().Trim())
                    break;
            }
            TextBox63.Text = dtInfo.Rows[0]["PJNumeroDocumentoAS5"].ToString().Trim();
            //TextBox64.Text = dtInfo.Rows[0]["PJIngresosMensuales"].ToString().Trim();
            for (int i = 0; i < CboIngresosPJ.Items.Count; i++)
            {
                CboIngresosPJ.SelectedIndex = i;
                if (CboIngresosPJ.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJIngresosMensuales"].ToString().Trim())
                    break;
            }

            TextBox65.Text = dtInfo.Rows[0]["PJActivos"].ToString().Trim();
            //TextBox66.Text = dtInfo.Rows[0]["PJEgresoMensuales"].ToString().Trim();
            for (int i = 0; i < CboEgresosPJ.Items.Count; i++)
            {
                CboEgresosPJ.SelectedIndex = i;
                if (CboEgresosPJ.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJEgresoMensuales"].ToString().Trim())
                    break;
            }

            TextBox67.Text = dtInfo.Rows[0]["PJPasivos"].ToString().Trim();
            //TextBox68.Text = dtInfo.Rows[0]["PJOtrosIngresos"].ToString().Trim();
            for (int i = 0; i < CboOtrosIngresosPJ.Items.Count; i++)
            {
                CboOtrosIngresosPJ.SelectedIndex = i;
                if (CboOtrosIngresosPJ.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJOtrosIngresos"].ToString().Trim())
                    break;
            }

            TextBox69.Text = dtInfo.Rows[0]["PJConceptoOtrosIngresos"].ToString().Trim();

            for (int i = 0; i < CboSexoRepLegal.Items.Count; i++)
            {
                CboSexoRepLegal.SelectedIndex = i;
                if (CboSexoRepLegal.SelectedItem.Text.Trim() == dtInfo.Rows[0]["PJSexoRepLegal"].ToString().Trim())
                    break;
            }

            TxtCorreoPrincipal.Text = dtInfo.Rows[0]["PJCorreoPrincipal"].ToString().Trim();
            TxtCorreoSucursal.Text = dtInfo.Rows[0]["PJCorreoSucursal"].ToString().Trim();

            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);
            for (int i = 0; i < DropDownList18.Items.Count; i++)
            {
                DropDownList18.SelectedIndex = i;
                if (DropDownList18.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim())
                {
                    if (DropDownList18.SelectedValue.ToString() == "SI")
                    {
                        Label95.Visible = true;
                        DropDownList19.Visible = true;
                    }
                    break;
                }
            }
            for (int i = 0; i < DropDownList19.Items.Count; i++)
            {
                DropDownList19.SelectedIndex = i;
                if (DropDownList19.SelectedItem.Text.Trim() == dtInfo.Rows[0]["TipoTransaccion"].ToString().Trim())
                {
                    if (DropDownList19.SelectedValue.ToString() == "OTRA")
                    {
                        Label96.Visible = true;
                        TextBox70.Visible = true;
                    }
                    break;
                }
            }
            TextBox70.Text = dtInfo.Rows[0]["OtroTipoTransaccion"].ToString().Trim();
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

            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);
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
            TextBox93.Text = dtInfo.Rows[0]["OrigenFondos"].ToString().Trim();

            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);
            TextBox94.Text = dtInfo.Rows[0]["LugarEntrevista"].ToString().Trim();
            TextBox95.Text = dtInfo.Rows[0]["FechaEntrevista"].ToString().Trim();
            TextBox96.Text = dtInfo.Rows[0]["HoraEntrevista"].ToString().Trim();
            for (int i = 0; i < DropDownList22.Items.Count; i++)
            {
                DropDownList22.SelectedIndex = i;
                if (DropDownList22.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Resultado"].ToString().Trim())
                    break;
            }
            TextBox97.Text = dtInfo.Rows[0]["Observaciones1"].ToString().Trim();
            TextBox98.Text = dtInfo.Rows[0]["NombreResponsable"].ToString().Trim();
            TextBox100.Text = dtInfo.Rows[0]["FechaVerificacion"].ToString().Trim();
            TextBox101.Text = dtInfo.Rows[0]["HoraVerificacion"].ToString().Trim();
            TextBox102.Text = dtInfo.Rows[0]["NombreVerifica"].ToString().Trim();
            TextBox104.Text = dtInfo.Rows[0]["Observaciones2"].ToString().Trim();

            dtInfo = cKnowClient.InfoFormDocsInu(IdConocimientoCliente);
            for (int i = 0; i < DropDownList23.Items.Count; i++)
            {
                DropDownList23.SelectedIndex = i;
                if (DropDownList23.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc1"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList24.Items.Count; i++)
            {
                DropDownList24.SelectedIndex = i;
                if (DropDownList24.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc2"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList25.Items.Count; i++)
            {
                DropDownList25.SelectedIndex = i;
                if (DropDownList25.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc3"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList26.Items.Count; i++)
            {
                DropDownList26.SelectedIndex = i;
                if (DropDownList26.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc4"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList27.Items.Count; i++)
            {
                DropDownList27.SelectedIndex = i;
                if (DropDownList27.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc5"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList28.Items.Count; i++)
            {
                DropDownList28.SelectedIndex = i;
                if (DropDownList28.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc6"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList29.Items.Count; i++)
            {
                DropDownList29.SelectedIndex = i;
                if (DropDownList29.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc7"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList30.Items.Count; i++)
            {
                DropDownList30.SelectedIndex = i;
                if (DropDownList30.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc8"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList40.Items.Count; i++)
            {
                DropDownList40.SelectedIndex = i;
                if (DropDownList40.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Conve1"].ToString().Trim())
                    break;
            }

            for (int i = 0; i < DropDownList31.Items.Count; i++)
            {
                DropDownList31.SelectedIndex = i;
                if (DropDownList31.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Conve2"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList32.Items.Count; i++)
            {
                DropDownList32.SelectedIndex = i;
                if (DropDownList32.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Conve3"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList33.Items.Count; i++)
            {
                DropDownList33.SelectedIndex = i;
                if (DropDownList33.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Conve4"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList34.Items.Count; i++)
            {
                DropDownList34.SelectedIndex = i;
                if (DropDownList34.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Conve5"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList35.Items.Count; i++)
            {
                DropDownList35.SelectedIndex = i;
                if (DropDownList35.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Conve6"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList36.Items.Count; i++)
            {
                DropDownList36.SelectedIndex = i;
                if (DropDownList36.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Conve7"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList37.Items.Count; i++)
            {
                DropDownList37.SelectedIndex = i;
                if (DropDownList37.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Conve8"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList43.Items.Count; i++)
            {
                DropDownList43.SelectedIndex = i;
                if (DropDownList43.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc9"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList44.Items.Count; i++)
            {
                DropDownList44.SelectedIndex = i;
                if (DropDownList44.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc10"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList45.Items.Count; i++)
            {
                DropDownList45.SelectedIndex = i;
                if (DropDownList45.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc11"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList46.Items.Count; i++)
            {
                DropDownList46.SelectedIndex = i;
                if (DropDownList46.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc12"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList47.Items.Count; i++)
            {
                DropDownList47.SelectedIndex = i;
                if (DropDownList47.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc13"].ToString().Trim())
                    break;
            }
            for (int i = 0; i < DropDownList48.Items.Count; i++)
            {
                DropDownList48.SelectedIndex = i;
                if (DropDownList48.SelectedItem.Text.Trim() == dtInfo.Rows[0]["Doc14"].ToString().Trim())
                    break;
            }
            tbConsulta.Visible = false;
            tbFormulario.Visible = true;
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
                    InfoGridArchivos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                           });
                }
                GridView2.DataSource = InfoGridArchivos;
                GridView2.DataBind();
            }
        }

        private void imprimirFormulario()
        {
            string str;
            str = "window.open('ReporteKnowCliente.aspx?IdConocimientoCliente=" + IdConocimientoCliente + "','Reporte','width=950px,height=900px,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        private void updateClienteForm()
        {
            IdConocimientoCliente = cKnowClient.agregarConocimientoCliente(String.Format("{0:yyyy MM dd}", DateTime.Now).Replace(" ", ""), String.Format("{0:yyyy}", DateTime.Now));
            cKnowClient.InfoFormCliente(IdConocimientoCliente, TextBox1.Text.Trim(), DropDownList1.SelectedValue.ToString().Trim(), TextBox2.Text.Trim(), DropDownList2.SelectedValue.ToString().Trim(), TextBox3.Text.Trim(), DropDownList3.SelectedValue.ToString().Trim(), TextBox4.Text.Trim(), DropDownList4.SelectedValue.ToString().Trim(), TextBox5.Text.Trim());
            cKnowClient.InfoFormPN(IdConocimientoCliente, TextBox6.Text.Trim(), TextBox7.Text.Trim(), TextBox8.Text.Trim(), DropDownList5.SelectedValue.ToString().Trim(), TextBox9.Text.Trim(), TextBox10.Text.Trim(), TextBox11.Text.Trim(), TextBox12.Text.Trim(), TextBox13.Text.Trim(), DropDownList6.SelectedValue.ToString().Trim(), CboProfesion.SelectedValue.ToString().Trim(), TextBox16.Text.Trim(), TextBox17.Text.Trim(), TextBox18.Text.Trim(), TextBox19.Text.Trim(), TextBox20.Text.Trim(), TextBox21.Text.Trim(), TextBox22.Text.Trim(), TextBox23.Text.Trim(), TextBox24.Text.Trim(), TextBox25.Text.Trim(), TextBox26.Text.Trim(), TextBox27.Text.Trim(), TextBox28.Text.Trim(), DropDownList7.SelectedValue.ToString().Trim(), DropDownList8.SelectedValue.ToString().Trim(), DropDownList9.SelectedValue.ToString().Trim(), TextBox29.Text.Trim(), CboIngresos.SelectedValue.ToString().Trim(), TextBox31.Text.Trim(), CboEgresos.SelectedValue.ToString().Trim(), TextBox33.Text.Trim(), CboOtrosIngresos.SelectedValue.ToString().Trim(), TextBox35.Text.Trim(), CboSexo.SelectedValue.ToString().Trim(), TxtPNCorreoElectronico.Text.Trim());
            cKnowClient.InfoFormPJ(IdConocimientoCliente, TextBox36.Text.Trim(), TextBox37.Text.Trim(), TextBox38.Text.Trim(), TextBox39.Text.Trim(), TextBox40.Text.Trim(), DropDownList10.SelectedValue.ToString().Trim(), TextBox41.Text.Trim(), TextBox43.Text.Trim(), "", TextBox44.Text.Trim(), TextBox45.Text.Trim(), TextBox46.Text.Trim(), TextBox47.Text.Trim(), TextBox48.Text.Trim(), TextBox49.Text.Trim(), TextBox50.Text.Trim(), TextBox51.Text.Trim(), DropDownList11.SelectedValue.ToString().Trim(), DropDownList12.SelectedValue.ToString().Trim(), TextBox52.Text.Trim(), TextBox53.Text.Trim(), TextBox54.Text.Trim(), DropDownList13.SelectedValue.ToString().Trim(), TextBox56.Text.Trim(), TextBox55.Text.Trim(), DropDownList14.SelectedValue.ToString().Trim(), TextBox57.Text.Trim(), TextBox58.Text.Trim(), DropDownList15.SelectedValue.ToString().Trim(), TextBox59.Text.Trim(), TextBox60.Text.Trim(), DropDownList16.SelectedValue.ToString().Trim(), TextBox61.Text.Trim(), TextBox62.Text.Trim(), DropDownList17.SelectedValue.ToString().Trim(), TextBox63.Text.Trim(), CboIngresosPJ.SelectedValue.ToString().Trim(), TextBox65.Text.Trim(), CboEgresosPJ.SelectedValue.ToString().Trim(), TextBox67.Text.Trim(), CboOtrosIngresosPJ.SelectedValue.ToString().Trim(), TextBox69.Text.Trim(), CboSexoRepLegal.SelectedValue.ToString().Trim(), TxtCorreoPrincipal.Text.Trim(), TxtCorreoSucursal.Text.Trim());
            cKnowClient.InfoFormPF(IdConocimientoCliente, DropDownList18.SelectedValue.ToString().Trim(), DropDownList19.SelectedValue.ToString().Trim(), TextBox70.Text.Trim(), TextBox71.Text.Trim(), TextBox72.Text.Trim(), TextBox73.Text.Trim(), TextBox74.Text.Trim(), TextBox75.Text.Trim(), TextBox76.Text.Trim(), TextBox77.Text.Trim(), TextBox78.Text.Trim(), TextBox79.Text.Trim(), TextBox80.Text.Trim(), TextBox81.Text.Trim(), TextBox82.Text.Trim(), TextBox83.Text.Trim(), TextBox84.Text.Trim());
            cKnowClient.InfoFormSeguros(IdConocimientoCliente, TextBox85.Text.Trim(), TextBox86.Text.Trim(), TextBox87.Text.Trim(), TextBox88.Text.Trim(), DropDownList20.SelectedValue.ToString().Trim(), TextBox89.Text.Trim(), TextBox90.Text.Trim(), TextBox91.Text.Trim(), TextBox92.Text.Trim(), DropDownList21.SelectedValue.ToString().Trim(), TextBox93.Text.Trim());
            cKnowClient.InfoFormEntrevista(IdConocimientoCliente, TextBox94.Text.Trim(), TextBox95.Text.Trim(), TextBox96.Text.Trim(), DropDownList22.SelectedValue.ToString().Trim(), TextBox97.Text.Trim(), TextBox98.Text.Trim(), TextBox100.Text.Trim(), TextBox101.Text.Trim(), TextBox102.Text.Trim(), TextBox104.Text.Trim());
            cKnowClient.InfoFormDocsInu(IdConocimientoCliente, DropDownList23.SelectedValue.ToString().Trim(), DropDownList24.SelectedValue.ToString().Trim(), DropDownList25.SelectedValue.ToString().Trim(), DropDownList26.SelectedValue.ToString().Trim(), DropDownList27.SelectedValue.ToString().Trim(), DropDownList28.SelectedValue.ToString().Trim(), DropDownList29.SelectedValue.ToString().Trim(), DropDownList30.SelectedValue.ToString().Trim(), "", DropDownList40.SelectedValue.ToString().Trim(), DropDownList31.SelectedValue.ToString().Trim(), DropDownList32.SelectedValue.ToString().Trim(), DropDownList33.SelectedValue.ToString().Trim(), DropDownList34.SelectedValue.ToString().Trim(), DropDownList35.SelectedValue.ToString().Trim(), DropDownList36.SelectedValue.ToString().Trim(), DropDownList37.SelectedValue.ToString().Trim(), DropDownList43.SelectedValue.ToString().Trim(), DropDownList44.SelectedValue.ToString().Trim(), DropDownList45.SelectedValue.ToString().Trim(), DropDownList46.SelectedValue.ToString().Trim(), DropDownList47.SelectedValue.ToString().Trim(), DropDownList48.SelectedValue.ToString());
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

        private void agregarRegistroOperacion()
        {
            String MensajeCorreo = string.Empty;

            if (DropDownList40.SelectedValue.ToString().Trim() != "NO")
                MensajeCorreo += "Nombre y/o identificación no consistentes con soportes. ";

            if (DropDownList31.SelectedValue.ToString().Trim() != "NO")
                MensajeCorreo += "Nombre de representante legal no coincide con cámara de comercio. ";

            if (DropDownList32.SelectedValue.ToString().Trim() != "NO")
                MensajeCorreo += "Información representante legal no consistente con documento de identidad. ";

            if (DropDownList33.SelectedValue.ToString().Trim() != "NO")
                MensajeCorreo += "Dirección domicilio u oficina no consistente con soporte. ";

            if (DropDownList34.SelectedValue.ToString().Trim() != "NO")
                MensajeCorreo += "Actividad económica no consistente con soporte. ";

            if (DropDownList35.SelectedValue.ToString().Trim() != "NO")
                MensajeCorreo += "Socios o accionistas no consistentes con soporte. ";

            if (DropDownList36.SelectedValue.ToString().Trim() != "NO")
                MensajeCorreo += "Cifras financieras no consistentes con estados financieros. ";

            if (DropDownList37.SelectedValue.ToString().Trim() != "NO")
                MensajeCorreo += "Falta verificación de listas.";

            cRegistroOperacion.agregarRegistroOperacion("4", InfoGrid.Rows[IdexRow]["NumeroDocumento"].ToString().Trim(), (InfoGrid.Rows[IdexRow]["Nombres"].ToString().Trim() + " " + InfoGrid.Rows[IdexRow]["PrimerApellido"].ToString().Trim() + " " + InfoGrid.Rows[IdexRow]["SegundoApellido"].ToString().Trim()).Trim(), "Formulario conocimiento del cliente.", "Inusualidad detectada en el formulario del conocimiento del cliente. " + MensajeCorreo, "Inusualidad detectada en el formulario del conocimiento del cliente.", String.Format("{0:yyyy-MM-dd}", DateTime.Now) + " 12:00:00:000", "Inusualidad detectada en el formulario del conocimiento del cliente.", "Inusualidad detectada en el formulario del conocimiento del cliente.", "", "", "");
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

        #region PDFs

        private void loadFile()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cKnowClient.loadCodigoInfoFormArchivo();
            if (dtInfo.Rows.Count > 0)
            {
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" +
                    IdConocimientoCliente.ToString() + "-" +
                    FileUpload1.FileName.ToString().Trim();
            }
            else
            {
                nameFile = "1-" + IdConocimientoCliente.ToString() + "-" + FileUpload1.FileName.ToString().Trim();
            }
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
    }
}