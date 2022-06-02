using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.Sarlaft
{
    public partial class FactorRiesgoPerfilSegmento : System.Web.UI.UserControl
    {
        cSegmentacion cSegmentacion = new cSegmentacion();
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "39";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadGridFactorRiesgo();
                loadInfoFactorRiesgo();                
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridFactorRiesgo = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    cargarSenalesAlerta();
                    break;
            }
        }

        private void cargarSenalesAlerta()
        {
            loadGridPerfilSegmento();
            loadInfoPerfilSegmento();
            tbPerfilSegmento.Visible = true;
        }

        private void loadInfoFactorRiesgo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cSegmentacion.loadInfoFactorRiesgo();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridFactorRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdFactorRiesgo"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Indicador"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                                });
                }
                GridView1.DataSource = InfoGridFactorRiesgo;
                GridView1.DataBind();
            }
        }

        private void loadInfoPerfilSegmento()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cSegmentacion.loadInfoSenalAlerta(InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["IdFactorRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPerfilSegmento.Rows.Add(new Object[] {dtInfo.Rows[rows]["FactorRiesgo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Segmento"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["TipoSegmento"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Atributo"].ToString().Trim(),                                                            
                                                                  dtInfo.Rows[rows]["PerfilSegmento"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["SenalAlerta"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Indicador"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["MensajeSenalAlerta"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreAtributo1"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreRango1"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreAtributo2"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreRango2"].ToString().Trim()                                                                 
                                                                 });
                }
                GridView2.DataSource = InfoGridPerfilSegmento;
                GridView2.DataBind();
            }
        }

        private void loadGridFactorRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdFactorRiesgo", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Indicador", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGridFactorRiesgo = grid;
            GridView1.DataSource = InfoGridFactorRiesgo;
            GridView1.DataBind();
        }

        private void loadGridPerfilSegmento()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("FactorRiesgo", typeof(string));
            grid.Columns.Add("Segmento", typeof(string));
            grid.Columns.Add("TipoSegmento", typeof(string));
            grid.Columns.Add("Atributo", typeof(string));
            grid.Columns.Add("PerfilSegmento", typeof(string));
            grid.Columns.Add("SenalAlerta", typeof(string));
            grid.Columns.Add("Indicador", typeof(string));
            grid.Columns.Add("MensajeSenalAlerta", typeof(string));
            grid.Columns.Add("NombreAtributo1", typeof(string));
            grid.Columns.Add("NombreRango1", typeof(string));
            grid.Columns.Add("NombreAtributo2", typeof(string));
            grid.Columns.Add("NombreRango2", typeof(string));
            InfoGridPerfilSegmento = grid;
            GridView2.DataSource = InfoGridPerfilSegmento;
            GridView2.DataBind();
        }
        
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties

        private DataTable infoGridFactorRiesgo;
        private DataTable InfoGridFactorRiesgo
        {
            get
            {
                infoGridFactorRiesgo = (DataTable)ViewState["infoGridFactorRiesgo"];
                return infoGridFactorRiesgo;
            }
            set
            {
                infoGridFactorRiesgo = value;
                ViewState["infoGridFactorRiesgo"] = infoGridFactorRiesgo;
            }
        }
        
        private int rowGridFactorRiesgo;
        private int RowGridFactorRiesgo
        {
            get
            {
                rowGridFactorRiesgo = (int)ViewState["rowGridFactorRiesgo"];
                return rowGridFactorRiesgo;
            }
            set
            {
                rowGridFactorRiesgo = value;
                ViewState["rowGridFactorRiesgo"] = rowGridFactorRiesgo;
            }
        }

        private DataTable infoGridPerfilSegmento;
        private DataTable InfoGridPerfilSegmento
        {
            get
            {
                infoGridPerfilSegmento = (DataTable)ViewState["infoGridPerfilSegmento"];
                return infoGridPerfilSegmento;
            }
            set
            {
                infoGridPerfilSegmento = value;
                ViewState["infoGridPerfilSegmento"] = infoGridPerfilSegmento;
            }
        }

        #endregion
    }
}