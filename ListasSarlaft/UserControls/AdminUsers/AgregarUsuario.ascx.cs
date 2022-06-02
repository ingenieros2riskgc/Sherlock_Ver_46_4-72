using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;

namespace ListasSarlaft.UserControls
{
    public partial class AgregarUsuario : System.Web.UI.UserControl
    {

        cCuenta cCuenta = new cCuenta();
        DataTable CantidadUsuarios = new DataTable();
        String IdFormulario = "1002";
        int CantidadUsuariosFull;
        int CantidadUsuariosEventos;


        private void CargarNoUsuarios()
        {
            int CantDigitosFull = System.Configuration.ConfigurationManager.AppSettings["System.Version.NuA"].Length - 18;
            CantidadUsuariosFull = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["System.Version.NuA"].Substring(18, CantDigitosFull));

            int CantDigitosEventos = System.Configuration.ConfigurationManager.AppSettings["System.Version.NuE"].Length - 18;
            CantidadUsuariosEventos = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["System.Version.NuE"].Substring(18, CantDigitosEventos));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadDDLRol();
                PopulateTreeView();
            }
        }

        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView1.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional]";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                TreeView1.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox34.Text = TreeView1.SelectedNode.Text;
            lblIdDependencia1.Text = TreeView1.SelectedNode.Value;
            String nombre = cCuenta.NombreDetalleJerarquia(TreeView1.SelectedNode.Value.Trim());
            string[] strSeparator = new string[] { " " };
            string[] arrName = nombre.Split(strSeparator, StringSplitOptions.None);
            int i = arrName.Length;
            switch (i)
            {
                case 1:
                    TextBox2.Text = arrName[0].Trim();
                    break;
                case 2:
                    TextBox2.Text = arrName[0].Trim();
                    TextBox3.Text = arrName[1].Trim();
                    break;
                case 3:
                    TextBox2.Text = arrName[0].Trim();
                    TextBox3.Text = arrName[1].Trim() + " " + arrName[2].Trim();
                    break;
                case 4:
                    TextBox2.Text = arrName[0].Trim() + " " + arrName[1].Trim();
                    TextBox3.Text = arrName[2].Trim() + " " + arrName[3].Trim();
                    break;
                default:
                    TextBox2.Text = arrName[0].Trim() + " " + arrName[1].Trim();
                    TextBox3.Text = arrName[2].Trim() + " " + arrName[3].Trim();
                    break;
            }
        }

        private void loadDDLRol()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cCuenta.Roles();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreRol"].ToString(), dtInfo.Rows[i]["IdRol"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Erro al cargar los tipos de roles. " + ex.Message);
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                CargarNoUsuarios();
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar cantidad de usuarios disponibles: " + ex.Message);
            }
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                if (DropDownList1.SelectedItem.Value == "99")
                {
                    CantidadUsuarios = cCuenta.ContarUsuariosEvento("99");
                    if (CantidadUsuariosEventos <= Convert.ToInt16(CantidadUsuarios.Rows[0]["Total"].ToString()))
                        Mensaje1("Ha llegado al límite de usuarios permitidos para este Rol");
                    else
                        validateUser();

                }
                else
                {
                    CantidadUsuarios = cCuenta.ContarUsuarios("99");
                    if (CantidadUsuariosFull <= Convert.ToInt16(CantidadUsuarios.Rows[0]["Total"].ToString()))
                        Mensaje1("Ha llegado al límite de usuarios permitidos");
                    else
                        validateUser();
                }
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            resetValues();
        }

        private void validateUser()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cCuenta.validateUser(TextBox4.Text.Trim());
            }
            catch (Exception ex)
            {
                Mensaje1("Error valiando el usuario: " + ex);
            }

            if (dtInfo.Rows.Count == 0)
                registerUser();
            else
                Mensaje1("El nombre de usuario ya se encuentra registrado.");
        }

        private void registerUser()
        {
            string strErrMsg = string.Empty, strURL = string.Empty;
            bool booResult = false;
            try
            {
                booResult = cCuenta.mtdRegistrarUsuario(DropDownList1.SelectedItem.Value.ToString(), TextBox1.Text.Trim(),
                TextBox2.Text.Trim(), TextBox3.Text.Trim(), TextBox4.Text.Trim(),
                "Sherlock+", DropDownList2.SelectedItem.Value.ToString(), lblIdDependencia1.Text.Trim(), //Vieja contraseña sherlock2012
                ddlMacroProceso.SelectedItem.Value.ToString(), ddlProceso.SelectedItem.Value.ToString(), CheckBox1.Checked, ref strErrMsg);
            }
            catch (Exception ex)
            {
                Mensaje1("Error en el registro del usuario: " + ex);
            }

            if (booResult == true)
            {
                strURL = System.Configuration.ConfigurationManager.AppSettings["URL"].ToString().Trim();
                try
                {
                    mtdEnviarCorreoElectronico(25, Session["IdUsuario"].ToString().Trim(), TextBox4.Text.Trim(), "Sherlock+", lblIdDependencia1.Text.Trim(), strURL);
                    Mensaje("Se ha registrado el usuario con éxito.");
                    resetValues();
                }
                catch (Exception ex)
                {
                    Mensaje1("Error en el envio del correo electronico: " + ex);
                }

            }
            else
                Mensaje(strErrMsg);
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void Mensaje1(String Mensaje1)
        {
            lblMsgBox1.Text = Mensaje1;
            mpeMsgBox1.Show();
        }

        private void resetValues()
        {
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox34.Text = string.Empty;
            lblIdDependencia1.Text = string.Empty;
            ddlMacroProceso.SelectedIndex = 0;
            ddlProceso.SelectedIndex = 0;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

        }

        #region DropDownlists for MacroProceso and Proceso
        protected void ddlMacroProceso_DataBound(object sender, EventArgs e)
        {
            ddlMacroProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProceso_DataBound(object sender, EventArgs e)
        {
            ddlProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlMacroProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProceso.Items.Clear();
            ddlProceso.DataBind();
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                ddlProceso.Enabled = false;
                ddlMacroProceso.Enabled = false;
            }
            else
            {
                ddlProceso.Enabled = true;
                ddlMacroProceso.Enabled = true;
            }
        }
        #endregion DropDownlists for MacroProceso and Proceso

        private void mtdEnviarCorreoElectronico(int intIdEvento, string strIdUsuarioActual,
            string strNewUser, string strPass, string strIdJerarquiaNuevoUsuario, string strURL)
        {
            #region Vars
            string strCopia = string.Empty, strAsunto = string.Empty, strOtros = string.Empty, strCuerpo = string.Empty,
                strDestinatario = string.Empty;

            DataTable dtInfo = new DataTable();
            #endregion

            #region Informacion basica
            dtInfo = cCuenta.mtdConsultarCorreo(intIdEvento);

            if (dtInfo != null)
            {
                strCopia = dtInfo.Rows[0]["Copia"].ToString();
                strOtros = dtInfo.Rows[0]["Otros"].ToString();
                strAsunto = dtInfo.Rows[0]["Asunto"].ToString();
                strCuerpo = string.Format("Se le informa que le ha sido creado un usuario de acceso a la aplicación Sherlock, " +
                    "para lo cual a continuación le informamos los datos de ingreso: <BR><BR> " +
                    "Usuario creado: {0} <BR>Contraseña: {1} <BR><BR>Dirección o URL de acceso: {2} <BR><BR>" +
                    "Este correo es enviado autómaticamente por Sherlock.<div><BR></div>", strNewUser, strPass, strURL);
            }
            #endregion

            #region Correo del Destinatario

            dtInfo = new DataTable();
            dtInfo = cCuenta.mtdConsultarIdJerarquia(strIdUsuarioActual);

            if (dtInfo != null)
            {
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(dtInfo.Rows[0]["idHijo"].ToString().Trim()))
                {
                    string strNodoJerarquia = dtInfo.Rows[0]["idHijo"].ToString().Trim();

                    dtInfo = new DataTable();
                    dtInfo = cCuenta.mtdConsultarCorreoDestinatario(strNodoJerarquia);

                    if (dtInfo != null)
                        if (!string.IsNullOrEmpty(dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim()))
                            strDestinatario = dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                }
            }
            #endregion

            #region Correo del Nuevo Usuario
            //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
            if (!string.IsNullOrEmpty(strIdJerarquiaNuevoUsuario))
            {
                dtInfo = new DataTable();
                dtInfo = cCuenta.mtdConsultarCorreoDestinatario(strIdJerarquiaNuevoUsuario);

                if (dtInfo != null)
                    if (!string.IsNullOrEmpty(dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim()))
                    {
                        if (!string.IsNullOrEmpty(strDestinatario))
                            strDestinatario = strDestinatario + ";" + dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                        else
                            strDestinatario = dtInfo.Rows[0]["CorreoResponsable"].ToString().Trim();
                    }
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
                foreach (string substr in strDestinatario.Split(';'))
                {
                    if (!string.IsNullOrEmpty(substr.Trim()))
                        message.To.Add(substr);
                }
                #endregion

                #region Copia
                if (!string.IsNullOrEmpty(strCopia.Trim()))
                    foreach (string substr in strCopia.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.CC.Add(substr);
                    }
                #endregion

                #region Otros
                if (!string.IsNullOrEmpty(strOtros.Trim()))
                    foreach (string substr in strOtros.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.CC.Add(substr);
                    }
                #endregion

                message.Subject = strAsunto;//subject of email
                message.IsBodyHtml = true;//To determine email body is html or not
                message.Body = strCuerpo;

                smtpClient.Send(message);
                #endregion
            }
            catch (Exception ex)
            {
                //throw exception here you can write code to handle exception here
                //omb.ShowMessage("Error en el envio de la notificacion." + "<br/>" + "Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }


    }
}