using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class LaboratoriosController : Controller
    {
        // GET: Laboratorios
        public ActionResult NuevoLaboratorio()
        {
            return View();
        }

        // GET: Modificar Laboratorios
        public ActionResult ModificarLaboratorio()
        {
            return View();
        }
    }
}