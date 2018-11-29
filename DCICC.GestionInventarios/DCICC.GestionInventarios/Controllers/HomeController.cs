using DCICC.GestionInventarios.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                if(LoginController.contMsj==1)
                {
                    ViewBag.MsjBienv = "true";
                }
                LoginController.contMsj = 0;
                return View();
            }
        }
    }
}