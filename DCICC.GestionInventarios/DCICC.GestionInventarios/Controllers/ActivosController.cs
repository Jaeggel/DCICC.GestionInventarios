using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class ActivosController : Controller
    {
        public ActionResult Index()
        {
            return View("NuevoActivo");
        }
    }
}