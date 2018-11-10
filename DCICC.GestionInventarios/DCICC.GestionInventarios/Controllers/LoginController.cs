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
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Mètodo para mostrar la vista Login.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Método POST para recibir los datos provenientes de la vista Login.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Login userInfo)
        {
            MenuActionFilter.ObtenerMenu("Admin");
            UsuarioActionFilter.ObtenerUsuario(userInfo.CorreoElectronico);
            Logs.Info("Autenticación Exitosa");
            return RedirectToAction("Index", "Home");
        }
    }
}