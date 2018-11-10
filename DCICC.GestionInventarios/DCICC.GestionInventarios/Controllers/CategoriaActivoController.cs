using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class CategoriaActivoController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista CategoriaActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoCategoriaActivo()
        {
            return View();
        }

        /// <summary>
        /// Método (GET) para mostrar la vista ModificarCategoriaActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarCategoriaActivo()
        {
            return View();
        }
    }
}