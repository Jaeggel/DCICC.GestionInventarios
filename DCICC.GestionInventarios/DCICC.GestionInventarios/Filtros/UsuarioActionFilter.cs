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
        /// <summary>
        /// Método para obtener el string del usuario que ha accedido en el sistema para ser mostrado en las vistas (Admin o Usuarios).
        /// </summary>
        /// <param name="menu"></param>
        public static void ObtenerUsuario(string usuario)
        {
            usuario_Login = usuario;
        }

        /// <summary>
        /// Método para definir el ViewBag.UsuarioLogin que será utilizado en todas las vistas de la aplicación.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.UsuarioLogin = usuario_Login;
        }
    }
}