using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Filtros
{
    public class UsuarioActionFilter : ActionFilterAttribute
    {
        static string usuario_Login = string.Empty;
        public static void ObtenerUsuario(string usuario)
        {
            usuario_Login = usuario;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.UsuarioLogin = usuario_Login;
        }
    }
}