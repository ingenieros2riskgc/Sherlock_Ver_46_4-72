using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class ObjetivoEstrategico : System.Web.UI.UserControl
    {
        string IdFormulario = "7004";
        private cGestion cGestion = new cGestion();
        cCuenta cCuenta = new cCuenta();

        #region Properties

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!IsPostBack)
            {
                loadGridPlan();
                cargarInfoGridPlan();
                inicializarValores();
                loadDDLRol();
                loadDDLRolAdd();
            }
        }

        private void inicializarValores()
        {
            IdexRow = 0;
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
            int contar = Convert.ToInt32(DropDownList2.Items.Count.ToString());
            DropDownList2.SelectedIndex = (contar - 1);
            TbAdicionarVision.Visible = false;
            TbModificarVision.Visible = false;
        }

        private void loadDDLRol()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.Perspectivas();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Erro al cargar los tipos de roles. " + ex.Message);
            }

        }

        private void loadDDLRolAdd()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.Perspectivas();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList2.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Erro al cargar Perspectivas. " + ex.Message);
            }

        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdObjetivo", typeof(string));
            grid.Columns.Add("CodigoObjetivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("NombreDetalle", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFin", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
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

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.ObjEstrategico(LabelIdPlan.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdObjetivo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoObjetivo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["NombreDetalle"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaFin"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
            }
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
            cGestion.agregarObj(Sanitizer.GetSafeHtmlFragment(TextBox13.Text.Trim()), LabelIdPlan.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), DropDownList2.SelectedItem.Value.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()) + " 12:00:00:000");
        }

        private void modificarLista()
        {
            cGestion.modificarObj(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), DropDownList1.SelectedItem.Value.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()) + " 12:00:00:000", InfoGrid.Rows[IdexRow]["IdObjetivo"].ToString().Trim());
        }

        private void verModificar()
        {
            TbModificarVision.Visible = true;
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                agregarLista();
                loadGrid();
                cargarInfoGrid();
                resetValues();
                Mensaje("Objetivo Estratégico almacenado correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar el Objetivo Estratégico. " + ex.Message);
            }
        }

        protected void BtnCancelaPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbAdicionarVision.Visible = false;
            resetValues();
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
                        modificarLista();
                        loadGrid();
                        cargarInfoGrid();
                        resetValues();
                        Mensaje("Objetivo Estratégico modificado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al modificar el Objetivo Estratégico. " + ex.Message);
            }
        }

        protected void BtnCancelaModPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbModificarVision.Visible = false;
        }

        protected void BtnAdicionaPlan_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (cGestion.ValidarFechaPE(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) == "S")
                        Mensaje1("Plan Estratégico vencido. Solo Lectura");
                    else
                    {
                        TbAdicionarVision.Visible = true;
                        TbModificarVision.Visible = false;
                        loadCodigo();
                        TextBox1.Focus();
                        TextBox8.Text = Session["loginUsuario"].ToString().Trim();
                        TextBox9.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al modificar el Plan Estratégico. " + ex.Message);
            }
        }

        protected void VerPlanEstrategico_Click(object sender, EventArgs e)
        {
            FiltroPE.Visible = true;
            loadGridPlan();
            cargarInfoGridPlan();
            FiltroAplicado.Visible = false;
            TablePEstrategico.Visible = false;
            TbModificarVision.Visible = false;
        }

        private void modificar()
        {
            TextBox14.Text = InfoGrid.Rows[IdexRow]["CodigoObjetivo"].ToString().Trim();
            TextBox2.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Text.Trim() == InfoGrid.Rows[IdexRow]["NombreDetalle"].ToString().Trim())
                {
                    break;
                }
            }
            TextBox5.Text = InfoGrid.Rows[IdexRow]["FechaInicio"].ToString().Trim();
            TextBox10.Text = InfoGrid.Rows[IdexRow]["FechaFin"].ToString().Trim();
            TextBox7.Text = InfoGrid.Rows[IdexRow]["Usuario"].ToString().Trim();
            TextBox6.Text = InfoGrid.Rows[IdexRow]["FechaRegistro"].ToString().Trim();
            TbAdicionarVision.Visible = false;
            TextBox2.Focus();
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
                    cGestion.eliminarObj(InfoGrid.Rows[IdexRow]["IdObjetivo"].ToString().Trim());
                    loadGrid();
                    cargarInfoGrid();
                    Mensaje("Objetivo Estratégico eliminado correctamente.");
                    TbAdicionarVision.Visible = false;
                    TbModificarVision.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al eliminar Objetivo Estratégico. " + ex.Message);
            }
        }

        private void loadCodigo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigo("IdObjetivo", "Gestion.ObjetivosEstrategicos");
                if (dtInfo.Rows.Count > 0)
                {
                    TextBox13.Text = "OB" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                }
                else
                {
                    TextBox13.Text = "OB1";
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar código. " + ex.Message);
            }
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
    }
}