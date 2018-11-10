using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class LogsController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista Logs
        /// </summary>
        /// <returns></returns>
        public ActionResult Logs()
        {
            return View();
        }
    }
}