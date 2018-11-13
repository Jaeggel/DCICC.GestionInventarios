using DCICC.GestionInventarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class RolesController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoRol
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoRol()
        {
            return View();
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ModificarRol
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarRol()
        {
            return View();
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoRol.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoRol(Roles infoRol)
        {
            return RedirectToAction("ModificarRol", "Roles");
        }
    }
}