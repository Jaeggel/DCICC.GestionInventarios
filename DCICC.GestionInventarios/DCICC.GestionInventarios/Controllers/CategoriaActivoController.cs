using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class CategoriaActivoController : Controller
    {
        // GET: CategoriaActivo
        public ActionResult NuevoCategoriaActivo()
        {
            return View();
        }

        // GET: Modificar CategoriaActivo
        public ActionResult ModificarCategoriaActivo()
        {
            return View();
        }
    }
}