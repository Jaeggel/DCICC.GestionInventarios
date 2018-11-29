using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using System;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class RolesController : Controller
    {
        //Instancia para la utilización de LOGS en la clase RolesController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoRol
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoRol()
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
        /// <summary>
        /// Método (GET) para mostrar la vista ModificarRol
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarRol()
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
                RolesAccDatos objRolesAccDatos = new RolesAccDatos((string)Session["NickUsuario"]);
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
            }
            catch (Exception e)
            {
                Logs.Error(mensajes_Roles + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarRol", "Roles");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarRol.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarRol(Roles infoRol)
        {
            string mensajes_Roles = string.Empty;
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                RolesAccDatos objRolesAccDatos = new RolesAccDatos((string)Session["NickUsuario"]);
                msjRoles = objRolesAccDatos.RegistrarRol(infoRol);
                if (msjRoles.OperacionExitosa)
                {
                    mensajes_Roles = "El rol ha sido modificado exitosamente.";
                    Logs.Info(mensajes_Roles);
                }
                else
                {
                    mensajes_Roles = "No se ha podido modificar el rol: " + msjRoles.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajes_Roles + ": " + e.Message);
            }
            return RedirectToAction("ModificarRol", "Roles");
        }
        /// <summary>
        /// Método para obtener los Roles habilitados de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerRolesHab()
        {
            RolesAccDatos objRolesAccDatos = new RolesAccDatos((string)Session["NickUsuario"]);
            return Json(objRolesAccDatos.ObtenerRoles("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}