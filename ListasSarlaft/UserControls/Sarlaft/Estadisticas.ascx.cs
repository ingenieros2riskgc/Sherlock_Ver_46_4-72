using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls
{
    public partial class Estadisticas : System.Web.UI.UserControl
    {
        private cSegmentacion cSegmentacion = new cSegmentacion();
        private cEstadisticas cEstadisticas = new cEstadisticas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlFactorRiesgo();
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties

        private int rowGridROI;
        private int RowGridROI
        {
            get
            {
                rowGridROI = (int)ViewState["rowGridROI"];
                return rowGridROI;
            }
            set
            {
                rowGridROI = value;
                ViewState["rowGridROI"] = rowGridROI;
            }
        }

        private DataTable infoGridROI;
        private DataTable InfoGridROI
        {
            get
            {
                infoGridROI = (DataTable)ViewState["infoGridROI"];
                return infoGridROI;
            }
            set
            {
                infoGridROI = value;
                ViewState["infoGridROI"] = infoGridROI;
            }
        }

        private int rowGridCliente;
        private int RowGridCliente
        {
            get
            {
                rowGridCliente = (int)ViewState["rowGridCliente"];
                return rowGridCliente;
            }
            set
            {
                rowGridCliente = value;
                ViewState["rowGridCliente"] = rowGridCliente;
            }
        }

        private DataTable infoGridCliente;
        private DataTable InfoGridCliente
        {
            get
            {
                infoGridCliente = (DataTable)ViewState["infoGridCliente"];
                return infoGridCliente;
            }
            set
            {
                infoGridCliente = value;
                ViewState["infoGridCliente"] = infoGridCliente;
            }
        }

        #endregion

        private void loadGridROI()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("NumeroRegistros", typeof(string));
            grid.Columns.Add("Segmento", typeof(string));
            InfoGridROI = grid;
            GridView1.DataSource = InfoGridROI;
            GridView1.DataBind();
        }

        private void loadGridCliente()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("NumeroRegistros", typeof(string));
            grid.Columns.Add("TipoPersona", typeof(string));            
            grid.Columns.Add("Inusualidad", typeof(string));
            grid.Columns.Add("ValueTipoPersona", typeof(string));            
            grid.Columns.Add("ValueInusualidad", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            InfoGridCliente = grid;
            GridView2.DataSource = InfoGridCliente;
            GridView2.DataBind();
        }

        private void verRegistrosConsultaCliente()
        {
            Response.Redirect("~/Formularios/Sarlaft/Admin/ConsultarFormCliente.aspx?ValueTipoPersona=" + InfoGridCliente.Rows[RowGridCliente]["ValueTipoPersona"].ToString().Trim() + "&ValueInusualidad=" + InfoGridCliente.Rows[RowGridCliente]["ValueInusualidad"].ToString().Trim() + "&Estado=" + InfoGridCliente.Rows[RowGridCliente]["Estado"].ToString().Trim());
        }

        private void verRegistrosConsultaROI()
        {
            Response.Redirect("~/Formularios/Sarlaft/Admin/AdminROI.aspx?Segmento=" + InfoGridROI.Rows[RowGridROI]["Segmento"].ToString().Trim());
        }

        private void cargarInfoGridCliente()
        {
            DataTable dtInfo = new DataTable();
            if(DropDownList9.SelectedValue.ToString().Trim() == "0")
            {
                dtInfo = cEstadisticas.conteoInusualidadFCPN(DropDownList7.SelectedValue.ToString().Trim(), DropDownList8.SelectedValue.ToString().Trim());
            }
            else
            {
                dtInfo = cEstadisticas.conteoInusualidadFCPJ(DropDownList7.SelectedValue.ToString().Trim(), DropDownList8.SelectedValue.ToString().Trim());
            }
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridCliente.Rows.Add(new Object[] { dtInfo.Rows[rows]["NumeroRegistros"].ToString().Trim(),
                                                            DropDownList9.SelectedItem.ToString().Trim(),                                                            
                                                            DropDownList7.SelectedItem.ToString().Trim(),                                                            
                                                            DropDownList9.SelectedValue.ToString().Trim(),                                                            
                                                            DropDownList7.SelectedValue.ToString().Trim(),                                                            
                                                            DropDownList8.SelectedValue.ToString().Trim()
                                                          });
                }
                GridView2.DataSource = InfoGridCliente;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        private void cargarInfoGridROI()
        {
            DataTable dtInfo = new DataTable();
            String Segmento = string.Empty;
            if (DropDownList2.SelectedItem.ToString().Trim() != "---")
            {
                Segmento += "Factor de riesgo: " + DropDownList2.SelectedItem.ToString().Trim();
            }
            if (DropDownList3.SelectedItem.ToString().Trim() != "---")
            {
                Segmento += "|Segmento: " + DropDownList3.SelectedItem.ToString().Trim();
            }
            if (DropDownList4.SelectedItem.ToString().Trim() != "---")
            {
                Segmento += "|Tipo segmento: " + DropDownList4.SelectedItem.ToString().Trim();
            }
            if (DropDownList5.SelectedItem.ToString().Trim() != "---")
            {
                Segmento += "|Atributo: " + DropDownList5.SelectedItem.ToString().Trim();
            }
            if (DropDownList6.SelectedItem.ToString().Trim() != "---")
            {
                Segmento += "|Clasificación: " + DropDownList6.SelectedItem.ToString().Trim(); ;
            }
            dtInfo = cEstadisticas.conteoROIEstudio(Segmento);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridROI.Rows.Add(new Object[] { dtInfo.Rows[rows]["NumeroRegistros"].ToString().Trim(),
                                                        dtInfo.Rows[rows]["Segmento"].ToString().Trim()
                                                      });
                }
                GridView1.DataSource = InfoGridROI;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        private void resetValuesROI()
        {
            tbROI.Visible = false;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "---"));
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "---"));
        }

        private void resetValuesCliente()
        {
            tbCliente.Visible = false;
            DropDownList7.SelectedIndex = 0;
            DropDownList8.SelectedIndex = 0;
            DropDownList9.SelectedIndex = 0;            
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridROI = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            { 
                case "Ver":
                    verRegistrosConsultaROI();
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridCliente = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verRegistrosConsultaCliente();
                    break;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(DropDownList1.SelectedValue.ToString().Trim())
            {
                case "0":
                    resetValuesROI();
                    resetValuesCliente();
                    break;
                case "1":
                    resetValuesROI();
                    resetValuesCliente();
                    tbROI.Visible = true;
                    break;
                case "2":
                    resetValuesROI();
                    resetValuesCliente();
                    tbCliente.Visible = true;
                    break;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                loadGridROI();
                cargarInfoGridROI();
            }
            catch(Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            resetValuesROI();
            resetValuesCliente();
            tbROI.Visible = true;
            loadGridROI();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "---"));
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList2.SelectedValue.ToString().Trim() != "---")
            {
                ddlSegmento(DropDownList2.SelectedValue.ToString().Trim());
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList3.SelectedValue.ToString().Trim() != "---")
            {
                ddlTipoSegmento(DropDownList3.SelectedValue.ToString().Trim());
            }
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList4.SelectedValue.ToString().Trim() != "---")
            {
                ddlAtributo(DropDownList4.SelectedValue.ToString().Trim());
            }
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList5.SelectedValue.ToString().Trim() != "---")
            {
                ddlClasificacion(DropDownList5.SelectedValue.ToString().Trim());
            }
        }

        private void ddlFactorRiesgo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cSegmentacion.loadInfoFactorRiesgo();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los factores de riesgo. " + ex.Message);
            }
        }

        private void ddlSegmento(String IdFactorRiesgo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cSegmentacion.loadInfoSegmento(IdFactorRiesgo);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList3.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdSegmento"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los segmentos. " + ex.Message);
            }
        }

        private void ddlTipoSegmento(String IdSegmento)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cSegmentacion.loadInfoTipoSegmento(IdSegmento);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList4.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdTipoSegmento"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los tipos de segmento. " + ex.Message);
            }
        }

        private void ddlAtributo(String IdTipoSegmento)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cSegmentacion.loadInfoAtributo(IdTipoSegmento);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList5.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdAtributo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los atributos. " + ex.Message);
            }
        }

        private void ddlClasificacion(String IdAtributo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                //dtInfo = cSegmentacion.loadInfoClasificacion(IdAtributo);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList6.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar la clasificación. " + ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                loadGridCliente();
                cargarInfoGridCliente();
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            resetValuesROI();
            resetValuesCliente();
            tbCliente.Visible = true;
            loadGridCliente();
        }
    }
}