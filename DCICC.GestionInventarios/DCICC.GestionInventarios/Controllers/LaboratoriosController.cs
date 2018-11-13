using DCICC.GestionInventarios.Models;
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
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoLaboratorio.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoLaboratorio(Laboratorios infoLaboratorio)
        {
            return RedirectToAction("ModificarLaboratorio", "Laboratorios");
        }
    }
}