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
    }
}