using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class TipoActivoController : Controller
    {
        // GET: TipoActivo
        public ActionResult NuevoTipoActivo()
        {
            return View();
        }

        // GET: Modificar TipoActivo
        public ActionResult ModificarTipoActivo()
        {
            return View();
        }
    }
}