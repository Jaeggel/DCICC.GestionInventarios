using DCICC.GestionInventarios.AccesoDatos.UsuariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0)]
    public class RolesController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoRol
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoRol()
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
        /// Método (GET) para mostrar la vista ModificarRol
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarRol()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoRol.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoRol(Roles infoRol)
        {
            return RedirectToAction("ModificarRol", "Roles");
        }
        /// <summary>
        /// Método para obtener los roles habilitados de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerRolesHab()
        {
            RolesAccDatos objRolesAccDatos = new RolesAccDatos();
            return Json(objRolesAccDatos.ObtenerRolesHab(), JsonRequestBehavior.AllowGet);
        }
    }
}