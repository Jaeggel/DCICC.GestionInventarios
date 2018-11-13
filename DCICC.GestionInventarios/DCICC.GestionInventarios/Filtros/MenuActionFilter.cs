using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Filtros
{
    public class MenuActionFilter : ActionFilterAttribute
    {
        static string opcion_Menu=string.Empty;
        /// <summary>
        /// Método para obtener el string del menú a utilizar en las vistas (Admin o Usuarios).
        /// </summary>
        /// <param name="menu"></param>
        public static void ObtenerMenu(string menu)
        {
            opcion_Menu=menu;
        }

        /// <summary>
        /// Método para definir el ViewBag.Menu que será utilizado en todas las vistas de la aplicación.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Menu = opcion_Menu;
        }
    }
}