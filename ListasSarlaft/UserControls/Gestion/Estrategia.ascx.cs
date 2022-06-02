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
    public partial class Estrategia : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "7006";
        private cGestion cGestion = new cGestion();

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
                loadDDLRolAdd();
                loadDDLRolAddFu();
                loadDDLRolAddFuAdd();
                loadDDLRolAddAdd();
            }
        }

        private void inicializarValores()
        {
            IdexRow = 0;
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdEstrategia", typeof(string));
            grid.Columns.Add("CodigoEstrategia", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void loadGridMeta()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdMeta", typeof(string));
            grid.Columns.Add("IdEstrategia", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Funcion", typeof(string));
            grid.Columns.Add("Formato", typeof(string));
            grid.Columns.Add("Valor", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGridMeta = grid;
        }
        
        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.Estrategia(LabelIdOBJ.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdEstrategia"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoEstrategia"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
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
        private void loadGridOBJ()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdObjetivo", typeof(string));
            grid.Columns.Add("CodigoObjetivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            GridViewOBJ.DataSource = grid;
            GridViewOBJ.DataBind();
            InfoGridOBJ = grid;
        }
        private void cargarInfoGridOBJ()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.ObjEstrategico(LabelIdPlan.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridOBJ.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdObjetivo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoObjetivo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    });
                }
                GridViewOBJ.DataSource = InfoGridOBJ;
                GridViewOBJ.DataBind();
            }
        }
        private void cargarInfoGridMeta()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.Meta(LabelIdEstrategia.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridMeta.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdMeta"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["IdEstrategia"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Funcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Formato"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Valor"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView3.DataSource = InfoGridMeta;
                GridView3.DataBind();
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
            cGestion.agregarEstrategia(Sanitizer.GetSafeHtmlFragment(TextBox13.Text.Trim()),LabelIdOBJ.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()));
        }
        private void agregarMeta()
        {
            cGestion.agregarMetas(LabelIdEstrategia.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), DropDownList3.SelectedItem.Value.ToString().Trim(), DropDownList4.SelectedItem.Value.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()));
        }
        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TbAdicionarVision.Visible = false;
            TbModificarVision.Visible = false;
            //meta
            TextBox16.Text = "";
            TextDesc.Text = "";
            TextValor.Text = "";
            TextUsuario.Text = "";
            TextFechaReg.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            DropDownList3.SelectedIndex = 2;
            DropDownList4.SelectedIndex = 3;
        }

        #region Propierties

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

        private DataTable infGridOBJ;
        private DataTable InfoGridOBJ
        {
            get
            {
                infGridOBJ = (DataTable)ViewState["infGridOBJ"];
                return infGridOBJ;
            }
            set
            {
                infGridOBJ = value;
                ViewState["infGridOBJ"] = infGridOBJ;
            }
        }

        private DataTable infGridMeta;
        private DataTable InfoGridMeta
        {
            get
            {
                infGridMeta = (DataTable)ViewState["infGridMeta"];
                return infGridMeta;
            }
            set
            {
                infGridMeta = value;
                ViewState["infGridMeta"] = infGridMeta;
            }
        }

        //private DataTable infGridvision;
        //private DataTable InfoGridVision
        //{
        //    get
        //    {
        //        infGridvision = (DataTable)ViewState["infGridvision"];
        //        return infGridvision;
        //    }
        //    set
        //    {
        //        infGridvision = value;
        //        ViewState["infGridvision"] = infGridvision;
        //    }
        //}

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

        private void modificarLista()
        {
            cGestion.modificarEstrategia(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim());
        }

        private void modificarMeta()
        {
            cGestion.modificarMetas(TextDesc.Text.Trim(), InfoGridMeta.Rows[IdexRow]["IdMeta"].ToString().Trim(), DropDownList1.SelectedItem.Value.ToString().Trim(), DropDownList2.SelectedItem.Value.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextValor.Text.Trim()));
        }

        private void verModificar()
        {
            TbModificarVision.Visible = true;
        }

        private void verModificarMeta()
        {
            TbModificarMeta.Visible = true;
            TextBox16.Text = InfoGridMeta.Rows[IdexRow]["IdMeta"].ToString().Trim();
            TextDesc.Text = InfoGridMeta.Rows[IdexRow]["Descripcion"].ToString().Trim();
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Text.Trim() == InfoGridMeta.Rows[IdexRow]["Funcion"].ToString().Trim())
                {
                    break;
                }
            }
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedItem.Text.Trim() == InfoGridMeta.Rows[IdexRow]["Formato"].ToString().Trim())
                {
                    break;
                }
            }
            TextValor.Text = InfoGridMeta.Rows[IdexRow]["Valor"].ToString().Trim();
            TextUsuario.Text = InfoGridMeta.Rows[IdexRow]["Usuario"].ToString().Trim();
            TextFechaReg.Text = InfoGridMeta.Rows[IdexRow]["FechaRegistro"].ToString().Trim();
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
                case "Metas":
                    metas();
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ModificarMeta":
                    verModificarMeta();
                    //modificar();
                    break;
                case "EliminarMeta":
                    eliminarMeta();
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
                    loadGridOBJ();
                    cargarInfoGridOBJ();
                    break;
            }
        }
        private void filtrar()
        {
            FiltroAplicado.Visible = true;
            FiltroPE.Visible = false;
            FiltroOBJ.Visible = true;
        }
        private void filtrarplan()
        {
            LabelIdPlan.Text = InfoGridPlan.Rows[IdexRow]["IdPlan"].ToString().Trim();
            TextBox12.Text = InfoGridPlan.Rows[IdexRow]["FechaInicio"].ToString().Trim();
            TextBox11.Text = InfoGridPlan.Rows[IdexRow]["FechaFin"].ToString().Trim();
            LabelCodigoPlan.Text = InfoGridPlan.Rows[IdexRow]["CodigoPlan"].ToString().Trim();
            LabelNombrePlan.Text = InfoGridPlan.Rows[IdexRow]["Nombre"].ToString().Trim();
        }

        protected void GridViewOBJ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "FiltrarOBJ":
                    filtrarOBJ();
                    filtrarObjetivo();
                    break;
            }
        }
        private void filtrarOBJ()
        {
            FiltroAplicadoOBJ.Visible = true;
            FiltroOBJ.Visible = false;
            VerPlanEstrategico.Visible = false;
        }
        private void filtrarObjetivo()
        {
            LabelIdOBJ.Text = InfoGridOBJ.Rows[IdexRow]["IdObjetivo"].ToString().Trim();
            LabelCodigoOBJ.Text = InfoGridOBJ.Rows[IdexRow]["CodigoObjetivo"].ToString().Trim();
            LabelDescOBJ.Text = InfoGridOBJ.Rows[IdexRow]["Descripcion"].ToString().Trim();
            TbEstrategia.Visible = true;
            loadGrid();
            cargarInfoGrid();
        }

        protected void VerPlanEstrategico_Click(object sender, EventArgs e)
        {
            FiltroPE.Visible = true;
            loadGridPlan();
            cargarInfoGridPlan();
            FiltroAplicado.Visible = false;
            FiltroOBJ.Visible = false;
        }

        protected void VerObjetivos_Click(object sender, EventArgs e)
        {
            FiltroOBJ.Visible = true;
            loadGridOBJ();
            cargarInfoGridOBJ();
            TbEstrategia.Visible = false;
            FiltroAplicadoOBJ.Visible = false;
            VerPlanEstrategico.Visible = true;
            TbModificarVision.Visible=false;
            TbAdicionarVision.Visible = false;
        }

        private void modificar()
        {
            TextBox14.Text = InfoGrid.Rows[IdexRow]["CodigoEstrategia"].ToString().Trim();
            TextBox2.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
            TextBox7.Text = InfoGrid.Rows[IdexRow]["Usuario"].ToString().Trim();
            TextBox6.Text = InfoGrid.Rows[IdexRow]["FechaRegistro"].ToString().Trim();
            TbAdicionarVision.Visible = false;
        }

        private void FiltrarEstrategia()
        {
            LabelIdEstrategia.Text = InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim();
            Label29.Text = InfoGrid.Rows[IdexRow]["CodigoEstrategia"].ToString().Trim();
            Label31.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
            
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
                        cGestion.eliminarEstrategia(InfoGrid.Rows[IdexRow]["IdEstrategia"].ToString().Trim());
                        loadGrid();
                        cargarInfoGrid();
                        TbAdicionarVision.Visible = false;
                        TbModificarVision.Visible = false;
                        Mensaje("Estrategia eliminada correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al eliminar Estrategia. " + ex.Message);
            }
        }

        private void eliminarMeta()
        {
            try
            {
                cGestion.eliminarMetas(InfoGridMeta.Rows[IdexRow]["IdMeta"].ToString().Trim());
                loadGridMeta();
                cargarInfoGridMeta();
                TbAdicionarMeta.Visible = false;
                TbModificarMeta.Visible = false;
                Mensaje("Meta eliminada correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al eliminar Meta. " + ex.Message);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
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
                Mensaje("Estrategia almacenada correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar Estrategia. " + ex.Message);
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
                        Mensaje("Estartegia modificada correctamente.");
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

                    TbAdicionarVision.Visible = true;
                    TbModificarVision.Visible = false;
                    loadCodigo();
                    TextBox8.Text = Session["loginUsuario"].ToString().Trim();
                    TextBox9.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
        }

        private void loadCodigo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigo("IdEstrategia", "Gestion.Estrategias");
                if (dtInfo.Rows.Count > 0)
                {
                    TextBox13.Text = "ES" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                }
                else
                {
                    TextBox13.Text = "ES1";
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar código. " + ex.Message);
            }
        }

        protected void BtnAdicionaMeta_Click(object sender, ImageClickEventArgs e)
        {

        }

        private void metas()
        {
            VerObjetivos.Visible = false;
            TbEstrategia.Visible = false;
            TbFiltroEstrategia.Visible = true;
            FiltrarEstrategia();
            TbMetas.Visible = true;
            loadGridMeta();
            cargarInfoGridMeta();
            inicializarValores();
        }

        protected void BtnCerrarMeta_Click(object sender, ImageClickEventArgs e)
        {
            TbMetas.Visible = false;
            TbFiltroEstrategia.Visible = false;
            TbEstrategia.Visible = true;
            VerObjetivos.Visible = true;
            loadGrid();
            cargarInfoGrid();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnCancelaModiMeta_Click(object sender, ImageClickEventArgs e)
        {
            TbModificarMeta.Visible = false;
        }

        protected void BtnModificaMeta_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                modificarMeta();
                loadGridMeta();
                cargarInfoGridMeta();
                resetValues();
                TbModificarMeta.Visible = false;
                Mensaje("Meta modificada correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al modificar la Meta. " + ex.Message);
            }
        }

        protected void GridViewPlanEstratagico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void VerEstrategias_Click(object sender, EventArgs e)
        {
            TbFiltroEstrategia.Visible = false;
            TbEstrategia.Visible = true;
            loadGrid();
            cargarInfoGrid();
            resetValues();
            VerObjetivos.Visible = true;
            TbMetas.Visible = false;
       }

        private void loadDDLRolAdd()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.LFormatos();

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

        private void loadDDLRolAddAdd()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.LFormatos();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList4.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar Perspectivas. " + ex.Message);
            }

        }
        
        private void loadDDLRolAddFu()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.LFuncion();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar Perspectivas. " + ex.Message);
            }
            
        }

        private void loadDDLRolAddFuAdd()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.LFuncion();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList3.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar Perspectivas. " + ex.Message);
            }

        }

        protected void BtnVerAdicionaMeta_Click(object sender, ImageClickEventArgs e)
        {
            TbAdicionarMeta.Visible = true;
            loadCodigometa();
        }

        private void loadCodigometa()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigo("IdMeta", "Gestion.Metas");
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

        protected void BtnCancelarAddMeta_Click(object sender, ImageClickEventArgs e)
        {
            TbAdicionarMeta.Visible = false;
            resetValues();
        }

        protected void BtnAdicionarMeta_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                agregarMeta();
                loadGridMeta();
                cargarInfoGridMeta();
                TbAdicionarMeta.Visible = false;
                inicializarValores();
                resetValues();
                Mensaje("Meta almacenada correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar Meta. " + ex.Message);
            }
        }
    }
}