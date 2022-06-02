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

namespace ListasSarlaft.UserControls.Proceso.Param.Iframe
{
    public partial class QuestionsIframe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
           // ScriptManager.RegisterStartupScript(Page, typeof(string), "script", "<script type=text/javascript>parent.location.href = parent.location.href;</script>", false);
                    if (Request.QueryString["idEncuesta"] != null)
                    {
                        IdEncuesta.Text = Request.QueryString["idEncuesta"].ToString();
                        CantQuestion.Text = Request.QueryString["cantQuestion"].ToString();
                        int cantQuestion = Convert.ToInt32(Request.QueryString["cantQuestion"].ToString());
                        int IdEncuestaint = Convert.ToInt32(Request.QueryString["idEncuesta"].ToString());
                        string strErrMsg = string.Empty;
                        List<clsPreguntasEncuestas> lstPreguntas = new List<clsPreguntasEncuestas>();
                        clsEncuestasBLL cEncuesta = new clsEncuestasBLL();
                        if (Request.QueryString["op"] == "1")
                        {
                            for (int i = 0; i < cantQuestion; i++)
                            {
                                CreateTextBox("TXquestion" + i, i);
                            }
                            IBinsertGVC.Visible = true;
                            IBupdateGVC.Visible = false;
                        }
                        else
                        {
                            lstPreguntas = cEncuesta.mtdConsultarPreguntas(ref lstPreguntas, ref strErrMsg, ref IdEncuestaint);
                            if (lstPreguntas != null)
                            {
                                for (int i = 0; i < cantQuestion; i++)
                                {
                                    CreateTextBoxUpdate("TXquestion" + i, i, lstPreguntas);
                                }
                                IBinsertGVC.Visible = false;
                                IBupdateGVC.Visible = true;
                            }
                            else
                            {
                                omb.ShowMessage("No hay preguntas registradas", 2, "Atención");
                            }
                        }
                    }
                    else
                    {
                        
                    }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("txtDynamic")).ToList();
            int i = 1;
            foreach (string key in keys)
            {
                //this.CreateTextBox("txtDynamic" + i);
                i++;
            }
        }

        protected void AddTextBox(object sender, EventArgs e)
        {
            int index = pnlTextBoxes.Controls.OfType<TextBox>().ToList().Count + 1;
            //this.CreateTextBox("txtDynamic" + index);
        }

        private void CreateTextBoxUpdate(string id, int i, List<clsPreguntasEncuestas> lstPreguntas)
        {
            TextBox txt = new TextBox();
            txt.ID = id;
            Label LtextQuestion = new Label();
            int dato = i + 1;
            LtextQuestion.Text = "Texto de la pregunta #"+ dato;
            TextBox LtextIdQuestion = new TextBox();
            LtextIdQuestion.Visible = false;
            LtextIdQuestion.ID = "Id" + i;
            foreach (clsPreguntasEncuestas objEvaComp in lstPreguntas)
            {
                int valor = Convert.ToInt32(objEvaComp.intConsecutivo.ToString().Trim());
                
                
                
                if (i + 1 == valor)
                {
                    LtextIdQuestion.Text = objEvaComp.intIdPregunta.ToString();
                    txt.Text = objEvaComp.strTextoPregunta.ToString().Trim();
                }
            }
            pnlTextBoxes.Controls.Add(LtextIdQuestion);
            pnlTextBoxes.Controls.Add(LtextQuestion);
            pnlTextBoxes.Controls.Add(txt);

            Literal lt = new Literal();
            lt.Text = "<br />";
            pnlTextBoxes.Controls.Add(lt);
        }
        private void CreateTextBox(string id, int i)
        {
            TextBox txt = new TextBox();
            txt.ID = id;
            Label LtextQuestion = new Label();
            int dato = i + 1;
            LtextQuestion.Text = "Texto de la pregunta #" + dato;
            pnlTextBoxes.Controls.Add(LtextQuestion);
            pnlTextBoxes.Controls.Add(txt);

            Literal lt = new Literal();
            lt.Text = "<br />";
            pnlTextBoxes.Controls.Add(lt);
        }

        protected void GetTextBoxValues(object sender, EventArgs e)
        {
            string message = "";
            foreach (TextBox textBox in pnlTextBoxes.Controls.OfType<TextBox>())
            {
                message += textBox.ID + ": " + textBox.Text + "\\n";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            #region Variables
            clsPreguntasEncuestas cPreguntas = new clsPreguntasEncuestas();
            int consecutivo = 0;
            bool booResult = false;
            string strErrMsg = string.Empty;
            clsEncuestasBLL cEncuesta = new clsEncuestasBLL();
            int cantQuestion = Convert.ToInt32(CantQuestion.Text);
            int IdEncuestaint = Convert.ToInt32(IdEncuesta.Text);
            #endregion
            foreach (TextBox textBox in pnlTextBoxes.Controls.OfType<TextBox>())
            {
                
                string IdLabel = "";
                foreach (TextBox Labels in pnlTextBoxes.Controls.OfType<TextBox>())
                {
                    if (Labels.ID == "Id" + consecutivo)
                    {
                        IdLabel = Labels.Text;
                    }
                    
                }
                if (IdLabel != "")
                {
                    string textoPregunta = "";
                    if (textBox.ID == "TXquestion" + consecutivo)
                    {
                        textoPregunta = textBox.Text;
                        consecutivo++;
                    }
                    
                    if (textoPregunta != "")
                    {
                        cPreguntas.intConsecutivo = consecutivo;
                        cPreguntas.strTextoPregunta = textoPregunta;
                        cPreguntas.intIdPregunta = Convert.ToInt32(IdLabel);
                        booResult = cEncuesta.mtdActualizarPregunta(cPreguntas, ref strErrMsg, ref IdEncuestaint);
                        
                    }
                    
                }
            }
            if (booResult == true)
            {
                strErrMsg = "Preguntas de la Encuesta registradas exitosamente";
                //OptionsQuestions.Text = "3";
            }
            else
            {
                strErrMsg = "Error en registro de Preguntas de la Encuesta";
            }

            omb.ShowMessage(strErrMsg, 3, "Atención");
            
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            #region Variables
            clsPreguntasEncuestas cPreguntas = new clsPreguntasEncuestas();
            int consecutivo = 0;
            bool booResult = false;
            string strErrMsg = string.Empty;
            clsEncuestasBLL cEncuesta = new clsEncuestasBLL();
            int cantQuestion = Convert.ToInt32(CantQuestion.Text);
            int IdEncuestaint = Convert.ToInt32(IdEncuesta.Text);
            #endregion
            foreach (TextBox textBox in pnlTextBoxes.Controls.OfType<TextBox>())
            {
                cPreguntas.intConsecutivo = consecutivo + 1;
                cPreguntas.strTextoPregunta = textBox.Text;
                booResult = cEncuesta.mtdInsertarPregunta(cPreguntas, ref strErrMsg, ref IdEncuestaint);
                consecutivo++;
            }
            if (booResult == true)
            {
                strErrMsg = "Preguntas de la Encuesta registradas exitosamente";
                OptionsQuestions.Text = "3";
            }
            else
            {
                strErrMsg = "Preguntas de la Encuesta registradas exitosamente";
            }

            omb.ShowMessage(strErrMsg, 3, "Atención");
            ClientScript.RegisterStartupScript(Page.GetType(), "RefreshParent", "<script type='text/javascript'>var btn = window.parent.document.getElementById('btnImgCancelar');if (btn) btn.click();</script>");
            //Parent.Page.AutoPostBackControl;
            //Response.Redirect("~/UserControls/Proceso/Param/Encuestas.ascx?metod=Rest");
        }

    }
}