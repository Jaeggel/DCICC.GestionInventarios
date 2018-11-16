using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
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
        //Instancia para la utilización de LOGS en la clase Roles
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            string mensajes_Roles = string.Empty;
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                RolesAccDatos objRolesAccDatos = new RolesAccDatos();
                msjRoles = objRolesAccDatos.RegistrarRol(infoRol);
                if (msjRoles.OperacionExitosa)
                {
                    mensajes_Roles = "El rol ha sido registrado exitosamente.";
                    TempData["Mensaje"] = mensajes_Roles;
                    Logs.Info(mensajes_Roles);
                }
                else
                {
                    mensajes_Roles = "No se ha podido registrar el rol: " + msjRoles.MensajeError;
                    TempData["MensajeError"] = mensajes_Roles;
                }
                return RedirectToAction("ModificarRol", "Roles");
            }
            catch (Exception e)
            {
                Logs.Error(mensajes_Roles + ": " + e.Message);
                return View();
            }
        }
        /// <summary>
        /// Método para obtener los roles habilitados de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerRolesHab()
        {
            RolesAccDatos objRolesAccDatos = new RolesAccDatos();
            return Json(objRolesAccDatos.ObtenerRolesHab().ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}