using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using clsLogica;
using clsDTO;
using Microsoft.Security.Application;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;


namespace ListasSarlaft.UserControls.ConfigEstructura
{
    public partial class TipoParametro : System.Web.UI.UserControl
    {
        string IdFormulario = "10002";
        clsCuenta cCuenta = new clsCuenta();
        private static int LastInsertIdCE;

        #region Properties

        private int pagIndex;
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

        private DataTable infoGrid;
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

        private int rowGrid;
        private string textoAdicional;
        private string strErrMsg;

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
            try
            {
                if (cCuenta.permisosConsulta(Convert.ToInt32(Session["IdUsuario"].ToString()), Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1",false);

                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    mtdLoadGridView();
                }
            }
            catch
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
        }

        #region Gridview
        protected void gvTipoParametro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvTipoParametro.PageIndex = PagIndex;
            gvTipoParametro.DataSource = InfoGrid;
            gvTipoParametro.DataBind();

            mtdLoadGridView();
        }

        protected void gvTipoParametro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvTipoParametro.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
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
                mtdResetValues();
                updateUser.Visible = true;
                ibtnGuardar.Visible = true;
                ibtnGuardarUpd.Visible = false;
            }
        }

        protected void ibtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim())))
                    {
                        mtdAgregarTipoParametro(Session["IdUsuario"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbNombreTipoParametro.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim()), chbActivo.Checked);

                        mtdResetValues();
                        mtdLoadGridView();
                    }
                    else
                        mtdMensaje("Por favor verificar que la calificación sea un número entero.");
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar la variable. [" + ex.Message + "].");
            }
        }

        protected void ibtnGuardarUpd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdActualizarTipoParametro(Session["IdUsuario"].ToString().Trim(), InfoGrid.Rows[RowGrid]["StrIdVariable"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbNombreTipoParametro.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim()), chbActivo.Checked);

                    mtdResetValues();
                    mtdLoadGridView();
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al modificar la variable. " + ex.Message);
            }
        }

        protected void ibtnCancelUpd_Click(object sender, EventArgs e)
        {
            mtdResetValues();
        }
        #endregion

        #region Loads
        private void mtdLoadGridView()
        {
            mtdLoadGrid();
            mtdLoadInfoGrid();
        }

        private void mtdLoadGrid()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdVariable", typeof(string));
            grid.Columns.Add("StrNombreVariable", typeof(string));
            grid.Columns.Add("StrCalificacion", typeof(string));
            grid.Columns.Add("BooActivo", typeof(string));

            gvTipoParametro.DataSource = grid;
            gvTipoParametro.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGrid()
        {
            string strErrMsg = string.Empty;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOVariable> lstTipoParam = new List<clsDTOVariable>();

            lstTipoParam = cParamArchivo.mtdCargarInfoVariables(ref strErrMsg);

            if (lstTipoParam != null)
            {
                mtdLoadInfoGrid(lstTipoParam);
                gvTipoParametro.DataSource = lstTipoParam;
                gvTipoParametro.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOVariable> lstVariable)
        {
            foreach (clsDTOVariable objVariable in lstVariable)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objVariable.StrIdVariable.ToString().Trim(),
                    objVariable.StrNombreVariable.ToString().Trim(),
                    objVariable.StrCalificacion.ToString().Trim(),
                    objVariable.BooActivo
                    });
            }
        }
        #endregion Loads

        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdResetValues()
        {
            tbNombreTipoParametro.Text = string.Empty;
            tbCalificacion.Text = string.Empty;
            chbActivo.Checked = false;

            updateUser.Visible = false;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = false;
        }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private string mtdModificar()
        {
            string PrimCalificacion = InfoGrid.Rows[RowGrid]["StrCalificacion"].ToString().Trim();

            updateUser.Visible = true;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = true;

            tbNombreTipoParametro.Text = InfoGrid.Rows[RowGrid]["StrNombreVariable"].ToString().Trim();
            tbCalificacion.Text = InfoGrid.Rows[RowGrid]["StrCalificacion"].ToString().Trim();
            chbActivo.Checked = InfoGrid.Rows[RowGrid][3].ToString().Trim() == "True" ? true : false;

            return PrimCalificacion;
        }

        private void mtdActualizarTipoParametro(string strIdUsuario, string strIdTipoParametro, string strNombreParametro, string strCalificacion, bool booActivo)
        {
            #region Vars
            int intSumVariables = 0, intSumTemp = 0;
            string strErrMsg = string.Empty;
            ////string PrimCalificacion = InfoGrid.Rows[RowGrid]["StrCalificacion"].ToString().Trim();
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOVariable objTipoParamIn = new clsDTOVariable(strIdUsuario, strIdTipoParametro, strNombreParametro, strCalificacion, booActivo);
            #endregion

            if (clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim())))
            {
                string PrimCalificacion = mtdModificar();
                intSumVariables = cParamArchivo.mtdConsultaSumatoria(ref strErrMsg);
                //string id = Convert.ToInt32(gvTipoParametro.DataKeys[strCalificacion].Value);


                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (Convert.ToInt32(strCalificacion) < Convert.ToInt32(PrimCalificacion))
                    {
                        int intCalc = Convert.ToInt32(PrimCalificacion) - Convert.ToInt32(strCalificacion);
                        intSumTemp = intSumVariables - intCalc;
                    }
                    else if (Convert.ToInt32(strCalificacion) > Convert.ToInt32(PrimCalificacion))
                    {
                        int intCalc = Convert.ToInt32(strCalificacion) - Convert.ToInt32(PrimCalificacion);
                        intSumTemp = intSumVariables + intCalc;
                    }
                    else if(Convert.ToInt32(strCalificacion) == Convert.ToInt32(PrimCalificacion))
                    {
                        intSumTemp = intSumVariables;
                    }

                    if (intSumTemp <= 100)
                    {
                        cParamArchivo.mtdActualizarVariable(objTipoParamIn, ref strErrMsg);
                        
                    }
                    else
                        strErrMsg = "Por favor verificar que la sumatoria de la calificación de todas las variables sea menor o igual a 100.";
                }
            }
            else
                strErrMsg = "Por favor verificar que la calificación sea un número entero.";

            if (string.IsNullOrEmpty(strErrMsg))
            {
                string StrNombreVariable = tbNombreTipoParametro.Text.Trim();
                string StrCalificacion = tbCalificacion.Text.Trim();
                bool BooActivo = false;
                if (chbActivo.Checked)
                    BooActivo = true;
                mtdGenerarNotificacion(StrNombreVariable, StrCalificacion, BooActivo);
                mtdMensaje("El tipo de parámetro fue actualizado exitósamente.");
            }
            else
                mtdMensaje(strErrMsg);

        }

        private void mtdAgregarTipoParametro(string strIdUsuario, string strNombreParametro, string strCalificacion, bool booActivo)
        {
            #region Vars
            string strErrMsg = string.Empty;
            int intSumVariables = 0, intSumTemp = 0;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOVariableCreate objTipoParamIn = new clsDTOVariableCreate(strIdUsuario, strNombreParametro, strCalificacion, booActivo);
            #endregion

            intSumVariables = cParamArchivo.mtdConsultaSumatoria(ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
            {
                intSumTemp = intSumVariables + Convert.ToInt32(strCalificacion);

                if (intSumTemp <= 100)
                    cParamArchivo.mtdAgregarVariableCreate(objTipoParamIn, ref strErrMsg);
                else
                    strErrMsg = "Por favor verificar que la sumatoria de la calificación de todas las variables sea menor o igual a 100.";

                if (string.IsNullOrEmpty(strErrMsg))
                {
 
                    string StrNombreVariable = tbNombreTipoParametro.Text.Trim();
                    string StrCalificacion = tbCalificacion.Text.Trim();
                    bool BooActivo = false;
                    if (chbActivo.Checked)
                        BooActivo = true;
                    mtdGenerarNotificacion(StrNombreVariable, StrCalificacion, BooActivo);
                    mtdMensaje("El tipo de parámetro fue creado exitósamente.");
                }
                else
                    mtdMensaje(strErrMsg);
            }
            else
                mtdMensaje(strErrMsg);
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdGenerarNotificacion(String StrNombreVariable, String StrCalificacion, bool BooActivo)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "<strong>NOTIFICACIÓN DE MODIFICACIÓN DE VARIABLES" + "</strong><br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el cambio de información de variables.<br>";
                TextoAdicional = TextoAdicional + " Nombre de la Variable : " + StrNombreVariable + "<br>";
                TextoAdicional = TextoAdicional + " Calificación de la variable : " + StrCalificacion + "<br>";
                if (BooActivo == true)
                {
                    TextoAdicional = TextoAdicional + " Estado : Variable activa <br>";
                }
                else
                {
                    TextoAdicional = TextoAdicional + " Estado : Variable inactiva <br>";
                }
                TextoAdicional = TextoAdicional + " Fecha de la modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario de Registro : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Registro : " + Session["nombreUsuario"].ToString() + "<br>";
                
                boolEnviarNotificacion(StrNombreVariable, Convert.ToInt16(Session["IdJerarquia"]), StrCalificacion, TextoAdicional);

            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }



        //Enviar notifación al modificar o ingresar categorías
        private Boolean boolEnviarNotificacion(string strNombreVariable, int idNodoJerarquia, 
            string strCalificacion, string textoAdicional)
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

                if (!string.IsNullOrEmpty(strNombreVariable.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = 102";

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
                    //dtblDiscuss.Clear();
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
                strErrMsg = string.Format("Mensaje de error. [{0}]", ex.Message);
                err = true;
            }

            if (!err)
            {
                #region Restro
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (RequiereFechaCierre == "SI" && strNombreVariable != "")
                {
                    ////Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    //SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    //SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    //SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    //SqlDataSource201.InsertParameters["Nombre de la Variable"].DefaultValue = strNombreVariable;
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