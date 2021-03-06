﻿using DCICC.GestionInventarios.Controllers;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Configuration
{
    public class SessionExpireFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        /// <summary>
        /// Método para controlar la expiración de la sesión
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["NickUsuario"] == null)
            {
                LoginController obj = new LoginController();
                //obj.RegistroSesionLogs("Logout");
                filterContext.Result = new RedirectResult("~/Login/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}