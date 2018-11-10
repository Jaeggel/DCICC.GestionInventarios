using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class SistOperativoController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoSistOperativo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoSistOperativo()
        {
            return View();
        }

        /// <summary>
        /// Método (GET) para mostrar la vista ModificarSistOperativo
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarSistOperativo()
        {
            return View();
        }
    }
}