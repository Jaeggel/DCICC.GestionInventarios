using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class MaqVirtualesController : Controller
    {
        // GET: MaqVirtuales
        public ActionResult NuevaMaqVirtual()
        {
            return View();
        }

        public ActionResult ModificarMaqVirtual()
        {
            return View();
        }
    }
}