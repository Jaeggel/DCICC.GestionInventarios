using DCICC.GestionInventarios.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class HomeController : Controller
    {
        #region Vistas (GET)
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
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                if(LoginController.cont_Msj==1)
                {
                    ViewBag.MsjBienv = "true";
                }
                LoginController.cont_Msj = 0;
                return View();
            }
        }
        #endregion
    }
}