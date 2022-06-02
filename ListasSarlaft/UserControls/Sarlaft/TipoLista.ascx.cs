using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls
{
    public partial class TipoLista : System.Web.UI.UserControl
    {
        private cListas cListas = new cListas();

        protected void Page_Load(object sender, EventArgs e)
        {
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
            grid.Columns.Add("IdClaseLista", typeof(string));
            grid.Columns.Add("CodigoClaseLisa", typeof(string));
            grid.Columns.Add("NombreClaseLista", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cListas.listaListas();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdClaseLista"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoClaseLisa"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["NombreClaseLista"].ToString().Trim()
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                agregarLista();
                loadGrid();
                cargarInfoGrid();
                resetValues();
                Mensaje("Lista registrada con éxito.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar la información. " + ex.Message); 
            }
        }

        private void agregarLista()
        {
            cListas.agregarLista(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            resetValues();
        }

        private void resetValues()
        {
            TextBox1.Text = "";            
            tbLista.Visible = true;
            tbModifica.Visible = false;
            TextBox2.Text = "";
            Label4.Text= "";            
        }

        #region Propierties

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

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                modificarLista();
                loadGrid();
                cargarInfoGrid();
                resetValues();
                Mensaje("Lista modificada con éxito.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar la información. " + ex.Message);
            }
        }

        private void modificarLista()
        {
            cListas.modificarLista(InfoGrid.Rows[IdexRow]["IdClaseLista"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()));
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            resetValues();            
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    modificar();
                    break;
            }
        }

        private void modificar()
        {
            Label4.Text = InfoGrid.Rows[IdexRow]["CodigoClaseLisa"].ToString().Trim();
            TextBox2.Text = InfoGrid.Rows[IdexRow]["NombreClaseLista"].ToString().Trim();
            tbLista.Visible = false;
            tbModifica.Visible = true;
        }
    }
}