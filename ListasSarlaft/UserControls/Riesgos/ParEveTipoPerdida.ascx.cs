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
    public partial class ParEveTipoPerdida : System.Web.UI.UserControl
    {
        string IdFormulario = "5001";
        cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private int rowGridTipoPerdida;
        private int RowGridTipoPerdida
        {
            get
            {
                rowGridTipoPerdida = (int)ViewState["rowGridTipoPerdida"];
                return rowGridTipoPerdida;
            }
            set
            {
                rowGridTipoPerdida = value;
                ViewState["rowGridTipoPerdida"] = rowGridTipoPerdida;
            }
        }

        private DataTable infoGridTipoPerdida;
        private DataTable InfoGridTipoPerdida
        {
            get
            {
                infoGridTipoPerdida = (DataTable)ViewState["infoGridTipoPerdida"];
                return infoGridTipoPerdida;
            }
            set
            {
                infoGridTipoPerdida = value;
                ViewState["infoGridTipoPerdida"] = infoGridTipoPerdida;
            }
        }

        private int pagIndexInfoGridTipoPerdida;
        private int PagIndexInfoGridTipoPerdida
        {
            get
            {
                pagIndexInfoGridTipoPerdida = (int)ViewState["pagIndexInfoGridTipoPerdida"];
                return pagIndexInfoGridTipoPerdida;
            }
            set
            {
                pagIndexInfoGridTipoPerdida = value;
                ViewState["pagIndexInfoGridTipoPerdida"] = pagIndexInfoGridTipoPerdida;
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
                loadGridTipoPerdida();
                loadInfoTipoPerdida();
            }
        }

        #region Loads
        private void loadGridTipoPerdida()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdTipoPerdidaEvento", typeof(string));
            grid.Columns.Add("NombreTipoPerdidaEvento", typeof(string));
            InfoGridTipoPerdida = grid;
            GridView1.DataSource = InfoGridTipoPerdida;
            GridView1.DataBind();
        }

        private void loadInfoTipoPerdida()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParametrizacionRiesgos.loadInfoTipoPerdida();

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridTipoPerdida.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdTipoPerdidaEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoPerdidaEvento"].ToString().Trim()
                    });
                }
                GridView1.PageIndex = PagIndexInfoGridTipoPerdida;
                GridView1.DataSource = InfoGridTipoPerdida;
                GridView1.DataBind();
            }
        }
        #endregion

        #region GridView
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridTipoPerdida = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridTipoPerdida;
            GridView1.DataSource = InfoGridTipoPerdida;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Modificar":
                    RowGridTipoPerdida = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridTipoPerdida) + Convert.ToInt16((e.CommandArgument).ToString());

                    resetValuesCamposTipoPerdida();
                    detalleRegistroTipoPerdida();
                    break;

                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        RowGridTipoPerdida = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridTipoPerdida) + Convert.ToInt16((e.CommandArgument).ToString());
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Tipo perdida";
                    }
                    break;
            }
        }
        #endregion

        #region Buttons
        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            switch (lbldummyOkNo.Text.Trim())
            {
                case "Tipo perdida":
                    try
                    {
                        resetValuesCamposTipoPerdida();
                        borrarRegistroTipoPerdida();
                        loadGridTipoPerdida();
                        loadInfoTipoPerdida();
                        Mensaje("Tipo perdida eliminada con éxito.");
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
                resetValuesCamposTipoPerdida();
                ImageButton2.Visible = true;
                trCamposTipoPerdida.Visible = true;
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesCamposTipoPerdida();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    cParametrizacionRiesgos.agregarRegistroTipoPerdida(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()));
                    resetValuesCamposTipoPerdida();
                    loadGridTipoPerdida();
                    loadInfoTipoPerdida();
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
                    cParametrizacionRiesgos.modificarRegistroTipoPerdida(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()),
                        InfoGridTipoPerdida.Rows[RowGridTipoPerdida]["IdTipoPerdidaEvento"].ToString().Trim());
                    resetValuesCamposTipoPerdida();
                    loadGridTipoPerdida();
                    loadInfoTipoPerdida();
                    Mensaje("Registro modificado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el registro. " + ex.Message);
            }
        }
        #endregion

        #region
        private void inicializarValores()
        {
            PagIndexInfoGridTipoPerdida = 0;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void borrarRegistroTipoPerdida()
        {
            cParametrizacionRiesgos.borrarRegistroTipoPerdida(InfoGridTipoPerdida.Rows[RowGridTipoPerdida]["IdTipoPerdidaEvento"].ToString().Trim());
        }

        private void detalleRegistroTipoPerdida()
        {
            TextBox1.Text = InfoGridTipoPerdida.Rows[RowGridTipoPerdida]["NombreTipoPerdidaEvento"].ToString().Trim();
            ImageButton3.Visible = true;
            trCamposTipoPerdida.Visible = true;
        }

        private void resetValuesCamposTipoPerdida()
        {
            trCamposTipoPerdida.Visible = false;
            TextBox1.Text = "";
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
        }
        #endregion
    }
}