using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
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
namespace ListasSarlaft.Classes.Utilerias
{
    public class Tools
    {
        public void exportExcel(System.Data.DataSet ds, string nameWorkSheet,HttpResponse Response, string filename)
        {
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(ds);
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
        public PdfPTable createPdftable(GridView grid)
        {
            PdfPTable pdfpTable = new PdfPTable(grid.HeaderRow.Cells.Count);

            foreach (TableCell headerCell in grid.HeaderRow.Cells)
            {
                Font titleFont = new Font(Font.HELVETICA, 10, Font.NORMAL, Color.WHITE);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, titleFont));
                pdfCell.BackgroundColor = new Color(grid.HeaderStyle.BackColor);
                pdfpTable.AddCell(pdfCell);
            }
            return pdfpTable;
        }

        public PdfPCell createPdfPCell(GridView grid, string dato)
        {
            Font font = new Font();
            font.Color = new Color(grid.RowStyle.ForeColor);
            PdfPCell pdfCell = new PdfPCell(new Phrase(dato));
            pdfCell.BackgroundColor = new Color(grid.RowStyle.BackColor);

            return pdfCell;
        }
        public PdfPCell createPdfPCellgrid(string Text, Font font, GridView grid)
        {
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase(Text, font));
            pdfCellEncabezado.BackgroundColor = new Color(grid.HeaderStyle.BackColor);

            return pdfCellEncabezado;
        }
        public PdfPTable createImageHeaders()
        {
            // Creamos la imagen y le ajustamos el tamaño
            string pathImg = HttpContext.Current.Server.MapPath("~") + "Imagenes/Logos/Risk.png";
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(pathImg);
            pathImg = HttpContext.Current.Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
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

            return pdftblImage;
        }
        public Font setFont(GridView grid)
        {
            Font font1 = new Font();
            font1.Color = new Color(grid.HeaderStyle.ForeColor);

            return font1;
        }
        public PdfPTable createPDFdataContent(string[] Encabezados, string[] datos, int colums, GridView grid)
        {
            Font font = setFont(grid);
            PdfPTable pdfTableData = new PdfPTable(colums);
            PdfPCell pdfCellEncabezado = new PdfPCell();
            for (int i = 0; i < Encabezados.Length; i++)
            {
                pdfCellEncabezado = createPdfPCellgrid(Encabezados[i].ToString(),font, grid);
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(datos[i].ToString()));
                pdfTableData.AddCell(pdfCellEncabezado);
            }

                return pdfTableData;
        }
        public Document createpdfDocument(string NombrePDF, HttpResponse Response)
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
            pdfDocument.AddTitle(NombrePDF);
            //....header
            // we Add a Header that will show up on PAGE 1

            return pdfDocument;
        }
        public void SaveDocument(HttpResponse Response, Document pdfDocument, string nombre)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + nombre + ".pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
    }
}