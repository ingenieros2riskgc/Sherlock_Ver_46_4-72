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
    public partial class ParRieFactorRiesgoOperativo : System.Web.UI.UserControl
    {
        string IdFormulario = "5001";
        cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
        cCuenta cCuenta = new cCuenta();
        clsDTOParaFactorRiesgoOperativo FactorRiesgoOp = new clsDTOParaFactorRiesgoOperativo();
        clsBLLParaFactorRiesgoOperativo FactorRiesgoOpBLL = new clsBLLParaFactorRiesgoOperativo();
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
            grid.Columns.Add("IdFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("NombreFactorRiesgoOperativo", typeof(string));
            InfoGrid = grid;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        private void loadInfo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParametrizacionRiesgos.loadInfoFactorRiesgoOperativo();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdFactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreFactorRiesgoOperativo"].ToString().Trim()
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
                        lbldummyOkNo.Text = "Factor de riesgo operativo";
                    }
                    break;
            }
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            switch (lbldummyOkNo.Text.Trim())
            {
                case "Factor de riesgo operativo":
                    try
                    {
                        resetValuesCampos();
                        borrarRegistro();
                        loadGrid();
                        loadInfo();
                        Mensaje("Factor de riesgo operativo eliminado con éxito.");
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
                    //cParametrizacionRiesgos.agregarRegistroFactorRiesgoOperativo(TextBox1.Text.Trim());
                    bool booResult = false;
                    FactorRiesgoOp.strNombreFactorRiesgoOperativo = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
                    string strErrMsg = string.Empty;
                    booResult = FactorRiesgoOpBLL.mtdInsertarParaFactorRiesgoOperativo(FactorRiesgoOp, ref strErrMsg);
                    resetValuesCampos();
                    loadGrid();
                    loadInfo();
                    Mensaje("Registro agregado con éxito.");
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
                    //cParametrizacionRiesgos.modificarRegistroFactorRiesgoOperativo(TextBox1.Text.Trim(), InfoGrid.Rows[RowGrid]["IdFactorRiesgoOperativo"].ToString().Trim());
                    bool booResult = false;
                    FactorRiesgoOp.strNombreFactorRiesgoOperativo = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
                    FactorRiesgoOp.intIdFactorRiesgoOperativo = Convert.ToInt32(InfoGrid.Rows[RowGrid]["IdFactorRiesgoOperativo"].ToString().Trim());
                    string strErrMsg = string.Empty;
                    booResult = FactorRiesgoOpBLL.mtdActualizarParaFactorRiesgoOperativo(FactorRiesgoOp, ref strErrMsg);
                    resetValuesCampos();
                    loadGrid();
                    loadInfo();
                    Mensaje("Registro modificado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el registro. " + ex.Message);
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGrid = 0;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void borrarRegistro()
        {
            cParametrizacionRiesgos.borrarRegistroFactorRiesgoOperativo(InfoGrid.Rows[RowGrid]["IdFactorRiesgoOperativo"].ToString().Trim());
        }

        private void detalleRegistro()
        {
            TextBox1.Text = InfoGrid.Rows[RowGrid]["NombreFactorRiesgoOperativo"].ToString().Trim();
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