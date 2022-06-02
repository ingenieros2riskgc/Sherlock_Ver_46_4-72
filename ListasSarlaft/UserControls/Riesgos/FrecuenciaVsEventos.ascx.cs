using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class FrecuenciaVsEventos : System.Web.UI.UserControl
    {
        string IdFormulario = "5022";
        cCuenta cCuenta = new cCuenta();
        cRiesgo cRiesgo = new cRiesgo();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.GVprogramaCapacitacion);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            scriptManager.RegisterPostBackControl(this.btnInsertarNuevo);
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
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;

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

        #endregion
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
            loadDDLProbabilidad();
            /*mtdLoadEvaluacionProveedor(ref strErrMsg);*/
            if (!mtdLoadFrecuenciavsEventos(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }
        protected void mtdResetFields()
        {
            BodyFormFVE.Visible = false;
            BodyGridFVE.Visible = true;

            txtId.Text = "";
            TXmaxEvent.Text = "";
            DDLfrecuencia.ClearSelection();
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
        }
        #region Buttons Events
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            BodyGridFVE.Visible = false;
            BodyFormFVE.Visible = true;

            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }
        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdInsertarFrecuenciavsEventos(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
                mtdResetFields();
                mtdStard();
            }
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdUpdateFrecuenciavsEventos(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
                mtdResetFields();
                mtdStard();
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
        }
        #endregion Buttons Events
        #region Loads
        private void loadDDLProbabilidad()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                if (DDLfrecuencia.Items.Count > 1)
                {
                    DDLfrecuencia.Items.Clear();
                    DDLfrecuencia.Items.Insert(0, new ListItem("---", "---"));
                }
                dtInfo = cRiesgo.loadDDLProbabilidad();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLfrecuencia.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProbabilidad"].ToString().Trim(), dtInfo.Rows[i]["IdProbabilidad"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar frecuencia. " + ex.Message, 1, "Atención");
            }
        }
        #endregion Loads
        #region Metodos

        private bool mtdInsertarFrecuenciavsEventos(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOFrecuenciavsEventos objFrecuenciaEvent = new clsDTOFrecuenciavsEventos();
            clsBLLFrecuenciavsEventos cFrequencyEvents = new clsBLLFrecuenciavsEventos();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objFrecuenciaEvent.intEventosMaximos = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TXmaxEvent.Text.Trim()));
            objFrecuenciaEvent.intCodigoFrecuencia = Convert.ToInt32(DDLfrecuencia.SelectedValue);
            objFrecuenciaEvent.strNombreFrecuencia = DDLfrecuencia.SelectedItem.Text;
            objFrecuenciaEvent.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objFrecuenciaEvent.dtFechaRegistro = DateTime.Now;

            booResult = cFrequencyEvents.mtdValidaInsertarFrecuenciavsEventos(objFrecuenciaEvent, ref strErrMsg);
            if (booResult == true)
            {
                booResult = cFrequencyEvents.mtdInsertarFrecuenciavsEventos(objFrecuenciaEvent, ref strErrMsg);
                if (booResult == true)
                {
                    strErrMsg = "Frecuencia de Eventos registrada exitosamente";
                }
                else
                {
                    strErrMsg = "Error al registrar la Frecuencia de Eventos";
                }
            }else
            {
                strErrMsg = "Error al registrar la Frecuencia de Eventos: El Código Frecuencia ya esta ingresado";
            }
            return booResult;
        }
        private bool mtdUpdateFrecuenciavsEventos(ref string strErrMsg)
        {
            bool booResult = false;
            clsDTOFrecuenciavsEventos objFrequencyEvents = new clsDTOFrecuenciavsEventos();
            clsBLLFrecuenciavsEventos cFrenquencyEvents = new clsBLLFrecuenciavsEventos();

            objFrequencyEvents.intIdFrecuenciaEventos = Convert.ToInt32(txtId.Text);
            objFrequencyEvents.intEventosMaximos = Convert.ToInt32(TXmaxEvent.Text);
            objFrequencyEvents.intCodigoFrecuencia = Convert.ToInt32(DDLfrecuencia.SelectedValue);
            objFrequencyEvents.strNombreFrecuencia = DDLfrecuencia.SelectedItem.Text;

            booResult = cFrenquencyEvents.mtdUpdateFrecuenciavsEventos(ref objFrequencyEvents, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Frecuencia de Eventos actualizada  exitosamente";
            else
                strErrMsg = "Error al actualizar la Frecuencia de Eventos";
            return booResult;
        }
        private bool mtdLoadFrecuenciavsEventos(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOFrecuenciavsEventos objFrequencyEvents = new clsDTOFrecuenciavsEventos();
            List<clsDTOFrecuenciavsEventos> lstFrequencyEvents = new List<clsDTOFrecuenciavsEventos>();
            clsBLLFrecuenciavsEventos cFrequencyEvents = new clsBLLFrecuenciavsEventos();
            #endregion Vars
            lstFrequencyEvents = cFrequencyEvents.mtdConsultarFrecuenciavsEventos(ref lstFrequencyEvents, ref strErrMsg);

            if (lstFrequencyEvents != null)
            {
                mtdLoadFrecuenciavsEventos();
                mtdLoadFrecuenciavsEventos(lstFrequencyEvents);
                GVfrecuenciavsEventos.DataSource = lstFrequencyEvents;
                GVfrecuenciavsEventos.PageIndex = pagIndex;
                GVfrecuenciavsEventos.DataBind();
                booResult = true;
            }else
            {
                strErrMsg = "No hay frecuencias registradas";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadFrecuenciavsEventos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdFrecuenciaEventos", typeof(string));
            grid.Columns.Add("intEventosMaximos", typeof(string));
            grid.Columns.Add("intCodigoFrecuencia", typeof(string));
            grid.Columns.Add("strNombreFrecuencia", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));

            GVfrecuenciavsEventos.DataSource = grid;
            GVfrecuenciavsEventos.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstFrequencyEvents">Lista con las Frecuencias de los Eventos</param>
        private void mtdLoadFrecuenciavsEventos(List<clsDTOFrecuenciavsEventos> lstFrequencyEvents)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsDTOFrecuenciavsEventos objFrequencyEvents in lstFrequencyEvents)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objFrequencyEvents.intIdFrecuenciaEventos.ToString().Trim(),
                    objFrequencyEvents.intEventosMaximos.ToString().Trim(),
                    objFrequencyEvents.intCodigoFrecuencia.ToString().Trim(),
                    objFrequencyEvents.strNombreFrecuencia.ToString().Trim(),
                    objFrequencyEvents.dtFechaRegistro.ToString().Trim(),
                    objFrequencyEvents.intIdUsuario.ToString().Trim(),
                    objFrequencyEvents.strUsuario.ToString().Trim()
                    });
            }
        }
        protected void mtdShowUpdate(int Rowgrid)
        {
            //loadDDLProbabilidad();
            GridViewRow row = GVfrecuenciavsEventos.Rows[RowGrid];
            var colsNoVisible = GVfrecuenciavsEventos.DataKeys[RowGrid].Values;
            txtId.Text = row.Cells[0].Text;
            TXmaxEvent.Text = row.Cells[1].Text;
            DDLfrecuencia.SelectedValue = colsNoVisible[0].ToString();
            tbxUsuarioCreacion.Text = colsNoVisible[2].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();
        }
        #endregion Metodos

        protected void GVfrecuenciavsEventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVfrecuenciavsEventos.PageIndex = PagIndex;
            GVfrecuenciavsEventos.DataBind();
            string strErrMsg = "";
            mtdLoadFrecuenciavsEventos(ref strErrMsg);
        }

        protected void GVfrecuenciavsEventos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    BodyFormFVE.Visible = true;
                    BodyGridFVE.Visible = false;
                    break;
            }
        }
    }
}