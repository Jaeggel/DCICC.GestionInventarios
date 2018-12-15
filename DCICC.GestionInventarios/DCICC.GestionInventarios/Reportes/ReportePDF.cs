using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DCICC.GestionInventarios.Reportes
{
    public class ReportePDF
    {
        readonly Font fuente_Datos = FontFactory.GetFont(FontFactory.HELVETICA, 8);
        readonly Font fuente_Titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, WebColors.GetRGBColor("#E7E7E7"));
        readonly BaseColor color_Datos = WebColors.GetRGBColor("#3c5a77");
        /// <summary>
        /// Método para generar el Reporte PDF.
        /// </summary>
        /// <param name="tablaReporte">Tabla para insertar en el Reporte</param>
        /// <param name="tituloReporte">Título que tendrá el Reporte</param>
        /// <returns></returns>
        public byte[] GenerarReportePDF(PdfPTable tablaReporte,string tituloReporte,string firmaUsuario)
        {
            byte[] bytesReportePDF=null;
            using (MemoryStream msReporte = new MemoryStream())
            {
                Rectangle reporteAlign;
                Document documentoReporte = new Document(reporteAlign=tituloReporte=="Tickets" || tituloReporte == "Especificaciones Adicionales" ? PageSize.A4.Rotate(): PageSize.A4);
                using (PdfWriter writerReporte = PdfWriter.GetInstance(documentoReporte, msReporte))
                {
                    documentoReporte.Open();
                    GenerarEncabezadoReporte(documentoReporte, writerReporte);
                    GenerarTituloReporte(documentoReporte, tituloReporte);
                    documentoReporte.Add(tablaReporte);
                    GenerarFirmaReporte(documentoReporte, firmaUsuario);
                    documentoReporte.Close();
                    bytesReportePDF = msReporte.ToArray();
                }
            }
            return bytesReportePDF;
        }
        /// <summary>
        /// Método para generar una tabla que será insertada en en el Reporte
        /// </summary>
        /// <param name="infoTable">DataTable con la información a ser insertada en la tabla.</param>
        /// <returns></returns>
        public PdfPTable GenerarTablaReporte(DataTable infoTable)
        {
            PdfPTable tablaReporte = new PdfPTable(infoTable.Columns.Count);
            var celdaReporte= new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };
            tablaReporte.WidthPercentage = 100;
            foreach (DataColumn c in infoTable.Columns)
            {
                celdaReporte.BackgroundColor = color_Datos;
                celdaReporte.Phrase = new Phrase(c.ColumnName, fuente_Titulo);
                tablaReporte.AddCell(celdaReporte);
            }
            int indiceColumna = 0;
            foreach (DataRow r in infoTable.Rows)
            {
                if (infoTable.Rows.Count > 0)
                {
                    while (indiceColumna < infoTable.Columns.Count)
                    {
                        tablaReporte.AddCell(new Phrase(r[indiceColumna].ToString(), fuente_Datos));
                        indiceColumna++;
                    }
                    indiceColumna = 0;
                }
            }
            return tablaReporte;
        }
        /// <summary>
        /// Método para insertar el título en el Reporte.
        /// </summary>
        /// <param name="documentoReporte">Documento actual en el cual se está generando el PDF.</param>
        /// <param name="tituloReporte">Título que será mostrado en el Reporte.</param>
        public void GenerarTituloReporte(Document documentoReporte,string tituloReporte)
        {
            using (HTMLWorker htmlWorker = new HTMLWorker(documentoReporte))
            {
                using (var sr = new StringReader(string.Format("<br/><br/><u><h2 style='text-align:center;font-family: Calibri,Candara,Segoe,Segoe UI,Optima,Arial,sans-serif; '>Reporte de {0}</h2></u><br/><br/>", tituloReporte)))
                {
                    htmlWorker.Parse(sr);
                }
            }
        }
        /// <summary>
        /// Método para insertar el encabezado en la primera página del Reporte.
        /// </summary>
        /// <param name="documentoReporte">Documento actual en el cual se está generando el PDF.</param>
        /// <param name="writerReporte">Writer actual con el cual se está generando el PDF.</param>
        public void GenerarEncabezadoReporte(Document documentoReporte, PdfWriter writerReporte)
        {
            var encabezadoHtml = string.Format(@"<!DOCTYPE html><html><head></head><body><table align='center' style='width:80%;'><tr><th rowspan='2'><p style='text-align:center'><img src='{0}' height=65 width=225 style='text-align:center;'></img></p></th><td rowspan='1'><p style='text-align:center'><b>Ingeniería de Ciencias de la Computación <br/>Sede Quito Campus Sur</b></p></td></tr><tr><td><p style='text-align:center'><b>Reporte de Activos de TI <br/>del Data Center y Laboratorios del ICC</b></p></td></tr></table></body></html>", System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Images/LogoUPS.png"));
            var encabezadoCss = @"body{font-family:Calibri,Candara,Segoe,Segoe UI,Optima,Arial,sans-serif}table,th,td{border:1px solid black;border-collapse:collapse}";
            using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(encabezadoCss)))
            {
                using (MemoryStream msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(encabezadoHtml)))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writerReporte, documentoReporte, msHtml, msCss);
                }
                msCss.Close();
            }
        }
        /// <summary>
        /// Método para generar la firma del responsable del Reporte.
        /// </summary>
        /// <param name="documentoReporte"></param>
        /// <param name="firmaUsuario"></param>
        public void GenerarFirmaReporte(Document documentoReporte,string firmaUsuario)
        {
            using (HTMLWorker htmlWorker = new HTMLWorker(documentoReporte))
            {
                using (var sr = new StringReader("<br/><br/><br/><br/><br/>"))
                {
                    htmlWorker.Parse(sr);
                }
            }
            Paragraph p = new Paragraph(new Chunk(new LineSeparator(0.0F, 25.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1)));
            Paragraph p1 = new Paragraph(new Chunk(new LineSeparator(0.0F, 25.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1)));
            documentoReporte.Add(p);
            documentoReporte.Add(p1);
            using (HTMLWorker htmlWorker = new HTMLWorker(documentoReporte))
            {
                using (var sr = new StringReader(string.Format("<div style='text-align: center;'><p style='font-family: Calibri,Candara,Segoe,Segoe UI,Optima,Arial,sans-serif;font-size:10px'><b>Elaborado por: </b>{0}</p></div>", Regex.Replace(firmaUsuario, @"(^\w)|(\s\w)", m => m.Value.ToUpper()))))
                {
                    htmlWorker.Parse(sr);
                }
            }
        }
    }
}