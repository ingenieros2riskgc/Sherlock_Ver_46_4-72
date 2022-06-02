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
    public partial class ParRieClasificacionRiesgo : System.Web.UI.UserControl
    {
        string IdFormulario = "5001";
        cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
        cCuenta cCuenta = new cCuenta();
        clsDTOParaCalificacionRiesgo CalificacionRiesgo = new clsDTOParaCalificacionRiesgo();
        clsBLLParaCalificacionRiesgo CalificacionRiesgoBLL = new clsBLLParaCalificacionRiesgo();
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
                loadGrid();
                loadInfo();
            }
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdClasificacionRiesgo", typeof(string));
            grid.Columns.Add("NombreClasificacionRiesgo", typeof(string));
            InfoGrid = grid;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        private void loadInfo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParametrizacionRiesgos.loadInfoClasificacionRiesgo();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdClasificacionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim()
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
                        lbldummyOkNo.Text = "Clasificación global";
                    }
                    break;
            }
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            switch (lbldummyOkNo.Text.Trim())
            {
                case "Clasificación global":
                    try
                    {
                        resetValuesCampos();
                        borrarRegistro();
                        loadGrid();
                        loadInfo();
                        Mensaje("Clasificación global eliminada con éxito.");
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
                    //cParametrizacionRiesgos.agregarRegistroClasificacionRiesgo(TextBox1.Text.Trim());
                    bool booResult = false;
                    CalificacionRiesgo.strNombreClasificacionRiesgo = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
                    CalificacionRiesgo.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString().Trim());
                    CalificacionRiesgo.dtFechaRegistro = DateTime.Now;
                    string strErrMsg = string.Empty;

                    booResult = CalificacionRiesgoBLL.mtdInsertarParaCalificacionRiesgo(CalificacionRiesgo, ref strErrMsg);
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
                    //cParametrizacionRiesgos.modificarRegistroClasificacionRiesgo(TextBox1.Text.Trim(), InfoGrid.Rows[RowGrid]["IdClasificacionRiesgo"].ToString().Trim());
                    bool booResult = false;
                    CalificacionRiesgo.strNombreClasificacionRiesgo = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
                    CalificacionRiesgo.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString().Trim());
                    CalificacionRiesgo.dtFechaRegistro = DateTime.Now;
                    CalificacionRiesgo.intIdClasificacionRiesgo = Convert.ToInt32(InfoGrid.Rows[RowGrid]["IdClasificacionRiesgo"].ToString().Trim());
                    string strErrMsg = string.Empty;

                    booResult = CalificacionRiesgoBLL.mtdActualizarParaCalificacionRiesgo(CalificacionRiesgo, ref strErrMsg);
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
            if (InfoGrid.Rows[RowGrid]["IdClasificacionRiesgo"].ToString().Trim() != "1" && InfoGrid.Rows[RowGrid]["IdClasificacionRiesgo"].ToString().Trim() != "2")
                cParametrizacionRiesgos.borrarRegistroClasificacionRiesgo(InfoGrid.Rows[RowGrid]["IdClasificacionRiesgo"].ToString().Trim());
            else
                Mensaje("No se permite eliminar esta opción.");
        }

        private void detalleRegistro()
        {
            TextBox1.Text = InfoGrid.Rows[RowGrid]["NombreClasificacionRiesgo"].ToString().Trim();
            ImageButton3.Visible = true;
            trCampos.Visible = true;
        }

        private void resetValuesCampos()
        {
            trCampos.Visible = false;
            TextBox1.Text = "";
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
        }
    }
}