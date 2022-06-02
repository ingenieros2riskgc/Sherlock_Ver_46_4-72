using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Reporting.WebForms;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class ReportePerfilesDetalle : System.Web.UI.UserControl
    {
        string IdFormulario = "11006";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();

        #region Properties
        private DataSets.DataTable infoOpGridFD1;
        private int indexRowOpFD1;
        private int pagOpIndexFD1;

        private DataSets.DataTable infoOpGridFD2;
        private int indexRowOpFD2;
        private int pagOpIndexFD2;

        private DataSets.DataTable infoOpGridFD3;
        private int indexRowOpFD3;
        private int pagOpIndexFD3;

        #region Operador Filtro 1
        private DataSets.DataTable InfoOpGridFD1
        {
            get
            {
                infoOpGridFD1 = (DataSets.DataTable)ViewState["infoOpGridFD1"];
                return infoOpGridFD1;
            }
            set
            {
                infoOpGridFD1 = value;
                ViewState["infoOpGridFD1"] = infoOpGridFD1;
            }
        }

        private int IndexRowOpFD1
        {
            get
            {
                indexRowOpFD1 = (int)ViewState["indexRowOpFD1"];
                return indexRowOpFD1;
            }
            set
            {
                indexRowOpFD1 = value;
                ViewState["indexRowOpFD1"] = indexRowOpFD1;
            }
        }

        private int PagOpIndexFD1
        {
            get
            {
                pagOpIndexFD1 = (int)ViewState["pagOpIndexFD1"];
                return pagOpIndexFD1;
            }
            set
            {
                pagOpIndexFD1 = value;
                ViewState["pagOpIndexFD1"] = pagOpIndexFD1;
            }
        }
        #endregion

        #region Operador Filtro 2
        private DataSets.DataTable InfoOpGridFD2
        {
            get
            {
                infoOpGridFD2 = (DataSets.DataTable)ViewState["infoOpGridFD2"];
                return infoOpGridFD2;
            }
            set
            {
                infoOpGridFD2 = value;
                ViewState["infoOpGridFD2"] = infoOpGridFD2;
            }
        }

        private int IndexRowOpFD2
        {
            get
            {
                indexRowOpFD2 = (int)ViewState["indexRowOpFD2"];
                return indexRowOpFD2;
            }
            set
            {
                indexRowOpFD2 = value;
                ViewState["indexRowOpFD2"] = indexRowOpFD2;
            }
        }

        private int PagOpIndexFD2
        {
            get
            {
                pagOpIndexFD2 = (int)ViewState["pagOpIndexFD2"];
                return pagOpIndexFD2;
            }
            set
            {
                pagOpIndexFD2 = value;
                ViewState["pagOpIndexFD2"] = pagOpIndexFD2;
            }
        }
        #endregion

        #region Operador Filtro 3
        private DataSets.DataTable InfoOpGridFD3
        {
            get
            {
                infoOpGridFD3 = (DataSets.DataTable)ViewState["infoOpGridFD3"];
                return infoOpGridFD3;
            }
            set
            {
                infoOpGridFD3 = value;
                ViewState["infoOpGridFD3"] = infoOpGridFD3;
            }
        }

        private int IndexRowOpFD3
        {
            get
            {
                indexRowOpFD3 = (int)ViewState["indexRowOpFD3"];
                return indexRowOpFD3;
            }
            set
            {
                indexRowOpFD3 = value;
                ViewState["indexRowOpFD3"] = indexRowOpFD3;
            }
        }

        private int PagOpIndexFD3
        {
            get
            {
                pagOpIndexFD3 = (int)ViewState["pagOpIndexFD3"];
                return pagOpIndexFD3;
            }
            set
            {
                pagOpIndexFD3 = value;
                ViewState["pagOpIndexFD3"] = pagOpIndexFD3;
            }
        }
        #endregion

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                mtdInicializarValores();
                mtdLoadDDLPerfiles();
                mtdLoadDDLFiltrosDinamicos();
                mtdLoadGridViewOperadoresFD1();
                mtdLoadGridViewOperadoresFD2();
                mtdLoadGridViewOperadoresFD3();
                //mtdHabilitarControlesFormulaFD1(1);
                //mtdHabilitarControlesFormulaFD2(1);
                //mtdHabilitarControlesFormulaFD3(1);
            }
        }

        #region Loads

        #region DDLs
        private void mtdLoadDDLPerfiles()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsLogica.clsPerfil cPerfil = new clsLogica.clsPerfil();
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

        private void mtdLoadDDLFiltrosDinamicos()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOEstructuraCampo> lstFiltros = new List<clsDTOEstructuraCampo>();
            #endregion Vars

            lstFiltros = cParamArchivo.mtdCargarInfoEstructura(objVariable, ref strErrMsg);

            if (lstFiltros != null)
            {
                int intCounter = 1;

                ddlFiltroDin1.Items.Clear();
                ddlFiltroDin1.Items.Insert(0, new ListItem("", "0"));

                ddlFiltroDin2.Items.Clear();
                ddlFiltroDin2.Items.Insert(0, new ListItem("", "0"));

                ddlFiltroDin3.Items.Clear();
                ddlFiltroDin3.Items.Insert(0, new ListItem("", "0"));

                foreach (clsDTOEstructuraCampo objEstruct in lstFiltros)
                {
                    ddlFiltroDin1.Items.Insert(intCounter, new ListItem(objEstruct.StrNombreCampo, objEstruct.StrPosicion));
                    ddlFiltroDin2.Items.Insert(intCounter, new ListItem(objEstruct.StrNombreCampo, objEstruct.StrPosicion));
                    ddlFiltroDin3.Items.Insert(intCounter, new ListItem(objEstruct.StrNombreCampo, objEstruct.StrPosicion));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }
        #endregion

        #region Operadores

        #region Operador Filtro 1
        private void mtdLoadGridViewOperadoresFD1()
        {
            mtdLoadGridOpsFD1();
            mtdLoadInfoGridOpsFD1();
        }

        private void mtdLoadGridOpsFD1()
        {
            DataSets.DataTable grid = new DataSets.DataTable();

            grid.Columns.Add("StrIdOperador", typeof(string));
            grid.Columns.Add("StrNombreOperador", typeof(string));
            grid.Columns.Add("StrIdentificadorOperador", typeof(string));

            gvOperadorFD1.DataSource = grid;
            gvOperadorFD1.DataBind();
            InfoOpGridFD1 = grid;
        }

        private void mtdLoadInfoGridOpsFD1()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOOperador> lstOps = new List<clsDTOOperador>();

            lstOps = cSenal.mtdCargarInfoOps(ref strErrMsg);

            if (lstOps != null)
            {
                mtdLoadInfoGridOpsFD1(lstOps);
                gvOperadorFD1.DataBind();
            }
        }

        private void mtdLoadInfoGridOpsFD1(List<clsDTOOperador> lstOps)
        {
            foreach (clsDTOOperador objOp in lstOps)
            {
                InfoOpGridFD1.Rows.Add(new Object[] {
                    objOp.StrIdOperador.ToString().Trim(),
                    objOp.StrNombreOperador.ToString().Trim(),
                    objOp.StrIdentificadorOperador.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Operador Filtro 2
        private void mtdLoadGridViewOperadoresFD2()
        {
            mtdLoadGridOpsFD2();
            mtdLoadInfoGridOpsFD2();
        }

        private void mtdLoadGridOpsFD2()
        {
            DataSets.DataTable grid = new DataSets.DataTable();

            grid.Columns.Add("StrIdOperador", typeof(string));
            grid.Columns.Add("StrNombreOperador", typeof(string));
            grid.Columns.Add("StrIdentificadorOperador", typeof(string));

            gvOperadorFD2.DataSource = grid;
            gvOperadorFD2.DataBind();
            InfoOpGridFD2 = grid;
        }

        private void mtdLoadInfoGridOpsFD2()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOOperador> lstOps = new List<clsDTOOperador>();

            lstOps = cSenal.mtdCargarInfoOps(ref strErrMsg);

            if (lstOps != null)
            {
                mtdLoadInfoGridOpsFD2(lstOps);
                gvOperadorFD2.DataBind();
            }
        }

        private void mtdLoadInfoGridOpsFD2(List<clsDTOOperador> lstOps)
        {
            foreach (clsDTOOperador objOp in lstOps)
            {
                InfoOpGridFD2.Rows.Add(new Object[] {
                    objOp.StrIdOperador.ToString().Trim(),
                    objOp.StrNombreOperador.ToString().Trim(),
                    objOp.StrIdentificadorOperador.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Operador Filtro 3
        private void mtdLoadGridViewOperadoresFD3()
        {
            mtdLoadGridOpsFD3();
            mtdLoadInfoGridOpsFD3();
        }

        private void mtdLoadGridOpsFD3()
        {
            DataSets.DataTable grid = new DataSets.DataTable();

            grid.Columns.Add("StrIdOperador", typeof(string));
            grid.Columns.Add("StrNombreOperador", typeof(string));
            grid.Columns.Add("StrIdentificadorOperador", typeof(string));

            gvOperadorFD3.DataSource = grid;
            gvOperadorFD3.DataBind();
            InfoOpGridFD3 = grid;
        }

        private void mtdLoadInfoGridOpsFD3()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOOperador> lstOps = new List<clsDTOOperador>();

            lstOps = cSenal.mtdCargarInfoOps(ref strErrMsg);

            if (lstOps != null)
            {
                mtdLoadInfoGridOpsFD3(lstOps);
                gvOperadorFD3.DataBind();
            }
        }

        private void mtdLoadInfoGridOpsFD3(List<clsDTOOperador> lstOps)
        {
            foreach (clsDTOOperador objOp in lstOps)
            {
                InfoOpGridFD3.Rows.Add(new Object[] {
                    objOp.StrIdOperador.ToString().Trim(),
                    objOp.StrNombreOperador.ToString().Trim(),
                    objOp.StrIdentificadorOperador.ToString().Trim()
                    });
            }
        }
        #endregion

        #endregion

        #endregion Loads

        #region Gridview
        #region Operador

        #region Operador Filtro 1
        protected void gvOperadorFD1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagOpIndexFD1 = e.NewPageIndex;
            gvOperadorFD1.PageIndex = PagOpIndexFD1;
            gvOperadorFD1.DataSource = InfoOpGridFD1;
            gvOperadorFD1.DataBind();
        }

        protected void gvOperadorFD1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRowOpFD1 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecOperador":
                    if (InfoOpGridFD1.Rows[IndexRowOpFD3]["StrNombreOperador"].ToString().Trim().ToUpper() == "ENTRE")
                        mtdHabilitarControlRangoFD1(true);
                    else
                        mtdHabilitarControlRangoFD1(false);

                    mtdSeleccionOperadorFD1();
                    break;
            }
        }
        #endregion

        #region Operador Filtro 2
        protected void gvOperadorFD2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagOpIndexFD2 = e.NewPageIndex;
            gvOperadorFD2.PageIndex = PagOpIndexFD2;
            gvOperadorFD2.DataSource = InfoOpGridFD2;
            gvOperadorFD2.DataBind();
        }

        protected void gvOperadorFD2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRowOpFD2 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecOperador":
                    if (InfoOpGridFD2.Rows[IndexRowOpFD3]["StrNombreOperador"].ToString().Trim().ToUpper() == "ENTRE")
                        mtdHabilitarControlRangoFD2(true);
                    else
                        mtdHabilitarControlRangoFD2(false);

                    mtdSeleccionOperadorFD2();
                    break;
            }
        }
        #endregion

        #region Operador Filtro 3
        protected void gvOperadorFD3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagOpIndexFD3 = e.NewPageIndex;
            gvOperadorFD3.PageIndex = PagOpIndexFD3;
            gvOperadorFD3.DataSource = InfoOpGridFD3;
            gvOperadorFD3.DataBind();
        }

        protected void gvOperadorFD3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRowOpFD3 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecOperador":
                    if (InfoOpGridFD3.Rows[IndexRowOpFD3]["StrNombreOperador"].ToString().Trim().ToUpper() == "ENTRE")
                        mtdHabilitarControlRangoFD3(true);
                    else
                        mtdHabilitarControlRangoFD3(false);

                    mtdSeleccionOperadorFD3();
                    break;
            }
        }
        #endregion

        #endregion
        #endregion

        #region Buttons
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            #region Vars
            string strErrMsg = string.Empty;
            DataSets.DataSet dsReporte = new DataSets.DataSet();
            clsLogica.clsPerfil cPerfil = new clsLogica.clsPerfil();
            #endregion

            trRptPerfiles.Visible = true;

            dsReporte = cPerfil.mtdConsultarRptPerfilesDetalle(Sanitizer.GetSafeHtmlFragment(tbFechaIni.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbFechaFin.Text.Trim()),
                Sanitizer.GetSafeHtmlFragment(tbNroIdentificacion.Text.Trim()), ddlPerfil.SelectedValue.Trim() == "0" ? string.Empty : ddlPerfil.SelectedValue.Trim(),
                ref strErrMsg);

            #region Filtros dinamicos
            if (string.IsNullOrEmpty(strErrMsg))
            {
                if (cbxFD1.Checked)
                    if (!mtdAplicarFiltrosDinamicos(ref dsReporte, ddlFiltroDin1.SelectedValue.Trim(),
                        InfoOpGridFD1.Rows[IndexRowOpFD1]["StrIdentificadorOperador"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbxOtroValorFD1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxDesdeFD1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxHastaFD1.Text.Trim())))
                        strErrMsg = "No se pueden aplicar los filtros dinámicos por falta de información";

                if (cbxFD2.Checked)
                    if (!mtdAplicarFiltrosDinamicos(ref dsReporte, ddlFiltroDin2.SelectedValue.Trim(),
                        InfoOpGridFD2.Rows[IndexRowOpFD2]["StrIdentificadorOperador"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbxOtroValorFD2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxDesdeFD2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxHastaFD2.Text.Trim())))
                        strErrMsg = "No se pueden aplicar los filtros dinámicos por falta de información";

                if (cbxFD3.Checked)
                    if (!mtdAplicarFiltrosDinamicos(ref dsReporte, ddlFiltroDin3.SelectedValue.Trim(),
                        InfoOpGridFD3.Rows[IndexRowOpFD3]["StrIdentificadorOperador"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbxOtroValorFD3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxDesdeFD3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxHastaFD3.Text.Trim())))
                        strErrMsg = "No se pueden aplicar los filtros dinámicos por falta de información";
            }
            #endregion

            #region Condicion generacion reporte
            if (string.IsNullOrEmpty(strErrMsg))
            {
                #region Reporte
                ReportDataSource rdsSource = new ReportDataSource("DataSet1", dsReporte.Tables["DataSet1"]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rdsSource);
                #endregion
            }
            else
            {
                trRptPerfiles.Visible = false;
                mtdMensaje(strErrMsg);
            }
            #endregion

            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mtdLimpiarCampos();
        }
        #endregion

        #region Methods
        private void mtdInicializarValores()
        {
            PagOpIndexFD1 = 0;
            PagOpIndexFD2 = 0;
            PagOpIndexFD3 = 0;
            IndexRowOpFD1 = 0;
            IndexRowOpFD2 = 0;
            IndexRowOpFD3 = 0; 
        }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdLimpiarCampos()
        {
            tbFechaFin.Text = string.Empty;
            tbFechaIni.Text = string.Empty;
            tbNroIdentificacion.Text = string.Empty;
            ddlPerfil.SelectedIndex = 0;
            tbPerfilTemp.Text = string.Empty;
            trRptPerfiles.Visible = false;

            tbxDesdeFD1.Text = string.Empty;
            tbxDesdeFD2.Text = string.Empty;
            tbxDesdeFD3.Text = string.Empty;

            tbxHastaFD1.Text = string.Empty;
            tbxHastaFD2.Text = string.Empty;
            tbxHastaFD3.Text = string.Empty;

            tbxOtroValorFD1.Text = string.Empty;
            tbxOtroValorFD2.Text = string.Empty;
            tbxOtroValorFD3.Text = string.Empty;

            cbxFD1.Checked = false;
            cbxFD2.Checked = false;
            cbxFD3.Checked = false;

            ddlFiltroDin1.SelectedIndex = 0;
            ddlFiltroDin2.SelectedIndex = 0;
            ddlFiltroDin3.SelectedIndex = 0;

        }

        #region Filtro 1
        private void mtdHabilitarControlRangoFD1(bool booActivar)
        {
            lblDesdeFD1.Visible = booActivar;
            lblHastaFD1.Visible = booActivar;
            tbxHastaFD1.Visible = booActivar;
            tbxDesdeFD1.Visible = booActivar;

            tbxOtroValorFD1.Visible = !booActivar;
        }

        private void mtdSeleccionOperadorFD1()
        {
            cbxFD1.Checked = true;

            mtdHabilitarControlesFormulaFD1(3);
        }

        private void mtdHabilitarControlesFormulaFD1(int intFase)
        {
            switch (intFase)
            {
                case 1:
                    gvOperadorFD1.Enabled = false;

                    break;
                case 2:
                    gvOperadorFD1.Enabled = true;

                    break;
                case 3:
                    gvOperadorFD1.Enabled = false;

                    break;
            }
        }
        #endregion

        #region Filtro 2
        private void mtdHabilitarControlRangoFD2(bool booActivar)
        {
            lblDesdeFD2.Visible = booActivar;
            lblHastaFD2.Visible = booActivar;
            tbxHastaFD2.Visible = booActivar;
            tbxDesdeFD2.Visible = booActivar;

            tbxOtroValorFD2.Visible = !booActivar;
        }

        private void mtdSeleccionOperadorFD2()
        {
            cbxFD2.Checked = true;

            mtdHabilitarControlesFormulaFD2(3);
        }

        private void mtdHabilitarControlesFormulaFD2(int intFase)
        {
            switch (intFase)
            {
                case 1:
                    gvOperadorFD2.Enabled = false;

                    break;
                case 2:
                    gvOperadorFD2.Enabled = true;

                    break;
                case 3:
                    gvOperadorFD2.Enabled = false;

                    break;
            }
        }
        #endregion

        #region Filtro 3
        private void mtdHabilitarControlRangoFD3(bool booActivar)
        {
            lblDesdeFD3.Visible = booActivar;
            lblHastaFD3.Visible = booActivar;
            tbxHastaFD3.Visible = booActivar;
            tbxDesdeFD3.Visible = booActivar;
            //ibtnRangoFD3.Visible = booActivar;

            tbxOtroValorFD3.Visible = !booActivar;
            //ibtnSelecOtroValorFD3.Visible = !booActivar;
        }

        private void mtdSeleccionOperadorFD3()
        {
            cbxFD3.Checked = true;
            //tbCodParametroFD3.Text = InfoOpGridFD3.Rows[IndexRowOpFD3]["StrIdentificadorOperador"].ToString().Trim();
            mtdHabilitarControlesFormulaFD3(3);
        }

        private void mtdHabilitarControlesFormulaFD3(int intFase)
        {
            switch (intFase)
            {
                case 1:
                    gvOperadorFD3.Enabled = false;
                    //ibtnSelecOtroValorFD3.Enabled = false;
                    //ibtnRangoFD3.Enabled = false;
                    break;
                case 2:
                    gvOperadorFD3.Enabled = true;
                    //ibtnSelecOtroValorFD3.Enabled = false;
                    break;
                case 3:
                    gvOperadorFD3.Enabled = false;
                    //ibtnSelecOtroValorFD3.Enabled = true;
                    //ibtnRangoFD3.Enabled = true;
                    break;
            }
        }
        #endregion

        bool mtdAplicarFiltrosDinamicos(ref DataSets.DataSet dsReporte,
            string strFiltroDinamico, string strOperador,
            string strOtroValor, string strDesde, string strHasta)
        {
            #region Vars
            string strFiltro = string.Empty;
            bool booResult = false;
            DataSets.DataSet dsTemp = new DataSets.DataSet();
            #endregion

            dsTemp = dsReporte.Copy();

            #region Construccion Filtros
            strFiltro = string.Format("Posicion ='{0}'", strFiltroDinamico);

            #region Filtros
            switch (strOperador)
            {
                case "<":
                    if (!string.IsNullOrEmpty(strOtroValor))
                    {
                        strFiltro = strFiltro + string.Format(" AND ValorCampoArchivo < {0}", strOtroValor);
                        booResult = true;
                    }
                    break;
                case ">":
                    if (!string.IsNullOrEmpty(strOtroValor))
                    {
                        strFiltro = strFiltro + string.Format(" AND ValorCampoArchivo > {0}", strOtroValor);
                        booResult = true;
                    }
                    break;
                case "=":
                    if (!string.IsNullOrEmpty(strOtroValor))
                    {
                        strFiltro = strFiltro + string.Format(" AND ValorCampoArchivo = {0}", strOtroValor);
                        booResult = true;
                    }
                    break;
                case ">=":
                    if (!string.IsNullOrEmpty(strOtroValor))
                    {
                        strFiltro = strFiltro + string.Format(" AND ValorCampoArchivo >= {0}", strOtroValor);
                        booResult = true;
                    }
                    break;
                case "<=":
                    if (!string.IsNullOrEmpty(strOtroValor))
                    {
                        strFiltro = strFiltro + string.Format(" AND ValorCampoArchivo <= {0}", strOtroValor);
                        booResult = true;
                    }
                    break;

                case "Entre":
                    if ((!string.IsNullOrEmpty(strDesde)) && (!string.IsNullOrEmpty(strHasta)))
                    {
                        strFiltro = strFiltro + string.Format(" AND ValorCampoArchivo >= {0} AND ValorCampoArchivo <= {1}", strDesde, strHasta);
                        booResult = true;
                    }
                    break;
            }
            #endregion
            #endregion

            if (booResult)
            {
                dsReporte.Tables["DataSet1"].Clear();
                foreach (DataSets.DataRow row in dsTemp.Tables["DataSet1"].Select(strFiltro))
                {
                    string strFiltro2 = string.Format("IdHistorico = {0}", row[0].ToString().Trim());

                    foreach (DataSets.DataRow row2 in dsTemp.Tables["DataSet1"].Select(strFiltro2))
                    {
                        dsReporte.Tables["DataSet1"].ImportRow(row2);
                    }
                }
            }

            return booResult;
        }

        #endregion

        protected void TheReport_ReportError(object sender, ReportErrorEventArgs e)
        {
            e.Handled = true;
        }

        
    }
}