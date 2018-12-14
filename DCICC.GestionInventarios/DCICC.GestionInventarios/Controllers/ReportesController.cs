using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Reportes;
using iTextSharp.text.pdf;
using log4net;
using System;
using System.Data;
using System.IO;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class ReportesController : Controller
    {
        //Instancia para la utilización de LOGS en la clase ReportesController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista ReportesActivos
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportesActivos()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ReportesTickets
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportesTickets()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ReportesMaqVirtuales
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportesMaqVirtuales()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ReportesLogs
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportesLogs()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        #endregion
        #region Generación Reportes
        static DataTable info_DataTable;
        static string titulo_Reporte;
        static string lab_Filtro;
        /// <summary>
        /// Método para generar el DataTable con el cual se generarán los reportes en Excel y PDF.
        /// </summary>
        /// <param name="infoHtml">Cadena de HTML con los datos para generar el reporte.</param>
        [HttpPost]
        [ValidateInput(false)]
        public void GenerarDataTable(string infoHtml,string tituloReporte,string labFiltro)
        {
            info_DataTable = null;
            titulo_Reporte = null;
            lab_Filtro = null;
            try
            {
                ConfigDatos objDatosReporte = new ConfigDatos();
                titulo_Reporte = tituloReporte;
                info_DataTable = objDatosReporte.ObtenerDatosTablaHTML(infoHtml);
                lab_Filtro = labFiltro;
                Logs.Info("DataTable generado correctamente.");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar el DataTable: {0}",e.Message));
            }
        }
        /// <summary>
        /// Método para obtener el Reporte PDF.
        /// </summary>
        /// <returns></returns>
        public ActionResult ObtenerReportePDF()
        {
            byte[] bytesReportePDF=null;
            try
            {
                ReportePDF objReporte = new ReportePDF();
                PdfPTable tablaReporte = objReporte.GenerarTablaReporte(info_DataTable);
                bytesReportePDF = objReporte.GenerarReportePDF(tablaReporte, titulo_Reporte,(string)Session["NombresUsuario"]);
                var contentDispositionHeader = new System.Net.Mime.ContentDisposition
                {
                    Inline = true,
                    FileName = string.Format("Reporte.{0}.{1}.{2}",titulo_Reporte,DateTime.Now.ToString("dd-MM-yyyy.hh-mm-ss"),"pdf"),
                };
                Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
                Logs.Info("Reporte PDF generado correctamente.");
            }
            catch(Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar el reporte PDF: {0}", e.Message));
            }
            return File(bytesReportePDF, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }
        /// <summary>
        /// Método para obtener el Reporte en Excel.
        /// </summary>
        /// <returns></returns>
        public ActionResult ObtenerReporteExcel()
        {
            MemoryStream streamReporteExcel = null;
            try
            {
                ReporteExcel objReporteExcel = new ReporteExcel();
                streamReporteExcel = objReporteExcel.GenerarReporteExcel(info_DataTable, titulo_Reporte, lab_Filtro, (string)Session["NombresUsuario"]);
                Logs.Info("Reporte Excel generado correctamente.");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar el reporte Excel: {0}", e.Message));
            }
            return File(streamReporteExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("Reporte.{0}.{1}.{2}", titulo_Reporte,DateTime.Now.ToString("dd-MM-yyyy.hh-mm-ss"),"xlsx"));
        }
        /// <summary>
        /// Método para mostrar el PDF con los códigos QR de activos seleccionados en la vista.
        /// </summary>
        /// <param name="lstActivos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ObtenerPDFActivosQRLote()
        {
            byte[] pdfQR = null;
            try
            {
                ConfigDatos objDatosReporte = new ConfigDatos();
                var lstActivos = objDatosReporte.ObtenerListaActivosQR(info_DataTable);
                ReporteQR objReporteQR = new ReporteQR();
                pdfQR = objReporteQR.GenerarPDFQRLista(lstActivos);
                Logs.Info("El PDF con códigos QR de activos en lote ha sido generado exitosamente.");
                var contentDispositionHeader = new System.Net.Mime.ContentDisposition
                {
                    Inline = true,
                    FileName = string.Format("DCICC.ActivosCQR.{0}.{1}", DateTime.Now.ToString("dd-MM-yyyy.hh-mm-ss"), "pdf")
                };
                Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", "No se ha podido generar el PDF con los códigos QR de activos en lote", e.Message));
            }
            return File(pdfQR, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }
        /// <summary>
        /// Método para mostrar el PDF con los códigos QR de accesorios seleccionados en la vista.
        /// </summary>
        /// <param name="lstAccesorios"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ObtenerPDFAccesoriosQRLote()
        {
            byte[] pdfQR = null;
            try
            {
                ConfigDatos objDatosReporte = new ConfigDatos();
                var lstAccesorios = objDatosReporte.ObtenerListaAccesoriosQR(info_DataTable);
                ReporteQR objReporteQR = new ReporteQR();
                pdfQR = objReporteQR.GenerarPDFQRLista(lstAccesorios);
                Logs.Info("El PDF con códigos QR de activos en lote ha sido generado exitosamente.");
                var contentDispositionHeader = new System.Net.Mime.ContentDisposition
                {
                    Inline = true,
                    FileName = string.Format("DCICC.AccesoriosCQR.{0}.{1}",DateTime.Now.ToString("dd-MM-yyyy.hh-mm-ss"), "pdf")
                };
                Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", "No se ha podido generar el PDF con los códigos QR de activos en lote", e.Message));
            }
            return File(pdfQR, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }
        #endregion
    }
}