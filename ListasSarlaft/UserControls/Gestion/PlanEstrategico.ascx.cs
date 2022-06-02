using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class PlanEstrategico : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "7003";
        private cGestion cGestion = new cGestion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!IsPostBack)
            {
                loadGrid();
                cargarInfoGrid();
                loadGridVision();
                cargarInfoGridVision();
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
            grid.Columns.Add("IdPlan", typeof(string));
            grid.Columns.Add("CodigoPlan", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFin", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridViewPlan.DataSource = grid;
            GridViewPlan.DataBind();
            InfoGrid = grid;
        }

        private void loadGridVision()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Descripcion", typeof(string));
            GridViewVision.DataSource = grid;
            GridViewVision.DataBind();
            InfoGridVision = grid;
        }
                
        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.PlanEstrategico();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlan"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoPlan"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaFin"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridViewPlan.DataSource = InfoGrid;
                GridViewPlan.DataBind();
            }
        }

        private void cargarInfoGridVision()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.VerVision();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridVision.Rows.Add(new Object[] {dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    });
                }
                GridViewVision.DataSource = InfoGridVision;
                GridViewVision.DataBind();
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

        private void agregarPlanEstrategico()
        {
            cGestion.agregarPlan(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()) + " 12:00:00:000");
        }

        private void resetValues()
        {
            TbAdicionaPlan.Visible = false;
            TbModificarPlan.Visible = false;
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
            TextBox11.Text = "";
            TextBox12.Text = "";
        }
        #region Propierties

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

        private DataTable infGridvision;
        private DataTable InfoGridVision
        {
            get
            {
                infGridvision = (DataTable)ViewState["infGridvision"];
                return infGridvision;
            }
            set
            {
                infGridvision = value;
                ViewState["infGridvision"] = infGridvision;
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

        #endregion

        private void modificarLista()
        {
            agregarPlanEstrategico();
        }

        private void verModificar()
        {
            TbModificarPlan.Visible = true;
            TbAdicionaPlan.Visible = false;
        }

        protected void GridViewPlan_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    cGestion.eliminarPlan(InfoGrid.Rows[IdexRow]["IdPlan"].ToString().Trim());
                    loadGrid();
                    cargarInfoGrid();
                    Mensaje("Plan Estratégico eliminado correctamente.");
                    TbAdicionaPlan.Visible = false;
                    TbModificarPlan.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al eliminar Plan Estratégico. " + ex.Message);
            }
        }

        private void modificar()
        {
            TextBox12.Text = InfoGrid.Rows[IdexRow]["CodigoPlan"].ToString().Trim();
            TextBox2.Text = InfoGrid.Rows[IdexRow]["Nombre"].ToString().Trim();
            TextBox5.Text = InfoGrid.Rows[IdexRow]["FechaInicio"].ToString().Trim();
            TextBox10.Text = InfoGrid.Rows[IdexRow]["FechaFin"].ToString().Trim();
            TextBox7.Text = InfoGrid.Rows[IdexRow]["Usuario"].ToString().Trim();
            TextBox6.Text = InfoGrid.Rows[IdexRow]["FechaRegistro"].ToString().Trim();
            TextBox2.Focus();
        }
        
        protected void BtnGuardaPlan_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                agregarPlanEstrategico();
                loadGrid();
                cargarInfoGrid();
                resetValues();
                Mensaje("Plan Estratégico almacenado correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al guardar el Plan Estratégico. " + ex.Message);
            }
        }

        protected void GridViewPlan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnAdicionaPlan_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                TbModificarPlan.Visible = false;
                TbAdicionaPlan.Visible = true;
                loadCodigo();
                TextBox1.Focus();
                TextBox8.Text = Session["loginUsuario"].ToString().Trim();
                TextBox9.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            }

        }
        private void loadCodigo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigo("IdPlan", "Gestion.PlanEstrategico");
                if (dtInfo.Rows.Count > 0)
                {
                    TextBox11.Text = "PE" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                }
                else
                {
                    TextBox11.Text = "PE1";
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar código. " + ex.Message);
            }
        }

        protected void BtnCalcelModPlan_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
        }

        protected void BtnCancelaPlan_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
        }

        private void modificarPlanEstrategico()
        {
            cGestion.modificarPlan(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()) + " 12:00:00:000", Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()) + " 12:00:00:000", InfoGrid.Rows[IdexRow]["IdPlan"].ToString().Trim());
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
                    modificarPlanEstrategico();
                    loadGrid();
                    cargarInfoGrid();
                    resetValues();
                    Mensaje("Plan Estratégico modificado correctamente.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el Plan Estratégico." + ex.Message);
            }

        }
    }
}