using DCICC.GestionInventarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Index(Login userInfo)
        {

            return RedirectToAction("Index", "Home");
        }
    }
}