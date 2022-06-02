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
using System.Net.Configuration;
using System.Configuration;
using System.IO;
using AjaxControlToolkit;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls
{
    public partial class ROIManual : System.Web.UI.UserControl
    {
        String IdFormulario = "6006";
        private static int LastInsertIdCE;
        string IdRegistro = string.Empty;
        cCuenta cCuenta = new cCuenta();
        private cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();

        #region Properties
        private DataTable infoGridArchivoRegistroOperacion;
        private DataTable InfoGridArchivoRegistroOperacion
        {
            get
            {
                infoGridArchivoRegistroOperacion = (DataTable)ViewState["infoGridArchivoRegistroOperacion"];
                return infoGridArchivoRegistroOperacion;
            }
            set
            {
                infoGridArchivoRegistroOperacion = value;
                ViewState["infoGridArchivoRegistroOperacion"] = infoGridArchivoRegistroOperacion;
            }
        }

        private int rowGridArchivoRegistroOperacion;
        private int RowGridArchivoRegistroOperacion
        {
            get
            {
                rowGridArchivoRegistroOperacion = (int)ViewState["rowGridArchivoRegistroOperacion"];
                return rowGridArchivoRegistroOperacion;
            }
            set
            {
                rowGridArchivoRegistroOperacion = value;
                ViewState["rowGridArchivoRegistroOperacion"] = rowGridArchivoRegistroOperacion;
            }
        }

        private int rowGridRegistroOperacion;
        private int RowGridRegistroOperacion
        {
            get
            {
                rowGridRegistroOperacion = (int)ViewState["rowGridRegistroOperacion"];
                return rowGridRegistroOperacion;
            }
            set
            {
                rowGridRegistroOperacion = value;
                ViewState["rowGridRegistroOperacion"] = rowGridRegistroOperacion;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack && !Page.IsCallback && !AjaxFileUpload11.IsInFileUploadPostBack)
            {
                loadDDLTipoIden();
                LoadAreaUsuario();
                TxbFechaHoy.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        #region Loads
        private void LoadAreaUsuario()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = cRegistroOperacion.LoadAreaUsuario();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Codigo"].ToString() != "")
                    {
                        Label6.Text = dt.Rows[0]["NombreArea"].ToString().Trim();
                        Label7.Text = dt.Rows[0]["Codigo"].ToString().Trim();
                    }
                    else
                        Mensaje("Faltan parámetros de Área Usuario");
                }
                else
                    Mensaje("No hay informacion de Área Usuario");
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar área de usuario. " + ex.Message);
            }
        }

        private void loadDDLTipoIden()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRegistroOperacion.loadDDLTipoIden();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los tipos de registro. " + ex.Message);
            }
        }

        private void loadFile()
        {
            //    DataTable dtInfo = new DataTable();
            //    string nameFile;
            //    dtInfo = cRegistroOperacion.loadCodigoArchivoRegistroOperacion();
            //    if (dtInfo.Rows.Count > 0)
            //    {
            //        nameFile = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() + "-" + txtIdProceso.Text.ToString().Trim() + "-" + FileUpload.FileName.ToString().Trim();
            //    }
            //    else
            //    {
            //        nameFile = "1-" + txtIdProceso.Text.ToString().Trim() + "-" + FileUpload.FileName.ToString().Trim();
            //    }
            //    FileUpload.SaveAs(Server.MapPath("~/Archivos/PDFsRegistroOperacion/") + nameFile);
            //    cRegistroOperacion.agregarArchivo(txtIdProceso.Text.ToString().Trim(), nameFile);
        }

        private void mtdLoadGridArchivoRO()
        {
            loadGridArchivoRegistroOperacion();
            loadInfoArchivoRegistroOperacion();
        }

        private void loadGridArchivoRegistroOperacion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoRegistroOperacion = grid;
            GridView1.DataSource = InfoGridArchivoRegistroOperacion;
            GridView1.DataBind();
        }

        private void loadInfoArchivoRegistroOperacion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRegistroOperacion.loadInfoIdRegistro_Indicador(Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()));

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoRegistroOperacion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                        dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                    });
                }

                GridView1.DataSource = InfoGridArchivoRegistroOperacion;
                GridView1.DataBind();
            }
        }
        #endregion

        #region Buttons
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    DataTable dtMaxId = new DataTable();
                    ///Se genera el último consecutivo
                    DataTable dtc = new DataTable();
                    dtc = cRegistroOperacion.LoadIndicador(Label7.Text.Trim());
                    string ConsecutioTotal = cRegistroOperacion.LoadIndicadorTotal();

                    if (dtc.Rows[0]["Registros"].ToString() == "0")
                        TextBox5.Text = Sanitizer.GetSafeHtmlFragment(Label7.Text.Trim()) + "_1";
                    else
                        TextBox5.Text = Sanitizer.GetSafeHtmlFragment(Label7.Text.Trim()) + "_" + dtc.Rows[0]["Registros"].ToString().Trim();

                    cRegistroOperacion.agregarRegistroOperacion("3", Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()) + " 12:00:00:000", "Ingreso de ROI manual.",
                        "Ingreso de ROI manual.", DropDownList1.SelectedValue.ToString().Trim(), Label4.Text.Trim(), Label7.Text);

                    dtMaxId = cRegistroOperacion.loadMaxIdRegistroOperacion();

                    if (dtMaxId.Rows.Count > 0)
                    {
                        Mensaje("La información se insertó con éxito en la Base de Datos.\n\n Código de Operación: " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text));
                        txtIdProceso.Text = dtMaxId.Rows[0]["MaxId"].ToString().Trim();
                        IdRegistro = txtIdProceso.Text;

                        if (!string.IsNullOrEmpty(Label6.Text.Trim()) && !string.IsNullOrEmpty(TextBox5.Text.ToString().Trim()) && !string.IsNullOrEmpty(ConsecutioTotal))
                        {
                            boolEnviarNotificacion(16, Convert.ToInt16(Session["idJerarquia"].ToString().Trim()), Convert.ToInt32(txtIdProceso.Text.ToString().Trim()), TextBox1.Text.Trim()
                            + " 12:00:00:000", "Tipo de registro: Ingreso de ROI manual <br />Estado: Creado <br />Identificación: " + TextBox7.Text.ToString().Trim() + "<br />Nombre y Apellido: "
                            + TextBox6.Text.ToString().Trim() + "<br />Indicador: " + Label6.Text.Trim() + " - " + TextBox5.Text.ToString().Trim() + " - " + ConsecutioTotal + "<br />Descripción: " + TextBox3.Text.ToString().Trim()
                            + "<br />Mensaje: " + TextBox4.Text.ToString().Trim() + "<br />Fecha detección: " + TextBox1.Text.Trim() + "<br />");
                            ImageButton1.Visible = false;
                        }
                        else
                        {
                            Mensaje("Error al enviar notificación. No existe información de Indicador");
                        }

                    }
                    else
                    {
                        resetValues();
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar la información. " + ex.Message);
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
            LoadAreaUsuario();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != string.Empty)
                mtdLoadGridArchivoRO();
        }
        #endregion

        #region Gridview
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoRegistroOperacion = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    //descargarArchivo();
                    mtdDescargarPdfRegOperacion();
                    break;
            }
        }
        #endregion

        #region DDL
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.ToString() == "NIT")
            {
                Label3.Visible = true;
                Label4.Visible = true;
                Label19.Text = "Razón Social:";
            }
            else
            {
                Label3.Visible = false;
                Label4.Visible = false;
                Label4.Text = string.Empty;
                Label19.Text = "Nombre y apellido:";
            }
            TextBox7.Focus();
        }
        #endregion

        #region TextBox
        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.ToString() == "NIT")
            {
                Label4.Text = CalcularDigitoVerificacion(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()));
            }
        }
        #endregion

        #region Ajax Event
        protected void AjaxFileUploadEvent(object sender, AjaxFileUploadEventArgs e)
        {
            try
            {
                string filename = System.IO.Path.GetFileName(e.FileName);
                byte[] pdf = e.GetContents();
                DataTable dtMaxId = new DataTable();
                dtMaxId = cRegistroOperacion.loadMaxIdRegistroOperacion();
                IdRegistro = dtMaxId.Rows[0]["MaxId"].ToString().Trim();
                mtdCargarPdfRegOperacionMultiples(filename, pdf, IdRegistro);
                mtdLoadGridArchivoRO();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message);
            }
        }
        #endregion

        #region Datasource
        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }
        #endregion

        #region

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            txtIdProceso.Text = string.Empty;
            IdRegistro = string.Empty;
            GridView1.DataSource = null;
            GridView1.DataBind();
            ImageButton1.Visible = true;
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idNodoJerarquia, int idRegistro, string FechaFinal, string textoAdicional)
        {
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string nomResponsable = "";

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            try
            {
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                selectCommand = "SELECT DJ.NombreResponsable,DJ.CorreoResponsable, JO.idPadre FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO, " +
                    "[Parametrizacion].[DetalleJerarquiaOrg] AS DJ WHERE JO.idHijo = " + idNodoJerarquia + " AND DJ.idHijo = JO.idHijo";

                SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
                DataTable dtblDiscuss = new DataTable();
                dad.Fill(dtblDiscuss);

                DataView view = new DataView(dtblDiscuss);

                foreach (DataRowView row in view)
                {
                    nomResponsable = row["NombreResponsable"].ToString().Trim();
                    Destinatario = row["CorreoResponsable"].ToString().Trim();
                    idJefeInmediato = row["idPadre"].ToString().Trim();
                }

                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre " +
                    "FROM [Notificaciones].[CorreosDestinatarios] AS CD, [Notificaciones].[Evento] AS E WHERE E. IdEvento = " + idEvento + " AND CD.IdEvento = E.IdEvento";
                dad = new SqlDataAdapter(selectCommand, conString);
                dtblDiscuss.Clear();
                dad.Fill(dtblDiscuss);
                view = new DataView(dtblDiscuss);

                foreach (DataRowView row in view)
                {
                    Copia = row["Copia"].ToString().Trim();
                    Otros = row["Otros"].ToString().Trim();
                    Asunto = row["Asunto"].ToString().Trim();
                    Cuerpo = textoAdicional + "Creado por: " + nomResponsable + "<br><br>" + row["Cuerpo"].ToString() + "</br>";
                    NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                    AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                    AJefeMediato = row["AJefeMediato"].ToString().Trim();
                    RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                }

                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO, " +
                        "[Parametrizacion].[DetalleJerarquiaOrg] AS DJ WHERE JO.idHijo = " + idJefeInmediato + " AND DJ.idHijo = JO.idHijo";
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

                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO, " +
                        "[Parametrizacion].[DetalleJerarquiaOrg] AS DJ WHERE JO.idHijo = " + idJefeMediato + " AND DJ.idHijo = JO.idHijo";
                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                    }
                }

                //Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.Insert();
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios 
                // y a enviar el correo 
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

                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");
                    message.From = fromAddress;//here you can set address

                    foreach (string substr in Destinatario.Split(';'))
                    {
                        message.To.Add(substr);
                    }

                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            message.CC.Add(substr);
                        }

                    if (Otros.Trim() != "")
                        foreach (string substr in Otros.Split(';'))
                        {
                            message.CC.Add(substr);
                        }

                    message.Subject = Asunto;//subject of email
                    message.IsBodyHtml = true;//To determine email body is html or not
                    message.Body = Cuerpo;

                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    omb.ShowMessage("Error en el envio de la notificacion." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
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

        public string CalcularDigitoVerificacion(string Nit)
        {
            string Temp;
            int Contador, Residuo = 0, Acumulador = 0;
            int[] Vector = new int[15];

            Vector[0] = 3;
            Vector[1] = 7;
            Vector[2] = 13;
            Vector[3] = 17;
            Vector[4] = 19;
            Vector[5] = 23;
            Vector[6] = 29;
            Vector[7] = 37;
            Vector[8] = 41;
            Vector[9] = 43;
            Vector[10] = 47;
            Vector[11] = 53;
            Vector[12] = 59;
            Vector[13] = 67;
            Vector[14] = 71;

            for (Contador = 0; Contador < Nit.Length; Contador++)
            {
                Temp = Nit[(Nit.Length - 1) - Contador].ToString().Trim();
                Acumulador = Acumulador + (Convert.ToInt32(Temp) * Vector[Contador]);
            }

            Residuo = Acumulador % 11;

            if (Residuo > 1)
                return Convert.ToString(11 - Residuo);

            TextBox6.Focus();
            return Residuo.ToString().Trim();
        }

        #region PDFs
        private void descargarArchivo()
        {
            try
            {
                Response.Clear();
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=file.pdf");
                Response.TransmitFile(Server.MapPath("~/Archivos/PDFsRegistroOperacion/" + InfoGridArchivoRegistroOperacion.Rows[RowGridArchivoRegistroOperacion]["UrlArchivo"].ToString().Trim()));
                Response.End();
            }
            catch (Exception ex)
            {
                Mensaje("Error al descargar el archivo. " + ex.Message);
            }
        }

        private void mtdDescargarPdfRegOperacion()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoRegistroOperacion.Rows[RowGridArchivoRegistroOperacion]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cRegistroOperacion.mtdDescargarArchivoPdf(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }

        protected void btnAgregarPDF_Click(object sender, ImageClickEventArgs e)
        {
        }

        private void mtdCargarPdfRegOperacion()
        {
            //#region Vars
            //DataTable dtInfo = new DataTable();
            //string strNombreArchivo = string.Empty;
            //#endregion Vars

            //dtInfo = cRegistroOperacion.loadCodigoArchivoRegistroOperacion();

            //#region Nombre Archivo
            //if (dtInfo.Rows.Count > 0)
            //    strNombreArchivo = string.Format("{0}-{1}-{2}",
            //        dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
            //        txtIdProceso.Text.ToString().Trim(),
            //        FileUpload.FileName.ToString().Trim());
            //else
            //    strNombreArchivo = string.Format("1-{0}-{1}",
            //        txtIdProceso.Text.ToString().Trim(),
            //        FileUpload.FileName.ToString().Trim());
            //#endregion Nombre Archivo

            //#region Archivo
            //Stream fs = FileUpload.PostedFile.InputStream;
            //BinaryReader br = new BinaryReader(fs);
            //Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            //#endregion Archivo

            //cRegistroOperacion.mtdAgregarArchivoPdf(txtIdProceso.Text.ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdCargarPdfRegOperacionMultiples(string NomArchivo, byte[] bPdfData, string IdRegistro)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cRegistroOperacion.loadCodigoArchivoRegistroOperacion();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(txtIdProceso.Text.ToString().Trim()),
                    NomArchivo.ToString());
            else
                strNombreArchivo = string.Format("1-{0}-{1}",
                    Sanitizer.GetSafeHtmlFragment(txtIdProceso.Text.ToString().Trim()),
                    NomArchivo.ToString());
            #endregion Nombre Archivo

            cRegistroOperacion.mtdAgregarArchivoPdf(IdRegistro, strNombreArchivo, bPdfData);
        }
        #endregion

        #endregion
    }
}