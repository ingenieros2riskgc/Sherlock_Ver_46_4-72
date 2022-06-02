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

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class GestionEvaluacionServicio : System.Web.UI.UserControl
    {
        string IdFormulario = "4025";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFEvaExport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    //PopulateTreeView();
                    mtdInicializarValores();

                }
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
            if (mtdLoadEvaluacionServicio(ref strErrMsg) == false)
            {
                strErrMsg = "No hay datos para cargar";
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }
        protected void mtdRestFields()
        {
            BodyGridGES.Visible = true;
            BodyFormGES.Visible = false;

            TXciudad.Text = "";
            TXciudad.Enabled = true;
            txtId.Text = "";
            TXfechaEva.Text = "";
            TXfechaEva.Enabled = true;
            TXnombreCliente.Text = "";
            TXnombreCliente.Enabled = true;
            TXnombreEncuestado.Text = "";
            TXnombreEncuestado.Enabled = true;
            TXcargo.Text = "";
            TXcargo.Enabled = true;
            TXfuncionarios.Text = "";
            TXfuncionarios.Enabled = true;
            RBtipoEncuesta.ClearSelection();
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
            DDLencuestas.Enabled = true;
            TXobservaciones.Text = "";

            DVgridEncuestas.Visible = false;
            GVrespuestas.Visible = false;
            DVprint.Visible = false;
            Dbutton.Visible = false;
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
            BodyGridGES.Visible = false;
            BodyFormGES.Visible = true;
            string strErrMsg = string.Empty;
            if (mtdLoadDDLEncuestas(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            GVencuestas.Visible = true;
        }
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLEncuestas(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsEncuestas> lstEncuestas = new List<clsEncuestas>();
            clsEncuestasBLL cEncuesta = new clsEncuestasBLL();
            #endregion Vars

            try
            {
                lstEncuestas = cEncuesta.mtdConsultarEncuesta(ref lstEncuestas, ref strErrMsg);
                DDLencuestas.Items.Clear();
                DDLencuestas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccionar--", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstEncuestas != null)
                    {
                        int intCounter = 1;

                        foreach (clsEncuestas objCadenaValor in lstEncuestas)
                        {
                            DDLencuestas.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objCadenaValor.strNombreEncuesta, objCadenaValor.intIdEncuesta.ToString()));
                            intCounter++;
                        }
                        booResult = false;

                    }
                    else
                    {
                        booResult = true;
                        strErrMsg = string.Format("No hay encuestas para cargar");
                    }
                }
                else
                {
                    booResult = true;
                    strErrMsg = string.Format("No hay encuestas para cargar");
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Encuestas. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        protected void DDLencuestas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            if (!mtdLoadEncuestas(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            DVgridEncuestas.Visible = true;
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
            DVprint.Visible = true;
        }
        #region LoadGridEncuesta
        private bool mtdLoadEncuestas(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEncuestas objEncuesta = new clsEncuestas();
            List<clsValorEvaluacionServicio> lstEncuesta = new List<clsValorEvaluacionServicio>();
            clsValorEvaluacionServicio encuesta = new clsValorEvaluacionServicio();
            clsValorEvaluacionServicioBLL cEncuesta = new clsValorEvaluacionServicioBLL();
            int IdEncuesta = Convert.ToInt32(DDLencuestas.SelectedValue);
            #endregion Vars
            lstEncuesta = cEncuesta.mtdConsultarEncuesta(ref encuesta, ref strErrMsg, ref IdEncuesta);

            if (lstEncuesta != null)
            {
                mtdLoadEncuestas();
                mtdLoadEncuestas(lstEncuesta);
                GVencuestas.DataSource = lstEncuesta;
                GVencuestas.PageIndex = pagIndex1;
                GVencuestas.DataBind();

                GVprint.DataSource = lstEncuesta;
                GVprint.DataBind();
                booResult = true;
            }
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadEncuestas()
        {
            DataTable gridEnc = new DataTable();

            gridEnc.Columns.Add("strNombreEncuesta", typeof(string));
            gridEnc.Columns.Add("intCantidadPregunta", typeof(string));
            gridEnc.Columns.Add("strTextoPregunta", typeof(string));
            gridEnc.Columns.Add("intIdPregunta", typeof(string));

            GVencuestas.DataSource = gridEnc;
            GVencuestas.DataBind();
            InfoGrid1 = gridEnc;

            DataTable gridPrint = new DataTable();
            gridPrint.Columns.Add("strNombreEncuesta", typeof(string));
            gridPrint.Columns.Add("strTextoPregunta", typeof(string));
            GVprint.DataSource = gridPrint;
            GVprint.DataBind();
            InfoGridPrint = gridPrint;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadEncuestas(List<clsValorEvaluacionServicio> lstEncuestas)
        {
            string strErrMsg = String.Empty;
            string Descripcion = String.Empty;
            foreach (clsValorEvaluacionServicio objEvaComp in lstEncuestas)
            {

                InfoGrid1.Rows.Add(new Object[] {
                    objEvaComp.strNombreEncuesta.ToString().Trim(),
                    objEvaComp.intCantidadPregunta.ToString().Trim(),
                    objEvaComp.strTextoPregunta.ToString().Trim(),
                    objEvaComp.intIdPregunta.ToString().Trim()
                    });
                //int cantQuestion = Convert.ToInt32(objEvaComp.intCantidadPregunta.ToString());
                Descripcion = objEvaComp.strDescripcionEmpresa.ToString();

                InfoGridPrint.Rows.Add(new Object[] {
                    objEvaComp.strNombreEncuesta.ToString().Trim(),
                    objEvaComp.strTextoPregunta.ToString().Trim()
                    });
            }
            LdescripcionEmpresa.Text = Descripcion;
        }
        #endregion
        #region LoadGridEncuestaRespuestas
        private bool mtdConsultarEvaluacionRespuestas(ref string strErrMsg, ref int IdEvaServicio)
        {
            #region Vars
            bool booResult = false;
            clsValorRespuesta objEncuesta = new clsValorRespuesta();
            List<clsValorRespuesta> lstEncuesta = new List<clsValorRespuesta>();
            clsValorRespuesta respuesta = new clsValorRespuesta();
            clsValorEvaluacionServicioBLL cEncuesta = new clsValorEvaluacionServicioBLL();
            //int IdEncuesta = Convert.ToInt32(DDLencuestas.SelectedValue);
            #endregion Vars
            lstEncuesta = cEncuesta.mtdConsultarEvaluacionRespuestas(ref respuesta, ref strErrMsg, ref IdEvaServicio);

            if (lstEncuesta != null)
            {
                mtdConsultarEvaluacionRespuestas();
                mtdConsultarEvaluacionRespuestas(lstEncuesta);
                GVrespuestas.DataSource = lstEncuesta;
                GVrespuestas.PageIndex = pagIndex2;
                GVrespuestas.DataBind();
                GVrespuestas.Visible = true;
                booResult = true;
            }
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdConsultarEvaluacionRespuestas()
        {
            DataTable gridRespuesta = new DataTable();

            gridRespuesta.Columns.Add("strPregunta", typeof(string));
            gridRespuesta.Columns.Add("strRespuesta", typeof(string));

            GVrespuestas.DataSource = gridRespuesta;
            GVrespuestas.DataBind();
            InfoGrid2 = gridRespuesta;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdConsultarEvaluacionRespuestas(List<clsValorRespuesta> lstEncuestas)
        {
            string strErrMsg = String.Empty;
            string Descripcion = String.Empty;
            foreach (clsValorRespuesta objEvaComp in lstEncuestas)
            {

                InfoGrid2.Rows.Add(new Object[] {
                    objEvaComp.strPregunta.ToString().Trim(),
                    objEvaComp.strRespuesta.ToString().Trim()
                    });
                //int cantQuestion = Convert.ToInt32(objEvaComp.intCantidadPregunta.ToString());
               
            }
            
        }
        private bool mtdConsultarEvaluacionObservacion(ref string strErrMsg, ref int IdEvaServicio)
        {
            #region Vars
            bool booResult = false;
            clsObservacionServicio objEncuesta = new clsObservacionServicio();
            List<clsObservacionServicio> lstEncuesta = new List<clsObservacionServicio>();
            clsObservacionServicio respuesta = new clsObservacionServicio();
            clsValorEvaluacionServicioBLL cEncuesta = new clsValorEvaluacionServicioBLL();
            //int IdEncuesta = Convert.ToInt32(DDLencuestas.SelectedValue);
            #endregion Vars
            lstEncuesta = cEncuesta.mtdConsultarEvaluacionObservacion(ref respuesta, ref strErrMsg, ref IdEvaServicio);
            string observacion = string.Empty;
            if (lstEncuesta != null)
            {
                foreach (clsObservacionServicio objEvaComp in lstEncuesta)
                {

                    /*InfoGrid3.Rows.Add(new Object[] {
                    objEvaComp.strObservacion.ToString().Trim()
                    });*/
                    observacion = objEvaComp.strObservacion.ToString().Trim();

                }
                TXobservaciones.Text = observacion;
                booResult = true;
            }
            return booResult;
        }
        #endregion

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdRestFields();
            mtdStard();

        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdActualizarValorEvaluacionSerivicio(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                mtdStard();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdInsertarValorEvaluacionSerivicio(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                mtdStard();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }
        /// <summary>
        /// Realiza la insercion de la evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        protected bool mtdInsertarValorEvaluacionSerivicio(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionServicio objEvaServicioInd = new clsEvaluacionServicio();
            clsObservacionServicio objObservacion = new clsObservacionServicio();
            clsValorEvaluacionServicioBLL cValorEvaluacionInd = new clsValorEvaluacionServicioBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objEvaServicioInd.strCiudad = TXciudad.Text;
            objEvaServicioInd.dtFecha = TXfechaEva.Text;
            string tipoEncuesta = RBtipoEncuesta.SelectedValue.ToString();
            if (tipoEncuesta.Contains("Presencial"))
                objEvaServicioInd.intIdTipoEncuesta = 1;
            else
                objEvaServicioInd.intIdTipoEncuesta = 2;
            objEvaServicioInd.strNombreCliente = TXnombreCliente.Text;
            objEvaServicioInd.strNombre = TXnombreEncuestado.Text;
            objEvaServicioInd.strCargo = TXcargo.Text;
            string[] textos = TXfuncionarios.Text.Split('\n');
            string funcionarios = string.Empty;
            if (textos.Length > 0)
            {
                for (int i = 0; i < textos.Length; i++)
                {
                    if (textos[i].ToString() != "")
                        funcionarios += "|" + (i + 1) + "." + textos[i].ToString();
                }
            }
            objEvaServicioInd.strFuncionarios = funcionarios;
            objEvaServicioInd.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objEvaServicioInd.dtFechaRegistro = DateTime.Now;

            booResult = mtdValidaRespuesta(GVencuestas, ref strErrMsg);
            if (booResult == true)
            {
                booResult = cValorEvaluacionInd.mtdInsertarValorEvaluacionServicio(objEvaServicioInd, ref strErrMsg);
                //booResult = true;
                int IdEvaluacionServicio = cValorEvaluacionInd.mtdLastIdEvaluacionServicio(ref strErrMsg);
                booResult = mtdInsertarResultados(GVencuestas, IdEvaluacionServicio, ref cValorEvaluacionInd, ref strErrMsg);

                objObservacion.intIdEvaServicio = IdEvaluacionServicio;
                objObservacion.strObservacion = TXobservaciones.Text;
                objObservacion.dtfecha = DateTime.Now;
                objObservacion.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());

                booResult = cValorEvaluacionInd.mtdInsertarObservacion(objObservacion, ref strErrMsg);
                strErrMsg = "Evaluación de servicio registrada exitosamente";
                if(booResult != true)
                    strErrMsg = "Error al registrar la evaluación servicio";
            }
            return booResult;
        }
        protected bool mtdInsertarResultados(GridView Grid, int IdEvaServicio, ref clsValorEvaluacionServicioBLL cValorEvaluacionInd, ref string strErrMsg)
        {
            bool booResult = false;
            clsValorRespuesta cRespuesta = new clsValorRespuesta();
            //clsValorEvaluacionServicioBLL cValorServicio = new clsValorEvaluacionServicioBLL();
            

            foreach (GridViewRow GridViewRow in Grid.Rows)
            {

                var colsNoVisible = GVencuestas.DataKeys[GridViewRow.RowIndex].Values;
                int IdEncuesta = Convert.ToInt32(DDLencuestas.SelectedValue);
                //cRespuesta.intIdEncuestaQ = IdEncuesta;
                cRespuesta.intIdEvaluacionServicio = IdEvaServicio;
                cRespuesta.intIdPregunta = Convert.ToInt32(colsNoVisible[0].ToString());
                if (((TextBox)GridViewRow.FindControl("TXcalificacion")).Text != "NC")
                    cRespuesta.strRespuesta = ((TextBox)GridViewRow.FindControl("TXcalificacion")).Text;
                else
                    cRespuesta.strRespuesta = "0";
                //cRespuesta.strRespuesta = ((TextBox)GridViewRow.FindControl("TXcalificacion")).Text;
                booResult = cValorEvaluacionInd.mtdInsertarValorRespuesta(cRespuesta, ref strErrMsg, ref IdEncuesta);

            }

            return booResult;
        }
        protected bool mtdValidaRespuesta(GridView Grid, ref string strErrMsg)
        {
            bool booResult = false;

            foreach (GridViewRow GridViewRow in Grid.Rows)
            {
                string respuesta = ((TextBox)GridViewRow.FindControl("TXcalificacion")).Text;
                if (respuesta.Equals("1") || respuesta.Equals("2") || respuesta.Equals("3") || respuesta.Equals("4") || respuesta.Equals("5") || respuesta.Equals("NC"))
                {
                    booResult = true;
                }
                else
                {
                    strErrMsg = "Respuesta no acorde, por favor califique cada pregunta en la escala de 1 a 5, siendo 1 deficiente y 5 Excelente. NC:  No contesta.";
                    break;
                }
            }
            return booResult;
        }
        /// <summary>
        /// Realiza la insercion de la evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        protected bool mtdActualizarValorEvaluacionSerivicio(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionServicio objEvaServicioInd = new clsEvaluacionServicio();
            clsObservacionServicio objObservacion = new clsObservacionServicio();
            clsValorEvaluacionServicioBLL cValorEvaluacionInd = new clsValorEvaluacionServicioBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            int IdEvaServicio = Convert.ToInt32(txtId.Text);
            string observaciones = TXobservaciones.Text;

            booResult = cValorEvaluacionInd.mtdActualizarValorObservacion(ref strErrMsg, ref observaciones, ref IdEvaServicio);
            if (booResult == true)
            {

                strErrMsg = "Evaluación de servicio actualizadad exitosamente";

            }
            else
            {
                strErrMsg = "Error al actualizar la evaluación servicio";
            }
            return booResult;
        }
        #region LoadGrid
        private bool mtdLoadEvaluacionServicio(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsEvaluacionServicio objServicio = new clsEvaluacionServicio();
            List<clsEvaluacionServicio> lstEvaServicio = new List<clsEvaluacionServicio>();
            clsValorEvaluacionServicioBLL cServicio = new clsValorEvaluacionServicioBLL();
            #endregion Vars
            lstEvaServicio = cServicio.mtdConsultarEvaluacionServicio(ref objServicio, ref strErrMsg);

            if (lstEvaServicio != null)
            {
                mtdLoadEvaluacionServicio();
                mtdLoadEvaluacionServicio(lstEvaServicio);
                GVevaluacionServicio.DataSource = lstEvaServicio;
                GVevaluacionServicio.PageIndex = pagIndex2;
                GVevaluacionServicio.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay datos para cargar";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadEvaluacionServicio()
        {
            DataTable gridEva = new DataTable();

            gridEva.Columns.Add("intId", typeof(string));
            gridEva.Columns.Add("strCiudad", typeof(string));
            gridEva.Columns.Add("dtFecha", typeof(string));
            gridEva.Columns.Add("strTipoEncuesta", typeof(string));
            gridEva.Columns.Add("strNombreCliente", typeof(string));
            gridEva.Columns.Add("strNombre", typeof(string));
            gridEva.Columns.Add("strCargo", typeof(string));
            gridEva.Columns.Add("strFuncionarios", typeof(string));
            gridEva.Columns.Add("dtFechaRegistro", typeof(string));
            gridEva.Columns.Add("intIdUsuario", typeof(string));
            gridEva.Columns.Add("strUsuario", typeof(string));            

            GVevaluacionServicio.DataSource = gridEva;
            GVevaluacionServicio.DataBind();
            InfoGrid2 = gridEva;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadEvaluacionServicio(List<clsEvaluacionServicio> lstProveedor)
        {
            string strErrMsg = String.Empty;
            //clsValorEvaluacionProveedorBLL cCrlInfra = new clsValorEvaluacionProveedorBLL();

            foreach (clsEvaluacionServicio objEvaComp in lstProveedor)
            {

                InfoGrid2.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strCiudad.ToString().Trim(),
                    objEvaComp.dtFecha.ToString().Trim(),
                    objEvaComp.strTipoEncuesta.ToString().Trim(),
                    objEvaComp.strNombreCliente.ToString().Trim(),
                    objEvaComp.strNombre.ToString().Trim(),
                    objEvaComp.strCargo.ToString().Trim(),
                    objEvaComp.strFuncionarios.ToString().Trim(),
                    objEvaComp.dtFechaRegistro.ToString().Trim(),
                    objEvaComp.intIdUsuario.ToString().Trim(),
                    objEvaComp.strUsuario.ToString().Trim()
                    });
            }
        }
        #endregion

        protected void GVevaluacionServicio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid1 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid1);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    break;
            }
        }
        private void mtdShowUpdate(int RowGrid1)
        {
            GridViewRow row = GVevaluacionServicio.Rows[RowGrid1];
            var colsNoVisible = GVevaluacionServicio.DataKeys[RowGrid1].Values;
            int IdEvaServicio = Convert.ToInt32(row.Cells[0].Text);
            txtId.Text = row.Cells[0].Text;
            TXciudad.Text = ((Label)row.FindControl("ciudad")).Text;
            TXciudad.Enabled = false;
            TXfechaEva.Text = row.Cells[2].Text;
            TXfechaEva.Enabled = false;
            string tipoEncuesta = ((Label)row.FindControl("tipoEncuesta")).Text;
            string strErrMsg = string.Empty;
            clsValorEvaluacionServicioBLL cValorEvaluacionInd = new clsValorEvaluacionServicioBLL();
            if (mtdLoadDDLEncuestas(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            DDLencuestas.Enabled = false;

            int IdEncuesta = cValorEvaluacionInd.mtdIdEncuesta(ref strErrMsg, ref IdEvaServicio);
            if (IdEncuesta > 0)
                DDLencuestas.SelectedIndex = IdEncuesta;
            if(tipoEncuesta == "Presencial")
            {
                RBtipoEncuesta.SelectedIndex = 0;
            }else
            {
                RBtipoEncuesta.SelectedIndex = 1;
            }
            //RBtipoEncuesta.SelectedValue = tipoEncuesta;
            TXnombreCliente.Text = ((Label)row.FindControl("nombreCliente")).Text;
            TXnombreCliente.Enabled = false;
            TXnombreEncuestado.Text = ((Label)row.FindControl("nombre")).Text;
            TXnombreEncuestado.Enabled = false;
            TXcargo.Text = ((Label)row.FindControl("cargo")).Text;
            TXcargo.Enabled = false;
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();
            string funcionarios = ((Label)row.FindControl("funcionarios")).Text;
            if (funcionarios != "N/A")
            {
                string[] texto = funcionarios.Split('|');
                for (int i = 0; i < texto.Length; i++)
                {
                    if (texto[i].ToString() != "")
                        TXfuncionarios.Text += texto[i].ToString();// + Environment.NewLine;
                }
            }
            TXfuncionarios.Enabled = false;
            strErrMsg = string.Empty;

            
            if (!mtdConsultarEvaluacionRespuestas(ref strErrMsg, ref IdEvaServicio))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (!mtdConsultarEvaluacionObservacion(ref strErrMsg, ref IdEvaServicio))
                omb.ShowMessage(strErrMsg, 2, "Atención");

            BodyGridGES.Visible = false;
            BodyFormGES.Visible = true;
            DVgridEncuestas.Visible = true;
            GVencuestas.Visible = false;
            Dbutton.Visible = true;
        }

        

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            string tipoEncuesta = RBtipoEncuesta.SelectedValue.ToString();
            if(tipoEncuesta.Equals("Presencial"))
                mtdExportPdf();
            else
                omb.ShowMessage("Solo presencial se puede exportar", 2, "Atención");
        }
        private void mtdExportPdf()
        {
            #region
            Tools tools = new Tools();
            PdfPTable pdfpTableFactorDesempeño = tools.createPdftable(GVprint);

            foreach (GridViewRow GridViewRow in GVprint.Rows)
            {
                string nombre = ((Label)GridViewRow.FindControl("NombreEncuesta")).Text;
                string puntaje = ((Label)GridViewRow.FindControl("TextoPregunta")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVprint.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(nombre));
                        pdfCell.BackgroundColor = new Color(GVprint.RowStyle.BackColor);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVprint.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(puntaje));
                        pdfCell.BackgroundColor = new Color(GVprint.RowStyle.BackColor);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVencuestas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(""));
                        pdfCell.BackgroundColor = new Color(GVprint.RowStyle.BackColor);
                        pdfpTableFactorDesempeño.AddCell(pdfCell);
                    }
                    iteracion++;
                }
            }
            #endregion

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4, 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Encuesta de Satisfacción");
            //....header
            // we Add a Header that will show up on PAGE 1
            // Creamos la imagen y le ajustamos el tamaño
            string pathImg = Server.MapPath("~") + "Imagenes/Logos/Risk.png";
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(pathImg);
            pathImg = Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
            iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(pathImg);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            PdfPTable pdftblImage = new PdfPTable(2);
            PdfPCell pdfcellImage = new PdfPCell(imagen, true);
            pdfcellImage.FixedHeight = 40f;
            /*pdfcellImage.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcellImage.VerticalAlignment = Element.ALIGN_LEFT;*/
            pdfcellImage.Border = Rectangle.NO_BORDER;
            pdfcellImage.Border = Rectangle.NO_BORDER;
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();

            phHeader.Add(pdftblImage);
            //phHeader.Add(chnCompany);
            #region Tabla de Datos Principales
            Font font1 = new Font();
            font1.Color = new Color(GVencuestas.HeaderStyle.ForeColor);
            Color BackColor = new Color(GVencuestas.HeaderStyle.BackColor);
            PdfPTable pdfTableData = new PdfPTable(2);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Ciudad:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVencuestas.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXciudad.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha:", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(""));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Tipo de Encuesta", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Presencial"));
            pdfTableData.AddCell(pdfCellEncabezado);
            
            #endregion

            PdfPTable pdfTableCliente = new PdfPTable(2);
            PdfPCell pdfCellCliente = new PdfPCell(new Phrase("Nombre del Cliente", font1));
            pdfCellCliente.BackgroundColor = BackColor;
            pdfTableCliente.AddCell(pdfCellCliente);
            pdfCellCliente = new PdfPCell(new Phrase("", font1));
            pdfTableCliente.AddCell(pdfCellCliente);
            pdfCellCliente = new PdfPCell(new Phrase("Nombre del Encuestado", font1));
            pdfCellCliente.BackgroundColor = BackColor;
            pdfTableCliente.AddCell(pdfCellCliente);
            pdfCellCliente = new PdfPCell(new Phrase("", font1));
            pdfTableCliente.AddCell(pdfCellCliente);
            pdfCellCliente = new PdfPCell(new Phrase("Cargo del Encuestado", font1));
            pdfCellCliente.BackgroundColor = BackColor;
            pdfTableCliente.AddCell(pdfCellCliente);
            pdfCellCliente = new PdfPCell(new Phrase("", font1));
            pdfTableCliente.AddCell(pdfCellCliente);

            PdfPTable pdfTableFuncionarios = new PdfPTable(1);
            PdfPCell pdfCellFuncionarios = new PdfPCell(new Phrase("Funcionarios:"));
            pdfTableFuncionarios.AddCell(pdfCellFuncionarios);
            pdfCellFuncionarios = new PdfPCell(new Phrase("1."));
            pdfTableFuncionarios.AddCell(pdfCellFuncionarios);
            pdfCellFuncionarios = new PdfPCell(new Phrase("2."));
            pdfTableFuncionarios.AddCell(pdfCellFuncionarios);
            pdfCellFuncionarios = new PdfPCell(new Phrase("3."));
            pdfTableFuncionarios.AddCell(pdfCellFuncionarios);
            pdfCellFuncionarios = new PdfPCell(new Phrase("4."));
            pdfTableFuncionarios.AddCell(pdfCellFuncionarios);

            PdfPTable pdfTableTexto = new PdfPTable(1);
            PdfPCell pdfCellTexto = new PdfPCell(new Phrase(""+LdescripcionEmpresa.Text));
            pdfTableTexto.AddCell(pdfCellTexto);

            PdfPTable pdfTableParrafo = new PdfPTable(1);
            PdfPCell pdfCellParrafo = new PdfPCell(new Phrase("" + TextoQuestion.Text));
            pdfCellParrafo.BackgroundColor = BackColor;
            pdfTableParrafo.AddCell(pdfCellParrafo);

            PdfPTable pdfTableObservaciones = new PdfPTable(1);
            PdfPCell pdfCellObservaciones = new PdfPCell(new Phrase("Observaciones y Oportunidades de Mejora:"));
            pdfTableObservaciones.AddCell(pdfCellObservaciones);
            pdfCellObservaciones = new PdfPCell(new Phrase("1."));
            pdfTableObservaciones.AddCell(pdfCellObservaciones);
            pdfCellObservaciones = new PdfPCell(new Phrase("2."));
            pdfTableObservaciones.AddCell(pdfCellObservaciones);
            pdfCellObservaciones = new PdfPCell(new Phrase("3."));
            pdfTableObservaciones.AddCell(pdfCellObservaciones);
            pdfCellObservaciones = new PdfPCell(new Phrase("4."));
            pdfTableObservaciones.AddCell(pdfCellObservaciones);

            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Encuesta de Satisfacción"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(pdfTableCliente);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableTexto);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableFuncionarios);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableParrafo);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableFactorDesempeño);
            pdfDocument.Add(pdfTableObservaciones);
            /*pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableDataRecom);*/
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=EncuestaSatisfaccion.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        public DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();
            if (dtg.HeaderRow != null)
            {
                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.RowHeaderColumn[i].ToString());
                }
            }

            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void ImButtonPDFEvaExport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdfEva();
        }
        private void mtdExportPdfEva()
        {
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4, 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de Evaluación de Servicio");
            //....header
            // we Add a Header that will show up on PAGE 1
            // Creamos la imagen y le ajustamos el tamaño
            string pathImg = Server.MapPath("~") + "Imagenes/Logos/Risk.png";
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(pathImg);
            pathImg = Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
            iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(pathImg);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            PdfPTable pdftblImage = new PdfPTable(2);
            PdfPCell pdfcellImage = new PdfPCell(imagen, true);
            pdfcellImage.FixedHeight = 40f;
            /*pdfcellImage.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcellImage.VerticalAlignment = Element.ALIGN_LEFT;*/
            pdfcellImage.Border = Rectangle.NO_BORDER;
            pdfcellImage.Border = Rectangle.NO_BORDER;
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();

            phHeader.Add(pdftblImage);
            //phHeader.Add(chnCompany);
            #region Tabla de Datos Principales
            Font font1 = new Font();
            font1.Color = new Color(GVevaluacionServicio.HeaderStyle.ForeColor);
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Código:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtId.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Ciudad:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXciudad.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Evaluación:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaEva.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            pdfCellEncabezado = new PdfPCell(new Phrase("Tipo de Encuesta:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(RBtipoEncuesta.SelectedValue.ToString()));
            pdfTableData.AddCell(pdfCellEncabezado);

            pdfCellEncabezado = new PdfPCell(new Phrase("Nombre del Cliente:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXnombreCliente.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            pdfCellEncabezado = new PdfPCell(new Phrase("Nombre Encuestado:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXnombreEncuestado.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            pdfCellEncabezado = new PdfPCell(new Phrase("Cargo del Encuestado:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXcargo.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            pdfCellEncabezado = new PdfPCell(new Phrase("Funcionarios:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfuncionarios.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            pdfCellEncabezado = new PdfPCell(new Phrase("Usuario Creación:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxUsuarioCreacion.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha de Creación:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtFecha.Text));
            pdfTableData.AddCell(pdfCellEncabezado);


            PdfPTable pdfTableEncuesta = new PdfPTable(2);
            pdfCellEncabezado = new PdfPCell(new Phrase("Encuesta:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableEncuesta.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(DDLencuestas.SelectedItem.Text));
            pdfTableEncuesta.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Información Valores Preguntas:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableEncuesta.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TextoQuestion.Text));
            pdfTableEncuesta.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Descripción Empresa:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableEncuesta.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(LdescripcionEmpresa.Text));
            pdfTableEncuesta.AddCell(pdfCellEncabezado);



            #endregion
            #region Totales
            PdfPTable pdfpTableTotales = new PdfPTable(GVrespuestas.HeaderRow.Cells.Count);

            foreach (TableCell headerCell in GVrespuestas.HeaderRow.Cells)
            {
                Font font = new Font();
                font.Color = new Color(GVrespuestas.HeaderStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                pdfCell.BackgroundColor = new Color(GVrespuestas.HeaderStyle.BackColor);
                pdfpTableTotales.AddCell(pdfCell);
            }

            foreach (GridViewRow GridViewRow in GVrespuestas.Rows)
            {
                string pregunta = ((Label)GridViewRow.FindControl("pregunta")).Text;
                string respuesta = ((Label)GridViewRow.FindControl("respuesta")).Text;
                
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    /*if (iteracion != 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }
                    else
                    {
                        Font font = new Font();
                        font.Color = new Color(GVtotal.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(nombre));
                        pdfCell.BackgroundColor = new Color(GVtotal.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }*/
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVrespuestas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(pregunta));
                        pdfCell.BackgroundColor = new Color(GVrespuestas.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVrespuestas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(respuesta));
                        pdfCell.BackgroundColor = new Color(GVrespuestas.RowStyle.BackColor);
                        pdfpTableTotales.AddCell(pdfCell);
                    }

                    iteracion++;
                }

            }
            #endregion
            #region TablaObservaciones
            
            PdfPTable pdfTableObservaciones = new PdfPTable(2);
            pdfCellEncabezado = new PdfPCell(new Phrase("Observaciones y Oportunidades de Mejora:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVevaluacionServicio.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXobservaciones.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);

            #endregion TablaObservaciones
            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Evaluación de Servicio"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableEncuesta);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableTotales);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableObservaciones);
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteEvaluacionServicio.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteEvaluacionServicios_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {
            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Código:");
            gridEncabezado.Columns.Add("Ciudad:");
            gridEncabezado.Columns.Add("Fecha Evaluación:");
            gridEncabezado.Columns.Add("Tipo Encuenta:");
            gridEncabezado.Columns.Add("Nombre del Cliente:");
            gridEncabezado.Columns.Add("Nombre Encuestado:");
            gridEncabezado.Columns.Add("Cargo del Encuestado:");
            gridEncabezado.Columns.Add("Funcionarios:");
            gridEncabezado.Columns.Add("Usuario Creación:");
            gridEncabezado.Columns.Add("Fecha de Creación:");
            gridEncabezado.Columns.Add("Encuesta:");
            gridEncabezado.Columns.Add("Información Valores Preguntas:");
            gridEncabezado.Columns.Add("Descripción Empresa:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Código:"] = txtId.Text;
            rowEncabezado["Ciudad:"] = TXciudad.Text;
            rowEncabezado["Fecha Evaluación:"] = TXfechaEva.Text;
            rowEncabezado["Tipo Encuenta:"] = RBtipoEncuesta.SelectedItem.Text;
            rowEncabezado["Nombre del Cliente:"] = TXnombreCliente.Text;
            rowEncabezado["Nombre Encuestado:"] = TXnombreEncuestado.Text;
            rowEncabezado["Cargo del Encuestado:"] = TXcargo.Text;
            rowEncabezado["Funcionarios:"] = TXfuncionarios.Text;
            rowEncabezado["Usuario Creación:"] = tbxUsuarioCreacion.Text;
            rowEncabezado["Fecha de Creación:"] = txtFecha.Text;
            rowEncabezado["Encuesta:"] = DDLencuestas.SelectedItem.Text;
            rowEncabezado["Información Valores Preguntas:"] = TextoQuestion.Text;
            rowEncabezado["Descripción Empresa:"] = LdescripcionEmpresa.Text;
            gridEncabezado.Rows.Add(rowEncabezado);

            DataTable gridEvaValue = new DataTable();
            gridEvaValue.Columns.Add("Pregunta:");
            gridEvaValue.Columns.Add("Calificación:");
            
            DataRow rowEvaValue;
            foreach (GridViewRow GridViewRow in GVrespuestas.Rows)
            {
                rowEvaValue = gridEvaValue.NewRow();
                string pregunta = ((Label)GridViewRow.FindControl("pregunta")).Text;
                string respuesta = ((Label)GridViewRow.FindControl("respuesta")).Text;
                
                rowEvaValue["Pregunta:"] = pregunta;
                rowEvaValue["Calificación:"] = respuesta;
                
                gridEvaValue.Rows.Add(rowEvaValue);
            }

            
            DataTable gridData = new DataTable();
            gridData.Columns.Add("Observaciones y Oportunidades de Mejora:");
            

            DataRow rowData;
            rowData = gridData.NewRow();
            rowData["Observaciones y Oportunidades de Mejora:"] = TXobservaciones.Text;
            
            gridData.Rows.Add(rowData);
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "Encabezado");
            workbook.Worksheets.Add(gridEvaValue, "Encuesta");
            workbook.Worksheets.Add(gridData, "Observaciones");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        protected void GVevaluacionServicio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex2 = e.NewPageIndex;
            /*GVevaluacionDesempeño.PageIndex = PagIndex1;
            GVevaluacionDesempeño.DataBind();*/
            string strErrMsg = "";
            mtdLoadEvaluacionServicio(ref strErrMsg);
        }
    }
}