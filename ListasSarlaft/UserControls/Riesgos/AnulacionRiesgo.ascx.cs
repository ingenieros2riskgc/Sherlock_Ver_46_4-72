using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using System.IO;
using Microsoft.Security.Application;
using System.Configuration;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class AnulacionRiesgo : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo(); 
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "5008";
        private static int LastInsertIdCE;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                inicializarValores();
                loadDDLCadenaValor();
                loadGridRiesgoAnulado();
                loadInfoRiesgoAnulado();
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGridAnulacionRiesgo = 0;
        }

        private void loadDDLCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCadenaValor();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {                    
                    DropDownList19.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar cadena valor. " + ex.Message);
            }
        }

        protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList20.Items.Clear();
            DropDownList20.Items.Insert(0, new ListItem("---", "---"));
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList19.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(DropDownList19.SelectedValue.ToString().Trim(), 3);
            }
        }

        protected void DropDownList20_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList20.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList20.SelectedValue.ToString().Trim(), 3);
            }
        }

        protected void DropDownList21_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList21.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(DropDownList21.SelectedValue.ToString().Trim(), 3);
            }
        }

        private void loadDDLMacroproceso(String IdCadenaValor, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLMacroproceso(IdCadenaValor);
                switch (Tipo)
                {
                    case 3:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList20.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar macroproceso. " + ex.Message);
            }
        }

        private void loadDDLProceso(String IdMacroproceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLProceso(IdMacroproceso);
                switch (Tipo)
                {
                    case 3:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList21.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar proceso. " + ex.Message);
            }
        }

        private void loadDDLSubProceso(String IdProceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubProceso(IdProceso);
                switch (Tipo)
                {
                    case 3:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList22.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar subproceso. " + ex.Message);
            }
        }

        private void loadGridRiesgoAnulado()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRiesgoAnulado", typeof(string));
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("MotivoAnulacion", typeof(string));
            grid.Columns.Add("FechaAnulacion", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));            
            InfoGridRiesgoAnulado = grid;
            GridView1.DataSource = InfoGridRiesgoAnulado;
            GridView1.DataBind();
        }

        private void loadInfoRiesgoAnulado()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoRiesgoAnulado();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRiesgoAnulado.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgoAnulado"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["MotivoAnulacion"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["FechaAnulacion"].ToString().Trim(),
                                                                 dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim()
                                                                });
                }
                GridView1.PageIndex = PagIndexInfoGridAnulacionRiesgo;
                GridView1.DataSource = InfoGridRiesgoAnulado;
                GridView1.DataBind();
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()) == "" && Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()) == "" && DropDownList19.SelectedValue.ToString().Trim() == "---" && DropDownList20.SelectedValue.ToString().Trim() == "---" && DropDownList21.SelectedValue.ToString().Trim() == "---" && DropDownList22.SelectedValue.ToString().Trim() == "---")
                {
                    Mensaje("Debe ingresar por lo menos un parámetro de consulta.");
                }
                else
                {                    
                    loadGridRiesgos();
                    loadInfoRiesgos();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void ImageButton18_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesConsulta();
            resetValues();
            loadGridRiesgos();
        }

        private void loadGridRiesgos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRiesgo", typeof(string));            
            grid.Columns.Add("NombreClasificacionRiesgo", typeof(string));            
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));            
            InfoGridRiesgos = grid;
            GridView2.DataSource = InfoGridRiesgos;
            GridView2.DataBind();
        }

        private void loadInfoRiesgos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoRiesgos(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()), DropDownList19.SelectedValue.ToString().Trim(), DropDownList20.SelectedValue.ToString().Trim(), DropDownList21.SelectedValue.ToString().Trim(), DropDownList22.SelectedValue.ToString().Trim(), "---");
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),                                                           
                                                           dtInfo.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim(),                                                           
                                                           dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Descripcion"].ToString().Trim()
                                                          });
                }
                GridView2.DataSource = InfoGridRiesgos;
                GridView2.DataBind();
            }
            else
            {
                loadGridRiesgos();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties
        private int rowGridRiesgos;
        private int RowGridRiesgos
        {
            get
            {
                rowGridRiesgos = (int)ViewState["rowGridRiesgos"];
                return rowGridRiesgos;
            }
            set
            {
                rowGridRiesgos = value;
                ViewState["rowGridRiesgos"] = rowGridRiesgos;
            }
        }

        private DataTable infoGridRiesgos;
        private DataTable InfoGridRiesgos
        {
            get
            {
                infoGridRiesgos = (DataTable)ViewState["infoGridRiesgos"];
                return infoGridRiesgos;
            }
            set
            {
                infoGridRiesgos = value;
                ViewState["infoGridRiesgos"] = infoGridRiesgos;
            }
        }

        private DataTable infoGridRiesgoAnulado;
        private DataTable InfoGridRiesgoAnulado
        {
            get
            {
                infoGridRiesgoAnulado = (DataTable)ViewState["infoGridRiesgoAnulado"];
                return infoGridRiesgoAnulado;
            }
            set
            {
                infoGridRiesgoAnulado = value;
                ViewState["infoGridRiesgoAnulado"] = infoGridRiesgoAnulado;
            }
        }

        private int pagIndexInfoGridAnulacionRiesgo;
        private int PagIndexInfoGridAnulacionRiesgo
        {
            get
            {
                pagIndexInfoGridAnulacionRiesgo = (int)ViewState["pagIndexInfoGridAnulacionRiesgo"];
                return pagIndexInfoGridAnulacionRiesgo;
            }
            set
            {
                pagIndexInfoGridAnulacionRiesgo = value;
                ViewState["pagIndexInfoGridAnulacionRiesgo"] = pagIndexInfoGridAnulacionRiesgo;
            }
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridAnulacionRiesgo = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridAnulacionRiesgo;
            GridView1.DataSource = InfoGridRiesgoAnulado;
            GridView1.DataBind();
        }

        private void resetValuesConsulta()
        {
            TextBox11.Text = "";
            TextBox17.Text = "";
            DropDownList19.SelectedIndex = 0;
            DropDownList20.Items.Clear();
            DropDownList20.Items.Insert(0, new ListItem("---", "---"));
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));            
        }

        private void resetValues()
        {
            Label4.Text = "";
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridRiesgos = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    resetValues();
                    detalleRiesgo();                    
                    break;
            }
        }

        private void detalleRiesgo()
        {
            int IdRiesgo = Convert.ToInt32(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            List<clsAnulacionRiesgoDTO> lstEventosR = new List<clsAnulacionRiesgoDTO>();
            clsAnulacionRiesgoBLL cAnulacion = new clsAnulacionRiesgoBLL();
            string strErrMsg = string.Empty;
            lstEventosR = cAnulacion.mtdEventosRiesgos(ref lstEventosR, ref strErrMsg, IdRiesgo);
            if(lstEventosR != null)
            {
                if (lstEventosR.Count > 0)
                {
                    TReventos.Visible = true;
                    TReventosText.Visible = true;
                    TRbuttonDel.Visible = false;
                    GVeventosRiesgos.DataSource = lstEventosR;
                    GVeventosRiesgos.DataBind();

                }
                else
                {
                    TReventos.Visible = false;
                    TReventosText.Visible = false;
                    TRbuttonDel.Visible = true;
                }
                    
                
            }
            else
            {
                TReventos.Visible = false;
                TRbuttonDel.Visible = true;
            }
                
            Label4.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Codigo"].ToString().Trim();
            TextBox1.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Descripcion"].ToString().Trim();
            TextBox3.Text = InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim();
        }
        
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBoxOkNo.Show();
                lbldummyOkNo.Text = "Anular riesgo";
            }            
        }
        
        private void anularRiesgo()
        {
            cRiesgo.anularRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()));
            string Responsable = cRiesgo.mtdResponsableRiesgo(Convert.ToInt32(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim()));
            string UsuarioAnolacion = Session["Usuario"].ToString();
            int IdResponsable = cRiesgo.mtdIdResponsableRiesgo(Convert.ToInt32(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim()));

            mtdGenerarNotificacion(Responsable, IdResponsable);
        }
        private void mtdGenerarNotificacion(string Responsable, int IdResponsable)
        {
            try
            {
                string TextoAdicional = string.Empty;

                TextoAdicional = "ANULACIÓN  DE RIESGO" + "<br>";
                TextoAdicional = TextoAdicional + "<br>";
                TextoAdicional = TextoAdicional + " Código : " + InfoGridRiesgos.Rows[RowGridRiesgos]["Codigo"].ToString().Trim() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre : " + InfoGridRiesgos.Rows[RowGridRiesgos]["Nombre"].ToString().Trim() + "<br>";
                TextoAdicional = TextoAdicional + " Justificación : "+ Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim())+".<br>";
                TextoAdicional = TextoAdicional + " Riesgo Global : " + InfoGridRiesgos.Rows[RowGridRiesgos]["NombreClasificacionRiesgo"].ToString().Trim() + ".<br>";
                TextoAdicional = TextoAdicional + " Responsable Riesgo : " + Responsable + ".<br>";
                TextoAdicional = TextoAdicional + " Fecha Anulación : " + System.DateTime.Now.ToString() + "<br>"; 
                 TextoAdicional = TextoAdicional + " Usuario Anulación : " + Session["loginUsuario"].ToString() + "<br>";
                TextoAdicional = TextoAdicional + " Nombre Usuario Anulación : " + Session["nombreUsuario"].ToString() + "<br>";

                boolEnviarNotificacion(19, Convert.ToInt32(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim()), IdResponsable, System.DateTime.Now.ToString().Trim(), TextoAdicional);
                boolEnviarNotificacion(19, Convert.ToInt32(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim()), Convert.ToInt32(Session["IdJerarquia"].ToString()), System.DateTime.Now.ToString().Trim(), TextoAdicional);
            }
            catch (Exception ex)
            {
                Mensaje("Error al generar la notificacion. " + ex.Message);
            }
        }
        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
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

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

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
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.Insert();
                #endregion
            }
            catch (Exception except)
            {
                // Handle the Exception.
                Mensaje("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim());
                err = true;
            }

            if (!err)
            {
                #region Restro
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource201.Insert();
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
                    //throw exception here you can write code to handle exception here
                    Mensaje("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim());
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                }
            }

            return (err);
        }
        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridRiesgoAnulado, Response, "Riesgos Anulados");
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
            dg.DataSource = dt;
            dg.DataBind();
            dg.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            switch (lbldummyOkNo.Text.Trim())
            {
                case "Anular riesgo":
                    try
                    {
                        anularRiesgo();
                        resetValues();
                        resetValuesConsulta();
                        loadGridRiesgoAnulado();
                        loadInfoRiesgoAnulado();
                        loadGridRiesgos();
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error al modificar riesgos. " + ex.Message);
                    }
                    break;
            }
        }
    }
}