using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class ActivosController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoActivo()
        {
            return View();
        }

        /// <summary>
        /// Método (GET) para mostrar la vista ConsutaActivos
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultaActivos()
        {
            return View();
        }

        /// <summary>
        /// Método (GET) para mostrar la vista ConsutaActivos
        /// </summary>
        /// <returns></returns>
        public ActionResult ReporteActivos()
        {
            return View();
        }
    }
}