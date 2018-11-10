using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class MarcasController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevaMarca
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevaMarca()
        {
            return View();
        }

        /// <summary>
        /// Método (GET) para mostrar la vista ModificarMarca
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarMarca()
        {
            return View();
        }
    }
}