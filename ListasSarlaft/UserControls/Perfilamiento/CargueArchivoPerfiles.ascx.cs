using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using clsLogica;
using clsDTO;
//using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class CargueArchivoPerfiles : System.Web.UI.UserControl
    {
        string IdFormulario = "11001";
        string SenalAlertaPosTipoIdenCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIdenCabecero"].ToString();
        string SenalAlertaPosNumeroIdenCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIdenCabecero"].ToString();
        string SenalAlertaPosNombreCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombreCabecero"].ToString();
        clsCuenta cCuenta = new clsCuenta();
        ListasSarlaft.Classes.cCuenta ccCuenta = new ListasSarlaft.Classes.cCuenta();

        #region Properties
        private int rowGridArchivo;
        private DataTable infoGridArchivo;

        private int RowGridArchivo
        {
            get
            {
                rowGridArchivo = (int)ViewState["rowGridArchivo"];
                return rowGridArchivo;
            }
            set
            {
                rowGridArchivo = value;
                ViewState["rowGridArchivo"] = rowGridArchivo;
            }
        }

        private DataTable InfoGridArchivo
        {
            get
            {
                infoGridArchivo = (DataTable)ViewState["infoGridArchivo"];
                return infoGridArchivo;
            }
            set
            {
                infoGridArchivo = value;
                ViewState["infoGridArchivo"] = infoGridArchivo;
            }
        }
        #endregion Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);

            scrtManager.RegisterPostBackControl(ibtnAgregarArchivo);
            scrtManager.RegisterPostBackControl(gvArchivos);

            if (!Page.IsPostBack)
            {
                mtdInicializarValores();
                mtdLoadGridView();
            }
        }

        #region Gridview
        protected void gvArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivo = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarArchivo();
                    break;
            }
        }

        #endregion Gridview

        #region Buttons
        protected void ibtnAgregarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (fuArchivoPerfil.HasFile)
                    {
                        if (Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".txt")
                        {
                            mtdCargarArchivo();
                            mtdLoadGridView();
                        }
                        else
                            mtdMensaje("Archivo sin guardar. Solo archivos en formato .txt");
                    }
                    else
                        mtdMensaje("No hay archivos para cargar.");
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar el archivo. " + ex.Message);
            }
        }
        #endregion Buttons

        #region Methods
        private void mtdInicializarValores() { }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion Methods

        #region Loads
        private void mtdLoadGridView()
        {
            mtdLoadGridArchivo();
            mtdLoadInfoArchivo();
        }

        private void mtdLoadGridArchivo()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdArchivo", typeof(string));
            grid.Columns.Add("StrFechaRegistro", typeof(string));
            grid.Columns.Add("StrUrlArchivo", typeof(string));

            InfoGridArchivo = grid;
            gvArchivos.DataSource = InfoGridArchivo;
            gvArchivos.DataBind();
        }

        private void mtdLoadInfoArchivo()
        {
            #region Vars
            string strErrMsg = string.Empty;
            DataTable dtInfo = new DataTable();
            clsArchivo cArchivo = new clsArchivo();
            List<clsDTOArchivo> lstArchivo = new List<clsDTOArchivo>();
            #endregion Vars

            lstArchivo = cArchivo.mtdConsultarArchivos(ref strErrMsg);

            if (lstArchivo != null)
            {
                mtdLoadInfoGrid(lstArchivo);
                gvArchivos.DataSource = lstArchivo;
                gvArchivos.DataBind();
            }
        }
        #endregion Loads

        #region Manejo Archivos
        private void mtdDescargarArchivo()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsArchivo cArchivo = new clsArchivo();
            clsDTOArchivo objArchivo = new clsDTOArchivo();
            string strNombreArchivo = InfoGridArchivo.Rows[RowGridArchivo]["StrUrlArchivo"].ToString().Trim();
            #endregion Vars

            objArchivo = cArchivo.mtdDescargarArchivo(strNombreArchivo, ref strErrMsg);

            if (objArchivo != null)
            {
                #region
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(objArchivo.BArchivo);
                Response.End();
                #endregion
            }
        }

        private void mtdCargarArchivo()
        {
            try
            {
                #region Vars
                string strErrMsg = string.Empty;
                string strNombreArchivo = string.Empty;
                int intConsecutivo = 0, intRegArchivo = 0;
                clsArchivo cArchivo = new clsArchivo();
                clsParamArchivo cParamArchivo = new clsParamArchivo();
                clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, string.Empty, true);
                #endregion Vars

                intConsecutivo = cArchivo.mtdConsultarConsecutivoArchivo(ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    #region Nombre Archivo
                    if (intConsecutivo > 0)
                        strNombreArchivo = string.Format("{0}-{1}", intConsecutivo.ToString().Trim(), fuArchivoPerfil.FileName.ToString().Trim());
                    else
                        strNombreArchivo = string.Format("1-{0}", fuArchivoPerfil.FileName.ToString().Trim());
                    #endregion Nombre Archivo

                    #region Archivo binario
                    Stream fs = fuArchivoPerfil.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bArchivoData = br.ReadBytes((Int32)fs.Length);
                    #endregion Archivo binario

                    #region Recorrido de informacion del Archivo
                    int intNumeroCampos = cParamArchivo.mtdCantidadCamposEstructura(objVariable, ref strErrMsg);
                    #endregion

                    // Validar archivo
                    Stream stream = new MemoryStream(bArchivoData);

                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string s = String.Empty;
                        int counter = 0;
                        while ((s = sr.ReadLine()) != null)
                        {
                            string[] source = s.Split(';');
                            if (counter == 0)
                            {
                                string[] strCabecero = s.Split(';');

                                if (!strCabecero.Any(x => x.ToUpper() == new[] { SenalAlertaPosTipoIdenCabecero.ToUpper(), SenalAlertaPosNumeroIdenCabecero.ToUpper(), SenalAlertaPosNombreCabecero.ToUpper() }.FirstOrDefault()))
                                {
                                    throw new Exception("El archivo no contiene los campos obligatorios.");
                                }
                            }
                            int cells = source.Length;
                            if (cells != intNumeroCampos)
                                throw new Exception("El número de campos no coincide con la estructura actual.");
                            counter++;
                        }
                    }


                    if (intNumeroCampos > 0)
                    {
                        #region Inserta la informacion del archivo y realiza calculos
                        clsDTOArchivo objArchivo = new clsDTOArchivo(string.Empty, string.Empty, strNombreArchivo, bArchivoData);
                        int intOcurrencias = 0;
                        int bitFormulaAut = 0;
                        cArchivo.mtdAgregarArchivo(objArchivo, intNumeroCampos,
                            Convert.ToInt32(Session["idUsuario"].ToString().Trim()), Session["nombreUsuario"].ToString().Trim(),
                            ref intRegArchivo, ref strErrMsg, ref intOcurrencias, ref bitFormulaAut);
                        #endregion

                        #region Busca en Inspektor
                        //Se valida si tiene o no el servicio de Inspektor habilitado 
                        if (System.Configuration.ConfigurationManager.AppSettings["CLR"].ToString().Trim() == "1")
                        {
                            if (string.IsNullOrEmpty(strErrMsg))
                                if (intRegArchivo > 0)
                                    mtdBuscarInspektor(intRegArchivo, ref strErrMsg);
                        }
                        #endregion

                        #region Visualizar mensajes
                        if (string.IsNullOrEmpty(strErrMsg))
                        {
                            string strMensaje = string.Empty;
                            if(bitFormulaAut > 0)
                                strMensaje = string.Format("Se ha realizado el cargue del archivo y el análisis de las señales de alerta auntomáticas exitósamente. Se encontraron {0} coincidencias.", intOcurrencias);
                            else
                                strMensaje = string.Format("Se ha realizado el cargue del archivo correctamente. No se detectaron señales de alerta automáticas para analizar.");
                            mtdMensaje(strMensaje);
                        }
                        else
                            mtdMensaje(strErrMsg);
                        //mtdMensaje("Archivo agregado exitósamente.");
                        #endregion
                    }
                    else
                        mtdMensaje(strErrMsg);
                }
                else
                    mtdMensaje(strErrMsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void mtdLoadInfoGrid(List<clsDTOArchivo> lstArchivo)
        {
            foreach (clsDTOArchivo objArchivo in lstArchivo)
            {
                InfoGridArchivo.Rows.Add(new Object[] {
                    objArchivo.StrIdArchivo.ToString().Trim(),
                    objArchivo.StrFechaRegistro.ToString().Trim(),
                    objArchivo.StrUrlArchivo.ToString().Trim(),
                    });
            }
        }

        private void mtdBuscarInspektor(int intRegArchivo, ref string strErrMsg)
        {
            #region Vars
            string strInfoInspektor = string.Empty;
            List<clsDTOHistoricoCalculoPerfil> lstHist = new List<clsDTOHistoricoCalculoPerfil>();
            clsPerfil cPerfil = new clsPerfil();
            string strPwdInspektor = System.Configuration.ConfigurationManager.AppSettings["CLRpw"].ToString();
            //Implementación Servicio Web Inspektor
            wsInspektorPrd.WSInspektor wsInspektorPrd = new wsInspektorPrd.WSInspektor();
            ServiceReference1.WebServiceListasSoapClient wsInspektor = new ServiceReference1.WebServiceListasSoapClient("WebServiceListasSoap12");
            #endregion

            try
            {
                #region Llamar a inspektor
                lstHist = cPerfil.mtdConsultarHistPerfiles(new clsDTOArchivo(intRegArchivo.ToString(), string.Empty, string.Empty, null), ref strErrMsg);

                foreach (clsDTOHistoricoCalculoPerfil objHist in lstHist)
                {
                    strInfoInspektor = wsInspektor.ConsultaListas(objHist.StrNroDocCliente, objHist.StrNombreCliente, strPwdInspektor);
                    //strInfoInspektor = wsInspektorPrd.LoadWSInspektor(objHist.StrNroDocCliente, objHist.StrNombreCliente,strPwdInspektor);
                    
                    //Se depura la respuesta del WsInspektor
                    if (strInfoInspektor.Contains("#No:"))
                    {
                        strInfoInspektor = strInfoInspektor.Substring(0, strInfoInspektor.IndexOf("#No:"));
                    }

                    if (!string.IsNullOrEmpty(strInfoInspektor.Trim()))
                    {
                        objHist.SetInfoInspektor(strInfoInspektor);
                        cPerfil.mtdActualizarHistoricoInfoInspektor(objHist, ref strErrMsg);
                        cPerfil.mtdInsHistClienteInfoInspektor(objHist, ref strErrMsg);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al llamar a INSPEKTOR. [{0}]", ex.Message);
            }
        }
        #endregion
    }
}