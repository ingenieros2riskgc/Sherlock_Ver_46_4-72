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
namespace ListasSarlaft.UserControls.Gestion
{
    public partial class CerrarPeriodo : System.Web.UI.UserControl
    {
        
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "7011";
        String Mes;
        String Ano;
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
            grid.Columns.Add("IdPeriodo", typeof(string));
            grid.Columns.Add("Mes", typeof(string));
            grid.Columns.Add("Ano", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.PeriodosCerrados();
            //String Mes;
            Mes = "";
            if (dtInfo.Rows.Count > 0)
            {
                
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    switch (dtInfo.Rows[rows]["Mes"].ToString().Trim())
                    {
                        case "1":
                            Mes = "Enero";
                            break;
                        case "2":
                            Mes = "Febrero";
                            break;
                        case "3":
                            Mes = "Marzo";
                            break;
                        case "4":
                            Mes = "Abril";
                            break;
                        case "5":
                            Mes = "Mayo";
                            break;
                        case "6":
                            Mes = "Junio";
                            break;
                        case "7":
                            Mes = "Julio";
                            break;
                        case "8":
                            Mes = "Agosto";
                            break;
                        case "9":
                            Mes = "Septiembre";
                            break;
                        case "10":
                            Mes = "Octubre";
                            break;
                        case "11":
                            Mes = "Noviembre";
                            break;
                        case "12":
                            Mes = "Diciembre";
                            break;
                    }
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPeriodo"].ToString().Trim(),
                                                    Mes,
                                                    dtInfo.Rows[rows]["Ano"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
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

        private void Eliminar()
        {
            try
            {
                cGestion.eliminarPeriodoCerrado(InfoGrid.Rows[IdexRow]["IdPeriodo"].ToString().Trim());
                loadGrid();
                cargarInfoGrid();
                Mensaje("Periodo Cerrado, eliminado correctamente");
            }
            catch (Exception ex)
            {
                Mensaje("Error al eliminar periodo" + ex.Message);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Eliminar":
                    Eliminar();
                    break;
            }
        }

        protected void BtnAddPeriodo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //CAmilo
                Ano = DropDownListAno.SelectedValue.ToString().Trim();
                Mes = DropDownListMes.SelectedItem.Text.ToString().Trim();
                cGestion.agregarPeriodoCerrado(DropDownListMes.SelectedValue.ToString().Trim(), DropDownListAno.SelectedValue.ToString());
                loadGrid();
                cargarInfoGrid();
                TbAddPeriodo.Visible = false;
                DropDownListAno.SelectedIndex = 0;
                DropDownListMes.SelectedIndex = 0;
                Mensaje("Periodo | " + Mes + " - " + Ano + " | cerrado correctamente.");
            }
            catch
            {
                Mensaje("El periodo | " + Mes + " - " + Ano + " | ya está cerrado. Valide e intente nuevamente");
            }
        }

        protected void BtnNoAddPeriodo_Click(object sender, ImageClickEventArgs e)
        {
            TbAddPeriodo.Visible = false;
        }

        protected void BtnVerPeriodo_Click(object sender, ImageClickEventArgs e)
        {
            TbAddPeriodo.Visible = true;
            TextBox1.Text = Session["loginUsuario"].ToString().Trim();
            TextBox2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}