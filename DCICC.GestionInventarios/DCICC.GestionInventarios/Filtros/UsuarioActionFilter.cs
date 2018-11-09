using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Filtros
{
    public class UsuarioActionFilter : ActionFilterAttribute
    {
        static string usuarioLogin = string.Empty;
        public static void ObtenerUsuario(string usuario)
        {
            usuarioLogin = usuario;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.UsuarioLogin = usuarioLogin;
        }
    }
}