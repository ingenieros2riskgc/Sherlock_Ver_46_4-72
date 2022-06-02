using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.AdminUsers
{
    public partial class Roles : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "1001";
        clsBLLRoles RolBLL = new clsBLLRoles();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
            else
            {
                if (cCuenta.permisosConsulta(IdFormulario) == "False")
                {
                    Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
                }
                if (!Page.IsPostBack)
                {
                    inicializarValores();
                    loadGridRoles();
                    loadInfoRoles();
                }
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGridRoles = 0;
        }

        private void loadInfoRoles()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cCuenta.RolesVer();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRoles.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRol"].ToString().Trim(),
                                                         dtInfo.Rows[rows]["NombreRol"].ToString().Trim()
                                                        });
                }
                GridView1.PageIndex = PagIndexInfoGridRoles;
                GridView1.DataSource = InfoGridRoles;
                GridView1.DataBind();
            }
        }

        private void loadGridRoles()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRol", typeof(string));
            grid.Columns.Add("NombreRol", typeof(string));
            InfoGridRoles = grid;
            GridView1.DataSource = InfoGridRoles;
            GridView1.DataBind();
        }

        #region Propierties
        private DataTable infoGridRoles;
        private DataTable InfoGridRoles
        {
            get
            {
                infoGridRoles = (DataTable)ViewState["infoGridRoles"];
                return infoGridRoles;
            }
            set
            {
                infoGridRoles = value;
                ViewState["infoGridRoles"] = infoGridRoles;
            }
        }

        private int rowGridRoles;
        private int RowGridRoles
        {
            get
            {
                rowGridRoles = (int)ViewState["rowGridRoles"];
                return rowGridRoles;
            }
            set
            {
                rowGridRoles = value;
                ViewState["rowGridRoles"] = rowGridRoles;
            }
        }

        private int pagIndexInfoGridRoles;
        private int PagIndexInfoGridRoles
        {
            get
            {
                pagIndexInfoGridRoles = (int)ViewState["pagIndexInfoGridRoles"];
                return pagIndexInfoGridRoles;
            }
            set
            {
                pagIndexInfoGridRoles = value;
                ViewState["pagIndexInfoGridRoles"] = pagIndexInfoGridRoles;
            }
        }

        private DataTable infoGridPermisos;
        private DataTable InfoGridPermisos
        {
            get
            {
                infoGridPermisos = (DataTable)ViewState["infoGridPermisos"];
                return infoGridPermisos;
            }
            set
            {
                infoGridPermisos = value;
                ViewState["infoGridPermisos"] = infoGridPermisos;
            }
        }
        #endregion

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {            
            resetValues();
        }

        private void resetValues()
        {
            trCampos.Visible = false;
            trPermisos.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            TextBox1.Text = "";
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                resetValues();
                trCampos.Visible = true;
                ImageButton2.Visible = true;
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
                    cCuenta.agregarRol(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()));
                    resetValues();
                    loadGridRoles();
                    loadInfoRoles();
                    Mensaje("Registro agregado con éxito.");
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el registro. " + ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
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
                    bool booResult = false;
                    //cCuenta.actualizarRol(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), InfoGridRoles.Rows[RowGridRoles]["IdRol"].ToString().Trim());
                    booResult = RolBLL.mtdActualizarRoles(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), InfoGridRoles.Rows[RowGridRoles]["IdRol"].ToString().Trim());
                    if (booResult == true)
                    {
                        resetValues();
                        loadGridRoles();
                        loadInfoRoles();
                        Mensaje("Registro actualizado con éxito.");
                    }
                    else
                    {
                        Mensaje("Error al actualizar el registro.");
                    }
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el registro. " + ex.Message);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridRoles = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridRoles) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    resetValues();
                    detalleRegistro();
                    break;
                case "Permisos":
                    resetValues();
                    loadGridPermisos();
                    loadInfoPermisos();
                    trPermisos.Visible = true;
                    break;
            }
        }

        private void loadGridPermisos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPermiso", typeof(string));
            grid.Columns.Add("NombreModulo", typeof(string));
            grid.Columns.Add("NombreSubModulo", typeof(string));
            grid.Columns.Add("NombreFormulario", typeof(string));
            grid.Columns.Add("Consultar", typeof(bool));
            grid.Columns.Add("Agregar", typeof(bool));
            grid.Columns.Add("Actualizar", typeof(bool));
            grid.Columns.Add("Borrar", typeof(bool));
            InfoGridPermisos = grid;
            GridView2.DataSource = InfoGridPermisos;
            GridView2.DataBind();
        }

        private void loadInfoPermisos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cCuenta.loadInfoPermisos(InfoGridRoles.Rows[RowGridRoles]["IdRol"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPermisos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPermiso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreModulo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreSubModulo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreFormulario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Consultar"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Agregar"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Actualizar"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Borrar"].ToString().Trim()
                                                           });
                }
                GridView2.DataSource = InfoGridPermisos;
                GridView2.DataBind();
            }
        }

        private void detalleRegistro()
        {
            TextBox1.Text = InfoGridRoles.Rows[RowGridRoles]["NombreRol"].ToString().Trim();
            ImageButton3.Visible = true;
            trCampos.Visible = true;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridRoles = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridRoles;
            GridView1.DataSource = InfoGridRoles;
            GridView1.DataBind();
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        GridViewRow row = GridView2.Rows[i];
                        String Consultar = "0";
                        if (((CheckBox)row.FindControl("CheckBox1")).Checked == true)
                        {
                            Consultar = "1";
                        }
                        String Agregar = "0";
                        if (((CheckBox)row.FindControl("CheckBox2")).Checked == true)
                        {
                            Agregar = "1";
                        }
                        String Actualizar = "0";
                        if (((CheckBox)row.FindControl("CheckBox3")).Checked == true)
                        {
                            Actualizar = "1";
                        }
                        String Borrar = "0";
                        if (((CheckBox)row.FindControl("CheckBox4")).Checked == true)
                        {
                            Borrar = "1";
                        }
                        cCuenta.actualizarPermisos(Consultar, Agregar, Actualizar, Borrar, InfoGridPermisos.Rows[i]["IdPermiso"].ToString().Trim());
                    }
                    resetValues();
                    Mensaje("Permisos actualizados con éxito.");
                }                
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar los permisos. " + ex.Message);
            }
        }
    }
}