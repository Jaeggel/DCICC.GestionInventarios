using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class TicketsController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista GestionTickets
        /// </summary>
        /// <returns></returns>
        public ActionResult GestionTickets()
        {
            return View();
        }
    }
}