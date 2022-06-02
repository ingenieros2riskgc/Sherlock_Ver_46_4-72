using clsDTO;
using clsLogica;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class ReporteDetalleCalificacion : System.Web.UI.UserControl
    {
        string IdFormulario = "11004";
        private clsPerfil cPerfil = new clsPerfil();
        clsCuenta cCuenta = new clsCuenta();
        Classes.cCuenta ccCuenta = new Classes.cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            scriptManager.RegisterPostBackControl(ibtnDescargarArchivo);
            scriptManager.RegisterPostBackControl(btnConsultar);

            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                CargarDropdownlistArchivo();
            }
        }

        public void CargarDropdownlistArchivo()
        {
            try
            {
                List<ArchivoSegmentacion> lst = cPerfil.ConsultarArchivos();
                ddlArchivo.DataSource = lst;
                ddlArchivo.DataTextField = "UrlArchivo";
                ddlArchivo.DataValueField = "IdArchivo";
                ddlArchivo.DataBind();
                ddlArchivo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los archivos. {ex.Message}", 1, "Atencion");
            }
        }

        protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                List<SalidaDetalleCalificacion> lst = cPerfil.ConsultarDetalle(txtNumeroIdentificacion.Text, Convert.ToInt32(ddlArchivo.SelectedValue));
                gvDetalleCalificacion.DataSource = lst;
                gvDetalleCalificacion.DataBind();
                trRptPerfiles.Visible = true;
                // Se calcula la calificación total
                double totalCalificación = 0, valorActual = 0;
                foreach (GridViewRow Row in gvDetalleCalificacion.Rows)
                {
                    valorActual = Row.Cells[6].Equals(DBNull.Value) ? 0 : Convert.ToDouble(Row.Cells[6].Text);
                    totalCalificación = totalCalificación + valorActual;
                }
                lblCalificacion.Text = totalCalificación.ToString();
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al consultar. {ex.Message}", 1, "Atencion");
            }
        }

        protected void ibtnDescargarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Creamos el tipo de Font que vamos utilizar
                Font titleFont = new Font(Font.HELVETICA, 10, Font.NORMAL, Color.WHITE);
                Font textFont = new Font(Font.HELVETICA, 8, Font.NORMAL, Color.BLACK);
                List<PdfPRow> pRows = new List<PdfPRow>();

                // Crea la informacion de documentos en el pdf
                PdfPTable pdfTable = new PdfPTable(gvDetalleCalificacion.HeaderRow.Cells.Count);

                // Crea el encabezado de la tabla
                foreach (TableCell headerCell in gvDetalleCalificacion.HeaderRow.Cells)
                {
                    Font font = new Font
                    {
                        Color = new Color(gvDetalleCalificacion.HeaderStyle.ForeColor)
                    };
                    PdfPCell pdfCell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(headerCell.Text), titleFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    pdfCell.BackgroundColor = new Color(gvDetalleCalificacion.HeaderStyle.BackColor);
                    pdfTable.AddCell(pdfCell);
                }

                // Crea las filas de la tabla
                foreach (GridViewRow Row in gvDetalleCalificacion.Rows)
                {
                    List<PdfPCell> rowdocumentos = new List<PdfPCell>();
                    for (int i = 0; i < gvDetalleCalificacion.HeaderRow.Cells.Count; i++)
                    {
                        rowdocumentos.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(Row.Cells[i].Text), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    }
                    pRows.Add(new PdfPRow(rowdocumentos.ToArray()));
                }
                pdfTable.Rows.AddRange(pRows);

                // Se calcula la calificación total
                double totalCalificación = 0, valorActual = 0;
                foreach (GridViewRow Row in gvDetalleCalificacion.Rows)
                {
                    valorActual = Row.Cells[6].Equals(DBNull.Value) ? 0 : Convert.ToDouble(Row.Cells[6].Text);
                    totalCalificación = totalCalificación + valorActual;
                }
                Document pdfDocument = new Document(PageSize.LETTER.Rotate(), 1, 1, 10, 10);
                PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
                pdfDocument.AddAuthor("Sherlock");
                pdfDocument.AddCreator("Sherlock");
                pdfDocument.AddCreationDate();
                pdfDocument.AddTitle("Reporte detalle calificacion");
                string pathImg = Server.MapPath("~") + "Imagenes/Logos/logo-sherlock.png";
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(pathImg);
                pathImg = Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(pathImg);
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_RIGHT;
                PdfPTable pdftblImage = new PdfPTable(2);
                PdfPCell pdfcellImage = new PdfPCell(imagen, true);
                pdfcellImage.FixedHeight = 40f;
                pdfcellImage.Border = Rectangle.NO_BORDER;
                pdfcellImage.Border = Rectangle.NO_BORDER;
                pdftblImage.AddCell(pdfcellImage);
                PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
                pdfcellImageEmpresa.FixedHeight = 40f;
                pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
                pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
                pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
                pdftblImage.AddCell(pdfcellImageEmpresa);
                Phrase phHeader = new Phrase();
                phHeader.Add(pdftblImage);
                HeaderFooter header = new HeaderFooter(phHeader, false);
                header.Border = Rectangle.NO_BORDER;
                header.Alignment = Element.ALIGN_CENTER;
                pdfDocument.Header = header;
                pdfDocument.Open();
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(Chunk.NEWLINE);
                Paragraph Titulo = new Paragraph(new Phrase("Reporte Detalle Calificación"));
                Titulo.SetAlignment("Center");
                pdfDocument.Add(Titulo);
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(pdfTable);
                Paragraph totalCalificacion = new Paragraph($"CALIFICACIÓN TOTAL: {totalCalificación}", textFont);
                totalCalificacion.Alignment = Element.ALIGN_CENTER;
                pdfDocument.Add(new Phrase(""));
                pdfDocument.Add(totalCalificacion);
                pdfDocument.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Reporte Detalle Calificación.pdf");
                Response.Write(pdfDocument);
                Response.Flush();
                Response.End();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al generar el reporte. {ex.Message}", 1, "Atencion");
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            txtNumeroIdentificacion.Text = string.Empty;
            trRptPerfiles.Visible = false;
            ddlArchivo.ClearSelection();
        }
    }
}