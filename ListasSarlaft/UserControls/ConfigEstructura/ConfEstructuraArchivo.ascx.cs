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

namespace ListasSarlaft.UserControls.ConfigEstructura
{
    public partial class ConfEstructuraArchivo : System.Web.UI.UserControl
    {
        string IdFormulario = "10001";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();
        private static int LastInsertIdCE;
        // Trae las posiciones donde se guardan estos campos
        string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();

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
        private string textoAdicional;
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
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                mtdInicializarValores();
                mtdLoadGridView();
            }
        }

        #region Gridview
        protected void gvEstructura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvEstructura.PageIndex = PagIndex;
            gvEstructura.DataBind();

            mtdLoadGridView();
        }

        protected void gvEstructura_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvEstructura.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Modificar":

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvRow = gvEstructura.Rows[index];

                    if (!new[] { SenalAlertaPosTipoIden, SenalAlertaPosNumeroIden, SenalAlertaPosNombre }.Any(x => x == gvRow.Cells[8].Text))
                    {
                        mtdLoadDDLTipoParametro();
                        mtdModificar();
                    }
                    else
                    {
                        mtdMensaje("No se permiten modificaciones a este campo");
                        updateUser.Visible = false;
                    }

                    break;
            }
        }
        #endregion Gridview

        #region Buttons

        protected void ibtnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                mtdLoadDDLTipoParametro();
                mtdResetValues();

                //ddlTipoParametro.SelectedIndex = 1;

                updateUser.Visible = true;
                ibtnGuardar.Visible = true;
                ibtnGuardarUpd.Visible = false;
            }
        }

        protected void ibtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (ddlTipoParametro.SelectedIndex != 0)
                    {
                        if (new[] { SenalAlertaPosTipoIden, SenalAlertaPosNumeroIden, SenalAlertaPosNombre }.Any(x => x == tbPosicion.Text))
                        {
                            mtdMensaje("No se puede asignar la posición especificada.");
                            return;
                        }
                        mtdAgregarEstructuraCampo(Session["IdUsuario"].ToString().Trim(),string.Empty, Sanitizer.GetSafeHtmlFragment(tbNombreCampo.Text.Trim()), "1", "0", chbEsParametrico.Checked,
                            ddlTipoParametro.SelectedValue.ToString().Trim(), string.Empty, string.Empty, Sanitizer.GetSafeHtmlFragment(tbPosicion.Text.Trim()), cbNumerico.Checked, ChBEstado.Checked, ref strErrMsg);

                        mtdResetValues();
                        mtdLoadGridView();

                        if (string.IsNullOrEmpty(strErrMsg))
                        {
                            string StrNombreCampo = tbNombreCampo.Text.Trim();
                            string StrPosicion = tbPosicion.Text.Trim();
                            bool BooParametrico = false;
                            if (chbEsParametrico.Checked)
                                BooParametrico = true;
                            mtdGenerarNotificacion(StrNombreCampo, StrPosicion, BooParametrico);
                            mtdMensaje("La estructura fue creada exitósamente.");
                        }
                        else
                            mtdMensaje(strErrMsg);
                    }
                    else
                        mtdMensaje("Por favor modifique el Tipo de Parámetro");
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar el Tipo Parámetro. [" + ex.Message + "].");
            }
        }

        protected void ibtnGuardarUpd_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");

                else
                {
                    if (new[] { SenalAlertaPosTipoIden, SenalAlertaPosNumeroIden, SenalAlertaPosNombre }.Any(x => x == tbPosicion.Text))
                    {
                        mtdMensaje("No se puede asignar la posición especificada.");
                        return;
                    }
                    mtdActualizarEstructura(Session["IdUsuario"].ToString().Trim(), InfoGrid.Rows[RowGrid]["StrIdEstructCampo"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbNombreCampo.Text.Trim()), "1", "0", chbEsParametrico.Checked, ddlTipoParametro.SelectedValue,
                        string.Empty, string.Empty, Sanitizer.GetSafeHtmlFragment(tbPosicion.Text.Trim()),ChBEstado.Checked, cbNumerico.Checked, ref strErrMsg);

                  

                    if (string.IsNullOrEmpty(strErrMsg))
                    {
                        string StrNombreCampo = tbNombreCampo.Text.Trim();
                        string StrPosicion = tbPosicion.Text.Trim();
                        bool BooParametrico = false;
                        if (chbEsParametrico.Checked)
                            BooParametrico = true;
                        mtdGenerarNotificacion(StrNombreCampo, StrPosicion, BooParametrico);
                        mtdMensaje("La estructura fue actualizada exitósamente.");
                            }
                        else
                            mtdMensaje(strErrMsg);
                    }

                mtdResetValues();
                mtdLoadGridView();
                
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al modificar el tipo de parámetro. " + ex.Message);
            }
        }

        protected void ibtnCancelUpd_Click(object sender, EventArgs e)
        {
            mtdResetValues();
        }
        #endregion

        protected void chbEsParametrico_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEsParametrico.Checked)
                ddlTipoParametro.Enabled = true;
            else
                ddlTipoParametro.Enabled = false;
        }

        #region Loads
        private void mtdLoadGridView()
        {
            mtdLoadGrid();
            mtdLoadInfoGrid();
        }

        private void mtdLoadGrid()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdUsuario", typeof(string));
            grid.Columns.Add("StrIdEstructCampo", typeof(string));
            grid.Columns.Add("StrNombreCampo", typeof(string));
            grid.Columns.Add("StrLongitud", typeof(string));
            grid.Columns.Add("BooParametrico", typeof(string));
            grid.Columns.Add("StrIdTipoParametro", typeof(string));
            grid.Columns.Add("StrNombreTipoParametro", typeof(string));
            grid.Columns.Add("StrIdTipoDato", typeof(string));
            grid.Columns.Add("StrNombreTipoDato", typeof(string));
            grid.Columns.Add("StrPosicion", typeof(string));
            grid.Columns.Add("BoolNumerico", typeof(string));

            gvEstructura.DataSource = grid;
            gvEstructura.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGrid()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOEstructuraCampo> lstEstructura = new List<clsDTOEstructuraCampo>();
            #endregion Vars

            lstEstructura = cParamArchivo.mtdCargarInfoEstructura(objVariable, ref strErrMsg);

            if (lstEstructura != null)
            {
                mtdLoadInfoGrid(lstEstructura);
                gvEstructura.DataSource = lstEstructura;
                gvEstructura.DataBind();

                // Se habilita el checkbox del estado de la columna
                foreach (GridViewRow gvrow in gvEstructura.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkEstado");
                    if (lstEstructura.Where(x => x.StrIdEstructCampo == gvrow.Cells[0].Text).Select(o => o.BoolEstado).FirstOrDefault() == true)
                        chk.Checked = true;
                    else
                        chk.Checked = false;
                    if (new[] { SenalAlertaPosTipoIden, SenalAlertaPosNumeroIden, SenalAlertaPosNombre }.Any(x => x == gvrow.Cells[8].Text))
                    {
                        chk.Enabled = false;
                    }
                    CheckBox chkNumerico = (CheckBox)gvrow.FindControl("chkNumerico");
                    if (lstEstructura.Where(x => x.StrIdEstructCampo == gvrow.Cells[0].Text).Select(o => o.BoolNumerico).FirstOrDefault() == true)
                        chkNumerico.Checked = true;
                    else
                        chkNumerico.Checked = false;
                }
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOEstructuraCampo> lstEstructura)
        {
            foreach (clsDTOEstructuraCampo objEstructura in lstEstructura)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objEstructura.StrIdUsuario.ToString().Trim(),
                    objEstructura.StrIdEstructCampo.ToString().Trim(),
                    objEstructura.StrNombreCampo.ToString().Trim(),
                    objEstructura.StrLongitud.ToString().Trim(),
                    objEstructura.BooEsParametrico,
                    objEstructura.StrIdVariable.ToString().Trim(),
                    objEstructura.StrNombreVariable.ToString().Trim(),
                    objEstructura.StrIdTipoDato.ToString().Trim(),
                    objEstructura.StrNombreTipoDato.ToString().Trim(),
                    objEstructura.StrPosicion.ToString().Trim(),
                    objEstructura.BoolNumerico.ToString().Trim()
                    });
            }
        }

        private void mtdLoadDDLTipoParametro()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsDTOVariable objVariableIn = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOVariable> lstVariables = new List<clsDTOVariable>();
            #endregion Vars

            lstVariables = cParamArchivo.mtdCargarInfoVariables(objVariableIn, ref strErrMsg);

            if (lstVariables != null)
            {
                int intCounter = 1;
                ddlTipoParametro.Items.Clear();
                ddlTipoParametro.Items.Insert(0, new ListItem("", "0"));

                foreach (clsDTOVariable objVariable in lstVariables)
                {
                    ddlTipoParametro.Items.Insert(intCounter, new ListItem(objVariable.StrNombreVariable, objVariable.StrIdVariable));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }

        #endregion Loads

        #region Methods
        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdResetValues()
        {
            tbNombreCampo.Text = string.Empty;
            tbTipoDato.Text = string.Empty;
            tbLongitud.Text = string.Empty;
            tbPosicion.Text = string.Empty;
            chbEsParametrico.Checked = false;//
            //ddlTipoParametro.SelectedIndex = 0;

            ddlTipoParametro.Enabled = false;
            updateUser.Visible = false;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = false;
        }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdModificar()
        {
            updateUser.Visible = true;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = true;

            tbNombreCampo.Text = InfoGrid.Rows[RowGrid]["StrNombreCampo"].ToString().Trim();
            tbPosicion.Text = InfoGrid.Rows[RowGrid]["StrPosicion"].ToString().Trim();

            #region CheckBox
            chbEsParametrico.Checked = InfoGrid.Rows[RowGrid][3].ToString().Trim() == "True" ? true : false;

            if (chbEsParametrico.Checked)
                ddlTipoParametro.Enabled = true;
            else
                ddlTipoParametro.Enabled = false;

            cbNumerico.Checked = InfoGrid.Rows[RowGrid][9].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            #region Ciclo ddlTipoParametro

            for (int i = 0; i < ddlTipoParametro.Items.Count; i++)
            {
                ddlTipoParametro.SelectedIndex = i;
                if (ddlTipoParametro.SelectedItem.Text.Trim() == InfoGrid.Rows[RowGrid]["StrNombreTipoParametro"].ToString().Trim())
                    break;
            }
            #endregion Ciclo ddlTipoParametro
        }

        private void mtdAgregarEstructuraCampo(string strIdUsuario, string strIdEstructCampo, string strNombreCampo, string strIdTipoDato, string strLongitud,
            bool booEsParametrico, string strIdTipoParametro, string strNombreTipoParametro, string strNombreTipoDato, string strPosicion,bool booNumerico, bool booEstado,ref string strErrMsg)
        {
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOEstructuraCampo objTipoParamIn = new clsDTOEstructuraCampo(strIdUsuario, strIdEstructCampo, strNombreCampo, strIdTipoDato, strLongitud,
                booEsParametrico, strIdTipoParametro, strNombreTipoParametro, strNombreTipoDato, strPosicion, booEstado, booNumerico);

            cParamArchivo.mtdAgregarEstructuraCampoCreate(objTipoParamIn, ref strErrMsg);
        }

        private void mtdActualizarEstructura(string strIdUsuario, string strIdEstrucCampo, string strNombreCampo, string strIdTipoDato, string strLongitud,
            bool booEsParametrico, string strIdTipoParametro, string strNombreTipoParametro, string strNombreTipoDato, string strPosicion, bool booEstado, bool booNumerico, ref string strErrMsg)
        {
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOEstructuraCampo objTipoParamIn = new clsDTOEstructuraCampo(strIdUsuario, strIdEstrucCampo, strNombreCampo, strIdTipoDato, strLongitud,
                booEsParametrico, strIdTipoParametro, strNombreTipoParametro, strNombreTipoDato, strPosicion, booEstado, booNumerico);

            cParamArchivo.mtdActualizarEstructura(objTipoParamIn, ref strErrMsg);
        }
        #endregion Methods

        protected void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                clsParamArchivo cParamArchivo = new clsParamArchivo();
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox cb1 = (CheckBox)gvEstructura.Rows[index].FindControl("chkEstado");
                int estado = cb1.Checked ? 1 : 0;
                string campo = row.Cells[0].Text;
                cParamArchivo.ActualizarEstadoCampo(Convert.ToInt32(campo), estado);
            }
            catch (Exception ex)
            {
                mtdMensaje($"Error al actualizar el estado {ex.Message}");
            }
        }







        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdGenerarNotificacion(String StrNombreCampo, String StrPosicion,
            bool BooEsParametrico)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "NOTIFICACIÓN DE MODIFICACIÓN DE ESTRUCTURA DE ARCHIVOS" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el cambio de información de estructura de archivos.<br>";
                TextoAdicional = TextoAdicional + " Nombre de la estructura : " + StrNombreCampo + "<br>";
                TextoAdicional = TextoAdicional + " Posición : " + StrPosicion + "<br>";
                if (BooEsParametrico == true)
                {
                    TextoAdicional = TextoAdicional + " Estructura parametizable. <br>";
                }
                else
                {
                    TextoAdicional = TextoAdicional + " Estructura no parametizable. <br>";
                }
                TextoAdicional = TextoAdicional + " Fecha de la modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario de Registro : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Registro : " + Session["nombreUsuario"].ToString() + "<br>";

                boolEnviarNotificacion(StrNombreCampo, Convert.ToInt16(Session["IdJerarquia"]), StrPosicion, TextoAdicional);
            }
            catch (Exception ex)
            {
                //strErrMsg = string.Format("Mensaje de error. [{0}]", ex.Message);
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }

        private Boolean boolEnviarNotificacion(string StrNombreCampo, int idNodoJerarquia,
            string StrPosicion, string textoAdicional)
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

                if (!string.IsNullOrEmpty(StrNombreCampo.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = 103";

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
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
                if (RequiereFechaCierre == "SI" && StrNombreCampo != "")
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



    }
}