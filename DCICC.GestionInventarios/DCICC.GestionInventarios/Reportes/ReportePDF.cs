using DCICC.GestionInventarios.Controllers;
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
        public byte[] GenerarReportePDF(PdfPTable tablaReporte,string tituloReporte,string firmaUsuario,string labFiltro)
        {
            byte[] bytesReportePDF=null;
            using (MemoryStream msReporte = new MemoryStream())
            {
                Rectangle reporteAlign;
                Document documentoReporte = new Document(reporteAlign=tituloReporte=="Tickets" || tituloReporte == "Especificaciones Adicionales" || tituloReporte== "Activos que han cumplido su vida útil" ? PageSize.A4.Rotate(): PageSize.A4);
                using (PdfWriter writerReporte = PdfWriter.GetInstance(documentoReporte, msReporte))
                {
                    documentoReporte.Open();
                    GenerarEncabezadoReporte(documentoReporte, writerReporte);
                    GenerarTituloReporte(documentoReporte, tituloReporte, tablaReporte.Rows.Count-1,labFiltro);
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
        public void GenerarTituloReporte(Document documentoReporte,string tituloReporte,int numEquipos,string labFiltro)
        {
            if (labFiltro == "Mostrar Todos")
            {
                labFiltro = "Todos";
            }
            else if (labFiltro == "" || labFiltro == null)
            {
                labFiltro = "-";
            }
            using (HTMLWorker htmlWorker = new HTMLWorker(documentoReporte))
            {
                using (var sr = new StringReader(string.Format("<br/><u><h2 style='text-align:center;font-family: Calibri,Candara,Segoe,Segoe UI,Optima,Arial,sans-serif; '>Reporte de {0}</h2></u><br/>", tituloReporte)))
                {
                    htmlWorker.Parse(sr);
                }
            }           
            documentoReporte.Add(GenerarParrafosParametros("Fecha de Elaboración: ", DateTime.Now.ToLongDateString()));
            documentoReporte.Add(GenerarParrafosParametros("Laboratorio: ", labFiltro));
            documentoReporte.Add(GenerarParrafosParametros("N° de Registros: ", numEquipos+""));
            documentoReporte.Add(Chunk.NEWLINE);
        }
        /// <summary>
        /// Método para generar los párrafos para el reporte PDF.
        /// </summary>
        /// <param name="contTitulo"></param>
        /// <param name="contValor"></param>
        /// <returns></returns>
        public Paragraph GenerarParrafosParametros(string contTitulo,string contValor)
        {
            Chunk parrafoTitulo = new Chunk(contTitulo, new Font(Font.FontFamily.UNDEFINED, 10, Font.BOLD));
            Chunk parrafoCont = new Chunk(contValor, new Font(Font.FontFamily.UNDEFINED, 10));
            Paragraph parrafo = new Paragraph();
            parrafo.Add(new Chunk(parrafoTitulo));
            parrafo.Add(new Chunk(parrafoCont));
            return parrafo;
        }
        /// <summary>
        /// Método para insertar el encabezado en la primera página del Reporte.
        /// </summary>
        /// <param name="documentoReporte">Documento actual en el cual se está generando el PDF.</param>
        /// <param name="writerReporte">Writer actual con el cual se está generando el PDF.</param>
        public void GenerarEncabezadoReporte(Document documentoReporte, PdfWriter writerReporte)
        {
            ReportesController objReportesCont = new ReportesController();
            Models.Reportes infoParametros = objReportesCont.ObtenerParametrosReporteJSON();
            if(infoParametros.TituloReporte== "Reporte de Activos de TI del Data Center y Laboratorios del ICC")
            {
                infoParametros.TituloReporte= "Reporte de Activos de TI <br/> del Data Center y Laboratorios del ICC";
            }
            var encabezadoHtml = string.Format(@"<!DOCTYPE html><html><head></head><body><table align='center' style='width:80%;'><tr><th rowspan='2'><p style='text-align:center'><img src='{0}' height=65 width=225 style='text-align:center;'></img></p></th><td rowspan='1'><p style='text-align:center'><b>{1} <br/>{2}</b></p></td></tr><tr><td><p style='text-align:center'><b>{3}</b></p></td></tr></table></body></html>", System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Images/LogoReporte/LogoUPS.png"),infoParametros.TituloCarrera,infoParametros.TituloSedeCampus,infoParametros.TituloReporte);
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
        public void GenerarFirmaReporte(Document documentoReporte, string firmaUsuario)
        {
            using (HTMLWorker htmlWorker = new HTMLWorker(documentoReporte))
            {
                using (var sr = new StringReader("<br/><br/><br/><br/><br/>"))
                {
                    htmlWorker.Parse(sr);
                }
            }
            PdfPTable tablaReporteQR = new PdfPTable(3);
            tablaReporteQR.DefaultCell.Border = PdfPCell.NO_BORDER;

            PdfPCell celda1 = new PdfPCell();
            celda1.UseVariableBorders = true;
            celda1.BorderColorTop = BaseColor.BLACK;
            celda1.BorderColorBottom = BaseColor.WHITE;
            celda1.BorderColorLeft = BaseColor.WHITE;
            celda1.BorderColorRight = BaseColor.WHITE;
            Paragraph IdCQR11 = new Paragraph("Elaborador por: ", new Font(Font.FontFamily.UNDEFINED, 10, Font.BOLD));
            IdCQR11.Alignment = Element.ALIGN_CENTER;
            celda1.AddElement(IdCQR11);
            Paragraph IdCQR1 = new Paragraph(Regex.Replace(firmaUsuario, @"(^\w)|(\s\w)", m => m.Value.ToUpper()), new Font(Font.FontFamily.UNDEFINED, 9));
            IdCQR1.Alignment = Element.ALIGN_CENTER;
            celda1.AddElement(IdCQR1);
            tablaReporteQR.AddCell(celda1);

            ActivosController objActivo = new ActivosController();
            PdfPCell celda2 = new PdfPCell();
            celda2.UseVariableBorders = true;
            celda2.BorderColorTop = BaseColor.WHITE;
            celda2.BorderColorBottom = BaseColor.WHITE;
            celda2.BorderColorLeft = BaseColor.WHITE;
            celda2.BorderColorRight = BaseColor.WHITE;
            Paragraph IdCQR2 = new Paragraph("");
            celda2.AddElement(IdCQR2);
            tablaReporteQR.AddCell(celda2);

            PdfPCell celda3 = new PdfPCell();
            celda3.UseVariableBorders = true;
            celda3.BorderColorTop = BaseColor.BLACK;
            celda3.BorderColorBottom = BaseColor.WHITE;
            celda3.BorderColorLeft = BaseColor.WHITE;
            celda3.BorderColorRight = BaseColor.WHITE;
            Paragraph IdCQR31 = new Paragraph("Administrador: ", new Font(Font.FontFamily.UNDEFINED, 10, Font.BOLD));
            IdCQR31.Alignment = Element.ALIGN_CENTER;
            celda3.AddElement(IdCQR31);
            Paragraph IdCQR3 = new Paragraph(objActivo.ObtenerResponsableActualObj().ResponsableActivo, new Font(Font.FontFamily.UNDEFINED, 9));
            IdCQR3.Alignment = Element.ALIGN_CENTER;
            celda3.AddElement(IdCQR3);
            tablaReporteQR.AddCell(celda3);

            tablaReporteQR.HorizontalAlignment = Element.ALIGN_CENTER;
            documentoReporte.Add(tablaReporteQR);
        }
    }
}