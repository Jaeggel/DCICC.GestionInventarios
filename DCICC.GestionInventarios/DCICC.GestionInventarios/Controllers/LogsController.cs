using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class LogsController : Controller
    {
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista Logs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logs()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            { 
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todos los Logs de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerLogsComp()
        {           
            LogsAccDatos objLogsAccDatos = new LogsAccDatos((string)Session["NickUsuario"]);
            var jsonResult = Json(objLogsAccDatos.ObtenerLogs("Comp"), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        /// <summary>
        /// Método para obtener todos los Logs de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerLogs()
        {
            LogsAccDatos objLogsAccDatos = new LogsAccDatos((string)Session["NickUsuario"]);
            var jsonResult = Json(objLogsAccDatos.ObtenerLogs(null), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion
    }
}