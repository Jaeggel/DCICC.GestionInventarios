using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class TipoAccesorioController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoTipoAccesorio
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoTipoAccesorio()
        {
            return View();
        }

        /// <summary>
        /// Método (GET) para mostrar la vista ModificarTipoAccesorio
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarTipoAccesorio()
        {
            return View();
        }
    }
}