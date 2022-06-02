using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class ConfigMapaRiesgos : System.Web.UI.UserControl
    {
        cRiesgo objStoredProcedures = new cRiesgo();
        cCuenta cCuenta = new cCuenta();
        public static int IntIndice = 0;
        String IdFormulario = "5036";
        /// <summary>
        /// Metodo que se ejecuta al cargar la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                DataTable dtGrid = new DataTable();
                dtGrid = objStoredProcedures.mtdGetRiesgoInherente();
                GViewConfigMR.DataSource = dtGrid;
                GViewConfigMR.DataBind();
                PanelConfigMapa.Visible = false;

                #region Cargar Colores

                DataTable dtColores = new DataTable();
                dtColores = objStoredProcedures.mtdCargarColoresMapa();
                DpColor.DataSource = dtColores;
                DpColor.DataTextField = "Color";
                DpColor.DataValueField = "Color";
                DpColor.DataBind();

                #endregion
            }
        }

        /// <summary>
        /// Boton Guardar Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            string strUsuarioRegistro = Session["Usuario"].ToString().Trim();

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {

                if (mtdActualizarColorMapa(IntIndice.ToString(), DpColor.SelectedValue))
                {
                    //mostrar el mensaje
                    Mensaje("Actualización Exitosa");
                }

                #region Refrescar GridView

                DataTable dtGrid = new DataTable();
                dtGrid = objStoredProcedures.mtdGetRiesgoInherente();
                GViewConfigMR.DataSource = dtGrid;
                GViewConfigMR.DataBind();
                PanelConfigMapa.Visible = false;

                #endregion
            }
        }

        /// <summary>
        /// Boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            PanelConfigMapa.Visible = false;
        }
      
        /// <summary>
        /// Metodo que ejecuta el evento de la Fila del GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GViewConfigMR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Actualizar"))
            {
                #region Cargar Datos en Controles

                this.LblInformacion.Visible = true;
                this.PanelConfigMapa.Visible = true;
                string strIndiceCaracter = string.Empty;
                int intFila = 0;
                intFila = intFila + Convert.ToInt32(e.CommandArgument);
                DataTable dtRiesgo = objStoredProcedures.mtdGetColoresRiesgoInherente();
                if (dtRiesgo.Rows.Count > 0)
                {
                    strIndiceCaracter = dtRiesgo.Rows[intFila]["IdRiesgoInherente"].ToString().Trim();
                    //Asigno el Indice a actualizar a la variable global que se utiliza en el metodo actualizarCaracter
                    IntIndice = int.Parse(strIndiceCaracter);
                    this.TxtNombreRiesgo.Text = dtRiesgo.Rows[intFila]["NombreRiesgoInherente"].ToString().Trim();
                    this.txtFrecuencia.Text = dtRiesgo.Rows[intFila]["IdProbabilidad"].ToString().Trim();
                    this.TxtImpacto.Text = dtRiesgo.Rows[intFila]["IdImpacto"].ToString().Trim();
                    for (int i = 0; i < DpColor.Items.Count; i++)
                    {
                        DpColor.SelectedIndex = i;
                        if (DpColor.SelectedItem.Value.ToString() == dtRiesgo.Rows[intFila]["Color"].ToString().Trim())
                        {
                            break;
                        }
                    }

                }

                #endregion
            }
        }

        /// <summary>
        /// Evento de cambio de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GViewConfigMR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GViewConfigMR.PageIndex = e.NewPageIndex;
            mtdLlenarGrid();
        }

        protected void GViewConfigMR_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtGrid = new DataTable();
            dtGrid = objStoredProcedures.mtdGetRiesgoInherente();
            GViewConfigMR.DataSource = dtGrid;
            GViewConfigMR.DataBind();

            //SetSortDirection(SortDireaction);
            string _sortDirection = "ASC";
            if (dtGrid != null)
            {
                //Sort the data.
                dtGrid.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                GViewConfigMR.DataSource = dtGrid;
                GViewConfigMR.DataBind();
                //SortDireaction = _sortDirection;
            }
        } 
        /// <summary>
        /// Metodo para mostrar ventana emergente
        /// </summary>
        /// <param name="strMensaje"></param>
        private void Mensaje(string strMensaje)
        {
            this.lblMsgBox.Text = strMensaje;
            this.mpeMsgBox.Show();
        }

        /// <summary>
        /// Metodo para llenar la Grilla de GviewCaracteres
        /// </summary>
        protected void mtdLlenarGrid()
        {
            DataTable dtGrid = new DataTable();
            dtGrid = objStoredProcedures.mtdGetRiesgoInherente();
            GViewConfigMR.DataSource = dtGrid;
            GViewConfigMR.DataBind();
        }

        /// <summary>
        /// Metodo que ejecuta el procedimiento almacenado SP_UpdateColorRiesgo 
        /// para la actualizacion de los colores del mapa de riesgo, 
        /// es llamado al pulsar el boton guardar del Panel
        /// </summary>
        /// <param name="strId">ID de Registro a Actualizar</param>
        /// <param name="strColor">Color a Actualizar</param>
        private bool mtdActualizarColorMapa(string strId, string strColor)
        {
            bool booActualiza = false;

            try
            {

                objStoredProcedures.mtdActualizaColorMapa(strId, strColor);
                booActualiza = true;
            }
            catch (Exception ex)
            {
                booActualiza = false;
                throw ex;
            }
            return booActualiza;
        }

        protected void SetSortDirection(string sortDirection)
        {
            //if (sortDirection == "ASC")
            //    _sortDirection = "DESC";
            //else
            //    _sortDirection = "ASC";
        } 
    }
}