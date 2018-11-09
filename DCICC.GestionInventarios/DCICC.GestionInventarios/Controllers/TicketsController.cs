using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class TicketsController : Controller
    {
        // GET: Tickets
        public ActionResult GestionTickets()
        {
            return View();
        }
    }
}