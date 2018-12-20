using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
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
        [HttpGet]
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
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todos los Laboratorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerDashboard()
        {
            DashboardAccDatos objDashboardActAccDatos = new DashboardAccDatos((string)Session["NickUsuario"]);
            return Json(objDashboardActAccDatos.ObtenerDashboard((string)Session["NickUsuario"]), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}