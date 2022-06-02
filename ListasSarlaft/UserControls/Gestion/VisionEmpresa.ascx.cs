using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class VisionEmpresa : System.Web.UI.UserControl
    {
        
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "7002";
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
            grid.Columns.Add("IdVision", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Justificacion", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaConsulta", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.VisionEmpresa();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdVision"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Justificacion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaConsulta"].ToString().Trim(),
                                                    });
                }
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void agregarLista()
        {
            cGestion.agregarVision(Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()));
        }

        private void resetValues()
        {
            TbModificarVision.Visible = false;
            TextBox2.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
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
            agregarLista();
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            resetValues();
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
            }
        }
        private void modificar()
        {
            //Label13.Text = InfoGrid.Rows[IdexRow]["IdVision"].ToString().Trim();
            TextBox5.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
            TextBox2.Text = InfoGrid.Rows[IdexRow]["Justificacion"].ToString().Trim();
            TextBox7.Text = InfoGrid.Rows[IdexRow]["Usuario"].ToString().Trim();
            TextBox6.Text = InfoGrid.Rows[IdexRow]["FechaConsulta"].ToString().Trim();
            TextBox5.Focus();
        }
        
        protected void BtnAdicionaVision_Click(object sender, ImageClickEventArgs e)
        {
            TbModificarVision.Visible = false;
        }
        protected void BtnGuardaVision_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                agregarLista();
                loadGrid();
                cargarInfoGrid();
                resetValues();
                Mensaje("Visión de la Empresa almacenada correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al guardar la visión de la empresa. " + ex.Message);
            }
        }
        protected void BtnCancelaVision_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
        }
        protected void BtnModificaVision_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    modificarLista();
                    loadGrid();
                    cargarInfoGrid();
                    resetValues();
                    Mensaje("Visión de la Empresa modificada correctamente.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar la visión de la empresa. " + ex.Message);
            }
        }
        protected void BtnCancelaMod_Click(object sender, ImageClickEventArgs e)
        {
            TbModificarVision.Visible = false;
        }
    }
}