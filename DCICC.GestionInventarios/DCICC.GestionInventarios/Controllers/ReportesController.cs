using DCICC.GestionInventarios.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    public class ReportesController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista Reportes
        /// </summary>
        /// <returns></returns>
        public ActionResult Reportes()
        {
            if (Session["userInfo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }
    }
}