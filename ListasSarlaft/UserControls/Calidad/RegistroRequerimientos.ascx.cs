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


namespace ListasSarlaft.UserControls.Calidad
{
    public partial class RegistroRequerimientos : System.Web.UI.UserControl
    {
        string IdFormulario = "10001";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();
        private int pagIndex;
        private DataTable infoGrid;
        private int rowGrid;
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        cAuditoria cAu = new cAuditoria();
        cQA cQa = new cQA();

        #region Properties
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
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.BtnAdjuntar);
                scriptManager.RegisterPostBackControl(this.gvEvidencias);
                this.StrFechaRegistro.Text = System.DateTime.Now.ToString().Trim();
                this.StrNombre.Text = Session["Usuario"].ToString().Trim();
                mtdHideAll();
                inicializarNumeroREQ();
                divCargaPDF.Visible = true;
                FileUpload1.Focus();
                scriptManager.RegisterPostBackControl(this.gvEvidencias);
                divGvGesReq.Visible = false;
                mtdInicializarValoresGesReq();
                mtdLoadGridViewGesReq();
                LblIdReq.Visible = false;
                StrId.Visible = false;
                HideEvidencias();
            }
        }

        protected void ShowEvidencias()
        {
            divTitle.Visible = true;
            gvEvidencias.Visible = true;
            divCargaPDF.Visible = true;
            divBotones.Visible = true;
            btnImgInsertar.Visible = false;
            btnImgCancelar.Visible = false;
        }

        protected void HideEvidencias()
        {
            divTitle.Visible = false;
            gvEvidencias.Visible = false;
            divCargaPDF.Visible = false;
            divBotones.Visible = false;
        }

        protected void mtdHideAll()
        {
            DDLopcionesIncInt.Visible = false;
            DDLopcionesIncExt.Visible = false;
        }
        #endregion

        #region DropDownList
        protected void DDLopcionesRequerimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLopcionesRequerimientos.SelectedValue.ToString() == "0")
            {
                mtdHideAll();
            }
            if (DDLopcionesRequerimientos.SelectedValue.ToString() == "1")
            {
                DDLopcionesIncExt.Visible = true;
                DDLopcionesIncInt.Visible = false;
            }
            if (DDLopcionesRequerimientos.SelectedValue.ToString() == "2")
            {
                DDLopcionesIncInt.Visible = true;
                DDLopcionesIncExt.Visible = false;
            }
            if (DDLopcionesRequerimientos.SelectedValue.ToString() == "3")
            {
                DDLopcionesIncInt.Visible = false;
                DDLopcionesIncExt.Visible = false;
            }
            if (DDLopcionesRequerimientos.SelectedValue.ToString() == "4")
            {
                DDLopcionesIncInt.Visible = false;
                DDLopcionesIncExt.Visible = false;
            }
        }
        
        protected void DDLopcionesIncInt_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DDLopcionesIncExt_SelectedIndexChanged(object sender, EventArgs e)
        {
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

            grid.Columns.Add("StrIdEvidencia", typeof(string));
            grid.Columns.Add("urlArchivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("StrFechaRegistroEvidencia", typeof(string));
            
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
                mtdLoadInfoGrid(lstEvidencias);
                gvEvidencias.DataSource = lstEvidencias;
                gvEvidencias.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTORegistroEvidencias> lstEvidencias)
        {
            foreach (clsDTORegistroEvidencias objRegReq in lstEvidencias)
            {
                for (int rows = 0; rows < InfoGrid.Rows.Count; rows++) {
                    InfoGrid.Rows.Add(
                        new Object[]
                        {
                        InfoGrid.Rows[rows]["StrIdEvidencia"].ToString().Trim(),
                        InfoGrid.Rows[rows]["urlArchivo"].ToString().Trim(),
                        InfoGrid.Rows[rows]["Descripcion"].ToString().Trim(),
                        InfoGrid.Rows[rows]["StrFechaRegistroEvidencia"].ToString().Trim()
                        }
                        );
                }
            }
        }
        

        protected void gvEvidencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string strErrMsg = string.Empty;

            //RowGrid = Convert.ToInt16(e.CommandArgument);
            ////RowGrid = (Convert.ToInt16(gvEvidencias.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            //switch (e.CommandName)
            //{
            //    case "Descargar":
            //        string URLArchivo = InfoGridAdjuntos.Rows[RowGrid]["urlArchivo"].ToString().Trim();

            //        #region Vars
            //        byte[] bPdfData = cAu.mtdDescargarEvidencia(URLArchivo);
            //        #endregion Vars

            //        if (bPdfData != null)
            //        {
            //            Response.Clear();
            //            Response.Buffer = true;
            //            Response.ContentType = "Application/pdf";
            //            Response.AddHeader("Content-Disposition", "attachment; filename=" + URLArchivo);
            //            Response.Charset = "";
            //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //            Response.BinaryWrite(bPdfData);
            //            Response.End();
            //        }
            //        //BtnDescargar_Click(ref strErrMsg);
            //        break;
            //}
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

        #region Registrar PDF

        protected void imgBtnAdjuntar_Click(object sender, ImageClickEventArgs e)
        {
            bool hasFile = FileUpload1.HasFile;
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                {
                    mtdCargarPdfPlanAccion();
                    gvEvidencias.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".docx")
                {
                    mtdCargarPdfPlanAccion();
                    gvEvidencias.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".xlsx")
                {
                    mtdCargarPdfPlanAccion();
                    gvEvidencias.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".txt")
                {
                    mtdCargarPdfPlanAccion();
                    gvEvidencias.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".jpg")
                {
                    mtdCargarPdfPlanAccion();
                    gvEvidencias.DataBind();
                    mtdMensaje("Archivo cargado exitosamente.");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".png")
                {
                    mtdCargarPdfPlanAccion();
                    gvEvidencias.DataBind();
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

        protected void btnImgCancelarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("RegistroRequerimientos.aspx");
        }
        #endregion

        #region Registrar requerimiento
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)   
        {
            if (StrEmpresa.Text != "") {

                if (DDLopcionesRequerimientos.SelectedItem.ToString().Trim() != "--Seleccione una opción--") {

                    if (StrDescripcion.Text != "") {

                        DataTable dtInfo = new DataTable();
                        string strErrMsg = string.Empty;
                        string DDLText;
                        string strId = Sanitizer.GetSafeHtmlFragment(StrId.Text.Trim());

                        if (DDLopcionesRequerimientos.SelectedValue.ToString() == "1") //Incidencia externa
                        {
                            DDLText = DDLopcionesIncExt.SelectedItem.ToString();
                        }
                        else if (DDLopcionesRequerimientos.SelectedValue.ToString() == "2") //Incidencia interna
                        {
                            DDLText = DDLopcionesIncInt.SelectedItem.ToString();
                        }
                        else //Opción de mejora u Orden de trabajo
                        {
                            DDLText = " ";
                        }

                        try
                        {
                            try
                            {
                                strErrMsg = "Requerimiento registrado exitosamente.";
                                gvEvidencias.Visible = true;

                                mtdInsertarRequerimiento(string.Empty,
                                    Session["Usuario"].ToString().Trim(),
                                    Sanitizer.GetSafeHtmlFragment(StrEmpresa.Text.Trim()),
                                    Sanitizer.GetSafeHtmlFragment(StrNumReq.Text),
                                    Sanitizer.GetSafeHtmlFragment(StrFechaRegistro.Text.Trim()),
                                    Sanitizer.GetSafeHtmlFragment(DDLopcionesRequerimientos.SelectedItem.ToString()),
                                    Sanitizer.GetSafeHtmlFragment(DDLText.ToString()),
                                    Sanitizer.GetSafeHtmlFragment(StrDescripcion.Text.Trim()),
                                    Sanitizer.GetSafeHtmlFragment(StrRutaError.Text.Trim()),
                                    ref strErrMsg
                                    );
                                mtdCorregirCaracter(
                                    );
                                mtdGenerarNotificacion(
                                    Session["Usuario"].ToString().Trim(),
                                    StrNumReq.Text.Trim(),
                                    DDLopcionesRequerimientos.SelectedItem.ToString(),
                                    StrDescripcion.Text.Trim(),
                                    StrRutaError.Text.Trim()
                                    );
                                mtdMensaje("Requerimiento creado exitosamente.");
                                ShowEvidencias();
                            }
                            catch (Exception except)
                            {
                                mtdMensaje("Error al registrar el requerimiento." + except.Message.ToString());
                            }
                        }
                        catch (Exception except)
                        {
                            mtdMensaje("Error al registrar el requerimiento." + except.Message.ToString());
                        }
                    }
                    else
                    {
                        mtdMensaje("Debe proporcionar una descripción al caso.");
                    }
                }
                else
                {
                    mtdMensaje("Debe seleccionar una opción diferente para el tipo de requerimiento.");
                }
            }
            else
            {
                mtdMensaje("Debe proporcionar un nombre para el campo Empresa.");
            }

        }

        private void mtdInsertarRequerimiento(string strId, string strNombre, string strEmpresa, string strNumReq,
            string strFechaRegistro, string strTipoFalla, string ddlText, string strDescripcion, string strRutaError, ref string strErrMsg) 
        {
            clsRegistroRequerimientos cRegReq = new clsRegistroRequerimientos();
            clsDTORegistroRequerimientos objRegReq = new clsDTORegistroRequerimientos(strId, strNombre, strEmpresa, strNumReq,
                strFechaRegistro, strTipoFalla, ddlText, strDescripcion, strRutaError);

            cRegReq.mtdInsertarRequerimientos(objRegReq, ref strErrMsg);   
        }

        public void mtdCorregirCaracter()
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("update QA.RegistroRequerimiento set Ruta = replace(replace(Ruta, '&lt;', '<'), '&gt;', '>') ");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Reset campos
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            gvEvidencias.Visible = true;
            mtdResetCampos();
        }

        private void mtdResetCampos()
        {
            DDLopcionesRequerimientos.ClearSelection();
            DDLopcionesIncInt.ClearSelection();
            DDLopcionesIncExt.ClearSelection();

            StrDescripcion.Text = string.Empty;
            StrRutaError.Text = string.Empty;
            StrEmpresa.Text = string.Empty;
        }
        #endregion

        #region Notificación correo
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdGenerarNotificacion(String StrNombre, String StrNumReq, String StrTipoFalla, String StrDescripcion, String StrRutaError)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "NOTIFICACIÓN DE REGISTRO DE REQUERIMIENTO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el registro de un requerimiento con los siguientes detalles: <br>";
                TextoAdicional = TextoAdicional + " Número de requerimiento : " + StrNumReq + "<br>";
                TextoAdicional = TextoAdicional + " Usuario que registra : " + StrNombre + "<br>";
                TextoAdicional = TextoAdicional + " Fecha de la modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Tipo de falla : " + StrTipoFalla + "<br>";
                TextoAdicional = TextoAdicional + " Descripción del requerimiento : " + StrDescripcion + "<br>";
                TextoAdicional = TextoAdicional + " Ruta del error : " + StrRutaError + "<br>";

                boolEnviarNotificacion(
                    StrNumReq, 
                    Convert.ToInt16(Session["IdJerarquia"]), 
                    StrNombre, 
                    StrTipoFalla, 
                    StrDescripcion,
                    StrRutaError, 
                    TextoAdicional
                    );
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }

        private Boolean boolEnviarNotificacion(string StrNombre, int idNodoJerarquia, string StrNumReq, string StrTipoFalla, 
            string StrDescripcion, string StrRutaError, string textoAdicional)
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
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, " +
                        "E.RequiereFechaCierre FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E " +
                        "ON CD.IdEvento = E.IdEvento WHERE E. IdEvento = 106";

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
        
        #region Cargar grid gvRegReq
        private void mtdLoadGridViewGesReq()
        {
            mtdLoadGridGesReq();
            mtdLoadInfoGridGesReq();
        }

        private void mtdLoadGridGesReq()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrId", typeof(string));
            grid.Columns.Add("StrEmpresa", typeof(string));
            grid.Columns.Add("StrNombre", typeof(string));
            grid.Columns.Add("StrNumReq", typeof(string));
            grid.Columns.Add("StrFechaRegistro", typeof(string));
            grid.Columns.Add("StrTipoFalla", typeof(string));
            grid.Columns.Add("DDLText", typeof(string));
            grid.Columns.Add("StrDescripcion", typeof(string));
            grid.Columns.Add("StrRutaError", typeof(string));

            gvGesReq.DataSource = grid;
            gvGesReq.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridGesReq()
        {
            string strErrMsg = string.Empty;
            clsRegistroRequerimientos cConsulReq = new clsRegistroRequerimientos();
            List<clsDTORegistroRequerimientos> lstEvidencias = new List<clsDTORegistroRequerimientos>();

            lstEvidencias = cConsulReq.mtdCargarDatos(ref strErrMsg);

            if (lstEvidencias != null)
            {
                mtdLoadInfoGrid(lstEvidencias);
                gvGesReq.DataSource = lstEvidencias;
                gvGesReq.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTORegistroRequerimientos> lstEvidencias)
        {
            foreach (clsDTORegistroRequerimientos objRegReq in lstEvidencias)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objRegReq.StrId.ToString().Trim(),
                    objRegReq.StrEmpresa.ToString().Trim(),
                    objRegReq.StrNombre.ToString().Trim(),
                    objRegReq.StrNumReq.ToString().Trim(),
                    objRegReq.StrFechaRegistro.ToString().Trim(),
                    objRegReq.StrTipoFalla.ToString().Trim(),
                    objRegReq.DDLText.ToString().Trim(),
                    objRegReq.StrDescripcion.ToString().Trim(),
                    objRegReq.StrRutaError.ToString().Trim()
                    });
            }
        }

        protected void gvGesReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvGesReq.PageIndex = PagIndex;
            gvGesReq.DataSource = InfoGrid;
            gvGesReq.DataBind();

            mtdLoadGridViewGesReq();
        }

        protected void gvGesReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Detalles":
                    if (!string.IsNullOrEmpty(strErrMsg))
                    {
                        strErrMsg = "Error al descargar.";
                    }
                    break;
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
        
        #region adjuntos
        protected void BtnAdjuntar_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                mtdCargarPdfPlanAccion();
                gvEvidencias.DataBind();
                mtdMensaje("Archivo cargado exitosamente.");
            }
            else
            {
                lblMsgBox.Text = "No hay archivos para cargar.";
                mpeMsgBox.Show();
            }
        }

        private void mtdCargarPdfPlanAccion()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cQa.loadEvidencia();

            #region Nombre Archivo

            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}",
                    FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            string StrId = dtInfo.Rows[0]["NumRegistros"].ToString().Trim();

            cQa.mtdAgregarEvidencia(StrId, txtDescArchivo.Text.Trim(), strNombreArchivo, bPdfData);


            gvEvidencias.Visible = true;
            filaSubirAnexos.Visible = true;
            mtdInicializarValoresEvidencias();
            mtdLoadGridViewEvidencias();
            
        }

        private void loadGridAdjuntos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridAdjuntos = grid;
            gvEvidencias.DataSource = InfoGridAdjuntos;
            gvEvidencias.DataBind();

        }

        private void loadInfoAdjuntos(string StrId)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.loadArchivoEvidencia(StrId);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAdjuntos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdEvidencia"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                        });
                }
                gvEvidencias.DataSource = InfoGridAdjuntos;
                gvEvidencias.DataBind();
            }
        }

        private DataTable infoGridAdjuntos;
        private DataTable InfoGridAdjuntos
        {
            get
            {
                infoGridAdjuntos = (DataTable)ViewState["infoGridAdjuntos"];
                return infoGridAdjuntos;
            }
            set
            {
                infoGridAdjuntos = value;
                ViewState["infoGridAdjuntos"] = infoGridAdjuntos;
            }
        }

        private int rowGridArchivoAud;
        private int RowGridArchivoAud
        {
            get
            {
                rowGridArchivoAud = (int)ViewState["rowGridArchivoAud"];
                return rowGridArchivoAud;
            }
            set
            {
                rowGridArchivoAud = value;
                ViewState["rowGridArchivoAud"] = rowGridArchivoAud;
            }
        }

        #endregion

    }
}