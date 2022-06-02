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
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class Tratamientos : System.Web.UI.UserControl
    {
        string IdFormulario = "5023";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.GVtratamientos);
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
        #region Properties
        private DataTable infoGridTratamientos;
        private int rowGridTratamientos;
        private int pagIndexTratamientos;

        private DataTable InfoGridTratamientos
        {
            get
            {
                infoGridTratamientos = (DataTable)ViewState["infoGridTratamientos"];
                return infoGridTratamientos;
            }
            set
            {
                infoGridTratamientos = value;
                ViewState["infoGridTratamientos"] = infoGridTratamientos;
            }
        }

        private int RowGridTratamientos
        {
            get
            {
                rowGridTratamientos = (int)ViewState["rowGridTratamientos"];
                return rowGridTratamientos;
            }
            set
            {
                rowGridTratamientos = value;
                ViewState["rowGridTratamientos"] = rowGridTratamientos;
            }
        }

        private int PagIndexTratamientos
        {
            get
            {
                pagIndexTratamientos = (int)ViewState["pagIndexTratamientos"];
                return pagIndexTratamientos;
            }
            set
            {
                pagIndexTratamientos = value;
                ViewState["pagIndex"] = pagIndexTratamientos;
            }
        }
        #endregion
        private void mtdInicializarValores()
        {
            pagIndexTratamientos = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;

            if (!mtdLoadTratamientos(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            
        }
        protected void mtdResetFields()
        {
            BodyFormT.Visible = false;
            BodyGridT.Visible = true;

            txtId.Text = string.Empty;
            TXtratamiento.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;

        }
        #region LoadGrid
        private bool mtdLoadTratamientos(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOTratamientos objTratamientos = new clsDTOTratamientos();
            List<clsDTOTratamientos> lstTratamientos = new List<clsDTOTratamientos>();
            clsBLLTratamientos cTratamientos = new clsBLLTratamientos();
            #endregion Vars
            lstTratamientos = cTratamientos.mtdConsultarTratamientos(ref lstTratamientos, ref strErrMsg);

            if (lstTratamientos != null)
            {
                mtdLoadGridTratamientos();
                mtdLoadGridTratamientos(lstTratamientos);
                GVtratamientos.DataSource = lstTratamientos;
                GVtratamientos.PageIndex = pagIndexTratamientos;
                GVtratamientos.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay tratamientos registrados";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridTratamientos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdTratamiento", typeof(string));
            grid.Columns.Add("strTratamiento", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));

            GVtratamientos.DataSource = grid;
            GVtratamientos.DataBind();
            InfoGridTratamientos = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridTratamientos(List<clsDTOTratamientos> lstTratamiento)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsDTOTratamientos objTratamiento in lstTratamiento)
            {

                InfoGridTratamientos.Rows.Add(new Object[] {
                    objTratamiento.intIdTratamiento.ToString().Trim(),
                    objTratamiento.strTratamiento.ToString().Trim(),
                    objTratamiento.dtFechaRegistro.ToString().Trim(),
                    objTratamiento.intIdUsuario.ToString().Trim(),
                    objTratamiento.strUsuario.ToString().Trim()
                    });
            }
        }
        #endregion LoadGrid
        #region Metodos
        private bool mtdInsertarTratamientos(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOTratamientos objTratamientos = new clsDTOTratamientos();
            clsBLLTratamientos cTratamientos = new clsBLLTratamientos();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objTratamientos.strTratamiento = Sanitizer.GetSafeHtmlFragment(TXtratamiento.Text.Trim());
            objTratamientos.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objTratamientos.dtFechaRegistro = DateTime.Now;
            
            booResult = cTratamientos.mtdInsertarTratamiento(objTratamientos, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Tratamiento registrado exitosamente";
            }
            else
            {
                strErrMsg = "Error al registrar el tratamiento";
            }
            return booResult;
        }

        protected void mtdShowUpdate(int Rowgrid)
        {
            GridViewRow row = GVtratamientos.Rows[Rowgrid];
            var colsNoVisible = GVtratamientos.DataKeys[Rowgrid].Values;
            txtId.Text = row.Cells[0].Text;
            TXtratamiento.Text = ((Label)row.FindControl("strTratamiento")).Text;
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();
            
        }
        private bool mtdUpdateTratamiento(ref string strErrMsg)
        {
            bool booResult = false;
            clsDTOTratamientos objTratamientos = new clsDTOTratamientos();
            objTratamientos.intIdTratamiento = Convert.ToInt32(txtId.Text);
            objTratamientos.strTratamiento = TXtratamiento.Text;
            clsBLLTratamientos cTratamiento = new clsBLLTratamientos();

            booResult = cTratamiento.mtdUpdateTratamiento(objTratamientos, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Tratamiento actualizada  exitosamente";
            else
                strErrMsg = "Error al actualizar el tratamiento";
            return booResult;
        }
        #endregion Metodos
        #region ButtonEvents
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            BodyFormT.Visible = true;
            BodyGridT.Visible = false;

            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertarTratamientos(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdUpdateTratamiento(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
        }
        #endregion ButtonEvents

        protected void GVtratamientos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndexTratamientos = e.NewPageIndex;
            GVtratamientos.PageIndex = pagIndexTratamientos;
            GVtratamientos.DataBind();
            string strErrMsg = "";
            mtdLoadTratamientos(ref strErrMsg);
        }

        protected void GVtratamientos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridTratamientos = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGridTratamientos);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    BodyFormT.Visible = true;
                    BodyGridT.Visible = false;
                    break;
            }
        }
    }
}