using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using System.Configuration;
using ClosedXML.Excel;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class CrlPropiedadClienteProveedor : System.Web.UI.UserControl
    {
        string IdFormulario = "4026";
        cCuenta cCuenta = new cCuenta();
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridCompetencias;
        private int rowGridCompetencias;
        private int pagIndexCompetencias;

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
        private DataTable InfoGridCompetencias
        {
            get
            {
                infoGridCompetencias = (DataTable)ViewState["infoGridCompetencias"];
                return infoGridCompetencias;
            }
            set
            {
                infoGridCompetencias = value;
                ViewState["infoGridCompetencias"] = infoGridCompetencias;
            }
        }

        private int RowGridCompetencias
        {
            get
            {
                rowGridCompetencias = (int)ViewState["rowGridCompetencias"];
                return rowGridCompetencias;
            }
            set
            {
                rowGridCompetencias = value;
                ViewState["rowGridCompetencias"] = rowGridCompetencias;
            }
        }

        private int PagIndexCompetencias
        {
            get
            {
                pagIndexCompetencias = (int)ViewState["pagIndexCompetencias"];
                return pagIndexCompetencias;
            }
            set
            {
                pagIndexCompetencias = value;
                ViewState["pagIndexCompetencias"] = pagIndexCompetencias;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdInicializarValores();

                }
            }
        }
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            mtdLoadControlPropiedad(ref strErrMsg);
            /*if (!mtdCargarDDLs(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            PopulateTreeView();
            mtdLoadControlInfraestructura(ref strErrMsg);*/
        }
        protected void mtdResetFields()
        {
            BodyFormCPCP.Visible = false;
            BodyGridCPCP.Visible = true;

            txtId.Text = "";
            TXDescripcion.Text = "";
            TXcaracteristicas.Text = "";
            TXnombre.Text = "";

            ddlClienteProveedor.ClearSelection();
            TXfechaingreso.Text = "";
            TXfechasalida.Text = "";
            TXobservaciones.Text = "";
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
        }
        private bool mtdLoadControlPropiedad(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsControlPropiedadCP objCrlPropiedad = new clsControlPropiedadCP();
            List<clsControlPropiedadCP> lstCrlPropiedad = new List<clsControlPropiedadCP>();
            clsControlPropiedadCPbll cCrlPropiedad = new clsControlPropiedadCPbll();
            #endregion Vars
            lstCrlPropiedad = cCrlPropiedad.mtdConsultarControlPropiedad(ref lstCrlPropiedad, ref strErrMsg);

            if (lstCrlPropiedad != null)
            {
                mtdLoadControlPropiedad();
                mtdLoadControlPropiedad(lstCrlPropiedad);
                GVpropiedadClienteProveedor.DataSource = lstCrlPropiedad;
                GVpropiedadClienteProveedor.PageIndex = pagIndex;
                GVpropiedadClienteProveedor.DataBind();
                booResult = true;
                GVpropiedadClienteProveedor.Visible = true;
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadControlPropiedad()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdCrlPropiedad", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("strCaracteristicas", typeof(string));
            grid.Columns.Add("strProveedorCliente", typeof(string));
            grid.Columns.Add("strNombre", typeof(string));
            grid.Columns.Add("dtFechaIngreso", typeof(string));
            grid.Columns.Add("dtFechaSalida", typeof(string));
            grid.Columns.Add("strObservaciones", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));
            

            GVpropiedadClienteProveedor.DataSource = grid;
            GVpropiedadClienteProveedor.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadControlPropiedad(List<clsControlPropiedadCP> lstControl)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsControlPropiedadCP objEvaComp in lstControl)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intIdCrlPropiedad.ToString().Trim(),
                    objEvaComp.strDescripcion.ToString().Trim(),
                    objEvaComp.strCaracteristicas.ToString().Trim(),
                    objEvaComp.strProveedorCliente.ToString().Trim(),
                    objEvaComp.dtFechaIngreso.ToString().Trim(),
                    objEvaComp.dtFechaSalida.ToString().Trim(),
                    objEvaComp.strObservaciones.ToString().Trim(),
                    objEvaComp.dtFechaRegistro.ToString().Trim(),
                    objEvaComp.intIdUsuario.ToString().Trim(),
                    objEvaComp.strUsuario.ToString().Trim(),
                    objEvaComp.strNombre.ToString().Trim()
                    });
            }
        }
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            BodyGridCPCP.Visible = false;
            BodyFormCPCP.Visible = true;
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdInsertarControlPropiedad(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
                mtdResetFields();
                mtdStard();
            }
        }
        private bool mtdInsertarControlPropiedad(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsControlPropiedadCP objCrlPropiedad = new clsControlPropiedadCP();
            clsControlPropiedadCPbll cCrlPropiedad = new clsControlPropiedadCPbll();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion

            objCrlPropiedad.strDescripcion = TXDescripcion.Text;
            string[] textos = TXcaracteristicas.Text.Split('\n');
            string caracteristicas = string.Empty;
            if (textos.Length > 0)
            {
                for (int i = 0; i < textos.Length; i++)
                {
                    if (textos[i].ToString() != "")
                        caracteristicas += "|" + (i + 1) + "." + textos[i].ToString();
                }
            }
            objCrlPropiedad.strCaracteristicas = caracteristicas;
            objCrlPropiedad.strProveedorCliente = ddlClienteProveedor.SelectedValue;
            objCrlPropiedad.dtFechaIngreso = TXfechaingreso.Text;
            objCrlPropiedad.dtFechaSalida = TXfechasalida.Text;
            objCrlPropiedad.strObservaciones = TXobservaciones.Text;
            objCrlPropiedad.dtFechaRegistro = DateTime.Now;
            objCrlPropiedad.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objCrlPropiedad.strNombre = TXnombre.Text;
            
            booResult = cCrlPropiedad.mtdInsertarControlPropiedad(objCrlPropiedad, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Control propiedad del cliente o proveedores registrado exitosamente";
            }
            else
            {
                strErrMsg = "Error al registrar control propiedad del cliente o proveedores externos";
            }
            return booResult;
        }
        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdUpdateControlPropiedad(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
        }
        private bool mtdUpdateControlPropiedad(ref string strErrMsg)
        {
            bool booResult = false;
            clsControlPropiedadCP objPrograma = new clsControlPropiedadCP();
            clsControlPropiedadCPbll cPrograma = new clsControlPropiedadCPbll();
            objPrograma.intIdCrlPropiedad = Convert.ToInt32(txtId.Text);
            objPrograma.strDescripcion = TXDescripcion.Text;
            
            string[] textos = TXcaracteristicas.Text.Split('\n');
            string Compromisos = string.Empty;
            if (textos.Length > 0)
            {

                for (int i = 0; i < textos.Length; i++)
                {
                    if (textos[i].ToString() != "\r" && textos[i].ToString() != "")
                    {
                        Compromisos += "|" + textos[i].ToString();
                    }
                }
            }
            objPrograma.strCaracteristicas = Compromisos;
            objPrograma.strProveedorCliente = ddlClienteProveedor.SelectedValue;
            objPrograma.dtFechaIngreso = TXfechaingreso.Text;
            objPrograma.dtFechaSalida = TXfechasalida.Text;
            objPrograma.strObservaciones = TXobservaciones.Text;
            objPrograma.dtFechaRegistro = DateTime.Now;
            objPrograma.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objPrograma.strNombre = TXnombre.Text;

            booResult = cPrograma.mtdUpdateControlPropiedad(ref objPrograma, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Control propiedad del cliente o proveedores externos actualizado  exitosamente";
            else
                strErrMsg = "Error al actualizar control propiedad del cliente o proveedores externos";
            return booResult;
        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            BodyFormCPCP.Visible = false;
            BodyGridCPCP.Visible = true;

            txtId.Text = "";
            TXDescripcion.Text = "";
            TXDescripcion.Enabled = true;
            TXcaracteristicas.Text = "";
            TXcaracteristicas.Enabled = true;
            ddlClienteProveedor.ClearSelection();
            ddlClienteProveedor.Enabled = true;
            TXfechaingreso.Text = "";
            TXfechaingreso.Enabled = true;
            TXfechasalida.Text = "";
            TXobservaciones.Text = "";

            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";

            TXnombre.Text = "";
            TXnombre.Enabled = true;
            mtdStard();
        }

        protected void GVpropiedadClienteProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    BodyFormCPCP.Visible = true;
                    BodyGridCPCP.Visible = false;
                    break;
            }
        }
        protected void mtdShowUpdate(int Rowgrid)
        {
            
            GridViewRow row = GVpropiedadClienteProveedor.Rows[RowGrid];
            var colsNoVisible = GVpropiedadClienteProveedor.DataKeys[RowGrid].Values;
            txtId.Text = row.Cells[0].Text;
            TXDescripcion.Text = ((Label)row.FindControl("strDescripcion")).Text;
            TXDescripcion.Enabled = false;
            //TXcaracteristicas.Text = ((Label)row.FindControl("strCaracteristicas")).Text;
            string reco = ((Label)row.FindControl("strCaracteristicas")).Text;
            string[] texto = reco.Split('|');
            for (int i = 0; i < texto.Length; i++)
            {
                TXcaracteristicas.Text += texto[i].ToString() + Environment.NewLine;
            }
            TXcaracteristicas.Enabled = false;
            ddlClienteProveedor.SelectedValue = ((Label)row.FindControl("strProveedorCliente")).Text;
            ddlClienteProveedor.Enabled = false;
            TXfechaingreso.Text = ((Label)row.FindControl("dtFechaIngreso")).Text;
            TXfechaingreso.Enabled = false;
            TXfechasalida.Text = ((Label)row.FindControl("dtFechaSalida")).Text;
            TXobservaciones.Text = ((Label)row.FindControl("strObservaciones")).Text;
            TXnombre.Text = ((Label)row.FindControl("strNombre")).Text;
            TXnombre.Enabled = false;
            tbxUsuarioCreacion.Text = colsNoVisible[2].ToString();
            txtFecha.Text = colsNoVisible[0].ToString();
            
        }

        protected void GVpropiedadClienteProveedor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndex = e.NewPageIndex;
            string strErrMsg = "";
            mtdLoadControlPropiedad(ref strErrMsg);
        }
    }
}