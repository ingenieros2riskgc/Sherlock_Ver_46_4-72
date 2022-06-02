using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;
//using ListasSarlaft.Classes;
using Microsoft.Security.Application;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class Perfiles : System.Web.UI.UserControl
    {
        string IdFormulario = "11002";
        clsCuenta cCuenta = new clsCuenta();
        private static int LastInsertIdCE;
        ListasSarlaft.Classes.cCuenta ccCuenta = new ListasSarlaft.Classes.cCuenta();

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
        protected void gvPerfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvPerfiles.PageIndex = PagIndex;
            gvPerfiles.DataBind();

            mtdLoadGridView();
        }

        protected void gvPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvPerfiles.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
                    break;
            }
        }
        #endregion Gridview

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
            grid.Columns.Add("StrIdPerfil", typeof(string));
            grid.Columns.Add("StrNombrePerfil", typeof(string));
            grid.Columns.Add("StrValorMinimo", typeof(string));
            grid.Columns.Add("StrValorMaximo", typeof(string));

            gvPerfiles.DataSource = grid;
            gvPerfiles.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGrid()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsPerfil cPerfil = new clsPerfil();
            List<clsDTOPerfil> lstPerfiles = new List<clsDTOPerfil>();
            #endregion Vars

            lstPerfiles = cPerfil.mtdCargarInfoPerfiles(ref strErrMsg);

            if (lstPerfiles != null)
            {
                mtdLoadInfoGrid(lstPerfiles);
                gvPerfiles.DataSource = lstPerfiles;
                gvPerfiles.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOPerfil> lstPerfiles)
        {
            foreach (clsDTOPerfil objPerfil in lstPerfiles)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objPerfil.StrIdUsuario.ToString().Trim(),
                    objPerfil.StrIdPerfil.ToString().Trim(),
                    objPerfil.StrNombrePerfil.ToString().Trim(),
                    objPerfil.StrValorMinimo.ToString().Trim(),
                    objPerfil.StrValorMaximo.ToString().Trim()
                    });
            }
        }
        #endregion Loads

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
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (mtdValidarRango(Sanitizer.GetSafeHtmlFragment(tbValorMinimo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim())))
                    {
                        if (Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim())) <= 100)
                        {
                            mtdAgregarPerfil(Session["IdUsuario"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(tbNombrePerfil.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMinimo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim()), ref strErrMsg);

                            //mtdResetValues();
                            //mtdLoadGridView();
                        }
                        else
                            strErrMsg = "El rango máximo debe ser menor o igual a 100.";
                    }
                    else
                        strErrMsg = "El rango mínimo debe ser inferior o igual al rango máximo.";

                    if (string.IsNullOrEmpty(strErrMsg))
                    {
                        string StrNombrePerfil = tbNombrePerfil.Text.Trim();
                        string StrValorMinimo = tbValorMinimo.Text.Trim();
                        string StrValorMaximo = tbValorMaximo.Text.Trim();
                        mtdGenerarNotificacion(StrNombrePerfil, StrValorMinimo,StrValorMaximo);
                        mtdMensaje("El perfil fue creado exitósamente.");
                    }
                    else
                        mtdMensaje(strErrMsg);
                }
                mtdResetValues();
                mtdLoadGridView();
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar el perfil. [" + ex.Message + "].");
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
                    if (mtdValidarRango(Sanitizer.GetSafeHtmlFragment(tbValorMinimo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim())))
                    {
                        if (Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim())) <= 100)
                        {
                            mtdActualizarPerfil(Session["IdUsuario"].ToString().Trim(), InfoGrid.Rows[RowGrid]["StrIdPerfil"].ToString().Trim(),
                                Sanitizer.GetSafeHtmlFragment(tbNombrePerfil.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMinimo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim()), ref strErrMsg);

                            //mtdResetValues();
                            //mtdLoadGridView();
                        }
                        else
                            strErrMsg = "El rango máximo debe ser menor o igual a 100.";
                    }
                    else
                        strErrMsg = "El rango mínimo debe ser inferior o igual al rango máximo.";

                    if (string.IsNullOrEmpty(strErrMsg))
                    {
                        string StrNombrePerfil = tbNombrePerfil.Text.Trim();
                        string StrValorMinimo = tbValorMinimo.Text.Trim();
                        string StrValorMaximo = tbValorMaximo.Text.Trim();
                        mtdGenerarNotificacion(StrNombrePerfil, StrValorMinimo, StrValorMaximo);
                        mtdMensaje("El perfil fue actualizado exitósamente.");
                    }
                    else
                        mtdMensaje(strErrMsg);
                }
                mtdResetValues();
                mtdLoadGridView();
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al modificar el perfil. " + ex.Message);
            }
        }

        protected void ibtnCancelUpd_Click(object sender, EventArgs e)
        {
            mtdResetValues();
        }
        #endregion

        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdModificar()
        {
            updateUser.Visible = true;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = true;

            tbNombrePerfil.Text = InfoGrid.Rows[RowGrid]["StrNombrePerfil"].ToString().Trim();
            tbValorMinimo.Text = InfoGrid.Rows[RowGrid]["StrValorMinimo"].ToString().Trim();
            tbValorMaximo.Text = InfoGrid.Rows[RowGrid]["StrValorMaximo"].ToString().Trim();
        }

        private void mtdResetValues()
        {
            tbNombrePerfil.Text = string.Empty;
            tbValorMinimo.Text = string.Empty;
            tbValorMaximo.Text = string.Empty;

            updateUser.Visible = false;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = false;
        }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private bool mtdValidarRango(string strValorMinimo, string strValorMaximo)
        {
            bool booResult = true;

            if (Convert.ToInt32(strValorMinimo) > Convert.ToInt32(strValorMaximo))
                booResult = false;

            return booResult;
        }

        private void mtdAgregarPerfil(string strIdUsuario, string strNombrePerfil, string strValorMinimo, string strValorMaximo, ref string strErrMsg)
        {
            clsPerfil cPerfil = new clsPerfil();
            clsDTOPerfilCreate objPerfil = new clsDTOPerfilCreate(strIdUsuario, strNombrePerfil, strValorMinimo, strValorMaximo);

            cPerfil.mtdAgregarPerfilCreate(objPerfil, ref strErrMsg);
        }

        private void mtdActualizarPerfil(string strIdUsuario, string strIdPerfil, string strNombrePerfil, string strValorMinimo, string strValorMaximo, ref string strErrMsg)
        {
            clsPerfil cPerfil = new clsPerfil();
            clsDTOPerfil objPerfil = new clsDTOPerfil(strIdUsuario, strIdPerfil, strNombrePerfil, strValorMinimo, strValorMaximo);

            cPerfil.mtdActualizarPerfil(objPerfil, ref strErrMsg);
        }











        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdGenerarNotificacion(String StrNombrePerfil, String StrValorMinimo, String StrValorMaximo)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "<strong>NOTIFICACIÓN DE MODIFICACIÓN DE PERFILES" + "</strong><br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el cambio de información de perfiles.<br>";
                TextoAdicional = TextoAdicional + " Nombre del perfil : " + StrNombrePerfil + "<br>";
                TextoAdicional = TextoAdicional + " Valor mínimo : " + StrValorMinimo + "<br>";
                TextoAdicional = TextoAdicional + " Valor máximo : " + StrValorMaximo + "<br>";
                TextoAdicional = TextoAdicional + " Fecha de la modificación : " + System.DateTime.Now.ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Usuario de Registro : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Registro : " + Session["nombreUsuario"].ToString() + "<br>";


                boolEnviarNotificacion(StrNombrePerfil, Convert.ToInt16(Session["IdJerarquia"]),StrValorMinimo, StrValorMaximo, TextoAdicional);
            }
            catch (Exception ex)
            {
                //strErrMsg = string.Format("Mensaje de error. [{0}]", ex.Message);
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }

        //String StrIdTipoParametro
        private Boolean boolEnviarNotificacion(String StrNombrePerfil, int idNodoJerarquia,
            String StrValorMinimo, String StrValorMaximo, string textoAdicional)
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

                if (!string.IsNullOrEmpty(StrNombrePerfil.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = 101";

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
                ////Insertar el Registro en la tabla de Correos Enviados
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
                if (RequiereFechaCierre == "SI" && StrNombrePerfil != "")
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