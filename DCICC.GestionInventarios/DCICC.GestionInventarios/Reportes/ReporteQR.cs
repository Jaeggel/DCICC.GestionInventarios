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
        readonly iTextSharp.text.Font fuente_Datos = FontFactory.GetFont("Arial", 8);
        /// <summary>
        /// Método para generar el PDF con el código QR de un nuevo activo.
        /// </summary>
        /// <param name="nombreActivo"></param>
        /// <param name="idQR"></param>
        /// <returns></returns>
        public byte[] GenerarPDFQRSimple(PdfPTable tablaQR)
        {
            byte[] pdfBytes = null;
            using (MemoryStream msReporte = new MemoryStream())
            {
                Document documentoReporte = new Document(PageSize.A4, 5f, 5f, 5f, 5f);
                using (PdfWriter writerReporte = PdfWriter.GetInstance(documentoReporte, msReporte))
                {
                    documentoReporte.Open();
                    documentoReporte.Add(tablaQR);
                    documentoReporte.Close();
                    pdfBytes = msReporte.ToArray();
                }
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
            using (MemoryStream msReporte = new MemoryStream())
            {
                Document documentoReporte = new Document(PageSize.A4, 5f, 5f, 5f, 5f);
                using (PdfWriter writerReporte = PdfWriter.GetInstance(documentoReporte, msReporte))
                {
                    documentoReporte.Open();
                    foreach (var item in lstActivos)
                    {
                        var tablaQR = GenerarTablaReporteQR(item.IdCQR, item.NombreActivo);
                        documentoReporte.Add(tablaQR);
                    }
                    documentoReporte.Close();
                    pdfBytes = msReporte.ToArray();
                }
            }
            return pdfBytes;
        }
        /// <summary>
        /// Método para generar el PDF con los códigos QR de los accesorios enviados como parámetro.
        /// </summary>
        /// <param name="nombreActivo"></param>
        /// <param name="idQR"></param>
        /// <returns></returns>
        public byte[] GenerarPDFQRLista(List<Accesorios> lstAccesorios)
        {
            byte[] pdfBytes = null;
            using (MemoryStream msReporte = new MemoryStream())
            {
                Document documentoReporte = new Document(PageSize.A4, 5f, 5f, 5f, 5f);
                using (PdfWriter writerReporte = PdfWriter.GetInstance(documentoReporte, msReporte))
                {
                    documentoReporte.Open();
                    foreach (var item in lstAccesorios)
                    {
                        var tablaQR = GenerarTablaReporteQR(item.IdCQR, item.NombreAccesorio);
                        documentoReporte.Add(tablaQR);
                    }
                    documentoReporte.Close();
                    pdfBytes = msReporte.ToArray();
                }
            }
            return pdfBytes;
        }
        /// <summary>
        /// Método para generar una tabla en donde se insertará la imagén del CQR con el nombre e id del QR.
        /// </summary>
        /// <param name="idQR"></param>
        /// <param name="nombreActivo"></param>
        /// <returns></returns>
        public PdfPTable GenerarTablaReporteQR(string idQR,string nombreActivo)
        {
            //Generación de imagen de Código QR para colocarlo en el PDF
            GeneracionCQR objGeneracionQR = new GeneracionCQR();
            Bitmap bitmap = objGeneracionQR.GenerarCodigoQR(idQR);
            byte[] bitmapBytes = objGeneracionQR.GenQRBytes(bitmap);
            System.Drawing.Image bitmapQRReporte = (Bitmap)new ImageConverter().ConvertFrom(bitmapBytes);
            iTextSharp.text.Image imagenQRReporte = iTextSharp.text.Image.GetInstance(bitmapQRReporte, System.Drawing.Imaging.ImageFormat.Jpeg);
            //Configuración de tabla e imagen que irá en el PDF
            PdfPTable tablaQR = new PdfPTable(1);
            tablaQR.WidthPercentage = 10;
            tablaQR.AddCell(imagenQRReporte);
            //Configuración de celda para imagen QR
            PdfPCell celdaQR;
            celdaQR = new PdfPCell(new Phrase(string.Format("{0}\n{1}",idQR,nombreActivo), new iTextSharp.text.Font(fuente_Datos)));
            celdaQR.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaQR.AddCell(celdaQR);
            tablaQR.HorizontalAlignment = Element.ALIGN_LEFT;
            return tablaQR;
        }
    }
}