using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
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
        readonly iTextSharp.text.Font fuente_Datos = FontFactory.GetFont("Arial", 5);
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
                Document documentoReporte = new Document(PageSize.LETTER);
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
                Document documentoReporte = new Document(PageSize.LETTER);
                using (PdfWriter writerReporte = PdfWriter.GetInstance(documentoReporte, msReporte))
                {
                    documentoReporte.Open();
                    var tablaQR = GenerarTablaReporteQR(lstActivos);
                    documentoReporte.Add(tablaQR);
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
                Document documentoReporte = new Document(PageSize.LETTER);
                using (PdfWriter writerReporte = PdfWriter.GetInstance(documentoReporte, msReporte))
                {
                    documentoReporte.Open();
                    var tablaQR = GenerarTablaReporteQR(lstAccesorios);
                    documentoReporte.Add(tablaQR);
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
        public PdfPTable GenerarTablaReporteQR(string idQR,string nombreActivo,string nombreActivoRaiz)
        {
            PdfPTable tablaQR = new PdfPTable(1);
            if (nombreActivoRaiz!="")
            {
                tablaQR.WidthPercentage = 10;
                PdfPCell celda = new PdfPCell();
                celda.AddElement(GenerarImagenCQR(idQR));
                Paragraph IdCQR = new Paragraph(idQR, new iTextSharp.text.Font(fuente_Datos));
                IdCQR.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(IdCQR);
                Paragraph NombreActivo = new Paragraph(nombreActivoRaiz, new iTextSharp.text.Font(fuente_Datos));
                NombreActivo.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(NombreActivo);
                Paragraph NombreCQR = new Paragraph(nombreActivo, new iTextSharp.text.Font(fuente_Datos));
                NombreCQR.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(NombreCQR);
                tablaQR.AddCell(celda);
                tablaQR.HorizontalAlignment = Element.ALIGN_LEFT;
            }
            else
            {
                tablaQR.WidthPercentage = 10;
                PdfPCell celda = new PdfPCell();
                celda.AddElement(GenerarImagenCQR(idQR));
                Paragraph IdCQR = new Paragraph(idQR, new iTextSharp.text.Font(fuente_Datos));
                IdCQR.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(IdCQR);
                Paragraph NombreCQR = new Paragraph(nombreActivo, new iTextSharp.text.Font(fuente_Datos));
                NombreCQR.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(NombreCQR);
                tablaQR.AddCell(celda);
                tablaQR.HorizontalAlignment = Element.ALIGN_LEFT;
            }
            return tablaQR;
        }
        /// <summary>
        /// Método para generar una tabla en donde se insertará la imagén del CQR con el nombre e id del QR.
        /// </summary>
        /// <param name="idQR"></param>
        /// <param name="nombreActivo"></param>
        /// <returns></returns>
        public PdfPTable GenerarTablaReporteQR(List<Activos> lstActivos)
        {
            PdfPTable tablaReporteQR = new PdfPTable(8);
            tablaReporteQR.DefaultCell.Border = PdfPCell.NO_BORDER;
            foreach (var item in lstActivos)
            {
                PdfPCell celda = new PdfPCell();
                celda.BorderColorTop=BaseColor.BLACK;
                celda.BorderColorBottom = BaseColor.BLACK;
                celda.BorderColorLeft = BaseColor.BLACK;
                celda.BorderColorRight = BaseColor.BLACK;
                celda.AddElement(GenerarImagenCQR(item.IdCQR));
                Paragraph IdCQR = new Paragraph(item.IdCQR, new iTextSharp.text.Font(fuente_Datos));
                IdCQR.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(IdCQR);
                Paragraph NombreCQR = new Paragraph(item.NombreActivo, new iTextSharp.text.Font(fuente_Datos));
                NombreCQR.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(NombreCQR);
                tablaReporteQR.AddCell(celda);
            }
            tablaReporteQR.CompleteRow();
            tablaReporteQR.HorizontalAlignment = Element.ALIGN_CENTER;
            return tablaReporteQR;
        }
        /// <summary>
        /// Método para generar una tabla en donde se insertará la imagén del CQR con el nombre e id del QR.
        /// </summary>
        /// <param name="idQR"></param>
        /// <param name="nombreActivo"></param>
        /// <returns></returns>
        public PdfPTable GenerarTablaReporteQR(List<Accesorios> lstAccesorios)
        {
            PdfPTable tablaReporteQR = new PdfPTable(8);
            tablaReporteQR.DefaultCell.Border = PdfPCell.NO_BORDER;
            foreach (var item in lstAccesorios)
            {
                PdfPCell celda = new PdfPCell();
                celda.BorderColorTop = BaseColor.BLACK;
                celda.BorderColorBottom = BaseColor.BLACK;
                celda.BorderColorLeft = BaseColor.BLACK;
                celda.BorderColorRight = BaseColor.BLACK;
                celda.AddElement(Chunk.NEWLINE);
                celda.AddElement(GenerarImagenCQR(item.IdCQR));
                Paragraph IdCQR = new Paragraph(item.IdCQR, new iTextSharp.text.Font(fuente_Datos));
                IdCQR.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(IdCQR);
                Paragraph NombreActivo = new Paragraph(item.NombreDetalleActivo, new iTextSharp.text.Font(fuente_Datos));
                NombreActivo.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(NombreActivo);
                Paragraph NombreCQR = new Paragraph(item.NombreAccesorio, new iTextSharp.text.Font(fuente_Datos));
                NombreCQR.Alignment = Element.ALIGN_CENTER;
                celda.AddElement(NombreCQR);
                tablaReporteQR.AddCell(celda);
            }
            tablaReporteQR.CompleteRow();
            tablaReporteQR.HorizontalAlignment = Element.ALIGN_CENTER;
            return tablaReporteQR;
        }
        /// <summary>
        /// Método para generar la imágen del CQR para el reporte PDF.
        /// </summary>
        /// <param name="idQR"></param>
        /// <returns></returns>
        public iTextSharp.text.Image GenerarImagenCQR(string idQR)
        {
            string[] auxCQR = idQR.Split('.');
            GeneracionCQR objGeneracionQR = new GeneracionCQR(auxCQR[1]);
            Bitmap bitmap = objGeneracionQR.GenerarCodigoQR(idQR);
            byte[] bitmapBytes = objGeneracionQR.GenQRBytes(bitmap);
            System.Drawing.Image bitmapQRReporte = (Bitmap)new ImageConverter().ConvertFrom(bitmapBytes);
            return iTextSharp.text.Image.GetInstance(bitmapQRReporte, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        /// <summary>
        /// Método para actualizar el estado impreso del CQR de una lista de Activos.
        /// </summary>
        /// <param name="lstActivos"></param>
        /// <param name="usuarioActual"></param>
        public void ActualizarImpresoActivosQR(List<Activos> lstActivos,string usuarioActual)
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos(usuarioActual);
            objActivosAccDatos.ActualizarCQR(null, lstActivos, true);
        }
        /// <summary>
        /// Método para actualizar el estado impreso del CQR de una lista de Accesorios.
        /// </summary>
        /// <param name="lstActivos"></param>
        /// <param name="usuarioActual"></param>
        public void ActualizarImpresoAccesoriosQR(List<Accesorios> lstAccesorio, string usuarioActual)
        {
            AccesoriosAccDatos objActivosAccDatos = new AccesoriosAccDatos(usuarioActual);
            objActivosAccDatos.ActualizarCQR(lstAccesorio);
        }
    }
}