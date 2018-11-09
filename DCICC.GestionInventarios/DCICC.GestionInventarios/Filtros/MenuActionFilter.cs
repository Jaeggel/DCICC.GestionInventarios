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
        public static void ObtenerMenu(string menu)
        {
            opcion_Menu=menu;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Menu = opcion_Menu;
        }
    }
}