using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using clsLogica;
using clsDTO;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class GVdataPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strErrMsg = "";
            mtdLoadGridHeaderCaracterizacion();
            //booResult = mtdLoadInfoGridIndicador(ref strErrMsg);
            mtdLoadInfoGridCaracterizacion(ref strErrMsg);
        }
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridDetalle;
        private int rowGridDetalle;
        private int pagIndexDetalle;
        private DataTable infoGridIndicador;
        private int rowGridIndicador;
        private int pagIndexIndicador;

        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
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
        private DataTable InfoGridDetalle
        {
            get
            {
                infoGridDetalle = (DataTable)ViewState["infoGridDetalle"];
                return infoGridDetalle;
            }
            set
            {
                infoGridDetalle = value;
                ViewState["infoGridDetalle"] = infoGridDetalle;
            }
        }

        private int RowGridDetalle
        {
            get
            {
                rowGridDetalle = (int)ViewState["rowGridDetalle"];
                return rowGridDetalle;
            }
            set
            {
                rowGridDetalle = value;
                ViewState["rowGridDetalle"] = rowGridDetalle;
            }
        }

        private int PagIndexDetalle
        {
            get
            {
                pagIndexDetalle = (int)ViewState["pagIndexDetalle"];
                return pagIndexDetalle;
            }
            set
            {
                pagIndexDetalle = value;
                ViewState["pagIndexDetalle"] = pagIndexDetalle;
            }
        }
        private DataTable InfoGridIndicador
        {
            get
            {
                infoGridIndicador = (DataTable)ViewState["infoGridIndicador"];
                return infoGridIndicador;
            }
            set
            {
                infoGridIndicador = value;
                ViewState["infoGridIndicador"] = infoGridIndicador;
            }
        }

        private int RowGridIndicador
        {
            get
            {
                rowGridIndicador = (int)ViewState["rowGridIndicador"];
                return rowGridIndicador;
            }
            set
            {
                rowGridIndicador = value;
                ViewState["rowGridIndicador"] = rowGridIndicador;
            }
        }

        private int PagIndexIndicador
        {
            get
            {
                pagIndexIndicador = (int)ViewState["pagIndexIndicador"];
                return pagIndexIndicador;
            }
            set
            {
                pagIndexIndicador = value;
                ViewState["pagIndexIndicador"] = pagIndexIndicador;
            }
        }
        #endregion
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridHeaderCaracterizacion()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strnombreProceso", typeof(string));
            grid.Columns.Add("strObjetivo", typeof(string));


            GVheader.DataSource = grid;
            GVheader.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Hace el llamado y la instancia de los campos del indicador al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridCaracterizacion(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsVerCaracterizacion> lstVerCaracterizacion = new List<clsVerCaracterizacion>();
            clsVerCaracterizacionBLL cVerCaracterizacion = new clsVerCaracterizacionBLL();
            
                clsVerCaracterizacion objVerCaracterizacionIn = new clsVerCaracterizacion(0, string.Empty, 0, Convert.ToInt32(Session["IdProceso"].ToString()),
                                    string.Empty, string.Empty);
                clsVerCaracterizacion objVerCaracterizacionOut = new clsVerCaracterizacion();
                lstVerCaracterizacion = cVerCaracterizacion.mtdConsultarVerCaracterizacion(objVerCaracterizacionIn, ref objVerCaracterizacionOut, ref strErrMsg);


            if (lstVerCaracterizacion != null)
            {
                mtdLoadInfoGridHeaderCaracterizacion(lstVerCaracterizacion);
                GVheader.DataSource = lstVerCaracterizacion;
                GVheader.PageIndex = pagIndex;
                GVheader.DataBind();
                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadInfoGridHeaderCaracterizacion(List<clsVerCaracterizacion> lstVerCaracterizacion)
        {
            foreach (clsVerCaracterizacion objVerCaracterizacion in lstVerCaracterizacion)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objVerCaracterizacion.intId.ToString().Trim(),
                    objVerCaracterizacion.strnombreProceso.ToString().Trim(),
                    objVerCaracterizacion.strObjetivo.ToString().Trim()
                    });
            }
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            PdfPTable pdfpTable = new PdfPTable(GVheader.HeaderRow.Cells.Count);

            foreach (TableCell headerCell in GVheader.HeaderRow.Cells)
                {
                    Font font = new Font();
                    font.Color = new Color(GVheader.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                    pdfCell.BackgroundColor = new Color(GVheader.HeaderStyle.BackColor);
                    pdfpTable.AddCell(pdfCell);
                }

            foreach (GridViewRow GridViewRow in GVheader.Rows)
            {
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    Font font = new Font();
                    font.Color = new Color(GVheader.RowStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                    pdfCell.BackgroundColor = new Color(GVheader.RowStyle.BackColor);
                    pdfpTable.AddCell(pdfCell);
                }
            }
            Document pdfDocument = new Document(PageSize.A4, 10f,10f,10f,10f);
            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            pdfDocument.Open();
            pdfDocument.Add(pdfpTable);
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Caracterizacion.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

    
    }
}