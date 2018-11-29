using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
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
        public ActionResult Logs()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
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
            return Json(objLogsAccDatos.ObtenerLogsComp().ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}