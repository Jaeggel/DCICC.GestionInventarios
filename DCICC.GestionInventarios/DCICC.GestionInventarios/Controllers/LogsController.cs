using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0)]
    public class LogsController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista Logs
        /// </summary>
        /// <returns></returns>
        public ActionResult Logs()
        {
            if (Session["userInfo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// Método para obtener todos los logs de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerLogsComp()
        {
            LogsAccDatos objLogsAccDatos = new LogsAccDatos();
            return Json(objLogsAccDatos.ObtenerLogsComp().ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}