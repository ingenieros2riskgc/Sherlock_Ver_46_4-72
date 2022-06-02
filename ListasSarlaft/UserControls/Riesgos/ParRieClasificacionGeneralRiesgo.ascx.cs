using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class ParRieClasificacionGeneralRiesgo : System.Web.UI.UserControl
    {
        string IdFormulario = "5001";
        cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
        cRiesgo cRiesgo = new cRiesgo();
        cCuenta cCuenta = new cCuenta();
        clsDTOParaCalificacionRiesgoGeneral CalificacionRiesgoGeneral = new clsDTOParaCalificacionRiesgoGeneral();
        clsBLLParaCalificacionRiesgoGeneral CalificacionRiesgoGeneralBLL = new clsBLLParaCalificacionRiesgoGeneral();
        #region Properties
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

        private DataTable infoGrid;
        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

        private int pagIndexInfoGrid;
        private int PagIndexInfoGrid
        {
            get
            {
                pagIndexInfoGrid = (int)ViewState["pagIndexInfoGrid"];
                return pagIndexInfoGrid;
            }
            set
            {
                pagIndexInfoGrid = value;
                ViewState["pagIndexInfoGrid"] = pagIndexInfoGrid;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            if (!Page.IsPostBack)
            {
                inicializarValores();
                loadDDL();
                loadGrid();
                loadInfo();
            }
        }

        private void loadDDL()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación riesgo. " + ex.Message);
            }
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("IdClasificacionRiesgo", typeof(string));
            grid.Columns.Add("NombreClasificacionRiesgo", typeof(string));
            grid.Columns.Add("NombreClasificacionGeneralRiesgo", typeof(string));
            InfoGrid = grid;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        private void loadInfo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParametrizacionRiesgos.loadInfoClasificacionGeneralRiesgo();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdClasificacionGeneralRiesgo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["IdClasificacionRiesgo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["NombreClasificacionGeneralRiesgo"].ToString().Trim()
                                                   });
                }
                GridView1.PageIndex = PagIndexInfoGrid;
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGrid = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGrid;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = /*(Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGrid) +*/ Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    resetValuesCampos();
                    detalleRegistro();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Clasificación general";
                    }
                    break;
            }
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            switch (lbldummyOkNo.Text.Trim())
            {
                case "Clasificación general":
                    try
                    {
                        resetValuesCampos();
                        borrarRegistro();
                        loadGrid();
                        loadInfo();
                        Mensaje("Clasificación general eliminada con éxito.");
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error al eliminar la información. " + ex.Message);
                    }
                    break;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                resetValuesCampos();
                ImageButton2.Visible = true;
                trCampos.Visible = true;
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesCampos();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    //cParametrizacionRiesgos.agregarRegistroClasificacionGeneralRiesgo(DropDownList1.SelectedValue.ToString().Trim(), TextBox1.Text.Trim());
                    bool booResult = false;
                    CalificacionRiesgoGeneral.intIdClasificacionRiesgo = Convert.ToInt32(DropDownList1.SelectedValue.ToString().Trim());
                    CalificacionRiesgoGeneral.strNombreClasificacionGeneralRiesgo = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
                    CalificacionRiesgoGeneral.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString().Trim());
                    CalificacionRiesgoGeneral.dtFechaRegistro = DateTime.Now;
                    string strErrMsg = string.Empty;
                    booResult = CalificacionRiesgoGeneralBLL.mtdInsertarParaCalificacionRiesgoGeneral(CalificacionRiesgoGeneral, ref strErrMsg);
                    if (booResult == true)
                    {
                        resetValuesCampos();
                        loadGrid();
                        loadInfo();
                        Mensaje("Registro agregado con éxito.");
                    }
                    else
                    {
                        Mensaje(strErrMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el registro. " + ex.Message);
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    //cParametrizacionRiesgos.modificarRegistroClasificacionGeneralRiesgo(DropDownList1.SelectedValue.ToString().Trim(), TextBox1.Text.Trim(), InfoGrid.Rows[RowGrid]["IdClasificacionGeneralRiesgo"].ToString().Trim());
                    bool booResult = false;
                    CalificacionRiesgoGeneral.intIdClasificacionRiesgo = Convert.ToInt32(DropDownList1.SelectedValue.ToString().Trim());
                    CalificacionRiesgoGeneral.strNombreClasificacionGeneralRiesgo = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
                    CalificacionRiesgoGeneral.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString().Trim());
                    CalificacionRiesgoGeneral.dtFechaRegistro = DateTime.Now;
                    CalificacionRiesgoGeneral.intIdClasificacionGeneralRiesgo = Convert.ToInt32(InfoGrid.Rows[RowGrid]["IdClasificacionGeneralRiesgo"].ToString().Trim());
                    string strErrMsg = string.Empty;
                    booResult = CalificacionRiesgoGeneralBLL.mtdActualizarParaCalificacionRiesgoGeneral(CalificacionRiesgoGeneral, ref strErrMsg);
                    if (booResult == true)
                    {
                        resetValuesCampos();
                        loadGrid();
                        loadInfo();
                        Mensaje("Registro modificado con éxito.");
                    }
                    else
                    {
                        Mensaje(strErrMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el registro. " + ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void inicializarValores()
        {
            PagIndexInfoGrid = 0;
        }

        private void borrarRegistro()
        {
            cParametrizacionRiesgos.borrarRegistroClasificacionGeneralRiesgo(InfoGrid.Rows[RowGrid]["IdClasificacionGeneralRiesgo"].ToString().Trim());
        }

        private void detalleRegistro()
        {
            TextBox1.Text = InfoGrid.Rows[RowGrid]["NombreClasificacionGeneralRiesgo"].ToString().Trim();
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedValue.ToString().Trim() == InfoGrid.Rows[RowGrid]["IdClasificacionRiesgo"].ToString().Trim())
                    break;
                else
                    DropDownList1.SelectedIndex = 0;
            }
            ImageButton3.Visible = true;
            trCampos.Visible = true;
        }

        private void resetValuesCampos()
        {
            trCampos.Visible = false;
            DropDownList1.SelectedIndex = 0;
            TextBox1.Text = "";
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
        }
    }
}