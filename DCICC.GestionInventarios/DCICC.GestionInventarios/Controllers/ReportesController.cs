using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Reportes;
using iTextSharp.text.pdf;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
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
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
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
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
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
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
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
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        #endregion
        #region Generación de Datos para Reportes
        static DataTable info_DataTable;
        static string titulo_Reporte;
        static string lab_Filtro;
        static List<Activos> lst_ActivosCQR = null;
        static List<Accesorios> lst_AccesoriosCQR = null;
        /// <summary>
        /// Método para generar el DataTable con el cual se generarán los reportes en Excel y PDF.
        /// </summary>
        /// <param name="infoHtml">Cadena de HTML con los datos para generar el reporte.</param>
        [HttpPost]
        [ValidateInput(false)]
        public void GenerarDataTable(string infoHtml, string tituloReporte, string labFiltro)
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
                Logs.Info("El DataTable ha sido generado correctamente.");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar el DataTable: {0}", e.Message));
            }
        }
        /// <summary>
        /// Método para generar la lista de Accesorios y CQR para obtener el PDF de QR en lotes.
        /// </summary>
        /// <param name="lstIdCQRAccesorios"></param>
        [HttpPost]
        public void GenerarListaAccesoriosCQR(List<string> lstIdCQRAccesorios)
        {
            try
            {
                lst_AccesoriosCQR = new List<Accesorios>();
                AccesoriosAccDatos objActivosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
                List<Accesorios> lstNombresAccesorios = objActivosAccDatos.ObtenerAccesorios("Nombres").ListaObjetoInventarios;
                foreach (var item in lstNombresAccesorios)
                {
                    foreach (var idcqr in lstIdCQRAccesorios)
                    {
                        if (idcqr == item.IdCQR)
                        {
                            Accesorios objAccesorios = new Accesorios()
                            {
                                IdCQR = idcqr,
                                NombreAccesorio = item.NombreAccesorio
                            };
                            lst_AccesoriosCQR.Add(objAccesorios);
                        }
                    }
                }
                Logs.Info("La Lista Accesorios CQR ha sido generado correctamente.");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar la lista de Accesorios CQR: {0}", e.Message));
            }
        }
        /// <summary>
        /// Método para generar la lista de Activos y CQR para obtener el PDF de QR en lotes.
        /// </summary>
        /// <param name="lstIdCQRActivos"></param>
        [HttpPost]
        public void GenerarListaActivosCQR(List<string> lstIdCQRActivos)
        {
            try
            {
                lst_ActivosCQR = new List<Activos>();
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                List<Activos> lstNombresActivos = objActivosAccDatos.ObtenerActivos("Nombres").ListaObjetoInventarios;
                foreach (var item in lstNombresActivos)
                {
                    foreach (var idcqr in lstIdCQRActivos)
                    {
                        if (idcqr == item.IdCQR)
                        {
                            Activos objActivos = new Activos()
                            {
                                IdCQR = idcqr,
                                NombreActivo = item.NombreActivo
                            };
                            lst_ActivosCQR.Add(objActivos);
                        }
                    }
                }
                Logs.Info("La Lista Activos CQR ha sido generado correctamente.");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar la lista de Activos CQR: {0}", e.Message));
            }
        }
        #endregion
        #region Generación de Reportes 
        /// <summary>
        /// Método para obtener el Reporte PDF.
        /// </summary>
        /// <returns></returns>
        public ActionResult ObtenerReportePDF()
        {
            byte[] bytesReportePDF = null;
            try
            {
                ReportePDF objReporte = new ReportePDF();
                PdfPTable tablaReporte = objReporte.GenerarTablaReporte(info_DataTable);
                bytesReportePDF = objReporte.GenerarReportePDF(tablaReporte, titulo_Reporte, (string)Session["NombresUsuario"],lab_Filtro);
                var contentDispositionHeader = new System.Net.Mime.ContentDisposition
                {
                    Inline = true,
                    FileName = string.Format("Reporte.{0}.{1}.{2}", titulo_Reporte, DateTime.Now.ToString("dd-MM-yyyy.hh-mm-ss"), "pdf"),
                };
                Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
                Logs.Info("Reporte PDF generado correctamente.");
            }
            catch (Exception e)
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
            return File(streamReporteExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("Reporte.{0}.{1}.{2}", titulo_Reporte, DateTime.Now.ToString("dd-MM-yyyy.hh-mm-ss"), "xlsx"));
        }
        /// <summary>
        /// Método para mostrar el PDF con los códigos QR de activos seleccionados en la vista.
        /// </summary>
        /// <param name="lstActivos"></param>
        /// <returns></returns>
        public ActionResult ObtenerPDFActivosQRLote()
        {
            byte[] pdfQR = null;
            try
            {
                ReporteQR objReporteQR = new ReporteQR();
                pdfQR = objReporteQR.GenerarPDFQRLista(lst_ActivosCQR);
                objReporteQR.ActualizarImpresoActivosQR(lst_ActivosCQR, (string)Session["NickUsuario"]);
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
        public ActionResult ObtenerPDFAccesoriosQRLote()
        {
            byte[] pdfQR = null;
            try
            {
                ReporteQR objReporteQR = new ReporteQR();
                pdfQR = objReporteQR.GenerarPDFQRLista(lst_AccesoriosCQR);
                objReporteQR.ActualizarImpresoAccesoriosQR(lst_AccesoriosCQR, (string)Session["NickUsuario"]);
                Logs.Info("El PDF con códigos QR de accesorios en lote ha sido generado exitosamente.");
                var contentDispositionHeader = new System.Net.Mime.ContentDisposition
                {
                    Inline = true,
                    FileName = string.Format("DCICC.AccesoriosCQR.{0}.{1}", DateTime.Now.ToString("dd-MM-yyyy.hh-mm-ss"), "pdf")
                };
                Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", "No se ha podido generar el PDF con los códigos QR de accesorios en lote", e.Message));
            }
            return File(pdfQR, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }
        #endregion
    }
}