using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista relacionada al error 404.
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404()
        {
            ViewBag.TipoError = "ERROR HTTP 404";
            ViewBag.MensajeError = "Página No Encontrada";
            return View("CustomError");
        }
        /// <summary>
        /// Método (GET) para mostrar la vista relacionada al error 403.
        /// </summary>
        /// <returns></returns>
        public ActionResult Error403()
        {
            ViewBag.TipoError = "ERROR HTTP 403";
            ViewBag.MensajeError = "Acceso Prohibido";
            return View("CustomError");
        }
        /// <summary>
        /// Método (GET) para mostrar la vista relacionada al error 500.
        /// </summary>
        /// <returns></returns>
        public ActionResult Error500()
        {
            ViewBag.TipoError = "ERROR HTTP 500";
            ViewBag.MensajeError = "Error Interno del Servidor";
            return View("CustomError");
        }
        /// <summary>
        /// Método (GET) para mostrar la vista relacionada al error 503.
        /// </summary>
        /// <returns></returns>
        public ActionResult Error503()
        {
            ViewBag.TipoError = "ERROR HTTP 503";
            ViewBag.MensajeError = "Servicio No Disponible";
            return View("CustomError");
        }
        /// <summary>
        /// Método (GET) para mostrar la vista relacionada al error 401.
        /// </summary>
        /// <returns></returns>
        public ActionResult Error401()
        {
            ViewBag.TipoError = "ERROR HTTP 401";
            ViewBag.MensajeError = "No Autorizado";
            return View("CustomError");
        }
    }
}