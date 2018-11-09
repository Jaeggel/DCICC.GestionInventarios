using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class SistOperativoController : Controller
    {
        // GET: Sistemas Operativos
        public ActionResult NuevoSistOperativo()
        {
            return View();
        }

        // GET: Modificar Sistemas Operativos
        public ActionResult ModificarSistOperativo()
        {
            return View();
        }
    }
}