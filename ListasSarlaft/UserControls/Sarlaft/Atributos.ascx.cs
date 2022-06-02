using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls
{
    public partial class Atributos : System.Web.UI.UserControl
    {
        private cAtributos cAtributos = new cAtributos();
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "38";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadDDLUnidades();
                loadGridAtributos();
                loadInfoGridAtributos();
            }
        }

        private void loadDDLUnidades()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cAtributos.loadDDLUnidades();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreUnidad"].ToString().Trim(), dtInfo.Rows[i]["IdUnidad"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar las unidades. " + ex.Message);
            }
        }

        private void loadInfoGridAtributos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAtributos.consultarAtributos();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAtributos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdAtributos"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreAtributo"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),                                                            
                                                             dtInfo.Rows[rows]["NombreUnidad"].ToString().Trim(),
                                                            });
                }
                GridView1.DataSource = InfoGridAtributos;
                GridView1.DataBind();
            }
        }

        private void loadInfoGridRangosAtributos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAtributos.consultarRangosAtributos(InfoGridAtributos.Rows[RowGridAtributos]["IdAtributos"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRangosAtributos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRangosAtributo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreAtributo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreRango"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["ValorInferior"].ToString().Trim(),                                                            
                                                                   dtInfo.Rows[rows]["ValorSuperior"].ToString().Trim(),
                                                                  });
                }
                GridView2.DataSource = InfoGridRangosAtributos;
                GridView2.DataBind();
            }
        }

        private void loadGridRangosAtributos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRangosAtributo", typeof(string));
            grid.Columns.Add("NombreAtributo", typeof(string));
            grid.Columns.Add("NombreRango", typeof(string));
            grid.Columns.Add("ValorInferior", typeof(string));
            grid.Columns.Add("ValorSuperior", typeof(string));
            InfoGridRangosAtributos = grid;
            GridView2.DataSource = InfoGridRangosAtributos;
            GridView2.DataBind();    
        }

        private void loadGridAtributos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdAtributos", typeof(string));
            grid.Columns.Add("NombreAtributo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("NombreUnidad", typeof(string));
            InfoGridAtributos = grid;
            GridView1.DataSource = InfoGridAtributos;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridAtributos = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    seleccionarAtributos();
                    break;
                case "Modificar":
                    modificarAtributos();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                    {
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    }
                    else
                    {
                        borrarAtributos();
                    }                    
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridRangosAtributos = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    modificarRangosAtributos();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                    {
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    }
                    else
                    {
                        borrarRangosAtributos();
                    }                    
                    break;
            }
        }

        private void seleccionarAtributos()
        {
            resetValuesAtributos();
            resetValuesRangosAtributos();
            trRangosAtributos.Visible = true;
            tbGridRangosAtributos.Visible = true;
            loadGridRangosAtributos();
            loadInfoGridRangosAtributos();
        }

        private void modificarAtributos()
        {
            resetValuesAtributos();
            resetValuesRangosAtributos();
            tbCamposAtributos.Visible = true;
            tbBotonesAtributos.Visible = true;
            ImageButton3.Visible = true;
            TextBox1.Text = InfoGridAtributos.Rows[RowGridAtributos]["NombreAtributo"].ToString().Trim();
            TextBox2.Text = InfoGridAtributos.Rows[RowGridAtributos]["Descripcion"].ToString().Trim();
        }

        private void modificarRangosAtributos()
        {
            resetValuesRangosAtributos();
            trRangosAtributos.Visible = true;
            tbGridRangosAtributos.Visible = true;
            tbCamposRangosAtributos.Visible = true;
            tbBotonesRangosAtributos.Visible = true;
            ImageButton7.Visible = true;
            TextBox3.Text = InfoGridRangosAtributos.Rows[RowGridRangosAtributos]["NombreRango"].ToString().Trim();
            TextBox4.Text = InfoGridRangosAtributos.Rows[RowGridRangosAtributos]["ValorInferior"].ToString().Trim();
            TextBox5.Text = InfoGridRangosAtributos.Rows[RowGridRangosAtributos]["ValorSuperior"].ToString().Trim();
        }

        private void borrarAtributos()
        {
            try
            {                
                cAtributos.eliminarAtributo(InfoGridAtributos.Rows[RowGridAtributos]["IdAtributos"].ToString().Trim());
                resetValuesAtributos();
                resetValuesRangosAtributos();
                loadGridAtributos();
                loadInfoGridAtributos();
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar el atributo. " + ex.Message);
            }
        }

        private void borrarRangosAtributos()
        {
            try
            {
                cAtributos.eliminarRangosAtributos(InfoGridRangosAtributos.Rows[RowGridRangosAtributos]["IdRangosAtributo"].ToString().Trim());
                resetValuesRangosAtributos();
                trRangosAtributos.Visible=true;
                tbGridRangosAtributos.Visible=true;
                loadGridRangosAtributos();
                loadInfoGridRangosAtributos();
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar el rango atributo. " + ex.Message);
            }
        }

        private void resetValuesAtributos()
        {
            tbGridAtributos.Visible = true;
            tbCamposAtributos.Visible = false;
            tbBotonesAtributos.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            TextBox1.Text = "";
            TextBox2.Text = "";
            DropDownList1.SelectedIndex = 0;
        }

        private void resetValuesRangosAtributos()
        {
            trRangosAtributos.Visible = false;
            tbGridRangosAtributos.Visible = false;
            tbCamposRangosAtributos.Visible = false;
            tbBotonesRangosAtributos.Visible = false;
            ImageButton6.Visible = false;
            ImageButton7.Visible = false;
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties

        private DataTable infoGridAtributos;
        private DataTable InfoGridAtributos
        {
            get
            {
                infoGridAtributos = (DataTable)ViewState["infoGridAtributos"];
                return infoGridAtributos;
            }
            set
            {
                infoGridAtributos = value;
                ViewState["infoGridAtributos"] = infoGridAtributos;
            }
        }

        private int rowGridAtributos;
        private int RowGridAtributos
        {
            get
            {
                rowGridAtributos = (int)ViewState["rowGridAtributos"];
                return rowGridAtributos;
            }
            set
            {
                rowGridAtributos = value;
                ViewState["rowGridAtributos"] = rowGridAtributos;
            }
        }

        private DataTable infoGridRangosAtributos;
        private DataTable InfoGridRangosAtributos
        {
            get
            {
                infoGridRangosAtributos = (DataTable)ViewState["infoGridRangosAtributos"];
                return infoGridRangosAtributos;
            }
            set
            {
                infoGridRangosAtributos = value;
                ViewState["infoGridRangosAtributos"] = infoGridRangosAtributos;
            }
        }

        private int rowGridRangosAtributos;
        private int RowGridRangosAtributos
        {
            get
            {
                rowGridRangosAtributos = (int)ViewState["rowGridRangosAtributos"];
                return rowGridRangosAtributos;
            }
            set
            {
                rowGridRangosAtributos = value;
                ViewState["rowGridRangosAtributos"] = rowGridRangosAtributos;
            }
        }

        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                resetValuesAtributos();
                resetValuesRangosAtributos();
                tbGridAtributos.Visible = false;
                tbCamposAtributos.Visible = true;
                tbBotonesAtributos.Visible = true;
                ImageButton2.Visible = true;
            }            
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                resetValuesRangosAtributos();
                trRangosAtributos.Visible = true;
                tbGridRangosAtributos.Visible = false;
                tbCamposRangosAtributos.Visible = true;
                tbBotonesRangosAtributos.Visible = true;
                ImageButton6.Visible = true;
            }            
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    //Insetar atributo
                    cAtributos.registrarAtributo(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), DropDownList1.SelectedValue.ToString().Trim());
                    resetValuesAtributos();
                    resetValuesRangosAtributos();
                    loadGridAtributos();
                    loadInfoGridAtributos();
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar el atributo. " + ex.Message);
            }
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {//Insertar rango atributo
                    cAtributos.registrarRangoAtributos(InfoGridAtributos.Rows[RowGridAtributos]["IdAtributos"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()));
                    resetValuesRangosAtributos();
                    trRangosAtributos.Visible = true;
                    tbGridRangosAtributos.Visible = true;
                    loadGridRangosAtributos();
                    loadInfoGridRangosAtributos();
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar el rango atributo. " + ex.Message);
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    //Modificar atributo
                    cAtributos.updateAtributos(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), DropDownList1.SelectedValue.ToString().Trim(), InfoGridAtributos.Rows[RowGridAtributos]["IdAtributos"].ToString().Trim());
                    resetValuesAtributos();
                    resetValuesRangosAtributos();
                    loadGridAtributos();
                    loadInfoGridAtributos();
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el atributo." + ex.Message);
            }
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    //Modificar rango atributo
                    cAtributos.updateRangoAtributos(Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()), InfoGridRangosAtributos.Rows[RowGridRangosAtributos]["IdRangosAtributo"].ToString().Trim());
                    resetValuesRangosAtributos();
                    trRangosAtributos.Visible = true;
                    tbGridRangosAtributos.Visible = true;
                    loadGridRangosAtributos();
                    loadInfoGridRangosAtributos();
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el rango atributo. " + ex.Message);
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            //Cancelar atributo
            resetValuesAtributos();
            resetValuesRangosAtributos();
            loadGridAtributos();
            loadInfoGridAtributos();
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            //Cancelar rango atributo
            resetValuesRangosAtributos();
            trRangosAtributos.Visible = true;
            tbGridRangosAtributos.Visible = true;
            loadGridRangosAtributos();
            loadInfoGridRangosAtributos();
        }
    }
}