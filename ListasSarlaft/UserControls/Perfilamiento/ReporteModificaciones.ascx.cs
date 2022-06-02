using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Security.Application;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;
using ClosedXML.Excel;
using clsDatos;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class ReporteModificaciones : System.Web.UI.UserControl
    {
        string IdFormulario = "11002";
        clsCuenta cCuenta = new clsCuenta();
        string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();
        ListasSarlaft.Classes.cCuenta ccCuenta = new ListasSarlaft.Classes.cCuenta();

        #region Properties

        private int pagIndex;
        private DataTable infoGrid;
        private int rowGrid;

        private int PagIndex
        {
            get
            {
                pagIndex = (int)ViewState["pagIndex"];
                return pagIndex;
            }
            set
            {
                pagIndex = value;
                ViewState["pagIndex"] = pagIndex;
            }
        }

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
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportVariables);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportCategorias);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportPerfiles);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportEstructuraArchivos);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportSenales);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExportConsulSenales);
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {

                mtdHideAll();
                mtdInicializarValoresVariables();
                mtdInicializarValoresCategorias();
                mtdInicializarValoresPerfiles();
                mtdInicializarValoresEstructuraArchivos();
                mtdInicializarValoresSenales();
                mtdInicializarValoresConsulSenales();
                mtdLoadGridViewVariables();
                mtdLoadGridViewCategorias();
                mtdLoadGridViewPerfiles();
                mtdLoadGridViewEstructuraArchivos();
                mtdLoadGridViewSenales();
                mtdLoadGridViewConsulSenales();

            }
        }

        protected void mtdHideAll()
        {
            gvTipoParametro.Visible = false;
            gvParametrizacion.Visible = false;
            gvPerfiles.Visible = false;
            gvEstructura.Visible = false;
            gvSenales.Visible = false;
            gvConsulSenales.Visible = false;
            Div0.Visible = false;
            Div1.Visible = false;
            Div2.Visible = false;
            Div3.Visible = false;
            Div4.Visible = false;
            Div5.Visible = false;
        }
        protected void DDLopciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLopciones.SelectedValue == "0")
            {
                mtdHideAll();
            }
            if (DDLopciones.SelectedValue == "1")
            {
                mtdHideAll();
                gvTipoParametro.Visible = true;
                Div0.Visible = true;
            }
            if (DDLopciones.SelectedValue == "2")
            {
                mtdHideAll();
                gvParametrizacion.Visible = true;
                Div1.Visible = true;
            }
            if (DDLopciones.SelectedValue == "3")
            {
                mtdHideAll();
                gvPerfiles.Visible = true;
                Div2.Visible = true;
            }
            if (DDLopciones.SelectedValue == "4")
            {
                mtdHideAll();
                gvEstructura.Visible = true;
                Div3.Visible = true;
            }
            if (DDLopciones.SelectedValue == "5")
            {
                mtdHideAll();
                gvSenales.Visible = true;
                Div4.Visible = true;
            }
            if (DDLopciones.SelectedValue == "6")
            {
                mtdHideAll();
                gvConsulSenales.Visible = true;
                Div5.Visible = true;
            }

        }

        /// <summary>
        /// Llamado del Grid Variables
        /// </summary>

        protected void gvTipoParametro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvTipoParametro.PageIndex = PagIndex;
            gvTipoParametro.DataSource = InfoGrid;
            gvTipoParametro.DataBind();

            mtdLoadGridViewVariables();
        }

        protected void gvTipoParametro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvTipoParametro.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
            }
        }
        private void mtdLoadGridViewVariables()
        {
            mtdLoadGridVariables();
            mtdLoadInfoGridVariables();
        }
        private void mtdLoadGridVariables()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdVariable", typeof(string));
            grid.Columns.Add("StrNombreVariable", typeof(string));
            grid.Columns.Add("StrCalificacion", typeof(string));
            grid.Columns.Add("BooActivo", typeof(string));
            grid.Columns.Add("StrUsuario", typeof(string));
            grid.Columns.Add("StrIdUsuario", typeof(string));
            grid.Columns.Add("StrFechaModificacion", typeof(string));

            gvTipoParametro.DataSource = grid;
            gvTipoParametro.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridVariables()
        {
            string strErrMsg = string.Empty;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOVariableMod> lstTipoParam = new List<clsDTOVariableMod>();

            lstTipoParam = cParamArchivo.mtdCargarInfoVariablesMod(ref strErrMsg);

            if (lstTipoParam != null)
            {
                mtdLoadInfoGrid(lstTipoParam);
                gvTipoParametro.DataSource = lstTipoParam;
                gvTipoParametro.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOVariableMod> lstVariable)
        {
            foreach (clsDTOVariableMod objVariable in lstVariable)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objVariable.StrIdVariable.ToString().Trim(),
                    objVariable.StrNombreVariable.ToString().Trim(),
                    objVariable.StrCalificacion.ToString().Trim(),
                    objVariable.BooActivo,
                    objVariable.StrUsuario.ToString().Trim(),
                    objVariable.StrIdUsuario.ToString().Trim(),
                    objVariable.StrFechaModificacion.ToString().Trim()
                    });
            }
        }

        private void mtdInicializarValoresVariables()
        {
            PagIndex = 0;
        }

        private void mtdMensajeVariables(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        //Excel
        protected void ImButtonExcelExportVariables_Click(object sender, ImageClickEventArgs e)
        {
            exportExcelVariables(Response, "ModificacionesVariables_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcelVariables(HttpResponse Response, string filename)
        {
            string strErrMsg = null;
            clsDtParamArchivo carga = new clsDtParamArchivo();
            DataTable ConsulVariables = carga.ConsulVariables(ref strErrMsg);
            if (ConsulVariables == null)
            {
                mtdMensajeVariables("Atención! No hay datos para exportar.");
                return;
            }
            ConsulVariables.TableName = "Modificaciones Variables";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(ConsulVariables);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(ds);
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        /// <summary>
        /// Fin del llamado del Grid Variables
        /// </summary>

        /// <summary>
        /// Llamado del Grid Categorías
        /// </summary>
        protected void gvParametrizacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvParametrizacion.PageIndex = PagIndex;
            gvParametrizacion.DataBind();

            mtdLoadGridViewCategorias();
        }
        protected void gvParametrizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvParametrizacion.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
            }
        }
        private void mtdLoadGridViewCategorias()
        {
            mtdLoadGridCategorias();
            mtdLoadInfoGridCategorias();
        }
        private void mtdLoadGridCategorias()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdParametros", typeof(string));
            grid.Columns.Add("StrNombreParametro", typeof(string));
            grid.Columns.Add("StrCodigoParametro", typeof(string));
            grid.Columns.Add("StrIdTipoParametro", typeof(string));
            grid.Columns.Add("StrNombreTipoParametro", typeof(string));
            grid.Columns.Add("StrCalificacionParametro", typeof(string));
            grid.Columns.Add("BooEsFormula", typeof(string));
            grid.Columns.Add("StrUsuario", typeof(string));
            grid.Columns.Add("StrIdUsuario", typeof(string));
            grid.Columns.Add("StrFechaModificacion", typeof(string));

            gvParametrizacion.DataSource = grid;
            gvParametrizacion.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridCategorias()
        {
            #region Variables
            string strErrMsg = string.Empty;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOParametrizacionMod> lstParametrizacion = new List<clsDTOParametrizacionMod>();
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
            #endregion

            lstParametrizacion = cParamArchivo.mtdCargarInfoParametrizacionMod(objVariable, ref strErrMsg);

            if (lstParametrizacion != null)
            {
                mtdLoadInfoGrid(lstParametrizacion);
                gvParametrizacion.DataSource = lstParametrizacion;
                gvParametrizacion.DataBind();
            }
        }
        private void mtdLoadInfoGrid(List<clsDTOParametrizacionMod> lstParametrizacion)
        {
            foreach (clsDTOParametrizacionMod objParametrizacion in lstParametrizacion)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objParametrizacion.StrIdCategoria.ToString().Trim(),
                    objParametrizacion.StrNombreCategoria.ToString().Trim(),
                    objParametrizacion.StrCodigoCategoria.ToString().Trim(),
                    objParametrizacion.StrIdVariable.ToString().Trim(),
                    objParametrizacion.StrNombreVariable.ToString().Trim(),
                    objParametrizacion.StrCalificacionCategoria.ToString().Trim(),
                    objParametrizacion.BooEsFormula.ToString(),
                    objParametrizacion.StrUsuario.ToString().Trim(),
                    objParametrizacion.StrIdUsuario.ToString().Trim(),
                    objParametrizacion.StrFechaModificacion.ToString().Trim()
                    });
            }
        }
        private void mtdInicializarValoresCategorias()
        {
            PagIndex = 0;
        }
        private void mtdMensajeCategorias(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        //Excel

        protected void ImButtonExcelExportCategorias_Click(object sender, ImageClickEventArgs e)
        {
            exportExcelCategorias(Response, "Reportecategorias_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcelCategorias(HttpResponse Response, string filename)
        {
            string strErrMsg = null;
            clsDtParamArchivo carga = new clsDtParamArchivo();
            DataTable ConsulCategorias = carga.ConsulCategorias(ref strErrMsg);
            if(ConsulCategorias == null)
            {
                mtdMensajeCategorias("Atención! No hay datos para exportar.");
                return;
            }
            ConsulCategorias.TableName = "Modificaciones Categorías";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(ConsulCategorias);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(ds);
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        /// <summary>
        /// Fin del llamado del Grid Categorías
        /// </summary>

        /// <summary>
        /// Llamado del Grid Perfiles
        /// </summary>
        #region Gridview
        protected void gvPerfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvPerfiles.PageIndex = PagIndex;
            gvPerfiles.DataBind();

            mtdLoadGridViewPerfiles();
        }

        protected void gvPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvPerfiles.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
            }
        }
        #endregion Gridview

        #region Loads
        private void mtdLoadGridViewPerfiles()
        {
            mtdLoadGridPerfiles();
            mtdLoadInfoGridPerfiles();
        }

        private void mtdLoadGridPerfiles()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdPerfil", typeof(string));
            grid.Columns.Add("StrNombrePerfil", typeof(string));
            grid.Columns.Add("StrValorMinimo", typeof(string));
            grid.Columns.Add("StrValorMaximo", typeof(string));
            grid.Columns.Add("StrIdUsuario", typeof(string));
            grid.Columns.Add("StrUsuario", typeof(string));
            grid.Columns.Add("StrFechaModificacion", typeof(string));

            gvPerfiles.DataSource = grid;
            gvPerfiles.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridPerfiles()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsPerfil cPerfil = new clsPerfil();
            List<clsDTOPerfilMod> lstPerfiles = new List<clsDTOPerfilMod>();
            #endregion Vars

            lstPerfiles = cPerfil.mtdCargarInfoPerfilesMod(ref strErrMsg);

            if (lstPerfiles != null)
            {
                mtdLoadInfoGrid(lstPerfiles);
                gvPerfiles.DataSource = lstPerfiles;
                gvPerfiles.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOPerfilMod> lstPerfiles)
        {
            foreach (clsDTOPerfilMod objPerfil in lstPerfiles)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objPerfil.StrIdPerfil.ToString().Trim(),
                    objPerfil.StrNombrePerfil.ToString().Trim(),
                    objPerfil.StrValorMinimo.ToString().Trim(),
                    objPerfil.StrValorMaximo.ToString().Trim(),
                    objPerfil.StrIdUsuario.ToString().Trim(),
                    objPerfil.StrUsuario.ToString().Trim(),
                    objPerfil.StrFechaModificacion.ToString().Trim()
                    });
            }
        }
        #endregion Loads

        private void mtdInicializarValoresPerfiles()
        {
            PagIndex = 0;
        }

        private void mtdMensajePerfiles(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        //Excel

        protected void ImButtonExcelExportPerfiles_Click(object sender, ImageClickEventArgs e)
        {
            exportExcelPerfiles(Response, "ReportePerfiles_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }

        protected void exportExcelPerfiles(HttpResponse Response, string filename)
        {
            string strErrMsg = null;
            clsDtPerfil carga = new clsDtPerfil();
            DataTable ConsulPerfiles = carga.ConsulPerfiles(ref strErrMsg);
            if(ConsulPerfiles == null)
            {
                mtdMensajePerfiles("Atención! No hay datos para exportar.");
                return;
            }
            ConsulPerfiles.TableName = "Modificaciones Categorías";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(ConsulPerfiles);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(ds);
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        /// <summary>
        /// Fin del llamado del Grid Perfiles
        /// </summary>

        /// <summary>
        /// Llamado del Grid Estructura de archivos
        /// </summary>

        protected void gvEstructura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvEstructura.PageIndex = PagIndex;
            gvEstructura.DataBind();

            mtdLoadGridViewEstructuraArchivos();
        }

        protected void gvEstructura_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvEstructura.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);

            switch (e.CommandName)
            {
            }
        }
        private void mtdLoadGridViewEstructuraArchivos()
        {
            mtdLoadGridEstructuraArchivos();
            mtdLoadInfoGridEstructuraArchivos();
        }

        private void mtdLoadGridEstructuraArchivos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdEstructCampo", typeof(string));
            grid.Columns.Add("StrNombreCampo", typeof(string));
            grid.Columns.Add("StrLongitud", typeof(string));
            grid.Columns.Add("BooParametrico", typeof(string));
            grid.Columns.Add("StrNombreParametro", typeof(string));
            grid.Columns.Add("StrPosicion", typeof(string));
            grid.Columns.Add("StrIdTipoParametro", typeof(string));
            grid.Columns.Add("StrIdVariable", typeof(string));
            grid.Columns.Add("StrIdTipoDato", typeof(string));
            grid.Columns.Add("BoolNumerico", typeof(string));
            grid.Columns.Add("StrUsuario", typeof(string));
            grid.Columns.Add("StrIdUsuario", typeof(string));
            grid.Columns.Add("StrFechaModificacion", typeof(string));

            gvEstructura.DataSource = grid;
            gvEstructura.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridEstructuraArchivos()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOEstructuraCampoMod> lstEstructura = new List<clsDTOEstructuraCampoMod>();
            #endregion Vars

            lstEstructura = cParamArchivo.mtdCargarInfoEstructuraMod(objVariable, ref strErrMsg);

            if (lstEstructura != null)
            {
                mtdLoadInfoGrid(lstEstructura);
                gvEstructura.DataSource = lstEstructura;
                gvEstructura.DataBind();

                // Se habilita el checkbox del estado de la columna
                foreach (GridViewRow gvrow in gvEstructura.Rows)
                {
                    CheckBox chkNumerico = (CheckBox)gvrow.FindControl("chkNumerico");
                    if (lstEstructura.Where(x => x.StrIdEstructCampo == gvrow.Cells[0].Text).Select(o => o.BoolNumerico).FirstOrDefault() == true)
                        chkNumerico.Checked = true;
                    else
                        chkNumerico.Checked = false;
                }
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOEstructuraCampoMod> lstEstructura)
        {
            foreach (clsDTOEstructuraCampoMod objEstructura in lstEstructura)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objEstructura.StrIdEstructCampo.ToString().Trim(),
                    objEstructura.StrNombreCampo.ToString().Trim(),
                    objEstructura.StrLongitud.ToString().Trim(),
                    objEstructura.BooEsParametrico,
                    objEstructura.StrNombreParametro.ToString().Trim(),
                    objEstructura.StrPosicion.ToString().Trim(),
                    objEstructura.StrIdVariable.ToString().Trim(),
                    objEstructura.StrNombreVariable.ToString().Trim(),
                    objEstructura.StrIdTipoDato.ToString().Trim(),
                    objEstructura.BoolNumerico.ToString().Trim(),
                    objEstructura.StrIdUsuario.ToString().Trim(),
                    objEstructura.StrUsuario.ToString().Trim(),
                    objEstructura.StrFechaModificacion.ToString().Trim(),
                    });
            }
        }
        private void mtdInicializarValoresEstructuraArchivos()
        {
            PagIndex = 0;
        }
        private void mtdMensajeEstructuraArchivos(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        //Excel

        protected void ImButtonExcelExportEstructuraArchivos_Click(object sender, ImageClickEventArgs e)
        {
            exportExcelEstructuraArchivos(Response, "ReporteEstructuraArchivos_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcelEstructuraArchivos(HttpResponse Response, string filename)
        {
            string strErrMsg = null;
            clsDtParamArchivo carga = new clsDtParamArchivo();
            DataTable ConsulEstructuraArchivos = carga.ConsulEstructuraArchivos(ref strErrMsg);
            if(ConsulEstructuraArchivos == null)
            {
                mtdMensajeEstructuraArchivos("Atención! No hay datos para exportar.");
                return;
            }
            ConsulEstructuraArchivos.TableName = "Modificaciones Estructura";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(ConsulEstructuraArchivos);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(ds);
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        /// <summary>
        /// Fin del llamado del Grid Estructura de archivos
        /// </summary>

        /// <summary>
        /// Inicio del llamado del Grid Señales de alerta
        /// </summary>

        protected void gvSenales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvSenales.PageIndex = PagIndex;
            gvSenales.DataSource = InfoGrid;
            gvSenales.DataBind();
        }

        protected void gvSenales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        private void mtdLoadGridViewSenales()
        {
            mtdLoadGridSenales();
            mtdLoadInfoGridSenales();
        }

        private void mtdLoadGridSenales()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdSenal", typeof(string));
            grid.Columns.Add("StrCodigoSenal", typeof(string));
            grid.Columns.Add("StrDescripcionSenal", typeof(string));
            grid.Columns.Add("StrUsuario", typeof(string));
            grid.Columns.Add("StrIdUsuario", typeof(string));
            grid.Columns.Add("StrFechaModificacion", typeof(string));

            gvSenales.DataSource = grid;
            gvSenales.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridSenales()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOSenalMod> lstSenal = new List<clsDTOSenalMod>();

            lstSenal = cSenal.mtdCargarInfoSenalMod(ref strErrMsg);

            if (lstSenal != null)
            {
                mtdLoadInfoGridSenales(lstSenal);
                gvSenales.DataSource = lstSenal;
                gvSenales.DataBind();
            }
        }

        private void mtdLoadInfoGridSenales(List<clsDTOSenalMod> lstSenal)
        {
            foreach (clsDTOSenalMod objSenal in lstSenal)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objSenal.StrIdSenal.ToString().Trim(),
                    objSenal.StrCodigoSenal.ToString().Trim(),
                    objSenal.StrDescripcionSenal.ToString().Trim(),
                    objSenal.StrUsuario.ToString().Trim(),
                    objSenal.StrIdUsuario.ToString().Trim(),
                    objSenal.StrFechaModificacion.ToString().Trim()
                    });
            }
        }
        private void mtdInicializarValoresSenales()
        {
            PagIndex = 0;
        }
        private void mtdMensajeSenales(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        //Excel

        protected void ImButtonExcelExportSenales_Click(object sender, ImageClickEventArgs e)
        {
            exportExcelSenales(Response, "ReporteSenalesAlerta_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcelSenales(HttpResponse Response, string filename)
        {
            string strErrMsg = null;
            clsDtSenal carga = new clsDtSenal();
            DataTable ConsulSenales = carga.ConsulSenales(ref strErrMsg);
            if(ConsulSenales == null)
            {
                mtdMensajeConsulSenales("Atención! No hay datos para exportar.");
                return;
            }
            ConsulSenales.TableName = "Modificaciones Señales Alerta";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(ConsulSenales);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(ds);
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        /// <summary>
        /// Fin del llamado del Grid Señales de alerta
        /// </summary>

        /// <summary>
        /// Inicio del llamado del Grid de Coincidencia de Señales de alerta
        /// </summary>

        protected void gvConsulSenales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvConsulSenales.PageIndex = PagIndex;
            gvConsulSenales.DataSource = InfoGrid;
            gvConsulSenales.DataBind();
        }

        protected void gvConsulSenales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        private void mtdLoadGridViewConsulSenales()
        {
            mtdLoadGridConsulSenales();
            mtdLoadInfoGridConsulSenales();
        }

        private void mtdLoadGridConsulSenales()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdSenal", typeof(string));
            grid.Columns.Add("StrCodigoSenal", typeof(string));
            grid.Columns.Add("StrDescripcionSenal", typeof(string));
            grid.Columns.Add("StrFechaInicial", typeof(string));
            grid.Columns.Add("StrFechaFinal", typeof(string));
            grid.Columns.Add("StrNumeroCoincidencias", typeof(string));
            grid.Columns.Add("StrUsuario", typeof(string));
            grid.Columns.Add("StrIdUsuario", typeof(string));
            grid.Columns.Add("StrFechaConsulta", typeof(string));

            gvConsulSenales.DataSource = grid;
            gvConsulSenales.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridConsulSenales()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOConsulSenalMod> lstSenal = new List<clsDTOConsulSenalMod>();

            lstSenal = cSenal.mtdCargarInfoConsulSenalMod(ref strErrMsg);

            if (lstSenal != null)
            {
                mtdLoadInfoGridConsulSenales(lstSenal);
                gvConsulSenales.DataSource = lstSenal;
                gvConsulSenales.DataBind();
            }
        }

        private void mtdLoadInfoGridConsulSenales(List<clsDTOConsulSenalMod> lstSenal)
        {
            foreach (clsDTOConsulSenalMod objSenal in lstSenal)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objSenal.StrIdSenal.ToString().Trim(),
                    objSenal.StrCodigoSenal.ToString().Trim(),
                    objSenal.StrDescripcionSenal.ToString().Trim(),
                    objSenal.StrFechaInicial.ToString().Trim(),
                    objSenal.StrFechaFinal.ToString().Trim(),
                    objSenal.StrNumeroCoincidencias.ToString().Trim(),
                    objSenal.StrUsuario.ToString().Trim(),
                    objSenal.StrIdUsuario.ToString().Trim(),
                    objSenal.StrFechaConsulta.ToString().Trim()
                    });
            }
        }
        private void mtdInicializarValoresConsulSenales()
        {
            PagIndex = 0;
        }
        private void mtdMensajeConsulSenales(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        //Excel

        protected void ImButtonExcelExportConsulSenales_Click(object sender, ImageClickEventArgs e)
        {
            exportExcelConsulSenales(Response, "ReporteConsulSenales_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcelConsulSenales(HttpResponse Response, string filename)
        {
            string strErrMsg = null;
            clsDtSenal carga = new clsDtSenal();
            DataTable ConsulModSenales = carga.ConsulModSenales(ref strErrMsg);
            if(ConsulModSenales == null)
            {
                mtdMensajeConsulSenales("Atención! No hay datos para exportar.");
                return;
            }
            ConsulModSenales.TableName = "Reporte Coincidencias";

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(ConsulModSenales);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(ds);
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        /// <summary>
        /// Fin del llamado del Grid de Coincidencia de Señales de alerta
        /// </summary>

    }
}