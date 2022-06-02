using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using ClosedXML.Excel;

namespace ListasSarlaft.UserControls.Proceso.Param
{
    public partial class Encuestas : System.Web.UI.UserControl
    {
        string IdFormulario = "4030";
        cCuenta cCuenta = new cCuenta();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            /*if (Request.QueryString["op"] == "Rest")
            {
                mtdRestFields();
                mtdStard();
            }*/
            /*scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);*/
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                /*if (Session["Postback"] == null)
                {*/
                    if (!Page.IsPostBack)
                    {
                        mtdStard();
                        //PopulateTreeView();
                        mtdInicializarValores();

                    }
                //}
            }
            
        }
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
            //txtFecha.Text = "" + DateTime.Now;
            PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            mtdLoadEncuestas(ref strErrMsg);
        }
        protected void mtdRestFields()
        {
            BodyGridEnc.Visible = true;
            BodyFormEnc.Visible = false;
            DVbuttons.Visible = false;
            txtId.Text = "";
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
            TXnombre.Text = "";
            TXcantPregunta.Text = "";
            TXdescripcion.Text = "";
            DVpreguntas.Visible = false;
            DVbuttons.Visible = false;
            TXpregunta.Text = "";
            TXconsecutivo.Text = "";
            DVbutonQuestion.Visible = false;
            IBinsertQuestions.Visible = false;
        }
        #region Properties
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;
        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;
        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

        private DataTable infoGridPrint;
        private int rowGridPrint;

        private DataTable InfoGrid1
        {
            get
            {
                infoGrid1 = (DataTable)ViewState["infoGrid1"];
                return infoGrid1;
            }
            set
            {
                infoGrid1 = value;
                ViewState["infoGrid1"] = infoGrid1;
            }
        }

        private int RowGrid1
        {
            get
            {
                rowGrid1 = (int)ViewState["rowGrid1"];
                return rowGrid1;
            }
            set
            {
                rowGrid1 = value;
                ViewState["rowGrid1"] = rowGrid1;
            }
        }

        private int PagIndex1
        {
            get
            {
                pagIndex1 = (int)ViewState["pagIndex1"];
                return pagIndex1;
            }
            set
            {
                pagIndex1 = value;
                ViewState["pagIndex1"] = pagIndex1;
            }
        }

        private DataTable InfoGrid2
        {
            get
            {
                infoGrid2 = (DataTable)ViewState["infoGrid2"];
                return infoGrid2;
            }
            set
            {
                infoGrid2 = value;
                ViewState["infoGrid2"] = infoGrid2;
            }
        }

        private int RowGrid2
        {
            get
            {
                rowGrid2 = (int)ViewState["rowGrid2"];
                return rowGrid2;
            }
            set
            {
                rowGrid2 = value;
                ViewState["rowGrid2"] = rowGrid2;
            }
        }

        private int PagIndex2
        {
            get
            {
                pagIndex2 = (int)ViewState["pagIndex2"];
                return pagIndex2;
            }
            set
            {
                pagIndex2 = value;
                ViewState["pagIndex2"] = pagIndex2;
            }
        }

        private DataTable InfoGrid3
        {
            get
            {
                infoGrid3 = (DataTable)ViewState["infoGrid3"];
                return infoGrid3;
            }
            set
            {
                infoGrid3 = value;
                ViewState["infoGrid3"] = infoGrid3;
            }
        }
        private int RowGrid3
        {
            get
            {
                rowGrid3 = (int)ViewState["rowGrid3"];
                return rowGrid3;
            }
            set
            {
                rowGrid3 = value;
                ViewState["rowGrid3"] = rowGrid3;
            }
        }

        private int PagIndex3
        {
            get
            {
                pagIndex3 = (int)ViewState["pagIndex3"];
                return pagIndex3;
            }
            set
            {
                pagIndex3 = value;
                ViewState["pagIndex3"] = pagIndex3;
            }
        }


        private DataTable InfoGridPrint
        {
            get
            {
                infoGridPrint = (DataTable)ViewState["infoGridPrint"];
                return infoGridPrint;
            }
            set
            {
                infoGridPrint = value;
                ViewState["infoGridPrint"] = infoGridPrint;
            }
        }

        private int RowGridPrint
        {
            get
            {
                rowGridPrint = (int)ViewState["rowGridPrint"];
                return rowGridPrint;
            }
            set
            {
                rowGridPrint = value;
                ViewState["rowGridPrint"] = rowGridPrint;
            }
        }
        #endregion
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }

            BodyGridEnc.Visible = false;
            BodyFormEnc.Visible = true;

            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
            DVbuttons.Visible = true;

            Session.Remove("IdEncuesta");
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }

            string strErrMsg = String.Empty;
            if (mtdInsertarEncuesta(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                mtdStard();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
                mtdRestFields();
                mtdStard();
            }
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }

            string strErrMsg = String.Empty;
            if (mtdUpdateEncuesta(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                mtdStard();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
                mtdRestFields();
                mtdStard();
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdRestFields();
            mtdStard();
        }

        
        /// <summary>
        /// Realiza la insercion de la evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        protected bool mtdInsertarEncuesta(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEncuestas cEncuesta = new clsEncuestas();
            clsEncuestasBLL cEncuestaBll = new clsEncuestasBLL();
            cEncuesta.strNombreEncuesta = TXnombre.Text;
            cEncuesta.strDescripcionEmpresa = TXdescripcion.Text;
            cEncuesta.intCantPreguntas = Convert.ToInt32(TXcantPregunta.Text);
            cEncuesta.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            cEncuesta.dtFechaRegistro = DateTime.UtcNow;
            

            #endregion

            booResult = cEncuestaBll.mtdInsertarEncuestas(cEncuesta, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Encuesta creada con Exito";
                //int IdEncuesta = cEncuestaBll.mtdLastIdEncuesta(ref strErrMsg);
                //booResult = mtdInsertarAspectos(GVProveedorCriterios, IdEvaProveedor, ref cValorEvaluacionInd, ref strErrMsg);
                /*int cantQuestion = Convert.ToInt32(TXcantPregunta.Text);
                DVpreguntas.Visible = true;
                questionsI.Attributes["src"] = "../../../Formularios/Proceso/Admin/Iframe/QuestionsIframe.aspx?idEncuesta=" + IdEncuesta + "&cantQuestion=" + cantQuestion+"&op=1";
                questionsI.Visible = true;
                DVbuttons.Visible = false;*/
            }
            else
            {
                strErrMsg = "Error al registrar la Encuesta";
            }
            return booResult;
        }
        /// <summary>
        /// Realiza la insercion de la evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        protected bool mtdUpdateEncuesta(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEncuestas cEncuesta = new clsEncuestas();
            clsEncuestasBLL cEncuestaBll = new clsEncuestasBLL();
            cEncuesta.intIdEncuesta = Convert.ToInt32(txtId.Text);
            cEncuesta.strNombreEncuesta = TXnombre.Text;
            cEncuesta.strDescripcionEmpresa = TXdescripcion.Text;
            cEncuesta.intCantPreguntas = Convert.ToInt32(TXcantPregunta.Text);
            cEncuesta.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            cEncuesta.dtFechaRegistro = DateTime.Now;


            #endregion

            booResult = cEncuestaBll.mtdActualizarEncuesta(cEncuesta, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Encuesta actualizada con Exito";
                //int IdEncuesta = cEncuestaBll.mtdLastIdEncuesta(ref strErrMsg);
                //booResult = mtdInsertarAspectos(GVProveedorCriterios, IdEvaProveedor, ref cValorEvaluacionInd, ref strErrMsg);
                /*int cantQuestion = Convert.ToInt32(TXcantPregunta.Text);
                DVpreguntas.Visible = true;
                questionsI.Attributes["src"] = "../../../Formularios/Proceso/Admin/Iframe/QuestionsIframe.aspx?idEncuesta=" + IdEncuesta + "&cantQuestion=" + cantQuestion+"&op=1";
                questionsI.Visible = true;
                DVbuttons.Visible = false;*/
            }
            else
            {
                strErrMsg = "Error al actualizar la Encuesta";
            }
            return booResult;
        }
        #region LoadGrid
        private void mtdLoadEncuestas(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEncuestas objEncuesta = new clsEncuestas();
            List<clsEncuestas> lstEncuesta = new List<clsEncuestas>();
            clsEncuestasBLL cEncuesta = new clsEncuestasBLL();
            #endregion Vars
            lstEncuesta = cEncuesta.mtdConsultarEncuesta(ref lstEncuesta, ref strErrMsg);

            if (lstEncuesta != null)
            {
                mtdLoadEncuestas();
                mtdLoadEncuestas(lstEncuesta);
                GVencuestas.DataSource = lstEncuesta;
                GVencuestas.PageIndex = pagIndex2;
                GVencuestas.DataBind();
                booResult = true;
            }

        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadEncuestas()
        {
            DataTable gridEnc = new DataTable();

            gridEnc.Columns.Add("intIdEncuesta", typeof(string));
            gridEnc.Columns.Add("strNombreEncuesta", typeof(string));
            gridEnc.Columns.Add("strDescripcionEmpresa", typeof(string));
            gridEnc.Columns.Add("intCantPreguntas", typeof(string));
            gridEnc.Columns.Add("dtFechaRegistro", typeof(string));
            gridEnc.Columns.Add("intIdUsuario", typeof(string));
            gridEnc.Columns.Add("strUsuario", typeof(string));

            GVencuestas.DataSource = gridEnc;
            GVencuestas.DataBind();
            InfoGrid1 = gridEnc;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadEncuestas(List<clsEncuestas> lstEncuestas)
        {
            string strErrMsg = String.Empty;
            clsValorEvaluacionProveedorBLL cCrlInfra = new clsValorEvaluacionProveedorBLL();

            foreach (clsEncuestas objEvaComp in lstEncuestas)
            {

                InfoGrid1.Rows.Add(new Object[] {
                    objEvaComp.intIdEncuesta.ToString().Trim(),
                    objEvaComp.strNombreEncuesta.ToString().Trim(),
                    objEvaComp.intCantPreguntas.ToString().Trim(),
                    objEvaComp.dtFechaRegistro.ToString().Trim(),
                    objEvaComp.strDescripcionEmpresa.ToString().Trim(),
                    objEvaComp.intIdUsuario.ToString().Trim(),
                    objEvaComp.strUsuario.ToString().Trim()
                    });
            }
        }
        #endregion

        protected void GVencuestas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session.Remove("IdEncuesta");
            RowGrid1 = Convert.ToInt16(e.CommandArgument);
            Session["RowGrid1"] = RowGrid1;
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdShowUpdate(RowGrid1);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    DVbuttons.Visible = true;
                    break;
                case "Preguntas":
                    //mtdShowQuestion(RowGrid1);
                    BodyGridEnc.Visible = false;
                    mtdLoadQuestions(RowGrid1);
                    break;
            }
        }
        private void mtdLoadQuestions(int RowGrid1)
        {
            //string encuesta = Session["IdEncuesta"].ToString();
            clsEncuestasBLL cEncuestaBll = new clsEncuestasBLL();
            List<clsPreguntasEncuestas> lstPreguntasEncuestas = new List<clsPreguntasEncuestas>();
            string strErrMsg = string.Empty;
            int IdEncuesta = 0;
            if (Session["IdEncuesta"] == null)
            {
                GridViewRow row = GVencuestas.Rows[RowGrid1];
                var colsNoVisible = GVencuestas.DataKeys[RowGrid1].Values;


                IdEncuesta = Convert.ToInt32(row.Cells[0].Text);
                int CantPreguntas = Convert.ToInt32(row.Cells[3].Text);

                Session["IdEncuesta"] = IdEncuesta;
                Session["Preguntas"] = CantPreguntas;
            }
            else
            {
                IdEncuesta = Convert.ToInt32(Session["IdEncuesta"].ToString());
            }
            lstPreguntasEncuestas = cEncuestaBll.mtdConsultarPreguntas(ref lstPreguntasEncuestas, ref strErrMsg, ref IdEncuesta);

            if (lstPreguntasEncuestas != null)
            {
                mtdLoadPreuntasEncuestas();
                mtdLoadPreguntasEncuestas(lstPreguntasEncuestas);
                GVpreguntas.DataSource = lstPreguntasEncuestas;
                GVpreguntas.PageIndex = pagIndex1;
                GVpreguntas.DataBind();
                GVpreguntas.Visible = true;
                //booResult = true;
                DVpreguntas.Visible = true;
                GridQuestions.Visible = true;
                FormQuestion.Visible = false;
                DVbuttons.Visible = false;
                DVbutonQuestion.Visible = false;
                tbuttonsQUestion.Visible = true;
                Dquestions.Visible = true;
            }
            else
            {
                GVpreguntas.Visible = false;
                DVpreguntas.Visible = true;
                GridQuestions.Visible = true;
                FormQuestion.Visible = false;
                DVbuttons.Visible = false;
                DVbutonQuestion.Visible = false;
                //tbuttonsQUestion.Visible = true;
                //DVbutonQuestion.Visible = true;
            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadPreuntasEncuestas()
        {
            DataTable gridEnc = new DataTable();

            gridEnc.Columns.Add("intIdPregunta", typeof(string));
            gridEnc.Columns.Add("strTextoPregunta", typeof(string));
            gridEnc.Columns.Add("intConsecutivo", typeof(string));

            GVencuestas.DataSource = gridEnc;
            GVencuestas.DataBind();
            InfoGrid2 = gridEnc;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadPreguntasEncuestas(List<clsPreguntasEncuestas> lstPreguntasEncuestas)
        {
            string strErrMsg = String.Empty;
            //clsValorEvaluacionProveedorBLL cCrlInfra = new clsValorEvaluacionProveedorBLL();

            foreach (clsPreguntasEncuestas objEvaComp in lstPreguntasEncuestas)
            {

                InfoGrid2.Rows.Add(new Object[] {
                    objEvaComp.intIdPregunta.ToString().Trim(),
                    objEvaComp.strTextoPregunta.ToString().Trim(),
                    objEvaComp.intConsecutivo.ToString().Trim()
                    });
            }
        }
        private void mtdShowQuestion(int RowGrid1)
        {
            /*GridViewRow row = GVencuestas.Rows[RowGrid1];
            var colsNoVisible = GVencuestas.DataKeys[RowGrid1].Values;*/
            clsEncuestasBLL cEncuestaBll = new clsEncuestasBLL();
            string strErrMsg = string.Empty;
            int IdEncuesta = Convert.ToInt32(Session["IdEncuesta"].ToString().Trim());
            int CantPreguntas = Convert.ToInt32(Session["Preguntas"].ToString().Trim());
            int Consecutivo = cEncuestaBll.mtdConsecutivoQuestions(ref strErrMsg, IdEncuesta);
            
                Consecutivo++;
                if(Consecutivo > CantPreguntas)
                {
                    omb.ShowMessage("Se han registrado toda la cantidad de preguntas de la encuesta registrada", 2, "Atención");
                mtdRestFields();
                mtdStard();
            }
            else
                {
                Session["Consecutivo"] = Consecutivo;
                    TXconsecutivo.Text = "" + Consecutivo;
                    FormQuestion.Visible = true;
                    DVbutonQuestion.Visible = true;
                    IBinsertQuestions.Visible = true;
                }
        }
        private void mtdShowUpdate(int RowGrid1)
        {
            GridViewRow row = GVencuestas.Rows[RowGrid1];
            var colsNoVisible = GVencuestas.DataKeys[RowGrid1].Values;
            txtId.Text = row.Cells[0].Text;
            TXnombre.Text = ((Label)row.FindControl("NombreEncuesta")).Text;
            TXdescripcion.Text = ((Label)row.FindControl("DescripcionEmpresa")).Text;
            TXcantPregunta.Text = row.Cells[3].Text;
            txtFecha.Text = colsNoVisible[1].ToString();
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            string strErrMsg = string.Empty;
            int IdEncuesta = Convert.ToInt32(row.Cells[0].Text);
            BodyGridEnc.Visible = false;
            BodyFormEnc.Visible = true;
            int cantQuestion = Convert.ToInt32(row.Cells[3].Text);
            /*List<clsPreguntasEncuestas> lstPreguntas = new List<clsPreguntasEncuestas>();
            clsEncuestasBLL cEncuesta = new clsEncuestasBLL();

            lstPreguntas = cEncuesta.mtdConsultarPreguntas(ref lstPreguntas, ref strErrMsg, ref IdEncuesta);
            
            if (lstPreguntas != null)
            {
                
                System.Web.UI.WebControls.Table Tquestion = new System.Web.UI.WebControls.Table();
                Tquestion.CssClass = "TableContains";

                int identificador = 0;
                for (int i = 0; i < cantQuestion; i++)
                {
                    identificador = i + 1;
                    TableRow tRow = new TableRow();
                    TableCell tCellText = new TableCell();
                    tCellText.CssClass = "RowsText";
                    Label LtextQuestion = new Label();
                    LtextQuestion.Text = "Texto de la pregunta #" + identificador;
                    tCellText.Controls.Add(LtextQuestion);
                    tRow.Cells.Add(tCellText);

                    TableCell tCellValue = new TableCell();
                    TextBox TXquestion = new TextBox();
                    TXquestion.Width = 300;
                    TXquestion.ID = "TXquestion" + i;
                    
                    foreach (clsPreguntasEncuestas objEvaComp in lstPreguntas)
                    {
                        int valor = Convert.ToInt32(objEvaComp.intConsecutivo.ToString().Trim());
                        if (i+1 == valor)
                        {
                            TXquestion.Text = objEvaComp.strTextoPregunta.ToString().Trim();
                        }
                    }
                    tCellValue.Controls.Add(TXquestion);
                    tRow.Cells.Add(tCellValue);
                    Tquestion.Rows.Add(tRow);
                }

                preguntas.Controls.Add(Tquestion);
                DVpreguntas.Visible = true;
            }*/
            //DVpreguntas.Visible = true;
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            mtdRestFields();
        }

        protected void IBinsertQuestions_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }


            #region Variables
            clsPreguntasEncuestas cPreguntas = new clsPreguntasEncuestas();
            bool booResult = false;
            string strErrMsg = string.Empty;
            clsEncuestasBLL cEncuesta = new clsEncuestasBLL();
            #endregion
            int IdEncuestaint = Convert.ToInt32(Session["IdEncuesta"].ToString().Trim());
            cPreguntas.intConsecutivo = Convert.ToInt32(Session["Consecutivo"].ToString().Trim());
            cPreguntas.strTextoPregunta = TXpregunta.Text;
            booResult = cEncuesta.mtdInsertarPregunta(cPreguntas, ref strErrMsg, ref IdEncuestaint);
            if (booResult == true)
            {
                omb.ShowMessage("Pregunta Registrada con Exito", 3, "Atención");
                FormQuestion.Visible = false;
            } else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
                FormQuestion.Visible = false;
            }
            
            int fila = Convert.ToInt32(Session["RowGrid1"].ToString().Trim());
            mtdLoadQuestions(fila);
            DVbuttons.Visible = false;
            TXconsecutivo.Text = "";
            TXpregunta.Text = "";
            /*Session["Postback"] = "1";
            Response.Redirect(Request.Url.AbsoluteUri);*/
        }

        protected void IBnewQuestion_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }

            int fila = Convert.ToInt32(Session["RowGrid1"].ToString().Trim());
            mtdShowQuestion(fila);
            GridQuestions.Visible = false;
            tbuttonsQUestion.Visible = false;
            IBupdateQuestions.Visible = false;
        }

        protected void GVpreguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid2 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdShowUpdateQuestion(RowGrid2);
                    IBinsertQuestions.Visible = false;
                    IBupdateQuestions.Visible = true;
                    DVbutonQuestion.Visible = true;
                    tbuttonsQUestion.Visible = false;
                    break;
            }
        }
        private void mtdShowUpdateQuestion(int RowGrid1)
        {
            GridViewRow row = GVpreguntas.Rows[RowGrid1];
            //var colsNoVisible = GVencuestas.DataKeys[RowGrid1].Values;
            TXidQuestion.Text = row.Cells[0].Text;
            TXpregunta.Text = ((Label)row.FindControl("strTextoPregunta")).Text;
            TXconsecutivo.Text = row.Cells[2].Text;
            //string strErrMsg = string.Empty;
            GridQuestions.Visible = false;
            FormQuestion.Visible = true;

        }

        protected void IBupdateQuestions_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }

            #region Variables
            clsPreguntasEncuestas cPreguntas = new clsPreguntasEncuestas();
            //int consecutivo = 0;
            bool booResult = false;
            string strErrMsg = string.Empty;
            clsEncuestasBLL cEncuesta = new clsEncuestasBLL();
            #endregion
            cPreguntas.intConsecutivo = Convert.ToInt32(TXconsecutivo.Text);
            cPreguntas.strTextoPregunta = TXpregunta.Text;
            cPreguntas.intIdPregunta = Convert.ToInt32(TXidQuestion.Text);
            int IdEncuesta = Convert.ToInt32(Session["IdEncuesta"].ToString().Trim());
            booResult = cEncuesta.mtdActualizarPregunta(cPreguntas, ref strErrMsg, ref IdEncuesta);
            int fila = Convert.ToInt32(Session["RowGrid1"].ToString().Trim());
            if (booResult == true)
            {
                omb.ShowMessage("Pregunta Actualizada con Exito", 3, "Atención");
                /*mtdRestFields();
                mtdStard();*/
                mtdLoadQuestions(fila);
                DVbuttons.Visible = false;
                TXconsecutivo.Text = "";
                TXpregunta.Text = "";
                /*Session["Postback"] = "1";
                Response.Redirect(Request.Url.AbsoluteUri);*/
            }
            else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
                /*mtdRestFields();
                mtdStard();*/
                
                mtdLoadQuestions(fila);
                DVbuttons.Visible = false;
                TXconsecutivo.Text = "";
                TXpregunta.Text = "";
            }
        }

        protected void IBcancelQuestions_Click(object sender, ImageClickEventArgs e)
        {
            int fila = Convert.ToInt32(Session["RowGrid1"].ToString().Trim());
            mtdLoadQuestions(fila);
            tbuttonsQUestion.Visible = true;
            DVbutonQuestion.Visible = false;
            TXconsecutivo.Text = "";
            TXpregunta.Text = "";
            /*GridQuestions.Visible = false;
            tbuttonsQUestion.Visible = false;
            DVbutonQuestion.Visible = false;
            DVpreguntas.Visible = true;*/
        }

        protected void GVpreguntas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex1 = e.NewPageIndex;
            GVpreguntas.PageIndex = PagIndex1;
            GVpreguntas.DataBind();
            string strErrMsg = String.Empty;
            //mtdLoadEncuestas(ref strErrMsg);
            int fila = Convert.ToInt32(Session["RowGrid1"].ToString().Trim());
            mtdLoadQuestions(fila);
        }

        protected void GVencuestas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex2 = e.NewPageIndex;
            GVencuestas.PageIndex = PagIndex2;
            GVencuestas.DataBind();
            string strErrMsg = String.Empty;
            mtdLoadEncuestas(ref strErrMsg);
            
        }
    }
}