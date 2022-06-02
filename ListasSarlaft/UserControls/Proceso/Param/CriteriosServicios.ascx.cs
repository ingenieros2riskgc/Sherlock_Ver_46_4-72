using ListasSarlaft.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Proceso.Param
{
    public partial class GestionEvaluacionServicio : System.Web.UI.UserControl
    {
        string IdFormulario = "4030";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            /*scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);*/
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    //PopulateTreeView();
                    mtdInicializarValores();

                }
            }
        }
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
            //txtFecha.Text = "" + DateTime.Now;
            PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            mtdLoadCriterioServicio(ref strErrMsg);
        }
        protected void mtdRestFields()
        {
            BodyGridCS.Visible = true;
            BodyFormCS.Visible = false;

            txtId.Text = "";
            TXrangoIni.Text = "";
            TXrangoFin.Text = "";
            TXdescripcion.Text = "";
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
        }
        #region Properties
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;
        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;
        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

        private DataTable infoGridPrint;
        private int rowGridPrint;

        private DataTable InfoGrid1
        {
            get
            {
                infoGrid1 = (DataTable)ViewState["infoGrid1"];
                return infoGrid1;
            }
            set
            {
                infoGrid1 = value;
                ViewState["infoGrid1"] = infoGrid1;
            }
        }

        private int RowGrid1
        {
            get
            {
                rowGrid1 = (int)ViewState["rowGrid1"];
                return rowGrid1;
            }
            set
            {
                rowGrid1 = value;
                ViewState["rowGrid1"] = rowGrid1;
            }
        }

        private int PagIndex1
        {
            get
            {
                pagIndex1 = (int)ViewState["pagIndex1"];
                return pagIndex1;
            }
            set
            {
                pagIndex1 = value;
                ViewState["pagIndex1"] = pagIndex1;
            }
        }

        private DataTable InfoGrid2
        {
            get
            {
                infoGrid2 = (DataTable)ViewState["infoGrid2"];
                return infoGrid2;
            }
            set
            {
                infoGrid2 = value;
                ViewState["infoGrid2"] = infoGrid2;
            }
        }

        private int RowGrid2
        {
            get
            {
                rowGrid2 = (int)ViewState["rowGrid2"];
                return rowGrid2;
            }
            set
            {
                rowGrid2 = value;
                ViewState["rowGrid2"] = rowGrid2;
            }
        }

        private int PagIndex2
        {
            get
            {
                pagIndex2 = (int)ViewState["pagIndex2"];
                return pagIndex2;
            }
            set
            {
                pagIndex2 = value;
                ViewState["pagIndex2"] = pagIndex2;
            }
        }

        private DataTable InfoGrid3
        {
            get
            {
                infoGrid3 = (DataTable)ViewState["infoGrid3"];
                return infoGrid3;
            }
            set
            {
                infoGrid3 = value;
                ViewState["infoGrid3"] = infoGrid3;
            }
        }
        private int RowGrid3
        {
            get
            {
                rowGrid3 = (int)ViewState["rowGrid3"];
                return rowGrid3;
            }
            set
            {
                rowGrid3 = value;
                ViewState["rowGrid3"] = rowGrid3;
            }
        }

        private int PagIndex3
        {
            get
            {
                pagIndex3 = (int)ViewState["pagIndex3"];
                return pagIndex3;
            }
            set
            {
                pagIndex3 = value;
                ViewState["pagIndex3"] = pagIndex3;
            }
        }


        private DataTable InfoGridPrint
        {
            get
            {
                infoGridPrint = (DataTable)ViewState["infoGridPrint"];
                return infoGridPrint;
            }
            set
            {
                infoGridPrint = value;
                ViewState["infoGridPrint"] = infoGridPrint;
            }
        }

        private int RowGridPrint
        {
            get
            {
                rowGridPrint = (int)ViewState["rowGridPrint"];
                return rowGridPrint;
            }
            set
            {
                rowGridPrint = value;
                ViewState["rowGridPrint"] = rowGridPrint;
            }
        }
        #endregion
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }

            BodyFormCS.Visible = true;
            BodyGridCS.Visible = false;

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

            string strErrMsg = string.Empty;

            if (!mtdInsertarCriterioServicio(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            omb.ShowMessage(strErrMsg, 3, "Atención");
            mtdRestFields();
            mtdStard();

        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }

            string strErrMsg = string.Empty;
            if (!mtdUpdateCriterioServicio(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            omb.ShowMessage(strErrMsg, 3, "Atención");
            mtdRestFields();
            mtdStard();
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdRestFields();
        }
        /// <summary>
        /// Realiza la insercion del CriterioServicio
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>+
        private bool mtdInsertarCriterioServicio(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCriterioServicioBLL cServicioBLL = new clsCriterioServicioBLL();
            clsCriterioServicio cCriterioServicio = new clsCriterioServicio();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            cCriterioServicio.intRangoInicial = Convert.ToDecimal(TXrangoIni.Text);
            cCriterioServicio.intRangoFinal = Convert.ToDecimal(TXrangoFin.Text);
            cCriterioServicio.strDescripcion = TXdescripcion.Text;
            cCriterioServicio.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            cCriterioServicio.dtFechaRegistro = DateTime.Now;

            booResult = cServicioBLL.mtdInsertarCriterioServicio(cCriterioServicio, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Criterio evaluación de servicio registrado exitosamente";
            }
            return booResult;
        }
        private bool mtdLoadCriterioServicio(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCriterioServicio objCrlInfra = new clsCriterioServicio();
            List<clsCriterioServicio> lstCriterio = new List<clsCriterioServicio>();
            clsCriterioServicioBLL cCrtno = new clsCriterioServicioBLL();
            #endregion Vars
            lstCriterio = cCrtno.mtdConsultarCriterioServicio(ref lstCriterio, ref strErrMsg);

            if (lstCriterio != null)
            {
                mtdLoadCriterioServicio();
                mtdLoadCriterioServicio(lstCriterio);
                GVcriteriosServicio.DataSource = lstCriterio;
                GVcriteriosServicio.PageIndex = pagIndex1;
                GVcriteriosServicio.DataBind();
                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadCriterioServicio()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdCriterioServicio", typeof(string));
            grid.Columns.Add("intRangoInicial", typeof(string));
            grid.Columns.Add("intRangoFinal", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));

            GVcriteriosServicio.DataSource = grid;
            GVcriteriosServicio.DataBind();
            InfoGrid1 = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadCriterioServicio(List<clsCriterioServicio> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsCriterioServicio objEvaComp in lstControl)
            {

                InfoGrid1.Rows.Add(new Object[] {
                    objEvaComp.intIdCriterioServicio.ToString().Trim(),
                    objEvaComp.intRangoInicial.ToString().Trim(),
                    objEvaComp.intRangoFinal.ToString().Trim(),
                    objEvaComp.strDescripcion.ToString().Trim(),
                    objEvaComp.dtFechaRegistro.ToString().Trim(),
                    objEvaComp.intIdUsuario.ToString().Trim(),
                    objEvaComp.strNombreUsuario.ToString().Trim()
                    });
            }
        }

        protected void GVcriteriosServicio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGridControl = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGridControl);
                    //Dbutton.Visible = true;
                    break;
            }
        }
        protected void mtdShowUpdate(int Rowgrid)
        {
            BodyFormCS.Visible = true;
            BodyGridCS.Visible = false;

            string strErrMsg = String.Empty;
            GridViewRow row = GVcriteriosServicio.Rows[Rowgrid];
            var colsNoVisible = GVcriteriosServicio.DataKeys[Rowgrid].Values;
            /*string[] list = new string[3];
            list[0] = colsNoVisible[0].ToString();
            list[1] = colsNoVisible[1].ToString();
            list[2] = colsNoVisible[2].ToString();*/
            //int IdControl = Convert.ToInt32(row.Cells[0].Text);
            txtId.Text = row.Cells[0].Text;
            TXrangoIni.Text = row.Cells[1].Text;
            TXrangoFin.Text = row.Cells[2].Text;
            TXdescripcion.Text = ((Label)row.FindControl("descripcion")).Text;
            txtFecha.Text = colsNoVisible[1].ToString();
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();



            IBinsertGVC.Visible = false;
            IBupdateGVC.Visible = true;

        }
        public bool mtdUpdateCriterioServicio(ref string strErrMsg)
        {
            bool booResult = false;
            clsCriterioServicio cServicio = new clsCriterioServicio();
            clsCriterioServicioBLL cServicioBLL = new clsCriterioServicioBLL();

            cServicio.intIdCriterioServicio = Convert.ToInt32(txtId.Text);
            cServicio.intRangoInicial = Convert.ToDecimal(TXrangoIni.Text);
            cServicio.intRangoFinal = Convert.ToDecimal(TXrangoFin.Text);
            cServicio.strDescripcion = TXdescripcion.Text;
            cServicio.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            cServicio.dtFechaRegistro = Convert.ToDateTime(txtFecha.Text);

            booResult = cServicioBLL.mtdUpdateCriterioServicio(cServicio, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Criterio evaluación de servicio modificada exitosamente";
            }
            else
            {
                strErrMsg = "Error al modificar criterio evaluación de servicio";
            }
            return booResult;
        }
    }
}