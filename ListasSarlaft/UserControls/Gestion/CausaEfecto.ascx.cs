using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;


namespace ListasSarlaft.UserControls.Gestion
{
    public partial class CausaEfecto : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "7005";
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
            }
        }


        private void inicializarValores()
        {
            IdexRow = 0;
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdObjetivo", typeof(string));
            grid.Columns.Add("CodigoObjetivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGrid = grid;
        }

        private void loadGridEfecto()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdObjetivo", typeof(string));
            grid.Columns.Add("CodigoObjetivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGridEfecto = grid;
        }

        private void loadGridCausaEfecto()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlan", typeof(string));
            grid.Columns.Add("IdCausaEfecto", typeof(string));
            grid.Columns.Add("IdObjetivoCausa", typeof(string));
            grid.Columns.Add("CodigoObjetivoC", typeof(string));
            grid.Columns.Add("DescripcionC", typeof(string));
            grid.Columns.Add("IdObjetivoEfecto", typeof(string));
            grid.Columns.Add("CodigoObjetivoE", typeof(string));
            grid.Columns.Add("DescripcionE", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            InfoGridCausaEfecto = grid;
        }

        private void cargarInfoGridCausaEfecto()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.CausaEfecto(LabelIdPlan.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridCausaEfecto.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlan"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["IdCausaEfecto"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["IdObjetivoCausa"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["CodigoObjetivoC"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["DescripcionC"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["IdObjetivoEfecto"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["CodigoObjetivoE"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["DescripcionE"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                               });
                }
            }
            GridViewCausaEfecto.DataSource = InfoGridCausaEfecto;
            GridViewCausaEfecto.DataBind();
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
                                                    });
                }
            }
            GridViewCausa.DataSource = InfoGrid;
            GridViewCausa.DataBind();
        }

        private void cargarInfoGridEfecto()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.ObjEstrategico(LabelIdPlan.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridEfecto.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdObjetivo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoObjetivo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    });
                }
            }
            GridViewEfecto.DataSource = InfoGridEfecto;
            GridViewEfecto.DataBind();
        }

        private void loadGridPlan()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlan", typeof(string));
            grid.Columns.Add("CodigoPlan", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
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

        private void Mensaje1(String Mensaje1)
        {
            lblMsgBox1.Text = Mensaje1;
            mpeMsgBox1.Show();
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

        private DataTable infGridEfecto;
        private DataTable InfoGridEfecto
        {
            get
            {
                infGridEfecto = (DataTable)ViewState["infGridEfecto"];
                return infGridEfecto;
            }
            set
            {
                infGridEfecto = value;
                ViewState["infGridEfecto"] = infGridEfecto;
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

        private DataTable infGridCausaEfecto;
        private DataTable InfoGridCausaEfecto
        {
            get
            {
                infGridCausaEfecto = (DataTable)ViewState["infGridCausaEfecto"];
                return infGridCausaEfecto;
            }
            set
            {
                infGridCausaEfecto = value;
                ViewState["infGridCausaEfecto"] = infGridCausaEfecto;
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

       protected void GridViewCausa_RowCommand(object sender, GridViewCommandEventArgs e)
       {
           IdexRow = Convert.ToInt16(e.CommandArgument);
           switch (e.CommandName)
           {
               case "FiltrarCausa":
                   filtrarCausa();
                   break;
           }
       }

       protected void GridViewEfecto_RowCommand(object sender, GridViewCommandEventArgs e)
       {
           IdexRow = Convert.ToInt16(e.CommandArgument);
           switch (e.CommandName)
           {
               case "FiltrarEfecto":
                   FiltrarEfecto();
                   break;
           }
       }

       protected void GridViewCausaEfecto_RowCommand(object sender, GridViewCommandEventArgs e)
       {
           IdexRow = Convert.ToInt16(e.CommandArgument);
           switch (e.CommandName)
           {
               case "EliminarCausaEfecto":
                   EliminarCausaEfecto();
                   break;
           }
       }

        protected void VerPlanEstrategico_Click(object sender, EventArgs e)
        {
            FiltroPE.Visible = true;
            loadGridPlan();
            cargarInfoGridPlan();
            FiltroAplicado.Visible = false;
            TbCausa.Visible = false;
            TbEfecto.Visible = false;
            TbCausaEfectoAsociados.Visible = false;
            LabelAviso.Visible = false;
            TbObjSelecionado.Visible = false;
            Label6.Text = "";
            Label8.Text = "";
        }

        private void filtrar()
        {
            FiltroAplicado.Visible = true;
            FiltroPE.Visible = false;
        }

        private void EliminarCausaEfecto()
        {
            try
            {
                if (cCuenta.permisosBorrar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (cGestion.ValidarFechaPE(LabelFechaFin.Text.Trim()) == "S")
                    {
                        Mensaje1("Plan Estratégico vencido. Solo Lectura");
                    }
                    else
                    {
                        cGestion.eliminarCausaEfecto(InfoGridCausaEfecto.Rows[IdexRow]["IdCausaEfecto"].ToString().Trim());
                        loadGridCausaEfecto();
                        cargarInfoGridCausaEfecto();
                        Mensaje("Asociación Causa y Efecto eliminada correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al eliminar Causa y Efecto." + ex.Message);
            }
        }

        private void filtrarCausa()
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                if (cGestion.ValidarFechaPE(LabelFechaFin.Text.Trim()) == "S")
                {
                    Mensaje1("Plan Estratégico vencido. Solo Lectura");
                }
                else
                {
                    TbObjSelecionado.Visible = true;
                    LabelAviso.Visible = true;
                    LabelIdObjetivoCausa.Text = InfoGrid.Rows[IdexRow]["IdObjetivo"].ToString().Trim();
                    Label6.Text = InfoGrid.Rows[IdexRow]["CodigoObjetivo"].ToString().Trim();
                    Label8.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
                    LabelAviso1.Visible = false;
                }
            }

        }

        private void filtrarplan()
        {
            LabelIdPlan.Text = InfoGridPlan.Rows[IdexRow]["IdPlan"].ToString().Trim();
            LabelCodigoPlan.Text = InfoGridPlan.Rows[IdexRow]["CodigoPlan"].ToString().Trim();
            LabelNombrePlan.Text = InfoGridPlan.Rows[IdexRow]["Nombre"].ToString().Trim();
            LabelFechaFin.Text = InfoGridPlan.Rows[IdexRow]["FechaFin"].ToString().Trim();
            TbCausa.Visible = true;
            TbEfecto.Visible = true;
            TbCausaEfectoAsociados.Visible = true;
            loadGrid();
            cargarInfoGrid();
            loadGridEfecto();
            cargarInfoGridEfecto();
            loadGridCausaEfecto();
            cargarInfoGridCausaEfecto();
        }

        private void FiltrarEfecto()
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                if (cGestion.ValidarFechaPE(LabelFechaFin.Text.Trim()) == "S")
                {
                    Mensaje1("Plan Estratégico vencido. Solo Lectura");
                }
                else
                {
                    LabelAviso.Visible = false;
                    TbObjSelecionado.Visible = false;
                    Label6.Text = "";
                    Label8.Text = "";
                    if (LabelIdObjetivoCausa.Text == "")
                    {
                        LabelAviso1.Visible = true;
                    }
                    else
                    {
                        if (LabelIdObjetivoCausa.Text == InfoGridEfecto.Rows[IdexRow]["IdObjetivo"].ToString().Trim())
                        {
                            Mensaje1("El objetivo causa y el objetivo efecto no puden ser el mismo. Valide e intente nuevamente.");
                            LabelIdObjetivoCausa.Text = "";
                        }
                        else
                        {
                            try
                            {
                                cGestion.agregarCausaEfecto(LabelIdObjetivoCausa.Text.Trim(), InfoGridEfecto.Rows[IdexRow]["IdObjetivo"].ToString().Trim(), LabelIdPlan.Text.Trim());
                                LabelIdObjetivoCausa.Text = "";
                                Mensaje("Asociación Causa y efecto creada correctamente.");
                                loadGridCausaEfecto();
                                cargarInfoGridCausaEfecto();
                            }
                            catch (Exception)
                            {
                                //Mensaje1("Error al guardar Causa y Efecto. perro. " + ex.Message);
                                Mensaje1("Ya existe una asociación del objetivo causa y el objetivo efecto seleccionados. Valide e intente nuevamente.");
                                LabelIdObjetivoCausa.Text = "";
                            }
                        }
                    }
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnBorrarCausaEfecto_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void BtnAsociarCausaEfecto_Click(object sender, ImageClickEventArgs e)
        {
            loadGridCausaEfecto();
            cargarInfoGridCausaEfecto();
        }

       
    }
}