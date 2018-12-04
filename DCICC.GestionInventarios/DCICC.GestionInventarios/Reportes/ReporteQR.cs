using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.QR;
using iTextSharp.text;
using iTextSharp.text.pdf;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DCICC.GestionInventarios.Reportes
{
    public class ReporteQR
    {
        //Instancia para la utilización de LOGS en la clase ReporteQR
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método para generar el PDF con el código QR de un nuevo activo.
        /// </summary>
        /// <param name="nombreActivo"></param>
        /// <param name="idQR"></param>
        /// <returns></returns>
        public byte[] GenerarPDFQRSimple(string nombreActivo, string idQR)
        {
            byte[] pdfBytes = null;
            Document doc = new Document(PageSize.A4, 5f, 5f, 5f, 5f);
            try
            {
                using (var mem = new MemoryStream())
                {
                    //Instancia de iTextsharp y definición de fuente de letra
                    PdfWriter writer = PdfWriter.GetInstance(doc, mem);
                    iTextSharp.text.Font verdana = FontFactory.GetFont("Arial", 8);
                    iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(verdana);
                    doc.Open();
                    //Generación de imagen de Código QR para colocarlo en el PDF
                    GeneracionCQR objGeneracionQR = new GeneracionCQR();
                    var bitmap = objGeneracionQR.GenerarCodigoQR(idQR);
                    var bitmapBytes = objGeneracionQR.GenQRBytes(bitmap);
                    System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(bitmapBytes));
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(x, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //Configuración de tabla e imagen que irá en el PDF
                    PdfPTable table = new PdfPTable(1);
                    table.WidthPercentage = 10;
                    table.AddCell(jpg);
                    PdfPCell cell;
                    cell = new PdfPCell(new Phrase(idQR + "\n" + nombreActivo, new iTextSharp.text.Font(verdana)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    doc.Add(table);
                    doc.Close();
                    pdfBytes = mem.ToArray();
                    Logs.Info("Bytes para PDF generados exitosamente.");
                }
            } catch (Exception e)
            {
                Logs.Error("No se han podido generar los bytes: " + e.Message);
            }
            return pdfBytes;
        }
        /// <summary>
        /// Método para generar el PDF con los códigos QR de los activos enviados como parámetro.
        /// </summary>
        /// <param name="nombreActivo"></param>
        /// <param name="idQR"></param>
        /// <returns></returns>
        public byte[] GenerarPDFQRLista(List<Activos> lstActivos)
        {
            byte[] pdfBytes = null;
            Document doc = new Document(PageSize.A4, 5f, 5f, 5f, 5f);
            try
            {
                using (var mem = new MemoryStream())
                {
                    //Instancia de iTextsharp y definición de fuente de letra
                    PdfWriter writer = PdfWriter.GetInstance(doc, mem);
                    iTextSharp.text.Font verdana = FontFactory.GetFont("Arial", 8);
                    iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(verdana);
                    doc.Open();
                    PdfPTable table = new PdfPTable(lstActivos.Count);
                    table.WidthPercentage = 10;
                    foreach (var itemActivo in lstActivos)
                    {
                        //Generación de imagen de Código QR para colocarlo en el PDF
                        GeneracionCQR objGeneracionQR = new GeneracionCQR();
                        var bitmap = objGeneracionQR.GenerarCodigoQR(itemActivo.IdCQR);
                        var bitmapBytes = objGeneracionQR.GenQRBytes(bitmap);
                        //Configuración de tabla e imagen que irá en el PDF
                        System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(bitmapBytes));
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(x, System.Drawing.Imaging.ImageFormat.Jpeg);
                        table.AddCell(jpg);
                        PdfPCell cell;
                        cell = new PdfPCell(new Phrase(itemActivo.IdCQR + "\n" + itemActivo.NombreActivo, new iTextSharp.text.Font(verdana)));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                    table.HorizontalAlignment = Element.ALIGN_LEFT; 
                    doc.Add(table);
                    doc.Close();
                    pdfBytes = mem.ToArray();
                    Logs.Info("Bytes para PDF generados exitosamente.");
                }
            }
            catch (Exception e)
            {
                Logs.Error("No se han podido generar los bytes: " + e.Message);
            }
            return pdfBytes;
        }
    }
}