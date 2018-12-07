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
        #region Vistas (GET)
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
        #endregion
        #region Registros (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoRol.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoRol(Roles infoRol)
        {
            string mensajesRoles = string.Empty;
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                RolesAccDatos objRolesAccDatos = new RolesAccDatos((string)Session["NickUsuario"]);
                msjRoles = objRolesAccDatos.RegistrarRol(infoRol);
                if (msjRoles.OperacionExitosa)
                {
                    mensajesRoles = string.Format("El rol \"{0}\" ha sido registrado exitosamente.",infoRol.NombreRol);
                    TempData["Mensaje"] = mensajesRoles;
                    Logs.Info(mensajesRoles);
                }
                else
                {
                    mensajesRoles = string.Format("No se ha podido registrar el rol \"{0}\": {1}",infoRol.NombreRol,msjRoles.MensajeError);
                    TempData["MensajeError"] = mensajesRoles;
                    Logs.Error(mensajesRoles);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesRoles, e.Message));
                return View();
            }
            return RedirectToAction("ModificarRol", "Roles");
        }
        #endregion
        #region Actualizaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarRol.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarRol(Roles infoRol)
        {
            string mensajesRoles = string.Empty;
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                RolesAccDatos objRolesAccDatos = new RolesAccDatos((string)Session["NickUsuario"]);
                //msjRoles = objRolesAccDatos.(infoRol);
                if (msjRoles.OperacionExitosa)
                {
                    mensajesRoles = string.Format("El rol con ID: {0} ha sido modificado exitosamente.",infoRol.IdRol);
                    Logs.Info(mensajesRoles);
                }
                else
                {
                    mensajesRoles = string.Format("No se ha podido modificar el rol con ID: {0}: {1}",infoRol.NombreRol,msjRoles.MensajeError);
                    Logs.Error(mensajesRoles);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesRoles, e.Message));
            }
            return Json(msjRoles, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener los Roles habilitados de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerRolesHab()
        {
            RolesAccDatos objRolesAccDatos = new RolesAccDatos((string)Session["NickUsuario"]);
            return Json(objRolesAccDatos.ObtenerRoles("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}