using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ListasSarlaft.UserControls.Calidad
{
    public partial class GestionRequerimientos : System.Web.UI.UserControl
    {
        string IdFormulario = "10001";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();
        clsDatabase cDatabase = new clsDatabase();
        cQA cQa = new cQA();
        cAuditoria cAu = new cAuditoria();
        private cError cError = new cError();
        private cDataBase cDataBase = new cDataBase();
        private int pagIndex;
        private DataTable infoGridRequerimientos;
        private int rowGrid;
        private DataTable infoGrid;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(gvEvidencias);
                scriptManager.RegisterPostBackControl(ddlGrupoAsignado);
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq();
                gvGesReq.Visible = true;
                div2.Visible = false;

                divComentarios.Visible = false;
                
                //btnImgActualizar.Visible = false;
                divCrear.Visible = false;
                divActualizar.Visible = false;
            }
        }

        private string mtdNombreEstado(string valor)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.ConsultarNombre(valor);
            if (dtInfo.Rows.Count > 0)
            {
                string estado = string.Format(dtInfo.Rows[0][0].ToString().Trim());
                return estado;
            }
            else
            {
                string estado = "";
                return estado;
            }
            
        }

        protected void mtdColorFila()
        {
            StrTipoFalla.Text = gvGesReq.Rows[RowGrid].Cells[3].Text;
            string valor = StrTipoFalla.Text;
            string estado = mtdNombreEstado(valor);

            if (estado == "Cerrado")
            {
                gvUpdData.Rows[0].Cells[1].BackColor = Color.Orange;
                gvUpdData.Rows[0].Cells[2].BackColor = Color.Orange;
                gvUpdData.Rows[0].Cells[3].BackColor = Color.Orange;
                gvUpdData.Rows[0].Cells[4].BackColor = Color.Orange;
                gvUpdData.Rows[0].Cells[5].BackColor = Color.Orange;
                gvUpdData.Rows[0].Cells[6].BackColor = Color.Orange;
                gvUpdData.Rows[0].Cells[7].BackColor = Color.Orange;
                mtdMensaje("Requerimiento finalizado. No podrá crear un nuevo control.");
                divDdlOptions.Visible = false;
                btnImgInsertar.Visible = false;
                divCrear.Visible = false;
            }
            else
            {
                divCrear.Visible = true;
            }
        }

        protected void ResetCampos()
        {
            ddlGrupoAsignado.ClearSelection();
            ddlEncargado.ClearSelection();
            ddlEstado.ClearSelection();
            ddlCriticidad.ClearSelection();

            StrFechaVencimiento.Text = string.Empty;

            divCrear.Visible = false;
            divActualizar.Visible = false;
        }

        protected void mtdHideAll()
        {
            divDatosGesReq.Visible = false;
            divDdlOptions.Visible = false;


            div1.Visible = false;
            div2.Visible = false;
            div3.Visible = false;
            gvEvidencias.Visible = false;
            btnImgInsertar.Visible = false;
            btnImgCancelar.Visible = false;
            gvComentarios.Visible = false;
            gvUpdData.Visible = false;
        }

        protected void mtdShowAll()
        {

            divDatosGesReq.Visible = true;
            divDdlOptions.Visible = true;

            div1.Visible = true;
            div3.Visible = true;
            gvEvidencias.Visible = true;
            btnImgInsertar.Visible = true;
            btnImgCancelar.Visible = true;
        }
        #endregion

        #region Grid properties
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

        private DataTable infoGridUpdData;

        private DataTable InfoGridUpdData
        {
            get
            {
                infoGridUpdData = (DataTable)ViewState["infGrid2"];
                return infoGridUpdData;
            }
            set
            {
                infoGridUpdData = value;
                ViewState["infGrid2"] = infoGridUpdData;
            }
        }

        private DataTable InfoGridRequerimientos
        {
            get
            {
                infoGridRequerimientos = (DataTable)ViewState["infGrid2"];
                return infoGridRequerimientos;
            }
            set
            {
                infoGridRequerimientos = value;
                ViewState["infGrid2"] = infoGridRequerimientos;
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
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion

        #region Grid gvGesReq
        private void mtdLoadGridViewGesReq()
        {
            mtdLoadGridGesReq();
            mtdLoadInfoGridGesReq();
        }

        private void mtdLoadGridGesReq()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("idREGREQ", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("Empresa", typeof(string));
            grid.Columns.Add("NumeroREQ", typeof(string));
            grid.Columns.Add("FechaCreacionREGREQ", typeof(string));
            grid.Columns.Add("TipoFalla", typeof(string));
            grid.Columns.Add("DetallesTipoFalla", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Ruta", typeof(string));

            gvGesReq.DataSource = grid;
            gvGesReq.DataBind();
            InfoGridRequerimientos = grid;
        }

        private void mtdLoadInfoGridGesReq()
        {
            string strErrMsg = string.Empty;
            clsRegistroRequerimientos cConsulReq = new clsRegistroRequerimientos();
            List<clsDTORegistroRequerimientos> lstEvidencias = new List<clsDTORegistroRequerimientos>();

            lstEvidencias = cConsulReq.mtdCargarDatos(ref strErrMsg);

            if (lstEvidencias != null)
            {
                mtdLoadGridGesReq();
                mtdLoadInfoGrid(lstEvidencias);
                gvGesReq.DataSource = lstEvidencias;
            }
        }

        private void mtdLoadInfoGrid(List<clsDTORegistroRequerimientos> lstEvidencias)
        {
            DataTable dtInfo = new DataTable();

            string strId = string.Empty;

            dtInfo = cQa.ConsultaGestionRequerimientos();   
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRequerimientos.Rows.Add(new Object[]
                        {
                        dtInfo.Rows[rows]["idREGREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["Empresa"].ToString().Trim(),
                        dtInfo.Rows[rows]["NumeroREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaCreacionREGREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoFalla"].ToString().Trim(),
                        dtInfo.Rows[rows]["DetalleTipoFalla"].ToString().Trim(),
                        dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ruta"].ToString().Trim()
                }
                        );
                }
                gvGesReq.DataSource = InfoGridRequerimientos;
                gvGesReq.DataBind();
            }
        }

        protected void gvGesReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvGesReq.PageIndex = PagIndex;
            gvGesReq.DataSource = InfoGridRequerimientos;
            gvGesReq.DataBind();

            mtdLoadGridViewGesReq();
        }

        protected void gvGesReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid = Convert.ToInt16(e.CommandArgument);
            ddlEncargado.Items.Clear();
            ddlGrupoAsignado.Items.Clear();

            switch (e.CommandName)
            {
                case "Detalles":
                    BtnDetalles_Click(sender, e);

                    if (!string.IsNullOrEmpty(strErrMsg))
                    {
                        strErrMsg = "Error al descargar.";
                    }
                    break;
            }
        }

        private void BtnDetalles_Click(object sender, EventArgs e)
        {
            mtdShowAll();
            StrId.Text = gvGesReq.Rows[RowGrid].Cells[0].Text;
            StrEmpresa.Text = gvGesReq.Rows[RowGrid].Cells[1].Text;
            StrNombre.Text = gvGesReq.Rows[RowGrid].Cells[2].Text;
            StrNumReq.Text = gvGesReq.Rows[RowGrid].Cells[3].Text;
            StrFechaRegistro.Text = gvGesReq.Rows[RowGrid].Cells[4].Text;
            StrTipoFalla.Text = gvGesReq.Rows[RowGrid].Cells[5].Text;
            StrDetalleTipoFalla.Text = gvGesReq.Rows[RowGrid].Cells[6].Text;
            StrDescripcion.Text = gvGesReq.Rows[RowGrid].Cells[7].Text;
            StrRutaError.Text = gvGesReq.Rows[RowGrid].Cells[8].Text;
            gvGesReq.Visible = true;

            mtdInicializarValoresEvidencias();
            mtdLoadGridViewEvidencias();
            mtdInicializarValoresUpdData();
            mtdLoadGridViewUpdData();
            gvUpdData.Visible = true;
            divCrear.Visible = true;
            divActualizar.Visible = false;
            gvGesReq.Visible = false;

            mtdColorFila();
            
            mtdCargaGrupos();


        }

        private void mtdCargaGrupos()
        {
            DataTable dtInfo = new DataTable();
            
            dtInfo = cQa.loadNombreGrupo();

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ddlGrupoAsignado.Items.Insert(i,new ListItem(dtInfo.Rows[i]["NombreGrupoSoporte"].ToString().Trim()));
            }
            llenar_Encargados();
        }

        private void mtdselectEncargado(string grupoSeleccionado)
        {
            DataTable dtInfo = new DataTable();

            string IdGrupoAsignado = string.Empty;

            string ddlEncargadoSeleccionado = grupoSeleccionado.ToString().Trim();
            IdGrupoAsignado = ConsultarGruposs(ddlEncargadoSeleccionado);
            
            dtInfo = cQa.loadNombreIntegrantes(IdGrupoAsignado);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ddlEncargado.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreUsuarioSoporte"].ToString().Trim()));
            }
        }

        private void mtdInicializarValoresGesReq()
        {
            PagIndex = 0;
        }

        private void mtdMensajeGesReq(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion

        #region Grid gvUpdData
        private void mtdLoadGridViewUpdData()
        {
            mtdLoadGridUpdData();
            mtdLoadInfoGridUpdData();
        }

        private void mtdLoadGridUpdData()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("idGESREQ", typeof(string));
            grid.Columns.Add("NumeroREQ", typeof(string));
            grid.Columns.Add("GrupoAsignado", typeof(string));
            grid.Columns.Add("Encargado", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            grid.Columns.Add("Criticidad", typeof(string));
            grid.Columns.Add("FechaVencimientoGESREQ", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));

            gvUpdData.DataSource = grid;
            gvUpdData.DataBind();
            InfoGridUpdData = grid;
        }

        private void mtdLoadInfoGridUpdData()
        {
            string strErrMsg = string.Empty;

            string strNumeroREQ = StrNumReq.Text.Trim();

            clsRegistroRequerimientos cConsulReq = new clsRegistroRequerimientos();
            List<clsDTOActualizarGestionRequerimientos> lstEvidencias = new List<clsDTOActualizarGestionRequerimientos>();

            lstEvidencias = cConsulReq.mtdCargarGestion(strNumeroREQ, ref strErrMsg);

            if (lstEvidencias != null)
            {
                mtdLoadGridUpdData();
                mtdLoadInfoGridUpdData(lstEvidencias);
                gvUpdData.DataSource = lstEvidencias;
            }
        }

        private void mtdLoadInfoGridUpdData(List<clsDTOActualizarGestionRequerimientos> lstEvidencias)
        {
            DataTable dtInfo = new DataTable();

            string strNumeroREQ = StrNumReq.Text.Trim();

            dtInfo = cQa.mtdConsultaGestion(strNumeroREQ);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridUpdData.Rows.Add(new Object[]
                        {
                        dtInfo.Rows[rows]["idGESREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["NumeroREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["GrupoAsignado"].ToString().Trim(),
                        dtInfo.Rows[rows]["Encargado"].ToString().Trim(),
                        dtInfo.Rows[rows]["Estado"].ToString().Trim(),
                        dtInfo.Rows[rows]["Criticidad"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaVencimientoGESREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                }
                        );
                }
                gvUpdData.DataSource = InfoGridUpdData;
                gvUpdData.DataBind();
            }
        }

        protected void gvUpdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvGesReq.PageIndex = PagIndex;
            gvGesReq.DataSource = InfoGridUpdData;
            gvGesReq.DataBind();

            mtdLoadGridViewGesReq();
        }

        protected void gvUpdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Detalles":
                    BtnDetallesUpdData_Click(sender, e);    //Arreglar rowcommand
                    if (!string.IsNullOrEmpty(strErrMsg))
                    {
                        strErrMsg = "Error al descargar.";
                    }
                    break;
            }
        }

        private void BtnDetallesUpdData_Click(object sender, EventArgs e)
        {
            mtdShowAll();
            
            btnImgInsertar.Visible = false;

            mtdInicializarValoresEvidencias();
            mtdLoadGridViewEvidencias();
            divCrear.Visible = false;
            divActualizar.Visible = true;

        }

        private void mtdInicializarValoresUpdData()
        {
            PagIndex = 0;
        }

        private void mtdMensajeUpdData(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion

        #region Cargar grid gvEvidencias
        private void mtdLoadGridViewEvidencias()
        {
            mtdLoadGridEvidencias();
            mtdLoadInfoGridEvidencias();
        }

        private void inicializarNumeroREQ()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.NuevoId();
            if (dtInfo.Rows.Count > 0)
            {
                string valor = string.Format("REQ{0}", dtInfo.Rows[0]["NumRegistros"].ToString().Trim());

                StrNumReq.Text = valor;
            }
            else
            {
                string valor = string.Format("REQ01");
                StrNumReq.Text = valor;
            }
        }

        private string inicializarIdEvidencia()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.ConsulIdEvidencia();
            string strId = string.Format(dtInfo.Rows[0]["NumRegistros"].ToString().Trim());
            return strId;
        }

        private void mtdLoadGridEvidencias()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("IdEvidencia", typeof(string));
            grid.Columns.Add("URLArchivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));

            gvEvidencias.DataSource = infoGrid;
            gvEvidencias.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridEvidencias()
        {
            string strErrMsg = string.Empty;
            clsRegistroRequerimientos cRegistroRequerimientos = new clsRegistroRequerimientos();
            List<clsDTORegistroEvidencias> lstEvidencias = new List<clsDTORegistroEvidencias>();

            inicializarIdEvidencia();
            string strId = inicializarIdEvidencia();
            lstEvidencias = cRegistroRequerimientos.mtdCargarEvidencias(ref strErrMsg, strId);

            if (lstEvidencias != null)
            {
                mtdLoadGridEvidencias();
                mtdLoadInfoGrid(lstEvidencias);
                gvEvidencias.DataSource = lstEvidencias;
                //gvEvidencias.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTORegistroEvidencias> lstEvidencias)
        {
            DataTable dtInfo = new DataTable();

            string strIdEvidencia = StrId.Text;

            string strId = string.Empty;

            dtInfo = cQa.ConsultaEvidencias(strIdEvidencia);    //llena la tabla
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(
                        new Object[]
                        {
                        dtInfo.Rows[rows]["IdEvidencia"].ToString().Trim(),
                        dtInfo.Rows[rows]["URLArchivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdUsuario"].ToString().Trim()
                        }
                        );
                }
                gvEvidencias.DataSource = InfoGrid;
                gvEvidencias.DataBind();
            }
        }

        protected void gvEvidencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid = Convert.ToInt16(e.CommandArgument);
            //RowGrid = (Convert.ToInt16(gvEvidencias.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdInicializarValoresEvidencias();
                    mtdLoadGridViewEvidencias();
                    string URLArchivo = InfoGrid.Rows[RowGrid]["URLArchivo"].ToString().Trim();
                    mtdDescargarEvidencia(URLArchivo);

                    break;
            }
        }

        private void mtdDescargarEvidencia(string URLArchivo)
        {

            byte[] bPdfData = cAu.mtdDescargarEvidencia(URLArchivo);

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + URLArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();

            }
        }

        private void mtdInicializarValoresEvidencias()
        {
            PagIndex = 0;
        }

        private void mtdMensajeEvidencias(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion

        #region Grid gvComentarios
        private void mtdLoadGridViewComentarios()
        {
            mtdLoadGridComentarios();
            mtdLoadInfoGridComentarios();
        }

        private void mtdLoadGridComentarios()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("URLArchivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));

            gvComentarios.DataSource = grid;
            gvComentarios.DataBind();
            InfoGrid = grid;
        }

        private string inicializarIdComentario()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.ConsulIdComentario();

            if (dtInfo.Rows.Count > 0)
            {
                string strIdComentarios = string.Format(dtInfo.Rows[0]["NumRegistros"].ToString().Trim());
                return strIdComentarios;
            }
            else
            {
                string strIdComentarios = string.Format("1");
                return strIdComentarios;
            }
        }

        private void mtdLoadInfoGridComentarios()
        {
            string strErrMsg = string.Empty;
            clsRegistroRequerimientos cGestionRequerimientos = new clsRegistroRequerimientos();
            DataTable lstComentarios = new DataTable();

            string IdComentarios = inicializarIdComentario();

            lstComentarios = cGestionRequerimientos.mtdCargarComentarios(ref strErrMsg, IdComentarios);

            if (lstComentarios != null)
            {
                mtdLoadGridComentarios();
                mtdLoadInfoGridComentarios(lstComentarios);
                //gvComentarios.DataSource = lstComentarios;
                //gvComentarios.DataBind();
            }
        }

        private void mtdLoadInfoGridComentarios(DataTable lstComentarios)
        {
            DataTable dtInfo = new DataTable();

            string strId = StrId.Text;

            //string strId = string.Empty;

            /*dtInfo = cQa.ConsultaComentario(); */   //llena la tabla
            if (lstComentarios.Rows.Count > 0)
            {
                for (int rows = 0; rows < lstComentarios.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(
                        new Object[]
                        {
                        lstComentarios.Rows[rows]["IdComentario"].ToString().Trim(),
                        lstComentarios.Rows[rows]["URLArchivo"].ToString().Trim(),
                        lstComentarios.Rows[rows]["Descripcion"].ToString().Trim(),
                        lstComentarios.Rows[rows]["FechaRegistro"].ToString().Trim()
                        }
                        );
                }
                gvComentarios.DataSource = InfoGrid;
                gvComentarios.DataBind();
            }
        }

        protected void gvComentarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvGesReq.PageIndex = PagIndex;
            gvGesReq.DataSource = InfoGrid;
            gvGesReq.DataBind();

            mtdLoadGridViewGesReq();
        }

        protected void gvComentarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Detalles":
                    BtnDetalless_Click(ref strErrMsg);    //Arreglar rowcomman
                    if (!string.IsNullOrEmpty(strErrMsg))
                    {
                        strErrMsg = "Error al descargar.";
                    }
                    break;
            }
        }

        private void BtnDetalless_Click(ref string strErrMsg)
        {
            mtdShowAll();
        }

        private void mtdInicializarValoresComentarios()
        {
            PagIndex = 0;
        }

        private void mtdMensajeComentarios(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion

        #region Registro de Gestión de requerimientos
        protected void ddlEncargado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
        }


        protected void ddlGrupoAsignado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEncargado.Items.Clear();
            llenar_Encargados();       
        }

        protected void llenar_Encargados()
        {
            string strErrMsg = string.Empty;
            string grupoSeleccionado = ddlGrupoAsignado.SelectedItem.ToString();
            mtdselectEncargado(grupoSeleccionado);
        }

        private string ConsultarGruposs(string ddlEncargadoSeleccionado)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.loadIdGrupo(ddlEncargadoSeleccionado);
            string strId = string.Format(dtInfo.Rows[0]["idGrupoSoporte"].ToString().Trim());

            return strId;
        }
        private string ConsultarIntegrantess(string IdGrupoAsignado)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.loadNombreIntegrantes(IdGrupoAsignado);
            string strId = string.Format(dtInfo.Rows[RowGrid]["NombreUsuarioSoporte"].ToString().Trim());

            return strId;
        }
        
        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedValue.ToString() == "0")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "1")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "2")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "3")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "4")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "5")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "6")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "7")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "8")
            {
            }
            if (ddlEstado.SelectedValue.ToString() == "9")
            {
            }
        }

        protected void ddlCriticidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCriticidad.SelectedValue.ToString() == "0")
            {
            }
            if (ddlCriticidad.SelectedValue.ToString() == "1")
            {
            }
            if (ddlCriticidad.SelectedValue.ToString() == "2")
            {
            }
        }



        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtInfo = new DataTable();
            string strErrMsg = string.Empty;
            string DDLText;
            string strId = Sanitizer.GetSafeHtmlFragment(StrId.Text.Trim());
            string grupoAsignado = Sanitizer.GetSafeHtmlFragment(ddlGrupoAsignado.SelectedItem.Text.Trim());
 
            try
            {
                try
                {
                    strErrMsg = "Control de requerimiento registrado exitosamente.";
                    gvEvidencias.Visible = true;

                    mtdInsertarGestionRequerimiento(string.Empty,
                        Session["Usuario"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(StrEmpresa.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(StrNumReq.Text),
                        Sanitizer.GetSafeHtmlFragment(StrFechaRegistro.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(StrTipoFalla.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(StrDetalleTipoFalla.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(StrDescripcion.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(StrRutaError.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(ddlGrupoAsignado.SelectedItem.ToString()),
                        Sanitizer.GetSafeHtmlFragment(ddlEncargado.SelectedItem.ToString()),
                        Sanitizer.GetSafeHtmlFragment(ddlEstado.SelectedItem.ToString()),
                        Sanitizer.GetSafeHtmlFragment(ddlCriticidad.SelectedItem.ToString()),
                        Sanitizer.GetSafeHtmlFragment(StrFechaVencimiento.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(StrComentario.Text.Trim()),
                        ref strErrMsg
                        );
                    mtdCorregirCaracter(
                                    );
                    mtdGenerarNotificacion(
                        Session["Usuario"].ToString().Trim(),
                        StrNumReq.Text.Trim(),
                        StrEmpresa.Text.Trim(),
                        StrDetalleTipoFalla.Text.Trim(),
                        StrDescripcion.Text.Trim(),
                        StrRutaError.Text.Trim(),
                        ddlGrupoAsignado.SelectedItem.ToString(),
                        ddlEncargado.SelectedItem.ToString(),
                        ddlEstado.SelectedItem.ToString(),
                        ddlCriticidad.SelectedItem.ToString(),
                        StrFechaVencimiento.Text.Trim()
                        );
                    mtdColorFila();
                    mtdMensaje("Control de requerimiento creado exitosamente.");
                }
                catch (Exception except)
                {
                    mtdMensaje("Error al registrar el control de requerimiento." + except.Message.ToString());
                }
            }
            catch (Exception except)
            {
                mtdMensaje("Error al registrar el control de requerimiento." + except.Message.ToString());
            }
            div2.Visible = true;
            divComentarios.Visible = true;
            mtdInicializarValoresComentarios();
            mtdLoadGridViewComentarios();
            mtdInicializarValoresUpdData();
            mtdLoadGridViewUpdData();

        }

        private void mtdInsertarGestionRequerimiento(string IdGESREQ, string usuario, string empresa, string numeroREQ, string fechaCreacionGESREQ,
            string tipoFalla, string detalleTipoFalla, string descripcion, string ruta, string grupoAsignado, string encargado, string estado,
            string criticidad, string fechaVencimientoGESREQ, string comentario, ref string strErrMsg)
        {
            clsRegistroRequerimientos cGesReq = new clsRegistroRequerimientos();
            clsDTOGestionRequerimientos objGesReq = new clsDTOGestionRequerimientos(IdGESREQ, usuario, empresa, numeroREQ, fechaCreacionGESREQ,
                tipoFalla, detalleTipoFalla, descripcion, ruta, grupoAsignado, encargado, estado, criticidad, fechaVencimientoGESREQ, 
                comentario);

            cGesReq.mtdInsertarControlRequerimientos(objGesReq, ref strErrMsg);
        }

        public void mtdCorregirCaracter()
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("update QA.GestionRequerimiento set Comentario = replace(replace(Comentario, '&lt;', '<'), '&gt;', '>') ");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            gvEvidencias.Visible = false;
            gvComentarios.Visible = false;
            mtdHideAll();
            divComentarios.Visible = false;
            //btnImgActualizar.Visible = false;
            ResetCampos();
            gvGesReq.Visible = true;
        }
        #endregion

        #region Actualizar Encargado Gestión de requerimientos
        //protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        //{
        //    DataTable dtInfo = new DataTable();
        //    string strErrMsg = string.Empty;
        //    string DDLText;
        //    string strId = Sanitizer.GetSafeHtmlFragment(StrId.Text.Trim());
        //    string grupoAsignado = Sanitizer.GetSafeHtmlFragment(ddlGrupoAsignado.SelectedItem.Text.Trim());


        //    try
        //    {
        //        try
        //        {
        //            strErrMsg = "Control de requerimiento actualizado exitosamente.";
        //            gvEvidencias.Visible = true;

        //            mtdActualizarRequerimiento(Sanitizer.GetSafeHtmlFragment(StrId.Text),
        //                Sanitizer.GetSafeHtmlFragment(StrNumReq.Text),
        //                Sanitizer.GetSafeHtmlFragment(ddlGrupoAsignado.SelectedItem.ToString()),
        //                Sanitizer.GetSafeHtmlFragment(ddlEncargado.SelectedItem.ToString()),
        //                Sanitizer.GetSafeHtmlFragment(ddlEstado.SelectedItem.ToString()),
        //                Sanitizer.GetSafeHtmlFragment(ddlCriticidad.SelectedItem.ToString()),
        //                Sanitizer.GetSafeHtmlFragment(StrFechaVencimiento.Text.Trim()),
        //                ref strErrMsg
        //                );
        //            mtdGenerarActualizarNotificacion(
        //                StrNumReq.Text.Trim(),
        //                ddlGrupoAsignado.SelectedItem.ToString(),
        //                ddlEncargado.SelectedItem.ToString(),
        //                ddlEstado.SelectedItem.ToString(),
        //                ddlCriticidad.SelectedItem.ToString(),
        //                StrFechaVencimiento.Text.Trim()
        //                );
        //            mtdMensaje("Control de requerimiento actualizado exitosamente.");
        //        }
        //        catch (Exception except)
        //        {
        //            mtdMensaje("Error al actualizar el control de requerimiento." + except.Message.ToString());
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        mtdMensaje("Error al actualizar el control de requerimiento." + except.Message.ToString());
        //    }
        //    div2.Visible = true;
        //    divComentarios.Visible = true;
        //    mtdInicializarValoresUpdData();
        //    mtdLoadGridViewUpdData();

        //}

        //private void mtdActualizarRequerimiento(string IdGESREQ, string numeroREQ, string grupoAsignado, string encargado, string estado,
        //    string criticidad, string fechaVencimientoGESREQ, ref string strErrMsg)
        //{
        //    clsRegistroRequerimientos cGesReq = new clsRegistroRequerimientos();
        //    clsDTOActualizarGestionRequerimientos objGesReq = new clsDTOActualizarGestionRequerimientos(IdGESREQ, numeroREQ, grupoAsignado,
        //        encargado, estado, criticidad, fechaVencimientoGESREQ);

        //    string strNumeroGES = StrNumReq.Text.Trim();

        //    cGesReq.mtdActualizarControlRequerimientos(strNumeroGES, objGesReq, ref strErrMsg);
        //}
        #endregion

        #region Registrar Adjunto comentario
        protected void imgBtnAdjuntar_Click(object sender, ImageClickEventArgs e)
        {
            bool hasFile = FileUpload1.HasFile;
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                {
                    mtdCargarPdfPlanAccion();
                    gvComentarios.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".docx")
                {
                    mtdCargarPdfPlanAccion();
                    gvComentarios.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".xlsx")
                {
                    mtdCargarPdfPlanAccion();
                    gvComentarios.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".txt")
                {
                    mtdCargarPdfPlanAccion();
                    gvComentarios.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".jpg")
                {
                    mtdCargarPdfPlanAccion();
                    gvComentarios.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".png")
                {
                    mtdCargarPdfPlanAccion();
                    gvComentarios.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else
                {
                    mtdMensaje("Error al cargar el archivo.");
                }
            }
            else
            {
                lblMsgBox.Text = "No hay archivos para cargar.";
                mpeMsgBox.Show();
            }

        }

        private void mtdCargarPdfPlanAccion()
        {
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;

            dtInfo = cQa.loadComentario();

            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}",
                    FileUpload1.FileName.ToString().Trim());

            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);

            //string StrId = dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
            string StrI = StrId.Text.Trim();

            //cQa.mtdAgregarComentario(StrId, txtDescArchivo.Text.Trim(), strNombreArchivo, bPdfData);
            cQa.mtdAgregarComentario(StrI, txtDescArchivo.Text.Trim(), strNombreArchivo, bPdfData);


            gvComentarios.Visible = true;
            filaSubirAnexos.Visible = true;
            mtdInicializarValoresComentarios();
            mtdLoadGridViewComentarios();
            mtdInicializarValoresEvidencias();
            mtdLoadGridViewEvidencias();

        }

        protected void btnImgCancelarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            gvEvidencias.Visible = false;
            gvComentarios.Visible = false;
            mtdHideAll();
            divComentarios.Visible = false;
            //btnImgActualizar.Visible = false;
            ResetCampos();
            gvGesReq.Visible = true;
        }
        #endregion

        #region Envío de notificación al crear requerimiento
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdGenerarNotificacion(String StrNombre, String StrNumReq, String StrEmpresa, String StrDetalleTipoFalla,
            String StrDescripcion, String StrRutaError, String ddlGrupoAsignado, String ddlEncargado, String ddlEstado, String ddlCriticidad,
            String StrFechaVencimiento)    
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "NOTIFICACIÓN DE REGISTRO DE CONTROL DE REQUERIMIENTO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el registro de un requerimiento con los siguientes detalles: <br>";
                TextoAdicional = TextoAdicional + " Número de requerimiento : " + StrNumReq + "<br>";
                TextoAdicional = TextoAdicional + " Empresa : " + StrEmpresa + "<br>";
                TextoAdicional = TextoAdicional + " Usuario que registra : " + StrNombre + "<br>";
                TextoAdicional = TextoAdicional + " Fecha de la modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Tipo de falla : " + StrDetalleTipoFalla + "<br>";
                TextoAdicional = TextoAdicional + " Descripción del requerimiento : " + StrDescripcion + "<br>";
                TextoAdicional = TextoAdicional + " Ruta del error : " + StrRutaError + "<br>";
                TextoAdicional = TextoAdicional + " Grupo asignado : " + ddlGrupoAsignado + "<br>";
                TextoAdicional = TextoAdicional + " Encargado responsable : " + ddlEncargado + "<br>";
                TextoAdicional = TextoAdicional + " Estados de la solicitud : " + ddlEstado + "<br>";
                TextoAdicional = TextoAdicional + " Criticidad : " + ddlCriticidad + "<br>";
                TextoAdicional = TextoAdicional + " Fecha de vencimiento : " + StrFechaVencimiento + "<br>";

                boolEnviarNotificacion(StrNumReq, Convert.ToInt16(Session["IdJerarquia"]), StrNombre, StrEmpresa, StrDetalleTipoFalla,
                    StrDescripcion, StrRutaError, ddlGrupoAsignado, ddlEncargado, ddlEstado, ddlCriticidad, StrFechaVencimiento, TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }

        private Boolean boolEnviarNotificacion(string StrNombre, int idNodoJerarquia, string StrNumReq, string StrEmpresa, string StrDetalleTipoFalla,
            string StrDescripcion, string StrRutaError, string ddlGrupoAsignado, string ddlEncargado, string ddlEstado, string ddlCriticidad,
            string StrFechaVencimiento, string TextoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = string.Empty, Copia = string.Empty, Asunto = string.Empty, Otros = string.Empty, Cuerpo = string.Empty, NroDiasRecordatorio = string.Empty;
            string selectCommand = string.Empty, AJefeInmediato = string.Empty, AJefeMediato = string.Empty, RequiereFechaCierre = string.Empty;
            string idJefeInmediato = string.Empty, idJefeMediato = string.Empty;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Variables

            try
            {
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(StrNombre.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = 107";

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = TextoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Destinatario
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = " + idNodoJerarquia;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = row["CorreoResponsable"].ToString().Trim();
                        idJefeInmediato = row["idPadre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeInmediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeInmediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                            idJefeMediato = row["idPadre"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region correo del Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeMediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeMediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                //SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                //SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                //SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                //SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                //SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                //SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                //SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                //SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                //SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                //SqlDataSource200.Insert();
                #endregion
            }
            catch (Exception ex)
            {
                // Handle the Exception.
                Mensaje("Error en el envío de la notificación. " + ex.Message);
                err = true;
            }

            if (!err)
            {
                #region Restro
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (RequiereFechaCierre == "SI" && StrNombre != "")
                {
                    ////Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    //SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    //SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    //SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    //SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = StrNombreParametro;
                    //SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    //SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    //SqlDataSource201.Insert();
                }
                #endregion

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");
                    message.From = fromAddress;//here you can set address

                    #region Destinatario
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region Copia
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region Otros
                    if (Otros.Trim() != "")
                        foreach (string substr in Otros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    message.Subject = Asunto;//subject of email
                    message.IsBodyHtml = true;//To determine email body is html or not
                    message.Body = Cuerpo;

                    smtpClient.Send(message);
                    #endregion
                }
                catch (Exception ex)
                {
                    Mensaje("Error en el envío de la notificación. " + ex.Message);
                    err = true;
                }

                if (!err)
                {
                    ////Actualiza el Estado del Correo Enviado
                    //SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    //SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    //SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    //SqlDataSource200.Update();
                }
            }

            return (err);
        }

        #endregion

        #region Envío de notificación al actualizar requerimiento
        
        private void mtdGenerarActualizarNotificacion(String StrNumReq, String ddlGrupoAsignado, String ddlEncargado, String ddlEstado, 
            String ddlCriticidad, String StrFechaVencimiento)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "NOTIFICACIÓN DE ACTUALIZACIÓN DE CONTROL DE REQUERIMIENTO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo una actualización de asignación de responsable de requerimiento con los siguientes detalles: <br>";
                TextoAdicional = TextoAdicional + " Número de requerimiento : " + StrNumReq + "<br>";
                TextoAdicional = TextoAdicional + " Grupo asignado : " + ddlGrupoAsignado + "<br>";
                TextoAdicional = TextoAdicional + " Encargado responsable : " + ddlEncargado + "<br>";
                TextoAdicional = TextoAdicional + " Estados de la solicitud : " + ddlEstado + "<br>";
                TextoAdicional = TextoAdicional + " Criticidad : " + ddlCriticidad + "<br>";
                TextoAdicional = TextoAdicional + " Fecha de vencimiento : " + StrFechaVencimiento + "<br>";

                boolEnviarNotificacion(Convert.ToInt16(Session["IdJerarquia"]), ddlGrupoAsignado, ddlEncargado, ddlEstado, 
                    ddlCriticidad, StrFechaVencimiento, TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }

        private Boolean boolEnviarNotificacion( int idNodoJerarquia, string ddlGrupoAsignado, string ddlEncargado, string ddlEstado, 
            string ddlCriticidad, string StrFechaVencimiento, string TextoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = string.Empty, Copia = string.Empty, Asunto = string.Empty, Otros = string.Empty, Cuerpo = string.Empty, NroDiasRecordatorio = string.Empty;
            string selectCommand = string.Empty, AJefeInmediato = string.Empty, AJefeMediato = string.Empty, RequiereFechaCierre = string.Empty;
            string idJefeInmediato = string.Empty, idJefeMediato = string.Empty;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Variables

            try
            {
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(StrNombre.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = 107";

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = TextoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Destinatario
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = " + idNodoJerarquia;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = row["CorreoResponsable"].ToString().Trim();
                        idJefeInmediato = row["idPadre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeInmediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeInmediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                            idJefeMediato = row["idPadre"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region correo del Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeMediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeMediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                //SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                //SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                //SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                //SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                //SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                //SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                //SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                //SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                //SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                //SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                //SqlDataSource200.Insert();
                #endregion
            }
            catch (Exception ex)
            {
                // Handle the Exception.
                Mensaje("Error en el envío de la notificación. " + ex.Message);
                err = true;
            }

            if (!err)
            {
                #region Restro
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (RequiereFechaCierre == "SI" && ddlGrupoAsignado != "")
                {
                    ////Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    //SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    //SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    //SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    //SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = StrNombreParametro;
                    //SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    //SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    //SqlDataSource201.Insert();
                }
                #endregion

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");
                    message.From = fromAddress;//here you can set address

                    #region Destinatario
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region Copia
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region Otros
                    if (Otros.Trim() != "")
                        foreach (string substr in Otros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    message.Subject = Asunto;//subject of email
                    message.IsBodyHtml = true;//To determine email body is html or not
                    message.Body = Cuerpo;

                    smtpClient.Send(message);
                    #endregion
                }
                catch (Exception ex)
                {
                    Mensaje("Error en el envío de la notificación. " + ex.Message);
                    err = true;
                }

                if (!err)
                {
                    ////Actualiza el Estado del Correo Enviado
                    //SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    //SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    //SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    //SqlDataSource200.Update();
                }
            }

            return (err);
        }

        #endregion
    }
}