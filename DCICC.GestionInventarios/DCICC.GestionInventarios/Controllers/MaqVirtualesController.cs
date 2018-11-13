using DCICC.GestionInventarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class MaqVirtualesController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevaMaqVirtual
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevaMaqVirtual()
        {
            return View();
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ModificarMaqVirtual
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarMaqVirtual()
        {
            return View();
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoMaqVirtual.
        /// </summary>
        /// <param name="infoMaqVirtuales"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevaMaqVirtual(MaqVirtuales infoMaqVirtuales)
        {
            return RedirectToAction("ModificarMaqVirtual", "MaqVirtuales");
        }
    }
}