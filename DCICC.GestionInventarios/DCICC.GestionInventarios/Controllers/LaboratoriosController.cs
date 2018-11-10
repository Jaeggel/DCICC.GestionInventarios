using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class LaboratoriosController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoLaboratorio
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoLaboratorio()
        {
            return View();
        }

        /// <summary>
        /// Método (GET) para mostrar la vista ModificarLaboratorio
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarLaboratorio()
        {
            return View();
        }
    }
}