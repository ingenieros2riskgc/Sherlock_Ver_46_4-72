using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;

namespace ListasSarlaft.UserControls.ConfigEstructura
{
    public partial class ConfigPerfilEstructArch : System.Web.UI.UserControl
    {
        string IdFormulario = "11003";
        clsCuenta cCuenta = new clsCuenta();

        #region Properties
        private DataTable infoGrid;
        private int rowGrid;

        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infGrid2"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infGrid2"] = infoGrid;
            }
        }

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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(Convert.ToInt32(Session["IdUsuario"].ToString()), Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                mtdLoadDDLPerfiles();
            }
        }

        #region Loads
        private void mtdLoadDDLPerfiles()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsPerfil cPerfil = new clsPerfil();
            List<clsDTOPerfil> lstPerfil = new List<clsDTOPerfil>();
            #endregion Vars

            lstPerfil = cPerfil.mtdCargarInfoPerfiles(ref strErrMsg);

            if (lstPerfil != null)
            {
                int intCounter = 1;
                ddlPerfil.Items.Clear();
                ddlPerfil.Items.Insert(0, new ListItem("", "0"));

                foreach (clsDTOPerfil objPerfil in lstPerfil)
                {
                    ddlPerfil.Items.Insert(intCounter, new ListItem(objPerfil.StrNombrePerfil, objPerfil.StrIdPerfil));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }

        private void mtdLoadGridView(string strIdPerfil)
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsDTOPerfil objPerfil = new clsDTOPerfil(string.Empty,strIdPerfil, string.Empty, string.Empty, string.Empty);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTORelacion> lstRelacion = new List<clsDTORelacion>();
            #endregion Vars

            lstRelacion = cParamArchivo.mtdConsultarRelaciones(objPerfil, false, ref strErrMsg);

            if (lstRelacion != null)
            {
                mtdLoadGrid();
                mtdLoadInfoGrid(lstRelacion);
                gvRelacion.DataSource = lstRelacion;
                gvRelacion.DataBind();
            }
        }

        private void mtdLoadGrid()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdRelacion", typeof(string));
            grid.Columns.Add("StrIdPerfil", typeof(string));
            grid.Columns.Add("StrIdConfCampo", typeof(string));
            grid.Columns.Add("StrNombreCampo", typeof(string));
            grid.Columns.Add("StrPosicion", typeof(string));
            grid.Columns.Add("BooActivo", typeof(string));

            gvRelacion.DataSource = grid;
            gvRelacion.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGrid(List<clsDTORelacion> lstRelacion)
        {
            foreach (clsDTORelacion objRelacion in lstRelacion)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objRelacion.StrIdRelacion.ToString().Trim(),
                    objRelacion.StrIdPerfil.ToString().Trim(),
                    objRelacion.StrIdConfCampo.ToString().Trim(),
                    objRelacion.StrNombreCampo.ToString().Trim(),
                    objRelacion.StrPosicion.ToString().Trim(),  
                    objRelacion.BooActivo
                    });
            }
        }
        #endregion Loads

        #region Buttons
        protected void ibtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {

                    mtdAgregarActualizarRelaciones(ddlPerfil.SelectedIndex.ToString().Trim(), gvRelacion, ref strErrMsg);

                    mtdLoadGridView(ddlPerfil.SelectedIndex.ToString());

                    if (string.IsNullOrEmpty(strErrMsg))
                        mtdMensaje("La relación fue creada exitósamente.");
                    else
                        mtdMensaje(strErrMsg);

                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar la relación. [" + ex.Message + "].");
            }
        }

        protected void ibtnCancelUpd_Click(object sender, EventArgs e)
        {
            mtdLoadGridView(ddlPerfil.SelectedIndex.ToString());
        }
        #endregion

        #region DDLs
        protected void ddlPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtdLoadGridView(ddlPerfil.SelectedIndex.ToString());
        }

        #endregion DDLs

        #region Methods
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdAgregarActualizarRelaciones(string strIdPerfil, GridView gvRelacion, ref string strErrMsg)
        {
            clsParamArchivo cParamArchivo = new clsParamArchivo();

            for (int intCounter = 0; intCounter < InfoGrid.Rows.Count; intCounter++)
            {
                if (InfoGrid.Rows[intCounter]["StrIdRelacion"].ToString().Trim() == "0")
                {
                    //Agregar
                    clsDTORelacion objRelacion = new clsDTORelacion(string.Empty, strIdPerfil,
                        InfoGrid.Rows[intCounter]["StrIdConfCampo"].ToString().Trim(), string.Empty, string.Empty,
                        string.Empty, ((CheckBox)gvRelacion.Rows[intCounter].FindControl("chbActivo")).Checked);

                    cParamArchivo.mtdAgregarRelacion(objRelacion, ref strErrMsg);
                }
                else
                {
                    //Actualizar
                    clsDTORelacion objRelacion = new clsDTORelacion(InfoGrid.Rows[intCounter]["StrIdRelacion"].ToString().Trim(),
                        strIdPerfil, string.Empty, InfoGrid.Rows[intCounter]["StrIdConfCampo"].ToString().Trim(), string.Empty,
                        string.Empty, ((CheckBox)gvRelacion.Rows[intCounter].FindControl("chbActivo")).Checked);

                    cParamArchivo.mtdActualizarRelacion(objRelacion, ref strErrMsg);
                }
            }

            /***********************
            foreach (GridViewRow gvrLine in gvRelacion.Rows)
            {

                if (string.IsNullOrEmpty(gvrLine.Cells[0].Text.Trim()))
                {
                    //Agregar
                    clsDTORelacion objRelacion = new clsDTORelacion(string.Empty, strIdPerfil, gvrLine.Cells[2].Text.Trim(), string.Empty,
                        string.Empty, ((CheckBox)gvrLine.FindControl("chbActivo")).Checked);

                    cParamArchivo.mtdAgregarRelacion(objRelacion, ref strErrMsg);
                }
                else
                {
                    //Actualizar
                    clsDTORelacion objRelacion = new clsDTORelacion(gvrLine.Cells[0].Text.Trim(), strIdPerfil, gvrLine.Cells[2].Text.Trim(), string.Empty,
                        string.Empty, ((CheckBox)gvrLine.FindControl("chbActivo")).Checked);
                    cParamArchivo.mtdActualizarRelacion(objRelacion, ref strErrMsg);
                }
            }
             ***********************/
        }
        #endregion Methods
    }
}