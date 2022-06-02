using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class Seguimiento : System.Web.UI.UserControl
    {
        string IdFormulario = "7009";
        string IdSeguimiento, IdPlan;
        private cGestion cGestion = new cGestion();
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private String nameFile;
        private String NameFile
        {
            get
            {
                nameFile = (String)ViewState["nameFile"];
                return nameFile;
            }
            set
            {
                nameFile = value;
                ViewState["nameFile"] = nameFile;
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

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)ViewState["infGrid"];
                return infGrid;
            }
            set
            {
                infGrid = value;
                ViewState["infGrid"] = infGrid;
            }
        }

        private DataTable infGridPlan;
        private DataTable InfoGridPlan
        {
            get
            {
                infGridPlan = (DataTable)ViewState["infGridPlan"];
                return infGridPlan;
            }
            set
            {
                infGridPlan = value;
                ViewState["infGridPlan"] = infGridPlan;
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

        private int rowGrid;
        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }

        private DataTable infoGridControles;
        private DataTable InfoGridControles
        {
            get
            {
                infoGridControles = (DataTable)ViewState["infoGridControles"];
                return infoGridControles;
            }
            set
            {
                infoGridControles = value;
                ViewState["infoGridControles"] = infoGridControles;
            }
        }

        private int rowGridControles;
        private int RowGridControles
        {
            get
            {
                rowGridControles = (int)ViewState["rowGridControles"];
                return rowGridControles;
            }
            set
            {
                rowGridControles = value;
                ViewState["rowGridControles"] = rowGridControles;
            }
        }

        private int rowGridArchivoControl;
        private int RowGridArchivoControl
        {
            get
            {
                rowGridArchivoControl = (int)ViewState["rowGridArchivoControl"];
                return rowGridArchivoControl;
            }
            set
            {
                rowGridArchivoControl = value;
                ViewState["rowGridArchivoControl"] = rowGridArchivoControl;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);
            scrtManager.RegisterPostBackControl(ImageButton7);
            scrtManager.RegisterPostBackControl(GridView3);

            if (!IsPostBack)
            {
                loadGridPlan();
                cargarInfoGridPlan();
                inicializarValores();
                loadOrganoControl();
            }
        }

        private void inicializarValores()
        {
            IdexRow = 0;
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdSeguimiento", typeof(string));
            grid.Columns.Add("IdPlan", typeof(string));
            grid.Columns.Add("NumeroActa", typeof(string));
            grid.Columns.Add("FechaReunion", typeof(string));
            grid.Columns.Add("OControl", typeof(string));
            grid.Columns.Add("NumeroSeguimiento", typeof(string));
            grid.Columns.Add("Comentarios", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.Seguimiento(LabelIdPlan.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdSeguimiento"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["IdPlan"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["NumeroActa"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaReunion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["OControl"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["NumeroSeguimiento"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Comentarios"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
            }
        }

        private void loadGridPlan()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlan", typeof(string));
            grid.Columns.Add("CodigoPlan", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFin", typeof(string));
            GridViewPlanEstratagico.DataSource = grid;
            GridViewPlanEstratagico.DataBind();
            InfoGridPlan = grid;
        }

        private void cargarInfoGridPlan()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.FiltroPlan();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPlan.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlan"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoPlan"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaFin"].ToString().Trim(),
                                                    });
                }

                GridViewPlanEstratagico.DataSource = InfoGridPlan;
                GridViewPlanEstratagico.DataBind();

            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void Mensaje1(String Mensaje)
        {
            lblMsgBox1.Text = Mensaje;
            mpeMsgBox1.Show();
        }

        private void agregarLista()
        {
            cGestion.agregarSeguimiento(LabelIdPlan.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()) + " 12:00:00:000", DropDownList1.SelectedItem.Value.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox8.Text.Trim()));
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TbAdicionarVision.Visible = false;
            TbModificarVision.Visible = false;
            int contar = Convert.ToInt32(DropDownList1.Items.Count.ToString());
            DropDownList1.SelectedIndex = (contar - 1);
            IdSeguimiento = "";
            IdPlan = "";
        }

        private void modificarLista()
        {
            cGestion.modificarSeguimiento(Sanitizer.GetSafeHtmlFragment(TextBox13.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + " 12:00:00:000", DropDownList3.SelectedItem.Value.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox14.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox16.Text.Trim()));
        }

        private void verModificar()
        {
            TbModificarVision.Visible = true;
            TbAdicionarVision.Visible = false;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    verModificar();
                    modificar();
                    break;
                case "Eliminar":
                    eliminar();
                    break;
            }
        }

        protected void GridViewPlanEstratagico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Filtrar":
                    filtrar();
                    filtrarplan();
                    break;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewPlanEstratagico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void modificar()
        {

            TextBox16.Text = InfoGrid.Rows[IdexRow]["IdSeguimiento"].ToString().Trim();
            TextBox13.Text = InfoGrid.Rows[IdexRow]["NumeroActa"].ToString().Trim();
            TextBox5.Text = InfoGrid.Rows[IdexRow]["FechaReunion"].ToString().Trim();
            for (int i = 0; i < DropDownList3.Items.Count; i++)
            {
                DropDownList3.SelectedIndex = i;
                if (DropDownList3.SelectedItem.Text.Trim() == InfoGrid.Rows[IdexRow]["OControl"].ToString().Trim())
                {
                    break;
                }
            }
            TextBox14.Text = InfoGrid.Rows[IdexRow]["NumeroSeguimiento"].ToString().Trim();
            TextBox2.Text = InfoGrid.Rows[IdexRow]["Comentarios"].ToString().Trim();
            TextBox7.Text = InfoGrid.Rows[IdexRow]["Usuario"].ToString().Trim();
            TextBox6.Text = InfoGrid.Rows[IdexRow]["FechaRegistro"].ToString().Trim();
            TbAdicionarVision.Visible = false;
            TbArchivos.Visible = true;
            IdSeguimiento = "";
            IdPlan = "";
            IdSeguimiento = InfoGrid.Rows[IdexRow]["IdSeguimiento"].ToString().Trim();
            //IdSeguimiento = TextBox15.Text;
            IdPlan = LabelIdPlan.Text;
            loadGridArchivoControl();
            loadInfoArchivoControl();
        }

        private void eliminar()
        {
            try
            {
                if (cCuenta.permisosBorrar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                    {
                        Mensaje1("Plan Estratégico vencido. Solo Lectura");
                    }
                    else
                    {
                        cGestion.eliminarSeguimientos(InfoGrid.Rows[IdexRow]["IdSeguimiento"].ToString().Trim());
                        loadGrid();
                        cargarInfoGrid();
                        Mensaje("Seguimiento eliminado correctamente.");
                        TbAdicionarVision.Visible = false;
                        TbModificarVision.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al eliminar Objetivo Estratégico. " + ex.Message);
            }
        }

        protected void BtnAdicionaVision_Click(object sender, ImageClickEventArgs e)
        {
            TbAdicionarVision.Visible = true;
            TbModificarVision.Visible = false;
        }

        protected void BtnGuardaPlan_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                loadCodigoOC();
                agregarLista();
                loadGrid();
                cargarInfoGrid();
                //resetValues();
                BtnGuardaPlan.Visible = false;
                Mensaje("Seguimiento almacenado correctamente.");
                IdSeguimiento = "";
                IdPlan = "";
                //IdSeguimiento = InfoGrid.Rows[IdexRow]["IdSeguimiento"].ToString().Trim();
                IdSeguimiento = Sanitizer.GetSafeHtmlFragment(TextBox15.Text);
                IdPlan = LabelIdPlan.Text;
                TbArchivos.Visible = true;
                loadGridArchivoControl();
                loadInfoArchivoControl();
            }
            catch (Exception ex)
            {
                Mensaje("Error al guardar el Seguimiento." + ex.Message);
            }
        }

        protected void BtnCancelaPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbAdicionarVision.Visible = false;
            resetValues();
            TbArchivos.Visible = false;
        }

        protected void BtnModificaPlan_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                    {
                        Mensaje1("Plan Estratégico vencido. Solo Lectura");
                    }
                    else
                    {
                        loadCodigoOCmodificar();
                        modificarLista();
                        loadGrid();
                        cargarInfoGrid();
                        resetValues();
                        IdSeguimiento = "";
                        IdPlan = "";
                        IdSeguimiento = InfoGrid.Rows[IdexRow]["IdSeguimiento"].ToString().Trim();
                        //IdSeguimiento = TextBox15.Text;
                        IdPlan = LabelIdPlan.Text;
                        Mensaje("Seguimiento modificado correctamente.");
                        TbArchivos.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el Seguimiento." + ex.Message);
            }
        }

        protected void BtnCancelaModPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbModificarVision.Visible = false;
            TbArchivos.Visible = false;
        }

        protected void BtnAdicionaPlan_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                {
                    Mensaje1("Plan Estratégico vencido. Solo Lectura");
                }
                else
                {
                    resetValues();
                    TbArchivos.Visible = false;
                    TbAdicionarVision.Visible = true;
                    TbModificarVision.Visible = false;
                    TextBox9.Text = Session["loginUsuario"].ToString().Trim();
                    TextBox10.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    loadCodigo();
                    IdSeguimiento = "";
                    IdPlan = "";
                    //IdSeguimiento = InfoGrid.Rows[IdexRow]["IdSeguimiento"].ToString().Trim();
                    IdSeguimiento = Sanitizer.GetSafeHtmlFragment(TextBox15.Text);
                    IdPlan = LabelIdPlan.Text;
                    BtnGuardaPlan.Visible = true;
                }
            }
        }

        private void loadCodigo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigo("IdSeguimiento", "Gestion.Seguimiento");
                if (dtInfo.Rows.Count > 0)
                {
                    TextBox15.Text = dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                }
                else
                {
                    TextBox15.Text = "1";
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar código. " + ex.Message);
            }
        }

        private void loadCodigoOC()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigoOC(LabelIdPlan.Text, DropDownList1.SelectedItem.Value.ToString().Trim());
                TextBox4.Text = dtInfo.Rows[0]["Codigo"].ToString().Trim();
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar código. " + ex.Message);
            }
        }

        private void loadCodigoOCmodificar()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigoOC(LabelIdPlan.Text, DropDownList3.SelectedItem.Value.ToString().Trim());
                TextBox14.Text = dtInfo.Rows[0]["Codigo"].ToString().Trim();
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar código. " + ex.Message);
            }
        }

        private void loadOrganoControl()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.LOControl();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                    DropDownList3.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Erro al cargar Perspectivas. " + ex.Message);
            }

        }

        protected void VerPlanEstrategico_Click(object sender, EventArgs e)
        {
            FiltroPE.Visible = true;
            loadGridPlan();
            cargarInfoGridPlan();
            FiltroAplicado.Visible = false;
            TablePEstrategico.Visible = false;
        }

        private void filtrar()
        {
            FiltroAplicado.Visible = true;
            FiltroPE.Visible = false;
        }

        private void filtrarplan()
        {
            LabelIdPlan.Text = InfoGridPlan.Rows[IdexRow]["IdPlan"].ToString().Trim();
            TextBox12.Text = InfoGridPlan.Rows[IdexRow]["FechaInicio"].ToString().Trim();
            TextBox11.Text = InfoGridPlan.Rows[IdexRow]["FechaFin"].ToString().Trim();
            LabelCodigoPlan.Text = InfoGridPlan.Rows[IdexRow]["CodigoPlan"].ToString().Trim();
            LabelNombrePlan.Text = InfoGridPlan.Rows[IdexRow]["Nombre"].ToString().Trim();
            TablePEstrategico.Visible = true;
            loadGrid();
            cargarInfoGrid();
        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                Mensaje1("Plan Estratégico vencido. Solo Lectura");
            else
            {
                if (TbAdicionarVision.Visible == true)
                {
                    IdSeguimiento = "";
                    IdPlan = "";
                    IdSeguimiento = Sanitizer.GetSafeHtmlFragment(TextBox15.Text);
                    IdPlan = LabelIdPlan.Text;
                }
                else
                {
                    IdSeguimiento = "";
                    IdPlan = "";
                    IdSeguimiento = InfoGrid.Rows[IdexRow]["IdSeguimiento"].ToString().Trim();
                    IdPlan = LabelIdPlan.Text;
                }

                try
                {
                    if (FileUpload1.HasFile)
                    {
                        if (Path.GetExtension(FileUpload1.FileName).ToLower() == ".pdf")
                        {
                            mtdCargarPdfSeguimiento();
                            loadGridArchivoControl();
                            loadInfoArchivoControl();
                            Mensaje("Archivo cargado exitósamente.");
                        }
                        else
                            Mensaje("Unicamente archivos .pdf");
                    }
                    else
                        Mensaje("No hay archivos para cargar.");
                }
                catch (Exception ex)
                {
                    Mensaje("Error al actualizar la lista. " + ex.Message);
                }
            }
        }

        private void loadGridArchivoControl()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("IdSeguimiento", typeof(string));
            grid.Columns.Add("IdPlanAccion", typeof(string));
            grid.Columns.Add("NombreArchivo", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            InfoGridControles = grid;
            GridView3.DataSource = InfoGridControles;
            GridView3.DataBind();
        }

        private void loadInfoArchivoControl()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.loadInfoArchivoSeguimiento(IdSeguimiento, IdPlan);

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridControles.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdSeguimiento"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdPlanAccion"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreArchivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim()
                                                                 });
                }
                GridView3.DataSource = InfoGridControles;
                GridView3.DataBind();
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoControl = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    //descargarArchivo();
                    mtdDescargarPdfSeguimiento();
                    break;
            }
        }

        #region PDFs

        private void loadFile()
        {
            DataTable dtInfo = new DataTable();
            string nameFile;
            dtInfo = cGestion.loadCodigoArchivoSeguimiento();
            if (dtInfo.Rows.Count > 0)
            {
                nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() +
                    "-" + IdPlan + "-" + IdSeguimiento +
                    "-" + FileUpload1.FileName.ToString().Trim();
            }
            else
            {
                nameFile = "1-" + IdPlan + "-" + IdSeguimiento + "-" + FileUpload1.FileName.ToString().Trim();
            }

            FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFGestionSeguimiento/") + nameFile);
            cGestion.agregarArchivoSeguimiento(IdSeguimiento, IdPlan, nameFile, FileUpload1.FileName);
            Mensaje("Archivo cargado correctamente.");
        }

        private void descargarArchivo()
        {
            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + InfoGridControles.Rows[RowGridArchivoControl]["NombreArchivo"].ToString().Trim());
            Response.TransmitFile(Server.MapPath("~/Archivos/PDFGestionSeguimiento/" + InfoGridControles.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim()));
            Response.End();
        }

        private void mtdCargarPdfSeguimiento()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cGestion.loadCodigoArchivoSeguimiento();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    IdPlan, IdSeguimiento, FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}-{2}",
                   IdPlan, IdSeguimiento, FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cGestion.mtdAgregarPdfSeguimiento(IdSeguimiento, IdPlan, FileUpload1.FileName, strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfSeguimiento()
        {
            #region Vars
            string strNombreArchivo = InfoGridControles.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cGestion.mtdDescargarPdfSeguimiento(strNombreArchivo);
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