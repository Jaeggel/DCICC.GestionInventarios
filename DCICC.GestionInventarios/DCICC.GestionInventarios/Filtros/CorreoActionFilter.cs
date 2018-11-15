using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Filtros
{
    public class CorreoActionFilter : ActionFilterAttribute
    {
        static string opcion_Correo = string.Empty;
        /// <summary>
        /// Método para obtener el string del correo a utilizar en las vistas (Admin o Usuarios).
        /// </summary>
        /// <param name="correo"></param>
        public static void ObtenerCorreo(string correo)
        {
            opcion_Correo = correo;
        }
        /// <summary>
        /// Método para definir el ViewBag.Correo que será utilizado en todas las vistas de la aplicación.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Correo = opcion_Correo;
        }
    }
}