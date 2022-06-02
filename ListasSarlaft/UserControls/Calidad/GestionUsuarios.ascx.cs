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

namespace ListasSarlaft.UserControls.Calidad
{
    public partial class GestionUsuarios : System.Web.UI.UserControl
    {
        string IdFormulario = "10001";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();
        private cGestion cGestion = new cGestion();
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        cQA cQa = new cQA();

        private int pagIndex;
        private DataTable infoGrid;
        private DataTable infoGridGrupo;
        private int rowGrid;

        #region Page_load

        protected void Page_Load( object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.gvGrupoTrabajo);
                scriptManager.RegisterPostBackControl(this.gvIntegrantes);
                divDatoGrupo.Visible = false;
                btnRegGrupo.Visible = false;
                btnNoModGrupo.Visible = false;
                mtdInicializarValoresGrupoTrabajo();
                mtdLoadGridViewGrupoTrabajo();
                divDatosUsuarios.Visible = false;
                btnRegIntegrante.Visible = false;
                btnNoRegIntegrante.Visible = false;
                
                imgBtnInsertarIntegrante.Visible = false;
                imgBtnCancelConsul.Visible = false;
                divUsuarios.Visible = false;
                btnUpdGrupo.Visible = false;
                btnUpdIntegrante.Visible = false;
            }
        }

        #endregion

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

        private DataTable infoGridUser;

        private DataTable InfoGridUser
        {
            get
            {
                infoGridUser = (DataTable)ViewState["infGrid2"];
                return infoGridUser;
            }
            set
            {
                infoGridUser = value;
                ViewState["infGrid2"] = infoGridUser;
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

        #region grid gvGrupoTrabajo
        ///
        ////// Grid gvGrupoTrabajo
        ///

        protected void gvGrupoTrabajo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvGrupoTrabajo.PageIndex = PagIndexInfoGridGrupo;
            gvGrupoTrabajo.DataSource = infoGridGrupo;
            gvGrupoTrabajo.DataBind();

            mtdLoadGridViewGrupoTrabajo();
        }
        private void mtdLoadGridViewGrupoTrabajo()
        {
            mtdLoadGridGrupoTrabajo();
            mtdLoadInfoGridGrupoTrabajo();
        }

        private void mtdLoadGridGrupoTrabajo()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("idGrupoSoporte", typeof(string));
            grid.Columns.Add("NombreGrupoSoporte", typeof(string));

            InfoGridGrupo = grid;
            gvGrupoTrabajo.DataSource = InfoGridGrupo;
            gvGrupoTrabajo.DataBind();
        }

        private void mtdLoadInfoGridGrupoTrabajo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cCuenta.RolesVer();
            string strErrMsg = string.Empty;
            clsGestionUsuarios cGestionUsuarios = new clsGestionUsuarios();
            List<clsDTOGestionGrupos> lstGrupoUsuario = new List<clsDTOGestionGrupos>();

            lstGrupoUsuario = cGestionUsuarios.mtdConsultaGrupoTrabajo(ref strErrMsg);
            
            if (lstGrupoUsuario != null)
            {
                mtdLoadGridGrupoTrabajo();
                mtdLoadInfoGridGrupoTrabajo(lstGrupoUsuario);
                gvGrupoTrabajo.DataSource = lstGrupoUsuario;
                //gvGrupoTrabajo.DataBind();
                
            }
        }

        private void mtdLoadInfoGridGrupoTrabajo(List<clsDTOGestionGrupos> lstGrupoUsuario)
        {
            DataTable dtInfo = new DataTable();

            string strId = string.Empty;
            
            dtInfo = cQa.ConsultaGrupos();    //llena la tabla
            if (dtInfo.Rows.Count > 0)
            {

                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridGrupo.Rows.Add(new Object[]
                        {
                        dtInfo.Rows[rows]["idGrupoSoporte"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreGrupoSoporte"].ToString().Trim()
                }
                        );
                }
                gvGrupoTrabajo.DataSource = InfoGridGrupo;
                gvGrupoTrabajo.DataBind();
            }
        }

        private void mtdInicializarValoresGrupoTrabajo()
        {
            PagIndexInfoGridGrupo = 0;
        }

        private void mtdMensajeGrupoTrabajo(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private DataTable InfoGridGrupo
        {
            get
            {
                infoGridGrupo = (DataTable)ViewState["infGrid2"];
                return infoGridGrupo;
            }
            set
            {
                infoGridGrupo = value;
                ViewState["infGrid2"] = infoGridGrupo;
            }
        }

        private int pagIndexInfoGridGrupo;
        private int PagIndexInfoGridGrupo
        {
            get
            {
                pagIndexInfoGridGrupo = (int)ViewState["pagIndexInfoGridGrupo"];
                return pagIndexInfoGridGrupo;
            }
            set
            {
                pagIndexInfoGridGrupo = value;
                ViewState["pagIndexInfoGridGrupo"] = pagIndexInfoGridGrupo;
            }
        }

        protected void gvGrupoTrabajo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;


            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    BtnModificar_Click(ref strErrMsg);
                    break;
                case "Integrantes":

                    mtdInicializarValoresIntegrantes();
                    mtdLoadGridViewIntegrantes();
                    BtnIntegrantes_Click(ref strErrMsg);
                    break;
            }
        }

        private void BtnModificar_Click(ref string strErrMsg)
        {
            divDatoGrupo.Visible = true;
            if (StrNombreGrupoTrabajo.Visible == true)
                //StrNombreGrupoTrabajo.Text = InfoGridGrupo.Rows[RowGrid]["StrNombreGrupoTrabajo"].ToString().Trim();
            btnRegGrupo.Visible = false;
            btnNoModGrupo.Visible = true;
            imgBtnInsertar.Visible = false;
            divUsuarios.Visible = false;
            imgBtnInsertarIntegrante.Visible = false;
            imgBtnCancelConsul.Visible = false;
            btnUpdGrupo.Visible = true;
        }

        private void BtnIntegrantes_Click(ref string strErrMsg)
        {
            imgBtnInsertarIntegrante.Visible = true;
            imgBtnCancelConsul.Visible = true;
            imgBtnCancelConsul.Visible = true;
            divUsuarios.Visible = true;
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            divDatoGrupo.Visible = true;
            btnRegGrupo.Visible = true;
            btnNoModGrupo.Visible = true;
            imgBtnInsertar.Visible = false;
        }
        #region registrar grupo de trabajo
        protected void imgbtnRegGrupo_Click(object sender, ImageClickEventArgs e)
        {

            string strErrMsg = string.Empty;

            try
            {
                try 
                {
                    strErrMsg = "Grupo registrado exitosamente.";
                    gvGrupoTrabajo.Visible = true;

                    mtdInsertarGrupo(
                        string.Empty,
                        Sanitizer.GetSafeHtmlFragment(StrNombreGrupoTrabajo.Text.Trim()),
                        ref strErrMsg
                        );

                    // Envia la notificación.
                    //EnviarNotificaciones(tbxResponsable.Text.Trim(), txtCodigo.Text.ToUpper().Trim());     Pendiente
                    mtdResetCampos();
                    gvGrupoTrabajo.Visible = true;
                    mtdMensaje("Grupo creado exitosamente");
                }
                catch (Exception except)
                {
                    strErrMsg = "Atención." + except.Message.ToString();
                }
                mtdLoadGridViewGrupoTrabajo();
            }
            catch (Exception except)
            {
                strErrMsg = "Error al registrar el grupo." + except.Message.ToString();

            }
        }

        private void mtdInsertarGrupo(
            string strIdGrupoTrabajo, 
            string strNombreGrupoTrabajo, ref string strErrMsg)
        {
            //bool booResult = false;

            clsGestionUsuarios cGrupUsu = new clsGestionUsuarios();
            clsDTOGestionGrupos objGrupUsu = new clsDTOGestionGrupos(
                strIdGrupoTrabajo, 
                strNombreGrupoTrabajo);

            cGrupUsu.mtdInsertarGrupoUsuario(objGrupUsu, ref strErrMsg);

        }
        #endregion

        protected void imgbtnUpdGrupo_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            mtdInicializarValoresGrupoTrabajo();
            mtdLoadGridViewGrupoTrabajo();

            bool booResult = false;
            booResult = mtdActualizarGrupo(
                InfoGridGrupo.Rows[RowGrid]["idGrupoSoporte"].ToString().Trim(),
                //InfoGridGrupo.Rows[RowGrid]["NombreGrupoSoporte"].ToString().Trim(),
                Sanitizer.GetSafeHtmlFragment(StrNombreGrupoTrabajo.Text.Trim()),
                ref strErrMsg, booResult
                );
            if (booResult == true)
            {
                mtdResetCampos();
                mtdMensajeGrupoTrabajo("Grupo actualizado con éxito.");
            }
            else
            {
                mtdMensajeGrupoTrabajo("Error al actualizar el grupo.");
            }
            mtdLoadGridGrupoTrabajo();
            mtdLoadInfoGridGrupoTrabajo();
        }

        private bool mtdActualizarGrupo(
            string strIdGrupoTrabajo, 
            string strNombreGrupoTrabajo, ref string strErrMsg, Boolean booResult)
        {
            clsGestionUsuarios cGrupUsu = new clsGestionUsuarios();
            clsDTOGestionGrupos objUpdReq = new clsDTOGestionGrupos(
                strIdGrupoTrabajo, 
                strNombreGrupoTrabajo);

            booResult = cGrupUsu.mtdActualizarGrupoTrabajo(objUpdReq, ref strErrMsg , booResult);
            return booResult;
        }

        protected void imgbtnNoModGrupo_Click(object sender, ImageClickEventArgs e)
        {
            divDatoGrupo.Visible = false;
            btnRegGrupo.Visible = false;
            btnNoModGrupo.Visible = false;
            btnUpdGrupo.Visible = false;
            imgBtnInsertar.Visible = true;
        }

        private void mtdResetCampos()
        {

            divDatoGrupo.Visible = false;
            btnRegGrupo.Visible = false;
            btnNoModGrupo.Visible = false;
            btnUpdGrupo.Visible = false;
            imgBtnInsertar.Visible = true;
            StrNombreGrupoTrabajo.Text = "";
            imgBtnInsertar.Visible = true;

        }
        
        protected void imgbtnCancelConsul_Click(object sender, ImageClickEventArgs e)
        {
            imgBtnInsertarIntegrante.Visible = false;
            imgBtnCancelConsul.Visible = false;
            imgBtnCancelConsul.Visible = false;
            divUsuarios.Visible = false;
            btnUpdGrupo.Visible = false;
            mtdInicializarValoresGrupoTrabajo();
            mtdLoadGridViewGrupoTrabajo();
        }

        ///
        ////// Grid gvGrupoTrabajos
        ///
        #endregion

        #region grid gvIntegrantes
        ///
        ////// Grid gvIntegrantes
        ///

        protected void gvIntegrantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            PagIndex = e.NewPageIndex;
            gvIntegrantes.PageIndex = PagIndex;
            gvIntegrantes.DataSource = InfoGridUser;
            gvIntegrantes.DataBind();

            mtdLoadGridViewIntegrantes();
        }
        private void mtdLoadGridViewIntegrantes()
        {

            mtdLoadGridIntegrantes();
            mtdLoadInfoGridIntegrantes();
        }

        private void mtdLoadGridIntegrantes()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdIntegrante", typeof(string));
            grid.Columns.Add("StrNombreIntegrante", typeof(string));
            grid.Columns.Add("StrUsuarioIntegrante", typeof(string));
            grid.Columns.Add("StrCorreoIntegrante", typeof(string));
            grid.Columns.Add("StrIdGrupoTrabajo", typeof(string));

            //gvIntegrantes.DataSource = grid;
            //gvIntegrantes.DataBind();
            //InfoGrid = grid;


            InfoGridUser = grid;
            gvIntegrantes.DataSource = InfoGridUser;
            gvIntegrantes.DataBind();
        }

        private string ConsultarUsuarios()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.ConsultaIntegrantes();
            string strId = string.Format(dtInfo.Rows[RowGrid]["idGrupoSoporte"].ToString().Trim());

            return strId;
        }

        private void mtdLoadInfoGridIntegrantes()
        {
            string strIds = string.Empty;
            string strErrMsg = string.Empty;
            //string strId = Convert.ToString(InfoGridGrupo.Rows[RowGrid]["StrIdGrupoTrabajo"].ToString().Trim());

            clsGestionUsuarios cGestionUsuario = new clsGestionUsuarios();
            List<clsDTOGestionUsuarios> lstIntegrantes = new List<clsDTOGestionUsuarios>();

            strIds = ConsultarUsuarios();

            lstIntegrantes = cGestionUsuario.mtdCargarIntegrantes(strIds,  ref strErrMsg);

            if (lstIntegrantes != null)
            {
                mtdLoadGridIntegrantes();
                mtdLoadInfoGridIntegrantes(lstIntegrantes);
                gvIntegrantes.DataSource = lstIntegrantes;
                gvIntegrantes.DataBind();
            }
        }

        private string ConsultarIdGrupo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.ConsultaIntegrantes();
            string strId = string.Format(dtInfo.Rows[RowGrid]["idGrupoSoporte"].ToString().Trim());

            return strId;
        }

        private void mtdLoadInfoGridIntegrantes(List<clsDTOGestionUsuarios> lstIntegrantes)
        {
            DataTable dtInfo = new DataTable();

            string strId = string.Empty;
            
            strId = ConsultarIdGrupo();  //id del grupo
            dtInfo = cQa.ConsultaUsers(strId);    //llena la tabla
            if(dtInfo.Rows.Count > 0)
            {

                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridUser.Rows.Add(new Object[]
                        {
                        dtInfo.Rows[rows]["IdUsuarioSoporte"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreUsuarioSoporte"].ToString().Trim(),
                        dtInfo.Rows[rows]["UsuarioSoporte"].ToString().Trim(),
                        dtInfo.Rows[rows]["CorreUsuarioSoporte"].ToString().Trim(),
                        dtInfo.Rows[rows]["idGrupoSoporte"].ToString().Trim()
                }
                        );
                }
                gvIntegrantes.DataSource = InfoGridUser;
                gvIntegrantes.DataBind();
            }
        }

        private void mtdInicializarValoresIntegrantes()
        {
            PagIndex = 0;
        }

        private void mtdMensajeIntegrantes(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void gvIntegrantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;
            
            //RowGrid = Convert.ToInt16(e.CommandArgument);
            RowGrid = (Convert.ToInt16(gvIntegrantes.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    BtnModificarUsuario_Click(ref strErrMsg);
                    break;
                case "Eliminar":
                    string strIdUserSoporte = InfoGridUser.Rows[RowGrid]["StrIdIntegrante"].ToString().Trim();
                    BtnDelete_Click( ref strErrMsg);
                    break;
            }
        }

        private void BtnModificarUsuario_Click(ref string strErrMsg)
        {
            divDatosUsuarios.Visible = true;
            btnUpdIntegrante.Visible = true;
            btnNoRegIntegrante.Visible = true;
        }

        private string ConsultarUs()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.ConsultaIntegrantes();
            string strId = string.Format(dtInfo.Rows[RowGrid]["idGrupoSoporte"].ToString().Trim());

            return strId;
        }

        protected void imgbtnUpdIntegrante_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string strIdIntegrante = string.Empty;
            strIdIntegrante = ConsultarUs();

            try
            {
                mtdActualizarIntegrante(
                    InfoGridUser.Rows[RowGrid]["StrIdIntegrante"].ToString().Trim(),
                    Sanitizer.GetSafeHtmlFragment(StrNombreIntegrante.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(StrUsuarioIntegrante.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(StrCorreoIntegrante.Text.Trim()),
                    Sanitizer.GetSafeHtmlFragment(strIdIntegrante),
                    //InfoGrid.Rows[RowGrid]["StrIdGrupoTrabajo"].ToString().Trim(),
                    ref strErrMsg
                    );

                mtdResetCamposs();
                mtdMensajeGrupoTrabajo("Usuario actualizado con éxito.");
            }
            catch(Exception except)
            {
                mtdMensajeGrupoTrabajo("Error al actualizar el usuario." + except.Message.ToString());
                
            }
            mtdLoadGridViewIntegrantes();
        }

        private void mtdActualizarIntegrante(string strIdIntegrante, string strNombreIntegrante, string strUsuarioIntegrante, string strCorreoIntegrante,
            string strIdGrupoTrabajo, ref string strErrMsg)
        {
            clsGestionUsuarios cGrupUsu = new clsGestionUsuarios();
            clsDTOGestionUsuarios objUpdUsu = new clsDTOGestionUsuarios(strIdIntegrante, strNombreIntegrante, strUsuarioIntegrante,
                strCorreoIntegrante, strIdGrupoTrabajo);

            cGrupUsu.mtdActualizarUsuario(objUpdUsu, ref strErrMsg);
        }

        private string ConsultaDelete()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cQa.ConsultaEliminar();
            string strId = string.Format(dtInfo.Rows[RowGrid]["IdUsuarioSoporte"].ToString().Trim());
            
            return strId;
        }

        private void BtnDelete_Click(ref string strErrMsg)
        {
            string strIdIntegrante = string.Empty;
            strIdIntegrante = ConsultaDelete();

            try
            {
                borrarUsuario(
                    InfoGridUser.Rows[RowGrid]["StrIdIntegrante"].ToString().Trim()
                    );
                mtdResetCamposs();
                mtdMensajeGrupoTrabajo("Usuario eliminado con éxito.");
            }
            catch (Exception ex)
            {
                mtdMensajeGrupoTrabajo("Error eliminando al usuario." + ex.Message);
            }
            mtdLoadGridViewIntegrantes();
        }

        public void borrarUsuario(String StrIdIntegrante)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM QA.parametrizacionUsuariosSoporte WHERE (IdUsuarioSoporte = " + StrIdIntegrante + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        protected void imgBtnInsertarIntegrante_Click(object sender, ImageClickEventArgs e)
        {
            divDatosUsuarios.Visible = true;
            btnRegIntegrante.Visible = true;
            btnNoRegIntegrante.Visible = true;
            imgBtnInsertarIntegrante.Visible = false;
        }

        protected void imgbtnRegIntegrante_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string strIdGrup = string.Empty;
            strIdGrup = ConsultarUsuarios();
            try
            {
                try
                {
                    strErrMsg = "Integrante registrado exitosamente.";
                    gvIntegrantes.Visible = true;
                    
                    mtdInsertarIntegrante(
                        string.Empty,
                        Sanitizer.GetSafeHtmlFragment(StrNombreIntegrante.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(StrUsuarioIntegrante.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(StrCorreoIntegrante.Text.Trim()),
                        Sanitizer.GetSafeHtmlFragment(strIdGrup),
                        ref strErrMsg
                        );

                    // Envia la notificación.
                    //EnviarNotificaciones(tbxResponsable.Text.Trim(), txtCodigo.Text.ToUpper().Trim());     Pendiente
                    mtdResetCamposs();
                    mtdMensajeGrupoTrabajo("Usuario creado con éxito.");
                }
                catch
                {
                    mtdMensajeGrupoTrabajo("Error al registrar al usuario.");
                }
                mtdLoadGridViewIntegrantes();
            }
            catch (Exception except)
            {
                strErrMsg = "Error al registrar el integrante." + except.Message.ToString();

            }
        }

        private void mtdInsertarIntegrante(string strIdIntegrante, string strNombreIntegrante, string strUsuarioIntegrante, string strCorreoIntegrante, 
            string strIdGrupoTrabajo, ref string strErrMsg)
        {
            clsGestionUsuarios cUsuario = new clsGestionUsuarios();
            clsDTOGestionUsuarios objUsuario = new clsDTOGestionUsuarios(strIdIntegrante, strNombreIntegrante, strUsuarioIntegrante, 
                strCorreoIntegrante, strIdGrupoTrabajo);

            cUsuario.mtdInsertarIntegrante(objUsuario, ref strErrMsg);
        }

        protected void imgbtnNoRegIntegrante_Click(object sender, ImageClickEventArgs e)
        {
            divDatosUsuarios.Visible = false;
            btnRegIntegrante.Visible = false;
            btnNoRegIntegrante.Visible = false;
            imgBtnInsertarIntegrante.Visible = true;
            StrNombreIntegrante.Text = "";
            StrUsuarioIntegrante.Text = "";
            StrCorreoIntegrante.Text = "";
        }

        private void mtdResetCamposs()
        {
            NombreIntegrante.Visible = false;
            StrNombreIntegrante.Text = "";
            StrUsuarioIntegrante.Text = "";
            StrCorreoIntegrante.Text = "";
            btnRegIntegrante.Visible = false;
            btnNoRegIntegrante.Visible = false;
            imgBtnInsertarIntegrante.Visible = true;
            btnUpdIntegrante.Visible = false;
        }

        ///
        ////// Grid gvIntegrantes
        ///
        #endregion

        #region Envío de notificaciones
        //private void Mensaje(String Mensaje)
        //{
        //    lblMsgBox.Text = Mensaje;
        //    mpeMsgBox.Show();
        //}

        //private void mtdGenerarNotificacion(String StrNombreCampo, String StrPosicion,
        //    bool BooEsParametrico)
        //{
        //    try
        //    {
        //        string TextoAdicional = string.Empty;

        //        TextoAdicional = "NOTIFICACIÓN DE MODIFICACIÓN DE ESTRUCTURA DE ARCHIVOS" + "<br>";
        //        TextoAdicional = TextoAdicional + "<br>";
        //        TextoAdicional = TextoAdicional + " Justificación : Se ha llevado a cabo el cambio de información de estructura de archivos.<br>";
        //        TextoAdicional = TextoAdicional + " Nombre de la estructura : " + StrNombreCampo + "<br>";
        //        TextoAdicional = TextoAdicional + " Posición : " + StrPosicion + "<br>";
        //        if (BooEsParametrico == true)
        //        {
        //            TextoAdicional = TextoAdicional + " Estructura parametizable. <br>";
        //        }
        //        else
        //        {
        //            TextoAdicional = TextoAdicional + " Estructura no parametizable. <br>";
        //        }
        //        TextoAdicional = TextoAdicional + " Fecha de la modificación : " + System.DateTime.Now.ToString() + "<br>";
        //        TextoAdicional = TextoAdicional + " Usuario de Registro : " + Session["loginUsuario"].ToString() + "<br>";
        //        TextoAdicional = TextoAdicional + " Nombre Usuario Registro : " + Session["nombreUsuario"].ToString() + "<br>";

        //        boolEnviarNotificacion(StrNombreCampo, Convert.ToInt16(Session["IdJerarquia"]), StrPosicion, TextoAdicional);
        //    }
        //    catch (Exception ex)
        //    {
        //        //strErrMsg = string.Format("Mensaje de error. [{0}]", ex.Message);
        //        Mensaje("Error al generar la notificacion. " + ex.Message);
        //    }
        //}

        //private Boolean boolEnviarNotificacion(string StrNombreCampo, int idNodoJerarquia,
        //    string StrPosicion, string textoAdicional)
        //{
        //    #region Variables
        //    bool err = false;
        //    string Destinatario = string.Empty, Copia = string.Empty, Asunto = string.Empty, Otros = string.Empty, Cuerpo = string.Empty, NroDiasRecordatorio = string.Empty;
        //    string selectCommand = string.Empty, AJefeInmediato = string.Empty, AJefeMediato = string.Empty, RequiereFechaCierre = string.Empty;
        //    string idJefeInmediato = string.Empty, idJefeMediato = string.Empty;
        //    string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
        //    #endregion Variables

        //    try
        //    {
        //        #region informacion basica
        //        SqlDataAdapter dad = null;
        //        DataTable dtblDiscuss = new DataTable();
        //        DataView view = null;

        //        if (!string.IsNullOrEmpty(StrNombreCampo.ToString().Trim()))
        //        {
        //            //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
        //            selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
        //                "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
        //                "WHERE E. IdEvento = 103";

        //            dad = new SqlDataAdapter(selectCommand, conString);
        //            dad.Fill(dtblDiscuss);
        //            view = new DataView(dtblDiscuss);

        //            foreach (DataRowView row in view)
        //            {
        //                Copia = row["Copia"].ToString().Trim();
        //                Otros = row["Otros"].ToString().Trim();
        //                Asunto = row["Asunto"].ToString().Trim();
        //                Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
        //                NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
        //                AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
        //                AJefeMediato = row["AJefeMediato"].ToString().Trim();
        //                RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
        //            }
        //        }
        //        #endregion

        //        #region correo del Destinatario
        //        //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
        //        if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
        //        {
        //            selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
        //                "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
        //                "WHERE JO.idHijo = " + idNodoJerarquia;

        //            dad = new SqlDataAdapter(selectCommand, conString);
        //            dtblDiscuss.Clear();
        //            dad.Fill(dtblDiscuss);
        //            view = new DataView(dtblDiscuss);

        //            foreach (DataRowView row in view)
        //            {
        //                Destinatario = row["CorreoResponsable"].ToString().Trim();
        //                idJefeInmediato = row["idPadre"].ToString().Trim();
        //            }
        //        }
        //        #endregion

        //        #region correo del Jefe Inmediato
        //        //Consulta el correo del Jefe Inmediato
        //        if (AJefeInmediato == "SI")
        //        {
        //            if (!string.IsNullOrEmpty(idJefeInmediato.Trim()))
        //            {
        //                selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
        //                    "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
        //                    "WHERE JO.idHijo = " + idJefeInmediato;

        //                dad = new SqlDataAdapter(selectCommand, conString);
        //                dtblDiscuss.Clear();
        //                dad.Fill(dtblDiscuss);
        //                view = new DataView(dtblDiscuss);

        //                foreach (DataRowView row in view)
        //                {
        //                    Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
        //                    idJefeMediato = row["idPadre"].ToString().Trim();
        //                }
        //            }
        //        }
        //        #endregion

        //        #region correo del Jefe Mediato
        //        //Consulta el correo del Jefe Mediato
        //        if (AJefeMediato == "SI")
        //        {
        //            if (!string.IsNullOrEmpty(idJefeMediato.Trim()))
        //            {
        //                selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
        //                    "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
        //                    "WHERE JO.idHijo = " + idJefeMediato;

        //                dad = new SqlDataAdapter(selectCommand, conString);
        //                dtblDiscuss.Clear();
        //                dad.Fill(dtblDiscuss);
        //                view = new DataView(dtblDiscuss);

        //                foreach (DataRowView row in view)
        //                {
        //                    Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
        //                }
        //            }
        //        }
        //        #endregion

        //        #region Correos Enviados
        //        //Insertar el Registro en la tabla de Correos Enviados
        //        //SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
        //        //SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
        //        //SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
        //        //SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
        //        //SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
        //        //SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
        //        //SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
        //        //SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
        //        //SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
        //        //SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
        //        //SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
        //        //SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
        //        //SqlDataSource200.Insert();
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the Exception.
        //        Mensaje("Error en el envío de la notificación. " + ex.Message);
        //        err = true;
        //    }

        //    if (!err)
        //    {
        //        #region Restro
        //        // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
        //        if (RequiereFechaCierre == "SI" && StrNombreCampo != "")
        //        {
        //            ////Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
        //            //SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
        //            //SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
        //            //SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
        //            //SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = StrNombreParametro;
        //            //SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
        //            //SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
        //            //SqlDataSource201.Insert();
        //        }
        //        #endregion

        //        try
        //        {
        //            #region Envio Correo
        //            MailMessage message = new MailMessage();
        //            SmtpClient smtpClient = new SmtpClient();
        //            MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");
        //            message.From = fromAddress;//here you can set address

        //            #region Destinatario
        //            foreach (string substr in Destinatario.Split(';'))
        //            {
        //                if (!string.IsNullOrEmpty(substr.Trim()))
        //                    message.To.Add(substr);
        //            }
        //            #endregion

        //            #region Copia
        //            if (Copia.Trim() != "")
        //                foreach (string substr in Copia.Split(';'))
        //                {
        //                    if (!string.IsNullOrEmpty(substr.Trim()))
        //                        message.CC.Add(substr);
        //                }
        //            #endregion

        //            #region Otros
        //            if (Otros.Trim() != "")
        //                foreach (string substr in Otros.Split(';'))
        //                {
        //                    if (!string.IsNullOrEmpty(substr.Trim()))
        //                        message.CC.Add(substr);
        //                }
        //            #endregion

        //            message.Subject = Asunto;//subject of email
        //            message.IsBodyHtml = true;//To determine email body is html or not
        //            message.Body = Cuerpo;

        //            smtpClient.Send(message);
        //            #endregion
        //        }
        //        catch (Exception ex)
        //        {
        //            Mensaje("Error en el envío de la notificación. " + ex.Message);
        //            err = true;
        //        }

        //        if (!err)
        //        {
        //            ////Actualiza el Estado del Correo Enviado
        //            //SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
        //            //SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
        //            //SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
        //            //SqlDataSource200.Update();
        //        }
        //    }

        //    return (err);
        //}
        #endregion

    }
}