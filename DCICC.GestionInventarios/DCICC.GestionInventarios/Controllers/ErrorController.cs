using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error404()
        {
            ViewBag.TipoError = "ERROR HTTP 404";
            ViewBag.MensajeError = "Página No Encontrada";
            return View("CustomError");
        }
        public ActionResult Error403()
        {
            ViewBag.TipoError = "ERROR HTTP 403";
            ViewBag.MensajeError = "Acceso Prohibido";
            return View("CustomError");
        }
        public ActionResult Error500()
        {
            ViewBag.TipoError = "ERROR HTTP 500";
            ViewBag.MensajeError = "Error Interno del Servidor";
            return View("CustomError");
        }
        public ActionResult Error503()
        {
            ViewBag.TipoError = "ERROR HTTP 503";
            ViewBag.MensajeError = "Servicio No Disponible";
            return View("CustomError");
        }
        public ActionResult Error401()
        {
            ViewBag.TipoError = "ERROR HTTP 401";
            ViewBag.MensajeError = "No Autorizado";
            return View("CustomError");
        }
    }
}