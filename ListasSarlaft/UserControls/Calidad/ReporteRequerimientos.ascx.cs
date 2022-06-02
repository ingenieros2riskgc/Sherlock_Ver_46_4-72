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
using ClosedXML.Excel;
using System.IO;
using clsDatos;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ListasSarlaft.Classes.Utilerias;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace ListasSarlaft.UserControls.Calidad
{
    public partial class ReporteRequerimientos : System.Web.UI.UserControl
    {
        string IdFormulario = "10001";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();
        cQA cQa = new cQA();
        private static int LastInsertIdCE;
        // Trae las posiciones donde se guardan estos campos
        string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();

        //#region Properties

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                divOpciones.Visible = true;
                divEstado.Visible = false;
                divIntegrantes.Visible = false;
                mtdHideAll();
                mtdCargaIntegrantes();
            }
        }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void mtdHideAll()
        {
            gvGesReq.Visible = false;
        }
        #endregion

        #region Grid properties
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

        #region Drop Down List de elección del tipo de búsqueda
        protected void DDLopciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOpciones.SelectedValue == "0")
            {
                mtdHideAll();
                divEstado.Visible = false;
                divIntegrantes.Visible = false;
                gvGesReq.Visible = false;
            }
            if (ddlOpciones.SelectedValue == "1")
            {
                divEstado.Visible = true;
                divIntegrantes.Visible = false;
                gvGesReq.Visible = false;
            }
            if (ddlOpciones.SelectedValue == "2")
            {

                divIntegrantes.Visible = true;
                divEstado.Visible = false;
                gvGesReq.Visible = false;
            }
        }
        #endregion

        #region Drop Down List de reporte por integrantes
        private void mtdCargaIntegrantes()
        {
            DataTable dtInfo = new DataTable();

            dtInfo = cQa.loadIntegrante ();

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ddlIntegrantes.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreUsuarioSoporte"].ToString().Trim()));
            }
        }

        protected void ddlIntegrantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenar_Encargados();
            gvGesReq.Visible = true;
        }

        protected void llenar_Encargados()
        {
            string strErrMsg = string.Empty;
            string integranteSeleccionado = ddlIntegrantes.SelectedItem.ToString();
            mtdselectEncargado(integranteSeleccionado);
        }

        private void mtdselectEncargado(string integranteSeleccionado)
        {
            DataTable dtInfo = new DataTable();

            string NomIntegrante = string.Empty;

            string ddlEncargadoSeleccionado = integranteSeleccionado.ToString().Trim();
            NomIntegrante = ConsultarIntegrantes(ddlEncargadoSeleccionado);

            mtdInicializarValoresGesReq();
            mtdLoadGridViewGesReque(NomIntegrante);

        }

        private string ConsultarIntegrantes(string ddlEncargadoSeleccionado)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.loadNombreResponsable(ddlEncargadoSeleccionado);
            string strIntegrante = string.Format(dtInfo.Rows[0]["NombreUsuarioSoporte"].ToString().Trim());

            return strIntegrante;
        }

        private void mtdLoadGridViewGesReque(string NomIntegrante)
        {
            mtdLoadGridGesReq();
            mtdLoadInfoGridGesReque(NomIntegrante);
        }

        private void mtdLoadInfoGridGesReque(string NomIntegrante)
        {
            string strErrMsg = string.Empty;
            clsRegistroRequerimientos cRegistroRequerimientos = new clsRegistroRequerimientos();
            List<clsDTOReporteRequerimientos> lstRegistroEvidencias = new List<clsDTOReporteRequerimientos>();

            lstRegistroEvidencias = cRegistroRequerimientos.mtdCargarReportes(NomIntegrante, ref strErrMsg);

            if (lstRegistroEvidencias != null)
            {
                mtdLoadGridGesReq();
                gvGesReq.DataSource = lstRegistroEvidencias;
                gvGesReq.DataBind();
            }
        }
        #endregion

        #region Drop Down List de reporte por estado
        protected void DDLopcionesComentarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLopcionesComentarios.SelectedValue == "0")
            {
                mtdHideAll();

            }
            if (DDLopcionesComentarios.SelectedValue == "1")
            {
                string strEstado = "Abierto";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
            if (DDLopcionesComentarios.SelectedValue == "2")
            {
                string strEstado = "Asignado";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
            if (DDLopcionesComentarios.SelectedValue == "3")
            {
                string strEstado = "En desarollo";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
            if (DDLopcionesComentarios.SelectedValue == "4")
            {
                string strEstado = "En catalogación";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
            if (DDLopcionesComentarios.SelectedValue == "5")
            {
                string strEstado = "En pruebas";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
            if (DDLopcionesComentarios.SelectedValue == "6")
            {
                string strEstado = "Devuelto";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
            if (DDLopcionesComentarios.SelectedValue == "7")
            {
                string strEstado = "Certificado";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
            if (DDLopcionesComentarios.SelectedValue == "8")
            {
                string strEstado = "En producción";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
            if (DDLopcionesComentarios.SelectedValue == "9")
            {
                string strEstado = "Cerrado";
                mtdHideAll();
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq(strEstado);
                gvGesReq.Visible = true;
            }
        }
        #endregion

        #region load Grid reporte por estados
        private void mtdLoadGridViewGesReq(string strEstado)
        {
            mtdLoadGridGesReq();
            mtdLoadInfoGridGesReq(strEstado);
        }

        private void mtdLoadGridGesReq()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("idGESREQ", typeof(string));
            grid.Columns.Add("Empresa", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NumeroREQ", typeof(string));
            grid.Columns.Add("FechaCreacionGESREQ", typeof(string));
            grid.Columns.Add("TipoFalla", typeof(string));
            grid.Columns.Add("GrupoAsignado", typeof(string));
            grid.Columns.Add("Encargado", typeof(string));
            grid.Columns.Add("Estado", typeof(string));
            grid.Columns.Add("Criticidad", typeof(string));
            grid.Columns.Add("FechaVencimientoGESREQ", typeof(string));

            gvGesReq.DataSource = grid;
            gvGesReq.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridGesReq(string strEstado)
        {
            string strErrMsg = string.Empty;
            clsRegistroRequerimientos cRegistroRequerimientos = new clsRegistroRequerimientos();
            List<clsDTOReporteRequerimientos> lstRegistroEvidencias = new List<clsDTOReporteRequerimientos>();

            lstRegistroEvidencias = cRegistroRequerimientos.mtdCargarReporte(strEstado, ref strErrMsg);

            if (lstRegistroEvidencias != null)
            {
                mtdLoadGridGesReq();
                gvGesReq.DataSource = lstRegistroEvidencias;
                gvGesReq.DataBind();
                //ImButtonExcelExportReporte.Visible = true;
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOReporteRequerimientos> lstRegistroRequerimientos)
        {
            DataTable dtInfo = new DataTable();
            
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[]
                        {
                        dtInfo.Rows[rows]["idREGREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["Empresa"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NumeroREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaCreacionREGREQ"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoFalla"].ToString().Trim(),
                        dtInfo.Rows[rows]["GrupoAsignado"].ToString().Trim(),
                        dtInfo.Rows[rows]["Encargado"].ToString().Trim(),
                        dtInfo.Rows[rows]["Estado"].ToString().Trim(),
                        dtInfo.Rows[rows]["Criticidad"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaVencimientoGESREQ"].ToString().Trim()
                }
                        );
                }
                gvGesReq.DataSource = InfoGrid;
                gvGesReq.DataBind();
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

    }
}