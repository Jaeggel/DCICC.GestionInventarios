using DCICC.GestionInventarios.Filtros;
using DCICC.GestionInventarios.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Index(Login userInfo)
        {
            MenuActionFilter.ObtenerMenu("Usuarios");
            UsuarioActionFilter.ObtenerUsuario(userInfo.CorreoElectronico);
            log.Info("Autenticación Exitosa");
            return RedirectToAction("Index", "Home");
        }
    }
}