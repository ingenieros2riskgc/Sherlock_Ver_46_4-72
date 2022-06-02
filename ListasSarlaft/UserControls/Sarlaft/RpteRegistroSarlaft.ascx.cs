using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls
{
    public partial class RpteRegistroSarlaft : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        private cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        String IdFormulario = "6009";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadDDLTipoIden();
                //TbPorRegistroComentario.Visible = false;
            }
        }

        private void loadDDLTipoIden()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRegistroOperacion.loadDDLTipoIden();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList5.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
                DropDownList5.SelectedIndex = DropDownList5.Items.Count - 1;
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los tipos de registro. " + ex.Message);
            }
        }
 
        
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)Session["infGrid"];
                return infGrid;
            }
        }

        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            TbPorRegistro.Visible = true;
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            TbPorRegistro.Visible = false;
            TbAdjuntos.Visible = false;
            TbPorRegistroComentario.Visible = false;
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LabelIdRegOper.Text = GridView3.Rows[Convert.ToInt16(e.CommandArgument)].Cells[0].Text;
            switch (e.CommandName)
            {
                case "Imprimir":
                    TbAdjuntos.Visible = false;
                    TbPorRegistroComentario.Visible = true;
                    ReportViewer3.LocalReport.Refresh();
                    break;
                case "Descargar":
                    TbPorRegistroComentario.Visible = false;
                    TbAdjuntos.Visible = true;
                    break;
            }

        }

        private int rowGridArchivoRegistroOperacion;
        private int RowGridArchivoRegistroOperacion
        {
            get
            {
                rowGridArchivoRegistroOperacion = (int)ViewState["rowGridArchivoRegistroOperacion"];
                return rowGridArchivoRegistroOperacion;
            }
            set
            {
                rowGridArchivoRegistroOperacion = value;
                ViewState["rowGridArchivoRegistroOperacion"] = rowGridArchivoRegistroOperacion;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string UrlArchivo = GridView1.Rows[Convert.ToInt16(e.CommandArgument)].Cells[4].Text.Trim();
            mtdDescargarPdfRegOperacion(UrlArchivo);
        }

        private void mtdDescargarPdfRegOperacion(string strNombreArchivo)
        {
            #region Vars
            byte[] bPdfData = cRegistroOperacion.mtdDescargarArchivoPdf(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }
     }
}