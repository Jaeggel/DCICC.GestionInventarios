using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class TipoAccesorioController : Controller
    {
        // GET: TipoAccesorio
        public ActionResult NuevoTipoAccesorio()
        {
            return View();
        }

        // GET: Modificar TipoAccesorio
        public ActionResult ModificarTipoAccesorio()
        {
            return View();
        }
    }
}